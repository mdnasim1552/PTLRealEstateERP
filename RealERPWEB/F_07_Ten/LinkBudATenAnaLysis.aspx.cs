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
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_07_Ten
{
    public partial class LinkBudATenAnaLysis : System.Web.UI.Page
    {
        ProcessAccess tasData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.lblHeadtitle.Text = (Request.QueryString["Type"].ToString() == "SchVsAna") ? "PROJECT SCHEDULE VS ANALYSIS" : "TENDER PROPOSAL";
                this.lblProject.Text = this.Request.QueryString["actdesc"].ToString();
                this.lblItem.Text = this.Request.QueryString["isirdesc"].ToString();
                this.lblflrdes.Text = this.Request.QueryString["flrdes"].ToString();

            }
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }





        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "TenAnalysis":
                    this.PrintTenAnalysis();
                    break;

                case "BudAnalysis":
                    this.PrintBudgetAnalysis();
                    break;


            }


        }




        protected void PrintTenAnalysis()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string mItmcod = this.Request.QueryString["isircode"].ToString();
            string mFlrCod = this.Request.QueryString["flrcod"].ToString();
            string mBldCode = this.Request.QueryString["actcode"].ToString();

            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS", "RPTACTANASHEET",
                          mBldCode, mFlrCod, mItmcod, "", "", "", "", "", "");

            ReportDocument rptAnaSheet = new RealERPRPT.R_07_Ten.rptTASAnaSheet();
            rptAnaSheet.SetDataSource(ds1.Tables[0]);

            TextObject TxtRptTitle1 = rptAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle1"] as TextObject;
            TxtRptTitle1.Text = hst["comnam"].ToString();

            TextObject TxtRptTitle2 = rptAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle2"] as TextObject;
            TxtRptTitle2.Text = "Project: " + this.Request.QueryString["actdesc"].ToString();

            TextObject TxtRptTitle3 = rptAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle3"] as TextObject;
            TxtRptTitle3.Text = "Sch.Item No: " + ds1.Tables[1].Rows[0]["SchItmNo1"].ToString(); // Sch. Item No

            TextObject TxtRptTitle4 = rptAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle4"] as TextObject;
            TxtRptTitle4.Text = ds1.Tables[1].Rows[0]["Itmdesc"].ToString();

            string mUnitFPS = ds1.Tables[1].Rows[0]["UnitFPS"].ToString();
            string mUnitMKS = ds1.Tables[1].Rows[0]["UnitMKS"].ToString();
            double mStdQtyF = Convert.ToDouble(ds1.Tables[1].Rows[0]["StdQtyF"]);
            double mStdQtyM = Convert.ToDouble(ds1.Tables[1].Rows[0]["StdQtyM"]);

            TextObject TxtRptTitle5 = rptAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle5"] as TextObject;
            TxtRptTitle5.Text = "Quantity Considered: " + mStdQtyF.ToString("#,##0.00") + " " + mUnitFPS +
                (mUnitFPS != mUnitMKS ? " = " + mStdQtyM.ToString("#,##0.00") + " " + mUnitMKS : "");

            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptAnaSheet.SetParameterValue("ComLogo", ComLogo);

            Session["Report1"] = rptAnaSheet;
            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        protected void PrintBudgetAnalysis()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string mItmcod = this.Request.QueryString["isircode"].ToString();
            string mFlrCod = this.Request.QueryString["flrcod"].ToString();
            string mBldCode = this.Request.QueryString["actcode"].ToString();
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RPTACTANASHEET",
                          mBldCode, mFlrCod, mItmcod, "", "", "", "", "", "");

            LocalReport Rpt1 = new LocalReport();
            //  DataTable dt1 = (DataTable)Session["tblbgd"];
            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_04_Bgd.EClassBudget.BugdAna>();
            string mUnitFPS = ds1.Tables[1].Rows[0]["UnitFPS"].ToString();
            string mUnitMKS = ds1.Tables[1].Rows[0]["UnitMKS"].ToString();
            double mStdQtyF = Convert.ToDouble(ds1.Tables[1].Rows[0]["StdQtyF"]);
            double mStdQtyM = Convert.ToDouble(ds1.Tables[1].Rows[0]["StdQtyM"]);
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptStdAnaSheet", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("date", "From " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", "Item Name: " + ds1.Tables[1].Rows[0]["Itmdesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("Quantity", "Quantity Considered: " + mStdQtyF.ToString("#,##0.00") + " " + mUnitFPS +
                (mUnitFPS != mUnitMKS ? " = " + mStdQtyM.ToString("#,##0.00") + " " + mUnitMKS : "")));
            //Rpt1.SetParameters(new ReportParameter("Floor", this.ddlFloorListRpt.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Standard Analysis"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptStdAnaSheet = new RealERPRPT.R_04_Bgd.rptStdAnaSheet();
            //TextObject TxtRptTitle2 = rptStdAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle2"] as TextObject;
            //TxtRptTitle2.Text = this.Request.QueryString["actdesc"].ToString();

            ////TextObject TxtRptTitle3 = rptStdAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle3"] as TextObject;
            ////TxtRptTitle3.Text = "Item Name:"; // "Sch.Item No: " + ds1.Tables[1].Rows[0]["SchItmNo1"].ToString(); // Sch. Item No

            //TextObject TxtRptTitle4 = rptStdAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle4"] as TextObject;
            //TxtRptTitle4.Text = "Item Name: " + ds1.Tables[1].Rows[0]["Itmdesc"].ToString();



            //TextObject TxtRptTitle5 = rptStdAnaSheet.ReportDefinition.ReportObjects["TxtRptTitle5"] as TextObject;
            //TxtRptTitle5.Text = "Quantity Considered: " + mStdQtyF.ToString("#,##0.00") + " " + mUnitFPS +
            //    (mUnitFPS != mUnitMKS ? " = " + mStdQtyM.ToString("#,##0.000") + " " + mUnitMKS : "");
            //TextObject txtuserinfo = rptStdAnaSheet.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptStdAnaSheet.SetDataSource(ds1.Tables[0]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptStdAnaSheet.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptStdAnaSheet;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

    }
}