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
namespace RealERPWEB.F_14_Pro
{
    public partial class RptSupListWithMat : System.Web.UI.Page
    {
        ProcessAccess MISData = new ProcessAccess();

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
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "SupWise") ? "SUPPLIER LIST WITH MATERIALS-SUPPLIER WISE" : "SUPPLIER LIST WITH MATERIALS-MATERIAL WISE";

                this.ShowView();




            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void ShowView()
        {

            string type = this.Request.QueryString["Type"].ToString();

            switch (type)
            {
                case "SupWise":
                    this.MultiView1.ActiveViewIndex = 0;

                    break;

                case "MatWise":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;


            }


        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();

            switch (type)
            {
                case "SupWise":
                    this.ShowSupLwMat();
                    break;

                case "MatWise":
                    this.ShowMatWiseSupplier();
                    break;


            }
        }

        private void ShowSupLwMat()
        {
            Session.Remove("tbldata");
            string comcod = this.GetComeCode();
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTSUPLWMATERIAL", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSupLwMat.DataSource = null;
                this.gvSupLwMat.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tbldata"] = dt;
            this.Data_Bind();
            ds1.Dispose();

        }
        private void ShowMatWiseSupplier()
        {
            Session.Remove("tbldata");
            string comcod = this.GetComeCode();
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTMATERIALWSUPPLIER", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvmatwsupplier.DataSource = null;
                this.gvmatwsupplier.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tbldata"] = dt;
            this.Data_Bind();
            ds1.Dispose();


        }
        private void Data_Bind()
        {


            string type = this.Request.QueryString["Type"].ToString();
            DataTable dt = ((DataTable)Session["tbldata"]);
            if (dt.Rows.Count == 0)
                return;

            switch (type)
            {
                case "SupWise":
                    this.gvSupLwMat.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvSupLwMat.DataSource = dt;
                    this.gvSupLwMat.DataBind();
                    break;

                case "MatWise":
                    this.gvmatwsupplier.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvmatwsupplier.DataSource = dt;
                    this.gvmatwsupplier.DataBind();
                    break;


            }



        }
        private DataTable HiddenSameData(DataTable dt1)
        {




            if (dt1.Rows.Count == 0)
                return dt1;

            string type = this.Request.QueryString["Type"].ToString();

            switch (type)
            {
                case "SupWise":
                    string sircode = dt1.Rows[0]["sircode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["sircode"].ToString() == sircode)
                        {
                            sircode = dt1.Rows[j]["sircode"].ToString();
                            dt1.Rows[j]["supdesc"] = "";
                            dt1.Rows[j]["addr"] = "";
                            dt1.Rows[j]["cperson"] = "";
                            dt1.Rows[j]["mobile"] = "";
                            dt1.Rows[j]["email"] = "";

                        }

                        else
                        {
                            sircode = dt1.Rows[j]["sircode"].ToString();
                        }

                    }
                    break;

                case "MatWise":
                    string rsircode = dt1.Rows[0]["rsircode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                        {

                            dt1.Rows[j]["rsirdesc"] = "";
                            dt1.Rows[j]["rsirunit"] = "";
                        }



                        rsircode = dt1.Rows[j]["rsircode"].ToString();
                    }

                    break;


            }




            return dt1;

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = System.DateTime.Now.ToString("dd.MM.yyyy");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = ((DataTable)Session["tbldata"]);
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.EclassSuplistWithMat>();

            string rptitle = "";

            LocalReport Rpt1 = new LocalReport();
            string type = this.Request.QueryString["Type"].ToString();

            switch (type)
            {
                case "SupWise":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptSuplistWithMat", lst, null, null);
                    rptitle = "Supplier List with Material";
                    break;

                case "MatWise":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptMatWiseSupList", lst, null, null);
                    rptitle = "Materials Wise Suppplier List ";
                    break;


            }


            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Rptname", rptitle));
            Rpt1.SetParameters(new ReportParameter("comlogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("printFooter", txtuserinfo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            //string type = this.Request.QueryString["Type"].ToString();

            //switch (type)
            //{
            //    case "SupWise":
            //        this.PrintShowSupLwMat();

            //        break;

            //    case "MatWise":
            //        this.PrintMatWiseSupplier();
            //        break;


            //}


        }

        private void PrintShowSupLwMat()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptstk = new RealERPRPT.R_14_Pro.rptSupLwMat();

            TextObject txtcompany = rptstk.ReportDefinition.ReportObjects["txtcompany"] as TextObject;
            txtcompany.Text = comnam;


            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Supplier List with Materials Status";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            rptstk.SetDataSource((DataTable)Session["tbldata"]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintMatWiseSupplier()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_14_Pro.rptMatWiseSupplier();

            TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            txtCompanyName.Text = comnam;

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            TextObject txtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy");
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Supplier List with Materials Status";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            rptstk.SetDataSource((DataTable)Session["tbldata"]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvmatwsupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvmatwsupplier.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvSupLwMat_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSupLwMat.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}