using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_36_Vehcl
{
    public partial class VehicleApply : System.Web.UI.Page
    {
        ProcessAccess _process = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetApplicantName();
                getVehicleType();
            }
        }
        private void getVehicleType()
        {
            string comcod = GetComCode();
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_VEHICLE_MANAGEMENT", "GETVEHICLETYP", "", "", "", "", "", "", "", "", "", "", "");
            if (ds == null)
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);

            ddlPrefVehicle.DataSource = ds.Tables[0];
            ddlPrefVehicle.DataTextField = "sirdesc";
            ddlPrefVehicle.DataValueField = "sircode";
            ddlPrefVehicle.DataBind();

        }
        private void GetApplicantName()
        {
            string comcod = GetComCode();
            DataSet ds1 = _process.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLIST", "%", "%", "%", "", "", "", "", "", "");
            ddlApplicantName.DataSource = ds1.Tables[0];
            ddlApplicantName.DataTextField = "empname";
            ddlApplicantName.DataValueField = "empid";
            ddlApplicantName.DataBind();
            ViewState["ListEmpAllInfo"] = ds1.Tables[0];
            ddlApplicantName_SelectedIndexChanged(null, null);
        }


        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {

        }



        private void Clear()
        {
         
        }
       

        protected void lnkClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void ddlApplicantName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["ListEmpAllInfo"];
            string empid = ddlApplicantName.SelectedValue.ToString();
            DataRow[] dr = dt.Select("empid = '" + empid + "'");
            if (dr.Length > 0)
            {
                lblDept.Text = dt.Select("empid='" + empid + "'")[0]["refdesc"].ToString();               
                lblDesignation.Text = dt.Select("empid='" + empid + "'")[0]["desig"].ToString();
            }
        }
    }
}