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
namespace RealERPWEB.F_23_CR
{
    public partial class EntryPrjCollSumAdj : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Project wise Summary of Collection (Adjustment)";

                //string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtfrmdate.Text = "01-" + ASTUtility.Right(date, 8);
                //this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");


                this.GetName();
                this.GetYearMonth();
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

        private void GetYearMonth()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = CustData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];

            this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            this.ddlyearmon.DataBind();
            ds1.Dispose();
        }
        private void GetName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT04", "GETPRJNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "gdesc";
            this.ddlProjectName.DataValueField = "gcod";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();

        }



        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetName();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {






        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string monthid = this.ddlyearmon.SelectedValue.ToString();
            string ProjectCode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT04", "RPTSUMMARYOFCOLLTRO", monthid, "", ProjectCode, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvprjstatus.DataSource = null;
                this.gvprjstatus.DataBind();
                return;
            }

            Session["tblPrjstatus"] = ds2.Tables[0];  // this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }







        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblPrjstatus"];
            this.gvprjstatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvprjstatus.DataSource = dt;
            this.gvprjstatus.DataBind();
            this.GridColoumnVisible();
            this.FooterCalculation();

        }


        private void GridColoumnVisible()
        {

            DataTable tbl1 = (DataTable)Session["tblPrjstatus"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvprjstatus.Rows.Count; j++)
            {

                TblRowIndex2 = (this.gvprjstatus.PageIndex) * this.gvprjstatus.PageSize + j;
                string prjadjcode = tbl1.Rows[TblRowIndex2]["prjadjcode"].ToString();
                if (prjadjcode != "78099")
                    ((TextBox)this.gvprjstatus.Rows[j].FindControl("txtgvmramt")).ReadOnly = true;
                //((TextBox)this.dgv2.Rows[j].FindControl("txtgvDrAmt")).ReadOnly = true;
            }
        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblPrjstatus"];
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvprjstatus.FooterRow.FindControl("lgvFmramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(mramt)", "")) ?
                        0.00 : dt.Compute("Sum(mramt)", ""))).ToString("#,##0;(#,##0); ");






        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();

        }

        protected void lnkFiUpdate_Click(object sender, EventArgs e)
        {
            this.Master.FindControl("lblmsg").Visible = true;
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblPrjstatus"];
            string comcod = this.GetCompCode();


            string adjdate = this.ddlyearmon.SelectedValue.ToString();


            foreach (DataRow dr in dt.Rows)
            {
                string prjadjcode = dr["prjadjcode"].ToString();
                string prjcode = dr["prjcode"].ToString();
                string mramt = dr["mramt"].ToString();



                if (prjadjcode == "78099")
                {
                    bool result = CustData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT04", "INSERTORUPDATECOLLSUMM", adjdate, prjcode, prjadjcode, mramt, "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = CustData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                }
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }

        private void SaveValue()
        {

            string comcod = this.GetCompCode();

            DataTable dt = (DataTable)Session["tblPrjstatus"];
            int rowindex;

            for (int i = 0; i < this.gvprjstatus.Rows.Count; i++)
            {

                double mramt = ASTUtility.StrPosOrNagative(((TextBox)this.gvprjstatus.Rows[i].FindControl("txtgvmramt")).Text.Trim());
                string prjcode = ((Label)this.gvprjstatus.Rows[i].FindControl("lgvPrjcode")).Text.Trim();
                rowindex = (this.gvprjstatus.PageSize) * (this.gvprjstatus.PageIndex) + i;

                dt.Rows[rowindex]["mramt"] = mramt;



            }


            Session["tblPrjstatus"] = dt;

        }

        protected void gvprjstatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvprjstatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void btnAdjTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

    }
}