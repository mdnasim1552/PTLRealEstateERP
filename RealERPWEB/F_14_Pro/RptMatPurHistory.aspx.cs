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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_14_Pro
{
    public partial class RptMatPurHistory : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

                //((Label)this.Master.FindControl("lblTitle")).Text = "PURCHASE HISTORY - MATERIAL WISE";
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                if (this.ddlProjectName.Items.Count == 0)
                {
                    this.GetProjectName();
                    this.GetMaterial();
                }


            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;

        }

        private void GetProjectName()
        {

            string comcod = this.GetComeCode();
            string txtSProject = this.txtSrcProject.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPROJECTNAMEFORREQ", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void imgbtnFindSupplier_Click(object sender, EventArgs e)
        {
            //this.GetSupplier();
        }

        private void GetMaterial()
        {
            string comcod = this.GetComeCode();
            string txtfindMat = '%' + this.txtSrcMat.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETMATERIALHISTORY", txtfindMat, "", "", "", "", "", "", "", "");
            this.ddlMaterialName.DataTextField = "rsirdesc";
            this.ddlMaterialName.DataValueField = "rsircode";
            this.ddlMaterialName.DataSource = ds1.Tables[0];
            this.ddlMaterialName.DataBind();

        }

        protected void imgbtnFindMat_Click(object sender, EventArgs e)
        {
            this.GetMaterial();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            DataTable dt = (DataTable)Session["MatPurHis"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptPurHisMatWise>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptMatPurHistory", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtDate", "From: " + Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", this.ddlMaterialName.SelectedItem.Text.Trim().Substring(14) + "         " + this.lblUnit.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetComeCode ();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["MatPurHis"];

            //ReportDocument rptstate = new RealERPRPT.R_14_Pro.RptMatPurHistory();
            //TextObject rpttxtMaterial = rptstate.ReportDefinition.ReportObjects["txtMaterial"] as TextObject;
            //rpttxtMaterial.Text = this.ddlMaterialName.SelectedItem.Text.Trim().Substring(14)+"         "+this.lblUnit.Text.Trim();
            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            //rptftdate.Text = "From: " + Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MM-yyyy");
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource(dt);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Materials Purchase History";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.ddlProjectName.SelectedItem.ToString() + " From: " + Convert.ToDateTime(this.txtFDate.Text).ToString("dd/MM/yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd/MM/yyyy");
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("MatPurHis");
            string comcod = this.GetComeCode();
            string proname = ddlProjectName.SelectedValue.Substring(0, 12).ToString();
            string matname = this.ddlMaterialName.SelectedValue.Substring(0, 12).ToString();
            string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTMATPURHISTORY", proname, matname, frmdate, todate, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMatPurHis.DataSource = null;
                this.gvMatPurHis.DataBind();
                return;
            }

            this.gvMatPurHis.Columns[3].Visible = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? true : false;
            this.lblUnit.Text = ds1.Tables[1].Rows[0]["sirunit"].ToString();
            Session["MatPurHis"] = this.HiddenSameDate(ds1.Tables[0]);
            this.Data_Bind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase History";
                string eventdesc = "Show Report";
                string eventdesc2 = this.ddlProjectName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void Data_Bind()
        {
            this.gvMatPurHis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvMatPurHis.DataSource = (DataTable)Session["MatPurHis"];
            this.gvMatPurHis.DataBind();
            this.FooterCalculation();
        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["MatPurHis"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvMatPurHis.FooterRow.FindControl("lgvMRRQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrrqty)", "")) ?
                                0 : dt.Compute("sum(mrrqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMatPurHis.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrramt)", "")) ?
                                0 : dt.Compute("sum(mrramt)", ""))).ToString("#,##0;(#,##0); ");

        }
        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string mrrno = dt1.Rows[0]["mrrno"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if ((dt1.Rows[j]["mrrno"].ToString() == mrrno))
                {
                    mrrno = dt1.Rows[j]["mrrno"].ToString();
                    dt1.Rows[j]["mrrno1"] = "";
                    dt1.Rows[j]["mrrdat1"] = "";
                }

                else
                {
                    mrrno = dt1.Rows[j]["mrrno"].ToString();
                }

            }
            return dt1;

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMaterial();
        }
        protected void ImgBtnMatName_Click(object sender, EventArgs e)
        {
            this.GetMaterial();
        }
        protected void gvMatPurHis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMatPurHis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}