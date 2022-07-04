using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_02_Fea
{
    public partial class RptProductCost : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");


                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");




            }
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            Session.Remove("tblprocostana");
            string comcod = this.GetCompCode();

            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
          

            DataSet ds3;

            ds3 = purData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY_03", "PROJECTCOSTINGTOPSHEET", fromdate, "", "", "", "", "", "", "", "");

            if (ds3 == null)
            {
                this.gvProCostAna.DataSource = null;
                this.gvProCostAna.DataBind();
                return;
            }
            DataTable dt = ds3.Tables[0];

            Session["tblprocostana"] = dt;

            this.Data_Bind();

        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblprocostana"];

            if (dt.Rows.Count > 0)
            {
                this.gvProCostAna.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());

                
                this.gvProCostAna.DataSource = dt;
                this.gvProCostAna.DataBind();

                this.FooterCalculation();


            }
            else
            {
                this.gvProCostAna.DataSource = null;
                this.gvProCostAna.DataBind();
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }







        }

        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tblprocostana"];

            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvProCostAna.FooterRow.FindControl("lFpurcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(purvalue)", "")) ? 0.00 : dt.Compute("sum(purvalue)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvProCostAna.FooterRow.FindControl("lFpurincentive")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(purinstive)", "")) ? 0.00 : dt.Compute("sum(purinstive)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProCostAna.FooterRow.FindControl("lFothcost")).Text = " - ";
            ((Label)this.gvProCostAna.FooterRow.FindControl("lFtpurcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tpurcost)", "")) ? 0.00 : dt.Compute("sum(tpurcost)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProCostAna.FooterRow.FindControl("lFfixedcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(fxtcost)", "")) ? 0.00 : dt.Compute("sum(fxtcost)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProCostAna.FooterRow.FindControl("lFvarcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othmktexp)", "")) ? 0.00 : dt.Compute("sum(othmktexp)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProCostAna.FooterRow.FindControl("lFtotalcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tcost)", "")) ? 0.00 : dt.Compute("sum(tcost)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProCostAna.FooterRow.FindControl("lFcommitedsaleval")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(commitedval)", "")) ? 0.00 : dt.Compute("sum(commitedval)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProCostAna.FooterRow.FindControl("lFmarloss")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(margin)", "")) ? 0.00 : dt.Compute("sum(margin)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProCostAna.FooterRow.FindControl("lFactcostintill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(accosttill)", "")) ? 0.00 : dt.Compute("sum(accosttill)", ""))).ToString("#,##0;(#,##0); ");

            Session["Report1"] = gvProCostAna;

            Session["ReportName"] = "Product Cost Analysis";
            ((HyperLink)this.gvProCostAna.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RDLCViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }

        protected void gvProCostAna_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvProCostAna_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvProCostAna.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        //protected void gvProCostAna_RowCreated(object sender, GridViewRowEventArgs e)
        //{
        //    GridViewRow gvr = e.Row;

        //    if (gvr.RowType == DataControlRowType.Header)
        //    {
        //        GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

              

        //        TableCell cell02 = new TableCell();
        //        cell02.Text = "";
        //        cell02.HorizontalAlign = HorizontalAlign.Center;
        //        cell02.RowSpan = 2;
        //        gvrow.Cells.Add(cell02);

        //        TableCell cell03 = new TableCell();
        //        cell03.Text = "";
        //        cell03.HorizontalAlign = HorizontalAlign.Center;
        //        cell03.RowSpan = 2;
        //        gvrow.Cells.Add(cell03);

        //        TableCell cell04 = new TableCell();
        //        cell04.Text = "";
        //        cell04.HorizontalAlign = HorizontalAlign.Center;
        //        cell04.RowSpan = 2;
        //        gvrow.Cells.Add(cell04);

        //        TableCell cell05 = new TableCell();
        //        cell05.Text = "";
        //        cell05.HorizontalAlign = HorizontalAlign.Center;
        //        cell05.RowSpan = 2;
        //        gvrow.Cells.Add(cell05);

        //        TableCell cell06 = new TableCell();
        //        cell06.Text = "Purchase Cost";
        //        cell06.HorizontalAlign = HorizontalAlign.Center;
        //        cell06.ColumnSpan = 4;
        //        cell06.RowSpan = 2;

             
        //        gvrow.Cells.Add(cell06);

        //        TableCell cell07 = new TableCell();
        //        cell07.Text = "Other estimated Cost";
        //        cell07.HorizontalAlign = HorizontalAlign.Center;
        //        cell07.ColumnSpan = 2;
        //        cell07.RowSpan = 2;
        //        gvrow.Cells.Add(cell07);

        //        TableCell cell08 = new TableCell();
        //        cell08.Text = "";
        //        cell08.HorizontalAlign = HorizontalAlign.Center;
        //        cell08.RowSpan = 2;
        //        gvrow.Cells.Add(cell08);

        //        TableCell cell09 = new TableCell();
        //        cell09.Text = "";
        //        cell09.HorizontalAlign = HorizontalAlign.Center;
        //        cell09.RowSpan = 2;
        //        gvrow.Cells.Add(cell09);

        //        TableCell cell10 = new TableCell();
        //        cell10.Text = "";
        //        cell10.HorizontalAlign = HorizontalAlign.Center;
        //        cell10.RowSpan = 2;
        //        gvrow.Cells.Add(cell10);

        //        TableCell cell11 = new TableCell();
        //        cell11.Text = "";
        //        cell11.HorizontalAlign = HorizontalAlign.Center;
        //        cell11.RowSpan = 2;
        //        gvrow.Cells.Add(cell11);

        //        TableCell cell12 = new TableCell();
        //        cell12.Text = "Agreement Validity";
        //        cell12.HorizontalAlign = HorizontalAlign.Center;
        //        cell12.ColumnSpan = 4;
        //        cell12.RowSpan = 2;
        //        gvrow.Cells.Add(cell12);

        //        this.gvProCostAna.Controls[0].Controls.AddAt(0, gvrow);
        //    }




        //}
    }
}