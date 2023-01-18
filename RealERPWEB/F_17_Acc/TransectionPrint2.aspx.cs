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
namespace RealERPWEB.F_17_Acc
{

    public partial class TransectionPrint2 : System.Web.UI.Page
    {
        public static double TAmount;
        ProcessAccess AccData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                //RadioButtonList2.Items.Remove("AccCheque");
                //RadioButtonList2.Items.Remove("Post Dated Cheque Print");
                //RadioButtonList2.Items.Remove("Pay Slip");

                RadioButtonList2.SelectedIndex = 0;
                RadioButtonList2_SelectedIndexChanged(null, null);



                //string Type = Request.QueryString["Type"].Trim();
                //((Label)this.Master.FindControl("lblTitle")).Text = (Type == "AccVoucher" ? "Voucher print" : (Type == "AccCheque" ? "Cheque Print" : "Post Dated Cheque Print")) + " Information ";
                //this.Master.Page.Title = (Type == "AccVoucher" ? "voucher print" : (Type == "AccCheque" ? "Cheque Print" : "Post Dated Cheque Print")) + " Information ";
                //this.SetView();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetView();
            string value = this.RadioButtonList2.SelectedValue.ToString();

            switch (value)
            {
                case "AccVoucher":
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Voucher Print";
                    this.RadioButtonList2.Items[0].Attributes["class"] = "lblactive blink_me";
                    break;
                case "AccCheque":
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Cheque Print";
                    this.RadioButtonList2.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                case "AccPostDatChq":
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Post Dated Cheque Print";
                    this.RadioButtonList2.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;
                //case "AccVoucherCan":
                //    ((Label)this.Master.FindControl("lblTitle")).Text = "Voucher Cancellation";
                //    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                //    break;
                case "Payslip":
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Pay Slip";
                    this.RadioButtonList2.Items[3].Attributes["class"] = "lblactive blink_me";
                    break;


            }
        }
        private void SetView()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Type = this.RadioButtonList2.SelectedValue.ToString();
            switch (Type)
            {
                case "AccVoucher":
                    this.rbtCprintList.SelectedIndex = 0;
                    this.txtfromdate.Text = (comcod == "3336" || comcod == "3337") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy"); ;
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.lstVouname.Visible = this.lstVouname.Items.Count > 0;
                    this.MultiView1.ActiveViewIndex = 0;
                    ((Label)this.Master.FindControl("lblmsg")).Text = "";
                    this.chqno.Visible = true;
                    this.paye.Visible = false;
                    //this.lnkbtnDelVoucher.Visible = false;
                    break;

                case "AccCheque":
                    this.rbtCprintList.SelectedIndex = 0;
                    this.CompanyCheckPrint();
                    this.txtfromdate.Text = (comcod == "3336" || comcod == "3337") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    this.GetVouNum();
                    this.chqno.Visible = false;
                    this.paye.Visible = true;
                    break;

                case "AccPostDatChq":
                    this.rbtCprintList.SelectedIndex = 0;
                    //this.CompanyCheckPrint();
                    this.txtfromdate.Text = (comcod == "3336" || comcod == "3337") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 2;
                    this.PostDatChqGetVouNum();
                    this.chqno.Visible = false;
                    this.paye.Visible = true;
                    break;

                case "Payslip":
                    this.rbtCprintList.SelectedIndex = 0;
                    this.CompanyCheckPrint();
                    this.txtfromdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    this.GetVouNum();
                    this.chqno.Visible = false;
                    this.paye.Visible = false;
                    break;


            }

        }
        protected void lnkbtnVouOk_Click(object sender, EventArgs e)
        {
            RadioButtonList2_SelectedIndexChanged(null, null);
            string Type = this.RadioButtonList2.SelectedValue.ToString(); //Request.QueryString["Type"].Trim();
            switch (Type)
            {
                case "AccVoucher":
                    this.printVou();
                    break;
                case "AccCheque":
                case "Payslip":
                    this.lnkbtnChkOk_Click(null, null);
                    break;
                case "AccPostDatChq":
                    this.btnPostDatChqOk_Click(null, null);
                    break;
            }
        }

        private void printVou()
        {
            Session.Remove("tblvoucher");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            int index1 = Convert.ToInt32(this.rbtnList1.SelectedIndex);
            string Voutype = ((index1 == 0) ? "B" : (((index1 == 1) ? "C" : ((index1 == 2) ? "J" : ((index1 == 3) ? "PV" : ((index1 == 4) ? "All" : "D"))))));
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string Caltype = ((index1 == 5) ? "DELETEDVOUCHER" : "GETVOUCHER");
            string cheqqueno = this.txtSearchChequeno.Text.Trim() + "%";
            string payment = Chkpayment.Checked ? "length" : "";

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", Caltype, Voutype, frmdate, todate, cheqqueno, payment, "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.lstVouname.Items.Clear();
                return;
            }
            Session["tblvoucher"] = ds1.Tables[0];

            this.lstVouname.DataTextField = "vounum1";
            this.lstVouname.DataValueField = "vounum";
            this.lstVouname.DataSource = ds1.Tables[0];
            this.lstVouname.DataBind();
            this.lstVouname.Visible = this.lstVouname.Items.Count > 0;
            if (Request.QueryString["Mod"].Trim() == "Management")
            {
                if (this.lstVouname.Items.Count > 0)
                {
                    this.lstVouname.SelectedIndex = 0;
                    this.lnkbtnDelVoucher.Visible = true;
                }
                else
                {
                    this.lnkbtnDelVoucher.Visible = false;
                }
            }
        }

        private string Companylimit()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string limit = "";
            switch (comcod)
            {

                case "2305":
                case "3305":
                case "3306":
                case "3309":
                case "3310":
                case "3311":
                    limit = "";
                    break;


                case "1301":
                case "2301":
                case "3301":
                    limit = "limit";
                    break;

                default:
                    limit = "limit";
                    break;
            }
            return limit;
        }

        protected DateTime GetBackDate()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds2 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETBDATE", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return (System.DateTime.Today);
            }

            return (Convert.ToDateTime(ds2.Tables[0].Rows[0]["bdate"]));
        }
        protected void lnkbtnDelVoucher_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["trmid"].ToString();
            string Sessionid = hst["session"].ToString();
            string vounum = this.lstVouname.SelectedValue.ToString();
            bool result;
            string comlimit = this.Companylimit();
            if (comlimit.Length > 0)
            {

                string voudat = Convert.ToDateTime((((DataTable)Session["tblvoucher"]).Select("vounum='" + vounum + "'"))[0]["voudat"]).ToString("dd-MMM-yyyy");
                DateTime Bdate = this.GetBackDate();
                bool dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(voudat));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Voucher Date is Equal or Greater then Transaction Limt');", true);
                    return;
                }

            }

            if (ASTUtility.Left(vounum, 2) == "PV" || ASTUtility.Left(vounum, 2) == "DV")
            {
                // result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "DELPVDVVOU", vounum, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                return;

            }
            else
            {
                result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "DELETEVOUCHER", vounum, userid, Terminal, Sessionid, "", "", "", "", "", "", "", "", "", "", "");
            }
            if (result == false)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Deleted";

            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted  Successfully";
                this.lnkbtnVouOk_Click(null, null);

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Voucher Print";
                    string eventdesc = "Delete Voucher";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }

        }
        private void CompanyCheckPrint()
        {
            //  this.rbtSalSheet.Visible = false;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            switch (comcod)
            {
                case "2305": //Ru Land
                    this.rbtCprintList.SelectedIndex = 1;
                    break;
            }
        }
        private string CompanyPrintVou()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string vouprint = "";
            switch (comcod)
            {

                case "2305":
                    vouprint = "VocherPrint4";
                    break;

                case "3306":
                case "3307":
                case "3308":
                    vouprint = "VocherPrint1";
                    break;
                case "3305":
                case "3310":
                case "3311":
                    vouprint = "VocherPrint2";
                    break;
                case "3309":
                    vouprint = "VocherPrint3";
                    break;


                case "3330":
                    vouprint = "VocherPrint6";
                    break;


                case "3332":

                    vouprint = "VocherPrintIns";
                    break;


                //  case "3101":
                case "3333":
                    vouprint = "VocherPrintMod";
                    break;

                case "3336":
                case "3337":
                    vouprint = "VocherPrintSuvastu";
                    break;

                case "2325":
                case "3325":
                    vouprint = "VocherPrintLei";
                    break;

                default:
                    vouprint = "VocherPrintMod";
                    break;
            }
            return vouprint;
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Type = this.RadioButtonList2.SelectedValue.ToString(); //Request.QueryString["Type"].Trim();
            switch (Type)
            {
                case "AccVoucher":

                    if (chktopsheet.Checked)
                    {
                        this.vouchertopsheetPrint();
                    }

                    else
                    {
                        if (this.rbtnList1.SelectedIndex == 3)
                        {
                            this.PostVouPrint();
                        }
                        else
                        {
                            this.Printvoucher();
                        }
                    }
                    break;

                case "AccCheque":
                    if (this.rbtCprintList.SelectedIndex == 0)
                    {
                        if ((comcod == "3337") || (comcod == "3101"))
                        {
                            this.PrintchKSuvastu();
                        }
                        else if ((comcod == "3344"))
                        {
                            this.PrinChequeTerraNova();
                        }
                        else
                        {
                            this.PrintCheque();
                        }
                    }

                    else
                    {
                        this.PrintChequeLand();
                    }
                    break;


                case "Payslip":
                    this.PrintPaySlip();
                    break;
                case "AccPostDatChq":
                    if ((comcod == "3336") || (comcod == "3337") || (comcod == "3101"))
                    {
                        this.RptPostDatChqSU();
                    }
                    else if (comcod == "3344")
                    {
                        this.PostDateChkTerranova();
                    }
                    else
                    {
                        this.RptPostDatChq();
                    }
                    break;
            }




        }

        private void PrintPaySlip()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string compname = hst["compname"].ToString();
                string comadd = hst["comadd1"].ToString();
                string username = hst["username"].ToString();
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string vounum = this.ddlChkVouNo.SelectedValue.ToString();
                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PRINTPAYSLIP", vounum, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                DataTable dt2 = _ReportDataSet.Tables[1];
                string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MM-yyyy");
                string cheqeno = dt2.Rows[0]["refnum"].ToString();
                string pactname = dt2.Rows[0]["actdesc"].ToString();
                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["tamt"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);

                var list = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.CollectionBrackDown>();

                LocalReport Rpt1 = new LocalReport();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptPaySlipSupplier", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                Rpt1.SetParameters(new ReportParameter("txtDate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("cheqDate", voudat));
                Rpt1.SetParameters(new ReportParameter("cheqNo", cheqeno));
                Rpt1.SetParameters(new ReportParameter("suppName", payto));
                Rpt1.SetParameters(new ReportParameter("pactName", pactname));
                Rpt1.SetParameters(new ReportParameter("txtAmt", amt.ToString()));
                Rpt1.SetParameters(new ReportParameter("txtInWord", amt1));
                Rpt1.SetParameters(new ReportParameter("txtMode", (cheqeno == "" ? "Cash" : "Paid By Cheque")));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "SUPPLIER PAYMENT SLIP"));
                Rpt1.SetParameters(new ReportParameter("comLogo", ComLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }

        }
        private void vouchertopsheetPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            // string Calltype = (this.rbtnList1.SelectedIndex == 5) ? "PRINTDELETEDVOUCHER01" : "PRINTVOUCHER01";
            //DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");


            DataTable dt = (DataTable)Session["tblvoucher"];
            //DataTable dt1 = (DataTable)Session["tblvoutype"];
            //string voutype = dt1.Rows[0]["voutyp"].ToString();
            // LocalReport rpt1 = new LocalReport();
            LocalReport rpt2 = new LocalReport();


            var Voulist = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccVoucher.VoutopSheet>();
            if (comcod == "3101" || comcod == "3338")
            {

                rpt2 = RptSetupClass1.GetLocalReport("R_17_Acc.RptVouTopSheet", Voulist, null, null);
                rpt2.SetParameters(new ReportParameter("footer", "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate));
                rpt2.SetParameters(new ReportParameter("comadd", comadd));
                rpt2.SetParameters(new ReportParameter("comname", comnam));
                rpt2.SetParameters(new ReportParameter("date", "( From " + Convert.ToDateTime(txtfromdate.Text).ToString("dd-MM-yyyy") + " To " + Convert.ToDateTime(txttodate.Text.Trim()).ToString("dd-MM-yyyy") + ")"));
                rpt2.SetParameters(new ReportParameter("voutype", "Top Sheet (" + rbtnList1.SelectedItem.Text.ToString() + ")"));

                Session["Report1"] = rpt2;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }



            else
            {
                ReportDocument rptvou = new RealERPRPT.R_17_Acc.rptTopSheetPrintVou();
                TextObject txtCompanyName = rptvou.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                txtCompanyName.Text = comnam;
                TextObject txtcomadd = rptvou.ReportDefinition.ReportObjects["compadd"] as TextObject;
                txtcomadd.Text = comadd;

                TextObject txtdate = rptvou.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                txtdate.Text = "( From " + Convert.ToDateTime(txtfromdate.Text).ToString("dd-MM-yyyy") + " To " + Convert.ToDateTime(txttodate.Text.Trim()).ToString("dd-MM-yyyy") + ")";

                TextObject txttype = rptvou.ReportDefinition.ReportObjects["txttype"] as TextObject;
                txttype.Text = "Top Sheet (" + rbtnList1.SelectedItem.Text.ToString() + ")";

                TextObject txtuserinfo = rptvou.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
                rptvou.SetDataSource(dt);
                Session["Report1"] = rptvou;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }







        }
        private void GetVouNum()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string chqno = "%" + this.txtSearchCheqno.Text + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds4 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETVOUCHERFORCHK", "", frmdate, todate, chqno, "", "", "", "", "");
            if (ds4.Tables[0].Rows.Count == 0)
            {
                this.ddlChkVouNo.Items.Clear();
                return;
            }

            DataView dv = ds4.Tables[0].DefaultView;
            dv.RowFilter = ("chk ='True'");
            this.ddlChkVouNo.DataTextField = "vounum1";
            this.ddlChkVouNo.DataValueField = "vounum";
            this.ddlChkVouNo.DataSource = dv.ToTable();
            this.ddlChkVouNo.DataBind();
        }
        private void PostDatChqGetVouNum()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string chqno = "%" + this.txtSearchPCheqno.Text + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds4 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETVOUCHERFOR_POSTDAT_CHQ", "", frmdate, todate, chqno, "", "", "", "", "");
            if (ds4.Tables[0].Rows.Count == 0)
            {
                this.ddlPostDatedCheque.Items.Clear();
                return;
            }

            //DataView dv = ds4.Tables[1].DefaultView;
            //dv.RowFilter = ("chk ='True'");

            this.ddlPostDatedCheque.DataTextField = "vounum1";
            this.ddlPostDatedCheque.DataValueField = "vounum";
            this.ddlPostDatedCheque.DataSource = ds4.Tables[1];
            this.ddlPostDatedCheque.DataBind();
        }

        private string CompanyPrintCheque()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string chequeprint = "";
            switch (comcod)
            {


                case "3305":
                case "3306":
                case "3307":
                case "3308":
                case "3309":
                    chequeprint = "PrintCheque01";
                    break;


                case "1301":
                case "2301":
                    //case "3301":
                    chequeprint = "PrintCheque02";
                    break;
                case "3315":
                case "3316":
                case "3317":
                    chequeprint = "PrintChequeAssure";
                    break;


                default:
                    chequeprint = "PrintCheque01";
                    break;
            }
            return chequeprint;
        }



        private void PrintchKSuvastu()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string vounum = this.ddlChkVouNo.SelectedValue.ToString();
                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PRINTCHECK", vounum, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                string voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");
                string voudat1 = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("dd.MM.yyyy");
                // voudat = voudat.Substring(0, 1) + "   " + voudat.Substring(1, 1) + "   " + voudat.Substring(2, 1) + "   " + voudat.Substring(3, 1) + "   " + voudat.Substring(4, 1) + "   " + voudat.Substring(5, 1) + "   " + voudat.Substring(6, 1) + "   " + voudat.Substring(7, 1);
                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["tamt"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string wam1 = string.Empty;
                string wam2 = string.Empty;
                string Chequeprint = this.CompanyPrintCheque();
                string[] amtWrd1 = ASTUtility.Trans(Math.Round(amt, 0), 2).Split('(', ')');
                string[] amtdivide = amtWrd1[1].Split(' ');

                string value = this.ChboxPayee.Checked ? "A/C Payee" : "";

                //if (this.ChBoxCheque.Checked == true)
                //{
                //    string value = this.ChBoxCheque.Text.ToString();
                //}
                //string value = this.ChboxPayee.Checked ? "A/C Payee" : "";

                string RNaration = _ReportDataSet.Tables[1].Rows[0]["naration"].ToString();

                string projnam1 = ((_ReportDataSet.Tables[1].Rows.Count == 0) ? "" : (string)_ReportDataSet.Tables[1].Rows[0]["actdesc"]);
                string projnam2 = ((_ReportDataSet.Tables[1].Rows.Count < 2) ? "" : (string)_ReportDataSet.Tables[1].Rows[1]["actdesc"]);
                string projnam3 = ((_ReportDataSet.Tables[1].Rows.Count < 3) ? "" : (string)_ReportDataSet.Tables[1].Rows[2]["actdesc"]);
                string projnam4 = ((_ReportDataSet.Tables[1].Rows.Count < 4) ? "" : (string)_ReportDataSet.Tables[1].Rows[3]["actdesc"]);
                string projnam5 = ((_ReportDataSet.Tables[1].Rows.Count < 5) ? "" : (string)_ReportDataSet.Tables[1].Rows[4]["actdesc"]);

                double projamt1 = ((_ReportDataSet.Tables[1].Rows.Count == 0) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[0]["trnam"]));
                double projamt2 = ((_ReportDataSet.Tables[1].Rows.Count < 2) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[1]["trnam"]));
                double projamt3 = ((_ReportDataSet.Tables[1].Rows.Count < 3) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[2]["trnam"]));
                double projamt4 = ((_ReportDataSet.Tables[1].Rows.Count < 4) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[3]["trnam"]));
                double projamt5 = ((_ReportDataSet.Tables[1].Rows.Count < 5) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[4]["trnam"]));

                // double advamt = Convert.ToDouble(dt1.Rows[0]["advamt"]);



                for (int i = 2; i <= amtdivide.Length - 1; i++)
                {
                    if (i == amtdivide.Length)
                    {
                        return;
                    }
                    else if (i > 8)
                    {
                        wam1 += " " + amtdivide[i].ToString();
                    }
                    else
                    {
                        wam2 += " " + amtdivide[i].ToString();
                    }
                }


                Hashtable hshtbl = new Hashtable();
                hshtbl["bankName"] = "";
                hshtbl["payTo"] = payto;
                hshtbl["acpayee"] = value;
                hshtbl["date"] = voudat;
                hshtbl["amtWord"] = wam2.ToUpper();//.ToUpper();
                hshtbl["amtWord1"] = wam1.ToUpper();//.ToUpper();
                                                    // hshtbl["payble"] = value;
                hshtbl["amt"] = Convert.ToDouble(amt).ToString("#,##0;(#,##0); ") + "/-";
                hshtbl["projnam1"] = projnam1;
                hshtbl["projnam2"] = projnam2;
                hshtbl["projnam3"] = projnam3;
                hshtbl["projnam4"] = projnam4;
                hshtbl["projnam5"] = projnam5;
                hshtbl["projamt1"] = Convert.ToDouble(projamt1).ToString("#,##0;(#,##0); ");
                hshtbl["projamt2"] = Convert.ToDouble(projamt2).ToString("#,##0;(#,##0); ");
                hshtbl["projamt3"] = Convert.ToDouble(projamt3).ToString("#,##0;(#,##0); ");
                hshtbl["projamt4"] = Convert.ToDouble(projamt4).ToString("#,##0;(#,##0); ");
                hshtbl["projamt5"] = Convert.ToDouble(projamt5).ToString("#,##0;(#,##0); ");
                hshtbl["date1"] = voudat1;
                hshtbl["naration"] = RNaration.ToUpper();



                LocalReport rpt1 = new LocalReport();

                //  rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptCheque", hshtbl, null, null);
                rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeSuvastu", hshtbl, null, null);

                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }

        }
        private void PrintCheque()
        {
            try
            {


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string vounum = this.ddlChkVouNo.SelectedValue.ToString();
                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PRINTCHECK", vounum, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                string voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");

                // voudat = voudat.Substring(0, 1) + "   " + voudat.Substring(1, 1) + "   " + voudat.Substring(2, 1) + "   " + voudat.Substring(3, 1) + "   " + voudat.Substring(4, 1) + "   " + voudat.Substring(5, 1) + "   " + voudat.Substring(6, 1) + "   " + voudat.Substring(7, 1);
                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["tamt"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string wam1 = string.Empty;
                string wam2 = string.Empty;
                string Chequeprint = this.CompanyPrintCheque();
                string[] amtWrd1 = ASTUtility.Trans(Math.Round(amt, 0), 2).Split('(', ')');
                string[] amtdivide = amtWrd1[1].Split(' ');

                string value = this.ChboxPayee.Checked ? "A/C Payee" : "";

                //if (this.ChBoxCheque.Checked == true)
                //{
                //    string value = this.ChBoxCheque.Text.ToString();
                //}
                //string value = this.ChboxPayee.Checked ? "A/C Payee" : "";



                // double advamt = Convert.ToDouble(dt1.Rows[0]["advamt"]);

                for (int i = 2; i <= amtdivide.Length - 1; i++)
                {
                    if (i == amtdivide.Length)
                    {
                        return;
                    }
                    else if (i > 8)
                    {
                        wam1 += " " + amtdivide[i].ToString();
                    }
                    else
                    {
                        wam2 += " " + amtdivide[i].ToString();
                    }
                }


                Hashtable hshtbl = new Hashtable();
                hshtbl["bankName"] = "";
                hshtbl["payTo"] = payto;
                hshtbl["acpayee"] = value;
                hshtbl["date"] = voudat;
                hshtbl["amtWord"] = wam2;//.ToUpper();
                hshtbl["amtWord1"] = wam1;//.ToUpper();
                                          // hshtbl["payble"] = value;
                hshtbl["amt"] = Convert.ToDouble(amt).ToString("#,##0;(#,##0); ") + "/-";




                LocalReport rpt1 = new LocalReport();

                if (comcod == "3338")
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeAcme", hshtbl, null, null);
                }
                else
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptCheque", hshtbl, null, null);
                }




                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }



        }
        private void PrinChequeTerraNova()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string vounum = this.ddlChkVouNo.SelectedValue.ToString();
                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PRINTCHECK", vounum, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                string voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("dd-MM-yyyy");
                string voudat1 = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("dd.MM.yyyy");
                // voudat = voudat.Substring(0, 1) + "   " + voudat.Substring(1, 1) + "   " + voudat.Substring(2, 1) + "   " + voudat.Substring(3, 1) + "   " + voudat.Substring(4, 1) + "   " + voudat.Substring(5, 1) + "   " + voudat.Substring(6, 1) + "   " + voudat.Substring(7, 1);
                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["tamt"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string wam1 = string.Empty;
                string wam2 = string.Empty;
                string Chequeprint = this.CompanyPrintCheque();
                string[] amtWrd1 = ASTUtility.Trans(Math.Round(amt, 0), 2).Split('(', ')');
                string[] amtdivide = amtWrd1[1].Split(' ');

                string value = this.ChboxPayee.Checked ? "A/C Payee" : "";

                ReportDocument rptinfo = new ReportDocument();
                rptinfo = new RealERPRPT.R_17_Acc.PrintCheequeTarraNova();

                TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                date.Text = voudat;
                TextObject rpttxtpayto = rptinfo.ReportDefinition.ReportObjects["txtpayto"] as TextObject;
                rpttxtpayto.Text = payto;
                TextObject rpttxtamtinword = rptinfo.ReportDefinition.ReportObjects["txtamtinword"] as TextObject;
                rpttxtamtinword.Text = amt2;
                TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                rpttxtamt.Text = amt.ToString("#,##0;(#,##0); ") + "/=";


                rptinfo.SetDataSource(dt1);
                Session["Report1"] = rptinfo;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }


        }
        private void PrintChequeLand()
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string vounum = this.ddlChkVouNo.SelectedValue.ToString();
                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PRINTCHECK", vounum, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("ddMMyyyy");
                voudat = voudat.Substring(0, 1) + "   " + voudat.Substring(1, 1) + "   " + voudat.Substring(2, 1) + "   " + voudat.Substring(3, 1) + "   " + voudat.Substring(4, 1) + "   " + voudat.Substring(5, 1) + "   " + voudat.Substring(6, 1) + "   " + voudat.Substring(7, 1);
                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["tamt"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                ReportDocument rptinfo = new RealERPRPT.R_17_Acc.PrintCheck();
                TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                date.Text = voudat;
                TextObject rpttxtpayto = rptinfo.ReportDefinition.ReportObjects["txtpayto"] as TextObject;
                rpttxtpayto.Text = payto;
                TextObject rpttxtamtinword = rptinfo.ReportDefinition.ReportObjects["txtamtinword"] as TextObject;
                rpttxtamtinword.Text = amt2;
                TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                rpttxtamt.Text = amt.ToString("#,##0;(#,##0); ") + "/=";
                rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                Session["Report1"] = rptinfo;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }



        }
        private void RptPostDatChqSU()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();


                string vounum = this.ddlPostDatedCheque.SelectedValue.Substring(0, 14);
                string chqno = this.ddlPostDatedCheque.SelectedValue.Substring(14);


                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "RPTPOSTDATCHECK", vounum, chqno, "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;


                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                string voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");
                string voudat1 = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("dd.MM.yyyy");
                string payto = dt1.Rows[0]["payto"].ToString(); //this.txtRecAndPayto.Text.Trim();
                double toamt, dramt, cramt;

                toamt = Convert.ToDouble(dt1.Rows[0]["trnam"].ToString());

                string amt1 = ASTUtility.Trans(Math.Round(toamt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string wam1 = string.Empty;
                string wam2 = string.Empty;
                string Chequeprint = this.CompanyPrintCheque();
                string[] amtWrd1 = ASTUtility.Trans(Math.Round(toamt, 0), 2).Split('(', ')');
                string[] amtdivide = amtWrd1[1].Split(' ');
                string value = this.ChboxPayee.Checked ? "A/C Payee" : "";

                string RNaration = _ReportDataSet.Tables[1].Rows[0]["naration"].ToString();

                string projnam1 = ((_ReportDataSet.Tables[1].Rows.Count == 0) ? "" : (string)_ReportDataSet.Tables[1].Rows[0]["actdesc"]);
                string projnam2 = ((_ReportDataSet.Tables[1].Rows.Count < 2) ? "" : (string)_ReportDataSet.Tables[1].Rows[1]["actdesc"]);
                string projnam3 = ((_ReportDataSet.Tables[1].Rows.Count < 3) ? "" : (string)_ReportDataSet.Tables[1].Rows[2]["actdesc"]);
                string projnam4 = ((_ReportDataSet.Tables[1].Rows.Count < 4) ? "" : (string)_ReportDataSet.Tables[1].Rows[3]["actdesc"]);
                string projnam5 = ((_ReportDataSet.Tables[1].Rows.Count < 5) ? "" : (string)_ReportDataSet.Tables[1].Rows[4]["actdesc"]);

                double projamt1 = ((_ReportDataSet.Tables[1].Rows.Count == 0) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[0]["trnam"]));
                double projamt2 = ((_ReportDataSet.Tables[1].Rows.Count < 2) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[1]["trnam"]));
                double projamt3 = ((_ReportDataSet.Tables[1].Rows.Count < 3) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[2]["trnam"]));
                double projamt4 = ((_ReportDataSet.Tables[1].Rows.Count < 4) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[3]["trnam"]));
                double projamt5 = ((_ReportDataSet.Tables[1].Rows.Count < 5) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[4]["trnam"]));



                for (int i = 2; i <= amtdivide.Length - 1; i++)
                {
                    if (i == amtdivide.Length)
                    {
                        return;
                    }
                    else if (i > 8)
                    {
                        wam1 += " " + amtdivide[i].ToString();
                    }
                    else
                    {
                        wam2 += " " + amtdivide[i].ToString();
                    }
                }

                Hashtable hshtbl = new Hashtable();
                hshtbl["bankName"] = "";
                hshtbl["payTo"] = payto;
                hshtbl["acpayee"] = value;
                hshtbl["date"] = voudat;
                hshtbl["amtWord"] = wam2;//.ToUpper();
                hshtbl["amtWord1"] = wam1;//.ToUpper();
                hshtbl["amt"] = toamt.ToString("#,##0;(#,##0); ") + "/-";


                hshtbl["projnam1"] = projnam1;
                hshtbl["projnam2"] = projnam2;
                hshtbl["projnam3"] = projnam3;
                hshtbl["projnam4"] = projnam4;
                hshtbl["projnam5"] = projnam5;
                hshtbl["projamt1"] = Convert.ToDouble(projamt1).ToString("#,##0;(#,##0); ");
                hshtbl["projamt2"] = Convert.ToDouble(projamt2).ToString("#,##0;(#,##0); ");
                hshtbl["projamt3"] = Convert.ToDouble(projamt3).ToString("#,##0;(#,##0); ");
                hshtbl["projamt4"] = Convert.ToDouble(projamt4).ToString("#,##0;(#,##0); ");
                hshtbl["projamt5"] = Convert.ToDouble(projamt5).ToString("#,##0;(#,##0); ");
                hshtbl["date1"] = voudat1;
                hshtbl["naration"] = RNaration;

                LocalReport rpt1 = new LocalReport();

                rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeSuvastu", hshtbl, null, null);

                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }

        private void PostDateChkTerranova()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string vounum = this.ddlPostDatedCheque.SelectedValue.Substring(0, 14);
            string chqno = this.ddlPostDatedCheque.SelectedValue.Substring(14);
            DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "RPTPOSTDATCHECK", vounum, chqno, "", "", "", "", "", "", "");
            if (_ReportDataSet == null)
                return;
            DataTable dt1 = _ReportDataSet.Tables[0];
            double toamt, dramt, cramt;
            string voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("dd-MM-yyyy");
            string payto = dt1.Rows[0]["payto"].ToString(); //this.txtRecAndPayto.Text.Trim();
            toamt = Convert.ToDouble(dt1.Rows[0]["trnam"].ToString());
            string amt1 = ASTUtility.Trans(Math.Round(toamt), 2);
            int len = amt1.Length;
            string amt2 = amt1.Substring(7, (len - 8));
            string wam1 = string.Empty;
            string wam2 = string.Empty;
            string Chequeprint = this.CompanyPrintCheque();
            string[] amtWrd1 = ASTUtility.Trans(Math.Round(toamt, 0), 2).Split('(', ')');
            string[] amtdivide = amtWrd1[1].Split(' ');
            string value = (this.Request.QueryString["paytype"] == "0") ? "A/C Payee" : "";

            //if (this.ChBoxCheque.Checked == true)
            //{
            //    string value = this.ChBoxCheque.Text.ToString();
            //}
            //string value = this.ChboxPayee.Checked ? "A/C Payee" : "";
            // double advamt = Convert.ToDouble(dt1.Rows[0]["advamt"]);

            for (int i = 2; i <= amtdivide.Length - 1; i++)
            {
                if (i == amtdivide.Length)
                {
                    return;
                }
                else if (i > 8)
                {
                    wam1 += " " + amtdivide[i].ToString();
                }
                else
                {
                    wam2 += " " + amtdivide[i].ToString();
                }
            }

            ReportDocument rptinfo = new ReportDocument();
            rptinfo = new RealERPRPT.R_17_Acc.PrintCheequeTarraNova();

            TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            date.Text = voudat;
            TextObject rpttxtpayto = rptinfo.ReportDefinition.ReportObjects["txtpayto"] as TextObject;
            rpttxtpayto.Text = payto;
            TextObject rpttxtamtinword = rptinfo.ReportDefinition.ReportObjects["txtamtinword"] as TextObject;
            rpttxtamtinword.Text = amt2;
            TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
            rpttxtamt.Text = "= " + toamt.ToString("#,##0;(#,##0); ") + "/=";
            rptinfo.SetDataSource(dt1);
            Session["Report1"] = rptinfo;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }

        private void RptPostDatChq()
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();


                string vounum = this.ddlPostDatedCheque.SelectedValue.Substring(0, 14);
                string chqno = this.ddlPostDatedCheque.SelectedValue.Substring(14);


                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "RPTPOSTDATCHECK", vounum, chqno, "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;


                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                string voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");
                string payto = dt1.Rows[0]["payto"].ToString(); //this.txtRecAndPayto.Text.Trim();
                double toamt, dramt, cramt;

                toamt = Convert.ToDouble(dt1.Rows[0]["trnam"].ToString());

                string amt1 = ASTUtility.Trans(Math.Round(toamt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string wam1 = string.Empty;
                string wam2 = string.Empty;
                string Chequeprint = this.CompanyPrintCheque();
                string[] amtWrd1 = ASTUtility.Trans(Math.Round(toamt, 0), 2).Split('(', ')');
                string[] amtdivide = amtWrd1[1].Split(' ');
                string value = this.ChboxPayee.Checked ? "A/C Payee" : "";
                for (int i = 2; i <= amtdivide.Length - 1; i++)
                {
                    if (i == amtdivide.Length)
                    {
                        return;
                    }
                    else if (i > 8)
                    {
                        wam1 += " " + amtdivide[i].ToString();
                    }
                    else
                    {
                        wam2 += " " + amtdivide[i].ToString();
                    }
                }

                Hashtable hshtbl = new Hashtable();
                hshtbl["bankName"] = "";
                hshtbl["payTo"] = payto;
                hshtbl["acpayee"] = value;
                hshtbl["date"] = voudat;
                hshtbl["amtWord"] = wam2;//.ToUpper();
                hshtbl["amtWord1"] = wam1;//.ToUpper();
                hshtbl["amt"] = toamt.ToString("#,##0;(#,##0); ") + "/-";

                LocalReport rpt1 = new LocalReport();

                if (comcod == "3338")
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeAcme", hshtbl, null, null);
                }
                else
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptCheque", hshtbl, null, null);
                }

                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }
        private void PostVouPrint()
        {
            try
            {
                //if (this.ddlPrivousVou.Items.Count > 0 && this.lnkOk.Text == "Ok")
                //    this.lnkOk_Click(null, null);

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string combranch = hst["combranch"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                //string curvoudat = this.txtEntryDate.Text.Substring(0, 11);
                //string pvnum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + this.txtcurrentvou.Text.Trim().Substring(2, 2) + "-" + this.txtCurrntlast6.Text.Trim();
                //string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                //this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
                //string VouType = this.Request.QueryString["tname"].ToString();
                string vounum = this.lstVouname.SelectedValue.ToString();


                string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
                string currentptah = "AccPrint.aspx?Type=PostDatVou&vounum=" + vounum;
                string totalpath = hostname + currentptah;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";



                //string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

                //DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");
                //if (_ReportDataSet == null)
                //    return;
                //DataTable dt = _ReportDataSet.Tables[0];
                //DataTable dt1 = _ReportDataSet.Tables[1];
                //if (dt.Rows.Count == 0)
                //    return;
                //double dramt, cramt;
                //dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                //cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));
                //string payto = dt1.Rows[0]["payto"].ToString();
                //string chequeno = dt1.Rows[0]["chequeno"].ToString();
                //string cheqdate = Convert.ToDateTime(dt1.Rows[0]["cheqdate"]).ToString("dd-MMM-yyyy");



                //if (dramt > 0 && cramt > 0)
                //{
                //    TAmount = cramt;

                //}
                //else if (dramt > 0 && cramt <= 0)
                //{
                //    TAmount = dramt;
                //}
                //else
                //{
                //    TAmount = cramt;
                //}

                //string Type = this.CompanyPrintVou();
                //LocalReport Rpt1 = new LocalReport(); 
                //ReportDocument rptinfo = new ReportDocument();
                //if (Type == "VocherPrint")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //}
                //else if (Type == "VocherPrint1")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher1();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtPay = rptinfo.ReportDefinition.ReportObjects["txtPay"] as TextObject;
                //    txtPay.Text = (vounum.Substring(0, 2).ToString() == "PV") ? "Pay To " : "Receive From";

                //}
                //else if (Type == "VocherPrint2")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher2();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtPay = rptinfo.ReportDefinition.ReportObjects["txtPay"] as TextObject;
                //    txtPay.Text = (vounum.Substring(0, 2).ToString() == "PV") ? "Pay To " : "Receive From";
                //}

                //else if (Type == "VocherPrint3")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher3();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //}
                //else if (Type == "VocherPrint5")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher5();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //}


                //else if (Type == "VocherPrint6")
                //{
                //    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                //    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherBridge", list, null, null);
                //    Rpt1.EnableExternalImages = true;

                //    Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                //    Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                //    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                //    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy")));
                //    Rpt1.SetParameters(new ReportParameter("voutype", dt1.Rows[0]["voutyp"].ToString()));
                //    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + dt1.Rows[0]["venar"].ToString()));
                //    Rpt1.SetParameters(new ReportParameter("txtPartyName", ""));
                //    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                //    Rpt1.SetParameters(new ReportParameter("InWrd", ASTUtility.Trans(Math.Round(TAmount), 2)));
                //    Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                //    Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

                //    Session["Report1"] = Rpt1;
                //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

                //    return;

                //    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucherBridge();
                //    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";

                //}
                //else if (Type == "VocherPrintLei")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher7();
                //}

                ////else if (Type == "VocherPrintIns")
                ////{
                ////    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherIns();
                ////    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                ////    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                ////}

                //else if (Type == "VocherPrintMod")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucherAlli();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //}

                //else if (Type == "VocherPrintSuvastu")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucherSuvastu();
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["chequeno"] as TextObject;
                //    Refnum.Text = chequeno;
                //    //TextObject txtProject = rptinfo.ReportDefinition.ReportObjects["txtProject"] as TextObject;
                //    //txtProject.Text = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";                          
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text = (payto == "") ? "" : payto;
                //    TextObject txtDesc = rptinfo.ReportDefinition.ReportObjects["txtDesc"] as TextObject;
                //    txtDesc.Text = dt1.Rows[0]["cactdesc"].ToString();

                //    TextObject chqedate = rptinfo.ReportDefinition.ReportObjects["chqedate"] as TextObject;
                //    chqedate.Text = cheqdate;


                //}

                //else
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher4();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtPay = rptinfo.ReportDefinition.ReportObjects["txtPay"] as TextObject;
                //    txtPay.Text = (vounum.Substring(0, 2).ToString() == "PV") ? "Pay To " : "Receive From";
                //}


                ////-----------------------------

                //string voutype = dt1.Rows[0]["voutyp"].ToString();
                //string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                //string venar = dt1.Rows[0]["venar"].ToString();
                ////ReportDocument rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher();
                //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //txtCompanyName.Text = comnam;
                //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                //txtcAdd.Text = comadd;


                //TextObject rpttxtVoutype = rptinfo.ReportDefinition.ReportObjects["txtVoutype"] as TextObject;
                //rpttxtVoutype.Text = voutype;
                //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //vounum1.Text = (comcod == "3336" || comcod == "3337" || comcod == "3101") ? vounum : "Voucher No: " + vounum;
                //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //date.Text = (comcod == "3336" || comcod == "3337" || comcod == "3101") ? voudat : " Date:" + voudat;
                //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //naration.Text = "Naration: " + venar;
                //TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                //rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);
                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                //if (ConstantInfo.LogStatus == true)
                //{
                //    string eventtype = "Post Dated Cheque";
                //    string eventdesc = "Print Voucher";
                //    string eventdesc2 = vounum;
                //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                //}
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rptinfo.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rptinfo;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }
        private string GetCompInstar()
        {
            string comcod = this.GetCompCode();
            string printinstar = "";
            switch (comcod)
            {
                case "3330"://bridge
                case "2325": //Leisure
                case "3325":
                    break;
                default:
                    printinstar = "Innstar";
                    break;
            }
            return printinstar;
        }
        private string pamentVoucherr()
        {
            string compamentVoucher = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            switch (comcod)
            {
                case "3105":
                    compamentVoucher = "PrintpamanevouRdlc";
                    break;

                default:

                    break;
            }
            return compamentVoucher;


        }

        private void Printvoucher()
        {
            string paymenttype = this.pamentVoucherr();

            if (paymenttype == "PrintpamanevouRdlc")
            {
                this.PrintpamanevouRdlc();
            }
            else
            {
                this.PrintvoucherOld();
            }

        }

        private void PrintpamanevouRdlc()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string combranch = hst["combranch"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string vounum = this.lstVouname.SelectedValue.ToString();
                string PrintInstar = this.GetCompInstar();
                string Calltype = (this.rbtnList1.SelectedIndex == 5) ? "PRINTDELETEDVOUCHER01" : "PRINTVOUCHER01";
                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", Calltype, vounum, PrintInstar, "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt = _ReportDataSet.Tables[0];

                double dramt, cramt;
                dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;



                if (dramt > 0 && cramt > 0)
                {
                    TAmount = cramt;

                }
                else if (dramt > 0 && cramt <= 0)
                {
                    TAmount = dramt;
                }
                else
                {
                    TAmount = cramt;
                }

                DataTable dt1 = _ReportDataSet.Tables[1];
                string Vounum = dt1.Rows[0]["vounum"].ToString();
                string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                string refnum = dt1.Rows[0]["refnum"].ToString();
                string payto = dt1.Rows[0]["payto"].ToString();
                string Partytype = (ASTUtility.Left(vounum, 2) == "BC") ? "Recieved From:" : (ASTUtility.Left(vounum, 2) == "CC") ? "Recieved From:" : "Pay To:";
                string voutype = dt1.Rows[0]["voutyp"].ToString();
                string venar = dt1.Rows[0]["venar"].ToString();
                string Isunum = (dt1.Rows[0]["isunum"]).ToString() == "" ? "" : ASTUtility.Right((dt1.Rows[0]["isunum"]).ToString(), 6);
                string Posteddat = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
                string postuser = dt1.Rows[0]["postuser"].ToString();


                string receivedBank = dt1.Rows[0]["banknam"].ToString();

                string Type = this.CompanyPrintVou();

                string billno = dt.Rows[0]["billno"].ToString();
                //string[] billno1;

                //string[] billno;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // dt1.Rows[j]["useridapp"].ToString() == useridapp

                    if (billno == dt.Rows[i]["billno"].ToString())
                    {

                    }

                    else
                    {
                        billno += dt.Rows[i]["billno"].ToString() + (((dt.Rows[i]["billno"].ToString()).Length == 0) ? " " : ", ");
                    }

                }

                int l = billno.Trim().Length;
                billno = (billno.Length == 0) ? "" : billno.Substring(0, l - 1);



                var lst = _ReportDataSet.Tables[0].DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PaymentVouClas1>();
                var lst1 = _ReportDataSet.Tables[1].DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PaymentVouClas2>();


                string payto1 = lst1[0].payto.ToString();
                string bankCash = lst1[0].cactdesc.ToString();
                string vouno1 = lst1[0].vounum.ToString();
                string project1 = lst1[0].isunum.ToString();
                string Narration = lst1[0].venar.ToString();
                double tAmt = lst.Select(p => p.totalamt).Sum();




                LocalReport Rpt1 = new LocalReport();

                if (Type == "VocherPrintMod")
                {
                    if (ASTUtility.Left(vounum, 2) == "JV")
                    {
                        //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli();

                        //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                        //vounum1.Text = "Voucher No.: " + vounum;
                        //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                        //date.Text = "Voucher Date: " + voudat;
                        //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                        //naration.Text = "Narration: " + venar;

                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptVoucherPrintJV", lst, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("companyname", comnam.ToUpper()));
                        Rpt1.SetParameters(new ReportParameter("payto1", payto));
                        Rpt1.SetParameters(new ReportParameter("bankCash", bankCash));
                        Rpt1.SetParameters(new ReportParameter("vouno", vouno1));
                        Rpt1.SetParameters(new ReportParameter("voudate", voudat));
                        Rpt1.SetParameters(new ReportParameter("project1", ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office"));
                        Rpt1.SetParameters(new ReportParameter("naration", Narration));
                        Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                        Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                        Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                        Rpt1.SetParameters(new ReportParameter("Isunum", Isunum));
                        Rpt1.SetParameters(new ReportParameter("postuser", postuser));
                        Rpt1.SetParameters(new ReportParameter("Posteddat", Posteddat));
                        Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                        Rpt1.SetParameters(new ReportParameter("Partytype", Partytype));

                        Rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(tAmt), 2)));
                        Rpt1.SetParameters(new ReportParameter("txtuserinfo", "Printed from Computer Address :" + compname + " ,Session: " + Session + " ,User: " + username + " ,Time: " + printdate));
                        Session["Report1"] = Rpt1;
                        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

                    }

                    else
                    {
                        string vouno = vounum.Substring(0, 2);
                        string paytoorecived = (vouno == "BC" || vouno == "CC") ? "Recieved From" : "Pay To";

                        if (vouno == "BC" || vouno == "CC")
                        {
                            //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli02();
                            //TextObject txtDesc = rptinfo.ReportDefinition.ReportObjects["txtDesc"] as TextObject;
                            //txtDesc.Text = dt1.Rows[0]["cactdesc"].ToString();// this.ddlConAccHead.SelectedItem.Text.Substring(13).ToString();
                            //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                            //rpttxtPartyName.Text = (payto == "") ? "" : payto;


                            //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                            //naration.Text = venar;

                            //TextObject txtporrecieved = rptinfo.ReportDefinition.ReportObjects["txtporrecieved"] as TextObject;
                            //txtporrecieved.Text = paytoorecived;

                            //TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
                            //txtusername.Text = (comcod == "3336" || comcod == "3337"||comcod=="3101") ? postuser : username;

                            //TextObject txtProject = rptinfo.ReportDefinition.ReportObjects["txtProject"] as TextObject;
                            //txtProject.Text = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";

                            //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                            //vounum1.Text = vounum;
                            //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                            //date.Text = voudat;
                            //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                            //Refnum.Text = refnum;
                            //TextObject txtReceivedBank = rptinfo.ReportDefinition.ReportObjects["txtReceivedBank"] as TextObject;
                            //txtReceivedBank.Text = receivedBank;

                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptVoucherPrint", lst, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("companyname", comnam.ToUpper()));
                            Rpt1.SetParameters(new ReportParameter("payto1", payto));
                            Rpt1.SetParameters(new ReportParameter("bankCash", bankCash));
                            Rpt1.SetParameters(new ReportParameter("vouno", vouno));
                            Rpt1.SetParameters(new ReportParameter("voudate", voudat));
                            Rpt1.SetParameters(new ReportParameter("project1", ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office"));
                            Rpt1.SetParameters(new ReportParameter("naration", Narration));
                            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                            Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                            Rpt1.SetParameters(new ReportParameter("Isunum", Isunum));
                            Rpt1.SetParameters(new ReportParameter("postuser", postuser));
                            Rpt1.SetParameters(new ReportParameter("Posteddat", Posteddat));
                            Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                            Rpt1.SetParameters(new ReportParameter("Partytype", Partytype));

                            Rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(tAmt), 2)));
                            Rpt1.SetParameters(new ReportParameter("txtuserinfo", "Printed from Computer Address :" + compname + " ,Session: " + Session + " ,User: " + username + " ,Time: " + printdate));
                            Session["Report1"] = Rpt1;
                            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


                        }

                        else
                        {

                            string vouno2 = vounum.Substring(0, 2);

                            //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli03();


                            if (vouno == "BD" || vouno == "CD")
                            {

                                //TextObject txtBillno = rptinfo.ReportDefinition.ReportObjects["txtBillno"] as TextObject;
                                //txtBillno.Text = "Bill No : " + billno;
                                //  TextObject txtissuno = rptinfo.ReportDefinition.ReportObjects["txtissuno"] as TextObject;
                                //   txtissuno.Text = "Payment ID : " + dt1.Rows[0]["isunum"].ToString();



                                //TextObject txtDesc = rptinfo.ReportDefinition.ReportObjects["txtDesc"] as TextObject;
                                //txtDesc.Text = dt1.Rows[0]["cactdesc"].ToString();// this.ddlConAccHead.SelectedItem.Text.Substring(13).ToString();
                                //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                                //rpttxtPartyName.Text = (payto == "") ? "" : payto;


                                //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                                //naration.Text = venar;

                                //TextObject txtporrecieved = rptinfo.ReportDefinition.ReportObjects["txtporrecieved"] as TextObject;
                                //txtporrecieved.Text = paytoorecived;

                                //TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
                                //txtusername.Text =postuser ;

                                //TextObject txtProject = rptinfo.ReportDefinition.ReportObjects["txtProject"] as TextObject;
                                //txtProject.Text = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";

                                //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                                //vounum1.Text = vounum;
                                //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                                //date.Text = voudat;
                                //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                                //Refnum.Text = refnum;

                                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptVoucherPrintJV", lst, null, null);
                                Rpt1.EnableExternalImages = true;
                                Rpt1.SetParameters(new ReportParameter("companyname", comnam.ToUpper()));
                                Rpt1.SetParameters(new ReportParameter("payto1", payto));
                                Rpt1.SetParameters(new ReportParameter("bankCash", bankCash));
                                Rpt1.SetParameters(new ReportParameter("vouno", vouno2));
                                Rpt1.SetParameters(new ReportParameter("voudate", voudat));
                                Rpt1.SetParameters(new ReportParameter("project1", ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office"));
                                Rpt1.SetParameters(new ReportParameter("naration", Narration));
                                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                                Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                                Rpt1.SetParameters(new ReportParameter("Isunum", Isunum));
                                Rpt1.SetParameters(new ReportParameter("postuser", postuser));
                                Rpt1.SetParameters(new ReportParameter("Posteddat", Posteddat));
                                Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                                Rpt1.SetParameters(new ReportParameter("Partytype", Partytype));

                                Rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(tAmt), 2)));
                                Rpt1.SetParameters(new ReportParameter("txtuserinfo", "Printed from Computer Address :" + compname + " ,Session: " + Session + " ,User: " + username + " ,Time: " + printdate));
                                Session["Report1"] = Rpt1;
                                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


                            }
                        }
                    }
                }






                //if (Type == "VocherPrintMod")
                //{
                //    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptVoucherPrint", lst, null, null);
                //    Rpt1.EnableExternalImages = true;
                //    Rpt1.SetParameters(new ReportParameter("companyname", comnam.ToUpper()));
                //    Rpt1.SetParameters(new ReportParameter("payto1", payto));
                //    Rpt1.SetParameters(new ReportParameter("bankCash", bankCash));
                //    Rpt1.SetParameters(new ReportParameter("vouno", vouno));
                //    Rpt1.SetParameters(new ReportParameter("voudate", voudat));
                //    Rpt1.SetParameters(new ReportParameter("project1", ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office"));
                //    Rpt1.SetParameters(new ReportParameter("naration", Narration));
                //    Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                //    Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                //    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                //    Rpt1.SetParameters(new ReportParameter("Isunum", Isunum));
                //    Rpt1.SetParameters(new ReportParameter("postuser", postuser));
                //    Rpt1.SetParameters(new ReportParameter("Posteddat", Posteddat));
                //    Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                //    Rpt1.SetParameters(new ReportParameter("Partytype", Partytype));

                //    Rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(tAmt), 2)));
                //    Rpt1.SetParameters(new ReportParameter("txtuserinfo", "Printed from Computer Address :" + compname + " ,Session: " + Session + " ,User: " + username + " ,Time: " + printdate));
                //    Session["Report1"] = Rpt1;
                //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
                //}






            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }

        }



        private void PrintvoucherOld()
        {


            // TransectionPrint


            try
            {



                string vounum = this.lstVouname.SelectedValue.ToString();
                string paytype = "0";
                // hlink1.NavigateUrl = "~/F_17_Acc/GeneralAccounts.aspx?Mod=Management&vounum=" + vounum;
                // hlnkPrintVoucher.NavigateUrl = "~/F_17_Acc/AccPrint.aspx?Type=accVou&vounum=" + vounum;

                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccPrint.aspx?Type=accVou&vounum=" + vounum + "&paytype=" + paytype
                           + "', target='_blank');</script>";



                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = hst["comcod"].ToString();
                //string comnam = hst["comnam"].ToString();
                //string comadd = hst["comadd1"].ToString();
                //string combranch = hst["combranch"].ToString();
                //string compname = hst["compname"].ToString();
                //string username = hst["username"].ToString();
                //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                //string vounum = this.lstVouname.SelectedValue.ToString();
                //string PrintInstar = this.GetCompInstar();
                //string Calltype = (this.rbtnList1.SelectedIndex == 5) ? "PRINTDELETEDVOUCHER01" : "PRINTVOUCHER01";
                //DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", Calltype, vounum, PrintInstar, "", "", "", "", "", "", "");
                //if (_ReportDataSet == null)
                //    return;
                //DataTable dt = _ReportDataSet.Tables[0];

                //double dramt, cramt;
                //dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                //cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));



                //if (dramt > 0 && cramt > 0)
                //{
                //    TAmount = cramt;

                //}
                //else if (dramt > 0 && cramt <= 0)
                //{
                //    TAmount = dramt;
                //}
                //else
                //{
                //    TAmount = cramt;
                //}

                //DataTable dt1 = _ReportDataSet.Tables[1];
                //string Vounum = dt1.Rows[0]["vounum"].ToString();
                //string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                //string refnum = dt1.Rows[0]["refnum"].ToString();
                //string payto = dt1.Rows[0]["payto"].ToString();
                //string Partytype = (ASTUtility.Left(vounum, 2) == "BC") ? "Recieved From:" : (ASTUtility.Left(vounum, 2) == "CC") ? "Recieved From:" : "Pay To:";
                //string voutype = dt1.Rows[0]["voutyp"].ToString();
                //string venar = dt1.Rows[0]["venar"].ToString();
                //string Isunum = (dt1.Rows[0]["isunum"]).ToString() == "" ? "" : ASTUtility.Right((dt1.Rows[0]["isunum"]).ToString(), 6);
                //string Posteddat = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
                //string postuser = dt1.Rows[0]["postuser"].ToString();
                //string pactdesc = dt.Rows[0]["actdesc"].ToString();
                //string receivedBank = dt1.Rows[0]["banknam"].ToString();

                //string Type = this.CompanyPrintVou();

                //string billno = dt.Rows[0]["billno"].ToString();
                ////string[] billno1;

                ////string[] billno;

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    // dt1.Rows[j]["useridapp"].ToString() == useridapp

                //    if (billno == dt.Rows[i]["billno"].ToString())
                //    {

                //    }

                //    else
                //    {
                //        billno += dt.Rows[i]["billno"].ToString() + (((dt.Rows[i]["billno"].ToString()).Length == 0) ? " " : ", ");
                //    }

                //}

                //int l = billno.Trim().Length;
                //billno = (billno.Length == 0) ? "" : billno.Substring(0, l - 1);


                //ReportDocument rptinfo = new ReportDocument();
                //if (Type == "VocherPrint")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();
                //    //TextObject txtConverssion = rptinfo.ReportDefinition.ReportObjects["txtConverssion"] as TextObject;
                //    //string curocde = _ReportDataSet.Tables[1].Rows[0]["curcode"].ToString();
                //    //double conrate = Convert.ToDouble(_ReportDataSet.Tables[1].Rows[0]["conrate"]);
                //    //txtConverssion.Text = (curocde == "001") ? "" : _ReportDataSet.Tables[1].Rows[0]["curdesc"].ToString() + " " + (TAmount * conrate).ToString("#,##0.00;(#,##0.00)") + " (" + "1 SGD" + "=" + conrate.ToString("#,##0.00;(#,##0.00)") + " " + _ReportDataSet.Tables[1].Rows[0]["curdesc"].ToString() + ")";
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //    naration.Text = "Narration: " + venar;
                //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //    vounum1.Text = "Voucher No.: " + vounum;
                //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //    date.Text = "Voucher Date: " + voudat;
                //}
                //else if (Type == "VocherPrint1")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher1();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";

                //    TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum1.Text = "Issue No: " + Isunum;
                //    TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate1.Text = "Entry Date: " + Posteddat;
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //    naration.Text = "Narration: " + venar;
                //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //    vounum1.Text = "Voucher No.: " + vounum;
                //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //    date.Text = "Voucher Date: " + voudat;


                //}
                //else if (Type == "VocherPrint2")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher2();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum2.Text = "Issue No: " + Isunum;
                //    TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate2.Text = "Entry Date: " + Posteddat;
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //    naration.Text = "Narration: " + venar;
                //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //    vounum1.Text = "Voucher No.: " + vounum;
                //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //    date.Text = "Voucher Date: " + voudat;
                //}
                //else if (Type == "VocherPrint5")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher5();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum2.Text = "Issue No: " + Isunum;
                //    TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate2.Text = "Entry Date: " + Posteddat;
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //    naration.Text = "Narration: " + venar;
                //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //    vounum1.Text = "Voucher No.: " + vounum;
                //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //    date.Text = "Voucher Date: " + voudat;
                //}

                //else if (Type == "VocherPrint3")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher3();
                //    TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum2.Text = "Issue No: " + Isunum;
                //    TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate2.Text = "Entry Date: " + Posteddat;
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto;  //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //    naration.Text = "Narration: " + venar;
                //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //    vounum1.Text = "Voucher No.: " + vounum;
                //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //    date.Text = "Voucher Date: " + voudat;
                //}

                //else if (Type == "VocherPrint5")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher5();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum1.Text = "Issue No: " + Isunum;
                //    TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate1.Text = "Entry Date: " + Posteddat;
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //    naration.Text = "Narration: " + venar;
                //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //    vounum1.Text = "Voucher No.: " + vounum;
                //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //    date.Text = "Voucher Date: " + voudat;

                //}
                //else if (Type == "VocherPrint6")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucherBridge();
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //    naration.Text = "Narration: " + venar;
                //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //    vounum1.Text = "Voucher No.: " + vounum;
                //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //    date.Text = "Voucher Date: " + voudat;


                //}

                //else if (Type == "VocherPrintLei")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher7();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //    vounum1.Text = "Voucher No.: " + vounum;

                //    TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum2.Text = "Issue No: " + Isunum;
                //    TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate2.Text = "Entry Date: " + Posteddat;
                //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //    date.Text = "Voucher Date: " + voudat;
                //}

                //else if (Type == "VocherPrintIns")
                //{
                //    if (ASTUtility.Left(vounum, 2) == "JV")
                //    {
                //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherIns();

                //        //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //        //txtCompanyName.Text = comnam;
                //        //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                //        //txtcAdd.Text = comadd;
                //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //        vounum1.Text = "Voucher No.: " + vounum;
                //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //        date.Text = "Voucher Date: " + voudat;
                //        //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //        //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //        //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //        //rpttxtPartyName.Text = "";// (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //        //TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
                //        //voutype1.Text = voutype;
                //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //        naration.Text = "Narration: " + venar;
                //    }

                //    else
                //    {
                //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherIns02();
                //        TextObject txtDesc = rptinfo.ReportDefinition.ReportObjects["txtDesc"] as TextObject;
                //        txtDesc.Text = dt1.Rows[0]["cactdesc"].ToString();// this.ddlConAccHead.SelectedItem.Text.Substring(13).ToString();
                //        TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //        rpttxtPartyName.Text = (payto == "") ? "" : payto;

                //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //        naration.Text = venar;

                //        TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
                //        txtusername.Text = username;

                //        TextObject txtProject = rptinfo.ReportDefinition.ReportObjects["txtProject"] as TextObject;
                //        txtProject.Text = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";

                //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //        vounum1.Text = vounum;
                //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //        date.Text = voudat;
                //        TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //        Refnum.Text = refnum;

                //    }



                //}


                //else if (Type == "VocherPrintSuvastu")
                //{

                //    if (ASTUtility.Left(vounum, 2) == "JV")
                //    {
                //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherSuvastu();

                //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //        vounum1.Text = "Voucher No.: " + vounum;
                //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //        date.Text = "Voucher Date: " + voudat;
                //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //        naration.Text = "Narration: " + venar;

                //        TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
                //        txtusername.Text = postuser;
                //    }




                //    else
                //    {
                //        string vouno = vounum.Substring(0, 2);
                //        string paytoorecived = (vouno == "BC" || vouno == "CC") ? "Recieved From" : "Pay To";

                //        if (vouno == "BC" || vouno == "CC")
                //        {
                //            rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherSuvastu02();
                //            TextObject txtDesc = rptinfo.ReportDefinition.ReportObjects["txtDesc"] as TextObject;
                //            txtDesc.Text = dt1.Rows[0]["cactdesc"].ToString();// this.ddlConAccHead.SelectedItem.Text.Substring(13).ToString();
                //            TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //            rpttxtPartyName.Text = (payto == "") ? "" : payto;


                //            TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //            naration.Text = venar;

                //            TextObject txtporrecieved = rptinfo.ReportDefinition.ReportObjects["txtporrecieved"] as TextObject;
                //            txtporrecieved.Text = paytoorecived;

                //            TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
                //            txtusername.Text = postuser;

                //            TextObject txtProject = rptinfo.ReportDefinition.ReportObjects["txtProject"] as TextObject;
                //            txtProject.Text = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";

                //            TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //            vounum1.Text = vounum;
                //            TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //            date.Text = voudat;
                //            TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //            Refnum.Text = refnum;
                //            TextObject txtReceivedBank = rptinfo.ReportDefinition.ReportObjects["txtReceivedBank"] as TextObject;
                //            txtReceivedBank.Text = receivedBank;


                //        }

                //        else
                //        {



                //            rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherSuvastu03();




                //            if (vouno == "BD" || vouno == "CD")
                //            {

                //                //TextObject txtBillno = rptinfo.ReportDefinition.ReportObjects["txtBillno"] as TextObject;
                //                //txtBillno.Text = "Bill No : " + billno;
                //                TextObject txtissuno = rptinfo.ReportDefinition.ReportObjects["txtissuno"] as TextObject;
                //                txtissuno.Text = "Payment ID : " + dt1.Rows[0]["isunum"].ToString();

                //            }

                //            TextObject txtDesc = rptinfo.ReportDefinition.ReportObjects["txtDesc"] as TextObject;
                //            txtDesc.Text = dt1.Rows[0]["cactdesc"].ToString();// this.ddlConAccHead.SelectedItem.Text.Substring(13).ToString();
                //            TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //            rpttxtPartyName.Text = (payto == "") ? "" : payto;


                //            TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //            naration.Text = venar;

                //            TextObject txtporrecieved = rptinfo.ReportDefinition.ReportObjects["txtporrecieved"] as TextObject;
                //            txtporrecieved.Text = paytoorecived;

                //            TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
                //            txtusername.Text = postuser;

                //            TextObject txtProject = rptinfo.ReportDefinition.ReportObjects["txtProject"] as TextObject;
                //            txtProject.Text = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";

                //            TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //            vounum1.Text = vounum;
                //            TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //            date.Text = voudat;
                //            TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //            Refnum.Text = refnum;


                //        }




                //    }


                //}



                //else if (Type == "VocherPrintMod")
                //{
                //    if (ASTUtility.Left(vounum, 2) == "JV")
                //    {
                //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli();

                //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //        vounum1.Text = "Voucher No.: " + vounum;
                //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //        date.Text = "Voucher Date: " + voudat;
                //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //        naration.Text = "Narration: " + venar;
                //    }




                //    else
                //    {
                //        string vouno = vounum.Substring(0, 2);
                //        string prjdesc = "";
                //        switch (comcod)
                //        {
                //            case "3338":

                //                prjdesc = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "1301" ||
                //                          ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" ||
                //                          ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" ||
                //                          (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "0000" &&
                //                           ASTUtility.Left(dt.Rows[0]["actcode"].ToString(), 4) == "1901") ||
                //                          ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "2201" ||
                //                          ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "2301" ||
                //                          ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26"
                //                    ? (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "1301"
                //                        ? dt.Rows[0]["actdesc"].ToString().Replace("ADVANCE-", "")
                //                        : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16"
                //                            ? dt.Rows[0]["actdesc"].ToString().Replace("WIP-", "")
                //                            : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18"
                //                                ? dt.Rows[0]["actdesc"].ToString().Replace("AR-", "")
                //                                : (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "0000" &&
                //                                   ASTUtility.Left(dt.Rows[0]["actcode"].ToString(), 4) == "1901")
                //                                    ? dt.Rows[0]["actdesc"].ToString().Replace("CASH-", "")
                //                                    : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "2201"
                //                                        ? dt.Rows[0]["actdesc"].ToString().Replace("LOAN-", "")
                //                                        : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "2301"
                //                                            ? dt.Rows[0]["actdesc"].ToString().Replace("TVS-", "")
                //                                            : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26"
                //                                                ? dt.Rows[0]["actdesc"].ToString().Replace("AP-", "")
                //                                                : "")
                //                    : "Head Office";
                //                break;

                //            default:

                //                prjdesc = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" ||
                //                          ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" ||
                //                          ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26"
                //                    ? "Project"
                //                    : "Head Office";
                //                break;
                //        }
                //        string paytoorecived = (vouno == "BC" || vouno == "CC") ? "Recieved From" : "Pay To";

                //        if (vouno == "BC" || vouno == "CC")
                //        {
                //            rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli02();
                //            TextObject txtDesc = rptinfo.ReportDefinition.ReportObjects["txtDesc"] as TextObject;
                //            txtDesc.Text = dt1.Rows[0]["cactdesc"].ToString();// this.ddlConAccHead.SelectedItem.Text.Substring(13).ToString();
                //            TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //            rpttxtPartyName.Text = (payto == "") ? "" : payto;


                //            TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //            naration.Text = venar;

                //            TextObject txtporrecieved = rptinfo.ReportDefinition.ReportObjects["txtporrecieved"] as TextObject;
                //            txtporrecieved.Text = paytoorecived;

                //            TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
                //            txtusername.Text = (comcod == "3336" || comcod == "3337" || comcod == "3101") ? postuser : username;

                //            TextObject txtProject = rptinfo.ReportDefinition.ReportObjects["txtProject"] as TextObject;
                //            //txtProject.Text = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";
                //            txtProject.Text = prjdesc;
                //            TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //            vounum1.Text = vounum;
                //            TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //            date.Text = voudat;
                //            TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //            Refnum.Text = refnum;
                //            TextObject txtReceivedBank = rptinfo.ReportDefinition.ReportObjects["txtReceivedBank"] as TextObject;
                //            txtReceivedBank.Text = receivedBank;


                //        }

                //        else
                //        {



                //            rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli03();


                //            if (vouno == "BD" || vouno == "CD")
                //            {

                //                //TextObject txtBillno = rptinfo.ReportDefinition.ReportObjects["txtBillno"] as TextObject;
                //                //txtBillno.Text = "Bill No : " + billno;
                //                TextObject txtissuno = rptinfo.ReportDefinition.ReportObjects["txtissuno"] as TextObject;
                //                txtissuno.Text = "Payment ID : " + dt1.Rows[0]["isunum"].ToString();

                //            }

                //            TextObject txtDesc = rptinfo.ReportDefinition.ReportObjects["txtDesc"] as TextObject;
                //            txtDesc.Text = dt1.Rows[0]["cactdesc"].ToString();// this.ddlConAccHead.SelectedItem.Text.Substring(13).ToString();
                //            TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //            rpttxtPartyName.Text = (payto == "") ? "" : payto;


                //            TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //            naration.Text = venar;

                //            TextObject txtporrecieved = rptinfo.ReportDefinition.ReportObjects["txtporrecieved"] as TextObject;
                //            txtporrecieved.Text = paytoorecived;

                //            TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
                //            txtusername.Text = postuser;

                //            TextObject txtProject = rptinfo.ReportDefinition.ReportObjects["txtProject"] as TextObject;
                //            //txtProject.Text = ASTUtility.Left (dt.Rows[0]["mactcode"].ToString (), 2) == "16" || ASTUtility.Left (dt.Rows[0]["mactcode"].ToString (), 2) == "18" || ASTUtility.Left (dt.Rows[0]["mactcode"].ToString (), 2) == "26" ? pactdesc : "Head Office";
                //            txtProject.Text = prjdesc;
                //            TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //            vounum1.Text = vounum;
                //            TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //            date.Text = voudat;
                //            TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //            Refnum.Text = refnum;


                //        }




                //    }






                //}


                //else
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher4();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtisunum3 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum3.Text = "Issue No: " + Isunum;
                //    TextObject txtPosteddate3 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate3.Text = "Entry Date: " + Posteddat;
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //    naration.Text = "Narration: " + venar;
                //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //    vounum1.Text = "Voucher No.: " + vounum;
                //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //    date.Text = "Voucher Date: " + voudat;

                //}

                ////ReportDocument rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();

                //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //txtCompanyName.Text = comnam;
                //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                //txtcAdd.Text = comadd;
                ////TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                ////vounum1.Text = "Voucher No.: " + vounum;
                ////TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                ////date.Text = "Voucher Date: " + voudat;
                ////TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                ////Refnum.Text = "Cheque/Ref. No.: " + refnum;

                ////TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                ////rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto;
                //TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
                //voutype1.Text = voutype;
                ////TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //// naration.Text = "Narration: " + venar;
                //TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                //rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);
                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                //if (ConstantInfo.LogStatus == true)
                //{
                //    string eventtype = "Voucher Print";
                //    string eventdesc = "Print Voucher";
                //    string eventdesc2 = "Voucher No.: " + vounum;
                //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                //}

                //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rptinfo.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rptinfo;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }


        }

        protected void lnkbtnChkOk_Click(object sender, EventArgs e)
        {
            this.GetVouNum();
            this.ShowCheque();
            //if (this.lnkbtnChkOk.Text == "Ok")
            //{
            //    this.lnkbtnChkOk.Text = "New";
            //    this.GetVouNum();
            //    this.ShowCheque();
            //}
            //else
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "";
            //    this.lnkbtnChkOk.Text = "Ok";
            //    this.gvCheque.DataSource = null;
            //    this.gvCheque.DataBind();

            //}




        }

        private void ShowCheque()
        {
            Session.Remove("tblchkprint");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Voutype = "BD";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string chqno = "%" + this.txtSearchCheqno.Text + "%";
            DataSet ds4 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETVOUCHERFORCHK", Voutype, frmdate, todate, chqno, "", "", "", "", "");


            if (ds4.Tables[0].Rows.Count == 0)
            {
                this.gvCheque.DataSource = null;
                this.gvCheque.DataBind();
                return;
            }

            Session["tblchkprint"] = ds4.Tables[0];
            this.LoadGrid();


        }
        private void ShowPostDatCheque()
        {
            Session.Remove("tblchkprint");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Voutype = "PV";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string chqno = "%" + this.txtSearchPCheqno.Text + "%";
            DataSet ds4 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETVOUCHERFOR_POSTDAT_CHQ", Voutype, frmdate, todate, chqno, "", "", "", "", "");


            if (ds4.Tables[0].Rows.Count == 0)
            {
                this.gvPostDatCheq.DataSource = null;
                this.gvPostDatCheq.DataBind();
                return;
            }

            Session["tblchkprint"] = ds4.Tables[0];
            this.LoadGrid();


        }
        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblchkprint"];
            string Type = this.RadioButtonList2.SelectedValue.ToString(); //Request.QueryString["Type"].Trim();
            switch (Type)
            {
                case "AccCheque":
                    this.gvCheque.DataSource = dt;
                    this.gvCheque.DataBind();
                    break;
                case "AccPostDatChq":
                    this.gvPostDatCheq.DataSource = dt;
                    this.gvPostDatCheq.DataBind();
                    break;
                case "Payslip":
                    this.gvCheque.DataSource = dt;
                    this.gvCheque.DataBind();
                    break;



            }


        }
        protected void gvCheque_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvCheque.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tblchkprint"];
            int Rowid;
            string Type = this.RadioButtonList2.SelectedValue.ToString();//Request.QueryString["Type"].Trim();
            switch (Type)
            {
                case "AccCheque":
                    for (int i = 0; i < this.gvCheque.Rows.Count; i++)
                    {

                        string payto = ((TextBox)gvCheque.Rows[i].FindControl("txtgvPayto")).Text.Trim();
                        Rowid = (this.gvCheque.PageSize) * (this.gvCheque.PageIndex) + i;
                        dt.Rows[Rowid]["payto"] = payto;

                    }
                    break;
                case "AccPostDatChq":
                    for (int i = 0; i < this.gvPostDatCheq.Rows.Count; i++)
                    {

                        string payto = ((TextBox)gvPostDatCheq.Rows[i].FindControl("txtgvPayto1")).Text.Trim();
                        Rowid = (this.gvPostDatCheq.PageSize) * (this.gvPostDatCheq.PageIndex) + i;
                        dt.Rows[Rowid]["payto"] = payto;

                    }
                    break;

            }
            Session["tblchkprint"] = dt;
        }
        protected void lnkbntUpPayto_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblchkprint"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string vounum = dt.Rows[i]["vounum"].ToString();
                string payto = dt.Rows[i]["payto"].ToString();

                bool result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEACTRNB",
                              vounum, payto, "", "", "", "", "", "", "", "", "", "", "", "", "");



                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Voucher Print";
                    string eventdesc = "Updated Voucher";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            this.GetVouNum();
            this.LoadGrid();

        }
        protected void btnPostDatChqOk_Click(object sender, EventArgs e)
        {
            this.PostDatChqGetVouNum();
            this.ShowPostDatCheque();
            //if (this.btnPostDatChqOk.Text == "Ok")
            //{
            //    this.btnPostDatChqOk.Text = "New";
            //    this.PostDatChqGetVouNum();
            //    this.ShowPostDatCheque();

            //}
            //else
            //{
            //    this.lmsg02.Text = "";
            //    this.btnPostDatChqOk.Text = "Ok";
            //    this.gvPostDatCheq.DataSource = null;
            //    this.gvPostDatCheq.DataBind();

            //}
        }
        protected void gvPostDatCheq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvPostDatCheq.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void lnkbntUpPayto1_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblchkprint"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string vounum = dt.Rows[i]["vounum"].ToString().Substring(0, 14);
                string chequeno = dt.Rows[i]["chequeno"].ToString();
                string payto = dt.Rows[i]["payto"].ToString();

                bool result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "UPDATEACPMNTA",
                              vounum, chequeno, payto, "", "", "", "", "", "", "", "", "", "", "", "");


                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Voucher Print";
                    string eventdesc = "Updated Voucher";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            this.PostDatChqGetVouNum();
            this.LoadGrid();
        }
        protected void imgbtnSearchChq_Click(object sender, EventArgs e)
        {
            this.GetVouNum();
        }
        protected void imgbtnSearchPChq_Click(object sender, EventArgs e)
        {
            this.PostDatChqGetVouNum();
        }
    }
}
