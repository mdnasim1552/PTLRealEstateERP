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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
using System.Net;
using System.Net.Mail;
using EASendMail;
using System.IO;

namespace RealERPWEB.F_99_Allinterface
{
    public partial class MKTProInterface : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                if (dr1.Length==0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = Convert.ToDateTime(date).AddYears(-2).ToString("dd-MMM-yyyy");
                this.txttoDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtmrfno.Attributes.Add("placeholder", ReadCookie());

               
                this.PurchaseInfoRpt();
                this.RadioButtonList1.SelectedIndex = 0;
                this.RadioButtonList1_SelectedIndexChanged(null, null);



            }
        }
     
        private string ReadCookie()
        {
            HttpCookie nameCookie = Request.Cookies["MRF"];
            string refno = nameCookie != null ? nameCookie.Value.Split('=')[1] : "Mrf No";
            return refno;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            lbtnOk_Click(null, null);
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            this.Timer1.Interval = 3600000;
        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            comcod = qcomcod.Length > 0 ? qcomcod : comcod;
            return comcod;
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
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
            Session.Remove("Alltable");         
            string comcod = this.GetCompCode();

            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");           
            string mrfno = "%" + this.txtmrfno.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_MKTPROCURE_INTERFACE", "RPTPURCHASEDASHBOARD", frmdate,   todate, mrfno, "", "");

            Session["Alltable"] = ds1;

            if (ds1 == null)
                return;


            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading green counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["reqst"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content green'><div class='circle-tile-description text-faded'>Status</div></div></div>";

            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["reqchk"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>" + "HOD Approval." + "</div></div></div>";

            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["reqapp"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>DIV App.</div></div></div>";

            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["cscreate"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>CS Preparation</div></div></div>";

            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["csapp"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>CS Approved</div></div></div>";

            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading deep-sky-blue counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["ordr"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content deep-sky-blue'><div class='circle-tile-description text-faded'>Purchase Order</div></div></div>";

            this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading lime counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["ordfapp"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content lime'><div class='circle-tile-description text-faded'>Ord. 1st App</div></div></div>";


            this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading chocolate counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["ordfinapp"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content chocolate'><div class='circle-tile-description text-faded'>Ord. final App</div></div></div>";

            this.RadioButtonList1.Items[8].Text = "<div class='circle-tile'><a><div class='circle-tile-heading deep-pink counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["mrr"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content deep-pink'><div class='circle-tile-description text-faded'>Received</div></div></div>";


            this.RadioButtonList1.Items[9].Text = "<div class='circle-tile'><a><div class='circle-tile-heading blue-violet counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["reqcom"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content blue-violet'><div class='circle-tile-description text-faded'>Completed</div></div></div>";



            ds1.Dispose();

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            DataSet ds1 = (DataSet)Session["Alltable"];
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataView dv = new DataView();
          
            switch (value)
            {
                //Status (All Reqinfo )                
                case "0":
                    dt = ((DataTable)ds1.Tables[0]).Copy();
                    dv = dt.DefaultView;
                    dt1 = dv.ToTable();
                    this.Data_Bind("gvReqInfo", dt1);

                    this.pnlReqStatus.Visible = true;
                    this.pnlReqChq.Visible = false;                    
                    this.pnlFinalApp.Visible = false;
                    this.pnlcsprepared.Visible = false;
                    this.pnlcsapproved.Visible = false;
                    this.pnlWorkOrder.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;                    
                    this.pnlMatRec.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";

                    if (dt1.Rows.Count > 0)
                    {
                        ((TextBox)this.gvReqInfo.HeaderRow.FindControl("txtSearchrefnum")).Attributes.Add("placeholder", ReadCookie());
                    }

                    break;

                //Checked
                case "1":
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("cstatus = 'Checked' ");
                    this.Data_Bind("gvReqChk", dv.ToTable());

                    this.pnlReqStatus.Visible = false;
                    this.pnlReqChq.Visible = true;                   
                    this.pnlFinalApp.Visible = false;
                    this.pnlcsprepared.Visible = false;
                    this.pnlcsapproved.Visible = false;
                    this.pnlWorkOrder.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.pnlMatRec.Visible = false;
                    this.pnlComplete.Visible = false;
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;

                //Final Approval
                case "2":
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("cstatus = 'DIV Approval' ");
                    dt1 = dv.ToTable();
                    this.Data_Bind("gvReqApp", dt1);

                    this.pnlReqStatus.Visible = false;
                    this.pnlReqChq.Visible = false;                    
                    this.pnlFinalApp.Visible = true;
                    this.pnlcsprepared.Visible = false;
                    this.pnlcsapproved.Visible = false;
                    this.pnlWorkOrder.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.pnlMatRec.Visible = false;
                    this.pnlComplete.Visible = false;
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;

                //CS Preparation
                case "3":
                    dt = ((DataTable)ds1.Tables[2]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("cstatus = 'CS Preparation'");
                    dt1 = dv.ToTable();
                    this.Data_Bind("gvcsprepared", dt1);

                    this.pnlReqStatus.Visible = false;
                    this.pnlReqChq.Visible = false;
                    this.pnlFinalApp.Visible = false;
                    this.pnlcsprepared.Visible = true;
                    this.pnlcsapproved.Visible = false;
                    this.pnlWorkOrder.Visible = false;
                   
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.pnlMatRec.Visible = false;
                    this.pnlComplete.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    break;

                //CS Approved
                case "4":
                    dt = ((DataTable)ds1.Tables[2]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("cstatus = 'CS Approved' ");
                    dt1 = dv.ToTable();
                    this.Data_Bind("gvcsapproved", dt1);

                    this.pnlReqStatus.Visible = false;
                    this.pnlReqChq.Visible = false;
                    this.pnlFinalApp.Visible = false;
                    this.pnlcsprepared.Visible = false;
                    this.pnlcsapproved.Visible = true;
                    this.pnlWorkOrder.Visible = false;

                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.pnlMatRec.Visible = false;
                    this.pnlComplete.Visible = false;
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    break;

                //Work Order
                case "5":
                    dt = (DataTable)ds1.Tables[3];
                    this.Data_Bind("gvWrkOrd", dt);

                    this.pnlReqStatus.Visible = false;
                    this.pnlReqChq.Visible = false;                  
                    this.pnlFinalApp.Visible = false;
                    this.pnlcsprepared.Visible = false;
                    this.pnlcsapproved.Visible = false;
                    this.pnlWorkOrder.Visible = true;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.pnlMatRec.Visible = false;
                    this.pnlComplete.Visible = false;
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    if (dt.Rows.Count > 0)
                    {
                        ((TextBox)this.gvWrkOrd.HeaderRow.FindControl("txtSearchrefnumporder")).Attributes.Add("placeholder", ReadCookie());
                    }
                    break;


                 
                //Work Order(1st Approval)
                case "6":

                    dt = (DataTable)ds1.Tables[4];
                    dv = dt.DefaultView;
                    dv.RowFilter = ("cstatus='Order (1st Approval)'  ");
                    this.Data_Bind("gvordfapp", dv.ToTable());  
                    this.pnlReqStatus.Visible = false;
                    this.pnlReqChq.Visible = false;
                    this.pnlFinalApp.Visible = false;
                    this.pnlcsprepared.Visible = false;
                    this.pnlcsapproved.Visible = false;
                    this.pnlWorkOrder.Visible = false;
                    this.pnlorderfapp.Visible = true;
                    this.pnlordersapp.Visible = false;
                    this.pnlMatRec.Visible = false;
                    this.pnlComplete.Visible = false;
                    this.RadioButtonList1.Items[6].Attributes["class"] = "lblactive blink_me";
                    
                    break;


                //Work Order(Final Approval)
                case "7":
                    dt = (DataTable)ds1.Tables[4];
                    dv = dt.DefaultView;
                    dv.RowFilter = ("cstatus='Order (Final Approval)'  ");
                    this.Data_Bind("gvordsapp", dv.ToTable());
                    this.pnlReqStatus.Visible = false;
                    this.pnlReqChq.Visible = false;
                    this.pnlFinalApp.Visible = false;
                    this.pnlcsprepared.Visible = false;
                    this.pnlcsapproved.Visible = false;
                    this.pnlWorkOrder.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = true;
                    this.pnlMatRec.Visible = false;
                    this.pnlComplete.Visible = false;
                    this.RadioButtonList1.Items[7].Attributes["class"] = "lblactive blink_me";
                    
                    break;

                //MRR
                case "8":
                    dt = (DataTable)ds1.Tables[5];
                    this.Data_Bind("grvMRec", dt);

                    this.pnlReqStatus.Visible = false;
                    this.pnlReqChq.Visible = false;                    
                    this.pnlFinalApp.Visible = false;
                    this.pnlcsprepared.Visible = false;
                    this.pnlcsapproved.Visible = false;
                    this.pnlWorkOrder.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.pnlMatRec.Visible = true;
                    this.pnlComplete.Visible = false;
                    this.RadioButtonList1.Items[8].Attributes["class"] = "lblactive blink_me";
                    if (dt.Rows.Count > 0)
                    {
                        ((TextBox)this.grvMRec.HeaderRow.FindControl("txtSearchrefnummrec")).Attributes.Add("placeholder", ReadCookie());
                    }
                    break;


                //MRR
                case "9":
                    dt = (DataTable)ds1.Tables[6];
                    this.Data_Bind("gvPurcom", dt);

                    this.pnlReqStatus.Visible = false;
                    this.pnlReqChq.Visible = false;
                    this.pnlFinalApp.Visible = false;
                    this.pnlcsprepared.Visible = false;
                    this.pnlcsapproved.Visible = false;
                    this.pnlWorkOrder.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.pnlMatRec.Visible = false;
                    this.pnlComplete.Visible = true;
                    this.RadioButtonList1.Items[9].Attributes["class"] = "lblactive blink_me";
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
                string comcod1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();



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
                if (cstatus == "Requisition Approval")
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



                hlnkgvgvmrfno.NavigateUrl = "~/F_28_MPro/RptMktPurchaseTracking?Type=PurchaseTrk&reqno=" + reqno + "&comcod=" + comcod1;


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
                string reqdat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "reqdat1")).ToString("dd-MMM-yyyy");  
                hlink2.NavigateUrl = "~/F_28_MPro/MKTPurReqEntry?InputType=ReqCheck&prjcode=" + pactcode + "&genno=" + reqno;
                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=MktReqPrint&reqno=" + reqno + "&reqdat=" + reqdat;


            }
        }
 
        protected void gvReqApp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string reqdat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "reqdat1")).ToString("dd-MMM-yyyy");
                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=MktReqPrint&reqno=" + reqno + "&reqdat=" + reqdat;
                hlink2.NavigateUrl = "~/F_28_MPro/MKTPurReqEntry?InputType=ReqApproval&prjcode=" + pactcode + "&genno=" + reqno;
                

            }
        }

        protected void gvcsprepared_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrintcsp");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("hlnkbtnEntrycsp");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string reqdat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "reqdat1")).ToString("dd-MMM-yyyy");
                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=MktReqPrint&reqno=" + reqno + "&reqdat=" + reqdat;
                hlink2.NavigateUrl = "~/F_28_MPro/MktMarketSurvey?Type=Entry&genno=" + reqno;


            }
        }

        protected void gvcsapproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrintcsap");               
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("hlnkbtnEntrycsap");
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=MktCSPrint&reqno=" + reqno;
                hlink2.NavigateUrl = "~/F_28_MPro/MktMarketSurvey?Type=Approval&genno=" + reqno;


            }
        }

        protected void btnDelReqApp_Click(object sender, EventArgs e)
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
            string genno = ((Label)this.gvReqApp.Rows[RowIndex].FindControl("lblgvreqnorapp")).Text.Trim();


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", genno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            bool result = this.XmlDataInsertReq(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = ("reqrat>0");
            DataTable dt = dv.ToTable();

            foreach (DataRow drd in dt.Rows)
            {

                string delreqapp = this.DelComReqApproval();
                string rsircode = drd["rsircode"].ToString();
                string spcfcod = drd["spcfcod"].ToString();
                bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEREQRATEAPP", genno, delreqapp, rsircode, spcfcod, "", "", "", "", "", "", "", "", "", "", "");




                if (!resulbill)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                    return;
                }
            }


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);

        }

        
        protected void gvWrkOrd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                string reqNo = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=MktCSPrint&reqno=" + reqNo;
                hlink2.NavigateUrl = "~/F_28_MPro/MktWorkOrderEntry?InputType=OrderEntry&genno=" + reqNo;

            }
        }


        protected void lnkbtnOrder_Click(object sender, EventArgs e)
        {


            // string []paymentid=new string[100];
            string comcod = this.GetCompCode();
            int i = 0;
            string caprovno = "", pssirocde = "";
            foreach (GridViewRow gv1 in gvWrkOrd.Rows)
            {

                string chkemerge = ((CheckBox)gv1.FindControl("chkorder")).Checked ? "True" : "False";
                string aprovno = ((Label)gv1.FindControl("lblgvaprovno")).Text;
                string ssirocde = ((Label)gv1.FindControl("lblgvssircode")).Text;
                if (chkemerge == "True")
                {
                    if (i == 0)
                    {
                        caprovno += aprovno;
                        pssirocde = ssirocde;
                        i++;

                    }
                    else
                    {
                        if (pssirocde != ssirocde)
                        {

                            ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select Same Supplier Name";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;

                        }
                        else
                        {
                            caprovno += aprovno;

                        }


                        pssirocde = ssirocde;



                    }

                    // paymentid[i++] = slnum;
                }
            }

            string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_14_Pro/";
            string currentptah = "PurWrkOrderEntry?InputType=OrderEntry&genno=" + caprovno;
            string totalpath = hostname + currentptah;
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "FunPurchaseOrder('" + totalpath + "');", true);
        }

        protected void gvordfapp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntryofapp");
                HyperLink hlnkPrintofapp = (HyperLink)e.Row.FindControl("HyInprPrintofapp");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();


                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_28_MPro/MktWorkOrderEntry?InputType=FirstApp&genno=" + orderno;
                hlnkPrintofapp.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=MktOrderPrint&orderno=" + orderno;

            }

        }
        protected void gvordsapp_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntryosapp");
                HyperLink hlnkPrintosapp = (HyperLink)e.Row.FindControl("HyInprPrintosapp");
                HyperLink hlnkPrintosappReq = (HyperLink)e.Row.FindControl("HyInprPrintosappReq");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string reqdat = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_28_MPro/MktWorkOrderEntry?InputType=SecondApp&genno=" + orderno;
                hlnkPrintosapp.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=MktOrderPrint&orderno=" + orderno;
                hlnkPrintosappReq.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ReqPrint&reqno=" + reqno + "&reqdat=" + reqdat;


            }
        }


        protected void btnofapp_Click(object sender, EventArgs e)
        {

            string url = "PurWrkOrderEntry?InputType=FirstApp";
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvordfapp.Rows[RowIndex].FindControl("lblgvordernoofapp")).Text.Trim();


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



            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEORDERFIRSTAPP", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");




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
        protected void btnosapp_Click(object sender, EventArgs e)
        {


            string url = "PurWrkOrderEntry?InputType=SecondApp";
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvordsapp.Rows[RowIndex].FindControl("lblgvordernosapp")).Text.Trim();


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



            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEORDERSECONDAPP", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");




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

        protected void btnDelOrderAprv_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";

            string url = "PurWrkOrderEntry?InputType=FirstApp";

            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string orderno = ((Label)this.gvordfapp.Rows[RowIndex].FindControl("lblgvordernoofapp")).Text.Trim();
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");

            if (orderno == "")
                return;

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURORDERINFO", orderno, date, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertOrder(orderno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEORDERAPPROVAL", orderno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

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

        protected void grvMRec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                HyperLink hlink3 = (HyperLink)e.Row.FindControl("HyperLink2");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString();

                hlink3.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=MktOrderPrint&orderno=" + orderno;              
                hlink2.NavigateUrl = "~/F_28_MPro/MktMRREntry?Type=Entry&prjcode=" + pactcode + "&genno=" + orderno + "&sircode=" + sircode;
               
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
            string comcod = this.GetCompCode();

            switch (comcod)
            {
                // case "3101":
                case "3340":
                    break;
                default:
                    int i = 0;
                    string pactcode = dt1.Rows[0]["pactcode"].ToString();
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        if (i == 0)
                        {


                            pactcode = dr1["pactcode"].ToString();
                            i++;
                            continue;
                        }

                        if (dr1["pactcode"].ToString() == pactcode)
                        {

                            dr1["pactdesc"] = "";

                        }


                        pactcode = dr1["pactcode"].ToString();
                    }


                    break;
            }



            return dt1;
        }
        private void Data_Bind(string gv, DataTable dt)
        {
            try
            {
                string comcod = this.GetCompCode();

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

                    case "gvReqApp":
                        this.gvReqApp.DataSource = HiddenSameData(dt);
                        this.gvReqApp.DataBind();
                        break;


                    case "gvcsprepared":
                        this.gvcsprepared.DataSource = HiddenSameData(dt);
                        this.gvcsprepared.DataBind();
                        break;



                    case "gvcsapproved":
                        this.gvcsapproved.DataSource = HiddenSameData(dt);
                        this.gvcsapproved.DataBind();
                        break;





                    case "gvWrkOrd":
                        this.gvWrkOrd.DataSource = HiddenSameData(dt);
                        this.gvWrkOrd.DataBind();
                        break;

                    case "gvordfapp":
                        this.gvordfapp.DataSource = HiddenSameData(dt);
                        this.gvordfapp.DataBind();
                        break;

                    case "gvordsapp":
                        this.gvordsapp.DataSource = HiddenSameData(dt);
                        this.gvordsapp.DataBind();
                        break;


                    case "grvMRec":
                        this.grvMRec.DataSource = HiddenSameData(dt);
                        this.grvMRec.DataBind();
                        break;

                    case "gvPurcom":
                        this.gvPurcom.DataSource = HiddenSameData(dt);
                        this.gvPurcom.DataBind();
                        break;



                        
                }

                this.FooterCalculation(gv, dt);
            }

            catch (Exception ex)
            { 
            
             
            
            }

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

                case "gvWrkOrd":
                    ((Label)this.gvWrkOrd.FooterRow.FindControl("lblgvFWoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(apamt)", "")) ?
                    0 : dt.Compute("sum(apamt)", ""))).ToString("#,##0;(#,##0);");


                    break;

                case "grvMRec":
                    ((Label)this.grvMRec.FooterRow.FindControl("lblgvFWoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recvamt)", "")) ?
                    0 : dt.Compute("sum(recvamt)", ""))).ToString("#,##0;(#,##0);");

                    ((Label)this.grvMRec.FooterRow.FindControl("lblgvFreceivedamtor")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrramt)", "")) ?
                  0 : dt.Compute("sum(mrramt)", ""))).ToString("#,##0;(#,##0);");
                    ((Label)this.grvMRec.FooterRow.FindControl("lblgvFbalamtor")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balamt)", "")) ?
                  0 : dt.Compute("sum(balamt)", ""))).ToString("#,##0;(#,##0);");


                    break;

                case "grvComp":
                    break;
            }
        }



        protected void lnkbtnPrintRD_Click(object sender, EventArgs e)
        {
        }

        protected void lnkbtnView_Click(object sender, EventArgs e)
        {
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

            /**/
            if (ds1.Tables[5].Rows.Count > 0)
            {
                bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEREQCRMBACKDATA", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                if (!resulbill)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Back  Fail');", true);
                    return;
                }
                else
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Removed Successfully.');", true);
            }

            else
            {
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
            }


            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);

        }

        private string DelComReqApproval()
        {


            string comcod = this.GetCompCode();
            string delreqapp = "";

            switch (comcod)
            {


                //case "3101":
                case "1103":
                    delreqapp = "DelFSTRECCOM";
                    break;

                default:
                    break;

            }

            return delreqapp;

        }
        protected void btnDelAprovedNo_Click(object sender, EventArgs e)
        {
            string url = "PurAprovEntry?InputType=PurProposal";
            string comcod = this.GetCompCode();

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvWrkOrd.Rows[RowIndex].FindControl("lblgvaprovno")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURAPROVINFO", genno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;


            bool result = this.XmlDataInsert(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEAPROVINFO", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");




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
            string toorder = Convert.ToDouble(((Label)this.grvMRec.Rows[RowIndex].FindControl("lblgvWoamt")).Text.Trim()).ToString();


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

            string delorder = "delsapp";
            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEORDERNOAAPPROVED", genno, delorder, toorder, "", "", "", "", "", "", "", "", "", "", "", "");




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



        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    this.PrintRequisitionStatus();
                    break;
                case "1":
                case "2":
                case "3":
                case "4":
                    this.Printcrateappanorder();
                    break;

                case "5":
                    this.PrintPurchaseOrder();
                    break;

                case "6":
                    this.PrintCashReceive();
                    break;

                case "7":
                    this.PrintBillConfirmation();
                    break;

            }



        }

        private void PrintRequisitionStatus()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            DataSet ds1 = (DataSet)Session["Alltable"];
            DataTable dt = ds1.Tables[0];


            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.RptReqSts>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_99_AllInterface.RptReqSts", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void Printcrateappanorder()
        {

            DataSet ds1 = (DataSet)Session["Alltable"];
            DataTable dt = new DataTable();
            string value = RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "1":
                    dt = ds1.Tables[1];
                    break;
                case "2":
                    dt = ds1.Tables[1];
                    break;
                case "3":
                    dt = ds1.Tables[1];
                    break;
                case "4":
                    dt = ds1.Tables[2];
                    break;

            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string title = (value == "1") ? "Requisition Chequed" : (value == "2") ? "Cash Purchase" : (value == "3") ? "Requisition Approval" : "Order Process";




            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.Rptcrateappanorder>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_99_AllInterface.Rptcrateappanorder", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("title", title));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintPurchaseOrder()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            DataSet ds1 = (DataSet)Session["Alltable"];
            DataTable dt = ds1.Tables[3];


            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.RptPurOrder>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_99_AllInterface.RptPurOrder", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintCashReceive()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            DataSet ds1 = (DataSet)Session["Alltable"];
            DataTable dt = ds1.Tables[4];


            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.RptCashRcv>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_99_AllInterface.RptCashRcv", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintBillConfirmation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            DataSet ds1 = (DataSet)Session["Alltable"];
            DataTable dt = ds1.Tables[5];


            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.RptBillCon>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_99_AllInterface.RptBillCon", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void gvreqfapproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("hlnkbtnEntryfapproved");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string comcod1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_12_Inv/PurReqEntry?InputType=ReqFirstApproved&prjcode=" + pactcode + "&genno=" + reqno + "&comcod=" + comcod1;

            }
        }

        protected void gvFRec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrintfrec");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntryfrec");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ReqPrint&reqno=" + reqno;
                hlink2.NavigateUrl = "~/F_12_Inv/PurReqApproval?Type=FirstRecom&prjcode=" + pactcode + "&genno=" + reqno;

            }
        }
        protected void gvSecRec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrintsrec");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntrysrec");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ReqPrint&reqno=" + reqno;
                hlink2.NavigateUrl = "~/F_12_Inv/PurReqApproval?Type=SecRecom&prjcode=" + pactcode + "&genno=" + reqno;

            }

        }
        protected void gvThRec_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrintthrec");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntrythrec");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ReqPrint&reqno=" + reqno;
                hlink2.NavigateUrl = "~/F_12_Inv/PurReqApproval?Type=ThirdRecom&prjcode=" + pactcode + "&genno=" + reqno;

            }
        }

      
        protected void lbtnSendMail_Click(object sender, EventArgs e)
        {

            string url = "PurWrkOrderEntry?InputType=OrderEntry";
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (dr1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }


            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.grvMRec.Rows[RowIndex].FindControl("lblgvorderno")).Text.Trim();

            this.AutoSavePDF(genno);
            bool ssl = Convert.ToBoolean(((Hashtable)Session["tblLogin"])["ssl"].ToString());


            switch (ssl)
            {
                case true:
                    this.SendSSLMail(genno);

                    break;

                case false:
                    this.SendNormalMail(genno);
                    break;

            }



        }
        private string PrintCallType()
        {


            string comcod = this.GetCompCode();
            string Calltype = "";
            switch (comcod)
            {
                case "3301":
                case "1301":
                case "3330":
                    //case "3101":
                    Calltype = "SHOWORKORDER01";
                    break;

                case "3332":
                    // case "3101":
                    Calltype = "SHOWORKORDER02";

                    break;
                default:
                    Calltype = "SHOWORKORDER01";
                    break;
            }
            return Calltype;


        }
        private string GetCompOrderCopy()
        {

            string comcod = this.GetCompCode();
            string ordernocopy = "";
            switch (comcod)
            {
                case "3330":
                    // case "3101":
                    ordernocopy = "Bridge";
                    break;
                // case "3101":
                case "3332":
                    ordernocopy = "Innstar";
                    break;
                default:
                    ordernocopy = "";
                    break;


            }
            return ordernocopy;


        }

        private void AutoSavePDF(string orderno)
        {




            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string wrkid = orderno;

                string Calltype = this.PrintCallType();
                string ordercopy = this.GetCompOrderCopy();
                DataSet _ReportDataSet = this.accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", Calltype, wrkid, ordercopy, "", "", "", "", "", "", "");


                ViewState["tblOrder"] = _ReportDataSet.Tables[0];
                DataTable dt = _ReportDataSet.Tables[0];
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("grp='A'");
                dt = dv.ToTable();


                string Para1 = _ReportDataSet.Tables[1].Rows[0]["leterdes"].ToString();
                string Orderdate = Convert.ToDateTime(_ReportDataSet.Tables[1].Rows[0]["orderdat"]).ToString("MMMM  dd, yyyy");
                string SupName = _ReportDataSet.Tables[1].Rows[0]["ssirdesc"].ToString();
                string Address = _ReportDataSet.Tables[1].Rows[0]["address"].ToString();
                string Cperson = _ReportDataSet.Tables[1].Rows[0]["cperson"].ToString();
                string Phone = _ReportDataSet.Tables[1].Rows[0]["phone"].ToString();
                string mobile = _ReportDataSet.Tables[1].Rows[0]["mobile"].ToString();

                // DataTable dtterm = _ReportDataSet.Tables[2];

                DataTable dtterm = _ReportDataSet.Tables[2];
                DataTable dtord = _ReportDataSet.Tables[4];
                DataTable dtpaycch = _ReportDataSet.Tables[5];

                // string Type = this.CompanyPrintWorkOrder();
                ReportDocument rptwork = new ReportDocument();

                string fax = _ReportDataSet.Tables[1].Rows[0]["fax"].ToString();

                string trmplace = ((comcod == "3338") ? "1. " + dtterm.Rows[0]["termssubj"].ToString() : "*" + dtterm.Rows[0]["termssubj"].ToString() + " : ");
                string place = dtterm.Rows[0]["termsdesc"].ToString().Trim();
                string trmpdate = ((comcod == "3338") ? "2. " + dtterm.Rows[1]["termssubj"].ToString() : "*" + dtterm.Rows[1]["termssubj"].ToString() + " : ");
                string pdate = dtterm.Rows[1]["termsdesc"].ToString().Trim();
                string trmcarring = ((comcod == "3338") ? "3. " + dtterm.Rows[2]["termssubj"].ToString() : "*" + dtterm.Rows[2]["termssubj"].ToString() + " : ");
                string carring = dtterm.Rows[2]["termsdesc"].ToString().Trim();
                string trmbill = (comcod == "3330") ? "" : (comcod == "3338") ? "4. " + (dtterm.Rows[3]["termssubj"]).ToString() : "*" + dtterm.Rows[3]["termssubj"].ToString() + ": ";
                string bill = (comcod == "3330") ? ("* " + dtterm.Rows[3]["termsdesc"].ToString().Trim()) : dtterm.Rows[3]["termsdesc"].ToString().Trim();
                string trmpayment = ((comcod == "3338") ? dtterm.Rows[4]["termssubj"].ToString() : "*" + dtterm.Rows[4]["termssubj"].ToString() + " : ");
                string payment = dtterm.Rows[4]["termsdesc"].ToString().Trim();

                string trmothers = ((comcod == "3338") ? dtterm.Rows[5]["termssubj"].ToString() : "*" + dtterm.Rows[5]["termssubj"].ToString() + " : ");
                string Others = dtterm.Rows[5]["termsdesc"].ToString().Trim();

                // For Acme




                //      
                string trmcperson = ((dtterm.Rows.Count == 0) ? "" : dtterm.Select("termsid='010'").Length > 0 ? "* " + (dtterm.Select("termsid='010'")[0]["termssubj"]).ToString() + " : " : "");
                string cperson = ((dtterm.Rows.Count == 0) ? "" : dtterm.Select("termsid='010'").Length > 0 ? (dtterm.Select("termsid='010'")[0]["termsdesc"]).ToString() : ""); ;



                switch (comcod)
                {


                    case "3332":
                        //case "3101":
                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderInstar();
                        TextObject rpttxtReqIns = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReqIns.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqAppIns = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqAppIns.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrdIns = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrdIns.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWordIns = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWordIns.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtcheckIns = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
                        rpttxtcheckIns.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();


                        break;


                    case "3336":
                    case "3337":


                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderSuvastu();


                        //Sign In
                        TextObject txtatt = rptwork.ReportDefinition.ReportObjects["txtatt"] as TextObject;
                        txtatt.Text = Cperson;

                        TextObject rpttxtReqSuv = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReqSuv.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqAppSuv = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqAppSuv.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrdSuv = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrdSuv.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWordSuv = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWordSuv.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtcheckSuv = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
                        rpttxtcheckSuv.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        TextObject txtAdvancedSuv = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvancedSuv.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); "); ;
                        TextObject txtappbySuv = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappbySuv.Text = "Approved By";

                        TextObject txtmoblieNumber = rptwork.ReportDefinition.ReportObjects["txtmoblieNumber"] as TextObject;
                        txtmoblieNumber.Text = mobile;

                        //txtappby.Text = (comcod == "3335") ? "Head of Procurement" : "Approved By";

                        // sign end 
                        break;


                    case "3338":
                        // case "3101":

                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderAcme();


                        //Sign In


                        TextObject rpttxtReqAcme = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReqAcme.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqAppAcme = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqAppAcme.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrdAcme = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrdAcme.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWordAcme = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWordAcme.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtcheckAcme = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;

                        TextObject txtfaprname = rptwork.ReportDefinition.ReportObjects["txtfaprname"] as TextObject;
                        txtfaprname.Text = _ReportDataSet.Tables[3].Rows[0]["faprname"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["faprdat"].ToString();

                        rpttxtcheckAcme.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        TextObject txtAdvancedAcme = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvancedAcme.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); "); ;
                        //TextObject txtappbyAcme = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        //txtappbyAcme.Text = "Approved By";

                        TextObject txtPhoneNumber = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber.Text = Phone;

                        // Section part

                        string Others7 = dtterm.Select("termsid='007'").Length > 0 ? ((dtterm.Select("termsid='007'")[0]["termsdesc"]).ToString()) : "";

                        string Others8 = dtterm.Select("termsid='008'").Length > 0 ? ((dtterm.Select("termsid='008'")[0]["termsdesc"]).ToString()) : "";

                        string Others9 = dtterm.Select("termsid='009'").Length > 0 ? ((dtterm.Select("termsid='009'")[0]["termsdesc"]).ToString()) : "";
                        string Others11 = dtterm.Select("termsid='011'").Length > 0 ? ((dtterm.Select("termsid='011'")[0]["termsdesc"]).ToString()) : "";
                        string Others12 = dtterm.Select("termsid='012'").Length > 0 ? ((dtterm.Select("termsid='012'")[0]["termsdesc"]).ToString()) : "";
                        string Others13 = dtterm.Select("termsid='013'").Length > 0 ? ((dtterm.Select("termsid='013'")[0]["termsdesc"]).ToString()) : "";
                        string Others14 = dtterm.Select("termsid='014'").Length > 0 ? ((dtterm.Select("termsid='014'")[0]["termsdesc"]).ToString()) : "";
                        string Others15 = dtterm.Select("termsid='015'").Length > 0 ? ((dtterm.Select("termsid='015'")[0]["termsdesc"]).ToString()) : "";
                        string Others16 = dtterm.Select("termsid='016'").Length > 0 ? ((dtterm.Select("termsid='016'")[0]["termsdesc"]).ToString()) : "";









                        TextObject txtothers7 = rptwork.ReportDefinition.ReportObjects["others1"] as TextObject;
                        txtothers7.Text = (Others7.Length > 0) ? Others7 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection15"].SectionFormat.EnableSuppress = (Others7.Length > 0) ? false : true;

                        TextObject txtothers8 = rptwork.ReportDefinition.ReportObjects["others2"] as TextObject;
                        txtothers8.Text = (Others8.Length > 0) ? Others8 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection16"].SectionFormat.EnableSuppress = (Others8.Length > 0) ? false : true;

                        TextObject txtothers9 = rptwork.ReportDefinition.ReportObjects["others3"] as TextObject;
                        txtothers9.Text = (Others9.Length > 0) ? Others9 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection17"].SectionFormat.EnableSuppress = (Others9.Length > 0) ? false : true;

                        TextObject txtothers10 = rptwork.ReportDefinition.ReportObjects["others4"] as TextObject;
                        txtothers10.Text = (Others11.Length > 0) ? Others11 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection18"].SectionFormat.EnableSuppress = (Others11.Length > 0) ? false : true;
                        TextObject txtothers12 = rptwork.ReportDefinition.ReportObjects["others5"] as TextObject;
                        txtothers12.Text = (Others11.Length > 0) ? Others12 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection19"].SectionFormat.EnableSuppress = (Others12.Length > 0) ? false : true;
                        TextObject txtothers13 = rptwork.ReportDefinition.ReportObjects["others6"] as TextObject;
                        txtothers13.Text = (Others13.Length > 0) ? Others13 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection20"].SectionFormat.EnableSuppress = (Others13.Length > 0) ? false : true;
                        TextObject txtothers14 = rptwork.ReportDefinition.ReportObjects["others7"] as TextObject;
                        txtothers14.Text = (Others14.Length > 0) ? Others14 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection21"].SectionFormat.EnableSuppress = (Others14.Length > 0) ? false : true;
                        TextObject txtothers15 = rptwork.ReportDefinition.ReportObjects["others8"] as TextObject;
                        txtothers15.Text = (Others14.Length > 0) ? Others15 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection22"].SectionFormat.EnableSuppress = (Others15.Length > 0) ? false : true;

                        TextObject txtothers16 = rptwork.ReportDefinition.ReportObjects["others9"] as TextObject;
                        txtothers16.Text = (Others16.Length > 0) ? Others16 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection23"].SectionFormat.EnableSuppress = (Others16.Length > 0) ? false : true;

                        //TextObject txtothers10 = rptwork.ReportDefinition.ReportObjects["others10"] as TextObject;
                        //txtothers10.Text = (Others10.Length > 0) ?"*"+ Others10 : "";
                        //rptwork.ReportDefinition.Sections["GroupFooterSection24"].SectionFormat.EnableSuppress = (Others10.Length > 0) ? false : true;


                        break;

                    case "3335":
                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderEdison();


                        //Sign In


                        TextObject rpttxtReqe = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReqe.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqAppe = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqAppe.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrde = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrde.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWorde = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWorde.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtchecke = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
                        rpttxtchecke.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        TextObject txtAdvancede = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvancede.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); "); ;
                        TextObject txtappbye = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappbye.Text = _ReportDataSet.Tables[3].Rows[0]["ordappnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["ordappdat"].ToString(); ;

                        TextObject txtPhoneNumber2e = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber2e.Text = Phone;
                        break;
                    default:
                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderBridge();


                        //Sign In


                        TextObject rpttxtReq = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReq.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqApp = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqApp.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrd = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrd.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWord = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWord.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtcheck = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
                        rpttxtcheck.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        TextObject txtAdvanced = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvanced.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); "); ;
                        TextObject txtappby = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappby.Text = "Approved By";

                        TextObject txtPhoneNumber2 = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber2.Text = Phone;

                        //txtappby.Text = (comcod == "3335") ? "Head of Procurement" : "Approved By";

                        // sign end 
                        break;


                }




                TextObject txtsubject = rptwork.ReportDefinition.ReportObjects["txtsubject"] as TextObject;
                txtsubject.Text = dtord.Rows[0]["subject"].ToString();
                TextObject txtCompany = rptwork.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
                txtCompany.Text = comnam;
                TextObject txtAddress = rptwork.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
                txtAddress.Text = comadd;
                TextObject rptpurno = rptwork.ReportDefinition.ReportObjects["purno"] as TextObject;
                rptpurno.Text = dtord.Rows[0]["orderno"].ToString().Substring(0, 3) + dtord.Rows[0]["orderno"].ToString().Substring(7, 2) + "-" + ASTUtility.Right(dtord.Rows[0]["orderno"].ToString(), 5);
                TextObject rptRefno = rptwork.ReportDefinition.ReportObjects["refno"] as TextObject;
                rptRefno.Text = dtord.Rows[0]["pordref"].ToString();
                TextObject supname = rptwork.ReportDefinition.ReportObjects["supname"] as TextObject;
                supname.Text = SupName;
                TextObject Supadd = rptwork.ReportDefinition.ReportObjects["saddress"] as TextObject;
                Supadd.Text = Address;

                //TextObject Fax = rptwork.ReportDefinition.ReportObjects["txtfax"] as TextObject;
                //Fax.Text =  fax;
                TextObject rptpurdate = rptwork.ReportDefinition.ReportObjects["txtOrderDate"] as TextObject;
                rptpurdate.Text = Orderdate;
                TextObject rptPara1 = rptwork.ReportDefinition.ReportObjects["TxtLETERDES"] as TextObject;
                rptPara1.Text = Para1;
                TextObject rptplace = rptwork.ReportDefinition.ReportObjects["place"] as TextObject;
                rptplace.Text = (place.Length > 0) ? trmplace + place : "";

                rptwork.ReportDefinition.Sections["GroupFooterSection5"].SectionFormat.EnableSuppress = (dtpaycch.Rows.Count > 0) ? false : true;


                TextObject rpttxtsupplydetails = rptwork.ReportDefinition.ReportObjects["txtsupplydetails"] as TextObject;
                rpttxtsupplydetails.Text = dtord.Rows[0]["pordnar"].ToString();
                rptwork.ReportDefinition.Sections["GroupFooterSection9"].SectionFormat.EnableSuppress = (place.Length > 0) ? false : true;


                TextObject rptpdate = rptwork.ReportDefinition.ReportObjects["pdate"] as TextObject;
                rptpdate.Text = (pdate.Length > 0) ? trmpdate + pdate : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection10"].SectionFormat.EnableSuppress = (pdate.Length > 0) ? false : true;


                TextObject rptcarring = rptwork.ReportDefinition.ReportObjects["carring"] as TextObject;
                rptcarring.Text = (carring.Length > 0) ? trmcarring + carring : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection11"].SectionFormat.EnableSuppress = (carring.Length > 0) ? false : true;


                TextObject rptpbill = rptwork.ReportDefinition.ReportObjects["bill"] as TextObject;
                rptpbill.Text = (bill.Length > 0) ? trmbill + bill : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection12"].SectionFormat.EnableSuppress = (bill.Length > 0) ? false : true;

                TextObject rptpayment1 = rptwork.ReportDefinition.ReportObjects["payment1"] as TextObject;
                rptpayment1.Text = (payment.Length > 0) ? trmpayment + payment : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection13"].SectionFormat.EnableSuppress = (payment.Length > 0) ? false : true;



                TextObject txtconcernperson = rptwork.ReportDefinition.ReportObjects["txtconcernperson"] as TextObject;
                txtconcernperson.Text = (cperson.Length > 0) ? cperson : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection3"].SectionFormat.EnableSuppress = (cperson.Length > 0) ? false : true;



                TextObject rptOthrs = rptwork.ReportDefinition.ReportObjects["others"] as TextObject;
                rptOthrs.Text = (Others.Length > 0) ? trmothers + Others : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection14"].SectionFormat.EnableSuppress = (Others.Length > 0) ? false : true;










                ///





                DataTable dtorder = (DataTable)ViewState["tblOrder"];
                DataTable dt1;
                DataTable dt2;
                DataTable dt3;


                // Carring
                DataView dv1 = dtorder.DefaultView;
                dv1.RowFilter = ("grp='A' and rsircode  like '019999901%'");
                dt1 = dv1.ToTable();

                //Deduction
                dv1 = dtorder.DefaultView;
                dv1.RowFilter = ("grp='A' and rsircode like'019999902%'");
                dt2 = dv1.ToTable();

                //Material
                dv1 = dtorder.DefaultView;
                dv1.RowFilter = ("grp='A' and rsircode not like '0199999%'");
                dt3 = dv1.ToTable();


                double amtcar = (dt1.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(ordramt)", "")) ? 0.00 : dt1.Compute("Sum(ordramt)", "")));
                double amtdis = (dt2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(ordramt)", "")) ? 0.00 : dt2.Compute("Sum(ordramt)", "")));
                //



                double amtmat = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(ordramt)", "")) ? 0.00 : dt3.Compute("Sum(ordramt)", "")));

                TextObject txtcarcost = rptwork.ReportDefinition.ReportObjects["txtcarcost"] as TextObject;
                txtcarcost.Text = amtcar.ToString("#,##0.00;(#,##0.00);");

                TextObject txtdiscount = rptwork.ReportDefinition.ReportObjects["txtdiscount"] as TextObject;
                txtdiscount.Text = amtdis.ToString("#,##0.00;(#,##0.00);");
                TextObject txtnettotal = rptwork.ReportDefinition.ReportObjects["txtnettotal"] as TextObject;
                txtnettotal.Text = (amtmat + amtcar - amtdis).ToString("#,##0.00;(#,##0.00);");



                TextObject txtkword = rptwork.ReportDefinition.ReportObjects["txtkword"] as TextObject;
                txtkword.Text = "In Word: " + ASTUtility.Trans(amtmat + amtcar - amtdis, 2);
                TextObject txtuserinfo = rptwork.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                // Sub Report 
                //ReportDocument  rptsub= new RealERPRPT.R_14_Pro.RptOrderPaymentSch();
                //rptsub.SetDataSource((DataTable)ViewState["tblpaysch"]);

                rptwork.SetDataSource(dt);
                rptwork.Subreports["RptOrderPaymentSch.rpt"].SetDataSource((DataTable)ViewState["tblpaysch"]);





                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rptwork.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rptwork;
                string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + orderno + ".pdf"; ;

                rptwork.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, apppath);



            }










            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }



        }

        private void SendSSLMail(string orderno)
        {


            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
            DataSet dssmtpandmail = this.accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");


            string mORDERNO = orderno;

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPUREMAIL", mORDERNO, "", "", "", "", "", "", "", "");

            string subject = "Work Order";
            //SMTP
            string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
            string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
            string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
            string mailtousr = ds1.Tables[0].Rows[0]["mailid"].ToString();
            string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf";


            EASendMail.SmtpMail oMail = new EASendMail.SmtpMail("TryIt");

            //Connection Details 
            SmtpServer oServer = new SmtpServer(hostname);
            oServer.User = frmemail;
            oServer.Password = psssword;
            oServer.Port = portnumber;
            //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;


            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
            oMail.From = frmemail;
            oMail.To = mailtousr;
            oMail.Cc = frmemail;
            oMail.Subject = subject;


            oMail.HtmlBody = "<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Dear Sir," + "<br/>" + "please find attached file" + "</pre></body></html>";
            oMail.AddAttachment(apppath);


            //System.Net.Mail.Attachment attachment;

            //attachment = new System.Net.Mail.Attachment(apppath);
            //oMail.AddAttachment(attachment);





            try
            {

                oSmtp.SendMail(oServer, oMail);
                ((Label)this.Master.FindControl("lblmsg")).Text = "Your message has been successfully sent.";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;
            }

        }


        private void SendNormalMail(string orderno)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
            DataSet dssmtpandmail = this.accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");


            string mORDERNO = orderno;

            DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPUREMAIL", mORDERNO, "", "", "", "", "", "", "", "");

            string subject = "Work Order";
            //SMTP
            string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());





            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(hostname, portnumber);
            //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = true;
            client.EnableSsl = false;
            string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
            string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            ///////////////////////

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new System.Net.Mail.MailAddress(frmemail);



            msg.To.Add(new System.Net.Mail.MailAddress(ds1.Tables[0].Rows[0]["mailid"].ToString()));
            msg.Subject = subject;
            msg.IsBodyHtml = true;

            System.Net.Mail.Attachment attachment;

            string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf"; ;

            attachment = new System.Net.Mail.Attachment(apppath);
            msg.Attachments.Add(attachment);



            msg.Body = string.Format("<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Dear Sir," + "<br/>" + "please find attached file" + "</pre></body></html>");
            try
            {
                client.Send(msg);

                ((Label)this.Master.FindControl("lblmsg")).Text = "Your message has been successfully sent.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


                //string savelocation = Server.MapPath("~") + "\\SupWorkOreder";
                //string[] filePaths = Directory.GetFiles(savelocation);
                //foreach (string filePath in filePaths)
                //    File.Delete(filePath);

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        protected void btnSetup_Click(object sender, EventArgs e)
        {

        }


        protected void gvreqsecapproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("hlnkbtnEntrysecapproved");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string comcod1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                hlink2.NavigateUrl = "~/F_12_Inv/PurReqEntry?InputType=ReqSecondApproved&prjcode=" + pactcode + "&genno=" + reqno + "&comcod=" + comcod1;

            }
        }
        
       
        protected void btnDirecdelReq_Click(object sender, EventArgs e)
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

        protected void btnDelFapproval_Click(object sender, EventArgs e)
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
            string toorder = Convert.ToDouble(((Label)this.grvMRec.Rows[RowIndex].FindControl("lblgvWoamt")).Text.Trim()).ToString();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURORDERINFO", genno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            bool result = this.XmlDataInsertOrder(genno, ds1);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;
            }

            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEORDERNOAAPPROVED", genno, "delsapp", toorder, "", "", "", "", "", "", "", "", "", "", "", "");


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

        protected void gvPurcom_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkmrrPrint");                
                string mrrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrrno")).ToString();
                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=MktMRRPrint&mrno=" + mrrno;
               

            }
        }
    }
}