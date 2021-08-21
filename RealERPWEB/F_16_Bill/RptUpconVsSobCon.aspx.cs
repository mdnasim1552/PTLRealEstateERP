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
namespace RealERPWEB.F_16_Bill
{
    public partial class RptUpconVsSobCon : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = (this.Request.QueryString["Date1"].Length > 0) ? this.Request.QueryString["Date1"] : Convert.ToDateTime("01" + Date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = (this.Request.QueryString["Date2"].Length > 0) ? this.Request.QueryString["Date2"] : Convert.ToDateTime(txtfromdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.GetProjectName();



            }

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //Iqbal  Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string PactCode = this.ddlProjectName.SelectedItem.Text.Substring(13).ToString();
            // DataSet ds = (DataSet)Session["tblData"];
            var lst = (List<RealEntity.C_16_Bill.EClassBilling.EClassUpconVsSubCon>)Session["tblData"];

            LocalReport Rpt1 = new LocalReport();
            // var lst = dt.DataTableToList<RealEntity.C_16_Bill.BO_BillEntry.UpcomSubCon>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_16_Bill.RptUpconSabCon", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", "Project Name: " + PactCode));
            Rpt1.SetParameters(new ReportParameter("Date", "From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("d-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Upcon Vs Sub-Contractor"));
            Rpt1.SetParameters(new ReportParameter("PrintFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = ((this.Request.QueryString["prjcode"].ToString().Trim().Length > 0) ? this.Request.QueryString["prjcode"].ToString() : ("%" + this.txtSrcPro.Text.Trim())) + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "GETPURPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }



        //[WebMethod (EnableSession = false)]
        //[ScriptMethod (ResponseFormat = ResponseFormat.Json)]
        //public static string GetAllData(string date1,string date2)
        //{
        //    Common ObjCommon = new Common ();
        //    string comcod = ObjCommon.GetCompCode ();
        //    ProcessAccess MktData = new ProcessAccess ();
        //    DataSet ds1 = MktData.GetTransInfo (comcod, "SP_REPORT_SALSMGT_SUM", "RPTMONTHLYSALE", date1, date2, "", "", "", "", "", "", "");
        //    var lst = ds1.Tables[0].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.monsale>();
        //    var lst1 = ds1.Tables[1].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.yearsale> ();
        //    var datalist = new MyAllGraphData (lst, lst1);
        //    var jsonSerialiser = new JavaScriptSerializer ();
        //    var json = jsonSerialiser.Serialize (datalist);
        //    return json;

        //}




        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "RPTUPCONVSSUBCON", PactCode, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSubBill.DataSource = null;
                this.gvSubBill.DataBind();
                return;
            }
            Session["tblData"] = ds1.Tables[0].DataTableToList<RealEntity.C_16_Bill.EClassBilling.EClassUpconVsSubCon>();

            this.Data_Bind();

        }


        //     private List<RealEntity.C_16_Bill.EClassBilling.EClassUpconVsSubCon> HiddenSameData(List<RealEntity.C_16_Bill.EClassBilling.EClassUpconVsSubCon> lst)
        //    {
        //        if (lst.Count == 0)
        //            return lst;
        //        int j=0;
        //        string pactcode = lst[0].pactcode;
        //        foreach (var lst1 in lst)
        //        {
        //            if (j == 0) 
        //            {
        //                j++;

        //            }

        //            else   if (lst1.pactcode.ToString() == pactcode)
        //            {
        //                lst1.pactdesc= "";

        //            }



        //            pactcode = lst1.pactcode;




        //        }

        //        return lst;

        //        }
        //}
        private void Data_Bind()
        {

            List<RealEntity.C_16_Bill.EClassBilling.EClassUpconVsSubCon> lst = (List<RealEntity.C_16_Bill.EClassBilling.EClassUpconVsSubCon>)Session["tblData"];

            this.gvSubBill.DataSource = lst;
            this.gvSubBill.DataBind();
            this.FooterCalculation();
        }
        protected void ibtnFindProject_OnClick(object sender, EventArgs e)
        {
            this.GetProjectName();

        }


        private void FooterCalculation()
        {
            List<RealEntity.C_16_Bill.EClassBilling.EClassUpconVsSubCon> lst = (List<RealEntity.C_16_Bill.EClassBilling.EClassUpconVsSubCon>)Session["tblData"];
            if (lst.Count == 0)
                return;

            ((Label)this.gvSubBill.FooterRow.FindControl("lblgvFconamt")).Text = lst.Sum(l => l.conamt).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lblgvFupconamt")).Text = lst.Sum(l => l.upconamt).ToString("#,##0;(#,##0); ");
            //  ((Label)this.gvSubBill.FooterRow.FindControl("lblgvFconuamt")).Text = lst.Sum(l => l.conuamt).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSubBill.FooterRow.FindControl("lblgvFupconuamt")).Text = lst.Sum(l => l.conamt).ToString("#,##0;(#,##0); ");
            //((Label)this.gvSubBill.FooterRow.FindControl("lblgvFdifamt")).Text = lst.Sum(l => l.difupacon).ToString("#,##0;(#,##0); ");



        }
        //protected void gvSubBill_RowCreated(object sender, GridViewRowEventArgs e)
        //{

        //    GridViewRow gvRow = e.Row;
        //    if (gvRow.RowType == DataControlRowType.Header)
        //    {


        //        GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

        //        //  gvrow.Cells.Remove(TableCell [0]);

        //        TableCell cell01 = new TableCell();
        //        cell01.Text = "Sl.No.";
        //        cell01.HorizontalAlign = HorizontalAlign.Center;
        //        cell01.RowSpan = 2;
        //        gvrow.Cells.Add(cell01);



        //        TableCell cell02 = new TableCell();
        //        cell02.Text = " Work Description";
        //        cell02.HorizontalAlign = HorizontalAlign.Center;
        //        cell02.RowSpan = 2;
        //        gvrow.Cells.Add(cell02);

        //        TableCell cell03 = new TableCell();
        //        cell03.Text = "Floor";
        //        cell03.HorizontalAlign = HorizontalAlign.Center;
        //        cell03.RowSpan = 2;
        //        gvrow.Cells.Add(cell03);

        //        TableCell cell04au = new TableCell();
        //        cell04au.Text = "Unit";
        //        cell04au.HorizontalAlign = HorizontalAlign.Center;
        //        cell04au.RowSpan = 2;
        //        gvrow.Cells.Add(cell04au);


        //        TableCell cell04aup = new TableCell();
        //        cell04aup.Text = "Masurement Book";
        //        cell04aup.HorizontalAlign = HorizontalAlign.Center;
        //        cell04aup.Attributes["style"] = "font-weight:bold;";
        //        cell04aup.ColumnSpan = 2;
        //        gvrow.Cells.Add(cell04aup);


        //        TableCell cell04 = new TableCell();
        //        cell04.Text = "Rate";
        //        cell04.HorizontalAlign = HorizontalAlign.Center;
        //        cell04.Attributes["style"] = "font-weight:bold;";
        //        cell04.ColumnSpan = 2;
        //        gvrow.Cells.Add(cell04);

        //        //TableCell cell04a = new TableCell();
        //        //cell04a.Text = "Rate";
        //        //cell04a.HorizontalAlign = HorizontalAlign.Center;
        //        //cell04a.RowSpan = 2;
        //        //gvrow.Cells.Add(cell04a);

        //        TableCell cell05 = new TableCell();
        //        cell05.Text = "Work Quantity for this R/A Bill";
        //        cell05.HorizontalAlign = HorizontalAlign.Center;
        //        cell05.Attributes["style"] = "font-weight:bold;";
        //        cell05.ColumnSpan = 2;
        //        gvrow.Cells.Add(cell05);



        //        TableCell cell06 = new TableCell();
        //        cell06.Text = "Value this R/A Bill";
        //        cell06.HorizontalAlign = HorizontalAlign.Center;
        //        cell06.Attributes["style"] = "font-weight:bold;";
        //        cell06.ColumnSpan = 2;
        //        gvrow.Cells.Add(cell06);


        //        TableCell cell07 = new TableCell();
        //        cell07.Text = "Cumilative Quantity Upto this R/A Bill";
        //        cell07.HorizontalAlign = HorizontalAlign.Center;
        //        cell07.Attributes["style"] = "font-weight:bold;";
        //        cell07.ColumnSpan = 2;
        //        gvrow.Cells.Add(cell07);


        //        TableCell cell08 = new TableCell();
        //        cell08.Text = "Difference";
        //        cell08.HorizontalAlign = HorizontalAlign.Center;
        //        cell08.RowSpan = 2;
        //        gvrow.Cells.Add(cell08);






        //        gvSubBill.Controls[0].Controls.AddAt(0, gvrow);
        //    }
        //}
        //protected void gvSubBill_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.Header)
        //    {
        //        e.Row.Cells[0].Visible = false;
        //        e.Row.Cells[1].Visible = false;
        //        e.Row.Cells[2].Visible = false;
        //        e.Row.Cells[3].Visible = false;
        //        e.Row.Cells[14].Visible = false;
        //    }
        //}
    }
}