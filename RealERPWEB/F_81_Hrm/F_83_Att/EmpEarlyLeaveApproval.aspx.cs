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
    public partial class EmpEarlyLeaveApproval : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetMonth();
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
            //string comcod = this.GetCompCode();
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "GETMONTHFORABS", "", "", "", "", "", "", "", "", "");
            //this.ddlMonth.DataTextField = "mnam";
            //this.ddlMonth.DataValueField = "mno";
            //this.ddlMonth.DataSource = ds1.Tables[0];
            //this.ddlMonth.DataBind();
            //this.ddlMonth.SelectedValue =System.DateTime.Today.Month.ToString().Trim();



            string comcod = this.GetCompCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETYEARMON", "", "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlMonth.DataTextField = "yearmon";
            this.ddlMonth.DataValueField = "ymon";
            this.ddlMonth.DataSource = ds1.Tables[0];
            this.ddlMonth.DataBind();
            this.ddlMonth.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            ds1.Dispose();
        }


        private void GetEmployeeName()
        {
            Session.Remove("tblEmpDesc");
            string comcod = this.GetCompCode();
            string IdCard = "%%";
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


            string comcod = this.GetCompCode();
            string Month = this.ddlMonth.SelectedItem.Text.Substring(0, 3);
            string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);
            string date = "01-" + Month + "-" + year;
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "ABSENT_DATE", date, empid, "", "", "", "", "", "", "");

            if (ds4 == null)
            {
                return;
            }

            DataTable dt = ds4.Tables[0];




        }

        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string empid = this.ddlEmpName.SelectedValue.ToString();
            //string month = this.ddlMonth.SelectedValue.ToString().Trim();
            //string month1=month.PadLeft(2, '0');
            //string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);
            //string monyr = month1+ year;
            //bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "DELETEABSCT", empid, monyr, "", "", "", "", "", "", "", "", "", "", "", "", "");
            //if (result == false) 
            //{
            //    this.lmsg11.Text = "Data was Noted Updated";
            //    return;

            //}


            //for (int i = 0; i < this.chkDate.Items.Count; i++) 
            //{
            //    if (this.chkDate.Items[i].Selected) 
            //    {

            //        string absdat = Convert.ToDateTime(this.chkDate.Items[i].Value).ToString("dd-MMM-yyyy");
            //        string absfl ="1";
            //        bool result1 = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "INORUPDATEABSENTCT", empid, absdat, absfl, monyr, "", "", "", "", "", "", "", "", "", "", "");

            //    }
            //}

            //this.lmsg11.Text = "Updated Successfully";

        }
        protected void imgbtnEmployee_Click(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }
        protected void gvMonthlyAttn_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }

        protected void lFinalTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }
        private void ShowData()
        {

          
            Session.Remove("tblEmpDesc");
            string comcod = this.GetCompCode();

            string Empid = this.ddlEmpName.SelectedValue.ToString();
            string MonthId = this.ddlMonth.SelectedValue.ToString().Trim();


            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPMONEARLYADJUSTMENT", Empid, MonthId, "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvMonthlyAttn.DataSource = null;
                this.gvMonthlyAttn.DataBind();
                return;
            }

            Session["tblEmpDesc"] = ds4.Tables[0];
            this.LoadGrid();

        }
        private void LoadGrid()
        {

            //this.gvMonthlyAttn.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvMonthlyAttn.DataSource = (DataTable)Session["tblEmpDesc"]; ;
            this.gvMonthlyAttn.DataBind();
        }
        protected void gvMonthlyAttn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblEmpDesc"];
            int TblRowIndex;
            for (int i = 0; i < this.gvMonthlyAttn.Rows.Count; i++)
            {
                string approved = ((CheckBox)this.gvMonthlyAttn.Rows[i].FindControl("txtgvapproved")).Checked.ToString();
                string rmrks = ((TextBox)this.gvMonthlyAttn.Rows[i].FindControl("txtgvrmrks")).Text.ToString();

                TblRowIndex = (gvMonthlyAttn.PageIndex) * gvMonthlyAttn.PageSize + i;
                dt.Rows[TblRowIndex]["earleaveapp"] = approved;
                dt.Rows[TblRowIndex]["rmrks"] = rmrks;
            }
            Session["tblEmpDesc"] = dt;
        }
        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string appdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string msg = "";
            this.SaveValue();
       
            DataTable dt = (DataTable)Session["tblEmpDesc"];
            string comcod = this.GetCompCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            foreach (DataRow dr1 in dt.Rows)
            {
                string dayid = Convert.ToDateTime(dr1["cdate"].ToString()).ToString("yyyyMMdd");
                string earleaveapp = dr1["earleaveapp"].ToString();
                string rmrks = dr1["rmrks"].ToString();
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "UPDATEEARLEAVEAPP", dayid, empid, earleaveapp, userid, appdate, rmrks, "", "", "", "", "", "", "", "", "");



            }




            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            this.LoadGrid();
        }
    }
}
