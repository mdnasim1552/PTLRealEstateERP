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
    public partial class MonthlySalesBudget : System.Web.UI.Page
    {
        ProcessAccess SalesData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Monthly Budget (Sales & Collection)";
                this.ViewSection();
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

        private void ViewSection()

        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Monthly":
                    this.GetYearMonth();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "Yearly":
                    this.GetYear();
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "DailyEntry":
                    this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "MonthlyTypeWise":
                    this.GetYearMonth02();
                    this.MultiView1.ActiveViewIndex = 3;
                    break;
            }


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        private void GetYear()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETYEAR", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyear.DataTextField = "year1";
            this.ddlyear.DataValueField = "year1";
            this.ddlyear.DataSource = ds1.Tables[0];
            this.ddlyear.DataBind();
            this.ddlyear.SelectedValue = System.DateTime.Today.Year.ToString();
            ds1.Dispose();
        }
        private void GetYearMonth()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];
            this.ddlyearmon.DataBind();
            this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            ds1.Dispose();


        }


        private void GetYearMonth02()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlmonthtypeWise.DataTextField = "yearmon";
            this.ddlmonthtypeWise.DataValueField = "ymon";
            this.ddlmonthtypeWise.DataSource = ds1.Tables[0];
            this.ddlmonthtypeWise.DataBind();
            this.ddlmonthtypeWise.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            ds1.Dispose();


        }

        
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            string comcod = this.GetCompCode();
            string YearMon = this.ddlyearmon.SelectedValue.ToString();
            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETSBUDGETINFO", YearMon, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSalbgd.DataSource = null;
                this.gvSalbgd.DataBind();
                return;


            }
            ViewState["tblsal"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }



        private void Data_Bind()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable tbl1 = (DataTable)ViewState["tblsal"];
            switch (Type)
            {
                case "Monthly":
                    this.gvSalbgd.DataSource = tbl1;
                    this.gvSalbgd.DataBind();
                    this.FooterCalculation(tbl1);
                    break;

                case "Yearly":
                    this.gvySalbgd.DataSource = tbl1;
                    this.gvySalbgd.DataBind();
                    this.FooterCalculation(tbl1);
                    break;

                case "DailyEntry":
                    this.gvDailyEntry.DataSource = tbl1;
                    this.gvDailyEntry.DataBind();
                    break;

                case "MonthlyTypeWise":
                    this.gvsbgdTypeWise.DataSource = tbl1;
                    this.gvsbgdTypeWise.DataBind();
                    break;

                    

            }


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string deptcode;
            string type = this.Request.QueryString["Type"].ToString().Trim();


            switch (type)
            {
                case "DailyEntry":
                case "Monthly":

                    deptcode = dt1.Rows[0]["deptcode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                            dt1.Rows[j]["deptname"] = "";

                        deptcode = dt1.Rows[j]["deptcode"].ToString();
                    }

                    break;
            }


            return dt1;

        }


        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;


            string Type = this.Request.QueryString["Type"].ToString().Trim();

            switch (Type)
            {
                case "Monthly":
                    ((Label)this.gvSalbgd.FooterRow.FindControl("lgvFSaleTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(saleamt)", "")) ? 0.00
                     : dt.Compute("Sum(saleamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvSalbgd.FooterRow.FindControl("lgvFCollTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(collamt)", "")) ? 0.00
                      : dt.Compute("Sum(collamt)", ""))).ToString("#,##0;(#,##0);  ");

                    ((Label)this.gvSalbgd.FooterRow.FindControl("lgvFtoSaleTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tsaleamt)", "")) ? 0.00
                      : dt.Compute("Sum(tsaleamt)", ""))).ToString("#,##0;(#,##0);  ");

                    ((Label)this.gvSalbgd.FooterRow.FindControl("lgvFtoCollTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tcollamt)", "")) ? 0.00
                     : dt.Compute("Sum(tcollamt)", ""))).ToString("#,##0;(#,##0);  ");
                    break;

                case "Yearly":
                    ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFSaleTotalyb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(saleamt)", "")) ? 0.00
                        : dt.Compute("Sum(saleamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFCollTotalyb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(collamt)", "")) ? 0.00
                        : dt.Compute("Sum(collamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFPurTotalyb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(puramt)", "")) ? 0.00
                          : dt.Compute("Sum(puramt)", ""))).ToString("#,##0;(#,##0);  ");

                    ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFtoSaleTotalyb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tsaleamt)", "")) ? 0.00
                        : dt.Compute("Sum(tsaleamt)", ""))).ToString("#,##0;(#,##0);  ");

                    ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFtoCollTotalyb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tcollamt)", "")) ? 0.00
                        : dt.Compute("Sum(tcollamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFtoPurTotalyb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tpuramt)", "")) ? 0.00
                          : dt.Compute("Sum(tpuramt)", ""))).ToString("#,##0;(#,##0);  ");
                    break;

            }







        }

        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)ViewState["tblsal"];
            string Type = this.Request.QueryString["Type"].ToString().Trim();

            switch (Type)
            {
                case "Monthly":
                    for (int i = 0; i < this.gvSalbgd.Rows.Count; i++)
                    {

                        tbl1.Rows[i]["saleamt"] = Convert.ToDouble("0" + ((TextBox)this.gvSalbgd.Rows[i].FindControl("txtgvsalamt")).Text.Trim()).ToString();
                        tbl1.Rows[i]["collamt"] = Convert.ToDouble("0" + ((TextBox)this.gvSalbgd.Rows[i].FindControl("txtgvcollamt")).Text.Trim()).ToString();
                        tbl1.Rows[i]["tsaleamt"] = Convert.ToDouble("0" + ((TextBox)this.gvSalbgd.Rows[i].FindControl("txtogvtosalamt")).Text.Trim()).ToString();
                        tbl1.Rows[i]["tcollamt"] = Convert.ToDouble("0" + ((TextBox)this.gvSalbgd.Rows[i].FindControl("txtgvtocollamt")).Text.Trim()).ToString();

                    }
                    break;

                case "Yearly":
                    for (int i = 0; i < this.gvySalbgd.Rows.Count; i++)
                    {

                        tbl1.Rows[i]["saleamt"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvsalamtyb")).Text.Trim()).ToString();
                        tbl1.Rows[i]["collamt"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvcollamtyb")).Text.Trim()).ToString();
                        tbl1.Rows[i]["puramt"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvpuramtyb")).Text.Trim()).ToString();
                        tbl1.Rows[i]["tsaleamt"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtogvtosalamtyb")).Text.Trim()).ToString();
                        tbl1.Rows[i]["tcollamt"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvtocollamtyb")).Text.Trim()).ToString();
                        tbl1.Rows[i]["tpuramt"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvtopuramtyb")).Text.Trim()).ToString();

                    }

                    break;



                case "DailyEntry":
                    for (int i = 0; i < this.gvDailyEntry.Rows.Count; i++)
                    {

                        tbl1.Rows[i]["prticlars"] = Convert.ToDouble("0" + ((TextBox)this.gvDailyEntry.Rows[i].FindControl("txtgvpertuculars")).Text.Trim()).ToString();
                        tbl1.Rows[i]["remarks"] = ((TextBox)this.gvDailyEntry.Rows[i].FindControl("txtgvremarks")).Text.Trim();


                    }

                    break;

                case "MonthlyTypeWise":
                    for (int i = 0; i < this.gvsbgdTypeWise.Rows.Count; i++)
                    {

                        tbl1.Rows[i]["aptqty"] = Convert.ToDouble("0" + ((TextBox)this.gvsbgdTypeWise.Rows[i].FindControl("txtgvaptqty")).Text.Trim()).ToString();
                        tbl1.Rows[i]["shopqty"] = Convert.ToDouble("0" + ((TextBox)this.gvsbgdTypeWise.Rows[i].FindControl("txtgvshopqty")).Text.Trim()).ToString();
                        tbl1.Rows[i]["aptamt"] = Convert.ToDouble("0" + ((TextBox)this.gvsbgdTypeWise.Rows[i].FindControl("txtgvAptcollamt")).Text.Trim()).ToString();
                        tbl1.Rows[i]["shopamt"] = Convert.ToDouble("0" + ((TextBox)this.gvsbgdTypeWise.Rows[i].FindControl("txtgvShopcollamt")).Text.Trim()).ToString();

                    }
                    break;



            }
            ViewState["tblsal"] = tbl1;


        }

        protected void lbTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {

                string comcod = this.GetCompCode();
                this.SaveValue();
                DataTable dt1 = (DataTable)ViewState["tblsal"];
                string Yearmon = this.ddlyearmon.SelectedValue.ToString();
                bool result = true;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {


                    string empid = dt1.Rows[i]["empid"].ToString();
                    string saleamt = Convert.ToDouble(dt1.Rows[i]["saleamt"].ToString()).ToString();
                    string collamt = Convert.ToDouble(dt1.Rows[i]["collamt"].ToString()).ToString();
                    string tsaleamt = Convert.ToDouble(dt1.Rows[i]["tsaleamt"].ToString()).ToString();
                    string tcollamt = Convert.ToDouble(dt1.Rows[i]["tcollamt"].ToString()).ToString();
                    result = SalesData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "INSORUPSALCOLTARINF", Yearmon, empid, saleamt, collamt, tsaleamt, tcollamt, "", "", "", "", "", "", "", "", "");


                    if (result == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                        return;
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    }



                }




            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
            }
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {


                case "DailyEntry":
                    this.RptPrintDailyreport();
                    break;
                case "MonthlyTypeWise":
                    this.RptPrintMonthlyTypeWise();
                    break;
            }

        }

        protected void lbtnYearbgd_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string Year = this.ddlyear.SelectedValue.ToString();
            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETYSCOLPURBGDINFO", Year, "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvySalbgd.DataSource = null;
                this.gvySalbgd.DataBind();
                return;


            }
            ViewState["tblsal"] = ds1.Tables[0];
            this.Data_Bind();
        }

        protected void lbYearbgdTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnYBgdUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {

                string comcod = this.GetCompCode();
                this.SaveValue();
                DataTable dt1 = (DataTable)ViewState["tblsal"];
                string Year = this.ddlyear.SelectedValue.ToString();
                bool result = true;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {


                    string deptcode = dt1.Rows[i]["deptcode"].ToString();
                    string saleamt = Convert.ToDouble(dt1.Rows[i]["saleamt"].ToString()).ToString();
                    string collamt = Convert.ToDouble(dt1.Rows[i]["collamt"].ToString()).ToString();
                    string puramt = Convert.ToDouble(dt1.Rows[i]["puramt"].ToString()).ToString();

                    string tsaleamt = Convert.ToDouble(dt1.Rows[i]["tsaleamt"].ToString()).ToString();
                    string tcollamt = Convert.ToDouble(dt1.Rows[i]["tcollamt"].ToString()).ToString();
                    string tpuramt = Convert.ToDouble(dt1.Rows[i]["tpuramt"].ToString()).ToString();

                    result = SalesData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "INSORUPYSCOLPURBGDINF", Year, deptcode, saleamt, collamt, puramt, tsaleamt, tcollamt, tpuramt, "", "", "", "", "", "", "");


                    if (result == false)
                    {
                        this.lblmsg02.Text = "Updated Failed";
                        return;
                    }
                    else
                    {
                        this.lblmsg02.Text = "Updated Successfully";
                    }



                }




            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
            }

        }
        protected void lbtnDailyRpt_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("yyyyMMdd");
            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETDAILYACTIVITIES", date, "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvDailyEntry.DataSource = null;
                this.gvDailyEntry.DataBind();
                return;


            }
            ViewState["tblsal"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }
        protected void lbtnUpDailyEntry_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {

                string comcod = this.GetCompCode();
                this.SaveValue();
                DataTable dt1 = (DataTable)ViewState["tblsal"];
                string dayid = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("yyyyMMdd");
                string EntryDate = this.txtDate.Text.Trim();
                bool result = true;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {


                    string gencode = dt1.Rows[i]["gencode"].ToString();
                    string Perticulars = Convert.ToDouble(dt1.Rows[i]["prticlars"].ToString()).ToString();
                    string Remarks = dt1.Rows[i]["remarks"].ToString();
                    result = SalesData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "INORUPDAILYACVITIESINF", dayid, gencode, EntryDate, Perticulars, Remarks, "", "", "", "", "", "", "", "", "", "");


                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Failed');", true);
                        return;
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Successfullyd');", true);


            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
            }

        }

        private void RptPrintDailyreport()

        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string comcod = this.GetCompCode();
            string dayid = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("yyyyMMdd");
            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "RPTDAILYACTIVITIES", dayid, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = this.HiddenSameData(ds1.Tables[0]);
            ReportDocument rptsl = new RealERPRPT.R_32_Mis.RptDailyActivities();
            TextObject txtCompany = rptsl.ReportDefinition.ReportObjects["txtcompanyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtdate = rptsl.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptsl.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptsl.SetDataSource(dt);
            Session["Report1"] = rptsl;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        private void RptPrintMonthlyTypeWise()

        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");

            string yearmon = this.ddlmonthtypeWise.SelectedItem.Text;
            
          
            DataTable dt = (DataTable)ViewState["tblsal"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales.MonthlySalesBudget>();
           
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMonthlySalesBudget", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printdate", printdate));
           
            Rpt1.SetParameters(new ReportParameter("RptTitle", "MONTHLY SALES & COLLECTION TARGET (EXECUTIVE WISE)"));
            Rpt1.SetParameters(new ReportParameter("Rptprintdate", "Month Of " + yearmon));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("date", "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        protected void gvSalbgd_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lblgvDepartment");
                string empname = ((HyperLink)e.Row.FindControl("lblgvDepartment")).Text.ToString();
                string empid = ((Label)e.Row.FindControl("empid")).Text.ToString();
                string yearmon = this.ddlyearmon.SelectedValue;

                // string empid = this.txtfromdate.Text;
                //string empname = this.txtfromdate.Text;

                hlink1.NavigateUrl = "~/F_32_Mis/SalesTarget.aspx?empid=" + empid + "&empname=" + empname + "&yearmon=" + yearmon;

            }
        }

        protected void lblMontypeWise_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            string comcod = this.GetCompCode();
            string YearMon = this.ddlmonthtypeWise.SelectedValue.ToString();
            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETSALCOLLTARTYPEWISE", YearMon, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvsbgdTypeWise.DataSource = null;
                this.gvsbgdTypeWise.DataBind();
                return;


            }
            ViewState["tblsal"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        protected void lbTotalbgd_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }

        protected void lbtnbgdFinalUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {

                string comcod = this.GetCompCode();
                this.SaveValue();
                DataTable dt1 = (DataTable)ViewState["tblsal"];
                string Yearmon = this.ddlmonthtypeWise.SelectedValue.ToString();
                bool result = true;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {


                    string empid = dt1.Rows[i]["empid"].ToString();
                    string aptqty = Convert.ToDouble(dt1.Rows[i]["aptqty"].ToString()).ToString();
                    string shopqty = Convert.ToDouble(dt1.Rows[i]["shopqty"].ToString()).ToString();
                    string aptamt = Convert.ToDouble(dt1.Rows[i]["aptamt"].ToString()).ToString();
                    string shopamt = Convert.ToDouble(dt1.Rows[i]["shopamt"].ToString()).ToString();

                    result = SalesData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "INSERTORUPDATESALCOLTARTYPEINF", Yearmon, empid, aptqty, shopqty, aptamt, shopamt, "", "", "", "", "", "", "", "", "");


                    if (result == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                        return;
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    }



                }




            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
            }

        }

  

        protected void btnlink_Click(object sender, EventArgs e)
        {
            DateTime frmdate, date1, date2;
            string comcod = this.GetCompCode();
            string date = this.ddlmonthtypeWise.SelectedValue.ToString();
            date1 = Convert.ToDateTime(ASTUtility.DateFormat("01" + "." + date.Substring(4, 2) + "." + date.Substring(0, 4)));
            date2 = Convert.ToDateTime(date1.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy") + " 12:00:00 AM");           
            string date3 = Convert.ToDateTime(date1).ToString("dd-MMM-yyyy");
            string date4 = Convert.ToDateTime(date2).ToString("dd-MMM-yyyy");

            //btnlink.Attributes.Add("href", "~/F_22_Sal/RptSalSummery?Type=SaleVsCollTypeWise&comcod " + comcod + "&date1=" + date3 + "&date2=" + date4);
            //btnlink.Attributes.Add("target", "_blank");
             Response.Redirect("~/F_22_Sal/RptSalSummery?Type=SaleVsCollTypeWise&comcod=" + comcod + "&date1=" + date3 + "&date2="+ date4);

      

        }

        protected void gvsbgdTypeWise_RowCreated(object sender, GridViewRowEventArgs e)
        {

            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell1 = new TableCell();
                cell1.Text = "";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                cell1.ColumnSpan = 1;
                gvrow.Cells.Add(cell1);

                TableCell cell2 = new TableCell();
                cell2.Text = "";
                cell2.HorizontalAlign = HorizontalAlign.Center;
                cell2.ColumnSpan = 1;
                gvrow.Cells.Add(cell2);



                TableCell cell3 = new TableCell();
                cell3.Text = "Quantity";
                cell3.HorizontalAlign = HorizontalAlign.Center;
                cell3.ColumnSpan = 2;
                cell3.Font.Bold = true;
                gvrow.Cells.Add(cell3);





                TableCell cell4 = new TableCell();
                cell4.Text = "Collection";
                cell4.HorizontalAlign = HorizontalAlign.Center;
                cell4.ColumnSpan = 2;
                cell4.Font.Bold = true;
                gvrow.Cells.Add(cell4);




                gvsbgdTypeWise.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
            
    }
}