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
    public partial class RptAccVouher : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (this.Request.QueryString["vounum"].ToString().Substring(0, 2) == "JV")
                {
                    this.lblBankDescription.Visible = false;
                    this.lblValBankDescription.Visible = false;

                }


                string title = (this.Request.QueryString["vounum"].ToString().Substring(0, 2) == "BD") ? "Payment Voucher :"

                    : (this.Request.QueryString["vounum"].ToString().Substring(0, 2) == "CD") ? "Payment Voucher  :"
                : (this.Request.QueryString["vounum"].ToString().Substring(0, 2) == "BC") ? "Received Voucher"
                : (this.Request.QueryString["vounum"].ToString().Substring(0, 2) == "CC") ? "Received Voucher"

                 : (this.Request.QueryString["vounum"].ToString().Substring(0, 2) == "CT") ? "Contra Voucher" : "Journal Voucher";
                ((Label)this.Master.FindControl("lblTitle")).Text = title;

                this.Master.Page.Title = title;

                if (this.Request.QueryString["vounum"].ToString().Substring(0, 2) == "JV")
                {
                    this.lblBankDescription.Visible = false;
                    this.lblValBankDescription.Visible = false;

                }


                this.ShowVoucher();



            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        protected string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }


        private void ShowVoucher()
        {
            string comcod = this.GetCompCode();
            string vounum = this.Request.QueryString["vounum"].ToString();

            DataSet _EditDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "EDITVOUCHER", vounum, "", "", "", "", "", "", "", "");
            DataTable dt = this.HiddenSameData(_EditDataSet.Tables[0]);

            if (dt.Rows.Count == 0)
                return;

            DataTable dtedit = _EditDataSet.Tables[1];
            this.lblvalVoucherDate.Text = Convert.ToDateTime(dtedit.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
            this.lblvalVoucherNo.Text = dtedit.Rows[0]["vounum"].ToString().Substring(0, 2) + "-" + dtedit.Rows[0]["vounum"].ToString().Substring(6);
            this.lblValBankDescription.Text = dtedit.Rows[0]["cactdesc"].ToString();

            this.lblisunum.Text = dtedit.Rows[0]["isunum"].ToString();
            this.lblvalRefNum.Text = dtedit.Rows[0]["refnum"].ToString();
            this.lblvalSirinfo.Text = dtedit.Rows[0]["srinfo"].ToString();


            this.lblvalpayto.Text = dtedit.Rows[0]["payto"].ToString();
            this.lblvalNarration.Text = dtedit.Rows[0]["venar"].ToString();
            this.dgv1.DataSource = dt;
            this.dgv1.DataBind();

            double dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trndram)", "")) ? 0 : dt.Compute("sum(trndram)", "")));
            double cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trncram)", "")) ? 0 : dt.Compute("sum(trncram)", "")));

            ((Label)this.dgv1.FooterRow.FindControl("lblFgvDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trndram)", "")) ?
                           0 : dt.Compute("sum(trndram)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.dgv1.FooterRow.FindControl("txtFgvCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trncram)", "")) ?
                  0 : dt.Compute("sum(trncram)", ""))).ToString("#,##0.00;(#,##0.00); ");

            double txttoamt = Math.Abs((dramt - cramt));
            this.lblInword.Text = (dtedit.Rows[0]["vounum"].ToString().Substring(0, 2) == "JV") ? "" : "Total:" + (txttoamt > 0 ? txttoamt.ToString("#,##0;(#,##0); ") : "");
            
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Click " + ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Click " + ((Label)this.Master.FindControl("lblTitle")).Text;

                string eventdesc2 = " Voucher No "+ this.lblvalVoucherNo.Text + " Date : " + this.lblvalVoucherDate.Text + " Bank Name : " + this.lblValBankDescription.Text;

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


            //-------------------------------------------------//
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


                case "3101":
                case "3333":
                    vouprint = "VocherPrintMod";
                    break;


                default:
                    vouprint = "VocherPrintMod";
                    break;
            }
            return vouprint;
        }

        private string GetCompInstar()
        {

            string comcod = this.GetCompCode();
            string printinstar = "";
            switch (comcod)
            {
                case "3330":
                    break;

                default:
                    printinstar = "Innstar";
                    break;


            }
            return printinstar;
        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string curvoudat = this.lblvalVoucherDate.Text.Substring(0, 11);
            string vounum = this.lblvalVoucherNo.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
            this.lblvalVoucherNo.Text.Trim().Substring(3);
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccPrint.aspx?Type=accVou&vounum=" +
                               vounum + "', target='_blank');</script>";




            //    try
            //    {

            //        Hashtable hst = (Hashtable)Session["tblLogin"];
            //        string comcod = hst["comcod"].ToString();
            //        string comnam = hst["comnam"].ToString();
            //        // (hst["comadd1"].ToString().Replace("<br />", "\n")).ToString();
            //        string comadd = hst["comadd1"].ToString();
            //        string combranch = hst["combranch"].ToString();
            //        string compname = hst["compname"].ToString();
            //        string username = hst["username"].ToString();
            //        string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //        string curvoudat = this.lblvalVoucherDate.Text.Substring(0, 11);
            //        string vounum = this.lblvalVoucherNo.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
            //                this.lblvalVoucherNo.Text.Trim().Substring(3);


            //        string PrintInstar = this.GetCompInstar();

            //      //  string CallType = (this.chkpost.Checked && aprovbyid.Length > 0) ? "PRINTVOUCHER01" : (this.chkpost.Checked) ? "PRINTUNPOSTEDVOUCHER01" : "PRINTVOUCHER01";



            //        DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, PrintInstar, "", "", "", "", "", "", "");



            //        if (_ReportDataSet == null)
            //            return;
            //        DataTable dt = _ReportDataSet.Tables[0];
            //        if (dt.Rows.Count == 0)
            //            return;
            //        double TAmount, dramt, cramt;
            //        dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
            //        cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));



            //        if (dramt > 0 && cramt > 0)
            //        {
            //            TAmount = cramt;

            //        }
            //        else if (dramt > 0 && cramt <= 0)
            //        {
            //            TAmount = dramt;
            //        }
            //        else
            //        {
            //            TAmount = cramt;
            //        }


            //        DataTable dt1 = _ReportDataSet.Tables[1];
            //        string Vounum = dt1.Rows[0]["vounum"].ToString();
            //        string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
            //        string refnum = dt1.Rows[0]["refnum"].ToString();
            //        string payto = dt1.Rows[0]["payto"].ToString();
            //        string receivedBank = dt1.Rows[0]["banknam"].ToString();
            //        string Partytype = (ASTUtility.Left(vounum, 2) == "BC") ? "Recieved From:" : (ASTUtility.Left(vounum, 2) == "CC") ? "Recieved From:" : "Pay To:";
            //        string voutype = dt1.Rows[0]["voutyp"].ToString();
            //        string venar = dt1.Rows[0]["venar"].ToString();
            //        string Isunum = (dt1.Rows[0]["isunum"]).ToString() == "" ? "" : ASTUtility.Right((dt1.Rows[0]["isunum"]).ToString(), 6);
            //        string Posteddat = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
            //        string Type = this.CompanyPrintVou();



            //        string billno = dt.Rows[0]["billno"].ToString();
            //        string billno1 = dt.Rows[0]["billno"].ToString();
            //        //string[] billno1;

            //        //string[] billno;

            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            // dt1.Rows[j]["useridapp"].ToString() == useridapp

            //            if (billno1 == dt.Rows[i]["billno"].ToString())
            //            {

            //            }

            //            else
            //            {
            //                billno += dt.Rows[i]["billno"].ToString() + (((dt.Rows[i]["billno"].ToString()).Length == 0) ? " " : ", ");
            //            }


            //            billno1 = dt.Rows[i]["billno"].ToString();

            //        }

            //        //int l = billno.Trim().Length;
            //        //billno = billno.Substring(0, l - 1);

            //        int l = billno.Trim().Length;
            //        billno = (billno.Length == 0) ? "" : billno.Substring(0, l - 1);

            //        ReportDocument rptinfo = new ReportDocument();
            //        if (Type == "VocherPrint")
            //        {
            //            rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();
            //        }
            //        else if (Type == "VocherPrint1")
            //        {
            //            rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher1();
            //            TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
            //            txtisunum1.Text = "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6));
            //            TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
            //            txtPosteddate1.Text = "Entry Date: " + Posteddat;

            //        }
            //        else if (Type == "VocherPrint2")
            //        {
            //            rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher2();
            //            TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
            //            txtisunum2.Text = "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6));
            //            TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
            //            txtPosteddate2.Text = "Entry Date: " + Posteddat;
            //        }


            //        else if (Type == "VocherPrint5")
            //        {
            //            rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher5();
            //            TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
            //            txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
            //            TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
            //            txtisunum2.Text = "Issue No: " + Isunum;
            //            TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
            //            txtPosteddate2.Text = "Entry Date: " + Posteddat;
            //            TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //            Refnum.Text = "Cheque/Ref. No.: " + refnum;
            //            TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //            rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
            //            TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //            naration.Text = "Narration: " + venar;
            //            TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //            vounum1.Text = "Voucher No.: " + vounum;
            //            TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //            date.Text = "Voucher Date: " + voudat;
            //        }

            //        else if (Type == "VocherPrint3")
            //        {
            //            rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher3();
            //            TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
            //            txtisunum2.Text = "Issue No: " + Isunum;
            //            TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
            //            txtPosteddate2.Text = "Entry Date: " + Posteddat;
            //            TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //            Refnum.Text = "Cheque/Ref. No.: " + refnum;
            //            TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //            rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto;  //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
            //            TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //            naration.Text = "Narration: " + venar;
            //            TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //            vounum1.Text = "Voucher No.: " + vounum;
            //            TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //            date.Text = "Voucher Date: " + voudat;
            //        }

            //        else if (Type == "VocherPrint5")
            //        {
            //            rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher5();
            //            TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
            //            txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
            //            TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
            //            txtisunum1.Text = "Issue No: " + Isunum;
            //            TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
            //            txtPosteddate1.Text = "Entry Date: " + Posteddat;
            //            TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //            Refnum.Text = "Cheque/Ref. No.: " + refnum;
            //            TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //            rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
            //            TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //            naration.Text = "Narration: " + venar;
            //            TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //            vounum1.Text = "Voucher No.: " + vounum;
            //            TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //            date.Text = "Voucher Date: " + voudat;

            //        }
            //        else if (Type == "VocherPrint6")
            //        {
            //            rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucherBridge();
            //            TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //            Refnum.Text = "Cheque/Ref. No.: " + refnum;
            //            TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //            rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
            //            TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //            naration.Text = "Narration: " + venar;
            //            TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //            vounum1.Text = "Voucher No.: " + vounum;
            //            TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //            date.Text = "Voucher Date: " + voudat;



            //        }

            //        else if (Type == "VocherPrintIns")
            //        {
            //            if (ASTUtility.Left(vounum, 2) == "JV")
            //            {
            //                rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherIns();

            //                //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //                //txtCompanyName.Text = comnam;
            //                //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
            //                //txtcAdd.Text = comadd;
            //                TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //                vounum1.Text = "Voucher No.: " + vounum;
            //                TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //                date.Text = "Voucher Date: " + voudat;
            //                //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //                //Refnum.Text = "Cheque/Ref. No.: " + refnum;
            //                //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //                //rpttxtPartyName.Text = "";// (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
            //                //TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
            //                //voutype1.Text = voutype;
            //                TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //                naration.Text = "Narration: " + venar;
            //            }

            //            else
            //            {
            //                rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherIns02();
            //                TextObject txtDesc = rptinfo.ReportDefinition.ReportObjects["txtDesc"] as TextObject;
            //                txtDesc.Text = dt1.Rows[0]["cactdesc"].ToString();// this.ddlConAccHead.SelectedItem.Text.Substring(13).ToString();
            //                TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //                rpttxtPartyName.Text = (payto == "") ? "" : payto;



            //                TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //                naration.Text = venar;

            //                TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
            //                txtusername.Text = username;

            //                TextObject txtProject = rptinfo.ReportDefinition.ReportObjects["txtProject"] as TextObject;
            //                txtProject.Text = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";

            //                TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //                vounum1.Text = vounum;
            //                TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //                date.Text = voudat;
            //                TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //                Refnum.Text = refnum;

            //            }



            //        }

            //        else if (Type == "VocherPrintMod")
            //        {

            //            if (ASTUtility.Left(vounum, 2) == "JV")
            //            {
            //                rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli();

            //                TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //                vounum1.Text = "Voucher No.: " + vounum;
            //                TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //                date.Text = "Voucher Date: " + voudat;
            //                TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //                naration.Text = "Narration: " + venar;
            //            }




            //            else
            //            {
            //                string vouno = vounum.Substring(0, 2);
            //                string paytoorecived = (vouno == "BC" || vouno == "CC") ? "Recieved From" : "Pay To";

            //                if (vouno == "BC" || vouno == "CC")
            //                {
            //                    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli02();
            //                    TextObject txtDesc = rptinfo.ReportDefinition.ReportObjects["txtDesc"] as TextObject;
            //                    txtDesc.Text = dt1.Rows[0]["cactdesc"].ToString();// this.ddlConAccHead.SelectedItem.Text.Substring(13).ToString();
            //                    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //                    rpttxtPartyName.Text = (payto == "") ? "" : payto;


            //                    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //                    naration.Text = venar;

            //                    TextObject txtporrecieved = rptinfo.ReportDefinition.ReportObjects["txtporrecieved"] as TextObject;
            //                    txtporrecieved.Text = paytoorecived;

            //                    TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
            //                    txtusername.Text = username;

            //                    TextObject txtProject = rptinfo.ReportDefinition.ReportObjects["txtProject"] as TextObject;
            //                    txtProject.Text = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";

            //                    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //                    vounum1.Text = vounum;
            //                    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //                    date.Text = voudat;
            //                    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //                    Refnum.Text = refnum;
            //                    TextObject txtReceivedBank = rptinfo.ReportDefinition.ReportObjects["txtReceivedBank"] as TextObject;
            //                    txtReceivedBank.Text = receivedBank;


            //                }

            //                else
            //                {
            //                    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli03();

            //                    if (vouno == "BD" || vouno == "CD")
            //                    {

            //                        //TextObject txtBillno = rptinfo.ReportDefinition.ReportObjects["txtBillno"] as TextObject;
            //                        //txtBillno.Text = "Bill No : " + billno;
            //                        TextObject txtissuno = rptinfo.ReportDefinition.ReportObjects["txtissuno"] as TextObject;
            //                        txtissuno.Text = "Payment ID : " + dt1.Rows[0]["isunum"].ToString();

            //                    }

            //                    TextObject txtDesc = rptinfo.ReportDefinition.ReportObjects["txtDesc"] as TextObject;
            //                    txtDesc.Text = dt1.Rows[0]["cactdesc"].ToString();// this.ddlConAccHead.SelectedItem.Text.Substring(13).ToString();
            //                    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //                    rpttxtPartyName.Text = (payto == "") ? "" : payto;


            //                    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //                    naration.Text = venar;

            //                    TextObject txtporrecieved = rptinfo.ReportDefinition.ReportObjects["txtporrecieved"] as TextObject;
            //                    txtporrecieved.Text = paytoorecived;

            //                    TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
            //                    txtusername.Text = username;

            //                    TextObject txtProject = rptinfo.ReportDefinition.ReportObjects["txtProject"] as TextObject;
            //                    txtProject.Text = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";

            //                    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //                    vounum1.Text = vounum;
            //                    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //                    date.Text = voudat;
            //                    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //                    Refnum.Text = refnum;


            //                }




            //            }




            //        }


            //        else
            //        {
            //            rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher4();
            //            TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
            //            txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
            //            TextObject txtisunum3 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
            //            txtisunum3.Text = "Issue No: " + Isunum;
            //            TextObject txtPosteddate3 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
            //            txtPosteddate3.Text = "Entry Date: " + Posteddat;
            //            TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //            Refnum.Text = "Cheque/Ref. No.: " + refnum;
            //            TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //            rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
            //            TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //            naration.Text = "Narration: " + venar;
            //            TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //            vounum1.Text = "Voucher No.: " + vounum;
            //            TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //            date.Text = "Voucher Date: " + voudat;

            //        }

            //        //ReportDocument rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();

            //        TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //        txtCompanyName.Text = comnam;
            //        TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
            //        txtcAdd.Text = comadd;
            //        //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //        //vounum1.Text = "Voucher No.: " + vounum;
            //        //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //        //date.Text = "Voucher Date: " + voudat;
            //        //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //        //Refnum.Text = "Cheque/Ref. No.: " + refnum;

            //        //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //        //rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto;
            //        TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
            //        voutype1.Text = voutype;
            //        //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //       // naration.Text = "Narration: " + venar;
            //        TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
            //        rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);
            //        TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //        txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //        if (ConstantInfo.LogStatus == true)
            //        {
            //            string eventtype = "Voucher Print";
            //            string eventdesc = "Print Voucher";
            //            string eventdesc2 = "Voucher No.: " + vounum; 
            //            bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //        }

            //        rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
            //        string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //        rptinfo.SetParameterValue("ComLogo", ComLogo);
            //        Session["Report1"] = rptinfo;
            //        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //    }
            //    catch (Exception ex)
            //    {

            //    }












            //    //    else
            //    //    {
            //    //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher3();
            //    //        TextObject txtisunum3 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
            //    //        txtisunum3.Text = "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6));
            //    //        TextObject txtPosteddate3 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
            //    //        txtPosteddate3.Text = "Entry Date: " + Posteddat;
            //    //    }


            //    //    rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
            //    //    TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //    //    txtCompanyName.Text = comnam;
            //    //    TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
            //    //    txtcAdd.Text = comadd;
            //    //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //    //    vounum1.Text = "Voucher No.: " + vounum;
            //    //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //    //    date.Text = "Voucher Date: " + voudat;
            //    //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //    //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
            //    //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //    //    rpttxtPartyName.Text = (this.lblvalpayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.lblvalpayto.Text.Trim();
            //    //    TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
            //    //    voutype1.Text = voutype;
            //    //    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //    //    naration.Text = "Narration: " + venar;

            //    //    //TextObject txtBname = rptinfo.ReportDefinition.ReportObjects["bankname"] as TextObject;
            //    //    //txtBname.Text = this.ddlConAccHead.SelectedItem.Text.Substring(13);
            //    //    TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
            //    //    rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);

            //    //    TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            //    //    //string comcod = this.GetComeCode();
            //    //    //string comcod = hst["comcod"].ToString();
            //    //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //    //    rptinfo.SetParameterValue("ComLogo", ComLogo);
            //    //    Session["Report1"] = rptinfo;
            //    //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //    //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //    //}
            //    //catch (Exception ex)
            //    //{

            //    //}




        }












    }
}