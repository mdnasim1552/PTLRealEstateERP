using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_21_MKT
{
    public partial class RptDailyWorkStatus : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();
        string msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetEmployee();
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
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetEmployee ()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_REPORT_CRM_MODULE", "GET_SALES_EMPLOYEE", "", "", "", "", "", "", "", "", "");
            if(ds1 == null)
            {
                msg = "No Data Found!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }

            this.ddlEmployee.DataTextField = "empname";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = ds1.Tables[0];
            this.ddlEmployee.DataBind();
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = this.txtDate.Text.Trim();
            string empid = this.ddlEmployee.SelectedValue.ToString();
            DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_REPORT_CRM_MODULE", "RPT_DAILY_WORK_STATUS", date, empid, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                msg = "No Data Found!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }

            ViewState["tbldayworkstatus"] = ds2.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tbldayworkstatus"];
            this.gvDailyWorkStatus.DataSource = dt;
            this.gvDailyWorkStatus.DataBind();
        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {
            
        }       
    }
}