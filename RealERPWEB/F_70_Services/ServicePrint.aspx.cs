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
using RealEntity;
using RealERPLIB;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;

namespace RealERPWEB.F_70_Services
{
    public partial class ServicePrint : System.Web.UI.Page
    {
        ProcessAccess serData = new ProcessAccess();
        Common Common = new Common();
        public static double TAmount;
        protected void Page_Load(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "PrintQuote":
                    PrintQuotations();
                    break;
            }
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void PrintQuotations() 
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string combranch = hst["combranch"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;


                //DataSet _ReportDataSet = serData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");


                DataSet _ReportDataSet = serData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "PRINTVOUCHER01", "", "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt = _ReportDataSet.Tables[0];
                DataTable dt1 = _ReportDataSet.Tables[1];

                string postrmid = dt1.Rows[0]["entryid"].ToString();
                string postuser = dt1.Rows[0]["entryPerson"].ToString();
                string Posteddat = Convert.ToDateTime(dt1.Rows[0]["entryDate"]).ToString("dd-MMM-yyyy");
                string postdesig = dt1.Rows[0]["entrydesig"].ToString();
                string txtsign1 = postuser + "\n" + postdesig + "\n" + Posteddat;

                LocalReport Rpt1 = new LocalReport();



                var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();

                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucher3", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";




                //if (ConstantInfo.LogStatus)
                //{
                //    string eventdesc = "Print Post Dated Voucher";
                //    string eventdesc2 = "Voucher: " + vounum + " Dated: " + voudat;
                //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "", eventdesc, eventdesc2);

                //}

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

    }
}
