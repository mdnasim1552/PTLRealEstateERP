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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_04_Bgd
{
    public partial class RptFrameAndFinishingCost : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);



                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                //((Label)this.Master.FindControl("lblTitle")).Text = "Frame and Finishing Cost";


                this.GetProjectName();
            }

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetProjectName()
        {
            string comcod = this.GetComeCode();
            //string txtSProject = this.txtSrcProject.Text.Trim();

            string txtSProject = this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }

        protected void lbtOk_Click(object sender, EventArgs e)
        {

            this.ShowData();

        }

        private void ShowData()
        {
            Session.Remove("tblbgd");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSISDIFWRK", "RPTFINISHINGWRK", "", "", pactcode, "000", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvbgdgrwisedet.DataSource = null;
                this.gvbgdgrwisedet.DataBind();
                return;
            }

            string Comcode = ASTUtility.Left((this.GetComeCode()), 1);
            string txtconarea = (Comcode == "2") ? "Development Area: " : "Construction Area: ";
            Session["tblbgd"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string acgcode = dt1.Rows[0]["acgcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["acgcode"].ToString() == acgcode)
                {
                    dt1.Rows[j]["acgdesc"] = "";
                }
                acgcode = dt1.Rows[j]["acgcode"].ToString();
            }


            return dt1;


        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblbgd"];
            this.gvbgdgrwisedet.DataSource = dt;
            this.gvbgdgrwisedet.DataBind();
        }

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void lnkgvActDescgrwisedet_Click(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string acgcode = ((DataTable)Session["tblbgd"]).Rows[index]["acgcode"].ToString();
            string colst = ((DataTable)Session["tblbgd"]).Rows[index]["colst"].ToString();
            DataTable dt = ((DataTable)Session["tblbgd"]);
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.RowFilter = ("rescode= '000000000000' or rescode= 'AAAAAAAAAAAA'");
            dt = dv.ToTable();

            DataRow[] dr1 = dt.Select("acgcode='" + acgcode + "'");
            dr1[0]["colst"] = (colst == "0") ? "1" : "0";

            // For Status 0
            foreach (DataRow dr2 in dt.Rows)
            {
                if (dr2["acgcode"] != acgcode)
                {
                    dr2["colst"] = "0";

                }
            }

            colst = (dt.Select("acgcode='" + acgcode + "'"))[0]["colst"].ToString();
            if (colst == "1")
            {
                DataTable dtb = ((DataTable)Session["tblbbgd"]).Copy();
                dv = dtb.DefaultView;
                dv.RowFilter = ("acgcode='" + acgcode + "' and  rescode not like '%00000'");
                dtb = dv.ToTable();
                dt.Merge(dtb);

            }


            dv = dt.DefaultView;
            dv.Sort = ("acgcode, rescode");
            Session["tblbgd"] = dv.ToTable();
            this.Data_Bind();

        }
        protected void gvbgdgrwisedet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label acresdesc = (Label)e.Row.FindControl("gvActDescgrwisedet");
                Label lblConsCost = (Label)e.Row.FindControl("lgvconcostgrwisedet");
                Label lblSalCost = (Label)e.Row.FindControl("lgvsalcostgrwisedet");
                Label lblbudgetamt = (Label)e.Row.FindControl("lblbudgetamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }







                if (code == "000000000000")
                {

                    acresdesc.Font.Bold = true;
                    lblbudgetamt.Font.Bold = true;
                    lblConsCost.Font.Bold = true;
                    lblSalCost.Font.Bold = true;
                    e.Row.BackColor = System.Drawing.Color.Orange;
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                    acresdesc.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lblbudgetamt.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lblConsCost.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lblSalCost.Attributes["style"] = "font-weight:bold; color:maroon;";
                    acresdesc.Style.Add("text-align", "left");
                }


                else if (code == "BBBBBBBBBBBB")
                {
                    acresdesc.Font.Size = 12;
                    lblbudgetamt.Font.Size = 12;
                    lblConsCost.Font.Size = 12;
                    lblSalCost.Font.Size = 12;
                    e.Row.BackColor = System.Drawing.Color.Red;
                    acresdesc.Attributes["style"] = "font-weight:bold; color:white;";
                    lblbudgetamt.Attributes["style"] = "font-weight:bold; color:white;";
                    lblConsCost.Attributes["style"] = "font-weight:bold; color:white;";
                    lblSalCost.Attributes["style"] = "font-weight:bold; color:white;";
                    acresdesc.Style.Add("text-align", "right");

                }


                else if (code == "CCCCCCCCCCCC")
                {
                    acresdesc.Font.Size = 12;
                    lblbudgetamt.Font.Size = 12;
                    lblConsCost.Font.Size = 12;
                    lblSalCost.Font.Size = 12;
                    e.Row.BackColor = System.Drawing.Color.Red;
                    acresdesc.Attributes["style"] = "font-weight:bold; color:white;";
                    lblbudgetamt.Attributes["style"] = "font-weight:bold; color:white;";
                    lblConsCost.Attributes["style"] = "font-weight:bold; color:white;";
                    lblSalCost.Attributes["style"] = "font-weight:bold; color:white;";
                    acresdesc.Style.Add("text-align", "right");

                }

                else if (code == "DDDDDDDDDDDD")
                {
                    acresdesc.Font.Size = 12;
                    lblbudgetamt.Font.Size = 12;
                    lblConsCost.Font.Size = 12;
                    lblSalCost.Font.Size = 12;
                    e.Row.BackColor = System.Drawing.Color.HotPink;
                    acresdesc.Attributes["style"] = "font-weight:bold; color:black;";
                    lblbudgetamt.Attributes["style"] = "font-weight:bold; color:black;";
                    lblConsCost.Attributes["style"] = "font-weight:bold; color:black;";
                    lblSalCost.Attributes["style"] = "font-weight:bold; color:black;";
                    acresdesc.Style.Add("text-align", "right");

                }









            }
        }

    }
}