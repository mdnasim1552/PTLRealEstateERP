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
using RealERPRDLC;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_17_Acc
{

    public partial class RptAccDayTransData : System.Web.UI.Page
    {

        ProcessAccess MktData = new ProcessAccess();
        public static double OpenBal, Clsbal, Dtdram, Dtcram;
        public static int PageNumber = 0;

        //double OpenBal = 0, Clsbal = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string comcod = GetCompCode();

                this.txtfromdate.Text = (comcod == "3336" || comcod == "3337") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Daily Transaction ";
                this.Master.Page.Title = "Transaction Listing";
                this.GetVisiblity();
                this.GetAccCode();
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            return (hst["comcod"].ToString());


        }

        private void GetVisiblity()
        {
            string comcod = this.GetCompCode();

            if (comcod == "3339")
            {
                this.lblcontrolAccHead.Visible = true;
                this.txtAccSearch.Visible = true;
                this.IbtnSearchAcc.Visible = true;
                this.ddlAccHead.Visible = true;
            }
            else
            {



            }


        }

        private void GetAccCode()
        {


            string comcod = this.GetCompCode();
            string filter = "%" + this.txtAccSearch.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "GETACCHEAD", filter, "", "", "", "", "", "", "", "");
            this.ddlAccHead.DataSource = ds1.Tables[0];
            this.ddlAccHead.DataTextField = "actdesc1";
            this.ddlAccHead.DataValueField = "actcode";
            this.ddlAccHead.DataBind();
            ds1.Dispose();
        }
        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            //PageNumber = 0;
            //this.lblCurPage.Text = "1";
            //this.lblPage.Visible = true;
            //this.txtPageNo.Visible = true;
            //this.imgbtnSearchPage.Visible = true;

            this.TransactionList();

            DataTable dt = (DataTable)Session["LastTable"];
            //double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            //int pageCount = (int)Math.Ceiling(getPageCount);
            //this.lblCurPage.ToolTip = "Page 1 of " + pageCount;

            if (ConstantInfo.LogStatus == true)
            {
                // string eventtype = this.LblTitle.Text;
                //string eventdesc = "Show Report";
                //string eventdesc2 = "";
                // bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void TransactionList()
        {
            Session.Remove("tranlist");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            // this.Paneltovoucherno.Visible = true;
            //this.GridPage.Visible = true;
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string txtVouType = this.ddlVouchar.SelectedItem.ToString().Trim();
            string txtSVoucher = (txtVouType == "ALL Voucher" ? "" : txtVouType) + "%";

            string pactcode = this.ddlAccHead.SelectedValue.ToString();
            string length = (comcod == "3339") ? "length" : "";



            string searchinfo = "";

            if (this.ddlSrch.SelectedValue != "")
            {

                if (this.ddlSrch.SelectedValue == "between")
                {
                    // searchinfo = "dram between " + Convert.ToDouble("0" + this.txtAmount1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmount2.Text.Trim()).ToString();
                }
                else
                {
                    searchinfo = "dram " + this.ddlSrch.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmount1.Text.Trim()).ToString();
                }
            }
            //int PageIndex = 0;
            //int pageSize = 100;
            //int startRow =PageIndex * pageSize;
            //  int startRow = PageNumber * 100;
            //int endRow = (PageNumber + 1) * 100;


            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "PRINTTRANSACTIONS", fromdate, todate, txtSVoucher, searchinfo, pactcode, length, "", "", "");
            if (ds1 == null)
                return;
            DataTable dtr = ds1.Tables[0];
            DataTable dtr1 = HiddenSameDate(dtr);
            Session["tranlist"] = dtr1;
            DataTable tblt03 = (DataTable)Session["tranlist"];
            // this.gvtranlsit.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvtranlsit.DataSource = dtr1;
            this.gvtranlsit.DataBind();
            Session["tranlist"] = dtr1;
            // Session["LastTable"] = ds1.Tables[3];
            // this.FooterCalculation(ds1.Tables[3], "gvtranlsit");

            this.lbltoCashVoucher.Text = Convert.ToDouble(ds1.Tables[2].Rows[0]["tonum"]).ToString("#, #,#0; (#, #,#0); ");
            this.lbltoBankVoucher.Text = Convert.ToDouble(ds1.Tables[2].Rows[1]["tonum"]).ToString("#, #,#0; (#, #,#0); ");
            this.lbltoContraVoucher.Text = Convert.ToDouble(ds1.Tables[2].Rows[2]["tonum"]).ToString("#, #,#0; (#, #,#0); ");
            this.lbltoJournalVoucher.Text = Convert.ToDouble(ds1.Tables[2].Rows[3]["tonum"]).ToString("#, #,#0; (#, #,#0); ");
            Session["Report1"] = gvtranlsit;
            if (dtr1.Rows.Count > 0)
                ((HyperLink)this.gvtranlsit.HeaderRow.FindControl("hlbtnbtbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }


        private void RptTransactionList()
        {
            Session.Remove("tranlist");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            //this.Paneltovoucherno.Visible = true;
            //this.GridPage.Visible = true;
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string txtVouType = this.ddlVouchar.SelectedItem.ToString().Trim();
            string txtSVoucher = (txtVouType == "ALL Voucher" ? "" : txtVouType) + "%";


            string searchinfo = "";

            if (this.ddlSrch.SelectedValue != "")
            {

                if (this.ddlSrch.SelectedValue == "between")
                {
                    //searchinfo = "dram between " + Convert.ToDouble("0" + this.txtAmount1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmount2.Text.Trim()).ToString();
                }
                else
                {
                    searchinfo = "dram " + this.ddlSrch.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmount1.Text.Trim()).ToString();
                }
            }

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "PRINTTRANSACTIONS", fromdate, todate, txtSVoucher, searchinfo, "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dtr = ds1.Tables[0];
            DataTable dtr1 = HiddenSameDate(dtr);
            Session["tranlist"] = dtr1;
            DataTable tblt03 = (DataTable)Session["tranlist"];

            Session["tranlist"] = dtr1;

            this.FooterCalculation(dtr1, "gvtranlsitp");


        }
        private void FooterCalculation(DataTable dt, string GvName)
        {
            if (dt.Rows.Count == 0)
                return;
            DataView dv = new DataView();

            //
            switch (GvName)
            {


                case "gvtranlsit":

                    ((Label)this.gvtranlsit.FooterRow.FindControl("lgvFDram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                             0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvtranlsit.FooterRow.FindControl("txtgvFCram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                           0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0) ;");
                    //Dtdram = Convert.ToDouble("0" + ((Label)this.gvtranlsit.FooterRow.FindControl("lgvFDram")).Text);
                    //Dtcram = Convert.ToDouble("0" + ((Label)this.gvtranlsit.FooterRow.FindControl("txtgvFCram")).Text);
                    break;
                case "gvdtranlsitp":
                    dv = dt.DefaultView;
                    dv.RowFilter = ("acrescode not in('" + "  Total Amt:" + "')");
                    dt = dv.ToTable();
                    string dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                             0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0) ;");
                    string cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                           0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0) ;");
                    Dtdram = Convert.ToDouble("0" + dramt);
                    Dtcram = Convert.ToDouble("0" + cramt);
                    break;



            }


        }

        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string Date1, vounum;
            int j;

            {
                Date1 = dt1.Rows[0]["voudat1"].ToString();
                vounum = dt1.Rows[0]["vounum1"].ToString();
                for (j = 1; j < dt1.Rows.Count; j++)
                {
                    if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                    {
                        vounum = dt1.Rows[j]["vounum1"].ToString();
                        dt1.Rows[j]["vounum1"] = "";
                        dt1.Rows[j]["voudat1"] = "";


                    }

                    else
                    {
                        vounum = dt1.Rows[j]["vounum1"].ToString();
                    }

                }
            }

            return dt1;

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();


            this.PrintTransaction();

            if (ConstantInfo.LogStatus == true)
            {
                //string eventtype = this.LblTitle.Text;
                // string eventdesc = "Print Report";//
                //string eventdesc2 = "";//
                // bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




        }



        private void PrintTransaction()
        {
            // Iqbal Nayan
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;


            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tranlist"];
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.EClassTranList>();
            //var TAmt = lst.Select(p => p.isuamt).Sum();


            if (comcod == "3348")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptDailyTransactionCredence", lst, null, null);
                Rpt1.SetParameters(new ReportParameter("ProjectDesc", comcod == "3338" ? (this.ddlAccHead.SelectedValue == "000000000000" ? "" : this.ddlAccHead.SelectedItem.Text.Substring(13)) : ""));
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptDailyTransaction", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            }
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("title", "TRANSACTION LIST"));
            Rpt1.SetParameters(new ReportParameter("dramt", Dtdram.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("cramt", Dtcram.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("Date", "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comcod=this.GetCompCode();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //this.TransactionList();

            //DataTable dt = (DataTable)Session["tranlist"];
            //ReportDocument rptdtlist = new RealERPRPT.R_17_Acc.RptDailyTransaction();
            //TextObject rptCname = rptdtlist.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptdate = rptdtlist.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";

            //TextObject prodesc = rptdtlist.ReportDefinition.ReportObjects["ProjectDesc"] as TextObject;
            //rptCname.Text = comcod == "3338"?(this.ddlAccHead.SelectedValue=="000000000000"?"": this.ddlAccHead.SelectedItem.Text.Substring(13)) : "" ;
            //TextObject rptdram = rptdtlist.ReportDefinition.ReportObjects["txtdram"] as TextObject;
            //rptdram.Text = Dtdram.ToString("#,##0;(#,##0); ");
            //TextObject rptcram = rptdtlist.ReportDefinition.ReportObjects["txtcram"] as TextObject;
            //rptcram.Text = Dtcram.ToString("#,##0;(#,##0); ");

            //TextObject txtuserinfo = rptdtlist.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptdtlist.SetDataSource(dt);
            //Session["Report1"] = rptdtlist;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            ////lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            ////this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //// string proname = dt.Select("prgcod='02002'").Length == 0 ? "" : dt.Select("prgcod='02002'")[0]["prgdesc1"].ToString();

        }



        protected void gvtranlsit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label acrescode = (Label)e.Row.FindControl("lblgvAcRsCode");
                Label acresdesc = (Label)e.Row.FindControl("lblgvAcRsDesc");
                Label lbldram = (Label)e.Row.FindControl("lgvDram");
                Label lblcramt = (Label)e.Row.FindControl("txtgvCram");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "acrescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "  Total Amt:")
                {
                    acrescode.Font.Bold = true;
                    acresdesc.Font.Bold = true;
                    lbldram.Font.Bold = true;
                    lblcramt.Font.Bold = true;
                    //lblcramt.Style.Add("text-transform", "initcap");
                }
            }
        }

        protected void gvtranlsit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //this.SessionUpdate2();
            this.gvtranlsit.PageIndex = e.NewPageIndex;
            this.TransactionList();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TransactionList();
            //this.gvtranlsit_DataBind();
        }

        protected void imgbtnSearchVoucher_Click(object sender, EventArgs e)
        {
            this.TransactionList();
        }

        protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblTo.Visible = (this.ddlSrch.SelectedValue == "between");
            //this.txtAmount2.Visible = (this.ddlSrch.SelectedValue == "between");
        }


        protected void imgBtnFirst_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["LastTable"];
            //double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            //int pageCount = (int)Math.Ceiling(getPageCount);

            //PageNumber = 0;
            this.TransactionList();
            //this.lblCurPage.Text = "1";
            //this.lblCurPage.ToolTip = "Page 1 of " + pageCount;
            //this.imgBtnPerv.Enabled = false;
            //this.imgBtnNext.Enabled = true;
        }
        //protected void imgBtnNext_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = (DataTable)Session["LastTable"];
        //    double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
        //    int pageCount = (int)Math.Ceiling(getPageCount);

        //    PageNumber = PageNumber + 1;

        //    if (PageNumber == pageCount)
        //    {
        //        PageNumber = PageNumber - 1;
        //       // this.imgBtnNext.Enabled = false;
        //        return;
        //    }
        //    //this.lblCurPage.ToolTip = "Page " + (PageNumber + 1) + " of " + pageCount;
        //    //this.TransactionList();
        //    //this.lblCurPage.Text = (PageNumber + 1).ToString();
        //    //this.imgBtnPerv.Enabled = true;
        //}
        //protected void imgBtnPerv_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = (DataTable)Session["LastTable"];
        //    double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
        //    int pageCount = (int)Math.Ceiling(getPageCount);

        //    PageNumber = PageNumber - 1;
        //    if (PageNumber < 0)
        //    {
        //        PageNumber = 0;
        //        this.imgBtnPerv.Enabled = false;
        //        return;
        //    }
        //    this.lblCurPage.ToolTip = "Page " + (PageNumber + 1) + " of " + pageCount;
        //    this.TransactionList();
        //    this.lblCurPage.Text = (PageNumber + 1).ToString();
        //    this.imgBtnNext.Enabled = true;
        //}
        //protected void imgBtnLast_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = (DataTable)Session["LastTable"];
        //    double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
        //    int pageCount = (int)Math.Ceiling(getPageCount);

        //    PageNumber = pageCount - 1;
        //    this.TransactionList();
        //    this.lblCurPage.Text = pageCount.ToString();
        //    this.lblCurPage.ToolTip = "Page " + (pageCount) + " of " + pageCount;
        //    this.imgBtnNext.Enabled = false;
        //    this.imgBtnPerv.Enabled = true;
        //}
        //protected void imgbtnSearchPage_Click(object sender, EventArgs e)
        //{
        //    PageNumber = (this.txtPageNo.Text.Length == 0) ? 0 : Convert.ToInt32(this.txtPageNo.Text) - 1;
        //    DataTable dt = (DataTable)Session["LastTable"];
        //    double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
        //    int pageCount = (int)Math.Ceiling(getPageCount);

        //    //PageNumber = PageNumber - 1;

        //    if (PageNumber >= pageCount)
        //    {
        //        this.imgBtnNext.Enabled = false;
        //        return;
        //    }
        //    this.lblCurPage.Text = (PageNumber + 1).ToString();
        //    this.lblCurPage.ToolTip = "Page " + (PageNumber + 1) + " of " + pageCount;
        //    this.TransactionList();
        //}

        protected void IbtnSearchAcc_Click(object sender, EventArgs e)
        {
            this.GetAccCode();
        }
    }
}
