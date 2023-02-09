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
namespace RealERPWEB.F_81_Hrm.F_90_PF
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
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((Label)this.Master.FindControl("lblprintstk")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "InterCompany Payment Voucher";
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
            string srchconhead = "%" + this.txtsercacc.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEAD", srchconhead, "", "", "", "", "", "", "", "");
            this.ddlConAccHead.DataSource = ds1.Tables[0];
            this.ddlConAccHead.DataTextField = "actdesc1";
            this.ddlConAccHead.DataValueField = "actcode";
            this.ddlConAccHead.DataBind();

        }
        private void GetHeadAccout()
        {
            string comcod = this.GetComcode();
            string filter = this.txtsercacc.Text + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETHEADACCOUNT", filter, "", "", "", "", "", "", "", "");
            this.ddlAccHead.DataSource = ds1.Tables[0];
            this.ddlAccHead.DataTextField = "actdesc";
            this.ddlAccHead.DataValueField = "actcode";
            this.ddlAccHead.DataBind();

        }

        private void GetContAccCode()
        {
            string comcod = this.ddlToCompany.SelectedValue.ToString();
            string srchconhead = "%" + this.txtsetrcacc.Text.Trim() + "%";
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
            string filter = this.txtsertoheacc.Text + "%";
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
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

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
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

            }

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string voudat = Convert.ToDateTime(this.txtfdate.Text.Trim()).ToString("dd-MMM-yyyy");
                string vounum = (this.rbtnList1.SelectedIndex == 0) ? (this.lblfVoucherNo.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                    this.lblfVoucherNo.Text.Trim().Substring(2, 2) + this.lblfVoucherNo.Text.Trim().Substring(5)) : (this.lbltVoucherNo.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                                this.lbltVoucherNo.Text.Trim().Substring(2, 2) + this.lbltVoucherNo.Text.Trim().Substring(5));



                string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
                string currentptah = "AccPrint.aspx?Type=accVou&vounum=" + vounum;
                string totalpath = hostname + currentptah;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";



                //DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");
                //if (_ReportDataSet == null)
                //    return;
                //DataTable dt = _ReportDataSet.Tables[0];
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

                //DataTable dt1 = _ReportDataSet.Tables[1];
                //string Vounum = dt1.Rows[0]["vounum"].ToString();
                //string refnum = dt1.Rows[0]["refnum"].ToString();
                //string voutype = dt1.Rows[0]["voutyp"].ToString();
                //string venar = dt1.Rows[0]["venar"].ToString();
                //ReportDocument rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();
                //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //txtCompanyName.Text = comnam;
                //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                //txtcAdd.Text = comadd;
                //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //vounum1.Text = "Voucher No.: " + vounum;
                //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //date.Text = " Date:" + voudat;
                //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //Refnum.Text = "Reference No.: " + refnum;
                //TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
                //voutype1.Text = voutype;
                //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //naration.Text = "Naration: " + venar;

                ////TextObject txtBname = rptinfo.ReportDefinition.ReportObjects["bankname"] as TextObject;
                ////txtBname.Text = this.ddlConAccHead.SelectedItem.Text.Substring(13);
                //TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                //rpttxtamt.Text = ASTUtility.Trans(TAmount, 2);

                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                //if (ConstantInfo.LogStatus == true)
                //{
                //    string eventtype = "Inter Company Payment Voucher";
                //    string eventdesc = "Print Voucher";
                //    string eventdesc2 = "Voucher No.: " + vounum;
                //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                //}

                //Session["Report1"] = rptinfo;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
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

            this.txttdate.Text = this.txtfdate.Text;
            this.lbltRefNum.Text = this.txtRefNum.Text;
            this.lbltcramt.Text = Convert.ToDouble(this.txtDrAmt.Text).ToString("#,##0.00;(#,##0.00); ");
            this.txtDrAmt.Text = Convert.ToDouble(this.txtDrAmt.Text).ToString("#,##0.00;(#,##0.00); ");
            //this.GetfVoucherNo();
            //this.GettVoucherNo();
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

            //issunum1 = this.GettoIssueNo();
            //return;

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            this.lbtnRefresh_Click(null, null);
            Hashtable hst = (Hashtable)Session["tblLogin"];


            string PostedByid = hst["usrid"].ToString();
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
            string srinfo1 = this.txtSrinfo.Text.Trim();
            string srinfo2 = this.txttSrinfo.Text.Trim();
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
            double Dramt, Cramt;
            Dramt = Convert.ToDouble(this.txtDrAmt.Text.Trim());
            string trnremarks = "";
            isunum = this.GetIssueNo();
            try
            {
                //-----------Update Transaction B Table-----------------//

                bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "INOFUPACPMNTB", vounum1, voudat, vounarration1,
                              vounarration2, voutype, vtcode, edit, PostedByid, Postedtrmid, PostedSession, Posteddat, "", "", "", "");

                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                //-----------Update Transaction A Table-----------------//


                bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "INOFUPACPMNTA", vounum1, actcode, rescode, refnum, cactcode,
                              voudat, Dramt.ToString(), voudat, trnremarks, vtcode, "", isunum, "00000000000000", "", "");

                if (!resulta)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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
                voutype = (vouno == "PV" ? "Payment Voucher" : vouno == "DV" ? "Deposit Voucherr" : "");
                cactcode = this.ddlContAccHead.SelectedValue.ToString();
                Dramt = 0;
                Cramt = Convert.ToDouble(this.lbltcramt.Text.Trim());
                trnamt = Convert.ToString(Dramt - Cramt);
                isunum = this.GettoIssueNo();


                resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "INOFUPACPMNTB", vounum2, voudat, vounarration1,
                          vounarration2, voutype, vtcode, edit, PostedByid, Postedtrmid, PostedSession, Posteddat, "", "", "", "");

                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }


                resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "INOFUPACPMNTA", vounum2, actcode, rescode, refnum, cactcode,
                                voudat, trnamt, voudat, trnremarks, vtcode, "", isunum, "00000000000000", "", "");

                if (!resulta)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }


             ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                this.lbtnRefresh.Enabled = false;
                this.lbtnUpdate.Enabled = false;
                this.txtfdate.Enabled = false;

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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





