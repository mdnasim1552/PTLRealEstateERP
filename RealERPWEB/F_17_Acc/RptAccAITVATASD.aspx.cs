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
    public partial class RptAccAITVATASD : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "AIT, VAT & SD DEDECUTION ";
                //this.Master.Page.Title = "AIT, VAT & SD DEDECUTION INFORMATION";
                string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDateFrom.Text = "01" + ASTUtility.Right(Date, 9);
                this.txtDateto.Text = Date;
                this.GetResList();
                this.GetProjectName();
                this.GetConOrSupName();
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

        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "GETROJECTNAME1", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }

        private void GetConOrSupName()
        {

            string comcod = this.GetCompCode();

            string serch1 = "%" + this.txtSrcSub.Text.Trim() + "%";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "GETSUPORCONNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlSuborConName.DataTextField = "sirdesc";
            this.ddlSuborConName.DataValueField = "sircode";
            this.ddlSuborConName.DataSource = ds1.Tables[0];
            this.ddlSuborConName.DataBind();



        }

        protected void ibtnFindRes_Click(object sender, EventArgs e)
        {
            this.GetResList();
        }
        protected void ibtnFindSubConName_Click(object sender, EventArgs e)
        {
            this.GetConOrSupName();
        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblaitvatsd");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string resource = this.ddlConAccResHead.SelectedValue.ToString();
            string SuporConName = ((this.ddlSuborConName.SelectedValue.ToString().Substring(2) == "0000000000") ? (this.ddlSuborConName.SelectedValue.ToString().Substring(0, 2)) : this.ddlSuborConName.SelectedValue.ToString()) + "%";
            string ProjectName = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTSUPPAITAVAT", frmdate, todate, resource, SuporConName, ProjectName, "", "", "", "");
            if (ds1 == null)
            {

                this.gvaitvsd.DataSource = null;
                this.gvaitvsd.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblaitvatsd"] = dt;
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            this.gvaitvsd.DataSource = (DataTable)Session["tblaitvatsd"];
            this.gvaitvsd.DataBind();
            this.FooterCal();


        }


        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string vounum = dt1.Rows[0]["vounum1"].ToString();
            string actcode = dt1.Rows[0]["actcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if ((dt1.Rows[j]["actcode"].ToString() == actcode) && (dt1.Rows[j]["vounum1"].ToString() == vounum))
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    vounum = dt1.Rows[j]["vounum1"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                    dt1.Rows[j]["vounum1"] = "";

                }

                else
                {

                    if (dt1.Rows[j]["actcode"].ToString() == actcode)
                    {

                        dt1.Rows[j]["actdesc"] = "";
                    }

                    if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                    {

                        dt1.Rows[j]["vounum1"] = "";

                    }
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    vounum = dt1.Rows[j]["vounum1"].ToString();
                }





            }
            return dt1;

        }


        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblaitvatsd"];
            if (dt.Rows.Count == 0)
                return;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "head1='03CT'";
            dt = dv.ToTable();
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

            DataTable dt1 = (DataTable)Session["tblaitvatsd"];

            DataTable dt = (DataTable)Session["tblaitvatsd"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "head1='03CT'";
            dt = dv.ToTable();

            string opam = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opam)", "")) ?
                    0 : dt.Compute("sum(opam)", ""))).ToString("#,##0;(#,##0); ");
            string dram = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                    0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");
            string cram = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                    0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");
            string clsam = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsam)", "")) ?
                    0 : dt.Compute("sum(clsam)", ""))).ToString("#,##0;(#,##0); ");

            string date = "( From: " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + "  To: " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";

            string rpttitle = "AIT VAT & SD Report";
            string resource = this.ddlConAccResHead.SelectedItem.ToString().Substring(13);
            string suborconname = this.ddlSuborConName.SelectedItem.ToString().Substring(13);

            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = dt1.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptAitVatSdDeduction>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccAitVatSd", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", rpttitle));
            Rpt1.SetParameters(new ReportParameter("resource", resource));
            Rpt1.SetParameters(new ReportParameter("suborconname", suborconname));
            Rpt1.SetParameters(new ReportParameter("opam", opam));
            Rpt1.SetParameters(new ReportParameter("dram", dram));
            Rpt1.SetParameters(new ReportParameter("cram", cram));
            Rpt1.SetParameters(new ReportParameter("clsam", clsam));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //ReportDocument rptsl = new RealERPRPT.R_17_Acc.RptAccAITVATASD();
            //TextObject txtCompany = rptsl.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtOpam = rptsl.ReportDefinition.ReportObjects["opening"] as TextObject;
            //txtOpam.Text = opam;
            //TextObject txtDr = rptsl.ReportDefinition.ReportObjects["dr"] as TextObject;
            //txtDr.Text = dram;
            //TextObject txtCr = rptsl.ReportDefinition.ReportObjects["cr"] as TextObject;
            //txtCr.Text = cram;
            //TextObject txtCloseing = rptsl.ReportDefinition.ReportObjects["closeing"] as TextObject;
            //txtCloseing.Text = clsam;
            ////TextObject txtfdate = rptsl.ReportDefinition.ReportObjects["fdate"] as TextObject;
            ////txtfdate.Text = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            ////TextObject txttdate = rptsl.ReportDefinition.ReportObjects["tdate"] as TextObject;
            ////txttdate.Text = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

            //TextObject txtdate = rptsl.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "( From: " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + "  To: " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy")+" )";


            //TextObject Resource = rptsl.ReportDefinition.ReportObjects["txtResource"] as TextObject;
            //Resource.Text = this.ddlConAccResHead.SelectedItem.ToString().Substring(13);
            //TextObject txtSupCon = rptsl.ReportDefinition.ReportObjects["txtSupCon"] as TextObject;
            //txtSupCon.Text = this.ddlSuborConName.SelectedItem.ToString().Substring(13);
            ////TextObject txtProject = rptsl.ReportDefinition.ReportObjects["txtProject"] as TextObject;
            ////txtProject.Text = "Project: " + this.ddlProjectName.SelectedItem.ToString().Substring(13);

            //TextObject txtuserinfo = rptsl.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsl.SetDataSource((DataTable)Session["tblaitvatsd"]);
            //Session["Report1"] = rptsl;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}