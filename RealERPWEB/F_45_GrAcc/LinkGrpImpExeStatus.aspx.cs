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
namespace RealERPWEB.F_45_GrAcc

{
    public partial class LinkGrpImpExeStatus : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Reports";
                this.ViewSection();

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

                case "SymGenTar":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.lblFDate.Text = Convert.ToDateTime(Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy");
                    this.lblTDate.Text = Convert.ToDateTime(Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy");

                    break;




            }

        }


        private string GetCompCode()
        {

            return (this.Request.QueryString["comcod"].ToString());

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


            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {

                case "MPlnVsEx":
                    this.maplanvaplanes();
                    break;

                case "SymGenTar":


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
            }



        }

        private void FooterCalcul(DataTable dt)
        {
            string Type = Request.QueryString["Type"].ToString();
            if (dt.Rows.Count == 0)
                return;


            switch (Type)
            {

                case "MPlnVsEx":

                    double MaAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mpamt)", "")) ? 0 : dt.Compute("sum(mpamt)", "")));
                    double ExeAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(examt)", "")) ? 0 : dt.Compute("sum(examt)", "")));

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
        protected void gvTvsImp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvTvsImp.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
    }
}

