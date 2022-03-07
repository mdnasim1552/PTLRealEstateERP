using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_89_Pay
{
    public partial class ModeofPaymentDept : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetMonth();
                this.bankname();
                this.GetDeptName();
                GetAllData();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Mode Of Payment Other Department";
            }
        }

      

        private void bankname()
        {
            string comcod = this.GetComCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETBANKNAME", "", "", "", "", "", "", "", "", "");
            this.ddlbank.DataTextField = "actdesc";
            this.ddlbank.DataValueField = "actcode";
            this.ddlbank.DataSource = ds1.Tables[0];
            this.ddlbank.DataBind();

        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetMonth()
        {
            string comcod = this.GetComCode();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETMONTHFOROFFDAY", "", "", "", "", "", "", "", "", "");
            this.ddlmon.DataTextField = "mnam";
            this.ddlmon.DataValueField = "yearmon";
            this.ddlmon.DataSource = ds2.Tables[0];
            this.ddlmon.DataBind();
            //this.ddlMonth.SelectedValue = System.DateTime.Today.ToString("dd-MM-yyyy").Trim();
            this.ddlmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM").Trim();


        }

        private void GetDeptName()
        {
            string comcod = this.GetComCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_SALARY_RECON", "GETOTHERDEPTNAME", "", "", "", "", "", "", "", "", "");
            this.ddldept.DataTextField = "SECTION";
            this.ddldept.DataValueField = "SECID";
            this.ddldept.DataSource = ds1.Tables[0];
            this.ddldept.DataBind();

        }

        protected void lnkAdd_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComCode();
            string secid = this.ddldept.SelectedValue.ToString();
            string montid = this.ddlmon.SelectedValue.ToString();
            string bankcod =this.ddlbank.SelectedValue.ToString();
            string amount = this.txtamt.Text.ToString()==""?"0": this.txtamt.Text.ToString();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTUPDATEOTHERDEPTAMOUNT", secid, montid, bankcod, amount, "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Data Insert Failed " + "');", true);
                return;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Data Inserted.... " + "');", true);       
            }


            this.GetAllData();
        }

        private void GetAllData()
        {
            string comcod = this.GetComCode();
            string monthid = this.ddlmon.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_SALARY_RECON", "GETOTHERDEPTSALRY", monthid, "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                return;
            }

            ViewState["OtherDeptSal"] = ds1.Tables[0];
            this.Data_bind();
        }

        private void Data_bind()
        {
           DataTable dt=(DataTable) ViewState["OtherDeptSal"];

            this.GvOtherDepSal.DataSource = dt;
            this.GvOtherDepSal.DataBind();
        }

        protected void ddlmon_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllData();
        }

        protected void GvOtherDepSal_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.GvOtherDepSal.EditIndex = e.NewEditIndex;
            this.Data_bind();
        }

        protected void GvOtherDepSal_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            this.GvOtherDepSal.EditIndex = -1;
            this.Data_bind();
        }
        
        protected void GvOtherDepSal_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string comcod = this.GetComCode();
            string secid = ((Label)GvOtherDepSal.Rows[e.RowIndex].FindControl("lblgvsection")).Text.Trim();
            string montid = ((Label)GvOtherDepSal.Rows[e.RowIndex].FindControl("lblgvmonid")).Text.Trim();
            string bankcod = ((Label)GvOtherDepSal.Rows[e.RowIndex].FindControl("lblgvbankcode")).Text.Trim();
            string amount = ((TextBox)GvOtherDepSal.Rows[e.RowIndex].FindControl("gvtxtamt")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTUPDATEOTHERDEPTAMOUNT", secid, montid, bankcod, amount, "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Updated Fail " + "');", true);
                return;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Updated Successfully " + "');", true);
            }

            this.GvOtherDepSal.EditIndex = -1;
            GetAllData();
        }
    }
}