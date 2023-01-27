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
    public partial class PayrollLink : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.Getuser();
                this.GetCompany();
                GetBranch();
                LoadOrderDapp();
                GetSectionName();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }



        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void Getuser()
        {
            if (this.lbtnOk.Text == "New")
                return;

            string comcod = this.GetCompCode();
            string mSrchTxt = this.ddlUserList.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETUSERNAME", mSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlUserList.DataTextField = "usrsname";
            this.ddlUserList.DataValueField = "usrid";
            this.ddlUserList.DataSource = ds1.Tables[0];
            this.ddlUserList.DataBind();
            ds1.Dispose();
        }

        protected void GetCompany()
        {

            string comcod = this.GetCompCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "PERGETCOMPANYNAME", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];

            ds1.Dispose();
        }

        protected void GetBranch()
        {

            string comcod = this.GetCompCode();

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETBRANCH", Company, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlBranch.DataTextField = "actdesc";
            this.ddlBranch.DataValueField = "actcode";
            this.ddlBranch.DataSource = ds1.Tables[0];
            this.ddlBranch.DataBind();
            ds1.Dispose();
            ddlBranch_SelectedIndexChanged(null,null);

        }
        public void LoadOrderDapp()
        {
            string comcod = this.GetCompCode();

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string branch = (this.ddlBranch.SelectedValue.ToString() == "000000000000") ? Company :( this.ddlBranch.SelectedValue.ToString().Substring(0, 4)) + "%";

            string txtSProject = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GETPROJECTNAMEPERMISSION", branch, txtSProject, "", "", "", "", "", "", "");
            this.ddlDptList.DataTextField = "actdesc";
            this.ddlDptList.DataValueField = "actcode";
            this.ddlDptList.DataSource = ds1.Tables[0];
            this.ddlDptList.DataBind();
            this.ddlDptList_SelectedIndexChanged(null, null);
        }

        private void GetSectionName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string branch = (this.ddlBranch.SelectedValue.ToString() == "000000000000") ? Company : this.ddlBranch.SelectedValue.ToString().Substring(0, 4) + "%";
            string Department = (this.ddlDptList.SelectedValue.ToString() == "000000000000") ? branch : this.ddlDptList.SelectedValue.ToString().Substring(0, 9) + "%";


            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GETSECTIONNAMEPERMISSION", Department, "", "", "", "", "", "", "", "");
            this.ddlSectionList.DataTextField = "actdesc";
            this.ddlSectionList.DataValueField = "actcode";
            this.ddlSectionList.DataSource = ds2.Tables[0];
            this.ddlSectionList.DataBind();

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                
                ViewState.Remove("tblPayPer");


                this.ddlUserList.Enabled = true;
                this.ddlCompany.Enabled = true;
                this.ddlCompany.Items.Clear();
                this.gvPayrollLinkInfo.DataSource = null;
                this.gvPayrollLinkInfo.DataBind();
                this.Panel2.Visible = false;
                this.lbtnOk.Text = "Ok";
                return;
            }

            this.ddlUserList.Enabled = false;
            this.Panel2.Visible = true;
            this.lbtnOk.Text = "New";
            GetCompany();
            this.ShowPayLink();

        }
        private void ShowPayLink()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string UserCode = this.ddlUserList.SelectedValue.ToString();


            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETUSERLINK", UserCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblPayPer"] = ds1.Tables[0];

            this.Data_Bind();
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }


        protected void Data_Bind()
        {
            DataTable tbl1 = (DataTable)ViewState["tblPayPer"];
            this.gvPayrollLinkInfo.DataSource = tbl1;
            this.gvPayrollLinkInfo.DataBind();

        }

        private void Session_tbltbPreLink_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tblPayPer"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvPayrollLinkInfo.Rows.Count; j++)
            {
                string dgvRemarks = ((TextBox)this.gvPayrollLinkInfo.Rows[j].FindControl("txtgvRemarks")).Text.Trim();

                TblRowIndex2 = (this.gvPayrollLinkInfo.PageIndex) * this.gvPayrollLinkInfo.PageSize + j;
                tbl1.Rows[TblRowIndex2]["remarks"] = dgvRemarks;
            }
            ViewState["tblPayPer"] = tbl1;
        }

        protected void lbtnSelectSupl1_Click(object sender, EventArgs e)
        {
            this.Session_tbltbPreLink_Update();
            DataTable tbl1 = (DataTable)ViewState["tblPayPer"];
            string actcode = this.ddlCompany.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("actcode = '" + actcode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["userid"] = this.ddlUserList.SelectedValue.ToString();
                dr1["actcode"] = this.ddlCompany.SelectedValue.ToString();
                dr1["actdesc"] = this.ddlCompany.SelectedItem.Text.Trim();
                dr1["remarks"] = "";
                tbl1.Rows.Add(dr1);
            }
            ViewState["tblPayPer"] = tbl1;
            this.Data_Bind();
        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //   ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}
            string comcod = this.GetCompCode();
            string msg = "";
            this.Session_tbltbPreLink_Update();
            DataTable tbl1 = (DataTable)ViewState["tblPayPer"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string userid = tbl1.Rows[i]["userid"].ToString();
                string pactcode = tbl1.Rows[i]["actcode"].ToString();
                string mRMRKS = tbl1.Rows[i]["remarks"].ToString();

                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "INSERTUPPAYROLLINK", userid, pactcode, mRMRKS, "", "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    msg = "Data Update Failed!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }
            }

            msg = "Data Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update Salary Sheet user Define";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void ImgbtnFindUser1_Click(object sender, EventArgs e)
        {
            this.Getuser();
        }
        protected void ImgbtnFindComp_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }
        protected void gvPayrollLinkInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)ViewState["tblPayPer"];
            string actcode = ((Label)this.gvPayrollLinkInfo.Rows[e.RowIndex].FindControl("lblgvCompCod")).Text.Trim();
            string usrid = ((Label)this.gvPayrollLinkInfo.Rows[e.RowIndex].FindControl("lblgvCompusrid")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "DELETEPAYLINK", actcode, usrid, "", "", "", "", "", "", "", "", "", "", "", "", "");
          
            if (result == true)
            {
                int rowindex = (this.gvPayrollLinkInfo.PageSize) * (this.gvPayrollLinkInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                ViewState["tblPayPer"] = dt;
                ///gvProLinkInfo_DataBind();
            }    
            //string Messagesd = "Updated Successfully";
            //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagesd + "');", true);
            this.Data_Bind();
        }
        protected void lbtnDeleteAll_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = this.ddlUserList.SelectedValue.ToString();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "DELETEPAYLINK", "", usrid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {
                string Messagesd = "Delete Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagesd + "');", true);
            }
            this.ShowPayLink();
        }

        protected void lnkdpt_Click(object sender, EventArgs e)
        {
            LoadOrderDapp();
        }

        protected void lnkSection_Click(object sender, EventArgs e)
        {
            GetSectionName();
        }


        protected void lnkAddList_Click(object sender, EventArgs e)
        {
            string userName = ddlUserList.SelectedItem.Text;
            string usrid = ddlUserList.SelectedItem.Value;
            string comcod = GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);

           
            string brnch = this.ddlBranch.SelectedValue.ToString();
            string dptlist = this.ddlDptList.SelectedValue.ToString();
            string actcode = this.ddlSectionList.SelectedValue.ToString();


            this.Session_tbltbPreLink_Update();
            DataTable tbl1 = (DataTable)ViewState["tblPayPer"];

            string section = "";

            ////Methode 1 Emdad bhai
            //if (actcode != "000000000000" && dptlist != "000000000000" && brnch != "000000000000")
            //{

            //    DataRow[] dr2 = tbl1.Select("actcode = '" + actcode + "'");
            //    if (dr2.Length == 0)
            //    {
            //        DataRow dr1 = tbl1.NewRow();
            //        dr1["userid"] = this.ddlUserList.SelectedValue.ToString();
            //        dr1["actcode"] = this.ddlSectionList.SelectedValue.ToString();
            //        dr1["actdesc"] = this.ddlSectionList.SelectedItem.Text.Trim();
            //        dr1["remarks"] = "";
            //        tbl1.Rows.Add(dr1);
            //    }
            //    ViewState["tblPayPer"] = tbl1;
            //}
            //else if (actcode == "000000000000" && dptlist != "000000000000" && brnch != "000000000000")
            //{

            //    section = this.ddlDptList.SelectedValue.ToString();
            //    DataRow[] dr2 = tbl1.Select("actcode = '" + actcode + "'");
            //    if (dr2.Length == 0)
            //    {
            //        DataRow dr1 = tbl1.NewRow();
            //        dr1["userid"] = this.ddlUserList.SelectedValue.ToString();
            //        dr1["actcode"] = this.ddlDptList.SelectedValue.ToString();
            //        dr1["actdesc"] = this.ddlDptList.SelectedItem.Text.Trim();
            //        dr1["remarks"] = "";
            //        tbl1.Rows.Add(dr1);
            //    }
            //    ViewState["tblPayPer"] = tbl1;
            //}
            //else  if (actcode == "000000000000" && dptlist == "000000000000" && brnch != "000000000000")
            //{

            //    section = this.ddlBranch.SelectedValue.ToString();
            //    DataRow[] dr2 = tbl1.Select("actcode = '" + actcode + "'");
            //    if (dr2.Length == 0)
            //    {
            //        DataRow dr1 = tbl1.NewRow();
            //        dr1["userid"] = this.ddlUserList.SelectedValue.ToString();
            //        dr1["actcode"] = this.ddlBranch.SelectedValue.ToString();
            //        dr1["actdesc"] = this.ddlBranch.SelectedItem.Text.Trim();
            //        dr1["remarks"] = "";
            //        tbl1.Rows.Add(dr1);
            //    }
            //    ViewState["tblPayPer"] = tbl1;
            //}
            //else if (actcode == "000000000000" && dptlist == "000000000000" && brnch == "000000000000")
            //{

            //    section = this.ddlCompany.SelectedValue.ToString();
            //    DataRow[] dr2 = tbl1.Select("actcode = '" + actcode + "'");
            //    if (dr2.Length == 0)
            //    {
            //        DataRow dr1 = tbl1.NewRow();
            //        dr1["userid"] = this.ddlUserList.SelectedValue.ToString();
            //        dr1["actcode"] = this.ddlCompany.SelectedValue.ToString();
            //        dr1["actdesc"] = this.ddlCompany.SelectedItem.Text.Trim();
            //        dr1["remarks"] = "";
            //        tbl1.Rows.Add(dr1);
            //    }
            //    ViewState["tblPayPer"] = tbl1;
            //}

            // Method Thinking all section added
            //Nahid
            if (actcode != "000000000000" && dptlist == "000000000000" && brnch == "000000000000") // for single section add
            {

                
                DataRow[] dr2 = tbl1.Select("actcode = '" + actcode + "'");
                if (dr2.Length == 0)
                {
                    DataRow dr1 = tbl1.NewRow();
                    dr1["userid"] = this.ddlUserList.SelectedValue.ToString();
                    dr1["actcode"] = this.ddlSectionList.SelectedValue.ToString();
                    dr1["actdesc"] = this.ddlSectionList.SelectedItem.Text.Trim();
                    dr1["remarks"] = "";
                    tbl1.Rows.Add(dr1);
                }
                ViewState["tblPayPer"] = tbl1;
            }
           else if (actcode != "000000000000" && dptlist == "000000000000" && brnch != "000000000000") // for single section add
            {


                DataRow[] dr2 = tbl1.Select("actcode = '" + actcode + "'");
                if (dr2.Length == 0)
                {
                    DataRow dr1 = tbl1.NewRow();
                    dr1["userid"] = this.ddlUserList.SelectedValue.ToString();
                    dr1["actcode"] = this.ddlSectionList.SelectedValue.ToString();
                    dr1["actdesc"] = this.ddlSectionList.SelectedItem.Text.Trim();
                    dr1["remarks"] = "";
                    tbl1.Rows.Add(dr1);
                }
                ViewState["tblPayPer"] = tbl1;
            }
            else if (actcode != "000000000000" && dptlist != "000000000000" && brnch == "000000000000") // for single section add
            {


                DataRow[] dr2 = tbl1.Select("actcode = '" + actcode + "'");
                if (dr2.Length == 0)
                {
                    DataRow dr1 = tbl1.NewRow();
                    dr1["userid"] = this.ddlUserList.SelectedValue.ToString();
                    dr1["actcode"] = this.ddlSectionList.SelectedValue.ToString();
                    dr1["actdesc"] = this.ddlSectionList.SelectedItem.Text.Trim();
                    dr1["remarks"] = "";
                    tbl1.Rows.Add(dr1);
                }
                ViewState["tblPayPer"] = tbl1;
            }
            else if (actcode != "000000000000" && dptlist != "000000000000" && brnch != "000000000000") // for single section add
            {


                DataRow[] dr2 = tbl1.Select("actcode = '" + actcode + "'");
                if (dr2.Length == 0)
                {
                    DataRow dr1 = tbl1.NewRow();
                    dr1["userid"] = this.ddlUserList.SelectedValue.ToString();
                    dr1["actcode"] = this.ddlSectionList.SelectedValue.ToString();
                    dr1["actdesc"] = this.ddlSectionList.SelectedItem.Text.Trim();
                    dr1["remarks"] = "";
                    tbl1.Rows.Add(dr1);
                }
                ViewState["tblPayPer"] = tbl1;
            }
            else if (actcode == "000000000000" && dptlist != "000000000000" && brnch != "000000000000") // for single branch all dpt
            {

                for (int i = 0; i < this.ddlSectionList.Items.Count; i++)
                {
                    section = this.ddlSectionList.Items[i].Value;
                    if (section != "000000000000")
                    {
                        string sectionDEsc = this.ddlSectionList.Items[i].Text;
                        DataRow[] dr2 = tbl1.Select("actcode = '" + section + "'");
                        if (dr2.Length == 0)
                        {
                            DataRow dr1 = tbl1.NewRow();
                            dr1["userid"] = this.ddlUserList.SelectedValue.ToString();
                            dr1["actcode"] = section;
                            dr1["actdesc"] = sectionDEsc;
                            dr1["remarks"] = "";
                            tbl1.Rows.Add(dr1);
                        }
                        ViewState["tblPayPer"] = tbl1;
                    }


                }

            }

            else if (actcode == "000000000000" && dptlist == "000000000000" && brnch == "000000000000") // for all branch 
            {
                for (int i = 0; i < this.ddlSectionList.Items.Count; i++)
                {
                    section = this.ddlSectionList.Items[i].Value;
                    if (section != "000000000000")
                    {
                        string sectionDEsc = this.ddlSectionList.Items[i].Text;
                        DataRow[] dr2 = tbl1.Select("actcode = '" + section + "'");
                        if (dr2.Length == 0)
                        {
                            DataRow dr1 = tbl1.NewRow();
                            dr1["userid"] = this.ddlUserList.SelectedValue.ToString();
                            dr1["actcode"] = section;
                            dr1["actdesc"] = sectionDEsc;
                            dr1["remarks"] = "";
                            tbl1.Rows.Add(dr1);
                        }
                        ViewState["tblPayPer"] = tbl1;
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.ddlSectionList.Items.Count; i++)
                {
                    section = this.ddlSectionList.Items[i].Value;
                    if (section != "000000000000")
                    {
                        string sectionDEsc = this.ddlSectionList.Items[i].Text;
                        DataRow[] dr2 = tbl1.Select("actcode = '" + section + "'");
                        if (dr2.Length == 0)
                        {
                            DataRow dr1 = tbl1.NewRow();
                            dr1["userid"] = this.ddlUserList.SelectedValue.ToString();
                            dr1["actcode"] = section;
                            dr1["actdesc"] = sectionDEsc;
                            dr1["remarks"] = "";
                            tbl1.Rows.Add(dr1);
                        }
                        ViewState["tblPayPer"] = tbl1;
                    }
                }
            }
            this.Data_Bind();
        }

        protected void ddlDptList_SelectedIndexChanged(object sender, EventArgs e)
        {

            GetSectionName();
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOrderDapp();
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetBranch();
        }

        protected void lnkBranch_Click(object sender, EventArgs e)
        {
            GetBranch();
        }
    }
}