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
                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee Resign";
                this.GetSepType();
                this.GetResignedEmpTable();
                this.CommonButton();
                //this.imgbtnEmployee_Click(null,null);
            }

        }
        public void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Update";
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdateResignEmp_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private string getLockMonthId()
        {
            string comcod = this.GetCompCode();
            string monthid = "";
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "ISLOCKSALSHEET", "", "", "", "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return monthid;

            monthid = ds.Tables[0].Rows[0]["monthid"].ToString();
            return monthid;
        }


        private void GetCompName()
        {


            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = "%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GET_ACCESSED_COMPANYLIST", txtCompany, userid, "", "", "", "", "", "", "");
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
            string txtSearchDept = "%";
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
            string srchsecion = "%";
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
            Session["tblSepType"] = ds1.Tables[0];
        }
        private void GetEmployeeName()
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
                string compcode = this.ddlCompanyName.SelectedValue.ToString().Substring(0, hrcomln) + "%";
                string deptcode = (this.ddlDepartment.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
                string Section = (this.ddlSection1.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection1.SelectedValue.ToString() + "%";
               
                string txtSProject = "%";
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

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);


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
        //protected void lnkbtnShow_Click(object sender, EventArgs e)
        //{
        //    if (this.lnkbtnShow.Text == "Ok")
        //    {

        //        this.ddlCompanyName.Enabled = false; 
        //        this.ddlDepartment.Enabled = false;
        //        this.ddlSection1.Enabled = false; 
        //        this.ddlEmployee.Enabled = false;
        //        this.lnkbtnShow.Text = "New";
        //        this.PnlSepType.Visible = true;
        //        this.GetSepType();

        //        return;
        //    }

        //    this.lnkbtnShow.Text = "Ok";            
        //    this.PnlSepType.Visible = false;
        //    this.ddlSepType.Items.Clear();
        //}
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        //protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        //{
        //    ((Label)this.Master.FindControl("lblmsg")).Visible = true;
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string userid = hst["usrid"].ToString();
        //    string postDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
        //    string sessionid = hst["session"].ToString();
        //    string trmid = hst["compname"].ToString();
        //    DataTable dt = (DataTable)Session["tblover"];
        //    string comcod = this.GetCompCode();
        //    string date = Convert.ToDateTime(this.txtSepDate.Text.Trim()).ToString("dd-MMM-yyyy");
        //    string empid = this.ddlEmployee.SelectedValue.ToString();
        //    string sptype = (this.ddlSepType.SelectedValue.ToString() == "00000") ? "" : this.ddlSepType.SelectedValue.ToString();
        //    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "INSERTORUPDATESEPARATION", empid, date, sptype, userid, postDat, trmid, sessionid, "", "", "", "", "", "", "", "");
        //    if (!result)
        //        return;
        //    string msg = "Updated Successfully";
        //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
        //}

        private void lbtnUpdateResignEmp_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "You have no permission" + "');", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string comcod = hst["comcod"].ToString();
            this.SaveValue();
            DataTable tbl1 = (DataTable)Session["tblsepemp"];
            int count = 0;
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                string empId = tbl1.Rows[i]["empid"].ToString();
                string sepDate = Convert.ToDateTime(tbl1.Rows[i]["sepdate"]).ToString("yyyyMMdd");
                string sepCode = tbl1.Rows[i]["sepcode"].ToString();
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "INSERTORUPDATESEPARATION", empId, sepDate, sepCode, userid, postDat, trmid, sessionid, "", "", "", "", "", "", "", "");
                count++;

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
            }

            string msg = "Resigned ("+count+ ") Employee Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update Resign Employee";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        private void GetResignedEmpTable()
        {
            ViewState.Remove("tblsepemp");
            DataTable tbl01 = new DataTable();
            tbl01.Columns.Add("empid", Type.GetType("System.String"));
            tbl01.Columns.Add("idcard", Type.GetType("System.String"));
            tbl01.Columns.Add("empname", Type.GetType("System.String"));
            tbl01.Columns.Add("septype", Type.GetType("System.String"));
            tbl01.Columns.Add("sepcode", Type.GetType("System.String"));
            tbl01.Columns.Add("sepdate", Type.GetType("System.DateTime"));
            Session["tblsepemp"] = tbl01;
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
                    string errMsg = ((DataTable)Session["tblempdsg"]).Select("empid='" + empid + "'")[0]["desig"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + errMsg + "');", true);

                }
            }
            catch (Exception ex)
            {


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);

            }
        }

        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["tblempdsg"];
                DataTable dt1 = (DataTable)Session["tblsepemp"];
                string empId = this.ddlEmployee.SelectedValue.ToString();
                string empIdCard = dt.Select("empid='"+empId+"'")[0]["idcard"].ToString();
                string empName = dt.Select("empid='"+empId+"'")[0]["empname1"].ToString();
                string sepType = this.ddlSepType.SelectedItem.Text.Trim();
                string sepCode = this.ddlSepType.SelectedValue.ToString();
                string sepDate = this.txtSepDate.Text;
                DataRow[] dr1 = dt1.Select("empid='" + empId + "'");
                if (dr1.Length == 0)
                {
                    DataRow dr = dt1.NewRow();
                    dr["empid"] = empId;
                    dr["idcard"] = empIdCard;
                    dr["empname"] = empName;
                    dr["septype"] = sepType;
                    dr["sepcode"] = sepCode;
                    dr["sepdate"] = sepDate;
                    dt1.Rows.Add(dr);
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Employee Already Added!" + "');", true);
                }

                Session["tblsepemp"] = dt1;
                this.Data_Bind();

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please Select Employee First!" + "');", true);
            }
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblsepemp"];
            this.gvEmpResign.DataSource =dt;
            this.gvEmpResign.DataBind();

            DataTable dt1 = (DataTable)Session["tblSepType"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownList ddlgvSepType = ((DropDownList)this.gvEmpResign.Rows[i].FindControl("ddlgvSepType"));
                ddlgvSepType.DataTextField = "hrgdesc";
                ddlgvSepType.DataValueField = "hrgcod";
                ddlgvSepType.DataSource = dt1;
                ddlgvSepType.DataBind();
                ddlgvSepType.SelectedValue = this.ddlSepType.SelectedValue.ToString();
            }
        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblsepemp"];
            int row = 0;
            foreach (GridViewRow gvr in gvEmpResign.Rows)
            {
                string sepDate = ((TextBox)gvr.FindControl("txtSepDate")).Text.ToString();
                string sepCode = ((DropDownList)gvr.FindControl("ddlgvSepType")).SelectedValue.ToString();
                dt.Rows[row]["sepdate"] = sepDate;
                dt.Rows[row]["sepcode"] = sepCode;
                row++;

            }
            Session["tblsepemp"] = dt;

        }
        protected void lbtnDeleteEmp_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblsepemp"];
            int gvRowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int rowindex = (this.gvEmpResign.PageSize) * (this.gvEmpResign.PageIndex) + gvRowIndex;
            dt.Rows[rowindex].Delete();
            dt.AcceptChanges();
            this.Data_Bind();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Employee Deleted Successfully" + "');", true);
        }
    }
}