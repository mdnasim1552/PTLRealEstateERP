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
namespace RealERPWEB.F_81_Hrm.F_90_PF
{
    public partial class TransectionPrint : System.Web.UI.Page
    {

        public static double TAmount;
        ProcessAccess AccData = new ProcessAccess();
        Common compUtility = new Common();
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
                string Type = Request.QueryString["Type"].Trim();
                //((Label)this.Master.FindControl("lblTitle")).Text = (Type == "AccVoucher" ? "voucher print" : (Type == "AccCheque" ? "Cheque Print" : "Post Dated Cheque Print")) + " Information ";
                this.SetView();



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
        private void SetView()
        {
            string Type = Request.QueryString["Type"].Trim(); DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please Setup Start Date Firstly!" + "');", true);
                return;
            }
            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            switch (Type)
            {
                case "AccVoucher":
                    this.rbtnList1.SelectedIndex = 0;
                    this.txtfromdate.Text = startdate + date.Substring(2);
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.lstVouname.Visible = this.lstVouname.Items.Count > 0;
                    this.MultiView1.ActiveViewIndex = 0;
                    ((Label)this.Master.FindControl("lblmsg")).Text = "";
                    //this.lnkbtnDelVoucher.Visible = false;
                    break;

                case "AccCheque":
                    this.rbtCprintList.SelectedIndex = 0;
                    this.CompanyCheckPrint();
                    this.txtfromdatec.Text = startdate + date.Substring(2);
                    this.txttodatec.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    this.GetVouNum();
                    break;
                case "AccPostDatChq":
                    //this.rbtCprintList.SelectedIndex = 0;
                    //this.CompanyCheckPrint();
                    this.txtfromdatec1.Text = startdate + date.Substring(2);
                    this.txttodatec1.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 2;
                    this.PostDatChqGetVouNum();
                    break;


            }

        }
        protected void lnkbtnVouOk_Click(object sender, EventArgs e)
        {
            string Type = Request.QueryString["Type"].Trim();
            switch (Type)
            {
                case "AccVoucher":
                    this.printVou();
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
            string Caltype = ((index1 == 5) ? "DELETEDVOUCHER" : "PFGETVOUCHER");
            string cheqqueno = this.txtSearchChequeno.Text.Trim() + "%";
            DataSet ds1 = AccData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", Caltype, Voutype, frmdate, todate, cheqqueno, "", "", "", "", "");
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
            this.lstVouname.Visible = true;


            // this.lstVouname.Visible = this.lstVouname.Items.Count > 0;
            //if (Request.QueryString["Mod"].Trim() == "Management")
            //{
            //    if (this.lstVouname.Items.Count > 0)
            //    {
            //        this.lstVouname.SelectedIndex = 0;
            //        this.lnkbtnDelVoucher.Visible = true;
            //    }
            //    else
            //    {
            //        this.lnkbtnDelVoucher.Visible = false;
            //    }

            //}

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

                case "3315":
                case "3316":
                case "3317":
                case "3325":
                case "2325":
                    vouprint = "VocherPrint5";
                    break;


                case "3330":
                    vouprint = "VocherPrint6";
                    break;
                // case "3101":
                case "3332":
                    vouprint = "VocherPrintIns";
                    break;
                default:
                    vouprint = "VocherPrint";
                    break;
            }
            return vouprint;
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Type = Request.QueryString["Type"].Trim();
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
                        this.PrintCheque();
                    }
                    else
                    {
                        this.PrintChequeLand();
                    }
                    break;
                case "AccPostDatChq":
                    this.RptPostDatChq();
                    break;
            }




        }

        private void vouchertopsheetPrint()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();

            //var lst = ((DataTable)(Session["tblvoucher"])).DataTableToList<RealEntity.C_17_Acc.EClassAccVoucher>();



            //// var asa=ASTUtility.
            //string rptTitle = "Voucher Print";


            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["address"].ToString();


            //LocalReport rpt1 = new LocalReport();


            //rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.TransectionPrint", lst, null, null);


            //rpt1.SetParameters(new ReportParameter("fromDate", this.txtfromdate.Text.ToString()));
            //rpt1.SetParameters(new ReportParameter("toDate", this.txttodate.Text.ToString()));
            //rpt1.SetParameters(new ReportParameter("Title", rptTitle));
            //// rpt1.SetParameters(new ReportParameter("ComAdd", comadd));
            //rpt1.SetParameters(new ReportParameter("ComName", comnam));
            //rpt1.SetParameters(new ReportParameter("footer", "Printed From: "));

            //Session["Report1"] = rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void GetVouNum()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string chqno = "%" + this.txtSearchCheqno.Text + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdatec.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodatec.Text).ToString("dd-MMM-yyyy");
            DataSet ds4 = AccData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", "PFGETVOUCHERFORCHK", "", frmdate, todate, chqno, "", "", "", "", "");
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
            string frmdate = Convert.ToDateTime(this.txtfromdatec1.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodatec1.Text).ToString("dd-MMM-yyyy");
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
                case "3301":
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
                string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("ddMMyyyy");
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
                for (int i = 2; i <= amtdivide.Length - 1; i++)
                {
                    if (i == amtdivide.Length)
                    {
                        return;
                    }
                    else if (i > 10)
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
                hshtbl["date"] = voudat;
                hshtbl["amtWord"] = wam2.ToUpper();
                hshtbl["amtWord1"] = wam1.ToUpper();
                hshtbl["amt"] = Convert.ToDouble(amt).ToString("#,##0;(#,##0); ") + "/-";

                LocalReport rpt1 = new LocalReport();

                rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptCheque", hshtbl, null, null);

                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = hst["comcod"].ToString();
                //string vounum = this.ddlChkVouNo.SelectedValue.ToString();
                //DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PRINTCHECK", vounum, "", "", "", "", "", "", "", "");
                //if (_ReportDataSet == null)
                //    return;
                //DataTable dt1 = _ReportDataSet.Tables[0];
                //string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("ddMMyyyy");
                //voudat = voudat.Substring(0, 1) + "   " + voudat.Substring(1, 1) + "   " + voudat.Substring(2, 1) + "   " + voudat.Substring(3, 1) + "   " + voudat.Substring(4, 1) + "   " + voudat.Substring(5, 1) + "   " + voudat.Substring(6, 1) + "   " + voudat.Substring(7, 1);
                //string payto = dt1.Rows[0]["payto"].ToString();
                //double amt = Convert.ToDouble(dt1.Rows[0]["tamt"].ToString());
                //string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                //int len = amt1.Length;
                //string amt2 = amt1.Substring(7, (len - 8));
                //string Chequeprint = this.CompanyPrintCheque();
                //ReportDocument rptinfo = new ReportDocument();
                //if (Chequeprint == "PrintCheque01")
                //    rptinfo = new RealERPRPT.R_17_Acc.PrintCheck();
                //else if (Chequeprint == "PrintChequeAssure")
                //    rptinfo = new RealERPRPT.R_17_Acc.PrintCheequeAssure();
                //else
                //    rptinfo = new RealERPRPT.R_17_Acc.PrintCheeque02();
                //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //date.Text = voudat;
                //TextObject rpttxtpayto = rptinfo.ReportDefinition.ReportObjects["txtpayto"] as TextObject;
                //rpttxtpayto.Text = payto;
                //TextObject rpttxtamtinword = rptinfo.ReportDefinition.ReportObjects["txtamtinword"] as TextObject;
                //rpttxtamtinword.Text = amt2;
                //TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                //rpttxtamt.Text = amt.ToString("#,##0;(#,##0); ") + "/=";

                //if (ConstantInfo.LogStatus == true)
                //{
                //    string eventtype = "Voucher Print";
                //    string eventdesc = "Print Cheque";
                //    string eventdesc2 = "Voucher No.: " + vounum;
                //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                //}
                //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                //Session["Report1"] = rptinfo;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            catch (Exception ex)
            {
                this.lmsg01.Text = "Error:" + ex.Message;
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
                this.lmsg01.Text = "Error:" + ex.Message;
            }



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
                DataTable dt1 = _ReportDataSet.Tables[0];
                string chequedat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");
                chequedat = chequedat.Substring(0, 1) + "   " + chequedat.Substring(1, 1) + "   " + chequedat.Substring(2, 1) + "   " + chequedat.Substring(3, 1) + "   " + chequedat.Substring(4, 1) + "   " + chequedat.Substring(5, 1) + "   " + chequedat.Substring(6, 1) + "   " + chequedat.Substring(7, 1);
                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["trnam"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));


                string Chequeprint = this.CompanyPrintCheque();
                ReportDocument rptinfo = new ReportDocument();




                if (Chequeprint == "PrintCheque01")
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheck();
                else if (Chequeprint == "PrintCheque02")
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheeque02();
                else if (Chequeprint == "PrintChequeAssure")
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheequeAssure();
                else
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheckHolding();


                TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                date.Text = chequedat;
                TextObject rpttxtpayto = rptinfo.ReportDefinition.ReportObjects["txtpayto"] as TextObject;
                rpttxtpayto.Text = payto;
                TextObject rpttxtamtinword = rptinfo.ReportDefinition.ReportObjects["txtamtinword"] as TextObject;
                rpttxtamtinword.Text = amt2;
                TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                rpttxtamt.Text = amt.ToString("#,##0;(#,##0); ") + "/=";

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Voucher Print";
                    string eventdesc = "Top Sheet";
                    string eventdesc2 = "Voucher No.: " + vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
                rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                Session["Report1"] = rptinfo;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            catch (Exception ex)
            {
                this.lmsg01.Text = "Error:" + ex.Message;
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
                string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                string vounum = this.lstVouname.SelectedValue.ToString();


                string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
                string currentptah = "AccPrint.aspx?Type=PostDatVou&vounum=" + vounum;
                string totalpath = hostname + currentptah;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";



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
                //ReportDocument rptinfo = new ReportDocument();
                //if (Type == "VocherPrint")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher();
                //}
                //else if (Type == "VocherPrint1")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher1();

                //}
                //else if (Type == "VocherPrint2")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher2();
                //}

                //else if (Type == "VocherPrint3")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher3();
                //}
                //else if (Type == "VocherPrint5")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher5();
                //}


                //else if (Type == "VocherPrint6")

                //{

                //    LocalReport Rpt1 = new LocalReport(); 
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
                //    Rpt1.SetParameters(new ReportParameter("InWrd", ASTUtility.Trans(Math.Round(TAmount), 2)));
                //    Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                //    Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

                //    Session["Report1"] = Rpt1;
                //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

                //    return;

                //}
                ////else if (Type == "VocherPrintIns")
                ////{
                ////    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherIns();
                ////}

                //else
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher4();
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

                //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //TextObject rpttxtVoutype = rptinfo.ReportDefinition.ReportObjects["txtVoutype"] as TextObject;
                //rpttxtVoutype.Text = voutype;
                //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //vounum1.Text = "Voucher No: " + vounum;
                //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //date.Text = " Date:" + voudat;
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
                case "3332":
                    // case "3101":
                    printinstar = "Innstar";
                    break;

                default:

                    break;


            }
            return printinstar;
        }
        private void Printvoucher()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                // (hst["comadd1"].ToString().Replace("<br />", "\n")).ToString();
                string comadd = hst["comadd1"].ToString();
                string combranch = hst["combranch"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string vounum = this.lstVouname.SelectedValue.ToString();
                //string PrintInstar = this.GetCompInstar();
                //string Calltype = (this.rbtnList1.SelectedIndex == 5) ? "PRINTDELETEDVOUCHER01" : "PFPRINTVOUCHER01";
                //DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", Calltype, vounum, PrintInstar, "", "", "", "", "", "", "");
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
                ////string Vounum = dt1.Rows[0]["vounum"].ToString();
                //string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                //string refnum = dt1.Rows[0]["refnum"].ToString();
                //string payto = dt1.Rows[0]["payto"].ToString();
                //string Partytype = (ASTUtility.Left(vounum, 2) == "BC") ? "Recieved From:" : (ASTUtility.Left(vounum, 2) == "CC") ? "Recieved From:" : "Pay To:";
                //string voutype = dt1.Rows[0]["voutyp"].ToString();
                //string venar = dt1.Rows[0]["venar"].ToString();
                //string Isunum = (dt1.Rows[0]["isunum"]).ToString() == "" ? "" : ASTUtility.Right((dt1.Rows[0]["isunum"]).ToString(), 6);
                //string Posteddat = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
                //string Type = this.CompanyPrintVou();


                //string vounum = dt1.Rows[0]["vounum"].ToString();

                string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
                string currentptah = "AccPrint.aspx?Type=PfaccVou&vounum=" + vounum;
                string totalpath = hostname + currentptah;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";



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
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                //               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }


        }


        protected void lnkbtnChkOk_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnChkOk.Text == "Ok")
            {
                this.lnkbtnChkOk.Text = "New";
                this.GetVouNum();
                this.ShowCheque();

            }
            else
            {
                this.lmsg01.Text = "";
                this.lnkbtnChkOk.Text = "Ok";
                this.gvCheque.DataSource = null;
                this.gvCheque.DataBind();

            }




        }

        private void ShowCheque()
        {
            Session.Remove("tblchkprint");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Voutype = "BD";
            string frmdate = Convert.ToDateTime(this.txtfromdatec.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodatec.Text).ToString("dd-MMM-yyyy");
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
            string frmdate = Convert.ToDateTime(this.txtfromdatec1.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodatec1.Text).ToString("dd-MMM-yyyy");
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
            string Type = Request.QueryString["Type"].Trim();
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
            string Type = Request.QueryString["Type"].Trim();
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
                this.lmsg01.Text = "You have no permission";
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

            this.lmsg01.Text = "Updated Successfully";
            this.GetVouNum();
            this.LoadGrid();

        }




        protected void btnPostDatChqOk_Click(object sender, EventArgs e)
        {
            if (this.btnPostDatChqOk.Text == "Ok")
            {
                this.btnPostDatChqOk.Text = "New";
                this.PostDatChqGetVouNum();
                this.ShowPostDatCheque();

            }
            else
            {
                this.lmsg02.Text = "";
                this.btnPostDatChqOk.Text = "Ok";
                this.gvPostDatCheq.DataSource = null;
                this.gvPostDatCheq.DataBind();

            }
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
                this.lmsg02.Text = "You have no permission";
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

            this.lmsg02.Text = "Updated Successfully";
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