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

namespace RealERPWEB.F_81_Hrm.F_97_MIS
{
    public partial class RptServiceStoryProjectWise : System.Web.UI.Page
    {
        Common compUtility = new Common();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetProjectName();
                //this.lbtnOk_Click(null, null);
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            this.RtpServiceHistoryProjectWise();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            //this.RtpServiceHistoryProjectWise();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string projectName = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTPROJECTEMPSERVICES", projectName, "", "", "", "", "", "", "", "");
            //DataTable dt = (DataTable)ds1.Tables[0];
            if (ds1 == null)
            {
                this.gvProjEmp.DataSource = null;
                this.gvProjEmp.DataBind();

                return;

            }

            Session["ProjEmp"] = ds1.Tables[0];

            this.Data_Bind();


        }
        private void RtpServiceHistoryProjectWise()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string projectName = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTPROJECTEMPSERVICES", projectName, "", "", "", "", "", "", "", "");
            DataTable dt = (DataTable)ds1.Tables[0];

            string rpthead = "Project Wise Employee List";

            if (dt == null)
                return;
            //var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmployeeInfo>();
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.RtpServiceHistoryProjectWise>();

            LocalReport Rpt1 = new LocalReport();
            //Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptInactiveEmplists", list, null, null);
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RtpServiceHistoryProjectWise", list, null, null);


            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtProject", "Project : " + this.ddlProjectName.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("txtTitle", rpthead));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SERVICEHISTORYPROJECTWISE", "", "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "SIRDESC";
            this.ddlProjectName.DataValueField = "SIRCODE";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
          }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["ProjEmp"];
            this.gvProjEmp.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvProjEmp.DataSource = dt;
            this.gvProjEmp.DataBind();

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "pageLoaded()", true);

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvProjEmp.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }
        protected void gvProjEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvProjEmp.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}