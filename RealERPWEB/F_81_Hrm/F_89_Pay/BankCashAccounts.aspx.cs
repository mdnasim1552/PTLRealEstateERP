using System;
using System.Collections;
using System.Collections.Generic;
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
using RealEntity;
using Microsoft.Reporting.WinForms;
using RealERPWEB.Service;
//using RealERPRPT;
namespace RealERPWEB.F_81_Hrm.F_89_Pay
{
    public partial class BankCashAccounts : System.Web.UI.Page
    {
        public static double TAmount;

        ProcessAccess accData = new ProcessAccess();
        UserService userSer = new UserService();
        AutoCompleted AutoData = new AutoCompleted();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.lnkFinalUpdate.Enabled = (Convert.ToBoolean(dr1[0]["entry"]));

                lnkFinalUpdate.Attributes.Add("onClick", " javascript:return confirm('You sure you want to Save the record?');");
                Session.Remove("tblvoucher");
                this.txtEntryDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.TableCreate();
                this.GetRecAndPayto();
                this.CompanyPost();
                this.GetMonth();
            }

            if (this.ddlConAccHead.Items.Count > 0)
                return;


            this.LoadAcccombo();
            this.GetAccHead();



            ((Label)this.Master.FindControl("lblTitle")).Text = Request.QueryString["tname"].ToString();
            string vcode = Request.QueryString["tcode"];


            if (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment") || ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra"))
            {
                this.dgv1.Columns[11].Visible = false;

            }
            else if (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Deposit"))
            {
                this.dgv1.Columns[10].Visible = false;

            }
            else
            {
                this.lblcontrolAccHead.Visible = false;
                this.ddlConAccHead.Visible = false;
                this.txtScrchConCode.Visible = false;
                this.ibtnFindConCode.Visible = false;
            }
            this.Visibility();
            this.lblPayto.Text = ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Deposit") ? "Received From:  " : "Pay To: ";
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void CompanyPost()
        {


            string comcod = this.GetCompCode();

            switch (comcod)
            {

                case "3332":
                case "3101":
                    this.chkpost.Checked = true;
                    break;


                default:
                    this.chkpost.Checked = false;
                    break;
            }


        }

        public void GetRecAndPayto()
        {
            Session.Remove("tblrecandPayto");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            AutoData.GetRecAndPayto(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETPAYRECCOD", "", "", "", "", "", "", "", "", "");

        }
        private void GetMonth()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETYEARMON", "", "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlMonth.DataTextField = "yearmon";
            this.ddlMonth.DataValueField = "ymon";
            this.ddlMonth.DataSource = ds1.Tables[0];
            this.ddlMonth.DataBind();
            this.ddlMonth.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            ds1.Dispose();
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
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
        //private string CompanyPrintVou()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string vouprint = "";
        //    switch (comcod)
        //    {

        //        case "2305":
        //            vouprint = "VocherPrint4";
        //            break;

        //        case "3306":
        //        case "3307":
        //        case "3308":
        //            vouprint = "VocherPrint1";
        //            break;
        //        case "3305":
        //        case "3310":
        //        case "3311":
        //            vouprint = "VocherPrint2";
        //            break;
        //        case "3309":
        //            vouprint = "VocherPrint3";
        //            break;
        //        default:
        //            vouprint = "VocherPrint";
        //            break;
        //    }
        //    return vouprint;
        //}
        private void GetPriviousVoucher()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string ConAccHead = this.ddlConAccHead.SelectedValue.ToString();
            string vtcode = this.Request.QueryString["tcode"].ToString().Trim();
            string date = this.txtEntryDate.Text.Substring(0, 11);


            string VNo1 = (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Journal") ? "J" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") ? "C" : (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B")));
            //string VNo1 = (this.lblGeneralAcc.Text.Contains("Journal") ? "J" : (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B"));
            string VNo2 = (VNo1 == "J" ? "V" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment") ? "D" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") ? "T" : "C")));
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
            Session["tblvoucher"] = tblt01;

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
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

        private void GetAccHead()
        {
            string accconhead = this.ddlConAccHead.SelectedValue.ToString();
            string vtname = this.Request.QueryString["tname"].ToString().Trim();
            string vounum = ((vtname == "Contra Voucher") ? "CT" : (vtname == "Journal Voucher" ? "JV" : ""));

            List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead> lst = new List<RealEntity.C_17_Acc.EClassAccVoucher.EClassAccHead>();
            lst = userSer.GetActHead("%", accconhead, vounum);

            Session["HeadAcc1"] = lst;
        }

















        protected void lnkOk_Click(object sender, EventArgs e)
        {


            if (this.lnkOk.Text == "Ok")
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                // string newvoumum = "NEW";
                string vounum = this.ddlPrivousVou.SelectedValue.ToString();
                DataSet _NewDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "EDITVOUCHER", vounum, "", "", "", "", "", "", "", "");
                Session["UserLog"] = _NewDataSet.Tables[1];
                Session.Remove("tblvoucher");
                if (this.ddlPrivousVou.Items.Count > 0)
                {
                    //DataSet _NewDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "EDITVOUCHER", newvoumum, "", "", "", "", "", "", "", "");
                    //Session["UserLog"] = _NewDataSet.Tables[1];

                    DataTable dtuser = (DataTable)Session["UserLog"];
                    string aprovbyid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvbyid"].ToString();
                    string CallType = (this.chkpost.Checked && aprovbyid.Length > 0) ? "EDITVOUCHER" : (this.chkpost.Checked) ? "EDITUNVOUCHER" : "EDITVOUCHER";
                    vounum = this.ddlPrivousVou.SelectedValue.ToString();
                    DataSet _EditDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", CallType, vounum, "", "", "", "", "", "", "", "");
                    DataTable dt = this.HiddenSameData(_EditDataSet.Tables[0]);
                    this.dgv1.DataSource = dt;
                    Session["tblvoucher"] = dt;
                    if (dt.Rows.Count == 0)
                        return;

                    this.dgv1.DataBind();

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
                    this.pnlNarration.Visible = true;
                    this.lblcurVounum.Text = "Edit Voucher No.";
                    string cvno1 = this.ddlPrivousVou.SelectedValue.ToString().Substring(0, 8);
                    this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
                    this.txtCurrntlast6.Text = this.ddlPrivousVou.SelectedValue.ToString().Substring(8);
                    this.txtCurrntlast6.Enabled = false;
                    this.CalculatrGridTotal();
                }
                else
                {

                    this.txtEntryDate.Enabled = true;
                    this.txtCurrntlast6.Enabled = true;


                    // Previous Nerration
                    double vcode1 = Convert.ToDouble(Request.QueryString["tcode"]);
                    string ConAccHead = this.ddlConAccHead.SelectedValue.ToString();
                    string VNo1 = (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Journal") ? "J" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") ? "C" :
                        (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B")));
                    string VNo2 = (VNo1 == "J" ? "V" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment") ? "D" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") ? "T" : "C")));
                    string VNo3 = Convert.ToString(VNo1 + VNo2);
                    DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "LASTNARRATION", VNo3, "", "", "", "", "", "", "", "");
                    if (ds4.Tables[0].Rows.Count == 0)
                        this.txtNarration.Text = "";
                    else
                        this.txtNarration.Text = ds4.Tables[0].Rows[0]["vernar"].ToString();
                    //---------------------

                    this.GetVouCherNumber();

                    if (VNo3 == "BD" || VNo3 == "CT")
                    {
                        this.ChequeNo();
                    }

                }
                this.lnkPrivVou.Visible = false;
                this.ddlPrivousVou.Visible = false;
                this.ibtnFindPrv.Visible = false;
                this.txtScrchPre.Visible = false;

                if (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment") || ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") || ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Deposit"))
                {
                    this.txtScrchConCode.Visible = true;
                    //this.ibtnFindConCode.Visible = false;
                    this.chkPrint.Checked = false;
                    this.chkPrint.Visible = false;
                }
                this.chkPrint.Visible = ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment") ? true : ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") ? true
                        : ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Deposit") ? true : false;
                this.Panel2.Visible = true;
                this.lnkFinalUpdate.Enabled = true;
                this.ddlConAccHead.Enabled = false;
                this.lnkOk.Text = "New";

            }
            else
            {
                this.lnkOk.Text = "Ok";
                this.txtCurrntlast6.Enabled = false;
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.Panel2.Visible = false;

                this.pnlNarration.Visible = false;
                dgv1.DataSource = null;
                dgv1.DataBind();
                this.ddlConAccHead.Enabled = true;
                this.lblInword.Text = "";

                if (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment") || ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") || ((Label)this.Master.FindControl("lblTitle")).Text.Contains("Deposit"))
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
                this.lblcurVounum.Text = "Voucher No.";
                this.txtcurrentvou.Text = "";
                this.txtCurrntlast6.Text = "";
                this.txtEntryDate.Enabled = true;
                this.txtRefNum.Text = "";
                this.ddlcheque.Items.Clear();

                this.txtScrchConCode.Focus();
                this.Refrsh();

            }
        }
        private void ChequeNo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = this.ddlConAccHead.SelectedValue.ToString();
            string flag = "";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "TOPCHEQUE", txtSProject, flag, "", "", "", "", "", "", "");
            this.ddlcheque.DataTextField = "chequeno";
            this.ddlcheque.DataValueField = "chequeno";
            this.ddlcheque.DataSource = ds1.Tables[0];
            Session["tblchequeno"] = ds1.Tables[0];
            this.ddlcheque.DataBind();
            this.ddlcheque_SelectedIndexChanged(null, null);

        }
        private void Refrsh()
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Text = "";
            //this.lblmsg.Visible = false;

            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";
            this.ddlPrivousVou.Items.Clear();
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


        }
        protected void CalculatrGridTotal()
        {
            double TQty = 0.00;
            double TRate = 0.00;
            double TDrAmt = 0.00;
            double TCrAmt = 0.00;
            DataTable dt = (DataTable)Session["tblvoucher"];

            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                double dg1TrnQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()));
                double dg1TrnRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text.Trim()));
                double dg1TrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
                double dg1TrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text.Trim()));
                TQty = dg1TrnQty;
                //TRate += dg1TrnRate;
                TDrAmt += dg1TrnDrAmt;
                TCrAmt += dg1TrnCrAmt;
                dg1TrnCrAmt = dg1TrnDrAmt > 0 ? 0.00 : dg1TrnCrAmt;
                TRate = (TQty == 0) ? 0 : Math.Abs(dg1TrnDrAmt - dg1TrnCrAmt) / TQty;
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text = dg1TrnQty.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRate")).Text = TRate.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text = dg1TrnDrAmt.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text = dg1TrnCrAmt.ToString("#,##0.00;(#,##0.00); ");

                dt.Rows[i]["trnqty"] = dg1TrnQty;
                dt.Rows[i]["trnrate"] = TRate;
                dt.Rows[i]["trndram"] = dg1TrnDrAmt;
                dt.Rows[i]["trncram"] = dg1TrnCrAmt;

            }

            Session["tblvoucher"] = dt;
            this.Data_Bind();

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
                case "9101":
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


            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            this.CalculatrGridTotal();

            //if (this.txtcurrentvou.Text.Trim() != "")
            //{

            double ToDramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvDrAmt")).Text.Trim());
            double ToCramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvCrAmt")).Text.Trim());

            if (ToDramt == 0 && ToCramt == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Amount is not Available";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            //Log Entry

            DataTable dtuser = (DataTable)Session["UserLog"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string userdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


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
            string EditTermId = Terminal;
            string pounaction = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["pounaction"].ToString();
            string aprovbyid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvbyid"].ToString();
            string aprvtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvtrmid"].ToString();
            string aprvseson = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvseson"].ToString();
            string aprvdat = (dtuser.Rows.Count == 0) ? "01-jan-1900" : dtuser.Rows[0]["aprvdat"].ToString();



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

            if ((this.Request.QueryString["Mod"] == "Accounts"))
            {
                if ((ASTUtility.Left(vounum, 2) == "BD"))
                {

                    if (refnum == "")
                        ;
                    else
                    {
                        DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "CHEQUENOCHECK", refnum, "", "", "", "", "", "", "", "");
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "This Cheque no is already exist.";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;

                        }

                    }




                }
            }

            string cactcode = (vouno == "JV" ? "000000000000" : this.ddlConAccHead.SelectedValue.ToString());


            string edit = (this.txtCurrntlast6.Enabled ? "" : "EDIT");
            string TgvDrAmt = ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvDrAmt")).Text;
            string TgvCrAmt = ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvCrAmt")).Text;
            if (vouno == "JV" && TgvDrAmt != TgvCrAmt)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Dr. Amount not equals to Cr. Amount.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            try
            {
                //string pounaction = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["pounaction"].ToString();
                //string aprovbyid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvbyid"].ToString();
                //string aprvtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvtrmid"].ToString();
                //string aprvseson = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvseson"].ToString();
                //string aprvdat = (dtuser.Rows.Count == 0) ? "01-jan-1900" : dtuser.Rows[0]["aprvdat"].ToString(); 


                string CallType = (this.chkpost.Checked && aprovbyid.Length > 0) ? "ACVUPDATE02" : (this.chkpost.Checked) ? "ACVUPDATEUNPOSTED" : "ACVUPDATE02";

                //-----------Update Transaction B Table-----------------//
                bool resultb = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, voudat, refnum, srinfo, vounarration1,
                                vounarration2, voutype, vtcode, edit, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, Payto, isunum, aprovbyid, aprvtrmid, aprvseson, aprvdat, pounaction, "", "", "", "");
                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                //-----------//Update Transaction A Table//
                string dtrnrmrks = ((Label)this.dgv1.Rows[0].FindControl("lblgvRemarks")).Text.Trim();
                for (int i = 0; i < dgv1.Rows.Count; i++)
                {
                    string actcode = ((Label)this.dgv1.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string rescode = ((Label)this.dgv1.Rows[i].FindControl("lblResCod")).Text.Trim();
                    string spclcode = ((Label)this.dgv1.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string trnqty = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()).ToString();
                    double Dramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim());
                    double Cramt = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text.Trim());
                    string trnamt = Convert.ToString(Dramt - Cramt);
                    string trnremarks = ((Label)this.dgv1.Rows[i].FindControl("lblgvRemarks")).Text.Trim();
                    trnremarks = (vounum.Substring(0, 2) == "JV" && dtrnrmrks.Contains("PBL")) ? dtrnrmrks : trnremarks;
                    string recndt = (this.Request.QueryString["Mod"] == "Accounts") ? ((cactcode.Substring(0, 4) == "1901") ? voudat : ((Label)this.dgv1.Rows[i].FindControl("lblrecndat")).Text.Trim()) : ((Label)this.dgv1.Rows[i].FindControl("lblrecndat")).Text.Trim();
                    string rpcode = ((Label)this.dgv1.Rows[i].FindControl("lblgvrpcode")).Text.Trim();
                    string billno = ((Label)this.dgv1.Rows[i].FindControl("lblgvBillno")).Text.Trim();
                    bool resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, actcode, rescode, cactcode,
                                   voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, recndt, rpcode, billno, userid, userdate, Terminal, "", "", "", "", "", "", "", "", "", "");

                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                }

                if ((ASTUtility.Left(vounum, 2) == "BD") || (ASTUtility.Left(vounum, 2) == "CT"))
                {
                    bool resultd = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "UPDATECHQLIST", cactcode, refnum, vounum, "", "",
                                   "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                }


             ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                this.lnkFinalUpdate.Enabled = false;


                if (ConstantInfo.LogStatus == true)
                {

                    string eventdesc = "Voucher: " + this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim() + " Dated: " + this.txtEntryDate.Text.Trim();
                    string eventdesc2 = this.txtNarration.Text.Trim();
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), voutype, eventdesc, eventdesc2);

                }


                //if (comcod == "3101" || comcod == "3316" || comcod == "3317")
                //{
                //    if (vouno == "JV")
                //        this.lnkPrint_Click(null, null);
                //}

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETBDATE", "", "", "", "", "", "", "", "", "");
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



                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate >= Convert.ToDateTime(this.txtEntryDate.Text.Trim().Substring(0, 11)))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;



                }

                double vcode1 = Convert.ToDouble(Request.QueryString["tcode"]);
                string ConAccHead = this.ddlConAccHead.SelectedValue.ToString();
                string VNo1 = (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Journal") ? "J" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") ? "C" :
                    (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B")));
                string VNo2 = (VNo1 == "J" ? "V" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment") ? "D" : (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Contra") ? "T" : "C")));
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

                case "3315":
                case "3316":
                case "3317":
                    vouprint = "VocherPrint5";
                    break;

                case "3330":
                    vouprint = "VocherPrint6";
                    break;

                case "3101":
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


                rptinfo.SetDataSource(dt);
                Session["Report1"] = rptinfo;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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
                case "3101":
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
                //string usrname = hst["usrname"].ToString();
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

                string PrintInstar = this.GetCompInstar();




                string CallType = (this.chkpost.Checked && aprovbyid.Length > 0) ? "PRINTVOUCHER01" : (this.chkpost.Checked) ? "PRINTUNPOSTEDVOUCHER01" : "PRINTVOUCHER01";



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

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim()));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));



                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();
                    //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                    //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                    //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                    //rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
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
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6))));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim()));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher1();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum1.Text = "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6));
                    //TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate1.Text = "Entry Date: " + Posteddat;
                    //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                    //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                    //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                    //rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
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
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6))));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim()));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher2();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum2.Text = "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6));
                    //TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate2.Text = "Entry Date: " + Posteddat;
                    //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                    //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                    //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                    //rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
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
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6))));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim()));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher3();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum1.Text = "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6));
                    //TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate1.Text = "Entry Date: " + Posteddat;
                    //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                    //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                    //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                    //rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
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
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6))));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim()));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher5();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum1.Text = "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6));
                    //TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate1.Text = "Entry Date: " + Posteddat;
                    //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                    //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                    //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                    //rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
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
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim()));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucherBridge();
                    //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                    //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                    //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                    //rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                    //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                    //naration.Text = "Narration: " + venar;
                    //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                    //vounum1.Text = "Voucher No.: " + vounum;
                    //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                    //date.Text = "Voucher Date: " + voudat;


                }

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
                //        txtDesc.Text = this.ddlConAccHead.SelectedItem.Text.Substring(13).ToString();
                //        TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //        rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.txtPayto.Text.Trim();
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
                //        Refnum.Text =refnum;

                //    }

                //}



                else
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher4", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6))));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim()));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher4();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum3 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum3.Text = "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6));
                    //TextObject txtPosteddate3 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate3.Text = "Entry Date: " + Posteddat;
                    //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                    //rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
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
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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


        protected void dgv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkAccdesc1 = (HyperLink)e.Row.FindControl("hlnkAccdesc1");



                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string subcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "subcode")).ToString();
                string subdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "subdesc")).ToString();


                if (subcode != "000000000000")
                {
                    hlnkAccdesc1.NavigateUrl = "LinkAccSpLedger.aspx?Type=DetailLedger&Date1=" + Convert.ToDateTime(this.txtEntryDate.Text).AddDays(-90).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txtEntryDate.Text).ToString("dd-MMM-yyyy") + "&sircode=" + subcode + "&sirdesc=" + subdesc;



                }
                else
                {
                    hlnkAccdesc1.NavigateUrl = "LinkAccLedger.aspx?Type=Ledger&RType=GLedger&Date1=" + Convert.ToDateTime(this.txtEntryDate.Text).AddDays(-90).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txtEntryDate.Text).ToString("dd-MMM-yyyy") + "&sircode=" + subcode + "&actcode=" + actcode;



                }

            }
        }
        protected void ddlcheque_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.ddlcheque.Items.Count == 0)
                return;
            this.txtRefNum.Text = this.ddlcheque.SelectedItem.Text;
        }
        protected void dgv1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {




            // DataTable dt = (DataTable)Session["tblvoucher"];
            // DataTable dtuser = (DataTable)Session["UserLog"];
            // Hashtable hst = (Hashtable)Session["tblLogin"];
            // string comcod = hst["comcod"].ToString();
            // string userid = hst["usrid"].ToString();
            // string Terminal = hst["compname"].ToString();
            // string Sessionid = hst["session"].ToString();
            // string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");        
            //string aprovbyid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvbyid"].ToString();

            // string voudat = ASTUtility.DateFormat(this.txtEntryDate.Text);
            // string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
            //                this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            // string actcode = ((Label)this.dgv1.Rows[e.RowIndex].FindControl("lblAccCod")).Text.Trim();
            // string rescode = ((Label)this.dgv1.Rows[e.RowIndex].FindControl("lblResCod")).Text.Trim();
            // string spclcode = ((Label)this.dgv1.Rows[e.RowIndex].FindControl("lblSpclCod")).Text.Trim();
            // string billno = ((Label)this.dgv1.Rows[e.RowIndex].FindControl("lblgvBillno")).Text.Trim();
            // string CallType = (this.chkpost.Checked && aprovbyid.Length > 0) ? "DELETEVOUITEM" : (this.chkpost.Checked) ? "DELETEVOUUNITEM" : "DELETEVOUITEM";

            // bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, actcode, rescode, spclcode, billno, userid, Terminal, Posteddat, "", "", "", "", "", "");

            // if (!result)
            // {
            //  ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
            //     return;

            // }
            // int rowindex = (this.dgv1.PageSize) * (this.dgv1.PageIndex) + e.RowIndex;
            // dt.Rows[rowindex].Delete();


            // DataView dv = dt.DefaultView;
            // Session.Remove("tblvoucher");
            // Session["tblvoucher"] = dv.ToTable();
            // this.lblmsg.Text = "Deleted Successfully";
            // dgv1.DataSource = dv.ToTable();
            // dgv1.DataBind();
            // this.CalculatrGridTotal();






        }
        protected void llnkAdd_Click(object sender, EventArgs e)
        {
            this.pnlNarration.Visible = true;
            string comcod = this.GetCompCode();
            string monthid = this.ddlMonth.SelectedValue.ToString();
            string cactcode = this.ddlConAccHead.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETSALARY", monthid,
                          cactcode, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];

            if (Session["tblvoucher"] == null)
            {
                this.TableCreate();
            }
            DataTable tblt01 = (DataTable)Session["tblvoucher"];

            foreach (DataRow dr in dt1.Rows)
            {
                string dgAccCode = dr["actcode"].ToString();
                string dgResCode = dr["subcode"].ToString();
                string dgSpclCode = dr["spclcode"].ToString();
                string dgAccDesc = dr["actdesc"].ToString();
                string dgResDesc = dr["subdesc"].ToString();
                string dgSpclDesc = dr["spcldesc"].ToString();
                double dgTrnQty = Convert.ToDouble(dr["trnqty"]);
                double dgTrnrate = Convert.ToDouble(dr["trnrate"]);
                double dgTrnDrAmt = Convert.ToDouble(dr["trndram"]);
                double dgTrnCrAmt = Convert.ToDouble(dr["trncram"]);
                string dgTrnRemarks = "";


                DataRow[] dr2 = tblt01.Select("actcode='" + dgAccCode + "'  and subcode='" + dgResCode + "' and spclcode='" + dgSpclCode + "'");
                if (dr2.Length > 0)
                {

                    return;

                }

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
                dr1["billno"] = dgTrnRemarks;

                tblt01.Rows.Add(dr1);
            }
            Session["tblvoucher"] = HiddenSameData(tblt01);
            this.Data_Bind();
        }
        protected void Data_Bind()
        {

            DataTable tbl1 = (DataTable)Session["tblvoucher"];
            this.dgv1.DataSource = tbl1;
            this.dgv1.DataBind();
            calculation();


        }
        private void calculation()
        {
            DataTable dt2 = (DataTable)Session["tblvoucher"];
            if (dt2.Rows.Count == 0)
                return;
            double dramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trndram)", "")) ?
                         0.00 : dt2.Compute("Sum(trndram)", ""))), 2);
            double cramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trncram)", "")) ?
                        0.00 : dt2.Compute("Sum(trncram)", ""))), 2);


            ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvDrAmt")).Text = dramt.ToString("#,##0.00;(#,##0.00); ");
            ((TextBox)this.dgv1.FooterRow.FindControl("txtTgvCrAmt")).Text = cramt.ToString("#,##0.00;(#,##0.00); ");
            string vouchertype = this.txtcurrentvou.Text.Substring(0, 2);
            this.lblInword.Text = (vouchertype == "JV") ? "" : dramt > 0 ? ASTUtility.Trans(dramt, 2) : cramt > 0 ? ASTUtility.Trans(cramt, 2) : "";


        }
    }
}


