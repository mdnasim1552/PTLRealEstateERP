using System;
using System.Collections;
using System.Collections.Generic;
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
namespace RealERPWEB.F_32_Mis
{
    public partial class ProjTrialBalanc : System.Web.UI.Page
    {
        public static double CAmt, EAmt, BalAmt;
        ProcessAccess accData = new ProcessAccess();
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


                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtDatefrom.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.ImgbtnFindProjind_Click(null, null);
                this.SelectView();
                string type = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "PrjTrailBal") ? "Project Trail Balance"
                    : (type == "LandPrj") ? "Project Trail Balance"
                    : (type == "PrjCost") ? "Project Cost" : (type == "PrjTrailBal3") ? "Trail Balance 3"
                    : (type == "RecAPayment") ? "Receipts & Payments (Project)" : "Trail Balance 2";


            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string date;
            switch (type)
            {
                case "PrjTrailBal":
                case "LandPrj":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "TrailBal2":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.lblPrjName.Visible = false;
                    this.txtSearchpIndp.Visible = false;
                    this.ImgbtnFindProjind.Visible = false;
                    this.ddlProjectInd.Visible = false;
                    this.lblGrp.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    break;
                case "PrjCost":

                    this.lbltodate.Visible = true;
                    this.txttodate.Visible = true;
                    date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtDatefrom.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(this.txtDatefrom.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "PrjTrailBal3":
                    this.lbltodate.Visible = true;
                    this.txttodate.Visible = true;
                    this.lblGrp.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.chkdetails.Visible = true;
                    date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtDatefrom.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(this.txtDatefrom.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 3;
                    this.GetResGroup();
                    break;


                case "RecAPayment":
                    this.lbltodate.Visible = true;
                    this.txttodate.Visible = true;
                    this.lblGrp.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.chkdetails.Visible = false;
                    date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtDatefrom.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(this.txtDatefrom.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 4;
                    break;


            }
        }

        private void GetResGroup()
        {

            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "GETRESMAINGROUP", "", "", "", "", "", "", "", "", "");

                this.ddlProGroup.DataSource = ds1.Tables[0];
                this.ddlProGroup.DataTextField = "resdesc";
                this.ddlProGroup.DataValueField = "rescode";
                this.ddlProGroup.DataBind();
                ds1.Dispose();
            }


            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }


        }

        protected void ImgbtnFindProjind_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            // string filter ="%" +this.txtSearchpIndp.Text.Trim() + "%";

            string filter = (this.Request.QueryString["prjcode"].ToString()).Length == 0 ? "%" + this.txtSearchpIndp.Text.Trim() + "%" : this.Request.QueryString["prjcode"].ToString() + "%";

            // string pactcode = (this.Request.QueryString["Type"].ToString() == "LandPrj") ? "16%" : "4[1-9]%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "GETPROJECTNAME", "", filter, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.ddlProjectInd.DataSource = dt1;
            this.ddlProjectInd.DataTextField = "actdesc1";
            this.ddlProjectInd.DataValueField = "actcode";
            this.ddlProjectInd.DataBind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PrjTrailBal":
                case "LandPrj":
                    this.ShowPrjTriBal();
                    break;

                case "TrailBal2":
                    this.ShowTrailsBal2();
                    break;
                case "PrjCost":
                    this.ShowProCost();
                    break;
                case "PrjTrailBal3":
                    this.ShowTrailsBal3();
                    break;


                case "RecAPayment":
                    this.ShowRecAPayment();
                    break;
            }
        }
        private void ShowPrjTriBal()
        {
            Session.Remove("tblprjtbl");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string actcode = (((ASTUtility.Right(this.ddlProjectInd.SelectedValue, 10) == "0000000000") ? this.ddlProjectInd.SelectedValue.ToString().Substring(0, 2)
                : (ASTUtility.Right(this.ddlProjectInd.SelectedValue, 8) == "00000000") ? this.ddlProjectInd.SelectedValue.ToString().Substring(0, 4)
                : (ASTUtility.Right(this.ddlProjectInd.SelectedValue, 4) == "0000") ? this.ddlProjectInd.SelectedValue.ToString().Substring(0, 8) : this.ddlProjectInd.SelectedValue.ToString()) + "%");

            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = mRptGroup == "0" ? "2" : mRptGroup == "1" ? "4" : mRptGroup == "2" ? "7" : mRptGroup == "3" ? "9" : "12";// (mRptGroup == "0" ? "4" : (mRptGroup == "1" ? "7" : (mRptGroup == "2" ? "9" : "12")));
            string CallType = (ASTUtility.Left(actcode, 2) == "41") ? "RPT_PROJ_TRIALBAL" : (ASTUtility.Left(actcode, 2) == "16") ? "RPT_PROJ_TRIALBAL" : "RPTPROJTRIALBALHF";


            string advance = String.Empty;
            if (comcod == "1205" || comcod == "3351" || comcod == "3352" || comcod == "3101" || comcod=="3353" || comcod== "1102" || comcod=="3364" || comcod == "3357" || comcod == "3367" || comcod == "3368")
            {
                advance = "ADVANCE";
            }

            // DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", CallType, "", date1, actcode, mRptGroup, "", "", "", "", "");

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", CallType, "", date1, actcode, mRptGroup, "", advance, "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.gvPrjtrbal.DataSource = null;
                this.gvPrjtrbal.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tblprjtbl"] = dt;
            this.gvPrjtrbal.DataSource = dt;
            this.gvPrjtrbal.DataBind();

            Session["tblFooter"] = ds2.Tables[1];
            Session["tblPrjname"] = ds2.Tables[2];


            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            ((HyperLink)this.gvPrjtrbal.HeaderRow.FindControl("hlbtntbCdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            Session["Report1"] = gvPrjtrbal;
            ((HyperLink)this.gvPrjtrbal.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            //((Label)this.gvPrjtrbal.FooterRow.FindControl("lgvFTDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(dramt)", "")) ?
            //          0.00 : ds2.Tables[1].Compute("Sum(dramt)", ""))).ToString("#,##0;(#,##0); - ");

            //((Label)this.gvPrjtrbal.FooterRow.FindControl("lgvFTCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(cramt)", "")) ?
            //         0.00 : ds2.Tables[1].Compute("Sum(cramt)", ""))).ToString("#,##0;(#,##0); - ");

        }
        private void ShowTrailsBal2()
        {
            Session.Remove("tblprjtbl");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTTRIALBALANCE2", date1, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.grvTrBal2.DataSource = null;
                this.grvTrBal2.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tblprjtbl"] = dt;

            this.grvTrBal2.DataSource = dt;
            this.grvTrBal2.DataBind();

            Session["tblFooter"] = ds2.Tables[1];


            ((Label)this.grvTrBal2.FooterRow.FindControl("lgvFTDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(dram)", "")) ?
                      0.00 : ds2.Tables[1].Compute("Sum(dram)", ""))).ToString("#,##0;(#,##0); - ");

            ((Label)this.grvTrBal2.FooterRow.FindControl("lgvFTCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(cram)", "")) ?
                     0.00 : ds2.Tables[1].Compute("Sum(cram)", ""))).ToString("#,##0;(#,##0); - ");

        }

        private void ShowProCost()
        {
            Session.Remove("tblprjtbl");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = this.txtDatefrom.Text.Substring(0, 11);
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string actcode = (((ASTUtility.Right(this.ddlProjectInd.SelectedValue, 10) == "0000000000") ? this.ddlProjectInd.SelectedValue.ToString().Substring(0, 2)
                : (ASTUtility.Right(this.ddlProjectInd.SelectedValue, 8) == "00000000") ? this.ddlProjectInd.SelectedValue.ToString().Substring(0, 4)
                : (ASTUtility.Right(this.ddlProjectInd.SelectedValue, 4) == "0000") ? this.ddlProjectInd.SelectedValue.ToString().Substring(0, 8) : this.ddlProjectInd.SelectedValue.ToString()) + "%");

            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "4" : (mRptGroup == "1" ? "7" : (mRptGroup == "2" ? "9" : "12")));

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTPROJECTCOST", "", frmdate, actcode, mRptGroup, todate, "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.gvprocost.DataSource = null;
                this.gvprocost.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tblprjtbl"] = dt;
            Session["tblPrjname"] = ds2.Tables[2];
            this.gvprocost.DataSource = dt;
            this.gvprocost.DataBind();

            Session["Report1"] = gvprocost;
            ((HyperLink)this.gvprocost.HeaderRow.FindControl("hlbtntbCdataExelpc")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            double tocost = Convert.ToDouble(ds2.Tables[1].Rows[0]["opnam"]) + Convert.ToDouble(ds2.Tables[1].Rows[0]["todramt"]);
            ((Label)this.gvprocost.FooterRow.FindControl("lgvFOpeningpc")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["opnam"]) == 0.00 ? "" : Convert.ToDouble(ds2.Tables[1].Rows[0]["opnam"]).ToString("#,##0;(#,##0);");
            ((Label)this.gvprocost.FooterRow.FindControl("lgvFTDrAmtpc")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["todramt"]) == 0.00 ? "" : Convert.ToDouble(ds2.Tables[1].Rows[0]["todramt"]).ToString("#,##0;(#,##0);");

            ((Label)this.gvprocost.FooterRow.FindControl("lgvFtoAmt")).Text = tocost.ToString("#,##0;(#,##0);");

            ((Label)this.gvprocost.FooterRow.FindControl("lgvFgtocost")).Text = tocost.ToString("#,##0;(#,##0);");


            //((Label)this.gvPrjtrbal.FooterRow.FindControl("lgvFTCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(cramt)", "")) ?
            //         0.00 : ds2.Tables[1].Compute("Sum(cramt)", ""))).ToString("#,##0;(#,##0); - ");

        }

        private void ShowTrailsBal3()
        {

            Session.Remove("tblprjtbl");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = this.txtDatefrom.Text.Substring(0, 11);
            string todate = this.txttodate.Text;
            string actcode = this.ddlProjectInd.SelectedValue.ToString();
            //string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            //mRptGroup = (mRptGroup == "0" ? "4" : (mRptGroup == "1" ? "7" : (mRptGroup == "2" ? "9" : "12")));

            string mRptGroup = this.chkdetails.Checked ? "Details" : "";
            string resgroup = (this.ddlProGroup.SelectedValue == "000000000000" ? "" : this.ddlProGroup.SelectedValue.ToString().Substring(0, 2)) + "%";

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPT_PROJ_TRIALBALALL", date, todate, actcode, mRptGroup, resgroup, "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.gvprjtbal03.DataSource = null;
                this.gvprjtbal03.DataBind();
                return;
            }

            DataTable dt = ds2.Tables[0];
            Session["tblprjtbl"] = this.HiddenSameData(dt);
            Session["tblFooter"] = ds2.Tables[1];
            Session["tblPrjname"] = ds2.Tables[2];


            this.gvprjtbal03.DataSource = dt;
            this.gvprjtbal03.DataBind();





            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            ((HyperLink)this.gvprjtbal03.HeaderRow.FindControl("hlbtntbCdataExeltp")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            Session["Report1"] = gvprjtbal03;
            ((HyperLink)this.gvprjtbal03.HeaderRow.FindControl("hlbtntbCdataExeltp")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            ((Label)this.gvprjtbal03.FooterRow.FindControl("lgvFopdbamt")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["opndram"]) == 0.00 ? "0" : Convert.ToDouble(ds2.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0);");
            ((Label)this.gvprjtbal03.FooterRow.FindControl("lgvFopCramt")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["opncram"]) == 0.00 ? "0" : Convert.ToDouble(ds2.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0);");
            ((Label)this.gvprjtbal03.FooterRow.FindControl("lgvFopnamtp")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["opnam"]) == 0.00 ? "0" : Convert.ToDouble(ds2.Tables[1].Rows[0]["opnam"]).ToString("#,##0;(#,##0);");



            ((Label)this.gvprjtbal03.FooterRow.FindControl("lgvFTDrAmttp")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["dramt"]) == 0.00 ? "0" : Convert.ToDouble(ds2.Tables[1].Rows[0]["dramt"]).ToString("#,##0;(#,##0);");
            ((Label)this.gvprjtbal03.FooterRow.FindControl("lgvFTCrAmttp")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["cramt"]) == 0.00 ? "0" : Convert.ToDouble(ds2.Tables[1].Rows[0]["cramt"]).ToString("#,##0;(#,##0);");
            ((Label)this.gvprjtbal03.FooterRow.FindControl("lgvFoClsdbamt")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["clsdram"]) == 0.00 ? "0" : Convert.ToDouble(ds2.Tables[1].Rows[0]["clsdram"]).ToString("#,##0;(#,##0);");
            ((Label)this.gvprjtbal03.FooterRow.FindControl("lgvFoClsCramt")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["clscram"]) == 0.00 ? "0" : Convert.ToDouble(ds2.Tables[1].Rows[0]["clscram"]).ToString("#,##0;(#,##0);");
            ((Label)this.gvprjtbal03.FooterRow.FindControl("lgvFClsamtp")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["clsam"]) == 0.00 ? "0" : Convert.ToDouble(ds2.Tables[1].Rows[0]["clsam"]).ToString("#,##0;(#,##0);");







        }

        private void ShowRecAPayment()
        {
            Session.Remove("tblprjtbl");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = this.txtDatefrom.Text.Substring(0, 11);
            string todate = this.txttodate.Text;
            string actcode = this.ddlProjectInd.SelectedValue.ToString();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTCASHFLOWPROWISE", date, todate, actcode, "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.gvRecAPayment.DataSource = null;
                this.gvRecAPayment.DataBind();
                return;
            }

            DataTable dt = ds2.Tables[0];
            Session["tblprjtbl"] = this.HiddenSameData(dt);
            this.gvRecAPayment.DataSource = dt;
            this.gvRecAPayment.DataBind();

            Session["Report1"] = gvRecAPayment;
            ((HyperLink)this.gvRecAPayment.HeaderRow.FindControl("hlbtnexpostexcelrp")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }
        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            int j, i = 0;
            string grpcode;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PrjTrailBal":
                    grpcode = dt1.Rows[0]["grp"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grpcode)
                        {
                            grpcode = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }
                        else
                        {
                            grpcode = dt1.Rows[j]["grp"].ToString();
                        }

                    }
                    break;

                case "TrailBal2":
                case "PrjTrailBal3":
                    string actcode1 = dt1.Rows[0]["actcode1"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
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
                    break;


                case "RecAPayment":

                    string grp = dt1.Rows[0]["grp"].ToString();
                    string sgrp = dt1.Rows[0]["sgrp"].ToString();
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        if (i == 0)
                        {
                            grp = dr1["grp"].ToString();
                            sgrp = dr1["sgrp"].ToString();
                            i++;
                            continue;
                        }

                        if (dr1["grp"].ToString() == grp && dr1["sgrp"].ToString() == sgrp)
                        {

                            dr1["grpdesc"] = "";
                            dr1["sgrpdesc"] = "";

                        }

                        else
                        {

                            if (dr1["grp"].ToString() == grp)
                                dr1["grpdesc"] = "";

                            if (dr1["sgrp"].ToString() == sgrp)
                                dr1["sgrpdesc"] = "";

                        }


                        grp = dr1["grp"].ToString();
                        sgrp = dr1["sgrp"].ToString();
                    }








                    break;

            }
            return dt1;
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PrjTrailBal":
                case "LandPrj":
                    this.RtpPrjTrBal();
                    break;
                case "TrailBal2":
                    this.RtpTrBal2();
                    break;
                case "PrjCost":
                    this.ReportProCost();
                    break;
                case "PrjTrailBal3":
                    this.PrintTrailBal3();
                    break;
                case "RecAPayment":
                    this.PrintRecPayT();
                    break;
            }
        }


        private void RtpPrjTrBal()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            switch (comcod)
            {

                case "3332":
                    this.PrintPrjTrBalaceInnstar();
                    break;
                default:
                    this.RtpPrjTrBalGen();
                    break;
            }
        }


        private void PrintRecPayT()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comnam"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string daterange = "(From " + this.txtDatefrom.Text + " To " + this.txttodate.Text + ")";
            string Actcode = this.ddlProjectInd.SelectedItem.Text.ToString().Substring(13);
            // Session.Remove("tblprjtbl");

            DataTable dt1 = (DataTable)Session["tblprjtbl"];
            //DataTable dt2 = (DataTable)Session["tblFooter"];
            //DataTable dt3 = (DataTable)Session["tblPrjname"];
            if (dt1.Rows.Count == 0)
                return;

            var lst = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.ResPayt>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptProjRecPay", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtprjname", "Project Name: " + Actcode));
            Rpt1.SetParameters(new ReportParameter("txtdate", daterange));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Receipts & Payments (Project)"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintTrailBal3()
        {
            // Iqbal Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Actcode = this.ddlProjectInd.SelectedItem.Text.ToString().Substring(13);
            string Date1 = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyy");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string daterange = "(From " + this.txtDatefrom.Text + " To " + this.txttodate.Text + ")";
            LocalReport Rpt1 = new LocalReport();
            DataTable dt1 = (DataTable)Session["tblprjtbl"];
            DataTable dt2 = (DataTable)Session["tblFooter"];

            string opndram = Convert.ToDouble(dt2.Rows[0]["opndram"]).ToString("##,###,#0; (#,#0); ");
            string opncram = Convert.ToDouble(dt2.Rows[0]["opncram"]).ToString("##,###,#0; (#,#0); ");
            string opnamt = Convert.ToDouble(dt2.Rows[0]["opnam"]).ToString("##,###,#0; (#,#0); ");
            string dramt = Convert.ToDouble(dt2.Rows[0]["dramt"]).ToString("##,###,#0; (#,#0); ");
            string cramt = Convert.ToDouble(dt2.Rows[0]["cramt"]).ToString("##,###,#0; (#,#0); ");
            string clsdram = Convert.ToDouble(dt2.Rows[0]["clsdram"]).ToString("##,###,#0; (#,#0); ");
            string clscram = Convert.ToDouble(dt2.Rows[0]["clscram"]).ToString("##,###,#0; (#,#0); ");
            string clsamt = Convert.ToDouble(dt2.Rows[0]["clsam"]).ToString("##,###,#0; (#,#0); ");


            var lst = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.TrailsBal3>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptTrailsBal3", lst, null, null);

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("ProjName", "Project Name:" + Actcode));
            Rpt1.SetParameters(new ReportParameter("FDate", daterange));
            Rpt1.SetParameters(new ReportParameter("RptTital", "Project Trial Balance-3"));
            Rpt1.SetParameters(new ReportParameter("txtopndram", opndram));
            Rpt1.SetParameters(new ReportParameter("txtopncram", opncram));
            Rpt1.SetParameters(new ReportParameter("txtopnam", opnamt));
            Rpt1.SetParameters(new ReportParameter("txtcurdram", dramt));
            Rpt1.SetParameters(new ReportParameter("txtcurcram", cramt));
            Rpt1.SetParameters(new ReportParameter("txtclsdram", clsdram));
            Rpt1.SetParameters(new ReportParameter("txtclscram", clscram));
            Rpt1.SetParameters(new ReportParameter("txtclsam", clsamt));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintPrjTrBalaceInnstar()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblprjtbl"];
            DataTable dt2 = (DataTable)Session["tblFooter"];
            DataTable dt3 = (DataTable)Session["tblPrjname"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptProjTrialBalanceInnStar();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
            txtCompany.Text = comnam;

            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtfdate.Text = "As on Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["txtProjName"] as TextObject;
            txtprojectname.Text = "Project Name: " + (dt3.Rows[0]["prjsdesc"]).ToString(); // prjsdesc   this.ddlProjectInd.SelectedItem.ToString().Trim().Substring(13);

            //TextObject txtdramt = rptstk.ReportDefinition.ReportObjects["txtdramt"] as TextObject;
            //txtdramt.Text = Convert.ToDouble(dt2.Rows[0]["dramt"]).ToString("#, #,#0; (#, #,#0); "); ;
            //TextObject txtcramt = rptstk.ReportDefinition.ReportObjects["txtcramt"] as TextObject;
            //txtcramt.Text = Convert.ToDouble(dt2.Rows[0]["cramt"]).ToString("#, #,#0; (#, #,#0); "); ;

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptstk.SetDataSource(dt1);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RtpPrjTrBalGen()
        {

            // Iqbal Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comnam"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();

            DataTable dt1 = (DataTable)Session["tblprjtbl"];
            DataTable dt2 = (DataTable)Session["tblFooter"];
            DataTable dt3 = (DataTable)Session["tblPrjname"];
            if (dt1.Rows.Count == 0)
                return;

            var lst = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.ProjectTrlBal>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptProjTrialBalance", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtprjname", "Project Name: " + (dt3.Rows[0]["prjsdesc"]).ToString()));
            Rpt1.SetParameters(new ReportParameter("txtdate", "As on Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Project Trial Balance"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string comcod = hst["comcod"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblprjtbl"];
            //DataTable dt2 = (DataTable)Session["tblFooter"];
            //DataTable dt3 = (DataTable)Session["tblPrjname"];
            //if (dt1.Rows.Count == 0)
            //    return;
            //ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptProjTrialBalance();
            ////TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["CompName"] as TextObject;
            ////txtCompany.Text = comnam;

            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtfdate.Text = "As on Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["txtProjName"] as TextObject;
            //txtprojectname.Text = "Project Name: " + (dt3.Rows[0]["prjsdesc"]).ToString(); // prjsdesc   this.ddlProjectInd.SelectedItem.ToString().Trim().Substring(13);

            ////TextObject txtdramt = rptstk.ReportDefinition.ReportObjects["txtdramt"] as TextObject;
            ////txtdramt.Text = Convert.ToDouble(dt2.Rows[0]["dramt"]).ToString("#, #,#0; (#, #,#0); "); ;
            ////TextObject txtcramt = rptstk.ReportDefinition.ReportObjects["txtcramt"] as TextObject;
            ////txtcramt.Text = Convert.ToDouble(dt2.Rows[0]["cramt"]).ToString("#, #,#0; (#, #,#0); "); ;

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptstk.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void RtpTrBal2()
        {
            // Iqbal Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comnam"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();

            DataTable dt1 = (DataTable)Session["tblprjtbl"];
            DataTable dt2 = (DataTable)Session["tblFooter"];

            if (dt1.Rows.Count == 0)
                return;

            var lst = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.Trialbal02>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptTrialBalance2", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("txtprjname", "Project Name: " + (dt3.Rows[0]["prjsdesc"]).ToString())); // commant by iqbal Nayan
            Rpt1.SetParameters(new ReportParameter("txtdate", "As on Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Trial Balance 2"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string comcod = hst["comcod"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblprjtbl"];
            //DataTable dt2 = (DataTable)Session["tblFooter"];

            //if (dt1.Rows.Count == 0)
            //    return;
            //ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptTrialBalance2();

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //txtCompany.Text = comnam;

            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtfdate.Text = "As on Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");

            ////TextObject txtdramt = rptstk.ReportDefinition.ReportObjects["txtdramt"] as TextObject;
            ////txtdramt.Text = Convert.ToDouble(dt2.Rows[0]["dram"]).ToString("#, #,#0; (#, #,#0); "); ;
            ////TextObject txtcramt = rptstk.ReportDefinition.ReportObjects["txtcramt"] as TextObject;
            ////txtcramt.Text = Convert.ToDouble(dt2.Rows[0]["cram"]).ToString("#, #,#0; (#, #,#0); "); ;

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptstk.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void ReportProCost()
        {
            // Iqbal Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comnam"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            DataTable dt1 = (DataTable)Session["tblprjtbl"];
            DataTable dt3 = (DataTable)Session["tblPrjname"];
            if (dt1.Rows.Count == 0)
                return;

            var lst = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.ProjectCost>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptProjCost", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtprjname", "Project Name: " + (dt3.Rows[0]["prjsdesc"]).ToString()));
            Rpt1.SetParameters(new ReportParameter("txtdate", "(From " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "TRIAL  BALANCE"));
            Rpt1.SetParameters(new ReportParameter("Gtotal", ((Label)this.gvprocost.FooterRow.FindControl("lgvFgtocost")).Text));
            Rpt1.SetParameters(new ReportParameter("opning", ((Label)this.gvprocost.FooterRow.FindControl("lgvFOpeningpc")).Text));
            Rpt1.SetParameters(new ReportParameter("Current", ((Label)this.gvprocost.FooterRow.FindControl("lgvFTDrAmtpc")).Text));

            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string comcod = hst["comcod"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblprjtbl"];

            //DataTable dt3 = (DataTable)Session["tblPrjname"];
            //if (dt1.Rows.Count == 0)
            //    return;
            //ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptProjCost();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //txtCompany.Text = comnam;

            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtfdate.Text = "(From " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";

            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["txtProjName"] as TextObject;
            //txtprojectname.Text = "Project Name: " + (dt3.Rows[0]["prjsdesc"]).ToString(); // prjsdesc   this.ddlProjectInd.SelectedItem.ToString().Trim().Substring(13);


            //TextObject txttodramt = rptstk.ReportDefinition.ReportObjects["txttodramt"] as TextObject;
            //txttodramt.Text = ((Label)this.gvprocost.FooterRow.FindControl("lgvFTDrAmtpc")).Text; // prjsdesc   this.ddlProjectInd.SelectedItem.ToString().Trim().Substring(13);
            //TextObject txtOpening = rptstk.ReportDefinition.ReportObjects["txtOpening"] as TextObject;
            //txtOpening.Text = ((Label)this.gvprocost.FooterRow.FindControl("lgvFOpeningpc")).Text;



            //TextObject txtgrntotal = rptstk.ReportDefinition.ReportObjects["txtgrntotal"] as TextObject;
            //txtgrntotal.Text = ((Label)this.gvprocost.FooterRow.FindControl("lgvFgtocost")).Text;

            ////TextObject txtdramt = rptstk.ReportDefinition.ReportObjects["txtdramt"] as TextObject;
            ////txtdramt.Text = Convert.ToDouble(dt2.Rows[0]["dramt"]).ToString("#, #,#0; (#, #,#0); "); ;
            ////TextObject txtcramt = rptstk.ReportDefinition.ReportObjects["txtcramt"] as TextObject;
            ////txtcramt.Text = Convert.ToDouble(dt2.Rows[0]["cramt"]).ToString("#, #,#0; (#, #,#0); "); ;

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptstk.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void gvPrjtrbal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesc");
                Label DAmount = (Label)e.Row.FindControl("lgvAmt");
                Label CAmount = (Label)e.Row.FindControl("lgvCre");

                string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
                mRptGroup = (mRptGroup == "0" ? "4" : (mRptGroup == "1" ? "7" : (mRptGroup == "2" ? "9" : "12")));
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right((code), 10) == "0000000000")
                {
                    actdesc.Font.Bold = true;
                    DAmount.Font.Bold = true;
                    CAmount.Font.Bold = true;

                    DAmount.Style.Add("text-align", "Left");
                    CAmount.Style.Add("text-align", "Left");
                    actdesc.Attributes["style"] = "color:maroon;";
                }
                else if (code == "000000000001" || (ASTUtility.Right((code), 3) == "000") && mRptGroup == "12")
                {
                    actdesc.Font.Bold = true;
                    DAmount.Font.Bold = true;
                    CAmount.Font.Bold = true;
                    DAmount.Style.Add("text-align", "Left");
                    CAmount.Style.Add("text-align", "Left");

                }
                if (code == "999999999999" || code == "000000000002" || code == "000000000003" || code == "000000000004")
                {
                    actdesc.Font.Bold = true;
                    DAmount.Font.Bold = true;
                    CAmount.Font.Bold = true;
                    actdesc.Style.Add("text-align", "Right");
                    DAmount.Style.Add("text-align", "Right");
                    CAmount.Style.Add("text-align", "Right");
                }
                if (code == "AAAAAAAAAAAA")
                {
                    actdesc.Style.Add("text-align", "Left");
                }


                //if (e.Row.RowType != DataControlRowType.DataRow)
                //    return;
                string rescode1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode1")).ToString();
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
                string Actcode = this.ddlProjectInd.SelectedValue.ToString();
                string Date1 = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyy");
                string rescode = ((Label)e.Row.FindControl("lblgvCode")).Text;
                if (ASTUtility.Left(rescode1, 2) == "51")
                {
                    hlink1.NavigateUrl = "RptProjectCollBrkDown.aspx?Type=PrjCol&pactcode=" + Actcode + "&Date1=" + Date1;
                }

                else if (ASTUtility.Right((code), 3) != "000" && code != "000000000001" && code != "999999999999" && code != "000000000002")
                {
                    hlink1.NavigateUrl = "RptProjectCollBrkDown.aspx?Type=SpLedger&pactcode=" + Actcode + "&Date1=" + Date1 + "&rescode=" + rescode;
                }




            }


        }
        protected void grvTrBal2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label actdesc = (Label)e.Row.FindControl("lgcActDesc");
                Label DAmount = (Label)e.Row.FindControl("lgvAmt");
                Label CAmount = (Label)e.Row.FindControl("lgvCre");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode1")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "DIFFERENCE")
                {
                    actdesc.Font.Bold = true;
                    DAmount.Font.Bold = true;
                    CAmount.Font.Bold = true;
                }


            }
        }
        protected void gvprocost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDescpc");
                Label Openingpc = (Label)e.Row.FindControl("lgvOpeningpc");

                Label DAmount = (Label)e.Row.FindControl("lgvAmtpc");
                Label toAmt = (Label)e.Row.FindControl("lgvtoAmt");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right((code), 10) == "0000000000" || code == "000000000001")
                {
                    actdesc.Font.Bold = true;
                    Openingpc.Font.Bold = true;
                    DAmount.Font.Bold = true;
                    DAmount.Style.Add("text-align", "Left");
                    toAmt.Font.Bold = true;
                    toAmt.Style.Add("text-align", "Left");

                }
                if (code == "999999999999" || code == "000000000002" || code == "000000000003" || code == "000000000004")
                {
                    actdesc.Font.Bold = true;
                    Openingpc.Font.Bold = true;
                    actdesc.Style.Add("text-align", "Right");
                    DAmount.Font.Bold = true;
                    DAmount.Style.Add("text-align", "Right");

                    toAmt.Font.Bold = true;
                    toAmt.Style.Add("text-align", "Right");

                }
                if (code == "AAAAAAAAAAAA")
                {
                    actdesc.Style.Add("text-align", "Left");
                }


                //if (e.Row.RowType != DataControlRowType.DataRow)
                //    return;
                string rescode1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode1")).ToString();
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDescpc");
                string Actcode = this.ddlProjectInd.SelectedValue.ToString();
                string Date1 = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyy");
                string rescode = ((Label)e.Row.FindControl("lblgvCodepc")).Text;
                if (ASTUtility.Left(rescode1, 4) == "AAAA")
                {
                    hlink1.NavigateUrl = "RptProjectCollBrkDown.aspx?Type=PrjCol&pactcode=" + Actcode + "&Date1=" + Date1;
                }

                if (ASTUtility.Right((code), 10) != "0000000000" && code != "000000000001" && code != "999999999999" && code != "000000000002")
                {
                    hlink1.NavigateUrl = "RptProjectCollBrkDown.aspx?Type=SpLedger&pactcode=" + Actcode + "&Date1=" + Date1 + "&rescode=" + rescode;
                }


            }
        }
        protected void gvprjtbal03_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label rescod = (Label)e.Row.FindControl("lblgvCodetp");
                HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesctp");
                Label opnam = (Label)e.Row.FindControl("lgvopnamtp");
                Label DAmount = (Label)e.Row.FindControl("lgvAmttp");
                Label CAmount = (Label)e.Row.FindControl("lgvCreamttp");
                Label Clsam = (Label)e.Row.FindControl("lgvClsamtp");

                string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
                mRptGroup = (mRptGroup == "0" ? "4" : (mRptGroup == "1" ? "7" : (mRptGroup == "2" ? "9" : "12")));
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();


                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right((code), 10) == "0000000000")
                {
                    //actdesc.Font.Bold = true;
                    //opnam.Font.Bold = true;
                    //DAmount.Font.Bold = true;
                    //DAmount.Font.Bold = true;
                    //Clsam.Font.Bold = true;

                    //opnam.Style.Add("text-align", "Left");
                    //DAmount.Style.Add("text-align", "Left");
                    //CAmount.Style.Add("text-align", "Left");
                    //Clsam.Style.Add("text-align", "Left");
                    actdesc.Attributes["style"] = "font-weight:bold;text-align:left; color:maroon;";
                    rescod.Attributes["style"] = "font-weight:bold;text-align:left; color:maroon;";
                    opnam.Attributes["style"] = "font-weight:bold;text-align:left; color:maroon;";
                    DAmount.Attributes["style"] = opnam.Attributes["style"] = "font-weight:bold;text-align:left; color:maroon;";
                    Clsam.Attributes["style"] = opnam.Attributes["style"] = "font-weight:bold;text-align:left; color:maroon;";


                }

                else if (code == "000000000001" || ((ASTUtility.Right((code), 3) == "000") && mRptGroup == "12"))
                {
                    actdesc.Font.Bold = true;
                    rescod.Font.Bold = true;
                    opnam.Font.Bold = true;
                    DAmount.Font.Bold = true;

                    CAmount.Font.Bold = true;
                    Clsam.Font.Bold = true;

                    opnam.Style.Add("text-align", "Left");
                    DAmount.Style.Add("text-align", "Left");
                    CAmount.Style.Add("text-align", "Left");
                    Clsam.Style.Add("text-align", "Left");

                }




                if (code == "AAAAAAAAAAAA")
                {
                    actdesc.Style.Add("text-align", "Left");
                }

                if (code == "010000000000")
                {
                    string frmdate = this.txtDatefrom.Text;
                    string todate = this.txttodate.Text;
                    string actcode = this.ddlProjectInd.SelectedValue;
                    actdesc.NavigateUrl = "~/F_17_Acc/CashBankposition.aspx?Type=cascredpur&frmdate=" + frmdate + "&todate=" +
                                          todate + "&actcode=" + actcode;
                }
                if (code == "040000000000")
                {
                    string frmdate = this.txtDatefrom.Text;
                    string todate = this.txttodate.Text;
                    string actcode = this.ddlProjectInd.SelectedValue;
                    actdesc.NavigateUrl = "~/F_17_Acc/CashBankposition.aspx?Type=labour&frmdate=" + frmdate + "&todate=" +
                                          todate + "&actcode=" + actcode;
                }



                if (code == "111111111111")
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    string frmdate = this.txtDatefrom.Text;
                    string todate = this.txttodate.Text;
                    //  string pactcode = this.ddlProjectInd.SelectedValue;
                    string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                    string witopn = "";

                    actdesc.NavigateUrl = "~/F_17_Acc/AccMultiReport.aspx?comcod=" + comcod + "&rpttype=ledger&Date1=" + frmdate + "&Date2=" +
                                      todate + "&actcode=" + pactcode + "&opnoption=" + witopn;




                }


                else if (code.Substring(9) != "000")
                {
                    string frmdate = this.txtDatefrom.Text;
                    string todate = this.txttodate.Text;
                    string pactcode = this.ddlProjectInd.SelectedValue;
                    string rescode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                    actdesc.NavigateUrl = "~/F_17_Acc/AccMultiReport.aspx?rpttype=spledgerprj&frmdate=" + frmdate + "&todate=" +
                                      todate + "&pactcode=" + pactcode + "&rescode=" + rescode;
                }




            }
        }
        protected void gvRecAPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblLgvDescrap = (Label)e.Row.FindControl("lblLgvDescrap");

                Label lgvbudgetamt = (Label)e.Row.FindControl("lgvbudgetamt");
                Label lgvpream = (Label)e.Row.FindControl("lgvpream");
                Label lgvcuram = (Label)e.Row.FindControl("lgvcuram");
                Label lgvtoam = (Label)e.Row.FindControl("lgvtoam");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "aarescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right((code), 4) == "AAAA")
                {
                    lblLgvDescrap.Attributes["style"] = "font-weight:bold;color:maroon;";
                    lgvbudgetamt.Attributes["style"] = "font-weight:bold;color:maroon;";
                    lgvpream.Attributes["style"] = "font-weight:bold;color:maroon;";
                    lgvcuram.Attributes["style"] = "font-weight:bold;color:maroon;";
                    lgvtoam.Attributes["style"] = "font-weight:bold;color:maroon;";
                }






            }
        }
    }
}