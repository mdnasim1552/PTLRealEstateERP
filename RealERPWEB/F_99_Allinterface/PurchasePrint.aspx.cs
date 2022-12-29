using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using Microsoft.Reporting.WinForms;
using System.IO;
using RealERPLIB;
using RealERPRPT;
using System.Reflection;
using RealERPRDLC;
using EASendMail;
using System.Net.Mail;

namespace RealERPWEB.F_99_Allinterface
{
    public partial class PurchasePrint : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {


            //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //string centrid = ASTUtility.Left(code, 12);
            //string orderno = ASTUtility.Right(code, 14);
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ReqPrint":
                    this.PrintRequisition();
                    //Dev change
                    //Dev change
                    break;

                //case "ReqPrintRDLC":
                //    this.PrintRequisitionRDLC();
                //    break;
                case "ReqFinalAppPrint":
                    this.PrintFinalAppRequisition();
                    break;
                case "OrderPrint":
                    this.Order_Print();
                    break;
                case "OrderPrintNew":
                    this.OrderPrintRDLC();
                    break;
                case "BillPrint":
                    this.PurBill_Print();
                    break;

                case "SubConBillReq":
                    this.printSubConReqBillP2p();
                    break;

                case "ConBillPrint":
                    this.PurConBill_Print();
                    break;
                case "ConBillFinalization":
                    this.PurConBillFinal_Print();
                    break;

                case "PurApproval":
                    this.PurApprovalPrint();
                    break;

                case "MRReceipt":
                    this.printMRReceipt();
                    break;

                //Marketing Procurement Print
                case "MktReqPrint":
                    this.MktReqPrint();
                    break;

                case "MktCSPrint":
                    this.MktCSPrint();
                    break;

                case "MktOrderPrint":
                    this.MktOrderPrint();
                    break;

                case "MktMRRPrint":
                    this.MktMRRPrint();
                    break;

                default:
                    break;
            }


        }

        private string ReadCookie()
        {
            HttpCookie nameCookie = Request.Cookies["MRF"];
            string refno = nameCookie != null ? nameCookie.Value.Split('=')[1] : "Mrf No";
            return refno;
        }

        private void printSubConReqBillP2p()
        {
            string comcod = this.GetCompCode();
            string reqno = this.Request.QueryString["lisuno"].ToString();
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string CurDate1 = Convert.ToDateTime(System.DateTime.Now.ToString()).ToString("dd-MMM-yyyy");

            DataSet ds1 = new DataSet();
            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT02", "GET_PURLAB_REQ_INFO", reqno, CurDate1,
                         pactcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblmatissue2"] = ds1.Tables[0];
            ViewState["tblbillinfo1"] = ds1.Tables[1];
            ViewState["tblsign"] = ds1.Tables[3];
            this.SubConReqBillP2pprint();

        }

        private void SubConReqBillP2pprint()
        {
            DataTable dt1 = (DataTable)ViewState["tblbillinfo1"];
            DataTable dt2 = (DataTable)ViewState["tblsign"];
            string refno = dt1.Rows[0]["lreqno"].ToString();
            string pactdesc = dt1.Rows[0]["pactdesc"].ToString();
            string csirdsec = dt1.Rows[0]["psirdesc"].ToString();
            string lisuno1 = dt1.Rows[0]["lreqno1"].ToString();
            string isudat = Convert.ToDateTime(dt1.Rows[0]["reqdat"]).ToString("dd-MMM-yyyy");
            string rano = dt1.Rows[0]["refno"].ToString();
            string rmrks = dt1.Rows[0]["rmrks"].ToString();


            DataTable dt = (DataTable)ViewState["tblmatissue2"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string txtsign1 = dt2.Rows[0]["USRNAME"].ToString() + "\n" + dt2.Rows[0]["USRDESIG"].ToString() + "\n" + Convert.ToDateTime(dt2.Rows[0]["REQDAT"]).ToString("dd-MMM-yyyy");

            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.SubConBill.SubConBillReq>();
            var TAmt = lst.Select(p => p.isuamt).Sum();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptSubconbillreqP2p", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Sub Contractor Bill (R/A Wise)"));
            Rpt1.SetParameters(new ReportParameter("ProjectName", "Project Name: " + pactdesc));
            Rpt1.SetParameters(new ReportParameter("txtSubConNam", "Sub Contractor Name: " + csirdsec));
            Rpt1.SetParameters(new ReportParameter("Issueno", "Issue No: " + lisuno1));
            Rpt1.SetParameters(new ReportParameter("txtRefno", "Bill Ref. No: " + refno));
            Rpt1.SetParameters(new ReportParameter("date", "Date: " + isudat));
            Rpt1.SetParameters(new ReportParameter("narrationname", rmrks));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("txtsign1", txtsign1));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }

        private void printMRReceipt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string mMRRNo = this.Request.QueryString["mrno"].ToString();
            string ssircode = this.Request.QueryString["sircode"].ToString();

            //string suppliername = this.Request.QueryString["supname"].ToString();
            //string prjname = this.Request.QueryString["prjname"].ToString();

            string CurDate1 = System.DateTime.Now.ToString("dd-MMM-yyyy");
            //string CurDate1 = this.GetStdDate(this.txtCurMRRDate.Text.Trim());
            // string mMRRNo = this.lblCurMRRNo1.Text.Trim().Substring(0, 3) + this.txtCurMRRDate.Text.Trim().Substring(6, 4) + this.lblCurMRRNo1.Text.Trim().Substring(3, 2) + this.txtCurMRRNo2.Text.Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURMRRINFO", mMRRNo, CurDate1, "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            DataTable dt = this.HiddenSameDataMRRReceipt(ds1.Tables[0]);

            string mrrno1 = ds1.Tables[1].Rows[0]["mrrno1"].ToString();
            string mrrno = ds1.Tables[1].Rows[0]["mrrno"].ToString();
            string address = ds1.Tables[1].Rows[0]["address"].ToString();
            string chlandate = ds1.Tables[1].Rows[0]["challandat"].ToString();
            string pordar = ds1.Tables[1].Rows[0]["orderno"].ToString();
            string orderdat = ds1.Tables[1].Rows[0]["orderdat"].ToString(); 
            string porno = ds1.Tables[1].Rows[0]["orderno1"].ToString();
            string prjname = ds1.Tables[1].Rows[0]["pactdesc1"].ToString();
            string suppliername = ds1.Tables[1].Rows[0]["ssirdesc1"].ToString();
            string clndate = Convert.ToDateTime(chlandate).ToString("dd-MMM-yyyy");
            string mrdate = Convert.ToDateTime(ds1.Tables[1].Rows[0]["mrrdat"]).ToString("dd-MMM-yyyy");

            // DataTable dt = ds1.Tables[0];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_12_Inv.EClassIDCode.EClasPurMrr>();
            switch (comcod)
            {
                case "3368":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.rptPurMrrEntryFinlay", lst, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtchalanno", "Chalan No : " + ds1.Tables[1].Rows[0]["chlnno"]));
                    mrdate = "MRR Date : " + mrdate;
                    prjname = "Project Name : " + prjname;
                    suppliername = "Supplier Name : " + suppliername;
                    mrrno1 = "MRR No : " + mrrno1;
                    break;

                case "3370"://CPDL
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.rptPurMrrEntryCPDL", lst, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                    Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                    Rpt1.SetParameters(new ReportParameter("mrrno", ASTUtility.CustomReqFormat(mrrno)));
                    Rpt1.SetParameters(new ReportParameter("address", address));
                    Rpt1.SetParameters(new ReportParameter("chlandate",clndate));
                    Rpt1.SetParameters(new ReportParameter("porno", porno));                    
                    Rpt1.SetParameters(new ReportParameter("orderdat", orderdat));
                    Rpt1.SetParameters(new ReportParameter("requeNo", ASTUtility.CustomReqFormat(ds1.Tables[0].Rows[0]["reqno"].ToString())));
                    Rpt1.SetParameters(new ReportParameter("MrrRef", ds1.Tables[1].Rows[0]["mrrref"].ToString()));
                    Rpt1.SetParameters(new ReportParameter("pordar", ASTUtility.CustomReqFormat(pordar)));
                    Rpt1.SetParameters(new ReportParameter("Note","MRR must be reached at Head Office within 2 working days of material receiving."));
                    Rpt1.SetParameters(new ReportParameter("txtchalanno",  ds1.Tables[1].Rows[0]["chlnno"].ToString()));
                    break;
                default:
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.rptPurMrrEntry", lst, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtchalanno", "Chalan No : " + ds1.Tables[1].Rows[0]["chlnno"]));
                    mrdate = "MRR Date : " + mrdate;
                    prjname = "Project Name : " + prjname;
                    suppliername = "Supplier Name : " + suppliername;
                    mrrno1 = "MRR No : " + mrrno1;
                    break;
            }

            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtprjname", prjname));
            Rpt1.SetParameters(new ReportParameter("txtSubName", suppliername));
            Rpt1.SetParameters(new ReportParameter("txtMrrno", mrrno1));
            Rpt1.SetParameters(new ReportParameter("txtMrrRef", "MRR Ref : " + ds1.Tables[1].Rows[0]["mrrref"]));
            Rpt1.SetParameters(new ReportParameter("txtDate", mrdate));
            Rpt1.SetParameters(new ReportParameter("txtQc", "Quality Certificate : " + ds1.Tables[1].Rows[0]["qcno"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtOrder", ds1.Tables[1].Rows[0]["pordref"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtpostedby", ds1.Tables[1].Rows[0]["usrnam"].ToString()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Material Receiving Report"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("txtOrderno", "Order No : " + porno));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }

        private void PurApprovalPrint()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string approvno = this.Request.QueryString["approvno"].ToString();
            string approvdat = this.Request.QueryString["approvdat"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURAPROVINFO", approvno
                , approvdat, "", "", "", "", "", "", "", "");
            DataTable dt3 = ds1.Tables[2];
            DataTable dt2 = ds1.Tables[1];
            var lst2 = dt3.DataTableToList<RealEntity.C_14_Pro.EClassPur.RequisionApprovalSignature>();
            //DataTable dt = (DataTable)ViewState["tblAprov"];
            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_14_Pro.EClassPur.EClassRequisitionApproval>();
            // edison
            LocalReport Rpt1 = new LocalReport();
            string title = (Request.QueryString["Type"].ToString() == "PurApproval") ? "Purchase Approval" : "Purchase Programm";
            string narration = "Narration : ";
            string cadate = "Date: " + Convert.ToDateTime(dt3.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy");
            string cano = "Purchase No: " + approvno;
            string approvedate = "Approve Date : " + Convert.ToDateTime(dt3.Rows[0]["APRVDAT"].ToString()).ToString("dd-MMM-yyyy");
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            string requisitioncreateby = dt3.Rows[0]["reqname"].ToString() + "\n" + Convert.ToDateTime(dt3.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy");
            string checkby = dt3.Rows[0]["reqchkname"].ToString() + "\n" + Convert.ToDateTime(dt3.Rows[0]["checkdat"].ToString()).ToString("dd-MMM-yyyy");
            string rateproposal = dt3.Rows[0]["reqratename"].ToString() + "\n" + Convert.ToDateTime(dt3.Rows[0]["rateidate"].ToString()).ToString("dd-MMM-yyyy"); ;
            string approveby = dt3.Rows[0]["reqaprname"].ToString() + "\n" + Convert.ToDateTime(dt3.Rows[0]["aprvdat"].ToString()).ToString("dd-MMM-yyyy"); ;
            string finalapproveby = dt3.Rows[0]["reqfaprname"].ToString() + "\n" + Convert.ToDateTime(dt2.Rows[0]["aprovdat"].ToString()).ToString("dd-MMM-yyyy"); ;
            if (comcod == "3335")
            {









                //string title = (Request.QueryString["InputType"].ToString() == "PurApproval") ? "Purchase Approval" : "Purchase Programm";
                //string narration = "Narration : " + this.txtAprovNarr.Text.ToString().Trim();
                //string cadate = "Date: " + this.txtCurAprovDate.Text.Trim();
                //string cano = "Purchase No: " + this.lblCurAprovNo1.Text.ToString().Trim() + this.txtCurAprovNo2.Text.ToString().Trim();
                //string approvedate = "Approve Date : " + this.txtApprovalDate.Text.ToString().Trim();
                //string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
                //string requisitioncreateby = this.txtPreparedBy.Text.Trim().ToString();
                //string checkby = "";
                //string rateproposal = "";
                //string approveby = this.txtApprovedBy.Text.ToString().Trim();
                //string finalapproveby = "";


                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurApprovEntryEdison", lst, null, null);

                Rpt1.SetParameters(new ReportParameter("companyname", comnam));
                Rpt1.SetParameters(new ReportParameter("title", title));
                Rpt1.SetParameters(new ReportParameter("requisitioncreateby", requisitioncreateby));
                Rpt1.SetParameters(new ReportParameter("approvedby", approveby));
                Rpt1.SetParameters(new ReportParameter("narration", narration));
                Rpt1.SetParameters(new ReportParameter("checkby", checkby));
                Rpt1.SetParameters(new ReportParameter("rateproposal", rateproposal));
                Rpt1.SetParameters(new ReportParameter("finalapprovedby", finalapproveby));
                Rpt1.SetParameters(new ReportParameter("cadate", cadate));
                Rpt1.SetParameters(new ReportParameter("cano", cano));
                Rpt1.SetParameters(new ReportParameter("approvdate", approvedate));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));




            }
            else
            {




                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurAprovEntry", lst, null, null);

                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("txtTitle", title));
                Rpt1.SetParameters(new ReportParameter("narration", narration));
                Rpt1.SetParameters(new ReportParameter("pbname", requisitioncreateby));
                Rpt1.SetParameters(new ReportParameter("apname", approveby));
                Rpt1.SetParameters(new ReportParameter("date", cadate));
                Rpt1.SetParameters(new ReportParameter("cano", cano));
                Rpt1.SetParameters(new ReportParameter("aprvdate", approvedate));
                Rpt1.SetParameters(new ReportParameter("printFooter", txtuserinfo));






            }




            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";




            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string approvno = this.Request.QueryString["approvno"].ToString();
            //string approvdat = this.Request.QueryString["approvdat"].ToString();


            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURAPROVINFO", approvno
            //    , approvdat, "", "", "", "", "", "", "", "");
            //DataTable dt3 = ds1.Tables[2];

            //var lst2 = dt3.DataTableToList<RealEntity.C_14_Pro.EClassPur.RequisionApprovalSignature>();


            //DataTable dt2 = ds1.Tables[1];
            ////DataTable dt = (DataTable)ViewState["tblAprov"];
            //DataTable dt1 = ds1.Tables[0];
            //var lst = dt1.DataTableToList<RealEntity.C_14_Pro.EClassPur.EClassRequisitionApproval>();

            //string title = (Request.QueryString["Type"].ToString() == "PurApproval") ? "Purchase Approval" : "Purchase Programm";
            //string narration = "Narration : ";
            //string cadate = "Date: " + Convert.ToDateTime(dt3.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy");
            //string cano = "Purchase No: " + approvno;
            //string approvedate = "Approve Date : " + Convert.ToDateTime(dt3.Rows[0]["APRVDAT"].ToString()).ToString("dd-MMM-yyyy");
            //string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            //string requisitioncreateby = dt3.Rows[0]["reqname"].ToString() + "\n" + Convert.ToDateTime(dt3.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy");
            //string checkby = dt3.Rows[0]["reqchkname"].ToString() + "\n" + Convert.ToDateTime(dt3.Rows[0]["checkdat"].ToString()).ToString("dd-MMM-yyyy");
            //string rateproposal = dt3.Rows[0]["reqratename"].ToString() + "\n" + Convert.ToDateTime(dt3.Rows[0]["rateidate"].ToString()).ToString("dd-MMM-yyyy"); ;
            //string approveby = dt3.Rows[0]["reqaprname"].ToString() + "\n" + Convert.ToDateTime(dt3.Rows[0]["aprvdat"].ToString()).ToString("dd-MMM-yyyy"); ;
            //string finalapproveby = dt3.Rows[0]["reqreqfaprname"].ToString() + "\n" + Convert.ToDateTime(dt2.Rows[0]["aprovdat"].ToString()).ToString("dd-MMM-yyyy"); ;

            //LocalReport Rpt1 = new LocalReport();
            //Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurAprovEntry", lst, null, null);



            //Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            //Rpt1.SetParameters(new ReportParameter("title", title));
            //Rpt1.SetParameters(new ReportParameter("requisitioncreateby", requisitioncreateby));
            //Rpt1.SetParameters(new ReportParameter("approvedby", approveby));
            //Rpt1.SetParameters(new ReportParameter("narration", narration));
            //Rpt1.SetParameters(new ReportParameter("checkby", checkby));
            //Rpt1.SetParameters(new ReportParameter("rateproposal", rateproposal));
            //Rpt1.SetParameters(new ReportParameter("finalapprovedby", finalapproveby));
            //Rpt1.SetParameters(new ReportParameter("cadate", cadate));
            //Rpt1.SetParameters(new ReportParameter("cano", cano));
            //Rpt1.SetParameters(new ReportParameter("approvdate", approvedate));
            //Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));



            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";



            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Order Process";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "Purchase No: " + approvno;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}









        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }




        private void PrintFinalAppRequisition()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");


            string mReqNo = this.Request.QueryString["reqno"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, "",
                     "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt1 = ds1.Tables[1];
            DataTable dt = ds1.Tables[2];
            DataTable dtr = ds1.Tables[0];
            DataTable dtPType = ds1.Tables[6];


            // rdlc start for manama
            if (comcod == "3353")
            {

                string txtcrno = dt1.Rows[0]["reqno1"].ToString(); ;

                string txtcrdate = Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy"); ;

                string txtmrfno = dt1.Rows[0]["mrfno"].ToString(); ;

                string txtprojectname = dt1.Rows[0]["pactdesc"].ToString();
                string txtAddress = dt1.Rows[0]["paddress"].ToString();



                string txtbuildno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='001'").Length > 0 ? (dt.Select("termsid='001'")[0]["termsdesc"]).ToString() : ""));
                //string txtbuildno = dt.Rows[0]["termsdesc"].ToString();
                string floorno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='002'").Length > 0 ? (dt.Select("termsid='002'")[0]["termsdesc"]).ToString() : ""));
                string txtfloorno = (dt.Rows.Count == 0) ? "" : (floorno + (dt.Select("termsid='003'").Length > 0 ? (", " + (dt.Select("termsid='003'")[0]["termsdesc"]).ToString()) : ""));

                //  string txtfloorno =(dt.Rows.Count<2)?"": (dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString());

                string txtpforused = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='004'").Length > 0 ? (dt.Select("termsid='004'")[0]["termsdesc"]).ToString() : ""));



                double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
                double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
                double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;



                string txttoamt = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
                string txttoamt02 = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
                string rpttxtnaration = dt1.Rows[0]["reqnar"].ToString();
                string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

                string txtSign1 = ds1.Tables[4].Rows[0]["reqname"].ToString() + "\n" + ds1.Tables[4].Rows[0]["reqdat"].ToString();
                string txtSign2 = ds1.Tables[4].Rows[0]["reqratename"].ToString() + "\n" + ds1.Tables[4].Rows[0]["rateidate"].ToString();

                //For Payment Type
                string payType = "";
                for (int i = 0; i < dtPType.Rows.Count; i++)
                {
                    payType += dtPType.Rows[i]["paytype"] + ",";
                }
                string fPayType = payType.Remove(payType.Length - 1, 1);

                var lst = dtr.DataTableToList<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>();
                LocalReport Rpt1 = new LocalReport();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptReqEntryManama", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));
                Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Materials Purchase Requisition"));
                Rpt1.SetParameters(new ReportParameter("txtReqNo", txtcrno));
                Rpt1.SetParameters(new ReportParameter("txtPayType", fPayType));
                Rpt1.SetParameters(new ReportParameter("txtReqDate", txtcrdate));
                Rpt1.SetParameters(new ReportParameter("txtMrfno", txtmrfno));
                Rpt1.SetParameters(new ReportParameter("txtProjectName", txtprojectname));
                Rpt1.SetParameters(new ReportParameter("txtAddress", txtAddress));
                Rpt1.SetParameters(new ReportParameter("txtBuildingNo", txtbuildno));
                Rpt1.SetParameters(new ReportParameter("txtFloorNo", txtfloorno));
                Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", txtpforused));
                Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
                Rpt1.SetParameters(new ReportParameter("txttoamt02", txttoamt02));
                Rpt1.SetParameters(new ReportParameter("rpttxtnaration", rpttxtnaration));
                Rpt1.SetParameters(new ReportParameter("txtRptFooter", txtuserinfo));
                Rpt1.SetParameters(new ReportParameter("txtSign1", txtSign1));
                Rpt1.SetParameters(new ReportParameter("txtSign2", txtSign2));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

                // rdlc end mamama
            }
            else
            {
                string txtfloorno = "", txtpforused = "", txtReqNo = "";
                if (comcod == "1205" || comcod == "3351" || comcod == "3352")
                {
                    txtReqNo = dt1.Rows[0]["reqno1"].ToString();
                    txtfloorno = dt.Rows[0]["termsdesc"].ToString() + ((dt.Rows[1]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[1]["termsdesc"].ToString(); ;
                    txtpforused = dt.Rows[1]["termsdesc"].ToString();
                }               
                else if(comcod=="3370")
                {
                    txtReqNo = ASTUtility.CustomReqFormat(dt1.Rows[0]["reqno"].ToString());
                    txtfloorno = dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString(); ;
                    txtpforused = dt.Rows[3]["termsdesc"].ToString();
                }
                else
                {
                    txtReqNo = dt1.Rows[0]["reqno1"].ToString();
                    txtfloorno = dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString(); ;
                    txtpforused = dt.Rows[3]["termsdesc"].ToString();
                }

                double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
                double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
                double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;

                var list = dtr.DataTableToList<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>();
                LocalReport Rpt1 = new LocalReport();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptReqEntryEdison", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));
                Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Materials Purchase Requisition"));
                Rpt1.SetParameters(new ReportParameter("txtReqNo", txtReqNo));
                Rpt1.SetParameters(new ReportParameter("txtReqDate", Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("txtMrfno", dt1.Rows[0]["mrfno"].ToString()));
                Rpt1.SetParameters(new ReportParameter("txtProjectName", dt1.Rows[0]["pactdesc"].ToString()));
                Rpt1.SetParameters(new ReportParameter("txtAddress", dt1.Rows[0]["paddress"].ToString()));
                Rpt1.SetParameters(new ReportParameter("txtBuildingNo", dt.Rows[0]["termsdesc"].ToString()));
                Rpt1.SetParameters(new ReportParameter("txtFloorNo", txtfloorno));
                Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", txtpforused));
                Rpt1.SetParameters(new ReportParameter("txttoamt", Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ")));
                Rpt1.SetParameters(new ReportParameter("txttoamt02", Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ")));
                Rpt1.SetParameters(new ReportParameter("rpttxtnaration", dt1.Rows[0]["reqnar"].ToString()));
                Rpt1.SetParameters(new ReportParameter("txtReqname", ds1.Tables[4].Rows[0]["reqname"].ToString() + "\n" + ds1.Tables[4].Rows[0]["reqdat"].ToString()));
                Rpt1.SetParameters(new ReportParameter("txtReqChkname", ds1.Tables[4].Rows[0]["reqchkname"].ToString() + "\n" + ds1.Tables[4].Rows[0]["checkdat"].ToString()));
                Rpt1.SetParameters(new ReportParameter("txtReqRatename", ds1.Tables[4].Rows[0]["reqratename"].ToString() + "\n" + ds1.Tables[4].Rows[0]["rateidate"].ToString()));
                Rpt1.SetParameters(new ReportParameter("txtReqAprname", ds1.Tables[4].Rows[0]["reqaprname"].ToString() + "\n" + ds1.Tables[4].Rows[0]["aprvdat"].ToString()));
                Rpt1.SetParameters(new ReportParameter("txtRptFooter", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("lblmrfno", ReadCookie()));


                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }

        }


        private void PrintRequisitionRDLC()
        {
            string printcomreq = this.CompanyRequisition();

            if (printcomreq == "PrintReque01")
            {
                this.PrintRequisition01();

            }

            else if (printcomreq == "PrintReque03")
            {
                this.PrintRequisition03();


            }

            else if (printcomreq == "PrintReque04")
            {
                this.PrintRequisition04();


            }

            else if (printcomreq == "PrintReque05")
            {
                this.PrintRequisition05();


            }

            else if (printcomreq == "PrintReque06")
            {
                //All Construction
                this.PrintRequisition06();


            }



            else
            {
                this.PrintRequisition02RDLC();



            }



        }


        private void PrintRequisition07()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");

            //string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            //string mReqNo = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();





            string mReqNo = this.Request.QueryString["reqno"].ToString();
            // string reqdat = this.Request.QueryString["reqdat"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, "",
                     "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            //Rdlc
            DataTable dt1 = ds1.Tables[1];
            // DataTable dt = (DataTable)ViewState["tblreqdesc"];





            string txtcrno = dt1.Rows[0]["reqno1"].ToString(); ;

            string txtcrdate = Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy"); ;

            string txtmrfno = dt1.Rows[0]["mrfno"].ToString(); ;

            string txtprojectname = dt1.Rows[0]["pactdesc"].ToString();
            string txtAddress = dt1.Rows[0]["paddress"].ToString();


            DataTable dt = ds1.Tables[2];
            //TextObject txtbuildno = rptstk.ReportDefinition.ReportObjects["txtbuildno"] as TextObject;
            //txtbuildno.Text = dt.Rows[0]["termsdesc"].ToString();
            //TextObject txtfloorno = rptstk.ReportDefinition.ReportObjects["txtfloorno"] as TextObject;
            //txtfloorno.Text = dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString(); ;
            ////TextObject txtflatno = rptstk.ReportDefinition.ReportObjects["txtflatno"] as TextObject;
            ////txtflatno.Text = dt.Rows[2]["termsdesc"].ToString();
            //TextObject txtpforused = rptstk.ReportDefinition.ReportObjects["txtpforused"] as TextObject;
            //txtpforused.Text = dt.Rows[3]["termsdesc"].ToString();



            DataTable dt2 = ds1.Tables[4];

            string reqby = dt2.Rows[0]["reqname"].ToString();

            string checkby = dt2.Rows[0]["reqchkname"].ToString();


            string txtbuildno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='001'").Length > 0 ? (dt.Select("termsid='001'")[0]["termsdesc"]).ToString() : ""));
            //string txtbuildno = dt.Rows[0]["termsdesc"].ToString();
            string floorno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='002'").Length > 0 ? (dt.Select("termsid='002'")[0]["termsdesc"]).ToString() : ""));
            string txtfloorno = (dt.Rows.Count == 0) ? "" : (floorno + (dt.Select("termsid='003'").Length > 0 ? (", " + (dt.Select("termsid='003'")[0]["termsdesc"]).ToString()) : ""));

            //  string txtfloorno =(dt.Rows.Count<2)?"": (dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString());

            string txtpforused = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='004'").Length > 0 ? (dt.Select("termsid='004'")[0]["termsdesc"]).ToString() : ""));

            DataTable dtr = ds1.Tables[0];

            double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;



            string txttoamt = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");

            string txttoamt02 = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            string rpttxtnaration = dt1.Rows[0]["reqnar"].ToString();
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            string txtSign1 = "";
            string txtSign2 = "";
            string txtSign3 = "";
            string txtSign4 = "";
            string txtSign5 = "";
            string txtSign6 = "";
            string txtSign7 = "";
            string txtSign8 = "";
            string txtSign9 = "";

            if (comcod == "3330")
            {

                txtSign1 = "Store In-charge";

                txtSign2 = "Project Incharge";

                txtSign3 = "DPM/PM (Operation)";

                txtSign4 = "Procurement";

                txtSign5 = "Cost & Budget";

                txtSign6 = "Head Of Construction";

                txtSign7 = "Approved By";
            }

            else if (comcod == "3332")
            {


                txtSign1 = "S.K";

                txtSign2 = "Project Incharge";

                txtSign3 = "Procurement";

                txtSign4 = "Cost & Budget";

                txtSign5 = "Cheif Engineer";

                txtSign6 = "Director";

                txtSign7 = "Managing Director/Chairman";

            }


            else
            {

                txtSign1 = "S.K";

                txtSign2 = "Project Incharge";

                txtSign3 = "DPM/PM/AGM/DGM";

                txtSign4 = "Procurement";

                txtSign5 = "Cost & Budget";

                txtSign6 = "Head Of Construction";

                txtSign7 = "Managing Director";

                txtSign8 = "Requisition By";

                txtSign9 = "Checked By";
            }

            var lst = dtr.DataTableToList<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptReqEntry07", lst, null, null);
            Rpt1.EnableExternalImages = true;


            Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));

            Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Materials Purchase Requisition"));
            Rpt1.SetParameters(new ReportParameter("txtReqNo", txtcrno));
            Rpt1.SetParameters(new ReportParameter("txtReqDate", txtcrdate));
            Rpt1.SetParameters(new ReportParameter("txtMrfno", txtmrfno));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", txtprojectname));
            Rpt1.SetParameters(new ReportParameter("txtAddress", txtAddress));
            Rpt1.SetParameters(new ReportParameter("txtBuildingNo", txtbuildno));
            Rpt1.SetParameters(new ReportParameter("txtFloorNo", txtfloorno));
            Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", txtpforused));
            Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
            Rpt1.SetParameters(new ReportParameter("txttoamt02", txttoamt02));
            Rpt1.SetParameters(new ReportParameter("rpttxtnaration", rpttxtnaration));
            Rpt1.SetParameters(new ReportParameter("txtRptFooter", txtuserinfo));

            Rpt1.SetParameters(new ReportParameter("txtSign1", txtSign1));
            Rpt1.SetParameters(new ReportParameter("txtSign2", txtSign2));
            Rpt1.SetParameters(new ReportParameter("txtSign3", txtSign3));
            Rpt1.SetParameters(new ReportParameter("txtSign4", txtSign4));
            Rpt1.SetParameters(new ReportParameter("txtSign5", txtSign5));
            Rpt1.SetParameters(new ReportParameter("txtSign6", txtSign6));
            Rpt1.SetParameters(new ReportParameter("txtSign7", txtSign7));

            Rpt1.SetParameters(new ReportParameter("txtSign8", txtSign8));
            Rpt1.SetParameters(new ReportParameter("txtSign9", txtSign9));
            Rpt1.SetParameters(new ReportParameter("reqby", reqby));
            Rpt1.SetParameters(new ReportParameter("checkby", checkby));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";





        }


        private void PrintRequisition08()
        {

            //DataTable dt1 = (DataTable)Session["tblUserReq"]; 
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");


            string mReqNo = this.Request.QueryString["reqno"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, "",
                     "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt1 = ds1.Tables[1];




            string txtcrno = dt1.Rows[0]["reqno1"].ToString(); ;

            string txtcrdate = Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy"); ;

            string txtmrfno = dt1.Rows[0]["mrfno"].ToString(); ;

            string txtprojectname = dt1.Rows[0]["pactdesc"].ToString();
            string txtAddress = dt1.Rows[0]["paddress"].ToString();


            DataTable dt = ds1.Tables[2];


            string txtbuildno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='001'").Length > 0 ? (dt.Select("termsid='001'")[0]["termsdesc"]).ToString() : ""));
            //string txtbuildno = dt.Rows[0]["termsdesc"].ToString();
            string floorno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='002'").Length > 0 ? (dt.Select("termsid='002'")[0]["termsdesc"]).ToString() : ""));
            string txtfloorno = (dt.Rows.Count == 0) ? "" : (floorno + (dt.Select("termsid='003'").Length > 0 ? (", " + (dt.Select("termsid='003'")[0]["termsdesc"]).ToString()) : ""));

            //  string txtfloorno =(dt.Rows.Count<2)?"": (dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString());

            string txtpforused = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='004'").Length > 0 ? (dt.Select("termsid='004'")[0]["termsdesc"]).ToString() : ""));

            DataTable dtr = ds1.Tables[0];

            double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;



            string txttoamt = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");

            string txttoamt02 = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            string rpttxtnaration = dt1.Rows[0]["reqnar"].ToString();
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            string txtSign1 = "";
            string txtSign2 = "";
            string txtSign3 = "";
            string txtSign4 = "";
            string txtSign5 = "";
            string txtSign6 = "";
            string txtSign7 = "";

            if (comcod == "3353")
            {

                txtSign1 = "S.K";

                txtSign2 = "Project Incharge";

                txtSign3 = "DPM/PM/AGM/DGM";

                txtSign4 = "Procurement";

                txtSign5 = "Cost & Budget";

                txtSign6 = "Head Of Construction";

                txtSign7 = "Managing Director";
            }

            var lst = dtr.DataTableToList<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptReqEntry08", lst, null, null);
            Rpt1.EnableExternalImages = true;


            Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));

            Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Materials Purchase Requisition"));
            Rpt1.SetParameters(new ReportParameter("txtReqNo", txtcrno));
            Rpt1.SetParameters(new ReportParameter("txtReqDate", txtcrdate));
            Rpt1.SetParameters(new ReportParameter("txtMrfno", txtmrfno));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", txtprojectname));
            Rpt1.SetParameters(new ReportParameter("txtAddress", txtAddress));
            Rpt1.SetParameters(new ReportParameter("txtBuildingNo", txtbuildno));
            Rpt1.SetParameters(new ReportParameter("txtFloorNo", txtfloorno));
            Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", txtpforused));
            Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
            Rpt1.SetParameters(new ReportParameter("txttoamt02", txttoamt02));
            Rpt1.SetParameters(new ReportParameter("rpttxtnaration", rpttxtnaration));
            Rpt1.SetParameters(new ReportParameter("txtRptFooter", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtSign1", txtSign1));
            Rpt1.SetParameters(new ReportParameter("txtSign2", txtSign2));
            Rpt1.SetParameters(new ReportParameter("txtSign3", txtSign3));
            Rpt1.SetParameters(new ReportParameter("txtSign4", txtSign4));
            Rpt1.SetParameters(new ReportParameter("txtSign5", txtSign5));
            Rpt1.SetParameters(new ReportParameter("txtSign6", txtSign6));
            Rpt1.SetParameters(new ReportParameter("txtSign7", txtSign7));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


        }


        private void PrintRequisition02RDLC()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");

            //string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            //string mReqNo = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();





            string mReqNo = this.Request.QueryString["reqno"].ToString();
            string reqdat = this.Request.QueryString["reqdat"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, reqdat,
                     "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            //Rdlc
            DataTable dt1 = ds1.Tables[1];
            // DataTable dt = (DataTable)ViewState["tblreqdesc"];





            string txtcrno = dt1.Rows[0]["reqno1"].ToString(); ;

            string txtcrdate = Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy"); ;

            string txtmrfno = dt1.Rows[0]["mrfno"].ToString(); ;

            string txtprojectname = dt1.Rows[0]["pactdesc"].ToString();
            string txtAddress = dt1.Rows[0]["paddress"].ToString();


            DataTable dt = ds1.Tables[2];
            //TextObject txtbuildno = rptstk.ReportDefinition.ReportObjects["txtbuildno"] as TextObject;
            //txtbuildno.Text = dt.Rows[0]["termsdesc"].ToString();
            //TextObject txtfloorno = rptstk.ReportDefinition.ReportObjects["txtfloorno"] as TextObject;
            //txtfloorno.Text = dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString(); ;
            ////TextObject txtflatno = rptstk.ReportDefinition.ReportObjects["txtflatno"] as TextObject;
            ////txtflatno.Text = dt.Rows[2]["termsdesc"].ToString();
            //TextObject txtpforused = rptstk.ReportDefinition.ReportObjects["txtpforused"] as TextObject;
            //txtpforused.Text = dt.Rows[3]["termsdesc"].ToString();






            string txtbuildno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='001'").Length > 0 ? (dt.Select("termsid='001'")[0]["termsdesc"]).ToString() : ""));
            //string txtbuildno = dt.Rows[0]["termsdesc"].ToString();
            string floorno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='002'").Length > 0 ? (dt.Select("termsid='002'")[0]["termsdesc"]).ToString() : ""));
            string txtfloorno = (dt.Rows.Count == 0) ? "" : (floorno + (dt.Select("termsid='003'").Length > 0 ? (", " + (dt.Select("termsid='003'")[0]["termsdesc"]).ToString()) : ""));

            //  string txtfloorno =(dt.Rows.Count<2)?"": (dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString());

            string txtpforused = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='004'").Length > 0 ? (dt.Select("termsid='004'")[0]["termsdesc"]).ToString() : ""));

            DataTable dtr = ds1.Tables[0];

            double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;



            string txttoamt = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");

            string txttoamt02 = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            string rpttxtnaration = dt1.Rows[0]["reqnar"].ToString();
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            string txtSign1 = "";
            string txtSign2 = "";
            string txtSign3 = "";
            string txtSign4 = "";
            string txtSign5 = "";
            string txtSign6 = "";
            string txtSign7 = "";

            if (comcod == "3330")
            {

                txtSign1 = "Store In-charge";

                txtSign2 = "Project Incharge";

                txtSign3 = "DPM/PM (Operation)";

                txtSign4 = "Procurement";

                txtSign5 = "Cost & Budget";

                txtSign6 = "Head Of Construction";

                txtSign7 = "Approved By";
            }

            else if (comcod == "3332" || comcod == "3101")
            {


                txtSign1 = "S.K";

                txtSign2 = "Project Incharge";

                txtSign3 = "Procurement";

                txtSign4 = "Cost & Budget";

                txtSign5 = "Cheif Engineer";

                txtSign6 = "Director";

                txtSign7 = "Managing Director/Chairman";

            }


            else
            {

                txtSign1 = "S.K";

                txtSign2 = "Project Incharge";

                txtSign3 = "DPM/PM/AGM/DGM";

                txtSign4 = "Procurement";

                txtSign5 = "Cost & Budget";

                txtSign6 = "Head Of Construction";

                txtSign7 = "Managing Director";
            }

            var lst = dtr.DataTableToList<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptReqEntry02", lst, null, null);
            Rpt1.EnableExternalImages = true;


            Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));

            Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Materials Purchase Requisition"));
            Rpt1.SetParameters(new ReportParameter("txtReqNo", txtcrno));
            Rpt1.SetParameters(new ReportParameter("txtReqDate", txtcrdate));
            Rpt1.SetParameters(new ReportParameter("txtMrfno", txtmrfno));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", txtprojectname));
            Rpt1.SetParameters(new ReportParameter("txtAddress", txtAddress));
            Rpt1.SetParameters(new ReportParameter("txtBuildingNo", txtbuildno));
            Rpt1.SetParameters(new ReportParameter("txtFloorNo", txtfloorno));
            Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", txtpforused));
            Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
            Rpt1.SetParameters(new ReportParameter("txttoamt02", txttoamt02));
            Rpt1.SetParameters(new ReportParameter("rpttxtnaration", rpttxtnaration));
            Rpt1.SetParameters(new ReportParameter("txtRptFooter", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtSign1", txtSign1));
            Rpt1.SetParameters(new ReportParameter("txtSign2", txtSign2));
            Rpt1.SetParameters(new ReportParameter("txtSign3", txtSign3));
            Rpt1.SetParameters(new ReportParameter("txtSign4", txtSign4));
            Rpt1.SetParameters(new ReportParameter("txtSign5", txtSign5));
            Rpt1.SetParameters(new ReportParameter("txtSign6", txtSign6));
            Rpt1.SetParameters(new ReportParameter("txtSign7", txtSign7));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


        }


        private string CompanyRequisition()
        {
            string comcod = this.GetCompCode();
            string PrintReq = "";
            switch (comcod)
            {
                case "2305"://Rupayan
                case "3305":
                case "3306":
                case "3307":
                case "3308":
                case "3309":
                case "3310":
                case "3330":

                    //case "3101":

                    PrintReq = "PrintReque02";
                    break;

                //case "3101":
                case "1108":
                case "1109":
                case "3315":
                case "3316":
                    PrintReq = "PrintReque09";
                    break;
                //case "3101":
                case "3325":
                case "2325":
                    PrintReq = "PrintReque03";
                    break;

                case "3332":
                    //case"3101":
                    PrintReq = "PrintReque04";
                    break;

                //Added 
                case "3339":
                    //case "3101":
                    PrintReq = "PrintReque05";
                    break;


                case "1101": //All Constuction
                case "1102":
                case "1103":
                    //case "3101":
                    PrintReq = "PrintReque06";
                    break;

                //case "3101":
                case "1205":
                case "3351":
                case "3352":
                    PrintReq = "PrintReque07";
                    break;

                //Adding End

                //case "3101":
                case "3353":
                    PrintReq = "PrintReque08";
                    break;

                case "3356":
                    //case "3101":
                    PrintReq = "PrintReqiNTECH";
                    break;

                case "3364":
                    PrintReq = "PrintReqJBS";
                    break;
                case "3101":
                case "3367":
                    PrintReq = "PrintReqEpic";
                    break;

                default:
                    PrintReq = "PrintReque02";
                    break;

            }

            return PrintReq;

        }
        private void PrintRequisition()
        {


            string printcomreq = this.CompanyRequisition();

            if (printcomreq == "PrintReque01")
            {
                this.PrintRequisition01();

            }
            // leisure
            else if (printcomreq == "PrintReque03")
            {
                this.PrintRequisition03();


            }

            else if (printcomreq == "PrintReque04")
            {
                this.PrintRequisition04();


            }


            else if (printcomreq == "PrintReque05")
            {
                this.PrintRequisition05();


            }

            else if (printcomreq == "PrintReque06")
            {
                //All Construction
                this.PrintRequisition06();


            }

            else if (printcomreq == "PrintReque07")
            {
                this.PrintRequisition07();
            }

            else if (printcomreq == "PrintReque08")
            {
                this.PrintRequisition08(); // manama
            }

            else if (printcomreq == "PrintReque09")
            {
                this.PrintRequisition09(); // assure
            }

            else if (printcomreq == "PrintReqiNTECH")
            {
                this.PrintRequisitioniNTECH(); //iNTECH
            }

            else if (printcomreq == "PrintReqJBS")
            {
                this.PrintRequisitionJBS();
            } 
            else if (printcomreq == "PrintReqEpic")
            {
                this.PrintRequisitionEpic();
            } 

            else
            {
                this.PrintRequisition02();

                // this.PrintRequisition02RDLC();


            }

        }


        private void PrintRequisition01()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptReqEntry();
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;
            //TextObject rpttxtexdeldate = rptstk.ReportDefinition.ReportObjects["eddate"] as TextObject;
            //rpttxtexdeldate.Text = this.txtExpDeliveryDate.Text.Trim();
            //TextObject rpttxtadate = rptstk.ReportDefinition.ReportObjects["adate"] as TextObject;
            //rpttxtadate.Text = this.txtApprovalDate.Text.Trim();
            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = this.txtReqNarr.Text.Trim();
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = "Project Name: " + this.ddlProject.SelectedItem.Text.Trim().Substring(14);
            //TextObject txtfloornoText = rptstk.ReportDefinition.ReportObjects["floornotext"] as TextObject;
            //TextObject txtfloorno = rptstk.ReportDefinition.ReportObjects["floorno"] as TextObject;
            //if (ddlFloor.SelectedValue.ToString().Trim() != "000")
            //{

            //    txtfloornoText.Text = "Floor No:";
            //    txtfloorno.Text = this.ddlFloor.SelectedValue.ToString().Trim();
            //}
            //else
            //{
            //    txtfloornoText.Text = "";
            //    txtfloorno.Text = " ";
            //}

            //TextObject txtmrfno = rptstk.ReportDefinition.ReportObjects["mrfno"] as TextObject;
            //txtmrfno.Text = this.txtMRFNo.Text.ToString().Trim();
            //TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
            //txtcrdate.Text = this.txtCurReqDate.Text.ToString().Trim();
            //TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
            //txtcrno.Text = this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{

            //    string eventtype = "Material Requisition";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "Project Name " + this.ddlProject.SelectedItem.ToString() + "Req No- " + this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //DataTable dt1 = new DataTable();
            //dt1 = (DataTable)ViewState["tblReq"];

            //rptstk.SetDataSource(dt1);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintRequisition02()
        {

            //DataTable dt1 = (DataTable)Session["tblUserReq"]; 
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");


            string mReqNo = this.Request.QueryString["reqno"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, "",
                     "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt1 = ds1.Tables[1];
            DataTable dt = ds1.Tables[2];

            string txtcrno = dt1.Rows[0]["reqno1"].ToString();
            string txtcrdate = Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy");
            string txtmrfno = dt1.Rows[0]["mrfno"].ToString();
            string txtprojectname = dt1.Rows[0]["pactdesc"].ToString();
            string txtAddress = dt1.Rows[0]["paddress"].ToString();


            string txtbuildno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='001'").Length > 0 ? (dt.Select("termsid='001'")[0]["termsdesc"]).ToString() : ""));
            //string txtbuildno = dt.Rows[0]["termsdesc"].ToString();
            string floorno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='002'").Length > 0 ? (dt.Select("termsid='002'")[0]["termsdesc"]).ToString() : ""));
            string txtfloorno = (dt.Rows.Count == 0) ? "" : (floorno + (dt.Select("termsid='003'").Length > 0 ? (", " + (dt.Select("termsid='003'")[0]["termsdesc"]).ToString()) : ""));

            //  string txtfloorno =(dt.Rows.Count<2)?"": (dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString());

            string txtpforused = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='004'").Length > 0 ? (dt.Select("termsid='004'")[0]["termsdesc"]).ToString() : ""));

            DataTable dtr = ds1.Tables[0];

            double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;



            string txttoamt = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            string txttoamt02 = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            string rpttxtnaration = dt1.Rows[0]["reqnar"].ToString();
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            string txtSign1 = "";
            string txtSign2 = "";
            string txtSign3 = "";
            string txtSign4 = "";
            string txtSign5 = "";
            string txtSign6 = "";
            string txtSign7 = "";

            if (comcod == "3330")
            {

                txtSign1 = "Store In-charge";
                txtSign2 = "Project Incharge";
                txtSign3 = "DPM/PM (Operation)";
                txtSign4 = "Procurement";
                txtSign5 = "Cost & Budget";
                txtSign6 = "Head Of Construction";
                txtSign7 = "Approved By";
            }

            else if (comcod == "3332")
            {
                txtSign1 = "S.K";
                txtSign2 = "Project Incharge";
                txtSign3 = "Procurement";
                txtSign4 = "Cost & Budget";
                txtSign5 = "Cheif Engineer";
                txtSign6 = "Director";
                txtSign7 = "Managing Director/Chairman";
            }
            else if(comcod=="3370")
            {
                txtSign1 = "S.K";
                txtSign2 = "Project Incharge";
                txtSign3 = "DPM/PM/AGM/DGM";
                txtSign4 = "Procurement";
                txtSign5 = "Cost & Budget";
                txtSign6 = "Head Of Construction";
                txtSign7 = "Managing Director";
                txtcrno = ASTUtility.CustomReqFormat(dt1.Rows[0]["reqno"].ToString());
            }
            else
            {
                txtSign1 = "S.K";
                txtSign2 = "Project Incharge";
                txtSign3 = "DPM/PM/AGM/DGM";
                txtSign4 = "Procurement";
                txtSign5 = "Cost & Budget";
                txtSign6 = "Head Of Construction";
                txtSign7 = "Managing Director";
            }

            var lst = dtr.DataTableToList<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptReqEntry02", lst, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Materials Purchase Requisition"));
            Rpt1.SetParameters(new ReportParameter("txtReqNo", txtcrno));
            Rpt1.SetParameters(new ReportParameter("txtReqDate", txtcrdate));
            Rpt1.SetParameters(new ReportParameter("txtMrfno", txtmrfno));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", txtprojectname));
            Rpt1.SetParameters(new ReportParameter("txtAddress", txtAddress));
            Rpt1.SetParameters(new ReportParameter("txtBuildingNo", txtbuildno));
            Rpt1.SetParameters(new ReportParameter("txtFloorNo", txtfloorno));
            Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", txtpforused));
            Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
            Rpt1.SetParameters(new ReportParameter("txttoamt02", txttoamt02));
            Rpt1.SetParameters(new ReportParameter("rpttxtnaration", rpttxtnaration));
            Rpt1.SetParameters(new ReportParameter("txtRptFooter", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtSign1", txtSign1));
            Rpt1.SetParameters(new ReportParameter("txtSign2", txtSign2));
            Rpt1.SetParameters(new ReportParameter("txtSign3", txtSign3));
            Rpt1.SetParameters(new ReportParameter("txtSign4", txtSign4));
            Rpt1.SetParameters(new ReportParameter("txtSign5", txtSign5));
            Rpt1.SetParameters(new ReportParameter("txtSign6", txtSign6));
            Rpt1.SetParameters(new ReportParameter("txtSign7", txtSign7));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


        }  
        private void PrintRequisitionEpic()
        {

            //DataTable dt1 = (DataTable)Session["tblUserReq"]; 
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string mReqNo = this.Request.QueryString["reqno"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, "",
                     "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt1 = ds1.Tables[1];
            DataTable dt = ds1.Tables[2];
            DataTable tblsign = ds1.Tables[4];

            string txtcrno = dt1.Rows[0]["reqno1"].ToString();
            string txtcrdate = Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy");
            string txtmrfno = dt1.Rows[0]["mrfno"].ToString();
            string txtprojectname = dt1.Rows[0]["pactdesc"].ToString();
            string txtAddress = dt1.Rows[0]["paddress"].ToString();


            string txtbuildno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='001'").Length > 0 ? (dt.Select("termsid='001'")[0]["termsdesc"]).ToString() : ""));
            //string txtbuildno = dt.Rows[0]["termsdesc"].ToString();
            string floorno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='002'").Length > 0 ? (dt.Select("termsid='002'")[0]["termsdesc"]).ToString() : ""));
            string txtfloorno = (dt.Rows.Count == 0) ? "" : (floorno + (dt.Select("termsid='003'").Length > 0 ? (", " + (dt.Select("termsid='003'")[0]["termsdesc"]).ToString()) : ""));

            //  string txtfloorno =(dt.Rows.Count<2)?"": (dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString());

            string txtpforused = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='004'").Length > 0 ? (dt.Select("termsid='004'")[0]["termsdesc"]).ToString() : ""));

            DataTable dtr = ds1.Tables[0];

            double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;



            string txttoamt = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            string txttoamt02 = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            string rpttxtnaration = dt1.Rows[0]["reqnar"].ToString();
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string post1 = tblsign.Rows[0]["reqpostname"].ToString() + "\n" + tblsign.Rows[0]["posteddat"].ToString();
            string check1 = tblsign.Rows[0]["reqchkname"].ToString() + "\n" + tblsign.Rows[0]["checkdat"].ToString();
            string first1 = tblsign.Rows[0]["fappname"].ToString() + "\n" + tblsign.Rows[0]["fappdat"].ToString();
            string second1 = tblsign.Rows[0]["sappname"].ToString() + "\n" + tblsign.Rows[0]["sappdat"].ToString();






            var lst = dtr.DataTableToList<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptReqEntryEpic", lst, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Materials Purchase Requisition"));
            Rpt1.SetParameters(new ReportParameter("txtReqNo", txtcrno));
            Rpt1.SetParameters(new ReportParameter("txtReqDate", txtcrdate));
            Rpt1.SetParameters(new ReportParameter("txtMrfno", txtmrfno));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", txtprojectname));
            Rpt1.SetParameters(new ReportParameter("txtAddress", txtAddress));
            Rpt1.SetParameters(new ReportParameter("txtBuildingNo", txtbuildno));
            Rpt1.SetParameters(new ReportParameter("txtFloorNo", txtfloorno));
            Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", txtpforused));
            Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
            Rpt1.SetParameters(new ReportParameter("txttoamt02", txttoamt02));
            Rpt1.SetParameters(new ReportParameter("rpttxtnaration", rpttxtnaration));
            Rpt1.SetParameters(new ReportParameter("txtRptFooter", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtSign1", post1));
            Rpt1.SetParameters(new ReportParameter("txtSign2", check1));
            Rpt1.SetParameters(new ReportParameter("txtSign3", first1));
            Rpt1.SetParameters(new ReportParameter("txtSign4", second1));
            Rpt1.SetParameters(new ReportParameter("txtSign5", ""));
            Rpt1.SetParameters(new ReportParameter("txtSign6", ""));
            Rpt1.SetParameters(new ReportParameter("txtSign7", ""));
             
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


        }


        private void PrintRequisition03()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");


            string mReqNo = this.Request.QueryString["reqno"].ToString();
            string reqdat = this.Request.QueryString["reqdat"].ToString();


            //Addded 
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, reqdat,
                  "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            DataTable dt1 = ds1.Tables[1];


            string txtcrno = dt1.Rows[0]["reqno1"].ToString(); ;

            string txtcrdate = Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy"); ;

            string txtmrfno = dt1.Rows[0]["mrfno"].ToString(); ;

            string txtprojectname = dt1.Rows[0]["pactdesc"].ToString();
            string txtAddress = dt1.Rows[0]["paddress"].ToString();


            DataTable dt = ds1.Tables[2];


            string txtbuildno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='001'").Length > 0 ? (dt.Select("termsid='001'")[0]["termsdesc"]).ToString() : ""));
            //string txtbuildno = dt.Rows[0]["termsdesc"].ToString();
            string floorno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='002'").Length > 0 ? (dt.Select("termsid='002'")[0]["termsdesc"]).ToString() : ""));
            string txtfloorno = (dt.Rows.Count == 0) ? "" : (floorno + (dt.Select("termsid='003'").Length > 0 ? (", " + (dt.Select("termsid='003'")[0]["termsdesc"]).ToString()) : ""));

            //  string txtfloorno =(dt.Rows.Count<2)?"": (dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString());

            string txtpforused = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='004'").Length > 0 ? (dt.Select("termsid='004'")[0]["termsdesc"]).ToString() : ""));

            DataTable dtr = ds1.Tables[0];

            double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;

            string txttoamt = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            string txttoamt02 = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");

            string rpttxtnaration = dt1.Rows[0]["reqnar"].ToString();
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            string txtSign1 = "S.K";
            string txtSign2 = "Project Incharge";
            string txtSign3 = "DPM/PM/AGM/DGM";
            string txtSign4 = "Procurement";
            string txtSign5 = "Head Of Construction";
            string txtSign6 = "Cost & Budget";
            string txtSign7 = "DMD";
            string txtSign8 = "Chairman";

            var list = dtr.DataTableToList<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptReqEntry03", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Materials Purchase Requisition"));
            Rpt1.SetParameters(new ReportParameter("txtReqNo", txtcrno));
            Rpt1.SetParameters(new ReportParameter("txtReqDate", txtcrdate));
            Rpt1.SetParameters(new ReportParameter("txtMrfno", txtmrfno));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", txtprojectname));
            Rpt1.SetParameters(new ReportParameter("txtAddress", txtAddress));
            Rpt1.SetParameters(new ReportParameter("txtBuildingNo", txtbuildno));
            Rpt1.SetParameters(new ReportParameter("txtFloorNo", txtfloorno));
            Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", txtpforused));
            Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
            Rpt1.SetParameters(new ReportParameter("txttoamt02", txttoamt02));
            Rpt1.SetParameters(new ReportParameter("rpttxtnaration", rpttxtnaration));
            Rpt1.SetParameters(new ReportParameter("txtRptFooter", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtSign1", txtSign1));
            Rpt1.SetParameters(new ReportParameter("txtSign2", txtSign2));
            Rpt1.SetParameters(new ReportParameter("txtSign3", txtSign3));
            Rpt1.SetParameters(new ReportParameter("txtSign4", txtSign4));
            Rpt1.SetParameters(new ReportParameter("txtSign5", txtSign5));
            Rpt1.SetParameters(new ReportParameter("txtSign6", txtSign6));
            Rpt1.SetParameters(new ReportParameter("txtSign7", txtSign7));
            Rpt1.SetParameters(new ReportParameter("txtSign8", txtSign8));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

        }


        private void PrintRequisition04()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");

            //string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            //string mReqNo = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();


            string mReqNo = this.Request.QueryString["reqno"].ToString();
            string reqdat = this.Request.QueryString["reqdat"].ToString();



            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, reqdat,
                     "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt1 = ds1.Tables[1];

            string txtcrno = dt1.Rows[0]["reqno1"].ToString(); ;

            string txtcrdate = Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy"); ;

            string txtmrfno = dt1.Rows[0]["mrfno"].ToString(); ;

            string txtprojectname = dt1.Rows[0]["pactdesc"].ToString();
            string txtAddress = dt1.Rows[0]["paddress"].ToString();


            DataTable dt = ds1.Tables[2];


            string txtbuildno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='001'").Length > 0 ? (dt.Select("termsid='001'")[0]["termsdesc"]).ToString() : ""));
            //string txtbuildno = dt.Rows[0]["termsdesc"].ToString();
            string floorno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='002'").Length > 0 ? (dt.Select("termsid='002'")[0]["termsdesc"]).ToString() : ""));
            string txtfloorno = (dt.Rows.Count == 0) ? "" : (floorno + (dt.Select("termsid='003'").Length > 0 ? (", " + (dt.Select("termsid='003'")[0]["termsdesc"]).ToString()) : ""));

            //  string txtfloorno =(dt.Rows.Count<2)?"": (dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString());

            string txtpforused = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='004'").Length > 0 ? (dt.Select("termsid='004'")[0]["termsdesc"]).ToString() : ""));

            DataTable dtr = ds1.Tables[0];

            double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;


            string txttoamt = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");

            string txttoamt02 = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            string rpttxtnaration = dt1.Rows[0]["reqnar"].ToString();
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            string txtSign1 = "";
            string txtSign2 = "";
            string txtSign3 = "";
            string txtSign4 = "";
            string txtSign5 = "";
            string txtSign6 = "";
            string txtSign7 = "";


            txtSign1 = "Site Engr";

            txtSign2 = "PM /P.Engr";

            txtSign3 = "Sr.PM";

            txtSign4 = "Chief Engineer";

            txtSign5 = "E.D";

            txtSign6 = "Director";

            txtSign7 = "Managing Director/Chairman";


            var list = dtr.DataTableToList<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptReqEntryInns", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Materials Purchase Requisition"));
            Rpt1.SetParameters(new ReportParameter("txtReqNo", txtcrno));
            Rpt1.SetParameters(new ReportParameter("txtReqDate", txtcrdate));
            Rpt1.SetParameters(new ReportParameter("txtMrfno", txtmrfno));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", txtprojectname));
            Rpt1.SetParameters(new ReportParameter("txtAddress", txtAddress));
            Rpt1.SetParameters(new ReportParameter("txtBuildingNo", txtbuildno));
            Rpt1.SetParameters(new ReportParameter("txtFloorNo", txtfloorno));
            Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", txtpforused));
            Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
            Rpt1.SetParameters(new ReportParameter("txttoamt02", txttoamt02));
            Rpt1.SetParameters(new ReportParameter("rpttxtnaration", rpttxtnaration));
            Rpt1.SetParameters(new ReportParameter("txtRptFooter", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtSign1", txtSign1));
            Rpt1.SetParameters(new ReportParameter("txtSign2", txtSign2));
            Rpt1.SetParameters(new ReportParameter("txtSign3", txtSign3));
            Rpt1.SetParameters(new ReportParameter("txtSign4", txtSign4));
            Rpt1.SetParameters(new ReportParameter("txtSign5", txtSign5));
            Rpt1.SetParameters(new ReportParameter("txtSign6", txtSign6));
            Rpt1.SetParameters(new ReportParameter("txtSign7", txtSign7));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptReqEntryInns();
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;

            ////TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
            ////txtcrno.Text = this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            //TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
            //txtcrdate.Text = Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy"); ;

            //TextObject txtmrfno = rptstk.ReportDefinition.ReportObjects["mrfno"] as TextObject;
            //txtmrfno.Text = dt1.Rows[0]["mrfno"].ToString();

            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = dt1.Rows[0]["pactdesc"].ToString();
            ////TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            ////txtAddress.Text = ((DataTable)Session["tblUserReq"]).Rows[0]["paddress"].ToString();//dt.Rows[2]["termsdesc"].ToString(); Session["tblUserReq"]

            //TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            //txtAddress.Text = dt1.Rows[0]["paddress"].ToString();

            //DataTable dt = (DataTable)ViewState["tblreqdesc"];
            ////TextObject txtbuildno = rptstk.ReportDefinition.ReportObjects["txtbuildno"] as TextObject;
            ////txtbuildno.Text = dt.Rows[0]["termsdesc"].ToString();
            ////TextObject txtfloorno = rptstk.ReportDefinition.ReportObjects["txtfloorno"] as TextObject;
            ////txtfloorno.Text = dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString(); ;
            ////TextObject txtflatno = rptstk.ReportDefinition.ReportObjects["txtflatno"] as TextObject;
            ////txtflatno.Text = dt.Rows[2]["termsdesc"].ToString();
            ////TextObject txtpforused = rptstk.ReportDefinition.ReportObjects["txtpforused"] as TextObject;
            ////txtpforused.Text = dt.Rows[3]["termsdesc"].ToString();


            //DataTable dtr = ds1.Tables[0]; ;

            ////double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            ////double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            ////double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;

            ////TextObject txttoamt = rptstk.ReportDefinition.ReportObjects["txttoamt"] as TextObject;
            ////txttoamt.Text = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            ////TextObject txttoamt02 = rptstk.ReportDefinition.ReportObjects["txttoamt02"] as TextObject;
            ////txttoamt02.Text = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");



            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = dt1.Rows[0]["reqnar"].ToString(); ;

            //TextObject txtSign1 = rptstk.ReportDefinition.ReportObjects["txtSign1"] as TextObject;
            //txtSign1.Text = "Site Engr";
            //TextObject txtSign2 = rptstk.ReportDefinition.ReportObjects["txtSign2"] as TextObject;
            //txtSign2.Text = "PM /P.Engr";
            //TextObject txtSign3 = rptstk.ReportDefinition.ReportObjects["txtSign3"] as TextObject;
            //txtSign3.Text = "Sr.PM";
            //TextObject txtSign4 = rptstk.ReportDefinition.ReportObjects["txtSign4"] as TextObject;
            //txtSign4.Text = "Chief Engineer";
            //TextObject txtSign5 = rptstk.ReportDefinition.ReportObjects["txtSign5"] as TextObject;
            //txtSign5.Text = "E.D";
            //TextObject txtSign6 = rptstk.ReportDefinition.ReportObjects["txtSign6"] as TextObject;
            //txtSign6.Text = "Director";
            //TextObject txtSign7 = rptstk.ReportDefinition.ReportObjects["txtSign7"] as TextObject;
            //txtSign7.Text = "Managing Director/Chairman";
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(ds1.Tables[0]);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


        }


        private void PrintRequisition05()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");

            //string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            //string mReqNo = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            string mReqNo = this.Request.QueryString["reqno"].ToString();
            string reqdat = this.Request.QueryString["reqdat"] ?? "";


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, reqdat,
                     "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt1 = ds1.Tables[1];
            DataTable dt2 = ds1.Tables[3];


            string txtcrno = dt1.Rows[0]["reqno1"].ToString(); ;

            string txtcrdate = Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy"); ;

            string txtmrfno = dt1.Rows[0]["mrfno"].ToString(); ;

            string txtprojectname = dt1.Rows[0]["pactdesc"].ToString();
            string txtAddress = dt1.Rows[0]["paddress"].ToString();


            DataTable dt = ds1.Tables[2];


            string txtbuildno = dt.Rows[0]["termsdesc"].ToString();
            string txtfloorno = dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString();
            string txtpforused = dt.Rows[3]["termsdesc"].ToString();

            DataTable dtr = ds1.Tables[0];

            double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;


            string txttoamt = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");

            string txttoamt02 = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            string rpttxtnaration = dt1.Rows[0]["reqnar"].ToString();
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);


            var list = dtr.DataTableToList<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptReqEntryTropical", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Materials Purchase Requisition"));
            Rpt1.SetParameters(new ReportParameter("txtReqNo", txtcrno));
            Rpt1.SetParameters(new ReportParameter("txtReqDate", txtcrdate));
            Rpt1.SetParameters(new ReportParameter("txtMrfno", txtmrfno));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", txtprojectname));
            Rpt1.SetParameters(new ReportParameter("txtAddress", txtAddress));
            Rpt1.SetParameters(new ReportParameter("txtBuildingNo", txtbuildno));
            Rpt1.SetParameters(new ReportParameter("txtFloorNo", txtfloorno));
            Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", txtpforused));
            Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
            Rpt1.SetParameters(new ReportParameter("txttoamt02", txttoamt02));
            Rpt1.SetParameters(new ReportParameter("rpttxtnaration", rpttxtnaration));
            Rpt1.SetParameters(new ReportParameter("txtRptFooter", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtReq", dt2.Rows[0]["reqnam"].ToString() + "\n" + dt2.Rows[0]["reqdat"].ToString()));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptReqEntryTropical();
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;

            //TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
            //txtcrno.Text = dt1.Rows[0]["reqno1"].ToString();
            //TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
            //txtcrdate.Text = Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy");

            //TextObject txtmrfno = rptstk.ReportDefinition.ReportObjects["mrfno"] as TextObject;
            //txtmrfno.Text = dt1.Rows[0]["mrfno"].ToString();


            //TextObject rpttxtReq = rptstk.ReportDefinition.ReportObjects["txtReq"] as TextObject;
            //rpttxtReq.Text = dt2.Rows[0]["reqnam"].ToString() + "\n" + dt2.Rows[0]["reqdat"].ToString();

            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = dt1.Rows[0]["pactdesc"].ToString();
            //TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            ////txtAddress.Text = ((DataTable)Session["tblUserReq"]).Rows[0]["paddress"].ToString() == "" ? "" : ((DataTable)Session["tblUserReq"]).Rows[0]["paddress"].ToString();//dt.Rows[2]["termsdesc"].ToString(); Session["tblUserReq"]
            //txtAddress.Text = dt1.Rows[0]["paddress"].ToString();

            //// DataTable dt = (DataTable)ViewState["tblreqdesc"];

            //DataTable dt = ds1.Tables[2];

            //TextObject txtbuildno = rptstk.ReportDefinition.ReportObjects["txtbuildno"] as TextObject;
            //txtbuildno.Text = dt.Rows[0]["termsdesc"].ToString();
            //TextObject txtfloorno = rptstk.ReportDefinition.ReportObjects["txtfloorno"] as TextObject;
            //txtfloorno.Text = dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString(); ;
            ////TextObject txtflatno = rptstk.ReportDefinition.ReportObjects["txtflatno"] as TextObject;
            ////txtflatno.Text = dt.Rows[2]["termsdesc"].ToString();
            //TextObject txtpforused = rptstk.ReportDefinition.ReportObjects["txtpforused"] as TextObject;
            //txtpforused.Text = dt.Rows[3]["termsdesc"].ToString();


            //// DataTable dtr = (DataTable)ViewState["tblReq"];

            //DataTable dtr = ds1.Tables[0];

            //double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            //double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            //double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;

            //TextObject txttoamt = rptstk.ReportDefinition.ReportObjects["txttoamt"] as TextObject;
            //txttoamt.Text = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            //TextObject txttoamt02 = rptstk.ReportDefinition.ReportObjects["txttoamt02"] as TextObject;
            //txttoamt02.Text = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");



            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = dt1.Rows[0]["reqnar"].ToString();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dtr);
            ////rptstk.SetDataSource((DataTable)ViewState["tblReq"]);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

        }


        private void PrintRequisition06()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");

            //string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            //string mReqNo = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            string mReqNo = this.Request.QueryString["reqno"].ToString();
            string reqdat = this.Request.QueryString["reqdat"].ToString();


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, reqdat,
                     "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt1 = ds1.Tables[1];
            DataTable dt2 = ds1.Tables[3];




            string txtcrno = dt1.Rows[0]["reqno1"].ToString(); ;

            string txtcrdate = Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy"); ;

            string txtmrfno = dt1.Rows[0]["mrfno"].ToString(); ;

            string txtprojectname = dt1.Rows[0]["pactdesc"].ToString();
            string txtAddress = dt1.Rows[0]["paddress"].ToString();


            DataTable dt = ds1.Tables[2];


            string txtbuildno = ((dt.Rows.Count == 0) ? "" : dt.Select("termsid='001'").Length > 0 ? (dt.Select("termsid='001'")[0]["termsdesc"]).ToString() : "");
            string txtfloorno = "";
            //string txtfloorno = dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString();


            string txtpforused = ((dt.Rows.Count == 0) ? "" : dt.Select("termsid='004'").Length > 0 ? (dt.Select("termsid='004'")[0]["termsdesc"]).ToString() : "");

            DataTable dtr = ds1.Tables[0];

            double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;


            string txttoamt = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");

            string txttoamt02 = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            string rpttxtnaration = dt1.Rows[0]["reqnar"].ToString();
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);


            var list = dtr.DataTableToList<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptReqEntryCons", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Materials Purchase Requisition"));
            Rpt1.SetParameters(new ReportParameter("txtReqNo", txtcrno));
            Rpt1.SetParameters(new ReportParameter("txtReqDate", txtcrdate));
            Rpt1.SetParameters(new ReportParameter("txtMrfno", txtmrfno));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", txtprojectname));
            Rpt1.SetParameters(new ReportParameter("txtAddress", txtAddress));
            Rpt1.SetParameters(new ReportParameter("txtBuildingNo", txtbuildno));
            Rpt1.SetParameters(new ReportParameter("txtFloorNo", txtfloorno));
            Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", txtpforused));
            Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
            Rpt1.SetParameters(new ReportParameter("txttoamt02", txttoamt02));
            Rpt1.SetParameters(new ReportParameter("rpttxtnaration", rpttxtnaration));
            Rpt1.SetParameters(new ReportParameter("txtRptFooter", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtReq", dt2.Rows[0]["reqnam"].ToString() + "\n" + dt2.Rows[0]["reqdat"].ToString()));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";



            //ReportDocument rptstk = new RealERPRPT.R_12_Inv.RptReqEntryCons();
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;

            //TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
            //txtcrno.Text = dt1.Rows[0]["reqno1"].ToString();
            //TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
            //txtcrdate.Text = Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy");

            //TextObject txtmrfno = rptstk.ReportDefinition.ReportObjects["mrfno"] as TextObject;
            //txtmrfno.Text = dt1.Rows[0]["mrfno"].ToString();


            //TextObject rpttxtReq = rptstk.ReportDefinition.ReportObjects["txtReq"] as TextObject;
            //rpttxtReq.Text = dt2.Rows[0]["reqnam"].ToString() + "\n" + dt2.Rows[0]["reqdat"].ToString();

            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = dt1.Rows[0]["pactdesc"].ToString();
            //TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            ////txtAddress.Text = ((DataTable)Session["tblUserReq"]).Rows[0]["paddress"].ToString() == "" ? "" : ((DataTable)Session["tblUserReq"]).Rows[0]["paddress"].ToString();//dt.Rows[2]["termsdesc"].ToString(); Session["tblUserReq"]
            //txtAddress.Text = dt1.Rows[0]["paddress"].ToString();


            ////DataTable dt = (DataTable)ViewState["tblreqdesc"];
            //DataTable dt = ds1.Tables[2];

            //string buildno = ((dt.Rows.Count == 0) ? "" : dt.Select("termsid='001'").Length > 0 ? (dt.Select("termsid='001'")[0]["termsdesc"]).ToString() : "");
            //string purpose = ((dt.Rows.Count == 0) ? "" : dt.Select("termsid='004'").Length > 0 ? (dt.Select("termsid='004'")[0]["termsdesc"]).ToString() : "");
            //TextObject txtbuildno = rptstk.ReportDefinition.ReportObjects["txtbuildno"] as TextObject;
            //txtbuildno.Text = buildno;

            //TextObject txtpforused = rptstk.ReportDefinition.ReportObjects["txtpforused"] as TextObject;
            //txtpforused.Text = purpose;

            //// DataTable dtr = (DataTable)ViewState["tblReq"];

            //DataTable dtr = ds1.Tables[0];

            //double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            //double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            //double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;

            //TextObject txttoamt = rptstk.ReportDefinition.ReportObjects["txttoamt"] as TextObject;
            //txttoamt.Text = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            //TextObject txttoamt02 = rptstk.ReportDefinition.ReportObjects["txttoamt02"] as TextObject;
            //txttoamt02.Text = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");



            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = dt1.Rows[0]["reqnar"].ToString();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dtr);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


        }


        private void PrintRequisition09()
        {

            //DataTable dt1 = (DataTable)Session["tblUserReq"]; 
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");


            string mReqNo = this.Request.QueryString["reqno"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, "",
                     "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt1 = ds1.Tables[1];

            string txtcrno = dt1.Rows[0]["reqno1"].ToString(); ;

            string txtcrdate = Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy"); ;

            string txtmrfno = dt1.Rows[0]["mrfno"].ToString(); ;

            string txtprojectname = dt1.Rows[0]["pactdesc"].ToString();
            string txtAddress = dt1.Rows[0]["paddress"].ToString();


            DataTable dt = ds1.Tables[2];
            DataTable dts = ds1.Tables[4];

            string txtbuildno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='001'").Length > 0 ? (dt.Select("termsid='001'")[0]["termsdesc"]).ToString() : ""));
            //string txtbuildno = dt.Rows[0]["termsdesc"].ToString();
            string floorno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='002'").Length > 0 ? (dt.Select("termsid='002'")[0]["termsdesc"]).ToString() : ""));
            string txtfloorno = (dt.Rows.Count == 0) ? "" : (floorno + (dt.Select("termsid='003'").Length > 0 ? (", " + (dt.Select("termsid='003'")[0]["termsdesc"]).ToString()) : ""));

            //  string txtfloorno =(dt.Rows.Count<2)?"": (dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString());

            string txtpforused = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='004'").Length > 0 ? (dt.Select("termsid='004'")[0]["termsdesc"]).ToString() : ""));

            DataTable dtr = ds1.Tables[0];

            double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;



            string txttoamt = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");

            string txttoamt02 = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            string rpttxtnaration = dt1.Rows[0]["reqnar"].ToString();
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string reqcheck = dts.Rows[0]["reqchkname"].ToString();
            string reqrate = dts.Rows[0]["reqratename"].ToString();
            string reqapv = dts.Rows[0]["reqaprname"].ToString();
            string crmcheck = dts.Rows[0]["crmcheckname"].ToString();
            string crmcheckdat = dts.Rows[0]["crmcheckdat"].ToString();

            // req post 
            string postname = dts.Rows[0]["reqpostname"].ToString();
            string postdat = dts.Rows[0]["POSTEDDAT"].ToString();

            //Narration part
            string crmnarr = dts.Rows[0]["crmnarr"].ToString();

            string estnarr = dts.Rows[0]["estnarr"].ToString();




            //Estimate Check


            string reqchkname = dts.Rows[0]["reqchkname"].ToString();
            string checkdat = dts.Rows[0]["checkdat"].ToString();


            // signatory Part 
            /*
            string txtSign1 = "S.K /Project Incharge";
            string txtSign2 = "DPM/PM/AGM/DGM";
            string txtSign3 = "Customer Care";
            string txtSign4 = "Estimation Sec";
            string txtSign5 = "Procrument";
            string txtSign6 = "Managing Director";
             */
            string txtSign1 = postname + "\n" + postdat;
            string txtSign2 = "";
            string txtSign3 = crmcheck + "\n" + crmcheckdat;
            string txtSign4 = reqchkname + "\n" + checkdat;
            string txtSign5 = "";
            string txtSign6 = "";

            var lst = dtr.DataTableToList<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptReqEntry02Assure", lst, null, null);
            Rpt1.EnableExternalImages = true;


            Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));

            Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Materials Purchase Requisition"));
            Rpt1.SetParameters(new ReportParameter("txtReqNo", txtcrno));
            Rpt1.SetParameters(new ReportParameter("txtReqDate", txtcrdate));
            Rpt1.SetParameters(new ReportParameter("txtMrfno", txtmrfno));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", txtprojectname));
            Rpt1.SetParameters(new ReportParameter("txtAddress", txtAddress));
            Rpt1.SetParameters(new ReportParameter("txtBuildingNo", txtbuildno));
            Rpt1.SetParameters(new ReportParameter("txtFloorNo", txtfloorno));
            Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", txtpforused));
            Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
            Rpt1.SetParameters(new ReportParameter("txttoamt02", txttoamt02));
            Rpt1.SetParameters(new ReportParameter("rpttxtnaration", rpttxtnaration));
            Rpt1.SetParameters(new ReportParameter("rpttxtCCDNar", crmnarr));
            Rpt1.SetParameters(new ReportParameter("rpttxtEstNar", estnarr));


            Rpt1.SetParameters(new ReportParameter("txtRptFooter", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtSign1", txtSign1));
            Rpt1.SetParameters(new ReportParameter("txtSign2", txtSign2));
            Rpt1.SetParameters(new ReportParameter("txtSign3", txtSign3));
            Rpt1.SetParameters(new ReportParameter("txtSign4", txtSign4));
            Rpt1.SetParameters(new ReportParameter("txtSign5", txtSign5));
            Rpt1.SetParameters(new ReportParameter("txtSign6", txtSign6));
            //Rpt1.SetParameters(new ReportParameter("txtSign7", txtSign7));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


        }
        private void PrintRequisitioniNTECH()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");


            string mReqNo = this.Request.QueryString["reqno"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, "",
                     "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt1 = ds1.Tables[1];

            string txtcrno = dt1.Rows[0]["reqno1"].ToString(); ;

            string txtcrdate = Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy"); ;

            string txtmrfno = dt1.Rows[0]["mrfno"].ToString(); ;

            string txtprojectname = dt1.Rows[0]["pactdesc"].ToString();
            string txtAddress = dt1.Rows[0]["paddress"].ToString();


            DataTable dt = ds1.Tables[2];
            DataTable dts = ds1.Tables[4];

            string txtbuildno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='001'").Length > 0 ? (dt.Select("termsid='001'")[0]["termsdesc"]).ToString() : ""));
            //string txtbuildno = dt.Rows[0]["termsdesc"].ToString();
            string floorno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='002'").Length > 0 ? (dt.Select("termsid='002'")[0]["termsdesc"]).ToString() : ""));
            string txtfloorno = (dt.Rows.Count == 0) ? "" : (floorno + (dt.Select("termsid='003'").Length > 0 ? (", " + (dt.Select("termsid='003'")[0]["termsdesc"]).ToString()) : ""));

            //  string txtfloorno =(dt.Rows.Count<2)?"": (dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString());

            string txtpforused = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='004'").Length > 0 ? (dt.Select("termsid='004'")[0]["termsdesc"]).ToString() : ""));

            DataTable dtr = ds1.Tables[0];

            double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;



            string txttoamt = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");

            string txttoamt02 = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            string rpttxtnaration = dt1.Rows[0]["reqnar"].ToString();
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string reqcheck = dts.Rows[0]["reqchkname"].ToString();
            string reqrate = dts.Rows[0]["reqratename"].ToString();
            string reqapv = dts.Rows[0]["reqaprname"].ToString();
            string crmcheck = dts.Rows[0]["crmcheckname"].ToString();
            string crmcheckdat = dts.Rows[0]["crmcheckdat"].ToString();

            // req post 
            string postname = dts.Rows[0]["reqpostname"].ToString();
            string postdat = dts.Rows[0]["POSTEDDAT"].ToString();

            //Narration part
            string crmnarr = dts.Rows[0]["crmnarr"].ToString();

            string estnarr = dts.Rows[0]["estnarr"].ToString();




            //Estimate Check


            string reqchkname = dts.Rows[0]["reqchkname"].ToString();
            string checkdat = dts.Rows[0]["checkdat"].ToString();
            string txtSign1 = postname + "\n" + postdat;
            string txtSign2 = "";
            string txtSign3 = crmcheck + "\n" + crmcheckdat;
            string txtSign4 = reqchkname + "\n" + checkdat;
            string txtSign5 = "";
            string txtSign6 = "";

            var lst = dtr.DataTableToList<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptReqEntryiNTECH", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Materials Purchase Requisition"));
            Rpt1.SetParameters(new ReportParameter("txtReqNo", txtcrno));
            Rpt1.SetParameters(new ReportParameter("txtReqDate", txtcrdate));
            Rpt1.SetParameters(new ReportParameter("txtMrfno", txtmrfno));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", txtprojectname));
            Rpt1.SetParameters(new ReportParameter("txtAddress", txtAddress));
            Rpt1.SetParameters(new ReportParameter("txtBuildingNo", txtbuildno));
            Rpt1.SetParameters(new ReportParameter("txtFloorNo", txtfloorno));
            Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", txtpforused));
            Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
            Rpt1.SetParameters(new ReportParameter("txttoamt02", txttoamt02));
            Rpt1.SetParameters(new ReportParameter("rpttxtnaration", rpttxtnaration));
            Rpt1.SetParameters(new ReportParameter("rpttxtCCDNar", crmnarr));
            Rpt1.SetParameters(new ReportParameter("rpttxtEstNar", estnarr));
            Rpt1.SetParameters(new ReportParameter("txtRptFooter", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtPreparedBy", txtSign1));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


        }


        private void PrintRequisitionJBS()
        {

            //DataTable dt1 = (DataTable)Session["tblUserReq"]; 
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");


            string mReqNo = this.Request.QueryString["reqno"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, "",
                     "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt1 = ds1.Tables[1];
            DataTable dt = ds1.Tables[2];


            string txtcrno = dt1.Rows[0]["reqno1"].ToString();
            string txtcrdate = Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy");
            string txtmrfno = dt1.Rows[0]["mrfno"].ToString();
            string txtprojectname = dt1.Rows[0]["pactdesc"].ToString();
            string txtAddress = dt1.Rows[0]["paddress"].ToString();

            string txtbuildno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='001'").Length > 0 ? (dt.Select("termsid='001'")[0]["termsdesc"]).ToString() : ""));
            //string txtbuildno = dt.Rows[0]["termsdesc"].ToString();
            string floorno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='002'").Length > 0 ? (dt.Select("termsid='002'")[0]["termsdesc"]).ToString() : ""));
            string txtfloorno = (dt.Rows.Count == 0) ? "" : (floorno + (dt.Select("termsid='003'").Length > 0 ? (", " + (dt.Select("termsid='003'")[0]["termsdesc"]).ToString()) : ""));

            //  string txtfloorno =(dt.Rows.Count<2)?"": (dt.Rows[1]["termsdesc"].ToString() + ((dt.Rows[2]["termsdesc"].ToString().Length == 0) ? "" : " , ") + dt.Rows[2]["termsdesc"].ToString());

            string txtpforused = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='004'").Length > 0 ? (dt.Select("termsid='004'")[0]["termsdesc"]).ToString() : ""));

            DataTable dtr = ds1.Tables[0];

            double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;



            string txttoamt = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            string txttoamt02 = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            string rpttxtnaration = dt1.Rows[0]["reqnar"].ToString();
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string txtSign1 = "S.K";
            string txtSign2 = "Project Incharge";
            string txtSign3 = "DPM/PM/AGM/DGM";
            string txtSign4 = "Procurement";
            string txtSign5 = "Cost & Budget";
            string txtSign6 = "Head Of Construction";
            string txtSign7 = "Managing Director";


            var lst = dtr.DataTableToList<RealEntity.C_12_Inv.RptMaterialPurchaseRequisition>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptReqEntryJBS", lst, null, null);
            Rpt1.EnableExternalImages = true;


            Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Materials Purchase Requisition"));
            Rpt1.SetParameters(new ReportParameter("txtReqNo", txtcrno));
            Rpt1.SetParameters(new ReportParameter("txtReqDate", txtcrdate));
            Rpt1.SetParameters(new ReportParameter("txtMrfno", txtmrfno));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", txtprojectname));
            Rpt1.SetParameters(new ReportParameter("txtAddress", txtAddress));
            Rpt1.SetParameters(new ReportParameter("txtBuildingNo", txtbuildno));
            Rpt1.SetParameters(new ReportParameter("txtFloorNo", txtfloorno));
            Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", txtpforused));
            Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
            Rpt1.SetParameters(new ReportParameter("txttoamt02", txttoamt02));
            Rpt1.SetParameters(new ReportParameter("rpttxtnaration", rpttxtnaration));
            Rpt1.SetParameters(new ReportParameter("txtRptFooter", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtSign1", txtSign1));
            Rpt1.SetParameters(new ReportParameter("txtSign2", txtSign2));
            Rpt1.SetParameters(new ReportParameter("txtSign3", txtSign3));
            Rpt1.SetParameters(new ReportParameter("txtSign4", txtSign4));
            Rpt1.SetParameters(new ReportParameter("txtSign5", txtSign5));
            Rpt1.SetParameters(new ReportParameter("txtSign6", txtSign6));
            Rpt1.SetParameters(new ReportParameter("txtSign7", txtSign7));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";



        }

        private string PrintCallType()
        {


            string comcod = this.GetCompCode();
            string Calltype = "";
            switch (comcod)
            {
                //case "3301":
                case "1301":
                case "3330":
                    //case "3101":
                    Calltype = "SHOWORKORDER01";
                    break;

                case "3332":
                    // case "3101":
                    Calltype = "SHOWORKORDER02";

                    break;
                default:
                    Calltype = "SHOWORKORDER01";
                    break;
            }
            return Calltype;


        }
        private string GetCompOrderCopy()
        {

            string comcod = this.GetCompCode();
            string ordernocopy = "";
            switch (comcod)
            {
                case "3330":
                    // case "3101":
                    ordernocopy = "Bridge";
                    break;
                // case "3101":
                case "3332":
                    ordernocopy = "Innstar";
                    break;
                default:
                    ordernocopy = "";
                    break;


            }
            return ordernocopy;


        }

        private string Compserial()
        {
            string comcod = this.GetCompCode();
            string comserial = "";
            switch (comcod)
            {
                case "3305":
                case "3306":
                case "3309":
                case "3310":
                case "3311":
                case "2305":
                case "2306":
                case "3101":
                case "1108":// Assure
                case "1109":
                case "3315":// Assure
                case "3316":

                case "3325":
                case "2325":


                    comserial = "Rup";
                    break;

                default:
                    comserial = "";
                    break;


            }
            return comserial;
        }

        private string ComOrderNo(string orderno, string oissueno)
        {
            string comcod = this.GetCompCode();
            string porderno = "";
            switch (comcod)
            {
                case "1108":
                case "1109":
                case "3315":
                case "3316":
                case "3317":
                    //case "3101":
                    porderno = ASTUtility.Right(oissueno, 6);
                    break;

                default:
                    porderno = orderno;
                    break;


            }
            return porderno;
        }
        //Order_Print
        // modify tarik for print rdlc in place of crystal
        private void Order_Print()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3101":
                case "3353":
                case "3355"://green wood
                case "3330":
                case "1205":
                case "3351":
                case "3352":
                case "3336": // shuvastu
                case "3337":
                case "3364": // jbs
                case "3339": // Tropical
                case "3354": // Edison
                case "3366": // Lanco

                case "1108": // assure
                case "1109": // assure
                case "3315": // assure
                case "3316": // assure

                case "3357": // Cube
                case "3367": // Epic
                case "3368": // Finlay

                case "3358": // Entrust
                case "3359": // Entrust
                case "3360": // Entrust
                case "3361": // Entrust 

                case "1206": // acme construction
                case "1207": // acme service
                case "3338": // acme technologies
                case "3369": // acme ai 
                case "3370": // cpdl 


                    this.OrderPrintRDLC();
                    break;

                default:
                    this.Order_printCrystal(); // todo for print crystal
                    break;

            }

        }

        private void Order_printCrystal()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string wrkid = this.Request.QueryString["orderno"].ToString();

                string Calltype = this.PrintCallType();
                string ordercopy = this.GetCompOrderCopy();
                DataSet _ReportDataSet = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", Calltype, wrkid, ordercopy, "", "", "", "", "", "", "");


                ViewState["tblOrder"] = _ReportDataSet.Tables[0];
                ViewState["tblpaysch"] = _ReportDataSet.Tables[5];
                DataTable dt = _ReportDataSet.Tables[0];
                string Para1 = _ReportDataSet.Tables[1].Rows[0]["leterdes"].ToString();
                string Orderdate = Convert.ToDateTime(_ReportDataSet.Tables[1].Rows[0]["orderdat"]).ToString("MMMM  dd, yyyy");
                string SupName = _ReportDataSet.Tables[1].Rows[0]["ssirdesc"].ToString();
                string Address = _ReportDataSet.Tables[1].Rows[0]["address"].ToString();
                string Cperson = _ReportDataSet.Tables[1].Rows[0]["cperson"].ToString();
                string Phone = _ReportDataSet.Tables[1].Rows[0]["phone"].ToString();
                string mobile = _ReportDataSet.Tables[1].Rows[0]["mobile"].ToString();

                string pactcode = _ReportDataSet.Tables[0].Rows[0]["pactcode"].ToString();

                // DataTable dtterm = _ReportDataSet.Tables[2];

                DataTable dtterm = _ReportDataSet.Tables[2];
                DataTable dtord = _ReportDataSet.Tables[4];
                DataTable dtpaycch = _ReportDataSet.Tables[5];

                string orderno = dtord.Rows[0]["orderno"].ToString().Substring(0, 3) + dtord.Rows[0]["orderno"].ToString().Substring(7, 2) + "-" + ASTUtility.Right(dtord.Rows[0]["orderno"].ToString(), 5);
                string oissueno = dtord.Rows[0]["oissueno"].ToString();
                string porderno = this.ComOrderNo(orderno, oissueno);
                string subcom = (comcod == "3351") ? "A Concern of P2P" : "";

                // string Type = this.CompanyPrintWorkOrder();
                ReportDocument rptwork = new ReportDocument();

                string comname = this.Compserial();
                string fax = _ReportDataSet.Tables[1].Rows[0]["fax"].ToString();
                string trmplace = ((comcod == "3338" || comname == "Rup") ? "1. " + dtterm.Rows[0]["termssubj"].ToString() : "*" + dtterm.Rows[0]["termssubj"].ToString() + " : ");
                string place = dtterm.Rows[0]["termsdesc"].ToString().Trim();
                string trmpdate = ((comcod == "3338" || comname == "Rup") ? "2. " + dtterm.Rows[1]["termssubj"].ToString() : "*" + dtterm.Rows[1]["termssubj"].ToString() + " : ");
                string pdate = dtterm.Rows[1]["termsdesc"].ToString().Trim();
                string trmcarring = ((comcod == "3338" || comname == "Rup") ? "3. " + dtterm.Rows[2]["termssubj"].ToString() : "*" + dtterm.Rows[2]["termssubj"].ToString() + " : ");
                string carring = dtterm.Rows[2]["termsdesc"].ToString().Trim();
                string trmbill = (comcod == "3330") ? "" : (comcod == "3338" || comname == "Rup") ? "4. " + (dtterm.Rows[3]["termssubj"]).ToString() : "*" + dtterm.Rows[3]["termssubj"].ToString() + ": ";
                string bill = (comcod == "3330") ? ("* " + dtterm.Rows[3]["termsdesc"].ToString().Trim()) : dtterm.Rows[3]["termsdesc"].ToString().Trim();
                string trmpayment = ((comcod == "3338") ? dtterm.Rows[4]["termssubj"].ToString() : (comname == "Rup") ? "4. " + dtterm.Rows[4]["termssubj"].ToString() + " : " : "*" + dtterm.Rows[4]["termssubj"].ToString() + " : ");
                string payment = dtterm.Rows[4]["termsdesc"].ToString().Trim();

                string trmothers = ((comcod == "3338") ? dtterm.Rows[5]["termssubj"].ToString() : (comname == "Rup") ? "5. " + dtterm.Rows[5]["termssubj"].ToString() + " : " : "*" + dtterm.Rows[5]["termssubj"].ToString() + " : ");
                string Others = dtterm.Rows[5]["termsdesc"].ToString().Trim();


                // For Acme
                //      
                string trmcperson = ((dtterm.Rows.Count == 0) ? "" : dtterm.Select("termsid='010'").Length > 0 ? "* " + (dtterm.Select("termsid='010'")[0]["termssubj"]).ToString() + " : " : "");
                string cperson = ((dtterm.Rows.Count == 0) ? "" : dtterm.Select("termsid='010'").Length > 0 ? (dtterm.Select("termsid='010'")[0]["termsdesc"]).ToString() : ""); ;

                //only Rupayan group 


                string tmflatno = "";
                string flatno1 = "";
                string tmflatower = "";
                string flatower = "";
                string tmthersflat = "";
                string otherflats = "";
                string tmcontact = "";
                string contact = "";




                if (comcod == "3305" || comcod == "2305" || comcod == "3306" || comcod == "3309" || comcod == "3310" || comcod == "3311" || comcod == "2306")
                {

                    tmflatno = (dtterm.Rows.Count <= 6) ? "" : ((comname == "Rup") ? "6. " + dtterm.Rows[6]["termssubj"].ToString() + " : " : "*" + dtterm.Rows[6]["termssubj"].ToString() + " : ");  //("* " + dtterm.Rows[6]["termssubj"].ToString().Trim() + " : ");
                    flatno1 = (dtterm.Rows.Count <= 6) ? "" : (dtterm.Rows[6]["termsdesc"].ToString().Trim());
                    tmflatower = (dtterm.Rows.Count <= 6) ? "" : ((comname == "Rup") ? "6. " + dtterm.Rows[7]["termssubj"].ToString() + " : " : "*" + dtterm.Rows[7]["termssubj"].ToString() + " : "); //("* " + dtterm.Rows[7]["termssubj"].ToString().Trim() + " : ");
                    flatower = (dtterm.Rows.Count <= 6) ? "" : (dtterm.Rows[7]["termsdesc"].ToString().Trim());
                    tmthersflat = (dtterm.Rows.Count <= 6) ? "" : ((comname == "Rup") ? "7. " + dtterm.Rows[8]["termssubj"].ToString() + " : " : "*" + dtterm.Rows[8]["termssubj"].ToString() + " : "); // ("* " + dtterm.Rows[8]["termssubj"].ToString().Trim() + " : ");
                    otherflats = (dtterm.Rows.Count <= 6) ? "" : (dtterm.Rows[8]["termsdesc"].ToString().Trim());
                    tmcontact = (dtterm.Rows.Count <= 6) ? "" : ((comname == "Rup") ? "8. " + dtterm.Rows[9]["termssubj"].ToString() + " : " : "*" + dtterm.Rows[9]["termssubj"].ToString() + " : "); // ("* " + dtterm.Rows[9]["termssubj"].ToString().Trim() + " : ");
                    contact = (dtterm.Rows.Count <= 6) ? "" : (dtterm.Rows[9]["termsdesc"].ToString().Trim());

                }


                ///


                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURORDERINFO", wrkid, "", "", "", "", "", "", "", "");

                ViewState["tblOrder1"] = ds1.Tables[0];

                DataTable dtorder1 = (DataTable)ViewState["tblOrder1"];
                DataTable dt1;
                DataTable dt2;
                DataTable dt3;

                DataTable dt4;

                // Carring
                DataView dv1 = dtorder1.DefaultView;
                dv1.RowFilter = ("rsircode  like '019999901%'");
                dt1 = dv1.ToTable();

                //Deduction
                dv1 = dtorder1.DefaultView;
                dv1.RowFilter = ("rsircode like'019999902%' ");
                dt2 = dv1.ToTable();

                //Material
                dv1 = dtorder1.DefaultView;
                dv1.RowFilter = ("rsircode not like '0199999%'");
                dt3 = dv1.ToTable();

                string discountdesc = dtorder1.Select("rsircode like '019999902003%'").Length == 0 ? "Discount" : dtorder1.Select("rsircode like '019999902003%'")[0]["rsirdesc1"].ToString();



                switch (comcod)
                {


                    case "3305":  //Rupayan group
                    case "3306":
                    case "3309":
                    case "3310":
                    case "3311":
                    case "2305":
                    case "2306":
                        // case "3101":

                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderRupayan();
                        //          
                        TextObject rptFlatnoru = rptwork.ReportDefinition.ReportObjects["flat"] as TextObject;
                        rptFlatnoru.Text = (tmflatno.Length > 0) ? tmflatno + flatno1 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection2"].SectionFormat.EnableSuppress = (tmflatno.Length > 0) ? false : true;

                        //TextObject rptvat = rptwork.ReportDefinition.ReportObjects["vat"] as TextObject;
                        //rptvat.Text = (vat1.Length > 0) ? vat1 + vat1 : "";

                        //rptwork.ReportDefinition.Sections["GroupFooterSection13"].SectionFormat.EnableSuppress = (vat1.Length > 0) ? false : true;

                        TextObject rptOtherru = rptwork.ReportDefinition.ReportObjects["Other"] as TextObject;
                        rptOtherru.Text = (tmthersflat.Length > 0) ? tmthersflat + otherflats : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection4"].SectionFormat.EnableSuppress = (tmflatno.Length > 0) ? false : true;

                        TextObject rptContactpru = rptwork.ReportDefinition.ReportObjects["contactp"] as TextObject;
                        rptContactpru.Text = (tmcontact.Length > 0) ? tmcontact + contact : "";

                        rptwork.ReportDefinition.Sections["GroupFooterSection8"].SectionFormat.EnableSuppress = (tmflatno.Length > 0) ? false : true;
                        break;


                    case "3332":
                        //case "3101":
                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderInstar();
                        TextObject rpttxtReqIns = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReqIns.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqAppIns = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqAppIns.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrdIns = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrdIns.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWordIns = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWordIns.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtcheckIns = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
                        rpttxtcheckIns.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();


                        break;


                    case "3336":
                    case "3337":


                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderSuvastu();


                        //Sign In
                        TextObject txtatt = rptwork.ReportDefinition.ReportObjects["txtatt"] as TextObject;
                        txtatt.Text = Cperson;

                        TextObject rpttxtReqSuv = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReqSuv.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqAppSuv = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqAppSuv.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrdSuv = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrdSuv.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWordSuv = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWordSuv.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtcheckSuv = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
                        rpttxtcheckSuv.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        TextObject txtAdvancedSuv = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvancedSuv.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); "); ;
                        TextObject txtappbySuv = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappbySuv.Text = "Approved By";

                        TextObject txtmoblieNumber = rptwork.ReportDefinition.ReportObjects["txtmoblieNumber"] as TextObject;
                        txtmoblieNumber.Text = mobile;

                        //txtappby.Text = (comcod == "3335") ? "Head of Procurement" : "Approved By";

                        // sign end 
                        break;

                    case "1206":
                    case "1207":
                    case "3338":
                    case "3369":
                        // case "3101":

                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderAcme();


                        //Sign In


                        TextObject rpttxtReqAcme = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReqAcme.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqAppAcme = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqAppAcme.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrdAcme = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrdAcme.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWordAcme = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWordAcme.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtcheckAcme = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;

                        TextObject txtreqfaprname = rptwork.ReportDefinition.ReportObjects["txtreqfaprname"] as TextObject;
                        txtreqfaprname.Text = _ReportDataSet.Tables[3].Rows[0]["reqfaprname"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqfaprdat"].ToString();

                        rpttxtcheckAcme.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        TextObject txtAdvancedAcme = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvancedAcme.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); "); ;
                        //TextObject txtappbyAcme = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        //txtappbyAcme.Text = "Approved By";

                        TextObject txtPhoneNumber = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber.Text = Phone;

                        // Section part

                        string Others7 = dtterm.Select("termsid='007'").Length > 0 ? ((dtterm.Select("termsid='007'")[0]["termsdesc"]).ToString()) : "";

                        string Others8 = dtterm.Select("termsid='008'").Length > 0 ? ((dtterm.Select("termsid='008'")[0]["termsdesc"]).ToString()) : "";

                        string Others9 = dtterm.Select("termsid='009'").Length > 0 ? ((dtterm.Select("termsid='009'")[0]["termsdesc"]).ToString()) : "";
                        string Others11 = dtterm.Select("termsid='011'").Length > 0 ? ((dtterm.Select("termsid='011'")[0]["termsdesc"]).ToString()) : "";
                        string Others12 = dtterm.Select("termsid='012'").Length > 0 ? ((dtterm.Select("termsid='012'")[0]["termsdesc"]).ToString()) : "";
                        string Others13 = dtterm.Select("termsid='013'").Length > 0 ? ((dtterm.Select("termsid='013'")[0]["termsdesc"]).ToString()) : "";
                        string Others14 = dtterm.Select("termsid='014'").Length > 0 ? ((dtterm.Select("termsid='014'")[0]["termsdesc"]).ToString()) : "";
                        string Others15 = dtterm.Select("termsid='015'").Length > 0 ? ((dtterm.Select("termsid='015'")[0]["termsdesc"]).ToString()) : "";
                        string Others16 = dtterm.Select("termsid='016'").Length > 0 ? ((dtterm.Select("termsid='016'")[0]["termsdesc"]).ToString()) : "";









                        TextObject txtothers7 = rptwork.ReportDefinition.ReportObjects["others1"] as TextObject;
                        txtothers7.Text = (Others7.Length > 0) ? Others7 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection15"].SectionFormat.EnableSuppress = (Others7.Length > 0) ? false : true;

                        TextObject txtothers8 = rptwork.ReportDefinition.ReportObjects["others2"] as TextObject;
                        txtothers8.Text = (Others8.Length > 0) ? Others8 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection16"].SectionFormat.EnableSuppress = (Others8.Length > 0) ? false : true;

                        TextObject txtothers9 = rptwork.ReportDefinition.ReportObjects["others3"] as TextObject;
                        txtothers9.Text = (Others9.Length > 0) ? Others9 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection17"].SectionFormat.EnableSuppress = (Others9.Length > 0) ? false : true;

                        TextObject txtothers10 = rptwork.ReportDefinition.ReportObjects["others4"] as TextObject;
                        txtothers10.Text = (Others11.Length > 0) ? Others11 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection18"].SectionFormat.EnableSuppress = (Others11.Length > 0) ? false : true;
                        TextObject txtothers12 = rptwork.ReportDefinition.ReportObjects["others5"] as TextObject;
                        txtothers12.Text = (Others11.Length > 0) ? Others12 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection19"].SectionFormat.EnableSuppress = (Others12.Length > 0) ? false : true;
                        TextObject txtothers13 = rptwork.ReportDefinition.ReportObjects["others6"] as TextObject;
                        txtothers13.Text = (Others13.Length > 0) ? Others13 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection20"].SectionFormat.EnableSuppress = (Others13.Length > 0) ? false : true;
                        TextObject txtothers14 = rptwork.ReportDefinition.ReportObjects["others7"] as TextObject;
                        txtothers14.Text = (Others14.Length > 0) ? Others14 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection21"].SectionFormat.EnableSuppress = (Others14.Length > 0) ? false : true;
                        TextObject txtothers15 = rptwork.ReportDefinition.ReportObjects["others8"] as TextObject;
                        txtothers15.Text = (Others14.Length > 0) ? Others15 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection22"].SectionFormat.EnableSuppress = (Others15.Length > 0) ? false : true;

                        TextObject txtothers16 = rptwork.ReportDefinition.ReportObjects["others9"] as TextObject;
                        txtothers16.Text = (Others16.Length > 0) ? Others16 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection23"].SectionFormat.EnableSuppress = (Others16.Length > 0) ? false : true;

                        //TextObject txtothers10 = rptwork.ReportDefinition.ReportObjects["others10"] as TextObject;
                        //txtothers10.Text = (Others10.Length > 0) ?"*"+ Others10 : "";
                        //rptwork.ReportDefinition.Sections["GroupFooterSection24"].SectionFormat.EnableSuppress = (Others10.Length > 0) ? false : true;


                        break;

                    case "1205":
                    case "3351":
                    case "3352":

                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderP2P();


                        //Sign In



                        TextObject rpttxtReqp2p = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReqp2p.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();

                        TextObject rpttxtReqAppp2p = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqAppp2p.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();

                        //TextObject rpttxtOrdp2p = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        //rpttxtOrdp2p.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();

                        // todo for order process  start
                        TextObject rpttxtOrdPp2p = rptwork.ReportDefinition.ReportObjects["txtOrdP"] as TextObject;
                        rpttxtOrdPp2p.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        // todo for order process end

                        TextObject rpttxtWordp2p = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWordp2p.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();


                        //TextObject txtreqfaprnamep2p = rptwork.ReportDefinition.ReportObjects["txtreqfaprname"] as TextObject;
                        //txtreqfaprnamep2p.Text = _ReportDataSet.Tables[3].Rows[0]["reqfaprname"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqfaprdat"].ToString();

                        TextObject rpttxtcheckp2p = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
                        rpttxtcheckp2p.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();

                        TextObject txtAdvancedp2p = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvancedp2p.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); "); ;

                        //TextObject txtappbyAcme = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        //txtappbyAcme.Text = "Approved By";

                        //txtRateProposal
                        TextObject rpttxtRatePp2p = rptwork.ReportDefinition.ReportObjects["txtRatep"] as TextObject;
                        rpttxtRatePp2p.Text = _ReportDataSet.Tables[3].Rows[0]["ratepname"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["ratepdate"].ToString();


                        TextObject txtPhoneNumberp2p = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumberp2p.Text = Phone;

                        TextObject txtsubcom = rptwork.ReportDefinition.ReportObjects["txtsubcom"] as TextObject;
                        txtsubcom.Text = subcom;


                        // Section part






                        string Others7p2p = dtterm.Select("termsid='007'").Length > 0 ? ((dtterm.Select("termsid='007'")[0]["termsdesc"]).ToString()) : "";

                        string Others8p2p = dtterm.Select("termsid='008'").Length > 0 ? ((dtterm.Select("termsid='008'")[0]["termsdesc"]).ToString()) : "";

                        string Others9p2p = dtterm.Select("termsid='009'").Length > 0 ? ((dtterm.Select("termsid='009'")[0]["termsdesc"]).ToString()) : "";
                        string Others10p2p = dtterm.Select("termsid='010'").Length > 0 ? ((dtterm.Select("termsid='010'")[0]["termsdesc"]).ToString()) : "";
                        string Others11p2p = dtterm.Select("termsid='011'").Length > 0 ? ((dtterm.Select("termsid='011'")[0]["termsdesc"]).ToString()) : "";
                        string Others12p2p = dtterm.Select("termsid='012'").Length > 0 ? ((dtterm.Select("termsid='012'")[0]["termsdesc"]).ToString()) : "";
                        string Others13p2p = dtterm.Select("termsid='013'").Length > 0 ? ((dtterm.Select("termsid='013'")[0]["termsdesc"]).ToString()) : "";
                        string Others14p2p = dtterm.Select("termsid='014'").Length > 0 ? ((dtterm.Select("termsid='014'")[0]["termsdesc"]).ToString()) : "";
                        string Others15p2p = dtterm.Select("termsid='015'").Length > 0 ? ((dtterm.Select("termsid='015'")[0]["termsdesc"]).ToString()) : "";
                        // string Others16p2p = dtterm.Select("termsid='016'").Length > 0 ? ((dtterm.Select("termsid='016'")[0]["termsdesc"]).ToString()) : "";    


                        string Others7p2pt = dtterm.Select("termsid='007'").Length > 0 ? "7. " + ((dtterm.Select("termsid='007'")[0]["termssubj"]).ToString()) : "";

                        string Others8p2pt = dtterm.Select("termsid='008'").Length > 0 ? "8. " + ((dtterm.Select("termsid='008'")[0]["termssubj"]).ToString()) : "";

                        string Others9p2pt = dtterm.Select("termsid='009'").Length > 0 ? "9. " + ((dtterm.Select("termsid='009'")[0]["termssubj"]).ToString()) : "";
                        string Others10p2pt = dtterm.Select("termsid='010'").Length > 0 ? "10. " + ((dtterm.Select("termsid='010'")[0]["termssubj"]).ToString()) : "";
                        string Others11p2pt = dtterm.Select("termsid='011'").Length > 0 ? "11. " + ((dtterm.Select("termsid='011'")[0]["termssubj"]).ToString()) : "";
                        string Others12p2pt = dtterm.Select("termsid='012'").Length > 0 ? "12. " + ((dtterm.Select("termsid='012'")[0]["termssubj"]).ToString()) : "";
                        string Others13p2pt = dtterm.Select("termsid='013'").Length > 0 ? "13. " + ((dtterm.Select("termsid='013'")[0]["termssubj"]).ToString()) : "";
                        string Others14p2pt = dtterm.Select("termsid='014'").Length > 0 ? "14. " + ((dtterm.Select("termsid='014'")[0]["termssubj"]).ToString()) : "";
                        string Others15p2pt = dtterm.Select("termsid='015'").Length > 0 ? "15. " + ((dtterm.Select("termsid='015'")[0]["termssubj"]).ToString()) : "";









                        TextObject txtothers7p2p = rptwork.ReportDefinition.ReportObjects["others1"] as TextObject;
                        txtothers7p2p.Text = (Others7p2p.Length > 0) ? Others7p2pt + " " + Others7p2p : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection15"].SectionFormat.EnableSuppress = (Others7p2p.Length > 0) ? false : true;

                        TextObject txtothers8p2p = rptwork.ReportDefinition.ReportObjects["others2"] as TextObject;
                        txtothers8p2p.Text = (Others8p2p.Length > 0) ? Others8p2pt + " " + Others8p2p : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection16"].SectionFormat.EnableSuppress = (Others8p2p.Length > 0) ? false : true;

                        TextObject txtothers9p2p = rptwork.ReportDefinition.ReportObjects["others3"] as TextObject;
                        txtothers9p2p.Text = (Others9p2p.Length > 0) ? Others9p2pt + " " + Others9p2p : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection17"].SectionFormat.EnableSuppress = (Others9p2p.Length > 0) ? false : true;

                        TextObject txtothers10p2p = rptwork.ReportDefinition.ReportObjects["others4"] as TextObject;
                        txtothers10p2p.Text = (Others10p2p.Length > 0) ? Others10p2pt + " " + Others10p2p : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection18"].SectionFormat.EnableSuppress = (Others10p2p.Length > 0) ? false : true;
                        TextObject txtothers11p2p = rptwork.ReportDefinition.ReportObjects["others5"] as TextObject;
                        txtothers11p2p.Text = (Others11p2p.Length > 0) ? Others11p2pt + " " + Others11p2p : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection19"].SectionFormat.EnableSuppress = (Others11p2p.Length > 0) ? false : true;
                        TextObject txtothers12p2p = rptwork.ReportDefinition.ReportObjects["others6"] as TextObject;
                        txtothers12p2p.Text = (Others12p2p.Length > 0) ? Others12p2pt + " " + Others12p2p : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection20"].SectionFormat.EnableSuppress = (Others12p2p.Length > 0) ? false : true;
                        TextObject txtothers13p2p = rptwork.ReportDefinition.ReportObjects["others7"] as TextObject;
                        txtothers13p2p.Text = (Others13p2p.Length > 0) ? Others13p2pt + " " + Others13p2p : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection21"].SectionFormat.EnableSuppress = (Others13p2p.Length > 0) ? false : true;
                        TextObject txtothers14p2p = rptwork.ReportDefinition.ReportObjects["others8"] as TextObject;
                        txtothers14p2p.Text = (Others14p2p.Length > 0) ? Others14p2pt + " " + Others14p2p : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection22"].SectionFormat.EnableSuppress = (Others14p2p.Length > 0) ? false : true;
                        TextObject txtothers15p2p = rptwork.ReportDefinition.ReportObjects["others9"] as TextObject;
                        txtothers15p2p.Text = (Others15p2p.Length > 0) ? Others15p2pt + " " + Others15p2p : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection23"].SectionFormat.EnableSuppress = (Others15p2p.Length > 0) ? false : true;

                        //TextObject txtothers16p2p = rptwork.ReportDefinition.ReportObjects["others10"] as TextObject;
                        //txtothers16p2p.Text = (Others16p2p.Length > 0) ? Others16p2p : "";
                        //rptwork.ReportDefinition.Sections["GroupFooterSection23"].SectionFormat.EnableSuppress = (Others16p2p.Length > 0) ? false : true;

                        //TextObject txtothers10 = rptwork.ReportDefinition.ReportObjects["others10"] as TextObject;
                        //txtothers10.Text = (Others10.Length > 0) ?"*"+ Others10 : "";
                        //rptwork.ReportDefinition.Sections["GroupFooterSection24"].SectionFormat.EnableSuppress = (Others10.Length > 0) ? false : true;


                        break;





                    case "3325":
                    case "2325":

                        string tmflatnolei = (dtterm.Rows.Count <= 6) ? "" : "*" + dtterm.Rows[6]["termssubj"].ToString();  //("* " + dtterm.Rows[6]["termssubj"].ToString().Trim() + " : ");
                        string flatno1lei = (dtterm.Rows.Count <= 6) ? "" : (dtterm.Rows[6]["termsdesc"].ToString().Trim());
                        string tmflatowerlei = (dtterm.Rows.Count <= 6) ? "" : "*" + dtterm.Rows[7]["termssubj"].ToString(); //("* " + dtterm.Rows[7]["termssubj"].ToString().Trim() + " : ");
                        string flatowerlei = (dtterm.Rows.Count <= 6) ? "" : (dtterm.Rows[7]["termsdesc"].ToString().Trim());
                        string tmthersflatlei = (dtterm.Rows.Count <= 6) ? "" : "*" + dtterm.Rows[8]["termssubj"].ToString(); // ("* " + dtterm.Rows[8]["termssubj"].ToString().Trim() + " : ");
                        string otherflatslei = (dtterm.Rows.Count <= 6) ? "" : (dtterm.Rows[8]["termsdesc"].ToString().Trim());
                        string tmcontactlei = (dtterm.Rows.Count <= 6) ? "" : "*" + dtterm.Rows[9]["termssubj"].ToString(); // ("* " + dtterm.Rows[9]["termssubj"].ToString().Trim() + " : ");
                        string contactlei = (dtterm.Rows.Count <= 6) ? "" : (dtterm.Rows[9]["termsdesc"].ToString().Trim());
                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderLeisure();
                        //          
                        TextObject rptFlatno1 = rptwork.ReportDefinition.ReportObjects["flat"] as TextObject;
                        rptFlatno1.Text = (tmflatnolei.Length > 0) ? tmflatnolei + flatno1lei : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection2"].SectionFormat.EnableSuppress = (tmflatno.Length > 0) ? false : true;

                        TextObject rptFlatOwner1 = rptwork.ReportDefinition.ReportObjects["flatOwner"] as TextObject;
                        rptFlatOwner1.Text = (tmflatowerlei.Length > 0) ? tmflatowerlei + flatowerlei : "";

                        rptwork.ReportDefinition.Sections["GroupFooterSection3"].SectionFormat.EnableSuppress = (tmflatowerlei.Length > 0) ? false : true;

                        TextObject rptOther1 = rptwork.ReportDefinition.ReportObjects["Other"] as TextObject;
                        rptOther1.Text = (tmthersflatlei.Length > 0) ? tmthersflatlei + otherflatslei : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection4"].SectionFormat.EnableSuppress = (tmflatnolei.Length > 0) ? false : true;

                        TextObject rptContactp1 = rptwork.ReportDefinition.ReportObjects["contactp"] as TextObject;
                        rptContactp1.Text = (tmcontactlei.Length > 0) ? tmcontactlei + contactlei : "";

                        rptwork.ReportDefinition.Sections["GroupFooterSection8"].SectionFormat.EnableSuppress = (tmflatnolei.Length > 0) ? false : true;
                        break;
                    //case "3336":
                    // case "3101":
                    case "3340": // Urban
                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderudller();


                        //Sign In


                        TextObject rpttxtReqb = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReqb.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqAppb = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqAppb.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrdb = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrdb.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWordb = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWordb.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtcheckb = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
                        rpttxtcheckb.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        TextObject txtAdvancedb = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvancedb.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); "); ;
                        TextObject txtappbyb = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappbyb.Text = "Approved By";

                        TextObject txtPhoneNumber2b = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber2b.Text = Phone;
                        TextObject txtconcernperson3b = rptwork.ReportDefinition.ReportObjects["txtconcernperson"] as TextObject;
                        txtconcernperson3b.Text = (cperson.Length > 0) ? cperson : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection3"].SectionFormat.EnableSuppress = (cperson.Length > 0) ? false : true;


                        break;

                    //case "3336":

                    case "3339":
                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderTropical();

                        TextObject rpttxtReqtro = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReqtro.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqApptro = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqApptro.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrdtro = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrdtro.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWordtro = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWordtro.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtchecktro = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
                        rpttxtchecktro.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        TextObject txtAdvancedtro = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvancedtro.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); "); ;
                        TextObject txtappbytro = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappbytro.Text = "Approved By";

                        TextObject txtPhoneNumber2tro = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber2tro.Text = Phone;
                        TextObject txtconcernperson3tro = rptwork.ReportDefinition.ReportObjects["txtconcernperson"] as TextObject;
                        txtconcernperson3tro.Text = (cperson.Length > 0) ? cperson : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection3"].SectionFormat.EnableSuppress = (cperson.Length > 0) ? false : true;

                        break;





                    //case "3101":
                    case "3335":
                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderEdison();


                        //Sign In


                        TextObject rpttxtReqe = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReqe.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqAppe = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqAppe.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrde = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrde.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWorde = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWorde.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtchecke = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
                        rpttxtchecke.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        TextObject txtAdvancede = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvancede.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); "); ;
                        TextObject txtappbye = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappbye.Text = _ReportDataSet.Tables[3].Rows[0]["ordappnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["ordappdat"].ToString(); ;

                        TextObject txtPhoneNumber2e = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber2e.Text = Phone;
                        break;


                    case "1108": // Assure
                    case "1109":// Assure
                    case "3315": // Assure
                    case "3316":// Assure
                                //case "3101":
                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderAssure();

                        TextObject txtAdvancedass = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvancedass.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); ");  //avdamt; //ToString("#,##0.00;(#,##0.00);");
                        rptwork.ReportDefinition.Sections["GroupFooterSection3"].SectionFormat.EnableSuppress = ((_ReportDataSet.Tables[6]).Rows.Count > 0) ? false : true;
                        rptwork.Subreports["RptOrderPaymentAssure.rpt"].SetDataSource(_ReportDataSet.Tables[6]);

                        break;


                    //case "3101": // 160100010004 // WIP-HASNAHENA
                    case "3330": // Bridge and Bridge with Jinnah
                        if (pactcode == "160100010025" || pactcode == "160100010027")
                        {
                            rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderWithoutLogoBR();
                        }
                        else
                        {
                            rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderBridge();
                        }

                        //rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderBridge();
                        TextObject rpttxtReqbr = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReqbr.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqAppbr = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqAppbr.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrdbr = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrdbr.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWordbr = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWordbr.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtcheckbr = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
                        rpttxtcheckbr.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        TextObject txtAdvancedbr = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvancedbr.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); "); ;
                        TextObject txtappbybr = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappbybr.Text = "Approved By";

                        TextObject txtPhoneNumber2br = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber2br.Text = Phone;
                        TextObject txtdiscountdescbr = rptwork.ReportDefinition.ReportObjects["txtdiscountdesc"] as TextObject;
                        txtdiscountdescbr.Text = discountdesc;


                        //txtappby.Text = (comcod == "3335") ? "Head of Procurement" : "Approved By";

                        break;


                    case "3353":
                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderManama();
                        //Sign In


                        TextObject rpttxtReqm = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReqm.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        //TextObject rpttxtReqAppm = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        //rpttxtReqAppm.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        //TextObject rpttxtOrdm = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        //rpttxtOrdm.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        //TextObject rpttxtWordm = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        //rpttxtWordm.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtcheckm = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
                        rpttxtcheckm.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        //TextObject txtAdvancedm = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        //txtAdvancedm.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); "); ;
                        TextObject txtappbym = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappbym.Text = "Approved By";

                        TextObject txtPhoneNumber2m = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber2m.Text = Phone;
                        TextObject txtdiscountdescm = rptwork.ReportDefinition.ReportObjects["txtdiscountdesc"] as TextObject;
                        txtdiscountdescm.Text = discountdesc;
                        break;


                    default:
                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderBridge();
                        //Sign In


                        TextObject rpttxtReq = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReq.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqApp = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqApp.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrd = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrd.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWord = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWord.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtcheck = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
                        rpttxtcheck.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        TextObject txtAdvanced = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvanced.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); "); ;
                        TextObject txtappby = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappby.Text = "Approved By";

                        TextObject txtPhoneNumber2 = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber2.Text = Phone;
                        TextObject txtdiscountdesc = rptwork.ReportDefinition.ReportObjects["txtdiscountdesc"] as TextObject;
                        txtdiscountdesc.Text = discountdesc;


                        //txtappby.Text = (comcod == "3335") ? "Head of Procurement" : "Approved By";

                        // sign end 
                        break;


                }
                // todo for bridge jinnah project

                if (comcod == "3330" && (pactcode == "160100010025" || pactcode == "160100010027"))
                {

                }
                else
                {
                    TextObject txtCompany = rptwork.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
                    txtCompany.Text = comnam;
                    TextObject txtAddress = rptwork.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
                    txtAddress.Text = comadd;
                }


                TextObject txtsubject = rptwork.ReportDefinition.ReportObjects["txtsubject"] as TextObject;
                txtsubject.Text = dtord.Rows[0]["subject"].ToString();

                // todo for bridge new project

                //TextObject txtCompany = rptwork.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
                //txtCompany.Text = comnam;
                //TextObject txtAddress = rptwork.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
                //txtAddress.Text = comadd;

                TextObject rptpurno = rptwork.ReportDefinition.ReportObjects["purno"] as TextObject;
                rptpurno.Text = porderno;
                TextObject rptRefno = rptwork.ReportDefinition.ReportObjects["refno"] as TextObject;
                rptRefno.Text = dtord.Rows[0]["pordref"].ToString();
                TextObject supname = rptwork.ReportDefinition.ReportObjects["supname"] as TextObject;
                supname.Text = SupName;
                TextObject Supadd = rptwork.ReportDefinition.ReportObjects["saddress"] as TextObject;
                Supadd.Text = Address;

                //TextObject Fax = rptwork.ReportDefinition.ReportObjects["txtfax"] as TextObject;
                //Fax.Text =  fax;
                TextObject rptpurdate = rptwork.ReportDefinition.ReportObjects["txtOrderDate"] as TextObject;
                rptpurdate.Text = Orderdate;
                TextObject rptPara1 = rptwork.ReportDefinition.ReportObjects["TxtLETERDES"] as TextObject;
                rptPara1.Text = Para1;
                TextObject rptplace = rptwork.ReportDefinition.ReportObjects["place"] as TextObject;
                rptplace.Text = (place.Length > 0) ? trmplace + place : "";

                rptwork.ReportDefinition.Sections["GroupFooterSection5"].SectionFormat.EnableSuppress = (dtpaycch.Rows.Count > 0) ? false : true;


                TextObject rpttxtsupplydetails = rptwork.ReportDefinition.ReportObjects["txtsupplydetails"] as TextObject;
                rpttxtsupplydetails.Text = dtord.Rows[0]["pordnar"].ToString();
                rptwork.ReportDefinition.Sections["GroupFooterSection9"].SectionFormat.EnableSuppress = (place.Length > 0) ? false : true;


                TextObject rptpdate = rptwork.ReportDefinition.ReportObjects["pdate"] as TextObject;
                rptpdate.Text = (pdate.Length > 0) ? trmpdate + pdate : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection10"].SectionFormat.EnableSuppress = (pdate.Length > 0) ? false : true;


                TextObject rptcarring = rptwork.ReportDefinition.ReportObjects["carring"] as TextObject;
                rptcarring.Text = (carring.Length > 0) ? trmcarring + carring : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection11"].SectionFormat.EnableSuppress = (carring.Length > 0) ? false : true;


                TextObject rptpbill = rptwork.ReportDefinition.ReportObjects["bill"] as TextObject;
                rptpbill.Text = (bill.Length > 0) ? trmbill + bill : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection12"].SectionFormat.EnableSuppress = (bill.Length > 0) ? false : true;

                TextObject rptpayment1 = rptwork.ReportDefinition.ReportObjects["payment1"] as TextObject;
                rptpayment1.Text = (payment.Length > 0) ? trmpayment + payment : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection13"].SectionFormat.EnableSuppress = (payment.Length > 0) ? false : true;

                if (comname != "Rup")
                {
                    TextObject txtconcernperson = rptwork.ReportDefinition.ReportObjects["txtconcernperson"] as TextObject;
                    txtconcernperson.Text = (cperson.Length > 0) ? cperson : "";
                    rptwork.ReportDefinition.Sections["GroupFooterSection3"].SectionFormat.EnableSuppress = (cperson.Length > 0) ? false : true;

                }

                TextObject rptOthrs = rptwork.ReportDefinition.ReportObjects["others"] as TextObject;
                rptOthrs.Text = (Others.Length > 0) ? trmothers + Others : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection14"].SectionFormat.EnableSuppress = (Others.Length > 0) ? false : true;


                ////Adjustment
                //dv1 = dtorder1.DefaultView;
                //dv1.RowFilter = ("rsircode like'019999902003%'");
                //dt4 = dv1.ToTable();



                double amtcar = (dt1.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(ordramt)", "")) ? 0.00 : dt1.Compute("Sum(ordramt)", "")));

                double amtdis = (dt2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(ordramt)", "")) ? 0.00 : dt2.Compute("Sum(ordramt)", "")));
                //

                // double amtadjustment = (dt2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(ordramt)", "")) ? 0.00 : dt4.Compute("Sum(ordramt)", "")));


                double amtmat = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(ordramt)", "")) ? 0.00 : dt3.Compute("Sum(ordramt)", "")));

                TextObject txtcarcost = rptwork.ReportDefinition.ReportObjects["txtcarcost"] as TextObject;
                txtcarcost.Text = amtcar.ToString("#,##0.00;(#,##0.00);");



                //if (comcod == "3330" || comcod == "3101")
                //{
                //    TextObject txtadjustmentamt = rptwork.ReportDefinition.ReportObjects["Text42"] as TextObject;
                //    txtadjustmentamt.Text = amtadjustment.ToString("#,##0.00;(#,##0.00);");

                //}


                TextObject txtdiscount = rptwork.ReportDefinition.ReportObjects["txtdiscount"] as TextObject;
                txtdiscount.Text = amtdis.ToString("#,##0.00;(#,##0.00);");
                TextObject txtnettotal = rptwork.ReportDefinition.ReportObjects["txtnettotal"] as TextObject;
                txtnettotal.Text = (amtmat + amtcar - amtdis).ToString("#,##0.00;(#,##0.00);");



                TextObject txtkword = rptwork.ReportDefinition.ReportObjects["txtkword"] as TextObject;
                txtkword.Text = "In Word: " + ASTUtility.Trans(amtmat + amtcar - amtdis, 2);
                TextObject txtuserinfo = rptwork.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                // Sub Report 
                //ReportDocument  rptsub= new RealERPRPT.R_14_Pro.RptOrderPaymentSch();
                //rptsub.SetDataSource((DataTable)ViewState["tblpaysch"]);


                if (comcod == "3340")
                {
                    rptwork.SetDataSource(_ReportDataSet.Tables[0]);
                    rptwork.Subreports["RptOrderPaymentSchUddl.rpt"].SetDataSource((DataTable)ViewState["tblpaysch"]);
                }

                else
                {
                    rptwork.SetDataSource(_ReportDataSet.Tables[0]);
                    rptwork.Subreports["RptOrderPaymentSch.rpt"].SetDataSource((DataTable)ViewState["tblpaysch"]);
                }


                // report.OpenSubReport(nameOfTheSubReport).SetDataSo urce(secondDataSet);


                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rptwork.SetParameterValue("ComLogo", ComLogo);


                Session["Report1"] = rptwork;

                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }


            //try
            //{



            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = this.GetCompCode();
            //    string comnam = hst["comnam"].ToString();
            //    string comadd = hst["comadd1"].ToString();
            //    string compname = hst["compname"].ToString();
            //    string username = hst["username"].ToString();
            //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //    string wrkid = this.Request.QueryString["orderno"].ToString();

            //    string Calltype = this.PrintCallType();
            //    string ordercopy = this.GetCompOrderCopy();
            //    DataSet _ReportDataSet = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", Calltype, wrkid, ordercopy, "", "", "", "", "", "", "");

            //    //Entry Screen


            //    DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURORDERINFO", wrkid, "", "", "", "", "", "", "", "");

            //    if (ds1 == null)
            //        return;

            //    ViewState["tblOrder"] = ds1.Tables[0];




            //    //DataTable dtorder = _ReportDataSet.Tables[0];




            //    string Para1 = _ReportDataSet.Tables[1].Rows[0]["leterdes"].ToString();
            //    string Orderdate = Convert.ToDateTime(_ReportDataSet.Tables[1].Rows[0]["orderdat"]).ToString("MMMM  dd, yyyy");
            //    string SupName = _ReportDataSet.Tables[1].Rows[0]["ssirdesc"].ToString();
            //    string Address = _ReportDataSet.Tables[1].Rows[0]["address"].ToString();
            //    string Cperson = _ReportDataSet.Tables[1].Rows[0]["cperson"].ToString();
            //    string Phone = _ReportDataSet.Tables[1].Rows[0]["phone"].ToString();

            //    DataTable dtterm = _ReportDataSet.Tables[2];
            //    DataTable dtord = _ReportDataSet.Tables[4];
            //    DataTable dtpaycch = _ReportDataSet.Tables[5];



            //    // string Type = this.CompanyPrintWorkOrder();
            //    ReportDocument rptwork = new ReportDocument();

            //    string fax = _ReportDataSet.Tables[1].Rows[0]["fax"].ToString();

            //    string trmplace = "* " + dtterm.Rows[0]["termssubj"].ToString() + " : ";
            //    string place = dtterm.Rows[0]["termsdesc"].ToString().Trim();
            //    string trmpdate = "* " + dtterm.Rows[1]["termssubj"].ToString() + " : ";
            //    string pdate = dtterm.Rows[1]["termsdesc"].ToString().Trim();
            //    string trmcarring = "* " + dtterm.Rows[2]["termssubj"].ToString() + " : ";
            //    string carring = dtterm.Rows[2]["termsdesc"].ToString().Trim();
            //    string trmbill = (comcod == "3330") ? "" : ("* " + dtterm.Rows[3]["termssubj"].ToString() + ": ");
            //    string bill = (comcod == "3330") ? ("* " + dtterm.Rows[3]["termsdesc"].ToString().Trim()) : dtterm.Rows[3]["termsdesc"].ToString().Trim(); ;
            //    string trmpayment = "* " + dtterm.Rows[4]["termssubj"].ToString() + " : ";
            //    string payment = dtterm.Rows[4]["termsdesc"].ToString().Trim();

            //    string trmothers = "* " + dtterm.Rows[5]["termssubj"].ToString() + " : ";
            //    string Others = dtterm.Rows[5]["termsdesc"].ToString().Trim();


            //    string trmcperson = ((dtterm.Rows.Count == 0) ? "" : dtterm.Select("termsid='010'").Length > 0 ? "* " + (dtterm.Select("termsid='010'")[0]["termssubj"]).ToString() + " : " : "");
            //    string cperson = ((dtterm.Rows.Count == 0) ? "" : dtterm.Select("termsid='010'").Length > 0 ? (dtterm.Select("termsid='010'")[0]["termsdesc"]).ToString() : ""); ;

            //    switch (comcod)
            //    {


            //        case "3332":
            //            //case "3101":
            //            rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderInstar();
            //            TextObject rpttxtReqIns = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
            //            rpttxtReqIns.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
            //            TextObject rpttxtReqAppIns = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
            //            rpttxtReqAppIns.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
            //            TextObject rpttxtOrdIns = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
            //            rpttxtOrdIns.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
            //            TextObject rpttxtWordIns = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
            //            rpttxtWordIns.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
            //            TextObject rpttxtcheckIns = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
            //            rpttxtcheckIns.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();


            //            break;




            //        default:
            //            rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderBridge();


            //            //Sign In


            //            TextObject rpttxtReq = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
            //            rpttxtReq.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
            //            TextObject rpttxtReqApp = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
            //            rpttxtReqApp.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
            //            TextObject rpttxtOrd = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
            //            rpttxtOrd.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
            //            TextObject rpttxtWord = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
            //            rpttxtWord.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
            //            TextObject rpttxtcheck = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
            //            rpttxtcheck.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
            //            TextObject txtAdvanced = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
            //            txtAdvanced.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); ");
            //            TextObject txtappby = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
            //            txtappby.Text = (comcod == "3335") ? "Head of Procurement" : "Approved By";

            //            // sign end 
            //            break;


            //    }




            //    TextObject txtsubject = rptwork.ReportDefinition.ReportObjects["txtsubject"] as TextObject;
            //    txtsubject.Text = dtord.Rows[0]["subject"].ToString();
            //    TextObject txtCompany = rptwork.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
            //    txtCompany.Text = comnam;
            //    TextObject txtAddress = rptwork.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            //    txtAddress.Text = comadd;
            //    TextObject rptpurno = rptwork.ReportDefinition.ReportObjects["purno"] as TextObject;
            //    rptpurno.Text = dtord.Rows[0]["orderno"].ToString().Substring(0, 3) + dtord.Rows[0]["orderno"].ToString().Substring(7, 2) + "-" + ASTUtility.Right(dtord.Rows[0]["orderno"].ToString(), 5);
            //    TextObject rptRefno = rptwork.ReportDefinition.ReportObjects["refno"] as TextObject;
            //    rptRefno.Text = dtord.Rows[0]["pordref"].ToString();
            //    TextObject supname = rptwork.ReportDefinition.ReportObjects["supname"] as TextObject;
            //    supname.Text = SupName;
            //    TextObject Supadd = rptwork.ReportDefinition.ReportObjects["saddress"] as TextObject;
            //    Supadd.Text = Address;
            //    TextObject txtPhoneNumber = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
            //    txtPhoneNumber.Text = Phone;
            //    //TextObject Fax = rptwork.ReportDefinition.ReportObjects["txtfax"] as TextObject;
            //    //Fax.Text =  fax;
            //    TextObject rptpurdate = rptwork.ReportDefinition.ReportObjects["txtOrderDate"] as TextObject;
            //    rptpurdate.Text = Orderdate;
            //    TextObject rptPara1 = rptwork.ReportDefinition.ReportObjects["TxtLETERDES"] as TextObject;
            //    rptPara1.Text = Para1;
            //    TextObject rptplace = rptwork.ReportDefinition.ReportObjects["place"] as TextObject;
            //    rptplace.Text = (place.Length > 0) ? trmplace + place : "";

            //    rptwork.ReportDefinition.Sections["GroupFooterSection5"].SectionFormat.EnableSuppress = (dtpaycch.Rows.Count > 0) ? false : true;


            //    TextObject rpttxtsupplydetails = rptwork.ReportDefinition.ReportObjects["txtsupplydetails"] as TextObject;
            //    rpttxtsupplydetails.Text = dtord.Rows[0]["pordnar"].ToString();
            //    rptwork.ReportDefinition.Sections["GroupFooterSection9"].SectionFormat.EnableSuppress = (place.Length > 0) ? false : true;


            //    TextObject rptpdate = rptwork.ReportDefinition.ReportObjects["pdate"] as TextObject;
            //    rptpdate.Text = (pdate.Length > 0) ? trmpdate + pdate : "";
            //    rptwork.ReportDefinition.Sections["GroupFooterSection10"].SectionFormat.EnableSuppress = (pdate.Length > 0) ? false : true;


            //    TextObject rptcarring = rptwork.ReportDefinition.ReportObjects["carring"] as TextObject;
            //    rptcarring.Text = (carring.Length > 0) ? trmcarring + carring : "";
            //    rptwork.ReportDefinition.Sections["GroupFooterSection11"].SectionFormat.EnableSuppress = (carring.Length > 0) ? false : true;


            //    TextObject rptpbill = rptwork.ReportDefinition.ReportObjects["bill"] as TextObject;
            //    rptpbill.Text = (bill.Length > 0) ? trmbill + bill : "";
            //    rptwork.ReportDefinition.Sections["GroupFooterSection12"].SectionFormat.EnableSuppress = (bill.Length > 0) ? false : true;

            //    TextObject rptpayment1 = rptwork.ReportDefinition.ReportObjects["payment1"] as TextObject;
            //    rptpayment1.Text = (payment.Length > 0) ? trmpayment + payment : "";
            //    rptwork.ReportDefinition.Sections["GroupFooterSection13"].SectionFormat.EnableSuppress = (payment.Length > 0) ? false : true;



            //    TextObject txtconcernperson = rptwork.ReportDefinition.ReportObjects["txtconcernperson"] as TextObject;
            //    txtconcernperson.Text = (cperson.Length > 0) ? cperson : "";
            //    rptwork.ReportDefinition.Sections["GroupFooterSection3"].SectionFormat.EnableSuppress = (cperson.Length > 0) ? false : true;



            //    TextObject rptOthrs = rptwork.ReportDefinition.ReportObjects["others"] as TextObject;
            //    rptOthrs.Text = (Others.Length > 0) ? trmothers + Others : "";
            //    rptwork.ReportDefinition.Sections["GroupFooterSection14"].SectionFormat.EnableSuppress = (Others.Length > 0) ? false : true;




            //    DataTable dtorder = (DataTable)ViewState["tblOrder"];
            //    DataTable dt1;
            //    DataTable dt2;
            //    DataTable dt3;


            //    // Carring
            //    DataView dv1 = dtorder.DefaultView;
            //    dv1.RowFilter = ("rsircode  like '019999901%'");
            //    dt1 = dv1.ToTable();

            //    //Deduction
            //    dv1 = dtorder.DefaultView;
            //    dv1.RowFilter = ("rsircode like'019999902%'");
            //    dt2 = dv1.ToTable();

            //    //Material
            //    dv1 = dtorder.DefaultView;
            //    dv1.RowFilter = ("rsircode not like '0199999%'");
            //    dt3 = dv1.ToTable();


            //    double amtcar = (dt1.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(ordramt)", "")) ? 0.00 : dt1.Compute("Sum(ordramt)", "")));
            //    double amtdis = (dt2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(ordramt)", "")) ? 0.00 : dt2.Compute("Sum(ordramt)", "")));
            //    //



            //    double amtmat = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(ordramt)", "")) ? 0.00 : dt3.Compute("Sum(ordramt)", "")));

            //    TextObject txtcarcost = rptwork.ReportDefinition.ReportObjects["txtcarcost"] as TextObject;
            //    txtcarcost.Text = amtcar.ToString("#,##0.00;(#,##0.00);");

            //    TextObject txtdiscount = rptwork.ReportDefinition.ReportObjects["txtdiscount"] as TextObject;
            //    txtdiscount.Text = amtdis.ToString("#,##0.00;(#,##0.00);");
            //    TextObject txtnettotal = rptwork.ReportDefinition.ReportObjects["txtnettotal"] as TextObject;
            //    txtnettotal.Text = (amtmat + amtcar - amtdis).ToString("#,##0.00;(#,##0.00);");



            //    TextObject txtkword = rptwork.ReportDefinition.ReportObjects["txtkword"] as TextObject;
            //    txtkword.Text = "In Word: " + ASTUtility.Trans(amtmat + amtcar - amtdis, 2);
            //    TextObject txtuserinfo = rptwork.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //    // Sub Report 
            //    //ReportDocument  rptsub= new RealERPRPT.R_14_Pro.RptOrderPaymentSch();
            //    //rptsub.SetDataSource((DataTable)ViewState["tblpaysch"]);

            //    rptwork.SetDataSource(_ReportDataSet.Tables[0]);
            //    rptwork.Subreports["RptOrderPaymentSch.rpt"].SetDataSource(dtpaycch);

            //    // report.OpenSubReport(nameOfTheSubReport).SetDataSo urce(secondDataSet);


            //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //    rptwork.SetParameterValue("ComLogo", ComLogo);
            //    Session["Report1"] = rptwork;

            //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


            //}
            //catch (Exception ex)
            //{

            //}

        }

        private void OrderPrintRDLC()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string wrkid = this.Request.QueryString["orderno"].ToString();
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

                string Calltype = this.PrintCallType();
                string ordercopy = this.GetCompOrderCopy();

                string PrintOpt = Request.QueryString.AllKeys.Contains("PrintOpt") ? this.Request.QueryString["PrintOpt"].ToString() : "";
                PrintOpt = PrintOpt.Length > 0 ? PrintOpt : ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();


                DataSet _ReportDataSet = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", Calltype, wrkid, ordercopy, "", "", "", "", "", "", "");


                List<RealEntity.C_12_Inv.EclassPurchase.PurchaseOrderInfo> purlist = _ReportDataSet.Tables[0].DataTableToList<RealEntity.C_12_Inv.EclassPurchase.PurchaseOrderInfo>();
                List<RealEntity.C_12_Inv.EclassPurchase.PurOrderTermsCondition> termscondition = _ReportDataSet.Tables[2].DataTableToList<RealEntity.C_12_Inv.EclassPurchase.PurOrderTermsCondition>();
                List<RealEntity.C_12_Inv.EclassPurchase.PaymentSchedule> paymentschedule = _ReportDataSet.Tables[5].DataTableToList<RealEntity.C_12_Inv.EclassPurchase.PaymentSchedule>();
                ViewState["tblpaysch"] = paymentschedule;
                List<RealEntity.C_12_Inv.EclassPurchase.MktPurchasePayment> payment01 = _ReportDataSet.Tables[6].DataTableToList<RealEntity.C_12_Inv.EclassPurchase.MktPurchasePayment>();
                ViewState["tblpayment01"] = payment01;
                string orderno = _ReportDataSet.Tables[4].Rows[0]["orderno"].ToString().Substring(0, 3) + _ReportDataSet.Tables[4].Rows[0]["orderno"].ToString().Substring(7, 2) + "-" + ASTUtility.Right(_ReportDataSet.Tables[4].Rows[0]["orderno"].ToString(), 5);
                string oissueno = _ReportDataSet.Tables[4].Rows[0]["oissueno"].ToString();
                string porderno = this.ComOrderNo(orderno, oissueno);
                string fax = _ReportDataSet.Tables[1].Rows[0]["fax"].ToString();


                string pactcode = _ReportDataSet.Tables[0].Rows[0]["pactcode"].ToString();
                string prjaddress = _ReportDataSet.Tables[0].Rows[0]["proadd"].ToString();
                string pactdesc = _ReportDataSet.Tables[0].Rows[0]["pactdesc"].ToString();
                string deptdesc = _ReportDataSet.Tables[0].Rows[0]["deptdesc"].ToString();


                string mrfno1 = _ReportDataSet.Tables[7].Rows[0]["mrfno"].ToString();

                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURORDERINFO", wrkid, "", "", "", "", "", "", "", "");

                ViewState["tblOrder1"] = ds1.Tables[0];

                DataTable dtorder1 = (DataTable)ViewState["tblOrder1"];
                DataTable dt1;
                DataTable dt2;
                DataTable dt3;

                // Carring
                DataView dv1 = dtorder1.DefaultView;
                dv1.RowFilter = ("rsircode  like '019999901%'");
                dt1 = dv1.ToTable();

                //Deduction
                dv1 = dtorder1.DefaultView;
                dv1.RowFilter = ("rsircode like'019999902%' ");
                dt2 = dv1.ToTable();

                //Material
                dv1 = dtorder1.DefaultView;
                dv1.RowFilter = ("rsircode not like '0199999%'");
                dt3 = dv1.ToTable();

                // return from switch case 

                string discountdesc = dtorder1.Select("rsircode like '019999902003%'").Length == 0 ? "Discount" : dtorder1.Select("rsircode like '019999902003%'")[0]["rsirdesc1"].ToString();

                double amtcar = (dt1.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(ordramt)", "")) ? 0.00 : dt1.Compute("Sum(ordramt)", "")));
                double amtdis = (dt2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(ordramt)", "")) ? 0.00 : dt2.Compute("Sum(ordramt)", "")));
                double amtmat = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(ordramt)", "")) ? 0.00 : dt3.Compute("Sum(ordramt)", "")));


                string nettotalamt = (amtmat + amtcar - amtdis).ToString("#,##0.00;(#,##0.00);");
                string advamt = Convert.ToDouble(_ReportDataSet.Tables[4].Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); ");



                string inword = "In Word: " + ASTUtility.Trans(amtmat + amtcar - amtdis, 2);


                string sign1 = "", sign2 = "", sign3 = "", sign4 = "", sign5 = "", sign6 = "", sign7 = "";
                string dat1 = "", dat2 = "", dat3 = "", dat4 = "", dat5 = "", dat6 = "";

                /// signature       // appnam - PURAPROVB and ordnam - purorder     
                switch (comcod)
                {
                    case "1205"://P2P
                    case "3351"://P2P
                    case "3352"://P2P 
                        sign1 = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        sign2 = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        sign3 = _ReportDataSet.Tables[3].Rows[0]["ratepname"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["ratepdate"].ToString();
                        sign4 = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        sign5 = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        sign6 = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        break;

                    case "3335": // Edison Properties           

                        sign1 = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        sign2 = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        sign3 = _ReportDataSet.Tables[3].Rows[0]["reqfaprname"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqfaprdat"].ToString();
                        sign4 = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        sign5 = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        sign6 = _ReportDataSet.Tables[3].Rows[0]["ordappnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["ordappdat"].ToString();
                        break;


                    case "1206": // Acme Technologies
                    case "1207": // Acme Technologies
                    case "3338": // Acme Technologies
                    case "3369": // Acme Technologies
                        sign1 = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        sign2 = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        sign3 = _ReportDataSet.Tables[3].Rows[0]["reqfaprname"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqfaprdat"].ToString();
                        sign4 = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        sign5 = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        sign6 = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        break;

                    case "3332": // InnStar
                    case "3336": // Suvastu
                    case "3337":  // Suvastu
                        sign1 = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        sign2 = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        sign3 = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        sign4 = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        sign5 = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        sign6 = "Approved By";
                        break;
                    case "3353": //Manama
                        sign1 = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        sign2 = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        sign3 = " ";
                        sign4 = " ";
                        sign5 = " ";
                        break;

                    case "3355": //greenwood
                        sign1 = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        sign2 = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        sign3 = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        sign4 = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        sign5 = _ReportDataSet.Tables[3].Rows[0]["ordfappnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["ordfappdat"].ToString();
                        sign6 = _ReportDataSet.Tables[3].Rows[0]["ordappnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["ordappdat"].ToString();
                        break;

                    case "3339": //tropical 
                        sign1 = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        sign2 = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        sign3 = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        sign4 = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        sign5 = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        sign6 = "Approved By";
                        break;


                    case "3101": //bridge
                    case "3330": //bridge
                        sign1 = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        sign2 = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        sign3 = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        sign4 = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        sign5 = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        sign6 = "Approved By";
                        break;


                    case "3354": //Edison Real Estate
                        sign1 = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        sign2 = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        sign3 = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        sign4 = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        sign5 = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        sign6 = _ReportDataSet.Tables[3].Rows[0]["ordfappnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["ordfappdat"].ToString();
                        sign7 = _ReportDataSet.Tables[3].Rows[0]["ordappnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["ordappdat"].ToString();
                        break;

                    case "3366": //Lanco
                        sign1 = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        sign2 = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        sign3 = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        sign4 = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        sign5 = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        sign6 = "Approved By";
                        break;

                    // finlay 
                    case "3368":
                        sign1 = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString();
                        dat1 = _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        //sign2 = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString();
                        //dat2 = _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();                        
                        sign2 = _ReportDataSet.Tables[3].Rows[0]["reqfaprname"].ToString();
                        dat2 = _ReportDataSet.Tables[3].Rows[0]["reqfaprdat"].ToString();
                        sign3 = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString();
                        dat3 = _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        sign4 = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString();
                        dat4 = _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        sign5 = (Convert.ToDateTime(_ReportDataSet.Tables[3].Rows[0]["ordfappdat"]).ToString("dd-MMM-yyyy")) == "01-Jan-1900" ? "" : _ReportDataSet.Tables[3].Rows[0]["ordfappnam"].ToString(); //_ReportDataSet.Tables[3].Rows[0]["ordfappnam"].ToString();
                        dat5 = (Convert.ToDateTime(_ReportDataSet.Tables[3].Rows[0]["ordfappdat"]).ToString("dd-MMM-yyyy")) == "01-Jan-1900" ? "" : _ReportDataSet.Tables[3].Rows[0]["ordfappdat"].ToString();
                        sign7 = (Convert.ToDateTime(_ReportDataSet.Tables[3].Rows[0]["ordappdat"]).ToString("dd-MMM-yyyy")) == "01-Jan-1900" ? "" : _ReportDataSet.Tables[3].Rows[0]["ordappnam"].ToString();
                        dat6 = (Convert.ToDateTime(_ReportDataSet.Tables[3].Rows[0]["ordappdat"]).ToString("dd-MMM-yyyy")) == "01-Jan-1900" ? "" : _ReportDataSet.Tables[3].Rows[0]["ordappdat"].ToString();

                        sign6 = "Approved By";




                        break;

                    // Epic ratepbyid	ratepname	ratepdate
                    //case "3101":
                    case "3357":
                        sign1 = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        sign2 = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        sign3 = _ReportDataSet.Tables[3].Rows[0]["ratepname"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["ratepdate"].ToString();
                        sign4 = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        sign5 = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        sign6 = "Approved By";
                        break;

                    case "3367":
                        sign1 = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        sign2 = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        sign3 = _ReportDataSet.Tables[3].Rows[0]["ratepname"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["ratepdate"].ToString();
                        sign4 = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        sign5 = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        sign6 = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        break;

                    default:
                        sign1 = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        sign2 = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        sign3 = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        sign4 = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        sign5 = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        sign6 = "Approved By";
                        break;
                }

                // basic information/////////
                string subject = _ReportDataSet.Tables[4].Rows[0]["subject"].ToString();
                string leterdesc = _ReportDataSet.Tables[4].Rows[0]["leterdes"].ToString();

                string ordrefno = _ReportDataSet.Tables[4].Rows[0]["pordref"].ToString();
                string supname = _ReportDataSet.Tables[1].Rows[0]["ssirdesc"].ToString();
                string Supadd = _ReportDataSet.Tables[1].Rows[0]["address"].ToString();
                string Supmobile = _ReportDataSet.Tables[1].Rows[0]["phone"].ToString();
                string cperson = _ReportDataSet.Tables[1].Rows[0]["cperson"].ToString();
                string supemail = _ReportDataSet.Tables[1].Rows[0]["supemail"].ToString();
                string podate = Convert.ToDateTime(_ReportDataSet.Tables[1].Rows[0]["orderdat"]).ToString("dd-MMM-yyyy");
                string pordnar = _ReportDataSet.Tables[4].Rows[0]["pordnar"].ToString();

                string terms = _ReportDataSet.Tables[4].Rows[0]["terms"].ToString();
                string costdesc = _ReportDataSet.Tables[4].Rows[0]["rsirdesc"].ToString();

                string cperson2 = "";

                string reqdat = _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                 
                // Terms & Conditions Variables//

                string terms1 = "", terms2 = "", terms3 = "", terms4 = "", terms5 = "", terms6 = "", terms7 = "", terms8 = "",
                    terms9 = "", terms10 = "", terms11 = "", terms12 = "";
                string pperson1 = "", pperson2 = "", pcperson = "";


                switch (comcod)
                {
                    case "3101": //ptl
                    case "3330": // Bridge Holdings
                        //terms1 = "1. " + termscondition[0].termssubj.ToString() + ":" + termscondition[0].termsdesc.ToString();
                        //terms2 = "2. " + termscondition[1].termssubj.ToString() + ":" + termscondition[1].termsdesc.ToString();
                        //terms3 = "3. " + termscondition[2].termssubj.ToString() + ":" + termscondition[2].termsdesc.ToString();
                        //terms4 = "4. " + termscondition[3].termssubj.ToString() + ":" + termscondition[3].termsdesc.ToString();
                        //terms5 = "5. " + termscondition[4].termssubj.ToString() + ":" + termscondition[4].termsdesc.ToString();
                        //cperson = termscondition.Find(p => p.termsid == "010").ToString().Length > 0 ? (termscondition.FindAll(p => p.termsid == "010")[0].termsdesc.ToString()) : "";

                        terms1 = termscondition.FindAll(p => p.termsid == "001")[0].termsdesc.ToString().Length > 0 ? "1." + (termscondition.FindAll(p => p.termsid == "001")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "001")[0].termsdesc.ToString()) : "";
                        terms2 = termscondition.FindAll(p => p.termsid == "002")[0].termsdesc.ToString().Length > 0 ? "2." + (termscondition.FindAll(p => p.termsid == "002")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "002")[0].termsdesc.ToString()) : "";
                        terms3 = termscondition.FindAll(p => p.termsid == "003")[0].termsdesc.ToString().Length > 0 ? "3." + (termscondition.FindAll(p => p.termsid == "003")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "003")[0].termsdesc.ToString()) : "";
                        terms4 = termscondition.FindAll(p => p.termsid == "004")[0].termsdesc.ToString().Length > 0 ? (termscondition.FindAll(p => p.termsid == "004")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "004")[0].termsdesc.ToString()) : "";
                        terms5 = termscondition.FindAll(p => p.termsid == "005")[0].termsdesc.ToString().Length > 0 ? (termscondition.FindAll(p => p.termsid == "005")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "005")[0].termsdesc.ToString()) : "";
                        cperson = termscondition.Find(p => p.termsid == "010").ToString().Length > 0 ? (termscondition.FindAll(p => p.termsid == "010")[0].termsdesc.ToString()) : "";

                        break;

                    case "3325": //Leisure
                    case "2325": //Leisure
                        terms1 = "1. " + termscondition[0].termssubj.ToString() + ":" + termscondition[0].termsdesc.ToString();
                        terms2 = "2. " + termscondition[2].termssubj.ToString() + ":" + termscondition[2].termsdesc.ToString();
                        terms3 = "3. " + termscondition[3].termssubj.ToString() + ":" + termscondition[3].termsdesc.ToString();
                        terms4 = "4. " + termscondition[4].termssubj.ToString() + ":" + termscondition[4].termsdesc.ToString();
                        terms5 = "5. " + termscondition[7].termssubj.ToString() + ":" + termscondition[7].termsdesc.ToString();
                        terms6 = "6. " + termscondition[8].termssubj.ToString() + ":" + termscondition[8].termsdesc.ToString();
                        terms7 = "7. " + termscondition[9].termssubj.ToString() + ":" + termscondition[9].termsdesc.ToString();

                        break;

                    case "3339": // Tropical Home
                    case "3332": // InnStar
                        terms1 = termscondition.FindAll(p => p.termsid == "001")[0].termsdesc.ToString().Length > 0 ? "1." + (termscondition.FindAll(p => p.termsid == "001")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "001")[0].termsdesc.ToString()) : "";
                        terms2 = termscondition.FindAll(p => p.termsid == "002")[0].termsdesc.ToString().Length > 0 ? "2." + (termscondition.FindAll(p => p.termsid == "002")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "002")[0].termsdesc.ToString()) : "";
                        terms3 = termscondition.FindAll(p => p.termsid == "003")[0].termsdesc.ToString().Length > 0 ? "3." + (termscondition.FindAll(p => p.termsid == "003")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "003")[0].termsdesc.ToString()) : "";
                        terms4 = termscondition.FindAll(p => p.termsid == "004")[0].termsdesc.ToString().Length > 0 ? "4." + (termscondition.FindAll(p => p.termsid == "004")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "004")[0].termsdesc.ToString()) : "";
                        terms5 = termscondition.FindAll(p => p.termsid == "005")[0].termsdesc.ToString().Length > 0 ? "5." + (termscondition.FindAll(p => p.termsid == "005")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "005")[0].termsdesc.ToString()) : "";
                        terms6 = termscondition.FindAll(p => p.termsid == "006")[0].termsdesc.ToString().Length > 0 ? "6." + (termscondition.FindAll(p => p.termsid == "006")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "006")[0].termsdesc.ToString()) : "";
                        terms7 = termscondition.FindAll(p => p.termsid == "007")[0].termsdesc.ToString().Length > 0 ? "7." + (termscondition.FindAll(p => p.termsid == "007")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "007")[0].termsdesc.ToString()) : "";
                        terms8 = termscondition.FindAll(p => p.termsid == "008")[0].termsdesc.ToString().Length > 0 ? "8." + (termscondition.FindAll(p => p.termsid == "008")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "008")[0].termsdesc.ToString()) : "";
                        terms9 = termscondition.FindAll(p => p.termsid == "009")[0].termsdesc.ToString().Length > 0 ? "9." + (termscondition.FindAll(p => p.termsid == "009")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "009")[0].termsdesc.ToString()) : "";
                        cperson = termscondition.Find(p => p.termsid == "010").ToString().Length > 0 ? (termscondition.FindAll(p => p.termsid == "010")[0].termsdesc.ToString()) : "";
                        break;

                    case "3336": // Suvastu
                    case "3337": // Suvastu

                        terms1 = "1. " + termscondition[0].termssubj.ToString() + ":" + termscondition[0].termsdesc.ToString();
                        terms2 = "2. " + termscondition[1].termssubj.ToString() + ":" + termscondition[1].termsdesc.ToString();
                        terms3 = "3. " + termscondition[2].termssubj.ToString() + ":" + termscondition[2].termsdesc.ToString();
                        terms4 = "4. " + termscondition[3].termssubj.ToString() + ":" + termscondition[3].termsdesc.ToString();
                        terms5 = "5. " + termscondition[4].termssubj.ToString() + ":" + termscondition[4].termsdesc.ToString();
                        terms6 = "6. " + termscondition[5].termssubj.ToString() + ":" + termscondition[5].termsdesc.ToString();
                        terms7 = "7. " + termscondition[6].termssubj.ToString() + ":" + termscondition[6].termsdesc.ToString();
                        terms8 = "8. " + termscondition[7].termssubj.ToString() + ":" + termscondition[7].termsdesc.ToString();
                        terms9 = "9. " + termscondition[8].termssubj.ToString() + ":" + termscondition[8].termsdesc.ToString();
                        cperson2 = termscondition.Find(p => p.termsid == "010").ToString().Length > 0 ? (termscondition.FindAll(p => p.termsid == "010")[0].termsdesc.ToString()) : "";
                        break;

                    //case "3101": // Pintech

                    case "3366": // Lanco
                    case "3370": // cpdl
                    case "1205": //P2P
                    case "3351": //P2P
                    case "3352": //P2P 
                        terms1 = terms.ToString();
                        break;

                    case "3368": // Finlay
                    case "3357": // Cube 
                        terms1 = terms.ToString();
                        pcperson = _ReportDataSet.Tables[1].Rows[0]["pperson"].ToString() + ", " + _ReportDataSet.Tables[1].Rows[0]["pcontact"].ToString();
                        break;

                    //
                    case "3367": // epic
                    case "3335": // Edison Properties
                        terms1 = "1. " + termscondition[0].termssubj.ToString() + ":" + termscondition[0].termsdesc.ToString();
                        terms2 = "2. " + termscondition[1].termssubj.ToString() + ":" + termscondition[1].termsdesc.ToString();
                        terms3 = "3. " + termscondition[2].termssubj.ToString() + ":" + termscondition[2].termsdesc.ToString();
                        terms4 = "4. " + termscondition[3].termssubj.ToString() + ":" + termscondition[3].termsdesc.ToString();
                        terms5 = "5. " + termscondition[4].termssubj.ToString() + ":" + termscondition[4].termsdesc.ToString();
                        terms6 = "6. " + termscondition[5].termssubj.ToString() + ":" + termscondition[5].termsdesc.ToString();
                        terms7 = "7. " + termscondition[6].termssubj.ToString() + ":" + termscondition[6].termsdesc.ToString();
                        terms8 = "8. " + termscondition[7].termssubj.ToString() + ":" + termscondition[7].termsdesc.ToString();
                        terms9 = "9. " + termscondition[8].termssubj.ToString() + ":" + termscondition[8].termsdesc.ToString();
                        terms10 = "10. " + termscondition[9].termssubj.ToString() + ":" + termscondition[9].termsdesc.ToString();
                        break;


                    case "1206": //Acme Technologies
                    case "1207": //Acme Technologies
                    case "3338": //Acme Technologies
                    case "3369": //Acme Technologies
                        terms1 = termscondition.Find(p => p.termsid == "001").ToString().Length > 0 ? "1." + (termscondition.FindAll(p => p.termsid == "001")[0].termsdesc.ToString()) : "";
                        terms2 = termscondition.Find(p => p.termsid == "002").ToString().Length > 0 ? "2." + (termscondition.FindAll(p => p.termsid == "002")[0].termsdesc.ToString()) : "";
                        terms3 = termscondition.Find(p => p.termsid == "003").ToString().Length > 0 ? "3." + (termscondition.FindAll(p => p.termsid == "003")[0].termsdesc.ToString()) : "";
                        terms4 = termscondition.Find(p => p.termsid == "004").ToString().Length > 0 ? "4." + (termscondition.FindAll(p => p.termsid == "004")[0].termsdesc.ToString()) : "";
                        terms5 = termscondition.Find(p => p.termsid == "005").ToString().Length > 0 ? (termscondition.FindAll(p => p.termsid == "005")[0].termsdesc.ToString()) : "";
                        terms6 = termscondition.Find(p => p.termsid == "006").ToString().Length > 0 ? (termscondition.FindAll(p => p.termsid == "006")[0].termsdesc.ToString()) : "";
                        terms7 = termscondition.Find(p => p.termsid == "007").ToString().Length > 0 ? (termscondition.FindAll(p => p.termsid == "007")[0].termsdesc.ToString()) : "";
                        // contact person come from Terms and Conditions
                        cperson = termscondition.Find(p => p.termsid == "010").ToString().Length > 0 ? (termscondition.FindAll(p => p.termsid == "010")[0].termsdesc.ToString()) : "";
                        break;


                    // manama
                    case "3353":
                        terms1 = "* " + termscondition[0].termssubj.ToString() + ":" + termscondition[0].termsdesc.ToString();
                        terms2 = "* " + termscondition[1].termssubj.ToString() + ":" + termscondition[1].termsdesc.ToString();
                        terms3 = "* " + termscondition[2].termssubj.ToString() + ":" + termscondition[2].termsdesc.ToString();
                        terms4 = "* " + termscondition[3].termssubj.ToString() + ":" + termscondition[3].termsdesc.ToString();
                        terms5 = "* " + termscondition[4].termssubj.ToString() + ":" + termscondition[4].termsdesc.ToString();
                        cperson = termscondition.Find(p => p.termsid == "010").ToString().Length > 0 ? (termscondition.FindAll(p => p.termsid == "010")[0].termsdesc.ToString()) : "";
                        break;


                    case "3355": // Green Wood

                        terms1 = "* " + termscondition[0].termssubj.ToString() + ":" + termscondition[0].termsdesc.ToString();
                        terms2 = "* " + termscondition[1].termssubj.ToString() + ":" + termscondition[1].termsdesc.ToString();
                        terms3 = "* " + termscondition[2].termssubj.ToString() + ":" + termscondition[2].termsdesc.ToString();
                        terms4 = "* " + termscondition[3].termssubj.ToString() + ":" + termscondition[3].termsdesc.ToString();
                        terms5 = "* " + termscondition[4].termssubj.ToString() + ":" + termscondition[4].termsdesc.ToString();
                        cperson = termscondition.Find(p => p.termsid == "010").ToString().Length > 0 ? (termscondition.FindAll(p => p.termsid == "010")[0].termsdesc.ToString()) : "";

                        break;


                    case "3364": //JBS
                        terms1 = "* " + termscondition[0].termssubj.ToString() + ":" + termscondition[0].termsdesc.ToString();
                        terms2 = "* " + termscondition[1].termssubj.ToString() + ":" + termscondition[1].termsdesc.ToString();
                        terms3 = "* " + termscondition[2].termssubj.ToString() + ":" + termscondition[2].termsdesc.ToString();
                        pperson1 = termscondition.Find(p => p.termsid == "009").ToString().Length > 0 ? (termscondition.FindAll(p => p.termsid == "009")[0].termsdesc.ToString()) : "";
                        pperson2 = termscondition.Find(p => p.termsid == "010").ToString().Length > 0 ? (termscondition.FindAll(p => p.termsid == "010")[0].termsdesc.ToString()) : "";
                        break;

                    //case "3101": // Pintech
                    case "3354": // Edison Real estate                     

                        terms1 = termscondition.FindAll(p => p.termsid == "001")[0].termsdesc.ToString().Length > 0 ? "1." + (termscondition.FindAll(p => p.termsid == "001")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "001")[0].termsdesc.ToString()) : "";
                        terms2 = termscondition.FindAll(p => p.termsid == "002")[0].termsdesc.ToString().Length > 0 ? "2." + (termscondition.FindAll(p => p.termsid == "002")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "002")[0].termsdesc.ToString()) : "";
                        terms3 = termscondition.FindAll(p => p.termsid == "003")[0].termsdesc.ToString().Length > 0 ? "3." + (termscondition.FindAll(p => p.termsid == "003")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "003")[0].termsdesc.ToString()) : "";
                        terms4 = termscondition.FindAll(p => p.termsid == "004")[0].termsdesc.ToString().Length > 0 ? "4." + (termscondition.FindAll(p => p.termsid == "004")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "004")[0].termsdesc.ToString()) : "";
                        terms5 = termscondition.FindAll(p => p.termsid == "005")[0].termsdesc.ToString().Length > 0 ? "5." + (termscondition.FindAll(p => p.termsid == "005")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "005")[0].termsdesc.ToString()) : "";
                        terms6 = termscondition.FindAll(p => p.termsid == "006")[0].termsdesc.ToString().Length > 0 ? "6." + (termscondition.FindAll(p => p.termsid == "006")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "006")[0].termsdesc.ToString()) : "";
                        terms7 = termscondition.FindAll(p => p.termsid == "007")[0].termsdesc.ToString().Length > 0 ? "7." + (termscondition.FindAll(p => p.termsid == "007")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "007")[0].termsdesc.ToString()) : "";
                        terms8 = termscondition.FindAll(p => p.termsid == "008")[0].termsdesc.ToString().Length > 0 ? "8." + (termscondition.FindAll(p => p.termsid == "008")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "008")[0].termsdesc.ToString()) : "";
                        terms9 = termscondition.FindAll(p => p.termsid == "009")[0].termsdesc.ToString().Length > 0 ? "9." + (termscondition.FindAll(p => p.termsid == "009")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "009")[0].termsdesc.ToString()) : "";
                        cperson = termscondition.Find(p => p.termsid == "010").ToString().Length > 0 ? (termscondition.FindAll(p => p.termsid == "010")[0].termsdesc.ToString()) : "";
                        break;


                    case "1108":
                    case "1109":
                    case "3315":
                    case "3316":

                        terms1 = termscondition.FindAll(p => p.termsid == "001")[0].termsdesc.ToString().Length > 0 ? "1." + (termscondition.FindAll(p => p.termsid == "001")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "001")[0].termsdesc.ToString()) : "";
                        terms2 = termscondition.FindAll(p => p.termsid == "002")[0].termsdesc.ToString().Length > 0 ? "2." + (termscondition.FindAll(p => p.termsid == "002")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "002")[0].termsdesc.ToString()) : "";
                        terms3 = termscondition.FindAll(p => p.termsid == "003")[0].termsdesc.ToString().Length > 0 ? "3." + (termscondition.FindAll(p => p.termsid == "003")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "003")[0].termsdesc.ToString()) : "";
                        terms4 = termscondition.FindAll(p => p.termsid == "004")[0].termsdesc.ToString().Length > 0 ? "4." + (termscondition.FindAll(p => p.termsid == "004")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "004")[0].termsdesc.ToString()) : "";
                        terms5 = termscondition.FindAll(p => p.termsid == "005")[0].termsdesc.ToString().Length > 0 ? "5." + (termscondition.FindAll(p => p.termsid == "005")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "005")[0].termsdesc.ToString()) : "";
                        terms6 = termscondition.FindAll(p => p.termsid == "006")[0].termsdesc.ToString().Length > 0 ? "6." + (termscondition.FindAll(p => p.termsid == "006")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "006")[0].termsdesc.ToString()) : "";
                        terms7 = termscondition.FindAll(p => p.termsid == "007")[0].termsdesc.ToString().Length > 0 ? "7." + (termscondition.FindAll(p => p.termsid == "007")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "007")[0].termsdesc.ToString()) : "";
                        terms8 = termscondition.FindAll(p => p.termsid == "008")[0].termsdesc.ToString().Length > 0 ? "8." + (termscondition.FindAll(p => p.termsid == "008")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "008")[0].termsdesc.ToString()) : "";
                        terms9 = termscondition.FindAll(p => p.termsid == "009")[0].termsdesc.ToString().Length > 0 ? "9." + (termscondition.FindAll(p => p.termsid == "009")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "009")[0].termsdesc.ToString()) : "";
                        cperson = termscondition.Find(p => p.termsid == "010").ToString().Length > 0 ? (termscondition.FindAll(p => p.termsid == "010")[0].termsdesc.ToString()) : "";
                        break;


                    case "3358":
                    case "3359":
                    case "3360":
                    case "3361":

                        terms1 = termscondition.FindAll(p => p.termsid == "001")[0].termsdesc.ToString().Length > 0 ? "1." + (termscondition.FindAll(p => p.termsid == "001")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "001")[0].termsdesc.ToString()) : "";
                        terms2 = termscondition.FindAll(p => p.termsid == "002")[0].termsdesc.ToString().Length > 0 ? "2." + (termscondition.FindAll(p => p.termsid == "002")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "002")[0].termsdesc.ToString()) : "";
                        terms3 = termscondition.FindAll(p => p.termsid == "003")[0].termsdesc.ToString().Length > 0 ? "3." + (termscondition.FindAll(p => p.termsid == "003")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "003")[0].termsdesc.ToString()) : "";
                        terms4 = termscondition.FindAll(p => p.termsid == "004")[0].termsdesc.ToString().Length > 0 ? "4." + (termscondition.FindAll(p => p.termsid == "004")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "004")[0].termsdesc.ToString()) : "";
                        terms5 = termscondition.FindAll(p => p.termsid == "005")[0].termsdesc.ToString().Length > 0 ? "5." + (termscondition.FindAll(p => p.termsid == "005")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "005")[0].termsdesc.ToString()) : "";
                        terms6 = termscondition.FindAll(p => p.termsid == "006")[0].termsdesc.ToString().Length > 0 ? "6." + (termscondition.FindAll(p => p.termsid == "006")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "006")[0].termsdesc.ToString()) : "";
                        terms7 = termscondition.FindAll(p => p.termsid == "007")[0].termsdesc.ToString().Length > 0 ? "7." + (termscondition.FindAll(p => p.termsid == "007")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "007")[0].termsdesc.ToString()) : "";
                        terms8 = termscondition.FindAll(p => p.termsid == "008")[0].termsdesc.ToString().Length > 0 ? "8." + (termscondition.FindAll(p => p.termsid == "008")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "008")[0].termsdesc.ToString()) : "";
                        terms9 = termscondition.FindAll(p => p.termsid == "009")[0].termsdesc.ToString().Length > 0 ? "9." + (termscondition.FindAll(p => p.termsid == "009")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "009")[0].termsdesc.ToString()) : "";
                        cperson = termscondition.Find(p => p.termsid == "010").ToString().Length > 0 ? (termscondition.FindAll(p => p.termsid == "010")[0].termsdesc.ToString()) : "";
                        break;



                    default: //Default
                        terms1 = "* " + termscondition[0].termssubj.ToString() + ":" + termscondition[0].termsdesc.ToString();
                        terms2 = "* " + termscondition[1].termssubj.ToString() + ":" + termscondition[1].termsdesc.ToString();
                        terms3 = "* " + termscondition[2].termssubj.ToString() + ":" + termscondition[2].termsdesc.ToString();
                        terms4 = "* " + termscondition[3].termssubj.ToString() + ":" + termscondition[3].termsdesc.ToString();
                        terms5 = "* " + termscondition[4].termssubj.ToString() + ":" + termscondition[4].termsdesc.ToString();
                        break;
                }

                // Set Report Name
                string Reportpath = "";
                switch (comcod)
                {

                    case "1206": //Acme Technologies
                    case "1207": //Acme Technologies
                    case "3338": //Acme Technologies
                    case "3369": //Acme Technologies
                        Reportpath = "~/Report/RptPurchaseOrderAcme.rdlc";
                        break;

                    case "1108": //Assure
                    case "1109": //Assure
                    case "3315": //Assure
                    case "3316": //Assure
                    case "3317": //Assure
                        //Reportpath = "~/Report/RptPurchaseOrder.rdlc";
                        Reportpath = "~/Report/RptPurchaseOrderAssure.rdlc";
                        break;

                    case "3339": //Tropical
                        Reportpath = "~/Report/RptPurchaseOrderTropical.rdlc";
                        break;

                    //case "3101": //pintech
                    case "3366": //Lanco
                        Reportpath = "~/Report/RptPurchaseOrderLanco.rdlc";
                        break;

                    case "3335": // Edison Properties
                        Reportpath = "~/Report/RptPurchaseOrderEdison.rdlc";
                        break;

                    case "1205"://P2P
                    case "3351"://P2P
                    case "3352"://P2P
                        Reportpath = "~/Report/RptPurchaseOrderP2P.rdlc";
                        break;

                    case "3305":  //Rupayan group
                    case "3306": //Rupayan group
                    case "3309": //Rupayan group
                    case "3310": //Rupayan group
                    case "3311": //Rupayan group
                    case "2305": //Rupayan group
                    case "2306": //Rupayan group
                        Reportpath = "~/Report/RptPurchaseOrderRupayon.rdlc";
                        break;

                    case "3332": // InnStar
                        Reportpath = "~/Report/RptPurchaseOrderInnstar.rdlc";
                        break;

                    case "3336": // Suvastu
                    case "3337":  // Suvastu
                        Reportpath = "~/Report/RptPurchaseOrderSuvastu.rdlc";
                        break;

                    case "3325": //Leisure
                    case "2325": //Leisure
                        Reportpath = "~/Report/RptPurchaseOrderLeisure.rdlc";
                        break;
                    case "3353": //Manama
                        Reportpath = "~/Report/RptPurchaseOrderManama.rdlc";
                        break;

                    case "3101": // ptl  
                    case "3330": //bridge 
                        if (pactcode == "160100010025" || pactcode == "160100010027")
                        {
                            Reportpath = "~/Report/RptPurchaseOrderBridgeWLogo.rdlc";
                        }
                        else
                        {
                            Reportpath = "~/Report/RptPurchaseOrderBridge.rdlc";
                        }
                        break;

                    case "3355": //Greenwood
                        Reportpath = "~/Report/RptPurchaseOrderGreenwood.rdlc";
                        break;

                    case "3364": //JBS
                        Reportpath = "~/Report/RptPurchaseOrderJBS.rdlc";
                        break;

                    //case "3101": 
                    case "3357": //Cube
                        Reportpath = "~/Report/RptPurchaseOrderCube.rdlc";
                        break;

                    //case "3101": //pintech                    
                    case "3354": //Edison Real Estate                        
                        Reportpath = "~/Report/RptPurchaseOrderEDR.rdlc";
                        break;

                    //case "3101": //pintech                      
                    case "3368": //Finlay Properties Ltd
                        if (pactcode == "110200990001")
                        {
                            Reportpath = "~/Report/RptPurchaseOrderFinlayInd.rdlc";
                        }
                        else
                        {
                            Reportpath = "~/Report/RptPurchaseOrderFinlay.rdlc";
                        }
                        break;

                    case "3358": //Entrust Ltd                        
                    case "3359": //Entrust Ltd                        
                    case "3360": //Entrust Ltd                        
                    case "3361": //Entrust Ltd                        
                        Reportpath = "~/Report/RptPurchaseOrderEntrust.rdlc";
                        break;
                        
                    case "3367": //Epic                        
                        Reportpath = "~/Report/RptPurchaseOrderEpic.rdlc";
                        break;
                                          
                    case "3370": //Epic cpdl                        
                        Reportpath = "~/Report/RptPurchaseOrderCPDL.rdlc";
                        porderno =ASTUtility.CustomReqFormat(wrkid);
                        break;

                    default:
                        Reportpath = "~/Report/RptPurchaseOrder.rdlc";
                        break;
                }


                LocalReport Rpt1 = new LocalReport();
                // var assamblyPath = Assembly.GetExecutingAssembly().CodeBase;
                // Assembly assembly1 = Assembly.LoadFrom(assamblyPath);
                //Assembly assembly1 = Assembly.LoadFrom("ASITHmsRpt2Inventory.dll");
                //   Stream stream1 = assembly1.GetManifestResourceStream("RptPurchaseOrder.rdlc");
                //  LocalReport Rpt1 = new LocalReport();
                Rpt1.DisplayName = "RptPurchaseOrder";
                //Rpt1.LoadReportDefinition(stream1);
                Rpt1.ReportPath = Server.MapPath(Reportpath);
                Rpt1.DataSources.Clear();
                Rpt1.DataSources.Add(new ReportDataSource("DataSet1", purlist));
                Rpt1.DataSources.Add(new ReportDataSource("DataSet2", termscondition));
                //Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptPurchaseOrder", purlist, termscondition, null);
                Rpt1.EnableExternalImages = true;

                Rpt1.SetParameters(new ReportParameter("compname", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("disamt", amtdis.ToString("#,##0.00;(#,##0.00); ")));
                Rpt1.SetParameters(new ReportParameter("carecost", amtcar.ToString("#,##0.00;(#,##0.00); ")));
                Rpt1.SetParameters(new ReportParameter("nettotal", nettotalamt));
                Rpt1.SetParameters(new ReportParameter("advamt", advamt));
                Rpt1.SetParameters(new ReportParameter("discountdesc", discountdesc));
                // basic information send to report
                Rpt1.SetParameters(new ReportParameter("subject", subject));
                Rpt1.SetParameters(new ReportParameter("leterdesc", leterdesc));
                Rpt1.SetParameters(new ReportParameter("porderno", porderno));
                Rpt1.SetParameters(new ReportParameter("ordrefno", ordrefno));
                Rpt1.SetParameters(new ReportParameter("supname", supname));
                Rpt1.SetParameters(new ReportParameter("supadd", Supadd));
                Rpt1.SetParameters(new ReportParameter("supmobile", Supmobile));
                Rpt1.SetParameters(new ReportParameter("cperson", cperson));
                Rpt1.SetParameters(new ReportParameter("podate", podate));
                Rpt1.SetParameters(new ReportParameter("faxnumber", fax));

                // signature send to report//
                Rpt1.SetParameters(new ReportParameter("sign1", sign1));
                Rpt1.SetParameters(new ReportParameter("sign2", sign2));
                Rpt1.SetParameters(new ReportParameter("sign3", sign3));
                Rpt1.SetParameters(new ReportParameter("sign4", sign4));
                Rpt1.SetParameters(new ReportParameter("sign5", sign5));
                Rpt1.SetParameters(new ReportParameter("sign6", sign6));
                Rpt1.SetParameters(new ReportParameter("terms1", terms1));
                Rpt1.SetParameters(new ReportParameter("terms2", terms2));
                Rpt1.SetParameters(new ReportParameter("terms3", terms3));
                Rpt1.SetParameters(new ReportParameter("terms4", terms4));
                Rpt1.SetParameters(new ReportParameter("terms5", terms5));
                Rpt1.SetParameters(new ReportParameter("terms6", terms6));
                Rpt1.SetParameters(new ReportParameter("terms7", terms7));
                Rpt1.SetParameters(new ReportParameter("terms8", terms8));
                Rpt1.SetParameters(new ReportParameter("terms9", terms9));
                Rpt1.SetParameters(new ReportParameter("terms10", terms10));
                Rpt1.SetParameters(new ReportParameter("terms11", terms11));
                Rpt1.SetParameters(new ReportParameter("terms12", terms12));

                Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("inword", inword));
                Rpt1.SetParameters(new ReportParameter("pordnar", pordnar));

                // todo for skip subreport and dynamic carrying charge
                this.SetCarryingDynamic(Rpt1);
                switch (comcod)
                {
                    case "1108":
                    case "1109":
                    case "3315":
                    case "3316":
                        Rpt1.SetParameters(new ReportParameter("subcompname", ""));
                        Rpt1.SubreportProcessing += new SubreportProcessingEventHandler(LoadSubReportAssure);
                        break;

                    case "3357": // cube
                        Rpt1.SetParameters(new ReportParameter("pcperson", pcperson));
                        break;

                    case "3368": // finlay
                        if (pactcode == "110200990001")
                        {
                            Rpt1.SetParameters(new ReportParameter("deptdesc", deptdesc));
                        }
                        Rpt1.SetParameters(new ReportParameter("sign7", sign7));
                        Rpt1.SetParameters(new ReportParameter("pcperson", pcperson));
                        Rpt1.SetParameters(new ReportParameter("supemail", supemail));
                        Rpt1.SetParameters(new ReportParameter("dat1", dat1));
                        Rpt1.SetParameters(new ReportParameter("dat2", dat2));
                        Rpt1.SetParameters(new ReportParameter("dat3", dat3));
                        Rpt1.SetParameters(new ReportParameter("dat4", dat4));
                        Rpt1.SetParameters(new ReportParameter("dat5", dat5));
                        Rpt1.SetParameters(new ReportParameter("dat6", dat6));
                        break;              
                    
                    case "3354": // edison
                        Rpt1.SetParameters(new ReportParameter("sign7", sign7));
                        break;
                        
                    case "3370": // cpdl
                        Rpt1.SetParameters(new ReportParameter("pcperson", pcperson));
                        Rpt1.SetParameters(new ReportParameter("supemail", supemail));
                        Rpt1.SetParameters(new ReportParameter("reqdat", reqdat));
                        break;
                    
                    case "1205": // p2p
                    case "3351": // p2p
                    case "3352": // p2p
                        string subcom = (comcod == "3351") ? "A Concern of P2P" : "";
                        Rpt1.SetParameters(new ReportParameter("subcompname", subcom));
                        Rpt1.SubreportProcessing += new SubreportProcessingEventHandler(LoadSubReport);
                        break;

                    case"3336":
                    case"3337":
                        Rpt1.SetParameters(new ReportParameter("cperson2", cperson2));
                        Rpt1.SubreportProcessing += new SubreportProcessingEventHandler(LoadSubReport);
                        break;
                    
                    case"3353":
                        Rpt1.SetParameters(new ReportParameter("refno01", mrfno1));
                        Rpt1.SubreportProcessing += new SubreportProcessingEventHandler(LoadSubReport);
                        break;
                    
                    case"3364": // jbs
                        Rpt1.SetParameters(new ReportParameter("prjaddress", prjaddress));
                        Rpt1.SetParameters(new ReportParameter("pactdesc", pactdesc));
                        Rpt1.SetParameters(new ReportParameter("pperson1", pperson1));
                        Rpt1.SetParameters(new ReportParameter("pperson2", pperson2));
                        Rpt1.SubreportProcessing += new SubreportProcessingEventHandler(LoadSubReport);
                        break;

                    case"3101":
                    case"3330":
                        double balamt = 0.00;
                        double ntotal = (amtmat + amtcar - amtdis);
                        double nadvamt = Convert.ToDouble(_ReportDataSet.Tables[4].Rows[0]["advamt"]);
                        balamt = ntotal - nadvamt;
                        Rpt1.SetParameters(new ReportParameter("balamt", balamt.ToString("#,##0.00;(#,##0.00); ")));
                        Rpt1.SubreportProcessing += new SubreportProcessingEventHandler(LoadSubReport);
                        break;

                    case "3358": // entrust
                    case "3359":  // entrust
                    case "3360":  // entrust
                    case "3361":  // entrust
                    case "3366": // lanco
                    case "3367": // epic
   
                        break;

                    default:
                        Rpt1.SubreportProcessing += new SubreportProcessingEventHandler(LoadSubReport);
                        break;
                }
                
                Session["Report1"] = Rpt1;
                string qtype = this.Request.QueryString["Orderstatus"] ?? "";

                if (qtype == "Download")
                {

                    string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>PDF</OutputFormat>" +

                    "  <EmbedFonts>None</EmbedFonts>" +
                    "</DeviceInfo>";
                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes = Rpt1.Render("PDF", deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                    //return File(renderedBytes, mimeType);


                    //Warning[] warnings;
                    //string[] streamids;
                    //string mimeType;
                    //string encoding;
                    //string extension;
                    //byte[] bytes = Rpt1.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                    // string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf"; ;
                    //  CODE TO SAVE THE REPORT FILE ON SERVER
                    if (File.Exists(Server.MapPath("~/SupWorkOreder/" + orderno + ".pdf")))
                    {
                        File.Delete(Server.MapPath("~/SupWorkOreder/" + orderno + ".pdf"));
                    }

                    FileStream fileStream = new FileStream(Server.MapPath("~/SupWorkOreder/" + orderno + ".pdf"), FileMode.Create);

                    for (int i = 0; i < renderedBytes.Length; i++)
                    {
                        fileStream.WriteByte(renderedBytes[i]);
                    }
                    fileStream.Close();

                    bool ssl = Convert.ToBoolean(((Hashtable)Session["tblLogin"])["ssl"].ToString());
                    switch (ssl)
                    {
                        case true:
                            this.SendSSLMail(orderno, supemail);

                            break;

                        case false:
                            this.SendNormalMail(orderno, supemail);
                            break;
                    }
                }

                else
                {
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" + PrintOpt + "', target='_self');</script>";

                    //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                    //      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
                    // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=EXCEL', target='_self');</script>";
                }


            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        private bool IsCarryingDynamic()
        {
            bool isDynamic = false;
            string comcod = GetCompCode();
            switch (comcod)
            {
                case "1205": // p2p
                case "3351": // p2p
                case "3352": // p2p
                case "1108": // assure
                case "1109": // assure
                case "3315": // assure
                case "3316": // assure
                case "3368": // finlay
                case "3354": // eidson
                case "3366": // lanco
                case "3367": // epic
                //case "3101":
                case "3370": // cpdl
                    isDynamic = true;
                    break;
                default:
                    isDynamic = false;
                    break;
            }
            return isDynamic;
        } 
        private void SetCarryingDynamic(LocalReport Rpt1)   
        {
            // Carring cost details brfeak p2p

            if (IsCarryingDynamic())
            {
                DataTable dtorder1 = (DataTable)ViewState["tblOrder1"];
                DataTable dt4;
                DataView dv4 = dtorder1.DefaultView;
                dv4.RowFilter = ("rsircode  like '019999901%'");
                dt4 = dv4.ToTable();

                string costa = "", costb = "", costc = "", costd = "", coste = "";
                string cost1 = "", cost2 = "", cost3 = "", cost4 = "", cost5 = "";

                if (dt4.Rows.Count > 0)
                {
                    costa = dt4.Rows[0]["rsirdesc1"].ToString() == null ? "" : dt4.Rows[0]["rsirdesc1"].ToString();
                    cost1 = dt4.Rows[0]["ordramt"].ToString() == null ? "" : Convert.ToDouble(dt4.Rows[0]["ordramt"]).ToString("#,##0.00;(#,##0.00); ");
                }
                if (dt4.Rows.Count > 1)
                {
                    costb = dt4.Rows[1]["rsirdesc1"].ToString() == null ? "" : dt4.Rows[1]["rsirdesc1"].ToString();
                    cost2 = dt4.Rows[1]["ordramt"].ToString() == null ? "" : Convert.ToDouble(dt4.Rows[1]["ordramt"]).ToString("#,##0.00;(#,##0.00); "); ;
                }
                if (dt4.Rows.Count > 2)
                {
                    costc = dt4.Rows[2]["rsirdesc1"].ToString() == null ? "" : dt4.Rows[2]["rsirdesc1"].ToString();
                    cost3 = dt4.Rows[2]["ordramt"].ToString() == null ? "" : Convert.ToDouble(dt4.Rows[2]["ordramt"]).ToString("#,##0.00;(#,##0.00); ");
                }
                if (dt4.Rows.Count > 3)
                {
                    costd = dt4.Rows[3]["rsirdesc1"].ToString() == null ? "" : dt4.Rows[3]["rsirdesc1"].ToString();
                    cost4 = dt4.Rows[3]["ordramt"].ToString() == null ? "" : Convert.ToDouble(dt4.Rows[3]["ordramt"]).ToString("#,##0.00;(#,##0.00); ");
                }
                if (dt4.Rows.Count > 4)
                {
                    coste = dt4.Rows[4]["rsirdesc1"].ToString() == null ? "" : dt4.Rows[4]["rsirdesc1"].ToString();
                    cost5 = dt4.Rows[4]["ordramt"].ToString() == null ? "" : Convert.ToDouble(dt4.Rows[4]["ordramt"]).ToString("#,##0.00;(#,##0.00); ");
                }
                Rpt1.SetParameters(new ReportParameter("costa", costa));
                Rpt1.SetParameters(new ReportParameter("costb", costb));
                Rpt1.SetParameters(new ReportParameter("costc", costc));
                Rpt1.SetParameters(new ReportParameter("costd", costd));
                Rpt1.SetParameters(new ReportParameter("coste", coste));
                Rpt1.SetParameters(new ReportParameter("cost1", cost1));
                Rpt1.SetParameters(new ReportParameter("cost2", cost2));
                Rpt1.SetParameters(new ReportParameter("cost3", cost3));
                Rpt1.SetParameters(new ReportParameter("cost4", cost4));
                Rpt1.SetParameters(new ReportParameter("cost5", cost5));
            }
           
        }


        private void SendNormalMail(string orderno, string supemail)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
            DataSet dssmtpandmail = this.purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");


            string mORDERNO = orderno;

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPUREMAIL", mORDERNO, "", "", "", "", "", "", "", "");

            string subject = "Work Order";
            //SMTP
            string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(hostname, portnumber);
            //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = true;
            client.EnableSsl = false;
            string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
            string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            ///////////////////////
            ///
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new System.Net.Mail.MailAddress(frmemail);

            //msg.To.Add(new System.Net.Mail.MailAddress(ds1.Tables[0].Rows[0]["mailid"].ToString()));
            msg.To.Add(supemail);
            msg.Subject = subject;
            msg.IsBodyHtml = true;

            System.Net.Mail.Attachment attachment;

            string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf"; ;

            attachment = new System.Net.Mail.Attachment(apppath);
            msg.Attachments.Add(attachment);



            msg.Body = string.Format("<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Dear Sir," + "<br/>" + "please find attached file" + "</pre></body></html>");
            try
            {
                client.Send(msg);

                ((Label)this.Master.FindControl("lblmsg")).Text = "Your message has been successfully sent.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


                //string savelocation = Server.MapPath("~") + "\\SupWorkOreder";
                //string[] filePaths = Directory.GetFiles(savelocation);
                //foreach (string filePath in filePaths)
                //    File.Delete(filePath);

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        private void SendSSLMail(string orderno, string supemail)
        {


            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
            DataSet dssmtpandmail = this.purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");


            string mORDERNO = orderno;

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPUREMAIL", mORDERNO, "", "", "", "", "", "", "", "");

            string subject = "Work Order";
            //SMTP
            string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
            string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
            string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
            string mailtousr = supemail;// ds1.Tables[0].Rows[0]["mailid"].ToString();
            string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf";


            EASendMail.SmtpMail oMail = new EASendMail.SmtpMail("TryIt");

            //Connection Details 
            SmtpServer oServer = new SmtpServer(hostname);
            oServer.User = frmemail;
            oServer.Password = psssword;
            oServer.Port = portnumber;
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;


            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
            oMail.From = frmemail;
            oMail.To = mailtousr;
            oMail.Cc = frmemail;
            oMail.Subject = subject;


            oMail.HtmlBody = "<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Dear Sir," + "<br/>" + "please find attached file" + "</pre></body></html>";
            oMail.AddAttachment(apppath);


            //System.Net.Mail.Attachment attachment;

            //attachment = new System.Net.Mail.Attachment(apppath);
            //oMail.AddAttachment(attachment);

            try
            {

                oSmtp.SendMail(oServer, oMail);
                ((Label)this.Master.FindControl("lblmsg")).Text = "Your message has been successfully sent.";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;
            }

        }
        private void MktReqPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");


            string mReqNo = this.Request.QueryString["reqno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT", "GET_MKT_PUR_REQ_INFO", mReqNo, "",
                     "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt1 = ds1.Tables[1];
            // DataTable dt = ds1.Tables[2];

            string txtcrno = dt1.Rows[0]["reqno1"].ToString();
            string txtcrdate = Convert.ToDateTime(dt1.Rows[0]["reqdat"].ToString()).ToString("dd-MMM-yyyy");
            string txtmrfno = dt1.Rows[0]["mrfno"].ToString();
            string txtprojectname = dt1.Rows[0]["pactdesc"].ToString();
            //string txtAddress = dt1.Rows[0]["paddress"].ToString();


            //string txtbuildno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='001'").Length > 0 ? (dt.Select("termsid='001'")[0]["termsdesc"]).ToString() : ""));
            //string floorno = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='002'").Length > 0 ? (dt.Select("termsid='002'")[0]["termsdesc"]).ToString() : ""));
            //string txtfloorno = (dt.Rows.Count == 0) ? "" : (floorno + (dt.Select("termsid='003'").Length > 0 ? (", " + (dt.Select("termsid='003'")[0]["termsdesc"]).ToString()) : ""));
            //string txtpforused = ((dt.Rows.Count == 0) ? "" : (dt.Select("termsid='004'").Length > 0 ? (dt.Select("termsid='004'")[0]["termsdesc"]).ToString() : ""));

            DataTable dtr = ds1.Tables[0];

            double reqamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(preqamt)", "")) ? 0.00 : dtr.Compute("Sum(preqamt)", "")));
            double aprvamt = Convert.ToDouble((Convert.IsDBNull(dtr.Compute("Sum(areqamt)", "")) ? 0.00 : dtr.Compute("Sum(areqamt)", "")));
            double reqoapamt = aprvamt > 0 ? aprvamt : reqamt;


            string txttoamt = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            string txttoamt02 = Convert.ToDouble(reqoapamt).ToString("#,##0.00;(#,##0.00); ");
            string rpttxtnaration = dt1.Rows[0]["reqnar"].ToString();
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            string txtSign1 = "S.K";
            string txtSign2 = "Project Incharge";
            string txtSign3 = "DPM/PM/AGM/DGM";
            string txtSign4 = "Procurement";
            string txtSign5 = "Cost & Budget";
            string txtSign6 = "Head Of Construction";
            string txtSign7 = "Managing Director";


            var list = dtr.DataTableToList<RealEntity.C_28_Mpro.EClassMktProcurement.RptMktPurchaseRequisition>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_28_MPro.RptMktRequisition", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtcompanyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtRptTitle", "Marketing Purchase Requisition"));
            Rpt1.SetParameters(new ReportParameter("txtReqNo", txtcrno));
            Rpt1.SetParameters(new ReportParameter("txtReqDate", txtcrdate));
            Rpt1.SetParameters(new ReportParameter("txtMrfno", txtmrfno));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", txtprojectname));
            Rpt1.SetParameters(new ReportParameter("txtAddress", ""));
            Rpt1.SetParameters(new ReportParameter("txtBuildingNo", ""));
            Rpt1.SetParameters(new ReportParameter("txtFloorNo", ""));
            Rpt1.SetParameters(new ReportParameter("txtPurposeofUsed", ""));
            Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
            Rpt1.SetParameters(new ReportParameter("txttoamt02", txttoamt02));
            Rpt1.SetParameters(new ReportParameter("rpttxtnaration", rpttxtnaration));
            Rpt1.SetParameters(new ReportParameter("txtRptFooter", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtSign1", txtSign1));
            Rpt1.SetParameters(new ReportParameter("txtSign2", txtSign2));
            Rpt1.SetParameters(new ReportParameter("txtSign3", txtSign3));
            Rpt1.SetParameters(new ReportParameter("txtSign4", txtSign4));
            Rpt1.SetParameters(new ReportParameter("txtSign5", txtSign5));
            Rpt1.SetParameters(new ReportParameter("txtSign6", txtSign6));
            Rpt1.SetParameters(new ReportParameter("txtSign7", txtSign7));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }

        private void MktCSPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string CurDate1 = this.GetStdDate(DateTime.Today.ToString("dd.MM.yyyy"));
            string Reqno = this.Request.QueryString["reqno"].ToString();

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "GETMATREQWISE", Reqno,
                        "%", CurDate1, "", "", "", "", "", "");


            DataTable dt = ds2.Tables[1];
            DataTable dt1 = ds2.Tables[0];


            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.ComparativeStatementCreate>();
            var lst1 = dt1.DataTableToList<RealEntity.C_14_Pro.EClassPur.ComparativeStatementCreate>();
            string msrno = ds2.Tables[2].Rows[0]["msrno"].ToString();
            string preparedby = ds2.Tables[2].Rows[0]["postedname"].ToString() + "\n" + ds2.Tables[2].Rows[0]["posteddat"];
            string checkedby = ds2.Tables[2].Rows[0]["fwdname"].ToString() + "\n" + ds2.Tables[2].Rows[0]["fwddat"];
            string varifiedby = ds2.Tables[2].Rows[0]["auditname"].ToString() + "\n" + ds2.Tables[2].Rows[0]["auditdat"];

            string SVJ = ds2.Tables[2].Rows[0]["msrnar"].ToString();
            string SVJ2 = ds2.Tables[2].Rows[0]["msrnar2"].ToString();
            string SVJ3 = ds2.Tables[2].Rows[0]["msrnar3"].ToString();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_28_MPro.RptMktPurMarketSurvey", lst, lst1, null);
            Rpt1.SetParameters(new ReportParameter("BestS", "Best Selection"));
            Rpt1.SetParameters(new ReportParameter("CS", "Comparative Statement"));
            Rpt1.SetParameters(new ReportParameter("SVJ", "Purchase Justification: " + SVJ));
            Rpt1.SetParameters(new ReportParameter("SVJ2", "Audit Justification: " + SVJ2));
            Rpt1.SetParameters(new ReportParameter("SVJ3", "MD Justification: " + SVJ3));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("msrno", "MSR No: " + msrno));
            Rpt1.SetParameters(new ReportParameter("Reqno", "Req No: " + Reqno));
            Rpt1.SetParameters(new ReportParameter("CurDate1", "Date: " + CurDate1));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Marketing Market Survey Information"));
            Rpt1.SetParameters(new ReportParameter("preparedby", preparedby));
            Rpt1.SetParameters(new ReportParameter("checkedby", checkedby));
            Rpt1.SetParameters(new ReportParameter("varifiedby", varifiedby));
            Rpt1.SetParameters(new ReportParameter("paystatus", ""));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }
        private void MktOrderPrint()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string orderNo = this.Request.QueryString["orderno"].ToString();
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

                string ordercopy = "";
                DataSet _ReportDataSet = purData.GetTransInfo(comcod, "SP_REPORT_MKT_PROCUREMENT", "SHOW_WORK_ORDER", orderNo, ordercopy, "", "", "", "", "", "", "");

                List<RealEntity.C_12_Inv.EclassPurchase.MktPurchaseOrderInfo> purlist = _ReportDataSet.Tables[0].DataTableToList<RealEntity.C_12_Inv.EclassPurchase.MktPurchaseOrderInfo>();
                List<RealEntity.C_12_Inv.EclassPurchase.PurOrderTermsCondition> termscondition = _ReportDataSet.Tables[2].DataTableToList<RealEntity.C_12_Inv.EclassPurchase.PurOrderTermsCondition>();
                List<RealEntity.C_12_Inv.EclassPurchase.PaymentSchedule> paymentschedule = _ReportDataSet.Tables[5].DataTableToList<RealEntity.C_12_Inv.EclassPurchase.PaymentSchedule>();
                ViewState["tblpaysch"] = paymentschedule;
                string orderno = _ReportDataSet.Tables[4].Rows[0]["orderno"].ToString().Substring(0, 3) + _ReportDataSet.Tables[4].Rows[0]["orderno"].ToString().Substring(7, 2) + "-" + ASTUtility.Right(_ReportDataSet.Tables[4].Rows[0]["orderno"].ToString(), 5);
                //string oissueno = _ReportDataSet.Tables[4].Rows[0]["oissueno"].ToString();
                string porderno = orderno;
                string fax = _ReportDataSet.Tables[1].Rows[0]["fax"].ToString();


                string pactcode = _ReportDataSet.Tables[0].Rows[0]["pactcode"].ToString();
                string prjaddress = _ReportDataSet.Tables[0].Rows[0]["proadd"].ToString();
                string pactdesc = _ReportDataSet.Tables[0].Rows[0]["pactdesc"].ToString();


                string mrfno1 = _ReportDataSet.Tables[6].Rows[0]["mrfno"].ToString();

                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_02", "GET_PUR_ORDER_INFO", orderNo, "", "", "", "", "", "", "", "");

                ViewState["tblOrder1"] = ds1.Tables[0];

                DataTable dtorder1 = (DataTable)ViewState["tblOrder1"];
                DataTable dt1;
                DataTable dt2;
                DataTable dt3;

                // Carring
                DataView dv1 = dtorder1.DefaultView;
                dv1.RowFilter = ("prtype  like '019999901%'");
                dt1 = dv1.ToTable();

                //Deduction
                dv1 = dtorder1.DefaultView;
                dv1.RowFilter = ("prtype like'019999902%' ");
                dt2 = dv1.ToTable();

                //Material
                dv1 = dtorder1.DefaultView;
                dv1.RowFilter = ("prtype not like '0199999%'");
                dt3 = dv1.ToTable();

                string discountdesc = dtorder1.Select("prtype like '019999902003%'").Length == 0 ? "Discount" : dtorder1.Select("prtype like '019999902003%'")[0]["prtypedesc"].ToString();

                double amtcar = (dt1.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(ordramt)", "")) ? 0.00 : dt1.Compute("Sum(ordramt)", "")));
                double amtdis = (dt2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(ordramt)", "")) ? 0.00 : dt2.Compute("Sum(ordramt)", "")));
                double amtmat = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(ordramt)", "")) ? 0.00 : dt3.Compute("Sum(ordramt)", "")));


                string nettotalamt = (amtmat + amtcar - amtdis).ToString("#,##0.00;(#,##0.00);");
                string advamt = Convert.ToDouble(_ReportDataSet.Tables[4].Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); ");



                string inword = "In Word: " + ASTUtility.Trans(amtmat + amtcar - amtdis, 2);


                string sign1 = "", sign2 = "", sign3 = "", sign4 = "", sign5 = "", sign6 = "", sign7 = "", sign8 = "";

                /// signature       // appnam - PURAPROVB and ordnam - purorder     
                switch (comcod)
                {

                    case "3354": //Edison Real Estate
                                 //case "3101":
                        sign1 = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        sign2 = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        sign3 = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        sign4 = _ReportDataSet.Tables[3].Rows[0]["csprep"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["csprepdat"].ToString();
                        sign5 = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        sign6 = _ReportDataSet.Tables[3].Rows[0]["ordfappnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["ordfappdat"].ToString();
                        sign7 = _ReportDataSet.Tables[3].Rows[0]["ordappnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["ordappdat"].ToString();
                        sign8 = _ReportDataSet.Tables[3].Rows[0]["csapp"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["csappdat"].ToString();
                        break;

                    default:
                        sign1 = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        sign2 = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        //sign3 = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        //sign4 = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        sign5 = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        sign6 = "Approved By";
                        break;
                }

                // basic information/////////
                string subject = _ReportDataSet.Tables[4].Rows[0]["subject"].ToString();
                string leterdesc = _ReportDataSet.Tables[4].Rows[0]["leterdes"].ToString();

                string ordrefno = _ReportDataSet.Tables[4].Rows[0]["pordref"].ToString();
                string supname = _ReportDataSet.Tables[1].Rows[0]["ssirdesc"].ToString();
                string Supadd = _ReportDataSet.Tables[1].Rows[0]["address"].ToString();
                string Supmobile = _ReportDataSet.Tables[1].Rows[0]["phone"].ToString();
                string cperson = _ReportDataSet.Tables[1].Rows[0]["cperson"].ToString();
                string podate = Convert.ToDateTime(_ReportDataSet.Tables[1].Rows[0]["orderdat"]).ToString("dd-MMM-yyyy");
                string pordnar = _ReportDataSet.Tables[4].Rows[0]["pordnar"].ToString();

                //string terms = _ReportDataSet.Tables[4].Rows[0]["terms"].ToString();

                string cperson2 = "";

                // Terms & Conditions Variables//

                string terms1 = "", terms2 = "", terms3 = "", terms4 = "", terms5 = "", terms6 = "", terms7 = "", terms8 = "",
                    terms9 = "", terms10 = "", terms11 = "", terms12 = "";
                string pperson1 = "", pperson2 = "";


                switch (comcod)
                {

                    case "3354": // Edison Real estate
                                 //case "3101":
                        terms1 = termscondition.FindAll(p => p.termsid == "001").Count == 0 ? "" : (termscondition.FindAll(p => p.termsid == "001")[0].termsdesc.ToString().Length > 0 ? "1." + (termscondition.FindAll(p => p.termsid == "001")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "001")[0].termsdesc.ToString()) : "");
                        terms2 = termscondition.FindAll(p => p.termsid == "002").Count == 0 ? "" : (termscondition.FindAll(p => p.termsid == "002")[0].termsdesc.ToString().Length > 0 ? "2." + (termscondition.FindAll(p => p.termsid == "002")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "002")[0].termsdesc.ToString()) : "");
                        terms3 = termscondition.FindAll(p => p.termsid == "003").Count == 0 ? "" : (termscondition.FindAll(p => p.termsid == "003")[0].termsdesc.ToString().Length > 0 ? "3." + (termscondition.FindAll(p => p.termsid == "003")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "003")[0].termsdesc.ToString()) : "");
                        terms4 = termscondition.FindAll(p => p.termsid == "004").Count == 0 ? "" : (termscondition.FindAll(p => p.termsid == "004")[0].termsdesc.ToString().Length > 0 ? "4." + (termscondition.FindAll(p => p.termsid == "004")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "004")[0].termsdesc.ToString()) : "");
                        terms5 = termscondition.FindAll(p => p.termsid == "005").Count == 0 ? "" : (termscondition.FindAll(p => p.termsid == "005")[0].termsdesc.ToString().Length > 0 ? "5." + (termscondition.FindAll(p => p.termsid == "005")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "005")[0].termsdesc.ToString()) : "");
                        terms6 = termscondition.FindAll(p => p.termsid == "006").Count == 0 ? "" : (termscondition.FindAll(p => p.termsid == "006")[0].termsdesc.ToString().Length > 0 ? "6." + (termscondition.FindAll(p => p.termsid == "006")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "006")[0].termsdesc.ToString()) : "");
                        terms7 = termscondition.FindAll(p => p.termsid == "007").Count == 0 ? "" : (termscondition.FindAll(p => p.termsid == "007")[0].termsdesc.ToString().Length > 0 ? "7." + (termscondition.FindAll(p => p.termsid == "007")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "007")[0].termsdesc.ToString()) : "");
                        terms8 = termscondition.FindAll(p => p.termsid == "008").Count == 0 ? "" : (termscondition.FindAll(p => p.termsid == "008")[0].termsdesc.ToString().Length > 0 ? "8." + (termscondition.FindAll(p => p.termsid == "008")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "008")[0].termsdesc.ToString()) : "");
                        terms9 = termscondition.FindAll(p => p.termsid == "009").Count == 0 ? "" : (termscondition.FindAll(p => p.termsid == "009")[0].termsdesc.ToString().Length > 0 ? "9." + (termscondition.FindAll(p => p.termsid == "009")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "009")[0].termsdesc.ToString()) : "");
                        terms10 = termscondition.FindAll(p => p.termsid == "010").Count == 0 ? "" : (termscondition.FindAll(p => p.termsid == "010")[0].termsdesc.ToString().Length > 0 ? "10." + (termscondition.FindAll(p => p.termsid == "010")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "010")[0].termsdesc.ToString()) : "");
                        terms11 = termscondition.FindAll(p => p.termsid == "011").Count == 0 ? "" : (termscondition.FindAll(p => p.termsid == "011")[0].termsdesc.ToString().Length > 0 ? "11." + (termscondition.FindAll(p => p.termsid == "011")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "011")[0].termsdesc.ToString()) : "");
                        terms12 = termscondition.FindAll(p => p.termsid == "012").Count == 0 ? "" : (termscondition.FindAll(p => p.termsid == "012")[0].termsdesc.ToString().Length > 0 ? "12." + (termscondition.FindAll(p => p.termsid == "012")[0].termssubj.ToString()) + " : " + (termscondition.FindAll(p => p.termsid == "012")[0].termsdesc.ToString()) : "");

                        break;

                    default: //Default
                        terms1 = "* " + termscondition[0].termssubj.ToString() + ":" + termscondition[0].termsdesc.ToString();
                        terms2 = "* " + termscondition[1].termssubj.ToString() + ":" + termscondition[1].termsdesc.ToString();
                        terms3 = "* " + termscondition[2].termssubj.ToString() + ":" + termscondition[2].termsdesc.ToString();
                        terms4 = "* " + termscondition[3].termssubj.ToString() + ":" + termscondition[3].termsdesc.ToString();
                        terms5 = "* " + termscondition[4].termssubj.ToString() + ":" + termscondition[4].termsdesc.ToString();
                        break;
                }

                // Set Report Name
                string Reportpath = "";
                switch (comcod)
                {
                    case "3354": //Edison Real Estate
                                 //case "3101":
                        Reportpath = "~/Report/RptMktPurchaseOrder.rdlc";
                        break;

                    default:
                        Reportpath = "~/Report/RptMktPurchaseOrder.rdlc";
                        break;
                }


                LocalReport Rpt1 = new LocalReport();
                Rpt1.DisplayName = "RptPurchaseOrder";
                Rpt1.ReportPath = Server.MapPath(Reportpath);
                Rpt1.DataSources.Clear();
                Rpt1.DataSources.Add(new ReportDataSource("DataSet1", purlist));
                Rpt1.DataSources.Add(new ReportDataSource("DataSet2", termscondition));
                Rpt1.EnableExternalImages = true;

                Rpt1.SetParameters(new ReportParameter("compname", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("disamt", amtdis.ToString("#,##0.00;(#,##0.00); ")));
                Rpt1.SetParameters(new ReportParameter("carecost", amtcar.ToString("#,##0.00;(#,##0.00); ")));
                Rpt1.SetParameters(new ReportParameter("nettotal", nettotalamt));
                Rpt1.SetParameters(new ReportParameter("advamt", advamt));
                Rpt1.SetParameters(new ReportParameter("discountdesc", discountdesc));

                // basic information send to report//
                Rpt1.SetParameters(new ReportParameter("subject", subject));
                Rpt1.SetParameters(new ReportParameter("leterdesc", leterdesc));
                Rpt1.SetParameters(new ReportParameter("porderno", porderno));
                Rpt1.SetParameters(new ReportParameter("ordrefno", ordrefno));
                Rpt1.SetParameters(new ReportParameter("supname", supname));
                Rpt1.SetParameters(new ReportParameter("supadd", Supadd));
                Rpt1.SetParameters(new ReportParameter("supmobile", Supmobile));
                Rpt1.SetParameters(new ReportParameter("cperson", cperson));
                Rpt1.SetParameters(new ReportParameter("podate", podate));
                Rpt1.SetParameters(new ReportParameter("faxnumber", fax));

                // signature send to report//
                Rpt1.SetParameters(new ReportParameter("sign1", sign1));
                Rpt1.SetParameters(new ReportParameter("sign2", sign2));
                Rpt1.SetParameters(new ReportParameter("sign3", sign3));
                Rpt1.SetParameters(new ReportParameter("sign4", sign4));
                Rpt1.SetParameters(new ReportParameter("sign5", sign5));
                Rpt1.SetParameters(new ReportParameter("sign6", sign6));
                Rpt1.SetParameters(new ReportParameter("sign7", sign7));
                Rpt1.SetParameters(new ReportParameter("sign8", sign8));

                // Terms & Condition send to report//
                Rpt1.SetParameters(new ReportParameter("terms1", terms1));
                Rpt1.SetParameters(new ReportParameter("terms2", terms2));
                Rpt1.SetParameters(new ReportParameter("terms3", terms3));
                Rpt1.SetParameters(new ReportParameter("terms4", terms4));
                Rpt1.SetParameters(new ReportParameter("terms5", terms5));
                Rpt1.SetParameters(new ReportParameter("terms6", terms6));
                Rpt1.SetParameters(new ReportParameter("terms7", terms7));
                Rpt1.SetParameters(new ReportParameter("terms8", terms8));
                Rpt1.SetParameters(new ReportParameter("terms9", terms9));
                Rpt1.SetParameters(new ReportParameter("terms10", terms10));
                Rpt1.SetParameters(new ReportParameter("terms11", terms11));
                Rpt1.SetParameters(new ReportParameter("terms12", terms12));

                Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("inword", inword));
                Rpt1.SetParameters(new ReportParameter("pordnar", pordnar));


                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
                return;
            }
        }

        private void MktMRRPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string mMRRNo = this.Request.QueryString["mrno"].ToString();
            string CurDate1 = System.DateTime.Now.ToString("dd-MMM-yyyy");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_03", "GET_PUR_MRR_INFO", mMRRNo, CurDate1,
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            string suppliername = ds1.Tables[1].Rows[0]["ssirdesc1"].ToString();
            string prjname = ds1.Tables[1].Rows[0]["pactdesc"].ToString();

            DataTable dt = this.HiddenSameDataMktMRR(ds1.Tables[0]);

            string mrrno1 = ds1.Tables[1].Rows[0]["mrrno1"].ToString();
            string porno = ds1.Tables[1].Rows[0]["orderno1"].ToString();
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_28_Mpro.EClassMktProcurement.RptMktPurchaseMrr>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_28_MPro.RptMktPurMRR", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtprjname", "Project Name : " + prjname));
            Rpt1.SetParameters(new ReportParameter("txtSubName", "Supplier Name : " + suppliername));
            Rpt1.SetParameters(new ReportParameter("txtchalanno", "Chalan No : " + ds1.Tables[1].Rows[0]["chlnno"]));
            Rpt1.SetParameters(new ReportParameter("txtMrrno", "MRR No : " + mrrno1));
            Rpt1.SetParameters(new ReportParameter("txtMrrRef", "MRR Ref : " + ds1.Tables[1].Rows[0]["mrrref"]));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date : " + Convert.ToDateTime(ds1.Tables[1].Rows[0]["mrrdat"]).ToString("dd.MM.yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtQc", "Quality Certificate : " + ds1.Tables[1].Rows[0]["qcno"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtOrder", ds1.Tables[1].Rows[0]["pordref"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtpostedby", ds1.Tables[1].Rows[0]["usrnam"].ToString()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Marketing Material Received"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("txtOrderno", "Order No : " + porno));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }
        void LoadSubReport(object sender, SubreportProcessingEventArgs e)
        {
            List<RealEntity.C_12_Inv.EclassPurchase.PaymentSchedule> lst = (List<RealEntity.C_12_Inv.EclassPurchase.PaymentSchedule>)ViewState["tblpaysch"];
            e.DataSources.Add(new ReportDataSource("DataSet1", lst));
        }
        void LoadSubReportAssure(object sender, SubreportProcessingEventArgs e)
        {
            List<RealEntity.C_12_Inv.EclassPurchase.MktPurchasePayment> lst = (List<RealEntity.C_12_Inv.EclassPurchase.MktPurchasePayment>)ViewState["tblpayment01"];
            e.DataSources.Add(new ReportDataSource("DataSet1", lst));
        }
        private string CompanyBill()
        {
            string comcod = this.GetCompCode();
            string PrintReq = "";
            switch (comcod)
            {
                case "1101":
                case "2305":
                case "3305":
                case "3306":
                case "3307":
                case "3308":
                case "3309":
                case "3310":
                    PrintReq = "PrintBill02";
                    break;

                case "3312":
                case "3311":
                case "1108":
                case "1109":
                case "3315":
                case "3316":
                case "3317":

                    PrintReq = "PrintBill03";

                    break;
                //case "3101":
                case "3330":
                    PrintReq = "PrintBill04";

                    break;
                // case "3101":
                case "3332":
                case "3335":
                case "1206": // acme
                case "1207": // acme
                case "3338": // acme
                case "3369": // acme
                case "3336":
                case "3337":
                    PrintReq = "PrintBill05";
                    break;

                //case "3101":
                case "3333":
                    PrintReq = "PrintBill06";
                    break;
                default:
                    PrintReq = "PrintBill04";
                    break;
            }

            return PrintReq;

        }

        private void PurBill_Print()
        {


            string printcomreq = this.CompanyBill();

            if (printcomreq == "PrintBill01")
                this.PrintBill01();
            else if (printcomreq == "PrintBill02")
                this.PrintBill02();
            else if (printcomreq == "PrintBill03")
                this.PrintBill03();
            else if (printcomreq == "PrintBill04")
                this.PrintBill04();

            else if (printcomreq == "PrintBill05")
                this.PrintBill05();

            else if (printcomreq == "PrintBill06")
                this.PrintBill06();
            else
                this.PrintBill02();

        }

        private void PrintBill06()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string mBILLNo = this.ddlPrevBillList.SelectedValue.ToString();
            string mBILLNo = this.Request.QueryString["billno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, "",
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];

            //For Tk In Word

            DataTable td1 = dt.Copy();
            DataTable td2 = dt.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like'019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            double amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));
            double amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));
            //

            double security = Convert.ToDouble(ds1.Tables[1].Rows[0]["sdamt"].ToString());
            double deduction = Convert.ToDouble(ds1.Tables[1].Rows[0]["dedamt"].ToString()); ;
            double penalty = Convert.ToDouble(ds1.Tables[1].Rows[0]["penamt"].ToString()); ;
            double advanced = Convert.ToDouble(ds1.Tables[1].Rows[0]["advamt"].ToString());
            double netAmount = (amt1 - amt2 - (security + deduction + penalty + advanced));
            double percntge = Convert.ToDouble(ds1.Tables[1].Rows[0]["percntge"].ToString());
            string CurDate1 = "Date: " + Convert.ToDateTime(ds1.Tables[1].Rows[0]["billdat"]).ToString("dd-MMM-yyyy");
            string suppname = "Supplier Name: " + ds1.Tables[1].Rows[0]["ssirdesc"].ToString();
            string billno = "Bill No: " + ds1.Tables[1].Rows[0]["billno1"].ToString();
            string mrrno = ds1.Tables[0].Rows[0]["mrrno1"].ToString();
            string projectName = "Project Name : " + ds1.Tables[0].Rows[0]["pactdesc"].ToString().Substring(4);
            string narration = "Narration : " + ds1.Tables[1].Rows[0]["billnar"].ToString();

            string inword = "Taka In Word: " + ASTUtility.Trans((netAmount), 2);
            string netamt = Convert.ToDouble(netAmount).ToString("#,##0.00;(#,##0.00); ");

            //
            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptBillAlliInfo", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Software Generated Bill"));
            rpt.SetParameters(new ReportParameter("supname", suppname));
            rpt.SetParameters(new ReportParameter("txtBillno", billno));
            rpt.SetParameters(new ReportParameter("date1", "Date: " + CurDate1));
            rpt.SetParameters(new ReportParameter("txtInword", inword));
            rpt.SetParameters(new ReportParameter("txtNarration", narration));
            rpt.SetParameters(new ReportParameter("txtSecurity", security.ToString()));
            rpt.SetParameters(new ReportParameter("txtAdv", advanced.ToString()));
            rpt.SetParameters(new ReportParameter("txtPenalty", penalty.ToString()));
            rpt.SetParameters(new ReportParameter("txtDeduc", deduction.ToString()));
            rpt.SetParameters(new ReportParameter("txtNetAmt", netamt.ToString()));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comLogo", ComLogo));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintBill01()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string mBILLNo = this.ddlPrevBillList.SelectedValue.ToString();
            string mBILLNo = this.Request.QueryString["billno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, "",
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];

            //For Tk In Word

            DataTable td1 = dt.Copy();
            DataTable td2 = dt.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like'019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            double amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));
            double amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));
            //



            ReportDocument rptstk = new RealERPRPT.R_14_Pro.rptBillInfo();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            TextObject rpttxtsupplier = rptstk.ReportDefinition.ReportObjects["txtsupplier"] as TextObject;
            rpttxtsupplier.Text = "Supplier Name: " + ds1.Tables[1].Rows[0]["ssirdesc"].ToString();
            TextObject rpttxtbillno = rptstk.ReportDefinition.ReportObjects["billno"] as TextObject;
            rpttxtbillno.Text = "Bill No: " + ds1.Tables[1].Rows[0]["billno1"].ToString();
            TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            rpttxtdate.Text = "Date: " + Convert.ToDateTime(ds1.Tables[1].Rows[0]["billdat"]).ToString("dd-MMM-yyyy");






            TextObject rpttxtTotalAmount = rptstk.ReportDefinition.ReportObjects["txtTotalAmount"] as TextObject;
            rpttxtTotalAmount.Text = (amt1 - amt2).ToString("#,##0;(#,##0); ");

            TextObject rpttxtTaka = rptstk.ReportDefinition.ReportObjects["takainword"] as TextObject;
            rpttxtTaka.Text = "Taka In Word: " + ASTUtility.Trans((amt1 - amt2), 2);
            TextObject rpttxtNarration = rptstk.ReportDefinition.ReportObjects["txtNarration"] as TextObject;
            rpttxtNarration.Text = ds1.Tables[1].Rows[0]["billnar"].ToString();

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            rptstk.SetDataSource(this.HiddenSameData(ds1.Tables[0]));
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintBill02()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string mBILLNo = this.ddlPrevBillList.SelectedValue.ToString();
            string mBILLNo = this.Request.QueryString["billno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, "",
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];

            double amt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(mrramt)", "")) ? 0.00 : dt.Compute("Sum(mrramt)", "")));

            DataTable dt1 = ds1.Tables[2];
            DataTable dtmrref = ds1.Tables[1];
            string mrfno = dtmrref.Rows[0]["mrfno"].ToString();
            string mrfno1 = dtmrref.Rows[0]["mrfno"].ToString();

            for (int i = 1; i < dtmrref.Rows.Count; i++)
            {

                if (dtmrref.Rows[i]["mrfno"].ToString() == mrfno)
                    ;
                else
                {
                    mrfno1 = mrfno1 + ", " + dtmrref.Rows[i]["mrfno"].ToString();

                }

                mrfno = dtmrref.Rows[i]["mrfno"].ToString();



            }

            string paytype = ds1.Tables[1].Rows[0]["paytype"].ToString();



            string txtProjName = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            string txtBuildno = dt1.Rows[0]["termsdesc"].ToString();
            string txtFloorno = dt1.Rows[1]["termsdesc"].ToString();
            string txtFlatno = dt1.Rows[2]["termsdesc"].ToString();
            string txtSupName = "Supplier Name: " + ds1.Tables[1].Rows[0]["ssirdesc"].ToString();
            string txtBillid = ds1.Tables[1].Rows[0]["billno1"].ToString();
            string billrefdate = Convert.ToDateTime(ds1.Tables[1].Rows[0]["billrefdat"].ToString()).ToString("dd-MMM-yyyy");
            string txtBillno = "Bill No: " + ds1.Tables[1].Rows[0]["billno1"].ToString();
            string chqdate = (paytype == "003") ? "" : Convert.ToDateTime(ds1.Tables[1].Rows[0]["chequedat"].ToString()).ToString("dd-MMM-yyyy");
            string txtInword = "Taka In Word: " + ASTUtility.Trans(Math.Round(amt, 0), 2);
            string CurDate1 = "Date: " + Convert.ToDateTime(ds1.Tables[1].Rows[0]["billdat"]).ToString("dd-MMM-yyyy");

            string txtNarration = "Narration : " + ds1.Tables[1].Rows[0]["billnar"].ToString();
            //string txtDepo = (Convert.ToDouble("0" + this.txtSDAmount.Text.ToString().Trim()) == 0.00) ? "" : ("Security Deposit (" + this.txtpercentage.Text.Trim() + ") : " + this.txtSDAmount.Text.ToString());
            //string txtAdv = (Convert.ToDouble("0" + this.txtAdvanced.Text.ToString().Trim()) == 0.00) ? "" : ("Advanced:   " + this.txtAdvanced.Text.ToString().Trim());
            //string txtPenalty = (Convert.ToDouble("0" + this.txtPenaltyAmount.Text.ToString().Trim()) == 0.00) ? "" : ("Pelanty:   " + this.txtPenaltyAmount.Text.ToString().Trim());
            //string txtDeduc = (Convert.ToDouble("0" + this.txtDedAmount.Text.ToString().Trim()) == 0.00) ? "" : ("Deduction:   " + this.txtDedAmount.Text.ToString().Trim());
            string txtNetAmt = "Net Amount : " + Convert.ToDouble(amt).ToString("#,##0.00;(#,##0.00); ");

            string ftReqIn = ds1.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqdat"].ToString();
            string ftReqApp = ds1.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqadat"].ToString();
            string ftOrder = ds1.Tables[3].Rows[0]["appnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["appdat"].ToString();
            string ftWorkOrd = ds1.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["orddat"].ToString();
            string ftMrRecv = ds1.Tables[3].Rows[0]["mrrnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["mrrdat"].ToString();
            string ftBillConf = ds1.Tables[3].Rows[0]["billnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["billdat"].ToString();

            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptBillConfirmation02", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Bill Confirmation Certificate"));
            rpt.SetParameters(new ReportParameter("txtComment", "( Comments On Enclosed Bills )"));
            rpt.SetParameters(new ReportParameter("txtProjName", " : " + txtProjName));
            rpt.SetParameters(new ReportParameter("txtBuildno", " : " + txtBuildno));
            rpt.SetParameters(new ReportParameter("txtFloorno", " : " + txtFloorno));
            rpt.SetParameters(new ReportParameter("txtFlatno", " : " + txtFlatno));
            rpt.SetParameters(new ReportParameter("txtSupName", "Supplier Name  :  " + txtSupName));
            rpt.SetParameters(new ReportParameter("txtBillid", " : " + txtBillid));
            rpt.SetParameters(new ReportParameter("txtBilldate", " : " + billrefdate));
            rpt.SetParameters(new ReportParameter("txtBillno", " : " + txtBillno));
            rpt.SetParameters(new ReportParameter("mprno", " : " + mrfno1));
            rpt.SetParameters(new ReportParameter("date", " : " + CurDate1));
            rpt.SetParameters(new ReportParameter("chqdate", " : " + chqdate));
            rpt.SetParameters(new ReportParameter("txtDepo", ""));
            rpt.SetParameters(new ReportParameter("txtAdv", ""));
            rpt.SetParameters(new ReportParameter("txtPenalty", ""));
            rpt.SetParameters(new ReportParameter("txtDeduc", ""));
            rpt.SetParameters(new ReportParameter("txtNetAmt", txtNetAmt));

            rpt.SetParameters(new ReportParameter("txtInword", txtInword));
            rpt.SetParameters(new ReportParameter("txtNarration", txtNarration));

            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            // rpt.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintBill03()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string mBILLNo = this.ddlPrevBillList.SelectedValue.ToString();
            string mBILLNo = this.Request.QueryString["billno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, "",
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];

            DataTable td1 = dt.Copy();
            DataTable td2 = dt.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like'019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            double amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));
            double amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));


            double security = Convert.ToDouble(ds1.Tables[1].Rows[0]["sdamt"].ToString());
            double deduction = Convert.ToDouble(ds1.Tables[1].Rows[0]["dedamt"].ToString()); ;
            double penalty = Convert.ToDouble(ds1.Tables[1].Rows[0]["penamt"].ToString()); ;
            double advanced = Convert.ToDouble(ds1.Tables[1].Rows[0]["advamt"].ToString());
            double netAmount = (amt1 - amt2 - (security + deduction + penalty + advanced));
            double percntge = Convert.ToDouble(ds1.Tables[1].Rows[0]["percntge"].ToString());


            DataTable dt1 = ds1.Tables[2];
            DataTable dtmrref = ds1.Tables[1];
            string mrfno = dtmrref.Rows[0]["mrfno"].ToString();
            string mrfno1 = dtmrref.Rows[0]["mrfno"].ToString();


            for (int i = 1; i < dtmrref.Rows.Count; i++)
            {

                if (dtmrref.Rows[i]["mrfno"].ToString() == mrfno)
                    ;
                else
                {
                    mrfno1 = mrfno1 + ", " + dtmrref.Rows[i]["mrfno"].ToString();

                }

                mrfno = dtmrref.Rows[i]["mrfno"].ToString();



            }
            //double amt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(mrramt)", "")) ? 0.00 : dt.Compute("Sum(mrramt)", "")));

            //double tamt=this.gvBillInfo.FooterRow
            string CurDate1 = "Date: " + Convert.ToDateTime(ds1.Tables[1].Rows[0]["billdat"]).ToString("dd-MMM-yyyy");
            string txtProjName = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            string txtBuildno = dt1.Rows[0]["termsdesc"].ToString();
            string txtFloorno = dt1.Rows[1]["termsdesc"].ToString();
            string txtFlatno = dt1.Rows[2]["termsdesc"].ToString();
            string txtSupName = ds1.Tables[1].Rows[0]["ssirdesc"].ToString();
            string txtBillid = ds1.Tables[1].Rows[0]["billno1"].ToString();
            string billrefdate = "Bill Date : " + Convert.ToDateTime(ds1.Tables[1].Rows[0]["billrefdat"].ToString()).ToString("dd-MMM-yyyy");
            string txtBillno = "Bill No : " + ds1.Tables[1].Rows[0]["billno1"].ToString();
            string txtReqno = mrfno1;
            string chqdate = "Cheque Date : " + Convert.ToDateTime(ds1.Tables[1].Rows[0]["chequedat"].ToString()).ToString("dd-MMM-yyyy");
            string txtInword = "Taka In Word: " + ASTUtility.Trans((netAmount), 2);


            string txtNarration = ds1.Tables[1].Rows[0]["billno1"].ToString();
            string txtDepo = (security == 0.00) ? "" : ("Security Deposit (" + percntge.ToString("#,##0.00;(#,##0.00);") + ") : " + security.ToString("#,##0.00;(#,##0.00);"));
            string txtAdv = (advanced == 0.00) ? "" : ("Advanced: " + advanced.ToString("#,##0.00;(#,##0.00);"));
            string txtPenalty = (penalty == 0.00) ? "" : ("Pelanty: " + penalty.ToString("#,##0.00;(#,##0.00);"));
            string txtDeduc = (deduction == 0.00) ? "" : ("Deduction: " + deduction.ToString("#,##0.00;(#,##0.00);"));
            string txtNetAmt = "Net Amount : " + Convert.ToDouble(netAmount).ToString("#,##0.00;(#,##0.00); ");

            string ftReqIn = ds1.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqdat"].ToString();
            string ftReqApp = ds1.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqadat"].ToString();
            string ftOrder = ds1.Tables[3].Rows[0]["appnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["appdat"].ToString();
            string ftWorkOrd = ds1.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["orddat"].ToString();
            string ftMrRecv = ds1.Tables[3].Rows[0]["mrrnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["mrrdat"].ToString();
            string ftBillConf = ds1.Tables[3].Rows[0]["billnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["billdat"].ToString();

            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptBillConfirmation03", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Bill Confirmation Certificate"));
            rpt.SetParameters(new ReportParameter("txtComment", "( Comments On Enclosed Bills )"));
            rpt.SetParameters(new ReportParameter("txtProjName", txtProjName));
            rpt.SetParameters(new ReportParameter("txtBuildno", txtBuildno));
            rpt.SetParameters(new ReportParameter("txtFloorno", txtFloorno));
            rpt.SetParameters(new ReportParameter("txtFlatno", txtFlatno));
            rpt.SetParameters(new ReportParameter("txtSupName", "Supplier Name :  " + txtSupName));
            rpt.SetParameters(new ReportParameter("txtBillid", txtBillid));
            rpt.SetParameters(new ReportParameter("txtBilldate", billrefdate));
            rpt.SetParameters(new ReportParameter("txtBillno", txtBillno));
            rpt.SetParameters(new ReportParameter("txtReqno", txtReqno));
            rpt.SetParameters(new ReportParameter("date", CurDate1));

            rpt.SetParameters(new ReportParameter("chqdate", chqdate));
            rpt.SetParameters(new ReportParameter("txtDepo", txtDepo));
            rpt.SetParameters(new ReportParameter("txtAdv", txtAdv));
            rpt.SetParameters(new ReportParameter("txtPenalty", txtPenalty));
            rpt.SetParameters(new ReportParameter("txtDeduc", txtDeduc));
            rpt.SetParameters(new ReportParameter("txtNetAmt", txtNetAmt));

            rpt.SetParameters(new ReportParameter("txtInword", txtInword));
            rpt.SetParameters(new ReportParameter("txtNarration", txtNarration));
            rpt.SetParameters(new ReportParameter("ftReqIn", ftReqIn));
            rpt.SetParameters(new ReportParameter("ftReqApp", ftReqApp));
            rpt.SetParameters(new ReportParameter("ftOrder", ftOrder));
            rpt.SetParameters(new ReportParameter("ftWorkOrd", ftWorkOrd));
            rpt.SetParameters(new ReportParameter("ftMrRecv", ftMrRecv));
            rpt.SetParameters(new ReportParameter("ftBillConf", ftBillConf));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            // rpt.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintBill04()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string mBILLNo = this.ddlPrevBillList.SelectedValue.ToString();
            string mBILLNo = this.Request.QueryString["billno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, "",
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];

            DataTable td1 = dt.Copy();
            DataTable td2 = dt.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like'019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            double amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));
            double amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));


            double security = Convert.ToDouble(ds1.Tables[1].Rows[0]["sdamt"].ToString());
            double deduction = Convert.ToDouble(ds1.Tables[1].Rows[0]["dedamt"].ToString()); ;
            double penalty = Convert.ToDouble(ds1.Tables[1].Rows[0]["penamt"].ToString()); ;
            double advanced = Convert.ToDouble(ds1.Tables[1].Rows[0]["advamt"].ToString());
            double netAmount = (amt1 - amt2 - (security + deduction + penalty + advanced));
            double percntge = Convert.ToDouble(ds1.Tables[1].Rows[0]["percntge"].ToString());

            //double amt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(mrramt)", "")) ? 0.00 : dt.Compute("Sum(mrramt)", "")));

            //double tamt=this.gvBillInfo.FooterRow



            DataTable dt1 = ds1.Tables[2];
            DataTable dtmrref = ds1.Tables[1];
            string mrfno = dtmrref.Rows[0]["mrfno"].ToString();
            string mrfno1 = dtmrref.Rows[0]["mrfno"].ToString();


            for (int i = 1; i < dtmrref.Rows.Count; i++)
            {
                if (dtmrref.Rows[i]["mrfno"].ToString() == mrfno) ;
                else
                {
                    mrfno1 = mrfno1 + ", " + dtmrref.Rows[i]["mrfno"].ToString();

                }

                mrfno = dtmrref.Rows[i]["mrfno"].ToString();
            }

            //double amout=this.txtDedAmount.Text.ToString()

            string CurDate1 = "Date: " + Convert.ToDateTime(ds1.Tables[1].Rows[0]["billdat"]).ToString("dd-MMM-yyyy");
            string txtProjName = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            string txtBuildno = dt1.Rows[0]["termsdesc"].ToString();
            string txtFloorno = dt1.Rows[1]["termsdesc"].ToString();
            string txtFlatno = dt1.Rows[2]["termsdesc"].ToString();
            string txtSupName = ds1.Tables[1].Rows[0]["ssirdesc"].ToString();
            string txtBillid = ds1.Tables[1].Rows[0]["billref"].ToString();
            string txtReqno = mrfno1;
            string txtInword = "Taka In Word: " + ASTUtility.Trans((netAmount), 2);
            string txtNarration = ds1.Tables[1].Rows[0]["billnar"].ToString();
            string txtDepo = (security == 0.00) ? "" : ("Security Deposit (" + percntge.ToString("#,##0.00;(#,##0.00);") + ") : " + security.ToString("#,##0.00;(#,##0.00);"));
            string txtAdv = advanced.ToString("#,##0.00;(#,##0.00;)");
            string txtPenalty = penalty.ToString("#,##0.00;(#,##0.00;)");
            string txtDeduc = deduction.ToString("#,##0.00;(#,##0.00;)");
            string txtNetAmt = Convert.ToDouble(netAmount).ToString("#,##0.00;(#,##0.00); ");

            string ftReqIn = ds1.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqdat"].ToString();
            string ftReqApp = ds1.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqadat"].ToString();
            string ftOrder = ds1.Tables[3].Rows[0]["appnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["appdat"].ToString();
            string ftWorkOrd = ds1.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["orddat"].ToString();
            string ftMrRecv = ds1.Tables[3].Rows[0]["mrrnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["mrrdat"].ToString();
            string ftBillConf = ds1.Tables[3].Rows[0]["billnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["billdat"].ToString();

            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptBillConfirmationBridge", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", ds1.Tables[1].Rows[0]["billno2"].ToString() == "" ? "Bill Confirmation Certificate" : "Bill Confirmation Certificate (Adjusted)"));
            rpt.SetParameters(new ReportParameter("txtComment", "( Comments On Enclosed Bills )"));
            rpt.SetParameters(new ReportParameter("txtProjName", txtProjName));
            rpt.SetParameters(new ReportParameter("txtBuildno", txtBuildno));
            rpt.SetParameters(new ReportParameter("txtFloorno", txtFloorno));
            rpt.SetParameters(new ReportParameter("txtFlatno", txtFlatno));
            rpt.SetParameters(new ReportParameter("txtSupName", "Supplier Name :  " + txtSupName));
            rpt.SetParameters(new ReportParameter("txtBillid", txtBillid));
            rpt.SetParameters(new ReportParameter("txtBilldate", CurDate1));
            rpt.SetParameters(new ReportParameter("txtBillno", "Bill No : " + ds1.Tables[1].Rows[0]["billref"].ToString()));
            rpt.SetParameters(new ReportParameter("txtReqno", txtReqno));

            rpt.SetParameters(new ReportParameter("txtDepo", txtDepo));
            rpt.SetParameters(new ReportParameter("txtAdv", txtAdv));
            rpt.SetParameters(new ReportParameter("txtPenalty", txtPenalty));
            rpt.SetParameters(new ReportParameter("txtDeduc", txtDeduc));
            rpt.SetParameters(new ReportParameter("txtNetAmt", txtNetAmt));

            rpt.SetParameters(new ReportParameter("txtInword", txtInword));
            rpt.SetParameters(new ReportParameter("txtNarration", txtNarration));
            rpt.SetParameters(new ReportParameter("ftReqIn", ftReqIn));
            rpt.SetParameters(new ReportParameter("ftReqApp", ftReqApp));
            rpt.SetParameters(new ReportParameter("ftOrder", ftOrder));
            rpt.SetParameters(new ReportParameter("ftWorkOrd", ftWorkOrd));
            rpt.SetParameters(new ReportParameter("ftMrRecv", ftMrRecv));
            rpt.SetParameters(new ReportParameter("ftBillConf", ftBillConf));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            // rpt.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        private void PrintBill05()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            //string mBILLNo = this.ddlPrevBillList.SelectedValue.ToString();
            string mBILLNo = this.Request.QueryString["billno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];

            //For Tk In Word

            DataTable td1 = dt.Copy();
            DataTable td2 = dt.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like'019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            double amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));
            double amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));
            //

            string suppname = "Supplier Name: " + ds1.Tables[1].Rows[0]["ssirdesc"].ToString();
            string inword = "Taka In Word: " + ASTUtility.Trans((amt1 - amt2), 2);
            string mrfno = ds1.Tables[1].Rows[0]["mrfno"].ToString();
            string orderno = ds1.Tables[0].Rows[0]["orderno1"].ToString();
            string refno = ds1.Tables[1].Rows[0]["billref"].ToString();
            string chlno = ds1.Tables[0].Rows[0]["chlnno"].ToString();
            string billno = ds1.Tables[1].Rows[0]["billno1"].ToString();
            string mrrno = ds1.Tables[0].Rows[0]["mrrno1"].ToString();
            string projectName = "Project Name : " + ds1.Tables[0].Rows[0]["pactdesc"].ToString().Substring(4);

            ////Signing Part

            string reqname = ds1.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqdat"].ToString();
            string reqapname = ds1.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqadat"].ToString();
            string ordpro = ds1.Tables[3].Rows[0]["appnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["appdat"].ToString();
            string purchord = ds1.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["orddat"].ToString();
            string recvby = ds1.Tables[3].Rows[0]["mrrnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["mrrdat"].ToString();
            string billname = ds1.Tables[3].Rows[0]["billnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["billdat"].ToString();
            string CurDate1 = "Date: " + Convert.ToDateTime(ds1.Tables[1].Rows[0]["billdat"]).ToString("dd-MMM-yyyy");


            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptBillInfoInns", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Software Generated Bill"));
            rpt.SetParameters(new ReportParameter("supname", suppname));
            rpt.SetParameters(new ReportParameter("txtMrfno", " : " + mrfno));
            rpt.SetParameters(new ReportParameter("txtPono", " : " + orderno));
            rpt.SetParameters(new ReportParameter("txtRefno", " : " + refno));
            rpt.SetParameters(new ReportParameter("txtChalan", " : " + chlno));
            rpt.SetParameters(new ReportParameter("txtBilldate", " : " + CurDate1));
            rpt.SetParameters(new ReportParameter("txtBillno", " : " + billno));
            rpt.SetParameters(new ReportParameter("txtMrrno", " : " + mrrno));
            rpt.SetParameters(new ReportParameter("txtProjectName", projectName));
            rpt.SetParameters(new ReportParameter("txtInword", inword));
            rpt.SetParameters(new ReportParameter("txtNarration", "Narration : " + ds1.Tables[1].Rows[0]["billnar"].ToString()));
            rpt.SetParameters(new ReportParameter("ftReqIn", reqname));
            rpt.SetParameters(new ReportParameter("ftReqapv", reqapname));
            rpt.SetParameters(new ReportParameter("ftOrdpro", ordpro));
            rpt.SetParameters(new ReportParameter("ftPurchord", purchord));
            rpt.SetParameters(new ReportParameter("ftRecvby", recvby));
            rpt.SetParameters(new ReportParameter("ftBillconf", billname));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comlogo", ComLogo));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private string CompanyBillCon()
        {
            string comcod = this.GetCompCode();
            string PrintReq = "";
            switch (comcod)
            {
                case "1101":
                    PrintReq = "PrintBill01";
                    break;

                case "3330":// Bridge
                    PrintReq = "PrintBill02";
                    break;
                //case "3101":
                case "3332": //inster
                    PrintReq = "PrintBill03";
                    break;

                case "3333":// Alliance
                            // case"3101":
                    PrintReq = "PrintBill04";
                    break;



                case "1206"://ACME
                case "1207"://ACME
                case "3338"://ACME
                case "3369"://ACME
                    PrintReq = "PrintBill05";
                    break;

                //case "3336":
                //    this.PrintBillFinalization();
                //    break;

                case "3335":// Edison          
                    PrintReq = "PrintBill06";
                    break;


                case "3336":// Suvastu
                case "3337":
                    PrintReq = "PrintBill07";
                    break;


                // case "3101"://ASIT
                case "2305": //Land
                case "3305":// Housing
                case "3306":// Ratul
                case "3307":
                case "3308":
                case "3309":
                case "3311":// Chittagong
                case "3310":// RCU

                    PrintReq = "PrintBill08";
                    break;



                // case "3101"://ASIT
                case "1108": //Assure
                case "1109":// Assure
                    PrintReq = "PrintBillAssure2";
                    break;

                case "3316": //Assure
                case "3315":// Assure

                    PrintReq = "PrintBill09";
                    break;

                case "3348":
                    PrintReq = "PrintBillCredence";
                    break;

                //case "3101"://ASIT
                case "3353":
                    PrintReq = "PrintBillManama";
                    break;

                case "3354":
                    PrintReq = "PrintBillEdisonErp";
                    break;
                case "3357":
                    PrintReq = "PrintBillIntech";
                    break;
                // bill finaly
                case "3368":
                    PrintReq = "PrintBillFinlay";
                    break;
                // bill epic
               
                case "3367":
                    PrintReq = "PrintBillEpic";
                    break;
                case "3370":
                    PrintReq = "PrintBillCPDL";
                    break;

                default:
                    PrintReq = "PrintBill01";
                    break;
            }

            return PrintReq;

        }

        private void PurConBill_Print()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = new DataSet();
            string lisuno = this.Request.QueryString["lisuno"].ToString();
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURLABISSUEINFO", lisuno, "",
                        pactcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblmatissue"] = ds1.Tables[0];
            ViewState["tblbillinfo"] = ds1.Tables[1];
            switch (comcod)
            {
                //case "2305":
                //case "3305":
                //case "3306":
                //case "3307":
                //case "3308":
                //case "3309":
                //    this.PrintLabIssue02();
                //    break;
                case "1108":
                case "1109":
                case "3315":
                case "3316":
                case "3317":
                    //case "3101":
                    this.PrintLabIssueAssure();
                    break;
                case "3339":// Tropical              
                    this.PrintLabIssueSubCon();
                    break;
                default:
                    this.PrintLabIssue();
                    break;
            }
        }

        private void PurConBillFinal_Print()
        {
            string comcod = this.GetCompCode();

            if (comcod == "3339")
            {
                this.PrintBillFinalization();
            }
            else
            {
                this.AllprintFinalization();
            }




        }
        private void AllprintFinalization()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();

            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string PrintOpt2 = Request.QueryString.AllKeys.Contains("PrintOpt") ? this.Request.QueryString["PrintOpt"].ToString() : "";
            string PrintOpt = PrintOpt2.Length > 0 ? PrintOpt2 : ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();

            // For  Image withdrawn 

            string mBillNo = this.Request.QueryString["billno"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETCBILLINFO", mBillNo, "",
                          "", "", "", "", "", "", "");
            ViewState["tblbill"] = HiddenSameDataConBil(ds1.Tables[0]);
            DataTable dt = (DataTable)ViewState["tblbill"];

            DataTable dtd = ds1.Tables[1];
            ViewState["utbl"] = dtd;

            double toamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(billamt)", "")) ? 0.00 : dt.Compute("Sum(billamt)", "")));

            //if (comcod=="3339" )
            //{
            //    this.PrintBillFinalization();
            //}

            string pCompanyBill = this.CompanyBillCon();

            // signatory part
            // lpostedbyid	lposteddat	lpostuser	lpostdesig	bpostedbyid	bpostuser bpostdesig	bposteddat	baprvbyid	baprvuser	baprvdesig	baprvdat

            DataTable _dtuser = ds1.Tables[2];
            string sign1 = _dtuser.Rows[0]["lpostuser"].ToString() +"\n" + _dtuser.Rows[0]["lpostdesig"].ToString() + "\n" + Convert.ToDateTime(_dtuser.Rows[0]["lposteddat"]).ToString("dd-MMM-yyyy");
            string sign2 = _dtuser.Rows[0]["bpostuser"].ToString() +"\n" + _dtuser.Rows[0]["bpostdesig"].ToString() + "\n" + Convert.ToDateTime(_dtuser.Rows[0]["bposteddat"]).ToString("dd-MMM-yyyy");
            string sign3 = _dtuser.Rows[0]["baprvuser"].ToString() +"\n" + _dtuser.Rows[0]["baprvdesig"].ToString() + "\n" + Convert.ToDateTime(_dtuser.Rows[0]["baprvdat"]).ToString("dd-MMM-yyyy");
            string sign4 = "";



            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>();
            LocalReport rptbill = new LocalReport();

            string IssueRefNo = (dt.Rows[0]["lisurefno"].ToString().Length > 0) ? "Issue Ref No: " + dt.Rows[0]["lisurefno"].ToString() : "Issue Ref No:";
            string billrefno = (dtd.Rows[0]["cbillref"].ToString().Length > 0) ? "Bill Ref. No: " + dtd.Rows[0]["cbillref"].ToString() : "";

            string pactcode = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            string secdep = Convert.ToDouble(dtd.Rows[0]["percntge"]).ToString("#,##0.00;(#,##0.00); ");
            string lblSecurity = "Security Deposit " + "(" + secdep + " %)";

            if (pCompanyBill == "PrintBill02")
            {
                //comcod == "3330" && pactcode == "160100010025"
                //lblSecurity
                if (pactcode == "160100010025" || pactcode == "160100010027")
                {
                    //RptConBillBridgeWithoutLogo
                    rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillBridgeWithoutLogo", lst, null, null);
                    rptbill.SetParameters(new ReportParameter("txtBilType", dtd.Rows[0]["billtype"].ToString()));
                    rptbill.SetParameters(new ReportParameter("lblSecurity", lblSecurity));
                }
                else
                {
                    rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillBridge", lst, null, null);
                    rptbill.EnableExternalImages = true;
                    rptbill.SetParameters(new ReportParameter("txtBilType", dtd.Rows[0]["billtype"].ToString()));
                    rptbill.SetParameters(new ReportParameter("lblSecurity", lblSecurity));
                }



                //rptstk = new RealERPRPT.R_09_PImp.RptConBillBridge();
                //TextObject txtBilType = rptstk.ReportDefinition.ReportObjects["txtBilType"] as TextObject;
                //txtBilType.Text = this.ddlbilltype.SelectedItem.Text;
            }

            //else if (pCompanyBill == "PrintBill03")
            //{
            //    rptstk = new RealERPRPT.R_09_PImp.RptConBillInstar();
            //    TextObject txtBilType = rptstk.ReportDefinition.ReportObjects["txtBilType"] as TextObject;
            //    txtBilType.Text = this.ddlbilltype.SelectedItem.Text;
            //}


            else if (pCompanyBill == "PrintBill04")
            {
                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillAlli", lst, null, null);
                rptbill.EnableExternalImages = true;
                // rptstk = new RealERPRPT.R_09_PImp.RptConBillAlli();

            }

            else if (pCompanyBill == "PrintBill05")
            {

                sign4= "Syed Fatemy Ahmed Roomy" + "\n" + "Major General (Retd.)" + "\n"+ "Chairman & Managing Director" + "\n" + Convert.ToDateTime(_dtuser.Rows[0]["baprvdat"]).ToString("dd-MMM-yyyy");
                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillAcme", lst, null, null);
                rptbill.EnableExternalImages = true;
                rptbill.SetParameters(new ReportParameter("IssueNo", "Issue No: " + dt.Rows[0]["lisuno2"].ToString()));
                rptbill.SetParameters(new ReportParameter("txtpreNam", dtd.Rows[0]["postednam"].ToString()));
                rptbill.SetParameters(new ReportParameter("txtapproveNam", dtd.Rows[0]["aprovnam"].ToString()));
                rptbill.SetParameters(new ReportParameter("IssueRefNo", IssueRefNo));
                rptbill.SetParameters(new ReportParameter("sign1", sign1));
                rptbill.SetParameters(new ReportParameter("sign2", sign2));
                rptbill.SetParameters(new ReportParameter("sign3", sign3));
                rptbill.SetParameters(new ReportParameter("sign4", sign4));
            }

            else if (pCompanyBill == "PrintBill06")
            {

                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillEdi", lst, null, null);
                rptbill.EnableExternalImages = true;

                rptbill.SetParameters(new ReportParameter("IssueNo", "Issue No: " + dt.Rows[0]["lisuno2"].ToString()));
                rptbill.SetParameters(new ReportParameter("IssueRefNo", IssueRefNo));

                //TextObject rptissueno = rptstk.ReportDefinition.ReportObjects["rptissueno"] as TextObject;
                //rptissueno.Text = "Issue No: " + dt.Rows[0]["lisuno2"].ToString();

                //TextObject rptrefno = rptstk.ReportDefinition.ReportObjects["rptrefno"] as TextObject;
                //rptrefno.Text = (dt.Rows[0]["lisurefno"].ToString().Length > 0) ? "Issue Ref No: " + dt.Rows[0]["lisurefno"].ToString() : "Issue Ref No:";


            }

            else if (pCompanyBill == "PrintBill07")
            {


                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillSuvastu", lst, null, null);
                rptbill.EnableExternalImages = true;

                rptbill.SetParameters(new ReportParameter("IssueNo", "Issue No: " + dt.Rows[0]["lisuno2"].ToString()));
                rptbill.SetParameters(new ReportParameter("IssueRefNo", IssueRefNo));

                rptbill.SetParameters(new ReportParameter("txtreward", Convert.ToDouble("0" + dtd.Rows[0]["reward"]).ToString("#,##0.00;#,##0.00")));



            }

            else if (pCompanyBill == "PrintBill08")
            {
                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillRup", lst, null, null);
                rptbill.EnableExternalImages = true;


            }


            else if (pCompanyBill == "PrintBill09")
            {
                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillAssure", lst, null, null);
                rptbill.EnableExternalImages = true;


            }

            else if (pCompanyBill == "PrintBillAssure2")
            {
                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillAssure02", lst, null, null);
                rptbill.EnableExternalImages = true;


            }


            else if (pCompanyBill == "PrintBillCredence")
            {

                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillCredence", lst, null, null);
                rptbill.EnableExternalImages = true;
                rptbill.SetParameters(new ReportParameter("IssueNo", "Issue No: " + dt.Rows[0]["lisuno2"].ToString()));
                rptbill.SetParameters(new ReportParameter("IssueRefNo", IssueRefNo));
            }

            else if (pCompanyBill == "PrintBillManama")
            {
                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillManama", lst, null, null);
                rptbill.EnableExternalImages = true;
                rptbill.SetParameters(new ReportParameter("IssueNo", "Issue No: " + dt.Rows[0]["lisuno2"].ToString()));
                rptbill.SetParameters(new ReportParameter("IssueRefNo", IssueRefNo));
                double netamt2 = toamt + Convert.ToDouble("0" + dtd.Rows[0]["reward"]) - Convert.ToDouble("0" + dtd.Rows[0]["sdamt"]) - Convert.ToDouble("0" + dtd.Rows[0]["dedamt"]) - Convert.ToDouble("0" + dtd.Rows[0]["penamt"]) - Convert.ToDouble("0" + dtd.Rows[0]["advamt"]);
                rptbill.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(netamt2), 2)));
            }


            else if (pCompanyBill == "PrintBillEdisonErp")
            {


                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillEdisonErp", lst, null, null);
                rptbill.EnableExternalImages = true;
                rptbill.SetParameters(new ReportParameter("IssueNo", "Issue No: " + dt.Rows[0]["lisuno2"].ToString()));
                rptbill.SetParameters(new ReportParameter("IssueRefNo", IssueRefNo));


            }

            else if (pCompanyBill == "PrintBillIntech")
            {
                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillIntech", lst, null, null);
                rptbill.EnableExternalImages = true;
                rptbill.SetParameters(new ReportParameter("IssueNo", "Issue No: " + dt.Rows[0]["lisuno2"].ToString()));
                rptbill.SetParameters(new ReportParameter("IssueRefNo", IssueRefNo));
            }

            else if (pCompanyBill == "PrintBillFinlay")
            {

                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillFinaly", lst, null, null);
                rptbill.EnableExternalImages = true;
                rptbill.SetParameters(new ReportParameter("IssueNo", "Issue No: " + dt.Rows[0]["lisuno2"].ToString()));
                rptbill.SetParameters(new ReportParameter("IssueRefNo", IssueRefNo));

            }
            else if(pCompanyBill== "PrintBillEpic")
            {
                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillEpic", lst, null, null);
                rptbill.EnableExternalImages = true;
                rptbill.SetParameters(new ReportParameter("IssueNo", "Issue No: " + dt.Rows[0]["lisuno2"].ToString()));
                rptbill.SetParameters(new ReportParameter("IssueRefNo", IssueRefNo));

            }
            else if(pCompanyBill== "PrintBillCPDL")
            {
                 
                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillCPDL", lst, null, null);

                rptbill.EnableExternalImages = true;
                rptbill.SetParameters(new ReportParameter("IssueNo", "Issue No: " + dt.Rows[0]["lisuno2"].ToString()));
                rptbill.SetParameters(new ReportParameter("IssueRefNo", IssueRefNo));

                if (_dtuser.Rows[0]["lpostuser"].ToString() == "") {
                    sign1 = "";
                }
                if (_dtuser.Rows[0]["bpostuser"].ToString() == "")
                {
                    sign2 = "";
                }
                if (_dtuser.Rows[0]["baprvuser"].ToString() == "")
                {
                    sign3 = "";
                }
                rptbill.SetParameters(new ReportParameter("sign1", sign1));
                rptbill.SetParameters(new ReportParameter("sign2", sign2));
                rptbill.SetParameters(new ReportParameter("sign3", sign3));
                rptbill.SetParameters(new ReportParameter("sign4", sign4));


            }
            else
            {
                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBill", lst, null, null);
                rptbill.EnableExternalImages = true;
                rptbill.SetParameters(new ReportParameter("IssueNo", "Issue No: " + dt.Rows[0]["lisuno2"].ToString()));
                rptbill.SetParameters(new ReportParameter("IssueRefNo", IssueRefNo));

                //rptstk = new RealERPRPT.R_09_PImp.RptConBill();

                //TextObject rptissueno = rptstk.ReportDefinition.ReportObjects["rptissueno"] as TextObject;
                //rptissueno.Text = "Issue No: " + dt.Rows[0]["lisuno2"].ToString();

                //TextObject rptrefno = rptstk.ReportDefinition.ReportObjects["rptrefno"] as TextObject;
                //rptrefno.Text = (dt.Rows[0]["lisurefno"].ToString().Length > 0) ? "Issue Ref No: " + dt.Rows[0]["lisurefno"].ToString() : "Issue Ref No:";

            }


            // var TAmt = lst.Select(p => p.billamt).Sum();
            double netamt = toamt + Convert.ToDouble("0" + dtd.Rows[0]["reward"]) - Convert.ToDouble("0" + dtd.Rows[0]["sdamt"]) - Convert.ToDouble("0" + dtd.Rows[0]["dedamt"]) - Convert.ToDouble("0" + dtd.Rows[0]["penamt"]) - Convert.ToDouble("0" + dtd.Rows[0]["advamt"]);
            //double TAmt = Convert.ToDouble("0" + ((Label)this.gvSubBill.FooterRow.FindControl("lgvFBillAmt")).Text) - Convert.ToDouble("0" + this.txtSDAmount.Text.Trim()) - Convert.ToDouble("0" + this.txtDedAmount.Text.Trim()) - Convert.ToDouble("0" + this.txtPenaltyAmount.Text.Trim()) - Convert.ToDouble("0" + this.txtAdvanced.Text.Trim());


            if (comcod == "3330" && (pactcode == "160100010025" || pactcode == "160100010027"))
            {

            }
            else
            {
                rptbill.SetParameters(new ReportParameter("compname", comnam));
                rptbill.SetParameters(new ReportParameter("ComLogo", ComLogo));
            }


            rptbill.SetParameters(new ReportParameter("comadd", comadd));
            rptbill.SetParameters(new ReportParameter("Rptname", "Sub-Contractor Bill"));
            rptbill.SetParameters(new ReportParameter("ProjectName", "Project Name : " + dt.Rows[0]["pactdesc"].ToString()));
            rptbill.SetParameters(new ReportParameter("SubContNam", "Contractor Name : " + dt.Rows[0]["csirdesc"].ToString()));
            rptbill.SetParameters(new ReportParameter("mBillNo", "Bill No: " + dtd.Rows[0]["billno1"].ToString()));
            rptbill.SetParameters(new ReportParameter("Date", "Date: " + Convert.ToDateTime(dtd.Rows[0]["billdate"]).ToString("dd-MMM-yyyy")));
            rptbill.SetParameters(new ReportParameter("SeDep", Convert.ToDouble(dtd.Rows[0]["sdamt"]).ToString("#,##0.00;(#,##0.00); ")));
            rptbill.SetParameters(new ReportParameter("DedAmt", Convert.ToDouble(dtd.Rows[0]["dedamt"]).ToString("#,##0.00;(#,##0.00); ")));
            rptbill.SetParameters(new ReportParameter("PenaltyAmt", Convert.ToDouble(dtd.Rows[0]["penamt"]).ToString("#,##0.00;(#,##0.00); ")));
            rptbill.SetParameters(new ReportParameter("Advanced", Convert.ToDouble(dtd.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); ")));
            rptbill.SetParameters(new ReportParameter("TotalAmt", Math.Round(netamt, 0).ToString("#,##0.00;(#,##0.00); ")));
            rptbill.SetParameters(new ReportParameter("BillRef", billrefno));
            rptbill.SetParameters(new ReportParameter("naration", dtd.Rows[0]["rmrks"].ToString()));
            rptbill.SetParameters(new ReportParameter("printFooter", txtuserinfo));

            //  Rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(TAmt), 2)));
            Session["Report1"] = rptbill;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" + PrintOpt + "', target='_self');</script>";







            //if (pCompanyBill == "PrintBill02")
            //{
            //    rptstk = new RealERPRPT.R_09_PImp.RptConBillBridge();
            //    TextObject txtBilType = rptstk.ReportDefinition.ReportObjects["txtBilType"] as TextObject;
            //    txtBilType.Text = dtd.Rows[0]["billtype"].ToString();
            //}


            //else if (pCompanyBill == "PrintBill03")
            //{
            //    rptstk = new RealERPRPT.R_09_PImp.RptConBillInstar();
            //    TextObject txtBilType = rptstk.ReportDefinition.ReportObjects["txtBilType"] as TextObject;
            //    txtBilType.Text = dtd.Rows[0]["billtype"].ToString();
            //}


            //else if (pCompanyBill == "PrintBill04")
            //{
            //    rptstk = new RealERPRPT.R_09_PImp.RptConBillAlli();

            //}

            //else if (pCompanyBill == "PrintBill05")
            //{
            //    rptstk = new RealERPRPT.R_09_PImp.RptConBillAcme();

            //    //  ViewState["utbl"] = dtd;

            //    DataTable dtuser = (DataTable)ViewState["utbl"];
            //    string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postednam"].ToString();
            //    string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprovnam"].ToString();

            //    TextObject rptissueno = rptstk.ReportDefinition.ReportObjects["rptissueno"] as TextObject;
            //    rptissueno.Text = "Issue No: " + dt.Rows[0]["lisuno2"].ToString();

            //    TextObject txtpreNam = rptstk.ReportDefinition.ReportObjects["txtpreNam"] as TextObject;
            //    txtpreNam.Text = tblPostedByid; // dtuser.Rows[0]["postednam"].ToString();

            //    TextObject txtapproveNam = rptstk.ReportDefinition.ReportObjects["txtapproveNam"] as TextObject;
            //    txtapproveNam.Text = tblPostedtrmid; // dtuser.Rows[0]["aprovnam"].ToString();

            //    TextObject rptrefno = rptstk.ReportDefinition.ReportObjects["rptrefno"] as TextObject;
            //    rptrefno.Text = (dt.Rows[0]["lisurefno"].ToString().Length > 0) ? "Issue Ref No: " + dt.Rows[0]["lisurefno"].ToString() : "Issue Ref No:";
            //}

            //else if (pCompanyBill == "PrintBill06")
            //{
            //    rptstk = new RealERPRPT.R_09_PImp.RptConBillEdi();

            //    TextObject rptissueno = rptstk.ReportDefinition.ReportObjects["rptissueno"] as TextObject;
            //    rptissueno.Text = "Issue No: " + dt.Rows[0]["lisuno2"].ToString();

            //    TextObject rptrefno = rptstk.ReportDefinition.ReportObjects["rptrefno"] as TextObject;
            //    rptrefno.Text = (dt.Rows[0]["lisurefno"].ToString().Length > 0) ? "Issue Ref No: " + dt.Rows[0]["lisurefno"].ToString() : "Issue Ref No:";


            //}


            //else if (pCompanyBill == "PrintBill07")
            //{
            //    rptstk = new RealERPRPT.R_09_PImp.RptConBillSuvastu();

            //    TextObject rptissueno = rptstk.ReportDefinition.ReportObjects["rptissueno"] as TextObject;
            //    rptissueno.Text = "Issue No: " + dt.Rows[0]["lisuno2"].ToString();

            //    TextObject rptrefno = rptstk.ReportDefinition.ReportObjects["rptrefno"] as TextObject;
            //    rptrefno.Text = (dt.Rows[0]["lisurefno"].ToString().Length > 0) ? "Issue Ref No: " + dt.Rows[0]["lisurefno"].ToString() : "Issue Ref No:";
            //    TextObject txtreward = rptstk.ReportDefinition.ReportObjects["txtreward"] as TextObject;
            //    txtreward.Text = Convert.ToDouble("0" + dtd.Rows[0]["reward"]).ToString("#,##0.00;#,##0.00"); ;


            //}

            //else
            //{
            //    rptstk = new RealERPRPT.R_09_PImp.RptConBill();

            //    TextObject rptissueno = rptstk.ReportDefinition.ReportObjects["rptissueno"] as TextObject;
            //    rptissueno.Text = "Issue No: " + dt.Rows[0]["lisuno2"].ToString();

            //    TextObject rptrefno = rptstk.ReportDefinition.ReportObjects["rptrefno"] as TextObject;
            //    rptrefno.Text = (dt.Rows[0]["lisurefno"].ToString().Length > 0) ? "Issue Ref No: " + dt.Rows[0]["lisurefno"].ToString() : "Issue Ref No:";


            //}

            //TextObject rpttxtComName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rpttxtComName.Text = comnam;
            //TextObject txtProjectName = rptstk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //txtProjectName.Text = dt.Rows[0]["pactdesc"].ToString();
            //TextObject txtContractor = rptstk.ReportDefinition.ReportObjects["txtContractor"] as TextObject;
            //txtContractor.Text = "Contractor Name : " + dt.Rows[0]["csirdesc"].ToString();
            //TextObject rpttxtbillno = rptstk.ReportDefinition.ReportObjects["billno"] as TextObject;
            //rpttxtbillno.Text = "Bill No: " + dtd.Rows[0]["billno1"].ToString();
            //TextObject rpttxtbillRefno = rptstk.ReportDefinition.ReportObjects["billRefno"] as TextObject;
            //rpttxtbillRefno.Text = (dtd.Rows[0]["cbillref"].ToString().Length > 0) ? "Bill Ref. No: " + dtd.Rows[0]["cbillref"].ToString() : "";

            //double seqpercnt = Convert.ToDouble("0" + dtd.Rows[0]["percntge"].ToString().Replace("%", ""));
            //TextObject txtsecurity = rptstk.ReportDefinition.ReportObjects["txtsecurity"] as TextObject;
            //txtsecurity.Text = (seqpercnt > 0 ? ("Security Deposit (" + dtd.Rows[0]["percntge"].ToString() + ")") : "Security Deposit");



            //TextObject rpttxtSequirityAmt = rptstk.ReportDefinition.ReportObjects["txtSecurityAmt"] as TextObject;
            //rpttxtSequirityAmt.Text = Convert.ToDouble(dtd.Rows[0]["sdamt"]).ToString("#,##0.00;#,##0.00");

            //TextObject textdedution = rptstk.ReportDefinition.ReportObjects["textdedution"] as TextObject;
            //textdedution.Text = Convert.ToDouble(dtd.Rows[0]["dedamt"]).ToString("#,##0.00;#,##0.00");
            //TextObject txtpanalty = rptstk.ReportDefinition.ReportObjects["txtpanalty"] as TextObject;
            //txtpanalty.Text = Convert.ToDouble(dtd.Rows[0]["penamt"]).ToString("#,##0.00;#,##0.00");
            //TextObject txtAdvanced = rptstk.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
            //txtAdvanced.Text = Convert.ToDouble(dtd.Rows[0]["dedamt"]).ToString("#,##0.00;#,##0.00");



            //double netamt = toamt + Convert.ToDouble("0" + dtd.Rows[0]["reward"]) - Convert.ToDouble("0" + dtd.Rows[0]["sdamt"]) - Convert.ToDouble("0" + dtd.Rows[0]["dedamt"]) - Convert.ToDouble("0" + dtd.Rows[0]["penamt"]) - Convert.ToDouble("0" + dtd.Rows[0]["advamt"]);
            //TextObject rpttxttoamtafSecurity = rptstk.ReportDefinition.ReportObjects["txttoamtafSecurity"] as TextObject;
            //rpttxttoamtafSecurity.Text = Math.Round(netamt, 0).ToString("#,##0.00;(#,##0.00); ");
            //TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //rpttxtdate.Text = "Date: " + Convert.ToDateTime(dtd.Rows[0]["billdate"]).ToString("dd-MMM-yyyy"); ;


            //TextObject txtremarks = rptstk.ReportDefinition.ReportObjects["txtremarks"] as TextObject;
            //txtremarks.Text = dtd.Rows[0]["rmrks"].ToString();

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)ViewState["tblbill"]);


            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "SUB-CONTRACTOR BILL";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "Bill No: " + dtd.Rows[0]["billno1"].ToString();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}


            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);


            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";







        }

        private void PrintBillFinalization()
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
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string mBillNo = this.Request.QueryString["billno"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETCBILLINFO", mBillNo, "",
                         "", "", "", "", "", "", "");
            ViewState["tblbill"] = HiddenSameDataConBil(ds1.Tables[0]);
            DataTable dt = (DataTable)ViewState["tblbill"];

            DataTable dtd = ds1.Tables[1];

            double toamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(billamt)", "")) ? 0.00 : dt.Compute("Sum(billamt)", "")));


            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>();
            // var TAmt = lst.Select(p => p.billamt).Sum();
            double TAmt = toamt + Convert.ToDouble("0" + dtd.Rows[0]["reward"]) - Convert.ToDouble("0" + dtd.Rows[0]["sdamt"]) - Convert.ToDouble("0" + dtd.Rows[0]["dedamt"]) - Convert.ToDouble("0" + dtd.Rows[0]["penamt"]) - Convert.ToDouble("0" + dtd.Rows[0]["advamt"]);
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptSubConBillFinalization", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Rptname", "Sub-Contractor Bill"));
            Rpt1.SetParameters(new ReportParameter("ProjectName", dt.Rows[0]["pactdesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("SubContNam", "Contractor Name: " + dt.Rows[0]["csirdesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("IssueNo", "Issue No: " + dt.Rows[0]["lisuno2"].ToString()));
            Rpt1.SetParameters(new ReportParameter("IssueRefNo", "Issue Ref No: " + dt.Rows[0]["lisurefno"].ToString()));
            Rpt1.SetParameters(new ReportParameter("mBillNo", "Bill No: " + mBillNo));
            Rpt1.SetParameters(new ReportParameter("Date", "Date: " + Convert.ToDateTime(dtd.Rows[0]["billdate"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("SeDep", Convert.ToDouble("0" + dtd.Rows[0]["sdamt"]).ToString("#,##0.00;(#,##0.00); ")));
            Rpt1.SetParameters(new ReportParameter("DedAmt", Convert.ToDouble("0" + dtd.Rows[0]["dedamt"]).ToString("#,##0.00;(#,##0.00); ")));
            Rpt1.SetParameters(new ReportParameter("PenaltyAmt", Convert.ToDouble("0" + dtd.Rows[0]["penamt"]).ToString("#,##0.00;(#,##0.00); ")));
            Rpt1.SetParameters(new ReportParameter("Advanced", Convert.ToDouble("0" + dtd.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); ")));
            Rpt1.SetParameters(new ReportParameter("TotalAmt", Math.Round(TAmt, 0).ToString("#,##0.00;(#,##0.00); ")));
            Rpt1.SetParameters(new ReportParameter("BillRef", "Bill Ref.No: " + dtd.Rows[0]["cbillref"].ToString()));
            Rpt1.SetParameters(new ReportParameter("naration", dtd.Rows[0]["rmrks"].ToString()));
            Rpt1.SetParameters(new ReportParameter("printFooter", txtuserinfo));
            //  Rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(TAmt), 2)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }

        //private void PrintLabIssue02()
        //{

        //    DataTable dt1 = (DataTable)ViewState["tblbillinfo"];

        //    string refno = dt1.Rows[0]["lisurefno"].ToString();
        //    string pactdesc = dt1.Rows[0]["pactdesc"].ToString();
        //    string csirdsec = dt1.Rows[0]["csirdesc"].ToString();
        //    string lisuno1 = dt1.Rows[0]["lisuno1"].ToString();
        //    string isudat = Convert.ToDateTime(dt1.Rows[0]["isudat"]).ToString("dd-MMM-yyyy"); ;



        //    DataTable dt = (DataTable)ViewState["tblmatissue"];
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    ReportDocument rptstk = new RealERPRPT.R_09_PImp.rptLabIssueRup();

        //    TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
        //    txtCompany.Text = comnam;
        //    //txtSubConNam;
        //    TextObject rptProjectName = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
        //    rptProjectName.Text = "Project Name: " + pactdesc;
        //    TextObject rptSunConName = rptstk.ReportDefinition.ReportObjects["txtSubConNam"] as TextObject;
        //    rptSunConName.Text = "Sub Contractor Name: " + csirdsec;
        //    TextObject rpttxtissueno = rptstk.ReportDefinition.ReportObjects["Issueno"] as TextObject;
        //    rpttxtissueno.Text = lisuno1;
        //    TextObject rpttxtrefno = rptstk.ReportDefinition.ReportObjects["txtRefno"] as TextObject;
        //    rpttxtrefno.Text = (refno.Length > 0) ? refno : "";

        //    TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
        //    rpttxtdate.Text = isudat;


        //    TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rptstk.SetDataSource(dt);
        //    Session["Report1"] = rptstk;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_Self');</script>";

        //}

        private void PrintLabIssueAssure()
        {


            DataTable dt1 = (DataTable)ViewState["tblbillinfo"];

            string refno = dt1.Rows[0]["lisurefno"].ToString();
            string pactdesc = dt1.Rows[0]["pactdesc"].ToString();
            string csirdsec = dt1.Rows[0]["csirdesc"].ToString();
            string lisuno1 = dt1.Rows[0]["lisuno1"].ToString();
            string isudat = Convert.ToDateTime(dt1.Rows[0]["isudat"]).ToString("dd-MMM-yyyy");
            string rano = dt1.Rows[0]["rano"].ToString();
            string rmrks = dt1.Rows[0]["rmrks"].ToString();

            DataTable dt = (DataTable)ViewState["tblmatissue"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.SubConBill.ConRaBill>();
            var TAmt = lst.Select(p => p.isuamt).Sum();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.rptLabIssueAssure", lst, null, null);




            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Sub Contractor Bill (R/A Wise)"));
            Rpt1.SetParameters(new ReportParameter("ProjectName", "Project Name: " + pactdesc));
            Rpt1.SetParameters(new ReportParameter("txtSubConNam", "Sub Contractor Name: " + csirdsec));
            Rpt1.SetParameters(new ReportParameter("txttrade", "Trade Name: " + rano));
            Rpt1.SetParameters(new ReportParameter("Issueno", "Issue No: " + lisuno1));
            Rpt1.SetParameters(new ReportParameter("txtRefno", "Bill Ref. No: " + refno));
            Rpt1.SetParameters(new ReportParameter("date", "Date: " + isudat));
            Rpt1.SetParameters(new ReportParameter("narrationname", rmrks));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";



            //ReportDocument rptstk = new RealERPRPT.R_09_PImp.rptLabIssueAssure();

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            ////txtSubConNam;
            //TextObject rptProjectName = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //rptProjectName.Text = "Project Name: " + pactdesc;
            //TextObject rptSunConName = rptstk.ReportDefinition.ReportObjects["txtSubConNam"] as TextObject;
            //rptSunConName.Text = "Sub Contractor Name: " + csirdsec;

            //TextObject txttrade = rptstk.ReportDefinition.ReportObjects["txttrade"] as TextObject;
            //txttrade.Text = "Trade Name: " + rano;

            //TextObject rpttxtissueno = rptstk.ReportDefinition.ReportObjects["Issueno"] as TextObject;
            //rpttxtissueno.Text = lisuno1;
            //TextObject rpttxtrefno = rptstk.ReportDefinition.ReportObjects["txtRefno"] as TextObject;
            //rpttxtrefno.Text = refno;

            //TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //rpttxtdate.Text = isudat;


            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_Self');</script>";


        }
        private void PrintLabIssueSubCon()
        {
            DataTable dt1 = (DataTable)ViewState["tblbillinfo"];

            string refno = dt1.Rows[0]["lisurefno"].ToString();
            string pactdesc = dt1.Rows[0]["pactdesc"].ToString();
            string csirdsec = dt1.Rows[0]["csirdesc"].ToString();
            string lisuno1 = dt1.Rows[0]["lisuno1"].ToString();
            string isudat = Convert.ToDateTime(dt1.Rows[0]["isudat"]).ToString("dd-MMM-yyyy");
            string rano = dt1.Rows[0]["rano"].ToString();
            string rmrks = dt1.Rows[0]["rmrks"].ToString();


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)ViewState["tblmatissue"];
            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.EClassOrder.Labissue>();
            var TAmt = lst.Select(p => p.isuamt).Sum();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptLabIssueSubCon", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Rptname", "Sub Contractor Bill (R/A Wise)"));
            Rpt1.SetParameters(new ReportParameter("ProjectName", "Project Name: " + pactdesc));
            Rpt1.SetParameters(new ReportParameter("SubContNam", "Sub Contractor Name: " + csirdsec));
            Rpt1.SetParameters(new ReportParameter("IssueNo", "Issue No: " + lisuno1));
            Rpt1.SetParameters(new ReportParameter("BillRef", "Bill Ref. No: " + refno));
            Rpt1.SetParameters(new ReportParameter("Date", "Date: " + isudat));
            Rpt1.SetParameters(new ReportParameter("naration", "Naration: " + rmrks));
            Rpt1.SetParameters(new ReportParameter("printFooter", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(TAmt), 2)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }
        private void PrintLabIssue()
        {
            DataTable dt1 = (DataTable)ViewState["tblbillinfo"];
            string refno = dt1.Rows[0]["lisurefno"].ToString();
            string pactdesc = dt1.Rows[0]["pactdesc"].ToString();
            string csirdsec = dt1.Rows[0]["csirdesc"].ToString();
            string lisuno1 = dt1.Rows[0]["lisuno1"].ToString();
            string isudat = Convert.ToDateTime(dt1.Rows[0]["isudat"]).ToString("dd-MMM-yyyy");
            string rano = dt1.Rows[0]["rano"].ToString();
            string rmrks = dt1.Rows[0]["rmrks"].ToString();

            DataTable dt = (DataTable)ViewState["tblmatissue"];

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.SubConBill.ConRaBill>();
            var TAmt = lst.Select(p => p.isuamt).Sum();


            switch (comcod)
            {

                case "1206":
                case "1207":
                case "3338":
                case "3369":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.rptLabIssueAcme", lst, null, null);

                    break;


                case "3336":
                case "3337":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.rptLabIssueSuvastu", lst, null, null);
                    break;


                case "3340":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.rptLabIssueUrban", lst, null, null);
                    break;
                //case "3348":
                //    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_83_Att.DailyAttendenceCHL", lst, null, null);
                //    break;

                case "2305"://Rupayan
                case "3305":
                case "3306":
                case "3307":
                case "3308":
                case "3309":
                case "3310":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptLabIssueRup", lst, null, null);
                    // rptstk = new RealERPRPT.R_09_PImp.rptLabIssue();
                    break;
                default:
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptLabIssue", lst, null, null);
                    // rptstk = new RealERPRPT.R_09_PImp.rptLabIssue();
                    break;



            }


            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Sub Contractor Bill (R/A Wise)"));
            Rpt1.SetParameters(new ReportParameter("ProjectName", "Project Name: " + pactdesc));
            Rpt1.SetParameters(new ReportParameter("txtSubConNam", "Sub Contractor Name: " + csirdsec));
            Rpt1.SetParameters(new ReportParameter("Issueno", "Issue No: " + lisuno1));
            Rpt1.SetParameters(new ReportParameter("txtRefno", "Bill Ref. No: " + refno));
            Rpt1.SetParameters(new ReportParameter("date", "Date: " + isudat));
            Rpt1.SetParameters(new ReportParameter("narrationname", rmrks));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";







            //DataTable dt1 = (DataTable)ViewState["tblbillinfo"];

            //string refno = dt1.Rows[0]["lisurefno"].ToString();
            //string pactdesc = dt1.Rows[0]["pactdesc"].ToString();
            //string csirdsec = dt1.Rows[0]["csirdesc"].ToString();
            //string lisuno1 = dt1.Rows[0]["lisuno1"].ToString();
            //string isudat = Convert.ToDateTime(dt1.Rows[0]["isudat"]).ToString("dd-MMM-yyyy");
            //string rano = dt1.Rows[0]["rano"].ToString();
            //string rmrks = dt1.Rows[0]["rmrks"].ToString();



            //DataTable dt = (DataTable)ViewState["tblmatissue"];

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RealERPRPT.R_09_PImp.rptLabIssue();

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            ////txtSubConNam;
            //TextObject rptProjectName = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //rptProjectName.Text = "Project Name: " + pactdesc;
            //TextObject rptSunConName = rptstk.ReportDefinition.ReportObjects["txtSubConNam"] as TextObject;
            //rptSunConName.Text = "Sub Contractor Name: " + csirdsec;
            //TextObject rpttxtissueno = rptstk.ReportDefinition.ReportObjects["Issueno"] as TextObject;
            //rpttxtissueno.Text = "Issue No: " + lisuno1;
            //TextObject rpttxtrefno = rptstk.ReportDefinition.ReportObjects["txtRefno"] as TextObject;
            //rpttxtrefno.Text = (refno.Length > 0) ? "Bill Ref. No: " + refno : "";

            //TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //rpttxtdate.Text = "Date: " + isudat;

            //TextObject narrationname = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //narrationname.Text = rmrks;
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_Self');</script>";


        }
        private DataTable HiddenSameDataConBil(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string lisuno = dt1.Rows[0]["lisuno"].ToString();
            //   string lisurefno = dt1.Rows[0]["lisurefno"].ToString();
            string grp = dt1.Rows[0]["grp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["lisuno"].ToString() == lisuno && dt1.Rows[j]["grp"].ToString() == grp)
                {


                    dt1.Rows[j]["lisuno2"] = "";
                    dt1.Rows[j]["lisurefno"] = "";
                    dt1.Rows[j]["grpdesc"] = "";

                }
                else
                {

                    if (dt1.Rows[j]["lisuno"].ToString() == lisuno)
                    {
                        dt1.Rows[j]["lisuno2"] = "";
                        dt1.Rows[j]["lisurefno"] = "";

                    }
                    if (dt1.Rows[j]["grp"].ToString() == grp)
                    {
                        dt1.Rows[j]["grpdesc"] = "";

                    }
                }


                lisuno = dt1.Rows[j]["lisuno"].ToString();
                grp = dt1.Rows[j]["grp"].ToString();



            }

            return dt1;
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            DataView dv = dt1.DefaultView;
            dv.Sort = "grp1, rsircode";
            dt1 = dv.ToTable();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["rsirdesc1"] = "";
                }

                else
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
            }

            return dt1;
        }
        private DataTable HiddenSameDataMRRReceipt(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string reqno = dt1.Rows[0]["reqno"].ToString();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["reqno"].ToString() == reqno && dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    reqno = dt1.Rows[j]["reqno"].ToString();
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["reqno1"] = "";
                    dt1.Rows[j]["rsirdesc1"] = "";
                }

                else
                {
                    if (dt1.Rows[j]["reqno"].ToString() == reqno)
                        dt1.Rows[j]["reqno1"] = "";

                    if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                        dt1.Rows[j]["rsirdesc1"] = "";
                    reqno = dt1.Rows[j]["reqno"].ToString();
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                }

            }

            return dt1;
        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }
        private DataTable HiddenSameDataMktMRR(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string reqno = dt1.Rows[0]["reqno"].ToString();
            string prtype = dt1.Rows[0]["prtype"].ToString();
            string acttype = dt1.Rows[0]["acttype"].ToString();
            string mkttype = dt1.Rows[0]["mkttype"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["reqno"].ToString() == reqno && dt1.Rows[j]["prtype"].ToString() == prtype && dt1.Rows[j]["acttype"].ToString() == acttype && dt1.Rows[j]["mkttype"].ToString() == mkttype)
                {
                    dt1.Rows[j]["reqno1"] = "";
                    dt1.Rows[j]["prtypedesc"] = "";
                    dt1.Rows[j]["acttypedesc"] = "";
                    dt1.Rows[j]["mkttypedesc"] = "";

                    reqno = dt1.Rows[j]["reqno"].ToString();
                    prtype = dt1.Rows[j]["prtype"].ToString();
                    acttype = dt1.Rows[j]["acttype"].ToString();
                    mkttype = dt1.Rows[j]["mkttype"].ToString();
                }

                else if (dt1.Rows[j]["reqno"].ToString() == reqno && dt1.Rows[j]["prtype"].ToString() == prtype && dt1.Rows[j]["acttype"].ToString() == acttype)
                {
                    dt1.Rows[j]["reqno1"] = "";
                    dt1.Rows[j]["prtypedesc"] = "";
                    dt1.Rows[j]["acttypedesc"] = "";

                    reqno = dt1.Rows[j]["reqno"].ToString();
                    prtype = dt1.Rows[j]["prtype"].ToString();
                    acttype = dt1.Rows[j]["acttype"].ToString();
                }
                else if (dt1.Rows[j]["reqno"].ToString() == reqno && dt1.Rows[j]["prtype"].ToString() == prtype)
                {
                    dt1.Rows[j]["reqno1"] = "";
                    dt1.Rows[j]["prtypedesc"] = "";

                    reqno = dt1.Rows[j]["reqno"].ToString();
                    prtype = dt1.Rows[j]["prtype"].ToString();
                }

                else
                {
                    if (dt1.Rows[j]["reqno"].ToString() == reqno)
                        dt1.Rows[j]["reqno1"] = "";

                    if (dt1.Rows[j]["prtype"].ToString() == prtype)
                        dt1.Rows[j]["prtypedesc"] = "";

                    if (dt1.Rows[j]["acttype"].ToString() == acttype)
                        dt1.Rows[j]["acttypedesc"] = "";

                    if (dt1.Rows[j]["mkttype"].ToString() == mkttype)
                        dt1.Rows[j]["mkttypedesc"] = "";

                    reqno = dt1.Rows[j]["reqno"].ToString();
                    prtype = dt1.Rows[j]["prtype"].ToString();
                    acttype = dt1.Rows[j]["acttype"].ToString();
                    mkttype = dt1.Rows[j]["mkttype"].ToString();
                }

            }

            return dt1;
        }
    }
}