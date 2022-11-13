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
    public partial class RptSalesVsAchievement : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = this.Request.QueryString["Type"]== "MonsalVsAchieve" ? "Month Wise Sales (Reconcilation)":
                    this.Request.QueryString["Type"] == "MonsalVsAchieveLO" ?  "Month Wise Sales (Reconcilation L/O)": "Down Payment Status (Prev.Sales)";
                string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01-" + ASTUtility.Right(Date, 8);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.ProjectName();
                this.GetGroup();
                this.SelectView();
               
               


            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void ProjectName()
        {
            string comcod = this.GetComeCode();
            string Srchpactcode = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT03", "GETPROJECTNAME02", Srchpactcode, "", "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "pactdesc";
            this.ddlPrjName.DataValueField = "pactcode";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();


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


            ////DataTable dt = ds1.Tables[0];
            ////DataRow dr1 = dt.NewRow();
            ////dr1["pactcode"] = "000000000000";
            ////dr1["pactdesc"] = "All Project";
            ////dt.Rows.Add(dr1);

            ////this.DropCheck1.DataTextField = "pactdesc1";
            ////this.DropCheck1.DataValueField = "pactdesc1";
            ////this.DropCheck1.DataSource = dt;
            ////this.DropCheck1.Text = "000000000000-All Project";
            ////this.DropCheck1.DataBind();

            //this.chkProjectName.DataTextField = "pactdesc";
            //this.chkProjectName.DataValueField = "pactcode";
            //this.chkProjectName.DataSource = dt;
            //this.chkProjectName.DataBind();


        }

        private void SelectView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "MonsalVsAchieveLO":
                case "MonsalVsAchieve":                  
                    this.MultiView1.ActiveViewIndex = 0;
                    this.Visibility();
                    break;


                case "DownpayClearnce":
                    this.MultiView1.ActiveViewIndex = 1;
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

        private void Visibility()
        {
            string Type = this.Request.QueryString["Type"];
            if (Type == "MonsalVsAchieveLO")
            {
                this.gvsalesvscoll.Columns[12].HeaderText = "Received Amount </br> Finlay Prom.";
                this.gvsalesvscoll.Columns[13].Visible = true;
                this.gvsalesvscoll.Columns[14].Visible = true;

            }

        }




        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
           
            switch (Type)
            {
                case "MonsalVsAchieveLO":
                case "MonsalVsAchieve":
                    this.GetSaleReconcilation();
                    break;


                case "DownpayClearnce":
                    this.ShowDownPayment();
                    break;

            }


        }

        private void ShowDownPayment()
        {
            Session.Remove("tblsalesvscoll02");
            string comcod = this.GetComeCode();
            string prjcode = this.ddlPrjName.SelectedValue.ToString() == "000000000000" ? "18%" : this.ddlPrjName.SelectedValue.ToString() + "%";
            string frmdate = this.txtfrmdate.Text.Trim();
            string todate = this.txttodate.Text.Trim();
            string lotype = "";   //this.GetLOType();
            string grpcode = this.ddlgrp.SelectedValue.ToString() == "000000000000" ? "51%" : this.ddlgrp.SelectedValue.ToString() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT03", "GETSALESDOWNPAYMENTCLEARANCE", prjcode, frmdate, todate, grpcode, lotype, "", "", "", "");
            if (ds1 == null)
            {
                this.gvDownpayment.DataSource = null;
                this.gvDownpayment.DataBind();

                return;
            }
            Session["tblsalesvscoll02"] = ((DataTable)ds1.Tables[0]).Copy();

            Session["tblsalesvscoll"] = this.HiddenSameData(ds1.Tables[0]);

            Session["tbltypecount"] = ds1.Tables[1];

            this.Data_Bind();

        }


        private void GetSaleReconcilation()
        {

            Session.Remove("tblsalesvscoll02");
            string comcod = this.GetComeCode();
            string prjcode = this.ddlPrjName.SelectedValue.ToString() == "000000000000" ? "18%" : this.ddlPrjName.SelectedValue.ToString() + "%";
            string frmdate = this.txtfrmdate.Text.Trim();
            string todate = this.txttodate.Text.Trim();
            string lotype = this.GetLOType();
            string grpcode = this.ddlgrp.SelectedValue.ToString() == "000000000000" ? "51%" : this.ddlgrp.SelectedValue.ToString() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT03", "GETSALESVUCOLLECTION", prjcode, frmdate, todate, grpcode, lotype, "", "", "", "");
            if (ds1 == null)
            {
                this.gvsalesvscoll.DataSource = null;
                this.gvsalesvscoll.DataBind();

                return;
            }
            Session["tblsalesvscoll02"] = ((DataTable)ds1.Tables[0]).Copy();

            Session["tblsalesvscoll"] = this.HiddenSameData(ds1.Tables[0]);

            Session["tbltypecount"] = ds1.Tables[1];

            this.Data_Bind();



        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string typecode = dt1.Rows[0]["typecode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["typecode"].ToString() == typecode)
                {
                    typecode = dt1.Rows[j]["typecode"].ToString();
                    dt1.Rows[j]["typedesc"] = "";
                }

                else
                {
                    typecode = dt1.Rows[j]["typecode"].ToString();

                }

            }

            return dt1;
        }

        private void Data_Bind()
        {
           
            //this.gvsalesvscoll.DataSource = (DataTable)Session["tblsalesvscoll"];
            //this.gvsalesvscoll.DataBind();

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblsalesvscoll"];
            switch (Type)
            {
                case "MonsalVsAchieveLO":
                case "MonsalVsAchieve":
                    this.gvsalesvscoll.DataSource = dt;
                    this.gvsalesvscoll.DataBind();
                   // this.FooterCalculation(dt);
                    break;


                case "DownpayClearnce":
                    this.gvDownpayment.DataSource = dt;
                    this.gvDownpayment.DataBind();
                    //this.FooterCalculation(dt);
                    break;

            }





            // this.FooterCal();

            //Session["Report1"] = gvothcoll;
            //((HyperLink)this.gvothcoll.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblgrpsoldunsold"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvsalesvscoll.FooterRow.FindControl("lblFsalableunit")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tqty)", "")) ? 0.00 :
                dt.Compute("sum(tqty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsalesvscoll.FooterRow.FindControl("lblFtotalsize")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tusize)", "")) ? 0.00 :
              dt.Compute("sum(tusize)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsalesvscoll.FooterRow.FindControl("lblFsoldunit")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(sqty)", "")) ? 0.00 :
             dt.Compute("sum(sqty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsalesvscoll.FooterRow.FindControl("lblFsoldarea")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(susize)", "")) ? 0.00 :
             dt.Compute("sum(susize)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvsalesvscoll.FooterRow.FindControl("lblFunsoldunit")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(usqty)", "")) ? 0.00 :
            dt.Compute("sum(usqty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsalesvscoll.FooterRow.FindControl("lblFgvunsoldarea")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(usize)", "")) ? 0.00 :
            dt.Compute("sum(usize)", ""))).ToString("#,##0.00;(#,##0); ");

            ((Label)this.gvsalesvscoll.FooterRow.FindControl("lblFgvtsoldamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(suamt)", "")) ? 0.00 :
            dt.Compute("sum(suamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsalesvscoll.FooterRow.FindControl("lblFgvunsoldamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(usuamt)", "")) ? 0.00 :
            dt.Compute("sum(usuamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsalesvscoll.FooterRow.FindControl("lblFgvcarparking")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(parking)", "")) ? 0.00 :
            dt.Compute("sum(parking)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvsalesvscoll.FooterRow.FindControl("lblFgvutility")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(utility)", "")) ? 0.00 :
            dt.Compute("sum(utility)", ""))).ToString("#,##0;(#,##0); ");



            //Session["Report1"] = gvsoldunsold;
            //((HyperLink)this.gvsoldunsold.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString().Trim();

            switch (Type)
            {
                case "MonsalVsAchieveLO":
                case "MonsalVsAchieve":
                case "DownpayClearnce":
                    this.PrintSaleReconcilation();
                    break;


                //case "DownpayClearnce":
                //    this.ShowDownPayment();
                //    break;

            }


        }


        private void PrintSaleReconcilation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMMM-yyyy");
            string type = this.ddlgrp.SelectedValue.ToString();
            string projectName = "";
            DataTable dt = (DataTable)Session["tblsalesvscoll02"];
            DataTable dt1 = (DataTable)Session["tbltypecount"];


            string shopno, aptno, officeno, totalsal;
            string grp = ASTUtility.Left(this.ddlgrp.SelectedValue.ToString(), 7);
            switch (grp)
            {
                case "5101001": //  shop
                    shopno = dt1.Rows[0]["shopno"].ToString() + " Units";
                    aptno = "";
                    officeno = "";
                    totalsal = "";
                    break;

                case "5101003": // appartment
                    shopno = "";
                    aptno = dt1.Rows[0]["aptno"].ToString() + " Units";
                    officeno = "";
                    totalsal = "";
                    break;

                default:
                    shopno = dt1.Rows[0]["shopno"].ToString() + " Units";
                    aptno = dt1.Rows[0]["aptno"].ToString() + " Units";
                    officeno = dt1.Rows[0]["officeno"].ToString() + " Units";
                    totalsal = "Total Sales              :   " + (Convert.ToDouble(dt1.Rows[0]["aptno"]) + Convert.ToDouble(dt1.Rows[0]["shopno"])).ToString() + " Units";
                    break;
            }


            LocalReport Rpt1 = new LocalReport();

            if (this.Request.QueryString["Type"] == "MonsalVsAchieveLO")

            {

                var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesvsAchievement>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptSalesVsAchivementLO", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("printdate", printdate));
                Rpt1.SetParameters(new ReportParameter("totalsal", totalsal));
                Rpt1.SetParameters(new ReportParameter("projectName", projectName));
                Rpt1.SetParameters(new ReportParameter("shopno", shopno));
                Rpt1.SetParameters(new ReportParameter("aptno", aptno));
                //Rpt1.SetParameters(new ReportParameter("officeno", officeno));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Achievement for month of " + frmdate + " to " + todate));
                Rpt1.SetParameters(new ReportParameter("RptTitle1", "Monthly Sales Report (External Sales)"));
                Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("grp", grp));
                //Rpt1.SetParameters(new ReportParameter("date", "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));
            }
            else if (this.Request.QueryString["Type"] == "DownpayClearnce")
            {

                var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesvsAchievement>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptSalesVsAchivementDPC", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("printdate", printdate));
                Rpt1.SetParameters(new ReportParameter("totalsal", totalsal));
                Rpt1.SetParameters(new ReportParameter("projectName", projectName));
                Rpt1.SetParameters(new ReportParameter("shopno", shopno));
                Rpt1.SetParameters(new ReportParameter("aptno", aptno));
                //Rpt1.SetParameters(new ReportParameter("officeno", officeno));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Achievement for month of " + frmdate + " to " + todate));
                Rpt1.SetParameters(new ReportParameter("RptTitle1", "Down Payment Status (Prev.Sales)"));
                Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("grp", grp));
                //Rpt1.SetParameters(new ReportParameter("date", "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));
            }
            else
            {

                var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesvsAchievement>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptSalesVsAchivement", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("printdate", printdate));
                Rpt1.SetParameters(new ReportParameter("totalsal", totalsal));
                Rpt1.SetParameters(new ReportParameter("projectName", projectName));
                Rpt1.SetParameters(new ReportParameter("shopno", shopno));
                Rpt1.SetParameters(new ReportParameter("aptno", aptno));
                //Rpt1.SetParameters(new ReportParameter("officeno", officeno));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Achievement for month of " + frmdate + " to " + todate));
                Rpt1.SetParameters(new ReportParameter("RptTitle1", "Monthly Sales Report"));
                Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("grp", grp));
            }


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


    }

}