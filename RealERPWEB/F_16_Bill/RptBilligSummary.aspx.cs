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
namespace RealERPWEB.F_16_Bill
{
    public partial class RptBilligSummary : System.Web.UI.Page
    {
        ProcessAccess BgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)

            {


                ((Label)this.Master.FindControl("lblTitle")).Text = "Budgeted Cost, Order , Order Balance";
                this.Master.Page.Title = "Budgeted Cost, Order , Order Balance";

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string date1 = this.Request.QueryString["Date1"];
                string date2 = this.Request.QueryString["Date2"];
                this.txtfrmDate.Text = date1.Length > 0 ? date1 : "01" + date.Substring(2);
                this.txttoDate.Text = date2.Length > 0 ? date2 : date;
            }
        }


        private string GetCompCode()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
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
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "RPTBUDORDERBILLORBAL", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSubBill.DataSource = null;
                this.gvSubBill.DataBind();
                return;
            }
            Session["tblData"] = ds1.Tables[0];
            this.LoadGrid();
        }





        private void LoadGrid()
        {

            DataTable dt = (DataTable)Session["tblData"];
            this.gvSubBill.DataSource = dt;
            this.gvSubBill.DataBind();
            this.FooterCalculation();

        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblData"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFbgdcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdam)", "")) ?
                               0 : dt.Compute("sum(bgdam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFordamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(orderam)", "")) ?
                           0 : dt.Compute("sum(orderam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFPreviousBill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(preordam)", "")) ?
                           0 : dt.Compute("sum(preordam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFCurrentBill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curordam)", "")) ?
                           0 : dt.Compute("sum(curordam)", ""))).ToString("#,##0;(#,##0); ");
            ((HyperLink)this.gvSubBill.FooterRow.FindControl("hlnkgvFTotalBill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toordam)", "")) ?
                           0 : dt.Compute("sum(toordam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFOrderbal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordbal)", "")) ?
                           0 : dt.Compute("sum(ordbal)", ""))).ToString("#,##0;(#,##0); ");

            ((HyperLink)this.gvSubBill.FooterRow.FindControl("hlnkgvFTotalBill")).NavigateUrl = "~/F_41_GAcc/RptProBillStatus.aspx?Type=Billstatus&prjcode=";


        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string basis = (Convert.ToInt32(this.rbtnList1.SelectedIndex.ToString()) == 0) ? "Performance Evaluation Report-Qty Basis" : "Performance Evaluation Report-Amount Basis";
            //DataTable dt = (DataTable)Session["tblData"];
            //ReportDocument rptsale = new RealERPRPT.R_16_Bill.rptPerformanceEvaluation();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptdBasis = rptsale.ReportDefinition.ReportObjects["Basis"] as TextObject;
            //rptdBasis.Text =basis;
            //TextObject rptpactdesc = rptsale.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //rptpactdesc.Text = "Project Name: " +this.ddlProjectName.SelectedItem.Text;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptDate.Text = "(From " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy")+" To "+Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy")+")";
            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //      ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";


        }









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
                cell02.Text = "Project Description";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.RowSpan = 2;
                gvrow.Cells.Add(cell02);

                TableCell cell03 = new TableCell();
                cell03.Text = "Quotation Amount";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.RowSpan = 2;
                gvrow.Cells.Add(cell03);


                TableCell cell04 = new TableCell();
                cell04.Text = "Budgeted Cost";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.RowSpan = 2;
                gvrow.Cells.Add(cell04);


                TableCell cell05 = new TableCell();
                cell05.Text = "Bill Completed";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.Attributes["style"] = "font-weight:bold;";
                cell05.ColumnSpan = 3;
                gvrow.Cells.Add(cell05);





                TableCell cell08 = new TableCell();
                cell08.Text = "Work In Hand";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.RowSpan = 2;
                gvrow.Cells.Add(cell08);

                gvSubBill.Controls[0].Controls.AddAt(0, gvrow);
            }


        }
        protected void gvSubBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[7].Visible = false;

            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkgcpactDesc");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                if (code == "")
                {
                    return;
                }

                else
                {
                    // hlink1.HLgvacTRAmt = "~/F_32_Mis/ProjTrialBalanc.aspx?Type=PrjTrailBal";
                    //hlink1.Style.Add("color", "blue");
                    string frmdate = this.txtfrmDate.Text;
                    string todate = this.txttoDate.Text;

                    hlink1.NavigateUrl = "~/F_16_Bill/RptBillStatus.aspx?Type=ProStatus&prjcode=" + code + "&Date1=" + frmdate + "&Date2=" + todate;//?type=ConstRpt&prjcode="+code;
                    hlink1.Style.Add("color", "blue");




                }
            }

        }
    }
}

