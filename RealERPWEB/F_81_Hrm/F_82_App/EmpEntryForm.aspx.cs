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
namespace RealERPWEB.F_81_Hrm.F_82_App
{
    public partial class EmpEntryForm : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee Name Entry";
                this.MultiView1.ActiveViewIndex = 0;
                this.GetCompany();
                this.chkNewEmp.Checked = true;
                this.chkNewEmp_CheckedChanged(null, null);
                // this.getLastEmpid();

            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetEmpName()
        {

            //string comcod = this.GetComeCode();
            //string txtSProject =(this.Request.QueryString["Type"].ToString().Trim()=="Services")?("%" + this.txtEmpSrc.Text.Trim()+ "%"):("%" + this.txtEmpSrcInfo.Text.Trim()+"%");
            //string date = this.txtDate.Text.Trim();
            //DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETEMPTNAME", txtSProject, date, "", "", "", "", "", "", "");

            //if (this.Request.QueryString["Type"].ToString().Trim() == "Services")
            //{
            //    this.ddlEmpName.DataTextField = "empname";
            //    this.ddlEmpName.DataValueField = "empid";
            //    this.ddlEmpName.DataSource = ds3.Tables[0];
            //    this.ddlEmpName.DataBind();
            //}
            //else 
            //{
            //    this.ddlEmpNameAllInfo.DataTextField = "empname";
            //    this.ddlEmpNameAllInfo.DataValueField = "empid";
            //    this.ddlEmpNameAllInfo.DataSource = ds3.Tables[0];
            //    this.ddlEmpNameAllInfo.DataBind();

            //}

        }



        private void GetCompany()
        {
            string comcod = this.GetComeCode();
            string txtCompany = this.txtComSrc.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCOMPANY", txtCompany, "", "", "", "", "", "", "", "");
            this.ddlCompName.DataTextField = "sirdesc";
            this.ddlCompName.DataValueField = "sircode";
            this.ddlCompName.DataSource = ds1.Tables[0];
            this.ddlCompName.DataBind();
            ds1.Dispose();
            this.ingbtnLoc_Click(null, null);

        }

        //protected void imgbtnCompany_Click(object sender, EventArgs e)
        //{
        //    this.GetCompany();
        //}

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        private void PrintDyInfo()
        {

            // Hashtable hst = (Hashtable)Session["tblLogin"];
            // string comnam = hst["comnam"].ToString();
            // string comadd = hst["comadd1"].ToString();
            // string compname = hst["compname"].ToString();
            // string username = hst["username"].ToString();
            // this.printSearch();
            // string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            // DataTable dt = (DataTable)ViewState["tblRptservices"];
            // string date = Convert.ToDateTime(this.txtDate.Text).ToString("MMMM dd, yyyy");
            // ReportDocument rptempdyinfo = new RealERPRPT.R_81_Hrm.R_82_App.RptDynamicInfo();
            // TextObject txtComName = rptempdyinfo.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            // txtComName.Text = this.ddlCompany.SelectedItem.Text.Trim() ;
            // TextObject txtuserinfo = rptempdyinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            // txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            // rptempdyinfo.SetDataSource(dt);
            //// string comcod = this.GetComeCode();
            // string comcod = hst["comcod"].ToString();
            // string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            // rptempdyinfo.SetParameterValue("ComLogo", ComLogo);
            // Session["Report1"] = rptempdyinfo;
            // lblprint.Text = "<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //               this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>"; 


        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            //ViewState.Remove("tblservices");
            //string comcod = this.GetComeCode();
            //string empid = this.ddlEmpName.SelectedValue.ToString();
            //string Date = this.txtDate.Text.Trim();
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPSERVICES", empid, Date, "", "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.gvempservices.DataSource = null;
            //    this.gvempservices.DataBind();
            //    return;
            //}
            //ViewState["tblservices"]=ds1.Tables[0];
            //    this.Data_Bind();
        }
        // img btn click 
        protected void imgbtnComp_Click(object sender, EventArgs e)
        {
            this.GetCompany();

        }
        protected void ingbtnLoc_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string txtLocSrc = "%" + this.txtLocSrc.Text.Trim() + "%";
            string company = this.ddlCompName.SelectedValue.ToString().Trim().Substring(0, 2);
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETLOCATION", txtLocSrc, company, "", "", "", "", "", "", "");
            this.ddlLocation.DataTextField = "sirdesc";
            this.ddlLocation.DataValueField = "sircode";
            this.ddlLocation.DataSource = ds2.Tables[0];
            this.ddlLocation.DataBind();
            ds2.Dispose();
            this.imgbtnBra_Click(null, null);
        }
        protected void imgbtnBra_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string txtBranch = "%" + this.txtBraSrc.Text.Trim() + "%";
            string location = this.ddlLocation.SelectedValue.ToString().Trim().Substring(0, 4);
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETBRANCH", txtBranch, location, "", "", "", "", "", "", "");
            this.ddlBranch.DataTextField = "sirdesc";
            this.ddlBranch.DataValueField = "sircode";
            this.ddlBranch.DataSource = ds3.Tables[0];
            this.ddlBranch.DataBind();
            ds3.Dispose();
            this.imgbtnDept_Click(null, null);
        }
        protected void imgbtnDept_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string txtDept = "%" + this.txtDptSrc.Text.Trim() + "%";
            string Branch = this.ddlBranch.SelectedValue.ToString().Trim().Substring(0, 7);
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPT", txtDept, Branch, "", "", "", "", "", "", "");
            this.ddlDept.DataTextField = "sirdesc";
            this.ddlDept.DataValueField = "sircode";
            this.ddlDept.DataSource = ds4.Tables[0];
            this.ddlDept.DataBind();
            ds4.Dispose();
            this.ddlEmpList.Items.Clear();
            this.txtEmpName.Text = "";

        }
        protected void mgbtnPreEMP_Click(object sender, EventArgs e)
        {

            if (this.chkNewEmp.Checked)
                return;
            string comcod = this.GetComeCode();
            string txtDept = "%" + this.txtEmpSrc.Text.Trim() + "%";
            string dept = this.ddlDept.SelectedValue.ToString().Trim().Substring(0, 9);
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPLIST", txtDept, dept, "", "", "", "", "", "", "");
            this.ddlEmpList.DataTextField = "sirdesc";
            this.ddlEmpList.DataValueField = "sircode";
            this.ddlEmpList.DataSource = ds5.Tables[0];
            this.ddlEmpList.DataBind();
            ds5.Dispose();
            this.ddlEmpList_SelectedIndexChanged(null, null);

        }
        //  selected index change 
        protected void ddlCompName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ingbtnLoc_Click(null, null);
        }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.imgbtnBra_Click(null, null);
        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.imgbtnDept_Click(null, null);
            //this.ddlEmpList.Items.Clear();
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlEmpList.Items.Clear();
            this.txtEmpName.Text = "";
        }
        protected void ddlEmpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmpList.Items.Count == 0)
                return;
            this.txtEmpName.Text = this.ddlEmpList.SelectedItem.Text.Trim().ToString();

        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetComeCode();
            string empdept = this.ddlDept.SelectedValue.ToString().Trim().Substring(0, 9);
            string empname = this.txtEmpName.Text;
            bool result = true;
            if (this.txtEmpName.Text.Length < 1)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Employee name can't be empty!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            if (this.ddlEmpList.Items.Count > 0)
            {
                string empcode = this.ddlEmpList.SelectedValue.ToString();
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "UPDATEEMPNAME", empcode, empname, "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTEMPNAME", empdept, empname, "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            if (result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                this.txtEmpName.Text = "";
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Sorry, Data Updated Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                //this.lblmsg.ForeColor
            }





        }

        private void getLastEmpid()
        {
            string comcod = this.GetComeCode();
            string compny = ASTUtility.Left(ddlCompName.SelectedValue, 2);
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "LASTEMPID", compny, "", "", "", "", "", "", "", "");
            this.lblEmplastId.Text = ds1.Tables[0].Rows[0]["lastempid"].ToString();


        }

        protected void chkNewEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkNewEmp.Checked)
            {
                this.ddlEmpList.Items.Clear();
                this.txtEmpName.Text = "";
            }

        }


        protected void lnkNextbtn_Click(object sender, EventArgs e)
        {
            this.getLastEmpid();
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('EmpEntry01.aspx?Type=Entry&empid=" + this.lblEmplastId.Text + "', target='_self');</script>";
        }
    }
}