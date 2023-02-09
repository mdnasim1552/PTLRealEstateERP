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
namespace RealERPWEB.F_81_Hrm.F_90_PF
{
    public partial class AccBankRecon : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                if (this.TxtDate1.Text.Trim().Length == 0)
                {
                    this.TxtDate1.Text = DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
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

        protected void lbtnGetData_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = hst["comcod"].ToString();
            string mACCODE = this.DDListBank.SelectedValue.ToString();
            string mTRNDAT1 = this.TxtDate1.Text;
            string mTRNDAT2 = this.TxtDate2.Text;

            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_ENTRY_ACCOUNTS_VOUCHER", "SHOWBANKRECON",
                        mACCODE, mTRNDAT1, mTRNDAT2, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count == 0)
                return;

            this.LblReportTitle.Text = "BANK RECONCILIATION - " + this.DDListBank.SelectedItem.Text.Trim();
            this.LblReportPeriod.Text = "(From " + mTRNDAT1 + " to " + mTRNDAT2 + ")";
            Session["tbl01r"] = ds1.Tables[0];
            gv1.EditIndex = -1;
            gv1.PageIndex = 0;
            this.gv1_DataBind();
        }

        protected void btnGetDataP_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = hst["comcod"].ToString();
            string mACCODE = this.DDListBank.SelectedValue.ToString();
            string mTRNDAT1 = this.TxtDate1.Text;
            string mTRNDAT2 = this.TxtDate2.Text;

            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_TRANS", "PRINTBANKRECON",
                   mACCODE, mTRNDAT1, mTRNDAT2, "", "", "", "", "", "");

            //ReportDocument RptBankRecon = new RealERPRPT.R_15_Acc.RptBankRecon();
            ReportDocument RptBankRecon = new RealERPRPT.R_17_Acc.RptBankReconc();

            RptBankRecon.SetDataSource(ds1.Tables[0]);

            TextObject TxtCompName = RptBankRecon.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            TxtCompName.Text = Convert.ToString(ds1.Tables[1].Rows[0]["comnam"]);

            TextObject TxtRptTitle = RptBankRecon.ReportDefinition.ReportObjects["TxtRptTitle"] as TextObject;
            TxtRptTitle.Text = "BANK RECONCILIATION - [" + this.DDListBank.SelectedItem.Text.Trim() + "]";

            TextObject TxtRptPeriod = RptBankRecon.ReportDefinition.ReportObjects["TxtRptPeriod"] as TextObject;
            TxtRptPeriod.Text = "(As on " + mTRNDAT2 + ")";

            //--------------------Export to PDF--------------------------------------------------
            Session["Report1"] = RptBankRecon;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //Response.Redirect("PDFViewer.aspx");   
        }

        protected void lbtnGetBankList_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = hst["comcod"].ToString();
            string mFILTERSTR = "";

            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONACCHEAD",
                   "", "", "", "", "", "", "", "", "");

            DataView dv1 = ds1.Tables[0].DefaultView;
            dv1.RowFilter = "actdesc1 like '%" + mFILTERSTR + "%'";
            this.DDListBank.DataTextField = "actdesc1";
            this.DDListBank.DataValueField = "actcode";
            this.DDListBank.DataSource = dv1;
            this.DDListBank.DataBind();
        }

        protected void gv1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv1.EditIndex = e.NewEditIndex;
            this.gv1_DataBind();
        }

        protected void gv1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            //try
            {
                string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string mCOMCOD = hst["comcod"].ToString();
                string mVOUNUM = ((Label)gv1.Rows[e.RowIndex].FindControl("lblVOUNUM")).Text.Trim();
                string mACTCODE = ((Label)gv1.Rows[e.RowIndex].FindControl("lblACTCODE")).Text.Trim();
                string mSUBCODE = ((Label)gv1.Rows[e.RowIndex].FindControl("lblSUBCODE")).Text.Trim();
                string mCACTCODE = ((Label)gv1.Rows[e.RowIndex].FindControl("lblCACTCODE")).Text.Trim();
                string mRECNDT1 = ((TextBox)gv1.Rows[e.RowIndex].FindControl("txtRECNDT")).Text.Trim() + "01.01.1900";
                string mCOMCOD1 = ((Label)gv1.Rows[e.RowIndex].FindControl("lblgvCOMCOD")).Text.Trim();

                mRECNDT1 = mRECNDT1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(mRECNDT1.Substring(3, 2))] + "-" + mRECNDT1.Substring(6, 4);
                DateTime mRECNDT = DateTime.Parse(mRECNDT1);
                bool vuchr = ((CheckBox)gv1.Rows[e.RowIndex].FindControl("chkVoucher")).Checked;

                int TblRowIndex = (gv1.PageIndex) * gv1.PageSize + e.RowIndex;
                DataTable tbl1 = ((DataTable)Session["tbl01r"]);
                if (vuchr)
                {
                    DataView dv1 = tbl1.DefaultView;
                    dv1.RowFilter = "VOUNUM like '" + mVOUNUM + "%'";
                    for (int i = 0; i < dv1.Count; i++)
                    {
                        dv1[i]["RECNDT"] = mRECNDT;
                        dv1[i]["OLDNEW"] = "NEW";
                    }
                    dv1.RowFilter = "";
                    mACTCODE = "VOUCHER";
                }
                else
                {
                    tbl1.Rows[TblRowIndex]["RECNDT"] = mRECNDT;
                    tbl1.Rows[TblRowIndex]["OLDNEW"] = "NEW";
                }

                string mUserID = "000000"; // hst["usrid"].ToString(); 
                string msg = "";
                bool result = accData.UpdateTransInfo(mCOMCOD, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEBANKRECON",
                          mRECNDT1, mVOUNUM, mACTCODE, mSUBCODE, mCACTCODE, "", "", "",
                          "", "", "", "", "", "", mUserID);


                if (!result)
                    //this.lblError.Text = "Invalid date";
                  
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);



                Session["tbl01r"] = tbl1;
            }
            //catch (Exception exp)
            //{
            //    this.lblError.Text = exp.Message.ToString();
            //}
            gv1.EditIndex = -1;
            this.gv1_DataBind();
        }

        protected void gv1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv1.EditIndex = -1;
            this.gv1_DataBind();
        }

        protected void gv1_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["tbl01r"];
            this.gv1.DataSource = tbl1;
            this.gv1.DataBind();
            if (tbl1.Rows.Count == 0)
                return;

            //((DropDownList)this.gv1.HeaderRow.FindControl("ddlPageNo")).Visible = false;
            //double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.gv1.PageSize);
            //((DropDownList)this.gv1.HeaderRow.FindControl("ddlPageNo")).Items.Clear();
            //for (int i = 1; i <= TotalPage; i++)
            //    ((DropDownList)this.gv1.HeaderRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            //if (TotalPage > 1)
            //    ((DropDownList)this.gv1.HeaderRow.FindControl("ddlPageNo")).Visible = true;
            //((DropDownList)this.gv1.HeaderRow.FindControl("ddlPageNo")).SelectedIndex = this.gv1.PageIndex;
        }

        //protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.gv1.PageIndex = ((DropDownList)this.gv1.HeaderRow.FindControl("ddlPageNo")).SelectedIndex;
        //    this.gv1.EditIndex = -1;
        //    this.gv1_DataBind();
        //}
        protected void gv1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gv1.PageIndex = e.NewPageIndex;
            this.gv1_DataBind();
        }
    }
}
