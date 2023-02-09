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
namespace RealERPWEB.F_81_Hrm.F_89_Pay
{
    public partial class EmpAllow : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtMonth.Text = System.DateTime.Today.ToString("yyyyMM");
                this.GetDepartment();




            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetDepartment()
        {

            string comcod = this.GetComeCode();
            string txtDepartment = this.txtSrcDept.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtDepartment, "", "", "", "", "", "", "", "");
            this.ddlDeptName.DataTextField = "actdesc";
            this.ddlDeptName.DataValueField = "actcode";
            this.ddlDeptName.DataSource = ds1.Tables[0];
            this.ddlDeptName.DataBind();



        }
        protected void ibtnFindDepartment_Click(object sender, ImageClickEventArgs e)
        {
            this.GetDepartment();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "Ok")
            {
                this.ddlDeptName.Visible = false;
                this.lblDepartment.Visible = true;
                this.lblPage.Visible = true;
                this.ddlpagesize.Visible = true;
                this.lnkbtnShow.Text = "New";
                this.lblDepartment.Text = this.ddlDeptName.SelectedItem.Text;
                this.ShowAllowance();
                return;
            }
            this.ddlDeptName.Visible = true;
            this.lblDepartment.Visible = false;
            this.lblPage.Visible = false;
            this.ddlpagesize.Visible = false;
            this.gvEmpAllow.DataSource = null;
            this.gvEmpAllow.DataBind();
            this.lnkbtnShow.Text = "Ok";
            this.lblDepartment.Text = "";


        }

        private void ShowAllowance()
        {
            Session.Remove("tblallow");
            string comcod = this.GetComeCode();
            string Department = this.ddlDeptName.SelectedValue.ToString();
            string Monthid = this.txtMonth.Text.Trim();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPALLOWANCE", Department, Monthid, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvEmpAllow.DataSource = null;
                this.gvEmpAllow.DataBind();
                return;
            }
            Session["tblallow"] = ds2.Tables[0];
            this.Data_Bind();


        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblallow"];
            int rowindex;

            for (int i = 0; i < this.gvEmpAllow.Rows.Count; i++)
            {

                double Overtime = Convert.ToDouble("0" + ((TextBox)this.gvEmpAllow.Rows[i].FindControl("txtgvovertime")).Text.Trim());
                double Tiffin = Convert.ToDouble("0" + ((TextBox)this.gvEmpAllow.Rows[i].FindControl("txtgvtiffallow")).Text.Trim());
                double holidays = Convert.ToDouble("0" + ((TextBox)this.gvEmpAllow.Rows[i].FindControl("txtgvholidays")).Text.Trim());
                double Othersallow = Convert.ToDouble("0" + ((TextBox)this.gvEmpAllow.Rows[i].FindControl("txtgvothersallow")).Text.Trim());
                double dailyallow = Convert.ToDouble("0" + ((TextBox)this.gvEmpAllow.Rows[i].FindControl("txtgvdailyallow")).Text.Trim());
                double cellphone = Convert.ToDouble("0" + ((TextBox)this.gvEmpAllow.Rows[i].FindControl("txtgvcellphone")).Text.Trim());
                double cellbill = Convert.ToDouble("0" + ((TextBox)this.gvEmpAllow.Rows[i].FindControl("txtgvcellbill")).Text.Trim());
                double a = Convert.ToDouble("0" + ((TextBox)this.gvEmpAllow.Rows[i].FindControl("txtgva")).Text.Trim());
                double b = Convert.ToDouble("0" + ((TextBox)this.gvEmpAllow.Rows[i].FindControl("txtgvb")).Text.Trim());
                double toamt = Overtime + Tiffin + holidays + Othersallow + dailyallow + cellphone + cellbill + a + b;
                rowindex = (this.gvEmpAllow.PageSize) * (this.gvEmpAllow.PageIndex) + i;
                dt.Rows[rowindex]["txt04021"] = Overtime;
                dt.Rows[rowindex]["txt04022"] = Tiffin;
                dt.Rows[rowindex]["txt04023"] = holidays;
                dt.Rows[rowindex]["txt04024"] = Othersallow;
                dt.Rows[rowindex]["txt04025"] = dailyallow;
                dt.Rows[rowindex]["txt04026"] = cellphone;
                dt.Rows[rowindex]["txt04027"] = cellbill;
                dt.Rows[rowindex]["txt04028"] = a;
                dt.Rows[rowindex]["txt04029"] = b;
                dt.Rows[rowindex]["toamt"] = toamt;

            }
            Session["tblallow"] = dt;
            this.Data_Bind();
        }



        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblallow"];
            this.gvEmpAllow.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvEmpAllow.DataSource = dt;
            this.gvEmpAllow.DataBind();
            this.FooterCalculation();

        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblallow"];
            ((Label)this.gvEmpAllow.FooterRow.FindControl("lgvFOvertime")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(txt04021)", "")) ? 0.00 : dt.Compute("sum(txt04021)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpAllow.FooterRow.FindControl("lgvFtiffallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(txt04022)", "")) ? 0.00 : dt.Compute("sum(txt04022)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpAllow.FooterRow.FindControl("lgvFholidays")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(txt04023)", "")) ? 0.00 : dt.Compute("sum(txt04023)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpAllow.FooterRow.FindControl("lgvFothersallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(txt04024)", "")) ? 0.00 : dt.Compute("sum(txt04024)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpAllow.FooterRow.FindControl("lgvFdailyallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(txt04025)", "")) ? 0.00 : dt.Compute("sum(txt04025)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpAllow.FooterRow.FindControl("lgvFcellphone")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(txt04026)", "")) ? 0.00 : dt.Compute("sum(txt04026)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpAllow.FooterRow.FindControl("lgvFcellbill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(txt04027)", "")) ? 0.00 : dt.Compute("sum(txt04027)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpAllow.FooterRow.FindControl("lgvFa")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(txt04028)", "")) ? 0.00 : dt.Compute("sum(txt04028)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpAllow.FooterRow.FindControl("lgvFb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(txt04029)", "")) ? 0.00 : dt.Compute("sum(txt04029)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpAllow.FooterRow.FindControl("lgvFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toamt)", "")) ? 0.00 : dt.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        protected void gvEmpAllow_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvEmpAllow.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }



        protected void lTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }
        protected void lUpdate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblallow"];
            string comcod = this.GetComeCode();
            string monthid = this.txtMonth.Text.Trim();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                double overtime = Convert.ToDouble(dt.Rows[i]["txt04021"]);
                double Tiffin = Convert.ToDouble(dt.Rows[i]["txt04022"]);
                double holidays = Convert.ToDouble(dt.Rows[i]["txt04023"]);
                double Othersallow = Convert.ToDouble(dt.Rows[i]["txt04024"]);
                double dailyallow = Convert.ToDouble(dt.Rows[i]["txt04025"]);
                double cellphone = Convert.ToDouble(dt.Rows[i]["txt04026"]);
                double cellbill = Convert.ToDouble(dt.Rows[i]["txt04027"]);
                double a = Convert.ToDouble(dt.Rows[i]["txt04028"]);
                double b = Convert.ToDouble(dt.Rows[i]["txt04029"]);
                if (overtime > 0 || Tiffin > 0 || holidays > 0 || Othersallow > 0 || dailyallow > 0 || cellphone > 0 || cellbill > 0 || a > 0 || b > 0)
                {

                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORDELETEALLOW", monthid, empid, overtime.ToString(), Tiffin.ToString(), holidays.ToString(), Othersallow.ToString(), dailyallow.ToString(), cellphone.ToString(), cellbill.ToString(), a.ToString(), b.ToString(), "", "", "", "");
                    if (!result)
                        return;
                }
            }

         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }


    }
}