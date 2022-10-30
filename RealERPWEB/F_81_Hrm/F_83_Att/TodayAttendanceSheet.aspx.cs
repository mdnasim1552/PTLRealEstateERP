using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace RealERPWEB.F_81_Hrm.F_83_Att
{
    public partial class TodayAttendanceSheet : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        Common compUtility = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = " Today Employee Attendance Information";
                // (Label)this.Master.FindControl("lblTitle").Text  = " Today Employee Attendance Information";
                this.GetCompany();
                //this.GetProjectName();
                this.SelectDate();
                this.lnkbtnShow_Click(null,null);
              //  this.GetEmpName();
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
            string comcod = this.GetComCode();
            DataSet datSetup = compUtility.GetCompUtility();
            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            if (datSetup == null)
                return;
            switch (comcod)
            {
                case "3330":
                case "3355":
                case "3365":
                    this.txtfromdate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                   
                    break;

                default:
                    this.txtfromdate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                   
                    break;
            }
        }

        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string txtCompany = "%%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            if (ds5 == null)
                return;
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds5.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds5.Tables[0];
            this.GetProjectName();
            //ds1.Dispose();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            string secid = dt1.Rows[0]["secid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["secid"].ToString() == secid)
                    dt1.Rows[j]["section"] = "";
                secid = dt1.Rows[j]["secid"].ToString();
            }

            return dt1;


        }

        private void GetProjectName()
        {
            string comcod = this.GetComCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            //tring Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string txtSProject = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            //this.ddlProjectName_SelectedIndexChanged(null, null);
           this.GetSectionName();
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private string Calltype()
        {
            string comcod = this.GetComCode();
            string calltype = "";
            switch (comcod)
            {
                case "3347":
                    calltype = "SECTIONNAMEDP01";
                    break;
                default:
                    calltype = "SECTIONNAMEDP";
                    break;
            }
            return calltype;
        }
        private void GetSectionName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Company = ((this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string Department = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9)) + "%";
            string txtSSec = "%%";
            string calltype = this.Calltype();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", calltype, Company, Department, txtSSec, userid, "", "", "", "", "");
            this.DropCheck1.DataTextField = "sectionname";
            this.DropCheck1.DataValueField = "sectionname";
            this.DropCheck1.DataSource = ds2.Tables[0];
            this.DropCheck1.DataBind();
            DropCheck1_SelectedIndexChanged(null, null);
        }

        //private void GetEmpName()
        //{
        //    string comcod = this.GetComCode();
        //    string company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
        //    string projectName = ((ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : ddlProjectName.SelectedValue.ToString().Substring(0, 8)) + "%";

        //    string txtSEmployee = "%%";
        //    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETEMPNAME", company, projectName, txtSEmployee, "", "", "", "", "", "");
        //    if (ds3 == null)
        //        return;
            
          
        //}
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();

        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionName();
         //   lnkbtnEmp_Click(null, null);

        }

        //protected void lnkbtnEmp_Click(object sender, EventArgs e)
        //{
        //    this.GetEmpName();
        //}

        protected void DropCheck1_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  this.GetEmpName();
        }

        private string GetComLateAccTime()
        {
            string comcod = this.GetComCode();
            string acclate = "";
            switch (comcod)
            {
                case "3336":
                    acclate = "acclate";
                    break;

                default:
                    break;
            }

            return acclate;
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetComCode();         

            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";            
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string deptCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";

            string Actime = this.GetComLateAccTime();

            string section = "";
             if ((this.ddlProjectName.SelectedValue.ToString() != "000000000000"))
            {
                string gp = this.DropCheck1.SelectedValue.Trim();
                if (gp.Length > 0)
                {
                    if (gp.Substring(0, 3).Trim() == "000" || gp.Trim() == "")
                        section = "";
                    else
                        foreach (ListItem s1 in DropCheck1.Items)
                        {
                            if (s1.Selected)
                            {
                                section = section + this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + s1.Value.Substring(0, 3);
                            }
                        }
                }
            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPDAILYATTN", frmdate, deptCode, Company, section, Actime, "", "", "", "");
            if (ds1 == null)
                return;

   
             DataTable dt = this.HiddenSameData(ds1.Tables[0]);
            Session["tblallData"] = dt;
            this.DataBind();
        }

        private void DataBind()
        {
          
            DataTable dt = (DataTable)Session["tblallData"];
            this.gvdailyatt.DataSource = dt;
            this.gvdailyatt.DataBind();

        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetComCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string deptCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";
            string Actime = this.GetComLateAccTime();
            string section = "";
            if ((this.ddlProjectName.SelectedValue.ToString() != "000000000000"))
            {
                string gp = this.DropCheck1.SelectedValue.Trim();
                if (gp.Length > 0)
                {
                    if (gp.Substring(0, 3).Trim() == "000" || gp.Trim() == "")
                        section = "";
                    else
                        foreach (ListItem s1 in DropCheck1.Items)
                        {
                            if (s1.Selected)
                            {
                                section = section + this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + s1.Value.Substring(0, 3);
                            }
                        }
                }
            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPDAILYATTN", frmdate, deptCode, Company, section, Actime, "", "", "", "");
            if (ds1 == null)
                return;

            var list = ds1.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.DailyAttenCHLGroupWize>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.RptDailyAllEmpAttn", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", PCompany));
            Rpt1.SetParameters(new ReportParameter("txtDate", Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd MMMM,yyyy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Daily Employee Attendance"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

       
    }
}