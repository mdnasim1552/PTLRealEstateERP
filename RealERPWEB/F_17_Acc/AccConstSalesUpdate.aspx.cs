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

    public partial class AccConstSalesUpdate : System.Web.UI.Page
    {
        public static double TAmount;
        ProcessAccess accData = new ProcessAccess();
        public static double todramt = 0.000000, tocramt = 0.000000;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Sales Journal(Construction)";
                //this.Master.Page.Title = "Sub-contractor bill finalization";
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                this.CreateTable();
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.LoadBillList();
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.CompanyPost();

            }

        }
        private void CompanyPost()
        {


            string comcod = this.GetComeCode();

            switch (comcod)
            {

                case "3332":

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


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
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
            tblt01.Columns.Add("rmrks", Type.GetType("System.String"));
            tblt01.Columns.Add("billno", Type.GetType("System.String"));
            ViewState["tblt01"] = tblt01;
        }

        protected void imgSearchBillno_Click(object sender, EventArgs e)
        {
            this.LoadBillList();
        }
        private void LoadBillList()
        {





            string comcod = this.GetComeCode();
            string txtSearchBill = (this.Request.QueryString["genno"].ToString()).Length == 0 ? "%" + this.txtBillno.Text.Trim() + "%" : this.Request.QueryString["genno"].ToString() + "%";
            string date = (this.Request.QueryString["Date1"].ToString()).Length == 0 ? Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy") : this.Request.QueryString["Date1"].ToString();




            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONSTBILLLIST", txtSearchBill, date, "", "", "", "", "", "", "");
            this.ddBillList.Items.Clear();
            this.ddBillList.DataTextField = "textfield";
            this.ddBillList.DataValueField = "billno";
            Session["narration"] = ds1.Tables[0];
            this.ddBillList.DataSource = ds1.Tables[0];
            this.ddBillList.DataBind();
        }

        private void Data_Bind()
        {
            dgv2.DataSource = (DataTable)ViewState["tblt01"];
            dgv2.DataBind();
            // this.GridColoumnVisible();
            this.calculation();
            this.Narration();
        }

        private void Narration()
        {
            DataTable dt = ((DataTable)Session["narration"]).Copy();
            string billno = this.ddBillList.SelectedValue.ToString();
            this.txtNarration.Text = (dt.Select("billno='" + billno + "'"))[0]["textfield1"].ToString();
        }
        private void calculation()
        {
            DataTable dt2 = (DataTable)ViewState["tblt01"];
            if (dt2.Rows.Count == 0)
                return;

            ((Label)this.dgv2.FooterRow.FindControl("lblgvFDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trndram)", "")) ?
                        0.00 : dt2.Compute("Sum(trndram)", ""))).ToString("#,##0.00;-#,##0.00; - ");
            ((Label)this.dgv2.FooterRow.FindControl("lblgvFCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trncram)", "")) ?
                        0.00 : dt2.Compute("Sum(trncram)", ""))).ToString("#,##0.00;-#,##0.00; - ");
            //todramt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trndram)", "")) ?
            //              0.00 : dt2.Compute("Sum(trndram)", "")));
            //tocramt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trncram)", "")) ?
            //            0.00 : dt2.Compute("Sum(trncram)", "")));


            accData.ToDramt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trndram)", "")) ?
                          0.00 : dt2.Compute("Sum(trndram)", "")));
            accData.ToCramt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(trncram)", "")) ?
                        0.00 : dt2.Compute("Sum(trncram)", "")));
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

                if (ASTUtility.Left(mRSIRCODE, 2) != "04")
                    ((TextBox)this.dgv2.Rows[j].FindControl("txtgvDrAmt")).ReadOnly = true;



            }


        }

        private void GetVouCherNumber()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            //DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
            //if (ds2.Tables[0].Rows.Count == 0)
            //{
            //    return;

            //}

            //DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);
            //DateTime curdate = Convert.ToDateTime(this.txtdate.Text.Trim());


            //if (txtopndate > Convert.ToDateTime(this.txtdate.Text.Trim().Substring(0, 11)))
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //    return;

            //}
            string VNo3 = "JV";
            string entrydate = this.txtdate.Text.Substring(0, 11).Trim();
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            string cvno1 = dt4.Rows[0]["couvounum"].ToString().Substring(0, 8);
            this.txtcurrentvou.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
            this.txtCurrntlast6.Text = dt4.Rows[0]["couvounum"].ToString().Substring(8);
            string pvno1 = ds4.Tables[1].Rows[0]["lastvounum"].ToString().Trim();


        }



        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;


            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);


            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = userid;
            string Posttrmid = Terminal;
            string PostSession = Sessionid;

            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string userdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            string EditByid = "";
            string Editdat = "01-Jan-1900";
            string pounaction = "";
            string aprovbyid = "";
            string aprvtrmid = "";
            string aprvseson = "";
            string aprvdat = "01-jan-1900";
            string Payto = "";
            string isunum = "";
            string recndt = "01-Jan-1900";
            string rpcode = "";






            //double todramt=accData.ToDramt;
            //double tocramt=accData.ToCramt;
            // todramt!=tocram
            if (Math.Round(accData.ToDramt) != Math.Round(accData.ToCramt))
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                ((Label)this.Master.FindControl("lblmsg")).Text = "Debit Amount must  be  Equal Credit Amount";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;

            }


            //string comcod =this.GetComeCode();
            //if (this.ibtnvounu.Visible)
            // this.ibtnvounu_Click(null, null);

            string refnum = this.txtRefNum.Text.Trim();
            string srinfo = this.txtSrinfo.Text;
            string vounarration1 = this.txtNarration.Text.Trim();
            string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
            string voutype = "Journal Voucher";
            string cactcode = "000000000000";
            string vtcode = "98";
            string edit = "";
            //Existing Bill

            string Billno = this.ddBillList.SelectedValue.ToString();
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGCONSBILL", Billno, "", "", "", "", "", "", "", "");
            if (ds4.Tables[0].Rows[0]["vounum"].ToString() != "00000000000000")
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher No already Existing in this Bill No";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }





            try
            {

                this.GetVouCherNumber();
                string voudat = this.txtdate.Text.Substring(0, 11);
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                                       this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();

                string CallType = (this.chkpost.Checked) ? "ACVUPDATEUNPOSTED" : "ACVUPDATE02";

                //-----------Update Transaction B Table-----------------//
                bool resultb = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, voudat, refnum, srinfo, vounarration1,
                                vounarration2, voutype, vtcode, edit, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, Payto, isunum, aprovbyid, aprvtrmid, aprvseson, aprvdat, pounaction, "", "", "", "");

                //-----------Update Transaction B Table-----------------//
                //bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo,
                //        vounarration1, vounarration2, voutype, vtcode, edit, userid, Terminal, Sessionid, Postdat, "", "");
                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Visible = true;

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
                    string trnamt = Convert.ToString(Dramt - Cramt);
                    string trnremarks = ((Label)this.dgv2.Rows[i].FindControl("lblBillno")).Text.Trim();
                    string billno = ((Label)this.dgv2.Rows[i].FindControl("lblBillno")).Text.Trim();

                    if (Dramt > 0 || Cramt > 0)
                    {
                        bool resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, actcode, rescode, cactcode,
                               voudat, trnqty, trnremarks, vtcode, trnamt.ToString(), spclcode, recndt, rpcode, "", userid, userdate, Terminal, "", "", "", "", "", "", "", "", "", "");

                        //bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum,
                        //        actcode, rescode, cactcode, voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, "", "", "", "", "");


                        if (!resulta)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }

                        if (billno2 != billno)
                        {
                            string advAmount = Convert.ToDouble("0" + this.txtAdvanced.Text).ToString();
                            resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATECONSSALE",
                                    billno, vounum, advAmount, "", "", "", "", "", "", "", "", "", "", "", "");
                            if (!resulta)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;
                            }
                            billno2 = billno;
                        }
                    }
                }




                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);



                this.lnkFinalUpdate.Enabled = false;
                this.txtcurrentvou.Enabled = false;
                this.txtCurrntlast6.Enabled = false;
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Contractor Bill Update";
                    string eventdesc = "Update Contractor Bill";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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


                case "3332":

                    vouprint = "VocherPrintIns";
                    break;


                case "3101":
                case "3333":
                    vouprint = "VocherPrintMod";
                    break;


                default:
                    vouprint = "VocherPrintMod";
                    break;
            }
            return vouprint;
        }
        private string GetCompInstar()
        {
            string comcod = this.GetComeCode();

            string printinstar = "";
            switch (comcod)
            {
                case "3330":
                    break;

                default:
                    printinstar = "Innstar";
                    break;


            }
            return printinstar;
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
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
                string curvoudat = this.txtdate.Text.Substring(0, 11);
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                        this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
                string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

                string CallType = (this.chkpost.Checked) ? "PRINTUNPOSTEDVOUCHER01" : "PRINTVOUCHER01";
                string PrintInstar = this.GetCompInstar();
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

                if (Type == "VocherPrint")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherDefault", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", ""));

                }
                else if (Type == "VocherPrint1")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher1", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", ""));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));

                }
                else if (Type == "VocherPrint2")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher2", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", ""));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));

                }

                else if (Type == "VocherPrint6")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherBridge", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", ""));


                }




                else if (Type == "VocherPrintMod")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherAlli", list, null, null);
                    Rpt1.EnableExternalImages = true;

                }



                else
                {


                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher3", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", ""));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));

                }

                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                Rpt1.SetParameters(new ReportParameter("InWrd", ASTUtility.Trans(Math.Round(TAmount), 2)));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }




        }



        protected void lbtnoK_Click(object sender, EventArgs e)
        {
            if (this.lbtnoK.Text == "Ok")
            {

                this.lbtnoK.Text = "New";
                string comcod = this.GetComeCode();
                string billno = this.ddBillList.SelectedValue.ToString();
                DataTable tblt01 = (DataTable)ViewState["tblt01"];
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONSTSALINFO", billno,
                              "", "", "", "", "", "", "", "");
                DataTable dt1 = ds1.Tables[0];

                for (int i = 0; i < dt1.Rows.Count; i++)
                {


                    string dgAccCode = dt1.Rows[i]["actcode"].ToString();
                    string dgResCode = dt1.Rows[i]["rescode"].ToString();
                    string dgSpclCode = dt1.Rows[i]["spcode"].ToString();
                    // string billno = dt1.Rows[i]["billid"].ToString();


                    DataRow[] dr2 = tblt01.Select("actcode='" + dgAccCode + "'  and subcode='" + dgResCode + "' and spclcode='" + dgSpclCode + "'  and billno='" + billno + "'");
                    if (dr2.Length > 0)
                    {
                        tblt01.Clear();
                        return;

                    }

                    DataRow dr1 = tblt01.NewRow();
                    dr1["actcode"] = dt1.Rows[i]["actcode"].ToString();
                    dr1["subcode"] = dt1.Rows[i]["rescode"].ToString();
                    dr1["spclcode"] = dt1.Rows[i]["spcode"].ToString();
                    dr1["actdesc"] = dt1.Rows[i]["actdesc"].ToString(); // dgAccCode + "-" + dgAccDesc;
                    dr1["subdesc"] = dt1.Rows[i]["resdesc"].ToString();  // dgResCode + "-" + dgResDesc;
                    dr1["spcldesc"] = "";

                    dr1["trnqty"] = Convert.ToDouble(dt1.Rows[i]["trnqty"]);
                    dr1["trnrate"] = Convert.ToDouble(dt1.Rows[i]["trnrate"]);
                    dr1["trndram"] = Convert.ToDouble(dt1.Rows[i]["dr"]);
                    dr1["trncram"] = Convert.ToDouble(dt1.Rows[i]["cr"]);
                    dr1["billno"] = dt1.Rows[i]["billno"].ToString();
                    tblt01.Rows.Add(dr1);
                }


                this.lnkFinalUpdate.Enabled = true;
                if (tblt01.Rows.Count == 0)
                    return;
                ViewState["tblt01"] = HiddenSameData(tblt01);
                this.Data_Bind();
                this.ddBillList.Enabled = false;
                this.Panel1.Visible = true;
                return;
            }


            this.lbtnoK.Text = "Ok";
            this.txtRefNum.Text = "";
            this.txtSrinfo.Text = "";
            this.txtNarration.Text = "";
            ViewState.Remove("tblt01");
            this.CreateTable();
            this.LoadBillList();
            this.dgv2.DataSource = null;
            this.dgv2.DataBind();
            this.ddBillList.Enabled = true;
            this.txtCurrntlast6.ReadOnly = true;
            this.Panel1.Visible = false;
        }



        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }
        private void SaveValue()
        {
            //DataTable dt1 = (DataTable)ViewState["tblt01"];
            //double todramt = 0, tocramt = 0;
            //int TblRowIndex2;
            //string billno = dt1.Rows[0]["billno"].ToString();
            //for (int j = 0; j < this.dgv2.Rows.Count; j++)
            //{

            //    string billno1 = ((Label)this.dgv2.Rows[j].FindControl("lblBillno")).Text.Trim();
            //    string actcode = ((Label)this.dgv2.Rows[j].FindControl("lblAccCod")).Text.Trim();
            //    double dramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvDrAmt")).Text.Trim()));
            //    // string spclcode = ((DropDownList)this.dgv2.Rows[j].FindControl("ddlspcfdesc")).SelectedValue.ToString();
            //    //string spcldesc = ((DropDownList)this.dgv2.Rows[j].FindControl("ddlspcfdesc")).SelectedItem.Text;
            //    if (billno1 != billno)
            //    {
            //        todramt = 0; tocramt = 0;

            //    }

            //    todramt = todramt + dramt;
            //    TblRowIndex2 = (this.dgv2.PageIndex) * this.dgv2.PageSize + j;
            //    if (ASTUtility.Left(actcode, 2) == "18")
            //    {
            //        dt1.Rows[TblRowIndex2]["trncram"] = todramt - tocramt;
            //        todramt = 0; tocramt = 0;
            //        continue;
            //    }

            //    double cramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvCrAmt")).Text.Trim()));
            //    tocramt = tocramt + cramt;

            //    dt1.Rows[TblRowIndex2]["trncram"] = cramt;
            //    dt1.Rows[TblRowIndex2]["trndram"] = dramt;
            //    billno = billno1;

            //}
            //ViewState["tblt01"] = dt1;
            //this.Data_Bind();








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

        protected void lbtnSelect_Click(object sender, EventArgs e)
        {

        }
    }
}