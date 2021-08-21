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
using System.Web.UI.DataVisualization;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_32_Mis
{
    public partial class LinkIndProjectStatus : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                ((Label)this.Master.FindControl("lblTitle")).Text = "PROJECT STATUS - AT A GALANCE";
                this.ShowData();



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
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            this.PrintProSummary();

        }

        private void PrintProSummary()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblprosummary"];

            //string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            //ReportDocument rptProSummary = new RealERPRPT.R_32_Mis.RptProSummary();
            //TextObject rpttxtPrjName = rptProSummary.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //rpttxtPrjName.Text = projectName;
            //TextObject rpttxtDate = rptProSummary.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rpttxtDate.Text ="Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptProSummary.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Project Summary - At a glance";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.ddlProjectName.SelectedItem.Text.Substring(13); ;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //rptProSummary.SetDataSource(dt);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptProSummary.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rptProSummary;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void ShowData()
        {
            Session.Remove("tblprosummary");
            string comcod = this.GetCompCode();
            string projectcode = this.Request.QueryString["prjcode"].ToString();
            string date = this.Request.QueryString["date"].ToString();
            string fdate = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(fdate).AddMonths(1).AddDays(-1).ToString();
            DataSet ds = purData.GetTransInfo(comcod, "SP_REPORT_MIS05", "RPTPRODETAILSINFO", projectcode, date, todate, "", "", "", "", "", "");

            if (ds == null) return;
            Session["tblprosummary"] = ds.Tables[0];
            this.lblvalprojectname.Text = ds.Tables[2].Rows[0]["actdesc"].ToString().Substring(4);
            DataTable dtp = ds.Tables[1];
            this.lblvalstartdate.Text = ((dtp.Select("gcod=01003")).Length == 0) ? "" : dtp.Select("gcod=01003")[0]["gval"].ToString();
            this.lblvalconsarea.Text = ((dtp.Select("gcod=01001")).Length == 0) ? "" : dtp.Select("gcod=01001")[0]["gval"].ToString();
            this.lblvalstoried.Text = ((dtp.Select("gcod=02008")).Length == 0) ? "" : dtp.Select("gcod=02008")[0]["gval"].ToString();
            this.lblvallandarea.Text = ((dtp.Select("gcod=02005")).Length == 0) ? "" : dtp.Select("gcod=02005")[0]["gval"].ToString();
            this.lblvalhandoverdate.Text = ((dtp.Select("gcod=01004")).Length == 0) ? "" : dtp.Select("gcod=01004")[0]["gval"].ToString();
            this.lblvalsalablearea.Text = ((dtp.Select("gcod=01002")).Length == 0) ? "" : dtp.Select("gcod=01002")[0]["gval"].ToString();
            this.lblvallocation.Text = ((dtp.Select("gcod=02002")).Length == 0) ? "" : dtp.Select("gcod=02002")[0]["gval"].ToString();
            this.Data_Bind();
            this.ShowBarChart();





        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblprosummary"];
            this.gv01.DataSource = dt;
            this.gv01.DataBind();

        }
        private void ShowBarChart()
        {
            //a.comcod, a.pactcode, a.bgdamt, a.mplan , a.mplanat, a.eamt, a.leamt, a.perontw, a.peronac,  peronlp= a.perontw-a.peronac, pactdesc=isnull(b.acttdesc,'')  
            DataTable drt = ((DataTable)Session["tblprosummary"]).Copy();
            double fblockamt = Convert.ToDouble(drt.Rows[1]["fblock"]);
            DataView dv = drt.DefaultView;
            dv.RowFilter = ("grp='1'");
            drt = dv.ToTable();
            Chart1.Series["Series1"].YValueMembers = "bgdsales";
            Chart1.Series["Series2"].YValueMembers = "bgdcost";
            Chart1.Series["Series3"].YValueMembers = "bgdmar";
            Chart1.Series["Series4"].YValueMembers = "salam";
            Chart1.Series["Series5"].YValueMembers = "collam";
            Chart1.Series["Series6"].YValueMembers = "cdueam";
            Chart1.Series["Series7"].YValueMembers = "pdueam";
            Chart1.Series["Series8"].YValueMembers = "cbgdcost";
            Chart1.Series["Series9"].YValueMembers = "cprogress";
            Chart1.Series["Series10"].YValueMembers = "cdelay";
            Chart1.Series["Series11"].YValueMembers = "invamt";
            Chart1.Series["Series12"].YValueMembers = "liaam";
            Chart1.Series["Series13"].YValueMembers = "rinflow";
            Chart1.Series["Series14"].YValueMembers = "routflow";
            Chart1.Series["Series15"].YValueMembers = "fblock";



            //    bgdsales, bgdcost, bgdmar, salam, collam , cbgdcost, cprogress, cdelay, pdueam, cdueam,invamt, 
            //rinflow, routflow,


            //Chart1.Series["Series1"].LegendText = "Budgeted Sales";
            //Chart1.Series["Series2"].LegendText = "Budgeted Cost";
            //Chart1.Series["Series3"].LegendText = "Budgeted Margin in %";
            //Chart1.Series["Series4"].LegendText = "Sales Completed";
            //Chart1.Series["Series5"].LegendText = "Collection";
            //Chart1.Series["Series6"].LegendText = "Dues";
            //Chart1.Series["Series7"].LegendText = "Over Dues";
            //Chart1.Series["Series8"].LegendText = "Construction Budget";
            //Chart1.Series["Series9"].LegendText = "Construction Progress";
            //Chart1.Series["Series10"].LegendText = "Construction Delay";
            //Chart1.Series["Series11"].LegendText = "Investment in Project";
            //Chart1.Series["Series12"].LegendText = "Liabilities";
            //Chart1.Series["Series13"].LegendText = "Remaining Inflow";
            //Chart1.Series["Series14"].LegendText = "Remaining Outflow";
            //Chart1.Series["Series15"].LegendText = fblockamt < 0 ? "Fund Blocked" : "Fund Generated";


            //Chart1.Legends["Series1"].Docking = Docking.Top;
            //Chart1.Legends["Series2"].Docking = Docking.Top;
            //Chart1.Legends["Series3"].Docking = Docking.Top;

            // Chart1.ChartAreas.ax

            //Chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.White;
            //Chart1.ChartAreas[0].AxisX.MinorGrid.LineColor = System.Drawing.Color.White;


            // Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;

            Chart1.DataSource = drt;
            Chart1.DataBind();
            //   Chart1.ChartAreas[0].AxisX.MinorGrid.LineDashStyle = this.Chart1.dataCharting.ChartDashStyle.Dash;
            Chart1.Series["Series1"].IsValueShownAsLabel = true;
            Chart1.Series["Series2"].IsValueShownAsLabel = true;
            Chart1.Series["Series3"].IsValueShownAsLabel = true;
            Chart1.Series["Series4"].IsValueShownAsLabel = true;
            Chart1.Series["Series5"].IsValueShownAsLabel = true;
            Chart1.Series["Series6"].IsValueShownAsLabel = true;
            Chart1.Series["Series7"].IsValueShownAsLabel = true;
            Chart1.Series["Series8"].IsValueShownAsLabel = true;
            Chart1.Series["Series9"].IsValueShownAsLabel = true;
            Chart1.Series["Series10"].IsValueShownAsLabel = true;
            Chart1.Series["Series11"].IsValueShownAsLabel = true;
            Chart1.Series["Series12"].IsValueShownAsLabel = true;
            Chart1.Series["Series13"].IsValueShownAsLabel = true;
            Chart1.Series["Series14"].IsValueShownAsLabel = true;
            Chart1.Series["Series15"].IsValueShownAsLabel = true;




            Chart1.Series["Series1"].LabelAngle = 90;
            Chart1.Series["Series1"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series1"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series1"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series1"]["PointWidth"] = "2";
            // Chart1.Series["Series1"]["groupPadding"] = "2";

            Chart1.Series["Series2"].LabelAngle = 90;
            Chart1.Series["Series2"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series2"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series2"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series2"]["PointWidth"] = "2";
            //  Chart1.Series["Series1"]["groupPadding"] = "2";

            Chart1.Series["Series3"].LabelAngle = 90;
            Chart1.Series["Series3"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series3"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series3"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series3"]["PointWidth"] = "2";


            Chart1.Series["Series4"].LabelAngle = 90;
            Chart1.Series["Series4"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series4"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series4"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series4"]["PointWidth"] = "2";

            Chart1.Series["Series5"].LabelAngle = 90;
            Chart1.Series["Series5"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series5"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series5"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series5"]["PointWidth"] = "2";

            Chart1.Series["Series6"].LabelAngle = 90;
            Chart1.Series["Series6"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series6"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series6"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series6"]["PointWidth"] = "2";

            Chart1.Series["Series7"].LabelAngle = 90;
            Chart1.Series["Series7"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series7"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series7"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series7"]["PointWidth"] = "2";

            Chart1.Series["Series8"].LabelAngle = 90;
            Chart1.Series["Series8"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series8"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series8"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series8"]["PointWidth"] = "2";

            Chart1.Series["Series9"].LabelAngle = 90;
            Chart1.Series["Series9"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series9"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series9"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series9"]["PointWidth"] = "2";

            Chart1.Series["Series10"].LabelAngle = 90;
            Chart1.Series["Series10"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series10"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series10"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series10"]["PointWidth"] = "2";

            Chart1.Series["Series11"].LabelAngle = 90;
            Chart1.Series["Series11"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series11"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series11"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series11"]["PointWidth"] = "2";

            Chart1.Series["Series12"].LabelAngle = 90;
            Chart1.Series["Series12"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series12"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series12"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series12"]["PointWidth"] = "2";

            Chart1.Series["Series13"].LabelAngle = 90;
            Chart1.Series["Series13"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series13"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series13"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series13"]["PointWidth"] = "2";

            Chart1.Series["Series14"].LabelAngle = 90;
            Chart1.Series["Series14"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series14"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series14"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series14"]["PointWidth"] = "2";

            Chart1.Series["Series15"].LabelAngle = 90;
            Chart1.Series["Series15"]["BarLabelStyle"] = "Center";
            Chart1.Series["Series15"]["DrawingStyle"] = "Cylinder";
            Chart1.Series["Series15"]["ShowMarkerLines"] = "true";
            Chart1.Series["Series15"]["PointWidth"] = "2";
            Chart1.Series["Series15"].Color = fblockamt < 0 ? System.Drawing.Color.FromArgb(255, 0, 0) : System.Drawing.Color.FromArgb(0, 128, 0);



            // Chart1.Series["Series15"].Color = System.Drawing.Color.FromArgb(0, 128, 0);


            // System.Drawing.Color.FromArgb(255, 0, 0);

        }




        protected void gv01_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkgvBgdsales = (HyperLink)e.Row.FindControl("hlnkgvBgdsales");
                HyperLink hlnkgvBgdCost = (HyperLink)e.Row.FindControl("hlnkgvBgdCost");
                HyperLink hlnkgvsalam = (HyperLink)e.Row.FindControl("hlnkgvsalam");
                HyperLink hlnkgvconsbgd = (HyperLink)e.Row.FindControl("hlnkgvconsbgd");
                HyperLink hlnkgvconspro = (HyperLink)e.Row.FindControl("hlnkgvconspro");
                HyperLink hlnkgvinvamt = (HyperLink)e.Row.FindControl("hlnkgvinvamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "2")
                {

                    string actcode = this.Request.QueryString["prjcode"].ToString();
                    string sactcode = "18" + this.Request.QueryString["prjcode"].ToString().Substring(2);
                    string sconactcode = "31" + this.Request.QueryString["prjcode"].ToString().Substring(2);
                    hlnkgvBgdsales.NavigateUrl = (ASTUtility.Left(comcod, 1) == "1") ? ("~/F_16_Bill/BillingRateEntry.aspx?Type=Entry&prjcode=" + actcode) : ("~/F_22_Sal/RptSaleSoldunsoldUnit.aspx?Type=soldunsold&comcod=" + comcod + "&prjcode=" + sactcode);
                    hlnkgvBgdsales.Attributes["style"] = "color:blue;";
                    hlnkgvBgdCost.NavigateUrl = "~/F_04_Bgd/RptBgdPrjoject.aspx?Type=MasterBgdAcWk&comcod=" + comcod + "&prjcode=" + actcode;
                    hlnkgvBgdCost.Attributes["style"] = "color:blue;";

                    hlnkgvsalam.NavigateUrl = (ASTUtility.Left(comcod, 1) == "1") ? ("~/F_41_GAcc/RptProBillStatus.aspx?Type=Billstatus&prjcode=" + sconactcode) : ("~/F_23_CR/RptReceivedList02.aspx?Type=DuesCollect&prjcode=" + sactcode);
                    hlnkgvsalam.Attributes["style"] = "color:blue;";




                    hlnkgvconsbgd.NavigateUrl = "~/F_04_Bgd/BgdPrjAna.aspx?InputType=BgdMainRptALL&prjcode=" + actcode;
                    hlnkgvconsbgd.Attributes["style"] = "color:blue;";


                    hlnkgvconspro.NavigateUrl = "~/F_32_Mis/LinkConstruProgress.aspx?pactcode=" + actcode + "&pactdesc=" + this.lblvalprojectname.Text.Trim() + "&date=" + this.Request.QueryString["date"].ToString(); ;
                    hlnkgvconspro.Attributes["style"] = "color:blue;";

                    hlnkgvinvamt.NavigateUrl = "~/F_32_Mis/ProjTrialBalanc.aspx?Type=PrjTrailBal&prjcode=" + actcode;
                    hlnkgvinvamt.Attributes["style"] = "color:blue;";


                }
                else
                {
                    hlnkgvBgdsales.Attributes["style"] = "color:black;";
                    hlnkgvBgdCost.Attributes["style"] = "color:black;";
                    hlnkgvsalam.Attributes["style"] = "color:black;";
                    hlnkgvconsbgd.Attributes["style"] = "color:black;";
                    hlnkgvconspro.Attributes["style"] = "color:black;";
                    hlnkgvinvamt.Attributes["style"] = "color:black;";

                }
                //if (torevnue == 0.00 && ASTUtility.Right(code, 4) != "AAAA")

            }
        }
        protected void gv01_RowCreated(object sender, GridViewRowEventArgs e)
        {

            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;
                cell01.Attributes["style"] = "font-weight:bold; font-size:12px; background-color:BlueViolet; color:white; ";




                TableCell cell04 = new TableCell();
                cell04.Text = "Budgeted";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 3;
                cell04.Attributes["style"] = "font-weight:bold; font-size:12px; background-color:BlueViolet; color:white;";



                TableCell cell05 = new TableCell();
                cell05.Text = "Sales";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 4;
                cell05.Attributes["style"] = "font-weight:bold; font-size:12px; background-color:Gray; color:white;";



                TableCell cell06 = new TableCell();
                cell06.Text = "Construction";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 3;
                cell06.Attributes["style"] = "font-weight:bold; font-size:12px; background-color:#993366; color:white; ";


                TableCell cell07 = new TableCell();
                cell07.Text = "Accounts";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.ColumnSpan = 4;
                cell07.Attributes["style"] = "font-weight:bold; font-size:12px; background-color:#009999; color:white;";




                TableCell cell08 = new TableCell();
                cell08.Text = " Fund";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.ColumnSpan = 1;
                cell08.Attributes["style"] = "font-weight:bold; font-size:12px; background-color:#5CB85C; color:white; ";

                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvrow.Cells.Add(cell06);
                gvrow.Cells.Add(cell07);
                gvrow.Cells.Add(cell08);



                gv01.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
    }
}

