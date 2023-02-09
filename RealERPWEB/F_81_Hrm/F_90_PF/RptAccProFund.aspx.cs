using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;

namespace RealERPWEB.F_81_Hrm.F_90_PF
{
    public partial class RptAccProFund : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.SelectView();
                //((Label)this.Master.FindControl("lblTitle")).Text = "PF Account";
                string type = this.Request.QueryString["Type"].ToString().Trim();
                //((Label)this.Master.FindControl("lblTitle")).Text = (type == "Pffund") ? "PF FUND" : "Month Wise Salary Report";

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01-Jan-" + date.Substring(7);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(12).ToString("dd-MMM-yyyy");
                this.GetCompany();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Pffund":
                    this.MultiView1.ActiveViewIndex = 0;


                    break;

                case "Salary":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.dchksum.Visible = true;
                    break;




            }
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
            this.GetDeptName();
        }

        protected void imgbtnSecSrch_Click(object sender, EventArgs e)
        {
            this.SectionName();
        }

        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            //DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PFACCOUNTS", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.ddlCompany_SelectedIndexChanged(null, null);
            ds1.Dispose();

        }

        private void GetDeptName()
        {

            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";

            string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");

            //DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PFACCOUNTS", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);

        }
        private void SectionName()
        {

            string comcod = this.GetCompCode();
            string Company = ((this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string Department = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9)) + "%";
            string txtSSec = "%" + this.txtSrcSec.Text.Trim() + "%";
            DataSet ds2 = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "SECTIONNAMEDP", Company, Department, txtSSec, "", "", "", "", "", "");

            //DataSet ds2 = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PFACCOUNTS", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();

        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptName();
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "Salary":
                    this.MonthWiseSal();
                    break;
                case "Pffund":
                    this.LoadPfFund();
                    break;


            }

        }

        private void MonthWiseSal()
        {

            Session.Remove("tblprofund");
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfrmdate.Text.Trim()));
            if (mon > 12)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Month Less Than Equal Twelve";
                return;
            }


            string CompanyName = ((this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string projectcode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 8)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string summary = (this.chksumm.Checked) ? "Summ" : "";

            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PFACCOUNTS", "SHOWEMPYEARLYSALARY", CompanyName, projectcode, section, frmdate, todate, summary, "", "", "");
            if (ds1 == null)
            {
                this.gvProFund.DataSource = null;
                this.gvProFund.DataBind();
                return;
            }


            Session["tblprofund"] = ds1.Tables[0];
            this.Data_Bind();




        }
        private void LoadPfFund()
        {

            Session.Remove("tblprofund");
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfrmdate.Text.Trim()));
            if (mon > 12)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Month Less Than Equal Twelve";
                return;
            }

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string CompanyName = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";

            //string CompanyName =((this.ddlCompany.SelectedValue.ToString().Substring(0,2)=="00")?"": this.ddlCompany.SelectedValue.ToString().Substring(0, 2))+"%";
            string projectcode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 8)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");

            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PFACCOUNTS", "SHOWEMPPFFUNDDP", CompanyName, projectcode, section, frmdate, todate, "", "", "", "");
            if (ds1 == null)
            {
                this.gvProFund.DataSource = null;
                this.gvProFund.DataBind();
                return;
            }


            Session["tblprofund"] = ds1.Tables[0];
            this.Data_Bind();

        }


        private void Data_Bind()
        {

            //start

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "Salary":
                    DateTime datefrm = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
                    DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
                    for (int i = 4; i < 16; i++)
                    {
                        if (datefrm > dateto)
                            break;

                        this.gvsalary.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                        datefrm = datefrm.AddMonths(1);

                    }


                    this.gvsalary.DataSource = (DataTable)Session["tblprofund"];
                    this.gvsalary.DataBind();
                    this.FooterCalCulation();


                    if (this.chksumm.Checked == true)
                    {
                        this.gvsalary.Columns[1].Visible = false;
                        this.gvsalary.Columns[2].Visible = false;
                    }
                    else
                    {
                        this.gvsalary.Columns[1].Visible = true;
                        this.gvsalary.Columns[2].Visible = true;
                    }

                    break;





                case "Pffund":

                    DateTime datefrm1 = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
                    DateTime dateto1 = Convert.ToDateTime(this.txttodate.Text.Trim());
                    for (int i = 6; i < 18; i++)
                    {
                        if (datefrm1 > dateto1)
                            break;

                        this.gvProFund.Columns[i].HeaderText = datefrm1.ToString("MMM yy");
                        datefrm1 = datefrm1.AddMonths(1);

                    }


                    this.gvProFund.DataSource = (DataTable)Session["tblprofund"];
                    this.gvProFund.DataBind();
                    this.FooterCalCulation();

                    break;



            }

            // enb
            //DateTime datefrm = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
            //DateTime  dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            //for (int i = 6; i < 18; i++)
            //{
            //    if (datefrm > dateto)
            //        break;

            //    this.gvProFund.Columns[i].HeaderText = datefrm.ToString("MMM yy");
            //    datefrm = datefrm.AddMonths(1);

            //}


            //this.gvProFund.DataSource = (DataTable)Session["tblprofund"];
            //this.gvProFund.DataBind();
            //this.FooterCalCulation();


        }

        private void FooterCalCulation()
        {



            DataTable dt = (DataTable)Session["tblprofund"];

            if (dt.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "Salary":
                    //((Label)this.gvsalary.FooterRow.FindControl("lgvFOpnam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ? 0.00 : dt.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalary.FooterRow.FindControl("lgvsalFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalary.FooterRow.FindControl("lgvsalFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalary.FooterRow.FindControl("lgvsalFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalary.FooterRow.FindControl("lgvsalFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalary.FooterRow.FindControl("lgvsalFamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalary.FooterRow.FindControl("lgvsalFamt6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalary.FooterRow.FindControl("lgvsalFamt7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalary.FooterRow.FindControl("lgvsalFamt8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalary.FooterRow.FindControl("lgvsalFamt9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalary.FooterRow.FindControl("lgvsalFamt10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalary.FooterRow.FindControl("lgvsalFamt11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalary.FooterRow.FindControl("lgvsalFamt12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalary.FooterRow.FindControl("lgvsalFtoam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toam)", "")) ? 0.00 : dt.Compute("sum(toam)", ""))).ToString("#,##0;(#,##0); ");
                    Session["Report1"] = gvsalary;
                    ((HyperLink)this.gvsalary.HeaderRow.FindControl("hlbtntbCdataExel11")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;
                case "Pffund":


                    ((Label)this.gvProFund.FooterRow.FindControl("lgvFOpnam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ? 0.00 : dt.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProFund.FooterRow.FindControl("lgvFtoam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toam)", "")) ? 0.00 : dt.Compute("sum(toam)", ""))).ToString("#,##0;(#,##0); ");
                    Session["Report1"] = gvProFund;
                    ((HyperLink)this.gvProFund.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;




            }



        }


        private string CompanyPF()
        {
            string comcod = this.GetCompCode();
            string PrintPF = "";
            switch (comcod)
            {
                case "3330":
                    // case "3101":
                    PrintPF = "PrintPF01";

                    break;

                case "3333":
                case "3101":
                    PrintPF = "PrintPF02";

                    break;

                default:
                    PrintPF = "PrintGeneral";
                    break;

            }

            return PrintPF;

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {


            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "Salary":
                    this.PrintMonthWise();
                    break;
                case "Pffund":
                    this.printpffund();
                    break;



            }


            //end 





        }

        private void PrintMonthWise()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblprofund"];

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.RptMonthlySalaryTax>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptMonthWiseSalSheet", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Month Wise Salary Sheet"));
            DateTime datefrm = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            for (int i = 1; i <= 12; i++)
            {
                if (datefrm > dateto)
                    break;
                Rpt1.SetParameters(new ReportParameter("txtMonth" + i.ToString(), datefrm.ToString("MMM yy")));
                datefrm = datefrm.AddMonths(1);
            }
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void printpffund()
        {
            string printcompf = this.CompanyPF();

            if (printcompf == "PrintPF01")
            {
                this.PrintPFBridge();

            }

            else if (printcompf == "PrintPF02")
            {
                this.PrintPFAlliance();
            }

            else
            {

                this.PrintPFGeneral();


            }

        }
        private void PrintPFAlliance()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblprofund"];


            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_90_PF.PFAlliance>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptHRSetup.GetLocalReport("R_81_Hrm.R_90_PF.RptMonthWisePFAlliance", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "PF Fund"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region Old

            //ReportDocument rptpf = new RealERPRPT.R_81_Hrm.R_90_PF.RptMonthWisePFAlliance();


            //TextObject rptCname = rptpf.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject rptTxtHead = rptpf.ReportDefinition.ReportObjects["txtHead"] as TextObject;
            //rptTxtHead.Text = "PF Fund";


            //TextObject rptdate = rptpf.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rptdate.Text = "(From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";

            //DateTime datefrm = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            //for (int i = 1; i <= 12; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    TextObject rpttxth = rptpf.ReportDefinition.ReportObjects["txtMonth" + i.ToString()] as TextObject;
            //    rpttxth.Text = datefrm.ToString("MMM yy");
            //    datefrm = datefrm.AddMonths(1);

            //}



            //TextObject txtuserinfo = rptpf.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptpf.SetDataSource(dt);
            //Session["Report1"] = rptpf;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion
        }
        private void PrintPFBridge()

        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblprofund"];

            ReportDocument rptpf = new RealERPRPT.R_81_Hrm.R_90_PF.RptMonthWisePFBridge();


            TextObject rptCname = rptpf.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptCname.Text = comnam;

            TextObject rptTxtHead = rptpf.ReportDefinition.ReportObjects["txtHead"] as TextObject;
            rptTxtHead.Text = "PF Fund";


            TextObject rptdate = rptpf.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            rptdate.Text = "(From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";

            DateTime datefrm = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            for (int i = 1; i <= 12; i++)
            {
                if (datefrm > dateto)
                    break;
                TextObject rpttxth = rptpf.ReportDefinition.ReportObjects["txtMonth" + i.ToString()] as TextObject;
                rpttxth.Text = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);

            }



            TextObject txtuserinfo = rptpf.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptpf.SetDataSource(dt);
            Session["Report1"] = rptpf;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintPFGeneral()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblprofund"];

            ReportDocument rptpf = new RealERPRPT.R_81_Hrm.R_90_PF.RptMonthWisePF();


            TextObject rptCname = rptpf.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptCname.Text = comnam;

            TextObject rptTxtHead = rptpf.ReportDefinition.ReportObjects["txtHead"] as TextObject;
            rptTxtHead.Text = "PF Fund";


            TextObject rptdate = rptpf.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            rptdate.Text = "(From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";

            DateTime datefrm = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            for (int i = 1; i <= 12; i++)
            {
                if (datefrm > dateto)
                    break;
                TextObject rpttxth = rptpf.ReportDefinition.ReportObjects["txtMonth" + i.ToString()] as TextObject;
                rpttxth.Text = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);

            }



            TextObject txtuserinfo = rptpf.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptpf.SetDataSource(dt);
            Session["Report1"] = rptpf;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



    }
}