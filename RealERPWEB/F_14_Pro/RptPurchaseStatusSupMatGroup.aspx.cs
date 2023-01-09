using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using System.Data;
using System.Configuration;
using System.Collections;
using RealERPLIB;
using RealERPRDLC;
using RealERPRPT;


namespace RealERPWEB.F_14_Pro
{
    public partial class RptPurchaseStatusSupMatGroup : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Purchase Status (Supplier and Material Group Wise)";

                this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01" + this.txtfrmdate.Text.Trim().Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.GetMaterial();
                //this.imgbtnFindSupplier_Click(null,null);
                this.GetProjectName();
                this.GetSupplier();
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {
            this.ShowPurStatus();
        }

        protected void lbtnresource_Click(object sender, EventArgs e)
        {
            this.GetMaterial();
        }

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void imgbtnFindSupplier_Click(object sender, EventArgs e)
        {
            this.GetSupplier();
        }


        private void GetMaterial()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //string pactcode = this.ddlProName.SelectedValue.ToString() == "000000000000" ? "%%" : "%" + this.ddlProName.SelectedValue.ToString() + "%";
            string txtfindMat = this.txtsrchresource.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETMATERIALEVA", "", txtfindMat, "", "", "", "", "", "", "");
            this.chkResourcelist.DataTextField = "rsirdesc";
            this.chkResourcelist.DataValueField = "rsircode";
            this.chkResourcelist.DataSource = ds1.Tables[0];
            this.chkResourcelist.DataBind();
            if (Request.QueryString.AllKeys.Contains("prjcode"))
            {
                this.chkResourcelist.Text = this.Request.QueryString["prjcode"].Length > 0 ? "000000000000" : "";
            }
            ds1.Dispose();
        }


        private void GetSupplier()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSrchSupplier = this.txtSrcSupplier.Text.Trim() + "%";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETSUPPLIER", pactcode, txtSrchSupplier, "", "", "", "", "", "", "");
            this.ddlSupplier.DataTextField = "ssirdesc";
            this.ddlSupplier.DataValueField = "ssircode";
            this.ddlSupplier.DataSource = ds2.Tables[0];
            this.ddlSupplier.DataBind();
        }

        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%" + this.txtSrcProject.Text + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }


        private void ShowPurStatus()
        {
            //Session.Remove ("tblconsddetails");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string supplier = this.ddlSupplier.SelectedValue.ToString() == "000000000000" ? "99%" : this.ddlSupplier.SelectedValue.ToString() + "%";
            string resListMulti = "";
            string resourcelist = this.chkResourcelist.SelectedValue.ToString();
            foreach (ListItem item in chkResourcelist.Items)
            {
                if (item.Selected)
                {
                    resListMulti += item.Value;
                }
            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURSTATUSSUPPIERANDMATGROUP", frmdate, todate, supplier, resListMulti, "", "", "");
            if (ds1 == null)
                return;
            Session["tblpursum"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string isircod = dt1.Rows[0]["mgrpcode"].ToString();
            for (int i = 1; i < dt1.Rows.Count; i++)
            {
                if (dt1.Rows[i]["mgrpcode"].ToString() == isircod)
                {
                    dt1.Rows[i]["mgrpdesc"] = "";
                }
                isircod = dt1.Rows[i]["mgrpcode"].ToString();
            }

            //string pactcode = dt1.Rows[0]["pactcode"].ToString();
            //for (int j = 1; j < dt1.Rows.Count; j++)
            //{
            //    if (dt1.Rows[j]["pactcode"].ToString() == isircod)
            //    {
            //        dt1.Rows[1]["pactdesc"] = "";
            //        dt1.Rows[j]["pactdesc"] = "";
            //    }

            //    isircod = dt1.Rows[j]["pactcode"].ToString();
            //}
            return dt1;
        }


        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblpursum"];
            this.gvpurvspay.DataSource = dt;
            this.gvpurvspay.DataBind();


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblpursum"];
            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPayment.RPTPURSTATUSSUPPIERANDMATGROUP>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_14_Pro.RptPurchaseStatusSupplierandMaterial", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "PURCHASE STATUS (SUPPLIER AND MATERIAL GROUP WISE)"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printdate", "( From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}