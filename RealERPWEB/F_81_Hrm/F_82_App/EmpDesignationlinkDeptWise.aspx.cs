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
    public partial class EmpDesignationlinkDeptWise : System.Web.UI.Page
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
            DataSet ds4 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLOYEEDESIGSERIAL", Company, projectcode, section, "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvEmpListslno.DataSource = null;
                this.gvEmpListslno.DataBind();
                return;
            }

            Session["tblempdesig"] = ds4.Tables[0];
            //Session["tblempdesig"] = HiddenSameData(ds4.Tables[0]);
            this.grvacc_DataBind();


        }



        protected void grvacc_DataBind()
        {

            this.gvEmpListslno.DataSource = (DataTable)Session["tblempdesig"];
            this.gvEmpListslno.DataBind();
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

            DataTable dt = (DataTable)Session["tblempdesig"];
            int rowindex;
            for (int i = 0; i < this.gvEmpListslno.Rows.Count; i++)
            {

                double slno = Convert.ToDouble("0" + ((TextBox)this.gvEmpListslno.Rows[i].FindControl("txtslno")).Text.Trim());
                rowindex = (this.gvEmpListslno.PageSize) * (this.gvEmpListslno.PageIndex) + i;
                dt.Rows[rowindex]["slno"] = slno;




            }

            Session["tblempdesig"] = dt;



        }

        protected void lbntUpdateSl_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            try
            {
                this.SaveValue();
                string comcod = this.GetCompCode();
                DataTable dt = (DataTable)Session["tblempdesig"];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // string section = dt.Rows[i]["section"].ToString();
                    string empid = dt.Rows[i]["empid"].ToString();
                    int slno = Convert.ToInt32(dt.Rows[i]["slno"]);
                    if (slno > 0)
                    {

                        bool result = purData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEEMPDESIGSERIAL", empid, slno.ToString(), "", "", "", "", "", "", "", "", "", "", "", "", "");

                        if (!result)
                            return;

                    }


                }
                //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                string Msg = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Msg + "');", true);




            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + ex.Message + "');", true);

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

        protected void gvEmpListslno_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpListslno.PageIndex = e.NewPageIndex;
            this.ShowAllEmp();
        }
    }
}