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
    public partial class EmpHold : System.Web.UI.Page
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
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetCompName();
                this.GetMonth();
                this.SelectDate();
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE HOLD LIST";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void SelectDate()
        {
            string comcod = this.GetCompCode();
            DataSet datSetup = compUtility.GetCompUtility();
            string monthid = ASTUtility.Right(this.ddlMonth.SelectedValue, 2).ToString();
            string yearID = ASTUtility.Left(this.ddlMonth.SelectedValue, 4).ToString();

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            string date = startdate + ASTUtility.Month3digit(Convert.ToInt32(monthid)) + "-" + yearID;

            if (datSetup == null)
                return;
            switch (comcod)
            {
                case "3365":
                case "3101":
                case "3368":
                     

                    string frmdate = Convert.ToDateTime(date).AddMonths(-1).ToString("dd-MMM-yyyy");
                    string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy"); 

                    this.txtfrmDate.Text = frmdate;
                    this.txttoDate.Text = todate;
                    break;

                default:
                    this.txtfrmDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfrmDate.Text = startdate + this.txtfrmDate.Text.Trim().Substring(2);
                    this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;
            }
        }
        protected void createtable()
        {

            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("comcod", Type.GetType("System.String"));
            dttemp.Columns.Add("empid", Type.GetType("System.String"));
            dttemp.Columns.Add("empname", Type.GetType("System.String"));
            dttemp.Columns.Add("idcard", Type.GetType("System.String"));
            dttemp.Columns.Add("desig", Type.GetType("System.String"));
            dttemp.Columns.Add("frmdate", Type.GetType("System.DateTime"));
            dttemp.Columns.Add("todate", Type.GetType("System.DateTime"));
            Session["tblemphold"] = dttemp;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        private void GetCompName()
        {
            if (this.lbtnOk.Text == "New")
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = this.ddlCompanyName.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GET_ACCESSED_COMPANYLIST", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompanyName.DataTextField = "actdesc";
            this.ddlCompanyName.DataValueField = "actcode";
            this.ddlCompanyName.DataSource = ds1.Tables[0];
            this.ddlCompanyName.DataBind();
            this.GetDepartment();
        }

        private void GetDepartment()
        {
            if (this.ddlCompanyName.Items.Count == 0)
                return;
            string comcod = this.GetCompCode();
            string txtCompanyname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSearchDept = this.ddlDepartment.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
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
            string Companycode = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string DeptCode = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 8) + "%";
            string srchsecion = this.ddlSection.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETSECTION", Companycode, DeptCode, srchsecion, "", "", "", "", "", "");
            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = ds1.Tables[0];
            this.ddlSection.DataBind();
        }

        private void GetEmployeeName()
        {

            Session.Remove("tblempdsg");
            string comcod = this.GetCompCode();
            string compcode = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptcode = (this.ddlDepartment.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 7) + "%";
            string Section = (this.ddlSection.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlSection.SelectedValue.ToString() + "%"; //"%"; 
                                                                                                                                                        //string txtSProject = "%" + this.ddlEmployee.Text + "%";
            string txtSProject = "%%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPNAMELIST", compcode, deptcode, Section, txtSProject, "", "", "", "", "");
            if(ds3==null)
            {
                return;
            }
            Session["tblempdsg"] = ds3.Tables[0];
            this.ddlEmployee.DataTextField = "empname";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = ds3.Tables[0];
            this.ddlEmployee.DataBind();

        }

        private void GetMonth()
        {

            string comcod = this.GetCompCode();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETMONTH", "", "", "", "", "", "", "", "", "");
            this.ddlMonth.DataTextField = "mnam";
            this.ddlMonth.DataValueField = "yearmon";
            this.ddlMonth.DataSource = ds2.Tables[0];
            this.ddlMonth.DataBind();
            this.ddlMonth.SelectedValue = System.DateTime.Today.ToString("yyyyMM");


        }
        protected void ibtnFindCompany_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetCompName();
        }
        protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetDepartment();
        }
        protected void imgbtnSection_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetSecion();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.ddlCompanyName.Enabled = false;
                this.ddlDepartment.Enabled = false;
                this.ddlSection.Enabled = false;
                this.ddlMonth.Enabled = false;
                this.lbtnOk.Text = "New";
                this.PnlSub.Visible = true;
                this.SelectDate();
                this.GetEmployeeName();
                this.ShowEmpHold();


                return;
            }
            this.ddlCompanyName.Enabled = true;

            this.ddlDepartment.Enabled = true;

            this.ddlSection.Enabled = true;

            this.ddlMonth.Enabled = true;

            this.lbtnOk.Text = "Ok";


            this.PnlSub.Visible = false;
            this.ddlEmployee.Items.Clear();
            this.gvemphold.DataSource = null;
            this.gvemphold.DataBind();
            Session.Remove("tblemphold");
        }

        private void ShowEmpHold()
        {

            Session.Remove("tblemphold");
            string comcod = this.GetCompCode();
            string Month = this.ddlMonth.SelectedValue.ToString();
            string company = this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string dept = ((this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9)) + "%"; ;
            string secid = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string empid = ((this.ddlEmployee.SelectedValue.ToString() == "000000000000") ? "" : this.ddlEmployee.SelectedValue.ToString()) + "%";

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPHOLD", Month, company, dept, secid, empid, "", "", "", "");

            if (ds1 == null)
            {

                this.gvemphold.DataSource = null;
                this.gvemphold.DataBind();
                return;

            }

            Session["tblemphold"] = ds1.Tables[0];
            this.Data_Bind();


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSecion();

        }
        protected void imgbtnEmployee_Click(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }

        private void SaveValue()
        {
            //if (Session["tblemphold"] == null) 
            //    this.createtable();

            DataTable dt = (DataTable)Session["tblemphold"];
            string empid = this.ddlEmployee.SelectedValue.ToString();
            if (empid == "")
                return;
            DataRow[] dr = dt.Select("empid='" + empid + "'");
            if (dr.Length == 0)
            {
                DataRow dr1 = dt.NewRow();

                dr1["empid"] = empid;
                dr1["empname"] = this.ddlEmployee.SelectedItem.Text.Trim();
                dr1["idcard"] = (((DataTable)Session["tblempdsg"]).Select("empid='" + empid + "'"))[0]["idcard"];
                dr1["desig"] = (((DataTable)Session["tblempdsg"]).Select("empid='" + empid + "'"))[0]["desig"];
                dr1["frmdate"] = this.txtfrmDate.Text.Trim();
                dr1["todate"] = this.txttoDate.Text.Trim();
                dt.Rows.Add(dr1);
            }
            else
            {

                dr[0]["frmdate"] = this.txtfrmDate.Text.Trim();
                dr[0]["todate"] = this.txttoDate.Text.Trim();
                Session["tblemphold"] = dt;
            }
            this.Data_Bind();


        }

        private void Data_Bind()
        {

            this.gvemphold.DataSource = (DataTable)Session["tblemphold"];
            this.gvemphold.DataBind();

        }

        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            this.SaveValue();

        }

        protected void lnkupdate_Click(object sender, EventArgs e)
        {
            string msg = "";
            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();
                string postDat = System.DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
                string sessionid = hst["session"].ToString();
                string trmid = hst["compname"].ToString();
                string comcod = this.GetCompCode();
                DataTable dt = (DataTable)Session["tblemphold"];
                string Month = this.ddlMonth.SelectedValue.ToString();





                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string frmdate = Convert.ToDateTime(dt.Rows[i]["frmdate"].ToString()).ToString("dd-MMMM-yyyy");
                    string todate = Convert.ToDateTime(dt.Rows[i]["todate"].ToString()).ToString("dd-MMMM-yyyy");
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSORUPEMPHOLD", Month, empid, frmdate, todate, userid, postDat, trmid, sessionid, "", "", "", "", "", "", "");

                    if (!result)
                        return;
                    string eventdesc2 = "Salary hold Monthid : "+ Month + ",Empid " + empid;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "EMPLOYEE HOLD", eventdesc2, "");

                }

                msg = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);



            }
            catch (Exception ex)
            {
                msg = "Update Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);


            }


        }

        protected void gvemphold_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblemphold"];
            string Month = this.ddlMonth.SelectedValue.ToString();
            int rowindex = (this.gvemphold.PageSize) * (this.gvemphold.PageIndex) + e.RowIndex;
            string empid = dt.Rows[rowindex]["empid"].ToString();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELETEEMPHOLD", Month, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                dt.Rows[rowindex].Delete();
                string eventdesc2 = "Salary hold Removed Monthid : " + Month + ",Empid " + empid;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "EMPLOYEE HOLD REMOVED", eventdesc2, "");

            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblemphold");
            Session["tblemphold"] = dv.ToTable();
            this.Data_Bind();


        }
    }
}
 