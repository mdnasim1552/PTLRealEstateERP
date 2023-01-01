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
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
//using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
using System.IO;
using AjaxControlToolkit;
namespace RealERPWEB.F_09_PImp
{
    public partial class PurSubConBillFinal : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        public static int i, j;
        public static string Url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);



                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "BillEntry") ? "Bill Finalization"
                    : (this.Request.QueryString["Type"].ToString() == "FirstRecom") ? "Bill Finalization-First Recommendation"
                    : (this.Request.QueryString["Type"].ToString() == "SecRecom") ? "Bill Finalization-Second Recommendation"
                    : (this.Request.QueryString["Type"].ToString() == "ThirdRecom") ? "Bill Finalization-Third Recommendation"
                    : (this.Request.QueryString["Type"].ToString() == "BillEdit") ? "Bill Finalization-Edit"
                    : (this.Request.QueryString["Type"].ToString() == "BillConfirmed") ? "Bill Finalization-Confirmed" : "Labour Issue Information";

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.IniGridColumnVisible();
                this.GetProjectName();
                this.GetSubContractor();
                this.GetBillType();

                string genno = this.Request.QueryString["genno"].ToString();
                if (genno.Length > 0)
                {

                    if (genno.Substring(0, 3) == "CBL")
                    {
                        this.GetPreBILLlist();
                        this.lbtnOk_Click(null, null);
                    }

                }

            }

        }

        private void Visibility()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3101":
                case "2305":
                case "3305":
                case "3306":
                case "3311":
                case "3310":

                    this.ChkTopSheet.Visible = true;
                    break;

                default:
                    this.ChkTopSheet.Visible = false;
                    break;





            }


        }

        private void IniGridColumnVisible()
        {

            string comcod = this.GetCompCode();



            switch (comcod)
            {
                case "3336":
                case "3337":
                    this.gvSubBill.Columns[11].Visible = false;
                    this.gvSubBill.Columns[13].Visible = false;
                    break;

                case "3339"://Tropical
                    this.gvSubBill.Columns[16].Visible = true;
                    this.gvSubBill.Columns[17].Visible = true;
                    this.gvSubBill.Columns[18].Visible = true;
                    this.gvSubBill.Columns[19].Visible = true;
                    this.gvSubBill.Columns[20].Visible = true;
                    break;


                case "3370"://CPDL
                    this.gvSubBill.Columns[8].Visible = false;
                    this.gvSubBill.Columns[9].Visible = false;                    
                    this.gvSubBill.Columns[10].Visible = true;                    
                    this.gvSubBill.Columns[12].Visible = true;
                    this.gvSubBill.Columns[13].Visible = false;
                    break;

                case "3367":
                case "3101":
                    this.gvSubBill.Columns[8].Visible = true;
                    this.gvSubBill.Columns[9].Visible = true;
                    break;

                default:
                    this.gvSubBill.Columns[8].Visible = false;
                    this.gvSubBill.Columns[9].Visible = false;
                    break;
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

        protected void GetSubConBiFin()
        {

            string comcod = this.GetCompCode();
            string mREQNO = "NEWBILL";
            if (this.ddlPrevBillList.Items.Count > 0)
                mREQNO = this.ddlPrevBillList.SelectedValue.ToString();

            string mREQDAT = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            if (mREQNO == "NEWBILL")
            {
                DataSet ds2 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "LASTCBILLNO", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mREQNO = ds2.Tables[0].Rows[0]["maxbillno"].ToString();
                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds2.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(6, 5);
                    this.ddlPrevBillList.DataTextField = "maxbillno1";
                    this.ddlPrevBillList.DataValueField = "maxbillno";
                    this.ddlPrevBillList.DataSource = ds2.Tables[0];
                    this.ddlPrevBillList.DataBind();
                }
            }
        }

        private void GetBillNo()
        {

            string comcod = this.GetCompCode();
            string date = this.txtCurDate.Text;

            DataSet ds3 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "LASTCBILLNO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(6);
        }

        private void GetProjectName()
        {
            string status = "";
            if (Request.QueryString["status"] != null)
            {
                status = Request.QueryString["status"].ToString();
            }
            string type = Request.QueryString["Type"].ToString();
            string comcod = this.GetCompCode();

            string serch1 = (this.Request.QueryString["prjcode"].ToString()).Length == 0 ? ("%" + this.txtSrcPro.Text.Trim() + "%") : (this.Request.QueryString["prjcode"].ToString() + "%");
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURPROJECTNAME", serch1, status, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.GetSubContractor();
        }

        private void GetSubContractor()
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();

            string serch1 = (this.Request.QueryString["sircode"].ToString()).Length == 0 ? ("%" + this.txtSrcSub.Text.Trim() + "%") : (this.Request.QueryString["sircode"].ToString() + "%");
            // string serch1 = "%" + this.txtSrcSub.Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURSUBNAME", pactcode, serch1, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlSubName.DataTextField = "csirdesc";
            this.ddlSubName.DataValueField = "csircode";
            this.ddlSubName.DataSource = ds1.Tables[0];
            this.ddlSubName.DataBind();
            //ddlRAList_SelectedIndexChanged(null, null);


        }

        private void GetBillType()
        {
            string comcod = this.GetCompCode();
            string serch1 = "%" + this.txtSrcSub.Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETBILLTYPE", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlbilltype.DataTextField = "billtype";
            this.ddlbilltype.DataValueField = "billtcode";
            this.ddlbilltype.DataSource = ds1.Tables[0];
            this.ddlbilltype.DataBind();


        }
        private void GetPreBILLlist()
        {


            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string csircode = this.ddlSubName.SelectedValue.ToString();
            string curdate = this.txtCurDate.Text.Trim();
            // string txtsearch = "%" +this.txtSrcPreBill.Text + "%";
            string txtsearch = (this.Request.QueryString["genno"].ToString()).Length == 0 ? ("%" + this.txtSrcPreBill.Text.Trim() + "%") : (this.Request.QueryString["genno"].ToString() + "%");
            string billconfirmed = (this.Request.QueryString["Type"].ToString() == "BillConfirmed") ? "BillConfirmed" : "";


            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPRECBILLLIST", pactcode, csircode, curdate, txtsearch, billconfirmed, "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlPrevBillList.DataTextField = "billno1";
            this.ddlPrevBillList.DataValueField = "billno";
            this.ddlPrevBillList.DataSource = ds1.Tables[0];
            this.ddlPrevBillList.DataBind();


        }


        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSubContractor();
        }
        protected void ibtnFindSubConName_Click(object sender, EventArgs e)
        {
            this.GetSubContractor();
        }



        protected void ibtnPreBillList_Click(object sender, EventArgs e)
        {

            this.GetPreBILLlist();
        }





        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            if (ChkTopSheet.Checked)
            {
                this.TopSheet();
            }

            else
            {
                this.General();
            }




        }


        //protected void lnkPrint_Click(object sender, EventArgs e)
        //{


        private void General()
        {


            string billno = this.ddlPrevBillList.SelectedValue.ToString();
            string PrintOpt = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            //if(billno==""){
            //    billno = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();
            //}
            // string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_99_Allinterface/";
            string currentptah = this.ResolveUrl("~/F_99_Allinterface/PurchasePrint.aspx?Type=ConBillFinalization&billno=" + billno + "&PrintOpt=" + PrintOpt);
            // string totalpath =  currentptah;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + currentptah + "', target='_blank');</script>";


            //string comcod = this.GetCompCode();




            //if (comcod == "3339")
            // {
            //     this.PrintBillFinalization();
            // }
            // else
            // {
            //     this.Allprint();
            // }

        }


        private void TopSheet()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string billno = this.ddlPrevBillList.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "CONTRACTORTOPSHEET", billno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                return;
            }

            Session["tblconbill"] = ds1.Tables[0];

            DataTable dt1 = (DataTable)Session["tblconbill"];
            DataTable dt = ds1.Tables[1];
            DataTable dt2 = ds1.Tables[2];



            LocalReport Rpt1 = new LocalReport();
            var lst = dt1.DataTableToList<RealEntity.C_09_PIMP.SubConBill.EClassSubConBillFinalTopSheet>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptTopSheet", lst, null, null);



            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "TOP SHEET"));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", "Project Name: " + this.ddlProjectName.SelectedItem.Text.Trim().Substring(17)));
            Rpt1.SetParameters(new ReportParameter("txtSubcon", "Contractor: " + this.ddlSubName.SelectedItem.Text.Trim().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("rnof", "R/A No: " + ds1.Tables[1].Rows[0]["cbillref"].ToString()));
            Rpt1.SetParameters(new ReportParameter("billno", "Bill No: " + this.lblCurNo1.Text.Trim() + this.lblCurNo2.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtdate", "Date: " + this.txtCurDate.Text));
            Rpt1.SetParameters(new ReportParameter("txtbilltype", " Bill Type : " + ds1.Tables[1].Rows[0]["billtdesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtnarration", "Narration: " + this.txtRemarks.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("takainword", "Total Payable Amount(Tk.)= " + Convert.ToDouble(dt2.Rows[0]["tbillamt"]).ToString("#,##0.00;(#,##0.00);") + ASTUtility.Trans((Convert.ToDouble(dt2.Rows[0]["tbillamt"])), 2) + " after necessary checking of account sections."));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





            //ReportDocument rptbillSummary = new RealERPRPT.R_09_PImp.RptTopSheet();
            //TextObject companyname = rptbillSummary.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //companyname.Text = comnam;
            //TextObject txtProjectName = rptbillSummary.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //txtProjectName.Text = "Project Name: " + this.ddlProjectName.SelectedItem.Text.Trim().Substring(17);


            //TextObject txtSubConName = rptbillSummary.ReportDefinition.ReportObjects["txtSubcon"] as TextObject;
            //txtSubConName.Text = "Contractor: " + this.ddlSubName.SelectedItem.Text.Trim().Substring(13);
            //TextObject txtDate = rptbillSummary.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtDate.Text = "Date: " + this.txtCurDate.Text;

            //TextObject rpttxtrefno = rptbillSummary.ReportDefinition.ReportObjects["rnof"] as TextObject;
            //rpttxtrefno.Text = "R/A No: " + ds1.Tables[1].Rows[0]["cbillref"].ToString();

            //TextObject rpttxtbillno = rptbillSummary.ReportDefinition.ReportObjects["billno"] as TextObject;
            //rpttxtbillno.Text = "Bill No: " + this.lblCurNo1.Text.Trim() + this.lblCurNo2.Text.Trim();

            //TextObject txtnarration = rptbillSummary.ReportDefinition.ReportObjects["txtnarration"] as TextObject;
            //txtnarration.Text = "Narration: " + this.txtRemarks.Text.Trim();
            //TextObject txtbilltype = rptbillSummary.ReportDefinition.ReportObjects["txtbilltype"] as TextObject;
            //txtbilltype.Text = "Bill Type : " + ds1.Tables[1].Rows[0]["billtdesc"].ToString(); ;  //ds1.Tables[2].Rows[0]["memono"].ToString();

            //TextObject rpttxtTaka = rptbillSummary.ReportDefinition.ReportObjects["takainword"] as TextObject;
            //rpttxtTaka.Text = "Total Payable Amount(Tk.)= " + Convert.ToDouble(dt2.Rows[0]["tbillamt"]).ToString("#,##0.00;(#,##0.00);") + ASTUtility.Trans((Convert.ToDouble(dt2.Rows[0]["tbillamt"])), 2) + " after necessary checking of account sections.";

            //TextObject txtuserinfo = rptbillSummary.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptbillSummary.SetDataSource(dt1);
            //Session["Report1"] = rptbillSummary;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        private string CompanyBill()
        {
            string comcod = this.GetCompCode();
            string PrintReq = "";
            switch (comcod)
            {
                case "1101":
                    PrintReq = "PrintBill01";
                    break;


                case "3330":// Bridge
                    PrintReq = "PrintBill02";
                    break;

                case "3332": //inster
                    PrintReq = "PrintBill03";
                    break;

                case "3333":// Alliance

                    PrintReq = "PrintBill04";
                    break;



                case "3338"://ACME
                    PrintReq = "PrintBill05";
                    break;

                //case "3336":
                //    this.PrintBillFinalization();
                //    break;

                case "3335":// Edison          
                    PrintReq = "PrintBill06";
                    break;


                case "3336":// Suvastu
                case "3337":
                    PrintReq = "PrintBill07";
                    break;



                case "2305": //Land
                case "3305":// Housing
                case "3306":// Ratul
                case "3307":
                case "3308":
                case "3309":
                case "3311":// Chittagong
                case "3310":// Chittagong

                    PrintReq = "PrintBill08";
                    break;
                
                case "3101":// ptl
                case "3370":// cpdl
                    PrintReq = "PrintBill09";
                    break;


                default:
                    PrintReq = "PrintBill01";
                    break;
            }

            return PrintReq;

        }
        private void Allprint()
        {

            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string mBillNo = this.ddlPrevBillList.SelectedValue.ToString();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETCBILLINFO", mBillNo, "",
                          "", "", "", "", "", "", "");
            Session["tblbill"] = HiddenSameData(ds1.Tables[0]);
            DataTable dt = (DataTable)Session["tblbill"];
            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>();
            LocalReport rptbill = new LocalReport();
            string pCompanyBill = this.CompanyBill();
            string IssueRefNo = (dt.Rows[0]["lisurefno"].ToString().Length > 0) ? "Issue Ref No: " + dt.Rows[0]["lisurefno"].ToString() : "Issue Ref No:";

            if (pCompanyBill == "PrintBill02")
            {
                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillBridge", lst, null, null);
                rptbill.SetParameters(new ReportParameter("txtBilType", this.ddlbilltype.SelectedItem.Text));


                //rptstk = new RealERPRPT.R_09_PImp.RptConBillBridge();
                //TextObject txtBilType = rptstk.ReportDefinition.ReportObjects["txtBilType"] as TextObject;
                //txtBilType.Text = this.ddlbilltype.SelectedItem.Text;
            }


            //else if (pCompanyBill == "PrintBill03")
            //{
            //    rptstk = new RealERPRPT.R_09_PImp.RptConBillInstar();
            //    TextObject txtBilType = rptstk.ReportDefinition.ReportObjects["txtBilType"] as TextObject;
            //    txtBilType.Text = this.ddlbilltype.SelectedItem.Text;
            //}


            else if (pCompanyBill == "PrintBill04")
            {
                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillAlli", lst, null, null);
                // rptstk = new RealERPRPT.R_09_PImp.RptConBillAlli();

            }

            else if (pCompanyBill == "PrintBill05")
            {





                //txtapproveNam
                //txtpreNam
                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillAcme", lst, null, null);
                DataTable dtuser = (DataTable)Session["UserLog"];

                string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postednam"].ToString();
                string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprovnam"].ToString();
                rptbill.SetParameters(new ReportParameter("IssueNo", "Issue No: " + dt.Rows[0]["lisuno2"].ToString()));
                rptbill.SetParameters(new ReportParameter("txtpreNam", dtuser.Rows[0]["postednam"].ToString()));
                rptbill.SetParameters(new ReportParameter("txtapproveNam", dtuser.Rows[0]["aprovnam"].ToString()));
                rptbill.SetParameters(new ReportParameter("IssueRefNo", IssueRefNo));
            }

            else if (pCompanyBill == "PrintBill06")

            {

                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillEdi", lst, null, null);

                rptbill.SetParameters(new ReportParameter("IssueNo", "Issue No: " + dt.Rows[0]["lisuno2"].ToString()));
                rptbill.SetParameters(new ReportParameter("IssueRefNo", IssueRefNo));

                //TextObject rptissueno = rptstk.ReportDefinition.ReportObjects["rptissueno"] as TextObject;
                //rptissueno.Text = "Issue No: " + dt.Rows[0]["lisuno2"].ToString();

                //TextObject rptrefno = rptstk.ReportDefinition.ReportObjects["rptrefno"] as TextObject;
                //rptrefno.Text = (dt.Rows[0]["lisurefno"].ToString().Length > 0) ? "Issue Ref No: " + dt.Rows[0]["lisurefno"].ToString() : "Issue Ref No:";


            }

            else if (pCompanyBill == "PrintBill07")
            {


                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillSuvastu", lst, null, null);

                rptbill.SetParameters(new ReportParameter("IssueNo", "Issue No: " + dt.Rows[0]["lisuno2"].ToString()));
                rptbill.SetParameters(new ReportParameter("IssueRefNo", IssueRefNo));

                rptbill.SetParameters(new ReportParameter("txtreward", this.txtreward.Text.Trim()));



                //rptstk = new RealERPRPT.R_09_PImp.RptConBillSuvastu();

                //TextObject rptissueno = rptstk.ReportDefinition.ReportObjects["rptissueno"] as TextObject;
                //rptissueno.Text = "Issue No: " + dt.Rows[0]["lisuno2"].ToString();

                //TextObject rptrefno = rptstk.ReportDefinition.ReportObjects["rptrefno"] as TextObject;
                //rptrefno.Text = (dt.Rows[0]["lisurefno"].ToString().Length > 0) ? "Issue Ref No: " + dt.Rows[0]["lisurefno"].ToString() : "Issue Ref No:";
                //TextObject txtreward = rptstk.ReportDefinition.ReportObjects["txtreward"] as TextObject;
                //txtreward.Text = this.txtreward.Text.Trim();


            }

            else if (pCompanyBill == "PrintBill08")
            {
                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillRup", lst, null, null);


            }
            else if (pCompanyBill == "PrintBill09")
            {
                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBillCPDL", lst, null, null);


            }


            else
            {


                rptbill = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptConBill", lst, null, null);

                rptbill.SetParameters(new ReportParameter("IssueNo", "Issue No: " + dt.Rows[0]["lisuno2"].ToString()));
                rptbill.SetParameters(new ReportParameter("IssueRefNo", IssueRefNo));

                //rptstk = new RealERPRPT.R_09_PImp.RptConBill();

                //TextObject rptissueno = rptstk.ReportDefinition.ReportObjects["rptissueno"] as TextObject;
                //rptissueno.Text = "Issue No: " + dt.Rows[0]["lisuno2"].ToString();

                //TextObject rptrefno = rptstk.ReportDefinition.ReportObjects["rptrefno"] as TextObject;
                //rptrefno.Text = (dt.Rows[0]["lisurefno"].ToString().Length > 0) ? "Issue Ref No: " + dt.Rows[0]["lisurefno"].ToString() : "Issue Ref No:";


            }














            // var TAmt = lst.Select(p => p.billamt).Sum();
            double TAmt = Convert.ToDouble("0" + ((Label)this.gvSubBill.FooterRow.FindControl("lgvFBillAmt")).Text) - Convert.ToDouble("0" + this.txtSDAmount.Text.Trim()) - Convert.ToDouble("0" + this.txtDedAmount.Text.Trim()) - Convert.ToDouble("0" + this.txtPenaltyAmount.Text.Trim()) - Convert.ToDouble("0" + this.txtAdvanced.Text.Trim());

            rptbill.EnableExternalImages = true;
            rptbill.SetParameters(new ReportParameter("compname", comnam));
            rptbill.SetParameters(new ReportParameter("comadd", comadd));
            rptbill.SetParameters(new ReportParameter("Rptname", "Sub-Contractor Bill"));
            rptbill.SetParameters(new ReportParameter("ProjectName", this.ddlProjectName.SelectedItem.Text.Trim().Substring(13)));
            rptbill.SetParameters(new ReportParameter("SubContNam", "Contractor Name: " + this.ddlSubName.SelectedItem.Text.Trim().Substring(13)));
            rptbill.SetParameters(new ReportParameter("mBillNo", "Bill No: " + this.lblCurNo1.Text.Trim() + this.lblCurNo2.Text.Trim()));
            rptbill.SetParameters(new ReportParameter("Date", "Date: " + this.txtCurDate.Text.Trim()));
            rptbill.SetParameters(new ReportParameter("SeDep", this.txtSDAmount.Text.Trim()));
            rptbill.SetParameters(new ReportParameter("DedAmt", this.txtDedAmount.Text.Trim()));
            rptbill.SetParameters(new ReportParameter("PenaltyAmt", this.txtPenaltyAmount.Text.Trim()));
            rptbill.SetParameters(new ReportParameter("Advanced", this.txtAdvanced.Text.Trim()));
            rptbill.SetParameters(new ReportParameter("TotalAmt", Math.Round(TAmt, 0).ToString("#,##0.00;(#,##0.00); ")));
            rptbill.SetParameters(new ReportParameter("BillRef", "Bill Ref.No: " + this.txtCBillRefNo.Text.Trim()));
            rptbill.SetParameters(new ReportParameter("naration", this.txtRemarks.Text.Trim()));
            rptbill.SetParameters(new ReportParameter("printFooter", txtuserinfo));
            rptbill.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //  Rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(TAmt), 2)));
            Session["Report1"] = rptbill;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




            //TextObject rpttxtComName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rpttxtComName.Text = comnam;
            //TextObject txtProjectName = rptstk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //txtProjectName.Text = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            //TextObject txtContractor = rptstk.ReportDefinition.ReportObjects["txtContractor"] as TextObject;
            //txtContractor.Text = "Contractor Name : " + this.ddlSubName.SelectedItem.Text.Trim().Substring(13);
            //TextObject rpttxtbillno = rptstk.ReportDefinition.ReportObjects["billno"] as TextObject;
            //rpttxtbillno.Text = "Bill No: " + this.lblCurNo1.Text.Trim() + this.lblCurNo2.Text.Trim();
            //TextObject rpttxtbillRefno = rptstk.ReportDefinition.ReportObjects["billRefno"] as TextObject;
            //rpttxtbillRefno.Text = (this.txtCBillRefNo.Text.Trim().Length > 0) ? "Bill Ref. No: " + this.txtCBillRefNo.Text.Trim() : "";

            //double seqpercnt = Convert.ToDouble("0" + this.txtpercentage.Text.Trim().Replace("%", ""));
            //TextObject txtsecurity = rptstk.ReportDefinition.ReportObjects["txtsecurity"] as TextObject;
            //txtsecurity.Text = (seqpercnt > 0 ? ("Security Deposit (" + this.txtpercentage.Text.Trim() + ")") : "Security Deposit");



            //TextObject rpttxtSequirityAmt = rptstk.ReportDefinition.ReportObjects["txtSecurityAmt"] as TextObject;
            //rpttxtSequirityAmt.Text = this.txtSDAmount.Text.Trim();

            //TextObject textdedution = rptstk.ReportDefinition.ReportObjects["textdedution"] as TextObject;
            //textdedution.Text = this.txtDedAmount.Text.Trim();
            //TextObject txtpanalty = rptstk.ReportDefinition.ReportObjects["txtpanalty"] as TextObject;
            //txtpanalty.Text = this.txtPenaltyAmount.Text.Trim();
            //TextObject txtAdvanced = rptstk.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
            //txtAdvanced.Text = this.txtAdvanced.Text.Trim();



            //double netamt = Convert.ToDouble("0" + ((Label)this.gvSubBill.FooterRow.FindControl("lgvFBillAmt")).Text) + Convert.ToDouble("0" + this.txtreward.Text.Trim()) - Convert.ToDouble("0" + this.txtSDAmount.Text.Trim()) - Convert.ToDouble("0" + this.txtDedAmount.Text.Trim()) - Convert.ToDouble("0" + this.txtPenaltyAmount.Text.Trim()) - Convert.ToDouble("0" + this.txtAdvanced.Text.Trim());
            //TextObject rpttxttoamtafSecurity = rptstk.ReportDefinition.ReportObjects["txttoamtafSecurity"] as TextObject;
            //rpttxttoamtafSecurity.Text = Math.Round(netamt, 0).ToString("#,##0.00;(#,##0.00); ");
            //TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //rpttxtdate.Text = "Date: " + this.txtCurDate.Text.Trim();


            //TextObject txtremarks = rptstk.ReportDefinition.ReportObjects["txtremarks"] as TextObject;
            //txtremarks.Text = this.txtRemarks.Text.Trim();

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)Session["tblbill"]);


            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "SUB-CONTRACTOR BILL";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "Bill No: " + this.lblCurNo1.Text.Trim() + this.lblCurNo2.Text.Trim();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}


            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);


            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintBillFinalization()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string mBillNo = this.ddlPrevBillList.SelectedValue.ToString();
            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tblbill"];
            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.EClassOrder.BillFinalization>();
            // var TAmt = lst.Select(p => p.billamt).Sum();
            double TAmt = Convert.ToDouble("0" + ((Label)this.gvSubBill.FooterRow.FindControl("lgvFBillAmt")).Text) - Convert.ToDouble("0" + this.txtSDAmount.Text.Trim()) - Convert.ToDouble("0" + this.txtDedAmount.Text.Trim()) - Convert.ToDouble("0" + this.txtPenaltyAmount.Text.Trim()) - Convert.ToDouble("0" + this.txtAdvanced.Text.Trim());
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptSubConBillFinalization", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Rptname", "Sub-Contractor Bill"));
            Rpt1.SetParameters(new ReportParameter("ProjectName", this.ddlProjectName.SelectedItem.Text.Trim().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("SubContNam", "Contractor Name: " + this.ddlSubName.SelectedItem.Text.Trim().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("IssueNo", "Issue No: " + dt.Rows[0]["lisuno2"].ToString()));
            Rpt1.SetParameters(new ReportParameter("IssueRefNo", "Issue Ref No: " + dt.Rows[0]["lisurefno"].ToString()));
            Rpt1.SetParameters(new ReportParameter("mBillNo", "Bill No: " + mBillNo));
            Rpt1.SetParameters(new ReportParameter("Date", "Date: " + this.txtCurDate.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("SeDep", this.txtSDAmount.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("DedAmt", this.txtDedAmount.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("PenaltyAmt", this.txtPenaltyAmount.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("Advanced", this.txtAdvanced.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("TotalAmt", Math.Round(TAmt, 0).ToString("#,##0.00;(#,##0.00); ")));
            Rpt1.SetParameters(new ReportParameter("BillRef", "Bill Ref.No: " + this.txtCBillRefNo.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("naration", this.txtRemarks.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("printFooter", txtuserinfo));
            //  Rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(TAmt), 2)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";

                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
                this.lblSubDesc.Text = this.ddlSubName.SelectedItem.Text.Trim().Substring(13);
                this.ddlProjectName.Visible = false;
                this.ddlSubName.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.lblSubDesc.Visible = true;
                this.PnlProRemarks.Visible = true;
                this.lblPreList.Visible = false;
                this.txtSrcPreBill.Visible = false;
                this.ibtnPreBillList.Visible = false;
                this.ddlPrevBillList.Visible = false;
                this.ddlpagesize.Visible = true;
                this.lblPage.Visible = true;
                this.txtCurDate.Enabled = true;
                if (ddlPrevBillList.Items.Count == 0)
                {
                    this.panel11.Visible = true;
                }
                //this.lblmsg.Text = "";
                // this.GetSubConBiFin();
                this.pnlAttached.Visible = true;

                this.ShowCBillInfo();
                createTable();
                this.Visibility();
                return;
            }
            this.ClearSecurity();
            this.lbtnOk.Text = "Ok";
            this.panel11.Visible = false;
            this.lblProjectdesc.Text = "";
            this.lblSubDesc.Text = "";
            this.txtRemarks.Text = "";
            this.lblvalnettotal.Text = "";

            this.txtCBillRefNo.Text = "";
            this.lblCurNo1.Text = "";
            this.lblCurNo2.Text = "";
            this.lblVounum.Text = "";
            //this.txtpercentage.Text ="";
            this.txtSDAmount.Text = "";
            this.txtDedAmount.Text = "";
            this.txtPenaltyAmount.Text = "";
            this.txtAdvanced.Text = "";

            this.ddlPrevBillList.Items.Clear();
            this.ddlProjectName.Visible = true;
            this.ddlSubName.Visible = true;
            this.lblProjectdesc.Visible = false;
            this.lblSubDesc.Visible = false;
            this.lblPreList.Visible = true;
            this.txtSrcPreBill.Visible = true;
            this.ibtnPreBillList.Visible = true;
            this.ddlPrevBillList.Visible = true;
            this.PnlProRemarks.Visible = false;
            this.gvSubBill.DataSource = null;
            this.ddlpagesize.Visible = false;
            this.ChkTopSheet.Visible = false;
            this.lblPage.Visible = false;
            this.txtCurDate.Enabled = true;
            this.gvSubBill.DataBind();
            this.ddlRAList.Items.Clear();

            this.pnlAttached.Visible = false;
            Session.Remove("tblAttDocs");
            ListViewEmpAll.DataSource = null;
            ListViewEmpAll.DataBind();
            ListViewEmpAll.Items.Clear();
        }

        private void ShowCBillInfo()
        {
            string comcod = this.GetCompCode();


            Session.Remove("tblbill");

            string CurDate1 = this.txtCurDate.Text.Trim();
            string mBillNo = "NEWBILL";
            if (this.ddlPrevBillList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mBillNo = this.ddlPrevBillList.SelectedValue.ToString();
            }
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETCBILLINFO", mBillNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblbill"] = HiddenSameData(ds1.Tables[0]);

            Session["UserLog"] = ds1.Tables[1];

            if (mBillNo == "NEWBILL")
            {
                ImgbtnFindRes_Click(null, null);


                DataSet ds2 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "LASTCBILLNO", CurDate1,
                    "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds2.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(6, 5);

                }
                return;
            }


            if (mBillNo != "NEWBILL")
            {
                this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["billno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["billno1"].ToString().Substring(6, 5);
                this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["billdate"]).ToString("dd-MMM-yyyy");
                this.ddlProjectName.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
                this.ddlProjectName_SelectedIndexChanged(null, null);
                this.ddlSubName.SelectedValue = ds1.Tables[1].Rows[0]["csircode"].ToString();
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
                this.lblSubDesc.Text = this.ddlSubName.SelectedItem.Text.Trim().Substring(13);
                this.txtCBillRefNo.Text = ds1.Tables[1].Rows[0]["cbillref"].ToString();
                this.txtpercentage.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["percntge"]).ToString("#,##0.00;(#,##0.00); ") + "%";
                this.txtSDAmount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["sdamt"]).ToString("#,##0.00;(#,##0.00); ");
                this.txtDedAmount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["dedamt"]).ToString("#,##0.00;(#,##0.00); ");
                this.txtPenaltyAmount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["penamt"]).ToString("#,##0.00;(#,##0.00); ");
                this.txtAdvanced.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); ");
                this.txtreward.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["reward"]).ToString("#,##0.00;(#,##0.00); ");
                this.txtadvpay.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["advpay"]).ToString("#,##0.00;(#,##0.00); ");
                this.ddlbilltype.SelectedValue = ds1.Tables[1].Rows[0]["billtcode"].ToString();

                this.txtRemarks.Text = ds1.Tables[1].Rows[0]["rmrks"].ToString();
                this.lblVounum.Text = ds1.Tables[1].Rows[0]["vounum"].ToString();
                this.Data_DataBind();

            }

            this.btnShowimg_Click(null, null);


        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string lisuno = dt1.Rows[0]["lisuno"].ToString();
            //   string lisurefno = dt1.Rows[0]["lisurefno"].ToString();
            string grp = dt1.Rows[0]["grp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["lisuno"].ToString() == lisuno && dt1.Rows[j]["grp"].ToString() == grp)
                {


                    dt1.Rows[j]["lisuno2"] = "";
                    dt1.Rows[j]["lisurefno"] = "";
                    dt1.Rows[j]["grpdesc"] = "";

                }
                else
                {

                    if (dt1.Rows[j]["lisuno"].ToString() == lisuno)
                    {
                        dt1.Rows[j]["lisuno2"] = "";
                        dt1.Rows[j]["lisurefno"] = "";

                    }
                    if (dt1.Rows[j]["grp"].ToString() == grp)
                    {
                        dt1.Rows[j]["grpdesc"] = "";

                    }
                }


                lisuno = dt1.Rows[j]["lisuno"].ToString();
                grp = dt1.Rows[j]["grp"].ToString();



            }

            return dt1;
        }


        private void GetNarration()
        {
            string comcod = this.GetCompCode();

            string ddlvalue = this.ddlRAList.SelectedValue.ToString();
            DataSet ds4 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETNARRATION", ddlvalue, "", "", "", "", "", "", "", "");
            if (ds4.Tables[0].Rows.Count == 0)
                this.txtRemarks.Text = "";
            else

                if (comcod == "3305" || comcod == "2305" || comcod == "3306" || comcod == "3310" || comcod == "3311")
            {
                this.txtRemarks.Text = "";
            }

            else
            {
                this.txtRemarks.Text = ds4.Tables[0].Rows[0]["narration"].ToString();
            }




        }
        private void Data_DataBind()
        {
            this.gvSubBill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSubBill.DataSource = (DataTable)Session["tblbill"];
            this.gvSubBill.DataBind();
            if (this.ddlPrevBillList.Items.Count == 0)
            {
                this.GetNarration();
            }

            ((LinkButton)this.gvSubBill.FooterRow.FindControl("lbtnUpdate")).Visible = (this.lblVounum.Text.Trim() == "00000000000000" || this.lblVounum.Text.Trim() == "") && (this.Request.QueryString["Type"].ToString().Trim() == "BillEntry" || this.Request.QueryString["Type"].ToString().Trim() == "BillServiceEntry" || this.Request.QueryString["Type"].ToString().Trim() == "BillEdit" || this.Request.QueryString["Type"].ToString().Trim() == "FirstRecom" || this.Request.QueryString["Type"].ToString().Trim() == "SecRecom" || this.Request.QueryString["Type"].ToString().Trim() == "ThirdRecom");
            ((LinkButton)this.gvSubBill.FooterRow.FindControl("lbtnDeleteBill")).Visible = (this.Request.QueryString["Type"].ToString().Trim() == "BillEdit" && this.lblVounum.Text.Trim() == "00000000000000");
            ((LinkButton)this.gvSubBill.FooterRow.FindControl("lbtnConfirmed")).Visible = (this.Request.QueryString["Type"].ToString().Trim() == "BillConfirmed" && this.lblVounum.Text.Trim() == "00000000000000");



            if (((DataTable)Session["tblbill"]).Rows.Count == 0)
                return;

            DataTable dt = (DataTable)Session["tblbill"];

            ((Label)this.gvSubBill.FooterRow.FindControl("lblgvFamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amount)", "")) ? 0.00 : dt.Compute("Sum(amount)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lblFidedamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(idedamt)", "")) ? 0.00 : dt.Compute("Sum(idedamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvSubBill.FooterRow.FindControl("lblgvFadedamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(adedamt)", "")) ? 0.00 : dt.Compute("Sum(adedamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFBillAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ? 0.00 : dt.Compute("Sum(billamt)", ""))).ToString("#,##0.0000;(#,##0.0000); ");




            double billamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ? 0.00 : dt.Compute("sum(billamt)", "")));
            double security = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim());
            double deduction = Convert.ToDouble("0" + this.txtDedAmount.Text.Trim());
            double penalty = Convert.ToDouble("0" + this.txtPenaltyAmount.Text.Trim());
            double Advanced = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim());
            double Reward = Convert.ToDouble("0" + this.txtreward.Text.Trim());

            this.lblvalnettotal.Text = (billamt + Reward - (security + deduction + penalty + Advanced)).ToString("#,##0;(#,##0); ");
        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_DataBind();

        }
        protected void gvSubBill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSubBill.PageIndex = e.NewPageIndex;
            this.Data_DataBind();
        }
        protected void lbtnShow_Click(object sender, EventArgs e)
        {

        }
        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tblbill"];
            int TblRowIndex;
            for (int i = 0; i < this.gvSubBill.Rows.Count; i++)
            {
                double conrate = ASTUtility.StrPosOrNagative(((TextBox)this.gvSubBill.Rows[i].FindControl("lgvSubRate")).Text.Trim());

                double conqty = ASTUtility.StrPosOrNagative(((TextBox)this.gvSubBill.Rows[i].FindControl("txtgvconqty")).Text.Trim());
                double billamt = ASTUtility.StrPosOrNagative(((TextBox)this.gvSubBill.Rows[i].FindControl("txtgvamt")).Text.Trim());
                TblRowIndex = (gvSubBill.PageIndex) * gvSubBill.PageSize + i;


                //if (Request.QueryString["status"] != null)
                //{
                //    if (Request.QueryString["status"].ToString() == "S")
                //    {

                //        dt.Rows[TblRowIndex]["conrate"] = conrate;
                //        dt.Rows[TblRowIndex]["billamt"] = conrate * conqty;
                //    }
                //}
                //else
                //{

                    dt.Rows[TblRowIndex]["conqty"] = conqty;
                    dt.Rows[TblRowIndex]["billamt"] = conqty>0? conqty*conrate: billamt;
                dt.Rows[TblRowIndex]["conrate"] = conqty == 0 ? 0.00 : billamt / conqty;
               // }


            }
            Session["tblbill"] = dt;

        }
        //protected void lbtnTotal_Click(object sender, EventArgs e) 
        //{
        //    this.SaveValue();
        //    this.Data_DataBind();

        //}
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_DataBind();
        }


        private void CreateDataTable()
        {

            ViewState.Remove("tblapproval");
            DataTable tblt01 = new DataTable();



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
                case "BillEntry":
                case "BillServiceEntry":
                    switch (comcod)
                    {

                        case "1103":
                        case "3368": // finlay

                            break;

                        default:
                            if (approval == "")
                            {
                                this.CreateDataTable();
                                DataTable dt = (DataTable)ViewState["tblapproval"];
                                DataRow dr1 = dt.NewRow();

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
                                dt.Rows.Add(dr1);

                                ds1.Merge(dt);
                                ds1.Tables[0].TableName = "tbl1";
                                approval = ds1.GetXml();
                            }
                            break;
                    }
                    break;


                case "FirstRecom":
                    if (approval == "")
                    {

                        if (comcod == "3368")
                        {
                            this.CreateDataTable();
                            DataTable dt = (DataTable)ViewState["tblapproval"];
                            DataRow dr1 = dt.NewRow();

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

                            dt.Rows.Add(dr1);
                            ds1.Merge(dt);
                            ds1.Tables[0].TableName = "tbl1";
                            approval = ds1.GetXml();

                        }

                        else
                        {
                            this.CreateDataTable();
                            DataTable dt = (DataTable)ViewState["tblapproval"];
                            DataRow dr1 = dt.NewRow();

                            dr1["frecid"] = usrid;
                            dr1["frecdat"] = Date;
                            dr1["frectrmid"] = trmnid;
                            dr1["frecseson"] = session;
                            dr1["secrecid"] = "";
                            dr1["secrecdat"] = "";
                            dr1["secrectrmid"] = "";
                            dr1["secrecseson"] = "";
                            dr1["threcid"] = "";
                            dr1["threcdat"] = "";
                            dr1["threctrmid"] = "";
                            dr1["threcseson"] = "";

                            dt.Rows.Add(dr1);
                            ds1.Merge(dt);
                            ds1.Tables[0].TableName = "tbl1";
                            approval = ds1.GetXml();
                        }

                    }

                    else
                    {

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
                        approval = ds1.GetXml();

                    }
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

            }


            return approval;

        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            // DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            try
            {
                this.SaveDeposit();
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                //string usrid = hst["usrid"].ToString();
                //string Sessionid = hst["session"].ToString();
                //string trmid = hst["compname"].ToString();
                //string PostedByid = (this.Request.QueryString["type"] == "Entry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
                //string Posttrmid = (this.Request.QueryString["type"] == "Entry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
                //string PostSession = (this.Request.QueryString["type"] == "Entry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
                //string Posteddat = (this.Request.QueryString["type"] == "Entry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;

                Hashtable hst = (Hashtable)Session["tblLogin"];

                DataTable dtuser = (DataTable)Session["UserLog"];
                string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
                string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
                string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
                string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

                string tblEditedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editbyid"].ToString();
                string tblEditedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["edittrmid"].ToString();
                string tblEditedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editseson"].ToString();
                string tblEditeddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["editdat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");




                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string PostedByid = (this.Request.QueryString["type"] == "BillEntry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
                string Posttrmid = (this.Request.QueryString["type"] == "BillEntry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
                string PostSession = (this.Request.QueryString["type"] == "BillEntry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
                string Posteddat = (this.Request.QueryString["type"] == "BillEntry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;

                string EditByid = (this.Request.QueryString["type"] == "BillEntry") ? tblEditedByid : (this.Request.QueryString["type"] == "BillEdit") ? userid : tblEditedByid;
                string Edittrmid = (this.Request.QueryString["type"] == "BillEntry") ? tblEditedtrmid : (this.Request.QueryString["type"] == "BillEdit") ? Terminal : tblEditedtrmid;
                string EditSession = (this.Request.QueryString["type"] == "BillEntry") ? tblEditedSession : (this.Request.QueryString["type"] == "BillEdit") ? Sessionid : tblEditedSession;
                string Editdat = (this.Request.QueryString["type"] == "BillEntry") ? tblEditeddat : (this.Request.QueryString["type"] == "BillEdit") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblEditeddat;

                // BillEdit



                DataTable dt = (DataTable)Session["tblbill"];

                if (this.txtCBillRefNo.Text.Trim() == "")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Please Fillup Ref. No";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }


                DataRow[] dr = dt.Select("billamt>0");
                if (dr.Length == 0)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Please Check Total Amount";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                //Existing   Lisue No  
                if (this.Request.QueryString["Type"] == "BillEntry")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string isuno = dt.Rows[i]["lisuno"].ToString();
                        DataSet ds4;
                        if (i == 0)
                            ds4 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "EXISTINGLISUNO", isuno, "", "", "", "", "", "", "", "");

                        else if ((dt.Rows[i - 1]["lisuno"].ToString().Trim()) == isuno)
                            continue;

                        else
                            ds4 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "EXISTINGLISUNO", isuno, "", "", "", "", "", "", "", "");


                        if (ds4.Tables[0].Rows[0]["billno"].ToString() != "00000000000000")
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Issue  No already Existing in Bill";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }

                    }
                }



                if (ddlPrevBillList.Items.Count == 0)
                    this.GetSubConBiFin();




                string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string billno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string csircode = this.ddlSubName.SelectedValue.ToString();
                string Remarks = this.txtRemarks.Text.Trim();
                string cbillref = this.txtCBillRefNo.Text.Trim();





                string percentage = Convert.ToDouble("0" + this.txtpercentage.Text.Replace("%", "").Trim()).ToString();
                string sdamt = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim()).ToString();
                string dedamt = Convert.ToDouble("0" + this.txtDedAmount.Text.Trim()).ToString();
                string Penalty = Convert.ToDouble("0" + this.txtPenaltyAmount.Text.Trim()).ToString();
                string advamt = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim()).ToString();
                string Reward = Convert.ToDouble("0" + this.txtreward.Text.Trim()).ToString();
                string advpay = Convert.ToDouble("0" + this.txtadvpay.Text.Trim()).ToString();
                string billtype = this.ddlbilltype.SelectedValue.ToString();
                bool result;

                string appxml = dt.Rows[0]["approval"].ToString();
                string Approval = this.GetReqApproval(appxml);
                string type = Request.QueryString["Type"].ToString();


                result = PurData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_PURCHASE_02", "INSORUPDATECBILL", "PURCBILLB", billno, pactcode, csircode, curdate,
                      Remarks, cbillref, percentage, sdamt, dedamt, Penalty, advamt, billtype, Reward, PostedByid, Posteddat, PostSession, Posttrmid, EditByid, Editdat, EditSession, Edittrmid, Approval, advpay,"","","","","","","","","");

                if (type == "BillServiceEntry")
                {
                    result = PurData.UpdateTransInfo(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "UPDATEPURCBILLB", billno);
                }

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + PurData.ErrorObject["Msg"];
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }




                DataTable dttemp = (DataTable)Session["tblAttDocs"];

                DataSet ds1 = new DataSet("ds1");
                DataView dv1 = new DataView(dttemp);
                ds1.Tables.Add(dv1.ToTable());
                ds1.Tables[0].TableName = "tbl1";

                bool resulta = PurData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "BILLFINALXMLATTACHEDDOCUS", ds1, null, null, billno);

                if (!resulta)
                {
                    //return;
                }
                else
                {
                    this.btnShowimg_Click(null, null);
                }






                //----------
                string isuno2 = "XXXXXXXXXXXXXX";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string isuno = dt.Rows[i]["lisuno"].ToString();
                    double conqty = Convert.ToDouble(dt.Rows[i]["conqty"].ToString());
                    string flrcod = dt.Rows[i]["flrcod"].ToString();
                    string grp = dt.Rows[i]["grp"].ToString();
                    string rsircode = dt.Rows[i]["rsircode"].ToString();
                    //double billqty = Convert.ToDouble(dt.Rows[i]["billqty"]);
                    string billamt = dt.Rows[i]["billamt"].ToString();
                    string above = dt.Rows[i]["above"].ToString().Trim();
                    string dedqty = dt.Rows[i]["dedqty"].ToString();
                    string dedunit = dt.Rows[i]["dedunit"].ToString();
                    string idedamt = dt.Rows[i]["idedamt"].ToString();


                    //if (conqty != 0)
                    //{
                    result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "INSORUPDATECBILL", "PURCBILLA", billno, flrcod, rsircode, conqty.ToString(),
                        billamt, isuno, grp, above, dedqty, dedunit, idedamt, "", "", "");
                    if (!result)
                        return;
                    // }
                    //-----------PurissueB-------------

                    if (isuno2 != isuno)
                    {
                        result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "INSORUPDATECBILL", "PURLISSUEB", billno, isuno, "", "",
                         "", "", "", "", "", "", "", "", "", "");
                        if (!result)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + PurData.ErrorObject["Msg"];
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;

                        }
                        isuno2 = isuno;
                    }




                }


                if (this.Request.QueryString["Type"] == "BillEntry")
                {

                    //Finalization Approved

                    switch (comcod)
                    {

                        case "3338": //ACME
                        case "1206": //ACME
                        case "1207": //ACME
                        case "3369": //ACME
                        case "3340": //Urban
                        case "1103": //Tanvir
                        case "2305":

                        case "3305"://Rupayan
                        case "3306":
                        case "3311":
                        case "3310":

                        case "3348": //Credence
                        case "3370": //cpdl                    
                        case "1205": //P2P                    
                        case "3351":
                        case "3352":
                        case "3353"://Manama realds

                        case "3368"://Finlay
                        case "3367"://epic
                        case "3366": //Lanco
                        case "3101": //ptl

                            break;

                        default:

                            //  Hashtable hst = (Hashtable)Session["tblLogin"];

                            string usrid = hst["usrid"].ToString();
                            string sessionid = hst["session"].ToString();
                            string trmid = hst["compname"].ToString();
                            string confirmdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                            result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "CONFIRMEDBILFINAL", billno, usrid, sessionid, trmid,
                                  confirmdate, "", "", "", "", "", "", "", "", "", "");

                            if (!result)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + PurData.ErrorObject["Msg"];
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;
                            }
                            break;

                    }
                }
             ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "SUB-CONTRACTOR BILL";
                    string eventdesc = "Update Con Bill";
                    string eventdesc2 = "Bill No: " + billno;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }


        }



        protected void lbtnConfirmed_Click(object sender, EventArgs e)
        {

            this.lbtnUpdate_Click(null, null);
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string confirmdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string billno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();

            bool result;

            result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "CONFIRMEDBILFINAL", billno, usrid, sessionid, trmid,
                  confirmdate, "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + PurData.ErrorObject["Msg"];
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;

            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Bill Confirmed Successfully.";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {
            Session.Remove("tblISuBill");

            string comcod = this.GetCompCode();
            string mProject = this.ddlProjectName.SelectedValue.ToString();
            string mSupCode = this.ddlSubName.SelectedValue.ToString();
            //string mOrderNo = this.ddlOrderList.SelectedValue.ToString();
            string mSrchTxt = "%" + this.txtResSearch.Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "SHOWCONISSUEINFO", mProject, mSupCode, mSrchTxt, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblISuBill"] = ds1.Tables[0];
            Session["tblSecuDed"] = ds1.Tables[1];

            this.ddlRAList.DataTextField = "lisuno1";
            this.ddlRAList.DataValueField = "lisuno";
            this.ddlRAList.DataSource = ds1.Tables[1];
            this.ddlRAList.DataBind();
            string genno = this.Request.QueryString["genno"].ToString();
            if (genno.Length > 0)
            {
                this.ddlRAList.SelectedValue = genno;
            }
        }



        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string issueno = this.ddlRAList.SelectedValue.ToString();
            DataTable tbl1 = (DataTable)Session["tblbill"];
            DataTable dt1 = (DataTable)Session["tblISuBill"];
            DataRow[] dr2 = tbl1.Select("lisuno='" + issueno + "'");
            if (dr2.Length == 0)
            {
                DataTable dt2 = new DataTable();
                DataView dv = dt1.DefaultView;
                dv.RowFilter = ("lisuno='" + issueno + "'");
                dt2 = dv.ToTable();

                for (int i = 0; i < dt2.Rows.Count; i++)
                {

                    DataRow dr1 = tbl1.NewRow();
                    dr1["flrcod"] = dt2.Rows[i]["flrcod"].ToString();
                    dr1["flrdes"] = dt2.Rows[i]["flrdes"].ToString();
                    dr1["lisuno"] = dt2.Rows[i]["lisuno"].ToString();
                    dr1["lisuno2"] = dt2.Rows[i]["lisuno2"].ToString();
                    dr1["lisurefno"] = dt2.Rows[i]["lisurefno"].ToString();
                    dr1["grp"] = dt2.Rows[i]["grp"].ToString();
                    dr1["grpdesc"] = dt2.Rows[i]["grpdesc"].ToString();
                    dr1["rsircode"] = dt2.Rows[i]["rsircode"].ToString();
                    dr1["rsirdesc"] = dt2.Rows[i]["rsirdesc"].ToString();
                    dr1["rsirunit"] = dt2.Rows[i]["rsirunit"].ToString();
                    dr1["billqty"] = dt2.Rows[i]["conqty"].ToString();
                    dr1["conqty"] = dt2.Rows[i]["conqty"].ToString();
                    dr1["conrate"] = dt2.Rows[i]["conrate"].ToString();
                    dr1["billamt"] = dt2.Rows[i]["billamt"].ToString();
                    dr1["peronbgd"] = dt2.Rows[i]["peronbgd"].ToString();

                    dr1["bgdqty"] = dt2.Rows[i]["bgdqty"].ToString();
                    dr1["balqty"] = dt2.Rows[i]["balqty"].ToString();
                    dr1["bgdrat"] = dt2.Rows[i]["bgdrat"].ToString();
                    dr1["above"] = dt2.Rows[i]["above"].ToString();
                    dr1["amount"] = dt2.Rows[i]["amount"].ToString();
                    dr1["dedqty"] = dt2.Rows[i]["dedqty"].ToString();
                    dr1["dedunit"] = dt2.Rows[i]["dedunit"].ToString();
                    dr1["dedrate"] = dt2.Rows[i]["dedrate"].ToString();
                    dr1["idedamt"] = dt2.Rows[i]["idedamt"].ToString();
                    dr1["adedamt"] = dt2.Rows[i]["adedamt"].ToString();
                    dr1["wrkqty"] = dt2.Rows[i]["wrkqty"].ToString();
                    dr1["prcent"] = dt2.Rows[i]["prcent"].ToString();
                    //dr1["rsircode"] = mResCode;

                    tbl1.Rows.Add(dr1);
                }
            }
            Session["tblbill"] = HiddenSameData(tbl1);
            DataTable dt = (DataTable)Session["tblSecuDed"];
            DataRow[] dr3 = dt.Select("lisuno='" + issueno + "'");

            this.txtpercentage.Text = Convert.ToDouble(dr3[0]["percntge"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            this.txtSDAmount.Text = Convert.ToDouble(dr3[0]["sdamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtDedAmount.Text = Convert.ToDouble(dr3[0]["dedamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtPenaltyAmount.Text = Convert.ToDouble(dr3[0]["penamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtAdvanced.Text = Convert.ToDouble(dr3[0]["advamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtreward.Text = Convert.ToDouble(dr3[0]["reward"]).ToString("#,##0.00;(#,##0.00); ");

            switch (comcod)
            {
                case "3336":
                case "3337":
                    this.txtCBillRefNo.Text = this.ddlRAList.SelectedItem.Text.Substring(3, 8);
                    break;
            }
            this.Data_DataBind();

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
        protected void ddlSubName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SaveDeposit()
        {

            if (((DataTable)Session["tblbill"]).Rows.Count == 0)
                return;

            string comcod = this.GetCompCode();
            double amount, penalty, deduc, netamt, percentage, sdamt, fpercntage;
            amount = Convert.ToDouble((Convert.IsDBNull(((DataTable)Session["tblbill"]).Compute("sum(billamt)", "")) ? 0.00
                    : ((DataTable)Session["tblbill"]).Compute("sum(billamt)", "")));
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
                case "3310":// RCU  


                case "3315":// Assure Builders
                case "3316":// Assure Development 

                case "3370": // cpdl 
                case "1205": // p2p 
                case "3351":
                case "3352":
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
            double Advpay = Convert.ToDouble("0" + this.txtadvpay.Text.Trim());
            this.lblvalnettotal.Text = (amount + Reward  - (security + deduction + penalty + Advanced)).ToString("#,##0;(#,##0); ");
            //if (((DataTable)Session["tblbill"]).Rows.Count == 0)
            //    return;
            //double amount = Convert.ToDouble((Convert.IsDBNull(((DataTable)Session["tblbill"]).Compute("sum(billamt)", "")) ? 0.00
            //        : ((DataTable)Session["tblbill"]).Compute("sum(billamt)", "")));
            //double penalty = Convert.ToDouble("0" + this.txtPenaltyAmount.Text.Trim());
            //double deduc = Convert.ToDouble("0" + this.txtDedAmount.Text.Trim());

            //double netamt =  (amount - penalty - deduc);

            //double percentage = Convert.ToDouble("0" + this.txtpercentage.Text.Replace("%", "").Trim());
            //double sdamt = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim());

            //this.txtSDAmount.Text = sdamt > 0 ? sdamt.ToString("#,#,#0.00;(#, #,#0.00); ") : Convert.ToDouble(netamt * percentage * 0.01).ToString("#,#,#0.00;(#, #,#0.00); ");
            //double fpercntage = (sdamt > 0) ? (netamt > 0 ? ((Convert.ToDouble(this.txtSDAmount.Text) * 100) / netamt) : 0.00) : percentage;
            //this.txtpercentage.Text = fpercntage.ToString("#,#,#0.00;(#, #,#0.00); ") + "%";
            //double security = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim());
            //double deduction = Convert.ToDouble("0" + this.txtDedAmount.Text.Trim());
            //double Advanced = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim());
            //double Reward = Convert.ToDouble("0" + this.txtreward.Text.Trim());
            //this.lblvalnettotal.Text = (amount + Reward - (security + deduction + penalty + Advanced)).ToString("#,##0;(#,##0); ");
        }
        protected void lbtnDepost_Click(object sender, EventArgs e)
        {
            this.SaveDeposit();


        }
        protected void lbtnDeleteBill_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblbill"];
            string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string billno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();

            bool result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELLCBILLFINAB", billno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
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
        protected void ddlProjectName_SelectedIndexChanged1(object sender, EventArgs e)
        {
            this.GetSubContractor();
        }




        public void createTable()
        {
            DataTable mnuTbl1 = new DataTable();
            // Add Auto Increment Column called ID
            mnuTbl1.Columns.Add(new DataColumn("id")
            {
                AutoIncrement = true,
                AllowDBNull = false,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                DataType = typeof(System.Int32),
                Unique = true
            });
            mnuTbl1.Columns.Add("billno", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("itemsurl", Type.GetType("System.String"));
            Session["tblAttDocs"] = mnuTbl1;


        }
        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {

            DataTable dt = (DataTable)Session["tblAttDocs"];
            string comcod = this.GetCompCode();

            string billno = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();


            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            if (AsyncFileUpload1.HasFile)
            {
                Random r = new Random();
                int next = r.Next(0, 999999999);
                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/Purchase/") + billno + '_' + next + extension);
                Url = billno + '_' + next + extension;

                DataRow[] dr2 = dt.Select("itemsurl = '" + Url + "'");
                if (dr2.Length == 0)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["billno"] = billno;
                    dr1["itemsurl"] = Url;
                    dt.Rows.Add(dr1);
                }
            }
            Session["tblAttDocs"] = dt;
            //this.btnShowimg_Click(null, null);



        }
        protected void btnShowimg_Click(object sender, EventArgs e)
        {
            Session.Remove("tblAttDocs");
            string comcod = this.GetCompCode();


            string billno = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(7, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.lblCurNo2.Text.Trim();


            DataSet ds = PurData.GetTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "SHOWXMLINFORAMTIONREQAFINAL", billno, "", "", "", "", "", "", "", "");
            if (ds == null)
            {
                return;
            }

            DataTable tbl1 = ds.Tables[0];
            ListViewEmpAll.DataSource = tbl1;
            ListViewEmpAll.DataBind();
            Session["tblAttDocs"] = tbl1;


        }

        protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Image imgname = (Image)e.Item.FindControl("GetImg");
                Label imglink = (Label)e.Item.FindControl("ImgLink");
                string extension = Path.GetExtension(imglink.Text.ToString());
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":
                        imgname.ImageUrl = imglink.Text.ToString();
                        break;
                    case ".PDF":
                    case ".pdf":
                        imgname.ImageUrl = "~/Images/pdf.png";
                        break;
                    case ".xls":
                    case ".xlsx":
                        imgname.ImageUrl = "~/Images/excel.svg";
                        break;
                    case ".doc":
                    case ".docx":
                        imgname.ImageUrl = "~/Images/word.png";
                        break;


                }

            }

        }

        protected void gvSubBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox rate = (TextBox)e.Row.FindControl("lgvSubRate");
                //rate.Enabled = false;
                string comcod = this.GetCompCode();
                switch (comcod)
                {
                    case "3370":
                        rate.Enabled = false;
                        break;

                    default:
                        rate.Enabled = true;
                        break;



                
                
                }
                
                //if (Request.QueryString["status"] != null)
                //{
                //    if (Request.QueryString["status"].ToString() == "S")
                //    {
                //        rate.Enabled = true;
                //    }
                //}

            }
        }

        protected void btnDelbill_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblbill"];
            string comcod = this.GetCompCode();
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string lisuno = ((Label)this.gvSubBill.Rows[index].FindControl("lgcIsuno1")).Text.Trim();
           
            bool result = PurData.UpdateTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "DELETEUPDATEPURLISUUE", lisuno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('"+PurData.ErrorObject["Msg"].ToString()+"');", true);
                return;
            }
            dt.Rows.RemoveAt(index);
            dt.AcceptChanges();
            Session["tblbill"] = dt;
           ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);
            
        }

        protected void btnDelall_OnClick(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblAttDocs"];
            string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");

            string comcod = this.GetCompCode();
            for (int j = 0; j < this.ListViewEmpAll.Items.Count; j++)
            {

                string billno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();


                string filesname = "Upload/Purchase/" + ((Label)this.ListViewEmpAll.Items[j].FindControl("ImgLink")).Text.ToString();




                if (((CheckBox)this.ListViewEmpAll.Items[j].FindControl("ChDel")).Checked == true)
                {
                    string id = ((Label)this.ListViewEmpAll.Items[j].FindControl("id")).Text.ToString();




                    DataSet ds1 = new DataSet("ds1");
                    DataView dv1 = new DataView(dt);

                    dv1.RowFilter = ("id<>" + id);
                    ds1.Tables.Add(dv1.ToTable());
                    ds1.Tables[0].TableName = "tbl1";

                    bool resulta = PurData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "BILLFINALXMLATTACHEDDOCUS", ds1, null, null, billno);

                    if (!resulta)
                    {

                        //return;
                    }
                    else
                    {
                        //string filePath = Server.MapPath("~/InteriorWEB/");
                        //System.IO.File.Delete(filePath + filesname);
                        Session["tblAttDocs"] = ds1.Tables[0];
                        this.lblMesg.Text = " Files Removed ";
                        this.btnShowimg_Click(null, null);
                    }



                }
            }


        }



    }
}