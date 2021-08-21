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
namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class RetiredEmployee : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.txtSepDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetCompName();
                //this.GetEmployeeName();

                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee Resign";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

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


        private void GetCompName()
        {
            if (this.lnkbtnShow.Text == "New")
                return;
            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompanyName.DataTextField = "actdesc";
            this.ddlCompanyName.DataValueField = "actcode";
            this.ddlCompanyName.DataSource = ds1.Tables[0];
            this.ddlCompanyName.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.GetDepartment();
            ds1.Dispose();
        }

        private void GetDepartment()
        {
            if (this.ddlCompanyName.Items.Count == 0)
                return;
            string comcod = this.GetCompCode();
            //  string txtCompanyname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompanyName.SelectedValue.ToString().Substring(0, hrcomln) + "%";


            string txtSearchDept = this.txtSrcDepartment.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", Company, txtSearchDept, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            this.GetSecion();
        }

        private void GetSecion()
        {
            if (this.ddlCompanyName.Items.Count == 0)
                return;
            string comcod = this.GetCompCode();
            //string Companycode = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompanyName.SelectedValue.ToString().Substring(0, hrcomln) + "%";

            string DeptCode = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string srchsecion = this.txtSrcSecion.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETSECTION", Company, DeptCode, srchsecion, "", "", "", "", "", "");
            this.ddlSection1.DataTextField = "actdesc";
            this.ddlSection1.DataValueField = "actcode";
            this.ddlSection1.DataSource = ds1.Tables[0];
            this.ddlSection1.DataBind();
            //this.ddlEmployee_SelectedIndexChanged(null,null);

            this.ddlSection1_SelectedIndexChanged(null, null);
        }


        private void GetSepType()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "GETSEAPRATIONTYPE", "", "", "", "", "", "", "", "", "");
            this.ddlSepType.DataTextField = "hrgdesc";
            this.ddlSepType.DataValueField = "hrgcod";
            this.ddlSepType.DataSource = ds1.Tables[0];
            this.ddlSepType.DataBind();
        }
        private void GetEmployeeName()
        {
            try
            {
                if (this.lnkbtnShow.Text == "New")
                    return;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                //  string compcode = (this.ddlCompanyName.SelectedValue.ToString().Substring(0,2)=="00")?"%":this.ddlCompanyName.SelectedValue.ToString().Substring(0,2) + "%";

                int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
                string compcode = this.ddlCompanyName.SelectedValue.ToString().Substring(0, hrcomln) + "%";


                string deptcode = (this.ddlDepartment.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
                string Section = this.ddlSection1.SelectedValue.ToString() + "%";
                string txtSProject = "%" + this.txtSrcEmployee.Text + "%";
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "GETEMPTNAME", compcode, deptcode, Section, txtSProject, "", "", "", "", "");
                Session["tblempdsg"] = ds3.Tables[0];
                this.ddlEmployee.DataTextField = "empname";
                this.ddlEmployee.DataValueField = "empid";
                this.ddlEmployee.DataSource = ds3.Tables[0];
                this.ddlEmployee.DataBind();
                this.ddlEmployee_SelectedIndexChanged(null, null);
            }

            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }


        }
        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();

        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSecion();
        }


        protected void ddlSection1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeName();

        }

        protected void ibtnFindCompany_Click(object sender, EventArgs e)
        {
            this.GetCompName();

        }

        protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }


        protected void imgbtnSection_Click(object sender, EventArgs e)
        {
            this.GetSecion();



        }


        protected void imgbtnEmployee_Click(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "Ok")
            {
                this.ddlCompanyName.Visible = false;
                this.lblCompanyName.Visible = true;
                this.lblCompanyName.Text = this.ddlCompanyName.SelectedItem.Text;
                this.ddlDepartment.Visible = false;
                this.lblDeptDesc.Visible = true;
                this.lblDeptDesc.Text = this.ddlDepartment.SelectedItem.Text;
                this.ddlSection1.Visible = false;
                this.lblSectionDesc.Visible = true;
                this.lblSectionDesc.Text = this.ddlSection1.SelectedItem.Text;
                this.ddlEmployee.Visible = false;
                this.lblddlEmployee.Visible = true;
                this.lblddlEmployee.Text = this.ddlEmployee.SelectedItem.Text;
                this.lnkbtnShow.Text = "New";
                this.PnlSepType.Visible = true;
                this.GetSepType();

                return;
            }
            this.ddlCompanyName.Visible = true;
            this.lblCompanyName.Visible = false;
            this.ddlDepartment.Visible = true;
            this.lblDeptDesc.Visible = false;
            this.ddlSection1.Visible = true;
            this.lblSectionDesc.Visible = false;
            this.ddlEmployee.Visible = true;
            this.lblddlEmployee.Visible = false;
            this.lnkbtnShow.Text = "Ok";
            this.lblCompanyName.Text = "";

            this.PnlSepType.Visible = false;
            this.ddlSepType.Items.Clear();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }



        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtSepDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string empid = this.ddlEmployee.SelectedValue.ToString();
            string sptype = (this.ddlSepType.SelectedValue.ToString() == "00000") ? "" : this.ddlSepType.SelectedValue.ToString();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "INSERTORUPDATESEPARATION", empid, date, sptype, userid, postDat, trmid, sessionid, "", "", "", "", "", "", "", "");
            if (!result)
                return;

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }



        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string empid = this.ddlEmployee.SelectedValue.ToString().Trim();
                DataTable dt = (DataTable)Session["tblempdsg"];
                DataRow[] dr = dt.Select("empid = '" + empid + "'");
                if (dr.Length > 0)
                {
                    this.lblDesig.Text = ((DataTable)Session["tblempdsg"]).Select("empid='" + empid + "'")[0]["desig"].ToString();
                    //string deptcode=((DataTable)Session["tblempdsg"]).Select("empid='" + empid + "'")[0]["deptid"].ToString().Substring(0,9)+"000";
                    //this.ddlDepartment.SelectedValue = ((DataTable)Session["tblempdsg"]).Select("empid='" + empid + "'")[0]["deptid"].ToString();
                    ////  this.SelectedSecion();
                    //  this.ddlSection1.SelectedValue = ((DataTable)Session["tblempdsg"]).Select("empid='" + empid + "'")[0]["refno"].ToString();
                    //this.ddlEmployee.SelectedValue = ((DataTable)Session["tblempdsg"]).Select("empid='" + empid + "'")[0]["empname"].ToString();


                    //this.ddlEmployee.SelectedValue=
                }
            }
            catch (Exception ex)
            {


                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }
        }
    }
}