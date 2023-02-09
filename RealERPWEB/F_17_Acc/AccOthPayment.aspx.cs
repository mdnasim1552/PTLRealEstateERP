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
    public partial class AccOthPayment : System.Web.UI.Page
    {
        public static double TAmount;
        ProcessAccess accData = new ProcessAccess();
        AutoCompleted AutoData = new AutoCompleted();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                {
                    if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]))
                        Response.Redirect("../AcceessError.aspx");
                    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
                    ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                    //((Label)this.Master.FindControl("lblTitle")).Text = "Post Dated Cheque(Issue-02)";
                    this.lnkFinalUpdate.Enabled = (Convert.ToBoolean(dr1[0]["entry"]));

                    //this.Master.Page.Title = "Post Dated Cheque(Issue-02)";
                    ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                    this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                }
                lnkFinalUpdate.Attributes.Add("onClick",
               "javascript:return confirm('You sure you want to Save the record?');");

                this.LoadAcccombo();
                this.GetRecAndPayto();
                //this.GetBillNo();
                this.TableCreate();
                this.txtEntryDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtChequeDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.Visibility();



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
            if (this.Request.QueryString["Type"] == "Acc")
            {

                this.lnkPrivVou.Visible = false;
                this.txtScrchPre.Visible = false;
                this.ibtnFindPrv.Visible = false;
                this.ddlPrivousVou.Visible = false;
            }

            else
            {
                this.ibtnFindPrv_Click(null, null);

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
                case "3305":
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
            string date = this.txtEntryDate.Text.Substring(0, 11);
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
            ViewState["tblt01"] = tblt01;

            //actcode,subcode,spclcode,actdesc,subdesc,spcldesc,trnqty,trnrate,trndram,trncram,trnrmrk
        }
        private void LoadAcccombo()
        {
            try
            {

                string comcod = this.GetCompCode();
                string ttsrch = "%" + this.txtScrchConCode.Text.Trim() + "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETCONACCHEAD", ttsrch, "", "", "", "", "", "", "", "");
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
                    ViewState["tblt01"] = dt;
                    Session["UserLog"] = _EditDataSet.Tables[1];
                    this.Data_Bind();

                    ////-------------** Edit **---------------------------//
                    DataTable dtedit = _EditDataSet.Tables[1];

                    if (vounum.Substring(0, 2).ToString() != "JV")
                        this.ddlConAccHead.SelectedValue = dtedit.Rows[0]["cactcode"].ToString();

                    this.txtEntryDate.Text = Convert.ToDateTime(dtedit.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
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
                        this.txtNarration.Text = "";
                    else
                        this.txtNarration.Text = ds4.Tables[0].Rows[0]["vernar"].ToString();
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
                this.lnkOk.Text = "New";


                //this.ddlConAccHead.BackColor = System.Drawing.Color.Pink;
                //this.txtEntryDate.BackColor = System.Drawing.Color.Aqua;
            }
            else
            {
                ViewState.Remove("tblt01");
                this.TableCreate();
                this.lnkOk.Text = "Ok";
                this.txtCurrntlast6.Enabled = false;
                this.Panel2.Visible = false;
                this.PanelChk.Visible = false;
                this.Panel4.Visible = false;
                dgv1.DataSource = null;
                dgv1.DataBind();
                // this.ddlPrivousVou.Items.Clear(); 


                if (this.Request.QueryString["Type"] == "Mgt")
                {

                    this.lnkPrivVou.Visible = true;
                    this.txtScrchPre.Visible = true;
                    this.ibtnFindPrv.Visible = true;
                    this.ddlPrivousVou.Visible = true;
                }


                if (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Payment"))
                {
                    this.txtScrchConCode.Visible = true;
                    this.ibtnFindConCode.Visible = true;
                }


                this.lblcurVounum.Text = "Voucher No.";
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

        private void GetAdvNo()
        {
            try
            {
                ViewState.Remove("tbladv");
                string comcod = this.GetCompCode();
                string SearchAdv = "%" + this.txtserchAdvanced.Text.Trim() + "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETADVANCED", SearchAdv, "", "", "", "", "", "", "", "");
                this.ddlAdvancedList.DataSource = ds1.Tables[0];
                this.ddlAdvancedList.DataTextField = "reqno1";
                this.ddlAdvancedList.DataValueField = "valuefield";
                this.ddlAdvancedList.DataBind();
                ViewState["tbladv"] = ds1.Tables[0];
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }

        }
        protected void lnkothadvance_Click(object sender, EventArgs e)
        {

            this.GetAdvNo();
        }

        private void Refrsh()
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";
            this.ddlAdvancedList.Items.Clear();
            this.txtNarration.Text = "";



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


            try
            {
                //----------------Add Data Into Grid--------------------------//

                this.SaveValue();
                if (this.txtChequeNo.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Fill Reference Number');", true);
                    return;
                }
             ((Label)this.Master.FindControl("lblmsg")).Text = "";
                string comcod = this.GetCompCode();
                this.Panel4.Visible = true;
                string reqno = this.ddlAdvancedList.SelectedValue.ToString().Substring(0, 14);
                string AccCode = this.ddlAdvancedList.SelectedValue.ToString().Substring(14, 12);
                string ResCode = this.ddlAdvancedList.SelectedValue.ToString().Substring(26);
                string Chequeno = this.txtChequeNo.Text.Trim();
                string Chequedate = this.txtChequeDate.Text;

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


                DataTable dt = (DataTable)ViewState["tblt01"];




                DataRow[] dr2 = dt.Select("actcode = '" + AccCode + "' and subcode='" + ResCode + "' and chequeno='" + Chequeno + "'");
                if (dr2.Length > 0)
                {
                    return;

                }

                string isunum = (dt.Rows.Count == 0) ? this.GetIssueNo() : this.IncrmentIssueNo();
                this.lblissueno.Text = isunum;


                DataRow[] drlist = ((DataTable)ViewState["tbladv"]).Select("reqno='" + reqno + "' and pactcode='" + AccCode + "' and  rsircode='" + ResCode + "'");

                DataRow dr1 = dt.NewRow();
                dr1["actcode"] = AccCode;
                dr1["subcode"] = ResCode;
                dr1["chequeno"] = Chequeno;
                dr1["isunum"] = isunum;
                dr1["actdesc"] = drlist[0]["pactdesc"].ToString();
                dr1["subdesc"] = drlist[0]["rsirdesc"].ToString();
                dr1["chequedate"] = Chequedate;
                dr1["trndram"] = Convert.ToDouble(drlist[0]["appamt"]).ToString();
                dr1["trnrmrk"] = reqno;
                dr1["payto"] = this.txtRecAndPayto.Text.Trim();
                dr1["billno"] = "";

                dt.Rows.Add(dr1);
                ViewState["tblt01"] = HiddenSameData(dt);
                this.Data_Bind();

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

            DataTable dt = (DataTable)ViewState["tblt01"];
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {

                dt.Rows[i]["chequeno"] = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvChequeno")).Text;
                dt.Rows[i]["chequedate"] = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvChequeDate")).Text;//.ToString("dd-MMM-yyyy");
                dt.Rows[i]["payto"] = ((TextBox)this.dgv1.Rows[i].FindControl("txtgvPayto")).Text;
            }
            ViewState["tblt01"] = dt;

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


        }

        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
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




            string voudat = this.txtEntryDate.Text.Substring(0, 11);
            if ((this.Request.QueryString["Type"] == "Acc"))
            {
                DateTime Bdate = this.GetBackDate();
                bool dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(voudat));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Issue Date is equal or less Current Date');", true);
                    return;
                }
                this.GetVouCherNumber();
            }

            DataTable dt = (DataTable)ViewState["tblt01"];
            //Existing   Purchase No  


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string reqno = dt.Rows[i]["trnrmrk"].ToString();
                string actcode = dt.Rows[i]["actcode"].ToString();
                string rescode = dt.Rows[i]["subcode"].ToString(); ;


                DataSet ds4;
                if (i == 0)
                    ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "EXISTINGREQNO", reqno, actcode, rescode, "", "", "", "", "", "");

                else if (dt.Rows[i - 1]["trnrmrk"].ToString().Trim() == reqno && dt.Rows[i - 1]["actcode"].ToString().Trim() == actcode && dt.Rows[i - 1]["subcode"].ToString().Trim() == rescode)
                    continue;

                else
                    ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "EXISTINGREQNO", reqno, actcode, rescode, "", "", "", "", "", "");


                if (ds4.Tables[0].Rows[0]["vounum"].ToString() != "00000000000000")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher No already Existing in this Advanced No";
                    return;
                }

            }



            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
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
                    if (this.Request.QueryString["Type"] == "Mgt")
                        isunum = dt.Rows[i]["isunum"].ToString();

                    bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "INOFUPACPMNTA", vounum, actcode, rescode, chequeno, cactcode,
                                   voudat, Dramt, chequedate, trnremarks, vtcode, payto, isunum, acvounum, billno, "");
                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        return;
                    }

                    if (this.Request.QueryString["Type"] == "Acc")
                    {
                        isunum = this.IncrmentIssueNo();
                        this.lblissueno.Text = isunum;
                    }


                    resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "UPDATEREQNO",
                                    trnremarks, actcode, rescode, vounum, "", "", "", "", "", "", "", "", "", "", "");
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
                    Naration = "Amount Paid to the above " + Naration;
                    break;



            }

            return Naration;


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

                if (txtopndate > Convert.ToDateTime(this.txtEntryDate.Text.Trim().Substring(0, 11)))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    return;

                }
                string VNo = (this.Request.QueryString["tname"].ToString().Contains("Payment") ? "PV" : "DV"); ;
                string entrydate = this.txtEntryDate.Text.Substring(0, 11).Trim();
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

                if (txtopndate > Convert.ToDateTime(this.txtEntryDate.Text.Trim().Substring(0, 11)))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    return;

                }
                string VNo = (this.Request.QueryString["tname"].ToString().Contains("Payment") ? "PV" : "DV"); ;
                string entrydate = this.txtEntryDate.Text.Substring(0, 11).Trim();
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
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string curvoudat = this.txtEntryDate.Text.Substring(0, 11);
                string pvnum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + this.txtcurrentvou.Text.Trim().Substring(2, 2) + "-" + this.txtCurrntlast6.Text.Trim();
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
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
                //else
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher3();
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
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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
                ReportDocument rptinfo = new RealERPRPT.R_17_Acc.PrintCheck();
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
            }

        }

        protected void ibtnFindConCode_Click(object sender, EventArgs e)
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
                string voudat = this.txtEntryDate.Text.Substring(0, 11);
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
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


        protected void ibtnFindPrv_Click(object sender, EventArgs e)
        {
            this.GetPriviousVoucher();
        }



    }
}