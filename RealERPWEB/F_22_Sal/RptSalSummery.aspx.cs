using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
//using System.Drawing;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using Label = System.Web.UI.WebControls.Label;
namespace RealERPWEB.F_22_Sal
{
    public partial class RptSalSummery : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        public static double OpenBal, Clsbal;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);


                string date1 = this.Request.QueryString["Date1"];
                string date2 = this.Request.QueryString["Date2"];


                string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = date1 ?? Convert.ToDateTime("01" + Date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = date2 ?? Convert.ToDateTime(txtfromdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                
                
                
                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string Type = this.Request.QueryString["Type"].ToString();
                //this.lblHeader.Text = (Type == "QtyBasis") ? "SALES  SUMMARY REPORT(QTY BASIS)" :(Type == "SalesRegister")? "SALES REGISTER"
                //    : (Type == "dSaleVsColl") ? "DAILY SALES & COLLECTION STATUS" : (Type == "CollectStatus") ? "REAL COLLECTION STATUS"
                //    : (Type == "BankRecon") ? "Bank Reconcillation Summary" : "SALES  SUMMARY REPORT (AMOUNT BASIS)";


                this.ViewSection();
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

                string type = this.Request.QueryString["Type"].ToString();

                ((Label)this.Master.FindControl("lblTitle")).Text = type == "SalesRegister" ? "Sales Register"
                    : type == "QtyBasis" ? "Sale Summary(Qty Basis)"
                    : type == "AmtBasis" ? "Sale Summary(Amount Basis)" : type == "CollVsHonoured" ? "Collection Vs Reconcillation - Summary" : "Daily Sales & Collection Status";

                this.lbtnOk_Click(null, null);
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            comcod = qcomcod.Length > 0 ? qcomcod : comcod;
            return comcod;

        }

        protected void ViewSection()
        {
            string mRepID = Request.QueryString["Type"].ToString();
            switch (mRepID)
            {
                case "QtyBasis":
                    this.MultiView1.ActiveViewIndex = 0;

                    break;
                case "AmtBasis":
                    this.MultiView1.ActiveViewIndex = 1;

                    break;

                case "dSaleVsColl":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "SalesRegister":
                    this.MultiView1.ActiveViewIndex = 3;
                    break;
                case "CollectStatus":
                    this.chkwithoutrep.Visible = true;
                    this.MultiView1.ActiveViewIndex = 4;
                    break;

                case "BankRecon":
                    this.MultiView1.ActiveViewIndex = 5;
                    break;

                case "CollVsHonoured":
                    this.MultiView1.ActiveViewIndex = 6;
                    break;  
                
                case "mSalesTarget":
                    this.MultiView1.ActiveViewIndex = 7;
                    break;



            }

        }



        private void FooterCalculation(DataTable dt, string GvName)
        {


            switch (GvName)
            {
                case "gvSalSummery":
                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFtSh")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(thead1)", "")) ?
                            0 : dt.Compute("sum(thead1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFtCs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(thead2)", "")) ?
                          0 : dt.Compute("sum(thead2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFtApt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(thead3)", "")) ?
                            0 : dt.Compute("sum(thead3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFtOt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(thead4)", "")) ?
                          0 : dt.Compute("sum(thead4)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFBSh")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bhead1)", "")) ?
                            0 : dt.Compute("sum(bhead1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFBCs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bhead2)", "")) ?
                          0 : dt.Compute("sum(bhead2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFBApt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bhead3)", "")) ?
                            0 : dt.Compute("sum(bhead3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFBOt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bhead4)", "")) ?
                          0 : dt.Compute("sum(bhead4)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFCSh")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(chead1)", "")) ?
                            0 : dt.Compute("sum(chead1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFCCs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(chead2)", "")) ?
                          0 : dt.Compute("sum(chead2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFCApt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(chead3)", "")) ?
                            0 : dt.Compute("sum(chead3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFCOt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(chead4)", "")) ?
                          0 : dt.Compute("sum(chead4)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFtSSh")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(shead1)", "")) ?
                            0 : dt.Compute("sum(shead1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFtSCs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(shead2)", "")) ?
                          0 : dt.Compute("sum(shead2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFtSApt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(shead3)", "")) ?
                            0 : dt.Compute("sum(shead3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFtSOt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(shead4)", "")) ?
                          0 : dt.Compute("sum(shead4)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFASh")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ahead1)", "")) ?
                            0 : dt.Compute("sum(ahead1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFACS")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ahead2)", "")) ?
                          0 : dt.Compute("sum(ahead2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFAApt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ahead3)", "")) ?
                            0 : dt.Compute("sum(ahead3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSummery.FooterRow.FindControl("lgvFAot")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ahead4)", "")) ?
                          0 : dt.Compute("sum(ahead4)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "gvSalAmt":
                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFtShA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salval1)", "")) ?
                            0 : dt.Compute("sum(salval1)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFtCsA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salval2)", "")) ?
                          0 : dt.Compute("sum(salval2)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFtAptA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salval3)", "")) ?
                            0 : dt.Compute("sum(salval3)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFtOtA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salval4)", "")) ?
                          0 : dt.Compute("sum(salval4)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFBShA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(brecvam1)", "")) ?
                            0 : dt.Compute("sum(brecvam1)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFBCsA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(brecvam2)", "")) ?
                          0 : dt.Compute("sum(brecvam2)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFBAptA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(brecvam3)", "")) ?
                            0 : dt.Compute("sum(brecvam3)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFBOtA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(brecvam4)", "")) ?
                          0 : dt.Compute("sum(brecvam4)", ""))).ToString("#,##0.00;(#,##0.00); ");


                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFCShA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(crecvam1)", "")) ?
                            0 : dt.Compute("sum(crecvam1)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFCCsA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(crecvam2)", "")) ?
                          0 : dt.Compute("sum(crecvam2)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFCAptA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(crecvam3)", "")) ?
                            0 : dt.Compute("sum(crecvam3)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFCOtA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(crecvam4)", "")) ?
                          0 : dt.Compute("sum(crecvam4)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFtRShA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trecvam1)", "")) ?
                            0 : dt.Compute("sum(trecvam1)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFtRCsA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trecvam2)", "")) ?
                          0 : dt.Compute("sum(trecvam2)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFtRAptA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trecvam3)", "")) ?
                            0 : dt.Compute("sum(trecvam3)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFtROtA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trecvam4)", "")) ?
                          0 : dt.Compute("sum(trecvam4)", ""))).ToString("#,##0.00;(#,##0.00); ");


                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFRShA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recvbal1)", "")) ?
                            0 : dt.Compute("sum(recvbal1)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFRCSA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recvbal2)", "")) ?
                          0 : dt.Compute("sum(recvbal2)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFRAptA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recvbal3)", "")) ?
                            0 : dt.Compute("sum(recvbal3)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalAmt.FooterRow.FindControl("lgvFRotA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recvbal4)", "")) ?
                          0 : dt.Compute("sum(recvbal4)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    break;

                case "gvCollvsHonoured":
                    ((Label)this.gvCollvsHonoured.FooterRow.FindControl("lgvFcollvshonourCash")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ?
                            0 : dt.Compute("sum(cashamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCollvsHonoured.FooterRow.FindControl("lgvFcollvshonourBankamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ?
                          0 : dt.Compute("sum(bankamt)", ""))).ToString("#,##0;(#,##0); ");

                    Session["Report1"] = gvCollvsHonoured;
                    ((HyperLink)this.gvCollvsHonoured.HeaderRow.FindControl("hlbbanknameExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    break;



            }
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string mRepID = Request.QueryString["Type"].ToString();
            switch (mRepID)
            {
                case "QtyBasis":
                    this.ShowSummeryQbasis();
                    break;
                case "AmtBasis":
                    this.SalSummeryAbasis();
                    break;
                case "dSaleVsColl":
                    this.ShowDailSalVsColl(); ;
                    break;
                case "SalesRegister":
                    this.ShowSaleRegister();
                    break;
                case "CollectStatus":
                    this.ShowRealCollection();
                    break;
                case "BankRecon":
                    this.ShowBankReconcillation();
                    break;
                case "CollVsHonoured":
                    this.ShowCollVsHonoured();
                    break;

                //case "mSalesTarget":
                //    this.ShowmSalesTarget();
                //    break;


            }
        }

        private void ShowSummeryQbasis()
        {
            try
            {
                Session.Remove("tblsalsum");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComeCode();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "SALESSUM01", frmdate, todate, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvSalSummery.DataSource = null;
                    this.gvSalSummery.DataBind();
                    return;
                }

                Session["tblsalsum"] = ds1.Tables[0];
                this.gvSalSummery.Columns[2].HeaderText = (comcod.Substring(0, 1) == "2") ? "Plot" : "Shop";
                this.gvSalSummery.Columns[6].HeaderText = (comcod.Substring(0, 1) == "2") ? "B.Plot" : "B.Shop";
                this.gvSalSummery.Columns[10].HeaderText = (comcod.Substring(0, 1) == "2") ? "C.Plot" : "C.Shop";
                this.gvSalSummery.Columns[14].HeaderText = (comcod.Substring(0, 1) == "2") ? "TS.Plot" : "TS.Shop";
                this.gvSalSummery.Columns[18].HeaderText = (comcod.Substring(0, 1) == "2") ? "A.Plot" : "A.Shop";
                this.gvSalSummery.DataSource = ds1.Tables[0];
                this.gvSalSummery.DataBind();
                this.FooterCalculation(ds1.Tables[0], "gvSalSummery");
            }
            catch (Exception ex)
            {

            }
        }

        private void SalSummeryAbasis()
        {

            try
            {
                Session.Remove("tblsalsum");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComeCode();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "SALESSUM02", frmdate, todate, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvSalAmt.DataSource = null;
                    this.gvSalAmt.DataBind();
                    return;
                }

                Session["tblsalsum"] = ds1.Tables[0];
                this.gvSalAmt.Columns[2].HeaderText = (comcod.Substring(0, 1) == "2") ? "Plot" : "Shop";
                this.gvSalAmt.Columns[6].HeaderText = (comcod.Substring(0, 1) == "2") ? "B.Plot" : "B.Shop";
                this.gvSalAmt.Columns[10].HeaderText = (comcod.Substring(0, 1) == "2") ? "C.Plot" : "C.Shop";
                this.gvSalAmt.Columns[14].HeaderText = (comcod.Substring(0, 1) == "2") ? "TR.Plot" : "TR.Shop";
                this.gvSalAmt.Columns[18].HeaderText = (comcod.Substring(0, 1) == "2") ? "R.Plot" : "R.Shop";
                this.gvSalAmt.DataSource = ds1.Tables[0];
                this.gvSalAmt.DataBind();
                this.FooterCalculation(ds1.Tables[0], "gvSalAmt");


            }

            catch (Exception ex)
            {


            }

        }

        private void ShowDailSalVsColl()
        {
            try
            {
                Session.Remove("tblsalsum");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComeCode();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "RPTDWISESALVSCOLTAR", frmdate, todate, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvSalVsColl.DataSource = null;
                    this.gvSalVsColl.DataBind();
                    return;
                }
                Session["tblsalsum"] = ds1.Tables[0];
                this.gvSalVsColl.DataSource = this.HiddenSameData(ds1.Tables[0]);
                this.gvSalVsColl.DataBind();
            }
            catch (Exception ex)
            {
            }
        }        
        private void ShowmSalesTarget()
        {
            try
            {
                Session.Remove("tblsalsum");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComeCode();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "RPTDWISESALVSCOLTAR", frmdate, todate, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvSalVsColl.DataSource = null;
                    this.gvSalVsColl.DataBind();
                    return;
                }
                Session["tblsalsum"] = ds1.Tables[0];
                this.gvSalVsColl.DataSource = this.HiddenSameData(ds1.Tables[0]);
                this.gvSalVsColl.DataBind();
            }
            catch (Exception ex)
            {
            }
        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string mdeptcode = dt1.Rows[0]["mdeptcode"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mdeptcode"].ToString() == mdeptcode)
                    dt1.Rows[j]["mdeptname"] = "";

                mdeptcode = dt1.Rows[j]["mdeptcode"].ToString();

            }
            return dt1;

        }



        private void ShowSaleRegister()
        {

            try
            {
                Session.Remove("tblsalsum");
                Session.Remove("tblSalereg");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComeCode();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "RPTDWISESALEREGISTER", frmdate, todate, "", "", "", "", "", "", "");

                if (ds1.Tables[0].Rows.Count == 0)
                {

                    this.gvSalReg.DataSource = null;
                    this.gvSalReg.DataBind();
                    this.gvTransSum.DataSource = null;
                    this.gvTransSum.DataBind();
                    return;


                }
                Session["tblsalsum"] = ds1.Tables[0];
                Session["tblSalereg"] = ds1.Tables[1];
                this.gvSalReg.DataSource = ds1.Tables[0];
                this.gvSalReg.DataBind();

                ((Label)this.gvSalReg.FooterRow.FindControl("txtFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(saleamt)", "")) ?
                         0 : ds1.Tables[0].Compute("sum(saleamt)", ""))).ToString("#,##0;(#,##0); ");


                DataTable dt = ds1.Tables[2];
                for (int i = 0; i < dt.Rows.Count; i++)
                    this.gvTransSum.Columns[i].HeaderText = dt.Rows[i]["deptname"].ToString();

                this.gvTransSum.DataSource = ds1.Tables[1];
                this.gvTransSum.DataBind();



            }

            catch (Exception ex)
            {


            }



        }


        private void ShowRealCollection()
        {
            try
            {
                Session.Remove("tblsalsum");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComeCode();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string withrep = (this.chkwithoutrep.Checked ? "without" : "");

                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "RPTDWCOLLECTSTATUS", frmdate, todate, withrep, "", "", "", "", "", "");

                if (ds1.Tables[0].Rows.Count == 0)
                {

                    this.gvrcoll.DataSource = null;
                    this.gvrcoll.DataBind();
                    return;
                }
                Session["tblsalsum"] = ds1.Tables[0];
                this.gvrcoll.DataSource = ds1.Tables[0];
                this.gvrcoll.DataBind();


            }

            catch (Exception ex)
            {


            }


        }

        private void ShowCollectionStatus()
        {
            try
            {
                Session.Remove("tblsalsum");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComeCode();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "RPTDWTARVSCOLLECTION", frmdate, todate, "", "", "", "", "", "", "");

                if (ds1.Tables[0].Rows.Count == 0)
                {

                    this.gvrcoll.DataSource = null;
                    this.gvrcoll.DataBind();
                    return;
                }
                Session["tblsalsum"] = ds1.Tables[0];
                this.gvrcoll.DataSource = ds1.Tables[0];
                this.gvrcoll.DataBind();


            }

            catch (Exception ex)
            {


            }




        }
        private void ShowBankReconcillation()
        {
            try
            {
                Session.Remove("tblsalsum");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComeCode();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "RPTDWRECCOLLSTATUS", frmdate, todate, "", "", "", "", "", "", "");

                if (ds1.Tables[0].Rows.Count == 0)
                {

                    this.gvbrecon.DataSource = null;
                    this.gvbrecon.DataBind();
                    return;
                }
                Session["tblsalsum"] = ds1.Tables[0];
                this.gvbrecon.DataSource = ds1.Tables[0];
                this.gvbrecon.DataBind();


            }
            catch (Exception ex)
            {

            }



        }



        private void ShowCollVsHonoured()
        {
            try
            {
                Session.Remove("tblsalsum");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComeCode();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "RPT_DATE_WISE_COLL_HONOUR_STATUS", frmdate, todate, "", "", "", "", "", "", "");

                if (ds1.Tables[0].Rows.Count == 0)
                {

                    this.gvCollvsHonoured.DataSource = null;
                    this.gvCollvsHonoured.DataBind();
                    return;
                }
                Session["tblsalsum"] = ds1.Tables[0];
                this.gvCollvsHonoured.DataSource = ds1.Tables[0];
                this.gvCollvsHonoured.DataBind();
                this.FooterCalculation(ds1.Tables[0], "gvCollvsHonoured");


            }
            catch (Exception ex)
            {

            }



        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string mRepID = Request.QueryString["Type"].ToString();
            switch (mRepID)
            {
                case "QtyBasis":
                    //this.MultiView1.ActiveViewIndex = 0;
                    this.PrintSaleSummeryQbasis();
                    break;
                case "AmtBasis":
                    this.PrintSaleSummeryAmtbasis();
                    break;

                case "dSaleVsColl":
                    this.PrintDailSalVsColl(); ;
                    break;

                case "SalesRegister":
                    this.PrintSaleRegister();
                    break;
                case "CollectStatus":
                    this.PrintCollection();
                    break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales Sumarry";
                string eventdesc = "Print Report";
                string eventdesc2 = mRepID;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        private void PrintSaleSummeryQbasis()
        {
            //Iqbal Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblsalsum"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_22_Sal.Sales_BO.SalesSumaryR>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptSalSumary", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("FTdate", "(" + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Sales Summary Report (Quantity Basis)"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("bdate", "(As On " + Convert.ToDateTime(this.txtfromdate.Text).AddDays(-1).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("betdate", "(" + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("asondate", "(As On " + Convert.ToDateTime(this.txttodate.Text).AddDays(1).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            // Iqbal Nayan
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetComeCode ( );
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblsalsum"];
            //ReportDocument rptsale = new RealERPRPT.R_22_Sal.rptSalSumary();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptDate.Text = "From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy")+" to "+Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //TextObject rptbDate = rptsale.ReportDefinition.ReportObjects["bdate"] as TextObject;
            //rptbDate.Text = "(As On " + Convert.ToDateTime(this.txtfromdate.Text).AddDays(-1).ToString("dd-MMM-yyyy")+")";
            //TextObject rptbetDate = rptsale.ReportDefinition.ReportObjects["betdate"] as TextObject;
            //rptbetDate.Text = "("+ Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")+")";
            //TextObject rpatDate = rptsale.ReportDefinition.ReportObjects["adate"] as TextObject;
            //rpatDate.Text = "(As On " + Convert.ToDateTime(this.txttodate.Text).AddDays(1).ToString("dd-MMM-yyyy")+")";

            //string shoporplot = (comcod.Substring(0, 1) == "2") ? "Plot" : "Shop";
            //TextObject tshop = rptsale.ReportDefinition.ReportObjects["tshop"] as TextObject;
            //tshop.Text = shoporplot;
            //TextObject bshop = rptsale.ReportDefinition.ReportObjects["bshop"] as TextObject;
            //bshop.Text = shoporplot;
            //TextObject cshop = rptsale.ReportDefinition.ReportObjects["cshop"] as TextObject;
            //cshop.Text = shoporplot;
            //TextObject tsshop = rptsale.ReportDefinition.ReportObjects["tsshop"] as TextObject;
            //tsshop.Text = shoporplot;
            //TextObject ashop = rptsale.ReportDefinition.ReportObjects["ashop"] as TextObject;
            //ashop.Text = shoporplot;

            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintSaleSummeryAmtbasis()
        {
            //Iqbal Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblsalsum"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_22_Sal.Sales_BO.SalasSumaryA>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptSalSumAmtBasis", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("FTdate", "(" + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Sales Summary Report (Amount Basis)"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("bdate", "(As On " + Convert.ToDateTime(this.txtfromdate.Text).AddDays(-1).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("betdate", "(" + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("asondate", "(As On " + Convert.ToDateTime(this.txttodate.Text).AddDays(1).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetComeCode ( );
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblsalsum"];
            //ReportDocument rptsale = new RealERPRPT.R_22_Sal.rptSalSumAmtBasis();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptDate.Text = "From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //TextObject rptbDate = rptsale.ReportDefinition.ReportObjects["bdate"] as TextObject;
            //rptbDate.Text = "(As On " + Convert.ToDateTime(this.txtfromdate.Text).AddDays(-1).ToString("dd-MMM-yyyy") + ")";
            //TextObject rptbetDate = rptsale.ReportDefinition.ReportObjects["betdate"] as TextObject;
            //rptbetDate.Text = "(" + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";
            //TextObject rpatDate = rptsale.ReportDefinition.ReportObjects["adate"] as TextObject;
            //rpatDate.Text = "(As On " + Convert.ToDateTime(this.txttodate.Text).AddDays(1).ToString("dd-MMM-yyyy") + ")";

            //string shoporplot = (comcod.Substring(0, 1) == "2") ? "Plot" : "Shop";
            //TextObject tshop = rptsale.ReportDefinition.ReportObjects["tshop"] as TextObject;
            //tshop.Text = shoporplot;
            //TextObject bshop = rptsale.ReportDefinition.ReportObjects["bshop"] as TextObject;
            //bshop.Text = shoporplot;
            //TextObject cshop = rptsale.ReportDefinition.ReportObjects["cshop"] as TextObject;
            //cshop.Text = shoporplot;
            //TextObject tsshop = rptsale.ReportDefinition.ReportObjects["tsshop"] as TextObject;
            //tsshop.Text = shoporplot;
            //TextObject ashop = rptsale.ReportDefinition.ReportObjects["rshop"] as TextObject;
            //ashop.Text = shoporplot;
            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintDailSalVsColl()
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
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt = (DataTable)Session["tblsalsum"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptSalSummery>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.rptDailySaleVsCollTarget", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtDate", "( From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )"));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Material Transfer Information"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetComeCode ( );
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblsalsum"];
            //ReportDocument rptsale = new RealERPRPT.R_22_Sal.rptDailySaleVsCollTarget();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptDate.Text = "( From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )";

            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintSaleRegister()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblsalsum"];
            DataTable dt1 = (DataTable)Session["tblSalereg"];

            ReportDocument rptsale = new RealERPRPT.R_22_Sal.rptSaleRegisterSummary();




            TextObject rptCname = rptsale.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptDate = rptsale.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            rptDate.Text = "( From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )";







            // ReportDocument rptsubsale = new RealERPRPT.R_22_Sal.RptSubSaleRegister();



            for (int i = 0; i < this.gvTransSum.Columns.Count - 1; i++)
            {
                //if (i > 20)

                //    break;
                TextObject rpttxth = rptsale.Subreports["RptSubSaleRegister.rpt"].ReportDefinition.ReportObjects["txtamt" + (i + 1).ToString()] as TextObject;
                rpttxth.Text = this.gvTransSum.Columns[i].HeaderText.ToString().Trim();


                //TextObject rpttxth1 = rptsale.ReportDefinition.ReportObjects["txtamt" + (i + 1).ToString()] as TextObject;
                //rpttxth1.Text = this.gvTransSum.Columns[i].HeaderText.ToString().Trim();
            }


            //for (int i = 0; i < this.gvTransSum.Columns.Count; i++)
            //{
            //    TextObject rpttxth = rptsubsale.ReportDefinition.ReportObjects["txtvamt" + (i + 1).ToString()] as TextObject;
            //    rpttxth.Text = Convert.ToDouble("0" + ((Label)this.gvTransSum.Rows[0].FindControl("lgvamt" + (i + 1).ToString())).Text.Trim()).ToString("#,##0;(#,##0); ");
            //}





            TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            rptsale.Subreports["RptSubSaleRegister.rpt"].SetDataSource(dt1);
            rptsale.SetDataSource(dt);
            Session["Report1"] = rptsale;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintCollection()
        {
            //Iqbal  Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblsalsum"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_22_Sal.Sales_BO.SaleSummarySum>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptDWiseRealCollection", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Date", "From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("d-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Real Collection  - SummaryS"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



        protected void gvSalVsColl_RowDataBound(object sender, GridViewRowEventArgs e)
        {




            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Deptdesc = (Label)e.Row.FindControl("lblgvDepartment");
                Label lgvmonsalamt = (Label)e.Row.FindControl("lgvmonsalamt");
                Label lgvmoncollamt = (Label)e.Row.FindControl("lgvmoncollamt");
                Label lgvtsalamt = (Label)e.Row.FindControl("lgvtsalamt");
                Label lgvtcollamt = (Label)e.Row.FindControl("lgvtcollamt");
                Label lgvuatsalamt = (Label)e.Row.FindControl("lgvuatsalamt");
                Label lgvtatsaleamt = (Label)e.Row.FindControl("lgvtatsaleamt");
                Label lgvuatcollamt = (Label)e.Row.FindControl("lgvuatcollamt");
                Label lgvtatcollamt = (Label)e.Row.FindControl("lgvtatcollamt");
                Label lgvpmonsalamt = (Label)e.Row.FindControl("lgvpmonsalamt");
                Label lgvpmoncollamt = (Label)e.Row.FindControl("lgvpmoncollamt");
                Label lgvperontsale = (Label)e.Row.FindControl("lgvperontsale");
                Label lgvperontcoll = (Label)e.Row.FindControl("lgvperontcoll");
                Label lgvsalsfall = (Label)e.Row.FindControl("lgvsalsfall");
                Label lgvcollsfall = (Label)e.Row.FindControl("lgvcollsfall");



                lgvuatsalamt.Attributes["style"] = "background-color:#C0C0C0";
                lgvperontsale.Attributes["style"] = "background-color:#C0C0C0";

                lgvuatcollamt.Attributes["style"] = "background-color:#C0C0C0";
                lgvperontcoll.Attributes["style"] = "background-color:#C0C0C0";

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deptcode")).ToString();
                //string mdeptcode = Convert.ToString (DataBinder.Eval (e.Row.DataItem, "mdeptcode")).ToString ();


                if (code == "")
                {
                    return;
                }
                //if (mdeptcode == "")
                //{
                //    return;
                //}
                //if (mdeptcode.Substring(0, 2) == "94")
                //{
                //    Deptdesc.Attributes["style"] = "font-weight:bold;color:maroon";
                //}
                if (ASTUtility.Right(code, 2) == "AA" || ASTUtility.Right(code, 2) == "59")
                {

                    Deptdesc.Font.Bold = true;
                    lgvmonsalamt.Font.Bold = true;
                    lgvmoncollamt.Font.Bold = true;
                    lgvtsalamt.Font.Bold = true;
                    lgvtcollamt.Font.Bold = true;
                    lgvtatsaleamt.Font.Bold = true;
                    lgvtatcollamt.Font.Bold = true;
                    lgvuatsalamt.Font.Bold = true;
                    lgvuatcollamt.Font.Bold = true;
                    lgvpmonsalamt.Font.Bold = true;
                    lgvpmoncollamt.Font.Bold = true;
                    lgvperontsale.Font.Bold = true;
                    lgvperontcoll.Font.Bold = true;
                    lgvsalsfall.Font.Bold = true;
                    lgvcollsfall.Font.Bold = true;

                    Deptdesc.Style.Add("text-align", "right");


                }

            }
        }
        protected void gvrcoll_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink HplgvDepartment = (HyperLink)e.Row.FindControl("hpgvDepartmentrc");
                HyperLink HplgvTemp = (HyperLink)e.Row.FindControl("lgvtocollection");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string fdate = this.txtfromdate.Text.ToString();
                string tdate = this.txttodate.Text.ToString();

                //Label lgvtocollection = (Label)e.Row.FindControl("lgvtocollection");
                Label lgvinhfchq = (Label)e.Row.FindControl("lgvinhfchq");
                Label lgvinhrchq = (Label)e.Row.FindControl("lgvinhrchq");
                Label lgvchqdep = (Label)e.Row.FindControl("lgvchqdep");
                Label lgvreconamt = (Label)e.Row.FindControl("lgvreconamt");
                Label lgvinhpchq = (Label)e.Row.FindControl("lgvinhpchq");
                Label lgvrepchq = (Label)e.Row.FindControl("lgvrepchq");
                Label lgvncollamt = (Label)e.Row.FindControl("lgvncollamt");
                //chkwithoutrep
                string withrep = (this.chkwithoutrep.Checked ? "without" : "");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deptcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 2) == "AA" || ASTUtility.Right(code, 2) == "59")
                {

                    //Deptdesc.Font.Bold = true;
                    //lgvtocollection.Font.Bold = true;
                    lgvinhfchq.Font.Bold = true;
                    lgvinhrchq.Font.Bold = true;
                    lgvchqdep.Font.Bold = true;
                    lgvreconamt.Font.Bold = true;
                    lgvinhpchq.Font.Bold = true;
                    lgvrepchq.Font.Bold = true;
                    lgvncollamt.Font.Bold = true;

                    // Deptdesc.Style.Add("text-align", "right");

                }
                HplgvTemp.NavigateUrl = "~/F_22_Sal/RptSalSummeryDetails.aspx?Type=ClientLedger&comcod=" + comcod + "&code=" + code + "&Date1=" + fdate + "&Date2=" + tdate + "&length=" + withrep;
                HplgvDepartment.NavigateUrl = "~/F_22_Sal/RptSalSummeryDetails.aspx?Type=ClientLedger&comcod=" + comcod + "&code=" + code + "&Date1=" + fdate + "&Date2=" + tdate + "&length=" + withrep;

            }
        }
        protected void gvCollSt_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void gvbrecon_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Deptdesc = (Label)e.Row.FindControl("lblgvDepartmentbrec");
                Label lgvreconamt = (Label)e.Row.FindControl("lgvreconamtbrec");
                Label lgvadjsutment = (Label)e.Row.FindControl("lgvadjsutment");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deptcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 2) == "AA" || ASTUtility.Right(code, 2) == "59")
                {

                    Deptdesc.Font.Bold = true;
                    lgvreconamt.Font.Bold = true;
                    lgvadjsutment.Font.Bold = true;
                    Deptdesc.Style.Add("text-align", "right");


                }

            }
        }

        protected void gvmSalesTarget_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvCollvsHonoured_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string fdate = this.txtfromdate.Text.ToString();
                string tdate = this.txttodate.Text.ToString();

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvcollvsbankName");
                //Label Deptdesc = (Label)e.Row.FindControl("lblgvDepartmentbrec");
                //Label lgvreconamt = (Label)e.Row.FindControl("lgvreconamtbrec");
                //Label lgvadjsutment = (Label)e.Row.FindControl("lgvadjsutment");
                string bankcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "bankcode")).ToString();

                if (bankcode == "")
                {
                    return;
                }
                else
                {
                    hlink1.NavigateUrl = "LinkCollVsHonouredDetails.aspx?&Bankcode=" + bankcode +
                    "&Date1=" + fdate + "&Date2=" + tdate;
                    hlink1.Target = "_blank";

                }


            }
        }
    }
}
