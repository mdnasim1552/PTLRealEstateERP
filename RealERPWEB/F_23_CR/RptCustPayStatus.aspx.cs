using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealEntity.C_17_Acc;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_23_CR
{
    public partial class RptCustPayStatus : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        //decimal cinsamount = 0;
        //decimal payamount = 0;
        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                CommonButton();
                this.ShowView();
                //this.lblHeadtitle.Text = (this.Request.QueryString["Type"].ToString() == "ClLedger") ? "Client Ledger Report" : "CUSTOMER PAYMENT STATUS";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string Type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = Type == "ClLedger" ? "Client Ledger" : Type == "ClPayDetails" ? "Client Payment Details" : "PAYMENT STATUS";
                //Type == "RecPayABal" ? "RECEIVED AND PAYMENT STATUS" : "CUSTOMER PAYMENT STATUS"; // "RECEIVED AND PAYMENT STATUS";

            }
        }


        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            string Type = this.Request.QueryString["Type"].ToString();
            if (Type == "Payment")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnupdateb_OnClick);
            }

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }



        private void ShowView()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            string comcod = this.GetComeCode();
            switch (Type)
            {
                case "Payment":
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "ClLedger":
                    //this.lbtnOk.Visible = false;
                    this.MultiView1.ActiveViewIndex = 1;
                    switch (comcod)
                    {
                        case "3336":
                        case "3337":
                            //case "3101":
                            this.chkConsolidate.Checked = true;
                            this.chkConsolidate.Visible = true;
                            break;


                        case "2305":// rupayan
                        case "3305":
                        case "3306":
                        case "3311":
                        case "3310": //RCU

                        case "3349":
                        case "3364":
                            this.chkConsolidate.Checked = false;
                            this.chkConsolidate.Visible = false;
                            break;




                        default:
                            this.chkConsolidate.Checked = true;
                            this.chkConsolidate.Visible = true;
                            break;
                    }
                    break;
                case "RecReceivable":
                    this.lblentryben.Visible = false;
                    this.txtentryben.Visible = false;
                    this.lbldelaychrg.Visible = false;
                    this.txtdelaychrg.Visible = false;
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
                case "RecPayABal":
                    this.lblentryben.Visible = false;
                    this.txtentryben.Visible = false;
                    this.lbldelaychrg.Visible = false;
                    this.txtdelaychrg.Visible = false;
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                    this.lblFdate.Visible = true;
                    this.txFdate.Visible = true;
                    this.Operningdat();
                    this.MultiView1.ActiveViewIndex = 3;
                    break;

                case "ClPayDetails":
                    this.MultiView1.ActiveViewIndex = 4;
                    switch (comcod)
                    {
                        case "3336":
                        case "3337":
                        case "3101":
                            this.upben.Visible = false;
                            this.chkConsolidate.Checked = true;
                            this.chkConsolidate.Visible = true;
                            break;
                        default:

                            this.upben.Visible = false;
                            this.chkConsolidate.Checked = true;
                            this.chkConsolidate.Visible = true;

                            break;
                    }
                    break;
            }
        }

        public void Operningdat()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string datepart;
            DataSet ds4 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            if (dt4.Rows.Count == 0)
            {
                datepart = "";
            }
            else
            {
                datepart = Convert.ToDateTime(dt4.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
            }
            if (datepart == "")
            {
                this.txFdate.Text = datepart.ToString();
                this.txFdate.Enabled = true;
            }
            else
            {
                this.txFdate.Text = datepart;
                //this.txtDatefrom.Enabled = false;
            }
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetProjectName()
        {
            string comcod = this.GetComeCode();


            string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            //(this.Request.QueryString["prjcode"] == null) ? "%" : this.Request.QueryString["prjcode"].ToString(); //
            if (this.Request.QueryString["prjcode"] == null || Request.QueryString["prjcode"].ToString() == "")
            {
                this.ddlProjectName.Enabled = true;
            }
            else
            {
                this.ddlProjectName.SelectedValue = "18" + ASTUtility.Right(this.Request.QueryString["prjcode"], 10);
                this.ddlProjectName.Enabled = false;
            }

            this.ddlProjectName_SelectedIndexChanged(null, null);
        }

        private void GetCustomerName()
        {
            ViewState.Remove("tblcustomer");

            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSProject = "%" + this.txtSrcCustomer.Text.Trim() + "%";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCUSTOMERNAME", pactcode, txtSProject, "", "", "", "", "", "", "");
            this.ddlCustName.DataTextField = "custnam";
            this.ddlCustName.DataValueField = "custid";
            this.ddlCustName.DataSource = ds2.Tables[0];
            this.ddlCustName.DataBind();
            ViewState["tblcustomer"] = ds2.Tables[0];
            ds2.Dispose();

        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetCustomerName();

        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void imgbtnFindCustomer_Click(object sender, EventArgs e)
        {
            this.GetCustomerName();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            if (Type == "ClLedger")
            {
                //this.upben.Visible = true;
                this.showClLedger();
            }

            else if (Type == "ClPayDetails")
            {
                this.showClPayDetail();
            }
            else if (Type == "RecReceivable")
            {
                this.showResReceivale();
            }
            else if (Type == "RecPayABal")
            {
                this.showRecPayable();
            }

            else
            {
                this.ShowCustPayment();
            }
        }

        private void showClLedger()
        {
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");

            string CallType = this.ClientCalltype();
            string length = comcod == "3348" ? "length" : "";


            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, custid, Date, length, "", "", "", "", "");
            //DataSet ds2= purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "INSTALLMANTWITHMRR", pactcode, custid, Date, "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvCustLedger.DataSource = null;
                this.gvCustLedger.DataBind();
                return;
            }

            Session["tblCustPayment"] = this.HiddenSameDate2(ds2.Tables[0]);
            DataTable dt = ds2.Tables[3];

            this.txtentryben.Text = ((dt.Select("code='001'")).Length == 0) ? "" : Convert.ToDouble(dt.Select("code='001'")[0]["charge"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtdelaychrg.Text = ((dt.Select("code='001'")).Length == 0) ? "" : Convert.ToDouble(dt.Select("code='002'")[0]["charge"]).ToString("#,##0.00;(#,##0.00); ");
            this.Data_Bind();
        }


        private void showClPayDetail()
        {
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");

            string CallType = this.ClientCalltype();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, custid, Date, "", "", "", "", "", "");
            //DataSet ds2= purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "INSTALLMANTWITHMRR", pactcode, custid, Date, "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvClpaydetials.DataSource = null;
                this.gvClpaydetials.DataBind();
                return;
            }

            Session["tblCustPayment"] = this.HiddenSameDate2(ds2.Tables[0]);
            DataTable dt = ds2.Tables[3];

            this.txtentryben.Text = ((dt.Select("code='001'")).Length == 0) ? "" : Convert.ToDouble(dt.Select("code='001'")[0]["charge"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtdelaychrg.Text = ((dt.Select("code='001'")).Length == 0) ? "" : Convert.ToDouble(dt.Select("code='002'")[0]["charge"]).ToString("#,##0.00;(#,##0.00); ");
            this.Data_Bind();

        }
        private void showResReceivale()
        {

            Session.Remove("tblResReceiv");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTPROUNITDUESSUMMARY", pactcode, custid, Date, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvRecReceived.DataSource = null;
                this.gvRecReceived.DataBind();
                return;
            }

            Session["tblResReceiv"] = ds2.Tables[0];
            this.gvRecReceived.DataSource = ds2.Tables[0];
            this.gvRecReceived.DataBind();
            this.FooterCalRes();
        }


        private void showRecPayable()
        {

            Session.Remove("tblResRecPayable");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string fdate = Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds3 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTSTDUNITRECPAYANDBAL", fdate, todate, pactcode, custid, "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvrecpayable.DataSource = null;
                this.gvrecpayable.DataBind();
                return;
            }

            Session["tblResRecPayable"] = ds3.Tables[0];
            this.gvrecpayable.DataSource = ds3.Tables[0];
            this.gvrecpayable.DataBind();
            this.FooterRecpayable();
        }



        private void FooterRecpayable()
        {
            //// IQBAL NAYAN
            DataTable dt = (DataTable)Session["tblResRecPayable"];
            if (dt.Rows.Count == 0)
            {
                return;
            }

            //double ReAmount = 0;
            //double RAmount = 0, DBalAmt = 0;
            //ReAmount = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(receivable)", "")) ? 0.00 : dt2.Compute("Sum(receivable)", "")));
            //RAmount = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(receipt)", "")) ? 0.00 : dt2.Compute("Sum(receipt)", "")));
            //DBalAmt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(duebal)", "")) ? 0.00 : dt2.Compute("Sum(duebal)", "")));
            //RAmount = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(receipt)", "")) ? 0.00 : dt2.Compute("Sum(receipt)", "")));
            //DBalAmt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(duebal)", "")) ? 0.00 : dt2.Compute("Sum(duebal)", "")));

            //((Label)this.gvRecReceived.FooterRow.FindControl("lblgvfResable")).Text = ReAmount.ToString("#,##0;(#,##0); ");
            //((Label)this.gvRecReceived.FooterRow.FindControl("lblgvFRreceipt")).Text = RAmount.ToString("#,##0;(#,##0); ");
            //((Label)this.gvRecReceived.FooterRow.FindControl("lblgvFduebal")).Text = DBalAmt.ToString("#,##0;(#,##0); ");
            //((Label)this.gvRecReceived.FooterRow.FindControl("lblgvFRreceipt")).Text = RAmount.ToString("#,##0;(#,##0); ");
            //((Label)this.gvRecReceived.FooterRow.FindControl("lblgvFduebal")).Text = DBalAmt.ToString("#,##0;(#,##0); ");

            //opndram, opncram, dram , cram ,	closam  Iqbal Nayan

            ((Label)this.gvrecpayable.FooterRow.FindControl("lblgvfopndrame")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opncram)", "")) ?
                    0.00 : dt.Compute("Sum(opncram)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvrecpayable.FooterRow.FindControl("lblgvFopncram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opndram)", "")) ?
                    0.00 : dt.Compute("Sum(opndram)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvrecpayable.FooterRow.FindControl("lblgvFdram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cram)", "")) ?
                    0.00 : dt.Compute("Sum(cram)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvrecpayable.FooterRow.FindControl("lblgvFcram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dram)", "")) ?
                    0.00 : dt.Compute("Sum(dram)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvrecpayable.FooterRow.FindControl("lblgvFclosam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closam)", "")) ?
                    0.00 : dt.Compute("Sum(closam)", ""))).ToString("#,##0;(#,##0); ");

        }
        private void FooterCalRes()
        {
            // IQBAL NAYAN
            DataTable dt2 = (DataTable)Session["tblResReceiv"];
            double ReAmount = 0;
            double RAmount = 0, DBalAmt = 0;
            ReAmount = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(receivable)", "")) ? 0.00 : dt2.Compute("Sum(receivable)", "")));
            RAmount = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(receipt)", "")) ? 0.00 : dt2.Compute("Sum(receipt)", "")));
            DBalAmt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(duebal)", "")) ? 0.00 : dt2.Compute("Sum(duebal)", "")));

            ((Label)this.gvRecReceived.FooterRow.FindControl("lblgvfResable")).Text = ReAmount.ToString("#,##0;(#,##0); ");
            ((Label)this.gvRecReceived.FooterRow.FindControl("lblgvFRreceipt")).Text = RAmount.ToString("#,##0;(#,##0); ");
            ((Label)this.gvRecReceived.FooterRow.FindControl("lblgvFduebal")).Text = DBalAmt.ToString("#,##0;(#,##0); ");

            //if (dt2.Rows.Count > 0)
            //    ((Label)this.gvCDHonour.FooterRow.FindControl("lblgvFamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(schamt)", "")) ? 0.00
            //            : dt2.Compute("Sum(schamt)", ""))).ToString("#,##0;(#,##0); -");
        }



        private string calltype()
        {

            string comcod = this.GetComeCode();
            string calltype = "";
            switch (comcod)
            {
                case "3330":
                    // case "3101":
                    calltype = "RPTCLIENTPAYMENTSTATUS";
                    break;

                default:
                    calltype = "INSTALLMANTWITHMRR";
                    break;


            }

            return calltype;

        }

        private string procedure()
        {

            string comcod = this.GetComeCode();
            string procedure = "";
            switch (comcod)
            {
                case "3330":
                    // case "3101":
                    procedure = "SP_REPORT_SALSMGT03";
                    break;

                default:
                    procedure = "SP_ENTRY_SALSMGT";
                    break;


            }

            return procedure;

        }

        private string CompanyDelSerial()
        {

            string comcod = this.GetComeCode();
            string delserial ;
            switch (comcod)
            {
                case "3354": //Edison Real Estate
                case "3101":

                    delserial = "delayserial";
                    break;

                default:
                    delserial = "";
                    break;


            }

            return delserial;




        }

        private void ShowCustPayment()
        {

            this.lblPayShe.Visible = true;
            this.lblchqdishonour.Visible = true;
            Session.Remove("tblCustPayment");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string calltype = this.calltype();
            string procedure = this.procedure();
            string delserial = this.CompanyDelSerial();
            DataSet ds2 = purData.GetTransInfo(comcod, procedure, calltype, pactcode, custid, Date, delserial, "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvCustPayment.DataSource = null;
                this.gvCustPayment.DataBind();
                return;
            }

            Session["tblCustPayment"] = this.HiddenSameDate2(ds2.Tables[0]);
            this.Data_Bind();
        }
        private void HiddenSameData(DataTable dtable)
        {

            //if (dtable.Rows.Count == 0)
            //    return dtable;

            //string gcod = dt1.Rows[0]["gcod"].ToString();

            //for (int j = 1; j < dt1.Rows.Count; j++)
            //{
            //    if (dt1.Rows[j]["grp"].ToString() == "AA" && dt1.Rows[j]["gcod"].ToString() == gcod)
            //    {
            //        gcod = dt1.Rows[j]["gcod"].ToString();
            //        dt1.Rows[j]["gcod"] = "";
            //        dt1.Rows[j]["gdesc"] = "";
            //        dt1.Rows[j]["pactcode"] = "";
            //        dt1.Rows[j]["usircode"] = "";
            //        dt1.Rows[j]["schamt"] = 0;
            //        dt1.Rows[j]["schdate"] = "";
            //    }

            //    else
            //    {
            //        gcod = dt1.Rows[j]["gcod"].ToString();
            //    }

            //}
            //return dt1;
            //Session.Remove("tblCustPayment");
            string gcod = dtable.Rows[0]["gcod"].ToString();

            DataTable dt1 = dtable;
            DataView dv1 = dt1.DefaultView;
            dv1.RowFilter = "grp like 'AA' ";
            dt1 = dv1.ToTable();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["gcod"].ToString() == gcod)
                {
                    gcod = dt1.Rows[j]["gcod"].ToString();
                    dt1.Rows[j]["gcod"] = "";
                    dt1.Rows[j]["gdesc"] = "";
                    dt1.Rows[j]["pactcode"] = "";
                    dt1.Rows[j]["usircode"] = "";
                    dt1.Rows[j]["schamt"] = 0;
                    dt1.Rows[j]["schdate"] = "";
                }

                else
                {
                    gcod = dt1.Rows[j]["gcod"].ToString();
                }

            }
            this.lblPayShe.Visible = true;
            Session["tblCustPayment"] = dt1;
            this.gvCustPayment.DataSource = dt1;
            this.gvCustPayment.DataBind();

            DataTable dt2 = dtable;
            DataView dv2 = dt2.DefaultView;
            dv2.RowFilter = "grp like 'BB' ";
            dt2 = dv2.ToTable();
            if (dt2 == null)
                return;
            this.lblchqdishonour.Visible = true;
            this.gvCDHonour.DataSource = dv2;
            this.gvCDHonour.DataBind();


        }
        private DataTable HiddenSameDate2(DataTable dtable)
        {

            Session.Remove("tblCustPayment");
            string gcod = dtable.Rows[0]["gcod"].ToString();
            string Type = this.Request.QueryString["Type"].ToString();
            DataTable dt1 = dtable;

            switch (Type)
            {
                case "Payment":
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == "AA" && dt1.Rows[j]["gcod"].ToString() == gcod)
                        {
                            gcod = dt1.Rows[j]["gcod"].ToString();
                            dt1.Rows[j]["gcod"] = "";
                            dt1.Rows[j]["gdesc"] = "";
                            dt1.Rows[j]["pactcode"] = "";
                            dt1.Rows[j]["usircode"] = "";
                            dt1.Rows[j]["schamt"] = 0;
                            dt1.Rows[j]["schdate"] = "";
                        }

                        else
                        {
                            gcod = dt1.Rows[j]["gcod"].ToString();
                        }

                    }
                    break;

                case "ClPayDetails":
                case "ClLedger":


                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == "AA" && dt1.Rows[j]["gcod"].ToString() == gcod)
                        {
                            gcod = dt1.Rows[j]["gcod"].ToString();
                            dt1.Rows[j]["gcod"] = "";
                            dt1.Rows[j]["gdesc"] = "";
                            dt1.Rows[j]["pactcode"] = "";
                            dt1.Rows[j]["usircode"] = "";
                            dt1.Rows[j]["schamt"] = 0;
                            dt1.Rows[j]["asondues"] = 0;
                            dt1.Rows[j]["schdate"] = "";
                        }

                        else
                        {
                            gcod = dt1.Rows[j]["gcod"].ToString();
                        }

                    }

                    int torow = dt1.Rows.Count - 1;

                    if (torow > 0)
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {

                            if (j == torow)
                            {
                                double ppaidamt = Convert.ToDouble(dt1.Rows[j - 1]["paidamt"].ToString());

                                DateTime schdate = System.DateTime.Today;
                                //string schdate = System.DateTime.Now;

                                if (dt1.Rows[j - 1]["schdate"].ToString().Trim().Length > 0)
                                {
                                    schdate = Convert.ToDateTime(dt1.Rows[j - 1]["schdate"]);
                                }


                                DateTime date = Convert.ToDateTime(this.txtDate.Text);


                                if (ppaidamt > 0)
                                {

                                    double schamt = Convert.ToDouble(dt1.Rows[j]["schamt1"].ToString());
                                    double paidamt = Convert.ToDouble(dt1.Rows[j]["paidamt"].ToString());
                                    dt1.Rows[j]["balamt"] = schdate <= date ? 0.00 : schamt - (paidamt > 0 ? paidamt : 0.00);
                                }
                            }
                            else
                            {

                                double npaidamt = Convert.ToDouble(dt1.Rows[j + 1]["paidamt"].ToString());
                                if (npaidamt > 0)
                                {
                                    double schamt = Convert.ToDouble(dt1.Rows[j]["schamt"].ToString());
                                    double paidamt = Convert.ToDouble(dt1.Rows[j]["schamt"].ToString());
                                    dt1.Rows[j]["balamt"] = schamt - paidamt;
                                }


                                else if (npaidamt < 0)
                                {

                                    dt1.Rows[j]["balamt"] = 0.00;


                                }


                            }
                        }

                    break;
            }


            return dt1;
            //Session["tblCustPayment"] = dt1;

        }




        private void Data_Bind()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            DataTable dt = (DataTable)Session["tblCustPayment"];

            if (Type == "ClLedger")
            {

                bool result = this.chkConsolidate.Checked;

                if (result == true)
                {
                    this.gvCustLedger.Columns[13].Visible = true;
                    this.gvCustLedger.Columns[14].Visible = false;
                }

                this.gvCustLedger.DataSource = dt;
                this.gvCustLedger.DataBind();
                this.FooterCalculation();
                Session["Report1"] = gvCustLedger;
                ((HyperLink)this.gvCustLedger.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }


            else if (Type == "ClPayDetails")
            {


                //  bool result = this.chkConsolidate.Checked;

                this.gvClpaydetials.DataSource = dt;
                this.gvClpaydetials.DataBind();
                this.FooterClPayDetialsCal();
                Session["Report1"] = gvClpaydetials;
                ((HyperLink)this.gvClpaydetials.HeaderRow.FindControl("hlbtntbCdataExelpay")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
            else
            {
                this.FooterCal((DataTable)Session["tblCustPayment"]);

            }

        }

        private void FooterClPayDetialsCal()
        {
            DataTable dt = (DataTable)Session["tblCustPayment"];
            ((Label)this.gvClpaydetials.FooterRow.FindControl("lblFscamtpay")).Text =
              Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(schamt)", "")) ?
              0.00 : dt.Compute("Sum(schamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvClpaydetials.FooterRow.FindControl("lblFrcvamtpay")).Text =
             Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(paidamt)", "")) ?
             0.00 : dt.Compute("Sum(paidamt)", ""))).ToString("#,##0;(#,##0); ");
            double Schamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(schamt)", "")) ? 0.00 : dt.Compute("Sum(schamt)", "")));
            double rcvamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(paidamt)", "")) ? 0.00 : dt.Compute("Sum(paidamt)", "")));
            double balamt = Schamt - rcvamt;
            ((Label)this.gvClpaydetials.FooterRow.FindControl("lblFBalTamtpay")).Text = balamt.ToString("#,##0;(#,##0); ");
        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblCustPayment"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "grp like 'AA' ";
            dt = dv1.ToTable();
            ((Label)this.gvCustLedger.FooterRow.FindControl("lblFscamt")).Text =
              Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(schamt)", "")) ?
              0.00 : dt.Compute("Sum(schamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCustLedger.FooterRow.FindControl("lblFrcvamt")).Text =
             Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(paidamt)", "")) ?
             0.00 : dt.Compute("Sum(paidamt)", ""))).ToString("#,##0;(#,##0); ");
            double Schamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(schamt)", "")) ? 0.00 : dt.Compute("Sum(schamt)", "")));
            double rcvamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(paidamt)", "")) ? 0.00 : dt.Compute("Sum(paidamt)", "")));
            double balamt = Schamt - rcvamt;
            ((Label)this.gvCustLedger.FooterRow.FindControl("lblFBalTamt")).Text = balamt.ToString("#,##0;(#,##0); ");
        }

        private void FooterCal(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;
            DataTable dt1 = dt;
            DataView dv1 = dt1.DefaultView;
            dv1.RowFilter = "grp like 'AA' ";
            dt1 = dv1.ToTable();
            this.gvCustPayment.DataSource = dt1;
            this.gvCustPayment.DataBind();

            double SAmount = 0;
            double PAmount = 0, BalAmt = 0;
            SAmount = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(schamt)", "")) ? 0.00 : dt1.Compute("Sum(schamt)", "")));
            PAmount = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(paidamt)", "")) ? 0.00 : dt1.Compute("Sum(paidamt)", "")));
            BalAmt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(balamt)", "")) ? 0.00 : dt1.Compute("Sum(balamt)", "")));

            ((Label)this.gvCustPayment.FooterRow.FindControl("lfAmt")).Text = SAmount.ToString("#,##0;(#,##0); ");
            ((Label)this.gvCustPayment.FooterRow.FindControl("lgvfpayamt")).Text = PAmount.ToString("#,##0;(#,##0); ");

            DataTable dt2 = dt;
            DataView dv2 = dt2.DefaultView;
            dv2.RowFilter = "grp like 'BB' ";
            dt2 = dv2.ToTable();
            this.gvCDHonour.DataSource = dt2;
            this.gvCDHonour.DataBind();

            if (dt2.Rows.Count > 0)
            {
                ((Label)this.gvCDHonour.FooterRow.FindControl("lblgvFamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(schamt)", "")) ? 0.00
                        : dt2.Compute("Sum(schamt)", ""))).ToString("#,##0;(#,##0); -");
            }
            Session["Report1"] = gvCustPayment;
            ((HyperLink)this.gvCustPayment.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //IQBAL NAYAN
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Type = Request.QueryString["Type"].ToString();
            if (Type == "Payment")
            {
                this.CompanyType();
            }
            else if (Type == "RecReceivable")
            {
                this.PrintResReceiable();
            }
            else if (Type == "RecPayABal")
            {
                this.PrintResPayable();
            }
            else
                this.PrintCleintLedger();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report";
                string eventdesc2 = this.Request.QueryString["Type"].ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        private void PrintResPayable()
        {

            //IQBAL NAYAN
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
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string fdate = Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblResRecPayable"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.RecePaya>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptReceiptPayable", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", "Project Name: " + this.ddlProjectName.SelectedItem.ToString()));
            Rpt1.SetParameters(new ReportParameter("Customa", "Customar Name: " + this.ddlCustName.SelectedItem.ToString().ToLower()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Receipt and Payable"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("Date", "(From " + Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + " )"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintResReceiable()
        {
            //IQBAL NAYAN
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
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt = (DataTable)Session["tblResReceiv"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.Resreceivable>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptResreceivable", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", "Project Name: " + this.ddlProjectName.SelectedItem.ToString()));
            Rpt1.SetParameters(new ReportParameter("Customa", "Customer Name: " + this.ddlCustName.SelectedItem.ToString().ToLower()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Receipt and Receivable"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("Date", "As On Date: " + Date));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void CompanyType()
        {
            string comcod = this.GetComeCode();

            if (comcod == "3330")
            {
                this.PrintClientPaymentSchedule();
            }

            //else if (comcod== "3348")
            //{
            //    this.PrintPaymentS();
            //}
            else
            {
                this.PrintPaymentSchedule();
            }

        }
        private void PrintPaymentSchedule()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetComeCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            DataTable dtstatus = (DataTable)Session["tblCustPayment"];
            DataSet ds5 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "REPORTPAYMENTSTATUS", pactcode, custid, "", "", "", "", "", "", "");
            if (ds5 == null)
                return;


            DataTable dtcust = ds5.Tables[0];
            //Session["REPORTPAYMENTSTATUS"] = dtcust;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string custname = dtcust.Rows[0]["custname"].ToString();
            string custadd = dtcust.Rows[0]["custadd"].ToString();
            string custmob = dtcust.Rows[0]["custmob"].ToString();
            string pactdesc = dtcust.Rows[0]["pactdesc"].ToString();
            string munit = dtcust.Rows[0]["munit"].ToString();
            string udesc = dtcust.Rows[0]["udesc"].ToString();
            string usize = Convert.ToDouble(dtcust.Rows[0]["usize"]).ToString("#,##0;(#,##0); -");

            double SAmount = Convert.ToDouble("0" + ((Label)this.gvCustPayment.FooterRow.FindControl("lfAmt")).Text);
            double PAmount = Convert.ToDouble("0" + ((Label)this.gvCustPayment.FooterRow.FindControl("lgvfpayamt")).Text);

            LocalReport Rpt1 = new LocalReport();
            var lst = dtstatus.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.PaymentStatus>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptPaymentStatus", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyName", comnam));
            //Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Client Payment Status"));
            Rpt1.SetParameters(new ReportParameter("cusname", custname));
            Rpt1.SetParameters(new ReportParameter("cusadd", custadd));
            //Rpt1.SetParameters(new ReportParameter("cusmob", custmob));
            Rpt1.SetParameters(new ReportParameter("projectName", pactdesc));
            Rpt1.SetParameters(new ReportParameter("unitdesc", udesc));
            //Rpt1.SetParameters(new ReportParameter("date", Date));
            Rpt1.SetParameters(new ReportParameter("comLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("bAmount", (SAmount - PAmount).ToString("#,##0;(#,##0); ")));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintClientPaymentSchedule()
        {
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
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            DataSet ds = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "REPORTPAYMENTSTATUS", pactcode, custid, "", "", "", "", "", "", "");
            if (ds == null)
                return;

            DataTable dtcust = ds.Tables[0];
            string custname = dtcust.Rows[0]["custname"].ToString();
            string custadd = dtcust.Rows[0]["custadd"].ToString();
            string custmob = dtcust.Rows[0]["custmob"].ToString();
            string pactdesc = dtcust.Rows[0]["pactdesc"].ToString();
            string munit = dtcust.Rows[0]["munit"].ToString();
            string udesc = dtcust.Rows[0]["udesc"].ToString();
            string usize = Convert.ToDouble(dtcust.Rows[0]["usize"]).ToString("#,##0;(#,##0); -");
            double SAmount = Convert.ToDouble("0" + ((Label)this.gvCustPayment.FooterRow.FindControl("lfAmt")).Text);
            double PAmount = Convert.ToDouble("0" + ((Label)this.gvCustPayment.FooterRow.FindControl("lgvfpayamt")).Text);

            string bAmount = (SAmount - PAmount).ToString("#,##0;(#,##0); ");
            string unitdesc = udesc + ", " + usize + " " + munit;

            DataTable dt = (DataTable)Session["tblCustPayment"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.ClientPaymentStatus>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptPaymentStatus02", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyName", comnam));
            //Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Client Payment Status"));
            Rpt1.SetParameters(new ReportParameter("cusname", custname));
            Rpt1.SetParameters(new ReportParameter("cusadd", custadd));
            Rpt1.SetParameters(new ReportParameter("cusmob", custmob));
            Rpt1.SetParameters(new ReportParameter("projectName", pactdesc));
            Rpt1.SetParameters(new ReportParameter("unitdesc", unitdesc));
            Rpt1.SetParameters(new ReportParameter("date", Date));
            Rpt1.SetParameters(new ReportParameter("comLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("bAmount", bAmount));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        private void PrintCleintLedgerBr()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetComeCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds5 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "INSTALLMANTWITHMRR", pactcode, custid, Date, "", "", "", "", "", "");

            DataTable tblins = this.HiddenSameDate2(ds5.Tables[0]);

            double aprment = Convert.ToDouble(ds5.Tables[1].Rows[0]["aptprice"]);
            double carparking = Convert.ToDouble(ds5.Tables[1].Rows[0]["carparking"]);
            double utility = Convert.ToDouble(ds5.Tables[1].Rows[0]["utility"]);
            double modicharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["adwrk"]);
            double delcharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["delchg"]);
            double regisfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["regavat"]);
            double assciationfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["devcharge"]);
            double associafeerec = Convert.ToDouble(ds5.Tables[1].Rows[0]["associarec"]);
            double transfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["transfee"]);

            double others = Convert.ToDouble(ds5.Tables[1].Rows[0]["transother"]);
            //Sales Part  
            double totalsales = aprment + carparking + utility;
            double totalreceevable = totalsales + delcharge + modicharge + regisfee + transfee + others;
            double totalrecived = Convert.ToDouble((Convert.IsDBNull(ds5.Tables[0].Compute("Sum(paidamt)", "")) ? 0.00 : ds5.Tables[0].Compute("Sum(paidamt)", "")));
            double balance = totalreceevable - totalrecived;
            // Association part
            double associabal = assciationfee - associafeerec;
            double netbal = balance + associabal;


            // rdlc start

            //string rptwelfarefund = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");
            string rptothers = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");
            //string rpttoprice = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            //string rptcooperative = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");

            string rptunittype = ds5.Tables[1].Rows[0]["unittype"].ToString();
            string txtdate = "Print date: " + this.txtDate.Text.Trim();
            string rptcustname = ds5.Tables[1].Rows[0]["name"].ToString();
            string rptcustadd = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            string rptcustphone = ds5.Tables[1].Rows[0]["telephone"].ToString();
            string rptpactdesc = ds5.Tables[1].Rows[0]["projectname"].ToString();

            string rptunitdesc = ds5.Tables[1].Rows[0]["aptname"].ToString();
            string rptusize = ds5.Tables[1].Rows[0]["aptsize"].ToString();
            string rptsalesteam = ds5.Tables[1].Rows[0]["salesteam"].ToString();
            string rptsalesdate = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-mmm-yyyy");
            string rptagreementdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-mmm-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-mmm-yyyy");
            string rpthandoverdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-mmm-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-mmm-yyyy");

            string rptdelcharge = (delcharge > 0) ? delcharge.ToString("#,##0;(#,##0); ") : "";

            //string txtearlyben = (ebenamt > 0) ? ("early benefit: " + ebenamt.ToString("#,##0;(#,##0); ")) : ""; ;

            string printFooter = ASTUtility.Concat(compname, username, printdate);
            string comlogo = new Uri(Server.MapPath(@"~\image\logo" + comcod + ".jpg")).AbsoluteUri;

            var lst = tblins.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedgerBridge02", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", comlogo));
            Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            Rpt1.SetParameters(new ReportParameter("Compadd", comadd));
            Rpt1.SetParameters(new ReportParameter("title", "Client Ledger"));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtdate));

            Rpt1.SetParameters(new ReportParameter("rptcustname", rptcustname));
            Rpt1.SetParameters(new ReportParameter("rptCustAdd", rptcustadd));
            Rpt1.SetParameters(new ReportParameter("rptCustPhone", rptcustphone));
            Rpt1.SetParameters(new ReportParameter("rptpactdesc", rptpactdesc));
            Rpt1.SetParameters(new ReportParameter("rptUnitDesc", rptunitdesc));
            Rpt1.SetParameters(new ReportParameter("rptUsize", rptusize));
            Rpt1.SetParameters(new ReportParameter("rptSalesteam", rptsalesteam));
            Rpt1.SetParameters(new ReportParameter("rptsalesdate", rptsalesdate));
            Rpt1.SetParameters(new ReportParameter("rptagreementdate", rptagreementdate));
            Rpt1.SetParameters(new ReportParameter("rptHandoverdate", rpthandoverdate));

            Rpt1.SetParameters(new ReportParameter("rptapartmentprice", aprment.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcarparking", carparking.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptUtility", utility.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txttoSalevalue", totalsales.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptdelcharge", rptdelcharge));
            Rpt1.SetParameters(new ReportParameter("modicharge", modicharge.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("regisfee", regisfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("transfee", transfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptOthers", rptothers));
            Rpt1.SetParameters(new ReportParameter("receivableTotal", totalreceevable.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txttoreceivedvalue", totalrecived.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("duebalanceA", balance.ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("assciationfee", assciationfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("assciationfeeRecv", associafeerec.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("duebalanceB", associabal.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetbalance", netbal.ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private string ClientCalltype()
        {
            bool result = this.chkConsolidate.Checked;
            string Calltype = "";
            switch (result)
            {
                case true:
                    Calltype = "RPTCLIENTLEDGER";
                    break;

                default:
                    Calltype = "INSTALLMANTWITHMRR";
                    break;


            }


            return Calltype;

        }


        private void PrintCleintLedgerSuvastu()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetComeCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string CallType = this.ClientCalltype();
            DataSet ds5 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, custid, Date, "", "", "", "", "", "");



            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");


            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTINTEREST", pactcode, custid, frmdate, frmdate, "", "", "", "", "");

            DataView dv;
            dv = ds2.Tables[0].DefaultView;
            dv.RowFilter = ("grp='A' and interest>0");
            DataTable dt1 = dv.ToTable();

            dv = ds2.Tables[0].DefaultView;
            dv.RowFilter = ("grp='A' and interest<0");
            DataTable dt2 = dv.ToTable();


            double delcharge = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(interest)", "")) ? 0.00
                    : dt1.Compute("Sum(interest)", "")));

            double ebenamt = (Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(interest)", "")) ? 0.00
                     : dt2.Compute("Sum(interest)", "")))) * -1;



            DataTable tblins = this.HiddenSameDate2(ds5.Tables[0]);

            DataTable dtsum = ds5.Tables[2];
            double tsalevalue = Convert.ToDouble(ds5.Tables[1].Rows[0]["acprice"]);
            double treceived = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(paidamt)", "")) ? 0.00
                    : tblins.Compute("Sum(paidamt)", "")));

            double asondues = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(asondues)", "")) ? 0.00
                    : tblins.Compute("Sum(asondues)", "")));

            double receivableTotal = treceived - asondues;
            string totalRceeivable = receivableTotal.ToString("#,##0;(#,##0); ");
            // May be Problem
            double fcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["fcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["fcheque"]) : 0.00;
            double retcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["retcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["retcheque"]) : 0.00;
            double pcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["pcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["pcheque"]) : 0.00;
            //double reconamt = treceived - (fcheque + retcheque + pcheque);
            double reconamt = treceived - (fcheque + pcheque);

            double netbal = tsalevalue - reconamt;

            string aprment = Convert.ToDouble(ds5.Tables[1].Rows[0]["aptprice"]).ToString("#,##0;(#,##0); ");
            string carparking = Convert.ToDouble(ds5.Tables[1].Rows[0]["carparking"]).ToString("#,##0;(#,##0); ");
            string utility = Convert.ToDouble(ds5.Tables[1].Rows[0]["utility"]).ToString("#,##0;(#,##0); ");
            string modicharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["adwrk"]).ToString("#,##0;(#,##0); ");
            //double delcharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["delchg"]);
            string regisfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["regavat"]).ToString("#,##0;(#,##0); ");
            string assciationfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["devcharge"]).ToString("#,##0;(#,##0); ");
            string transfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["transfee"]).ToString("#,##0;(#,##0); ");

            string rptwelfarefund = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");
            string rptothers = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");
            string rpttoprice = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            string rptcooperative = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");

            string txttosalevalue = tsalevalue.ToString("#,##0;(#,##0); ");
            string txttoreceivedvalue = treceived.ToString("#,##0;(#,##0); ");
            string txtfcheque = fcheque.ToString("#,##0;(#,##0); ");
            string txtpcheque = pcheque.ToString("#,##0;(#,##0); ");
            string txtreconamt = reconamt.ToString("#,##0;(#,##0); ");
            string txtnetbalance = netbal.ToString("#,##0;(#,##0); ");

            string rpttxtcompanyname = comnam;
            string rptcompadd = comadd;
            string rptunittype = ds5.Tables[1].Rows[0]["unittype"].ToString();
            string txtdate = "print date: " + this.txtDate.Text.Trim();
            string rptcustname = ds5.Tables[1].Rows[0]["name"].ToString();
            string rptcustadd = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            string rptcustphone = ds5.Tables[1].Rows[0]["telephone"].ToString();
            string rptpactdesc = ds5.Tables[1].Rows[0]["projectname"].ToString();

            string rptunitdesc = ds5.Tables[1].Rows[0]["aptname"].ToString();
            string rptusize = ds5.Tables[1].Rows[0]["aptsize"].ToString();
            string rptsalesteam = ds5.Tables[1].Rows[0]["salesteam"].ToString();
            string rptsalesdate = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-mmm-yyyy");

            string rptagreementdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-mmm-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-mmm-yyyy");

            string rpthandoverdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-mmm-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-mmm-yyyy");

            string rptapartmentprice = Convert.ToDecimal(ds5.Tables[1].Rows[0]["aptprice"]).ToString("#,##0;(#,##0); ");
            string rptcarparking = Convert.ToDecimal(ds5.Tables[1].Rows[0]["carparking"]).ToString("#,##0;(#,##0); ");
            string rptutility = Convert.ToDecimal(ds5.Tables[1].Rows[0]["utility"]).ToString("#,##0;(#,##0); ");
            string rptregistration = Convert.ToDecimal(ds5.Tables[1].Rows[0]["regavat"]).ToString("#,##0;(#,##0);");
            string rptdevelopmentcost = Convert.ToDecimal(ds5.Tables[1].Rows[0]["devcharge"]).ToString("#,##0;(#,##0); ");
            string rptdelcharge = (delcharge > 0) ? ("delay charge: " + delcharge.ToString("#,##0;(#,##0); ")) : "";
            string txtearlyben = (ebenamt > 0) ? ("early benefit: " + ebenamt.ToString("#,##0;(#,##0); ")) : ""; ;
            string printFooter = ASTUtility.Concat(compname, username, printdate);
            string comlogo = new Uri(Server.MapPath(@"~\image\logo" + comcod + ".jpg")).AbsoluteUri;
            var lst = tblins.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptSalClPayDetails", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", comlogo));

            Rpt1.SetParameters(new ReportParameter("aprment", aprment));
            Rpt1.SetParameters(new ReportParameter("carparking", carparking));
            Rpt1.SetParameters(new ReportParameter("utility", utility));
            Rpt1.SetParameters(new ReportParameter("modicharge", modicharge));
            Rpt1.SetParameters(new ReportParameter("regisfee", regisfee));
            Rpt1.SetParameters(new ReportParameter("assciationfee", assciationfee));
            Rpt1.SetParameters(new ReportParameter("transfee", transfee));
            Rpt1.SetParameters(new ReportParameter("rptwelfarefund", rptwelfarefund));
            Rpt1.SetParameters(new ReportParameter("rpttoprice", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("rptOthers", rptothers));
            Rpt1.SetParameters(new ReportParameter("rptcooperative", rptcooperative));
            Rpt1.SetParameters(new ReportParameter("txttoSalevalue", txttosalevalue));
            Rpt1.SetParameters(new ReportParameter("txttoreceivedvalue", txttoreceivedvalue));
            Rpt1.SetParameters(new ReportParameter("txtfcheque", txtfcheque));
            Rpt1.SetParameters(new ReportParameter("txtpcheque", txtpcheque));

            Rpt1.SetParameters(new ReportParameter("txtreconamt", txtreconamt));
            Rpt1.SetParameters(new ReportParameter("txtnetbalance", txtnetbalance));

            Rpt1.SetParameters(new ReportParameter("rpttxtCompanyName", rpttxtcompanyname));
            Rpt1.SetParameters(new ReportParameter("rptcompadd", rptcompadd));
            Rpt1.SetParameters(new ReportParameter("rptunittype", rptunittype));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtdate));
            Rpt1.SetParameters(new ReportParameter("rptcustname", rptcustname));
            Rpt1.SetParameters(new ReportParameter("rptCustAdd", rptcustadd));
            Rpt1.SetParameters(new ReportParameter("rptCustPhone", rptcustphone));
            Rpt1.SetParameters(new ReportParameter("rptpactdesc", rptpactdesc));
            Rpt1.SetParameters(new ReportParameter("rptUnitDesc", rptunitdesc));

            Rpt1.SetParameters(new ReportParameter("rptUsize", rptusize));
            Rpt1.SetParameters(new ReportParameter("rptSalesteam", rptsalesteam));
            Rpt1.SetParameters(new ReportParameter("rptsalesdate", rptsalesdate));
            Rpt1.SetParameters(new ReportParameter("rptagreementdate", rptagreementdate));
            Rpt1.SetParameters(new ReportParameter("rptHandoverdate", rpthandoverdate));

            Rpt1.SetParameters(new ReportParameter("rptapartmentprice", rptapartmentprice));
            Rpt1.SetParameters(new ReportParameter("rptcarparking", rptcarparking));
            Rpt1.SetParameters(new ReportParameter("rptUtility", rptutility));
            Rpt1.SetParameters(new ReportParameter("rptregistration", rptregistration));
            Rpt1.SetParameters(new ReportParameter("rptdevelopmentcost", rptdevelopmentcost));

            Rpt1.SetParameters(new ReportParameter("rptdelcharge", rptdelcharge));
            Rpt1.SetParameters(new ReportParameter("txtearlyben", txtearlyben));

            Rpt1.SetParameters(new ReportParameter("txtuserinfo", printFooter));
            Rpt1.SetParameters(new ReportParameter("title", "Client Ledger"));
            Rpt1.SetParameters(new ReportParameter("receivableTotal", totalRceeivable));

            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintCleintLedgerTropical()
        {
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
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt = ((DataTable)Session["tblCustPayment"]).Copy();


            //Discount 

            DataView dv = dt.DefaultView;
            dv.RowFilter = ("dischk=1");
            DataTable dt1 = dv.ToTable();

            string disinfo = "";
            foreach (DataRow dr1 in dt1.Rows)
            {

                disinfo = "MR" + dr1["mrno"] + "-" + dr1["rmrks"] + ", ";


            }


            disinfo = disinfo.Length > 0 ? ("Discount:" + disinfo.Substring(0, disinfo.Length - 2)) : "";


            //var lst = dt.DataTableToList< C_23_CRR.EClassSales_03.DueCollStatmentRe>();
            var lst = dt.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.EClassClientLedger>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedgerTropical", lst, null, null);
            string usircode = this.ddlCustName.SelectedValue.ToString();
            DataRow[] drc = ((DataTable)ViewState["tblcustomer"]).Select("custid='" + usircode + "'");

            string pactdesc = this.ddlProjectName.SelectedItem.Text.Trim();
            string custname = drc[0]["custname"].ToString();
            string udesc = drc[0]["udesc"].ToString();
            string usize = Convert.ToDouble(drc[0]["usize"].ToString()).ToString("#,##0;(#,##0); ") + " " + drc[0]["unit"].ToString();

           
            double totalreceived = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(paidamt)", "")) ? 0.00 : dt.Compute("Sum(paidamt)", "")));

            double totalscheduleamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(schamt)", "")) ? 0.00 : dt.Compute("Sum(schamt)", "")));

            string balance = (totalscheduleamt - totalreceived).ToString("#,##0;(#,##0); ");

            //  Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtComName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtcomadd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtProject", pactdesc));
            Rpt1.SetParameters(new ReportParameter("txtcustomer", custname));
            Rpt1.SetParameters(new ReportParameter("txtunit", udesc));
            Rpt1.SetParameters(new ReportParameter("txtunitsize", usize));
            Rpt1.SetParameters(new ReportParameter("txtdisinfo", disinfo));
            Rpt1.SetParameters(new ReportParameter("balance", balance));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        private void PrintCleintLedgerLeisure()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetComeCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");

            string CallType = this.ClientCalltype();
            DataSet ds5 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, custid, Date, "", "", "", "", "", "");

            DataTable tblins = this.HiddenSameDate2(ds5.Tables[0]);

            double aprment = Convert.ToDouble(ds5.Tables[1].Rows[0]["aptprice"]);
            double carparking = Convert.ToDouble(ds5.Tables[1].Rows[0]["carparking"]);
            double utility = Convert.ToDouble(ds5.Tables[1].Rows[0]["utility"]);
            double modicharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["adwrk"]);
            double delcharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["delchg"]);
            double regisfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["regavat"]);
            double assciationfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["devcharge"]);
            double transfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["transfee"]);
            double bgcost = Convert.ToDouble(ds5.Tables[1].Rows[0]["bgcost"]);
            //double proadd = Convert.ToDouble(ds5.Tables[1].Rows[0]["proadd"]);




            // rdlc start

            string rptwelfarefund = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");
            string rptothers = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");
            string rpttoprice = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            string rptcooperative = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");
            string rptdiscount = Convert.ToDecimal(ds5.Tables[1].Rows[0]["discount"]).ToString("#,##0;(#,##0); ");


            DataTable dtsum = ds5.Tables[2];
            double tsalevalue = Convert.ToDouble(ds5.Tables[1].Rows[0]["acprice"]);
            double treceived = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(paidamt)", "")) ? 0.00
                    : tblins.Compute("Sum(paidamt)", "")));

            double fcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["fcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["fcheque"]) : 0.00;
            double retcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["retcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["retcheque"]) : 0.00;
            double pcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["pcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["pcheque"]) : 0.00;
            double reconamt = treceived - (fcheque + retcheque + pcheque);
            double netbal = tsalevalue - reconamt;



            string rptunittype = ds5.Tables[1].Rows[0]["unittype"].ToString();
            string txtdate = "Print date: " + this.txtDate.Text.Trim();
            string rptcustname = ds5.Tables[1].Rows[0]["name"].ToString();
            string rptcustadd = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            string rptcustphone = ds5.Tables[1].Rows[0]["telephone"].ToString();
            string rptpactdesc = ds5.Tables[1].Rows[0]["projectname"].ToString();

            string rptunitdesc = ds5.Tables[1].Rows[0]["aptname"].ToString();
            string rptusize = ds5.Tables[1].Rows[0]["aptsize"].ToString();
            string rptsalesteam = ds5.Tables[1].Rows[0]["salesteam"].ToString();
            string rptsalesdate = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy");
            string rptagreementdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
            string rpthandoverdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");

            string rptdelcharge = (delcharge > 0) ? delcharge.ToString("#,##0;(#,##0); ") : "";

            //string txtearlyben = (ebenamt > 0) ? ("early benefit: " + ebenamt.ToString("#,##0;(#,##0); ")) : ""; ;

            string printFooter = ASTUtility.Concat(compname, username, printdate);
            string comlogo = new Uri(Server.MapPath(@"~\image\logo" + comcod + ".jpg")).AbsoluteUri;

            var lst = tblins.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedgerLeisure02", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", comlogo));
            Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            Rpt1.SetParameters(new ReportParameter("Compadd", comadd));
            Rpt1.SetParameters(new ReportParameter("title", "Client Ledger"));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtdate));

            Rpt1.SetParameters(new ReportParameter("rptcustname", rptcustname));
            Rpt1.SetParameters(new ReportParameter("rptCustAdd", rptcustadd));
            Rpt1.SetParameters(new ReportParameter("rptCustPhone", rptcustphone));
            Rpt1.SetParameters(new ReportParameter("rptpactdesc", rptpactdesc));
            Rpt1.SetParameters(new ReportParameter("rptUnitDesc", rptunitdesc));
            Rpt1.SetParameters(new ReportParameter("rptUsize", rptusize));
            Rpt1.SetParameters(new ReportParameter("rptSalesteam", rptsalesteam));
            Rpt1.SetParameters(new ReportParameter("rptsalesdate", rptsalesdate));
            Rpt1.SetParameters(new ReportParameter("rptagreementdate", rptagreementdate));
            Rpt1.SetParameters(new ReportParameter("rptHandoverdate", rpthandoverdate));

            Rpt1.SetParameters(new ReportParameter("txtbgcost", bgcost.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptapartmentprice", aprment.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcarparking", carparking.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptUtility", utility.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcooperative", rptcooperative));
            Rpt1.SetParameters(new ReportParameter("regisfee", regisfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("transfee", transfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("modicharge", modicharge.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("welfarefund", rptwelfarefund));
            Rpt1.SetParameters(new ReportParameter("rptOthers", rptothers));
            Rpt1.SetParameters(new ReportParameter("receivableTotal", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("lessdisamt", rptdiscount));

            Rpt1.SetParameters(new ReportParameter("txttovalueamt", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("txttoreceived", treceived.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtfcheque", fcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtrcheque", retcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtpcheque", pcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetencash", reconamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetbalance", netbal.ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintCleintLedgerRupayan()
        {

            // rdlc start

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetComeCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");

            string CallType = this.ClientCalltype();
            DataSet ds5 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, custid, Date, "", "", "", "", "", "");

            DataTable tblins = this.HiddenSameDate2(ds5.Tables[0]);

            double aprment = Convert.ToDouble(ds5.Tables[1].Rows[0]["aptprice"]);
            double carparking = Convert.ToDouble(ds5.Tables[1].Rows[0]["carparking"]);
            double utility = Convert.ToDouble(ds5.Tables[1].Rows[0]["utility"]);
            double modicharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["adwrk"]);
            double delcharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["delchg"]);
            double regisfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["regavat"]);
            double assciationfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["devcharge"]);
            double transfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["transfee"]);
            double bgcost = Convert.ToDouble(ds5.Tables[1].Rows[0]["bgcost"]);
            //double proadd = Convert.ToDouble(ds5.Tables[1].Rows[0]["proadd"]);




            // rdlc start

            string rptwelfarefund = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");
            string rptothers = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");
            string rpttoprice = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            string rptcooperative = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");
            string rptdiscount = Convert.ToDecimal(ds5.Tables[1].Rows[0]["discount"]).ToString("#,##0;(#,##0); ");


            DataTable dtsum = ds5.Tables[2];
            double tsalevalue = Convert.ToDouble(ds5.Tables[1].Rows[0]["acprice"]);
            double treceived = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(paidamt)", "")) ? 0.00
                    : tblins.Compute("Sum(paidamt)", "")));

            double fcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["fcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["fcheque"]) : 0.00;
            double retcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["retcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["retcheque"]) : 0.00;
            double pcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["pcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["pcheque"]) : 0.00;
            double reconamt = treceived - (fcheque + retcheque + pcheque);
            double netbal = tsalevalue - reconamt;



            string rptunittype = ds5.Tables[1].Rows[0]["unittype"].ToString();
            string txtdate = "Print date: " + this.txtDate.Text.Trim();
            string rptcustname = ds5.Tables[1].Rows[0]["name"].ToString();
            string rptcustadd = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            string rptcustphone = ds5.Tables[1].Rows[0]["telephone"].ToString();
            string email = ds5.Tables[1].Rows[0]["custemail"].ToString();
            string rptpactdesc = ds5.Tables[1].Rows[0]["projectname"].ToString();
            string projadd = ds5.Tables[1].Rows[0]["proadd"].ToString();


            string rptunitdesc = ds5.Tables[1].Rows[0]["aptname"].ToString();
            string rptusize = ds5.Tables[1].Rows[0]["aptsize"].ToString();
            string rptsalesteam = ds5.Tables[1].Rows[0]["salesteam"].ToString();
            string rptsalesdate = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy");
            string rptagreementdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
            string rpthandoverdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");

            string rptdelcharge = (delcharge > 0) ? delcharge.ToString("#,##0;(#,##0); ") : "";

            //string txtearlyben = (ebenamt > 0) ? ("early benefit: " + ebenamt.ToString("#,##0;(#,##0); ")) : ""; ;

            string printFooter = ASTUtility.Concat(compname, username, printdate);
            string comlogo = new Uri(Server.MapPath(@"~\image\logo" + comcod + ".jpg")).AbsoluteUri;

            var lst = tblins.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedgerRup02", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            Rpt1.SetParameters(new ReportParameter("Compadd", comadd));
            Rpt1.SetParameters(new ReportParameter("title", "Client Ledger"));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtdate));

            Rpt1.SetParameters(new ReportParameter("rptcustname", rptcustname));
            Rpt1.SetParameters(new ReportParameter("rptCustAdd", rptcustadd));
            Rpt1.SetParameters(new ReportParameter("rptCustPhone", rptcustphone));
            Rpt1.SetParameters(new ReportParameter("rptCustemail", email));

            Rpt1.SetParameters(new ReportParameter("rptpactdesc", rptpactdesc));
            Rpt1.SetParameters(new ReportParameter("rptPrjadd", projadd));
            Rpt1.SetParameters(new ReportParameter("rptUnitDesc", rptunitdesc));
            Rpt1.SetParameters(new ReportParameter("rptUsize", rptusize));
            Rpt1.SetParameters(new ReportParameter("rptSalesteam", rptsalesteam));
            Rpt1.SetParameters(new ReportParameter("rptsalesdate", rptsalesdate));
            Rpt1.SetParameters(new ReportParameter("rptagreementdate", rptagreementdate));
            Rpt1.SetParameters(new ReportParameter("rptHandoverdate", rpthandoverdate));

            Rpt1.SetParameters(new ReportParameter("txtbgcost", bgcost.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptapartmentprice", aprment.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcarparking", carparking.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptUtility", utility.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcooperative", rptcooperative));
            Rpt1.SetParameters(new ReportParameter("regisfee", regisfee.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("transfee", transfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("modicharge", modicharge.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("welfarefund", rptwelfarefund));
            Rpt1.SetParameters(new ReportParameter("rptOthers", rptothers));
            Rpt1.SetParameters(new ReportParameter("receivableTotal", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("lessdisamt", rptdiscount));

            Rpt1.SetParameters(new ReportParameter("txttovalueamt", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("txttoreceived", treceived.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtfcheque", fcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtrcheque", retcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtpcheque", pcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetencash", reconamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetbalance", netbal.ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", comlogo));

            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintCleintLedgerManama()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetComeCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");

            string CallType = this.ClientCalltype();
            string length = comcod == "3348" ? "length" : "";
            DataSet ds5 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, custid, Date, length, "", "", "", "", "");

            DataTable tblins = this.HiddenSameDate2(ds5.Tables[0]);

            DataTable tbl6 = ds5.Tables[0];

            DataView dv1 = tbl6.DefaultView;
            dv1.RowFilter = "grp like 'AA' ";
            tbl6 = dv1.ToTable();


            double Schamt = Convert.ToDouble((Convert.IsDBNull(tbl6.Compute("Sum(schamt)", "")) ? 0.00 : tbl6.Compute("Sum(schamt)", "")));
            double rcvamt = Convert.ToDouble((Convert.IsDBNull(tbl6.Compute("Sum(paidamt)", "")) ? 0.00 : tbl6.Compute("Sum(paidamt)", "")));
            double baldues = Convert.ToDouble((Convert.IsDBNull(tbl6.Compute("Sum(balamt)", "")) ? 0.00 : tbl6.Compute("Sum(balamt)", "")));        


            double balamt = Schamt - rcvamt;
            double tobuildingamt = 0;


            LocalReport Rpt1 = new LocalReport();
            var lst1 = tblins.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.EClassClientLedger>();
            if (this.chkConsolidate.Checked)
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedgerManama", lst1, null, null);
            }
            else
            {
                var lst2 = ds5.Tables[4].DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.EClassRevenue>();
                var lst3 = ds5.Tables[5].DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.EClassRevenue>();
                tobuildingamt = lst2.Sum(l => l.uamt);
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedgerManama", lst1, lst2, lst3);
            }
            DataTable dtsum = ds5.Tables[2];
            //double tsalevalue = Convert.ToDouble(ds5.Tables[1].Rows[0]["acprice"]);
            //double treceived = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(paidamt)", "")) ? 0.00
            //        : tblins.Compute("Sum(paidamt)", "")));

            //// May be Problem
            //double fcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["fcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["fcheque"]) : 0.00;
            //double retcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["retcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["retcheque"]) : 0.00;
            //double pcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["pcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["pcheque"]) : 0.00;
            //double reconamt = treceived - (fcheque + retcheque + pcheque);
            //double netbal = tsalevalue - reconamt;
            string pactdesc = "Project : " + this.ddlProjectName.SelectedItem.Text.Trim();





            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comnam));
            Rpt1.SetParameters(new ReportParameter("compadd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtprojectname", pactdesc));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Print Date: " + this.txtDate.Text.Trim()));


            Rpt1.SetParameters(new ReportParameter("txtcustcode", ds5.Tables[1].Rows[0]["custcode"].ToString()));
            Rpt1.SetParameters(new ReportParameter("custname", ds5.Tables[1].Rows[0]["name"].ToString()));
            Rpt1.SetParameters(new ReportParameter("CustAdd", ds5.Tables[1].Rows[0]["peraddress"].ToString()));
            Rpt1.SetParameters(new ReportParameter("CustPhone", ds5.Tables[1].Rows[0]["telephone"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtEmail", ds5.Tables[1].Rows[0]["custemail"].ToString()));


            Rpt1.SetParameters(new ReportParameter("txtprojectcode", ds5.Tables[1].Rows[0]["procode"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtproadd", ds5.Tables[1].Rows[0]["proadd"].ToString()));
            Rpt1.SetParameters(new ReportParameter("UnitDesc", ds5.Tables[1].Rows[0]["aptname"].ToString()));
            Rpt1.SetParameters(new ReportParameter("usize", ds5.Tables[1].Rows[0]["aptsize"].ToString()));
            Rpt1.SetParameters(new ReportParameter("Salesteam", ds5.Tables[1].Rows[0]["salesteam"].ToString()));
            Rpt1.SetParameters(new ReportParameter("salesdate", Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("agreementdate", (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("Handoverdate", (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy")));

            Rpt1.SetParameters(new ReportParameter("TotalBuildingAmt", tobuildingamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtSchamt", Schamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtrcvamt", rcvamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtbalamt", balamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtbaldues", baldues.ToString("#,##0;(#,##0); ")));



            //Rpt1.SetParameters(new ReportParameter("txttoSalevalue", tsalevalue.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("txttoreceivedvalue", treceived.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("txtfcheque", fcheque.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("txtretcheque", retcheque.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("txtpcheque", pcheque.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("txtreconamt", reconamt.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("txtnetbalance", netbal.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtDevelopedby", ASTUtility.Cominformation()));



            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintCleintLedgergen()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetComeCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");

            string CallType = this.ClientCalltype();
            DataSet ds5 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, custid, Date, "", "", "", "", "", "");

            DataTable tblins = this.HiddenSameDate2(ds5.Tables[0]);

            double aprment = Convert.ToDouble(ds5.Tables[1].Rows[0]["aptprice"]);
            double carparking = Convert.ToDouble(ds5.Tables[1].Rows[0]["carparking"]);
            double utility = Convert.ToDouble(ds5.Tables[1].Rows[0]["utility"]);
            double modicharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["adwrk"]);
            double delcharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["delchg"]);
            double regisfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["regavat"]);
            double assciationfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["devcharge"]);
            double transfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["transfee"]);
            double bgcost = Convert.ToDouble(ds5.Tables[1].Rows[0]["bgcost"]);
            //double proadd = Convert.ToDouble(ds5.Tables[1].Rows[0]["proadd"]);




            // rdlc start

            string rptwelfarefund = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");
            string rptothers = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");
            string rpttoprice = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            string rptcooperative = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");
            string rptdiscount = Convert.ToDecimal(ds5.Tables[1].Rows[0]["discount"]).ToString("#,##0;(#,##0); ");


            DataTable dtsum = ds5.Tables[2];
            double tsalevalue = Convert.ToDouble(ds5.Tables[1].Rows[0]["acprice"]);
            double treceived = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(paidamt)", "")) ? 0.00
                    : tblins.Compute("Sum(paidamt)", "")));

            double fcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["fcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["fcheque"]) : 0.00;
            double retcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["retcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["retcheque"]) : 0.00;
            double pcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["pcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["pcheque"]) : 0.00;
            double reconamt = treceived - (fcheque + retcheque + pcheque);
            double netbal = tsalevalue - reconamt;



            string rptunittype = ds5.Tables[1].Rows[0]["unittype"].ToString();
            string txtdate = "Print date: " + this.txtDate.Text.Trim();
            string rptcustname = ds5.Tables[1].Rows[0]["name"].ToString();
            string rptcustadd = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            string rptcustphone = ds5.Tables[1].Rows[0]["telephone"].ToString();
            string rptpactdesc = ds5.Tables[1].Rows[0]["projectname"].ToString();
            string projadd = ds5.Tables[1].Rows[0]["proadd"].ToString();

            string rptunitdesc = ds5.Tables[1].Rows[0]["aptname"].ToString();
            string rptusize = ds5.Tables[1].Rows[0]["aptsize"].ToString();
            string rptsalesteam = ds5.Tables[1].Rows[0]["salesteam"].ToString();
            string rptsalesdate = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy");
            string rptagreementdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
            string rpthandoverdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");

            string rptdelcharge = (delcharge > 0) ? delcharge.ToString("#,##0;(#,##0); ") : "";

            //string txtearlyben = (ebenamt > 0) ? ("early benefit: " + ebenamt.ToString("#,##0;(#,##0); ")) : ""; ;

            string printFooter = ASTUtility.Concat(compname, username, printdate);
            string comlogo = new Uri(Server.MapPath(@"~\image\logo" + comcod + ".jpg")).AbsoluteUri;

            var lst = tblins.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedger02", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", comlogo));
            Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            Rpt1.SetParameters(new ReportParameter("Compadd", comadd));
            Rpt1.SetParameters(new ReportParameter("title", "Client Ledger"));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtdate));

            Rpt1.SetParameters(new ReportParameter("rptcustname", rptcustname));
            Rpt1.SetParameters(new ReportParameter("rptCustAdd", rptcustadd));
            Rpt1.SetParameters(new ReportParameter("rptCustPhone", rptcustphone));
            Rpt1.SetParameters(new ReportParameter("rptpactdesc", rptpactdesc));
            Rpt1.SetParameters(new ReportParameter("projAddress", projadd));
            Rpt1.SetParameters(new ReportParameter("rptUnitDesc", rptunitdesc));
            Rpt1.SetParameters(new ReportParameter("rptUsize", rptusize));
            Rpt1.SetParameters(new ReportParameter("rptSalesteam", rptsalesteam));
            Rpt1.SetParameters(new ReportParameter("rptsalesdate", rptsalesdate));
            Rpt1.SetParameters(new ReportParameter("rptagreementdate", rptagreementdate));
            Rpt1.SetParameters(new ReportParameter("rptHandoverdate", rpthandoverdate));

            Rpt1.SetParameters(new ReportParameter("txtbgcost", bgcost.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptapartmentprice", aprment.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcarparking", carparking.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptUtility", utility.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcooperative", rptcooperative));
            Rpt1.SetParameters(new ReportParameter("regisfee", regisfee.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("transfee", transfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("modicharge", modicharge.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("welfarefund", rptwelfarefund));
            Rpt1.SetParameters(new ReportParameter("rptOthers", rptothers));
            Rpt1.SetParameters(new ReportParameter("receivableTotal", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("lessdisamt", rptdiscount));

            Rpt1.SetParameters(new ReportParameter("txttovalueamt", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("txttoreceived", treceived.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtfcheque", fcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtrcheque", retcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtpcheque", pcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetencash", reconamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetbalance", netbal.ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            // rdlc  end 



            //double aprment = Convert.ToDouble(ds5.Tables[1].Rows[0]["aptprice"]);
            //double carparking = Convert.ToDouble(ds5.Tables[1].Rows[0]["carparking"]);
            //double utility = Convert.ToDouble(ds5.Tables[1].Rows[0]["utility"]);
            //double modicharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["adwrk"]);
            //double delcharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["delchg"]);
            //double regisfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["regavat"]);
            //double assciationfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["devcharge"]);
            //double transfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["transfee"]);
            ////double proadd = Convert.ToDouble(ds5.Tables[1].Rows[0]["proadd"]);


            //ReportDocument rptStatus = new ReportDocument();

            //rptStatus = new RealERPRPT.R_23_CR.RptClientLedger();
            //TextObject rptwelfarefund = rptStatus.ReportDefinition.ReportObjects["welfarefund"] as TextObject;
            //rptwelfarefund.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");

            //TextObject rptOthers = rptStatus.ReportDefinition.ReportObjects["Others"] as TextObject;
            //rptOthers.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");

            //TextObject rpttoprice = rptStatus.ReportDefinition.ReportObjects["toprice"] as TextObject;
            //rpttoprice.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            //TextObject rptbudgcost = rptStatus.ReportDefinition.ReportObjects["bgdcost"] as TextObject;
            //rptbudgcost.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["bgcost"]).ToString("#,##0;(#,##0); ");
            //TextObject rptcooperative = rptStatus.ReportDefinition.ReportObjects["coopcost"] as TextObject;
            //rptcooperative.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");

            //DataTable dtsum = ds5.Tables[2];
            //double tsalevalue = Convert.ToDouble(ds5.Tables[1].Rows[0]["acprice"]);
            //double treceived = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(paidamt)", "")) ? 0.00
            //        : tblins.Compute("Sum(paidamt)", "")));
            //// May be Problem
            //double fcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["fcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["fcheque"]) : 0.00;
            //double retcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["retcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["retcheque"]) : 0.00;
            //double pcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["pcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["pcheque"]) : 0.00;
            //double reconamt = treceived - (fcheque + retcheque + pcheque);
            //double netbal = tsalevalue - reconamt;

            //TextObject txttoSalevalue = rptStatus.ReportDefinition.ReportObjects["txttoSalevalue"] as TextObject;
            //txttoSalevalue.Text = tsalevalue.ToString("#,##0;(#,##0); ");
            //TextObject txttoreceivedvalue = rptStatus.ReportDefinition.ReportObjects["txttoreceivedvalue"] as TextObject;
            //txttoreceivedvalue.Text = treceived.ToString("#,##0;(#,##0); ");
            //TextObject txtfcheque = rptStatus.ReportDefinition.ReportObjects["txtfcheque"] as TextObject;
            //txtfcheque.Text = fcheque.ToString("#,##0;(#,##0); ");
            //TextObject txtretcheque = rptStatus.ReportDefinition.ReportObjects["txtretcheque"] as TextObject;
            //txtretcheque.Text = retcheque.ToString("#,##0;(#,##0); ");
            //TextObject txtpcheque = rptStatus.ReportDefinition.ReportObjects["txtpcheque"] as TextObject;
            //txtpcheque.Text = pcheque.ToString("#,##0;(#,##0); ");
            //TextObject txtreconamt = rptStatus.ReportDefinition.ReportObjects["txtreconamt"] as TextObject;
            //txtreconamt.Text = reconamt.ToString("#,##0;(#,##0); ");
            //TextObject txtnetbalance = rptStatus.ReportDefinition.ReportObjects["txtnetbalance"] as TextObject;
            //txtnetbalance.Text = netbal.ToString("#,##0;(#,##0); ");


            //TextObject rptdiscount = rptStatus.ReportDefinition.ReportObjects["discount"] as TextObject;
            //rptdiscount.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["discount"]).ToString("#,##0;(#,##0); ");




            //TextObject rpttxtCompanyName = rptStatus.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rpttxtCompanyName.Text = comnam;
            //TextObject rptcompadd = rptStatus.ReportDefinition.ReportObjects["compadd"] as TextObject;
            //rptcompadd.Text = comadd;

            //TextObject txtDate = rptStatus.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtDate.Text = "Print Date: " + this.txtDate.Text.Trim();
            //TextObject rptcustname = rptStatus.ReportDefinition.ReportObjects["custname"] as TextObject;
            //rptcustname.Text = ds5.Tables[1].Rows[0]["name"].ToString();
            //TextObject rptCustAdd = rptStatus.ReportDefinition.ReportObjects["CustAdd"] as TextObject;
            //rptCustAdd.Text = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            //TextObject rptCustPhone = rptStatus.ReportDefinition.ReportObjects["CustPhone"] as TextObject;
            //rptCustPhone.Text = ds5.Tables[1].Rows[0]["telephone"].ToString();
            //TextObject rptpactdesc = rptStatus.ReportDefinition.ReportObjects["pactdesc"] as TextObject;
            //rptpactdesc.Text = ds5.Tables[1].Rows[0]["projectname"].ToString();
            //TextObject txtproadd = rptStatus.ReportDefinition.ReportObjects["txtproadd"] as TextObject;
            //txtproadd.Text = ds5.Tables[1].Rows[0]["proadd"].ToString();


            //TextObject rptUnitDesc = rptStatus.ReportDefinition.ReportObjects["UnitDesc"] as TextObject;
            //rptUnitDesc.Text = ds5.Tables[1].Rows[0]["aptname"].ToString();
            //TextObject rptUsize = rptStatus.ReportDefinition.ReportObjects["usize"] as TextObject;
            //rptUsize.Text = ds5.Tables[1].Rows[0]["aptsize"].ToString();

            //TextObject rptSalesteam = rptStatus.ReportDefinition.ReportObjects["Salesteam"] as TextObject;
            //rptSalesteam.Text = ds5.Tables[1].Rows[0]["salesteam"].ToString();
            //TextObject rptsalesdate = rptStatus.ReportDefinition.ReportObjects["salesdate"] as TextObject;
            //rptsalesdate.Text = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy");
            //TextObject rptagreementdate = rptStatus.ReportDefinition.ReportObjects["agreementdate"] as TextObject;
            //rptagreementdate.Text = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
            //TextObject rptHandoverdate = rptStatus.ReportDefinition.ReportObjects["Handoverdate"] as TextObject;
            //rptHandoverdate.Text = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");

            //TextObject rptapartmentprice = rptStatus.ReportDefinition.ReportObjects["apartmentprice"] as TextObject;
            //rptapartmentprice.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["aptprice"]).ToString("#,##0;(#,##0); ");
            //TextObject rptcarparking = rptStatus.ReportDefinition.ReportObjects["carparking"] as TextObject;
            //rptcarparking.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["carparking"]).ToString("#,##0;(#,##0); ");
            //TextObject rptUtility = rptStatus.ReportDefinition.ReportObjects["Utility"] as TextObject;
            //rptUtility.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["utility"]).ToString("#,##0;(#,##0); ");

            //TextObject rptregistration = rptStatus.ReportDefinition.ReportObjects["registration"] as TextObject;
            //rptregistration.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["regavat"]).ToString("#,##0;(#,##0);");
            //TextObject rptdevelopmentcost = rptStatus.ReportDefinition.ReportObjects["developmentcost"] as TextObject;
            //rptdevelopmentcost.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["devcharge"]).ToString("#,##0;(#,##0); ");




            ////TextObject rptaccost = rptStatus.ReportDefinition.ReportObjects["accost"] as TextObject;
            ////rptaccost.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            ////---------



            //// Summary Total






            //TextObject txtuserinfo = rptStatus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptStatus.SetDataSource(tblins);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptStatus.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptStatus;
            ////this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            ////                  this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        } 
        private void PrintCleintLedgerLanco()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetComeCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");

            string CallType = this.ClientCalltype();
            DataSet ds5 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, custid, Date, "", "", "", "", "", "");

            DataTable tblins = this.HiddenSameDate2(ds5.Tables[0]);

            double aprment = Convert.ToDouble(ds5.Tables[1].Rows[0]["aptprice"]);
            double carparking = Convert.ToDouble(ds5.Tables[1].Rows[0]["carparking"]);
            double utility = Convert.ToDouble(ds5.Tables[1].Rows[0]["utility"]);
            double modicharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["adwrk"]);
            double delcharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["delchg"]);
            double regisfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["regavat"]);
            double assciationfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["devcharge"]);
            double transfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["transfee"]);
            double bgcost = Convert.ToDouble(ds5.Tables[1].Rows[0]["bgcost"]);
            //double proadd = Convert.ToDouble(ds5.Tables[1].Rows[0]["proadd"]);




            // rdlc start

            string rptwelfarefund = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");
            string rptothers = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");
            string rpttoprice = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            string rptcooperative = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");
            string rptdiscount = Convert.ToDecimal(ds5.Tables[1].Rows[0]["discount"]).ToString("#,##0;(#,##0); ");


            DataTable dtsum = ds5.Tables[2];
            double tsalevalue = Convert.ToDouble(ds5.Tables[1].Rows[0]["acprice"]);
            double treceived = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(paidamt)", "")) ? 0.00
                    : tblins.Compute("Sum(paidamt)", "")));

            double fcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["fcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["fcheque"]) : 0.00;
            double retcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["retcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["retcheque"]) : 0.00;
            double pcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["pcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["pcheque"]) : 0.00;
            double reconamt = treceived - (fcheque + retcheque + pcheque);
            double netbal = tsalevalue - reconamt;



            string rptunittype = ds5.Tables[1].Rows[0]["unittype"].ToString();
            string txtdate = "Print date: " + this.txtDate.Text.Trim();
            string rptcustname = ds5.Tables[1].Rows[0]["name"].ToString();
            string rptcustadd = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            string rptcustphone = ds5.Tables[1].Rows[0]["telephone"].ToString();
            string rptpactdesc = ds5.Tables[1].Rows[0]["projectname"].ToString();
            string projadd = ds5.Tables[1].Rows[0]["proadd"].ToString();

            string rptunitdesc = ds5.Tables[1].Rows[0]["aptname"].ToString();
            string rptusize = ds5.Tables[1].Rows[0]["aptsize"].ToString();
            string rptsalesteam = ds5.Tables[1].Rows[0]["salesteam"].ToString();
            string rptsalesdate = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy");
            string rptagreementdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
            string rpthandoverdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");

            string rptdelcharge = (delcharge > 0) ? delcharge.ToString("#,##0;(#,##0); ") : "";

            //string txtearlyben = (ebenamt > 0) ? ("early benefit: " + ebenamt.ToString("#,##0;(#,##0); ")) : ""; ;

            string printFooter = ASTUtility.Concat(compname, username, printdate);
            string comlogo = new Uri(Server.MapPath(@"~\image\logo" + comcod + ".jpg")).AbsoluteUri;

            var lst = tblins.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedger02Lanco", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", comlogo));
            Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            Rpt1.SetParameters(new ReportParameter("Compadd", comadd));
            Rpt1.SetParameters(new ReportParameter("title", "Client Ledger"));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtdate));

            Rpt1.SetParameters(new ReportParameter("rptcustname", rptcustname));
            Rpt1.SetParameters(new ReportParameter("rptCustAdd", rptcustadd));
            Rpt1.SetParameters(new ReportParameter("rptCustPhone", rptcustphone));
            Rpt1.SetParameters(new ReportParameter("rptpactdesc", rptpactdesc));
            Rpt1.SetParameters(new ReportParameter("projAddress", projadd));
            Rpt1.SetParameters(new ReportParameter("rptUnitDesc", rptunitdesc));
            Rpt1.SetParameters(new ReportParameter("rptUsize", rptusize));
            Rpt1.SetParameters(new ReportParameter("rptSalesteam", rptsalesteam));
            Rpt1.SetParameters(new ReportParameter("rptsalesdate", rptsalesdate));
            Rpt1.SetParameters(new ReportParameter("rptagreementdate", rptagreementdate));
            Rpt1.SetParameters(new ReportParameter("rptHandoverdate", rpthandoverdate));

            Rpt1.SetParameters(new ReportParameter("txtbgcost", bgcost.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptapartmentprice", aprment.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcarparking", carparking.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptUtility", utility.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcooperative", rptcooperative));
            Rpt1.SetParameters(new ReportParameter("regisfee", regisfee.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("transfee", transfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("modicharge", modicharge.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("welfarefund", rptwelfarefund));
            Rpt1.SetParameters(new ReportParameter("rptOthers", rptothers));
            Rpt1.SetParameters(new ReportParameter("receivableTotal", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("lessdisamt", rptdiscount));

            Rpt1.SetParameters(new ReportParameter("txttovalueamt", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("txttoreceived", treceived.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtfcheque", fcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtrcheque", retcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtpcheque", pcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetencash", reconamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetbalance", netbal.ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
           

        }
        private void PrintCleintLedgerAssure()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetComeCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");

            string CallType = this.ClientCalltype();
            DataSet ds5 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, custid, Date, "", "", "", "", "", "");

            DataTable tblins = this.HiddenSameDate2(ds5.Tables[0]);

            double aprment = Convert.ToDouble(ds5.Tables[1].Rows[0]["aptprice"]);
            double carparking = Convert.ToDouble(ds5.Tables[1].Rows[0]["carparking"]);
            double utility = Convert.ToDouble(ds5.Tables[1].Rows[0]["utility"]);
            double modicharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["adwrk"]);
            double delcharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["delchg"]);
            double regisfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["regavat"]);
            double assciationfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["devcharge"]);
            double transfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["transfee"]);
            double bgcost = Convert.ToDouble(ds5.Tables[1].Rows[0]["bgcost"]);
            //double proadd = Convert.ToDouble(ds5.Tables[1].Rows[0]["proadd"]);

            // rdlc start

            string rptwelfarefund = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");
            string rptothers = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");
            string rpttoprice = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            string rptcooperative = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");
            string rptdiscount = Convert.ToDecimal(ds5.Tables[1].Rows[0]["discount"]).ToString("#,##0;(#,##0); ");


            DataTable dtsum = ds5.Tables[2];
            double tsalevalue = Convert.ToDouble(ds5.Tables[1].Rows[0]["acprice"]);
            double treceived = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(paidamt)", "")) ? 0.00
                    : tblins.Compute("Sum(paidamt)", "")));

            double fcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["fcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["fcheque"]) : 0.00;
            double retcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["retcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["retcheque"]) : 0.00;
            double pcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["pcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["pcheque"]) : 0.00;
            double reconamt = treceived - (fcheque + retcheque + pcheque);
            double netbal = tsalevalue - reconamt;



            string rptunittype = ds5.Tables[1].Rows[0]["unittype"].ToString();
            string txtdate = "Print date: " + this.txtDate.Text.Trim();
            string rptcustname = ds5.Tables[1].Rows[0]["name"].ToString();
            string rptcustadd = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            string rptcustphone = ds5.Tables[1].Rows[0]["telephone"].ToString();
            string rptpactdesc = ds5.Tables[1].Rows[0]["projectname"].ToString();
            string projadd = ds5.Tables[1].Rows[0]["proadd"].ToString();

            string rptunitdesc = ds5.Tables[1].Rows[0]["aptname"].ToString();
            string rptusize = ds5.Tables[1].Rows[0]["aptsize"].ToString();
            string rptsalesteam = ds5.Tables[1].Rows[0]["salesteam"].ToString();
            string rptsalesdate = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy");
            string rptagreementdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
            string rpthandoverdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");

            string rptdelcharge = (delcharge > 0) ? delcharge.ToString("#,##0;(#,##0); ") : "";

            //string txtearlyben = (ebenamt > 0) ? ("early benefit: " + ebenamt.ToString("#,##0;(#,##0); ")) : ""; ;

            string printFooter = ASTUtility.Concat(compname, username, printdate);
            string comlogo = new Uri(Server.MapPath(@"~\image\logo" + comcod + ".jpg")).AbsoluteUri;

            var lst = tblins.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedgerAssure", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", comlogo));
            Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            Rpt1.SetParameters(new ReportParameter("Compadd", comadd));
            Rpt1.SetParameters(new ReportParameter("title", "Client Ledger"));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtdate));

            Rpt1.SetParameters(new ReportParameter("rptcustname", rptcustname));
            Rpt1.SetParameters(new ReportParameter("rptCustAdd", rptcustadd));
            Rpt1.SetParameters(new ReportParameter("rptCustPhone", rptcustphone));
            Rpt1.SetParameters(new ReportParameter("rptpactdesc", rptpactdesc));
            Rpt1.SetParameters(new ReportParameter("projAddress", projadd));
            Rpt1.SetParameters(new ReportParameter("rptUnitDesc", rptunitdesc));
            Rpt1.SetParameters(new ReportParameter("rptUsize", rptusize));
            Rpt1.SetParameters(new ReportParameter("rptSalesteam", rptsalesteam));
            Rpt1.SetParameters(new ReportParameter("rptsalesdate", rptsalesdate));
            Rpt1.SetParameters(new ReportParameter("rptagreementdate", rptagreementdate));
            Rpt1.SetParameters(new ReportParameter("rptHandoverdate", rpthandoverdate));

            Rpt1.SetParameters(new ReportParameter("txtbgcost", bgcost.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptapartmentprice", aprment.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcarparking", carparking.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptUtility", utility.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcooperative", rptcooperative));
            Rpt1.SetParameters(new ReportParameter("regisfee", regisfee.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("transfee", transfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("modicharge", modicharge.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("welfarefund", rptwelfarefund));
            Rpt1.SetParameters(new ReportParameter("rptOthers", rptothers));
            Rpt1.SetParameters(new ReportParameter("receivableTotal", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("lessdisamt", rptdiscount));

            Rpt1.SetParameters(new ReportParameter("txttovalueamt", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("txttoreceived", treceived.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtfcheque", fcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtrcheque", retcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtpcheque", pcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetencash", reconamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetbalance", netbal.ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";          

        }
        private void PrintCleintLedgerCube()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetComeCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlCustName.SelectedValue.ToString();
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");

            string CallType = this.ClientCalltype();
            DataSet ds5 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, custid, Date, "", "", "", "", "", "");

            DataTable tblins = this.HiddenSameDate2(ds5.Tables[0]);

            double aprment = Convert.ToDouble(ds5.Tables[1].Rows[0]["aptprice"]);
            double carparking = Convert.ToDouble(ds5.Tables[1].Rows[0]["carparking"]);
            double utility = Convert.ToDouble(ds5.Tables[1].Rows[0]["utility"]);
            double modicharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["adwrk"]);
            double delcharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["delchg"]);
            double regisfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["regavat"]);
            double assciationfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["devcharge"]);
            double transfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["transfee"]);
            double bgcost = Convert.ToDouble(ds5.Tables[1].Rows[0]["bgcost"]);
            //double proadd = Convert.ToDouble(ds5.Tables[1].Rows[0]["proadd"]);


            // rdlc start

            string rptwelfarefund = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");
            string rptothers = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");
            string rpttoprice = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            string rptcooperative = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");
            string rptdiscount = Convert.ToDecimal(ds5.Tables[1].Rows[0]["discount"]).ToString("#,##0;(#,##0); ");


            DataTable dtsum = ds5.Tables[2];
            double tsalevalue = Convert.ToDouble(ds5.Tables[1].Rows[0]["acprice"]);
            double treceived = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(paidamt)", "")) ? 0.00
                    : tblins.Compute("Sum(paidamt)", "")));

            double fcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["fcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["fcheque"]) : 0.00;
            double retcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["retcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["retcheque"]) : 0.00;
            double pcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["pcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["pcheque"]) : 0.00;
            double reconamt = treceived - (fcheque + retcheque + pcheque);
            double netbal = tsalevalue - reconamt;



            string rptunittype = ds5.Tables[1].Rows[0]["unittype"].ToString();
            string txtdate = "Print date: " + this.txtDate.Text.Trim();
            string rptcustname = ds5.Tables[1].Rows[0]["name"].ToString();
            string rptcustadd = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            string rptcustphone = ds5.Tables[1].Rows[0]["telephone"].ToString();
            string rptpactdesc = ds5.Tables[1].Rows[0]["projectname"].ToString();
            string projadd = ds5.Tables[1].Rows[0]["proadd"].ToString();

            string rptunitdesc = ds5.Tables[1].Rows[0]["aptname"].ToString();
            string rptusize = ds5.Tables[1].Rows[0]["aptsize"].ToString();
            string rptsalesteam = ds5.Tables[1].Rows[0]["salesteam"].ToString();
            string rptsalesdate = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy");
            string rptagreementdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
            string rpthandoverdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");

            string rptdelcharge = (delcharge > 0) ? delcharge.ToString("#,##0;(#,##0); ") : "";

            //string txtearlyben = (ebenamt > 0) ? ("early benefit: " + ebenamt.ToString("#,##0;(#,##0); ")) : ""; ;

            string printFooter = ASTUtility.Concat(compname, username, printdate);
            string comlogo = new Uri(Server.MapPath(@"~\image\logo" + comcod + ".jpg")).AbsoluteUri;

            var lst = tblins.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedger02Cube", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", comlogo));
            Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            Rpt1.SetParameters(new ReportParameter("Compadd", comadd));
            Rpt1.SetParameters(new ReportParameter("title", "Client Ledger"));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtdate));

            Rpt1.SetParameters(new ReportParameter("rptcustname", rptcustname));
            Rpt1.SetParameters(new ReportParameter("rptCustAdd", rptcustadd));
            Rpt1.SetParameters(new ReportParameter("rptCustPhone", rptcustphone));
            Rpt1.SetParameters(new ReportParameter("rptpactdesc", rptpactdesc));
            Rpt1.SetParameters(new ReportParameter("projAddress", projadd));
            Rpt1.SetParameters(new ReportParameter("rptUnitDesc", rptunitdesc));
            Rpt1.SetParameters(new ReportParameter("rptUsize", rptusize));
            Rpt1.SetParameters(new ReportParameter("rptSalesteam", rptsalesteam));
            Rpt1.SetParameters(new ReportParameter("rptsalesdate", rptsalesdate));
            Rpt1.SetParameters(new ReportParameter("rptagreementdate", rptagreementdate));
            Rpt1.SetParameters(new ReportParameter("rptHandoverdate", rpthandoverdate));

            Rpt1.SetParameters(new ReportParameter("txtbgcost", bgcost.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptapartmentprice", aprment.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcarparking", carparking.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptUtility", utility.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcooperative", rptcooperative));
            Rpt1.SetParameters(new ReportParameter("regisfee", regisfee.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("transfee", transfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("modicharge", modicharge.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("welfarefund", rptwelfarefund));
            Rpt1.SetParameters(new ReportParameter("rptOthers", rptothers));
            Rpt1.SetParameters(new ReportParameter("receivableTotal", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("lessdisamt", rptdiscount));

            Rpt1.SetParameters(new ReportParameter("txttovalueamt", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("txttoreceived", treceived.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtfcheque", fcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtrcheque", retcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtpcheque", pcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetencash", reconamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetbalance", netbal.ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
  

        }
        private void PrintCleintLedger()
        {




            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            switch (comcod)
            {
                //case "3101":
                case "3330":
                    this.PrintCleintLedgerBr();
                    break;
                // case "3101":
                case "3336":
                case "3337":

                    this.PrintCleintLedgerSuvastu();
                    break;


                case "3339":
                    this.PrintCleintLedgerTropical();
                    break;


                case "3325":
                case "2325":
                    this.PrintCleintLedgerLeisure();
                    break;

                //case "3101":
                case "2305":
                case "3305":
                case "3306":
                case "3311":
                case "3310":
                    this.PrintCleintLedgerRupayan();
                    break;

                case "3348"://Credence               
                case "3353":// Manama
                case "3355":// greenwwod
                case "3364":// JBS
                    this.PrintCleintLedgerManama();
                    break;

               // case"3101":
                case"3357":
                    this.PrintCleintLedgerCube();
                    break;

                case "1108":
                case "1109":
                case "3315":
                case "3316":
                    this.PrintCleintLedgerAssure();
                    break;
                  
                case "3101":
                case "3366":
                    this.PrintCleintLedgerLanco();
                    break;

                default:
                    this.PrintCleintLedgergen();
                    break;
            }
        }


        protected void gvCustLedger_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 6;

                TableCell cell02 = new TableCell();
                cell02.Text = "Details";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 4;

                TableCell cell03 = new TableCell();
                cell03.Text = "";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 2;

                TableCell cell04 = new TableCell();
                cell04.Text = "Amount";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 2;

                TableCell cell05 = new TableCell();
                cell05.Text = "";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 2;

                //TableCell cell06 = new TableCell();
                //cell06.Text = "";
                //cell06.HorizontalAlign = HorizontalAlign.Center;
                //cell06.ColumnSpan = 1;

                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                //gvrow.Cells.Add(cell06);

                gvCustLedger.Controls[0].Controls.AddAt(0, gvrow);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblParticlr = (Label)e.Row.FindControl("lblParticlr");
                Label lblDueAmnt = (Label)e.Row.FindControl("lblDueAmnt");
                Label lblRcvAmnt = (Label)e.Row.FindControl("lblRcvAmnt");
                Label lblUnClr = (Label)e.Row.FindControl("lblUnClr");

                
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();


                if (code == "")
                {
                    return;
                }

                if (code == "BB" || code == "CC")
                {

                    lblParticlr.Font.Bold = true;
                    lblDueAmnt.Font.Bold = true;
                    lblRcvAmnt.Font.Bold = true;
                    lblUnClr.Font.Bold = true;

                    
                    lblParticlr.Font.Size = 11;
                    lblDueAmnt.Font.Size = 11;
                    lblRcvAmnt.Font.Size = 11;
                    lblUnClr.Font.Size = 11;



                    // e.Row.BackColor = System.Drawing.Color.Orange;
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                    lblParticlr.Attributes["style"] = "font-weight:bold; color:#FF0066;";
                    lblDueAmnt.Attributes["style"] = "font-weight:bold; color:#FF0066;";
                    lblRcvAmnt.Attributes["style"] = "font-weight:bold; color:#FF0066;";
                    lblUnClr.Attributes["style"] = "font-weight:bold; color:#FF0066;";



                    lblParticlr.Style.Add("text-align", "right");
                    //lgvNetPayment.Style.Add("text-align", "right");

                }
            }

            




        }

        protected void lbtnupdateb_OnClick(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetComeCode();
                string entryben = Convert.ToDouble("0" + this.txtentryben.Text.Trim()).ToString();
                string delaychrg = Convert.ToDouble("0" + this.txtdelaychrg.Text.Trim()).ToString();
                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string custid = this.ddlCustName.SelectedValue.ToString();

                DataSet ds1 = new DataSet("ds1");
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("code", Type.GetType("System.String"));
                dt1.Columns.Add("charge", Type.GetType("System.String"));

                DataRow dr1;

                dr1 = dt1.NewRow();
                dr1["code"] = "001";
                dr1["charge"] = entryben;
                dt1.Rows.Add(dr1);

                dr1 = dt1.NewRow();
                dr1["code"] = "002";
                dr1["charge"] = delaychrg;
                dt1.Rows.Add(dr1);
                ds1.Tables.Add(dt1);

                ds1.Tables[0].TableName = "tbl1";


                bool result = purData.UpdateXmlTransInfo(comcod, "SP_REPORT_SALSMGT01", "UPDATEBEN", ds1, null, null, pactcode, custid, "", "", "", "", "", "", "",
                    "", "", "", "", "", "", "", "", "", "", "");

            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }

}
