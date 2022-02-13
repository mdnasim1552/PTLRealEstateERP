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
                //if ((!ASTUtility.PagePermission (HttpContext.Current.Request.Url.AbsoluteUri.ToString (),
                //        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean (hst["permission"]))
                //    Response.Redirect ("../../AcceessError.aspx");

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

               

                this.GetDateSet();


                this.GetCompName();
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "EmpLeaveStatus") ? "Employee Leave Status"
                    : (this.Request.QueryString["Type"].ToString() == "MonWiseLeave") ? "Employee Leave Status(Month Wise)" : "";
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
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please Setup Start Date Firstly!" + "');", true);
                return;
            }
            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
            this.txtfrmDate.Text = startdate + this.txtfrmDate.Text.Trim().Substring(2);
            this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
             
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
            string txtCompany = this.txtSrcCompany.Text.Trim() + "%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            //this.ddlCompanyName.DataTextField = "actdesc";
            //this.ddlCompanyName.DataValueField = "actcode";
            //this.ddlCompanyName.DataSource = ds1.Tables[0];
            //this.ddlCompanyName.DataBind();
            //this.ddlCompanyName_SelectedIndexChanged(null, null);



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
            string txtSearchDept = this.txtSrcDepartment.Text.Trim() + "%";
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
            string txtSearchDept = this.txtSrcDepartment.Text.Trim() + "%";
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




            }



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




            }




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
    }
}