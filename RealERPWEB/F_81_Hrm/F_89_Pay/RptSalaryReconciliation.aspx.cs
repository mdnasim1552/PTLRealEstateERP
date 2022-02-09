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
using RealERPLIB;

namespace RealERPWEB.F_81_Hrm.F_89_Pay
{
    public partial class RptSalaryReconciliation : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.GetCompanyName();
                this.GetMonth();
                ((Label)this.Master.FindControl("lblTitle")).Text = "SALARY RECONCILIATION";

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


        private void GetMonth()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "GETMONTHFORABS", "", "", "", "", "", "", "", "", "");
            if (ds1==null)
                return;

            this.ddlMonth.DataTextField = "mnam";
            this.ddlMonth.DataValueField = "mno";
            this.ddlMonth.DataSource = ds1.Tables[0];
            this.ddlMonth.DataBind();
            this.ddlMonth.SelectedValue = System.DateTime.Today.Month.ToString().Trim();
        }
        private void GetCompanyName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string txtCompany = "%%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            if (ds2==null)
                return;

            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds2.Tables[0];
            this.ddlCompany.DataBind();
            this.GetBranch();
            this.ddlCompany_SelectedIndexChanged(null, null);
        }
        private void GetBranch()
        {
            string comcod = this.GetCompCode();
            string Company = ((this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string txtSProject = "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEPTNAMENEW", Company, txtSProject, "", "", "", "", "", "", "");
            if (ds3==null)
                return;

            this.ddlBranch.DataTextField = "deptdesc";
            this.ddlBranch.DataValueField = "deptcode";
            this.ddlBranch.DataSource = ds3.Tables[0];
            this.ddlBranch.DataBind();
        }
        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetBranch();
        }

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string txtMonth = this.ddlMonth.SelectedItem.Text.Substring(0, 3);
            string Month = ASITUtility03.GetMonth(txtMonth);
            string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);
            string txtDate = year+Month;
            string companyCode = this.ddlCompany.SelectedValue.ToString()=="000000000000" ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)+"%";
            string branchCode = this.ddlBranch.SelectedValue.ToString()=="000000000000" ? "%" : this.ddlBranch.SelectedValue.ToString().Substring(0, 9)+"%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_SALARY_RECON", "RPTSALRECONCILLATION", txtDate, branchCode, companyCode, "", "", "", "", "", "");
            if (ds4==null)
                return;

            ViewState["tblSalaryRecon"] = ds4.Tables[0];
            this.Data_Bind();
        }

        protected void gvSalaryRecon_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSalaryRecon.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblSalaryRecon"];
            this.gvSalaryRecon.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue.ToString());
            this.gvSalaryRecon.DataSource=dt;
            this.gvSalaryRecon.DataBind();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvSalaryRecon_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label empName = (Label)e.Row.FindControl("lblgvEmpName");
                Label curAmt = (Label)e.Row.FindControl("lblgvCurAmt");
                Label prevAmt = (Label)e.Row.FindControl("lblgvPrevAmt");

                string empId = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();

                if (empId == "")
                {
                    return;
                }
                if (empId == "000000000000" || empId == "AAAAAAAAAAAA")
                {
                    empName.Font.Bold = true;
                    curAmt.Font.Bold = true;
                    prevAmt.Font.Bold = true;
                }

            }
        }
    }
}