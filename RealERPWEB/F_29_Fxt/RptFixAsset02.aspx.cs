using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_29_Fxt
{
    public partial class RptFixAsset02 : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetDeparment();
                this.GetAsset();
                this.GetUser();
                this.GetAssetDetails();
                DropDownList1_SelectedIndexChanged(null, null);
                this.txtTodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDateFrom.Text = "01" + (this.txtTodate.Text).Substring(2);

            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void GetAsset()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FIXASSTLIST", "%", "", "", "", "", "", "", "", "");
            DataRow dr1 = ds1.Tables[0].NewRow();
            dr1["comcod"] = comcod.ToString();
            dr1["sircode"] = "%%";
            dr1["sirdesc1"] = "All";
            ds1.Tables[0].Rows.Add(dr1);

            this.ddlasset.DataSource = ds1.Tables[0];
            this.ddlasset.DataTextField = "sirdesc1";
            this.ddlasset.DataValueField = "sircode";
            this.ddlasset.DataBind();
            this.ddlasset.SelectedValue = "%%";

        }


        private void GetAssetDetails()
        {
            string comcod = this.GetComeCode();
            string sercCode;
            string valuess = this.ddlasset.SelectedValue;
            if (valuess == "%%")
            {
                sercCode = "%%";
            }
            else
            {
                sercCode = ASTUtility.Left(this.ddlasset.SelectedValue, 4).ToString() + "%";
            }


            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FIXASSTLIST", sercCode, "", "", "", "", "", "", "", "");
            DataRow dr1 = ds1.Tables[1].NewRow();
            dr1["comcod"] = comcod.ToString();
            dr1["sircode"] = "%%";
            dr1["sirdesc1"] = "All";
            ds1.Tables[1].Rows.Add(dr1);

            this.ddlAssetDetails.DataSource = ds1.Tables[1];
            this.ddlAssetDetails.DataTextField = "sirdesc1";
            this.ddlAssetDetails.DataValueField = "sircode";
            this.ddlAssetDetails.DataBind();


            this.ddlAssetDetails.SelectedValue = "%%";

        }


        private void GetUser()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETEMPTIDNAME", "%", "", "", "", "", "", "", "", "");
            DataRow dr1 = ds1.Tables[0].NewRow();
            dr1["comcod"] = comcod.ToString();
            dr1["empid"] = "%%";
            dr1["empname"] = "All";
            ds1.Tables[0].Rows.Add(dr1);

            ddluser.DataTextField = "empname";
            ddluser.DataValueField = "empid";
            ddluser.DataSource = ds1.Tables[0];
            ddluser.DataBind();
            this.ddluser.SelectedValue = "%%";
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void GetDeparment()
        {
            string comcod = this.GetComeCode();
            // string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FXTASSTGETDEPARTMENT", "%", "", "", "", "", "", "", "", "");
            DataRow dr1 = ds1.Tables[0].NewRow();
            dr1["comcod"] = comcod.ToString();
            dr1["fxtgcod"] = "%%";
            dr1["fxtgdesc"] = "All";
            ds1.Tables[0].Rows.Add(dr1);

            this.ddldeptName.DataTextField = "fxtgdesc";
            this.ddldeptName.DataValueField = "fxtgcod";
            this.ddldeptName.DataSource = ds1.Tables[0];
            this.ddldeptName.DataBind();
            this.ddldeptName.SelectedValue = "%%";
        }


        protected void GetItemType()
        {
            //string comcod = this.GetComeCode();

            //string serchType = this.ddldeptName.SelectedValue.ToString();

            //DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETASSETCAT", serchType, "", "", "", "", "", "", "", "");

            //ds1.Tables[0].Rows.Add(comcod, "000000000000", "All Type");

            //this.ddlProjectName.DataTextField = "sirdesc";
            //this.ddlProjectName.DataValueField = "sircode";
            //this.ddlProjectName.DataSource = ds1.Tables[0];
            //this.ddlProjectName.DataBind();
            //this.ddlProjectName.SelectedValue = "000000000000";

        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();

            string val = DropDownList1.SelectedValue.ToString();
            switch (val)
            {
                case "location":
                    this.gvuser.DataSource = null;
                    this.gvuser.DataBind();
                    this.gvuser.Visible = false;
                    string loct = this.ddldeptName.SelectedValue.ToString();
                    string frmdate = this.txtDateFrom.Text.Trim();
                    string todate = this.txtTodate.Text.Trim();
                    DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSET_INFO", "RPTASSETDEPARTMENT", loct, frmdate, todate, "", "", "", "", "", "");
                    if (ds1 == null)
                        return;
                    Session["tblasset"] = ds1.Tables[0];

                    this.gvFixAsset.DataSource = this.HiddenSameDataDept(ds1.Tables[0]);
                    this.gvFixAsset.DataBind();
                    this.gvFixAsset.Visible = true;


                    ((Label)this.gvFixAsset.FooterRow.FindControl("lgFPurValue")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(pvalu)", "")) ? 0.00 : ds1.Tables[0].Compute("sum(pvalu)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvFixAsset.FooterRow.FindControl("lgFDepciation")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(depreamt)", "")) ? 0.00 : ds1.Tables[0].Compute("sum(depreamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvFixAsset.FooterRow.FindControl("lgFBookVal")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(bookval)", "")) ? 0.00 : ds1.Tables[0].Compute("sum(bookval)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    break;
                case "asset":
                    this.gvuser.DataSource = null;
                    this.gvuser.DataBind();
                    this.gvuser.Visible = false;
                    this.gvFixAsset.DataSource = null;
                    this.gvFixAsset.DataBind();
                    this.gvFixAsset.Visible = false;
                    string asst = this.ddlAssetDetails.SelectedValue.ToString();
                    string valuess = this.ddlasset.SelectedValue;

                    string asstparent = ((valuess == "%%") ? "%%" : ASTUtility.Left(valuess, 4).ToString() + "%");



                    DataSet ds2 = PurData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSET_INFO", "RPTASSETEQUIPWISE", asst, asstparent, "", "", "", "", "", "", "");
                    if (ds2 == null)
                        return;
                    Session["tblasset"] = ds2.Tables[0];
                    this.gvassetwise.DataSource = this.HiddenSameDataAsset(ds2.Tables[0]);
                    this.gvassetwise.DataBind();
                    this.gvassetwise.Visible = true;
                    ((Label)this.gvassetwise.FooterRow.FindControl("lgFPurValue1")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(pvalu)", "")) ? 0.00 : ds2.Tables[0].Compute("sum(pvalu)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvassetwise.FooterRow.FindControl("lgFDepciation1")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(depreamt)", "")) ? 0.00 : ds2.Tables[0].Compute("sum(depreamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvassetwise.FooterRow.FindControl("lgFBookVal1")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(bookval)", "")) ? 0.00 : ds2.Tables[0].Compute("sum(bookval)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    break;
                case "user":
                    this.gvFixAsset.DataSource = null;
                    this.gvFixAsset.DataBind();
                    this.gvFixAsset.Visible = false;
                    this.gvassetwise.DataSource = null;
                    this.gvassetwise.DataBind();
                    this.gvassetwise.Visible = false;

                    string user = this.ddluser.SelectedValue.ToString();
                    DataSet ds3 = PurData.GetTransInfo(comcod, "SP_REPORT_FIXEDASSET_INFO", "RPTASSETUSERWISE", user, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    Session["tblasset"] = ds3.Tables[0];
                    this.gvuser.DataSource = this.HiddenSameDataUser(ds3.Tables[0]);
                    this.gvuser.DataBind();
                    this.gvuser.Visible = true;
                    ((Label)this.gvuser.FooterRow.FindControl("lgFPurValue2")).Text = Convert.ToDouble((Convert.IsDBNull(ds3.Tables[0].Compute("sum(pvalu)", "")) ? 0.00 : ds3.Tables[0].Compute("sum(pvalu)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvuser.FooterRow.FindControl("lgFDepreciation2")).Text = Convert.ToDouble((Convert.IsDBNull(ds3.Tables[0].Compute("sum(depreamt)", "")) ? 0.00 : ds3.Tables[0].Compute("sum(depreamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvuser.FooterRow.FindControl("lgFBookVal2")).Text = Convert.ToDouble((Convert.IsDBNull(ds3.Tables[0].Compute("sum(bookval)", "")) ? 0.00 : ds3.Tables[0].Compute("sum(bookval)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    break;
            }


        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string val = DropDownList1.SelectedValue.ToString();
            switch (val)
            {
                case "location":
                    this.gvuser.Visible = false;
                    this.gvFixAsset.Visible = false;
                    this.gvassetwise.Visible = false;

                    this.divDept.Visible = true;
                    this.lblDept.Visible = true;
                    this.ddldeptName.Visible = true;
                    this.divUser.Visible = false;
                    this.lblusr.Visible = false;
                    this.ddluser.Visible = false;
                    this.divAsset.Visible = false;
                    this.lblasst.Visible = false;
                    this.ddlasset.Visible = false;
                    this.divAssetDetails.Visible = false;
                    this.lblasstdet.Visible = false;
                    this.ddlAssetDetails.Visible = false;
                    break;
                case "asset":
                    this.gvuser.Visible = false;
                    this.gvFixAsset.Visible = false;
                    this.gvassetwise.Visible = false;

                    this.divDept.Visible = false;
                    this.lblDept.Visible = false;
                    this.ddldeptName.Visible = false;
                    this.divUser.Visible = false;
                    this.lblusr.Visible = false;
                    this.ddluser.Visible = false;
                    this.divAsset.Visible = true;
                    this.lblasst.Visible = true;
                    this.ddlasset.Visible = true;
                    this.divAssetDetails.Visible = true;
                    this.lblasstdet.Visible = true;
                    this.ddlAssetDetails.Visible = true;



                    break;
                case "user":
                    this.gvuser.Visible = false;
                    this.gvFixAsset.Visible = false;
                    this.gvassetwise.Visible = false;
                    this.divDept.Visible = false;
                    this.lblDept.Visible = false;
                    this.ddldeptName.Visible = false;
                    this.divUser.Visible = true;
                    this.lblusr.Visible = true;
                    this.ddluser.Visible = true;
                    this.divAsset.Visible = false;
                    this.lblasst.Visible = false;
                    this.ddlasset.Visible = false;
                    this.divAssetDetails.Visible = false;
                    this.lblasstdet.Visible = false;
                    this.ddlAssetDetails.Visible = false;
                    break;
            }
        }
        private DataTable HiddenSameDataAsset(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string sircode = dt1.Rows[0]["rsircatcode"].ToString();
            string advno = dt1.Rows[0]["rsircode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == advno && dt1.Rows[j]["rsircatcode"].ToString() == sircode)
                {
                    sircode = dt1.Rows[j]["rsircatcode"].ToString();
                    advno = dt1.Rows[j]["rsircode"].ToString();

                    dt1.Rows[j]["sirdesc1"] = "";
                    dt1.Rows[j]["assetnam"] = "";
                }
                else
                {

                    if (dt1.Rows[j]["rsircatcode"].ToString() == sircode)
                    {
                        dt1.Rows[j]["sirdesc1"] = "";
                    }

                    if (dt1.Rows[j]["rsircode"].ToString() == advno)
                    {
                        dt1.Rows[j]["assetnam"] = "";

                    }


                    sircode = dt1.Rows[j]["rsircatcode"].ToString();
                    advno = dt1.Rows[j]["rsircode"].ToString();

                }
            }

            return dt1;
        }


        private DataTable HiddenSameDataDept(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string advno = dt1.Rows[0]["pactcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == advno)
                {
                    dt1.Rows[j]["dept"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["pactcode"].ToString() == advno)
                    {
                        dt1.Rows[j]["dept"] = "";

                    }

                    advno = dt1.Rows[j]["pactcode"].ToString();

                }
            }

            return dt1;
        }

        private DataTable HiddenSameDataUser(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }
            string rsircode = dt1.Rows[0]["empid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["empid"].ToString() == rsircode)
                {

                    dt1.Rows[j]["EMPNAME"] = "";

                }

                rsircode = dt1.Rows[j]["empid"].ToString();

            }
            return dt1;

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            DataTable dt1 = (DataTable)Session["tblasset"];


            string txtDate = "As On Date : " + this.txtDateFrom.Text;
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);


            var lst = dt1.DataTableToList<RealEntity.C_29_Fxt.EClassFixedAsset.EClassFixedAssetRegister>();

            LocalReport Rpt1 = new LocalReport();
            string val = DropDownList1.SelectedValue.ToString();
            string txtHeaderType;
            string txtDepartment;
            if (val == "location")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_29_Fxt.RptFixAssetLocWise", lst, null, null);

                txtHeaderType = "Fixed Assets Register Locaction Wise";
                txtDepartment = "Department : " + ddldeptName.SelectedItem.Text.ToString();

            }

            else if (val == "asset")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_29_Fxt.RptFixAssetEquipWise", lst, null, null);

                txtHeaderType = "Fixed Assets Register Equipment Wise";
                txtDepartment = "Asset : " + ddlasset.SelectedItem.Text.ToString();

            }

            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_29_Fxt.RptFixAssetUserWise", lst, null, null);

                txtHeaderType = "Fixed Assets Register User Wise";
                txtDepartment = "User : " + ddluser.SelectedItem.Text.ToString();

            }

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));

            Rpt1.SetParameters(new ReportParameter("rptTitle", txtHeaderType));
            Rpt1.SetParameters(new ReportParameter("txtDepartment", txtDepartment));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtDate));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Fixed Asset ";
                string eventdesc = "Fixed Asset Register Report";
                string eventdesc2 = "";
                bool IsFixedAssetRegisterReportPrint = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }




        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.GetItemType();
        }
        protected void ddlasset_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.GetAssetDetails();
        }
    }
}