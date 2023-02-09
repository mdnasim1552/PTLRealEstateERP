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
using System.IO;
using Microsoft.Reporting.WinForms;
using RealERPRPT;
using Excel = Microsoft.Office.Interop.Excel;
namespace RealERPWEB.F_17_Acc
{


    public partial class AccBankRecon : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                {
                    int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                    if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                        Response.Redirect("../AcceessError.aspx");
                    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                    ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                    //((Label)this.Master.FindControl("lblTitle")).Text = "Bank Reconcilation";
                    //this.Master.Page.Title = "Bank Reconcilation";
                    ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                    this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                }
                this.GetBankName();
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

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        protected void ibtnFindBankName_Click(object sender, EventArgs e)
        {
            if (this.lbtnGetData.Text == "New")
                return;
            this.GetBankName();
        }
        private void GetBankName()
        {

            string mCOMCOD = this.GetCompCode();
            string mFILTERSTR = this.txtBankSearch.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBANKNAME", mFILTERSTR, "", "", "", "", "", "", "", "");
            this.DDListBank.DataTextField = "actdesc1";
            this.DDListBank.DataValueField = "actcode";
            this.DDListBank.DataSource = ds1.Tables[0];
            this.DDListBank.DataBind();
        }

        protected void lbtnGetData_Click(object sender, EventArgs e)
        {
            if (this.lbtnGetData.Text == "Ok")
            {
                this.lbtnGetData.Text = "New";
                this.txtBankSearch.Enabled = false;
                this.btnExcel.Visible = true;
                this.ibtnFindBankName.Enabled = false;
                this.DDListBank.Enabled = false;
                this.TxtDate1.Enabled = false;
                this.TxtDate2.Enabled = false;
                this.LoadData();
            }
            else
            {
                this.lbtnGetData.Text = "Ok";
                this.btnExcel.Visible = false;
                this.txtBankSearch.Enabled = true;
                this.ibtnFindBankName.Enabled = true;
                this.DDListBank.Enabled = true;
                this.TxtDate1.Enabled = true;
                this.TxtDate2.Enabled = true;
                this.gv1.DataSource = null;
                this.gv1.DataBind();
            }
        }
        private void LoadData()
        {
            Session.Remove("tbl01r");
            string mCOMCOD = this.GetCompCode();
            string mACCODE = this.DDListBank.SelectedValue.ToString();
            string mTRNDAT1 = this.TxtDate1.Text;
            string mTRNDAT2 = this.TxtDate2.Text;
            string chqno = this.txtChqSearch.Text + "%";
            string Type = (this.Request.QueryString["Type"].ToString() == "Mgt") ? "Mgt" : "";
            string voudate = this.chkvoudate.Checked ? "voudate" : "";
            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_ENTRY_ACCOUNTS_VOUCHER", "SHOWBANKRECON", mACCODE, mTRNDAT1, mTRNDAT2, chqno, Type, voudate, "", "", "");
            if (ds1 == null)
            {
                this.gv1.DataSource = null;
                this.gv1.DataBind();
                return;
            }

            Session["tbl01r"] = ds1.Tables[0];
            this.gv1_DataBind();

        }
        //protected void btnGetDataP_Click(object sender, EventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

        //    string comcod = this.GetCompCode();
        //    string mACCODE = this.DDListBank.SelectedValue.ToString();
        //    string mTRNDAT1 = this.TxtDate1.Text;
        //    string mTRNDAT2 = this.TxtDate2.Text;
        //    DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "PRINTBANKRECON",
        //           mACCODE, mTRNDAT1, mTRNDAT2, "", "", "", "", "", "");
        //    ReportDocument RptBankRecon = new RealERPRPT.R_17_Acc.RptBankReconc();
        //    //TextObject TxtCompName = RptBankRecon.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
        //    //TxtCompName.Text = Convert.ToString(ds1.Tables[1].Rows[0]["comnam"]);

        //    TextObject TxtRptTitle = RptBankRecon.ReportDefinition.ReportObjects["TxtRptTitle"] as TextObject;
        //    TxtRptTitle.Text = this.DDListBank.SelectedItem.Text.Trim().Substring(13);

        //    TextObject TxtRptPeriod = RptBankRecon.ReportDefinition.ReportObjects["TxtRptPeriod"] as TextObject;
        //    TxtRptPeriod.Text = "(As on " + mTRNDAT2 + ")";
        //    TextObject txtuserinfo = RptBankRecon.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

        //    RptBankRecon.SetDataSource(ds1.Tables[0]);

        //    if (ConstantInfo.LogStatus == true)
        //    {
        //        string eventtype = "Bank Reconcilaition";
        //        string eventdesc = "Print Reconcilaition";
        //        string eventdesc2 = "Bank Name: " + this.DDListBank.SelectedItem.ToString().Substring(13);
        //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //    }


        //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    RptBankRecon.SetParameterValue("ComLogo", ComLogo);

        //    //--------------------Export to PDF--------------------------------------------------
        //    Session["Report1"] = RptBankRecon;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //    //Response.Redirect("PDFViewer.aspx");   
        //}


        protected void btnGetDataP_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string comcod = this.GetCompCode();
            string mACCODE = this.DDListBank.SelectedValue.ToString();
            string mTRNDAT1 = this.TxtDate1.Text;
            string mTRNDAT2 = this.TxtDate2.Text;

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "PRINTBANKRECON",
                   mACCODE, mTRNDAT1, mTRNDAT2, "", "", "", "", "", "");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            DataView dv = ds1.Tables[0].DefaultView;

            DataTable dt;

            dv.RowFilter = "(vounum <> '' or note1 <> '') and vounum1 <> '-' ";


            dt = dv.ToTable();


            //var lst = ds1.Tables[0].DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptBankReconc>();
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptBankReconc>();
            //var lst = ds1.Table[0].DataTableToList<RealEntity.C_14_Pro.EClassPur.RptPurchaseSummary02>();m

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptBankReconc", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            // Rpt1.SetParameters(new ReportParameter("txtPrjName", "Project Name : " + this.ddlProjectName.SelectedItem.Text.Trim().Substring(13)));
            //Rpt1.SetParameters(new ReportParameter("todate", "(From: " + Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy") + "  To : " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("todate", "As On: " + mTRNDAT2));
            Rpt1.SetParameters(new ReportParameter("TxtRptTitle", this.DDListBank.SelectedItem.Text.Trim().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Bank Reconcilation"));


            Rpt1.SetParameters(new ReportParameter("RptFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("ExePer", MaAmt > 0 ? ((ExeAmt * 100) / MaAmt).ToString("#,##0.00;(#,##0.00); ") + "%" : "";));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }





        protected void lbtnGetBankList_Click(object sender, EventArgs e)
        {

        }

        //protected void gv1_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    gv1.EditIndex = e.NewEditIndex;
        //    this.gv1_DataBind();
        //}

        //protected void gv1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        // ((Label)this.Master.FindControl("lblmsg")).Text = "";
        //    //try
        //    {
        //        string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        //        Hashtable hst = (Hashtable)Session["tblLogin"];
        //        string mCOMCOD = GetCompCode();
        //        string mVOUNUM = ((Label)gv1.Rows[e.RowIndex].FindControl("lblVOUNUM")).Text.Trim();
        //        string mACTCODE = ((Label)gv1.Rows[e.RowIndex].FindControl("lblACTCODE")).Text.Trim();
        //        string mSUBCODE = ((Label)gv1.Rows[e.RowIndex].FindControl("lblSUBCODE")).Text.Trim();
        //        string mCACTCODE = ((Label)gv1.Rows[e.RowIndex].FindControl("lblCACTCODE")).Text.Trim();
        //        string mRECNDT1 = ((TextBox)gv1.Rows[e.RowIndex].FindControl("txtRECNDT")).Text.Trim() + "01.01.1900";
        //        string mCOMCOD1 = ((Label)gv1.Rows[e.RowIndex].FindControl("lblgvCOMCOD")).Text.Trim();

        //        mRECNDT1 = mRECNDT1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(mRECNDT1.Substring(3, 2))] + "-" + mRECNDT1.Substring(6, 4);
        //        DateTime mRECNDT = DateTime.Parse(mRECNDT1);
        //        bool vuchr = ((CheckBox)gv1.Rows[e.RowIndex].FindControl("chkVoucher")).Checked;

        //        int TblRowIndex = (gv1.PageIndex) * gv1.PageSize + e.RowIndex;
        //        DataTable tbl1 = ((DataTable)Session["tbl01r"]);
        //        if (vuchr)
        //        {
        //            DataView dv1 = tbl1.DefaultView;
        //            dv1.RowFilter = "VOUNUM like '" + mVOUNUM + "%'";
        //            for (int i = 0; i < dv1.Count; i++)
        //            {
        //                dv1[i]["RECNDT"] = mRECNDT;
        //                dv1[i]["OLDNEW"] = "NEW";
        //            }
        //            dv1.RowFilter = "";
        //            mACTCODE = "VOUCHER";
        //        }
        //        else
        //        {
        //            tbl1.Rows[TblRowIndex]["RECNDT"] = mRECNDT;
        //            tbl1.Rows[TblRowIndex]["OLDNEW"] = "NEW";
        //        }

        //        string mUserID = "000000"; // hst["usrid"].ToString(); 

        //        bool result = accData.UpdateTransInfo(mCOMCOD, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEBANKRECON",
        //                  mRECNDT1, mVOUNUM, mACTCODE, mSUBCODE, mCACTCODE, "", "", "",
        //                  "", "", "", "", "", "", mUserID);


        //        if (!result)
        //         ((Label)this.Master.FindControl("lblmsg")).Text = "Invalid date";

        //        Session["tbl01r"] = tbl1;
        //    }
        //    //catch (Exception exp)
        //    //{
        //    //    this.lblError.Text = exp.Message.ToString();
        //    //}
        //    gv1.EditIndex = -1;
        //    this.gv1_DataBind();
        //}

        //protected void gv1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    gv1.EditIndex = -1;
        //    this.gv1_DataBind();
        //}


        protected void gv1_DataBind()
        {

            this.gv1.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gv1.DataSource = (DataTable)Session["tbl01r"];
            this.gv1.DataBind();

            //Session["Report1"] = gv1;

            //((HyperLink)this.gv1.HeaderRow.FindControl ("hlbtntbCdataExelsp")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
        }

        //protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.gv1.PageIndex = ((DropDownList)this.gv1.HeaderRow.FindControl("ddlPageNo")).SelectedIndex;
        //    this.gv1.EditIndex = -1;
        //    this.gv1_DataBind();
        //}

        private void SaveValue()
        {

            DataTable dt = ((DataTable)Session["tbl01r"]);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            int TblRowIndex;
            for (int i = 0; i < this.gv1.Rows.Count; i++)
            {
                string mRECNDT1 = ((TextBox)this.gv1.Rows[i].FindControl("txtgvRECNDT")).Text.Trim() + "01.01.1900";


                //string mVOUNUM1 = (i == 0) ? dt.Rows[i]["vounum"].ToString() : dt.Rows[i-1]["vounum"].ToString(); 
                //mRECNDT1=(i==0)?mRECNDT1:
                //    ((mVOUNUM==mVOUNUM1)?((ASTUtility.Left(bankcode, 4) == "1901") ? ((Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? Convert.ToDateTime(dt.Rows[i-1]["voudat"]).ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt.Rows[i-1]["recndt"]).ToString("dd-MMM-yyyy")):Convert.ToDateTime(dt.Rows[i-1]["recndt"]).ToString("dd-MMM-yyyy"))
                //    : mRECNDT1);

                mRECNDT1 = mRECNDT1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(mRECNDT1.Substring(3, 2))] + "-" + mRECNDT1.Substring(6, 4);
                TblRowIndex = (gv1.PageIndex) * gv1.PageSize + i;
                dt.Rows[TblRowIndex]["recndt"] = mRECNDT1;
            }
            Session["tbl01r"] = dt;

        }


        //protected void txtgvRECNDT_TextChanged(object sender, EventArgs e)
        //{

        //    ((LinkButton)this.gv1.FooterRow.FindControl("lbtnUpdate")).Enabled = true;
        //    int index = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;

        //    string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        //    string mRECNDT1 = ((TextBox)this.gv1.Rows[index].FindControl("txtgvRECNDT")).Text.Trim() + "01.01.1900";

        //    mRECNDT1 = mRECNDT1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(mRECNDT1.Substring(3, 2))] + "-" + mRECNDT1.Substring(6, 4);

        //    // string mRECNDT1 = ((TextBox)this.gv1.Rows[i].FindControl("txtgvRECNDT")).Text.Trim() + "01.01.1900";

        //    string chequedate = ((Label)this.gv1.Rows[index].FindControl("lblgvchequedat")).Text.Trim();

      
        //    //DateTime birthDate = Convert.ToDateTime(recondate);

        //    //string recondate1 = Convert.ToDateTime(birthDate).ToString("dd-MMM-yyyy");
        //    //dd.MM.yyyy



        //    DateTime date1 = DateTime.Parse(chequedate);
        //    DateTime date2 = DateTime.Parse(mRECNDT1);  //Moment


        //    //double adjqty = Convert.ToDouble("0" + ((TextBox)this.gv1.Rows[index].FindControl("txtgvadjqty")).Text.Trim());


        //    if (date2 <= date1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Bank Reconcilation date must be equal or greater cheque date');", true);
        //        ((LinkButton)this.gv1.FooterRow.FindControl("lbtnUpdate")).Enabled = false;
                
        //        return;
        //    }
        //}





        protected void gv1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gv1.PageIndex = e.NewPageIndex;
            this.gv1_DataBind();
        }
        private void PutSameValueVounum()
        {
            DataTable dt = ((DataTable)Session["tbl01r"]);
            string bankcode = this.DDListBank.SelectedValue.ToString();
            string mVOUNUM = "", mRECNDT1 = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    dt.Rows[i]["recndt"] = (ASTUtility.Left(bankcode, 4) == "1901") ? ((Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? Convert.ToDateTime(dt.Rows[i]["voudat"]).ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy")) : Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy");
                    mVOUNUM = dt.Rows[i]["vounum"].ToString();
                    mRECNDT1 = Convert.ToDateTime(dt.Rows[i]["recndt"].ToString()).ToString("dd-MMM-yyyy");

                }


                else if (mVOUNUM == dt.Rows[i]["vounum"].ToString())
                {
                    dt.Rows[i]["recndt"] = mRECNDT1;
                    mVOUNUM = dt.Rows[i]["vounum"].ToString();
                    mRECNDT1 = (ASTUtility.Left(bankcode, 4) == "1901") ? ((Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? Convert.ToDateTime(dt.Rows[i]["voudat"]).ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy")) : Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy");

                }
                else
                {
                    dt.Rows[i]["recndt"] = (ASTUtility.Left(bankcode, 4) == "1901") ? ((Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? Convert.ToDateTime(dt.Rows[i]["voudat"]).ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy")) : Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy");
                    mVOUNUM = dt.Rows[i]["vounum"].ToString();
                    mRECNDT1 = Convert.ToDateTime(dt.Rows[i]["recndt"].ToString()).ToString("dd-MMM-yyyy");

                }
            }


            Session["tbl01r"] = dt;

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
            string bankcode = this.DDListBank.SelectedValue.ToString();
            this.SaveValue();
            this.PutSameValueVounum();
            string comcod = this.GetCompCode();
            DataTable dt = ((DataTable)Session["tbl01r"]);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string mVOUNUM = dt.Rows[i]["vounum"].ToString();
                string RefNo = dt.Rows[i]["refnum"].ToString();
                string mACTCODE = dt.Rows[i]["actcode"].ToString();
                string mSUBCODE = dt.Rows[i]["rescode"].ToString();
                string mCACTCODE = dt.Rows[i]["cactcode"].ToString();
                string mRECNDT1 = Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy");//(ASTUtility.Left(bankcode, 4) == "1901") ? ((Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? Convert.ToDateTime(dt.Rows[i]["voudat"]).ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy")) : Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy");
                                                                                                   //string mVOUNUM1 = (i == 0) ? dt.Rows[i]["vounum"].ToString() : dt.Rows[i - 1]["vounum"].ToString();
                                                                                                   //mRECNDT1 = (i == 0) ? mRECNDT1 :
                                                                                                   //    ((mVOUNUM == mVOUNUM1) ? ((ASTUtility.Left(bankcode, 4) == "1901") ? ((Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? Convert.ToDateTime(dt.Rows[i - 1]["voudat"]).ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt.Rows[i - 1]["recndt"]).ToString("dd-MMM-yyyy")) : Convert.ToDateTime(dt.Rows[i - 1]["recndt"]).ToString("dd-MMM-yyyy"))
                                                                                                   //    : mRECNDT1);

                DateTime voudat = Convert.ToDateTime(dt.Rows[i]["voudat"]);

                string mUserID = "000000";
                bool dcon = (mRECNDT1 == "01-Jan-1900" || mVOUNUM.Substring(0, 2) == "BC" || mVOUNUM.Substring(0, 2) == "CC") ? true : ASITUtility02.TransReconDate(Convert.ToDateTime(mRECNDT1), voudat);
                if ((!dcon))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Reconcilation Date is equal or greater Voucher Date');", true);
                    return;
                }
                bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEBANKRECON", mRECNDT1, mVOUNUM, mACTCODE, mSUBCODE, mCACTCODE, "", "", "", "", "", "", "", "", "", mUserID);

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Invalid date";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }

         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Bank Reconcilaition";
                string eventdesc = "Update Reconcilaition";
                string eventdesc2 = "Bank Name: " + this.DDListBank.SelectedItem.ToString().Substring(13);
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.gv1_DataBind();
        }
        //protected void gv1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    this.gv1.EditIndex = -1;
        //    this.gv1_DataBind();
        //}
        //protected void gv1_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    this.gv1.EditIndex = e.NewEditIndex;
        //    this.gv1_DataBind();
        //    int rowindex = (gv1.PageSize) * (this.gv1.PageIndex) + e.NewEditIndex;

        //    string rpcode = ((DataTable)Session["tbl01r"]).Rows[rowindex]["rpcode"].ToString();

        //    DropDownList ddl2 = (DropDownList)this.gv1.Rows[e.NewEditIndex].FindControl("ddlRpName");

        //        Hashtable hst = (Hashtable)Session["tblLogin"];
        //        string comcod = GetCompCode();
        //        DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETRPCODE", "", "", "", "", "", "", "", "", "");
        //        ddl2.DataTextField = "rpdesc";
        //        ddl2.DataValueField = "rpcode";
        //        ddl2.DataSource = ds1;
        //        ddl2.DataBind();
        //        ddl2.SelectedValue = rpcode; 
        //        //ddl2.Visible = true;

        //        //ddl2.Visible = false;
        //        //ddl2.Items.Clear();

        //}

        //protected void gv1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = GetCompCode();
        //    DataTable tbl1 = (DataTable)Session["tbl01r"];
        //    string rpcode = ((DropDownList)this.gv1.Rows[e.RowIndex].FindControl("ddlRpName")).SelectedValue.ToString();
        //    string Rptdesc = ((DropDownList)this.gv1.Rows[e.RowIndex].FindControl("ddlRpName")).SelectedItem.ToString();

        //    int index = (this.gv1.PageIndex) * this.gv1.PageSize + e.RowIndex;

        //    tbl1.Rows[index]["rpcode"] = rpcode;
        //    tbl1.Rows[index]["rpdesc"] = Rptdesc;
        //    Session["tbl01r"] = tbl1;
        //    this.gv1.EditIndex = -1;
        //    this.gv1_DataBind();   

        //}

        protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("lblVOUNUM1");

                Label lblREFNO = (Label)e.Row.FindControl("lblREFNO");
                Label lblgvDetailsHead = (Label)e.Row.FindControl("lblgvDetailsHead");
                string voucher = ((HyperLink)e.Row.FindControl("lblVOUNUM")).Text.ToString();

                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();
                if (grp == "B")
                {
                    lblREFNO.Font.Bold = true;
                    lblgvDetailsHead.Font.Bold = true;
                    e.Row.Attributes["style"] = "background:DarkSeaGreen;";
                    //lblREFNO.Attributes["style"]="color:blue;";
                    //lblgvDetailsHead.Attributes["style"] = "color:blue;";
                }

                hlink.NavigateUrl = "RptAccVouher.aspx?vounum=" + voucher;


            }
        }


        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            this.LoadData();
            //DataTable dtex = (DataTable)Session["tblexel"];
            if ((DataTable)Session["tblexel"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Reconcillation Date", Type.GetType("System.DateTime"));
                dt.Columns.Add("Chq/Ref No.", Type.GetType("System.String"));
                dt.Columns.Add("Voucher Date", Type.GetType("System.DateTime"));
                dt.Columns.Add("Chq Date", Type.GetType("System.DateTime"));
                dt.Columns.Add("Vou No", Type.GetType("System.String"));
                dt.Columns.Add("Deposit", Type.GetType("System.Double"));
                dt.Columns.Add("Payment", Type.GetType("System.Double"));
                dt.Columns.Add("Accounts Head", Type.GetType("System.String"));
                dt.Columns.Add("Details Head", Type.GetType("System.String"));
                dt.Columns.Add("Narration", Type.GetType("System.String"));
                Session["tblexel"] = dt;


            }



            DataTable dtr = (DataTable)Session["tbl01r"];
            DataTable dtex = (DataTable)Session["tblexel"];

            foreach (DataRow dr1 in dtr.Rows)
            {
                DataRow dre = dtex.NewRow();

                dre["Reconcillation Date"] = dr1["recndt"];
                dre["Chq/Ref No."] = dr1["refnum"];
                dre["Voucher Date"] = dr1["voudat"];
                dre["Chq Date"] = dr1["chequedat"];
                dre["Vou No"] = dr1["vounum1"];
                dre["Deposit"] = dr1["depam"];
                dre["Payment"] = dr1["payam"];
                dre["Accounts Head"] = dr1["actdesc"];
                dre["Details Head"] = dr1["resdesc1"];
                dre["Narration"] = dr1["venar"];

                dtex.Rows.Add(dre);
            }


            DataSet dsF = new DataSet("Bank Reconcilation");
            dsF.Tables.Add(dtex);
            dsF.Tables[0].TableName = "Bank Reconcillation";


            //  var config1 = ConfigurationManager.AppSettings["ExcelFile"];

            var config1 = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.ToString() + "ExcelFile\\";
            if (!System.IO.File.Exists(config1))
            {
                System.IO.Directory.CreateDirectory(config1);
            }

            string config = config1 + "Appendix.xlsx";

            if (File.Exists(config))
            {
                File.Delete(config);
            }

            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            // Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelWorkBook;// = excelApp.Workbooks.Open(@"E:\sdf.xls");
            excelWorkBook = excelApp.Workbooks.Add();

            foreach (DataTable table in dsF.Tables)
            {
                //Add a new worksheet to workbook with the Datatable name
                Excel.Worksheet excelWorkSheet = (Excel.Worksheet)excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = table.TableName;
                excelWorkSheet = this.SetColWidth(excelWorkSheet);
                for (int i = 1; i < table.Columns.Count + 1; i++)
                {


                    var Format1h = ((Excel.Range)excelWorkSheet.Cells[2, i]);
                    Format1h.Font.Bold = true;
                    Format1h.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    excelWorkSheet.Cells[2, i] = table.Columns[i - 1].ColumnName;

                    //-----------------------------------------------------------//
                    //if (table.TableName == "SupplyLines" || table.TableName == "PurchaseLines" || table.TableName == "GLDataLines")
                    //{
                    var Footer = ((Excel.Range)excelWorkSheet.Cells[table.Rows.Count + 2, i]);
                    Footer.Font.Bold = true;
                    Footer.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    // }
                    //-----------------------------------------------------------//
                }

                for (int j = 0; j < table.Rows.Count; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 3, k + 1] = table.Rows[j].ItemArray[k].ToString();

                    }

                }
            }

            excelWorkBook.SaveAs(config);
            excelWorkBook.Close();
            excelApp.Quit();
            string Path = "'Excel File Write Successfully' Location- " + ASTUtility.Left(config1, 1) + ": TEMPS/Appendix.xlsx";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + Path + "');", true);
            this.hlnkDownload.Text = "Click Here to Download The Excel File";
            this.hlnkDownload.NavigateUrl = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath.ToString() + "ExcelFile/Appendix.xlsx";
        }

        protected Excel.Worksheet SetColWidth(Excel.Worksheet excelWorkSheet)
        {
            string strvalFormat = "_(* #,##0.00_);_(* (#,##0.00);_(* " + (char)34 + " -" + (char)34 + "??_);_(@_)";
            Excel.Range chartRange;
            Excel.Range chartRange1;
            Excel.Range chartRange2;
            switch (excelWorkSheet.Name.Trim())
            {
                case "Bank Reconcillation":
                    //((Excel.Range)excelWorkSheet.get_Range ("A1")).Merge (false);
                    //chartRange = (Excel.Range)excelWorkSheet.get_Range ("A1");
                    //chartRange.FormulaR1C1 = "SuppDataStart";

                    //chartRange.Font.Size = 18;

                    //chartRange1 = (Excel.Range)excelWorkSheet.get_Range ("A2", "M2");
                    //chartRange1.Interior.Color = System.Drawing.ColorTranslator.ToOle (System.Drawing.Color.Yellow);
                    //chartRange1.Font.Color = System.Drawing.ColorTranslator.ToOle (System.Drawing.Color.Red);
                    //chartRange1.Font.Size = 11;
                    ((Excel.Range)excelWorkSheet.Cells[2, 1]).ColumnWidth = 15;
                    ((Excel.Range)excelWorkSheet.Cells[2, 2]).ColumnWidth = 10;
                    ((Excel.Range)excelWorkSheet.Cells[2, 3]).ColumnWidth = 15;
                    ((Excel.Range)excelWorkSheet.Cells[2, 4]).ColumnWidth = 15;
                    ((Excel.Range)excelWorkSheet.Cells[2, 5]).ColumnWidth = 12;
                    ((Excel.Range)excelWorkSheet.Cells[2, 6]).ColumnWidth = 12;
                    ((Excel.Range)excelWorkSheet.Cells[2, 7]).ColumnWidth = 13;
                    ((Excel.Range)excelWorkSheet.Cells[2, 8]).ColumnWidth = 30;
                    ((Excel.Range)excelWorkSheet.Cells[2, 9]).ColumnWidth = 30;
                    ((Excel.Range)excelWorkSheet.Cells[2, 10]).ColumnWidth = 50;
                    ((Excel.Range)excelWorkSheet.get_Range("A3", "A2000")).NumberFormat = "dd-MMM-yyyy";
                    ((Excel.Range)excelWorkSheet.get_Range("C3", "C2000")).NumberFormat = "dd-MMM-yyyy";
                    ((Excel.Range)excelWorkSheet.get_Range("D3", "D2000")).NumberFormat = "dd-MMM-yyyy";
                    ((Excel.Range)excelWorkSheet.get_Range("G3", "G2000")).NumberFormat = strvalFormat;
                    ((Excel.Range)excelWorkSheet.get_Range("A3", "A2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    ((Excel.Range)excelWorkSheet.get_Range("B3", "B2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    ((Excel.Range)excelWorkSheet.get_Range("C3", "C2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    ((Excel.Range)excelWorkSheet.get_Range("D3", "D2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    //((Excel.Range)excelWorkSheet.Cells[1, 3]).ColumnWidth = 11;
                    //((Excel.Range)excelWorkSheet.get_Range ("C2", "C2000")).NumberFormat = "dd/MM/yyyy";
                    //((Excel.Range)excelWorkSheet.get_Range ("C1", "C2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //((Excel.Range)excelWorkSheet.Cells[1, 4]).ColumnWidth = 11;
                    //((Excel.Range)excelWorkSheet.get_Range ("D1", "D2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    //((Excel.Range)excelWorkSheet.Cells[1, 5]).ColumnWidth = 8;
                    //((Excel.Range)excelWorkSheet.get_Range ("E1", "E2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //((Excel.Range)excelWorkSheet.Cells[1, 6]).ColumnWidth = 40;
                    //((Excel.Range)excelWorkSheet.get_Range ("F1", "F2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    ////((Excel.Range)excelWorkSheet.Cells[1, 7]).ColumnWidth = 12;
                    ////((Excel.Range)excelWorkSheet.get_Range("G1", "G2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    //((Excel.Range)excelWorkSheet.Cells[1, 7]).ColumnWidth = 18;
                    //((Excel.Range)excelWorkSheet.get_Range ("G2", "G2000")).NumberFormat = strvalFormat;
                    //((Excel.Range)excelWorkSheet.get_Range ("G1", "G1")).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    //((Excel.Range)excelWorkSheet.Cells[1, 8]).ColumnWidth = 14;
                    //((Excel.Range)excelWorkSheet.get_Range ("H2", "H2000")).NumberFormat = strvalFormat;
                    //((Excel.Range)excelWorkSheet.get_Range ("H1", "H1")).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    //((Excel.Range)excelWorkSheet.Cells[1, 9]).ColumnWidth = 10;
                    //((Excel.Range)excelWorkSheet.get_Range ("I1", "I2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //((Excel.Range)excelWorkSheet.Cells[1, 10]).ColumnWidth = 10;
                    //((Excel.Range)excelWorkSheet.get_Range ("J1", "J2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //((Excel.Range)excelWorkSheet.Cells[1, 11]).ColumnWidth = 10;
                    //((Excel.Range)excelWorkSheet.get_Range ("K1", "K2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //((Excel.Range)excelWorkSheet.Cells[1, 12]).ColumnWidth = 12;
                    //((Excel.Range)excelWorkSheet.get_Range ("L1", "L1")).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    //((Excel.Range)excelWorkSheet.get_Range ("L2", "L2000")).NumberFormat = strvalFormat;
                    //((Excel.Range)excelWorkSheet.Cells[1, 13]).ColumnWidth = 10;
                    //((Excel.Range)excelWorkSheet.get_Range ("M1", "M1")).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    //((Excel.Range)excelWorkSheet.get_Range ("M2", "M2000")).NumberFormat = strvalFormat;
                    //((Excel.Range)excelWorkSheet.Cells[1, 14]).ColumnWidth = 12;
                    break;
                case "TaxCodeMaster":
                    ((Excel.Range)excelWorkSheet.get_Range("A1")).Merge(false);
                    chartRange = (Excel.Range)excelWorkSheet.get_Range("A1");
                    chartRange.FormulaR1C1 = "Tax Code Master Data";
                    //chartRange.FormulaR1C1 = "Tax Code Master Data";
                    //chartRange.HorizontalAlignment = 1;
                    //chartRange.VerticalAlignment = 3;
                    //chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);
                    //chartRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Purple);
                    chartRange.Font.Size = 18;

                    chartRange1 = (Excel.Range)excelWorkSheet.get_Range("A2", "D2");
                    chartRange1.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    chartRange1.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    chartRange1.Font.Size = 11;

                    chartRange2 = (Excel.Range)excelWorkSheet.get_Range("A10", "D10");
                    chartRange2.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    chartRange2.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    chartRange2.Font.Size = 11;
                    chartRange2.Font.Bold = true;

                    ((Excel.Range)excelWorkSheet.Cells[2, 1]).ColumnWidth = 50;
                    ((Excel.Range)excelWorkSheet.Cells[2, 3]).ColumnWidth = 60;
                    ((Excel.Range)excelWorkSheet.get_Range("B2", "B2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)excelWorkSheet.get_Range("D2", "D2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;




                    break;

                case "SupplyLines":
                    ((Excel.Range)excelWorkSheet.get_Range("A1")).Merge(false);
                    chartRange = (Excel.Range)excelWorkSheet.get_Range("A1");
                    chartRange.FormulaR1C1 = "SuppDataStart";
                    //chartRange.FormulaR1C1 = "Supply Listings";
                    //chartRange.HorizontalAlignment = 1;
                    //chartRange.VerticalAlignment = 3;
                    //chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);
                    //chartRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Purple);
                    chartRange.Font.Size = 18;

                    chartRange1 = (Excel.Range)excelWorkSheet.get_Range("A2", "M2");
                    chartRange1.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    chartRange1.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    chartRange1.Font.Size = 11;

                    ((Excel.Range)excelWorkSheet.Cells[1, 1]).ColumnWidth = 30;
                    ((Excel.Range)excelWorkSheet.Cells[1, 2]).ColumnWidth = 14;
                    ((Excel.Range)excelWorkSheet.get_Range("B2", "B2000")).NumberFormat = "000000000000";
                    ((Excel.Range)excelWorkSheet.get_Range("B1", "B2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)excelWorkSheet.Cells[1, 3]).ColumnWidth = 11;
                    ((Excel.Range)excelWorkSheet.get_Range("C2", "C2000")).NumberFormat = "dd/MM/yyyy";
                    ((Excel.Range)excelWorkSheet.get_Range("C1", "C2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)excelWorkSheet.Cells[1, 4]).ColumnWidth = 11;
                    ((Excel.Range)excelWorkSheet.get_Range("D1", "D2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    ((Excel.Range)excelWorkSheet.Cells[1, 5]).ColumnWidth = 8;
                    ((Excel.Range)excelWorkSheet.get_Range("E1", "E2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)excelWorkSheet.Cells[1, 6]).ColumnWidth = 40;
                    ((Excel.Range)excelWorkSheet.get_Range("F1", "F2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    //((Excel.Range)excelWorkSheet.Cells[1, 7]).ColumnWidth = 12;
                    //((Excel.Range)excelWorkSheet.get_Range("G1", "G2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    ((Excel.Range)excelWorkSheet.Cells[1, 7]).ColumnWidth = 18;
                    ((Excel.Range)excelWorkSheet.get_Range("G2", "G2000")).NumberFormat = strvalFormat;
                    ((Excel.Range)excelWorkSheet.get_Range("G1", "G1")).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    ((Excel.Range)excelWorkSheet.Cells[1, 8]).ColumnWidth = 14;
                    ((Excel.Range)excelWorkSheet.get_Range("H2", "H2000")).NumberFormat = strvalFormat;
                    ((Excel.Range)excelWorkSheet.get_Range("H1", "H1")).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    ((Excel.Range)excelWorkSheet.Cells[1, 9]).ColumnWidth = 10;
                    ((Excel.Range)excelWorkSheet.get_Range("I1", "I2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)excelWorkSheet.Cells[1, 10]).ColumnWidth = 10;
                    ((Excel.Range)excelWorkSheet.get_Range("J1", "J2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)excelWorkSheet.Cells[1, 11]).ColumnWidth = 10;
                    ((Excel.Range)excelWorkSheet.get_Range("K1", "K2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)excelWorkSheet.Cells[1, 12]).ColumnWidth = 12;
                    ((Excel.Range)excelWorkSheet.get_Range("L1", "L1")).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    ((Excel.Range)excelWorkSheet.get_Range("L2", "L2000")).NumberFormat = strvalFormat;
                    ((Excel.Range)excelWorkSheet.Cells[1, 13]).ColumnWidth = 10;
                    ((Excel.Range)excelWorkSheet.get_Range("M1", "M1")).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    ((Excel.Range)excelWorkSheet.get_Range("M2", "M2000")).NumberFormat = strvalFormat;
                    //((Excel.Range)excelWorkSheet.Cells[1, 14]).ColumnWidth = 12;
                    break;
                case "PurchaseLines":

                    ((Excel.Range)excelWorkSheet.get_Range("A1")).Merge(false);

                    chartRange = (Excel.Range)excelWorkSheet.get_Range("A1");
                    chartRange.FormulaR1C1 = "PurcDataStart";
                    //chartRange.FormulaR1C1 = "Purchase Listings";
                    //chartRange.HorizontalAlignment = 1;
                    //chartRange.VerticalAlignment = 3;
                    //chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);
                    //chartRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Purple);
                    chartRange.Font.Size = 18;

                    chartRange1 = (Excel.Range)excelWorkSheet.get_Range("A2", "M2");
                    chartRange1.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    chartRange1.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    chartRange1.Font.Size = 11;

                    ((Excel.Range)excelWorkSheet.Cells[1, 1]).ColumnWidth = 30;
                    ((Excel.Range)excelWorkSheet.Cells[1, 2]).ColumnWidth = 14;
                    ((Excel.Range)excelWorkSheet.get_Range("B2", "B2000")).NumberFormat = "000000000000";
                    ((Excel.Range)excelWorkSheet.get_Range("B1", "B2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)excelWorkSheet.Cells[1, 3]).ColumnWidth = 11;
                    ((Excel.Range)excelWorkSheet.get_Range("C2", "C2000")).NumberFormat = "dd/MM/yyyy";
                    ((Excel.Range)excelWorkSheet.get_Range("C1", "C2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)excelWorkSheet.Cells[1, 4]).ColumnWidth = 13;
                    ((Excel.Range)excelWorkSheet.get_Range("D1", "D2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    ((Excel.Range)excelWorkSheet.Cells[1, 5]).ColumnWidth = 15;
                    ((Excel.Range)excelWorkSheet.get_Range("E1", "E2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)excelWorkSheet.Cells[1, 6]).ColumnWidth = 8;
                    ((Excel.Range)excelWorkSheet.get_Range("F1", "F2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    ((Excel.Range)excelWorkSheet.Cells[1, 7]).ColumnWidth = 38;
                    ((Excel.Range)excelWorkSheet.get_Range("G1", "G2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    //((Excel.Range)excelWorkSheet.Cells[1, 8]).ColumnWidth = 13;
                    //((Excel.Range)excelWorkSheet.get_Range("H2", "H2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    //((Excel.Range)excelWorkSheet.get_Range("H1", "H1")).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    ((Excel.Range)excelWorkSheet.Cells[1, 8]).ColumnWidth = 18;
                    ((Excel.Range)excelWorkSheet.get_Range("H2", "H2000")).NumberFormat = strvalFormat;
                    ((Excel.Range)excelWorkSheet.get_Range("H1", "H1")).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    ((Excel.Range)excelWorkSheet.Cells[1, 9]).ColumnWidth = 16;
                    ((Excel.Range)excelWorkSheet.get_Range("I1", "I2000")).NumberFormat = strvalFormat;
                    ((Excel.Range)excelWorkSheet.Cells[1, 10]).ColumnWidth = 10;
                    ((Excel.Range)excelWorkSheet.get_Range("J1", "J2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //((Excel.Range)excelWorkSheet.Cells[1, 12]).ColumnWidth = 10;
                    //((Excel.Range)excelWorkSheet.get_Range("L1", "L2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)excelWorkSheet.Cells[1, 11]).ColumnWidth = 12;
                    ((Excel.Range)excelWorkSheet.get_Range("K1", "K1")).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    ((Excel.Range)excelWorkSheet.get_Range("L2", "L2000")).NumberFormat = strvalFormat;
                    ((Excel.Range)excelWorkSheet.Cells[1, 12]).ColumnWidth = 14;
                    ((Excel.Range)excelWorkSheet.get_Range("M1", "M1")).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    ((Excel.Range)excelWorkSheet.get_Range("M2", "M2000")).NumberFormat = strvalFormat;
                    ((Excel.Range)excelWorkSheet.Cells[1, 13]).ColumnWidth = 12;
                    break;
                case "CompanyInfo":
                    ((Excel.Range)excelWorkSheet.get_Range("A1")).Merge(false);
                    chartRange = (Excel.Range)excelWorkSheet.get_Range("A1");
                    chartRange.FormulaR1C1 = "CompInfoStart";
                    //chartRange.FormulaR1C1 = "Master Company Data";//.Insert();//

                    //chartRange.HorizontalAlignment = 1;
                    //chartRange.VerticalAlignment = 3;
                    //chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);
                    //chartRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Purple);
                    chartRange.Font.Size = 18;

                    chartRange1 = (Excel.Range)excelWorkSheet.get_Range("A2", "H2");
                    chartRange1.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    chartRange1.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    chartRange1.Font.Size = 11;

                    ((Excel.Range)excelWorkSheet.Cells[1, 1]).ColumnWidth = 40;
                    ((Excel.Range)excelWorkSheet.Cells[1, 2]).ColumnWidth = 14;
                    ((Excel.Range)excelWorkSheet.get_Range("B1", "B2")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)excelWorkSheet.Cells[1, 3]).ColumnWidth = 14;
                    ((Excel.Range)excelWorkSheet.get_Range("C1", "C2")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)excelWorkSheet.Cells[1, 4]).ColumnWidth = 12;
                    ((Excel.Range)excelWorkSheet.get_Range("D2", "D2")).NumberFormat = "dd/MM/yyyy";
                    ((Excel.Range)excelWorkSheet.get_Range("D1", "D2")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    ((Excel.Range)excelWorkSheet.Cells[1, 5]).ColumnWidth = 12;
                    ((Excel.Range)excelWorkSheet.get_Range("E2", "E2")).NumberFormat = "dd/MM/yyyy";
                    ((Excel.Range)excelWorkSheet.get_Range("E1", "E2")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    ((Excel.Range)excelWorkSheet.Cells[1, 6]).ColumnWidth = 15;
                    ((Excel.Range)excelWorkSheet.get_Range("F2", "F2")).NumberFormat = "dd/MM/yyyy";
                    ((Excel.Range)excelWorkSheet.get_Range("F1", "F2")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    ((Excel.Range)excelWorkSheet.Cells[1, 7]).ColumnWidth = 35;
                    ((Excel.Range)excelWorkSheet.Cells[1, 8]).ColumnWidth = 12;
                    break;
                case "GLDataLines":
                    ((Excel.Range)excelWorkSheet.get_Range("A1", "B1")).Merge(false);
                    chartRange = (Excel.Range)excelWorkSheet.get_Range("A1", "B1");
                    chartRange.FormulaR1C1 = "GLDataStart";
                    //chartRange.FormulaR1C1 = "General Ledger Data";
                    //chartRange.HorizontalAlignment = 1;
                    //chartRange.VerticalAlignment = 3;
                    //chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);
                    //chartRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Purple);
                    chartRange.Font.Size = 18;

                    chartRange1 = (Excel.Range)excelWorkSheet.get_Range("A2", "k2");
                    chartRange1.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    chartRange1.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    chartRange1.Font.Size = 11;
                    //chartRange1.Font.Name = "Cambria";

                    ((Excel.Range)excelWorkSheet.Cells[1, 1]).ColumnWidth = 15;
                    ((Excel.Range)excelWorkSheet.get_Range("A2", "A2000")).NumberFormat = "dd/MM/yyyy";
                    ((Excel.Range)excelWorkSheet.get_Range("A1", "A2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)excelWorkSheet.Cells[1, 2]).ColumnWidth = 10;
                    ((Excel.Range)excelWorkSheet.Cells[1, 3]).ColumnWidth = 25;
                    ((Excel.Range)excelWorkSheet.Cells[1, 4]).ColumnWidth = 75;
                    ((Excel.Range)excelWorkSheet.Cells[1, 5]).ColumnWidth = 25;
                    ((Excel.Range)excelWorkSheet.Cells[1, 6]).ColumnWidth = 12;
                    ((Excel.Range)excelWorkSheet.get_Range("F1", "F2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    ((Excel.Range)excelWorkSheet.Cells[1, 7]).ColumnWidth = 17;
                    ((Excel.Range)excelWorkSheet.get_Range("G1", "G2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    ((Excel.Range)excelWorkSheet.Cells[1, 8]).ColumnWidth = 11;
                    ((Excel.Range)excelWorkSheet.get_Range("H1", "H2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)excelWorkSheet.Cells[1, 9]).ColumnWidth = 12;
                    ((Excel.Range)excelWorkSheet.Cells[1, 9]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)excelWorkSheet.get_Range("I2", "I2000")).NumberFormat = strvalFormat;
                    ((Excel.Range)excelWorkSheet.Cells[1, 10]).ColumnWidth = 12;
                    ((Excel.Range)excelWorkSheet.Cells[1, 10]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)excelWorkSheet.get_Range("J2", "J2000")).NumberFormat = strvalFormat;
                    ((Excel.Range)excelWorkSheet.Cells[1, 11]).ColumnWidth = 15;
                    //((Excel.Range)excelWorkSheet.get_Range("K1", "K2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    ((Excel.Range)excelWorkSheet.Cells[1, 11]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)excelWorkSheet.get_Range("K1", "K2000")).NumberFormat = strvalFormat;
                    //((Excel.Range)excelWorkSheet.Cells[1, 12]).ColumnWidth = 50;
                    //((Excel.Range)excelWorkSheet.get_Range("L1", "L2000")).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    break;

            }
            return excelWorkSheet;
        }

    }
}
