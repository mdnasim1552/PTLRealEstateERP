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

    public partial class AccSalesUpdate : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        public static double TAmount;
        protected void Page_Load(object sender, EventArgs e)
        {

            //dgv1.Attributes.Add("onClick",
            //         " javascript:return confirm('Are You sure you want to input the record?');");

            if (!IsPostBack)
            {
                //((Label)this.Master.FindControl("lblTitle")).Text = "Invoice Update";
                //this.Master.Page.Title = "Invoice Update";
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //----------------udate-20150120---------
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));



                //----------------udate-20150120 end---------


                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy ddd");
                // this.LoadBillCombo();
                CreateTable();

            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void CreateTable()
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
            tblt01.Columns.Add("billno", Type.GetType("System.String"));
            tblt01.Columns.Add("billnar", Type.GetType("System.String"));
            ViewState["tblt01"] = tblt01;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void LoadBillCombo()
        {

            string comcod = this.GetCompCode();
            string Billno = this.txtBillno.Text.Trim() + "%";
            string date = this.txtdate.Text.Substring(0, 11);
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETINVOICE", Billno, date, "", "", "", "", "", "", "");
            this.ddlInvList.Items.Clear();
            this.ddlInvList.DataTextField = "textfield";
            this.ddlInvList.DataValueField = "sinvno";
            this.ddlInvList.DataSource = ds1.Tables[0];
            this.ddlInvList.DataBind();
        }


        private void calculation()
        {
            DataTable dt2 = (DataTable)ViewState["tblt01"];
            if (dt2.Rows.Count == 0)
                return;
            accData.ToDramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trndram)", "")) ?
                         0.00 : dt2.Compute("Sum(trndram)", ""))), 2);
            accData.ToCramt = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trncram)", "")) ?
                        0.00 : dt2.Compute("Sum(trncram)", ""))), 2);
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvDrAmt")).Text = (accData.ToDramt).ToString("#,##0.00;(#,##0.00); - ");
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvCrAmt")).Text = (accData.ToCramt).ToString("#,##0.00;(#,##0.00); - ");



        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.pnlBill.Visible = true;
                this.PnlNarration.Visible = true;

                return;
            }
            this.lbtnOk.Text = "Ok";
            this.pnlBill.Visible = false;
            this.PnlNarration.Visible = false;
            ViewState.Remove("tblt01");
            this.CreateTable();


            this.txtRefNum.Text = "";
            this.txtSrinfo.Text = "";
            this.txtNarration.Text = "";
            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";
            this.ddlInvList.Items.Clear();
            this.lnkFinalUpdate.Enabled = true;
            this.dgv2.DataSource = null;
            this.dgv2.DataBind();
        }

        private void GetVouCherNumber()
        {
            try
            {

                string comcod = this.GetCompCode();
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate > Convert.ToDateTime(this.txtdate.Text.Trim().Substring(0, 11)))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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
        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }




            if (Math.Round(accData.ToDramt) != Math.Round(accData.ToCramt))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Debit Amount must be Equal Credit Amount";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //string vounum = this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim();
            //Existing   Purchase No  

            if (dgv2.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Select at least one Bill');", true);
                return;

            }


            for (int i = 0; i < dgv2.Rows.Count; i++)
            {

                string billno = ((Label)this.dgv2.Rows[i].FindControl("lblBillno")).Text.Trim();
                DataSet ds4;
                if (i == 0)
                    ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGINVVOUCHER", billno, "", "", "", "", "", "", "", "");

                else if (((Label)this.dgv2.Rows[i - 1].FindControl("lblBillno")).Text.Trim() == billno)
                    continue;

                else
                    ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGINVVOUCHER", billno, "", "", "", "", "", "", "", "");


                if (ds4.Tables[0].Rows[0]["vounum"].ToString() != "00000000000000")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher No already Existing in this Bill No";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

            }

            this.GetVouCherNumber();
            string voudat = this.txtdate.Text.Substring(0, 11);
            string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                                   this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
            string refnum = this.txtRefNum.Text.Trim();
            string srinfo = this.txtSrinfo.Text;
            string vounarration1 = this.txtNarration.Text.Trim();
            string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
            string voutype = "Journal Voucher";
            string cactcode = "000000000000";
            string vtcode = "98";
            string edit = "";

            try
            {
                //-----------Update Transaction B Table-----------------//
                bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo,
                        vounarration1, vounarration2, voutype, vtcode, edit, userid, Terminal, Sessionid, Postdat, "", "");
                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                //-----------Update Transaction A Table-----------------//
                string billno2 = "XXXXXXXXXXXXXX";

                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    string actcode = ((Label)this.dgv2.Rows[i].FindControl("lblAccCod")).Text.Trim();
                    string rescode = ((Label)this.dgv2.Rows[i].FindControl("lblResCod")).Text.Trim();
                    string spclcode = ((Label)this.dgv2.Rows[i].FindControl("lblSpclCod")).Text.Trim();
                    string trnqty = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvQty")).Text.Trim()).ToString();
                    double Dramt = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvDrAmt")).Text.Trim());
                    double Cramt = Convert.ToDouble("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvCrAmt")).Text.Trim());

                    double trnamt = (Dramt - Cramt);
                    string trnremarks = ((TextBox)this.dgv2.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                    string billno = ((Label)this.dgv2.Rows[i].FindControl("lblBillno")).Text.Trim();
                    if (trnamt != 0)
                    {
                        bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum,
                                actcode, rescode, cactcode, voudat, trnqty, trnremarks, vtcode, trnamt.ToString(), spclcode, "", "", "", "", "");
                        if (!resulta)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }

                        if (billno2 != billno)
                        {
                            resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEINVOICE",
                                    billno, vounum, "", "", "", "", "", "", "", "", "", "", "", "", "");
                            if (!resulta)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                                return;
                            }
                            billno2 = billno;
                        }
                    }
                }
             ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                //this.lblmsg.Text=@"<SCRIPT language= "JavaScript"  > window.open('RptViewer.aspx');</script>";
                this.lnkFinalUpdate.Enabled = false;
                this.txtcurrentvou.Enabled = false;
                this.txtCurrntlast6.Enabled = false;

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Purchase Update";
                    string eventdesc = "Update Purchase Bill";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

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


                //string vounum = this.ddlPrivousVou.SelectedValue.ToString();
                // DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");
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
                // string Posteddat = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
                //// string Type = this.CompanyPrintVou();
                // ReportDocument rptinfo = new ReportDocument();
                // rptinfo = new  RealERPRPT.R_17_Acc.rptPrintVoucher();
                // //if (Type == "VocherPrint")
                // //{
                // //    rptinfo = new  RealERPRPT.R_17_Acc.rptPrintVoucher();
                // //}
                // //else if (Type == "VocherPrint1")
                // //{
                // //    rptinfo = new  RealERPRPT.R_17_Acc.rptPrintVoucher1();
                // //    TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                // //    txtPosteddate1.Text = "Entry Date: " + Posteddat;

                // //}
                // //else if (Type == "VocherPrint2")
                // //{
                // //    rptinfo = new  RealERPRPT.R_17_Acc.rptPrintVoucher2();
                // //    TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate2"] as TextObject;
                // //    txtPosteddate2.Text = "Entry Date: " + Posteddat;
                // //}
                // //else
                // //{
                // //    rptinfo = new  RealERPRPT.R_17_Acc.rptPrintVoucher3();
                // //    TextObject txtPosteddate3 = rptinfo.ReportDefinition.ReportObjects["entrydate3"] as TextObject;
                // //    txtPosteddate3.Text = "Entry Date: " + Posteddat;
                // //}


                // rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                // TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                // txtCompanyName.Text = comnam;
                // TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                // txtcAdd.Text = comadd;
                // TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                // vounum1.Text = "Voucher No.: " + vounum;
                // TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                // date.Text = "Voucher Date: " + voudat;
                // TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                // Refnum.Text = "Cheque/Ref. No.: " + refnum;
                // TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                // rpttxtPartyName.Text = "";// (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                // TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
                // voutype1.Text = voutype;
                // TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                // naration.Text = "Narration: " + venar;

                // //TextObject txtBname = rptinfo.ReportDefinition.ReportObjects["bankname"] as TextObject;
                // //txtBname.Text = this.ddlConAccHead.SelectedItem.Text.Substring(13);
                // TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                // rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);

                // TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                // txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                // if (ConstantInfo.LogStatus == true)
                // {
                //     string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                //     string eventdesc = "Print Voucher";
                //     string eventdesc2 = vounum;
                //     bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                // }
                // //string comcod = this.GetComeCode();
                // //string comcod = hst["comcod"].ToString();
                // string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                // rptinfo.SetParameterValue("ComLogo", ComLogo);
                // Session["Report1"] = rptinfo;
                // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }
        protected void lbtnSelectBill_Click(object sender, EventArgs e)
        {


            string comcod = this.GetCompCode();
            string billid = this.ddlInvList.SelectedValue.ToString();
            string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string callType = (comcod == "9102") ? "GETACCSALESJOURNAL02" : "GETACCSALESJOURNAL";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", callType, billid,
                          Date, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            DataTable tblt01 = (DataTable)ViewState["tblt01"];

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string dgAccCode = dt1.Rows[i]["actcode"].ToString();
                string dgResCode = dt1.Rows[i]["rescode"].ToString();
                string dgSpclCode = dt1.Rows[i]["spcode"].ToString();
                string dgAccDesc = dt1.Rows[i]["actdesc"].ToString();
                string dgResDesc = dt1.Rows[i]["resdesc"].ToString();
                string dgSpclDesc = dt1.Rows[i]["spcfdesc"].ToString();
                double dgTrnrate = 0.00;
                double dgTrnQty = Convert.ToDouble(dt1.Rows[i]["billqty"]);
                if (Convert.ToDouble(dt1.Rows[i]["billqty"]) > 0)
                {
                    dgTrnrate = Convert.ToDouble(dt1.Rows[i]["Cr"]) / Convert.ToDouble(dt1.Rows[i]["billqty"]);
                }

                double dgTrnDrAmt = Convert.ToDouble(dt1.Rows[i]["Dr"]);
                double dgTrnCrAmt = Convert.ToDouble(dt1.Rows[i]["Cr"]);
                string dgTrnRemarks = dt1.Rows[i]["billid"].ToString();
                string dgBillnarr = dt1.Rows[i]["billnar"].ToString();

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
                dr1["billnar"] = dgBillnarr;
                tblt01.Rows.Add(dr1);
            }
            ViewState["tblt01"] = HiddenSameData(tblt01);
            this.Data_Bind();
        }

        protected void Data_Bind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblt01"];
            dgv2.DataSource = tbl1;
            dgv2.DataBind();
            this.GridColoumnVisible();
            calculation();


        }

        private void GridColoumnVisible()
        {
            DataTable tbl1 = (DataTable)ViewState["tblt01"];
            int TblRowIndex2;
            for (int j = 0; j < this.dgv2.Rows.Count; j++)
            {

                TblRowIndex2 = (this.dgv2.PageIndex) * this.dgv2.PageSize + j;
                string mRSIRCODE = tbl1.Rows[TblRowIndex2]["subcode"].ToString();
                if (ASTUtility.Left(mRSIRCODE, 2) != "97")
                    ((TextBox)this.dgv2.Rows[j].FindControl("txtgvCrAmt")).ReadOnly = true;
            }


        }

        private DataTable HiddenSameData(DataTable dt1)
        {


            if (dt1.Rows.Count == 0)
                return dt1;
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




        protected void imgSearchBillno_Click(object sender, EventArgs e)
        {
            this.LoadBillCombo();
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblt01"];
            double todramt = 0, tocramt = 0;
            int TblRowIndex2;
            for (int j = 0; j < this.dgv2.Rows.Count; j++)
            {
                string Supplier = ((Label)this.dgv2.Rows[j].FindControl("lblResCod")).Text.Trim();
                double dramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvDrAmt")).Text.Trim()));
                todramt = todramt + dramt;
                TblRowIndex2 = (this.dgv2.PageIndex) * this.dgv2.PageSize + j;
                if (ASTUtility.Left(Supplier, 2) == "99")
                {
                    dt1.Rows[TblRowIndex2]["trncram"] = todramt - tocramt;
                    todramt = 0; tocramt = 0;
                    continue;
                }

                double cramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvCrAmt")).Text.Trim()));
                tocramt = tocramt + cramt;

                dt1.Rows[TblRowIndex2]["trncram"] = cramt;

            }
            ViewState["tblt01"] = dt1;
            this.Data_Bind();



        }
    }
}
