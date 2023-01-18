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
namespace RealERPWEB.F_81_Hrm.F_82_App
{
    public partial class RptEmpInformation : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "Services") ? "EMPLOYEE  SERVICES INFORMATION" : "EMPLOYEE INFORMATION";
                this.SelectView();
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.GetEmpName();
                this.GetEmpInformation();

                if (hst["comcod"].ToString().Substring(0, 1) == "8")
                {
                    this.comlist.Visible = true;
                    this.Company();
                }




            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        //public string GetCompCode()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    return (hst["comcod"].ToString());

        //}

        private void Company()
        {
            string comcod = this.GetComeCode();
            string consolidate = "";
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", "COMPLIST", consolidate, "", "", "", "", "", "", "", "");
            this.ddlComName.DataTextField = "comsnam";
            this.ddlComName.DataValueField = "comcod";
            this.ddlComName.DataSource = ds1.Tables[0];
            this.ddlComName.DataBind();

        }
        private void SelectView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Services":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "EmpAllInfo":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "EmpDyInfo":
                case "EmpDyInfo02":
                    this.GetCompany();
                    this.GetDynamcifield();
                    this.GetDesignation();
                    this.FieldVisible();
                    this.MultiView1.ActiveViewIndex = 2;
                    this.ddlSrch1_SelectedIndexChanged(null, null);
                    this.ddlSrch2_SelectedIndexChanged(null, null);
                    this.ddlSrch3_SelectedIndexChanged(null, null);
                    break;
            }

        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.ddlComName.SelectedValue.Length > 0 ? this.ddlComName.SelectedValue.ToString() : comcod;
            return comcod;

        }


        private void GetEmpInformation()
        {
            string comcod = this.GetComeCode();
            string txtSProject = "%" + this.txtEmpSrc.Text.Trim() + "%";//?("%" + this.txtEmpSrc.Text.Trim()+ "%");//:("%" + this.txtEmpSrcInfo.Text.Trim()+"%");
            string date = this.txtDate.Text.Trim();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETEMPTNAME", txtSProject, date, "", "", "", "", "", "", "");

            if (this.Request.QueryString["Type"].ToString().Trim() == "Services")
            {
                this.ddlEmpName.DataTextField = "empname";
                this.ddlEmpName.DataValueField = "empid";
                this.ddlEmpName.DataSource = ds3.Tables[0];
                this.ddlEmpName.DataBind();
            }
        }
        private void GetEmpName()
        {



            string comcod = this.GetComeCode();
            string ProjectCode = (this.txtEmpSrcInfo.Text.Trim().Length > 0) ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string txtSProject = "%" + this.txtEmpSrcInfo.Text + "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPREMPNAME", ProjectCode, txtSProject, "", "", "", "", "", "", "");
            this.ddlEmpNameAllInfo.DataTextField = "empname";
            this.ddlEmpNameAllInfo.DataValueField = "empid";
            this.ddlEmpNameAllInfo.DataSource = ds5.Tables[0];
            this.ddlEmpNameAllInfo.DataBind();
            ViewState["tblemp"] = ds5.Tables[0];
            this.GetComASecSelected();
            //string comcod = this.GetComeCode();
            //string txtSProject =(this.Request.QueryString["Type"].ToString().Trim()=="Services")?("%" + this.txtEmpSrc.Text.Trim()+ "%"):("%" + this.txtEmpSrcInfo.Text.Trim()+"%");
            //string date = this.txtDate.Text.Trim();
            //DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETEMPTNAME", txtSProject, date, "", "", "", "", "", "", "");

            //if (this.Request.QueryString["Type"].ToString().Trim() == "Services")
            //{
            //    this.ddlEmpName.DataTextField = "empname";
            //    this.ddlEmpName.DataValueField = "empid";
            //    this.ddlEmpName.DataSource = ds3.Tables[0];
            //    this.ddlEmpName.DataBind();
            //}
            //else 
            //{
            //    this.ddlEmpNameAllInfo.DataTextField = "empname";
            //    this.ddlEmpNameAllInfo.DataValueField = "empid";
            //    this.ddlEmpNameAllInfo.DataSource = ds3.Tables[0];
            //    this.ddlEmpNameAllInfo.DataBind();

            //}

        }

        private void GetDynamcifield()
        {
            ViewState.Remove("tbldyfield");
            string comcod = this.GetComeCode();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTDYNAMICFIELD", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.cblEmployee.Items.Clear();
                return;
            }

            this.cblEmployee.DataTextField = "descrip";
            this.cblEmployee.DataValueField = "code";
            this.cblEmployee.DataSource = ds4.Tables[0];
            this.cblEmployee.DataBind();
            ViewState["tbldyfield"] = ds4.Tables[0];


        }

        private void GetDesignation()
        {

            // ViewState.Remove("tbldesignation");
            string comcod = this.GetComeCode();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETDESIGNATION", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
                return;

            this.ddldesig01.DataTextField = "desig";
            this.ddldesig01.DataValueField = "desigid";
            this.ddldesig01.DataSource = ds4.Tables[0];
            this.ddldesig01.DataBind();

            this.ddldesig02.DataTextField = "desig";
            this.ddldesig02.DataValueField = "desigid";
            this.ddldesig02.DataSource = ds4.Tables[0];
            this.ddldesig02.DataBind();

            this.ddldesig03.DataTextField = "desig";
            this.ddldesig03.DataValueField = "desigid";
            this.ddldesig03.DataSource = ds4.Tables[0];
            this.ddldesig03.DataBind();

            this.ddltodesig1.DataTextField = "desig";
            this.ddltodesig1.DataValueField = "desigid";
            this.ddltodesig1.DataSource = ds4.Tables[0];
            this.ddltodesig1.DataBind();

            this.ddltodesig2.DataTextField = "desig";
            this.ddltodesig2.DataValueField = "desigid";
            this.ddltodesig2.DataSource = ds4.Tables[0];
            this.ddltodesig2.DataBind();

            this.ddltodesig3.DataTextField = "desig";
            this.ddltodesig3.DataValueField = "desigid";
            this.ddltodesig3.DataSource = ds4.Tables[0];
            this.ddltodesig3.DataBind();


            //ViewState["tbldesignation"] = ds4.Tables[0];
            ds4.Dispose();

        }

        private void FieldVisible()
        {
            this.lblSearchlist.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.ddlFieldList1.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.ddlSrch1.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.txtSearch1.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.ddlOperator1.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.ddlFieldList2.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.ddlSrch2.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.txtSearch2.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.ddlOperator2.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.ddlFieldList3.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.ddlSrch3.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.txtSearch3.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
        }

        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string txtCompany = this.txtSrcCompany.Text.Trim() + "%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETCOMPANYNAME", txtCompany, "", "", "", "", "", "", "", "");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GET_ACCESSED_COMPANYLIST", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            ds1.Dispose();

        }

        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Services":
                    this.PrintServices();
                    break;

                case "EmpAllInfo":
                    this.PrintEmpAllInfo();
                    break;

                case "EmpDyInfo":
                case "EmpDyInfo02":
                    this.PrintDyInfo();
                    break;


            }
        }
        private void PrintServices()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblservices"];
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("MMMM dd, yyyy");
            ReportDocument rptempservices = new RealERPRPT.R_81_Hrm.R_82_App.RptEmpServices();
            TextObject txtempname = rptempservices.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            txtempname.Text = "Name: " + this.ddlEmpName.SelectedItem.Text.Trim();
            TextObject rptdate = rptempservices.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "Date: " + date;
            TextObject txtuserinfo = rptempservices.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptempservices.SetDataSource(dt);
            //string comcod = this.GetComeCode();
            string comcod = GetComeCode();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptempservices.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptempservices;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        private void PrintEmpAllInfo()
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "3340":
                    this.PrintEmpAllInfoUrban();
                    break;
                case "3101":
                    this.EmployeeAllInfo();
                  
                    break;
                default:
                    this.PrintAllInfo();
                    break;
            }


            //this.ShowName();
        }

        private void EmployeeAllInfo()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPINFORMATION", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();//txtcomaddress
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string empName = this.ddlEmpNameAllInfo.SelectedItem.Text.Trim();
            string deptName = (ds1.Tables[2].Rows.Count == 0) ? "Department Name: " : "DEPARTMENT NAME: " + ds1.Tables[2].Rows[0]["empdeptdesc"].ToString();
            string netSal = Convert.ToDouble(ds1.Tables[1].Rows[0]["netsal"]).ToString("#,##0; (#,##0); ");

           // DataTable dt = (DataTable) ds1;
             LocalReport Rpt1 = new LocalReport();
            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.EmployeeAllInfo>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_82_App.RptEmployeeAllInfo", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("empName", empName));
            Rpt1.SetParameters(new ReportParameter("deptName", deptName));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Employee Information"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("date", "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }


        private void PrintEmpAllInfoUrban()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPINFORMATIONUN", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();//txtcomaddress
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("MMMM dd, yyyy");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            var list = ds1.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_81_Rec.EmpAllInformation>();
            string empName = this.ddlEmpNameAllInfo.SelectedItem.Text.Trim();
            string deptName = (ds1.Tables[2].Rows.Count == 0) ? "Department Name: " : "DEPARTMENT NAME: " + ds1.Tables[2].Rows[0]["empdeptdesc"].ToString();
            string netSal = Convert.ToDouble(ds1.Tables[1].Rows[0]["netsal"]).ToString("#,##0; (#,##0); ");

            LocalReport report = new LocalReport();

            report = RptSetupClass1.GetLocalReport("R_81_Hrm.R_82_App.RptEmpAllInformationENG", list, null, null);


            report.EnableExternalImages = true;
            report.SetParameters(new ReportParameter("compName", comnam));
            report.SetParameters(new ReportParameter("compAdd", comadd));
            report.SetParameters(new ReportParameter("empName", empName));
            report.SetParameters(new ReportParameter("deptName", deptName));
            report.SetParameters(new ReportParameter("comLogo", comLogo));
            report.SetParameters(new ReportParameter("netSal", netSal));
            report.SetParameters(new ReportParameter("rptTitle", "Employee Information"));
            report.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = report;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //ReportDocument rptempservices = new RealERPRPT.R_81_Hrm.R_82_App.RptEmpAllInformationUrban ();

            ////string empname=this.ddlEmpNameAllInfo.SelectedItem.Text.Trim().Substring(8,20);
            ////int index=empname.IndexOf("-");


            ////TextObject txtempname = rptempservices.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            ////txtempname.Text = empname.Substring(8, index);


            ////TextObject txtcomaddress = rptempservices.ReportDefinition.ReportObjects["txtcomaddress"] as TextObject;
            ////txtcomaddress.Text = comadd;

            //TextObject txtCompName = rptempservices.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //txtCompName.Text = (ds1.Tables[2].Rows.Count == 0) ? comnam : ds1.Tables[2].Rows[0]["empcomdesc"].ToString ();
            //TextObject txtempname = rptempservices.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            //txtempname.Text = ds1.Tables[2].Rows[0]["empname"].ToString ();

            //TextObject rpttxtempdept = rptempservices.ReportDefinition.ReportObjects["txtempdept"] as TextObject;
            //rpttxtempdept.Text = (ds1.Tables[2].Rows.Count == 0) ? "Department Name: " : "DEPARTMENT NAME: " + ds1.Tables[2].Rows[0]["empdeptdesc"].ToString ();
            //TextObject txtcomaddress = rptempservices.ReportDefinition.ReportObjects["txtcomaddress"] as TextObject;
            //txtcomaddress.Text = comadd;
            ////TextObject txtnetsalary = rptempservices.ReportDefinition.ReportObjects["txtnetsalary"] as TextObject;
            ////txtnetsalary.Text = Convert.ToDouble (ds1.Tables[1].Rows[0]["netsal"]).ToString ("#,##0; (#,##0); ");
            ////TextObject txtNetpayable = rptempservices.ReportDefinition.ReportObjects["txtNetpayable"] as TextObject;
            ////txtNetpayable.Text = Convert.ToDouble (ds1.Tables[1].Rows[0]["netpay"]).ToString ("#,##0; (#,##0); ");
            //rptempservices.SetDataSource (ds1.Tables[0]);
            ////string comcod = GetComeCode();
            //string ComLogo = Server.MapPath (@"~\Image\LOGO" + comcod + ".jpg");
            //rptempservices.SetParameterValue ("ComLogo", ComLogo);
            //Session["Report1"] = rptempservices;
            //((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //             ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";
        }
        private void PrintAllInfo()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
            //RPTEMPINFORMATION
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPINFORMATION", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();//txtcomaddress
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("MMMM dd, yyyy");

            ReportDocument rptempservices = new RealERPRPT.R_81_Hrm.R_82_App.RptEmpAllInformation();

            //string empname=this.ddlEmpNameAllInfo.SelectedItem.Text.Trim().Substring(8,20);
            //int index=empname.IndexOf("-");


            //TextObject txtempname = rptempservices.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            //txtempname.Text = empname.Substring(8, index);


            //TextObject txtcomaddress = rptempservices.ReportDefinition.ReportObjects["txtcomaddress"] as TextObject;
            //txtcomaddress.Text = comadd;

            TextObject txtCompName = rptempservices.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            txtCompName.Text = (ds1.Tables[2].Rows.Count == 0) ? comnam : ds1.Tables[2].Rows[0]["empcomdesc"].ToString();
            TextObject txtempname = rptempservices.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            txtempname.Text = ds1.Tables[2].Rows[0]["empname"].ToString();

            TextObject rpttxtempdept = rptempservices.ReportDefinition.ReportObjects["txtempdept"] as TextObject;
            rpttxtempdept.Text = (ds1.Tables[2].Rows.Count == 0) ? "Department Name: " : "DEPARTMENT NAME: " + ds1.Tables[2].Rows[0]["empdeptdesc"].ToString();
            TextObject txtcomaddress = rptempservices.ReportDefinition.ReportObjects["txtcomaddress"] as TextObject;
            txtcomaddress.Text = comadd;
            TextObject txtnetsalary = rptempservices.ReportDefinition.ReportObjects["txtnetsalary"] as TextObject;
            txtnetsalary.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["netsal"]).ToString("#,##0; (#,##0); ");
            TextObject txtNetpayable = rptempservices.ReportDefinition.ReportObjects["txtNetpayable"] as TextObject;
            txtNetpayable.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["netpay"]).ToString("#,##0; (#,##0); ");
            rptempservices.SetDataSource(ds1.Tables[0]);
            //string comcod = GetComeCode();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptempservices.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptempservices;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void ShowName()
        {

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPINFORMATION", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportClass rptempservices01 = new RealERPRPT.R_81_Hrm.R_82_App.RptDouPrint();

            //TextObject txtCompName = rptempservices01.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //txtCompName.Text = comnam;
            //TextObject txtcomaddress = rptempservices01.ReportDefinition.ReportObjects["txtcomaddress"] as TextObject;
            //txtcomaddress.Text = comadd;
            rptempservices01.SetDataSource(ds1.Tables[0]);
            Session["Report1"] = rptempservices01;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }


        private void PrintDyInfo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            this.printSearch();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblRptservices"];
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("MMMM dd, yyyy");
            ReportDocument rptempdyinfo = new RealERPRPT.R_81_Hrm.R_82_App.RptDynamicInfo();
            TextObject txtComName = rptempdyinfo.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            txtComName.Text = this.ddlCompany.SelectedItem.Text.Trim();

            //int j=0;
            for (int i = 0; i < this.cblEmployee.Items.Count; i++)
            {


                if (((cblEmployee.Items[i].Selected) && (cblEmployee.Items[i].Value == "comname")) || ((cblEmployee.Items[i].Selected) && (cblEmployee.Items[i].Value == "section")) || ((cblEmployee.Items[i].Selected) && (cblEmployee.Items[i].Value == "desigid")) || ((cblEmployee.Items[i].Selected) && (cblEmployee.Items[i].Value == "brcode"))) ;
                //{
                //    //string header = this.cblEmployee.Items[i].Text.Trim().Replace(" ", "_");
                //    //TextObject rpttxth = rptempdyinfo.ReportDefinition.ReportObjects[header] as TextObject;
                //    //rpttxth.Text = header.Replace("_", " ");
                //    //continue;
                //}

                else if (cblEmployee.Items[i].Selected)
                {
                    string header = this.cblEmployee.Items[i].Value;
                    string title = this.cblEmployee.Items[i].Text.Trim();
                    TextObject rpttxth = rptempdyinfo.ReportDefinition.ReportObjects[header] as TextObject;
                    rpttxth.Text = title;
                }

                else if ((cblEmployee.Items[i].Value == "comname") || (cblEmployee.Items[i].Value == "section") || (cblEmployee.Items[i].Value == "desigid")
                        || (cblEmployee.Items[i].Value == "brcode") || (cblEmployee.Items[i].Value == "Spouse") || (cblEmployee.Items[i].Value == "Grade")
                    || (cblEmployee.Items[i].Value == "Present Address") || (cblEmployee.Items[i].Value == "National Id") || (cblEmployee.Items[i].Value == "No Of Children")
                    || (cblEmployee.Items[i].Value == "Contact Number")) ;





                else
                {

                    string header = this.cblEmployee.Items[i].Value;
                    TextObject rpttxth = rptempdyinfo.ReportDefinition.ReportObjects[header] as TextObject;
                    rpttxth.Text = "";

                }
                //j++;
            }


            TextObject txtuserinfo = rptempdyinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptempdyinfo.SetDataSource(dt);
            // string comcod = this.GetComeCode();
            string comcod = GetComeCode();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptempdyinfo.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptempdyinfo;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            //this.GetEmpName();
            this.GetEmpInformation();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            ViewState.Remove("tblservices");
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string Date = this.txtDate.Text.Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPSERVICES", empid, Date, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvempservices.DataSource = null;
                this.gvempservices.DataBind();
                return;
            }
            ViewState["tblservices"] = ds1.Tables[0];
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["tblservices"];

                string Type = this.Request.QueryString["Type"].ToString().Trim();
                switch (Type)
                {
                    case "Services":
                        this.gvempservices.DataSource = dt;
                        this.gvempservices.DataBind();
                        break;

                    case "EmpDyInfo":
                    case "EmpDyInfo02":

                        int i;


                        for (i = 2; i < this.gvempDyInfo.Columns.Count; i++)
                            this.gvempDyInfo.Columns[i].Visible = false;


                        for (i = 2; i < this.cblEmployee.Items.Count; i++)
                        {
                            if (this.cblEmployee.Items[i].Selected)
                            {
                                this.gvempDyInfo.Columns[i].Visible = true;
                                this.gvempDyInfo.Columns[i].HeaderText = this.cblEmployee.Items[i].Text.Trim();

                                if (((cblEmployee.Items[i].Selected) && (cblEmployee.Items[i].Value == "comname")) || ((cblEmployee.Items[i].Selected) && (cblEmployee.Items[i].Value == "section")) || ((cblEmployee.Items[i].Selected) && (cblEmployee.Items[i].Value == "desigid")))
                                {
                                    this.gvempDyInfo.Columns[i].Visible = false;
                                }
                            }

                        }

                        this.gvempDyInfo.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvempDyInfo.DataSource = dt;
                        this.gvempDyInfo.DataBind();
                        Session["Report1"] = gvempDyInfo;
                        ((HyperLink)this.gvempDyInfo.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        break;
                }


            }

            catch (Exception ex)
            { }

        }
        protected void ibtnEmpListAllinfo_Click(object sender, EventArgs e)
        {
            this.GetEmpName();

            //ViewState.Remove ("tblemp");
            //string comcod = this.GetComeCode ();
            //string ProjectCode = this.ddlProjectName.SelectedValue.ToString ();
            //string txtSProject = "%" + this.txtEmpSrcInfo.Text + "%";
            //DataSet ds5 = HRData.GetTransInfo (comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETNEWPNAME", "", txtSProject, "", "", "", "", "", "", "");
            //this.ddlEmpNameAllInfo.DataTextField = "empname";
            //this.ddlEmpNameAllInfo.DataValueField = "empid";
            //this.ddlEmpNameAllInfo.DataSource = ds5.Tables[0];
            //this.ddlEmpNameAllInfo.DataBind ();
            //ViewState["tblemp"] = ds5.Tables[0];
        }
        protected void lbtnEmpDyInfo_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string txtjorbirdate = "";
            string txtlike = "";
            string SearchInfo = "";
            string orderinfo = "";



            if (this.ddlFieldList1.SelectedValue != "00000")
            {

                txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "joindate") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "birthdate") ? "'" : "");
                txtlike = (this.ddlSrch1.SelectedValue == "like") ? "%'" : "";

                string srch1 = "";
                if (this.ddlFieldList1.SelectedValue.ToString() == "desigid" && this.ddlSrch1.SelectedValue.ToString() == "between")
                    srch1 = this.ddltodesig1.SelectedValue.ToString() + " and " + this.ddldesig01.SelectedValue.ToString();


                else if (this.ddlSrch1.SelectedValue.ToString() == "between")

                    srch1 = this.txtSearch1.Text.Trim() + "'" + " and '" + this.txttoSearch1.Text.Trim();
                else
                    srch1 = this.txtSearch1.Text.Trim();



                SearchInfo = SearchInfo + "" + this.ddlFieldList1.SelectedValue.ToString() + " " + this.ddlSrch1.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch1.SelectedValue == "like") ? "'" : "") + srch1 + txtjorbirdate + txtlike;

            }


            if (this.ddlFieldList2.SelectedValue != "00000")
            {

                txtjorbirdate = ((this.ddlFieldList2.SelectedValue.ToString() == "joindate") ? "'" : (this.ddlFieldList2.SelectedValue.ToString() == "birthdate") ? "'" : "");
                txtlike = (this.ddlSrch2.SelectedValue == "like") ? "%'" : "";


                string srch1 = "";
                if (this.ddlFieldList2.SelectedValue.ToString() == "desigid" && this.ddlSrch2.SelectedValue.ToString() == "between")
                    srch1 = this.ddltodesig2.SelectedValue.ToString() + " and " + this.ddldesig02.SelectedValue.ToString();


                else if (this.ddlSrch2.SelectedValue.ToString() == "between")

                    srch1 = this.txtSearch2.Text.Trim() + "'" + " and '" + this.txttoSearch2.Text.Trim();
                else
                    srch1 = this.txtSearch2.Text.Trim();



                SearchInfo = SearchInfo + " " + this.ddlOperator1.SelectedValue.ToString() + " " + this.ddlFieldList2.SelectedValue.ToString() + " " + this.ddlSrch2.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch2.SelectedValue == "like") ? "'" : "") + srch1 + txtjorbirdate + txtlike;

            }

            if (this.ddlFieldList3.SelectedValue != "00000")
            {
                txtjorbirdate = ((this.ddlFieldList3.SelectedValue.ToString() == "joindate") ? "'" : (this.ddlFieldList2.SelectedValue.ToString() == "birthdate") ? "'" : "");
                txtlike = (this.ddlSrch3.SelectedValue == "like") ? "%'" : "";

                string srch1 = "";
                if (this.ddlFieldList3.SelectedValue.ToString() == "desigid" && this.ddlSrch3.SelectedValue.ToString() == "between")
                    srch1 = this.ddltodesig3.SelectedValue.ToString() + " and " + this.ddldesig03.SelectedValue.ToString();


                else if (this.ddlSrch3.SelectedValue.ToString() == "between")

                    srch1 = this.txtSearch3.Text.Trim() + "'" + " and '" + this.txttoSearch3.Text.Trim();
                else
                    srch1 = this.txtSearch3.Text.Trim();




                SearchInfo = SearchInfo + " " + this.ddlOperator2.SelectedValue.ToString() + " " + this.ddlFieldList3.SelectedValue.ToString() + " " + this.ddlSrch3.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch3.SelectedValue == "like") ? "'" : "") + srch1 + txtjorbirdate + txtlike;

            }

            if (this.ddlOrder1.SelectedValue != "00000")
            {
                orderinfo = orderinfo + " " + this.ddlOrder1.SelectedValue.ToString() + " " + this.ddlOrderad1.SelectedValue.ToString() + ", ";

            }
            if (this.ddlOrder2.SelectedValue != "00000")
            {
                orderinfo = orderinfo + " " + this.ddlOrder2.SelectedValue.ToString() + " " + this.ddlOrderad2.SelectedValue.ToString() + ", ";

            }
            if (this.ddlOrder3.SelectedValue != "00000")
            {
                orderinfo = orderinfo + " " + this.ddlOrder3.SelectedValue.ToString() + " " + this.ddlOrderad3.SelectedValue.ToString() + ",";

            }
            //fieldinfo = ASTUtility.Left(fieldinfo.Trim(), fieldinfo.Trim().Length - 1);
            SearchInfo = SearchInfo.Trim();
            if (orderinfo.Length > 0)
                orderinfo = ASTUtility.Left(orderinfo.Trim(), orderinfo.Trim().Length - 1);

            //int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);

            //string company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
            string company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "HREMPDYINFORMATION", SearchInfo, orderinfo, company, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvempDyInfo.DataSource = null;
                this.gvempDyInfo.DataBind();
                return;
            }

            ViewState["tblservices"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string comname = dt1.Rows[0]["comname"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["comname"].ToString() == comname)
                {
                    comname = dt1.Rows[j]["comname"].ToString();
                    dt1.Rows[j]["comdesc"] = "";
                }

                else
                    comname = dt1.Rows[j]["comname"].ToString();
            }

            return dt1;

        }
        private void printSearch()
        {
            string comcod = this.GetComeCode();
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string fieldinfo = "";
            string txtjorbirdate = "";
            string txtlike = "";
            string SearchInfo = "";
            string orderinfo = "";
            for (int i = 0; i < this.cblEmployee.Items.Count; i++)
            {
                if (cblEmployee.Items[i].Selected)
                {
                    fieldinfo = fieldinfo + "" + cblEmployee.Items[i].Value.ToString() + ", ";
                }


            }

            if (this.ddlFieldList1.SelectedValue != "00000")
            {

                txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "joindate") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "birthdate") ? "'" : "");
                txtlike = (this.ddlSrch1.SelectedValue == "like") ? "%'" : "";

                string srch1 = "";
                if (this.ddlFieldList1.SelectedValue.ToString() == "desigid" && this.ddlSrch1.SelectedValue.ToString() == "between")
                    srch1 = this.ddltodesig1.SelectedValue.ToString() + " and " + this.ddldesig01.SelectedValue.ToString();


                else if (this.ddlSrch1.SelectedValue.ToString() == "between")

                    srch1 = this.txtSearch1.Text.Trim() + " and " + this.txttoSearch1.Text.Trim();
                else
                    srch1 = this.txtSearch1.Text.Trim();
                SearchInfo = SearchInfo + "" + this.ddlFieldList1.SelectedValue.ToString() + " " + this.ddlSrch1.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch1.SelectedValue == "like") ? "'" : "") + srch1 + txtjorbirdate + txtlike;

            }


            if (this.ddlFieldList2.SelectedValue != "00000")
            {

                txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "joindate") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "birthdate") ? "'" : "");
                txtlike = (this.ddlSrch2.SelectedValue == "like") ? "%'" : "";

                string srch1 = "";
                if (this.ddlFieldList2.SelectedValue.ToString() == "desigid" && this.ddlSrch2.SelectedValue.ToString() == "between")
                    srch1 = this.ddltodesig2.SelectedValue.ToString() + " and " + this.ddldesig02.SelectedValue.ToString();


                else if (this.ddlSrch2.SelectedValue.ToString() == "between")

                    srch1 = this.txtSearch2.Text.Trim() + " and " + this.txttoSearch2.Text.Trim();
                else
                    srch1 = this.txtSearch2.Text.Trim();

                SearchInfo = SearchInfo + " " + this.ddlOperator1.SelectedValue.ToString() + " " + this.ddlFieldList2.SelectedValue.ToString() + " " + this.ddlSrch2.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch2.SelectedValue == "like") ? "'" : "") + srch1 + txtjorbirdate + txtlike;

            }

            if (this.ddlFieldList3.SelectedValue != "00000")
            {
                txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "joindate") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "birthdate") ? "'" : "");
                txtlike = (this.ddlSrch3.SelectedValue == "like") ? "%'" : "";

                string srch1 = "";
                if (this.ddlFieldList3.SelectedValue.ToString() == "desigid" && this.ddlSrch3.SelectedValue.ToString() == "between")
                    srch1 = this.ddltodesig3.SelectedValue.ToString() + " and " + this.ddldesig03.SelectedValue.ToString();


                else if (this.ddlSrch3.SelectedValue.ToString() == "between")

                    srch1 = this.txtSearch3.Text.Trim() + " and " + this.txttoSearch3.Text.Trim();
                else
                    srch1 = this.txtSearch3.Text.Trim();
                SearchInfo = SearchInfo + " " + this.ddlOperator2.SelectedValue.ToString() + " " + this.ddlFieldList3.SelectedValue.ToString() + " " + this.ddlSrch3.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch3.SelectedValue == "like") ? "'" : "") + srch1 + txtjorbirdate + txtlike;

            }

            if (this.ddlOrder1.SelectedValue != "00000")
            {
                orderinfo = orderinfo + " " + this.ddlOrder1.SelectedValue.ToString() + " " + this.ddlOrderad1.SelectedValue.ToString() + ", ";

            }
            if (this.ddlOrder2.SelectedValue != "00000")
            {
                orderinfo = orderinfo + " " + this.ddlOrder2.SelectedValue.ToString() + " " + this.ddlOrderad2.SelectedValue.ToString() + ", ";

            }
            if (this.ddlOrder3.SelectedValue != "00000")
            {
                orderinfo = orderinfo + " " + this.ddlOrder3.SelectedValue.ToString() + " " + this.ddlOrderad3.SelectedValue.ToString() + ",";

            }
            fieldinfo = ASTUtility.Left(fieldinfo.Trim(), fieldinfo.Trim().Length - 1);
            SearchInfo = SearchInfo.Trim();
            if (orderinfo.Length > 0)
                orderinfo = ASTUtility.Left(orderinfo.Trim(), orderinfo.Trim().Length - 1);
            string company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTHREMPDYINFORMATION", fieldinfo, SearchInfo, orderinfo, company, "", "", "", "", "");
            if (ds1 == null)
            {
                //this.gvempDyInfo.DataSource = null;
                //this.gvempDyInfo.DataBind();
                //return;
            }

            ViewState["tblRptservices"] = this.HiddenSameData(ds1.Tables[0]);
            //this.Data_Bind();
        }
        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {

            if (chkall.Checked)
            {
                for (int i = 0; i < this.cblEmployee.Items.Count; i++)
                {
                    cblEmployee.Items[i].Selected = true;

                }


            }

            else
            {
                for (int i = 0; i < this.cblEmployee.Items.Count; i++)
                {
                    cblEmployee.Items[i].Selected = false;

                }

            }
            this.cblEmployee_SelectedIndexChanged(null, null);
        }



        protected void cblEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tbldyfield"];


            for (int i = 0; i < this.cblEmployee.Items.Count; i++)
            {
                dt.Rows[i]["ffalse"] = (this.cblEmployee.Items[i].Selected) ? "True" : "False";
            }


            DataRow dr1 = dt.NewRow();
            dr1["code"] = "00000";
            dr1["descrip"] = "----selecttion---";
            dr1["ffalse"] = "True";
            dt.Rows.Add(dr1);
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("ffalse like 'True%'");

            //Search Field Option

            this.ddlFieldList1.DataTextField = "descrip";
            this.ddlFieldList1.DataValueField = "code";
            this.ddlFieldList1.DataSource = dv.ToTable();
            this.ddlFieldList1.DataBind();

            this.ddlFieldList2.DataTextField = "descrip";
            this.ddlFieldList2.DataValueField = "code";
            this.ddlFieldList2.DataSource = dv.ToTable();
            this.ddlFieldList2.DataBind();

            this.ddlFieldList3.DataTextField = "descrip";
            this.ddlFieldList3.DataValueField = "code";
            this.ddlFieldList3.DataSource = dv.ToTable();
            this.ddlFieldList3.DataBind();

            this.ddlFieldList1.SelectedValue = "00000";
            this.ddlFieldList2.SelectedValue = "00000";
            this.ddlFieldList3.SelectedValue = "00000";

            // dv.Sort="code";

            this.ddlOrder1.DataTextField = "descrip";
            this.ddlOrder1.DataValueField = "code";
            this.ddlOrder1.DataSource = dv.ToTable();
            this.ddlOrder1.DataBind();

            this.ddlOrder2.DataTextField = "descrip";
            this.ddlOrder2.DataValueField = "code";
            this.ddlOrder2.DataSource = dv.ToTable();
            this.ddlOrder2.DataBind();

            this.ddlOrder3.DataTextField = "descrip";
            this.ddlOrder3.DataValueField = "code";
            this.ddlOrder3.DataSource = dv.ToTable();
            this.ddlOrder3.DataBind();




            this.ddlOrder1.SelectedValue = "00000";
            this.ddlOrder2.SelectedValue = "00000";
            this.ddlOrder3.SelectedValue = "00000";


            dv.RowFilter = ("code not in ('00000')");
            ViewState["tbldyfield"] = dv.ToTable();






        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data_Bind();
        }

        protected void ddlSrch1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string fieldlist1 = (this.ddlFieldList1.Items.Count == 0) ? "AAAAA" : this.ddlFieldList1.SelectedValue.ToString();
            string srchlist1 = this.ddlSrch1.SelectedValue.ToString();

            if (fieldlist1 == "desigid" && srchlist1 == "between")
            {
                this.ddldesig01.Visible = true;
                this.txtSearch1.Visible = false;
                this.lbland1.Visible = true;
                this.ddltodesig1.Visible = true;
                this.txttoSearch1.Visible = false;
            }
            else if (srchlist1 == "between")
            {
                this.ddldesig01.Visible = false;
                this.txtSearch1.Visible = true;
                this.lbland1.Visible = true;
                this.ddltodesig1.Visible = false;
                this.txttoSearch1.Visible = true;
            }


            else
            {
                this.ddldesig01.Visible = false;
                this.txtSearch1.Visible = true;
                this.lbland1.Visible = false;
                this.ddltodesig1.Visible = false;
                this.txttoSearch1.Visible = false;
            }

        }
        protected void ddlSrch2_SelectedIndexChanged(object sender, EventArgs e)
        {

            string fieldlist2 = (this.ddlFieldList2.Items.Count == 0) ? "AAAAA" : this.ddlFieldList2.SelectedValue.ToString();
            string srchlist2 = this.ddlSrch2.SelectedValue.ToString();

            if (fieldlist2 == "desigid" && srchlist2 == "between")
            {
                this.ddldesig02.Visible = true;
                this.txtSearch2.Visible = false;
                this.lbland2.Visible = true;
                this.ddltodesig2.Visible = true;
                this.txttoSearch2.Visible = false;
            }
            else if (srchlist2 == "between")
            {
                this.ddldesig02.Visible = false;
                this.txtSearch2.Visible = true;
                this.lbland2.Visible = true;
                this.ddltodesig2.Visible = false;
                this.txttoSearch2.Visible = true;
            }


            else
            {
                this.ddldesig02.Visible = false;
                this.txtSearch2.Visible = true;
                this.lbland2.Visible = false;
                this.ddltodesig2.Visible = false;
                this.txttoSearch2.Visible = false;
            }
        }
        protected void ddlSrch3_SelectedIndexChanged(object sender, EventArgs e)
        {

            string fieldlist3 = (this.ddlFieldList3.Items.Count == 0) ? "AAAAA" : this.ddlFieldList3.SelectedValue.ToString();
            string srchlist3 = this.ddlSrch2.SelectedValue.ToString();

            if (fieldlist3 == "desigid" && srchlist3 == "between")
            {
                this.ddldesig03.Visible = true;
                this.txtSearch3.Visible = false;
                this.lbland3.Visible = true;
                this.ddltodesig3.Visible = true;
                this.txttoSearch3.Visible = false;
            }
            else if (srchlist3 == "between")
            {
                this.ddldesig03.Visible = false;
                this.txtSearch3.Visible = true;
                this.lbland3.Visible = true;
                this.ddltodesig3.Visible = false;
                this.txttoSearch3.Visible = true;
            }


            else
            {
                this.ddldesig03.Visible = false;
                this.txtSearch3.Visible = true;
                this.lbland3.Visible = false;
                this.ddltodesig3.Visible = false;
                this.txttoSearch3.Visible = false;
            }

        }

        protected void ddlFieldList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlSrch1_SelectedIndexChanged(null, null);
        }
        protected void ddlFieldList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlSrch2_SelectedIndexChanged(null, null);
        }
        protected void ddlFieldList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlSrch2_SelectedIndexChanged(null, null);
        }
        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {

        }

        private void GetCompanyName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = GetComeCode();


            string txtCompany = "%";// (this.Request.QueryString["Type"].ToString().Trim() == "Aggrement") ? this.txtSrcCompanyAgg.Text.Trim() + "%" : this.txtSrcCompany.Text.Trim() + "%";


            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");

            //if (this.Request.QueryString["Type"].ToString().Trim() == "Aggrement")
            //{
            this.ddlCompanyAgg.DataTextField = "actdesc";
            this.ddlCompanyAgg.DataValueField = "actcode";
            this.ddlCompanyAgg.DataSource = ds5.Tables[0];
            this.ddlCompanyAgg.DataBind();
            this.GetDepartment();
            this.ddlCompanyAgg_SelectedIndexChanged(null, null);
            //    return;
            //}
            //else if (this.Request.QueryString["Type"].ToString().Trim() == "Officetime")

            //    this.ddlCompany.DataTextField = "actdesc";
            //this.ddlCompany.DataValueField = "actcode";
            //this.ddlCompany.DataSource = ds5.Tables[0];
            //this.ddlCompany.DataBind();
            //this.ddlCompany_SelectedIndexChanged(null, null);
            //ds1.Dispose();

            //this.ddlCompany.SelectedValue = (this.Request.QueryString["empid"] == "") ? "" : this.Request.QueryString["empid"].ToString();


        }
        private void GetDepartment()
        {
            string comcod = this.GetComeCode();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string Company = ((this.ddlCompanyAgg.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, 2)) + "%";

            string txtSProject = "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEPTNAME", Company, txtSProject, "", "", "", "", "", "", "");

            this.ddldepartmentagg.DataTextField = "deptdesc";
            this.ddldepartmentagg.DataValueField = "deptcode";
            this.ddldepartmentagg.DataSource = ds4.Tables[0];
            this.ddldepartmentagg.DataBind();

            this.GetProjectName();
            //this.ddlProjectName_SelectedIndexChanged(null, null);



        }

        private void GetProjectName()
        {

            string comcod = this.GetComeCode();
            //string type = this.Request.QueryString["Type"].ToString().Trim();
            //string Company = this.ddlCompanyAgg.SelectedValue.ToString().Trim();
            string deptcode = this.ddldepartmentagg.SelectedValue.ToString().Substring(0, 2) + "%";


            //string Company = (this.ddlCompanyAgg.SelectedValue.ToString() == "000000000000")? "" : (this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, 2)+ "%");
            // : this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            string txtSProject = "%";// ;// (type == "Aggrement") ? (this.txtSrcPro.Text.Trim() + "%") : (this.txtSrcDepartment.Text.Trim() + "%");
                                                                  //string CallType = (this.Request.QueryString["Type"].ToString().Trim() == "EmpAllInfo") ? "GETPROJECTNAME" : "GETPROJECTNAMEFOT";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAME", deptcode, txtSProject, "", "", "", "", "", "", "");



            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds4.Tables[0];
            this.ddlProjectName.DataBind();
            this.GetEmpName();
            //this.ddlProjectName_SelectedIndexChanged(null, null);
            //this.GetEmpName();





        }
        protected void ibtnFindCompanyAgg_Click(object sender, EventArgs e)
        {
            this.GetCompanyName();
        }
        protected void lbtndeptagg_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        protected void txtsrchdeptagg_TextChanged(object sender, EventArgs e)
        {

        }
        protected void ddlCompanyAgg_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
            //this.ddlProjectName_SelectedIndexChanged(null,null);
        }
        protected void ddldepartmentagg_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this.GetDepartment();
            this.GetProjectName();

        }

        //protected void ibtnFindCompanyAgg_Click(object sender, EventArgs e)
        //{
        //    this.GetProjectName();
        //}
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.GetProjectName();//ibtnFindProject
            // this.ibtnFindProject_Click(null, null);
            this.GetEmpName();
            //this.GetEmpName();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.ddlProjectName_SelectedIndexChanged(null, null);
            //this.ddlProjectName_SelectedIndexChanged(null, null);
            this.GetProjectName();
        }
        protected void ddlEmpNameAllInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetComASecSelected();
        }

        private void GetComASecSelected()
        {

            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
            if (empid == "000000000000" || empid == "")
                return;
            DataTable dt = (DataTable)ViewState["tblemp"];
            DataRow[] dr = dt.Select("empid = '" + empid + "'");
            if (dr.Length > 0)
            {
                this.ddlCompanyAgg.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["companycode"].ToString();
                this.ddldepartmentagg.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["deptcode"].ToString();
                this.ddlProjectName.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["refno"].ToString();


            }


        }

        protected void ddlComName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmpInformation();
        }
    }
}
