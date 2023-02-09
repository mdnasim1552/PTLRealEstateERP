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
namespace RealERPWEB.F_09_PImp
{
    public partial class RptSubConBillStatus : System.Web.UI.Page
    {
        ProcessAccess BgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = " SUB CONTRACTOR BILL STATUS";
                this.txtDateto.Text = DateTime.Today.ToString("dd-MMM-yyyy");


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

        protected void btnok_Click(object sender, EventArgs e)
        {
            this.ShowBill();
        }



        private void ShowBill()
        {
            Session.Remove("tblconsddetails");
            string comcod = this.GetCompCode();
            // string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_04", "RPTSUBCONBILLSTATUS", todate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["billstatus"] = ds1.Tables[0];
            this.Data_Bind();

        }



        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["billstatus"];
            this.gvbillstatus.DataSource = dt;
            this.gvbillstatus.DataBind();
            this.FooterCalculation(dt);
        }



        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)

                return;

            else
            {

                ((Label)this.gvbillstatus.FooterRow.FindControl("lgvFgvbillamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ? 0.00 :
                                 dt.Compute("sum(billamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvbillstatus.FooterRow.FindControl("lgvFgvappamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(approvamt)", "")) ? 0.00 :
                     dt.Compute("sum(approvamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvbillstatus.FooterRow.FindControl("lgvFgvunapproved")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(upappovedamt)", "")) ? 0.00 :
                     dt.Compute("sum(upappovedamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvbillstatus.FooterRow.FindControl("lgvFgvpayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payment)", "")) ? 0.00 :
                     dt.Compute("sum(payment)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvbillstatus.FooterRow.FindControl("lgvFgvbalance")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balanec)", "")) ? 0.00 :
                     dt.Compute("sum(balanec)", ""))).ToString("#,##0.00;(#,##0.00); ");


            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string date = this.txtDateto.Text;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["billstatus"];

            var rptlist = dt.DataTableToList<RealEntity.C_09_PIMP.EClassOrder.SubConBillStatus>();

            LocalReport Rpt1a = new LocalReport();

            Rpt1a = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptSubConBillStu", rptlist, null, null);
            Rpt1a.EnableExternalImages = true;
            Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1a.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1a.SetParameters(new ReportParameter("date", "Date :" + date));
            Rpt1a.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1a.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1a;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void gvbillstatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvbillstatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }


}

//    ReportDocument rptConSD = new RealERPRPT.R_09_PImp.RptSubConOverAll();
//    TextObject rptCname = rptConSD.ReportDefinition.ReportObjects["CompName"] as TextObject;
//    rptCname.Text = comnam;
//    //TextObject rptpactdesc = rptConSD.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
//    //rptpactdesc.Text = "Project Name: " + this.ddlProjectName.SelectedItem.Text.Substring(13);
//    //TextObject rptSubdesc = rptConSD.ReportDefinition.ReportObjects["SubConName"] as TextObject;
//    //rptSubdesc.Text = this.ddlSubName.SelectedItem.Text.Substring(13); ;
//    TextObject rptDate = rptConSD.ReportDefinition.ReportObjects["date"] as TextObject;
//    rptDate.Text = "Date: " + Convert.ToDateTime(this.txtDat.Text).ToString("dd-MMM-yyyy");
//    TextObject txtuserinfo = rptConSD.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
//    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
//    rptConSD.SetDataSource((DataTable)Session["tblconsddetails"]);


//    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
//    rptConSD.SetParameterValue("ComLogo", ComLogo);
//    Session["Report1"] = rptConSD;
//    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
//          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";








//protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
//{

//    this.Data_Bind();

//}
//protected void gvbillstatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
//{
//    this.gvbillstatus.PageIndex = e.NewPageIndex;
//    this.Data_Bind();
//}



