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
namespace RealERPWEB.F_45_GrAcc
{
    public partial class LinkGrpMatPurHistory : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string type = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "PurHisMatWise") ? "PURCHASE HISTORY - MATERIAL WISE" : "PURCHASE HISTORY -SUPPLIER WISE ";

                this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";

                this.GetProjectName();
                this.SelectView();


            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PurHisMatWise":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.GetMaterial();
                    break;

                case "PurHisSupWise":
                    this.lblMaterials.Visible = false;
                    this.txtSrcMat.Visible = false;
                    this.ImgBtnMatName.Visible = false;
                    this.ddlMaterialName.Visible = false;
                    this.MultiView1.ActiveViewIndex = 1;
                    this.GetSupplier();
                    break;

            }




        }

        private string GetComeCode()
        {
            return (this.Request.QueryString["comcod"].ToString());
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
            this.GetSupplier();
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

        private void GetSupplier()
        {
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSrchSupplier = this.txtSrcSupplier.Text.Trim() + "%";
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETSUPPLIER", pactcode, txtSrchSupplier, "", "", "", "", "", "", "");
            this.ddlSupplier.DataTextField = "ssirdesc";
            this.ddlSupplier.DataValueField = "ssircode";
            this.ddlSupplier.DataSource = ds2.Tables[0];
            this.ddlSupplier.DataBind();

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
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["MatPurHis"];

            ReportDocument rptstate = new RealERPRPT.R_14_Pro.RptMatPurHistory();
            TextObject rpttxtMaterial = rptstate.ReportDefinition.ReportObjects["txtMaterial"] as TextObject;
            rpttxtMaterial.Text = this.ddlMaterialName.SelectedItem.Text.Trim().Substring(14) + "         " + this.lblUnit.Text.Trim();
            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            rptftdate.Text = this.lblAsDate.Text.Trim();
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource(dt);

            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PurHisMatWise":
                    this.ShowPurhisMatWise();

                    break;

                case "PurHisSupWise":
                    this.ShowPurhisSupWise();

                    break;

            }


        }

        private void ShowPurhisMatWise()
        {
            Session.Remove("MatPurHis");
            string comcod = this.GetComeCode();
            string proname = ddlProjectName.SelectedValue.Substring(0, 12).ToString();
            string matname = this.ddlMaterialName.SelectedValue.Substring(0, 12).ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTMATPURHISTORY", proname, matname, frmdate, todate, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMatPurHis.DataSource = null;
                this.gvMatPurHis.DataBind();
                return;
            }

            this.gvMatPurHis.Columns[3].Visible = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? true : false;
            this.lblUnit.Text = ds1.Tables[1].Rows[0]["sirunit"].ToString();
            Session["MatPurHis"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();


        }

        private void ShowPurhisSupWise()
        {

            Session.Remove("MatPurHis");
            string comcod = this.GetComeCode();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string supplier = this.ddlSupplier.SelectedValue.ToString();
            string mrfno = "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTINDSUPINFO", frmdate, todate, pactcode, supplier, mrfno, "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurStatus.DataSource = null;
                this.gvPurStatus.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["MatPurHis"] = dt;
            this.Data_Bind();

        }

        private void Data_Bind()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PurHisMatWise":
                    this.gvMatPurHis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvMatPurHis.DataSource = (DataTable)Session["MatPurHis"];
                    this.gvMatPurHis.DataBind();
                    this.FooterCalculation();

                    break;

                case "PurHisSupWise":
                    this.gvPurStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvPurStatus.DataSource = (DataTable)Session["MatPurHis"];
                    this.gvPurStatus.DataBind();
                    break;

            }


        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["MatPurHis"];
            if (dt.Rows.Count == 0)
                return;

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PurHisMatWise":
                    ((Label)this.gvMatPurHis.FooterRow.FindControl("lgvMRRQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrrqty)", "")) ?
                                 0 : dt.Compute("sum(mrrqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvMatPurHis.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrramt)", "")) ?
                                0 : dt.Compute("sum(mrramt)", ""))).ToString("#,##0;(#,##0); ");

                    break;

                case "PurHisSupWise":
                    ((Label)this.gvPurStatus.FooterRow.FindControl("lgvFAmtsup")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                        0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");

                    break;

            }




        }
        private DataTable HiddenSameData(DataTable dt1)
        {



            if (dt1.Rows.Count == 0)
                return dt1;
            string mrrno = "";
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PurHisMatWise":
                    mrrno = dt1.Rows[0]["mrrno"].ToString();

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

                    break;

                case "PurHisSupWise":


                    string pactcode = dt1.Rows[0]["pactcode"].ToString();
                    mrrno = dt1.Rows[0]["mrrno"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["mrrno"].ToString() == mrrno)
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            mrrno = dt1.Rows[j]["mrrno"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["mrrno1"] = "";
                        }

                        else
                        {

                            if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                            {
                                dt1.Rows[j]["pactdesc"] = "";
                            }

                            if (dt1.Rows[j]["mrrno"].ToString() == mrrno)
                            {
                                dt1.Rows[j]["mrrno1"] = "";
                            }
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            mrrno = dt1.Rows[j]["mrrno"].ToString();

                        }

                    }

                    break;

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
        protected void gvPurStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPurStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
    }
}