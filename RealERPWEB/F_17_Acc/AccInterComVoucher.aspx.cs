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
    public partial class AccInterComVoucher : System.Web.UI.Page
    {
        public static double TAmount;
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblprintstk")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "InterCompany Payment Voucher";
                this.Master.Page.Title = "InterCompany Payment Voucher";
                Hashtable hst = (Hashtable)Session["tblLogin"];
                this.lblFromCmpName.Text = hst["comnam"].ToString();
                this.txtfdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttdate.Text = this.txtfdate.Text;
                this.rbtnList1.SelectedIndex = 0;
                this.GetConAccCode();
                this.GetHeadAccout();
                this.GetToCompany();
                this.GetContAccCode();
                this.GetToHeadAccount();
                this.GetfVoucherNo();
                this.GettVoucherNo();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComcode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetConAccCode()
        {
            string comcod = this.GetComcode();
            string srchconhead = "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEAD", srchconhead, "", "", "", "", "", "", "", "");
            this.ddlConAccHead.DataSource = ds1.Tables[0];
            this.ddlConAccHead.DataTextField = "actdesc1";
            this.ddlConAccHead.DataValueField = "actcode";
            this.ddlConAccHead.DataBind();

        }
        private void GetHeadAccout()
        {
            string comcod = this.GetComcode();
            string filter = "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETHEADACCOUNT", filter, "", "", "", "", "", "", "", "");
            this.ddlAccHead.DataSource = ds1.Tables[0];
            this.ddlAccHead.DataTextField = "actdesc";
            this.ddlAccHead.DataValueField = "actcode";
            this.ddlAccHead.DataBind();

        }

        private void GetContAccCode()
        {
            string comcod = this.ddlToCompany.SelectedValue.ToString();
            string srchconhead = "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEAD", srchconhead, "", "", "", "", "", "", "", "");
            this.ddlContAccHead.DataSource = ds1.Tables[0];
            this.ddlContAccHead.DataTextField = "actdesc1";
            this.ddlContAccHead.DataValueField = "actcode";
            this.ddlContAccHead.DataBind();

        }


        private void GetToCompany()
        {

            string comcod = this.GetComcode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETTOCOMPANY", "", "", "", "", "", "", "", "", "");
            this.ddlToCompany.DataSource = ds1.Tables[0];
            this.ddlToCompany.DataTextField = "comnam";
            this.ddlToCompany.DataValueField = "comcod";
            this.ddlToCompany.DataBind();


        }

        private void GetToHeadAccount()
        {
            string comcod = this.ddlToCompany.SelectedValue.ToString();
            string filter = "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETHEADACCOUNT", filter, "", "", "", "", "", "", "", "");
            this.ddlAcctoHead.DataSource = ds1.Tables[0];
            this.ddlAcctoHead.DataTextField = "actdesc";
            this.ddlAcctoHead.DataValueField = "actcode";
            this.ddlAcctoHead.DataBind();

        }

        private void GetfVoucherNo()
        {

            try
            {
                string comcod = this.GetComcode();


                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate > Convert.ToDateTime(ASTUtility.DateFormat(this.txtfdate.Text.Trim())))
                {
                    string msg = "Voucher Date Must  Be Greater then Opening Date";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;

                }
                string VNo = "PV";
                string entrydate = ASTUtility.DateFormat(this.txtfdate.Text.Trim());
                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETNEWVOUCHER", entrydate, VNo, "", "", "", "", "", "", "");
                DataTable dt4 = ds4.Tables[0];
                this.lblfVoucherNo.Text = dt4.Rows[0]["couvounum"].ToString().Substring(0, 2) + dt4.Rows[0]["couvounum"].ToString().Substring(6, 2) + '-' + dt4.Rows[0]["couvounum"].ToString().Substring(8);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);

            }






        }
        private void GettVoucherNo()
        {



            try
            {
                string comcod = this.ddlToCompany.SelectedValue.ToString();


                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate > Convert.ToDateTime(ASTUtility.DateFormat(this.txttdate.Text.Trim())))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Voucher Date Must  Be Greater then Opening Date" + "');", true);
                    return;

                }
                string VNo = "DV";
                string entrydate = ASTUtility.DateFormat(this.txttdate.Text.Trim());
                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETNEWVOUCHER", entrydate, VNo, "", "", "", "", "", "", "");
                DataTable dt4 = ds4.Tables[0];

                this.lbltVoucherNo.Text = dt4.Rows[0]["couvounum"].ToString().Substring(0, 2) + dt4.Rows[0]["couvounum"].ToString().Substring(6, 2) + '-' + dt4.Rows[0]["couvounum"].ToString().Substring(8);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);

            }

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            // string comnam = hst["comnam"].ToString();
            string comcod = (this.rbtnList1.SelectedIndex == 0) ? hst["comcod"].ToString() : this.ddlToCompany.SelectedValue.ToString();
            string voudat = this.txtfdate.Text.Substring(0, 11);
            string vounum = (this.rbtnList1.SelectedIndex == 0) ? (this.lblfVoucherNo.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                this.lblfVoucherNo.Text.Trim().Substring(2, 2) + this.lblfVoucherNo.Text.Trim().Substring(5)) : (this.lbltVoucherNo.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                            this.lbltVoucherNo.Text.Trim().Substring(2, 2) + this.lbltVoucherNo.Text.Trim().Substring(5));
            string comname = (this.rbtnList1.SelectedIndex == 0) ? hst["comnam"].ToString() : this.ddlToCompany.SelectedItem.Text;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccPrint.aspx?Type=PostDatVou&comcod=" + comcod + "&comname=" + comname + "&vounum=" + vounum + "', target='_blank');</script>";

        }



        protected void imgbtnFindCAccount_Click(object sender, EventArgs e)
        {
            this.GetConAccCode();
        }
        protected void imgbtnFindAccount_Click(object sender, EventArgs e)
        {
            this.GetHeadAccout();
        }

        protected void ddlConAccHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetfVoucherNo();
        }


        protected void imgbtnFindtCAccount_Click(object sender, EventArgs e)
        {
            this.GetContAccCode();
        }

        protected void imgbtnFindtoAccount_Click(object sender, EventArgs e)
        {
            this.GetToHeadAccount();
        }
        protected void ddlToCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetContAccCode();
            this.GetToHeadAccount();
            this.GettVoucherNo();
        }
        protected void ddlContAccHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.GetToHeadAccount();
        }
        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {
            if (lbtnRefresh.Text == "NEW")
            {
                this.txttdate.Text = this.txtfdate.Text;
                this.lbltRefNum.Text = "";
                this.lbltcramt.Text = "";
                this.txtDrAmt.Text = "";
                this.txtDrAmt2.Visible = true;
                this.txtDrAmt.Visible = false;
                this.lbtnRefresh.Text = "SELECT";
                this.lbtnRefresh.Enabled = true;
                this.lbtnUpdate.Enabled = true;
                this.txtfdate.Enabled = true;
                this.txtDrAmt.Text = "";

            }
            else
            {
                if (isRefnoInValid())
                {
                    txtSrinfo.Focus();
                    return;
                }
                this.txttdate.Text = this.txtfdate.Text;
                this.lbltRefNum.Text = this.txtRefNum.Text;
                this.txtDrAmt.Visible = true;
                this.lbltcramt.Text = Convert.ToDouble("0" + this.txtDrAmt2.Text.Trim()).ToString("#,##0.00;(#,##0.00); ");
                this.txtDrAmt.Text = Convert.ToDouble("0" + this.txtDrAmt2.Text.Trim()).ToString("#,##0.00;(#,##0.00); ");
                this.txtDrAmt2.Visible = false;
                this.lbtnRefresh.Text = "NEW";
            }

            //this.GetfVoucherNo();
            //this.GettVoucherNo();


        }

        private bool isRefnoInValid()
        {
            string refNo = this.txtSrinfo.Text.Trim().ToString();
            string comcod = this.GetComcode();
            string msg = "";
            if (refNo.Length == 0)
            {
                msg = "Ref No. Should Not Be Empty";               
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return true;
            }
            refNo = "%" + refNo + "%";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "CHECKEDDUPREFNO", refNo, "", "", "", "", "", "", "", "");

            if (ds2.Tables[0].Rows.Count == 0)
                return false;

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Found Duplicate Ref No" + "');", true);
                return true;
            }

        }

        private string GetIssueNo()
        {
            string comcod = this.GetComcode();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETISSUENO", "", "", "", "", "", "", "", "", "");
            return ds2.Tables[0].Rows[0]["isunum"].ToString();
        }


        private string GettoIssueNo()
        {
            string comcod = this.ddlToCompany.SelectedValue.ToString();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETISSUENO", "", "", "", "", "", "", "", "", "");
            return ds2.Tables[0].Rows[0]["isunum"].ToString();
        }


        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {

            string isunum = "";
            string msg = "";


            //issunum1 = this.GettoIssueNo();
            //return;

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                msg = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }
            if (isRefnoInValid())
            {
                txtSrinfo.Focus();
                return;
            }
            
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string PostedByid = hst["usrid"].ToString();
            string toPostedByid = this.ddlToCompany.SelectedValue.ToString() + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Postedtrmid = hst["compname"].ToString();
            string PostedSession = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetComcode();
            this.GetfVoucherNo();
            this.GettVoucherNo();
            string voudat = Convert.ToDateTime(this.txtfdate.Text).ToString("dd-MMM-yyyy");
            string vounum1 = this.lblfVoucherNo.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                            this.lblfVoucherNo.Text.Trim().Substring(2, 2) + this.lblfVoucherNo.Text.Trim().Substring(5);

            string vounum2 = this.lbltVoucherNo.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                           this.lbltVoucherNo.Text.Trim().Substring(2, 2) + this.lbltVoucherNo.Text.Trim().Substring(5);
            string refnum = this.txtRefNum.Text.Trim();
            string refno = this.txtSrinfo.Text.Trim();
            string vounarration1, vounarration2, vouno, voutype, cactcode, trnamt;
            vounarration1 = this.txtNarration.Text.Trim();
            vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
            vouno = this.lblfVoucherNo.Text.Trim().Substring(0, 2);
            voutype = (vouno == "PV" ? "Payment Voucher" : vouno == "DV" ? "Deposit Voucherr" : "");
            cactcode = this.ddlConAccHead.SelectedValue.ToString();
            string vtcode = "99";
            string edit = "";
            string actcode = this.ddlAccHead.SelectedValue.ToString();
            string rescode = "000000000000";
            string spclcode = "000000000000";
            string trnqty = "0";
            string billno = "";
            string insofissueno = "";

            double Dramt, Cramt;
            Cramt = Convert.ToDouble(this.lbltcramt.Text.Trim().ToString());
            Dramt = Convert.ToDouble(this.txtDrAmt.Text.Trim().ToString());
            string trnremarks = "";
            isunum = this.GetIssueNo();


            try
            {
                //-----------Update Transaction B Table-----------------//

                bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "INOFUPACPMNTB", vounum1, voudat, vounarration1,
                              vounarration2, voutype, vtcode, edit, PostedByid, Postedtrmid, PostedSession, Posteddat, "", "", refno, "");


                if (!resultb)
                {
                    msg = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }
                //-----------Update Transaction A Table-----------------//


                bool resulta = accData.UpdateTransInfo3(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "INOFUPACPMNTA", vounum1, actcode, rescode, refnum, cactcode,
                              voudat, Dramt.ToString(), voudat, trnremarks, vtcode, "", isunum, "00000000000000", billno, insofissueno, spclcode, "", "", "", "", "", "");




                if (!resulta)
                {
                    msg = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }

                //////////Update Transfer To                  



                comcod = this.ddlToCompany.SelectedValue.ToString();
                actcode = this.ddlAcctoHead.SelectedValue.ToString();
                cactcode = this.ddlContAccHead.SelectedValue.ToString();
                vounarration1 = this.txttNarration.Text.Trim();
                vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
                vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
                vouno = this.lbltVoucherNo.Text.Trim().Substring(0, 2);
                voutype = (vouno == "PV" ? "Payment Voucher" : vouno == "DV" ? "Deposit Voucher" : "");
                cactcode = this.ddlContAccHead.SelectedValue.ToString();
                Dramt = 0;
                Cramt = Convert.ToDouble(this.lbltcramt.Text.Trim());
                trnamt = Convert.ToString(Dramt - Cramt);
                isunum = this.GettoIssueNo();


                resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "INOFUPACPMNTB", vounum2, voudat, vounarration1,
                          vounarration2, voutype, vtcode, edit, toPostedByid, Postedtrmid, PostedSession, Posteddat, "", "", refno, "");

                if (!resultb)
                {
                    msg = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }


                resulta = accData.UpdateTransInfo3(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "INOFUPACPMNTA", vounum2, actcode, rescode, refnum, cactcode,
                                voudat, trnamt, voudat, trnremarks, vtcode, "", isunum, "00000000000000", billno, insofissueno, spclcode, "", "", "", "", "", "");



                if (!resulta)
                {
                    msg = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated Successfully" + "');", true);

                this.lbtnRefresh.Enabled = true;
                this.lbtnUpdate.Enabled = false;
                this.txtfdate.Enabled = false;

            }
            catch (Exception ex)
            {
                msg = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Inter Company Payment Voucher";
                string eventdesc = "Update Voucher";
                string eventdesc2 = vounum2;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }



    }
}





