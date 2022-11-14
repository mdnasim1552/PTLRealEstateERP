using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using System.Net;
using RealEntity;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_14_Pro
{
    public partial class PurAprovEntry : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        UserManProSupply objuserman = new UserManProSupply();
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
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                ((Label)this.Master.FindControl("lblTitle")).Text = "Order Process";
                if (Request.QueryString["InputType"].ToString() == "PurApproval" || Request.QueryString["InputType"].ToString() == "ProposalEdit")
                {


                    this.lbtnPrevAprovList_Click(null, null);
                }
                this.lbtnOk.Text = "New";
                this.lbtnOk_Click(null, null);
                this.txtCurAprovDate_CalendarExtender.EndDate = System.DateTime.Today;
                this.gvAprovInfo.Columns[10].HeaderText = this.ReadCookie();

                if ((this.Request.QueryString["genno"].ToString().Length > 0))
                {
                    this.lbtnPrevAprovList.Visible = false;
                    this.ddlPrevAprovList.Visible = false;
                }
            }


        }


        private string GetResSupplier()
        {
            string comcod = this.GetCompCode();
            string Calltype = "";
            switch (comcod)
            {
                case "3330": //Bridge
                case "3333": //Alliance  
                case "3339": //Tropical       
                    Calltype = "GETAPROVSUPLIST";
                    break;



                default:
                    Calltype = "GETAPROVALLSUPLIST";

                    break;

            }
            return Calltype;


        }

        private string ReadCookie()
        {
            HttpCookie nameCookie = Request.Cookies["MRF"];
            string refno = nameCookie != null ? nameCookie.Value.Split('=')[1] : "Mrf No";
            return refno;
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    if (Request.QueryString["InputType"].ToString() == "PurApproval")
        //    {

        //        Page.MasterPageFile="~/ApprovalMgt.master";
        //    }
        //    else
        //    {
        //        Page.MasterPageFile = "~/PurchaseMgt.master";

        //    }
        //}

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            // edison
            if (comcod == "3335")
            {
                DataTable dt = (DataTable)ViewState["tblAprov"];
                var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.EClassRequisitionApproval>();

                string title = (Request.QueryString["InputType"].ToString() == "PurApproval") ? "Purchase Approval" : "Purchase Programm";
                string narration = "Narration : " + this.txtAprovNarr.Text.ToString().Trim();
                string cadate = "Date: " + this.txtCurAprovDate.Text.Trim();
                string cano = "Purchase No: " + this.lblCurAprovNo1.Text.ToString().Trim() + this.txtCurAprovNo2.Text.ToString().Trim();
                string approvedate = "Approve Date : " + this.txtApprovalDate.Text.ToString().Trim();
                string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
                string requisitioncreateby = this.txtPreparedBy.Text.Trim().ToString();
                string checkby = "";
                string rateproposal = "";
                string approveby = this.txtApprovedBy.Text.ToString().Trim();
                string finalapproveby = "";

                LocalReport Rpt1 = new LocalReport();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurApprovEntryEdison", lst, null, null);

                Rpt1.SetParameters(new ReportParameter("companyname", comnam));
                Rpt1.SetParameters(new ReportParameter("title", title));
                Rpt1.SetParameters(new ReportParameter("requisitioncreateby", requisitioncreateby));
                Rpt1.SetParameters(new ReportParameter("approvedby", approveby));
                Rpt1.SetParameters(new ReportParameter("narration", narration));
                Rpt1.SetParameters(new ReportParameter("checkby", checkby));
                Rpt1.SetParameters(new ReportParameter("rateproposal", rateproposal));
                Rpt1.SetParameters(new ReportParameter("finalapprovedby", finalapproveby));
                Rpt1.SetParameters(new ReportParameter("cadate", cadate));
                Rpt1.SetParameters(new ReportParameter("cano", cano));
                Rpt1.SetParameters(new ReportParameter("approvdate", approvedate));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));



                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Order Process";
                    string eventdesc = "Print Report";
                    string eventdesc2 = "Purchase No: " + this.lblCurAprovNo1.Text.ToString().Trim() + this.txtCurAprovNo2.Text.ToString().Trim();
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            else
            {

                DataTable dt = (DataTable)ViewState["tblAprov"];
                var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.EClassRequisitionApproval>();

                string title = (Request.QueryString["InputType"].ToString() == "PurApproval") ? "Purchase Approval" : "Purchase Programm";
                string narration = "Narration : " + this.txtAprovNarr.Text.ToString().Trim();
                string cadate = "Date: " + this.txtCurAprovDate.Text.Trim();
                string cano = "Purchase No: " + this.lblCurAprovNo1.Text.ToString().Trim() + this.txtCurAprovNo2.Text.ToString().Trim();
                string approvedate = "Approve Date : " + this.txtApprovalDate.Text.ToString().Trim();
                string printFooter = ASTUtility.Concat(compname, username, printdate);
                string pbnamyby = this.txtPreparedBy.Text.ToString().Trim();
                string approveby = this.txtApprovedBy.Text.ToString().Trim();

                LocalReport Rpt1 = new LocalReport();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurAprovEntry", lst, null, null);

                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("txtTitle", title));
                Rpt1.SetParameters(new ReportParameter("narration", narration));
                Rpt1.SetParameters(new ReportParameter("pbname", pbnamyby));
                Rpt1.SetParameters(new ReportParameter("apname", approveby));
                Rpt1.SetParameters(new ReportParameter("date", cadate));
                Rpt1.SetParameters(new ReportParameter("cano", cano));
                Rpt1.SetParameters(new ReportParameter("aprvdate", approvedate));
                Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


                //ReportDocument rptstk = new RealERPRPT.R_14_Pro.RptPurAprovEntry();
                //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
                //txtCompanyName.Text = comnam;
                //string HeaderTitle = (Request.QueryString["InputType"].ToString() == "PurApproval") ? "Purchase Approval" : "Purchase Programm";

                //TextObject rpttxtHeaderTitle = rptstk.ReportDefinition.ReportObjects["txtHeaderTitle"] as TextObject;
                //rpttxtHeaderTitle.Text = HeaderTitle;
                //TextObject txtcadate = rptstk.ReportDefinition.ReportObjects["cadate"] as TextObject;
                //txtcadate.Text = "Date: " + this.txtCurAprovDate.Text.Trim();
                //TextObject txtcano = rptstk.ReportDefinition.ReportObjects["cano"] as TextObject;
                //txtcano.Text = "Purchase No: " + this.lblCurAprovNo1.Text.ToString().Trim() + this.txtCurAprovNo2.Text.ToString().Trim();
                //TextObject txtadate = rptstk.ReportDefinition.ReportObjects["adate"] as TextObject;
                //txtadate.Text = this.txtApprovalDate.Text.ToString().Trim();
                //TextObject txtnarrationname = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
                //txtnarrationname.Text = this.txtAprovNarr.Text.ToString().Trim();
                //TextObject txtpbname = rptstk.ReportDefinition.ReportObjects["pbname"] as TextObject;
                //txtpbname.Text = this.txtPreparedBy.Text.ToString().Trim();
                //TextObject txtabname = rptstk.ReportDefinition.ReportObjects["abname"] as TextObject;
                //txtabname.Text = this.txtApprovedBy.Text.ToString().Trim();
                //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                //if (ConstantInfo.LogStatus == true)
                //{
                //    string eventtype = "Order Process";
                //    string eventdesc = "Print Report";
                //    string eventdesc2 = "Purchase No: " + this.lblCurAprovNo1.Text.ToString().Trim() + this.txtCurAprovNo2.Text.ToString().Trim();
                //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                //}

                //rptstk.SetDataSource((DataTable)ViewState["tblAprov"]);
                //Session["Report1"] = rptstk;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }


        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }
        private string CompanyLength()
        {
            string comcod = this.GetCompCode();
            string length = "";
            switch (comcod)
            {
                //case "3101":            
                case "3340":
                    length = "length";
                    break;


                default:
                    length = "";
                    break;
            }

            return length;

        }
        protected void lbtnPrevAprovList_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string length = this.CompanyLength();
            string CurDate1 = this.GetStdDate(this.txtCurAprovDate.Text.Trim());

            string qgenno = this.Request.QueryString["genno"] ?? "";
            string SearchPAP = (qgenno.Length == 0 ? "%" : this.Request.QueryString["genno"].ToString()) + "%";
            string InType = (Request.QueryString["InputType"].ToString() == "ProposalEdit") ? "ProposalEdit" : "";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPREVAPROVLIST", CurDate1,
                          InType, length, usrid, SearchPAP, "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevAprovList.Items.Clear();
            this.ddlPrevAprovList.DataTextField = "aprovno1";
            this.ddlPrevAprovList.DataValueField = "aprovno";
            this.ddlPrevAprovList.DataSource = ds1.Tables[0];
            this.ddlPrevAprovList.DataBind();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                //this.lblPrevious.Visible = true;
                //this.txtsrchprevious.Visible = true;
                this.lbtnPrevAprovList.Visible = true;
                this.ddlPrevAprovList.Visible = true;
                if (Request.QueryString["InputType"].ToString() == "PurProposal")
                    this.ddlPrevAprovList.Items.Clear();
                if (Request.QueryString["InputType"].ToString() == "PurApproval" || Request.QueryString["InputType"].ToString() == "ProposalEdit")
                    this.lbtnPrevAprovList_Click(null, null);
                this.lcurApprNo.Visible = (Request.QueryString["InputType"].ToString() == "PurApproval" || Request.QueryString["InputType"].ToString() == "ProposalEdit") ? false : true;
                this.lblCurAprovNo1.Visible = (Request.QueryString["InputType"].ToString() == "PurApproval" || Request.QueryString["InputType"].ToString() == "ProposalEdit") ? false : true;
                this.txtCurAprovNo2.Visible = (Request.QueryString["InputType"].ToString() == "PurApproval" || Request.QueryString["InputType"].ToString() == "ProposalEdit") ? false : true;
                this.txtCurAprovDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.lblCurAprovNo1.Text = "PAP" + DateTime.Today.ToString("MM") + "-";
                this.txtCurAprovDate.Enabled = true;


                //this.txtCurAprovDate.ReadOnly = false;

                this.txtResSearch.Text = "";
                this.ddlResList.Items.Clear();
                this.ddlResourcelist.Items.Clear();
                this.ddlSpecification.Items.Clear();
                this.txtSupSearch.Text = "";
                this.ddlSupList.Items.Clear();
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.txtPreparedBy.Text = "";
                this.txtApprovedBy.Text = "";
                this.txtApprovalDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtAprovNarr.Text = "";

                this.gvAprovInfo.DataSource = null;
                this.gvAprovInfo.DataBind();
                this.Panel1.Visible = false;
                this.lbtnOk.Text = "Ok";

                return;
            }
            //this.lblPrevious.Visible = false;
            //this.txtsrchprevious.Visible = false;
            this.lbtnPrevAprovList.Visible = false;
            this.ddlPrevAprovList.Visible = false;
            //this.txtCurAprovDate.ReadOnly = true;
            this.txtCurAprovNo2.ReadOnly = true;
            this.Panel1.Visible = true;
            this.lbtnOk.Text = "New";
            if (Request.QueryString["InputType"].ToString() == "PurApproval" || Request.QueryString["InputType"].ToString() == "ProposalEdit")
            {

                this.VisibleEntry();
            }
            this.ImgbtnFindRes_Click(null, null);
            this.Get_Approval_Info();
        }


        private void VisibleEntry()
        {
            //this.llaprdate.Visible = true;
            //this.llapprovno.Visible = true;
            //this.lblLastAprovDate.Visible = true;
            //this.lblLastAprovNo.Visible = true;
            this.lCurAppdate.Visible = true;
            this.lcurApprNo.Visible = true;
            this.txtCurAprovDate.Visible = true;
            this.lblCurAprovNo1.Visible = true;
            this.txtCurAprovNo2.Visible = true;

            //  this.lbtnAprove.Visible = true;
            this.lblResList.Visible = false;
            this.lblResList0.Visible = false;
            this.lblResList1.Visible = false;
            this.txtResSearch.Visible = false;
            this.txtSupSearch.Visible = false;
            this.ImgbtnFindRes.Visible = false;
            this.ImgbtnFindSup.Visible = false;
            this.lbtnSelectRes.Visible = false;
            this.lbtnSelectAll.Visible = false;
            this.lblSpecification.Visible = false;
            this.ddlSpecification.Visible = false;
            this.ddlPayType.Visible = false;
            this.ddlResList.Visible = false;
            this.ddlSupList.Visible = false;
            this.lblResList2.Visible = false;
            this.ddlResourcelist.Visible = false;

        }
        protected void GetAprovNo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string CurDate1 = this.GetStdDate(this.txtCurAprovDate.Text.Trim());
            string mAprovgNo = "NEWAPROV";
            if (this.ddlPrevAprovList.Items.Count > 0)
            {
                mAprovgNo = this.ddlPrevAprovList.SelectedValue.ToString();
            }

            if (mAprovgNo == "NEWAPROV")
            {
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETLASTAPROVINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurAprovNo1.Text = ds1.Tables[0].Rows[0]["maxaprovno1"].ToString().Substring(0, 6);
                    this.txtCurAprovNo2.Text = ds1.Tables[0].Rows[0]["maxaprovno1"].ToString().Substring(6, 5);
                    this.ddlPrevAprovList.DataTextField = "maxaprovno1";
                    this.ddlPrevAprovList.DataValueField = "maxaprovno";
                    this.ddlPrevAprovList.DataSource = ds1.Tables[0];
                    this.ddlPrevAprovList.DataBind();
                }

            }
        }



        protected void Get_Approval_Info()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string CurDate1 = this.GetStdDate(this.txtCurAprovDate.Text.Trim());
            string mAprovgNo = "NEWAPROV";
            if (this.ddlPrevAprovList.Items.Count > 0)
            {
                mAprovgNo = this.ddlPrevAprovList.SelectedValue.ToString();
                this.txtCurAprovDate.Enabled = false;
            }

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURAPROVINFO", mAprovgNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblAprov"] = this.HiddenSameData(ds1.Tables[0]);
            Session["tblUserAprov"] = ds1.Tables[1];
            //this.lbtnResFooterTotal_Click(null, null);


            if (mAprovgNo == "NEWAPROV")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETLASTAPROVINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurAprovNo1.Text = ds1.Tables[0].Rows[0]["maxaprovno1"].ToString().Substring(0, 6);
                    this.txtCurAprovNo2.Text = ds1.Tables[0].Rows[0]["maxaprovno1"].ToString().Substring(6, 5);
                }
                return;
            }
            this.lblCurAprovNo1.Text = ds1.Tables[1].Rows[0]["aprovno1"].ToString().Substring(0, 6);
            this.txtCurAprovNo2.Text = ds1.Tables[1].Rows[0]["aprovno1"].ToString().Substring(6, 5);
            this.txtCurAprovDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["aprovdat"]).ToString("dd.MM.yyyy");
            this.txtPreparedBy.Text = ds1.Tables[1].Rows[0]["aprovbydes"].ToString();
            this.txtApprovedBy.Text = ds1.Tables[1].Rows[0]["appbydes"].ToString();
            this.txtApprovalDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["apprdat"]).ToString("dd.MM.yyyy");
            this.txtAprovNarr.Text = ds1.Tables[1].Rows[0]["aprovnar"].ToString();
            this.gvAprovInfo_DataBind();
            string msrno = ((DataTable)ViewState["tblAprov"]).Rows[0]["msrno"].ToString();
            this.ShowMarketSurvey(msrno);
            // ViewState["tblAprov"]
        }

        protected void gvAprovInfo_DataBind()
        {
            DataTable tbl1 = this.HiddenSameData((DataTable)ViewState["tblAprov"]);
            this.gvAprovInfo.DataSource = tbl1;
            this.gvAprovInfo.DataBind();

            string comcod = GetCompCode();
            if (comcod == "3353" || comcod == "3101")
            {
                this.gvAprovInfo.Columns[1].Visible = true;                
            }            
            if (Request.QueryString["InputType"].ToString() == "PurProposal")
            {
                ((LinkButton)this.gvAprovInfo.FooterRow.FindControl("lbtnDelete")).Visible = false;
            }
            ((Label)this.gvAprovInfo.FooterRow.FindControl("lblgvFooterTAprovAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(aprovamt)", "")) ?
                                          0.00 : tbl1.Compute("Sum(aprovamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((DropDownList)this.gvAprovInfo.FooterRow.FindControl("ddlPageNo")).Visible = false;
            double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.gvAprovInfo.PageSize);
            ((DropDownList)this.gvAprovInfo.FooterRow.FindControl("ddlPageNo")).Items.Clear();
            for (int i = 1; i <= TotalPage; i++)
                ((DropDownList)this.gvAprovInfo.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            if (TotalPage > 1)
                ((DropDownList)this.gvAprovInfo.FooterRow.FindControl("ddlPageNo")).Visible = true;
            ((DropDownList)this.gvAprovInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.gvAprovInfo.PageIndex;
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            DataView dv = dt1.DefaultView;
            dv.Sort = "reqno";
            dt1 = dv.ToTable();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["projdesc1"] = "";
                }
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["rsirdesc1"] = "";
                }

                else
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                rsircode = dt1.Rows[j]["rsircode"].ToString();
            }

            return dt1;
        }


        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //string SerchText = this.txtResSearch.Text.Trim() + "%";
            string SerchText = (this.Request.QueryString["genno"].ToString()).Length == 0 ? "%" + this.txtResSearch.Text.Trim() + "%" : this.Request.QueryString["genno"].ToString() + "%";
            string CurDate1 = this.GetStdDate(this.txtCurAprovDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETAPROVRESLIST", CurDate1,
                          SerchText, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            ViewState["tblsp"] = ds1.Tables[0];
            ViewState["tblRes"] = ds1.Tables[1];
            Session["tblReq"] = ds1.Tables[2];

            this.ddlResList.DataTextField = "textfield";
            this.ddlResList.DataValueField = "valuefiled";
            this.ddlResList.DataSource = ds1.Tables[2];
            this.ddlResList.DataBind();
            //this.ImgbtnFindSup_Click(null, null);
            this.ddlResList_SelectedIndexChanged(null, null);


        }

        protected void ddlResList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtres = (DataTable)ViewState["tblRes"];
            string reqno = this.ddlResList.SelectedValue.ToString();

            DataView dv = dtres.DefaultView;
            this.ddlResourcelist.Items.Clear();
            dv.RowFilter = "reqno in ('" + reqno + "')";
            //dv.RowFilter = "prcod not in('" + ProdCode + "')";
            DataTable dtd = dv.ToTable();
            this.ddlResourcelist.DataTextField = "rsirdesc1";
            this.ddlResourcelist.DataValueField = "rsircode";
            this.ddlResourcelist.DataSource = dv.ToTable();
            this.ddlResourcelist.DataBind();
            this.ddlResourcelist_SelectedIndexChanged(null, null);
            this.ImgbtnFindSup_Click(null, null);
        }
        protected void ddlResourcelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtres = (DataTable)ViewState["tblsp"];
            string reqno = this.ddlResList.SelectedValue.ToString();
            string rsircode = this.ddlResourcelist.SelectedValue.ToString();
            DataView dv = dtres.DefaultView;

            dv.RowFilter = "reqno='" + reqno + "' and  rsircode='" + rsircode + "'";
            //dv.RowFilter = "prcod not in('" + ProdCode + "')";

            this.ddlSpecification.DataTextField = "textfield";
            this.ddlSpecification.DataValueField = "valuefiled";
            this.ddlSpecification.DataSource = dv.ToTable();
            this.ddlSpecification.DataBind();

            this.ImgbtnFindSup_Click(null, null);
        }



        protected void ImgbtnFindSup_Click(object sender, EventArgs e)
        {
            if (this.ddlResourcelist.Items.Count == 0)
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.ddlSupList.Items.Clear();
            string mSrchTxt = "%" + this.txtSupSearch.Text.Trim() + "%";
            string mResCode = this.ddlSpecification.SelectedValue.ToString().Substring(14, 12);
            string mSpcfCod = this.ddlSpecification.SelectedValue.ToString().Substring(26, 12);


            string CallType = this.GetResSupplier();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", CallType, mSrchTxt, mResCode, mSpcfCod, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;
            DataTable dt1 = ds1.Tables[0];
            DataView dv2 = dt1.DefaultView;
            dv2.Sort = "ssircode";
            this.ddlSupList.DataTextField = "ssirdesc1";
            this.ddlSupList.DataValueField = "ssircode";
            this.ddlSupList.DataSource = dv2.ToTable();
            this.ddlSupList.DataBind();


            // Supplier Selected
            DataTable dt = (DataTable)ViewState["tblsp"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("rsircode='" + mResCode + "' and spcfcod='" + mSpcfCod + "'");
            dt = dv.ToTable();
            string ssircode = dt.Rows[0]["ssircode"].ToString();
            this.ddlSupList.SelectedValue = ssircode;
        }

        protected void Session_tblAprov_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tblAprov"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvAprovInfo.Rows.Count; j++)
            {
                // double Orderqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvAprovInfo.Rows[j].FindControl("txtgvNewOrderQty")).Text.Trim()));
                double balqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvAprovInfo.Rows[j].FindControl("lblgvBalqty")).Text.Trim()));

                double dgvNewOrderQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvAprovInfo.Rows[j].FindControl("txtgvNewOrderQty")).Text.Trim()));

                double aprovsrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvAprovInfo.Rows[j].FindControl("txtgvApprovsRate")).Text.Trim()));
                double dispercnt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvAprovInfo.Rows[j].FindControl("txtgvdispercnt")).Text.Trim().Replace("%", "")));
                double aprovrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvAprovInfo.Rows[j].FindControl("txtgvNewApprovRate")).Text.Trim()));

                if (aprovsrate < aprovrate)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Supplier rate must be greater then Actual Rate";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }


                dispercnt = (aprovrate > 0) ? ((aprovsrate - aprovrate) * 100) / aprovsrate : dispercnt;
                aprovrate = (aprovrate > 0) ? aprovrate : (aprovsrate - aprovsrate * .01 * dispercnt);
                double dgvAprovAmt = dgvNewOrderQty * aprovrate;





                //if (this.chkneBudget.Checked)
                //{
                //    if (dgvBgdQty < dgvReqQty)
                //    {
                //       ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                //        break;

                //    }

                //}

                if (balqty >= dgvNewOrderQty)
                {
                    ((TextBox)this.gvAprovInfo.Rows[j].FindControl("txtgvNewOrderQty")).Text = dgvNewOrderQty.ToString("#,##0.000;(#,##0.000); ");


                    ((TextBox)this.gvAprovInfo.Rows[j].FindControl("txtgvApprovsRate")).Text = aprovsrate.ToString("#,##0.0000;(#,##0.0000); ");
                    ((TextBox)this.gvAprovInfo.Rows[j].FindControl("txtgvdispercnt")).Text = dispercnt.ToString("#,##0.00;(#,##0.00); ") + (dispercnt > 0 ? "%" : "");
                    ((TextBox)this.gvAprovInfo.Rows[j].FindControl("txtgvNewApprovRate")).Text = aprovrate.ToString("#,##0.0000;(#,##0.0000); ");
                    ((Label)this.gvAprovInfo.Rows[j].FindControl("lblgvNewOrderAmt")).Text = dgvAprovAmt.ToString("#,##0.000;(#,##0.000); ");
                    TblRowIndex2 = (this.gvAprovInfo.PageIndex) * this.gvAprovInfo.PageSize + j;
                    tbl1.Rows[TblRowIndex2]["aprovqty"] = dgvNewOrderQty;
                    tbl1.Rows[TblRowIndex2]["aprovsrate"] = aprovsrate;
                    tbl1.Rows[TblRowIndex2]["dispercnt"] = dispercnt;
                    tbl1.Rows[TblRowIndex2]["aprovrate"] = aprovrate;
                    tbl1.Rows[TblRowIndex2]["aprovamt"] = dgvAprovAmt;

                }
                else
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Order Qty  must be less then equal Balance Qty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }
            }

            ViewState["tblAprov"] = tbl1;
        }
        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {

            string ssircode = this.ddlSupList.SelectedValue.ToString().Trim();
            if (this.ddlResourcelist.Items.Count == 0 || this.ddlSupList.Items.Count == 0)
                return;

            if (ssircode == "000000000000" || ssircode == "")
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select Supplier";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            this.Session_tblAprov_Update();
            DataTable tbl1 = (DataTable)ViewState["tblAprov"];
            string spcocod = this.ddlSpecification.SelectedValue.ToString();
            string mReqNo = this.ddlSpecification.SelectedValue.ToString().Substring(0, 14);
            //string mProgNo = this.ddlResList.SelectedValue.ToString().Substring(14, 14);
            string mResCode = this.ddlSpecification.SelectedValue.ToString().Substring(14, 12);
            string mSpcfCod = this.ddlSpecification.SelectedValue.ToString().Substring(26, 12);
            string mSuplCode = this.ddlSupList.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("reqno = '" + mReqNo + "' and rsircode = '" + mResCode +
                                        "' and spcfcod = '" + mSpcfCod + "'");

            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["reqno"] = mReqNo;
                dr1["rsircode"] = mResCode;
                dr1["spcfcod"] = mSpcfCod;
                dr1["rspcfcod"] = mSpcfCod;
                dr1["ssircode"] = mSuplCode;
                DataTable tbl2 = (DataTable)ViewState["tblsp"];
                DataRow[] dr3 = tbl2.Select("reqno = '" + mReqNo + "' and rsircode = '" + mResCode +
                                        "' and spcfcod = '" + mSpcfCod + "'");
                dr1["reqno1"] = dr3[0]["reqno1"];
                dr1["mrfno"] = dr3[0]["mrfno"];
                dr1["reqdat"] = dr3[0]["reqdat"];
                dr1["pactcode"] = dr3[0]["pactcode"];
                dr1["projdesc1"] = dr3[0]["projdesc1"];
                dr1["rsirdesc1"] = dr3[0]["rsirdesc1"];
                dr1["spcfdesc"] = dr3[0]["spcfdesc"];
                dr1["rsirunit"] = dr3[0]["rsirunit"];
                dr1["ssirdesc1"] = this.ddlSupList.SelectedItem.Text.Trim();
                dr1["areqty"] = dr3[0]["areqty"];
                dr1["comqty"] = dr3[0]["comqty"];
                dr1["balqty"] = dr3[0]["balqty"];
                dr1["aprovqty"] = dr3[0]["balqty"];
                dr1["maprovrate"] = dr3[0]["maprovrate"];
                dr1["aprovsrate"] = dr3[0]["aprovsrate"];
                dr1["dispercnt"] = dr3[0]["dispercnt"];
                dr1["aprovrate"] = dr3[0]["aprovrate"];
                dr1["aprovamt"] = Convert.ToDouble(dr3[0]["balqty"]) * Convert.ToDouble(dr3[0]["aprovrate"]);
                dr1["paytype"] = this.ddlPayType.SelectedItem.Text.Trim();
                dr1["eusrname"] = "";
                dr1["rowid"] = dr3[0]["rowid"];
                tbl1.Rows.Add(dr1);
            }
            else
            {
                dr2[0]["ssirdesc1"] = this.ddlSupList.SelectedItem.Text.Trim();
                dr2[0]["paytype"] = this.ddlPayType.SelectedItem.Text.Trim();
            }
            ViewState["tblAprov"] = this.HiddenSameData(tbl1);
            int RowNo = 1;
            double PageNo = Math.Ceiling(RowNo * 1.00 / this.gvAprovInfo.PageSize);
            this.gvAprovInfo.PageIndex = Convert.ToInt32(PageNo - 1);
            this.gvAprovInfo_DataBind();
            this.lbtnResFooterTotal_Click(null, null);

            string msrno = (((DataTable)Session["tblReq"]).Select("reqno='" + mReqNo + "'"))[0]["msrno"].ToString();
            this.ShowMarketSurvey(msrno);
            this.ShowNaration();
        }
        protected void lbtnSelectAll_Click(object sender, EventArgs e)
        {

            if (this.ddlResourcelist.Items.Count == 0 || this.ddlSupList.Items.Count == 0)
                return;

            this.Session_tblAprov_Update();
            DataTable tbl1 = (DataTable)ViewState["tblAprov"];
            string Mreqno1 = this.ddlResList.SelectedValue.ToString();
            DataTable tbl2 = (DataTable)ViewState["tblsp"];
            DataView dv1 = tbl2.DefaultView;
            dv1.RowFilter = "reqno in('" + Mreqno1 + "')";
            tbl2 = dv1.ToTable();
            string ssircode = this.ddlSupList.SelectedValue.ToString();
            if (this.GetCompCode() != "3368")
            {
                if (ssircode == "000000000000" || ssircode == "")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select Supplier";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

            }
            


            DataRow[] dr2 = tbl1.Select("reqno = '" + Mreqno1 + "'");
            if (dr2.Length == 0)
            {
                for (int i = 0; i < tbl2.Rows.Count; i++)
                {
                    DataRow dr1 = tbl1.NewRow();
                    dr1["reqno"] = tbl2.Rows[i]["reqno"].ToString();
                    dr1["rsircode"] = tbl2.Rows[i]["rsircode"].ToString();
                    dr1["spcfcod"] = tbl2.Rows[i]["spcfcod"].ToString();
                    dr1["rspcfcod"] = tbl2.Rows[i]["spcfcod"].ToString();

                    //dr1["ssircode"] = tbl2.Rows[i]["ssircode"].ToString().Trim() == ""
                    //    ? this.ddlSupList.SelectedValue.ToString()
                    //    : tbl2.Rows[i]["ssircode"].ToString();
                    dr1["ssircode"] = this.ddlSupList.SelectedValue.ToString();

                    dr1["reqno1"] = tbl2.Rows[i]["reqno1"].ToString();
                    dr1["mrfno"] = tbl2.Rows[i]["mrfno"].ToString();
                    dr1["reqdat"] = tbl2.Rows[i]["reqdat"].ToString();
                    dr1["pactcode"] = tbl2.Rows[i]["pactcode"].ToString();
                    dr1["projdesc1"] = tbl2.Rows[i]["projdesc1"].ToString();
                    dr1["rsirdesc1"] = tbl2.Rows[i]["rsirdesc1"].ToString();
                    dr1["spcfdesc"] = tbl2.Rows[i]["spcfdesc"].ToString();
                    dr1["rsirunit"] = tbl2.Rows[i]["rsirunit"].ToString();
                    //dr1["ssirdesc1"] = tbl2.Rows[i]["ssircode"].ToString().Trim() == "" ? this.ddlSupList.SelectedItem.Text.Trim() : tbl2.Rows[i]["ssirdesc"].ToString();
                    dr1["ssirdesc1"] = this.ddlSupList.SelectedItem.Text.Trim();
                    dr1["areqty"] = Convert.ToDouble(tbl2.Rows[i]["areqty"]).ToString();
                    dr1["comqty"] = Convert.ToDouble(tbl2.Rows[i]["comqty"]).ToString();
                    dr1["balqty"] = Convert.ToDouble(tbl2.Rows[i]["balqty"]).ToString();
                    dr1["aprovqty"] = Convert.ToDouble(tbl2.Rows[i]["balqty"]).ToString();
                    dr1["aprovsrate"] = Convert.ToDouble(tbl2.Rows[i]["aprovsrate"]).ToString();
                    dr1["dispercnt"] = Convert.ToDouble(tbl2.Rows[i]["dispercnt"]).ToString();
                    dr1["aprovrate"] = Convert.ToDouble(tbl2.Rows[i]["aprovrate"]).ToString();
                    dr1["maprovrate"] = Convert.ToDouble(tbl2.Rows[i]["maprovrate"]).ToString();
                    dr1["aprovamt"] = Convert.ToDouble(tbl2.Rows[i]["balqty"]) * Convert.ToDouble(tbl2.Rows[i]["aprovrate"]);
                    dr1["paytype"] = this.ddlPayType.SelectedItem.Text.Trim();
                    dr1["eusrname"] = "";
                    dr1["rowid"] = Convert.ToDouble(tbl2.Rows[i]["rowid"]).ToString();
                    tbl1.Rows.Add(dr1);

                }

                ViewState["tblAprov"] = this.HiddenSameData(tbl1);
            }
            this.gvAprovInfo_DataBind();
            this.lbtnResFooterTotal_Click(null, null);
            string msrno = (((DataTable)Session["tblReq"]).Select("reqno='" + Mreqno1 + "'"))[0]["msrno"].ToString();
            this.ShowMarketSurvey(msrno);
            this.ShowNaration();

        }
        private void ShowMarketSurvey(string msrno)
        {
            if (msrno == "")
            {
                this.pnlMarketSurvey.Visible = false;
                this.lblsurveyby.Text = "";
                this.gvMSRInfo.DataSource = null;
                this.gvMSRInfo.DataBind();
                return;
            }
            this.pnlMarketSurvey.Visible = true;
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURMSRINFO", msrno, "",
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt1 = HiddenSameData1(ds1.Tables[0]);
            this.gvMSRInfo.DataSource = dt1;
            this.gvMSRInfo.DataBind();
            this.lblsurveyby.Text = (ds1.Tables[1].Rows.Count == 0) ? "" : "Survey Completed By: " + ds1.Tables[1].Rows[0]["username"].ToString();




        }


        private void ShowNaration()
        {


            string comcod = this.GetCompCode();
            string reqno = this.ddlResList.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETREQNARATION", reqno, "",
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.lblreqnaration.Text = (ds1.Tables[0].Rows.Count == 0) ? "" : "Req Naration : " + ds1.Tables[0].Rows[0]["reqnaration"].ToString();




        }




        private DataTable HiddenSameData1(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["rsirdesc1"] = "";
                }

                else
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
            }

            return dt1;
        }
        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblAprov_Update();
            this.gvAprovInfo.PageIndex = ((DropDownList)this.gvAprovInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
            this.gvAprovInfo_DataBind();
        }
        protected void lbtnUpdatePurAprov_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //   ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.lbtnResFooterTotal_Click(null, null);
            DataTable tbl1 = (DataTable)ViewState["tblAprov"];
           

            DataRow[] dr = tbl1.Select("aprovqty>0");
            if (dr.Length == 0)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Input Order Qty";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            // Supplier 
            string ssircode = "000000000000";
            DataRow[] drs = tbl1.Select("ssircode='" + ssircode  + "' or ssircode='"+ "'");
            if (drs.Length > 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Supplier Name Is Undefind";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }



            switch (comcod)
            {
                case "3301":
                case "1301":
                case "2301":
                    break;

                default:
                    //foreach(DataRow dr2 in tbl1.Rows)
                    //{

                    //    if (Convert.ToDouble(dr2["maprovrate"]) < Convert.ToDouble(dr2["aprovrate"]))
                    //    {

                    //       ((Label)this.Master.FindControl("lblmsg")).Text = "Rate Equal or Below Aproved  Rate";
                    //        return;
                    //    }

                    //}

                    break;

            }




            string mAPROVDAT = this.GetStdDate(this.txtCurAprovDate.Text.Trim());
            string mAPROVUSRID = "";
            string mAPPRUSRID = "";
            string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());
            string mAPROVBYDES = this.txtPreparedBy.Text.Trim();
            string mAPPBYDES = this.txtApprovedBy.Text.Trim();
            string mAPROVNAR = this.txtAprovNarr.Text.Trim();

            //log Report
            DataTable dtuser = (DataTable)Session["tblUserAprov"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (this.Request.QueryString["InputType"] == "PurProposal") ? userid : (this.Request.QueryString["InputType"] == "ProposalEdit") ? userid
                : (tblPostedByid == "") ? userid : tblPostedByid;

            string Posttrmid = (this.Request.QueryString["InputType"] == "PurProposal") ? Terminal : (this.Request.QueryString["InputType"] == "ProposalEdit") ? Terminal
                : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;

            string PostSession = (this.Request.QueryString["InputType"] == "PurProposal") ? Sessionid : (this.Request.QueryString["InputType"] == "ProposalEdit") ? Sessionid
                : (tblPostedSession == "") ? Sessionid : tblPostedSession;

            string ApprovByid = (this.Request.QueryString["InputType"] == "PurProposal") ? "" : userid;
            string approvdat = (this.Request.QueryString["InputType"] == "PurProposal") ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");
            string Approvtrmid = (this.Request.QueryString["InputType"] == "PurProposal") ? "" : Terminal;
            string ApprovSession = (this.Request.QueryString["InputType"] == "PurProposal") ? "" : Sessionid;

            /////log end


            ////For Balace Req Qty

            if (this.Request.QueryString["InputType"].ToString().Trim() == "PurProposal")
            {

                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    string mREQNO = tbl1.Rows[i]["reqno"].ToString();
                    string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                    string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                    DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "BALREQQTY", mREQNO, mRSIRCODE, mSPCFCOD, "", "", "", "", "", "");
                    if (ds.Tables[0].Rows.Count == 0) continue;
                    else if (Convert.ToDouble(ds.Tables[0].Rows[0]["balqty"]) <= 0)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "There is no balance qty in Requisition";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                }

            }


            //Forward Requisition

            foreach (DataRow drf in tbl1.Rows)
            {


                bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(drf["reqdat"].ToString()), Convert.ToDateTime(mAPROVDAT));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Approved Date is equal or greater Requisition Date');", true);
                    return;
                }


            }



            if (this.ddlPrevAprovList.Items.Count == 0)
                this.GetAprovNo();
            string mAPROVNO = this.lblCurAprovNo1.Text.Trim().Substring(0, 3) + this.txtCurAprovDate.Text.Trim().Substring(6, 4) + this.lblCurAprovNo1.Text.Trim().Substring(3, 2) + this.txtCurAprovNo2.Text.Trim();


            //////////

            bool result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURAPROVINFO", "PURAPROVB", mAPROVNO, mAPROVDAT, mAPROVUSRID, mAPPRUSRID,
                        mAPPRDAT, mAPROVBYDES, mAPPBYDES, mAPROVNAR, PostedByid, PostSession, Posttrmid, ApprovByid, approvdat, Approvtrmid, ApprovSession, "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                string mREQNO = tbl1.Rows[i]["reqno"].ToString();

                bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(tbl1.Rows[i]["reqdat"].ToString()), Convert.ToDateTime(mAPROVDAT));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Approved Date is equal or greater Requisition Date');", true);
                    return;
                }




                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                string rSPCFCOD = tbl1.Rows[i]["rspcfcod"].ToString();
                string mSSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                double Balqty = Convert.ToDouble(tbl1.Rows[i]["aprovqty"]);
                string mAPROVQTY = Convert.ToDouble("0" + tbl1.Rows[i]["aprovqty"]).ToString();
                double mAPROVQTY1 = Convert.ToDouble("0" + tbl1.Rows[i]["aprovqty"]);

                string mAPROVRATE = tbl1.Rows[i]["aprovrate"].ToString();
                string mPAYTYPE = tbl1.Rows[i]["paytype"].ToString();
                string aprovsrate = Convert.ToDouble(tbl1.Rows[i]["aprovsrate"]).ToString();
                string dispercnt = Convert.ToDouble(tbl1.Rows[i]["dispercnt"]).ToString();
                if (Balqty >= mAPROVQTY1)
                {

                    if (mAPROVQTY1 > 0)
                        result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURAPROVINFO", "PURAPROVA",
                                    mAPROVNO, mREQNO, mRSIRCODE, mSPCFCOD, mSSIRCODE, mAPROVQTY, mAPROVRATE, mPAYTYPE, aprovsrate, dispercnt, rSPCFCOD, "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                }

                else
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Order Qty Less then or Equal Balance Qty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }

            }
            this.txtCurAprovDate.Enabled = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            string reqno = Request.QueryString["genno"].ToString();

            if (hst["compsms"].ToString() == "True")
            {

                switch (comcod)
                {
                    case "3333":
                        break;

                    default:

                        if (this.Request.QueryString["InputType"].ToString().Trim() == "PurProposal")
                        {
                            SendSmsProcess sms = new SendSmsProcess();
                            string comnam = hst["comnam"].ToString();
                            string compname = hst["compname"].ToString();
                            string frmname = "PurWrkOrderEntry.aspx?InputType=OrderEntry";


                            string SMSHead = "Ready To Purchase Order";



                            string SMSText = comnam + ":\n" + SMSHead + "\n" + "Aprov. No.:" + mAPROVNO;
                            bool resultsms = sms.SendSmms(SMSText, userid, frmname);
                        }
                        break;
                }
            }




            //if (ConstantInfo.LogStatus == true)
            //{

            //    string text = this.ddlResList.SelectedItem.Text + " forwarded to Purchase Order";
            //    sendSmsFromAPI(text);

            //}
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Order Process";
            //    string eventdesc = "Update Process";
            //    string eventdesc2 = mAPROVNO;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
        }



        //private void sendSmsFromAPI(string text)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string Sesion = hst["session"].ToString();
        //    string userid = hst["usrid"].ToString();
        //    int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        //    string frmname = HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp);
        //    frmname = frmname.Substring(frmname.LastIndexOf('/') + 1) + "";

        //    DataSet ds3 = purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWAPIINFO", userid, frmname, "", "", "");
        //    if (ds3.Tables[0].Rows.Count == 0)
        //        return;
        //    string user = ds3.Tables[0].Rows[0]["apiusrid"].ToString().Trim(); //"nahid@asit.com.bd";
        //    string pass = ds3.Tables[0].Rows[0]["apipass"].ToString().Trim(); //"asit321";
        //    string routeid = ds3.Tables[0].Rows[0]["apirouid"].ToString().Trim();//3;
        //    string typeid = ds3.Tables[0].Rows[0]["apitypeid"].ToString().Trim();//1;
        //    string sender = ds3.Tables[0].Rows[0]["apisender"].ToString().Trim(); //"ASITNAHID";  //Sender


        //    string SMSText = text; //        
        //    string catname = ds3.Tables[0].Rows[0]["apicatname"].ToString().Trim();//General
        //    string ApiUrl = ds3.Tables[0].Rows[0]["apiurl"].ToString().Trim(); //"http://login.smsnet24.com/apimanager/sendsms?user_id=";
        //    for (int i = 0; i < ds3.Tables[1].Rows.Count; i++)
        //    {
        //        string mobile = "88" + ds3.Tables[1].Rows[i]["phno"].ToString().Trim(); //"880" + "1817610879";//this.txtMob.Text.ToString().Trim();1813934120

        //        //String myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=" + mobile + "&sms[0][1]=" + System.Web.HttpUtility.UrlEncode(SMSText) + "&sms[0][2]=" + "1234567890" + "&sid=" + sender;
        //        //using (WebClient wc = new WebClient())
        //        //{
        //        //    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        //        //    string HtmlResult = wc.UploadString(ApiUrl, myParameters); Console.Write(HtmlResult);
        //        //}
        //        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(ApiUrl + user + "&user_password=" + pass + "&route_id=" + routeid
        //           + "&sms_type_id=" + typeid + "&sms_sender=" + sender + "&sms_receiver=" + mobile + "&sms_text=" + SMSText + "&sms_category_name=" + catname);

        //        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
        //        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
        //        string responseString = respStreamReader.ReadToEnd();
        //        respStreamReader.Close();
        //        myResp.Close();
        //    }


        //}





        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblAprov_Update();
            DataTable tbl1 = (DataTable)ViewState["tblAprov"];
            ((Label)this.gvAprovInfo.FooterRow.FindControl("lblgvFooterTAprovAmt")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(aprovamt)", "")) ?
                    0.00 : tbl1.Compute("Sum(aprovamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }
        protected void ChkgvFooterHide_CheckedChanged(object sender, EventArgs e)
        {

        }



        protected void lbtnAprove_Click(object sender, EventArgs e)
        {

            DataTable tbl1 = (DataTable)ViewState["tblAprov"];
            string ssircode = "000000000000";
            DataRow[] dr = tbl1.Select("ssircode='" + ssircode + "'");
            if (dr.Length > 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Supplier Name Is Undefind";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string Apruserid = hst["usrid"].ToString();
            string AprTerminal = hst["compname"].ToString();
            string AprSessionid = hst["session"].ToString();
            string AprDate = System.DateTime.Today.ToString();
            string comcod = hst["comcod"].ToString();

            string appno = this.ddlPrevAprovList.SelectedValue.ToString();
            string approved = "ok";


            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEPURAPROVINFOB",
                          appno, approved, Apruserid, AprTerminal, AprSessionid, AprDate, "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Order Process";
                    string eventdesc = "Approved Process";
                    string eventdesc2 = appno;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
                //this.lbtnPrevAprovList_Click(null, null);

            }
        }





        protected void gvAprovInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvAprovInfo.EditIndex = -1;
            this.gvAprovInfo_DataBind();
        }
        protected void gvAprovInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvAprovInfo.EditIndex = e.NewEditIndex;
            this.gvAprovInfo_DataBind();

            // Supplier
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt = "%" + this.txtSupSearch.Text.Trim() + "%";
            string mResCode = ((Label)this.gvAprovInfo.Rows[e.NewEditIndex].FindControl("lblgvResCod")).Text.Trim();
            string mSupCode = ((Label)this.gvAprovInfo.Rows[e.NewEditIndex].FindControl("lblgvResCod1")).Text.Trim();
            string mSpcfCod = ((Label)this.gvAprovInfo.Rows[e.NewEditIndex].FindControl("lblgvSpcfCod")).Text.Trim();


            string Calltype = this.GetResSupplier();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", Calltype, mSrchTxt, mResCode, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            DropDownList ddl1 = (DropDownList)this.gvAprovInfo.Rows[e.NewEditIndex].FindControl("ddlSupname");
            ddl1.DataTextField = "ssirdesc1";
            ddl1.DataValueField = "ssircode";
            ddl1.DataSource = ds1.Tables[0];
            ddl1.DataBind();
            ddl1.SelectedValue = mSupCode;

            // Specification

            DropDownList ddlspeci = (DropDownList)this.gvAprovInfo.Rows[e.NewEditIndex].FindControl("ddlspecification");
            ddlspeci.DataTextField = "spcfdesc";
            ddlspeci.DataValueField = "spcfcod";
            ddlspeci.DataSource = ds1.Tables[1];
            ddlspeci.DataBind();
            ddlspeci.SelectedValue = mSpcfCod;





        }
        protected void gvAprovInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable tbl1 = (DataTable)ViewState["tblAprov"];
            string mSSIRCODE = ((DropDownList)this.gvAprovInfo.Rows[e.RowIndex].FindControl("ddlSupname")).SelectedValue.ToString();
            string mSSIRDesc = ((DropDownList)this.gvAprovInfo.Rows[e.RowIndex].FindControl("ddlSupname")).SelectedItem.Text.Trim();
            string spcfcod = ((DropDownList)this.gvAprovInfo.Rows[e.RowIndex].FindControl("ddlspecification")).SelectedValue.ToString();
            string spcfdesc = ((DropDownList)this.gvAprovInfo.Rows[e.RowIndex].FindControl("ddlspecification")).SelectedItem.Text.Trim();
            string mAPROVQTY = Convert.ToDouble("0" + ((TextBox)this.gvAprovInfo.Rows[e.RowIndex].FindControl("txtgvNewOrderQty")).Text.Trim()).ToString();

            string mAPROVRATE = Convert.ToDouble("0" + ((TextBox)this.gvAprovInfo.Rows[e.RowIndex].FindControl("txtgvNewApprovRate")).Text.Trim()).ToString();
            // string mAPROVRATE = Convert.ToDouble("0" + ((Label)this.gvAprovInfo.Rows[e.RowIndex].FindControl("lgvNewApprovRate")).Text.Trim()).ToString();
            int index = (this.gvAprovInfo.PageIndex) * this.gvAprovInfo.PageSize + e.RowIndex;
            tbl1.Rows[index]["ssircode"] = mSSIRCODE;
            tbl1.Rows[index]["aprovqty"] = mAPROVQTY;
            tbl1.Rows[index]["aprovrate"] = mAPROVRATE;
            tbl1.Rows[index]["ssirdesc1"] = mSSIRDesc;
            tbl1.Rows[index]["spcfcod"] = spcfcod;
            tbl1.Rows[index]["spcfdesc"] = spcfdesc;
            ViewState["tblAprov"] = tbl1;
            this.gvAprovInfo.EditIndex = -1;
            this.gvAprovInfo_DataBind();
        }


        protected void gvAprovInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)ViewState["tblAprov"];
            string mAPROVNO = this.lblCurAprovNo1.Text.Trim().Substring(0, 3) + this.txtCurAprovDate.Text.Trim().Substring(6, 4) + this.lblCurAprovNo1.Text.Trim().Substring(3, 2) + this.txtCurAprovNo2.Text.Trim();
            string reqno = ((Label)this.gvAprovInfo.Rows[e.RowIndex].FindControl("lblgvReqNo")).Text.Trim();
            string rescode = ((Label)this.gvAprovInfo.Rows[e.RowIndex].FindControl("lblgvResCod")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEAPPRES",
                        mAPROVNO, reqno, rescode, "", "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {

                int rowindex = (this.gvAprovInfo.PageSize) * (this.gvAprovInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("rsircode<>''");
                ViewState["tblAprov"] = dv.ToTable();
                this.gvAprovInfo_DataBind();
            }
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            DataTable tbl1 = (DataTable)ViewState["tblAprov"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mAPROVNO = this.lblCurAprovNo1.Text.Trim().Substring(0, 3) + this.txtCurAprovDate.Text.Trim().Substring(6, 4) + this.lblCurAprovNo1.Text.Trim().Substring(3, 2) + this.txtCurAprovNo2.Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEPURPROGMAM", mAPROVNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Order Process";
                string eventdesc = "Delete Process";
                string eventdesc2 = mAPROVNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void lbtnUpdateSpeDetails_OnClick(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string Desc = this.txtspcfdesc.Text.Trim();
            string sircod = this.ddlSupList.SelectedValue.ToString().Substring(0, 9);//.Substring (0, 9)
            List<RealEntity.C_14_Pro.EClassPur.LastSupplier> lst = objuserman.GetLastSupplierCode(comcod, sircod);
            string sircode = lst[0].sircode;


            bool result = this.purData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTUPDATE", sircode.Substring(0, 2), sircode, Desc, "", "", "", "0", userid, "", "",
                    "", "", "", "", "");

            //bool result = this.purData.UpdateTransInfo (comcod, "SP_ENTRY_CODEBOOK", "SPACCOUNTUPDATE", spcfcod.Substring (0, 2), spcfcod, Desc, "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                return;
            }
        }

    }
}
