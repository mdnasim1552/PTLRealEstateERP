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
namespace RealERPWEB.F_81_Hrm.F_90_PF
{
    public partial class RptPaymentAndProvidentinf : System.Web.UI.Page
    {
        Common compUtility = new Common();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = (Convert.ToBoolean(dr1[0]["printable"]));

                //((Label)this.Master.FindControl("lblTitle")).Text = "";
                this.GetCompany();

            }

        }

        private void GetDate()
        {
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
                return;

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.txtfromdate.Text = startdate + date.Substring(2);
            this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            this.ddlCompany_SelectedIndexChanged(null, null);
            ds1.Dispose();

        }

        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }

        private void SectionName()
        {

            string comcod = this.GetCompCode();
            string projectcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSSec = "%" + this.txtSrcSec.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();

        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string projectcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9);
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string CompanyName = (this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PFACCOUNTS", "GETPROVIDENTFUND", frmdate, todate, CompanyName, projectcode, section, "", "", "", "");
            Session["tblprovident"] = this.HiddenSameData(ds.Tables[0]);
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string refno = dt1.Rows[0]["refno"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["refno"].ToString() == refno)
                {
                    refno = dt1.Rows[j]["refno"].ToString();
                    dt1.Rows[j]["refdesc"] = "";
                }
                else
                {
                    refno = dt1.Rows[j]["refno"].ToString();
                }
            }
            return dt1;

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblprovident"];
            this.gvprovident.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvprovident.DataSource = dt;
            this.gvprovident.DataBind();
            this.FooterCal();
        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblprovident"];


            ((Label)this.gvprovident.FooterRow.FindControl("lgvFPsal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gspay)", "")) ? 0.00 : dt.Compute("sum(gspay)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprovident.FooterRow.FindControl("lgvFamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfund)", "")) ? 0.00 : dt.Compute("sum(pfund)", ""))).ToString("#,##0;(#,##0); ");

        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblprovident"];

            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_90_PF.ProvidedFund>();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptHRSetup.GetLocalReport("R_81_Hrm.R_90_PF.RptProvidedFund", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Payment Schedule Of Provident Fund"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region Old
            //ReportDocument providentfund = new RealERPRPT.R_81_Hrm.R_90_PF.RptProvidedFund();

            //TextObject txtCompName = providentfund.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //txtCompName.Text = this.ddlCompany.SelectedItem.Text;  //txtTitle

            //TextObject txtTitle = providentfund.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            //txtTitle.Text = "Payment Schedule of Provident Fund " + Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");

            //TextObject txtuserinfo = providentfund.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //providentfund.SetDataSource(dt);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //providentfund.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = providentfund;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #endregion
        }

        protected void imgbtnProSrch_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void imgbtnSecSrch_Click(object sender, EventArgs e)
        {
            this.SectionName();
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }


        protected void gvprovident_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvprovident.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}