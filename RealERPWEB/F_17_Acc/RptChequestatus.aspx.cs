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
using RealEntity;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_17_Acc
{
    public partial class RptChequestatus : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        UserManMAccount objUser = new UserManMAccount();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString().Trim();

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + date.Substring(2);
                this.txttodate.Text = date;
                this.lblHeader.Text = (type == "SupChequeSt") ? "Party wise Cheque Status" : "Cheque In Hand Report";
                //this.Master.Page.Title = (type == "SupChequeSt") ? "Party wise Cheque Status" : "Cheque In Hand Report";
                this.SelectView();
            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        protected void ibtnFindSupplier_Click(object sender, ImageClickEventArgs e)
        {
            this.GetSupplier();
        }

        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SupChequeSt":
                    this.GetSupplier();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;




            }
        }

        private void GetSupplier()
        {

            string mSrchTxt = this.txtSrchSupplier.Text.Trim() + "%";
            List<RealEntity.EClassResource> lst = new List<RealEntity.EClassResource>();
            lst = objUser.ShowResource(mSrchTxt);
            this.ddlSupplier.DataTextField = "ResDesc";
            this.ddlSupplier.DataValueField = "ResCode";
            this.ddlSupplier.DataSource = lst;
            this.ddlSupplier.DataBind();


        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SupChequeSt":
                    this.ShowSupChequeStatus();

                    break;

            }
        }


        private void ShowSupChequeStatus()
        {

            string Rescode = this.ddlSupplier.SelectedValue.ToString();
            string frmdate = this.txtfromdate.Text;
            string todate = this.txttodate.Text;
            List<RealEntity.C_17_Acc.EClassSupChequeSt> lst = new List<RealEntity.C_17_Acc.EClassSupChequeSt>();
            lst = objUser.ShowSupChequeSt(frmdate, todate, Rescode);
            if (lst == null)
                return;
            Session["tblchequest"] = lst;
            this.Data_Bind();


        }
        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblbdeposit"];
            switch (type)
            {

                case "SupChequeSt":


                    List<RealEntity.C_17_Acc.EClassSupChequeSt> lst = (List<RealEntity.C_17_Acc.EClassSupChequeSt>)Session["tblchequest"];
                    this.dgvChequeStatus.DataSource = lst;
                    this.dgvChequeStatus.DataBind();
                    //this.gvChqdep.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    //this.gvChqdep.DataSource = dt;
                    //this.gvChqdep.DataBind();
                    this.FooterCalculation(lst);
                    break;



            }

        }

        private void FooterCalculation(List<RealEntity.C_17_Acc.EClassSupChequeSt> lst)
        {
            if (lst.Count == 0)
                return;


            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "SupChequeSt":
                    ((Label)this.dgvChequeStatus.FooterRow.FindControl("lgvFChequeam")).Text = Convert.ToDouble(lst.Select(p => p.chequeam).Sum()).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvChequeStatus.FooterRow.FindControl("lgvFclChequeam")).Text = Convert.ToDouble(lst.Select(p => p.clchequeam).Sum()).ToString("#,##0;(#,##0); ");


                    break;

            }



        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "SupChequeSt":
                    this.rptChequeStatusPrint();
                    break;



            }
        }

        private void rptChequeStatusPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            List<RealEntity.C_17_Acc.EClassSupChequeSt> lst = (List<RealEntity.C_17_Acc.EClassSupChequeSt>)Session["tblchequest"];

            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptChequeStatus();

            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["TxtHeader"] as TextObject;
            txtprojectname.Text = "Party wise Cheque Status";

            TextObject txtdate = rptstk.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            txtdate.Text = "From: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptstk.SetDataSource(lst);
            Session["Report1"] = rptstk;
            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        private void ReportDepositAllBank()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataTable dt = (DataTable)Session["tblbdeposit"];
            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.ChequeDepositPrint>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeDepositAllBank", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Daily Bank Transaction"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate));

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Transaction Statement";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }










        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }































        protected void dgvChequeStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void dgvChequeStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {

                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;

                TableCell cell02 = new TableCell();
                cell02.Text = "";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 1;

                TableCell cell03 = new TableCell();
                cell03.Text = "";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 1;

                TableCell cell04 = new TableCell();
                cell04.Text = "";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 1;

                TableCell cell05 = new TableCell();
                cell05.Text = "";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 1;

                TableCell cell06 = new TableCell();
                cell06.Text = "";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 1;


                TableCell cell07 = new TableCell();
                cell07.Text = "Uncleared Cheque";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.ColumnSpan = 3;

                TableCell cell08 = new TableCell();
                cell08.Text = "Last Payment Status";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.ColumnSpan = 3;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvrow.Cells.Add(cell06);
                gvrow.Cells.Add(cell07);
                gvrow.Cells.Add(cell08);
                dgvChequeStatus.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
    }
}











