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
namespace RealERPWEB.F_81_Hrm.F_97_MIS
{
    public partial class LinkMgtInterface : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "Services") ? "EMPLOYEE  SERVICES INFORMATION" : "EMPLOYEE INFORMATION";
                this.SelectView();



            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void SelectView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Services":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.Service();
                    break;
                case "LateStatus":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.GetEmpName();
                    this.lblFDate.Text = this.Request.QueryString["Date1"].ToString();
                    this.lblTDate.Text = this.Request.QueryString["Date2"].ToString();
                    break;
                case "EmpSal":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.EmpSalary();
                    break;


            }

        }
        private void Service()
        {
            ViewState.Remove("tblservices");
            string comcod = this.Request.QueryString["comcod"].ToString().Trim();
            string empid = this.Request.QueryString["empid"].ToString().Trim();
            string Date = this.Request.QueryString["Date"].ToString().Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPSERVICES", empid, Date, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvempservices.DataSource = null;
                this.gvempservices.DataBind();
                return;
            }
            this.txtEmpSrc.Text = ds1.Tables[0].Rows[0]["empname"].ToString();
            this.lblDate.Text = this.Request.QueryString["Date"].ToString().Trim();
            ViewState["tblservices"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void EmpSalary()
        {
            ViewState.Remove("tblservices");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string empid = this.Request.QueryString["empid"].ToString().Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MGT_INTERFACE", "GETEMPSALINFO", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSalAdd.DataSource = null;
                this.gvSalAdd.DataBind();
                this.gvSalSub.DataSource = null;
                this.gvSalSub.DataBind();
                return;
            }
            this.lblEmpNameSal.Text = ds1.Tables[0].Rows[0]["empname"].ToString();
            ViewState["tblservices"] = ds1.Tables[0];

            this.Data_Bind();
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Services":
                    this.PrintServices();
                    break;
                case "LateStatus":
                    this.PrintEmpAttnIdWise();
                    break;
                case "EmpSal":

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
            string date = Convert.ToDateTime(this.lblDate.Text).ToString("MMMM dd, yyyy");
            ReportDocument rptempservices = new RealERPRPT.R_81_Hrm.R_82_App.RptEmpServices();
            TextObject txtempname = rptempservices.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            txtempname.Text = "Name: " + this.txtEmpSrc.Text.Trim();
            TextObject rptdate = rptempservices.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "Date: " + date;
            TextObject txtuserinfo = rptempservices.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptempservices.SetDataSource(dt);
            //string comcod = this.GetComeCode();
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptempservices.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptempservices;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblservices"];

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Services":
                    this.gvempservices.DataSource = dt;
                    this.gvempservices.DataBind();
                    break;
                case "LateStatus":

                    break;
                case "EmpSal":
                    DataView dvr = new DataView();
                    DataTable dtr1 = new DataTable();
                    dtr1 = dt;
                    dvr = dtr1.DefaultView;
                    dvr.RowFilter = ("gcod like '040%'");
                    dtr1 = dvr.ToTable();
                    Session["tblsaladd"] = dtr1;
                    double SalAdd = Convert.ToDouble((Convert.IsDBNull(dtr1.Compute("sum(gval)", "")) ? 0.00 : dtr1.Compute("sum(gval)", "")));
                    this.gvSalAdd.DataSource = dtr1;
                    this.gvSalAdd.DataBind();
                    this.FooterCalculation(dtr1, "gvSalAdd");

                    dtr1 = dt;
                    dvr = dtr1.DefaultView;
                    dvr.RowFilter = ("gcod like '041%'");
                    dtr1 = dvr.ToTable();
                    Session["tblsalsub"] = dtr1;
                    double SallSub = Convert.ToDouble((Convert.IsDBNull(dtr1.Compute("sum(gval)", "")) ? 0.00 : dtr1.Compute("sum(gval)", "")));
                    this.gvSalSub.DataSource = dtr1;
                    this.gvSalSub.DataBind();
                    this.FooterCalculation(dtr1, "gvSalSub");



                    this.lbltotalsal.Text = (SalAdd - SallSub).ToString("#,##0;(#,##0); ");


                    break;


            }




        }

        private void FooterCalculation(DataTable dt, string GvName)
        {
            if (dt.Rows.Count == 0)
                return;


            switch (GvName)
            {
                case "gvSalAdd":

                    double toaddamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gval)", "")) ? 0 : dt.Compute("sum(gval)", "")));
                    ((Label)this.gvSalAdd.FooterRow.FindControl("lgvFSalAdd")).Text = toaddamt.ToString("#,##0;(#,##0); ");
                    //this.txtgrossal.Text = toaddamt.ToString("#,##0;(#,##0); ");

                    break;


                case "gvSalSub":
                    ((Label)this.gvSalSub.FooterRow.FindControl("lgvFSalSub")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gval)", "")) ?
                             0 : dt.Compute("sum(gval)", ""))).ToString("#,##0;(#,##0); ");

                    break;
            }




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


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data_Bind();
        }
        protected void imgbtnEmpName_Click(object sender, EventArgs e)
        {
            this.GetEmpName();
        }
        private void GetEmpName()
        {
            string comcod = this.Request.QueryString["comcod"].ToString().Trim();
            string txtSEmployee = "%" + this.txtSrcEmpName.Text.Trim() + "%";
            string empid = (this.Request.QueryString["empid"].ToString() == "000000000000") ? "%" : this.Request.QueryString["empid"].ToString() + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MGT_INTERFACE", "GETEMPID", txtSEmployee, empid, "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();
        }
        private void PrintEmpAttnIdWise()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = this.GetComeCode();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.lblFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.lblTDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPATTNIDWISE", frmdate, todate, empid, "", "", "", "", "", "");


            DataTable dtdailyiemp = ds4.Tables[0];
            int sum = 0;
            string hour, minute;
            for (int i = 0; i < dtdailyiemp.Rows.Count; i++)
            {
                sum += Convert.ToInt32(dtdailyiemp.Rows[i]["actualattnminute"]);
            }
            hour = Convert.ToInt32(sum / 60).ToString();
            minute = ASTUtility.Right((Convert.ToString("00" + (sum % 60))), 2);
            string TotalHour = hour + ":" + minute;
            ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_83_Att.rptDailyAttnEmp();
            rptTest.SetDataSource(ds4.Tables[0]);
            TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            txtRptComName.Text = ds4.Tables[2].Rows[0]["companyname"].ToString();

            TextObject txttowrkday = rptTest.ReportDefinition.ReportObjects["txttowrkday"] as TextObject;
            txttowrkday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["twrkday"]).ToString("#,##0;(#,##0); ");
            TextObject txttolateday = rptTest.ReportDefinition.ReportObjects["txttolateday"] as TextObject;
            txttolateday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tLday"]).ToString("#,##0;(#,##0); ");
            TextObject txttoleaveday = rptTest.ReportDefinition.ReportObjects["txttoleaveday"] as TextObject;
            txttoleaveday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tlvday"]).ToString("#,##0;(#,##0); ");
            TextObject txtoabsday = rptTest.ReportDefinition.ReportObjects["txtoabsday"] as TextObject;
            txtoabsday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tabsday"]).ToString("#,##0;(#,##0); ");
            TextObject txtohday = rptTest.ReportDefinition.ReportObjects["txtohday"] as TextObject;
            txtohday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["thday"]).ToString("#,##0;(#,##0); ");


            TextObject txtrptTotalHour = rptTest.ReportDefinition.ReportObjects["txtTHour"] as TextObject;
            txtrptTotalHour.Text = TotalHour;
            TextObject txtuserinfo = rptTest.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            Session["Report1"] = rptTest;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}