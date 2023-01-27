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
namespace RealERPWEB.F_01_LPA
{
    public partial class LandBgdPrjAna : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        public static double ToCost = 0, ToSalrate = 0, conarea = 0, ToSamt = 0, Tosalsize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.ProjectName();
                this.lblHeader.Text = (Request.QueryString["Type"].ToString().Trim() == "LandAnly") ? "Project Feasibility Project Information" : "";


            }
        }


        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void ProjectName()
        {
            if (this.lbtnOk.Text == "New")
                return;
            string comcod = this.GetComCode();
            string Filter1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY_BGD", "GETLDPPROJECTLIST", Filter1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "infdesc";
            this.ddlProjectName.DataValueField = "infcod";
            ViewState["ConSFT"] = ds1.Tables[0];
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

            //DataRow dr[]=select
        }

        protected void ibtnFindProject_Click(object sender, ImageClickEventArgs e)
        {
            this.ProjectName();
        }
        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
                this.ddlProjectName.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.PnlResInput.Visible = true;


                this.Load_Grid();
                return;
            }


            this.lbtnOk.Text = "Ok";
            this.gvResInfo.DataSource = null;
            this.gvResInfo.DataBind();
            this.PnlResInput.Visible = false;
            this.ddlProjectName.Visible = true;
            this.lblProjectdesc.Visible = false;
            this.chkAllRes.Checked = false;
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comname = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string prjname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            //ReportDocument rpcp = new RealERPRPT.R_02_Fea.RptReviseFea();

            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            //CompName.Text = comname;
            //TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //txtPrjName.Text = prjname;

            //if (ASTUtility.Left(comcod, 1) != "2")
            //{
            //    TextObject txtLand = rpcp.ReportDefinition.ReportObjects["txtLand"] as TextObject;
            //    txtLand.Text = " Land Owner Share:  " + this.lblLownerval.Text;
            //    TextObject txtComp = rpcp.ReportDefinition.ReportObjects["txtComp"] as TextObject;
            //    txtComp.Text = "Company Share:  " + this.lblCompanyval.Text;

            //    TextObject txtDate = rpcp.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //    txtDate.Text = "Date :" + System.DateTime.Now.ToString("dd-MMM-yyyy");
            //}



            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            //    if (ConstantInfo.LogStatus == true)
            //    {
            //        string eventtype = this.lblHeader.Text;
            //        string eventdesc = "Print Report";
            //        string eventdesc2 = "";
            //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //    }

            //    rpcp.SetDataSource((DataTable)ViewState["tblfeaprj"]);
            //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //    rpcp.SetParameterValue("ComLogo", ComLogo);
            //    Session["Report1"] = rpcp;

            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>"; 
        }
        // protected void Load_Grid(object sender, EventArgs e)
        protected void Load_Grid()
        {
            string comcod = this.GetComCode();

            string qtype = this.Request.QueryString["Type"];
            switch (qtype)
            {
                case "LandAnly":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
            }

        }

        private void Data_Bind()
        {
            string comcod = this.GetComCode();
            string qtype = this.Request.QueryString["Type"];
            DataTable dt = (DataTable)ViewState["tblLandAnly"];
            switch (qtype)
            {
                case "LandAnly":
                    DataTable tbl1 = (DataTable)Session["tblLandAnly"];
                    this.gvResInfo.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvResInfo.DataSource = tbl1;
                    this.gvResInfo.DataBind();

                    if (tbl1.Rows.Count > 0)
                        ((Label)this.gvResInfo.FooterRow.FindControl("lblgvTResAmtFooter")).Text =
                            Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("sum(amount)", "")) ? 0.00 : tbl1.Compute("sum(amount)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    break;
            }
        }


        protected void lbtnSelectFloorRes_Click(object sender, EventArgs e)
        {
            this.CallResourceRata();
        }
        protected void CallResourceRata()
        {
            string comcod = this.GetComCode();
            string ProjCod = this.ddlProjectName.SelectedValue.ToString();
            string SearchItem = this.txtSearchItem.Text.Trim() + "%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY_BGD", "GETERSDATA", ProjCod, SearchItem, "", "", "", "", "", "", "");
            Session["tblLandAnly"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }
        protected void CallAllResourceRata()
        {

            string comcod = this.GetComCode();
            string ProjCod = this.ddlProjectName.SelectedValue.ToString();
            string SearchItem = this.txtSearchItem.Text.Trim() + "%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY_BGD", "GETERSDATAALL", ProjCod, SearchItem, "", "", "", "", "", "", "");
            Session["tblLandAnly"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string isircode = dt1.Rows[0]["isircode"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "LandAnly":


                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["isircode"].ToString() == isircode)
                        {
                            isircode = dt1.Rows[j]["isircode"].ToString();
                            dt1.Rows[j]["isirdesc"] = "";
                        }
                        isircode = dt1.Rows[j]["isircode"].ToString();


                    }

                    break;
            }

            return dt1;


        }
        protected void lbtnUpdateResRate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                return;
            }
            this.UpdateSessionResource01();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PrjCod = this.ddlProjectName.SelectedValue.ToString().Trim();
            DataTable tbl1 = (DataTable)Session["tblLandAnly"];

            bool result = false;

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string isircode = tbl1.Rows[i]["isircode"].ToString();
                string rescode = tbl1.Rows[i]["rescode"].ToString().Trim();
                string qty = "0" + tbl1.Rows[i]["qty"].ToString().Trim();
                double amount = Convert.ToDouble("0" + tbl1.Rows[i]["amount"]);
                if (amount > 0)
                    result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY_BGD", "INSERUPDATEBGDRES", PrjCod, isircode, rescode, qty, amount.ToString(), "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
             ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Land Budget";
                string eventdesc = "Resource rate Input & Report";
                string eventdesc2 = "Update rate";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        //protected void lbtnSameValue_Click(object sender, EventArgs e)
        //{
        //    this.UpdateSessionResource();
        //    this.Data_Bind();
        //}
        //protected void UpdateSessionResource()
        //{
        //    DataTable tbl1 = (DataTable)Session["tblLandAnly"];
        //    string Rescode = "";
        //    double ResQty = 0;
        //    double ResRat = 0;
        //    int RowIndex = 0;
        //    for (int i = 0; i < this.gvResInfo.Rows.Count; i++)
        //    {

        //        if (i == 0)
        //        {
        //            Rescode = ((Label)this.gvResInfo.Rows[i].FindControl("lblgvResCod")).Text.Trim();
        //            ResRat = Convert.ToDouble("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvResRat")).Text.Trim());
        //        }

        //        ResQty = Convert.ToDouble("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvResQty")).Text.Trim());
        //        if (Rescode == ((Label)this.gvResInfo.Rows[i].FindControl("lblgvResCod")).Text.Trim())
        //        {
        //            Rescode = ((Label)this.gvResInfo.Rows[i].FindControl("lblgvResCod")).Text.Trim();
        //            RowIndex = this.gvResInfo.PageIndex * this.gvResInfo.PageSize + i;
        //            tbl1.Rows[RowIndex]["qty"] = ResQty;
        //            tbl1.Rows[RowIndex]["rate"] = ResRat;
        //            tbl1.Rows[RowIndex]["amount"] = ResQty * ResRat;
        //        }

        //        else
        //        {
        //            Rescode = ((Label)this.gvResInfo.Rows[i].FindControl("lblgvResCod")).Text.Trim();
        //            ResRat = Convert.ToDouble("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvResRat")).Text.Trim());
        //            RowIndex = this.gvResInfo.PageIndex * this.gvResInfo.PageSize + i;
        //            tbl1.Rows[RowIndex]["qty"] = ResQty;
        //            tbl1.Rows[RowIndex]["rate"] = ResRat;
        //            tbl1.Rows[RowIndex]["amount"] = ResQty * ResRat;
        //        }
        //    }
        //    Session["tblLandAnly"] = tbl1;

        //}
        protected void lbtnResTotal_Click(object sender, EventArgs e)
        {
            this.UpdateSessionResource01();
            this.Data_Bind();
        }
        private void UpdateSessionResource01()
        {

            DataTable tbl1 = (DataTable)Session["tblLandAnly"];
            for (int i = 0; i < this.gvResInfo.Rows.Count; i++)
            {
                double ResQty = Convert.ToDouble("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvResQty")).Text.Trim());
                double ResRat = Convert.ToDouble("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvResRat")).Text.Trim());
                int RowIndex = this.gvResInfo.PageIndex * this.gvResInfo.PageSize + i;
                tbl1.Rows[RowIndex]["qty"] = ResQty;
                tbl1.Rows[RowIndex]["rate"] = ResRat;
                tbl1.Rows[RowIndex]["amount"] = ResQty * ResRat;
            }
            Session["tblLandAnly"] = tbl1;


        }
        protected void gvResInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvResInfo.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void chkAllSInf_CheckedChanged(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            if (this.chkAllRes.Checked)
            {

                this.CallAllResourceRata();

            }

            else
            {

                this.CallResourceRata();
            }
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
    }
}