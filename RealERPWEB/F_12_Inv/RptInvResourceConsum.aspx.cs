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
namespace RealERPWEB.F_12_Inv
{
    public partial class RptInvResourceConsum : System.Web.UI.Page
    {
        ProcessAccess ImpleData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Ind. Material Consumtion";

                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
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

        protected void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%" + this.txtProjectSearch.Text.Trim() + "%";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTPROJECTFORBGD", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }

        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text;
                this.ddlProjectName.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.Panel2.Visible = true;
                this.ShowFloor();
                this.ShowResource();




            }

            else
            {
                this.lbtnOk.Text = "Ok";
                this.ddlProjectName.Visible = true;
                this.lblProjectdesc.Visible = false;
                this.Panel2.Visible = false;
                this.lblPage.Visible = false;
                this.ddlpagesize.Visible = false;
                this.gvRptResBasis.DataSource = null;
                this.gvRptResBasis.DataBind();

            }
            if (ConstantInfo.LogStatus == true)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string eventtype = "Resource Basis Reprot";
                string eventdesc = "Show Report:";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }



        private void ShowFloor()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string projectName = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTFLRCODFORBGD", projectName, "", "", "", "", "", "", "", "");
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

        private void ShowResource()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string serch1 = "%" + this.txtResSearch.Text.Trim() + "%";
            DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RESCODELISTRPT", serch1, pactcode, "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            this.ddlResource.DataTextField = "rsirdesc1";
            this.ddlResource.DataValueField = "rsircode";
            this.ddlResource.DataSource = ds2.Tables[0];
            this.ddlResource.DataBind();

        }


        protected void ImgbtnFindResource_Click(object sender, EventArgs e)
        {
            this.ShowResource();
        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {

            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            Session.Remove("tblRes");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string floor = this.ddlFloorListRpt.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string rescode = this.ddlResource.SelectedValue.ToString();

            DataSet ds3 = ImpleData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTINDRESCONSUM", pactcode, floor, mRptGroup, date, "", "", rescode, "", "");
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
                string eventtype = "Resource Basis Reprot";
                string eventdesc = "Show Materials wise Report:";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString() + "- " + this.ddlResource.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }

        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblRes"];
            this.gvRptResBasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvRptResBasis.DataSource = dt;
            this.gvRptResBasis.DataBind();
            ((Label)this.gvRptResBasis.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rptamt)", "")) ?
                0.00 : dt.Compute("sum(rptamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvRptResBasis.FooterRow.FindControl("lgvFqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rptqty)", "")) ?
                0.00 : dt.Compute("sum(rptqty)", ""))).ToString("#,##0.00;(#,##0.00); ");



        }




        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ResCode = this.ddlResource.SelectedValue.ToString();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "INDIVIDUALRES", ResCode, "", "", "", "", "", "", "", "");
            DataTable dt = (DataTable)Session["tblRes"];

            string txtProject = this.ddlProjectName.SelectedItem.ToString().Substring(13);
            string txtFloor = this.ddlFloorListRpt.SelectedItem.ToString();
            string txtMaterial = ds1.Tables[0].Rows[0]["sirdesc"].ToString() + "  " + ds1.Tables[0].Rows[0]["sirunit"].ToString();

            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            var lst = dt.DataTableToList<RealEntity.C_12_Inv.RptInResource>();


            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.rptInResource", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));


            Rpt1.SetParameters(new ReportParameter("txtMaterial", txtMaterial));
            Rpt1.SetParameters(new ReportParameter("txtFloor", txtFloor));
            Rpt1.SetParameters(new ReportParameter("txtProject", txtProject));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Budgeted Material Requirements"));


            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Resource Basis Reprot";
                string eventdesc = "Print Report:";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13) + "- " + this.ddlResource.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptResource = new RealERPRPT.R_12_Inv.rptInResource();
            //TextObject rpttxtComName = rptResource.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            //rpttxtComName.Text = comnam;
            //TextObject rpttxtHeader = rptResource.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //rpttxtHeader.Text = "Budgeted Material Requirements";
            //TextObject rpttxtProName = rptResource.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //rpttxtProName.Text = this.ddlProjectName.SelectedItem.ToString().Substring(13);

            //TextObject rpttxtFloor = rptResource.ReportDefinition.ReportObjects["txtfloor"] as TextObject;
            //rpttxtFloor.Text = this.ddlFloorListRpt.SelectedItem.ToString();
            //TextObject rpttxtResourceName = rptResource.ReportDefinition.ReportObjects["rptResource"] as TextObject;
            //string ResourceUnit = ds1.Tables[0].Rows[0]["sirdesc"].ToString() + "  " + ds1.Tables[0].Rows[0]["sirunit"].ToString();
            //rpttxtResourceName.Text = ResourceUnit;
            //TextObject txtuserinfo = rptResource.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Resource Basis Reprot";
            //    string eventdesc = "Print Report:";
            //    string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13) + "- " + this.ddlResource.SelectedItem.ToString();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //} 
            //rptResource.SetDataSource(dt);
            //Session["Report1"] = rptResource;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void gvRptResBasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvRptResBasis.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
    }
}
