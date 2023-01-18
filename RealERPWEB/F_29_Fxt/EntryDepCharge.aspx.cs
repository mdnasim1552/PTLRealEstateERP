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
namespace RealERPWEB.F_29_Fxt
{
    public partial class EntryDepCharge : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "DEPRECIATION CHARGE CALCULATION";



                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.GetOpeningDate();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }



        private string GetComcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());



        }

        private void GetOpeningDate()
        {

            string date = "";
            string comcod = this.GetComcod();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSET_INFO", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                date = Convert.ToDateTime(System.DateTime.Today).ToString("dd-MMM-yyyy");
                this.txtFromdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy"); ;
                this.txtTodate.Text = Convert.ToDateTime(this.txtFromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                return;
            }

            date = Convert.ToDateTime(ds1.Tables[0].Rows[0]["depodate"]).AddDays(1).ToString("dd-MMM-yyyy");
            this.txtFromdate.Text = Convert.ToDateTime(date).ToString("dd-MMM-yyyy"); ;
            this.txtTodate.Text = Convert.ToDateTime(this.txtFromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            this.txtFromdate.ReadOnly = true;


            ds1.Dispose();


        }



        private DataTable HiddenSameData(DataTable dt1)
        {

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                    dt1.Rows[j]["rsirdesc"] = "";
                }

                else
                {



                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                    {
                        dt1.Rows[j]["pactdesc"] = "";
                    }

                    if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                    {
                        dt1.Rows[j]["rsirdesc"] = "";

                    }
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    rsircode = dt1.Rows[j]["rsircode"].ToString();

                }

            }
            return dt1;


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

            //Rdlc
            string date = "Period: " + Convert.ToDateTime(this.txtFromdate.Text).ToString("dd-MMM-yyyy") + "  To  " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd-MMM-yyyy");
            int dateDife = ASTUtility.Datediffday(Convert.ToDateTime(this.txtTodate.Text), Convert.ToDateTime(this.txtFromdate.Text));
            int dateDife1 = dateDife + 1;
            string rpttxtDays = "Days : " + dateDife1.ToString();
            string txtBalance = "Balance as on " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            string txtTotal = "Balane as on " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");
            string txtDepr = "Depreciatoin as on " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            string txtWD = "W.D Values as on " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            DataTable dt = (DataTable)Session["tblDepcost"];

            var lst = dt.DataTableToList<RealEntity.C_29_Fxt.EClassFixedAsset.EClassDepricationCost>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_29_Fxt.RptDepricationCharge", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Fixed Assets Schedule"));
            Rpt1.SetParameters(new ReportParameter("rpttxtDays", rpttxtDays));
            Rpt1.SetParameters(new ReportParameter("txtBalance", txtBalance));
            Rpt1.SetParameters(new ReportParameter("txtTotal", txtTotal));
            Rpt1.SetParameters(new ReportParameter("txtDepr", txtDepr));
            Rpt1.SetParameters(new ReportParameter("txtWD", txtWD));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }






        protected void grDep_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grDep.PageIndex = e.NewPageIndex;
            this.grDep_DataBind();
        }

        private void grDep_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["tblDepcost"];
            this.grDep.Columns[2].HeaderText = "Balance as on " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            this.grDep.Columns[6].HeaderText = "Balance as on " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");
            this.grDep.Columns[8].HeaderText = "Depreciation as on " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            this.grDep.Columns[12].HeaderText = "W.D Values as on " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");
            this.grDep.DataSource = tbl1;
            this.grDep.DataBind();

        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            Session.Remove("tblDepcost");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frdate = Convert.ToDateTime(this.txtFromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTodate.Text).ToString("dd-MMM-yyyy");

            string straight = (this.chkStraight.Checked) ? "straight" : "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSET_INFO", "RPTDEPRECIATION", frdate, todate, straight, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grDep.DataSource = null;
                this.grDep.DataBind();
                return;
            }


            //int dateDife = TimeSpan(Convert.ToDateTime(this.txtTodate.Text), Convert.ToDateTime(this.txtFromdate.Text)); ASTUtility.Datediffday(Convert.ToDateTime(this.txtTodate.Text), Convert.ToDateTime(this.txtFromdate.Text));


            this.txtDays.Text = "Days: " + Convert.ToDouble(ds1.Tables[1].Rows[0]["cday"]).ToString("#,##0;(#,##0);");
            Session["tblDepcost"] = (DataTable)ds1.Tables[0];
            this.grDep_DataBind();
            this.FooterRowCal();
        }
        private void FooterRowCal()
        {
            DataTable dt = (DataTable)Session["tblDepcost"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.grDep.FooterRow.FindControl("lgvFTOpening")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ?
                                   0 : dt.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grDep.FooterRow.FindControl("lgvFTAddition")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curam)", "")) ?
                                   0 : dt.Compute("sum(curam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.grDep.FooterRow.FindControl("lgvFsalesdec")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(saleam)", "")) ?
                                 0 : dt.Compute("sum(saleam)", ""))).ToString("#,##0;(#,##0); ");


            ((Label)this.grDep.FooterRow.FindControl("lgvFTDisposal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(disam)", "")) ?
                                  0 : dt.Compute("sum(disam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.grDep.FooterRow.FindControl("lgvFTTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toam)", "")) ?
                                   0 : dt.Compute("sum(toam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.grDep.FooterRow.FindControl("lgvFTDepOpen")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opndep)", "")) ?
                                   0 : dt.Compute("sum(opndep)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.grDep.FooterRow.FindControl("lgvFadjment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(adjam)", "")) ?
                                   0 : dt.Compute("sum(adjam)", ""))).ToString("#,##0;(#,##0); ");



            ((Label)this.grDep.FooterRow.FindControl("lgvFTDepCur")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curdep)", "")) ?
                                   0 : dt.Compute("sum(curdep)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grDep.FooterRow.FindControl("lgvFTDepTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(todep)", "")) ?
                                   0 : dt.Compute("sum(todep)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grDep.FooterRow.FindControl("lgvFTCBal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsam)", "")) ?
                                   0 : dt.Compute("sum(clsam)", ""))).ToString("#,##0;(#,##0); ");

            string straight = (this.chkStraight.Checked) ? "straight" : "";
            ((HyperLink)this.grDep.FooterRow.FindControl("hlnkgvFdep")).NavigateUrl = "~/F_17_Acc/AccDepJournal.aspx?&Date1=" + Convert.ToDateTime(this.txtFromdate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txtTodate.Text).ToString("dd-MMM-yyyy") + "&Method=" + straight;
        }

        protected void grDep_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;

                TableCell cell1 = new TableCell();
                cell1.Text = "";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                cell1.ColumnSpan = 1;
                cell1.Font.Bold = true;

                TableCell cell2 = new TableCell();
                cell2.Text = "Cost";
                cell2.HorizontalAlign = HorizontalAlign.Center;
                cell2.ColumnSpan = 5;
                cell2.Font.Bold = true;

                TableCell cell3 = new TableCell();
                cell3.Text = "";
                cell3.HorizontalAlign = HorizontalAlign.Center;
                cell3.ColumnSpan = 1;
                cell3.Font.Bold = true;

                TableCell cell4 = new TableCell();
                cell4.Text = "Depreciation";
                cell4.HorizontalAlign = HorizontalAlign.Center;
                cell4.ColumnSpan = 4;
                cell4.Font.Bold = true;

                TableCell cell5 = new TableCell();
                cell5.Text = "";
                cell5.HorizontalAlign = HorizontalAlign.Center;
                cell5.ColumnSpan = 1;
                cell5.Font.Bold = true;



                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell1);
                gvrow.Cells.Add(cell2);
                gvrow.Cells.Add(cell3);
                gvrow.Cells.Add(cell4);
                gvrow.Cells.Add(cell5);

                grDep.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
    }
}











