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
namespace RealERPWEB.F_32_Mis
{
    public partial class RptLinkImpExeStatus : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00, totaramt = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((Label)this.Master.FindControl("lblTitle")).Text = "PROJECT SCHDUEL VS. ANALYSIS";

                //string Type = Request.QueryString["Type"].ToString();
                //this.lblHeadtitle.Text = (Type == "ImpPlan" ? "MONTHLY IMPLEMENTATION PLAN " : (Type == "Execution" ? "WORK EXECUTION " 
                //    :(Type == "PlanVSEx" ? "MONTHLY PLAN VS EXECUTION": (Type == "BgdVSEx" ? "BUDGET VS EXECUTION"                
                //    :(Type == "MaPlanVsPlanVsEx" ? "MASTER PLAN, MONTHLY PLAN & EXECUTION"

                //    :(Type == "DayWiseExecution" ?"DAY WISE EXECUTION":"MATERIALS EVALUTION"))))));


                this.ViewSection();


                //this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");            
                //this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void ViewSection()
        {
            string Type = Request.QueryString["Type"].ToString();

            switch (Type)
            {

                case "MPlnVsEx":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.lblFDate.Text = Convert.ToDateTime(Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy");
                    this.lblTDate.Text = Convert.ToDateTime(Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy");
                    this.ShowFloorcode();
                    this.maplanvaplanes();
                    break;
                case "BgdAll":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.lblFDate.Text = Convert.ToDateTime(Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy");
                    this.lbldatefrm.Text = "As on Date: ";
                    this.lbldateto.Visible = false;
                    this.lblTDate.Visible = false;
                    this.lblflrlist.Visible = false;
                    this.ddlFloorListRpt.Visible = false;
                    this.lblRptGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.lblflrlistAll.Visible = true;
                    this.lblFlDesc.Visible = true;
                    this.AllBgdWork();
                    this.lbtnOk.Visible = false;
                    break;
                case "ProTarVsAchievement":
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.MultiView1.ActiveViewIndex = 2;
                    this.ShowProtarvsAchievement();
                    this.lbldateto.Visible = false;
                    this.lblTDate.Visible = false;
                    this.lblRptGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.lbtnOk.Visible = false;
                    this.lbldatefrm.Visible = true;
                    this.lblflrlist.Visible = false;
                    this.lblFDate.Text = Convert.ToDateTime(Request.QueryString["date"].ToString()).ToString("dd-MMM-yyyy");

                    this.ddlFloorListRpt.Visible = false;
                    break;


            }

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        private void ShowFloorcode()
        {

            string comcod = this.GetCompCode();
            string pactcode = Request.QueryString["pactcode"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", "GETFLOORCOD", pactcode, "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];

            DataRow dr2 = dt.NewRow();
            dr2["flrcod"] = "000";
            dr2["flrdes"] = "All Floors-Sum";
            DataRow dr3 = dt.NewRow();
            dr3["flrcod"] = "AAA";
            dr3["flrdes"] = "All Floors-Details";
            dt.Rows.Add(dr2);
            dt.Rows.Add(dr3);
            DataView dv = dt.DefaultView;
            dv.Sort = "flrcod";
            dt = dv.ToTable();
            this.ddlFloorListRpt.DataTextField = "flrdes";
            this.ddlFloorListRpt.DataValueField = "flrcod";
            this.ddlFloorListRpt.DataSource = dt;
            this.ddlFloorListRpt.DataBind();
            this.ddlFloorListRpt.SelectedValue = "AAA";
        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            //this.lblPage.Visible = true;
            //this.ddlpagesize.Visible = true;
            this.ShowValue();

        }

        private void ShowValue()
        {


            this.lbljavascript.Text = "";
            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {

                case "MPlnVsEx":
                    this.maplanvaplanes();
                    break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string comcod = this.GetCompCode();
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Show Report  " + Type;
                string eventdesc2 = "Project Name: " + Request.QueryString["pactcode"].ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void ShowProtarvsAchievement()
        {
            this.pnlTarVsAchievement.Visible = true;
            Session.Remove("tblplanexe");
            string comcod = Request.QueryString["comcod"].ToString();
            string actcode = Request.QueryString["pactcode"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_PRJ_INFO", "RPTTARVSACHIEVEMENT", actcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvtarvsachivement.DataSource = null;
                this.gvtarvsachivement.DataBind();
                return;

            }
            this.lblPrjName.Text = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            this.lblStartDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["prjstrtdat"]).ToString("dd-MMM-yyyy");
            this.lblEndDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["prjenddat"]).ToString("dd-MMM-yyyy");
            this.lblDuration.Text = Convert.ToInt32(ds1.Tables[1].Rows[0]["duration"]).ToString("#,#0;(#,#0); ");
            totaramt = Convert.ToDouble(ds1.Tables[0].Rows[(ds1.Tables[0].Rows.Count) - 1]["comtamt"]);
            this.lblExe.Text = "Less Execution upto- " + Request.QueryString["date"].ToString();
            this.lblExAmt.Text = "Tk. " + Request.QueryString["lexamt"].ToString();
            Session["tblplanexe"] = ds1.Tables[0];
            this.LoadGrid();



        }
        private void maplanvaplanes()
        {

            string comcod = this.GetCompCode();
            string pactcode = Request.QueryString["pactcode"].ToString();
            string fdate = Convert.ToDateTime(Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy");
            string floorcode = this.ddlFloorListRpt.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            string CallType = (ASTUtility.Left(pactcode, 2) == "41") ? "RPTMAPLNVSMPLNVSEXE" : "RPTFINMAPLNVSMPLNVSEXE";

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", CallType, pactcode, fdate, todate, floorcode, mRptGroup, "", "", "", "");
            if (ds1 == null)
            {
                this.gvmplanvaexe.DataSource = null;
                this.gvmplanvaexe.DataBind();
                return;
            }
            this.lblPrjName.Text = ds1.Tables[1].Rows[0]["actdesc"].ToString();
            Session["tblplanexe"] = ds1.Tables[0];
            this.LoadGrid();


        }
        private void AllBgdWork()
        {

            string comcod = this.Request.QueryString["comcod"].ToString();
            string pactcode = Request.QueryString["pactcode"].ToString();
            string fdate = Convert.ToDateTime(Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy");
            string floorcode = Request.QueryString["FlrCode"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", "RPTBGDPLANEXE", pactcode, "", fdate, floorcode, "12", "", "", "", "");
            if (ds1 == null)
            {
                this.gvmplanvaexe.DataSource = null;
                this.gvmplanvaexe.DataBind();
                return;
            }
            this.lblPrjName.Text = ds1.Tables[1].Rows[0]["actdesc"].ToString();
            this.lblFlDesc.Text = (ds1.Tables[0].Rows.Count > 0) ? ds1.Tables[0].Rows[0]["flrdes"].ToString() : "";
            Session["tblplanexe"] = ds1.Tables[0];
            this.LoadGrid();


        }


        //private DataTable HiddenSameData(DataTable dt1)
        //{
        //    if (dt1.Rows.Count == 0)
        //        return dt1;
        //    string isuno = dt1.Rows[0]["isuno"].ToString();
        //    for (int j = 1; j < dt1.Rows.Count; j++)
        //    {
        //        if (dt1.Rows[j]["isuno"].ToString() == isuno)
        //        {
        //            dt1.Rows[j]["isuno1"] = "";
        //            dt1.Rows[j]["isudat"] = "";

        //        }
        //            isuno = dt1.Rows[j]["isuno"].ToString();
        //    }
        //    return dt1;

        //}



        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblplanexe"];
            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {

                case "MPlnVsEx":
                    this.gvmplanvaexe.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvmplanvaexe.DataSource = dt;
                    this.gvmplanvaexe.DataBind();
                    this.FooterCalcul(dt);
                    break;
                case "BgdAll":
                    this.gvAllWork.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvAllWork.DataSource = dt;
                    this.gvAllWork.DataBind();
                    this.FooterCalcul(dt);
                    break;
                case "ProTarVsAchievement":
                    //Chart
                    this.showChart();
                    this.gvtarvsachivement.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvtarvsachivement.DataSource = dt;
                    this.gvtarvsachivement.DataBind();
                    this.FooterCalcul(dt);

                    break;
            }



        }
        private void showChart()
        {


            Chart1.Series["Series1"].XValueMember = "monyear";
            Chart1.Series["Series1"].YValueMembers = "comtamt";
            Chart1.Series["Series2"].XValueMember = "monyear";
            Chart1.Series["Series2"].YValueMembers = "comacamt";
            Chart1.Series["Series1"].LegendText = "Target";
            Chart1.Series["Series2"].LegendText = "Achievement";

            Chart1.DataSource = (DataTable)Session["tblplanexe"];
            Chart1.DataBind();













        }
        private void FooterCalcul(DataTable dt)
        {
            string Type = Request.QueryString["Type"].ToString();
            if (dt.Rows.Count == 0)
                return;


            switch (Type)
            {

                case "MPlnVsEx":
                    //double mBgdAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rptamt)", "")) ? 0 : dt.Compute("sum(rptamt)", "")));
                    double MaAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mpamt)", "")) ? 0 : dt.Compute("sum(mpamt)", "")));
                    double ExeAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(examt)", "")) ? 0 : dt.Compute("sum(examt)", "")));

                    //((Label)this.gvmplanvaexe.FooterRow.FindControl("lgvFrptamt")).Text = mBgdAmt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvmplanvaexe.FooterRow.FindControl("lgvFmpamt")).Text = MaAmt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvmplanvaexe.FooterRow.FindControl("lgvFmonthamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mamt)", "")) ?
                                   0 : dt.Compute("sum(mamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvmplanvaexe.FooterRow.FindControl("lgvFexeamt")).Text = ExeAmt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvmplanvaexe.FooterRow.FindControl("lgvFexepercentage")).Text = MaAmt > 0 ? ((ExeAmt * 100) / MaAmt).ToString("#,##0.00;(#,##0.00); ") + "%" : "";
                    break;
                case "BgdAll":
                    double BgdAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rptamt)", "")) ? 0 : dt.Compute("sum(rptamt)", "")));
                    double MaAmt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mpamt)", "")) ? 0 : dt.Compute("sum(mpamt)", "")));
                    double ExeAmt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(examt)", "")) ? 0 : dt.Compute("sum(examt)", "")));

                    ((Label)this.gvAllWork.FooterRow.FindControl("lgvFmpamt")).Text = MaAmt1.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvAllWork.FooterRow.FindControl("lgvFBgdamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rptamt)", "")) ?
                                   0 : dt.Compute("sum(rptamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvAllWork.FooterRow.FindControl("lgvFmonthamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mamt)", "")) ?
                                   0 : dt.Compute("sum(mamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvAllWork.FooterRow.FindControl("lgvFexeamt")).Text = ExeAmt1.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvAllWork.FooterRow.FindControl("lgvFexepercentage")).Text = BgdAmt > 0 ? ((ExeAmt1 * 100) / BgdAmt).ToString("#,##0.00;(#,##0.00); ") + "%" : "";

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

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {

                case "MPlnVsEx":
                    this.PrintMaPlanVsPlanVsEx();
                    break;
                case "BgdAll":
                    this.PrintBgdAllWork();
                    break;

                case "ProTarVsAchievement":
                    this.PrintProTarVsAchiev();
                    break;
            }

        }


        private void PrintMaPlanVsPlanVsEx()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblplanexe"];
            //string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            ReportDocument rptMat = new RealERPRPT.R_09_PImp.RptMaPlanVsPlanVsEx();
            double MaAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mpamt)", "")) ? 0 : dt.Compute("sum(mpamt)", "")));
            double ExeAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(examt)", "")) ? 0 : dt.Compute("sum(examt)", "")));
            TextObject txtProjectname = rptMat.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            txtProjectname.Text = this.lblPrjName.Text;
            TextObject txtdat = rptMat.ReportDefinition.ReportObjects["rpttxtdat"] as TextObject;
            txtdat.Text = "(From: " + Convert.ToDateTime(Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy") + "  To : " + Convert.ToDateTime(Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy") + ")";
            TextObject rpttxtExePer = rptMat.ReportDefinition.ReportObjects["txtExePer"] as TextObject;
            rpttxtExePer.Text = MaAmt > 0 ? ((ExeAmt * 100) / MaAmt).ToString("#,##0.00;(#,##0.00); ") + "%" : "";


            TextObject txtuserinfo = rptMat.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptMat.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptMat.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptMat;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintBgdAllWork()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblplanexe"];
            //string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            ReportDocument rptMat = new RealERPRPT.R_32_Mis.RptMaPlanVsPlanVsExAll();
            double BgdAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rptamt)", "")) ? 0 : dt.Compute("sum(rptamt)", "")));
            double MaAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mpamt)", "")) ? 0 : dt.Compute("sum(mpamt)", "")));
            double ExeAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(examt)", "")) ? 0 : dt.Compute("sum(examt)", "")));
            TextObject txtProjectname = rptMat.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            txtProjectname.Text = "Project Name: " + this.lblPrjName.Text;
            TextObject txtFlrDesc = rptMat.ReportDefinition.ReportObjects["txtFlrDesc"] as TextObject;
            txtFlrDesc.Text = "Floor Desc.: " + dt.Rows[0]["flrdes"].ToString();
            TextObject txtdat = rptMat.ReportDefinition.ReportObjects["rpttxtdat"] as TextObject;
            txtdat.Text = "As on Date: " + Convert.ToDateTime(Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy");
            TextObject rpttxtExePer = rptMat.ReportDefinition.ReportObjects["txtExePer"] as TextObject;
            rpttxtExePer.Text = BgdAmt > 0 ? ((ExeAmt * 100) / BgdAmt).ToString("#,##0.00;(#,##0.00); ") + "%" : "";

            //
            TextObject txtuserinfo = rptMat.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptMat.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptMat.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptMat;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void PrintProTarVsAchiev()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblplanexe"];
            string PrjName = this.lblPrjName.Text;
            string grpname = this.ddlRptGroup.SelectedItem.Text.Trim();




            if (this.Request.QueryString["Type"].ToString().Trim() == "ProTarVsAchievement")
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





            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }


        protected void gvmplanvaexe_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvmplanvaexe.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvAllWork_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAllWork.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvtarvsachivement_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkgvtaramt = (HyperLink)e.Row.FindControl("hlnkgvtaramt");
                HyperLink hlnkgvacamt = (HyperLink)e.Row.FindControl("hlnkgvacamt");
                //string pactcode=this.

                // Label tosaleamt = (Label)e.Row.FindControl("lgvtosaleamt");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "yearmon")).ToString();
                // double per = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "perotsale"));
                if (code == "")
                {
                    return;
                }

                else
                {
                    string comcod = this.GetCompCode();

                    string pactcode = Request.QueryString["pactcode"].ToString();
                    string frmdate = Convert.ToDateTime(code.Substring(4, 2) + "-" + "01" + "-" + code.Substring(0, 4)).ToString("dd-MMM-yyyy");
                    string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    hlnkgvacamt.Style.Add("color", "blue");
                    hlnkgvtaramt.Style.Add("color", "blue");
                    hlnkgvacamt.NavigateUrl = "~/F_09_PImp/RptImpExeStatus.aspx?Type=DayWiseExecution&comcod=" + comcod + "&prjcode=" + pactcode + "&Date1=" + frmdate + "&Date2=" + todate;
                    hlnkgvtaramt.NavigateUrl = "~/F_09_PImp/RptImpExeStatus.aspx?Type=MaPlanVsPlanVsEx&comcod=" + comcod + "&prjcode=" + pactcode + "&Date1=" + frmdate + "&Date2=" + todate; ;




                }



            }


        }
    }
}

