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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
using RealERPRPT;
namespace RealERPWEB.F_09_PImp
{

    public partial class RptSubconWiseWrkOrder : System.Web.UI.Page
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Sub-Contractor Wise Order Report";

                this.GetOpeningdate();

                //this.txtDateFrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtDateFrom.Text = "01" + this.txtDateFrom.Text.Trim().Substring(2);
                //this.txtDateto.Text = Convert.ToDateTime(this.txtDateFrom.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.GetSubContractor();
                this.GetProjectName();



            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);


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
            //this.txtDateFrom.Text = "01" + this.txtDateFrom.Text.Trim().Substring(2);
            this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

        }
        protected void btnok_Click(object sender, EventArgs e)
        {
            this.ShowBill();
        }

        private void GetSubContractor()
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
            // this.GetRANumber();

        }
        private void ShowBill()
        {
            Session.Remove("tblconsddetails");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string subcode = this.ddlSubName.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSubName.SelectedValue.ToString() + "%";  //this.ddlSubName.SelectedValue.ToString();
            string pactcode = this.ddlprojname.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlprojname.SelectedValue.ToString() + "%";

            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "GETSUBCONWRKORDER", frmdate, todate, subcode, pactcode, "", "", "", "");
            if (ds1 == null)
                return;
            //Session["tblconsddetails"] = HiddenSameData(ds1.Tables[0]);

            Session["wrkorder"] = ds1.Tables[0];
            Session["conmobno"] = ds1.Tables[1];

            //this.gvSubBill.DataSource = ds1;
            //this.gvSubBill.DataBind();
            //this.FooterCalculation();
            this.Data_Bind();

        }



        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["wrkorder"];
            this.gvwrkorder.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());

            this.gvwrkorder.DataSource = dt;
            this.gvwrkorder.DataBind();

            this.FooterCalculation(dt);
        }



        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)

                return;

            else
            {


                //((Label)this.gvwrkorder.FooterRow.FindControl("lgvFgvbilamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tramt)", "")) ? 0.00 :
                //     dt.Compute("sum(tramt)", ""))).ToString("#,##0.00;(#,##0.00); ");



            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            DataTable dt = (DataTable)Session["wrkorder"];
            DataTable dt2 = (DataTable)Session["conmobno"];
            string frmdate = this.txtDateFrom.Text;
            string todate = this.txtDateto.Text;
            string wrknature = dt2.Rows[0]["wrkn"].ToString();
            string mobno = dt2.Rows[0]["mobno"].ToString();
            string subcon = dt.Rows[0]["csirdesc"].ToString();

            var list = dt.DataTableToList<RealEntity.C_09_PIMP.EClassOrder.SubConWrkOrder>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_09_PIMP.RptSubConWrkOrder", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("subcon", subcon));
            Rpt1.SetParameters(new ReportParameter("frmdate", frmdate));
            Rpt1.SetParameters(new ReportParameter("todate", todate));
            Rpt1.SetParameters(new ReportParameter("wrknature", wrknature));
            Rpt1.SetParameters(new ReportParameter("mobno", mobno));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();

        }
        protected void gvwrkorder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvwrkorder.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }



        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {

        }

    }
}