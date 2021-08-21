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
namespace RealERPWEB.F_14_Pro
{
    public partial class LinkRptSupplierChequeHistory : System.Web.UI.Page
    {
        ProcessAccess BgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                ((Label)this.Master.FindControl("lblTitle")).Text = "Supplier Cheque Details";
                //this.txtDat.Text = DateTime.Today.ToString("dd-MMM-yyyy");

                this.ShowChqueDetails();
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }


        //protected void lbtnOk_Click(object sender, EventArgs e)
        //{
        //    this.ShowChqueDetails();
        //}

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




        private void ShowChqueDetails()
        {
            Session.Remove("tblChqdetails");
            string comcod = this.GetCompCode();
            string bankcode = this.Request.QueryString["bankcode"].ToString();
            string Date = this.Request.QueryString["Date1"].ToString();
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "RPTSUPBAKCHEQUEDETAIL", Date, bankcode, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            //Session["tblconsddetails"] = HiddenSameData(ds1.Tables[0]);

            Session["tblChqdetails"] = ds1.Tables[0];
            this.Data_Bind();

        }



        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblChqdetails"];
            this.gvSupChequeHis.DataSource = dt;
            this.gvSupChequeHis.DataBind();
            this.FooterCalculation(dt);
        }



        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)

                return;

            else
            {


                ((Label)this.gvSupChequeHis.FooterRow.FindControl("lgvFChqAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamt)", "")) ? 0.00 :
                     dt.Compute("sum(tamt)", ""))).ToString("#,##0.00;(#,##0.000); ");

                ((Label)this.gvSupChequeHis.FooterRow.FindControl("lgvFadv")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(advamt)", "")) ? 0.00 :
               dt.Compute("sum(advamt)", ""))).ToString("#,##0.00;(#,##0.000); ");

                ((Label)this.gvSupChequeHis.FooterRow.FindControl("lgvFdues1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam1)", "")) ? 0.00 :
                    dt.Compute("sum(dueam1)", ""))).ToString("#,##0.00;(#,##0.000); ");

                ((Label)this.gvSupChequeHis.FooterRow.FindControl("lgvFdues2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam2)", "")) ? 0.00 :
                 dt.Compute("sum(dueam1)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvSupChequeHis.FooterRow.FindControl("lgvFdues2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam2)", "")) ? 0.00 :
                 dt.Compute("sum(dueam2)", ""))).ToString("#,##0.00;(#,##0.000); ");
                ((Label)this.gvSupChequeHis.FooterRow.FindControl("lgvFdues3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam3)", "")) ? 0.00 :
                 dt.Compute("sum(dueam3)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvSupChequeHis.FooterRow.FindControl("lgvFdues4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam4)", "")) ? 0.00 :
                 dt.Compute("sum(dueam4)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvSupChequeHis.FooterRow.FindControl("lgvFdues5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam5)", "")) ? 0.00 :
                 dt.Compute("sum(dueam5)", ""))).ToString("#,##0.00;(#,##0.00); ");

                ((Label)this.gvSupChequeHis.FooterRow.FindControl("lgvFdues7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam7)", "")) ? 0.00 :
                dt.Compute("sum(dueam7)", ""))).ToString("#,##0.00;(#,##0.00); ");

                ((Label)this.gvSupChequeHis.FooterRow.FindControl("lgvFdues6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam6)", "")) ? 0.00 :
                dt.Compute("sum(dueam6)", ""))).ToString("#,##0.00;(#,##0.00); ");




                Session["Report1"] = gvSupChequeHis;
                ((HyperLink)this.gvSupChequeHis.HeaderRow.FindControl("hlbbanknameExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



            }
        }

        protected void gvSupChequeHis_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string mCOMCOD = comcod;
            string head = ((HyperLink)e.Row.FindControl("HLgvDesc")).Text.ToString();
            string subcode = ((Label)e.Row.FindControl("lgvsubcode")).Text;
            string Bankcode = ((Label)e.Row.FindControl("lgvbankcode")).Text;

            string date = this.Request.QueryString["Date1"].ToString();

            hlink1.NavigateUrl = "LinkRptSupplierChequeDetails.aspx?&Subcode=" + subcode +
                     "&Date1=" + date + "&Bankcode=" + Bankcode;
        }
    }
}