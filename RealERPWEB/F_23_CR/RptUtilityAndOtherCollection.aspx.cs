using Microsoft.Reporting.WinForms;
using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_23_CR
{
    public partial class RptUtilityAndOtherCollection : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Other collection";

                this.txttoDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();

            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {

            string rpt = this.ddlReport.SelectedValue;
            switch (rpt)
            {
                case "AllOtherColl":
                    this.PrintRptAllOtherColl();
                    break;

                default:
                    this.PrintRptIndOtherColl();
                    break;
            }
        }


        private string GetCompCod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }

        private void GetProjectName()
        {
            string comcod = this.GetCompCod();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "GETPROJECTNAME", "", "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string rpt = this.ddlReport.SelectedValue;
            switch (rpt)
            {
                case "AllOtherColl":
                    this.AllOtherCollection();
                    break;

                default:
                    this.IndOtherCollection();
                    break;
            }


        }

        public void AllOtherCollection()
        {
            string comcod = this.GetCompCod();
            string Date = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            string projectcode = this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "25%" : ddlProjectName.SelectedValue.ToString() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTSALOTHERRECPAYMENTALL", projectcode, Date, "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvallothrcoll.DataSource = null;
                this.gvallothrcoll.DataBind();

                return;

            }
            Session["tblallothers"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();


        }

        public void IndOtherCollection()
        {
            string comcod = this.GetCompCod();
            string Date = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            string projectcode = this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "25%" : ddlProjectName.SelectedValue.ToString() + "%";
            string rpt = this.ddlReport.SelectedValue;
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTSALOTHERRECPAYMENT01", projectcode, Date, rpt, "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvotherColl.DataSource = null;
                this.gvotherColl.DataBind();
                this.gvotherColl.DataSource = null;
                this.gvotherColl.DataBind();
                return;

            }
            Session["tblothers"] = this.HiddenSameData(ds1.Tables[0]);
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
                    dt1.Rows[j]["pactdesc"] = "";
                }

                pactcode = dt1.Rows[j]["pactcode"].ToString();
            }

            return dt1;
        }





        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblothers"];
            DataTable dt1 = (DataTable)Session["tblallothers"];
            string rpt = this.ddlReport.SelectedValue;
            switch (rpt)
            {
                case "AllOtherColl":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.gvallothrcoll.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvallothrcoll.DataSource = dt1;
                    this.gvallothrcoll.DataBind();
                    if (dt1.Rows.Count > 0)
                    {
                        Session["Report1"] = gvallothrcoll;
                        ((HyperLink)this.gvallothrcoll.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    }


                    break;
                default:
                    this.MultiView1.ActiveViewIndex = 0;
                    this.gvotherColl.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvotherColl.DataSource = dt;
                    this.gvotherColl.DataBind();
                    if (dt.Rows.Count > 0)
                    {
                        Session["Report1"] = gvotherColl;
                        ((HyperLink)this.gvotherColl.HeaderRow.FindControl("hlbtntbCdataExcel1")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    }

                    break;


            }
            //DataTable dt = (DataTable)Session["tblothers"];

            //string rpt = this.ddlReport.SelectedValue;
            //this.MultiView1.ActiveViewIndex = 0;
            //this.gvotherColl.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            //this.gvotherColl.DataSource = dt;
            //this.gvotherColl.DataBind();
            //    this.FooterCal();

            //default:
            //    break;
            //}



        }

        private void FooterCal()
        {
            //    DataTable dt = (DataTable)Session["amtbasis"];

            //    ((Label)this.gvMatStock.FooterRow.FindControl("lblrcvf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(rcvamt)", "")) ?
            //        0.00 : dt.Compute("Sum(rcvamt)", ""))).ToString("#,##0;(#,##0); ");

            //    ((Label)this.gvMatStock.FooterRow.FindControl("lbltinf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trninamt)", "")) ?
            //        0.00 : dt.Compute("Sum(trninamt)", ""))).ToString("#,##0;(#,##0); ");

            //    ((Label)this.gvMatStock.FooterRow.FindControl("lbltoutf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnoutamt)", "")) ?
            //       0.00 : dt.Compute("Sum(trnoutamt)", ""))).ToString("#,##0;(#,##0); ");

            //    ((Label)this.gvMatStock.FooterRow.FindControl("lbllstf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(lsamt)", "")) ?
            //        0.00 : dt.Compute("Sum(lsamt)", ""))).ToString("#,##0;(#,##0); ");

            //    ((Label)this.gvMatStock.FooterRow.FindControl("lblnetrcvf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(netrcvamt)", "")) ?
            //       0.00 : dt.Compute("Sum(netrcvamt)", ""))).ToString("#,##0;(#,##0); ");

            //    ((Label)this.gvMatStock.FooterRow.FindControl("lblisuf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(issueamt)", "")) ?
            //        0.00 : dt.Compute("Sum(issueamt)", ""))).ToString("#,##0;(#,##0); ");

            //    ((Label)this.gvMatStock.FooterRow.FindControl("lblbgdconf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdconamt)", "")) ?
            //       0.00 : dt.Compute("Sum(bgdconamt)", ""))).ToString("#,##0;(#,##0); ");

            //    ((Label)this.gvMatStock.FooterRow.FindControl("lblactstktf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(actstock)", "")) ?
            //        0.00 : dt.Compute("Sum(actstock)", ""))).ToString("#,##0;(#,##0); ");

            //    ((Label)this.gvMatStock.FooterRow.FindControl("lblbgdstkf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdstock)", "")) ?
            //       0.00 : dt.Compute("Sum(bgdstock)", ""))).ToString("#,##0;(#,##0); ");

            //    ((Label)this.gvMatStock.FooterRow.FindControl("lblvarf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(varamt)", "")) ?
            //        0.00 : dt.Compute("Sum(varamt)", ""))).ToString("#,##0;(#,##0); ");
        }
        private void PrintRptAllOtherColl()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = hst["comcod"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string reportType = this.ddlReport.SelectedItem.ToString();
            string date = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblallothers"];

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.RptUtilityAndOtherCollection>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptUtilityAndOtherCollAll", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Utility And Other Collection"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + date));
            Rpt1.SetParameters(new ReportParameter("reportType", "Report: " + reportType));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintRptIndOtherColl()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = hst["comcod"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string reportType = this.ddlReport.SelectedItem.ToString();
            string date = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblothers"];

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.RptUtilityAndOtherCollection>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptUtilityAndOtherCollInd", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Utility And Other Collection"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + date));
            Rpt1.SetParameters(new ReportParameter("reportType", "Report: " + reportType));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void gvotherColl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvotherColl.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvotherColl.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }
        protected void gvallothrcoll_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvallothrcoll.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void gvallothrcoll_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //gvrow.Cells.Remove(TableCell[0]);

                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.RowSpan = 1;
                gvrow.Cells.Add(cell01);



                TableCell cell02 = new TableCell();
                cell02.Text = "";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.RowSpan = 1;
                gvrow.Cells.Add(cell02);

                TableCell cell03 = new TableCell();
                cell03.Text = "";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.RowSpan = 1;
                gvrow.Cells.Add(cell03);


                TableCell cell04 = new TableCell();
                cell04.Text = "";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.RowSpan = 1;
                gvrow.Cells.Add(cell04);


                //TableCell cell05 = new TableCell();
                //cell05.Text = "";
                //cell05.Attributes["style"] = "font-weight:bold;";
                //cell05.HorizontalAlign = HorizontalAlign.Center;
                //cell05.RowSpan = 1;
                //gvrow.Cells.Add(cell05);


                //TableCell cell05 = new TableCell();
                //cell05.Text = "Installment";
                //cell05.HorizontalAlign = HorizontalAlign.Center;
                //cell05.Attributes["style"] = "font-weight:bold;";
                //cell05.ColumnSpan = 3;
                //gvrow.Cells.Add(cell05);

                //TableCell cell06 = new TableCell();
                //cell06.Text = "Utility";
                //cell06.HorizontalAlign = HorizontalAlign.Center;
                //cell06.Attributes["style"] = "font-weight:bold;";
                //cell06.ColumnSpan = 3;
                //gvrow.Cells.Add(cell06);



                //TableCell cell06B = new TableCell();
                //cell06B.Text = "Upgradation";
                //cell06B.HorizontalAlign = HorizontalAlign.Center;
                //cell06B.Attributes["style"] = "font-weight:bold;";
                //cell06B.ColumnSpan = 3;
                //gvrow.Cells.Add(cell06B);


                TableCell cell07 = new TableCell();
                cell07.Text = "Registration Fee";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.Attributes["style"] = "font-weight:bold;";
                cell07.ColumnSpan = 3;
                gvrow.Cells.Add(cell07);

                TableCell cell08 = new TableCell();
                cell08.Text = "Additional";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.Attributes["style"] = "font-weight:bold;";
                cell08.ColumnSpan = 3;
                gvrow.Cells.Add(cell08);

                TableCell cell09 = new TableCell();
                cell09.Text = "Association Fee";
                cell09.HorizontalAlign = HorizontalAlign.Center;
                cell09.Attributes["style"] = "font-weight:bold;";
                cell09.ColumnSpan = 3;
                gvrow.Cells.Add(cell09);


                TableCell cell10 = new TableCell();
                cell10.Text = "Utility";
                cell10.HorizontalAlign = HorizontalAlign.Center;
                cell10.Attributes["style"] = "font-weight:bold;";
                cell10.ColumnSpan = 3;
                gvrow.Cells.Add(cell10);


                TableCell cell11 = new TableCell();
                cell11.Text = "Society";
                cell11.HorizontalAlign = HorizontalAlign.Center;
                cell11.Attributes["style"] = "font-weight:bold;";
                cell11.ColumnSpan = 3;
                gvrow.Cells.Add(cell11);

                TableCell cell12 = new TableCell();
                cell12.Text = "Service Fee";
                cell12.HorizontalAlign = HorizontalAlign.Center;
                cell12.Attributes["style"] = "font-weight:bold;";
                cell12.ColumnSpan = 3;
                gvrow.Cells.Add(cell12);

                TableCell cell13 = new TableCell();
                cell13.Text = "Mutation";
                cell13.HorizontalAlign = HorizontalAlign.Center;
                cell13.Attributes["style"] = "font-weight:bold;";
                cell13.ColumnSpan = 3;
                gvrow.Cells.Add(cell13);

                TableCell cell14 = new TableCell();
                cell14.Text = "Optoinal";
                cell14.HorizontalAlign = HorizontalAlign.Center;
                cell14.Attributes["style"] = "font-weight:bold;";
                cell14.ColumnSpan = 3;
                gvrow.Cells.Add(cell14);

                TableCell cell15 = new TableCell();
                cell15.Text = "Delay Charge";
                cell15.HorizontalAlign = HorizontalAlign.Center;
                cell15.Attributes["style"] = "font-weight:bold;";
                cell15.ColumnSpan = 3;
                gvrow.Cells.Add(cell15);

                gvallothrcoll.Controls[0].Controls.AddAt(0, gvrow);



                gvallothrcoll.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
        protected void gvallothrcoll_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;


            // Label code = (Label)e.Row.FindControl("lblgvrptcode");

            Label lgvCustNameall = (Label)e.Row.FindControl("lgvCustNameall");
            Label lgvreceiptreg = (Label)e.Row.FindControl("lgvreceiptreg");
            Label lgvpaymentreg = (Label)e.Row.FindControl("lgvpaymentreg");
            Label lgvBalancereg = (Label)e.Row.FindControl("lgvBalancereg");
            Label lgvreceiptadw = (Label)e.Row.FindControl("lgvreceiptadw");
            Label lgvpaymentadw = (Label)e.Row.FindControl("lgvpaymentadw");
            Label lgvBalanceadw = (Label)e.Row.FindControl("lgvBalanceadw");
            Label lgvreceiptasscia = (Label)e.Row.FindControl("lgvreceiptasscia");
            Label lgvpaymentasscia = (Label)e.Row.FindControl("lgvpaymentasscia");
            Label lgvBalanceasscia = (Label)e.Row.FindControl("lgvBalanceasscia");
            Label lgvreceiptutility = (Label)e.Row.FindControl("lgvreceiptutility");
            Label lgvpaymentutility = (Label)e.Row.FindControl("lgvpaymentutility");
            Label lgvBalanceutility = (Label)e.Row.FindControl("lgvBalanceutility");
            Label lgvreceiptsociety = (Label)e.Row.FindControl("lgvreceiptsociety");
            Label lgvpaymentsociety = (Label)e.Row.FindControl("lgvpaymentsociety");
            Label lgvBalancesociety = (Label)e.Row.FindControl("lgvBalancesociety");
            Label lgvreceiptservice = (Label)e.Row.FindControl("lgvreceiptservice");
            Label lgvpaymentservice = (Label)e.Row.FindControl("lgvpaymentservice");
            Label lgvBalanceservice = (Label)e.Row.FindControl("lgvBalanceservice");
            Label lgvreceiptmutation = (Label)e.Row.FindControl("lgvreceiptmutation");
            Label lgvpaymentmutation = (Label)e.Row.FindControl("lgvpaymentmutation");
            Label lgvBalancemutation = (Label)e.Row.FindControl("lgvBalancemutation");
            Label lgvreceiptoptional = (Label)e.Row.FindControl("lgvreceiptoptional");
            Label lgvpaymentoptional = (Label)e.Row.FindControl("lgvpaymentoptional");
            Label lgvBalanceoptional = (Label)e.Row.FindControl("lgvBalanceoptional");
            Label lgvreceiptdelay = (Label)e.Row.FindControl("lgvreceiptdelay");
            Label lgvpaymentdelay = (Label)e.Row.FindControl("lgvpaymentdelay");
            Label lgvBalancedelay = (Label)e.Row.FindControl("lgvBalancedelay");





            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString().Trim();

            if (code == "")
            {
                return;
            }
            if (ASTUtility.Right(code, 3) == "AAA")
            {
                lgvCustNameall.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvreceiptreg.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvpaymentreg.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvBalancereg.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvreceiptadw.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvpaymentadw.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvBalanceadw.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvreceiptasscia.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvpaymentasscia.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvBalanceasscia.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvreceiptutility.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvpaymentutility.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvBalanceutility.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvreceiptsociety.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvpaymentsociety.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvBalancesociety.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvreceiptservice.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvpaymentservice.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvBalanceservice.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvreceiptmutation.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvpaymentmutation.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvBalancemutation.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvreceiptoptional.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvpaymentoptional.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvBalanceoptional.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvreceiptdelay.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvpaymentdelay.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvBalancedelay.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";





                //lgvNagad.Style.Add("text-align", "left");
                lgvCustNameall.Style.Add("text-align", "right");

            }

            if (ASTUtility.Right(code, 3) == "BBB")
            {
                lgvCustNameall.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvreceiptreg.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvpaymentreg.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvBalancereg.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvreceiptadw.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvpaymentadw.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvBalanceadw.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvreceiptasscia.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvpaymentasscia.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvBalanceasscia.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvreceiptutility.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvpaymentutility.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvBalanceutility.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvreceiptsociety.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvpaymentsociety.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvBalancesociety.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvreceiptservice.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvpaymentservice.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvBalanceservice.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvreceiptmutation.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvpaymentmutation.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvBalancemutation.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvreceiptoptional.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvpaymentoptional.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvBalanceoptional.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvreceiptdelay.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvpaymentdelay.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvBalancedelay.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";

                lgvCustNameall.Style.Add("text-align", "right");

            }


        }

        protected void gvotherColl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;


            // Label code = (Label)e.Row.FindControl("lblgvrptcode");

            Label lgvCustName = (Label)e.Row.FindControl("lgvCustName");
            Label lgvreceiptamt = (Label)e.Row.FindControl("lgvreceiptamt");
            Label lgvpayment = (Label)e.Row.FindControl("lgvpayment");
            Label lgvBalance = (Label)e.Row.FindControl("lgvBalance");


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString().Trim();

            if (code == "")
            {
                return;
            }
            if (ASTUtility.Right(code, 3) == "AAA")
            {
                lgvCustName.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvreceiptamt.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvpayment.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvBalance.Attributes["style"] = "font-weight:bold; color:maroon;font-family: Century Gothic, sans-serif;";
                lgvCustName.Style.Add("text-align", "right");
            }

            if (ASTUtility.Right(code, 3) == "BBB")
            {
                lgvCustName.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvreceiptamt.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvpayment.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvBalance.Attributes["style"] = "font-weight:bold; color:blue;font-family: Century Gothic, sans-serif;";
                lgvCustName.Style.Add("text-align", "right");
            }

        }
    }

}