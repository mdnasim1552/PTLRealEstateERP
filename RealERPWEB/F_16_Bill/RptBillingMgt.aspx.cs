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
    public partial class RptBillingMgt : System.Web.UI.Page
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Rate Variance";

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmDate.Text = (this.Request.QueryString["Date1"].ToString().Length > 0) ? this.Request.QueryString["Date1"] : ("01" + date.Substring(2));
                this.txttoDate.Text = (this.Request.QueryString["Date2"].ToString().Length > 0) ? this.Request.QueryString["Date2"] : date;

                this.rbtnList1.SelectedIndex = 1;
            }
            if (this.ddlProjectName.Items.Count == 0)
            {
                this.GetProjectName();

            }




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
            int index = Convert.ToInt32(this.rbtnList1.SelectedIndex.ToString());
            switch (index)
            {
                case 0:
                    this.ShowBillQtyBasis();
                    break;
                case 1:
                    this.ShowBillAmtBasis();
                    break;
            }
        }

        private void ShowBillQtyBasis()
        {

            Session.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "RPTBILLENTRYQTYBASIS", PactCode, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSubBill.DataSource = null;
                this.gvSubBill.DataBind();
                return;
            }
            Session["tblData"] = ds1.Tables[0];
            this.LoadGrid();

        }

        private void ShowBillAmtBasis()
        {

            Session.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "RPTBILLENTRYAMTBASIS", PactCode, frmdate, todate, "", "", "", "", "", "");
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
            this.gvSubBill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSubBill.DataSource = dt;
            this.gvSubBill.DataBind();

            if (Convert.ToInt32(this.rbtnList1.SelectedIndex.ToString()) == 1)
            {
                this.FooterCalculation();
            }





        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblData"];
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFrcvQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rcvqty)", "")) ?
                               0 : dt.Compute("sum(rcvqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFSubQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(subqty)", "")) ?
                            0 : dt.Compute("sum(subqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFNyetSQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(nyetsqty)", "")) ?
                            0 : dt.Compute("sum(nyetsqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFAdvQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(advqty)", "")) ?
                            0 : dt.Compute("sum(advqty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFNettoQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(nettoqty)", "")) ?
                               0 : dt.Compute("sum(nettoqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFpBgdQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pbgdqty)", "")) ?
                            0 : dt.Compute("sum(pbgdqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFCBgdQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cbgdqty)", "")) ?
                            0 : dt.Compute("sum(cbgdqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFToExQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toexqty)", "")) ?
                            0 : dt.Compute("sum(toexqty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFgrsQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(grsqty)", "")) ?
                               0 : dt.Compute("sum(grsqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFdecQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(decqty)", "")) ?
                            0 : dt.Compute("sum(decqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFnetovQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netqty)", "")) ?
                            0 : dt.Compute("sum(netqty)", ""))).ToString("#,##0;(#,##0); ");


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
            string basis = (Convert.ToInt32(this.rbtnList1.SelectedIndex.ToString()) == 0) ? "Performance Evaluation Report-Qty Basis" : "Performance Evaluation Report-Amount Basis";
            DataTable dt = (DataTable)Session["tblData"];
            ReportDocument rptsale = new RealERPRPT.R_16_Bill.rptPerformanceEvaluation();
            TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptdBasis = rptsale.ReportDefinition.ReportObjects["Basis"] as TextObject;
            rptdBasis.Text = basis;
            TextObject rptpactdesc = rptsale.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            rptpactdesc.Text = "Project Name: " + this.ddlProjectName.SelectedItem.Text;
            TextObject rptDate = rptsale.ReportDefinition.ReportObjects["date"] as TextObject;
            rptDate.Text = "(From " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy") + ")";
            TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptsale.SetDataSource(dt);
            Session["Report1"] = rptsale;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


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



    }
}

