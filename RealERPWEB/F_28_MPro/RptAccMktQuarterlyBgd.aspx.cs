using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;

namespace RealERPWEB.F_28_MPro
{
    public partial class RptAccMktQuarterlyBgd : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess accData = new ProcessAccess();
        string msg = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                if (dr1.Length==0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetYear();
                this.lbtnOk_Click(null, null);
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetYear()
        {
            string comcod = this.GetCompCode();
            string yr = System.DateTime.Today.ToString("yyyy");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETYEAR", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlYear.DataTextField = "year1";
            this.ddlYear.DataValueField = "year1";
            this.ddlYear.DataSource = ds1.Tables[0];
            this.ddlYear.DataBind();
            this.ddlYear.SelectedValue = yr;
            ds1.Dispose();

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //
            //

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.GetBudgetInfo();

        }
        private void GetBudgetInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string year = this.ddlYear.SelectedValue.ToString();

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_MKT_PROCUREMENT", "RPT_MKT_QUARTERLY_BUDGET", year, "", "", "", "");
            if (ds2==null)
                return;

            Session["AccTbl02"] = ds2.Tables[0];
            this.Data_Bind();
        }

        protected void Data_Bind()
        {
            DataTable tblt03 = (DataTable)Session["AccTbl02"];
            this.gvQuartBgd.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvQuartBgd.DataSource = tblt03;
            this.gvQuartBgd.DataBind();
            this.FooterCalculation();
        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["AccTbl02"];
            if(dt.Rows.Count>0)
            {
                ((Label)this.gvQuartBgd.FooterRow.FindControl("gvlblFATLQ1")).Text = Convert.ToDouble(Convert.IsDBNull(dt.Compute("Sum(atlq1)","")) ?
                    0.00 : dt.Compute("Sum(atlq1)","")).ToString("#,##0;(#,##0); ");
                ((Label)this.gvQuartBgd.FooterRow.FindControl("gvlblFBTLQ1")).Text = Convert.ToDouble(Convert.IsDBNull(dt.Compute("Sum(btlq1)", "")) ?
                    0.00 : dt.Compute("Sum(btlq1)", "")).ToString("#,##0;(#,##0); ");
                ((Label)this.gvQuartBgd.FooterRow.FindControl("gvlblFTTLQ1")).Text = Convert.ToDouble(Convert.IsDBNull(dt.Compute("Sum(ttlq1)", "")) ?
                    0.00 : dt.Compute("Sum(ttlq1)", "")).ToString("#,##0;(#,##0); ");
                ((Label)this.gvQuartBgd.FooterRow.FindControl("gvlblFQ1")).Text = Convert.ToDouble(Convert.IsDBNull(dt.Compute("Sum(totq1)", "")) ?
                    0.00 : dt.Compute("Sum(totq1)", "")).ToString("#,##0;(#,##0); ");
                ((Label)this.gvQuartBgd.FooterRow.FindControl("gvlblFATLQ2")).Text = Convert.ToDouble(Convert.IsDBNull(dt.Compute("Sum(atlq2)", "")) ?
                    0.00 : dt.Compute("Sum(atlq2)", "")).ToString("#,##0;(#,##0); ");
                ((Label)this.gvQuartBgd.FooterRow.FindControl("gvlblFBTLQ2")).Text = Convert.ToDouble(Convert.IsDBNull(dt.Compute("Sum(btlq2)", "")) ?
                    0.00 : dt.Compute("Sum(btlq2)", "")).ToString("#,##0;(#,##0); ");
                ((Label)this.gvQuartBgd.FooterRow.FindControl("gvlblFTTLQ2")).Text = Convert.ToDouble(Convert.IsDBNull(dt.Compute("Sum(ttlq2)", "")) ?
                    0.00 : dt.Compute("Sum(ttlq2)", "")).ToString("#,##0;(#,##0); ");
                ((Label)this.gvQuartBgd.FooterRow.FindControl("gvlblFQ2")).Text = Convert.ToDouble(Convert.IsDBNull(dt.Compute("Sum(totq2)", "")) ?
                    0.00 : dt.Compute("Sum(totq2)", "")).ToString("#,##0;(#,##0); ");
                ((Label)this.gvQuartBgd.FooterRow.FindControl("gvlblFATLQ3")).Text = Convert.ToDouble(Convert.IsDBNull(dt.Compute("Sum(atlq3)", "")) ?
                    0.00 : dt.Compute("Sum(atlq3)", "")).ToString("#,##0;(#,##0); ");
                ((Label)this.gvQuartBgd.FooterRow.FindControl("gvlblFBTLQ3")).Text = Convert.ToDouble(Convert.IsDBNull(dt.Compute("Sum(btlq3)", "")) ?
                    0.00 : dt.Compute("Sum(btlq3)", "")).ToString("#,##0;(#,##0); ");
                ((Label)this.gvQuartBgd.FooterRow.FindControl("gvlblFTTLQ3")).Text = Convert.ToDouble(Convert.IsDBNull(dt.Compute("Sum(ttlq3)", "")) ?
                    0.00 : dt.Compute("Sum(ttlq3)", "")).ToString("#,##0;(#,##0); ");
                ((Label)this.gvQuartBgd.FooterRow.FindControl("gvlblFQ3")).Text = Convert.ToDouble(Convert.IsDBNull(dt.Compute("Sum(totq3)", "")) ?
                   0.00 : dt.Compute("Sum(totq3)", "")).ToString("#,##0;(#,##0); ");
                ((Label)this.gvQuartBgd.FooterRow.FindControl("gvlblFATLQ4")).Text = Convert.ToDouble(Convert.IsDBNull(dt.Compute("Sum(atlq4)", "")) ?
                    0.00 : dt.Compute("Sum(atlq4)", "")).ToString("#,##0;(#,##0); ");
                ((Label)this.gvQuartBgd.FooterRow.FindControl("gvlblFBTLQ4")).Text = Convert.ToDouble(Convert.IsDBNull(dt.Compute("Sum(btlq4)", "")) ?
                    0.00 : dt.Compute("Sum(btlq4)", "")).ToString("#,##0;(#,##0); ");
                ((Label)this.gvQuartBgd.FooterRow.FindControl("gvlblFTTLQ4")).Text = Convert.ToDouble(Convert.IsDBNull(dt.Compute("Sum(ttlq4)", "")) ?
                    0.00 : dt.Compute("Sum(ttlq4)", "")).ToString("#,##0;(#,##0); ");
                ((Label)this.gvQuartBgd.FooterRow.FindControl("gvlblFQ4")).Text = Convert.ToDouble(Convert.IsDBNull(dt.Compute("Sum(totq4)", "")) ?
                   0.00 : dt.Compute("Sum(totq4)", "")).ToString("#,##0;(#,##0); ");
            }
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void dgv3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvQuartBgd.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvQuartBgd_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //TableCell cell01 = new TableCell();
                //cell01.Text = "SL.";
                //cell01.HorizontalAlign = HorizontalAlign.Center;
                //cell01.RowSpan = 2;
                //gvrow.Cells.Add(cell01);

                //TableCell cell012 = new TableCell();
                //cell012.Text = "Project";
                //cell012.HorizontalAlign = HorizontalAlign.Center;
                //cell012.RowSpan = 2;
                //gvrow.Cells.Add(cell012);

                TableCell cell02 = new TableCell();
                cell02.Text = "Q1";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 6;
                gvrow.Cells.Add(cell02);

                TableCell cell03 = new TableCell();
                cell03.Text = "Q2";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 4;
                gvrow.Cells.Add(cell03);

                TableCell cell04 = new TableCell();
                cell04.Text = "Q3";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 4;
                gvrow.Cells.Add(cell04);

                TableCell cell05 = new TableCell();
                cell05.Text = "Q4";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 4;
                gvrow.Cells.Add(cell05);

                this.gvQuartBgd.Controls[0].Controls.AddAt(0, gvrow);
            }
        }

        protected void gvQuartBgd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    e.Row.Cells[0].Visible = false;
            //    e.Row.Cells[1].Visible = false;

            //}
        }
    }
}