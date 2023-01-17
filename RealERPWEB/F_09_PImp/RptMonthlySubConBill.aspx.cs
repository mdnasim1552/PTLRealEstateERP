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
    public partial class RptMonthlySubConBill : System.Web.UI.Page
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
                //((Label)this.Master.FindControl("lblTitle")).Text = " SUB CONTRACTOR Monthly BILL ";
                this.txtDateFrom.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                // this.GetConTractorName();
                //this.GetProjectName();
                //this.SelectView();

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


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowBillDetails();
        }





        private void ShowBillDetails()
        {
            Session.Remove("tblconsddetails");
            string comcod = this.GetCompCode();
            string frmdat = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string tdate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTSUBCONTOPSHEET", frmdat, tdate, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            //Session["tblconsddetails"] = HiddenSameData(ds1.Tables[0]);

            Session["tblconsddetails"] = ds1.Tables[0];

            //this.gvSubBill.DataSource = ds1;
            //this.gvSubBill.DataBind();
            //this.FooterCalculation();
            this.Data_Bind();

        }

        //private DataTable HiddenSameData(DataTable dt1)
        //{


        //    if (dt1.Rows.Count == 0)
        //    {
        //        return dt1;
        //    }
        //    string pactcode = dt1.Rows[0]["pactcode"].ToString();
        //    for (int j = 1; j < dt1.Rows.Count; j++)
        //    {
        //        if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
        //        {

        //            dt1.Rows[j]["pactdesc"] = "";

        //        }

        //        pactcode = dt1.Rows[j]["pactcode"].ToString();

        //    }

        //    return dt1;



        //}  




        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblconsddetails"];
            this.gvSubConBill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSubConBill.DataSource = dt;
            this.gvSubConBill.DataBind();
            this.FooterCalculation(dt);
        }



        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)

                return;

            else
            {


                ((Label)this.gvSubConBill.FooterRow.FindControl("lgvFBillAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ? 0.00 :
                     dt.Compute("sum(billamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSubConBill.FooterRow.FindControl("lgvFSecurityAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(sdamt)", "")) ? 0.00 :
                    dt.Compute("sum(sdamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSubConBill.FooterRow.FindControl("lgvFdedAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dedamt)", "")) ? 0.00 :
                       dt.Compute("sum(dedamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSubConBill.FooterRow.FindControl("lgvFPenaltyAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(penamt)", "")) ? 0.00 :
                       dt.Compute("sum(penamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSubConBill.FooterRow.FindControl("lgvFNetAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netamt)", "")) ? 0.00 :
                      dt.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");

                ((Label)this.gvSubConBill.FooterRow.FindControl("lgvFPayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payment)", "")) ? 0.00 :
                      dt.Compute("sum(payment)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSubConBill.FooterRow.FindControl("lgvFNetpayableAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpayable)", "")) ? 0.00 :
                      dt.Compute("sum(netpayable)", ""))).ToString("#,##0;(#,##0); ");


            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblconsddetails"];

            string date = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + ")";
            string rpttitle = "Monthly Sub-Contractor Bill";
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptMonthlySubConBill>();


            LocalReport Rpt1 = new LocalReport();
            switch (comcod)
            {
                // case "3101":
                case "3316": //Assure
                case "3315":// Assure

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptMonthlySubConBillAssure", lst, null, null);
                    break;
                default:
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptMonthlySubConBill", lst, null, null);
                    break;

            }
            //Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptMonthlySubConBill", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", rpttitle));

            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptConSD = new RealERPRPT.R_09_PImp.RptMonthlySubContractor();
            //TextObject rptCname = rptConSD.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject txtdate = rptConSD.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + ")";

            ////TextObject rptpactdesc = rptConSD.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            ////rptpactdesc.Text = "Project Name: " + this.ddlProjectName.SelectedItem.Text.Substring(13);
            ////TextObject rptSubdesc = rptConSD.ReportDefinition.ReportObjects["SubConName"] as TextObject;
            ////rptSubdesc.Text = this.ddlSubName.SelectedItem.Text.Substring(13); ;
            ////TextObject rptDate = rptConSD.ReportDefinition.ReportObjects["date"] as TextObject;
            ////rptDate.Text = "Date: " + Convert.ToDateTime(this.txtDat.Text).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptConSD.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptConSD.SetDataSource((DataTable)Session["tblconsddetails"]);


            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptConSD.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptConSD;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }


        protected void gvSubConBill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvSubConBill.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
    }
}