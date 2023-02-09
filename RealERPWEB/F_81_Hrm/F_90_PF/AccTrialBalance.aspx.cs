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
using System.Xml.Linq;
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
using RealERPRDLC;
namespace RealERPWEB.F_81_Hrm.F_90_PF
{


    public partial class AccTrialBalance : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        Common compUtility = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();

            //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            //((Label)this.Master.FindControl("lblTitle")).Text = "Accounts Trial Balance";
            this.GetDate();

        }
        private void GetDate()
        {
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
                return;

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.txtDatefrom.Text = startdate + date.Substring(2);
            this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private DataSet GetDataForReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11).ToString();
            string date2 = this.txtDateto.Text.Substring(0, 11).ToString();
            string level = this.ddlReportLevel.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "TB_COMPANY_0" + level, date1, date2, "", "", "", "", "", "", "");
            return ds1;
        }
        protected void lnkok_Click(object sender, EventArgs e)
        {
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;
            this.dgv1.DataSource = ds1.Tables[0];
            this.dgv1.DataBind();
            ((Label)this.dgv1.FooterRow.FindControl("lblfopnamt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0); ") + "<br>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0); ");
            //this.dgv1.Columns[2].FooterText = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0.00;(#,##0.00); ") + "<br>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.dgv1.FooterRow.FindControl("lblfDramt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["dram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.dgv1.FooterRow.FindControl("lblfCramt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["cram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.dgv1.FooterRow.FindControl("lblfcloamt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ") + "<br>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ");

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
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            string frmdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;
            var AccTrialBl1 = ds1.Tables[0].DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.AccTrialBl1>();
            LocalReport Rpt1 = new LocalReport();
            //Hashtable reportParm = new Hashtable();
            string opndram = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0); ");
            string opncram = Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0); ");
            string dram = Convert.ToDouble(ds1.Tables[1].Rows[0]["dram"]).ToString("#,##0;(#,##0); ");
            string cram = Convert.ToDouble(ds1.Tables[1].Rows[0]["cram"]).ToString("#,##0;(#,##0); ");
            string closdram = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");
            string closcram = Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ");
            string closam = Convert.ToDouble(ds1.Tables[1].Rows[0]["closam"]).ToString("#,##0;(#,##0); ");

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptTrialBl1", AccTrialBl1, null, null);
            Rpt1.SetParameters(new ReportParameter("companyname", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("txtopndram", opndram));
            Rpt1.SetParameters(new ReportParameter("txtopncram", opncram));
            Rpt1.SetParameters(new ReportParameter("txtdram", dram));
            Rpt1.SetParameters(new ReportParameter("txtcram", cram));
            Rpt1.SetParameters(new ReportParameter("txtclosdram", closdram));
            Rpt1.SetParameters(new ReportParameter("txtcloscram", closcram));
            Rpt1.SetParameters(new ReportParameter("txtnetam", closam));
            Rpt1.SetParameters(new ReportParameter("txtHeader", (this.Request.QueryString["Type"].ToString().Trim() == "TBConsolidated") ? "TRIAL BALANCE - " + this.ddlReportLevel.SelectedValue.ToString().Trim() : "TRIAL BALANCE ( Level - " + this.ddlReportLevel.SelectedValue.ToString().Trim() + " )"));
            Rpt1.SetParameters(new ReportParameter("txtdate", "( For The Period From " + Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd-MMM-yyyy") + " )"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("frmdate", frmdate));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataSet ds1 = this.GetDataForReport();
            //if (ds1 == null)
            //    return;

            //if (ds1.Tables[0].Rows.Count == 0)
            //    return;

            //ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccTrialBalance();

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text =comnam;

            //TextObject txtadress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            //txtadress.Text = comadd;
            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text ="TRIAL BALANCE - "+ this.ddlReportLevel.SelectedValue.ToString().Trim();
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "(From " + Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd-MMM-yyyy")+")";
            //TextObject txtopeingname1 = rptstk.ReportDefinition.ReportObjects["opeingname1"] as TextObject;
            //txtopeingname1.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0); ");

            //TextObject txtopeingname2 = rptstk.ReportDefinition.ReportObjects["opeingname2"] as TextObject;
            //txtopeingname2.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0); ");

            //TextObject txtdramount = rptstk.ReportDefinition.ReportObjects["dramount"] as TextObject;
            //txtdramount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["dram"]).ToString("#,##0;(#,##0); "); ;

            //TextObject txtcramount = rptstk.ReportDefinition.ReportObjects["cramount"] as TextObject;
            //txtcramount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["cram"]).ToString("#,##0;(#,##0); "); ;

            //TextObject txtclosingamount1 = rptstk.ReportDefinition.ReportObjects["closingamount1"] as TextObject;
            //txtclosingamount1.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");

            //TextObject txtclosingamount2 = rptstk.ReportDefinition.ReportObjects["closingamount2"] as TextObject;
            //txtclosingamount2.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); "); ;


            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(ds1.Tables[0]);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void dgv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string mCOMCOD = comcod;
            string mACTCODE = ((Label)e.Row.FindControl("lblgvcode")).Text;
            string mTRNDAT1 = this.txtDatefrom.Text;
            string mTRNDAT2 = this.txtDateto.Text;

            if (ASTUtility.Right(mACTCODE, 4) == "0000")
                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                     "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            else
                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                     "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
        }
    }
}
