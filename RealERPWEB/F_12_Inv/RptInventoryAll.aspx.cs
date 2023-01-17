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
namespace RealERPWEB.F_12_Inv
{
    public partial class RptInventoryAll : System.Web.UI.Page
    {
        ProcessAccess ImpleData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Inventory Report(All in One)";
                string date1 = this.Request.QueryString["Date1"];
                string date2 = this.Request.QueryString["Date2"];
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = date1.Length > 0 ? date1 : Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttoDate.Text = date2.Length > 0 ? date2 : System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.GetProjectName();

            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            // ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
        }
        private string GetCompCod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
        }
        //protected void GetProjectName()
        //{

        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = GetCompCod();
        //    //string date = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
        //    string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
        //    DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETPURPROJECTNAMEALL", serch1, "", "", "", "", "", "", "", "");
        //    if (ds1 == null)
        //        return;
        //    this.ddlProjectName.DataTextField = "pactdesc";
        //    this.ddlProjectName.DataValueField = "pactcode";
        //    this.ddlProjectName.DataSource = ds1.Tables[0];
        //    this.ddlProjectName.DataBind();

        //}


        public void LoadData()
        {
            string rpt = this.ddlReport.SelectedValue;
            switch (rpt)
            {
                case "amtbasis":
                    this.InventoryAmtBasis();

                    break;
                case "qtybasis":
                    this.InventoryAmtBasis();

                    break;
                case "amtbasisp":
                    this.InventoryAmtBasisPeriodic();
                    break;
                case "qtybasisp":
                    this.InventoryAmtBasisPeriodic();
                    break;

                default:
                    break;
            }
        }

        private void InventoryAmtBasisPeriodic()
        {
            string comcod = this.GetCompCod();

            string frmdate = this.txtFDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            // string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "RPTAMTBASISRESPERIODIC", frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvAmtPeriodic.DataSource = null;
                this.gvAmtPeriodic.DataBind();
                this.dvQtyBasisPeriodic.DataSource = null;
                this.dvQtyBasisPeriodic.DataBind();
                return;

            }

            // DataTable dt=this.HiddenSamaData(ds1.Tables[0])

            Session["amtbasisp"] = ds1.Tables[0];
            Session["qtybasisp"] = ds1.Tables[1];

            this.Data_Bind();
        }
        public void InventoryAmtBasis()
        {
            string comcod = this.GetCompCod();

            string frmdate = this.txtFDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            // string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "RPTSTOCKINVPRO", "%", frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMatStock.DataSource = null;
                this.gvMatStock.DataBind();
                this.gvQtyBasis.DataSource = null;
                this.gvQtyBasis.DataBind();
                return;

            }

            // DataTable dt=this.HiddenSamaData(ds1.Tables[0])

            Session["amtbasis"] = ds1.Tables[0];
            Session["qtybasis"] = ds1.Tables[1];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["amtbasis"];
            DataTable dt1 = (DataTable)Session["qtybasis"];
            DataTable dt2 = (DataTable)Session["amtbasisp"];
            DataTable dt3 = (DataTable)Session["qtybasisp"];
            string rpt = this.ddlReport.SelectedValue;
            switch (rpt)
            {
                case "amtbasis":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvMatStock.DataSource = dt;
                    this.gvMatStock.DataBind();
                    this.FooterCal();
                    break;
                case "qtybasis":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.gvQtyBasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvQtyBasis.DataSource = dt1;
                    this.gvQtyBasis.DataBind();
                    break;
                case "amtbasisp":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.gvAmtPeriodic.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvAmtPeriodic.DataSource = dt2;
                    this.gvAmtPeriodic.DataBind();
                    this.FooterCalperiodic();
                    break;
                case "qtybasisp":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.dvQtyBasisPeriodic.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.dvQtyBasisPeriodic.DataSource = dt3;
                    this.dvQtyBasisPeriodic.DataBind();
                    ///this.FooterCalperiodicqty();
                    break;
                default:
                    break;
            }



        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.LoadData();

        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["amtbasis"];

            ((Label)this.gvMatStock.FooterRow.FindControl("lblrcvf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(rcvamt)", "")) ?
                0.00 : dt.Compute("Sum(rcvamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lbltinf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trninamt)", "")) ?
                0.00 : dt.Compute("Sum(trninamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lbltoutf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnoutamt)", "")) ?
               0.00 : dt.Compute("Sum(trnoutamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lbllstf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(lsamt)", "")) ?
                0.00 : dt.Compute("Sum(lsamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lblnetrcvf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(netrcvamt)", "")) ?
               0.00 : dt.Compute("Sum(netrcvamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lblisuf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(issueamt)", "")) ?
                0.00 : dt.Compute("Sum(issueamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lblbgdconf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdconamt)", "")) ?
               0.00 : dt.Compute("Sum(bgdconamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lblactstktf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(actstock)", "")) ?
                0.00 : dt.Compute("Sum(actstock)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lblbgdstkf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdstock)", "")) ?
               0.00 : dt.Compute("Sum(bgdstock)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lblvarf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(varamt)", "")) ?
                0.00 : dt.Compute("Sum(varamt)", ""))).ToString("#,##0;(#,##0); ");
        }
        private void FooterCalperiodic()
        {
            DataTable dt = (DataTable)Session["amtbasisp"];

            ((Label)this.gvAmtPeriodic.FooterRow.FindControl("lblopnf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opamt)", "")) ?
                0.00 : dt.Compute("Sum(opamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvAmtPeriodic.FooterRow.FindControl("lblrcvfp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(rcvamt)", "")) ?
               0.00 : dt.Compute("Sum(rcvamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvAmtPeriodic.FooterRow.FindControl("lbltinfp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trninamt)", "")) ?
                0.00 : dt.Compute("Sum(trninamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvAmtPeriodic.FooterRow.FindControl("lbltoutfp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnoutamt)", "")) ?
               0.00 : dt.Compute("Sum(trnoutamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvAmtPeriodic.FooterRow.FindControl("lbllstfp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(lsamt)", "")) ?
                0.00 : dt.Compute("Sum(lsamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvAmtPeriodic.FooterRow.FindControl("lblnetrcvfp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(netrcvamt)", "")) ?
               0.00 : dt.Compute("Sum(netrcvamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvAmtPeriodic.FooterRow.FindControl("lblisufp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(issueamt)", "")) ?
                0.00 : dt.Compute("Sum(issueamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvAmtPeriodic.FooterRow.FindControl("lblactstktfp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(actstock)", "")) ?
                0.00 : dt.Compute("Sum(actstock)", ""))).ToString("#,##0;(#,##0); ");


        }

        //private void FooterCalperiodicqty()
        //{
        //    DataTable dt = (DataTable)Session["qtybasisp"];

        //    ((Label)this.dvQtyBasisPeriodic.FooterRow.FindControl("lblopnfq")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opqty)", "")) ?
        //        0.00 : dt.Compute("Sum(opqty)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.dvQtyBasisPeriodic.FooterRow.FindControl("lblrcvfq")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(rcvqty)", "")) ?
        //       0.00 : dt.Compute("Sum(rcvqty)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.dvQtyBasisPeriodic.FooterRow.FindControl("lbltinfq")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trninqty)", "")) ?
        //        0.00 : dt.Compute("Sum(trninqty)", ""))).ToString("#,##0;(#,##0); ");

        //    ((Label)this.dvQtyBasisPeriodic.FooterRow.FindControl("lbltoutfq")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnoutqty)", "")) ?
        //       0.00 : dt.Compute("Sum(trnoutqty)", ""))).ToString("#,##0;(#,##0); ");

        //    ((Label)this.dvQtyBasisPeriodic.FooterRow.FindControl("lbllstfq")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(lqty)", "")) ?
        //        0.00 : dt.Compute("Sum(lqty)", ""))).ToString("#,##0;(#,##0); ");

        //    ((Label)this.dvQtyBasisPeriodic.FooterRow.FindControl("lblnetrcvfq")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(netrcvqty)", "")) ?
        //       0.00 : dt.Compute("Sum(netrcvqty)", ""))).ToString("#,##0;(#,##0); ");

        //    ((Label)this.dvQtyBasisPeriodic.FooterRow.FindControl("lblisufq")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(issueqty)", "")) ?
        //        0.00 : dt.Compute("Sum(issueqty)", ""))).ToString("#,##0;(#,##0); ");

        //    ((Label)this.dvQtyBasisPeriodic.FooterRow.FindControl("lblactstktfq")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(actstock)", "")) ?
        //        0.00 : dt.Compute("Sum(actstock)", ""))).ToString("#,##0;(#,##0); ");
        //}
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }

        protected void gvMatStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMatStock.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvQtyBasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvQtyBasis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvMatStock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkgcResDesc");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("hlnkqtyBasis");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                hlink1.NavigateUrl = "~/F_12_Inv/RptIndPrjAmtBasisRes.aspx?prjcode=" + pactcode;
                hlink2.NavigateUrl = "~/F_12_Inv/RptProjectStock.aspx?Type=inv&prjcode=" + pactcode;
            }
        }
        protected void gvAmtPeriodic_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAmtPeriodic.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void dvQtyBasisPeriodic_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dvQtyBasisPeriodic.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }
        protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rpt = this.ddlReport.SelectedValue;
            switch (rpt)
            {
                case "amtbasis":
                case "qtybasis":
                    this.lblDate.Visible = false;
                    this.txtFDate.Visible = false;
                    break;
                default:
                    this.lblDate.Visible = true;
                    this.txtFDate.Visible = true;
                    break;
            }
        }

        protected void gvAmtPeriodic_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkNetBasis");
                string date1 = this.txtFDate.Text;
                string date2 = this.txttoDate.Text;
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                hlink1.NavigateUrl = "~/F_12_Inv/RptInventoryNet.aspx?prjcode=" + pactcode + "&Date1=" + date1 + "&Date2=" + date2;
            }


        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string frmdate = this.txtFDate.Text;
            string todate = this.txttoDate.Text;
            string reportType = GetReportType();

            LocalReport Rpt1 = new LocalReport();
            if (reportType == "amtbasis")
            {
                DataTable dt = (DataTable)Session["amtbasis"];
                var list = dt.DataTableToList<RealEntity.C_12_Inv.EclassPurchase.InventoryAmountBasis>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptInvenAmtBasis", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Inventory Report Amount Basis"));
            }else if(reportType== "qtybasis")
            {
                DataTable dt = (DataTable)Session["qtybasis"];
                var list = dt.DataTableToList<RealEntity.C_12_Inv.EclassPurchase.InventoryAmountBasis>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptInvenQtyBasis", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Inventory Report Quantity Basis"));
            }else if(reportType == "amtbasisp")
            {
                DataTable dt = (DataTable)Session["amtbasisp"];
                var list = dt.DataTableToList<RealEntity.C_12_Inv.EclassPurchase.InventoryAmountBasis>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptInvenAmtBasisPeriodic", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Inventory Report Amount Basis Periodic"));
            }
            else if (reportType== "qtybasisp")
            {
                DataTable dt = (DataTable)Session["qtybasisp"];
                var list = dt.DataTableToList<RealEntity.C_12_Inv.EclassPurchase.InventoryQtyBasisPeriodic>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptInvenQtyBasisPeriodic", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Inventory Report Quantity Basis Periodic"));
            }
            Rpt1.SetParameters(new ReportParameter("comNam", comnam));
            Rpt1.SetParameters(new ReportParameter("comAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("footer", printFooter));
            Rpt1.SetParameters(new ReportParameter("comLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("frmdate", frmdate));
            Rpt1.SetParameters(new ReportParameter("todate", todate));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private string GetReportType()
        {
            string Type = "";
            int index = this.ddlReport.SelectedIndex;
            switch (index)
            {
                case 0:
                    Type = "amtbasis";
                    break;

                case 1:
                    Type = "qtybasis";
                    break;

                case 2:
                    Type = "amtbasisp";
                    break;

                default:
                    Type = "qtybasisp";
                    break;
            }
            return Type;
        }




    }
}