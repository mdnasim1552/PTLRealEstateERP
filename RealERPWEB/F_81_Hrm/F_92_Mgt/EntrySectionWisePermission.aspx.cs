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


namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class EntrySectionWisePermission : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Project Permission";

                this.Getuser();
            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        protected void GetCompany()
        {
            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.ddlCompany_SelectedIndexChanged(null, null);
            ds1.Dispose();



        }

        private void GetDeptName()
        {

            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;



            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln)+"%";
    
            //string deptcode = (this.ddlBranch.SelectedValue.ToString() == "000000000000" ? Company : this.ddlBranch.SelectedValue.ToString().Substring(0, 4)) + "%";

            string txtSProject = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_ALLOWANCE", "GETDEPARTMENTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlDeptName.DataTextField = "actdesc";
            this.ddlDeptName.DataValueField = "actcode";
            this.ddlDeptName.DataSource = ds1.Tables[0];
            this.ddlDeptName.DataBind();
            this.ddlDeptName_SelectedIndexChanged(null, null);
            // this.SectionName();

        }
        private void SectionName()
        {

            string comcod = this.GetCompCode();
            string projectcode = this.ddlDeptName.SelectedValue.ToString() == "000000000000" ? "%%" : this.ddlDeptName.SelectedValue.ToString();

            string txtSSec = "%%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_ALLOWANCE", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();
            // this.GetEmpName();
            //ddlSection_SelectedIndexChanged(null, null);

        }





        //protected void Load_Project_Combo()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string FindProject = "%";
        //    DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "GETPRJLIST", FindProject, "", "", "", "", "", "", "", "");
        //    if (ds1 == null)
        //        return;

        //    this.ddlProjectName.DataTextField = "actdesc";
        //    this.ddlProjectName.DataValueField = "actcode";
        //    this.ddlProjectName.DataSource = ds1.Tables[0];
        //    this.ddlProjectName.DataBind();


        //}
        private void Getuser()
        {
            if (this.lbtnOk.Text == "New")
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt = "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "GETUSERNAME", mSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlUserList.DataTextField = "usrsname";
            this.ddlUserList.DataValueField = "usrid";
            this.ddlUserList.DataSource = ds1.Tables[0];
            this.ddlUserList.DataBind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {

                this.ddlUserList.Enabled = true;
                this.ddlDeptName.Enabled = true;
                this.ddlDeptName.Items.Clear();
                this.gvProLinkInfo.DataSource = null;
                this.gvProLinkInfo.DataBind();
                this.Panel2.Visible = false;
                this.lbtnOk.Text = "Ok";
                return;
            }



            this.ddlUserList.Enabled = false;

            this.Panel2.Visible = true;
            this.lbtnOk.Text = "New";
            this.GetCompany();

            this.Get_Receive_Info();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //ReportClass rptstk = null;
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //if (this.ddlSurveyType.SelectedValue.ToString().Trim() == "1")
            //{
            //    DataTable dt = (DataTable)Session["tblMSR"];
            //   RealERPRPT.R_14_Pro.RptPurMktSurvey rptstk1 = new RealERPRPT.R_14_Pro.RptPurMktSurvey() ;
            //    rptstk1.SetDataSource((DataTable)Session["tblMSR"]);
            //    Session["Report1"] = rptstk1;
            //    rptstk = rptstk1;
            //}
            //else if (this.ddlSurveyType.SelectedValue.ToString().Trim() == "2")
            //{
            //     RealERPRPT.R_14_Pro.RptMktSurveyMatWiseSupList rptstk2 = new RealERPRPT.R_14_Pro.RptMktSurveyMatWiseSupList()  ;
            //    rptstk2.SetDataSource((DataTable)Session["tbPreLink"]);
            //    Session["Report1"] = rptstk2;
            //    rptstk = rptstk2;
            //}
            //else if (this.ddlSurveyType.SelectedValue.ToString().Trim() == "3")
            //{
            //    RealERPRPT.R_14_Pro.RptMktSurveySupWiseMatList rptstk3 = new RealERPRPT.R_14_Pro. RptMktSurveySupWiseMatList();
            //    rptstk3.SetDataSource((DataTable)Session["SuplRes"]);
            //    Session["Report1"] = rptstk3;
            //    rptstk = rptstk3;
            //}


            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text =comnam;
            ////TextObject txtCompanyAddress = rptstk.ReportDefinition.ReportObjects["companyaddress"] as TextObject;
            ////txtCompanyAddress.Text = ConstantInfo.ComAdd;
            //TextObject txtsurveynoname = rptstk.ReportDefinition.ReportObjects["surveynoname"] as TextObject;
            //txtsurveynoname.Text =this.lblCurMSRNo1.Text.Trim()+ this.txtCurMSRNo2.Text.ToString().Trim();
            //TextObject txtadate = rptstk.ReportDefinition.ReportObjects["adate"] as TextObject;
            //txtadate.Text = this.txtApprovalDate.Text.ToString().Trim();
            //TextObject txtnarrationname = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //txtnarrationname.Text = this.txtMSRNarr.Text.ToString().Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = this.Label1.Text;
            //    string eventdesc = "Print Report Survey";
            //    string eventdesc2 = this.lblCurMSRNo1.Text.Trim() + this.txtCurMSRNo2.Text.ToString().Trim(); 
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //this.lblprintstk.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        protected void ImgbtnFindSupl1_Click(object sender, ImageClickEventArgs e)
        {
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string mSrchTxt = "%";
        //    DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", mSrchTxt, "", "", "", "", "", "", "", "");
        //    if (ds1 == null)
        //        return;

        //    this.ddlProjectName.DataTextField = "ssirdesc1";
        //    this.ddlProjectName.DataValueField = "ssircode";
        //    this.ddlProjectName.DataSource = ds1.Tables[0];
        //    this.ddlProjectName.DataBind();
        }

        private void Get_Receive_Info()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string UserCode = this.ddlUserList.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_ALLOWANCE", "GETPERSECTION", UserCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tbPreLink"] = ds1.Tables[0];

            this.gvProLinkInfo_DataBind();

        }
        protected void gvProLinkInfo_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["tbPreLink"];
            this.gvProLinkInfo.DataSource = tbl1;
            this.gvProLinkInfo.DataBind();

        }

        private void Session_tbltbPreLink_Update()
        {
            DataTable tbl1 = (DataTable)Session["tbPreLink"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvProLinkInfo.Rows.Count; j++)
            {
                string dgvRemarks = ((TextBox)this.gvProLinkInfo.Rows[j].FindControl("txtgvSuplRemarks")).Text.Trim();

                TblRowIndex2 = (this.gvProLinkInfo.PageIndex) * this.gvProLinkInfo.PageSize + j;
                tbl1.Rows[TblRowIndex2]["remarks"] = dgvRemarks;
            }
            Session["tbPreLink"] = tbl1;
        }

        protected void lbtnSelectSupl1_Click(object sender, EventArgs e)
        {
            this.Session_tbltbPreLink_Update();
            DataTable tbl1 = (DataTable)Session["tbPreLink"];
            string ProCode = this.ddlSection.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("code = '" + ProCode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["code"] = this.ddlSection.SelectedValue.ToString();
                dr1["sectiondesc"] = this.ddlSection.SelectedItem.Text.Trim();
                dr1["remarks"] = "";
                tbl1.Rows.Add(dr1);
            }
            Session["tbPreLink"] = tbl1;
            this.gvProLinkInfo_DataBind();
        }

        protected void lbtnSuplUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.Session_tbltbPreLink_Update();
            string userid = this.ddlUserList.SelectedValue.ToString();
            DataTable tbl1 = (DataTable)Session["tbPreLink"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                string code = tbl1.Rows[i]["code"].ToString();
                string mRMRKS = tbl1.Rows[i]["remarks"].ToString();

                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_ALLOWANCE", "INSERTUPDATESECTIONLINK",
                              userid, code, mRMRKS, "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = HRData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }
           ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update Project user Define";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        //protected void ImgbtnFindUser1_Click(object sender, EventArgs e)
        //{
        //    this.Getuser();
        //}
        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {
           this.GetCompany();
        }

        protected void gvProLinkInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tbPreLink"];
            string UserName = this.ddlUserList.SelectedValue.ToString();
            string Code = ((Label)this.gvProLinkInfo.Rows[e.RowIndex].FindControl("lblgvprocode")).Text.Trim();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_ALLOWANCE", "DELETESECTIONCODE", UserName, Code, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvProLinkInfo.PageSize) * (this.gvProLinkInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tbPreLink");
            Session["tbPreLink"] = dv.ToTable();
            this.gvProLinkInfo_DataBind();

        }
        protected void lbtnSelectAll_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbPreLink"];

            for (int i = 0; i < this.ddlSection.Items.Count; i++)
            {
                string ProCode = this.ddlSection.Items[i].Value;
                DataRow[] dr = dt.Select("code='" + ProCode + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["code"] = this.ddlSection.Items[i].Value;
                    dr1["sectiondesc"] = this.ddlSection.Items[i].Text;
                    dr1["remarks"] = "";
                    dt.Rows.Add(dr1);


                }


            }

            Session["tbPreLink"] = dt;
            this.gvProLinkInfo_DataBind();
        }

        protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.SectionName();

        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptName();
        }
    }
}
