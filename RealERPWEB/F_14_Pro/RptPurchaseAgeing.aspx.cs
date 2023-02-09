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
using RealERPRDLC;
namespace RealERPWEB.F_14_Pro
{
    public partial class RptPurchaseAgeing : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
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


                //((Label)this.Master.FindControl("lblTitle")).Text = "Supplier Credit Status (Purchase)";

                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetSupCategory();

                hlnkSupinfo.NavigateUrl = "~/F_14_Pro/SuppLimitCodeBook";

            }

        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetSupCategory()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_04", "SUPPLIERCATEGORY", todate, "", "", "", "", "", "", "", "");
            this.ddlSupCategory.DataTextField = "sirdesc";
            this.ddlSupCategory.DataValueField = "rescode";
            this.ddlSupCategory.DataSource = ds1.Tables[0];
            this.ddlSupCategory.DataBind();

            //this.chkSupCategory.DataTextField = "sirdesc";
            //this.chkSupCategory.DataValueField = "rsircode1";
            //this.chkSupCategory.DataSource = ds1.Tables[0];
            //this.chkSupCategory.DataBind();


        }


        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {

            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string suptype = this.ddlSupCategory.SelectedValue.ToString();
            string day1 = "";
            string day2 = "";
            string day3 = "";
           // string 
            switch (comcod)
            {
                case "3101":
                case "3354":
                   day1= "30";
                   day2= "45";
                   day3 = "60";
                    break;

                default: // For Rupayan
                    day1 = "30";
                    day2 = "90";
                    day3 = "120";
                    break;
            }



            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_04", "PURCHASEAGEINGSTATUS", suptype, todate, day1, day2, day3, "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvsupstatus.DataSource = null;
                this.gvsupstatus.DataBind();
                return;

            }
            Session["tblstatus"] = ds1.Tables[0];
            //Session["tblstatus"] = HiddenSameData(ds1.Tables[0]);

            this.Data_Bind();

        }
       

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            //DataView dv = dt1.DefaultView;
            //dv.Sort = "reqno";
            //dt1 = dv.ToTable();
            //string rsircode = dt1.Rows[0]["rsircode"].ToString();
            string pactcode = dt1.Rows[0]["pactcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }


                else
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
            }

            return dt1;
        }

        private void Data_Bind()
        {
            this.MultiView1.ActiveViewIndex = 0;
            this.gvsupstatus.DataSource = (DataTable)Session["tblstatus"];
            this.gvsupstatus.DataBind();

            this.FooterCalculation();

            string comcod = this.GetCompCode();
            string Htext1Month = "";
            string Htext4Month = "";
            string HtextUp4Month = "";
            switch (comcod)
            {
              
                case "3101":
                case "3354":
                    Htext1Month   = "Upto 30 Days";
                    Htext4Month   = "45 Days Over";
                    HtextUp4Month = "60 Days Over";
                    break;

                default: // For Rupayan
                    Htext1Month  = "Upto 90 Days";
                    Htext4Month  = "90 Days Over";
                    HtextUp4Month = "120 Days Over";
                    break;
            }
            gvsupstatus.HeaderRow.Cells[9].Text = HtextUp4Month;
            gvsupstatus.HeaderRow.Cells[10].Text = Htext4Month;
            gvsupstatus.HeaderRow.Cells[11].Text = Htext1Month;
        }



        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblstatus"];

            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvsupstatus.FooterRow.FindControl("lblFlimit")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(limit)", "")) ? 0.00 :
                 dt.Compute("sum(limit)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvsupstatus.FooterRow.FindControl("lblFchqhamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(chqhamt)", "")) ? 0.00 :
                 dt.Compute("sum(chqhamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvsupstatus.FooterRow.FindControl("lblFbillacamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billacamt)", "")) ? 0.00 :
                dt.Compute("sum(billacamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvsupstatus.FooterRow.FindControl("lblFbilliccdamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billiccdamt)", "")) ? 0.00 :
                dt.Compute("sum(billiccdamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvsupstatus.FooterRow.FindControl("lblFbillinpamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billinpamt)", "")) ? 0.00 :
                dt.Compute("sum(billinpamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvsupstatus.FooterRow.FindControl("lblFtotalduebill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalduebill)", "")) ? 0.00 :
                 dt.Compute("sum(totalduebill)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvsupstatus.FooterRow.FindControl("lblFmon4amt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mon4amt)", "")) ? 0.00 :
                dt.Compute("sum(mon4amt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvsupstatus.FooterRow.FindControl("lblFmon34amt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mon34amt)", "")) ? 0.00 :
                 dt.Compute("sum(mon34amt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvsupstatus.FooterRow.FindControl("lblFmon13amt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mon13amt)", "")) ? 0.00 :
                dt.Compute("sum(mon13amt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsupstatus.FooterRow.FindControl("lblFtotaldaysamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totaldaysamt)", "")) ? 0.00 :
                dt.Compute("sum(totaldaysamt)", ""))).ToString("#,##0;(#,##0); ");


            Session["Report1"] = gvsupstatus;
            ((HyperLink)this.gvsupstatus.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }


        //protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.Data_Bind();
        //}


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }


        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            /*
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string date1 = "From "+fromdate + " To " +todate; 

            DataTable dt = (DataTable)Session["tblstatus"];

            DataView dv1 = dt.Copy().DefaultView;
            dv1.RowFilter = ("checkdat1<>'1/1/1900 12:00:00 AM'");
            DataTable dt01 = dv1.ToTable();

            if (dt.Rows.Count == 0)
                return;
            string totalnreqQty = Convert.ToDouble((Convert.IsDBNull(dt01.Compute("sum(nreqQty)", "")) ? 0.00 :
                 dt01.Compute("sum(nreqQty)", ""))).ToString("#,##0;(#,##0); ");


            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.DateWiseReqCheckHistory>();
            LocalReport rpt = new LocalReport();

            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptDateWiseReqCheckHistory", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("comName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Date Wise Requisition Check History"));
            rpt.SetParameters(new ReportParameter("date1", date1));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comLogo", ComLogo));
            rpt.SetParameters(new ReportParameter("totalnreqQty", totalnreqQty));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


             */
        }


        //protected void gvmrfstatus_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {

        //        Label lblreqtext = (Label)e.Row.FindControl("lblgvReqMrfno");
        //        Label lblreqty = (Label)e.Row.FindControl("lblgvReqQty");

        //        string chkdate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "checkdat1")).ToString("dd-MMM-yyyy");
        //            //Convert.ToString(DataBinder.Eval(e.Row.DataItem, "checkdat1")).ToString("dd-MMM-yyyy");

        //        if (chkdate == "01-Jan-1900")
        //        {

        //            lblreqtext.Attributes["style"] = "color:green;font-weight:bold;text-align:right";
        //            lblreqty.Attributes["style"] = "color:green;font-weight:bold";
        //            //maroon


        //        }

        //    }
        //}

        protected void gvsupstatus_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //gvrow.Cells.Remove(TableCell[0]);

                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.Attributes["style"] = "font-weight:bold;";
                cell01.RowSpan = 1;
                gvrow.Cells.Add(cell01);

                TableCell cell02 = new TableCell();
                cell02.Text = "";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.Attributes["style"] = "font-weight:bold;";
                cell02.RowSpan = 1;
                gvrow.Cells.Add(cell02);

                //TableCell cell03 = new TableCell();
                //cell03.Text = "Credit Limits";
                //cell03.HorizontalAlign = HorizontalAlign.Center;
                //cell03.Attributes["style"] = "font-weight:bold;";
                //cell03.ColumnSpan = 2;
                //gvrow.Cells.Add(cell03);

                TableCell cell04 = new TableCell();
                cell04.Text = "Credit Limits";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.Attributes["style"] = "font-weight:bold;";
                cell04.ColumnSpan = 2;
                gvrow.Cells.Add(cell04);


                TableCell cell05 = new TableCell();
                cell05.Text = "Due Status";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.Attributes["style"] = "font-weight:bold;";
                cell05.ColumnSpan = 5;
                gvrow.Cells.Add(cell05);

                TableCell cell10 = new TableCell();
                cell10.Text = "Credit Days";
                cell10.HorizontalAlign = HorizontalAlign.Center;
                cell10.Attributes["style"] = "font-weight:bold;";
                cell10.ColumnSpan = 3;
                gvrow.Cells.Add(cell10);

                TableCell cell13 = new TableCell();
                cell13.Text = "";
                cell13.HorizontalAlign = HorizontalAlign.Center;
                cell13.Attributes["style"] = "font-weight:bold;";
                cell13.RowSpan = 1;
                gvrow.Cells.Add(cell13);


                gvsupstatus.Controls[0].Controls.AddAt(0, gvrow);

            }

        }
        protected void gvsupstatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkgvchqhamt = (HyperLink)e.Row.FindControl("hlnkgvchqhamt");
                HyperLink hlnkgvbillacamt = (HyperLink)e.Row.FindControl("hlnkgvbillacamt");
                HyperLink hlnkgvbilliccdamt = (HyperLink)e.Row.FindControl("hlnkgvbilliccdamt");
                HyperLink hlnkgvbillinpamt = (HyperLink)e.Row.FindControl("hlnkgvbillinpamt");
                HyperLink hlnkgvmon4amt = (HyperLink)e.Row.FindControl("hlnkgvmon4amt");
                HyperLink hlmkgvmon34amt = (HyperLink)e.Row.FindControl("hlmkgvmon34amt");
                HyperLink hlnkgvmon13amt = (HyperLink)e.Row.FindControl("hlnkgvmon13amt");

                //

                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString();
                string chqhamt = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "chqhamt")).ToString();
                string ssirdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssirdesc")).ToString();

                if (pactcode == "")
                {
                    return;
                }

                else
                {

                    hlnkgvchqhamt.Style.Add("color", "blue");
                    hlnkgvbillacamt.Style.Add("color", "blue");
                    hlnkgvbilliccdamt.Style.Add("color", "blue");
                    hlnkgvbillinpamt.Style.Add("color", "blue");
                    hlnkgvmon4amt.Style.Add("color", "blue");
                    hlmkgvmon34amt.Style.Add("color", "blue");
                    hlnkgvmon13amt.Style.Add("color", "blue");

                    //hlnkgvchqhamt.NavigateUrl = "~/F_14_Pro/RptPurchaseAgeingDetails.aspx?pactcode=" + pactcode + "&chqhamt=" + chqhamt + "&frmdate=" + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + "&todate=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + "&pactdesc=" + this.ddlProName.SelectedItem.Text + "&rsirdesc=" + rsirdesc + "&chalan=" + chalan;

                    hlnkgvchqhamt.NavigateUrl = "~/F_14_Pro/RptPurchaseAgeingDetails?pactcode=" + pactcode +"&SupName="+ ssirdesc + "&Type=" + "chqhamt" + "&todate=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    hlnkgvbillacamt.NavigateUrl = "~/F_14_Pro/RptPurchaseAgeingDetails?pactcode=" + pactcode + "&SupName=" + ssirdesc + "&Type=" + "billacamt" + "&todate=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    hlnkgvbilliccdamt.NavigateUrl = "~/F_14_Pro/RptPurchaseAgeingDetails?pactcode=" + pactcode + "&SupName=" + ssirdesc + "&Type=" + "billiccdamt" + "&todate=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    hlnkgvbillinpamt.NavigateUrl = "~/F_14_Pro/RptPurchaseAgeingDetails?pactcode=" + pactcode + "&SupName=" + ssirdesc + "&Type=" + "billinpamt" + "&todate=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    hlnkgvmon4amt.NavigateUrl = "~/F_14_Pro/RptPurchaseAgeingDetails?pactcode=" + pactcode + "&SupName=" + ssirdesc + "&Type=" + "mon4amt" + "&todate=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    hlmkgvmon34amt.NavigateUrl = "~/F_14_Pro/RptPurchaseAgeingDetails?pactcode=" + pactcode + "&SupName=" + ssirdesc + "&Type=" + "mon34amt" + "&todate=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    hlnkgvmon13amt.NavigateUrl = "~/F_14_Pro/RptPurchaseAgeingDetails?pactcode=" + pactcode + "&SupName=" + ssirdesc + "&Type=" + "mon13amt" + "&todate=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                }

            }
        }

        //protected void lnkbtnSupinfo_Click(object sender, EventArgs e)
        //{
        //    //F_14_Pro/SuppLimitCodeBook
        //    lnkbtnSupinfo.PostBackUrl = "~/F_14_Pro/SuppLimitCodeBook";
        //}
    }
}