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

namespace RealERPWEB.F_32_Mis
{
    public partial class RptMisProIncomeExe : System.Web.UI.Page
    {
        ProcessAccess MISData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "PrjIncome") ? "BUDGETED INCOME - ALL PROJECT(SUMMARY)" :
                //    (this.Request.QueryString["Type"].ToString().Trim() == "MasPVsMonPVsExAllPro") ? "Master Plan, Monthly Plan Vs Executon - All Project" :
                //    (this.Request.QueryString["Type"].ToString().Trim() == "PrjExecution") ? "Work Execuation - All Project" :
                //    (this.Request.QueryString["Type"].ToString().Trim() == "ConBgdVsExe") ? "Budget Vs Execution. All Project" : "PROJECT ISSUE STATEMENT";
                this.SectionView();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //  this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);



        }


        private void SectionView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "PrjIncome":
                    this.Panel1.Visible = false;
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowProjectIncomeSt();
                    break;


                case "PrjExecution":
                    this.Panel1.Visible = true;
                    this.txtDatefrom.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "ConBgdVsExe":
                    this.Panel1.Visible = true;
                    this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    //this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.lblDaterange0.Text = "Date :";
                    this.lblDateto0.Visible = false;
                    this.txtDateto.Visible = false;
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "MasPVsMonPVsExAllPro":
                    this.Panel1.Visible = true;
                    this.txtDatefrom.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 3;
                    break;


            }
            if (ConstantInfo.LogStatus == true)
            {
                string comcod = this.GetComeCode();
                // string eventtype = this.lblHtitle.Text;
                string eventdesc = "Show Report: " + Type;
                string eventdesc2 = "";
                //bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {

                case "PrjExecution":
                    this.ShowProExecution();
                    break;
                case "ConBgdVsExe":
                    this.ShowConBgdVsExe();
                    break;

                case "MasPVsMonPVsExAllPro":
                    this.ShowMMonPlnVsAchAllPro();
                    break;


            }
        }

        private void ShowProjectIncomeSt()
        {
            Session.Remove("tbldata");
            string comcod = this.GetComeCode();
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS01", "RPTPROINCOMEST", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProIncome.DataSource = null;
                this.gvProIncome.DataBind();
                return;
            }
            Session["tbldata"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();

        }




        private void ShowProExecution()
        {
            Session.Remove("tbldata");
            string comcod = this.GetComeCode();
            string frmdate = this.txtDatefrom.Text.Trim();
            string todate = this.txtDateto.Text.Trim();
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS01", "RPTPROWORKISSUE", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProExecution.DataSource = null;
                this.gvProExecution.DataBind();
                return;
            }
            Session["tbldata"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();
        }
        private void ShowConBgdVsExe()
        {
            Session.Remove("tbldata");
            string comcod = this.GetComeCode();
            string frmdate = this.txtDatefrom.Text.Trim();
            string todate = this.txtDateto.Text.Trim();
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS01", "RPTCONBGDVSEXPENSES", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvConBgdVsExe.DataSource = null;
                this.gvConBgdVsExe.DataBind();
                return;
            }
            Session["tbldata"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();
        }


        private void ShowMMonPlnVsAchAllPro()
        {
            Session.Remove("tbldata");
            string comcod = this.GetComeCode();
            string frmdate = this.txtDatefrom.Text.Trim();
            string todate = this.txtDateto.Text.Trim();
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS", "RPTMASPVSMONPVSEX", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMMPlanVsAch.DataSource = null;
                this.gvMMPlanVsAch.DataBind();
                return;
            }
            Session["tbldata"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();

        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "MasPVsMonPVsExAllPro":
                    string grp = dt1.Rows[0]["grp"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";

                        grp = dt1.Rows[j]["grp"].ToString();
                    }
                    break;

            }


            return dt1;

        }

        private void Data_Bind()
        {

            DataTable dt = ((DataTable)Session["tbldata"]);
            if (dt.Rows.Count == 0)
                return;
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {

                case "PrjIncome":
                    this.gvProIncome.DataSource = dt;
                    this.gvProIncome.DataBind();
                    ((Label)this.gvProIncome.FooterRow.FindControl("lgvFinamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(inam)", "")) ? 0.00 : dt.Compute("sum(inam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProIncome.FooterRow.FindControl("lgvFcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdam)", "")) ? 0.00 : dt.Compute("sum(bgdam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProIncome.FooterRow.FindControl("lgvFmargin")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(maram)", "")) ? 0.00 : dt.Compute("sum(maram)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "PrjExecution":
                    this.gvProExecution.DataSource = dt;
                    this.gvProExecution.DataBind();
                    ((Label)this.gvProExecution.FooterRow.FindControl("lgvFpreamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pream)", "")) ? 0.00 : dt.Compute("sum(pream)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProExecution.FooterRow.FindControl("lgvFcuramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curam)", "")) ? 0.00 : dt.Compute("sum(curam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProExecution.FooterRow.FindControl("lgvFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toam)", "")) ? 0.00 : dt.Compute("sum(toam)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "ConBgdVsExe":
                    this.gvConBgdVsExe.DataSource = dt;
                    this.gvConBgdVsExe.DataBind();
                    ((Label)this.gvConBgdVsExe.FooterRow.FindControl("lgvFBgdCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ? 0.00 : dt.Compute("sum(bgdamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvConBgdVsExe.FooterRow.FindControl("lgvFEexcution")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(exeam)", "")) ? 0.00 : dt.Compute("sum(exeam)", ""))).ToString("#,##0;(#,##0); ");

                    //((Label)this.gvProExecution.FooterRow.FindControl("lgvFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toam)", "")) ? 0.00 : dt.Compute("sum(toam)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "MasPVsMonPVsExAllPro":

                    this.gvMMPlanVsAch.DataSource = dt;
                    this.gvMMPlanVsAch.DataBind();


                    double Bgd = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bdgamt)", "")) ? 0.00 : dt.Compute("sum(bdgamt)", "")));
                    double masplan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(masplan)", "")) ? 0.00 : dt.Compute("sum(masplan)", "")));
                    double monplan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(monplan)", "")) ? 0.00 : dt.Compute("sum(monplan)", "")));
                    double Execution = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(excution)", "")) ? 0.00 : dt.Compute("sum(excution)", "")));

                    double peronmasplan = (masplan == 0) ? 0.00 : ((Execution * 100) / masplan);
                    double peronmonplan = (monplan == 0) ? 0.00 : ((Execution * 100) / monplan); ;


                    ((Label)this.gvMMPlanVsAch.FooterRow.FindControl("lgvFbdgamt")).Text = Bgd.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMMPlanVsAch.FooterRow.FindControl("lgvFmasPlan")).Text = masplan.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMMPlanVsAch.FooterRow.FindControl("lgvFmonPlan")).Text = monplan.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMMPlanVsAch.FooterRow.FindControl("lgvFExecutionpAC")).Text = Execution.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMMPlanVsAch.FooterRow.FindControl("lgvFperonmasplan")).Text = peronmasplan.ToString("#,##0.00;(#,##0.00); ") + "%";
                    ((Label)this.gvMMPlanVsAch.FooterRow.FindControl("lgvFperonmonplan")).Text = peronmonplan.ToString("#,##0.00;(#,##0.00); ") + "%";


                    break;



            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "PrjIncome":

                    this.PrintProjectIncomeSt();
                    break;
                case "PrjExecution":
                    this.PrintProjectPrjExecution();
                    break;
                case "ConBgdVsExe":
                    this.PrintConBgdVsExe();
                    break;
                case "MasPVsMonPVsExAllPro":
                    this.PrintMMonPlnVsAchAllPro();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string comcod = this.GetComeCode();
                //string eventtype = this.lblHtitle.Text;
                string eventdesc = "Print Report: " + Type;
                string eventdesc2 = "";
                //bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void PrintProjectIncomeSt()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptstk = new RealERPRPT.R_32_Mis.rptBgdInSt();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["CompName"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptstk.SetDataSource((DataTable)Session["tbldata"]);
            //string comcod = this.GetComeCode();
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintProjectPrjExecution()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            this.txtDatefrom.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
            this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptWorkExecution();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["compName"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = "Date :" + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + " To :" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptstk.SetDataSource((DataTable)Session["tbldata"]);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintConBgdVsExe()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tbldata"];

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.MMonPlnVsAchAllPro>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptMasMonPlanVsAcvment", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("date", "(From " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("txtPeronMasplan", ((Label)this.gvMMPlanVsAch.FooterRow.FindControl("lgvFperonmasplan")).Text));
            Rpt1.SetParameters(new ReportParameter("txtPeronMonplan", ((Label)this.gvMMPlanVsAch.FooterRow.FindControl("lgvFperonmonplan")).Text));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("comLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Budget Vs Execution - All Project"));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintMMonPlnVsAchAllPro()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["tbldata"];

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.MMonPlnVsAchAllPro>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptMasMonPlanVsAcvment", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("date", "(From " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("txtPeronMasplan", ((Label)this.gvMMPlanVsAch.FooterRow.FindControl("lgvFperonmasplan")).Text));
            Rpt1.SetParameters(new ReportParameter("txtPeronMonplan", ((Label)this.gvMMPlanVsAch.FooterRow.FindControl("lgvFperonmonplan")).Text));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("comLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Budget Vs Execution - All Project"));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void gvMMPlanVsAch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc1");
            string mCOMCOD = comcod;
            string mPACTCODE = ((Label)e.Row.FindControl("lblgvpactcode")).Text;
            //string mPACTDESC = ((Label)e.Row.FindControl("Actdesc")).Text;
            string mTRNDAT1 = this.txtDatefrom.Text;
            string mTRNDAT2 = this.txtDateto.Text;
            //------------------------------//////
            //Label actcode = (Label)e.Row.FindControl("lblgvcode");
            //HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesc1");


            hlink1.NavigateUrl = "RptLinkImpExeStatus.aspx?Type=MPlnVsEx&pactcode=" + mPACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
        }
    }
}