using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using RealERPLIB;
namespace RealERPWEB.F_81_Hrm.F_81_Rec
{
    public partial class RptEmpAssessment : System.Web.UI.Page
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

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string date = System.DateTime.Today.AddMonths(-2).ToString("dd-MMM-yyyy");
                this.txtfrmDate.Text = "01" + date.Substring(2);

                this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(3).AddDays(-1).ToString("dd-MMM-yyyy");

                // this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //this.lblfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE  ASSESSMENT REPORT";
                this.GetCompName();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetCompName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string txtCompany = this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompanyName.DataTextField = "actdesc";
            this.ddlCompanyName.DataValueField = "actcode";
            this.ddlCompanyName.DataSource = ds1.Tables[0];
            this.ddlCompanyName.DataBind();
            this.ddlCompanyName_OnSelectedIndexChanged(null, null);
        }

        private void GetDepartment()
        {
            if (this.ddlCompanyName.Items.Count == 0)
                return;
            string comcod = this.GetComeCode();
            string txtCompanyname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSearchDept = this.txtSrcDepartment.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
        }

        private void ShowEmpPerformance()
        {
            Session.Remove("tblper");
            string comcod = this.GetComeCode();
            string comcode = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString() + "%";

            string fromdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy"); // this.txtfrmDate.Text.Trim();
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");//this.txttoDate.Text.Trim();

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "ASSPOSITION", comcode, deptname, fromdate, todate, "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvEmpper.DataSource = null;
                this.gvEmpper.DataBind();
                return;
            }
            Session["tblper"] = ds2.Tables[1];

            this.Data_Bind();
        }

        private void Data_Bind()
        {
            //int i;
            //for (i = 3; i < 5; i++)
            //    this.gvEmpper.Columns[i].Visible = false;

            //DateTime datefrm = Convert.ToDateTime(this.txtfrmDate.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txttoDate.Text.Trim());
            //for (i = 3; i < 5; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    this.gvEmpper.Columns[i].Visible = true;
            //    this.gvEmpper.Columns[i].HeaderText = datefrm.ToString("MMM");
            //    datefrm = datefrm.AddMonths(1);

            //}

            DataTable dt = (DataTable)Session["tblper"];
            this.gvEmpper.DataSource = dt;
            this.gvEmpper.DataBind();
        }

        protected void ibtnFindDepartment_OnClick(object sender, EventArgs e)
        {
            this.GetCompName();
        }

        protected void ddlCompanyName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();

        }

        protected void lnkbtnShow_OnClick(object sender, EventArgs e)
        {
            this.ShowEmpPerformance();
        }

        protected void imgbtnDeptSrch_OnClick(object sender, EventArgs e)
        {
            this.GetDepartment();
        }

        protected void ddlDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            //string Company = (this.ddlCompanyName.SelectedItem.Text.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedItem.Text.ToString().Substring(0, 2) + "%";
            //string deptname = this.ddlDepartment.SelectedItem.Text.ToString().Substring(13);  //== "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString() + "%";
            string fromdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy"); // this.txtfrmDate.Text.Trim();
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy"); //this.txttoDate.Text.Trim();
            string FTdate = "( From " + fromdate + " To " + todate + " )";
            string deptname = "Employee Assessment";


            DataTable dt = (DataTable)Session["tblper"];
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_81_Rec.RptAssYearly>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_81_Rec.RptAssYearly", lst, null, null);
            //DateTime datefrm = Convert.ToDateTime(this.txtfrmDate.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txttoDate.Text.Trim());
            //string txtmark1 = "", txtmark2 = "", txtmark3 = "";
            //for (int i = 1; i <= 3; i++)
            //{
            //    if (datefrm > dateto)
            //        break;

            //    if (i == 1)
            //        txtmark1 = datefrm.ToString("MMM");

            //    else if (i == 2)
            //        txtmark2 = datefrm.ToString("MMM");
            //    if (i == 3)
            //        txtmark3 = datefrm.ToString("MMM");

            //    datefrm = datefrm.AddMonths(1);

            //}

            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("deptname", deptname));
            Rpt1.SetParameters(new ReportParameter("FTdate", FTdate));


            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}