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
namespace RealERPWEB.F_09_PImp
{
    public partial class RptExeResRequirement : System.Web.UI.Page
    {
        ProcessAccess ImpleData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Consumption Based On Execution";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                // this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtfrmDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        private void GetProjectName()
        {


            string comcod = this.GetComeCode();
            string serch1 = this.txtProjectSearch.Text.Trim() + "%";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "GETPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrjName.DataTextField = "pactdesc";
            this.ddlPrjName.DataValueField = "pactcode";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();
            this.ddlPrjName_SelectedIndexChanged(null, null);


        }


        protected void GetIssueNo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlPrjName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string serch1 = this.txtIsuNo.Text.Trim() + "%";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "GETISSUENNO", pactcode, frmdate, todate, serch1, "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlIssueNO.DataTextField = "isuno1";
            this.ddlIssueNO.DataValueField = "isuno";
            this.ddlIssueNO.DataSource = ds1.Tables[0];
            this.ddlIssueNO.DataBind();
            this.ddlIssueNO_SelectedIndexChanged(null, null);


        }

        protected void ImgbtnFindPrjName_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void ImgbtnFindImpNo_Click(object sender, EventArgs e)
        {
            this.GetIssueNo();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblRes");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlPrjName.SelectedValue.ToString();
            string isuno = this.ddlIssueNO.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string flrcod = this.ddlFloorListRpt.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));

            DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "RPTPROJECTRESOURCE", pactcode, isuno, frmdate, todate, flrcod, mRptGroup, "", "", "");
            if (ds2 == null)
            {
                this.gvRptResBasis.DataSource = null;
                this.gvRptResBasis.DataBind();
                return;
            }

            Session["tblRes"] = ds2.Tables[0];
            this.LoadGrid();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Resource Requirement Info";
                string eventdesc = "Show Report";
                string eventdesc2 = "Project Name: " + this.ddlPrjName.SelectedItem.ToString() + "Imp Mo: " + this.ddlIssueNO.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }

        private void LoadGrid()
        {
            this.gvRptResBasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString().Trim());
            this.FooterCal((DataTable)Session["tblRes"]);
            this.gvRptResBasis.DataSource = (DataTable)Session["tblRes"];
            this.gvRptResBasis.DataBind();

        }



        private void FooterCal(DataTable TptTable)
        {
            double mSUMAM = Convert.ToDouble((Convert.IsDBNull(TptTable.Compute("sum(rptamt)", "")) ?
               0.00 : TptTable.Compute("sum(rptamt)", "")));
            this.gvRptResBasis.Columns[6].FooterText = mSUMAM.ToString("#,##0.00;(#,##0.00);-");

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblRes"];
            ReportDocument rptResource = new RealERPRPT.R_09_PImp.RptExeResReqrment(); // All Project
            TextObject rpttxtComName = rptResource.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            rpttxtComName.Text = comnam;
            TextObject rpttxtPrjName = rptResource.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            rpttxtPrjName.Text = this.ddlPrjName.SelectedItem.ToString().Substring(13);
            TextObject rpttxtIssueno = rptResource.ReportDefinition.ReportObjects["txtIssueno"] as TextObject;
            rpttxtIssueno.Text = "Issue No.: " + (this.ddlIssueNO.SelectedValue.ToString() == "00000000000000" ? "All Issue No" : this.ddlIssueNO.SelectedItem.Text.Trim().Substring(0, 11));
            TextObject rpttxtdate = rptResource.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            rpttxtdate.Text = "(From " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";
            TextObject rpttxtgroupName = rptResource.ReportDefinition.ReportObjects["txtgroupName"] as TextObject;
            rpttxtgroupName.Text = "Group: " + this.ddlRptGroup.SelectedItem.Text.Trim();
            TextObject txtuserinfo = rptResource.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Resource Requirement Info";
                string eventdesc = "Print Report";
                string eventdesc2 = "Project Name: " + this.ddlPrjName.SelectedItem.ToString() + "Issue No.: " + (this.ddlIssueNO.SelectedValue.ToString() == "00000000000000" ? "All Issue No" : this.ddlIssueNO.SelectedItem.Text.Trim().Substring(0, 11));
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            rptResource.SetDataSource(dt);
            Session["Report1"] = rptResource;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }



        protected void ddlPrjName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetIssueNo();
        }

        protected void ddlIssueNO_SelectedIndexChanged(object sender, EventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlPrjName.SelectedValue.ToString();
            string isuno = this.ddlIssueNO.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "GETFLOORCOD", pactcode, isuno, frmdate, todate, "", "", "", "", "");
            DataTable dt = ds1.Tables[0];

            DataRow dr2 = dt.NewRow();
            dr2["flrcod"] = "000";
            dr2["flrdes"] = "All Floors-Sum";
            DataRow dr3 = dt.NewRow();
            dr3["flrcod"] = "AAA";
            dr3["flrdes"] = "All Floors-Details";
            dt.Rows.Add(dr2);
            dt.Rows.Add(dr3);
            DataView dv = dt.DefaultView;
            dv.Sort = "flrcod";
            dt = dv.ToTable();
            this.ddlFloorListRpt.DataTextField = "flrdes";
            this.ddlFloorListRpt.DataValueField = "flrcod";
            this.ddlFloorListRpt.DataSource = dt;
            this.ddlFloorListRpt.DataBind();



        }

        protected void gvRptResBasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvRptResBasis.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

    }
}
