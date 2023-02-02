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

    public partial class HREmpEntry : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("~/AcceessError.aspx");

                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "Personal") ? "EMPLOYEE PERSONAL INFORMATION VIEW/EDIT" : (this.Request.QueryString["Type"].ToString().Trim() == "Aggrement") ? "EMPLOYMENT AGREEMENT INFORMATION VIEW/EDIT" : "EMPLOYMENT OFFICE TIME INFORMATION VIEW/EDIT";
                this.SelectView();
                this.lblmsg2.Visible = false;
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

                //Company Hourly Rate divisor
                this.CompanyDivisorRate();
                this.GetCompany();
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void SelectView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Aggrement":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ValueChange();
                    this.GenInfo();
                    break;
                case "Officetime":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.GetCompany();
                    //this.GetProjectName();
                    break;
                case "shifttime":
                    this.lblfrmdate.Visible = true;
                    this.txtfromdate.Visible = true;
                    this.lbltodate.Visible = true;
                    this.txttodate.Visible = true;
                    DateTime curdate = System.DateTime.Today;
                    this.txtfromdate.Text = curdate.ToString("dd-MMM-yyyy");
                    this.txttodate.Text = curdate.AddDays(7).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    this.GetCompany();
                    //this.GetProjectName();
                    break;
            }
        }
        private void ValueChange()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            switch (comcod)
            {
                case "4325":
                case "4330":
                case "4101":
                    this.lbldeptnameagg.Text = "Location";
                    this.lblsection.Text = "Department";
                    break;

                default:
                    break;

            }
        }
        private void CompanyDivisorRate()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3347": // Peb Steeel
                case "3368": // Finlay
                case "3101": // Pinovation
                    this.txtdevided.Text = "208";
                    break;

                default:
                    this.txtdevided.Text = "238";
                    break;
            }
        }

        protected void ibtnFindCompanyAgg_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }
        protected void lbtndeptagg_Click(object sender, EventArgs e)
        {
            this.GetDepartment();

        }

        private void GetCompany()
        {

            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string txtCompany = (this.Request.QueryString["Type"].ToString().Trim() == "Aggrement") ? this.txtSrcCompanyAgg.Text.Trim() + "%" : this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            Session["tblcompany"] = ds5.Tables[0];
            if (this.Request.QueryString["Type"].ToString().Trim() == "Aggrement")
            {
                this.ddlCompanyAgg.DataTextField = "actdesc";
                this.ddlCompanyAgg.DataValueField = "actcode";
                this.ddlCompanyAgg.DataSource = ds5.Tables[0];
                this.ddlCompanyAgg.DataBind();
                this.GetDepartment();
                //this.ddlCompanyAgg_SelectedIndexChanged(null, null);
                return;
            }
            else if (this.Request.QueryString["Type"].ToString().Trim() == "Officetime")
                this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds5.Tables[0];
            this.ddlCompany.DataBind();
            this.ddlCompany_SelectedIndexChanged(null, null);
            //ds1.Dispose();
            //this.ddlCompany.SelectedValue = (this.Request.QueryString["empid"] == "") ? "" : this.Request.QueryString["empid"].ToString();
        }

        protected void ddlCompanyAgg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.chkEdit.Checked)
                return;
            this.GetDepartment();
        }
        private void GetDepartment()
        {
            string comcod = this.GetCompCode();
            string type = this.Request.QueryString["Type"].ToString().Trim();

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyAgg.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string txtSProject = this.txtsrchdeptagg.Text.Trim() + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEPTNAMENEW", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddldepartmentagg.DataTextField = "deptdesc";
            this.ddldepartmentagg.DataValueField = "deptcode";
            this.ddldepartmentagg.DataSource = ds4.Tables[0];
            this.ddldepartmentagg.DataBind();
            this.GetProjectName();
        }

        private void GetProjectNameOT()
        {
            string comcod = this.GetCompCode();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            int hrcomln = (type == "Aggrement") ? Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyAgg.SelectedValue.ToString() + "'"))[0]["hrcomln"])
                    : Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);

            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";

            string txtSProject = this.txtSrcDepartment.Text.Trim() + "%";

            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAMEFOT", Company, txtSProject, "", "", "", "", "", "", "");


            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds4.Tables[0];
            this.ddlDepartment.DataBind();

        }

        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            int hrcomln = (type == "Aggrement") ? Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyAgg.SelectedValue.ToString() + "'"))[0]["hrcomln"])
                    : Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);

            //string Company = ((type == "Aggrement") ? this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, hrcomln) : this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln)) + "%";


            string Company = ((type == "Aggrement") ? (this.ddldepartmentagg.SelectedValue.ToString() == "000000000000" ? "" : this.ddldepartmentagg.SelectedValue.ToString().Substring(0, 9)) : (this.ddlDepartment.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9))) + "%";

            string txtSProject = (type == "Aggrement") ? (this.txtSrcPro.Text.Trim() + "%") : (this.txtSrcDepartment.Text.Trim() + "%");
            string CallType = (this.Request.QueryString["Type"].ToString().Trim() == "Aggrement") ? "GETPROJECTNAME" : "GETPROJECTNAMEFOT";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", CallType, Company, txtSProject, "", "", "", "", "", "", "");

            if (this.Request.QueryString["Type"].ToString().Trim() == "Aggrement")
            {
                this.ddlProjectName.DataTextField = "actdesc";
                this.ddlProjectName.DataValueField = "actcode";
                this.ddlProjectName.DataSource = ds4.Tables[0];
                this.ddlProjectName.DataBind();
                this.ddlProjectName_SelectedIndexChanged(null, null);
                return;
            }
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds4.Tables[0];
            this.ddlDepartment.DataBind();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //string prjname = this.ddlProjectName.SelectedItem.Text;
            //string PactCode = this.ddlProjectName.SelectedValue.ToString();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comname = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ////item info
            //DataTable basicinfo = (DataTable)Session["UsirBasicInformation"];
            //string UsirCode = this.lblCode.Text;
            //string ItemName = basicinfo.Rows[0]["udesc"].ToString();
            //string size = Convert.ToDouble(basicinfo.Rows[0]["usize"]).ToString("#,##0.00;(#,##0.00); ");
            ////ToString("#,##0;(#,##0); ")
            //string unit = basicinfo.Rows[0]["munit"].ToString();

            //string concat1 = ItemName + " , " + "Unit Size: " + size + " " + unit;
            ////direct cost
            //string ldiscounttT = this.ldiscountt.Text;
            //string ldiscountpP = this.ldiscountp.Text;

            //string salesteams = ddlSalesTeam.SelectedItem.Text;
            //string cuscareteam = ddlCCareTeam.SelectedItem.Text;

            //DataSet dss = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "COMBINEDTABLEFORSALES", PactCode, UsirCode, "", "", "", "", "", "", "");
            //RptPCPayment rpcp = new RptPCPayment();

            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comname;

            //TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //txtPrjName.Text = "Project Name: " + prjname.ToString().Substring(13);
            //TextObject txtItemName = rpcp.ReportDefinition.ReportObjects["txtItemName"] as TextObject;
            //txtItemName.Text = "Unit Description: " + concat1;

            //TextObject txtdist = rpcp.ReportDefinition.ReportObjects["txtdist"] as TextObject;
            //txtdist.Text = "Discount in Tk. " + ldiscounttT;

            //TextObject txtdisp = rpcp.ReportDefinition.ReportObjects["txtdisp"] as TextObject;
            //txtdisp.Text = "Discount in (%) " + ldiscountpP;

            //TextObject txtsalest = rpcp.ReportDefinition.ReportObjects["txtsalest"] as TextObject;
            //txtsalest.Text = "Sales Team: " + salesteams;

            //TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["txtccaret"] as TextObject;
            //txtccaret.Text = "Customer Care Team: " + cuscareteam;
            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //TextObject txtcominfo = rpcp.ReportDefinition.ReportObjects["txtcominfo"] as TextObject;
            //txtcominfo.Text = ASTUtility.Cominformation();


            //rpcp.SetDataSource(dss.Tables[0]);
            //Session["Report1"] = rpcp;


            //this.lbljavascript.Text = @"<script>window.open('RptViewer.aspx');</script>";
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {

        }
        protected void ibtnFindEmp_Click(object sender, EventArgs e)
        {
            this.ddlProjectName_SelectedIndexChanged(null, null);
        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void ibtnNFindEmp_Click(object sender, EventArgs e)
        {

            ViewState.Remove("tblemp");
            string comcod = this.GetCompCode();
            string ProjectCode = this.ddlProjectName.SelectedValue.ToString();
            string txtSProject = "%" + this.txtNSrcEmp.Text + "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETNEWPNAME", "", txtSProject, "", "", "", "", "", "", "");
            this.ddlNPEmpName.DataTextField = "empname";
            this.ddlNPEmpName.DataValueField = "empid";
            this.ddlNPEmpName.DataSource = ds5.Tables[0];
            this.ddlNPEmpName.DataBind();
            ViewState["tblemp"] = ds5.Tables[0];
            // this.ddlNPEmpName.SelectedValue = (this.Request.QueryString["empid"] == "") ? "" : this.Request.QueryString["empid"].ToString();
        }

        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lnkbtnSerOk.Text == "Ok")
                {
                    this.lnkbtnSerOk.Text = "New";
                    this.lblCompanyNameAgg.Text = this.ddlCompanyAgg.SelectedItem.Text;
                    this.lblvaldeptagg.Text = this.ddldepartmentagg.SelectedItem.Text;
                    this.lblProjectdesc.Text = this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "All Section" : this.ddlProjectName.SelectedItem.Text.Substring(13);
                    string empid = "";
                    if (this.ddlNPEmpName.Items.Count > 0)
                    {
                        empid = this.ddlNPEmpName.SelectedValue.ToString();
                        this.lblPEmpName.Text = this.ddlNPEmpName.SelectedItem.Text.Substring(7);
                        this.chknewEmp.Checked = false;
                    }
                    else
                    {
                        this.lblPEmpName.Text = this.ddlPEmpName.SelectedItem.Text.Substring(7);
                        empid = this.ddlPEmpName.SelectedValue.ToString();
                    }

                    this.chknewEmp_CheckedChanged(null, null);
                    this.lbtnDeletelink.Visible = false;
                    this.ddlCompanyAgg.Visible = false;
                    this.ddldepartmentagg.Visible = false;
                    this.ddlProjectName.Visible = false;
                    this.ddlPEmpName.Visible = false;
                    this.lblCompanyNameAgg.Visible = true;
                    this.lblvaldeptagg.Visible = true;
                    this.lblProjectdesc.Visible = true;
                    this.lblPEmpName.Visible = true;
                    this.pnlGenInfo.Visible = true;
                    this.lblhSalary.Visible = true;
                    this.lblAllowance.Visible = true;
                    this.lbltxtTotalSal.Visible = true;
                    this.lbltotalsal.Visible = true;
                    this.lblhSalaryAdd.Visible = true;
                    this.lblhSalaryDed.Visible = true;
                    this.lblhAllowAdd.Visible = true;
                    this.lblhAllowDed.Visible = true;
                    this.lnkbtnFinalSWUpdate.Visible = true;
                    this.chknewEmp.Visible = false;
                    this.chkEdit.Visible = true;
                    this.txtgrossal.Visible = true;
                    // this.rbtGross.Visible = true;
                    //this.rbtGross.SelectedIndex =0;   
                    this.rbtholiday.SelectedIndex = 0;
                    this.rbtnOverTime.SelectedIndex = 0;
                    this.rbtPaymentType.SelectedIndex = 0;
                    this.rbtnOverTime_SelectedIndexChanged(null, null);
                    this.GetGrossType();
                    this.EmpSerRule();
                    this.TSandAllow();
                    this.OverTimeFORRate();
                    this.GetEmpBasicData(empid);

                    CreateTableOverTimeFORRate();
                    this.SlotOverTimeFORRate(empid);
                    this.lblvaljoindate.Text = Convert.ToDateTime(((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["joindate"]).ToString("dd-MMM-yyyy");
                    //this.txtPf.Text = Convert.ToDateTime(((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["pfdate"]).ToString("dd-MMM-yyyy");
                }
                else
                {
                    this.lnkbtnSerOk.Text = "Ok";
                    this.lbtnDeletelink.Visible = true;
                    this.ddlCompanyAgg.Visible = true;
                    this.ddldepartmentagg.Visible = true;
                    this.ddlProjectName.Visible = true;
                    this.ddlPEmpName.Visible = true;
                    this.lblCompanyNameAgg.Visible = false;
                    this.lblvaldeptagg.Visible = false;
                    this.lblProjectdesc.Visible = false;
                    this.lblPEmpName.Visible = false;
                    this.pnlGenInfo.Visible = false;
                    this.lblhSalary.Visible = false;
                    this.lblAllowance.Visible = false;
                    this.lbltxtTotalSal.Visible = false;
                    this.lbltotalsal.Visible = false;
                    this.lblhSalaryAdd.Visible = false;
                    this.lblhSalaryDed.Visible = false;
                    this.lblhAllowAdd.Visible = false;
                    this.lblhAllowDed.Visible = false;
                    this.lnkbtnFinalSWUpdate.Visible = false;
                    this.chknewEmp.Checked = false;
                    this.chknewEmp.Visible = true;
                    this.chkEdit.Checked = false;
                    this.chkEdit.Visible = false;
                    this.lblholidayallowance.Visible = false;
                    this.txtholidayallowance.Visible = false;
                    this.txtgrossal.Visible = false;
                    // this.rbtGross.Visible = false;
                    //this.rbtGross.Visible = true;
                    this.pnlPaymenttype.Visible = false;
                    this.ddlNPEmpName.Items.Clear();
                    this.gvSalAdd.DataSource = null;
                    this.gvSalAdd.DataBind();
                    this.gvSalSub.DataSource = null;
                    this.gvSalSub.DataBind();
                    this.gvAllowAdd.DataSource = null;
                    this.gvAllowAdd.DataBind();
                    this.gvAllowSub.DataSource = null;
                    this.gvAllowSub.DataBind();

                    this.gvTimsSlot.DataSource = null;
                    this.gvTimsSlot.DataBind();

                    this.lblCompanyNameAgg.Text = "";
                    this.lblProjectdesc.Text = "";
                    this.lblPEmpName.Text = "";
                    this.lblDesgination.Text = "";
                    this.lbloffintime.Text = "";
                    this.lbloffouttime.Text = "";
                    this.lbllanintime.Text = "";
                    this.lbllanouttime.Text = "";
                    this.lblEduQua.Text = "";
                    this.lblAtype.Text = "";
                    this.txtholidayallowance.Text = "";
                    this.txtfixedRate.Text = "";
                    this.txthourlyRate.Text = "";
                    this.txtceilingRate1.Text = "";
                    this.txtceilingRate2.Text = "";
                    this.txtceilingRate3.Text = "";
                    this.txtgrossal.Text = "";

                    this.txtAcNo1.Text = "";
                    this.txtAcNo2.Text = "";
                    this.lblvaljoindate.Text = "";
                    this.lblforrate.Text = "";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something wrong');", true);

            }

        }
        private void GetEmpBasicData(string empid)
        {
            string comcod = this.GetCompCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPBASICDATA", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            this.hiddnCardId.Value = ds3.Tables[0].Rows[0]["idcard"].ToString();
            this.hiddnempname1.Value = ds3.Tables[0].Rows[0]["empname1"].ToString();
            string idcard = ds3.Tables[0].Rows[0]["isUserId"].ToString().Trim();
            this.lnkUserGenerate.Visible = idcard.Length == 0 ? true : false;
        }
        private void GenInfo()
        {
            string comcod = this.GetCompCode();
            string empid = (this.ddlNPEmpName.Items.Count > 0) ? this.ddlNPEmpName.SelectedValue.ToString() : this.ddlPEmpName.SelectedValue.ToString();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETGENINFO", empid, "", "", "", "", "", "", "", "");
            Session["UserLog"] = ds5.Tables[7];



            if (ds5 == null)
                return;

            this.ddlDesignation.DataTextField = "designame";
            this.ddlDesignation.DataValueField = "desigid";
            this.ddlDesignation.DataSource = ds5.Tables[0];
            this.ddlDesignation.DataBind();

            this.ddlOffintime.DataTextField = "offintime";
            this.ddlOffintime.DataValueField = "offinid";
            this.ddlOffintime.DataSource = ds5.Tables[1];
            this.ddlOffintime.DataBind();

            this.ddlOffouttime.DataTextField = "offouttime";
            this.ddlOffouttime.DataValueField = "offoutid";
            this.ddlOffouttime.DataSource = ds5.Tables[2];
            this.ddlOffouttime.DataBind();

            this.ddlLanintime.DataTextField = "lanintime";
            this.ddlLanintime.DataValueField = "laninid";
            this.ddlLanintime.DataSource = ds5.Tables[3];
            this.ddlLanintime.DataBind();

            this.ddlLanouttime.DataTextField = "lanouttime";
            this.ddlLanouttime.DataValueField = "lanoutid";
            this.ddlLanouttime.DataSource = ds5.Tables[4];
            this.ddlLanouttime.DataBind();


            this.ddlEduQua.DataTextField = "eduqua";
            this.ddlEduQua.DataValueField = "eduid";
            this.ddlEduQua.DataSource = ds5.Tables[5];
            this.ddlEduQua.DataBind();

            this.ddlAggrement.DataTextField = "agtype";
            this.ddlAggrement.DataValueField = "agtypeid";
            this.ddlAggrement.DataSource = ds5.Tables[6];
            this.ddlAggrement.DataBind();



            this.ddlBankName1.DataTextField = "actdesc";
            this.ddlBankName1.DataValueField = "actcode";
            this.ddlBankName1.DataSource = ds5.Tables[8];
            this.ddlBankName1.DataBind();

            this.ddlBankName2.DataTextField = "actdesc";
            this.ddlBankName2.DataValueField = "actcode";
            this.ddlBankName2.DataSource = ds5.Tables[9];
            this.ddlBankName2.DataBind();

            ds5.Dispose();
        }
        private void GetGrossType()
        {

            string comcod = this.GetCompCode();
            this.rbtGross.Visible = false;
            switch (comcod)
            {
                //case "4101": //foster
                //   this.rbtGross.Visible = true;
                //    this.rbtGross.SelectedIndex = 2;
                //    //this.rbtGross.Visible = false;
                //    break;

                //case "4301": //sanmar
                //    this.rbtGross.Visible = true;
                //    this.rbtGross.SelectedIndex = 1;
                //    this.rbtGross.Items[2].Enabled = false;
                //    this.rbtGross.Items[3].Enabled = false;
                //    break;
                case "4305"://Rupayan
                case "4330"://Bridge
                case "4315"://Assure
                case "4325"://Leisure
                case "4332"://Leisure
                //case "3338": //Acme 
                case "3347": //PEB           
                case "3348": //Credence           
                case "3355": //GreenWood
                case "3365": // BTI

                    // this.rbtGross.Visible = false;
                    this.rbtGross.SelectedIndex = 2;
                    //this.rbtGross.Items[0].Enabled = false;
                    //this.rbtGross.Items[1].Enabled = false;
                    //this.rbtGross.Items[3].Enabled = false;
                    break;

                case "4201"://Multiplan
                            //  this.rbtGross.Visible = false;
                    this.rbtGross.SelectedIndex = 3;
                    this.rbtGross.Items[0].Enabled = false;
                    this.rbtGross.Items[1].Enabled = false;
                    this.rbtGross.Items[2].Enabled = false;
                    break;
                case "4306"://GLG
                            // this.rbtGross.Visible = false;
                    this.rbtGross.SelectedIndex = 4;
                    this.rbtGross.Items[0].Enabled = false;
                    this.rbtGross.Items[1].Enabled = false;
                    this.rbtGross.Items[2].Enabled = false;
                    this.rbtGross.Items[3].Enabled = false;
                    break;

                case "3333"://GLG

                    // this.rbtGross.Visible = true;
                    this.rbtGross.SelectedIndex = 5;
                    //this.rbtGross.Items[0].Enabled = false;
                    //this.rbtGross.Items[1].Enabled = false;
                    //this.rbtGross.Items[2].Enabled = false;
                    //this.rbtGross.Items[3].Enabled = false;
                    break;

                case "3339"://Tropical

                    // this.rbtGross.Visible = true;
                    this.rbtGross.SelectedIndex = 6;
                    //this.rbtGross.Items[0].Enabled = false;
                    //this.rbtGross.Items[1].Enabled = false;
                    //this.rbtGross.Items[2].Enabled = false;
                    //this.rbtGross.Items[3].Enabled = false;
                    break;

                //case "3347"://PEB

                // this.rbtGross.Visible = false;
                // this.rbtGross.SelectedIndex = 7;
                // //this.rbtGross.Items[0].Enabled = false;
                // //this.rbtGross.Items[1].Enabled = false;
                // //this.rbtGross.Items[2].Enabled = false;
                // //this.rbtGross.Items[3].Enabled = false;
                // break;
                //case "3101": 
                case "3338": //Acme tec
                case "1206": //Acme serv
                case "1207": //Acme con
                             // case "3369": //Acme ai
                    this.rbtGross.Visible = false;
                    this.rbtGross.SelectedIndex = 3;
                    break;
                case "3370": //CPDL
                    this.rbtGross.SelectedIndex = 7;
                    break;
                default:
                    //  this.rbtGross.Visible = true;
                    this.rbtGross.SelectedIndex = 2;
                    break;

            }
        }
        private void EmpSerRule()
        {
            Session.Remove("tblData");

            string comcod = this.GetCompCode();
            string projectcode = this.ddlProjectName.SelectedValue.ToString();
            string empid = (this.ddlNPEmpName.Items.Count > 0) ? this.ddlNPEmpName.SelectedValue.ToString() : this.ddlPEmpName.SelectedValue.ToString();
            DataSet ds6 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLOYEEINFO", projectcode, empid, "", "", "", "", "", "", "");
            if (ds6 == null)
                return;
            if (ds6.Tables[2].Rows.Count > 0)
            {
                Session["tblData"] = ds6.Tables[2];
                this.ShowSalAllow();

            }
            if (ds6.Tables[0].Rows.Count > 0)
            {
                //this.lblDesgination.Text = ds6.Tables[0].Rows[0]["gdesc"].ToString().Trim();
                //this.lblEduQua.Text = ds6.Tables[0].Rows[1]["gdesc"].ToString().Trim();
                //this.lblProQua.Text = ds6.Tables[0].Rows[2]["gdesc"].ToString().Trim();
                //this.ddlDesignation.SelectedValue = ds6.Tables[0].Rows[0]["gcod"].ToString().Trim();
                //this.ddlEduQua.SelectedValue=ds6.Tables[0].Rows[1]["gcod"].ToString().Trim();
                //this.ddlProQua.SelectedValue=ds6.Tables[0].Rows[2]["gcod"].ToString().Trim();
                DataRow[] dr1 = (ds6.Tables[0]).Select("gcod like '03%'");
                if (dr1.Length > 0)
                {
                    this.lblDesgination.Text = dr1[0]["gdesc"].ToString().Trim();
                    this.ddlDesignation.SelectedValue = dr1[0]["gcod"].ToString().Trim();
                }
                //Over Time 
                dr1 = (ds6.Tables[0]).Select("gcod like '07004%'");
                if (dr1.Length > 0)
                {

                    this.rbtnOverTime.SelectedIndex = (dr1[0]["gdatat1"].ToString().Trim() == "0") ? 0 : (dr1[0]["gdatat1"].ToString().Trim() == "1") ? 1
                                                : (dr1[0]["gdatat1"].ToString().Trim() == "2") ? 2 : (dr1[0]["gdatat1"].ToString().Trim() == "4") ? 4: 3;
                    if (this.rbtnOverTime.SelectedIndex == 2)
                    {
                        switch (comcod)
                        {
                            case "3336":

                                double gssal = Convert.ToDouble("0" + this.txtgrossal.Text.Trim());

                                this.txtdevided.Text = Convert.ToDouble(dr1[0]["hrate"].ToString().Trim()) == 0 ? "0" : Math.Round(gssal / Convert.ToDouble(dr1[0]["hrate"].ToString().Trim()), 0).ToString();
                                // dhourlyrate = Convert.ToDouble("0" + this.txtdevided.Text.Trim()) > 0 ? gssal / Convert.ToDouble("0" + this.txtdevided.Text.Trim()) : 0;

                                this.lblforrate.Text = "Rate:<span class='color:blue !important'>" + Convert.ToDouble(dr1[0]["hrate"]).ToString("#,##0;(#,##0); ") + "</span>";

                                break;




                            case "3347":// Peb Steel
                                double bsal = Convert.ToDouble((ds6.Tables[2].Select("gcod='04001'"))[0]["gval"]);
                                double dailallow = Convert.ToDouble((ds6.Tables[2].Select("gcod='04012'"))[0]["gval"]);
                                double hrate = Convert.ToDouble(dr1[0]["hrate"].ToString().Trim());
                                //this.txtdevided.Text = Convert.ToDouble(dr1[0]["hrate"].ToString().Trim()) == 0 ? "0" : Math.Round(((Convert.ToDouble((ds6.Tables[2].Select("gcod='04001'"))[0]["gval"])),0)   //  + ((Convert.ToDouble((ds6.Tables[2].Select("gcod='04012'"))[0]["gval"])))*2  /  (26*8), 0).ToString();
                                //this.txtdevided.Text = Convert.ToDouble(dr1[0]["hrate"].ToString().Trim()) == 0 ? "0"
                                //     : Math.Ceiling(((bsal + dailallow) * 2) / hrate).ToString();
                                this.txtdevided.Text = Convert.ToDouble(dr1[0]["hrate"].ToString().Trim()) == 0 ? "0"
                                  : Convert.ToDouble(((bsal + dailallow) * 2) / hrate).ToString("#,##0;(#,##0); ");
                                this.lblforrate.Text = "Rate:<span class='color:blue !important'>" + Convert.ToDouble(dr1[0]["hrate"]).ToString("#,##0;(#,##0); ") + "</span>";
                                break;

                            case "3368"://Finlay
                            case "3101"://Model
                                this.txtdevided.Text = Convert.ToDouble(dr1[0]["hrate"].ToString().Trim()) == 0 ? "0" : Math.Round(((Convert.ToDouble((ds6.Tables[2].Select("gcod='04001'"))[0]["gval"]) * 1.5) / Convert.ToDouble(dr1[0]["hrate"].ToString().Trim())), 0).ToString();
                                this.lblforrate.Text = "Rate:<span class='color:blue !important'>" + Convert.ToDouble(dr1[0]["hrate"]).ToString("#,##0;(#,##0); ") + "</span>";
                                break;


                            default:
                                this.txtdevided.Text = Convert.ToDouble(dr1[0]["hrate"].ToString().Trim()) == 0 ? "0" : Math.Round(((Convert.ToDouble((ds6.Tables[2].Select("gcod='04001'"))[0]["gval"])) / Convert.ToDouble(dr1[0]["hrate"].ToString().Trim())), 0).ToString();
                                this.lblforrate.Text = "Rate:<span class='color:blue !important'>" + Convert.ToDouble(dr1[0]["hrate"]).ToString("#,##0;(#,##0); ") + "</span>";
                                break;
                        }
                    }
                    this.txtfixedRate.Text = Convert.ToDouble(dr1[0]["rate"].ToString().Trim()).ToString("#,##0;(#,##0); ");
                    this.txthourlyRate.Text = Convert.ToDouble(dr1[0]["hrate"].ToString().Trim()).ToString("#,##0;(#,##0); ");
                    this.txtceilingRate1.Text = Convert.ToDouble(dr1[0]["crate1"].ToString().Trim()).ToString("#,##0;(#,##0); ");
                    this.txtceilingRate2.Text = Convert.ToDouble(dr1[0]["crate2"].ToString().Trim()).ToString("#,##0;(#,##0); ");
                    this.txtceilingRate3.Text = Convert.ToDouble(dr1[0]["crate3"].ToString().Trim()).ToString("#,##0;(#,##0); ");
                    this.rbtnOverTime_SelectedIndexChanged(null, null);
                }
                dr1 = (ds6.Tables[0]).Select("gcod like '07005%'");
                if (dr1.Length > 0)
                {
                    this.rbtholiday.SelectedIndex = (dr1[0]["gdatat1"].ToString().Trim() == "0") ? 0 : (dr1[0]["gdatat1"].ToString().Trim() == "1") ? 1 : 2;
                    this.txtholidayallowance.Text = Convert.ToDouble(dr1[0]["rate"].ToString().Trim()).ToString("#,##0;(#,##0); ");
                    this.rbtholiday_SelectedIndexChanged(null, null);
                }
                dr1 = (ds6.Tables[0]).Select("gcod like '11%'");
                if (dr1.Length > 0)
                {
                    this.lblEduQua.Text = dr1[0]["gdesc"].ToString().Trim();
                    this.ddlEduQua.SelectedValue = dr1[0]["gcod"].ToString().Trim();
                    this.txtEduPass.Text = dr1[0]["gdatat2"].ToString().Trim();
                }
                dr1 = (ds6.Tables[0]).Select("gcod like '12%'");
                if (dr1.Length > 0)
                {
                    this.lblAtype.Text = dr1[0]["gdesc"].ToString().Trim();
                    this.ddlAggrement.SelectedValue = dr1[0]["gcod"].ToString().Trim();
                }
                //DataRow []dr = ds6.Tables[0].Select("gcod='21001'");
                //this.txtEduPass.Text = (dr.Length == 0) ? "" :dr[0]["gdesc"].ToString();
                // dr = ds6.Tables[0].Select("gcod='21002'");
                //this.txtProPass.Text = (dr.Length == 0) ? "" : dr[0]["gdesc"].ToString();
                // (ds6.Tables[0].Select("gcod='13001'"))["gdesc"].ToString()
            }
            if (ds6.Tables[1].Rows.Count > 0)
            {
                this.lbloffintime.Text = Convert.ToDateTime(ds6.Tables[1].Rows[0]["gdesc"]).ToString("hh:mm tt");
                this.lbloffouttime.Text = Convert.ToDateTime(ds6.Tables[1].Rows[1]["gdesc"]).ToString("hh:mm tt");
                this.lbllanintime.Text = Convert.ToDateTime(ds6.Tables[1].Rows[2]["gdesc"]).ToString("hh:mm tt");
                this.lbllanouttime.Text = Convert.ToDateTime(ds6.Tables[1].Rows[3]["gdesc"]).ToString("hh:mm tt");
                this.ddlOffintime.SelectedValue = ds6.Tables[1].Rows[0]["gcod"].ToString().Trim();
                this.ddlOffouttime.SelectedValue = ds6.Tables[1].Rows[1]["gcod"].ToString().Trim();
                this.ddlLanintime.SelectedValue = ds6.Tables[1].Rows[2]["gcod"].ToString().Trim();
                this.ddlLanouttime.SelectedValue = ds6.Tables[1].Rows[3]["gcod"].ToString().Trim();
            }
            if (ds6.Tables[0].Rows.Count == 0 && ds6.Tables[1].Rows.Count == 0)
            {
                this.rbtholiday.SelectedIndex = 0;
                this.ddlDesignation_SelectedIndexChanged(null, null);
                this.ddlOffintime_SelectedIndexChanged(null, null);
                this.ddlOffouttime_SelectedIndexChanged(null, null);
                this.ddlLanintime_SelectedIndexChanged(null, null);
                this.ddlLanouttime_SelectedIndexChanged(null, null);
                this.ddlEduQua_SelectedIndexChanged(null, null);
                this.ddlProQua_SelectedIndexChanged(null, null);
            }

            if (ds6.Tables[4].Rows.Count > 0)
            {
                this.txtPf.Text = (Convert.ToDateTime(ds6.Tables[4].Rows[0]["pfdate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds6.Tables[4].Rows[0]["pfdate"]).ToString("dd-MMM-yyyy");
            }
            if (ds6.Tables[5].Rows.Count > 0)
            {
                this.txtpfend.Text = (Convert.ToDateTime(ds6.Tables[5].Rows[0]["pfenddat"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds6.Tables[5].Rows[0]["pfenddat"]).ToString("dd-MMM-yyyy");
            }
            else
            {
                //this.txtpfend.Text = "";
            }
            if (ds6.Tables[3].Rows.Count > 0)
            {
                string Bankname = ds6.Tables[3].Rows[0]["gdatat"].ToString();
                if (Bankname.Length != 0)
                {
                    this.pnlPaymenttype.Visible = true;
                    this.rbtPaymentType.SelectedIndex = 1;
                    this.ddlBankName1.SelectedValue = (ds6.Tables[3].Rows[0]["gdatat"]).ToString();
                    this.txtAcNo1.Text = (ds6.Tables[3].Rows[0]["acno"]).ToString();
                    this.txtroute.Text = (ds6.Tables[3].Rows[0]["routing1"]).ToString();
                    this.txtroute2.Text = (ds6.Tables[3].Rows[0]["routing2"]).ToString();
                    this.txtBankamt02.Text = Convert.ToDouble(ds6.Tables[3].Rows[0]["bankamt"]).ToString("#,##0;(#,##0);");
                    this.txtCashAmt.Text = Convert.ToDouble(ds6.Tables[3].Rows[0]["cashamt"]).ToString("#,##0;(#,##0);");
                    this.chkcash0bank1.Checked = ds6.Tables[3].Rows[0]["cash0bank1"].ToString() == "False" ? false : true;
                    this.chkcash0bank1_CheckedChanged(null, null);
                }
                string Bankname2 = ds6.Tables[3].Rows[0]["bankcode"].ToString();
                if (Bankname2.Trim().Length != 0)
                {
                    this.pnlPaymenttype.Visible = true;
                    this.rbtPaymentType.SelectedIndex = 1;
                    this.ddlBankName2.SelectedValue = (ds6.Tables[3].Rows[0]["bankcode"]).ToString();
                    this.txtAcNo2.Text = (ds6.Tables[3].Rows[0]["acno2"]).ToString();
                }

            }
            else
            {
                this.rbtPaymentType.SelectedIndex = 0;
            }
            if (ds6.Tables[3].Rows.Count > 0)
            {
                string paytype = ds6.Tables[3].Rows[0]["paytype"].ToString();

                if (paytype == "Cheque")
                {
                    this.rbtPaymentType.SelectedIndex = 2;
                    this.pnlPaymenttype.Visible = true;
                    this.txtCashAmt.Text = Convert.ToDouble(ds6.Tables[3].Rows[0]["cashamt"]).ToString("#,##0;(#,##0);");
                }
            }
        }
        private void ShowSalAllow()
        {

            Session.Remove("tblsaladd");
            Session.Remove("tblsalsub");
            Session.Remove("tblallowadd");
            Session.Remove("tblallowsub");
            DataTable dtr = (DataTable)Session["tblData"];
            DataView dvr = new DataView();
            DataTable dtr1 = new DataTable();

            dtr1 = dtr;
            dvr = dtr1.DefaultView;
            dvr.RowFilter = ("gcod like '040%'");
            dtr1 = dvr.ToTable();
            Session["tblsaladd"] = dtr1;
            this.gvSalAdd.DataSource = dtr1;
            this.gvSalAdd.DataBind();
            this.FooterCalculation(dtr1, "gvSalAdd");

            dtr1 = dtr;
            dvr = dtr1.DefaultView;
            dvr.RowFilter = ("gcod like '041%'");
            dtr1 = dvr.ToTable();
            Session["tblsalsub"] = dtr1;
            this.gvSalSub.DataSource = dtr1;
            this.gvSalSub.DataBind();
            this.FooterCalculation(dtr1, "gvSalSub");

            string comcod = this.GetCompCode();
            int i;

            switch (comcod)
            {

                case "3339":

                    for (i = 0; i < this.gvSalAdd.Rows.Count; i++)
                    {
                        ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                    }
                    for (i = 0; i < this.gvSalSub.Rows.Count; i++)
                    {
                        ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");
                    }
                    break;
                default:
                    for (i = 0; i < this.gvSalAdd.Rows.Count; i++)
                    {
                        ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim())).ToString("#,##0;-#,##0; ");
                    }

                    for (i = 0; i < this.gvSalSub.Rows.Count; i++)
                    {
                        ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim())).ToString("#,##0;-#,##0; ");
                    }

                    break;
            }


            dtr1 = dtr;
            dvr = dtr1.DefaultView;
            dvr.RowFilter = ("gcod like '070%'");
            dtr1 = dvr.ToTable();
            Session["tblallowadd"] = dtr1;
            this.gvAllowAdd.DataSource = dtr1;
            this.gvAllowAdd.DataBind();
            this.FooterCalculation(dtr1, "gvAllowAdd");

            dtr1 = dtr;
            dvr = dtr1.DefaultView;
            dvr.RowFilter = ("gcod like '071%'");
            dtr1 = dvr.ToTable();
            Session["tblallowsub"] = dtr1;
            this.gvAllowSub.DataSource = dtr1;
            this.gvAllowSub.DataBind();
            this.FooterCalculation(dtr1, "gvAllowSub");




        }
        private void TSandAllow()
        {
            double SalAdd = 0, SallSub = 0;
            DataTable dtsaladd = (DataTable)Session["tblsaladd"];
            DataTable dtsalsub = (DataTable)Session["tblsalsub"];
            DataTable dtweagadd = (DataTable)Session["tblallowadd"];
            DataTable dtweafsub = (DataTable)Session["tblallowsub"];
            SalAdd = Convert.ToDouble((Convert.IsDBNull(dtsaladd.Compute("sum(gval)", "")) ? 0.00 : dtsaladd.Compute("sum(gval)", "")));
            SallSub = Convert.ToDouble((Convert.IsDBNull(dtsalsub.Compute("sum(gval)", "")) ? 0.00 : dtsalsub.Compute("sum(gval)", "")));
            this.lbltotalsal.Text = (SalAdd - SallSub).ToString("#,##0;(#,##0); ");





        }

        private void OverTimeFORRate()
        {

            string comcod = this.GetCompCode();
            DataTable dtsaladd = (DataTable)Session["tblsaladd"];
            double fhrate, bsal;
            double devided = Convert.ToDouble("0" + this.txtdevided.Text.Trim());

            if (this.rbtnOverTime.SelectedIndex == 2)
            {
                switch (comcod)
                {
                    case "3336":

                        double gssal = Convert.ToDouble("0" + this.txtgrossal.Text.Trim());
                        fhrate = Math.Round((gssal / devided), 0);
                        this.lblforrate.Text = "Rate:<span class='color:blue !important'>" + fhrate.ToString("#,##0;(#,##0); ") + "</span>";

                        break;




                    case "3347":// Peb Steel
                        bsal = Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"]);
                        double dailallow = Convert.ToDouble((dtsaladd.Select("gcod='04012'"))[0]["gval"]);
                        fhrate = Math.Round((((bsal + dailallow) * 2) / devided), 0);

                        this.lblforrate.Text = "Rate:<span class='color:blue !important'>" + fhrate.ToString("#,##0;(#,##0); ") + "</span>";
                        break;

                    case "3368"://Finlay
                    case "3101"://Model
                        bsal = Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"]);
                        fhrate = Math.Round(((bsal * 1.5) / devided), 0);

                        this.lblforrate.Text = "Rate:<span class='color:blue !important'>" + fhrate.ToString("#,##0;(#,##0); ") + "</span>";
                        break;


                    default:
                        bsal = Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"]);
                        fhrate = Math.Round((bsal / devided), 0);

                        this.lblforrate.Text = "Rate:<span class='color:blue !important'>" + fhrate.ToString("#,##0;(#,##0); ") + "</span>";
                        break;
                }
            }







        }


        private void FooterCalculation(DataTable dt, string GvName)
        {
            if (dt.Rows.Count == 0)
                return;
            DataTable dt1 = dt.Copy();
            string comcod = this.GetCompCode();
            DataView dv;
            double toaddamt, topaddamt, todedamt, basic;
            switch (GvName)
            {
                case "gvSalAdd":

                    switch (comcod)
                    {
                        case "3339":
                            double pfcont = Convert.ToDouble(dt1.Select("gcod='04011'")[0]["gval"]);
                            double mcell = Convert.ToDouble(dt1.Select("gcod='04009'")[0]["gval"]);
                            toaddamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));
                            ((Label)this.gvSalAdd.FooterRow.FindControl("lgvFSalAdd")).Text = (toaddamt - pfcont - mcell).ToString("#,##0;(#,##0); ");


                            this.txtgrossal.Text = (toaddamt - pfcont - mcell).ToString("#,##0;(#,##0); ");
                            break;




                        case "3347":// PEB Steel

                            toaddamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));
                            ((Label)this.gvSalAdd.FooterRow.FindControl("lgvFSalAdd")).Text = toaddamt.ToString("#,##0;(#,##0); ");

                            //dv = dt1.DefaultView;
                            //dv.RowFilter = ("percnt>0");
                            //dt1 = dv.ToTable();
                            //double topaddamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));
                            this.txtgrossal.Text = toaddamt.ToString("#,##0;(#,##0); ");




                            break;

                        //case "3101":
                        case "3338": //Acme tec
                        case "1206": //Acme serv
                        case "1207": //Acme con
                                     // case "3369": //Acme ai
                            toaddamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));
                            ((Label)this.gvSalAdd.FooterRow.FindControl("lgvFSalAdd")).Text = toaddamt.ToString("#,##0;(#,##0); ");

                            //dv = dt1.DefaultView;
                            //dv.RowFilter = ("percnt>0");
                            //dt1 = dv.ToTable();
                            //double topaddamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));
                            this.txtgrossal.Text = toaddamt.ToString("#,##0;(#,##0); ");




                            break;


                        case "3365":// BTI

                            toaddamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));
                            ((Label)this.gvSalAdd.FooterRow.FindControl("lgvFSalAdd")).Text = toaddamt.ToString("#,##0;(#,##0); ");
                            //this.txtgrossal.Text = toaddamt.ToString("#,##0;(#,##0); ");

                            // toaddamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));


                            dv = dt1.DefaultView;
                            dv.RowFilter = ("percnt>0");
                            dt1 = dv.ToTable();
                            topaddamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));
                            this.txtgrossal.Text = topaddamt.ToString("#,##0;(#,##0); ");
                            //   ((Label)this.gvSalAdd.FooterRow.FindControl("lgvFSalAdd")).Text = topaddamt.ToString("#,##0;(#,##0); ");


                            break;

                        case "3368":// Finlay
                            toaddamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));
                            ((Label)this.gvSalAdd.FooterRow.FindControl("lgvFSalAdd")).Text = toaddamt.ToString("#,##0;(#,##0); ");
                            dv = dt1.DefaultView;
                            dv.RowFilter = ("percnt>0");
                            dt1 = dv.ToTable();
                            topaddamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));
                            this.txtgrossal.Text = topaddamt.ToString("#,##0;(#,##0); ");
                            //((Label)this.gvSalAdd.FooterRow.FindControl("lgvFSalAdd")).Text = toaddamt.ToString("#,##0;(#,##0); ");
                            break;

                        case "3370": //Acme serv
                            toaddamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));
                            ((Label)this.gvSalAdd.FooterRow.FindControl("lgvFSalAdd")).Text = toaddamt.ToString("#,##0;(#,##0); ");
                            this.txtgrossal.Text = toaddamt.ToString("#,##0;(#,##0); ");

                            break;
                        default:

                            toaddamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));
                            ((Label)this.gvSalAdd.FooterRow.FindControl("lgvFSalAdd")).Text = toaddamt.ToString("#,##0;(#,##0); ");

                            dv = dt1.DefaultView;
                            dv.RowFilter = ("percnt>0");
                            dt1 = dv.ToTable();
                            topaddamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));
                            this.txtgrossal.Text = topaddamt.ToString("#,##0;(#,##0); ");




                            break;
                    }


                    break;


                case "gvSalSub":

                    ((Label)this.gvSalSub.FooterRow.FindControl("lgvFSalSub")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gval)", "")) ? 0 : dt.Compute("sum(gval)", ""))).ToString("#,##0;(#,##0); ");

                    //switch (comcod)
                    //{


                    //    case "3365":// BTI



                    //        //toaddamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));
                    //        //((Label)this.gvSalSub.FooterRow.FindControl("lgvFSalSub")).Text = toaddamt.ToString("#,##0;(#,##0); ");

                    //        dv = dt1.DefaultView;
                    //        dv.RowFilter = ("percnt>0");
                    //        dt1 = dv.ToTable();
                    //        todedamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));
                    //        ((Label)this.gvSalSub.FooterRow.FindControl("lgvFSalSub")).Text = todedamt.ToString("#,##0;(#,##0); ");


                    //        break;


                    //    default:

                    //        ((Label)this.gvSalSub.FooterRow.FindControl("lgvFSalSub")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gval)", "")) ? 0 : dt.Compute("sum(gval)", ""))).ToString("#,##0;(#,##0); ");
                    //        break;
                    //}
                    break;


            }
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.chkEdit.Checked)
                return;
            else if (this.chknewEmp.Checked)
                return;
            ViewState.Remove("tblemp");
            string comcod = this.GetCompCode();
            string ProjectCode = (this.txtSrcEmp.Text.Trim().Length > 0) ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string txtSProject = "%" + this.txtSrcEmp.Text + "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPREMPNAME", ProjectCode, txtSProject, "", "", "", "", "", "", "");
            this.ddlPEmpName.DataTextField = "empname";
            this.ddlPEmpName.DataValueField = "empid";
            this.ddlPEmpName.DataSource = ds5.Tables[0];
            this.ddlPEmpName.DataBind();
            ViewState["tblemp"] = ds5.Tables[0];
            this.GetComASecSelected();

        }
        protected void ddlPEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetComASecSelected();
        }
        private void GetComASecSelected()
        {
            string empid = this.ddlPEmpName.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)ViewState["tblemp"];
    
            DataRow[] dr = dt.Select("empid = '" + empid + "'");
            if (dr.Length > 0)
            {   
                //for multiple compane (Rakib)
                if (GetCompCode() == "3315")
                {
                    string Company = "%"+ dt.Rows[0]["deptcode"].ToString().Substring(0, 4)+"%";
                    string txtSProject = this.txtsrchdeptagg.Text.Trim() + "%";
                    DataSet ds4 = HRData.GetTransInfo(GetCompCode(), "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEPTNAMENEW", Company, txtSProject, "", "", "", "", "", "", "");
                    this.ddldepartmentagg.DataTextField = "deptdesc";
                    this.ddldepartmentagg.DataValueField = "deptcode";
                    this.ddldepartmentagg.DataSource = ds4.Tables[0];
                    this.ddldepartmentagg.DataBind();
                }
                this.ddlCompanyAgg.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["companycode"].ToString();
                this.ddldepartmentagg.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["deptcode"].ToString();
                this.ddlProjectName.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["refno"].ToString();
            }
        }


        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblDesgination.Text = this.ddlDesignation.SelectedItem.Text;
        }
        protected void ddlOffintime_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbloffintime.Text = this.ddlOffintime.SelectedItem.Text;
        }
        protected void ddlOffouttime_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbloffouttime.Text = this.ddlOffouttime.SelectedItem.Text;

        }
        protected void ddlLanintime_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbllanintime.Text = this.ddlLanintime.SelectedItem.Text;

        }
        protected void ddlLanouttime_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbllanouttime.Text = this.ddlLanouttime.SelectedItem.Text;
        }
        protected void ddlEduQua_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblEduQua.Text = (this.ddlEduQua.Items.Count == 0) ? "" : this.ddlEduQua.SelectedItem.Text;
        }
        protected void ddlProQua_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblAtype.Text = this.ddlAggrement.SelectedItem.Text;
        }
        protected void lbtnTSalAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblsaladd"];

            for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
            {
                double txtsal = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());
                dt.Rows[i]["gval"] = txtsal;
            }
            Session["tblsaladd"] = dt;
            this.FooterCalculation(dt, "gvSalAdd");
            this.TSandAllow();
            this.OverTimeFORRate();
        }
        protected void lbtnTSalSub_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblsalsub"];

            for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
            {
                double txtsal = Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim());
                dt.Rows[i]["gval"] = txtsal;
            }
            Session["tblsalsub"] = dt;
            this.FooterCalculation(dt, "gvSalSub");
            this.TSandAllow();

        }
        protected void lbtnTAllowAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblallowadd"];

            for (int i = 0; i < this.gvAllowAdd.Rows.Count; i++)
            {

                double txtrate = Convert.ToDouble("0" + ((TextBox)this.gvAllowAdd.Rows[i].FindControl("txtgvAllowAdd")).Text.Trim());
                dt.Rows[i]["rate"] = txtrate;
            }
            Session["tblallowadd"] = dt;
            this.gvAllowAdd.DataSource = dt;
            this.gvAllowAdd.DataBind();
            //this.FooterCalculation(dt, "gvAllowAdd");
            //this.TSandAllow();
        }
        protected void lbtnTAllowSub_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblallowsub"];

            for (int i = 0; i < this.gvAllowSub.Rows.Count; i++)
            {

                double txtallrate = Convert.ToDouble("0" + ((TextBox)this.gvAllowSub.Rows[i].FindControl("txtgvAllowSub")).Text.Trim());
                dt.Rows[i]["rate"] = txtallrate;
            }


            Session["tblallowsub"] = dt;
            this.gvAllowSub.DataSource = dt;
            this.gvAllowSub.DataBind();

        }

        protected void lnkbtnFinalSWUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                DataTable dtuser = (DataTable)Session["UserLog"];
                string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
                string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
                string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
                string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                //string tblEditByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editbyid"].ToString();
                //string tblEditDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["editdat"]).ToString("dd-MMM-yyyy");
                //string tblEdittrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editrmid"].ToString();

                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string PostedByid = (tblPostedByid == "") ? userid : tblPostedByid;
                string Posttrmid = (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
                string PostSession = (tblPostedSession == "") ? Sessionid : tblPostedSession;
                string Posteddat = (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
                string EditByid = (dtuser.Rows.Count == 0) ? "" : userid;
                string Editdat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");
                string Editrmid = (dtuser.Rows.Count == 0) ? "" : Terminal;

                //userid,Editrmid

                //-------------------------////

                string projectcode = this.ddlProjectName.SelectedValue.ToString();

                if (projectcode == "000000000000")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select Section !!!!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }

                string empid = (this.ddlNPEmpName.Items.Count > 0) ? this.ddlNPEmpName.SelectedValue.ToString() : this.ddlPEmpName.SelectedValue.ToString();
                string desigid = this.ddlDesignation.SelectedValue.ToString();
                string designame = this.ddlDesignation.SelectedItem.Text;
                string offinid = this.ddlOffintime.SelectedValue.ToString();
                string offintime = this.ddlOffintime.SelectedItem.Text;
                string offoutid = this.ddlOffouttime.SelectedValue.ToString();
                string offouttime = this.ddlOffouttime.SelectedItem.Text;
                string laninid = this.ddlLanintime.SelectedValue.ToString();
                string lanintime = this.ddlLanintime.SelectedItem.Text;
                string lanoutid = this.ddlLanouttime.SelectedValue.ToString();
                string lanouttime = this.ddlLanouttime.SelectedItem.Text;
                string eduid = (this.ddlEduQua.Items.Count == 0) ? "" : this.ddlEduQua.SelectedValue.ToString();
                string education = (this.ddlEduQua.Items.Count == 0) ? "" : this.ddlEduQua.SelectedItem.Text;
                string agtypeid = this.ddlAggrement.SelectedValue.ToString();
                string agtype = this.ddlAggrement.SelectedItem.Text;
                string paytype = this.rbtPaymentType.SelectedIndex.ToString();
                string paytypedesc = this.rbtPaymentType.SelectedValue.ToString();

                string bankname1 = this.ddlBankName1.SelectedValue.ToString();
                string bankname2 = this.ddlBankName2.SelectedValue.ToString();
                string bankacno1 = this.txtAcNo1.Text;
                string bankacno2 = this.txtAcNo2.Text;
                string routing1 = this.txtroute.Text;
                string routing2 = this.txtroute2.Text;
                string bank1 = (paytype == "0" || paytype == "2") ? "" : bankname1;
                string acno1 = (paytype == "0" || paytype == "2") ? "" : bankacno1;
                string bank2 = (paytype == "0" || paytype == "2") ? "" : bankname2;
                string acno2 = (paytype == "0" || paytype == "2") ? "" : bankacno2;
                string bankamt2 = (paytype == "0") ? "0" : Convert.ToDouble("0" + this.txtBankamt02.Text.Trim()).ToString();
                string cashamt = Convert.ToDouble("0" + this.txtCashAmt.Text.Trim()).ToString();
                string txtedupass = "Passing Year";
                string edupass = this.txtEduPass.Text.Trim();
                string holidaytype = (this.rbtholiday.SelectedIndex).ToString();
                string overtimetype = (this.rbtnOverTime.SelectedIndex).ToString();
                string pfdate = (this.txtPf.Text.Trim() == "") ? "01-jan-1900" : Convert.ToDateTime(txtPf.Text.Trim()).ToString("dd-MMM-yyyy");
                string pfenddat = (txtpfend.Text.Trim() == "") ? "01-jan-1900" : Convert.ToDateTime(txtpfend.Text.Trim()).ToString("dd-MMM-yyyy");
                string cash0Bank1 = this.chkcash0bank1.Checked ? "1" : "0";

                bool result;
                //(Convert.ToDateTime(ds6.Tables[5].Rows[0]["pfenddat"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds6.Tables[5].Rows[0]["pfenddat"]).ToString("dd-MMM-yyyy");
                //string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt")
                // string Posteddat = (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
                /////////-------------------Log----------------------///////

                result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTUPDATEAGG", empid, PostedByid, PostSession, Posttrmid, Posteddat,
                        EditByid, Editdat, Editrmid, "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (result == false)
                    return;


                //////---------------------------------------------////////////



                result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMPIDANDREFNO", empid, "94%", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Updated";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, desigid, "T", designame, projectcode, "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "0", "", "01-jan-1900", "01-jan-1900", "", "", "", "", "", "", "", "", "", userid, Editrmid);
                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Updated";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, offinid, "D", "01-Jan-1900 " + offintime, projectcode, "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "0", "", "01-jan-1900", "01-jan-1900", "", "", "", "", "", "", "", "", "", userid, Editrmid);
                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Updated";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, offoutid, "D", "01-Jan-1900 " + offouttime, projectcode, "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "0", "", "01-jan-1900", "01-jan-1900", "", "", "", "", "", "", "", "", "", userid, Editrmid);
                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Updated";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, laninid, "D", "01-Jan-1900 " + lanintime, projectcode, "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "0", "", "01-jan-1900", "01-jan-1900", "", "", "", "", "", "", "", "", "", userid, Editrmid);
                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Updated";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, lanoutid, "D", "01-Jan-1900 " + lanouttime, projectcode, "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "0", "", "01-jan-1900", "01-jan-1900", "", "", "", "", "", "", "", "", "", userid, Editrmid);
                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Updated";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                if (comcod != "4330")

                    result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, eduid, "T", education, projectcode, txtedupass, edupass, "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "0", "", "01-jan-1900", "01-jan-1900", "", "", "", "", "", "", "", "", "", userid, Editrmid);
                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Updated";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, agtypeid, "T", agtype, projectcode, "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "0", "", "01-jan-1900", "01-jan-1900","","","","","","","","","", userid, Editrmid);
                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Updated";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                // Bank COde
                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, "19001", "T", bank1, projectcode, "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", acno1, bank2, acno2, bankamt2, "0", cashamt, "", "01-jan-1900", "01-jan-1900", "", "", "", paytypedesc, "", cash0Bank1, "", routing1, routing2, userid, Editrmid);
                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Updated";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                ////only Peb steel cheque payment

                //result = HRData.UpdateTransHREMPInfo3(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, "19001", "T", bank1, projectcode, "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", acno1, bank2, acno2, bankamt2, "0", cashamt, "", "01-jan-1900", "01-jan-1900", "", "", "", paytypedesc);
                //if (result == false)
                //{
                //    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Updated";
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                //    return;
                //}




                // PF Start Date
                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, "20001", "D", pfdate, projectcode, "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", acno2, "0", "0", cashamt, "", "01-jan-1900", "01-jan-1900", "", "", "", "", "", "", "", "", "", userid, Editrmid);
                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Updated";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                //PF END DATE
                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, "20002", "D", pfenddat, projectcode, "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", acno2, "0", "0", cashamt, "", "01-jan-1900", "01-jan-1900", "", "", "", "", "", "", "", "", "", userid, Editrmid);
                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Updated";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }



                DataTable dtsaladd = (DataTable)Session["tblsaladd"];
                DataTable dtsalsub = (DataTable)Session["tblsalsub"];
                DataTable dtallowadd = (DataTable)Session["tblallowadd"];
                DataTable dtallowsub = (DataTable)Session["tblallowsub"];
                DataTable tblTimeSlot = (DataTable)Session["tblTimeSlot"];



                string holidayrate = (this.rbtholiday.SelectedIndex == 0) ? "0" : (this.rbtholiday.SelectedIndex == 2) ? Convert.ToDouble("0" + this.txtholidayallowance.Text.Trim()).ToString() : (Math.Round((Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"]) / 31), 0)).ToString();

                string fixedrate = Convert.ToDouble("0" + this.txtfixedRate.Text.Trim()).ToString();



                double gssal = Convert.ToDouble((Convert.IsDBNull(dtsaladd.Compute("Sum(gval)", "")) ? 0.00 : dtsaladd.Compute("Sum(gval)", "")));
                double dhourlyrate = 0.00;
                double bsal = 0.00, dailallow = 0.00;
                switch (comcod)
                {
                    case "3347":
                        bsal = Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"]);
                        dailallow = Convert.ToDouble((dtsaladd.Select("gcod='04012'"))[0]["gval"]);
                        dhourlyrate = Convert.ToDouble("0" + this.txtdevided.Text.Trim()) > 0 ? ((bsal + dailallow) * 2) / Convert.ToDouble("0" + this.txtdevided.Text.Trim()) : 0;
                        break;


                    case "3368"://Finlay
                    case "3101"://Finlay
                        bsal = Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"]);
                        dhourlyrate = Convert.ToDouble("0" + this.txtdevided.Text.Trim()) > 0 ? (bsal * 1.5) / Convert.ToDouble("0" + this.txtdevided.Text.Trim()) : 0;
                        break;


                    case "3336":
                        dhourlyrate = Convert.ToDouble("0" + this.txtdevided.Text.Trim()) > 0 ? gssal / Convert.ToDouble("0" + this.txtdevided.Text.Trim()) : 0;
                        break;


                    default:
                        dhourlyrate = Convert.ToDouble("0" + this.txtdevided.Text.Trim()) > 0 ? (Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"])) / Convert.ToDouble("0" + this.txtdevided.Text.Trim()) : 0;
                        break;



                }

                this.lblforrate.Text = "Rate:<span class='color:blue !important'>" + dhourlyrate.ToString("#,##0;(#,##0); ") + "</span>";


                string hourlyrate = (this.rbtnOverTime.SelectedIndex == 2) ? (dhourlyrate).ToString("#,##0.00;(#,##0.00); ") : (this.rbtnOverTime.SelectedIndex == 1) ? Convert.ToDouble("0" + this.txthourlyRate.Text.Trim()).ToString() : "0";

                //string hourlyrate = (this.rbtnOverTime.SelectedIndex == 2) ? Math.Round(dhourlyrate, 0).ToString() : (this.rbtnOverTime.SelectedIndex == 1) ? Convert.ToDouble("0" + this.txthourlyRate.Text.Trim()).ToString() : "0";
                string ceilingrate1 = Convert.ToDouble("0" + this.txtceilingRate1.Text.Trim()).ToString();
                string ceilingrate2 = Convert.ToDouble("0" + this.txtceilingRate2.Text.Trim()).ToString();
                string ceilingrate3 = Convert.ToDouble("0" + this.txtceilingRate3.Text.Trim()).ToString();


                int i;
                string gcode, gtype, gval, percnt, unit, qty, rate;

                for (i = 0; i < dtsaladd.Rows.Count; i++)
                {
                    gcode = dtsaladd.Rows[i]["gcod"].ToString();
                    gtype = dtsaladd.Rows[i]["gtype"].ToString();
                    gval = dtsaladd.Rows[i]["gval"].ToString();
                    percnt = dtsaladd.Rows[i]["percnt"].ToString();
                    result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, gcode, gtype, gval, projectcode, "", "", "", "", percnt, "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "0", "", "01-jan-1900", "01-jan-1900", "", "", "", "", "", "", "", "", "", userid, Editrmid);
                }

                for (i = 0; i < dtsalsub.Rows.Count; i++)
                {
                    gcode = dtsalsub.Rows[i]["gcod"].ToString();
                    gtype = dtsalsub.Rows[i]["gtype"].ToString();
                    gval = dtsalsub.Rows[i]["gval"].ToString();
                    percnt = dtsalsub.Rows[i]["percnt"].ToString();
                    result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, gcode, gtype, gval, projectcode, "", "", "", "", percnt, "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "0", "", "01-jan-1900", "01-jan-1900", "", "", "", "", "", "", "", "", "", userid, Editrmid);
                }
                for (i = 0; i < dtallowadd.Rows.Count; i++)
                {
                    gcode = dtallowadd.Rows[i]["gcod"].ToString();
                    gtype = dtallowadd.Rows[i]["gtype"].ToString();
                    gval = dtallowadd.Rows[i]["gval"].ToString();
                    percnt = dtallowadd.Rows[i]["percnt"].ToString();
                    unit = dtallowadd.Rows[i]["unit"].ToString();
                    qty = dtallowadd.Rows[i]["qty"].ToString();
                    rate = dtallowadd.Rows[i]["rate"].ToString();

                    result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, gcode, gtype, gval, projectcode, "", "", "", "", percnt, unit, qty, rate, "0", "0", "0", "0", "", "", "", "0", "0", "0", "", "01-jan-1900", "01-jan-1900", "", "", "", "", "", "", "", "", "", userid, Editrmid);
                }

                // Overtime
                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, "07004", "N", "0", projectcode, overtimetype, "", "", "", "0", "", "0", fixedrate,
               hourlyrate, ceilingrate1, ceilingrate2, ceilingrate3, "", "", "", "0", "0", "0", "", "01-jan-1900", "01-jan-1900", "", "", "", "", "", "", "", "", "", userid, Editrmid);

                //holiday rate
                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, "07005", "N", "0", projectcode, holidaytype, "", "", "", "0", "", "0", holidayrate
                    , "0", "0", "0", "0", "", "", "", "0", "0", "0", "", "01-jan-1900", "01-jan-1900", "", "", "", "", "", "", "", "", "", userid, Editrmid);

                ///Time sloat Insert 
                string slothour; string otrate;
                result = HRData.UpdateTransHREMPInfo3(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETETIMESSLOT", empid, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                for (i = 0; i < tblTimeSlot.Rows.Count; i++)
                {
                    slothour = Convert.ToDouble("0" + tblTimeSlot.Rows[i]["slothour"]).ToString();
                    otrate = Convert.ToDouble("0" + tblTimeSlot.Rows[i]["otrate"]).ToString(); 

                    result = HRData.UpdateTransHREMPInfo3(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTTIMESSLOT", empid, slothour, otrate, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                }


                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Updated";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }



                for (i = 0; i < dtallowsub.Rows.Count; i++)
                {
                    gcode = dtallowsub.Rows[i]["gcod"].ToString();
                    gtype = dtallowsub.Rows[i]["gtype"].ToString();
                    gval = dtallowsub.Rows[i]["gval"].ToString();
                    percnt = dtallowsub.Rows[i]["percnt"].ToString();
                    unit = dtallowsub.Rows[i]["unit"].ToString();
                    qty = dtallowsub.Rows[i]["qty"].ToString();
                    rate = dtallowsub.Rows[i]["rate"].ToString();
                    result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, gcode, gtype, gval, projectcode, "", "", "", "", percnt, unit, qty, rate, "0", "0", "0", "0", "", "", "", "0", "0", "0", "", "01-jan-1900", "01-jan-1900", "", "", "", "", "", "", "", "", "", userid, Editrmid);
                }

                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


                string eventtype = "Change Aggrement";
                string eventdesc = empid;
                string eventdesc2 = "Change somethings";

                if (ConstantInfo.LogStatus == true)
                {
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);

            }



        }
        protected void chknewEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chknewEmp.Checked)
            {
                this.lblEmp.Visible = false;
                this.txtSrcEmp.Visible = false;
                this.ibtnFindEmp.Visible = false;
                this.ddlPEmpName.Visible = false;
                this.ibtnNFindEmp_Click(null, null);
                this.lblnewEmp.Visible = true;
                this.txtNSrcEmp.Visible = true;
                this.ibtnNFindEmp.Visible = true;
                this.ddlNPEmpName.Visible = true;

            }
            else
            {

                this.lblEmp.Visible = true;
                this.txtSrcEmp.Visible = true;
                this.ibtnFindEmp.Visible = true;
                this.ddlPEmpName.Visible = true;
                this.lblnewEmp.Visible = false;
                this.txtNSrcEmp.Visible = false;
                this.ibtnNFindEmp.Visible = false;
                this.ddlNPEmpName.Visible = false;

            }


        }









        protected void lbtnCalculation_Click(object sender, EventArgs e)
        {
            DataTable dtsaladd = (DataTable)Session["tblsaladd"];
            DataTable dtsalsub = (DataTable)Session["tblsalsub"];
            double basic, gross, percent, na;
            string code;
            string comcod = this.GetCompCode();

            //Gross Salary Sanmar  62.50
            gross = Convert.ToDouble("0" + this.txtgrossal.Text.Trim());
            na = gross + Convert.ToDouble("0" + this.txtCashAmt.Text.Trim());
            if (this.rbtGross.SelectedIndex == 0)
            {
                for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                {
                    code = ((Label)this.gvSalAdd.Rows[i].FindControl("lblgvItmCodesaladd")).Text.Trim();
                    percent = (code == "04001") ? 62.50 : (code == "04002") ? 40.00 : (code == "04003") ? 10.00 : (code == "04004") ? 10.00 : 0.00;

                    if (i == 0)
                    {
                        dtsaladd.Rows[i]["gval"] = Math.Round((gross * 0.01 * percent), 0);
                        dtsaladd.Rows[i]["percnt"] = percent;
                        continue;
                    }

                    dtsaladd.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"]) * 0.01), 0)
                                                : Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());
                    dtsaladd.Rows[i]["percnt"] = percent;
                }


                for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                {
                    code = ((Label)this.gvSalSub.Rows[i].FindControl("lblgvItmCodesalsub")).Text.Trim();
                    percent = (code == "04101") ? 5.00 : 0.00;
                    dtsalsub.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * 0.01 * Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"])), 0)
                            : Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim());
                    dtsalsub.Rows[i]["percnt"] = percent;
                }
            }


            //sanmar 55.55
            else if (this.rbtGross.SelectedIndex == 1)
            {
                for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                {
                    code = ((Label)this.gvSalAdd.Rows[i].FindControl("lblgvItmCodesaladd")).Text.Trim();
                    percent = (code == "04001") ? 55.55 : (code == "04002") ? 40.00 : (code == "04003") ? 20.00 : (code == "04004") ? 20.00 : 0.00;

                    if (i == 0)
                    {
                        dtsaladd.Rows[i]["gval"] = Math.Round((gross * 0.01 * percent), 0);
                        dtsaladd.Rows[i]["percnt"] = percent;
                        continue;
                    }

                    dtsaladd.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"]) * 0.01), 0)
                            : Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());
                    dtsaladd.Rows[i]["percnt"] = percent;
                }


                for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                {
                    code = ((Label)this.gvSalSub.Rows[i].FindControl("lblgvItmCodesalsub")).Text.Trim();
                    percent = (code == "04101") ? 5.00 : 0.00;
                    dtsalsub.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * 0.01 * Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"])), 0)
                            : Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim());
                    dtsalsub.Rows[i]["percnt"] = percent;
                }


            }

            else if (this.rbtGross.SelectedIndex == 2)
            {

                // Additional Part
                switch (comcod)
                {
                    case "3347":

                        string bcode = this.ddlProjectName.SelectedValue.Substring(0, 4);
                        double cashamt = Convert.ToDouble("0" + this.txtCashAmt.Text.Trim());

                        if (bcode == "9401" && cashamt > 0) // if salary in both(cash and bank)
                        {
                            for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                            {
                                code = ((Label)this.gvSalAdd.Rows[i].FindControl("lblgvItmCodesaladd")).Text.Trim();
                                percent = (code == "04001") ? 40.00 : (code == "04002") ? 20.00 : (code == "04004") ? 15.00 : (code == "04012") ? 5.00 : 0.00;

                                double cgross = (code == "04001") ? na : gross;
                                dtsaladd.Rows[i]["gval"] = percent > 0 ? Math.Round((cgross * 0.01 * percent), 0) : Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());

                                //dtsaladd.Rows[i]["gval"] = Math.Round((gross * 0.01 * percent), 0);
                                dtsaladd.Rows[i]["percnt"] = percent;

                            }

                            double toaddamt = Convert.ToDouble((Convert.IsDBNull(dtsaladd.Compute("sum(gval)", "")) ? 0 : dtsaladd.Compute("sum(gval)", "")));
                            // Medical Allowance

                            double nmedical = Convert.ToDouble(dtsaladd.Select("gcod='04004'")[0]["gval"]) - (toaddamt - gross);
                            (dtsaladd.Select("gcod='04004'"))[0]["gval"] = Convert.ToDouble(dtsaladd.Select("gcod='04004'")[0]["gval"]) - (toaddamt - gross);
                        }
                        //else if (bcode == "9401" && cashamt == 0) 
                        //    for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                        //    {
                        //        percent = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvgperadd")).Text.Trim());
                        //        dtsaladd.Rows[i]["gval"] = percent > 0 ? Math.Round((gross * 0.01 * percent), 0) : Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());

                        //        //dtsaladd.Rows[i]["gval"] = Math.Round((gross * 0.01 * percent), 0);
                        //        dtsaladd.Rows[i]["percnt"] = percent;

                        //    }

                        else // if salary in  factory and heaadoffice cash / bank

                            for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                            {
                                percent = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvgperadd")).Text.Trim());
                                dtsaladd.Rows[i]["gval"] = percent > 0 ? Math.Round((gross * 0.01 * percent), 0) : Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());

                                //dtsaladd.Rows[i]["gval"] = Math.Round((gross * 0.01 * percent), 0);
                                dtsaladd.Rows[i]["percnt"] = percent;

                            }





                        break;

                    default:
                        for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                        {
                            percent = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvgperadd")).Text.Trim());
                            dtsaladd.Rows[i]["gval"] = percent > 0 ? Math.Round((gross * 0.01 * percent), 0) : Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());

                            //dtsaladd.Rows[i]["gval"] = Math.Round((gross * 0.01 * percent), 0);
                            dtsaladd.Rows[i]["percnt"] = percent;

                        }
                        break;



                }

                // Deduction Part
                switch (comcod)
                {
                    case "3347":

                        string bcode = this.ddlProjectName.SelectedValue.Substring(0, 4);
                        if (bcode == "9401")
                        {
                            for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                            {
                                code = ((Label)this.gvSalSub.Rows[i].FindControl("lblgvItmCodesalsub")).Text.Trim();
                                percent = (code == "04101") ? 10 : 0.00;
                                //  percent = Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvgpersub")).Text.Trim());
                                dtsalsub.Rows[i]["gval"] = percent > 0 ? Math.Round((Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"]) * 0.01 * percent), 0) : Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim()); ;
                                //  dtsalsub.Rows[i]["gval"] = Math.Round((percent * 0.01 * gross), 0);
                                dtsalsub.Rows[i]["percnt"] = percent;

                            }
                        }

                        else
                        {

                            for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                            {

                                percent = Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvgpersub")).Text.Trim());
                                dtsalsub.Rows[i]["gval"] = percent > 0 ? Math.Round((Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"]) * 0.01 * percent), 0) : Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim()); ;
                                //  dtsalsub.Rows[i]["gval"] = Math.Round((percent * 0.01 * gross), 0);
                                dtsalsub.Rows[i]["percnt"] = percent;

                            }

                        }

                        break;

                    default:

                        for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                        {
                            percent = Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvgpersub")).Text.Trim());
                            dtsalsub.Rows[i]["gval"] = percent > 0 ? Math.Round((gross * 0.01 * percent), 0) : Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim()); ;
                            //  dtsalsub.Rows[i]["gval"] = Math.Round((percent * 0.01 * gross), 0);
                            dtsalsub.Rows[i]["percnt"] = percent;

                        }

                        break;



                }





            }

            // basic salary acme
            else if (this.rbtGross.SelectedIndex == 3)
            {
                basic = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[0].FindControl("txtgvSaladd")).Text.Trim());

                for (int i = 1; i < this.gvSalAdd.Rows.Count; i++)
                {
                    percent = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvgperadd")).Text.Trim());
                    dtsaladd.Rows[i]["gval"] = Math.Round((percent * basic * 0.01), 0);
                    dtsaladd.Rows[i]["percnt"] = percent;
                }


                if ((comcod == "3338") || (comcod == "1206") || (comcod == "1207"))
                {
                    dtsaladd.Rows[0]["gval"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[0].FindControl("txtgvSaladd")).Text.Trim());
                }

                for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                {
                    percent = Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvgpersub")).Text.Trim());
                    dtsalsub.Rows[i]["gval"] = Math.Round((percent * 0.01 * basic), 0);
                    dtsalsub.Rows[i]["percnt"] = percent;
                }
            }

            ///-------------------------GLG
            else if (this.rbtGross.SelectedIndex == 4)
            {
                for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                {
                    code = ((Label)this.gvSalAdd.Rows[i].FindControl("lblgvItmCodesaladd")).Text.Trim();
                    percent = (code == "04001") ? 50 : (code == "04002") ? 25.00 : (code == "04003") ? 12.50 : (code == "04004") ? 6.25 : (code == "04012") ? 6.25 : 0.00;

                    //if (i == 0)
                    //{
                    dtsaladd.Rows[i]["gval"] = Math.Round((gross * 0.01 * percent), 0);
                    dtsaladd.Rows[i]["percnt"] = percent;
                    //    continue;
                    //}

                    //dtsaladd.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"]) * 0.01), 0)
                    //        : Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());
                    //dtsaladd.Rows[i]["percnt"] = percent;
                }
            }

            else if (this.rbtGross.SelectedIndex == 5)
            {
                for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                {
                    code = ((Label)this.gvSalAdd.Rows[i].FindControl("lblgvItmCodesaladd")).Text.Trim();

                    percent = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvgperadd")).Text.Trim());

                    if (i == 0)
                    {
                        dtsaladd.Rows[i]["gval"] = Math.Round((gross * 0.01 * percent), 0);
                        dtsaladd.Rows[i]["percnt"] = percent;
                        continue;
                    }



                    dtsaladd.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"]) * 0.01), 0)
                            : Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());
                    dtsaladd.Rows[i]["percnt"] = percent;
                }



                for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                {
                    //code = ((Label)this.gvSalSub.Rows[i].FindControl("lblgvItmCodesalsub")).Text.Trim();
                    percent = Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvgpersub")).Text.Trim());
                    dtsalsub.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * 0.01 * Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"])), 0)
                            : Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim());
                    //  dtsalsub.Rows[i]["gval"] = Math.Round((gross * 0.01 * percent), 0);
                    dtsalsub.Rows[i]["percnt"] = percent;
                }


            }


            else if (this.rbtGross.SelectedIndex == 6) //Tropical Homese Ltd.
            {
                for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                {
                    code = ((Label)this.gvSalAdd.Rows[i].FindControl("lblgvItmCodesaladd")).Text.Trim();

                    percent = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvgperadd")).Text.Trim());

                    if (i == 0)
                    {
                        dtsaladd.Rows[i]["gval"] = Math.Round((gross / 1.4), 2);
                        dtsaladd.Rows[i]["percnt"] = percent;
                        continue;
                    }



                    dtsaladd.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"]) * 0.01), 2)
                            : Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());
                    dtsaladd.Rows[i]["percnt"] = percent;
                }



                for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                {
                    //code = ((Label)this.gvSalSub.Rows[i].FindControl("lblgvItmCodesalsub")).Text.Trim();
                    percent = Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvgpersub")).Text.Trim());
                    dtsalsub.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * 0.01 * Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"])), 2)
                            : Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim());
                    //  dtsalsub.Rows[i]["gval"] = Math.Round((gross * 0.01 * percent), 0);
                    dtsalsub.Rows[i]["percnt"] = percent;
                }


            }


            // basic salary cpdl
            else if (this.rbtGross.SelectedIndex == 7)
            {
                basic = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[0].FindControl("txtgvSaladd")).Text.Trim());
                double grsal = Convert.ToDouble("0" + this.txtgrossal.Text.Trim());
                double totaladd = 0.0;



                for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                {
                    string gcod = ((Label)this.gvSalAdd.Rows[i].FindControl("lblgvItmCodesaladd")).Text.ToString();
                    if (gcod == "04002" || gcod == "04004")
                    {
                        percent = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvgperadd")).Text.Trim());
                        dtsaladd.Rows[i]["gval"] = Math.Round((percent * basic * 0.01), 0);
                        dtsaladd.Rows[i]["percnt"] = percent;
                    }
                    else
                    {
                        dtsaladd.Rows[i]["gval"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());
                        dtsaladd.Rows[i]["percnt"] = 0.0;

                    }
                }

                for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                {
                    string gcod = ((Label)this.gvSalAdd.Rows[i].FindControl("lblgvItmCodesaladd")).Text.ToString();
                    if (gcod != "04012")
                    {
                        totaladd = totaladd + Convert.ToDouble(dtsaladd.Rows[i]["gval"]);
                    }

                }


                for (int i = 1; i < this.gvSalAdd.Rows.Count; i++)
                {
                    string gcod = ((Label)this.gvSalAdd.Rows[i].FindControl("lblgvItmCodesaladd")).Text.ToString();
                    if (gcod == "04012")
                    {
                        dtsaladd.Rows[i]["gval"] = grsal - totaladd;

                    }
                    else if (gcod == "04020" || gcod == "04099")
                    {
                        dtsaladd.Rows[i]["gval"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());

                    }

                }






                if (comcod == "3370")
                {
                    dtsaladd.Rows[0]["gval"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[0].FindControl("txtgvSaladd")).Text.Trim());
                }

                for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                {
                    string gcod = ((Label)this.gvSalSub.Rows[i].FindControl("lblgvItmCodesalsub")).Text.ToString();
                    if (gcod == "04101")
                    {
                        percent = Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvgpersub")).Text.Trim());
                        dtsalsub.Rows[i]["gval"] = Math.Round((percent * 0.01 * basic), 0);
                        dtsalsub.Rows[i]["percnt"] = percent;
                    }
                    else
                    {
                        dtsalsub.Rows[i]["gval"] = Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim());
                    }

                }
            }

            // txtgvSalSub
            // Basic Salary
            else
            {
                basic = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[0].FindControl("txtgvSaladd")).Text.Trim());

                for (int i = 1; i < this.gvSalAdd.Rows.Count; i++)
                {
                    percent = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvgperadd")).Text.Trim());
                    dtsaladd.Rows[i]["gval"] = Math.Round((percent * basic * 0.01), 0);
                    dtsaladd.Rows[i]["percnt"] = percent;

                }


                for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                {
                    percent = Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvgpersub")).Text.Trim());
                    dtsalsub.Rows[i]["gval"] = Math.Round((percent * 0.01 * basic), 0);
                    dtsalsub.Rows[i]["percnt"] = percent;


                }


            }

            //////////////////////////////
            Session["tblsaladd"] = dtsaladd;
            Session["tblsalsub"] = dtsalsub;
            this.gvSalAdd.DataSource = dtsaladd;
            this.gvSalAdd.DataBind();
            this.gvSalSub.DataSource = dtsalsub;
            this.gvSalSub.DataBind();


            //  string comcod = this.GetCompCode();


            switch (comcod)
            {
                case "3335":
                case "3339":

                    for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                    {
                        ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");


                    }

                    for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                    {
                        ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim())).ToString("#,##0.00;-#,##0.00; ");


                    }

                    break;

                default:
                    break;



            }



            this.FooterCalculation(dtsaladd, "gvSalAdd");
            this.FooterCalculation(dtsalsub, "gvSalSub");
            this.TSandAllow();
            this.OverTimeFORRate();


        }

        protected void lbtnDeletelink_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string Section = this.ddlProjectName.SelectedValue.ToString();
            if (this.ddlPEmpName.Items.Count == 0)
                return;
            string EmpName = this.ddlPEmpName.SelectedValue.ToString();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMPIDANDREFNO", EmpName, Section, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Unlink Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


           ((Label)this.Master.FindControl("lblmsg")).Text = "Unlink Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            this.ddlProjectName_SelectedIndexChanged(null, null);
        }
        protected void rbtholiday_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtholidayallowance.Visible = (this.rbtholiday.SelectedIndex == 2);
            this.lblholidayallowance.Visible = (this.rbtholiday.SelectedIndex == 2);
        }


        protected void rbtnOverTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblfiexedRate.Visible = (this.rbtnOverTime.SelectedIndex == 0);
            this.txtfixedRate.Visible = (this.rbtnOverTime.SelectedIndex == 0);
            this.lblhourlyRate.Visible = (this.rbtnOverTime.SelectedIndex == 1);
            this.txthourlyRate.Visible = (this.rbtnOverTime.SelectedIndex == 1);
            this.PnlMultiply.Visible = (this.rbtnOverTime.SelectedIndex == 2);
            this.txtdevided.Visible = (this.rbtnOverTime.SelectedIndex == 2);

            this.lblCeilingRate1.Visible = (this.rbtnOverTime.SelectedIndex == 3);
            this.lblCeilingRate2.Visible = (this.rbtnOverTime.SelectedIndex == 3);
            this.lblCeilingRate3.Visible = (this.rbtnOverTime.SelectedIndex == 3);
            this.txtceilingRate1.Visible = (this.rbtnOverTime.SelectedIndex == 3);
            this.txtceilingRate2.Visible = (this.rbtnOverTime.SelectedIndex == 3);
            this.txtceilingRate3.Visible = (this.rbtnOverTime.SelectedIndex == 3);

            if (this.rbtnOverTime.SelectedIndex == 4)
            {
                this.pnlTimesslot.Visible = true;
                this.lblfiexedRate.Visible = false;
                this.txtfixedRate.Visible = false;

            }
            else
            {
                this.pnlTimesslot.Visible = false;
                this.lblfiexedRate.Visible = true;
                this.txtfixedRate.Visible = true;

            }

            if (this.rbtnOverTime.SelectedIndex == 0)
            {
                this.txthourlyRate.Text = "";
                this.txtceilingRate1.Text = "";
                this.txtceilingRate2.Text = "";
                this.txtceilingRate3.Text = "";

            }





            else if (this.rbtnOverTime.SelectedIndex == 1 || this.rbtnOverTime.SelectedIndex == 2)
            {
                this.txtfixedRate.Text = "";
                this.txtceilingRate1.Text = "";
                this.txtceilingRate2.Text = "";
                this.txtceilingRate3.Text = "";

            }

            else
            {
                this.txtfixedRate.Text = "";
                this.txthourlyRate.Text = "";


            }


            // 
            if (this.rbtnOverTime.SelectedIndex == 2)
                this.OverTimeFORRate();


        }

        protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {

            if (this.lnkbtnShow.Text == "Ok")
            {
                this.lnkbtnShow.Text = "New";
                this.lblDeptDesc.Text = this.ddlDepartment.SelectedItem.Text.Trim().Substring(13);
                this.ddlDepartment.Visible = false;
                this.lblDeptDesc.Visible = true;
                this.pnlOfftime.Visible = true;
                this.ShowOffTime();
                return;
            }

            this.lnkbtnShow.Text = "Ok";
            this.lblmsg2.Text = "";
            this.ddlDepartment.Visible = true;
            this.lblDeptDesc.Visible = false;
            this.pnlOfftime.Visible = false;
        }

        private void ShowOffTime()
        {

            string comcod = this.GetCompCode();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETOFFTIME", "", "", "", "", "", "", "", "", "");
            if (ds5 == null)
                return;

            this.ddlOffintimedw.DataTextField = "offintime";
            this.ddlOffintimedw.DataValueField = "offinid";
            this.ddlOffintimedw.DataSource = ds5.Tables[0];
            this.ddlOffintimedw.DataBind();

            this.ddlOffouttimedw.DataTextField = "offouttime";
            this.ddlOffouttimedw.DataValueField = "offoutid";
            this.ddlOffouttimedw.DataSource = ds5.Tables[1];
            this.ddlOffouttimedw.DataBind();

            this.ddlLanintimedw.DataTextField = "lanintime";
            this.ddlLanintimedw.DataValueField = "laninid";
            this.ddlLanintimedw.DataSource = ds5.Tables[2];
            this.ddlLanintimedw.DataBind();

            this.ddlLanouttimedw.DataTextField = "lanouttime";
            this.ddlLanouttimedw.DataValueField = "lanoutid";
            this.ddlLanouttimedw.DataSource = ds5.Tables[3];
            this.ddlLanouttimedw.DataBind();
            ds5.Dispose();


        }
        protected void lnkbtnUpdateOfftime_Click(object sender, EventArgs e)
        {


            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {



                case "Officetime":
                    this.UpdateOffTime();

                    break;


                case "shifttime":
                    this.UpdateShiftTime();

                    break;






            }

        }

        private void UpdateOffTime()
        {
            this.lblmsg2.Visible = true;

            string comcod = this.GetCompCode();
            string projectcode = this.ddlDepartment.SelectedValue.ToString();
            string company = this.ddlCompany.SelectedValue.Trim().Substring(0, 2);
            string offinid = this.ddlOffintimedw.SelectedValue.ToString();
            string offintime = this.ddlOffintimedw.SelectedItem.Text;
            string offoutid = this.ddlOffouttimedw.SelectedValue.ToString();
            string offouttime = this.ddlOffouttimedw.SelectedItem.Text;
            string laninid = this.ddlLanintimedw.SelectedValue.ToString();
            string lanintime = this.ddlLanintimedw.SelectedItem.Text;
            string lanoutid = this.ddlLanouttimedw.SelectedValue.ToString();
            string lanouttime = this.ddlLanouttimedw.SelectedItem.Text;
            bool result;

            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMPOFFTIME", projectcode, company, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                this.lblmsg2.Text = "Data Is Not Updated";
                return;
            }

            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPDATEHREMPOFFTIME", projectcode, offinid, "01-Jan-1900 " + offintime, company, "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                this.lblmsg2.Text = "Data Is Not Updated";
                return;
            }
            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPDATEHREMPOFFTIME", projectcode, offoutid, "01-Jan-1900 " + offouttime, company, "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                this.lblmsg2.Text = "Data Is Not Updated";
                return;
            }
            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPDATEHREMPOFFTIME", projectcode, laninid, "01-Jan-1900 " + lanintime, company, "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                this.lblmsg2.Text = "Data Is Not Updated";
                return;
            }
            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPDATEHREMPOFFTIME", projectcode, lanoutid, "01-Jan-1900 " + lanouttime, company, "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                this.lblmsg2.Text = "Data Is Not Updated";
                return;
            }

            this.lblmsg2.Text = "Updated Successfully";

        }


        private void UpdateShiftTime()
        {
            this.lblmsg2.Visible = true;

            DateTime frmdate, todate, offintime, today, addday, offouttime, lanintime, lanouttime, systime;

            string dayid;
            frmdate = Convert.ToDateTime(this.txtfromdate.Text);
            todate = Convert.ToDateTime(this.txttodate.Text);

            string comcod = this.GetCompCode();





            while (frmdate <= todate)
            {

                dayid = frmdate.ToString("yyyyMMdd");
                string company = this.ddlCompany.SelectedValue.Trim().Substring(0, 2) + "%";
                string Dept1 = (this.ddlDepartment.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDepartment.SelectedValue.ToString()) + "%";
                string projectcode = (this.ddlDepartment.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDepartment.SelectedValue.ToString()) + "%";

                offintime = Convert.ToDateTime(frmdate.ToString("dd-MMM-yyyy") + " " + this.ddlOffintimedw.SelectedItem.Text);
                systime = Convert.ToDateTime(frmdate.ToString("dd-MMM-yyyy") + " " + "08:00 PM");
                today = frmdate;
                addday = today.AddDays(1);
                offouttime = (systime <= offintime) ? Convert.ToDateTime(addday.ToString("dd-MMM-yyyy") + " " + this.ddlOffouttimedw.SelectedItem.Text) : Convert.ToDateTime(today.ToString("dd-MMM-yyyy") + " " + this.ddlOffouttimedw.SelectedItem.Text);
                lanintime = (systime <= offintime) ? Convert.ToDateTime(addday.ToString("dd-MMM-yyyy") + " " + this.ddlLanintimedw.SelectedItem.Text) : Convert.ToDateTime(today.ToString("dd-MMM-yyyy") + " " + this.ddlLanintimedw.SelectedItem.Text);
                lanouttime = (systime <= offintime) ? Convert.ToDateTime(addday.ToString("dd-MMM-yyyy") + " " + this.ddlLanouttimedw.SelectedItem.Text) : Convert.ToDateTime(today.ToString("dd-MMM-yyyy") + " " + this.ddlLanouttimedw.SelectedItem.Text);
                frmdate = frmdate.AddDays(1);




                bool result;

                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATESHIFTTIME", dayid, company, Dept1, projectcode, "", offintime.ToString(), offouttime.ToString(), lanintime.ToString(), lanouttime.ToString(), "", "", "", "", "", "");
                if (result == false)
                {
                    this.lblmsg2.Text = "Data Is Not Updated";
                    return;
                }

            }


            this.lblmsg2.Text = "Updated Successfully";

        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectNameOT();
        }
        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }


        protected void rbtAgreementType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.GetBankName();
        }
        protected void rbtPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rbtPaymentType.SelectedIndex == 1)
            {
                //this.GetBankName();
                this.pnlPaymenttype.Visible = true;
            }

            else if (this.rbtPaymentType.SelectedIndex == 2)
            {
                this.pnlPaymenttype.Visible = true;
            }


            else
            {
                this.pnlPaymenttype.Visible = false;
            }
        }
        protected void ddlBankName2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkNextbtn_Click(object sender, EventArgs e)
        {
            //bool tf = (this.chknewEmp.Checked == true);
            //string empid = (this.chknewEmp.Checked == true) ? this.ddlNPEmpName.SelectedValue.ToString() : this.ddlPEmpName.SelectedValue.ToString();
            string empid = (this.ddlNPEmpName.Items.Count > 0) ? this.ddlNPEmpName.SelectedValue.ToString() : this.ddlPEmpName.SelectedValue.ToString();

            this.lnkNextbtn.PostBackUrl = "ImgUpload.aspx?Type=Entry&empid=" + empid;



        }


        protected void ddldepartmentagg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.chkEdit.Checked)
                return;
            this.GetProjectName();
        }
        protected void chkEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkEdit.Checked)
            {

                this.ddlCompanyAgg.Visible = true;
                this.ddldepartmentagg.Visible = true;
                this.ddlProjectName.Visible = true;
                this.ddlPEmpName.Visible = true;
                this.lblCompanyNameAgg.Visible = false;
                this.lblvaldeptagg.Visible = false;
                this.lblProjectdesc.Visible = false;

                this.lblPEmpName.Visible = false;

            }

            else
            {


                this.ddlCompanyAgg.Visible = false;
                this.ddldepartmentagg.Visible = false;
                this.ddlProjectName.Visible = false;
                this.ddlPEmpName.Visible = false;
                this.lblCompanyNameAgg.Visible = true;
                this.lblvaldeptagg.Visible = true;
                this.lblProjectdesc.Visible = true;
                this.lblPEmpName.Visible = false;

            }
        }

        protected void chkcash0bank1_CheckedChanged(object sender, EventArgs e)
        {
            this.chkcash0bank1.Text = this.chkcash0bank1.Checked ? "Bank" : "Cash";
        }


        private string GetLastUSerID()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETLASTUSERID", "", "", "", "", "", "", "", "", "");
            string userid = ds1.Tables[0].Rows[0]["userid"].ToString();
            return (userid);
        }

        private void SlotOverTimeFORRate(string empid)
        {
            string comcod = this.GetCompCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPTIMESLOTDATA", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvTimsSlot.DataSource = null;
                this.gvTimsSlot.DataBind();
                return;
            }
            Session["tblTimeSlot"] = ds3.Tables[0];

            this.DataSlotOTBind();

        }

        private void DataSlotOTBind()
        {
            DataTable tbl1 = (DataTable)Session["tblTimeSlot"];

            this.gvTimsSlot.DataSource = tbl1;
            this.gvTimsSlot.DataBind();
        }
        private void CreateTableOverTimeFORRate()
        {
            DataTable tblTimeSlot = new DataTable();
            tblTimeSlot.Columns.Add("slothour", Type.GetType("System.Double"));
            tblTimeSlot.Columns.Add("otrate", Type.GetType("System.Double"));
            Session["tblTimeSlot"] = tblTimeSlot;
        }


        protected void lnkUserGenerate_Click(object sender, EventArgs e)
        {
            string empid = "";
            if (this.ddlNPEmpName.Items.Count > 0)
            {
                empid = this.ddlNPEmpName.SelectedValue.ToString();
            }
            else
            {
                empid = this.ddlPEmpName.SelectedValue.ToString();
            }

            if (empid == "")
                return;
            string Message;
            string msg;
            string comcod = this.GetCompCode();
            string usrid = this.GetLastUSerID();

            string usrfname = this.hiddnempname1.Value;
            string usrsname = this.hiddnCardId.Value;
            string usrdesig = this.ddlDesignation.SelectedItem.Text.ToString();
            string usrpass = "123";
            string usrrmrk = "";
            string active = "1";
            usrsname = (comcod == "3365" ? "bti" + usrsname : usrsname);
            string usermail = "";
            string webmailpwd = "";
            string userRole = "3";
            usrpass = (usrpass.Length == 0) ? "" : ASTUtility.EncodePassword(usrpass);
            bool result = HRData.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSORUPDATEUSR", usrid, usrsname,
                      usrfname, usrdesig, usrpass, usrrmrk, active, empid, usermail, webmailpwd, userRole, "UserProfile", "", "", "");
            if (!result)
            {
                msg = HRData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "New User Created Failed!" + "');", true);
                return;

            }

            //user page permission auto 
            result = HRData.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSERTPAGEPERMISSION_AUTO", usrid, "",
                   "", "", "", "", "", "", "", "", "", "", "", "", "");


            msg = "New User Created Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            this.GetEmpBasicData(empid);

            string eventtype = "User Login From";
            string eventdesc = "Update ID";
            string eventdesc2 = "Your profile Updated,";

            if (ConstantInfo.LogStatus == true)
            {
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            DataTable tbl1 = (DataTable)Session["tblTimeSlot"];
            string slothour = this.txtHourTimeSlot.Text;
            string SlototRae = this.txtRateTimeSlot.Text;
            DataRow[] dr2 = tbl1.Select("slothour = '" + slothour + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["slothour"] = Convert.ToDouble("0" + slothour.Trim());
                dr1["otrate"] = Convert.ToDouble("0" + SlototRae.Trim());
                tbl1.Rows.Add(dr1);
            }
            Session["tblTimeSlot"] = tbl1;
            this.DataSlotOTBind();
        }

        protected void TImeSlotlnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();

                DataTable dt = (DataTable)Session["tblTimeSlot"];
                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                
                    dt.Rows[index].Delete();
                
                Session["tblTimeSlot"] = dt;
                this.DataSlotOTBind();

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
    }
}

