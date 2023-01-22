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
using Microsoft.Reporting.WinForms;
using System.IO;
using RealERPLIB;
using RealERPRPT;
using System.Reflection;
namespace RealERPWEB.F_81_Hrm.F_89_Pay
{

    public partial class RptSalarySummary : System.Web.UI.Page
    {
        Common compUtility = new Common();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                //if ((!ASTUtility.PagePermission (HttpContext.Current.Request.Url.AbsoluteUri.ToString (),
                //        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean (hst["permission"]))
                //    Response.Redirect ("../../AcceessError.aspx");
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.SelectType();
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
               // ((Label)this.Master.FindControl("lblTitle")).Text = "Salary Summary";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
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
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.ddlComName.SelectedValue.Length > 0 ? this.ddlComName.SelectedValue.ToString() : comcod;
            return comcod;
        }

        private void Company()
        {
            string comcod = this.GetCompCode();
            string consolidate = "";
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", "COMPLIST", consolidate, "", "", "", "", "", "", "", "");
            this.ddlComName.DataTextField = "comsnam";
            this.ddlComName.DataValueField = "comcod";
            this.ddlComName.DataSource = ds1.Tables[0];
            this.ddlComName.DataBind();

        }
        private void SelectType()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
                return;

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);

            switch (type)
            {
                case "SalSum":
                    this.MultiView1.ActiveViewIndex = 0;
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    switch (comcod)
                    {

                        case "4305"://Sanamr
                        case "3330"://Bridge
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
                    break;


                case "SalSum02":
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = startdate + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;


            }

        }



        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.SelectIndex();
        }

        private void SelectIndex()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "SalSum":
                    //this.EmpCashPay();
                    this.loadSalSum();

                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "SalSum02":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.loadSalSum02();
                    break;
            }
        }
        protected void loadSalSum()
        {
            //  load data into salsum

            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string curmonhtid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM");
            string prmonthid = Convert.ToDateTime(this.txttodate.Text).AddMonths(-1).ToString("yyyyMM");
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPT_SALSUM", "", curmonhtid, prmonthid, "", "", "", "", "", "");

            if (ds3 == null)
            {
                this.gvSalSum.DataSource = null;
                this.gvSalSum.DataBind();
                return;

            }

            DataTable dt = ds3.Tables[0];
            Session["tblSalSum"] = dt;
            this.Data_Bind();

        }


        private void loadSalSum02()
        {
            //  load data into salsum
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string curmonhtid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM");
            string prmonthid = Convert.ToDateTime(this.txttodate.Text).AddMonths(-1).ToString("yyyyMM");
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPTGROUPSALSUM", "", curmonhtid, prmonthid, "", "", "", "", "", "");

            if (ds3 == null)
            {
                this.gvgssal02.DataSource = null;
                this.gvgssal02.DataBind();
                return;

            }

            DataTable dt = ds3.Tables[0];
            Session["tblSalSum"] = dt;
            this.Data_Bind();


        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblSalSum"];

            //  gvSalSum.Columns[1].HeaderText =(this.ddlCompany.SelectedValue.ToString()=="000000000000")?"Company Name":"Department Name";

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SalSum":

                    this.gvSalSum.DataSource = dt;
                    this.gvSalSum.DataBind();
                    this.FooterCalculation();
                    break;

                case "SalSum02":

                    this.gvgssal02.Columns[3].HeaderText = this.txtfromdate.Text.ToString();
                    this.gvgssal02.Columns[4].HeaderText = this.txttodate.Text.ToString();
                    this.gvgssal02.Columns[5].HeaderText = "Gross Salary Last Month " + Convert.ToDateTime(this.txtfromdate.Text).AddMonths(-1).ToString("MMMM-yyyy");
                    this.gvgssal02.Columns[6].HeaderText = "Gross Salary & Allowance " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM-yyyy");
                    this.gvgssal02.Columns[13].HeaderText = "Net Payable after tax " + Convert.ToDateTime(this.txtfromdate.Text).AddMonths(-1).ToString("MMMM-yyyy");
                    this.gvgssal02.Columns[14].HeaderText = "Net Payable after tax " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM-yyyy");
                    this.gvgssal02.DataSource = dt;
                    this.gvgssal02.DataBind();
                    this.FooterCalculation();
                    break;


            }

        }


        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            if (dt.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SalSum":
                    // this.gvSalSum.HeaderRow.FindControl()
                    //lgvHTCurMamt
                    //lgvHTPreMamt
                    //lgvHTPreMEmp
                    //lgvHTCurMEmp
                    string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMM-yy");
                    string premonth = Convert.ToDateTime(this.txttodate.Text).AddMonths(-1).ToString("MMM-yy");
                    ((Label)this.gvSalSum.HeaderRow.FindControl("lgvHTCurMEmp")).Text = todate + "</br>" + "Emp.";
                    ((Label)this.gvSalSum.HeaderRow.FindControl("lgvHTCurMamt")).Text = todate + "</br>" + "Net </br>Payable";
                    ((Label)this.gvSalSum.HeaderRow.FindControl("lgvHTPreMEmp")).Text = premonth + "</br>" + "Emp.";
                    ((Label)this.gvSalSum.HeaderRow.FindControl("lgvHTPreMamt")).Text = premonth + "</br>" + "Net </br>Payable";

                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFTCurEmp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curempno)", "")) ? 0.00 : dt.Compute("sum(curempno)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFTCurMamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curpay)", "")) ? 0.00 : dt.Compute("sum(curpay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFTPreEmp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(preempno)", "")) ? 0.00 : dt.Compute("sum(preempno)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFTPreMamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(prepay)", "")) ? 0.00 : dt.Compute("sum(prepay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFTCurSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cursal)", "")) ? 0.00 : dt.Compute("sum(cursal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFTPreSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(presal)", "")) ? 0.00 : dt.Compute("sum(presal)", ""))).ToString("#,##0;(#,##0); ");



                    break;



                case "SalSum02":

                    ((Label)this.gvgssal02.FooterRow.FindControl("lgvFcempno")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curempno)", "")) ? 0.00 : dt.Compute("sum(curempno)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvgssal02.FooterRow.FindControl("lgvFpreloanbal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(loanbal)", "")) ? 0.00 : dt.Compute("sum(loanbal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvgssal02.FooterRow.FindControl("lgvFcurloanbal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(loanbal2)", "")) ? 0.00 : dt.Compute("sum(loanbal2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvgssal02.FooterRow.FindControl("lgvFpregssalary")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pregssal)", "")) ? 0.00 : dt.Compute("sum(pregssal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvgssal02.FooterRow.FindControl("lgvFgspay")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 : dt.Compute("sum(gssal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvgssal02.FooterRow.FindControl("lgvFloan")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(loanins)", "")) ? 0.00 : dt.Compute("sum(loanins)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvgssal02.FooterRow.FindControl("lgvFadvalateduc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tothdeduc)", "")) ? 0.00 : dt.Compute("sum(tothdeduc)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvgssal02.FooterRow.FindControl("lgvFpffund")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfund)", "")) ? 0.00 : dt.Compute("sum(pfund)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvgssal02.FooterRow.FindControl("lgvFnetpaybftax")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpaybftax)", "")) ? 0.00 : dt.Compute("sum(netpaybftax)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvgssal02.FooterRow.FindControl("lgvFait")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itax)", "")) ? 0.00 : dt.Compute("sum(itax)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvgssal02.FooterRow.FindControl("lgvFprenetpayaftax")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(prenetpay)", "")) ? 0.00 : dt.Compute("sum(prenetpay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvgssal02.FooterRow.FindControl("lgvFnetpayaftax")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", ""))).ToString("#,##0;(#,##0); ");
                    break;
            }

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "SalSum":
                    this.PrintSalSum();
                    break;

                case "SalSum02":
                    this.PrintSalSum02();

                    break;
            }

        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }

        protected void gvcashpay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvSalSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSalSum.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void PrintSalSum()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMM-yy");
            string premonth = Convert.ToDateTime(this.txttodate.Text).AddMonths(-1).ToString("MMM-yy");
            string comcod = hst["comcod"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.SalarySumm>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalSumm", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("Comname", comname));
            Rpt1.SetParameters(new ReportParameter("comaddress", comadd));
            Rpt1.SetParameters(new ReportParameter("rpttitle", "Salary Summary"));
            Rpt1.SetParameters(new ReportParameter("date", "Month of " + Convert.ToDateTime(todate).ToString("MMMM, yyyy")));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintSalSum02()
        {

            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMM-yy");
            string premonth = Convert.ToDateTime(this.txttodate.Text).AddMonths(-1).ToString("MMM-yy");

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.RptGrpSalSummary>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptGrpSummary", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comname));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtMonth", "Salary & Remuneration Statment For The Month Of " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtPreloan", this.txtfromdate.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("txtCurloan", this.txttodate.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("txtPregssal", "Gross Salary Last Month " + Convert.ToDateTime(this.txtfromdate.Text).AddMonths(-1).ToString("MMMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtCurgssal", "Gross Salary & Allowance " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtPrenetpay", "Net Payable After Tax " + Convert.ToDateTime(this.txtfromdate.Text).AddMonths(-1).ToString("MMMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtCurnetpay", "Net Payable After Tax " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM-yyyy")));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void gvgssal02_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void gvgssal02_RowCreated(object sender, GridViewRowEventArgs e)
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
                cell03.ColumnSpan = 1;
                cell03.Font.Bold = true;




                TableCell cell04 = new TableCell();
                cell04.Text = "Loan Balance";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 2;
                cell04.Font.Bold = true;




                TableCell cell05 = new TableCell();
                cell05.Text = "";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 1;
                cell05.Font.Bold = true;


                TableCell cell06 = new TableCell();
                cell06.Text = "";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 1;
                cell06.Font.Bold = true;


                TableCell cell07 = new TableCell();
                cell07.Text = "Salary Deduction";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.ColumnSpan = 3;
                cell07.Font.Bold = true;

                TableCell cell08 = new TableCell();
                cell08.Text = "";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.ColumnSpan = 1;
                cell08.Font.Bold = true;

                TableCell cell09 = new TableCell();
                cell09.Text = "Deduction";
                cell09.HorizontalAlign = HorizontalAlign.Center;
                cell09.ColumnSpan = 2;
                cell09.Font.Bold = true;

                TableCell cell10 = new TableCell();
                cell10.Text = "";
                cell10.HorizontalAlign = HorizontalAlign.Center;
                cell10.ColumnSpan = 1;
                cell10.Font.Bold = true;

                TableCell cell11 = new TableCell();
                cell11.Text = "";
                cell11.HorizontalAlign = HorizontalAlign.Center;
                cell11.ColumnSpan = 1;
                cell11.Font.Bold = true;




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

                gvgssal02.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
    }

}

