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
namespace RealERPWEB.F_81_Hrm.F_84_Lea
{
    public partial class RptYearlyLeaveRecord : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                //this.txtCurTransDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                //this.txtpatplacedate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Yearly Leave Record ";
                //this.Get_Trnsno();
                //this.tableintosession();



                this.GetCompany();

            }

        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetCompany()
        {
            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
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
            if (dt1.Rows.Count == 0)
                return dt1;

            string section;

            int j;

            section = dt1.Rows[0]["section"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["section"].ToString() == section)
                {
                    section = dt1.Rows[j]["section"].ToString();
                    dt1.Rows[j]["sectionname"] = "";
                }

                else
                {
                    section = dt1.Rows[j]["section"].ToString();
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

            Session["tbllvRecord"] = ds4.Tables[0];
            //Session["tblempdesig"] = HiddenSameData(ds4.Tables[0]);
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
            string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
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
            string txtSSec = "%" + this.txtSrcSec.Text.Trim() + "%";
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
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);




            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

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