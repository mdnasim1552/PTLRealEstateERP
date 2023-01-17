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
    public partial class InvResourceRequirement : System.Web.UI.Page
    {
        ProcessAccess ImpleData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtfrmDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }
            if (this.ddlImpNO.Items.Count == 0)
            {
                this.GetImplentNo();
                this.rbtnList1.SelectedIndex = 0;
            }
        }

        protected void GetImplentNo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string serch1 = "%" + this.txtProjectSearch.Text.Trim() + "%";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETIMPLEMENTATIONNO", frmdate, todate, serch1, "", "", "", "", "", "");
            if (ds1 == null)
                return;


            this.ddlImpNO.DataTextField = "voudesc";
            this.ddlImpNO.DataValueField = "vouno";
            this.ddlImpNO.DataSource = ds1.Tables[0];
            this.ddlImpNO.DataBind();
            this.ddlImpNO_SelectedIndexChanged(null, null);


        }



        protected void ImgbtnFindImpNo_Click(object sender, ImageClickEventArgs e)
        {
            this.GetImplentNo();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (this.rbtnList1.SelectedIndex == 0)
            {
                this.ResBasisRpt();
            }

            if (this.rbtnList1.SelectedIndex == 1)
            {
                this.ALLResBasisRpt();
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Inventory Resource Requirment";
                string eventdesc = "Show Report";
                string eventdesc2 = this.ddlImpNO.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void ALLResBasisRpt()
        {

            Session.Remove("tblRes");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string flrcod = this.ddlFloorListRpt.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));

            DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTALLPROJECTRESOURCEBASIS", frmdate, todate, flrcod, mRptGroup, "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvRptResBasis.DataSource = null;
                this.gvRptResBasis.DataBind();
                return;
            }

            DataTable dtres = ds2.Tables[0];
            Session["tblRes"] = ds2.Tables[0];
            this.FooterCal(dtres);
            this.gvRptResBasis.DataSource = ds2.Tables[0];
            this.gvRptResBasis.DataBind();

        }

        private void ResBasisRpt()
        {
            Session.Remove("tblRes");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string vouno = this.ddlImpNO.SelectedValue.ToString();
            string flrcod = this.ddlFloorListRpt.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));

            DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTRESOURCEBASIS", vouno, flrcod, mRptGroup, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvRptResBasis.DataSource = null;
                this.gvRptResBasis.DataBind();
                return;
            }

            DataTable dtres = ds2.Tables[0];
            Session["tblRes"] = ds2.Tables[0];
            this.FooterCal(dtres);
            this.gvRptResBasis.DataSource = ds2.Tables[0];
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

            if (this.rbtnList1.SelectedIndex == 0)
            {
                this.IndiVidualItemNo();

            }

            else
            {

                this.DatewiseProImp();

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Inventory Resource Requirment";
                string eventdesc = "Print Report";
                string eventdesc2 = this.ddlImpNO.SelectedItem.ToString() + " - " + this.rbtnList1.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        private void IndiVidualItemNo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblRes"];
            ReportDocument rptResource = new RealERPRPT.R_09_PImp.RptImpResourceBasis();

            TextObject rpttxtProName = rptResource.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            rpttxtProName.Text = "Project Description: " + this.ddlImpNO.SelectedItem.Text.Trim();
            TextObject rpttxtfdes = rptResource.ReportDefinition.ReportObjects["flrdes"] as TextObject;
            rpttxtfdes.Text = this.ddlFloorListRpt.SelectedItem.Text.Trim();
            TextObject rptdate = rptResource.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "(From " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";
            TextObject txtuserinfo = rptResource.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptResource.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptResource.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptResource;
            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void DatewiseProImp()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblRes"];
            ReportDocument rptResource = new RealERPRPT.R_09_PImp.RptImpDatawiseResource(); // All Project

            TextObject rptdate = rptResource.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "(From " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";
            TextObject txtuserinfo = rptResource.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptResource.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptResource.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptResource;
            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



        protected void ddlImpNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string vouno = this.ddlImpNO.SelectedValue.ToString();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETFLOORCOD", vouno, "", "", "", "", "", "", "", "");
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
    }
}
