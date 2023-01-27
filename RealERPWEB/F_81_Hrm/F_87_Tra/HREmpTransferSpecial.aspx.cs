
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
namespace RealERPWEB.F_81_Hrm.F_87_Tra
{

    public partial class HREmpTransferSpecial : System.Web.UI.Page
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

                this.txtCurTransDate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                //this.txtpatplacedate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");

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

        //protected void GetPrvNm()
        //{

        //    string comcod = GetCompCode();
        //    //string mREQNO = "NEWISS";
        //    string mREQNO;
        //    if (this.ddlPrevISSList.Items.Count > 0)
        //        mREQNO = this.ddlPrevISSList.SelectedValue.ToString();
        //    string mREQDAT = this.GetStdDate(this.txtCurTransDate.Text);
        //    DataSet ds2 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTTRANSFETNO", mREQDAT,
        //           "", "", "", "", "", "", "", "");
        //    if (ds2 == null)
        //        return;
        //    if (ds2.Tables[0].Rows.Count > 0)
        //    {
        //        mREQNO = ds2.Tables[0].Rows[0]["maxtrnno"].ToString();
        //        this.lblCurTransNo1.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(0, 5);
        //        this.txtCurTransNo2.Text = ds2.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(6, 5);
        //        this.ddlPrevISSList.DataTextField = "maxtrnno1";
        //        this.ddlPrevISSList.DataValueField = "maxtrnno";
        //        this.ddlPrevISSList.DataSource = ds2.Tables[0];
        //        this.ddlPrevISSList.DataBind();
        //    }



        //}


        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            if (lbtnOk.Text.Trim() == "Ok")
            {
                lbtnOk.Text = "New";
                this.ddlCompany.Enabled = false;
                this.ddlProjectName.Enabled = false;
                this.ddlSection.Enabled = false;
                this.Get_Trnsno();
                this.ShowTranferEmp();
                return;

            }

            this.lbtnOk.Text = "Ok";
            this.ddlCompany.Enabled = true;
            this.ddlProjectName.Enabled = true;
            this.ddlSection.Enabled = true;
            this.pnlToCompany.Visible = false;

            this.gvEmpListTrans.DataSource = null;
            this.gvEmpListTrans.DataBind();

        }


        private void ShowTranferEmp()
        {
            Session.Remove("tbltrnasemp");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string projectcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";//            
            DataSet ds4 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETTRANSFERALLEMPLOYEE", Company, projectcode, section, "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvEmpListTrans.DataSource = null;
                this.gvEmpListTrans.DataBind();
                return;
            }

            Session["tbltrnasemp"] = ds4.Tables[0];
            //Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.grvacc_DataBind();


        }

        private void Get_Trnsno()
        {

            string comcod = this.GetCompCode();
            string date = this.GetStdDate(this.txtCurTransDate.Text);
            DataSet ds3 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LASTTRANSFETNO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            DataTable dt1 = ds3.Tables[0];
            this.txtCurTransDate.Text = Convert.ToDateTime(ds3.Tables[0].Rows[0]["maxtrndt"].ToString().Trim()).ToString("dd.MM.yyyy");
            this.lblCurTransNo1.Text = ds3.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(0, 5);
            this.txtCurTransNo2.Text = ds3.Tables[0].Rows[0]["maxtrnno1"].ToString().Substring(6);
        }

        protected void grvacc_DataBind()
        {
            DataTable dt = (DataTable)Session["tbltrnasemp"];
            this.gvEmpListTrans.DataSource = dt;
            this.gvEmpListTrans.DataBind();

            if(dt.Rows.Count>0)
            {
                Session["Report1"] = gvEmpListTrans;
                ((HyperLink)this.gvEmpListTrans.HeaderRow.FindControl("hlbtntbCdataExcelemplist")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
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

        private void GetToCompany()
        {

            DataTable dt = (DataTable)Session["tblcompany"];
            this.ddlToCompany.DataTextField = "actdesc";
            this.ddlToCompany.DataValueField = "actcode";
            this.ddlToCompany.DataSource = dt;
            this.ddlToCompany.DataBind();
            this.ddlToCompany_SelectedIndexChanged(null, null);

        }


        private void GetToProject()
        {
            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlToCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string txtSProject = "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETTOPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlToProject.DataTextField = "actdesc";
            this.ddlToProject.DataValueField = "actcode";
            this.ddlToProject.DataSource = ds1.Tables[0];
            this.ddlToProject.DataBind();
            ddlToProject_SelectedIndexChanged(null, null);
        }


        protected void GetToSection()
        {
            string comcod = this.GetCompCode();
            string projectcode = this.ddlToProject.SelectedValue.Substring(0, 9).ToString() + "%";
            string txtSSec = "%%";
            DataSet ds2 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "SECTIONNAMETO", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlToSection.DataTextField = "sectionname";
            this.ddlToSection.DataValueField = "section";
            this.ddlToSection.DataSource = ds2.Tables[0];
            this.ddlToSection.DataBind();



        }

        protected void ddlToProject_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.GetToSection();
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);


        }
        protected void ddlToCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetToProject();


        }


        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tbltrnasemp"];
            int TblRowIndex;
            for (int i = 0; i < this.gvEmpListTrans.Rows.Count; i++)
            {

                string chkmerge = ((CheckBox)this.gvEmpListTrans.Rows[i].FindControl("chkId")).Checked ? "True" : "False";
                TblRowIndex = (gvEmpListTrans.PageIndex) * gvEmpListTrans.PageSize + i;
                dt.Rows[TblRowIndex]["chkmerge"] = chkmerge;

            }

            Session["tbltrnasemp"] = dt;



        }



        protected void lnkbtnTrnserTo_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string msg = "";
            try
            {
                this.SaveValue();
                string comcod = this.GetCompCode();
                DataTable dt = (DataTable)Session["tbltrnasemp"];
                this.Get_Trnsno();
                string curdate = this.GetStdDate(this.txtCurTransDate.Text.ToString().Trim());
                string tansno = this.lblCurTransNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurTransNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurTransNo2.Text.ToString().Trim();
                //  string information = this.txtfmaters.Text.Trim();
                //string spnote = this.txtspnote.Text.Trim();

                string tocompany = ddlToCompany.SelectedValue.ToString();
                string toprj = ddlToProject.SelectedValue.ToString();
                string toprjcode = ddlToSection.SelectedValue.ToString();
                string atplacedat = System.DateTime.Today.ToString("dd-MMM-yyyy");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string fromprj = dt.Rows[i]["tfprjcode"].ToString();
                    string chkmerge = dt.Rows[i]["chkmerge"].ToString();
                    // string date = Convert.ToDateTime(dt.Rows[i]["pplacedate"]).ToString();
                    string desigid = dt.Rows[i]["desigid"].ToString();

                    if (chkmerge == "True")
                    {

                        bool result = purData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSORUPHREMPTNSINF", tansno, fromprj, toprjcode, empid,
                        curdate, "", "", "", atplacedat, desigid, "", "", "", "", "");
                    }



                    //bool result = purData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSORUPHREMPTNSINF", tansno, fromprj, toprj, empid,
                    //     curdate, remarks, information, spnote, date, desigid, "", "", "", "", "");
                }
                //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                msg = "Updated Successfully ";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);



            }
            catch (Exception ex)
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                msg = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);


            }

        }



        protected void lnkbtnApproved_Click(object sender, EventArgs e)
        {

            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);

            this.pnlToCompany.Visible = true;
            this.GetToCompany();



        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {

        }

        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {

        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSection();

        }



        protected void ddlToSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);

        }

    }
}