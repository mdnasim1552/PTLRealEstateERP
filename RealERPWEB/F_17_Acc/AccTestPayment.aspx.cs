﻿using System;
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
    public partial class AccTestPayment : System.Web.UI.Page
    {
        public static double TAmount;
        ProcessAccess accData = new ProcessAccess();
        AutoCompleted AutoData = new AutoCompleted();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                {
                    //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]))
                    //    Response.Redirect("../AcceessError.aspx");
                    //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
                    //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                    //this.lnkFinalUpdate.Enabled = (Convert.ToBoolean(dr1[0]["entry"]));
                    this.BindModalDialog();


                }
                lnkFinalUpdate.Attributes.Add("onClick",
               "javascript:return confirm('You sure you want to Save the record?');");

                this.LoadAcccombo();
                this.GetRecAndPayto();
                //this.GetBillNo();
                this.TableCreate();
                this.txtEntryDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                this.txtChequeDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                this.Visibility();
                this.TxtnameChange();

            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void Visibility()
        {
            if (this.Request.QueryString["Type"] == "Acc")
            {
                this.dgv1.Columns[1].Visible = false;
                this.dgv1.Columns[2].Visible = false;
                this.lnkPrivVou.Visible = false;
                this.txtScrchPre.Visible = false;
                this.ibtnFindPrv.Visible = false;
                this.ddlPrivousVou.Visible = false;
            }

            else
            {
                // this.ibtnFindPrv_Click(null, null);
                this.dgv1.Columns[1].Visible = true;
                this.dgv1.Columns[2].Visible = true;
            }
        }
        private void TxtnameChange()
        {
            this.lblPayto.Text = this.Request.QueryString["tname"].ToString().Contains("Payment") ? "Pay To: " : "Received From:";
            this.dgv1.Columns[11].HeaderText = this.Request.QueryString["tname"].ToString().Contains("Payment") ? "Pay To " : "Received From";

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

        public void GetRecAndPayto()
        {
            Session.Remove("tblrecandPayto");
            string comcod = this.GetCompCode();
            AutoData.GetRecAndPayto(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETPAYRECCOD", "", "", "", "", "", "", "", "", "");

        }
        private void GetPriviousVoucher()
        {

            string comcod = this.GetCompCode();
            string date = ASTUtility.DateFormat(this.txtEntryDate.Text);
            string VNo = (this.Request.QueryString["tname"].ToString().Contains("Payment") ? "PV" : "DV");
            string vounum = "%" + this.txtScrchPre.Text + "%";
            DataSet ds5 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETPRIVOUSVOUCHER", VNo, "99", date, vounum, "", "", "", "", "");
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
            tblt01.Columns.Add("trnrmrk", Type.GetType("System.String"));
            tblt01.Columns.Add("payto", Type.GetType("System.String"));
            tblt01.Columns.Add("acvounum", Type.GetType("System.String"));
            tblt01.Columns.Add("billno", Type.GetType("System.String"));
            tblt01.Columns.Add("insofissue", Type.GetType("System.String"));
            Session["tblt01"] = tblt01;

            //actcode,subcode,spclcode,actdesc,subdesc,spcldesc,trnqty,trnrate,trndram,trncram,trnrmrk
        }
        private void LoadAcccombo()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string ttsrch = "%" + this.txtScrchConCode.Text.Trim() + "%";
                string UserId = hst["usrid"].ToString();
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETCONACCHEAD", ttsrch, UserId, "", "", "", "", "", "", "");
                DataTable dt1 = ds1.Tables[0];
                this.ddlConAccHead.DataSource = dt1;
                this.ddlConAccHead.DataTextField = "actdesc1";
                this.ddlConAccHead.DataValueField = "actcode";
                this.ddlConAccHead.DataBind();
                this.ddlConAccHead.Focus();
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

                string comcod = this.GetCompCode();
                string pactcode = this.ddlacccode.SelectedValue.ToString();
                string supcode = this.ddlresuorcecode.SelectedValue.ToString();
                string ttsrch = "%" + this.txtserchBill.Text.Trim() + "%";
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
        //private void LoadPayRec()
        //{
        //    try
        //    {

        //        string comcod = this.GetCompCode();
        //        string ttsrch = this.txtScrchConCode.Text.Trim() + "%";
        //        DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETPAYRECCOD", ttsrch, "", "", "", "", "", "", "", "");
        //        DataTable dt1 = ds1.Tables[0];
        //        this.ddlPayTo.DataSource = dt1;
        //        this.ddlPayTo.DataTextField = "prdesc";
        //        this.ddlPayTo.DataValueField = "prcode";
        //        this.ddlPayTo.DataBind();
        //        //this.GetPriviousVoucher();
        //    }
        //    catch (Exception ex)
        //    {
        //     ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
        //    }

        //}
        protected void lnkAcccode_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string filter = "%" + this.txtserceacc.Text + "%";
            string accconhead = this.ddlConAccHead.SelectedValue.ToString();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETACCCODE", filter, accconhead, "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
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
                this.GetResource();

            }
            else
            {
                this.txtserchReCode.Visible = false;
                this.lnkRescode.Visible = false;
                this.ddlresuorcecode.Visible = false;
                this.ddlresuorcecode.Items.Clear();

            }
            //---------------------------------------------//
            this.txtserceacc.Text = "";
            this.ddlacccode.Focus();
        }

        private void GetResource()
        {
            string comcod = this.GetCompCode();
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
            this.ddlresuorcecode.Focus();

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





        protected void lnkRescode_Click(object sender, EventArgs e)
        {
            this.GetResource();



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

        protected void lnkOk_Click(object sender, EventArgs e)
        {
            if (this.lnkOk.Text == "Ok")
            {

                string comcod = this.GetCompCode();

                DataSet _NewDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "EDITVOUCHER", "NEW", "", "", "", "", "", "", "", "");
                Session["UserLog"] = _NewDataSet.Tables[1];
                if (this.ddlPrivousVou.Items.Count > 0)
                {

                    string vounum = this.ddlPrivousVou.SelectedValue.ToString();
                    DataSet _EditDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "EDITVOUCHER", vounum, "", "", "", "", "", "", "", "");
                    DataTable dt = this.HiddenSameData(_EditDataSet.Tables[0]);

                    if (dt.Rows.Count == 0)
                        return;
                    Session["tblt01"] = dt;
                    Session["UserLog"] = _EditDataSet.Tables[1];
                    this.Data_Bind();

                    ////-------------** Edit **---------------------------//
                    DataTable dtedit = _EditDataSet.Tables[1];

                    if (vounum.Substring(0, 2).ToString() != "JV")
                        this.ddlConAccHead.SelectedValue = dtedit.Rows[0]["cactcode"].ToString();

                    this.txtEntryDate.Text = Convert.ToDateTime(dtedit.Rows[0]["voudat"]).ToString("dd.MM.yyyy");
                    this.txtNarration.Text = dtedit.Rows[0]["venar"].ToString();
                    //this.ddlConAccHead.Enabled = false;
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
                    //  this.ddlConAccHead.Enabled = true;
                    this.txtEntryDate.Enabled = true;
                    this.txtCurrntlast6.Enabled = true;
                    string VNo = (this.Request.QueryString["tname"].ToString().Contains("Payment") ? "PV" : "DV");
                    DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "LASTNARRATION", VNo, "", "", "", "", "", "", "", "");
                    if (ds4.Tables[0].Rows.Count == 0)
                    {
                        this.txtChequeNo.Text = "";
                        this.txtNarration.Text = "";


                    }
                    else
                    {
                        this.txtChequeNo.Text = ds4.Tables[0].Rows[0]["chequeno"].ToString();
                        this.txtDrAmt.Text = Convert.ToDouble(ds4.Tables[0].Rows[0]["trnam"]).ToString("#,##0;(#,##0); ");
                        this.txtNarration.Text = ds4.Tables[0].Rows[0]["vernar"].ToString();
                    }
                }

                if (this.Request.QueryString["Type"] == "Mgt")
                {
                    this.lnkPrivVou.Visible = false;
                    this.txtScrchPre.Visible = false;
                    this.ibtnFindPrv.Visible = false;
                    this.ddlPrivousVou.Visible = false;

                }

                this.Panel2.Visible = true;
                this.PanelChk.Visible = true;
                this.lnkFinalUpdate.Enabled = true;
                this.lnkOk.Text = "New Entry";
                this.txtserceacc.Focus();


                //this.ddlConAccHead.BackColor = System.Drawing.Color.Pink;
                //this.txtEntryDate.BackColor = System.Drawing.Color.Aqua;
            }
            else
            {
                Session.Remove("tblt01");
                this.TableCreate();
                this.lnkOk.Text = "Ok";
                this.txtCurrntlast6.Enabled = false;
                this.Panel2.Visible = false;
                this.PanelChk.Visible = false;
                this.Panel4.Visible = false;
                //this.txtserchReCode.Visible = false;
                //this.lnkRescode.Visible = false;
                //this.ddlresuorcecode.Visible = false;
                dgv1.DataSource = null;
                dgv1.DataBind();
                // this.ddlPrivousVou.Items.Clear(); 


                if (this.Request.QueryString["Type"] == "Mgt")
                {

                    this.lnkPrivVou.Visible = true;
                    this.txtScrchPre.Visible = true;
                    this.ibtnFindPrv.Visible = true;
                    this.ddlPrivousVou.Visible = true;
                    this.ddlPrivousVou.Items.Clear();
                }


                if (this.lblGeneralAcc.Text.Contains("Payment"))
                {
                    this.txtScrchConCode.Visible = true;
                    this.ibtnFindConCode.Visible = true;
                }


                this.lblcurVounum.Text = "Current Voucher No.";
                this.txtcurrentvou.Text = "";
                this.txtCurrntlast6.Text = "";
                //this.txtCurrntlast6.Enabled = true;
                //this.lnkPrivVou.Visible = true;
                //this.ddlPrivousVou.Visible = true;
                this.ddlConAccHead.Enabled = true;
                this.txtEntryDate.Enabled = true;
                this.txtScrchConCode.Focus();

                this.Refrsh();

            }
        }
        private void Refrsh()
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";
            //  this.ddlacccode.Items.Clear();
            // this.ddlresuorcecode.Items.Clear();
            // this.ddlBillList.Items.Clear();
            this.txtserceacc.Text = "";
            this.txtserchReCode.Text = "";
            this.txtremarks.Text = "";
            this.txtNarration.Text = "";
            this.txtChequeNo.Text = "";
            this.chkPrint.Checked = false;
            this.ddlChqList.Items.Clear();


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

        protected void lbtnAdd_Click(object sender, EventArgs e)

        {
            //Session.Remove("tblt01");

            try
            {
                //----------------Add Data Into Grid--------------------------//

                this.SaveValue();

                if (this.txtChequeNo.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Fill Reference Number');", true);
                    this.txtChequeNo.Focus();
                    return;
                }
                else if (Convert.ToDouble("0" + this.txtDrAmt.Text.Trim()) == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Amount will not be empty');", true);
                    this.txtDrAmt.Focus();
                    return;
                }
             ((Label)this.Master.FindControl("lblmsg")).Text = "";
                string comcod = this.GetCompCode();
                string isunum = "";
                this.Panel4.Visible = true;
                string AccCode = this.ddlacccode.SelectedValue.ToString();
                string ResCode = this.ddlresuorcecode.SelectedValue.ToString();
                string Chequeno = this.txtChequeNo.Text.Trim();
                ResCode = (ResCode.Length < 12 ? "000000000000" : ResCode);
                string AccDesc = this.ddlacccode.SelectedItem.Text.Trim();
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

                string ResDesc = (ResCode == "000000000000" ? "" : this.ddlresuorcecode.SelectedItem.Text.Trim());
                string Chequedate = this.txtChequeDate.Text;
                string billno = this.ddlBillList.SelectedValue.ToString();
                //Duplicate Chequeno

                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "CHKDUPCHEQUENO", Chequeno, "", "", "", "", "", "", "", "");

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds2.Tables[0];
                    string vounoum = "";
                    for (int i = 0; i < dt1.Rows.Count; i++)
                        vounoum = vounoum + dt1.Rows[i]["vounum"].ToString() + ",";

                    vounoum = vounoum.Substring(0, ((vounoum.Length) - 1));
                    ((Label)this.Master.FindControl("lblmsg")).Text = "This Chequeno exist against Voucher # " + vounoum;
                }



                double TrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtDrAmt.Text.Trim()));
                string TrnRemarks = this.txtremarks.Text.Trim();
                DataTable dt = (DataTable)Session["tblt01"];




                DataRow[] dr2 = dt.Select("actcode = '" + AccCode + "' and subcode='" + ResCode + "' and chequeno='" + Chequeno + "'");
                if (dr2.Length > 0)
                {
                    return;

                }

                if ((this.Request.QueryString["Type"] == "Acc"))
                {
                    isunum = (dt.Rows.Count == 0) ? this.GetIssueNo() : (this.chkPettyCash.Checked) ? this.GetIssueNo() : this.IncrmentIssueNo();
                    this.lblissueno.Text = isunum;
                }

                DataRow dr1 = dt.NewRow();
                dr1["actcode"] = AccCode;
                dr1["subcode"] = ResCode;
                dr1["chequeno"] = Chequeno;
                dr1["isunum"] = isunum;
                dr1["actdesc"] = AccDesc;
                dr1["subdesc"] = ResDesc;
                dr1["chequedate"] = ASTUtility.DateFormat(Chequedate);
                dr1["trndram"] = TrnDrAmt;
                dr1["trnrmrk"] = TrnRemarks;
                dr1["payto"] = this.txtRecAndPayto.Text.Trim();
                dr1["billno"] = billno;
                dr1["insofissue"] = "";
                dt.Rows.Add(dr1);
                Session["tblt01"] = HiddenSameData(dt);
                this.Data_Bind();
                this.txtDrAmt.Text = "";
                // this.txtChequeNo.Text = "";
                this.txtremarks.Text = "";
                //   this.txtDrAmt.Focus();
                this.txtNarration.Focus();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error :" + ex.Message;
            }

        }

        //private void CheeckRefNumber()
        //{
        //    string Vounum = ASTUtility.Left(this.txtcurrentvou.Text.Trim(), 2);
        //    switch (Vounum)
        //    {
        //        case "BD":
        //        case "BC":
        //        case "CT":
        //            if (this.txtRefNum.Text.Trim() == "")
        //                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Fill Reference Number');", true);
        //            break;
        //    }




        //}

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

        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tblt01"];
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {

                dt.Rows[i]["chequeno"] = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvChequeno")).Text;
                dt.Rows[i]["chequedate"] = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvChequeDate")).Text;//.ToString("dd-MMM-yyyy");
                dt.Rows[i]["trndram"] = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
                dt.Rows[i]["trnrmrk"] = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvRemarks")).Text;
                dt.Rows[i]["payto"] = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvPayto")).Text;
                dt.Rows[i]["billno"] = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvBillno")).Text;
                dt.Rows[i]["insofissue"] = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvinsissueno")).Text;

            }
            Session["tblt01"] = dt;

        }




        private void Data_Bind()
        {
            dgv1.DataSource = (DataTable)Session["tblt01"];
            dgv1.DataBind();
            this.FooterAmount();
        }





        private void FooterAmount()
        {
            DataTable dt = (DataTable)Session["tblt01"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.dgv1.FooterRow.FindControl("lblTgvDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trndram)", "")) ?
                                 0 : dt.Compute("sum(trndram)", ""))).ToString("#,##0;(#,##0); ");


        }

        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        private string Companylimit()
        {

            string comcod = this.GetCompCode();
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

            this.SaveValue();
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
            if ((this.Request.QueryString["Type"] == "Acc"))
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

                if ((this.Request.QueryString["Type"] == "Mgt"))
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
            if ((this.Request.QueryString["Type"] == "Acc"))
            {
                vounarration1 = this.GetCompanyPrefix(vounarration1);

            }
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


            //string tblApprovtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvtrmid"].ToString();
            //string tblApprovSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvseson"].ToString();

            string PostedByid = (this.Request.QueryString["Type"] == "Acc") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = (this.Request.QueryString["Type"] == "Acc") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = (this.Request.QueryString["Type"] == "Acc") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string Posteddat = (this.Request.QueryString["Type"] == "Acc") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
            string EditByid = (this.Request.QueryString["Type"] == "Acc") ? "" : userid;
            string Editdat = (this.Request.QueryString["Type"] == "Acc") ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");


            //string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblt01"];

            DataRow[] dr = dt.Select("trndram=0");
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
                if (this.Request.QueryString["Type"] == "Acc")
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
                    string Dramt = (this.txtcurrentvou.Text.Trim().Substring(0, 2) == "PV" ? Convert.ToDouble(dt.Rows[i]["trndram"]).ToString() : (Convert.ToDouble(dt.Rows[i]["trndram"]) * -1).ToString());
                    string trnremarks = dt.Rows[i]["trnrmrk"].ToString();
                    string payto = dt.Rows[i]["payto"].ToString();
                    string billno = dt.Rows[i]["billno"].ToString();
                    string insofissueno = dt.Rows[i]["insofissue"].ToString();

                    if (this.Request.QueryString["Type"] == "Mgt")
                    {
                        isunum = dt.Rows[i]["isunum"].ToString();
                        if (isunum.Length == 0)
                            isunum = this.GetIssueNo();

                    }

                    bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "INOFUPACPMNTA", vounum, actcode, rescode, chequeno, cactcode,
                                   voudat, Dramt, chequedate, trnremarks, vtcode, payto, isunum, acvounum, billno, insofissueno);
                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        return;
                    }

                    if (this.Request.QueryString["Type"] == "Acc")
                    {
                        isunum = (this.chkPettyCash.Checked) ? isunum : this.IncrmentIssueNo(); ;
                        this.lblissueno.Text = isunum;
                    }
                }



             ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                this.lnkFinalUpdate.Enabled = false;
                this.lnkPrint.Focus();
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

        private void DirectPrint()
        {

            //ReportDocument report1 = new CrystalReport1();
            //PrintDialog dialog1 = new PrintDialog();

            //report1.SetDatabaseLogon("username", "password");

            //dialog1.AllowSomePages = true;
            //dialog1.AllowPrintToFile = false;

            //if (dialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    int copies = dialog1.PrinterSettings.Copies;
            //    int fromPage = dialog1.PrinterSettings.FromPage;
            //    int toPage = dialog1.PrinterSettings.ToPage;
            //    bool collate = dialog1.PrinterSettings.Collate;

            //    report1.PrintOptions.PrinterName = dialog1.PrinterSettings.PrinterName;
            //    report1.PrintToPrinter(copies, collate, fromPage, toPage);            
            //}

            //report1.Dispose();
            //dialog1.Dispose();
        }



        protected DateTime GetBackDate()
        {

            //HttpCookie cbdateCookie = new HttpCookie("cbdatename");
            //cbdateCookie.Value = "name";
            //cbdateCookie.Expires = DateTime.Today.AddDays(1);
            //if(cbdateCookie[])

            string comcod = this.GetCompCode();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETBDATE", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return (System.DateTime.Today);
            }

            return (Convert.ToDateTime(ds2.Tables[0].Rows[0]["bdate"]));


            //if ( Request.Cookies["cookie"] != null )
            //{
            //   cookievalue = Request.Cookies["cookie"].ToString();
            //}
            //else
            //{
            //   Response.Cookies["cookie"].Value = "cookie value";
            //    Response.Cookies["cookie"].Expires = DateTime.Now.AddMinutes(1); // add expiry time
            //}





        }

        protected string GetCompanyPrefix(string Naration)
        {
            string comcod = this.GetCompCode();
            if (Naration.Trim().Length == 0)
                return Naration;
            switch (comcod)
            {
                case "3305":
                    //case "3101":
                    Naration = Naration.Contains("Amount Paid to the above") ? Naration : ("Amount Paid to the above " + Naration);
                    break;



            }

            return Naration;


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
                this.GetResource();

            }
            else
            {
                this.txtserchReCode.Visible = false;
                this.lnkRescode.Visible = false;
                this.ddlresuorcecode.Visible = false;
                this.ddlresuorcecode.Items.Clear();

            }
        }
        protected void ibtnvounu_Click(object sender, ImageClickEventArgs e)
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

            //this.ddlPrivousVou.BackColor = System.Drawing.Color.Aqua;
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
            {
                this.RptPostDatChq();
            }
            else
            {
                this.VouPrint();
            }

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




                //string VouType = this.Request.QueryString["tname"].ToString();

                //DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");
                //if (_ReportDataSet == null)
                //    return;
                //DataTable dt = _ReportDataSet.Tables[0];
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
                //else
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher4();
                //}


                ////-----------------------------
                //DataTable dt1 = _ReportDataSet.Tables[1];
                ////string Vounum = dt1.Rows[0]["vounum"].ToString();
                //string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                //string venar = dt1.Rows[0]["venar"].ToString();
                ////ReportDocument rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher();
                //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //txtCompanyName.Text = comnam;
                //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                //txtcAdd.Text = comadd;

                //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //txtComBranch.Text = (combranch.Length>0)?("Unit: "+combranch):"";

                //TextObject rpttxtVoutype = rptinfo.ReportDefinition.ReportObjects["txtVoutype"] as TextObject;
                //rpttxtVoutype.Text = VouType;
                //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //vounum1.Text = "Voucher No: " + pvnum;
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
                //this.lblprint.Text = "<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //            this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }


        private string GetCChequePrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string vouprint = "";
            switch (comcod)
            {

                case "2305":
                case "3305":
                case "3306":
                case "3307":
                case "3308":
                    vouprint = "CPrint1";
                    break;

                case "3309":
                    vouprint = "CPrint2";
                    break;
                default:
                    vouprint = "CPrint1";
                    break;

            }
            return vouprint;
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

                case "3309":
                    chequeprint = "PrintCheque04";
                    break;

                default:
                    chequeprint = "PrintCheque01";
                    break;
            }
            return chequeprint;
        }


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
                string chequedat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");
                chequedat = chequedat.Substring(0, 1) + "   " + chequedat.Substring(1, 1) + "   " + chequedat.Substring(2, 1) + "   " + chequedat.Substring(3, 1) + "   " + chequedat.Substring(4, 1) + "   " + chequedat.Substring(5, 1) + "   " + chequedat.Substring(6, 1) + "   " + chequedat.Substring(7, 1);
                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["trnam"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));

                string type = this.CompanyPrintCheque();
                ReportDocument rptinfo = new ReportDocument();
                if (type == "PrintCheque01")
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheck();

                else if (type == "PrintCheque02")
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheeque02();
                else if (type == "PrintCheque03")
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheeque03();

                else rptinfo = new RealERPRPT.R_17_Acc.PrintCheckHolding();




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
                this.lblprint.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
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
        protected void dgv1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblt01"];
            int rowindex = (this.dgv1.PageSize) * (this.dgv1.PageIndex) + e.RowIndex;
            string voudat = this.txtEntryDate.Text;
            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(6, 4) +
                             this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            string actcode = dt.Rows[rowindex]["actcode"].ToString();
            string rescode = dt.Rows[rowindex]["subcode"].ToString();
            string chequeno = dt.Rows[rowindex]["chequeno"].ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "DELETEAPMNTNA", vounum, actcode, rescode, chequeno, "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have been deleted Voucher " + accData.ErrorObject["Msg"].ToString() + " before deleted this Entry";
            }

            else
            {

                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session["tblt01"] = dv.ToTable();
            this.Data_Bind();


        }

        protected void ibtnFindPrv_Click(object sender, ImageClickEventArgs e)
        {
            this.GetPriviousVoucher();
        }
        protected void lnkBillNo_Click(object sender, EventArgs e)
        {
            this.GetBillNo();
        }

        protected void txtEntryDate_TextChanged(object sender, EventArgs e)
        {
            this.txtEntryDate.Text = ASTUtility.DateInVal(this.txtEntryDate.Text);
            this.txtScrchConCode.Focus();


        }
        protected void txtChequeDate_TextChanged(object sender, EventArgs e)
        {
            this.txtChequeDate.Text = ASTUtility.DateInVal(this.txtChequeDate.Text);
            this.txtremarks.Focus();
        }

        private void BindModalDialog()
        {



            string TString = "javascript:window.showModalDialog('RptTransactionSearch02.aspx', 'Unit Description', 'dialogHeight:1024px;dialogWidth:1024px;status:no')";
            this.lbtnSearch.Attributes.Add("OnClick", TString);

        }
        protected void dgv1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.dgv1.EditIndex = -1;
            this.Data_Bind();

        }

        protected void dgv1_RowEditing(object sender, GridViewEditEventArgs e)


        {
            this.dgv1.EditIndex = e.NewEditIndex;
            this.Data_Bind();


            string comcod = this.GetCompCode();
            int rowindex = (dgv1.PageSize) * (this.dgv1.PageIndex) + e.NewEditIndex;
            string accconhead = this.ddlConAccHead.SelectedValue.ToString();
            string actcode = ((DataTable)Session["tblt01"]).Rows[rowindex]["actcode"].ToString();
            string subcode = ((DataTable)Session["tblt01"]).Rows[rowindex]["subcode"].ToString();
            DropDownList ddlgrdacccode = (DropDownList)this.dgv1.Rows[e.NewEditIndex].FindControl("ddlgrdacccode");

            ViewState["gindex"] = e.NewEditIndex;
            string SearchProject = "%" + ((TextBox)this.dgv1.Rows[e.NewEditIndex].FindControl("txtgrdserceacc")).Text.Trim() + "%";


            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETACCCODE", SearchProject, accconhead, "", "", "", "", "", "", "");
            DataTable dt2 = ds2.Tables[0];
            ViewState["HeadAcc1"] = ds2.Tables[0];

            ddlgrdacccode.DataTextField = "actdesc1";
            ddlgrdacccode.DataValueField = "actcode";
            ddlgrdacccode.DataSource = dt2;
            ddlgrdacccode.DataBind();
            ddlgrdacccode.SelectedValue = actcode;




            //ddlgrdresouce.SelectedValue = actcode; 
            DataTable dt01 = (DataTable)ViewState["HeadAcc1"];
            string search1 = ddlgrdacccode.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;



            if (dr1[0]["actelev"].ToString() == "2")
            {


                ((TextBox)this.dgv1.Rows[e.NewEditIndex].FindControl("txtgrdserresource")).Visible = true;
                ((ImageButton)this.dgv1.Rows[e.NewEditIndex].FindControl("ibtngrdFindResource")).Visible = true;
                ((DropDownList)this.dgv1.Rows[e.NewEditIndex].FindControl("ddlrgrdesuorcecode")).Visible = true;
                DropDownList ddlgrdresouce = (DropDownList)this.dgv1.Rows[e.NewEditIndex].FindControl("ddlrgrdesuorcecode");
                string SearchResourche = "%" + ((TextBox)this.dgv1.Rows[e.NewEditIndex].FindControl("txtgrdserresource")).Text.Trim() + "%";

                DataSet ds3 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETRESCODE", actcode, SearchResourche, "", "", "", "", "", "", "");
                DataTable dt3 = ds3.Tables[0];
                Session["HeadRsc1"] = ds3.Tables[0];

                ddlgrdresouce.DataTextField = "resdesc1";
                ddlgrdresouce.DataValueField = "rescode";
                ddlgrdresouce.DataSource = dt3;
                ddlgrdresouce.DataBind();
                ddlgrdresouce.SelectedValue = subcode;
                ddlgrdresouce.Focus();






            }
            else
            {
                ((TextBox)this.dgv1.Rows[e.NewEditIndex].FindControl("txtgrdserresource")).Visible = false;
                ((ImageButton)this.dgv1.Rows[e.NewEditIndex].FindControl("ibtngrdFindResource")).Visible = false;
                ((DropDownList)this.dgv1.Rows[e.NewEditIndex].FindControl("ddlrgrdesuorcecode")).Visible = false;
                this.ddlresuorcecode.Items.Clear();

            }
            //---------------------------------------------//
            ((TextBox)this.dgv1.Rows[e.NewEditIndex].FindControl("txtgrdserceacc")).Text = "";
            ((DropDownList)this.dgv1.Rows[e.NewEditIndex].FindControl("ddlrgrdesuorcecode")).Focus();



        }


        private void GetgrdResource()
        {
            string comcod = this.GetCompCode();
            int rowindex = (int)ViewState["gindex"];
            // string actcode = ((DataTable)Session["tblt01"]).Rows[rowindex]["actcode"].ToString();
            // string subcode = ((DataTable)Session["tblt01"]).Rows[rowindex]["subcode"].ToString();
            DropDownList ddlactcode = (DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlgrdacccode");
            DropDownList ddlgrdresouce = (DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlrgrdesuorcecode");
            string SearchResourche = "%" + ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgrdserresource")).Text.Trim() + "%";
            string actcode = ddlactcode.SelectedValue.ToString();


            DataSet ds3 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETRESCODE", actcode, SearchResourche, "", "", "", "", "", "", "");
            DataTable dt3 = ds3.Tables[0];
            Session["HeadRsc1"] = ds3.Tables[0];

            ddlgrdresouce.DataTextField = "resdesc1";
            ddlgrdresouce.DataValueField = "rescode";
            ddlgrdresouce.DataSource = dt3;
            ddlgrdresouce.DataBind();
            ddlgrdresouce.Focus();
        }



        private void GetgrdAccountCode()
        {

            string comcod = this.GetCompCode();
            int rowindex = (int)ViewState["gindex"];
            string accconhead = this.ddlConAccHead.SelectedValue.ToString();
            string actcode = ((DataTable)Session["tblt01"]).Rows[rowindex]["actcode"].ToString();
            string subcode = ((DataTable)Session["tblt01"]).Rows[rowindex]["subcode"].ToString();
            DropDownList ddlgrdacccode = (DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlgrdacccode");
            string SearchProject = "%" + ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgrdserceacc")).Text.Trim() + "%";


            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETACCCODE", SearchProject, accconhead, "", "", "", "", "", "", "");
            DataTable dt2 = ds2.Tables[0];
            ViewState["HeadAcc1"] = ds2.Tables[0];

            ddlgrdacccode.DataTextField = "actdesc1";
            ddlgrdacccode.DataValueField = "actcode";
            ddlgrdacccode.DataSource = dt2;
            ddlgrdacccode.DataBind();
            DataTable dt01 = (DataTable)ViewState["HeadAcc1"];
            string search1 = ddlgrdacccode.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;

            if (dr1[0]["actelev"].ToString() == "2")
            {


                ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgrdserresource")).Visible = true;
                ((ImageButton)this.dgv1.Rows[rowindex].FindControl("ibtngrdFindResource")).Visible = true;
                ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).Visible = true;
                this.GetgrdResource();

            }
            else
            {
                ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgrdserresource")).Visible = false;
                ((ImageButton)this.dgv1.Rows[rowindex].FindControl("ibtngrdFindResource")).Visible = false;
                ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).Visible = false;
                this.ddlresuorcecode.Items.Clear();

            }
            //---------------------------------------------//
            ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgrdserceacc")).Text = "";
            ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).Focus();


        }


        protected void dgv1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblt01"];
            int rowindex = (int)ViewState["gindex"];
            string isunum = ((DataTable)Session["tblt01"]).Rows[rowindex]["isunum"].ToString();
            string actcode = ((DataTable)Session["tblt01"]).Rows[rowindex]["actcode"].ToString();
            string subcode = ((DataTable)Session["tblt01"]).Rows[rowindex]["subcode"].ToString();
            string chequeno = ((DataTable)Session["tblt01"]).Rows[rowindex]["chequeno"].ToString();
            DataRow[] dr2 = dt.Select("actcode = '" + actcode + "' and subcode='" + subcode + "' and chequeno='" + chequeno + "'  and isunum='" + isunum + "'");
            string ResCode = "";
            if (dr2.Length > 0)
            {
                dr2[0]["actcode"] = ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlgrdacccode")).SelectedValue.ToString();
                ResCode = ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).SelectedValue.ToString();
                ResCode = (ResCode.Length < 12 ? "000000000000" : ResCode);
                dr2[0]["subcode"] = ResCode;
                dr2[0]["chequeno"] = ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvChequeno")).Text;
                dr2[0]["isunum"] = isunum;
                dr2[0]["actdesc"] = ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlgrdacccode")).SelectedItem.Text;
                dr2[0]["subdesc"] = ResCode == "000000000000" ? "" : ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).SelectedItem.Text;

                dr2[0]["chequedate"] = ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvChequeDate")).Text;
                dr2[0]["trndram"] = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvDrAmt")).Text.Trim())); ;
                dr2[0]["trnrmrk"] = ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvRemarks")).Text;
                dr2[0]["payto"] = ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvPayto")).Text;
                dr2[0]["billno"] = ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvBillno")).Text;
                dr2[0]["insofissue"] = ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvinsissueno")).Text;

            }






            string voudat = ASTUtility.DateFormat(this.txtEntryDate.Text);
            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(6, 4) +
                               this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            string vtcode = Request.QueryString["tcode"];
            string cactcode = this.ddlConAccHead.SelectedValue.ToString();

            string actcode1 = ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlgrdacccode")).SelectedValue.ToString();
            string subcode1 = ResCode;
            string cheqno1 = ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvChequeno")).Text;
            string chequedate1 = ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvChequeDate")).Text;
            string dramt1 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvDrAmt")).Text.Trim())).ToString();
            string rmrks1 = ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvRemarks")).Text;
            string payto1 = ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvPayto")).Text;
            string billno1 = ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvBillno")).Text; ;
            string insofissueno1 = ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgvinsissueno")).Text;
            string acvounum = dr2[0]["acvounum"].ToString() == "" ? "00000000000000" : dr2[0]["acvounum"].ToString();


            bool resulta = false;


            resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "UPDATEPAYMENTA", vounum, actcode1, subcode1, cheqno1, cactcode,
                                    voudat, dramt1, chequedate1, rmrks1, vtcode, payto1, isunum, acvounum, billno1, insofissueno1);


            if (!resulta)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                return;
            }


         ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";



            this.dgv1.EditIndex = -1;
            Session["tblt01"] = HiddenSameData(dt);
            this.Data_Bind();

        }



        protected void ddlgrdacccode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowindex = (int)ViewState["gindex"];
            DataTable dt01 = (DataTable)ViewState["HeadAcc1"];
            string search1 = ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlgrdacccode")).Text;
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;

            if (dr1[0]["actelev"].ToString() == "2")
            {


                ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgrdserresource")).Visible = true;
                ((ImageButton)this.dgv1.Rows[rowindex].FindControl("ibtngrdFindResource")).Visible = true;
                ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).Visible = true;
                this.GetgrdResource();

            }
            else
            {
                ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgrdserresource")).Visible = false;
                ((ImageButton)this.dgv1.Rows[rowindex].FindControl("ibtngrdFindResource")).Visible = false;
                ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).Visible = false;
                this.ddlresuorcecode.Items.Clear();

            }
            //---------------------------------------------//
            ((TextBox)this.dgv1.Rows[rowindex].FindControl("txtgrdserceacc")).Text = "";
            ((DropDownList)this.dgv1.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).Focus();


        }
        protected void ibtngrdFindAccCode_Click(object sender, ImageClickEventArgs e)
        {
            this.GetgrdAccountCode();
        }

        protected void ibtngrdFindResource_Click(object sender, ImageClickEventArgs e)
        {

            this.GetgrdResource();
        }

    }
}


