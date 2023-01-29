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
    public partial class AccPayUpdate : System.Web.UI.Page
    {
        //public static string Narration = "";
        public static double TAmount = 0;
        ProcessAccess accData = new ProcessAccess();
        public static int PageNumber = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblprintstk")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "AccIsu") ? "Post Dated Cheque (Issued)" : "Post Dated Cheque (Received)";
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.Master.Page.Title = (this.Request.QueryString["Type"] == "AccIsu") ? "Post Dated Cheque (Issued)" : "Post Dated Cheque (Received)";
                this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01" + this.txtfrmdate.Text.Trim().Substring(2);
                this.txttodate.Text = System.DateTime.Today.AddYears(1).ToString("dd-MMM-yyyy");

                this.txtVouDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string Type = this.Request.QueryString["Type"].ToString();
                this.GetBankName();
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;



            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void Refrsh()
        {

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void imgbtnSrchBank_Click(object sender, EventArgs e)
        {
            this.GetBankName();
        }

        private void GetBankName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string ddldesc = hst["ddldesc"].ToString();
            string comcod = this.GetCompCode();
            string SeachBankName = "%" + this.txtserchBankName.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBANKNAME", SeachBankName, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlBankName.Items.Clear();
                return;
            }

            DataTable dt = ds1.Tables[0];
            DataRow dr1 = dt.NewRow();
            dr1["comcod"] = this.GetCompCode();
            dr1["actcode"] = "000000000000";
            dr1["actdesc"] = "All Bank";
            dr1["actdesc1"] = "000000000000- All Bank";
            dt.Rows.Add(dr1);
            DataView dv = dt.DefaultView;
            dv.Sort = ("actcode");
            dt = dv.ToTable();

            string TextField = (ddldesc == "True" ? "actdesc" : "actdesc1");
            this.ddlBankName.DataTextField = TextField;
            this.ddlBankName.DataValueField = "actcode";
            this.ddlBankName.DataSource = dt;
            this.ddlBankName.DataBind();
            ds1.Dispose();

        }
        protected void lnkOk_Click(object sender, EventArgs e)
        {
            PageNumber = 0;
            this.lblCurPage.Text = "1";
            this.pnlGridPage.Visible = true;
            this.ShowData();
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);
            this.lblCurPage.ToolTip = "Page 1 of " + pageCount;
        }
        public string getcomchknum()
        {

            string comcod = this.GetCompCode();
            string orderbychknum = "";
            switch(comcod)
            {
                case "3368"://finlay
                    orderbychknum = "orderbychknum";
                    break;
                default:
                    break;
            }

            return orderbychknum;
        }

        private void ShowData()
        {


            try
            {
                Session.Remove("tblMrr");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                string orderbycjlnum = this.getcomchknum();

                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                          
                string voutype = (this.Request.QueryString["Type"].ToString() == "AccIsu") ? "PV%" : (this.Request.QueryString["Type"].ToString() == "AccRec") ? "DV%" : "%";
                int startRow = PageNumber * 100;
                int endRow = (PageNumber + 1) * 100;
                string SrchChequeno = "%" + this.txtserchChequeno.Text.Trim() + "%";
                string BankName = ((this.ddlBankName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlBankName.SelectedValue.ToString()) + "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "REPORTCHEQUEUPDATE", frmdate, todate, voutype, startRow.ToString(), endRow.ToString(), SrchChequeno, BankName, orderbycjlnum, "");
                if (ds1 == null)
                {
                    this.dgv1.DataSource = null;
                    this.dgv1.DataBind();
                    return;
                }
                Session["tblMrr"] = this.HiddenSameDate(ds1.Tables[0]);
                Session["tbltopage"] = ds1.Tables[1];
                this.Data_Bind();



            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error :" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblMrr"];
            this.dgv1.DataSource = dt;
            this.dgv1.DataBind();




            string Type = this.Request.QueryString["Type"].ToString();
            if (Type == "AccIsu" || Type == "AccRec")
            {
                this.dgv1.FooterRow.FindControl("lbtnUpdateAllVou").Visible = false;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked = true;
                    ((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Enabled = false;
                    ((LinkButton)this.dgv1.Rows[i].FindControl("lbok")).Enabled = false;
                }
            }
            //DataTable dt1 = (DataTable)Session["tblMrr"]; 
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                string actcode = ((Label)dgv1.Rows[i].FindControl("lblgvAccCod")).Text.Trim();
                string rescode = ((Label)dgv1.Rows[i].FindControl("lgcUcode")).Text.Trim();
                string chequeno = ((Label)dgv1.Rows[i].FindControl("lgvchnono")).Text.Trim();
                string vounum = ((Label)dgv1.Rows[i].FindControl("lgvPVnum")).Text.Trim();

                ((CheckBox)dgv1.Rows[i].FindControl("chkvmrno")).Visible = (!((CheckBox)dgv1.Rows[i].FindControl("chkvmrno")).Checked);
                ((LinkButton)dgv1.Rows[i].FindControl("lbok")).Visible = (!((CheckBox)dgv1.Rows[i].FindControl("chkvmrno")).Checked);




                LinkButton lbtn1 = (LinkButton)dgv1.Rows[i].FindControl("lbok");
                if (lbtn1 != null)
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = actcode + rescode + vounum + chequeno;
            }



            this.CalculatrGridTotal();
            Session["Report1"] = dgv1;
            if (dt.Rows.Count > 0)
                ((HyperLink)this.dgv1.HeaderRow.FindControl("hlbtnbtbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



        }

        private DataTable HiddenSameDate(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string grp = dt1.Rows[0]["grp"].ToString();
            string pactcode = dt1.Rows[0]["actcode"].ToString();
            string cactcode = dt1.Rows[0]["cactcode"].ToString();
            int j;
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }

                else
                {
                    grp = dt1.Rows[j]["grp"].ToString();

                }

            }



            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if ((dt1.Rows[j]["actcode"].ToString() == pactcode) && (dt1.Rows[j]["cactcode"].ToString() == cactcode))
                {
                    pactcode = dt1.Rows[j]["actcode"].ToString();
                    cactcode = dt1.Rows[j]["cactcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                    dt1.Rows[j]["cactdesc"] = "";
                }

                else
                {
                    if (dt1.Rows[j]["actcode"].ToString() == pactcode)
                        dt1.Rows[j]["actdesc"] = "";
                    if (dt1.Rows[j]["cactcode"].ToString() == cactcode)
                        dt1.Rows[j]["cactdesc"] = "";
                    pactcode = dt1.Rows[j]["actcode"].ToString();
                    cactcode = dt1.Rows[j]["cactcode"].ToString();
                }

            }
            return dt1;


            //grpcode = dt1.Rows[0]["grpcode"].ToString();
            //            string actcode = dt1.Rows[0]["actcode"].ToString();
            //            for (j = 1; j < dt1.Rows.Count; j++)
            //            {
            //                if (dt1.Rows[j]["grpcode"].ToString() == grpcode && dt1.Rows[j]["actcode"].ToString() == actcode)
            //                {
            //                    grpcode = dt1.Rows[j]["grpcode"].ToString();
            //                    actcode = dt1.Rows[j]["actcode"].ToString();
            //                    dt1.Rows[j]["grpdesc"] = "";
            //                    dt1.Rows[j]["actdesc"] = "";

            //                }

            //                else
            //                {


            //                    if (dt1.Rows[j]["grpcode"].ToString() == grpcode)
            //                    {
            //                        dt1.Rows[j]["grpdesc"] = "";
            //                    }
            //                    if (dt1.Rows[j]["actcode"].ToString() == actcode)
            //                    {
            //                        dt1.Rows[j]["actdesc"] = "";
            //                    }

            //                    grpcode = dt1.Rows[j]["grpcode"].ToString();
            //                    actcode = dt1.Rows[j]["actcode"].ToString();

            //                }

            //            }
            //        break;

            //}


            //  return dt1;



        }
        protected void CalculatrGridTotal()
        {
            DataTable dttotal = (DataTable)Session["tbltopage"];
            double cramt = Convert.ToDouble(((DataTable)Session["tbltopage"]).Rows[0]["cramt"]);
            ((Label)this.dgv1.FooterRow.FindControl("lgvFCrAmt")).Text = cramt.ToString("#,##0;-#,##0; ");
        }
        private void CheckValue()
        {
            DataTable dt = (DataTable)Session["tblMrr"];
            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                string chkmr = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? "True" : "False";
                string Recdate = (((TextBox)this.dgv1.Rows[i].FindControl("txtgvReconDat")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.dgv1.Rows[i].FindControl("txtgvReconDat")).Text.Trim();
                dt.Rows[i]["chkmv"] = chkmr;
                dt.Rows[i]["recndt"] = Recdate;

                ((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Enabled = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
                ((LinkButton)this.dgv1.Rows[i].FindControl("lbok")).Enabled = (((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
            }
            Session["tblMrr"] = dt;
        }
        protected void lbok_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            this.CheckValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            string EditByid = "";
            string Editdat = "01-Jan-1900";
            string pounaction = "";
            string aprovbyid = "";
            string aprvtrmid = "";
            string aprvseson = "";
            string aprvdat = "01-jan-1900";
            string srinfo = "";
            string edit = "";
            string rbankname = "";

            string userdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");



            string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string actcode = code.Substring(0, 12).ToString();
            string rescode = code.Substring(12, 12).ToString();
            string vounum = code.Substring(24, 14);
            string chequeno = code.Substring(38).ToString();
            DataTable dt = (DataTable)Session["tblMrr"];
            DataTable dt1 = dt.Copy();
            DataView dv = dt1.DefaultView;
            dv.RowFilter = ("vounum='" + vounum + "' and chequeno='" + chequeno + "'");
            dt1 = dv.ToTable();
            string chequedat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("dd-MMM-yyyy");

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string Chk = dt1.Rows[i]["chkmv"].ToString();
                if (Chk == "False")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Check CheckBox');", true);
                    return;
                }
            }

            string voudat = this.txtVouDate.Text.Substring(0, 11);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                string chequqdat = Convert.ToDateTime(dt1.Rows[i]["chequedat"]).ToString("dd-MMM-yyyy");
                bool dcon = ASITUtility02.TransPostDateCheque(Convert.ToDateTime(chequqdat), Convert.ToDateTime(voudat));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Voucher Date is equal or greater Cheque Date');", true);
                    return;
                }

            }

            string isunum = dt1.Rows[0]["isunum"].ToString();
            string recondat = (Convert.ToDateTime(dt1.Rows[0]["recndt"].ToString().Trim()).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? voudat : dt1.Rows[0]["recndt"].ToString().Trim();
            string vounara = dt1.Rows[0]["vnar"].ToString();
            string cactcode = dt1.Rows[0]["cactcode"].ToString();
            string Remarks = dt1.Rows[0]["vounum"].ToString().Trim();
            string PartyName = dt1.Rows[0]["payto"].ToString().Trim();

            string Vno = (dt1.Rows[0]["vounum"].ToString().Substring(0, 2) == "PV" && (actcode.Substring(0, 2) == "19" || actcode.Substring(0, 2) == "29")) ? "CT"
                : (dt1.Rows[0]["vounum"].ToString().Substring(0, 2) == "DV" && (actcode.Substring(0, 2) == "19" || actcode.Substring(0, 2) == "29")) ? "CT"
                : dt1.Rows[0]["vounum"].ToString().Substring(0, 2) == "PV" ? "BD" : "BC";


            /////////////////--------------------------------------------------
            ////Existing PV Or DV

            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGPVNUM", vounum, chequeno, "", "", "", "", "", "", "");
            if (ds4.Tables[0].Rows[0]["acvounum"].ToString() != "00000000000000")
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher No already Existing in this PV or DV No";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }





            DataSet ds5 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", voudat, Vno, "", "", "", "", "", "", "");
            Session["NEWVOU"] = ds5.Tables[0];
            DataTable dt12 = (DataTable)Session["NEWVOU"];


            string acvounum = dt12.Rows[0]["couvounum"].ToString();
            string acvouno = acvounum.Substring(0, 2).ToString();
            string vounarration1 = vounara.ToString();
            string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            string vtcode = (Vno == "CT") ? "92" : "99";


            string voutype = (vtcode == "92") ? "Contra Voucher" : (acvouno == "JV" ? "Journal Voucher" :
                                 (acvouno == "CD" ? "Cash Payment Voucher" :
                                 (acvouno == "BD" ? "Bank Payment Voucher" :
                                 (acvouno == "CC" ? "Cash Deposit Voucher" :
                                 (acvouno == "BC" ? "Bank Deposit Voucher" : "Unknown Voucher")))));


            string spclcode = "000000000000";
            //dr[0]["newvocnum"] = acvounum.Substring(0, 2) + acvounum.Substring(6, 2) + "-" + acvounum.Substring(8);
            //dr[0]["recndt"] = recondat;


            try
            {
                // -----------Update Transaction B Table-----------------//

                //bool resultb = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", acvounum, voudat, chequeno, "", vounarration1,
                //                vounarration2, voutype, vtcode, "", userid, Terminal, Sessionid, Postdat, "", "", PartyName, isunum, "", "", "", "");

                bool resultb = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE02", acvounum, voudat, chequeno, "", vounarration1,
                   vounarration2, voutype, vtcode, "", userid, Terminal, Sessionid, Postdat, EditByid, Editdat, PartyName, isunum, aprovbyid, aprvtrmid, aprvseson, aprvdat, pounaction, rbankname, chequedat, "", "");

                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                //    //-----------Update Transaction A Table-----------------//




                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    actcode = dt1.Rows[i]["actcode"].ToString();
                    rescode = dt1.Rows[i]["rescode"].ToString();
                    rescode = dt1.Rows[i]["rescode"].ToString();
                    spclcode = dt1.Rows[i]["spclcode"].ToString();
                    cactcode = dt1.Rows[i]["cactcode"].ToString();
                    double amount = Convert.ToDouble(dt1.Rows[i]["cramt"].ToString());
                    string billno = dt1.Rows[i]["billno"].ToString().Trim();
                    string trnamt = (dt1.Rows[i]["vounum"].ToString().Substring(0, 2) == "PV" ? amount.ToString() : (amount * -1).ToString());
                    // string recndt = "01-Jan-1900"; 
                    string recndt = Convert.ToDateTime(dt1.Rows[i]["recndt"]).ToString("dd-MMM-yyyy");
                    string rpcode = dt1.Rows[i]["rpcode"].ToString().Trim();
                    string sectcode = "000000000000";
                    //bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", acvounum, actcode, rescode, cactcode,
                    //                       voudat, "0", Remarks, vtcode, trnamt, spclcode, recondat, "", billno, "", "");

                    bool resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE02", acvounum, actcode, rescode, cactcode,
                                                           voudat, "0", Remarks, vtcode, trnamt, spclcode, recondat, rpcode, billno, userid, userdate, Terminal, sectcode, "", "", "", "", "", "", "", "", "");







                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                    // -----------Update Patment A Table-----------------//

                    bool resultpa = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACPAYVUPDATE", acvounum, vounum, actcode, rescode, cactcode, chequeno, spclcode, "", "",
                                    "", "", "", "", "", "");

                    if (!resultb)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                 ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                }
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Post Dated Cheque Update";
                    string eventdesc = "Update Cheque";
                    string eventdesc2 = acvounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {



                    string vounum1 = dt.Rows[i]["vounum"].ToString();
                    string chequeno1 = dt.Rows[i]["chequeno"].ToString();

                    if (vounum == vounum1 && chequeno == chequeno1)
                    {


                        dt.Rows[i]["newvocnum"] = acvounum.Substring(0, 2) + acvounum.Substring(6, 2) + "-" + acvounum.Substring(8);
                        dt.Rows[i]["recndt"] = recondat;

                    }

                }
                Session["tblMrr"] = dt;

                this.Data_Bind();
                this.CheckValue();

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }















            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            // ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}    

            //     this.CheckValue();
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    string userid = hst["usrid"].ToString();
            //    string Terminal = hst["compname"].ToString();
            //    string Sessionid = hst["session"].ToString(); 
            //    string Postdat =System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //    string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //    string actcode = code.Substring(0, 12).ToString();
            //    string rescode = code.Substring(12, 12).ToString();
            //    string vounum = code.Substring(24, 14);
            //    string chequeno = code.Substring(38).ToString();
            //    DataTable dt = (DataTable)Session["tblMrr"];
            //    DataRow[] dr = dt.Select(" actcode='" + actcode + "' and rescode='" + rescode + "' and vounum='"+vounum+"' and chequeno='" + chequeno + "'");

            //    string isunum = dr[0]["isunum"].ToString();
            //    string chequqdat = Convert.ToDateTime(dr[0]["chequedat"]).ToString("dd-MMM-yyyy");
            //    string voudat = this.txtVouDate.Text.Substring(0, 11);


            //    bool dcon = ASITUtility02.TransPostDateCheque(Convert.ToDateTime(chequqdat), Convert.ToDateTime(voudat));
            //    if (!dcon)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Voucher Date is equal or greater Cheque Date');", true);
            //        return;
            //    }

            //    string recondat = (Convert.ToDateTime(dr[0]["recndt"].ToString().Trim()).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? voudat : dr[0]["recndt"].ToString().Trim(); 
            //    string vounara = dr[0]["vnar"].ToString();
            //    double amount =Convert.ToDouble(dr[0]["cramt"].ToString());
            //    string cactcode = dr[0]["cactcode"].ToString();
            //    string Remarks = dr[0]["vounum"].ToString().Trim();
            //    string PartyName = dr[0]["payto"].ToString().Trim();
            //    string billno = dr[0]["billno"].ToString().Trim();
            //    string Chk = dr[0]["chkmv"].ToString();
            //    string trnamt =(dr[0]["vounum"].ToString().Substring(0,2)=="PV"?amount.ToString():(amount*-1).ToString());
            //    string Vno=(dr[0]["vounum"].ToString().Substring(0,2)=="PV"?"BD":"BC");
            //      if (Chk == "False")
            //        {
            //         ((Label)this.Master.FindControl("lblmsg")).Text = "Please Check CheckBox";
            //            return;
            //        }

            //  /////////////////--------------------------------------------------
            //  ////Existing PV Or DV

            //  DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGPVNUM", vounum, actcode, rescode, cactcode,chequeno, "", "", "", "");
            //  if (ds4.Tables[0].Rows[0]["acvounum"].ToString() != "00000000000000")
            //  {
            //   ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher No already Existing in this PV or DV No";
            //      return;
            //  }





            //  DataSet ds5 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", voudat, Vno, "", "", "", "", "", "", "");
            //    Session["NEWVOU"] = ds5.Tables[0];
            //    DataTable dt12 = (DataTable)Session["NEWVOU"];


            //    string acvounum = dt12.Rows[0]["couvounum"].ToString();
            //    string acvouno = acvounum.Substring(0, 2).ToString();
            //    string vounarration1 = vounara.ToString();
            //    string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            //    string voutype = (acvouno == "JV" ? "Journal Voucher" :
            //                     (acvouno == "CD" ? "Cash Payment Voucher" :
            //                     (acvouno == "BD" ? "Bank Payment Voucher" :
            //                     (acvouno == "CC" ? "Cash Deposit Voucher" :
            //                     (acvouno == "BC" ? "Bank Deposit Voucher" : "Unknown Voucher")))));
            //    string vtcode = "99";
            //    string spclcode = "000000000000";
            //    dr[0]["newvocnum"] = acvounum.Substring(0,2)+ acvounum.Substring(6,2) + "-" + acvounum.Substring(8);
            //    dr[0]["recndt"] = recondat;


            //    try
            //    {
            //        // -----------Update Transaction B Table-----------------//

            //        bool resultb = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", acvounum, voudat, chequeno, "", vounarration1,
            //                        vounarration2, voutype, vtcode, "", userid, Terminal, Sessionid, Postdat, "", "", PartyName, isunum, "", "", "", "");

            //        if (!resultb)
            //        {
            //         ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
            //            return;
            //        }
            //        //    //-----------Update Transaction A Table-----------------//


            //        bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", acvounum, actcode, rescode, cactcode,
            //                                   voudat, "0", Remarks, vtcode, trnamt, spclcode, recondat, "", billno, "", "");
            //        if (!resulta)
            //        {
            //         ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
            //            return;
            //        }

            //        // -----------Update Patment A Table-----------------//

            //        bool resultpa = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACPAYVUPDATE", acvounum, vounum, actcode, rescode, cactcode, chequeno, "", "", "",
            //                        "", "", "","","","");

            //        if (!resultb)
            //        {
            //         ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
            //            return;
            //        }

            //     ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";

            //        if (ConstantInfo.LogStatus == true)
            //        {
            //            string eventtype = "Post Dated Cheque Update";
            //            string eventdesc = "Update Cheque";
            //            string eventdesc2 = acvounum;
            //            bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //        }

            //        this.Data_Bind();
            //        this.CheckValue();

            //    }
            //        catch (Exception ex)
            //        {
            //         ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            //        }
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblMrr"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.DayWiseissueCheek>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptPostDatCheque", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Date", "From : " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy") + " To:" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "List of Post Dated Cheque"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void lbtnUpdateAllVou_Click(object sender, EventArgs e)
        {
            //    DataTable dt = (DataTable)Session["tblMrr"];
            //    for (int i = 0; i < dgv1.Rows.Count; i++)
            //    {
            //        Hashtable hst = (Hashtable)Session["tblLogin"];
            //        string comcod = hst["comcod"].ToString();
            //        string userid = hst["usrid"].ToString();
            //        string Terminal = hst["compname"].ToString();
            //        string Sessionid = hst["session"].ToString();
            //        string Postdat =System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            //        string chequqdat = Convert.ToDateTime(dt.Rows[i]["chequedat"]).ToString("dd-MMM-yyyy");
            //        string Vno = (dt.Rows[i]["vounum"].ToString().Substring(0, 2) == "PV" ? "BD" : "BC");
            //        DataSet ds5 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", chequqdat, Vno, "", "", "", "", "", "", "");
            //        Session["NEWVOU"] = ds5.Tables[0];
            //        DataTable dt12 = (DataTable)Session["NEWVOU"];
            //        string acvounum = dt12.Rows[0]["couvounum"].ToString();

            //        string actcode = dt.Rows[i]["actcode"].ToString();
            //        string rescode = dt.Rows[i]["rescode"].ToString();
            //        string chequeno = dt.Rows[i]["chequeno"].ToString();
            //        string vounara = dt.Rows[i]["vnar"].ToString();
            //        double amount = Convert.ToDouble(dt.Rows[i]["cramt"].ToString());
            //        string cactcode = dt.Rows[i]["cactcode"].ToString();
            //        string Remarks = dt.Rows[i]["vounum"].ToString().Trim();
            //        string PartyName = dt.Rows[i]["payto"].ToString().Trim();
            //        string Chk = dt.Rows[i]["chkmv"].ToString();
            //        string trnamt = (dt.Rows[i]["vounum"].ToString().Substring(0, 2) == "PV" ? amount.ToString() : (amount * -1).ToString());

            //        string acvouno = acvounum.Substring(0, 2).ToString();
            //        string vounarration1 = vounara.ToString();
            //        string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            //        string voutype = (acvouno == "JV" ? "Journal Voucher" :
            //                         (acvouno == "CD" ? "Cash Payment Voucher" :
            //                         (acvouno == "BD" ? "Bank Payment Voucher" :
            //                         (acvouno == "CC" ? "Cash Deposit Voucher" :
            //                         (acvouno == "BC" ? "Bank Deposit Voucher" : "Unknown Voucher")))));
            //        string vtcode = "99";
            //        string spclcode = "000000000000";
            //        dt.Rows[i]["newvocnum"] = acvounum.Substring(0, 2) + acvounum.Substring(6, 2) + "-" + acvounum.Substring(8);
            //        try
            //        {
            //            // -----------Update Transaction B Table-----------------//

            //            bool resultb = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", acvounum, chequqdat, chequeno, "", vounarration1,
            //                            vounarration2, voutype, vtcode, "", userid, Terminal, Sessionid, Postdat, "", "", PartyName, "", "", "", "", "");

            //            if (!resultb)
            //            {
            //             ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
            //                return;
            //            }
            //            //    //-----------Update Transaction A Table-----------------//


            //            bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", acvounum, actcode, rescode, cactcode,
            //                                       chequqdat, "0", Remarks, vtcode, trnamt, spclcode, "", "", "", "", "");
            //            if (!resulta)
            //            {
            //             ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
            //                return;
            //            }

            //            // -----------Update Patment A Table-----------------//

            //            bool resultpa = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACPAYVUPDATE", acvounum, Remarks, chequeno, "", "", "",
            //                            "", "", "", "", "", "", "", "", "");

            //            if (!resultb)
            //            {
            //             ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
            //                return;
            //            }

            //         ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";

            //            if (ConstantInfo.LogStatus == true)
            //            {
            //                string eventtype = "Post Dated Cheque Update";
            //                string eventdesc = "Update Cheque";
            //                string eventdesc2 = acvounum;
            //                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //            }
            //            //this.Data_Bind();
            //            //this.CheckValue();

            //            this.dgv1.DataSource = (DataTable)Session["tblMrr"];
            //            this.dgv1.DataBind();
            //            ((LinkButton)this.dgv1.FooterRow.FindControl("lbtnUpdateAllVou")).Enabled = false;

            //        }
            //        catch (Exception ex)
            //        {
            //         ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            //        }
            //    }
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        ((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Checked = true;
            //        ((CheckBox)this.dgv1.Rows[i].FindControl("chkvmrno")).Enabled = false;
            //        ((LinkButton)this.dgv1.Rows[i].FindControl("lbok")).Enabled = false;
            //    }

        }
        protected void dgv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label prodesc = (Label)e.Row.FindControl("lgactdesc");
                Label amt = (Label)e.Row.FindControl("lgvcramt");
                //Label sign = (Label)e.Row.FindControl("gvsign");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "typesum")).ToString().Trim();


                if (code == "")
                {
                    return;
                }

                else if (ASTUtility.Right(code, 1) == "Z")
                {
                    prodesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    //sign.Font.Bold = true;
                    prodesc.Style.Add("text-align", "right");

                }


            }
        }
        protected void imgBtnFirst_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);
            PageNumber = 0;
            this.ShowData();
            this.lblCurPage.Text = "1";
            this.lblCurPage.ToolTip = "Page 1 of " + pageCount;
            this.imgBtnPerv.Enabled = false;
            this.imgBtnNext.Enabled = true;

        }
        protected void imgBtnNext_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);
            PageNumber = PageNumber + 1;

            if (PageNumber == pageCount)
            {
                PageNumber = PageNumber - 1;
                this.imgBtnNext.Enabled = false;
                return;
            }
            this.lblCurPage.ToolTip = "Page " + (PageNumber + 1) + " of " + pageCount;
            this.lblCurPage.Text = (PageNumber + 1).ToString();
            this.imgBtnPerv.Enabled = true;
            this.ShowData();
        }

        protected void imgBtnPerv_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);

            PageNumber = PageNumber - 1;
            if (PageNumber < 0)
            {
                PageNumber = 0;
                this.imgBtnPerv.Enabled = false;
                return;
            }
            this.lblCurPage.ToolTip = "Page " + (PageNumber + 1) + " of " + pageCount;
            this.ShowData();
            this.lblCurPage.Text = (PageNumber + 1).ToString();
            this.imgBtnNext.Enabled = true;
        }
        protected void imgBtnLast_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);

            PageNumber = pageCount - 1;
            this.ShowData();
            this.lblCurPage.Text = pageCount.ToString();
            this.lblCurPage.ToolTip = "Page " + (pageCount) + " of " + pageCount;
            this.imgBtnNext.Enabled = false;
            this.imgBtnPerv.Enabled = true;
        }
        protected void imgbtnSearchCheqNO_Click(object sender, EventArgs e)
        {
            PageNumber = 0;
            this.lblCurPage.Text = "1";
            this.ShowData();
            DataTable dt = (DataTable)Session["tbltopage"];
            double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            int pageCount = (int)Math.Ceiling(getPageCount);
            this.lblCurPage.ToolTip = "Page 1 of " + pageCount;
        }

        protected void txtgvReconDat_TextChanged(object sender, EventArgs e)
        {

            int index = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
            string voudat = ((Label)this.dgv1.Rows[index].FindControl("lgvPVDate")).Text.Trim();
            string recondat = ((TextBox)this.dgv1.Rows[index].FindControl("txtgvReconDat")).Text.Trim();
            DateTime dtvou = Convert.ToDateTime(voudat);
            DateTime dtrecon = Convert.ToDateTime(recondat);
            if (dtvou > dtrecon)                                                                              
            {
                this.RiseError("Reconcilation Date Should be larger than Voucher Date");
            }
        }

        private void RiseError(string msg)
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = msg;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
        }
    }
}


