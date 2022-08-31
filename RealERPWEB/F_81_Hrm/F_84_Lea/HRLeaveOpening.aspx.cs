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
    public partial class HRLeaveOpening : System.Web.UI.Page
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

                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.ShowView();
                this.GetCompany();
                this.GetProjectName();
                this.LeaveOpen();
            }

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string txtCompany = "%%";
            string UserID = "%" + this.ddlCompany.Text.Trim() + "%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GET_ACCESSED_COMPANYLIST", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            this.ddlCompany_SelectedIndexChanged(null, null);

        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        private void GetProjectName()
        {

            string comcod = this.GetComeCode();
            string txtSProject = "%%";
            string company = (this.ddlCompany.SelectedValue.Substring(0, 2).ToString() == "00") ? "%" : this.ddlCompany.SelectedValue.Substring(0, 2).ToString() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAMEFL", txtSProject, company, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "deptname";
            this.ddlProjectName.DataValueField = "deptid";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

            this.LeaveOpen();

        }
        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }
        protected void imgbtnProSrch_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ddlCompany.Enabled = false;
                this.ddlProjectName.Enabled = false;
                this.txtdate.ReadOnly = true;

                this.ShowValue();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.ddlCompany.Enabled = true;
                this.ddlProjectName.Enabled = true;
                this.txtdate.ReadOnly = false;
                //this.chkLeave.Visible = false;
                this.gvLeaveRule.DataSource = null;
                this.gvLeaveRule.DataBind();
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
            }
        }

        protected void imgbtnEmpSrch_Click(object sender, EventArgs e)
        {
            this.ShowValue();
        }

        private void LeaveOpen()
        {

            string comcod = this.GetComeCode();
            string company = this.ddlCompany.SelectedValue.ToString();
            // string yearid = this.txtdate.Text;
            //string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "OPENDATETEST", company, "", "", "", "", "", "", "", "");
            DataTable dt = ds4.Tables[0];
            if (dt.Rows.Count > 0)
            {
                this.txtdate.Text = Convert.ToDateTime(dt.Rows[0]["dayid"]).ToString("dd-MMM-yyyy");
                this.txtdate.Enabled = false;
            }
            //else {
            //    this.txtdate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("dd-MMM-yyyy");
            //    this.txtdate.Enabled = true;
            //}            
        }
        private void ShowValue()
        {
            Session.Remove("LeavOpen");
            string comcod = this.GetComeCode();
            string company = (this.ddlCompany.SelectedValue.Substring(0, 2).ToString() == "00") ? "%" : this.ddlCompany.SelectedValue.Substring(0, 2).ToString() + "%";
            string yearid = this.txtdate.Text;
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string empcode = this.txtSrcEmpCode.Text.Trim() + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLEAVE01", yearid, pactcode, company, empcode, "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvLeaveRule.DataSource = null;
                this.gvLeaveRule.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds4.Tables[0]);
            Session["LeavOpen"] = dt;
            this.LoadGrid();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid = dt1.Rows[0]["secid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["secid"].ToString() == secid)
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["secid"] = "";
                    dt1.Rows[j]["secname"] = "";
                }
                else
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                }
            }
            return dt1;
        }

        private void LoadGrid()
        {

            DataTable dt = (DataTable)Session["LeavOpen"];
            this.gvLeaveRule.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvLeaveRule.DataSource = dt;
            this.gvLeaveRule.DataBind();


            Session["Report1"] = gvLeaveRule;
            ((HyperLink)this.gvLeaveRule.HeaderRow.FindControl("hlbtntbCdataExelSP2")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //string type = this.Request.QueryString["Type"].ToString().Trim();
            //switch (type)
            //{
            //    case "LeaveRule":
            //        break;

            //}
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.SaveValue();
            this.LoadGrid();

        }
        protected void gvLeaveRule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //this.SaveValue();
            this.gvLeaveRule.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void lnkbtnFUpLeave_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["LeavOpen"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString(); //comcod
            string yearid = this.txtdate.Text; //year
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString(); //empid
                string ernid = dt.Rows[i]["ernid"].ToString();  //gcod
                string leaopen = dt.Rows[i]["opening"].ToString();
                string entitlement = dt.Rows[i]["ernleave"].ToString(); // entitlement
                string msg = "";

                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTLEAVEOPEN", empid, ernid, yearid, leaopen, entitlement, "", "", "", "", "", "", "", "", "", "");
                if (result == false)
                {

                     msg = "Data Is Not Updated";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

                }
                else
                {
                    msg = "Updated Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

                }
            }
            
        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["LeavOpen"];
            int TblRowIndex;
            for (int i = 0; i < this.gvLeaveRule.Rows.Count; i++)
            {

                string entitlement = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvel")).Text.Trim()).ToString();
                string leaveopen = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvelOpen")).Text.Trim()).ToString();
                //string skleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvsl")).Text.Trim()).ToString();
                //string mtleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvml")).Text.Trim()).ToString();
                //string wpleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvWPl")).Text.Trim()).ToString();
                //string trpleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvTrL")).Text.Trim()).ToString();
                TblRowIndex = (gvLeaveRule.PageIndex) * gvLeaveRule.PageSize + i;

                dt.Rows[TblRowIndex]["opening"] = leaveopen;
                dt.Rows[TblRowIndex]["ernleave"] = entitlement;
                //dt.Rows[TblRowIndex]["csleave"] = csleave;
                //dt.Rows[TblRowIndex]["skleave"] = skleave;
                //dt.Rows[TblRowIndex]["mtleave"] = mtleave;
                //dt.Rows[TblRowIndex]["wpleave"] = wpleave;
                //dt.Rows[TblRowIndex]["trpleave"] = trpleave;

            }
            Session["LeavOpen"] = dt;

        }


    }
}