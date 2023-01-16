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
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_22_Sal
{
    public partial class RptRateChart : System.Web.UI.Page
    {

        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                Session.Remove("Unit");
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "SALES INVENTORY (DETAILS)";

            }
            if (this.ddlProjectName.Items.Count == 0)
            {
                this.GetProjectName();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
        }
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME01", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.LoadGrid();
        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.LoadGrid();
        }

        private void LoadGrid()
        {
            Session.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string PactCode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "18" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTUNSOLDUNIT", PactCode, date, mRptGroup, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvStock.DataSource = null;
                this.gvStock.DataBind();
                return;
            }

            this.gvStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvStock.Columns[1].Visible = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? true : false;
            Session["tblData"] = HiddenSameData(ds1.Tables[0]);
            this.gvStock.DataSource = (DataTable)Session["tblData"];
            this.gvStock.DataBind();
            //this.FooterCalculation();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }

            }

            return dt1;
        }

        private void FooterCalculation()
        {


            DataTable dt = (DataTable)Session["tblData"];

            ((Label)this.gvStock.FooterRow.FindControl("lgvFUAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(uamt)", "")) ?
                            0 : dt.Compute("sum(uamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvStock.FooterRow.FindControl("lgvFPamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pamt)", "")) ?
                            0 : dt.Compute("sum(pamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvStock.FooterRow.FindControl("lgvFUtAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(utility)", "")) ?
                            0 : dt.Compute("sum(utility)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvStock.FooterRow.FindControl("lgvFCoAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cooperative)", "")) ?
                            0 : dt.Compute("sum(cooperative)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvStock.FooterRow.FindControl("lgvFUsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(usuamt)", "")) ?
                            0 : dt.Compute("sum(usuamt)", ""))).ToString("#,##0;(#,##0); ");

        }

        protected void gvSpayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvStock.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        //protected void lbtnPrint_Click(object sender, EventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = GetCompCode();
        //    string comnam = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    //string PactCode = this.ddlProjectName.SelectedValue.ToString();
        //    //DataSet ds3 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJINFO", PactCode, "", "", "", "", "", "", "", "");
        //    //if (ds3 == null)
        //    //{           
        //    //    return;
        //    //}
        //    //DataTable dt01=ds3.Tables[0];
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    DataTable dt = (DataTable)Session["tblData"];
        //    ReportDocument rptsale = new RealERPRPT.R_22_Sal.RptUnsoldUnit();
        //    TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
        //    rptCname.Text = comnam;
        //    //TextObject rptCode = rptsale.ReportDefinition.ReportObjects["ProjName"] as TextObject;
        //    //rptCode.Text =  this.ddlProjectName.SelectedItem.Text;

        //    //TextObject location = rptsale.ReportDefinition.ReportObjects["txt1"] as TextObject;
        //    //location.Text = (dt01.Rows.Count == 0) ? "" : "Location: " + dt01.Rows[0]["padd"].ToString();
        //    //TextObject hdat = rptsale.ReportDefinition.ReportObjects["txt2"] as TextObject;
        //    //hdat.Text = (dt01.Rows.Count == 0) ? "" : "Hand Over: " + dt01.Rows[0]["hdate"].ToString();
        //    //TextObject btype = rptsale.ReportDefinition.ReportObjects["txt3"] as TextObject;
        //    //btype.Text = (dt01.Rows.Count == 0) ? "" : "Building Type: " + dt01.Rows[0]["prjtyp"].ToString();
        //    //TextObject dpay = rptsale.ReportDefinition.ReportObjects["txt4"] as TextObject;
        //    //dpay.Text = (dt01.Rows.Count == 0) ? "" : "Down Payment: " + dt01.Rows[0]["downpay"].ToString();
        //    //TextObject loanfac = rptsale.ReportDefinition.ReportObjects["txt5"] as TextObject;
        //    //loanfac.Text = (dt01.Rows.Count == 0) ? "" : "Loan Facility: " + dt01.Rows[0]["loan"].ToString();

        //    TextObject rptDate = rptsale.ReportDefinition.ReportObjects["txtDate"] as TextObject;
        //    rptDate.Text = "As on Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
        //    TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rptsale.SetDataSource(dt);
        //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    rptsale.SetParameterValue("ComLogo", ComLogo);
        //    Session["Report1"] = rptsale;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //}

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Type = this.Request.QueryString["Type"].ToString();


            //string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd.MM.yyyy");
            //string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd.MM.yyyy");

            string session = hst["session"].ToString();

            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblData"];
            var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptUnsoldUnit>();



            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptUnsoldUnit", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtdate", "As on Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Unsold Status"));
            Rpt1.SetParameters(new ReportParameter("RptFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }




        protected void ddlRptGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void gvStock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblgvPactdesc = (Label)e.Row.FindControl("lblgvPactdesc");
                Label lgvUsize = (Label)e.Row.FindControl("lgvUsize");
                Label lgvUAmt = (Label)e.Row.FindControl("lgvUAmt");
                Label lgvPamt = (Label)e.Row.FindControl("lgvPamt");
                Label lgvUtAmt = (Label)e.Row.FindControl("lgvUtAmt");
                Label lgvCoAmt = (Label)e.Row.FindControl("lgvCoAmt");
                Label lgvUsAmt = (Label)e.Row.FindControl("lgvUsAmt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lblgvPactdesc.Font.Bold = true;
                    lgvUsize.Font.Bold = true;
                    lgvUAmt.Font.Bold = true;
                    lgvPamt.Font.Bold = true;
                    lgvUtAmt.Font.Bold = true;
                    lgvCoAmt.Font.Bold = true;
                    lgvUsAmt.Font.Bold = true;
                    lblgvPactdesc.Style.Add("text-align", "right");


                }

            }

        }
    }
}