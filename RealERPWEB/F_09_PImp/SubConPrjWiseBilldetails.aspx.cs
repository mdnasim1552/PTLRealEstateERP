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
using RealERPRPT;
namespace RealERPWEB.F_09_PImp
{
    public partial class SubConPrjWiseBilldetails : System.Web.UI.Page
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Sub-Contractor's Project Wise Bill Report ";
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetSupplierName();
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
        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURPROJECTNAME_01", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();


        }

        private void GetSupplierName()
        {

            string comcod = this.GetCompCode();
            string serch1 = "%" + this.txtSrcSub.Text.Trim() + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_SUBCONTRACTOR", "GETSUBCONTRACTORLIST", serch1, "", "", "", "", "", "", "", "");
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

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowBillDetails();


        }
        private void ShowBillDetails()
        {
            Session.Remove("tblconsddetails");
            string comcod = this.GetCompCode();
            string PactCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            string SupplierName = this.ddlSubName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_SUBCONTRACTOR", "RPTSUBCONBILLPRJWISE", PactCode, SupplierName, date, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblConbill"] = ds1.Tables[0];
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

            DataTable dt = (DataTable)Session["tblConbill"];
            this.gvSubBill.DataSource = dt;
            this.gvSubBill.DataBind();
            this.FooterCalculation(dt);

        }

        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFBillAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ? 0.00 :
                 dt.Compute("sum(billamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFPayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payment)", "")) ? 0.00 :
                dt.Compute("sum(payment)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSubBill.FooterRow.FindControl("lgvFNetpayableAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpayable)", "")) ? 0.00 :
                dt.Compute("sum(netpayable)", ""))).ToString("#,##0.00;(#,##0.00); ");



        }




        protected void lnkPrint_Click(object sender, EventArgs e)
        {


            DataTable dt = (DataTable)Session["tblConbill"];
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



            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.EClassExecution.SubConPrjBillDetails>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptSubcontractorPrjWisebill", lst, null, null);

            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtSubConNam", "Sub-Contractor Name : " + this.ddlSubName.SelectedItem.Text.Substring(14)));
            Rpt1.SetParameters(new ReportParameter("txtdate", "Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Sub-Contractor's Project Wise  Bill Report"));
            Rpt1.SetParameters(new ReportParameter("RptFooter", printFooter));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //ReportDocument rptconbill = new RealERPRPT.R_09_PImp.RptSubcontractorPrjWisebill();
            //TextObject rptCname = rptconbill.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptpactdesc = rptconbill.ReportDefinition.ReportObjects["ContractorName"] as TextObject;
            //rptpactdesc.Text =this.ddlSubName.SelectedItem.Text.Substring(14);

            //TextObject rptDate = rptconbill.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptDate.Text = "Date : " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptconbill.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptconbill.SetDataSource((DataTable)Session["tblConbill"]);


            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptConSD.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptconbill;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void ibtnFindSubConName_Click(object sender, EventArgs e)
        {
            this.GetSupplierName();
        }

        protected void ddlSubName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
    }
}