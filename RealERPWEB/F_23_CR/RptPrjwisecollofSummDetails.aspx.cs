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
namespace RealERPWEB.F_23_CR
{
    public partial class RptPrjwisecollofSummDetails : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Project wise Summary of Collection Details";

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01-" + ASTUtility.Right(date, 8);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");


                this.GetName();
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


        private void GetName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT03", "GETPRJNAME01", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "gdesc";
            this.ddlProjectName.DataValueField = "gcod";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();

        }



        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetName();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            //this.PrintProjectWiseCollection();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string frmDate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string toDate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string date1 = " From " + frmDate + " To " + toDate;
            DataTable dt = (DataTable)Session["tblPrjColl"];
            DataTable dt1 = (DataTable)Session["tblavgcoll"];

            double netcoll = Convert.ToDouble(dt1.Rows[0]["tnetcoll"]);
            double netavgcoll = Convert.ToDouble(dt1.Rows[0]["tnetavg"]);
            double totalcoll = Convert.ToDouble(dt1.Rows[0]["tcollamt"]);
            double totalavgcoll = Convert.ToDouble(dt1.Rows[0]["avgval"]);
            double tmonth = Convert.ToDouble(dt1.Rows[0]["tmonth"]);


            var list = dt.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.ProjWiseColSummaryDetails>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_23_CR.RptPrjwiseCollofSummDetails", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Project Wise Summary Collections"));
            rpt.SetParameters(new ReportParameter("date1", date1));

            //Rpt1.SetParameters(new ReportParameter("txtbgcost", bgcost.ToString("#,##0;(#,##0); ")));
            rpt.SetParameters(new ReportParameter("txtNetcoll", netcoll.ToString("#,##0;(#,##0); ")));
            rpt.SetParameters(new ReportParameter("txtNetavgcoll", netavgcoll.ToString("#,##0;(#,##0); ")));
            rpt.SetParameters(new ReportParameter("txtTotalcoll", totalcoll.ToString("#,##0;(#,##0); ")));
            rpt.SetParameters(new ReportParameter("txtTotalAvgcoll", totalavgcoll.ToString("#,##0;(#,##0); ")));
            rpt.SetParameters(new ReportParameter("txtmonths", tmonth.ToString("#,##0;(#,##0); ") + " Month's Collection Summary"));

            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comLogo", ComLogo));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }


        private void PrintProjectWiseCollection()
        {


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comsnam = hst["comsnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            //string Date = "Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            //string ProjectName = "Project Name: " + this.ddlProjectName.SelectedItem.Text.ToString();

            //DataTable dt = (DataTable)Session["tblPrjstatus"];
            //var lst = dt.DataTableToList<RealEntity.C_17_Acc.RptProjectWiseCollectionStatus>();
            //LocalReport Rpt1 = new LocalReport();
            //Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptProjectWiseCollection", lst, null, null);
            ////Rpt1.EnableExternalImages = true;
            ////Rpt1.SetParameters(new ReportParameter("comname", comnam));
            ////Rpt1.SetParameters(new ReportParameter("Date", Date));
            ////Rpt1.SetParameters(new ReportParameter("ProjectName", ProjectName));
            ////Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            ////Rpt1.SetParameters(new ReportParameter("txtTitle", "Project Wise Collection Status"));
            //// Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptProjCancellationUnit", lst, null, null);
            //Rpt1.SetParameters(new ReportParameter("comname", comnam));
            //Rpt1.SetParameters(new ReportParameter("date1", "Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy")));
            //Rpt1.SetParameters(new ReportParameter("Rpttitle", ProjectName));
            //Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));


            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string frmDate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string toDate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string ProjectCode = (this.ddlProjectName.SelectedValue.ToString() == "00000") ? "78%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT03", "RPTSUMMARYOFCOLLDETAILS", frmDate, toDate, ProjectCode, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvprjstatus.DataSource = null;
                this.gvprjstatus.DataBind();
                return;
            }


            DataTable dt = this.HiddenSameData(ds2.Tables[0]);
            Session["tblPrjColl"] = ds2.Tables[0];  // this.HiddenSameData(ds2.Tables[0]);
            Session["tblavgcoll"] = ds2.Tables[1];
            this.Data_Bind();
        }


        private DataTable HiddenSameData(DataTable dt1)
        {


            if (dt1.Rows.Count == 0)
                return dt1;

            string prjcode = dt1.Rows[0]["prjcode"].ToString();



            string gdesc = dt1.Rows[0]["gdesc"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["prjcode"].ToString() == prjcode)
                {

                    prjcode = dt1.Rows[j]["prjcode"].ToString();
                    dt1.Rows[j]["gdesc"] = "";
                }

                else
                {
                    prjcode = dt1.Rows[j]["prjcode"].ToString();
                }
            }

            return dt1;
        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblPrjColl"];
            // this.gvprjstatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvprjstatus.Columns[3].Visible = (this.ddlProjectName.SelectedValue.ToString() == "00000") ? true : false;
            this.gvprjstatus.DataSource = dt;
            this.gvprjstatus.DataBind();
            this.pnlavg.Visible = true;

            if (dt.Rows.Count == 0)
                return;

            Session["Report1"] = gvprjstatus;
            ((HyperLink)this.gvprjstatus.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


            this.Avgcollection();

        }

        private void Avgcollection()
        {
            DataTable dt = (DataTable)Session["tblavgcoll"];
            this.txttotal.Text = Convert.ToDouble(dt.Rows[0]["tcollamt"]).ToString("#,##0;(#,##0); ");
            this.txtNetcoll.Text = Convert.ToDouble(dt.Rows[0]["tnetcoll"]).ToString("#,##0;(#,##0); ");
            this.txtavgAmt.Text = Convert.ToDouble(dt.Rows[0]["avgval"]).ToString("#,##0;(#,##0); ");
            this.txtnetavgcoll.Text = Convert.ToDouble(dt.Rows[0]["tnetavg"]).ToString("#,##0;(#,##0); ");
            this.txtmonth.Text = Convert.ToDouble(dt.Rows[0]["tmonth"]).ToString("#,##0;(#,##0); ");


        }


        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblPrjColl"];
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvprjstatus.FooterRow.FindControl("lgvFmramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(mramt)", "")) ?
                        0.00 : dt.Compute("Sum(mramt)", ""))).ToString("#,##0;(#,##0); ");






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

        protected void gvprjstatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            // Label code = (Label)e.Row.FindControl("lblgvrptcode");

            Label pactdesc = (Label)e.Row.FindControl("lgvpactdesc");
            Label lblgvmramt = (Label)e.Row.FindControl("lblgvmramt");





            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prjmcode")).ToString().Trim();

            if (code == "")
            {
                return;
            }


            if (ASTUtility.Right(code, 2) == "BB")
            {
                pactdesc.Attributes["style"] = "font-weight:bold;";
                lblgvmramt.Attributes["style"] = "font-weight:bold;";


                pactdesc.Style.Add("text-align", "right");




            }
            if (ASTUtility.Right(code, 2) == "CC")
            {
                pactdesc.Attributes["style"] = "font-weight:bold; color:maroon;";
                lblgvmramt.Attributes["style"] = "font-weight:bold;  color:maroon;";
                pactdesc.Style.Add("text-align", "right");




            }

            if (ASTUtility.Right(code, 2) == "DD")
            {
                pactdesc.Attributes["style"] = "font-weight:bold; color:blue;";
                lblgvmramt.Attributes["style"] = "font-weight:bold; color:blue;";
                pactdesc.Style.Add("text-align", "right");

                //lgvNagad.Style.Add("text-align", "left");



            }




        }
    }
}