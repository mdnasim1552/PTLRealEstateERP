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
using RealERPRDLC;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_22_Sal
{
    public partial class RptSalesRegister2 : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");

                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");


                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //    ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

                //((Label)this.Master.FindControl("lblTitle")).Text = "Sales Register 2";
                this.GetProjectName();

            }


        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            // Iqbal Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComocd();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string PactCode = this.ddlProjectName.SelectedItem.Text.Trim().ToString();
            DataTable dt = (DataTable)Session["tbldata"];
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.SaleReg2>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptSalesReg02", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("todate", "Date: " + date));
            Rpt1.SetParameters(new ReportParameter("PactCode", "Project Name: " + PactCode));
            Rpt1.SetParameters(new ReportParameter("txtHeader", "Sales Register"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private string GetComocd()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComocd();
            string txtSProject = ("%" + this.txtSrcPro.Text + "%");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT03", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

            //if (this.Request.QueryString["prjcode"].ToString().Trim().Length > 0)
            //    this.ddlProjectName.SelectedValue = this.Request.QueryString["prjcode"].ToString().Trim();

        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        private void LoadGrid()
        {
            Session.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComocd();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            //string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            // mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT03", "RPTSALESREGISTERBR", PactCode, date, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSalesRegis.DataSource = null;
                this.gvSalesRegis.DataBind();
                return;
            }

            Session["tbldata"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tbldata"];
            this.gvSalesRegis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSalesRegis.DataSource = dt;
            this.gvSalesRegis.DataBind();
            this.FooterCalculation();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }

            }

            return dt1;
        }

        private void FooterCalculation()
        {


            DataTable dt = (DataTable)Session["tblData"];

            if (dt.Rows.Count == 0)
                return;



            ((Label)this.gvSalesRegis.FooterRow.FindControl("lgvFSalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tuamt)", "")) ?
                               0 : dt.Compute("sum(tuamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSalesRegis.FooterRow.FindControl("lgvFColl")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(colamt)", "")) ?
                       0 : dt.Compute("sum(colamt)", ""))).ToString("#,##0.00;(#,##0.00); ");



        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.LoadGrid();

        }

        protected void gvSalesRegis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}