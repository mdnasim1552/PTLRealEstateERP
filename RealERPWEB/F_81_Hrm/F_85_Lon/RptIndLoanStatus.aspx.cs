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
    public partial class RptIndLoanStatus1 : System.Web.UI.Page
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

                //this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE LOAN Installment Details";
                this.GetEmplist();
                this.GetLoanType();
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetLoanType()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLOANTYPE", "", "", "", "", "", "", "", "", "");
            this.ddlLoantype.DataTextField = "loantype";
            this.ddlLoantype.DataValueField = "gcod";
            this.ddlLoantype.DataSource = ds1.Tables[0];
            this.ddlLoantype.DataBind();
            ddlLoantype.Items.Insert(0, new ListItem("ALL Loan", ""));
            ddlLoantype.SelectedValue = "0";
        }
        private void GetEmplist()
        {
            string comcod = this.GetComeCode();
            string txtEmpname = "%%";
            string type = "";
            switch (comcod)
            {
                case "3365":
                case "3101":
                    type = "lnemp";
                    break;
                default:
                    type = "";
                    break;
            }
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLNEMPLIST", txtEmpname, type, "", "", "", "", "", "", "");
            this.ddlEmpList.DataTextField = "empname";
            this.ddlEmpList.DataValueField = "empid";
            this.ddlEmpList.DataSource = ds1.Tables[0];
            this.ddlEmpList.DataBind();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ddlEmpList.Enabled = false;
                this.ShowLoanInfo();
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.ddlEmpList.Enabled = true;
            this.gvEmpLoanStatus.DataSource = null;
            this.gvEmpLoanStatus.DataBind();
        }

        private void ShowLoanInfo()
        {
            Session.Remove("tbloan");
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpList.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string loantype = this.ddlLoantype.SelectedValue.ToString() == "" ? "%%" : this.ddlLoantype.SelectedValue.ToString();

            string calltype = "";
            string procedure = "";

            switch (comcod)
            {
                case "3365":

                    calltype = "RPTLOANINDBTI";
                    procedure = "dbo_hrm.SP_REPORT_PAYROLL";
                    break;
                default:
                    calltype = "RPTEMPLOANIND";
                    procedure = "dbo_hrm.SP_REPORT_PAYROLL";
                    break;
            }
            

            //DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "RPTEMPLOANIND", date, empid, loantype, "", "", "", "", "", "");
            DataSet ds2 = HRData.GetTransInfo(comcod, procedure, calltype , date, empid, loantype, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvEmpLoanStatus.DataSource = null;
                this.gvEmpLoanStatus.DataBind();
                return;
            }
            Session["tbloan"] = ds2.Tables[0];// this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }
        private void Data_Bind()
        {
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tbloan"];
            this.gvEmpLoanStatus.DataSource = dt;
            this.gvEmpLoanStatus.DataBind();
            this.FooterCalculation();

            switch (comcod)
            {
                case "3365":
                case "3101":
                    this.gvEmpLoanStatus.Columns[4].Visible = true;
                    this.gvEmpLoanStatus.Columns[7].Visible = true;
                    this.gvEmpLoanStatus.Columns[9].Visible = true;
                    
                    break;
                default:
                    this.gvEmpLoanStatus.Columns[3].HeaderText = "Loan Amt";
                    this.gvEmpLoanStatus.Columns[6].HeaderText = "Paid Amt";
                    this.gvEmpLoanStatus.Columns[8].HeaderText = "Bal Amt";
                    break;



            }



        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tbloan"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblgvFLoanamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lnamt)", "")) ? 0.00
                    : dt.Compute("sum(lnamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblgvUptoanamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(uppermon)", "")) ? 0.00
             : dt.Compute("sum(uppermon)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblgvFPaidamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ? 0.00
                   : dt.Compute("sum(paidamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblgvFbalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balamt)", "")) ? 0.00
                    : dt.Compute("sum(balamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblgvFPaidamtCom")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamtcom)", "")) ? 0.00
           : dt.Compute("sum(paidamtcom)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblgvFbalamtCom")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balamtcom)", "")) ? 0.00
                   : dt.Compute("sum(balamtcom)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblgvFLoanamtcom")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lnamtcom)", "")) ? 0.00
                    : dt.Compute("sum(lnamtcom)", ""))).ToString("#,##0;(#,##0); ");





        }
        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            this.GetEmplist();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbloan"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_mgt.EmpSettlmnt.EmpLoanStatus>();
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string EmpId = lst[0].idcard.ToString();
            string EmpDpt = lst[0].desig.ToString();
            string EmpName = lst[0].empname.ToString();
            string EmpDesg = lst[0].secdesc.ToString();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_85_Lon.rptEmpLoanInsDetails", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Loan Installment Details"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + frmdate));
            Rpt1.SetParameters(new ReportParameter("EmpId", EmpId));
            Rpt1.SetParameters(new ReportParameter("EmpDpt", EmpDpt));
            Rpt1.SetParameters(new ReportParameter("EmpName", EmpName));
            Rpt1.SetParameters(new ReportParameter("EmpDesg", EmpDesg));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
    }
}