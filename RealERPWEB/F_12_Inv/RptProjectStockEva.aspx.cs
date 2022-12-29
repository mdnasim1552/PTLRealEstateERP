﻿using System;
using System.Linq;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_12_Inv
{
    public partial class RptProjectStockEva : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

                // Session.Remove("Unit");
                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "acc" ? "MATERIALS STOCK REPORT " : "MATERIALS STOCK REPORT INVENTORY");

                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.GetMaterial();
                if (Request.QueryString.AllKeys.Contains("prjcode") && this.Request.QueryString["prjcode"].Length > 0)
                {
                    this.lbtnOk_Click(null, null);

                }
            }

        }

        private string Complength()
        {
            string comcod = this.GetCompCode();
            string Complength = "";
            switch (comcod)
            {
                // case "3101":
                case "3348":
                    Complength = "Length";
                    break;

                default:
                    Complength = "";

                    break;
            }

            return Complength;


        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbMatStc"];
            this.gvStocjEvaluation.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvStocjEvaluation.DataSource = dt;
            this.gvStocjEvaluation.DataBind();
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.MatStockEva();
        }

        private void MatStockEva()
        {
            Session.Remove("UserLog");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;
            string pactcode = this.ddlProName.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlProName.SelectedValue.ToString() + "%";

            string resListMulti = "";
            string resourcelist = this.chkResourcelist.SelectedValue.ToString();

            foreach (ListItem item in chkResourcelist.Items)
            {
                if (item.Selected)
                {
                    resListMulti += item.Value;
                }
            }

            string group = this.group.SelectedValue.ToString();


            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_MAT_STOCK", "GETSTOCKVALUATIONMATWISE", fdate, tdate, pactcode, resListMulti, group, "", "", "", "", "", "");

            Session["tbMatStc"] = HiddenSameData(ds1.Tables[0]);
            //Session["tbMatStc"] = ds1.Tables[0];
            DataTable dt = ds1.Tables[0];

            this.gvStocjEvaluation.DataSource = dt;
            this.gvStocjEvaluation.DataBind();
            //this.FooterCalculation();
        }


        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string isircod = dt1.Rows[0]["mrsircode"].ToString();
            for (int i = 1; i < dt1.Rows.Count; i++)
            {
                if (dt1.Rows[i]["mrsircode"].ToString() == isircod)
                {
                    dt1.Rows[i]["msirdesc"] = "";
                }
                isircod = dt1.Rows[i]["mrsircode"].ToString();
            }

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == isircod)
                {
                    dt1.Rows[0]["pactdesc"] = "";
                    dt1.Rows[j]["pactdesc"] = "";
                }

                isircod = dt1.Rows[j]["pactcode"].ToString();
            }
            return dt1;
        }


        private void GetMaterial()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProName.SelectedValue.ToString() == "000000000000" ? "%%" : "%" + this.ddlProName.SelectedValue.ToString() + "%";
            string txtfindMat = this.txtsrchresource.Text.Trim() + "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETMATERIALEVA", pactcode, txtfindMat, "", "", "", "", "", "", "");
            this.chkResourcelist.DataTextField = "rsirdesc";
            this.chkResourcelist.DataValueField = "rsircode";
            this.chkResourcelist.DataSource = ds1.Tables[0];
            this.chkResourcelist.DataBind();
            if (Request.QueryString.AllKeys.Contains("prjcode"))
            {
                this.chkResourcelist.Text = this.Request.QueryString["prjcode"].Length > 0 ? "000000000000" : "";
            }

            ds1.Dispose();
        }


        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            string length = this.Complength();
            string userid = hst["usrid"].ToString();
            string ctype = this.CompCallType();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", ctype, serch1, length, userid, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProName.DataTextField = "pactdesc";
            this.ddlProName.DataValueField = "pactcode";
            this.ddlProName.DataSource = ds1.Tables[0];
            this.ddlProName.DataBind();
            if (Request.QueryString.AllKeys.Contains("prjcode"))
            {
                this.ddlProName.SelectedValue = this.Request.QueryString["prjcode"].Length > 0 ? this.Request.QueryString["prjcode"] : "";
            }

        }

        private string CompCallType()
        {
            string comcod = this.GetCompCode();
            string ctype = "";
            switch (comcod)
            {
                case "3101":
                case "2325":
                case "3325":
                    ctype = "GETPURPROJECTNAMELEISURE";
                    break;
                default:
                    ctype = "GETPURPROJECTNAME";
                    break;
            }
            return ctype;
        }

        protected void lbtnresource_Click(object sender, EventArgs e)
        {
            this.GetMaterial();
        }



        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tbMatStc"];

            if (dt.Rows.Count > 0)
            {
                //((Label)this.gvStocjEvaluation.FooterRow.FindControl("lgvttaccrecvbale")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(actstock)", "")) ? 0.00 : dt.Compute("Sum(actstock)", ""))).ToString("#,##0.00;(#,##0.00); ");
                //((Label)this.gvStocjEvaluation.FooterRow.FindControl("lgvttlsolamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(percnt)", "")) ? 0.00 : dt.Compute("Sum(percnt)", ""))).ToString("#,##0.00;(#,##0.00); ");


                //Session["Report1"] = gvMatStock;
                //((HyperLink)this.gvMatStock.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


            }

            else
            {
                return;
            }
        }

        protected void gvStocjEvaluation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvStocjEvaluation.PageIndex = e.NewPageIndex;
            this.ddlpagesize_SelectedIndexChanged(null, null);
        }

        protected void gvStocjEvaluation_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            //GridViewRow gvRow = e.Row;
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            Label RecDesc = (Label)e.Row.FindControl("lblActualStock");
            string msirdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rsirdesc")).ToString();

            if (msirdesc == "")
            {
                return;
            }
            else
            {
                RecDesc.Font.Bold = true;
            }
        }
    }
}