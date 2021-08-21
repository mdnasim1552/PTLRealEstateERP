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
namespace RealERPWEB.F_12_Inv
{
    public partial class RptIndPrjAmtBasisRes : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttoDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.lbtnOk_Click(null, null);

            }
        }

        private string GetCompCod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
        }
        protected void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCod();
            string serch1 = "%%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETPURPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName.SelectedValue = this.Request.QueryString["prjcode"];
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCod();

            string frmdate = this.txtFDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string pactcode = this.ddlProjectName.SelectedValue;
            // string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "RPTSTOCKINDAMTRES", pactcode, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMatStock.DataSource = null;
                this.gvMatStock.DataBind();

                return;

            }

            Session["amtbasis"] = ds1.Tables[0];

            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["amtbasis"];
            this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvMatStock.DataSource = dt;
            this.gvMatStock.DataBind();
            this.FooterCal();

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }
        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["amtbasis"];

            ((Label)this.gvMatStock.FooterRow.FindControl("lblrcvf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(rcvamt)", "")) ?
                0.00 : dt.Compute("Sum(rcvamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lbltinf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trninamt)", "")) ?
                0.00 : dt.Compute("Sum(trninamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lbltoutf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnoutamt)", "")) ?
               0.00 : dt.Compute("Sum(trnoutamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lbllstf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(lsamt)", "")) ?
                0.00 : dt.Compute("Sum(lsamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lblnetrcvf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(netrcvamt)", "")) ?
               0.00 : dt.Compute("Sum(netrcvamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lblisuf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(issueamt)", "")) ?
                0.00 : dt.Compute("Sum(issueamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lblbgdconf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdconamt)", "")) ?
               0.00 : dt.Compute("Sum(bgdconamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lblactstktf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(actstock)", "")) ?
                0.00 : dt.Compute("Sum(actstock)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lblbgdstkf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdstock)", "")) ?
               0.00 : dt.Compute("Sum(bgdstock)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvMatStock.FooterRow.FindControl("lblvarf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(varamt)", "")) ?
                0.00 : dt.Compute("Sum(varamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }
        protected void gvMatStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMatStock.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}