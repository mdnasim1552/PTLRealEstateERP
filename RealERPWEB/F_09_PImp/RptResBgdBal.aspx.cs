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
    public partial class RptResBgdBal : System.Web.UI.Page
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
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Budget Balance (Resource)";
                if (this.ddlProjectName.Items.Count == 0)
                {
                    this.GetProjectName();

                }

            }
        }

        protected void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%" + this.txtProjectSearch.Text.Trim() + "%";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "GETPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblRes"];
            string PrjName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            string grpname = this.ddlRptGroup.SelectedItem.Text.Trim();
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rrs1 = new RealERPRPT.R_09_PImp.RptResBgdBalance();

            TextObject rpttxtprjname = rrs1.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            rpttxtprjname.Text = PrjName;
            TextObject txtTitle = rrs1.ReportDefinition.ReportObjects["txtgroup"] as TextObject;
            txtTitle.Text = "Group: " + grpname;
            TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtFDate1.Text = "As On " + date;
            TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rrs1.SetDataSource(dt1);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Buget Balance Report";
                string eventdesc = "Print Report:";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rrs1.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rrs1;
            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblRes"];
            this.gvRptResBasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvRptResBasis.DataSource = dt;
            this.gvRptResBasis.DataBind();
            if (dt.Rows.Count == 0)
                return;
            //((Label)this.gvRptResBasis.FooterRow.FindControl("lgvfbgdqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdqty)", "")) ?
            //    0.00 : dt.Compute("sum(bgdqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvRptResBasis.FooterRow.FindControl("lgvfexqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(exqty)", "")) ?
            //    0.00 : dt.Compute("sum(exqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvRptResBasis.FooterRow.FindControl("lgvfrecvqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recvqty)", "")) ?
            //    0.00 : dt.Compute("sum(recvqty)", ""))).ToString("#,##0.00;(#,##0.00); ");

            //((Label)this.gvRptResBasis.FooterRow.FindControl("lgvfbalqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balqty)", "")) ?
            //   0.00 : dt.Compute("sum(balqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvRptResBasis.FooterRow.FindControl("lgvFbalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balamt)", "")) ?
                0.00 : dt.Compute("sum(balamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvRptResBasis.FooterRow.FindControl("lgvFrbalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rbalamt)", "")) ?
                0.00 : dt.Compute("sum(rbalamt)", ""))).ToString("#,##0.00;(#,##0.00); ");




        }

        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblRes");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds3 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "RPTBUDGETBALRESBAS", pactcode, mRptGroup, date, "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvRptResBasis.DataSource = null;
                this.gvRptResBasis.DataBind();
                return;
            }

            if (ds3.Tables[0].Rows.Count == 0)
                return;
            Session["tblRes"] = ds3.Tables[0];
            this.LoadGrid();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Buget Balance Report";
                string eventdesc = "Show Report:";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
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
