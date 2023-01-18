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
using System.IO;
using Microsoft.Office.Interop.Excel;
using RealERPLIB;
using RealERPRPT;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTable = System.Data.DataTable;
using Label = System.Web.UI.WebControls.Label;
using TextBox = System.Web.UI.WebControls.TextBox;
using CheckBox = System.Web.UI.WebControls.CheckBox;
using AjaxControlToolkit;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

//using Newtonsoft.Json;
using ListBox = System.Web.UI.WebControls.ListBox;
namespace RealERPWEB.F_81_Hrm.F_94_Task
{
    public partial class TaskCodeBook : System.Web.UI.Page
    {
        ProcessAccess da = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

                getDeptCode();
            }
        }
        private void getDeptCode()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = da.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMP_INTERFACE", "GETDEPTDDL", "", "", "", "", "", "", "", "", "", "");
            ddldeptcode.DataTextField = "deptdesc";
            ddldeptcode.DataValueField = "deptid";
            ddlFdeptcode.DataTextField = "deptdesc";
            ddlFdeptcode.DataValueField = "deptid";
            ddlTdeptcode.DataTextField = "deptdesc";
            ddlTdeptcode.DataValueField = "deptid";
            ddldeptcode.DataSource = ds1.Tables[0];
            ddldeptcode.DataBind();
            ddlFdeptcode.DataSource = ds1.Tables[0];
            ddlFdeptcode.DataBind();
            ddlTdeptcode.DataSource = ds1.Tables[0];
            ddlTdeptcode.DataBind();

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }



        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        private void SelectView()
        {


        }
        protected void grvacc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();

        }
        protected void grvacc_RowEditing(object sender, GridViewEditEventArgs e)
        {

            this.grvacc.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();
            //string comcod = this.GetCompCode();
            //int rowindex = (this.grvacc.PageSize) * (this.grvacc.PageIndex) + e.NewEditIndex;
            // string code = ((DataTable)Session["storedata"]).Rows[rowindex]["ddlRank"].ToString();

        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            string comcod = this.GetCompCode();
            string gcode2 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
            if (gcode2.Length != 5)
                return;
            string tgcod = gcode2;
            string gdesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string active = (((CheckBox)grvacc.Rows[e.RowIndex].FindControl("chkActive")).Checked) ? "1" : "0";
            string deptcode = this.ddldeptcode.SelectedValue.ToString();
            string msg = "";

            bool result = da.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMP_INTERFACE", "INSERTUPDATETKCODEBOOK", tgcod,
                           deptcode, gdesc, active, "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {


                msg = "Updated Successfully ";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

            }
            else
            {
                msg = "Update Failed ";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
            this.grvacc.EditIndex = -1;
            this.ShowInformation();
            this.grvacc_DataBind();
        }

        protected void grvacc_DataBind()
        {
            try
            {

                DataTable tbl1 = (DataTable)Session["storedata"];
                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();
                ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = false;
                double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.grvacc.PageSize);
                ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Items.Clear();
                for (int i = 1; i <= TotalPage; i++)
                    ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
                if (TotalPage > 1)
                    ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = true;
                ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.grvacc.PageIndex;



            }
            catch (Exception ex)
            {
            }

        }
        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                this.grvacc.PageIndex = ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
                this.grvacc_DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        public void ShowInformation()
        {

            string comcod = this.GetCompCode();
            string deptcode = (this.ddldeptcode.SelectedValue.ToString() == "000000000000" ? "%" : this.ddldeptcode.SelectedValue.ToString());
            DataSet ds1 = da.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMP_INTERFACE", "GETTASKCODEBOOKDATA", deptcode, "", "", "", "", "", "", "", "", "");
            Session["storedata"] = ds1.Tables[0];
            DataTable dt = (DataTable)Session["storedata"];
            if (ds1.Tables[0].Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["comcod"] = comcod;
                dr["taskcode"] = "01000";
                dr["taskcode1"] = "01-000";
                dr["taskdesc"] = "Add Text";
                dr["Active"] = 1;
                dt.Rows.Add(dr);

            }
            grvacc_DataBind();

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            ShowInformation();
        }
        protected void lnkCopy_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string deptcode = (this.ddlFdeptcode.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlFdeptcode.SelectedValue.ToString());
            string Tdeptcode = this.ddlTdeptcode.SelectedValue.ToString();
            string msg = "";
            DataSet ds1 = da.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMP_INTERFACE", "GETTASKCODEBOOKDATA", deptcode, "", "", "", "", "", "", "", "", "");
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                string gcode2 = ds1.Tables[0].Rows[i]["taskcode"].ToString();
                string tgcod = gcode2;
                string gdesc = ds1.Tables[0].Rows[i]["taskdesc"].ToString();
                string active = ds1.Tables[0].Rows[i]["active"].ToString();


                bool result = da.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMP_INTERFACE", "INSERTUPDATETKCODEBOOK", tgcod,
                           Tdeptcode, gdesc, active, "", "", "", "", "", "", "", "", "", "", "");

                if (result == true)
                {
                    //((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Updated ";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    msg = "Updated Successfully ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                }
                else
                {
                    //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    msg = "Updated Successfully ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                }
            }

        }
        protected void chkcopy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkcopy.Checked == true)
            {
                this.pnl.Visible = true;
            }
            else
            {
                this.pnl.Visible = false;
            }
        }
    }
}


