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
using RealERPLIB;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;

namespace RealERPWEB.F_81_Hrm.F_89_Pay
{
    public partial class RptSalaryReconciliation : System.Web.UI.Page
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

                this.GetCompanyName();
                this.GetMonth();
                //((Label)this.Master.FindControl("lblTitle")).Text = "SALARY RECONCILIATION";

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void GetMonth()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_SALARY_RECON", "GETYEARMONTH", "", "", "", "", "", "", "", "", "");
            if (ds1==null)
                return;

            this.ddlMonth.DataTextField = "yearmon";
            this.ddlMonth.DataValueField = "ymon";
            this.ddlMonth.DataSource = ds1.Tables[0];
            this.ddlMonth.DataBind();
            this.ddlMonth.SelectedValue = System.DateTime.Today.ToString("yyyyMM").Trim();
        }
        private void GetCompanyName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string txtCompany = "%%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            if (ds2==null)
                return;

            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds2.Tables[0];
            this.ddlCompany.DataBind();
            this.GetBranch();
            this.ddlCompany_SelectedIndexChanged(null, null);
        }
        private void GetBranch()
        {
            string comcod = this.GetCompCode();
            string Company = ((this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string txtSProject = "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEPTNAMENEW", Company, txtSProject, "", "", "", "", "", "", "");
            if (ds3==null)
                return;

            this.ddlBranch.DataTextField = "deptdesc";
            this.ddlBranch.DataValueField = "deptcode";
            this.ddlBranch.DataSource = ds3.Tables[0];
            this.ddlBranch.DataBind();
        }
       
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetBranch();
        }

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string txtMonth = this.ddlMonth.SelectedItem.Text.Substring(0, 3);
            string Month = ASITUtility03.GetMonth(txtMonth);
            string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);
            string txtDate = year+Month;
            string companyCode = this.ddlCompany.SelectedValue.ToString()=="000000000000" ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)+"%";
            string branchCode = this.ddlBranch.SelectedValue.ToString()=="000000000000" ? "%" : this.ddlBranch.SelectedValue.ToString().Substring(0, 9)+"%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_SALARY_RECON", "RPTSALRECONCILLATION", txtDate, branchCode, companyCode, "", "", "", "", "", "");
            if (ds4==null)
                return;

            ViewState["tblSalaryRecon"] = ds4.Tables[0];
            this.Data_Bind();
        }

        protected void gvSalaryRecon_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSalaryRecon.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblSalaryRecon"];
            this.gvSalaryRecon.DataSource=dt;
            this.gvSalaryRecon.DataBind();
            if (dt.Rows.Count > 0)
            {

                Session["Report1"] = gvSalaryRecon;
                ((HyperLink)this.gvSalaryRecon.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

        }



        protected void gvSalaryRecon_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label empName = (Label)e.Row.FindControl("lblgvEmpName");
                Label curAmt = (Label)e.Row.FindControl("lblgvCurAmt");
                Label prevAmt = (Label)e.Row.FindControl("lblgvPrevAmt");

                string empId = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();

                if (empId == "")
                {
                    return;
                }
                if (empId == "000000000000" || empId == "AAAAAAAAAAAA")
                {
                    empName.Font.Bold = true;
                    curAmt.Font.Bold = true;
                    prevAmt.Font.Bold = true;
                    empName.Attributes["style"] = " font-size:14px; color:blue !important;";
                }
                else if (empId == "GGGGAAAAAAAA")
                {
                    empName.Font.Bold = true;
                    
                    prevAmt.Font.Bold = true;
                    prevAmt.Attributes["style"] = " font-size:14px; color:red !important;";
                    empName.Attributes["style"] = " font-size:14px; color:red !important;";


                }

            }
        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtMonth = this.ddlMonth.SelectedItem.Text.Substring(0, 3);
            string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO"+comcod+".jpg")).AbsoluteUri;
            string rptMonth = "Month of "+txtMonth+"'"+year;
            DataTable dt = (DataTable)ViewState["tblSalaryRecon"];
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalaryReconciliation>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryReconciliation", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "RECONCILIATION HEAD OFFICE"));
            Rpt1.SetParameters(new ReportParameter("txtMonth", rptMonth));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}