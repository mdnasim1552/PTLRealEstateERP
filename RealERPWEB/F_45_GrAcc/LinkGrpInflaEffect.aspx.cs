using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web.UI.DataVisualization.Charting;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_45_GrAcc
{

    public partial class LinkGrpInflaEffect : System.Web.UI.Page
    {
        ProcessAccess prjData = new ProcessAccess();
        public static double totaramt = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.txtdate.Text = this.Request.QueryString["Date2"].ToString();
                this.GetProjectName();
                this.gvVisibility();
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "Const") ? "Project Cost Per SFT" :
                    (this.Request.QueryString["Type"].ToString() == "ProTarVsAchievement") ? "Construction Target Vs. Achievement"
                    : (this.Request.QueryString["Type"].ToString() == "RemainingCost") ? "Additional Budget for Influation" : "Cost of Sales Per SFT";


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

            return (this.Request.QueryString["comcod"].ToString());

        }

        private void gvVisibility()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Const":
                    this.gvCost.Columns[7].Visible = false;
                    this.gvCost.Columns[8].Visible = false;
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "Sales":
                    this.gvCost.Columns[5].Visible = false;
                    this.gvCost.Columns[6].Visible = false;
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "RemainingCost":
                    this.lblgroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;


                case "ProTarVsAchievement":
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.lblDate.Visible = false;
                    this.txtdate.Visible = false;
                    this.lblgroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.MultiView1.ActiveViewIndex = 2;
                    break;






            }




        }

        private void GetProjectName()
        {

            string comcod = this.GetComeCode();
            string serch1 = this.txtSearch.Text.Trim() + "%";
            DataSet ds1 = prjData.GetTransInfo(comcod, "SP_REPORT_PRJ_INFO", "GETEXPRJNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlAccProject.DataTextField = "actdesc";
            this.ddlAccProject.DataValueField = "actcode";
            this.ddlAccProject.DataSource = ds1.Tables[0];
            this.ddlAccProject.DataBind();

        }

        protected void ImgbtnFindProj_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Const":
                case "Sales":
                    this.ShowCostASalesPerSFT();
                    break;
                case "RemainingCost":
                    this.ShowRemainingCost();
                    break;

                case "ProTarVsAchievement":
                    this.ShowProtarvsAchievement();
                    break;
            }

            if (ConstantInfo.LogStatus == true)
            {
                string comcod = this.GetComeCode();
                string eventtype = "Project Cost Info";
                string eventdesc = "Show Report: " + type;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




        }

        private void ShowCostASalesPerSFT()
        {

            ViewState.Remove("tblcost");
            string comcod = this.GetComeCode();
            string actcode = this.ddlAccProject.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            DataSet ds1 = prjData.GetTransInfo(comcod, "SP_REPORT_PRJ_INFO", "RPTPROJECTCOST", actcode, date, mRptGroup, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.lbltxtCArea.Text = "";
                this.gvCost.DataSource = null;
                this.gvCost.DataBind();
                return;

            }
            this.lblConArea.Visible = true;
            this.lbltxtCArea.Visible = true;
            ViewState["tblcost"] = ds1.Tables[0];
            if (ds1.Tables[1].Rows.Count > 0)
            {

                this.lbltxtCArea.Text = (this.Request.QueryString["Type"].ToString().Trim() == "Const") ? Convert.ToDouble(ds1.Tables[1].Rows[0]["conarea"]).ToString("#,#0.00;(#,#0.00); ") + " " + ds1.Tables[1].Rows[0]["conunit"].ToString() : Convert.ToDouble(ds1.Tables[1].Rows[0]["salarea"]).ToString("#,#0.00;(#,#0.00); ") + " " + ds1.Tables[1].Rows[0]["salunit"].ToString();
                this.lblConArea.Text = (this.Request.QueryString["Type"].ToString().Trim() == "Const") ? "Construction Area:" : "Selable Area:";

            }
            else
            {
                this.lbltxtCArea.Text = "";
            }

            this.Data_Bind();
        }
        private void ShowRemainingCost()
        {
            ViewState.Remove("tblcost");
            string comcod = this.GetComeCode();
            string actcode = this.ddlAccProject.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = prjData.GetTransInfo(comcod, "SP_REPORT_PRJ_INFO", "RPTPROJECTREMAINCOST", actcode, date, "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvRCost.DataSource = null;
                this.gvRCost.DataBind();
                return;

            }

            ViewState["tblcost"] = ds1.Tables[0];
            ///////-----------------Note-------------------------------//////////
            if (ds1.Tables[1].Rows.Count != 0)
            {
                this.PanelNote.Visible = true;

                this.lblOrgBgdVal.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["amount"]).ToString("#,##0;(#,##0); ");
                this.lblOrgBgdCon.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["tconsft"]).ToString("#,##0;(#,##0); ");
                this.lblOrgBgdConSFT.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["consft"]).ToString("#,##0.00;(#,##0.00); ");
                this.lblOrgBgdSal.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["tsalsft"]).ToString("#,##0;(#,##0); ");
                this.lblOrgBgdSFT.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["salsft"]).ToString("#,##0.00;(#,##0.00); ");
                this.lblOrgBgdPr.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["inper"]).ToString("#,##0;(#,##0); ") + " %"; ;

                this.lblRevBgdAmVal.Text = Convert.ToDouble(ds1.Tables[1].Rows[1]["amount"]).ToString("#,##0;(#,##0); ");
                this.lblRevPriIncVal.Text = Convert.ToDouble(ds1.Tables[1].Rows[2]["amount"]).ToString("#,##0;(#,##0); ");
                this.lblRevPriDecVal.Text = Convert.ToDouble(ds1.Tables[1].Rows[3]["amount"]).ToString("#,##0;(#,##0); ");
                this.lblRevRemPurVal.Text = Convert.ToDouble(ds1.Tables[1].Rows[4]["amount"]).ToString("#,##0;(#,##0); ");

                this.lblRevTotalVal.Text = Convert.ToDouble(ds1.Tables[1].Rows[5]["amount"]).ToString("#,##0;(#,##0); ");
                this.lblRevCon.Text = Convert.ToDouble(ds1.Tables[1].Rows[5]["tconsft"]).ToString("#,##0;(#,##0); ");
                this.lblRevConSFT.Text = Convert.ToDouble(ds1.Tables[1].Rows[5]["consft"]).ToString("#,##0.00;(#,##0.00); ");
                this.lblRevSal.Text = Convert.ToDouble(ds1.Tables[1].Rows[5]["tsalsft"]).ToString("#,##0;(#,##0); ");
                this.lblRevSalSFT.Text = Convert.ToDouble(ds1.Tables[1].Rows[5]["salsft"]).ToString("#,##0.00;(#,##0.00); ");
                this.lblRevInPr.Text = Convert.ToDouble(ds1.Tables[1].Rows[5]["inper"]).ToString("#,##0;(#,##0); ") + " %"; ;

                this.lblIncVal.Text = Convert.ToDouble(ds1.Tables[1].Rows[6]["amount"]).ToString("#,##0;(#,##0); ");
                this.lblInccon.Text = Convert.ToDouble(ds1.Tables[1].Rows[6]["tconsft"]).ToString("#,##0;(#,##0); ");
                this.lblIncconSFT.Text = Convert.ToDouble(ds1.Tables[1].Rows[6]["consft"]).ToString("#,##0.00;(#,##0.00); ");
                this.lblIncSal.Text = Convert.ToDouble(ds1.Tables[1].Rows[6]["tsalsft"]).ToString("#,##0;(#,##0); ");
                this.lblIncSalSFT.Text = Convert.ToDouble(ds1.Tables[1].Rows[6]["salsft"]).ToString("#,##0.00;(#,##0.00); ");
                this.lblIncPr.Text = Convert.ToDouble(ds1.Tables[1].Rows[6]["inper"]).ToString("#,##0;(#,##0); ") + " %";

            }
            if (ds1.Tables[2].Rows.Count != 0)
            {
                this.lblNAmt.Text = Convert.ToDouble(ds1.Tables[2].Rows[0]["amount"]).ToString("#,##0;(#,##0); ");
                this.lblNAmt1.Text = Convert.ToDouble(ds1.Tables[2].Rows[1]["amount"]).ToString("#,##0;(#,##0); ");
                this.lblNAmt2.Text = Convert.ToDouble(ds1.Tables[2].Rows[2]["amount"]).ToString("#,##0;(#,##0); ");
                this.lblNAmt3.Text = Convert.ToDouble(ds1.Tables[2].Rows[3]["amount"]).ToString("#,##0;(#,##0); ");
                this.lblNAmt4.Text = Convert.ToDouble(ds1.Tables[2].Rows[4]["amount"]).ToString("#,##0;(#,##0); ");
                this.lblNAmt5.Text = Convert.ToDouble(ds1.Tables[2].Rows[5]["amount"]).ToString("#,##0;(#,##0); ");
                this.lblNAmt6.Text = Convert.ToDouble(ds1.Tables[2].Rows[6]["amount"]).ToString("#,##0;(#,##0); ");

                this.lblNAmt8.Text = Convert.ToDouble(ds1.Tables[2].Rows[7]["amount"]).ToString("#,##0;(#,##0); ");
                this.lblNAmt9.Text = Convert.ToDouble(ds1.Tables[2].Rows[8]["amount"]).ToString("#,##0;(#,##0); ");
                this.lblNAmt10.Text = Convert.ToDouble(ds1.Tables[2].Rows[9]["amount"]).ToString("#,##0;(#,##0); ");
                this.lblNAmt11.Text = Convert.ToDouble(ds1.Tables[2].Rows[10]["amount"]).ToString("#,##0;(#,##0); ");
                this.lblNAmt12.Text = Convert.ToDouble(ds1.Tables[2].Rows[11]["amount"]).ToString("#,##0;(#,##0); ");


            }

            this.Data_Bind();


        }


        private void ShowProtarvsAchievement()
        {
            this.pnlTarVsAchievement.Visible = true;
            ViewState.Remove("tblcost");
            string comcod = this.GetComeCode();
            string actcode = this.ddlAccProject.SelectedValue.ToString();
            DataSet ds1 = prjData.GetTransInfo(comcod, "SP_REPORT_PRJ_INFO", "RPTTARVSACHIEVEMENT", actcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvtarvsachivement.DataSource = null;
                this.gvtarvsachivement.DataBind();
                return;

            }

            this.lblStartDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["prjstrtdat"]).ToString("ddd-MMM-yyyy");
            this.lblEndDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["prjenddat"]).ToString("dd-MMM-yyyy");
            this.lblDuration.Text = Convert.ToInt32(ds1.Tables[1].Rows[0]["duration"]).ToString("#,#0;(#,#0); ");
            totaramt = Convert.ToDouble(ds1.Tables[0].Rows[(ds1.Tables[0].Rows.Count) - 1]["comtamt"]);
            ViewState["tblcost"] = ds1.Tables[0];
            this.Data_Bind();



        }




        private void Data_Bind()
        {
            string comcod = this.GetComeCode();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Const":
                case "Sales":
                    this.gvCost.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvCost.DataSource = (DataTable)ViewState["tblcost"];
                    if (ASTUtility.Left(comcod, 1) == "2")
                    {
                        this.gvCost.Columns[5].HeaderText = "Budgeted Cost/KHATA";
                        this.gvCost.Columns[6].HeaderText = "Actual Cost/KHATA";
                        this.gvCost.Columns[7].HeaderText = "Budgeted Cost/KHATA";
                        this.gvCost.Columns[8].HeaderText = "Actual Cost/KHATA";
                    }

                    this.gvCost.DataBind();
                    this.FooterCal((DataTable)ViewState["tblcost"]);
                    break;
                case "RemainingCost":
                    //this.gvRCost.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvRCost.DataSource = (DataTable)ViewState["tblcost"];
                    this.gvRCost.DataBind();
                    //this.FooterCal((DataTable)ViewState["tblcost"]);
                    break;

                case "ProTarVsAchievement":
                    //Chart
                    this.showChart();
                    this.gvtarvsachivement.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvtarvsachivement.DataSource = (DataTable)ViewState["tblcost"];
                    this.gvtarvsachivement.DataBind();
                    this.FooterCal((DataTable)ViewState["tblcost"]);

                    break;
            }


        }

        private void FooterCal(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;


            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Const":
                case "Sales":

                    ((Label)this.gvCost.FooterRow.FindControl("lgvFamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trnam)", "")) ?
                0.00 : dt.Compute("sum(trnam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCost.FooterRow.FindControl("lgvFbamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdam)", "")) ?
                       0.00 : dt.Compute("sum(bgdam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCost.FooterRow.FindControl("lgvFbcncst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bcncst)", "")) ?
                      0.00 : dt.Compute("sum(bcncst)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCost.FooterRow.FindControl("lgvFcncst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cncst)", "")) ?
                        0.00 : dt.Compute("sum(cncst)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvCost.FooterRow.FindControl("lgvFbslcst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bslcst)", "")) ?
                       0.00 : dt.Compute("sum(bslcst)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCost.FooterRow.FindControl("lgvFcnslcst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cnslcst)", "")) ?
                        0.00 : dt.Compute("sum(cnslcst)", ""))).ToString("#,##0;(#,##0); ");

                    break;
                case "RemainingCost":
                    double bdgprqty = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bdgprqty)", "")) ? 0.00 : dt.Compute("sum(bdgprqty)", "")));
                    double bgdbalqty = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdbalqty)", "")) ? 0.00 : dt.Compute("sum(bgdbalqty)", "")));
                    double tbgdreq = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tbgdreq)", "")) ? 0.00 : dt.Compute("sum(tbgdreq)", "")));
                    ((Label)this.gvRCost.FooterRow.FindControl("lgvFbgdPuramt")).Text = bdgprqty.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvRCost.FooterRow.FindControl("lgvFbgdBalamt")).Text = bgdbalqty.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvRCost.FooterRow.FindControl("lgvtBgdamt")).Text = tbgdreq.ToString("#,##0;(#,##0); ");
                    ////((Label)this.gvRCost.FooterRow.FindControl("lgvFDifamt")).Text = (bgdamt - Reamt).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvRCost.FooterRow.FindControl("lgvFDifamt")).Text = (bgdamt - (recivedamt + Balamt)).ToString("#,##0;(#,##0); ");
                    break;

                case "ProTarVsAchievement":
                    double totaramt = Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["comtamt"]);
                    double comacamt = Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["comacamt"]);
                    ((Label)this.gvtarvsachivement.FooterRow.FindControl("lgvFtaramt")).Text = totaramt.ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvtarvsachivement.FooterRow.FindControl("lgvFacamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(acamt)", "")) ? 0.00 : dt.Compute("sum(acamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    this.lblProgressInPer.Text = "Total Progress in " + ((totaramt > 0) ? ((comacamt * 100) / totaramt).ToString("#,##0.00;(#,##0.00); ") + "%" : "0.00%");

                    break;
            }






        }


        private void showChart()
        {




            //if (Chart1.Series["Series1"].Points.Count > 0)
            //{
            //    plotY = Chart1.Series["Series1"].Points[Chart1.Series["Series1"].Points.Count - 1].YValues[0];
            //}


            //if (Chart1.Series["Series1"].Points.Count > 0)
            //{
            //    plotY = Chart1.Series["Series1"].Points[0].YValues[0] = Convert.ToDouble(dt.Rows[0]["comtamt"].ToString());
            //    plotY2 = Chart1.Series["Series2"].Points[0].YValues[0] = Convert.ToDouble(dt.Rows[0]["comacamt"].ToString());
            //}



            //DataTable dt = (DataTable)ViewState["tblcost"];
            //for (int i = 0; i <dt.Rows.Count; i++)
            //{
            //    Chart1.Series["Series1"].Points.AddXY(dt.Rows[i]["monyear"].ToString(), Convert.ToDouble(dt.Rows[i]["comtamt"].ToString()));
            //    Chart1.Series["Series2"].Points.AddXY(dt.Rows[i]["monyear"].ToString(), Convert.ToDouble(dt.Rows[i]["comacamt"].ToString()));

            //}

            //Chart1.Series["Series1"].ChartType = SeriesChartType.Line;
            //Chart1.Series["Series2"].ChartType = SeriesChartType.Line;


            Chart1.Series["Series1"].XValueMember = "monyear";
            Chart1.Series["Series1"].YValueMembers = "comtamt";
            Chart1.Series["Series2"].XValueMember = "monyear";
            Chart1.Series["Series2"].YValueMembers = "comacamt";

            Chart1.DataSource = (DataTable)ViewState["tblcost"];
            Chart1.DataBind();







        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)ViewState["tblcost"];
            string PrjName = this.ddlAccProject.SelectedItem.Text.Substring(13);
            string grpname = this.ddlRptGroup.SelectedItem.Text.Trim();
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            if (this.Request.QueryString["Type"].ToString().Trim() == "Const")
            {
                ReportDocument rrs1 = new RealERPRPT.R_17_Acc.RptPrjCostperSft();
                TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtComName"] as TextObject;
                rptCname.Text = comnam;
                TextObject rpttxtprjname = rrs1.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
                rpttxtprjname.Text = PrjName;
                TextObject txtConArea = rrs1.ReportDefinition.ReportObjects["txtConArea"] as TextObject;
                txtConArea.Text = "Construction Area: " + this.lbltxtCArea.Text.Trim();

                TextObject txtTitle = rrs1.ReportDefinition.ReportObjects["txtgroup"] as TextObject;
                txtTitle.Text = "Group: " + grpname;
                TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                txtFDate1.Text = "As On " + date;
                TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rrs1.SetDataSource(dt1);
                Session["Report1"] = rrs1;
            }
            else if (this.Request.QueryString["Type"].ToString().Trim() == "Sales")
            {
                ReportDocument rrs1 = new RealERPRPT.R_17_Acc.RptPrjSalCostPerSFT();
                TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtComName"] as TextObject;
                rptCname.Text = comnam;
                TextObject rpttxtprjname = rrs1.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
                rpttxtprjname.Text = PrjName;
                TextObject txtConArea = rrs1.ReportDefinition.ReportObjects["txtConArea"] as TextObject;
                txtConArea.Text = "Saleable Area: " + this.lbltxtCArea.Text.Trim();
                TextObject txtTitle = rrs1.ReportDefinition.ReportObjects["txtgroup"] as TextObject;
                txtTitle.Text = "Group: " + grpname;
                TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                txtFDate1.Text = "As On " + date;
                TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rrs1.SetDataSource(dt1);
                Session["Report1"] = rrs1;

            }

            else if (this.Request.QueryString["Type"].ToString().Trim() == "ProTarVsAchievement")
            {
                ReportDocument rrs1 = new ReportDocument();
                if (this.chkGraph.Checked)
                {
                    rrs1 = new RealERPRPT.R_32_Mis.RptProjectGraph();
                    TextObject rpttxtprjname = rrs1.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
                    rpttxtprjname.Text = PrjName;

                    TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                    rrs1.SetDataSource(dt1);
                }
                else
                {
                    rrs1 = new RealERPRPT.R_32_Mis.RptPrjCostPerSFT();

                    TextObject rpttxtprjname = rrs1.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
                    rpttxtprjname.Text = PrjName;

                    TextObject rpttxtPregressInPer = rrs1.ReportDefinition.ReportObjects["txtPregressInPer"] as TextObject;
                    rpttxtPregressInPer.Text = this.lblProgressInPer.Text.Trim();
                    TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                    rrs1.SetDataSource(dt1);
                    string comcod = hst["comcod"].ToString();
                    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                    rrs1.SetParameterValue("ComLogo", ComLogo);
                }


                Session["Report1"] = rrs1;
                this.showChart();
            }

            else
            {
                ReportDocument rrs1 = new RealERPRPT.R_32_Mis.RptPrjRemainingCost();
                TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtComName"] as TextObject;
                rptCname.Text = comnam;
                TextObject rpttxtprjname = rrs1.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
                rpttxtprjname.Text = PrjName;
                TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                txtFDate1.Text = "As On " + date;

                TextObject OrgBgdVal = rrs1.ReportDefinition.ReportObjects["OrgBgdVal"] as TextObject;
                OrgBgdVal.Text = this.lblOrgBgdVal.Text;
                TextObject OrgBgdCon = rrs1.ReportDefinition.ReportObjects["OrgBgdCon"] as TextObject;
                OrgBgdCon.Text = this.lblOrgBgdCon.Text;
                TextObject OrgBgdConSFT = rrs1.ReportDefinition.ReportObjects["OrgBgdConSFT"] as TextObject;
                OrgBgdConSFT.Text = this.lblOrgBgdConSFT.Text;
                TextObject OrgBgdSal = rrs1.ReportDefinition.ReportObjects["OrgBgdSal"] as TextObject;
                OrgBgdSal.Text = this.lblOrgBgdSal.Text;
                TextObject OrgBgdSFT = rrs1.ReportDefinition.ReportObjects["OrgBgdSFT"] as TextObject;
                OrgBgdSFT.Text = this.lblOrgBgdSFT.Text;
                TextObject OrgBgdPr = rrs1.ReportDefinition.ReportObjects["OrgBgdPr"] as TextObject;
                OrgBgdPr.Text = this.lblOrgBgdPr.Text;

                TextObject RevBgdAmVal = rrs1.ReportDefinition.ReportObjects["RevBgdAmVal"] as TextObject;
                RevBgdAmVal.Text = this.lblRevBgdAmVal.Text;
                TextObject RevPriIncVal = rrs1.ReportDefinition.ReportObjects["RevPriIncVal"] as TextObject;
                RevPriIncVal.Text = this.lblRevPriIncVal.Text;
                TextObject RevPriDecVal = rrs1.ReportDefinition.ReportObjects["RevPriDecVal"] as TextObject;
                RevPriDecVal.Text = this.lblRevPriDecVal.Text;
                TextObject RevRemPurVal = rrs1.ReportDefinition.ReportObjects["RevRemPurVal"] as TextObject;
                RevRemPurVal.Text = this.lblRevRemPurVal.Text;

                TextObject RevTotalVal = rrs1.ReportDefinition.ReportObjects["RevTotalVal"] as TextObject;
                RevTotalVal.Text = this.lblRevTotalVal.Text;
                TextObject RevCon = rrs1.ReportDefinition.ReportObjects["RevCon"] as TextObject;
                RevCon.Text = this.lblRevCon.Text;
                TextObject RevConSFT = rrs1.ReportDefinition.ReportObjects["RevConSFT"] as TextObject;
                RevConSFT.Text = this.lblRevConSFT.Text;
                TextObject RevSal = rrs1.ReportDefinition.ReportObjects["RevSal"] as TextObject;
                RevSal.Text = this.lblRevSal.Text;
                TextObject RevSalSFT = rrs1.ReportDefinition.ReportObjects["RevSalSFT"] as TextObject;
                RevSalSFT.Text = this.lblRevSalSFT.Text;
                TextObject RevInPr = rrs1.ReportDefinition.ReportObjects["RevInPr"] as TextObject;
                RevInPr.Text = this.lblRevInPr.Text;

                TextObject IncVal = rrs1.ReportDefinition.ReportObjects["IncVal"] as TextObject;
                IncVal.Text = this.lblIncVal.Text;
                TextObject Inccon = rrs1.ReportDefinition.ReportObjects["Inccon"] as TextObject;
                Inccon.Text = this.lblInccon.Text;
                TextObject IncconSFT = rrs1.ReportDefinition.ReportObjects["IncconSFT"] as TextObject;
                IncconSFT.Text = this.lblIncconSFT.Text;
                TextObject IncSal = rrs1.ReportDefinition.ReportObjects["IncSal"] as TextObject;
                IncSal.Text = this.lblIncSal.Text;
                TextObject IncSalSFT = rrs1.ReportDefinition.ReportObjects["IncSalSFT"] as TextObject;
                IncSalSFT.Text = this.lblIncSalSFT.Text;
                TextObject IncPr = rrs1.ReportDefinition.ReportObjects["IncPr"] as TextObject;
                IncPr.Text = this.lblIncPr.Text;



                TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rrs1.SetDataSource(dt1);
                Session["Report1"] = rrs1;

            }


            if (ConstantInfo.LogStatus == true)
            {

                string comcod = this.GetComeCode();
                string eventtype = "Project Cost Info";
                string eventdesc = "Print Report: " + this.Request.QueryString["Type"].ToString().Trim();
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        //private DataTable HiddenSameData(DataTable dt1)
        //{
        //    if (dt1.Rows.Count == 0)
        //        return dt1;

        //    string grp = dt1.Rows[0]["grp"].ToString();
        //    for (int j = 1; j < dt1.Rows.Count; j++)
        //    {
        //        if (dt1.Rows[j]["grp"].ToString() == grp)
        //        {
        //            grp = dt1.Rows[j]["grp"].ToString();
        //            dt1.Rows[j]["grpdesc"] = "";
        //        }
        //        grp = dt1.Rows[j]["grp"].ToString();


        //    }

        //    return dt1;

        //}
        protected void gvCost_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCost.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtntotal_Click(object sender, EventArgs e)
        {
            this.Savevalue();
            Data_Bind();
        }

        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            this.Savevalue();
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)ViewState["tblcost"];
            string actcode = this.ddlAccProject.SelectedValue.ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string rescode = dt.Rows[i]["rescode"].ToString();
                string rate = dt.Rows[i]["acrat"].ToString();

                if (ASTUtility.Right(rescode, 4) != "AAAA")
                {
                    bool result = prjData.UpdateTransInfo(comcod, "SP_REPORT_PRJ_INFO", "INSORUPDPRJRMNCOST", actcode, rescode, rate,
                              "", "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Failed');", true);
                        return;
                    }
                }
            }
            this.ShowRemainingCost();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Successfully');", true);

        }
        protected void gvRCost_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Savevalue();
            this.gvRCost.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        private void Savevalue()
        {
            DataTable dt = (DataTable)ViewState["tblcost"];
            int i;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "RemainingCost":
                    for (i = 0; i < this.gvRCost.Rows.Count; i++)
                    {
                        string rescode = dt.Rows[i]["rescode"].ToString();
                        if (ASTUtility.Right(rescode, 4) != "AAAA")
                        {
                            double balqty = Convert.ToDouble(dt.Rows[i]["balqty"].ToString());
                            double bdgprIn = Convert.ToDouble(dt.Rows[i]["princ"].ToString());
                            double bdgprDec = Convert.ToDouble(dt.Rows[i]["pridesc"].ToString());
                            double bgdrat = Convert.ToDouble(dt.Rows[i]["bgdrat"].ToString());
                            double acrate = Convert.ToDouble("0" + ((TextBox)this.gvRCost.Rows[i].FindControl("txtgvacrate")).Text.Trim());
                            double incrate = acrate - bgdrat;
                            //int TblRowIndex = (gvRCost.PageIndex) * gvRCost.PageSize + i;
                            dt.Rows[i]["acrat"] = acrate;
                            dt.Rows[i]["incrate"] = incrate;
                            dt.Rows[i]["bgdbalqty"] = balqty * incrate;
                            dt.Rows[i]["tbgdreq"] = (balqty * incrate) + bdgprIn + bdgprDec;

                        }
                    }

                    break;

                case "ProTarVsAchievement":

                    double toamt = 0.00;
                    for (i = 0; i < this.gvtarvsachivement.Rows.Count; i++)
                    {
                        double taramt = Convert.ToDouble("0" + ((TextBox)this.gvtarvsachivement.Rows[i].FindControl("txtgvtaramt")).Text.Trim().Replace("%", ""));
                        dt.Rows[i]["taramt"] = taramt;
                        if (i == ((this.gvtarvsachivement.Rows.Count) - 1))
                            dt.Rows[i]["taramt"] = totaramt - toamt;
                        toamt = toamt + taramt;
                    }
                    break;
            }




            ViewState["tblcost"] = dt;

        }




        protected void lbtnTotalTarVsAchievement_Click(object sender, EventArgs e)
        {
            this.Savevalue();
            this.Data_Bind();
        }

        protected void lbtnUpdateTarVsAchievement_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.Savevalue();
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)ViewState["tblcost"];
            string actcode = this.ddlAccProject.SelectedValue.ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string yearmon = dt.Rows[i]["yearmon"].ToString();
                string taramt = dt.Rows[i]["taramt"].ToString();


                bool result = prjData.UpdateTransInfo(comcod, "SP_REPORT_PRJ_INFO", "INSORUPDATEPCHART", actcode, yearmon, taramt,
                          "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Failed');", true);
                    return;
                }
            }
            ShowProtarvsAchievement();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Successfully');", true);
        }


        protected void gvRCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label actdesc = (Label)e.Row.FindControl("lblgvresdesc");
                Label BdgPurIn = (Label)e.Row.FindControl("lblgvBdgPurIn");
                Label BdgPurDe = (Label)e.Row.FindControl("lblgvBdgPurDe");
                Label newpurratw = (Label)e.Row.FindControl("txtgvbalPur");
                Label tamt = (Label)e.Row.FindControl("txtgvBgdReq");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA" || ASTUtility.Right(code, 4) == "BBBB")
                {

                    actdesc.Font.Bold = true;
                    BdgPurIn.Font.Bold = true;
                    BdgPurDe.Font.Bold = true;
                    newpurratw.Font.Bold = true;
                    tamt.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");


                }

            }
        }
    }
}
