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
namespace RealERPWEB.F_14_Pro
{
    public partial class LinkRptPurchaseStatusUr : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string url = HttpContext.Current.Request.Url.AbsoluteUri.ToString();
                //int index1 = (url.Contains("&")) ? url.IndexOf('&') : url.Length;
                //int index2 = (url.Contains("&")) ? url.Substring(index1 + 1).IndexOf('&') : 0;

                //int indexofamp = index1 + (index2 > 0 ? index2 + 1 : index2);

                //if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                //        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                //    Response.Redirect("../AcceessError.aspx");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //CommonButton();

                string title = "Budget Tracking";
                ((Label)this.Master.FindControl("lblTitle")).Text = title;
                this.Master.Page.Title = title;

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttodate.Text = date;
                this.txtFDate.Text =  "01" + date.Substring(2);

                //this.ShowView();
                //this.GetOrderNo();
                this.lbtnOk_Click(null, null);

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
            return comcod;
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rpt = this.Request.QueryString["Rpt"].ToString().Trim();

            this.RptBgdBal();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase Status";
                string eventdesc = "Print Report: " + rpt;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void RptBgdBal()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = System.DateTime.Now.ToString("dd.MM.yyyy");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            
            DataTable dt = ((DataTable)Session["tblpurchase"]);
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.EclassBudgetTracking>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptBudgetTracking", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Rptname", "Budget Tracking"));
            Rpt1.SetParameters(new ReportParameter("comlogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("project", ""));
            Rpt1.SetParameters(new ReportParameter("material", ""));
            Rpt1.SetParameters(new ReportParameter("opening", this.lblvalOpenig.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("bgdqty", this.lblvalBudget.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("transfer", this.lblvaltrans.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("ttlsuply", this.lblvalTotalSupp.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("balqty", this.lblvalBalance.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("printFooter", txtuserinfo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                Session.Remove("tblpurchase");
                string pactcode = this.Request.QueryString["pactcode"].ToString();
                string rescode = this.Request.QueryString["rsircode"].ToString();
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTBUDGETBAL", pactcode, rescode, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvBgdBal.DataSource = null;
                    this.gvBgdBal.DataBind();
                    return;
                }
                Session["tblpurchase"] = ds1.Tables[0];
                this.lblvalBudget.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["bgdqty"]).ToString("#,##0;(#,##0); ");
                this.lblvalOpenig.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opqty"]).ToString("#,##0;(#,##0); ");
                this.lbltxtvaldqty.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["dqty"]).ToString("#,##0;(#,##0); ");
                this.lblvalRequisition.Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(areqty)", "")) ?
                                             0 : ds1.Tables[0].Compute("sum(areqty)", ""))).ToString("#,##0;(#,##0); ");
                this.lblvaltrans.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["trnqty"]).ToString("#,##0;(#,##0); ");
                this.lblvalTotalSupp.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["tosupqty"]).ToString("#,##0;(#,##0); ");
                this.lblvalBalance.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["bgdbal"]).ToString("#,##0;(#,##0); ");
                this.LoadGrid();
            }

            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }

        }

        private void LoadGrid()
        {

            try
            {
                DataTable dt = ((DataTable)Session["tblpurchase"]).Copy();
                string comcod = this.GetComeCode();
                if ((dt.Rows.Count == 0)) //Problem
                    return;

                this.gvBgdBal.DataSource = dt;
                this.gvBgdBal.DataBind();

                ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFareqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(areqty)", "")) ?
                                        0 : dt.Compute("sum(areqty)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFprogqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(progqty)", "")) ?
                                    0 : dt.Compute("sum(progqty)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFordrqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordrqty)", "")) ?
                                    0 : dt.Compute("sum(ordrqty)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFmrrqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrrqty)", "")) ?
                                    0 : dt.Compute("sum(mrrqty)", ""))).ToString("#,##0;(#,##0); ");

                ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFordradjqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oadjqty)", "")) ?
                                   0 : dt.Compute("sum(oadjqty)", ""))).ToString("#,##0;(#,##0); ");
            }
            catch (Exception ex)
            {


            }
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {

        }
    }
}