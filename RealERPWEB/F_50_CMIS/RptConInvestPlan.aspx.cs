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
namespace RealERPWEB.F_50_CMIS
{
    public partial class RptConInvestPlan : System.Web.UI.Page
    {

        ProcessAccess MISData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "InvestPlan") ? "INVESTMENT PLAN - ALL PROJECTS"
                    : (this.Request.QueryString["Type"].ToString().Trim() == "PrjStatus") ? "PROJECT STATUS REPORT" : "MONTHLY PROJECT STATUS";
                //this.lblHtitle.Text = (this.Request.QueryString["Type"].ToString().Trim() == "InvestPlan") ? "INVESTMENT PLAN - ALL PROJECTS"
                //    : (this.Request.QueryString["Type"].ToString().Trim() == "PrjStatus") ? "PROJECT STATUS REPORT" : "MONTHLY PROJECT STATUS";
                this.SectionView();


            }
        }

        private void SectionView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {


                case "InvestPlan":
                    this.lblToDate.Visible = false;
                    this.txttodate.Visible = false;
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "PrjStatus":
                    this.lblToDate.Visible = false;
                    this.txttodate.Visible = false;
                    this.lblTkInCrore.Visible = false;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "MProStatus":
                    this.chkCash.Visible = true;
                    string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = Convert.ToDateTime("01" + Date.Substring(2)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.lblfrmdate.Text = "From";
                    this.chkconsolidate.Visible = false;
                    this.lblTkInCrore.Visible = false;
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {


                case "InvestPlan":
                    this.ShowInvestment();
                    break;

                case "PrjStatus":
                    this.ShowPrjStatus();
                    break;
                case "MProStatus":
                    this.ShowMonProStatus();
                    break;
            }
        }

        private void ShowInvestment()
        {
            ViewState.Remove("tblmasterbgd");
            string comcod = this.GetCompCode();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string consolidate = (this.chkconsolidate.Checked) ? "consolidate" : "";
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MISCON", "RPTINVESTPLAN", txtfromdate, consolidate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvInvest.DataSource = null;
                this.gvInvest.DataBind();
                return;
            }
            this.PanelNote.Visible = true;
            this.txtColl.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["percnt"]).ToString("#,##0.00;(#,##0.00); ") + " %";
            this.txtNp.Text = Convert.ToDouble(ds1.Tables[1].Rows[1]["percnt"]).ToString("#,##0.00;(#,##0.00); ") + " %";
            this.txtBgd.Text = Convert.ToDouble(ds1.Tables[1].Rows[2]["percnt"]).ToString("#,##0.00;(#,##0.00); ") + " %";

            ViewState["tblmasterbgd"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        private string PrjCallType()
        {
            string comcod = this.GetCompCode();
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
        private void ShowPrjStatus()
        {
            ViewState.Remove("tblmasterbgd");
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string consolidate = (this.chkconsolidate.Checked) ? "consolidate" : "";
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MISCON", "RPTPROJECTSTATUS", date, consolidate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.grvPrjStatus.DataSource = null;
                this.grvPrjStatus.DataBind();
                return;

            }
            ViewState["tblinterest"] = ds1.Tables[1];
            ViewState["tblmasterbgd"] = HiddenSameData(ds1.Tables[0]);
            this.grvPrjStatus.Columns[10].HeaderText = "Month Of " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMM yy");
            this.Data_Bind();
        }

        private void ShowMonProStatus()
        {
            ViewState.Remove("tblmasterbgd");

            this.PnlNote.Visible = false;
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string toDate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string Cash = (this.chkCash.Checked) ? "Cash" : "";

            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MISCON", "RPTMONPROSTATUS", frmdate, toDate, Cash, "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvMonPorStatus.DataSource = null;
                this.gvMonPorStatus.DataBind();
                return;

            }

            ViewState["tblmasterbgd"] = this.HiddenSameData(ds1.Tables[0]);
            ViewState["tblresdesc"] = ds1.Tables[1];

            this.Data_Bind();
            this.lblvalConsCost.Text = (ds1.Tables[2].Rows.Count == 0) ? "0" : Convert.ToDouble(ds1.Tables[2].Rows[0]["concost"]).ToString("#,##0;(#,##0); ");
            this.lblnonvalConsCost.Text = (ds1.Tables[2].Rows.Count == 0) ? "0" : Convert.ToDouble(ds1.Tables[2].Rows[0]["nconcost"]).ToString("#,##0;(#,##0); ");
            ds1.Dispose();


        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string comcod = dt1.Rows[0]["comcod"].ToString();
            string grp = "";

            switch (type)
            {
                case "InvestPlan":

                    grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["comcod"].ToString() == comcod && dt1.Rows[j]["grp"].ToString() == grp)
                        {

                            dt1.Rows[j]["comnam"] = "";
                            dt1.Rows[j]["grpdesc"] = "";

                        }

                        else
                        {

                            if (dt1.Rows[j]["comcod"].ToString() == comcod)
                                dt1.Rows[j]["comnam"] = "";

                            if (dt1.Rows[j]["grp"].ToString() == grp)
                                dt1.Rows[j]["grpdesc"] = "";
                        }

                        comcod = dt1.Rows[j]["comcod"].ToString();
                        grp = dt1.Rows[j]["grp"].ToString();

                    }

                    break;

                case "PrjStatus":
                    grp = dt1.Rows[0]["grp"].ToString();
                    string pactcode1 = dt1.Rows[0]["pactcode1"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["comcod"].ToString() == comcod && dt1.Rows[j]["grp"].ToString() == grp && dt1.Rows[j]["pactcode1"].ToString() == pactcode1)
                        {
                            dt1.Rows[j]["comnam"] = "";
                            dt1.Rows[j]["grpdesc"] = "";
                            dt1.Rows[j]["pactdesc1"] = "";

                        }

                        else
                        {
                            if (dt1.Rows[j]["comcod"].ToString() == comcod)
                                dt1.Rows[j]["comnam"] = "";


                            if (dt1.Rows[j]["grp"].ToString() == grp)

                                dt1.Rows[j]["grpdesc"] = "";

                            if (dt1.Rows[j]["pactcode1"].ToString() == pactcode1)

                                dt1.Rows[j]["pactdesc1"] = "";

                        }

                        comcod = dt1.Rows[j]["comcod"].ToString();
                        grp = dt1.Rows[j]["grp"].ToString();
                        pactcode1 = dt1.Rows[j]["pactcode1"].ToString();

                    }
                    break;

                case "MProStatus":
                    string actcode = dt1.Rows[0]["actcode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["comcod"].ToString() == comcod && dt1.Rows[j]["actcode"].ToString() == actcode)
                        {
                            dt1.Rows[j]["comnam"] = "";
                            dt1.Rows[j]["actdesc"] = "";
                        }

                        else
                        {
                            if (dt1.Rows[j]["comcod"].ToString() == comcod)
                                dt1.Rows[j]["comnam"] = "";
                            if (dt1.Rows[j]["actcode"].ToString() == actcode)
                                dt1.Rows[j]["actdesc"] = "";

                        }

                        comcod = dt1.Rows[j]["comcod"].ToString();
                        actcode = dt1.Rows[j]["actcode"].ToString();
                    }


                    break;


            }


            return dt1;

        }

        private void Data_Bind()
        {

            DataTable dt1 = ((DataTable)ViewState["tblmasterbgd"]).Copy();
            if (dt1.Rows.Count == 0)
                return;
            string Type = this.Request.QueryString["Type"].ToString().Trim();


            switch (Type)
            {

                case "InvestPlan":

                    this.gvInvest.DataSource = dt1;
                    this.gvInvest.DataBind();

                    DataView dv1 = dt1.Copy().DefaultView;
                    dv1.RowFilter = ("actcode = 'CCCCAAAAAAAA'");
                    DataTable dts = dv1.ToTable();

                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFToSalVal")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(tosalval)", "")) ? 0.00 : dts.Compute("sum(tosalval)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFSaleAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(salamt)", "")) ? 0.00 : dts.Compute("sum(salamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFCollAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(collamt)", "")) ? 0.00 : dts.Compute("sum(collamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFperonsale")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(perontosal)", "")) ? 0.00 : dts.Compute("sum(perontosal)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFperontocol")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(perontocol)", "")) ? 0.00 : dts.Compute("sum(perontocol)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFBgdAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(bgdamt)", "")) ? 0.00 : dts.Compute("sum(bgdamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFExpAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(examt)", "")) ? 0.00 : dts.Compute("sum(examt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFperonPro")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(peronpgres)", "")) ? 0.00 : dts.Compute("sum(peronpgres)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFipamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(ipamt)", "")) ? 0.00 : dts.Compute("sum(ipamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFreCollAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(rsalamt)", "")) ? 0.00 : dts.Compute("sum(rsalamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFusoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(usoamt)", "")) ? 0.00 : dts.Compute("sum(usoamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFRbgdAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(rbgdamt)", "")) ? 0.00 : dts.Compute("sum(rbgdamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFfsamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(fsamt)", "")) ? 0.00 : dts.Compute("sum(fsamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFfsuamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(fsuamt)", "")) ? 0.00 : dts.Compute("sum(fsuamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFresdramt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(invreqamt)", "")) ? 0.00 : dts.Compute("sum(invreqamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFrescramt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(invblkamt)", "")) ? 0.00 : dts.Compute("sum(invblkamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFlibamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(libamt)", "")) ? 0.00 : dts.Compute("sum(libamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFnoiamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(noiamt)", "")) ? 0.00 : dts.Compute("sum(noiamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFltoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(ltoamt)", "")) ? 0.00 : dts.Compute("sum(ltoamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFlfrmamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(lfrmamt)", "")) ? 0.00 : dts.Compute("sum(lfrmamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFbgdsaamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(bgdsaamt)", "")) ? 0.00 : dts.Compute("sum(bgdsaamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFnp")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(np)", "")) ? 0.00 : dts.Compute("sum(np)", ""))).ToString("#,##0.00;(#,##0.00); ");


                    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                    ((HyperLink)this.gvInvest.HeaderRow.FindControl("hlbtntbCdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                    Session["Report1"] = gvInvest;
                    ((HyperLink)this.gvInvest.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;


                case "PrjStatus":
                    string comcod = this.GetCompCode();
                    this.grvPrjStatus.DataSource = dt1;
                    if (ASTUtility.Left(comcod, 1) == "2")
                    {
                        this.grvPrjStatus.Columns[13].HeaderText = "Land Cost + Admin + Selling Exp.";
                        this.grvPrjStatus.Columns[15].HeaderText = "Other Cost(Non-Refundable)";
                    }
                    this.grvPrjStatus.DataBind();
                    this.FooteCalculation();

                    DataRow[] dr2 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                    ((HyperLink)this.grvPrjStatus.HeaderRow.FindControl("hlbtntbCdataExel")).Enabled = (Convert.ToBoolean(dr2[0]["printable"]));

                    Session["Report1"] = grvPrjStatus;
                    ((HyperLink)this.grvPrjStatus.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


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

                    this.gvMonPorStatus.DataSource = dt1;
                    this.gvMonPorStatus.DataBind();
                    Session["Report1"] = gvMonPorStatus;
                    if (dt1.Rows.Count > 0)
                        ((HyperLink)this.gvMonPorStatus.HeaderRow.FindControl("hlbtnCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    this.FooteCalculation();
                    break;


            }
        }


        private void FooteCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblmasterbgd"];
            if (dt.Rows.Count == 0)
                return;
            DataView dv = new DataView();
            DataTable dt1;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {


                case "InvestPlan":


                    DataView dv1 = dt.Copy().DefaultView;
                    dv1.RowFilter = ("actcode = 'CCCCAAAAAAAA'");
                    DataTable dts = dv1.ToTable();

                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFToSalVal")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(tosalval)", "")) ? 0.00 : dts.Compute("sum(tosalval)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFSaleAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(salamt)", "")) ? 0.00 : dts.Compute("sum(salamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFCollAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(collamt)", "")) ? 0.00 : dts.Compute("sum(collamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFperonsale")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(perontosal)", "")) ? 0.00 : dts.Compute("sum(perontosal)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFperontocol")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(perontocol)", "")) ? 0.00 : dts.Compute("sum(perontocol)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFBgdAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(bgdamt)", "")) ? 0.00 : dts.Compute("sum(bgdamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFExpAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(examt)", "")) ? 0.00 : dts.Compute("sum(examt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFperonPro")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(peronpgres)", "")) ? 0.00 : dts.Compute("sum(peronpgres)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFipamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(ipamt)", "")) ? 0.00 : dts.Compute("sum(ipamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFreCollAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(rsalamt)", "")) ? 0.00 : dts.Compute("sum(rsalamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFusoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(usoamt)", "")) ? 0.00 : dts.Compute("sum(usoamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFRbgdAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(rbgdamt)", "")) ? 0.00 : dts.Compute("sum(rbgdamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFfsamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(fsamt)", "")) ? 0.00 : dts.Compute("sum(fsamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFfsuamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(fsuamt)", "")) ? 0.00 : dts.Compute("sum(fsuamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFresdramt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(invreqamt)", "")) ? 0.00 : dts.Compute("sum(invreqamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFrescramt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(invblkamt)", "")) ? 0.00 : dts.Compute("sum(invblkamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFlibamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(libamt)", "")) ? 0.00 : dts.Compute("sum(libamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFnoiamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(noiamt)", "")) ? 0.00 : dts.Compute("sum(noiamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFltoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(ltoamt)", "")) ? 0.00 : dts.Compute("sum(ltoamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFlfrmamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(lfrmamt)", "")) ? 0.00 : dts.Compute("sum(lfrmamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFbgdsaamt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(bgdsaamt)", "")) ? 0.00 : dts.Compute("sum(bgdsaamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvInvest.FooterRow.FindControl("lgvFnp")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("sum(np)", "")) ? 0.00 : dts.Compute("sum(np)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    break;




                case "PrjStatus":

                    dv = dt.Copy().DefaultView;
                    dv.RowFilter = ("pactcode='CCCCAAAAAAAA'");
                    dt1 = dv.ToTable();
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
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFExpAmtp")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(texpamt)", "")) ?
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
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFLibAmtp")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tliamt)", "")) ?
                                            0.00 : dt1.Compute("sum(tliamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFLframt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(lfrmhoff)", "")) ?
                                            0.00 : dt1.Compute("sum(lfrmhoff)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFLtoamtp")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ltohoff)", "")) ?
                                            0.00 : dt1.Compute("sum(ltohoff)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFRLamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(treloanamt)", "")) ?
                                            0.00 : dt1.Compute("sum(treloanamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "MProStatus":

                    dv = dt.Copy().DefaultView;
                    dv.RowFilter = ("actcode='BBBBBBBBAAAA'");
                    dt1 = dv.ToTable();

                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR1")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r1)", "")) ? 0.00 : dt1.Compute("sum(r1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR2")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r2)", "")) ? 0.00 : dt1.Compute("sum(r2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR3")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r3)", "")) ? 0.00 : dt1.Compute("sum(r3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR4")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r4)", "")) ? 0.00 : dt1.Compute("sum(r4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR5")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r5)", "")) ? 0.00 : dt1.Compute("sum(r5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR6")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r6)", "")) ? 0.00 : dt1.Compute("sum(r6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR7")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r7)", "")) ? 0.00 : dt1.Compute("sum(r7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR8")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r8)", "")) ? 0.00 : dt1.Compute("sum(r8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR9")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r9)", "")) ? 0.00 : dt1.Compute("sum(r9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR10")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r10)", "")) ? 0.00 : dt1.Compute("sum(r10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR11")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r1)", "")) ? 0.00 : dt1.Compute("sum(r11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR12")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r12)", "")) ? 0.00 : dt1.Compute("sum(r12)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR13")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r13)", "")) ? 0.00 : dt1.Compute("sum(r13)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR14")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r14)", "")) ? 0.00 : dt1.Compute("sum(r14)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR15")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r15)", "")) ? 0.00 : dt1.Compute("sum(r15)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR16")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r16)", "")) ? 0.00 : dt1.Compute("sum(r16)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR17")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r17)", "")) ? 0.00 : dt1.Compute("sum(r17)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR18")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r18)", "")) ? 0.00 : dt1.Compute("sum(r18)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR19")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r19)", "")) ? 0.00 : dt1.Compute("sum(r19)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR20")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r20)", "")) ? 0.00 : dt1.Compute("sum(r20)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFtoCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(toramt)", "")) ? 0.00 : dt1.Compute("sum(toramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFtoCollection")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tocollamt)", "")) ? 0.00 : dt1.Compute("sum(tocollamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFnetposition")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(netamt)", "")) ? 0.00 : dt1.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;


            }





        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {

                case "InvestPlan":
                    this.PrintInvestPlan();
                    break;
                case "PrjStatus":
                    this.PrintPrjStatus();
                    break;

                case "MProStatus":
                    this.PrintMonProStatus();
                    break;



            }
        }


        private void PrintInvestPlan()
        {



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)ViewState["tblmasterbgd"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_50_CMIS.RptConInvestPlan();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;


            TextObject txtfromdate = rptstk.ReportDefinition.ReportObjects["txtfromdate"] as TextObject;
            txtfromdate.Text = "As On:" + this.txtfromdate.Text.Trim();

            TextObject txtColl = rptstk.ReportDefinition.ReportObjects["txtColl"] as TextObject;
            txtColl.Text = this.txtColl.Text;
            TextObject txtNp = rptstk.ReportDefinition.ReportObjects["txtNp"] as TextObject;
            txtNp.Text = this.txtNp.Text;
            TextObject txtBgd = rptstk.ReportDefinition.ReportObjects["txtBgd"] as TextObject;
            txtBgd.Text = this.txtBgd.Text;

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            //string comcod = this.GetComeCode();
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





        }

        private void PrintPrjStatus()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblmasterbgd"];
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptsaldperiod = new RealERPRPT.R_50_CMIS.RptConProjectStatus();
            TextObject rptComp = rptsaldperiod.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptComp.Text = comnam;
            TextObject rptdate = rptsaldperiod.ReportDefinition.ReportObjects["txtfromdate"] as TextObject;
            rptdate.Text = "Upto  " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM yyyy");
            TextObject txtthisonsal = rptsaldperiod.ReportDefinition.ReportObjects["txtthisonsal"] as TextObject;
            txtthisonsal.Text = "Month of " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMM yy");

            DataTable dt1 = (DataTable)ViewState["tblinterest"];
            double tchrgedpro, tpaidpro, nettotal;
            tchrgedpro = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(interam)", "")) ? 0.00 : dt1.Compute("sum(interam)", "")));
            tpaidpro = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(inpaid)", "")) ? 0.00 : dt1.Compute("sum(inpaid)", "")));
            nettotal = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(netam)", "")) ? 0.00 : dt1.Compute("sum(netam)", "")));
            // ViewState["tblinterest"] = ds1.Tables[1];


            TextObject txtonchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txtonchrgetopro"] as TextObject;
            txtonchrgetopro.Text = Convert.ToDouble(dt1.Rows[0]["interam"]).ToString("#,##0;(#,##0); ");
            TextObject txtfinchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txtfinchrgetopro"] as TextObject;
            txtfinchrgetopro.Text = Convert.ToDouble(dt1.Rows[1]["interam"]).ToString("#,##0;(#,##0); ");
            TextObject txthovrchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txthovrchrgetopro"] as TextObject;
            txthovrchrgetopro.Text = Convert.ToDouble(dt1.Rows[2]["interam"]).ToString("#,##0;(#,##0); ");
            TextObject txtlandchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txtlandchrgetopro"] as TextObject;
            txtlandchrgetopro.Text = Convert.ToDouble(dt1.Rows[3]["interam"]).ToString("#,##0;(#,##0); ");
            TextObject txttotalchrgetopro = rptsaldperiod.ReportDefinition.ReportObjects["txttotalchrgetopro"] as TextObject;
            txttotalchrgetopro.Text = tchrgedpro.ToString("#,##0;(#,##0); ");


            TextObject txtonpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txtonpaidtopro"] as TextObject;
            txtonpaidtopro.Text = Convert.ToDouble(dt1.Rows[0]["inpaid"]).ToString("#,##0;(#,##0); ");
            TextObject txtfinpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txtfinpaidtopro"] as TextObject;
            txtfinpaidtopro.Text = Convert.ToDouble(dt1.Rows[1]["inpaid"]).ToString("#,##0;(#,##0); ");
            TextObject txthovrpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txthovrpaidtopro"] as TextObject;
            txthovrpaidtopro.Text = Convert.ToDouble(dt1.Rows[2]["inpaid"]).ToString("#,##0;(#,##0); ");
            TextObject txtlandpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txtlandpaidtopro"] as TextObject;
            txtlandpaidtopro.Text = Convert.ToDouble(dt1.Rows[3]["inpaid"]).ToString("#,##0;(#,##0); ");
            TextObject txttotalpaidtopro = rptsaldperiod.ReportDefinition.ReportObjects["txttotalpaidtopro"] as TextObject;
            txttotalpaidtopro.Text = tpaidpro.ToString("#,##0;(#,##0); ");



            TextObject txtonnet = rptsaldperiod.ReportDefinition.ReportObjects["txtonnet"] as TextObject;
            txtonnet.Text = Convert.ToDouble(dt1.Rows[0]["netam"]).ToString("#,##0;(#,##0); ");
            TextObject txtfinneto = rptsaldperiod.ReportDefinition.ReportObjects["txtfinnet"] as TextObject;
            txtfinneto.Text = Convert.ToDouble(dt1.Rows[1]["netam"]).ToString("#,##0;(#,##0); ");
            TextObject txthovrnet = rptsaldperiod.ReportDefinition.ReportObjects["txthovrnet"] as TextObject;
            txthovrnet.Text = Convert.ToDouble(dt1.Rows[2]["netam"]).ToString("#,##0;(#,##0); ");
            TextObject txtlandnet = rptsaldperiod.ReportDefinition.ReportObjects["txtlandnet"] as TextObject;
            txtlandnet.Text = Convert.ToDouble(dt1.Rows[3]["netam"]).ToString("#,##0;(#,##0); ");
            TextObject txtnettotal = rptsaldperiod.ReportDefinition.ReportObjects["txtnettotal"] as TextObject;
            txtnettotal.Text = nettotal.ToString("#,##0;(#,##0); ");






            string calltype = this.PrjCallType();

            TextObject rptCost = rptsaldperiod.ReportDefinition.ReportObjects["txtCost"] as TextObject;
            rptCost.Text = (calltype == "RPTPROJECTSTATUS") ? "Const+Admin+Selling Expen." : "Apt. Purchase+Admin+Selling";

            TextObject txtuserinfo = rptsaldperiod.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptsaldperiod.SetDataSource(dt);





            string comcod = hst["comcod"].ToString();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales During the Peroid";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptsaldperiod.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptsaldperiod;
            //this.lbljavascript.Text = "<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //              this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }


        private void PrintMonProStatus()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblmasterbgd"];

            ReportDocument rptmprost = new RealERPRPT.R_50_CMIS.RptConMonProjectStatus();
            TextObject rptComp = rptmprost.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptComp.Text = comnam;
            TextObject rptdate = rptmprost.ReportDefinition.ReportObjects["txtfromdate"] as TextObject;
            rptdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";

            DataTable dtrname = (DataTable)ViewState["tblresdesc"];
            int j = 1;
            for (int i = 0; i < dtrname.Rows.Count; i++)
            {

                TextObject rpttxth = rptmprost.ReportDefinition.ReportObjects["txtR" + j.ToString()] as TextObject;
                rpttxth.Text = dtrname.Rows[i]["resdesc"].ToString();
                j++;
                if (j == 15)
                    break;


            }
            TextObject txtconcost = rptmprost.ReportDefinition.ReportObjects["txtconcost"] as TextObject;
            txtconcost.Text = this.lblvalConsCost.Text;

            TextObject txtnonconcost = rptmprost.ReportDefinition.ReportObjects["txtnonconcost"] as TextObject;
            txtnonconcost.Text = this.lblnonvalConsCost.Text;



            TextObject txtuserinfo = rptmprost.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptmprost.SetDataSource(dt);
            Session["Report1"] = rptmprost;
            //this.lbljavascript.Text = "<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //              this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }


        protected void gvInvest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvActDesc = (Label)e.Row.FindControl("lgvIActDesc");
                Label lgvtosales = (Label)e.Row.FindControl("lgvtosales");
                Label lgvsales = (Label)e.Row.FindControl("lgvsales");
                Label lgvcoll = (Label)e.Row.FindControl("lgvcoll");
                Label lgvrecoll = (Label)e.Row.FindControl("lgvrecoll");
                Label lgvBgdAmt = (Label)e.Row.FindControl("lgvBgdAmt");
                Label lgvExpAmt = (Label)e.Row.FindControl("lgvExp");
                Label lgvRbgdAmt = (Label)e.Row.FindControl("lgvrebgdamt");
                Label lgvresdramt = (Label)e.Row.FindControl("lgvresdramt");
                Label lgvrescramt = (Label)e.Row.FindControl("lgvrescramt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lgvActDesc.Font.Bold = true;
                    lgvtosales.Font.Bold = true;
                    lgvsales.Font.Bold = true;
                    lgvcoll.Font.Bold = true;
                    lgvrecoll.Font.Bold = true;
                    lgvBgdAmt.Font.Bold = true;
                    lgvExpAmt.Font.Bold = true;
                    lgvRbgdAmt.Font.Bold = true;
                    lgvresdramt.Font.Bold = true;
                    lgvrescramt.Font.Bold = true;
                    lgvActDesc.Style.Add("text-align", "right");
                }

            }
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
                Label lgvExpAmt = (Label)e.Row.FindControl("lgvExpAmtp");
                Label lgvPAdvAmt = (Label)e.Row.FindControl("lgvPAdvAmt");
                Label lgvLCNFAmt = (Label)e.Row.FindControl("lgvLCNFAmt");
                Label lgvOvmt = (Label)e.Row.FindControl("lgvOvmt");
                Label lgvIAmt = (Label)e.Row.FindControl("lgvIAmt");
                Label lgvtExp = (Label)e.Row.FindControl("lgvtExp");
                Label lgvLibAmt = (Label)e.Row.FindControl("lgvLibAmtp");
                Label lgvLframt = (Label)e.Row.FindControl("lgvLframt");
                Label lgvLtoamt = (Label)e.Row.FindControl("lgvLtoamtp");
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

            }


        }

        protected void gvMonPorStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvActDesc = (Label)e.Row.FindControl("lgvActDesc");
                Label lgvR1 = (Label)e.Row.FindControl("lgvR1");
                Label lgvR2 = (Label)e.Row.FindControl("lgvR2");
                Label lgvR3 = (Label)e.Row.FindControl("lgvR3");
                Label lgvR4 = (Label)e.Row.FindControl("lgvR4");
                Label lgvR5 = (Label)e.Row.FindControl("lgvR5");
                Label lgvR6 = (Label)e.Row.FindControl("lgvR6");
                Label lgvR7 = (Label)e.Row.FindControl("lgvR7");
                Label lgvR8 = (Label)e.Row.FindControl("lgvR8");
                Label lgvR9 = (Label)e.Row.FindControl("lgvR9");
                Label lgvR10 = (Label)e.Row.FindControl("lgvR10");
                Label lgvR11 = (Label)e.Row.FindControl("lgvR11");
                Label lgvR12 = (Label)e.Row.FindControl("lgvR12");
                Label lgvR13 = (Label)e.Row.FindControl("lgvR13");
                Label lgvR14 = (Label)e.Row.FindControl("lgvR14");
                //Label lgvR15= (Label)e.Row.FindControl("lgvR15");
                //Label lgvR16 = (Label)e.Row.FindControl("lgvR16");
                //Label lgvR17 = (Label)e.Row.FindControl("lgvR17");
                //Label lgvR18 = (Label)e.Row.FindControl("lgvR18");
                //Label lgvR19 = (Label)e.Row.FindControl("lgvR19");
                //Label lgvR20 = (Label)e.Row.FindControl("lgvR20");
                Label lgvtocost = (Label)e.Row.FindControl("lgvtocost");
                Label lgvtoCollection = (Label)e.Row.FindControl("lgvtoCollection");
                Label lgvnetposition = (Label)e.Row.FindControl("lgvnetposition");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lgvActDesc.Font.Bold = true;
                    lgvR1.Font.Bold = true;
                    lgvR2.Font.Bold = true;
                    lgvR3.Font.Bold = true;
                    lgvR4.Font.Bold = true;
                    lgvR5.Font.Bold = true;
                    lgvR6.Font.Bold = true;
                    lgvR7.Font.Bold = true;
                    lgvR8.Font.Bold = true;
                    lgvR9.Font.Bold = true;
                    lgvR10.Font.Bold = true;
                    lgvR11.Font.Bold = true;
                    lgvR12.Font.Bold = true;
                    lgvR13.Font.Bold = true;
                    lgvR14.Font.Bold = true;

                    //lgvR19.Font.Bold = true;
                    //lgvR20.Font.Bold = true;
                    lgvtocost.Font.Bold = true;
                    lgvtoCollection.Font.Bold = true;
                    lgvnetposition.Font.Bold = true;
                    lgvActDesc.Style.Add("text-align", "right");
                }

            }
        }

    }
}