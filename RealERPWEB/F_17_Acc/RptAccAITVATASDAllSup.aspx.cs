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
namespace RealERPWEB.F_17_Acc
{
    public partial class RptAccAITVATASDAllSup : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
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


                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = " AIT, VAT & SD DEDUCTION -All Supplier";
                //this.Master.Page.Title = "AIT, VAT & SD DEDECUTION INFORMATION";
                this.GetResList();
                string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string Todate = "01" + ASTUtility.Right(Date, 9);
                string qDate1 = this.Request.QueryString["Date1"] ?? "";
                string qDate2 = this.Request.QueryString["Date2"] ?? "";
                this.txtDateFrom.Text = qDate1.Length > 0 ? qDate1 : Todate;
                this.txtDateto.Text = qDate1.Length > 0 ? qDate1 : Date;
                string taxcode = this.Request.QueryString["sircode"] ?? "";

                if (taxcode.Length > 0)
                    this.ddlConAccResHead.SelectedValue = taxcode;





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
        private void GetResList()
        {

            string comcod = this.GetCompCode();
            string filter = "97%";
            string Seacch = "%" + this.txtSrchRes.Text.Trim() + "%";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTACCRESLIST", filter, Seacch, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlConAccResHead.DataTextField = "resdesc1";
            this.ddlConAccResHead.DataValueField = "rescode";
            this.ddlConAccResHead.DataSource = ds1.Tables[0];
            this.ddlConAccResHead.DataBind();
        }

        protected void ibtnFindRes_Click(object sender, EventArgs e)
        {
            this.GetResList();
        }





        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblaitvatsd");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string resource = this.ddlConAccResHead.SelectedValue.ToString();

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTALLSUPPAITAVAT", frmdate, todate, resource, "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvaitvsd.DataSource = null;
                this.gvaitvsd.DataBind();
                return;
            }
            DataTable dt = ds1.Tables[0];
            Session["tblaitvatsd"] = dt;
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            this.gvaitvsd.DataSource = (DataTable)Session["tblaitvatsd"];
            this.gvaitvsd.DataBind();

            Session["Report1"] = gvaitvsd;
            if (((DataTable)Session["tblaitvatsd"]).Rows.Count > 0)
            {

                //((Label)this.dgv2.FooterRow.FindControl ("lgvFScAmt")).Text = Convert.ToDouble ((Convert.IsDBNull (dt1.Compute ("sum(insamt)", "")) ?
                //                0 : dt1.Compute ("sum(insamt)", ""))).ToString ("#,##0.00;(#,##0.00); ");

                ((HyperLink)this.gvaitvsd.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
            this.FooterCal();


        }





        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblaitvatsd"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFOpAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opam)", "")) ?
                    0 : dt.Compute("sum(opam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                    0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                    0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFClsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsam)", "")) ?
                    0 : dt.Compute("sum(clsam)", ""))).ToString("#,##0;(#,##0); ");
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblaitvatsd"];

            string date = "( From: " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + "  To: " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";

            string rpttitle = "AIT VAT & SD Deduction Information";
            string resource = this.ddlConAccResHead.SelectedItem.ToString().Substring(13);

            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.EClassAitVatSdDeduction>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccAitVatSdAllSupp", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", rpttitle));
            Rpt1.SetParameters(new ReportParameter("resource", resource));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptsl = new RealERPRPT.R_17_Acc.RptAccAITVATASDAllSup();
            //TextObject txtcompanyname = rptsl.ReportDefinition.ReportObjects["txtcompanyname"] as TextObject;
            //txtcompanyname.Text = comnam;


            //TextObject Resource = rptsl.ReportDefinition.ReportObjects["txtResource"] as TextObject;
            //Resource.Text = this.ddlConAccResHead.SelectedItem.ToString().Substring(13);

            //TextObject txtdate = rptsl.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "( From: " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + "  To: " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy")+" )";




            //TextObject txtuserinfo = rptsl.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsl.SetDataSource(dt);
            //Session["Report1"] = rptsl;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void gvaitvsd_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDescription");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                string Rescode = this.ddlConAccResHead.SelectedValue.ToString();
                string frmdate = this.txtDateFrom.Text.Trim();
                string todate = this.txtDateto.Text.Trim();

                hlink1.NavigateUrl = "RptlinkAccAITVATASD.aspx?frmdate=" + frmdate + "&todate=" + todate + "&rescode=" + Rescode + "&subcode=" + code;
            }






        }
    }
}