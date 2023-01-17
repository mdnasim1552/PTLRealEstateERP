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

    public partial class SubConBillWiseReport : System.Web.UI.Page
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Sub-Contractor Wise Bill Report";

                this.GetOpeningdate();

                this.GetConTractorName();


            }

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        private void GetOpeningdate()
        {

            string comcod = this.GetCompCode();

            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.txtDateFrom.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["opndat"]).ToString("dd-MMM-yyyy");
            this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

        }
        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURPROJECTNAME_01", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprojname.DataTextField = "pactdesc";
            this.ddlprojname.DataValueField = "pactcode";
            this.ddlprojname.DataSource = ds1.Tables[0];
            this.ddlprojname.DataBind();

        }

        private void GetConTractorName()
        {
            string comcod = this.GetCompCode();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "GETSUBCONTRACTOR", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlSubName.DataTextField = "sirdesc";
            this.ddlSubName.DataValueField = "sircode";
            this.ddlSubName.DataSource = ds1.Tables[0];
            this.ddlSubName.DataBind();
            this.GetProjectName();



        }


        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();

        }
        protected void ibtnFindSubConName_Click(object sender, EventArgs e)
        {
            this.GetConTractorName();

        }

        protected void ddlSubName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void btnok_Click(object sender, EventArgs e)
        {
            this.ShowBillDetails();


        }


        private void ShowBillDetails()
        {
            Session.Remove("tblsubbill");
            string comcod = this.GetCompCode();
            string PactCode = (this.ddlprojname.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlprojname.SelectedValue.ToString() + "%";
            string SubconName = this.ddlSubName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_SUBCONTRACTOR", "RPTSUBCONSBILLREPORT", PactCode, SubconName, frmdate, todate, "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblsubbill"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

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


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblsubbill"];
            this.gvSubBill.DataSource = dt;
            this.gvSubBill.DataBind();
            //this.FooterCalculation();
        }



        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblsubbill"];
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFBillAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ? 0.00 :
                         dt.Compute("sum(billamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFjournal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(directamt)", "")) ? 0.00 :
                        dt.Compute("sum(directamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFpaidAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payment)", "")) ? 0.00 :
                        dt.Compute("sum(payment)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFbalance")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balance)", "")) ? 0.00 :
                        dt.Compute("sum(balance)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }



        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);



        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string frmdate = this.txtDateFrom.Text;
            string todate = this.txtDateto.Text;


            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblsubbill"];

            var rptlist = dt.DataTableToList<RealEntity.C_09_PIMP.SubConBill.Subconbillwise>();

            LocalReport Rpt1a = new LocalReport();

            Rpt1a = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptSubConWiseBillReport", rptlist, null, null);
            Rpt1a.EnableExternalImages = true;
            Rpt1a.EnableHyperlinks = true;
            Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1a.SetParameters(new ReportParameter("Supname", "Sub-Contractor Name : " + ddlSubName.SelectedItem.Text.Substring(13).ToString()));

            Rpt1a.SetParameters(new ReportParameter("date", frmdate + " To " + todate));
            Rpt1a.SetParameters(new ReportParameter("footer", printFooter));
            Rpt1a.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1a;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        protected void gvSubBill_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }
        protected void gvSubBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // HyperLink hlink = (HyperLink)e.Row.FindControl("HLgvvounum");
                Label lgvbildate = (Label)e.Row.FindControl("lgvbildate");

                Label lgvbillamt = (Label)e.Row.FindControl("lgvbillamt");
                Label lgvjournal = (Label)e.Row.FindControl("lgvjournal");
                Label lgvpaidamt = (Label)e.Row.FindControl("lgvpaidamt");
                Label lgvbalance = (Label)e.Row.FindControl("lgvbalance");




                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billdate")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code.Trim() == "Total")
                {

                    lgvbildate.Font.Bold = true;
                    lgvbillamt.Font.Bold = true;
                    lgvjournal.Font.Bold = true;
                    lgvpaidamt.Font.Bold = true;
                    lgvbalance.Font.Bold = true;
                    lgvbildate.Style.Add("text-align", "Center");


                }

                if (pactcode.Trim() == "ZZZZZZZZZZZZ")
                {

                    lgvbildate.Font.Bold = true;
                    lgvbillamt.Font.Bold = true;
                    lgvjournal.Font.Bold = true;
                    lgvpaidamt.Font.Bold = true;
                    lgvbalance.Font.Bold = true;
                    lgvbildate.Style.Add("text-align", "Center");


                }
            }
        }
    }
}