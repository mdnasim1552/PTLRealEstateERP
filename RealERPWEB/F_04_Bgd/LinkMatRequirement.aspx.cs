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
using RealERPRDLC;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_04_Bgd
{
    public partial class LinkMatRequirement : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "MATERIALS REQUIREMENT";
                this.lblvalDate.Text = this.Request.QueryString["date"].ToString();
                this.MatStock();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.MatStock();
        }

        private void MatStock()
        {
            Session.Remove("UserLog");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string rsircode = this.Request.QueryString["rsircode"].ToString();
            string date = this.lblvalDate.Text;


            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTPROMATRECEIPTAANALYSIS", pactcode, rsircode, date, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMatStock.DataSource = null;
                this.gvMatStock.DataBind();
                this.gvRptResBasis.DataSource = null;
                this.gvRptResBasis.DataBind();
                return;
            }
            Session["tbMatStc"] = ds1.Tables[0].DataTableToList<RealEntity.C_04_Bgd.EClassBudget.EClassMatStock>();
            Session["tbMatreq"] = ds1.Tables[1].DataTableToList<RealEntity.C_04_Bgd.EClassBudget.EClassMatRequired>();
            this.Data_Bind();
            this.lblvalproject.Text = ds1.Tables[2].Rows[0]["pactdesc"].ToString();
            this.lblvalMaterial.Text = ds1.Tables[2].Rows[0]["rsirdesc"].ToString();



        }





        private void Data_Bind()
        {

            List<RealEntity.C_04_Bgd.EClassBudget.EClassMatStock> lst = (List<RealEntity.C_04_Bgd.EClassBudget.EClassMatStock>)Session["tbMatStc"];
            List<RealEntity.C_04_Bgd.EClassBudget.EClassMatRequired> lstr = (List<RealEntity.C_04_Bgd.EClassBudget.EClassMatRequired>)Session["tbMatreq"];
            // Session["tbMatreq"] = ds1.Tables[0].DataTableToList<RealEntity.C_04_Bgd.EClassBudget.EClassMatRequired>();
            //DataTable dt = (DataTable)Session["tbMatStc"];
            this.gvMatStock.DataSource = lst;
            this.gvMatStock.DataBind();


            this.gvRptResBasis.DataSource = lstr;
            this.gvRptResBasis.DataBind();

            this.FooterCalculation();

        }


        private void FooterCalculation()
        {
            List<RealEntity.C_04_Bgd.EClassBudget.EClassMatStock> lst = (List<RealEntity.C_04_Bgd.EClassBudget.EClassMatStock>)Session["tbMatStc"];
            List<RealEntity.C_04_Bgd.EClassBudget.EClassMatRequired> lstr = (List<RealEntity.C_04_Bgd.EClassBudget.EClassMatRequired>)Session["tbMatreq"];

            // DataTable dt = (DataTable)Session["tbMatStc"];
            if (lst.Count == 0)
                return;
            ((Label)this.gvMatStock.FooterRow.FindControl("lblgvFinqty")).Text = Convert.ToDouble(lst.Sum(l => l.inqty)).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMatStock.FooterRow.FindControl("lblgvFoutqty")).Text = Convert.ToDouble(lst.Sum(l => l.outqty)).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMatStock.FooterRow.FindControl("lblgvFclsqty")).Text = Convert.ToDouble(lst[(lst.Count) - 1].clqty).ToString("#,##0.00;(#,##0.00); "); ;

            if (lstr.Count == 0)
                return;

            ((Label)this.gvRptResBasis.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble(lstr.Sum(l => l.rptamt)).ToString("#,##0.00;(#,##0.00); "); ;
            ((Label)this.gvRptResBasis.FooterRow.FindControl("lgvFqty")).Text = Convert.ToDouble(lstr.Sum(l => l.rptqty)).ToString("#,##0.00;(#,##0.00); ");




            //Convert.ToDouble(lst.Sum(l => l.clqty)).ToString("#,##0.0000;(#,##0.0000); "); ;
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string date = this.lblvalDate.Text;
            string ProjectNam = this.lblvalproject.Text.Trim().ToString();
            string material = this.lblvalMaterial.Text.Trim();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tblbgd"];

            var lst = (List<RealEntity.C_04_Bgd.EClassBudget.EClassMatStock>)Session["tbMatStc"];
            var lst1 = (List<RealEntity.C_04_Bgd.EClassBudget.EClassMatRequired>)Session["tbMatreq"];

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptMaterialsReqDetails", lst, lst1, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", "PROJECT NAME: " + ProjectNam));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "MATERIALS REQUIREMENT DETAILS"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("date", "DATE: " + date));
            Rpt1.SetParameters(new ReportParameter("material", "MATERIAL: " + material));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        protected void gvMatStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMatStock.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

    }
}

