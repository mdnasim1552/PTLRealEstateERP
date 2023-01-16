using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_17_Acc
{
    public partial class RptTransactionSearch03 : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)

            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Transaction Search - 03";
                //this.Master.Page.Title = "Transaction Search";
                this.Accpayee();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void Accpayee()
        {
            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3101":
                case "2305":
                case "3305":
                case "3306":
                case "3309":
                case "3310":
                case "3311":
                    this.ChboxPayee.Checked = true;
                    break;

                default:
                    this.ChboxPayee.Checked = false;
                    break;
            }
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            this.lbljavascript.Text = "";

            if (this.chkPrint.Checked)
            {
                this.RptPostDatChq();
            }
            else
            {
                this.VouPrint();
            }
        }
        protected void lnkOk_Click(object sender, EventArgs e)
        {

            this.Pnlmain.Visible = true;
            this.PnlNarration.Visible = true;
            string comcod = this.GetCompCode();
            string SearchOption = "";

            if (this.txtvoudate.Text.Length > 0)
            {
                SearchOption = SearchOption + "voudat='" + ASTUtility.DateFormat(txtvoudate.Text.Trim()) + "' and ";
            }
            if (this.txtBankDesc.Text.Length > 0)
            {
                SearchOption = SearchOption + "cactdesc like '%" + this.txtBankDesc.Text.Trim() + "%' and ";
            }

            if (this.txtAccountHead.Text.Length > 0)
            {
                SearchOption = SearchOption + " actdesc like '%" + this.txtAccountHead.Text.Trim() + "%'  and ";
            }

            if (this.txtDetailsHead.Text.Length > 0)
            {
                SearchOption = SearchOption + "resdesc like '%" + this.txtDetailsHead.Text.Trim() + "%' and ";
            }
            if (this.txtamount.Text.Length > 0)
            {
                SearchOption = SearchOption + "amount='" + this.txtamount.Text.Trim() + "' and ";
            }
            if (this.txtchequeno.Text.Length > 0)
            {
                SearchOption = SearchOption + "chequeno like '%" + this.txtchequeno.Text.Trim() + "%' and ";
            }
            if (this.txtissueno.Text.Length > 0)
            {
                SearchOption = SearchOption + "isunum like '%" + this.txtissueno.Text.Trim() + "%' and ";
            }
            if (this.txtchequedate.Text.Length > 0)
            {
                SearchOption = SearchOption + "chequedat='" + ASTUtility.DateFormat(this.txtchequedate.Text.Trim()) + "' and ";
            }
            if (this.txtpayto.Text.Length > 0)
            {
                SearchOption = SearchOption + "payto like '%" + this.txtpayto.Text.Trim() + "%' and ";
            }
            if (this.txtnarration.Text.Length > 0)
            {
                SearchOption = SearchOption + "venar like '%" + this.txtnarration.Text.Trim() + "%' and ";
            }


            SearchOption = (SearchOption.Length) > 0 ? ASTUtility.Left(SearchOption, (SearchOption.Length - 4)) : SearchOption;

            //string SearchOption=(this.txtvoudate.Text.Length>0)?"voudat='"+ASTUtility.DateFormat(txtvoudate.Text.Trim())+"'"
            //    :  (this.txtBankDesc.Text.Length>0)?" cactdesc like '%"+this.txtBankDesc.Text.Trim()+"%'"
            //    : (this.txtAccountHead.Text.Length>0)?" actdesc like '%"+this.txtAccountHead.Text.Trim()+"%'"
            //    : (this.txtDetailsHead.Text.Length > 0) ? " resdesc like '%" + this.txtDetailsHead.Text.Trim() + "%'"
            //    : (this.txtamount.Text.Length > 0) ? "amount='" + this.txtamount.Text.Trim() + "'"
            //    :  (this.txtchequeno.Text.Length>0)?"chequeno like '%"+this.txtchequeno.Text.Trim()+"%'"
            //    : (this.txtissueno.Text.Length > 0) ? "isunum like '%" + this.txtissueno.Text.Trim() + "%'"
            //    : (this.txtchequedate.Text.Length > 0) ? "chequedat='" + ASTUtility.DateFormat(this.txtchequedate.Text.Trim()) + "'"

            //    : (this.txtpayto.Text.Length>0)?" payto like '%"+this.txtpayto.Text.Trim()+"%'"
            //    :  (this.txtnarration.Text.Length>0)?"venar like '%"+this.txtnarration.Text.Trim()+"%'":"";



            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "GETPAYVOUCHER", SearchOption, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.lstVouname.Items.Clear();
                return;
            }

            this.lstVouname.DataTextField = "vounum1";
            this.lstVouname.DataValueField = "vounum";
            this.lstVouname.DataSource = ds1.Tables[0];
            this.lstVouname.DataBind();
            ds1.Dispose();
            this.lstVouname.Focus();
            if (this.lstVouname.Items.Count > 0)
                this.lstVouname.SelectedIndex = 0;
            this.ShowVoucher();



        }


        protected void lnkRefresh_Click(object sender, EventArgs e)
        {
            this.txtvoudate.Text = "";
            this.txtBankDesc.Text = "";
            this.txtAccountHead.Text = "";
            this.txtDetailsHead.Text = "";
            this.txtamount.Text = "";
            this.txtchequeno.Text = "";
            this.txtissueno.Text = "";
            this.txtchequedate.Text = "";
            this.txtpayto.Text = "";
            this.txtnarration.Text = "";
        }
        private void ShowVoucher()
        {
            string comcod = this.GetCompCode();
            string vounum = this.lstVouname.SelectedValue.ToString();

            DataSet _EditDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "EDITVOUCHER", vounum, "", "", "", "", "", "", "", "");
            DataTable dt = this.HiddenSameData(_EditDataSet.Tables[0]);

            if (dt.Rows.Count == 0)
                return;

            DataTable dtedit = _EditDataSet.Tables[1];
            this.lblvalVoucherDate.Text = Convert.ToDateTime(dtedit.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
            this.lblvalVoucherNo.Text = dtedit.Rows[0]["vounum"].ToString().Substring(0, 2) + "-" + dtedit.Rows[0]["vounum"].ToString().Substring(6);
            this.lblValBankDescription.Text = dtedit.Rows[0]["cactdesc"].ToString();
            this.lblvalNarration.Text = dtedit.Rows[0]["venar"].ToString();
            this.dgv1.DataSource = dt;
            this.dgv1.DataBind();

            ((Label)this.dgv1.FooterRow.FindControl("lblFgvDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trndram)", "")) ?
                           0 : dt.Compute("sum(trndram)", ""))).ToString("#,##0;(#,##0); ");


        }
        protected void txtvoudate_TextChanged(object sender, EventArgs e)
        {
            this.txtvoudate.Text = ASTUtility.DateInVal(this.txtvoudate.Text);
            this.txtBankDesc.Focus();
        }
        protected void txtchequedate_TextChanged(object sender, EventArgs e)
        {
            this.txtchequedate.Text = ASTUtility.DateInVal(this.txtchequedate.Text);
            this.txtpayto.Focus();

        }
        protected void lstVouname_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowVoucher();

            this.lstVouname.Focus();
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string actcode = dt1.Rows[0]["actcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode)
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";

                }

                else
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();

                }

            }
            return dt1;

        }



        private void VouPrint()
        {



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string curvoudat = this.lblvalVoucherDate.Text.Substring(0, 11);
            string vounum = this.lblvalVoucherNo.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                    this.lblvalVoucherNo.Text.Trim().Substring(3);

            string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
            string currentptah = "AccPrint.aspx?Type=PostDatVou&vounum=" + vounum;
            string totalpath = hostname + currentptah;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";


            //DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");
            //if (_ReportDataSet == null)
            //    return;
            //DataTable dt = _ReportDataSet.Tables[0];
            //if (dt.Rows.Count == 0)
            //    return;
            //double TAmount, dramt, cramt;
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

            //else
            //{
            //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher4();
            //}


            ////-----------------------------
            //DataTable dt1 = _ReportDataSet.Tables[1];
            //string Vounum = dt1.Rows[0]["vounum"].ToString();
            //string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
            //string voutype = dt1.Rows[0]["voutyp"].ToString();
            //string venar = dt1.Rows[0]["venar"].ToString();

            //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
            //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //txtCompanyName.Text = comnam;
            //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
            //txtcAdd.Text = comadd;
            //TextObject txtPay = rptinfo.ReportDefinition.ReportObjects["txtPay"] as TextObject;
            //txtPay.Text = (vounum.Substring(0, 2).ToString() == "PV") ? "Pay To " : "Receive From";

            //TextObject rpttxtVoutype = rptinfo.ReportDefinition.ReportObjects["txtVoutype"] as TextObject;
            //rpttxtVoutype.Text = voutype;
            //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //vounum1.Text = "Voucher No: " + Vounum;
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
            //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private string CompanyPrintCheque()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string chequeprint = "";
            switch (comcod)
            {

                case "2305":
                case "3305":
                case "3306":
                case "3307":
                case "3308":
                case "3309":
                case "3311":
                case "3310":




                    chequeprint = "PrintCheque01";
                    break;


                case "1301":
                case "2301":
                case "3301":
                    chequeprint = "PrintCheque02";
                    break;

                //case "3306":
                //    chequeprint = "PrintCheque03";
                //    break;

                //case "3309":
                //    chequeprint = "PrintCheque04";
                //    break;
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
        //private void RptPostDatChq()
        //{
        //    try
        //    {

        //        Hashtable hst = (Hashtable)Session["tblLogin"];
        //        string comcod = hst["comcod"].ToString();
        //        string vounum = this.ddlChqList.SelectedValue.Substring(0, 14);
        //        string chqno = this.ddlChqList.SelectedValue.Substring(14);
        //        string type = this.CompanyPrintCheque();
        //        DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "RPTPOSTDATCHECK", vounum, chqno, "", "", "", "", "", "", "");
        //        if (_ReportDataSet == null)
        //            return;
        //        DataTable dt1 = _ReportDataSet.Tables[0];
        //        string chequedat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");
        //        chequedat = chequedat.Substring(0, 1) + "   " + chequedat.Substring(1, 1) + "   " + chequedat.Substring(2, 1) + "   " + chequedat.Substring(3, 1) + "   " + chequedat.Substring(4, 1) + "   " + chequedat.Substring(5, 1) + "   " + chequedat.Substring(6, 1) + "   " + chequedat.Substring(7, 1);
        //        string payto = dt1.Rows[0]["payto"].ToString();
        //        double amt = Convert.ToDouble(dt1.Rows[0]["trnam"].ToString());
        //        string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
        //        int len = amt1.Length;
        //        string amt2 = amt1.Substring(7, (len - 8));
        //        ReportDocument rptinfo = new RealERPRPT.R_17_Acc.PrintCheck();
        //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
        //        date.Text = chequedat;
        //        TextObject rpttxtpayto = rptinfo.ReportDefinition.ReportObjects["txtpayto"] as TextObject;
        //        rpttxtpayto.Text = payto;
        //        TextObject rpttxtamtinword = rptinfo.ReportDefinition.ReportObjects["txtamtinword"] as TextObject;
        //        rpttxtamtinword.Text = amt2;
        //        TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
        //        rpttxtamt.Text = amt.ToString("#,##0;(#,##0); ") + "/=";

        //        if (ConstantInfo.LogStatus == true)
        //        {
        //            string eventtype = "Post Dated Cheque";
        //            string eventdesc = "Print Cheque";
        //            string eventdesc2 = vounum + "  " + chqno;
        //            bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //        }
        //        rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
        //        Session["Report1"] = rptinfo;
        //        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('"+ex.Message+"');", true);
        //    }

        //}

        private void RptPostDatChq()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string vounum = this.ddlChqList.SelectedValue.Substring(0, 14);
                string chqno = this.ddlChqList.SelectedValue.Substring(14);
                DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "RPTPOSTDATCHECK", vounum, chqno, "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                DataTable dt2 = _ReportDataSet.Tables[1];



                //  voudat = voudat.Substring(0, 1) + "   " + voudat.Substring(1, 1) + "   " + voudat.Substring(2, 1) + "   " + voudat.Substring(3, 1) + "   " + voudat.Substring(4, 1) + "   " + voudat.Substring(5, 1) + "   " + voudat.Substring(6, 1) + "   " + voudat.Substring(7, 1);

                string chequedat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");


                if (chequedat != "01011900")
                {


                    chequedat = chequedat.Substring(0, 1) + "   " + chequedat.Substring(1, 1) + "   " + chequedat.Substring(2, 1) + "   " + chequedat.Substring(3, 1) + "   " + chequedat.Substring(4, 1) + "   " + chequedat.Substring(5, 1) + "   " + chequedat.Substring(6, 1) + "   " + chequedat.Substring(7, 1);



                }
                else
                {
                    chequedat = "";
                }
                string payto = dt1.Rows[0]["payto"].ToString();
                string banktype = dt2.Rows[0]["bnkcode"].ToString();

                double amt = Convert.ToDouble(dt1.Rows[0]["trnam"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));

                string value = ChboxPayee.Checked ? "A/C Payee" : "";
                if (ChboxPayee.Checked)
                {
                    _ReportDataSet.Tables[0].Rows[0]["acpay"] = value;
                }
                else
                {
                    _ReportDataSet.Tables[0].Rows[0]["acpay"] = value;
                }

                string type = this.CompanyPrintCheque();
                ReportDocument rptinfo = new ReportDocument();
                if (type == "PrintCheque01")

                    if (banktype == "SBL")
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheck();
                    }

                    else if (banktype == "JBL")
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheckJBL();

                    }

                    else if (banktype == "MBL" || banktype == "AFL")
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheckMBL();

                    }

                    else if (banktype == "PBL")
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheckPBL();

                    }

                    else if (banktype == "SDBL")
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheckSDBL();

                    }

                    else if (banktype == "IFIC" || banktype == "BAL")
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheckIFIC();

                    }
                    else
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheck();

                    }







                else if (type == "PrintCheque02")
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheeque02();
                else if (type == "PrintChequeAssure")
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheequeAssure();

                //else rptinfo = new MFGRPT.R_15_Acc.PrintCheckHolding(); 




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
                    string eventtype = "Post Dated Cheque";
                    string eventdesc = "Print Cheque";
                    string eventdesc2 = vounum + "  " + chqno;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
                rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                Session["Report1"] = rptinfo;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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
                case "3306":
                case "3307":
                case "3308":
                    vouprint = "VocherPrint1";
                    break;
                case "3101":
                case "3305":
                case "3311":
                case "3310":
                    vouprint = "VocherPrint2";
                    break;
                case "3309":
                    vouprint = "VocherPrint3";
                    break;
                default:
                    vouprint = "VocherPrint";
                    break;
            }
            return vouprint;
        }

        protected void chkPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrint.Checked)
            {
                this.ddlChqList.Visible = true;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string curvoudat = this.lblvalVoucherDate.Text.Substring(0, 11);
                string vounum = this.lblvalVoucherNo.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                        this.lblvalVoucherNo.Text.Trim().Substring(3);

                DataSet ds5 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "POSTDATCHECKPRT", vounum, "", "", "", "", "", "", "", "");

                this.ddlChqList.DataTextField = "vounum1";
                this.ddlChqList.DataValueField = "vounum";
                this.ddlChqList.DataSource = ds5.Tables[0];
                this.ddlChqList.DataBind();

            }
            else
            {
                this.ddlChqList.Items.Clear();
                this.ddlChqList.Visible = false;
            }
        }

    }
}