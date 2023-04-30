using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using RealERPLIB;
using RealERPRPT;
using System.Reflection;
using RealERPRDLC;
using EASendMail;
using System.Net.Mail;
using Microsoft.Reporting.WinForms;


namespace RealERPWEB.F_99_Allinterface
{
    public partial class Inventoryprint : System.Web.UI.Page
    {
        ProcessAccess InvData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "Rptaprroval":
                    this.PrintMatTransferGen();
                    break;

                case "ReqFinalAppPrint":

                    break;

                default:
                    break;
            }
        }


        private void PrintMatTransferGen()
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
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string mGPassNo = this.Request.QueryString["genno"].ToString();
            DataSet ds2 = InvData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPURGERPASSINFO", mGPassNo, "",
                        "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            ViewState["tblgetPass"] = ds2.Tables[0];

            DataTable dt1 = (DataTable)ViewState["tblgetPass"];

            DataSet ds1 = InvData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPADDANDOTHER", mGPassNo, "",
                          "", "", "", "", "", "", "");


            string fpactdesc = dt1.Rows[0]["tfpactdesc"].ToString();
            string tpactdesc = dt1.Rows[0]["ttpactdesc"].ToString();
            string mtrref = dt1.Rows[0]["mtrref"].ToString();
            string mtrdat = Convert.ToDateTime(dt1.Rows[0]["mtrdat"]).ToString("dd.MM.yyyy");
            string getpdatdat = Convert.ToDateTime(ds2.Tables[1].Rows[0]["getpdat"]).ToString("dd.MM.yyyy");

            LocalReport Rpt1 = new LocalReport();
            var lst = dt1.DataTableToList<RealEntity.C_12_Inv.PurEqisition.PurGetPass>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptMaterialTrnsGatepass", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtComAddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtfpactdesc", fpactdesc + "\n" + ds1.Tables[0].Rows[0]["tfpactadd"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtPrjAddress", tpactdesc + "\n" + ds1.Tables[0].Rows[0]["ttpactadd"].ToString()));
            Rpt1.SetParameters(new ReportParameter("rpttxtmgatepno", ds2.Tables[1].Rows[0]["refno"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtmtrref", "MTRF No: " + mtrref));

            Rpt1.SetParameters(new ReportParameter("txtDate", getpdatdat));
            Rpt1.SetParameters(new ReportParameter("txtmtrdat", mtrdat));
            Rpt1.SetParameters(new ReportParameter("Getpassno", "Gate Pass No: " + ds2.Tables[1].Rows[0]["getpno1"].ToString()));
            Rpt1.SetParameters(new ReportParameter("narrationname", ""));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Gete Pass"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";









        }
    }
}