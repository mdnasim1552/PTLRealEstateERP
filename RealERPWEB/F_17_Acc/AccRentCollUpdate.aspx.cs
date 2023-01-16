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

    public partial class AccRentCollUpdate : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        public static double TAmount;
        protected void Page_Load(object sender, EventArgs e)
        {

            //dgv1.Attributes.Add("onClick",
            //         " javascript:return confirm('Are You sure you want to input the record?');");

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Collection Update-Rental";
                //this.Master.Page.Title = "Collection Update-Rental";
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy ddd");
                // this.LoadBillCombo();


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
        private void GetMRNo()
        {

            string comcod = this.GetCompCode();
            string MRno = this.txtsrchMRno.Text.Trim() + "%";
            string date = this.txtdate.Text.Substring(0, 11);
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "GETRENTMRNO", date, MRno, "", "", "", "", "", "", "");
            this.ddlMRList.Items.Clear();
            this.ddlMRList.DataTextField = "mrno1";
            this.ddlMRList.DataValueField = "mrno";
            this.ddlMRList.DataSource = ds1.Tables[0];
            this.ddlMRList.DataBind();

        }


        private void calculation()
        {
            DataTable dt = (DataTable)Session["tblMrr"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.dgv1.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cramt)", "")) ?
          0.00 : dt.Compute("Sum(cramt)", ""))).ToString("#,##0;-#,##0; ");



        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.GetMRNo();
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.pnlBill.Visible = true;

                return;
            }
            this.lbtnOk.Text = "Ok";
            this.pnlBill.Visible = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.txtRefNum.Text = "";
            this.txtSrinfo.Text = "";
            this.txtNarration.Text = "";
            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";

            this.lnkFinalUpdate.Enabled = true;
            this.dgv1.DataSource = null;
            this.dgv1.DataBind();
        }

        private void GetVouCherNumber()
        {
            try
            {

                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                string comcod = this.GetCompCode();
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

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

        protected void imgSearchMRno_Click(object sender, EventArgs e)
        {
            this.GetMRNo();
        }

        private string vounum(string Cactcode, string VouMode)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return "";

            }

            DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

            if (txtopndate >= Convert.ToDateTime(this.txtdate.Text.Trim().Substring(0, 11)))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                return "";

            }
            double vcode1 = Convert.ToDouble(Request.QueryString["tcode"]);
            string ConAccHead = Cactcode;
            string VNo1 = (((Label)this.Master.FindControl("lblTitle")).Text.Contains("Journal") ? "J" : (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B"));
            string VNo2 = (VNo1 == "J" ? "V" : ((VouMode == "Payment") ? "D" : "C"));
            string VNo3 = Convert.ToString(VNo1 + VNo2);
            string entrydate = this.txtdate.Text.Substring(0, 11).Trim();
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");

            string cvno1 = ds4.Tables[0].Rows[0]["couvounum"].ToString().Substring(0, 8);
            this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
            this.txtCurrntlast6.Text = ds4.Tables[0].Rows[0]["couvounum"].ToString().Substring(8);
            return (ds4.Tables[0].Rows[0]["couvounum"].ToString());

        }
        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (!Convert.ToBoolean(dr1[0]["entry"]))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                    return;
                }

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string Terminal = hst["trmid"].ToString();
                string Sessionid = hst["session"].ToString();
                string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");



                DataTable dt = (DataTable)Session["tblMrr"];
                DataRow[] dr = dt.Select();
                string mrno = dr[0]["mrno"].ToString();
                string cheqno = dr[0]["chqno"].ToString();
                string pactcode1 = dr[0]["pactcode"].ToString();
                string usircode1 = dr[0]["usircode"].ToString();
                DateTime Paydate = Convert.ToDateTime(dr[0]["paydate"]);
                string voudat = this.txtdate.Text.Substring(0, 11);
                bool dcon = ASITUtility02.TransPostDateCheque(Paydate, Convert.ToDateTime(voudat));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Voucher Date is equal or greater Cheque Date');", true);
                    return;
                }


                /////////////////--------------------------------------------------
                //Existing MR




                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGCOLUP", mrno, pactcode1, usircode1, cheqno, "", "", "", "", "");

                if (ds4.Tables[0].Rows.Count == 0)
                    ;
                else if (ds4.Tables[0].Rows[0]["vounum"].ToString() != "00000000000000")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher No already Existing in this MR No";
                    return;
                }

                //return;
                //////////////-----------------------------
                string Cactcode = (dr[0]["Cactcode"].ToString());
                string VouMode = "";
                string vounum = this.vounum(Cactcode, VouMode);
                string Narration = "";
                string RefNo = "";



                RefNo = this.txtRefNum.Text.Trim();
                Narration = this.txtNarration.Text.Trim();
                /////////---------------------------------------


                string refnum = RefNo;
                string srinfo = dr[0]["sirialno"].ToString().Trim();
                string recondat = this.txtdate.Text.Substring(0, 11);
                string vounarration1 = Narration;
                string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
                vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
                string vouno = vounum.Substring(0, 2).ToString();
                string voutype = (vouno == "JV" ? "Journal Voucher" :
                                    (vouno == "CD" ? "Cash Payment Voucher" :
                                    (vouno == "BD" ? "Bank Payment Voucher" :
                                    (vouno == "CC" ? "Cash Deposit Voucher" :
                                    (vouno == "BC" ? "Bank Deposit Voucher" : "Unknown Voucher")))));
                string cactcode = (vouno == "JV" ? "000000000000" : Cactcode);
                string vtcode = "99";
                string edit = "EDIT";

                //-----------Update Transaction B Table-----------------//
                bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo, vounarration1,
                                vounarration2, voutype, vtcode, edit, userid, Terminal, Sessionid, Postdat, "", "");
                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    return;
                }
                //-----------Update Transaction A Table-----------------//

                bool resulta = true;
                string spclcode = "000000000000";
                foreach (DataRow dr2 in dt.Rows)
                {


                    string pactcode = dr2["pactcode"].ToString();
                    string usircode = dr2["usircode"].ToString();
                    double tqty = Convert.ToDouble(dr2["qty"].ToString());
                    double dramt = Convert.ToDouble(dr2["dramt"].ToString());
                    double cramt = Convert.ToDouble(dr2["cramt"].ToString());
                    string trnRemarks = dr2["urmrks"].ToString().Trim();
                    string trnamt = Convert.ToString(dramt - cramt);
                    resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, pactcode, usircode, cactcode,
                                    voudat, tqty.ToString(), trnRemarks, vtcode, trnamt, spclcode, recondat, "", "", "", "");

                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        return;
                    }
                }

                // Update  MRR------------------------------------

                resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "UPDATERENTMRINF", "", "", mrno, cheqno, vounum,
                            "", "", "", "", "", "", "", "", "", "");
                if (!resulta)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    return;
                }


             ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                this.lnkFinalUpdate.Enabled = false;
                this.txtcurrentvou.Enabled = false;
                this.txtCurrntlast6.Enabled = false;

            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //if (this.ddlPrivousVou.Items.Count > 0 && this.lnkOk.Text == "Ok")
                //    this.lnkOk_Click(null, null);

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string curvoudat = this.txtdate.Text.Substring(0, 11);
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                        this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();


                string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
                string currentptah = "AccPrint.aspx?Type=accVou&vounum=" + vounum;
                string totalpath = hostname + currentptah;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }
        protected void lbtnSelectMR_Click(object sender, EventArgs e)
        {

            Session.Remove("tblMrr");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string Mrno = this.ddlMRList.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "GETCOLLECTION", Mrno,
                          "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.dgv1.DataSource = null;
                this.dgv1.DataBind();
                return;
            }
            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.dgv1.DataSource = null;
                this.dgv1.DataBind();
                return;
            }
            Session["tblMrr"] = ds1.Tables[0];

            string Narration = "";
            string RefNo = "";


            ///-----------------------------------------------------///////////////////

            DataRow[] dr = ds1.Tables[0].Select();
            string type = dr[0]["paydesc"].ToString();
            if (type == "CASH")
            {
                Narration = "Mr" + dr[0]["mrno"].ToString() + " Date:" + Convert.ToDateTime(dr[0]["paydate"]).ToString("dd.MM.yyyy") + "; ";
            }

            else
            {
                RefNo = dr[0]["chqno"].ToString();
                Narration = "Mr" + dr[0]["mrno"].ToString() + ", " + "Date:" + Convert.ToDateTime(dr[0]["paydate"]).ToString("dd.MM.yyyy")
                        + (dr[0]["bankname"].ToString() == "" ? "" : ", " + "Bank: " + dr[0]["bankname"].ToString()) + (dr[0]["bbranch"].ToString() == "" ? "" : ", " + "Branch: " + dr[0]["bbranch"].ToString())
                        + (dr[0]["rmrks"].ToString() == "" ? "" : ", " + "Remarks: " + dr[0]["rmrks"].ToString()) + "; ";

            }
            this.txtRefNum.Text = RefNo;

            int lenght = Narration.Length;
            Narration = Narration.Substring(0, lenght - 2);
            this.txtNarration.Text = Narration;
            this.Data_Bind();

        }











        protected void Data_Bind()
        {

            DataTable tbl1 = (DataTable)Session["tblMrr"];
            dgv1.DataSource = tbl1;
            dgv1.DataBind();
            calculation();


        }

    }
}
