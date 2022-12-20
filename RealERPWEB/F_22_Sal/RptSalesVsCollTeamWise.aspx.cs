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

namespace RealERPWEB.F_22_Sal
{
    public partial class RptSalesVsCollTeamWise : System.Web.UI.Page
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

                if (this.Request.QueryString["Type"] == "SalesTeam")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Executive Wise Sales";
                }

                else
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "";
                }

                string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01-" + ASTUtility.Right(Date, 8);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                this.GetGroup();
                this.SalesPerson();

                this.SelectView();
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
           // ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void SalesPerson() 
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT03", "GETSALESPERSON", "", "", "", "", "", "", "", "", "");
            this.ddlSalesperson.DataTextField = "salpname";
            this.ddlSalesperson.DataValueField = "salpercode";
            this.ddlSalesperson.DataSource = ds1.Tables[0];
            this.ddlSalesperson.DataBind();
        }

        private void GetGroup()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT02", "GETGROUP", "", "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];
            DataRow dr1 = dt.NewRow();
            dr1["grpcode"] = "000000000000";
            dr1["grpdesc"] = "All Type";
            dt.Rows.Add(dr1);

            this.ddlgrp.DataTextField = "grpdesc";
            this.ddlgrp.DataValueField = "grpcode";
            this.ddlgrp.DataSource = ds1.Tables[0];
            this.ddlgrp.DataBind();
            this.ddlgrp.SelectedValue = "000000000000";


            


        }

        private void SelectView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                
                case "SalesTeam":
                    this.MultiView1.ActiveViewIndex = 0;
                   
                    break;


                

            }


        }




        private string GetLOType()
        {
            string Type = this.Request.QueryString["Type"];
            string lotype = "";
            switch (Type)
            {
                case "MonsalVsAchieveLO":
                    lotype = "lotype";
                    break;
                default:
                    break;

            }
            return lotype;
        }


        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();

            switch (Type)
            {
                case "SalesTeam":
              
                    this.ShowData();
                    break;


               

            }


        }


        private void ShowData()
        {
            Session.Remove("tblsales");
            string comcod = this.GetComeCode();         
            string frmdate = this.txtfrmdate.Text.Trim();
            string todate = this.txttodate.Text.Trim();
            string lotype = "";   //this.GetLOType();
            string grpcode = this.ddlgrp.SelectedValue.ToString() == "000000000000" ? "51%" : this.ddlgrp.SelectedValue.ToString() + "%";
            string Salesteam = this.ddlSalesperson.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSalesperson.SelectedValue.ToString() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_COLLECTIONMGT", "GETTOSEALESVSCOLLTEAMWISE", "", frmdate, todate, lotype, Salesteam, grpcode, "", "");
            if (ds1 == null)
            {
                this.gvsalesvscoll.DataSource = null;
                this.gvsalesvscoll.DataBind();


                return;
            }
            //Session["tblsales"] = ((DataTable)ds1.Tables[0]).Copy();

            //Session["tblsalesvscoll"] = this.HiddenSameData(ds1.Tables[0]);

            Session["tblsales"] = ds1.Tables[0];

            this.Data_Bind();

        }


        private void Data_Bind()
        {

           

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblsales"];
          

            switch (Type)
            {
                case "SalesTeam":
                
                    this.gvsalesvscoll.DataSource = dt;
                    this.gvsalesvscoll.DataBind();
                    
                    break;


                
            }





            // this.FooterCal();

            //Session["Report1"] = gvothcoll;
            //((HyperLink)this.gvothcoll.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

    }


   
}