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
namespace RealERPWEB.F_81_Hrm.F_89_Pay
{
    public partial class RptSalSummaryDetails : System.Web.UI.Page
    {
        Common compUtility = new Common();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                this.GetDate();
                this.GetCompany();
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
  
                // this.lblmsg.Visible = false;
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void GetDate()
        {
            string comcod = this.GetComeCode();
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
                return;

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);

            switch (comcod)
            {
                case "4301":
                    //case "4305":
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = startdate + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;

                default:
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = startdate + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;


            }


        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        private void GetCompany()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string txtCompany = "%%";

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            //string comcod = this.GetComeCode();
            //string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "GETCOMPANYNAME", txtCompany, "", "", "", "", "", "", "", "");
            //this.ddlCompany.DataTextField = "actdesc";
            //this.ddlCompany.DataValueField = "actcode";
            //this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.loadSalSum();
            this.LoadGrid();
        }


        protected void loadSalSum()
        {
            string comcod = this.GetComeCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string CompanyName = (this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "000000000000" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "PAYROLL_DETAIL1_SUMMEAY", frmdate, todate, CompanyName, "", "", "", "", "", "");

            if (ds3 == null)
            {
                this.gvSalSumDet.DataSource = null;
                this.gvSalSumDet.DataBind();
                return;

            }

            DataTable dt = this.HiddenSameData(ds3.Tables[0]);
            Session["tblSalSum"] = dt;

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }
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
        private void LoadGrid()
        {

            DataTable dt = (DataTable)Session["tblSalSum"];
            this.gvSalSumDet.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSalSumDet.DataSource = dt;
            this.gvSalSumDet.DataBind();
            //this.FooterCalculation();


        }


        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvSalSumDet.FooterRow.FindControl("lgvFTCurEmp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curempno)", "")) ? 0.00 : dt.Compute("sum(curempno)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalSumDet.FooterRow.FindControl("lgvFTCurMamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curpay)", "")) ? 0.00 : dt.Compute("sum(curpay)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalSumDet.FooterRow.FindControl("lgvFTPreEmp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(preempno)", "")) ? 0.00 : dt.Compute("sum(preempno)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalSumDet.FooterRow.FindControl("lgvFTPreMamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(prepay)", "")) ? 0.00 : dt.Compute("sum(prepay)", ""))).ToString("#,##0;(#,##0); ");


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintSalSum();
        }

        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }


        protected void PrintSalSum()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string namedep = (this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "Company Name" : "Department Name";
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMM-yy");
            string premonth = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(-1).ToString("MMM-yy");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");


            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.SalSummary2>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalSummDetails", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("Comname", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("date", "Details Salary Summary, Month of " + todate1));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptSalSummDetails();

            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();

            //TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtccaret.Text = " Month of " + todate1; 


            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rpcp.SetDataSource(dt);
            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rpcp.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rpcp;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void gvSalSumDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSalSumDet.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
    }
}
