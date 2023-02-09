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
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_17_Acc
{


    public partial class RptPrjPcash : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = " Project Wise Pettty Cash";
                this.txtDateFrom.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.ProjectLlist();
                this.Resourlist();
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void ProjectLlist()
        {
            string comcod = this.GetComeCode();
            string filter = this.txtshrprj.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "PROJECTLIST", filter, "", "", "", "", "", "", "", "");
            this.ddlPrjlist.DataSource = ds1.Tables[0];
            this.ddlPrjlist.DataTextField = "actdesc1";
            this.ddlPrjlist.DataValueField = "actcode";
            this.ddlPrjlist.DataBind();

        }

        private void Resourlist()
        {
            string comcod = this.GetComeCode();
            string txtsrchCustomer = "%" + this.txtSrchRes.Text + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "RESSOURCELIST", txtsrchCustomer, "", "", "", "", "", "", "", "");
            this.ddlReslist.DataTextField = "actdesc1";
            this.ddlReslist.DataValueField = "actcode";
            this.ddlReslist.DataSource = ds1.Tables[0];
            this.ddlReslist.DataBind();
            ds1.Dispose();

        }

        private void ShowData()
        {
            Session.Remove("tblpt");
            string comcod = this.GetComeCode();
            string rescode = this.ddlReslist.SelectedValue.ToString();
            string actcode = this.ddlPrjlist.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "DATEWISE", actcode, rescode, frmdate, todate, "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvPtcash.DataSource = null;
                this.gvPtcash.DataBind();
                return;
            }

            Session["tblpt"] = ds1.Tables[0];          //ds1.Tables[0];
            this.Data_Bind();

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblpt"];
            this.gvPtcash.DataSource = dt;
            this.gvPtcash.DataBind();
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvPtcash.FooterRow.FindControl("lblFamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amount)", "")) ?
              0.00 : dt.Compute("Sum(amount)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }
        protected void ibtnFindPrj_Click(object sender, EventArgs e)
        {
            this.ProjectLlist();
        }
        protected void ibtnFindRes_Click(object sender, EventArgs e)
        {
            this.Resourlist();
        }
        protected void lnkdetail_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string asst = this.Request.QueryString["rsirdesc"].ToString();
            DataTable dt1 = (DataTable)Session["tblpt"];

            string date = " (From " + this.txtDateFrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + ")";
            string txtprojectname = "Project Name: " + this.ddlPrjlist.SelectedItem.Text.Substring(19);
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            if (dt1.Rows.Count == 0)
                return;

            var list = dt1.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptProjectPettyCash>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptProjectPettyCash", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtprojectname", txtprojectname));

            Rpt1.SetParameters(new ReportParameter("title", "Project Wise Petty Cash"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




            //ReportDocument rptPtCash = new RealERPRPT.R_17_Acc.rptPettyCash();
            //TextObject rptCname = rptPtCash.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject prjlist = rptPtCash.ReportDefinition.ReportObjects["txtPrjlist"] as TextObject;
            //prjlist.Text = this.ddlPrjlist.SelectedItem.Text.Substring(19);

            //TextObject txtdate = rptPtCash.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = " (From " + this.txtDateFrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + ")";
            //TextObject txtuserinfo = rptPtCash.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptPtCash.SetDataSource(dt1);

            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptPtCash.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptPtCash;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

    }
}