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
    public partial class LinkSupPayableStatus : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.ShowView();

            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }






        private void ShowView()
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "RptSuppPayable":

                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.lblvalDescription.Text = this.Request.QueryString["Grpdesc"].ToString();

                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowSupplierPayable();
                    break;

                case "RptgrpPayable":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;


            }
        }













        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "RptSuppPayable":
                    this.RptSupPayableStatus();
                    break;



            }





        }

        //private void RptSupPayableStatus() 
        //{



        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = this.GetCompCode();
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string frmdate = this.Request.QueryString["Date1"].ToString() ;
        //    string todate = this.Request.QueryString["Date2"].ToString();
        //        ReportDocument rptImp = new RealERPRPT.R_14_Pro.RptGrpwiseSupPayable();
        //    TextObject txtCompanyName = rptImp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
        //    txtCompanyName.Text = comnam;
        //    TextObject txtWork = rptImp.ReportDefinition.ReportObjects["txtWork"] as TextObject;
        //    txtWork.Text = this.lblvalDescription.Text.Trim();

        //    TextObject txtdat = rptImp.ReportDefinition.ReportObjects["rpttxtdat"] as TextObject;
        //    txtdat.Text = " (" + "From  " + frmdate + " To " + todate + ")";
        //    DataTable dt = (DataTable)Session["tblpayst"];
        //    TextObject txtuserinfo = rptImp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
        //    rptImp.SetDataSource(dt);
        //    //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    //rptImp.SetParameterValue("ComLogo", ComLogo);
        //    Session["Report1"] = rptImp;
        //    this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                        this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        //}


        private void RptSupPayableStatus()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();

            string session = hst["session"].ToString();

            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblpayst"];


            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptSupPayableStatus>();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptGrpwiseSupPayable", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtWork", this.lblvalDescription.Text.Trim()));

            Rpt1.SetParameters(new ReportParameter("todate", " (" + "From  " + frmdate + " To " + todate + ")"));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Monthly Payable (Group Wise )"));
            Rpt1.SetParameters(new ReportParameter("RptFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }





        private void ShowSupplierPayable()
        {
            Session.Remove("tblpayst");
            string comcod = this.GetCompCode();
            string fromdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            string pactcode = "%";
            string supplier = "99%";
            string mRptGroup = "12";
            string mwgcod = this.Request.QueryString["grp"].ToString();


            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_SUPPLER", "RPTGRPSUPPAYABLE", fromdate, todate, pactcode, supplier, mRptGroup, mwgcod, "", "", "");
            if (ds1 == null)
            {
                this.gvPayableStatus.DataSource = null;
                this.gvPayableStatus.DataBind();
                return;
            }


            Session["tblpayst"] = ds1.Tables[0];
            this.Data_Bind();


        }










        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblpayst"];
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "RptSuppPayable":

                    this.gvPayableStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvPayableStatus.DataSource = dt;
                    this.gvPayableStatus.DataBind();
                    this.FooterCalCulation();

                    break;

                case "RptgrpPayable":


                    this.gvPayableSum.DataSource = dt;
                    this.gvPayableSum.DataBind();
                    this.FooterCalCulation();

                    break;




            }






        }


        private void FooterCalCulation()
        {

            DataTable dt = ((DataTable)Session["tblpayst"]).Copy();
            if (dt.Rows.Count == 0)
                return;


            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "RptSuppPayable":



                    ((Label)this.gvPayableStatus.FooterRow.FindControl("lgvFOpening")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ?
                                  0 : dt.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPayableStatus.FooterRow.FindControl("lgvFperiodAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(periodam)", "")) ?
                                  0 : dt.Compute("sum(periodam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPayableStatus.FooterRow.FindControl("lgvFnetpayableAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netbal)", "")) ?
                                  0 : dt.Compute("sum(netbal)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "RptgrpPayable":

                    ((Label)this.gvPayableSum.FooterRow.FindControl("lgvFOpeninggp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ?
                                  0 : dt.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPayableSum.FooterRow.FindControl("lgvFperiodAmtgp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(periodam)", "")) ?
                                  0 : dt.Compute("sum(periodam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPayableSum.FooterRow.FindControl("lgvFnetpayableAmtgp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netbal)", "")) ?
                                  0 : dt.Compute("sum(netbal)", ""))).ToString("#,##0;(#,##0); ");
                    break;




            }



        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();

        }



        protected void gvPayableStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPayableStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void gvPayableStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    HyperLink desc = (HyperLink)e.Row.FindControl("hlnkgvresdesc");
            //    Label Opening = (Label)e.Row.FindControl("lgvOpening");
            //    Label periodAmt = (Label)e.Row.FindControl("lgvperiodAmt");
            //    Label netpayableAmt = (Label)e.Row.FindControl("lgvnetpayableAmt");



            //    string rescode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

            //    if (rescode == "")
            //    {
            //        return;
            //    }
            //    if (ASTUtility.Right(rescode, 4) == "AAAA")
            //    {

            //        desc.Font.Bold = true;
            //        Opening.Font.Bold = true;
            //        periodAmt.Font.Bold = true;
            //        netpayableAmt.Font.Bold = true;
            //        desc.Style.Add("text-align", "right");


            //    }

            //}
        }



    }
}