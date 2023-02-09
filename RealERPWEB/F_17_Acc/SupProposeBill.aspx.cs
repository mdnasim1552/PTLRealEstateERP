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
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_17_Acc
{

    public partial class SupProposeBill : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string type = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = type == "Entry" ? "Supplier Proposed Payment" : "Supplier Proposed Payment-(Management)";


                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.GetSupplier();

                CommonButton();
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txtProDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy"); ;

            }
        }
        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
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
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lFinalUpdate_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }



        private void GetSupplier()
        {

            string comcod = this.GetCompCode();
            string SrchSupplier = "%" + this.txtSearchSupplier.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "GETSUPPLIER", SrchSupplier, "", "", "", "", "", "", "", "");
            this.ddlSupplierName.DataTextField = "resdesc";
            this.ddlSupplierName.DataValueField = "rescode";
            this.ddlSupplierName.DataSource = ds1.Tables[0];
            this.ddlSupplierName.DataBind();
            ds1.Dispose();
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string Prodate = Convert.ToDateTime(this.txtProDate.Text).ToString("yyyyMMdd");
            string Supplier = ((this.ddlSupplierName.SelectedValue == "000000000000") ? "99" : this.ddlSupplierName.SelectedValue.ToString()) + "%";

            //Previously Called Procedure But Data not retrieved from this procedure For Checking Procedure changed........

            //DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "RPTALLSUPPAYMENT02", fromdate, todate, Supplier, Prodate, "", "", "", "", "");

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "RPTALLSUPPAYMENT", fromdate, todate, Supplier, Prodate, "", "", "", "", "");
            DataTable dt1 = this.HiddenSameData(ds1.Tables[0]);

            var lst = dt1.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.EClassSupplierProposedPayment>();

            string date = "From " + fromdate + " To " + todate;
            string proposaldate = "Proposal Date: " + Convert.ToDateTime(this.txtProDate.Text).ToString("dd-MMM-yyyy"); ;
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptSupplierProposedPayment", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Supplier's Payment Proposal "));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("proposaldate", proposaldate));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rrs2 = new RealERPRPT.R_17_Acc.RptSupProBill();
            //TextObject rptCname = rrs2.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject txtFDate1 = rrs2.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = "From " + fromdate + " To " + todate;

            //TextObject txtProDate = rrs2.ReportDefinition.ReportObjects["txtPrDate"] as TextObject;
            //txtProDate.Text = "Proposal Date: " + Convert.ToDateTime(this.txtProDate.Text).ToString("dd-MMM-yyyy"); ;

            //TextObject txtuserinfo = rrs2.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rrs2.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rrs2.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rrs2;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Supplier Proposal Bill";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnOk.Text == "Ok")
            {
                this.lnkbtnOk.Text = "New";
                this.lblSupplierName.Text = this.ddlSupplierName.SelectedItem.Text.Trim();
                this.ddlSupplierName.Visible = false;
                this.lblSupplierName.Visible = true;
                this.txtFDate.Enabled = false;
                this.txttodate.Enabled = false;
                this.txtProDate.Enabled = false;
                this.LoadData();
            }
            else
            {
                this.lnkbtnOk.Text = "Ok";
                this.ddlSupplierName.Visible = true;
                this.lblSupplierName.Visible = false;
                this.txtFDate.Enabled = true;
                this.txttodate.Enabled = true;
                this.txtProDate.Enabled = true;
                this.gvProBill.DataSource = null;
                this.gvProBill.DataBind();
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
            }

        }
        protected void imgbtnFindRequiSition_Click(object sender, ImageClickEventArgs e)
        {
            this.LoadData();
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }
        private void LoadData()
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string Prodate = Convert.ToDateTime(this.txtProDate.Text).ToString("yyyyMMdd");
            string Supplier = ((this.ddlSupplierName.SelectedValue == "000000000000") ? "99" : this.ddlSupplierName.SelectedValue.ToString()) + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "RPTALLSUPPAYMENT", fromdate, todate, Supplier, Prodate, "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvProBill.DataSource = null;
                this.gvProBill.DataBind();
                return;

            }

            string type = this.Request.QueryString["Type"];
            DataView dv;
            DataTable dt1;
            if (type == "Mgt")
            {

                dv = ds1.Tables[0].DefaultView;
                dv.RowFilter = ("proamt>0");
                dt1 = this.HiddenSameData(dv.ToTable());

            }
            else
            {
                dt1 = this.HiddenSameData(ds1.Tables[0]);
            }
            //  DataTable dt1 =this.HiddenSameData(ds1.Tables[0]);

            ViewState["tblstatus"] = dt1;
            this.LoadGv();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Supplier Proposal Bill";
                string eventdesc = "Show Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string rescode = dt1.Rows[0]["rescode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rescode"].ToString() == rescode)
                {
                    rescode = dt1.Rows[j]["rescode"].ToString();
                    dt1.Rows[j]["resdesc"] = "";
                }

                else
                    rescode = dt1.Rows[j]["rescode"].ToString();
            }

            return dt1;
        }


        private void LoadGv()
        {
            DataTable dt = (DataTable)ViewState["tblstatus"];
            this.gvProBill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvProBill.DataSource = dt;
            this.gvProBill.DataBind();

            if (dt.Rows.Count > 0)
            {
                //((Label)this.gvProBill.FooterRow.FindControl("lblgvFReqAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(proamt)", "")) ?
                //                        0 : dt.Compute("sum(proamt)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)this.gvProBill.FooterRow.FindControl("lblgvFAppamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(appamt)", "")) ?
                //                       0 : dt.Compute("sum(appamt)", ""))).ToString("#,##0;(#,##0); ");
            }
        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGv();
        }
        protected void gvReqStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvProBill.PageIndex = e.NewPageIndex;
            this.LoadGv();
        }

        private void SaveValue()
        {
            int rowindex;
            DataTable tblt02 = (DataTable)ViewState["tblstatus"];
            for (int i = 0; i < this.gvProBill.Rows.Count; i++)
            {
                string ssircode = ((Label)this.gvProBill.Rows[i].FindControl("lblCodeo1")).Text.Trim();
                double proamt = Convert.ToDouble('0' + ((TextBox)this.gvProBill.Rows[i].FindControl("txtgvProamt")).Text.Trim());
                double apramt = Convert.ToDouble('0' + ((TextBox)this.gvProBill.Rows[i].FindControl("txtgvAproamt")).Text.Trim());



                rowindex = (this.gvProBill.PageSize * this.gvProBill.PageIndex) + i;

                tblt02.Rows[rowindex]["rescode"] = ssircode;
                tblt02.Rows[rowindex]["proamt"] = proamt;
                tblt02.Rows[rowindex]["apramt"] = apramt;
            }
            ViewState["tblstatus"] = tblt02;


        }
        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.SaveValue();
            DataTable dt = (DataTable)ViewState["tblstatus"];
            string dayid = Convert.ToDateTime(this.txtProDate.Text).ToString("yyyyMMdd");
            string comcod = this.GetCompCode();
            bool result = false;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string ssircode = dt.Rows[i]["rescode"].ToString();
                string actcode = dt.Rows[i]["actcode"].ToString();
                double Pamt = Convert.ToDouble(dt.Rows[i]["proamt"].ToString());
                string apramt = dt.Rows[i]["apramt"].ToString();
                if (Pamt > 0)
                {
                    result = MktData.UpdateTransInfo2(comcod, "SP_REPORT_BILLMGT", "INSERTUPPROBILL", dayid, ssircode, actcode, Pamt.ToString(), apramt, "", "",
                        "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated fail";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                }


            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            this.LoadGv(); //this.LoadGrid();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Supplier Proposal Bill";
                string eventdesc = "Supplier Proposal Bill";
                string eventdesc2 = "Supplier Proposal Bill Date Wise";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void imgbtnFindSupplier_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnOk.Text == "Ok")
                this.GetSupplier();
        }

    }
}