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
    public partial class RptCollectionStatusLO : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "Collection Status";

                DateTime date = System.DateTime.Today;
                DateTime frmdate = Convert.ToDateTime("01" + date.ToString("dd-MMM-yyyy").Substring(2));
                this.txtfrmdate.Text = frmdate.ToString("dd-MMM-yyyy");
                this.txttodate.Text = frmdate.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.ProjectName();
                this.Benefname();

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
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT03", "GETPROJECTNAME02", "", "", "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "pactdesc";
            this.ddlPrjName.DataValueField = "pactcode";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();


        }

        private void Benefname()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_LANDOWNERMGT", "GETBENEFNAME", "", "", "", "", "", "", "", "", "");
            this.ddlbenefname.DataTextField = "benefname";
            this.ddlbenefname.DataValueField = "benefcode";
            this.ddlbenefname.DataSource = ds1.Tables[0];
            this.ddlbenefname.DataBind();
        }



        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tbllocollstatus");
            string comcod = this.GetComeCode();
            string frmdate = this.txtfrmdate.Text.Trim();
            string todate = this.txttodate.Text.Trim();        
            string prjcode = this.ddlPrjName.SelectedValue.ToString() == "000000000000" ? "18%" : this.ddlPrjName.SelectedValue.ToString() + "%";
            string benefname = this.ddlbenefname.SelectedValue.ToString() == "0000000" ? "%" : this.ddlbenefname.SelectedValue.ToString() + "%";
           // string LomonColl = this.GetLoMonColl();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_LANDOWNERMGT", "GETCOLLLANDOWNERBENESTATUS", prjcode, frmdate, todate, benefname, "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tbllocollstatus"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {

            this.gvcollStatus.DataSource = (DataTable)Session["tbllocollstatus"];
            this.gvcollStatus.DataBind();
            this.FooterCal();

            //Session["Report1"] = gvothcoll;
            //((HyperLink)this.gvothcoll.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tbllocollstatus"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvcollStatus.FooterRow.FindControl("lblFamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ? 0.00 :
                dt.Compute("sum(paidamt)", ""))).ToString("#,##0.00;(#,##0); ");

            //Session["Report1"] = gvprobacoll;
            //((HyperLink)this.gvprobacoll.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetComeCode();
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string fromdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["tbllocollstatus"]; 

            string RptTittle = "Monthly Collection Report(L/O)" ;

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_23_CR.EClassLand.RptLandownerColStatus>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptCollectionStatusLO", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("printdate", printdate));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", RptTittle));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("date", "( From " + fromdate + " To " + todate + ") "));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


    }

}