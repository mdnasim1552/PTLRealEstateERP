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
namespace RealERPWEB.F_14_Pro
{
    public partial class RptReqAdjust : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();


            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblReqAdj");
            string comcod = this.GetComeCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string MrfNo = this.txtMRFNO.Text.Trim() + "%";
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "REQADJSTATUS", fromdate, todate, pactcode, MrfNo, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvReqAdjStatus.DataSource = null;
                this.gvReqAdjStatus.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblReqAdj"] = dt;
            this.Data_Bind();

        }
        private void GetProjectName()
        {

            string comcod = this.GetComeCode();
            string txtSProject = this.txtSrcProject.Text + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }
        private void Data_Bind()
        {
            this.gvReqAdjStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvReqAdjStatus.DataSource = (DataTable)Session["tblReqAdj"];
            this.gvReqAdjStatus.DataBind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string adjstno = dt1.Rows[0]["adjstno"].ToString();
            string reqno = dt1.Rows[0]["reqno"].ToString();

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

                if (dt1.Rows[j]["adjstno"].ToString() == adjstno)
                {
                    adjstno = dt1.Rows[j]["adjstno"].ToString();
                    dt1.Rows[j]["adjstno"] = "";
                    dt1.Rows[j]["adjstdat"] = "";
                }
                else
                {
                    adjstno = dt1.Rows[j]["adjstno"].ToString();
                }

                if (dt1.Rows[j]["reqno"].ToString() == reqno)
                {
                    reqno = dt1.Rows[j]["reqno"].ToString();
                    dt1.Rows[j]["reqno"] = "";
                    dt1.Rows[j]["mrfno"] = "";
                    dt1.Rows[j]["reqdat"] = "";
                }
                else
                {
                    reqno = dt1.Rows[j]["reqno"].ToString();
                }
            }
            return dt1;

        }




        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            ReportDocument rrs1 = new RealERPRPT.R_14_Pro.RptReqAdjStatus();
            TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = comnam;
            TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtFDate1.Text = "From: " + fromdate + " To: " + todate;
            TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rrs1.SetDataSource((DataTable)Session["tblReqAdj"]);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Reqisition Adjustment";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rrs1.SetParameterValue("ComLogo", ComLogo);

            Session["Report1"] = rrs1;
            lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void imgbtnFindProject_Click(object sender, ImageClickEventArgs e)
        {
            this.GetProjectName();
        }
        protected void gvReqAdjStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvReqAdjStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}