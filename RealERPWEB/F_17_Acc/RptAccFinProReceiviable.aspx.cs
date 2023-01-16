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
namespace RealERPWEB.F_17_Acc
{
    public partial class RptAccFinProReceiviable : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
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
                this.SelectView();
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();


            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "AccRec":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "AccRecSum":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

            }
        }
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RECEIVIABLE", "GETACRECPRONAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }
        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            Session.Remove("tblfinproject");
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string projectcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "000000000000" : this.ddlProjectName.SelectedValue.ToString();

            string searchinfo = "";
            if (this.ddlSrchCash.SelectedValue != "")
            {

                if (this.ddlSrchCash.SelectedValue == "between")
                {
                    searchinfo = "(balam between " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmountC2.Text.Trim()).ToString() + " )";

                }

                else
                {
                    searchinfo = "( balam " + this.ddlSrchCash.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " )";

                }
            }
            string CallType = (this.Request.QueryString["Type"].ToString().Trim() == "AccRec") ? "RPTFPRRECEIVIABLE" : "RPTFPRRECEIVIABLESUM";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RECEIVIABLE", CallType, date, projectcode, searchinfo, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblfinproject"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            string actcode = dt1.Rows[0]["actcode"].ToString();
            switch (Type)
            {
                case "AccRec":
                    //case "AccRecSum":
                    string grp = dt1.Rows[0]["grp"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp && dt1.Rows[j]["actcode"].ToString() == actcode)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            actcode = dt1.Rows[j]["actcode"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                            dt1.Rows[j]["actdesc"] = "";

                        }

                        else
                        {
                            if (dt1.Rows[j]["actcode"].ToString() == actcode)
                            {
                                dt1.Rows[j]["actdesc"] = "";
                            }

                            if (dt1.Rows[j]["grp"].ToString() == grp)
                            {
                                dt1.Rows[j]["grpdesc"] = "";
                            }

                            grp = dt1.Rows[j]["grp"].ToString();
                            actcode = dt1.Rows[j]["actcode"].ToString();

                        }

                    }
                    break;
                    //case "AccRecSum":
                    //    for (int j = 1; j < dt1.Rows.Count; j++)
                    //    {
                    //        if (dt1.Rows[j]["actcode"].ToString() == actcode)
                    //        {
                    //            actcode = dt1.Rows[j]["actcode"].ToString();
                    //            dt1.Rows[j]["actdesc"] = "";
                    //        }

                    //        else
                    //            actcode = dt1.Rows[j]["actcode"].ToString();
                    //    }
                    //break;
            }
            return dt1;


        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblfinproject"];
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "AccRec":
                    this.gvfinProject.PageSize = Convert.ToInt16(this.ddlpagesize.SelectedValue.ToString());
                    this.gvfinProject.DataSource = dt;
                    this.gvfinProject.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    ((Label)this.gvfinProject.FooterRow.FindControl("lgvFtamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamt)", "")) ?
                                   0 : dt.Compute("sum(tamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvfinProject.FooterRow.FindControl("lgvFSalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salam)", "")) ?
                                   0 : dt.Compute("sum(salam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvfinProject.FooterRow.FindControl("lgvFRecAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recam)", "")) ?
                                   0 : dt.Compute("sum(recam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvfinProject.FooterRow.FindControl("lgvFBalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balam)", "")) ?
                                   0 : dt.Compute("sum(balam)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "AccRecSum":
                    this.grvAccRecSum.PageSize = Convert.ToInt16(this.ddlpagesize.SelectedValue.ToString());
                    this.grvAccRecSum.DataSource = dt;
                    this.grvAccRecSum.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    ((Label)this.grvAccRecSum.FooterRow.FindControl("lgvFtamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamt)", "")) ?
                                   0 : dt.Compute("sum(tamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvAccRecSum.FooterRow.FindControl("lgvFusold")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(usold)", "")) ?
                                   0 : dt.Compute("sum(usold)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvAccRecSum.FooterRow.FindControl("lgvFSalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salam)", "")) ?
                                   0 : dt.Compute("sum(salam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvAccRecSum.FooterRow.FindControl("lgvFRecAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recam)", "")) ?
                                   0 : dt.Compute("sum(recam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvAccRecSum.FooterRow.FindControl("lgvFBalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balam)", "")) ?
                                   0 : dt.Compute("sum(balam)", ""))).ToString("#,##0;(#,##0); ");
                    break;

            }




        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvfinProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvfinProject.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "AccRec":
                    this.RtpAccRec();
                    break;

                case "AccRecSum":
                    this.RtpAccRecSum();
                    break;
            }

        }
        private void RtpAccRec()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

                ReportDocument rptinfo = new RealERPRPT.R_17_Acc.rptAccFinProReceivable();//.rptAccPaymntApp();
                TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["companyname"] as TextObject;
                txtCompanyName.Text = comnam;
                TextObject txtcDate = rptinfo.ReportDefinition.ReportObjects["txtAsondate"] as TextObject;
                txtcDate.Text = "As on Date : " + this.txtdate.Text.ToString();

                TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                rptinfo.SetDataSource((DataTable)Session["tblfinproject"]);
                Session["Report1"] = rptinfo;
                this.lbljavascript.Text = "<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }
        private void RtpAccRecSum()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                ReportDocument rptinfo = new RealERPRPT.R_17_Acc.rptAccFinProReceivable2();//.rptAccPaymntApp();
                TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["companyname"] as TextObject;
                txtCompanyName.Text = comnam;
                TextObject txtcDate = rptinfo.ReportDefinition.ReportObjects["txtAsondate"] as TextObject;
                txtcDate.Text = "As on Date : " + this.txtdate.Text.ToString();

                TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                rptinfo.SetDataSource((DataTable)Session["tblfinproject"]);
                Session["Report1"] = rptinfo;
                this.lbljavascript.Text = "<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }
        protected void ibtnFindProject_Click(object sender, ImageClickEventArgs e)
        {
            this.GetProjectName();
        }
        protected void ddlSrchCash_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblToCash.Visible = (this.ddlSrchCash.SelectedValue == "between");
            this.txtAmountC2.Visible = (this.ddlSrchCash.SelectedValue == "between");
        }
        protected void grvAccRecSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {


        }
        protected void gvfinProject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvActdesc");
            string mCOMCOD = comcod;

            string mACTCODE = ((Label)e.Row.FindControl("lblgvActcode")).Text;
            string mRESCODE = ((Label)e.Row.FindControl("lblgvAcRes")).Text;
            string mTRNDAT1 = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string mACTDESC = ((Label)e.Row.FindControl("lblgvActDesc")).Text;
            string mREESDESC = ((Label)e.Row.FindControl("lblgvResDesc")).Text;

            hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=AccRec&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&rescode=" + mRESCODE + "&Date1=" + mTRNDAT1
                    + "&actdesc=" + mACTDESC + "&resdesc=" + mREESDESC;


        }
    }
}