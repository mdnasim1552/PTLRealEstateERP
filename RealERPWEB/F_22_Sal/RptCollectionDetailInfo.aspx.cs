using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;

namespace RealERPWEB.F_22_Sal
{
    public partial class RptCollectionDetailInfo : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
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

                //((Label)this.Master.FindControl("lblTitle")).Text = "Collection Details Information";

                this.txtDateFrom.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string date1 = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + ")";
            string title = "Collection Detail Information ";

            DataTable dt = (DataTable)Session["tblsupbill"];

            var list = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptCollDetailsinfo>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_22_Sal.RptCollDetailsInfo", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("companyname", comnam));
            rpt.SetParameters(new ReportParameter("txtDate", date1));
            rpt.SetParameters(new ReportParameter("rptTitle", title));

            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptsl = new RealERPRPT.R_22_Sal.RptCollDetailsInfo();

            //DataTable dt = (DataTable)Session["tblsupbill"];
            //TextObject txtCompany = rptsl.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtRptHead = rptsl.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtRptHead.Text = "Collection Detail Information ";
            //TextObject txtdate = rptsl.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + ")";

            //TextObject txtuserinfo = rptsl.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsl.SetDataSource(dt);

            //Session["Report1"] = rptsl;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        protected void btnok_Click(object sender, EventArgs e)
        {

            this.ShowCollectionInfo();
        }
        private void ShowCollectionInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "SHOWDUESCOLLECTION", frmdate, todate, "", "", "", "", "", "", "");

            Session["tblsupbill"] = this.HiddenSameData(ds2.Tables[0]);


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
            DataTable dt = (DataTable)Session["tblsupbill"];
            this.gvCollection.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
            this.gvCollection.DataSource = dt;
            this.gvCollection.DataBind();
            this.FooterCalCulation();


        }
        private void FooterCalCulation()
        {
            DataTable dt = (DataTable)Session["tblsupbill"];

            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvCollection.FooterRow.FindControl("lblFPBooking")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pbookam)", "")) ?
                       0 : dt.Compute("sum(pbookam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCollection.FooterRow.FindControl("lblFInstall")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pinsam)", "")) ?
                       0 : dt.Compute("sum(pinsam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCollection.FooterRow.FindControl("lblFcbookam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cbookam)", "")) ?
                       0 : dt.Compute("sum(cbookam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCollection.FooterRow.FindControl("lblFcinsam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cinsam)", "")) ?
                       0 : dt.Compute("sum(cinsam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCollection.FooterRow.FindControl("lblFAdvCollam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(advcoll)", "")) ?
                       0 : dt.Compute("sum(advcoll)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCollection.FooterRow.FindControl("lblFTotalColl")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tocollam)", "")) ?
                       0 : dt.Compute("sum(tocollam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCollection.FooterRow.FindControl("lblFReAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(repchqamt)", "")) ?
                       0 : dt.Compute("sum(repchqamt)", ""))).ToString("#,##0;(#,##0); ");



        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvCollection_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvCollection.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvCollection_RowCreated(object sender, GridViewRowEventArgs e)
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
                cell04.Text = "Previous";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 2;
                cell04.Font.Bold = true;
                TableCell cell05 = new TableCell();
                cell05.Text = "Current";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 2;
                cell05.Font.Bold = true;


                TableCell cell06 = new TableCell();
                cell06.Text = "";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 1;

                TableCell cell07 = new TableCell();
                cell07.Text = "";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.ColumnSpan = 1;

                TableCell cell08 = new TableCell();
                cell08.Text = "";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.ColumnSpan = 1;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvrow.Cells.Add(cell06);
                gvrow.Cells.Add(cell07);
                gvrow.Cells.Add(cell08);


                gvCollection.Controls[0].Controls.AddAt(0, gvrow);
            }
        }

    }
}