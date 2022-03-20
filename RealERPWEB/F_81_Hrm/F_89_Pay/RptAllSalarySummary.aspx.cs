﻿using Microsoft.Reporting.WinForms;
using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_89_Pay
{
    public partial class RptAllSalarySummary : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetMonth();
                lnkOk_Click(null,null);
            }
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetMonth()
        {
            string comcod = this.GetComCode();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETMONTHFOROFFDAY", "", "", "", "", "", "", "", "", "");
            this.ddlmon.DataTextField = "mnam";
            this.ddlmon.DataValueField = "yearmon";
            this.ddlmon.DataSource = ds2.Tables[0];
            this.ddlmon.DataBind();
            //this.ddlMonth.SelectedValue = System.DateTime.Today.ToString("dd-MM-yyyy").Trim();
            this.ddlmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM").Trim();
        }

        protected void rbtnAtten_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PanelVisivility();
        }

        private void PanelVisivility()
        {
            int rdbutton = this.rbtnAtten.SelectedIndex;
            switch (rdbutton)
            {
                case 0:
                    this.PnlBankSumary.Visible = true;
                    this.PnlModPayment.Visible = false;
                    this.PnlNetComparison.Visible = false;
                    this.PnlGrossSummary.Visible = false;
                    this.PnlGrossRecon.Visible = false;
                    this.PnlTotal.Visible = false;
                    
                    break;
                case 1:
                    this.PnlBankSumary.Visible = false;
                    this.PnlModPayment.Visible = true;
                    this.PnlNetComparison.Visible = false;
                    this.PnlGrossSummary.Visible = false;
                    this.PnlGrossRecon.Visible = false;
                    this.PnlTotal.Visible = false;
                    break;
                case 2:
                    this.PnlBankSumary.Visible = false;
                    this.PnlModPayment.Visible = false;
                    this.PnlNetComparison.Visible = true;
                    this.PnlGrossSummary.Visible = false;
                    this.PnlGrossRecon.Visible = false;
                    this.PnlTotal.Visible = false;
                    break;

                case 3:
                    this.PnlBankSumary.Visible = false;
                    this.PnlModPayment.Visible = false;
                    this.PnlNetComparison.Visible = false;
                    this.PnlGrossSummary.Visible = true;
                    this.PnlGrossRecon.Visible = false;
                    this.PnlTotal.Visible = false;
                    break;
                case 4:
                    this.PnlBankSumary.Visible = false;
                    this.PnlModPayment.Visible = false;
                    this.PnlNetComparison.Visible = false;
                    this.PnlGrossSummary.Visible = false;
                    this.PnlGrossRecon.Visible = true;
                    this.PnlTotal.Visible = false;
                    break;

                case 5:
                    this.PnlBankSumary.Visible = false;
                    this.PnlModPayment.Visible = false;
                    this.PnlNetComparison.Visible = false;
                    this.PnlGrossSummary.Visible = false;
                    this.PnlGrossRecon.Visible = true;
                    this.PnlTotal.Visible = true;
                    break;
                default:
                    this.PnlBankSumary.Visible = false;
                    this.PnlModPayment.Visible = true;
                    this.PnlNetComparison.Visible = false;
                    this.PnlGrossSummary.Visible = false;
                    this.PnlGrossRecon.Visible = false;
                    this.PnlTotal.Visible = false;

                    break;
            }
        }
        protected void lnkOk_Click(object sender, EventArgs e)
        {

            this.PanelVisivility();
            int index = this.rbtnAtten.SelectedIndex;
            switch (index)
            {
                case 0:
                    this.GetBankSummary();

                    break;

                case 1:
                    this.GetModeOfPayment();
                    break;

                case 2:
                    this.GetNetPayComparison();
                    break;
                case 3:
                    this.GetGrossPayComparison();
                    break;
                case 4:
                    this.GetGrossPayRecon();
                    break;
                case 5:
                    this.GetTotalSalSum();
                    break;
                default:
                    break;
            }          
        }

        private void GetTotalSalSum()
        {
            string comcod = this.GetComCode();
            string prevmon = "";
            string monthid = this.ddlmon.SelectedValue.ToString();
            string cudate = "";
            string date = "";
            switch (comcod)
            {
                case "3365":
                case "3101":
                    date = "26-" + ASTUtility.Month3digit(Convert.ToInt32(monthid.Substring(4, 2))) + "-" + monthid.Substring(0, 4);
                    prevmon = Convert.ToDateTime(date).AddMonths(-1).ToString("yyyyMM");
                    //cudate = date1.AddMonths(-1).ToString("dd-MMM-yyyy");
                    break;

                default:
                    date = "01-" + ASTUtility.Month3digit(Convert.ToInt32(monthid.Substring(4, 2))) + "-" + monthid.Substring(0, 4);
                    prevmon = Convert.ToDateTime(date).ToString("yyyyMM");
                    break;
            }
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_SALARY_RECON", "RPTNETCOMPARISONTOTAL", monthid, prevmon, "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                return;
            }

            Session["tblSalSummary"] = ds1.Tables[0];
            Session["tblmondesc"] = ds1.Tables[1];
            this.Data_bind();
        }

        private void GetGrossPayRecon()
        {
            string comcod = this.GetComCode();
            string monthid = this.ddlmon.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_SALARY_RECON", "RPTGROSSRECONCILMONTHWISE", monthid, "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                return;
            }

            Session["tblSalSummary"] = ds1.Tables[0];
         
            this.Data_bind();
        }

        private void GetGrossPayComparison()
        {
            string comcod = this.GetComCode();
            string prevmon = "";
            string monthid = this.ddlmon.SelectedValue.ToString();
            string cudate = "";
            string date = "";
            switch (comcod)
            {
                case "3365":
                case "3101":
                    date = "26-" + ASTUtility.Month3digit(Convert.ToInt32(monthid.Substring(4, 2))) + "-" + monthid.Substring(0, 4);
                    prevmon = Convert.ToDateTime(date).AddMonths(-1).ToString("yyyyMM");
                    //cudate = date1.AddMonths(-1).ToString("dd-MMM-yyyy");
                    break;

                default:
                    date = "01-" + ASTUtility.Month3digit(Convert.ToInt32(monthid.Substring(4, 2))) + "-" + monthid.Substring(0, 4);
                    prevmon = Convert.ToDateTime(date).ToString("yyyyMM");
                    break;
            }
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_SALARY_RECON", "RPTGROSSCOMPARISONMONTHWISE", monthid, prevmon, "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                return;
            }

            Session["tblSalSummary"] = ds1.Tables[0];
            Session["tblmondesc"] = ds1.Tables[1];
            this.Data_bind();
        }

        private void GetNetPayComparison()
        {
            string comcod = this.GetComCode();
            string prevmon = "";
            string monthid = this.ddlmon.SelectedValue.ToString();
            string cudate = "";
            string date = "";
            switch (comcod)
            {
                case "3365":
                case "3101":
                    date = "26-" + ASTUtility.Month3digit(Convert.ToInt32(monthid.Substring(4, 2))) + "-" + monthid.Substring(0, 4);
                    prevmon = Convert.ToDateTime(date).AddMonths(-1).ToString("yyyyMM");
                    //cudate = date1.AddMonths(-1).ToString("dd-MMM-yyyy");
                    break;

                default:
                    date = "01-" + ASTUtility.Month3digit(Convert.ToInt32(monthid.Substring(4, 2))) + "-" + monthid.Substring(0, 4);
                    prevmon = Convert.ToDateTime(date).ToString("yyyyMM");
                    break;
            }
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_SALARY_RECON", "RPTNETCOMPARISONMONTHWISE", monthid, prevmon, "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                return;
            }

            Session["tblSalSummary"] = ds1.Tables[0];
            Session["tblmondesc"] = ds1.Tables[1];
            this.Data_bind();
        }

        private void GetBankSummary()
        {
            string comcod = this.GetComCode();
            string monthid = this.ddlmon.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_SALARY_RECON", "RPTBANKSUMMARY", monthid, "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                return;
            }

            Session["tblSalSummary"] = ds1.Tables[0];
            this.Data_bind();
        }
        private void GetModeOfPayment()
        {
            string comcod = this.GetComCode();
            string monthid = this.ddlmon.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_SALARY_RECON", "RPTBANKDEATILSSUMMARY", monthid, "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                return;
            }

            Session["tblSalSummary"] = ds1.Tables[0];
            Session["tblbankdesc"] =ds1.Tables[1];
            this.Data_bind();
            //this.exportexcel();
        }

        private void exportexcel()
        {
            //comcod, grp, grpdesc, refno, refdesc, empid, empname, replacement, joresigndate, curamt, preamt, desig
        }

        private void Data_bind()
        {
            string comcod = this.GetComCode();

            DataTable dt = (DataTable)Session["tblSalSummary"];
            
            int index = this.rbtnAtten.SelectedIndex;
            switch (index)
            {
                case 0:
                    this.GvBankSummary.DataSource = dt;
                    this.GvBankSummary.DataBind();
                    Session["Report1"] = GvBankSummary;
                    ((HyperLink)this.GvBankSummary.HeaderRow.FindControl("hlbtntbCdataExcelbank")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;

                case 1:
                    DataTable dtp = (DataTable)Session["tblbankdesc"];
                    if (dtp == null)
                    {
                        return;
                    }
                    int i, j;
                    //for (i = 2; i < this.GvModPayment.Columns.Count - 1; i++)
                    //    this.GvModPayment.Columns[i].Visible = false;
                    j = 2;
                  
                    for (i = 0; i < dtp.Rows.Count; i++)
                    {                   
                        this.GvModPayment.Columns[j].HeaderText = dtp.Rows[i]["bankname"].ToString();
                        j++;                     
                    }
                    this.GvModPayment.DataSource = dt;
                    this.GvModPayment.DataBind();
                    Session["Report1"] = GvModPayment;
                    ((HyperLink)this.GvModPayment.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;
                case 2:               
                    //for (i = 2; i < this.GvModPayment.Columns.Count - 1; i++)
                    //    this.GvModPayment.Columns[i].Visible = false;
                    j = 1;
                    DataTable dtmon = (DataTable)Session["tblmondesc"];
                    if (dtmon == null)
                    {
                        return;
                    }
                    for (i = 0; i < dtmon.Rows.Count; i++)
                    {
                        this.GvNetComparison.Columns[j].HeaderText = dtmon.Rows[i]["monname"].ToString();
                        j++;
                    }
                    this.GvNetComparison.DataSource = dt;
                    this.GvNetComparison.DataBind();
                    Session["Report1"] = GvNetComparison;
                    ((HyperLink)this.GvNetComparison.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;

                case 3:
                    //for (i = 2; i < this.GvModPayment.Columns.Count - 1; i++)
                    //    this.GvModPayment.Columns[i].Visible = false;
                    j = 1;
                    DataTable dtmongross = (DataTable)Session["tblmondesc"];
                    if (dtmongross == null)
                    {
                        return;
                    }
                    for (i = 0; i < dtmongross.Rows.Count; i++)
                    {
                        this.GvgrossSalSummary.Columns[j].HeaderText = dtmongross.Rows[i]["monname"].ToString();
                        j++;
                    }
                    this.GvgrossSalSummary.DataSource = dt;
                    this.GvgrossSalSummary.DataBind();

                    Session["Report1"] = GvgrossSalSummary;
                    ((HyperLink)this.GvgrossSalSummary.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;


                case 4:               
                    this.GvGrossRecon.DataSource = dt;
                    this.GvGrossRecon.DataBind();

                    Session["Report1"] = GvGrossRecon;
                    ((HyperLink)this.GvGrossRecon.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;

                case 5:
                    //for (i = 2; i < this.GvModPayment.Columns.Count - 1; i++)
                    //    this.GvModPayment.Columns[i].Visible = false;
                    j = 1;
                    DataTable dtmontotal = (DataTable)Session["tblmondesc"];
                    if (dtmontotal == null)
                    {
                        return;
                    }
                    for (i = 0; i < dtmontotal.Rows.Count; i++)
                    {
                        this.GvTotalSumm.Columns[j].HeaderText = dtmontotal.Rows[i]["monname"].ToString();
                        j++;
                    }
                    this.GvTotalSumm.DataSource = dt;
                    this.GvTotalSumm.DataBind();

                    Session["Report1"] = GvTotalSumm;
                    ((HyperLink)this.GvTotalSumm.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;


                default:
                    break;

            }






        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            // ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
    
        }


        private void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string rptitle = "";
            DataTable dt = (DataTable)Session["tblSalSummary"];
            DataTable dt2 = (DataTable)Session["tblmondesc"];
            DataTable dt3 = (DataTable)Session["tblbankdesc"];



            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.AllBankSummary>();
     
          

            LocalReport Rpt1 = new LocalReport();

            int index = this.rbtnAtten.SelectedIndex;
            if (index == 0)
            {
                 rptitle= "Summary For Bank Advice";
                 Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptAllBankSummary", list, null, null);
                 Rpt1.EnableExternalImages = true;
            }
            else if (index == 1)
            {
                rptitle = "Mode of payment";
                var list3 = dt3.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.BankDesc>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptModePayment", list, list3, null);
                Rpt1.EnableExternalImages = true;
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string monname = dt3.Rows[i]["bankname"].ToString();
                    Rpt1.SetParameters(new ReportParameter("monname" + i.ToString(), monname));
                }
            }
            else if (index == 2)
            {
                rptitle = "Net Comparison";
                var list2 = dt2.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.MonthDesc>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptNetComparison", list, list2, null);
                Rpt1.EnableExternalImages = true;
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    string monname = dt2.Rows[i]["monname"].ToString();
                    Rpt1.SetParameters(new ReportParameter("monname" + i.ToString(), monname));
                }
            }
            else if (index == 3)
            {
                rptitle = "Gross Comparison";
                var list2 = dt2.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.MonthDesc>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptGrossComparison", list, list2, null);
                Rpt1.EnableExternalImages = true;


                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                  string monname = dt2.Rows[i]["monname"].ToString();
                    Rpt1.SetParameters(new ReportParameter("monname"+i.ToString(), monname));
                }
            }
            else 
            {
                rptitle = "Gross Recon";
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptGrossRecon", list, null, null);
                Rpt1.EnableExternalImages = true;
            }
      
   
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", rptitle));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
         

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



}

}