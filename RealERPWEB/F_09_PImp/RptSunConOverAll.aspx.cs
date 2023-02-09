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
using RealERPRDLC;

namespace RealERPWEB.F_09_PImp
{
    public partial class RptSunConOverAll : System.Web.UI.Page
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
                //((Label)this.Master.FindControl("lblTitle")).Text = " SUB CONTRACTOR BILL  (R/A Wise)";
                this.txtDat.Text = DateTime.Today.ToString("dd-MMM-yyyy");
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
            string date = Convert.ToDateTime(this.txtDat.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "RPTSUBCONSDDETAILSSUMMARY", date, "", "", "", "", "", "", "", "");
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
        //        return dt1;
        //    string pactcode = dt1.Rows[0]["csircode"].ToString();

        //    for (int j = 1; j < dt1.Rows.Count; j++)
        //    {
        //        if (dt1.Rows[j]["csircode"].ToString() == pactcode)
        //        {
        //            pactcode = dt1.Rows[j]["csircode"].ToString();
        //            dt1.Rows[j]["sirdesc"] = "";
        //        }

        //        else
        //        {
        //            pactcode = dt1.Rows[j]["csircode"].ToString();
        //        }
        //    }
        //    return dt1;

        //}

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblconsddetails"];
            this.gvSubBill.DataSource = dt;
            this.gvSubBill.DataBind();
            this.FooterCalculation(dt);
        }



        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)

                return;

            else
            {


                ((Label)this.gvSubBill.FooterRow.FindControl("lgvFBillAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ? 0.00 :
                     dt.Compute("sum(billamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSubBill.FooterRow.FindControl("lgvFSecurityAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(sdamt)", "")) ? 0.00 :
                    dt.Compute("sum(sdamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSubBill.FooterRow.FindControl("lgvFdedAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dedamt)", "")) ? 0.00 :
                       dt.Compute("sum(dedamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSubBill.FooterRow.FindControl("lgvFPenaltyAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(penamt)", "")) ? 0.00 :
                       dt.Compute("sum(penamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSubBill.FooterRow.FindControl("lgvFNetAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netamt)", "")) ? 0.00 :
                      dt.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");

                ((Label)this.gvSubBill.FooterRow.FindControl("lgvFPayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payment)", "")) ? 0.00 :
                      dt.Compute("sum(payment)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSubBill.FooterRow.FindControl("lgvFNetpayableAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpayable)", "")) ? 0.00 :
                      dt.Compute("sum(netpayable)", ""))).ToString("#,##0;(#,##0); ");


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
            DataTable dt = (DataTable)Session["tblconsddetails"];
            var list = dt.DataTableToList<RealEntity.C_09_PIMP.SubConBill.RptSubConBillOverAll>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_09_PIMP.RptSubConOverAll", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Sub-Contractor Bill Over All"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtDat.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void gvSubBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string mDate = this.txtDat.Text;



            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "csircode")).ToString().Trim();
            //string lebel2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "leb2")).ToString().Trim();
            string csirdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sirdesc")).ToString().Trim();

            if (code == "")
            {
                return;
            }

            hlink1.NavigateUrl = hlink1.NavigateUrl = "LinkSubContractorSd.aspx?ssircode=" + code + "&ssirdesc=" + csirdesc + "&Date1=" + mDate;


            //else if (lebel2 == "")
            //{

            //        if (ASTUtility.Right(mACTCODE, 4) == "0000")
            //            hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
            //                 "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            //        else
            //            hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
            //                 "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&actdesc=" + mACTDESC;
            //    }
            //    else
            //    {
            //        if (ASTUtility.Right(mACTCODE, 4) == "0000")
            //            hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
            //                 "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            //        else
            //            hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=detailsTB&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
            //                 "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            //    }

            //}
        }
    }
}