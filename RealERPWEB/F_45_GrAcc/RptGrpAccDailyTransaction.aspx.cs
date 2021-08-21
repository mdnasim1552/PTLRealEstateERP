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
using RealERPRDLC;
namespace RealERPWEB.F_45_GrAcc
{
    public partial class RptGrpAccDailyTransaction : System.Web.UI.Page
    {

        ProcessAccess GrpData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = ((this.Request.QueryString["Type"].ToString().Trim() == "GrpDTransaction") ? "DAILY TRANSACTION " : "Working Budget Vs. Achievement ") + "INFORMATION VIEW/EDI";
                this.SectionView();

                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = Convert.ToDateTime("01" + (this.txttodate.Text.Trim().Substring(3))).ToString("dd-MMM-yyyy"); ;


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

        }
        private string GetCompCode()
        {

            //return (this.Request.QueryString["comcod"].ToString());

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void SectionView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "GrpDTransaction":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "GrpWBudVsAchv":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.LodeCompany();
                    break;

                case "GrpProTrans":
                    this.MultiView1.ActiveViewIndex = 2;

                    break;


            }

        }

        protected void LodeCompany()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = GrpData.GetTransInfo(comcod, "SP_REPORT_GRPACC_TRANS", "COMCODELIST", "", "", "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "comnam";
            this.ddlCompany.DataValueField = "comcod";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
        }


        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "GrpDTransaction":
                    this.lblTransactionTitle.Visible = true;
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.lblVoucher.Visible = true;
                    this.ddlVouchar.Visible = true;
                    this.imgbtnSearchVoucher.Visible = true;
                    this.TransactionList();
                    break;
                case "GrpWBudVsAchv":
                    this.ShowWbggVsEx();
                    break;

                case "GrpProTrans":
                    this.ShowpProTransaction();
                    break;
            }

        }

        private void TransactionList()
        {

            Session.Remove("tranlist");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string txtVouType = this.ddlVouchar.SelectedItem.ToString().Trim();
            string txtSVoucher = (txtVouType == "ALL Voucher" ? "" : txtVouType) + "%";
            DataSet ds1 = GrpData.GetTransInfo("xxxx", "SP_REPORT_GRPACC_TRANS", "PRINTGRPTRANSACTIONS", fromdate, todate, txtSVoucher, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvtranlsit.DataSource = null;
                this.gvtranlsit.DataBind();
                return;
            }
            Session["tranlist"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }


        private void ShowWbggVsEx()
        {
            //Session["tranlist"];
            Session.Remove("tranlist");
            string comcod = this.ddlCompany.SelectedValue.Trim().ToString();//.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = GrpData.GetTransInfo(comcod, "SP_REPORT_GRPACC_TRANS", "RPTWRVSACAMT", fromdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tranlist"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        private void ShowpProTransaction()
        {
            Session.Remove("tranlist");

            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlAccHead.SelectedValue.ToString();
            DataSet ds1 = GrpData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "RPTPROTRANSACTION", fromdate, todate, pactcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dtr = this.HiddenSameData(ds1.Tables[0]);
            Session["tranlist"] = dtr;
            this.Data_Bind();


        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tranlist"];
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "GrpDTransaction":
                    this.gvtranlsit.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvtranlsit.DataSource = dt;
                    this.gvtranlsit.DataBind();
                    this.FooterCalculation(dt);
                    break;

                case "GrpWBudVsAchv":
                    this.gvbgdvse.DataSource = dt;
                    this.gvbgdvse.DataBind();
                    break;

                case "GrpProTrans":
                    this.gvPtotranlsit.DataSource = dt;
                    this.gvPtotranlsit.DataBind();
                    this.FooterCalculation(dt);
                    Session["Report1"] = gvPtotranlsit;
                    if (dt.Rows.Count > 0)
                        ((HyperLink)this.gvPtotranlsit.HeaderRow.FindControl("hlbtnbtbCdataExelp")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;

            }





        }



        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "GrpDTransaction":
                    ((Label)this.gvtranlsit.FooterRow.FindControl("lgvFDram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                             0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvtranlsit.FooterRow.FindControl("txtgvFCram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                           0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0) ;");
                    break;

                case "GrpProTrans":

                    ((Label)this.gvPtotranlsit.FooterRow.FindControl("lgvFDramp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                           0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvPtotranlsit.FooterRow.FindControl("lgvFCramp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                           0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0) ;");

                    break;

            }
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string grpcode;
            string type = this.Request.QueryString["Type"].ToString().Trim();


            switch (type)
            {
                case "GrpDTransaction":
                    string comcod = dt1.Rows[0]["comcod"].ToString();
                    string Date1 = dt1.Rows[0]["voudat1"].ToString();
                    string vounum = dt1.Rows[0]["vounum"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["comcod"].ToString() == comcod && dt1.Rows[j]["vounum"].ToString() == vounum)
                        {
                            comcod = dt1.Rows[j]["comcod"].ToString();
                            vounum = dt1.Rows[j]["vounum"].ToString();
                            dt1.Rows[j]["comsnam"] = "";
                            dt1.Rows[j]["vounum1"] = "";
                            dt1.Rows[j]["voudat1"] = "";
                        }
                        else
                        {
                            if (dt1.Rows[j]["comcod"].ToString() == comcod)
                            {
                                dt1.Rows[j]["comsnam"] = "";
                            }
                            if (dt1.Rows[j]["vounum"].ToString() == vounum)
                            {
                                dt1.Rows[j]["vounum1"] = "";
                                dt1.Rows[j]["voudat1"] = "";
                            }
                            comcod = dt1.Rows[j]["comcod"].ToString();
                            vounum = dt1.Rows[j]["vounum"].ToString();
                            //vounum = dt1.Rows[j]["vounum1"].ToString();
                        }

                    }
                    break;

                case "GrpWBudVsAchv":
                    grpcode = dt1.Rows[0]["grpcode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grpcode"].ToString() == grpcode)
                        {
                            grpcode = dt1.Rows[j]["grpcode"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }
                        else
                        {
                            grpcode = dt1.Rows[j]["grpcode"].ToString();
                        }
                    }
                    break;

                case "GrpProTrans":

                    Date1 = dt1.Rows[0]["voudat"].ToString();
                    vounum = dt1.Rows[0]["vounum"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["voudat"].ToString() == Date1 && dt1.Rows[j]["vounum"].ToString() == vounum)
                        {
                            dt1.Rows[j]["vounum1"] = "";
                            dt1.Rows[j]["voudat1"] = "";
                        }

                        else
                        {
                            if (dt1.Rows[j]["vounum"].ToString() == vounum)
                                dt1.Rows[j]["vounum1"] = "";

                            if (dt1.Rows[j]["voudat"].ToString() == Date1)
                                dt1.Rows[j]["voudat1"] = "";
                        }

                        Date1 = dt1.Rows[j]["voudat"].ToString();
                        vounum = dt1.Rows[j]["vounum"].ToString();


                    }
                    break;

            }
            return dt1;

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "GrpDTransaction":
                    this.PrintDailyTransaction();
                    break;

                case "GrpWBudVsAchv":
                    this.PrintBudgetdVsAc();
                    break;
            }


        }
        private void PrintDailyTransaction()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tranlist"];
            ReportDocument rptdtlist = new RealERPRPT.R_45_GrAcc.RptGrpDailyTransaction();
            TextObject rptdate = rptdtlist.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            TextObject txtuserinfo = rptdtlist.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptdtlist.SetDataSource(dt);
            Session["Report1"] = rptdtlist;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintBudgetdVsAc()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tranlist"];

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.WorkingBudgetVsAchievement>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.rptAccBudVsExpen", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Budget Vs Achievement Details"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void gvtranlsit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label acrescode = (Label)e.Row.FindControl("lblgvAcRsCode");
                Label acresdesc = (Label)e.Row.FindControl("lblgvAcRsDesc");
                Label lbldram = (Label)e.Row.FindControl("lgvDram");
                Label lblcramt = (Label)e.Row.FindControl("txtgvCram");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "acrescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "  Total Amt:")
                {

                    acrescode.Font.Bold = true;
                    acresdesc.Font.Bold = true;
                    lbldram.Font.Bold = true;
                    lblcramt.Font.Bold = true;

                }

            }
        }

        protected void gvtranlsit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvtranlsit.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void imgbtnSearchVoucher_Click(object sender, EventArgs e)
        {
            this.TransactionList();
        }

        protected void gvbgdvse_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label actdesc = (Label)e.Row.FindControl("lblgvAcDesc");
                Label bgdamt = (Label)e.Row.FindControl("lgvbgdamt");
                Label acamt = (Label)e.Row.FindControl("lgvacamt");
                Label diffamt = (Label)e.Row.FindControl("txtgvdiffamt");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    actdesc.Font.Bold = true;
                    bgdamt.Font.Bold = true;
                    acamt.Font.Bold = true;
                    diffamt.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");
                }

            }
        }
        private void GetAccCode()
        {


            string comcod = this.GetCompCode();
            string filter = "%" + this.txtAccSearch.Text.Trim() + "%";
            DataSet ds1 = GrpData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "GETACCHEAD", filter, "", "", "", "", "", "", "", "");
            this.ddlAccHead.DataSource = ds1.Tables[0];
            this.ddlAccHead.DataTextField = "actdesc1";
            this.ddlAccHead.DataValueField = "actcode";
            this.ddlAccHead.DataBind();
            ds1.Dispose();
        }

        protected void IbtnSearchAcc_Click(object sender, EventArgs e)
        {
            this.GetAccCode();
        }
    }
}
