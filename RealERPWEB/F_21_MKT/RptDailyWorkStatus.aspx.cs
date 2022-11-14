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

            ViewState["tbldayworkstatus"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }

        private object HiddenSameData(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                return dt;
            }
            string grp = dt.Rows[0]["grp"].ToString();

            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if(dt.Rows[i]["grp"].ToString() == grp)
                {
                    dt.Rows[i]["grpdesc"] = "";
                }

                grp = dt.Rows[i]["grp"].ToString();
            }
            return dt;
        }

        private void Data_Bind()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["tbldayworkstatus"];
                this.gvDailyWorkStatus.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
                this.gvDailyWorkStatus.DataSource = dt;
                this.gvDailyWorkStatus.DataBind();

                if (dt.Rows.Count > 0)
                {
                    Session["Report1"] = gvDailyWorkStatus;
                    string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
                    Session["ReportName"] = "Daily_Work_Report_" + frmdate;
                    ((HyperLink)this.gvDailyWorkStatus.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../../RDLCViewer.aspx?PrintOpt=GRIDTOEXCELNEW";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            }
          
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvDailyWorkStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDailyWorkStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvDailyWorkStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string grp = ((Label)e.Row.FindControl("lblgvGrp")).Text.Trim();
                Label grpdesc = (Label)e.Row.FindControl("lblgvWorkStatus");

                if (grp == "AAAA")
                {
                    grpdesc.Attributes["style"] = "background:blue; color:white!important";
                }
                else if(grp == "BBBB")
                {
                    grpdesc.Attributes["style"] = "background:red; color:white!important";
                }               
            }
        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
           
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)ViewState["tbldayworkstatus"];
            string txtDate =Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string teamdesc = dt.Rows[0]["teamdesc"].ToString();

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_21_Mkt.ECRMClientInfo.EClassDailyWorkStatus>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_21_MKT.RptDailyWorkStatus", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("teamdesc", "Employee Name : " + teamdesc));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Daily Work Status (Employee Wise)"));
            Rpt1.SetParameters(new ReportParameter("txtDate","Date : "+ txtDate));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate, session)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_');</script>";
        }
    }
}