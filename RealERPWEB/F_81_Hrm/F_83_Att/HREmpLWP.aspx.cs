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
namespace RealERPWEB.F_81_Hrm.F_83_Att
{
    public partial class HREmpLWP : System.Web.UI.Page
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
                this.GetEmployeeName();
                //((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE DAILY ABSENT INFORMATION ";
                this.lmsg11.Visible = false;

            }
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
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "GETLWPDATE", date, empid, "", "", "", "", "", "", "");

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

                string flg = dt.Rows[i]["lwpfl"].ToString().Trim();
                this.chkDate.Items[i].Text = Convert.ToDateTime(dt.Rows[i]["sdate1"]).ToString("dd-MMM-yyyy (ddd)");
                if (flg == "1")
                    this.chkDate.Items[i].Selected = true;

            }

        }
        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string month = this.ddlMonth.SelectedValue.ToString().Trim();
            string month1 = month.PadLeft(2, '0');
            string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);
            string monyr = month1 + year;
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "DELETELWPCT", empid, monyr, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                this.lmsg11.Text = "Data was Noted Updated";
                return;

            }


            for (int i = 0; i < this.chkDate.Items.Count; i++)
            {
                if (this.chkDate.Items[i].Selected)
                {

                    string absdat = Convert.ToDateTime(this.chkDate.Items[i].Value).ToString("dd-MMM-yyyy");
                    string absfl = "1";
                    bool result1 = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "INORUPDATELWPCT", empid, absdat, absfl, monyr, "", "", "", "", "", "", "", "", "", "", "");

                }
            }

            this.lmsg11.Text = "Updated Successfully";

        }
        protected void imgbtnEmployee_Click(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }
    }
}
