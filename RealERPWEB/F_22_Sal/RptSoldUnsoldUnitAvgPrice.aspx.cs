﻿using System;
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
using RealERPRDLC;

namespace RealERPWEB.F_22_Sal
{
    public partial class RptSoldUnsoldUnitAvgPrice : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
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

                //((Label)this.Master.FindControl("lblTitle")).Text = "Sold & Unsold Unit (Average Price)";

                //var dtoday = System.DateTime.Today;
                this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtfrmdate.Text = new System.DateTime(dtoday.Year, dtoday.Month, 1).ToString("dd-MMM-yyyy");
                this.ProjectName();
                
                //this.GetGroup();


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
            return (hst["comcod"].ToString());

        }




        private void ProjectName()
        {
            string comcod = this.GetComeCode();
            string Srchpactcode = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT03", "GETPROJECTNAME", Srchpactcode, "", "", "", "", "", "", "", "");


            DataTable dt = ds1.Tables[0];
            DataRow dr1 = dt.NewRow();
            dr1["pactcode"] = "000000000000";
            dr1["pactdesc"] = "All Project";           
            dt.Rows.Add(dr1);

            //this.DropCheck1.DataTextField = "pactdesc1";
            //this.DropCheck1.DataValueField = "pactdesc1";
            //this.DropCheck1.DataSource = dt;
            //this.DropCheck1.Text = "000000000000-All Project";
            //this.DropCheck1.DataBind();

            this.chkProjectName.DataTextField = "pactdesc";
            this.chkProjectName.DataValueField = "pactcode";
            this.chkProjectName.DataSource = dt;
            this.chkProjectName.DataBind();


        }

        //private void GetGroup()
        //{
        //    string comcod = this.GetComeCode();
        //    DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT02", "GETGROUP", "", "", "", "", "", "", "", "", "");
        //    this.ddlgrp.DataTextField = "grpdesc";
        //    this.ddlgrp.DataValueField = "grpcode";
        //    this.ddlgrp.DataSource = ds1.Tables[0];
        //    this.ddlgrp.DataBind();


        //}







        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblsoldunsoldavg");
            string comcod = this.GetComeCode();

            string prjcode = "";
            string gp = this.chkProjectName.SelectedValue.Trim();
            if (gp.Length > 0)
            {
                if (gp.Trim() == "000000000000" || gp.Trim() == "")
                    prjcode = "";
                else
                    foreach (ListItem s1 in chkProjectName.Items)
                    {
                        if (s1.Selected)
                        {
                            prjcode = prjcode + s1.Value.Substring(0, 12);
                        }
                    }

            }

           
            string type = rbtnStatus.SelectedValue.ToString();         
            string date = this.txtfrmdate.Text.Trim();       
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT02", "GETSOLDUNSOLDAVERAGEPRICE", prjcode, date, type, "", "", "", "", "", "");
            if (ds1 == null)
            {
                //this.gvsoldunsold.DataSource = null;
                //this.gvsoldunsold.DataBind();

                return;
            }

            Session["tblsoldunsoldavg"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {

            this.gvsoldunsoldavg.DataSource = (DataTable)Session["tblsoldunsoldavg"];
            this.gvsoldunsoldavg.DataBind();
            //this.FooterCal();

            //Session["Report1"] = gvothcoll;
            //((HyperLink)this.gvothcoll.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblgrpsoldunsold"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvsoldunsoldavg.FooterRow.FindControl("lblFsalableunit")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tqty)", "")) ? 0.00 :
                dt.Compute("sum(tqty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsoldunsoldavg.FooterRow.FindControl("lblFtotalsize")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tusize)", "")) ? 0.00 :
              dt.Compute("sum(tusize)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsoldunsoldavg.FooterRow.FindControl("lblFsoldunit")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(sqty)", "")) ? 0.00 :
             dt.Compute("sum(sqty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsoldunsoldavg.FooterRow.FindControl("lblFsoldarea")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(susize)", "")) ? 0.00 :
             dt.Compute("sum(susize)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvsoldunsoldavg.FooterRow.FindControl("lblFunsoldunit")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(usqty)", "")) ? 0.00 :
            dt.Compute("sum(usqty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsoldunsoldavg.FooterRow.FindControl("lblFgvunsoldarea")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(usize)", "")) ? 0.00 :
            dt.Compute("sum(usize)", ""))).ToString("#,##0.00;(#,##0); ");

            ((Label)this.gvsoldunsoldavg.FooterRow.FindControl("lblFgvtsoldamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(suamt)", "")) ? 0.00 :
            dt.Compute("sum(suamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsoldunsoldavg.FooterRow.FindControl("lblFgvunsoldamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(usuamt)", "")) ? 0.00 :
            dt.Compute("sum(usuamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsoldunsoldavg.FooterRow.FindControl("lblFgvcarparking")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(parking)", "")) ? 0.00 :
            dt.Compute("sum(parking)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsoldunsoldavg.FooterRow.FindControl("lblFgvutility")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(utility)", "")) ? 0.00 :
            dt.Compute("sum(utility)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsoldunsoldavg.FooterRow.FindControl("lblFgvcooprative")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cooperative)", "")) ? 0.00 :
            dt.Compute("sum(cooperative)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsoldunsoldavg.FooterRow.FindControl("lblFgvtcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tsalamt)", "")) ? 0.00 :
            dt.Compute("sum(tsalamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsoldunsoldavg.FooterRow.FindControl("lblFgvreceived")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tramt)", "")) ? 0.00 :
            dt.Compute("sum(tramt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsoldunsoldavg.FooterRow.FindControl("lblFgvbalance")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balance)", "")) ? 0.00 :
            dt.Compute("sum(balance)", ""))).ToString("#,##0;(#,##0); ");

            //Session["Report1"] = gvsoldunsold;
            //((HyperLink)this.gvsoldunsold.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
         
            DataTable dt = (DataTable)Session["tblsoldunsoldavg"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales.SoldUnsoltInfavg>();
           
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptSoldUnsoldUnitAvgPrice", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printdate", printdate));
           // Rpt1.SetParameters(new ReportParameter("projectName", projectName));
            Rpt1.SetParameters(new ReportParameter("RptTitle", ""+ this.rbtnStatus.SelectedValue+ " Unit (Average Price)"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("date", "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


    }

}