using RealERPLIB;
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
              //  this.GetEmpName();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
           // ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
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
            Session["tblallData"] = ds1.Tables[0];
            this.DataBind();
        }

        private void DataBind()
        {
          
            DataTable dt = (DataTable)Session["tblallData"];
            this.gvdailyatt.DataSource = dt;
            this.gvdailyatt.DataBind();

        }

        //protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        //protected void gvdailyatt_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    GridViewRow gvRow = e.Row;
        //    if (gvRow.RowType == DataControlRowType.Header)
        //    {

        //        GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

        //        TableCell cell01 = new TableCell();
        //        cell01.Text = "Sl";
        //        cell01.HorizontalAlign = HorizontalAlign.Center;
        //        cell01.RowSpan = 2;
        //        gvrow.Cells.Add(cell01);


        //        TableCell cell02 = new TableCell();
        //        cell02.Text = "Emp ID";
        //        cell02.HorizontalAlign = HorizontalAlign.Center;
        //        cell02.RowSpan = 2;
        //        gvrow.Cells.Add(cell02);

        //        TableCell cell03 = new TableCell();
        //        cell03.Text = "Card No";
        //        cell03.HorizontalAlign = HorizontalAlign.Center;
        //        cell03.RowSpan = 2;
        //        gvrow.Cells.Add(cell03);


        //        TableCell cell04 = new TableCell();
        //        cell04.Text = "Name";
        //        cell04.HorizontalAlign = HorizontalAlign.Center;
        //        cell04.RowSpan = 2;
        //        gvrow.Cells.Add(cell04);


        //        TableCell cell05 = new TableCell();
        //        cell05.Text = "Designation";
        //        cell05.HorizontalAlign = HorizontalAlign.Center;
        //        cell05.RowSpan = 2;
        //        gvrow.Cells.Add(cell05);

        //        TableCell cell06 = new TableCell();
        //        cell06.Text = "Office Time";
        //        cell06.HorizontalAlign = HorizontalAlign.Center;
        //        cell06.Attributes["style"] = "font-weight:bold;";
        //        cell06.ColumnSpan = 2;
        //        gvrow.Cells.Add(cell06);


        //        TableCell cell07 = new TableCell();
        //        cell07.Text = "Actual Time";
        //        cell07.HorizontalAlign = HorizontalAlign.Center;
        //        cell07.Attributes["style"] = "font-weight:bold;";
        //        cell07.ColumnSpan = 2;
        //        gvrow.Cells.Add(cell07);

        //        TableCell cell08 = new TableCell();
        //        cell08.Text = "Late";
        //        cell08.HorizontalAlign = HorizontalAlign.Center;
        //        cell08.RowSpan = 2;
        //        gvrow.Cells.Add(cell08);


        //        TableCell cell09 = new TableCell();
        //        cell09.Text = "Early Leave";
        //        cell09.HorizontalAlign = HorizontalAlign.Center;
        //        cell09.RowSpan = 2;
        //        gvrow.Cells.Add(cell09);

        //        TableCell cell10 = new TableCell();
        //        cell10.Text = "Absent";
        //        cell10.HorizontalAlign = HorizontalAlign.Center;
        //        cell10.RowSpan = 2;
        //        gvrow.Cells.Add(cell10);

        //        gvdailyatt.Controls[0].Controls.AddAt(0, gvrow);
        //    }

        //    if (e.Row.RowType == DataControlRowType.Header)
        //    {
        //        e.Row.Cells[0].Visible = false;
        //        e.Row.Cells[1].Visible = false;
        //        e.Row.Cells[2].Visible = false;
        //        e.Row.Cells[3].Visible = false;
        //        e.Row.Cells[4].Visible = false;
        //        e.Row.Cells[09].Visible = false;
        //        e.Row.Cells[10].Visible = false;
        //        e.Row.Cells[11].Visible = false;

        //    }
        //}
    }
}