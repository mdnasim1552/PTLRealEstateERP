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
namespace RealERPWEB.F_16_Bill
{
    public partial class RptBillStatus : System.Web.UI.Page
    {
        ProcessAccess BgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Project Status";
                //this.Master.Page.Title = "Project Status";

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmDate.Text = (this.Request.QueryString["Date1"].ToString().Length > 0) ? this.Request.QueryString["Date1"] : ("01" + date.Substring(2));
                this.txttoDate.Text = (this.Request.QueryString["Date2"].ToString().Length > 0) ? this.Request.QueryString["Date2"] : date;


            }
            if (this.ddlProjectName.Items.Count == 0)
            {
                this.GetProjectName();

            }




        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = ((this.Request.QueryString["prjcode"].ToString().Trim().Length > 0) ? this.Request.QueryString["prjcode"].ToString() : ("%" + this.txtSrcPro.Text.Trim())) + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "GETPURPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }

        protected void ibtnFindProject_OnClick(object sender, EventArgs e)
        {
            this.GetProjectName();
        }




        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowBill();

        }



        private void ShowBill()
        {
            Session.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "RPTPROJECTSTATUS", PactCode, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSubBill.DataSource = null;
                this.gvSubBill.DataBind();
                return;
            }
            Session["tblData"] = this.HiddenSameData(ds1.Tables[0]);
            this.LoadGrid();

        }




        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string grp;
            grp = dt1.Rows[0]["grp"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)

                    dt1.Rows[j]["grpdesc"] = "";

                grp = dt1.Rows[j]["grp"].ToString();

            }
            return dt1;

        }

        private void LoadGrid()
        {

            DataTable dt = (DataTable)Session["tblData"];
            this.gvSubBill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSubBill.DataSource = dt;
            this.gvSubBill.DataBind();
            this.FooterCalculation();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //Iqbal Nayan
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
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            string date = "(" + "From: " + frmdate + " To " + todate + ")";
            string Project = this.ddlProjectName.SelectedItem.Text.Trim().ToString();
            DataTable dt = (DataTable)Session["tblData"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_16_Bill.BO_BillEntry.ProjectStarus>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_16_Bill.RptProjStarus", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Project Status"));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", Project));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblData"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvSubBill.FooterRow.FindControl("lblgvFordam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordam)", "")) ?
                               0 : dt.Compute("sum(ordam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lblgvFprebam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pream)", "")) ?
                              0 : dt.Compute("sum(pream)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvSubBill.FooterRow.FindControl("lblgvFcuram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curam)", "")) ?
                              0 : dt.Compute("sum(curam)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvSubBill.FooterRow.FindControl("lblgvFtbillam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tbillam)", "")) ?
                              0 : dt.Compute("sum(tbillam)", ""))).ToString("#,##0;(#,##0); ");


            ((Label)this.gvSubBill.FooterRow.FindControl("lblgvFbalam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balam)", "")) ?
                              0 : dt.Compute("sum(balam)", ""))).ToString("#,##0;(#,##0); ");


            ((Label)this.gvSubBill.FooterRow.FindControl("lblgvFbgdam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdam)", "")) ?
                              0 : dt.Compute("sum(bgdam)", ""))).ToString("#,##0;(#,##0); ");
        }


        //protected void lbtnPrint_Click(object sender, EventArgs e)
        //{

        //    //Hashtable hst = (Hashtable)Session["tblLogin"];
        //    //string comcod = hst["comcod"].ToString();
        //    //string comnam = hst["comnam"].ToString();
        //    //string comadd = hst["comadd1"].ToString();
        //    //string compname = hst["compname"].ToString();
        //    //string username = hst["username"].ToString();
        //    //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    //string basis = (Convert.ToInt32(this.rbtnList1.SelectedIndex.ToString()) == 0) ? "Performance Evaluation Report-Qty Basis" : "Performance Evaluation Report-Amount Basis";
        //    //DataTable dt = (DataTable)Session["tblData"];
        //    //ReportDocument rptsale = new RealERPRPT.R_16_Bill.rptPerformanceEvaluation();
        //    //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
        //    //rptCname.Text = comnam;
        //    //TextObject rptdBasis = rptsale.ReportDefinition.ReportObjects["Basis"] as TextObject;
        //    //rptdBasis.Text =basis;
        //    //TextObject rptpactdesc = rptsale.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
        //    //rptpactdesc.Text = "Project Name: " +this.ddlProjectName.SelectedItem.Text;
        //    //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["date"] as TextObject;
        //    //rptDate.Text = "(From " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy")+" To "+Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy")+")";
        //    //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    //rptsale.SetDataSource(dt);
        //    //Session["Report1"] = rptsale;
        //    //((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //    //      ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";


        //}









        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void gvSpayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSubBill.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }



        protected void gvSubBill_RowCreated(object sender, GridViewRowEventArgs e)
        {

            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {


                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //  gvrow.Cells.Remove(TableCell [0]);

                TableCell cell01 = new TableCell();
                cell01.Text = "Sl.No.";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.RowSpan = 2;
                gvrow.Cells.Add(cell01);



                TableCell cell02 = new TableCell();
                cell02.Text = "Floor";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.RowSpan = 2;
                gvrow.Cells.Add(cell02);

                TableCell cell03 = new TableCell();
                cell03.Text = "Work Description";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.RowSpan = 2;
                gvrow.Cells.Add(cell03);

                TableCell cell04au = new TableCell();
                cell04au.Text = "Unit";
                cell04au.HorizontalAlign = HorizontalAlign.Center;
                cell04au.RowSpan = 2;
                gvrow.Cells.Add(cell04au);


                TableCell cell04 = new TableCell();
                cell04.Text = "Quantity";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.RowSpan = 2;
                gvrow.Cells.Add(cell04);

                //TableCell cell04a = new TableCell();
                //cell04a.Text = "Rate";
                //cell04a.HorizontalAlign = HorizontalAlign.Center;
                //cell04a.RowSpan = 2;
                //gvrow.Cells.Add(cell04a);

                TableCell cell05 = new TableCell();
                cell05.Text = "Tender Amount";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.Attributes["style"] = "font-weight:bold;";
                cell05.ColumnSpan = 2;
                gvrow.Cells.Add(cell05);



                TableCell cell06 = new TableCell();
                cell06.Text = "Previous Bill";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.Attributes["style"] = "font-weight:bold;";
                cell06.ColumnSpan = 2;
                gvrow.Cells.Add(cell06);


                TableCell cell07 = new TableCell();
                cell07.Text = "Current Bill";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.Attributes["style"] = "font-weight:bold;";
                cell07.ColumnSpan = 2;
                gvrow.Cells.Add(cell07);


                TableCell cell08 = new TableCell();
                cell08.Text = "Total Bill";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.Attributes["style"] = "font-weight:bold;";
                cell08.ColumnSpan = 2;
                gvrow.Cells.Add(cell08);






                TableCell cell09 = new TableCell();
                cell09.Text = "Work In Hand";
                cell09.HorizontalAlign = HorizontalAlign.Center;
                cell09.Attributes["style"] = "font-weight:bold;";
                cell09.ColumnSpan = 2;
                gvrow.Cells.Add(cell09);


                TableCell cell10 = new TableCell();
                cell10.Text = "Bugted Amount";
                cell10.HorizontalAlign = HorizontalAlign.Center;
                cell10.Attributes["style"] = "font-weight:bold;";
                cell10.ColumnSpan = 2;
                gvrow.Cells.Add(cell10);
                gvSubBill.Controls[0].Controls.AddAt(0, gvrow);
            }

        }
        protected void gvSubBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {


                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //  gvrow.Cells.Remove(TableCell [0]);

                TableCell cell01 = new TableCell();
                cell01.Text = "Sl.No.";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.RowSpan = 2;
                gvrow.Cells.Add(cell01);



                TableCell cell02 = new TableCell();
                cell02.Text = "Floor";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.RowSpan = 2;
                gvrow.Cells.Add(cell02);

                TableCell cell03 = new TableCell();
                cell03.Text = "Work Description";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.RowSpan = 2;
                gvrow.Cells.Add(cell03);

                TableCell cell04au = new TableCell();
                cell04au.Text = "Unit";
                cell04au.HorizontalAlign = HorizontalAlign.Center;
                cell04au.RowSpan = 2;
                gvrow.Cells.Add(cell04au);


                TableCell cell04 = new TableCell();
                cell04.Text = "Quantity";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.RowSpan = 2;
                gvrow.Cells.Add(cell04);

                //TableCell cell04a = new TableCell();
                //cell04a.Text = "Rate";
                //cell04a.HorizontalAlign = HorizontalAlign.Center;
                //cell04a.RowSpan = 2;
                //gvrow.Cells.Add(cell04a);

                TableCell cell05 = new TableCell();
                cell05.Text = "Tender Amount";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.Attributes["style"] = "font-weight:bold;";
                cell05.ColumnSpan = 2;
                gvrow.Cells.Add(cell05);



                TableCell cell06 = new TableCell();
                cell06.Text = "Previous Bill";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.Attributes["style"] = "font-weight:bold;";
                cell06.ColumnSpan = 2;
                gvrow.Cells.Add(cell06);


                TableCell cell07 = new TableCell();
                cell07.Text = "Current Bill";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.Attributes["style"] = "font-weight:bold;";
                cell07.ColumnSpan = 2;
                gvrow.Cells.Add(cell07);


                TableCell cell08 = new TableCell();
                cell08.Text = "Total Bill";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.Attributes["style"] = "font-weight:bold;";
                cell08.ColumnSpan = 2;
                gvrow.Cells.Add(cell08);






                TableCell cell09 = new TableCell();
                cell09.Text = "Work In Hand";
                cell09.HorizontalAlign = HorizontalAlign.Center;
                cell09.Attributes["style"] = "font-weight:bold;";
                cell09.ColumnSpan = 2;
                gvrow.Cells.Add(cell09);


                TableCell cell10 = new TableCell();
                cell10.Text = "Bugted Amount";
                cell10.HorizontalAlign = HorizontalAlign.Center;
                cell10.Attributes["style"] = "font-weight:bold;";
                cell10.ColumnSpan = 2;
                gvrow.Cells.Add(cell10);
                gvSubBill.Controls[0].Controls.AddAt(0, gvrow);
            }


            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                //e.Row.Cells[5].Visible = false;
                //e.Row.Cells[14].Visible = false;

            }

        }
    }
}

