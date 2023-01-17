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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;

namespace RealERPWEB.F_23_CR
{
    public partial class RptOtherCollHistory : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                //((Label)this.Master.FindControl("lblTitle")).Text = "Other's Collection History";

                //var dtoday = System.DateTime.Today;
                //this.txttodate.Text = dtoday.ToString("dd-MMM-yyyy");
                //this.txtfrmdate.Text = new System.DateTime(dtoday.Year, dtoday.Month, 1).ToString("dd-MMM-yyyy");
                this.ProjectName();
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

       


        private void ProjectName()
        {
            string comcod = this.GetComeCode();
            string SrchSupplier = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT06", "GETOTHERPROJECTNAME", SrchSupplier, "", "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "pactesc";
            this.ddlPrjName.DataValueField = "pactcode";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();
            this.GetResourceName();
            
        }

        protected void ddlPrjName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetResourceName();
        }

        private  void GetResourceName()
        {
            string comcod = this.GetComeCode();
            string prjcode = this.ddlPrjName.SelectedValue.ToString();           
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT06", "GETOTHERRESTNAME", prjcode, "", "", "", "", "", "", "", "");
            this.ddlreslist.DataTextField = "resdesc";
            this.ddlreslist.DataValueField = "rescode";
            this.ddlreslist.DataSource = ds1.Tables[0];
            this.ddlreslist.DataBind();
          

        }


       
        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();           
            string prjcode = this.ddlPrjName.SelectedValue.ToString();
            string rescode = this.ddlreslist.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT06", "GETOTHERMRHISTORY", prjcode, rescode, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblmrhistory"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            
            this.gvothcoll.DataSource = (DataTable)Session["tblmrhistory"];
            this.gvothcoll.DataBind();
            this.FooterCal();

            //Session["Report1"] = gvothcoll;
            //((HyperLink)this.gvothcoll.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblmrhistory"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvothcoll.FooterRow.FindControl("lblFgvreciveamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ? 0.00 :
                dt.Compute("sum(paidamt)", ""))).ToString("#,##0.00;(#,##0); ");

            Session["Report1"] = gvothcoll;
            ((HyperLink)this.gvothcoll.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        private void lbtnPrint_Click(object sender, EventArgs e)
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
            DataTable dt = (DataTable)Session["tblmrhistory"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.OtherCollHistory>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptOtherCollHistory", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Other's Collection History"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("date", "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

      
    }

}