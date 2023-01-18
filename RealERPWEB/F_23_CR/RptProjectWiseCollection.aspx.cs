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
namespace RealERPWEB.F_23_CR

{
    public partial class RptProjectWiseCollection : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Project Wise Collection Status";
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
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
            string txtSProject = this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCLIENTPRJNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();

        }

        //protected void imgbtnFindProject_Click(object sender, EventArgs e)
        //{
        //    this.GetProjectName();
        //}
        //protected void lbtnPrint_Click(object sender, EventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    ReportDocument rptcusdues = new RealERPRPT.R_22_Sal.RptCalTValAvgVal();
        //    TextObject rpttxtCompanyName = rptcusdues.ReportDefinition.ReportObjects["CompName"] as TextObject;
        //    rpttxtCompanyName.Text = comnam;

        //    TextObject txtuserinfo = rptcusdues.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

        //    rptcusdues.SetDataSource((DataTable)Session["tbsoldinf"]);
        //    if (ConstantInfo.LogStatus == true)
        //    {
        //        string eventtype = "Sold Info";
        //        string eventdesc = "Print Report Sold Inf";
        //        string eventdesc2 = "";
        //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //    }
        //    //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    //rptcusdues.SetParameterValue("ComLogo", ComLogo);
        //    Session["Report1"] = rptcusdues;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //}

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            this.PrintProjectWiseCollection();

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comsnam = hst["comsnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            //DataTable dt = (DataTable)Session["tbsoldinf"];


            ////string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd.MM.yyyy");
            ////string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd.MM.yyyy");

            //LocalReport Rpt1 = new LocalReport();


            //var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptCalTValAvgVal>();
            //Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptCalTValAvgVal", lst, null, null);
            //Rpt1.EnableExternalImages = true;
            //Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            ////Rpt1.SetParameters(new ReportParameter("txtDate", " (" + "From  " + frmdate + " To " + todate + ")"));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("rptTitle", "Calculation of Total Value & Average Value of Sold Units"));
            //Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void PrintProjectWiseCollection()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            string Date = "Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string ProjectName = "Project Name: " + this.ddlProjectName.SelectedItem.Text.ToString();

            DataTable dt = (DataTable)Session["tblPrjstatus"];
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.RptProjectWiseCollectionStatus>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptProjectWiseCollection", lst, null, null);
            //Rpt1.EnableExternalImages = true;
            //Rpt1.SetParameters(new ReportParameter("comname", comnam));
            //Rpt1.SetParameters(new ReportParameter("Date", Date));
            //Rpt1.SetParameters(new ReportParameter("ProjectName", ProjectName));
            //Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            //Rpt1.SetParameters(new ReportParameter("txtTitle", "Project Wise Collection Status"));
            // Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptProjCancellationUnit", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("date1", "Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", ProjectName));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string ProjectCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "18%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "PROJECTWISECOLLSTATUS", Date, ProjectCode, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvprjstatus.DataSource = null;
                this.gvprjstatus.DataBind();
                return;
            }

            Session["tblPrjstatus"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();

                }

            }

            return dt1;
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblPrjstatus"];
            this.gvprjstatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvprjstatus.Columns[1].Visible = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? true : false;
            this.gvprjstatus.DataSource = dt;
            this.gvprjstatus.DataBind();

            this.FooterCalculation();

        }


        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblPrjstatus"];
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvprjstatus.FooterRow.FindControl("lgvFTval")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(totalsval)", "")) ?
                        0.00 : dt.Compute("Sum(totalsval)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprjstatus.FooterRow.FindControl("lgvFreceivedamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(paidamt)", "")) ?
                0.00 : dt.Compute("Sum(paidamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprjstatus.FooterRow.FindControl("lgvFlgvmodamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(modfiamt)", "")) ?
              0.00 : dt.Compute("Sum(modfiamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprjstatus.FooterRow.FindControl("lgvFIncreseamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(increseamt)", "")) ?
              0.00 : dt.Compute("Sum(increseamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprjstatus.FooterRow.FindControl("lgvFutilityamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(utilityamt)", "")) ?
               0.00 : dt.Compute("Sum(utilityamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprjstatus.FooterRow.FindControl("lgvFdelayamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(delayamt)", "")) ?
               0.00 : dt.Compute("Sum(delayamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprjstatus.FooterRow.FindControl("lgvFassociationamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(associaamt)", "")) ?
               0.00 : dt.Compute("Sum(associaamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprjstatus.FooterRow.FindControl("lgvFtotalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(totalrecamt)", "")) ?
               0.00 : dt.Compute("Sum(totalrecamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprjstatus.FooterRow.FindControl("lgvFbalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(balance)", "")) ?
                0.00 : dt.Compute("Sum(balance)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprjstatus.FooterRow.FindControl("lgvFutility")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(utlityam)", "")) ?
                0.00 : dt.Compute("Sum(utlityam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvprjstatus.FooterRow.FindControl("lgvFparkam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cparkam)", "")) ?
                0.00 : dt.Compute("Sum(cparkam)", ""))).ToString("#,##0;(#,##0); ");

            Session["Report1"] = gvprjstatus;
            ((HyperLink)this.gvprjstatus.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";




        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();

        }


        protected void gvprjstatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvprjstatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}