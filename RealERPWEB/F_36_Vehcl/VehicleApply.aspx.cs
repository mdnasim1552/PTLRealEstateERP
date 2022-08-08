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
                txtFromDatetime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm").Replace(' ', 'T');
                txtToDatetime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm").Replace(' ', 'T');
                lblTransportId.Text = "0";
                GetApplicantName();
                getVehicleType();
                GetTransportInf();
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

        private void GetTransportInf()
        {
            string comcod = GetComCode();
            string trpid = lblTransportId.Text == "0" ? "%" : lblTransportId.Text + "%";
            DataSet ds1 = _process.GetTransInfo(comcod, "SP_ENTRY_VEHICLE_MANAGEMENT", "GETTRANSPORTINF", trpid, "", "", "", "", "", "", "", "");
            gvTransportInf.DataSource = ds1.Tables[0];
            gvTransportInf.DataBind();
            ViewState["TransportInf"]= ds1.Tables[0];
        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = GetComCode();
                string applicationId = ddlApplicantName.SelectedValue.ToString();
                string sdatetime = txtFromDatetime.Text.ToString().Replace('T', ' ');
                string edatetime = txtToDatetime.Text.ToString().Replace('T', ' ');
                string prefveh = ddlPrefVehicle.SelectedValue.ToString();
                string purpose = txtPurpose.Text;
                string transportId = lblTransportId.Text;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userId = hst["usrid"].ToString();
                string destination = txtDestination.Text;

                bool resultA = _process.UpdateTransInfo3(comcod, "SP_ENTRY_VEHICLE_MANAGEMENT", "UPSERTTRNSPORTINF", transportId,
                                applicationId, sdatetime, edatetime, purpose, prefveh, "1", destination, "", "", "", "",
                                     "", "", "", "", "", "", "", "", "", "", userId);
                if (resultA==false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated Successful" + "');", true);                   
                    Clear();
                    GetTransportInf();
                }


            }
            catch(Exception ex)
            {

            }
        }



        private void Clear()
        {
            txtFromDatetime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm").Replace(' ', 'T');
            txtToDatetime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm").Replace(' ', 'T');
            lblTransportId.Text = "0";
            GetApplicantName();
            getVehicleType();
            txtPurpose.Text = "";
            txtDestination.Text = "";
            ddlApplicantName.Enabled = true;
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

        protected void LnkbtnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string trpid = ((Label)this.gvTransportInf.Rows[index].FindControl("lblvehicleId")).Text.ToString();
            lblTransportId.Text = trpid;
            string comcod = GetComCode();           
            DataSet ds1 = _process.GetTransInfo(comcod, "SP_ENTRY_VEHICLE_MANAGEMENT", "GETTRANSPORTINF", trpid, "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];
            ddlApplicantName.SelectedValue = dt.Rows[0]["aplid"].ToString();
            lblDept.Text = dt.Rows[0]["deptname"].ToString();
            lblDesignation.Text = dt.Rows[0]["gdatat"].ToString();
            ddlPrefVehicle.Text= dt.Rows[0]["PREFVEHTYPE"].ToString();
            txtFromDatetime.Text = Convert.ToDateTime(dt.Rows[0]["fdate"].ToString()).ToString("yyyy-MM-dd HH:mm").Replace(' ', 'T');
            txtToDatetime.Text = Convert.ToDateTime(dt.Rows[0]["tdate"].ToString()).ToString("yyyy-MM-dd HH:mm").Replace(' ', 'T');
            txtPurpose.Text= dt.Rows[0]["apppurpose"].ToString();
            ddlApplicantName.Enabled = false;
        }
    }
}