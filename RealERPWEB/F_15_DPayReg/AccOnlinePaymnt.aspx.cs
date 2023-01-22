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
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
using System.Drawing;
namespace RealERPWEB.F_15_DPayReg
{
    public partial class AccOnlinePaymnt : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //this.GetProjectName();
                //this.GetPartyName();
                //this.GetBillNature();
                //this.GetRescode();


                this.GetResourceHead();
                this.TableCreate();
                this.GetBillNo();
                this.GetSelectedBillNo();
                this.txtReceiveDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Create Proposal";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
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


        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string BillList = this.ddlBillList.SelectedValue.Trim().ToString();
            string txtSProject = "%%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            Session["HeadAcc1"] = ds1.Tables[0];
        }
        private void GetPartyName()
        {

            string comcod = this.GetCompCode();
            string SearchParty = "%%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETPARTYNAME", SearchParty, "", "", "", "", "", "", "", "");
            Session["tblparty"] = ds1.Tables[0];
            ds1.Dispose();
        }


        private void GetResourceHead()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string ttsrch = "%" + this.txtsrchBillno.Text.Trim() + "%";
            Session.Remove("tblressourcehead");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETBILLNO", ttsrch, "", "", "", "", "", "", "", "");
            this.ddlResourceHead.DataSource = ds1.Tables[2];
            this.ddlResourceHead.DataTextField = "textfield";
            this.ddlResourceHead.DataValueField = "valfield";
            this.ddlResourceHead.DataBind();

            Session["tblressourcehead"] = ds1.Tables[0];
            foreach (ListItem litem in ddlResourceHead.Items)
            {

                string rescode = litem.Value;

                //string Link = (.Select("actcode='" + item + "'"))[0]["link"].ToString();
                if (rescode == "980000000000" || rescode == "990000000000" || rescode == "GBL000000000")
                {
                    litem.Attributes.Add("style", "background-color:#a3ffa3");
                }

            }
        }



        private void GetBillNo()
        {
            try
            {

                Session.Remove("tblbilsaconareq");
                Session.Remove("BillAmt");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string ttsrch = "%" + this.txtsrchBillno.Text.Trim() + "%";
                string searchbill = (rblpaytype.SelectedIndex == 0 ? "resource" : "");

                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETBILLNO", ttsrch, searchbill, "", "", "", "", "", "", "");
                Session["tblbilsaconareq"] = ds1.Tables[1];
                Session["BillAmt"] = ds1.Tables[0];


            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }


        private void GetSelectedBillNo()
        {
            string ssircode = this.ddlResourceHead.SelectedValue.ToString();
            DataTable dt = ((DataTable)Session["tblbilsaconareq"]).Copy();
            DataView dv = dt.DefaultView;
            string valfield1 = ASTUtility.Left(ssircode, 3);
            string supasub = "";

            if (valfield1 == "GBL")
            {

                dv.RowFilter = ("valfield1 ='" + valfield1 + "'");

            }


            else
            {
                supasub = ssircode.Substring(0, 2);

                if (ssircode == "990000000000" && valfield1 != "GBL")
                {
                    dv.RowFilter = ("sircode like '" + supasub + "%'  and sircode not like 'GBL%'");


                }

                else if (ssircode == "980000000000" && valfield1 != "GBL")
                {


                    dv.RowFilter = ("sircode like '" + supasub + "%'  and sircode not like 'GBL%'");

                }
                else
                {

                    dv.RowFilter = ("sircode ='" + ssircode + "' and sircode not like 'GBL%'");
                }



            }
            //else
            //{
            //    dv.RowFilter = ("sircode ='" + ssircode + "'");

            //}
            this.ddlBillList.DataSource = dv.ToTable();
            this.ddlBillList.DataTextField = "textfield";
            this.ddlBillList.DataValueField = "valfield";
            this.ddlBillList.DataBind();
        }
        private void GetBillNature()
        {
            string comcod = this.GetCompCode();
            string srchBillnature = "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETBILLNATURE", srchBillnature, "", "", "", "", "", "", "", "");
            Session["tblnature"] = ds1.Tables[0];
            ds1.Dispose();
        }
        private void GetRescode()
        {
            string comcod = this.GetCompCode();
            string srchRes = "%%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETRESCODE", srchRes, "", "", "", "", "", "", "", "");
            ds1.Dispose();
            Session["tblres"] = ds1.Tables[0];
        }


        private void TableCreate()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("mslnum", Type.GetType("System.String"));
            tblt01.Columns.Add("mslnum1", Type.GetType("System.String"));
            tblt01.Columns.Add("slnum", Type.GetType("System.String"));
            tblt01.Columns.Add("rcvdate", Type.GetType("System.String"));
            tblt01.Columns.Add("billnature", Type.GetType("System.String"));
            tblt01.Columns.Add("billndesc", Type.GetType("System.String"));
            tblt01.Columns.Add("actcode", Type.GetType("System.String"));
            tblt01.Columns.Add("actdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("rescode", Type.GetType("System.String"));
            tblt01.Columns.Add("spcfcod", Type.GetType("System.String"));

            tblt01.Columns.Add("resdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("paycode", Type.GetType("System.String"));
            tblt01.Columns.Add("paydesc", Type.GetType("System.String"));
            tblt01.Columns.Add("refno", Type.GetType("System.String"));
            tblt01.Columns.Add("billno", Type.GetType("System.String"));
            tblt01.Columns.Add("billno1", Type.GetType("System.String"));
            tblt01.Columns.Add("valdate", Type.GetType("System.String"));
            //tblt01.Columns.Add("chqdate", Type.GetType("System.String"));
            tblt01.Columns.Add("apppaydate", Type.GetType("System.String"));
            tblt01.Columns.Add("billamt", Type.GetType("System.Double"));
            tblt01.Columns.Add("billamt1", Type.GetType("System.Double"));
            tblt01.Columns.Add("amt", Type.GetType("System.Double"));
            tblt01.Columns.Add("advamt", Type.GetType("System.Double"));
            tblt01.Columns.Add("netamt", Type.GetType("System.Double"));
            ViewState["tblpayment"] = tblt01;


        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string BillList = this.ddlBillList.SelectedValue.Trim().ToString();
            DataTable dt = (DataTable)ViewState["tblpayment"];

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.SupPayPro>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_15_DPayReg.RptSupPaymentProposal", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("ReceiveDate", "Receive Date: " + Convert.ToDateTime(this.txtReceiveDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("ReceiveHead", "Resource Head: " + this.ddlResourceHead.SelectedItem.Text.Trim().ToString()));
            Rpt1.SetParameters(new ReportParameter("billno", this.ddlBillList.SelectedItem.Text.Trim().ToString()));
            Rpt1.SetParameters(new ReportParameter("Rptname", "Supplier Payment Proposal"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void lblAddToTable_Click(object sender, EventArgs e)
        {

        }

        protected void txtpaymentdate_TextChanged(object sender, EventArgs e)
        {
            //this.txtpaymentdate.Text = ASTUtility.DateInVal(this.txtpaymentdate.Text);
            //this.lbtnAddTable.Focus();
        }
        protected void lbtnAddTable_Click(object sender, EventArgs e)
        {

            if (this.txtrecordno.Text.Trim() == "")
            {

                string BillList = this.ddlBillList.SelectedValue.Trim().ToString();
                DataTable dt = (DataTable)ViewState["tblpayment"];


                string mslnum = (dt.Rows.Count == 0) ? this.GetMSlNum() : (this.chkSinglIssue.Checked) ? this.lblmslnum.Text : this.IncrmentMSlNum();
                this.lblmslnum.Text = mslnum;

                string slnum = (dt.Rows.Count == 0) ? this.GetSlNum() : (this.chkSinglIssue.Checked) ? this.lblslnum.Text : this.IncrmentSlNum();
                this.lblslnum.Text = slnum;






                DataRow[] drp = dt.Select("billno='" + BillList + "'");


                if (drp.Length == 0)
                {
                    DataRow[] dr2 = (((DataTable)Session["BillAmt"]).Select("valfield='" + BillList + "'"));

                    for (int i = 0; i < dr2.Length; i++)
                    {
                        DataRow dr1 = dt.NewRow();

                        dr1["mslnum"] = mslnum;
                        dr1["mslnum1"] = mslnum;
                        dr1["slnum"] = slnum;
                        dr1["rcvdate"] = this.txtReceiveDate.Text;
                        dr1["billnature"] = "";
                        dr1["billndesc"] = "";

                        dr1["actcode"] = (BillList.Length <= 0) ? "" : dr2[i]["pactcode"].ToString();
                        dr1["actdesc"] = (BillList.Length <= 0) ? "" : dr2[i]["pactdesc"].ToString();
                        dr1["rescode"] = (BillList.Length <= 0) ? "" : dr2[i]["sircode"].ToString();
                        dr1["resdesc"] = (BillList.Length <= 0) ? "" : dr2[i]["sirdesc1"].ToString();
                        dr1["spcfcod"] = (BillList.Length <= 0) ? "" : dr2[i]["spcfcod"].ToString();

                        dr1["paycode"] = "";
                        dr1["paydesc"] = "";
                        dr1["refno"] = (BillList.Length <= 0) ? "" : dr2[i]["billref"].ToString(); ;
                        dr1["billno"] = (BillList.Length <= 0) ? "" : this.ddlBillList.SelectedValue.ToString();
                        dr1["billno1"] = (BillList.Length <= 0) ? "" : this.ddlBillList.SelectedItem.Text.Substring(12, 11);
                        dr1["apppaydate"] = this.txtReceiveDate.Text;
                        dr1["valdate"] = Convert.ToDateTime(dr2[i]["billdat"].ToString()).ToString("dd-MMM-yyyy");
                        // dr1["chqdate"] = Convert.ToDateTime(dr2[i]["billdat"].ToString()).ToString("dd-MMM-yyyy");
                        dr1["billamt"] = (BillList.Length < 0) ? "" : dr2[i]["amt"].ToString();
                        dr1["billamt1"] = (BillList.Length < 0) ? "" : dr2[i]["amt"].ToString();
                        dr1["amt"] = (BillList.Length < 0) ? "" : dr2[i]["amt"].ToString();
                        dr1["advamt"] = 0.00;
                        dr1["netamt"] = (BillList.Length < 0) ? "" : dr2[i]["amt"].ToString(); ;
                        dt.Rows.Add(dr1);

                    }



                }

                ViewState["tblpayment"] = this.HiddenSameData(dt);

                //   ViewState["tblpayment"] = this.HiddenSameData(dt);
                this.Data_Bind();
                //this.txtReceiveDate.Focus();
            }
            else
            {

                this.GetNoRecord();

            }

        }
        protected void btnAllBill_Click(object sender, EventArgs e)
        {

            string sircode1 = this.ddlResourceHead.SelectedValue.ToString();
            string BillList = this.ddlBillList.SelectedValue.Trim().ToString();
            DataTable dt1 = (DataTable)ViewState["tblpayment"];
            DataTable dt = (DataTable)Session["BillAmt"];
            DataTable dt2 = dt.Copy();
            DataView dv = dt2.DefaultView;


            string valfield1 = ASTUtility.Left(BillList, 3);
            string supasub = "";

            if (valfield1 == "GBL")
            {

                dv.RowFilter = ("valfield like '" + valfield1 + "%'");

            }


            else
            {
                supasub = sircode1.Substring(0, 2);

                if (sircode1 == "990000000000")
                {
                    dv.RowFilter = ("sircode like '" + supasub + "%'");


                }

                else if (sircode1 == "980000000000")
                {


                    dv.RowFilter = ("sircode like '" + supasub + "%'");

                }
                else
                {

                    dv.RowFilter = ("sircode ='" + sircode1 + "'");
                }



            }




            //dv.RowFilter = ("sircode ='" + sircode1 + "'");
            //  dv.RowFilter = "(sircode like '" + sircode + "*') ";


            dt = dv.ToTable();
            string billno1 = dt.Rows[0]["valfield"].ToString();


            //string mslnum = (dt1.Rows.Count == 0) ? this.GetMSlNum() : this.IncrmentMSlNum();
            //this.lblmslnum.Text = mslnum;
            //string slnum = (dt1.Rows.Count == 0) ? this.GetSlNum() : this.IncrmentSlNum();
            //this.lblslnum.Text = slnum;


            string mslnum = (dt1.Rows.Count == 0) ? this.GetMSlNum() : (this.chkSinglIssue.Checked) ? this.lblmslnum.Text : this.IncrmentMSlNum();
            this.lblmslnum.Text = mslnum;

            string slnum = (dt1.Rows.Count == 0) ? this.GetSlNum() : (this.chkSinglIssue.Checked) ? this.lblslnum.Text : this.IncrmentSlNum();
            this.lblslnum.Text = slnum;






            //DataRow dr1 = dt.NewRow();
            DataRow[] dr2 = (((DataTable)ViewState["tblpayment"]).Select("billno='" + BillList + "'"));


            if (dr2.Length == 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr1 = dt1.NewRow();
                    mslnum = (billno1 == dt.Rows[i]["valfield"].ToString()) ? this.GetMSlNum() : (this.chkSinglIssue.Checked) ? this.lblmslnum.Text : this.IncrmentMSlNum();
                    this.lblmslnum.Text = mslnum;

                    slnum = (billno1 == dt.Rows[i]["valfield"].ToString()) ? this.GetSlNum() : (this.chkSinglIssue.Checked) ? this.lblslnum.Text : this.IncrmentSlNum();
                    this.lblslnum.Text = slnum;
                    dr1["mslnum"] = this.lblmslnum.Text;//ASTUtility.Right(("000000000" + (Convert.ToDouble(slnum) + i)), 9);
                    dr1["mslnum1"] = this.lblmslnum.Text;//ASTUtility.Right(("000000000" + (Convert.ToDouble(slnum) + i)), 9);;

                    dr1["slnum"] = this.lblslnum.Text;//ASTUtility.Right(("000000000" + (Convert.ToDouble(slnum) + i)), 9);
                    dr1["rcvdate"] = this.txtReceiveDate.Text;
                    dr1["billnature"] = "";
                    dr1["billndesc"] = "";

                    dr1["actcode"] = dt.Rows[i]["pactcode"].ToString();
                    dr1["actdesc"] = dt.Rows[i]["pactdesc"].ToString();
                    dr1["rescode"] = dt.Rows[i]["sircode"].ToString();
                    dr1["resdesc"] = dt.Rows[i]["sirdesc1"].ToString();
                    dr1["spcfcod"] = dt.Rows[i]["spcfcod"].ToString();
                    dr1["paycode"] = "";
                    dr1["paydesc"] = "";
                    dr1["refno"] = dt.Rows[i]["billref"].ToString();
                    dr1["billno"] = dt.Rows[i]["valfield"].ToString();
                    //string textfield = dt.Rows[i]["textfield"].ToString();
                    dr1["billno1"] = dt.Rows[i]["billno"].ToString();
                    dr1["apppaydate"] = this.txtReceiveDate.Text;

                    dr1["valdate"] = Convert.ToDateTime(dt.Rows[i]["billdat"].ToString()).ToString("dd-MMM-yyyy");
                    //dr1["chqdate"] = Convert.ToDateTime(dt.Rows[i]["billdat"].ToString()).ToString("dd-MMM-yyyy");
                    dr1["billamt1"] = dt.Rows[i]["amt"].ToString();

                    dr1["billamt"] = dt.Rows[i]["amt"].ToString();
                    dr1["amt"] = dt.Rows[i]["amt"].ToString();
                    dr1["advamt"] = 0.00;
                    dr1["netamt"] = dt.Rows[i]["amt"].ToString();
                    dt1.Rows.Add(dr1);

                    billno1 = dt.Rows[i]["valfield"].ToString();
                }
            }

            ViewState["tblpayment"] = dt1;
            this.Data_Bind();

            // emdad vi before

            //this.ddlBillList.SelectedIndex = 1;



            ////this.gvPayment.FooterRow.FindControl("lbtnUpdate").Visible = false;

            //string BillList = this.ddlBillList.SelectedValue.Trim().ToString();
            //DataTable dt1 = (DataTable)ViewState["tblpayment"];
            //DataTable dt = (DataTable)Session["BillAmt"];
            //DataTable dt2 = dt.Copy();
            //DataView dv = dt2.DefaultView;
            //dv.RowFilter = "valfield <> ''";
            //dt = dv.ToTable();

            //string billno1 = dt.Rows[0]["valfield"].ToString();

            //string mslnum = (dt1.Rows.Count == 0) ? this.GetMSlNum() : this.IncrmentMSlNum();
            //this.lblmslnum.Text = mslnum;
            //string slnum = (dt1.Rows.Count == 0) ? this.GetSlNum() : this.IncrmentSlNum();
            //this.lblslnum.Text = slnum;
            ////DataRow dr1 = dt.NewRow();
            //DataRow[] dr2 = (((DataTable)ViewState["tblpayment"]).Select("billno='" + BillList + "'"));
            //if (dr2.Length == 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        DataRow dr1 = dt1.NewRow();
            //        mslnum = (billno1 == dt.Rows[i]["valfield"].ToString()) ? this.lblmslnum.Text : this.IncrmentMSlNum();
            //        this.lblmslnum.Text = mslnum;

            //        slnum = (billno1 == dt.Rows[i]["valfield"].ToString()) ? this.lblslnum.Text : this.IncrmentSlNum();
            //        this.lblslnum.Text = slnum;
            //        dr1["mslnum"] = this.lblmslnum.Text;//ASTUtility.Right(("000000000" + (Convert.ToDouble(slnum) + i)), 9);
            //        dr1["mslnum1"] = this.lblmslnum.Text;//ASTUtility.Right(("000000000" + (Convert.ToDouble(slnum) + i)), 9);;

            //        dr1["slnum"] = this.lblslnum.Text;//ASTUtility.Right(("000000000" + (Convert.ToDouble(slnum) + i)), 9);
            //        dr1["rcvdate"] = this.txtReceiveDate.Text;
            //        dr1["billnature"] = "";
            //        dr1["billndesc"] = "";

            //        dr1["actcode"] = dt.Rows[i]["pactcode"].ToString();
            //        dr1["actdesc"] = dt.Rows[i]["pactdesc"].ToString();
            //        dr1["rescode"] = dt.Rows[i]["sircode"].ToString();
            //        dr1["resdesc"] = dt.Rows[i]["sirdesc1"].ToString();
            //        dr1["paycode"] = "";
            //        dr1["paydesc"] = "";
            //        dr1["refno"] = dt.Rows[i]["billref"].ToString();
            //        dr1["billno"] = dt.Rows[i]["valfield"].ToString();
            //        //string textfield = dt.Rows[i]["textfield"].ToString();
            //        dr1["billno1"] = dt.Rows[i]["billno"].ToString();
            //        dr1["apppaydate"] =this.txtReceiveDate.Text;

            //        dr1["valdate"] = Convert.ToDateTime(dt.Rows[i]["billdat"].ToString()).ToString("dd-MMM-yyyy");
            //        //dr1["chqdate"] = Convert.ToDateTime(dt.Rows[i]["billdat"].ToString()).ToString("dd-MMM-yyyy");
            //        dr1["billamt1"] = dt.Rows[i]["amt"].ToString();

            //        dr1["billamt"] = dt.Rows[i]["amt"].ToString();
            //        dr1["amt"] = dt.Rows[i]["amt"].ToString();
            //        dr1["advamt"] = 0.00;
            //        dr1["netamt"] = dt.Rows[i]["amt"].ToString();
            //        dt1.Rows.Add(dr1);

            //        billno1 = dt.Rows[i]["valfield"].ToString();
            //    }
            //}

            //ViewState["tblpayment"] = dt1;
            //this.Data_Bind();
            ////this.txtReceiveDate.Focus();
        }
        private void GetNoRecord()
        {

            int recordno = Convert.ToInt32("0" + this.txtrecordno.Text.Trim());
            DataTable dt = (DataTable)ViewState["tblpayment"];
            for (int i = 0; i < recordno; i++)
            {
                string slnum = (dt.Rows.Count == 0) ? this.GetSlNum() : this.IncrmentSlNum();
                this.lblslnum.Text = slnum;
                DataRow dr1 = dt.NewRow();
                dr1["slnum"] = slnum;
                dr1["rcvdate"] = this.txtReceiveDate.Text;
                dr1["billnature"] = "";
                dr1["billndesc"] = "";
                dr1["actcode"] = "";
                dr1["actdesc"] = "";
                dr1["rescode"] = "";
                dr1["resdesc"] = "";
                dr1["paycode"] = "";
                dr1["paydesc"] = "";
                dr1["refno"] = "";
                dr1["billno"] = "";
                dr1["billno1"] = "";
                dr1["apppaydate"] = this.txtReceiveDate.Text;
                dr1["valdate"] = ASTUtility.DateInVal(this.txtReceiveDate.Text);
                dr1["amt"] = 0.00;
                dr1["advamt"] = 0.00;
                dr1["netamt"] = 0.00;
                dt.Rows.Add(dr1);

            }

            ViewState["tblpayment"] = dt;
            this.Data_Bind();


        }
        private void Data_Bind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblpayment"];
            this.gvPayment.DataSource = tbl1;
            this.gvPayment.DataBind();

            if (tbl1.Rows.Count > 0)
            {
                ((Label)this.gvPayment.FooterRow.FindControl("txtFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(billamt1)", "")) ? 0.00 : tbl1.Compute("Sum(billamt1)", ""))).ToString("#,##0;(#,##0); -");
                ((Label)this.gvPayment.FooterRow.FindControl("lblgvFtopayamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(amt)", "")) ? 0.00 : tbl1.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); -");


            }

            string paydate = tbl1.Rows[0]["apppaydate"].ToString();
            string billno = tbl1.Rows[0]["billno"].ToString();

            //int indxa = tbl1.Rows.Count;

            string amslnum1 = "";
            string nmslnum1 = "";
            for (int k = 0; k < tbl1.Rows.Count - 1; k++)
            {


                amslnum1 = tbl1.Rows[k]["mslnum"].ToString();
                nmslnum1 = tbl1.Rows[k + 1]["mslnum"].ToString();


                if (amslnum1 == nmslnum1)
                {
                    ((LinkButton)this.gvPayment.Rows[k].FindControl("lbok")).Style["display"] = "none";

                }
            }




            for (int j = 1; j < tbl1.Rows.Count; j++)
            {
                string mslnum1 = tbl1.Rows[j]["mslnum1"].ToString();

                DateTime vadate = Convert.ToDateTime(tbl1.Rows[j]["valdate"].ToString());
                DateTime paymdate = Convert.ToDateTime(this.txtReceiveDate.Text);

                // to get the total days in between
                int daydif = ASTUtility.Datediffday(paymdate, vadate);







                if (daydif >= 15)
                {
                    ((Label)this.gvPayment.Rows[j].FindControl("lblgvValdate")).ForeColor = Color.Red;
                }


                if (billno == tbl1.Rows[j]["billno"].ToString() && (billno.Substring(0, 3) == "PBL" || billno.Substring(0, 3) == "CBL" || billno.Substring(0, 3) == "POR"))
                {
                    this.gvPayment.Rows[j].BackColor = Color.Bisque;

                    ((TextBox)this.gvPayment.Rows[j].FindControl("txtgvpayamt")).Focus();
                    ((TextBox)this.gvPayment.Rows[j].FindControl("txtgvpayamt")).ForeColor = Color.Red;
                }
                billno = tbl1.Rows[j]["billno"].ToString();
            }







        }


        private string GetMSlNum()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETMASSLNUM", "", "", "", "", "", "", "", "", "");
            return ds2.Tables[0].Rows[0]["mslnum"].ToString();
        }




        private string GetSlNum()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETSLNUM", "", "", "", "", "", "", "", "", "");
            return ds2.Tables[0].Rows[0]["slnum"].ToString();
        }





        private string IncrmentMSlNum()
        {
            //string isunum="000000000";
            string mslnum = (Convert.ToInt32(this.lblmslnum.Text.Trim()) + 1).ToString();
            return (ASTUtility.Right(("000000000" + mslnum), 9));



        }
        private string IncrmentSlNum()
        {
            //string isunum="000000000";
            string slnum = (Convert.ToInt32(this.lblslnum.Text.Trim()) + 1).ToString();
            return (ASTUtility.Right(("000000000" + slnum), 9));



        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;


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
                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();


                this.SaveValue();
               

                    DataTable dt1 = (DataTable)ViewState["tblpayment"];

                for (int i = 0; i < this.gvPayment.Rows.Count; i++)
                {


                    double billamt = Convert.ToDouble(dt1.Rows[i]["billamt"]);
                    double payamt = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvpayamt")).Text.Trim());
                    string billno = dt1.Rows[i]["billno"].ToString();
                    if (billamt < payamt)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Payment Amount Greater then Bill Amount";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                        return;

                    }
                }


                bool result = true;

                string mslnum = this.GetMSlNum();
                this.lblmslnum.Text = mslnum;

                string slnum = this.GetSlNum();
                this.lblslnum.Text = slnum;

                string billno2 = dt1.Rows[0]["billno"].ToString();
                string apppaydate02 = Convert.ToDateTime(dt1.Rows[0]["apppaydate"].ToString()).ToString("dd-MMM-yyyy");

                foreach (DataRow dr in dt1.Rows)
                {
                    string rcvdate = ASTUtility.DateFormat(dr["rcvdate"].ToString());
                    string refno = dr["refno"].ToString().Trim();
                    string actcode = dr["actcode"].ToString();
                    string rescode = (dr["rescode"].ToString() == "") ? "000000000000" : dr["rescode"].ToString();
                    string spcfcod = (dr["spcfcod"].ToString() == "") ? "000000000000" : dr["spcfcod"].ToString();
                    string paycode = dr["paycode"].ToString();
                    string billno = dr["billno"].ToString();
                    string apppaydate = dr["apppaydate"].ToString();
                    //sl Number

                    mslnum = (billno2 != billno) ? (this.chkSinglIssue.Checked ? this.lblmslnum.Text : this.IncrmentMSlNum()) : this.lblmslnum.Text;
                    this.lblmslnum.Text = mslnum;



                    // Multi Payment
                    if (billno2 == dr["billno"].ToString())
                    {

                        if (apppaydate02 == Convert.ToDateTime(dr["apppaydate"].ToString()).ToString("dd-MMM-yyyy"))
                        {
                            slnum = this.lblslnum.Text;
                            // slnum =(this.chkSinglIssue.Checked) ? this.lblslnum.Text : this.IncrmentSlNum();
                        }
                        else
                        {
                            //slnum = this.IncrmentSlNum();
                            slnum = (this.chkSinglIssue.Checked) ? this.lblslnum.Text : this.IncrmentSlNum();

                        }


                    }
                    else
                    {
                        //slnum = this.IncrmentSlNum();
                        slnum = (this.chkSinglIssue.Checked) ? this.lblslnum.Text : this.IncrmentSlNum();


                    }




                    //slnum = (billno2 != billno) ? this.IncrmentSlNum() : this.lblslnum.Text;

                    this.lblslnum.Text = slnum;

                    string billnature = dr["billnature"].ToString();
                    string billamt = dr["billamt"].ToString();

                    double amt = Convert.ToDouble("0" + dr["amt"].ToString());
                    double advamt = Convert.ToDouble("0" + dr["advamt"].ToString());
                    string valdate = dr["valdate"].ToString();

                    if (amt > 0)
                    {
                        result = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "INSERTORUPONLINEPAY", slnum, rcvdate, refno, actcode, paycode, billnature, amt.ToString(),
                                                                   apppaydate, rescode, billno, advamt.ToString(), valdate, userid, Terminal, Sessionid, mslnum, billamt, spcfcod, "", "", "");

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
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Bill Amount Empty";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    }

                    billno2 = billno;
                    apppaydate02 = apppaydate;
                }


                ((LinkButton)this.gvPayment.FooterRow.FindControl("lbtnUpdate")).Enabled = false;


                //Log Report





            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }


        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblpayment"];
            //for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            //{


            //    dt.Rows[i]["rcvdate"] = ((Label)this.gvPayment.Rows[i].FindControl("txtgvrcvdate")).Text.Trim();
            //    dt.Rows[i]["refno"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvref")).Text.Trim();
            //    dt.Rows[i]["apppaydate"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvpaymentdate")).Text.Trim();
            //    dt.Rows[i]["amt"] = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvpayamt")).Text.Trim()).ToString("#,##0.00;(#,##0.00);");
            //    // dt.Rows[i]["chqdate"] = Convert.ToDateTime(((TextBox)this.gvPayment.Rows[i].FindControl("txtgvchequedate")).Text.Trim()).ToString("dd-MMM-yyyy");


            //    //dt.Rows[i]["advamt"] = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAdvamt")).Text.Trim()).ToString("#,##0.00;(#,##0.00);");
            //    //dt.Rows[i]["netamt"] = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvbillamt")).Text.Trim()) - Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAdvamt")).Text.Trim());

            //}
            //ViewState["tblpayment"] = this.HiddenSameData(dt);


            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            string pbillno = "000000000000";

            double chkamt = 0.00;
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {


                double billamt = Convert.ToDouble(dt.Rows[i]["billamt"]);
                double payamt = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvpayamt")).Text.Trim());
                string billno = dt.Rows[i]["billno"].ToString();
                if (billamt < payamt)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                    return;

                }
                if (pbillno == billno)
                {
                    chkamt = chkamt - payamt;
                    if (chkamt < 0)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                        return;
                    }

                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "";
                    chkamt = billamt - payamt;
                }
                pbillno = dt.Rows[i]["billno"].ToString();


                dt.Rows[i]["rcvdate"] = ((Label)this.gvPayment.Rows[i].FindControl("txtgvrcvdate")).Text.Trim();
                dt.Rows[i]["refno"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvref")).Text.Trim();
                dt.Rows[i]["apppaydate"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvpaymentdate")).Text.Trim();
                dt.Rows[i]["amt"] = payamt;


            }



            ViewState["tblpayment"] = this.HiddenSameData(dt);




        }
 

        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {

            ViewState.Remove("tblpayment");
            // this.GetResourceHead();
            this.TableCreate();
            this.GetBillNo();
            this.GetSelectedBillNo();
            this.gvPayment.EditIndex = -1;
            this.gvPayment.DataSource = null;
            this.gvPayment.DataBind();


        }

        protected void ibtnBillNo_Click(object sender, EventArgs e)
        {
            this.GetBillNo();
        }

        protected void gvPayment_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvPayment.EditIndex = -1;
            this.Data_Bind();

        }
        protected void gvPayment_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvPayment.EditIndex = e.NewEditIndex;
            this.Data_Bind();
            int rowindex = (this.gvPayment.PageSize) * (this.gvPayment.PageIndex) + e.NewEditIndex;
            string actcode = ((DataTable)ViewState["tblpayment"]).Rows[rowindex]["actcode"].ToString();
            string rescode = ((DataTable)ViewState["tblpayment"]).Rows[rowindex]["rescode"].ToString();
            string paycode = ((DataTable)ViewState["tblpayment"]).Rows[rowindex]["paycode"].ToString();
            string billnature = ((DataTable)ViewState["tblpayment"]).Rows[rowindex]["billnature"].ToString();
            //string received

            DropDownList ddlproject = (DropDownList)this.gvPayment.Rows[e.NewEditIndex].FindControl("ddlProject");
            ViewState["gindex"] = e.NewEditIndex;
            DataTable dt01 = (DataTable)Session["HeadAcc1"];
            //Project
            ddlproject.DataTextField = "actdesc";
            ddlproject.DataValueField = "actcode";
            ddlproject.DataSource = dt01;
            ddlproject.DataBind();
            ddlproject.SelectedValue = actcode;


            //Resource

            string search1 = ((DropDownList)this.gvPayment.Rows[e.NewEditIndex].FindControl("ddlProject")).SelectedValue.ToString();
            DropDownList ddlRescode = ((DropDownList)this.gvPayment.Rows[e.NewEditIndex].FindControl("ddlRescode"));
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;


            if (dr1[0]["actelev"].ToString() == "2")
            {


                if (actcode == "18" || actcode == "24" || actcode == "25" || actcode == "25" || actcode == "18" || ddlRescode.Items.Count == 0)
                {
                    DataTable dt01a = ((DataTable)Session["HeadAcc1"]).Copy();
                    DataView dv1 = dt01a.DefaultView;
                    dv1.RowFilter = "actcode like '" + search1 + "%'";
                    string filter2 = dv1.ToTable().Rows[0]["acttype"].ToString();
                    ///string actcode=
                    DataTable dt = ((DataTable)Session["tblres"]).Copy();
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "rescode like '" + filter2 + "%'";

                    ddlRescode.DataTextField = "resdesc";
                    ddlRescode.DataValueField = "rescode";
                    ddlRescode.DataSource = dv.ToTable();
                    ddlRescode.DataBind();
                    ddlRescode.SelectedValue = rescode;



                }

            }
            else
            {

                ddlRescode.Visible = false;
                ddlRescode.Items.Clear();


            }

            //Party 

            DropDownList ddlPartyName = ((DropDownList)this.gvPayment.Rows[e.NewEditIndex].FindControl("ddlPartyName"));
            ddlPartyName.DataTextField = "prdesc";
            ddlPartyName.DataValueField = "prcode";
            ddlPartyName.DataSource = (DataTable)Session["tblparty"];
            ddlPartyName.DataBind();
            ddlPartyName.SelectedValue = paycode;


            //Nature
            //---------------------------------------------//
            DropDownList ddlBillNature = ((DropDownList)this.gvPayment.Rows[e.NewEditIndex].FindControl("ddlBillNature"));
            ddlBillNature.DataTextField = "rpdesc";
            ddlBillNature.DataValueField = "rpcode";
            ddlBillNature.DataSource = (DataTable)Session["tblnature"];
            ddlBillNature.DataBind();
            ddlBillNature.SelectedValue = billnature;


            //Another Part:
            ((TextBox)this.gvPayment.Rows[e.NewEditIndex].FindControl("txteditReceiveDate")).Text = ((DataTable)ViewState["tblpayment"]).Rows[rowindex]["rcvdate"].ToString();
            ((TextBox)this.gvPayment.Rows[e.NewEditIndex].FindControl("txteditRefno")).Text = ((DataTable)ViewState["tblpayment"]).Rows[rowindex]["refno"].ToString();
            ((TextBox)this.gvPayment.Rows[e.NewEditIndex].FindControl("txteditpaymentdate")).Text = ((DataTable)ViewState["tblpayment"]).Rows[rowindex]["apppaydate"].ToString();
            ((Label)this.gvPayment.Rows[e.NewEditIndex].FindControl("lbleditValDate")).Text = ((DataTable)ViewState["tblpayment"]).Rows[rowindex]["valdate"].ToString();
            ((TextBox)this.gvPayment.Rows[e.NewEditIndex].FindControl("txteditBillAmount")).Text = Convert.ToDouble(((DataTable)ViewState["tblpayment"]).Rows[rowindex]["amt"]).ToString("#,##0.00;(#,##0.00)");
            ((TextBox)this.gvPayment.Rows[e.NewEditIndex].FindControl("txteditAdvAmt")).Text = Convert.ToDouble(((DataTable)ViewState["tblpayment"]).Rows[rowindex]["advamt"]).ToString("#,##0.00;(#,##0.00)");


        }
        protected void gvPayment_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            DataTable dt = (DataTable)ViewState["tblpayment"];
            try
            {

                int rowindex = (this.gvPayment.PageSize) * (this.gvPayment.PageIndex) + e.RowIndex;
                dt.Rows[rowindex]["rcvdate"] = ((Label)this.gvPayment.Rows[e.RowIndex].FindControl("txteditReceiveDate")).Text.Trim();
                dt.Rows[rowindex]["actcode"] = ((DropDownList)this.gvPayment.Rows[e.RowIndex].FindControl("ddlProject")).SelectedValue.ToString();
                dt.Rows[rowindex]["actdesc"] = ((DropDownList)this.gvPayment.Rows[e.RowIndex].FindControl("ddlProject")).SelectedItem.Text;
                dt.Rows[rowindex]["rescode"] = ((DropDownList)this.gvPayment.Rows[e.RowIndex].FindControl("ddlRescode")).SelectedValue.ToString();
                dt.Rows[rowindex]["resdesc"] = (((DropDownList)this.gvPayment.Rows[e.RowIndex].FindControl("ddlRescode")).Items.Count == 0) ? "" : ((DropDownList)this.gvPayment.Rows[e.RowIndex].FindControl("ddlRescode")).SelectedItem.Text;
                dt.Rows[rowindex]["billnature"] = ((DropDownList)this.gvPayment.Rows[e.RowIndex].FindControl("ddlBillNature")).SelectedValue.ToString();
                dt.Rows[rowindex]["billndesc"] = ((DropDownList)this.gvPayment.Rows[e.RowIndex].FindControl("ddlBillNature")).SelectedItem.Text;
                dt.Rows[rowindex]["paycode"] = ((DropDownList)this.gvPayment.Rows[e.RowIndex].FindControl("ddlPartyName")).SelectedValue.ToString();
                dt.Rows[rowindex]["paydesc"] = ((DropDownList)this.gvPayment.Rows[e.RowIndex].FindControl("ddlPartyName")).SelectedItem.Text;
                dt.Rows[rowindex]["refno"] = ((TextBox)this.gvPayment.Rows[e.RowIndex].FindControl("txteditRefno")).Text.Trim();
                dt.Rows[rowindex]["apppaydate"] = ((TextBox)this.gvPayment.Rows[e.RowIndex].FindControl("txteditpaymentdate")).Text.Trim();
                dt.Rows[rowindex]["valdate"] = ((Label)this.gvPayment.Rows[e.RowIndex].FindControl("lbleditValDate")).Text.Trim();
                dt.Rows[rowindex]["amt"] = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[e.RowIndex].FindControl("txteditBillAmount")).Text.Trim()).ToString("#,##0.00;(#,##0.00);");
                dt.Rows[rowindex]["advamt"] = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[e.RowIndex].FindControl("txteditAdvAmt")).Text.Trim()).ToString("#,##0.00;(#,##0.00);");
                dt.Rows[rowindex]["netamt"] = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[e.RowIndex].FindControl("txteditBillAmount")).Text.Trim()) - Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[e.RowIndex].FindControl("txteditAdvAmt")).Text.Trim());

                ViewState["tblpayment"] = dt;
                this.gvPayment.EditIndex = -1;
                this.Data_Bind();

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }
        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {

            int rowindex = (int)ViewState["gindex"];
            string actcode = ((DropDownList)this.gvPayment.Rows[rowindex].FindControl("ddlProject")).SelectedValue.ToString();
            string rescode = ((DropDownList)this.gvPayment.Rows[rowindex].FindControl("ddlRescode")).SelectedValue.ToString();
            DropDownList ddlRescode = ((DropDownList)this.gvPayment.Rows[rowindex].FindControl("ddlRescode"));
            //Resource
            DataTable dt01 = (DataTable)Session["HeadAcc1"];
            DataRow[] dr1 = dt01.Select("actcode='" + actcode + "'");
            if (dr1.Length == 0)
                return;


            if (dr1[0]["actelev"].ToString() == "2")
            {
                DataView dv1 = dt01.DefaultView;
                dv1.RowFilter = "actcode like '" + actcode + "%'";
                string filter2 = dv1.ToTable().Rows[0]["acttype"].ToString();
                ///string actcode=
                DataTable dt = ((DataTable)Session["tblres"]).Copy();
                DataView dv = dt.DefaultView;
                dv.RowFilter = "rescode like '" + filter2 + "%'";

                ddlRescode.DataTextField = "resdesc";
                ddlRescode.DataValueField = "rescode";
                ddlRescode.DataSource = dv.ToTable();
                ddlRescode.DataBind();

            }
            else
            {

                ddlRescode.Visible = false;
                ddlRescode.Items.Clear();


            }
        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int rowindex = (int)ViewState["gindex"];
            DropDownList ddlProjectName = ((DropDownList)this.gvPayment.Rows[rowindex].FindControl("ddlProject"));
            string SearchProject = ((TextBox)this.gvPayment.Rows[rowindex].FindControl("txtsrchProject")).Text.Trim();
            DataTable dt = (DataTable)Session["HeadAcc1"];
            var results = (from project in dt.AsEnumerable()
                           where (project.Field<string>("actcode").Contains(SearchProject) || project.Field<string>("actdesc").ToUpper().Contains(SearchProject.ToUpper()))
                           select project);
            ddlProjectName.DataTextField = "actdesc";
            ddlProjectName.DataValueField = "actcode";
            ddlProjectName.DataSource = results.AsDataView().ToTable();
            ddlProjectName.DataBind();
            this.ddlProject_SelectedIndexChanged(null, null);
        }

        protected void ibtnRes_Click(object sender, EventArgs e)
        {

            int rowindex = (int)ViewState["gindex"];
            DropDownList ddlRescode = ((DropDownList)this.gvPayment.Rows[rowindex].FindControl("ddlRescode"));
            string SearchResource = ((TextBox)this.gvPayment.Rows[rowindex].FindControl("txtsrchRes")).Text.Trim();
            DataTable dt = (DataTable)Session["tblres"];
            var results = (from resource in dt.AsEnumerable()
                           where (resource.Field<string>("rescode").Contains(SearchResource) || resource.Field<string>("resdesc").ToUpper().Contains(SearchResource.ToUpper()))
                           select resource);
            ddlRescode.DataTextField = "resdesc";
            ddlRescode.DataValueField = "rescode";
            ddlRescode.DataSource = results.AsDataView().ToTable();
            ddlRescode.DataBind();




        }

        protected void ibtnFindParty_Click(object sender, EventArgs e)
        {


            int rowindex = (int)ViewState["gindex"];
            DropDownList ddlPartyName = ((DropDownList)this.gvPayment.Rows[rowindex].FindControl("ddlPartyName"));
            string SearchParty = ((TextBox)this.gvPayment.Rows[rowindex].FindControl("txtSrhParty")).Text.Trim();
            DataTable dt = (DataTable)Session["tblparty"];
            var results = (from party in dt.AsEnumerable()

                           where party.Field<string>("prdesc").ToUpper().Contains(SearchParty.ToUpper())
                           select party);
            ddlPartyName.DataTextField = "prdesc";
            ddlPartyName.DataValueField = "prcode";
            ddlPartyName.DataSource = results.AsDataView().ToTable();
            ddlPartyName.DataBind();



            //this.ddlPartyName.Focus();
        }
        protected void ibtnnature_Click(object sender, EventArgs e)
        {

            int rowindex = (int)ViewState["gindex"];
            DropDownList ddlBillNature = ((DropDownList)this.gvPayment.Rows[rowindex].FindControl("ddlBillNature"));
            string SearchNature = ((TextBox)this.gvPayment.Rows[rowindex].FindControl("txtsrchnature")).Text.Trim();
            DataTable dt = (DataTable)Session["tblnature"];
            var results = (from nature in dt.AsEnumerable()
                           where nature.Field<string>("rpdesc").ToUpper().Contains(SearchNature.ToUpper())
                           select nature);

            ddlBillNature.DataTextField = "rpdesc";
            ddlBillNature.DataValueField = "rpcode";
            ddlBillNature.DataSource = results.AsDataView().ToTable();
            ddlBillNature.DataBind();


        }

        protected void gvPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblpayment"];
            int rowindex = (this.gvPayment.PageSize) * (this.gvPayment.PageIndex) + e.RowIndex;
            string slnum = dt.Rows[rowindex]["slnum"].ToString();
            dt.Rows[rowindex].Delete();

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblpayment");
            ViewState["tblpayment"] = dv.ToTable();
            this.Data_Bind();



        }
        protected void lbtnGrdUpdate_Click(object sender, EventArgs e)
        {


            DataTable dt = (DataTable)ViewState["tblpayment"];
            try
            {
                int rowindex = (int)ViewState["gindex"];
                // int rowindex = (this.gvPayment.PageSize) * (this.gvPayment.PageIndex) + e.RowIndex;
                dt.Rows[rowindex]["rcvdate"] = ((Label)this.gvPayment.Rows[rowindex].FindControl("txteditReceiveDate")).Text.Trim();
                dt.Rows[rowindex]["actcode"] = ((DropDownList)this.gvPayment.Rows[rowindex].FindControl("ddlProject")).SelectedValue.ToString();
                dt.Rows[rowindex]["actdesc"] = ((DropDownList)this.gvPayment.Rows[rowindex].FindControl("ddlProject")).SelectedItem.Text;
                dt.Rows[rowindex]["rescode"] = ((DropDownList)this.gvPayment.Rows[rowindex].FindControl("ddlRescode")).SelectedValue.ToString();
                dt.Rows[rowindex]["resdesc"] = (((DropDownList)this.gvPayment.Rows[rowindex].FindControl("ddlRescode")).Items.Count == 0) ? "" : ((DropDownList)this.gvPayment.Rows[rowindex].FindControl("ddlRescode")).SelectedItem.Text;
                dt.Rows[rowindex]["billnature"] = ((DropDownList)this.gvPayment.Rows[rowindex].FindControl("ddlBillNature")).SelectedValue.ToString();
                dt.Rows[rowindex]["billndesc"] = ((DropDownList)this.gvPayment.Rows[rowindex].FindControl("ddlBillNature")).SelectedItem.Text;
                dt.Rows[rowindex]["paycode"] = ((DropDownList)this.gvPayment.Rows[rowindex].FindControl("ddlPartyName")).SelectedValue.ToString();
                dt.Rows[rowindex]["paydesc"] = ((DropDownList)this.gvPayment.Rows[rowindex].FindControl("ddlPartyName")).SelectedItem.Text;
                dt.Rows[rowindex]["refno"] = ((TextBox)this.gvPayment.Rows[rowindex].FindControl("txteditRefno")).Text.Trim();
                dt.Rows[rowindex]["apppaydate"] = ((TextBox)this.gvPayment.Rows[rowindex].FindControl("txteditpaymentdate")).Text.Trim();
                dt.Rows[rowindex]["valdate"] = ((Label)this.gvPayment.Rows[rowindex].FindControl("lbleditValDate")).Text.Trim();
                dt.Rows[rowindex]["amt"] = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[rowindex].FindControl("txteditBillAmount")).Text.Trim()).ToString("#,##0.00;(#,##0.00);");
                dt.Rows[rowindex]["advamt"] = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[rowindex].FindControl("txteditAdvAmt")).Text.Trim()).ToString("#,##0.00;(#,##0.00);");
                dt.Rows[rowindex]["netamt"] = Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[rowindex].FindControl("txteditBillAmount")).Text.Trim()) - Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[rowindex].FindControl("txteditAdvAmt")).Text.Trim());

                ViewState["tblpayment"] = dt;
                this.gvPayment.EditIndex = -1;
                this.Data_Bind();

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }




        private DataTable HiddenSameData(DataTable dt)
        {

            //string slnum = dt.Rows[0]["slnum"].ToString();
            string paydate = dt.Rows[0]["apppaydate"].ToString();
            string billno = dt.Rows[0]["billno"].ToString();
            for (int j = 1; j < dt.Rows.Count; j++)
            {
                if (billno == dt.Rows[j]["billno"].ToString())
                {
                    if (paydate != Convert.ToDateTime(dt.Rows[j]["apppaydate"].ToString()).ToString("dd-MMM-yyyy"))
                        dt.Rows[j]["billamt1"] = 0.00;
                    dt.Rows[j]["mslnum1"] = "";

                }

                //if(chkSinglIssue.Checked)
                //{


                //    dt.Rows[j]["mslnum1"] = "";
                //    dt.Rows[j]["slnum"] = "";


                //}



                billno = dt.Rows[j]["billno"].ToString();
                paydate = dt.Rows[j]["apppaydate"].ToString();
            }

            return dt;


        }


        protected void Add_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["tblpayment"];
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;


            string slnum = dt.Rows[RowIndex]["slnum"].ToString();
            string mslnum = dt.Rows[RowIndex]["mslnum"].ToString();
            DataRow[] dr2 = dt.Select("slnum='" + slnum + "'");

            this.lblmslnum.Text = mslnum;
            //mslnum = this.IncrmentMSlNum();
            //this.lblmslnum.Text = mslnum;



            //int indexz = gvr.RowIndex; // this is count start 1,2,3,4,5,6
            //int indexy = dt.Rows.Count - 1; // this is count start 1,2,3,4,5,6

            //if (indexz == indexy)
            //{

            //    ((LinkButton)this.gvPayment.Rows[indexz].FindControl("lbok")).Style["display"] = "none";
            //}


            this.lblslnum.Text = slnum;
            slnum = this.IncrmentSlNum();
            this.lblslnum.Text = slnum;

            DataRow dr1 = dt.NewRow();
            dr1["mslnum"] = mslnum;
            dr1["mslnum1"] = mslnum;
            dr1["slnum"] = slnum;
            dr1["rcvdate"] = this.txtReceiveDate.Text;
            dr1["billnature"] = "";
            dr1["billndesc"] = "";

            dr1["actcode"] = dr2[0]["actcode"].ToString();
            dr1["actdesc"] = dr2[0]["actdesc"].ToString();
            dr1["rescode"] = dr2[0]["rescode"].ToString();
            dr1["resdesc"] = dr2[0]["resdesc"].ToString();
            dr1["paycode"] = "";
            dr1["paydesc"] = "";
            dr1["refno"] = dr2[0]["refno"].ToString(); ;
            dr1["billno"] = dr2[0]["billno"].ToString();
            dr1["billno1"] = dr2[0]["billno1"].ToString();
            dr1["apppaydate"] = Convert.ToDateTime(dr2[0]["apppaydate"].ToString()).ToString("dd-MMM-yyyy");
            dr1["valdate"] = Convert.ToDateTime(dr2[0]["valdate"].ToString()).ToString("dd-MMM-yyyy");
            // dr1["chqdate"] = Convert.ToDateTime(dr2[0]["chqdate"].ToString()).ToString("dd-MMM-yyyy");
            dr1["billamt"] = dr2[0]["billamt"].ToString();
            dr1["billamt1"] = 0.00;
            dr1["amt"] = dr2[0]["amt"].ToString();
            dr1["advamt"] = 0.00;
            dr1["netamt"] = dr2[0]["netamt"].ToString();
            dt.Rows.Add(dr1);


            string billno1 = dr2[0]["billno"].ToString();


            for (int i = RowIndex + 1; i < dt.Rows.Count - 1; i++)
            {

                mslnum = (billno1 == dt.Rows[i]["billno"].ToString()) ? this.lblmslnum.Text : this.IncrmentMSlNum();
                this.lblmslnum.Text = mslnum;
                slnum = (billno1 == dt.Rows[i]["billno"].ToString()) ? this.lblslnum.Text : this.IncrmentSlNum();
                this.lblslnum.Text = slnum;
                dt.Rows[i]["mslnum"] = this.lblmslnum.Text;//ASTUtility.Right(("000000000" + (Convert.ToDouble(slnum) + i)), 9);
                dt.Rows[i]["slnum"] = this.lblslnum.Text;//ASTUtility.Right(("000000000" + (Convert.ToDouble(slnum) + i)), 9);
                billno1 = dt.Rows[i]["billno"].ToString();
            }


            DataView dv = dt.DefaultView;
            dv.Sort = "slnum";

            ViewState["tblpayment"] = this.HiddenSameData(dv.ToTable());
            this.Data_Bind();


            //string billno1 = dt.Rows[0]["valfield"].ToString();

            //string mslnum = (dt.Rows.Count == 0) ? this.GetMSlNum() : this.IncrmentMSlNum();
            //this.lblmslnum.Text = mslnum;
            //string slnum = (dt1.Rows.Count == 0) ? this.GetSlNum() : this.IncrmentSlNum();
            //this.lblslnum.Text = slnum;
            ////DataRow dr1 = dt.NewRow();
            //DataRow[] dr2 = (((DataTable)ViewState["tblpayment"]).Select("billno='" + BillList + "'"));
            //if (dr2.Length == 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        DataRow dr1 = dt1.NewRow();
            //        mslnum = (billno1 == dt.Rows[i]["valfield"].ToString()) ? this.lblmslnum.Text : this.IncrmentMSlNum();
            //        this.lblmslnum.Text = mslnum;

            //        slnum = (billno1 == dt.Rows[i]["valfield"].ToString()) ? this.lblslnum.Text : this.IncrmentSlNum();
            //        this.lblslnum.Text = slnum;
            //        dr1["mslnum"] = this.lblmslnum.Text;//ASTUtility.Right(("000000000" + (Convert.ToDouble(slnum) + i)), 9);
            //        dr1["slnum"] = this.lblslnum.Text;//ASTUtility.Right(("000000000" + (Convert.ToDouble(slnum) + i)), 9);
            //        dr1["rcvdate"] = this.txtReceiveDate.Text;
            //        dr1["billnature"] = "";
            //        dr1["billndesc"] = "";

            //        dr1["actcode"] = dt.Rows[i]["pactcode"].ToString();
            //        dr1["actdesc"] = dt.Rows[i]["pactdesc"].ToString();
            //        dr1["rescode"] = dt.Rows[i]["sircode"].ToString();
            //        dr1["resdesc"] = dt.Rows[i]["sirdesc"].ToString();
            //        dr1["paycode"] = "";
            //        dr1["paydesc"] = "";
            //        dr1["refno"] = dt.Rows[i]["billref"].ToString();
            //        dr1["billno"] = dt.Rows[i]["valfield"].ToString();
            //        //string textfield = dt.Rows[i]["textfield"].ToString();
            //        dr1["billno1"] = dt.Rows[i]["textfield"].ToString();
            //        dr1["apppaydate"] = Convert.ToDateTime(ASTUtility.DateFormat(ASTUtility.DateInVal(this.txtReceiveDate.Text))).AddDays(3).ToString("dd.MM.yyyy");

            //        dr1["valdate"] = Convert.ToDateTime(dt.Rows[i]["billdat"].ToString()).ToString("dd-MMM-yyyy");
            //        dr1["chqdate"] = Convert.ToDateTime(dt.Rows[i]["billdat"].ToString()).ToString("dd-MMM-yyyy");

            //        dr1["billamt"] = dt.Rows[i]["amt"].ToString();
            //        dr1["amt"] = dt.Rows[i]["amt"].ToString();
            //        dr1["advamt"] = 0.00;
            //        dr1["netamt"] = dt.Rows[i]["amt"].ToString();
            //        dt1.Rows.Add(dr1);

            //        billno1 = dt.Rows[i]["valfield"].ToString();
            //    }
            //}




        }
        protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                // HyperLink hlink2 = (HyperLink)e.Row.FindControl("lbok");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno1")).ToString();

                //int a = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "mslnum1"));
                //int b = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "mslnum1"));

                string billtyp = ASTUtility.Left(billno, 3).ToString();





                //if (a == b-1)
                //{
                //    ((LinkButton)e.Row.FindControl("lbok")).Style["display"] = "none";
                //    //((LinkButton)this.gvPayment.Rows[indexz].FindControl("lbok")).Style["display"] = "none";
                //}



                if (billtyp == "GBL")
                {
                    e.Row.FindControl("lbok").Visible = false;
                }
                else if (billtyp == "PBL" || billtyp == "CBL" || billtyp == "POR")
                {
                    e.Row.FindControl("lbok").Visible = true;
                }

                if (chkSinglIssue.Checked)
                {
                    e.Row.FindControl("lbok").Visible = false;
                }

            }

        }
        protected void ddlResourceHead_SelectedIndexChanged(object sender, EventArgs e)
        {

            //  DataTable dt=(DataTable)Session["tblressourcehead"];
            foreach (ListItem litem in ddlResourceHead.Items)
            {

                string rescode = litem.Value;

                //string Link = (.Select("actcode='" + item + "'"))[0]["link"].ToString();
                if (rescode == "980000000000" || rescode == "990000000000" || rescode == "GBL000000000")
                {
                    litem.Attributes.Add("style", "background-color:#a3ffa3");
                }

            }

            // Session["tblressourcehead"] = ds1.Tables[0];
            this.GetSelectedBillNo();
        }
        protected void chkSinglIssue_CheckedChanged(object sender, EventArgs e)
        {


        }



    }
}