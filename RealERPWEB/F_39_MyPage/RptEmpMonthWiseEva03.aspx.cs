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
using RealEntity;

namespace RealERPWEB.F_39_MyPage
{

    public partial class RptEmpMonthWiseEva03 : System.Web.UI.Page
    {
        private UserManagerKPI objUser = new UserManagerKPI();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Month Wise Evaluation";
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.txtfrmdate.Text = Convert.ToDateTime("01-Jan-" + date.Substring(7)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                this.lbluseid.Text = (Request.QueryString["Type"] == "IndEmp") ? hst["usrid"].ToString() : "";
                this.GetEmpList();


            }
        }

        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetEmpList()
        {

            //-----------Get Person List ---------------//

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Getcomcod();
            string srchEmp = "%" + this.txtSrchSalesTeam.Text.Trim() + "%";
            string userid = (this.Request.QueryString["Type"] == "IndEmp") ? hst["usrid"].ToString() : "";
            string deptcode = hst["deptcode"].ToString();
            List<RealEntity.C_47_Kpi.EClassEmpCode> lst3 = new List<RealEntity.C_47_Kpi.EClassEmpCode>();
            lst3 = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", srchEmp, userid, deptcode);

            this.ddlEmpid.DataTextField = "empname";
            this.ddlEmpid.DataValueField = "empid";
            this.ddlEmpid.DataSource = lst3;
            this.ddlEmpid.DataBind();
            Session["tblemployee"] = lst3;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            // ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkprint_Click);

        }


    }
}