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
    public partial class GeneralAccountsjv : System.Web.UI.Page
    {
        public static double TAmount;

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

                this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.lnkFinalUpdate.Enabled = (Convert.ToBoolean(dr1[0]["entry"]));

                lnkFinalUpdate.Attributes.Add("onClick", " javascript:return confirm('You sure you want to Save the record?');");

                this.txtEntryDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.TableCreate();
            }

            if (this.ddlConAccHead.Items.Count > 0)
                return;


            this.LoadAcccombo();

            this.lblGeneralAcc.Text = Request.QueryString["tname"].ToString();
            string vcode = Request.QueryString["tcode"];


            if (this.lblGeneralAcc.Text.Contains("Payment") || this.lblGeneralAcc.Text.Contains("Contra"))
            {
                this.dgv1.Columns[10].Visible = false;
                this.lblCramt.Visible = false;
                this.txtCrAmt.Visible = false;
            }
            else if (this.lblGeneralAcc.Text.Contains("Deposit"))
            {
                this.dgv1.Columns[9].Visible = false;
                this.lblDramt.Visible = false;
                this.txtDrAmt.Visible = false;
            }
            else
            {
                this.lblcontrolAccHead.Visible = false;
                this.ddlConAccHead.Visible = false;
                this.txtScrchConCode.Visible = false;
                this.ibtnFindConCode.Visible = false;
            }
            this.Visibility();
            this.lblPayto.Text = this.lblGeneralAcc.Text.Contains("Deposit") ? "Received From:  " : "Pay To: ";
        }



        private void Visibility()
        {
            if (this.Request.QueryString["Mod"] == "Accounts")
            {
                this.lnkPrivVou.Visible = false;
                this.ddlPrivousVou.Visible = false;
                this.ibtnFindPrv.Visible = false;
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

            string VNo1 = (this.lblGeneralAcc.Text.Contains("Journal") ? "J" : (this.lblGeneralAcc.Text.Contains("Contra") ? "C" : (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B")));
            //string VNo1 = (this.lblGeneralAcc.Text.Contains("Journal") ? "J" : (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B"));
            string VNo2 = (VNo1 == "J" ? "V" : (this.lblGeneralAcc.Text.Contains("Payment") ? "D" : (this.lblGeneralAcc.Text.Contains("Contra") ? "T" : "C")));
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
            tblt01.Columns.Add("trncram", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnrmrk", Type.GetType("System.String"));
            tblt01.Columns.Add("recndt", Type.GetType("System.String"));
            tblt01.Columns.Add("rpcode", Type.GetType("System.String"));
            tblt01.Columns.Add("billno", Type.GetType("System.String"));
            Session["tblt01"] = tblt01;
            DataTable tblt02 = new DataTable();
            tblt02.Columns.Add("actcode", Type.GetType("System.String"));
            tblt02.Columns.Add("subcode", Type.GetType("System.String"));
            tblt02.Columns.Add("spclcode", Type.GetType("System.String"));
            tblt02.Columns.Add("actdesc", Type.GetType("System.String"));
            tblt02.Columns.Add("subdesc", Type.GetType("System.String"));
            tblt02.Columns.Add("spcldesc", Type.GetType("System.String"));
            tblt02.Columns.Add("trnqty", Type.GetType("System.Double"));
            tblt02.Columns.Add("trnrate", Type.GetType("System.Double"));
            tblt02.Columns.Add("trndram", Type.GetType("System.Double"));
            tblt02.Columns.Add("trncram", Type.GetType("System.Double"));
            tblt02.Columns.Add("trnrmrk", Type.GetType("System.String"));
            tblt02.Columns.Add("recndt", Type.GetType("System.String"));
            tblt02.Columns.Add("rpcode", Type.GetType("System.String"));
            tblt02.Columns.Add("billno", Type.GetType("System.String"));
            Session["tblt02"] = tblt02;
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
        private void GetBillNo()
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = this.ddlacccode.SelectedValue.ToString();
                string supcode = this.ddlresuorcecode.SelectedValue.ToString();
                string ttsrch = "%" + this.txtScrchConCode.Text.Trim() + "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETBILLNO", pactcode, supcode, ttsrch, "", "", "", "", "", "");
                this.ddlBillList.DataSource = ds1.Tables[0];
                this.ddlBillList.DataTextField = "textfield";
                this.ddlBillList.DataValueField = "valfield";
                this.ddlBillList.DataBind();
                //this.GetPriviousVoucher();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }

        }
        protected void lnkAcccode_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string filter = this.txtserceacc.Text + "%";
            string accconhead = this.ddlConAccHead.SelectedValue.ToString();
            string vtname = this.Request.QueryString["tname"].ToString().Trim();
            string vounum = ((vtname == "Contra Voucher") ? "CT" : (vtname == "Journal Voucher" ? "JV" : ""));
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETACCCODE", filter, accconhead, vounum, "", "", "", "", "", "");
            DataTable dt2 = ds2.Tables[0];
            ViewState["HeadAcc1"] = ds2.Tables[0];
            this.ddlacccode.DataSource = dt2;
            this.ddlacccode.DataTextField = "actdesc1";
            this.ddlacccode.DataValueField = "actcode";
            this.ddlacccode.DataBind();
            //----Show Resource code and Specification Code------------// 

            DataTable dt01 = (DataTable)ViewState["HeadAcc1"];
            string search1 = this.ddlacccode.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;


            if (dr1[0]["actelev"].ToString() == "2")
            {
                this.txtserchReCode.Visible = true;
                this.lnkRescode.Visible = true;
                this.ddlresuorcecode.Visible = true;
                this.txtSearchSpeci.Visible = true;
                this.lnkSpecification.Visible = true;
                this.ddlSpclinf.Visible = true;
                this.lblqty.Visible = true;
                this.txtqty.Visible = true;
                this.lblrate.Visible = true;
                this.txtrate.Visible = true;
                this.txtqty.Text = "";
                this.txtrate.Text = "";
                string actcode = this.ddlacccode.SelectedValue.ToString().Substring(0, 2);
                // if (actcode == "18" || actcode == "24" || actcode == "25" || actcode == "25" || actcode == "18" || this.ddlresuorcecode.Items.Count==0)
                this.GetResCode();
            }
            else
            {
                this.txtserchReCode.Visible = false;
                this.lnkRescode.Visible = false;
                this.ddlresuorcecode.Visible = false;
                this.txtSearchSpeci.Visible = false;
                this.lnkSpecification.Visible = false;
                this.ddlSpclinf.Visible = false;
                this.lblqty.Visible = false;
                this.txtqty.Visible = false;
                this.lblrate.Visible = false;
                this.txtrate.Visible = false;
                this.txtqty.Text = "";
                this.txtrate.Text = "";
                this.ddlSpclinf.Items.Clear();
                this.ddlresuorcecode.Items.Clear();


            }
            //---------------------------------------------//
            this.txtserceacc.Text = "";
        }
        protected void lnkRescode_Click(object sender, EventArgs e)
        {

            this.GetResCode();


        }


        private void GetResCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string actcode = this.ddlacccode.SelectedValue.ToString();
            string filter1 = "%" + this.txtserchReCode.Text.Trim() + "%";
            DataSet ds3 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETRESCODE", actcode, filter1, "", "", "", "", "", "", "");
            DataTable dt3 = ds3.Tables[0];
            Session["HeadRsc1"] = ds3.Tables[0];
            this.ddlresuorcecode.DataSource = dt3;
            this.ddlresuorcecode.DataTextField = "resdesc1";
            this.ddlresuorcecode.DataValueField = "rescode";
            this.ddlresuorcecode.DataBind();
            this.txtserchReCode.Text = "";

            string seaRes = this.ddlresuorcecode.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt3.Select("rescode='" + seaRes + "'");
            if (dr1.Length == 0)
                return;

            if (ASTUtility.Left(dr1[0]["rescode"].ToString(), 1) == "9")
            {
                this.txtserchBill.Visible = true;
                this.lnkBillNo.Visible = true;
                this.ddlBillList.Visible = true;


            }
            else
            {
                this.txtserchBill.Visible = false;
                this.lnkBillNo.Visible = false;
                this.ddlBillList.Visible = false;
                this.ddlBillList.Items.Clear();

            }


        }
        protected void lnkSpecification_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rescode = this.ddlresuorcecode.SelectedValue.ToString().Trim();
            string filter2 = "%" + this.txtSearchSpeci.Text + "%";
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETSPCILINFCODE", filter2, rescode, "", "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            this.ddlSpclinf.DataSource = dt4;
            this.ddlSpclinf.DataTextField = "spdesc1";
            this.ddlSpclinf.DataValueField = "spcod";
            this.ddlSpclinf.DataBind();
            this.txtSearchSpeci.Text = "";
        }
        protected void Calculate_Rate()
        {
            double Qty1 = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtqty.Text.Trim()));
            if (Qty1 == 0)
                return;

            double DrAmt2 = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtDrAmt.Text.Trim()));
            double CrAmt2 = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtCrAmt.Text.Trim()));
            this.txtrate.Text = ((DrAmt2 + CrAmt2) / Qty1).ToString("#,##0.00;(#,##0.00); ");
        }

        protected void ddlresuorcecode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlresuorcecode.BackColor = System.Drawing.Color.Pink;
            DataTable dt02 = (DataTable)Session["HeadRsc1"];
            string seaRes = this.ddlresuorcecode.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt02.Select("rescode='" + seaRes + "'");
            if (dr1.Length == 0)
                return;

            if (ASTUtility.Left(dr1[0]["rescode"].ToString(), 1) == "9")
            {
                this.txtserchBill.Visible = true;
                this.lnkBillNo.Visible = true;
                this.ddlBillList.Visible = true;

            }
            else
            {
                this.txtserchBill.Visible = false;
                this.lnkBillNo.Visible = false;
                this.ddlBillList.Visible = false;
                this.ddlBillList.Items.Clear();

            }
            this.GetBillNo();
        }
        protected void ddlSpclinf_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlSpclinf.BackColor = System.Drawing.Color.Pink;
        }
        protected void lnkOk_Click(object sender, EventArgs e)
        {
            if (this.lnkOk.Text == "Ok")
            {
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

                    if (vounum.Substring(0, 2).ToString() != "JV")
                    {
                        this.txtScrchConCode.Text = "";
                        this.txtScrchConCode.Visible = true;
                        this.ibtnFindConCode.Visible = true;
                        this.LoadAcccombo();
                        this.ddlConAccHead.SelectedValue = dtedit.Rows[0]["cactcode"].ToString();
                    }
                    this.txtEntryDate.Text = Convert.ToDateTime(dtedit.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                    this.lblisunum.Text = dtedit.Rows[0]["isunum"].ToString();
                    this.txtRefNum.Text = dtedit.Rows[0]["refnum"].ToString();
                    this.txtSrinfo.Text = dtedit.Rows[0]["srinfo"].ToString();
                    this.txtPayto.Text = dtedit.Rows[0]["payto"].ToString();
                    this.txtNarration.Text = dtedit.Rows[0]["venar"].ToString();
                    this.txtEntryDate.Enabled = false;
                    //-------------------------------------------------//
                    this.Panel4.Visible = true;
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
                    string VNo1 = (this.lblGeneralAcc.Text.Contains("Journal") ? "J" : (this.lblGeneralAcc.Text.Contains("Contra") ? "C" :
                        (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B")));
                    string VNo2 = (VNo1 == "J" ? "V" : (this.lblGeneralAcc.Text.Contains("Payment") ? "D" : (this.lblGeneralAcc.Text.Contains("Contra") ? "T" : "C")));
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

                if (this.lblGeneralAcc.Text.Contains("Payment") || this.lblGeneralAcc.Text.Contains("Contra") || this.lblGeneralAcc.Text.Contains("Deposit"))
                {
                    this.txtScrchConCode.Visible = true;
                    //this.ibtnFindConCode.Visible = false;
                    this.chkPrint.Checked = false;
                    this.chkPrint.Visible = false;
                }
                this.chkPrint.Visible = this.lblGeneralAcc.Text.Contains("Payment") ? true : this.lblGeneralAcc.Text.Contains("Contra") ? true
                        : this.lblGeneralAcc.Text.Contains("Deposit") ? true : false;
                this.Panel2.Visible = true;
                this.lnkFinalUpdate.Enabled = true;
                this.lnkOk.Text = "New Entry";
                this.txtserceacc.Focus();
            }
            else
            {
                this.lnkOk.Text = "Ok";
                this.txtCurrntlast6.Enabled = false;

                this.Panel2.Visible = false;
                this.Panel4.Visible = false;
                this.txtserchReCode.Visible = false;
                this.lnkRescode.Visible = false;
                this.ddlresuorcecode.Visible = false;
                this.txtSearchSpeci.Visible = false;
                this.lnkSpecification.Visible = false;
                this.ddlSpclinf.Visible = false;
                this.lblqty.Visible = false;
                this.lblrate.Visible = false;
                this.txtqty.Visible = false;
                this.txtrate.Visible = false;
                this.txtserchBill.Visible = false;
                this.lnkBillNo.Visible = false;
                this.ddlBillList.Visible = false;
                dgv1.DataSource = null;
                dgv1.DataBind();

                if (this.lblGeneralAcc.Text.Contains("Payment") || this.lblGeneralAcc.Text.Contains("Contra") || this.lblGeneralAcc.Text.Contains("Deposit"))
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
                this.ddlacccode.BackColor = System.Drawing.Color.White;
                this.ddlresuorcecode.BackColor = System.Drawing.Color.White;
                this.ddlSpclinf.BackColor = System.Drawing.Color.White;
                this.txtScrchConCode.Focus();
                this.Refrsh();

            }
        }
        private void Refrsh()
        {
            this.lblprint.Text = "";
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";
            this.ddlPrivousVou.Items.Clear();
            this.ddlacccode.Items.Clear();
            this.ddlSpclinf.Items.Clear();
            this.ddlresuorcecode.Items.Clear();
            this.ddlBillList.Items.Clear();
            this.txtserceacc.Text = "";
            this.txtserchReCode.Text = "";
            this.txtSearchSpeci.Text = "";
            this.txtDrAmt.Text = "";
            this.txtCrAmt.Text = "";
            this.txtqty.Text = "";
            this.txtrate.Text = "";
            this.txtremarks.Text = "";
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

        protected void lnkOk0_Click(object sender, EventArgs e)
        {
            this.Calculate_Rate();
            try
            {
                //----------------Add Data Into Grid--------------------------//
                this.Panel4.Visible = true;
                string AccCode = this.ddlacccode.SelectedValue.ToString();
                string ResCode = this.ddlresuorcecode.SelectedValue.ToString();
                string Billno = this.ddlBillList.Items.Count > 0 ? this.ddlBillList.SelectedValue.ToString() : "";
                ResCode = (ResCode.Length < 12 ? "000000000000" : ResCode);

                string actlev = (((DataTable)ViewState["HeadAcc1"]).Select("actcode='" + AccCode + "'"))[0]["actelev"].ToString();
                if (actlev == "2")
                {
                    if (ResCode == "000000000000")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Select Details Head');", true);
                        this.txtserchReCode.Focus();
                        return;

                    }

                }

                string billno = this.ddlBillList.SelectedValue.ToString();

                string SpclCode = this.ddlSpclinf.SelectedValue.ToString();
                SpclCode = (SpclCode.Length < 12 ? "000000000000" : SpclCode);
                string AccDesc = this.ddlacccode.SelectedItem.Text.Trim();
                string ResDesc = (ResCode == "000000000000" ? "" : this.ddlresuorcecode.SelectedItem.Text.Trim());
                string SpclDesc = (SpclCode == "000000000000" ? "" : this.ddlSpclinf.SelectedItem.Text.Trim().Substring(13));
                double TrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtqty.Text.Trim()));
                double Trnrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtrate.Text.Trim()));
                double TrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtDrAmt.Text.Trim()));
                double TrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtCrAmt.Text.Trim()));
                string TrnRemarks = this.txtremarks.Text.Trim();
                DataTable tblt01 = (DataTable)Session["tblt01"];
                DataTable tblt02 = (DataTable)Session["tblt02"];
                DataTable tblt03 = new DataTable();
                tblt01.Rows.Clear();
                tblt02.Rows.Clear();
                tblt03.Rows.Clear();

                for (int i = 0; i < this.dgv1.Rows.Count; i++)
                {
                    string dgAccCode = ((Label)this.dgv1.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string dgResCode = ((Label)this.dgv1.Rows[i].FindControl("lblResCod")).Text.Trim();
                    string dgBillno = ((Label)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text.Trim();


                    //-----------If Repetation ---------------------------------------------------------//
                    if (dgAccCode + dgResCode + dgBillno == AccCode + ResCode + Billno)
                    {
                        ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text = SpclCode;
                        ((Label)this.dgv1.Rows[i].FindControl("lblSpcldesc")).Text = SpclDesc;
                        ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text = TrnQty.ToString("#,##0.00;(#,##0.00); ");
                        ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text = Trnrate.ToString("#,##0.00;(#,##0.00); ");
                        ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text = TrnDrAmt.ToString("#,##0.00;(#,##0.00); ");
                        ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text = TrnCrAmt.ToString("#,##0.00;(#,##0.00); ");
                        ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRemarks")).Text = TrnRemarks;
                        ((Label)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text = billno;


                        //this.CalculatrGridTotal();
                        //return;
                    }
                    else
                    {
                        //--------------------------------------------------------------------------------//
                        string dgSpclCode = ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                        string dgAccDesc = ((Label)this.dgv1.Rows[i].FindControl("lblAccdesc")).Text.Trim();
                        string dgResDesc = ((Label)this.dgv1.Rows[i].FindControl("lblResdesc")).Text.Trim();
                        string dgSpclDesc = ((Label)this.dgv1.Rows[i].FindControl("lblSpcldesc")).Text.Trim();
                        double dgTrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()));
                        double dgTrnrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text.Trim()));
                        double dgTrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
                        double dgTrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text.Trim()));
                        string dgTrnRemarks = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                        string recndt = ((Label)this.dgv1.Rows[i].FindControl("lblrecndat")).Text;
                        string rpcode = ((Label)this.dgv1.Rows[i].FindControl("lblgvrpcode")).Text;
                        string billno1 = ((Label)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text;
                        //actcode subcode actdesc subdesc trnqty trnrate trndram trncram trnrmrk 
                        DataRow dr1 = tblt01.NewRow();
                        dr1["actcode"] = dgAccCode;
                        dr1["subcode"] = dgResCode;
                        dr1["spclcode"] = dgSpclCode;
                        dr1["actdesc"] = dgAccDesc;
                        dr1["subdesc"] = dgResDesc;
                        dr1["spcldesc"] = dgSpclDesc;
                        dr1["trnqty"] = dgTrnQty;
                        dr1["trnrate"] = dgTrnrate;
                        dr1["trndram"] = dgTrnDrAmt;
                        dr1["trncram"] = dgTrnCrAmt;
                        dr1["trnrmrk"] = dgTrnRemarks;
                        dr1["recndt"] = recndt;
                        dr1["rpcode"] = rpcode;
                        dr1["billno"] = billno1;
                        tblt01.Rows.Add(dr1);
                    }
                }
                DataRow dr2 = tblt01.NewRow();
                dr2["actcode"] = AccCode;
                dr2["subcode"] = ResCode;
                dr2["spclcode"] = SpclCode;
                dr2["actdesc"] = AccDesc;
                dr2["subdesc"] = ResDesc;
                dr2["spcldesc"] = SpclDesc;
                dr2["trnqty"] = TrnQty;
                dr2["trnrate"] = Trnrate;
                dr2["trndram"] = TrnDrAmt;
                dr2["trncram"] = TrnCrAmt;
                dr2["trnrmrk"] = TrnRemarks;
                dr2["recndt"] = "";
                dr2["rpcode"] = "";
                dr2["billno"] = billno;
                tblt01.Rows.Add(dr2);
                //--------------** Remove Duplicate Value **----------------------------//
                //--** Only Actdesc remove actcod not remove from grid **---------------// 
                DataView dv1 = tblt01.DefaultView;
                dv1.Sort = "actcode";
                tblt03 = dv1.ToTable();
                string AccDesc1 = null;
                for (int j = 0; j < tblt03.Rows.Count; j++)
                {
                    DataRow dr3 = tblt02.NewRow();
                    dr3["actcode"] = tblt03.Rows[j]["actcode"].ToString();
                    dr3["subcode"] = tblt03.Rows[j]["subcode"].ToString();
                    dr3["spclcode"] = tblt03.Rows[j]["spclcode"].ToString();
                    string tserch = tblt03.Rows[j]["actcode"].ToString();
                    if (tserch == AccDesc1 || tserch == "")
                    {
                        dr3["actdesc"] = "";
                    }
                    else
                    {
                        dr3["actdesc"] = tblt03.Rows[j]["actdesc"].ToString();
                        AccDesc1 = tblt03.Rows[j]["actcode"].ToString();
                    }

                    dr3["subdesc"] = tblt03.Rows[j]["subdesc"].ToString();
                    dr3["spcldesc"] = tblt03.Rows[j]["spcldesc"].ToString();
                    dr3["trnqty"] = Convert.ToDouble(tblt03.Rows[j]["trnqty"].ToString());
                    dr3["trnrate"] = Convert.ToDouble(tblt03.Rows[j]["trnrate"].ToString());
                    dr3["trndram"] = Convert.ToDouble(tblt03.Rows[j]["trndram"].ToString());
                    dr3["trncram"] = Convert.ToDouble(tblt03.Rows[j]["trncram"].ToString());
                    dr3["trnrmrk"] = tblt03.Rows[j]["trnrmrk"].ToString();
                    dr3["recndt"] = tblt03.Rows[j]["recndt"].ToString();
                    dr3["billno"] = tblt03.Rows[j]["billno"].ToString();
                    tblt02.Rows.Add(dr3);

                }
                //---------------------------------------------//
                dgv1.DataSource = tblt02;


                //dgv1.DataSource = tblt01;
                dgv1.DataBind();
                this.CalculatrGridTotal();
                //this.ddlacccode.BackColor = System.Drawing.Color.Beige;
                //this.ddlresuorcecode.BackColor = System.Drawing.Color.Beige;
                //this.ddlSpclinf.BackColor = System.Drawing.Color.Beige;
                this.txtDrAmt.Text = "";
                this.txtCrAmt.Text = "";
                this.txtqty.Text = "";
                this.txtrate.Text = "";
                this.txtremarks.Text = "";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error :" + ex.Message;
            }

        }
        protected void CalculatrGridTotal()
        {
            double TQty = 0.00;
            double TRate = 0.00;
            double TDrAmt = 0.00;
            double TCrAmt = 0.00;
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                double dg1TrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()));
                double dg1TrnRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text.Trim()));
                double dg1TrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
                double dg1TrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text.Trim()));
                TQty += dg1TrnQty;
                TRate += dg1TrnRate;
                TDrAmt += dg1TrnDrAmt;
                TCrAmt += dg1TrnCrAmt;
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text = dg1TrnQty.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text = dg1TrnRate.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text = dg1TrnDrAmt.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text = dg1TrnCrAmt.ToString("#,##0.00;(#,##0.00); ");
            }
            if (this.dgv1.Rows.Count > 0)
            {
                ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvQty")).Text = TQty.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvRate")).Text = TRate.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvDrAmt")).Text = TDrAmt.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvCrAmt")).Text = TCrAmt.ToString("#,##0.00;(#,##0.00); ");
            }
        }

        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            this.CalculatrGridTotal();
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
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            // ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}



            this.CalculatrGridTotal();

            //if (this.txtcurrentvou.Text.Trim() != "")
            //{

            double ToDramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvDrAmt")).Text.Trim());
            double ToCramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvCrAmt")).Text.Trim());

            if (ToDramt == 0 && ToCramt == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Amount is not Available";
                return;
            }


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

            //string voudat = this.txtEntryDate.Text.Substring(0, 11);





            //    if ((this.Request.QueryString["Mod"] == "Accounts"))
            //    {
            //        DateTime Bdate = this.GetBackDate();
            //        bool dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(voudat));
            //        if (!dcon)
            //        {
            //            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Issue Date is equal or less Current Date');", true);
            //            return;
            //        }
            //        this.GetVouCherNumber();
            //    }
            //    else
            //    {

            //        if ((this.Request.QueryString["Mod"] == "Management"))
            //        {

            //            if (this.txtCurrntlast6.Enabled)
            //                this.GetVouCherNumber();
            //        };


            // }
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
            try
            {
                //-----------Update Transaction B Table-----------------//
                bool resultb = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo, vounarration1,
                                vounarration2, voutype, vtcode, edit, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, Payto, isunum, "", "", "", "");
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
                    double Dramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim());
                    double Cramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text.Trim());
                    string trnamt = Convert.ToString(Dramt - Cramt);
                    string trnremarks = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                    string recndt = ((Label)this.dgv1.Rows[i].FindControl("lblrecndat")).Text.Trim();
                    string rpcode = ((Label)this.dgv1.Rows[i].FindControl("lblgvrpcode")).Text.Trim();
                    string billno = ((Label)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text.Trim();

                    bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, actcode, rescode, cactcode,
                                   voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, recndt, rpcode, billno, "", "");
                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        return;
                    }
                }



             ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                this.lnkFinalUpdate.Enabled = false;
                string eventdesc = "Voucher: " + this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim() + " Dated: " + this.txtEntryDate.Text.Trim();
                string eventdesc2 = this.txtNarration.Text.Trim();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), voutype, eventdesc, eventdesc2);


            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
            //}
            //else
            //{
            // ((Label)this.Master.FindControl("lblmsg")).Text = "Please Get Vocher No.";
            //}
        }

        protected DateTime GetBackDate()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string entrydate = this.txtEntryDate.Text;
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
        protected void ddlacccode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlacccode.BackColor = System.Drawing.Color.Pink;
            DataTable dt01 = (DataTable)ViewState["HeadAcc1"];
            string search1 = this.ddlacccode.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");

            if (dr1[0]["actelev"].ToString() == "2")
            {
                this.txtserchReCode.Visible = true;
                this.lnkRescode.Visible = true;
                this.ddlresuorcecode.Visible = true;
                this.txtSearchSpeci.Visible = true;
                this.lnkSpecification.Visible = true;
                this.ddlSpclinf.Visible = true;
                this.lblqty.Visible = true;
                this.txtqty.Visible = true;
                this.lblrate.Visible = true;
                this.txtrate.Visible = true;
                this.txtqty.Text = "";
                this.txtrate.Text = "";
                string actcode = this.ddlacccode.SelectedValue.Substring(0, 2);
                if (actcode == "18" || actcode == "24" || actcode == "25" || actcode == "25" || actcode == "18")

                    this.GetResCode();
            }
            else
            {
                this.txtserchReCode.Visible = false;
                this.lnkRescode.Visible = false;
                this.ddlresuorcecode.Visible = false;
                this.txtSearchSpeci.Visible = false;
                this.lnkSpecification.Visible = false;
                this.ddlSpclinf.Visible = false;
                this.lblqty.Visible = false;
                this.txtqty.Visible = false;
                this.lblrate.Visible = false;
                this.txtrate.Visible = false;
                this.ddlSpclinf.Items.Clear();
                this.ddlresuorcecode.Items.Clear();
                this.txtqty.Text = "";
                this.txtrate.Text = "";
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
                string VNo1 = (this.lblGeneralAcc.Text.Contains("Journal") ? "J" : (this.lblGeneralAcc.Text.Contains("Contra") ? "C" :
                    (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B")));
                string VNo2 = (VNo1 == "J" ? "V" : (this.lblGeneralAcc.Text.Contains("Payment") ? "D" : (this.lblGeneralAcc.Text.Contains("Contra") ? "T" : "C")));
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
                else if (Chequeprint == "PrintCheque03")
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheeque03();
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
                    string eventtype = this.lblGeneralAcc.Text;
                    string eventdesc = "Print Cheque";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
                rptinfo.SetDataSource(dt);
                Session["Report1"] = rptinfo;
                this.lblprint.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
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
        private void PrintVoucher()
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
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string curvoudat = this.txtEntryDate.Text.Substring(0, 11);
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                        this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
                string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

                //string vounum = this.ddlPrivousVou.SelectedValue.ToString();
                DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");
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

                ReportDocument rptinfo = new ReportDocument();

                if (Type == "VocherPrint")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher", list, null, null);
                    Rpt1.EnableExternalImages = true;

                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();

                }
                else if (Type == "VocherPrint1")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher1", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtisunum", "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6))));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher1();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum1.Text = "Issue No: " +(this.lblisunum.Text.Trim()==""?this.lblisunum.Text.Trim():ASTUtility.Right(this.lblisunum.Text.Trim(),6));
                    //TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate1.Text = "Entry Date: " + Posteddat;

                }
                else if (Type == "VocherPrint2")
                {


                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher2", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtisunum", "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6))));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher2();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum2.Text = "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6));
                    //TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate2.Text = "Entry Date: " + Posteddat;

                }
                else if (Type == "VocherPrint3")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher3", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtisunum", "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6))));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher3();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum1.Text = "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6));
                    //TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate1.Text = "Entry Date: " + Posteddat;

                }

                else
                {


                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher4", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtisunum", "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6))));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher4();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum3 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum3.Text = "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6));
                    //TextObject txtPosteddate3 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate3.Text = "Entry Date: " + Posteddat;

                }



                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                Rpt1.SetParameters(new ReportParameter("txtPartyName", (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim()));
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
                //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //vounum1.Text = "Voucher No.: " + vounum;
                //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //date.Text = "Voucher Date: " + voudat;
                //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //rpttxtPartyName.Text =(this.txtPayto.Text.Trim()=="")?"": this.lblPayto.Text.Trim()+" "+ this.txtPayto.Text.Trim();
                //TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
                //voutype1.Text = voutype;
                //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //naration.Text = "Narration: " + venar;

                ////TextObject txtBname = rptinfo.ReportDefinition.ReportObjects["bankname"] as TextObject;
                ////txtBname.Text = this.ddlConAccHead.SelectedItem.Text.Substring(13);
                //TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                //rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);

                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                //if (ConstantInfo.LogStatus == true)
                //{
                //    string eventtype = this.lblGeneralAcc.Text;
                //    string eventdesc = "Print Voucher";
                //    string eventdesc2 = vounum;
                //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                //}
                ////string comcod = this.GetComeCode();
                ////string comcod = hst["comcod"].ToString();
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rptinfo.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rptinfo;
                //this.lblprint.Text = "<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //            this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }

        protected void ibtnFindConCode_Click(object sender, ImageClickEventArgs e)
        {
            this.LoadAcccombo();
        }
        protected void ibtnFindPrv_Click(object sender, ImageClickEventArgs e)
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


