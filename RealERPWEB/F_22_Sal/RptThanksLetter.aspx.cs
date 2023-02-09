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

namespace RealERPWEB.F_22_Sal
{
    public partial class RptThanksLetter : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy ddd");
                if (!IsPostBack)
                {
                    string TypeDesc = "";
                    TypeDesc = Request.QueryString["Type"].ToString().Trim();
                    //this.lCTitle.Text = (TypeDesc == "Thanks" ? "THANKS LETTER " : (TypeDesc == "Down" ? "DOWN PAYMENT LETTER " : (TypeDesc == "Install" ? "INSTALLMENT LETTER " :(TypeDesc == "Dues" ? "DUES LETTER":"OTHERS")))) + " INFORMATIOIN VIEW/EDIT";
                    //((Label)this.Master.FindControl("lblTitle")).Text = (TypeDesc == "Thanks" ? "THANKS LETTER " : (TypeDesc == "Down" ? "DOWN PAYMENT LETTER " : (TypeDesc == "Install" ? "INSTALLMENT LETTER " : (TypeDesc == "Dues" ? "DUES LETTER" : (TypeDesc == "Remind" ? "REMINDER LETTER" : (TypeDesc == "RRemind" ? "LAST REMINDER LETTER" : "CANCELATION LETTER")))))) + " INFORMATIOIN VIEW/EDIT";

                    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                    ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                    this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                    ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                    //((Label)this.Master.FindControl("lblTitle")).Text = "REMINDER LETTER INFORMATIOIN";
                }

            }
            if (this.ddlProjectName.Items.Count == 0)
            {
                this.GetProjectName();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "GETSPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);




        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {

            string TypeDesc = "";
            TypeDesc = Request.QueryString["Type"].ToString().Trim();
            switch (TypeDesc)
            {
                case "Thanks":
                    this.ThankLetterInfo();
                    break;
                case "Down":
                    this.DownLetterInfo();
                    break;
                case "Install":
                    this.InstallmentInfo();
                    break;

                case "Dues":
                    this.DuesInfo();
                    break;
                case "Remind":
                case "LRemind":
                    this.RemindInfo();
                    break;
                case "Cancel":
                    this.CancellationInfo();
                    break;

            }

        }


        private void ThankLetterInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "REPORTTHANKSLETTER", pactcode, fromdate, "", "", "", "", "", "", "");
            this.ddlCustName.DataTextField = "usirdesc";
            this.ddlCustName.DataValueField = "usircode";
            this.ddlCustName.DataSource = ds1.Tables[0];
            this.ddlCustName.DataBind();

        }
        private void DownLetterInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "REPORTDOWNLETTER", pactcode, fromdate, "", "", "", "", "", "", "");
            this.ddlCustName.DataTextField = "usirdesc";
            this.ddlCustName.DataValueField = "usircode";
            this.ddlCustName.DataSource = ds1.Tables[0];
            this.ddlCustName.DataBind();


        }

        private void InstallmentInfo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTINSTALLMENTIFORMATION", pactcode, fromdate, "", "", "", "", "", "", "");
            this.ddlCustName.DataTextField = "usirdesc";
            this.ddlCustName.DataValueField = "usircode";
            this.ddlCustName.DataSource = ds1.Tables[0];
            this.ddlCustName.DataBind();

        }
        private void DuesInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "REPORTDUELETTER", pactcode, fromdate, "", "", "", "", "", "", "");
            this.ddlCustName.DataTextField = "usirdesc";
            this.ddlCustName.DataValueField = "usircode";
            this.ddlCustName.DataSource = ds1.Tables[0];
            this.ddlCustName.DataBind();


        }
        private void RemindInfo()
        {


            string type = (Request.QueryString["Type"].ToString().Trim() == "Remind") ? "2" : "3";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds11 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTREMAINDERLETTER", pactcode, fromdate, type, "", "", "", "", "", "");
            this.ddlCustName.DataTextField = "usirdesc";
            this.ddlCustName.DataValueField = "usircode";
            this.ddlCustName.DataSource = ds11.Tables[0];
            this.ddlCustName.DataBind();

        }

        private void CancellationInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds11 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTCANCELLATIONLETTER", pactcode, fromdate, "", "", "", "", "", "", "");
            this.ddlCustName.DataTextField = "usirdesc";
            this.ddlCustName.DataValueField = "usircode";
            this.ddlCustName.DataSource = ds11.Tables[0];
            this.ddlCustName.DataBind();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string TypeDesc = Request.QueryString["Type"].ToString().Trim();
            switch (TypeDesc)
            {
                case "Thanks":
                    this.PrintThanksLetterInfo();
                    break;
                case "Down":
                    this.PrintDownPaymentInfo();
                    break;
                case "Install":
                    this.PrintInstallmentInfo();
                    break;
                case "Dues":
                    this.PrintDuesLetter();
                    break;
                case "Remind":
                    this.PrintReminderLetterInfo();
                    break;
                case "LRemind":
                    this.PrintLReminderLetterInfo();
                    break;

                case "Cancel":
                    this.PrintCancellationLetter();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report";
                string eventdesc2 = TypeDesc;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }



        private void PrintDownPaymentInfo()

        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.ddlCustName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTCUSTDOWNINFORMATION", pactcode, usircode, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];
            string sdate = dt.Rows[0]["schdate"].ToString() == "01-jan-1900" ? "" : Convert.ToDateTime(dt.Rows[0]["schdate"]).ToString("MMMM dd, yyyy");
            string Pactdesc = (this.ddlProjectName.SelectedItem.Text).Substring(13);
            string usirdesc = dt.Rows[0]["udesc"].ToString();
            double schamt = Convert.ToDouble(dt.Rows[0]["schamt"]) == 0 ? 0.00 : Convert.ToDouble(dt.Rows[0]["schamt"]);
            string paidamt1 = ASTUtility.Trans(schamt, 2);
            double damt = Convert.ToDouble(dt.Rows[0]["damt"]) == 0 ? 0.00 : Convert.ToDouble(dt.Rows[0]["damt"]);
            string damt1 = ASTUtility.Trans(damt, 2);
            string ddat = dt.Rows[0]["downdat"].ToString() == "01-jan-1900" ? "" : Convert.ToDateTime(dt.Rows[0]["downdat"]).ToString("MMMM dd, yyyy");
            string Aptname = ((comcod.Substring(0, 1) == "3") ? " apartment" : ((comcod.Substring(0, 1) == "2") ? " plot" : "apartment"));
            string AptNo = ((comcod.Substring(0, 1) == "3") ? "Apt.No. " : ((comcod.Substring(0, 1) == "2") ? " Plot No. " : "apartment"));
            ReportDocument rptdletter = new RealERPRPT.R_22_Sal.RptDown_paymentLetter();
            TextObject rptProjectName = rptdletter.ReportDefinition.ReportObjects["Compwell"] as TextObject;

            rptProjectName.Text = "Request to pay your down payment against" + AptNo + usircode + " (" + usirdesc + ") " + this.ddlProjectName.SelectedItem.Text.Substring(13);
            TextObject rptdate = rptdletter.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "Date: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM dd, yyyy");
            TextObject rptPara1 = rptdletter.ReportDefinition.ReportObjects["pare1"] as TextObject;
            rptPara1.Text = "We are very happy to have you as a valued client of our project " + Pactdesc + " at " + sdate + " . " + " For your kind information, you have already paid Tk. " + schamt + " " + paidamt1 + "  only against the agreed value of your allotted" + Aptname + " as booking money. According to the Payment schedule, the date of your down payment is on " + ddat + " amounting Tk. " + damt + " " + damt1 + "  .";

            TextObject rptPara2 = rptdletter.ReportDefinition.ReportObjects["pare2"] as TextObject;
            rptPara2.Text = "So, you are reqested to pay the said amount on or before " + ddat + " to avoid cancellation of booking as per primary agreement.";

            rptdletter.SetDataSource(ds1.Tables[0]);
            Session["Report1"] = rptdletter;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        //private void PrintThanksLetterInfo()
        //{

        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
        //    string pactcode = this.ddlProjectName.SelectedValue.ToString();
        //    string usircode = this.ddlCustName.SelectedValue.ToString();
        //    DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTCUSTDOWNINFORMATION", pactcode, usircode, "", "", "", "", "", "", "");
        //    if (ds2 == null)
        //        return;
        //    string sdate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["schdate"]).ToString("MMMM dd, yyyy");
        //    string Pactdesc = (this.ddlProjectName.SelectedItem.Text).Substring(13);
        //    string Aptname = ((comcod.Substring(0, 1) == "3") ? "an apartment" : ((comcod.Substring(0, 1) == "2") ? "a plot" : "an apartment"));
        //    string para2 = ((comcod == "2101") ? "" : comnam + " is highly commited to complete the project within the stipulated time. We shall use higest quality bulding materials and finished products to ensure durability and satisfaction. Keeping emphasis on earthquake resistant sound structure as per BNBC(Bangladesh National Building Code 1993) and visual design are our main focus which will be on the Quality of the apartment.");
        //    ReportDocument rptletter = new RealERPRPT.R_22_Sal.RptThanksLetter11();

        //    TextObject rptProjectName = rptletter.ReportDefinition.ReportObjects["Compwell"] as TextObject;

        //    rptProjectName.Text = "Welcome to  " + '"' + " " + comnam + "  Familly " + '"';
        //    TextObject rptdate = rptletter.ReportDefinition.ReportObjects["date"] as TextObject;
        //    rptdate.Text = "Date: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM dd, yyyy");
        //    TextObject rptPara1 = rptletter.ReportDefinition.ReportObjects["pare1"] as TextObject;
        //    rptPara1.Text = "We are extremly glad to have you as a valued member of " + '"' + comnam + '"' + " family. We would like to congratulate "
        //                    + "you for booking " + Aptname + " in " + '"' + Pactdesc + '"' + " at " + sdate;
        //    TextObject rptPara2 = rptletter.ReportDefinition.ReportObjects["pare2"] as TextObject;
        //    rptPara2.Text = para2;
        //    rptletter.SetDataSource(ds2.Tables[0]);
        //    Session["Report1"] = rptletter;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //}


        private void PrintThanksLetterInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.ddlCustName.SelectedValue.ToString();
            string Pactdesc = (this.ddlProjectName.SelectedItem.Text).Substring(13);
            string Aptname = ((comcod.Substring(0, 1) == "3") ? "an apartment" : ((comcod.Substring(0, 1) == "2") ? "a plot" : "an apartment"));
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTCUSTDOWNINFORMATION", pactcode, usircode, "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            string sdate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["schdate"]).ToString("MMMM dd, yyyy")??"";
            string title = "THANKS LETTER";
            string name = ds2.Tables[0].Rows[0]["custname"].ToString() ?? "";
            string phone = "Phone: "+ds2.Tables[0].Rows[0]["phone"].ToString() ?? "";
            string address = "Address: "+ds2.Tables[0].Rows[0]["cusadd"].ToString() ?? "";
            string subject = "Welcome to  " + '"' + " " + comnam + "  Familly " + '"';
            string salutation = "Dear Sir,";
            string greetings = "Assalamu Alaikum";
            string startline = "We are extremly glad to have you as a valued member of " + '"' + comnam + '"' + " family. We would like to congratulate "
                  + "you for booking " + Aptname + " in " + '"' + Pactdesc + '"' + " at " + sdate;
            string body = ((comcod == "2101") ? "" : comnam + " is highly commited to complete the project within the stipulated time. We shall use higest quality bulding materials and finished products to ensure durability and satisfaction. Keeping emphasis on earthquake resistant sound structure as per BNBC(Bangladesh National Building Code 1993) and visual design are our main focus which will be on the Quality of the apartment.");

            string clouserBefor = "We desire your continuous cooperation to make this commitment a success.";
            string closure = "Thank you, once again for being with us and we are always ready to give our best services.";

            LocalReport Rpt1 = new LocalReport();
 
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_21_MKT.ClientLetter", null, null, null);
                Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("title", title));
            Rpt1.SetParameters(new ReportParameter("sdate", sdate));
            Rpt1.SetParameters(new ReportParameter("name", name));
            Rpt1.SetParameters(new ReportParameter("phone", phone));
            Rpt1.SetParameters(new ReportParameter("address", address));
            Rpt1.SetParameters(new ReportParameter("subject", subject));
            Rpt1.SetParameters(new ReportParameter("salutation", salutation));
            Rpt1.SetParameters(new ReportParameter("greetings", greetings));

            Rpt1.SetParameters(new ReportParameter("startline", startline));
            Rpt1.SetParameters(new ReportParameter("body", body));
            Rpt1.SetParameters(new ReportParameter("closureBefore", clouserBefor));
            Rpt1.SetParameters(new ReportParameter("closure", closure));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


            private void PrintInstallmentInfo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.ddlCustName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtfromdate.Text).ToString();
            string type = "Ins";
            DataSet ds3 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTCUSTINSINFORMATION", pactcode, usircode, date, type, "", "", "", "", "");
            if (ds3 == null)
                return;
            DataTable dt = ds3.Tables[0];
            string sdate = Convert.ToDateTime(ds3.Tables[0].Rows[0]["schdate"]).ToString("MMMM dd, yyyy");
            string Pactdesc = (this.ddlProjectName.SelectedItem.Text).Substring(13);
            string usirdesc = dt.Rows[0]["udesc"].ToString();
            double schamt = Convert.ToDouble(dt.Rows[0]["paidamt"]);
            string paidamt1 = ASTUtility.Trans(schamt, 2);
            double damt = Convert.ToDouble(dt.Rows[0]["damt"]);
            string damt1 = ASTUtility.Trans(damt, 2);
            string ddat = Convert.ToDateTime(dt.Rows[0]["indate"]).ToString("MMMM dd, yyyy");
            ReportDocument rptdletter = new RealERPRPT.R_22_Sal.RptInstallmenttLetter();
            string Aptname = ((comcod.Substring(0, 1) == "3") ? " apartment" : ((comcod.Substring(0, 1) == "2") ? " plot" : "apartment"));
            string AptNo = ((comcod.Substring(0, 1) == "3") ? " Apt.No. " : ((comcod.Substring(0, 1) == "2") ? " Plot No. " : "apartment"));

            //TextObject txtCompanyName = rptdletter.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //txtCompanyName.Text = comnam;

            string custname = Convert.ToString(dt.Rows[0]["custname"]);
            TextObject custname1 = rptdletter.ReportDefinition.ReportObjects["custname1"] as TextObject;
            custname1.Text = custname;

            TextObject custadd1 = rptdletter.ReportDefinition.ReportObjects["custadd1"] as TextObject;
            custadd1.Text = Convert.ToString(dt.Rows[0]["custadd"]);


            TextObject rptProjectName = rptdletter.ReportDefinition.ReportObjects["Compwell"] as TextObject;
            rptProjectName.Text = "Request to pay your Installment against" + AptNo + usircode + " (" + usirdesc + ") " + this.ddlProjectName.SelectedItem.Text.Substring(13);

            TextObject rptdate = rptdletter.ReportDefinition.ReportObjects["Date"] as TextObject;
            rptdate.Text = "Date: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM dd, yyyy");

            TextObject rptPara1 = rptdletter.ReportDefinition.ReportObjects["pare1"] as TextObject;
            rptPara1.Text = "We are very happy to have you as a valued client of our project " + Pactdesc + " at " + sdate + " . " + " For your kind information, you have already paid Tk. " + schamt.ToString("#,##0;(#,##0); ") + " " + paidamt1 + "  only against the agreed value of your allotted" + Aptname + ". According to the Payment schedule, the date of your installment payment is on " + ddat + " amounting Tk. " + damt.ToString("#,##0;(#,##0); ") + " " + damt1 + "  .";

            TextObject rptPara2 = rptdletter.ReportDefinition.ReportObjects["pare2"] as TextObject;
            rptPara2.Text = "So, you are requested to pay the installment amount in due time. If you unable to pay the installment by the scheduled time, there is a provision of delay charge which will be added with principle amount.";

            rptdletter.SetDataSource(ds3.Tables[0]);
            Session["Report1"] = rptdletter;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintDuesLetter()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.ddlCustName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtfromdate.Text).ToString();
            string type = "";
            DataSet ds4 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTCUSTINSINFORMATION", pactcode, usircode, date, type, "", "", "", "", "");
            if (ds4 == null)
                return;
            DataTable dt = ds4.Tables[0];
            string sdate = Convert.ToDateTime(ds4.Tables[0].Rows[0]["schdate"]).ToString("MMMM dd, yyyy");
            string Pactdesc = (this.ddlProjectName.SelectedItem.Text).Substring(13);
            string usirdesc = dt.Rows[0]["udesc"].ToString();
            double schamt = Convert.ToDouble(dt.Rows[0]["paidamt"]);
            string paidamt1 = ASTUtility.Trans(schamt, 2);
            double damt = Convert.ToDouble(dt.Rows[0]["damt"]);
            string damt1 = ASTUtility.Trans(damt, 2);
            string ddat = Convert.ToDateTime(dt.Rows[0]["indate"]).ToString("MMMM dd, yyyy");
            //double delaycharge = schamt-damt;
            //string delaycharge1 = ASTUtility.Trans(delaycharge, 2);
            string Aptname = ((comcod.Substring(0, 1) == "3") ? " apartment" : ((comcod.Substring(0, 1) == "2") ? " plot" : "apartment"));
            string AptNo = ((comcod.Substring(0, 1) == "3") ? " Apt.No. " : ((comcod.Substring(0, 1) == "2") ? " Plot No. " : " apartment"));

            ReportDocument rdl = new RealERPRPT.R_22_Sal.RptDuesLetter();
            string custname = Convert.ToString(dt.Rows[0]["custname"]);
            TextObject custname1 = rdl.ReportDefinition.ReportObjects["custname1"] as TextObject;
            custname1.Text = custname;

            TextObject custadd1 = rdl.ReportDefinition.ReportObjects["custadd1"] as TextObject;
            custadd1.Text = Convert.ToString(dt.Rows[0]["custadd"]);

            TextObject rptProjectName = rdl.ReportDefinition.ReportObjects["Compwell"] as TextObject;
            rptProjectName.Text = "Request to pay your Installment against" + AptNo + " " + usirdesc + " " + this.ddlProjectName.SelectedItem.Text.Substring(13);

            TextObject rptdate = rdl.ReportDefinition.ReportObjects["Date"] as TextObject;
            rptdate.Text = "Date: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM dd,yyyy");

            TextObject rptPara1 = rdl.ReportDefinition.ReportObjects["pare1"] as TextObject;
            rptPara1.Text = "We are very happy to have you as a valued client of our project " + Pactdesc + " at " + sdate + "." + " For your kind information, you have already paid Tk. " + schamt.ToString("#,##0;(#,##0); ") + " " + paidamt1 + "  only against the agreed value of your allotted" + Aptname + ". According to the Payment schedule, the date of your last installment was on " + ddat + " amounting Tk. " + damt.ToString("#,##0;(#,##0); ") + " " + damt1 + " . But you have failed to pay the installment by the scheduled time. Now your due amount is Tk. " + damt.ToString("#,##0;(#,##0); ") + damt1 + " only with delay charge. You are requested to pay the above mentioned amount as soon as possible.";

            rdl.SetDataSource(ds4.Tables[0]);
            Session["Report1"] = rdl;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintReminderLetterInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.ddlCustName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds5 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTCUSTINSINFORMATION", pactcode, usircode, date, "", "", "", "", "", "");
            if (ds5 == null)
                return;
            DataTable dt = ds5.Tables[0];
            string sdate = Convert.ToDateTime(ds5.Tables[0].Rows[0]["schdate"]).ToString("MMMM dd, yyyy");
            string Pactdesc = (this.ddlProjectName.SelectedItem.Text).Substring(13);
            string usirdesc = dt.Rows[0]["udesc"].ToString();
            double schamt = Convert.ToDouble(dt.Rows[0]["paidamt"]);
            string paidamt1 = ASTUtility.Trans(schamt, 2);
            double damt = Convert.ToDouble(dt.Rows[0]["damt"]);
            string damt1 = ASTUtility.Trans(damt, 2);
            string ddat = Convert.ToDateTime(dt.Rows[0]["indate"]).ToString("MMMM dd, yyyy");
            string Aptname = ((comcod.Substring(0, 1) == "3") ? " apartment" : ((comcod.Substring(0, 1) == "2") ? " plot" : "apartment"));
            string AptNo = ((comcod.Substring(0, 1) == "3") ? " Apt.No. " : ((comcod.Substring(0, 1) == "2") ? " Plot No. " : "apartment"));

            ReportDocument rdl = new RealERPRPT.R_22_Sal.RptReminderLetter();

            string custname = Convert.ToString(dt.Rows[0]["custname"]);
            TextObject custname1 = rdl.ReportDefinition.ReportObjects["custname1"] as TextObject;
            custname1.Text = custname;

            TextObject custadd1 = rdl.ReportDefinition.ReportObjects["custadd1"] as TextObject;
            custadd1.Text = Convert.ToString(dt.Rows[0]["custadd"]);

            TextObject rptProjectName = rdl.ReportDefinition.ReportObjects["Compwell"] as TextObject;
            rptProjectName.Text = "Request to pay your total dues installment against" + AptNo + usircode + " (" + usirdesc + ") " + this.ddlProjectName.SelectedItem.Text.Substring(13);

            TextObject rptdate = rdl.ReportDefinition.ReportObjects["Date"] as TextObject;
            rptdate.Text = "Date: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM dd,yyyy");

            TextObject rptPara1 = rdl.ReportDefinition.ReportObjects["pare1"] as TextObject;
            rptPara1.Text = "We are very happy to have you as a valued client of our project " + Pactdesc + " at " + sdate + "." + " For your kind information, you have already paid Tk. " + schamt.ToString("#,##0;(#,##0); ") + " " + paidamt1 + "  only against the agreed value of your allotted" + Aptname + ". According to the Payment schedule, the date of your last installment was on " + ddat + " amounting Tk. " + damt.ToString("#,##0;(#,##0); ") + " " + damt1 + " is still unpaid. Now your due amount is Tk. " + damt.ToString("#,##0;(#,##0); ") + " " + damt1 + " with delay charge. In this connection we have already given you a reminder but unfortunately, you didn't co-operate with us.";

            rdl.SetDataSource(ds5.Tables[0]);
            Session["Report1"] = rdl;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintLReminderLetterInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.ddlCustName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds5 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTCUSTINSINFORMATION", pactcode, usircode, date, "", "", "", "", "", "");
            if (ds5 == null)
                return;
            DataTable dt = ds5.Tables[0];
            string sdate = Convert.ToDateTime(ds5.Tables[0].Rows[0]["schdate"]).ToString("MMMM dd, yyyy");
            string Pactdesc = (this.ddlProjectName.SelectedItem.Text).Substring(13);
            string usirdesc = dt.Rows[0]["udesc"].ToString();
            double schamt = Convert.ToDouble(dt.Rows[0]["paidamt"]);
            string paidamt1 = ASTUtility.Trans(schamt, 2);
            double damt = Convert.ToDouble(dt.Rows[0]["damt"]);
            string damt1 = ASTUtility.Trans(damt, 2);
            string ddat = Convert.ToDateTime(dt.Rows[0]["indate"]).ToString("MMMM dd, yyyy");
            string Aptname = ((comcod.Substring(0, 1) == "3") ? " apartment" : ((comcod.Substring(0, 1) == "2") ? " plot" : "apartment"));
            string AptNo = ((comcod.Substring(0, 1) == "3") ? " Apt.No. " : ((comcod.Substring(0, 1) == "2") ? " Plot No. " : "apartment"));
            ReportDocument rdl = new RealERPRPT.R_22_Sal.RptLastReminderLetter();


            string custname = dt.Rows[0]["custname"].ToString();
            TextObject custname1 = rdl.ReportDefinition.ReportObjects["custname1"] as TextObject;
            custname1.Text = custname;

            TextObject custadd1 = rdl.ReportDefinition.ReportObjects["custadd1"] as TextObject;
            custadd1.Text = dt.Rows[0]["custadd"].ToString();

            TextObject rptProjectName = rdl.ReportDefinition.ReportObjects["Compwell"] as TextObject;
            rptProjectName.Text = "Request to pay your total dues installment against" + Aptname + usircode + " (" + usirdesc + ") " + this.ddlProjectName.SelectedItem.Text.Substring(13);

            TextObject rptdate = rdl.ReportDefinition.ReportObjects["Date"] as TextObject;
            rptdate.Text = "Date: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM dd,yyyy");

            TextObject rptPara1 = rdl.ReportDefinition.ReportObjects["para1"] as TextObject;
            rptPara1.Text = "We are very happy to have you as a valued client of our project " + Pactdesc + " at " + sdate + " . " + " For your kind information, you have already paid Tk. " + schamt.ToString("#,##0;(#,##0); ") + " " + paidamt1 + " only against the agreed value of your allotted" + Aptname + ". According to the Payment schedule, you have failed to pay your last installment dated " + sdate + "in due time. " + "Now your due amount is Tk. " + damt.ToString("#,##0;(#,##0); ") + " " + damt1 + " with delay charge. " + " In this connection we have already given you two letters before where we requested yu to pay all the dues against your allotted" + Aptname + " but unfortunately, you have failed to co-operate accordingly. So you are once again requested to clear all the dues mentioned above on or before " + sdate;
            TextObject rptPara2 = rdl.ReportDefinition.ReportObjects["para2"] as TextObject;
            rptPara2.Text = "Please note that, if you fail to pay your total dues Tk. " + damt.ToString("#,##0;(#,##0); ") + " " + damt1 + " only within the stipulated time, we will have no other option but cancel the booking of your allotted" + Aptname + ".";
            rdl.SetDataSource(ds5.Tables[0]);
            Session["Report1"] = rdl;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintCancellationLetter()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //string usircode = this.ddlCustName.SelectedValue.ToString();
            //string date = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //DataSet ds6 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTCUSTINSINFORMATION", pactcode, usircode, date, "", "", "", "", "", "");
            //if (ds6 == null)
            //    return;
            //DataTable dt = ds6.Tables[0];
            //string sdate = Convert.ToDateTime(ds6.Tables[0].Rows[0]["schdate"]).ToString("MMMM dd, yyyy");
            //string Pactdesc = (this.ddlProjectName.SelectedItem.Text).Substring(13);
            //string usirdesc = dt.Rows[0]["udesc"].ToString();
            //double schamt = Convert.ToDouble(dt.Rows[0]["paidamt"]);
            //string paidamt1 = ASTUtility.Trans(schamt, 2);
            //double damt = Convert.ToDouble(dt.Rows[0]["damt"]);
            //string damt1 = ASTUtility.Trans(damt, 2);
            //string ddat = Convert.ToDateTime(dt.Rows[0]["indate"]).ToString("MMMM dd, yyyy");
            //double delaycharge = schamt - damt;
            //string delaycharge1 = ASTUtility.Trans(delaycharge, 2);
            //string Aptname = ((comcod.Substring(0, 1) == "3") ? " apartment" : ((comcod.Substring(0, 1) == "2") ? " plot" : " apartment"));
            //string AptNo = ((comcod.Substring(0, 1) == "3") ? " Apt.No. " : ((comcod.Substring(0, 1) == "2") ? " Plot No. " : " Apt.No. "));
            //ReportDocument rdl = new RealERPRPT.R_22_Sal.RptCancellationLetter();



            //string custname = Convert.ToString(dt.Rows[0]["custname"]);
            //TextObject custname1 = rdl.ReportDefinition.ReportObjects["custname1"] as TextObject;
            //custname1.Text = custname;

            //TextObject custadd1 = rdl.ReportDefinition.ReportObjects["custadd1"] as TextObject;
            //custadd1.Text = Convert.ToString(dt.Rows[0]["custadd"]);

            //TextObject rptProjectName = rdl.ReportDefinition.ReportObjects["Compwell"] as TextObject;
            //rptProjectName.Text = "Cancellation of your booking against" + AptNo + usircode + " (" + usirdesc + ") " + this.ddlProjectName.SelectedItem.Text.Substring(13);

            //TextObject rptdate = rdl.ReportDefinition.ReportObjects["Date"] as TextObject;
            //rptdate.Text = "Date: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM dd,yyyy");

            ////TextObject rptPara1 = rdl.ReportDefinition.ReportObjects["pare1"] as TextObject;
            ////rptPara1.Text = "We are very happy to have you as a valued client of our project " + Pactdesc + " at " + sdate + "." + " For your kind information, you have already paid Tk " + schamt.ToString("#,##0.00;(#,##0.00); ") + " " + paidamt1 + "  only against the agreed value of your allotted apartment . According to the Payment schedule, the date of your last installment was on " + ddat + " amounting Tk  " + damt.ToString("#,##0.00;(#,##0.00); ") + " " + damt1 + " is still unpaid. Now your due amount is Tk " + delaycharge.ToString("#,##0.00;(#,##0.00); ") + " " + delaycharge1 + "with delay charge. In this connection we have already given you a reminder but unfortunately, you didn't co-operate with us.";

            //TextObject rptPara1 = rdl.ReportDefinition.ReportObjects["pare1"] as TextObject;
            //rptPara1.Text = "We draw your kind attention on our letters above reference, wherein we requested you to pay your dues Tk. " + damt.ToString("#,##0;(#,##0))") + " " + damt1 + " only against your allotted" + Aptname + ". We also requested you over phone to pay the dues but unfortunately we didn't get any positive response from your end.";

            //TextObject pare2 = rdl.ReportDefinition.ReportObjects["pare2"] as TextObject;
            //pare2.Text = "It is to be noted that, in terms of your schedule of payment for the" + Aptname + ", you have become over due defaulter. You have failed to pay the dues Tk. " + damt.ToString("#,##0;(#,##0))") + " " + damt1 + " despite our repeated request for payment of your dues. In the light of above facts, your booking for" + Aptname + " is liable to be cancelled and accordingly the authority of the company has cancelled the booking today.";

            //rdl.SetDataSource(ds6.Tables[0]);
            //Session["Report1"] = rdl;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


      
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.ddlCustName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTCUSTINSINFORMATION", pactcode, usircode, date, "", "", "", "", "", "");
            if (ds2 == null)
                return;

            DataTable dt = ds2.Tables[0];
          
            string Pactdesc = (this.ddlProjectName.SelectedItem.Text).Substring(13);
            string usirdesc = dt.Rows[0]["udesc"].ToString();
            double schamt = Convert.ToDouble(dt.Rows[0]["paidamt"]);
            string paidamt1 = ASTUtility.Trans(schamt, 2);
            double damt = Convert.ToDouble(dt.Rows[0]["damt"]);
            string damt1 = ASTUtility.Trans(damt, 2);
            string ddat = Convert.ToDateTime(dt.Rows[0]["indate"]).ToString("MMMM dd, yyyy")??System.DateTime.Now.ToString("MMMM dd, yyyy");
            double delaycharge = schamt - damt;
            string delaycharge1 = ASTUtility.Trans(delaycharge, 2);
            string Aptname = ((comcod.Substring(0, 1) == "3") ? " apartment" : ((comcod.Substring(0, 1) == "2") ? " plot" : " apartment"));
            string AptNo = ((comcod.Substring(0, 1) == "3") ? " Apt.No. " : ((comcod.Substring(0, 1) == "2") ? " Plot No. " : " Apt.No. "));
            string sdate = "Date:"+Convert.ToDateTime(ds2.Tables[0].Rows[0]["schdate"]).ToString("MMMM dd, yyyy") ?? System.DateTime.Now.ToString("MMMM dd, yyyy");
     

            string title = "CANCELLATION LETTER";
            string name = ds2.Tables[0].Rows[0]["custname"].ToString() ?? "";
            string phone = "Phone: " + ds2.Tables[0].Rows[0]["phone"].ToString() ?? "";
            string address = "Address: " + ds2.Tables[0].Rows[0]["cusadd"].ToString() ?? "";
            string subject = "Welcome to  " + '"' + " " + comnam + "  Familly " + '"';
            string salutation = "Dear Sir,";
            string greetings = "Assalamu Alaikum,";
            string startline = "We draw your kind attention on our letters above reference, wherein we requested you to pay your dues Tk. " + damt.ToString("#,##0;(#,##0))") + " " + damt1 + " only against your allotted" + Aptname + ". We also requested you over phone to pay the dues but unfortunately we didn't get any positive response from your end.";
            string body = "It is to be noted that, in terms of your schedule of payment for the" + Aptname + ", you have become over due defaulter. You have failed to pay the dues Tk. " + damt.ToString("#,##0;(#,##0))") + " " + damt1 + " despite our repeated request for payment of your dues. In the light of above facts, your booking for" + Aptname + " is liable to be cancelled and accordingly the authority of the company has cancelled the booking today.";

            string clouserBefor = "You are further informed that your refundable money will be paid within 90 days from the date of the cancellation after deducting the service charges.";
            string closure = "Thank you,";

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_21_MKT.ClientLetter", null, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("title", title));
            Rpt1.SetParameters(new ReportParameter("sdate", sdate));
            Rpt1.SetParameters(new ReportParameter("name", name));
            Rpt1.SetParameters(new ReportParameter("phone", phone));
            Rpt1.SetParameters(new ReportParameter("address", address));
            Rpt1.SetParameters(new ReportParameter("subject", subject));
            Rpt1.SetParameters(new ReportParameter("salutation", salutation));
            Rpt1.SetParameters(new ReportParameter("greetings", greetings));

            Rpt1.SetParameters(new ReportParameter("startline", startline));
            Rpt1.SetParameters(new ReportParameter("body", body));
            Rpt1.SetParameters(new ReportParameter("closureBefore", clouserBefor));
            Rpt1.SetParameters(new ReportParameter("closure", closure));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }



    }
}










