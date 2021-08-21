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
namespace RealERPWEB.F_20_BillMod
{
    public partial class RptBillTracking : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        DataTable dttemp = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "Billtracking") ? "BILL TRACKING INFORMATION" : "BILL APPROVAL INFORMATION";
                this.SectionView();



            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void SectionView()
        {

            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Billtracking":
                    this.lbltodate.Visible = false;
                    this.txttodate.Visible = false;
                    this.txtfrmDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "BillApproval":

                    string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfrmDate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    break;



            }


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }














        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Billtracking":
                    this.ShowBillTracking();

                    break;
                case "BillApproval":
                    this.ShowApprovedStatus();

                    break;



            }





        }


        private void ShowBillTracking()
        {

            Session.Remove("tblrcvbill");
            string comcod = this.GetCompCode();
            string date = this.txtfrmDate.Text;
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "RPTBILLTRACKING", date, "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvPayment.DataSource = null;
                this.gvPayment.DataBind();
                return;

            }

            Session["tblrcvbill"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();

        }

        private void ShowApprovedStatus()
        {

            Session.Remove("tblrcvbill");
            string comcod = this.GetCompCode();
            string fdate = this.txtfrmDate.Text;
            string tdate = this.txttodate.Text;

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "GETBILLINFO", fdate, tdate, "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvApprStatus.DataSource = null;
                this.gvApprStatus.DataBind();
                return;

            }

            Session["tblrcvbill"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();

        }




        protected void Data_Bind()
        {
            DataTable tbl1 = (DataTable)Session["tblrcvbill"];

            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Billtracking":
                    this.gvPayment.DataSource = tbl1;
                    this.gvPayment.DataBind();

                    break;
                case "BillApproval":

                    this.gvApprStatus.DataSource = tbl1;
                    this.gvApprStatus.DataBind();
                    break;



            }


        }





        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            DataTable tbl1 = (DataTable)Session["tblrcvbill"];

            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Billtracking":
                    PrintBillTracking();


                    break;
                case "BillApproval":
                    this.printBillapproval();

                    break;



            }


        }


        private void PrintBillTracking()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");

            DataTable dt1 = (DataTable)Session["tblrcvbill"];
            ReportDocument rptstate = new RealERPRPT.R_20_BillMod.RptBillTracking();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            rptftdate.Text = "Date:As On  " + Convert.ToDateTime(this.txtfrmDate.Text.Trim()).ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource(dt1);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void printBillapproval()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");

            DataTable dt1 = (DataTable)Session["tblrcvbill"];
            ReportDocument rptstate = new RealERPRPT.R_20_BillMod.RptBillApproval();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            rptftdate.Text = "Date:As On  " + Convert.ToDateTime(this.txtfrmDate.Text.Trim()).ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource(dt1);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



    }
}
