using System;
using System.Linq;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.IO;
using System.Collections.Generic;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_17_Acc
{

    public partial class AccRpCodeRecon : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Transaction Link";
                //this.Master.Page.Title = "Transaction Link";
                this.GetRpCode();
                if (this.TxtDate1.Text.Trim().Length == 0)
                {
                    string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.TxtDate1.Text = "01" + date.Substring(2);
                    this.TxtDate2.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                }
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(btnGetDataP_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        protected void lbtnGetData_Click(object sender, EventArgs e)
        {
            Session.Remove("tbl01r");
            string mCOMCOD = this.GetCompCode();
            string mTRNDAT1 = this.TxtDate1.Text;
            string mTRNDAT2 = this.TxtDate2.Text;
            string chqno = this.txtChqSearch.Text + "%";
            string grprpt = ((this.rbtnGroup.SelectedIndex == 0) ? "A" : (this.rbtnGroup.SelectedIndex == 1) ? "B" : "") + "%";
            string rpname = ((this.ddlRpName.SelectedValue == "BBBBBBB") ? "" : this.ddlRpName.SelectedValue.ToString()) + "%";
            //string chequeno = this.AssenCheque.Checked ? "chequeno" : "";

            string ordering = (this.rbtnordering.SelectedIndex == 0) ? "Issueno" : (this.rbtnGroup.SelectedIndex == 2) ? "hondate" : "";

            string range = this.ddlRange.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_ENTRY_ACCOUNTS_VOUCHER", "SHOWRPCODRECON", "", mTRNDAT1, mTRNDAT2, chqno, grprpt, rpname, ordering, range, "");
            if (ds1 == null)
            {
                this.gv1.DataSource = null;
                this.gv1.DataBind();
                return;
            }
            int RptGroup = this.rbtnGroup.SelectedIndex;

            // switch (RptGroup) 
            //{
            //    case 0:
            //         dv1 =ds1.Tables[0].DefaultView;
            //         dv1.RowFilter = ("grp = 'A'");
            //        break;
            //    case 1:
            //         dv1 = ds1.Tables[0].DefaultView;
            //         dv1.RowFilter = ("grp = 'B'");
            //        break;

            //    case 2:
            //        dv1 = ds1.Tables[0].DefaultView;
            //        break;
            //}
            this.gv1.Columns[6].Visible = (this.rbtnGroup.SelectedIndex == 0) ? false : true;
            Session["tbl01r"] = ds1.Tables[0];
            this.gv1_DataBind();
        }
        private void Footer_Calculation()
        {
            DataTable tblt05 = ((DataTable)Session["tbl01r"]).Copy();
            DataView dv = tblt05.DefaultView;
            dv.RowFilter = ("rescode like 'AAAAAAAAAAAA'");
            tblt05 = dv.ToTable();
            if (tblt05.Rows.Count == 0)
                return;

            ((Label)this.gv1.FooterRow.FindControl("lgvFDpAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(depam)", "")) ?
              0.00 : tblt05.Compute("Sum(depam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1.FooterRow.FindControl("lgvFPayAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(payam)", "")) ?
              0.00 : tblt05.Compute("Sum(payam)", ""))).ToString("#,##0;(#,##0); ");

        }
        //protected void btnGetDataP_Click(object sender, EventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string fromdate = Convert.ToDateTime(this.TxtDate1.Text).ToString("dd-MMM-yyyy");
        //    string todate = Convert.ToDateTime(this.TxtDate2.Text).ToString("dd-MMM-yyyy");
        //    ReportDocument rptstate = new RealERPRPT.R_17_Acc.RptTransactionLink();
        //    TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
        //    rptCname.Text = comnam;

        //    TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
        //    rptftdate.Text = "(From  " + fromdate + " To " + todate+")";

        //    TextObject txtgrandtotal = rptstate.ReportDefinition.ReportObjects["txtgrandtotal"] as TextObject;
        //    txtgrandtotal.Text = ((Label)this.gv1.FooterRow.FindControl("lgvFPayAmt")).Text;
        //    TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
        //    rptstate.SetDataSource((DataTable)Session["tbl01r"]);       
        //    Session["Report1"] = rptstate;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        //}


        protected void btnGetDataP_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            //string Type = Request.QueryString["Type"].ToString();
            //string rpthead = (Type == "ImpPlan" ? "Monthly Implementation Plan" : (Type == "Execution" ? "Work Execution" : "Monthly Plan Vs. Execution"));

            string fromdate = Convert.ToDateTime(this.TxtDate1.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.TxtDate2.Text).ToString("dd-MMM-yyyy");

            string session = hst["session"].ToString();

            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tbl01r"];
            DataTable dt2 = (DataTable)Session["tbl01r"];


            DataView dv = dt2.DefaultView;
            dv.RowFilter = ("rescode like 'AAAAAAAAAAAA'");
            dt2 = dv.ToTable();

            string depam = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(depam)", "")) ?
              0.00 : dt2.Compute("Sum(depam)", ""))).ToString("#,##0;(#,##0); ");
            string payam = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(payam)", "")) ?
              0.00 : dt2.Compute("Sum(payam)", ""))).ToString("#,##0;(#,##0); ");


            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptTransactionLink>();



            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptTransactionLink", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("todate", "(From: " + Convert.ToDateTime(this.TxtDate1.Text).ToString("dd-MMM-yyyy") + "  To : " + Convert.ToDateTime(this.TxtDate2.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("txtdepam", depam));
            Rpt1.SetParameters(new ReportParameter("txtpayam", payam));

            Rpt1.SetParameters(new ReportParameter("RptTitle", "Transaction Link"));

            Rpt1.SetParameters(new ReportParameter("RptTitle", "Transaction Link"));

            Rpt1.SetParameters(new ReportParameter("RptFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }








        protected void lbtnGetBankList_Click(object sender, EventArgs e)
        {

        }


        protected void gv1_DataBind()
        {

            this.gv1.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gv1.DataSource = (DataTable)Session["tbl01r"];
            this.gv1.DataBind();
            Session["Report1"] = gv1;
            if (((DataTable)Session["tbl01r"]).Rows.Count > 0)
                ((HyperLink)this.gv1.HeaderRow.FindControl("hlbtnbtbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            this.Footer_Calculation();
        }
        protected void gv1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gv1.PageIndex = e.NewPageIndex;
            this.gv1_DataBind();
        }

        //protected void lbtnUpdate_Click(object sender, EventArgs e)
        //{
        //    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
        //    if (!Convert.ToBoolean(dr1[0]["entry"]))
        //    {
        //     ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
        //        return;
        //    }

        //    //this.SaveValue();
        //    string comcod = this.GetCompCode();
        //    DataTable dt = ((DataTable)Session["tbl01r"]);

        //    for (int i = 0; i <dt.Rows.Count; i++)
        //    {
        //        string mVOUNUM = dt.Rows[i]["vounum"].ToString();
        //        string mACTCODE = dt.Rows[i]["actcode"].ToString();
        //        string mSUBCODE = dt.Rows[i]["rescode"].ToString();
        //        string mCACTCODE = dt.Rows[i]["cactcode"].ToString();
        //        //string mRECNDT1 =Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy");  
        //        string mUserID = "000000";
        //        string rpcode = (dt.Rows[i]["rpcode"].ToString() == "0000000") ? "" : dt.Rows[i]["rpcode"].ToString();
        //        bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATERPCODRECON", "", mVOUNUM, mACTCODE, mSUBCODE, mCACTCODE, rpcode, "", "", "", "", "", "", "", "", mUserID);

        //        if (!result)
        //        {
        //         ((Label)this.Master.FindControl("lblmsg")).Text = "Invalid date";
        //            return;
        //        }
        //    }

        // ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

        //    if (ConstantInfo.LogStatus == true)
        //    {
        //        string eventtype = "Bank Reconcilaition";
        //        string eventdesc = "Update Reconcilaition";
        //        string eventdesc2 = "Bank Name: " + this.DDListBank.SelectedItem.ToString().Substring(13);
        //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //    }

        //}
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.SaveValue();
            this.gv1_DataBind();
        }
        protected void gv1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gv1.EditIndex = -1;
            this.gv1_DataBind();
        }
        protected void gv1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gv1.EditIndex = e.NewEditIndex;
            this.gv1_DataBind();
            int rowindex = (gv1.PageSize) * (this.gv1.PageIndex) + e.NewEditIndex;

            string rpcode = ((DataTable)Session["tbl01r"]).Rows[rowindex]["rpcode"].ToString();

            DropDownList ddl2 = (DropDownList)this.gv1.Rows[e.NewEditIndex].FindControl("ddlRpName");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETRPCODE", "%%", "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "rpdesc";
            ddl2.DataValueField = "rpcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
            ddl2.SelectedValue = rpcode;
            //ddl2.Visible = true;

            //ddl2.Visible = false;
            //ddl2.Items.Clear();

        }

        protected void gv1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //DataTable tbl1 = (DataTable)Session["tbl01r"];
            string gvrpcode = ((DropDownList)this.gv1.Rows[e.RowIndex].FindControl("ddlRpName")).SelectedValue.ToString();
            string Rptdesc = ((DropDownList)this.gv1.Rows[e.RowIndex].FindControl("ddlRpName")).SelectedItem.ToString();
            string actcode = ((Label)this.gv1.Rows[e.RowIndex].FindControl("lbgractcode")).Text;
            string rescode = ((Label)this.gv1.Rows[e.RowIndex].FindControl("lbgrrescode")).Text;
            string cactcode = ((Label)this.gv1.Rows[e.RowIndex].FindControl("lbgrcactcode")).Text;
            string vounum = ((Label)this.gv1.Rows[e.RowIndex].FindControl("lbgrvounum")).Text;
            string mUserID = "000000";
            string rpcode = (gvrpcode.Trim() == "AAAAAAA") ? "" : gvrpcode.Trim();


            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATERPCODRECON", "", vounum, actcode, rescode, cactcode, rpcode, "", "", "", "", "", "", "", "", mUserID);

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Invalid date";
                return;
            }

         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Transaction Link";
                string eventdesc = "Update Transaction Link";
                string eventdesc2 = vounum;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            this.gv1.EditIndex = -1;
            this.lbtnGetData_Click(null, null);
        }

        protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblgvDetailsHead = (Label)e.Row.FindControl("lblgvDetailsHead");
                Label Deposit = (Label)e.Row.FindControl("lblgvDeposit");
                Label Payment = (Label)e.Row.FindControl("lblgvPayment");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lblgvDetailsHead.Font.Bold = true;
                    Deposit.Font.Bold = true;
                    Payment.Font.Bold = true;
                    // actdesc.Style.Add("text-align", "right");


                }

            }
        }

        private void GetRpCode()
        {

            string comcod = this.GetCompCode();
            string srchrpname = "%" + this.txtSearchName.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETRPCODE", srchrpname, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlRpName.Items.Clear();
                return;
            }


            DataTable dt = ds1.Tables[0];
            DataRow dr2 = dt.NewRow();
            dr2["rpcode"] = "BBBBBBB";
            dr2["rpdesc"] = "All Type";
            dt.Rows.Add(dr2);
            //DataView dv2 = dt.DefaultView;
            //dv2.Sort = "flrcod";
            //dt = dv2.ToTable();

            this.ddlRpName.DataTextField = "rpdesc";
            this.ddlRpName.DataValueField = "rpcode";
            this.ddlRpName.DataSource = dt;
            this.ddlRpName.DataBind();
            this.ddlRpName.SelectedValue = "BBBBBBB";
            ds1.Dispose();


        }

        protected void imgSearchRpName_Click(object sender, EventArgs e)
        {

            this.GetRpCode();


        }
    }
}
