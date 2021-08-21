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
namespace RealERPWEB.F_22_Sal
{
    public partial class LinkCollVsHonouredDetails : System.Web.UI.Page
    {
        ProcessAccess BgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                ((Label)this.Master.FindControl("lblTitle")).Text = "Collection Vs Honoured  Detaiils";
                this.ShowCollVsHonourDetails();
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

            string date = this.Request.QueryString["Date1"].ToString();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            DataTable dt = (DataTable)Session["tblChqdetails"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.SupplierCheqHistory01>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptLinkRptSupplierChequeHistory", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("date1", "Date : " + date));
            Rpt1.SetParameters(new ReportParameter("txtTitle", "Supplier Cheque History"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }




        private void ShowCollVsHonourDetails()
        {
            Session.Remove("tblCollvsHonourdetails");
            string comcod = this.GetCompCode();
            string bankcode = this.Request.QueryString["Bankcode"].ToString();
            string Date1 = this.Request.QueryString["Date1"].ToString();
            string Date2 = this.Request.QueryString["Date2"].ToString();

            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "RPT_DATE_WISE_COLL_HONOUR_DETAILS", Date1, Date2, bankcode, "", "", "", "", "");
            if (ds1 == null)
                return;


            Session["tblCollvsHonourdetails"] = ds1.Tables[0];
            this.Data_Bind();

        }



        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblCollvsHonourdetails"];
            this.gvCollVsHonourDetails.DataSource = dt;
            this.gvCollVsHonourDetails.DataBind();
            this.FooterCalculation(dt);
        }



        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)

                return;

            else
            {


                ((Label)this.gvCollVsHonourDetails.FooterRow.FindControl("lgvFvmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trnam)", "")) ? 0.00 :
                     dt.Compute("sum(trnam)", ""))).ToString("#,##0;(#,##0); ");




                //Session["Report1"] = gvSupChequeHis;
                //((HyperLink)this.gvSupChequeHis.HeaderRow.FindControl("hlbbanknameExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



            }
        }



        protected void gvCollVsHonourDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lgvvounum1");

                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();

                if (vounum == "")
                {
                    return;
                }
                else
                {
                    hlink1.NavigateUrl = "~/F_17_Acc/RptAccVouher.aspx?vounum=" + vounum;
                    hlink1.Target = "_blank";

                }


            }

        }
    }
}