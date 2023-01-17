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
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_17_Acc
{

    public partial class SuplierPaymentPost : System.Web.UI.Page
    {
        public static double TAmount;

        ProcessAccess accData = new ProcessAccess();
        public static string lblTitle;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //((Label)this.Master.FindControl("lblTitle")).Text = Request.QueryString["tname"].ToString();
                lblTitle = Request.QueryString["tname"].ToString();

                //this.Master.Page.Title = Request.QueryString["tname"].ToString();
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.lnkFinalUpdate.Enabled = (Convert.ToBoolean(dr1[0]["entry"]));




                lnkFinalUpdate.Attributes.Add("onClick", " javascript:return confirm('You sure you want to Save the record?');");

                this.txtEntryDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                this.txtChequeDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                this.Visibility();
                this.LoadAcccombo();

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
        private void Visibility()
        {
            if (this.Request.QueryString["Mod"] == "Accounts")
            {
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                this.ddlPrivousVou.Visible = false;
                this.ibtnFindPrv.Visible = false;
                this.lnkPrivVou.Visible = false;
                this.txtScrchPre.Visible = false;
            }

            else
            {
                // this.ibtnFindPrv_Click(null, null);

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
                // case "3101":
                case "3315":
                case "3316":
                case "3317":
                case "3325":
                case "2325":
                    vouprint = "VocherPrint5";
                    break;
                //case "3101":
                case "3330":
                    vouprint = "VocherPrint6";
                    break;
                case "3101":
                case "3333":
                    vouprint = "VocherPrintMod";
                    break;

                default:
                    vouprint = "VocherPrint";
                    break;
            }
            return vouprint;
        }
        private void GetPriviousVoucher()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string ConAccHead = this.ddlConAccHead.SelectedValue.ToString();
            string vtcode = this.Request.QueryString["tcode"].ToString().Trim();
            string date = this.txtEntryDate.Text.Substring(0, 11);

            string VNo1 = (lblTitle.Contains("Journal") ? "J" : (lblTitle.Contains("Contra") ? "C" : (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B")));
            //string VNo1 = (this.lblGeneralAcc.Text.Contains("Journal") ? "J" : (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B"));
            string VNo2 = (VNo1 == "J" ? "V" : (lblTitle.Contains("Payment") ? "D" : (lblTitle.Contains("Contra") ? "T" : "C")));
            // string VNo2 = (VNo1 == "J" ? "V" : (this.lblGeneralAcc.Text.Contains("Payment") ? "D" : "C"));
            string VNo3 = Convert.ToString(VNo1 + VNo2);
            string vounum = "%" + this.txtScrchPre.Text + "%";
            DataSet ds5 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETPRIVOUSVOUCHER", VNo3, vtcode, date, vounum, "", "", "", "", "");

            this.ddlPrivousVou.DataSource = ds5.Tables[0];
            this.ddlPrivousVou.DataTextField = "vounum1";
            this.ddlPrivousVou.DataValueField = "vounum";
            this.ddlPrivousVou.DataBind();
        }


        private void TableCreate()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("actcode", Type.GetType("System.String"));
            tblt01.Columns.Add("subcode", Type.GetType("System.String"));
            tblt01.Columns.Add("actdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("subdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("chequeno", Type.GetType("System.String"));
            tblt01.Columns.Add("isunum", Type.GetType("System.String"));
            tblt01.Columns.Add("chequedate", Type.GetType("System.DateTime"));

            tblt01.Columns.Add("trndram", Type.GetType("System.Double"));
            tblt01.Columns.Add("taxam", Type.GetType("System.Double"));
            tblt01.Columns.Add("netam", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnrmrk", Type.GetType("System.String"));
            tblt01.Columns.Add("payto", Type.GetType("System.String"));
            tblt01.Columns.Add("acvounum", Type.GetType("System.String"));
            tblt01.Columns.Add("billno", Type.GetType("System.String"));
            tblt01.Columns.Add("insofissue", Type.GetType("System.String"));
            ViewState["tblt01"] = tblt01;

            //actcode,subcode,spclcode,actdesc,subdesc,spcldesc,trnqty,trnrate,trndram,trncram,trnrmrk
        }

        private void LoadAcccombo()
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string ttsrch = this.txtScrchConCode.Text.Trim() + "%";
                string UserId = hst["usrid"].ToString();
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONHEADPAONPAY", ttsrch, UserId, "", "", "", "", "", "", "");
                DataTable dt1 = ds1.Tables[0];
                this.ddlConAccHead.DataSource = dt1;
                this.ddlConAccHead.DataTextField = "actdesc1";
                this.ddlConAccHead.DataValueField = "actcode";
                this.ddlConAccHead.DataBind();
                //this.GetPriviousVoucher();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }

        }


        protected void lnkRescode_Click(object sender, EventArgs e)
        {

            this.GetResCode();
        }


        private void GetResCode()
        {

            string comcod = this.GetCompCode();
            string filter1 = "%" + this.txtserchReCode.Text.Trim() + "%";
            DataSet ds3 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETSUPLILIST", "", filter1, "", "", "", "", "", "", "");
           // DataSet ds3 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBILLNO", "", filter1, "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.ddlresuorcecode.Items.Clear();
                return;

            }

            this.ddlresuorcecode.DataSource = ds3.Tables[0];
            this.ddlresuorcecode.DataTextField = "resdesc1";
            this.ddlresuorcecode.DataValueField = "rescode";
            this.ddlresuorcecode.DataBind();
            this.txtserchReCode.Text = "";
            //ViewState["tblbill"] = ds3.Tables[0];
            this.GetBillNo();




        }

        private void GetBillNo()
        {
            try
            {
                string comcod = this.GetCompCode();

                string ssircode = this.ddlresuorcecode.SelectedValue.ToString();
                string mrfno = "%" + this.txtserchBill.Text.Trim() + "%";
                //DataTable dt = ((DataTable)ViewState["tblbill"]).Copy();
                //DataView dv = dt.DefaultView;
                //dv.RowFilter = ("rescode='" + ssircode + "' and textfield like '" + mrfno + "'");
                ////this.ddlBillList.DataSource = dv.ToTable();
                ////this.ddlBillList.DataTextField = "textfield";
                ////this.ddlBillList.DataValueField = "billno";
                ////this.ddlBillList.DataBind(); 
               
                DataSet ds3 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBILLSUPLILIST", ssircode, mrfno, "", "", "", "", "", "", "");
                if (ds3 == null)
                {
                    this.DropCheck1.Items.Clear();
                    return;

                }
                 

                this.DropCheck1.DataSource = ds3.Tables[0];
                this.DropCheck1.DataTextField = "textfield";
                this.DropCheck1.DataValueField = "billno";
                this.DropCheck1.DataBind();

                ViewState["tblbill"] = ds3.Tables[0];
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }


        }

        protected void ddlresuorcecode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetBillNo();
        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {
            if (this.lnkOk.Text == "Ok")
            {
                this.TableCreate();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string newvoumum = "NEW";
                this.PanelChk.Visible = true;

                DataSet _NewDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "EDITVOUCHER", newvoumum, "", "", "", "", "", "", "", "");
                Session["UserLog"] = _NewDataSet.Tables[1];



                if (this.ddlPrivousVou.Items.Count > 0)
                {

                    string vounum = this.ddlPrivousVou.SelectedValue.ToString();
                    DataSet _EditDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "EDITVOUCHER", vounum, "", "", "", "", "", "", "", "");
                    DataTable dt = this.HiddenSameData(_EditDataSet.Tables[0]);

                    ViewState["tblt01"] = dt;
                    Session["UserLog"] = _EditDataSet.Tables[1];
                    this.FooterAmount();

                    //-------------** Edit **---------------------------//
                    DataTable dtedit = _EditDataSet.Tables[1];
                    this.txtEntryDate.Text = Convert.ToDateTime(dtedit.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                    this.lblisunum.Text = dtedit.Rows[0]["isunum"].ToString();
                    this.txtNarration.Text = dtedit.Rows[0]["venar"].ToString();
                    this.txtEntryDate.Enabled = false;
                    //-------------------------------------------------//
                    this.pnlNarration.Visible = true;
                    this.lblcurVounum.Text = "Edit Voucher No.";
                    string cvno1 = this.ddlPrivousVou.SelectedValue.ToString().Substring(0, 8);
                    this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
                    this.txtCurrntlast6.Text = this.ddlPrivousVou.SelectedValue.ToString().Substring(8);
                    this.txtCurrntlast6.Enabled = false;
                }
                else
                {

                    this.txtEntryDate.Enabled = true;
                    this.txtCurrntlast6.Enabled = true;


                    // Previous Nerration
                    double vcode1 = Convert.ToDouble(Request.QueryString["tcode"]);
                    string ConAccHead = this.ddlConAccHead.SelectedValue.ToString();
                    string VNo1 = (lblTitle.Contains("Journal") ? "J" : (lblTitle.Contains("Contra") ? "C" :
                        (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B")));
                    string VNo2 = (VNo1 == "J" ? "V" : (lblTitle.Contains("Payment") ? "D" : (lblTitle.Contains("Contra") ? "T" : "C")));
                    string VNo3 = Convert.ToString(VNo1 + VNo2);
                    DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "LASTNARRATION", VNo3, "", "", "", "", "", "", "", "");
                    if (ds4.Tables[0].Rows.Count == 0)
                        this.txtNarration.Text = "";
                    else
                        this.txtNarration.Text = ds4.Tables[0].Rows[0]["vernar"].ToString();
                    //---------------------

                    this.GetVouCherNumber();

                }
                this.lnkPrivVou.Visible = false;
                this.ddlPrivousVou.Visible = false;
                this.ibtnFindPrv.Visible = false;
                this.txtScrchPre.Visible = false;

                if (lblTitle.Contains("Payment") || lblTitle.Contains("Contra") || lblTitle.Contains("Deposit"))
                {
                    this.txtScrchConCode.Visible = true;

                    this.chkPrint.Checked = false;
                    this.chkPrint.Visible = false;
                }
                this.chkPrint.Visible = lblTitle.Contains("Payment") ? true : lblTitle.Contains("Contra") ? true
                        : lblTitle.Contains("Deposit") ? true : false;
                this.Panel2.Visible = true;
                this.pnlNarration.Visible = true;
                this.lnkFinalUpdate.Enabled = true;
                this.lnkOk.Text = "New";
                this.GetResCode();

            }
            else
            {

                ViewState.Remove("tblt01");
                this.lnkOk.Text = "Ok";
                this.txtCurrntlast6.Enabled = false;

                this.Panel2.Visible = false;
                this.pnlNarration.Visible = false;
                this.pnlNarration.Visible = false;
                dgv1.DataSource = null;
                dgv1.DataBind();

                if (lblTitle.Contains("Payment") || lblTitle.Contains("Contra") || lblTitle.Contains("Deposit"))
                {
                    this.txtScrchConCode.Visible = true;
                    // this.ibtnFindConCode.Visible = true;
                    this.chkPrint.Visible = false;
                }
                if (this.Request.QueryString["Mod"] == "Accounts")
                {
                    this.lnkPrivVou.Visible = false;
                    this.ddlPrivousVou.Visible = false;
                    this.ibtnFindPrv.Visible = false;
                    this.txtScrchPre.Visible = false;

                }
                else
                {
                    this.lnkPrivVou.Visible = true;
                    this.ddlPrivousVou.Visible = true;
                    this.ibtnFindPrv.Visible = true;
                    this.txtScrchPre.Visible = true;

                }
                this.PanelChk.Visible = false;
                this.lblcurVounum.Text = "Current Vou No.";
                this.txtcurrentvou.Text = "";
                this.txtCurrntlast6.Text = "";
                this.txtEntryDate.Enabled = true;
                this.ddlresuorcecode.BackColor = System.Drawing.Color.White;
                this.txtScrchConCode.Focus();
                this.Refrsh();

            }
        }
        private void Refrsh()
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Text = "";
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";
            this.ddlPrivousVou.Items.Clear();
            this.ddlresuorcecode.Items.Clear();
            //this.ddlBillList.Items.Clear();
            this.DropCheck1.Items.Clear();

            this.txtserchReCode.Text = "";

            this.txtNarration.Text = "";
            this.lblisunum.Text = "";
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
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


        protected void ddlConAccHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlConAccHead.BackColor = System.Drawing.Color.Pink;
            //this.GetPriviousVoucher();
        }

        private DataTable HiddenSameData02(DataTable tblt03)
        {
            if (tblt03.Rows.Count == 0)
                return tblt03;

            string actcode = tblt03.Rows[0]["actcode"].ToString();

            for (int j = 1; j < tblt03.Rows.Count; j++)
            {

                if (tblt03.Rows[j]["actcode"].ToString() == actcode)
                    tblt03.Rows[j]["actdesc"] = "";
                actcode = tblt03.Rows[j]["actcode"].ToString();
            }

            return tblt03;

        }
        private string GetIssueNo()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETISSUENO", "", "", "", "", "", "", "", "", "");
            return ds2.Tables[0].Rows[0]["isunum"].ToString();
        }


        private string IncrmentIssueNo()
        {
            //string isunum="000000000";
            string isunumber = (Convert.ToInt32(this.lblissueno.Text.Trim()) + 1).ToString();
            return (ASTUtility.Right(("000000000" + isunumber), 9));



        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {

            string rescode = this.ddlresuorcecode.SelectedValue.ToString();
            //string billno = this.ddlBillList.SelectedValue.ToString();
            //string billno = this.DropCheck1.SelectedValue.ToString();

            string[] arrbilno = this.DropCheck1.Text.Trim().Split(',');
            string comcod = this.GetCompCode();


            DataTable dt = (DataTable)ViewState["tblt01"];

            if (this.txtChequeNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Fill Reference Number');", true);
                this.txtChequeNo.Focus();
                return;
            }
            string isunum = "";
            if ((this.Request.QueryString["Mod"] == "Accounts"))
            {
                isunum = this.GetIssueNo();
                this.lblissueno.Text = isunum;
            }

            DataTable dt1 = (DataTable)ViewState["tblbill"];

            foreach (string billno in arrbilno)
            {
                double taxam = Convert.ToDouble("0" + this.txttaxamt.Text.Trim());
                DataRow[] dr2 = dt.Select("billno='" + billno + "'");
                if (dr2.Length == 0)
                {
                    DataRow[] drb = dt1.Select("billno='" + billno + "'");

                    DataRow dr1 = dt.NewRow();
                    dr1["actcode"] = drb[0]["pactcode"].ToString(); ;
                    dr1["subcode"] = rescode;
                    dr1["chequeno"] = this.txtChequeNo.Text.Trim();
                    dr1["isunum"] = isunum;
                    dr1["actdesc"] = drb[0]["pactdesc"].ToString();
                    dr1["subdesc"] = this.ddlresuorcecode.SelectedItem.Text.Substring(13);
                    dr1["chequedate"] = ASTUtility.DateFormat(this.txtChequeDate.Text);
                    dr1["trndram"] = drb[0]["amt"].ToString();
                    dr1["taxam"] = taxam;
                    dr1["netam"] = Convert.ToDouble(drb[0]["amt"]) - taxam;
                    dr1["trnrmrk"] = "";

                    //dr1["payto"] = (comcod == "3315" || comcod == "3316") ? this.ddlresuorcecode.SelectedItem.Text.Substring(19) : this.ddlresuorcecode.SelectedItem.Text.Substring(13);

                    dr1["payto"] = this.ddlresuorcecode.SelectedItem.Text.Substring(13, 6) == "SUP - " ? this.ddlresuorcecode.SelectedItem.Text.Substring(19) :
                        this.ddlresuorcecode.SelectedItem.Text.Substring(13, 5) == "SUP -" ? this.ddlresuorcecode.SelectedItem.Text.Substring(18) :
                        this.ddlresuorcecode.SelectedItem.Text.Substring(13);


                    dr1["billno"] = billno;
                    dr1["insofissue"] = "";
                    dt.Rows.Add(dr1);

                }
            }

            ViewState["tblt01"] = HiddenSameData(dt);

            ViewState["tblt01"] = HiddenSameData02(dt);
            this.Data_Bind();





        }


        private void Data_Bind()
        {
            dgv1.DataSource = (DataTable)ViewState["tblt01"];
            dgv1.DataBind();
            this.FooterAmount();


        }
        private void FooterAmount()
        {
            DataTable dt = (DataTable)ViewState["tblt01"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.dgv1.FooterRow.FindControl("lblTgvDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trndram)", "")) ?
                                 0 : dt.Compute("sum(trndram)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.dgv1.FooterRow.FindControl("lblgvFtaxamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(taxam)", "")) ?
                              0 : dt.Compute("sum(taxam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.dgv1.FooterRow.FindControl("lblgvFnetamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netam)", "")) ?
                              0 : dt.Compute("sum(netam)", ""))).ToString("#,##0;(#,##0); ");


        }
        protected void lnkOk0_Click(object sender, EventArgs e)
        {

            //try
            //{
            //    //----------------Add Data Into Grid--------------------------//
            //    this.Panel4.Visible = true;
            //    string AccCode = this.ddlacccode.SelectedValue.ToString();
            //    string ResCode = this.ddlresuorcecode.SelectedValue.ToString();
            //    string Billno = this.ddlBillList.Items.Count>0?this.ddlBillList.SelectedValue.ToString():"";
            //    ResCode = (ResCode.Length < 12 ? "000000000000" : ResCode);

            //    string actlev = (((DataTable)ViewState["HeadAcc1"]).Select("actcode='" + AccCode + "'"))[0]["actelev"].ToString();
            //    if (actlev == "2")
            //    {
            //        if (ResCode == "000000000000")
            //        {
            //            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Select Details Head');", true);
            //            this.txtserchReCode.Focus();
            //            return;

            //        }

            //    }

            //    string billno = this.ddlBillList.SelectedValue.ToString();

            //    string SpclCode = this.ddlSpclinf.SelectedValue.ToString();
            //    SpclCode = (SpclCode.Length < 12 ? "000000000000" : SpclCode);
            //    string AccDesc = this.ddlacccode.SelectedItem.Text.Trim();
            //    string ResDesc = (ResCode == "000000000000" ? "" : this.ddlresuorcecode.SelectedItem.Text.Trim());
            //    string SpclDesc = (SpclCode == "000000000000" ? "" : this.ddlSpclinf.SelectedItem.Text.Trim().Substring(13));
            //    double TrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtqty.Text.Trim()));
            //    double Trnrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtrate.Text.Trim()));
            //    double TrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtDrAmt.Text.Trim()));
            //    double TrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtCrAmt.Text.Trim()));
            //    string TrnRemarks = this.txtremarks.Text.Trim();
            //    DataTable tblt01 = (DataTable)Session["tblt01"];
            //    DataTable tblt02 = (DataTable)Session["tblt02"];
            //    DataTable tblt03 = new DataTable();
            //    tblt01.Rows.Clear();
            //    tblt02.Rows.Clear();
            //    tblt03.Rows.Clear();

            //    for (int i = 0; i < this.dgv1.Rows.Count; i++)
            //    {
            //        string dgAccCode = ((Label)this.dgv1.Rows[i].FindControl("lblAccCod")).Text.Trim();
            //        string dgResCode = ((Label)this.dgv1.Rows[i].FindControl("lblResCod")).Text.Trim();
            //        string dgBillno = ((Label)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text.Trim();


            //        //-----------If Repetation ---------------------------------------------------------//
            //        if (dgAccCode + dgResCode + dgBillno == AccCode + ResCode + Billno)
            //        {
            //            ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text = SpclCode;
            //            ((Label)this.dgv1.Rows[i].FindControl("lblSpcldesc")).Text = SpclDesc;
            //            ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text = TrnQty.ToString("#,##0.00;(#,##0.00); ");
            //            ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text = Trnrate.ToString("#,##0.00;(#,##0.00); ");
            //            ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text = TrnDrAmt.ToString("#,##0.00;(#,##0.00); ");
            //            ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text = TrnCrAmt.ToString("#,##0.00;(#,##0.00); ");
            //            ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRemarks")).Text = TrnRemarks;
            //            ((Label)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text = billno;


            //            //this.CalculatrGridTotal();
            //            //return;
            //        }
            //        else
            //        {
            //            //--------------------------------------------------------------------------------//
            //            string dgSpclCode = ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text.Trim();
            //            string dgAccDesc = ((Label)this.dgv1.Rows[i].FindControl("lblAccdesc")).Text.Trim();
            //            string dgResDesc = ((Label)this.dgv1.Rows[i].FindControl("lblResdesc")).Text.Trim();
            //            string dgSpclDesc = ((Label)this.dgv1.Rows[i].FindControl("lblSpcldesc")).Text.Trim();
            //            double dgTrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()));
            //            double dgTrnrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text.Trim()));
            //            double dgTrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
            //            double dgTrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text.Trim()));
            //            string dgTrnRemarks = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
            //            string recndt = ((Label)this.dgv1.Rows[i].FindControl("lblrecndat")).Text ;
            //            string rpcode = ((Label)this.dgv1.Rows[i].FindControl("lblgvrpcode")).Text;
            //            string billno1 = ((Label)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text;
            //            //actcode subcode actdesc subdesc trnqty trnrate trndram trncram trnrmrk 
            //            DataRow dr1 = tblt01.NewRow();
            //            dr1["actcode"] = dgAccCode;
            //            dr1["subcode"] = dgResCode;
            //            dr1["spclcode"] = dgSpclCode;
            //            dr1["actdesc"] = dgAccDesc;
            //            dr1["subdesc"] = dgResDesc;
            //            dr1["spcldesc"] = dgSpclDesc;
            //            dr1["trnqty"] = dgTrnQty;
            //            dr1["trnrate"] = dgTrnrate;
            //            dr1["trndram"] = dgTrnDrAmt;
            //            dr1["trncram"] = dgTrnCrAmt;
            //            dr1["trnrmrk"] = dgTrnRemarks;
            //            dr1["recndt"] = recndt;
            //            dr1["rpcode"] = rpcode;
            //            dr1["billno"] = billno1;
            //            tblt01.Rows.Add(dr1);
            //        }
            //    }
            //    DataRow dr2 = tblt01.NewRow();
            //    dr2["actcode"] = AccCode;
            //    dr2["subcode"] = ResCode;
            //    dr2["spclcode"] = SpclCode;
            //    dr2["actdesc"] = AccDesc;
            //    dr2["subdesc"] = ResDesc;
            //    dr2["spcldesc"] = SpclDesc;
            //    dr2["trnqty"] = TrnQty;
            //    dr2["trnrate"] = Trnrate;
            //    dr2["trndram"] = TrnDrAmt;
            //    dr2["trncram"] = TrnCrAmt;
            //    dr2["trnrmrk"] = TrnRemarks;
            //    dr2["recndt"] = "";
            //    dr2["rpcode"] = "";
            //    dr2["billno"] = billno;
            //    tblt01.Rows.Add(dr2);
            //    //--------------** Remove Duplicate Value **----------------------------//
            //    //--** Only Actdesc remove actcod not remove from grid **---------------// 
            //    DataView dv1 = tblt01.DefaultView;
            //    dv1.Sort = "actcode";
            //    tblt03 = dv1.ToTable();
            //    string AccDesc1 = null;
            //    for (int j = 0; j < tblt03.Rows.Count; j++)
            //    {
            //        DataRow dr3 = tblt02.NewRow();
            //        dr3["actcode"] = tblt03.Rows[j]["actcode"].ToString();
            //        dr3["subcode"] = tblt03.Rows[j]["subcode"].ToString();
            //        dr3["spclcode"] = tblt03.Rows[j]["spclcode"].ToString();
            //        string tserch = tblt03.Rows[j]["actcode"].ToString();
            //        if (tserch == AccDesc1 || tserch == "")
            //        {
            //            dr3["actdesc"] = "";
            //        }
            //        else
            //        {
            //            dr3["actdesc"] = tblt03.Rows[j]["actdesc"].ToString();
            //            AccDesc1 = tblt03.Rows[j]["actcode"].ToString();
            //        }

            //        dr3["subdesc"] = tblt03.Rows[j]["subdesc"].ToString();
            //        dr3["spcldesc"] = tblt03.Rows[j]["spcldesc"].ToString();
            //        dr3["trnqty"] = Convert.ToDouble(tblt03.Rows[j]["trnqty"].ToString());
            //        dr3["trnrate"] = Convert.ToDouble(tblt03.Rows[j]["trnrate"].ToString());
            //        dr3["trndram"] = Convert.ToDouble(tblt03.Rows[j]["trndram"].ToString());
            //        dr3["trncram"] = Convert.ToDouble(tblt03.Rows[j]["trncram"].ToString());
            //        dr3["trnrmrk"] = tblt03.Rows[j]["trnrmrk"].ToString();
            //        dr3["recndt"] = tblt03.Rows[j]["recndt"].ToString();
            //        dr3["billno"] = tblt03.Rows[j]["billno"].ToString(); 
            //        tblt02.Rows.Add(dr3);

            //    }
            //    //---------------------------------------------//
            //    dgv1.DataSource = tblt02;


            //    //dgv1.DataSource = tblt01;
            //    dgv1.DataBind();
            //    this.CalculatrGridTotal();
            //    //this.ddlacccode.BackColor = System.Drawing.Color.Beige;
            //    //this.ddlresuorcecode.BackColor = System.Drawing.Color.Beige;
            //    //this.ddlSpclinf.BackColor = System.Drawing.Color.Beige;

            //}
            //catch (Exception ex)
            //{
            // ((Label)this.Master.FindControl("lblmsg")).Text = "Error :" + ex.Message;
            //}

        }
        protected void SaveValue()
        {

            DataTable dt = (DataTable)ViewState["tblt01"];
            double dram, taxam, netamt, billbal;
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {

                dt.Rows[i]["chequeno"] = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvChequeno")).Text;
                dt.Rows[i]["chequedate"] = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvChequeDate")).Text;//.ToString("dd-MMM-yyyy");

                dram = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
                taxam = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvtaxamt")).Text.Trim()));
                billbal = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.dgv1.Rows[i].FindControl("lblgvbalance")).Text.Trim()));


                if (billbal >= dram)
                {

                    netamt = dram - taxam;
                    dt.Rows[i]["trndram"] = dram;
                    dt.Rows[i]["taxam"] = taxam;
                    dt.Rows[i]["netam"] = netamt;


                    dt.Rows[i]["trnrmrk"] = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRemarks")).Text;
                    dt.Rows[i]["payto"] = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvPayto")).Text;
                    dt.Rows[i]["billno"] = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvBillno")).Text;
                    dt.Rows[i]["insofissue"] = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvinsissueno")).Text;

                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Bill Amount  must be less then or equal bill Balance !!!!');", true);
                    return;

                }



            }
            ViewState["tblt01"] = dt;





        }

        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
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
        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();

            //Ref Number

            string voudat = ASTUtility.DateFormat(this.txtEntryDate.Text);
            DateTime Bdate;
            bool dcon;
            Bdate = this.GetBackDate();
            if ((this.Request.QueryString["Mod"] == "Accounts"))
            {
                dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(voudat));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Issue Date is equal or less Current Date');", true);
                    return;
                }
                this.GetVouCherNumber();
            }
            else
            {

                if ((this.Request.QueryString["Mod"] == "Management"))
                {
                    string comlimit = this.Companylimit();
                    if (comlimit.Length > 0)
                    {

                        dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(voudat));
                        if (!dcon)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Voucher Date is Equal or Greater then Transaction Limt');", true);
                            return;
                        }

                    }

                    if (this.txtCurrntlast6.Enabled)
                        this.GetVouCherNumber();
                };


            }


            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(6, 4) +
                            this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            string vounarration1 = this.txtNarration.Text.Trim();

            string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
            string vouno = this.txtcurrentvou.Text.Trim().Substring(0, 2);
            string vtcode = Request.QueryString["tcode"];
            string voutype = this.Request.QueryString["tname"].ToString();
            string cactcode = this.ddlConAccHead.SelectedValue.ToString();

            if (cactcode == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Bank Name can not be empty');", true);
                return;
            }
            string edit = (this.txtCurrntlast6.Enabled ? "" : "EDIT");





            //Log Entry

            DataTable dtuser = (DataTable)Session["UserLog"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string PostedByid = (this.Request.QueryString["Mod"] == "Accounts") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = (this.Request.QueryString["Mod"] == "Accounts") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = (this.Request.QueryString["Mod"] == "Accounts") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string Posteddat = (this.Request.QueryString["Mod"] == "Accounts") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
            string EditByid = (this.Request.QueryString["Mod"] == "Accounts") ? "" : userid;
            string Editdat = (this.Request.QueryString["Mod"] == "Accounts") ? "01-Jan-1900" : System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            //string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)ViewState["tblt01"];

            DataRow[] dr = dt.Select("netam=0");
            if (dr.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Amount will not be empty');", true);
                return;
            }

            try
            {
                //-----------Update Payment B Table-----------------//
                bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "INOFUPACPMNTB", vounum, voudat, vounarration1,
                                vounarration2, voutype, vtcode, edit, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, "", "");
                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    return;
                }
                //-----------Update Payment  A Table-----------------//

                //string isuno = this.GetIssueNo();


                string isunum = "";
                if (this.Request.QueryString["Mod"] == "Accounts")
                {
                    isunum = this.GetIssueNo();
                    this.lblissueno.Text = isunum;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {





                    string actcode = dt.Rows[i]["actcode"].ToString();
                    string rescode = dt.Rows[i]["subcode"].ToString();
                    string chequeno = dt.Rows[i]["chequeno"].ToString();
                    string acvounum = dt.Rows[i]["acvounum"].ToString().Trim() == "" ? "00000000000000" : dt.Rows[i]["acvounum"].ToString();


                    string chequedate = Convert.ToDateTime(dt.Rows[i]["chequedate"]).ToString("dd-MMM-yyyy");
                    string Dramt = (this.txtcurrentvou.Text.Trim().Substring(0, 2) == "PV" ? Convert.ToDouble(dt.Rows[i]["netam"]).ToString() : (Convert.ToDouble(dt.Rows[i]["netam"]) * -1).ToString());
                    string trnremarks = dt.Rows[i]["trnrmrk"].ToString();
                    string payto = dt.Rows[i]["payto"].ToString();
                    string billno = dt.Rows[i]["billno"].ToString();
                    string insofissueno = dt.Rows[i]["insofissue"].ToString();
                    double taxamt = Convert.ToDouble(dt.Rows[i]["taxam"]);

                    if (this.Request.QueryString["Type"] == "Mgt")
                    {
                        isunum = dt.Rows[i]["isunum"].ToString();
                        if (isunum.Length == 0)
                            isunum = this.GetIssueNo();

                    }

                    bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "INOFUPACPMNTA", vounum, actcode, rescode, chequeno, cactcode,
                                   voudat, Dramt, chequedate, trnremarks, vtcode, payto, isunum, acvounum, billno, insofissueno);


                    //970100101001 TAX AMOUNT



                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        return;
                    }

                    if (taxamt != 0.00)


                    {
                        string rsircode = "970100101001";
                        bool resultt = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "INSERTORUPSUPTAX", billno, rsircode, taxamt.ToString(), "", "",
                                     "", "", "", "", "", "", "", "", "", "");


                        //970100101001 TAX AMOUNT



                        if (!resulta)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            return;
                        }


                    }


                    //if (this.Request.QueryString["Mod"] == "Accounts")
                    //{
                    //    isunum = this.GetIssueNo();
                    //    this.lblissueno.Text = isunum;
                    //}
                }



             ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                this.lnkFinalUpdate.Enabled = false;
                ((LinkButton)this.Master.FindControl("lnkPrint")).Focus();
                string eventdesc = "Voucher: " + this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim() + " Dated: " + this.txtEntryDate.Text.Trim();
                string eventdesc2 = this.txtNarration.Text.Trim();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), voutype, eventdesc, eventdesc2);
                // this.GetPriviousVoucher();
                // this.txtcurrentvou.Text = "";
                //this.txtCurrntlast6.Text = "";


            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }



        }

        protected DateTime GetBackDate()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string entrydate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETBDATE", userid, entrydate, "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return (System.DateTime.Today);
            }

            return (Convert.ToDateTime(ds2.Tables[0].Rows[0]["bdate"]));
        }





        protected void GetVouCherNumber()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                ((Label)this.Master.FindControl("lblmsg")).Text = "";

                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate > Convert.ToDateTime(ASTUtility.DateFormat(this.txtEntryDate.Text.Trim())))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    return;

                }
                string VNo = (this.Request.QueryString["tname"].ToString().Contains("Payment") ? "PV" : "DV"); ;
                string entrydate = ASTUtility.DateFormat(this.txtEntryDate.Text.Trim());
                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETNEWVOUCHER", entrydate, VNo, "", "", "", "", "", "", "");
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

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            if (this.chkPrint.Checked)
                this.RptPostDatChq();
            else
                this.VouPrint();
        }
        private void VouPrint()
        {
            try
            {
                if (this.ddlPrivousVou.Items.Count > 0 && this.lnkOk.Text == "Ok")
                    this.lnkOk_Click(null, null);

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string combranch = hst["combranch"].ToString();
                string compname = hst["compname"].ToString();
                string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string curvoudat = this.txtEntryDate.Text;
                string pvnum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + this.txtcurrentvou.Text.Trim().Substring(2, 2) + "-" + this.txtCurrntlast6.Text.Trim();
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(6, 4) +
                        this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();


                string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
                string currentptah = "AccPrint.aspx?Type=PostDatVou&vounum=" + vounum;
                string totalpath = hostname + currentptah;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }
        private void RptPostDatChq()
        {
            try
            {


                try
                {
                    string vounum = this.ddlChqList.SelectedValue.ToString();
                    string paytype = "1";
                    string type = "Type=AccPostDatChq&vounum=" + vounum + "&paytype=" + paytype;
                    ScriptManager.RegisterStartupScript(this, GetType(), "target", "PrintRptCheque('" + type + "');", true);
                }
                catch (Exception ex)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }

        }
        protected void chkPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrint.Checked)
            {
                this.ddlChqList.Visible = true;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string voudat = this.txtEntryDate.Text;
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(6, 4) +
                                this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();

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
        private string CompanyPrintCheque()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string chequeprint = "";
            switch (comcod)
            {

                case "2305":
                case "3305":

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


                case "3306":
                    chequeprint = "PrintCheque03";
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

        private void PrinCheque()
        {

        }


        private void PrintCheque01()
        {

        }

        private void PrintCheque02()
        {



        }
        private void PrintVoucher()
        {

        }

        protected void ibtnFindConCode_Click(object sender, EventArgs e)
        {
            this.LoadAcccombo();
        }
        protected void ibtnFindPrv_Click(object sender, EventArgs e)
        {
            this.GetPriviousVoucher();
        }
        protected void lnkPrivVou_Click(object sender, EventArgs e)
        {

        }
        protected void lnkBillNo_Click(object sender, EventArgs e)
        {
            this.GetBillNo();
        }


    }
}


