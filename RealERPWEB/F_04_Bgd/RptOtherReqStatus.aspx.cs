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
namespace RealERPWEB.F_04_Bgd
{
    public partial class RptOtherReqStatus : System.Web.UI.Page
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

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "General REQUISITION STATUS";

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                // this.ChkBalance.Checked = false;
                //this.rbtnList1.SelectedIndex = 0;
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                if (this.ddlProjectName.Items.Count == 0)
                {
                    this.GetProjectName();
                }

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

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

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            // *****Iqbal Nayan    
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            LocalReport Rpt1 = new LocalReport();
            DataTable dt1 = (DataTable)Session["tblstatus"];
            var lst = dt1.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.OtherReqStatus>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptOtherReqStatus01", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("date", "From " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlProjectName.SelectedItem.ToString().Substring(13)));
            //Rpt1.SetParameters(new ReportParameter("Resource", this.ddlReports.SelectedItem.Text.Trim()));
            //Rpt1.SetParameters(new ReportParameter("Floor", this.ddlFloorListRpt.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Work List"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            //DataTable dt1 = (DataTable)Session["tblstatus"];
            //ReportDocument rrs2 = new RealERPRPT.R_04_Bgd.RptOtherReqStatus01();//.RptRequisitionStatus2();
            //TextObject rptCname = rrs2.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject txtFDate1 = rrs2.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = "From " + fromdate + " To " + todate;

            //TextObject txtuserinfo = rrs2.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rrs2.SetDataSource(dt1);
            //Session["Report1"] = rrs2;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //if (ConstantInfo.LogStatus == true)
            //{
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    string eventtype = "Requsition Status";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = rbtnList1.SelectedItem.ToString();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}            
        }

        private void ProjectBasisStatus()
        {
            if (this.lnkbtnOk.Text == "Ok")
            {
                this.lnkbtnOk_Click(null, null);
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            DataTable dt1 = (DataTable)Session["tblstatus"];
            ReportDocument rrs2 = new RealERPRPT.R_14_Pro.RptRequisitionStatus2();
            TextObject rptCname = rrs2.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = comnam;
            TextObject txtFDate1 = rrs2.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtFDate1.Text = "From " + fromdate + " To " + todate;

            TextObject txtuserinfo = rrs2.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rrs2.SetDataSource(dt1);
            Session["Report1"] = rrs2;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //  this.ChkBalance.Checked = false;
        }

        private void RequisitionBasisStatus()
        {
            if (this.lnkbtnOk.Text == "Ok")
            {
                this.lnkbtnOk_Click(null, null);
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");

            DataTable dt1 = (DataTable)Session["tblstatus"];
            ReportDocument rrs1 = new RealERPRPT.R_14_Pro.RptRequisitionStatus1();
            TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = comnam;
            TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtFDate1.Text = "From " + fromdate + " To " + todate;
            TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rrs1.SetDataSource(dt1);
            Session["Report1"] = rrs1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //this.ChkBalance.Checked = false;

        }



        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }
        protected void imgbtnFindRequiSition_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }
        private void LoadData()
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            // string basis = this.rbtnList1.SelectedItem.Text;
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            if (pactcode == "000000000000")
                pactcode = "%%";
            string balance = "";// (this.ChkBalance.Checked) ? "woz" : "";
            string Reqno = this.txtSrcRequisition.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "OTHERREQSATIONSTATUS", fromdate, todate, pactcode, balance, Reqno, "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvOtherReqStatus.DataSource = null;
                this.gvOtherReqStatus.DataBind();
                return;

            }
            DataTable dt1 = this.HiddenSameDate(ds1.Tables[0]);
            Session["tblstatus"] = dt1;
            this.LoadGv();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Requsition Status";
                string eventdesc = "Show Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void LoadGv()
        {
            DataTable dt = (DataTable)Session["tblstatus"];
            this.gvOtherReqStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvOtherReqStatus.DataSource = dt;
            this.gvOtherReqStatus.DataBind();

            if (dt.Rows.Count > 0)
            {
                ((Label)this.gvOtherReqStatus.FooterRow.FindControl("lblgvFReqAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(proamt)", "")) ?
                                        0 : dt.Compute("sum(proamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvOtherReqStatus.FooterRow.FindControl("lblgvFAppamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(appamt)", "")) ?
                                       0 : dt.Compute("sum(appamt)", ""))).ToString("#,##0;(#,##0); ");
            }
        }


        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string reqno = dt1.Rows[0]["reqno"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["reqno"].ToString() == reqno)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    reqno = dt1.Rows[j]["reqno"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                    dt1.Rows[j]["reqno1"] = "";
                    dt1.Rows[j]["reqdat1"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                    {
                        dt1.Rows[j]["pactdesc"] = "";
                    }
                    if (dt1.Rows[j]["reqno"].ToString() == reqno)
                    {
                        dt1.Rows[j]["reqno1"] = "";
                    }
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    reqno = dt1.Rows[j]["reqno"].ToString();
                }
            }
            return dt1;

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGv();
        }
        protected void gvReqStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvOtherReqStatus.PageIndex = e.NewPageIndex;
            this.LoadGv();
        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

    }
}