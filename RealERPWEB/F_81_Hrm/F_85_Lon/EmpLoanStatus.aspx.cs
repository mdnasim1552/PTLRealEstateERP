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


namespace RealERPWEB.F_81_Hrm.F_85_Lon
{
    public partial class EmpLoanStatus1 : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE LOAN STATUS";
                this.GetCompName();
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.GetDepartment();
                this.GetLoanType();

                string lnno = this.Request.QueryString["lnno"] ?? "";
                if(lnno== "update")
                {
                    this.lnkbtnShow_Click(null, null);
                }

                //this.lnkbtnShow_Click(null, null);
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
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.ddlComName.SelectedValue.Length > 0 ? this.ddlComName.SelectedValue.ToString() : comcod;
            return comcod;
        }
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
        private void GetCompName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = GetComeCode();
            string txtDepartment = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME", txtDepartment, userid, "", "", "", "", "", "", "");
            this.ddlComp.DataTextField = "actdesc";
            this.ddlComp.DataValueField = "actcode";
            this.ddlComp.DataSource = ds1.Tables[0];
            this.ddlComp.DataBind();
        }
        protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        private void GetDepartment()
        {
            string comcod = this.GetComeCode();
            string company = this.ddlComp.SelectedValue.ToString().Substring(0, 2) + "%";
            string dept = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", company, dept, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
        }
        protected void ibtnFindDepartment_Click(object sender, EventArgs e)
        {
            //this.GetDepartment();
            this.GetCompName();
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
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.empLoanStatus();          
            //this.lblmsg.Text = "";
        }
        private void empLoanStatus()
        {
            Session.Remove("tbloan");
            string comcod = this.GetComeCode();
            string comnam = this.ddlComp.SelectedValue.Substring(0, 2).ToString();
            string deptname = this.ddlDepartment.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string chkbal = this.Chkbalance.Checked ? "Length" : "";
            string loantype = this.ddlLoantype.SelectedValue.ToString()==""?"%%": this.ddlLoantype.SelectedValue.ToString();
            string calltype = "";
            string procedure = "";

            switch (comcod)
            {
                case "3365":                
                     
                    calltype = "EMPLOANSTATUSBTI";
                    procedure = "dbo_hrm.SP_REPORT_HR_INTERFACE";
                    break;
                 default:
                    calltype = "EMPLOANSTATUS";
                    procedure = "dbo_hrm.SP_REPORT_PAYROLL";
                    break;
            }
            DataSet ds2 = HRData.GetTransInfo(comcod, procedure, calltype, date, deptname, comnam, chkbal, loantype, "", "", "", "");
            if (ds2 == null)
            {
                this.gvEmpLoanStatus.DataSource = null;
                this.gvEmpLoanStatus.DataBind();
                return;
            }
            Session["tbloan"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tbloan"];
            string empst = this.ddlempst.SelectedValue.ToString();
            DataView view = new DataView();      
            view.Table = dt;
            if (empst == "1")
            {
                view.RowFilter = "empst='1'";            
                dt = view.ToTable();
            }
            else if (empst == "0")
            {
                view.RowFilter = "empst='0'";              
                dt = view.ToTable();
            }
            else
            {
                dt = view.ToTable();
            }
            Session["tbloan"] = dt;

            this.gvEmpLoanStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvEmpLoanStatus.DataSource = dt;
            this.gvEmpLoanStatus.DataBind();
            this.FooterCalculation();
            switch (comcod)
            {               
                case "3365":
                case "3101":
                    this.gvEmpLoanStatus.Columns[7].Visible = true;
                    this.gvEmpLoanStatus.Columns[8].Visible = true;
                    this.gvEmpLoanStatus.Columns[9].Visible = true;
                    this.gvEmpLoanStatus.Columns[10].Visible = true;
                    this.gvEmpLoanStatus.Columns[11].Visible = true;
                    this.gvEmpLoanStatus.Columns[14].Visible = true;
                    this.gvEmpLoanStatus.Columns[15].Visible = true;
                    this.gvEmpLoanStatus.Columns[17].Visible = true;
                    this.gvEmpLoanStatus.Columns[18].Visible = true;


                    break;
            }             
        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tbloan"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblgvFLoanamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tloan)", "")) ? 0.00
                    : dt.Compute("sum(tloan)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblgvFPaidamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ? 0.00
                   : dt.Compute("sum(paidamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblgvFbalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balamt)", "")) ? 0.00
                    : dt.Compute("sum(balamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblTgvMonlon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(monlon)", "")) ? 0.00
                : dt.Compute("sum(monlon)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblgvMonlon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(monloanemp)", "")) ? 0.00
                : dt.Compute("sum(monloanemp)", ""))).ToString("#,##0;(#,##0); ");
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbloan"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_mgt.EmpSettlmnt.EmpLoanStatus>();
            LocalReport Rpt1 = new LocalReport(); //rptEmpLoanStatus rptEmpLoanStatus
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_85_Lon.rptEmpLoanStatus", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Loan Status Information"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + frmdate));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //    DataTable dt = (DataTable)Session["tbloan"];
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comname = hst["comnam"].ToString();
            //    string comadd = hst["comadd1"].ToString();
            //    string compname = hst["compname"].ToString();
            //    string username = hst["username"].ToString();
            //    string companyname = ddlDeptName.SelectedItem.Text.Trim().Substring(13);
            //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //    string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            //    ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_85_Lon.rptEmpLoanStatus();
            //    //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //    //CompName.Text = companyname;
            //    TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["Asdate"] as TextObject;
            //    txtccaret.Text = "Date: " + frmdate;
            //    TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //    rpcp.SetDataSource(dt);
            //    string comcod =  GetComeCode();
            //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //    rpcp.SetParameterValue("ComLogo", ComLogo);
            //    Session["Report1"] = rpcp;
            //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string secid;
            string empid;
            int j;
            secid = dt1.Rows[0]["section"].ToString();
            empid = dt1.Rows[0]["empid"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["section"].ToString() == secid)
                {
                    secid = dt1.Rows[j]["section"].ToString();
                    dt1.Rows[j]["secdesc"] = "";
                }
                else
                {
                    secid = dt1.Rows[j]["section"].ToString();
                }
                if (dt1.Rows[j]["empid"].ToString() == empid)
                {
                    empid = dt1.Rows[j]["empid"].ToString();
                    dt1.Rows[j]["empname"] = "";
                    dt1.Rows[j]["idcard"] = "";
                    dt1.Rows[j]["desig"] = "";
                }
                else
                {
                    empid = dt1.Rows[j]["empname"].ToString();       
                }
            }
            return dt1;
        }
        protected void ddlComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        protected void gvEmpLoanStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpLoanStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void ddlempst_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.empLoanStatus();
        }

        protected void ddlLoantype_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.empLoanStatus();
        }
    }
}