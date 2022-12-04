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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_09_PImp
{
    public partial class PurLabIssue2 : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);


                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "Current") ? "Sub-Contractor Bill-Work Wise"
                    : (this.Request.QueryString["Type"].ToString() == "CheckaVerify") ? "Sub-Contractor Bill-Checked"
                    : (this.Request.QueryString["Type"].ToString() == "FirstRecom") ? "Sub-Contractor Bill-First Recommendation"
                    : (this.Request.QueryString["Type"].ToString() == "SecRecom") ? "Sub-Contractor Bill-Second Recommendation"
                    : (this.Request.QueryString["Type"].ToString() == "ThirdRecom") ? "Sub-Contractor Bill-Third Recommendation"
                    : (this.Request.QueryString["Type"].ToString() == "Edit") ? " Sub-Contractor Bill-Work Wise Edit"
                    : (this.Request.QueryString["Type"].ToString() == "BillApproval") ? " Sub-Contractor Bill Approval"
                    : "Labour Issue Information";

                this.GetProjectList();
                this.GetConList();
                this.GetTrade();
                this.DateForOpeningBill();
                this.ComRefText();
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

                if (this.Request.QueryString["genno"].ToString().Length > 0)
                {
                    this.ibtnPreBillList_Click(null, null);
                    if (this.Request.QueryString["Type"].ToString() == "BillApproval")
                    {
                        this.lbtnOk_Click(null, null);
                    }
                }

            }
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
        private void ComRefText()
        {

            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3335":
                    //case "3101":
                    this.txtRefno.Enabled = false;
                    this.grvissue.Columns[7].Visible = false;
                    this.grvissue.Columns[8].Visible = false;
                    this.grvissue.Columns[11].Visible = false;
                    this.grvissue.Columns[14].Visible = false;
                    break;

                case "3336":
                case "3337":
                    this.grvissue.Columns[8].Visible = false;
                    this.grvissue.Columns[9].Visible = false;
                    this.grvissue.Columns[11].Visible = false;
                    this.grvissue.Columns[12].Visible = false;
                    this.grvissue.Columns[13].Visible = false;
                    this.grvissue.Columns[14].Visible = false;
                    //this.grvissue.Columns[7].Visible = true;
                    //this.grvissue.Columns[10].Visible = true;
                    this.txtRefno.Enabled = false;
                    this.lblTrade.Visible = false;
                    this.ddltrade.Visible = false;
                    //this.pnlsecurity.Visible = true;

                    break;

                case "3339"://Tropical
                case "3101"://Tropical
                case "3368"://finlay
                case "3367"://epic
                case "3370"://cpdl

                    //this.grvissue.Columns[7].Visible = false;
                    //this.grvissue.Columns[8].Visible = false;
                    //this.grvissue.Columns[12].Visible = false;
                    // this.grvissue.Columns[17].Visible = true;
                    //this.grvissue.Columns[18].Visible = true;

                    this.grvissue.Columns[14].Visible = false;
                    this.grvissue.Columns[7].Visible = false;
                    this.grvissue.Columns[9].Visible = false;
                    this.grvissue.Columns[11].Visible = false;
                    this.grvissue.Columns[12].Visible = true;
                    this.grvissue.Columns[13].Visible = true;

                    this.grvissue.Columns[19].Visible = true;
                    this.grvissue.Columns[20].Visible = true;
                    this.ddlgroup.Visible = true;
                    this.lblgrp.Visible = true;

                    //this.ddlgroup.Visible = true;
                    //this.lblgrp.Visible = true;
                    break;

                case "3340": // Urban

                    this.grvissue.Columns[7].Visible = false;
                    this.grvissue.Columns[8].Visible = false;
                    this.grvissue.Columns[9].Visible = false;
                    this.grvissue.Columns[10].Visible = false;
                    this.grvissue.Columns[11].Visible = false;
                    this.grvissue.Columns[12].Visible = false;
                    this.grvissue.Columns[13].Visible = false;
                    this.grvissue.Columns[14].Visible = true;
                    this.grvissue.Columns[17].Visible = true;

                    //this.grvissue.Columns[18].Visible = false;
                    //this.ddlgroup.Visible = true;
                    //this.lblgrp.Visible = true;se
                    break;

                default:

                    this.grvissue.Columns[7].Visible = false;
                    this.grvissue.Columns[8].Visible = false;
                    this.grvissue.Columns[9].Visible = false;

                    break;
            }


        }
        protected void GetLabSuEntNo()
        {

            string comcod = this.GetCompCode();
            string mREQNO = "NEWLISS";
            if (this.ddlPrevISSList.Items.Count > 0)
                mREQNO = this.ddlPrevISSList.SelectedValue.ToString();
            string mREQDAT = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();
            string Prefix = (this.Request.QueryString["Type"] == "Opening") ? "OPB" : "LIS";
            if (mREQNO == "NEWLISS")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTLABISSUENO", mREQDAT,
                       Prefix, "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mREQNO = ds2.Tables[0].Rows[0]["maxmisuno"].ToString();
                    this.lblCurISSNo1.Text = ds2.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(0, 6);
                    this.txtCurISSNo2.Text = ds2.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(6, 5);
                    this.ddlPrevISSList.DataTextField = "maxmisuno1";
                    this.ddlPrevISSList.DataValueField = "maxmisuno";
                    this.ddlPrevISSList.DataSource = ds2.Tables[0];
                    this.ddlPrevISSList.DataBind();
                }
            }
        }




        private void GetProjectList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string ddldesc = hst["ddldesc"].ToString();
            string comcod = this.GetCompCode();
            string pactcode = (this.Request.QueryString["prjcode"].ToString()).Length == 0 ? ("%" + this.txtSrcPro.Text.Trim() + "%") : (this.Request.QueryString["prjcode"].ToString() + "%");
            this.txtCurISSDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEPRJLIST", pactcode, userid, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            string TextField = (ddldesc == "True" ? "actdesc" : "actdesc1");
            this.ddlprjlist.DataTextField = TextField;
            this.ddlprjlist.DataValueField = "actcode";
            this.ddlprjlist.DataSource = ds1.Tables[0];
            this.ddlprjlist.DataBind();
            ds1.Dispose();
        }

        private void GetConList()
        {
            string comcod = this.GetCompCode();
            //string conlist = "%" + this.txtSrcSub.Text + "%";
            string conlist = (this.Request.QueryString["sircode"].ToString()).Length == 0 ? ("%" + this.txtSrcSub.Text.Trim() + "%") : (this.Request.QueryString["sircode"].ToString() + "%");

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUECONTLIST", conlist, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlcontractorlist.DataTextField = "sircode1";
            this.ddlcontractorlist.DataValueField = "sircode";
            this.ddlcontractorlist.DataSource = ds1.Tables[0];
            this.ddlcontractorlist.DataBind();


            this.ddlgroup.DataTextField = "grpdesc";
            this.ddlgroup.DataValueField = "grp";
            this.ddlgroup.DataSource = ds1.Tables[1];
            this.ddlgroup.DataBind();
        }
        private void GetTrade()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETTRADENAME", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddltrade.DataTextField = "tradedesc";
            this.ddltrade.DataValueField = "tradecod";
            this.ddltrade.DataSource = ds1.Tables[0];
            this.ddltrade.DataBind();

            // R/A No



            DataTable dt = ds1.Tables[1];
            DataView dv;

            switch (comcod)
            {

                case "3335":
                    //case "3101":
                    dv = dt.DefaultView;
                    dv.RowFilter = ("racode<>''");

                    break;

                default:
                    dv = dt.DefaultView;
                    break;




            }
            this.ddlRA.DataTextField = "radesc";
            this.ddlRA.DataValueField = "racode";
            this.ddlRA.DataSource = dv.ToTable();
            this.ddlRA.DataBind();
            ds1.Dispose();
            this.ddlRA_SelectedIndexChanged(null, null);



        }
        private void DateForOpeningBill()
        {
            string Type = this.Request.QueryString["Type"].ToString();

            if (Type == "Opening")
            {
                string comcod = this.GetCompCode();
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                this.txtCurISSDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                this.txtCurISSDate.Enabled = false;

            }





        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {


            string lisuno = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_99_Allinterface/";
            string currentptah = "PurchasePrint.aspx?Type=ConBillPrint&lisuno=" + lisuno + "&pactcode=" + pactcode;
            string totalpath = hostname + currentptah;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";

            //string comcod = this.GetCompCode();

            //switch (comcod)
            //{  




            //    case "3315":
            //    case "3316":
            //    case "3317":
            //    //case "3101":
            //        this.PrintLabIssueAssure();
            //        break;
            //    case "3339":
            //        this.PrintLabIssueSubCon();
            //        break;
            //    default:
            //        this.PrintLabIssue();
            //        break;
            //}
        }

        private void PrintLabIssueSubCon()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ddldesc = hst["ddldesc"].ToString();
            string TextField = (ddldesc == "True" ? this.ddlprjlist.SelectedItem.Text.Trim().ToString() : this.ddlprjlist.SelectedItem.Text.Substring(14));
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)ViewState["tblmatissue"];
            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.EClassOrder.Labissue>();
            var TAmt = lst.Select(p => p.isuamt).Sum();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptLabIssueSubCon", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Rptname", "Sub Contractor Bill (R/A Wise)"));
            Rpt1.SetParameters(new ReportParameter("ProjectName", "Project Name: " + TextField));
            Rpt1.SetParameters(new ReportParameter("SubContNam", "Sub Contractor Name: " + this.ddlcontractorlist.SelectedItem.Text.Substring(13)));
            Rpt1.SetParameters(new ReportParameter("IssueNo", "Issue No: " + this.lblCurISSNo1.Text.Trim() + this.txtCurISSNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("BillRef", "Bill Ref. No: " + this.txtRefno.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("Date", "Date: " + this.txtCurISSDate.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("naration", this.txtISSNarr.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("printFooter", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(TAmt), 2)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }




        private void PrintLabIssueAssure()
        {
            DataTable dt = (DataTable)ViewState["tblmatissue"];

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.SubConBill.ConRaBill>();
            var TAmt = lst.Select(p => p.isuamt).Sum();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.rptLabIssueAssure", lst, null, null);




            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Sub Contractor Bill (R/A Wise)"));
            Rpt1.SetParameters(new ReportParameter("ProjectName", "Project Name: " + this.ddlprjlist.SelectedItem.Text.Substring(14)));
            Rpt1.SetParameters(new ReportParameter("txtSubConNam", "Sub Contractor Name: " + this.ddlcontractorlist.SelectedItem.Text.Substring(13)));
            Rpt1.SetParameters(new ReportParameter("txttrade", "Trade Name: " + this.ddltrade.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("Issueno", "Issue No: " + this.lblCurISSNo1.Text.Trim() + this.txtCurISSNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtRefno", "Bill Ref. No: " + this.txtRefno.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("date", "Date: " + this.txtCurISSDate.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("narrationname", this.txtISSNarr.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintLabIssue()
        {

            DataTable dt = (DataTable)ViewState["tblmatissue"];

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.SubConBill.ConRaBill>();
            var TAmt = lst.Select(p => p.isuamt).Sum();


            switch (comcod)
            {
                case "3338":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.rptLabIssueAcme", lst, null, null);
                    break;

                case "3336":
                case "3337":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.rptLabIssueSuvastu", lst, null, null);
                    break;


                case "3340":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.rptLabIssueUrban", lst, null, null);
                    break;

                default:
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptLabIssue", lst, null, null);
                    // rptstk = new RealERPRPT.R_09_PImp.rptLabIssue();
                    break;



            }


            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Sub Contractor Bill (R/A Wise)"));
            Rpt1.SetParameters(new ReportParameter("ProjectName", "Project Name: " + this.ddlprjlist.SelectedItem.Text.Substring(14)));
            Rpt1.SetParameters(new ReportParameter("txtSubConNam", "Sub Contractor Name: " + this.ddlcontractorlist.SelectedItem.Text.Substring(13)));
            Rpt1.SetParameters(new ReportParameter("Issueno", "Issue No: " + this.lblCurISSNo1.Text.Trim() + this.txtCurISSNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtRefno", "Bill Ref. No: " + this.txtRefno.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("date", "Date: " + this.txtCurISSDate.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("narrationname", this.txtISSNarr.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void lbtnPrevISSList_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string ProjectCode = this.ddlprjlist.SelectedValue.ToString();
            string Contractor = this.ddlcontractorlist.SelectedValue.ToString();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPREVLISSUELIST", ProjectCode, Contractor, CurDate1, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevISSList.Items.Clear();
            this.ddlPrevISSList.DataTextField = "lisuno1";
            this.ddlPrevISSList.DataValueField = "lisuno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)         // OK Button
        {
            if (this.lbtnOk.Text == "New")
            {
                this.lbtnOk.Text = "Ok";
                this.ddlprjlist.Visible = true;
                this.ddlcontractorlist.Visible = true;
                this.lblddlProject.Visible = false;
                this.lblSubContractor.Visible = false;
                this.lblCurISSNo1.Text = "LIS00-";
                this.txtCurISSNo2.Text = "";
                this.txtISSNarr.Text = "";
                this.lblBillno.Text = "";

                this.lbtnPrevISSList.Visible = true;
                this.ddlPrevISSList.Visible = true;
                this.txtSrcPreBill.Visible = true;
                this.ibtnPreBillList.Visible = true;
                this.txtCurISSDate.Enabled = (this.Request.QueryString["Type"].ToString() == "Opening") ? false : true;
                this.ddlPrevISSList.Items.Clear();
                this.ddlcontractorlist.Enabled = true;
                this.ddlRA.Enabled = true;
                this.ddlWorkList.Items.Clear();
                DropCheck1.Items.Clear();
                this.PnlRes.Visible = false;
                this.PnlNarration.Visible = false;
                this.grvissue.DataSource = null;
                this.grvissue.DataBind();
                this.lblBillno.Text = "";
                this.lblvalvounum.Text = "";
                this.txtSearchLabour.Text = "";
                //  this.txtRefno.Text = "";
                //this.lbljavascript.Text = "";
                this.ClearSecurity();
                return;
            }


            this.lblddlProject.Text = this.ddlprjlist.SelectedItem.Text.Trim();
            this.lblSubContractor.Text = this.ddlcontractorlist.SelectedItem.Text.Trim();
            this.ddlprjlist.Visible = false;
            this.lblddlProject.Visible = true;
            this.ddlcontractorlist.Visible = false;
            this.lblSubContractor.Visible = true;
            this.lbtnPrevISSList.Visible = false;
            this.ddlPrevISSList.Visible = false;
            this.txtSrcPreBill.Visible = false;
            this.ibtnPreBillList.Visible = false;
            this.ddlcontractorlist.Enabled = true;
            this.PnlRes.Visible = true;
            this.PnlNarration.Visible = true;
            this.lbtnOk.Text = "New";
            //this.GetFloorCode();
            this.GetCataGory();
            //this.GetMaterials();
            this.Get_Issue_Info();
            this.SupplierOverallAdvanced(this.ddlprjlist.SelectedValue.ToString(), this.ddlcontractorlist.SelectedValue.ToString());
            if (this.Request.QueryString["Type"].ToString() == "BillApproval")
            {
                this.Pnlgrp.Visible = false;
                //this.pnlsecurity.Visible = false;
                //this.PnlNarration.Visible = false;
            }

        }


        private void GetFloorCode()
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString().Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEFLRLIST", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.DropCheck1.DataTextField = "flrdes";
            this.DropCheck1.DataValueField = "flrcod";
            this.DropCheck1.DataSource = ds1.Tables[0];
            this.DropCheck1.DataBind();

            //string Worklist=this.ddlWorkList.SelectedValue.ToString();

            //DataTable dt = ((DataTable)ViewState["itemlist"]).Copy();
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = ("rsircode= " + Worklist);
            //dt = dv.ToTable(true, "flrcod");

            //data.DefaultView.ToTable(true, "Id", "Name", "Role", "DC1", "DC2", "DC3", "DC4", "DC5", "DC6", "DC7");





            //this.ddlfloorno_SelectedIndexChanged(null, null);

        }
        //protected void ddlfloorno_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.GetMaterials();
        //}

        protected void ibtnSearchMaterisl_Click(object sender, EventArgs e)
        {
            this.GetMaterials();
        }
        private string ComCalltype()
        {

            string withmat = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3335":
                case "3333":

                    // case "3101":
                    withmat = "WithMat";
                    break;




                default:
                    withmat = "";
                    break;




            }

            return withmat;


        }
        private string ComZeroBal()
        {

            string zerobal = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3336":
                    zerobal = "Zero";
                    break;

                default:
                    zerobal = "";
                    break;

            }

            return zerobal;


        }

        private void GetCataGory()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            //string flrcode = this.ddlfloorno.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string SearchMat = "%" + this.txtSearchLabour.Text.Trim() + "%";
            string withmat = this.ComCalltype();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETCATAGORY", pactcode, date, "", SearchMat, withmat, "", "", "", "");

            if (ds1 == null)
                return;

            this.ddlcatagory.DataValueField = "rsircode";
            this.ddlcatagory.DataTextField = "rsirdesc";
            this.ddlcatagory.DataSource = ds1.Tables[0];
            this.ddlcatagory.DataBind();
            this.GetMaterials();



        }

        private void GetMaterials()
        {
            //DropCheck1.SelectedValue = "";
            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            //string flrcode = this.ddlfloorno.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string SearchMat = (ddlcatagory.SelectedValue.ToString() == "") ? "%" : this.ddlcatagory.SelectedValue.Substring(0, 4) + "%";
            // string SearchMat = (ddlcatagory.SelectedValue.ToString() == "") ? "%"  : this.ddlcatagory.SelectedValue.Substring(0, 9) + "%"; // sub head rupayan

            // string SearchMat = "%" + this.txtSearchLabour.Text.Trim() + "%";
            string withmat = this.ComCalltype();
            string zerobal = this.ComZeroBal();
            // DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETCATAGORY", pactcode, date, flrcode, SearchMat, withmat, zerobal, "", "", "");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLABFLRCODE", pactcode, date, "", SearchMat, withmat, zerobal, "", "", "", "");
            ViewState["itemlist"] = ds1.Tables[0];
            if (ds1 == null)
                return;

            this.ddlWorkList.DataTextField = "rsirdesc1";
            this.ddlWorkList.DataValueField = "rsircode";
            this.ddlWorkList.DataSource = ds1.Tables[1];
            this.ddlWorkList.DataBind();
            ds1.Dispose();
            this.ddlWorkList_SelectedIndexChanged(null, null);

        }
        protected void ddlWorkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Worklist = this.ddlWorkList.SelectedValue.ToString();

            DataTable dt = ((DataTable)ViewState["itemlist"]).Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("rsircode= " + Worklist);
            dt = dv.ToTable(true, "flrcod", "flrdes", "flrdes1");

            this.DropCheck1.DataTextField = "flrdes1";
            this.DropCheck1.DataValueField = "flrdes1";
            this.DropCheck1.DataSource = dt;
            this.DropCheck1.DataBind();
        }
        private void Get_Issue_Info()
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy"); 

            string mISSNo = "NEWLISS";
            if (this.ddlPrevISSList.Items.Count > 0)
            {
                this.txtCurISSDate.Enabled = false;
                this.ddlRA.Enabled = false;
                mISSNo = this.ddlPrevISSList.SelectedValue.ToString();
            }
            DataSet ds1 = new DataSet();
            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURLABISSUEINFO", mISSNo, CurDate1,
                         pactcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblmatissue"] = this.HiddenSameData(ds1.Tables[0]);

            if (mISSNo == "NEWLISS")
            {

                string Prefix = (this.Request.QueryString["Type"] == "Opening") ? "OPB" : "LIS";

                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTLABISSUENO", CurDate1,
                       Prefix, "", "", "", "", "", "", "");

                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    this.lblCurISSNo1.Text = ds1.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(0, 6);
                    this.txtCurISSNo2.Text = ds1.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(6, 5);
                }


                //string SearchMat = "%";
                //DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLABFLRCODE", pactcode, CurDate1, "", SearchMat, "", "", "", "", "");
                //ViewState["tblmatissue"] = ds2.Tables[0];
                //if (ds2 == null)
                //    return;
                //ds2.Dispose();

                //this.grvissue_DataBind();
                return;
            }

            this.lblfloorno.Visible = true;
            this.ddlWorkList.Visible = true;
            this.txtSearchLabour.Visible = true;
            this.ibtnSearchMaterisl.Visible = true;
            DropCheck1.Visible = true;
            this.lbtnSelect.Visible = true;
            this.txtRefno.Text = ds1.Tables[1].Rows[0]["lisurefno"].ToString();
            this.lblCurISSNo1.Text = ds1.Tables[1].Rows[0]["lisuno1"].ToString().Substring(0, 6);
            this.txtCurISSNo2.Text = ds1.Tables[1].Rows[0]["lisuno1"].ToString().Substring(6, 5);
            this.txtCurISSDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["isudat"]).ToString("dd-MMM-yyyy");
            this.ddlprjlist.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.lblddlProject.Text = this.ddlprjlist.SelectedItem.Text.Trim();
            this.ddlcontractorlist.SelectedValue = ds1.Tables[1].Rows[0]["csircode"].ToString();
            this.txtISSNarr.Text = ds1.Tables[1].Rows[0]["rmrks"].ToString();
            this.lblBillno.Text = ds1.Tables[1].Rows[0]["billno"].ToString();
            this.lblvalvounum.Text = ds1.Tables[1].Rows[0]["vounum"].ToString();
            this.ddltrade.SelectedValue = ds1.Tables[1].Rows[0]["tradecod"].ToString();
            this.ddlRA.SelectedValue = ds1.Tables[1].Rows[0]["rano"].ToString();


            this.txtpercentage.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["percntge"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.txtSDAmount.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["sdamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtDedAmount.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["dedamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtPenaltyAmount.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["penamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtAdvanced.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtreward.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["reward"]).ToString("#,##0.00;(#,##0.00); ");

            double amount = Convert.ToDouble((Convert.IsDBNull(((DataTable)ViewState["tblmatissue"]).Compute("sum(isuamt)", "")) ? 0.00 : ((DataTable)ViewState["tblmatissue"]).Compute("sum(isuamt)", "")));
            double security = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim());
            double deduction = Convert.ToDouble("0" + this.txtDedAmount.Text.Trim());
            double penalty = Convert.ToDouble("0" + this.txtPenaltyAmount.Text.Trim());
            double Advanced = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim());
            double Reward = Convert.ToDouble("0" + this.txtreward.Text.Trim());

            this.lblvalnettotal.Text = (amount + Reward - (security + deduction + penalty + Advanced)).ToString("#,##0;(#,##0); ");

            this.grvissue_DataBind();

            // ((LinkButton)this.grvissue.FooterRow.FindControl("lnkupdate")).Visible = (ds1.Tables[1].Rows[0]["billno"].ToString() == "00000000000000");

        }


        private void ClearSecurity()
        {
            this.txtpercentage.Text = "";
            this.txtSDAmount.Text = "";
            this.txtDedAmount.Text = "";
            this.txtPenaltyAmount.Text = "";
            this.txtAdvanced.Text = "";
            this.txtreward.Text = "";
            this.lblvalnettotal.Text = "";
        }
        protected void grvissue_DataBind()
        {

            this.grvissue.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvissue.DataSource = (DataTable)ViewState["tblmatissue"];
            this.grvissue.DataBind();


            //this.grvissue.Columns[1].Visible = ((this.Request.QueryString["Type"].ToString().Trim() == "Opening" || this.Request.QueryString["Type"].ToString().Trim() == "Edit" || this.Request.QueryString["Type"].ToString().Trim() == "Current")) && (this.lblvalvounum.Text.Trim() == "00000000000000" || this.lblvalvounum.Text.Trim() == "");
            //((LinkButton)this.grvissue.FooterRow.FindControl("lbtnDeleteBill")).Visible = (this.Request.QueryString["Type"].ToString().Trim() == "Edit" && this.lblBillno.Text.Trim() == "00000000000000");
            //((LinkButton)this.grvissue.FooterRow.FindControl("lnkupdate")).Visible =  (this.lblvalvounum.Text.Trim() == "00000000000000" || this.lblvalvounum.Text.Trim() == "");

            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3336":
                case "3337":
                    this.grvissue.Columns[1].Visible = ((this.Request.QueryString["Type"].ToString().Trim() == "Opening" || this.Request.QueryString["Type"].ToString().Trim() == "Edit" || this.Request.QueryString["Type"].ToString().Trim() == "Current")) && (this.lblBillno.Text.Trim() == "00000000000000" || this.lblvalvounum.Text.Trim() == "");
                    ((LinkButton)this.grvissue.FooterRow.FindControl("lbtnDeleteBill")).Visible = (this.Request.QueryString["Type"].ToString().Trim() == "Edit" && this.lblBillno.Text.Trim() == "00000000000000");
                    ((LinkButton)this.grvissue.FooterRow.FindControl("lnkupdate")).Visible = (this.lblBillno.Text.Trim() == "00000000000000" || this.lblBillno.Text.Trim() == "");

                    for (int i = 0; i < this.grvissue.Rows.Count; i++)
                        ((TextBox)this.grvissue.Rows[i].FindControl("txtissueamt")).Enabled = false;
                    break;


                default:
                    this.grvissue.Columns[1].Visible = ((this.Request.QueryString["Type"].ToString().Trim() == "Opening" || this.Request.QueryString["Type"].ToString().Trim() == "Edit" || this.Request.QueryString["Type"].ToString().Trim() == "Current")) && (this.lblvalvounum.Text.Trim() == "00000000000000" || this.lblvalvounum.Text.Trim() == "");
                    ((LinkButton)this.grvissue.FooterRow.FindControl("lbtnDeleteBill")).Visible = (this.Request.QueryString["Type"].ToString().Trim() == "Edit" && this.lblBillno.Text.Trim() == "00000000000000");
                    ((LinkButton)this.grvissue.FooterRow.FindControl("lnkupdate")).Visible = (this.lblvalvounum.Text.Trim() == "00000000000000" || this.lblvalvounum.Text.Trim() == "");

                    break;
            }

            if (this.Request.QueryString["Type"] == "BillApproval")
            {
                switch (comcod)
                {
                    case "3370":
                        ((LinkButton)this.grvissue.FooterRow.FindControl("lnkTotal")).Visible = true;
                        break;
                    default:
                        ((LinkButton)this.grvissue.FooterRow.FindControl("lnkTotal")).Visible = false;
                        break;
                }
               
                ((LinkButton)this.grvissue.FooterRow.FindControl("lnkupdate")).Visible = false;
                ((LinkButton)this.grvissue.FooterRow.FindControl("lnkApproved")).Visible = true;
            }
            this.FooterCalculaton();

        }

        private void FooterCalculaton()
        {

            DataTable dt = (DataTable)ViewState["tblmatissue"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.grvissue.FooterRow.FindControl("lblFissueamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(isuamt)", "")) ? 0.00 : dt.Compute("Sum(isuamt)", ""))).ToString("#,##0.0000;(#,##0.0000); ");


        }


        protected void lbtnSelect_Click(object sender, EventArgs e)         // Select Button
        {


            try
            {
                this.SaveValue();
                string rsircode = this.ddlWorkList.SelectedValue.ToString().Trim();

                //String[] lab = this.DropCheck1.Text.Trim().Split(',');
                //String[] lab11 = this.DropCheck1.SelectedValue.ToString().Split(',');
                string grp = this.ddlgroup.SelectedValue.ToString();
                string grpdesc = this.ddlgroup.SelectedItem.Text;
                foreach (ListItem lab1 in DropCheck1.Items)
                {
                    if (lab1.Selected)
                    {
                        string flrcode = lab1.Value.Substring(0, 3);
                        // string rsirdesc = lab1.Substring(13);

                        DataTable dt = (DataTable)ViewState["tblmatissue"];


                        DataRow[] dr = dt.Select(" flrcod='" + flrcode + "' and rsircode='" + rsircode + "' and grp='" + grp + "'");


                        DataTable dt1 = (DataTable)ViewState["itemlist"];
                        if (dr.Length == 0)
                        {

                            DataRow dr1 = dt.NewRow();
                            dr1["flrcod"] = flrcode;// this.ddlfloorno.SelectedValue.ToString();
                            dr1["flrdes"] = (((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "' and flrcod= '" + flrcode + "'"))[0]["flrdes"];// this.ddlfloorno.SelectedItem.Text.Trim();
                            dr1["grp"] = grp;
                            dr1["grpdesc"] = grpdesc;
                            dr1["rsircode"] = rsircode;
                            dr1["rsirdesc"] = (((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "' and flrcod= '" + flrcode + "'"))[0]["rsirdesc"];
                            dr1["rsirunit"] = (((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "' and flrcod= '" + flrcode + "'"))[0]["rsirunit"];
                            dr1["wrkqty"] = 0.00;
                            dr1["wrkrate"] = Convert.ToDouble((((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "' and flrcod= '" + flrcode + "'"))[0]["wrkrate"]).ToString();
                            dr1["balqty"] = Convert.ToDouble((((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "' and flrcod= '" + flrcode + "'"))[0]["balqty"]).ToString();
                            dr1["balamt"] = Convert.ToDouble((((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "' and flrcod= '" + flrcode + "'"))[0]["balamt"]).ToString();
                            dr1["preqty"] = Convert.ToDouble((((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "' and flrcod= '" + flrcode + "'"))[0]["preqty"]).ToString();
                            dr1["bgdrat"] = Convert.ToDouble((((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "' and flrcod= '" + flrcode + "'"))[0]["bgdrat"]).ToString();
                            dr1["peronbgd"] = Convert.ToDouble((((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "' and flrcod= '" + flrcode + "'"))[0]["peronbgd"]).ToString();

                            dr1["prcent"] = 0.00;
                            dr1["isuqty"] = 0.00;
                            dr1["isurat"] = Convert.ToDouble((((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "' and flrcod= '" + flrcode + "'"))[0]["isurat"]).ToString();
                            dr1["amount"] = 0.00;
                            dr1["above"] = 0.00;
                            dr1["isuamt"] = 0.00;
                            dr1["dedqty"] = 0.00;
                            dr1["dedrate"] = 0.00;
                            dr1["idedamt"] = 0.00;
                            dr1["dedunit"] = "";
                            dr1["toqty"] = 0.00;
                            dr1["prevrat"] = Convert.ToDouble((((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["prevrat"]).ToString(); ;
                            dr1["mbbook"] = "";

                            dt.Rows.Add(dr1);

                        }
                        ViewState["tblmatissue"] = this.HiddenSameData(dt);
                        this.grvissue_DataBind();
                    }
                }
            }

            catch (Exception ed)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ed.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string grp = dt1.Rows[0]["grp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                    dt1.Rows[j]["grpdesc"] = "";


                grp = dt1.Rows[j]["grp"].ToString();


            }

            return dt1;
        }
        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.grvissue_DataBind();

        }
        protected void lnkCalculation_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblmatissue"];
            int TblRowIndex;
            for (int i = 0; i < this.grvissue.Rows.Count; i++)
            {
                double balqty = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.grvissue.Rows[i].FindControl("lblbalqty")).Text.Trim()));
                double dgvQty = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtisuqty")).Text.Trim());
                double percent = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtpercentge")).Text.Trim());
                double labrate = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtlabrate")).Text.Trim());



                TblRowIndex = (grvissue.PageIndex) * grvissue.PageSize + i;

                dt.Rows[TblRowIndex]["prcent"] = percent;

                dt.Rows[TblRowIndex]["isuqty"] = balqty * percent * 0.01;
                dt.Rows[TblRowIndex]["isurat"] = labrate;
                dt.Rows[TblRowIndex]["isuamt"] = balqty * percent * 0.01 * labrate;




            }
            ViewState["tblmatissue"] = dt;
            this.grvissue_DataBind();
        }

        private void CreateDataTable()
        {

            ViewState.Remove("tblapproval");
            DataTable tblt01 = new DataTable();

            tblt01.Columns.Add("chkid", Type.GetType("System.String"));
            tblt01.Columns.Add("chkdat", Type.GetType("System.String"));
            tblt01.Columns.Add("chktrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("chkseson", Type.GetType("System.String"));

            tblt01.Columns.Add("frecid", Type.GetType("System.String"));
            tblt01.Columns.Add("frecdat", Type.GetType("System.String"));
            tblt01.Columns.Add("frectrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("frecseson", Type.GetType("System.String"));
            tblt01.Columns.Add("secrecid", Type.GetType("System.String"));
            tblt01.Columns.Add("secrecdat", Type.GetType("System.String"));
            tblt01.Columns.Add("secrectrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("secrecseson", Type.GetType("System.String"));
            tblt01.Columns.Add("threcid", Type.GetType("System.String"));
            tblt01.Columns.Add("threcdat", Type.GetType("System.String"));
            tblt01.Columns.Add("threctrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("threcseson", Type.GetType("System.String"));
            tblt01.Columns.Add("fiappid", Type.GetType("System.String"));
            tblt01.Columns.Add("fiappdat", Type.GetType("System.String"));
            tblt01.Columns.Add("fiapptrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("fiappseson", Type.GetType("System.String"));

            ViewState["tblapproval"] = tblt01;
        }


        private string GetReqApproval(string approval)
        {


            string type = this.Request.QueryString["Type"];
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataSet ds1 = new DataSet("ds1");
            System.IO.StringReader xmlSR;

            switch (type)
            {
                case "Current":
                    switch (comcod)
                    {
                        // case "3101":
                        case "1103":
                            break;

                        default:
                            if (approval == "")
                            {


                                this.CreateDataTable();
                                DataTable dt = (DataTable)ViewState["tblapproval"];
                                DataRow dr1 = dt.NewRow();

                                dr1["chkid"] = usrid;
                                dr1["chkdat"] = Date;
                                dr1["chktrmid"] = trmnid;
                                dr1["chkseson"] = session;
                                dr1["frecid"] = usrid;
                                dr1["frecdat"] = Date;
                                dr1["frectrmid"] = trmnid;
                                dr1["frecseson"] = session;
                                dr1["secrecid"] = usrid;
                                dr1["secrecdat"] = Date;
                                dr1["secrectrmid"] = trmnid;
                                dr1["secrecseson"] = session;
                                dr1["threcid"] = usrid;
                                dr1["threcdat"] = Date;
                                dr1["threctrmid"] = trmnid;
                                dr1["threcseson"] = session;
                                dr1["fiappid"] = usrid;
                                dr1["fiappdat"] = Date;
                                dr1["fiapptrmid"] = trmnid;
                                dr1["fiappseson"] = session;
                                dt.Rows.Add(dr1);


                                ds1.Merge(dt);
                                ds1.Tables[0].TableName = "tbl1";
                                approval = ds1.GetXml();

                            }




                            break;

                    }

                    break;




                case "CheckaVerify":
                    // string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString();  

                    if (approval == "")
                    {


                        this.CreateDataTable();
                        DataTable dt = (DataTable)ViewState["tblapproval"];
                        DataRow dr1 = dt.NewRow();

                        dr1["chkid"] = usrid;
                        dr1["chkdat"] = Date;
                        dr1["chktrmid"] = trmnid;
                        dr1["chkseson"] = session;
                        dr1["frecid"] = "";
                        dr1["frecdat"] = "";
                        dr1["frectrmid"] = "";
                        dr1["frecseson"] = "";
                        dr1["secrecid"] = "";
                        dr1["secrecdat"] = "";
                        dr1["secrectrmid"] = "";
                        dr1["secrecseson"] = "";
                        dr1["threcid"] = "";
                        dr1["threcdat"] = "";
                        dr1["threctrmid"] = "";
                        dr1["threcseson"] = "";
                        dr1["fiappid"] = "";
                        dr1["fiappdat"] = "";
                        dr1["fiapptrmid"] = "";
                        dr1["fiappseson"] = "";
                        dt.Rows.Add(dr1);
                        ds1.Merge(dt);
                        ds1.Tables[0].TableName = "tbl1";
                        approval = ds1.GetXml();

                    }

                    else
                    {

                        xmlSR = new System.IO.StringReader(approval);
                        ds1.ReadXml(xmlSR);
                        ds1.Tables[0].TableName = "tbl1";
                        ds1.Tables[0].Rows[0]["chkid"] = usrid;
                        ds1.Tables[0].Rows[0]["chkdat"] = Date;
                        ds1.Tables[0].Rows[0]["chktrmid"] = trmnid;
                        ds1.Tables[0].Rows[0]["chkseson"] = session;
                        ds1.Tables[0].Rows[0]["frecid"] = "";
                        ds1.Tables[0].Rows[0]["frecdat"] = "";
                        ds1.Tables[0].Rows[0]["frectrmid"] = "";
                        ds1.Tables[0].Rows[0]["frecseson"] = "";
                        ds1.Tables[0].Rows[0]["secrecid"] = "";
                        ds1.Tables[0].Rows[0]["secrecdat"] = "";
                        ds1.Tables[0].Rows[0]["secrectrmid"] = "";
                        ds1.Tables[0].Rows[0]["secrecseson"] = "";
                        ds1.Tables[0].Rows[0]["threcid"] = "";
                        ds1.Tables[0].Rows[0]["threcdat"] = "";
                        ds1.Tables[0].Rows[0]["threctrmid"] = "";
                        ds1.Tables[0].Rows[0]["threcseson"] = "";
                        ds1.Tables[0].Rows[0]["fiappid"] = "";
                        ds1.Tables[0].Rows[0]["fiappdat"] = "";
                        ds1.Tables[0].Rows[0]["fiapptrmid"] = "";
                        ds1.Tables[0].Rows[0]["fiappseson"] = "";



                        approval = ds1.GetXml();






                    }

                    break;



                case "FirstRecom":
                    // string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString(); 
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["frecid"] = usrid;
                    ds1.Tables[0].Rows[0]["frecdat"] = Date;
                    ds1.Tables[0].Rows[0]["frectrmid"] = trmnid;
                    ds1.Tables[0].Rows[0]["frecseson"] = session;
                    ds1.Tables[0].Rows[0]["secrecid"] = "";
                    ds1.Tables[0].Rows[0]["secrecdat"] = "";
                    ds1.Tables[0].Rows[0]["secrectrmid"] = "";
                    ds1.Tables[0].Rows[0]["secrecseson"] = "";
                    ds1.Tables[0].Rows[0]["threcid"] = "";
                    ds1.Tables[0].Rows[0]["threcdat"] = "";
                    ds1.Tables[0].Rows[0]["threctrmid"] = "";
                    ds1.Tables[0].Rows[0]["threcseson"] = "";
                    ds1.Tables[0].Rows[0]["fiappid"] = "";
                    ds1.Tables[0].Rows[0]["fiappdat"] = "";
                    ds1.Tables[0].Rows[0]["fiapptrmid"] = "";
                    ds1.Tables[0].Rows[0]["fiappseson"] = "";
                    approval = ds1.GetXml();

                    break;




                case "SecRecom":
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["secrecid"] = usrid;
                    ds1.Tables[0].Rows[0]["secrecdat"] = Date;
                    ds1.Tables[0].Rows[0]["secrectrmid"] = trmnid;
                    ds1.Tables[0].Rows[0]["secrecseson"] = session;
                    approval = ds1.GetXml();

                    break;

                case "ThirdRecom":
                    // string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString();  
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["threcid"] = usrid;
                    ds1.Tables[0].Rows[0]["threcdat"] = Date;
                    ds1.Tables[0].Rows[0]["threctrmid"] = trmnid;
                    ds1.Tables[0].Rows[0]["threcseson"] = session;
                    approval = ds1.GetXml();
                    break;


                case "Approved":
                    // string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString();  
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["fiappid"] = usrid;
                    ds1.Tables[0].Rows[0]["fiappdat"] = Date;
                    ds1.Tables[0].Rows[0]["fiapptrmid"] = trmnid;
                    ds1.Tables[0].Rows[0]["fiappseson"] = session;
                    approval = ds1.GetXml();
                    break;





            }


            return approval;

        }

        protected void lnkupdate_Click(object sender, EventArgs e)      // Update Button
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            // DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string Sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();

            this.SaveValue();
            DataTable tbl2 = (DataTable)ViewState["tblmatissue"];

            string comcod = this.GetCompCode();
            if (ddlPrevISSList.Items.Count == 0)
            {
                this.GetLabSuEntNo();
            }
            string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string Refno = this.txtRefno.Text.Trim();

            switch (comcod)
            {

                case "3335":
                case "3336":
                case "3337":
                    //case "3101":
                    string pactcode = this.ddlprjlist.SelectedValue.ToString();
                    string csircode = this.ddlcontractorlist.SelectedValue.ToString();
                    DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "CHECKEDDURANO", Refno, pactcode, csircode, "", "", "", "", "", "");
                    if (ds2.Tables[0].Rows.Count == 0)
                        ;


                    else
                    {

                        DataView dv1 = ds2.Tables[0].DefaultView;
                        dv1.RowFilter = ("lisuno <>'" + mISUNO + "'");
                        DataTable dt = dv1.ToTable();
                        if (dt.Rows.Count == 0)
                            ;
                        else
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate R/A No";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            //this.ddlPrevReqList.Items.Clear();
                            return;
                        }
                    }
                    //if (bbgdamt < 0 || dgvBgdQty < dgvReqQty)


                    break;


                default:
                    break;

            }



            string percentage = Convert.ToDouble("0" + this.txtpercentage.Text.Replace("%", "").Trim()).ToString();
            string sdamt = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim()).ToString();
            string dedamt = Convert.ToDouble("0" + this.txtDedAmount.Text.Trim()).ToString();
            string Penalty = Convert.ToDouble("0" + this.txtPenaltyAmount.Text.Trim()).ToString();
            string advamt = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim()).ToString();
            string Reward = Convert.ToDouble("0" + this.txtreward.Text.Trim()).ToString();

            string mISUDAT = this.txtCurISSDate.Text.Trim();
            string mPACTCODE = this.ddlprjlist.SelectedValue.ToString().Trim();
            string mCONCODE = this.ddlcontractorlist.SelectedValue.ToString().Trim();
            string mISURNAR = this.txtISSNarr.Text.Trim();

            string trade = this.ddltrade.SelectedValue.ToString();
            string rano = this.ddlRA.SelectedValue.ToString();
            //string mbbookno = this.txtmbbook.Text.Trim();

            if (Refno.Length == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select R/A No";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            //string appxml = tbl2.Rows[0]["approval"].ToString();
            //string Approval = this.GetReqApproval(appxml);
            bool result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURLABISSUEINFO", "PURLISSUEB",
                             mISUNO, mISUDAT, mPACTCODE, mCONCODE, mISURNAR, Refno, usrid, Sessionid, trmid, trade, rano, percentage, sdamt, dedamt, Penalty, advamt, Reward, "", "", "");
            // string mISUDAT = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();
            // string mPACTCODE = this.ddlprjlist.SelectedValue.ToString().Trim();
            // string mCONCODE = this.ddlcontractorlist.SelectedValue.ToString().Trim();
            // string mISURNAR = this.txtISSNarr.Text.Trim();     
            // string trade = this.ddltrade.SelectedValue.ToString();

            // string rano = this.ddlRA.SelectedValue.ToString();
            // if (Refno.Length == 0)
            // {
            //     ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select R/A No";
            //     ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //     return;
            // }
            //// string mbbookno = this.txtmbbook.Text.Trim();
            // bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURLABISSUEINFO", "PURLISSUEB",
            //                  mISUNO, mISUDAT, mPACTCODE, mCONCODE, mISURNAR, Refno, usrid, Sessionid, trmid, trade, rano, "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            foreach (DataRow dr in tbl2.Rows)
            {
                string Flrcod = dr["flrcod"].ToString();
                string grp = dr["grp"].ToString();
                string Rsircode = dr["rsircode"].ToString();
                string prcent = Convert.ToDouble(dr["prcent"].ToString().Trim()).ToString();
                double Isuqty = Convert.ToDouble(dr["isuqty"].ToString().Trim());
                double Isuamt = Convert.ToDouble(dr["isuamt"].ToString().Trim());  //dr["isuamt"].ToString().Trim();
                string wrkqty = dr["wrkqty"].ToString().Trim();
                double balqty = Convert.ToDouble(dr["balqty"].ToString().Trim());
                string mbbook = dr["mbbook"].ToString().Trim();
                string above = dr["above"].ToString();
                string dedqty = dr["dedqty"].ToString();
                string dedunit = dr["dedunit"].ToString();
                string idedamt = dr["idedamt"].ToString();
                double balamt = Convert.ToDouble(dr["balamt"].ToString().Trim());

                // dedqty, dedunit, idedamt



                if (comcod == "3336" || comcod == "3337" || comcod == "3335" || comcod == "3340" || comcod == "1103" || comcod == "3344")
                {


                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURLABISSUEINFO", "PURLISSUEA", mISUNO, Flrcod,
                        Rsircode, prcent, Isuqty.ToString(), Isuamt.ToString(), wrkqty, grp, mbbook, above, dedqty, dedunit, idedamt, "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }



                }


                else if (comcod == "3339")
                {
                    if (balamt >= Isuamt)
                    {

                        result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURLABISSUEINFO", "PURLISSUEA", mISUNO, Flrcod,
                        Rsircode, prcent, Isuqty.ToString(), Isuamt.ToString(), wrkqty, grp, mbbook, above, dedqty, dedunit, idedamt, "");
                        if (!result)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }


                    }

                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Not Greater than balance amount  ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }


                }


                else
                {


                    if (balqty >= Isuqty)
                    {

                        result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURLABISSUEINFO", "PURLISSUEA", mISUNO, Flrcod,
                            Rsircode, prcent, Isuqty.ToString(), Isuamt.ToString(), wrkqty, grp, mbbook, above, dedqty, dedunit, idedamt, "");
                        if (!result)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }

                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }



                }

                //if (Isuqty > 0)
                //{

                //    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURLABISSUEINFO", "PURLISSUEA", mISUNO, Flrcod,
                //        Rsircode, prcent, Isuqty.ToString(), Isuamt, wrkqty, "", "", "", "", "", "", "");
                //    if (!result)
                //    {
                //       ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                //       ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                //        return;
                //    }

                //}

            }
            
            this.txtCurISSDate.Enabled = false;
            if (this.BillApprovalCompwise(mISUNO))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Data Updated Failed" + "');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Data Updated successfully" + "');", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Labour Issue Information";
                string eventdesc = "Update Labour QTY & RATE";
                string eventdesc2 = "Issue No: " + this.lblCurISSNo1.Text.Trim().Substring(0, 3) +
                        ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private bool BillApprovalCompwise(string _issuno) 
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string session = hst["session"].ToString();
            string trmnid = hst["compname"].ToString();
            string comcod = hst["comcod"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            bool isFailed = false; 
            switch (comcod)
            {
                case "3101":
                case "3368": // finlay
                case "3367": // epic
                case "3370": // cpdl
                case "3366": // lanco
                    break;
                default:
                    bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "ISSUEAPPROVEDAUTO", _issuno, usrid, Date, trmnid, session, "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        isFailed = true;
                    }
                    break;
            }
            return isFailed;
        }



        private void SaveValue()
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            DataTable dt = (DataTable)ViewState["tblmatissue"];
            int TblRowIndex;
            for (int i = 0; i < this.grvissue.Rows.Count; i++)
            {

                double wrkqty = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.grvissue.Rows[i].FindControl("txtwrkqty")).Text.Trim()));
                double balqty = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.grvissue.Rows[i].FindControl("lblbalqty")).Text.Trim()));
                double preqty = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.grvissue.Rows[i].FindControl("lblpreqty")).Text.Trim()));
                double dgvQty = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.grvissue.Rows[i].FindControl("txtisuqty")).Text.Trim()));
                double percent = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtpercentge")).Text.Trim());
                double labrate = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtlabrate")).Text.Trim());
                double amount = ASTUtility.StrPosOrNagative(((TextBox)this.grvissue.Rows[i].FindControl("txtgvamount")).Text.Trim());
                double above = ASTUtility.StrPosOrNagative(((TextBox)this.grvissue.Rows[i].FindControl("txtgvabove")).Text.Trim());

                double issueamt = ASTUtility.StrPosOrNagative(((TextBox)this.grvissue.Rows[i].FindControl("txtissueamt")).Text.Trim());
                string mbbook = (((TextBox)this.grvissue.Rows[i].FindControl("txtmbbook")).Text.Trim());
                TblRowIndex = (grvissue.PageIndex) * grvissue.PageSize + i;
                double toqty = preqty + dgvQty;

                string comcod = this.GetCompCode();
                TblRowIndex = (grvissue.PageIndex) * grvissue.PageSize + i;
                double isuqty;
                switch (comcod)
                {
                    case "3338":
                        dgvQty = (issueamt > 0) ? (labrate > 0 ? Math.Round(issueamt / labrate, 4) : 0.00) : dgvQty;
                        issueamt = (issueamt > 0) ? issueamt : (dgvQty * labrate);

                        dt.Rows[TblRowIndex]["wrkqty"] = wrkqty;
                        dt.Rows[TblRowIndex]["prcent"] = percent;
                        isuqty = (percent > 0) ? (wrkqty > 0 ? wrkqty * percent * 0.01 : balqty * percent * 0.01) : dgvQty;
                        dt.Rows[TblRowIndex]["isuqty"] = isuqty;
                        dt.Rows[TblRowIndex]["toqty"] = toqty;
                        dt.Rows[TblRowIndex]["isurat"] = labrate;
                        dt.Rows[TblRowIndex]["amount"] = amount;
                        dt.Rows[TblRowIndex]["above"] = above;
                        dt.Rows[TblRowIndex]["isuamt"] = issueamt;
                        dt.Rows[TblRowIndex]["mbbook"] = mbbook;

                        break;

                    default:
                        dt.Rows[TblRowIndex]["wrkqty"] = wrkqty;
                        dt.Rows[TblRowIndex]["prcent"] = percent;
                        isuqty = (percent > 0) ? (wrkqty > 0 ? wrkqty * percent * 0.01 : balqty * percent * 0.01) : dgvQty;
                        amount = isuqty * labrate;
                        issueamt = amount + (amount * 0.01 * above);
                        dt.Rows[TblRowIndex]["isuqty"] = isuqty;
                        dt.Rows[TblRowIndex]["toqty"] = toqty;
                        dt.Rows[TblRowIndex]["isurat"] = labrate;
                        dt.Rows[TblRowIndex]["amount"] = amount;
                        dt.Rows[TblRowIndex]["above"] = above;
                        dt.Rows[TblRowIndex]["isuamt"] = issueamt;
                        dt.Rows[TblRowIndex]["mbbook"] = mbbook;

                        break;


                }

                //dt.Rows[TblRowIndex]["wrkqty"] = wrkqty;
                //dt.Rows[TblRowIndex]["prcent"] = percent;
                //double isuqty = (percent > 0) ? (wrkqty > 0 ? wrkqty * percent * 0.01 : balqty * percent * 0.01) : dgvQty;
                //dt.Rows[TblRowIndex]["isuqty"] = isuqty;
                //dt.Rows[TblRowIndex]["isurat"] = labrate;
                //dt.Rows[TblRowIndex]["isuamt"] =isuqty* labrate;
            }
            ViewState["tblmatissue"] = dt;
        }


        protected void grvissue_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string ddldesc = hst["ddldesc"].ToString();
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblmatissue"];
            string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string Labcode = ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblitemcode")).Text.Trim();
            string Flrcode = ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblgvflrCode")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELETELABISUE", mISUNO, Flrcode, Labcode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.grvissue.PageSize) * (this.grvissue.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblmatissue");
            ViewState["tblmatissue"] = dv.ToTable();
            this.grvissue_DataBind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Labour Issue Information";
                string eventdesc = "Delete Labour";
                string eventdesc2 = "Project Name: " + ddldesc == "True" ? this.ddlprjlist.SelectedItem.Text : this.ddlprjlist.SelectedItem.Text.Substring(14) + "- " + "Sub Contractor Name: " +
                        this.ddlcontractorlist.SelectedItem.Text.Substring(14) + "- " + "Issue No: " + this.lblCurISSNo1.Text.Trim().Substring(0, 3) +
                        ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim() + "- " +
                        ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblitemcode")).Text.Trim();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)ViewState["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }
        protected void grvissue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.grvissue.PageIndex = e.NewPageIndex;
            this.grvissue_DataBind();
        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.grvissue_DataBind();
        }



        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectList();
        }
        protected void ibtnFindSubConName_Click(object sender, EventArgs e)
        {
            this.GetConList();
        }
        protected void ibtnPreBillList_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string ProjectCode = this.ddlprjlist.SelectedValue.ToString();
            string Contractor = this.ddlcontractorlist.SelectedValue.ToString();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            // string Prefix = (this.Request.QueryString["Type"] == "Opening") ? "OPB%" : "LIS%";

            string Prefix = this.Request.QueryString["Type"] == "Opening" ? "OPB%" :
               (this.Request.QueryString["genno"].ToString().Length == 0 ? "LIS%" : (this.Request.QueryString["genno"].ToString() + "%"));

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPREVLISSUELIST", ProjectCode, Contractor, CurDate1, Prefix, "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevISSList.Items.Clear();
            this.ddlPrevISSList.DataTextField = "lisuno1";
            this.ddlPrevISSList.DataValueField = "lisuno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();

        }
        protected void lbtnDeleteBill_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblmatissue"];
            string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELLISSUEAB", mISUNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Deleted Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            else
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Deleted Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }


        protected void ddlRA_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtRefno.Text = this.ddlRA.SelectedItem.Text.Trim();
        }

        protected void ddlcatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMaterials();
        }

        private void SaveDeposit()
        {

            DataTable dt = (DataTable)ViewState["tblmatissue"];
            if (((DataTable)ViewState["tblmatissue"]).Rows.Count == 0)
                return;
            string comcod = this.GetCompCode();
            double amount, penalty, deduc, netamt, percentage, sdamt, fpercntage;
            amount = Convert.ToDouble((Convert.IsDBNull(((DataTable)ViewState["tblmatissue"]).Compute("sum(isuamt)", "")) ? 0.00
                   : ((DataTable)ViewState["tblmatissue"]).Compute("sum(isuamt)", "")));
            penalty = Convert.ToDouble("0" + this.txtPenaltyAmount.Text.Trim());
            deduc = Convert.ToDouble("0" + this.txtDedAmount.Text.Trim());
            netamt = (amount - penalty - deduc);
            percentage = Convert.ToDouble("0" + this.txtpercentage.Text.Replace("%", "").Trim());
            sdamt = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim());

            switch (comcod)
            {
                case "3305":// Rupayan Housing
                case "3306":// Ratul
                case "3311":// Rupayan Chittagong 
                case "3310":// Rcu 


                case "3315":// Assure Builders
                case "3316":// Assure Development  

                case "1205": // p2p 
                case "3351":
                case "3352":
                case "3370": //cpdl
               


                    this.txtSDAmount.Text = sdamt > 0 ? sdamt.ToString("#,#,#0.00;(#, #,#0.00); ") : Convert.ToDouble(amount * percentage * 0.01).ToString("#,#,#0.00;(#, #,#0.00); ");
                    fpercntage = (sdamt > 0) ? (amount > 0 ? ((Convert.ToDouble(this.txtSDAmount.Text) * 100) / amount) : 0.00) : percentage;
                    break;


                default:
                    this.txtSDAmount.Text = sdamt > 0 ? sdamt.ToString("#,#,#0.00;(#, #,#0.00); ") : Convert.ToDouble(netamt * percentage * 0.01).ToString("#,#,#0.00;(#, #,#0.00); ");
                    fpercntage = (sdamt > 0) ? (netamt > 0 ? ((Convert.ToDouble(this.txtSDAmount.Text) * 100) / netamt) : 0.00) : percentage;
                    break;



            }

            this.txtpercentage.Text = fpercntage.ToString("#,#,#0.00;(#, #,#0.00); ") + "%";
            double security = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim());
            double deduction = Convert.ToDouble("0" + this.txtDedAmount.Text.Trim());
            double Advanced = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim());
            double Reward = Convert.ToDouble("0" + this.txtreward.Text.Trim());
            this.lblvalnettotal.Text = (amount + Reward - (security + deduction + penalty + Advanced)).ToString("#,##0;(#,##0); ");





            //DataTable dt = (DataTable)ViewState["tblmatissue"];
            //if (((DataTable)ViewState["tblmatissue"]).Rows.Count == 0)
            //    return;
            //double amount = Convert.ToDouble ((Convert.IsDBNull (((DataTable)ViewState["tblmatissue"]).Compute ("sum(isuamt)", "")) ? 0.00
            //        : ((DataTable)ViewState["tblmatissue"]).Compute ("sum(isuamt)", "")));
            //double penalty = Convert.ToDouble ("0" + this.txtPenaltyAmount.Text.Trim ());
            //double deduc = Convert.ToDouble ("0" + this.txtDedAmount.Text.Trim ());

            //double netamt = (amount - penalty - deduc);

            //double percentage = Convert.ToDouble ("0" + this.txtpercentage.Text.Replace ("%", "").Trim ());
            //double sdamt = Convert.ToDouble ("0" + this.txtSDAmount.Text.Trim ());

            //this.txtSDAmount.Text = sdamt > 0 ? sdamt.ToString ("#,#,#0.00;(#, #,#0.00); ") : Convert.ToDouble (netamt * percentage * 0.01).ToString ("#,#,#0.00;(#, #,#0.00); ");
            //double fpercntage = (sdamt > 0) ? (netamt > 0 ? ((Convert.ToDouble (this.txtSDAmount.Text) * 100) / netamt) : 0.00) : percentage;
            //this.txtpercentage.Text = fpercntage.ToString ("#,#,#0.00;(#, #,#0.00); ") + "%";
            //double security = Convert.ToDouble ("0" + this.txtSDAmount.Text.Trim ());
            //double deduction = Convert.ToDouble ("0" + this.txtDedAmount.Text.Trim ());
            //double Advanced = Convert.ToDouble ("0" + this.txtAdvanced.Text.Trim ());
            //double Reward = Convert.ToDouble ("0" + this.txtreward.Text.Trim ());
            //this.lblvalnettotal.Text = (amount + Reward - (security + deduction + penalty + Advanced)).ToString ("#,##0;(#,##0); ");
        }
        protected void lbtnDepost_Click(object sender, EventArgs e)
        {
            this.SaveDeposit();

        }

        protected void grvissue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                TextBox txtisuqty = (TextBox)e.Row.FindControl("txtisuqty");
                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();
                if (grp.Substring(0, 1) == "2")
                {
                    txtisuqty.Attributes["style"] = "background:#f9f9a1";
                    txtisuqty.Attributes["placeholder"] = "use - (minus qty)";
                }

            }
        }

        private void SupplierOverallAdvanced(string pactcode, string csircode)
        {
            string comcod = this.GetCompCode();
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string frmdate = "01" + date.Substring(2);
            string spclcode = "%";
            string _pactcode = "26" + ASTUtility.Right(pactcode, 10);
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGERSUB", _pactcode, frmdate, date, csircode, "", "", "", "", spclcode);

            this.lbtnBalance.Text = "Balance : " + Convert.ToDouble(ds1.Tables[2].Rows[0]["balam"]).ToString("#,##0.00;(#,##0.00);");
            lbtnBalance.NavigateUrl = this.ResolveUrl("~/F_17_Acc/AccLedger.aspx?Type=SubLedger&prjcode=" + _pactcode + "&sircode=" + csircode + "");
            // lbtnBalance.NavigateUrl = "~/F_17_Acc/AccPurchase.aspx?Type=Entry&genno=" + billno + "&ssircode=" + ssircode + "&Date1=" + Date1;

        }

        protected void lnkApproved_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string issuno = this.Request.QueryString["genno"].ToString() == "" ? "" : this.Request.QueryString["genno"].ToString();


            this.SaveValue();
            DataTable tbl2 = (DataTable)ViewState["tblmatissue"];
            string Refno = this.txtRefno.Text.Trim();

            string percentage = Convert.ToDouble("0" + this.txtpercentage.Text.Replace("%", "").Trim()).ToString();
            string sdamt = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim()).ToString();
            string dedamt = Convert.ToDouble("0" + this.txtDedAmount.Text.Trim()).ToString();
            string Penalty = Convert.ToDouble("0" + this.txtPenaltyAmount.Text.Trim()).ToString();
            string advamt = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim()).ToString();
            string Reward = Convert.ToDouble("0" + this.txtreward.Text.Trim()).ToString();

            string mISUDAT = this.txtCurISSDate.Text.Trim();
            string mPACTCODE = this.ddlprjlist.SelectedValue.ToString().Trim();
            string mCONCODE = this.ddlcontractorlist.SelectedValue.ToString().Trim();
            string mISURNAR = this.txtISSNarr.Text.Trim();

            string trade = this.ddltrade.SelectedValue.ToString();
            string rano = this.ddlRA.SelectedValue.ToString();
            string msg = "";

            //bool result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "ISSUEAPPROVED", issuno, mISUDAT, mPACTCODE, mCONCODE, mISURNAR, Refno, usrid, session, trmnid, trade, rano, percentage, sdamt, dedamt, Penalty, advamt, Reward, "", "", "");
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "ISSUEAPPROVED", issuno, usrid, Date, trmnid, session, percentage, sdamt, dedamt, Penalty, advamt, Reward, "", "", "", "");

            if (!result)
            {
                msg = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }

            foreach (DataRow dr in tbl2.Rows)
            {
                string Flrcod = dr["flrcod"].ToString();
                string grp = dr["grp"].ToString();
                string Rsircode = dr["rsircode"].ToString();
                string prcent = Convert.ToDouble(dr["prcent"].ToString().Trim()).ToString();
                double Isuqty = Convert.ToDouble(dr["isuqty"].ToString().Trim());
                double Isuamt = Convert.ToDouble(dr["isuamt"].ToString().Trim());
                string wrkqty = dr["wrkqty"].ToString().Trim();
                double balqty = Convert.ToDouble(dr["balqty"].ToString().Trim());
                string mbbook = dr["mbbook"].ToString().Trim();
                string above = dr["above"].ToString();
                string dedqty = dr["dedqty"].ToString();
                string dedunit = dr["dedunit"].ToString();
                string idedamt = dr["idedamt"].ToString();
                double balamt = Convert.ToDouble(dr["balamt"].ToString().Trim());

                if (balqty >= Isuqty)
                {
                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURLABISSUEINFO", "PURLISSUEA", issuno, Flrcod,
                        Rsircode, prcent, Isuqty.ToString(), Isuamt.ToString(), wrkqty, grp, mbbook, above, dedqty, dedunit, idedamt, "");
                    if (!result)
                    {
                        msg = purData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                        return;
                    }
                }
                else
                {
                    msg = "Not Within the Budget ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Approved Successfully');", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Labour Issue Information";
                string eventdesc = "Update Labour QTY & RATE";
                string eventdesc2 = "Issue No: " + this.lblCurISSNo1.Text.Trim().Substring(0, 3) +
                        ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
    }
}