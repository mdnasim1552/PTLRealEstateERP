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
namespace RealERPWEB.F_24_CC
{

    public partial class LinkOtherCollDetials : System.Web.UI.Page
    {
        ProcessAccess BgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                ((Label)this.Master.FindControl("lblTitle")).Text = "Details Information";
                this.ShowOtherCollDetails();
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




        private void ShowOtherCollDetails()
        {
            Session.Remove("tblCollvsHonourdetails");
            string comcod = this.GetCompCode();
            string pactcode = this.Request.QueryString["Pactcode"].ToString();
            string usircode = this.Request.QueryString["Usircode"].ToString();

            string Date1 = this.Request.QueryString["Date1"].ToString();
            string Date2 = this.Request.QueryString["Date2"].ToString();

            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTTRANSRTYPEWISEDETAILS", Date1, Date2, pactcode, usircode, "", "", "", "");
            if (ds1 == null)
                return;


            Session["tblothercoll"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }


        private DataTable HiddenSameData(DataTable dt1)

        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode;
            string usircode;



            pactcode = dt1.Rows[0]["pactcode"].ToString();
            usircode = dt1.Rows[0]["usircode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["usircode"].ToString() == usircode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    usircode = dt1.Rows[j]["usircode"].ToString();

                    dt1.Rows[j]["pactdesc"] = "";
                    dt1.Rows[j]["custname"] = "";

                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    usircode = dt1.Rows[j]["usircode"].ToString();

                }

            }
            return dt1;

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblothercoll"];
            this.gvOthCollDetails.DataSource = dt;
            this.gvOthCollDetails.DataBind();
            this.FooterCalculation(dt);
        }



        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)

                return;

            else
            {


                ((Label)this.gvOthCollDetails.FooterRow.FindControl("lgvFsales")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(sales)", "")) ? 0.00 :
                     dt.Compute("sum(sales)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvOthCollDetails.FooterRow.FindControl("lgvFregvat")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(regavat)", "")) ? 0.00 :
                     dt.Compute("sum(regavat)", ""))).ToString("#,##0;(#,##0); ");

                ((Label)this.gvOthCollDetails.FooterRow.FindControl("lgvFsolarpanel")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(spanal)", "")) ? 0.00 :
                     dt.Compute("sum(spanal)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvOthCollDetails.FooterRow.FindControl("lgvFdwrk")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(adwork)", "")) ? 0.00 :
                     dt.Compute("sum(adwork)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvOthCollDetails.FooterRow.FindControl("lgvFsociteyfee")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(societyfee)", "")) ? 0.00 :
                     dt.Compute("sum(societyfee)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvOthCollDetails.FooterRow.FindControl("lgvFdelaycharge")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(delcharge)", "")) ? 0.00 :
                      dt.Compute("sum(delcharge)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvOthCollDetails.FooterRow.FindControl("lgvFTransfer")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(transfee)", "")) ? 0.00 :
                   dt.Compute("sum(transfee)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvOthCollDetails.FooterRow.FindControl("lgvFOther")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(others)", "")) ? 0.00 :
                   dt.Compute("sum(others)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvOthCollDetails.FooterRow.FindControl("lgvFCommer")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(comrecv)", "")) ? 0.00 :
                  dt.Compute("sum(comrecv)", ""))).ToString("#,##0;(#,##0); ");


                //Session["Report1"] = gvSupChequeHis;
                //((HyperLink)this.gvSupChequeHis.HeaderRow.FindControl("hlbbanknameExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



            }
        }




    }
}