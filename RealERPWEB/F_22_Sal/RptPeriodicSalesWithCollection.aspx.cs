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
namespace RealERPWEB.F_22_Sal
{
    public partial class RptPeriodicSalesWithCollection : System.Web.UI.Page
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
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "periodic Sales With Collection";

                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDate.Text = "01" + this.txtDate.Text.Trim().Substring(2);
                this.txttodat.Text = Convert.ToDateTime(this.txtDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");



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



        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            this.PrintProjectWiseCollection();




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
            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodat.Text).ToString("dd-MMM-yyyy");
            string ProjectCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "18%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT03", "RPTSALESVSCOLLECTION", frmdate, todate, ProjectCode, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvsaleswithcoll.DataSource = null;
                this.gvsaleswithcoll.DataBind();
                return;
            }

            Session["tblsalesvscoll"] = this.HiddenSameData(ds2.Tables[0]);
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
            DataTable dt = (DataTable)Session["tblsalesvscoll"];
            this.gvsaleswithcoll.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            //this.gvsaleswithcoll.Columns[1].Visible = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? true : false;
            this.gvsaleswithcoll.DataSource = dt;
            this.gvsaleswithcoll.DataBind();

            this.FooterCalculation();

        }


        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblsalesvscoll"];
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvsaleswithcoll.FooterRow.FindControl("lgvFBudgetamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdamt)", "")) ?
                        0.00 : dt.Compute("Sum(bgdamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsaleswithcoll.FooterRow.FindControl("lgvFSalesval")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(suamt)", "")) ?
                       0.00 : dt.Compute("Sum(suamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsaleswithcoll.FooterRow.FindControl("lgvFbookdues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bookdues)", "")) ?
                  0.00 : dt.Compute("Sum(bookdues)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsaleswithcoll.FooterRow.FindControl("lgvFInsdues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(insdues)", "")) ?
                  0.00 : dt.Compute("Sum(insdues)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsaleswithcoll.FooterRow.FindControl("lgvFbookam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cbookam)", "")) ?
                0.00 : dt.Compute("Sum(cbookam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsaleswithcoll.FooterRow.FindControl("lgvFinstallamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cinsam)", "")) ?
                0.00 : dt.Compute("Sum(cinsam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsaleswithcoll.FooterRow.FindControl("lgvFtocall")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tocollam)", "")) ?
                0.00 : dt.Compute("Sum(tocollam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsaleswithcoll.FooterRow.FindControl("lgvFnetbooking")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(netbookam)", "")) ?
            0.00 : dt.Compute("Sum(netbookam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsaleswithcoll.FooterRow.FindControl("lgvFnetIns")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(netinsdues)", "")) ?
                0.00 : dt.Compute("Sum(netinsdues)", ""))).ToString("#,##0;(#,##0); ");
       
            ((Label)this.gvsaleswithcoll.FooterRow.FindControl("lgvFbalance")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(balance)", "")) ?
                0.00 : dt.Compute("Sum(balance)", ""))).ToString("#,##0;(#,##0); ");

            Session["Report1"] = gvsaleswithcoll;
            ((HyperLink)this.gvsaleswithcoll.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";




        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();

        }

        protected void gvsaleswithcoll_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvsaleswithcoll.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvsaleswithcoll_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;

                TableCell cell02 = new TableCell();
                cell02.Text = "";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 1;


                TableCell cell03 = new TableCell();
                cell03.Text = "";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 2;

                TableCell cell04 = new TableCell();
                cell04.Text = "";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 1;

                TableCell cell05 = new TableCell();
                cell05.Text = "";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 1;

                TableCell cell06 = new TableCell();
                cell06.Text = "";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 1;

                TableCell cell07 = new TableCell();
                cell07.Text = "";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.ColumnSpan = 1;


                TableCell cell08 = new TableCell();
                cell08.Text = "Receivable";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.ColumnSpan = 2;
                cell08.Font.Bold = true;


                TableCell cell09 = new TableCell();
                cell09.Text = "Received (Encash)";
                cell09.HorizontalAlign = HorizontalAlign.Center;
                cell09.ColumnSpan = 2;
                cell09.Font.Bold = true;


                TableCell cell10 = new TableCell();
                cell10.Text = "";
                cell10.HorizontalAlign = HorizontalAlign.Center;
                cell10.ColumnSpan = 1;

                TableCell cell11 = new TableCell();
                cell11.Text = "Balance";
                cell11.HorizontalAlign = HorizontalAlign.Center;
                cell11.ColumnSpan = 3;
                cell11.Font.Bold = true;
                //TableCell cell12 = new TableCell();
                //cell12.Text = "";
                //cell12.HorizontalAlign = HorizontalAlign.Center;
                //cell12.ColumnSpan = 1;

                //TableCell cell13 = new TableCell();
                //cell13.Text = "";
                //cell13.HorizontalAlign = HorizontalAlign.Center;
                //cell13.ColumnSpan = 1;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvrow.Cells.Add(cell06);
                gvrow.Cells.Add(cell07);
                gvrow.Cells.Add(cell08);
                gvrow.Cells.Add(cell09);
                gvrow.Cells.Add(cell10);
                gvrow.Cells.Add(cell11);
                //gvrow.Cells.Add(cell12);
                //gvrow.Cells.Add(cell13);



                gvsaleswithcoll.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
    }
}