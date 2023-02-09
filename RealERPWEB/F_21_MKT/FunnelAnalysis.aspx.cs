using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_21_MKT
{
    public partial class FunnelAnalysis : System.Web.UI.Page
    {
        ProcessAccess dbAccess = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = "Funnel Analysis";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetYear();
                this.rbtType.SelectedValue = "0";
                rbtType_SelectedIndexChanged(null, null);
            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


        }

        protected void rbtType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = this.rbtType.SelectedValue.ToString();
            if (type == "0")
            {
                GetMonthWiseReport();
            }
            else if (type == "1")
            {
                GetMonthSRCWiseReport();
            }
            else
            {

            }

        }

        private void GetYear()
        {
            string comcod = this.GetComeCode();
            DataSet ds5 = dbAccess.GetTransInfo(comcod, "SP_REPORT_CRM_MODULE", "GETTRANSETIONYEAR", "", "", "", "", "", "", "", "", "");
            if (ds5 == null)
            {
                return;
            }
            string date1 = System.DateTime.Today.ToString("yyyy");
            this.ddlYear.DataTextField = "yearlist";
            this.ddlYear.DataValueField = "yearlist";
            this.ddlYear.DataSource = ds5.Tables[0];
            this.ddlYear.DataBind();
            this.ddlYear.SelectedValue = date1;
        }
        private void GetMonthWiseReport()
        {
            try
            {
                string comcod = this.GetComeCode();
                string yearid = this.ddlYear.SelectedValue.ToString();
                DataSet ds5 = dbAccess.GetTransInfo(comcod, "SP_REPORT_CRM_MODULE", "GETFUNNELANALYSISREPORT", yearid, "", "", "", "", "", "", "", "");
                if (ds5 == null)
                {
                    this.gvFunAnaMonths.DataSource = null;
                    this.gvFunAnaMonths.DataBind();
                    return;
                }
                ViewState["tblfunnleMonths"] = ds5.Tables[0];
                this.Data_Bind();
                this.gvFunAnaMonths.Columns[2].Visible = false;
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }
        }
        private void GetMonthSRCWiseReport()
        {
            try
            {
                string comcod = this.GetComeCode();
                string yearid = this.ddlYear.SelectedValue.ToString();
                DataSet ds5 = dbAccess.GetTransInfo(comcod, "SP_REPORT_CRM_MODULE", "GETFUNNELANALYSISREPORTSOURCEWISE", yearid, "", "", "", "", "", "", "", "");
                if (ds5 == null)
                {
                    this.gvFunAnaMonths.DataSource = null;
                    this.gvFunAnaMonths.DataBind();
                    return;
                }
                DataTable dt = HiddenSameData(ds5.Tables[0]);

                ViewState["tblfunnleMonths"] = ds5.Tables[0];
                this.Data_Bind();
                this.gvFunAnaMonths.Columns[2].Visible = true;


            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            string monthid = dt1.Rows[0]["monthid"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["monthid"].ToString() == monthid)
                {
                    dt1.Rows[j]["months"] = "";
                }
                monthid = dt1.Rows[j]["monthid"].ToString();
            }
            return dt1;
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblfunnleMonths"];
            this.gvFunAnaMonths.DataSource = dt;
            this.gvFunAnaMonths.DataBind();

            Session["Report1"] = gvFunAnaMonths;
            string frmdate = this.ddlYear.SelectedValue.ToString();
            string rptTitle = this.rbtType.SelectedItem.ToString();
            Session["ReportName"] = rptTitle+"_" + frmdate;
            ((HyperLink)this.gvFunAnaMonths.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../../RDLCViewer.aspx?PrintOpt=GRIDTOEXCELNEW";
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            rbtType_SelectedIndexChanged(null, null);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}