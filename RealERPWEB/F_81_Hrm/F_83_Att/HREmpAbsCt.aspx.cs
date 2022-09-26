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
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
//using CrystalDecisions.ReportSource;
using RealERPLIB;
using System.Globalization;

//using RealERPRPT;
namespace RealERPWEB.F_81_Hrm.F_83_Att
{
    public partial class HREmpAbsCt : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        Common compUtility = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.GetMonth();
                this.GetCompanyName();
               
                this.ddlMonth_SelectedIndexChanged(null, null);
                //this.GetEmployeeName();


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
            this.ddlMonth.DataValueField = "yearmon";
            this.ddlMonth.DataSource = ds1.Tables[0];
            this.ddlMonth.DataBind();
            this.ddlMonth.SelectedValue = System.DateTime.Today.ToString("yyyyMM").Trim();
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
            //string empid = this.ddlEmpName.SelectedValue.ToString();
            //DataTable dt = (DataTable)Session["tblEmpDesc"];
            //DataRow[] dr1 = dt.Select("empid='" + empid + "'");
            //if (dr1.Length > 0)
            //{
            //    this.lblCompany.Text = dr1[0]["companydesc"].ToString();
            //    this.lblSection.Text = dr1[0]["secdesc"].ToString();
            //    this.lblDesignation.Text = dr1[0]["desig"].ToString();
            //}
            this.ddlMonth_SelectedIndexChanged(null, null);
        }


        private string Getdatestart()
        {
            DataSet datSetup = compUtility.GetCompUtility();

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);

            return startdate;
        }
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.chkDate.Items.Clear();

            string comcod = this.GetCompCode();
            string yearmon = this.ddlMonth.SelectedValue.ToString(); ;
           // string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);        
          // DateTime date2 = DateTime.ParseExact(date1, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
            //DateTime date1 = DateTime.Parse(this.ddlMonth.SelectedValue.ToString());
            string cudate = "";
            string date = "";
            switch (comcod)
            {
                case "3365":
                case "3101":
                case "3338":
                case "1206":
                case "1207":
                case "3369":
                    date ="26-"+ASTUtility.Month3digit( Convert.ToInt32(yearmon.Substring(4, 2)))  +"-"+ yearmon.Substring(0, 4);
                    cudate = Convert.ToDateTime(date).AddMonths(-1).ToString("dd-MMM-yyyy");
                   
                    break;

                default:
                    date = "01-" + ASTUtility.Month3digit(Convert.ToInt32(yearmon.Substring(4, 2))) + "-" + yearmon.Substring(0, 4);
                    cudate = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
                    break;
            }
            //string date = Getdatestart() + cudate.Trim().Substring(2);

            //string date = "01-" + Month + "-" + year;
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "ABSENT_DATE", cudate, empid, "", "", "", "", "", "", "");

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
                if (flg == "1")
                    this.chkDate.Items[i].Selected = true;

            }

        }
        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
         
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string month = this.ddlMonth.SelectedValue.ToString();
            //string month1 = month;
            //string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);
            //string monyr = this.ddlMonth.SelectedValue.ToString();

            string month1 = ASTUtility.Right(month.Trim(), 2); // month.PadLeft(2, '0');
            string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);
            string monyr = month1 + year;
            string msg = "";


            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "DELETEABSCT", empid, monyr, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {



                msg = "Update Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;

            }
            for (int i = 0; i < this.chkDate.Items.Count; i++)
            {
                if (this.chkDate.Items[i].Selected)
                {

                    string absdat = Convert.ToDateTime(this.chkDate.Items[i].Value).ToString("dd-MMM-yyyy");
                    string absfl = "1";
                    bool result1 = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "INORUPDATEABSENTCT", empid, absdat, absfl, monyr, "", "", "", "", "", "", "", "", "", "", "");

                }
            }


            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetCompanyName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();


            string txtCompany = "%%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            Session["tblcompany"] = ds5.Tables[0];

            this.ddlCompanyAgg.DataTextField = "actdesc";
            this.ddlCompanyAgg.DataValueField = "actcode";
            this.ddlCompanyAgg.DataSource = ds5.Tables[0];
            this.ddlCompanyAgg.DataBind();
            this.GetDepartment();
            //this.ddlCompanyAgg_SelectedIndexChanged(null, null);
        }
        private void GetDepartment()
        {
            string comcod = this.GetComeCode();
            //   string type = this.Request.QueryString["Type"].ToString().Trim();
          //  string Company = ((this.ddlCompanyAgg.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, 2)) + "%";
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyAgg.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, hrcomln) + "%";



            string txtSProject = "%%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEPTNAMENEW", Company, txtSProject, "", "", "", "", "", "", "");

            this.ddldepartmentagg.DataTextField = "deptdesc";
            this.ddldepartmentagg.DataValueField = "deptcode";
            this.ddldepartmentagg.DataSource = ds4.Tables[0];
            this.ddldepartmentagg.DataBind();
            this.GetProjectName();
        }

        private void GetProjectName()
        {
            string comcod = this.GetComeCode();

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyAgg.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string deptcode = this.ddldepartmentagg.SelectedValue.ToString()== "000000000000" ? Company : this.ddldepartmentagg.SelectedValue.ToString() + "%";

 

            string txtSProject = "%%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAMENEW", deptcode, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds4.Tables[0];
            this.ddlProjectName.DataBind();
            this.GetEmpName();
        }

        private void GetEmpName()
        {
            string comcod = this.GetComeCode();

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyAgg.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string dptcode = this.ddldepartmentagg.SelectedValue.ToString() == "000000000000" ? Company : ASTUtility.Left(this.ddldepartmentagg.SelectedValue.ToString(),9) + "%";
            string ProjectCode = this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? dptcode : this.ddlProjectName.SelectedValue.ToString() + "%";



            
            string txtSProject = "%%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPREMPNAME", ProjectCode, txtSProject, "", "", "", "", "", "", "");
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds5.Tables[0];
            this.ddlEmpName.DataBind();
            ViewState["tblemp"] = ds5.Tables[0];
           // this.GetComASecSelected();
        }
        private void GetComASecSelected()
        {
            string empid = this.ddlEmpName.SelectedValue.ToString();
            if (empid == "000000000000" || empid == "")
                return;
            DataTable dt = (DataTable)ViewState["tblemp"];
            DataRow[] dr = dt.Select("empid = '" + empid + "'");
            if (dr.Length > 0)
            {
                this.ddlCompanyAgg.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["companycode"].ToString();
                this.ddldepartmentagg.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["deptcode"].ToString();
                this.ddlProjectName.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["refno"].ToString();
            }
        }
        protected void imgbtnEmployee_Click(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }
        protected void ibtnFindCompany_Click(object sender, EventArgs e)
        {
            this.GetCompanyName();
        }
        protected void ddlCompanyAgg_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        protected void imgbtnEmployee_Click1(object sender, EventArgs e)
        {
            this.GetEmpName();

            //this.GetEmployeeName();

        }
        protected void ddldepartmentagg_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.GetDepartment();
            this.GetProjectName();
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmpName();
            this.ddlMonth_SelectedIndexChanged(null, null);

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtndeptagg_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        //protected void ddlEmpName_SelectedIndexChanged1(object sender, EventArgs e)
        //{
        //    this.ddlMonth_SelectedIndexChanged(null, null);
        //}
    }
}
