using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Diagnostics;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.IO;
using System.Web.Mail;
using RealERPLIB;
using RealERPRPT;
using System.Net;
using System.Drawing;
using AjaxControlToolkit;
using RealEntity;
using RealERPRDLC;
using Microsoft.Reporting.WinForms;
using System.Net.Mail;

namespace RealERPWEB.F_14_Pro
{
    public partial class PurWrkOrderEntry : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        UserManPurchase objUserMan = new UserManPurchase();
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();
        private Hashtable _errObj;

        public static string Url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string title = (Request.QueryString["InputType"].ToString() == "OrderEntry") ? "Purchase Order"
                   : (Request.QueryString["InputType"].ToString() == "FirstApp") ? "Purchase Order 1st Approval"
                   : (Request.QueryString["InputType"].ToString() == "SecondApp") ? "Purchase Order Final Approval"
                   : "Purchase Order";

                ((Label)this.Master.FindControl("lblTitle")).Text = title;
                this.Master.Page.Title = title;

                this.txtCurOrderDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtApprovalDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtLETDES.Text = comnam + this.CompanySubject();
                // this.lbtnPrevOrderList_Click(null, null);
                this.txtCurOrderDate_CalendarExtender.EndDate = System.DateTime.Today;
                this.SendMail();
                //only current date
                // this.CurDate();
                if (Session["tblordrange"] == null)
                {
                    switch (comcod)
                    {
                        case "3354": //Edison Real Estate
                        case "3335": //Edison
                        //case "3101": // ptl
                        case "3355": // greenwood
                        case "3368": // finlay
                            this.GetOrderRange();
                            this.lnkSendEmail.Visible = false;
                            break;

                        default:
                            break;
                    }
                }
                string ordero = this.Request.QueryString["genno"].ToString().Trim();
                if (ordero.Length > 0)
                {
                    if (ordero.Substring(0, 3) == "POR")
                    {
                        this.lbtnPrevOrderList_Click(null, null);
                        this.lbtnOk_Click(null, null);
                    }
                }
            }
        }

        //private void CurDate()
        //{
        //    string comcod = this.GetCompCode();
        //    string type = this.Request.QueryString["InputType"].ToString().Trim();
        //    if ((comcod == "3339") && type== "OrderEntry")
        //    {
        //        this.txtCurOrderDate_CalendarExtender.StartDate = System.DateTime.Today;
        //    }
        //}
        private void SendMail()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3335": //Edison
                             //case "3101":
                    this.lnkSendEmail.Visible = false;
                    break;
                case "3368": // finlay                             
                case "3367": // finlay                             
                case "3354": // finlay                             
                    this.lnkSendEmail.Visible = true;
                    break;
                default:
                    break;
            }
        }
        private void GetOrderRange()
        {
            Session.Remove("tblordrange");
            string comcod = this.GetCompCode();
            List<RealEntity.C_14_Pro.EClassPur.EClassOrderRange> lst = objUserMan.GetOrderRange(comcod);
            Session["tblordrange"] = lst;
        }
        private string CompanySubject()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comsubject = "";
            switch (comcod)
            {
                case "3330":
                    //case "3101":
                    comsubject = " Requests you to arrange supply of following materials from your organization.";
                    break;

                case "3101":
                case "3368":
                    comsubject = " As per our agreed terms and conditions, please arrange to deliver the following items :";
                    break;

                default:
                    comsubject = " Requests you to  supply the following materials from your organization.";
                    break;
            }
            return comsubject;
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //  ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click2);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void lnkPrint_Click2(object sender, EventArgs e)
        {



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string orderno = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();
            /**
            var scheme = HttpContext.Current.Request.Url.Scheme;
            var host = HttpContext.Current.Request.Url.Host;
            var port = HttpContext.Current.Request.Url.IsDefaultPort ? "" : ":"+HttpContext.Current.Request.Url.Port.ToString();
            string hostname = scheme+"://" + host + port + HttpContext.Current.Request.ApplicationPath + "/F_99_Allinterface/";
            **/

            string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_99_Allinterface/";
            string currentptah = "PurchasePrint.aspx?Type=OrderPrint&orderno=" + orderno;
            string totalpath = hostname + currentptah;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";

            //lbtnPrint.PostBackUrl = "~/F_99_Allinterface/PurchasePrint.aspx?Type=OrderPrint&orderno=" + orderno;
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private string CompanyPrintWorkOrder()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PrintWorkOrder = "";
            switch (comcod)
            {
                case "1301":
                case "3301":
                    PrintWorkOrder = "PrintWorkOrder02";
                    break;
                default:
                    PrintWorkOrder = "PrintWorkOrder";
                    break;
            }
            return PrintWorkOrder;
        }
        private void PrintRupaSan()
        {
            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                if (this.lbtnOk.Text == "Ok")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Plese Select Order No";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string wrkid = "POR" + this.txtCurOrderDate.Text.Substring(6, 4) + this.txtCurOrderDate.Text.Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();

                //string Calltype = this.PrintCallType();
                DataSet _ReportDataSet = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "SHOWORKORDER01", wrkid, "", "", "", "", "", "", "", "");
                ViewState["tblRep"] = _ReportDataSet;
                DataTable dt = _ReportDataSet.Tables[0];
                string Para1 = _ReportDataSet.Tables[1].Rows[0]["leterdes"].ToString();
                string Orderdate = Convert.ToDateTime(_ReportDataSet.Tables[1].Rows[0]["orderdat"]).ToString("MMMM  dd, yyyy");
                string SupName = _ReportDataSet.Tables[1].Rows[0]["ssirdesc"].ToString();
                string Address = _ReportDataSet.Tables[1].Rows[0]["address"].ToString();
                string Cperson = _ReportDataSet.Tables[1].Rows[0]["cperson"].ToString();
                string Phone = _ReportDataSet.Tables[1].Rows[0]["phone"].ToString();

                DataTable dtterm = _ReportDataSet.Tables[2];

                string Type = this.CompanyPrintWorkOrder();
                ReportDocument rptwork = new ReportDocument();
                //if (Type == "PrintWorkOrder")
                //{
                //    string trmplace = "* " + dtterm.Rows[0]["termssubj"].ToString() + " : ";
                //    string place = dtterm.Rows[0]["termsdesc"].ToString().Trim();
                //    string trmpdate = "* " + dtterm.Rows[1]["termssubj"].ToString() + " : ";
                //    string pdate = dtterm.Rows[1]["termsdesc"].ToString().Trim();
                //    string trmpayment = "* " + dtterm.Rows[2]["termssubj"].ToString() + " : ";
                //    string payment = dtterm.Rows[2]["termsdesc"].ToString().Trim();
                //    string trmcarring = "* " + dtterm.Rows[3]["termssubj"].ToString() + " : ";
                //    string carring = dtterm.Rows[3]["termsdesc"].ToString().Trim();
                //    string trmmeasurement = "* " + dtterm.Rows[4]["termssubj"].ToString() + " : ";
                //    string measurement = dtterm.Rows[4]["termsdesc"].ToString().Trim();
                //    string trmflatno = "* " + dtterm.Rows[5]["termssubj"].ToString() + " : ";
                //    string flatno = dtterm.Rows[5]["termsdesc"].ToString().Trim();
                //    string trmflatowner = "* " + dtterm.Rows[6]["termssubj"].ToString() + " : ";
                //    string flatowner = dtterm.Rows[6]["termsdesc"].ToString().Trim();
                //    string trmothers = "* " + dtterm.Rows[7]["termssubj"].ToString() + " : ";
                //    string Others = dtterm.Rows[7]["termsdesc"].ToString().Trim();
                //    string trmCOnperson = "* " + dtterm.Rows[8]["termssubj"].ToString() + " : ";
                //    string ConPerson = dtterm.Rows[8]["termsdesc"].ToString().Trim();

                //    rptwork = new RealERPRPT.R_14_Pro.rptWorkOrder();
                //    TextObject txtsubject = rptwork.ReportDefinition.ReportObjects["txtsubject"] as TextObject;
                //    txtsubject.Text = this.txtSubject.Text;
                //    TextObject txtCompany = rptwork.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
                //    txtCompany.Text = comnam;
                //    TextObject txtAddress = rptwork.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
                //    txtAddress.Text = comadd;

                //    TextObject rptpurno = rptwork.ReportDefinition.ReportObjects["purno"] as TextObject;
                //    rptpurno.Text = "Purchase  Request No:- " + this.lblCurOrderNo1.Text.Trim() + this.txtCurOrderNo2.Text;
                //    TextObject rptRefno = rptwork.ReportDefinition.ReportObjects["refno"] as TextObject;
                //    rptRefno.Text = "Order Ref No:-" + this.txtOrderRefNo.Text;
                //    TextObject supname = rptwork.ReportDefinition.ReportObjects["supname"] as TextObject;
                //    supname.Text = SupName;
                //    TextObject Supadd = rptwork.ReportDefinition.ReportObjects["saddress"] as TextObject;
                //    Supadd.Text = Address;
                //    TextObject CperSon = rptwork.ReportDefinition.ReportObjects["txtcperson"] as TextObject;
                //    CperSon.Text = Cperson;
                //    TextObject txtPhoneNumber = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                //    txtPhoneNumber.Text = "Phone Number: " + Phone;
                //    TextObject rptpurdate = rptwork.ReportDefinition.ReportObjects["txtOrderDate"] as TextObject;
                //    rptpurdate.Text = Orderdate;
                //    TextObject rptPara1 = rptwork.ReportDefinition.ReportObjects["TxtLETERDES"] as TextObject;
                //    rptPara1.Text = Para1;
                //    TextObject rptplace = rptwork.ReportDefinition.ReportObjects["place"] as TextObject;
                //    rptplace.Text = (place.Length > 0) ? trmplace + place : "";

                //    TextObject rpttxtsupplydetails = rptwork.ReportDefinition.ReportObjects["txtsupplydetails"] as TextObject;
                //    rpttxtsupplydetails.Text = this.txtOrderNarr.Text.Trim(); ;
                //    rptwork.ReportDefinition.Sections["ReportFooterSection2"].SectionFormat.EnableSuppress = (place.Length > 0) ? false : true;

                //    TextObject rptpdate = rptwork.ReportDefinition.ReportObjects["pdate"] as TextObject;
                //    rptpdate.Text = (pdate.Length > 0) ? trmpdate + pdate : "";
                //    rptwork.ReportDefinition.Sections["ReportFooterSection10"].SectionFormat.EnableSuppress = (pdate.Length > 0) ? false : true;

                //    TextObject rptpayment = rptwork.ReportDefinition.ReportObjects["payment"] as TextObject;
                //    rptpayment.Text = (payment.Length > 0) ? trmpayment + payment : "";
                //    rptwork.ReportDefinition.Sections["ReportFooterSection11"].SectionFormat.EnableSuppress = (payment.Length > 0) ? false : true;

                //    TextObject rptcarring = rptwork.ReportDefinition.ReportObjects["carring"] as TextObject;
                //    rptcarring.Text = (carring.Length > 0) ? trmcarring + carring : "";
                //    rptwork.ReportDefinition.Sections["ReportFooterSection12"].SectionFormat.EnableSuppress = (carring.Length > 0) ? false : true;

                //    TextObject rptmeasrment = rptwork.ReportDefinition.ReportObjects["measrment"] as TextObject;
                //    rptmeasrment.Text = (measurement.Length > 0) ? trmmeasurement + measurement : "";
                //    rptwork.ReportDefinition.Sections["ReportFooterSection13"].SectionFormat.EnableSuppress = (measurement.Length > 0) ? false : true;

                //    TextObject rptflatno = rptwork.ReportDefinition.ReportObjects["flatno"] as TextObject;
                //    rptflatno.Text = (flatno.Length > 0) ? trmflatno + flatno : "";
                //    rptwork.ReportDefinition.Sections["ReportFooterSection14"].SectionFormat.EnableSuppress = (flatno.Length > 0) ? false : true;

                //    TextObject rptflatowner = rptwork.ReportDefinition.ReportObjects["flatowner"] as TextObject;
                //    rptflatowner.Text = (flatowner.Length > 0) ? trmflatowner + flatowner : "";
                //    rptwork.ReportDefinition.Sections["ReportFooterSection15"].SectionFormat.EnableSuppress = (flatowner.Length > 0) ? false : true;

                //    TextObject rptCperson = rptwork.ReportDefinition.ReportObjects["cperson"] as TextObject;
                //    rptCperson.Text = (ConPerson.Length > 0) ? trmCOnperson + ConPerson : "";
                //    rptwork.ReportDefinition.Sections["ReportFooterSection16"].SectionFormat.EnableSuppress = (ConPerson.Length > 0) ? false : true;

                //    TextObject rptOthrs = rptwork.ReportDefinition.ReportObjects["others"] as TextObject;
                //    rptOthrs.Text = (Others.Length > 0) ? trmothers + Others : "";
                //    rptwork.ReportDefinition.Sections["ReportFooterSection17"].SectionFormat.EnableSuppress = (Others.Length > 0) ? false : true;

                //    if (comcod == "3202")
                //    {
                //        double totalqty = Convert.ToDouble((Convert.IsDBNull(_ReportDataSet.Tables[0].Compute("Sum(ordrqty)", "")) ?
                //             0.00 : _ReportDataSet.Tables[0].Compute("Sum(ordrqty)", "")));
                //        TextObject txtrpttoordrqty = rptwork.ReportDefinition.ReportObjects["txttoqty"] as TextObject;
                //        txtrpttoordrqty.Text = totalqty.ToString("#,##0.00;(#,##0.00); ");
                //    }
                //    double t1 = Convert.ToDouble((Convert.IsDBNull(_ReportDataSet.Tables[0].Compute("Sum(ordramt)", "")) ?
                //     0.00 : _ReportDataSet.Tables[0].Compute("Sum(ordramt)", "")));
                //    TextObject txtkword = rptwork.ReportDefinition.ReportObjects["txtkword"] as TextObject;
                //    txtkword.Text = "In Word: " + ASTUtility.Trans(t1, 2);
                //    TextObject txtuserinfo = rptwork.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                //    rptwork.SetDataSource(this.HiddenSameDataPrint(_ReportDataSet.Tables[0]));

                //    if (ConstantInfo.LogStatus == true)
                //    {
                //        string eventtype = "Materials Purchase Order Info";
                //        string eventdesc = "Print Order";
                //        string eventdesc2 = "Request No:- " + this.lblCurOrderNo1.Text.Trim() + this.txtCurOrderNo2.Text; ;
                //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                //    }

                //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //    rptwork.SetParameterValue("ComLogo", ComLogo);
                //    Session["Report1"] = rptwork;
                //    this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
                //}
                //else
                //{
                string fax = _ReportDataSet.Tables[1].Rows[0]["fax"].ToString();

                string trmplace = "* " + dtterm.Rows[0]["termssubj"].ToString() + " : ";
                string place = dtterm.Rows[0]["termsdesc"].ToString().Trim();
                string trmpdate = "* " + dtterm.Rows[1]["termssubj"].ToString() + " : ";
                string pdate = dtterm.Rows[1]["termsdesc"].ToString().Trim();
                string trmcarring = "* " + dtterm.Rows[2]["termssubj"].ToString() + " : ";
                string carring = dtterm.Rows[2]["termsdesc"].ToString().Trim();
                //string trmbill = "* " + dtterm.Rows[3]["termssubj"].ToString() + "";
                //string bill = "* " + dtterm.Rows[3]["termsdesc"].ToString().Trim();

                string trmbill = (comcod == "3330") ? "" : ("* " + dtterm.Rows[3]["termssubj"].ToString() + ": ");
                string bill = (comcod == "3330") ? ("* " + dtterm.Rows[3]["termsdesc"].ToString().Trim()) : dtterm.Rows[3]["termsdesc"].ToString().Trim();

                string trmpayment = "* " + dtterm.Rows[4]["termssubj"].ToString() + " : ";
                string payment = dtterm.Rows[4]["termsdesc"].ToString().Trim();

                string trmothers = "* " + dtterm.Rows[5]["termssubj"].ToString() + " : ";
                string Others = dtterm.Rows[5]["termsdesc"].ToString().Trim();

                rptwork = new RealERPRPT.R_14_Pro.rptWorkOrder02();

                TextObject txtsubject = rptwork.ReportDefinition.ReportObjects["txtsubject"] as TextObject;
                txtsubject.Text = this.txtSubject.Text;
                TextObject txtCompany = rptwork.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
                txtCompany.Text = comnam;
                TextObject txtAddress = rptwork.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
                txtAddress.Text = comadd;

                TextObject rptpurno = rptwork.ReportDefinition.ReportObjects["purno"] as TextObject;
                rptpurno.Text = "Purchase  Request No:- " + this.lblCurOrderNo1.Text.Trim() + this.txtCurOrderNo2.Text;
                TextObject rptRefno = rptwork.ReportDefinition.ReportObjects["refno"] as TextObject;
                rptRefno.Text = "Order Ref No:-" + this.txtOrderRefNo.Text;
                TextObject supname = rptwork.ReportDefinition.ReportObjects["supname"] as TextObject;
                supname.Text = SupName;
                TextObject Supadd = rptwork.ReportDefinition.ReportObjects["saddress"] as TextObject;
                Supadd.Text = Address;
                TextObject txtPhoneNumber = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                txtPhoneNumber.Text = "Phone Number: " + Phone;
                TextObject Fax = rptwork.ReportDefinition.ReportObjects["txtfax"] as TextObject;
                Fax.Text = "Fax Number: " + fax;
                TextObject rptpurdate = rptwork.ReportDefinition.ReportObjects["txtOrderDate"] as TextObject;
                rptpurdate.Text = Orderdate;
                TextObject rptPara1 = rptwork.ReportDefinition.ReportObjects["TxtLETERDES"] as TextObject;
                rptPara1.Text = Para1;
                TextObject rptplace = rptwork.ReportDefinition.ReportObjects["place"] as TextObject;
                rptplace.Text = (place.Length > 0) ? trmplace + place : "";

                rptwork.ReportDefinition.Sections["GroupFooterSection5"].SectionFormat.EnableSuppress = (((DataTable)ViewState["tblpaysch"]).Rows.Count > 0) ? false : true;


                TextObject rpttxtsupplydetails = rptwork.ReportDefinition.ReportObjects["txtsupplydetails"] as TextObject;
                rpttxtsupplydetails.Text = this.txtOrderNarr.Text.Trim(); ;
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

                TextObject rptOthrs = rptwork.ReportDefinition.ReportObjects["others"] as TextObject;
                rptOthrs.Text = trmothers + Others;



                DataTable dtorder = (DataTable)ViewState["tblOrder"];
                DataTable dt1;
                DataTable dt2;
                DataTable dt3;


                // Carring
                DataView dv1 = dtorder.DefaultView;
                dv1.RowFilter = ("rsircode  like '019999901%'");
                dt1 = dv1.ToTable();

                //Deduction
                dv1 = dtorder.DefaultView;
                dv1.RowFilter = ("rsircode like'019999902%'");
                dt2 = dv1.ToTable();

                //Material
                dv1 = dtorder.DefaultView;
                dv1.RowFilter = ("rsircode not like '0199999%'");
                dt3 = dv1.ToTable();


                double amtcar = (dt1.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(ordramt)", "")) ? 0.00 : dt1.Compute("Sum(ordramt)", "")));
                double amtdis = (dt2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(ordramt)", "")) ? 0.00 : dt2.Compute("Sum(ordramt)", "")));
                //



                double amtmat = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(ordramt)", "")) ? 0.00 : dt3.Compute("Sum(ordramt)", "")));

                TextObject txtcarcost = rptwork.ReportDefinition.ReportObjects["txtcarcost"] as TextObject;
                txtcarcost.Text = amtcar.ToString("#,##0.00;(#,##0.00);");

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

                rptwork.SetDataSource(_ReportDataSet.Tables[0]);
                rptwork.Subreports["RptOrderPaymentSch.rpt"].SetDataSource((DataTable)ViewState["tblpaysch"]);

                // report.OpenSubReport(nameOfTheSubReport).SetDataSo urce(secondDataSet);

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Materials Purchase Order Info";
                    string eventdesc = "Print Order";
                    string eventdesc2 = " Request No:- " + this.lblCurOrderNo1.Text.Trim() + this.txtCurOrderNo2.Text; ;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rptwork.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rptwork;

                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

                // }
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

        private string ComOrderNo()
        {
            string comcod = this.GetCompCode();
            string orderno = "";
            switch (comcod)
            {
                case "1108":
                case "1109":
                case "3315":
                case "3316":
                case "3317":
                    //case "3101":
                    orderno = ASTUtility.Right(this.lblissueno.Text.Trim(), 6);
                    break;

                default:
                    orderno = this.lblCurOrderNo1.Text.Trim() + this.txtCurOrderNo2.Text;
                    break;


            }
            return orderno;
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









        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }


        private string CompanyLength()
        {
            string comcod = this.GetCompCode();
            string length = "";
            switch (comcod)
            {
                case "3101":
                case "3340":
                    length = "length";
                    break;


                default:
                    length = "";
                    break;
            }

            return length;

        }
        protected void lbtnPrevOrderList_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string length = this.CompanyLength();
            string CurDate1 = this.GetStdDate(this.txtCurOrderDate.Text.Trim());
            string orderno = (this.Request.QueryString["genno"].ToString().Trim().Length == 0 ? "" : this.Request.QueryString["genno"].ToString()) + "%";


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPREVORDERLIST", CurDate1,
                          orderno, length, usrid, "", "", "", "", "");






            if (ds1 == null)
                return;
            this.ddlPrevOrderList.Items.Clear();
            this.ddlPrevOrderList.DataTextField = "orderno1";
            this.ddlPrevOrderList.DataValueField = "orderno";
            this.ddlPrevOrderList.DataSource = ds1.Tables[0];
            this.ddlPrevOrderList.DataBind();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            if (this.lbtnOk.Text == "New")
            {
                this.MultiView1.ActiveViewIndex = -1;

                //this.lblPrevious.Visible = true;
                //this.txtsearchpre.Visible = true;
                this.lbtnPrevOrderList.Visible = true;
                this.ddlPrevOrderList.Visible = true;
                this.ddlPrevOrderList.Items.Clear();
                this.lblCurOrderNo1.Text = "POR" + DateTime.Today.ToString("MM") + "-";
                this.txtCurOrderDate.Enabled = true;
                this.lblissueno.Enabled = true;
                this.txtOrderRefNo.Text = "";
                this.txtOrderRefNo.ReadOnly = false;
                this.lssircode.Text = "";
                // this.txtLETDES.Text =comnam+" requests you to arrange supply of following materials from your organization.";
                //"Refer to your offer with specification dated on " +
                //                     DateTime.Today.AddDays(-7).ToString("dd.MM.yyyy") +
                //                     " and subsequent discussion our management is pleased to issue work " +
                //                     "order for the following terms & conditions";

                //For Charging
                ViewState.Remove("tblproject");
                this.ddlProjectName.Items.Clear();



                this.txtPreparedBy.Text = "";
                this.txtApprovedBy.Text = "";
                this.txtOrderNarr.Text = "";
                this.lblissueno.Text = "";
                this.gvOrderInfo.DataSource = null;
                this.gvOrderInfo.DataBind();
                this.gvOrderTerms.DataSource = null;
                this.gvOrderTerms.DataBind();
                this.gvPayment.DataSource = null;
                this.gvPayment.DataBind();
                this.lbtnOk.Text = "Ok";
                this.ddlSuplierList.Items.Clear();
                return;
            }


            //"Refer to your offer with specification dated on " +
            //this.lblPrevious.Visible = false;
            //this.txtsearchpre.Visible = false;




            this.lbtnPrevOrderList.Visible = false;
            this.ddlPrevOrderList.Visible = false;
            this.txtCurOrderNo2.ReadOnly = true;
            // this.lblissueno.Enabled = true;
            this.lbtnOk.Text = "New";

            if (this.ddlPrevOrderList.Items.Count <= 0)
            {
                this.MultiView1.ActiveViewIndex = 0;
                this.ResourceForOrder();
                return;

            }
            this.MultiView1.ActiveViewIndex = 1;
            this.Get_Pur_Order_Info();
            //this.lbtnPrevOrderList_Click(null, null);
            this.ShowProjectFiles();
            this.hideTermsConditions();
            /// email button visible ture or false
            SendMail();
        }


        private void GetIssueNO()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETISSUENO", "", "", "", "", "", "", "", "", "");
            this.lblissueno.Text = ds2.Tables[0].Rows[0]["isunum"].ToString();

        }
        private void GetProConPerson(string pactcode)
        {
            //Session.Remove("tblproconper");
            string comcod = this.GetCompCode();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPREPROCONPER", pactcode, "", "", "", "", "", "", "", "");
            //  Session["tblproconper"] = ds2.Tables[0];
            if (ds2.Tables[0].Rows.Count > 0)
            {
                this.gvOrderTerms.DataSource = ds2.Tables[0];
                this.gvOrderTerms.DataBind();
            }
        }


        private void GetPreNarration()
        {
            //Session.Remove("tblproconper");
            string comcod = this.GetCompCode();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPRENARRATION", "", "", "", "", "", "", "", "", "");
            //  Session["tblproconper"] = ds2.TabltxtLETDESes[0];
            if (ds2.Tables[0].Rows.Count > 0)
            {
                this.txtOrderNarr.Text = ds2.Tables[0].Rows[0]["pordnar"].ToString().Trim();
            }

        }
        private void GetOrRefno(string pactcode)
        {


            string comcod = this.GetCompCode();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETORDERREFNO", pactcode, "", "", "", "", "", "", "", "");
            this.txtOrderRefNo.Text = ds2.Tables[0].Rows[0]["pordref"].ToString();

        }
        protected void GetOrderNo()
        {

            string comcod = this.GetCompCode();
            string mOrderdate = this.GetStdDate(this.txtCurOrderDate.Text.Trim());
            string mOrderNo = "NEWORDER";
            if (this.ddlPrevOrderList.Items.Count > 0)
                mOrderNo = this.ddlPrevOrderList.SelectedValue.ToString();

            if (mOrderNo == "NEWORDER")
            {



                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETLASTORDERINFO", mOrderdate, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurOrderNo1.Text = ds1.Tables[0].Rows[0]["maxorderno1"].ToString().Substring(0, 6);
                    this.txtCurOrderNo2.Text = ds1.Tables[0].Rows[0]["maxorderno1"].ToString().Substring(6, 5);
                    this.ddlPrevOrderList.DataTextField = "maxorderno1";
                    this.ddlPrevOrderList.DataValueField = "maxorderno";
                    this.ddlPrevOrderList.DataSource = ds1.Tables[0];
                    this.ddlPrevOrderList.DataBind();
                }



            }
        }

        private void ResourceForOrder()
        {

            ViewState.Remove("tblResP");

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurOrderDate.Text.Trim());
            string findSupplier = this.txtsrchSupplier.Text.Trim() + "%";
            string[] approvno;


            string qgenno = this.Request.QueryString["genno"] ?? "";
            //ngenno=
            //if (qgenno.Length > 0)
            //{ 
            // for


            //}
            //  if()


            string findReq = qgenno.Length == 0 ? "%%" : this.Request.QueryString["genno"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "RESOURCEINFFORORDER", CurDate1,
                         findSupplier, findReq, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlSuplierList.DataTextField = "ssirdesc1";
            this.ddlSuplierList.DataValueField = "ssircode";
            this.ddlSuplierList.DataSource = ds1.Tables[1];
            this.ddlSuplierList.DataBind();

            if (this.Request.QueryString.AllKeys.Contains("ssircode") && this.Request.QueryString["ssircode"].ToString() != "")
            {
                this.ddlSuplierList.SelectedValue = this.Request.QueryString["ssircode"].ToString();
            }

            ViewState["tblResP"] = ds1.Tables[0];
            ViewState["tblProject"] = ds1.Tables[1];
            this.ddlSuplierList_SelectedIndexChanged(null, null);
        }

        protected void imgSearchOrderno_Click(object sender, EventArgs e)
        {
            this.ResourceForOrder();
        }

        protected void ddlSuplierList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblResP"];
            string supcode = this.ddlSuplierList.SelectedValue.ToString();
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "ssircode in ('" + supcode + "')";
            this.gvAprovInfo.DataSource = dv1.ToTable();
            this.gvAprovInfo.DataBind();

            //For Visible Item Serial Manama
            string comcod = GetCompCode();
            if (comcod == "3353" || comcod == "3101")
            {
                this.gvAprovInfo.Columns[1].Visible = true;
            }

        }


        protected void Get_Pur_Order_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurOrderDate.Text.Trim());
            string mOrderNo = "NEWORDER";
            string typecod = this.ddltypecod.SelectedValue;
            if (comcod == "3301" || comcod == "2301" || comcod == "1301")
            {
                this.lblReqNarr.Text = "Special Notes:- ";
            }
            if (this.ddlPrevOrderList.Items.Count > 0)
            {
                // this.ddlSuplierList.Items.Clear();
                this.txtCurOrderDate.Enabled = false;
                this.lblissueno.Enabled = false;
                mOrderNo = this.ddlPrevOrderList.SelectedValue.ToString();
            }

            DataTable dt2 = (DataTable)ViewState["tblProject"];
            string pactcode = "";
            if (dt2 != null)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    pactcode += dt2.Rows[i]["pactcode"].ToString();
                }
            }

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURORDERINFO", mOrderNo, CurDate1, pactcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["dsOrder"] = ds1;
            ViewState["tblOrder"] = this.HiddenSameData(ds1.Tables[0]);
            ViewState["purtermcon"] = ds1.Tables[1];

            this.gvOrderTerms.DataSource = ds1.Tables[1];
            this.gvOrderTerms.DataBind();
            // Modified by emdad 08.06.2021

            //switch (comcod)
            //{

            //    //case "3335":

            //    //    if (this.ddlPrevOrderList.Items.Count > 0)
            //    //    {
            //    //        this.gvOrderTerms.DataSource = ds1.Tables[1];
            //    //        this.gvOrderTerms.DataBind();

            //    //    }

            //        break;
            //    default:
            //        this.gvOrderTerms.DataSource = ds1.Tables[1];
            //        this.gvOrderTerms.DataBind();
            //        break;
            //}
            //if (comcod != "3335" || comcod != "3101")
            //{
            //    this.gvOrderTerms.DataSource = ds1.Tables[1];
            //    this.gvOrderTerms.DataBind();
            //}

            if (comcod == "3338")
            {
                gvOrderTerms.Columns[2].Visible = false;
            }

            ViewState["tblpaysch"] = ds1.Tables[2];
            this.SchData_Bind();


            if (mOrderNo == "NEWORDER")
            {

                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETLASTORDERINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurOrderNo1.Text = ds1.Tables[0].Rows[0]["maxorderno1"].ToString().Substring(0, 6);
                    this.txtCurOrderNo2.Text = ds1.Tables[0].Rows[0]["maxorderno1"].ToString().Substring(6, 5);
                }

                switch (comcod)
                {
                    case "1108":
                    case "1109":
                    case "3315":
                    case "3316":
                    case "3317":
                    //case "3101":
                    case "5101":
                    case "3330":
                        this.GetIssueNO();
                        break;
                }
                return;
            }

            this.lblCurOrderNo1.Text = ds1.Tables[3].Rows[0]["orderno1"].ToString().Substring(0, 6);
            this.txtCurOrderNo2.Text = ds1.Tables[3].Rows[0]["orderno1"].ToString().Substring(6, 5);
            this.txtOrderRefNo.Text = ds1.Tables[3].Rows[0]["pordref"].ToString();
            this.txtLETDES.Text = ds1.Tables[3].Rows[0]["leterdes"].ToString();
            this.txtSubject.Text = ds1.Tables[3].Rows[0]["subject"].ToString();

            this.txtCurOrderDate.Text = Convert.ToDateTime(ds1.Tables[3].Rows[0]["orderdat"]).ToString("dd.MM.yyyy");
            this.txtPreparedBy.Text = ds1.Tables[3].Rows[0]["pordbydes"].ToString();
            this.lssircode.Text = ds1.Tables[3].Rows[0]["ssircode"].ToString();
            this.txtApprovedBy.Text = ds1.Tables[3].Rows[0]["appbydes"].ToString();
            this.txtApprovalDate.Text = Convert.ToDateTime(ds1.Tables[3].Rows[0]["apprdat"]).ToString("dd.MM.yyyy");
            this.txtOrderNarr.Text = ds1.Tables[3].Rows[0]["pordnar"].ToString();
            this.txtadvAmt.Text = Convert.ToDouble(ds1.Tables[3].Rows[0]["advamt"]).ToString("#,##0;(#,##0); ");
            this.lblissueno.Text = ds1.Tables[3].Rows[0]["oissueno"].ToString();

            this.txtOrderNarrP.Text = ds1.Tables[3].Rows[0]["terms"].ToString();

            this.gvOrderInfo_DataBind();
        }
        protected void gvOrderInfo_DataBind()
        {
            string comcod = this.GetCompCode();
            DataTable tbl1 = this.HiddenSameData((DataTable)ViewState["tblOrder"]);
            this.gvOrderInfo.DataSource = tbl1;
            this.gvOrderInfo.DataBind();

            //if (this.ddlPrevOrderList.Items.Count > 0)
            //{
            ((LinkButton)this.gvOrderInfo.FooterRow.FindControl("lbtnDelete")).Visible = (this.Request.QueryString["InputType"].ToString().Trim() == "OrderEntry" || this.Request.QueryString["InputType"].ToString().Trim() == "OrderEdit" || this.Request.QueryString["InputType"].ToString().Trim() == "FirstApp" || this.Request.QueryString["InputType"].ToString().Trim() == "SecondApp");
            ((LinkButton)this.gvOrderInfo.FooterRow.FindControl("lbtnUpdatePurOrder")).Visible = (this.Request.QueryString["InputType"].ToString().Trim() == "OrderEntry" || this.Request.QueryString["InputType"].ToString().Trim() == "OrderEdit" || this.Request.QueryString["InputType"].ToString().Trim() == "FirstApp" || this.Request.QueryString["InputType"].ToString().Trim() == "SecondApp");






            //For Forward
            switch (comcod)
            {

                case "3354"://Edison Real Estate
                case "3335":
                case "3368":
                case "3101":
                    ((CheckBox)this.gvOrderInfo.FooterRow.FindControl("lblfchkbox")).Visible = (this.Request.QueryString["InputType"].ToString().Trim() == "FirstApp");
                    break;

                default:
                    break;


            }

            //For Visible Item Serial Manama
            if (comcod == "3353")
            {
                this.gvOrderInfo.Columns[1].Visible = true;
            }
            if (comcod == "3354" || comcod == "3101")
            {
                this.gvOrderInfo.Columns[20].Visible = true;
            }

            if (tbl1.Rows.Count == 0)
                return;


            double amt1 = 0.00, amt2 = 0.00;
            DataTable td1 = tbl1.Copy();
            DataTable td2 = tbl1.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like '019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(ordramt)", "")) ? 0.00 : td2.Compute("Sum(ordramt)", "")));
            amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(ordramt)", "")) ? 0.00 : td1.Compute("Sum(ordramt)", "")));
            ((Label)this.gvOrderInfo.FooterRow.FindControl("lblgvFooterTOrderAmt")).Text = (amt1 - amt2).ToString("#,##0.00;(#,##0.00); ");

        }




        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            DataView dv = dt1.DefaultView;
            dv.Sort = "rsircode";
            dt1 = dv.ToTable();

            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {

                    dt1.Rows[j]["rsirdesc1"] = "";
                }
                else
                {
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                }
            }
            return dt1;
        }


        private DataTable HiddenSameDataPrint(DataTable dt1)
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
                    dt1.Rows[j]["reqno"] = "";
                    dt1.Rows[j]["mrfno"] = "";
                    dt1.Rows[j]["rsirdesc"] = "";
                }

                else
                {
                    if (dt1.Rows[j]["reqno"].ToString() == reqno)
                    {
                        dt1.Rows[j]["reqno"] = "";
                        dt1.Rows[j]["mrfno"] = "";

                    }

                    if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                        dt1.Rows[j]["rsirdesc"] = "";


                    reqno = dt1.Rows[j]["reqno"].ToString();
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                }

            }

            return dt1;
        }








        protected void Session_tblOrder_Update()
        {

            DataTable tbl1 = (DataTable)ViewState["tblOrder"];

            string pactcode = ASTUtility.Left(tbl1.Rows[0]["pactcode"].ToString(), 8);
            int TblRowIndex2;
            for (int j = 0; j < this.gvOrderInfo.Rows.Count; j++)
            {
                //Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.grvissue.Rows[i].FindControl("lblbalqty")).Text.Trim()));
                //double dispercnt = Convert.ToDouble(ASTUtility.StrPosOrNagative(ASTUtility.ExprToValue("0" + ((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvdispercnt")).Text.Trim().Replace("%", ""))));

                string rsircode = ((Label)this.gvOrderInfo.Rows[j].FindControl("lblgvResCod")).Text.Trim();
                double dgvorderQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvOrderQty")).Text.Trim()));

                double aprovsrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvOrderInfo.Rows[j].FindControl("lblgvApprovsRate")).Text.Trim()));
                double dispercnt = Convert.ToDouble(ASTUtility.StrPosOrNagative("0" + ((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvdispercnt")).Text.Trim().Replace("%", "")));
                double aprovrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvOrderRate")).Text.Trim()));
                double dgvAppAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvOrderAmt")).Text.Trim()));

                string rmrks = ((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvrmrks")).Text.Trim();

                TblRowIndex2 = (this.gvOrderInfo.PageIndex) * this.gvOrderInfo.PageSize + j;

                if (pactcode != "11020099")
                {
                    if (aprovsrate < aprovrate)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Supplier rate must be greater then Actual Rate";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                }


                if (rsircode.Substring(0, 7) == "0199999")
                {
                    double dgvMRRRate = (dgvorderQty > 0) ? dgvAppAmt / dgvorderQty : 00;
                    tbl1.Rows[TblRowIndex2]["ordrqty"] = dgvorderQty;
                    tbl1.Rows[TblRowIndex2]["aprovrate"] = dgvMRRRate;
                    tbl1.Rows[TblRowIndex2]["ordramt"] = dgvAppAmt;
                }
                else
                {
                    dispercnt = (aprovrate > 0) ? ((aprovsrate - aprovrate) * 100) / aprovsrate : dispercnt;
                    aprovrate = (aprovrate > 0) ? aprovrate : (aprovsrate - aprovsrate * .01 * dispercnt);
                    dgvAppAmt = dgvorderQty * aprovrate;
                    tbl1.Rows[TblRowIndex2]["ordrqty"] = dgvorderQty;
                    tbl1.Rows[TblRowIndex2]["aprovsrate"] = aprovsrate;
                    tbl1.Rows[TblRowIndex2]["dispercnt"] = dispercnt;
                    tbl1.Rows[TblRowIndex2]["aprovrate"] = aprovrate;
                    tbl1.Rows[TblRowIndex2]["ordramt"] = dgvAppAmt;
                    tbl1.Rows[TblRowIndex2]["rmrks"] = rmrks;
                }

            }
            ViewState["tblOrder"] = tbl1;

            //DataTable tbl1 = (DataTable)ViewState["tblOrder"];
            //int TblRowIndex2;
            //for (int j = 0; j < this.gvOrderInfo.Rows.Count; j++)
            //{
            //    double dgvOrderQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvOrderQty")).Text.Trim()));
            //    double dgvApprovRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvOrderInfo.Rows[j].FindControl("txtgvApprovRate")).Text.Trim()));
            //    double dgvOrderAmt = dgvOrderQty * dgvApprovRate;

            //    ((TextBox)this.gvOrderInfo.Rows[j].FindControl("txtgvOrderQty")).Text = dgvOrderQty.ToString("#,##0.000;(#,##0.000); ");
            //    ((Label)this.gvOrderInfo.Rows[j].FindControl("txtgvApprovRate")).Text = dgvApprovRate.ToString("#,##0.0000;(#,##0.0000); ");
            //    ((Label)this.gvOrderInfo.Rows[j].FindControl("lblgvOrderAmt")).Text = dgvOrderAmt.ToString("#,##0.000;(#,##0.000); ");

            //    TblRowIndex2 = (this.gvOrderInfo.PageIndex) * this.gvOrderInfo.PageSize + j;
            //    tbl1.Rows[TblRowIndex2]["ordrqty"] = dgvOrderQty;
            //    tbl1.Rows[TblRowIndex2]["ordramt"] = dgvOrderAmt;
            //}
            //ViewState["tblOrder"] = tbl1;
        }



        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblOrder_Update();
            this.gvOrderInfo.PageIndex = ((DropDownList)this.gvOrderInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
            this.gvOrderInfo_DataBind();
        }


        private void CreateDataTable()
        {

            ViewState.Remove("tblapproval");
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("fappid", Type.GetType("System.String"));
            tblt01.Columns.Add("fappdat", Type.GetType("System.String"));
            tblt01.Columns.Add("fapptrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("fappseson", Type.GetType("System.String"));
            tblt01.Columns.Add("secappid", Type.GetType("System.String"));
            tblt01.Columns.Add("secappdat", Type.GetType("System.String"));
            tblt01.Columns.Add("secapptrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("secappseson", Type.GetType("System.String"));
            ViewState["tblapproval"] = tblt01;
        }


        private string GetReqApproval(string approval)
        {


            string type = this.Request.QueryString["InputType"];
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataTable tbl1 = (DataTable)ViewState["tblOrder"];

            string pactcode = ASTUtility.Left(tbl1.Rows[0]["pactcode"].ToString(), 8);

            DataSet ds1 = new DataSet("ds1");
            System.IO.StringReader xmlSR;

            switch (type)
            {
                case "OrderEntry":
                    switch (comcod)
                    {

                        case "3335": // Edison
                        case "3355": // Green Wood
                        case "3354":  // Edison Real Estate
                        case "1205":  //P2P Construction
                        case "3351":  //wecon Properties
                        case "3352":  //p2p360
                        case "3370":  //p2p360
                                      //case "3101": // ASIT
                            break;

                        //case "3101":
                        case "3368"://finlay
                            /*
                            if (comcod == "3368" & pactcode != "11020099")//Finlay
                            {
                                if (approval == "")
                                {
                                    this.CreateDataTable();
                                    DataTable dt = (DataTable)ViewState["tblapproval"];
                                    DataRow dr1 = dt.NewRow();
                                    dr1["fappid"] = usrid;
                                    dr1["fappdat"] = Date;
                                    dr1["fapptrmid"] = trmnid;
                                    dr1["fappseson"] = session;
                                    dr1["secappid"] = "";
                                    dr1["secappdat"] = "";
                                    dr1["secapptrmid"] = "";
                                    dr1["secappseson"] = "";

                                    dt.Rows.Add(dr1);
                                    ds1.Merge(dt);
                                    ds1.Tables[0].TableName = "tbl1";
                                    approval = ds1.GetXml();
                                }
                            }*/
                            if (comcod == "3368" & pactcode == "11020099")//Finlay
                            {
                                if (approval == "")
                                {
                                    this.CreateDataTable();
                                    DataTable dt = (DataTable)ViewState["tblapproval"];
                                    DataRow dr1 = dt.NewRow();
                                    dr1["fappid"] = usrid;
                                    dr1["fappdat"] = Date;
                                    dr1["fapptrmid"] = trmnid;
                                    dr1["fappseson"] = session;
                                    dr1["secappid"] = usrid;
                                    dr1["secappdat"] = Date;
                                    dr1["secapptrmid"] = trmnid;
                                    dr1["secappseson"] = session;

                                    dt.Rows.Add(dr1);
                                    ds1.Merge(dt);
                                    ds1.Tables[0].TableName = "tbl1";
                                    approval = ds1.GetXml();
                                }
                            }
                            else
                            {
                                break;
                                /*
                                xmlSR = new System.IO.StringReader(approval);
                                ds1.ReadXml(xmlSR);
                                ds1.Tables[0].TableName = "tbl1";
                                ds1.Tables[0].Rows[0]["fappid"] = usrid;
                                ds1.Tables[0].Rows[0]["fappdat"] = Date;
                                ds1.Tables[0].Rows[0]["fapptrmid"] = trmnid;
                                ds1.Tables[0].Rows[0]["fappseson"] = session;
                                ds1.Tables[0].Rows[0]["secappid"] = "";
                                ds1.Tables[0].Rows[0]["secappdat"] = "";
                                ds1.Tables[0].Rows[0]["secapptrmid"] = "";
                                ds1.Tables[0].Rows[0]["secappseson"] = "";

                                approval = ds1.GetXml();
                                */
                            }

                            break;

                        default:
                            if (approval == "")
                            {
                                this.CreateDataTable();
                                DataTable dt = (DataTable)ViewState["tblapproval"];
                                DataRow dr1 = dt.NewRow();
                                dr1["fappid"] = usrid;
                                dr1["fappdat"] = Date;
                                dr1["fapptrmid"] = trmnid;
                                dr1["fappseson"] = session;
                                dr1["secappid"] = usrid;
                                dr1["secappdat"] = Date;
                                dr1["secapptrmid"] = trmnid;
                                dr1["secappseson"] = session;
                                dt.Rows.Add(dr1);
                                ds1.Merge(dt);
                                ds1.Tables[0].TableName = "tbl1";
                                approval = ds1.GetXml();
                            }
                            else
                            {
                                xmlSR = new System.IO.StringReader(approval);
                                ds1.ReadXml(xmlSR);
                                ds1.Tables[0].TableName = "tbl1";
                                ds1.Tables[0].Rows[0]["fappid"] = usrid;
                                ds1.Tables[0].Rows[0]["fappdat"] = Date;
                                ds1.Tables[0].Rows[0]["fapptrmid"] = trmnid;
                                ds1.Tables[0].Rows[0]["fappseson"] = session;
                                ds1.Tables[0].Rows[0]["secappid"] = usrid;
                                ds1.Tables[0].Rows[0]["secappdat"] = Date;
                                ds1.Tables[0].Rows[0]["secapptrmid"] = trmnid;
                                ds1.Tables[0].Rows[0]["secappseson"] = session;
                                approval = ds1.GetXml();

                            }
                            break;
                    }
                    break;

                case "FirstApp":
                    switch (comcod)
                    {
                        //case "3101": // ptl
                        case "3355": // grenwood
                            string sappusridg = "";
                            string sapptrmnidg = "";
                            string sappsessiong = "";
                            string sappDateg = "";

                            List<RealEntity.C_14_Pro.EClassPur.EClassOrderRange> lst2 = (List<RealEntity.C_14_Pro.EClassPur.EClassOrderRange>)Session["tblordrange"];

                            bool forardg = ((CheckBox)this.gvOrderInfo.FooterRow.FindControl("lblfchkbox")).Checked ? true : false;
                            double toamtg = Convert.ToDouble(((Label)this.gvOrderInfo.FooterRow.FindControl("lblgvFooterTOrderAmt")).Text.ToString());
                            string sslnumg = "";
                            foreach (RealEntity.C_14_Pro.EClassPur.EClassOrderRange lst1 in lst2)
                            {
                                string slnumg = lst1.slnum;
                                double minamtg = lst1.minamt;
                                double maxamtg = lst1.maxamt;
                                if (toamtg > minamtg && toamtg <= maxamtg)
                                {
                                    sslnumg = slnumg;
                                }

                            }
                            string fslnumg = lst2[0].slnum.ToString();
                            // First Approval
                            if (sslnumg == fslnumg)
                            {
                                if (forardg == true)
                                    ;
                                else
                                {

                                    sappusridg = hst["usrid"].ToString();
                                    sapptrmnidg = hst["compname"].ToString();
                                    sappsessiong = hst["session"].ToString();
                                    sappDateg = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                                }
                            }
                            if (approval == "")
                            {
                                this.CreateDataTable();
                                DataTable dt = (DataTable)ViewState["tblapproval"];
                                DataRow dr1 = dt.NewRow();

                                dr1["fappid"] = usrid;
                                dr1["fappdat"] = Date;
                                dr1["fapptrmid"] = trmnid;
                                dr1["fappseson"] = session;
                                dr1["secappid"] = "";
                                dr1["secappdat"] = "";
                                dr1["secapptrmid"] = "";
                                dr1["secappseson"] = "";

                                dt.Rows.Add(dr1);
                                ds1.Merge(dt);
                                ds1.Tables[0].TableName = "tbl1";
                                approval = ds1.GetXml();
                            }
                            else
                            {
                                xmlSR = new System.IO.StringReader(approval);
                                ds1.ReadXml(xmlSR);
                                ds1.Tables[0].TableName = "tbl1";
                                ds1.Tables[0].Rows[0]["fappid"] = usrid;
                                ds1.Tables[0].Rows[0]["fappdat"] = Date;
                                ds1.Tables[0].Rows[0]["fapptrmid"] = trmnid;
                                ds1.Tables[0].Rows[0]["fappseson"] = session;
                                ds1.Tables[0].Rows[0]["secappid"] = "";
                                ds1.Tables[0].Rows[0]["secappdat"] = "";
                                ds1.Tables[0].Rows[0]["secapptrmid"] = "";
                                ds1.Tables[0].Rows[0]["secappseson"] = "";
                                approval = ds1.GetXml();
                            }
                            break;

                        case "3335":
                        case "3354":// Edison Real Estate
                            string sappusrid = "";
                            string sapptrmnid = "";
                            string sappsession = "";
                            string sappDate = "";
                            string sslnum = "";
                            List<RealEntity.C_14_Pro.EClassPur.EClassOrderRange> lst = (List<RealEntity.C_14_Pro.EClassPur.EClassOrderRange>)Session["tblordrange"];
                            bool forard = ((CheckBox)this.gvOrderInfo.FooterRow.FindControl("lblfchkbox")).Checked ? true : false;
                            double toamt = Convert.ToDouble(((Label)this.gvOrderInfo.FooterRow.FindControl("lblgvFooterTOrderAmt")).Text.ToString());
                            foreach (RealEntity.C_14_Pro.EClassPur.EClassOrderRange lst1 in lst)
                            {
                                string slnum = lst1.slnum;
                                double minamt = lst1.minamt;
                                double maxamt = lst1.maxamt;

                                if (toamt > minamt && toamt <= maxamt)
                                {
                                    sslnum = slnum;
                                }
                            }
                            string fslnum = lst[0].slnum.ToString();

                            // First Approval
                            if (sslnum == fslnum)
                            {
                                if (forard == true)
                                    ;
                                else
                                {
                                    sappusrid = hst["usrid"].ToString();
                                    sapptrmnid = hst["compname"].ToString();
                                    sappsession = hst["session"].ToString();
                                    sappDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                                }
                            }
                            if (approval == "")
                            {
                                this.CreateDataTable();
                                DataTable dt = (DataTable)ViewState["tblapproval"];
                                DataRow dr1 = dt.NewRow();

                                dr1["fappid"] = usrid;
                                dr1["fappdat"] = Date;
                                dr1["fapptrmid"] = trmnid;
                                dr1["fappseson"] = session;
                                dr1["secappid"] = sappusrid;
                                dr1["secappdat"] = sappDate;
                                dr1["secapptrmid"] = sapptrmnid;
                                dr1["secappseson"] = sappsession;

                                dt.Rows.Add(dr1);
                                ds1.Merge(dt);
                                ds1.Tables[0].TableName = "tbl1";
                                approval = ds1.GetXml();
                            }
                            else
                            {
                                xmlSR = new System.IO.StringReader(approval);
                                ds1.ReadXml(xmlSR);
                                ds1.Tables[0].TableName = "tbl1";
                                ds1.Tables[0].Rows[0]["fappid"] = usrid;
                                ds1.Tables[0].Rows[0]["fappdat"] = Date;
                                ds1.Tables[0].Rows[0]["fapptrmid"] = trmnid;
                                ds1.Tables[0].Rows[0]["fappseson"] = session;
                                ds1.Tables[0].Rows[0]["secappid"] = sappusrid;
                                ds1.Tables[0].Rows[0]["secappdat"] = sappDate;
                                ds1.Tables[0].Rows[0]["secapptrmid"] = sapptrmnid;
                                ds1.Tables[0].Rows[0]["secappseson"] = sappsession;
                                approval = ds1.GetXml();
                            }
                            break;

                        case "3368":// Finaly Properties Ltd
                            string sappusridf = "";
                            string sapptrmnidf = "";
                            string sappsessionf = "";
                            string sappDatef = "";
                            List<RealEntity.C_14_Pro.EClassPur.EClassOrderRange> lstf = (List<RealEntity.C_14_Pro.EClassPur.EClassOrderRange>)Session["tblordrange"];
                            bool forardf = ((CheckBox)this.gvOrderInfo.FooterRow.FindControl("lblfchkbox")).Checked ? true : false;
                            double toamtf = Convert.ToDouble(((Label)this.gvOrderInfo.FooterRow.FindControl("lblgvFooterTOrderAmt")).Text.ToString());
                            string sslnumf = "";
                            foreach (RealEntity.C_14_Pro.EClassPur.EClassOrderRange lst1 in lstf)
                            {
                                string slnum = lst1.slnum;
                                double minamt = lst1.minamt;
                                double maxamt = lst1.maxamt;

                                if (toamtf > minamt && toamtf <= maxamt)
                                {
                                    sslnum = slnum;
                                }
                            }
                            string fslnumf = lstf[0].slnum.ToString();

                            // First Approval
                            if (sslnumf == fslnumf)
                            {
                                if (forardf == true)
                                    ;
                                else
                                {
                                    sappusridf = hst["usrid"].ToString();
                                    sapptrmnidf = hst["compname"].ToString();
                                    sappsessionf = hst["session"].ToString();
                                    sappDatef = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                                }
                            }
                            if (approval == "")
                            {
                                this.CreateDataTable();
                                DataTable dt = (DataTable)ViewState["tblapproval"];
                                DataRow dr1 = dt.NewRow();

                                dr1["fappid"] = usrid;
                                dr1["fappdat"] = Date;
                                dr1["fapptrmid"] = trmnid;
                                dr1["fappseson"] = session;
                                dr1["secappid"] = sappusridf;
                                dr1["secappdat"] = sappDatef;
                                dr1["secapptrmid"] = sapptrmnidf;
                                dr1["secappseson"] = sappsessionf;

                                dt.Rows.Add(dr1);
                                ds1.Merge(dt);
                                ds1.Tables[0].TableName = "tbl1";
                                approval = ds1.GetXml();
                            }
                            else
                            {
                                xmlSR = new System.IO.StringReader(approval);
                                ds1.ReadXml(xmlSR);
                                ds1.Tables[0].TableName = "tbl1";
                                ds1.Tables[0].Rows[0]["fappid"] = usrid;
                                ds1.Tables[0].Rows[0]["fappdat"] = Date;
                                ds1.Tables[0].Rows[0]["fapptrmid"] = trmnid;
                                ds1.Tables[0].Rows[0]["fappseson"] = session;
                                ds1.Tables[0].Rows[0]["secappid"] = sappusridf;
                                ds1.Tables[0].Rows[0]["secappdat"] = sappDatef;
                                ds1.Tables[0].Rows[0]["secapptrmid"] = sapptrmnidf;
                                ds1.Tables[0].Rows[0]["secappseson"] = sappsessionf;
                                approval = ds1.GetXml();
                            }
                            break;

                        default:

                            if (approval == "")
                            {
                                this.CreateDataTable();
                                DataTable dt = (DataTable)ViewState["tblapproval"];
                                DataRow dr1 = dt.NewRow();

                                dr1["fappid"] = usrid;
                                dr1["fappdat"] = Date;
                                dr1["fapptrmid"] = trmnid;
                                dr1["fappseson"] = session;
                                dr1["secappid"] = usrid;
                                dr1["secappdat"] = Date;
                                dr1["secapptrmid"] = trmnid;
                                dr1["secappseson"] = session;

                                dt.Rows.Add(dr1);
                                ds1.Merge(dt);
                                ds1.Tables[0].TableName = "tbl1";
                                approval = ds1.GetXml();
                            }
                            else
                            {
                                xmlSR = new System.IO.StringReader(approval);
                                ds1.ReadXml(xmlSR);
                                ds1.Tables[0].TableName = "tbl1";
                                ds1.Tables[0].Rows[0]["fappid"] = usrid;
                                ds1.Tables[0].Rows[0]["fappdat"] = Date;
                                ds1.Tables[0].Rows[0]["fapptrmid"] = trmnid;
                                ds1.Tables[0].Rows[0]["fappseson"] = session;
                                ds1.Tables[0].Rows[0]["secappid"] = usrid;
                                ds1.Tables[0].Rows[0]["secappdat"] = Date;
                                ds1.Tables[0].Rows[0]["secapptrmid"] = trmnid;
                                ds1.Tables[0].Rows[0]["secappseson"] = session;
                                approval = ds1.GetXml();
                            }
                            break;

                    }
                    break;
                case "SecondApp":
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["secappid"] = usrid;
                    ds1.Tables[0].Rows[0]["secappdat"] = Date;
                    ds1.Tables[0].Rows[0]["secapptrmid"] = trmnid;
                    ds1.Tables[0].Rows[0]["secappseson"] = session;
                    approval = ds1.GetXml();
                    break;
            }
            return approval;

        }

        protected DateTime GetBackDate()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string entrydate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETBDATEORDER", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                return (System.DateTime.Today);
            }

            return (Convert.ToDateTime(ds2.Tables[0].Rows[0]["bdate"]));
        }


        protected void lbtnUpdatePurOrder_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            this.Session_tblOrder_Update();

            string mORDERDAT = this.GetStdDate(this.txtCurOrderDate.Text.Trim());

            // Back date Entry  only Tropical
            if (comcod == "3339")
            {
                DateTime Bdate;
                Bdate = this.GetBackDate();
                bool dconi = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(mORDERDAT));
                string type1 = this.Request.QueryString["InputType"].ToString().Trim();

                if (type1 == "OrderEntry")
                {
                    if (!dconi)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Purchase Order Entry Only Current Date');", true);
                        return;
                    }
                }
            }

            string mPORDUSRID = "";
            string mAPPRUSRID = "";
            string mSSIRCODE = this.ddlSuplierList.Items.Count > 0 ? this.ddlSuplierList.SelectedValue.ToString() : this.lssircode.Text.Trim();
            string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());
            string mPORDBYDES = this.txtPreparedBy.Text.Trim();
            string mAPPBYDES = this.txtApprovedBy.Text.Trim();
            string mPORDREF = this.txtOrderRefNo.Text.Trim();
            string mLETERDES = this.txtLETDES.Text.Trim();
            string mPORDNAR = this.txtOrderNarr.Text.Trim();
            string subject = this.txtSubject.Text.Trim();
            double AdvAmt = Convert.ToDouble("0" + this.txtadvAmt.Text.Trim());
            //log report
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();

            //end log
            bool result = false;
            //forward Programm
            //Balance Approval
            DataTable tbl1 = (DataTable)ViewState["tblOrder"];
            foreach (DataRow drf in tbl1.Rows)
            {
                bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(drf["aprovdat"].ToString()), Convert.ToDateTime(mORDERDAT));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Date is equal or greater Approved Date');", true);
                    return;
                }
            }
            if (this.ddlPrevOrderList.Items.Count == 0)
                this.GetOrderNo();

            string mORDERNO = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();
            if ((this.Request.QueryString["InputType"].ToString().Trim() == "OrderEntry"))
            {

                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    string mAPROVNO = tbl1.Rows[i]["aprovno"].ToString();
                    string mREQNO = tbl1.Rows[i]["reqno"].ToString();
                    string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                    string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                    DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "EMPTYORDERNO", mAPROVNO, mREQNO, mRSIRCODE, mSPCFCOD, "", "", "", "", "");
                    if (ds1 == null)
                        return;
                    if (ds1.Tables[0].Rows.Count == 0)
                        continue;
                    if (ds1.Tables[0].Rows[0]["orderno"].ToString().Trim() != "")
                    {

                        DataView dv1 = ds1.Tables[0].DefaultView;
                        dv1.RowFilter = ("orderno <>'" + mORDERNO + "'");
                        DataTable dt = dv1.ToTable();
                        if (dt.Rows.Count == 0)
                            ;
                        else
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Materials  already Orderred another order";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }
                    }
                }

                switch (comcod)
                {
                    case "1108":// Assure
                    case "1109":// Assure
                    case "3315":// Assure
                    case "3316":// Assure
                    case "3317":// Assure
                    //case "3101":
                    case "5101":
                    case "3330":// Bridge

                        if (this.lblissueno.Enabled)
                        {
                            this.GetIssueNO();
                            this.lblissueno.Enabled = false;
                        }

                        break;


                }




            }

            double netamt = Convert.ToDouble(((Label)this.gvOrderInfo.FooterRow.FindControl("lblgvFooterTOrderAmt")).Text.ToString());  //(Label)gvOrderInfo.FindControl("lblgvFooterTOrderAmt");


            if (AdvAmt <= netamt)
            { }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Advanced Amount must be equal /less in Total Amount');", true);
                return;
            }



            string issueno = this.lblissueno.Text.Trim();
            string appxml = tbl1.Rows[0]["approval"].ToString();
            string Approval = this.GetReqApproval(appxml);


            bool forwarddesc = ((CheckBox)this.gvOrderInfo.FooterRow.FindControl("lblfchkbox")).Checked ? true : false;
            string type = this.Request.QueryString["InputType"];
            switch (type)
            {
                case "FirstApp":
                    tbl1.Rows[0]["forward"] = forwarddesc;
                    break;

                default:
                    break;
            }


            string terms = "";
            bool istxtTerms;
            switch (comcod)
            {
                case "1205": //p2p
                case "3351":
                case "3352":

                //case "3101":
                case "3366": // lanco
                case "3357": // Cube
                case "3368": // finlay
                case "3370": // cpdl 
                    terms = txtOrderNarrP.Text.Trim().ToString();
                    istxtTerms = false;
                    break;
                default:
                    terms = "";
                    istxtTerms = true;
                    break;
            }

            string forward = (tbl1.Rows[0]["forward"].ToString().Trim().Length == 0) ? "False" : tbl1.Rows[0]["forward"].ToString();
            result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURORDERINFO", "PURORDERB",
                             mORDERNO, mORDERDAT, mSSIRCODE, mPORDUSRID, mAPPRUSRID, mAPPRDAT, mPORDBYDES, mAPPBYDES, mPORDREF, mLETERDES, mPORDNAR, subject, userid, Sessionid, Terminal, AdvAmt.ToString(), issueno, Approval, forward,
                             terms, "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mAPROVNO = tbl1.Rows[i]["aprovno"].ToString();

                bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(tbl1.Rows[i]["aprovdat"].ToString()), Convert.ToDateTime(mORDERDAT));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Date is equal or greater Approved Date');", true);
                    return;
                }
                string mREQNO = tbl1.Rows[i]["reqno"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string SSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                double mAprovqty = Convert.ToDouble(tbl1.Rows[i]["aprovqty"]);
                double mORDRQTY = Convert.ToDouble(tbl1.Rows[i]["ordrqty"]);
                string dispercnt = Convert.ToDouble(tbl1.Rows[i]["dispercnt"]).ToString();
                string rmrks = tbl1.Rows[i]["rmrks"].ToString();



                // string mORDRQTY = tbl1.Rows[i]["ordrqty"].ToString();
                if (mAprovqty < mORDRQTY)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Order Qty Must be Less Or Equal  Approve Qty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURPROPOSAL", mAPROVNO, mREQNO, mRSIRCODE, mSPCFCOD, mSSIRCODE, mORDERNO, mORDRQTY.ToString(), "", "", "", "", "", "", "", "");

                if (mREQNO != "")
                    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURORDERINFO", "PURORDERA",
                             mORDERNO, mAPROVNO, mREQNO, mRSIRCODE, mSPCFCOD, mORDRQTY.ToString(), dispercnt, rmrks, "", "", "", "", "", "", "", "", "", "", "", "");

                else
                {
                    string mPactcode = tbl1.Rows[i]["pactcode"].ToString();
                    string mOrderAmt = Convert.ToDouble(tbl1.Rows[i]["ordramt"]).ToString();

                    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURORDERINFO", "PURORDERE", mORDERNO, mPactcode, mRSIRCODE, "000000000000", mOrderAmt, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                }



                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }

            // todo for p2p terms and conditions in text box
            if (istxtTerms)
            {
                for (int j = 0; j < this.gvOrderTerms.Rows.Count; j++)
                {
                    string mTERMSID = ((Label)this.gvOrderTerms.Rows[j].FindControl("lblgvTermsID")).Text.Trim();
                    string mTERMSSUBJ = ((TextBox)this.gvOrderTerms.Rows[j].FindControl("txtgvSubject")).Text.Trim();
                    string mTERMSDESC = ((TextBox)this.gvOrderTerms.Rows[j].FindControl("txtgvDesc")).Text.Trim();
                    string mTERMSRMRK = ((TextBox)this.gvOrderTerms.Rows[j].FindControl("txtgvRemarks")).Text.Trim();
                    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURORDERINFO", "PURORDERC",
                            mORDERNO, mTERMSID, mTERMSSUBJ, mTERMSDESC, mTERMSRMRK, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                }
            }



            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mAPROVNO = tbl1.Rows[i]["aprovno"].ToString();
                string mREQNO = tbl1.Rows[i]["reqno"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string SSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                string mORDRQTY = tbl1.Rows[i]["ordrqty"].ToString();
                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURAPPROVA", mAPROVNO, mREQNO, mRSIRCODE, SSIRCODE, mORDERNO, mSPCFCOD, "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }


            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {

                string inscode = ((Label)this.gvPayment.Rows[i].FindControl("lblgvschcode")).Text.Trim();
                string desc = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschdesc")).Text.Trim();
                string Date = Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.gvPayment.Rows[i].FindControl("lblnetamt")).Text.Trim())).ToString();
                string ait = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvait")).Text.Trim())).ToString();
                string Remarks = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschrmrks")).Text.Trim();
                string Remarks02 = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschrmrks02")).Text.Trim();




                result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURORDERINFO", "PURORDERD",
                        mORDERNO, inscode, desc, Date, Amt, Remarks, Remarks02, ait, "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

            }

            DataTable dsty = (DataTable)ViewState["tblOrder"];
            this.txtCurOrderDate.Enabled = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            //string projname=dsty.Tables[0].Rows[]
            ViewState["purtermcon"] = null;

            if (hst["compsms"].ToString() == "True")
            {
                switch (comcod)
                {
                    case "3333":
                        break;

                    default:
                        if ((this.Request.QueryString["InputType"].ToString().Trim() == "OrderEntry"))
                        {
                            SendSmsProcess sms = new SendSmsProcess();
                            string comnam = hst["comnam"].ToString();
                            string compname = hst["compname"].ToString();
                            string frmname = "PurMRREntry.aspx?Type=Entry";
                            string SMSHead = "Ready To Recived, ";
                            string SMSText = comnam + ":\n" + SMSHead + "\n" + dsty.Rows[0]["projdesc1"].ToString() + "\n" + "MRF No:" + dsty.Rows[0]["mrfno"].ToString() + "\n" + "to Supplier: " +
                             dsty.Rows[0]["ssirdesc1"].ToString();
                            bool resultsms = sms.SendSmms(SMSText, userid, frmname);
                        }
                        break;
                }
            }

            if (comcod == "3368" || comcod == "3101")
            {

                string pactcode = dsty.Rows[0]["pactcode"].ToString().Substring(0, 4);
                if (pactcode != "1102")
                {
                    try
                    {
                        string empid = "930100101086"; // MD Sir Employee ID  
                        /*  string empid = "930100101005";*/ // MD Sir Employee 
                        var ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETSUPERVISERMAIL", empid, "", "", "", "", "", "", "", "");

                        if (ds1 == null)
                            return;

                        string suserid = ds1.Tables[0].Rows[0]["suserid"].ToString();
                        string tomail = ds1.Tables[0].Rows[0]["mail"].ToString();
                        string idcard = (string)ds1.Tables[1].Rows[0]["idcard"];
                        string project = dsty.Rows[0]["projdesc1"].ToString();
                        string supname = dsty.Rows[0]["ssirdesc1"].ToString();
                        string amount = dsty.Rows[0]["ordramt"].ToString() + " BDT";

                        string uhostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;
                        string currentptah = "/F_14_Pro/PurWrkOrderEntry?InputType=SecondApp&genno=" + mORDERNO + "&comcod=" + comcod + "&usrid=" + suserid;
                        string totalpath = uhostname + currentptah;

                        string maildescription = "Dear Sir, Please check details information <br>" + "<br> Project Name : " + project + ",<br>" + "Supplier Name : " + supname + ",<br>" + "Amount : " + amount + "." + "<br>" +
                             " <br> <br> <br> N.B: This email is system generated. ";

                        maildescription += "<br> <br><div style='color:red'><a style='color:blue; text-decoration:underline' href = '" + totalpath + "'>Click for Approved</a> or Login ERP Software and check Interface</div>" + "<br/>";
                        ///GET SMTP AND SMS API INFORMATION
                        #region
                        string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
                        DataSet dssmtpandmail = purData.GetTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");
                        if (dssmtpandmail == null)
                            return;
                        //SMTP
                        string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
                        int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
                        string frmemail = dssmtpandmail.Tables[0].Rows[0]["mailid"].ToString();
                        string psssword = dssmtpandmail.Tables[0].Rows[0]["mailpass"].ToString();
                        bool isSSL = Convert.ToBoolean(dssmtpandmail.Tables[0].Rows[0]["issl"].ToString());
                        #endregion


                        #region
                        string subj = "Purchase Order";
                        string msgbody = maildescription;

                        bool Result_email = UserNotify.SendEmailPTL(hostname, portnumber, frmemail, psssword, subj, "", "", "", "", tomail, msgbody, isSSL);


                        #endregion
                    }
                    catch (Exception ex)
                    {
                        string Messagesd = "Mail Not Send " + ex.Message;
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                    }
                }

            }

            //if (ConstantInfo.LogStatus == true)
            //{

            //    string text = "Purchase order for Project:" + dsty.Rows[0]["projdesc1"].ToString() + ", Ref No" + dsty.Rows[0]["mrfno"].ToString() + "to Supplier: " +
            //     dsty.Rows[0]["ssirdesc1"].ToString();
            //    sendSmsFromAPI(text);

            //}


        }



        //private void sendSmsFromAPI(string text)
        //{

        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string Sesion = hst["session"].ToString();
        //    string userid = hst["usrid"].ToString();
        //    int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        //    string frmname = HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp);
        //    frmname = frmname.Substring(frmname.LastIndexOf('/') + 1) + "";

        //    DataSet ds3 = purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWAPIINFO", userid, frmname, "", "", "");
        //    if (ds3.Tables[0].Rows.Count == 0)
        //        return;
        //    string user = ds3.Tables[0].Rows[0]["apiusrid"].ToString().Trim(); //"nahid@asit.com.bd";
        //    string pass = ds3.Tables[0].Rows[0]["apipass"].ToString().Trim(); //"asit321";
        //    string routeid = ds3.Tables[0].Rows[0]["apirouid"].ToString().Trim();//3;
        //    string typeid = ds3.Tables[0].Rows[0]["apitypeid"].ToString().Trim();//1;
        //    string sender = ds3.Tables[0].Rows[0]["apisender"].ToString().Trim(); //"ASITNAHID";  //Sender


        //    string SMSText = text; //        
        //    string catname = ds3.Tables[0].Rows[0]["apicatname"].ToString().Trim();//General
        //    string ApiUrl = ds3.Tables[0].Rows[0]["apiurl"].ToString().Trim(); //"http://login.smsnet24.com/apimanager/sendsms?user_id=";
        //    for (int i = 0; i < ds3.Tables[1].Rows.Count; i++)
        //    {
        //        string mobile = "88" + ds3.Tables[1].Rows[i]["phno"].ToString().Trim(); //"880" + "1817610879";//this.txtMob.Text.ToString().Trim();1813934120

        //        //String myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=" + mobile + "&sms[0][1]=" + System.Web.HttpUtility.UrlEncode(SMSText) + "&sms[0][2]=" + "1234567890" + "&sid=" + sender;
        //        //using (WebClient wc = new WebClient())
        //        //{
        //        //    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        //        //    string HtmlResult = wc.UploadString(ApiUrl, myParameters); Console.Write(HtmlResult);
        //        //}
        //        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(ApiUrl + user + "&user_password=" + pass + "&route_id=" + routeid
        //           + "&sms_type_id=" + typeid + "&sms_sender=" + sender + "&sms_receiver=" + mobile + "&sms_text=" + SMSText + "&sms_category_name=" + catname);

        //        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
        //        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
        //        string responseString = respStreamReader.ReadToEnd();
        //        respStreamReader.Close();
        //        myResp.Close();
        //    }


        //}
        protected void lbtnSelectedOrdr_Click(object sender, EventArgs e)
        {

            this.Get_Pur_Order_Info();



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            if (comcod == "3335")
            {
                this.ddltypecod.Visible = true;
                this.lnkselect.Visible = true;

            }
            switch (comcod)
            {
                case "3335":
                    this.ddltypecod.Visible = true;
                    this.lnkselect.Visible = true;
                    this.txtSubject.Text = "Purchase Order For Materials";
                    this.txtLETDES.Text = "Refer to your offer with specification dated on 15/02/2009 and subsequent discussion our management is pleased to issue work order for the following terms &amp; conditions";
                    break;

                case "3364":
                    this.txtSubject.Text = "Purchase Order For ";
                    this.txtLETDES.Text = "This is an reference to your discussion had with us today, we are pleased to place an order for supplying Rmc at our project under the following terms & conditions.";
                    break;


                case "3357":
                    this.txtSubject.Text = "Purchase Order For ";
                    this.txtLETDES.Text = "Thank you very much for cooperating with Cube Holdings Ltd. Against your offer and further discussion we are offering you for the supply of ... under the following terms & condition and rate.";
                    break;

                case "3330":
                    //case "3101":
                    this.txtSubject.Text = "Purchase Order For ";
                    this.txtLETDES.Text = comnam + " " + "Requests you to arrange supply of following materials from your organization.";
                    break;
                default:
                    this.txtSubject.Text = "Purchase Order For ";
                    this.txtLETDES.Text = " Requests you to arrange supply of following materials from your organization.";
                    break;


            }



            DataTable dt1 = (DataTable)ViewState["tblOrder"];
            DataTable dtResP = (DataTable)ViewState["tblResP"];
            int i;
            for (i = 0; i < this.gvAprovInfo.Rows.Count; i++)
            {
                bool chkitm = ((CheckBox)this.gvAprovInfo.Rows[i].FindControl("chkitem")).Checked;

                string PactCode = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvPrjCod11")).Text.Trim();
                string Appno = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvPAPNo")).Text.Trim();
                string Reqno = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvReqNo2")).Text.Trim();
                string Rsircode = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvResCod2")).Text.Trim();
                string Spcfcod = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvspfcod02")).Text.Trim();
                string Ssircode = ((Label)this.gvAprovInfo.Rows[i].FindControl("lblgvSupCod")).Text.Trim();
                if (chkitm == true)
                {


                    DataRow[] dr2 = dtResP.Select("pactcode='" + PactCode + "'and aprovno='" + Appno + " 'and reqno = '" + Reqno + "' and rsircode = '" + Rsircode +
                                    "' and spcfcod='" + Spcfcod + "' and ssircode = '" + Ssircode + "'");
                    if (dr2.Length > 0)
                    {
                        dr2[0]["chk"] = "1";
                    }


                }

                else
                {

                    DataRow[] dr2 = dtResP.Select("pactcode='" + PactCode + "'and aprovno='" + Appno + " 'and reqno = '" + Reqno + "' and rsircode = '" + Rsircode +
                                      "' and spcfcod='" + Spcfcod + "' and ssircode = '" + Ssircode + "'");
                    if (dr2.Length > 0)
                    {
                        dr2[0]["chk"] = "0";
                    }
                }

            }

            string Narration = "";
            string aprovno1 = "00000000000000";
            //string comcod = this.GetCompCode();

            switch (comcod)
            {

                case "1301":
                case "2301":
                case "3301":
                    for (i = 0; i < dtResP.Rows.Count; i++)
                    {


                        string chkitem = dtResP.Rows[i]["chk"].ToString();
                        if (chkitem == "1")
                        {
                            DataRow dr1 = dt1.NewRow();

                            string aprovno = dtResP.Rows[i]["aprovno"].ToString();
                            dr1["aprovno"] = dtResP.Rows[i]["aprovno"];
                            dr1["reqno"] = dtResP.Rows[i]["reqno"];
                            dr1["rsircode"] = dtResP.Rows[i]["rsircode"];
                            dr1["ssircode"] = dtResP.Rows[i]["ssircode"];
                            dr1["spcfcod"] = dtResP.Rows[i]["spcfcod"];
                            dr1["aprovno1"] = dtResP.Rows[i]["aprovno1"];
                            dr1["aprovdat"] = dtResP.Rows[i]["aprovdat"];
                            dr1["reqno1"] = dtResP.Rows[i]["reqno1"];
                            dr1["mrfno"] = dtResP.Rows[i]["mrfno"];
                            dr1["pactcode"] = dtResP.Rows[i]["pactcode"];
                            dr1["projdesc1"] = dtResP.Rows[i]["projdesc1"];
                            dr1["rsirdesc1"] = dtResP.Rows[i]["rsirdesc1"];
                            dr1["ssirdesc1"] = dtResP.Rows[i]["ssirdesc1"];
                            dr1["spcfdesc"] = dtResP.Rows[i]["spcfdesc"];
                            dr1["rsirunit"] = dtResP.Rows[i]["rsirunit"];
                            dr1["aprovqty"] = dtResP.Rows[i]["aprovqty"];
                            dr1["ordrqty"] = dtResP.Rows[i]["aprovqty"];
                            dr1["aprovsrate"] = dtResP.Rows[i]["aprovsrate"];
                            dr1["dispercnt"] = dtResP.Rows[i]["dispercnt"]; ;
                            dr1["aprovrate"] = dtResP.Rows[i]["aprovrate"];

                            dr1["ordramt"] = Convert.ToDouble(dtResP.Rows[i]["aprovqty"]) * Convert.ToDouble(dtResP.Rows[i]["aprovrate"]);
                            dr1["paytype"] = dtResP.Rows[i]["paytype"];
                            dt1.Rows.Add(dr1);
                            if (aprovno1 != aprovno)
                            {
                                Narration = Narration + dtResP.Rows[i]["aprovnar"] + ", ";

                            }
                            aprovno1 = aprovno;

                        }
                    }

                    this.txtOrderNarr.Text = Narration.Substring(0, (Narration.Length) - 2);
                    break;

                case "1108":
                case "1109":
                case "3315":
                case "3316":
                case "3317":

                    for (i = 0; i < dtResP.Rows.Count; i++)
                    {


                        string chkitem = dtResP.Rows[i]["chk"].ToString();
                        if (chkitem == "1")
                        {
                            DataRow dr1 = dt1.NewRow();
                            string aprovno = dtResP.Rows[i]["aprovno"].ToString();
                            dr1["aprovno"] = dtResP.Rows[i]["aprovno"];
                            dr1["reqno"] = dtResP.Rows[i]["reqno"];
                            dr1["rsircode"] = dtResP.Rows[i]["rsircode"];
                            dr1["ssircode"] = dtResP.Rows[i]["ssircode"];
                            dr1["spcfcod"] = dtResP.Rows[i]["spcfcod"];
                            dr1["aprovno1"] = dtResP.Rows[i]["aprovno1"];
                            dr1["aprovdat"] = dtResP.Rows[i]["aprovdat"];
                            dr1["reqno1"] = dtResP.Rows[i]["reqno1"];
                            dr1["mrfno"] = dtResP.Rows[i]["mrfno"];
                            dr1["pactcode"] = dtResP.Rows[i]["pactcode"];
                            dr1["projdesc1"] = dtResP.Rows[i]["projdesc1"];
                            dr1["rsirdesc1"] = dtResP.Rows[i]["rsirdesc1"];
                            dr1["ssirdesc1"] = dtResP.Rows[i]["ssirdesc1"];
                            dr1["spcfdesc"] = dtResP.Rows[i]["spcfdesc"];
                            dr1["rsirunit"] = dtResP.Rows[i]["rsirunit"];
                            dr1["aprovqty"] = dtResP.Rows[i]["aprovqty"];
                            dr1["ordrqty"] = dtResP.Rows[i]["aprovqty"];
                            dr1["aprovsrate"] = dtResP.Rows[i]["aprovsrate"];
                            dr1["dispercnt"] = dtResP.Rows[i]["dispercnt"]; ;
                            dr1["aprovrate"] = dtResP.Rows[i]["aprovrate"];

                            dr1["ordramt"] = Convert.ToDouble(dtResP.Rows[i]["aprovqty"]) * Convert.ToDouble(dtResP.Rows[i]["aprovrate"]);
                            dr1["paytype"] = dtResP.Rows[i]["paytype"];
                            dt1.Rows.Add(dr1);
                            if (aprovno1 != aprovno)
                            {
                                Narration = Narration + dtResP.Rows[i]["reqnar"] + ", ";

                            }
                            aprovno1 = aprovno;

                        }
                    }

                    this.txtOrderNarr.Text = Narration.Substring(0, (Narration.Length) - 2);
                    break;

                case "3101":
                case "3354":
                    for (i = 0; i < dtResP.Rows.Count; i++)
                    {
                        string aprovno = dtResP.Rows[i]["aprovno"].ToString();
                        string chkitem = dtResP.Rows[i]["chk"].ToString();
                        if (chkitem == "1")
                        {
                            DataRow dr1 = dt1.NewRow();
                            dr1["aprovno"] = dtResP.Rows[i]["aprovno"];
                            dr1["reqno"] = dtResP.Rows[i]["reqno"];
                            dr1["rsircode"] = dtResP.Rows[i]["rsircode"];
                            dr1["ssircode"] = dtResP.Rows[i]["ssircode"];
                            dr1["spcfcod"] = dtResP.Rows[i]["spcfcod"];
                            dr1["aprovno1"] = dtResP.Rows[i]["aprovno1"];
                            dr1["aprovdat"] = dtResP.Rows[i]["aprovdat"];
                            dr1["reqno1"] = dtResP.Rows[i]["reqno1"];
                            dr1["mrfno"] = dtResP.Rows[i]["mrfno"];
                            dr1["pactcode"] = dtResP.Rows[i]["pactcode"];
                            dr1["projdesc1"] = dtResP.Rows[i]["projdesc1"];
                            dr1["rsirdesc1"] = dtResP.Rows[i]["rsirdesc1"];
                            dr1["ssirdesc1"] = dtResP.Rows[i]["ssirdesc1"];
                            dr1["spcfdesc"] = dtResP.Rows[i]["spcfdesc"];
                            dr1["rsirunit"] = dtResP.Rows[i]["rsirunit"];
                            dr1["aprovqty"] = dtResP.Rows[i]["aprovqty"];
                            dr1["ordrqty"] = dtResP.Rows[i]["aprovqty"];
                            dr1["aprovsrate"] = dtResP.Rows[i]["aprovsrate"];
                            dr1["dispercnt"] = dtResP.Rows[i]["dispercnt"]; ;
                            dr1["aprovrate"] = dtResP.Rows[i]["aprovrate"];
                            dr1["ordramt"] = Convert.ToDouble(dtResP.Rows[i]["aprovqty"]) * Convert.ToDouble(dtResP.Rows[i]["aprovrate"]);
                            dr1["paytype"] = dtResP.Rows[i]["paytype"];
                            dr1["rowid"] = dtResP.Rows[i]["rowid"];
                            dr1["rmrks"] = dtResP.Rows[i]["rmrks"]; // todo add brand
                            dt1.Rows.Add(dr1);
                            if (aprovno1 != aprovno)
                            {
                                Narration = Narration + dtResP.Rows[i]["reqnar"] + ", ";
                            }
                            aprovno1 = aprovno;
                        }
                    }

                    this.lblreqnaration.Text = "Req Naration : " + Narration.Substring(0, (Narration.Length) - 2);
                    break;
                default:
                    for (i = 0; i < dtResP.Rows.Count; i++)
                    {

                        string aprovno = dtResP.Rows[i]["aprovno"].ToString();
                        string chkitem = dtResP.Rows[i]["chk"].ToString();
                        if (chkitem == "1")
                        {
                            DataRow dr1 = dt1.NewRow();
                            dr1["aprovno"] = dtResP.Rows[i]["aprovno"];
                            dr1["reqno"] = dtResP.Rows[i]["reqno"];
                            dr1["rsircode"] = dtResP.Rows[i]["rsircode"];
                            dr1["ssircode"] = dtResP.Rows[i]["ssircode"];
                            dr1["spcfcod"] = dtResP.Rows[i]["spcfcod"];
                            dr1["aprovno1"] = dtResP.Rows[i]["aprovno1"];
                            dr1["aprovdat"] = dtResP.Rows[i]["aprovdat"];
                            dr1["reqno1"] = dtResP.Rows[i]["reqno1"];
                            dr1["mrfno"] = dtResP.Rows[i]["mrfno"];
                            dr1["pactcode"] = dtResP.Rows[i]["pactcode"];
                            dr1["projdesc1"] = dtResP.Rows[i]["projdesc1"];
                            dr1["rsirdesc1"] = dtResP.Rows[i]["rsirdesc1"];
                            dr1["ssirdesc1"] = dtResP.Rows[i]["ssirdesc1"];
                            dr1["spcfdesc"] = dtResP.Rows[i]["spcfdesc"];
                            dr1["rsirunit"] = dtResP.Rows[i]["rsirunit"];
                            dr1["aprovqty"] = dtResP.Rows[i]["aprovqty"];
                            dr1["ordrqty"] = dtResP.Rows[i]["aprovqty"];
                            dr1["aprovsrate"] = dtResP.Rows[i]["aprovsrate"];
                            dr1["dispercnt"] = dtResP.Rows[i]["dispercnt"]; ;
                            dr1["aprovrate"] = dtResP.Rows[i]["aprovrate"];
                            dr1["ordramt"] = Convert.ToDouble(dtResP.Rows[i]["aprovqty"]) * Convert.ToDouble(dtResP.Rows[i]["aprovrate"]);
                            dr1["paytype"] = dtResP.Rows[i]["paytype"];
                            dr1["rowid"] = dtResP.Rows[i]["rowid"];
                            dt1.Rows.Add(dr1);
                            if (aprovno1 != aprovno)
                            {
                                Narration = Narration + dtResP.Rows[i]["reqnar"] + ", ";

                            }

                            aprovno1 = aprovno;



                        }
                    }

                    this.lblreqnaration.Text = "Req Naration : " + Narration.Substring(0, (Narration.Length) - 2);
                    break;



            }




            //Order Ref
            //  string comcod = this.GetCompCode();
            switch (comcod)
            {

                case "3332":
                    //case "3101":
                    this.txtOrderNarr.Text = "This is narration  ";
                    break;

                case "3330":
                case "5101":
                    //case "3348":



                    var groupedData = (from pactcode in dt1.AsEnumerable()
                                       group pactcode by pactcode.Field<string>("pactcode") into grp
                                       select new
                                       {
                                           pactcode = grp.Key,
                                           Count = grp.Count(),

                                       }).ToList();





                    if (groupedData.Count > 1)
                    {
                        dt1.Clear();

                        ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select only One Project";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;

                    }

                    else
                    {

                        ((Label)this.Master.FindControl("lblmsg")).Visible = false;


                    }


                    //case "3101":
                    //case "3348":

                    this.GetOrRefno(dt1.Rows[0]["pactcode"].ToString());
                    this.GetProConPerson(dt1.Rows[0]["pactcode"].ToString());
                    this.GetPreNarration();
                    break;
                default:
                    break;


            }

            this.MultiView1.ActiveViewIndex = 1;
            this.hideTermsConditions();

            ViewState["tblOrder"] = this.HiddenSameData(dt1);
            this.gvOrderInfo_DataBind();

            this.ShowProjectFiles();

        }

        private void hideTermsConditions()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "1205":
                case "3351":
                case "3352":
                    this.divtermsp2p.Visible = true;
                    this.divterms.Visible = false;
                    //this.ImagePanel.Visible = false;
                    break;

                //case "3101":
                case "3357":
                case "3366": //lanco
                case "3368": // finlay
                case "3370": // cpdl
                    this.divtermsp2p.Visible = true;
                    this.divterms.Visible = false;
                    this.txtOrderNarrP.Text = this.bindDataText();
                    break;

                default:
                    this.divtermsp2p.Visible = false;
                    this.divterms.Visible = true;
                    //this.ImagePanel.Visible = true;
                    break;
            }
        }

        private string bindDataText()
        {
            string comcod = this.GetCompCode();
            string msg = "";
            string date1 = DateTime.Today.ToString("dd.MM.yyyy");
            switch (comcod)
            {
                //case "3101":
                case "3357":
                    msg = "1. Product quality must be ensured on the basis of requirement and as per site count. " +
                        "\n2. Product should be newly produced, fresh and free from cracks and broken edges." +
                        "\n3. Product delivery time must be on time." +
                        "\n4. Payment shall be made by cash/A/C cheque after ………. Days of receipt of all materials in good conditions." +
                        "\n5. Delivery place: at project site " +
                        "\n6. Delivery date: " + date1 +
                        "\n7. Cube Holdings Ltd. has the right to cancel the work order in any time." +
                        "\n8. TDS will be applicable as per TAX ordinance compliance by 3 percent" +
                        "\n9. Please send all bill in duplicate." +
                        "\n10. Contact Person : ";
                    break;

                case "3366":
                    msg = "1. Delivery Place : " +
                        "\n2. Delivery Date : " +
                        "\n3. Contact Person : " +
                        "\n4. Cell Number : " +
                        "\n5. Bill of any supply order against purchase order shall be enclosed with the copy of purchase order and challan detected description of goods. Any discrepancy shall not be accepted." +
                        "\n6. Copy of delivery challan must be signed by proprietor of supplying designation with seal containing name of his organization. " +
                        "\n7. Supply must be completed within 24 hours of any purchase order otherwise the purchase order will be cancelled unless otherwise instructed." +
                        "\n8. Any payment to the supplies more than Tk. 10,000.00 (Taka Ten thousand) will be made through A/c payee cheque." +
                        "\n9. Payment shall have to be received from this office through money receipt of the company." +
                        "\n10. The supplier will be obliged to change the quantity if it is damaged, unspecified and if there is a mismatch in the model according to the purchase order inside the supplied product packet. If not in stock, will be obliged to return the money";
                    break;


                //case "3101":
                case "3368":
                    msg = "1. Please send all your bills in duplicate." +
                        "\n2. Payment shall be made after (30) days of receipt of all materials in good conditions" +
                        "\n3. Company may have the right to alter/change/reject the PO at any time.";
                    break;

                case "3370":
                    msg = "1. Delivery should be made as per sample & specifications." +
                        "\n2. Quantity should be ensured at the time of delivery" +
                        "\n3. Unspecified / bad quality material would be rejected and taken back by the supplier's own cost" +
                        "\n4. Bill to be submitted with receiving challan of respective project." +
                        "\n5. Payment will be made by Account payee Cheque after full delivery." +
                        "\n6. PO No. would be mentioned in all your Invoice & Correspondence.";
                    break;

                default:
                    msg = "";
                    break;
            }

            return msg;
        }

        protected void chkAllfrm_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (((CheckBox)this.gvAprovInfo.HeaderRow.FindControl("chkAllfrm")).Checked)
            {

                for (i = 0; i < this.gvAprovInfo.Rows.Count; i++)
                {
                    ((CheckBox)this.gvAprovInfo.Rows[i].FindControl("chkitem")).Checked = true;
                    //index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    //dt.Rows[i]["chkper"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvAprovInfo.Rows.Count; i++)
                {
                    ((CheckBox)this.gvAprovInfo.Rows[i].FindControl("chkitem")).Checked = false;
                    //index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    //dt.Rows[i]["chkper"] = "False";

                }

            }
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataTable tbl1 = (DataTable)ViewState["tblOrder"];

            string comcod = this.GetCompCode();
            string mORDERNO = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();
            bool result;


            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEORDERNO",
                           mORDERNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mAPROVNO = tbl1.Rows[i]["aprovno"].ToString();
                string mREQNO = tbl1.Rows[i]["reqno"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string SSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UNLINKAPPROVENO",
                         mAPROVNO, mREQNO, mRSIRCODE, "", "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Deleted";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase Order Info";
                string eventdesc = "Update Order";
                string eventdesc2 = mORDERNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }

        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {
            this.pnlschgenerate.Visible = false;
            DataTable dt = (DataTable)ViewState["tblpaysch"];
            int toins = Convert.ToInt32("0" + this.txtTInstall.Text.Trim());
            int incode = 0;
            for (int i = 0; i < toins; i++)
            {
                incode = incode + 1;
                string inscode = incode.ToString().PadLeft(3, '0');
                incode = Convert.ToInt32(inscode);
                DataRow dr = dt.NewRow();

                dr["inscode"] = inscode;
                dr["insdesc"] = "";
                dr["insdate"] = System.DateTime.Today.ToString("dd-MMM-yyyy");
                dr["insamt"] = 0.00;
                dr["amt"] = 0.00;
                dr["ait"] = 0.00;
                dr["aitper"] = 0.00;
                dr["rmrks"] = "";
                dr["rmrks02"] = "";
                dt.Rows.Add(dr);
            }
            ViewState["tblpaysch"] = dt;

            this.chkVisible.Checked = false;
            this.SchData_Bind();

        }


        private void SchData_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblpaysch"];

            this.gvPayment.DataSource = dt;
            this.gvPayment.DataBind();

            if (dt.Rows.Count > 0)
            {

                ((Label)this.gvPayment.FooterRow.FindControl("lblgvfschAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(insamt)", "")) ? 0.00 : dt.Compute("sum(insamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPayment.FooterRow.FindControl("lblgvfait")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ait)", "")) ? 0.00 : dt.Compute("sum(ait)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPayment.FooterRow.FindControl("lblgvfAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
            }


        }
        private void SavePaymentSchdule()
        {

            DataTable dt = (DataTable)ViewState["tblpaysch"];
            //dt.Columns.Add ("aitper", typeof (double));
            //DataRow dr = dt.NewRow ();
            //dr["aitper"] = 0;
            //dt.Rows.Add (dr);


            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {
                string desc = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschdesc")).Text.Trim();
                string Date = Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                double ait = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvait")).Text.Trim()));
                double aitper = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvaitper")).Text.Trim()));

                double Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim()));
                double insamt = Amt - ait;
                ait = ait > 0 ? ait : (Amt * .01 * aitper);
                aitper = ait > 0 ? (ait * 100) / Amt : aitper;

                string Remarks = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschrmrks")).Text.Trim();
                string Remarks02 = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschrmrks02")).Text.Trim();

                dt.Rows[i]["insdesc"] = desc;
                dt.Rows[i]["insdate"] = Date;
                dt.Rows[i]["insamt"] = insamt;
                dt.Rows[i]["amt"] = Amt;
                dt.Rows[i]["ait"] = ait;
                dt.Rows[i]["rmrks"] = Remarks;
                dt.Rows[i]["rmrks02"] = Remarks02;
                dt.Rows[i]["aitper"] = aitper;
                //dt.Rows[i]["aitpercen"] = aitper;
            }

            ViewState["tblpaysch"] = dt;

        }

        protected void lUpdatpayment_Click(object sender, EventArgs e)
        {
            this.SavePaymentSchdule();
            DataTable dt = (DataTable)ViewState["tblpaysch"];
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {
                string desc = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschdesc")).Text.Trim();
                string Date = Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvDate")).Text.Trim()).ToString("dd-MMM-yyyy");
                double Amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAmt")).Text.Trim()));
                string Remarks = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvschrmrks")).Text.Trim();
                dt.Rows[i]["insdesc"] = desc;
                dt.Rows[i]["insdate"] = Date;
                dt.Rows[i]["insamt"] = Amt;
                dt.Rows[i]["rmrks"] = Remarks;
            }

            ViewState["tblpaysch"] = dt;
        }
        protected void lTotalPayment_Click(object sender, EventArgs e)
        {
            this.SavePaymentSchdule();
            this.SchData_Bind();
        }
        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlschgenerate.Visible = this.chkVisible.Checked;
        }
        protected void gvPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void chkCharging_CheckedChanged(object sender, EventArgs e)
        {
            this.PnlCharging.Visible = (chkCharging.Checked);
            this.imgSearchProject_Click(null, null);
            this.imgSearchCharge_Click(null, null);
        }
        protected void imgSearchCharge_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLABOURCHARGE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlCharge.DataTextField = "sirdesc";
            this.ddlCharge.DataValueField = "sircode";
            this.ddlCharge.DataSource = ds1.Tables[0];
            this.ddlCharge.DataBind();
            ds1.Dispose();
        }
        private void TblProject()
        {
            if (ViewState["tblproject"] == null)
            {
                DataTable tblproject = new DataTable();
                tblproject.Columns.Add("pactcode", Type.GetType("System.String"));
                tblproject.Columns.Add("pactdesc", Type.GetType("System.String"));
                ViewState["tblproject"] = tblproject;
            }
        }
        protected void imgSearchProject_Click(object sender, EventArgs e)
        {


            this.TblProject();
            DataTable dt = (DataTable)(DataTable)ViewState["tblOrder"];
            DataTable dt1 = (DataTable)ViewState["tblproject"];
            if (dt.Rows.Count == 0)
            {
                this.ddlProjectName.Items.Clear();
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Pactcode = dt.Rows[i]["pactcode"].ToString();
                DataRow[] dr1 = dt1.Select("pactcode='" + Pactcode + "'");
                if (dr1.Length == 0)
                {
                    DataRow dr2 = dt1.NewRow();
                    dr2["pactcode"] = Pactcode;
                    dr2["pactdesc"] = dt.Rows[i]["projdesc1"].ToString();
                    dt1.Rows.Add(dr2);

                }

            }

            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = dt1;
            this.ddlProjectName.DataBind();
        }
        protected void lbtnSelect_Click(object sender, EventArgs e)
        {

            DataTable tbl1 = (DataTable)ViewState["tblOrder"];

            string mProjectCode = this.ddlProjectName.SelectedValue.ToString();
            string mResCode = this.ddlCharge.SelectedValue.ToString();
            string mSpcfCode = "000000000000";
            DataRow[] dr2 = tbl1.Select("pactcode ='" + mProjectCode + "' and rsircode = '" + mResCode + "' and spcfcod = '" + mSpcfCode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();


                dr1["aprovno"] = "";
                dr1["reqno"] = "";
                dr1["rsircode"] = mResCode;
                dr1["ssircode"] = "";
                dr1["spcfcod"] = mSpcfCode;
                dr1["aprovno1"] = "";
                dr1["aprovdat"] = "01-Jan-1900";
                dr1["reqno1"] = "";
                dr1["mrfno"] = "";
                dr1["pactcode"] = mProjectCode;
                dr1["projdesc1"] = this.ddlProjectName.SelectedItem.ToString();
                dr1["rsirdesc1"] = this.ddlCharge.SelectedItem.ToString(); ;
                dr1["ssirdesc1"] = "";
                dr1["spcfdesc"] = "";
                dr1["rsirunit"] = "";
                dr1["aprovqty"] = 0.00;
                dr1["ordrqty"] = 0.00;
                dr1["aprovsrate"] = 0.00;
                dr1["dispercnt"] = 0.00;
                dr1["aprovrate"] = 0.00;
                dr1["ordramt"] = 0.00;
                dr1["paytype"] = "";
                tbl1.Rows.Add(dr1);
            }






            ViewState["tblOrder"] = this.HiddenSameData(tbl1);
            this.gvOrderInfo_DataBind();
            // this.gvBillInfo_DataBind();
        }


        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblOrder_Update();
            this.gvOrderInfo_DataBind();

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
                    //case "3101":
                    comserial = "Rup";
                    break;

                default:
                    comserial = "";
                    break;


            }
            return comserial;
        }


        private string PrintCallType()
        {


            string comcod = this.GetCompCode();
            string Calltype = "";
            switch (comcod)
            {

                case "3301":
                case "1301":
                case "3330"://Bridge
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
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                if (this.lbtnOk.Text == "Ok")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Plese Select Order No";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    return;

                }

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string wrkid = "POR" + this.txtCurOrderDate.Text.Substring(6, 4) + this.txtCurOrderDate.Text.Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();

                string Calltype = this.PrintCallType();
                string ordercopy = this.GetCompOrderCopy();
                DataSet _ReportDataSet = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", Calltype, wrkid, ordercopy, "", "", "", "", "", "", "");

                DataTable dt = _ReportDataSet.Tables[0];
                string Para1 = _ReportDataSet.Tables[1].Rows[0]["leterdes"].ToString();
                string Orderdate = Convert.ToDateTime(_ReportDataSet.Tables[1].Rows[0]["orderdat"]).ToString("MMMM  dd, yyyy");
                string SupName = _ReportDataSet.Tables[1].Rows[0]["ssirdesc"].ToString();
                string Address = _ReportDataSet.Tables[1].Rows[0]["address"].ToString();
                string Cperson = _ReportDataSet.Tables[1].Rows[0]["cperson"].ToString();
                string Phone = _ReportDataSet.Tables[1].Rows[0]["phone"].ToString();
                string mobile = _ReportDataSet.Tables[1].Rows[0]["mobile"].ToString();




                DataTable dtterm = _ReportDataSet.Tables[2];

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



                ///-----/////
                ///



                ////

                //only Rupayan group 



                string tmflatno = (dtterm.Rows.Count <= 6) ? "" : ((comname == "Rup") ? "6. " + dtterm.Rows[6]["termssubj"].ToString() + " : " : "*" + dtterm.Rows[6]["termssubj"].ToString() + " : ");  //("* " + dtterm.Rows[6]["termssubj"].ToString().Trim() + " : ");
                string flatno1 = (dtterm.Rows.Count <= 6) ? "" : (dtterm.Rows[6]["termsdesc"].ToString().Trim());
                string tmflatower = (dtterm.Rows.Count <= 6) ? "" : ((comname == "Rup") ? "6. " + dtterm.Rows[7]["termssubj"].ToString() + " : " : "*" + dtterm.Rows[7]["termssubj"].ToString() + " : "); //("* " + dtterm.Rows[7]["termssubj"].ToString().Trim() + " : ");
                string flatower = (dtterm.Rows.Count <= 6) ? "" : (dtterm.Rows[7]["termsdesc"].ToString().Trim());
                string tmthersflat = (dtterm.Rows.Count <= 6) ? "" : ((comname == "Rup") ? "7. " + dtterm.Rows[8]["termssubj"].ToString() + " : " : "*" + dtterm.Rows[8]["termssubj"].ToString() + " : "); // ("* " + dtterm.Rows[8]["termssubj"].ToString().Trim() + " : ");
                string otherflats = (dtterm.Rows.Count <= 6) ? "" : (dtterm.Rows[8]["termsdesc"].ToString().Trim());
                string tmcontact = (dtterm.Rows.Count <= 6) ? "" : ((comname == "Rup") ? "8. " + dtterm.Rows[9]["termssubj"].ToString() + " : " : "*" + dtterm.Rows[9]["termssubj"].ToString() + " : "); // ("* " + dtterm.Rows[9]["termssubj"].ToString().Trim() + " : ");
                string contact = (dtterm.Rows.Count <= 6) ? "" : (dtterm.Rows[9]["termsdesc"].ToString().Trim());





                DataTable dtorder = (DataTable)ViewState["tblOrder"];
                DataTable dt1;
                DataTable dt2;
                DataTable dt3;


                // Carring
                DataView dv1 = dtorder.DefaultView;
                dv1.RowFilter = ("rsircode  like '019999901%'");
                dt1 = dv1.ToTable();

                //Deduction
                dv1 = dtorder.DefaultView;
                dv1.RowFilter = ("rsircode like'019999902%'");
                dt2 = dv1.ToTable();

                //Material
                dv1 = dtorder.DefaultView;
                dv1.RowFilter = ("rsircode not like '0199999%'");
                dt3 = dv1.ToTable();


                double amtcar = (dt1.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(ordramt)", "")) ? 0.00 : dt1.Compute("Sum(ordramt)", "")));
                double amtdis = (dt2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(ordramt)", "")) ? 0.00 : dt2.Compute("Sum(ordramt)", "")));
                //



                double amtmat = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(ordramt)", "")) ? 0.00 : dt3.Compute("Sum(ordramt)", "")));
                double advamt = Convert.ToDouble("0" + this.txtadvAmt.Text);

                switch (comcod)
                {

                    case "3305":  //Rupayan group
                    case "3306":
                    case "3309":
                    case "3310":
                    case "3311":
                    case "2305":
                    case "2306":
                    case "3101":

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
                        TextObject txtPhoneNumber = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber.Text = Phone;



                        break;


                    case "3336":
                    case "3337":
                        //case"3101":

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
                        txtAdvancedSuv.Text = advamt.ToString("#,##0.00;(#,##0.00); ");
                        TextObject txtappbySuv = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappbySuv.Text = "Approved By";

                        TextObject txtmoblieNumber = rptwork.ReportDefinition.ReportObjects["txtmoblieNumber"] as TextObject;
                        txtmoblieNumber.Text = mobile;
                        TextObject txtconcernperson = rptwork.ReportDefinition.ReportObjects["txtconcernperson"] as TextObject;
                        txtconcernperson.Text = (cperson.Length > 0) ? cperson : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection3"].SectionFormat.EnableSuppress = (cperson.Length > 0) ? false : true;

                        //txtappby.Text = (comcod == "3335") ? "Head of Procurement" : "Approved By";

                        // sign end 
                        break;


                    case "3338":
                        // case"3336":
                        //rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderAcme2();

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
                        rpttxtcheckAcme.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        TextObject txtfaprname = rptwork.ReportDefinition.ReportObjects["txtfaprname"] as TextObject;
                        txtfaprname.Text = _ReportDataSet.Tables[3].Rows[0]["faprname"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["faprdat"].ToString();

                        TextObject txtAdvancedAcme = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvancedAcme.Text = Convert.ToDouble("0" + this.txtadvAmt.Text).ToString("#,##0.00;(#,##0.00); ");
                        //TextObject txtappbyAcme = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        //txtappbyAcme.Text = "Approved By";
                        TextObject txtPhoneNumber1 = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber1.Text = Phone;

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

                        TextObject txtconcernperson1 = rptwork.ReportDefinition.ReportObjects["txtconcernperson"] as TextObject;
                        txtconcernperson1.Text = (cperson.Length > 0) ? cperson : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection3"].SectionFormat.EnableSuppress = (cperson.Length > 0) ? false : true;

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
                        txtAdvancedb.Text = Convert.ToDouble("0" + this.txtadvAmt.Text).ToString("#,##0.00;(#,##0.00); ");
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
                        txtAdvancedtro.Text = Convert.ToDouble("0" + this.txtadvAmt.Text).ToString("#,##0.00;(#,##0.00); ");
                        TextObject txtappbytro = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappbytro.Text = "Approved By";

                        TextObject txtPhoneNumber2tro = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber2tro.Text = Phone;
                        TextObject txtconcernperson3tro = rptwork.ReportDefinition.ReportObjects["txtconcernperson"] as TextObject;
                        txtconcernperson3tro.Text = (cperson.Length > 0) ? cperson : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection3"].SectionFormat.EnableSuppress = (cperson.Length > 0) ? false : true;

                        break;


                    //case "3336":
                    case "1103":// Tanvir
                    case "1101":// Tanvir
                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderTanvir();


                        //Sign In


                        TextObject rpttxtReqtan = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReqtan.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqApptan = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqApptan.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrdtan = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrdtan.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWordtan = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWordtan.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtchecktan = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
                        rpttxtchecktan.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        TextObject txtAdvancedtan = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvancedtan.Text = Convert.ToDouble("0" + this.txtadvAmt.Text).ToString("#,##0.00;(#,##0.00); ");
                        TextObject txtappbytan = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappbytan.Text = "Approved By";

                        TextObject txtPhoneNumber2tan = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber2tan.Text = Phone;
                        TextObject txtconcernperson3tan = rptwork.ReportDefinition.ReportObjects["txtconcernperson"] as TextObject;
                        txtconcernperson3tan.Text = (cperson.Length > 0) ? cperson : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection3"].SectionFormat.EnableSuppress = (cperson.Length > 0) ? false : true;

                        TextObject txtnetamt = rptwork.ReportDefinition.ReportObjects["txtnetamt"] as TextObject;
                        txtnetamt.Text = (amtmat + amtcar - amtdis - advamt).ToString("#,##0.00;(#,##0.00); ");
                        //  txtnettotal.Text = (amtmat + amtcar - amtdis).ToString("#,##0.00;(#,##0.00);");

                        break;

                    case "3335":
                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderEdison();
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
                        txtAdvanced.Text = Convert.ToDouble("0" + this.txtadvAmt.Text).ToString("#,##0.00;(#,##0.00); ");

                        TextObject txtappbyeed = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappbyeed.Text = _ReportDataSet.Tables[3].Rows[0]["ordappnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["ordappdat"].ToString(); ;



                        TextObject txtPhoneNumber2 = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber2.Text = Phone;
                        TextObject txtconcernperson3 = rptwork.ReportDefinition.ReportObjects["txtconcernperson"] as TextObject;
                        txtconcernperson3.Text = (cperson.Length > 0) ? cperson : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection3"].SectionFormat.EnableSuppress = (cperson.Length > 0) ? false : true;

                        //txtappby.Text = (comcod == "3335") ? "Head of Procurement" : "Approved By";

                        // sign end 
                        break;



                    case "3315": // Assure
                    case "3316":// Assure
                                //case "3101":
                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderAssure();

                        TextObject txtAdvancedass = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvancedass.Text = Convert.ToDouble("0" + this.txtadvAmt.Text).ToString("#,##0.00;(#,##0.00); ");  //avdamt; //ToString("#,##0.00;(#,##0.00);");
                        rptwork.ReportDefinition.Sections["GroupFooterSection3"].SectionFormat.EnableSuppress = ((_ReportDataSet.Tables[6]).Rows.Count > 0) ? false : true;
                        rptwork.Subreports["RptOrderPaymentAssure.rpt"].SetDataSource(_ReportDataSet.Tables[6]);

                        break;




                    default:

                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderBridge();

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
                        txtAdvancede.Text = Convert.ToDouble("0" + this.txtadvAmt.Text).ToString("#,##0.00;(#,##0.00); ");
                        TextObject txtappbye = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappbye.Text = "Approved By";

                        TextObject txtPhoneNumber2e = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber2e.Text = Phone;
                        TextObject txtconcernperson3e = rptwork.ReportDefinition.ReportObjects["txtconcernperson"] as TextObject;
                        txtconcernperson3e.Text = (cperson.Length > 0) ? cperson : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection3"].SectionFormat.EnableSuppress = (cperson.Length > 0) ? false : true;

                        //txtappby.Text = (comcod == "3335") ? "Head of Procurement" : "Approved By";

                        // sign end 
                        break;


                }




                TextObject txtsubject = rptwork.ReportDefinition.ReportObjects["txtsubject"] as TextObject;
                txtsubject.Text = this.txtSubject.Text;
                TextObject txtCompany = rptwork.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
                txtCompany.Text = comnam;
                TextObject txtAddress = rptwork.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
                txtAddress.Text = comadd;
                TextObject rptpurno = rptwork.ReportDefinition.ReportObjects["purno"] as TextObject;
                rptpurno.Text = this.ComOrderNo();
                TextObject rptRefno = rptwork.ReportDefinition.ReportObjects["refno"] as TextObject;
                rptRefno.Text = this.txtOrderRefNo.Text;
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

                rptwork.ReportDefinition.Sections["GroupFooterSection5"].SectionFormat.EnableSuppress = (((DataTable)ViewState["tblpaysch"]).Rows.Count > 0) ? false : true;


                TextObject rpttxtsupplydetails = rptwork.ReportDefinition.ReportObjects["txtsupplydetails"] as TextObject;
                rpttxtsupplydetails.Text = this.txtOrderNarr.Text.Trim(); ;
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


                //if(comcod=="2325"||comcod=="3325")
                //{
                //    TextObject rptFlatOwner1 = rptwork.ReportDefinition.ReportObjects["flatOwner"] as TextObject;
                //    rptFlatOwner1.Text = (tmflatower.Length > 0) ? tmflatower + flatower : "";

                //    rptwork.ReportDefinition.Sections["GroupFooterSection3"].SectionFormat.EnableSuppress = (tmflatower.Length > 0) ? false : true;

                //}
                //else
                //{
                //    TextObject txtconcernperson = rptwork.ReportDefinition.ReportObjects["txtconcernperson"] as TextObject;
                //    txtconcernperson.Text = (cperson.Length > 0) ? cperson : "";
                //    rptwork.ReportDefinition.Sections["GroupFooterSection3"].SectionFormat.EnableSuppress = (cperson.Length > 0) ? false : true;

                //}




                TextObject rptOthrs = rptwork.ReportDefinition.ReportObjects["others"] as TextObject;
                rptOthrs.Text = (Others.Length > 0) ? trmothers + Others : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection14"].SectionFormat.EnableSuppress = (Others.Length > 0) ? false : true;













                TextObject txtcarcost = rptwork.ReportDefinition.ReportObjects["txtcarcost"] as TextObject;
                txtcarcost.Text = amtcar.ToString("#,##0.00;(#,##0.00);");

                TextObject txtdiscount = rptwork.ReportDefinition.ReportObjects["txtdiscount"] as TextObject;
                txtdiscount.Text = amtdis.ToString("#,##0.00;(#,##0.00);");
                TextObject txtnettotal = rptwork.ReportDefinition.ReportObjects["txtnettotal"] as TextObject;
                txtnettotal.Text = (amtmat + amtcar - amtdis).ToString("#,##0.00;(#,##0.00);");


                double netamt;

                switch (comcod)
                {

                    case "1103":
                    case "1101":
                        netamt = amtmat + amtcar - amtdis - advamt;
                        break;

                    default:
                        netamt = amtmat + amtcar - amtdis;
                        break;




                }

                TextObject txtkword = rptwork.ReportDefinition.ReportObjects["txtkword"] as TextObject;
                txtkword.Text = "In Word: " + ASTUtility.Trans(netamt, 2);
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

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Materials Purchase Order Info";
                    string eventdesc = "Print Order";
                    string eventdesc2 = " Request No:- " + this.lblCurOrderNo1.Text.Trim() + this.txtCurOrderNo2.Text; ;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rptwork.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rptwork;

                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        private void AutoSavePDF()
        {


            try
            {
                //((Label)this.Master.FindControl("lblmsg")).Visible = true;
                if (this.lbtnOk.Text == "Ok")
                {
                    string Messagesd = "Plese Select Order No";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                    return;
                }

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string wrkid = "POR" + this.txtCurOrderDate.Text.Substring(6, 4) + this.txtCurOrderDate.Text.Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();

                string Calltype = this.PrintCallType();
                string ordercopy = this.GetCompOrderCopy();
                DataSet _ReportDataSet = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", Calltype, wrkid, ordercopy, "", "", "", "", "", "", "");

                DataTable dt = _ReportDataSet.Tables[0].Copy();
                // txtsrchSupplier Coppy
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("grp='A'");
                dt = dv.ToTable();
                string Para1 = _ReportDataSet.Tables[1].Rows[0]["leterdes"].ToString();
                string Orderdate = Convert.ToDateTime(_ReportDataSet.Tables[1].Rows[0]["orderdat"]).ToString("MMMM  dd, yyyy");
                string SupName = _ReportDataSet.Tables[1].Rows[0]["ssirdesc"].ToString();
                string Address = _ReportDataSet.Tables[1].Rows[0]["address"].ToString();
                string Cperson = _ReportDataSet.Tables[1].Rows[0]["cperson"].ToString();
                string Phone = _ReportDataSet.Tables[1].Rows[0]["phone"].ToString();
                string mobile = _ReportDataSet.Tables[1].Rows[0]["mobile"].ToString();
                string pcontact = _ReportDataSet.Tables[1].Rows[0]["pcontact"].ToString();
                string supemail = _ReportDataSet.Tables[1].Rows[0]["supemail"].ToString();




                DataTable dtterm = _ReportDataSet.Tables[2];
                // string Type = this.CompanyPrintWorkOrder();
                ReportDocument rptwork = new ReportDocument();
                string fax = "";
                string comname = "";
                string trmplace = "";
                string place = "";
                string trmpdate = "";
                string pdate = "";
                string trmcarring = "";
                string carring = "";
                string trmbill = "";
                string bill = "";
                string trmpayment = "";
                string payment = "";
                string trmothers = "";
                string Others = "";
                if (dtterm.Rows.Count != 0)
                {
                    fax = _ReportDataSet.Tables[1].Rows[0]["fax"].ToString();
                    comname = this.Compserial();
                    trmplace = ((comcod == "3338" || comname == "Rup") ? "1. " + dtterm.Rows[0]["termssubj"].ToString() : "*" + dtterm.Rows[0]["termssubj"].ToString() + " : ");
                    place = dtterm.Rows[0]["termsdesc"].ToString().Trim();
                    trmpdate = ((comcod == "3338" || comname == "Rup") ? "2. " + dtterm.Rows[1]["termssubj"].ToString() : "*" + dtterm.Rows[1]["termssubj"].ToString() + " : ");
                    pdate = dtterm.Rows[1]["termsdesc"].ToString().Trim();
                    trmcarring = ((comcod == "3338" || comname == "Rup") ? "3. " + dtterm.Rows[2]["termssubj"].ToString() : "*" + dtterm.Rows[2]["termssubj"].ToString() + " : ");
                    carring = dtterm.Rows[2]["termsdesc"].ToString().Trim();
                    trmbill = (comcod == "3330") ? "" : (comcod == "3338" || comname == "Rup") ? "4. " + (dtterm.Rows[3]["termssubj"]).ToString() : "*" + dtterm.Rows[3]["termssubj"].ToString() + ": ";
                    bill = (comcod == "3330") ? ("* " + dtterm.Rows[3]["termsdesc"].ToString().Trim()) : dtterm.Rows[3]["termsdesc"].ToString().Trim();
                    trmpayment = ((comcod == "3338") ? dtterm.Rows[4]["termssubj"].ToString() : (comname == "Rup") ? "4. " + dtterm.Rows[4]["termssubj"].ToString() + " : " : "*" + dtterm.Rows[4]["termssubj"].ToString() + " : ");
                    payment = dtterm.Rows[4]["termsdesc"].ToString().Trim();
                    trmothers = ((comcod == "3338") ? dtterm.Rows[5]["termssubj"].ToString() : (comname == "Rup") ? "5. " + dtterm.Rows[5]["termssubj"].ToString() + " : " : "*" + dtterm.Rows[5]["termssubj"].ToString() + " : ");
                    Others = dtterm.Rows[5]["termsdesc"].ToString().Trim();
                }


                // For Acme



                string trmcperson = ((dtterm.Rows.Count == 0) ? "" : dtterm.Select("termsid='010'").Length > 0 ? "* " + (dtterm.Select("termsid='010'")[0]["termssubj"]).ToString() + " : " : "");
                string cperson = ((dtterm.Rows.Count == 0) ? "" : dtterm.Select("termsid='010'").Length > 0 ? (dtterm.Select("termsid='010'")[0]["termsdesc"]).ToString() : ""); ;

                //only Rupayan group 



                string tmflatno = (dtterm.Rows.Count <= 6) ? "" : ((comname == "Rup") ? "6. " + dtterm.Rows[6]["termssubj"].ToString() + " : " : "*" + dtterm.Rows[6]["termssubj"].ToString() + " : ");  //("* " + dtterm.Rows[6]["termssubj"].ToString().Trim() + " : ");
                string flatno1 = (dtterm.Rows.Count <= 6) ? "" : (dtterm.Rows[6]["termsdesc"].ToString().Trim());
                string tmflatower = (dtterm.Rows.Count <= 6) ? "" : ((comname == "Rup") ? "6. " + dtterm.Rows[7]["termssubj"].ToString() + " : " : "*" + dtterm.Rows[7]["termssubj"].ToString() + " : "); //("* " + dtterm.Rows[7]["termssubj"].ToString().Trim() + " : ");
                string flatower = (dtterm.Rows.Count <= 6) ? "" : (dtterm.Rows[7]["termsdesc"].ToString().Trim());
                string tmthersflat = (dtterm.Rows.Count <= 6) ? "" : ((comname == "Rup") ? "7. " + dtterm.Rows[8]["termssubj"].ToString() + " : " : "*" + dtterm.Rows[8]["termssubj"].ToString() + " : "); // ("* " + dtterm.Rows[8]["termssubj"].ToString().Trim() + " : ");
                string otherflats = (dtterm.Rows.Count <= 6) ? "" : (dtterm.Rows[8]["termsdesc"].ToString().Trim());
                string tmcontact = (dtterm.Rows.Count <= 6) ? "" : ((comname == "Rup") ? "8. " + dtterm.Rows[9]["termssubj"].ToString() + " : " : "*" + dtterm.Rows[9]["termssubj"].ToString() + " : "); // ("* " + dtterm.Rows[9]["termssubj"].ToString().Trim() + " : ");
                string contact = (dtterm.Rows.Count <= 6) ? "" : (dtterm.Rows[9]["termsdesc"].ToString().Trim());

                switch (comcod)
                {
                    case "3305":  //Rupayan group
                    case "3306":
                    case "3309":
                    case "3310":
                    case "3311":
                    case "2305":
                    case "2306":
                    

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
                        TextObject txtPhoneNumber = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber.Text = Phone;

                        break;

                    case "3336":
                    case "3337":
                        //case "3101":

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
                        txtAdvancedSuv.Text = Convert.ToDouble("0" + this.txtadvAmt.Text).ToString("#,##0.00;(#,##0.00); ");
                        TextObject txtappbySuv = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappbySuv.Text = "Approved By";
                        TextObject txtmoblieNumber = rptwork.ReportDefinition.ReportObjects["txtmoblieNumber"] as TextObject;
                        txtmoblieNumber.Text = mobile;
                        //txtappby.Text = (comcod == "3335") ? "Head of Procurement" : "Approved By";

                        // sign end 
                        break;


                    case "3338":
                        //case"3101":



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
                        rpttxtcheckAcme.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        TextObject txtAdvancedAcme = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvancedAcme.Text = Convert.ToDouble("0" + this.txtadvAmt.Text).ToString("#,##0.00;(#,##0.00); ");
                        TextObject txtappbyAcme = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappbyAcme.Text = "Approved By";
                        TextObject txtPhoneNumber1 = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber1.Text = Phone;

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
                        txtAdvanced.Text = Convert.ToDouble("0" + this.txtadvAmt.Text).ToString("#,##0.00;(#,##0.00); ");
                        TextObject txtappby = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappby.Text = (comcod == "3335") ? "Head of Procurement" : "Approved By";
                        TextObject txtPhoneNumber2e = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber2e.Text = Phone;
                        TextObject txtperson = rptwork.ReportDefinition.ReportObjects["txtperson"] as TextObject;
                        txtperson.Text = Cperson;
                        TextObject txtsupemail = rptwork.ReportDefinition.ReportObjects["txtsupemail"] as TextObject;
                        txtsupemail.Text = supemail;

                        TextObject txtcontact = rptwork.ReportDefinition.ReportObjects["txtcontact"] as TextObject;
                        txtcontact.Text = pcontact;

                        // sign end 
                        break;


                }




                TextObject txtsubject = rptwork.ReportDefinition.ReportObjects["txtsubject"] as TextObject;
                txtsubject.Text = this.txtSubject.Text;
                TextObject txtCompany = rptwork.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
                txtCompany.Text = comnam;
                TextObject txtAddress = rptwork.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
                txtAddress.Text = comadd;
                TextObject rptpurno = rptwork.ReportDefinition.ReportObjects["purno"] as TextObject;
                rptpurno.Text = this.ComOrderNo();
                TextObject rptRefno = rptwork.ReportDefinition.ReportObjects["refno"] as TextObject;
                rptRefno.Text = this.txtOrderRefNo.Text;
                TextObject supname = rptwork.ReportDefinition.ReportObjects["supname"] as TextObject;
                supname.Text = SupName;
                TextObject Supadd = rptwork.ReportDefinition.ReportObjects["saddress"] as TextObject;
                Supadd.Text = Address;
                //TextObject txtPhoneNumber = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                //txtPhoneNumber.Text = Phone;
                //TextObject Fax = rptwork.ReportDefinition.ReportObjects["txtfax"] as TextObject;
                //Fax.Text =  fax;
                TextObject rptpurdate = rptwork.ReportDefinition.ReportObjects["txtOrderDate"] as TextObject;
                rptpurdate.Text = Orderdate;
                TextObject rptPara1 = rptwork.ReportDefinition.ReportObjects["TxtLETERDES"] as TextObject;
                rptPara1.Text = Para1;
                TextObject rptplace = rptwork.ReportDefinition.ReportObjects["place"] as TextObject;
                rptplace.Text = (place.Length > 0) ? trmplace + place : "";

                rptwork.ReportDefinition.Sections["GroupFooterSection5"].SectionFormat.EnableSuppress = (((DataTable)ViewState["tblpaysch"]).Rows.Count > 0) ? false : true;


                TextObject rpttxtsupplydetails = rptwork.ReportDefinition.ReportObjects["txtsupplydetails"] as TextObject;
                rpttxtsupplydetails.Text = this.txtOrderNarr.Text.Trim(); ;
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



                TextObject txtconcernperson = rptwork.ReportDefinition.ReportObjects["txtconcernperson"] as TextObject;
                txtconcernperson.Text = (cperson.Length > 0) ? cperson : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection3"].SectionFormat.EnableSuppress = (cperson.Length > 0) ? false : true;



                TextObject rptOthrs = rptwork.ReportDefinition.ReportObjects["others"] as TextObject;
                rptOthrs.Text = (Others.Length > 0) ? trmothers + Others : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection14"].SectionFormat.EnableSuppress = (Others.Length > 0) ? false : true;





                DataTable dtorder = (DataTable)ViewState["tblOrder"];
                DataTable dt1;
                DataTable dt2;
                DataTable dt3;


                // Carring
                DataView dv1 = dtorder.DefaultView;
                dv1.RowFilter = ("rsircode  like '019999901%'");
                dt1 = dv1.ToTable();

                //Deduction
                dv1 = dtorder.DefaultView;
                dv1.RowFilter = ("rsircode like'019999902%'");
                dt2 = dv1.ToTable();

                //Material
                dv1 = dtorder.DefaultView;
                dv1.RowFilter = ("rsircode not like '0199999%'");
                dt3 = dv1.ToTable();


                double amtcar = (dt1.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(ordramt)", "")) ? 0.00 : dt1.Compute("Sum(ordramt)", "")));
                double amtdis = (dt2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(ordramt)", "")) ? 0.00 : dt2.Compute("Sum(ordramt)", "")));
                //



                double amtmat = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(ordramt)", "")) ? 0.00 : dt3.Compute("Sum(ordramt)", "")));

                TextObject txtcarcost = rptwork.ReportDefinition.ReportObjects["txtcarcost"] as TextObject;
                txtcarcost.Text = amtcar.ToString("#,##0.00;(#,##0.00);");

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

                rptwork.SetDataSource(dt);
                rptwork.Subreports["RptOrderPaymentSch.rpt"].SetDataSource((DataTable)ViewState["tblpaysch"]);

                // report.OpenSubReport(nameOfTheSubReport).SetDataSo urce(secondDataSet);

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Materials Purchase Order Info";
                    string eventdesc = "Print Order";
                    string eventdesc2 = " Request No:- " + this.lblCurOrderNo1.Text.Trim() + this.txtCurOrderNo2.Text; ;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rptwork.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rptwork;



                string mORDERNO = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();

                string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf"; ;



                //string OrderSend = @"E:\" + mORDERNO + ".pdf";



                //


                rptwork.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, apppath);

                apppath = "../SupWorkOreder/" + mORDERNO + ".pdf";
                this.ifrmanPdf.Src = apppath;

                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            }
            catch (Exception ex)
            {
                string Messagesd = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                //return;
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
            //    string wrkid = "POR" + this.txtCurOrderDate.Text.Substring(6, 4) + this.txtCurOrderDate.Text.Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();

            //    //string Calltype = this.PrintCallType();
            //    DataSet _ReportDataSet = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "SHOWORKORDER01", wrkid, "", "", "", "", "", "", "", "");

            //    DataTable dt = _ReportDataSet.Tables[0];

            //    DataTable dtA = dt.Copy();
            //    DataView dv = dtA.DefaultView;
            //    dv.RowFilter = "grp='A'";
            //    dtA = dv.ToTable();


            //    string Para1 = _ReportDataSet.Tables[1].Rows[0]["leterdes"].ToString();
            //    string Orderdate = Convert.ToDateTime(_ReportDataSet.Tables[1].Rows[0]["orderdat"]).ToString("MMMM  dd, yyyy");
            //    string SupName = _ReportDataSet.Tables[1].Rows[0]["ssirdesc"].ToString();
            //    string Address = _ReportDataSet.Tables[1].Rows[0]["address"].ToString();
            //    string Cperson = _ReportDataSet.Tables[1].Rows[0]["cperson"].ToString();
            //    string Phone = _ReportDataSet.Tables[1].Rows[0]["phone"].ToString();

            //    DataTable dtterm = _ReportDataSet.Tables[2];

            //    string Type = this.CompanyPrintWorkOrder();
            //    ReportDocument rptwork = new ReportDocument();


            //    string fax = "";

            //    string trmplace = "";
            //    string place = "";
            //    string trmpdate = "";
            //    string pdate = "";
            //    string trmcarring = "";
            //    string carring = "";

            //    string bill = "";
            //    string trmpayment = "";
            //    string payment = "";
            //    string trmothers = "";
            //    string Others = "";

            //    if (dtterm.Rows.Count != 0)
            //    {
            //        fax = _ReportDataSet.Tables[1].Rows[0]["fax"].ToString();
            //        trmplace = "* " + dtterm.Rows[0]["termssubj"].ToString() + " : ";
            //        place = dtterm.Rows[0]["termsdesc"].ToString().Trim();
            //        trmpdate = "* " + dtterm.Rows[1]["termssubj"].ToString() + " : ";
            //        pdate = dtterm.Rows[1]["termsdesc"].ToString().Trim();
            //        trmcarring = "* " + dtterm.Rows[2]["termssubj"].ToString() + " : ";
            //        carring = dtterm.Rows[2]["termsdesc"].ToString().Trim();
            //        //string trmbill = "* " + dtterm.Rows[9]["termssubj"].ToString() + "";
            //        bill = "* " + dtterm.Rows[3]["termsdesc"].ToString().Trim();
            //        trmpayment = "* " + dtterm.Rows[4]["termssubj"].ToString() + " : ";
            //        payment = dtterm.Rows[4]["termsdesc"].ToString().Trim();

            //        trmothers = "* " + dtterm.Rows[5]["termssubj"].ToString() + " : ";
            //        Others = dtterm.Rows[5]["termsdesc"].ToString().Trim();

            //    }




            //    rptwork = new RealERPRPT.R_14_Pro.rptWorkOrder02();

            //    TextObject txtsubject = rptwork.ReportDefinition.ReportObjects["txtsubject"] as TextObject;
            //    txtsubject.Text = this.txtSubject.Text;
            //    TextObject txtCompany = rptwork.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
            //    txtCompany.Text = comnam;
            //    TextObject txtAddress = rptwork.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            //    txtAddress.Text = comadd;

            //    TextObject rptpurno = rptwork.ReportDefinition.ReportObjects["purno"] as TextObject;
            //    rptpurno.Text = this.lblCurOrderNo1.Text.Trim() + this.txtCurOrderNo2.Text;
            //    TextObject rptRefno = rptwork.ReportDefinition.ReportObjects["refno"] as TextObject;
            //    rptRefno.Text = this.txtOrderRefNo.Text;
            //    TextObject supname = rptwork.ReportDefinition.ReportObjects["supname"] as TextObject;
            //    supname.Text = SupName;
            //    TextObject Supadd = rptwork.ReportDefinition.ReportObjects["saddress"] as TextObject;
            //    Supadd.Text = Address;
            //    TextObject txtPhoneNumber = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
            //    txtPhoneNumber.Text = Phone;
            //    //TextObject Fax = rptwork.ReportDefinition.ReportObjects["txtfax"] as TextObject;
            //    //Fax.Text = fax;
            //    TextObject rptpurdate = rptwork.ReportDefinition.ReportObjects["txtOrderDate"] as TextObject;
            //    rptpurdate.Text = Orderdate;
            //    TextObject rptPara1 = rptwork.ReportDefinition.ReportObjects["TxtLETERDES"] as TextObject;
            //    rptPara1.Text = Para1;
            //    TextObject rptplace = rptwork.ReportDefinition.ReportObjects["place"] as TextObject;
            //    rptplace.Text = (place.Length > 0) ? trmplace + place : "";

            //    rptwork.ReportDefinition.Sections["GroupFooterSection5"].SectionFormat.EnableSuppress = (((DataTable)ViewState["tblpaysch"]).Rows.Count > 0) ? false : true;


            //    TextObject rpttxtsupplydetails = rptwork.ReportDefinition.ReportObjects["txtsupplydetails"] as TextObject;
            //    rpttxtsupplydetails.Text = this.txtOrderNarr.Text.Trim(); ;
            //    rptwork.ReportDefinition.Sections["GroupFooterSection9"].SectionFormat.EnableSuppress = (place.Length > 0) ? false : true;


            //    TextObject rptpdate = rptwork.ReportDefinition.ReportObjects["pdate"] as TextObject;
            //    rptpdate.Text = (pdate.Length > 0) ? trmpdate + pdate : "";
            //    rptwork.ReportDefinition.Sections["GroupFooterSection10"].SectionFormat.EnableSuppress = (pdate.Length > 0) ? false : true;


            //    TextObject rptcarring = rptwork.ReportDefinition.ReportObjects["carring"] as TextObject;
            //    rptcarring.Text = (carring.Length > 0) ? trmcarring + carring : "";
            //    rptwork.ReportDefinition.Sections["GroupFooterSection11"].SectionFormat.EnableSuppress = (carring.Length > 0) ? false : true;


            //    TextObject rptpbill = rptwork.ReportDefinition.ReportObjects["bill"] as TextObject;
            //    rptpbill.Text = (bill.Length > 0) ? bill : "";
            //    rptwork.ReportDefinition.Sections["GroupFooterSection12"].SectionFormat.EnableSuppress = (bill.Length > 0) ? false : true;

            //    TextObject rptpayment1 = rptwork.ReportDefinition.ReportObjects["payment1"] as TextObject;
            //    rptpayment1.Text = (payment.Length > 0) ? trmpayment + payment : "";
            //    rptwork.ReportDefinition.Sections["GroupFooterSection13"].SectionFormat.EnableSuppress = (payment.Length > 0) ? false : true;


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

            //    rptwork.SetDataSource(dtA);
            //    rptwork.Subreports["RptOrderPaymentSch.rpt"].SetDataSource((DataTable)ViewState["tblpaysch"]);

            //    // report.OpenSubReport(nameOfTheSubReport).SetDataSo urce(secondDataSet);

            //    if (ConstantInfo.LogStatus == true)
            //    {
            //        string eventtype = "Materials Purchase Order Info";
            //        string eventdesc = "Print Order";
            //        string eventdesc2 = " Request No:- " + this.lblCurOrderNo1.Text.Trim() + this.txtCurOrderNo2.Text; ;
            //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //    }

            //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //    rptwork.SetParameterValue("ComLogo", ComLogo);
            //    Session["Report1"] = rptwork;


            //    string mORDERNO = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();

            //    string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf"; ;



            //    //string OrderSend = @"E:\" + mORDERNO + ".pdf";



            //    //


            //    rptwork.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, apppath);


            //}
            //catch (Exception ex)
            //{

            //    string Messagesd = "Error:" + ex.Message;
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
            //    //return;
            //}



        }

        protected void lnkSendMail_Click(object sender, EventArgs e)
        {
            string OrderNo = this.Request.QueryString["genno"] ?? "";
            Response.Redirect("~/F_99_Allinterface/PurchasePrint?Type=OrderPrint&orderno=" + OrderNo + "&Orderstatus=Download");
        }
        

        private void SendNotificaion(string compsms, string compmail, string ssl, string compName)
        {
            try
            {
                //this.AutoSavePDF();
                string comcod = this.GetCompCode();


                ///GET SMTP AND SMS API INFORMATION
                #region
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string sendUsername = hst["userfname"].ToString();
                string sendDptdesc = hst["dptdesc"].ToString();
                string sendUsrdesig = hst["usrdesig"].ToString();
                string usrid = hst["usrid"].ToString();
                string deptcode = hst["deptcode"].ToString();


                DataSet dssmtpandmail = purData.GetTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");
                if (dssmtpandmail == null)
                    return;
                //SMTP
                string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
                int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
                string frmemail = dssmtpandmail.Tables[0].Rows[0]["mailid"].ToString();
                string psssword = dssmtpandmail.Tables[0].Rows[0]["mailpass"].ToString();
                bool isSSL = Convert.ToBoolean(dssmtpandmail.Tables[0].Rows[0]["issl"].ToString());
                #endregion

                #region
                // get data
                string mORDERNO = this.lblPONO.Text.ToString();

                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPUREMAIL", mORDERNO, "", "", "", "", "", "", "", "");
                if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                {
                    string Messagesd = "Purchase order didn't save";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                    return;
                }
                string subj = "Purchase Order";
                string tomail = ds1.Tables[0].Rows[0]["mailid"].ToString();
                string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf";

                if(tomail=="")
                {
                    string Messagesd = "Email Send Fail !!!  Update the suppliers email address to be used for email notifications";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                    return;
                }


                string msgbody = @"
<html lang=""en"">
	<head>	
		<meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">
		<title>
			Work Order
		</title>
		<style type=""text/css"">
			HTML{background-color: #e8e8e8;}
			.courses-table{font-size: 12px; padding: 3px; border-collapse: collapse; border-spacing: 0;}
			.courses-table .description{color: #505050;}
			.courses-table td{border: 1px solid #D1D1D1; background-color: #F3F3F3; padding: 0 10px;}
			.courses-table th{border: 1px solid #424242; color: #FFFFFF;text-align: left; padding: 0 10px;}
			.green{background-color: #6B9852;}
.badge-success {
    color: #fff;
    background-color: #44cf9c;
}
.badge-pink {
    color: #fff;
    background-color: #f672a7;
}
.badge-warning {
    color: #fff;
    background-color: #fcc015;
}
.badge-info {
    color: #fff;
    background-color: #43bee1;
}
.text-danger {
    color:red;
    font-weight:bold;
}
.badge-danger {
    color: #fff;
    background-color: #f672a7;
}
.badge-success {
    color: #fff;
    background-color: #44cf9c;
}
.badge {
    display: inline-block;
    padding: 0.25em 0.4em;
    font-size: 75%;
    font-weight: 700;
    line-height: 1;
    text-align: center;
    white-space: nowrap;
    vertical-align: baseline;
    border-radius: 0.25rem;
    transition: color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out;
}
		</style>
	</head>
	<body>
<p>Dear Sir/ Madam,</p>
<p>Hope you are having a good day. Please check the attached file for your kind consideration.</p>

<p></br>It is a pleasure to do business with an esteemed company such as yours and we hope to continue working together in the future.</p>
<p></br></p>
<p>Best Regards,</p>
<p>" + sendUsername + "</p><p> " + sendUsrdesig + " </p><p> " + compName + " </p></body></html>";
                #endregion



                if (compmail == "True")
                {
                    bool Result_email = this.SendEmailPTLSUP(hostname, portnumber, frmemail, psssword, subj, sendUsername, sendUsrdesig, sendDptdesc, compName, tomail, msgbody, isSSL, apppath);
                    if (Result_email == false)
                    {
                        string Messagesd = "Email has not been sent, Email or SMTP info Empty";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                        return;
                    }
                    else
                    {
                        string Messagesd = "Email has been sent";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagesd + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                string Messagesd = "Email has not been sent " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                return;

            }

        }

        public bool SendEmailPTLSUP(string hostname, int portnumber, string frmemail, string psssword, string subj, string sendUsername, string sendUsrdesig, string sendDptdesc, string compName, string tomail, string msgbody, bool isSSL, string apppath)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                SmtpClient client = new SmtpClient(hostname, portnumber);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = isSSL;
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(apppath);
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.From = new MailAddress(frmemail);
                string body = string.Empty;
                msg.To.Add(new MailAddress(tomail));
               
                /// msg.CC.Add(new MailAddress("ibrahim.diu26@gmail.com"));
                //msg.Bcc.Add(new MailAddress("nahid@pintechltd.com"));
                msg.Subject = subj;
                body += msgbody;
                // body += "<br />Thanks & Regards<br/>" + sendUsername + "<br>" + sendUsrdesig + "<br>" + sendDptdesc + "<br>" + compName;
                msg.Body = body;
                msg.Attachments.Add(attachment);
                msg.IsBodyHtml = true;
                try
                {
                    client.Send(msg);
                    return true;
                }
                catch (Exception ex)
                {
                    this.SetError(ex);
                    return false;

                }

            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }


        }


        private void SendNormalMail()
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //string comcod = this.GetCompCode();
            //string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
            //DataSet dssmtpandmail = this.purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");


            //string mORDERNO = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();

            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPUREMAIL", mORDERNO, "", "", "", "", "", "", "", "");

            //string subject = "Work Order";
            ////SMTP
            //string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            //int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());

            //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(hostname, portnumber);
            ////SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            ////client.EnableSsl = true;
            //client.EnableSsl = false;
            //string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
            //string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
            //System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
            //client.UseDefaultCredentials = false;
            //client.Credentials = credentials;

            /////////////////////////
            /////
            //System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            //msg.From = new System.Net.Mail.MailAddress(frmemail);

            //msg.To.Add(new System.Net.Mail.MailAddress(ds1.Tables[0].Rows[0]["mailid"].ToString()));
            //msg.Subject = subject;
            //msg.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;

            //string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf"; ;

            //attachment = new System.Net.Mail.Attachment(apppath);
            //msg.Attachments.Add(attachment);



            //msg.Body = string.Format("<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Dear Sir," + "<br/>" + "please find attached file" + "</pre></body></html>");
            //try
            //{
            //    client.Send(msg);

            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Your message has been successfully sent.";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


            //    //string savelocation = Server.MapPath("~") + "\\SupWorkOreder";
            //    //string[] filePaths = Directory.GetFiles(savelocation);
            //    //foreach (string filePath in filePaths)
            //    //    File.Delete(filePath);

            //}
            //catch (Exception ex)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //}
        }
        private void SendSSLMail()
        {

            //try
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //    string comcod = this.GetCompCode();
            //    string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
            //    DataSet dssmtpandmail = this.purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");


            //    string mORDERNO = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();

            //    DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPUREMAIL", mORDERNO, "", "", "", "", "", "", "", "");
            //    if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            //    {
            //        string Messagesd = "Purchase order didn't save";
            //        ((Label)this.Master.FindControl("lblmsg")).Text = Messagesd;
            //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //        return;
            //    }
            //    string subject = "Work Order";
            //    //SMTP
            //    string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            //    int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
            //    string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
            //    string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
            //    string mailtousr = ds1.Tables[0].Rows[0]["mailid"].ToString();
            //    string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf";


            //    EASendMail.SmtpMail oMail = new EASendMail.SmtpMail("TryIt");

            //    //Connection Details 
            //    SmtpServer oServer = new SmtpServer(hostname);
            //    oServer.User = frmemail;
            //    oServer.Password = psssword;
            //    oServer.Port = portnumber;
            //    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            //    //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;


            //    EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
            //    oMail.From = frmemail;
            //    oMail.To = mailtousr;
            //    oMail.Cc = frmemail;
            //    oMail.Subject = subject;


            //    oMail.HtmlBody = "<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Dear Sir," + "<br/>" + "please find attached file" + "</pre></body></html>";
            //    oMail.AddAttachment(apppath);


            //    //System.Net.Mail.Attachment attachment;

            //    //attachment = new System.Net.Mail.Attachment(apppath);
            //    //oMail.AddAttachment(attachment);





            //    try
            //    {

            //        oSmtp.SendMail(oServer, oMail);
            //        ((Label)this.Master.FindControl("lblmsg")).Text = "Your message has been successfully sent.";

            //    }
            //    catch (Exception ex)
            //    {
            //        ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;
            //}


        }
        protected void gvOrderInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Session_tblOrder_Update();
            this.gvOrderInfo.PageIndex = e.NewPageIndex;
            this.gvOrderInfo_DataBind();
        }

        private void ShowProjectFiles()
        {
            ViewState.Remove("tblimages");
            string comcod = this.GetCompCode();
            string orderno = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURORDERFILES", orderno, "", "", "", "", "", "", "");

            ViewState["tblimages"] = ds1.Tables[0];
            ListViewEmpAll.DataSource = ds1.Tables[0];
            ListViewEmpAll.DataBind();

        }
        protected void btnDelall_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblimages"];
            for (int j = 0; j < this.ListViewEmpAll.Items.Count; j++)
            {
                string orderno = ((Label)this.ListViewEmpAll.Items[j].FindControl("orderno")).Text.ToString();
                string filesname = ((Label)this.ListViewEmpAll.Items[j].FindControl("ImgLink")).Text.ToString();
                if (((CheckBox)this.ListViewEmpAll.Items[j].FindControl("ChDel")).Checked == true)
                {
                    DataRow dr = dt.Rows[j];
                    dr.Delete();
                    DataSet ds1 = new DataSet("ds1");
                    ds1.Tables.Add(dt);
                    ds1.Tables[0].TableName = "tbl1";
                    bool result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURORDERIMG", ds1, null, null, orderno, "", "", "", "", "", "", "", "", "", "", "", "");

                    if (result == true)
                    {
                        string filePath = Server.MapPath("~/");
                        System.IO.File.Delete(filePath + filesname.Replace("~", ""));
                        this.lblMesg.Text = " Files Removed ";
                        this.ShowProjectFiles();
                    }
                }

            }

        }
        protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                System.Web.UI.WebControls.Image imgname = (System.Web.UI.WebControls.Image)e.Item.FindControl("GetImg");
                Label imglink = (Label)e.Item.FindControl("ImgLink");
                string extension = Path.GetExtension(imglink.Text.ToString());
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":
                        imgname.ImageUrl = imglink.Text.ToString();
                        break;
                    case ".PDF":
                    case ".pdf":
                        imgname.ImageUrl = "~/Images/pdf.png";
                        break;
                    case ".xls":
                    case ".xlsx":
                        imgname.ImageUrl = "~/Images/excel.svg";
                        break;
                    case ".doc":
                    case ".docx":
                        imgname.ImageUrl = "~/Images/word.png";
                        break;
                }
            }
            //if (e.Item.ItemType == ListViewItemType.DataItem)
            //{
            //    System.Web.UI.WebControls.Image imgname = (System.Web.UI.WebControls.Image)e.Item.FindControl("GetImg");
            //    Label imglink = (Label)e.Item.FindControl("ImgLink");
            //    string extension = Path.GetExtension(imglink.Text.ToString());
            //    switch (extension)
            //    {
            //        case ".PNG":
            //        case ".png":
            //        case ".JPEG":
            //        case ".JPG":
            //        case ".jpg":
            //        case ".jpeg":
            //        case ".GIF":
            //        case ".gif":
            //            imgname.ImageUrl = imglink.Text.ToString();
            //            break;
            //    }

            //}

        }
        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblimages"];
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            string orderno = "";
            if (AsyncFileUpload1.HasFile)
            {
                orderno = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();

                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/purorder/") + orderno + random + extension);

                // Url = Server.MapPath("~/Upload/purorder/") + orderno + random + extension;
                Url = "~/Upload/purorder/" + orderno + random + extension;
                //  Url = Url.Substring(0, (Url.Length - 1));
                dt.Rows.Add(comcod, orderno, Url);
            }

            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt);
            ds1.Tables[0].TableName = "tbl1";
            bool result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURORDERIMG", ds1, null, null, orderno, "", "", "", "", "");

            if (result == true)
            {
                this.lblMesg.Text = " Successfully Updated ";
                this.ShowProjectFiles();

            }
            else
            {
                string filePath = Server.MapPath("~/");
                System.IO.File.Delete(filePath + Url.Replace("~", ""));
            }


        }
        protected void lnkselect_Click(object sender, EventArgs e)
        {

            DataTable dt = ((DataTable)ViewState["purtermcon"]).Copy();
            string typecod = this.ddltypecod.SelectedValue;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "typecod='" + typecod + "'";

            this.gvOrderTerms.DataSource = dv.ToTable();
            this.gvOrderTerms.DataBind();
        }
        protected void lnkAddTerms_Click(object sender, EventArgs e)
        {
            this.bindTermsintoGrid();

            DataTable dt = ((DataTable)ViewState["purtermcon"]).Copy();
            //string comcod = this.GetCompCode();

            // string mOrderNo = "NEWORDER";

            //if (this.ddlPrevOrderList.Items.Count > 0)
            //{

            //    mOrderNo = this.ddlPrevOrderList.SelectedValue.ToString();
            //}



            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETNEWTERMSONDITIONS", mOrderNo, "",   
            //          "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;

            //ViewState["NewTerms"] = ds1.Tables[0];
            //DataTable dt1 = ((DataTable)ViewState["NewTerms"]);  
            //if (dt1.Rows.Count > 0)
            int count = dt.Rows.Count - 1;
            int termsid = Convert.ToInt32(dt.Rows[count]["termsid"].ToString());
            termsid++;
            string stermsid = ASTUtility.Right("000" + termsid.ToString(), 3);

            //termsid, termssubj,   termsdesc, termsrmrk,termsdesc1


            DataRow dr1 = dt.NewRow();
            dr1["termsid"] = stermsid;
            dr1["termssubj"] = "";
            dr1["termsdesc"] = "";
            dr1["termsrmrk"] = "";
            dr1["termsdesc"] = "";
            dt.Rows.Add(dr1);
            ViewState["purtermcon"] = dt;
            this.gvOrderTerms.DataSource = (DataTable)ViewState["purtermcon"];
            this.gvOrderTerms.DataBind();

        }


        private void bindTermsintoGrid()
        {
            DataTable dt = (DataTable)ViewState["purtermcon"];

            for (int j = 0; j < this.gvOrderTerms.Rows.Count; j++)
            {
                string mTERMSID = ((Label)this.gvOrderTerms.Rows[j].FindControl("lblgvTermsID")).Text.Trim();
                string mTERMSSUBJ = ((TextBox)this.gvOrderTerms.Rows[j].FindControl("txtgvSubject")).Text.Trim();
                string mTERMSDESC = ((TextBox)this.gvOrderTerms.Rows[j].FindControl("txtgvDesc")).Text.Trim();
                string mTERMSRMRK = ((TextBox)this.gvOrderTerms.Rows[j].FindControl("txtgvRemarks")).Text.Trim();

                dt.Rows[j]["termsid"] = mTERMSID;
                dt.Rows[j]["termssubj"] = mTERMSSUBJ;
                dt.Rows[j]["termsdesc"] = mTERMSDESC;
                dt.Rows[j]["termsrmrk"] = mTERMSRMRK;
                dt.AcceptChanges();
            }
            ViewState["purtermcon"] = dt;
        }



        //protected void btnDelTerms_Click(object sender, GridView e)
        //{

        //    string mOrderNo = "NEWORDER";

        //    if (this.ddlPrevOrderList.Items.Count > 0)
        //    {

        //        mOrderNo = this.ddlPrevOrderList.SelectedValue.ToString();
        //    }

        //    DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETNEWTERMSONDITIONS", "", "",
        //           "", "", "", "", "", "", "");


        //}








        protected void lnkSedningEmail_Click(object sender, EventArgs e)
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string compsms = hst["compsms"].ToString();
                string compmail = hst["compmail"].ToString();
                string ssl = hst["ssl"].ToString();
                string comcod = hst["comcod"].ToString();
                string sendUsername = hst["userfname"].ToString();

                string sendDptdesc = hst["dptdesc"].ToString();
                string sendUsrdesig = hst["usrdesig"].ToString();
                string compName = hst["comnam"].ToString();

                string usrid = hst["usrid"].ToString();
                string deptcode = hst["deptcode"].ToString();
                this.SendNotificaion(compsms, compmail, ssl, compName);
            }
            catch (Exception ex)
            {

                string Messagesd = "Error occured while sending your message." + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
            }
        }

        protected void lnkSendEmail_Click(object sender, EventArgs e)
        {
            AutoSavePDF();

            string mORDERNO = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();
            this.lblPONO.Text = mORDERNO;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }


        protected void lbtndelterm_Click(object sender, EventArgs e)
        {

            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string termsid = ((DataTable)ViewState["purtermcon"]).Rows[index]["termsid"].ToString();
            string comcod = this.GetCompCode();
            string mOrderNo = "NEWORDER";
            if (this.ddlPrevOrderList.Items.Count > 0)
            {

                mOrderNo = this.ddlPrevOrderList.SelectedValue.ToString();
            }

            if (mOrderNo != "NEWORDER")
            {
                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETETERMSANDCONDITIONS",
                  "", mOrderNo, termsid, "", "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Failed";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                else
                {

                    ((DataTable)ViewState["purtermcon"]).Rows[index].Delete();
                    ((DataTable)ViewState["purtermcon"]).AcceptChanges();
                    gvOrderTerms.DataSource = (DataTable)ViewState["purtermcon"];
                    gvOrderTerms.DataBind();

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                }
            }

            else
            {

                ((DataTable)ViewState["purtermcon"]).Rows[index].Delete();
                ((DataTable)ViewState["purtermcon"]).AcceptChanges();
                gvOrderTerms.DataSource = (DataTable)ViewState["purtermcon"];
                gvOrderTerms.DataBind();

                ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


            }

        }
        private void SetError(Exception exp)
        {
            this._errObj["Src"] = exp.Source;
            this._errObj["Msg"] = exp.Message;
            this._errObj["Location"] = exp.StackTrace;
        }

        
    }
}
