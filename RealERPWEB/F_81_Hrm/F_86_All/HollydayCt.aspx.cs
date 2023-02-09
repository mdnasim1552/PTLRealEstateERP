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
namespace RealERPWEB.F_81_Hrm.F_86_All
{
    public partial class HollydayCt : System.Web.UI.Page
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

                this.GetMonth();
                //((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE HOLLYDAY";
                this.lmsg11.Visible = false;
                this.GetEmployeeName();

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
            this.ddlMonth.DataTextField = "mnam";
            this.ddlMonth.DataValueField = "mno";
            this.ddlMonth.DataSource = ds1.Tables[0];
            this.ddlMonth.DataBind();
            this.ddlMonth.SelectedValue = System.DateTime.Today.Month.ToString().Trim();
        }


        private void GetEmployeeName()
        {
            Session.Remove("tblEmpDesc");
            string comcod = this.GetCompCode();
            string IdCard = this.txtSrcEmpCode.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLOYEENAME", IdCard, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds1.Tables[0];
            this.ddlEmpName.DataBind();
            Session["tblEmpDesc"] = ds1.Tables[0];
            this.ddlEmpName_SelectedIndexChanged(null, null);

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["tblEmpDesc"];
            DataRow[] dr1 = dt.Select("empid='" + empid + "'");
            if (dr1.Length > 0)
            {
                this.lblCompany.Text = dr1[0]["companydesc"].ToString();
                this.lblSection.Text = dr1[0]["secdesc"].ToString();
                this.lblDesignation.Text = dr1[0]["desig"].ToString();
            }
            this.ddlMonth_SelectedIndexChanged(null, null);
        }
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.chkDate.Items.Clear();

            string comcod = this.GetCompCode();
            string Month = this.ddlMonth.SelectedItem.Text.Substring(0, 3);
            string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);
            string date = "01-" + Month + "-" + year;
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "HOLLY_DATE", date, empid, "", "", "", "", "", "", "");

            if (ds4 == null)
            {
                return;
            }
            this.chkDate.DataTextField = "sdate1";
            this.chkDate.DataValueField = "sdate";
            this.chkDate.DataSource = ds4.Tables[0];
            this.chkDate.DataBind();
            DataTable dt = ds4.Tables[0];


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string flg = dt.Rows[i]["absfl"].ToString().Trim();
                this.chkDate.Items[i].Text = Convert.ToDateTime(dt.Rows[i]["sdate1"]).ToString("dd-MMM-yyyy (ddd)");
                if (flg == "True")
                    this.chkDate.Items[i].Selected = true;

            }

        }
        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            this.lmsg11.Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string empid = this.ddlEmpName.SelectedValue.ToString();

            for (int i = 0; i < this.chkDate.Items.Count; i++)
            {
                string dayid = Convert.ToDateTime(this.chkDate.Items[i].Value).ToString("yyyyMMdd");
                string date = Convert.ToDateTime(this.chkDate.Items[i].Value).ToString("dd-MMM-yyyy");
                string gcod = "07005";
                string txthstatus = (this.chkDate.Items[i].Selected == true) ? "True" : "False";
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPDATEHOLIDAY", dayid, empid, gcod, date, txthstatus, "", "", "", "", "", "", "", "", "", "");
            }

            this.lmsg11.Text = "Updated Successfully";
        }
        protected void imgbtnEmployee_Click(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }
    }
}
