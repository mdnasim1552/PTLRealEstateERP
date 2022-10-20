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
                case "PrintInvoice":
                    PrintInvoice();
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
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

                string QId = this.Request.QueryString["QId"].ToString();


                DataSet _ReportDataSet = serData.GetTransInfo(comcod, "[dbo_Services].[SP_REPORT_QUOTATION]", "GETQUOTATIONS", QId, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;

                DataTable dt1 = _ReportDataSet.Tables[0];
                string ToCutomerName = dt1.Rows[0]["custdesc"].ToString();
                string Mobile = dt1.Rows[0]["phone"].ToString();
                string Servicefor = dt1.Rows[0]["workdesc"].ToString();
                string aptfor = dt1.Rows[0]["aptdesc"].ToString();
                string addressfor = dt1.Rows[0]["paddress"].ToString();
                string quotnofor = dt1.Rows[0]["quotid"].ToString();
                string qdate = Convert.ToDateTime(dt1.Rows[0]["quotdate"]).ToString("dd-MMM-yyyy");
                string remarks = dt1.Rows[0]["remarks"].ToString();
                //double apramt = Convert.ToDouble (dt1.Rows[0]["apramt"]);
                double apramt = (dt1.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(apramt)", "")) ? 0.00 : dt1.Compute("Sum(apramt)", "")));

                string inword = "In Word: " + ASTUtility.Trans(apramt, 2);

                LocalReport Rpt1 = new LocalReport();
                var list = dt1.DataTableToList<RealERPEntity.C_70_Services.EClass_Quotation.EQuotationinfo>();

                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_70_Services.RptQuotationPrint", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("printdate", printdate));
                Rpt1.SetParameters(new ReportParameter("ToCutomerName", ToCutomerName));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "QUOTATION"));
                Rpt1.SetParameters(new ReportParameter("Rptgrat", "Thank You For Your Business"));
                Rpt1.SetParameters(new ReportParameter("Mobile", Mobile));
                Rpt1.SetParameters(new ReportParameter("inword", inword));
                Rpt1.SetParameters(new ReportParameter("aptfor", aptfor));
                Rpt1.SetParameters(new ReportParameter("Servicefor", Servicefor));
                Rpt1.SetParameters(new ReportParameter("addressfor", addressfor));
                Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("quotnofor", quotnofor));
                Rpt1.SetParameters(new ReportParameter("qdate", qdate));
                Rpt1.SetParameters(new ReportParameter("remarks", remarks));
                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }
        private void PrintInvoice()
        {
            try
            {
                //RptInvoicePrint
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string combranch = hst["combranch"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

                string QId = this.Request.QueryString["QId"].ToString();


                DataSet _ReportDataSet = serData.GetTransInfo(comcod, "[dbo_Services].[SP_REPORT_QUOTATION]", "GETQUOTATIONS", QId, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;

                DataTable dt1 = _ReportDataSet.Tables[0];
                string ToCutomerName = dt1.Rows[0]["custdesc"].ToString();
                string Mobile = dt1.Rows[0]["phone"].ToString();
                string Servicefor = dt1.Rows[0]["workdesc"].ToString();
                string aptfor = dt1.Rows[0]["aptdesc"].ToString();
                string addressfor = dt1.Rows[0]["paddress"].ToString();
                string quotnofor = dt1.Rows[0]["quotid"].ToString();
                string qdate = Convert.ToDateTime(dt1.Rows[0]["quotdate"]).ToString("dd-MMM-yyyy");
                string remarks = dt1.Rows[0]["remarks"].ToString();
                //double apramt = Convert.ToDouble (dt1.Rows[0]["apramt"]);
                double apramt = (dt1.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(apramt)", "")) ? 0.00 : dt1.Compute("Sum(apramt)", "")));

                string inword = "In Word: " + ASTUtility.Trans(apramt, 2);

                LocalReport Rpt1 = new LocalReport();
                var list = dt1.DataTableToList<RealERPEntity.C_70_Services.EClass_Quotation.EQuotationinfo>();

                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_70_Services.RptInvoicePrint", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("printdate", printdate));
                Rpt1.SetParameters(new ReportParameter("ToCutomerName", ToCutomerName));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "INVOICE"));
                Rpt1.SetParameters(new ReportParameter("Rptgrat", "Thank You For Your Business"));
                Rpt1.SetParameters(new ReportParameter("Mobile", Mobile));
                Rpt1.SetParameters(new ReportParameter("inword", inword));
                Rpt1.SetParameters(new ReportParameter("aptfor", aptfor));
                Rpt1.SetParameters(new ReportParameter("Servicefor", Servicefor));
                Rpt1.SetParameters(new ReportParameter("addressfor", addressfor));
                Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("quotnofor", quotnofor));
                Rpt1.SetParameters(new ReportParameter("qdate", qdate));
                Rpt1.SetParameters(new ReportParameter("remarks", remarks));
                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

    }
}
