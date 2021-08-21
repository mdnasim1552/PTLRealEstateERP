using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
//using System.Drawing;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using Label = System.Web.UI.WebControls.Label;
namespace RealERPWEB.F_22_Sal
{

    public partial class RptSalSummeryDetails : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        public static double OpenBal, Clsbal;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Type = this.Request.QueryString["Type"].ToString();
                this.getGridSummary();

            }

        }

        private void getGridSummary()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string frmdate = this.Request.QueryString["Date1"];
                string todate = this.Request.QueryString["Date2"];
                string depCode = this.Request.QueryString["code"];
                string length = this.Request.QueryString["length"];

                string comcod = GetComeCode();
                DataSet ds1 = MktData.GetTransInfo(comcod, "[SP_REPORT_SALSMGT_SUM]", "RPTDWCOLLECTSTATUSDTAILS", frmdate, todate, length, depCode);

                if (ds1 == null)
                    return;

                if (ds1.Tables[0].Rows.Count == 0)
                {

                    this.gvSalSummeryDetails.DataSource = null;
                    this.gvSalSummeryDetails.DataBind();
                    return;
                }

                Session["tblSalSumDetails"] = HiddenSameData(ds1.Tables[0]);
                this.bind_data();
                this.FooterCalculation();


            }

            catch (Exception ex)
            {


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
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }
            }
            return dt1;

        }

        private void bind_data()
        {
            DataTable dt = (DataTable)Session["tblSalSumDetails"];
            this.gvSalSummeryDetails.DataSource = dt;
            this.gvSalSummeryDetails.DataBind();
            return;
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            comcod = qcomcod.Length > 0 ? qcomcod : comcod;
            return comcod;

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComeCode();
                string comnam = hst["comnam"].ToString();
                string compname = hst["compname"].ToString();
                string comsnam = hst["comsnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string session = hst["session"].ToString();
                string username = hst["username"].ToString();
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

                string fdate = this.Request.QueryString["Date1"] ?? "";
                string todate = this.Request.QueryString["Date2"] ?? "";
                DataTable dt = (DataTable)Session["tblSalSumDetails"];
                var salsummary = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptSalSummeryDetails>();
                LocalReport Rpt1 = new LocalReport();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptSalSummeryDetails", salsummary, null, null);
                //Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comname", comnam));
                Rpt1.SetParameters(new ReportParameter("rptitle", "Sales Summery Details"));
                Rpt1.SetParameters(new ReportParameter("date", "From " + Convert.ToDateTime(fdate).ToString("d-MMM-yyyy") + " To " + Convert.ToDateTime(todate).ToString("dd-MMM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
                Session["Report1"] = Rpt1;
                string type = "PDF";
                ScriptManager.RegisterStartupScript(this, GetType(), "target", "printEnvelop('" + type + "');", true);

                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }


        }


        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tblSalSumDetails"];
            if (dt == null)
            {
                return;
            }
            ((Label)this.gvSalSummeryDetails.FooterRow.FindControl("lgvFtotalcal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(acamt)", "")) ?
                    0 : dt.Compute("sum(acamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvSalSummeryDetails.FooterRow.FindControl("lgvFbankClear")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(reconamt)", "")) ?
                 0 : dt.Compute("sum(reconamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvSalSummeryDetails.FooterRow.FindControl("lgvFchqdepo")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(depchq)", "")) ?
                 0 : dt.Compute("sum(depchq)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvSalSummeryDetails.FooterRow.FindControl("lgvFreturnchq")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(inhrchq)", "")) ?
                 0 : dt.Compute("sum(inhrchq)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvSalSummeryDetails.FooterRow.FindControl("lgvFretunfchq")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(inhfchq)", "")) ?
                 0 : dt.Compute("sum(inhfchq)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvSalSummeryDetails.FooterRow.FindControl("lgvFpastdchq")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(inhpchq)", "")) ?
                 0 : dt.Compute("sum(inhpchq)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvSalSummeryDetails.FooterRow.FindControl("lgvFrepchq")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(repchq)", "")) ?
                 0 : dt.Compute("sum(repchq)", ""))).ToString("#,##0;(#,##0); ");




            Session["Report1"] = gvSalSummeryDetails;
            ((HyperLink)this.gvSalSummeryDetails.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }




    }
}
