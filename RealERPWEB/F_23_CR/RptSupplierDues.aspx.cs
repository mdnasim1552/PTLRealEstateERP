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
namespace RealERPWEB.F_23_CR
{
    public partial class RptSupplierDues : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                // string date1 = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txtfrmDate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy"); ;


                this.txttodate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "CUSTOMER DUES INFORMATION";
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
        }


        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();

        }

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        public string CompanyWiserpt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string chequeprint = "";
            switch (comcod)
            {

                case "3315":
                case "3116":
                case "3167":

                case "3302":

                    chequeprint = "PrintDues1";
                    break;


                case "3325":
                case "2325":
                case "3101":
                    chequeprint = "PrintDues2";
                    break;
                default:
                    //chequeprint = "PrintDues1";
                    chequeprint = "PrintDues3";
                    break;
            }
            return chequeprint;

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();

            string printrpt = this.CompanyWiserpt();

            ReportDocument rptcusdues = new ReportDocument();
            if (printrpt == "PrintDues1")
            {
                rptcusdues = new RealERPRPT.R_23_CR.RptCustDuesInfoAssure();
            }
            else if (printrpt == "PrintDues2")
            {
                rptcusdues = new RealERPRPT.R_23_CR.RptCustDuesInfoLeisure();

            }
            else
            {
                rptcusdues = new RealERPRPT.R_23_CR.RptCustDuesInfo();
            }

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            TextObject rpttxtCompanyName = rptcusdues.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rpttxtCompanyName.Text = comnam;
            TextObject rptdate = rptcusdues.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "(From " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("MMMM dd, yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("MMMM dd, yyyy") + ")";
            TextObject txtuserinfo = rptcusdues.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            TextObject rpttxttotalDueAmt = rptcusdues.ReportDefinition.ReportObjects["txttotalDueAmt"] as TextObject;
            rpttxttotalDueAmt.Text = ((Label)this.gvcustdues.FooterRow.FindControl("lgvFDueAmt")).Text;
            rptcusdues.SetDataSource((DataTable)Session["tblCustDues"]);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Received List Info";
                string eventdesc = "Print Report MR";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptcusdues.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptcusdues;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string ProjectCode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string curdues = (this.chkCurrentdues.Checked) ? "current" : "";
            string overdues = (this.chkoverdues.Checked) ? "overdues" : "";
            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTCUSTDUES", ProjectCode, frmdate, todate, curdues, overdues, "", "", "", "");
            if (ds2 == null)
            {
                this.gvcustdues.DataSource = null;
                this.gvcustdues.DataBind();
                return;
            }

            Session["tblCustDues"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string usircode = dt1.Rows[0]["usircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["usircode"].ToString() == usircode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    usircode = dt1.Rows[j]["usircode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                    dt1.Rows[j]["udesc"] = "";
                    dt1.Rows[j]["custname"] = "";
                    dt1.Rows[j]["custadd"] = "";
                    dt1.Rows[j]["cteam"] = "";

                }

                else
                {
                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        dt1.Rows[j]["pactdesc"] = "";
                    else if (dt1.Rows[j]["usircode"].ToString() == usircode)
                    {
                        dt1.Rows[j]["udesc"] = "";
                        dt1.Rows[j]["custname"] = "";
                        dt1.Rows[j]["custadd"] = "";
                        dt1.Rows[j]["cteam"] = "";
                    }


                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    usircode = dt1.Rows[j]["usircode"].ToString();
                }

            }

            return dt1;
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblCustDues"];
            this.gvcustdues.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvcustdues.DataSource = dt;
            this.gvcustdues.DataBind();
            this.FooterCalculation(dt);




        }

        private void FooterCalculation(DataTable dt)
        {

            DataTable dt1 = dt.Copy();
            if (dt1.Rows.Count == 0)
                return;

            //DataView dv = dt1.DefaultView;
            //dv.RowFilter = ("gcod like '81AAAA%'");
            //dt = dv.ToTable();
            ((Label)this.gvcustdues.FooterRow.FindControl("lgvFDueAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(dueamt)", "")) ?
                0.00 : dt1.Compute("Sum(dueamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvcustdues.FooterRow.FindControl("lgvFcurDueAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(cdueamt)", "")) ?
             0.00 : dt1.Compute("Sum(cdueamt)", ""))).ToString("#,##0;(#,##0); ");

            Session["Report1"] = gvcustdues;
            ((HyperLink)this.gvcustdues.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();

        }
        protected void gvcustdues_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvcustdues.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvcustdues_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Installment = (Label)e.Row.FindControl("lgvInstallment");
                Label duesins = (Label)e.Row.FindControl("lgvDues");
                Label duesamt = (Label)e.Row.FindControl("lgvDamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "81AAAAA")
                {

                    Installment.Font.Bold = true;
                    duesins.Font.Bold = true;
                    duesamt.Font.Bold = true;
                    Installment.Style.Add("text-align", "right");


                }

            }
        }
        protected void ddlpagesize_SelectedIndexChanged1(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void chkoverdues_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkoverdues.Checked)
            {
                this.chkCurrentdues.Checked = false;
            }
        }
    }
}