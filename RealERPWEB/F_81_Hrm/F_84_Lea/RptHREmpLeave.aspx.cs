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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_81_Hrm.F_84_Lea
{
    public partial class RptHREmpLeave : System.Web.UI.Page
    {
        Common compUtility = new Common();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.GetDate();
                this.ShowView();
                this.GetCompanyName();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Individual Employee LEAVE STATUS ";

            }

        }

        private void GetDate()
        {
            string comcod = this.GetComeCode();

            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
                return;
            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "26" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);

            //this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
            //this.txtfromdate.Text = startdate + this.txtfromdate.Text.Trim().Substring(2);
            //this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

            switch (comcod)
            {
                case "3330":
                case "3355":
                case "3365":
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = startdate + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;

                default:
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = startdate + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetCompanyName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();


            string txtCompany = "%%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");

            this.ddlCompanyAgg.DataTextField = "actdesc";
            this.ddlCompanyAgg.DataValueField = "actcode";
            this.ddlCompanyAgg.DataSource = ds5.Tables[0];
            this.ddlCompanyAgg.DataBind();
            this.GetDepartment();
            this.ddlCompanyAgg_SelectedIndexChanged(null, null);
        }
        private void GetDepartment()
        {
            string comcod = this.GetComeCode();
            //   string type = this.Request.QueryString["Type"].ToString().Trim();
            string Company = ((this.ddlCompanyAgg.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, 2)) + "%";

            string txtSProject = this.txtsrchdeptagg.Text.Trim() + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEPTNAME", Company, txtSProject, "", "", "", "", "", "", "");

            this.ddldepartmentagg.DataTextField = "deptdesc";
            this.ddldepartmentagg.DataValueField = "deptcode";
            this.ddldepartmentagg.DataSource = ds4.Tables[0];
            this.ddldepartmentagg.DataBind();
            this.GetProjectName();
        }

        private void GetProjectName()
        {
            string comcod = this.GetComeCode();
            string deptcode = this.ddldepartmentagg.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSProject = this.txtSrcPro.Text.Trim() + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAME", deptcode, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds4.Tables[0];
            this.ddlProjectName.DataBind();
            this.GetEmpName();
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetEmpName()
        {
            string comcod = this.GetComeCode();
            string ProjectCode = (this.txtSrcEmployee.Text.Trim().Length > 0) ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string txtSProject = "%" + this.txtSrcEmployee.Text + "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPREMPNAME", ProjectCode, txtSProject, "", "", "", "", "", "", "");
            this.ddlEmployee.DataTextField = "empname";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = ds5.Tables[0];
            this.ddlEmployee.DataBind();
            ViewState["tblemp"] = ds5.Tables[0];
            this.GetComASecSelected();
        }

        private void GetComASecSelected()
        {
            string empid = this.ddlEmployee.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblemp"];
            DataRow[] dr = dt.Select("empid = '" + empid + "'");
            if (dr.Length > 0)
            {
                this.ddlCompanyAgg.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["companycode"].ToString();
                this.ddldepartmentagg.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["deptcode"].ToString();
                this.ddlProjectName.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["refno"].ToString();
            }

        }

        protected void ibtnFindCompanyAgg_Click(object sender, EventArgs e)
        {
            this.GetCompanyName();
        }

        protected void ddlCompanyAgg_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();

        }

        protected void lbtndeptagg_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        protected void ddldepartmentagg_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {

            //this.ddlProjectName_SelectedIndexChanged(null, null);
            this.GetProjectName();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmpName();

        }
        protected void ibtnEmpListAllinfo_Click(object sender, EventArgs e)
        {
            this.GetEmpName();
        }
        protected void ddlEmpNameAllInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetComASecSelected();
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        //private void GetEmployee()
        //{


        //    string comcod = this.GetCompCode();
        //    string txtSProject = "%" + this.txtSrcEmployee.Text + "%";
        //    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "GETEMPNAME", txtSProject, "", "", "", "", "", "", "", "");
        //    this.ddlEmployee.DataTextField = "empname";
        //    this.ddlEmployee.DataValueField = "empid";
        //    this.ddlEmployee.DataSource = ds3.Tables[0];
        //    this.ddlEmployee.DataBind();
        //    ds3.Dispose();



        //}


        private void ShowView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "EmpLeaveSt":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

            }

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.SelectIndex();
        }
        private void SelectIndex()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "EmpLeaveSt":
                    this.ShowLeaveStatus();

                    break;

            }


        }



        private void ShowLeaveStatus()
        {
            this.lblleavesummary.Visible = true;
            this.lblleavesDetails.Visible = true;
            ViewState.Remove("tblleave");
            string comcod = this.GetCompCode();
            string Empid = this.ddlEmployee.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "LEAVESTATUS02", Empid, frmdate, todate, "", "", "", "", "", "");


            if (ds3 == null)
            {
                this.gvLeaveStatus.DataSource = null;
                this.gvLeaveStatus.DataBind();
                this.gvLeavedetails.DataSource = null;
                this.gvLeavedetails.DataBind();
                return;

            }


            ViewState["tblleave"] = ds3.Tables[0];
            this.Data_Bind();

        }


        private void Data_Bind()
        {
            DataTable dt = ((DataTable)ViewState["tblleave"]).Copy();
            DataTable dt1 = new DataTable();
            DataView dvr = new DataView();





            //A. Sales
            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'A'");
            dt1 = dvr.ToTable();
            this.gvLeaveStatus.DataSource = dt1;
            this.gvLeaveStatus.DataBind();
            //this.FooterCalculation(dt1, "gvLeaveStatus");   

            //B. Collection Summary
            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'B'");
            dt1 = dvr.ToTable();
            this.gvLeavedetails.DataSource = dt1;
            this.gvLeavedetails.DataBind();
            //  this.FooterCalculation(dt1, "gvLeavedetails"); 
            //C. Cheque In Hand




        }


        private void FooterCalculation(DataTable dt, string grview)
        {

            if (dt.Rows.Count == 0)
                return;

            switch (grview)
            {
                case "gvLeaveStatus":
                    ((Label)this.gvLeaveStatus.FooterRow.FindControl("lblgvFOpening")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opleave)", "")) ? 0.00
                    : dt.Compute("sum(opleave)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLeaveStatus.FooterRow.FindControl("lblgvFleaveentitled")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(enleave)", "")) ? 0.00
                          : dt.Compute("sum(enleave)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLeaveStatus.FooterRow.FindControl("lblgvFleaveenjoy")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(enjleave)", "")) ? 0.00
                          : dt.Compute("sum(enjleave)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLeaveStatus.FooterRow.FindControl("lblgvFleavebal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balleave)", "")) ? 0.00
                          : dt.Compute("sum(balleave)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "gvLeavedetails":
                    ((Label)this.gvLeavedetails.FooterRow.FindControl("lblgvFleavedays")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lvday)", "")) ? 0.00
                         : dt.Compute("sum(lvday)", ""))).ToString("#,##0;(#,##0); ");
                    break;



            }


        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "EmpLeaveSt":
                    this.PrintEmpLvStatus();
                    break;


            }


        }

        private void PrintEmpLvStatus()
        {



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMM, yyyy");
            string subtitle = "Period: " + this.txtfromdate.Text + " To " + this.txttodate.Text;
            string userinf = ASTUtility.Concat(comname, username, session, printdate);

            DataTable dt = (DataTable)ViewState["tblleave"];
            string empid = this.ddlEmployee.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "GETEMPDETAILSINFO", empid, todate, "", "", "", "", "", "", "");
            DataTable dt1 = ds.Tables[0];




            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptEmpLeavStatus>();
            // var list2 = dt1.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptEmpInfoData>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_81_Hrm.R_84_Lea.RptEmpLeavStatus", list, null, null);


            Rpt1.SetParameters(new ReportParameter("comnam", comname));
            Rpt1.SetParameters(new ReportParameter("subtitle", subtitle));
            Rpt1.SetParameters(new ReportParameter("userinf", userinf));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));

            string jondate = Convert.ToDateTime(dt1.Rows[0]["joindate"]).ToString("dd-MMM-yyyy");

            string lstrtdat = Convert.ToDateTime(dt1.Rows[0]["lstrtdat"]).ToString("dd-MMM-yyyy");

            Rpt1.SetParameters(new ReportParameter("idcardno", dt1.Rows[0]["idcardno"].ToString()));
            Rpt1.SetParameters(new ReportParameter("companyname", dt1.Rows[0]["companyname"].ToString()));
            Rpt1.SetParameters(new ReportParameter("empname", dt1.Rows[0]["empname"].ToString()));
            Rpt1.SetParameters(new ReportParameter("desig", dt1.Rows[0]["desig"].ToString()));
            Rpt1.SetParameters(new ReportParameter("section", dt1.Rows[0]["section"].ToString()));
            Rpt1.SetParameters(new ReportParameter("joindate", jondate));
            Rpt1.SetParameters(new ReportParameter("serlength", dt1.Rows[0]["serlength"].ToString()));
            Rpt1.SetParameters(new ReportParameter("lstrtdat", lstrtdat));





            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            /////crystal
            //ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_84_Lea.RptEmpLeavStatus();
            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //CompName.Text = dt1.Rows[0]["companyname"].ToString();
            //TextObject txtaddress = rpcp.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            //txtaddress.Text = comadd;
            //TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtccaret.Text = "Period: "+this.txtfromdate.Text+" To "+this.txttodate.Text;
            //TextObject txtcardno = rpcp.ReportDefinition.ReportObjects["txtcardno"] as TextObject;
            //txtcardno.Text = dt1.Rows[0]["idcardno"].ToString();
            //TextObject txtEmpName = rpcp.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            //txtEmpName.Text =dt1.Rows[0]["empname"].ToString();
            //TextObject txtDesig = rpcp.ReportDefinition.ReportObjects["txtDesig"] as TextObject;
            //txtDesig.Text = dt1.Rows[0]["desig"].ToString();

            //TextObject txtDepartment = rpcp.ReportDefinition.ReportObjects["txtDepartment"] as TextObject;
            //txtDepartment.Text = dt1.Rows[0]["section"].ToString();

            //TextObject txtjoindate = rpcp.ReportDefinition.ReportObjects["txtjoindate"] as TextObject;
            //txtjoindate.Text = Convert.ToDateTime(dt1.Rows[0]["joindate"]).ToString("dd-MMM-yyyy");

            //TextObject txtprobitionperiod = rpcp.ReportDefinition.ReportObjects["txtservicelength"] as TextObject;
            //txtprobitionperiod.Text = dt1.Rows[0]["serlength"].ToString();
            ////TextObject txtcondate = rpcp.ReportDefinition.ReportObjects["txtcondate"] as TextObject;
            ////txtcondate.Text = (Convert.ToDateTime(dt1.Rows[0]["condate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(dt1.Rows[0]["condate"]).ToString("dd-MMM-yyyy");
            ////TextObject txtsalary = rpcp.ReportDefinition.ReportObjects["txtsalary"] as TextObject;
            ////txtsalary.Text = Convert.ToDouble(dt1.Rows[0]["gssal"]).ToString("#,##0;(#,##0); ");
            //TextObject txtllenjoydate = rpcp.ReportDefinition.ReportObjects["txtllenjoydate"] as TextObject;
            //txtllenjoydate.Text = (Convert.ToDateTime(dt1.Rows[0]["lstrtdat"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(dt1.Rows[0]["lstrtdat"]).ToString("dd-MMM-yyyy"); ;

            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rpcp.SetDataSource(dt);
            //Session["Report1"] = rpcp;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



        protected void gvLeaveStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                Label Description = (Label)e.Row.FindControl("lblgvDescription");
                Label opnleave = (Label)e.Row.FindControl("lblgvopnleave");
                Label leaveentitled = (Label)e.Row.FindControl("lblgvleaveentitled");
                Label leaveenjoy = (Label)e.Row.FindControl("lblgvleaveenjoy");
                Label leavebal = (Label)e.Row.FindControl("lblgvleavebal");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();
                if (code == "")
                {
                    return;
                }
                if (code == "51AAA")
                {


                    Description.Font.Bold = true;
                    opnleave.Font.Bold = true;
                    leaveentitled.Font.Bold = true;
                    leaveenjoy.Font.Bold = true;
                    leavebal.Font.Bold = true;
                    Description.Style.Add("text-align", "right");
                }

            }

        }
        protected void gvLeavedetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                Label Description = (Label)e.Row.FindControl("lblgvDescriptionld");

                Label leavedays = (Label)e.Row.FindControl("lblgvleavedays");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();
                if (code == "")
                {
                    return;
                }
                if (code == "51AAA")
                {


                    Description.Font.Bold = true;
                    leavedays.Font.Bold = true;
                    Description.Style.Add("text-align", "right");
                }

            }

        }
        protected void imgbtnEmployee_Click(object sender, EventArgs e)
        {
            this.GetEmpName();
        }
    }
}
