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
namespace RealERPWEB.F_17_Acc
{
    public partial class RptAccPDCStatus : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString().Trim();
                this.SelectView();
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtfromdate.Text = "01" + date.Substring(2);
                //this.txttodate.Text = date;
                this.lblHeader.Text = (type == "DayWisePDC") ? "PDC Issue Status Report" : "";


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
                case "DayWisePDC":
                    this.rbtPayment.SelectedIndex = 0;
                    break;
            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "DayWisePDC":
                    if (this.rbtPayment.SelectedIndex == 2)
                    {
                        this.MultiView1.ActiveViewIndex = 1;
                        this.ShowPDCIsuDetails();

                    }
                    else if (this.rbtPayment.SelectedIndex == 1)
                    {
                        this.MultiView1.ActiveViewIndex = 2;
                        this.ShowPDCIsuSummary();
                    }
                    else if (this.rbtPayment.SelectedIndex == 3)
                    {
                        this.MultiView1.ActiveViewIndex = 3;
                        this.ShowPDCIsuCostWise();
                    }
                    else
                    {
                        this.MultiView1.ActiveViewIndex = 0;
                        this.ShowDayWisePDC();
                        this.rbtPayment.Visible = true;
                    }
                    break;
            }
        }


        private void ShowDayWisePDC()
        {
            Session.Remove("tbpdcStatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "PDCISUDAYWISE", frmdate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPDCDayWise.DataSource = null;
                this.gvPDCDayWise.DataBind();
                return;
            }
            Session["tbpdcStatus"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }
        private void ShowPDCIsuDetails()
        {
            Session.Remove("tbpdcStatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //string CallType = (this.chbDetails.Checked) ? "RPTDAILYPAYMENTDETAILS" : "RPTDAILYPAYMENT";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "PDCISUDETAILS", frmdate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPdcIsuDet.DataSource = null;
                this.gvPdcIsuDet.DataBind();
                return;
            }
            Session["tbpdcStatus"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }

        private void ShowPDCIsuSummary()
        {
            Session.Remove("tbpdcStatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "PDCISUSUMMARY", frmdate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPdcIsuSummary.DataSource = null;
                this.gvPdcIsuSummary.DataBind();
                return;
            }
            Session["tbpdcStatus"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }

        private void ShowPDCIsuCostWise()
        {
            Session.Remove("tbpdcStatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "RPTISUCOSTWISE", frmdate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPdcIsuCostWise.DataSource = null;
                this.gvPdcIsuCostWise.DataBind();
                return;
            }
            Session["tbpdFotter"] = ds1.Tables[1];
            Session["tbpdcStatus"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {


                case "DayWisePDC":
                    if (this.rbtPayment.SelectedIndex == 0)
                    {
                        //string grp = dt1.Rows[0]["grp"].ToString();
                        string pactcode = dt1.Rows[0]["actcode"].ToString();
                        string cactcode = dt1.Rows[0]["cactcode"].ToString();
                        int j;
                        for (j = 1; j < dt1.Rows.Count; j++)
                        {
                            if ((dt1.Rows[j]["actcode"].ToString() == pactcode) && (dt1.Rows[j]["cactcode"].ToString() == cactcode))
                            {
                                pactcode = dt1.Rows[j]["actcode"].ToString();
                                cactcode = dt1.Rows[j]["cactcode"].ToString();
                                dt1.Rows[j]["actdesc"] = "";
                                dt1.Rows[j]["cactdesc"] = "";
                            }

                            else
                            {
                                if (dt1.Rows[j]["actcode"].ToString() == pactcode)
                                    dt1.Rows[j]["actdesc"] = "";
                                if (dt1.Rows[j]["cactcode"].ToString() == cactcode)
                                    dt1.Rows[j]["cactdesc"] = "";
                                pactcode = dt1.Rows[j]["actcode"].ToString();
                                cactcode = dt1.Rows[j]["cactcode"].ToString();
                            }

                        }
                    }
                    else if (this.rbtPayment.SelectedIndex == 1)
                    {
                        //string actcode1 = dt1.Rows[0]["actcode1"].ToString();
                        //for (int j = 1; j < dt1.Rows.Count; j++)
                        //{
                        //    if (dt1.Rows[j]["actcode1"].ToString() == actcode1)
                        //    {
                        //        actcode1 = dt1.Rows[j]["actcode1"].ToString();
                        //        dt1.Rows[j]["actdesc1"] = "";
                        //    }
                        //    else
                        //    {
                        //        actcode1 = dt1.Rows[j]["actcode1"].ToString();
                        //    }
                        //}
                    }
                    else if (this.rbtPayment.SelectedIndex == 3)
                    {
                        string actcode1 = dt1.Rows[0]["actcode1"].ToString();
                        for (int j = 1; j < dt1.Rows.Count; j++)
                        {
                            if (dt1.Rows[j]["actcode1"].ToString() == actcode1)
                            {
                                actcode1 = dt1.Rows[j]["actcode1"].ToString();
                                dt1.Rows[j]["actdesc1"] = "";
                            }
                            else
                            {
                                actcode1 = dt1.Rows[j]["actcode1"].ToString();
                            }
                        }
                    }
                    else
                    {

                    }

                    break;

            }

            return dt1;

        }


        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "DayWisePDC":
                    if (this.rbtPayment.SelectedIndex == 0)
                    {
                        this.gvPDCDayWise.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvPDCDayWise.DataSource = (DataTable)Session["tbpdcStatus"];
                        this.gvPDCDayWise.DataBind();

                        Session["Report1"] = gvPDCDayWise;
                        if (((DataTable)Session["tbpdcStatus"]).Rows.Count > 0)
                            ((HyperLink)this.gvPDCDayWise.HeaderRow.FindControl("hlbtnbtbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        this.FooterCalculation();
                    }
                    else if (this.rbtPayment.SelectedIndex == 2)
                    {
                        this.gvPdcIsuDet.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvPdcIsuDet.DataSource = (DataTable)Session["tbpdcStatus"];
                        this.gvPdcIsuDet.DataBind();

                        this.FooterCalculation();
                    }
                    else if (this.rbtPayment.SelectedIndex == 1)
                    {
                        this.gvPdcIsuSummary.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvPdcIsuSummary.DataSource = (DataTable)Session["tbpdcStatus"];
                        this.gvPdcIsuSummary.DataBind();
                        this.FooterCalculation();
                    }
                    else
                    {
                        this.gvPdcIsuCostWise.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvPdcIsuCostWise.DataSource = (DataTable)Session["tbpdcStatus"];
                        this.gvPdcIsuCostWise.DataBind();
                        this.FooterCalculation();
                    }
                    break;
            }

        }


        private void FooterCalculation()
        {
            DataTable dt1 = (DataTable)Session["tbpdcStatus"];
            if (dt1.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {
                case "DayWisePDC":
                    if (this.rbtPayment.SelectedIndex == 0)
                    {
                        DataTable ddt1 = dt1.Copy();
                        DataView dv1 = ddt1.DefaultView;
                        dv1.RowFilter = ("typesum='ZZZZ'");
                        ddt1 = dv1.ToTable();
                        //((Label)this.gvPDCDayWise.FooterRow.FindControl("lgvFCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ddt1.Compute("sum(cramt)", "")) ?
                        //       0 : ddt1.Compute("sum(cramt)", ""))).ToString("#,##0;(#,##0); ");
                    }
                    else if (this.rbtPayment.SelectedIndex == 2)
                    {
                        DataTable ddt = dt1.Copy();
                        DataView dv = ddt.DefaultView;
                        dv.RowFilter = ("grp='B'");
                        ddt = dv.ToTable();
                        ((Label)this.gvPdcIsuDet.FooterRow.FindControl("lgvFPayAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ddt.Compute("sum(payam)", "")) ?
                              0 : ddt.Compute("sum(payam)", ""))).ToString("#,##0;(#,##0); ");
                    }
                    else if (this.rbtPayment.SelectedIndex == 1)
                    {
                        ((Label)this.gvPdcIsuSummary.FooterRow.FindControl("lgvFPayAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(payam)", "")) ?
                                  0 : dt1.Compute("sum(payam)", ""))).ToString("#,##0;(#,##0); ");
                    }
                    else
                    {
                        DataTable ddtc = (DataTable)Session["tbpdFotter"];

                        ((Label)this.gvPdcIsuCostWise.FooterRow.FindControl("lgvFTDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ddtc.Compute("sum(dram)", "")) ?
                                  0 : ddtc.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");
                    }
                    break;
            }



        }
        protected void gvCollVsCleared_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label date = (Label)e.Row.FindControl("lgvDate");
                Label collChqno = (Label)e.Row.FindControl("lgcollChqno");
                Label CollAmt = (Label)e.Row.FindControl("lgvCollAmt");
                Label ClChqno = (Label)e.Row.FindControl("lgvClChqno");
                Label cuamt = (Label)e.Row.FindControl("lgvclcuram");
                Label pramt = (Label)e.Row.FindControl("lgvClPramt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "clcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    //date.Font.Bold = true;
                    collChqno.Font.Bold = true;
                    CollAmt.Font.Bold = true;
                    ClChqno.Font.Bold = true;
                    cuamt.Font.Bold = true;
                    pramt.Font.Bold = true;
                    collChqno.Style.Add("text-align", "right");
                    ClChqno.Style.Add("text-align", "right");


                }

            }
        }


        private void PrintPdcIsuSummary()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RealERPRPT.R_17_Acc.RptDailyPaymentSumm();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptCname.Text = comnam;


            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            rptftdate.Text = "As on Date: " + fromdate;
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["tbpdcStatus"]);

            Session["Report1"] = rptstate;
            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintPDCDayWise()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RealERPRPT.R_17_Acc.rptPDCIsuDayWise();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;


            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            rptftdate.Text = "As on Date: " + fromdate;


            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["tbpdcStatus"]);

            Session["Report1"] = rptstate;
            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintPdcIsuDetails()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RealERPRPT.R_17_Acc.RptPaymentDetails();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptCname.Text = comnam;

            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            rptftdate.Text = "As on Date: " + fromdate;
            TextObject rpAmt = rptstate.ReportDefinition.ReportObjects["txtPayAmt"] as TextObject;
            rpAmt.Text = ((Label)this.gvPdcIsuDet.FooterRow.FindControl("lgvFPayAmt")).Text;
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["tbpdcStatus"]);

            Session["Report1"] = rptstate;
            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintIsuCostWise()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");

            ReportDocument rptstate = new RealERPRPT.R_17_Acc.RptDailyPaymentCostDet();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptCname.Text = comnam;


            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            rptftdate.Text = "As on Date: " + fromdate;
            TextObject rpttotal = rptstate.ReportDefinition.ReportObjects["txttotal"] as TextObject;
            rpttotal.Text = ((Label)this.gvPdcIsuCostWise.FooterRow.FindControl("lgvFTDrAmt")).Text;

            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["tbpdcStatus"]);

            Session["Report1"] = rptstate;
            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "DayWisePDC":
                    if (this.rbtPayment.SelectedIndex == 2)
                    {
                        this.PrintPdcIsuDetails();
                    }
                    else if (this.rbtPayment.SelectedIndex == 1)
                    {
                        this.PrintPdcIsuSummary();
                    }
                    else if (this.rbtPayment.SelectedIndex == 3)
                    {
                        this.PrintIsuCostWise();
                    }
                    else
                    {
                        this.PrintPDCDayWise();
                    }
                    break;
            }


        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }


        protected void gvPDCDayWise_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label prodesc = (Label)e.Row.FindControl("lgactdesc");
                Label amt = (Label)e.Row.FindControl("lgvcramt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "typesum")).ToString().Trim();


                if (code == "")
                {
                    return;
                }

                else if (ASTUtility.Right(code, 1) == "Z")
                {
                    prodesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    prodesc.Style.Add("text-align", "right");

                }


            }
        }
        protected void gvPDCDayWise_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPDCDayWise.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvPdcIsuDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPdcIsuDet.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvPdcIsuSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPdcIsuSummary.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvPdcIsuSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void gvPdcIsuDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Rescode = (Label)e.Row.FindControl("lgvResCod");
                Label Resdesc = (Label)e.Row.FindControl("lgvResName");
                Label Payamt = (Label)e.Row.FindControl("lgvPayAmt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "A")
                {

                    Rescode.Font.Bold = true;
                    Resdesc.Font.Bold = true;
                    Payamt.Font.Bold = true;
                    Payamt.Style.Add("text-align", "left");


                }
                if (code == "B")
                {
                    Rescode.Style.Add("text-align", "right");


                }

            }

        }

        protected void gvPdcIsuCostWise_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Payamt = (Label)e.Row.FindControl("lgvAmt");
                Label Code = (Label)e.Row.FindControl("lblgvCode");
                Label Desc = (Label)e.Row.FindControl("lgcActDesc");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode1")).ToString();
                string rescode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode2")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "A")
                {
                    Payamt.Font.Bold = true;
                    Payamt.Style.Add("text-align", "left");


                }
                if (code == "B" && ASTUtility.Left(actcode, 2) == "40" && ASTUtility.Right(rescode, 10) == "0000000000")
                {
                    Code.Font.Bold = true;
                    Desc.Font.Bold = true;
                    Payamt.Font.Bold = true;
                    Desc.Style.Add("text-align", "right");


                }
            }
        }
    }
}
