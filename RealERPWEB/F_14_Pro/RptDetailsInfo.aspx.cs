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
    public partial class RptDetailsInfo : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();

            ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            string type = this.Request.QueryString["Type"];
            //((Label)this.Master.FindControl("lblTitle")).Text = (type == "suplist")
            //    ? "SUPPLIERS Details INFORMATION"
            //    : "CONTRATOR Details INFORMATION";

            
            this.ViewSection();
            this.GetProjectName();


        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"];
            string ctype;

            if (type == "suplist")
            {
                ctype = "sup";

            }
            else
            {
                ctype = "con";
            }


            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETSUPPLIERINFO", txtSProject, ctype, "", "", "", "", "", "", "");
            this.DropCheck1.DataTextField = "sirdesc";
            this.DropCheck1.DataValueField = "sircode";
            this.DropCheck1.DataSource = ds1.Tables[0];
            this.DropCheck1.DataBind();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }




        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"];
            switch (type)
            {
                case "suplist":
                    this.GetSuplierDetails();
                    break;
                case "conlist":
                    this.GetContractorDetails();
                    break;

            }

            this.Data_Bind();


        }

        private void GetSuplierDetails()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string SupplierCode = "";
            string[] sec = this.DropCheck1.Text.Trim().Split(',');
            if (sec[0].Substring(0, 3) == "000")
                SupplierCode = "";
            else
                foreach (string s1 in sec)
                    SupplierCode = SupplierCode + s1.Substring(0, 12);
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "SUPPLIERDETAILS", "%", SupplierCode, "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                return;
            }

            Session["tblsuppler"] = ds1.Tables[0];
        }

        private void GetContractorDetails()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string SupplierCode = "";
            string[] sec = this.DropCheck1.Text.Trim().Split(',');
            if (sec[0].Substring(0, 3) == "000")
                SupplierCode = "";
            else
                foreach (string s1 in sec)
                    SupplierCode = SupplierCode + s1.Substring(0, 12);
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "CONDETAILS", "%", SupplierCode, "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                return;
            }

            Session["tblsuppler"] = ds1.Tables[0];
        }

        private void ViewSection()
        {
            string type = this.Request.QueryString["Type"];
            switch (type)
            {
                case "suplist":
                    this.supname.Visible = true;
                    this.Multiview1.ActiveViewIndex = 0;
                    break;
                case "conlist":
                    this.conname.Visible = true;
                    this.Multiview1.ActiveViewIndex = 1;
                    break;
            }
        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblsuppler"];

            string type = this.Request.QueryString["Type"];
            switch (type)
            {
                case "suplist":
                    //this.gvPersonalInfo.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvPersonalInfo.DataSource = dt;
                    this.gvPersonalInfo.DataBind();
                    break;
                case "conlist":
                    //this.gvcondetails.PageSize = Convert.ToInt32 (this.ddlpagesize.SelectedValue.ToString ());
                    this.gvcondetails.DataSource = dt;
                    this.gvcondetails.DataBind();
                    break;

            }



        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"];
            switch (type)
            {
                case "suplist":
                    this.RptSupDetails();
                    break;
                case "conlist":
                    this.RptConDetails();
                    break;

            }
        }

        private void RptSupDetails()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tblsuppler"];

            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.SuppDetails>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptSupplierDetials", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Supplier Details Information"));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void RptConDetails()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tblsuppler"];

            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.ConDetails>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptConDetials", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Contrator Details Information"));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();

        }
    }
}

