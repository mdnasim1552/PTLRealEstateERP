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
namespace RealERPWEB.F_22_Sal
{
    public partial class MktEntryUnitFH : System.Web.UI.Page
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

                this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //this.gvUnit.Columns[1].Visible = (Convert.ToBoolean(dr1[0]["entry"]));

                this.GetProjectName();
            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string txtSProject = this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETPROJECTNAMEHF", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }




        protected void ibtnFindProject_Click(object sender, ImageClickEventArgs e)
        {
            if (this.lbtnOk.Text == "New")
                return;

            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            if (this.lbtnOk.Text == "Ok")
            {

                this.PanelGroup.Visible = true;
                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text;
                this.lblProjectmDesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.ddlProjectName.Visible = false;
                this.lblProjectmDesc.Visible = true;
                this.lblProjectdesc.Visible = true;
                //this.ibtnFindProject.Enabled = false;
                this.LoadGrid();


            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.PanelGroup.Visible = false;
                this.ClearScreen();
                this.txtResDesc.Text = "";
                this.chkAllSInf.Checked = false;
            }
        }

        private void ClearScreen()
        {
            this.ddlProjectName.Visible = true;
            //this.ibtnFindProject.Enabled = true;
            this.lblProjectmDesc.Text = "";
            this.lblProjectmDesc.Visible = false;
            this.lblProjectdesc.Text = "";
            this.lblProjectdesc.Visible = false;
            this.gvUnit.DataSource = null;
            this.gvUnit.DataBind();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";


        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_bind();
        }


        private void SaveValue()
        {

            int rowindex;
            DataTable tblt02 = (DataTable)ViewState["tblUnit"];
            for (int i = 0; i < this.gvUnit.Rows.Count; i++)
            {
                string UsirCode = ((Label)this.gvUnit.Rows[i].FindControl("lblgvItmCod")).Text.Trim();
                //if (ASTUtility.Right(UsirCode, 3) == "000")
                //    continue;
                string udesc = ((TextBox)this.gvUnit.Rows[i].FindControl("txtItemdesc")).Text.Trim();
                string munit = ((TextBox)this.gvUnit.Rows[i].FindControl("txtgUnitnum")).Text.Trim();

                double usize = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvUSize")).Text.Trim());
                double urate = Convert.ToDouble('0' + ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvRate")).Text.Trim());
                double uamt = usize * urate;
                string Remarsk = ((TextBox)this.gvUnit.Rows[i].FindControl("txtgvRemarks")).Text.Trim();

                rowindex = (this.gvUnit.PageSize * this.gvUnit.PageIndex) + i;
                tblt02.Rows[rowindex]["udesc"] = udesc;
                tblt02.Rows[rowindex]["usize"] = usize;
                tblt02.Rows[rowindex]["urate"] = urate;
                tblt02.Rows[rowindex]["uamt"] = uamt;
                tblt02.Rows[rowindex]["urmrks"] = Remarsk;
                tblt02.Rows[rowindex]["munit"] = munit;
            }
            ViewState["tblUnit"] = tblt02;


        }

        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}
            this.SaveValue();
            DataTable dt = (DataTable)ViewState["tblUnit"];

            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string UsirCode = dt.Rows[i]["usircode"].ToString();
                //if (ASTUtility.Right(UsirCode, 3) == "000")
                //    continue;
                string Udesc = dt.Rows[i]["udesc"].ToString();
                string munit = dt.Rows[i]["munit"].ToString();
                double usize = Convert.ToDouble(dt.Rows[i]["usize"].ToString());
                double urate = Convert.ToDouble(dt.Rows[i]["urate"].ToString());
                string Uramrks = dt.Rows[i]["urmrks"].ToString();
                double uamt = usize * urate;
                if (uamt > 0)
                {
                    MktData.UpdateTransInfo2(comcod, "SP_ENTRY_SALSMGT02", "INSERTUPDATE_HFSALINF", PactCode, UsirCode, munit, usize.ToString(), Udesc, Uramrks, uamt.ToString(),
                            "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                }

            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            this.LoadGrid();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Unit Fixation";
                string eventdesc = "Update Fixation";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13);
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        private void LoadGrid()
        {

            ViewState.Remove("tblUnit");

            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string usirdesc = "%" + this.txtResDesc.Text + "%";
            DataSet ds4 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "SIRINFINFORHF", PactCode, usirdesc, "", "", "", "", "", "", "");
            if (ds4 == null)
                return;
            ViewState["tblUnit"] = ds4.Tables[0];
            this.Data_bind();

        }

        private void Data_bind()
        {
            DataTable tblt05 = (DataTable)ViewState["tblUnit"];
            this.gvUnit.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvUnit.DataSource = tblt05;
            this.gvUnit.DataBind();

            if (tblt05.Rows.Count == 0)
                return;
            ((Label)this.gvUnit.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(uamt)", "")) ?
              0.00 : tblt05.Compute("Sum(uamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvUnit.FooterRow.FindControl("lFUsize")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(usize)", "")) ?
              0.00 : tblt05.Compute("Sum(usize)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //ReportDocument rptstk = new ASITfarmRPT.R_06_Sal.rptUnitFxInf();
            //DataTable dt1 = (DataTable)ViewState["tblUnit"];
            //DataView dv1 = dt1.DefaultView;
            //dv1.RowFilter = "uamt>0";
            //rptstk.SetDataSource(dv1);

            //TextObject txtCompname = rptstk.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            //txtCompname.Text = comnam;
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["ProjectName"] as TextObject;
            //txtprojectname.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Unit Fixation";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13);
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //dv1.RowFilter = "";

        }
        protected void chkAllSInf_CheckedChanged(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds3 = new DataSet();
            DataTable dt1 = new DataTable();
            string usirdesc = "%" + this.txtResDesc.Text + "%";
            if (this.chkAllSInf.Checked)
            {

                ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "SIRINFHFUNITINFO", PactCode, usirdesc, "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
            }

            else
            {
                ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "SIRINFINFORHF", PactCode, usirdesc, "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                //this.gvUnit.Columns[1].Visible = true;


            }
            ViewState["tblUnit"] = ds3.Tables[0];
            this.Data_bind();

        }


        protected void gvUnit_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string UsirCode = ((Label)this.gvUnit.Rows[e.RowIndex].FindControl("lblgvItmCod")).Text.Trim();

            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "DELETEHFUNIT", pactcode, UsirCode, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                this.LoadGrid();

            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Unit Fixation";
                string eventdesc = "Delete Fixation";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13);
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void gvUnit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    TextBox txt01 = (TextBox)e.Row.FindControl("txtItemdesc");
            //    TextBox txt02 = (TextBox)e.Row.FindControl("txtgUnitnum");
            //    TextBox txt03 = (TextBox)e.Row.FindControl("txtgvUSize");
            //    TextBox txt04 = (TextBox)e.Row.FindControl("txtgvUqty");
            //    TextBox txt05 = (TextBox)e.Row.FindControl("txtgvRate");
            //    TextBox txt06 = (TextBox)e.Row.FindControl("txtgvbstat");
            //    TextBox txt07 = (TextBox)e.Row.FindControl("txtgvRemarks");
            //    TextBox txt08 = (TextBox)e.Row.FindControl("txtgvPqty");
            //    TextBox txt09 = (TextBox)e.Row.FindControl("txtgvPamt");


            //    string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();

            //    if (code == "")
            //    {
            //        return;
            //    }
            //    if (code.Substring(9, 3) == "000")
            //    {

            //        txt01.ReadOnly = true;
            //        txt02.ReadOnly = true;
            //        txt03.ReadOnly = true;
            //        txt04.ReadOnly = true;
            //        txt05.ReadOnly = true;
            //        txt06.ReadOnly = true;
            //        txt07.ReadOnly = true;

            //    }

            //}
        }


        protected void gvUnit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            gvUnit.PageIndex = e.NewPageIndex;
            this.Data_bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_bind();
        }
        protected void ibtnTotal_Click(object sender, ImageClickEventArgs e)
        {
            this.SaveValue();
            this.Data_bind();
        }
        protected void ibtnResDesc_Click(object sender, ImageClickEventArgs e)
        {
            //this.LoadGrid();
            this.chkAllSInf_CheckedChanged(null, null);
        }
    }
}
