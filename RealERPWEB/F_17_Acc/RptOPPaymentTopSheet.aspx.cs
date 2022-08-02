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
    public partial class RptOPPaymentTopSheet : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Project Summary Infow ";

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
            this.ShowData();
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



        private void ShowData()
        {
            int grpinded = this.rbtnGroup.SelectedIndex;
            
            switch (grpinded)
            {
                case 0:

                    this.ShowOPPayment();
                    break;
                case 1:

                    //this.ShowOPCollection();
                    break;


            }

        }


        private void ShowOPPayment()
        {
            Session.Remove("tbOpPay");

            string comcod = this.GetCompCode();
            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;
            string Type = ddlVaule.SelectedValue.ToString();
            //string Type = (this.Request.QueryString["Type"] == "HonourBasis") ? "HonourBasis" : "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "RPTOTALPPAYMENTSUMMARY", fdate, tdate, Type, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvpayment.DataSource = null;
                this.gvpayment.DataBind();
                return;
            }
            Session["tbOpPay"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "USER LOG DETAILS";
                string eventdesc = "Show Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string grp = dt1.Rows[0]["grp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {

                if (dt1.Rows[j]["grp"].ToString() == grp)
                {

                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }

                else
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                }
            }

            return dt1;

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbOpPay"];
            if (dt.Rows.Count < 0)
                return;
            int grpinded = this.rbtnGroup.SelectedIndex;

            switch (grpinded)
            {
                case 0:
                    //this.gvtbOpPay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvpayment.DataSource = dt;
                    this.gvpayment.DataBind();
                    //Session["Report1"] = gvpayment;
                    //((HyperLink)this.gvpayment.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;
                case 1:
                    //this.gvtbOpDep.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvpayment.DataSource = dt;
                    this.gvpayment.DataBind();
                    //Session["Report1"] = gvpayment;
                    //((HyperLink)this.gvpayment.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;
            }

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

        protected void gvpayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label actdesc = (Label)e.Row.FindControl("lgcType");
                Label Amount1 = (Label)e.Row.FindControl("lgvUp");
                //Label Amount2 = (Label)e.Row.FindControl("lgvbtween");
                //Label Amount3 = (Label)e.Row.FindControl("lgvAv");
                //Label Amount4 = (Label)e.Row.FindControl("lgvAv4");

                Label Amount = (Label)e.Row.FindControl("lgvtAmt");

                //Label CAmount = (Label)e.Row.FindControl("lgvCre");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rpcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "0000000")
                {
                    Amount.Font.Bold = true;

                    Amount.Style.Add("text-align", "Left");
                }

                else if (code == "0000AAA" || code == "0000BBB")
                {

                    actdesc.Attributes["style"] = "font-weight:bold; color:maroon;";
                }


                else if (code == "AAAAAAA" || code == "BBBBBBB")
                {
                    actdesc.Font.Bold = true;
                    Amount1.Font.Bold = true;


                    Amount.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");

                }
                else if (code == "BBBBAAA")
                {
                    actdesc.Font.Bold = true;
                    Amount1.Font.Bold = true;

                    Amount.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");


                }


                else if (code == "CCCCAAA")
                {
                    actdesc.Font.Bold = true;
                    Amount1.Font.Bold = true;
                    Amount.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");


                }
            }
        }

        
    }
}
