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

namespace RealERPWEB.F_81_Hrm.F_84_Lea
{
    public partial class RptYearlyLeaveRecord : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.Get_Trnsno();
                //this.tableintosession();



                this.GetCompany();

            }

        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
           ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string year = this.ddlyear.SelectedValue.ToString();


            DataTable dt2 = (DataTable)Session["tbllvRecord"];
            //var lst = (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.YearlyLeaveRecord>)Session["tbllvRecord"];

            var lst = dt2.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.YearlyLeaveRecord>();


            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_84_Lea.RptYearlyLeaveRecord", lst, null, null);

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("RptHead", "Yearly Leave Record"));
            Rpt1.SetParameters(new ReportParameter("year", "Year : " + year));
            
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }









        private void GetCompany()
        {
            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];

            this.ddlCompany_SelectedIndexChanged(null, null);
            ds1.Dispose();

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.ShowAllEmp();


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            string companycode = dt1.Rows[0]["companycode"].ToString();
            string departcode = dt1.Rows[0]["departcode"].ToString();
            string secid = dt1.Rows[0]["secid"].ToString();


            //section = dt1.Rows[0]["section"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {

                if (dt1.Rows[j]["companycode"].ToString() == companycode &&  dt1.Rows[j]["departcode"].ToString() == departcode && dt1.Rows[j]["secid"].ToString() == secid)
                {
                    companycode = dt1.Rows[j]["companycode"].ToString();
                    departcode = dt1.Rows[j]["departcode"].ToString();
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["companyname"] = "";
                    dt1.Rows[j]["sectionname"] = "";
                    dt1.Rows[j]["deptdesc"] = "";

                }

                else if (dt1.Rows[j]["companycode"].ToString() == companycode && dt1.Rows[j]["departcode"].ToString() == departcode && dt1.Rows[j]["secid"].ToString() != secid)
                {
                    companycode = dt1.Rows[j]["companycode"].ToString();
                    departcode = dt1.Rows[j]["departcode"].ToString();
                    secid = dt1.Rows[j]["secid"].ToString();

                    dt1.Rows[j]["companyname"] = "";
                    dt1.Rows[j]["deptdesc"] = "";

                }
               
                else if (dt1.Rows[j]["departcode"].ToString() == departcode && dt1.Rows[j]["secid"].ToString() == secid)
                {
                    departcode = dt1.Rows[j]["departcode"].ToString();
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["sectionname"] = "";
                    dt1.Rows[j]["deptdesc"] = "";

                }
                else if ( dt1.Rows[j]["secid"].ToString() == secid)
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["sectionname"] = "";

                }
                else
                {
                    companycode = dt1.Rows[j]["companycode"].ToString();
                    departcode = dt1.Rows[j]["departcode"].ToString();
                    secid = dt1.Rows[j]["secid"].ToString();
                }
            }


            return dt1;

        }

            

      


        private void ShowAllEmp()
        {
            Session.Remove("tblempdesig");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string projectcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";//            
            string yearid = ddlyear.SelectedValue.ToString();
            DataSet ds4 = purData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "EMPLEAVERECORD", yearid,Company, projectcode, section, "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvEmplvStatus.DataSource = null;
                this.gvEmplvStatus.DataBind();
                return;
            }

             //Session["tbllvRecord"] = ds4.Tables[0];
            

            Session["tbllvRecord"] = HiddenSameData(ds4.Tables[0]);

            this.grvacc_DataBind();


        }



        protected void grvacc_DataBind()
        {

            this.gvEmplvStatus.DataSource = (DataTable)Session["tbllvRecord"];
            this.gvEmplvStatus.DataBind();
        }


        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;


            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string txtSProject = "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);

        }
        protected void GetSection()
        {
            string comcod = this.GetCompCode();
            string projectcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSSec = "%%";
            DataSet ds2 = purData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();

            // this.ddlprjlistfrom_SelectedIndexChanged(null, null);


        }

        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tbllvRecord"];
            int rowindex;
            for (int i = 0; i < this.gvEmplvStatus.Rows.Count; i++)
            {

                //ASTUtility.StrPosOrNagative(((TextBox)this.dgv1.Rows[i].FindControl("txtgvCrAmt")).Text.Trim());

                double txtallocation = ASTUtility.StrPosOrNagative(((TextBox)this.gvEmplvStatus.Rows[i].FindControl("txtallocation")).Text.Trim());
                double txtExercise = ASTUtility.StrPosOrNagative(((TextBox)this.gvEmplvStatus.Rows[i].FindControl("txtExercise")).Text.Trim());
                double txtbalance = ASTUtility.StrPosOrNagative(((TextBox)this.gvEmplvStatus.Rows[i].FindControl("txtbalance")).Text.Trim());
                double txtcarryfoward = ASTUtility.StrPosOrNagative(((TextBox)this.gvEmplvStatus.Rows[i].FindControl("txtcarryfoward")).Text.Trim());


                //Convert.ToDouble("0" + ((TextBox)this.gvEmplvStatus.Rows[i].FindControl("txtallocation")).Text.Trim());
               


                //double txtallocation = Convert.ToDouble("0" + ((TextBox)this.gvEmplvStatus.Rows[i].FindControl("txtallocation")).Text.Trim());
                //double txtExercise = Convert.ToDouble("0" + ((TextBox)this.gvEmplvStatus.Rows[i].FindControl("txtExercise")).Text.Trim());
                //double txtbalance = Convert.ToDouble("0" + ((TextBox)this.gvEmplvStatus.Rows[i].FindControl("txtbalance")).Text.Trim());
                //double txtcarryfoward = Convert.ToDouble("0" + ((TextBox)this.gvEmplvStatus.Rows[i].FindControl("txtcarryfoward")).Text.Trim());

                rowindex = (this.gvEmplvStatus.PageSize) * (this.gvEmplvStatus.PageIndex) + i;
                dt.Rows[rowindex]["earnlv"] = txtallocation;
                dt.Rows[rowindex]["enjoylv"] = txtExercise;
                dt.Rows[rowindex]["ballv"] = txtallocation- txtExercise;
                dt.Rows[rowindex]["carrfor"] = txtcarryfoward;

            }

            Session["tbllvRecord"] = dt;



        }

        protected void lbnLvRecordUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string msg = "";
            try
            {
                this.SaveValue();
                string comcod = this.GetCompCode();
                DataTable dt = (DataTable)Session["tbllvRecord"];
                string yearid = ddlyear.SelectedValue.ToString();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // string section = dt.Rows[i]["section"].ToString();
                    string empid = dt.Rows[i]["empid"].ToString();
                    double earnlv = Convert.ToDouble(dt.Rows[i]["earnlv"]);
                    double enjoylv = Convert.ToDouble(dt.Rows[i]["enjoylv"]);
                    double ballv = Convert.ToDouble(dt.Rows[i]["ballv"]);
                    double carrfor = Convert.ToDouble(dt.Rows[i]["carrfor"]);
                 

                    

                        bool result = purData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "INSERTORUPDATEYEARLYLV", yearid, empid, earnlv.ToString(), enjoylv.ToString(), carrfor.ToString(),"", "", "", "", "", "", "", "", "", "");

                        if (!result)
                            return;

                   


                }

                msg = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);





            }
            catch (Exception ex)
            {
                msg = "Update Failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
     

            }

        }

        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {

        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSection();

        }

        protected void btnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.grvacc_DataBind();
        }
    }
}