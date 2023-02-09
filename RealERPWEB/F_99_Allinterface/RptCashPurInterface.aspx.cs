using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using RealERPLIB;
namespace RealERPWEB.F_99_Allinterface
{
    public partial class RptCashPurInterface : System.Web.UI.Page
    {
        // public static string recvno = "", centrid = "", custid = "", orderno1 = "", orderdat = "", Delstatus = "", Delorderno = "", RDsostatus = "";
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "PURCHASE INTERFACE";//


                // string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //  this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                //this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.RadioButtonList1.SelectedIndex = 0;
                //this.SaleRequRpt();

                lbtnOk_Click(null, null);
                //this.txtIme_TextChanged(null, null);



                //this.RadioButtonList1_SelectedIndexChanged(null, null);

            }
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)Session["tblspledger"];
            //if (dt == null)
            lbtnOk_Click(null, null);


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        //protected void txtdate_TextChanged(object sender, EventArgs e)
        //{
        //    this.SaleRequRpt();
        //    this.RadioButtonList1_SelectedIndexChanged(null, null);
        //}
        //protected void lnkOk_Click(object sender, EventArgs e)
        //{
        //    this.SaleRequRpt();
        //}

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            //DateTime frmdate = Convert.ToDateTime(this.txtfrmdate.Text);
            //DateTime todate = Convert.ToDateTime(this.txttodate.Text);
            //int mon = ASTUtility.Datediff(todate, frmdate);
            //if (mon > 1)
            //{

            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert(' Month Less Than Equal Two.');", true);
            //    return;
            //}


            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }

        protected void lnkInteface_Click(object sender, EventArgs e)
        {
            this.pnlInterf.Visible = true;
            this.pnlPurchase.Visible = false;
            this.RadioButtonList1.SelectedIndex = 0;
        }
        protected void lnkRept_Click(object sender, EventArgs e)
        {
            this.pnlInterf.Visible = false;
            this.pnlPurchase.Visible = true;
        }


        private void PurchaseInfoRpt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();

            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            // string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string paytype = "002";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE02", "RPTPURCHASEDASHBOARD", frmdate, paytype, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.RadioButtonList1.Items[0].Text = "<span class='fa  fa-signal fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["reqqty"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + "Requisition Status" + "</span>";
            this.RadioButtonList1.Items[1].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["chqqty"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Requisition Checked" + "</span>";
            this.RadioButtonList1.Items[2].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["raproqty"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Rate Proposal" + "</span>";
            this.RadioButtonList1.Items[3].Text = "<span class='fa fa-check-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["appqty"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Requisition Approval" + "</span>";


            this.RadioButtonList1.Items[4].Text = ((ASTUtility.Left(comcod, 1) == "7") ? "Goods" : "") + "<span class='fa fa-life-ring fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["mrrqty"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + " Received" + "</span>";

            this.RadioButtonList1.Items[5].Text = "<span class='fa fa-file-text-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["billqty"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Bill Confirmation" + "</span>";
            this.RadioButtonList1.Items[6].Text = "<span class='fa fa-line-chart  fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["compqty"]).ToString("#,##0;(#,##0); ") + "</span><a href='../F_17_Acc/AccPurchase?Type=Entry&genno='>" + "<span class=lbldata2>" + "Accounts Update" + "</a></span>";







            //this.RadioButtonList1.Items[0].Text = "Requisition Status" + "<span class=lbldata>" + ds1.Tables[4].Rows[0]["reqqty"].ToString() + "</span>";
            //this.RadioButtonList1.Items[1].Text = "Rate Proposal" + "<span class='lbldata'>" + ds1.Tables[4].Rows[0]["raproqty"].ToString() + "</span>";
            //this.RadioButtonList1.Items[2].Text = "Requisition Approval" + "<span class=lbldata>" + ds1.Tables[4].Rows[0]["appqty"].ToString() + "</span>";
            //this.RadioButtonList1.Items[3].Text = "Order Process" + "<span class=lbldata>" + ds1.Tables[4].Rows[0]["ordprqty"].ToString() + "</span>";
            //this.RadioButtonList1.Items[4].Text = "Purchase Order" + "<span class=lbldata>" + ds1.Tables[4].Rows[0]["worderqty"].ToString() + "</span>";
            //this.RadioButtonList1.Items[5].Text = ((ASTUtility.Left(comcod, 1) == "7") ? "Goods" : "Material") + " Received" + "<span class=lbldata>" + ds1.Tables[4].Rows[0]["mrrqty"].ToString() + "</span>";
            //this.RadioButtonList1.Items[6].Text = "Bill Confirmation" + "<span class=lbldata>" + ds1.Tables[4].Rows[0]["billqty"].ToString() + "</span>";
            //this.RadioButtonList1.Items[7].Text = "Total Complite" + "<span class=lbldata>" + ds1.Tables[4].Rows[0]["billqty"].ToString() + "</span>";


            // All Recive
            DataTable dt = new DataTable();

            DataView dv = new DataView();
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            this.Data_Bind("gvReqInfo", dv.ToTable());

            //Req Check
            dt = ((DataTable)ds1.Tables[1]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("cstatus = 'Requisition Checked' ");
            this.Data_Bind("gvReqChk", dv.ToTable());
            //

            //Rate Proposal
            dt = ((DataTable)ds1.Tables[1]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("cstatus = 'Rate Proposal' ");
            this.Data_Bind("gvRatePro", dv.ToTable());

            //Rate Approval
            dt = ((DataTable)ds1.Tables[1]).Copy();
            dv = dt.DefaultView;

            dv.RowFilter = ("cstatus='Rate Approval'  ");
            //dv.RowFilter = ("empid ='" + usrid + "'");
            this.Data_Bind("gvRateApp", dv.ToTable());









            //MRR
            dt = (DataTable)ds1.Tables[4];
            this.Data_Bind("grvMRec", dt);


            //Bill
            dt = (DataTable)ds1.Tables[5];
            this.Data_Bind("gvPurBill", dt);


            ////Compilte
            //dt = ((DataTable)ds1.Tables[4]).Copy();
            //this.Data_Bind("grvComp", dt);


            ////Delivery (9)
            //dt = ((DataTable)ds1.Tables[0]).Copy();
            //dv = dt.DefaultView;
            //dv.RowFilter = ("qcdone = 'Ok' and billst='' ");
            //this.Data_Bind("gvBill", dv.ToTable());




        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    //Status

                    this.pnlReqInfo.Visible = true;
                    this.PnlReqChq.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.PanelComp.Visible = false;
                    this.PanelBill.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes["style"] = "background:#5A5C59; display:block";
                    break;
                case "1":
                    //Status

                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = true;
                    this.pnlRatePro.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.PanelComp.Visible = false;
                    this.PanelBill.Visible = false;
                    this.RadioButtonList1.Items[1].Attributes["style"] = "background:#5A5C59; display:block";
                    break;

                case "2":
                    //Rate Proposal
                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.pnlRatePro.Visible = true;
                    this.pnlRateApp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;

                    this.RadioButtonList1.Items[2].Attributes["style"] = "background:#5A5C59; display:block";
                    break;
                case "3":
                    //Approval

                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.pnlRateApp.Visible = true;
                    this.PanelRecv.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;

                    this.RadioButtonList1.Items[3].Attributes["style"] = "background:#5A5C59; display:block";


                    break;



                case "4":
                    //MRR
                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelRecv.Visible = true;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;
                    this.RadioButtonList1.Items[4].Attributes["style"] = "background:#5A5C59; display:block";

                    break;
                case "5":
                    //Bill
                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = true;

                    this.RadioButtonList1.Items[5].Attributes["style"] = "background:#5A5C59; display:block";

                    break;
                case "6":
                    //Compilte
                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelComp.Visible = true;

                    this.RadioButtonList1.Items[6].Attributes["style"] = "background:#5A5C59; display:block";

                    break;




            }
        }

        protected void gvReqInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                // HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnEditIN");
                HyperLink hlnkgvgvmrfno = (HyperLink)e.Row.FindControl("hlnkgvgvmrfno");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string mrfno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrfno")).ToString();




                TableCell cell = e.Row.Cells[9];
                string cstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cstatus")).ToString();
                if (cstatus == "Bill Confirm")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4BCF9E");
                }
                if (cstatus == "Purchase Invoice")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#00CBF3");
                }
                if (cstatus == "Rate Proposal")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#5EB75B");
                }
                if (cstatus == "Order Process")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D95350");
                }
                if (cstatus == "Rate Approval")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#EFAD4D");
                }
                if (cstatus == "Materials Receved")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#769BF4");
                }
                if (cstatus == "Purchase Order")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#76C9B5");
                }



                //string fDate = Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy");
                //string tDate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");



                //hlink2.NavigateUrl = "~/F_20_Service/RecProductEntry?Type=Entry";
                //hlink2.ToolTip = "Create New";

                hlnkgvgvmrfno.NavigateUrl = "~/F_14_Pro/RptPurchasetracking?Type=Purchasetrk&reqno=" + reqno;


            }
        }
        protected void gvReqChk_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_12_Inv/PurReqEntry?InputType=ReqCheck&prjcode=" + pactcode + "&genno=" + reqno;

            }
        }
        protected void gvRatePro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ReqPrint&reqno=" + reqno;
                hlink2.NavigateUrl = "~/F_12_Inv/PurReqApproval?Type=RateInput&prjcode=" + pactcode + "&genno=" + reqno;

            }
        }
        protected void gvRateApp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ReqPrint&reqno=" + reqno;
                hlink2.NavigateUrl = "~/F_12_Inv/PurReqApproval?Type=Approval&prjcode=" + pactcode + "&genno=" + reqno;

            }
        }
        protected void gvOrdeProc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_14_Pro/PurAprovEntry?InputType=PurProposal&genno=" + reqno;

            }
        }
        protected void gvWrkOrd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                string aprovno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "aprovno")).ToString();


                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_14_Pro/PurWrkOrderEntry?InputType=OrderEntry&genno=" + aprovno;

            }
        }
        protected void grvMRec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=OrderPrint&orderno=" + orderno;// +"&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_12_Inv/PurMRREntry?Type=Entry&prjcode=" + pactcode + "&genno=" + orderno + "&sircode=" + sircode;
            }
        }
        protected void gvPurBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();
                // string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_14_Pro/PurBillEntry?Type=BillEntry&genno=" + orderno + "&sircode=" + sircode;

            }
        }
        protected void grvComp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                //hlink2.NavigateUrl = "~/F_07_Inv/PurBillEntry?Type=BillEntry";
                hlink1.NavigateUrl = "~/F_99_Allinterface/PuchasePrint?Type=BillPrint&comcod=" + comcod + "&billno=" + billno;
                hlink2.NavigateUrl = "~/F_14_Pro/RptPurchaseStatus?Type=Purchase&Rpt=Purchasetrk";

            }
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
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
            }

            return dt1;
        }
        private void Data_Bind(string gv, DataTable dt)
        {


            switch (gv)
            {
                case "gvReqInfo":
                    this.gvReqInfo.DataSource = HiddenSameData(dt);
                    this.gvReqInfo.DataBind();

                    break;
                case "gvReqChk":
                    this.gvReqChk.DataSource = HiddenSameData(dt);
                    this.gvReqChk.DataBind();

                    break;

                case "gvRatePro":

                    this.gvRatePro.DataSource = HiddenSameData(dt);
                    this.gvRatePro.DataBind();

                    if (dt.Rows.Count == 0)
                        return;

                    break;
                case "gvRateApp":

                    this.gvRateApp.DataSource = HiddenSameData(dt);
                    this.gvRateApp.DataBind();

                    if (dt.Rows.Count == 0)
                        return;

                    break;

                case "gvOrdeProc":

                    //this.gvOrdeProc.DataSource = HiddenSameData(dt);
                    //this.gvOrdeProc.DataBind();

                    if (dt.Rows.Count == 0)
                        return;



                    break;
                case "gvWrkOrd":

                    //this.gvWrkOrd.DataSource = HiddenSameData(dt);
                    //this.gvWrkOrd.DataBind();

                    break;

                case "grvMRec":

                    this.grvMRec.DataSource = HiddenSameData(dt);
                    this.grvMRec.DataBind();

                    //if (dt.Rows.Count == 0)
                    //    return;


                    //((Label)this.gvDispatch.FooterRow.FindControl("lblDispAmtTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itmamt)", "")) ?
                    //0 : dt.Compute("sum(itmamt)", ""))).ToString("#,##0.00;(#,##0.00);");
                    //((Label)this.gvDispatch.FooterRow.FindControl("lblDispPTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ?
                    //0 : dt.Compute("sum(paidamt)", ""))).ToString("#,##0.00;(#,##0.00);");
                    //((Label)this.gvDispatch.FooterRow.FindControl("lblDispQTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itmqty)", "")) ?
                    //0 : dt.Compute("sum(itmqty)", ""))).ToString("#,##0;(#,##0);");


                    break;




                case "gvPurBill":

                    this.gvPurBill.DataSource = HiddenSameData(dt);
                    this.gvPurBill.DataBind();
                    break;
                case "grvComp":

                    this.grvComp.DataSource = HiddenSameData(dt);
                    this.grvComp.DataBind();
                    break;


            }


            this.FooterCalculation(gv, dt);




        }

        private void FooterCalculation(string gv, DataTable dt)
        {


            if (dt.Rows.Count == 0)
                return;
            switch (gv)
            {

                case "gvReqInfo":
                    ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFApamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(apamt)", "")) ?
                    0 : dt.Compute("sum(apamt)", ""))).ToString("#,##0;(#,##0);");




                    break;
                case "gvReqChk":


                    break;

                case "gvRatePro":



                    break;
                case "gvRateApp":



                    break;

                case "gvOrdeProc":


                    //((Label)this.gvOrdeProc.FooterRow.FindControl("lblgvFOrProAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(apamt)", "")) ?
                    //0 : dt.Compute("sum(apamt)", ""))).ToString("#,##0;(#,##0);"); 



                    break;
                case "gvWrkOrd":
                    //((Label)this.gvWrkOrd.FooterRow.FindControl("lblgvFWoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(woamt)", "")) ?
                    //0 : dt.Compute("sum(woamt)", ""))).ToString("#,##0;(#,##0);"); 


                    break;

                case "grvMRec":
                    ((Label)this.grvMRec.FooterRow.FindControl("lblgvFWoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recvamt)", "")) ?
                    0 : dt.Compute("sum(recvamt)", ""))).ToString("#,##0;(#,##0);");

                    ((Label)this.grvMRec.FooterRow.FindControl("lblgvFreceivedamtor")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrramt)", "")) ?
                  0 : dt.Compute("sum(mrramt)", ""))).ToString("#,##0;(#,##0);");
                    ((Label)this.grvMRec.FooterRow.FindControl("lblgvFbalamtor")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balamt)", "")) ?
                  0 : dt.Compute("sum(balamt)", ""))).ToString("#,##0;(#,##0);");


                    break;




                case "gvPurBill":
                    ((Label)this.gvPurBill.FooterRow.FindControl("lblgvFmrramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrramt)", "")) ?
                    0 : dt.Compute("sum(mrramt)", ""))).ToString("#,##0;(#,##0);");



                    break;
                case "grvComp":


                    break;
            }
        }



        protected void lnkbtnPrintRD_Click(object sender, EventArgs e)
        {


            //    int rbtIndex = Convert.ToInt16(this.RadioButtonList1.SelectedIndex);
            //    this.RadioButtonList1.Items[rbtIndex].Attributes["style"] = "background: #189697;  display:block; -webkit-border-radius:30px;-moz-border-radius: 30px;border-radius: 30px;";

            //    string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //    string centrid = ASTUtility.Left(code, 12);
            //    string orderno = ASTUtility.Right(code, 14);

            //    try
            //    {

            //        string comcod = this.GetCompCode();
            //        Hashtable hst = (Hashtable)Session["tblLogin"];
            //        string comnam = hst["comnam"].ToString();
            //        string compname = hst["compname"].ToString();
            //        string comadd = hst["comadd1"].ToString();
            //        string username = hst["username"].ToString();
            //        string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");




            //        DataSet ds2 = accData.GetTransInfo(comcod, "dbo_Sales.SP_REPORT_SALES_INFO", "RPTCUSTINFORMATION", orderno, centrid, "", "", "", "", "", "", "");
            //        if (ds2 == null)
            //            return;
            //        ReportDocument rptSOrder = new ReportDocument();
            //        //string qType = this.Request.QueryString["Type"].ToString();
            //        //if (qType == "dNote")
            //        //{
            //        //    rptSOrder = new MFGRPT.R_23_SaM.RptSalDelNoteZelta();
            //        //    TextObject txtHeader = rptSOrder.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //        //    txtHeader.Text = "DELIVERY NOTE";
            //        //}
            //        //else
            //        //{
            //        rptSOrder = new MFGRPT.R_23_SaM.RptSalOrdrZelta();
            //        TextObject txtHeader = rptSOrder.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //        txtHeader.Text = "SALES ORDER";
            //        // }


            //        TextObject txtrptcomp = rptSOrder.ReportDefinition.ReportObjects["Company"] as TextObject;
            //        txtrptcomp.Text = comnam;



            //        TextObject txtCompAdd = rptSOrder.ReportDefinition.ReportObjects["txtCompAdd"] as TextObject;
            //        txtCompAdd.Text = comadd;

            //        TextObject txtsaledate = rptSOrder.ReportDefinition.ReportObjects["txtsaledate"] as TextObject;
            //        txtsaledate.Text = ds2.Tables[2].Rows[0]["orderdat"].ToString().Trim();

            //        TextObject txtCust = rptSOrder.ReportDefinition.ReportObjects["txtCust"] as TextObject;
            //        txtCust.Text = ds2.Tables[2].Rows[0]["name"].ToString().Trim();

            //        TextObject txtAdd = rptSOrder.ReportDefinition.ReportObjects["txtAdd"] as TextObject;
            //        txtAdd.Text = ds2.Tables[2].Rows[0]["addr"].ToString().Trim();

            //        TextObject txtPhone = rptSOrder.ReportDefinition.ReportObjects["txtPhone"] as TextObject;
            //        txtPhone.Text = ds2.Tables[2].Rows[0]["phone"].ToString().Trim();

            //        TextObject txtTrans = rptSOrder.ReportDefinition.ReportObjects["txtTrans"] as TextObject;
            //        txtTrans.Text = ds2.Tables[0].Rows[0]["courie"].ToString().Trim();

            //        TextObject txtStore = rptSOrder.ReportDefinition.ReportObjects["txtStore"] as TextObject;
            //        txtStore.Text = ds2.Tables[2].Rows[0]["storename"].ToString().Trim();


            //        TextObject txtCode = rptSOrder.ReportDefinition.ReportObjects["txtCode"] as TextObject;
            //        txtCode.Text = ds2.Tables[2].Rows[0]["sirtdes"].ToString().Trim();

            //        TextObject txtOrdTime = rptSOrder.ReportDefinition.ReportObjects["txtOrdTime"] as TextObject;
            //        txtOrdTime.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["posteddat"]).ToString("hh:mm:ss tt").Trim();

            //        TextObject txtRemarks = rptSOrder.ReportDefinition.ReportObjects["txtRemarks"] as TextObject;
            //        txtRemarks.Text = (ds2.Tables[2].Rows[0]["narration"]).ToString().Trim();

            //        TextObject txtChannel = rptSOrder.ReportDefinition.ReportObjects["txtChannel"] as TextObject;
            //        txtChannel.Text = (ds2.Tables[2].Rows[0]["chnl"]).ToString().Trim();

            //        TextObject txtsaleNo = rptSOrder.ReportDefinition.ReportObjects["txtsaleNo"] as TextObject;
            //        txtsaleNo.Text = orderno;
            //        //BALANCE 

            //        DataTable dt = ds2.Tables[0];
            //        DataTable dt2 = ds2.Tables[1];
            //        DataTable dt3 = ds2.Tables[2];

            //        double oStdAmt, Dipsamt, ordAmt, balAmt;
            //        oStdAmt = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("sum(dues)", "")) ? 0.00 : dt3.Compute("sum(dues)", "")));
            //        ordAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamount)", "")) ? 0.00 : dt.Compute("sum(tamount)", "")));
            //        Dipsamt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(paidamt)", "")) ? 0.00 : dt2.Compute("sum(paidamt)", "")));

            //        balAmt = (oStdAmt + ordAmt) - Dipsamt;
            //        //if (qType == "All")
            //        //{
            //        TextObject txtOutStdBal = rptSOrder.ReportDefinition.ReportObjects["txtOutStdBal"] as TextObject;
            //        txtOutStdBal.Text = oStdAmt.ToString("#,##0.00;(#,##0.00);");

            //        TextObject txtDipositeAmt = rptSOrder.ReportDefinition.ReportObjects["txtDipositeAmt"] as TextObject;
            //        txtDipositeAmt.Text = Dipsamt.ToString("#,##0.00;(#,##0.00);");

            //        TextObject txtOrderAmt = rptSOrder.ReportDefinition.ReportObjects["txtOrderAmt"] as TextObject;
            //        txtOrderAmt.Text = ordAmt.ToString("#,##0.00;(#,##0.00);");

            //        TextObject txtBalanceAmt = rptSOrder.ReportDefinition.ReportObjects["txtBalanceAmt"] as TextObject;
            //        txtBalanceAmt.Text = balAmt.ToString("#,##0.00;(#,##0.00);");
            //        //}

            //        TextObject txtAppby = rptSOrder.ReportDefinition.ReportObjects["txtAppby"] as TextObject;
            //        txtAppby.Text = ds2.Tables[2].Rows[0]["appby"].ToString().Trim();

            //        TextObject txtPreBy = rptSOrder.ReportDefinition.ReportObjects["txtPreBy"] as TextObject;
            //        txtPreBy.Text = ds2.Tables[0].Rows[0]["usrname"].ToString().Trim();

            //        TextObject txtuserinfo = rptSOrder.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //        txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //        rptSOrder.SetDataSource(ds2.Tables[0]);

            //        // rptSOrder.Subreports["RptSaleOrderPaymentInfo.rpt"].SetDataSource((DataTable)ViewState["tblpaysch"]);

            //        // rptSOrder.Subreports["RptSaleOrderPaymentInfo.rpt"].SetDataSource(ds2.Tables[1]);


            //        string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //        rptSOrder.SetParameterValue("ComLogo", ComLogo);

            //        Session["Report1"] = rptSOrder;

            //        this.lblprintstkl.Text = @"<script>window.open('../RptViewer?PrintOpt=" +
            //                     "PDF" + "', target='_blank');</script>";


            //        Common.LogStatus("Order", "Order Print", "Order No: ", orderno + " - " + centrid);

            //    }
            //    catch (Exception ex)
            //    {

            //    }

        }

        protected void lnkbtnView_Click(object sender, EventArgs e)
        {
            //    int rbtIndex = Convert.ToInt16(this.RadioButtonList1.SelectedIndex);
            //    this.RadioButtonList1.Items[rbtIndex].Attributes["style"] = "background: #189697;  display:block; -webkit-border-radius:30px;-moz-border-radius: 30px;border-radius: 30px;";
            //    string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //    string centrid = ASTUtility.Left(code, 12);
            //    string Delorderno = ASTUtility.Right(code, 14);

            //    if (Delorderno.Length == 0)
            //        return;
            //    try
            //    {
            //        string comcod = this.GetCompCode();
            //        Hashtable hst = (Hashtable)Session["tblLogin"];
            //        string comnam = hst["comnam"].ToString();
            //        string compname = hst["compname"].ToString();
            //        string comadd = hst["comadd1"].ToString();
            //        string username = hst["username"].ToString();
            //        string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //        DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_CHALLAN", "PRINTDELIVERYCHALLAN", Delorderno, centrid, "", "", "", "", "", "", "");

            //        double Qty = Convert.ToDouble(ds.Tables[0].Rows[0]["delqty"]);
            //        //double Vat = Convert.ToDouble((Convert.IsDBNull(ds.Tables[0].Compute("sum(vat)", "")) ? 0.00 : ds.Tables[0].Compute("sum(vat)", "")));

            //        ReportDocument rptChallan = new MFGRPT.R_23_SaM.RptDelChallanZelta();

            //        TextObject txtCompAdd = rptChallan.ReportDefinition.ReportObjects["txtCompAdd"] as TextObject;
            //        txtCompAdd.Text = comnam + "\n" + "Corporate Office" + "\n" + comadd;
            //        TextObject txtrptHeader = rptChallan.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //        txtrptHeader.Text = "DELIVERY CHALLAN";

            //        TextObject txtDelNo = rptChallan.ReportDefinition.ReportObjects["txtDelNo"] as TextObject;
            //        txtDelNo.Text = Delorderno;// "DO" + sdelno.Substring(3);
            //        TextObject txtChallan = rptChallan.ReportDefinition.ReportObjects["txtChallan"] as TextObject;
            //        txtChallan.Text = ds.Tables[1].Rows[0]["orderno"].ToString();
            //        TextObject txtDate = rptChallan.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //        txtDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["memodat"]).ToString("dd-MMM-yyyy");
            //        //TextObject txtOrder = rptChallan.ReportDefinition.ReportObjects["txtOrder"] as TextObject;
            //        //txtOrder.Text = (ds.Tables[2].Rows[0]["orderno"].ToString() == "00000000000000") ? "CURRENT SALES" :
            //        //    ASTUtility.Left(ds.Tables[2].Rows[0]["orderno"].ToString(), 2) + ds.Tables[2].Rows[0]["orderno"].ToString().Substring(7, 2) + "-" + ASTUtility.Right(ds.Tables[2].Rows[0]["orderno"].ToString(), 5); ;

            //        TextObject txtCust = rptChallan.ReportDefinition.ReportObjects["txtCust"] as TextObject;
            //        txtCust.Text = ds.Tables[1].Rows[0]["custname"].ToString();
            //        TextObject txtCustadd = rptChallan.ReportDefinition.ReportObjects["txtAdd"] as TextObject;
            //        txtCustadd.Text = ds.Tables[1].Rows[0]["custadd"].ToString();
            //        TextObject txtPhone = rptChallan.ReportDefinition.ReportObjects["txtPhone"] as TextObject;
            //        txtPhone.Text = ds.Tables[1].Rows[0]["custphone"].ToString();

            //        TextObject txtBag = rptChallan.ReportDefinition.ReportObjects["txtBag"] as TextObject;
            //        txtBag.Text = Convert.ToDouble(ds.Tables[1].Rows[0]["bagqty"]).ToString("#,##0;(#,##0);");

            //        TextObject txtSsirdesc = rptChallan.ReportDefinition.ReportObjects["txtSsirdesc"] as TextObject;
            //        txtSsirdesc.Text = ds.Tables[1].Rows[0]["ssirdesc"].ToString();

            //        TextObject txtRemarks = rptChallan.ReportDefinition.ReportObjects["txtRemarks"] as TextObject;
            //        txtRemarks.Text = ds.Tables[1].Rows[0]["narr"].ToString();

            //        TextObject txtOrdTime = rptChallan.ReportDefinition.ReportObjects["txtDelTime"] as TextObject;
            //        txtOrdTime.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["posteddat"].ToString()).ToString("hh:mm:ss tt");
            //        TextObject txtuserinfo = rptChallan.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //        txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //        rptChallan.SetDataSource(ds.Tables[0]);
            //        TextObject txtPreBy = rptChallan.ReportDefinition.ReportObjects["txtPreBy"] as TextObject;
            //        txtPreBy.Text = ds.Tables[1].Rows[0]["username"].ToString();

            //        TextObject txtDesBy = rptChallan.ReportDefinition.ReportObjects["txtDesBy"] as TextObject;
            //        txtDesBy.Text = ds.Tables[1].Rows[0]["apusername"].ToString();

            //        string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //        rptChallan.SetParameterValue("ComLogo", ComLogo);

            //        Session["Report1"] = rptChallan;
            //        this.lblprintstkl.Text = @"<script>window.open('../RptViewer?PrintOpt=" +
            //                     "PDF" + "', target='_blank');</script>";
            //        //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer?PrintOpt=" +
            //        //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //        ///
            //        if (ConstantInfo.LogStatus == true)
            //        {
            //            string eventtype = "Delivery ORDER";
            //            string eventdesc = "Print Report";
            //            string eventdesc2 = "Del No : " + Delorderno;
            //            bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //        }


            //    }
            //    catch (Exception ex)
            //    {

            //    }
        }


        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
            //    return;
            //}
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();

            //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //string centrid = ASTUtility.Left(code, 12);
            //string orderno = ASTUtility.Right(code, 14);

            ////if (RDsostatus != "Approved")
            ////    return;

            //bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALES_ORDER_APPROVAL", "ORDERLASTAPPDELETE", centrid, orderno, "", "", "");

            //if (result == true)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Reverse Successfully');", true);
            //}
            //Common.LogStatus("Sales Interface", "Order Reverse", "Order No: ", orderno + " - " + centrid);
        }
        protected void btnDelRev_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
            //    return;
            //}
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string usrid = hst["usrid"].ToString();

            //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //string centrid = ASTUtility.Left(code, 12);
            //string orderno = ASTUtility.Right(code, 14);
            //if (orderno.Length == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            //    return;
            //}

            //DataSet ds = accData.GetTransInfo(comcod, "SP_ENTRY_SALES_ORDER_APPROVAL", "CHKFINALAPP", usrid, centrid, "", "", "");
            //if (ds.Tables[0].Rows.Count == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You Have no Permission');", true);
            //    return;
            //}

            //bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALES_ORDER_APPROVAL", "ORDERAPPDELETE", centrid, orderno, "", "", "");

            //if (result == true)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Reverse Successfully');", true);
            //}
            //Common.LogStatus("Sales Interface", "Order Reverse", "Order No: ", orderno + " - " + centrid);
        }
        protected void btnDelDO_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
            //    return;
            //}
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string usrid = hst["usrid"].ToString();

            //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //string centrid = ASTUtility.Left(code, 12);
            //string Delorderno = ASTUtility.Right(code, 14);
            //if (Delorderno.Length == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            //    return;
            //}


            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_SALES_ORDER_02", "SHOWDELIVERYORDER", centrid, Delorderno);

            //DataSet ds = lst.GetDataSetForXmlDo(ds1, centrid, Delorderno);

            //bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds, null, null, centrid, Delorderno);

            //if (!resulta)
            //{

            //    return;
            //}
            //bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALES_ORDER_02", "DELETEDOLIST", centrid, Delorderno, "", "", "");
            //if (result == true)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('DO Delete Successfully');", true);
            //}
            //Common.LogStatus("Sales Interface", "DO Delete", "DO No: ", Delorderno + " - " + centrid);
        }
        protected void txtTrack_TextChanged(object sender, EventArgs e)
        {
            this.TrackingHistory_Modal();
        }

        private void TrackingHistory_Modal()
        {
            string comcod = this.GetCompCode();

            string date = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string Mrfno = this.txtTrack.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETREQNO", "000000000000", date, Mrfno, "", "", "", "", "", "");

            string reqno = ds1.Tables[0].Rows[0]["reqno"].ToString();

            DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURCHASETRACK01", reqno, "", "", "", "", "", "", "", "");
            if (ds == null)
            {
                //this.pnlErrorMsg.Visible = true;
                return;
            }
            else
            {
                //this.PnlProdInor.Visible = true;

            }
            //DataTable dt = HiddenSameData(ds.Tables[0]);

            this.gvPurstk01.DataSource = (ds.Tables[0]);
            this.gvPurstk01.DataBind();
            this.lblMIMEInfo.Focus();
            this.txtTrack.Text = "";

        }



        protected void btnDelReq_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";

            string url = "PurReqEntry?InputType=Entry";


            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvReqChk.Rows[RowIndex].FindControl("lblgvreqnorq")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");



            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", genno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertReq(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEREQINFO", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");




            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);

        }

        protected void btnDelReqCheck_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string url = "PurReqEntry?InputType=ReqCheck";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvRatePro.Rows[RowIndex].FindControl("lblgvreqnocheck")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");



            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", genno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertReq(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEREQCHECK", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");




            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);

        }

        protected void btnDelReqRateApp_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string url = "PurReqApproval?Type=RateInput";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvRateApp.Rows[RowIndex].FindControl("lblgvreqnorapp")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");



            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", genno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertReq(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEREQRATEAPP", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");




            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);

        }




        protected void btnDelOrder_Click(object sender, EventArgs e)
        {
            string url = "PurWrkOrderEntry?InputType=OrderEntry";
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.grvMRec.Rows[RowIndex].FindControl("lblgvorderno")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURORDERINFO", genno, "",
                         //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertOrder(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEORDERNOAAPPROVED", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");




            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
            //int rowindex = (this.gvPurchase.PageSize) * (this.gvPurchase.PageIndex) + RowIndex;


            //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //string centrid = ASTUtility.Left(code, 12);
            //string orderno = ASTUtility.Right(code, 14);
            //if (orderno.Length == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            //}

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();

            //DataSet ds1 = accData.GetTransInfo(comcod, "dbo_sales.SP_REPORT_SALES_INFO", "DELORDERSHOW", centrid, orderno);

            //DataSet ds = lst.GetDataSetForXml(ds1, centrid, orderno);

            //bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds, null, null, centrid, orderno);

            //if (!resulta)
            //{

            //    return;
            //}

            //bool result = accData.UpdateTransInfo(comcod, "dbo_Sales.SP_REPORT_SALES_INFO", "ORDERDELETE", centrid, orderno, "", "", "");

            //if (result == true)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Delete Successfully');", true);
            //}
            //Common.LogStatus("Sales Interface", "Order Delete", "Order No: ", orderno + " - " + centrid);
        }

        protected void btnDelBill_Click(object sender, EventArgs e)
        {

            string url = "PurMRREntry?Type=Entry";
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvPurBill.Rows[RowIndex].FindControl("lblgvmrrno")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURMRRINFO", genno, "",
                         //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            bool result = this.XmlDataInsert(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELPURMRRINFO", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
            //int rowindex = (this.gvPurchase.PageSize) * (this.gvPurchase.PageIndex) + RowIndex;


            //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //string centrid = ASTUtility.Left(code, 12);
            //string orderno = ASTUtility.Right(code, 14);
            //if (orderno.Length == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            //}

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();

            //DataSet ds1 = accData.GetTransInfo(comcod, "dbo_sales.SP_REPORT_SALES_INFO", "DELORDERSHOW", centrid, orderno);

            //DataSet ds = lst.GetDataSetForXml(ds1, centrid, orderno);

            //bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds, null, null, centrid, orderno);

            //if (!resulta)
            //{

            //    return;
            //}

            //bool result = accData.UpdateTransInfo(comcod, "dbo_Sales.SP_REPORT_SALES_INFO", "ORDERDELETE", centrid, orderno, "", "", "");

            //if (result == true)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Delete Successfully');", true);
            //}
            //Common.LogStatus("Sales Interface", "Order Delete", "Order No: ", orderno + " - " + centrid);
        }



        private bool XmlDataInsertReq(string geno, DataSet ds)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("delbyid", typeof(string));
            dt1.Columns.Add("delseson", typeof(string));
            dt1.Columns.Add("deltrmnid", typeof(string));
            dt1.Columns.Add("deldate", typeof(DateTime));

            DataRow dr1 = dt1.NewRow();
            dr1["delbyid"] = usrid;
            dr1["delseson"] = session;
            dr1["deltrmnid"] = trmnid;
            dr1["deldate"] = Date;
            dt1.Rows.Add(dr1);
            dt1.TableName = "tbl1";

            ds1.Merge(dt1);
            ds1.Merge(ds.Tables[0]);
            ds1.Merge(ds.Tables[1]);
            ds1.Merge(ds.Tables[2]);

            ds1.Tables[0].TableName = "tbl1";
            ds1.Tables[1].TableName = "tbl2";
            ds1.Tables[2].TableName = "tbl3";
            ds1.Tables[3].TableName = "tbl4";


            bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, geno);

            if (!resulta)
            {

                return false;
            }


            return true;


        }
        private bool XmlDataInsertOrder(string geno, DataSet ds)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("delbyid", typeof(string));
            dt1.Columns.Add("delseson", typeof(string));
            dt1.Columns.Add("deltrmnid", typeof(string));
            dt1.Columns.Add("deldate", typeof(DateTime));

            DataRow dr1 = dt1.NewRow();
            dr1["delbyid"] = usrid;
            dr1["delseson"] = session;
            dr1["deltrmnid"] = trmnid;
            dr1["deldate"] = Date;
            dt1.Rows.Add(dr1);
            dt1.TableName = "tbl1";

            ds1.Merge(dt1);
            ds1.Merge(ds.Tables[0]);
            ds1.Merge(ds.Tables[1]);
            ds1.Merge(ds.Tables[2]);
            ds1.Merge(ds.Tables[3]);
            ds1.Tables[0].TableName = "tbl1";
            ds1.Tables[1].TableName = "tbl2";
            ds1.Tables[2].TableName = "tbl3";
            ds1.Tables[3].TableName = "tbl4";
            ds1.Tables[4].TableName = "tbl5";

            bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, geno);

            if (!resulta)
            {

                return false;
            }


            return true;


        }

        private bool XmlDataInsert(string Reqno, DataSet ds)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("delbyid", typeof(string));
            dt1.Columns.Add("delseson", typeof(string));
            dt1.Columns.Add("deltrmnid", typeof(string));
            dt1.Columns.Add("deldate", typeof(DateTime));

            DataRow dr1 = dt1.NewRow();
            dr1["delbyid"] = usrid;
            dr1["delseson"] = session;
            dr1["deltrmnid"] = trmnid;
            dr1["deldate"] = Date;
            dt1.Rows.Add(dr1);
            dt1.TableName = "tbl1";

            ds1.Merge(dt1);
            ds1.Merge(ds.Tables[0]);
            ds1.Merge(ds.Tables[1]);
            ds1.Tables[0].TableName = "tbl1";
            ds1.Tables[1].TableName = "tbl2";
            ds1.Tables[2].TableName = "tbl3";

            bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, Reqno);

            if (!resulta)
            {

                return false;
            }


            return true;
        }









        //private DataTable HiddenSameData(DataTable dt1)
        //{
        //    if (dt1.Rows.Count == 0)
        //    {
        //        return dt1;
        //    }


        //    string grp = dt1.Rows[0]["grp"].ToString();

        //    for (int j = 1; j < dt1.Rows.Count; j++)
        //    {
        //        if (dt1.Rows[j]["grp"].ToString() == grp)
        //        {
        //            grp = dt1.Rows[j]["grp"].ToString();
        //            dt1.Rows[j]["grpdesc"] = "";

        //        }

        //        else
        //        {
        //            grp = dt1.Rows[j]["grp"].ToString();
        //        }

        //    }


        //    return dt1;

        //}

    }
}