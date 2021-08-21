using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using RealERPLIB;
namespace RealERPWEB.F_81_Hrm.F_85_Lon
{
    public partial class EmpLoanStatusOpn : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission (HttpContext.Current.Request.Url.AbsoluteUri.ToString (), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect ("../../AcceessError.aspx");
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDate.Text = "01" + date.Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE LOAN STATUS With Opening";
                this.GetCompName();
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.GetDepartment();
                this.lnkbtnShow_Click(null, null);

            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

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
            string comcod = hst["comcod"].ToString();
            string txtDepartment = "%" + this.txtSrcDept.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME", txtDepartment, userid, "", "", "", "", "", "", "");
            this.ddlDeptName.DataTextField = "actdesc";
            this.ddlDeptName.DataValueField = "actcode";
            this.ddlDeptName.DataSource = ds1.Tables[0];
            this.ddlDeptName.DataBind();
        }
        protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        private void GetDepartment()
        {

            string comcod = this.GetComeCode();
            string txtDepartment = this.ddlDeptName.SelectedValue.ToString().Substring(0, 2) + "%";
            string dept = "%" + this.txtSrcDepartment.Text + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", txtDepartment, dept, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();

        }

        protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
        }




        protected void ibtnFindDepartment_Click(object sender, EventArgs e)
        {
            //this.GetDepartment();
            this.GetCompName();

        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.empLoanStatus();
            //this.lblmsg.Text = "";
        }


        private void empLoanStatus()
        {
            Session.Remove("tbloan");
            string comcod = this.GetComeCode();
            string comnam = this.ddlDeptName.SelectedValue.Substring(0, 2).ToString();
            string deptname = this.ddlDepartment.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "EMPLOANSTATUSOPN", frmdate, todate, deptname, comnam, "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvEmpLoanStatus.DataSource = null;
                this.gvEmpLoanStatus.DataBind();
                return;
            }
            Session["tbloan"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }


        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tbloan"];

            this.gvEmpLoanStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvEmpLoanStatus.DataSource = dt;
            this.gvEmpLoanStatus.DataBind();
            this.FooterCalculation();



        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tbloan"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblgvFLoanamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnln)", "")) ? 0.00
                    : dt.Compute("sum(opnln)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblgvFcrlnamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(crntln)", "")) ? 0.00
                   : dt.Compute("sum(crntln)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblgvFlnPaidamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(crlnins)", "")) ? 0.00
                   : dt.Compute("sum(crlnins)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblgvFbalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bal)", "")) ? 0.00
                    : dt.Compute("sum(bal)", ""))).ToString("#,##0;(#,##0); ");

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbloan"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string companyname = ddlDeptName.SelectedItem.Text.Trim().Substring(13);
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");


            ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_85_Lon.rptEmpLoanStatus();
            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = companyname;
            TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["Asdate"] as TextObject;
            txtccaret.Text = "Date: " + frmdate;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rpcp.SetDataSource(dt);
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid;

            int j;

            secid = dt1.Rows[0]["section"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["section"].ToString() == secid)
                {
                    secid = dt1.Rows[j]["section"].ToString();
                    dt1.Rows[j]["secdesc"] = "";
                }

                else
                {
                    secid = dt1.Rows[j]["section"].ToString();
                }

            }


            return dt1;

        }
    }
}