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

namespace RealERPWEB.F_29_Fxt

{
    public partial class RptFxtStore : System.Web.UI.Page
    {
        public static double CAmt, EAmt, BalAmt;
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Final Accounts Reports View/Print Screen
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "Stockrpt") ? "Materials Stock Report"
                //    : (this.Request.QueryString["Type"] == "Stockrptqbasis") ? "Materials Stock- Quantity Basis" : "Materials Stock- Amount Basis";
                this.GetProjectName();
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtDatefrom.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                SectionView();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        //protected string GetStdDate(string Date1)
        //{
        //    Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
        //    string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        //    Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
        //    return Date1;
        //}

        //protected void SelectedDates(string stat1)
        //{
        //    string mTRNDAT1 = (stat1 == "Init" ? "01-" + DateTime.Today.ToString("MMM-yyyy") : this.txtDatefrom.Text.Trim());
        //    string mTRNDAT2 = (stat1 == "Init" ? DateTime.Today.ToString("dd-MMM-yyyy") : this.txtDateto.Text.Trim());
        //    this.Calfr.SelectedDate = Convert.ToDateTime(mTRNDAT1);
        //    this.Calto.SelectedDate = Convert.ToDateTime(mTRNDAT2);
        //}


        private void SectionView()
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {


                case "Stockrptqbasis":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "Stockrptamtbasis":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;


            }


        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "Stockrpt":
                    this.rptCentralStock();
                    break;

                case "Stockrptqbasis":
                    this.PrintMStkQbasis();

                    break;
                case "Stockrptamtbasis":
                    this.PrintMStkAmtbasis();

                    break;


            }

        }
        protected void rptCentralStock()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)ViewState["tblVeiw"];

            ReportDocument rptstk = new RealERPRPT.R_13_Cen.RptCentralStore();
            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Central Store";
                string eventdesc = "Print Report";
                string eventdesc2 = this.ddlAccProject.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            rptstk.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintMStkQbasis()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            DataTable dt = (DataTable)ViewState["tblVeiw"];

            var lst = dt.DataTableToList<RealEntity.C_29_Fxt.EClassFixedAsset.EClassMaterialStock>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_29_Fxt.RptMatStkQtyBasis", lst, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("rptTitle", "Materials Stock - Quantity Basis "));

            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Central Store";
                string eventdesc = "Print Report";
                string eventdesc2 = this.ddlAccProject.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





            //ReportDocument rptstk = new RealERPRPT.R_13_Cen.RptMatStkQtyBasis();
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";      
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Central Store";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.ddlAccProject.SelectedItem.ToString();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintMStkAmtbasis()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblVeiw"];
            var lst = dt.DataTableToList<RealEntity.C_29_Fxt.EClassFixedAsset.EClassMaterialStock>();
            string date = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_29_Fxt.RptMatStkAmtBasis", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Materials Stock - Amount Basis "));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Central Store";
                string eventdesc = "Print Report";
                string eventdesc2 = this.ddlAccProject.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




            //ReportDocument rptstk = new RealERPRPT.R_13_Cen.RptMatStkAmtBasis();
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Central Store";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.ddlAccProject.SelectedItem.ToString();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //rptstk.SetDataSource(dt);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.GetMatStore();



        }

        private void GetMatStore()
        {

            string comcod = this.GetCompCode();
            string branch = "";
            string[] sec = this.DropCheck1.Text.Trim().Split(',');
            if (sec[0].Substring(0, 4) == "0000")
                branch = "";
            else
                foreach (string s1 in sec)
                    branch = branch + this.ddlAccProject.SelectedValue.ToString().Substring(0, 8) + s1.Substring(0, 4);

            string date1 = this.txtDatefrom.Text.Trim();
            string date2 = this.txtDateto.Text.Trim();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSETSTOCK", "RPTPROJECTSTK", branch, date1, date2, "", "", "", "", "", "");
            ViewState["tblVeiw"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();



        }


        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    dt1.Rows[j]["pactdesc"] = "";
                    dt1.Rows[j]["rsirdesc"] = "";
                }
                else
                {

                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        dt1.Rows[j]["pactdesc"] = "";
                    if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                        dt1.Rows[j]["rsirdesc"] = "";
                }


                pactcode = dt1.Rows[j]["pactcode"].ToString();
                rsircode = dt1.Rows[j]["rsircode"].ToString();
            }
            return dt1;
        }


        private void Data_Bind()
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {






                case "Stockrptqbasis":
                    this.gvCenStorewlsd.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvCenStorewlsd.DataSource = (DataTable)ViewState["tblVeiw"];
                    this.gvCenStorewlsd.DataBind();
                    break;

                case "Stockrptamtbasis":
                    this.gvMatSAbasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvMatSAbasis.DataSource = (DataTable)ViewState["tblVeiw"];
                    this.gvMatSAbasis.DataBind();
                    this.FooterCalculation();
                    break;


            }




        }





        protected void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string filter = this.txtSearch.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSETSTOCK", "GETSTORENAME", filter, "", "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.ddlAccProject.DataSource = dt1;
            this.ddlAccProject.DataTextField = "actdesc";
            this.ddlAccProject.DataValueField = "actcode";
            this.ddlAccProject.DataBind();
            ds1.Dispose();
            this.GetBranch();


        }


        private void GetBranch()
        {

            string comcod = this.GetCompCode();
            string storecode = this.ddlAccProject.SelectedValue.ToString().Substring(0, 8) + "%";
            string filter = this.txtSearch.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSETSTOCK", "GETBRANCHNAME", storecode, filter, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.DropCheck1.DataSource = dt1;
            this.DropCheck1.DataTextField = "actdesc";
            this.DropCheck1.DataValueField = "actdesc";
            this.DropCheck1.DataBind();
            ds1.Dispose();



        }


        private void FooterCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblVeiw"];

            if (dt.Rows.Count == 0)
                return;

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {




                case "Stockrptamtbasis":
                    ((Label)this.gvMatSAbasis.FooterRow.FindControl("lgvFOpnAmtab")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opnam)", "")) ?
                        0.00 : dt.Compute("Sum(opnam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvMatSAbasis.FooterRow.FindControl("lgvFpuramab")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(recam)", "")) ?
                        0.00 : dt.Compute("Sum(recam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvMatSAbasis.FooterRow.FindControl("lgvFtinamab")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tinam)", "")) ?
                        0.00 : dt.Compute("Sum(tinam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvMatSAbasis.FooterRow.FindControl("lgvFissueamab")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(issueam)", "")) ?
                        0.00 : dt.Compute("Sum(issueam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvMatSAbasis.FooterRow.FindControl("lgvFtrnoutam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(toutam)", "")) ?
                        0.00 : dt.Compute("Sum(toutam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvMatSAbasis.FooterRow.FindControl("lgvFlostam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(lam)", "")) ?
                        0.00 : dt.Compute("Sum(lam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvMatSAbasis.FooterRow.FindControl("lgvFsoldam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(sam)", "")) ?
                        0.00 : dt.Compute("Sum(sam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvMatSAbasis.FooterRow.FindControl("lgvFdesam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dam)", "")) ?
                        0.00 : dt.Compute("Sum(dam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvMatSAbasis.FooterRow.FindControl("lgvFstkamab")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(stkam)", "")) ?
                      0.00 : dt.Compute("Sum(stkam)", ""))).ToString("#,##0;(#,##0);  ");
                    break;

            }







        }


        protected void ImgbtnFindProj_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }











        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvCenStorewlsd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCenStorewlsd.PageIndex = e.NewPageIndex;
            this.Data_Bind();


        }
        protected void gvMatSAbasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMatSAbasis.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }

        protected void ddlAccProject_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ImgbtnFindBrach_Click(object sender, EventArgs e)
        {
            this.GetBranch();
        }
    }
}
