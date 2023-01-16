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
    public partial class AccConBillUpdate : System.Web.UI.Page
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Contractor Bill Update";
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                this.CreateTable();
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetConAndBill();
                //this.LoadBillList();
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
                case "3370":
               // case "3101":

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

        private string CompBillConFirmed()
        {

            string comcod = this.GetComeCode();
            string BillConfirmed = "";
            switch (comcod)
            {
                case "3332":
                case "3338": //ACME
                case "3340": //Urban
                case "2305":
                case "3305":
                case "3306":
                case "3311":

                    BillConfirmed = "BillConfirmed";
                    break;

                default:


                    break;

            }

            return BillConfirmed;
        }

        private void GetConAndBill()
        {
            Session.Remove("narration");
            string comcod = this.GetComeCode();
            string txtSearchBill = (this.Request.QueryString["genno"].ToString()).Length == 0 ? "%" + this.txtBillno.Text.Trim() + "%" : this.Request.QueryString["genno"].ToString() + "%";
            string date = (this.Request.QueryString["Date1"].ToString()).Length == 0 ? Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy") : this.Request.QueryString["Date1"].ToString();

            string BillConfirmed = this.CompBillConFirmed();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCBILLLIST", txtSearchBill, date, BillConfirmed, "", "", "", "", "", "");

            Session["narration"] = ds1.Tables[0];
            this.ddlConList.DataTextField = "csirdesc";
            this.ddlConList.DataValueField = "csircode";
            this.ddlConList.DataSource = ds1.Tables[1];
            this.ddlConList.DataBind();
            ds1.Dispose();
            this.LoadBillList();
        }
        private void LoadBillList()
        {


            string csircode = this.ddlConList.SelectedValue.ToString();
            DataTable dt = ((DataTable)Session["narration"]).Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("csircode='" + csircode + "'");
            dt = dv.ToTable();
            this.DropCheck1.Items.Clear();
            this.DropCheck1.DataTextField = "textfield";
            this.DropCheck1.DataValueField = "billno";
            this.DropCheck1.DataSource = dt;
            this.DropCheck1.DataBind();

            if (this.Request.QueryString["genno"].Length > 0)
            {
                //this.DropCheck1.Text = this.Request.QueryString["genno"].ToString();
                this.DropCheck1.SelectedValue = this.Request.QueryString["genno"].ToString();
            }



            //string comcod =this.GetComeCode();
            //   string txtSearchBill = (this.Request.QueryString["genno"].ToString()).Length == 0 ? "%" + this.txtBillno.Text.Trim() + "%" : this.Request.QueryString["genno"].ToString() + "%";
            //   string date = (this.Request.QueryString["Date1"].ToString()).Length == 0 ?  Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy") : this.Request.QueryString["Date1"].ToString() ;



            //string BillConfirmed = this.CompBillConFirmed();
            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCBILLLIST", txtSearchBill, date, BillConfirmed, "", "", "", "", "", "");
            //this.DropCheck1.Items.Clear();
            //this.DropCheck1.DataTextField = "textfield";
            //this.DropCheck1.DataValueField = "billno";
            //Session["narration"] = ds1.Tables[0];
            //this.DropCheck1.DataSource = ds1.Tables[0];
            //this.DropCheck1.DataBind();
        }

        private void Data_Bind()
        {
            dgv2.DataSource = (DataTable)ViewState["tblt01"];
            dgv2.DataBind();
            // this.GridColoumnVisible();
            this.calculation();
            //this.Narration();
        }

        private void GetNarration()
        {





            DataTable dt = ((DataTable)Session["narration"]).Copy();

            string narration = "";

            //string[] billno = this.DropCheck1.Text.Trim().Split(',');
            DataView dv = dt.DefaultView;
            foreach (ListItem billno1 in DropCheck1.Items)
            {
                if (billno1.Selected)
                {
                    dv.RowFilter = "billno='" + billno1.Value + "'";
                    dt = dv.ToTable();

                    narration = narration + dt.Rows[0]["textfield1"].ToString() + "; ";
                }
            }
            narration = narration.Length > 0 ? (narration.Substring(0, ((narration.Length) - 2))) : "";

            this.txtNarration.Text = narration;





            //DataTable dt = (DataTable)Session["narration"];
            //string ddlbillno = this.ddlBillLIst.SelectedValue.ToString();
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = "billno='" + ddlbillno + "'";
            //dt = dv.ToTable();
            //this.txtNarration.Text = dt.Rows[0]["textfield1"].ToString();

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


            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return;

            }

            DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);
            DateTime curdate = Convert.ToDateTime(this.txtdate.Text.Trim());


            if (txtopndate > Convert.ToDateTime(this.txtdate.Text.Trim().Substring(0, 11)))
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

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

            SaveValue();

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
            //string pounaction = "";
            string pounaction = ((this.chkpost.Checked) ? "U" : "");
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

            //string[] billnoa = this.DropCheck1.Text.Trim().Split(',');



            foreach (ListItem Billno in DropCheck1.Items)
            {
                if (Billno.Selected)
                {
                    DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGCBILL", Billno.Value, "", "", "", "", "", "", "", "");
                    if (ds4.Tables[0].Rows[0]["vounum"].ToString() != "00000000000000")
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                        ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher No already Existing in this Bill No";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                }

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
                            resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATECONBILL",
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

                string curvoudat = this.txtdate.Text.Substring(0, 11);
                string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                        this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();

                string paytype = "0";
                // hlink1.NavigateUrl = "~/F_17_Acc/GeneralAccounts.aspx?Mod=Management&vounum=" + vounum;
                // hlnkPrintVoucher.NavigateUrl = "~/F_17_Acc/AccPrint.aspx?Type=accVou&vounum=" + vounum;

                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccPrint.aspx?Type=accVou&vounum=" + vounum + "&paytype=" + paytype
                           + "', target='_blank');</script>";


                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = hst["comcod"].ToString();
                //string comnam = hst["comnam"].ToString();
                //string comadd = hst["comadd1"].ToString();
                //string compname = hst["compname"].ToString();
                //string username = hst["username"].ToString();
                //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                //string curvoudat = this.txtdate.Text.Substring(0, 11);
                //string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                //        this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();

                //string CallType = (this.chkpost.Checked) ? "PRINTUNPOSTEDVOUCHER01" : "PRINTVOUCHER01";
                //string PrintInstar = this.GetCompInstar();
                //DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", CallType, vounum, PrintInstar, "", "", "", "", "", "", "");
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

                //DataTable dt1 = _ReportDataSet.Tables[1];
                //string Vounum = dt1.Rows[0]["vounum"].ToString();
                //string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                //string refnum = dt1.Rows[0]["refnum"].ToString();
                //string voutype = dt1.Rows[0]["voutyp"].ToString();
                //string venar = dt1.Rows[0]["venar"].ToString();
                //string Posteddat = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
                //string Type = this.CompanyPrintVou();
                //ReportDocument rptinfo = new ReportDocument();
                //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();
                //if (Type == "VocherPrint")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text = "";// (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //}
                //else if (Type == "VocherPrint1")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher1();
                //    TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate1.Text = "Entry Date: " + Posteddat;
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text = "";// (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();

                //}
                //else if (Type == "VocherPrint2")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher2();
                //    TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate2"] as TextObject;
                //    txtPosteddate2.Text = "Entry Date: " + Posteddat;
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text = "";// (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //}

                //else if (Type == "VocherPrint6")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucherBridge();
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text = "";// (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();


                //}
                //else if (Type == "VocherPrintIns")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherIns();
                //}




                //else if (Type == "VocherPrintMod")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli();

                //}



                //else
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher3();
                //    TextObject txtPosteddate3 = rptinfo.ReportDefinition.ReportObjects["entrydate3"] as TextObject;
                //    txtPosteddate3.Text = "Entry Date: " + Posteddat;
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text = "";// (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                //}


                //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //txtCompanyName.Text = comnam;
                //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                //txtcAdd.Text = comadd;
                //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //vounum1.Text = "Voucher No.: " + vounum;
                //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //date.Text = "Voucher Date: " + voudat;
                ////TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                ////Refnum.Text = "Cheque/Ref. No.: " + refnum;

                //TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
                //voutype1.Text = voutype;
                //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //naration.Text = "Narration: " + venar;

                //TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                //rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);

                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                //if (ConstantInfo.LogStatus == true)
                //{
                //    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
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
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }




        }

        private string ComCallType()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string CallType = "";
            switch (comcod)
            {

                case "3330":// Bridge
                            //case "3101":// Bridge

                    CallType = "GETBRACCBILLINFO";
                    break;



                //case "3336":

                case "3339":// Tropical
                case "1101":
                case "1102":
                case "1103":
                case "3338":// ACME
                case "1206": 
                case "1207":
                case "3369":


                    CallType = "GETACMEACCBILLINFO";
                    break;

                case "3101":// own
                case "3348":// credence
                    CallType = "GETCREDENCEACCBILLINFO";
                    break;



                default:
                    CallType = "GETACCBILLINFO";
                    break;
            }
            return CallType;
        }

        protected void lbtnoK_Click(object sender, EventArgs e)
        {
            if (this.lbtnoK.Text == "Ok")
            {

                this.lbtnoK.Text = "New";
                string comcod = this.GetComeCode();
                //string[] billnoa = this.DropCheck1.Text.Trim().Split(',');
                DataTable tblt01 = (DataTable)ViewState["tblt01"];
                foreach (ListItem billno in DropCheck1.Items)
                {
                    if (billno.Selected)
                    {

                        string billno1 = billno.Value.ToString();
                        string CallType = this.ComCallType();
                        DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, billno1,
                                      "", "", "", "", "", "", "", "");
                        DataTable dt1 = ds1.Tables[0];

                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {


                            string dgAccCode = dt1.Rows[i]["actcode"].ToString();
                            string dgResCode = dt1.Rows[i]["rescode"].ToString();
                            string dgSpclCode = dt1.Rows[i]["spcode"].ToString();
                            // string billno = dt1.Rows[i]["billid"].ToString();


                            DataRow[] dr2 = tblt01.Select("actcode='" + dgAccCode + "'  and subcode='" + dgResCode + "' and spclcode='" + dgSpclCode + "'  and billno='" + billno1 + "'");
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
                    }
                }

                //string Narration="";
                //for (int i = 0; i < dtbill.Rows.Count; i++)         
                //    Narration = Narration + dtbill.Rows[i]["cbillref"].ToString() + ", " + dtbill.Rows[i]["lisurefno"].ToString();

                //this.txtNarration.Text = Narration;
                this.lnkFinalUpdate.Enabled = true;
                if (tblt01.Rows.Count == 0)
                    return;
                ViewState["tblt01"] = HiddenSameData(tblt01);
                this.Data_Bind();
                this.DropCheck1.Enabled = false;

                //this.ibtnvounu.Visible = true;
                this.Panel1.Visible = true;
                string pactcode = "26" + ASTUtility.Right(tblt01.Rows[0]["actcode"].ToString(), 10);
                this.SupplierOverallAdvanced(pactcode);
                this.GetAdvanced();

                this.GetNarration(); //get narration
                return;
            }


            this.lbtnoK.Text = "Ok";
            this.txtRefNum.Text = "";
            this.txtSrinfo.Text = "";
            this.txtNarration.Text = "";
            ViewState.Remove("tblt01");
            this.CreateTable();
            this.GetConAndBill();
            this.dgv2.DataSource = null;
            this.dgv2.DataBind();
            this.DropCheck1.Enabled = true;
            this.txtCurrntlast6.ReadOnly = true;
            this.Panel1.Visible = false;
        }
        private void GetAdvanced()
        {

            string billno = this.DropCheck1.SelectedValue;
            if (billno.Length > 0)
            {

                string billnoin = "";
                double advamt = 0.00;
                //string[] billnoa = this.DropCheck1.Text.Trim().Split(',');

                foreach (ListItem billno1 in DropCheck1.Items)
                {
                    if (billno1.Selected)
                    {
                        billnoin = billno1.Value.Substring(0, 14);
                        advamt = advamt + Convert.ToDouble(((DataTable)Session["narration"]).Select("billno='" + billnoin + "'")[0]["advamt"]);
                    }

                }

                this.txtAdvanced.Text = advamt.ToString("#,##0.00;(#,##0.00); ");
            }







        }
        private void SupplierOverallAdvanced(string pactcode)
        {
            string billno = this.DropCheck1.SelectedValue;
            string csircode = ((DataTable)Session["narration"]).Select("billno='" + billno + "'").Length == 0 ? "000000000000" : ((DataTable)Session["narration"]).Select("billno='" + billno + "'")[0]["csircode"].ToString();
            //string csircode = ((DataTable)Session["narration"]).Select("billno='" + billno + "'")[0]["csircode"].ToString();
            string comcod = this.GetComeCode();
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string frmdate = "01" + date.Substring(2);
            string spclcode = "%";

            //SP_REPORT_ACCOUNTS_LG '3101', 'ACCOUNTSLEDGERSUB', '410100010011', '01-Jan-2012', '31-Dec-2013', '990100101002'
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGERSUB", pactcode, frmdate, date, csircode, "", "", "", "", spclcode);

            this.lbtnBalance.Text = "Balance :" + Convert.ToDouble(ds1.Tables[2].Rows[0]["balam"]).ToString("#,##0.00;(#,##0.00);");
            lbtnBalance.NavigateUrl = this.ResolveUrl("~/F_17_Acc/AccLedger.aspx?Type=SubLedger&prjcode=" + pactcode + "&sircode=" + csircode + "");




            // lbtnBalance.NavigateUrl = "~/F_17_Acc/AccPurchase.aspx?Type=Entry&genno=" + billno + "&ssircode=" + ssircode + "&Date1=" + Date1;


        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }
        private void SaveValue()
        {
            DataTable dt1 = (DataTable)ViewState["tblt01"];
            double todramt = 0, tocramt = 0;
            int TblRowIndex2;
            string billno = dt1.Rows[0]["billno"].ToString();
            for (int j = 0; j < this.dgv2.Rows.Count; j++)
            {

                string billno1 = ((Label)this.dgv2.Rows[j].FindControl("lblBillno")).Text.Trim();
                string Supplier = ((Label)this.dgv2.Rows[j].FindControl("lblResCod")).Text.Trim();
                string spcfcod = ((Label)this.dgv2.Rows[j].FindControl("lblSpclCod")).Text.Trim();
                double dramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvDrAmt")).Text.Trim()));
                // string spclcode = ((DropDownList)this.dgv2.Rows[j].FindControl("ddlspcfdesc")).SelectedValue.ToString();
                //string spcldesc = ((DropDownList)this.dgv2.Rows[j].FindControl("ddlspcfdesc")).SelectedItem.Text;
                if (billno1 != billno)
                {
                    todramt = 0; tocramt = 0;

                }

                todramt = todramt + dramt;
                TblRowIndex2 = (this.dgv2.PageIndex) * this.dgv2.PageSize + j;
                if (ASTUtility.Left(Supplier, 2) == "98" && spcfcod == "000000000000")
                {
                    dt1.Rows[TblRowIndex2]["trncram"] = todramt - tocramt;
                    todramt = 0; tocramt = 0;
                    continue;
                }

                double cramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvCrAmt")).Text.Trim()));
                tocramt = tocramt + cramt;

                dt1.Rows[TblRowIndex2]["trncram"] = cramt;
                dt1.Rows[TblRowIndex2]["trndram"] = dramt;
                billno = billno1;

            }
            ViewState["tblt01"] = dt1;
            this.Data_Bind();








            //DataTable dt1 = (DataTable)ViewState["tblt01"];
            //double todramt = 0, tocramt = 0;
            //int rowindex;
            //for (int j = 0; j < this.dgv2.Rows.Count; j++)
            //{
            //    string Contractor = ((Label)this.dgv2.Rows[j].FindControl("lblResCod")).Text.Trim();
            //    double dramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvDrAmt")).Text.Trim()));
            //    todramt = todramt + dramt;
            //    rowindex = (this.dgv2.PageIndex) * this.dgv2.PageSize + j;
            //    if (ASTUtility.Left(Contractor, 2) == "98")
            //    {
            //        dt1.Rows[rowindex]["trncram"] = todramt - tocramt;
            //        todramt = 0; tocramt = 0;
            //        continue;
            //    }

            //    double cramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvCrAmt")).Text.Trim()));
            //    tocramt = tocramt + cramt;

            //    dt1.Rows[rowindex]["trncram"] = cramt;
            //    dt1.Rows[rowindex]["trndram"] = dramt;

            //}
            //ViewState["tblt01"] = dt1;
            //this.Data_Bind();



            //ViewState["tblt01"] = dt;
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


        protected void ImgbtnFindCon_Click(object sender, EventArgs e)
        {
            this.GetConAndBill();

        }
        protected void ddlConList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadBillList();

        }
    }
}