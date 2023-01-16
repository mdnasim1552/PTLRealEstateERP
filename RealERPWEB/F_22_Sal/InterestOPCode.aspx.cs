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
    public partial class InterestOPCode : System.Web.UI.Page
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
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "GETTRADINGCODE", txtSProject, "", "", "", "", "", "", "", "");
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


                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text;
                this.ddlProjectName.Visible = false;
                this.lblProjectdesc.Visible = true;
                //this.ibtnFindProject.Enabled = false;
                this.LoadGrid();


            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.ClearScreen();
            }
        }

        private void ClearScreen()
        {
            this.ddlProjectName.Visible = true;
            this.lblProjectdesc.Text = "";
            this.lblProjectdesc.Visible = false;
            this.gvOPInt.DataSource = null;
            this.gvOPInt.DataBind();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";


        }

        private void SaveValue()
        {

            int rowindex;
            DataTable tblt02 = (DataTable)ViewState["tbOpInt"];
            for (int i = 0; i < this.gvOPInt.Rows.Count; i++)
            {
                string rescode = ((Label)this.gvOPInt.Rows[i].FindControl("lblgvCod")).Text.Trim();
                string resdesc = ((TextBox)this.gvOPInt.Rows[i].FindControl("txtIResdesc")).Text.Trim();
                double priamt = Convert.ToDouble('0' + ((TextBox)this.gvOPInt.Rows[i].FindControl("txtgvPri")).Text.Trim());
                double intamt = Convert.ToDouble('0' + ((TextBox)this.gvOPInt.Rows[i].FindControl("txtgvIamt")).Text.Trim());
                double oheadamt = Convert.ToDouble('0' + ((TextBox)this.gvOPInt.Rows[i].FindControl("txtgvOvAmt")).Text.Trim());

                rowindex = (this.gvOPInt.PageSize * this.gvOPInt.PageIndex) + i;
                tblt02.Rows[rowindex]["resdesc"] = resdesc;
                tblt02.Rows[rowindex]["priamt"] = priamt;
                tblt02.Rows[rowindex]["intamt"] = intamt;
                tblt02.Rows[rowindex]["oheadamt"] = oheadamt;


            }
            ViewState["tbOpInt"] = tblt02;


        }

        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.SaveValue();
            DataTable dt = (DataTable)ViewState["tbOpInt"];

            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string rescode = dt.Rows[i]["rescode"].ToString();
                double priamt = Convert.ToDouble(dt.Rows[i]["priamt"].ToString());
                double intamt = Convert.ToDouble(dt.Rows[i]["intamt"].ToString());
                string oheadamt = Convert.ToDouble(dt.Rows[i]["oheadamt"].ToString()).ToString();

                if (priamt > 0)
                {
                    MktData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_04", "UPDATEOPINTEREST", PactCode, rescode, priamt.ToString(), intamt.ToString(), oheadamt.ToString(), "",
                        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                }


            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            this.LoadGrid();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Opening Interest";
                string eventdesc = "Opening Interest";
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13);
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        private void LoadGrid()
        {

            ViewState.Remove("tbOpInt");

            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();

            DataSet ds4 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "SHOWINTINFO", PactCode, "", "", "", "", "", "", "", "");
            if (ds4 == null)
                return;
            ViewState["tbOpInt"] = ds4.Tables[0];
            this.Data_bind();

        }

        private void Data_bind()
        {
            DataTable tblt05 = (DataTable)ViewState["tbOpInt"];
            //this.gvUnit.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvOPInt.DataSource = tblt05;
            this.gvOPInt.DataBind();
            if (tblt05.Rows.Count == 0)
                return;
            ((Label)this.gvOPInt.FooterRow.FindControl("lFPri")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(priamt)", "")) ?
              0.00 : tblt05.Compute("Sum(priamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvOPInt.FooterRow.FindControl("lgvFIAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(intamt)", "")) ?
              0.00 : tblt05.Compute("Sum(intamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOPInt.FooterRow.FindControl("lgvFOAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt05.Compute("Sum(oheadamt)", "")) ?
              0.00 : tblt05.Compute("Sum(oheadamt)", ""))).ToString("#,##0;(#,##0); ");

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

            //ReportDocument rptstk = new RealERPRPT.R_22_Sal.rptUnitFxInf();
            //DataTable dt1 = (DataTable)ViewState["tbOpInt"];
            //DataView dv1 = dt1.DefaultView;
            //dv1.RowFilter = "uamt>0";
            //rptstk.SetDataSource(dv1);


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


        protected void gvOPInt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.SaveValue();
            gvOPInt.PageIndex = e.NewPageIndex;
            this.Data_bind();
        }
        protected void gvOPInt_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //    string comcod = this.GetCompCode();
            //    string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //    string UsirCode = ((Label)this.gvOPInt.Rows[e.RowIndex].FindControl("lblgvItmCod")).Text.Trim();

            //    bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "DELETEUNITENTRY", pactcode, UsirCode, "", "", "", "", "", "", "", "", "", "", "", "", "");

            //    if (result == true)
            //    {
            //        this.LoadGrid();

            //    }

            //    if (ConstantInfo.LogStatus == true)
            //    {
            //        string eventtype = "Unit Fixation";
            //        string eventdesc = "Delete Fixation";
            //        string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString().Substring(13);
            //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //    }
        }
    }
}
