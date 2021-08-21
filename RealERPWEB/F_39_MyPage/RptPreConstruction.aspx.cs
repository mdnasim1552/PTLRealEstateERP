﻿using System;
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
namespace RealERPWEB.F_39_MyPage
{
    public partial class RptPreConstruction : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Pre-Constructin Planning";
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetProjectName()
        {

            string comcod = this.GetComCode();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "GETEXPRJNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "actdesc";
            this.ddlPrjName.DataValueField = "actcode";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComCode();
            string ProjectCode = this.ddlPrjName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "RPTPROPRECONSTRUCTION", ProjectCode, "", "", "", "", "", "", "", "");

            ViewState["tblprocom"] = this.HiddenSameData(ds1.Tables[0]);

            if (ds1.Tables[1].Rows.Count > 0)
            {
                this.txtdate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["laprdate"]).ToString("dd-MMM-yyyy");
                this.txtdate.Enabled = false;

            }
            this.Data_Bind();

        }




        private void Data_Bind()
        {


            this.gvPrjInfo.DataSource = (DataTable)ViewState["tblprocom"];
            this.gvPrjInfo.DataBind();
            this.FooterCalculation();


        }
        private void FooterCalculation()
        {


            DataTable dt = (DataTable)ViewState["tblprocom"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvPrjInfo.FooterRow.FindControl("lblgvFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(duration)", "")) ?
                                0 : dt.Compute("sum(duration)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPrjInfo.FooterRow.FindControl("lblgvFacTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(aduration)", "")) ?
                                0 : dt.Compute("sum(aduration)", ""))).ToString("#,##0;(#,##0); ");




        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string deptcod = dt1.Rows[0]["deptcod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["deptcod"].ToString() == deptcod)
                    dt1.Rows[j]["deptdesc"] = "";
                deptcod = dt1.Rows[j]["deptcod"].ToString();
            }
            return dt1;

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)ViewState["tblprocom"];

            ReportDocument rptResource = new RealERPRPT.R_39_MyPage.rptProFlowChart01();
            TextObject rpttxtComName = rptResource.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            rpttxtComName.Text = comnam;

            TextObject rpttxtProName = rptResource.ReportDefinition.ReportObjects["txtprojectname"] as TextObject;
            rpttxtProName.Text = this.ddlPrjName.SelectedItem.ToString().Substring(13);

            TextObject txtuserinfo = rptResource.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptResource.SetDataSource(dt);
            Session["Report1"] = rptResource;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }






        protected void lbtnCalculaton_Click(object sender, EventArgs e)
        {
            DateTime inidate, preenddate;
            DataTable dt = (DataTable)ViewState["tblprocom"];

            for (int i = 0; i < this.gvPrjInfo.Rows.Count; i++)
            {

                int duration = Convert.ToInt32("0" + ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvduration")).Text.Trim());

                if (i == 0)
                {
                    inidate = Convert.ToDateTime(this.txtdate.Text);

                    if (duration > 0)
                    {

                        dt.Rows[i]["duration"] = duration;
                        dt.Rows[i]["acstdat"] = inidate;
                        dt.Rows[i]["acenddat"] = inidate.AddDays(duration);
                    }
                }


                else
                {
                    //Previous Enddate
                    preenddate = Convert.ToDateTime(dt.Rows[i - 1]["acenddat"].ToString());


                    if (duration > 0)
                    {

                        dt.Rows[i]["duration"] = duration;
                        dt.Rows[i]["acstdat"] = preenddate.AddDays(1);
                        dt.Rows[i]["acenddat"] = preenddate.AddDays(duration);
                    }
                }





            }

            ViewState["tblprocom"] = dt;
            this.Data_Bind();

        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlPrjName.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblprocom"];
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            bool result = false;
            result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "INSORUPEMPSTDKPI02B", pactcode, date, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                return;
            }
            foreach (DataRow dr2 in dt.Rows)
            {
                string Actcode = dr2["actcode"].ToString();
                string dur = Convert.ToDouble(dr2["duration"].ToString()).ToString();
                string strtdat = dr2["acstdat"].ToString();
                string enddate = dr2["acenddat"].ToString();
                string empid = dr2["empid"].ToString();
                //Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "INSORUPEMPSTDKPI02", pactcode, Actcode, dur, strtdat, enddate, empid, "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                    return;
                }
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Project Information";
                string eventdesc = "Update Project Information";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void gvPrjInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            //string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblprocom"];
            //string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            //string MatCode = ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblitemcode")).Text.Trim();
            //string spcfcode = ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblgvspcfcode")).Text.Trim();
            //bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELETEMATISUE", mISUNO, MatCode, spcfcode, "", "", "", "", "", "", "", "", "", "", "", "");


            int rowindex = (this.gvPrjInfo.PageSize) * (this.gvPrjInfo.PageIndex) + e.RowIndex;
            dt.Rows[rowindex].Delete();

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblprocom");
            ViewState["tblprocom"] = dv.ToTable();
            this.Data_Bind();


        }
        protected void gvPrjInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvPrjInfo.EditIndex = -1;
            this.Data_Bind();
        }
        protected void gvPrjInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {


            this.gvPrjInfo.EditIndex = e.NewEditIndex;
            this.Data_Bind();
            string comcod = this.GetComCode();
            int rowindex = (this.gvPrjInfo.PageSize) * (this.gvPrjInfo.PageIndex) + e.NewEditIndex;
            string empcode = ((DataTable)ViewState["tblprocom"]).Rows[rowindex]["empid"].ToString();
            DropDownList ddl2 = (DropDownList)this.gvPrjInfo.Rows[e.NewEditIndex].FindControl("ddlUserName");

            ViewState["gindex"] = e.NewEditIndex;
            string SearchProject = "%" + ((TextBox)gvPrjInfo.Rows[e.NewEditIndex].FindControl("txtSearchUserName")).Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "empname";
            ddl2.DataValueField = "empid";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
            ddl2.SelectedValue = empcode; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();

        }

        protected void gvPrjInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int Index = gvPrjInfo.PageSize * gvPrjInfo.PageIndex + e.RowIndex;
            DataTable dt = (DataTable)ViewState["tblprocom"];
            dt.Rows[Index]["empid"] = ((DropDownList)this.gvPrjInfo.Rows[e.RowIndex].FindControl("ddlUserName")).SelectedValue.ToString();
            dt.Rows[Index]["empname"] = ((DropDownList)this.gvPrjInfo.Rows[e.RowIndex].FindControl("ddlUserName")).SelectedItem.Text;

            this.gvPrjInfo.EditIndex = -1;
            this.Data_Bind();

        }

        protected void ibtnSrchProject_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            int rowindex = (int)ViewState["gindex"];
            //string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            DropDownList ddl2 = (DropDownList)this.gvPrjInfo.Rows[rowindex].FindControl("ddlUserName");
            string SearchProject = "%" + ((TextBox)gvPrjInfo.Rows[rowindex].FindControl("txtSearchUserName")).Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "empname";
            ddl2.DataValueField = "empid";
            ddl2.DataSource = ds1;
            ddl2.DataBind();
        }
        protected void gvPrjInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkgvcomments = (HyperLink)e.Row.FindControl("hlnkgvcomments");
                Label deloadv = (Label)e.Row.FindControl("lblgvdelay");
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();
                string pactcode = this.ddlPrjName.SelectedValue.ToString();
                string pactdesc = this.ddlPrjName.SelectedItem.Text.Substring(13);
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string actdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actdesc")).ToString();
                string deloadvsign = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deloadvsign")).ToString();
                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "acenddat")).ToString("dd-MMM-yyyy");
                string sdate = Convert.ToDateTime(System.DateTime.Today).ToString("dd-MMM-yyyy");
                date = (date == "01-Jan-1900") ? sdate : date;
                if (deloadvsign == "delay")
                {
                    deloadv.Style.Add("color", "red");


                }

                hlnkgvcomments.NavigateUrl = "~/F_05_MyPage/LinkActiComments.aspx?empid=" + empid + "&pactcode=" + pactcode + "&pactdesc=" + pactdesc + "&actcode=" + actcode + "&actdesc=" + actdesc + "&date=" + date;


            }


        }
    }
}



