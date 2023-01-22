using System;
using System.Collections;
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
namespace RealERPWEB.F_32_Mis
{
    public partial class BankBalance : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

            if (this.txtFromdat.Text.Trim().Length == 0)
            {
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtFromdat.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                this.txtTodat.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtFromdat.Text = DateTime.Today.AddDays(-90).ToString("dd-MMM-yyyy ddd");
                //this.txtTodat.Text = DateTime.Today.ToString("dd-MMM-yyyy ddd");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            }

        }

        private DataSet GetDataForReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string date1 = this.txtFromdat.Text.Substring(0, 11);
            string date2 = this.txtTodat.Text.Substring(0, 11);
            string level = this.ddlRptlbl0.SelectedItem.Value;
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_MIS01", "RPTBANKBALANCE", date1, date2, level,
                          "", "", "", "", "", "");
            return ds2;
        }
        protected void lnkok_Click(object sender, EventArgs e)
        {
            if (this.txtFromdat.Text == "" && this.txtTodat.Text == "")
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please select from date and to date.";
                return;
            }// End If

            try
            {
                this.dgv2.DataSource = null;
                this.dgv2.DataBind();
                DataSet ds2 = GetDataForReport();
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "There is no Transaction in this Accounts Code.";
                    this.lblmsg.ForeColor = System.Drawing.Color.Blue;
                    return;
                }
                this.dgv2.DataSource = ds2.Tables[0];
                this.dgv2.DataBind();
                ((Label)this.dgv2.FooterRow.FindControl("lblfopnamt")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0); ") + "<br>" + Convert.ToDouble(ds2.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0); ");
                ((Label)this.dgv2.FooterRow.FindControl("lblfDramt")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["dram"]).ToString("#,##0;(#,##0); ");
                ((Label)this.dgv2.FooterRow.FindControl("lblfCramt")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["cram"]).ToString("#,##0;(#,##0); ");
                ((Label)this.dgv2.FooterRow.FindControl("lblfcloamt")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ") + "<br>" + Convert.ToDouble(ds2.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ") + "<br>" + Convert.ToDouble(ds2.Tables[1].Rows[0]["closam"]).ToString("#,##0;(#,##0); ");
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }

            if (ConstantInfo.LogStatus == true)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string eventtype = "Bank Balance";
                string eventdesc = "Show Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataSet ds2 = GetDataForReport();
            if (ds2 == null)
                return;
            ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptMISBankBalance();

            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "(From " + Convert.ToDateTime(this.txtFromdat.Text.Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtTodat.Text.Trim()).ToString("dd-MMM-yyyy") + ")";



            TextObject txtopeingname1 = rptstk.ReportDefinition.ReportObjects["opeingname1"] as TextObject;
            txtopeingname1.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0); ");
            TextObject txtopeingname2 = rptstk.ReportDefinition.ReportObjects["opeingname2"] as TextObject;
            txtopeingname2.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0); "); ;

            TextObject txtdramount = rptstk.ReportDefinition.ReportObjects["dramount"] as TextObject;
            txtdramount.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["dram"]).ToString("#,##0;(#,##0); ");

            TextObject txtcramount = rptstk.ReportDefinition.ReportObjects["cramount"] as TextObject;
            txtcramount.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["cram"]).ToString("#,##0;(#,##0); "); ;

            TextObject txtclosingamount1 = rptstk.ReportDefinition.ReportObjects["closingamount1"] as TextObject;
            txtclosingamount1.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");

            TextObject txtclosingamount2 = rptstk.ReportDefinition.ReportObjects["closingamount2"] as TextObject;
            txtclosingamount2.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ");

            TextObject rpttxtbalamt = rptstk.ReportDefinition.ReportObjects["txtbalamt"] as TextObject;
            rpttxtbalamt.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["closam"]).ToString("#,##0;(#,##0); ");


            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            if (ConstantInfo.LogStatus == true)
            {
                string comcod = hst["comcod"].ToString();
                string eventtype = "Bank Balance";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            rptstk.SetDataSource(ds2.Tables[0]);
            Session["Report1"] = rptstk;
            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        //protected void dgv2_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType != DataControlRowType.DataRow)
        //        return;
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
        //    string mCOMCOD = comcod;
        //    string mACTCODE = ((Label)e.Row.FindControl("lblgvCode")).Text;
        //    string mTRNDAT1 = this.txtFromdat.Text;
        //    string mTRNDAT2 = this.txtTodat.Text;

        //    if (ASTUtility.Right(mACTCODE, 4) == "0000")
        //        hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
        //             "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
        //    else
        //        hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
        //                 "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
        //}














    }
}