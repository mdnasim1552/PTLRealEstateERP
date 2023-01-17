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
    public partial class RptDailyTransCashBank : System.Web.UI.Page
    {

        ProcessAccess MktData = new ProcessAccess();

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
                //((Label)this.Master.FindControl("lblTitle")).Text = " Daily Payment Reports";
                //this.Master.Page.Title = " Daily Payment Reports";
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = (comcod == "3336" || comcod == "3337") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : "01-" + ASTUtility.Right(Date, 8);
                this.txttodate.Text = (comcod == "3336" || comcod == "3337") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(this.txtfromdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            }



        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowCashBook();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Show Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }



        private void ShowCashBook()
        {
            Session.Remove("cashbank");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string comcod = hst["comcod"].ToString();
            // string RptGroup = ((this.rbtnGroup.SelectedItem.Text.Trim() == "Deposit") ? "R" : "P");

            string cashorbank = (this.rbtnGroup.SelectedIndex == 0) ? "C" : (this.rbtnGroup.SelectedIndex == 1) ? "B" : "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "RPTDAILYRANSCASHBANK", fromdate, todate, cashorbank, "", "", "", "", "", "");
            if (ds1 == null)
                return;




            Session["cashbank"] = ds1.Tables[0];
            DataTable dtr1 = HiddenSameDate(ds1.Tables[0]);
            this.gvcashbook.DataSource = dtr1;
            this.gvcashbook.DataBind();



            Session["Report1"] = gvcashbook;
            if (dtr1.Rows.Count > 0)
                ((HyperLink)this.gvcashbook.HeaderRow.FindControl("hlbtnbtbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }


        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string vounum = dt1.Rows[0]["vounum"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["vounum"].ToString() == vounum)
                {

                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";

                }



                vounum = dt1.Rows[j]["vounum"].ToString();

            }
            return dt1;
        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.PrintCashBook02();
            //switch (comcod)
            //{ 
            //    case "2305":
            //    case "3305":
            //    case "3306":
            //    case "3307":
            //    case "3308":
            //    case "3309":

            //        this.PrintCashBook02();
            //        break;
            //    default:
            //        this.PrintCashBook();
            //        break;

            //}
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void SaveValue()
        {

            DataTable dt = ((DataTable)Session["cashbank"]);



            for (int i = 0; i < this.gvcashbook.Rows.Count; i++)
            {
                string chkmr = (((CheckBox)this.gvcashbook.Rows[i].FindControl("chkvounum")).Checked) ? "True" : "False";
                dt.Rows[i]["chk"] = chkmr;
            }
            Session["cashbank"] = dt;
        }
        private void PrintCashBook02()
        {


            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();
            this.SaveValue();

            DataTable dt = ((DataTable)Session["cashbank"]).Copy();

            DataRow[] dr1 = dt.Select("chk='True'");
            DataView dv = dt.DefaultView; ;
            if (dr1.Length > 0)
            {

                dv.RowFilter = ("chk='True'");


            }
            dt = dv.ToTable();

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.DailyPaymentTrn>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptDailyTrans", lst, null, null);

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("rptname", "DAILY PAYMENT TRANSACTION"));
            Rpt1.SetParameters(new ReportParameter("FTDate", "(From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")"));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintCashBook02Old()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataSet ds = (DataSet)Session["cashbank"];
            DataTable dt = HiddenSameDate(ds.Tables[0]);
            ReportDocument rptcb1 = new RealERPRPT.R_17_Acc.RptAccNetTransCashBanK();
            TextObject rptCname = rptcb1.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptCname.Text = comnam;



            TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "(From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";
            TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptcb1.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptcb1.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptcb1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        //private void PrintCashBook() 
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

        //    DataSet ds = (DataSet)Session["cashbank"];
        //    DataTable dt = HiddenSameDate(ds.Tables[0]);
        //    ReportDocument rptcb1 = new RealERPRPT.R_17_Acc.RptAccNetTransCashBanK();
        //    TextObject rptCname = rptcb1.ReportDefinition.ReportObjects["CompName"] as TextObject;
        //    rptCname.Text = comnam;

        //    string RptGroup = this.rbtnGroup.SelectedItem.Text.Trim();
        //    switch (RptGroup)
        //    {
        //        case "Deposit":
        //            TextObject rptAmt = rptcb1.ReportDefinition.ReportObjects["txtamt"] as TextObject;
        //            rptAmt.Text = "Deposit";
        //            break;
        //        case "Withdraw":
        //            TextObject rptAmt1 = rptcb1.ReportDefinition.ReportObjects["txtamt"] as TextObject;
        //            rptAmt1.Text = "Withdraw";
        //            break;
        //    }


        //    TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["date"] as TextObject;
        //    rptdate.Text = "(From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";
        //    TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rptcb1.SetDataSource(dt);
        //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    rptcb1.SetParameterValue("ComLogo", ComLogo);
        //    Session["Report1"] = rptcb1;
        //    lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                        this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        //}

        protected void gvcashbook_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblpayto = (Label)e.Row.FindControl("lblgvPaytoRec");
                CheckBox chkvounum = (CheckBox)e.Row.FindControl("chkvounum");
                Label lblamt = (Label)e.Row.FindControl("txtgvDpAmt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lblpayto.Font.Bold = true;
                    lblamt.Font.Bold = true;
                    lblpayto.Style.Add("text-align", "right");
                    chkvounum.Visible = false;
                }

            }
        }

    }
}
