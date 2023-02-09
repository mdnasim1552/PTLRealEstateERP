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
namespace RealERPWEB.F_29_Fxt
{
    public partial class RptFxtAsstBillRent : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "FIXED ASSET RENT BILL INFORMATION";
                this.txtfromdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy ddd");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyy ddd");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            }
            if (this.ddlProjectName.Items.Count == 0)
            {
                this.GetProjectName();
                this.GetBillNo();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }


        private string GetComcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;


        }
        private void GetBillNo()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            string comcod = this.GetComcod();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "NEWBILLNO", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.lblBillNo.Text = ds1.Tables[0].Rows[0]["billno"].ToString();

        }
        private void GetProjectName()
        {
            string comcod = this.GetComcod();
            string txtSProject = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GetProjectName", txtSProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectName.Text = this.ddlProjectName.SelectedItem.Text;
                this.TransferInfo();
                this.lblProjectName.Visible = true;
                this.ddlProjectName.Visible = false;
            }

            else
            {
                this.lbtnOk.Text = "Ok";
                this.lblProjectName.Visible = false;
                this.ddlProjectName.Visible = true;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                this.GetBillNo();

            }

        }

        private void TransferInfo()
        {
            Session.Remove("tbltransfer");
            string comcod = this.GetComcod();
            string ProjectName = this.ddlProjectName.SelectedValue.ToString();
            string Fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string Todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "BILLRENTINFO", ProjectName, Fromdate, Todate, "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }
            DataTable dt = ds2.Tables[0];
            Session["tbltransfer"] = dt;
            this.grvacc.DataSource = ds2.Tables[0];
            this.grvacc.DataBind();
            double Amount = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amount)", "")) ? 0.00 : dt.Compute("sum(amount)", "")));
            ((Label)this.grvacc.FooterRow.FindControl("lgvFAmount")).Text = Amount.ToString("#,##0;(#,##0); -");

        }

        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            string comcod = this.GetComcod();
            string ProjectCode = this.ddlProjectName.SelectedValue.ToString();
            string transno = this.lblBillNo.Text.Trim();
            DataTable dt = (DataTable)Session["tbltransfer"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string rsircode = dt.Rows[i]["rsircode"].ToString();
                string qty = dt.Rows[i]["qty"].ToString();
                string rcvdate = Convert.ToDateTime(dt.Rows[i]["rcvdate"]).ToString("dd-MMM-yyyy");
                string trnsdate = Convert.ToDateTime(dt.Rows[i]["trnsdate"]).ToString("dd-MMM-yyyy");
                string duratiion = dt.Rows[i]["duration"].ToString();
                string amount = dt.Rows[i]["amount"].ToString();

                bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "INSERTORUPDATEBILLINFO", transno, ProjectCode, rsircode, rcvdate,
                    trnsdate, qty, duratiion, amount, "", "", "", "", "", "", "");
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "FixedAsset Rent Bill";
                string eventdesc = "Updated Info";
                string eventdesc2 = transno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ////string UsirCode = this.lusircode.Text.Trim();
            ////string PactCode = this.lPactCode.Text.Trim();
            ////string mrno = this.ddlMRNO.SelectedValue.ToString();
            //DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSTALLMANTWITHMRR", PactCode, UsirCode, "", "", "", "", "", "", "");
            //DataTable dtstatus = ds2.Tables[0];
            //DataView dv1 = dtstatus.DefaultView;
            //dv1.RowFilter = "mrno='" + mrno + "'";
            //DataTable dtmr = dv1.ToTable();
            //string Installment = "";
            //for (int i = 0; i < dtmr.Rows.Count; i++)
            //{
            //    if (i == 0)
            //        Installment = Installment + dtmr.Rows[i]["gdesc"] + ", ";
            //    else if (dtmr.Rows[i - 1]["gdesc"].ToString().Trim() != dtmr.Rows[i]["gdesc"].ToString().Trim())
            //    {
            //        Installment = Installment + dtmr.Rows[i]["gdesc"] + ", ";
            //    }


            //}
            //int len = Installment.Length;
            //Installment = ASTUtility.Left(Installment, len - 2);
            //DataSet ds4 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "REPORTMONEYRECEIPT", PactCode, UsirCode, mrno, "", "", "", "", "", "");
            //if (ds4 == null)
            //    return;
            //DataTable dtrpt = ds4.Tables[0];
            //string custadd = dtrpt.Rows[0]["custadd"].ToString();
            //string custmob = dtrpt.Rows[0]["custmob"].ToString();
            //string udesc = dtrpt.Rows[0]["udesc"].ToString();
            //string usize = Convert.ToDouble(dtrpt.Rows[0]["usize"]).ToString("#,##0;(#,##0); -");
            //string munit = dtrpt.Rows[0]["munit"].ToString();
            //string paytype = dtrpt.Rows[0]["paytype"].ToString();
            //string chqno = dtrpt.Rows[0]["chqno"].ToString();
            //string bankname = dtrpt.Rows[0]["bankname"].ToString();
            //string branch = dtrpt.Rows[0]["bbranch"].ToString();
            //string paidamt = Convert.ToDouble(dtrpt.Rows[0]["paidamt"]).ToString("#,##0;(#,##0); -");
            //string refno = dtrpt.Rows[0]["refno"].ToString();
            //string custteam = dtrpt.Rows[0]["custteam"].ToString();
            //string rmrks = dtrpt.Rows[0]["rmrks"].ToString();

            //string amt = paidamt;
            //double amt1 = Convert.ToDouble(paidamt);
            //string amt1t = ASTUtility.Trans(amt1, 2);
            //string Typedes = "";
            //if (paytype == "CHEQUE")
            //{
            //    Typedes = paytype + ", " + "Cheque No: " + chqno + ", Bank: " + bankname + ", Branch: " + branch;

            //}
            //else
            //{

            //    Typedes = paytype;
            //}
            //ReportDocument rptMoneyRcpt = new RptMoneyReceipt();
            //TextObject rptCname = rptMoneyRcpt.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text =comnam;
            //TextObject rptCname1 = rptMoneyRcpt.ReportDefinition.ReportObjects["CompName1"] as TextObject;
            //rptCname1.Text = comnam;
            ////TextObject rptCId = rptMoneyRcpt.ReportDefinition.ReportObjects["custid"] as TextObject;
            ////rptCId.Text = UsirCode;
            ////TextObject rptCId1 = rptMoneyRcpt.ReportDefinition.ReportObjects["custid1"] as TextObject;
            ////rptCId1.Text = UsirCode;
            //TextObject rptCustAdd = rptMoneyRcpt.ReportDefinition.ReportObjects["CustAdd"] as TextObject;
            //rptCustAdd.Text = custadd + ", " + "Mobile: " + custmob;
            //TextObject rptCustAdd1 = rptMoneyRcpt.ReportDefinition.ReportObjects["CustAdd1"] as TextObject;
            //rptCustAdd1.Text = custadd + ", " + "Mobile: " + custmob;
            ////TextObject rptrefno = rptMoneyRcpt.ReportDefinition.ReportObjects["refno"] as TextObject;
            ////rptrefno.Text = refno;
            ////TextObject rptrefno1 = rptMoneyRcpt.ReportDefinition.ReportObjects["refno1"] as TextObject;
            ////rptrefno1.Text = refno;
            //TextObject rptcteam = rptMoneyRcpt.ReportDefinition.ReportObjects["custteam"] as TextObject;
            //rptcteam.Text = "Received by: "+custteam;
            //TextObject rptcteam1 = rptMoneyRcpt.ReportDefinition.ReportObjects["custteam1"] as TextObject;
            //rptcteam1.Text ="Received by: " +custteam;
            //TextObject rptrmrks = rptMoneyRcpt.ReportDefinition.ReportObjects["rmrks"] as TextObject;
            //rptrmrks.Text ="Remarks: " +rmrks;
            //TextObject rptrmrks1 = rptMoneyRcpt.ReportDefinition.ReportObjects["rmrks1"] as TextObject;
            //rptrmrks1.Text = "Remarks: " + rmrks;

            //TextObject rptUsize = rptMoneyRcpt.ReportDefinition.ReportObjects["usize"] as TextObject;
            //rptUsize.Text = udesc + ", " + usize + " " + munit;
            //TextObject rptUsize1 = rptMoneyRcpt.ReportDefinition.ReportObjects["usize1"] as TextObject;
            //rptUsize1.Text = udesc + ", " + usize + " " + munit;
            //TextObject rptvalue = rptMoneyRcpt.ReportDefinition.ReportObjects["amount"] as TextObject;
            //rptvalue.Text = "TK." + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0);");
            //TextObject rptvalue1 = rptMoneyRcpt.ReportDefinition.ReportObjects["amount1"] as TextObject;
            //rptvalue1.Text = "TK." + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0);");
            //TextObject rptamtinward = rptMoneyRcpt.ReportDefinition.ReportObjects["takainword"] as TextObject;
            //rptamtinward.Text = amt1t + " " + "AS " + Installment;
            //TextObject rptamtinward1 = rptMoneyRcpt.ReportDefinition.ReportObjects["takainword1"] as TextObject;
            //rptamtinward1.Text = amt1t;
            //TextObject rptptype = rptMoneyRcpt.ReportDefinition.ReportObjects["paytype"] as TextObject;
            //rptptype.Text = Typedes;
            //TextObject rptptype1 = rptMoneyRcpt.ReportDefinition.ReportObjects["paytype1"] as TextObject;
            //rptptype1.Text = Typedes;
            //TextObject txtuserinfo = rptMoneyRcpt.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //TextObject txtuserinfo1 = rptMoneyRcpt.ReportDefinition.ReportObjects["txtuserinfo1"] as TextObject;
            //txtuserinfo1.Text = ASTUtility.Concat(compname, username, printdate);
            //TextObject txtcominfo = rptMoneyRcpt.ReportDefinition.ReportObjects["txtcominfo"] as TextObject;
            //txtcominfo.Text = ASTUtility.Cominformation();
            //TextObject txtcominfo1 = rptMoneyRcpt.ReportDefinition.ReportObjects["txtcominfo1"] as TextObject;
            //txtcominfo1.Text = ASTUtility.Cominformation();
            //rptMoneyRcpt.SetDataSource(ds4.Tables[0]);
            //Session["Report1"] = rptMoneyRcpt;
            //lbljavascript.Text = @"<script>window.open('RptViewer.aspx');</script>";
        }





    }
}











