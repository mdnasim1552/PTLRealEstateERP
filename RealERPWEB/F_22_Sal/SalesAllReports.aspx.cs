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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;

namespace RealERPWEB.F_22_Sal
{
    public partial class SalesAllReports : System.Web.UI.Page
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
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Sales Reports";
                //string date1 = this.Request.QueryString["Date1"];
                //string date2 = this.Request.QueryString["Date2"];
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                 this.txtFDate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttoDate.Text =  System.DateTime.Today.ToString("dd-MMM-yyyy");

              this.GetProjectName();
              this.ddlReport_SelectedIndexChanged(null, null);



            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            // ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
        }


        protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rpt = this.ddlReport.SelectedValue;
            switch (rpt)
            {
                case "PrjCollect":

                    this.lblDate.Visible = true; 
                    this.txtFDate.Visible = true;
                    this.clfdate.Visible = true;
                    this.lbltoDate.Visible = true; 
                    this.txttoDate.Visible = true;
                    this.clstodat.Visible = true;
                    this.imgbtnFindCustomer.Visible = false;
                    this.ddlCustName.Visible = false;
                    this.clcust.Visible = false;
                    break;

                case "PrjCollTilldate":                               
                    this.lblDate.Visible = false;
                    this.txtFDate.Visible = false;
                    this.clfdate.Visible = false;
                    this.imgbtnFindCustomer.Visible = false;
                    this.ddlCustName.Visible = false;
                    this.clcust.Visible = false;
                    break;

                case "PaymentStatus":
                    this.lblDate.Visible = false;
                    this.txtFDate.Visible = false;
                    this.clfdate.Visible = false;
                    this.imgbtnFindCustomer.Visible = true;
                    this.ddlCustName.Visible = true;
                    this.clcust.Visible = true;
                    break;
                default:
                    this.lblDate.Visible = true;
                    this.txtFDate.Visible = true;
                    this.clfdate.Visible = false;
                    this.lbltoDate.Visible = true;
                    this.txttoDate.Visible = true;
                    this.clstodat.Visible = true;
                    this.imgbtnFindCustomer.Visible = false;
                    this.ddlCustName.Visible = false;
                    this.clcust.Visible = false;
                    break;
            }
        }


        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        private void GetProjectName()
        {
            string comcod = this.GetComeCode();
            string Srchpactcode = "%%";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_COLLECTIONMGT", "GETPROJECTNAME", Srchpactcode, "", "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "pactdesc";
            this.ddlPrjName.DataValueField = "pactcode";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();


        }

        protected void ddlPrjName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetCustomerName();

        }

        private void GetCustomerName()
        {
            ViewState.Remove("tblcustomer");

            string comcod = this.GetComeCode();
            string pactcode = this.ddlPrjName.SelectedValue.ToString();
            string txtSProject = "%" + this.txtSrcCustomer.Text.Trim() + "%";
            string islandowner = this.Request.QueryString["Type"] == "LOClLedger" ? "1" : "0";
            DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCUSTOMERNAME", pactcode, txtSProject, islandowner, "", "", "", "", "", "");
            this.ddlCustName.DataTextField = "custnam";
            this.ddlCustName.DataValueField = "custid";
            this.ddlCustName.DataSource = ds2.Tables[0];
            this.ddlCustName.DataBind();
            ViewState["tblcustomer"] = ds2.Tables[0];
            ds2.Dispose();

        }

        protected void imgbtnFindCustomer_Click(object sender, EventArgs e)
        {
            this.GetCustomerName();
        }


        public void LoadData()
        {
            string rpt = this.ddlReport.SelectedValue;
            switch (rpt)
            {
                case "PrjCollect":
                    this.GetPrCollection();

                    break;
                case "PrjCollTilldate":
                    this.GetPrCollectionTillDate();

                    break;
                case "PaymentStatus":
                    this.ShowPaymentStatus();
                    break;
                //case "qtybasisp":
                //    this.InventoryAmtBasisPeriodic();
                //    break;

                default:
                    break;
            }
        }


        private void GetPrCollection()
        {
            string comcod = this.GetComeCode();
            string frmdate = this.txtFDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string pactcode = this.ddlPrjName.SelectedValue.ToString()=="000000000000"? "18"+"%" : this.ddlPrjName.SelectedValue.ToString() + "%";

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_COLLECTIONMGT", "GETTOTALCOLLECTION", pactcode, frmdate, todate, "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvprjcoll.DataSource = null;
                this.gvprjcoll.DataBind();
               
                return;

            }

            // DataTable dt=this.HiddenSamaData(ds1.Tables[0])

            ViewState["prjcoll"] = ds1.Tables[0];
          

            this.Data_Bind();

        }
        private void GetPrCollectionTillDate()
        {
            string comcod = this.GetComeCode();

           // string frmdate = this.txtFDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string pactcode = this.ddlPrjName.SelectedValue.ToString() == "000000000000" ? "18" + "%" : this.ddlPrjName.SelectedValue.ToString() + "%";
            string length = "Length";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_COLLECTIONMGT", "GETTOTALCOLLECTION", pactcode, "", todate, length, "", "", "", "");
            if (ds1 == null)
            {

                this.gvprjcolltilldate.DataSource = null;
                this.gvprjcolltilldate.DataBind();
                
                return;

            }

            // DataTable dt=this.HiddenSamaData(ds1.Tables[0])

            ViewState["prjcoll"] = ds1.Tables[0];
            this.Data_Bind();
        }
        public void ShowPaymentStatus()
        {
            string comcod = this.GetComeCode();

            // string frmdate = this.txtFDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string pactcode = this.ddlPrjName.SelectedValue.ToString() == "000000000000" ? "18" + "%" : this.ddlPrjName.SelectedValue.ToString() + "%";
            string usircode = this.ddlCustName.SelectedValue.ToString();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_COLLECTIONMGT", "GETPAYMENTSATATUS", pactcode, usircode, todate, "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvpaystatus.DataSource = null;
                this.gvpaystatus.DataBind();

                return;

            }

            // DataTable dt=this.HiddenSamaData(ds1.Tables[0])

            ViewState["prjcoll"] = ds1.Tables[0];
            ViewState["prjdesc"] = ds1.Tables[1];
            ViewState["prjcust"] = ds1.Tables[2];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["prjcoll"];
           
            string rpt = this.ddlReport.SelectedValue;
            switch (rpt)
            {
                case "amtbasis":
                    
                    break;
                case "PrjCollect":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.gvprjcoll.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvprjcoll.DataSource = dt;
                    this.gvprjcoll.DataBind();
                    this.FooterCal();
                    break;
                case "PrjCollTilldate":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.gvprjcolltilldate.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvprjcolltilldate.DataSource = dt;
                    this.gvprjcolltilldate.DataBind();
                    this.FooterCal();
                    break;
                case "PaymentStatus":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.gvpaystatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvpaystatus.DataSource = dt;
                    this.gvpaystatus.DataBind();
                    this.FooterCal();
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
            DataTable dt = (DataTable)ViewState["prjcoll"];
            if (dt.Rows.Count == 0)
                return;
            
            string rpt = this.ddlReport.SelectedValue;

            switch (rpt)
            {
                case "PrjCollect":
                    ((Label)this.gvprjcoll.FooterRow.FindControl("lgvFtamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ramt)", "")) ?
               0.00 : dt.Compute("Sum(ramt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    break;
                case "PrjCollTilldate":
                    ((Label)this.gvprjcolltilldate.FooterRow.FindControl("lgvFtamounttill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ramt)", "")) ?
               0.00 : dt.Compute("Sum(ramt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    break;
                case "PaymentStatus":
                    ((Label)this.gvpaystatus.FooterRow.FindControl("lgvFPaidamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(paidamt)", "")) ?
               0.00 : dt.Compute("Sum(paidamt)", ""))).ToString("#,##0.00;(#,##0.00); ");



                    break;
                default:
                    break;
            }



           
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
        //protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
        //    this.Data_Bind();
        //}

        //protected void gvMatStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    this.gvMatStock.PageIndex = e.NewPageIndex;
        //    this.Data_Bind();
        //}

        //protected void gvQtyBasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    this.gvQtyBasis.PageIndex = e.NewPageIndex;
        //    this.Data_Bind();
        //}
        //protected void gvMatStock_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkgcResDesc");
        //        HyperLink hlink2 = (HyperLink)e.Row.FindControl("hlnkqtyBasis");
        //        string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
        //        hlink1.NavigateUrl = "~/F_12_Inv/RptIndPrjAmtBasisRes.aspx?prjcode=" + pactcode;
        //        hlink2.NavigateUrl = "~/F_12_Inv/RptProjectStock.aspx?Type=inv&prjcode=" + pactcode;
        //    }
        //}
        //protected void gvAmtPeriodic_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    this.gvAmtPeriodic.PageIndex = e.NewPageIndex;
        //    this.Data_Bind();
        //}
        protected void dvQtyBasisPeriodic_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dvQtyBasisPeriodic.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
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
            string printdate = System.DateTime.Now.ToString("dd-MMMM-yyyy");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string frmdate = this.txtFDate.Text;
            string todate = this.txttoDate.Text;
            string reportType = GetReportType();
            DataTable dt2 = (DataTable)ViewState["prjcust"];
           string custname = dt2.Rows[0]["custname"].ToString();
           string udesc = dt2.Rows[0]["udesc"].ToString();
           string mobileno = dt2.Rows[0]["mobileno"].ToString();
           string preaddress = dt2.Rows[0]["preaddress"].ToString();

            LocalReport Rpt1 = new LocalReport();
            if (this.ddlReport.SelectedValue == "PaymentStatus")
            {
                DataTable dt = (DataTable)ViewState["prjcoll"];
                DataTable dt1 = (DataTable)ViewState["prjdesc"];
               
                
                var list = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales.PaymentStatusReconcile>();
                var list1 = dt1.DataTableToList<RealEntity.C_22_Sal.EClassSales.PaymentStatusRevenue>();
               
                   Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptPaymentSystem", list, list1, null);
                   Rpt1.EnableExternalImages = true;
                   Rpt1.SetParameters(new ReportParameter("rptTitle", "Payment Status"));
            }
          
           
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("custname", custname));
            Rpt1.SetParameters(new ReportParameter("udesc", udesc));
            Rpt1.SetParameters(new ReportParameter("mobileno", mobileno));
            Rpt1.SetParameters(new ReportParameter("preaddress", preaddress));
            
            Rpt1.SetParameters(new ReportParameter("printdate", "Print Date : " + printdate));


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