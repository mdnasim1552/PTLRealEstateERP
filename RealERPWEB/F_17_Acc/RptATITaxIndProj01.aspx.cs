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

    public partial class RptATITaxIndProj01 : System.Web.UI.Page
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "TDS VDS SD Deduction Individual Project Wise ";
                //this.Master.Page.Title = "TDS VDS SD Deduction Individual Project Wise ";
                this.ProjectName();
                this.GetResList();
                string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDateFrom.Text = "01" + ASTUtility.Right(Date, 9);
                this.txtDateto.Text = Date;

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



        private void ProjectName()
        {
            string comcod = this.GetCompCode();

            string Seacch = "%" + this.txtProj.Text.Trim() + "%";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "GETPROJEXTATIVAT", Seacch, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlProj.DataTextField = "Pactdesc";
            this.ddlProj.DataValueField = "pactcode";
            this.ddlProj.DataSource = ds1.Tables[0];
            this.ddlProj.DataBind();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblaitvatsd");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string resource = this.ddlConAccResHead.SelectedValue.ToString();
            string projcode = this.ddlProj.SelectedValue.ToString();


            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTALLSUPPAITAVATINDIPROJECT", frmdate, todate, resource, projcode, "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvaitvsd.DataSource = null;
                this.gvaitvsd.DataBind();
                return;
            }

            Session["tblaitvatsd"] = ds1.Tables[0];
            this.Data_Bind();

        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblaitvatsd"];
            this.gvaitvsd.DataSource = dt;
            this.gvaitvsd.DataBind();
            this.FooterCal();


        }





        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblaitvatsd"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFopen")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opam)", "")) ?
            0 : dt.Compute("sum(opam)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFPayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
            0 : dt.Compute("sum(cram)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFDeposit")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
            0 : dt.Compute("sum(dram)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvaitvsd.FooterRow.FindControl("lgvFNetamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netamt)", "")) ?
             0 : dt.Compute("sum(netamt)", ""))).ToString("#,##0.00;(#,##0.00); ");



        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string fromdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");


            DataTable dt1 = (DataTable)Session["tblaitvatsd"];

            string projname = "Project Name: " + ddlProj.SelectedItem.Text.Substring(13);
            string date = "Date: " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string rpttitle = "TDS,VDS,SD Deduction Project Wise";
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = dt1.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.TdsVdsSdDeducProjWise>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptATIVat", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", rpttitle));
            Rpt1.SetParameters(new ReportParameter("projname", projname));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptstate = new RealERPRPT.R_17_Acc.RptAITVAT();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject txtProjectName = rptstate.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //txtProjectName.Text = "Project Name: "+ddlProj.SelectedItem.Text.Substring(13);
            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptftdate.Text = "Date: " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource(dt1);
            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void lblprj_Click(object sender, EventArgs e)
        {
            this.ProjectName();
        }

        protected void gvaitvsd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkSupname = (HyperLink)e.Row.FindControl("hlnkSupname");

                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string rescode = this.ddlConAccResHead.SelectedValue.ToString();
                string ssircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString(); 
                string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy"); 
                string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

                hlnkSupname.Style.Add("color", "blue");
                hlnkSupname.NavigateUrl = "~/F_17_Acc/LinkRptATITaxIndProj01?pactcode=" + pactcode + "&rescode=" + rescode + "&ssircode="+ ssircode+ "&frmdate=" + frmdate + "&todate=" + todate;

            }
        }
    }
}