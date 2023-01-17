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
using RealERPRDLC;

namespace RealERPWEB.F_09_PImp
{
    public partial class RptSubConBill : System.Web.UI.Page
    {
        ProcessAccess BgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString() == "SubBill") ? "Sub-Contractor Bill" : " 	Periodic Sub-Contractor Bill";
                this.txtFDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            }
            if (this.ddlProjectName.Items.Count == 0)
            {
                this.GetProjectName();

            }
            if (this.ddlSubName.Items.Count == 0)
            {
                this.GetConTractorName();

            }
            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {

                case "SubBill":
                    this.lblDate.Visible = false;
                    this.txtFDate.Visible = false;

                    break;

                case "SubConBill":
                    this.lbldateTo.Text = "To";
                    break;
            }


        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            if (Request.QueryString["prjcode"].Length > 0)
            {
                this.ddlProjectName.SelectedValue = Request.QueryString["prjcode"].ToString();
                ddlProjectName.Enabled = false;
            }
            this.ShowFloorcode();

        }

        private void GetConTractorName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string serch1 = "%" + this.txtSrcSub.Text.Trim() + "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURSUBNAME", pactcode, serch1, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlSubName.DataTextField = "csirdesc";
            this.ddlSubName.DataValueField = "csircode";
            this.ddlSubName.DataSource = ds1.Tables[0];
            this.ddlSubName.DataBind();
            if (Request.QueryString["prjcode"].Length > 0)
            {
                ddlSubName.SelectedValue = "000000000000";
            }

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void ibtnFindSubConName_Click(object sender, EventArgs e)
        {
            this.GetConTractorName();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetConTractorName();
            this.ShowFloorcode();
        }

        private void ShowFloorcode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", "GETFLOORCOD", pactcode, "", "", "", "", "", "", "", "");
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
            this.ddlFloorListRpt.SelectedValue = "AAA";
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            //if (this.lbtnOk.Text == "Ok")
            //{

            //    this.lbtnOk.Text = "New";
            //this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);           
            //this.lblSubDesc.Text = this.ddlSubName.SelectedItem.Text.Substring(13);
            //this.ddlProjectName.Visible = false;
            //this.lblProjectdesc.Visible = true;
            //this.ddlSubName.Visible = false;
            //this.lblSubDesc.Visible=true;
            // this.lblPage.Visible = true;
            //this.ddlpagesize.Visible = true;
            //this.LoadGrid();
            //this.lbljavascript.Text = "";    
            this.ShowValue();

            //}
            //else
            //{
            //    this.lbtnOk.Text = "Ok";          
            //    this.ClearScreen();
            //}
        }
        private void ShowValue()
        {
            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {

                case "SubBill":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowSubBill();
                    break;
                case "SubConBill":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.ShowSubConBill();
                    break;
            }

            if (ConstantInfo.LogStatus == true)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                // string eventtype = this.HeaderText.Text;
                string eventdesc = "Show Report: " + Type;
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString();
                //   bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void ClearScreen()
        {
            this.ddlProjectName.Visible = true;
            //this.lblProjectdesc.Text = "";
            //this.lblProjectdesc.Visible = false;
            //this.lblSubDesc.Text = "";
            this.ddlSubName.Visible = true;
            //this.lblSubDesc.Visible = false;
            this.lblPage.Visible = false;
            this.ddlpagesize.Visible = false;
            this.gvSubBill.DataSource = null;
            this.gvSubBill.DataBind();

        }



        private void ShowSubBill()
        {

            Session.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string SubconName = this.ddlSubName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            //----------
            string flrcod = this.ddlFloorListRpt.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            //-----------
            string Calltype = (this.ddlSubName.SelectedValue.ToString() == "000000000000") ? "RPTALLSUBCONBILL" : "RPTSUBCONBILL";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", Calltype, PactCode, SubconName, date, flrcod, mRptGroup, "", "", "", "");
            if (ds1 == null)
            {
                this.gvSubBill.DataSource = null;
                this.gvSubBill.DataBind();
                return;
            }
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.gvSubBill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSubBill.DataSource = ds1.Tables[0];
            this.gvSubBill.DataBind();
            Session["tblData"] = ds1.Tables[0];
            this.FooterCalculation();

        }

        private void ShowSubConBill()
        {

            Session.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            //string SubconName = this.ddlSubName.SelectedValue.ToString();
            string Fdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string Todate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string SubconName = (this.ddlSubName.SelectedValue.ToString() == "000000000000") ? "%%" : this.ddlSubName.SelectedValue.ToString();

            string flrcod = this.ddlFloorListRpt.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));

            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTPERIODICSUBCONBILL", PactCode, SubconName, Fdate, Todate, flrcod, mRptGroup, "", "", "");
            if (ds1 == null)
            {
                this.gvSubCon.DataSource = null;
                this.gvSubCon.DataBind();
                return;
            }
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.gvSubCon.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSubCon.DataSource = ds1.Tables[0];
            this.gvSubCon.DataBind();
            Session["tblData"] = ds1.Tables[0];
            this.FooterCalculation();

        }


        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblData"];
            if (dt.Rows.Count == 0)
                return;
            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "SubBill":
                    ((Label)this.gvSubBill.FooterRow.FindControl("lgvBgdFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rptamt)", "")) ?
                                       0 : dt.Compute("sum(rptamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSubBill.FooterRow.FindControl("lgvSubFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(subconamt)", "")) ?
                                    0 : dt.Compute("sum(subconamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSubBill.FooterRow.FindControl("lgvFdiffAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(difamt)", "")) ?
                                    0 : dt.Compute("sum(difamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "SubConBill":
                    ((Label)this.gvSubCon.FooterRow.FindControl("lgvFPreAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(preamt)", "")) ?
                                       0 : dt.Compute("sum(preamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSubCon.FooterRow.FindControl("lgvFCurAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curamt)", "")) ?
                                    0 : dt.Compute("sum(curamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSubCon.FooterRow.FindControl("lgvFTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalamt)", "")) ?
                                    0 : dt.Compute("sum(totalamt)", ""))).ToString("#,##0;(#,##0); ");

                    break;
            }

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "SubBill":
                    this.rptPrint_subconbill();
                    break;
                case "SubConBill":
                    this.Print_ShowSubConBill();
                    break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                // string eventtype = this.HeaderText.Text;
                string eventdesc = "Print Report: " + Type;
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString();
                // bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void rptPrint_subconbill()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblData"];
            var list = dt.DataTableToList<RealEntity.C_09_PIMP.SubConBill.SubConAllBill>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_09_PIMP.rptsubconbill", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("projectName", this.ddlProjectName.SelectedItem.Text.ToString().Substring(17)));
            Rpt1.SetParameters(new ReportParameter("txtSubConName", "Sub-Contractor Name: " + this.ddlSubName.SelectedItem.Text.ToString().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Bill Details - Sub-Contractor"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void Print_ShowSubConBill()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblData"];
            var list = dt.DataTableToList<RealEntity.C_09_PIMP.SubConBill.SubConAllBill>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_09_PIMP.RptPerodSubConBill", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("projectName", this.ddlProjectName.SelectedItem.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("txtSubConName", "Sub-Contractor Name: " + this.ddlSubName.SelectedItem.Text.ToString()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Periodic Subcontractor Bill"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy") + " To :" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "SubBill":
                    this.ShowSubBill();
                    break;
                case "SubConBill":
                    this.ShowSubConBill();
                    break;
            }

        }

        protected void gvSubBill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSubBill.PageIndex = e.NewPageIndex;
            this.ShowSubBill();
        }


        //protected void gvSubBill_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Label resdesc = (Label)e.Row.FindControl("lgcRptDesc");
        //        Label Bgdqty = (Label)e.Row.FindControl("lgvBgdqty");
        //        Label SubConQty = (Label)e.Row.FindControl("lgvSubqty");
        //        Label Diffqty = (Label)e.Row.FindControl("lgvdiffqty");


        //        string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grpirsircode")).ToString();

        //        if (code == "")
        //        {
        //            return;
        //        }
        //        if (ASTUtility.Right(code, 12) == "000000000000")
        //        {

        //            resdesc.Font.Bold = true;
        //            Bgdqty.Font.Bold = true;
        //            SubConQty.Font.Bold = true;
        //            Diffqty.Font.Bold = true;


        //        }
        //    }
        //}
        protected void gvSubCon_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSubCon.PageIndex = e.NewPageIndex;
            this.ShowSubConBill();
        }

    }
}

