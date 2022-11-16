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
using RealERPRDLC;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_17_Acc
{

    public partial class AccPurchaseOth : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        public static double TAmount;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Other Purchase Update";
                this.Master.Page.Title = "Purchase Update";
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);


                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                // this.LoadBillCombo();
                // CreateTable();
                // this.CompanyPost();

            }


        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void CreateTable()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("actcode", Type.GetType("System.String"));
            tblt01.Columns.Add("subcode", Type.GetType("System.String"));
            tblt01.Columns.Add("spclcode", Type.GetType("System.String"));
            tblt01.Columns.Add("actdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("subdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("spcldesc", Type.GetType("System.String"));
            tblt01.Columns.Add("trnqty", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnrate", Type.GetType("System.Double"));
            tblt01.Columns.Add("trndram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trncram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnrmrk", Type.GetType("System.String"));
            tblt01.Columns.Add("billno", Type.GetType("System.String"));
            tblt01.Columns.Add("billnar", Type.GetType("System.String"));
            ViewState["tblt01"] = tblt01;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
     
        private void LoadBillCombo()
        {

            string comcod = this.GetCompCode();
            string Billno = (this.Request.QueryString["genno"].ToString()).Length == 0 ? "%" + this.txtBillno.Text.Trim() + "%" : this.Request.QueryString["genno"].ToString() + "%"; //"%" + this.txtBillno.Text.Trim() + "%";
                                                                                                                                                                                      // string Billno ="%" +this.txtBillno.Text.Trim()+"%";
            string date = (this.Request.QueryString["Date1"].ToString()).Length == 0 ? Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy") : this.Request.QueryString["Date1"].ToString();
          
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETOTHERBILL", Billno, date, "", "", "", "", "", "", "");
            this.ddlBillList.Items.Clear();
            this.ddlBillList.DataTextField = "textfield";
            this.ddlBillList.DataValueField = "billno";
            Session["narration"] = ds1.Tables[0];
            this.ddlBillList.DataSource = ds1.Tables[0];
            this.ddlBillList.DataBind();
        }


        private void calculation()
        {
            DataTable dt2 = (DataTable)ViewState["tblt01"];
            if (dt2.Rows.Count == 0)
                return;
            accData.ToDramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trndram)", "")) ?
                         0.00 : dt2.Compute("Sum(trndram)", ""))), 2);
            accData.ToCramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trncram)", "")) ?
                        0.00 : dt2.Compute("Sum(trncram)", ""))), 2);
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvDrAmt")).Text = (accData.ToDramt).ToString("#,##0.00;(#,##0.00); - ");
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvCrAmt")).Text = (accData.ToCramt).ToString("#,##0.00;(#,##0.00); - ");



        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.LoadBillCombo();
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.pnlBill.Visible = true;
                this.PnlNarration.Visible = true;
                // this.GetPreNarration();

                return;
            }
            this.lbtnOk.Text = "Ok";
            this.pnlBill.Visible = false;
            this.PnlNarration.Visible = false;

            //ViewState.Remove("tblt01");
            //this.CreateTable();
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.txtRefNum.Text = "";
            this.txtSrinfo.Text = "";
            this.txtNarration.Text = "";
            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";

            this.lnkFinalUpdate.Enabled = true;
            this.dgv2.DataSource = null;
            this.dgv2.DataBind();
        }


        private void GetPreNarration()
        {

            // Previous Nerration
            string comcod = this.GetCompCode();
            string VNo3 = "JV";
            string date = this.txtdate.Text;
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "LASTNARRATION", VNo3, date, "", "", "", "", "", "", "");
            if (ds4.Tables[0].Rows.Count == 0)
                this.txtNarration.Text = "";
            else
                this.txtNarration.Text = ds4.Tables[0].Rows[0]["vernar"].ToString();
        }
        private void GetVouCherNumber()
        {
            try
            {

                string comcod = this.GetCompCode();
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                //if (txtopndate==Convert.ToDateTime(this.txtdate.Text.Trim().Substring(0, 11)))
                //    ;
                if (txtopndate > Convert.ToDateTime(this.txtdate.Text.Trim().Substring(0, 11)))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    return;

                }

                string VNo3 = "JV";
                string entrydate = this.txtdate.Text.Substring(0, 11).Trim();
                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
                DataTable dt4 = ds4.Tables[0];
                string cvno1 = dt4.Rows[0]["couvounum"].ToString().Substring(0, 8);
                this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
                this.txtCurrntlast6.Text = dt4.Rows[0]["couvounum"].ToString().Substring(8);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

            }



        }
        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);


            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }




            if (Math.Round(accData.ToDramt) != Math.Round(accData.ToCramt))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Debit Amount must be Equal Credit Amount";
                return;
            }



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string userid = hst["usrid"].ToString();
            //string Terminal = hst["compname"].ToString();
            //string Sessionid = hst["session"].ToString();
            //string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = userid;
            string Posttrmid = Terminal;
            string PostSession = Sessionid;

            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string userdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            string EditByid = "";
            string Editdat = "01-Jan-1900";
            string pounaction = "";
            string aprovbyid = "";
            string aprvtrmid = "";
            string aprvseson = "";
            string aprvdat = "01-jan-1900";
            string Payto = "";
            string isunum = "";
            string recndt = "01-Jan-1900";
            string rpcode = "";






            //Existing   Purchase No  

            if (dgv2.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Select at least one Bill');", true);
                return;

            }


            for (int i = 0; i < dgv2.Rows.Count; i++)
            {

                string billno = ((Label)this.dgv2.Rows[i].FindControl("lblBillno")).Text.Trim();
                DataSet ds4;
                if (i == 0)
                    ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGPURBILLOTHER", billno, "", "", "", "", "", "", "", "");

                else if (((Label)this.dgv2.Rows[i - 1].FindControl("lblBillno")).Text.Trim() == billno)
                    continue;

                else
                    ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGPURBILLOTHER", billno, "", "", "", "", "", "", "", "");


                if (ds4.Tables[0].Rows[0]["pvounum"].ToString() != "00000000000000")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher No already Existing in this Bill No";
                    return;
                }

            }

            this.GetVouCherNumber();
            string voudat = this.txtdate.Text.Substring(0, 11);
            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                                   this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            string refnum = this.txtRefNum.Text.Trim();
            string srinfo = this.txtSrinfo.Text;
            string vounarration1 = this.txtNarration.Text.Trim();
            string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
            string voutype = "Journal Voucher";
            string cactcode = "000000000000";
            string vtcode = "98";
            string edit = "";

            try
            {


                string CallType = (this.chkpost.Checked) ? "ACVUPDATEUNPOSTED" : "ACVUPDATE02";

                //-----------Update Transaction B Table-----------------//
                bool resultb = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, voudat, refnum, srinfo, vounarration1,
                                vounarration2, voutype, vtcode, edit, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, Payto, isunum, aprovbyid, aprvtrmid, aprvseson, aprvdat, pounaction, "", "", "", "");
                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    return;
                }



                ////-----------Update Transaction B Table-----------------//
                //bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo,
                //        vounarration1, vounarration2, voutype, vtcode, edit, userid, Terminal, Sessionid, Postdat, "", "");
                //if (!resultb)
                //{
                // ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                //    return;
                //}
                //-----------Update Transaction A Table-----------------//
                string billno2 = "XXXXXXXXXXXXXX";

                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    string actcode = ((Label)this.dgv2.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string rescode = ((Label)this.dgv2.Rows[i].FindControl("lblResCod")).Text.Trim();
                    string spclcode = ((Label)this.dgv2.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string trnqty = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvQty")).Text.Trim()).ToString();
                    double Dramt = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvDrAmt")).Text.Trim());
                    double Cramt = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvCrAmt")).Text.Trim());

                    double trnamt = (Dramt - Cramt);
                    string trnremarks = ((TextBox)this.dgv2.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                    string billno = ((Label)this.dgv2.Rows[i].FindControl("lblBillno")).Text.Trim();
                    if (trnamt != 0)
                    {

                        bool resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, actcode, rescode, cactcode,
                              voudat, trnqty, trnremarks, vtcode, trnamt.ToString(), spclcode, recndt, rpcode, "", userid, userdate, Terminal, "", "", "", "", "", "", "", "", "", "");

                        //bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum,
                        //        actcode, rescode, cactcode, voudat, trnqty, trnremarks, vtcode, trnamt.ToString(), spclcode, "", "", "", "", "");
                        if (!resulta)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            return;
                        }

                        if (billno2 != billno)
                        {
                            //string advAmount = Convert.ToDouble("0" + this.txtAdvanced.Text).ToString();
                            resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEPURBILLOTHER",
                                    billno, vounum, "", "", "", "", "", "", "", "", "", "", "", "", "");
                            if (!resulta)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                                return;
                            }
                            billno2 = billno;
                        }
                    }
                }
             ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully.";
                //this.lblmsg.Text=@"<SCRIPT language= "JavaScript"  > window.open('RptViewer.aspx');</script>";
                this.lnkFinalUpdate.Enabled = false;
                this.txtcurrentvou.Enabled = false;
                this.txtCurrntlast6.Enabled = false;

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Purchase Update";
                    string eventdesc = "Update Purchase Bill";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            if (this.chkBill.Checked)
            {
                this.BillPrint();

            }
            else
            {

                this.PrintVoucher();
            }


        }

        private void BillPrint()
        {
            try
            {



                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comnam = hst["comnam"].ToString();
                // var AccTrialBl1 = ds1.Tables[0].DataTableToList<BDACCRDLC.R_17_Acc.AccRptList1.AccTrialBl1>();
                var lst = ASITUtility03.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.EClassSupplierBill>((DataTable)ViewState["tblt01"]);

                string billno = this.ddlBillList.SelectedValue.ToString();
                DataRow[] dr1 = ((DataTable)Session["narration"]).Select("billno='" + billno + "'");

                string billdate = Convert.ToDateTime(dr1[0]["billdat"]).ToString("dd-MMM-yyyy");
                // string billdate=Convert.ToDateTime(dr1[0]["billdate"]).ToString("dd-MMM-yyyy");

                LocalReport rpt1 = new LocalReport();
                Hashtable hshtbl = new Hashtable();
                hshtbl["companyname"] = comnam;
                hshtbl["billno"] = "Bill No :" + billno;
                hshtbl["billdate"] = "Bill Date :" + billdate;

                rpt1 = RDLCAccountSetup.GetLocalReport("R_17_Acc.rptPrintSubBill", hshtbl, lst, null);


                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




            }
            catch (Exception ex)
            {

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
        private void PrintVoucher()
        {
            try
            {


                Hashtable hst = (Hashtable)Session["tblLogin"];
                //string usrname = hst["usrname"].ToString();
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string combranch = hst["combranch"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string curvoudat = this.txtdate.Text.Substring(0, 11);
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                        this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
                string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;


                string PrintInstar = this.GetCompInstar();




                string CallType = (this.chkpost.Checked) ? "PRINTUNPOSTEDVOUCHER01" : "PRINTVOUCHER01";



                //string vounum = this.ddlPrivousVou.SelectedValue.ToString();

                DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", CallType, vounum, PrintInstar, "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt = _ReportDataSet.Tables[0];
                if (dt.Rows.Count == 0)
                    return;
                double dramt, cramt;
                dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));



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
                string voutype = dt1.Rows[0]["voutyp"].ToString();
                string venar = dt1.Rows[0]["venar"].ToString();
                string Posteddat = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
                string Type = this.CompanyPrintVou();

                LocalReport Rpt1 = new LocalReport();

                //ReportDocument rptinfo = new ReportDocument();

                if (Type == "VocherPrint")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherDefault", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", ""));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));

                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();
                    //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                    //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                    //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                    //rpttxtPartyName.Text = "";
                    //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                    //naration.Text = "Narration: " + venar;
                    //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                    //vounum1.Text = "Voucher No.: " + vounum;
                    //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                    //date.Text = "Voucher Date: " + voudat;

                }
                else if (Type == "VocherPrint1")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher1", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", ""));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", ""));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher1();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum1.Text = "";
                    //TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate1.Text = "Entry Date: " + Posteddat;
                    //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                    //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                    //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                    //rpttxtPartyName.Text = "";
                    //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                    //naration.Text = "Narration: " + venar;
                    //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                    //vounum1.Text = "Voucher No.: " + vounum;
                    //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                    //date.Text = "Voucher Date: " + voudat;

                }
                else if (Type == "VocherPrint2")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher2", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", ""));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", ""));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher2();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum2.Text = "";
                    //TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate2.Text = "Entry Date: " + Posteddat;
                    //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                    //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                    //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                    //rpttxtPartyName.Text = "";
                    //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                    //naration.Text = "Narration: " + venar;
                    //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                    //vounum1.Text = "Voucher No.: " + vounum;
                    //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                    //date.Text = "Voucher Date: " + voudat;

                }
                else if (Type == "VocherPrint3")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher3", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", ""));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", ""));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));

                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher3();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum1.Text = "";
                    //TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate1.Text = "Entry Date: " + Posteddat;
                    //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                    //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                    //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                    //rpttxtPartyName.Text = "";
                    //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                    //naration.Text = "Narration: " + venar;
                    //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                    //vounum1.Text = "Voucher No.: " + vounum;
                    //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                    //date.Text = "Voucher Date: " + voudat;

                }
                else if (Type == "VocherPrint5")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher5", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", ""));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", ""));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher5();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum1.Text = "";
                    //TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate1.Text = "Entry Date: " + Posteddat;
                    //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                    //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                    //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                    //rpttxtPartyName.Text = "";
                    //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                    //naration.Text = "Narration: " + venar;

                    //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                    //vounum1.Text = "Voucher No.: " + vounum;
                    //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                    //date.Text = "Voucher Date: " + voudat;

                }
                else if (Type == "VocherPrint6")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherBridge", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", ""));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucherBridge();
                    //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                    //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                    //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                    //rpttxtPartyName.Text = "";
                    //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                    //naration.Text = "Narration: " + venar;
                    //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                    //vounum1.Text = "Voucher No.: " + vounum;
                    //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                    //date.Text = "Voucher Date: " + voudat;


                }

                //else if (Type == "VocherPrintIns")
                //{

                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherIns();

                //    //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //    //txtCompanyName.Text = comnam;
                //    //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                //    //txtcAdd.Text = comadd;
                //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //    vounum1.Text = "Voucher No.: " + vounum;
                //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //    date.Text = "Voucher Date: " + voudat;
                //    //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    //rpttxtPartyName.Text = "";// (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //    //TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
                //    //voutype1.Text = voutype;
                //    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //    naration.Text = "Narration: " + venar;

                //}



                else if (Type == "VocherPrintMod")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherAlli", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli();

                    //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                    //vounum1.Text = "Voucher No.: " + vounum;
                    //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                    //date.Text = "Voucher Date: " + voudat;
                    //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                    //naration.Text = "Narration: " + venar;

                }

                else
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher4", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher4();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";

                    //TextObject txtPosteddate3 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate3.Text = "Entry Date: " + Posteddat;

                    //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                    //naration.Text = "Narration: " + venar;

                    //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                    //vounum1.Text = "Voucher No.: " + vounum;
                    //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                    //date.Text = "Voucher Date: " + voudat;

                }


                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                Rpt1.SetParameters(new ReportParameter("InWrd", ASTUtility.Trans(Math.Round(TAmount), 2)));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";



                //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //txtCompanyName.Text = comnam;
                //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                //txtcAdd.Text = comadd;


                ////TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                ////rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
                //voutype1.Text = voutype;


                ////TextObject txtBname = rptinfo.ReportDefinition.ReportObjects["bankname"] as TextObject;
                ////txtBname.Text = this.ddlConAccHead.SelectedItem.Text.Substring(13);
                //TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                //rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);

                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


                ////string comcod = this.GetComeCode();
                ////string comcod = hst["comcod"].ToString();
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rptinfo.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rptinfo;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }

        private string CompanyPurchase()
        {

            String excludetax = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3332":
                case "3101":
                    excludetax = "excluede";
                    break;

                default:
                    excludetax = "";
                    break;



            }

            return excludetax;

        }

        private string GetCompCallType()
        {
            string CallType = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {

                case "3370":// CPDL
                case "3101"://
                    CallType = "GETACCOTHERPURCHASESADJ";
                    break;


                default:
                    CallType = "GETACCOTHERPURCHASES";
                    break;
            }

            return CallType;



        }
        protected void lbtnSelectBill_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblt01");
            this.CreateTable();
            string comcod = this.GetCompCode();
            string CallType = this.GetCompCallType();
            // string ExcludeTax = this.CompanyPurchase();
            string billid = this.ddlBillList.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", CallType, billid,
                          "", "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            DataTable tblt01 = (DataTable)ViewState["tblt01"];

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string dgAccCode = dt1.Rows[i]["actcode"].ToString();
                string dgResCode = dt1.Rows[i]["rescode"].ToString();
                //string dgSpclCode = dt1.Rows[i]["spcode"].ToString();
                string dgAccDesc = dt1.Rows[i]["actdesc"].ToString();
                string dgResDesc = dt1.Rows[i]["resdesc"].ToString();
                //string dgSpclDesc = dt1.Rows[i]["spcfdesc"].ToString();
                double dgTrnrate = 0.00;
                double dgTrnQty = Convert.ToDouble(dt1.Rows[i]["billqty"]);
                if (Convert.ToDouble(dt1.Rows[i]["billqty"]) > 0)
                {
                    dgTrnrate = Convert.ToDouble(dt1.Rows[i]["Dr"]) / Convert.ToDouble(dt1.Rows[i]["billqty"]);
                }

                double dgTrnDrAmt = Convert.ToDouble(dt1.Rows[i]["Dr"]);
                double dgTrnCrAmt = Convert.ToDouble(dt1.Rows[i]["Cr"]);
                string dgTrnRemarks = dt1.Rows[i]["billid"].ToString();
                string dgBillnarr = dt1.Rows[i]["billnar"].ToString();

                DataRow[] dr2 = tblt01.Select("actcode='" + dgAccCode + "'  and subcode='" + dgResCode + "'");
                //if (dr2.Length > 0)
                //{

                //    tblt01.Clear();
                //    return;

                //}

                DataRow dr1 = tblt01.NewRow();
                dr1["actcode"] = dgAccCode;
                dr1["subcode"] = dgResCode;
                //dr1["spclcode"] = dgSpclCode;
                dr1["actdesc"] = dgAccDesc;
                dr1["subdesc"] = dgResDesc;

                dr1["trnqty"] = dgTrnQty;
                dr1["trnrate"] = dgTrnrate;
                dr1["trndram"] = dgTrnDrAmt;
                dr1["trncram"] = dgTrnCrAmt;
                dr1["trnrmrk"] = dgTrnRemarks;
                dr1["billno"] = dgTrnRemarks;
                dr1["billnar"] = dgBillnarr;
                tblt01.Rows.Add(dr1);
            }


            ViewState["tblt01"] = HiddenSameData(tblt01);
            this.Data_Bind();
        }

        protected void Data_Bind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblt01"];
            dgv2.DataSource = tbl1;
            dgv2.DataBind();
            this.GridColoumnVisible();
            calculation();
            this.GetNarration();



        }

        private void GetNarration()
        {
            DataTable dt = (DataTable)Session["narration"];
            string ddlbillno = this.ddlBillList.SelectedValue.ToString();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "billno='" + ddlbillno + "'";
            dt = dv.ToTable();
            this.txtNarration.Text = "Bill No : " + dt.Rows[0]["billno"].ToString() + " , Bill Ref No : " + dt.Rows[0]["mrfno"].ToString() + " , Bill Date : " + Convert.ToDateTime(dt.Rows[0]["reqdat"]).ToString("dd-MMM-yyyy");
        }



        private void GridColoumnVisible()
        {
            DataTable tbl1 = (DataTable)ViewState["tblt01"];
            int TblRowIndex2;
            for (int j = 0; j < this.dgv2.Rows.Count; j++)
            {

                TblRowIndex2 = (this.dgv2.PageIndex) * this.dgv2.PageSize + j;
                string mRSIRCODE = tbl1.Rows[TblRowIndex2]["subcode"].ToString();
                if (ASTUtility.Left(mRSIRCODE, 2) != "97")
                    ((TextBox)this.dgv2.Rows[j].FindControl("txtgvCrAmt")).ReadOnly = true;
                //((TextBox)this.dgv2.Rows[j].FindControl("txtgvDrAmt")).ReadOnly = true;
            }


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




        protected void imgSearchBillno_Click(object sender, EventArgs e)
        {
            this.LoadBillCombo();
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblt01"];
            double todramt = 0, tocramt = 0;
            int TblRowIndex2;
            for (int j = 0; j < this.dgv2.Rows.Count; j++)
            {
                string Supplier = ((Label)this.dgv2.Rows[j].FindControl("lblResCod")).Text.Trim();
                double dramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvDrAmt")).Text.Trim()));
                todramt = todramt + dramt;
                TblRowIndex2 = (this.dgv2.PageIndex) * this.dgv2.PageSize + j;
                if (ASTUtility.Left(Supplier, 2) == "99")
                {
                    dt1.Rows[TblRowIndex2]["trncram"] = todramt - tocramt;
                    todramt = 0; tocramt = 0;
                    continue;
                }

                double cramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvCrAmt")).Text.Trim()));
                tocramt = tocramt + cramt;

                dt1.Rows[TblRowIndex2]["trncram"] = cramt;
                dt1.Rows[TblRowIndex2]["trndram"] = dramt;

            }
            ViewState["tblt01"] = dt1;
            this.Data_Bind();



        }
    }
}