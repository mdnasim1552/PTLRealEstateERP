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
    public partial class RptPurchaseOrdItemRate : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
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

                // string type = this.Request.QueryString["Type"].ToString().Trim();
                //((Label)this.Master.FindControl("lblTitle")).Text = "";

                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.GetProjectName();
            }

        }

        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    // Create an event handler for the master page's contentCallEvent event
        //    ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

        //    //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        //}

        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPROJECTNAMEFORREQ", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.GetResource();



        }

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void imgbtnFindResource_Click(object sender, EventArgs e)
        {
            this.GetResource();
        }
        private void GetResource()
        {
            string comcod = this.GetCompCode();
            string SrchResource = "%" + this.txtSrcResource.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTMATRATERESOURCE", SrchResource, "", "", "", "", "", "", "", "");
            this.ddlResource.DataTextField = "sirdesc";
            this.ddlResource.DataValueField = "sircode";
            this.ddlResource.DataSource = ds1.Tables[0];
            this.ddlResource.DataBind();
            ds1.Dispose();


        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }


        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();

            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string rsircode = (this.ddlResource.SelectedValue.ToString() == "000000000000" ? "" : this.ddlResource.SelectedValue.ToString()) + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTWORKORDERMATSTATUS", fromdate, todate, pactcode, rsircode, "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvOrdItemStatus.DataSource = null;
                this.gvOrdItemStatus.DataBind();
                return;

            }
            DataTable dt1 = ds1.Tables[0];   //this.HiddenSameDate(ds1.Tables[0]);
            Session["tblstatus"] = dt1;
            Session["tblstatusavg"] = ds1.Tables[1];

            this.LoadGrid();


        }

        //private DataTable HiddenSameDate(DataTable dt1)
        //{
        //    string type = this.Request.QueryString["Type"].ToString().Trim();



        //            string pactcode = dt1.Rows[0]["pactcode"].ToString();
        //            string reqno = dt1.Rows[0]["reqno"].ToString();

        //            for (int j = 1; j < dt1.Rows.Count; j++)
        //            {
        //                if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["reqno"].ToString() == reqno)
        //                {
        //                    pactcode = dt1.Rows[j]["pactcode"].ToString();
        //                    reqno = dt1.Rows[j]["reqno"].ToString();
        //                    dt1.Rows[j]["pactdesc"] = "";
        //                    dt1.Rows[j]["reqno1"] = "";
        //                    dt1.Rows[j]["reqdat1"] = "";
        //                }

        //                else
        //                {



        //                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
        //                    {
        //                        dt1.Rows[j]["pactdesc"] = "";
        //                    }

        //                    if (dt1.Rows[j]["reqno"].ToString() == reqno)
        //                    {
        //                        dt1.Rows[j]["reqno1"] = "";

        //                    }
        //                    pactcode = dt1.Rows[j]["pactcode"].ToString();
        //                    reqno = dt1.Rows[j]["reqno"].ToString();

        //                }

        //            }



        //    }
        //    return dt1;
        //}

        private void LoadGrid()
        {

            DataTable dt = (DataTable)Session["tblstatus"];
            DataTable dt1 = (DataTable)Session["tblstatusavg"];

            this.gvOrdItemStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvOrdItemStatus.DataSource = dt;
            this.gvOrdItemStatus.DataBind();

            this.gvOrdAvg.DataSource = dt1;
            this.gvOrdAvg.DataBind();
            this.FooterCal();
        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblstatus"];

            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvOrdItemStatus.FooterRow.FindControl("gvFgvordamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amount)", "")) ?
                                    0 : dt.Compute("sum(amount)", ""))).ToString("#,##0;(#,##0); ");

        }

        protected void gvOrdItemStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvOrdItemStatus.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

    }
}