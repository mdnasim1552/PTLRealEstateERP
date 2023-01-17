using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using Microsoft.Reporting.WinForms;
using System.Xml.Linq;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_12_Inv
{
    public partial class RptDayWiseAdvance : System.Web.UI.Page
    {

        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Day Wise Order Advanced";

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtDatefrom.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.ShowProjectlist();
            }



        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void ShowProjectlist()
        {
            string comcod = this.GetCompCode();
            string srchTxt = "%" + this.txtSrcPRro.Text.Trim() + "%";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "PRJCODELIST", srchTxt, "", "", "", "", "", "", "", "");
            Session["tblPrjCod"] = ds1.Tables[0];
            this.ddlProName.DataTextField = "prjdesc1";
            this.ddlProName.DataValueField = "prjcod";
            this.ddlProName.DataSource = (DataTable)Session["tblPrjCod"];
            this.ddlProName.DataBind();
        }
        protected void ibtnFindProName_Click(object sender, EventArgs e)
        {
            ShowProjectlist();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string projectname = this.ddlProName.SelectedValue.ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11).ToString();
            string date2 = this.txtDateto.Text.Substring(0, 11).ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "ORDERADVANCED", projectname, date1, date2, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvOrderAdvanced.DataSource = null;
                this.gvOrderAdvanced.DataBind();
                return;
            }
            Session["tblDaywiseAd"] = ds1.Tables[0];
            this.gvOrderAdvanced.DataSource = ds1.Tables[0];
            this.gvOrderAdvanced.DataBind();

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
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

            DataTable dt = (DataTable)Session["tblDaywiseAd"];



            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.RptDayWiseAdv>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptsDayWiseAdvanced", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", " Day Wise Order Advanced "));
            Rpt1.SetParameters(new ReportParameter("txtDate", " (From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + ")"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));




            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //ReportDocument rptdaywise = new RealERPRPT.R_14_Pro.RptsDayWiseAdvanced();
            //TextObject CompName = rptdaywise.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comnam;
            //TextObject txtdate = rptdaywise.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = " (From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + ")";
            //TextObject txtuserinfo = rptdaywise.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptdaywise.SetDataSource((DataTable)Session["tblDaywiseAd"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptdaywise.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptdaywise;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }


    }
}