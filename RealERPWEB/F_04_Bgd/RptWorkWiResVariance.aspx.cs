﻿using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_04_Bgd
{
    public partial class RptWorkWiResVariance : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Permission Part
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() == "Edit")
                {
                    //((Label)this.Master.FindControl("lblTitle")).Text = "Work Execution With Material Issue - EDIT MODE";
                }
                GetProjectName();
                ShowFloorcode();
            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
        }
        private void GetProjectName()
        {
            string comcod = this.GetComeCode();
            string txtSProject = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");

            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataTextField = "actdesc";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataBind();
        }

        private void ShowFloorcode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "GETFLOORCOD", pactcode, "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];

            DataRow dr3 = dt.NewRow();
            dr3["flrcod"] = "AAA";
            dr3["flrdes"] = "All Floors-Details";
            dt.Rows.Add(dr3);
            DataView dv = dt.DefaultView;
            dv.Sort = "flrcod";
            dt = dv.ToTable();
            this.ddlFloor.DataTextField = "flrdes";
            this.ddlFloor.DataValueField = "flrcod";
            this.ddlFloor.DataSource = dt;
            this.ddlFloor.DataBind();
            this.ddlFloor.SelectedValue = "AAA";
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            ShowWorkVsResource();
        }
        private void ShowWorkVsResource()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string Floorcode = this.ddlFloor.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "RPTWORKVSRESOURCEWITHVARIANCE", pactcode, Floorcode, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvWrkVsRes.DataSource = null;
                this.gvWrkVsRes.DataBind();
                return;
            }

            Session["tblbgd"] = HiddenSameData(ds2.Tables[0]);// HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }
        private void Data_Bind()
        {
            try
            {
                this.gvWrkVsRes.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvWrkVsRes.DataSource = (DataTable)Session["tblbgd"];
                this.gvWrkVsRes.DataBind();
                this.FooterCalculation((DataTable)Session["tblbgd"]);
            }
            catch (Exception ex)
            {

            }
        }
        private void FooterCalculation(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvWrkVsRes.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(resamt)", "")) ?
                                       0 : dt.Compute("sum(resamt)", ""))).ToString("#,##0;(#,##0); ");

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string flrcod = dt1.Rows[0]["flrcod"].ToString();
            string isircode = dt1.Rows[0]["isircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["flrcod"].ToString() == flrcod && dt1.Rows[j]["isircode"].ToString() == isircode)
                {
                    flrcod = dt1.Rows[j]["flrcod"].ToString();
                    isircode = dt1.Rows[j]["isircode"].ToString();
                    dt1.Rows[j]["flrdes"] = "";
                    dt1.Rows[j]["isirdesc"] = "";
                    dt1.Rows[j]["isirunit"] = "";
                    dt1.Rows[j]["itemqty"] = 0.00;
                }

                else
                {
                    if (dt1.Rows[j]["flrcod"].ToString() == flrcod)
                    {
                        dt1.Rows[j]["flrdes"] = "";
                    }
                    if (dt1.Rows[j]["isircode"].ToString() == isircode)
                    {
                        dt1.Rows[j]["isirdesc"] = "";
                        dt1.Rows[j]["isirunit"] = "";
                        dt1.Rows[j]["itemqty"] = 0.00;
                    }

                    flrcod = dt1.Rows[j]["flrcod"].ToString();
                    isircode = dt1.Rows[j]["isircode"].ToString();
                }

            }
            return dt1;
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvWrkVsRes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvWrkVsRes.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvWrkVsRes_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = new GridViewRow(6, 6, DataControlRowType.Header, DataControlRowState.Normal);
            TableHeaderCell cell = new TableHeaderCell();
            cell.Text = "Customers";
            cell.ColumnSpan = 2;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 2;
            cell.Text = "Employees";
            row.Controls.Add(cell);

            row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            gvWrkVsRes.HeaderRow.Parent.Controls.AddAt(0, row);
        }
    }
}