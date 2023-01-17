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
using RealERPLIB;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_28_MPro
{
    public partial class RptMktProjectStock : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

                //((Label)this.Master.FindControl("lblTitle")).Text = "Marketing MATERIALS STOCK REPORT";

                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                if (Request.QueryString.AllKeys.Contains("prjcode") && this.Request.QueryString["prjcode"].Length > 0)
                {
                    this.lbtnOk_Click(null, null);

                }
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
            return (hst["comcod"].ToString());

        }

        protected void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%%";
            string length = "";
            string userid = hst["usrid"].ToString();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETPURPROJECTNAME", serch1, length, userid, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProName.DataTextField = "pactdesc";
            this.ddlProName.DataValueField = "pactcode";
            this.ddlProName.DataSource = ds1.Tables[0];
            this.ddlProName.DataBind();
            if (Request.QueryString.AllKeys.Contains("prjcode"))
            {
                this.ddlProName.SelectedValue = this.Request.QueryString["prjcode"].Length > 0 ? this.Request.QueryString["prjcode"] : "";
            }

        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.MatStock();
        }
        private void MatStock()
        {
            Session.Remove("UserLog");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProName.SelectedValue.ToString();
            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;
            string chalan =  "chalan";
            string mRptGroup ="%";
            string rsircode = "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_MKT_PROCUREMENT", "RPT_MKT_PROJ_STOCK", pactcode, fdate, tdate, mRptGroup, rsircode, chalan, "", "", "");
            if (ds1 == null)
            {
                this.gvMatStock.DataSource = null;
                this.gvMatStock.DataBind();
                return;
            }
            
            Session["tbMatStc"] = ds1.Tables[0];
            this.Data_Bind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Show Report:";
                string eventdesc2 = "Project Name: " + this.ddlProName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbMatStc"];

            this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvMatStock.DataSource = dt;
            this.gvMatStock.DataBind();

            this.FooterCalculation();    

        }


        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tbMatStc"];

            if (dt.Rows.Count > 0)
            {
                ((Label)this.gvMatStock.FooterRow.FindControl("lgvFbudgetqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdqty)", "")) ? 0.00 : dt.Compute("Sum(bgdqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvMatStock.FooterRow.FindControl("lgvOpF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opqty)", "")) ? 0.00 : dt.Compute("Sum(opqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvMatStock.FooterRow.FindControl("lgvRecamtF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(rcvqty)", "")) ? 0.00 : dt.Compute("Sum(rcvqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvMatStock.FooterRow.FindControl("lgvIsuamtF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(issueqty)", "")) ? 0.00 : dt.Compute("Sum(issueqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvMatStock.FooterRow.FindControl("lgvAcSamtF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(acstock)", "")) ? 0.00 : dt.Compute("Sum(acstock)", ""))).ToString("#,##0.00;(#,##0.00); ");

                Session["Report1"] = gvMatStock;
                ((HyperLink)this.gvMatStock.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


            }

            else
            {
                return;
            }

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.RtpStockReportGeneral();
        }

        private void RtpStockReportGeneral()
        {
            DataTable dt = (DataTable)Session["tbMatStc"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fdate = this.txtfromdate.Text.ToString();
            string tdate = this.txttodate.Text.ToString();
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string Headertitle = "";
            if (this.Request.QueryString["Type"].ToString() == "inv")
            {

                Headertitle = "Materials Stock Information(Inventory)";
            }

            DataTable dt1 = (DataTable)Session["tbMatStc"];

            if (comcod == "3315" || comcod == "3316" || comcod=="3101")
            {
                DataView dv = dt1.DefaultView; //only Assure
                dv.RowFilter = ("tqty<>0 or opqty<>0 or rcvqty<>0 or trninqty<>0 or trnoutqty<>0");
                dt1 = dv.ToTable();

            }

            if (dt1 == null)
                return;
            var lst = dt1.DataTableToList<RealEntity.C_12_Inv.ErptStock>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.rptProMatStock2", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("header", Headertitle));
            Rpt1.SetParameters(new ReportParameter("ProjectName", "Project Name : " + this.ddlProName.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("date", "From: " + fdate + " To: " + tdate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report:";
                string eventdesc2 = "Project Name: " + this.ddlProName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }

        protected void gvMatStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMatStock.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
       
    }
}