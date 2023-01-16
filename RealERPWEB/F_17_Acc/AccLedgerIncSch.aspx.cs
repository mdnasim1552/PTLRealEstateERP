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
using Microsoft.Reporting.WinForms;
//using RealERPRPT;
namespace RealERPWEB.F_17_Acc
{

    public partial class AccLedgerIncSch : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        public double balamt = 0.000000;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);



                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                //((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString() == "SubLedgerincsch") ? "Subsidiary Ledger Inc. Schedule" : "Ledger";
                //this.Master.Page.Title = (Request.QueryString["Type"].ToString() == "SubLedgerincsch") ? "Subsidiary Ledger Inc. Schedule" : "Ledger";
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.IbtnSearchAcc_Click(null, null);
                this.ibtnFindRes_Click(null, null);
                if (Request.QueryString["Type"].ToString() == "SubLedger")
                {


                    this.Panel1.Visible = true;
                }
                if (Request.QueryString["Type"].ToString() == "SubLedgerincsch")
                {


                    this.Panel1.Visible = true;
                }

                this.rbtnList1.SelectedIndex = 0;
            }
            if (this.txtDateFrom.Text.Trim().Length == 0)
            {
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtDateFrom.Text = DateTime.Today.AddYears(-1).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        private string GetCompcode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void IbtnSearchAcc_Click(object sender, EventArgs e)
        {
            try
            {


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string filter = (this.Request.QueryString["prjcode"].ToString()).Length == 0 ? "%" + this.txtAccSearch.Text.Trim() + "%" : this.Request.QueryString["prjcode"].ToString() + "%";
                DataSet ds1 = new DataSet();
                // &prjcode=&sircode=
                if (Request.QueryString["Type"].ToString() == "SubLedger")
                {

                    ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEADWITHRES", filter, "", "", "", "", "", "", "", "");
                }

                else
                {
                    ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEAD01", filter, "", "", "", "", "", "", "", "");

                }
                DataTable dt1 = ds1.Tables[0];
                this.ddlConAccHead.DataSource = dt1;
                this.ddlConAccHead.DataTextField = "actdesc1";
                this.ddlConAccHead.DataValueField = "actcode";
                this.ddlConAccHead.DataBind();

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        protected void ibtnFindRes_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string actcode = this.ddlConAccHead.SelectedValue.ToString();
            string comcod = hst["comcod"].ToString();

            // string filter = "%" + this.txtSrchRes.Text.Trim() + "%";

            string filter = (this.Request.QueryString["sircode"].ToString()).Length == 0 ? "%" + this.txtSrchRes.Text.Trim() + "%" : this.Request.QueryString["sircode"].ToString() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCRESHEAD", actcode, filter, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            //DataTable dt1 = ds1.Tables[0];
            this.ddlConAccResHead.DataSource = ds1.Tables[0];
            this.ddlConAccResHead.DataTextField = "resdesc1";
            this.ddlConAccResHead.DataValueField = "rescode";
            this.ddlConAccResHead.DataBind();

        }


        protected void lnkShowLedger_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            Session.Remove("StoreTable");
            DataSet ds2 = this.GetDataForReport();
            DataTable dt = ds2.Tables[0];
            if (dt.Rows.Count == 0)
            {
                this.dgv2.DataSource = null;
                this.dgv2.DataBind();
                return;
            }
            Session["StoreTable"] = dt;
            this.BalCalculation(dt);
            this.HiddenSameDate(dt);
            //  (Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")).Trim().Length==14 ? Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") : "")
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Account Ledger";
                string eventdesc = "Show Ledger";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            //string chkBlance = (this.chkwitoutopn.Checked) ? "" : "GetWithoutOpning()";
            //this.GetWithoutOpning();

            if (this.chkwitoutopn.Checked)
            {
                //this.GetWithoutOpning();
                //this.HiddenSameDate(dt);
            }
            else
            {
                this.GetDataForReport();
            }


        }

        private DataTable BalCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            double dramt, cramt;
            //string grp=
            for (int i = 0; i < dt.Rows.Count - 2; i++)
            {



                if ((dt.Rows[i]["vounum"]).ToString().Trim() == "TOTAL" || (dt.Rows[i]["vounum"]).ToString().Trim() == "BALANCE")
                    continue;
                if ((dt.Rows[i]["grp"]).ToString().Trim() == "C")
                    break;

                if (((dt.Rows[i]["cactcode"]).ToString().Trim()).Length == 12)
                {
                    dramt = Convert.ToDouble(dt.Rows[i]["dram"]);
                    cramt = Convert.ToDouble(dt.Rows[i]["cram"]);
                    balamt = balamt + (dramt - cramt);
                    dt.Rows[i]["balamt"] = balamt;
                }
            }
            return dt;


        }

        private void HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return;
            string grp = dt1.Rows[0]["grp"].ToString();
            string Date1 = dt1.Rows[0]["voudat1"].ToString();
            string vounum = dt1.Rows[0]["vounum"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }

                if (dt1.Rows[j]["vounum"].ToString() == vounum)
                {

                    if (vounum.Trim().Length > 0)
                    {
                        dt1.Rows[j]["vounum1"] = "";
                        dt1.Rows[j]["voudat1"] = "";
                    }
                    //dt1.Rows[j]["refnum"] = "";
                }

                if (dt1.Rows[j]["vounum"].ToString().Trim() == "TOTAL")
                {
                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";

                }
                if (dt1.Rows[j]["vounum"].ToString().Trim() == "BALANCE")
                {
                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";
                }

                grp = dt1.Rows[j]["grp"].ToString();
                vounum = dt1.Rows[j]["vounum"].ToString();
            }


            this.dgv2.DataSource = dt1;
            this.dgv2.DataBind();



            Session["Report1"] = dgv2;
            //if (dt1.Rows.Count > 0)
            //{

            //    ((Label)this.dgv2.FooterRow.FindControl ("lgvFScAmt")).Text = Convert.ToDouble ((Convert.IsDBNull (dt1.Compute ("sum(insamt)", "")) ?
            //                    0 : dt1.Compute ("sum(insamt)", ""))).ToString ("#,##0.00;(#,##0.00); ");
            //    ((Label)this.dgv2.FooterRow.FindControl ("lgvFCrAmt")).Text = Convert.ToDouble ((Convert.IsDBNull (dt1.Compute ("sum(dram)", "")) ?
            //                    0 : dt1.Compute ("sum(dram)", ""))).ToString ("#,##0.00;(#,##0.00); ");

            //    //((HyperLink)this.dgv2.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            //}
            //this.dgv2.Columns[0].Visible = (Request.QueryString["Type"].ToString() == "SubLedger") || (this.chkqty.Checked);
            //this.dgv2.Columns[8].Visible = (Request.QueryString["Type"].ToString () == "SubLedger" || Request.QueryString["Type"].ToString () == "SubLedgerincsch") && (this.chkqty.Checked);
            //this.dgv2.Columns[9].Visible = (Request.QueryString["Type"].ToString () == "SubLedger" || Request.QueryString["Type"].ToString () == "SubLedgerincsch") && (this.chkqty.Checked);

        }

        private void HiddenSameDate2(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;
            string grp = dt.Rows[0]["grp"].ToString();
            string Date1 = dt.Rows[0]["voudat1"].ToString();
            string vounum = dt.Rows[0]["vounum"].ToString();
            for (int j = 1; j < dt.Rows.Count; j++)
            {
                if (dt.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt.Rows[j]["grp"].ToString();
                    dt.Rows[j]["grpdesc"] = "";
                }

                if (dt.Rows[j]["vounum"].ToString() == vounum)
                {

                    dt.Rows[j]["vounum1"] = "";
                    dt.Rows[j]["voudat1"] = "";
                    //dt1.Rows[j]["refnum"] = "";
                }

                if (dt.Rows[j]["vounum"].ToString().Trim() == "TOTAL")
                {
                    dt.Rows[j]["vounum1"] = "";
                    dt.Rows[j]["voudat1"] = "";

                }
                if (dt.Rows[j]["vounum"].ToString().Trim() == "BALANCE")
                {
                    dt.Rows[j]["vounum1"] = "";
                    dt.Rows[j]["voudat1"] = "";
                }

                grp = dt.Rows[j]["grp"].ToString();
                vounum = dt.Rows[j]["vounum"].ToString();
            }


            this.dgv2.DataSource = dt;
            this.dgv2.DataBind();


            Session["Report1"] = dgv2;
            if (dt.Rows.Count > 0)
            {
                //((HyperLink)this.dgv2.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
            //this.dgv2.Columns[0].Visible = (Request.QueryString["Type"].ToString() == "SubLedger") || (this.chkqty.Checked);
            //this.dgv2.Columns[8].Visible = (Request.QueryString["Type"].ToString () == "SubLedger" || Request.QueryString["Type"].ToString () == "SubLedgerincsch") && (this.chkqty.Checked);
            //this.dgv2.Columns[9].Visible = (Request.QueryString["Type"].ToString () == "SubLedger" || Request.QueryString["Type"].ToString () == "SubLedgerincsch") && (this.chkqty.Checked);

        }
        private DataSet GetDataForReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string actcode = this.ddlConAccHead.SelectedValue.ToString();
            string date1 = this.txtDateFrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string withOutOpn = (this.chkwitoutopn.Checked) ? "withoutOpn" : "";
            string Narration = (this.rbtnList1.SelectedIndex == 0) ? "" : "WithoutNar";
            DataSet ds1 = new DataSet();

            if (Request.QueryString["Type"].ToString() == "SubLedger")
            {
                //string withOutOpn = (this.chkwitoutopn.Checked) ? "withoutOpn" : "";

                string rescode = this.ddlConAccResHead.SelectedValue.ToString();
                ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGERSUB", actcode, date1, date2, rescode, Narration, "", "", withOutOpn, "");

            }
            else if (Request.QueryString["Type"].ToString() == "SubLedgerincsch")
            {
                string rescode = this.ddlConAccResHead.SelectedValue.ToString();
                ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_CSCH", "ACCOUNTSUBLEDGERINCSCH", actcode, date1, date2, rescode, Narration, "", "", withOutOpn, "");
            }
            else
            {



                string calltype = (this.chksum.Checked) ? "ACCOUNTSLEDGERSUM" : "ACCOUNTSLEDGER";
                string ltype = (Request.QueryString["RType"].ToString() == "GLedger") ? "Without Cancel" : "";
                ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", calltype, actcode, date1, date2, "", Narration, "", ltype, withOutOpn, "");
            }

            return ds1;


        }





        protected void dgv2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            //Label hlink1 = (Label)e.Row.FindControl("HLgvVounum1");
            string mCOMCOD = comcod;

            //string mVOUNUM = hlink1.Text;
            string mTRNDAT1 = ((Label)e.Row.FindControl("lblgvvoudate")).Text;

            //if (mVOUNUM.Trim().Length == 14 && ASTUtility.Left(mVOUNUM.Trim(), 2) != "PV")
            //{


            //    //hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=voucher&comcod=" + mCOMCOD + "&vounum=" + mVOUNUM + "&Date1=" + mTRNDAT1;
            //    //hlink1.Text = mVOUNUM.Substring(0, 2) + mVOUNUM.Substring(6, 2) + "-" + mVOUNUM.Substring(8, 6)

            //         hlink1.NavigateUrl = "RptAccVouher.aspx?vounum=" + mVOUNUM;
            //    hlink1.Text = mVOUNUM.Substring(0, 2) + mVOUNUM.Substring(6, 2) + "-" + mVOUNUM.Substring(8, 6);
            //}

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grpdesc")).ToString();
                string vouno1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum1")).ToString();
                string insdate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "insdate")).ToString();
                Label pdc = (Label)e.Row.FindControl("lblgvGrpDesc");
                Label voudate = (Label)e.Row.FindControl("lblgvvoudate");
                Label vouamt = (Label)e.Row.FindControl("HLgvVounum1");
                Label amt = (Label)e.Row.FindControl("lblgvDrAmount0");
                Label rmks = (Label)e.Row.FindControl("lgvRemarks");
                Label schdate = (Label)e.Row.FindControl("lblgvscdate");
                Label scAmt = (Label)e.Row.FindControl("lblgvscamt");


                if (grp == "Post Dated Cheque")
                {
                    pdc.Attributes["style"] = "font-weight:bold;color:red";
                    voudate.Attributes["style"] = "font-weight:bold;color:red";
                    vouamt.Attributes["style"] = "font-weight:bold;color:red";
                    amt.Attributes["style"] = "font-weight:bold;color:red";
                    rmks.Attributes["style"] = "font-weight:bold;color:red";
                }

                if (vouno1 == "Total Payment" || vouno1 == "Payment Due" || vouno1 == "Total Due" || insdate == "Total")
                {
                    vouamt.Attributes["style"] = "font-weight:bold";
                    amt.Attributes["style"] = "font-weight:bold";
                    schdate.Attributes["style"] = "font-weight:bold";
                    scAmt.Attributes["style"] = "font-weight:bold";
                }

            }
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string todate = Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd-MMM-yyyy");


            DataTable dt1 = (DataTable)Session["StoreTable"];
            if (dt1 == null)
                return;

            var lst = dt1.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.LedgerinSch>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptPaymentIncSch", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("prjname", "Accounts Head: " + this.ddlConAccHead.SelectedItem.ToString().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )"));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }
        //private void PrintLedgerWithQty ( )
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString ();
        //    string comadd = hst["comadd1"].ToString ();
        //    string compname = hst["compname"].ToString ();
        //    string username = hst["username"].ToString ();
        //    string printdate = System.DateTime.Now.ToString ("dd.MM.yyyy hh:mm:ss tt");
        //    RealERPRPT.R_17_Acc.RptAccSLedger rptstk = new RealERPRPT.R_17_Acc.RptAccSLedger ();
        //    string Resdesc = "SUBSIDIARY HEAD: " + this.ddlConAccResHead.SelectedItem.Text.Substring (13);
        //    DataTable dt = (DataTable)Session["StoreTable"];
        //    if (dt == null)
        //        return;
        //    TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
        //    txtfdate.Text = "(From " + Convert.ToDateTime (this.txtDateFrom.Text).ToString ("dd-MMM-yyyy") + " To " + Convert.ToDateTime (this.txtDateto.Text).ToString ("dd-MMM-yyyy") + " )";
        //    TextObject rpttxtAccDesc = rptstk.ReportDefinition.ReportObjects["actdesc"] as TextObject;
        //    rpttxtAccDesc.Text = "ACCOUNT HEAD: " + this.ddlConAccHead.SelectedItem.ToString ().Substring (13);
        //    TextObject txtResDesc = rptstk.ReportDefinition.ReportObjects["txtResDesc"] as TextObject;
        //    txtResDesc.Text = Resdesc;

        //    TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat (compname, username, printdate);
        //    rptstk.SetDataSource ((DataTable)Session["StoreTable"]);
        //    string comcod = hst["comcod"].ToString ();
        //    string ComLogo = Server.MapPath (@"~\Image\LOGO" + comcod + ".jpg");
        //    rptstk.SetParameterValue ("ComLogo", ComLogo);
        //    Session["Report1"] = rptstk;
        //    ((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                          ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";
        //}




        private string ComLedger()
        {

            string comcod = this.GetCompcode();
            string comledger = "";
            switch (comcod)
            {
                case "3101":
                    comledger = "LedgerRupayan";
                    break;


                case "3330":
                    comledger = "LedgerBridge";
                    break;

                case "2305":
                case "3305":
                case "3306":
                case "3309":
                case "3310":
                case "3311":
                    comledger = "LedgerRupayan";
                    break;

                default:
                    comledger = "LedgerGen";
                    break;


            }

            return comledger;
        }










        private void PrintLedgerRDLC()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string todate = Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd-MMM-yyyy");
            string Headertitle = (this.ddlConAccHead.SelectedValue.ToString().Substring(0, 2) == "19") ? "Cash/Bank Book"
                 : (this.ddlConAccHead.SelectedValue.ToString().Substring(0, 2) == "29") ? "Cash/Bank Book"
                 : (Request.QueryString["Type"].ToString() == "SubLedger") ? "Subsidary Ledger" : "Ledger";
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;



            DataTable dt1 = (DataTable)Session["StoreTable"];
            if (dt1 == null)
                return;



            //DataSet ds1 = this.GetDataForReport();
            //if (dt1 == null)
            //    return;

            //if (ds1.Tables[0].Rows.Count == 0)
            //    return;
            var lst = dt1.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptLedger", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("txtHeadertitle", Headertitle));
            Rpt1.SetParameters(new ReportParameter("prjname", "Accounts Head: " + this.ddlConAccHead.SelectedItem.ToString().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )"));
            Rpt1.SetParameters(new ReportParameter("txtresdesc", "Resdesc"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintLedger()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //ReportDocument rptstk = new ReportDocument();

            //string comledger = this.ComLedger();
            //if (comledger == "LedgerBridge")
            //{
            //     rptstk = new RealERPRPT.R_17_Acc.RptAccLedgerBridge();
            //     TextObject rpttxtAccDesc = rptstk.ReportDefinition.ReportObjects["actdesc"] as TextObject;
            //     rpttxtAccDesc.Text = "Accounts Head: " + this.ddlConAccHead.SelectedItem.ToString().Substring(13);
            //     TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //     txtfdate.Text = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";
            //}

            //else if(comledger == "LedgerAlli")
            //{
            //     rptstk = new RealERPRPT.R_17_Acc.RptAccLedgerAlli();
            //    TextObject rpttxtAccDesc = rptstk.ReportDefinition.ReportObjects["txtactdesc"] as TextObject;
            //    rpttxtAccDesc.Text = "Accounts Head: " + this.ddlConAccHead.SelectedItem.ToString().Substring(13);
            //    TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //    txtfdate.Text = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";

            //}

            //else
            //{
            //    rptstk = new RealERPRPT.R_17_Acc.RptAccLedger();
            //    TextObject rpttxtAccDesc = rptstk.ReportDefinition.ReportObjects["txtactdesc"] as TextObject;
            //    rpttxtAccDesc.Text = "Accounts Head: " + this.ddlConAccHead.SelectedItem.ToString().Substring(13);
            //    TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //    txtfdate.Text = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";

            //}



            //    string Resdesc = "";
            //    if (Request.QueryString["Type"].ToString() == "SubLedger")
            //    {
            //        Resdesc = this.ddlConAccResHead.SelectedItem.Text.Substring(13);

            //    }
            //    DataTable dt = (DataTable)Session["StoreTable"];
            //    if (dt == null)
            //        return;
            //    string Headertitle = (this.ddlConAccHead.SelectedValue.ToString().Substring(0, 2) == "19") ? "Cash/Bank Book"
            //        : (this.ddlConAccHead.SelectedValue.ToString().Substring(0, 2) == "29") ? "Cash/Bank Book"
            //        : (Request.QueryString["Type"].ToString() == "SubLedger") ? "Subsidary Ledger" : "Ledger";


            //    TextObject txtHeadertitle = rptstk.ReportDefinition.ReportObjects["txtHeadertitle"] as TextObject;
            //    txtHeadertitle.Text = Headertitle;


            //    //TextObject txtSubHeadertitle = rptstk.ReportDefinition.ReportObjects["txtSubHeadertitle"] as TextObject;
            //    //txtSubHeadertitle.Text = Headertitle;

            //    TextObject txtcompanyname = rptstk.ReportDefinition.ReportObjects["txtcompanyname"] as TextObject;
            //    txtcompanyname.Text = comnam;





            //    //TextObject rpttxtActdesc = rptstk.ReportDefinition.ReportObjects["txtactdesc"] as TextObject;
            //    //rpttxtActdesc.Text = "Accounts Head: " + this.ddlConAccHead.SelectedItem.ToString().Substring(13);

            //    TextObject txtResDesc = rptstk.ReportDefinition.ReportObjects["txtResDesc"] as TextObject;
            //    txtResDesc.Text = (Resdesc.Length == 0) ? "" : "Details Head: " + Resdesc;




            //    TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            //    rptstk.SetDataSource((DataTable)Session["StoreTable"]);
            //    //tring comcod = hst["comcod"].ToString();
            //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //    rptstk.SetParameterValue("ComLogo", ComLogo);
            //    Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


    }
}
