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
    public partial class RptPaymetDueAllPrj : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime currdate = Convert.ToDateTime(System.DateTime.Today.ToString("dd-MMM-yyyy"));
                this.txtDatefrom.Text = Convert.ToDateTime("01" + currdate.ToString("dd-MMM-yyyy").Substring(3)).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = Convert.ToDateTime(this.txtDatefrom.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Payment Dues -All Project";
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {



            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);



        }



        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            //string frmdate = this.txtDatefrom.Text;
            string todate = this.txtDateto.Text;
            string details = this.ChboxDetails.Checked ? "Details" : "";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "PAYMENTDUESALLPRJ", todate, details, "", "", "", "", "", "");
            Session["tblpayment"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string actcode = dt1.Rows[0]["actcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode)
                    dt1.Rows[j]["actdesc"] = "";
                actcode = dt1.Rows[j]["actcode"].ToString();

            }




            return dt1;

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
            DataTable dt = ((DataTable)Session["tblpayment"]).Copy();
            if (dt.Rows.Count == 0)
                return;
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("rescode like '%000'");
            dt = dv.ToTable();

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
                Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(paytdue)", "")) ?
                    0.00 : dt.Compute("Sum(paytdue)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }

        protected void gvAcc_RowCreated(object sender, GridViewRowEventArgs e)
        {
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

                TableCell cell01 = new TableCell();
                cell01.Text = "Budget";
                cell01.HorizontalAlign = HorizontalAlign.Center;
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
                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvAcc.Controls[0].Controls.AddAt(0, gvrow);
            }

        }
        protected void gvAcc_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;

            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink description = (HyperLink)e.Row.FindControl("lblAccDesc");
                Label lblbgdqty = (Label)e.Row.FindControl("lblbgdqty");
                Label lblbgdrat = (Label)e.Row.FindControl("lblbgdrat");
                Label lblbgdam = (Label)e.Row.FindControl("lblbgdam");
                Label lblpurqty = (Label)e.Row.FindControl("lblpurqty");
                Label lblpurrat = (Label)e.Row.FindControl("lblpurrat");
                Label lblpuramt = (Label)e.Row.FindControl("lblpuramt");
                Label lblpayamt = (Label)e.Row.FindControl("lblpayamt");
                Label lblpaydue = (Label)e.Row.FindControl("lblpaydue");
                Label lblpayduet = (Label)e.Row.FindControl("lblpayduet");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString().Trim();
                string rescode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (rescode == "000000000000")
                {
                    description.Attributes["style"] = "color:maroon; font-weight:bold";
                    lblbgdqty.Attributes["style"] = "color:maroon; font-weight:bold";
                    lblbgdrat.Attributes["style"] = "color:maroon; font-weight:bold";
                    lblbgdam.Attributes["style"] = "color:maroon; font-weight:bold";
                    lblpurqty.Attributes["style"] = "color:maroon; font-weight:bold";
                    lblpurrat.Attributes["style"] = "color:maroon; font-weight:bold";
                    lblpuramt.Attributes["style"] = "color:maroon; font-weight:bold";
                    lblpayamt.Attributes["style"] = "color:maroon; font-weight:bold";
                    lblpaydue.Attributes["style"] = "color:maroon; font-weight:bold";
                    lblpayduet.Attributes["style"] = "color:maroon; font-weight:bold";




                }


                if (rescode == "000000000000")
                {
                    description.NavigateUrl = "~/F_14_Pro/RptPaymenDuesIndPrj.aspx?prjcode=" + code;
                }

                else
                {
                    description.NavigateUrl = "~/F_14_Pro/RptLandOwnerPaySch.aspx?Type=Report&actcode=" + code + "&usircode=" + rescode; ;

                }




            }















        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {



        }


    }
}