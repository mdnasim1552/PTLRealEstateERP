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

    public partial class SuplierPayment02 : System.Web.UI.Page
    {
        public static double TAmount;

        ProcessAccess accData = new ProcessAccess();
        AutoCompleted AutoData = new AutoCompleted();
        public static string lblTitle;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //((Label)this.Master.FindControl("lblTitle")).Text = "Supplier Payment Voucher(After Deduction)";  //Request.QueryString["tname"].ToString();
                lblTitle = Request.QueryString["tname"].ToString();
                this.Master.Page.Title = Request.QueryString["tname"].ToString();
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.lnkFinalUpdate.Enabled = (Convert.ToBoolean(dr1[0]["entry"]));




                lnkFinalUpdate.Attributes.Add("onClick", " javascript:return confirm('You sure you want to Save the record?');");

                this.txtEntryDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //this.GetResCode();
                this.LoadAcccombo();
                this.GetRecAndPayto();
                this.CompanyPost();

            }

            this.Visibility();

        }

        private void CompanyPost()
        {


            string comcod = this.GetCompCode();

            switch (comcod)
            {

                case "3332":
                    //  case "3101":
                    this.chkpost.Checked = true;
                    break;


                default:
                    this.chkpost.Checked = false;
                    break;
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


                case "3330":
                    vouprint = "VocherPrint6";
                    break;

                case "3101":
                case "3332":
                case "3333":
                    vouprint = "VocherPrintIns";
                    break;
                default:
                    vouprint = "VocherPrint";
                    break;
            }
            return vouprint;
        }

        public void GetRecAndPayto()
        {
            Session.Remove("tblrecandPayto");
            string comcod = this.GetCompCode();
            AutoData.GetRecAndPayto(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETPAYRECCOD", "", "", "", "", "", "", "", "", "");

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
            tblt01.Columns.Add("spclcode", Type.GetType("System.String"));
            tblt01.Columns.Add("actdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("subdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("spcldesc", Type.GetType("System.String"));
            tblt01.Columns.Add("trnqty", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnrate", Type.GetType("System.Double"));
            tblt01.Columns.Add("trndram", Type.GetType("System.Double"));

            tblt01.Columns.Add("taxam", Type.GetType("System.Double"));
            tblt01.Columns.Add("netam", Type.GetType("System.Double"));
            tblt01.Columns.Add("trncram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnrmrk", Type.GetType("System.String"));
            tblt01.Columns.Add("recndt", Type.GetType("System.String"));
            tblt01.Columns.Add("rpcode", Type.GetType("System.String"));
            tblt01.Columns.Add("billno", Type.GetType("System.String"));
            ViewState["tblt01"] = tblt01;
            //DataTable tblt02 = new DataTable();
            //tblt02.Columns.Add("actcode", Type.GetType("System.String"));
            //tblt02.Columns.Add("subcode", Type.GetType("System.String"));
            //tblt02.Columns.Add("spclcode", Type.GetType("System.String"));
            //tblt02.Columns.Add("actdesc", Type.GetType("System.String"));
            //tblt02.Columns.Add("subdesc", Type.GetType("System.String"));
            //tblt02.Columns.Add("spcldesc", Type.GetType("System.String"));
            //tblt02.Columns.Add("trnqty", Type.GetType("System.Double"));
            //tblt02.Columns.Add("trnrate", Type.GetType("System.Double"));
            //tblt02.Columns.Add("trndram", Type.GetType("System.Double"));
            //tblt02.Columns.Add("trncram", Type.GetType("System.Double"));
            //tblt02.Columns.Add("trnrmrk", Type.GetType("System.String"));
            //tblt02.Columns.Add("recndt", Type.GetType("System.String"));
            //tblt02.Columns.Add("rpcode", Type.GetType("System.String"));
            //tblt02.Columns.Add("billno", Type.GetType("System.String"));
            //Session["tblt02"] = tblt02;
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
            DataSet ds3 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBILLNO", "", filter1, "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.ddlresuorcecode.Items.Clear();
                return;

            }

            this.ddlresuorcecode.DataSource = ds3.Tables[1];
            this.ddlresuorcecode.DataTextField = "resdesc1";
            this.ddlresuorcecode.DataValueField = "rescode";
            this.ddlresuorcecode.DataBind();
            this.txtserchReCode.Text = "";
            ViewState["tblbill"] = ds3.Tables[0];
            this.GetBillNo();




        }

        private void GetBillNo()
        {
            try
            {
                string ssircode = this.ddlresuorcecode.SelectedValue.ToString();
                string mrfno = "%" + this.txtserchBill.Text.Trim() + "%";
                DataTable dt = ((DataTable)ViewState["tblbill"]).Copy();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("rescode='" + ssircode + "' and textfield like '" + mrfno + "'");
                //this.ddlBillList.DataSource = dv.ToTable();
                //this.ddlBillList.DataTextField = "textfield";
                //this.ddlBillList.DataValueField = "billno";
                //this.ddlBillList.DataBind();


                this.DropCheck1.DataSource = dv.ToTable();
                this.DropCheck1.DataTextField = "textfield";
                this.DropCheck1.DataValueField = "billno";
                this.DropCheck1.DataBind();
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

                DataSet _NewDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "EDITVOUCHER", newvoumum, "", "", "", "", "", "", "", "");
                Session["UserLog"] = _NewDataSet.Tables[0];

                if (this.ddlPrivousVou.Items.Count > 0)
                {

                    string vounum = this.ddlPrivousVou.SelectedValue.ToString();
                    DataSet _EditDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "EDITVOUCHER", vounum, "", "", "", "", "", "", "", "");
                    DataTable dt = this.HiddenSameData(_EditDataSet.Tables[0]);
                    this.dgv1.DataSource = dt;
                    if (dt.Rows.Count == 0)
                        return;

                    this.dgv1.DataBind();
                    this.CalculatrGridTotal();
                    Session["UserLog"] = _EditDataSet.Tables[1];
                    //-------------** Edit **---------------------------//
                    DataTable dtedit = _EditDataSet.Tables[1];
                    this.txtEntryDate.Text = Convert.ToDateTime(dtedit.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                    this.lblisunum.Text = dtedit.Rows[0]["isunum"].ToString();
                    this.txtRefNum.Text = dtedit.Rows[0]["refnum"].ToString();
                    this.txtSrinfo.Text = dtedit.Rows[0]["srinfo"].ToString();
                    this.txtPayto.Text = dtedit.Rows[0]["payto"].ToString();
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
                this.txttaxamt.Text = " ";
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
                this.lblcurVounum.Text = "Current Voucher No.";
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
            this.txtSrinfo.Text = "";
            this.txtRefNum.Text = "";
            this.txtPayto.Text = "";
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
        protected void lnkAdd_Click(object sender, EventArgs e)
        {

            //  string billnoal="";
            string rescode = this.ddlresuorcecode.SelectedValue.ToString();
            // string billno = this.ddlBillList.SelectedValue.ToString();
            // string billno = this.DropCheck1.SelectedValue.ToString();

            string[] arbillno = this.DropCheck1.Text.Trim().Split(',');

            DataTable tbl1 = (DataTable)ViewState["tblt01"];
            DataTable dt1 = (DataTable)ViewState["tblbill"];

            foreach (string billno in arbillno)
            {
                double taxam = Convert.ToDouble("0" + this.txttaxamt.Text.Trim());
                DataRow[] dr2 = tbl1.Select("billno='" + billno + "'");
                if (dr2.Length == 0)
                {

                    DataRow[] drb = dt1.Select("billno='" + billno + "'");

                    DataRow dr1 = tbl1.NewRow();
                    dr1["actcode"] = drb[0]["pactcode"].ToString();
                    dr1["subcode"] = rescode;
                    dr1["spclcode"] = "000000000000";
                    dr1["actdesc"] = drb[0]["pactdesc"].ToString();
                    dr1["subdesc"] = this.ddlresuorcecode.SelectedItem.Text;
                    dr1["spcldesc"] = "";
                    dr1["trnqty"] = 0.00;
                    dr1["trnrate"] = 0.00;
                    dr1["taxam"] = taxam;
                    dr1["trndram"] = drb[0]["amt"].ToString();
                    dr1["trncram"] = 0.00;
                    dr1["trnrmrk"] = "";
                    dr1["recndt"] = "";
                    dr1["netam"] = Convert.ToDouble(drb[0]["amt"]) - taxam;
                    dr1["rpcode"] = "";
                    dr1["billno"] = billno;
                    tbl1.Rows.Add(dr1);
                }


            }



            ViewState["tblt01"] = HiddenSameData02(tbl1);
            this.Data_Bind();




            //    try
            //    {
            //        //----------------Add Data Into Grid--------------------------//
            //        this.Panel4.Visible = true;
            //        string AccCode = this.ddlacccode.SelectedValue.ToString();
            //        string ResCode = this.ddlresuorcecode.SelectedValue.ToString();
            //        string Billno = this.ddlBillList.Items.Count > 0 ? this.ddlBillList.SelectedValue.ToString() : "";
            //        ResCode = (ResCode.Length < 12 ? "000000000000" : ResCode);

            //        string actlev = (((DataTable)ViewState["HeadAcc1"]).Select("actcode='" + AccCode + "'"))[0]["actelev"].ToString();
            //        if (actlev == "2")
            //        {
            //            if (ResCode == "000000000000")
            //            {
            //                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Select Details Head');", true);
            //                this.txtserchReCode.Focus();
            //                return;

            //            }

            //        }

            //        string billno = this.ddlBillList.SelectedValue.ToString();

            //        string SpclCode = this.ddlSpclinf.SelectedValue.ToString();
            //        SpclCode = (SpclCode.Length < 12 ? "000000000000" : SpclCode);
            //        string AccDesc = this.ddlacccode.SelectedItem.Text.Trim();
            //        string ResDesc = (ResCode == "000000000000" ? "" : this.ddlresuorcecode.SelectedItem.Text.Trim());
            //        string SpclDesc = (SpclCode == "000000000000" ? "" : this.ddlSpclinf.SelectedItem.Text.Trim().Substring(13));
            //        double TrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtqty.Text.Trim()));
            //        double Trnrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtrate.Text.Trim()));
            //        double TrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtDrAmt.Text.Trim()));
            //        double TrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtCrAmt.Text.Trim()));
            //        string TrnRemarks = this.txtremarks.Text.Trim();
            //        DataTable tblt01 = (DataTable)Session["tblt01"];
            //        DataTable tblt02 = (DataTable)Session["tblt02"];
            //        DataTable tblt03 = new DataTable();
            //        tblt01.Rows.Clear();
            //        tblt02.Rows.Clear();
            //        tblt03.Rows.Clear();

            //        for (int i = 0; i < this.dgv1.Rows.Count; i++)
            //        {
            //            string dgAccCode = ((Label)this.dgv1.Rows[i].FindControl("lblAccCod")).Text.Trim();
            //            string dgResCode = ((Label)this.dgv1.Rows[i].FindControl("lblResCod")).Text.Trim();
            //            string dgBillno = ((Label)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text.Trim();


            //            //-----------If Repetation ---------------------------------------------------------//
            //            if (dgAccCode + dgResCode + dgBillno == AccCode + ResCode + Billno)
            //            {
            //                ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text = SpclCode;
            //                ((Label)this.dgv1.Rows[i].FindControl("lblSpcldesc")).Text = SpclDesc;
            //                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text = TrnQty.ToString("#,##0.00;(#,##0.00); ");
            //                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text = Trnrate.ToString("#,##0.00;(#,##0.00); ");
            //                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text = TrnDrAmt.ToString("#,##0.00;(#,##0.00); ");
            //                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text = TrnCrAmt.ToString("#,##0.00;(#,##0.00); ");
            //                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRemarks")).Text = TrnRemarks;
            //                ((Label)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text = billno;


            //                //this.CalculatrGridTotal();
            //                //return;
            //            }
            //            else
            //            {
            //                //--------------------------------------------------------------------------------//
            //                string dgSpclCode = ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text.Trim();
            //                string dgAccDesc = ((Label)this.dgv1.Rows[i].FindControl("lblAccdesc")).Text.Trim();
            //                string dgResDesc = ((Label)this.dgv1.Rows[i].FindControl("lblResdesc")).Text.Trim();
            //                string dgSpclDesc = ((Label)this.dgv1.Rows[i].FindControl("lblSpcldesc")).Text.Trim();
            //                double dgTrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()));
            //                double dgTrnrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text.Trim()));
            //                double dgTrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
            //                double dgTrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text.Trim()));
            //                string dgTrnRemarks = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
            //                string recndt = ((Label)this.dgv1.Rows[i].FindControl("lblrecndat")).Text;
            //                string rpcode = ((Label)this.dgv1.Rows[i].FindControl("lblgvrpcode")).Text;
            //                string billno1 = ((Label)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text;
            //                //actcode subcode actdesc subdesc trnqty trnrate trndram trncram trnrmrk 
            //                DataRow dr1 = tblt01.NewRow();
            //                dr1["actcode"] = dgAccCode;
            //                dr1["subcode"] = dgResCode;
            //                dr1["spclcode"] = dgSpclCode;
            //                dr1["actdesc"] = dgAccDesc;
            //                dr1["subdesc"] = dgResDesc;
            //                dr1["spcldesc"] = dgSpclDesc;
            //                dr1["trnqty"] = dgTrnQty;
            //                dr1["trnrate"] = dgTrnrate;
            //                dr1["trndram"] = dgTrnDrAmt;
            //                dr1["trncram"] = dgTrnCrAmt;
            //                dr1["trnrmrk"] = dgTrnRemarks;
            //                dr1["recndt"] = recndt;
            //                dr1["rpcode"] = rpcode;
            //                dr1["billno"] = billno1;
            //                tblt01.Rows.Add(dr1);
            //            }
            //        }
            //        DataRow dr2 = tblt01.NewRow();
            //        dr2["actcode"] = AccCode;
            //        dr2["subcode"] = ResCode;
            //        dr2["spclcode"] = SpclCode;
            //        dr2["actdesc"] = AccDesc;
            //        dr2["subdesc"] = ResDesc;
            //        dr2["spcldesc"] = SpclDesc;
            //        dr2["trnqty"] = TrnQty;
            //        dr2["trnrate"] = Trnrate;
            //        dr2["trndram"] = TrnDrAmt;
            //        dr2["trncram"] = TrnCrAmt;
            //        dr2["trnrmrk"] = TrnRemarks;
            //        dr2["recndt"] = "";
            //        dr2["rpcode"] = "";
            //        dr2["billno"] = billno;
            //        tblt01.Rows.Add(dr2);
            //        //--------------** Remove Duplicate Value **----------------------------//
            //        //--** Only Actdesc remove actcod not remove from grid **---------------// 
            //        DataView dv1 = tblt01.DefaultView;
            //        dv1.Sort = "actcode";
            //        tblt03 = dv1.ToTable();
            //        string AccDesc1 = null;
            //        for (int j = 0; j < tblt03.Rows.Count; j++)
            //        {
            //            DataRow dr3 = tblt02.NewRow();
            //            dr3["actcode"] = tblt03.Rows[j]["actcode"].ToString();
            //            dr3["subcode"] = tblt03.Rows[j]["subcode"].ToString();
            //            dr3["spclcode"] = tblt03.Rows[j]["spclcode"].ToString();
            //            string tserch = tblt03.Rows[j]["actcode"].ToString();
            //            if (tserch == AccDesc1 || tserch == "")
            //            {
            //                dr3["actdesc"] = "";
            //            }
            //            else
            //            {
            //                dr3["actdesc"] = tblt03.Rows[j]["actdesc"].ToString();
            //                AccDesc1 = tblt03.Rows[j]["actcode"].ToString();
            //            }

            //            dr3["subdesc"] = tblt03.Rows[j]["subdesc"].ToString();
            //            dr3["spcldesc"] = tblt03.Rows[j]["spcldesc"].ToString();
            //            dr3["trnqty"] = Convert.ToDouble(tblt03.Rows[j]["trnqty"].ToString());
            //            dr3["trnrate"] = Convert.ToDouble(tblt03.Rows[j]["trnrate"].ToString());
            //            dr3["trndram"] = Convert.ToDouble(tblt03.Rows[j]["trndram"].ToString());
            //            dr3["trncram"] = Convert.ToDouble(tblt03.Rows[j]["trncram"].ToString());
            //            dr3["trnrmrk"] = tblt03.Rows[j]["trnrmrk"].ToString();
            //            dr3["recndt"] = tblt03.Rows[j]["recndt"].ToString();
            //            dr3["billno"] = tblt03.Rows[j]["billno"].ToString();
            //            tblt02.Rows.Add(dr3);

            //        }
            //        //---------------------------------------------//
            //        dgv1.DataSource = tblt02;


            //        //dgv1.DataSource = tblt01;
            //        dgv1.DataBind();
            //        this.CalculatrGridTotal();
            //        //this.ddlacccode.BackColor = System.Drawing.Color.Beige;
            //        //this.ddlresuorcecode.BackColor = System.Drawing.Color.Beige;
            //        //this.ddlSpclinf.BackColor = System.Drawing.Color.Beige;

            //    }
            //    catch (Exception ex)
            //    {
            //     ((Label)this.Master.FindControl("lblmsg")).Text = "Error :" + ex.Message;
            //    }
        }


        private void Data_Bind()
        {
            dgv1.DataSource = (DataTable)ViewState["tblt01"];
            dgv1.DataBind();
            this.CalculatrGridTotal();


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
        protected void CalculatrGridTotal()
        {

            DataTable dt = (DataTable)ViewState["tblt01"];
            if (dt.Rows.Count == 0)
                return;


            //((TextBox)this.dgv1.FooterRow.FindControl("txtTgvQty")).Text = TQty.ToString("#,##0.00;(#,##0.00); ");
            //((TextBox)this.dgv1.FooterRow.FindControl("txtTgvRate")).Text = TRate.ToString("#,##0.00;(#,##0.00); ");
            ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trndram)", "")) ? 0.00 : dt.Compute("Sum(trndram)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((Label)this.dgv1.FooterRow.FindControl("lblgvFtaxamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(taxam)", "")) ? 0.00 : dt.Compute("Sum(taxam)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((Label)this.dgv1.FooterRow.FindControl("lblgvFnetamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(netam)", "")) ? 0.00 : dt.Compute("Sum(netam)", ""))).ToString("#,##0.00;(#,##0.00);  ");



        }

        private void SaveValue()
        {

            DataTable dt = (DataTable)ViewState["tblt01"];
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                double dg1TrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()));
                double dg1TrnRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text.Trim()));
                double dg1TrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
                double dg1TrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text.Trim()));
                double dg1Tax = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvtaxamt")).Text.Trim()));
                // double dg1netamout = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("lblgvnetamt")).Text.Trim()));


                dt.Rows[i]["trnqty"] = dg1TrnQty;
                dt.Rows[i]["trnrate"] = 0.00;
                dt.Rows[i]["taxam"] = dg1Tax;
                dt.Rows[i]["trndram"] = dg1TrnDrAmt;
                dt.Rows[i]["trncram"] = 0.00;
                dt.Rows[i]["trnrmrk"] = "";
                dt.Rows[i]["recndt"] = "";
                dt.Rows[i]["netam"] = dg1TrnDrAmt - dg1Tax;
            }



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


            DataTable dt = (DataTable)ViewState["tblt01"];

            this.CalculatrGridTotal();

            //if (this.txtcurrentvou.Text.Trim() != "")
            //{
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            double ToDramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvDrAmt")).Text.Trim());
            double ToCramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvCrAmt")).Text.Trim());

            if (ToDramt == 0 && ToCramt == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Amount is not Available";
                return;
            }


            ////Log Entry

            //DataTable dtuser = (DataTable)Session["UserLog"];
            //string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            //string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            //string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            //string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string userid = hst["usrid"].ToString();
            //string Terminal = hst["compname"].ToString();
            //string Sessionid = hst["session"].ToString();
            //string PostedByid = (this.Request.QueryString["Mod"] == "Accounts") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            //string Posttrmid = (this.Request.QueryString["Mod"] == "Accounts") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            //string PostSession = (this.Request.QueryString["Mod"] == "Accounts") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            //string Posteddat = (this.Request.QueryString["Mod"] == "Accounts") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
            //string EditByid = (this.Request.QueryString["Mod"] == "Accounts") ? "" : userid;
            //string Editdat = (this.Request.QueryString["Mod"] == "Accounts") ? "01-Jan-1900" : System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");





            //Log Entry

            DataTable dtuser = (DataTable)Session["UserLog"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (this.Request.QueryString["Mod"] == "Accounts") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = (this.Request.QueryString["Mod"] == "Accounts") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = (this.Request.QueryString["Mod"] == "Accounts") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string Posteddat = (this.Request.QueryString["Mod"] == "Accounts") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
            string EditByid = (this.Request.QueryString["Mod"] == "Accounts") ? "" : userid;
            string Editdat = (this.Request.QueryString["Mod"] == "Accounts") ? "01-Jan-1900" : System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string pounaction = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["pounaction"].ToString();
            string aprovbyid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvbyid"].ToString();
            string aprvtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvtrmid"].ToString();
            string aprvseson = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvseson"].ToString();
            string aprvdat = (dtuser.Rows.Count == 0) ? "01-jan-1900" : dtuser.Rows[0]["aprvdat"].ToString();
            string userdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");





            string Payto = this.txtPayto.Text.Trim();
            string isunum = this.lblisunum.Text.Trim();
            //string EditByid = (this.Request.QueryString["Mod"] == "Accounts") ? "" : (tblEditByid == "") ? userid : tblEditByid;


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

            //Ref Number
            this.CheeckRefNumber();
            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                            this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            string refnum = this.txtRefNum.Text.Trim();
            string srinfo = this.txtSrinfo.Text;
            string vounarration1 = this.txtNarration.Text.Trim();
            string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
            string vouno = this.txtcurrentvou.Text.Trim().Substring(0, 2);
            string vtcode = Request.QueryString["tcode"];
            string voutype = (vtcode == "92") ? "Contra Voucher" : (vouno == "JV" ? "Journal Voucher" :
                             (vouno == "CD" ? "Cash Payment Voucher" :
                             (vouno == "BD" ? "Bank Payment Voucher" :
                             (vouno == "CC" ? "Cash Deposit Voucher" :
                             (vouno == "BC" ? "Bank Deposit Voucher" : "Unknown Voucher")))));


            string cactcode = (vouno == "JV" ? "000000000000" : this.ddlConAccHead.SelectedValue.ToString());


            string edit = (this.txtCurrntlast6.Enabled ? "" : "EDIT");
            string TgvDrAmt = ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvDrAmt")).Text;
            string TgvCrAmt = ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvCrAmt")).Text;
            if (vouno == "JV" && TgvDrAmt != TgvCrAmt)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Dr. Amount not equals to Cr. Amount.";
                return;
            }



            string CallType = (this.chkpost.Checked && aprovbyid.Length > 0) ? "ACVUPDATE02" : (this.chkpost.Checked) ? "ACVUPDATEUNPOSTED" : "ACVUPDATE02";


            //-----------Update Transaction B Table-----------------//
            bool resultb = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, voudat, refnum, srinfo, vounarration1,
                            vounarration2, voutype, vtcode, edit, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, Payto, isunum, aprovbyid, aprvtrmid, aprvseson, aprvdat, pounaction, "", "", "", "");



            ////-----------Update Transaction B Table-----------------//
            //    bool resultb = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo, vounarration1,
            //                    vounarration2, voutype, vtcode, edit, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, Payto, isunum, "", "", "", "");
            if (!resultb)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                return;
            }
            //-----------Update Transaction A Table-----------------//
            for (int i = 0; i < dgv1.Rows.Count; i++)
            {
                string actcode = ((Label)this.dgv1.Rows[i].FindControl("lblAccCod")).Text.Trim();
                string rescode = ((Label)this.dgv1.Rows[i].FindControl("lblResCod")).Text.Trim();
                string spclcode = ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                string trnqty = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()).ToString();
                double Dramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("lblgvnetamt")).Text.Trim());
                //double Dramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim());//lblgvnetamt
                double Cramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text.Trim());
                string trnamt = Convert.ToString(Dramt - Cramt);
                string trnremarks = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                string recndt = ((Label)this.dgv1.Rows[i].FindControl("lblrecndat")).Text.Trim();
                string rpcode = ((Label)this.dgv1.Rows[i].FindControl("lblgvrpcode")).Text.Trim();
                string billno = ((Label)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text.Trim();
                double taxamt = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvtaxamt")).Text.Trim());

                bool resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, actcode, rescode, cactcode,
                     voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, recndt, rpcode, billno, userid, userdate, Terminal, "", "", "", "", "", "", "", "", "", "");

                //bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, actcode, rescode, cactcode,
                //               voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, recndt, rpcode, billno, "", "");
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
            }






            DataRow[] dr1 = dt.Select("taxam>0");
            if (dr1.Length > 0)
            {
                //////////////////////////////// JV Part /////////////////////////////////////////////


                string entrydate = this.txtEntryDate.Text.Substring(0, 11).Trim();
                DataSet ds6 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, "JV", "", "", "", "", "", "", "");
                string jvvounum = ds6.Tables[0].Rows[0]["couvounum"].ToString();

                string jvvouno1 = jvvounum.Substring(0, 2).ToString();
                string voutype1 = (jvvouno1 == "JV" ? "Journal Voucher" :
                                    (jvvouno1 == "CD" ? "Cash Payment Voucher" :
                                    (jvvouno1 == "BD" ? "Bank Payment Voucher" :
                                    (jvvouno1 == "CC" ? "Cash Deposit Voucher" :
                                    (jvvouno1 == "BC" ? "Bank Deposit Voucher" : "Unknown Voucher")))));
                string cactcode1 = (jvvouno1 == "JV" ? "000000000000" : cactcode);

                //-----------Update Transaction B Table-----------------//
                //bool resultd = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", jvvounum, voudat, refnum, srinfo, vounarration1,
                //                vounarration2, voutype1, vtcode, edit, userid, Terminal, Sessionid, Posteddat, "", "");

                bool resultd = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, jvvounum, voudat, refnum, srinfo, vounarration1,
                       vounarration2, voutype, vtcode, edit, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, Payto, isunum, aprovbyid, aprvtrmid, aprvseson, aprvdat, pounaction, "", "", "", "");

                if (!resultd)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    return;
                }
                //-----------Update Transaction A Table-----------------//

                //string spclcode = "000000000000";
                //bool resulta;





                for (int i = 0; i < dgv1.Rows.Count; i++)
                {
                    string actcode = ((Label)this.dgv1.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string rescode = ((Label)this.dgv1.Rows[i].FindControl("lblResCod")).Text.Trim();
                    string spclcode = ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string recndt = "01-jan-1900";
                    string rpcode = ((Label)this.dgv1.Rows[i].FindControl("lblgvrpcode")).Text.Trim();
                    string billno = ((Label)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text.Trim();

                    double taxamt = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvtaxamt")).Text.Trim());


                    bool resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, jvvounum, actcode, rescode, cactcode1,
                     voudat, "0", billno, vtcode, taxamt.ToString(), spclcode, recndt, rpcode, "", userid, userdate, Terminal, "", "", "", "", "", "", "", "", "", "");

                    //bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", jvvounum, actcode, rescode, cactcode1,
                    //               voudat, "0", billno, vtcode, taxamt.ToString(), spclcode, recndt, rpcode, "", "", "");
                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        return;
                    }

                    rescode = "970100101001";
                    resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, jvvounum, actcode, rescode, cactcode1,
                    voudat, "0", billno, vtcode, (taxamt * -1).ToString(), spclcode, recndt, rpcode, "", userid, userdate, Terminal, "", "", "", "", "", "", "", "", "", "");

                    //resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", jvvounum, actcode, rescode, cactcode1,
                    //              voudat, "0", billno, vtcode, (taxamt * -1).ToString(), spclcode, recndt, rpcode, "", "", "");
                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        return;
                    }




                }

            }
                 //if (ConstantInfo.LogStatus == true)
                 //{
                 //    string eventtype = "Collection Update";
                 //    string eventdesc = "Update Collection";
                 //    string eventdesc2 = jvvounum;
                 //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                 //}





                 ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
            this.lnkFinalUpdate.Enabled = false;

            //string eventdesc = "Voucher: " + this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim() + " Dated: " + this.txtEntryDate.Text.Trim();
            //string eventdesc2 = this.txtNarration.Text.Trim();
            //bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]),  voutype, eventdesc, eventdesc2);



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

        private void CheeckRefNumber()
        {
            string Vounum = ASTUtility.Left(this.txtcurrentvou.Text.Trim(), 2);
            switch (Vounum)
            {
                case "BD":
                case "BC":
                case "CT":
                    if (this.txtRefNum.Text.Trim() == "")
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Fill Reference Number');", true);
                    break;
            }




        }



        private void GetVouCherNumber()
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

                if (txtopndate >= Convert.ToDateTime(this.txtEntryDate.Text.Trim().Substring(0, 11)))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    return;



                }

                double vcode1 = Convert.ToDouble(Request.QueryString["tcode"]);
                string ConAccHead = this.ddlConAccHead.SelectedValue.ToString();
                string VNo1 = (lblTitle.Contains("Journal") ? "J" : (lblTitle.Contains("Contra") ? "C" :
                    (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B")));
                string VNo2 = (VNo1 == "J" ? "V" : (lblTitle.Contains("Payment") ? "D" : (lblTitle.Contains("Contra") ? "T" : "C")));
                string VNo3 = Convert.ToString(VNo1 + VNo2);
                string entrydate = this.txtEntryDate.Text.Substring(0, 11).Trim();

                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
                DataTable dt4 = ds4.Tables[0];
                string cvno1 = dt4.Rows[0]["couvounum"].ToString().Substring(0, 8);
                this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
                this.txtCurrntlast6.Text = dt4.Rows[0]["couvounum"].ToString().Substring(8);
                string pvno1 = ds4.Tables[1].Rows[0]["lastvounum"].ToString().Trim();

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

            }


        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            if (this.chkPrint.Checked)
                this.PrinCheque();
            else
                this.PrintVoucher();


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
            try
            {

                //string Chequeprint = this.CompanyPrintCheque();
                //if(Chequeprint=="PrintCheque01")
                //    this.PrintCheque01();


                //else
                //else  PrintCheque02();

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string curvoudat = this.txtEntryDate.Text.Substring(0, 11);
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                        this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
                DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt = _ReportDataSet.Tables[0];
                if (dt.Rows.Count == 0)
                    return;
                double toamt, dramt, cramt;
                dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));
                if (dramt > 0 && cramt <= 0)
                    toamt = dramt;
                else
                    toamt = cramt;
                string chequedat = Convert.ToDateTime(this.txtEntryDate.Text).ToString("ddMMyyyy");
                chequedat = chequedat.Substring(0, 1) + "   " + chequedat.Substring(1, 1) + "   " + chequedat.Substring(2, 1) + "   " + chequedat.Substring(3, 1) + "   " + chequedat.Substring(4, 1) + "   " + chequedat.Substring(5, 1) + "   " + chequedat.Substring(6, 1) + "   " + chequedat.Substring(7, 1);
                string payto = this.txtPayto.Text.Trim();
                string amt1 = ASTUtility.Trans(Math.Round(toamt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));


                string Chequeprint = this.CompanyPrintCheque();
                //if(Chequeprint=="PrintCheque01")
                //    this.PrintCheque01();

                ReportDocument rptinfo = new ReportDocument();
                if (Chequeprint == "PrintCheque01")
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheck();

                else if (Chequeprint == "PrintCheque02")
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheeque02();
                else if (Chequeprint == "PrintChequeAssure")
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheequeAssure();
                //else if (Chequeprint == "PrintCheque03")
                //    rptinfo = new RealERPRPT.R_17_Acc.PrintCheeque03();
                else
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheeque02();

                TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                date.Text = chequedat;
                TextObject rpttxtpayto = rptinfo.ReportDefinition.ReportObjects["txtpayto"] as TextObject;
                rpttxtpayto.Text = payto;
                TextObject rpttxtamtinword = rptinfo.ReportDefinition.ReportObjects["txtamtinword"] as TextObject;
                rpttxtamtinword.Text = amt2;
                TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                rpttxtamt.Text = toamt.ToString("#,##0;(#,##0); ") + "/=";

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = lblTitle;
                    string eventdesc = "Print Cheque";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
                rptinfo.SetDataSource(dt);
                Session["Report1"] = rptinfo;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }

        }


        private void PrintCheque01()
        {

        }

        private void PrintCheque02()
        {



        }
        private string GetCompInstar()
        {

            string comcod = this.GetCompCode();
            string printinstar = "";
            switch (comcod)
            {
                case "3332":
                    //case "3101":
                    printinstar = "Innstar";
                    break;

                default:

                    break;


            }
            return printinstar;
        }
        private void PrintVoucher()
        {
            try
            {
                if (this.ddlPrivousVou.Items.Count > 0 && this.lnkOk.Text == "Ok")
                    this.lnkOk_Click(null, null);

                DataTable dtuser = (DataTable)Session["UserLog"];
                string aprovbyid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvbyid"].ToString();

                Hashtable hst = (Hashtable)Session["tblLogin"];

                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string combranch = hst["combranch"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string curvoudat = this.txtEntryDate.Text.Substring(0, 11);
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                        this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
                string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;



                string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
                string currentptah = "AccPrint.aspx?Type=accVou&vounum=" + vounum;
                string totalpath = hostname + currentptah;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";



                // //string vounum = this.ddlPrivousVou.SelectedValue.ToString();
                //// string CallType = (this.chkpost.Checked && aprovbyid.Length > 0) ? "ACVUPDATE02" : (this.chkpost.Checked) ? "ACVUPDATEUNPOSTED" : "ACVUPDATE02";
                // string CallType = (this.chkpost.Checked && aprovbyid.Length > 0) ? "PRINTVOUCHER01" : (this.chkpost.Checked) ? "PRINTUNPOSTEDVOUCHER01" : "PRINTVOUCHER01";
                // string PrintInstar = this.GetCompInstar();
                // DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", CallType, vounum, PrintInstar, "", "", "", "", "", "", "");
                // if (_ReportDataSet == null)
                //     return;
                // DataTable dt = _ReportDataSet.Tables[0];
                // if (dt.Rows.Count == 0)
                //     return;
                // double dramt, cramt;
                // dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                // cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));



                // if (dramt > 0 && cramt > 0)
                // {
                //     TAmount = cramt;

                // }
                // else if (dramt > 0 && cramt <= 0)
                // {
                //     TAmount = dramt;
                // }
                // else
                // {
                //     TAmount = cramt;
                // }

                // DataTable dt1 = _ReportDataSet.Tables[1];
                // string Vounum = dt1.Rows[0]["vounum"].ToString();
                // string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                // string refnum = dt1.Rows[0]["refnum"].ToString();
                // string voutype = dt1.Rows[0]["voutyp"].ToString();
                // string venar = dt1.Rows[0]["venar"].ToString();
                // string Posteddat =Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
                // string Type = this.CompanyPrintVou();

                // LocalReport Rpt1 = new LocalReport();

                // //ReportDocument rptinfo = new ReportDocument();

                // if (Type == "VocherPrint")
                // {

                //     var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                //     Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher", list, null, null);
                //     Rpt1.EnableExternalImages = true;
                //     Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                //     Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                //     Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                //     Rpt1.SetParameters(new ReportParameter("txtPartyName", (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim()));
                //     Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));


                //     //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();
                //     //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //     //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //     //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //     //rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //     //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //     //naration.Text = "Narration: " + venar;
                //     //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //     //vounum1.Text = "Voucher No.: " + vounum;
                //     //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //     //date.Text = "Voucher Date: " + voudat;

                // }
                // else if (Type == "VocherPrint1")
                // {
                //     var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                //     Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher1", list, null, null);
                //     Rpt1.EnableExternalImages = true;
                //     Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                //     Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                //     Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                //     Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6))));
                //     Rpt1.SetParameters(new ReportParameter("txtPartyName", (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim()));
                //     Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                //     Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                //     Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                //     Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                //     //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher1();
                //     //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //     //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //     //TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //     //txtisunum1.Text = "Issue No: " +(this.lblisunum.Text.Trim()==""?this.lblisunum.Text.Trim():ASTUtility.Right(this.lblisunum.Text.Trim(),6));
                //     //TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //     //txtPosteddate1.Text = "Entry Date: " + Posteddat;
                //     //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //     //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //     //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //     //rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //     //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //     //naration.Text = "Narration: " + venar;
                //     //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //     //vounum1.Text = "Voucher No.: " + vounum;
                //     //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //     //date.Text = "Voucher Date: " + voudat;


                // }
                // else if (Type == "VocherPrint2")
                // {
                //     var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                //     Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher2", list, null, null);
                //     Rpt1.EnableExternalImages = true;
                //     Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                //     Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                //     Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                //     Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6))));
                //     Rpt1.SetParameters(new ReportParameter("txtPartyName", (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim()));
                //     Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                //     Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                //     Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                //     Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                //     //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher2();
                //     //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //     //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //     //TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //     //txtisunum2.Text = "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6));
                //     //TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //     //txtPosteddate2.Text = "Entry Date: " + Posteddat;
                //     //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //     //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //     //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //     //rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //     //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //     //naration.Text = "Narration: " + venar;
                //     //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //     //vounum1.Text = "Voucher No.: " + vounum;
                //     //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //     //date.Text = "Voucher Date: " + voudat;

                // }
                // else if (Type == "VocherPrint3")
                // {
                //     var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                //     Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher3", list, null, null);
                //     Rpt1.EnableExternalImages = true;
                //     Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                //     Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                //     Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                //     Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6))));
                //     Rpt1.SetParameters(new ReportParameter("txtPartyName", (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim()));
                //     Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                //     Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                //     Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                //     Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                //     //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher3();
                //     //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //     //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //     //TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //     //txtisunum1.Text = "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6));
                //     //TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //     //txtPosteddate1.Text = "Entry Date: " + Posteddat;
                //     //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //     //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //     //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //     //rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //     //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //     //naration.Text = "Narration: " + venar;
                //     //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //     //vounum1.Text = "Voucher No.: " + vounum;
                //     //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //     //date.Text = "Voucher Date: " + voudat;


                // }
                // else if (Type == "VocherPrint6")
                // {
                //     var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                //     Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherBridge", list, null, null);
                //     Rpt1.EnableExternalImages = true;
                //     Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                //     Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                //     Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                //     Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6))));
                //     Rpt1.SetParameters(new ReportParameter("txtPartyName", (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim()));
                //     Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));


                //     //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucherBridge();

                //     //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //     //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //     //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //     //rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //     //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //     //naration.Text = "Narration: " + venar;
                //     //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //     //vounum1.Text = "Voucher No.: " + vounum;
                //     //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //     //date.Text = "Voucher Date: " + voudat;


                // }

                // //else if (Type == "VocherPrintIns")
                // //{

                // //       if (ASTUtility.Left(vounum, 2) == "JV")
                // //    {
                // //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherIns();

                // //        //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                // //        //txtCompanyName.Text = comnam;
                // //        //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                // //        //txtcAdd.Text = comadd;
                // //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                // //        vounum1.Text = "Voucher No.: " + vounum;
                // //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                // //        date.Text = "Voucher Date: " + voudat;
                // //        TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                // //        Refnum.Text = "Cheque/Ref. No.: " + refnum;
                // //        TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                // //        rpttxtPartyName.Text = "";// (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                // //        //TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
                // //        //voutype1.Text = voutype;
                // //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                // //        naration.Text = "Narration: " + venar;
                // //    }

                // //    else
                // //    {

                // //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherIns02();

                // //        TextObject txtDesc = rptinfo.ReportDefinition.ReportObjects["txtDesc"] as TextObject;
                // //        txtDesc.Text = this.ddlConAccHead.SelectedItem.Text.Substring(13).ToString();
                // //        TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                // //        rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.txtPayto.Text.Trim();
                // //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                // //        naration.Text = venar;

                // //        TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
                // //        txtusername.Text = username;

                // //        TextObject txtProject = rptinfo.ReportDefinition.ReportObjects["txtProject"] as TextObject;
                // //        txtProject.Text = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";

                // //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                // //        vounum1.Text = vounum;
                // //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                // //        date.Text = voudat;
                // //        TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                // //        Refnum.Text =refnum;

                // //    }


                // //}


                //     //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherIns02();
                //     //TextObject txtDesc = rptinfo.ReportDefinition.ReportObjects["txtDesc"] as TextObject;
                //     //txtDesc.Text = this.ddlConAccHead.SelectedItem.Text.Substring(13).ToString();
                //     //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //     //rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.txtPayto.Text.Trim();
                //     //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //     //naration.Text = venar;

                //     //TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
                //     //txtusername.Text = username;

                //     //TextObject txtProject = rptinfo.ReportDefinition.ReportObjects["txtProject"] as TextObject;
                //     //txtProject.Text = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";

                //     //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //     //vounum1.Text = vounum;
                //     //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //     //date.Text = voudat;


                // else
                // {


                //     var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                //     Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher4", list, null, null);
                //     Rpt1.EnableExternalImages = true;
                //     Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                //     Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                //     Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                //     Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6))));
                //     Rpt1.SetParameters(new ReportParameter("txtPartyName", (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim()));
                //     Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                //     Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                //     Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                //     Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));



                //     //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher4();
                //     //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //     //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //     //TextObject txtisunum3 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //     //txtisunum3.Text = "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6));
                //     //TextObject txtPosteddate3 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //     //txtPosteddate3.Text = "Entry Date: " + Posteddat;
                //     //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //     //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //     //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //     //rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //     //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //     //naration.Text = "Narration: " + venar;
                //     //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //     //vounum1.Text = "Voucher No.: " + vounum;
                //     //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //     //date.Text = "Voucher Date: " + voudat;

                // }




                // Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                // Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                // Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                // Rpt1.SetParameters(new ReportParameter("InWrd", ASTUtility.Trans(Math.Round(TAmount), 2)));
                // Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                // Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

                // Session["Report1"] = Rpt1;
                // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";




                // //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                // //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                // //txtCompanyName.Text = comnam;
                // //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                // //txtcAdd.Text = comadd;
                // ////TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                // ////vounum1.Text = "Voucher No.: " + vounum;
                // ////TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                // ////date.Text = "Voucher Date: " + voudat;
                // ////TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                // ////Refnum.Text = "Cheque/Ref. No.: " + refnum;
                // ////TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                // ////rpttxtPartyName.Text =(this.txtPayto.Text.Trim()=="")?"": this.lblPayto.Text.Trim()+" "+ this.txtPayto.Text.Trim();
                // //TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
                // //voutype1.Text = voutype;
                // ////TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                // ////naration.Text = "Narration: " + venar;

                // ////TextObject txtBname = rptinfo.ReportDefinition.ReportObjects["bankname"] as TextObject;
                // ////txtBname.Text = this.ddlConAccHead.SelectedItem.Text.Substring(13);
                // //TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                // //rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);

                // //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                // //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                // //if (ConstantInfo.LogStatus == true)
                // //{
                // //    string eventtype = lblTitle;
                // //    string eventdesc = "Print Voucher";
                // //    string eventdesc2 = vounum;
                // //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                // //}
                // ////string comcod = this.GetComeCode();
                // ////string comcod = hst["comcod"].ToString();
                // //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                // //rptinfo.SetParameterValue("ComLogo", ComLogo);
                // //Session["Report1"] = rptinfo;
                // //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                // //           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
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


