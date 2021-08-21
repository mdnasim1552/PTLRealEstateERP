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
    public partial class LinkGrpSysGenTarget : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                string Type = Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = Type == "ImpPlan" ? "MONTHLY IMPLEMENTATION PLAN " : Type == "Execution" ? "WORK EXECUTION "
                    : Type == "PlanVSEx" ? "MONTHLY PLAN VS EXECUTION" : Type == "BgdVSEx" ? "BUDGET VS EXECUTION"
                    : Type == "MaPlanVsPlanVsEx" ? "MASTER PLAN, MONTHLY PLAN & EXECUTION"

                    : Type == "DayWiseExecution" ? "DAY WISE EXECUTION"
                    : Type == "ImpPlan02" ? "System Generated Work Plan Vs. Real Plan" : "MATERIALS EVALUTION";







                this.ViewSection();


                this.GetProjectName();


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


                case "SymGenTar":
                    this.lblFDate.Text = Convert.ToDateTime(Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy");
                    this.lblTDate.Text = Convert.ToDateTime(Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

            }

        }


        private string GetCompCode()
        {
            return (this.Request.QueryString["comcod"].ToString());

        }


        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);


        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowFloorcode();
        }
        private void ShowFloorcode()
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
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

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.ShowValue();

        }

        private void ShowValue()
        {


            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {

                case "SymGenTar":

                    this.ShowTarVsImp();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string comcod = this.GetCompCode();
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Show Report  " + Type;
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }



        private void ShowTarVsImp()
        {


            Session.Remove("tblplanexe");
            string comcod = this.GetCompCode();
            string pactcode = ddlProjectName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy");
            string flrcod = this.ddlFloorListRpt.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", "RPTTARVSIMP", pactcode, frmdate, todate, flrcod, mRptGroup, "", "", "", "");
            if (ds1 == null)
            {
                this.gvTvsImp.DataSource = null;
                this.gvTvsImp.DataBind();
                return;
            }

            Session["tblplanexe"] = this.HiddenSameData(ds1.Tables[0]);
            this.LoadGrid();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {





                case "SymGenTar":
                    string rptcod = dt1.Rows[0]["rptcod"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["rptcod"].ToString() == rptcod)
                        {
                            dt1.Rows[j]["rptdesc1"] = "";
                            dt1.Rows[j]["rptunit"] = "";
                        }
                        rptcod = dt1.Rows[j]["rptcod"].ToString();
                    }

                    break;


            }


            return dt1;

        }



        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblplanexe"];
            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {


                case "SymGenTar":
                    this.gvTvsImp.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvTvsImp.DataSource = dt;
                    this.gvTvsImp.DataBind();
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




                case "SymGenTar":
                    ((Label)this.gvTvsImp.FooterRow.FindControl("lgvFtramttvp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tramt)", "")) ?
                                   0 : dt.Compute("sum(tramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTvsImp.FooterRow.FindControl("lgvFimpAmttvp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(impamt)", "")) ?
                                    0 : dt.Compute("sum(impamt)", ""))).ToString("#,##0;(#,##0); ");

                    break;





            }

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {


                case "SymGenTar":

                    PrintTarVsPlan();
                    break;





            }

        }


        private void PrintTarVsPlan()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Type = Request.QueryString["Type"].ToString();
            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);

            string frmdate = Convert.ToDateTime(Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy");

            ReportDocument rptImp = new RealERPRPT.R_09_PImp.RptTarVsPlan();

            TextObject txtCompanyName = rptImp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            txtCompanyName.Text = comnam;
            TextObject rpttxtPrjName = rptImp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            rpttxtPrjName.Text = projectName;

            TextObject txtdat = rptImp.ReportDefinition.ReportObjects["rpttxtdat"] as TextObject;
            txtdat.Text = " (" + "From  " + frmdate + " To " + todate + ")";
            DataTable dt = (DataTable)Session["tblplanexe"];
            TextObject txtuserinfo = rptImp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptImp.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptImp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptImp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }





        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        protected void gvTvsImp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvTvsImp.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
    }
}

