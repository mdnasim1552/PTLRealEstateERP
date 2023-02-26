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
using RealERPRDLC;

namespace RealERPWEB.F_09_PImp
{
    public partial class RptLabourSearchRaWise : System.Web.UI.Page
    {
        ProcessAccess BgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
           if(!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();              
                this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.GetProjectName();

            }

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string serch1 = "%";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrjlist.DataTextField = "pactdesc";
            this.ddlPrjlist.DataValueField = "pactcode";
            this.ddlPrjlist.DataSource = ds1.Tables[0];
            this.ddlPrjlist.DataBind();
            this.GetLabour();
            this.ShowFloorcode();

        }

       
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void ddlPrjlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetLabour();
        }


        private void GetLabour()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlPrjlist.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETLABOURLIST", pactcode, date, "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];         
            this.ddlLabour.DataTextField = "rsirdesc";
            this.ddlLabour.DataValueField = "rsircode";
            this.ddlLabour.DataSource = dt;
            this.ddlLabour.DataBind();
           
        }





        private void ShowFloorcode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlPrjlist.SelectedValue.ToString();
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", "GETFLOORCOD", pactcode, "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];

            DataRow dr2 = dt.NewRow();
            dr2["flrcod"] = "000";
            dr2["flrdes"] = "All Floors-Sum";
            DataRow dr3 = dt.NewRow();
            dr3["flrcod"] = "AAA";
            dr3["flrdes"] = "All Floors-Details";
            dt.Rows.Add(dr2);
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

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {


            Session.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlPrjlist.SelectedValue.ToString();          
            string date = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");      
            string flrcod = this.ddlFloor.SelectedValue.ToString();
            string labour = this.ddlLabour.SelectedValue.ToString();


            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTPERIODICSUBCONPREQTYRAWISE", PactCode, date, flrcod, labour,"", "", "", "");
            if (ds1 == null)
            {
                this.gvsubbill.DataSource = null;
                this.gvsubbill.DataBind();
                return;
            }
            this.gvsubbill.DataSource = ds1.Tables[0];
            this.gvsubbill.DataBind();
            ViewState["tblData"] = ds1.Tables[0];
             this.FooterCalculation();

        }


        private void FooterCalculation()
        {
            
            DataTable dt = (DataTable)ViewState["tblData"];
            if (dt.Rows.Count == 0)
                return;

           
            ((Label)this.gvsubbill.FooterRow.FindControl("lgvFissueqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(isuqty)", "")) ? 0.00 :
                 dt.Compute("sum(isuqty)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvsubbill.FooterRow.FindControl("lgvafbillamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(isuamt)", "")) ? 0.00 :
                 dt.Compute("sum(isuamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            Session["Report1"] = gvsubbill;
            ((HyperLink)this.gvsubbill.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblData"];
            //var list = dt.DataTableToList<RealEntity.C_09_PIMP.SubConBill.SubConAllBill>();

            //LocalReport Rpt1 = new LocalReport();
            //Rpt1 = RptSetupClass1.GetLocalReport("R_09_PIMP.rptsubconbill", list, null, null);
            //Rpt1.SetParameters(new ReportParameter("compName", comnam));
            //Rpt1.SetParameters(new ReportParameter("projectName", this.ddlProjectName.SelectedItem.Text.ToString().Substring(17)));
            //Rpt1.SetParameters(new ReportParameter("txtSubConName", "Sub-Contractor Name: " + this.ddlSubName.SelectedItem.Text.ToString().Substring(13)));
            //Rpt1.SetParameters(new ReportParameter("rptTitle", "Bill Details - Sub-Contractor"));
            //Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy")));
            //Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));


            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

       
    }
}