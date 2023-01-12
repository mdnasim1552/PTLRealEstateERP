using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
namespace RealERPWEB.F_22_Sal
{
    public partial class RptCollSumOfCurMSale : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmDate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(txtfrmDate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Collection Summary of Current Month Sales";

                this.GetProjectName();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);



        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.ShowCollSummary();


        }

        private void ShowCollSummary()
        {
            Session.Remove("tblpayst");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "TRANSSTCURSALES", frmdate, todate, pactcode, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.rpcsummary.DataSource = null;
                this.rpcsummary.DataBind();
                return;
            }

            Session["tblcolsum"] = this.HiddenSameData(ds1.Tables[0]);
            //Session["tblcolsum"] =ds1.Tables[0];
            this.Data_Bind();

        }

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }


        private void GetProjectName()
        {
            try
            {
                string comcod = this.GetCompCode();
                string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_DUMMYSALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
                this.ddlProjectName.DataTextField = "actdesc";
                this.ddlProjectName.DataValueField = "actcode";
                this.ddlProjectName.DataSource = ds1.Tables[0];
                this.ddlProjectName.DataBind();
                ds1.Dispose();
            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }




        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblcolsum"];
            this.rpcsummary.DataSource = dt;
            this.rpcsummary.DataBind();
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;





            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    dt1.Rows[j]["pactdesc"] = "";

                }



                pactcode = dt1.Rows[j]["pactcode"].ToString();




            }

            return dt1;

        }

        //private void FooterCalCulation()
        //{

        //    DataTable dt = (DataTable)Session["tblpayst"];
        //    if (dt.Rows.Count == 0)
        //        return;

        //    ((Label)e.Item.FindControl("lblrpFbMoney")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cbookam)", "")) ?
        //                            0 : dt.Compute("sum(cbookam)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)e.Item.FindControl("lblFcinstall")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cinsam)", "")) ?
        //                      0 : dt.Compute("sum(cinsam)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)e.Item.FindControl("lblFtotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tcollam)", "")) ?
        //                      0 : dt.Compute("sum(tcollam)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)e.Item.FindControl("lblFbclearace")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bclam)", "")) ?
        //                      0 : dt.Compute("sum(bclam)", ""))).ToString("#,##0;(#,##0); ");

        //}
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptsale = new RealERPRPT.R_22_Sal.RptCollSummCurMSale();
            TextObject txtCompanyName = rptsale.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            txtCompanyName.Text = comnam;
            TextObject txtDate = rptsale.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = " (From " + this.txtfrmDate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";
            DataTable dt = (DataTable)Session["tblcolsum"];
            TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptsale.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg"); txtuserinfo
            //rptImp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptsale;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void rpcsummary_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                DataTable dt = (DataTable)Session["tblcolsum"];

                ((Label)e.Item.FindControl("lblrpFbMoney")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cbookam)", "")) ?
                                  0 : dt.Compute("sum(cbookam)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)e.Item.FindControl("lblFcinstall")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cinsam)", "")) ?
                                  0 : dt.Compute("sum(cinsam)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)e.Item.FindControl("lblFtotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tcollam)", "")) ?
                                  0 : dt.Compute("sum(tcollam)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)e.Item.FindControl("lblFbclearace")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bclam)", "")) ?
                                  0 : dt.Compute("sum(bclam)", ""))).ToString("#,##0;(#,##0); ");

            }

        }

    }
}