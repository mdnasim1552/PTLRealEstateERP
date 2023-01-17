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

namespace RealERPWEB.F_12_Inv
{

    public partial class RptInterMatTransStatus : System.Web.UI.Page
    {

        ProcessAccess purData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Materials Transfer Status (Inter Company)";
                this.txtFDate.Text = DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txtToDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
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


        protected void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPURPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProName.DataTextField = "pactdesc";
            this.ddlProName.DataValueField = "pactcode";
            this.ddlProName.DataSource = ds1.Tables[0];
            this.ddlProName.DataBind();
            ds1.Dispose();
        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowTransStatus();
        }

        protected void imgbtnFindRefno_Click(object sender, EventArgs e)
        {
            this.ShowTransStatus();

        }

        protected void ShowTransStatus()
        {
            Session.Remove("tblMatTranStatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string dateFrom = this.txtFDate.Text.Trim();
            string dateTo = this.txtToDate.Text.Trim();
            string pactcode = ((this.ddlProName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProName.SelectedValue.ToString()) + "%";
            string SearchRefno = "%" + this.txtSrcRefNo.Text.Trim() + "%";
            //string TrnsTo = (this.chkProjectTrnsTo.Checked) ? "trnsto" : "";

            DataSet ds = purData.GetTransInfo(comcod, "SP_REPORT_MAT_TRANS", "INTERCOMMATTRANSSTATUS", dateFrom, dateTo, pactcode, SearchRefno, "", "", "", "", "");
            if (ds == null)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }

            Session["tblMatTranStatus"] = ds.Tables[0];
            this.grvacc_DataBind();

        }
        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc.PageIndex = ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
            this.grvacc_DataBind();
        }
        protected void grvacc_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["tblMatTranStatus"];
            this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvacc.DataSource = tbl1;
            this.grvacc.DataBind();
            if (tbl1.Rows.Count > 0)
            {
                ((Label)this.grvacc.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("sum(tamount)", "")) ?
                                        0 : tbl1.Compute("sum(tamount)", ""))).ToString("#,##0.00;(#,##0.00); ");
            }

        }



        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvacc.PageIndex = e.NewPageIndex;
            this.grvacc_DataBind();
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt = (DataTable)Session["tblMatTranStatus"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_12_Inv.PurEqisition.InterComMaterial>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptInterComTransStatus", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", "Project Name : " + this.ddlProName.SelectedItem.ToString()));
            Rpt1.SetParameters(new ReportParameter("actdesc", comnam));

            Rpt1.SetParameters(new ReportParameter("date", "(From " + Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtToDate.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            ////string hostname = hst["hostname"].ToString();

            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblMatTranStatus"];
            //DataSet ds = new DataSet();
            //ds.Merge(dt1);

            //var list = ds.Tables[0].DataTableToList<REALERPRDLC.RD_12_Inv.RptInvList.InterComMaterial>();

            //LocalReport Rpt1 = null;
            //Hashtable reportParm = new Hashtable();
            //reportParm["companyname"] = comnam.ToUpper();
            //reportParm["date"] = "(From " + Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtToDate.Text).ToString("dd-MMM-yyyy") + ")";
            //reportParm["actdesc"] = "Project Name : " + this.ddlProName.SelectedItem.ToString();
            //reportParm["txtuserinfo"] = "Print Source :" + compname + " , " + username + " , " + printdate;
            //Rpt1 = REALESTATESETUP.GetLocalReport("RD_12_Inv.RptInterComTransStatus", list, reportParm, null);

            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc_DataBind();
        }
    }
}
