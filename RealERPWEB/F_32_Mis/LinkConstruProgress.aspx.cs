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
using RealERPRDLC;
namespace RealERPWEB.F_32_Mis
{
    public partial class LinkConstruProgress : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00, bgdpercent = 0.00, bgdexepercent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                this.lblvalDate.Text = this.Request.QueryString["date"].ToString();
                this.lblvalproject.Text = this.Request.QueryString["pactdesc"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Floor Wise Construction Progress";
                this.ShowValue();


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }











        private void ShowValue()
        {
            this.lbljavascript.Text = "";
            this.Pnlnote.Visible = true;
            this.ShowConProgress();
        }



        private void ShowConProgress()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string date = this.Request.QueryString["date"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_MIS01", "RPTCONPROGRAM", pactcode, date, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvConPro.DataSource = null;
                this.gvConPro.DataBind();
                return;
            }

            Session["tblConPro"] = ds1.Tables[0];
            this.LoadGrid();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Floor Wise Construction Progress";
                string eventdesc = "Show Report";
                string eventdesc2 = this.Request.QueryString["pactdesc"].ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblConPro"];
            this.gvConPro.DataSource = dt;
            this.gvConPro.DataBind();
            this.FooterCalcul(dt);




            this.BindChartYear(dt);

        }

        private void BindChartYear(DataTable dt)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string toDate = this.Request.QueryString["date"].ToString();
            string pactcode = this.Request.QueryString["pactcode"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_MIS01", "RPTCONPROGRAMSUM", toDate, pactcode, "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                return;
            }




            //if (dt.Rows.Count == 0)
            //    return;

            //double bgdamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ? 0 : dt.Compute("sum(bgdamt)", "")));
            //double mplan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mplanat)", "")) ? 0 : dt.Compute("sum(mplanat)", "")));
            //double examt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(eamt)", "")) ? 0 : dt.Compute("sum(eamt)", "")));

            //percent = (mplan == 0 ? 0.00 : ((examt * 100) / mplan));
            //bgdpercent = (bgdamt == 0 ? 0.00 : ((mplan * 100) / bgdamt));
            //bgdexepercent = (bgdamt == 0 ? 0.00 : ((examt * 100) / bgdamt));


            //a.comcod, a.pactcode, a.bgdamt, a.mplan , a.mplanat, a.eamt, a.leamt, a.perontw, a.peronac,  peronlp= a.perontw-a.peronac, pactdesc=isnull(b.acttdesc,'')  
            DataTable drt = ds1.Tables[1];

            Chart1.Series["Series1"].YValueMembers = "perontw";
            Chart1.Series["Series2"].YValueMembers = "peronac";
            Chart1.Series["Series3"].YValueMembers = "peronlp";



            Chart1.Series["Series1"].LegendText = "Budgeted Execution";
            Chart1.Series["Series2"].LegendText = "Actual Execution";
            Chart1.Series["Series3"].LegendText = "Varriance";


            //Chart1.Legends["Series1"].Docking = Docking.Top;
            //Chart1.Legends["Series2"].Docking = Docking.Top;
            //Chart1.Legends["Series3"].Docking = Docking.Top;

            Chart1.DataSource = drt;
            Chart1.DataBind();

            Chart1.Series["Series1"].IsValueShownAsLabel = true;
            Chart1.Series["Series2"].IsValueShownAsLabel = true;
            Chart1.Series["Series3"].IsValueShownAsLabel = true;

            Chart1.Series["Series1"].LabelAngle = 90;
            Chart1.Series["Series1"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series1"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series1"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series1"]["PointWidth"] = "0.9";

            Chart1.Series["Series2"].LabelAngle = 90;
            Chart1.Series["Series2"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series2"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series2"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series2"]["PointWidth"] = "0.9";

            Chart1.Series["Series3"].LabelAngle = 90;
            Chart1.Series["Series3"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series3"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series3"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series3"]["PointWidth"] = "0.9";


        }
        private void FooterCalcul(DataTable dt)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (dt.Rows.Count == 0)
                return;


            double bgdamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ? 0 : dt.Compute("sum(bgdamt)", "")));
            double mplan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mplanat)", "")) ? 0 : dt.Compute("sum(mplanat)", "")));
            double examt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(eamt)", "")) ? 0 : dt.Compute("sum(eamt)", "")));

            percent = (mplan == 0 ? 0.00 : ((examt * 100) / mplan));
            bgdpercent = (bgdamt == 0 ? 0.00 : ((mplan * 100) / bgdamt));
            bgdexepercent = (bgdamt == 0 ? 0.00 : ((examt * 100) / bgdamt));


            ((Label)this.gvConPro.FooterRow.FindControl("lgvFBgdAmt")).Text = bgdamt.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvConPro.FooterRow.FindControl("lgvFMasPlan")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mplan)", "")) ?
                                0 : dt.Compute("sum(mplan)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((HyperLink)this.gvConPro.FooterRow.FindControl("hlnkgvFexAmt")).Text = examt.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvConPro.FooterRow.FindControl("lgvFMPlanastoday")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mplanat)", "")) ?
                                0 : dt.Compute("sum(mplanat)", ""))).ToString("#,##0.00;(#,##0.00); ");

            //((Label)this.gvConPro.FooterRow.FindControl("lgvFexAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(eamt)", "")) ?
            //                    0 : dt.Compute("sum(eamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((HyperLink)this.gvConPro.FooterRow.FindControl("hlnkgvFlessexAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(leamt)", "")) ?
                         0 : dt.Compute("sum(leamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvConPro.FooterRow.FindControl("lgvFWorkP")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(perwork)", "")) ?
                                0 : dt.Compute("sum(perwork)", ""))).ToString("#,##0.00;(#,##0.00); ") + "%";
            ((Label)this.gvConPro.FooterRow.FindControl("lgvFPercent")).Text = percent.ToString("#,##0.00;(#,##0.00); ") + "%";

            this.lPercentonbgd.Text = bgdpercent.ToString("#,##0.00;(#,##0.00); ") + "%";
            this.lPercentonbgdexe.Text = bgdexepercent.ToString("#,##0.00;(#,##0.00); ") + "%";


            string pactcode = this.Request.QueryString["pactcode"].ToString();
            // string pactdesc = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13) ;
            string frmdate = Convert.ToDateTime("01" + this.Request.QueryString["date"].ToString().Substring(2)).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.Request.QueryString["date"].ToString()).ToString("dd-MMM-yyyy");


            ((HyperLink)this.gvConPro.FooterRow.FindControl("hlnkgvFlessexAmt")).NavigateUrl = "~/F_32_Mis/LinkMis.aspx?Type=ImpPlan02&comcod=" + comcod + "&Pactcode=" + pactcode + "&Date1=" + frmdate + "&Date2=" + todate;
            ((HyperLink)this.gvConPro.FooterRow.FindControl("hlnkgvFexAmt")).NavigateUrl = "~/F_09_PImp/RptImpExeStatus.aspx?Type=BgdVSEx02&comcod=" + comcod + "&prjcode=" + pactcode + "&Date1=&Date2=";





        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            this.PrintBgsdVsExe();

        }

        private void PrintBgsdVsExe()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblConPro"];
            double bgdamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ? 0 : dt.Compute("sum(bgdamt)", "")));
            double mplan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mplanat)", "")) ? 0 : dt.Compute("sum(mplanat)", "")));
            double examt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(eamt)", "")) ? 0 : dt.Compute("sum(eamt)", "")));
            if (dt.Rows.Count == 0)
                return;

            percent = (mplan == 0 ? 0.00 : ((examt * 100) / mplan));
            bgdpercent = (bgdamt == 0 ? 0.00 : ((mplan * 100) / bgdamt));
            bgdexepercent = (bgdamt == 0 ? 0.00 : ((examt * 100) / bgdamt));

            string projectName = this.lblvalproject.Text;

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.CatWiseConProgress>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptConProgram", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Floor Wise Construction Progress"));
            Rpt1.SetParameters(new ReportParameter("projectName", projectName));
            Rpt1.SetParameters(new ReportParameter("date", "As On " + this.lblvalDate.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtPercent", percent.ToString("#,##0.00;(#,##0.00); ") + " %"));
            Rpt1.SetParameters(new ReportParameter("txtBPercent", bgdpercent.ToString("#,##0.00;(#,##0.00); ") + " %"));
            Rpt1.SetParameters(new ReportParameter("txtBExePrcent", bgdexepercent.ToString("#,##0.00;(#,##0.00); ") + " %"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblConPro"];
            //double bgdamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ? 0 : dt.Compute("sum(bgdamt)", "")));
            //double mplan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mplanat)", "")) ? 0 : dt.Compute("sum(mplanat)", "")));
            //double examt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(eamt)", "")) ? 0 : dt.Compute("sum(eamt)", "")));

            //percent = (mplan == 0 ? 0.00 : ((examt * 100) / mplan));
            //bgdpercent = (bgdamt == 0 ? 0.00 : ((mplan * 100) / bgdamt));
            //bgdexepercent = (bgdamt == 0 ? 0.00 : ((examt * 100) / bgdamt));

            //string projectName = this.lblvalproject.Text;
            //ReportDocument rptConPro = new RealERPRPT.R_32_Mis.RptConProgram();
            //TextObject rpttxtPrjName = rptConPro.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //rpttxtPrjName.Text = projectName;
            //TextObject rpttxtDate = rptConPro.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rpttxtDate.Text ="As On "+this.lblvalDate.Text.Trim();
            //TextObject rpttxtpercent = rptConPro.ReportDefinition.ReportObjects["txtpercent"] as TextObject;
            //rpttxtpercent.Text = percent.ToString("#,##0.00;(#,##0.00); ") + " %";
            //TextObject rpttxBtpercent = rptConPro.ReportDefinition.ReportObjects["txtbpercent"] as TextObject;
            //rpttxBtpercent.Text = bgdpercent.ToString("#,##0.00;(#,##0.00); ") + " %";

            //TextObject txtbexepercent = rptConPro.ReportDefinition.ReportObjects["txtbexepercent"] as TextObject;
            //txtbexepercent.Text = bgdexepercent.ToString("#,##0.00;(#,##0.00); ") + " %";
            //TextObject txtuserinfo = rptConPro.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptConPro.SetDataSource(dt);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Floor Wise Construction Progress";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.lblvalproject.Text;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptConPro.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rptConPro;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }


        protected void gvConPro_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvConPro.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvConPro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string mPACTCODE = this.Request.QueryString["pactcode"].ToString();
            string mFlrCode = ((Label)e.Row.FindControl("lblgvflrCode")).Text;
            string mTRNDAT1 = this.Request.QueryString["date"].ToString();




            hlink1.NavigateUrl = "~/F_32_Mis/RptLinkImpExeStatus.aspx?Type=BgdAll&comcod=" + comcod + "&pactcode=" + mPACTCODE + "&FlrCode=" + mFlrCode + "&Date1=" + mTRNDAT1;

        }

    }
}

