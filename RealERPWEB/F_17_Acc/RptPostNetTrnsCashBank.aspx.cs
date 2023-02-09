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

namespace RealERPWEB.F_17_Acc
{
    public partial class RptPostNetTrnsCashBank : System.Web.UI.Page
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

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Project Summary Infow ";

                string Date = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01-" + ASTUtility.Right(Date, 8);
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            }



        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            // ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
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
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text.ToString();
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

            string CallType = ((this.rbtnGroup.SelectedValue.Trim() == "Deposit") ? "RPTNETTRANSCASHBANK02" : "RPTPOSTNETTRANSCASHABANK");

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", CallType, fromdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.MultiView1.ActiveViewIndex = 0;
            DataView dvr = new DataView();
            string RptGroup1 = this.rbtnGroup.SelectedValue.Trim();
            switch (RptGroup1)
            {
                case "Deposit":
                   
                    this.lblReceiptCash.Visible = true;
                    this.lblDetailsCash.Visible = true;
                    this.lblReceiptCash.Text = "Deposit";
                    this.gvcashbook.Columns[2].HeaderText = "Received Date";
                    this.gvcashbook.Columns[3].HeaderText = "MR No";
                    this.gvcashbook.Columns[8].HeaderText = "Received From";
                    this.gvcashbook.Columns[9].HeaderText = "Received From(Clients)";
                    // this.gvcashbook.Columns[10].HeaderText = "Deposit";
                    break;
                case "Withdraw":                  
                    this.lblReceiptCash.Visible = true;
                    this.lblDetailsCash.Visible = true;
                    this.lblReceiptCash.Text = "Withdraw";
                    this.gvcashbook.Columns[2].HeaderText = "Cheque Date";
                    this.gvcashbook.Columns[3].HeaderText = "Issue #";
                    this.gvcashbook.Columns[8].HeaderText = "Party/Suppliers/Receivers Name";
                    this.gvcashbook.Columns[9].HeaderText = "Pay To";
                    // this.gvcashbook.Columns[10].HeaderText = "Deposit";
                    break;
            }

         

            Session["cashbank"] = ds1.Tables[0];

            dvr = ds1.Tables[0].DefaultView;
            dvr.RowFilter = ("grp1<>'F'");// ("grp1 = 'A' or grp1 = 'B' or grp1 = 'C' or grp1 = 'D' or grp1 = 'F'");
            DataTable dtr1 = HiddenSameDate(dvr.ToTable());
            this.gvcashbook.DataSource = dtr1;
            this.gvcashbook.DataBind();
            this.FooterCalculation(dtr1, "gvcashbook");


            dvr = ds1.Tables[0].DefaultView;
            dvr.RowFilter = ("grp1='F'");
            DataTable dtr3 = HiddenSameDate(dvr.ToTable());
            this.gvcashbookDB.DataSource = dtr3;
            this.gvcashbookDB.DataBind();
            this.FooterCalculation(dtr3, "gvcashbookDB");
            Session["Report1"] = gvcashbook;
            if (dtr1.Rows.Count > 0)
                ((HyperLink)this.gvcashbook.HeaderRow.FindControl("hlbtnbtbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        

        private void FooterCalculation(DataTable dt, string GvName)
        {
            if (dt.Rows.Count == 0)
                return;
            DataView dv = new DataView();
            string RptGroup = this.rbtnGroup.SelectedItem.Text.Trim();
            switch (GvName)
            {
                case "gvcashbook":

                    //switch (RptGroup)
                    //{
                    //    case "Deposit":
                    //        this.gvcashbook.Columns[10].HeaderText = "Deposit";
                    //        break;
                    //    case "Withdraw":
                    //        this.gvcashbook.Columns[10].HeaderText = "Withdraw";
                    //        break;
                    //    default:
                    //        this.gvcashbook.Columns[10].HeaderText = "";
                    //        break;
                    //}

                    break;
                case "gvcashbookDB":


                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lblgvFrecam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(depam)", "")) ?
                                   0 : dt.Compute("sum(depam)", ""))).ToString("#,##0;(#,##0) ;");

                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lgvFpayam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payam)", "")) ?
                             0 : dt.Compute("sum(payam)", ""))).ToString("#,##0;(#,##0) ;");


                    switch (RptGroup)
                    {
                        case "Deposit":
                            this.gvcashbookDB.Columns[3].Visible = false;
                            this.gvcashbookDB.Columns[2].Visible = true;
                            break;
                        case "Withdraw":
                            this.gvcashbookDB.Columns[2].Visible = false;
                            this.gvcashbookDB.Columns[3].Visible = true;
                            break;



                    }
                    break;
            }
        }

        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string Date1 = dt1.Rows[0]["voudat1"].ToString();
            string vounum = dt1.Rows[0]["vounum1"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                {
                    vounum = dt1.Rows[j]["vounum1"].ToString();
                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";
                    dt1.Rows[j]["vounar"] = "";
                }

                else
                {
                    vounum = dt1.Rows[j]["vounum1"].ToString();
                }
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
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text.ToString();
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void PrintCashBook02()
              {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string printType = rbtnGroup.SelectedValue;
            string frmDate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string toDate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string txtDate = "(From " + frmDate + " To " + toDate + ")";

            DataTable dt = (DataTable)Session["cashbank"];
            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptCashBank>();
 
            LocalReport Rpt1 = new LocalReport();


            if (printType == "Deposit")
            {


                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptCashBank", list, null, null);
                Rpt1.EnableExternalImages = true;
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptCashBankWithdraw", list, null, null);
                Rpt1.EnableExternalImages = true;
            }


            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", printFooter));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtDate));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Cash & Bank Balance"));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            /*
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //string RptGroup = ((this.rbtnGroup.SelectedItem.Text.Trim() == "Deposit") ? "R" : "P");
            //DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "RPTNETTRANSCASHBANK02", fromdate, todate, RptGroup, "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;

            //Session["cashbank"] = HiddenSameDate(ds1.Tables[0]);

            DataTable dt = (DataTable)Session["cashbank"];
            ReportDocument rptcb1 = new RealERPRPT.R_17_Acc.RptPostTrnsCashBanK02();
            TextObject rptCname = rptcb1.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptCname.Text = comnam;


            TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "(From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";
            TextObject IsueDate = rptcb1.ReportDefinition.ReportObjects["txtisudate"] as TextObject;
            IsueDate.Text = (this.rbtnGroup.SelectedIndex == 0) ? "Received Date" : "Cheque Date";

            TextObject Isuno = rptcb1.ReportDefinition.ReportObjects["txtisuno"] as TextObject;
            Isuno.Text = (this.rbtnGroup.SelectedIndex == 0) ? "MR No" : "Issue #";

            TextObject rptPartyName = rptcb1.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            rptPartyName.Text = (this.rbtnGroup.SelectedIndex == 0) ? "Received From" : "Party/Suppliers/Receivers Name";

            TextObject rptPaytoreceovedrfrm = rptcb1.ReportDefinition.ReportObjects["txtPayto"] as TextObject;
            rptPaytoreceovedrfrm.Text = (this.rbtnGroup.SelectedIndex == 0) ? "Received From(Clients)" : "Pay To";


            TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptcb1.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptcb1.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptcb1;
            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            Session["Report1"] = rptcb1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        */

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
                Label lblamt = (Label)e.Row.FindControl("txtgvDpAmt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grpsum")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code.Trim() == "AX" || code.Trim() == "AZ" || code.Trim() == "ZZ" || code.Trim() == "AB" || code.Trim() == "AD")
                {

                    lblpayto.Font.Bold = true;
                    lblamt.Font.Bold = true;
                    lblpayto.Style.Add("text-align", "right");
                }

            }
        }

    }
}
