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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
using RealERPRDLC;

namespace RealERPWEB.F_32_Mis
{
    public partial class RptProjectStatus : System.Web.UI.Page
    {
        ProcessAccess prjData = new ProcessAccess();
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
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.SelectView();
                string type = this.Request.QueryString["Type"].ToString().Trim();
                //((Label)this.Master.FindControl("lblTitle")).Text = (type == "PrjStatus") ? "Project Status Report"
                //    : (type == "MProStatus") ? "Monthly Project Status"

                //    : (type == "Proturnover") ? "Project Remaining  Turnover"
                //    : (type == "GPNPCal") ? "GP NP Calculation"
                //    : (type == "GPNPSum") ? "GP NP Calculation Summary"

                //    : (type == "Prjwiseres") ? "Project Wise Resource" : "Collection Break Down";


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;


        }
        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string Date = "";
            switch (type)
            {
                case "PrjStatus":
                    this.chkconsolidate.Visible = true;
                    this.MultiView1.ActiveViewIndex = 0;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.GridColoumnChange();
                    break;

                case "CollBrkDown":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "MProStatus":
                    this.chkCash.Visible = true;
                    Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = Convert.ToDateTime("01" + Date.Substring(2)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.lblfrmDate.Text = "From :";
                    this.lbltoDate.Visible = true;
                    this.txttodate.Visible = true;
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "Proturnover":
                    this.chkconsolidate.Visible = true;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.MultiView1.ActiveViewIndex = 3;
                    break;
                case "GPNPCal":
                    this.MultiView1.ActiveViewIndex = 4;
                    this.lbltoDate.Visible = false;
                    this.txttodate.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    break;
                case "GPNPSum":
                    this.MultiView1.ActiveViewIndex = 5;
                    string Date1 = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = Convert.ToDateTime("01" + Date1.Substring(2)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Date1;
                    this.lblfrmDate.Text = "From :";
                    this.lbltoDate.Visible = true;
                    this.txttodate.Visible = true;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    break;


                case "Prjwiseres":
                    this.MultiView1.ActiveViewIndex = 6;
                    Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = Convert.ToDateTime("01" + Date.Substring(2)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.lblfrmDate.Text = "From :";
                    this.lbltoDate.Visible = true;
                    this.txttodate.Visible = true;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    break;



            }
        }

        private void GridColoumnChange()
        {
            string comcod = this.GetComeCode();
            this.grvPrjStatus.Columns[6].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Land Area </br>(Bigha)" : (comcod == "3306") ? "Land Area </br>(SFT/Katha)" : "Land Area </br>(Katha)";
            this.grvPrjStatus.Columns[7].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Nature </br> of Land" : (comcod == "3306") ? "Nature </br>of Product" : "Nature </br>of Land";
            this.grvPrjStatus.Columns[16].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Admin </br> Selling Exp." : (comcod == "3306") ? "Appt.  Price </br> Admin </br>Selling Exp." : "Const</br> Admin </br> Selling Exp.";

        }
        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "PrjStatus":
                    this.ShowPrjStatus();
                    break;
                case "CollBrkDown":
                    this.ShowCollBrkDown();
                    break;
                case "MProStatus":
                    this.ShowMonProStatus();
                    break;

                case "Proturnover":
                    this.ShowProjectturnover();
                    break;
                case "GPNPCal":
                    this.ShowGPNPCal();
                    break;
                case "GPNPSum":
                    this.ShowGPNPSumm();
                    break;

                case "Prjwiseres":
                    this.ShowProWiseResource();
                    break;

            }


        }
        private string PrjCallType()
        {
            string comcod = this.GetComeCode();
            string callType = "";
            switch (comcod)
            {
                //case "2305":
                case "3306":
                    callType = "RPTPROJECTSTATUSWIP";
                    break;
                default:
                    callType = "RPTPROJECTSTATUS";
                    break;
            }
            return callType;
        }


        private string CallType()
        {
            string comcod = this.GetComeCode();
            string calltype = "";
            switch (comcod)
            {
                case "2305":
                case "3305":
                case "3306":
                case "3309":
                case "3310":
                case "3311":
                    calltype = "RPTPROJECTSTATUSRUPAYAN";
                    break;
                default:
                    calltype = "RPTPROJECTSTATUS";
                    break;
            }
            return calltype;
        }






        private void ShowPrjStatus()
        {
            Session.Remove("tbPrjStatus");
            string comcod = this.GetComeCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string consolidate = (this.chkconsolidate.Checked) ? "consolidate" : "";
            string calltype = this.CallType();
            DataSet ds1 = prjData.GetTransInfo(comcod, "SP_REPORT_PROJECT_STATUS", calltype, frmdate, consolidate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.grvPrjStatus.DataSource = null;
                this.grvPrjStatus.DataBind();
                return;

            }
            ViewState["tblinterest"] = ds1.Tables[1];
            Session["tbPrjStatus"] = HiddenSameData(ds1.Tables[0]);
            this.grvPrjStatus.Columns[10].HeaderText = "Month Of " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMM yy");
            this.Data_Bind();
        }
        private void ShowCollBrkDown()
        {
            Session.Remove("tbPrjStatus");
            string comcod = this.GetComeCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = prjData.GetTransInfo(comcod, "SP_REPORT_PROJECT_STATUS", "RPTCOLLBREAKDOWN", frmdate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.grvCollBrkDown.DataSource = null;
                this.grvCollBrkDown.DataBind();
                return;

            }

            Session["tbPrjStatus"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }
        private void ShowMonProStatus()
        {
            Session.Remove("tbPrjStatus");
            this.PnlNote.Visible = true;
            string comcod = this.GetComeCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string toDate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string Cash = (this.chkCash.Checked) ? "Cash" : "";

            DataSet ds1 = prjData.GetTransInfo(comcod, "SP_REPORT_MIS", "RPTMONPROSTATUS", frmdate, toDate, Cash, "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvMonPorStatus.DataSource = null;
                this.gvMonPorStatus.DataBind();
                return;

            }

            Session["tbPrjStatus"] = ds1.Tables[0];
            ViewState["tblresdesc"] = ds1.Tables[1];

            this.Data_Bind();

            this.lblvalConsCost.Text = (ds1.Tables[2].Rows.Count == 0) ? "0" : Convert.ToDouble(ds1.Tables[2].Rows[0]["concost"]).ToString("#,##0;(#,##0); ");
            this.lblnonvalConsCost.Text = (ds1.Tables[2].Rows.Count == 0) ? "0" : Convert.ToDouble(ds1.Tables[2].Rows[0]["nconcost"]).ToString("#,##0;(#,##0); ");
            ds1.Dispose();
        }

        private void ShowProjectturnover()
        {
            Session.Remove("tbPrjStatus");
            string comcod = this.GetComeCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string consolidate = (this.chkconsolidate.Checked) ? "consolidate" : "";

            DataSet ds1 = prjData.GetTransInfo(comcod, "SP_REPORT_MIS", "RPTPRORMAINTURNOVER", frmdate, consolidate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.grvPrjturnover.DataSource = null;
                this.grvPrjturnover.DataBind();
                return;

            }

            Session["tbPrjStatus"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();


        }
        private void ShowGPNPCal()
        {
            Session.Remove("tbPrjStatus");
            string comcod = this.GetComeCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = prjData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GPNPCALCULATION", frmdate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvGPNPCal.DataSource = null;
                this.gvGPNPCal.DataBind();
                return;

            }

            Session["tbPrjStatus"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }
        private void ShowGPNPSumm()
        {
            Session.Remove("tbPrjStatus");
            string comcod = this.GetComeCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string trmdate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = prjData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "GPNPCALCULATIONSUMMARY", frmdate, trmdate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvGpNpSum.DataSource = null;
                this.gvGpNpSum.DataBind();
                return;

            }
            this.gvGpNpSum.Columns[2].HeaderText = "Upto " + "<br />" + Convert.ToDateTime(this.txtfromdate.Text).AddDays(-1).ToString("dd-MMM-yyyy");
            Session["tbPrjStatus"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }

        private void ShowProWiseResource()
        {

            Session.Remove("tbPrjStatus");
            string comcod = this.GetComeCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = prjData.GetTransInfo(comcod, "SP_REPORT_PROJECT_STATUS", "RPTPROJECTWISERES", "", frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvprowres.DataSource = null;
                this.gvprowres.DataBind();
                return;

            }
            // 
            Session["tbPrjStatus"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            string comcod = this.GetComeCode();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "PrjStatus":
                    this.grvPrjStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvPrjStatus.DataSource = (DataTable)Session["tbPrjStatus"];

                    this.grvPrjStatus.DataBind();
                    this.FooteCalculation();

                    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                    // ((HyperLink)this.grvPrjStatus.HeaderRow.FindControl("hlbtntbCdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                    Session["Report1"] = grvPrjStatus;
                    ((HyperLink)this.grvPrjStatus.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


                    break;
                case "CollBrkDown":
                    this.grvCollBrkDown.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvCollBrkDown.DataSource = (DataTable)Session["tbPrjStatus"];
                    this.grvCollBrkDown.DataBind();
                    this.FooteCalculation();
                    break;
                case "MProStatus":
                    DataTable dtpname = (DataTable)ViewState["tblresdesc"];
                    int j = 2;
                    for (int i = 0; i < dtpname.Rows.Count; i++)
                    {

                        this.gvMonPorStatus.Columns[j].HeaderText = dtpname.Rows[i]["resdesc"].ToString();
                        j++;
                        if (j == 16)
                            break;


                    }

                    this.gvMonPorStatus.DataSource = (DataTable)Session["tbPrjStatus"];
                    this.gvMonPorStatus.DataBind();
                    Session["Report1"] = gvMonPorStatus;
                    if (((DataTable)Session["tbPrjStatus"]).Rows.Count > 0)
                        ((HyperLink)this.gvMonPorStatus.HeaderRow.FindControl("hlbtnCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    this.FooteCalculation();
                    break;

                case "Proturnover":

                    this.grvPrjturnover.DataSource = (DataTable)Session["tbPrjStatus"];
                    this.grvPrjturnover.DataBind();

                    break;
                case "GPNPCal":
                    this.gvGPNPCal.DataSource = (DataTable)Session["tbPrjStatus"];
                    this.gvGPNPCal.DataBind();
                    this.FooteCalculation();
                    break;
                case "GPNPSum":
                    this.gvGpNpSum.DataSource = (DataTable)Session["tbPrjStatus"];
                    this.gvGpNpSum.DataBind();
                    break;


                case "Prjwiseres":
                    this.gvprowres.DataSource = (DataTable)Session["tbPrjStatus"];
                    this.gvprowres.DataBind();
                    Session["Report1"] = gvprowres;

                    this.FooteCalculation();
                    break;

            }


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string grp = "";
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string pactcode1;
            switch (type)
            {

                case "PrjStatus":

                    grp = dt1.Rows[0]["grp"].ToString();
                    pactcode1 = dt1.Rows[0]["pactcode1"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp && dt1.Rows[j]["pactcode1"].ToString() == pactcode1)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            pactcode1 = dt1.Rows[j]["pactcode1"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                            dt1.Rows[j]["pactdesc1"] = "";

                        }

                        else
                        {


                            if (dt1.Rows[j]["grp"].ToString() == grp)
                            {
                                dt1.Rows[j]["grpdesc"] = "";
                            }
                            if (dt1.Rows[j]["pactcode1"].ToString() == pactcode1)
                            {
                                dt1.Rows[j]["pactdesc1"] = "";
                            }

                            grp = dt1.Rows[j]["grp"].ToString();
                            pactcode1 = dt1.Rows[j]["pactcode1"].ToString();

                        }

                    }
                    break;



                case "Proturnover":

                    pactcode1 = dt1.Rows[0]["pactcode1"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode1"].ToString() == pactcode1)

                            dt1.Rows[j]["pactdesc1"] = "";
                        pactcode1 = dt1.Rows[j]["pactcode1"].ToString();


                    }
                    break;
                ////
                case "CollBrkDown":
                case "GPNPCal":
                case "GPNPSum":

                    grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }
                        grp = dt1.Rows[j]["grp"].ToString();


                    }

                    break;

                case "Prjwiseres":

                    string catmcode = dt1.Rows[0]["catmcode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["catmcode"].ToString() == catmcode)
                            dt1.Rows[j]["catmdesc"] = "";
                        catmcode = dt1.Rows[j]["catmcode"].ToString();
                    }
                    break;
            }

            return dt1;


        }
        private void FooteCalculation()
        {
            DataTable dt = (DataTable)Session["tbPrjStatus"];
            if (dt.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {



                case "PrjStatus":

                    DataView dv = dt.Copy().DefaultView;
                    dv.RowFilter = ("pactcode='BBBBAAAAAAAA'");
                    DataTable dt1 = dv.ToTable();
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFTSVal")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tsalamt)", "")) ?
                                    0.00 : dt1.Compute("sum(tsalamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFTmonSVal")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(msalamt)", "")) ?
                                    0.00 : dt1.Compute("sum(msalamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFTReSVal")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(trecamt)", "")) ?
                                            0.00 : dt1.Compute("sum(trecamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFNOI")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(noiamt)", "")) ?
                                            0.00 : dt1.Compute("sum(noiamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFRecamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(recamt)", "")) ?
                                            0.00 : dt1.Compute("sum(recamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFBRecSalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(balsalrec)", "")) ?
                                            0.00 : dt1.Compute("sum(balsalrec)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFExpAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(texpamt)", "")) ?
                                            0.00 : dt1.Compute("sum(texpamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFPAdvAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tpadvamt)", "")) ?
                                            0.00 : dt1.Compute("sum(tpadvamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFLCNFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tlcamt)", "")) ?
                                            0.00 : dt1.Compute("sum(tlcamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFOvmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tovamt)", "")) ?
                                            0.00 : dt1.Compute("sum(tovamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFIAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tbankinamt)", "")) ?
                                            0.00 : dt1.Compute("sum(tbankinamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFtExp")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tactamt)", "")) ?
                                            0.00 : dt1.Compute("sum(tactamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFLibAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tliamt)", "")) ?
                                            0.00 : dt1.Compute("sum(tliamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFLframt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(lfrmhoff)", "")) ?
                                            0.00 : dt1.Compute("sum(lfrmhoff)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFLtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ltohoff)", "")) ?
                                            0.00 : dt1.Compute("sum(ltohoff)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFRLamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(treloanamt)", "")) ?
                                            0.00 : dt1.Compute("sum(treloanamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "CollBrkDown":
                    ((Label)this.grvCollBrkDown.FooterRow.FindControl("lgvFTSVal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tsalamt)", "")) ?
                                    0.00 : dt.Compute("sum(tsalamt)", ""))).ToString("#,##0;(#,##0); ");



                    ((Label)this.grvCollBrkDown.FooterRow.FindControl("lgvFtclramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tclramt)", "")) ?
                                            0.00 : dt.Compute("sum(tclramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvCollBrkDown.FooterRow.FindControl("lgvFtretamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(retcheque)", "")) ?
                                            0.00 : dt.Compute("sum(retcheque)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvCollBrkDown.FooterRow.FindControl("lgvFtframt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(fcheque)", "")) ?
                                            0.00 : dt.Compute("sum(fcheque)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.grvCollBrkDown.FooterRow.FindControl("lgvFtpdamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pcheque)", "")) ?
                                           0.00 : dt.Compute("sum(pcheque)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.grvCollBrkDown.FooterRow.FindControl("lgvFtmat")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tmat)", "")) ?
                                           0.00 : dt.Compute("sum(tmat)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvCollBrkDown.FooterRow.FindControl("lgvFnoiamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(noiamt)", "")) ?
                                            0.00 : dt.Compute("sum(noiamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvCollBrkDown.FooterRow.FindControl("lgvFstdamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(stdamt)", "")) ?
                                            0.00 : dt.Compute("sum(stdamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvCollBrkDown.FooterRow.FindControl("lgvFcuamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cuamt)", "")) ?
                                            0.00 : dt.Compute("sum(cuamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvCollBrkDown.FooterRow.FindControl("lgvFgtotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gtotal)", "")) ?
                                            0.00 : dt.Compute("sum(gtotal)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "MProStatus":
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r1)", "")) ? 0.00 : dt.Compute("sum(r1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r2)", "")) ? 0.00 : dt.Compute("sum(r2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r3)", "")) ? 0.00 : dt.Compute("sum(r3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r4)", "")) ? 0.00 : dt.Compute("sum(r4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r5)", "")) ? 0.00 : dt.Compute("sum(r5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r6)", "")) ? 0.00 : dt.Compute("sum(r6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r7)", "")) ? 0.00 : dt.Compute("sum(r7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r8)", "")) ? 0.00 : dt.Compute("sum(r8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r9)", "")) ? 0.00 : dt.Compute("sum(r9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r10)", "")) ? 0.00 : dt.Compute("sum(r10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r1)", "")) ? 0.00 : dt.Compute("sum(r11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r12)", "")) ? 0.00 : dt.Compute("sum(r12)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r13)", "")) ? 0.00 : dt.Compute("sum(r13)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR14")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r14)", "")) ? 0.00 : dt.Compute("sum(r14)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFtoCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toramt)", "")) ? 0.00 : dt.Compute("sum(toramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFtoCollection")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tocollamt)", "")) ? 0.00 : dt.Compute("sum(tocollamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFnetposition")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netamt)", "")) ? 0.00 : dt.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "Proturnover":
                    break;
                case "GPNPCal":
                    DataView dv1 = dt.Copy().DefaultView;
                    dv1.RowFilter = ("pactcode='BBBBAAAAAAAA'");
                    DataTable dt2 = dv1.ToTable();
                    ((Label)this.gvGPNPCal.FooterRow.FindControl("lblgvFAdvSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(advsamt)", "")) ? 0.00 : dt2.Compute("sum(advsamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGPNPCal.FooterRow.FindControl("lblgvFSales")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(samt)", "")) ? 0.00 : dt2.Compute("sum(samt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGPNPCal.FooterRow.FindControl("lblgvFconstamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(constamt)", "")) ? 0.00 : dt2.Compute("sum(constamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGPNPCal.FooterRow.FindControl("lblgvFlandamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(landamt)", "")) ? 0.00 : dt2.Compute("sum(landamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGPNPCal.FooterRow.FindControl("lblgvFprodamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(prodamt)", "")) ? 0.00 : dt2.Compute("sum(prodamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGPNPCal.FooterRow.FindControl("lblgvFgpamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(gpamt)", "")) ? 0.00 : dt2.Compute("sum(gpamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvGPNPCal.FooterRow.FindControl("lblgvFhovamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(hovamt)", "")) ? 0.00 : dt2.Compute("sum(hovamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGPNPCal.FooterRow.FindControl("lblgvFtbankinamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(tbankinamt)", "")) ? 0.00 : dt2.Compute("sum(tbankinamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGPNPCal.FooterRow.FindControl("lblgvFtothamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(tothamt)", "")) ? 0.00 : dt2.Compute("sum(tothamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGPNPCal.FooterRow.FindControl("lblgvFTCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(tcostamt)", "")) ? 0.00 : dt2.Compute("sum(tcostamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGPNPCal.FooterRow.FindControl("lblgvFnpbnoi")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(npbnoi)", "")) ? 0.00 : dt2.Compute("sum(npbnoi)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGPNPCal.FooterRow.FindControl("lblgvFnoiamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(noiamt)", "")) ? 0.00 : dt2.Compute("sum(noiamt)", ""))).ToString("#,##0;(#,##0); ");

                    break;
                case "GPNPSum":

                    break;


                case "Prjwiseres":

                    ((HyperLink)this.gvprowres.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFiopnam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(iopnam)", "")) ? 0.00 : dt.Compute("sum(iopnam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFiaddam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(iaddam)", "")) ? 0.00 : dt.Compute("sum(iaddam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFiadjam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(iadjam)", "")) ? 0.00 : dt.Compute("sum(iadjam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFiclsam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(iclsam)", "")) ? 0.00 : dt.Compute("sum(iclsam)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFlopnam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lopnam)", "")) ? 0.00 : dt.Compute("sum(lopnam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFladdam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(laddam)", "")) ? 0.00 : dt.Compute("sum(laddam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFladjam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ladjam)", "")) ? 0.00 : dt.Compute("sum(ladjam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFlclsam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lclsam)", "")) ? 0.00 : dt.Compute("sum(lclsam)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFmopnam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mopnam)", "")) ? 0.00 : dt.Compute("sum(mopnam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFmaddam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(maddam)", "")) ? 0.00 : dt.Compute("sum(maddam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFmadjam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(madjam)", "")) ? 0.00 : dt.Compute("sum(madjam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFmclsam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mclsam)", "")) ? 0.00 : dt.Compute("sum(mclsam)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFsopnam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(sopnam)", "")) ? 0.00 : dt.Compute("sum(sopnam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFsaddam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(saddam)", "")) ? 0.00 : dt.Compute("sum(saddam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFsadjam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(sadjam)", "")) ? 0.00 : dt.Compute("sum(sadjam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFsclsam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(sclsam)", "")) ? 0.00 : dt.Compute("sum(sclsam)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFtopnam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(topnam)", "")) ? 0.00 : dt.Compute("sum(topnam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFtaddam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(taddam)", "")) ? 0.00 : dt.Compute("sum(taddam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFtadjam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tadjam)", "")) ? 0.00 : dt.Compute("sum(tadjam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvprowres.FooterRow.FindControl("lgvFtclsam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tclsam)", "")) ? 0.00 : dt.Compute("sum(tclsam)", ""))).ToString("#,##0;(#,##0); ");

                    break;



            }





        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "PrjStatus":
                    this.RptPrjStatus();
                    break;
                case "CollBrkDown":
                    this.RptCollBrkDown();
                    break;
                case "MProStatus":
                    this.PrintMonProStatus();
                    break;

                case "Proturnover":
                    this.PrintProTurnOver();
                    break;
                case "GPNPCal":
                    this.RptNpGpCollection();
                    break;
                case "GPNPSum":
                    this.RptNpGpCollSumm();
                    break;

                case "Prjwiseres":
                    this.RptProWisResource();
                    break;
            }


        }



        private void RptPrjStatus()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbPrjStatus"];
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.ProjectStatus>();

            if (ASTUtility.Left(comcod, 1) == "2")
            {
                //rptsaldperiod=new RealERPRPT.R_32_Mis.RptProjectStatusLand();
                Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptProjectStatusLand", list, null, null);

            }
            else
            {
                this.PrintRealProjectStatus();
                return;

                // rptsaldperiod = new RealERPRPT.R_32_Mis.RptProjectStatus();

            }

            DataTable dt1 = (DataTable)ViewState["tblinterest"];
            double tchrgedpro, tpaidpro, nettotal;
            tchrgedpro = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(interam)", "")) ? 0.00 : dt1.Compute("sum(interam)", "")));
            tpaidpro = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(inpaid)", "")) ? 0.00 : dt1.Compute("sum(inpaid)", "")));
            nettotal = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(netam)", "")) ? 0.00 : dt1.Compute("sum(netam)", "")));

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("date", "Upto  " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtthisonsal", "Month of " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMM yy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Project Status Report"));
            Rpt1.SetParameters(new ReportParameter("txtlandarea", (comcod == "3306") ? "Land Area(SFT/Katha)" : "Land Area(Katha)"));
            Rpt1.SetParameters(new ReportParameter("txtnofland", (comcod == "3306") ? "Nature of Product" : "Nature of Land"));
            Rpt1.SetParameters(new ReportParameter("txtCost", (comcod == "3306") ? "Appt. Price+Admin+Selling Exp." : "Const+Admin+Selling Exp."));
            Rpt1.SetParameters(new ReportParameter("txtonchrgetopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='410000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='410000000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtfinchrgetopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471800000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471800000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txthovrchrgetopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471900000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471900000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtlandchrgetopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='472000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='472000000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txttotalchrgetopro", tchrgedpro.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtonpaidtopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='410000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='410000000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtfinpaidtopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471800000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471800000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txthovrpaidtopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471900000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471900000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtlandpaidtopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='472000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='472000000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txttotalpaidtopro", tpaidpro.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtonnet", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='410000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='410000000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtfinnet", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471800000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471800000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txthovrnet", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471900000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471900000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtlandnet", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='472000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='472000000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtnettotal", nettotal.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptsaldperiod = new ReportDocument();
            // TextObject rptComp = rptsaldperiod.ReportDefinition.ReportObjects["CompName"] as TextObject;
            // rptComp.Text = comnam;
            // TextObject rptdate = rptsaldperiod.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            // rptdate.Text = "Upto  " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM yyyy");
            // TextObject txtthisonsal = rptsaldperiod.ReportDefinition.ReportObjects["txtthisonsal"] as TextObject;
            // txtthisonsal.Text = "Month of "+Convert.ToDateTime(this.txtfromdate.Text).ToString("MMM yy");

            // DataTable dt1 = (DataTable)ViewState["tblinterest"];
            // double tchrgedpro, tpaidpro, nettotal;
            // tchrgedpro = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(interam)", "")) ? 0.00 : dt1.Compute("sum(interam)", "")));
            // tpaidpro = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(inpaid)", "")) ? 0.00 : dt1.Compute("sum(inpaid)", "")));
            // nettotal = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(netam)", "")) ? 0.00 : dt1.Compute("sum(netam)", "")));
            //// ViewState["tblinterest"] = ds1.Tables[1];

            // //(((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["rsirunit"]

            //     TextObject txtonchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txtonchrgetopro"] as TextObject;
            //     txtonchrgetopro.Text =(dt1.Rows.Count==0)?"":dt1.Select("pactcode='410000000000'").Length>0?Convert.ToDouble(dt1.Select("pactcode='410000000000'")[0]["interam"]).ToString("#,##0;(#,##0);"):"";

            //    TextObject txtfinchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txtfinchrgetopro"] as TextObject;
            //    txtfinchrgetopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471800000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471800000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : "";

            //     TextObject txthovrchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txthovrchrgetopro"] as TextObject;
            //     txthovrchrgetopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471900000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471900000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : "";

            //     TextObject txtlandchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txtlandchrgetopro"] as TextObject;
            //     txtlandchrgetopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='472000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='472000000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : "";

            //     TextObject txttotalchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txttotalchrgetopro"] as TextObject;
            //     txttotalchrgetopro.Text = tchrgedpro.ToString("#,##0;(#,##0); ");

            //     TextObject txtonpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txtonpaidtopro"] as TextObject;
            //     txtonpaidtopro.Text = (dt1.Rows.Count == 0) ? "" :dt1.Select("pactcode='410000000000'").Length>0?Convert.ToDouble(dt1.Select("pactcode='410000000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);"):"";
            //     TextObject txtfinpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txtfinpaidtopro"] as TextObject;
            //     txtfinpaidtopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471800000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471800000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : "";
            //     TextObject txthovrpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txthovrpaidtopro"] as TextObject;
            //     txthovrpaidtopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471900000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471900000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : "";
            //     TextObject txtlandpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txtlandpaidtopro"] as TextObject;
            //     txtlandpaidtopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='472000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='472000000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : "";
            //     TextObject txttotalpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txttotalpaidtopro"] as TextObject;
            //     txttotalpaidtopro.Text = tpaidpro.ToString("#,##0;(#,##0); ");

            //     TextObject txtonnet = rptsaldperiod.ReportDefinition.ReportObjects["txtonnet"] as TextObject;
            //     txtonnet.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='410000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='410000000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : "";
            //     TextObject txtfinneto = rptsaldperiod.ReportDefinition.ReportObjects["txtfinnet"] as TextObject;
            //     txtfinneto.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471800000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471800000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : "";
            //     TextObject txthovrnet = rptsaldperiod.ReportDefinition.ReportObjects["txthovrnet"] as TextObject;
            //     txthovrnet.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471900000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471900000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : "";
            //     TextObject txtlandnet = rptsaldperiod.ReportDefinition.ReportObjects["txtlandnet"] as TextObject;
            //     txtlandnet.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='472000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='472000000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : "";
            //     TextObject txtnettotal = rptsaldperiod.ReportDefinition.ReportObjects["txtnettotal"] as TextObject;
            //     txtnettotal.Text = nettotal.ToString("#,##0;(#,##0); ");

            // //string calltype = this.PrjCallType();

            // //TextObject rptCost = rptsaldperiod.ReportDefinition.ReportObjects["txtCost"] as TextObject;
            // //rptCost.Text = (ASTUtility.Left(comcod, 1) == "4") ? "Const+Admin+Selling Expen." : "Apt. Purchase+Admin+Selling";

            // TextObject txtuserinfo = rptsaldperiod.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            // txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            // rptsaldperiod.SetDataSource(dt);


            // //string comcod = hst["comcod"].ToString();

            // if (ConstantInfo.LogStatus == true)
            // {
            //     string eventtype = "Sales During the Peroid";
            //     string eventdesc = "Print Report";
            //     string eventdesc2 = "";
            //     bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            // }
            // string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            // rptsaldperiod.SetParameterValue("ComLogo", ComLogo);
            // Session["Report1"] = rptsaldperiod;
            // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintRealProjectStatus()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbPrjStatus"];
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");

            DataTable dt1 = (DataTable)ViewState["tblinterest"];
            double tchrgedpro, tpaidpro, nettotal;
            tchrgedpro = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(interam)", "")) ? 0.00 : dt1.Compute("sum(interam)", "")));
            tpaidpro = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(inpaid)", "")) ? 0.00 : dt1.Compute("sum(inpaid)", "")));
            nettotal = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(netam)", "")) ? 0.00 : dt1.Compute("sum(netam)", "")));

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.ProjectStatus>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptProjectStatus", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("date", "Upto  " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtthisonsal", "Month of " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMM yy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Project Status Report"));
            Rpt1.SetParameters(new ReportParameter("txtlandarea", (comcod == "3306") ? "Land Area(SFT/Katha)" : "Land Area(Katha)"));
            Rpt1.SetParameters(new ReportParameter("txtnofland", (comcod == "3306") ? "Nature of Product" : "Nature of Land"));
            Rpt1.SetParameters(new ReportParameter("txtCost", (comcod == "3306") ? "Appt. Price+Admin+Selling Exp." : "Const+Admin+Selling Exp."));
            Rpt1.SetParameters(new ReportParameter("txtonchrgetopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='410000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='410000000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtfinchrgetopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471800000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471800000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txthovrchrgetopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471900000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471900000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtlandchrgetopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='472000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='472000000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txttotalchrgetopro", tchrgedpro.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtonpaidtopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='410000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='410000000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtfinpaidtopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471800000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471800000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txthovrpaidtopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471900000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471900000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtlandpaidtopro", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='472000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='472000000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txttotalpaidtopro", tpaidpro.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtonnet", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='410000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='410000000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtfinnet", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471800000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471800000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txthovrnet", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471900000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471900000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtlandnet", (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='472000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='472000000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : ""));
            Rpt1.SetParameters(new ReportParameter("txtnettotal", nettotal.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptsaldperiod = new ReportDocument();
            //rptsaldperiod = new RealERPRPT.R_32_Mis.RptProjectStatus();
            //TextObject rptComp = rptsaldperiod.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptComp.Text = comnam;
            //TextObject rptdate = rptsaldperiod.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptdate.Text = "Upto  " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM yyyy");
            //TextObject txtthisonsal = rptsaldperiod.ReportDefinition.ReportObjects["txtthisonsal"] as TextObject;
            //txtthisonsal.Text = "Month of " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMM yy");

            //DataTable dt1 = (DataTable)ViewState["tblinterest"];
            //double tchrgedpro, tpaidpro, nettotal;
            //tchrgedpro = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(interam)", "")) ? 0.00 : dt1.Compute("sum(interam)", "")));
            //tpaidpro = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(inpaid)", "")) ? 0.00 : dt1.Compute("sum(inpaid)", "")));
            //nettotal = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(netam)", "")) ? 0.00 : dt1.Compute("sum(netam)", "")));
            //// ViewState["tblinterest"] = ds1.Tables[1];

            ////(((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["rsirunit"]

            //TextObject txtlandarea = rptsaldperiod.ReportDefinition.ReportObjects["txtlandarea"] as TextObject;
            //txtlandarea.Text = (comcod == "3306") ? "Land Area(SFT/Katha)" : "Land Area(Katha)";

            //TextObject txtnofland = rptsaldperiod.ReportDefinition.ReportObjects["txtnofland"] as TextObject;
            //txtnofland.Text = (comcod == "3306") ? "Nature of Product" : "Nature of Land";

            //TextObject txtCost = rptsaldperiod.ReportDefinition.ReportObjects["txtCost"] as TextObject;
            //txtCost.Text = (comcod == "3306") ? "Appt. Price+Admin+Selling Exp." : "Const+Admin+Selling Exp.";

            //TextObject txtonchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txtonchrgetopro"] as TextObject;
            //txtonchrgetopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='410000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='410000000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : "";

            //TextObject txtfinchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txtfinchrgetopro"] as TextObject;
            //txtfinchrgetopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471800000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471800000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : "";

            //TextObject txthovrchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txthovrchrgetopro"] as TextObject;
            //txthovrchrgetopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471900000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471900000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : "";

            //TextObject txtlandchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txtlandchrgetopro"] as TextObject;
            //txtlandchrgetopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='472000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='472000000000'")[0]["interam"]).ToString("#,##0;(#,##0);") : "";

            //TextObject txttotalchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txttotalchrgetopro"] as TextObject;
            //txttotalchrgetopro.Text = tchrgedpro.ToString("#,##0;(#,##0); ");


            //TextObject txtonpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txtonpaidtopro"] as TextObject;
            //txtonpaidtopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='410000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='410000000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : "";
            //TextObject txtfinpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txtfinpaidtopro"] as TextObject;
            //txtfinpaidtopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471800000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471800000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : "";
            //TextObject txthovrpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txthovrpaidtopro"] as TextObject;
            //txthovrpaidtopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471900000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471900000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : "";
            //TextObject txtlandpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txtlandpaidtopro"] as TextObject;
            //txtlandpaidtopro.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='472000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='472000000000'")[0]["inpaid"]).ToString("#,##0;(#,##0);") : "";
            //TextObject txttotalpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txttotalpaidtopro"] as TextObject;
            //txttotalpaidtopro.Text = tpaidpro.ToString("#,##0;(#,##0); ");



            //TextObject txtonnet = rptsaldperiod.ReportDefinition.ReportObjects["txtonnet"] as TextObject;
            //txtonnet.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='410000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='410000000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : "";
            //TextObject txtfinneto = rptsaldperiod.ReportDefinition.ReportObjects["txtfinnet"] as TextObject;
            //txtfinneto.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471800000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471800000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : "";
            //TextObject txthovrnet = rptsaldperiod.ReportDefinition.ReportObjects["txthovrnet"] as TextObject;
            //txthovrnet.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='471900000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='471900000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : "";
            //TextObject txtlandnet = rptsaldperiod.ReportDefinition.ReportObjects["txtlandnet"] as TextObject;
            //txtlandnet.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("pactcode='472000000000'").Length > 0 ? Convert.ToDouble(dt1.Select("pactcode='472000000000'")[0]["netam"]).ToString("#,##0;(#,##0);") : "";
            //TextObject txtnettotal = rptsaldperiod.ReportDefinition.ReportObjects["txtnettotal"] as TextObject;
            //txtnettotal.Text = nettotal.ToString("#,##0;(#,##0); ");
            //TextObject txtuserinfo = rptsaldperiod.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsaldperiod.SetDataSource(dt);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Sales During the Peroid";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptsaldperiod.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptsaldperiod;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptCollBrkDown()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt1 = (DataTable)Session["tbPrjStatus"];
            if (dt1.Rows.Count == 0)
                return;

            var lst = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.CollectionBrackDown>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptCollBrkDown", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtdate", "As on Date: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Collection Break Down"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tbPrjStatus"];
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //ReportDocument rptsaldperiod = new RealERPRPT.R_32_Mis.RptCollBrkDown();
            //TextObject rptComp = rptsaldperiod.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptComp.Text = comnam;
            //TextObject rptdate = rptsaldperiod.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptdate.Text = "As on Date: " + frmdate;
            //TextObject txtuserinfo = rptsaldperiod.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsaldperiod.SetDataSource(dt);
            //string comcod = hst["comcod"].ToString();

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Sales During the Peroid";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptsaldperiod.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptsaldperiod;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintMonProStatus()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbPrjStatus"];

            var list = dt.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.MontWiseProjectStatus>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptMonProjectStatus", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Month Wise Project Status"));
            Rpt1.SetParameters(new ReportParameter("date", "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
            DataTable dtrname = (DataTable)ViewState["tblresdesc"];
            int j = 1;
            for (int i = 0; i < dtrname.Rows.Count; i++)
            {
                Rpt1.SetParameters(new ReportParameter("txtR" + j.ToString(), dtrname.Rows[i]["resdesc"].ToString()));
                j++;
                if (j == 21)
                    break;
            }
            Rpt1.SetParameters(new ReportParameter("txtconcost", this.lblvalConsCost.Text));
            Rpt1.SetParameters(new ReportParameter("txtnonconcost", this.lblnonvalConsCost.Text));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            // ReportDocument rptmprost = new RealERPRPT.R_32_Mis.RptMonProjectStatus();
            // TextObject rptComp = rptmprost.ReportDefinition.ReportObjects["CompName"] as TextObject;
            // rptComp.Text = comnam;
            // TextObject rptdate = rptmprost.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            // rptdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy")+" To "+Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")+")";

            //DataTable dtrname = (DataTable)ViewState["tblresdesc"];
            //int j = 1;
            // for (int i = 0; i < dtrname.Rows.Count; i++)
            // {
            //     TextObject rpttxth = rptmprost.ReportDefinition.ReportObjects["txtR" + j.ToString()] as TextObject;
            //     rpttxth.Text = dtrname.Rows[i]["resdesc"].ToString();
            //     j++;
            //     if (j == 21)
            //         break;                    
            // }
            // TextObject txtconcost = rptmprost.ReportDefinition.ReportObjects["txtconcost"] as TextObject;
            // txtconcost.Text = this.lblvalConsCost.Text;

            // TextObject txtnonconcost = rptmprost.ReportDefinition.ReportObjects["txtnonconcost"] as TextObject;
            // txtnonconcost.Text = this.lblnonvalConsCost.Text;

            // TextObject txtuserinfo = rptmprost.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            // txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            // rptmprost.SetDataSource(dt);
            // Session["Report1"] = rptmprost;
            // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";   
        }

        private void PrintProTurnOver()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbPrjStatus"];
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptsaldperiod = new RealERPRPT.R_32_Mis.RptProTurnOver();
            TextObject rptComp = rptsaldperiod.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptComp.Text = comnam;
            TextObject rptdate = rptsaldperiod.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            rptdate.Text = "As on Date: " + frmdate;
            TextObject txtuserinfo = rptsaldperiod.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptsaldperiod.SetDataSource(dt);

            Session["Report1"] = rptsaldperiod;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void RptNpGpCollection()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbPrjStatus"];
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string comcod = this.GetComeCode();
            string comlogo = new Uri(Server.MapPath(@"~\image\logo" + comcod + ".jpg")).AbsoluteUri;
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            var lst = dt.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.GpNpCalc>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptGpNpCalc", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", comlogo));

            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("title", "GP NP Calculation"));
            Rpt1.SetParameters(new ReportParameter("date", "As on Date: " + frmdate));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptsaldperiod = new RealERPRPT.R_32_Mis.RptGpNpCal();
            //TextObject rptComp = rptsaldperiod.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptComp.Text = comnam;
            //TextObject rptdate = rptsaldperiod.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rptdate.Text = "As on Date: " + frmdate;
            //TextObject txtuserinfo = rptsaldperiod.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsaldperiod.SetDataSource(dt);
            //string comcod = hst["comcod"].ToString();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Gp Np Calculation";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptsaldperiod.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptsaldperiod;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptNpGpCollSumm()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbPrjStatus"];
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string trmdate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptsaldperiod = new RealERPRPT.R_32_Mis.RptGpNpSumm();
            TextObject rptComp = rptsaldperiod.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptComp.Text = comnam;
            TextObject rptdate = rptsaldperiod.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            rptdate.Text = "From: " + frmdate + "To: " + trmdate;

            TextObject rptuDate = rptsaldperiod.ReportDefinition.ReportObjects["uDate"] as TextObject;
            rptuDate.Text = "Upto " + "<br />" + Convert.ToDateTime(this.txtfromdate.Text).AddDays(-1).ToString("dd-MMM-yyyy");


            TextObject txtuserinfo = rptsaldperiod.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptsaldperiod.SetDataSource(dt);
            string comcod = hst["comcod"].ToString();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Gp Np Calculation";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptsaldperiod.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptsaldperiod;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void RptProWisResource()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string FTDate = "( From " + frmdate + " To " + todate + " )";



            LocalReport Rpt1 = new LocalReport();
            DataTable Pfinfo = (DataTable)Session["tbPrjStatus"];

            var lst = Pfinfo.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.ProjectWisRes>();



            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptProjectWisResource", lst, null, null);




            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("FTDate", FTDate));
            Rpt1.SetParameters(new ReportParameter("RptTitale", "Project Wise Resource"));
            Rpt1.SetParameters(new ReportParameter("PFooter", printFooter));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void grvPrjStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvPrjStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void grvCollBrkDown_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvCollBrkDown.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void grvCollBrkDown_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string mCOMCOD = comcod;
            string mPACTCODE = ((Label)e.Row.FindControl("Actcode")).Text;
            string mPACTDESC = ((Label)e.Row.FindControl("Actdesc")).Text;
            string mTRNDAT1 = this.txtfromdate.Text;
            //------------------------------//////
            Label actcode = (Label)e.Row.FindControl("lblgvcode");
            HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesc");


            hlink1.NavigateUrl = "RptProjectCollBrkDown.aspx?Type=PrjCol&pactcode=" + mPACTCODE + "&Date1=" + mTRNDAT1;

        }
        protected void grvPrjStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
                Label lgvTSVal = (Label)e.Row.FindControl("lgvTSVal");
                Label lgvTmonSVal = (Label)e.Row.FindControl("lgvTmonSVal");
                Label lgvTReSVal = (Label)e.Row.FindControl("lgvTReSVal");
                Label lgvNOI = (Label)e.Row.FindControl("lgvNOI");
                Label lgvRecamt = (Label)e.Row.FindControl("lgvRecamt");
                Label lgvBRecSalamt = (Label)e.Row.FindControl("lgvBRecSalamt");
                Label lgvExpAmt = (Label)e.Row.FindControl("lgvExpAmt");
                Label lgvPAdvAmt = (Label)e.Row.FindControl("lgvPAdvAmt");
                Label lgvLCNFAmt = (Label)e.Row.FindControl("lgvLCNFAmt");
                Label lgvOvmt = (Label)e.Row.FindControl("lgvOvmt");
                Label lgvIAmt = (Label)e.Row.FindControl("lgvIAmt");
                Label lgvtExp = (Label)e.Row.FindControl("lgvtExp");
                Label lgvLibAmt = (Label)e.Row.FindControl("lgvLibAmt");
                Label lgvLframt = (Label)e.Row.FindControl("lgvLframt");
                Label lgvLtoamt = (Label)e.Row.FindControl("lgvLtoamt");
                Label lgvRLamt = (Label)e.Row.FindControl("lgvRLamt");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();



                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                string mCOMCOD = comcod;
                string mPACTCODE = ((Label)e.Row.FindControl("lblActcode")).Text;
                string mTRNDAT1 = this.txtfromdate.Text;
                //------------------------------//////
                Label actcode = (Label)e.Row.FindControl("lblgvcode");
                HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesc");
                hlink1.NavigateUrl = "RptProjectCollBrkDown.aspx?Type=IndPrjStDet&pactcode=" + mPACTCODE + "&Date1=" + mTRNDAT1;

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    hlink1.Font.Bold = true;
                    lgvTSVal.Font.Bold = true;
                    lgvTmonSVal.Font.Bold = true;
                    lgvTReSVal.Font.Bold = true;
                    lgvNOI.Font.Bold = true;
                    lgvRecamt.Font.Bold = true;
                    lgvBRecSalamt.Font.Bold = true;
                    lgvExpAmt.Font.Bold = true;
                    lgvPAdvAmt.Font.Bold = true;
                    lgvLCNFAmt.Font.Bold = true;
                    lgvOvmt.Font.Bold = true;
                    lgvIAmt.Font.Bold = true;
                    lgvtExp.Font.Bold = true;
                    lgvLibAmt.Font.Bold = true;
                    lgvLframt.Font.Bold = true;
                    lgvLtoamt.Font.Bold = true;
                    lgvRLamt.Font.Bold = true;

                    hlink1.Style.Add("text-align", "right");
                }
                else
                {
                    hlink1.Style.Add("color", "blue");
                }

            }



        }

        protected void grvPrjturnover_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvProjectdesctvr = (Label)e.Row.FindControl("lgvProjectdesctvr");
                Label lgvOrsalam = (Label)e.Row.FindControl("lgvOrsalam");
                Label lgvrsalam = (Label)e.Row.FindControl("lgvrsalam");
                Label lgvsoldam = (Label)e.Row.FindControl("lgvsoldam");
                Label lgvusoldam = (Label)e.Row.FindControl("lgvusoldam");
                Label lgvreceivedam = (Label)e.Row.FindControl("lgvreceivedam");
                Label lgvreceiable = (Label)e.Row.FindControl("lgvreceiable");
                Label lgvavailable = (Label)e.Row.FindControl("lgvavailable");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lgvProjectdesctvr.Font.Bold = true;
                    lgvOrsalam.Font.Bold = true;
                    lgvrsalam.Font.Bold = true;
                    lgvsoldam.Font.Bold = true;
                    lgvusoldam.Font.Bold = true;
                    lgvreceivedam.Font.Bold = true;
                    lgvreceiable.Font.Bold = true;
                    lgvavailable.Font.Bold = true;
                    lgvProjectdesctvr.Style.Add("text-align", "right");
                }

            }

        }



        protected void gvGPNPCal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblgvPDesc = (Label)e.Row.FindControl("lblgvPDesc");
                Label lblgvAdvSal = (Label)e.Row.FindControl("lblgvAdvSal");
                Label lblgvSales = (Label)e.Row.FindControl("lblgvSales");
                Label lblgvconstamt = (Label)e.Row.FindControl("lblgvconstamt");
                Label lblgvlandamt = (Label)e.Row.FindControl("lblgvlandamt");
                Label lblgvprodamt = (Label)e.Row.FindControl("lblgvprodamt");
                Label lblgvgpamt = (Label)e.Row.FindControl("lblgvgpamt");
                Label lblgvhovamt = (Label)e.Row.FindControl("lblgvhovamt");
                Label lblgvtbankinamt = (Label)e.Row.FindControl("lblgvtbankinamt");
                Label lblgvtothamt = (Label)e.Row.FindControl("lblgvtothamt");
                Label lblgvTCost = (Label)e.Row.FindControl("lblgvTCost");
                Label lblgvnpbnoi = (Label)e.Row.FindControl("lblgvnpbnoi");
                Label lblgvnoiamt = (Label)e.Row.FindControl("lblgvnoiamt");
                Label lblgvnpnoiper = (Label)e.Row.FindControl("lblgvnpnoiper");
                Label lblgvDesc = (Label)e.Row.FindControl("lblgvDesc");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lblgvPDesc.Font.Bold = true;
                    lblgvAdvSal.Font.Bold = true;
                    lblgvSales.Font.Bold = true;
                    lblgvconstamt.Font.Bold = true;
                    lblgvlandamt.Font.Bold = true;
                    lblgvprodamt.Font.Bold = true;
                    lblgvgpamt.Font.Bold = true;
                    lblgvhovamt.Font.Bold = true;
                    lblgvtbankinamt.Font.Bold = true;
                    lblgvtothamt.Font.Bold = true;
                    lblgvTCost.Font.Bold = true;
                    lblgvnpbnoi.Font.Bold = true;
                    lblgvnoiamt.Font.Bold = true;
                    lblgvnpnoiper.Font.Bold = true;
                    lblgvDesc.Font.Bold = true;

                    lblgvPDesc.Style.Add("text-align", "right");
                }

            }
        }

        protected void gvGpNpSum_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblgvPDesc = (Label)e.Row.FindControl("lblgvPDesc");
                Label lblgvopamt = (Label)e.Row.FindControl("lblgvopamt");
                Label lblgvcuramt = (Label)e.Row.FindControl("lblgvcuramt");
                Label lblgvtamt = (Label)e.Row.FindControl("lblgvtamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 10) == "BBBBAAAAAA")
                {

                    lblgvPDesc.Font.Bold = true;
                    lblgvopamt.Font.Bold = true;
                    lblgvcuramt.Font.Bold = true;
                    lblgvtamt.Font.Bold = true;

                    lblgvPDesc.Style.Add("text-align", "right");
                }

            }
        }
        protected void gvprowres_RowCreated(object sender, GridViewRowEventArgs e)
        {

            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 3;
                cell01.Attributes["style"] = "font-weight:bold; font-size:14px; width:30px; background-color:#5CB85C; color:white; ";



                TableCell cell04 = new TableCell();
                cell04.Text = "Initial & Professional Cost";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 4;
                cell04.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:#5CB85C; color:white;";



                TableCell cell05 = new TableCell();
                cell05.Text = "Labour Cost";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 4;
                cell05.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:pink; color:white;";



                TableCell cell06 = new TableCell();
                cell06.Text = "Material Cost";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 4;
                cell06.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:#5CB85C; color:white; ";


                TableCell cell07 = new TableCell();
                cell07.Text = "Site Overhead Cost";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.ColumnSpan = 4;
                cell07.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:pink; color:white;";




                TableCell cell08 = new TableCell();
                cell08.Text = " Total Cost";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.ColumnSpan = 4;
                cell08.Attributes["style"] = "font-weight:bold; font-size:14px; background-color:#5CB85C; color:white; ";








                gvrow.Cells.Add(cell01);

                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvrow.Cells.Add(cell06);
                gvrow.Cells.Add(cell07);
                gvrow.Cells.Add(cell08);
                gvprowres.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
        protected void gvprowres_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDescpwres");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                if (code == "")
                {
                    return;
                }

                // hlink1.NavigateUrl = "~/F_32_Mis/ProjTrialBalanc.aspx?Type=PrjTrailBal&prjcode=" + code;

            }

        }
    }
}