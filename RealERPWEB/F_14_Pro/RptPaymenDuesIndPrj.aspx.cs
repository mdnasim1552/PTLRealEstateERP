using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
namespace RealERPWEB.F_14_Pro
{
    public partial class RptPaymenDuesIndPrj : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime currdate = Convert.ToDateTime(System.DateTime.Today.ToString("dd-MMM-yyyy"));
                this.txtDatefrom.Text = Convert.ToDateTime("01" + currdate.ToString("dd-MMM-yyyy").Substring(3)).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = Convert.ToDateTime(this.txtDatefrom.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Payment Dues -Project";
                this.GetAccCode();
            }

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetAccCode()
        {
            string comcod = this.GetComeCode();
            //string filter = this.txtAccSearch.Text.Trim() + "%";
            string filter = "16%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEAD01", filter, "", "", "",
                "", "", "", "", "");
            this.ddlConAccHead.DataSource = ds1.Tables[0];
            this.ddlConAccHead.DataTextField = "actdesc1";
            this.ddlConAccHead.DataValueField = "actcode";
            this.ddlConAccHead.DataBind();
            this.ddlConAccHead.SelectedValue = this.Request.QueryString["prjcode"].ToString();

        }

        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            //string frmdate = this.txtDatefrom.Text;
            string todate = this.txtDateto.Text;
            string prjcode = this.ddlConAccHead.SelectedValue;
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "PAYMENTDUES", prjcode, todate, "", "", "", "", "", "", "");
            Session["tblpayment"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblpayment"];
            this.gvAcc.DataSource = dt;
            this.gvAcc.DataBind();
            this.FooterCalculation();
        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblpayment"];

            ((Label)this.gvAcc.FooterRow.FindControl("Lnkfqty")).Text =
                Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdqty)", "")) ?
                    0.00 : dt.Compute("Sum(bgdqty)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvAcc.FooterRow.FindControl("Lnkftmat")).Text =
                Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdam)", "")) ?
                    0.00 : dt.Compute("Sum(bgdam)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvAcc.FooterRow.FindControl("Lnkfactqty")).Text =
                Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(purqty)", "")) ?
                    0.00 : dt.Compute("Sum(purqty)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvAcc.FooterRow.FindControl("Lnkfacttamt")).Text =
                Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(puramt)", "")) ?
                    0.00 : dt.Compute("Sum(puramt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvAcc.FooterRow.FindControl("Lnkfpayp")).Text =
                Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(payamt)", "")) ?
                    0.00 : dt.Compute("Sum(payamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvAcc.FooterRow.FindControl("Lnkfpaydue")).Text =
                Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(paydue)", "")) ?
                    0.00 : dt.Compute("Sum(paydue)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvAcc.FooterRow.FindControl("Lnkfpaytmt")).Text =
                Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(payduet)", "")) ?
                    0.00 : dt.Compute("Sum(payduet)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }
        protected void gvAcc_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string actcode = this.ddlConAccHead.SelectedValue;
                string rescode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString().Trim();
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lblAccDesc");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lblpayduet");
                hlink1.NavigateUrl = "~/F_14_Pro/RptLandOwnerPaySch.aspx?Type=Report&actcode=" + actcode + "&usircode=" + rescode;
                hlink2.NavigateUrl = "~/F_51_LBgd/AccLandPaySlip.aspx?Type=Report&prjcode=" + actcode + "&sircode=" + rescode;
            }



            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell01a = new TableCell();
                cell01a.Text = "SL";
                cell01a.HorizontalAlign = HorizontalAlign.Center;
                cell01a.RowSpan = 2;

                TableCell cell01b = new TableCell();
                cell01b.Text = "Description";
                cell01b.HorizontalAlign = HorizontalAlign.Center;
                cell01b.RowSpan = 2;

                TableCell cell01c = new TableCell();
                cell01c.Text = "Unit";
                cell01c.HorizontalAlign = HorizontalAlign.Center;
                cell01c.RowSpan = 2;

                TableCell cell01 = new TableCell();
                cell01.Text = "Budget";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.Attributes.Add("style", "font-weight:bold");
                cell01.ColumnSpan = 3;

                TableCell cell02 = new TableCell();
                cell02.Text = "Actual Purchase";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 3;

                TableCell cell03 = new TableCell();
                cell03.Text = "Payment";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 3;



                gvrow.Cells.Add(cell01a);
                gvrow.Cells.Add(cell01b);
                gvrow.Cells.Add(cell01c);
                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);

                gvAcc.Controls[0].Controls.AddAt(0, gvrow);


                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;

                }
            }
        }
    }
}
