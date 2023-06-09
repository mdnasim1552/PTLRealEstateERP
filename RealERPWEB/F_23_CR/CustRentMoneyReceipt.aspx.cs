﻿using System;
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
namespace RealERPWEB.F_23_CR
{
    public partial class CustRentMoneyReceipt : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtReceiveDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtpaydate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.GetCustomer();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "MONEY RECEIPT INFORMATION VIEW/EDIT";

                this.PrintDupOrOrginal();
                Session.Remove("tblfincoll");

                if (this.Request.QueryString["Type"] == "CustCare")
                {
                    this.chkPrevious.Visible = false;

                }
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void PrintDupOrOrginal()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3301":
                case "1301":
                case "3101":
                    this.chkOrginal.Visible = true;
                    break;
            }


        }

        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETRENTPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ViewState["tblproject"] = ds1.Tables[0];
            ds1.Dispose();


            //----Show Resource code and Specification Code------------// 

            DataTable dt01 = (DataTable)ViewState["tblproject"];
            string search1 = this.ddlProjectName.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;

            if (dr1[0]["actelev"].ToString() == "2")
            {
                this.lblscustomer.Visible = true;
                this.txtSrcCustomer.Visible = true;
                this.ibtnFindCustomer.Visible = true;
                this.ddlCustomer.Visible = true;
                this.GetCustomer();

            }
            else
            {
                this.lblscustomer.Visible = false;
                this.txtSrcCustomer.Visible = false;
                this.ibtnFindCustomer.Visible = false;
                this.ddlCustomer.Visible = false;
                this.ddlCustomer.Items.Clear();

            }



        }
        private void GetCustomer()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtsrchCustomer = "%" + this.txtSrcCustomer.Text + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETCUSTOMERNAME", pactcode, txtsrchCustomer, "", "", "", "", "", "", "");
            this.ddlCustomer.DataTextField = "sirdesc";
            this.ddlCustomer.DataValueField = "sircode";
            this.ddlCustomer.DataSource = ds1.Tables[0];
            this.ddlCustomer.DataBind();
            ds1.Dispose();

        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetProjectName();

        }
        protected void ibtnFindCustomer_Click(object sender, EventArgs e)
        {
            this.GetCustomer();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.lblCustomer.Text = (this.ddlCustomer.Items.Count == 0) ? "" : this.ddlCustomer.SelectedItem.Text.Substring(13);
                this.ddlProjectName.Visible = false;
                this.ddlCustomer.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.lblCustomer.Visible = (this.ddlCustomer.Items.Count == 0) ? false : true;
                this.PnlMoneyReceipt.Visible = true;
                this.GetLastMrNo();
                this.PayType();
                return;
            }
            Session.Remove("tblfincoll");
            this.lbtnOk.Text = "Ok";
            this.ddlProjectName.Visible = true;
            this.ddlCustomer.Visible = true;
            this.lblProjectdesc.Visible = false;
            this.lblCustomer.Visible = false;
            this.PnlMoneyReceipt.Visible = false;
            this.gvMoneyreceipt.DataSource = null;
            this.ddlPreMrr.Items.Clear();
            this.gvMoneyreceipt.DataBind();
            this.Clearmrscreen();

        }


        private void GetLastMrNo()
        {
            string comcod = this.GetCompCode();
            DataSet ds3 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETNEWMRNO", "", "", "", "", "", "", "", "", "");
            this.lblReceiveNo.Text = ds3.Tables[0].Rows[0]["mrno"].ToString();


        }
        private void GetMrNo()
        {
            try
            {
                string comcod = this.GetCompCode();
                DataSet ds3 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETNEWMRNO", "", "", "", "", "", "", "", "", "");
                this.lblReceiveNo.Text = ds3.Tables[0].Rows[0]["mrno"].ToString();
                this.ddlPreMrr.DataTextField = "mrno";
                this.ddlPreMrr.DataValueField = "mrno";
                this.ddlPreMrr.DataSource = ds3.Tables[0];
                this.ddlPreMrr.DataBind();
                ds3.Dispose();

            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

            }



        }

        private void PayType()
        {

            string comcod = this.GetCompCode();
            DataSet ds4 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "PAYTYPE", "", "", "", "", "", "", "", "", "");
            this.ddlpaytype.DataTextField = "gdesc";
            this.ddlpaytype.DataValueField = "gcod";
            this.ddlpaytype.DataSource = ds4.Tables[0];
            this.ddlpaytype.DataBind();

            this.ddlCollType.DataTextField = "gdesc";
            this.ddlCollType.DataValueField = "gcod";
            this.ddlCollType.DataSource = ds4.Tables[1];
            this.ddlCollType.DataBind();
            this.ddlCollType.SelectedValue = "53061001001";

            this.ddlRecType.DataTextField = "gdesc";
            this.ddlRecType.DataValueField = "gcod";
            this.ddlRecType.DataSource = ds4.Tables[2];
            this.ddlRecType.DataBind();
            this.ddlRecType.SelectedValue = "54003";

            ds4.Dispose();

        }

        private void Clearmrscreen()
        {

            this.txtPaidamt.Text = "";
            this.txtchqno.Text = "";
            this.txtBName.Text = "";
            this.txtBranchName.Text = "";
            this.txtrefid.Text = "";
            this.txtremarks.Text = "";

            this.ddlPreMrr.Items.Clear();
            this.txtPaidamt.Focus();

        }
        private string CompanyPrintMR()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mrprint = "";
            switch (comcod)
            {
                case "1301":
                case "3301":
                case "3101":
                    mrprint = "MRPrint1";
                    break;
                default:
                    mrprint = "MRPrint";
                    break;
            }
            return mrprint;
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string UsirCode = (this.ddlCustomer.Items.Count == 0) ? "000000000000" : this.ddlCustomer.SelectedValue.ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string mrno = this.lblReceiveNo.Text.Trim();
            string date = Convert.ToDateTime(this.txtReceiveDate.Text).ToString("dd-MMM-yyyy");
            string Installment = "";
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataSet ds4 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "REPORTMONEYRECEIPT", PactCode, UsirCode, mrno, "", "", "", "", "", "");
            if (ds4 == null)
                return;
            DataTable dtrpt = ds4.Tables[0];
            string custadd = dtrpt.Rows[0]["custadd"].ToString();
            string custmob = dtrpt.Rows[0]["custmob"].ToString();
            string udesc = dtrpt.Rows[0]["udesc"].ToString();
            string usize = Convert.ToDouble(dtrpt.Rows[0]["usize"]).ToString("#,##0;(#,##0); -");
            string munit = dtrpt.Rows[0]["munit"].ToString();
            string paytype = dtrpt.Rows[0]["paytype"].ToString();
            string chqno = dtrpt.Rows[0]["chqno"].ToString();
            string bankname = dtrpt.Rows[0]["bankname"].ToString();
            string branch = dtrpt.Rows[0]["bbranch"].ToString();
            string refno = dtrpt.Rows[0]["refno"].ToString();
            string custteam = dtrpt.Rows[0]["custteam"].ToString();
            string rmrks = dtrpt.Rows[0]["rmrks"].ToString();
            string amt = this.txtPaidamt.Text.Trim();
            Installment = dtrpt.Rows[0]["rectype"].ToString();
            string rectype = dtrpt.Rows[0]["rectype"].ToString();
            string rectcode = dtrpt.Rows[0]["rectcode"].ToString();
            DataTable tbl1 = (DataTable)Session["tblfincoll"];
            double amt1 = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(paidamount)", "")) ? 0.00 : tbl1.Compute("Sum(paidamount)", "")));
            string amt1t = ASTUtility.Trans(amt1, 2);
            string Typedes = "";
            if (paytype == "CHEQUE" || paytype == "P.O")
            {
                Typedes = paytype + ", " + "No: " + chqno + ", Bank: " + bankname + ", Branch: " + branch;

            }

            else
            {

                Typedes = paytype;
            }
            string Type = this.CompanyPrintMR();
            ReportDocument rptMoneyRcpt = new ReportDocument();
            if (Type == "MRPrint1")
            {
                if (this.chkOrginal.Checked && this.chkOrginal.Enabled)
                    this.MoneyReceiptPrint();


                LocalReport Rpt1 = new LocalReport();

                var lst = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceipt1", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("CompName", comnam));
                Rpt1.SetParameters(new ReportParameter("CompName1", comnam));
                Rpt1.SetParameters(new ReportParameter("CompAdd", comadd));
                Rpt1.SetParameters(new ReportParameter("CompAdd1", comadd));
                Rpt1.SetParameters(new ReportParameter("txtmontype1", (rectcode == "54097") ? rectype : (rectcode == "54099") ? rectype : "MONEY RECEIPT"));
                Rpt1.SetParameters(new ReportParameter("txtmontype2", (rectcode == "54097") ? rectype : (rectcode == "54099") ? rectype : "MONEY RECEIPT"));
                Rpt1.SetParameters(new ReportParameter("txtptintable", (this.chkOrginal.Checked && this.chkOrginal.Enabled) ? "Orginal" : "Duplicate"));
                Rpt1.SetParameters(new ReportParameter("txtptintable1", (this.chkOrginal.Checked && this.chkOrginal.Enabled) ? "Orginal" : "Duplicate"));
                Rpt1.SetParameters(new ReportParameter("txtrecno1", (rectcode == "54097") ? "Refund No" : (rectcode == "54099") ? "Adjustment No" : "Receipt No"));
                Rpt1.SetParameters(new ReportParameter("txtrecno2", (rectcode == "54097") ? "Refund No" : (rectcode == "54099") ? "Adjustment No" : "Receipt No"));
                Rpt1.SetParameters(new ReportParameter("txtamttitle1", (rectcode == "54097") ? "Being the amount Refunded" : (rectcode == "54099") ? "Being the amount Adjusted" : "Received with thanks a sum of"));
                Rpt1.SetParameters(new ReportParameter("txtamttitle2", (rectcode == "54097") ? "Being the amount Refunded" : (rectcode == "54099") ? "Being the amount Adjusted" : "Received with thanks a sum of"));
                Rpt1.SetParameters(new ReportParameter("txtpayorroradajnst1", (rectcode == "54097") ? "Refund Against" : (rectcode == "54099") ? "Adjusted Against" : "Payment Received Against"));
                Rpt1.SetParameters(new ReportParameter("txtpayorroradajnst2", (rectcode == "54097") ? "Refund Against" : (rectcode == "54099") ? "Adjusted Against" : "Payment Received Against"));
                Rpt1.SetParameters(new ReportParameter("takainword", "Inwords: " + amt1t));
                Rpt1.SetParameters(new ReportParameter("takainword1", "Inwords: " + amt1t));
                Rpt1.SetParameters(new ReportParameter("txtsignature", (rectcode == "54097") ? "Client Signature" : (rectcode == "54099") ? "Client Signature" : "Prepared By"));
                Rpt1.SetParameters(new ReportParameter("txtnote1", (rectcode == "54097") ? "" : (rectcode == "54099") ? "" : "Note: This Money Receipt will be valid Subject to Encashment of the Cheque/DD/Advice/Pay Order"));
                Rpt1.SetParameters(new ReportParameter("txtnote2", (rectcode == "54097") ? "" : (rectcode == "54099") ? "" : "Note: This Money Receipt will be valid Subject to Encashment of the Cheque/DD/Advice/Pay Order"));
                Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(this.txtPaidamt.Text.Trim()).ToString("#,##0;(#,##0);") + " /-  "));
                Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(this.txtPaidamt.Text.Trim()).ToString("#,##0;(#,##0);") + " /-  "));
                Rpt1.SetParameters(new ReportParameter("paytype", paytype));
                Rpt1.SetParameters(new ReportParameter("paytype1", paytype));
                Rpt1.SetParameters(new ReportParameter("paydesc", (rectcode == "54097") ? rmrks : (rectcode == "54099") ? rmrks : (rectcode == "54009") ? rectype : Installment));
                Rpt1.SetParameters(new ReportParameter("paydesc1", (rectcode == "54097") ? rmrks : (rectcode == "54099") ? rmrks : (rectcode == "54009") ? rectype : Installment));
                Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.Cominformation()));
                Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.Cominformation()));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }



        }

        private void MoneyReceiptPrint()
        {
            string comcod = this.GetCompCode();
            string mrrno = this.ddlPreMrr.SelectedValue.ToString().Trim();
            string mPrint = this.chkOrginal.Checked ? "1" : "0";
            bool result = CustData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSORUPDATEMPRINT", mrrno, mPrint, "", "", "", "", "", "", "", "", "", "", "", "", "");



        }
        protected void lbtRefreshMrr_Click(object sender, EventArgs e)
        {
            this.Clearmrscreen();
            this.GetMrNo();

        }


        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)Session["tblfincoll"];
            for (int i = 0; i < gvMoneyreceipt.Rows.Count; i++)
            {

                tbl1.Rows[i]["chequeno"] = ((TextBox)this.gvMoneyreceipt.Rows[i].FindControl("txtgvCheckno")).Text.Trim();
                tbl1.Rows[i]["repchqno"] = ((TextBox)this.gvMoneyreceipt.Rows[i].FindControl("txtgvrepchqno")).Text.Trim();
                tbl1.Rows[i]["bankname"] = ((TextBox)this.gvMoneyreceipt.Rows[i].FindControl("txtgvbankna")).Text.Trim();
                tbl1.Rows[i]["branchname"] = ((TextBox)this.gvMoneyreceipt.Rows[i].FindControl("txtgvBrance")).Text.Trim();
                tbl1.Rows[i]["paydate"] = ((TextBox)this.gvMoneyreceipt.Rows[i].FindControl("txtgvpaydate")).Text.Trim();
                tbl1.Rows[i]["refid"] = ((TextBox)this.gvMoneyreceipt.Rows[i].FindControl("txtgvrefid")).Text.Trim();
                tbl1.Rows[i]["paidamount"] = Convert.ToDouble("0" + ((TextBox)this.gvMoneyreceipt.Rows[i].FindControl("txtgvpaidamount")).Text.Trim()).ToString();
                tbl1.Rows[i]["remarks"] = ((TextBox)this.gvMoneyreceipt.Rows[i].FindControl("txtgvremarks")).Text.Trim();
            }
            Session["tblfincoll"] = tbl1;


        }
        protected void lblAddToTable_Click(object sender, EventArgs e)
        {
            string chequeno = this.txtchqno.Text.Trim();
            string rectype = this.ddlCollType.SelectedValue.ToString();
            if ((DataTable)Session["tblfincoll"] == null)
                this.Sessiontable();
            DataTable dt = (DataTable)Session["tblfincoll"];

            DataRow[] projectrow1 = dt.Select("chequeno = '" + chequeno + "' and  RecType='" + rectype + "' "); //repchqno
            if (projectrow1.Length == 0)
            {
                DataRow drforgrid = dt.NewRow();
                drforgrid["paidamount"] = Convert.ToDouble("0" + this.txtPaidamt.Text.Trim());
                drforgrid["paytype"] = this.ddlpaytype.SelectedItem.Text.Trim();
                drforgrid["paytypecod"] = this.ddlpaytype.SelectedValue.ToString();
                drforgrid["chequeno"] = this.txtchqno.Text.Trim();
                drforgrid["bankname"] = this.txtBName.Text.Trim();
                drforgrid["branchname"] = this.txtBranchName.Text.Trim();
                drforgrid["paydate"] = this.txtpaydate.Text.Trim();
                drforgrid["refid"] = this.txtrefid.Text.Trim();
                drforgrid["repchqno"] = this.txtRpChqNo.Text.Trim();
                drforgrid["remarks"] = this.txtremarks.Text.Trim();
                drforgrid["collfrm"] = this.ddlCollType.SelectedValue.ToString();
                drforgrid["collfrmd"] = this.ddlCollType.SelectedItem.Text.Trim();
                drforgrid["RecType"] = this.ddlRecType.SelectedValue.ToString();
                drforgrid["RecTyped"] = this.ddlRecType.SelectedItem.Text.Trim();
                dt.Rows.Add(drforgrid);
            }

            else
            {
                projectrow1[0]["collfrm"] = this.ddlCollType.SelectedValue.ToString();
                projectrow1[0]["collfrmd"] = this.ddlCollType.SelectedItem.Text.Trim();
                //projectrow1[0]["RecType"] = this.ddlRecType.SelectedValue.ToString();
                //projectrow1[0]["RecTyped"] = this.ddlRecType.SelectedItem.Text.Trim();

            }

            Session["tblfincoll"] = dt;
            this.Data_Bind();
            this.txtPaidamt.Focus();
        }

        protected void Sessiontable()
        {

            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("paidamount", Type.GetType("System.Double"));
            dttemp.Columns.Add("paytype", Type.GetType("System.String"));
            dttemp.Columns.Add("paytypecod", Type.GetType("System.String"));
            dttemp.Columns.Add("chequeno", Type.GetType("System.String"));
            dttemp.Columns.Add("bankname", Type.GetType("System.String"));
            dttemp.Columns.Add("branchname", Type.GetType("System.String"));
            dttemp.Columns.Add("paydate", Type.GetType("System.String"));
            dttemp.Columns.Add("refid", Type.GetType("System.String"));
            dttemp.Columns.Add("repchqno", Type.GetType("System.String"));
            dttemp.Columns.Add("remarks", Type.GetType("System.String"));
            dttemp.Columns.Add("collfrm", Type.GetType("System.String"));
            dttemp.Columns.Add("collfrmd", Type.GetType("System.String"));
            dttemp.Columns.Add("RecType", Type.GetType("System.String"));
            dttemp.Columns.Add("RecTyped", Type.GetType("System.String"));
            Session["tblfincoll"] = dttemp;

        }


        protected void Data_Bind()
        {
            DataTable tbl1 = (DataTable)Session["tblfincoll"];
            this.gvMoneyreceipt.DataSource = tbl1;
            this.gvMoneyreceipt.DataBind();

            if (tbl1.Rows.Count > 0)
            {
                ((Label)this.gvMoneyreceipt.FooterRow.FindControl("txtFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(paidamount)", "")) ? 0.00 : tbl1.Compute("Sum(paidamount)", ""))).ToString("#,##0;(#,##0); -");

            }

        }

        protected void lbtnShowPreMrr_Click(object sender, EventArgs e)
        {
            Session.Remove("tblfincoll");
            this.Sessiontable();
            DataTable dt = (DataTable)Session["tblpremrr"];
            DataTable dt01 = (DataTable)Session["tblfincoll"];
            string mrrno = this.ddlPreMrr.SelectedValue.ToString();
            // DataTable dt02 = dt.Select("mrrno='" + mrrno + "'");
            DataRow[] dr1 = dt.Select("mrrno='" + mrrno + "'");
            if (dr1.Length > 0)
            {
                this.lblReceiveNo.Text = dr1[0]["mrrno"].ToString();
                this.txtReceiveDate.Text = Convert.ToDateTime(dr1[0]["mrdate"]).ToString("dd-MMM-yyyy");
                this.txtPaidamt.Text = Convert.ToDouble(dr1[0]["paidamt"]).ToString("#,##0;-#,##0; ");
                this.ddlpaytype.SelectedValue = dr1[0]["paytype"].ToString();
                this.txtchqno.Text = dr1[0]["chqno"].ToString();
                this.txtBName.Text = dr1[0]["bankname"].ToString();
                this.txtBranchName.Text = dr1[0]["bbranch"].ToString();
                this.txtpaydate.Text = Convert.ToDateTime(dr1[0]["paydate"]).ToString("dd-MMM-yyyy");
                this.txtrefid.Text = dr1[0]["refno"].ToString();
                this.txtRpChqNo.Text = dr1[0]["repchqno"].ToString();
                this.txtremarks.Text = dr1[0]["rmrks"].ToString();
                string schcode = dr1[0]["schcode"].ToString();
                this.ddlCollType.SelectedValue = dr1[0]["collfrm"].ToString();
                this.ddlRecType.SelectedValue = dr1[0]["recType"].ToString();
                this.chkOrginal.Checked = Convert.ToBoolean(dr1[0]["oprint"]);
                this.chkOrginal.Enabled = !(Convert.ToBoolean(dr1[0]["oprint"]));
            }
            //---------
            DataTable dtstatus = dt;//.Tables[0];
            DataView dv1 = dtstatus.DefaultView;
            dv1.RowFilter = "mrrno='" + mrrno + "'";
            DataTable dtmr = dv1.ToTable();
            for (int i = 0; dtmr.Rows.Count > i; i++)
            {
                DataRow drforgrid = dt01.NewRow();
                drforgrid["paidamount"] = Convert.ToDouble(dtmr.Rows[i]["paidamt"]).ToString("#,##0;-#,##0; ");
                drforgrid["paytype"] = dtmr.Rows[i]["paytypedesc"].ToString(); // this.ddlpaytype.SelectedItem.Text.Trim(); paytypedesc
                drforgrid["paytypecod"] = dtmr.Rows[i]["paytype"].ToString();
                drforgrid["chequeno"] = dtmr.Rows[i]["chqno"].ToString();
                drforgrid["bankname"] = dtmr.Rows[i]["bankname"].ToString();
                drforgrid["branchname"] = dtmr.Rows[i]["bbranch"].ToString();
                drforgrid["paydate"] = Convert.ToDateTime(dtmr.Rows[i]["paydate"]).ToString("dd-MMM-yyyy");
                drforgrid["refid"] = dtmr.Rows[i]["refno"].ToString();
                drforgrid["repchqno"] = dtmr.Rows[i]["repchqno"].ToString();
                drforgrid["remarks"] = dtmr.Rows[i]["rmrks"].ToString();
                drforgrid["collfrm"] = dtmr.Rows[i]["collfrm"].ToString();
                drforgrid["collfrmd"] = dtmr.Rows[i]["collfrmd"].ToString();
                drforgrid["RecType"] = dtmr.Rows[i]["RecType"].ToString();
                drforgrid["RecTyped"] = dtmr.Rows[i]["RecTyped"].ToString();
                dt01.Rows.Add(drforgrid);
                //---------
            }


            this.chkPrevious.Checked = false;
            this.chkPrevious_CheckedChanged(null, null);
            this.Data_Bind();
        }
        protected void chkPrevious_CheckedChanged(object sender, EventArgs e)
        {
            this.Panel3.Visible = this.chkPrevious.Checked;
            if (this.chkPrevious.Checked)
            {
                this.PreviousMrr();
            }
        }


        private void PreviousMrr()
        {
            Session.Remove("tblpremrr");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = ((this.ddlCustomer.Items.Count == 0) ? "" : this.ddlCustomer.SelectedValue.ToString()) + "%";
            DataSet ds4 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GetPreOthMrr", pactcode, usircode, "", "", "", "", "", "", "");
            if (ds4 == null)
                return;
            this.ddlPreMrr.DataTextField = "mrrno1";
            this.ddlPreMrr.DataValueField = "mrrno";
            this.ddlPreMrr.DataSource = ds4.Tables[1];
            this.ddlPreMrr.DataBind();
            Session["tblpremrr"] = ds4.Tables[0];
        }

        protected void gvMoneyreceipt_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblfincoll"];
            string mrrno = this.ddlPreMrr.SelectedValue.ToString().Trim();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.ddlCustomer.SelectedValue.ToString();
            string chqno = ((TextBox)this.gvMoneyreceipt.Rows[e.RowIndex].FindControl("txtgvCheckno")).Text.Trim();
            bool result = CustData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "DELETEMRRREFNO", mrrno, pactcode, usircode, chqno, "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                int rowindex = (this.gvMoneyreceipt.PageSize) * (this.gvMoneyreceipt.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("chequeno<>''");
                Session["tblfincoll"] = dv.ToTable();
                this.Data_Bind();
            }

        }
        protected void lbTotal_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            this.Data_Bind();

        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string logmrno = (ddlPreMrr.Items.Count > 0) ? this.ddlPreMrr.SelectedItem.ToString() : "NEW";
                DataSet ds3 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETMRNOLOG", logmrno, "", "", "", "", "", "", "", "");
                Session["UserLog"] = ds3.Tables[0];
                this.SaveValue();
                DataTable tbl2 = (DataTable)Session["status"];
                string SchCode = "";
                if (ddlPreMrr.Items.Count == 0)
                    this.GetMrNo();


                string PactCode = this.ddlProjectName.SelectedValue.ToString();
                string actlev = (((DataTable)ViewState["tblproject"]).Select("actcode='" + PactCode + "'"))[0]["actelev"].ToString();

                string usircode = (this.ddlCustomer.Items.Count == 0) ? "000000000000" : this.ddlCustomer.SelectedValue.ToString();

                if (actlev == "2")
                {
                    if (usircode == "000000000000")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Select Details Head');", true);
                        return;

                    }

                }



                string mrno = this.lblReceiveNo.Text.Trim();
                if (mrno.Length == 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Required MR No";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }



                // string SchCode=
                string mrdate = Convert.ToDateTime(this.txtReceiveDate.Text).ToString("dd-MMM-yyyy");
                //////////////////////userlog
                DataTable dtuser = (DataTable)Session["UserLog"];
                string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
                string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
                string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
                string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["entrydat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string PostedByid = (this.Request.QueryString["type"] == "CustCare") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
                string Posttrmid = (this.Request.QueryString["type"] == "CustCare") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
                string PostSession = (this.Request.QueryString["type"] == "CustCare") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
                string Posteddat = (this.Request.QueryString["type"] == "CustCare") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
                string EditByid = (this.Request.QueryString["type"] == "CustCare") ? "" : userid;
                string Editdat = (this.Request.QueryString["type"] == "CustCare") ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");

                DataTable dt1 = (DataTable)Session["tblfincoll"];
                bool result = true;

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string type = dt1.Rows[i]["paytypecod"].ToString(); // this.ddlpaytype.SelectedValue.ToString();repchqno
                    double paidamt = Convert.ToDouble("0" + dt1.Rows[i]["paidamount"].ToString());
                    string chqno = dt1.Rows[i]["chequeno"].ToString();
                    string bname = dt1.Rows[i]["bankname"].ToString();
                    string branchname = dt1.Rows[i]["branchname"].ToString();
                    string paydate = Convert.ToDateTime(dt1.Rows[i]["paydate"].ToString()).ToString("dd-MMM-yyyy");
                    string refno = dt1.Rows[i]["refid"].ToString();
                    string repchqno = dt1.Rows[i]["repchqno"].ToString();
                    string remrks = dt1.Rows[i]["remarks"].ToString();
                    string Collfrm = dt1.Rows[i]["collfrm"].ToString();
                    string RecType = dt1.Rows[i]["recType"].ToString();
                    paidamt = (RecType == "54097") ? paidamt * -1 : paidamt;
                    //schamt = schamt + paidamt;
                    if (paidamt != 0)
                        result = CustData.UpdateTransInfo01(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATEMRINF", PactCode, usircode, mrno, type, mrdate, paidamt.ToString(), chqno,
                                                              bname, branchname, paydate, refno, remrks, PostedByid, PostSession, Posttrmid, Posteddat, EditByid, Editdat, SchCode, repchqno, Collfrm, RecType, "0");

                    if (result == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    }
                }



                //Log Report
                string eventtype = "Money Receipt";
                string eventdesc = "Receipt No: " + mrno + " Dated: " + mrdate;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);




            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        protected void ddlpaytype_SelectedIndexChanged(object sender, EventArgs e)
        {
            string paytype = this.ddlpaytype.SelectedValue.ToString();
            if (paytype == "82002" || paytype == "82007")
            {
                txtpaydate.Text = Convert.ToDateTime(this.txtReceiveDate.Text).ToString("dd-MMM-yyyy");
            }
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable dt01 = (DataTable)ViewState["tblproject"];
            string search1 = this.ddlProjectName.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;

            if (dr1[0]["actelev"].ToString() == "2")
            {
                this.lblscustomer.Visible = true;
                this.txtSrcCustomer.Visible = true;
                this.ibtnFindCustomer.Visible = true;
                this.ddlCustomer.Visible = true;
                this.GetCustomer();

            }
            else
            {
                this.lblscustomer.Visible = false;
                this.txtSrcCustomer.Visible = false;
                this.ibtnFindCustomer.Visible = false;
                this.ddlCustomer.Visible = false;
                this.ddlCustomer.Items.Clear();

            }


        }
        protected void ibtnCollfrm_Click(object sender, EventArgs e)
        {
            this.GetSalesOrCustCare();
        }

        private void GetSalesOrCustCare()
        {
            string comcod = this.GetCompCode();
            string SechSorCusName = "%" + this.txtSrchCollfrm.Text.Trim() + "%";
            DataSet ds4 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETSALEORCUSTCARETM", SechSorCusName, "", "", "", "", "", "", "", "");
            this.ddlCollType.DataTextField = "gdesc";
            this.ddlCollType.DataValueField = "gcod";
            this.ddlCollType.DataSource = ds4.Tables[0];
            this.ddlCollType.DataBind();
            this.ddlCollType.SelectedValue = "53061001001";




        }
    }
}