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
namespace RealERPWEB.F_23_CR
{
    public partial class RptReceivedList : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            }




        }

        private void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");

            string txtSProject = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.cblProject.DataTextField = "pactdesc";
            this.cblProject.DataValueField = "pactcode";
            this.cblProject.DataSource = ds1.Tables[0];
            this.cblProject.DataBind();

        }
        protected void ibtnFindProject_Click(object sender, ImageClickEventArgs e)
        {
            this.GetProjectName();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptRcvList = new RealERPRPT.R_22_Sal.RptReceivedlist();
            TextObject rptdate = rptRcvList.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "As On " + Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM dd, yyyy");
            TextObject txtuserinfo = rptRcvList.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptRcvList.SetDataSource(this.HiddenSameData((DataTable)Session["tblAccRec"]));
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Received List Info";
                string eventdesc = "Print Report MR";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptRcvList.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptRcvList;
            lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
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
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblAccRec"];
            this.dgvAccRec.DataSource = dt;
            this.dgvAccRec.DataBind();
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.dgvAccRec.FooterRow.FindControl("lgvAcAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(uamt)", "")) ?
                0.00 : dt.Compute("Sum(uamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.dgvAccRec.FooterRow.FindControl("lgvRecAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ramt)", "")) ?
                0.00 : dt.Compute("Sum(ramt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.dgvAccRec.FooterRow.FindControl("lgvBalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bamt)", "")) ?
                0.00 : dt.Compute("Sum(bamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.dgvAccRec.FooterRow.FindControl("lgvFDueAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueamt)", "")) ?
                0.00 : dt.Compute("Sum(dueamt)", ""))).ToString("#,##0;(#,##0); ");




        }


        protected void lnkOk_Click(object sender, EventArgs e)
        {

            string fieldinfo = "";

            for (int i = 0; i < this.cblProject.Items.Count; i++)
            {
                if (cblProject.Items[i].Selected)
                {

                    fieldinfo = fieldinfo + cblProject.Items[i].Value.ToString();
                    if (cblProject.Items[i].Value.ToString() == "000000000000")
                        fieldinfo = cblProject.Items[i].Value.ToString();

                }


            }

            if (fieldinfo.Length == 0) return;
            //fieldinfo = (fieldinfo == "000000000000") ? fieldinfo : fieldinfo.Substring(0, (fieldinfo.Length - 2));


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");

            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "DATEWISERECEIVEDLISTSP", fieldinfo, fromdate, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.dgvAccRec.DataSource = null;
                this.dgvAccRec.DataBind();
                return;
            }
            this.dgvAccRec.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            // this.dgvAccRec.Columns[2].Visible = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? true : false;
            Session["tblAccRec"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }
        protected void dgvAccRec_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvAccRec.PageIndex = e.NewPageIndex;
            this.dgvAccRec.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dgvAccRec.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }

        protected void chkDeselectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkDeselectAll.Checked)
            {
                for (int i = 0; i < this.cblProject.Items.Count; i++)
                    cblProject.Items[i].Selected = false;
            }

        }
    }
}











