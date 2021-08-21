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
namespace RealERPWEB.F_45_GrAcc
{
    public partial class LinkGrpAccount02 : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.lblHeadtitle.Text = (this.Request.QueryString["Type"].ToString() == "BalConfirmation") ? "Balance Confirmation Information"
                    : "Date Wise Sales";
                this.SelectView();
            }
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "BalConfirmation":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.lblfrmdate.Text = this.Request.QueryString["Date1"];
                    this.lbltodate.Text = this.Request.QueryString["Date2"];
                    this.ShowBalConfirmation();
                    break;
                case "SalesProj":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.sfrDate.Text = this.Request.QueryString["Date1"];
                    this.stDate.Text = this.Request.QueryString["Date2"];
                    this.salesStatus();
                    break;
            }
        }

        private void ShowBalConfirmation()
        {

            ViewState.Remove("tbAcc");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string date1 = this.Request.QueryString["Date1"];
            string date2 = this.Request.QueryString["Date2"];

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTCASHANDBANKBAL", date1, date2, "12", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvCABankBal.DataSource = null;
                this.gvCABankBal.DataBind();

                return;
            }
            DataTable dt = ds1.Tables[0];
            ViewState["tbAcc"] = ds1.Tables[0];
            this.Data_Bind();

        }
        private void salesStatus()
        {
            Session.Remove("tbAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.Request.QueryString["pactcode"];
            string frdate = this.Request.QueryString["Date1"];
            string todate = this.Request.QueryString["Date2"];

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTDAYWISHSAL", PactCode, frdate, todate, "12", "%", "", "", "", "");
            if (ds1 == null)
            {
                this.gvDayWSale.DataSource = null;
                this.gvDayWSale.DataBind();
                return;
            }

            this.lblPrijDesc.Text = ds1.Tables[0].Rows[0]["pactdesc"].ToString();
            ViewState["tbAcc"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }

            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {


                case "SalesProj":
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


                    break;

            }

            return dt1;
        }
        private void FooterCalculation()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)ViewState["tbAcc"];
            if (dt.Rows.Count == 0)
                return;

            switch (type)
            {

                case "SalesProj":
                    ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tuamt)", "")) ?
                                    0 : dt.Compute("sum(tuamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDSAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(suamt)", "")) ?
                                    0 : dt.Compute("sum(suamt)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDDisAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(disamt)", "")) ?
                    //                0 : dt.Compute("sum(disamt)", ""))).ToString("#,##0;(#,##0); ");


                    break;

            }


        }
        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tbAcc"];

            if ((dt.Rows.Count == 0)) //Problem
                return;

            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "BalConfirmation":
                    this.gvCABankBal.DataSource = dt;
                    this.gvCABankBal.DataBind();
                    break;
                case "SalesProj":

                    this.gvDayWSale.DataSource = dt;
                    this.gvDayWSale.DataBind();

                    this.FooterCalculation();
                    break;
            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "BalConfirmation":
                    this.PrintBalConfirmation();
                    break;
                case "SalesProj":
                    this.rptDayWSale();
                    break;

            }
        }

        protected void PrintBalConfirmation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccBalConfirmation();
            DataTable dt = (DataTable)ViewState["tbAcc"];
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            rpttxtcompanyname.Text = comnam;
            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtfdate.Text = "(From " + Convert.ToDateTime(this.lblfrmdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.lbltodate.Text).ToString("dd-MMM-yyyy") + " )";
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);
            Session["Report1"] = rptstk;
            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void rptDayWSale()
        {
            //Iqbal  Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblData"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_22_Sal.Sales_BO.DaywiseSale>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptDayWiseSales", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Date", "From : " + Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy") + " To:" + Convert.ToDateTime(this.Request.QueryString["Date2"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Day Wise Sales"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("Level", "Level: Details"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)ViewState["tbAcc"];
            //ReportDocument rptsale = new RealERPRPT.R_22_Sal.rptDayWiseSales();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptCode = rptsale.ReportDefinition.ReportObjects["CodeDesc"] as TextObject;
            //rptCode.Text = "Level: Details";
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptDate.Text = "From : " + Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy") + " To:" + Convert.ToDateTime(this.Request.QueryString["Date2"]).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void gvCABankBal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink description = (HyperLink)e.Row.FindControl("HLgvDescbankcb");
                Label opnbalcb = (Label)e.Row.FindControl("lblgvopnbalcb");
                Label opnliabilitiescb = (Label)e.Row.FindControl("lblgvopnliabilitiescb");
                Label clobalbankcb = (Label)e.Row.FindControl("lblgvclobalbankcb");
                Label cloliabilitiescb = (Label)e.Row.FindControl("lblgvcloliabilitiescb");
                Label netReceived = (Label)e.Row.FindControl("lblgvnetReceived");
                Label netPayment = (Label)e.Row.FindControl("lblgvnetPayment");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    description.Font.Bold = true;
                    opnbalcb.Font.Bold = true;
                    opnliabilitiescb.Font.Bold = true;
                    clobalbankcb.Font.Bold = true;
                    cloliabilitiescb.Font.Bold = true;

                    netReceived.Font.Bold = true;
                    netPayment.Font.Bold = true;
                    description.Style.Add("text-align", "right");


                }
                else if (ASTUtility.Right(code, 4) == "0000")
                {

                    description.Font.Bold = true;
                    opnbalcb.Font.Bold = true;
                    opnliabilitiescb.Font.Bold = true;
                    clobalbankcb.Font.Bold = true;
                    cloliabilitiescb.Font.Bold = true;
                    netReceived.Font.Bold = true;
                    netPayment.Font.Bold = true;
                    description.Style.Add("text-align", "left");


                }


            }

        }

        protected void gvDayWSale_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDayWSale.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}