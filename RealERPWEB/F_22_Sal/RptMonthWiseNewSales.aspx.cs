using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;

namespace RealERPWEB.F_22_Sal
{
    public partial class RptMonthWiseNewSales : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Month Wise Sale Report (New Sale) ";
                this.GetProjectName();
                string Date = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01-" + ASTUtility.Right(Date, 8);
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
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

        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string txtSProject =  "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCLIENTPRJNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();

        }
        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowData();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text.ToString();
                string eventdesc = "Show Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        

        private void ShowData()
        {
            Session.Remove("tblnewsales");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string PactCode = this.ddlProjectName.SelectedValue.ToString()=="000000000000"?"18%": this.ddlProjectName.SelectedValue.ToString()+"%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTMONTHWISESALESREPORT", fromdate, todate, PactCode, "", "", "", "", "", "");
            if (ds1 == null)
                return;


            Session["tblnewsales"] = HiddenSameDate(ds1.Tables[0]);
            this.gvNewsales.DataSource = ds1.Tables[0];
            this.gvNewsales.DataBind();
            //this.FooterCalculation();



        }

        

        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string monthid = dt1.Rows[0]["monthid"].ToString();
            string monthid1 = dt1.Rows[0]["monthid1"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["monthid"].ToString() == monthid)
                {
                    monthid1 = dt1.Rows[j]["monthid1"].ToString();
                    dt1.Rows[j]["monthid1"] = "";
                 
                }

                else
                {
                    monthid = dt1.Rows[j]["monthid"].ToString();
                }
            }
            return dt1;
        }
        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblnewsales"];
            var list = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptMonWiseNewSales>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_22_Sal.RptMonthWiseNewSales", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Month Wise Sale Report (New Sale)"));
            Rpt1.SetParameters(new ReportParameter("txtProjName", "Project Name: " + this.ddlProjectName.SelectedItem.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("txtDate", "From: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + "  To:  " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void gvNewsales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblprjName = (Label)e.Row.FindControl("lblprjName");
                Label lblgvsalesvalue = (Label)e.Row.FindControl("lblgvsalesvalue");
                Label lblgvReceive = (Label)e.Row.FindControl("lblgvReceive");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lblprjName.Font.Bold = true;
                    lblgvsalesvalue.Font.Bold = true;
                    lblgvReceive.Font.Bold = true;

                    lblprjName.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                    lblgvsalesvalue.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                    lblgvReceive.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
            

                    lblprjName.Style.Add("text-align", "right");
                }

            }
        }
    }
}