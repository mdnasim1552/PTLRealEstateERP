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
    public partial class LinkConstruDashDetails : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00, bgdpercent = 0.00, bgdexepercent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((Label)this.Master.FindControl("lblTitle")).Text = "Construction Details";
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


            this.ShowConProgress();
        }



        private void ShowConProgress()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string gencode = this.Request.QueryString["Gencode"].ToString() + "%";
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_DASH_BOARD_INFO_LP", "CONSTRUCTION_DASHBOARD_Detials", pactcode, date, gencode, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvConPro.DataSource = null;
                this.gvConPro.DataBind();
                return;
            }

            Session["tblConPro"] = HiddenSameData(ds1.Tables[0]);
            this.LoadGrid();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Floor Wise Construction Progress";
                string eventdesc = "Show Report";
                string eventdesc2 = this.Request.QueryString["pactdesc"].ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";

                }

                else
                    pactcode = dt1.Rows[j]["rsircode"].ToString();
            }

            return dt1;
        }

        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblConPro"];
            this.gvConPro.DataSource = dt;
            this.gvConPro.DataBind();
            this.FooterCalcul();




            // this.BindChartYear(dt);

        }


        private void FooterCalcul()
        {
            DataTable dt = (DataTable)Session["tblConPro"];

            if (dt.Rows.Count == 0)

                return;


            ((Label)this.gvConPro.FooterRow.FindControl("lgvFWorkPer")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(parcent)", "")) ? 0.00
                : dt.Compute("Sum(parcent)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvConPro.FooterRow.FindControl("lgvFWorkRest")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(restwrk)", "")) ? 0.00
                : dt.Compute("Sum(restwrk)", ""))).ToString("#,##0.00;(#,##0.00); ");




        }




        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            //  this.PrintBgsdVsExe();

        }

        //private void PrintBgsdVsExe() 
        //{

        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    DataTable dt = (DataTable)Session["tblConPro"];
        //    double bgdamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ? 0 : dt.Compute("sum(bgdamt)", "")));
        //    double mplan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mplanat)", "")) ? 0 : dt.Compute("sum(mplanat)", "")));
        //    double examt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(eamt)", "")) ? 0 : dt.Compute("sum(eamt)", "")));

        //    percent = (mplan == 0 ? 0.00 : ((examt * 100) / mplan));
        //    bgdpercent = (bgdamt == 0 ? 0.00 : ((mplan * 100) / bgdamt));
        //    bgdexepercent = (bgdamt == 0 ? 0.00 : ((examt * 100) / bgdamt));

        //    string projectName = this.lblvalproject.Text;
        //    ReportDocument rptConPro = new RealERPRPT.R_32_Mis.RptConProgram();
        //    TextObject rpttxtPrjName = rptConPro.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
        //    rpttxtPrjName.Text = projectName;
        //    TextObject rpttxtDate = rptConPro.ReportDefinition.ReportObjects["txtDate"] as TextObject;
        //    rpttxtDate.Text ="As On "+this.lblvalDate.Text.Trim();
        //    TextObject rpttxtpercent = rptConPro.ReportDefinition.ReportObjects["txtpercent"] as TextObject;
        //    rpttxtpercent.Text = percent.ToString("#,##0.00;(#,##0.00); ") + " %";
        //    TextObject rpttxBtpercent = rptConPro.ReportDefinition.ReportObjects["txtbpercent"] as TextObject;
        //    rpttxBtpercent.Text = bgdpercent.ToString("#,##0.00;(#,##0.00); ") + " %";

        //    TextObject txtbexepercent = rptConPro.ReportDefinition.ReportObjects["txtbexepercent"] as TextObject;
        //    txtbexepercent.Text = bgdexepercent.ToString("#,##0.00;(#,##0.00); ") + " %";
        //    TextObject txtuserinfo = rptConPro.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
        //    rptConPro.SetDataSource(dt);

        //    if (ConstantInfo.LogStatus == true)
        //    {
        //        string eventtype = "Floor Wise Construction Progress";
        //        string eventdesc = "Print Report";
        //        string eventdesc2 = this.lblvalproject.Text;
        //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //    }
        //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    rptConPro.SetParameterValue("ComLogo", ComLogo);

        //    Session["Report1"] = rptConPro;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        //}

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

        }

    }
}

