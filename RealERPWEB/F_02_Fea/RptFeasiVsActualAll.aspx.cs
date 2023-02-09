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
namespace RealERPWEB.F_02_Fea
{
    public partial class RptFeasiVsActualAll : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                //((Label)this.Master.FindControl("lblTitle")).Text = "Top Sheet Feasibility Vs Actual"; //(Request.QueryString["Type"].ToString().Trim() == "FeInSumm") ? "Income Statement All Project(Summary)" : "Feasibility Report-All Project(Summary)";
                this.ShowFeaVSActual();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptProfea = new ReportDocument();
            rptProfea = new RealERPRPT.R_02_Fea.RptFeasVsActual();

            TextObject rpttxtCompanyName = rptProfea.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rpttxtCompanyName.Text = comnam;

            //TextObject rpttxtDate = rptProfea.ReportDefinition.ReportObjects["TxtDate"] as TextObject;
            //rpttxtDate.Text = "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy"); ;

            TextObject txtuserinfo = rptProfea.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptProfea.SetDataSource((DataTable)Session["tbfeaall"]);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sold Info";
                string eventdesc = "Print Report Sold Inf";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptProfea.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptProfea;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void ShowFeaVSActual()
        {

            string comcod = this.GetCompCode();
            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY", "RPTFEASIVEACTUAL", "", "", "", "", "", "", "", "", "");

            //DataSet ds2 = CustData.GetTransInfo(comcod, "SP_ENTRY_FEA_PROFEASIBILITY", "RPTPROJECTFEASIBILITYALL", "", "", "", "", "", "", "", "", "");
            //DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY", CallType, "", "", "", "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.grvFeaVsAct.DataSource = null;
                this.grvFeaVsAct.DataBind();
                return;
            }
            Session["tbfeaall"] = ds2.Tables[0];
            this.Data_Bind();

        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tbfeaall"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.grvFeaVsAct.FooterRow.FindControl("lgvFSFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(feasize)", "")) ?
                              0 : dt.Compute("sum(feasize)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvFeaVsAct.FooterRow.FindControl("lgvFSATotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(actsize)", "")) ?
                            0 : dt.Compute("sum(actsize)", ""))).ToString("#,##0;(#,##0); ");

            //((Label)this.grvFeaVsAct.FooterRow.FindControl("lgvFRevenue")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(revamt)", "")) ?
            //                   0 : dt.Compute("sum(revamt)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.grvFeaVsAct.FooterRow.FindControl("lgvFCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(costamt)", "")) ?
            //                0 : dt.Compute("sum(costamt)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.grvFeaVsAct.FooterRow.FindControl("lgvFmargin")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(prolos)", "")) ?
            //                    0 : dt.Compute("sum(prolos)", ""))).ToString("#,##0;(#,##0); ");

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbfeaall"];
            this.grvFeaVsAct.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvFeaVsAct.DataSource = dt;
            this.grvFeaVsAct.DataBind();
            this.FooterCalculation();
        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();

        }

        protected void grvFeallAnly_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvFeaVsAct.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void grvFeaVsAct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label difference = (Label)e.Row.FindControl("lgvDiffFea");
            Label parcent = (Label)e.Row.FindControl("lgvParFea");


            string differ = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "differ")).ToString().Trim();

            if (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "differ")) < 0)
            {
                difference.ForeColor = System.Drawing.Color.Red;
                parcent.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}