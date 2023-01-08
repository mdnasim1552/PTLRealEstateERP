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
//using RealERPRPT;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_81_Hrm.F_84_Lea
{
    public partial class RptEmpLeaveStatus02 : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        Common compUtility = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                if (dr1.Length==0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();

                this.GetDateSet();
                this.GetCompName();
                        
                this.ViewSaction();

                if (hst["comcod"].ToString().Substring(0, 1) == "8")
                {
                    this.comlist.Visible = true;
                    this.Company();
                }



            }


        }

        private void GetDateSet()
        {
            string comcod = this.GetCompCode();

            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please Setup Start Date Firstly!" + "');", true);
                return;
            }
            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "26" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            //this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
            //this.txtfrmDate.Text = startdate + this.txtfrmDate.Text.Trim().Substring(2);
            //this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

            switch (comcod)
            {
                case "3330":
                case "3355":
                case "3365":
                    this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfrmDate.Text = startdate + this.txtfrmDate.Text.Trim().Substring(2);
                    this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;

                default:
                    this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfrmDate.Text = startdate + this.txtfrmDate.Text.Trim().Substring(2);
                    this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void ViewSaction()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "EmpLeaveStatus":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "MonWiseLeave":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;


                case "yearlylvRegister":

                    DateTime curdate = System.DateTime.Today;
                    string pyear =Convert.ToString( Convert.ToInt32(curdate.ToString("yyyy"))-1);
                    //string frmDate = "01-"+Convert.ToDateTime(curdate).ToString("MMM-yyyy");
                    this.txtfrmDate.Text = Convert.ToDateTime("26-Dec-" + pyear).ToString("dd-MMM-yyyy");
                    //this.txttoDate.Text = Convert.ToDateTime(frmDate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");                    
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "DateRange":
                    this.MultiView1.ActiveViewIndex = 3;

                    break;





            }


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = hst["comcod"].ToString();
            comcod = this.ddlComName.SelectedValue.Length > 0 ? this.ddlComName.SelectedValue.ToString() : comcod;
            return comcod;
        }

        private void Company()
        {
            string comcod = this.GetCompCode();
            string consolidate = "";
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", "COMPLIST", consolidate, "", "", "", "", "", "", "", "");
            this.ddlComName.DataTextField = "comsnam";
            this.ddlComName.DataValueField = "comcod";
            this.ddlComName.DataSource = ds1.Tables[0];
            this.ddlComName.DataBind();

        }

        private void GetCompName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = "%";           
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlCompanyName.DataTextField = "actdesc";
            this.ddlCompanyName.DataValueField = "actcode";
            this.ddlCompanyName.DataSource = ds1.Tables[0];
            this.ddlCompanyName.DataBind();
            ds1.Dispose();
            this.ddlCompanyName_SelectedIndexChanged(null, null);




        }

        private void GetDepartment()
        {
            if (this.ddlCompanyName.Items.Count == 0)
                return;
            string comcod = this.GetCompCode();
            string txtCompanyname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSearchDept = "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            this.ddlDepartment_SelectedIndexChanged(null, null);
        }
        private void GetSection()
        {
            if (this.ddlCompanyName.Items.Count == 0)
                return;
            string comcod = this.GetCompCode();
            string txtCompanyname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string Department = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 8) + "%";
            string txtSearchDept = "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETSECTIONDP", txtCompanyname, Department, txtSearchDept, "", "", "", "", "", "");
            this.DropCheck1.DataTextField = "actdesc";
            this.DropCheck1.DataValueField = "actdesc";
            this.DropCheck1.DataSource = ds1.Tables[0];
            this.DropCheck1.DataBind();
            ds1.Dispose();

        }

        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();

        }
        protected void ibtnFindCompany_Click(object sender, EventArgs e)
        {
            this.GetCompName();
        }

        protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }

        protected void imgbtnSection_Click(object sender, EventArgs e)
        {
            this.GetSection();
        }

        protected void imgbtnSearchEmployee_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }


        private void ShowData()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "EmpLeaveStatus":
                    this.ShowEmpLeaveStatus();
                    break;

                case "MonWiseLeave":
                    this.ShowMonLeave();
                    break;

                case "yearlylvRegister":
                    this.ShowyleaveRegis() ;
                    break;

                case "DateRange":
                    this.ShowDateRange();
                    break;




            }



        }

        private void ShowDateRange()
        {
            Session.Remove("tblover");
            string comcod = this.GetCompCode();
            string compname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 8) + "%";
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

            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            string calltype = "LEAVEDATERANGE";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", calltype, compname, deptname, section, frmdate, todate, Empcode, "", "", "");
            if (ds2 == null ||ds2.Tables.Count==0)
            {
                this.gvDateRange.DataSource = null;
                this.gvDateRange.DataBind();
                return;
            }
          
            Session["tblover"] =ds2.Tables[0];
            Session["tbldaterng"] = ds2.Tables[0];
            this.Data_Bind();


        }


        private void ShowEmpLeaveStatus()
        {
            Session.Remove("tblover");
            string comcod = this.GetCompCode();
            string compname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";

            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            //string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

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

            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "RPTCOMLEAVESTATUS", compname, deptname, section, frmdate, todate, Empcode, "", "", "");
            if (ds2 == null)
            {
                this.gvLeaveStatus.DataSource = null;
                this.gvLeaveStatus.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }


        private void ShowMonLeave()
        {
            Session.Remove("tblover");
            string comcod = this.GetCompCode();
            string compname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 8) + "%";
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

            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            string calltype = (comcod == "3330") ? "RPTYEARLYEMPLEAVEBR" : "RPTYEARLYEMPLEAVE";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", calltype, compname, deptname, section, frmdate, todate, Empcode, "", "", "");
            if (ds2 == null)
            {
                this.gvMonEmpLeave.DataSource = null;
                this.gvMonEmpLeave.DataBind();
                return;
            }
            Session["tblover"] = ds2.Tables[0];
            this.Data_Bind();


        }


        private void ShowyleaveRegis()
        {
            Session.Remove("tblover");
            string comcod = this.GetCompCode();
            string compname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 8) + "%";
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

            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVE_SUMMARY", "RPTYEARLYEMPLEAVE", compname, deptname, section, frmdate, todate, Empcode, "", "", "");
            if (ds2 == null)
            {
                this.gvMonEmpLeave.DataSource = null;
                this.gvMonEmpLeave.DataBind();
                return;
            }
            Session["tblover"] = ds2.Tables[0];
            this.Data_Bind();


        }

        




        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;



            string empid = dt1.Rows[0]["empid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["empid"].ToString() == empid)
                {

                    dt1.Rows[j]["section"] = "";
                    dt1.Rows[j]["empname"] = "";
                    dt1.Rows[j]["idcardno"] = "";
                    dt1.Rows[j]["desig"] = "";
                    dt1.Rows[j]["section"] = "";
                    dt1.Rows[j]["joindate"] = "";
                    dt1.Rows[j]["gssal"] = 0;
                    dt1.Rows[j]["rowid"] = 0;
                    dt1.Rows[j]["lstrtdat"] = "";

                }

                else
                {



                    if (dt1.Rows[j]["empid"].ToString() == empid)
                    {
                        dt1.Rows[j]["empname"] = "";
                        dt1.Rows[j]["idcardno"] = "";
                        dt1.Rows[j]["desig"] = "";
                        dt1.Rows[j]["section"] = "";
                        dt1.Rows[j]["joindate"] = "";
                        dt1.Rows[j]["gssal"] = 0;
                        dt1.Rows[j]["rowid"] = 0;
                        dt1.Rows[j]["lstrtdat"] = "";

                    }
                }



                empid = dt1.Rows[j]["empid"].ToString();
            }
            return dt1;

        }




        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblover"];
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "EmpLeaveStatus":
                    this.gvLeaveStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvLeaveStatus.DataSource = dt;
                    this.gvLeaveStatus.DataBind();
                    break;

                case "MonWiseLeave":

                    for (int i = 5; i < 17; i++)
                        this.gvMonEmpLeave.Columns[i].Visible = false;

                    DateTime datefrm = Convert.ToDateTime(this.txtfrmDate.Text.Trim());
                    DateTime dateto = Convert.ToDateTime(this.txttoDate.Text.Trim());
                    for (int i = 5; i < 17; i++)
                    {
                        if (datefrm > dateto)
                            break;
                        this.gvMonEmpLeave.Columns[i].Visible = true;
                        this.gvMonEmpLeave.Columns[i].HeaderText = datefrm.ToString("MMM");
                        datefrm = datefrm.AddMonths(1);

                    }


                    this.gvMonEmpLeave.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvMonEmpLeave.DataSource = dt;
                    this.gvMonEmpLeave.DataBind();
                    string comcod = this.GetCompCode();
                    if (comcod == "3330")
                    {
                        this.gvMonEmpLeave.Columns[20].Visible = false;
                        this.gvMonEmpLeave.Columns[21].Visible = true;

                    }

                    else
                    {
                        this.gvMonEmpLeave.Columns[20].Visible = true; ;
                        this.gvMonEmpLeave.Columns[21].Visible = false;

                    }
                    break;



                case "yearlylvRegister":
                    this.gvyearlylv.DataSource = dt;
                    this.gvyearlylv.DataBind();


                    if (dt.Rows.Count > 0)
                    {
                        Session["Report1"] = gvyearlylv;
                        ((HyperLink)this.gvyearlylv.FooterRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    }


                    break;

                case "DateRange":
                    this.gvDateRange.DataSource = dt;
                    this.gvDateRange.DataBind();
                    break;




            }




        }


        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSection();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "EmpLeaveStatus":
                    this.PrintEmpLeaveStatus();
                    break;

                case "MonWiseLeave":
                    this.PrintMonLeave();
                    break;

                case "DateRange":
                    this.PrintDateRange();
                    break;
                case "yearlylvRegister":
                    this.PrintYearlyLv();
                    break;


            }




        }
        private void PrintYearlyLv()
        {
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            string yr = Convert.ToDateTime(this.txttoDate.Text).ToString("MMM-yy");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptLeaveRegister>();

            LocalReport Rpt1 = new LocalReport();


            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_84_Lea.RptLeaveRegister", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", " Leave Register (Yearly)"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Period: " + frmdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtaddress", comadd));
            Rpt1.SetParameters(new ReportParameter("yr", yr));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));




            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintDateRange()
        {
            DataTable dt = (DataTable)Session["tbldaterng"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString(); 
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
           string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveStatus>();

            LocalReport Rpt1 = new LocalReport();


            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_84_Lea.RptLeaveDateRange", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", " Leave Report"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Period: " + frmdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtaddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));




            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintEmpLeaveStatus()
        {

            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveStatus>();

            LocalReport Rpt1 = new LocalReport();

            switch (comcod)
            {
                // case "3101":
                case "3330":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_84_Lea.RptAllEmpLeavStatusBR", list, null, null);
                    break;


                default:
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_84_Lea.RptAllEmpLeavStatus", list, null, null);
                    break;



            }



            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            //  Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", " Increment Information Status"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Period: " + frmdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtaddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));




            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void PrintMonLeave()
        {
            string comcod = this.GetCompCode();   //GetComeCode();
            switch (comcod)
            {
                case "3315":
                case "4101":
                    this.PrintEmpMonLeaveAssure();
                    break;
                case "3101":
                case "3330":
                    this.PrintEmpMonLeaveBR();
                    break;

                default:
                    this.PrintMonLeaveGen();                   //PrintMonLeave();
                    break;
            }
        }



        private void PrintEmpMonLeaveBR()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string subtitle = "Employee Leave - Month Wise";
            string userinf = ASTUtility.Concat(comname, username, session, printdate);
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblover"];




            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptMonWiseEmpLeave>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_84_Lea.RptMonWiseLeaveBR", list, null, null);
            string subtitle_date = "(From  " + frmdate + " To " + todate + ")";


            Rpt1.SetParameters(new ReportParameter("comnam", comname));
            Rpt1.SetParameters(new ReportParameter("subtitle", subtitle));
            Rpt1.SetParameters(new ReportParameter("userinf", userinf));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("subtitle_date", subtitle_date));




            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //DataTable dt = (DataTable)Session["tblover"];
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comname = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");

            //ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_84_Lea.RptMonWiseEmpLeaveBR();
            ////TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            ////CompName.Text = this.ddlCompanyName.SelectedItem.Text;
            ////TextObject txtaddress = rpcp.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            ////txtaddress.Text = comadd;
            //TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtccaret.Text = "(From  " + frmdate + " To " + todate + ")";


            //DateTime datefrm = Convert.ToDateTime(this.txtfrmDate.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txttoDate.Text.Trim());
            //for (int i = 1; i <= 12; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    TextObject rpttxth = rpcp.ReportDefinition.ReportObjects["txtlv" + i.ToString()] as TextObject;
            //    rpttxth.Text = datefrm.ToString("MMM");
            //    datefrm = datefrm.AddMonths(1);

            //}

            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rpcp.SetDataSource(dt);
            //Session["Report1"] = rpcp;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintEmpMonLeaveAssure()

        {
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");



            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");

            ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_84_Lea.RptMonWiseEmpLeaveAssure_();
            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //CompName.Text = this.ddlCompanyName.SelectedItem.Text;
            //TextObject txtaddress = rpcp.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            //txtaddress.Text = comadd;
            TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtccaret.Text = "(From  " + frmdate + " To " + todate + ")";


            DateTime datefrm = Convert.ToDateTime(this.txtfrmDate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttoDate.Text.Trim());
            for (int i = 1; i <= 12; i++)
            {
                if (datefrm > dateto)
                    break;
                TextObject rpttxth = rpcp.ReportDefinition.ReportObjects["txtlv" + i.ToString()] as TextObject;
                rpttxth.Text = datefrm.ToString("MMM");
                datefrm = datefrm.AddMonths(1);

            }

            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rpcp.SetDataSource(dt);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintMonLeaveGen()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string subtitle = "Employee Leave - Month Wise";
            string userinf = ASTUtility.Concat(comname, username, session, printdate);

            DataTable dt = (DataTable)ViewState["tblover"];




            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptMonWiseEmpLeave>();
            // var list2 = dt1.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptEmpInfoData>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_81_Hrm.R_84_Lea.RptMonWiseEmpLeave", list, null, null);


            Rpt1.SetParameters(new ReportParameter("comnam", comname));
            Rpt1.SetParameters(new ReportParameter("subtitle", subtitle));
            Rpt1.SetParameters(new ReportParameter("userinf", userinf));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));




            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //DataTable dt = (DataTable)Session["tblover"];
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comname = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");

            //ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_84_Lea.RptMonWiseEmpLeave();
            ////TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            ////CompName.Text = this.ddlCompanyName.SelectedItem.Text;
            ////TextObject txtaddress = rpcp.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            ////txtaddress.Text = comadd;
            //TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtccaret.Text = "(From  " + frmdate + " To " + todate+")";


            //DateTime datefrm = Convert.ToDateTime(this.txtfrmDate.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txttoDate.Text.Trim());
            //for (int i = 1; i <= 12; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    TextObject rpttxth = rpcp.ReportDefinition.ReportObjects["txtlv" + i.ToString()] as TextObject;
            //    rpttxth.Text = datefrm.ToString("MMM");
            //    datefrm = datefrm.AddMonths(1);

            //}

            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rpcp.SetDataSource(dt);
            //Session["Report1"] = rpcp;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }


        protected void gvLeaveStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvLeaveStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvLeaveStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label Description = (Label)e.Row.FindControl("lblgvDescription");
                Label opnleave = (Label)e.Row.FindControl("lblgvopnleave");
                Label leaveentitled = (Label)e.Row.FindControl("lblgvleaveentitled");
                Label leaveenjoy = (Label)e.Row.FindControl("lblgvleaveenjoy");
                Label leavebal = (Label)e.Row.FindControl("lblgvleavebal");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 3) == "AAA")
                {

                    Description.Font.Bold = true;
                    opnleave.Font.Bold = true;
                    leaveentitled.Font.Bold = true;
                    leaveenjoy.Font.Bold = true;
                    leavebal.Font.Bold = true;
                    Description.Style.Add("text-align", "right");
                }

            }
        }
        protected void gvMonEmpLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMonEmpLeave.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvyearlylv_RowCreated(object sender, GridViewRowEventArgs e)
        {

            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {

                if (this.txtfrmDate.Text.Trim().Length == 0)
                    return;


                string year = Convert.ToDateTime(this.txtfrmDate.Text).ToString("yyyy");
                string uptoachive = Convert.ToDateTime(this.txttoDate.Text).ToString("MMM-yy");

                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //  gvrow.Cells.Remove(TableCell [0]);

                //TableCell cell01 = new TableCell();
                //cell01.Text = "Sl";
                //cell01.HorizontalAlign = HorizontalAlign.Center;
                //cell01.RowSpan = 2;
                //gvrow.Cells.Add(cell01);



                TableCell cell02 = new TableCell();
                cell02.Text = "Employee Details";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.Attributes["style"] = "font-weight:bold;";
                cell02.ColumnSpan = 6;
                gvrow.Cells.Add(cell02);

                TableCell cell03 = new TableCell();
                cell03.Text = "Standard Leave-"+ year;
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.Attributes["style"] = "font-weight:bold;";
                cell03.ColumnSpan = 3;
                gvrow.Cells.Add(cell03);



                TableCell cell04 = new TableCell();
                cell04.Text = "Achieved upto "+ uptoachive;
                cell04.Attributes["style"] = "font-weight:bold;";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 3;
                gvrow.Cells.Add(cell04);

                TableCell cell05 = new TableCell();
                cell05.Text = "Jan";                
                cell05.Attributes["style"] = "font-weight:bold; text-align:center;";
                cell05.ColumnSpan = 3;
                gvrow.Cells.Add(cell05);

                TableCell cell06 = new TableCell();
                cell06.Text = "FEB";
                cell06.Attributes["style"] = "font-weight:bold; text-align:center;";
                cell06.ColumnSpan = 3;
                gvrow.Cells.Add(cell06);

                TableCell cell07 = new TableCell();
                cell07.Text = "Mar";
                cell07.Attributes["style"] = "font-weight:bold; text-align:center;";
                cell07.ColumnSpan = 3;
                gvrow.Cells.Add(cell07);
                
                TableCell cell08 = new TableCell();
                cell08.Text = "Apr";
                cell08.Attributes["style"] = "font-weight:bold; text-align:center;";
                cell08.ColumnSpan = 3;
                gvrow.Cells.Add(cell08);

                TableCell cell09 = new TableCell();
                cell09.Text = "May";
                cell09.Attributes["style"] = "font-weight:bold; text-align:center;";
                cell09.ColumnSpan = 3;
                gvrow.Cells.Add(cell09);

                TableCell cell10 = new TableCell();
                cell10.Text = "Jun";
                cell10.Attributes["style"] = "font-weight:bold; text-align:center;";
                cell10.ColumnSpan = 3;
                gvrow.Cells.Add(cell10);

                TableCell cell11 = new TableCell();
                cell11.Text = "Jul";
                cell11.Attributes["style"] = "font-weight:bold; text-align:center;";
                cell11.ColumnSpan = 3;
                gvrow.Cells.Add(cell11);

                TableCell cell12 = new TableCell();
                cell12.Text = "Aug";
                cell12.Attributes["style"] = "font-weight:bold; text-align:center;";
                cell12.ColumnSpan = 3;
                gvrow.Cells.Add(cell12);

                TableCell cell13 = new TableCell();
                cell13.Text = "Sep";
                cell13.Attributes["style"] = "font-weight:bold; text-align:center;";
                cell13.ColumnSpan = 3;
                gvrow.Cells.Add(cell13);

                TableCell cell14 = new TableCell();
                cell14.Text = "Oct";
                cell14.Attributes["style"] = "font-weight:bold; text-align:center;";
                cell14.ColumnSpan = 3;
                gvrow.Cells.Add(cell14);

                TableCell cell15 = new TableCell();
                cell15.Text = "Nov";
                cell15.Attributes["style"] = "font-weight:bold; text-align:center;";
                cell15.ColumnSpan = 3;
                gvrow.Cells.Add(cell15);

                TableCell cell16 = new TableCell();
                cell16.Text = "Dec";
                cell16.Attributes["style"] = "font-weight:bold; text-align:center;";
                cell16.ColumnSpan = 3;
                gvrow.Cells.Add(cell16);


                TableCell cell17 = new TableCell();
                cell17.Text = "Total Enjoyed upto "+ uptoachive;
                cell17.Attributes["style"] = "font-weight:bold; text-align:center;";
                cell17.ColumnSpan = 3;
                gvrow.Cells.Add(cell17);


                TableCell cell18 = new TableCell();
                cell18.Text = "Supplus/(Excess)";
                cell18.Attributes["style"] = "font-weight:bold; text-align:center;";
                cell18.ColumnSpan = 3;
                gvrow.Cells.Add(cell18);

                TableCell cell19 = new TableCell();
                cell19.Text = "Balance In Hand";
                cell19.Attributes["style"] = "font-weight:bold; text-align:center;";
                cell19.ColumnSpan = 3;
                gvrow.Cells.Add(cell19);
                gvyearlylv.Controls[0].Controls.AddAt(0, gvrow);

            }
        }

        protected void gvyearlylv_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    e.Row.Cells[0].Visible = false;
               

            //}

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();
                Label lblgvempnamemwiseylv = (Label)e.Row.FindControl("lblgvempnamemwiseylv");

                if (empid.Length == 0)
                    return;

                if (empid == "000000000000")
                {

                    lblgvempnamemwiseylv.Attributes["style"] = "font-weight:bold; font-size:14px;color:blue;";



                }





            }

        }

        protected void gvDateRange_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}