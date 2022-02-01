﻿using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
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
    public partial class PrintPaySlip : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            string Type = "";
            if (this.Request.QueryString["Type"].ToString() != "")
            {
                Type = this.Request.QueryString["Type"].ToString();
            }
            switch (Type)
            {
                case "paySlip":
                    this.PrintPaySlipAll();
                    break;
            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void PrintPaySlipAll()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string Inwords = "";
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string frmdate,  todate;
            string empid = this.Request.QueryString["empid"].ToString();
            string monthid = this.Request.QueryString["monthid"].ToString();
           

            frmdate = Convert.ToDateTime(ASTUtility.Right(monthid, 2) + "/01/" + ASTUtility.Left(monthid, 4)).ToString("dd-MMM-yyyy");
            todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            string month = Convert.ToDateTime(frmdate).ToString("MMM-yyyy");
            string projectcode = "%";
            string section = "%";
            string CompanyName = "94";           
            DataSet ds3 = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_PAYSLIP", "RPTPAYSLIP", frmdate, todate, projectcode, section, CompanyName, empid, "", "", "");
            DataTable dt = ds3.Tables[0];

            LocalReport Rpt1 = new LocalReport();

            if (comcod == "4301")
            {

                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptPaySlip1", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("txtMonth1", Convert.ToDateTime(todate).ToString("MMM, yyyy")));
                Rpt1.SetParameters(new ReportParameter("txtMonth2", Convert.ToDateTime(todate).ToString("MMM, yyyy")));
                Rpt1.SetParameters(new ReportParameter("txtDate1", Convert.ToDateTime(frmdate).ToString("dd-MMM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("txtDate2", Convert.ToDateTime(todate).ToString("dd-MMM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }
            // rdlc for greenwood
            else if (comcod == "3355" || comcod == "3101")
            {
                double netamt = Convert.ToDouble(dt.Rows[0]["netpay"]);
                string Inword = "In Word: " + ASTUtility.Trans(netamt, 2);
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>();
                LocalReport rpt = new LocalReport();
                rpt = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptPaySlipGreenwood", list, null, null);
                rpt.EnableExternalImages = true;
                rpt.SetParameters(new ReportParameter("txtCompName", comnam));
                rpt.SetParameters(new ReportParameter("txtTitle", "PAY SLIP"));
                rpt.SetParameters(new ReportParameter("txtDate", "FOR THE MONTH OF " + month));
                rpt.SetParameters(new ReportParameter("txtInword", Inword));
                rpt.SetParameters(new ReportParameter("comlogo", ComLogo));

                Session["Report1"] = rpt;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }

            else if (comcod == "3339" )
            {
                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptPaySlipTro", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "SALARY PAYMENT VOUCHER"));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }


            else if (comcod == "3347")
            {
                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptPaySlipPEB", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


            }

            else if (comcod == "3315")
            {

                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptPaySlipAssure", list, null, null);

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }

            else if (comcod == "3354")
            {

                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptPaySlipEdisonReal", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }

            else
            {
                // All Pay Slip Except Tropical, Peb Steel
                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptPaySlip", list, null, null);

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


            }


        }

    }
}