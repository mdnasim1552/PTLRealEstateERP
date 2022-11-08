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
using RealERPRDLC;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_17_Acc
{
    public partial class AccPurchase : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        public static double TAmount;
        public static bool isTotal = false;
        protected void Page_Load(object sender, EventArgs e)
        {

            //dgv1.Attributes.Add("onClick",
            //         " javascript:return confirm('Are You sure you want to input the record?');");

            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Purchase Update";
                this.Master.Page.Title = "Purchase Update";
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetSpecification();
                // this.LoadBillCombo();
                // CreateTable();
                this.CompanyPost();

            }

        }

        private void GetSpecification()
        {
            try
            {
                ViewState.Remove("tblspecific");
                string comcod = this.GetCompCode();
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETPURSPECIFICATION", "%%", "", "", "", "", "", "", "", "");
                ViewState["tblspecific"] = ds1.Tables[0];
            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }



        }


        private void CompanyPost()
        {


            string comcod = this.GetCompCode();

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
        private void CreateTable()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("actcode", Type.GetType("System.String"));
            tblt01.Columns.Add("subcode", Type.GetType("System.String"));
            tblt01.Columns.Add("spclcode", Type.GetType("System.String"));
            tblt01.Columns.Add("actdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("subdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("spcldesc", Type.GetType("System.String"));
            tblt01.Columns.Add("trnqty", Type.GetType("System.Decimal"));
            tblt01.Columns.Add("trnrate", Type.GetType("System.Decimal"));
            tblt01.Columns.Add("trndram", Type.GetType("System.Decimal"));
            tblt01.Columns.Add("trncram", Type.GetType("System.Decimal"));
            tblt01.Columns.Add("trnrmrk", Type.GetType("System.String"));
            tblt01.Columns.Add("billno", Type.GetType("System.String"));
            tblt01.Columns.Add("billnar", Type.GetType("System.String"));
            Session["tblt01"] = tblt01;



        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private string GetCompCallType()
        {

            string comcod = this.GetCompCode();
            string CallType = "";
            switch (comcod)
            {
                case "1301":
                case "2301":
                case "3301":
                    CallType = "GETBILlACCSAN";
                    break;


                default:
                    CallType = "GETBILlACC";
                    break;




            }

            return CallType;

        }

        private void GetConAndBill()
        {

            Session.Remove("narration");
            string comcod = this.GetCompCode();
            string Billno = (this.Request.QueryString["genno"].ToString()).Length == 0 ? ("%" + this.txtBillno.Text.Trim() + "%") : (this.Request.QueryString["genno"].ToString() + "%");
            // string Billno ="%" +this.txtBillno.Text.Trim()+"%";
            string date = this.txtdate.Text.Substring(0, 11);
            string CalType = this.GetCompCallType();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CalType, Billno, date, "", "", "", "", "", "", "");
            Session["narration"] = ds1.Tables[0];
            this.ddlSupList.DataTextField = "ssirdesc";
            this.ddlSupList.DataValueField = "ssircode";
            this.ddlSupList.DataSource = ds1.Tables[1];
            this.ddlSupList.DataBind();
            ds1.Dispose();
            this.LoadBillCombo();



        }
        private void LoadBillCombo()
        {

            string ssircode = this.ddlSupList.SelectedValue.ToString();
            DataTable dt = ((DataTable)Session["narration"]).Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("ssircode='" + ssircode + "'");
            dt = dv.ToTable();
            this.DropCheck1.Items.Clear();
            this.DropCheck1.DataTextField = "textfield";
            this.DropCheck1.DataValueField = "billno";
            this.DropCheck1.DataSource = dt;
            this.DropCheck1.DataBind();

            if (this.Request.QueryString["genno"].Length > 0)
            {
                this.DropCheck1.Text = this.Request.QueryString["genno"].ToString();
            }
            // this.GetAdvanced();
        }


        private void GetAdvanced()
        {

            string billno = this.DropCheck1.Text;
            if (billno.Length > 0)
            {

                string billnoin = "";
                double advamt = 0.00;
                string[] billnoa = this.DropCheck1.Text.Trim().Split(',');

                foreach (string billno1 in billnoa)
                {
                    billnoin = billno1.Substring(0, 14);
                    advamt = advamt + Convert.ToDouble(((DataTable)Session["narration"]).Select("billno='" + billnoin + "'")[0]["advamt"]);
                }

                this.txtAdvanced.Text = advamt.ToString("#,##0.00;(#,##0.00); ");
            }



        }


        private void calculation()
        {
            DataTable dt2 = (DataTable)Session["tblt01"];
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

            this.GetConAndBill();

            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.pnlBill.Visible = true;
                this.PnlNarration.Visible = true;
                this.GetPreNarration();

                Session.Remove("tblt01");
                this.CreateTable();

                return;
            }
            this.lbtnOk.Text = "Ok";
            this.pnlBill.Visible = false;
            this.PnlNarration.Visible = false;

            //ViewState.Remove("tblt01");
            //this.CreateTable();
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;

            this.txtRefNum.Text = "";
            this.txtSrinfo.Text = "";
            this.txtNarration.Text = "";
            this.txtcurrentvou.Text = "";
            this.txtCurrntlast6.Text = "";
            Session.Remove("tblt01");

            this.lnkFinalUpdate.Enabled = true;
            this.dgv2.DataSource = null;
            this.dgv2.DataBind();
        }


        private void GetPreNarration()
        {

            // Previous Nerration
            string comcod = this.GetCompCode();
            string VNo3 = "JV";
            string date = this.txtdate.Text;
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "LASTNARRATION", VNo3, date, "", "", "", "", "", "", "");
            if (ds4.Tables[0].Rows.Count == 0)
                this.txtNarration.Text = "";
            else
                this.txtNarration.Text = ds4.Tables[0].Rows[0]["vernar"].ToString();
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

                //if (txtopndate==Convert.ToDateTime(this.txtdate.Text.Trim().Substring(0, 11)))
                //    ;
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
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);


            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            //for cr dr amount check (without click total button)
            lbtnTotal_Click(null, null);
            //this.calculation();
            //end nahid 


            if (Math.Round(accData.ToDramt) != Math.Round(accData.ToCramt))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Debit Amount must be Equal Credit Amount";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string userid = hst["usrid"].ToString();
            //string Terminal = hst["compname"].ToString();
            //string Sessionid = hst["session"].ToString();
            //string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

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
            string pounaction =  ((this.chkpost.Checked) ? "U" : "");
            string aprovbyid = "";
            string aprvtrmid = "";
            string aprvseson = "";
            string aprvdat = "01-jan-1900";
            //this.txtPayto.Text.Trim() == ""
            string Payto = txtPayto.Text.Trim();
            string isunum = "";
            string recndt = "01-Jan-1900";
            string rpcode = "";






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
                    ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGPURBILL", billno, "", "", "", "", "", "", "", "");

                else if (((Label)this.dgv2.Rows[i - 1].FindControl("lblBillno")).Text.Trim() == billno)
                    continue;

                else
                    ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGPURBILL", billno, "", "", "", "", "", "", "", "");


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


                string CallType = (this.chkpost.Checked) ? "ACVUPDATEUNPOSTED" : "ACVUPDATE02";

                //-----------Update Transaction B Table-----------------//
                bool resultb = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, voudat, refnum, srinfo, vounarration1,
                                vounarration2, voutype, vtcode, edit, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, Payto, isunum, aprovbyid, aprvtrmid, aprvseson, aprvdat, pounaction, "", "", "", "");
                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }



                ////-----------Update Transaction B Table-----------------//
                //bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo,
                //        vounarration1, vounarration2, voutype, vtcode, edit, userid, Terminal, Sessionid, Postdat, "", "");
                //if (!resultb)
                //{
                // ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                //    return;
                //}
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



                        bool resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum, actcode, rescode, cactcode,
                              voudat, trnqty, trnremarks, vtcode, trnamt.ToString(), spclcode, recndt, rpcode, "", userid, userdate, Terminal, "", "", "", "", "", "", "", "", "", "");

                        //bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum,
                        //        actcode, rescode, cactcode, voudat, trnqty, trnremarks, vtcode, trnamt.ToString(), spclcode, "", "", "", "", "");
                        if (!resulta)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }

                        if (billno2 != billno)
                        {




                            string advAmount = Convert.ToDouble("0" + this.txtAdvanced.Text).ToString();
                            resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEPURBILL",
                                    billno, vounum, advAmount, "", "", "", "", "", "", "", "", "", "", "", "");
                            if (!resulta)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;
                            }
                            billno2 = billno;
                        }
                    }
                }



                //Advanced 
                // string actcode=
                double advamt = Convert.ToDouble(((DataTable)Session["tblt01"]).Select("actcode like '13%'")[0]["trncram"]);
                string vbillno = ((DataTable)Session["tblt01"]).Select("actcode like '13%'")[0]["billno"].ToString();
                if (advamt > 0)
                {
                    bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEPURBILL",
                                        vbillno, vounum, advamt.ToString(), "", "", "", "", "", "", "", "", "", "", "", "");
                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                }


                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
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
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            if (this.chkBill.Checked)
            {
                this.BillPrint();

            }
            else
            {

                this.PrintVoucher();
            }


        }

        private void BillPrint()
        {
            try
            {



                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string comnam = hst["comnam"].ToString();




                var lst = ASITUtility03.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.EClassSupplierBill>((DataTable)Session["tblt01"]);

                string billno = this.DropCheck1.Text;
                DataRow[] dr1 = ((DataTable)Session["narration"]).Select("billno='" + billno + "'");

                string billdate = Convert.ToDateTime(dr1[0]["billdat"]).ToString("dd-MMM-yyyy");
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETPURCHASESIGN", billno, "",
                              "", "", "", "", "", "", "");

                // string billdate=Convert.ToDateTime(dr1[0]["billdate"]).ToString("dd-MMM-yyyy");



                ///-------------------------Sign Part--------------------------///
                //TextObject rpttxtReq = rptstk.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                //rpttxtReq.Text = ds1.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqdat"].ToString();
                //TextObject rpttxtReqApp = rptstk.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                //rpttxtReqApp.Text = ds1.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqadat"].ToString();
                //TextObject rpttxtOrd = rptstk.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                //rpttxtOrd.Text = ds1.Tables[3].Rows[0]["appnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["appdat"].ToString();
                //TextObject rpttxtWord = rptstk.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                //rpttxtWord.Text = ds1.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["orddat"].ToString();
                //TextObject rpttxtRec = rptstk.ReportDefinition.ReportObjects["txtRec"] as TextObject;
                //rpttxtRec.Text = ds1.Tables[3].Rows[0]["mrrnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["mrrdat"].ToString();
                //TextObject rpttxtBill = rptstk.ReportDefinition.ReportObjects["txtBill"] as TextObject;
                //rpttxtBill.Text = ds1.Tables[3].Rows[0]["billnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["billdat"].ToString();


                LocalReport rpt1 = new LocalReport();
                Hashtable hshtbl = new Hashtable();
                hshtbl["companyname"] = comnam;
                hshtbl["billno"] = "Bill No :" + billno;
                hshtbl["billdate"] = "Bill Date :" + billdate;
                hshtbl["reqnam"] = ds1.Tables[0].Rows[0]["reqnam"].ToString() + "\n" + ds1.Tables[0].Rows[0]["reqdat"].ToString();
                hshtbl["reqanam"] = ds1.Tables[0].Rows[0]["reqanam"].ToString() + "\n" + ds1.Tables[0].Rows[0]["reqadat"].ToString(); ;
                hshtbl["appnam"] = ds1.Tables[0].Rows[0]["appnam"].ToString() + "\n" + ds1.Tables[0].Rows[0]["appdat"].ToString(); ;
                hshtbl["ordnam"] = ds1.Tables[0].Rows[0]["ordnam"].ToString() + "\n" + ds1.Tables[0].Rows[0]["orddat"].ToString(); ;
                hshtbl["mrrnam"] = ds1.Tables[0].Rows[0]["mrrnam"].ToString() + "\n" + ds1.Tables[0].Rows[0]["mrrdat"].ToString(); ;
                hshtbl["billnam"] = ds1.Tables[0].Rows[0]["billnam"].ToString() + "\n" + ds1.Tables[0].Rows[0]["billdat"].ToString(); ;

                rpt1 = RDLCAccountSetup.GetLocalReport("R_17_Acc.rptPrintSubBill", hshtbl, lst, null);


                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




            }
            catch (Exception ex)
            {

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

            string comcod = this.GetCompCode();
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
        private void PrintVoucher()
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
                ////string usrname = hst["usrname"].ToString();
                //string comcod = hst["comcod"].ToString();
                //string comnam = hst["comnam"].ToString();
                //string comadd = hst["comadd1"].ToString();
                //string combranch = hst["combranch"].ToString();
                //string compname = hst["compname"].ToString();
                //string username = hst["username"].ToString();
                //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                //string curvoudat = this.txtdate.Text.Substring(0, 11);
                //string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                //        this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();






                //string PrintInstar = this.GetCompInstar();





                //string CallType = (this.chkpost.Checked) ? "PRINTUNPOSTEDVOUCHER01" : "PRINTVOUCHER01";



                ////string vounum = this.ddlPrivousVou.SelectedValue.ToString();

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
                //if (Type == "VocherPrint")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text ="";
                //    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //    naration.Text = "Narration: " + venar;
                //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //    vounum1.Text = "Voucher No.: " + vounum;
                //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //    date.Text = "Voucher Date: " + voudat;
                //}
                //else if (Type == "VocherPrint1")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher1();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum1.Text ="";
                //    TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate1.Text = "Entry Date: " + Posteddat;
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text ="";
                //    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //    naration.Text = "Narration: " + venar;
                //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //    vounum1.Text = "Voucher No.: " + vounum;
                //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //    date.Text = "Voucher Date: " + voudat;

                //}
                //else if (Type == "VocherPrint2")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher2();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum2.Text ="";
                //    TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate2.Text = "Entry Date: " + Posteddat;
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text = "";
                //    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //    naration.Text = "Narration: " + venar;
                //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //    vounum1.Text = "Voucher No.: " + vounum;
                //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //    date.Text = "Voucher Date: " + voudat;
                //}
                //else if (Type == "VocherPrint3")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher3();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum1.Text = "";
                //    TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate1.Text = "Entry Date: " + Posteddat;
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text = "";
                //    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //    naration.Text = "Narration: " + venar;
                //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //    vounum1.Text = "Voucher No.: " + vounum;
                //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //    date.Text = "Voucher Date: " + voudat;

                //}
                //else if (Type == "VocherPrint5")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher5();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum1.Text ="";
                //    TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate1.Text = "Entry Date: " + Posteddat;
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text ="";
                //    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //    naration.Text = "Narration: " + venar;

                //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //    vounum1.Text = "Voucher No.: " + vounum;
                //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //    date.Text = "Voucher Date: " + voudat;
                //}
                //else if (Type == "VocherPrint6")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucherBridge();
                //    TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //    Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //    TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //    rpttxtPartyName.Text = "";
                //    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //    naration.Text = "Narration: " + venar;
                //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //    vounum1.Text = "Voucher No.: " + vounum;
                //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //    date.Text = "Voucher Date: " + voudat;


                //}

                //else if (Type == "VocherPrintIns")
                //{

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

                //}



                //else if (Type == "VocherPrintMod")
                //{

                //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli();

                //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //        vounum1.Text = "Voucher No.: " + vounum;
                //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //        date.Text = "Voucher Date: " + voudat;
                //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //        naration.Text = "Narration: " + venar;
                //}

                //else
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher4();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";

                //    TextObject txtPosteddate3 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate3.Text = "Entry Date: " + Posteddat;

                //    TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //    naration.Text = "Narration: " + venar;

                //    TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //    vounum1.Text = "Voucher No.: " + vounum;
                //    TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //    date.Text = "Voucher Date: " + voudat;
                //}


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
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        private string CompanyPurchase()
        {

            String excludetax = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3332":
                    // case "3101":
                    //   excludetax = "excluede";
                    break;

                default:
                    excludetax = "";
                    break;



            }

            return excludetax;

        }

        private string CompanyAdvancedAdjust()
        {

            string advadj = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3330": // Bridge
                             //case "3101":
                    advadj = "advancedadj";
                    break;

                default:
                    advadj = "";
                    break;



            }

            return advadj;

        }


        private string CompanyAdvancedCode()
        {

            string advcode = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3340": // Urban

                    advcode = "advancedadj";
                    break;

                default:
                    advcode = "";
                    break;



            }

            return advcode;

        }
        protected void lbtnSelectBill_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string ExcludeTax = this.CompanyPurchase();
            string advadj = this.CompanyAdvancedAdjust();
            string advcode = this.CompanyAdvancedCode();
            //string billid = this.ddlBillList.SelectedValue.ToString();
            string billid = "";
            string[] billno = this.DropCheck1.Text.Trim().Split(',');
            DataTable tblt01 = (DataTable)Session["tblt01"];
            foreach (string billno1 in billno)
            {
                billid = billno1.Substring(0, 14);

                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETACCPURCHASES", billid,
                              ExcludeTax, advadj, advcode, "", "", "", "", "");
                DataTable dt1 = ds1.Tables[0];

                DataRow[] dr2 = tblt01.Select("billno='" + billid + "'");

                if (dr2.Length == 0)
                    tblt01.Merge(dt1);

                //for (int i = 0; i < dt1.Rows.Count; i++)
                //{
                //    string dgAccCode = dt1.Rows[i]["actcode"].ToString();
                //    string dgResCode = dt1.Rows[i]["rescode"].ToString();
                //    string dgSpclCode = dt1.Rows[i]["spcode"].ToString();
                //    string dgAccDesc = dt1.Rows[i]["actdesc"].ToString();
                //    string dgResDesc = dt1.Rows[i]["resdesc"].ToString();
                //    string dgSpclDesc = dt1.Rows[i]["spcfdesc"].ToString();
                //    double dgTrnrate = 0.00;
                //    double dgTrnQty = Convert.ToDouble(dt1.Rows[i]["billqty"]);
                //    if (Convert.ToDouble(dt1.Rows[i]["billqty"]) > 0)
                //    {
                //        dgTrnrate = Convert.ToDouble(dt1.Rows[i]["Dr"]) / Convert.ToDouble(dt1.Rows[i]["billqty"]);
                //    }

                //    double dgTrnDrAmt = Convert.ToDouble(dt1.Rows[i]["Dr"]);
                //    double dgTrnCrAmt = Convert.ToDouble(dt1.Rows[i]["Cr"]);
                //    string dgTrnRemarks = dt1.Rows[i]["billid"].ToString();
                //    string dgBillnarr = dt1.Rows[i]["billnar"].ToString();

                //    DataRow[] dr2 = tblt01.Select("actcode='" + dgAccCode + "'  and subcode='" + dgResCode + "' and spclcode='" + dgSpclCode + "'  and trnrmrk='" + dgTrnRemarks + "'");
                //    if (dr2.Length > 0)
                //    {

                //        tblt01.Clear();
                //        return;

                //    }

                //    DataRow dr1 = tblt01.NewRow();
                //    dr1["actcode"] = dgAccCode;
                //    dr1["subcode"] = dgResCode;
                //    dr1["spclcode"] = dgSpclCode;
                //    dr1["actdesc"] = dgAccDesc;
                //    dr1["subdesc"] = dgResDesc;
                //    dr1["spcldesc"] = dgSpclDesc;
                //    dr1["trnqty"] = dgTrnQty;
                //    dr1["trnrate"] = dgTrnrate;
                //    dr1["trndram"] = dgTrnDrAmt;
                //    dr1["trncram"] = dgTrnCrAmt;
                //    dr1["trnrmrk"] = dgTrnRemarks;
                //    dr1["billno"] = dgTrnRemarks;
                //    dr1["billnar"] = dgBillnarr;
                //    tblt01.Rows.Add(dr1);
                //}

            }
            Session["tblt01"] = HiddenSameData(tblt01);
            string pactcode = "26" + ASTUtility.Right(tblt01.Rows[0]["actcode"].ToString(), 10);
            this.GetAdvanced();
            this.SupplierOverallAdvanced(pactcode);
            this.Data_Bind();
            this.GetNarration();


        }

        private void SupplierOverallAdvanced(string pactcode)
        {
            string billno = this.DropCheck1.Text;
            string ssircode = ((DataTable)Session["narration"]).Select("billno='" + billno + "'").Length == 0 ? "000000000000" : ((DataTable)Session["narration"]).Select("billno='" + billno + "'")[0]["ssircode"].ToString();
            string comcod = this.GetCompCode();
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string frmdate = "01" + date.Substring(2);
            string spclcode = "%";

            //SP_REPORT_ACCOUNTS_LG '3101', 'ACCOUNTSLEDGERSUB', '410100010011', '01-Jan-2012', '31-Dec-2013', '990100101002'
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGERSUB", pactcode, frmdate, date, ssircode, "", "", "", "", spclcode);

            this.lbtnBalance.Text = "Balance :" + Convert.ToDouble(ds1.Tables[2].Rows[0]["balam"]).ToString("#,##0.00;(#,##0.00);");
            lbtnBalance.NavigateUrl = this.ResolveUrl("~/F_17_Acc/AccLedger.aspx?Type=SubLedger&prjcode=" + pactcode + "&sircode=" + ssircode + "");




            // lbtnBalance.NavigateUrl = "~/F_17_Acc/AccPurchase.aspx?Type=Entry&genno=" + billno + "&ssircode=" + ssircode + "&Date1=" + Date1;


        }


        protected void Data_Bind()
        {

            DataTable tbl1 = (DataTable)Session["tblt01"];
            dgv2.DataSource = tbl1;
            dgv2.DataBind();
            this.GridColoumnVisible();
            this.calculation();
            //this.GetNarration();
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3101":
                case "3355":
                case "3368":
                    this.txtPayto.Text = ddlSupList.SelectedItem.Text.Trim();
                    break;
                default:
                    this.txtPayto.Text = "";
                    break;

            }
        }

        private void GetNarration()
        {
            DataTable dt = ((DataTable)Session["narration"]).Copy();
            string comcod = this.GetCompCode();
            string narration = "";

            string[] billno = this.DropCheck1.Text.Trim().Split(',');
            DataView dv = dt.DefaultView;
            foreach (string billno1 in billno)
            {

                dv.RowFilter = "billno='" + billno1 + "'";
                dt = dv.ToTable();

                if (comcod == "3330")
                {
                    narration = narration + "Order No : " + dt.Rows[0]["orderno"].ToString() + " , Bill No : " + dt.Rows[0]["billno"].ToString() + " , Bill Ref No : " + dt.Rows[0]["billref"].ToString() + " , Bill Date : " + Convert.ToDateTime(dt.Rows[0]["billdat"]).ToString("dd-MMM-yyyy") + " ,Pay to : " + dt.Rows[0]["ssirdesc"].ToString() + "; ";

                }

                else if (comcod == "3339")
                {
                    narration = narration + "Bill No : " + dt.Rows[0]["billno"].ToString() + " , Bill Ref No : " + dt.Rows[0]["billref"].ToString() + " , Bill Date : " + Convert.ToDateTime(dt.Rows[0]["billdat"]).ToString("dd-MMM-yyyy") + " , Chalan  No : " + dt.Rows[0]["chlnno"].ToString() + "; ";

                }

                else if (comcod == "3101" || comcod == "3305" || comcod == "3306" || comcod == "3311" || comcod == "2305")
                {
                    narration = narration + ((dt.Rows.Count == 0) ? "" : ("Bill No : " + dt.Rows[0]["billno"].ToString() + " , Bill Ref No : " + dt.Rows[0]["billref"].ToString() + " , Bill Date : " + Convert.ToDateTime(dt.Rows[0]["billrefdat"]).ToString("dd-MMM-yyyy"))) + " , Cheque Date : " + Convert.ToDateTime(dt.Rows[0]["chequedat"]).ToString("dd-MMM-yyy") + "; ";

                }

                else
                {
                    narration = narration + ((dt.Rows.Count == 0) ? "" : ("Bill No : " + dt.Rows[0]["billno"].ToString() + " , Bill Ref No : " + dt.Rows[0]["billref"].ToString() + " , Bill Date : " + Convert.ToDateTime(dt.Rows[0]["billdat"]).ToString("dd-MMM-yyyy"))) + "; ";

                }




            }

            narration = narration.Length > 0 ? (narration.Substring(0, ((narration.Length) - 2))) : "";

            this.txtNarration.Text = narration;


            //if (comcod == "3330" || comcod == "3101")
            //{
            //    this.txtNarration.Text = "Order No : " + dt.Rows[0]["orderno"].ToString() + " , Bill No : " + dt.Rows[0]["billno"].ToString() + " , Bill Ref No : " + dt.Rows[0]["billref"].ToString() + " , Bill Date : " + Convert.ToDateTime(dt.Rows[0]["billdat"]).ToString("dd-MMM-yyyy") + " ,Pay to : " + dt.Rows[0]["ssirdesc"].ToString();

            //}

            //else if (comcod == "3339")
            //{
            //    this.txtNarration.Text = "Bill No : " + dt.Rows[0]["billno"].ToString() + " , Bill Ref No : " + dt.Rows[0]["billref"].ToString() + " , Bill Date : " + Convert.ToDateTime(dt.Rows[0]["billdat"]).ToString("dd-MMM-yyyy") + " , Chalan  No : " + dt.Rows[0]["chlnno"].ToString();

            //}

            //else
            //{
            //    this.txtNarration.Text = (dt.Rows.Count == 0) ? "" : ("Bill No : " + dt.Rows[0]["billno"].ToString() + " , Bill Ref No : " + dt.Rows[0]["billref"].ToString() + " , Bill Date : " + Convert.ToDateTime(dt.Rows[0]["billdat"]).ToString("dd-MMM-yyyy"));

            //}



            //string ddlbillno=this.DropCheck1.Text;
            //DataView dv = dt.DefaultView;      
            //dv.RowFilter = "billno='" + ddlbillno + "'" ;
            //dt=dv.ToTable();



            //if (comcod=="3330"||comcod=="3101")
            //{
            //    this.txtNarration.Text = "Order No : " + dt.Rows[0]["orderno"].ToString() + " , Bill No : " + dt.Rows[0]["billno"].ToString() + " , Bill Ref No : " + dt.Rows[0]["billref"].ToString() + " , Bill Date : " + Convert.ToDateTime(dt.Rows[0]["billdat"]).ToString("dd-MMM-yyyy") + " ,Pay to : " + dt.Rows[0]["ssirdesc"].ToString();

            //}

            //else if (comcod == "3339" )
            //{
            //    this.txtNarration.Text = "Bill No : " + dt.Rows[0]["billno"].ToString() + " , Bill Ref No : " + dt.Rows[0]["billref"].ToString() + " , Bill Date : " + Convert.ToDateTime(dt.Rows[0]["billdat"]).ToString("dd-MMM-yyyy") + " , Chalan  No : " + dt.Rows[0]["chlnno"].ToString();

            //}

            //else
            //{
            //    this.txtNarration.Text =(dt.Rows.Count==0)?"": ("Bill No : " + dt.Rows[0]["billno"].ToString() + " , Bill Ref No : " + dt.Rows[0]["billref"].ToString() + " , Bill Date : " + Convert.ToDateTime(dt.Rows[0]["billdat"]).ToString("dd-MMM-yyyy"));

            //}



        }



        private void GridColoumnVisible()
        {
            DataTable tbl1 = (DataTable)Session["tblt01"];
            int TblRowIndex2;
            for (int j = 0; j < this.dgv2.Rows.Count; j++)
            {

                TblRowIndex2 = (this.dgv2.PageIndex) * this.dgv2.PageSize + j;
                string mRSIRCODE = tbl1.Rows[TblRowIndex2]["subcode"].ToString();
                if (ASTUtility.Left(mRSIRCODE, 2) != "97" && ASTUtility.Left(mRSIRCODE, 2) != "99" && ASTUtility.Left(mRSIRCODE, 9) != "019999902")
                    ((TextBox)this.dgv2.Rows[j].FindControl("txtgvCrAmt")).ReadOnly = true;
                //((TextBox)this.dgv2.Rows[j].FindControl("txtgvDrAmt")).ReadOnly = true;
            }


        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            int i = 0;
            string actcode = dt1.Rows[0]["actcode"].ToString();
            foreach (DataRow dr1 in dt1.Rows)
            {
                if (i == 0)
                {


                    actcode = dr1["actcode"].ToString();
                    i++;
                    continue;
                }

                if (dr1["actcode"].ToString() == actcode)
                {

                    dr1["actdesc"] = "";

                }


                actcode = dr1["actcode"].ToString();
            }



            return dt1;




            //if (dt1.Rows.Count == 0)
            //    return dt1;
            //string actcode = dt1.Rows[0]["actcode"].ToString();

            //for (int j = 1; j < dt1.Rows.Count; j++)
            //{
            //    if (dt1.Rows[j]["actcode"].ToString() == actcode)
            //    {
            //        actcode = dt1.Rows[j]["actcode"].ToString();
            //        dt1.Rows[j]["actdesc"] = "";

            //    }

            //    else
            //    {
            //        actcode = dt1.Rows[j]["actcode"].ToString();
            //    }

            //}

            //return dt1;
        }




        protected void imgSearchBillno_Click(object sender, EventArgs e)
        {
            this.LoadBillCombo();
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)Session["tblt01"];
            double todramt = 0, tocramt = 0;
            int TblRowIndex2;

            string billno = dt1.Rows[0]["billno"].ToString();

            for (int j = 0; j < this.dgv2.Rows.Count; j++)
            {
                string actcode = ((Label)this.dgv2.Rows[j].FindControl("lblAccCod")).Text.Trim();
                string billno1 = ((TextBox)this.dgv2.Rows[j].FindControl("txtgvRemarks")).Text.Trim();
                string Supplier = ((Label)this.dgv2.Rows[j].FindControl("lblResCod")).Text.Trim();
                double dramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvDrAmt")).Text.Trim()));
                string spclcode = ((DropDownList)this.dgv2.Rows[j].FindControl("ddlspcfdesc")).SelectedValue.ToString();
                string spcldesc = ((DropDownList)this.dgv2.Rows[j].FindControl("ddlspcfdesc")).SelectedItem.Text;
                if (billno1 != billno)
                {
                    todramt = 0; tocramt = 0;
                }

                todramt = todramt + dramt;
                TblRowIndex2 = (this.dgv2.PageIndex) * this.dgv2.PageSize + j;


                if (ASTUtility.Left(actcode, 2) == "26" && ASTUtility.Left(Supplier, 2) == "99" && spclcode == "000000000000")
                {
                    dt1.Rows[TblRowIndex2]["trncram"] = todramt - tocramt;
                    todramt = 0; tocramt = 0;
                    continue;
                }

                double cramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[j].FindControl("txtgvCrAmt")).Text.Trim()));
                tocramt = tocramt + cramt;

                dt1.Rows[TblRowIndex2]["trncram"] = cramt;
                dt1.Rows[TblRowIndex2]["trndram"] = dramt;
                dt1.Rows[TblRowIndex2]["spclcode"] = spclcode;
                dt1.Rows[TblRowIndex2]["spcldesc"] = spcldesc;
                billno = billno1;
            }
            Session["tblt01"] = dt1;
            this.Data_Bind();
            isTotal = true;

        }


        protected void dgv2_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddlspcfdesc = (DropDownList)e.Row.FindControl("ddlspcfdesc");
                DataTable dts;
                dts = ((DataTable)ViewState["tblspecific"]).Clone();
                string spclcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "spclcode")).ToString().Trim();
                string spcldesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "spcldesc")).ToString().Trim();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString().Trim();
                if (actcode.Substring(0, 2) == "23")
                {
                    dts = (DataTable)ViewState["tblspecific"];


                }
                else
                {
                    DataRow dr1 = dts.NewRow();
                    dr1["spcfcod"] = spclcode;
                    dr1["spcfdesc"] = spcldesc;
                    dts.Rows.Add(dr1);

                }


                if (dts.Rows.Count == 0)
                    return;
                ddlspcfdesc.DataTextField = "spcfdesc";
                ddlspcfdesc.DataValueField = "spcfcod";
                ddlspcfdesc.DataSource = dts;
                ddlspcfdesc.DataBind();
                ddlspcfdesc.SelectedValue = spclcode;

                //Specification           
                ddlspcfdesc.Enabled = (actcode.Substring(0, 2) == "23") ? true : false;
                if(actcode.Substring(0, 2) == "23")
                {
                    ddlspcfdesc.SelectedValue = ddlSupList.SelectedValue.ToString();
                }

            }
        }
        protected void ImgbtnFindSup_Click(object sender, EventArgs e)
        {
            this.GetConAndBill();
        }
        protected void ddlSupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadBillCombo();


        }
    }
}
