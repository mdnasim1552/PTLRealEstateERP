using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WinForms;
using RealERPLIB;

using Label = System.Web.UI.WebControls.Label;
using RealERPRDLC;

namespace RealERPWEB.F_17_Acc
{
    public partial class RptAccCollVsClearance : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();

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
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : Convert.ToBoolean(dr1[0]["printable"]);

                string type = this.Request.QueryString["Type"].ToString().Trim();


                DateTime nowDate = DateTime.Now;
                DateTime yearfday = new DateTime(nowDate.Year, 1, 1);
                DateTime ylDay = new DateTime(nowDate.Year, 12, 31);


                string fdate = yearfday.ToString("dd-MMM-yyyy");
                string edate = ylDay.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = fdate;
                this.txttodate.Text = edate;
                this.SelectView();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "CollVsClearance") ? "Cheque Received Vs. Clearance" : (type == "DailyPayment") ? "Payment Status(All)"
                    : (type == "DetRealColl") ? "Real Collection - Details "
                    : (type == "MonCollection") ? "Month Wise Collection(Received)"
                    : (type == "MonCollHonoured") ? "Month Wise Collection(Honoured)"
                    : (type == "MonPayment") ? "Month Wise Payment - All Project"
                    : (type == "MonSales") ? "Month Wise Sales"
                    : (type == "MonReceipt") ? "Month Wise Collection"
                    : (type == "MonPaymentDet") ? "Month Wise Payment(Cost)"
                    : (type == "MonSalPerWise") ? "Month Wise Sales (Marketing Person)"
                    : (type == "MonAR") ? "Month Wise Collection"
                    : (type == "CollBuyer") ? "Month Wise Collection (Buyer & Project)"
                    : "Month Wise Payment-Summary";


            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void CustInf()
        {

            try

            {
                Session.Remove("tblcost");
                Session.Remove("tblPay");
                Session.Remove("tpripay");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string empid = hst["empid"].ToString();
                DataSet ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT04", "GETSALESTEAM", "", "", "", "", "", "", "", "", "");




                //Sales Team, CR Team
                DataTable dtscr = ds1.Tables[0].Copy();
                DataView dv;
                dv = dtscr.DefaultView;
                dv.RowFilter = ("secid like '9402%'");
                this.ddlSalesTeam.DataTextField = "gdesc";
                this.ddlSalesTeam.DataValueField = "gcod";
                this.ddlSalesTeam.DataSource = dv.ToTable();
                this.ddlSalesTeam.DataBind();


                if ((dv.ToTable().Select("gcod='" + empid + "'")).Length > 0)
                    this.ddlSalesTeam.SelectedValue = empid;





                //dv = dtscr.DefaultView;
                //dv.RowFilter = ("secid like '9403%'");
                //this.ddlCollectionTeam.DataTextField = "gdesc";
                //this.ddlCollectionTeam.DataValueField = "gcod";
                //this.ddlCollectionTeam.DataSource = dv.ToTable(); ;
                //this.ddlCollectionTeam.DataBind();
            }


            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + ex.Message + "');", true);



            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
        }


        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "CollVsClearance":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "DailyPayment":
                    this.rbtPayment.SelectedIndex = 0;
                    break;
                case "DetRealColl":
                    this.MultiView1.ActiveViewIndex = 3;
                    break;

                case "MonCollection":
                case "MonCollHonoured":
                case "MonSales":
                    this.CustInf();
                    DateTime nowDate = DateTime.Now;
                    DateTime yearfday = new DateTime(nowDate.Year, 1, 1);
                    DateTime ylDay = new DateTime(nowDate.Year, 12, 31);


                    string fdate = yearfday.ToString("dd-MMM-yyyy");
                    string edate = ylDay.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = fdate;
                    this.txttodate.Text = edate;

                    //this.txtfromdate.Text = System.DateTime.Today.AddMonths(-11).ToString("dd-MMM-yyyy");
                    //this.txtfromdate.Text =Convert.ToDateTime("01" + (this.txtfromdate.Text.Trim()).Substring(2)).ToString("dd-MMM-yyyy");
                    //this.txttodate.Text =Convert.ToDateTime(this.txtfromdate.Text).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 5;
                    break;

                case "MonPayment":
                case "MonReceipt":
                case "MonPaymentDet":

                    DateTime nowdate = DateTime.Now;
                    DateTime yearFday = new DateTime(nowdate.Year, 1, 1);
                    DateTime yLDay = new DateTime(nowdate.Year, 12, 31);
                    string fDate = yearFday.ToString("dd-MMM-yyyy");
                    string eDate = yLDay.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = fDate;
                    this.txttodate.Text = eDate;
                    this.MultiView1.ActiveViewIndex = 6;
                    break;


                case "MonPaymentSumm":
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-11).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = Convert.ToDateTime("01" + (this.txtfromdate.Text.Trim()).Substring(2)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 7;
                    break;
                case "MonSalPerWise":


                    DateTime now = DateTime.Now;
                    DateTime Fday = new DateTime(now.Year, 1, 1);
                    DateTime LDay = new DateTime(now.Year, 12, 31);


                    string firstdate = Fday.ToString("dd-MMM-yyyy");
                    string enddate = LDay.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = firstdate;
                    this.txttodate.Text = enddate;

                    this.chkSal.Visible = true;
                    //this.txtfromdate.Text = System.DateTime.Today.AddMonths (-11).ToString ("dd-MMM-yyyy");
                    //this.txtfromdate.Text = Convert.ToDateTime ("01" + (this.txtfromdate.Text.Trim ()).Substring (2)).ToString ("dd-MMM-yyyy");
                    //this.txttodate.Text = Convert.ToDateTime (this.txtfromdate.Text).AddYears (1).AddDays (-1).ToString ("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 8;
                    break;


                case "MonAR":

                    DateTime nowdatez = DateTime.Now;
                    DateTime yearFdayz = new DateTime(nowdatez.Year, 1, 1);
                    DateTime yLDayz = new DateTime(nowdatez.Year, 12, 31);
                    string fDatez = yearFdayz.ToString("dd-MMM-yyyy");
                    string eDatez = yLDayz.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = fDatez;
                    this.txttodate.Text = eDatez;
                    this.MultiView1.ActiveViewIndex = 9;
                    break;

                case "CollBuyer":

                    DateTime nowdatec = DateTime.Now;
                    DateTime yearFdayc = new DateTime(nowdatec.Year, 1, 1);
                    DateTime yLDayc = new DateTime(nowdatec.Year, 12, 31);
                    string fDatec = yearFdayc.ToString("dd-MMM-yyyy");
                    string eDatec = yLDayc.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = fDatec;
                    this.txttodate.Text = eDatec;
                    this.MultiView1.ActiveViewIndex = 10;
                    break;

                case "MonSalPerTarWise":
                    DateTime nowmkt = DateTime.Now;
                    DateTime mktFday = new DateTime(nowmkt.Year, 1, 1);
                    DateTime mkttDay = new DateTime(nowmkt.Year, 12, 31);


                    string mktfirstdate = mktFday.ToString("dd-MMM-yyyy");
                    string mktenddate = mkttDay.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = mktfirstdate;
                    this.txttodate.Text = mktenddate;

                    this.chkSal.Visible = true;
                    this.MultiView1.ActiveViewIndex = 11;
                    break;

            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "CollVsClearance":
                    this.ShowCollVsClearacne();
                    break;
                case "DailyPayment":
                    if (this.rbtPayment.SelectedIndex == 1)
                    {
                        this.MultiView1.ActiveViewIndex = 2;
                        this.ShowDailyPaymentDet();
                        ((Label)this.Master.FindControl("lblTitle")).Text = "Daily Payment Details Report";
                    }
                    else if (this.rbtPayment.SelectedIndex == 2)
                    {
                        this.MultiView1.ActiveViewIndex = 4;
                        this.ShowPaymentDet();
                    }
                    else
                    {
                        this.MultiView1.ActiveViewIndex = 1;
                        this.ShowDailyPayment();
                        this.rbtPayment.Visible = true;
                    }
                    break;

                case "DetRealColl":
                    this.ShowCollDetails();
                    break;
                case "MonCollection":
                case "MonCollHonoured":
                    this.ShowMonCollection();
                    break;

                case "MonPayment":
                    this.ShowMonPayorReceipt();
                    break;

                case "MonSales":
                    this.ShowMonSales();
                    break;

                case "MonReceipt":
                    this.ShowMonPayorReceipt();
                    break;

                case "MonPaymentDet":
                    this.ShowMonPayDetCostWise();
                    break;


                case "MonPaymentSumm":

                    this.ShowMonPaySummary();
                    break;


                case "MonSalPerWise":

                    this.ShowSalPerWise();
                    break;
                case "MonAR":

                    this.ShowMonthCollection();
                    break;
                case "CollBuyer":
                    this.ShowMonthCollectionBuyer();
                    break;

                case "MonSalPerTarWise":
        
                    this.ShowSalMktPerWise();
                    break;

            }
        }



        private void ShowCollVsClearacne()
        {
            ViewState.Remove("tblcollvscl");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RPTRECEIVEDVSCOLLEC", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvCollVsCleared.DataSource = null;
                this.gvCollVsCleared.DataBind();
                return;
            }
            ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }
        private void ShowDailyPayment()
        {
            ViewState.Remove("tblcollvscl");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //string CallType = (this.chbDetails.Checked) ? "RPTDAILYPAYMENTDETAILS" : "RPTDAILYPAYMENT";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RPTDAILYPAYMENT", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvDPayment.DataSource = null;
                this.gvDPayment.DataBind();
                return;
            }
            ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }
        private void ShowDailyPaymentDet()
        {
            ViewState.Remove("tblcollvscl");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RPTDAILYPAYMENTDETAILS", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPayDetails.DataSource = null;
                this.gvPayDetails.DataBind();
                return;
            }
            ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }
        private void ShowPaymentDet()
        {
            ViewState.Remove("tblcollvscl");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RPTPAYMENTDETAILS", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPayDet.DataSource = null;
                this.gvPayDet.DataBind();
                return;
            }
            ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }

        private void ShowCollDetails()
        {
            ViewState.Remove("tblcollvscl");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "RPTREALCOLLDETAILS", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvCollDet.DataSource = null;
                this.gvCollDet.DataBind();
                return;
            }
            ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();
        }

        private void ShowMonCollection()
        {
            ViewState.Remove("tblcollvscl");
            string comcod = this.GetCompCode();

            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfromdate.Text.Trim()));
            if (mon > 12)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Twelve');", true);
                return;
            }
            string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string realColl = (this.Request.QueryString["Type"] == "MonCollHonoured") ? "realization" : "";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "RPTMONWISECOLLECT", txtdatefrm, txtdateto, realColl, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMonCollect.DataSource = null;
                this.gvMonCollect.DataBind();
                return;
            }
            ViewState["tblcollvscl"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void ShowMonPayorReceipt()
        {

            ViewState.Remove("tblcollvscl");
            string comcod = this.GetCompCode();

            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfromdate.Text.Trim()));
            if (mon > 12)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Twelve');", true);
                return;


            }

            string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");

            string Receipt = (this.Request.QueryString["Type"].ToString() == "MonReceipt") ? "receipt" : "";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "MONWPAYORRECEIPT", txtdatefrm, txtdateto, Receipt, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMonPayment.DataSource = null;
                this.gvMonPayment.DataBind();
                return;
            }


            ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();



        }

        private void ShowMonPayDetCostWise()
        {

            ViewState.Remove("tblcollvscl");
            string comcod = this.GetCompCode();

            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfromdate.Text.Trim()));
            if (mon > 12)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Twelve');", true);
                return;


            }

            string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "MONWPAYDETAILS", txtdatefrm, txtdateto, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMonPayment.DataSource = null;
                this.gvMonPayment.DataBind();
                return;
            }


            ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();


        }
        private void ShowMonPaySummary()
        {

            ViewState.Remove("tblcollvscl");
            string comcod = this.GetCompCode();

            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfromdate.Text.Trim()));
            if (mon > 12)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Twelve');", true);
                return;


            }

            string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "MONWPAYSUMMARY", txtdatefrm, txtdateto, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMonPaymentSumm.DataSource = null;
                this.gvMonPaymentSumm.DataBind();
                return;
            }


            ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();



        }


        private void ShowMonthCollection()
        {
            this.rbtCol.Visible = true;
            this.divcol.Visible = true;

            ViewState.Remove("tblcollvscl");
            string comcod = this.GetCompCode();

            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfromdate.Text.Trim()));
            if (mon > 12)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Twelve');", true);
                return;


            }

            string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALES", "MONTHWISEMAR", txtdatefrm, txtdateto, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvViewAR.DataSource = null;
                this.gvViewAR.DataBind();
                return;
            }


            ViewState["tblcollvscl"] = ds1.Tables[0];
            ViewState["tblgraph"] = ds1.Tables[1];
            this.Data_Bind();

            DataTable dtc = (DataTable)ViewState["tblgraph"];
            var lst = dtc.DataTableToList<RealEntity.C_17_Acc.RptMonWiseCol>();


           // List<RealEntity.C_17_Acc.RptMonWiseCol> Lists = new List<RealEntity.C_17_Acc.RptMonWiseCol>();
          //  List<RealEntity.C_17_Acc.RptMonWiseCol> newList = ds1.Tables[2].DataTableToList<RealEntity.C_17_Acc.RptMonWiseCol>();
            //double a1 = newList.Select(a => a.amt1).Sum();
            //double a2 = newList.Select(a => a.amt2).Sum();
            //double a3 = newList.Select(a => a.amt3).Sum();
            //double a4 = newList.Select(a => a.amt4).Sum();
            //double a5 = newList.Select(a => a.amt5).Sum();
            //double a6 = newList.Select(a => a.amt6).Sum();
            //double a7 = newList.Select(a => a.amt7).Sum();
            //double a8 = newList.Select(a => a.amt8).Sum();
            //double a9 = newList.Select(a => a.amt9).Sum();
            //double a10 = newList.Select(a => a.amt10).Sum();
            //double a11 = newList.Select(a => a.amt11).Sum();
            //double a12 = newList.Select(a => a.amt12).Sum();
            ////double crore = 10000000;
            ////Lists.Add(new RealEntity.C_17_Acc.RptMonWiseCol(a1 / crore, a2 / crore, a3 / crore, a4 / crore, a5 / crore, a6 / crore, a7 / crore, a8 / crore, a9 / crore, a10 / crore, a11 / crore, a12 / crore));
           // Lists.Add(new RealEntity.C_17_Acc.RptMonWiseCol(a1, a2, a3, a4, a5, a6 , a7, a8, a9, a10, a11, a12));
            var jsonSerializer = new JavaScriptSerializer();
            //var json = jsonSerializer.Serialize(Lists);
            var json = jsonSerializer.Serialize(lst);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteSalesGraph('" + json + "')", true);

        }

        private void ShowMonthCollectionBuyer()
        {

            ViewState.Remove("tblcollvscl");
            string comcod = this.GetCompCode();
            string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALES", "MONTHWISECOLLBUYER", txtdatefrm, txtdateto, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvCollBuyer.DataSource = null;
                this.gvCollBuyer.DataBind();
                return;
            }


            ViewState["tblcollvscl"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }


        private void ShowSalMktPerWise()
        {

            ViewState.Remove("tblcollvscl");
            string comcod = this.GetCompCode();

           

            string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string proj = (this.chkSal.Checked) ? "project" : "";

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "RPTDWISEMSALESTARGET", txtdatefrm, txtdateto, proj, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSalPerWise.DataSource = null;
                this.gvSalPerWise.DataBind();
                return;
            }


            ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        private void ShowSalPerWise()
        {


            ViewState.Remove("tblcollvscl");
            string comcod = this.GetCompCode();

            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfromdate.Text.Trim()));
            if (mon > 12)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Twelve');", true);
                return;


            }

            string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string proj = (this.chkSal.Checked) ? "project" : "";

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "MONWISESALESPER", txtdatefrm, txtdateto, proj, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSalPerWise.DataSource = null;
                this.gvSalPerWise.DataBind();
                return;
            }


            ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }



        private void ShowMonSales()
        {
            this.tabdiv.Visible = true;
            this.rbtCol.Visible = true;
            ViewState.Remove("tblcollvscl");
            string comcod = this.GetCompCode();

            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfromdate.Text.Trim()));
            if (mon > 12)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Twelve');", true);
                return;


            }
            string teamcode = this.ddlSalesTeam.SelectedValue.ToString()+"%";
            string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "RPTMONWISESALES", txtdatefrm, txtdateto, teamcode, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMonCollect.DataSource = null;
                this.gvMonCollect.DataBind();
                return;
            }




            ViewState["tblcollvscl"] = ds1.Tables[0];
            ViewState["tblcollvsclGraph"] = ds1.Tables[1];
            this.Data_Bind();

            DataTable dt1 = (DataTable)ViewState["tblcollvsclGraph"];

            List<RealEntity.C_17_Acc.RptMonWiseCol> lists = dt1.DataTableToList<RealEntity.C_17_Acc.RptMonWiseCol>();
            //List<RealEntity.C_17_Acc.RptMonWiseCol> newlist = new List<RealEntity.C_17_Acc.RptMonWiseCol>();
            //var list = lists.FindAll(s => s.pactdesc == "Grand Total");
            //double crore = 10000000;
            //newlist.Add(new RealEntity.C_17_Acc.RptMonWiseCol(list[0].amt1 / crore, list[0].amt2 / crore, list[0].amt3 / crore, list[0].amt4 / crore, list[0].amt5 / crore, list[0].amt6 / crore, list[0].amt7 / crore, list[0].amt8 / crore, list[0].amt9 / crore, list[0].amt10 / crore, list[0].amt11 / crore, list[0].amt12 / crore));
            var jsonSerializer = new JavaScriptSerializer();
            var json = jsonSerializer.Serialize(lists);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteSalesGraph('" + json + "')", true);

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "CollVsClearance":
                    string rarcndate1 = dt1.Rows[0]["rarcndate1"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["rarcndate1"].ToString() == rarcndate1)
                            dt1.Rows[j]["rarcndate"] = "";


                        rarcndate1 = dt1.Rows[j]["rarcndate1"].ToString();

                    }
                    break;
                case "DailyPayment":
                    if (this.rbtPayment.SelectedIndex == 1)
                    {
                        string actcode1 = dt1.Rows[0]["actcode1"].ToString();
                        for (int j = 1; j < dt1.Rows.Count; j++)
                        {
                            if (dt1.Rows[j]["actcode1"].ToString() == actcode1)
                            {
                                actcode1 = dt1.Rows[j]["actcode1"].ToString();
                                dt1.Rows[j]["actdesc1"] = "";
                            }
                            else
                            {
                                actcode1 = dt1.Rows[j]["actcode1"].ToString();
                            }
                        }
                    }
                    break;


                case "DetRealColl":
                case "MonPayment":
                case "MonReceipt":
                case "MonPaymentSumm":
                case "MonPaymentDet":
                    string grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }

                        else
                            grp = dt1.Rows[j]["grp"].ToString();
                    }

                    break;

                case "MonSalPerTarWise":

                    string salesper = dt1.Rows[0]["salesperson"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["salesperson"].ToString() == salesper)
                        {
                            salesper = dt1.Rows[j]["salesperson"].ToString();
                            dt1.Rows[j]["salesperson"] = "";
                        }

                        else
                            salesper = dt1.Rows[j]["salesperson"].ToString();
                    }
                    break;

                case "MonSalPerWise":

                    string salesperson = dt1.Rows[0]["salesperson"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["salesperson"].ToString() == salesperson)
                        {
                            salesperson = dt1.Rows[j]["salesperson"].ToString();
                            dt1.Rows[j]["salesperson"] = "";
                        }

                        else
                            salesperson = dt1.Rows[j]["salesperson"].ToString();
                    }
                    break;
                case "CollBuyer":

                    string pactcode = dt1.Rows[0]["pactcode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                        }
                        else
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                        }
                    }

                    break;


            }

            return dt1;

        }


        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            double amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12;
            DateTime datefrm, dateto;
            DataView dv; DataTable dt;
            switch (type)
            {


                case "CollVsClearance":
                    this.gvCollVsCleared.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvCollVsCleared.DataSource = (DataTable)ViewState["tblcollvscl"];
                    this.gvCollVsCleared.DataBind();
                    this.FooterCalculation();
                    break;
                case "DailyPayment":
                    if (this.rbtPayment.SelectedIndex == 1)
                    {
                        this.gvPayDetails.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvPayDetails.DataSource = (DataTable)ViewState["tblcollvscl"];
                        this.gvPayDetails.DataBind();
                        this.FooterCalculation();
                    }
                    else if (this.rbtPayment.SelectedIndex == 2)
                    {
                        this.gvPayDet.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvPayDet.DataSource = (DataTable)ViewState["tblcollvscl"];
                        this.gvPayDet.DataBind();
                        this.FooterCalculation();
                    }
                    else
                    {
                        this.gvDPayment.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvDPayment.DataSource = (DataTable)ViewState["tblcollvscl"];
                        this.gvDPayment.DataBind();
                        this.FooterCalculation();
                    }
                    break;


                case "DetRealColl":
                    this.gvCollDet.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvCollDet.DataSource = (DataTable)ViewState["tblcollvscl"];
                    this.gvCollDet.DataBind();
                    this.FooterCalculation();
                    break;

                case "MonCollection":
                case "MonCollHonoured":
                case "MonSales":

                    dv = ((DataTable)ViewState["tblcollvscl"]).Copy().DefaultView;
                    dv.RowFilter = ("pactcode  like '%99BBBBAAAAAA%'");
                    dt = dv.ToTable();
                    amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", "")));
                    amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", "")));
                    amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", "")));
                    amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", "")));
                    amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", "")));
                    amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", "")));
                    amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", "")));
                    amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", "")));
                    amt9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", "")));
                    amt10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", "")));
                    amt11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", "")));
                    amt12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", "")));

                    this.gvMonCollect.Columns[3].Visible = (amt1 != 0);
                    this.gvMonCollect.Columns[4].Visible = (amt2 != 0);
                    this.gvMonCollect.Columns[5].Visible = (amt3 != 0);
                    this.gvMonCollect.Columns[6].Visible = (amt4 != 0);
                    this.gvMonCollect.Columns[7].Visible = (amt5 != 0);
                    this.gvMonCollect.Columns[8].Visible = (amt6 != 0);
                    this.gvMonCollect.Columns[9].Visible = (amt7 != 0);
                    this.gvMonCollect.Columns[10].Visible = (amt8 != 0);
                    this.gvMonCollect.Columns[11].Visible = (amt9 != 0);
                    this.gvMonCollect.Columns[12].Visible = (amt10 != 0);
                    this.gvMonCollect.Columns[13].Visible = (amt11 != 0);
                    this.gvMonCollect.Columns[14].Visible = (amt12 != 0);

                    datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                    dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
                    for (int i = 3; i < 15; i++)
                    {
                        if (datefrm > dateto)
                            break;

                        this.gvMonCollect.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                        datefrm = datefrm.AddMonths(1);

                    }

                    this.gvMonCollect.DataSource = (DataTable)ViewState["tblcollvscl"];
                    this.gvMonCollect.DataBind();
                    this.FooterCalculation();


                    break;



                case "MonPayment":
                case "MonReceipt":
                case "MonPaymentDet":
                    dv = ((DataTable)ViewState["tblcollvscl"]).Copy().DefaultView;
                    dv.RowFilter = ("pactcode  like '%99BBBBAAAAAA%'");
                    dt = dv.ToTable();
                    amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", "")));
                    amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", "")));
                    amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", "")));
                    amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", "")));
                    amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", "")));
                    amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", "")));
                    amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", "")));
                    amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", "")));
                    amt9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", "")));
                    amt10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", "")));
                    amt11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", "")));
                    amt12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", "")));

                    this.gvMonPayment.Columns[5].Visible = (amt1 != 0);
                    this.gvMonPayment.Columns[6].Visible = (amt2 != 0);
                    this.gvMonPayment.Columns[7].Visible = (amt3 != 0);
                    this.gvMonPayment.Columns[8].Visible = (amt4 != 0);
                    this.gvMonPayment.Columns[9].Visible = (amt5 != 0);
                    this.gvMonPayment.Columns[10].Visible = (amt6 != 0);
                    this.gvMonPayment.Columns[11].Visible = (amt7 != 0);
                    this.gvMonPayment.Columns[12].Visible = (amt8 != 0);
                    this.gvMonPayment.Columns[13].Visible = (amt9 != 0);
                    this.gvMonPayment.Columns[14].Visible = (amt10 != 0);
                    this.gvMonPayment.Columns[15].Visible = (amt11 != 0);
                    this.gvMonPayment.Columns[16].Visible = (amt12 != 0);



                    datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                    dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
                    for (int i = 5; i < 17; i++)
                    {
                        if (datefrm > dateto)
                            break;

                        this.gvMonPayment.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                        datefrm = datefrm.AddMonths(1);

                    }

                    this.gvMonPayment.DataSource = (DataTable)ViewState["tblcollvscl"];
                    this.gvMonPayment.DataBind();
                    this.FooterCalculation();
                    break;


                case "MonAR":
                    dt = ((DataTable)ViewState["tblcollvscl"]).Copy();
                    amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", "")));
                    amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", "")));
                    amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", "")));
                    amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", "")));
                    amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", "")));
                    amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", "")));
                    amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", "")));
                    amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", "")));
                    amt9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", "")));
                    amt10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", "")));
                    amt11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", "")));
                    amt12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", "")));

                    this.gvViewAR.Columns[3].Visible = (amt1 != 0);
                    this.gvViewAR.Columns[4].Visible = (amt2 != 0);
                    this.gvViewAR.Columns[5].Visible = (amt3 != 0);
                    this.gvViewAR.Columns[6].Visible = (amt4 != 0);
                    this.gvViewAR.Columns[7].Visible = (amt5 != 0);
                    this.gvViewAR.Columns[8].Visible = (amt6 != 0);
                    this.gvViewAR.Columns[9].Visible = (amt7 != 0);
                    this.gvViewAR.Columns[10].Visible = (amt8 != 0);
                    this.gvViewAR.Columns[11].Visible = (amt9 != 0);
                    this.gvViewAR.Columns[12].Visible = (amt10 != 0);
                    this.gvViewAR.Columns[13].Visible = (amt11 != 0);
                    this.gvViewAR.Columns[14].Visible = (amt12 != 0);



                    datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                    dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
                    for (int i = 3; i < 15; i++)
                    {
                        if (datefrm > dateto)
                            break;

                        this.gvViewAR.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                        datefrm = datefrm.AddMonths(1);

                    }

                    this.gvViewAR.DataSource = (DataTable)ViewState["tblcollvscl"];
                    this.gvViewAR.DataBind();
                    this.FooterCalculation();
                    break;



                case "MonPaymentSumm":
                    dv = ((DataTable)ViewState["tblcollvscl"]).Copy().DefaultView;
                    dv.RowFilter = ("pactcode  like '%99BBBBAAAAAA%'");
                    dt = dv.ToTable();
                    amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", "")));
                    amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", "")));
                    amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", "")));
                    amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", "")));
                    amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", "")));
                    amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", "")));
                    amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", "")));
                    amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", "")));
                    amt9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", "")));
                    amt10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", "")));
                    amt11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", "")));
                    amt12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", "")));

                    this.gvMonPaymentSumm.Columns[3].Visible = (amt1 != 0);
                    this.gvMonPaymentSumm.Columns[4].Visible = (amt2 != 0);
                    this.gvMonPaymentSumm.Columns[5].Visible = (amt3 != 0);
                    this.gvMonPaymentSumm.Columns[6].Visible = (amt4 != 0);
                    this.gvMonPaymentSumm.Columns[7].Visible = (amt5 != 0);
                    this.gvMonPaymentSumm.Columns[8].Visible = (amt6 != 0);
                    this.gvMonPaymentSumm.Columns[9].Visible = (amt7 != 0);
                    this.gvMonPaymentSumm.Columns[10].Visible = (amt8 != 0);
                    this.gvMonPaymentSumm.Columns[11].Visible = (amt9 != 0);
                    this.gvMonPaymentSumm.Columns[12].Visible = (amt10 != 0);
                    this.gvMonPaymentSumm.Columns[13].Visible = (amt11 != 0);
                    this.gvMonPaymentSumm.Columns[14].Visible = (amt12 != 0);



                    datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                    dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
                    for (int i = 3; i < 15; i++)
                    {
                        if (datefrm > dateto)
                            break;

                        this.gvMonPaymentSumm.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                        datefrm = datefrm.AddMonths(1);

                    }

                    this.gvMonPaymentSumm.DataSource = (DataTable)ViewState["tblcollvscl"];
                    this.gvMonPaymentSumm.DataBind();
                    this.FooterCalculation();
                    break;

                case "MonSalPerWise":
                    dv = ((DataTable)ViewState["tblcollvscl"]).Copy().DefaultView;
                    dt = dv.ToTable();

                    datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                    dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
                    //for (int i = 2; i < 15; i++)
                    //{
                    //    if (datefrm > dateto)
                    //        break;

                    //    this.gvSalPerWise.Columns[i].HeaderText = datefrm.ToString("MMM");
                    //    datefrm = datefrm.AddMonths(1);
                    //}
                    //this.gvSalPerWise.Columns[2].Visible = this.chkSal.Checked;
                    this.gvSalPerWise.DataSource = (DataTable)ViewState["tblcollvscl"];
                    this.gvSalPerWise.DataBind();
                    this.FooterCalculation();
                    break;

                case "CollBuyer":
                    dt = (DataTable)ViewState["tblcollvscl"];
                    this.gvCollBuyer.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvCollBuyer.DataSource = dt;
                    this.gvCollBuyer.DataBind();
                    this.FooterCalculation();
                    break;

                case "MonSalPerTarWise":
                    dt = (DataTable)ViewState["tblcollvscl"];
                    this.gvmsaletarget.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvmsaletarget.DataSource = dt;
                    this.gvmsaletarget.DataBind();
                    //this.FooterCalculation();
                    break;

                    
            }

        }


        private void FooterCalculation()
        {
            DataTable dt1 = (DataTable)ViewState["tblcollvscl"];
            if (dt1.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt4;
            DataView dv1;
            switch (type)
            {


                case "CollVsClearance":
                    dt4 = dt1.Copy();
                    dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("chqno like 'Grand Total'");
                    dt4 = dv1.ToTable();
                    double curamt = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(clcuram)", "")) ? 0 : dt4.Compute("sum(clcuram)", "")));
                    double preamt = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(clpream)", "")) ? 0 : dt4.Compute("sum(clpream)", "")));

                    ((Label)this.gvCollVsCleared.FooterRow.FindControl("lgvFCollAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(recam)", "")) ?
                               0 : dt4.Compute("sum(recam)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvCollVsCleared.FooterRow.FindControl("lgvFCuamt")).Text = curamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCollVsCleared.FooterRow.FindControl("lgvFPreamt")).Text = preamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCollVsCleared.FooterRow.FindControl("lgvNetTotal")).Text = (curamt + preamt).ToString("#,##0;(#,##0); ");
                    break;
                case "DailyPayment":
                    if (this.rbtPayment.SelectedIndex == 1)
                    {
                        DataTable ddtc = dt1.Copy();
                        DataView dvc = ddtc.DefaultView;
                        dvc.RowFilter = ("grp='B'");
                        ddtc = dvc.ToTable();
                        ((Label)this.gvPayDetails.FooterRow.FindControl("lgvFTDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ddtc.Compute("sum(dram)", "")) ?
                               0 : ddtc.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");
                    }
                    else if (this.rbtPayment.SelectedIndex == 2)
                    {
                        DataTable ddt = dt1.Copy();
                        DataView dv = ddt.DefaultView;
                        dv.RowFilter = ("grp='B'");
                        ddt = dv.ToTable();
                        ((Label)this.gvPayDet.FooterRow.FindControl("lgvFPayAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ddt.Compute("sum(payam)", "")) ?
                              0 : ddt.Compute("sum(payam)", ""))).ToString("#,##0;(#,##0); ");
                    }
                    else
                    {
                        ((Label)this.gvDPayment.FooterRow.FindControl("lgvFPayAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(payam)", "")) ?
                                  0 : dt1.Compute("sum(payam)", ""))).ToString("#,##0;(#,##0); ");
                    }
                    break;


                case "DetRealColl":
                    dt4 = dt1.Copy();
                    dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("grp='G' and usircode='CCCCAAAAAAAA' ");
                    dt4 = dv1.ToTable();
                    double cashamt = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(cashamt)", "")) ? 0 : dt4.Compute("sum(cashamt)", "")));
                    double chqamt = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(chqamt)", "")) ? 0 : dt4.Compute("sum(chqamt)", "")));


                    ((Label)this.gvCollDet.FooterRow.FindControl("lgvFCashamt")).Text = cashamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCollDet.FooterRow.FindControl("lgvFChqamt")).Text = chqamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCollDet.FooterRow.FindControl("lgvCDNetTotal")).Text = (cashamt + chqamt).ToString("#,##0;(#,##0); ");
                    break;


                case "MonCollection":
                case "MonCollHonoured":
                case "MonSales":

                    dt4 = dt1.Copy();
                    dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("pactcode='99BBBBAAAAAA'");
                    dt4 = dv1.ToTable();

                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(toamt)", "")) ? 0.00 : dt4.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt1)", "")) ? 0.00 : dt4.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt2)", "")) ? 0.00 : dt4.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt3)", "")) ? 0.00 : dt4.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt4)", "")) ? 0.00 : dt4.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt5)", "")) ? 0.00 : dt4.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt6")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt6)", "")) ? 0.00 : dt4.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt7")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt7)", "")) ? 0.00 : dt4.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt8")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt8)", "")) ? 0.00 : dt4.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt9")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt9)", "")) ? 0.00 : dt4.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt10")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt10)", "")) ? 0.00 : dt4.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt11")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt11)", "")) ? 0.00 : dt4.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt12")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt12)", "")) ? 0.00 : dt4.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");

                    break;


                case "MonPayment":
                case "MonReceipt":
                case "MonPaymentDet":
                    dt4 = dt1.Copy();
                    dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("pactcode='99BBBBAAAAAA'");
                    dt4 = dv1.ToTable();
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFnetTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(netamt)", "")) ? 0.00 : dt4.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFopening")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(opnamt)", "")) ? 0.00 : dt4.Compute("sum(opnamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFtoamtmpay")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(toamt)", "")) ? 0.00 : dt4.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay1")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt1)", "")) ? 0.00 : dt4.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay2")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt2)", "")) ? 0.00 : dt4.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay3")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt3)", "")) ? 0.00 : dt4.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay4")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt4)", "")) ? 0.00 : dt4.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay5")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt5)", "")) ? 0.00 : dt4.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay6")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt6)", "")) ? 0.00 : dt4.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay7")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt7)", "")) ? 0.00 : dt4.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay8")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt8)", "")) ? 0.00 : dt4.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay9")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt9)", "")) ? 0.00 : dt4.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay10")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt10)", "")) ? 0.00 : dt4.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay11")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt11)", "")) ? 0.00 : dt4.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay12")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt12)", "")) ? 0.00 : dt4.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "MonAR":
                    dt4 = dt1.Copy();
                    double[] anmt = new double[12];
                    int counter = 0;

                    anmt[0] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt1)", "")) ? 0.00 : dt4.Compute("sum(amt1)", "")));
                    anmt[1] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt2)", "")) ? 0.00 : dt4.Compute("sum(amt2)", "")));
                    anmt[2] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt3)", "")) ? 0.00 : dt4.Compute("sum(amt3)", "")));
                    anmt[3] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt4)", "")) ? 0.00 : dt4.Compute("sum(amt4)", "")));
                    anmt[4] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt5)", "")) ? 0.00 : dt4.Compute("sum(amt5)", "")));
                    anmt[5] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt6)", "")) ? 0.00 : dt4.Compute("sum(amt6)", "")));
                    anmt[6] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt7)", "")) ? 0.00 : dt4.Compute("sum(amt7)", "")));
                    anmt[7] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt8)", "")) ? 0.00 : dt4.Compute("sum(amt8)", "")));
                    anmt[8] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt9)", "")) ? 0.00 : dt4.Compute("sum(amt9)", "")));
                    anmt[9] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt10)", "")) ? 0.00 : dt4.Compute("sum(amt10)", "")));
                    anmt[10] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt11)", "")) ? 0.00 : dt4.Compute("sum(amt11)", "")));
                    anmt[11] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt12)", "")) ? 0.00 : dt4.Compute("sum(amt12)", "")));




                    ((Label)this.gvViewAR.FooterRow.FindControl("lgvFtoamtc")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(netamt)", "")) ? 0.00 : dt4.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvViewAR.FooterRow.FindControl("lgvFamt1c")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt1)", "")) ? 0.00 : dt4.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvViewAR.FooterRow.FindControl("lgvFamt2c")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt2)", "")) ? 0.00 : dt4.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvViewAR.FooterRow.FindControl("lgvFamt3c")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt3)", "")) ? 0.00 : dt4.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvViewAR.FooterRow.FindControl("lgvFamt4c")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt4)", "")) ? 0.00 : dt4.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvViewAR.FooterRow.FindControl("lgvFamt5c")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt5)", "")) ? 0.00 : dt4.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvViewAR.FooterRow.FindControl("lgvFamt6c")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt6)", "")) ? 0.00 : dt4.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvViewAR.FooterRow.FindControl("lgvFamt7c")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt7)", "")) ? 0.00 : dt4.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvViewAR.FooterRow.FindControl("lgvFamt8c")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt8)", "")) ? 0.00 : dt4.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvViewAR.FooterRow.FindControl("lgvFamt9c")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt9)", "")) ? 0.00 : dt4.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvViewAR.FooterRow.FindControl("lgvFamt10c")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt10)", "")) ? 0.00 : dt4.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvViewAR.FooterRow.FindControl("lgvFamt11c")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt11)", "")) ? 0.00 : dt4.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvViewAR.FooterRow.FindControl("lgvFamt12c")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt12)", "")) ? 0.00 : dt4.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");

                    foreach (int amt1 in anmt)
                    {
                        if (amt1 > 0)
                            ++counter;

                    }

                    double total = Convert.ToDouble(((Label)this.gvViewAR.FooterRow.FindControl("lgvFtoamtc")).Text);
                    double avrg = total / counter;
                    ((Label)this.gvViewAR.FooterRow.FindControl("lgvFtoavgamt")).Text = Convert.ToDouble(avrg).ToString("#,##0.00;(#,##0.00); ");

                    break;

                case "MonPaymentSumm":
                    dt4 = dt1.Copy();
                    dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("pactcode='99BBBBAAAAAA'");
                    dt4 = dv1.ToTable();

                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFtoamtmpaysum")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(toamt)", "")) ? 0.00 : dt4.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum1")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt1)", "")) ? 0.00 : dt4.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum2")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt2)", "")) ? 0.00 : dt4.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum3")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt3)", "")) ? 0.00 : dt4.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum4")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt4)", "")) ? 0.00 : dt4.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum5")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt5)", "")) ? 0.00 : dt4.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum6")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt6)", "")) ? 0.00 : dt4.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum7")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt7)", "")) ? 0.00 : dt4.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum8")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt8)", "")) ? 0.00 : dt4.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum9")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt9)", "")) ? 0.00 : dt4.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum10")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt10)", "")) ? 0.00 : dt4.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum11")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt11)", "")) ? 0.00 : dt4.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPaymentSumm.FooterRow.FindControl("lgvFamtmpaysum12")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt12)", "")) ? 0.00 : dt4.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "MonSalPerWise":
                    dt4 = dt1.Copy();

                    double[] amt = new double[12];
                    int count = 0;
                    amt[0] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt1)", "")) ? 0.00 : dt4.Compute("sum(amt1)", "")));
                    amt[1] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt2)", "")) ? 0.00 : dt4.Compute("sum(amt2)", "")));
                    amt[2] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt3)", "")) ? 0.00 : dt4.Compute("sum(amt3)", "")));
                    amt[3] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt4)", "")) ? 0.00 : dt4.Compute("sum(amt4)", "")));
                    amt[4] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt5)", "")) ? 0.00 : dt4.Compute("sum(amt5)", "")));
                    amt[5] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt6)", "")) ? 0.00 : dt4.Compute("sum(amt6)", "")));
                    amt[6] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt7)", "")) ? 0.00 : dt4.Compute("sum(amt7)", "")));
                    amt[7] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt8)", "")) ? 0.00 : dt4.Compute("sum(amt8)", "")));
                    amt[8] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt9)", "")) ? 0.00 : dt4.Compute("sum(amt9)", "")));
                    amt[9] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt10)", "")) ? 0.00 : dt4.Compute("sum(amt10)", "")));
                    amt[10] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt11)", "")) ? 0.00 : dt4.Compute("sum(amt11)", "")));
                    amt[11] = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt12)", "")) ? 0.00 : dt4.Compute("sum(amt12)", "")));

                    foreach (int amt1 in amt)
                    {
                        if (amt1 > 0)
                            ++count;

                    }

                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvtotalF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(toamt)", "")) ? 0.00 : dt4.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvjanF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt1)", "")) ? 0.00 : dt4.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvfebF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt2)", "")) ? 0.00 : dt4.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvmarF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt3)", "")) ? 0.00 : dt4.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvapF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt4)", "")) ? 0.00 : dt4.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvmayF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt5)", "")) ? 0.00 : dt4.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvjunF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt6)", "")) ? 0.00 : dt4.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvjulF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt7)", "")) ? 0.00 : dt4.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvaugF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt8)", "")) ? 0.00 : dt4.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvsepF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt9)", "")) ? 0.00 : dt4.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvoctF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt10)", "")) ? 0.00 : dt4.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvnovF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt11)", "")) ? 0.00 : dt4.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvdecF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt12)", "")) ? 0.00 : dt4.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");

                    double Total = Convert.ToDouble(((Label)this.gvSalPerWise.FooterRow.FindControl("lgvtotalF")).Text);

                    double avg = Total / count;

                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvavgamt")).Text = Convert.ToDouble(avg).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvtotalF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(toamt)", "")) ? 0.00 : dt4.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvjanF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt1)", "")) ? 0.00 : dt4.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvfebF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt2)", "")) ? 0.00 : dt4.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvmarF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt3)", "")) ? 0.00 : dt4.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvapF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt4)", "")) ? 0.00 : dt4.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvmayF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt5)", "")) ? 0.00 : dt4.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvjunF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt6)", "")) ? 0.00 : dt4.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvjulF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt7)", "")) ? 0.00 : dt4.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvaugF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt8)", "")) ? 0.00 : dt4.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvsepF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt9)", "")) ? 0.00 : dt4.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvoctF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt10)", "")) ? 0.00 : dt4.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvnovF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt11)", "")) ? 0.00 : dt4.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvdecF")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt12)", "")) ? 0.00 : dt4.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "CollBuyer":
                    dv1 = dt1.DefaultView;
                    dv1.RowFilter = ("usircode='000000000000'");
                    dt1 = dv1.ToTable();
                    ((Label)this.gvCollBuyer.FooterRow.FindControl("lgvdecFc")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amount)", "")) ? 0.00 : dt1.Compute("sum(amount)", ""))).ToString("#,##0;(#,##0); ");
                    break;



            }



        }



        protected void gvCollVsCleared_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCollVsCleared.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvCollVsCleared_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label date = (Label)e.Row.FindControl("lgvDate");
                Label collChqno = (Label)e.Row.FindControl("lgcollChqno");
                Label CollAmt = (Label)e.Row.FindControl("lgvCollAmt");
                Label ClChqno = (Label)e.Row.FindControl("lgvClChqno");
                Label cuamt = (Label)e.Row.FindControl("lgvclcuram");
                Label pramt = (Label)e.Row.FindControl("lgvClPramt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "clcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    //date.Font.Bold = true;
                    collChqno.Font.Bold = true;
                    CollAmt.Font.Bold = true;
                    ClChqno.Font.Bold = true;
                    cuamt.Font.Bold = true;
                    pramt.Font.Bold = true;
                    collChqno.Style.Add("text-align", "right");
                    ClChqno.Style.Add("text-align", "right");
                }
            }
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "CollVsClearance":
                    this.PrintCollVsClearacne();

                    break;
                case "DailyPayment":
                    if (this.rbtPayment.SelectedIndex == 1)
                    {
                        this.PrintDailyDetails();
                    }
                    else if (this.rbtPayment.SelectedIndex == 2)
                    {
                        this.PrintPaymentDetails();
                    }
                    else
                    {
                        this.PrintDailyPayment();
                    }
                    break;
                case "DetRealColl":
                    this.PrintRealCollDet();
                    break;

                case "MonCollection":
                case "MonCollHonoured":
                    this.PrintMonCollection();
                    break;
                case "MonPayment":
                case "MonReceipt":
                case "MonPaymentSumm":
                case "MonPaymentDet":
                    this.PrintMonRecorPayment();
                    break;
                case "MonSales":
                    this.PrintMonSales();
                    break;
                case "MonSalPerWise":
                    this.PrintSalePerWise();
                    break;
                case "MonAR":
                    this.PrintMonWiseCol();
                    break;
                case "CollBuyer":
                    this.PrintMonWiseColBuyer();
                    break;
                case "MonSalPerTarWise":
                    this.PrintMonSalPerTarWise();
                    break;
            }
        }
        private void PrintMonSalPerTarWise()
        {
 
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblcollvscl"];
            if(dt==null || dt.Rows.Count==0)
                return;

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_22_Sal.Sales_BO.MonSalPerTarWise>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptMonSalPerTarWise", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Monthly Target Sales"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("date", "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintMonCollection()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)ViewState["tblcollvscl"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptMonWiseCollection();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            txtHeader.Text = (this.Request.QueryString["Type"] == "MonCollHonoured") ? "Month Wise Collection (Honoured)" : "Month Wise Collection(Received)";

            TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )";


            DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            for (int i = 1; i <= 12; i++)
            {
                if (datefrm > dateto)
                    break;
                TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtamt" + i.ToString()] as TextObject;
                rpttxth.Text = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);

            }

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        private void PrintMonRecorPayment()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)ViewState["tblcollvscl"];
            if (dt1.Rows.Count == 0)
                return;

            var lst = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.EClassMonthWiseProjectPayment>();

            //Rdlc
            LocalReport Rpt1 = new LocalReport();

            if (this.Request.QueryString["Type"].ToString() == "MonPaymentDet")
            {
                Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptMonthWiseProjectPaymentDet", lst, null, null);

            }
            else
            {
                Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptMonthWiseProjectPayment", lst, null, null);

            }

            string title = (this.Request.QueryString["Type"].ToString() == "MonReceipt") ? "Month Wise Receipt"
                : (this.Request.QueryString["Type"].ToString() == "MonPaymentSumm") ? "Month Wise Payment-Summary"
                : (this.Request.QueryString["Type"].ToString() == "MonPaymentDet") ? "Month Wise Payment(Cost Wise)" : "Month Wise Payment(Project Wise)";

            DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            string date = "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )";
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("title", title));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));


            for (int i = 1; i <= 12; i++)
            {
                if (datefrm > dateto) break;

                Rpt1.SetParameters(new ReportParameter("month" + i.ToString(), datefrm.ToString("MMM yy")));
                datefrm = datefrm.AddMonths(1);

            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Crystal
            //ReportDocument rptstk = new ReportDocument();
            //if (this.Request.QueryString["Type"].ToString() == "MonPaymentDet")
            //{
            //    rptstk = new RealERPRPT.R_17_Acc.RptMonWisePaymentDet();
            //}
            //else
            //{
            //    rptstk = new RealERPRPT.R_17_Acc.RptMonWisePayment();
            //}
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = (this.Request.QueryString["Type"].ToString() == "MonReceipt") ? "Month Wise Receipt"
            //    : (this.Request.QueryString["Type"].ToString() == "MonPaymentSumm") ? "Month Wise Payment-Summary"
            //    : (this.Request.QueryString["Type"].ToString() == "MonPaymentDet") ? "Month Wise Payment(Cost Wise)" : "Month Wise Payment(Project Wise)";

            //TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )";


            //DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            //for (int i = 1; i <= 12; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtamt" + i.ToString()] as TextObject;
            //    rpttxth.Text = datefrm.ToString("MMM yy");
            //    datefrm = datefrm.AddMonths(1);

            //}

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt1);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintMonSales()
        {
            int index = this.rbtCol.SelectedIndex;
            switch (index)
            {
                case 0:
                    this.PrintMonSalesData();
                    break;
                case 1:
                    this.PrintMonSalesBar();
                    break;
                case 2:
                    this.PrintMonSalesLine();
                    break;
            }

        }

        private void PrintMonSalesData()
        {


            //Iqbal  Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblbgd"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_22_Sal.Sales_BO.MonthWisseSales>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptMonWiseCollection", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Month Wise Sales"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("date", "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)ViewState["tblcollvscl"];
            //if (dt1.Rows.Count == 0)
            //    return;
            //ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptMonWiseCollection();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;

            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = "Month Wise Sales";

            //TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )";


            //DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            //for (int i = 1; i <= 12; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtamt" + i.ToString()] as TextObject;
            //    rpttxth.Text = datefrm.ToString("MMM yy");
            //    datefrm = datefrm.AddMonths(1);

            //}

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt1);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintMonSalesBar()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            ////string hostname = hst["hostname"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt = (DataTable)ViewState["tblcollvscl"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "pactcode not like '18AAAAAAAAAA' and pactcode not like '99BBBBAAAAAA'";
            dt = dv.ToTable();

            List<RealEntity.C_17_Acc.RptMonWiseCol> List1 = new List<RealEntity.C_17_Acc.RptMonWiseCol>();
            List<RealEntity.C_17_Acc.RptMonthValue> list2 = new List<RealEntity.C_17_Acc.RptMonthValue>();

            double a1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", "")));
            double a2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", "")));
            double a3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", "")));
            double a4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", "")));
            double a5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", "")));
            double a6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", "")));
            double a7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", "")));
            double a8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", "")));
            double a9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", "")));
            double a10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", "")));
            double a11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", "")));
            double a12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", "")));
            double crore = 10000000;
            List1.Add(new RealEntity.C_17_Acc.RptMonWiseCol { amt1 = a1 / crore, amt2 = a2 / crore, amt3 = a3 / crore, amt4 = a4 / crore, amt5 = a5 / crore, amt6 = a6 / crore, amt7 = a7 / crore, amt8 = a8 / crore, amt9 = a9 / crore, amt10 = a10 / crore, amt11 = a11 / crore, amt12 = a12 / crore });


            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Jan", a1 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Feb", a2 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("March", a3 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("April", a4 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("May", a5 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("June", a6 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("July", a7 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Aug", a8 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Sep", a9 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Oct", a10 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Nov", a11 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Dec", a12 / crore));



            string month = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM");
            string year = Convert.ToDateTime(this.txttodate.Text).ToString("yyyy");
            string motnhRpt = month + "," + year;
            //double totalamt = Convert.ToDouble (((Label)this.gvViewAR.FooterRow.FindControl ("lgvFtoamtc")).Text);
            //string total = ((Label)this.gvViewAR.FooterRow.FindControl ("lgvFtoamtc")).Text;
            //string avg = ((Label)this.gvViewAR.FooterRow.FindControl ("lgvFtoavgamt")).Text;
            var list = dt.DataTableToList<RealEntity.C_17_Acc.RptMonWiseCol>();
            LocalReport Rpt1 = new LocalReport();




            Rpt1 = RDLCAccountSetup.GetLocalReport("R_17_Acc.RptMonSalesBarChart", list, list2, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters (new ReportParameter ("txtprint", printFooter));
            //Rpt1.SetParameters (new ReportParameter ("Rptmonth", motnhRpt));
            //Rpt1.SetParameters (new ReportParameter ("avg", avg));
            //Rpt1.SetParameters (new ReportParameter ("total", total));
            Rpt1.SetParameters(new ReportParameter("year", year));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            //Rpt1.SetParameters (new ReportParameter ("InWrd", "In Words : " + ASTUtility.Trans (Math.Round (totalamt), 2)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintMonSalesLine()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            ////string hostname = hst["hostname"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt = (DataTable)ViewState["tblcollvscl"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "pactcode not like '18AAAAAAAAAA' and pactcode not like '99BBBBAAAAAA'";
            dt = dv.ToTable();
            List<RealEntity.C_17_Acc.RptMonWiseCol> List1 = new List<RealEntity.C_17_Acc.RptMonWiseCol>();
            List<RealEntity.C_17_Acc.RptMonthValue> list2 = new List<RealEntity.C_17_Acc.RptMonthValue>();

            double a1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", "")));
            double a2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", "")));
            double a3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", "")));
            double a4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", "")));
            double a5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", "")));
            double a6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", "")));
            double a7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", "")));
            double a8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", "")));
            double a9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", "")));
            double a10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", "")));
            double a11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", "")));
            double a12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", "")));
            double crore = 10000000;
            List1.Add(new RealEntity.C_17_Acc.RptMonWiseCol { amt1 = a1 / crore, amt2 = a2 / crore, amt3 = a3 / crore, amt4 = a4 / crore, amt5 = a5 / crore, amt6 = a6 / crore, amt7 = a7 / crore, amt8 = a8 / crore, amt9 = a9 / crore, amt10 = a10 / crore, amt11 = a11 / crore, amt12 = a12 / crore });


            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Jan", a1 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Feb", a2 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("March", a3 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("April", a4 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("May", a5 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("June", a6 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("July", a7 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Aug", a8 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Sep", a9 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Oct", a10 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Nov", a11 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Dec", a12 / crore));




            string month = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM");
            string year = Convert.ToDateTime(this.txttodate.Text).ToString("yyyy");
            string motnhRpt = month + "," + year;
            //double totalamt = Convert.ToDouble (((Label)this.gvViewAR.FooterRow.FindControl ("lgvFtoamtc")).Text);
            //string total = ((Label)this.gvViewAR.FooterRow.FindControl ("lgvFtoamtc")).Text;
            //string avg = ((Label)this.gvViewAR.FooterRow.FindControl ("lgvFtoavgamt")).Text;
            var list = dt.DataTableToList<RealEntity.C_17_Acc.RptMonWiseCol>();
            LocalReport Rpt1 = new LocalReport();




            Rpt1 = RDLCAccountSetup.GetLocalReport("R_17_Acc.RptMonSalesLineChart", list, list2, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters (new ReportParameter ("txtprint", printFooter));
            //Rpt1.SetParameters (new ReportParameter ("Rptmonth", motnhRpt));
            //Rpt1.SetParameters (new ReportParameter ("avg", avg));
            //Rpt1.SetParameters (new ReportParameter ("total", total));
            Rpt1.SetParameters(new ReportParameter("year", year));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));


            //Rpt1.SetParameters (new ReportParameter ("InWrd", "In Words : " + ASTUtility.Trans (Math.Round (totalamt), 2)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintSalePerWise()
        {
            if (this.chkSal.Checked)
            {
                this.PrintSalePerProjWise();
            }
            else
            {

                this.PrintSalePerWiseQty();
            }



        }

        private void PrintSalePerWiseQty()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            ////string hostname = hst["hostname"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt1 = (DataTable)ViewState["tblcollvscl"];

            string month = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM");
            string year = Convert.ToDateTime(this.txttodate.Text).ToString("yyyy");
            string motnhRpt = "Year " + month + "-" + year;

            string avg = ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvavgamt")).Text;
            var list = dt1.DataTableToList<RealEntity.C_17_Acc.RptSalPerWise>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_17_Acc.RptSalPerWise", list, null, null);
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtprint", printFooter));
            Rpt1.SetParameters(new ReportParameter("Rptmonth", motnhRpt));
            Rpt1.SetParameters(new ReportParameter("avg", avg));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintSalePerProjWise()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            ////string hostname = hst["hostname"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt1 = (DataTable)ViewState["tblcollvscl"];

            string month = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM");
            string year = Convert.ToDateTime(this.txttodate.Text).ToString("yyyy");
            string motnhRpt = "Year " + month + "-" + year;

            string avg = ((Label)this.gvSalPerWise.FooterRow.FindControl("lgvavgamt")).Text;
            var list = dt1.DataTableToList<RealEntity.C_17_Acc.RptSalPerWise>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_17_Acc.RptSalPerProjWise", list, null, null);
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtprint", printFooter));
            Rpt1.SetParameters(new ReportParameter("Rptmonth", motnhRpt));
            Rpt1.SetParameters(new ReportParameter("avg", avg));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintMonWiseCol()
        {
            int index = this.rbtCol.SelectedIndex;
            switch (index)
            {
                case 0:
                    this.printMonthWisedata();
                    break;
                case 1:
                    this.printMonthWisebar();
                    break;
                case 2:
                    this.printMonthWiseline();
                    break;
                default:
                    break;


            }
        }

        private void PrintMonWiseColBuyer()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            ////string hostname = hst["hostname"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt1 = (DataTable)ViewState["tblcollvscl"];

            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string total = ((Label)this.gvCollBuyer.FooterRow.FindControl("lgvdecFc")).Text;

            var list = dt1.DataTableToList<RealEntity.C_17_Acc.RptMonWiseColBuyer>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_17_Acc.RptMonWiseColBuyer", list, null, null);
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("frmdate", frmdate));
            Rpt1.SetParameters(new ReportParameter("todate", todate));
            Rpt1.SetParameters(new ReportParameter("total", total));



            // Rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(totalamt), 2)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void printMonthWisedata()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            ////string hostname = hst["hostname"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt1 = (DataTable)ViewState["tblcollvscl"];

            string month = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM");
            string year = Convert.ToDateTime(this.txttodate.Text).ToString("yyyy");
            string motnhRpt = month + "," + year;
            double totalamt = Convert.ToDouble(((Label)this.gvViewAR.FooterRow.FindControl("lgvFtoamtc")).Text);
            string total = ((Label)this.gvViewAR.FooterRow.FindControl("lgvFtoamtc")).Text;
            string avg = ((Label)this.gvViewAR.FooterRow.FindControl("lgvFtoavgamt")).Text;
            var list = dt1.DataTableToList<RealEntity.C_17_Acc.RptMonWiseCol>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_17_Acc.RptMonWiseCol", list, null, null);
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtprint", printFooter));
            Rpt1.SetParameters(new ReportParameter("Rptmonth", motnhRpt));
            Rpt1.SetParameters(new ReportParameter("avg", avg));
            Rpt1.SetParameters(new ReportParameter("total", total));
            Rpt1.SetParameters(new ReportParameter("year", year));

            Rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(totalamt), 2)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void printMonthWisebar()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            ////string hostname = hst["hostname"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt = (DataTable)ViewState["tblcollvscl"];

            List<RealEntity.C_17_Acc.RptMonWiseCol> List1 = new List<RealEntity.C_17_Acc.RptMonWiseCol>();
            List<RealEntity.C_17_Acc.RptMonthValue> list2 = new List<RealEntity.C_17_Acc.RptMonthValue>();

            double a1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", "")));
            double a2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", "")));
            double a3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", "")));
            double a4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", "")));
            double a5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", "")));
            double a6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", "")));
            double a7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", "")));
            double a8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", "")));
            double a9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", "")));
            double a10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", "")));
            double a11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", "")));
            double a12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", "")));
            double crore = 10000000;
            List1.Add(new RealEntity.C_17_Acc.RptMonWiseCol { amt1 = a1 / crore, amt2 = a2 / crore, amt3 = a3 / crore, amt4 = a4 / crore, amt5 = a5 / crore, amt6 = a6 / crore, amt7 = a7 / crore, amt8 = a8 / crore, amt9 = a9 / crore, amt10 = a10 / crore, amt11 = a11 / crore, amt12 = a12 / crore });


            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Jan", a1 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Feb", a2 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("March", a3 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("April", a4 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("May", a5 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("June", a6 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("July", a7 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Aug", a8 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Sep", a9 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Oct", a10 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Nov", a11 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Dec", a12 / crore));




            string month = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM");
            string year = Convert.ToDateTime(this.txttodate.Text).ToString("yyyy");
            string motnhRpt = month + "," + year;
            double totalamt = Convert.ToDouble(((Label)this.gvViewAR.FooterRow.FindControl("lgvFtoamtc")).Text);
            string total = ((Label)this.gvViewAR.FooterRow.FindControl("lgvFtoamtc")).Text;
            string avg = ((Label)this.gvViewAR.FooterRow.FindControl("lgvFtoavgamt")).Text;
            var list = dt.DataTableToList<RealEntity.C_17_Acc.RptMonWiseCol>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RDLCAccountSetup.GetLocalReport("R_17_Acc.RptMonColBarChart", list, list2, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters (new ReportParameter ("txtprint", printFooter));
            //Rpt1.SetParameters (new ReportParameter ("Rptmonth", motnhRpt));
            //Rpt1.SetParameters (new ReportParameter ("avg", avg));
            //Rpt1.SetParameters (new ReportParameter ("total", total));
            Rpt1.SetParameters(new ReportParameter("year", year));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            //Rpt1.SetParameters (new ReportParameter ("InWrd", "In Words : " + ASTUtility.Trans (Math.Round (totalamt), 2)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void printMonthWiseline()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            ////string hostname = hst["hostname"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt = (DataTable)ViewState["tblcollvscl"];

            List<RealEntity.C_17_Acc.RptMonWiseCol> List1 = new List<RealEntity.C_17_Acc.RptMonWiseCol>();
            List<RealEntity.C_17_Acc.RptMonthValue> list2 = new List<RealEntity.C_17_Acc.RptMonthValue>();

            double a1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", "")));
            double a2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", "")));
            double a3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", "")));
            double a4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", "")));
            double a5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", "")));
            double a6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", "")));
            double a7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", "")));
            double a8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", "")));
            double a9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", "")));
            double a10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", "")));
            double a11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", "")));
            double a12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", "")));
            double crore = 10000000;
            List1.Add(new RealEntity.C_17_Acc.RptMonWiseCol { amt1 = a1 / crore, amt2 = a2 / crore, amt3 = a3 / crore, amt4 = a4 / crore, amt5 = a5 / crore, amt6 = a6 / crore, amt7 = a7 / crore, amt8 = a8 / crore, amt9 = a9 / crore, amt10 = a10 / crore, amt11 = a11 / crore, amt12 = a12 / crore });


            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Jan", a1 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Feb", a2 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("March", a3 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("April", a4 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("May", a5 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("June", a6 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("July", a7 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Aug", a8 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Sep", a9 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Oct", a10 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Nov", a11 / crore));
            list2.Add(new RealEntity.C_17_Acc.RptMonthValue("Dec", a12 / crore));




            string month = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM");
            string year = Convert.ToDateTime(this.txttodate.Text).ToString("yyyy");
            string motnhRpt = month + "," + year;
            double totalamt = Convert.ToDouble(((Label)this.gvViewAR.FooterRow.FindControl("lgvFtoamtc")).Text);
            string total = ((Label)this.gvViewAR.FooterRow.FindControl("lgvFtoamtc")).Text;
            string avg = ((Label)this.gvViewAR.FooterRow.FindControl("lgvFtoavgamt")).Text;
            var list = dt.DataTableToList<RealEntity.C_17_Acc.RptMonWiseCol>();
            LocalReport Rpt1 = new LocalReport();



            Rpt1 = RDLCAccountSetup.GetLocalReport("R_17_Acc.RptMonColLineChart", list, list2, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters (new ReportParameter ("txtprint", printFooter));
            //Rpt1.SetParameters (new ReportParameter ("Rptmonth", motnhRpt));
            //Rpt1.SetParameters (new ReportParameter ("avg", avg));
            //Rpt1.SetParameters (new ReportParameter ("total", total));
            Rpt1.SetParameters(new ReportParameter("year", year));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));


            //Rpt1.SetParameters (new ReportParameter ("InWrd", "In Words : " + ASTUtility.Trans (Math.Round (totalamt), 2)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintCollVsClearacne()
        {
            //Iqbal  Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblcollvscl"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.ChequeReceiClear>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RrptChqReceivedVsClr", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Date", "From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("d-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("Cheque Received Vs. Clearance"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod =GetCompCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //ReportDocument rptstate = new RealERPRPT.R_17_Acc.rptChqReceivedVsClr();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;


            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptftdate.Text = "(From " + fromdate + " To " + todate + ")";
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)ViewState["tblcollvscl"]);

            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintDailyPayment()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataTable dt = (DataTable)ViewState["tblcollvscl"];

            string date = "(From " + fromdate + " To " + todate + ")";

            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptDailyPayment>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptDailyPayment1", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Daily Payment Summary"));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //ReportDocument rptstate = new RealERPRPT.R_17_Acc.RptDailyPaymentSumm();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptftdate.Text = "(From " + fromdate + " To " + todate + ")";
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)ViewState["tblcollvscl"]);
            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintDailyDetails()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataTable dt = (DataTable)ViewState["tblcollvscl"];

            DataTable dt1 = new DataTable();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "actcode2 = '000000000000'";
            dt1 = dv.ToTable();

            string totaldram = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(dram)", "")) ?
                 0 : dt1.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");

            string date = "(From " + fromdate + " To " + todate + ")";

            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptDailyPaymentSummaryCostWise>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptDailyPaymentCostWise1", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Daily Payment Summary - Cost Wise"));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("totaldram", totaldram));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //ReportDocument rptstate = new RealERPRPT.R_17_Acc.RptDailyPaymentCostDet();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;


            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptftdate.Text = "(From " + fromdate + " To " + todate + ")";

            //TextObject rpttotal = rptstate.ReportDefinition.ReportObjects["txttotal"] as TextObject;
            //rpttotal.Text = ((Label)this.gvPayDetails.FooterRow.FindControl("lgvFTDrAmt")).Text;


            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)ViewState["tblcollvscl"]);

            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintPaymentDetails()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataTable dt = (DataTable)ViewState["tblcollvscl"];

            DataView dv = dt.DefaultView;
            dv.RowFilter = "rescode not like  '%0000'";
            dt = dv.ToTable();

            string date = "(From " + fromdate + " To " + todate + ")";

            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptDailyPaymentDetails>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptDailyPaymentDetails1", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Daily Payment Details"));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptstate = new RealERPRPT.R_17_Acc.RptPaymentDetails();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptftdate.Text = "(From " + fromdate + " To " + todate + ")";
            //TextObject rpAmt = rptstate.ReportDefinition.ReportObjects["txtPayAmt"] as TextObject;
            //rpAmt.Text = ((Label)this.gvPayDet.FooterRow.FindControl("lgvFPayAmt")).Text;
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)ViewState["tblcollvscl"]);

            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        private void PrintRealCollDet()
        {
            //Iqbal  Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblcollvscl"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_22_Sal.Sales_BO.SaleSummarySum>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptRealCollDetails", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Date", "From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("d-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Real Collection - Details"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod =GetCompCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //ReportDocument rptstate = new RealERPRPT.R_17_Acc.RptRealCollDetails();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;


            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            //rptftdate.Text = "Date: " + fromdate + " To " + todate;
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)ViewState["tblcollvscl"]);
            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvDPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDPayment.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvPayDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Payamt = (Label)e.Row.FindControl("lgvAmt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "A")
                {
                    Payamt.Font.Bold = true;
                    Payamt.Style.Add("text-align", "left");


                }
            }
        }
        protected void gvCollDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCollDet.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


        protected void gvCollDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label udesc = (Label)e.Row.FindControl("lgvudesc");
                Label cashamt = (Label)e.Row.FindControl("lgvcashamt");
                Label chqamt = (Label)e.Row.FindControl("lgvchqamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    udesc.Font.Bold = true;
                    cashamt.Font.Bold = true;
                    chqamt.Font.Bold = true;
                    udesc.Style.Add("text-align", "right");
                }

            }

        }
        protected void gvPayDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPayDet.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvPayDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Rescode = (Label)e.Row.FindControl("lgvResCod");
                Label Resdesc = (Label)e.Row.FindControl("lgvResName");
                Label Payamt = (Label)e.Row.FindControl("lgvPayAmt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "A")
                {

                    Rescode.Font.Bold = true;
                    Resdesc.Font.Bold = true;
                    Payamt.Font.Bold = true;
                    Payamt.Style.Add("text-align", "left");


                }
                if (code == "B")
                {
                    Rescode.Style.Add("text-align", "right");


                }

            }
        }
        protected void gvMonCollect_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink HygvResDesc = (HyperLink)e.Row.FindControl("HygvResDesc");
                HyperLink lgvtoamt = (HyperLink)e.Row.FindControl("hlnkgvtoamt");
                HyperLink lgvamt1 = (HyperLink)e.Row.FindControl("hlnkgvamt1");
                HyperLink lgvamt2 = (HyperLink)e.Row.FindControl("hlnkgvamt2");
                HyperLink lgvamt3 = (HyperLink)e.Row.FindControl("hlnkgvamt3");
                HyperLink lgvamt4 = (HyperLink)e.Row.FindControl("hlnkgvamt4");
                HyperLink lgvamt5 = (HyperLink)e.Row.FindControl("hlnkgvamt5");
                HyperLink lgvamt6 = (HyperLink)e.Row.FindControl("hlnkgvamt6");
                HyperLink lgvamt7 = (HyperLink)e.Row.FindControl("hlnkgvamt7");
                HyperLink lgvamt8 = (HyperLink)e.Row.FindControl("hlnkgvamt8");
                HyperLink lgvamt9 = (HyperLink)e.Row.FindControl("hlnkgvamt9");
                HyperLink lgvamt10 = (HyperLink)e.Row.FindControl("hlnkgvamt10");
                HyperLink lgvamt11 = (HyperLink)e.Row.FindControl("hlnkgvamt11");
                HyperLink lgvamt12 = (HyperLink)e.Row.FindControl("hlnkgvamt12");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    HygvResDesc.Font.Bold = true;
                    lgvtoamt.Font.Bold = true;
                    lgvamt1.Font.Bold = true;
                    lgvamt2.Font.Bold = true;
                    lgvamt3.Font.Bold = true;
                    lgvamt4.Font.Bold = true;
                    lgvamt5.Font.Bold = true;
                    lgvamt6.Font.Bold = true;
                    lgvamt7.Font.Bold = true;
                    lgvamt8.Font.Bold = true;
                    lgvamt9.Font.Bold = true;
                    lgvamt10.Font.Bold = true;
                    lgvamt11.Font.Bold = true;
                    lgvamt12.Font.Bold = true;
                    HygvResDesc.Style.Add("text-align", "right");
                }
                if (Request.QueryString["Type"] == "MonSales")
                {
                    if (ASTUtility.Left(code, 2) == "18" || ASTUtility.Left(code, 2) == "24")
                    {
                        HygvResDesc.NavigateUrl = "LinkAccount.aspx?Type=SalesProj&pactcode=" + code + "&Date1=" + this.txtfromdate.Text.Trim() + "&Date2=" + this.txttodate.Text.Trim();
                    }
                }
            }
        }


        protected void gvMonPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvActdescmpay = (Label)e.Row.FindControl("lgvActdescmpay");
                Label lgvopening = (Label)e.Row.FindControl("lgvopening");
                Label lgvnetTotal = (Label)e.Row.FindControl("lgvnetTotal");

                Label lgvtoamtmpay = (Label)e.Row.FindControl("lgvtoamtmpay");
                Label lgvamtmpay1 = (Label)e.Row.FindControl("lgvamtmpay1");
                Label lgvamtmpay2 = (Label)e.Row.FindControl("lgvamtmpay2");
                Label lgvamtmpay3 = (Label)e.Row.FindControl("lgvamtmpay3");
                Label lgvamtmpay4 = (Label)e.Row.FindControl("lgvamtmpay4");
                Label lgvamtmpay5 = (Label)e.Row.FindControl("lgvamtmpay5");
                Label lgvamtmpay6 = (Label)e.Row.FindControl("lgvamtmpay6");
                Label lgvamtmpay7 = (Label)e.Row.FindControl("lgvamtmpay7");
                Label lgvamtmpay8 = (Label)e.Row.FindControl("lgvamtmpay8");
                Label lgvamtmpay9 = (Label)e.Row.FindControl("lgvamtmpay9");
                Label lgvamtmpay10 = (Label)e.Row.FindControl("lgvamtmpay10");
                Label lgvamtmpay11 = (Label)e.Row.FindControl("lgvamtmpay11");
                Label lgvamtmpay12 = (Label)e.Row.FindControl("lgvamtmpay12");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lgvActdescmpay.Font.Bold = true;
                    lgvopening.Font.Bold = true;

                    lgvnetTotal.Font.Bold = true;
                    lgvtoamtmpay.Font.Bold = true;
                    lgvamtmpay1.Font.Bold = true;
                    lgvamtmpay2.Font.Bold = true;
                    lgvamtmpay3.Font.Bold = true;
                    lgvamtmpay4.Font.Bold = true;
                    lgvamtmpay5.Font.Bold = true;
                    lgvamtmpay6.Font.Bold = true;
                    lgvamtmpay7.Font.Bold = true;
                    lgvamtmpay8.Font.Bold = true;
                    lgvamtmpay9.Font.Bold = true;
                    lgvamtmpay10.Font.Bold = true;
                    lgvamtmpay11.Font.Bold = true;
                    lgvamtmpay12.Font.Bold = true;
                    lgvActdescmpay.Style.Add("text-align", "right");
                }
                if (this.Request.QueryString["Type"] == "MonPaymentDet" || this.Request.QueryString["Type"] == "MonReceipt")
                {
                    if (ASTUtility.Right(code, 8) == "00000000")
                    {

                        lgvActdescmpay.Font.Bold = true;
                        lgvtoamtmpay.Font.Bold = true;
                        lgvamtmpay1.Font.Bold = true;
                        lgvamtmpay2.Font.Bold = true;
                        lgvamtmpay3.Font.Bold = true;
                        lgvamtmpay4.Font.Bold = true;
                        lgvamtmpay5.Font.Bold = true;
                        lgvamtmpay6.Font.Bold = true;
                        lgvamtmpay7.Font.Bold = true;
                        lgvamtmpay8.Font.Bold = true;
                        lgvamtmpay9.Font.Bold = true;
                        lgvamtmpay10.Font.Bold = true;
                        lgvamtmpay11.Font.Bold = true;
                        lgvamtmpay12.Font.Bold = true;
                        //lgvActdescmpay.Style.Add("text-align", "right");
                    }
                }

            }

        }
        protected void gvMonPaymentSumm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink HLgvDescpaysum = (HyperLink)e.Row.FindControl("HLgvDescpaysum");
                Label lgvtoamtmpaysum = (Label)e.Row.FindControl("lgvtoamtmpaysum");
                Label lgvamtmpaysum1 = (Label)e.Row.FindControl("lgvamtmpaysum1");
                Label lgvamtmpaysum2 = (Label)e.Row.FindControl("lgvamtmpaysum2");
                Label lgvamtmpaysum3 = (Label)e.Row.FindControl("lgvamtmpaysum3");
                Label lgvamtmpaysum4 = (Label)e.Row.FindControl("lgvamtmpaysum4");
                Label lgvamtmpaysum5 = (Label)e.Row.FindControl("lgvamtmpaysum5");
                Label lgvamtmpaysum6 = (Label)e.Row.FindControl("lgvamtmpaysum6");
                Label lgvamtmpaysum7 = (Label)e.Row.FindControl("lgvamtmpaysum7");
                Label lgvamtmpaysum8 = (Label)e.Row.FindControl("lgvamtmpaysum8");
                Label lgvamtmpaysum9 = (Label)e.Row.FindControl("lgvamtmpaysum9");
                Label lgvamtmpaysum10 = (Label)e.Row.FindControl("lgvamtmpaysum10");
                Label lgvamtmpaysum11 = (Label)e.Row.FindControl("lgvamtmpaysum11");
                Label lgvamtmpaysum12 = (Label)e.Row.FindControl("lgvamtmpaysum12");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    HLgvDescpaysum.Font.Bold = true;
                    lgvtoamtmpaysum.Font.Bold = true;
                    lgvamtmpaysum1.Font.Bold = true;
                    lgvamtmpaysum2.Font.Bold = true;
                    lgvamtmpaysum3.Font.Bold = true;
                    lgvamtmpaysum4.Font.Bold = true;
                    lgvamtmpaysum5.Font.Bold = true;
                    lgvamtmpaysum6.Font.Bold = true;
                    lgvamtmpaysum7.Font.Bold = true;
                    lgvamtmpaysum8.Font.Bold = true;
                    lgvamtmpaysum9.Font.Bold = true;
                    lgvamtmpaysum10.Font.Bold = true;
                    lgvamtmpaysum11.Font.Bold = true;
                    lgvamtmpaysum12.Font.Bold = true;
                    HLgvDescpaysum.Style.Add("text-align", "right");
                }

                if (grp == "4" && ASTUtility.Right(code, 10) == "0000000000")
                {

                    string pactdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactdesc")).ToString();

                    HLgvDescpaysum.NavigateUrl = "~/F_32_Mis/LinkMis.aspx?Type=ResCostDet&rescode=" + code + "&resdesc=" + pactdesc + "&frmdate=" + this.txtfromdate.Text.Trim() + "&todate=" + this.txttodate.Text.Trim();

                }




            }
        }

        protected void gvViewAR_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HlnkResDesc");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                hlink1.Style.Add("color", "blue");
                //HyperLink lgvamt1 = (HyperLink)e.Row.FindControl("hlnkgvamt1c");
                //HyperLink lgvamt2 = (HyperLink)e.Row.FindControl("hlnkgvamt2c");
                //HyperLink lgvamt3 = (HyperLink)e.Row.FindControl("hlnkgvamt3c");
                //HyperLink lgvamt4 = (HyperLink)e.Row.FindControl("hlnkgvamt4c");
                //HyperLink lgvamt5 = (HyperLink)e.Row.FindControl("hlnkgvamt5c");
                //HyperLink lgvamt6 = (HyperLink)e.Row.FindControl("hlnkgvamt6c");
                //HyperLink lgvamt7 = (HyperLink)e.Row.FindControl("hlnkgvamt7c");
                //HyperLink lgvamt8 = (HyperLink)e.Row.FindControl("hlnkgvamt8c");
                //HyperLink lgvamt9 = (HyperLink)e.Row.FindControl("hlnkgvamt9c");
                //HyperLink lgvamt10 = (HyperLink)e.Row.FindControl("hlnkgvamt10c");
                //HyperLink lgvamt11 = (HyperLink)e.Row.FindControl("hlnkgvamt11c");
                //HyperLink lgvamt12 = (HyperLink)e.Row.FindControl("hlnkgvamt12c");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string pactdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactdesc")).ToString();

                if (code == "")
                {
                    return;
                }


                DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
                DateTime fdateto;



                for (int i = 1; i <= 12; i++)
                {
                    if (datefrm > dateto)
                        break;



                    HyperLink lgvamt = (HyperLink)e.Row.FindControl("hlnkgvamt" + i.ToString() + "c");
                    fdateto = datefrm.AddMonths(1).AddDays(-1);
                    lgvamt.NavigateUrl = "~/F_23_CR/LinkRptSaleDues.aspx?Type=WeeklyColl&pactcode=" + code + "&pactdesc=" + pactdesc + "&date1=" + datefrm.ToString("dd-MMM-yyyy") + " &date2=" + fdateto.ToString("dd-MMM-yyyy");
                    datefrm = datefrm.AddMonths(1);
                }


                hlink1.NavigateUrl = "~/F_22_Sal/RptTransactionSt.aspx?Type=TransDateWise&prjcode=" + pactcode + "&Date1=" + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " &Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            }
        }
        protected void gvCollBuyer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label hlink1 = (Label)e.Row.FindControl("lgvtoamtc");
                Label hlink2 = (Label)e.Row.FindControl("lblprjbuyer");


                string usircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();

                if (usircode == "000000000000")
                {
                    hlink1.Style.Add("color", "maroon");
                    hlink1.Style.Add("font-weight", "bold");
                    hlink2.Style.Add("color", "maroon");
                    hlink2.Style.Add("font-weight", "bold");
                }
            }
        }
        protected void gvCollBuyer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvCollBuyer.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvSalPerWise_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {


                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);


                TableCell cell01 = new TableCell();
                cell01.Text = "Sl.No.";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.RowSpan = 2;
                gvrow.Cells.Add(cell01);



                TableCell cell02 = new TableCell();
                cell02.Text = "Name";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.RowSpan = 2;
                gvrow.Cells.Add(cell02);



                TableCell cell03 = new TableCell();
                cell03.Text = "Project Name";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.RowSpan = 2;
                gvrow.Cells.Add(cell03);


                TableCell cell04 = new TableCell();
                cell04.Text = "Jan";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 2;
                gvrow.Cells.Add(cell04);

                TableCell cell05 = new TableCell();
                cell05.Text = "Feb";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 2;
                gvrow.Cells.Add(cell05);

                TableCell cell06 = new TableCell();
                cell06.Text = "Mar";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 2;
                gvrow.Cells.Add(cell06);

                TableCell cell07 = new TableCell();
                cell07.Text = "Apr";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.ColumnSpan = 2;
                gvrow.Cells.Add(cell07);


                TableCell cell08 = new TableCell();
                cell08.Text = "May";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.ColumnSpan = 2;
                gvrow.Cells.Add(cell08);


                TableCell cell09 = new TableCell();
                cell09.Text = "June";
                cell09.HorizontalAlign = HorizontalAlign.Center;
                cell09.ColumnSpan = 2;
                gvrow.Cells.Add(cell09);


                TableCell cell10 = new TableCell();
                cell10.Text = "July";
                cell10.HorizontalAlign = HorizontalAlign.Center;
                cell10.ColumnSpan = 2;
                gvrow.Cells.Add(cell10);


                TableCell cell11 = new TableCell();
                cell11.Text = "Aug";
                cell11.HorizontalAlign = HorizontalAlign.Center;
                cell11.ColumnSpan = 2;
                gvrow.Cells.Add(cell11);


                TableCell cell12 = new TableCell();
                cell12.Text = "Sep";
                cell12.HorizontalAlign = HorizontalAlign.Center;
                cell12.ColumnSpan = 2;
                gvrow.Cells.Add(cell12);


                TableCell cell13 = new TableCell();
                cell13.Text = "Oct";
                cell13.HorizontalAlign = HorizontalAlign.Center;
                cell13.ColumnSpan = 2;
                gvrow.Cells.Add(cell13);


                TableCell cell14 = new TableCell();
                cell14.Text = "Nov";
                cell14.HorizontalAlign = HorizontalAlign.Center;
                cell14.ColumnSpan = 2;
                gvrow.Cells.Add(cell14);


                TableCell cell15 = new TableCell();
                cell15.Text = "Dec";
                cell15.HorizontalAlign = HorizontalAlign.Center;
                cell15.ColumnSpan = 2;
                gvrow.Cells.Add(cell15);


                TableCell cell16 = new TableCell();
                cell16.Text = "Total";
                cell16.HorizontalAlign = HorizontalAlign.Center;
                cell16.ColumnSpan = 2;
                gvrow.Cells.Add(cell16);

                gvSalPerWise.Controls[0].Controls.AddAt(0, gvrow);



            }





        }
        protected void gvSalPerWise_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;


            }

        }
    }
}
