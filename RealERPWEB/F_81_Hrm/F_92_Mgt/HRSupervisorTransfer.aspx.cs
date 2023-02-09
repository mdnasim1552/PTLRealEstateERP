using Microsoft.Reporting.WinForms;
using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class HRSupervisorTransfer : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                if (dr1.Length==0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetSupervisorEmployee();
                this.GetAllEmployee();
                this.ddlEmpid_SelectedIndexChanged(null, null);

            }
        }
        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

        }

        private void GetSupervisorEmployee()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();
            DataSet ds1 = instcrm.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GET_SUPERVISOR_EMP", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlEmpid.DataTextField = "empname";
            this.ddlEmpid.DataValueField = "empid";
            this.ddlEmpid.DataSource = ds1.Tables[0];
            this.ddlEmpid.DataBind();

            ViewState["tblsupemp"] = ds1.Tables[0];
            ds1.Dispose();
        }

        private void GetAllEmployee()
        {
            string comcod = GetComeCode();
            DataSet ds1 = instcrm.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GET_ACTIVE_EMPLOYEE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblallemp"] = ds1.Tables[0];
            this.BindAllEmployee();
            ds1.Dispose();

        }
        private void BindAllEmployee()
        {
            DataTable dt = (DataTable)ViewState["tblallemp"];
            string empid = this.ddlEmpid.SelectedValue.ToString();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("empid <>'" + empid + "'");

            this.ddlEmpNameTo.DataTextField = "empname";
            this.ddlEmpNameTo.DataValueField = "empid";
            this.ddlEmpNameTo.DataSource = dv.ToTable();
            this.ddlEmpNameTo.DataBind();
        }
        protected void ddlEmpid_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetData();
            this.BindAllEmployee();
        }

        private void GetData()
        {
            ViewState.Remove("tblsupvisedempdet");
            string comcod = this.GetComeCode();
            string empId = this.ddlEmpid.SelectedValue.ToString();
            DataSet ds1 = instcrm.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GET_SUPERVISED_EMP_DETAILS", empId, "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblsupvisedempdet"] = (ds1.Tables[0]);
            this.Data_Bind();

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblsupvisedempdet"];
            this.gvSupEmpDetails.DataSource = dt;
            this.gvSupEmpDetails.DataBind();

            if (gvSupEmpDetails.Rows.Count > 0)
            {
                Session["Report1"] = gvSupEmpDetails;
                ((HyperLink)this.gvSupEmpDetails.HeaderRow.FindControl("hlnkbtnSupEmp")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }


        protected void chkAllfrm_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblsupvisedempdet"];

            int i, index;
            if (((CheckBox)this.gvSupEmpDetails.HeaderRow.FindControl("chkAllfrm")).Checked)
            {

                for (i = 0; i < this.gvSupEmpDetails.Rows.Count; i++)
                {
                    ((CheckBox)this.gvSupEmpDetails.Rows[i].FindControl("chckTrnsfer")).Checked = true;
                    index = (this.gvSupEmpDetails.PageSize) * (this.gvSupEmpDetails.PageIndex) + i;
                    dt.Rows[index]["chkper"] = "True";
                }
            }

            else
            {
                for (i = 0; i < this.gvSupEmpDetails.Rows.Count; i++)
                {
                    ((CheckBox)this.gvSupEmpDetails.Rows[i].FindControl("chckTrnsfer")).Checked = false;
                    index = (this.gvSupEmpDetails.PageSize) * (this.gvSupEmpDetails.PageIndex) + i;
                    dt.Rows[index]["chkper"] = "False";
                }

            }
            ViewState["tblsupvisedempdet"] = dt;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string msg;
            bool result = false;
            string comcod = this.GetComeCode();

            for (int i = 0; i < this.gvSupEmpDetails.Rows.Count; i++)
            {
                string chkper = (((CheckBox)gvSupEmpDetails.Rows[i].FindControl("chckTrnsfer")).Checked) ? "True" : "False";
                if (chkper == "True")
                {
                    string empId = ((Label)gvSupEmpDetails.Rows[i].FindControl("lblgvEmpId")).Text.Trim();
                    string toSupId = this.ddlEmpNameTo.SelectedValue.ToString();

                    result = instcrm.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEE_TRANSFER_TOSUPERVISOR", null, null, null, empId, toSupId, "", "",
                   "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        msg = instcrm.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                        return;
                    }
                    else
                    {
                        msg = "Employees Transfered to New Supervisor Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                    }
                }

            }

            this.ddlEmpid_SelectedIndexChanged(null, null);

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }


    }
}