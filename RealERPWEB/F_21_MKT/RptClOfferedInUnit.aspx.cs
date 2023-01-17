using System;
using System.Collections;
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
using System.Net.Mail;
namespace RealERPWEB.F_21_MKT
{
    public partial class RptClOfferedInUnit : System.Web.UI.Page
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

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.GetProjectName();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "CLIENT OFFERING INDIVIDUAL UNIT ";

            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();
            this.GetUnitName();


        }


        private void GetUnitName()
        {

            string comcod = this.GetCompCode();
            string ProjectCode = this.ddlProjectName.SelectedValue.ToString();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "GETUNITNAME", ProjectCode, txtSProject, "", "", "", "", "", "", "");
            this.ddlUnitName.DataTextField = "udesc";
            this.ddlUnitName.DataValueField = "usircode";
            this.ddlUnitName.DataSource = ds1.Tables[0];
            this.ddlUnitName.DataBind();
            ds1.Dispose();


        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetUnitName();
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblcloffer");
            string comcod = this.GetCompCode();
            string ProjectCode = this.ddlProjectName.SelectedValue.ToString();
            string UnitCode = this.ddlUnitName.SelectedValue.ToString();
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "RPRCLOFEREDINUNIT", ProjectCode, UnitCode, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvClientOff.DataSource = null;
                this.gvClientOff.DataBind();
                return;

            }
            ViewState["tblcloffer"] = ds1.Tables[0];
            this.lblUNitRate.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["bgdrate"]).ToString("#,##0;(#,##0); ");
            this.Data_Bind();
        }


        private void Data_Bind()
        {
            this.gvClientOff.DataSource = (DataTable)ViewState["tblcloffer"];
            this.gvClientOff.DataBind();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptAppMonitor = new RealERPRPT.R_21_Mkt.RptClOfferInUnit();
            TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = comnam;
            TextObject rptProjectName = rptAppMonitor.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            rptProjectName.Text = "Project: " + this.ddlProjectName.SelectedItem.Text;

            TextObject rptUnitName = rptAppMonitor.ReportDefinition.ReportObjects["txtUnitName"] as TextObject;
            rptUnitName.Text = "Unit : " + this.ddlUnitName.SelectedItem.Text.Trim();
            TextObject rptunitrate = rptAppMonitor.ReportDefinition.ReportObjects["txtunitrate"] as TextObject;
            rptunitrate.Text = "Actual Rate: " + this.lblUNitRate.Text;
            TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptAppMonitor.SetDataSource((DataTable)ViewState["tblcloffer"]);
            Session["Report1"] = rptAppMonitor;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ibtnFindProject_OnClick(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void ibtnFindUnit_OnClick(object sender, EventArgs e)
        {
            this.GetUnitName();
        }
    }
}