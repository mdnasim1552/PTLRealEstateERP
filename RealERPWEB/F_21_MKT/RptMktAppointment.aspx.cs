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
using System.Collections.Generic;
using RealERPLIB;
using RealERPRPT;
using System.Net.Mail;
using RealEntity;
namespace RealERPWEB.F_21_MKT
{

    public partial class RptMktAppointment : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        private UserManagerKPI objUser = new UserManagerKPI();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "Todaysdis") ? "TODAY'S DISCUSSION REPORT"
                //    : this.Request.QueryString["Type"].ToString().Trim() == "DiscussHis" ? "DISCUSSION HISTORY REPORT"
                //    : this.Request.QueryString["Type"].ToString().Trim() == "NextApp" ? "NEXT APPOINTMENT REPORT"
                //    : this.Request.QueryString["Type"].ToString().Trim() == "SalePerformance" ? "SALES PERFORMANCE REPORT"
                //    : this.Request.QueryString["Type"].ToString().Trim() == "OffPerformance" ? "SALES PERSON HISTORY"
                //    : this.Request.QueryString["Type"].ToString().Trim() == "SendOnlineLetter" ? "Send Letter(Online)"
                //    : this.Request.QueryString["Type"].ToString().Trim() == "AllOffPerformance" ? "SALES PERSON HISTORY (ALL)"
                //    : this.Request.QueryString["Type"].ToString().Trim() == "ClientTrans" ? "Client Transfer" : "CLIENT LETTER INFORMATION REPORT";
                this.GetSalesList();
                this.ShowView();


            }
        }

        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }

        private void GetSalesList()
        {
            //-----------Get Sales Person List ---------------//
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //Session.Remove("tblsaleteam");
            string comcod = Getcomcod();
            string txtsrchteam = "%" + this.txtSrcteam.Text.Trim() + "%";
            string userid = (this.Request.QueryString["UType"] == "Client") ? hst["usrid"].ToString() : "";


            //DataSet dss = this.MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CLIENT_INFORMATION", "GETMKTTEAM", txtsrchteam, userid, "", "", "", "", "", "", "");

            string deptcode = "9402%";
            string srchEmp = "%" + "%";
            string Date = System.DateTime.Now.ToString("yyyyMM");
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lst3 = objUser.GetEmpCode2(srchEmp, userid, deptcode, Date);


            //DataSet dss = this.MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "GETMKTTEAM", txtsrchteam, userid, "", "", "", "", "", "", "");
            this.ddlSalesTeam.DataTextField = "empname";
            this.ddlSalesTeam.DataValueField = "empid";
            this.ddlSalesTeam.DataSource = lst3;
            this.ddlSalesTeam.DataBind();
            //Session["tblsaleteam"]=dss.Tables[0];
            //dss.Dispose();
        }
        private void GetClientList()
        {
            if (this.ddlSalesTeam.Items.Count == 0)
                return;

            string comcod = this.Getcomcod();
            string teamcode = this.ddlSalesTeam.SelectedValue.ToString();
            //string datefrom = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            //string dateto =Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //string txtSerch = "%" + this.txtSrchclient.Text.Trim() + "%";
            DataSet dset = this.MktData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_CLIENT_INFORMATION", "GETCLIENT", teamcode, "", "", "", "", "");
            this.ddlClientList.DataTextField = "prosdesc";
            this.ddlClientList.DataValueField = "proscod";
            this.ddlClientList.DataSource = dset.Tables[0];
            this.ddlClientList.DataBind();
        }

        private void ShowView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Todaysdis":
                    this.lbltodate.Visible = false;
                    this.txttodate.Visible = false;
                    this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "DiscussHis":
                    this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txttodate.Text = System.DateTime.Today.AddDays(15).ToString("dd-MMM-yyyy");
                    this.GetClientList();
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "NextApp":
                    this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txttodate.Text = System.DateTime.Today.AddDays(15).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "OffPerformance":
                    this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txttodate.Text = System.DateTime.Today.AddDays(15).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "SalePerformance":
                    this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txttodate.Text = System.DateTime.Today.AddDays(15).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 3;
                    break;

                case "ClientLetter":
                case "SendOnlineLetter":
                    //this.lblSalesTeam.Visible = false;
                    //this.txtSrcteam.Visible = false;
                    //this.imgbtnteam.Visible = false;
                    //this.ddlSalesTeam.Visible = false;
                    this.lblfrmdate.Visible = false;
                    this.txtfrmdate.Visible = false;
                    this.lbltodate.Visible = false;
                    this.txttodate.Visible = false;
                    this.MultiView1.ActiveViewIndex = 4;
                    break;
                case "ProsClient":

                    this.lblSalesTeam.Visible = false;
                    this.txtSrcteam.Visible = false;
                    this.imgbtnteam.Visible = false;
                    this.ddlSalesTeam.Visible = false;
                    this.lbltodate.Visible = false;
                    this.txttodate.Visible = false;
                    this.lblfrmdate.Visible = false;
                    this.txtfrmdate.Visible = false;
                    this.MultiView1.ActiveViewIndex = 5;
                    break;

                case "AllOffPerformance":
                    this.lblSalesTeam.Visible = false;
                    this.txtSrcteam.Visible = false;
                    this.imgbtnteam.Visible = false;
                    this.ddlSalesTeam.Visible = false;
                    this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txttodate.Text = System.DateTime.Today.AddDays(15).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 6;
                    break;

                case "ClientTrans":

                    this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txttodate.Text = System.DateTime.Today.AddDays(15).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 7;
                    break;



            }





        }



        protected void imgbtnProSrch_Click(object sender, EventArgs e)
        {

        }
        protected void lbntOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            switch (type)
            {
                case "Todaysdis":
                    this.ShowToDiscussion();
                    break;

                case "NextApp":
                    this.ShowNextApp();
                    break;

                case "DiscussHis":
                    showCleintHis();
                    break;

                case "OffPerformance":
                    showOffPerformance();
                    break;

                case "SalePerformance":
                    showSalePerformance();
                    break;


                case "ClientLetter":
                case "SendOnlineLetter":
                    this.PanelSendMail.Visible = (type == "SendOnlineLetter") ? true : false;
                    this.ShowClinetLetterInfo();
                    break;
                case "ProsClient":
                    this.GetProsClientList();
                    break;

                case "AllOffPerformance":
                    this.showALLOffPerformance();
                    break;


                case "ClientTrans":
                    this.showClientTrans();
                    break;


            }

        }

        private void ShowToDiscussion()
        {
            if (this.ddlSalesTeam.Items.Count == 0)
                return;
            Session.Remove("tbltoapp");
            string comcod = this.Getcomcod();
            string teamcode = this.ddlSalesTeam.SelectedValue.ToString();

            // string teamcode = this.ddlSalesTeam.SelectedValue.Substring(0, 14).ToString();
            string date = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CLIENT_INFORMATION", "RPTTODAYSDIS", teamcode, date, "", "", "", "", "", "", "");

            //DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "RPTTODAYSDIS", teamcode, date, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvtodayapp.DataSource = null;
                this.gvtodayapp.DataBind();
                return;

            }
            Session["tbltoapp"] = ds1.Tables[0];
            this.Data_Bind();



        }

        private void ShowNextApp()
        {
            if (this.ddlSalesTeam.Items.Count == 0)
                return;
            Session.Remove("tbltoapp");
            string comcod = this.Getcomcod();
            string teamcode = this.ddlSalesTeam.SelectedValue.Substring(0, 14).ToString();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "RPTNEXTAPP", teamcode, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvtodayapp.DataSource = null;
                this.gvtodayapp.DataBind();
                return;

            }
            Session["tbltoapp"] = ds1.Tables[0];
            this.Data_Bind();

        }

        private void showCleintHis()
        {
            if (this.ddlSalesTeam.Items.Count == 0)
                return;
            Session.Remove("tbltoapp");
            string comcod = this.Getcomcod();
            string teamcode = this.ddlSalesTeam.SelectedValue.ToString();
            string clientcode = this.ddlClientList.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "RPTCLIENTHIS", teamcode, clientcode, frmdate, todate, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvtodayapp.DataSource = null;
                this.gvtodayapp.DataBind();
                return;

            }
            Session["tbltoapp"] = ds1.Tables[0];
            this.Data_Bind();


        }

        private void showOffPerformance()
        {
            if (this.ddlSalesTeam.Items.Count == 0)
                return;
            Session.Remove("tbltoapp");
            string comcod = this.Getcomcod();
            string teamcode = this.ddlSalesTeam.SelectedValue.Substring(0, 14).ToString();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "RPTOFFPERFORMANCE", teamcode, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvoffper.DataSource = null;
                this.gvoffper.DataBind();
                return;

            }
            Session["tbltoapp"] = ds1.Tables[0];
            this.Data_Bind();


        }


        private void showALLOffPerformance()
        {


            Session.Remove("tbltoapp");
            string comcod = this.Getcomcod();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "RPTALLOFFPERFORMANCE", "", frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvoffper.DataSource = null;
                this.gvoffper.DataBind();
                return;

            }
            Session["tbltoapp"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();




        }
        private void showSalePerformance()
        {
            if (this.ddlSalesTeam.Items.Count == 0)
                return;
            Session.Remove("tbltoapp");
            string comcod = this.Getcomcod();

            string teamcode = this.ddlSalesTeam.SelectedValue.Substring(0, 14).ToString();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_REPORT_CLIENT_INFORMATION", "RPTTEAMSALINFO", "", teamcode, frmdate, todate, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSal.DataSource = null;
                this.gvSal.DataBind();
                return;

            }
            Session["tbltoapp"] = HiddenSameData(ds1.Tables[0]);
            // MktData.Toamt = Convert.ToInt32(ds1.Tables[1].Rows[0]["toamt"]);
            this.Data_Bind();


        }

        private void ShowClinetLetterInfo()
        {
            if (this.ddlSalesTeam.Items.Count == 0)
                return;
            Session.Remove("tbltoapp");
            this.lblletterinfo.Visible = true;
            this.lblClientInfo.Visible = true;
            string comcod = this.Getcomcod();
            string teamcode = this.ddlSalesTeam.SelectedValue.Substring(0, 14).ToString();
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_REPORT_CLIENT_INFORMATION", "RPTLETTERINFOANDCLIENT", teamcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLetter.DataSource = null;
                this.gvLetter.DataBind();
                this.gvClientLetter.DataSource = null;
                this.gvClientLetter.DataBind();
                return;

            }

            this.gvLetter.DataSource = ds1.Tables[0];
            this.gvLetter.DataBind();
            Session["tbltoapp"] = ds1.Tables[1];
            this.Data_Bind();
        }
        private void showClientTrans()
        {

            if (this.ddlSalesTeam.Items.Count == 0)
                return;
            Session.Remove("tbltoapp");
            string comcod = this.Getcomcod();
            string teamcode = ((this.ddlSalesTeam.SelectedValue.Substring(0, 14) == "00000000000000") ? "" : this.ddlSalesTeam.SelectedValue.Substring(0, 14).ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_REPORT_CLIENT_INFORMATION", "RPTTRANSFERINFO", teamcode, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvClientTransfer.DataSource = null;
                this.gvClientTransfer.DataBind();

                return;

            }

            Session["tbltoapp"] = ds1.Tables[0];
            this.Data_Bind();
        }
        private void GetProsClientList()
        {
            //if (this.ddlSalesTeam.Items.Count == 0)
            //    return;
            Session.Remove("tbltoapp");
            string comcod = this.Getcomcod();
            //  string teamcode = this.ddlSalesTeam.SelectedValue.Substring(0, 14).ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_CLIENT_INFORMATION", "GETPROSCLIENT", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProsClient.DataSource = null;
                this.gvProsClient.DataBind();
                return;
            }

            Session["tbltoapp"] = ds1.Tables[0];
            this.Data_Bind();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string teamcode;
            switch (type)
            {
                case "Todaysdis":

                    break;

                case "DiscussHis":

                    break;

                case "NextApp":

                    break;

                case "OffPerformance":

                    break;

                case "SalePerformance":
                    // teamcode = dt1.Rows[0]["teamcode"].ToString();
                    //string proscod = dt1.Rows[0]["proscod"].ToString();
                    //for (int j = 1; j < dt1.Rows.Count; j++)
                    //{
                    //    if (dt1.Rows[j]["teamcode"].ToString() == teamcode && dt1.Rows[j]["proscod"].ToString() == proscod)
                    //    {
                    //        teamcode = dt1.Rows[j]["teamcode"].ToString();
                    //        proscod = dt1.Rows[j]["proscod"].ToString();
                    //        dt1.Rows[j]["teamdesc"] = "";
                    //        dt1.Rows[j]["prosdesc"] = "";
                    //        dt1.Rows[j]["saldate"] = "";


                    //    }

                    //    else
                    //    {
                    //        if (dt1.Rows[j]["teamcode"].ToString() == teamcode)
                    //        {
                    //            dt1.Rows[j]["teamdesc"] = "";
                    //        }

                    //        if (dt1.Rows[j]["proscod"].ToString() == proscod)
                    //        {
                    //            dt1.Rows[j]["prosdesc"] = "";
                    //            dt1.Rows[j]["saldate"] = "";

                    //        }
                    //        teamcode = dt1.Rows[j]["teamcode"].ToString();
                    //        proscod = dt1.Rows[j]["proscod"].ToString();
                    //    }



                    //}

                    break;

                case "ClientLetter":
                case "SendOnlineLetter":

                    break;
                case "ProsClient":

                    break;

                case "AllOffPerformance":
                    teamcode = dt1.Rows[0]["teamcode"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["teamcode"].ToString() == teamcode)
                            dt1.Rows[j]["teamdesc"] = "";
                        teamcode = dt1.Rows[j]["teamcode"].ToString();
                    }

                    break;


            }




            return dt1;

        }


        private void Data_Bind()
        {

            try
            {
                string type = this.Request.QueryString["Type"].ToString().Trim();

                switch (type)
                {
                    case "Todaysdis":
                    case "NextApp":
                        this.gvtodayapp.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvtodayapp.DataSource = (DataTable)Session["tbltoapp"];
                        this.gvtodayapp.DataBind();
                        break;

                    case "DiscussHis":
                        this.gvclient.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvclient.DataSource = (DataTable)Session["tbltoapp"];
                        this.gvclient.DataBind();
                        break;

                    case "OffPerformance":
                        this.gvoffper.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvoffper.DataSource = (DataTable)Session["tbltoapp"];
                        this.gvoffper.DataBind();
                        break;

                    case "SalePerformance":
                        this.gvSal.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvSal.DataSource = (DataTable)Session["tbltoapp"];
                        this.gvSal.DataBind();
                        this.FooterCalculation();

                        break;

                    case "ClientLetter":
                    case "SendOnlineLetter":
                        this.gvClientLetter.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvClientLetter.DataSource = (DataTable)Session["tbltoapp"];
                        this.gvClientLetter.DataBind();
                        break;
                    case "ProsClient":
                        this.gvProsClient.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvProsClient.DataSource = (DataTable)Session["tbltoapp"];
                        this.gvProsClient.DataBind();
                        break;

                    case "AllOffPerformance":
                        this.gvalloffper.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvalloffper.DataSource = (DataTable)Session["tbltoapp"];
                        this.gvalloffper.DataBind();
                        break;

                    case "ClientTrans":
                        this.gvClientTransfer.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvClientTransfer.DataSource = (DataTable)Session["tbltoapp"];
                        this.gvClientTransfer.DataBind();
                        break;


                }
            }
            catch (Exception ex) { }


        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tbltoapp"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvSal.FooterRow.FindControl("lgvFToamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(uamt)", "")) ? 0.00 : dt.Compute("sum(uamt)", ""))).ToString("#,##0;(#,##0); ");



        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Todaysdis":
                case "NextApp":
                    this.PrintTodaysDisANextApp();
                    break;

                case "DiscussHis":
                    this.RptCleintHis();
                    break;

                case "OffPerformance":
                    this.PritOffPerformance();
                    break;


                case "SalePerformance":
                    this.PrintSalePerformance();
                    break;

                case "ClientLetter":
                    PrintClientLetter();
                    break;
                case "ProsClient":
                    this.printProsClntList();
                    break;


                case "AllOffPerformance":
                    this.PritAllOffPerformance();
                    break;

                case "ClientTrans":
                    this.printClientTrans();
                    break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report: " + type;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }



        }

        private void PrintTodaysDisANextApp()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string Salesteamcode=this.ddlSalesTeam.SelectedValue.ToString();
            //ReportDocument rptAppMonitor = new KPIRPT.R_21_Mkt.RptTodaysDisAndNextApp();
            //TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comnam;
            //string Date = (this.Request.QueryString["Type"].ToString().Trim() == "Todaysdis") ? "Date: "+this.txtfrmdate.Text.Trim() : " (From "+this.txtfrmdate.Text.Trim()+" To "+this.txttodate.Text.Trim()+")";
            //TextObject rpttxtHeaderTitle = rptAppMonitor.ReportDefinition.ReportObjects["txtHeaderTitle"] as TextObject;
            //rpttxtHeaderTitle.Text = this.lblHeadertitle.Text;


            //TextObject txtsalesp = rptAppMonitor.ReportDefinition.ReportObjects["txtsalesp"] as TextObject;
            //txtsalesp.Text = "Executive Name: " + this.ddlSalesTeam.SelectedItem.Text;

            //TextObject txtDesignation = rptAppMonitor.ReportDefinition.ReportObjects["txtdesignation"] as TextObject;
            //txtDesignation.Text = "Designation: " + (((DataTable)Session["tblsaleteam"]).Select("teamcode='" + Salesteamcode + "'"))[0]["designtion"];

            //TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = Date;


            //TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptAppMonitor.SetDataSource((DataTable)Session["tbltoapp"]);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptAppMonitor.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptAppMonitor;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        private void RptCleintHis()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string Salesteamcode = this.ddlSalesTeam.SelectedValue.ToString();
            //ReportDocument rptAppMonitor = new KPIRPT.R_21_Mkt.RptClientHistory();
            //TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comnam;
            //TextObject txtsalesp = rptAppMonitor.ReportDefinition.ReportObjects["txtsalesp"] as TextObject;
            //txtsalesp.Text = "Executive Name: " + this.ddlSalesTeam.SelectedItem.Text;

            //TextObject txtDesignation = rptAppMonitor.ReportDefinition.ReportObjects["txtdesignation"] as TextObject;
            //txtDesignation.Text = "Designation: " + (((DataTable)Session["tblsaleteam"]).Select("teamcode='" + Salesteamcode + "'"))[0]["designtion"];
            //TextObject txtclientname = rptAppMonitor.ReportDefinition.ReportObjects["txtclientname"] as TextObject;
            //txtclientname.Text = "Client Name: " + this.ddlClientList.SelectedItem.Text;  
            //TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptAppMonitor.SetDataSource( (DataTable)Session["tbltoapp"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptAppMonitor.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptAppMonitor;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PritOffPerformance()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string Salesteamcode = this.ddlSalesTeam.SelectedValue.ToString();
            //ReportDocument rptAppMonitor = new KPIRPT.R_21_Mkt.RptOfficerPerformance();
            //TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comnam;
            //TextObject txtsalesp = rptAppMonitor.ReportDefinition.ReportObjects["txtsalesp"] as TextObject;
            //txtsalesp.Text = "Executive Name: " + this.ddlSalesTeam.SelectedItem.Text;

            //TextObject txtDesignation = rptAppMonitor.ReportDefinition.ReportObjects["txtdesignation"] as TextObject;
            //txtDesignation.Text = "Designation: " + (((DataTable)Session["tblsaleteam"]).Select("teamcode='" + Salesteamcode + "'"))[0]["designtion"];

            //TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = " (From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";

            //TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptAppMonitor.SetDataSource((DataTable)Session["tbltoapp"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptAppMonitor.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptAppMonitor;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }
        private void PritAllOffPerformance()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string Salesteamcode = this.ddlSalesTeam.SelectedValue.ToString();
            //ReportDocument rptAppMonitor = new KPIRPT.R_21_Mkt.RptAllOfficerPerformance();
            //TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comnam;


            //TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = " (From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";

            //TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptAppMonitor.SetDataSource((DataTable)Session["tbltoapp"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptAppMonitor.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptAppMonitor;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintSalePerformance()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptAppMonitor = new KPIRPT.R_21_Mkt.RptSalePerformance();
            //TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comnam;
            ////TextObject txttoamt = rptAppMonitor.ReportDefinition.ReportObjects["txttoamt"] as TextObject;
            ////txttoamt.Text =MktData.Toamt.ToString("#,##0;(#,##0); ");
            //TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptAppMonitor.SetDataSource((DataTable)Session["tbltoapp"]);
            //Session["Report1"] = rptAppMonitor;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintClientLetter()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //this.SaveValue();
            //string comcod = this.Getcomcod();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string Letcode = "";
            //for (int i = 0; i < this.gvLetter.Rows.Count; i++) 
            //{
            //    if (((CheckBox)this.gvLetter.Rows[i].FindControl("chkletter")).Checked) 
            //    {

            //        Letcode = ((Label)this.gvLetter.Rows[i].FindControl("lgvletcode")).Text.Trim();
            //        break;
            //    } 

            //}

            //DataView dv = ((DataTable)Session["tbltoapp"]).DefaultView;
            //dv.RowFilter = ("chk='True'");

            //DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_REPORT_CLIENT_INFORMATION", "RPTLETTERINFO", Letcode, "", "", "", "", "", "", "", "");
            //string Title = ds1.Tables[0].Rows[0]["letdesc"].ToString().Trim();
            //string subject = "Subject: " + ds1.Tables[0].Rows[1]["letdesc"].ToString().Trim();
            //string Description = ds1.Tables[0].Rows[2]["letdesc"].ToString().Trim();

            //ReportDocument rptAppMonitor = new KPIRPT.R_21_Mkt.RptMktClientLetter();
            //TextObject txtHeadertitle = rptAppMonitor.ReportDefinition.ReportObjects["txtHeadertitle"] as TextObject;
            //txtHeadertitle.Text = Title;
            //TextObject txtSubject = rptAppMonitor.ReportDefinition.ReportObjects["txtSubject"] as TextObject;
            //txtSubject.Text = subject;

            //TextObject txtDescription = rptAppMonitor.ReportDefinition.ReportObjects["txtDescription"] as TextObject;
            //txtDescription.Text = Description;      
            //TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptAppMonitor.SetDataSource(dv.ToTable());
            //Session["Report1"] = rptAppMonitor;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void printProsClntList()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tbltoapp"];
            //if (dt1.Rows.Count == 0)
            //    return;
            //ReportDocument rptstk = new KPIRPT.R_21_Mkt.RptProsClient();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtTitle = rptstk.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            //txtTitle.Text = "Prospective Client List";
            //TextObject txtSealsTeam = rptstk.ReportDefinition.ReportObjects["txtTeam"] as TextObject;
            //txtSealsTeam.Text = "Team Name: " +this.ddlSalesTeam.SelectedItem.Text;
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt1);
            ////string comcod = this.GetComeCode();
            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void printClientTrans()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tbltoapp"];


            //ReportDocument rptstk = new KPIRPT.R_21_Mkt.RptClientTransferInfo();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtTitle = rptstk.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            //txtTitle.Text = "Client transfer Information";
            //TextObject txtSealsTeam = rptstk.ReportDefinition.ReportObjects["txtTeam"] as TextObject;
            //txtSealsTeam.Text = "Team Name: " + this.ddlSalesTeam.SelectedItem.Text;     

            //TextObject rptdate = rptstk.ReportDefinition.ReportObjects["betweenDate"] as TextObject;
            //rptdate.Text = "(From " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";


            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt1);
            ////string comcod = this.GetComeCode();
            ////string comcod = hst["comcod"].ToString();
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void gvtodayapp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvtodayapp.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void imgbtnteam_Click(object sender, EventArgs e)
        {
            this.GetSalesList();
        }
        protected void imgbtnclient_Click(object sender, EventArgs e)
        {
            this.GetClientList();
        }
        protected void gvclient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvclient.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvoffper_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvoffper.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvSal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSal.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvSal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Label Amount = (Label)e.Row.FindControl("lgvAmount");
            //Label Unitdesc = (Label)e.Row.FindControl("lgvUnitdesc");
            //string gcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

            //if (gcod == "")
            //{
            //    return;
            //}
            //if (gcod == "AAAAA")
            //{

            //    Amount.Font.Bold = true;
            //    Unitdesc.Font.Bold = true;


            //}

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvClientLetter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.SaveValue();
            this.gvClientLetter.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tbltoapp"];
            int i, index;
            for (i = 0; i < this.gvClientLetter.Rows.Count; i++)
            {

                index = (this.gvClientLetter.PageSize) * (this.gvClientLetter.PageIndex) + i;
                dt.Rows[index]["chk"] = ((CheckBox)this.gvClientLetter.Rows[i].FindControl("chkletter")).Checked ? "True" : "False";

            }

            Session["tbltoapp"] = dt;
        }
        protected void chkAllfrm_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbltoapp"];
            int i, index;
            if (((CheckBox)this.gvClientLetter.HeaderRow.FindControl("chkAllfrm")).Checked)
            {

                for (i = 0; i < this.gvClientLetter.Rows.Count; i++)
                {
                    ((CheckBox)this.gvClientLetter.Rows[i].FindControl("chkletter")).Checked = true;
                    index = (this.gvClientLetter.PageSize) * (this.gvClientLetter.PageIndex) + i;
                    dt.Rows[index]["chk"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvClientLetter.Rows.Count; i++)
                {
                    ((CheckBox)this.gvClientLetter.Rows[i].FindControl("chkletter")).Checked = false;
                    index = (this.gvClientLetter.PageSize) * (this.gvClientLetter.PageIndex) + i;
                    dt.Rows[index]["chk"] = "False";

                }

            }

            Session["tbltoapp"] = dt;
        }

        protected void chkletter_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void lbtnSend_Click(object sender, EventArgs e)
        {
            SaveValue();
            this.SendLetterOnline();
        }



        private void SendLetterOnline()
        {

            string comcod = this.Getcomcod();
            string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
            DataSet dssmtpandmail = this.MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");
            string Letcode = "";
            for (int i = 0; i < this.gvLetter.Rows.Count; i++)
            {
                if (((CheckBox)this.gvLetter.Rows[i].FindControl("chkletter")).Checked)
                {

                    Letcode = ((Label)this.gvLetter.Rows[i].FindControl("lgvletcode")).Text.Trim();
                    break;
                }

            }

            DataView dv = ((DataTable)Session["tbltoapp"]).DefaultView;
            dv.RowFilter = ("chk='True'");
            DataTable dt = dv.ToTable();



            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_REPORT_CLIENT_INFORMATION", "RPTLETTERINFO", Letcode, "", "", "", "", "", "", "", "");

            string subject = ds1.Tables[0].Rows[1]["letdesc"].ToString().Trim();
            this.txtDescription.Text = ds1.Tables[0].Rows[2]["letdesc"].ToString().Trim();


            //SMTP
            string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
            SmtpClient client = new SmtpClient(hostname, portnumber);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
            string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;



            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(frmemail);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["email"].ToString().Trim() != "")
                    msg.To.Add(new MailAddress(dt.Rows[i]["email"].ToString().Trim()));

            }


            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + this.txtDescription.Text + "</pre></body></html>");



            try
            {
                client.Send(msg);
                ((Label)this.Master.FindControl("lblmsg")).Text = "Your message has been successfully sent.";

            }
            catch (Exception ex)
            {


                ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;
            }


            //Mydelegate mdel = new Mydelegate(MktData.Prod);
            //int a = mdel(12, 20);


        }



        protected void gvProsClient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvProsClient.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvalloffper_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvalloffper.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
    }
}
