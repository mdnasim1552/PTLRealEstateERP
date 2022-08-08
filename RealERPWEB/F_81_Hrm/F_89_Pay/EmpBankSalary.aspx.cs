using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using Microsoft.Reporting.WinForms;
using System.IO;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_81_Hrm.F_89_Pay
{
    public partial class EmpBankSalary : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                this.rbtBankSt.SelectedIndex = 0;
                this.GetMonth();
                this.GetBankName();
                this.GetBranch();
                this.GetSalTypeVisible();


                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Salary Summary";
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetBranch()
        {

            string comcod = this.GetComeCode();
            //if (this.ddlCompany.Items.Count == 0)
            //    return;


           // int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = "94" + "%";

            string txtSProject = "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETBRANCH", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlBranch.DataTextField = "actdesc";
            this.ddlBranch.DataValueField = "actcode";
            this.ddlBranch.DataSource = ds1.Tables[0];
            this.ddlBranch.DataBind();
           // this.ddlBranch_SelectedIndexChanged(null, null);

        }
        private void GetMonth()
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "4301":
                case "4305":
                    this.txtDate.Text = System.DateTime.Today.ToString("yyyyMM");
                    break;

                case "3333":
                case "3101":
                    this.lblasdate.Visible = true;
                    this.txtasdate.Visible = true;
                    this.txtDate.Text = System.DateTime.Today.AddMonths(-1).ToString("yyyyMM");
                    this.txtasdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    break;

                default:
                    this.txtDate.Text = System.DateTime.Today.AddMonths(-1).ToString("yyyyMM");
                    break;


            }


        }
        private void GetSalTypeVisible()
        {

            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "3101":
                case "3338":
                    this.rbtnlistsaltypeAddItem();
                    break;

                case "3355":
                    this.rbtnlistsaltypeAddItem();
                    break;

                case "3347":
                    this.rbtnlistsaltypeAddItem();
                    this.ChkAll.Visible = true;
                    this.lblcompany.Visible = true;
                    this.ddlCompany.Visible = true;
                    this.GetCompany();
                    break;
                default:
                    this.rbtnlistsaltype.Visible = true;
                    break;
            }


        }

        private void rbtnlistsaltypeAddItem()
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "3355":
                    this.rbtnlistsaltype.Visible = true;
                    this.rbtnlistsaltype.Items.Add(new ListItem("Management", "1"));
                    this.rbtnlistsaltype.Items.Add(new ListItem("Acting Management", "2"));
                    this.rbtnlistsaltype.Items.Add(new ListItem("General Employee", "3"));
                    this.rbtnlistsaltype.Items.Add(new ListItem("All", "4"));
                    this.rbtnlistsaltype.SelectedIndex = 3;
                    break;

                default:
                    this.rbtnlistsaltype.Visible = true;
                    this.rbtnlistsaltype.Items.Add(new ListItem("Management", "1"));
                    this.rbtnlistsaltype.Items.Add(new ListItem("Non Management", "2"));
                    this.rbtnlistsaltype.Items.Add(new ListItem("Both", "3"));
                    this.rbtnlistsaltype.SelectedIndex = 0;
                    break;
            }

        }



        private void GetCompany()
        {
            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string txtCompany = "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];


            ds1.Dispose();

        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        
        private void GetBankName()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETBANKNAME", "", "", "", "", "", "", "", "", "");
            this.ddlBankName.DataTextField = "actdesc";
            this.ddlBankName.DataValueField = "actcode";
            this.ddlBankName.DataSource = ds1.Tables[0];
            this.ddlBankName.DataBind();
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            switch (rbtBankSt.SelectedIndex)
            {
                case 0:
                    //this.txtDate.Text = System.DateTime.Today.ToString("yyyyMM");
                    //this.lbldate.Text = "Month Id:";
                    //this.txtDate_CalendarExtender.Format = "yyyyMM";
                    //this.txtDate.MaxLength = 6;
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.ShowBankPayment();
                    break;
                case 1:
                    //this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    break;

                case 2:
                    //this.PrintAccountTrans();
                    break;
            }


        }

        private string ComBankStatement()
        {

            string comcod = this.GetComeCode();

            string comBankStatement = "";
            switch (comcod)
            {
                //case "3101":
                case "3330":
                    comBankStatement = "EMPBANKPAYINFOBR";
                    break;

                //case "3101":
                case "3333":
                    comBankStatement = "EMPBANKPAYINFOALLI";
                    break;
                case "3347":
                    //case "3101":
                    comBankStatement = "EMPBANKPAYINFOPEB";
                    break;


                //case "3333":
                //    // case "3101":
                //    //this.txtDate.Text = System.DateTime.Today.ToString("yyyyMM");
                //    //this.lbldate.Text = "Month Id:";
                //    //this.txtDate_CalendarExtender.Format = "yyyyMM";
                //    //this.txtDate.MaxLength = 6;
                //    comBankStatement = "EMPBANKPAYINFOALLI";
                //    break;
                default:
                    comBankStatement = "EMPBANKPAYINFO";
                    //this.PrintAccountTrans();
                    break;
            }

            return comBankStatement;
        }



        private string ComBonusStatement()
        {

            string comcod = this.GetComeCode();

            string comBonumStatement = "";
            switch (comcod)
            {
                //case "3101":
                case "3330":
                    comBonumStatement = "EMPBONBANKPAYINFO";
                    break;

                case "3347":
                    comBonumStatement = "EMPBANKPAYINFOPEB02";
                    break;


                default:
                    comBonumStatement = "EMPBANKPAYINFOGEN";
                    //this.PrintAccountTrans();
                    break;
            }

            return comBonumStatement;




        }
        private string GetWithoutResign()
        {


            string comcod = this.GetComeCode();

            string withoutresign = "";
            switch (comcod)
            {
                
                case "3365": //BTI
                    withoutresign = "withoutresign";
                    break;

                

                default:
                    withoutresign = "";
                    //this.PrintAccountTrans();
                    break;
            }

            return withoutresign;

        }




        private void ShowBankPayment()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string bankname = this.ddlBankName.SelectedValue.ToString();
            string date = this.txtDate.Text.Trim();

            string all = (this.ChkAll.Checked) ? "All" : "";

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "BANKLOCK", date, bankname, "", "", "", "", "", "", "");
            this.lblBankLock.Text = (ds1.Tables[0].Rows.Count == 0) ? "False" : Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"]).ToString();

            string banklock = (this.lblBankLock.Text == "True") ? "Lock" : "";
            string CallType = (this.chkBonus.Checked) ? this.ComBonusStatement() : this.ComBankStatement();
            // string CallType = (this.chkBonus.Checked) ? "EMPBONBANKPAYINFO" : this.ComBankStatement();

            string todaysbs = (this.chklksalary.Checked) ? "todaybks" : "";
            string saldate = (this.txtasdate.Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : this.txtasdate.Text.Trim();

            string mantype = "";
            switch (comcod)
            {
                case "3338":
                    mantype = (this.rbtnlistsaltype.SelectedIndex == 0) ? "86001%" : (this.rbtnlistsaltype.SelectedIndex == 1) ? "86002%" : "86%";
                    break;

                case "3355":
                    mantype = (this.rbtnlistsaltype.SelectedIndex == 0) ? "86001%" : (this.rbtnlistsaltype.SelectedIndex == 1) ? "86002%" : (this.rbtnlistsaltype.SelectedIndex == 2) ? "86003%" : "86%";
                    break;

                default:
                    mantype = "86%";
                    break;
            }

            string company = comcod == "3347" ? ddlCompany.SelectedValue.ToString().Substring(0, 4) + "%" : "";
            string withoutresign = this.GetWithoutResign();
            string branch = this.ddlBranch.SelectedValue.ToString().Substring(0, 4)=="0000"? "%%" : this.ddlBranch.SelectedValue.ToString().Substring(0, 4) + "%";
            //DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", CallType, date, bankname, banklock, todaysbs, saldate, mantype, "", "", "");

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", CallType, date, bankname, banklock, todaysbs, saldate, mantype, all, company, withoutresign, branch);
            if (ds2 == null)
            {
                this.gvBankPayment.DataSource = null;
                this.gvBankPayment.DataBind();
                return;
            }
            DataTable dt = (ds2.Tables[0]);
            Session["tblover"] = dt;
            if (ds2.Tables.Count > 1)
            {
                Session["tblBankTrns"] = ds2.Tables[1];
            }
            this.Data_Bind();
        }
        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblover"];
            this.gvBankPayment.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvBankPayment.DataSource = dt;
            this.gvBankPayment.DataBind();

            if (dt.Rows.Count != 0)
            {
                ((CheckBox)this.gvBankPayment.FooterRow.FindControl("chkBankLock")).Checked = (this.lblBankLock.Text == "True") ? true : false;

                this.FooterCalculation();
                Session["Report1"] = gvBankPayment;
                ((HyperLink)this.gvBankPayment.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCELNEW";
                //  ((HyperLink)this.GvGrossRecon.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
            if (Request.QueryString["Type"].ToString() == "Entry")
            {
                if (dt.Rows.Count > 0)
                {
                    ((LinkButton)this.gvBankPayment.FooterRow.FindControl("lbtSalUpdate")).Visible = (((CheckBox)this.gvBankPayment.FooterRow.FindControl("chkBankLock")).Checked) ? false : true;
                    ((CheckBox)this.gvBankPayment.FooterRow.FindControl("chkBankLock")).Enabled = false;
                }
            }

        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblover"];
            if (dt.Rows.Count == 0)
                return;
            string comcod = this.GetComeCode();
            if (comcod == "3330" || comcod == "3101")
            {
                ((Label)this.gvBankPayment.FooterRow.FindControl("lgvFBamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00
                  : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
            }
            else
            {
                ((Label)this.gvBankPayment.FooterRow.FindControl("lgvFBamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00
                 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
            }

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            switch (rbtBankSt.SelectedIndex)
            {
                case 0:
                    this.PrintBankStatement();
                    break;
                case 1:
                    this.PrintForwardingLetter();
                    break;

                case 2:
                    this.PrintAccountTrans();
                    break;
            }
        }


        private void PrintBankStatement()
        {

            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "4301":
                    this.PrintBankStatementSan();
                    break;

                case "3333":
                    this.PrintBankStatementAlli();
                    break;

                //case "3101":
                case "3330":
                    this.PrintBankStatementBridge();
                    break;

                case "3347":
                    this.PrintBankStatementPEB();
                    break;

                case "3354":
                    this.PrintBankStatementEdison();
                    break;

                case "3101":
                case "3368":
                    this.PrintBankStatementFinlay();
                    break;

                default:
                    this.PrintrptBankStatement();
                    break;


            }
        }

        private void PrintBankStatementFinlay()
        {
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string bankname = this.ddlBankName.SelectedItem.Text.Trim();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string year = this.txtDate.Text.Substring(0, 4).ToString();
            string month = ASITUtility03.GetFullMonthName(this.txtDate.Text.Substring(4));
            string printtype = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("saltrn='True'");
            dt = dv.ToTable();

            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>();

            LocalReport Rpt1 = new LocalReport();

            if (printtype == "EXCEL")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBankStatementFinlayExcel", lst, null, null);
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBankStatementFinlay", lst, null, null);
            }
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comname));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Bank Forwarding Report For " + bankname ));
            Rpt1.SetParameters(new ReportParameter("txtMonth", month));
            Rpt1.SetParameters(new ReportParameter("txtYear", year));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintBankStatementEdison()
        {
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string bankname = this.ddlBankName.SelectedItem.Text.Trim();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string year = this.txtDate.Text.Substring(0, 4).ToString();
            string month = ASITUtility03.GetFullMonthName(this.txtDate.Text.Substring(4));

            DataView dv = dt.DefaultView;
            dv.RowFilter = ("saltrn='True'");
            dt = dv.ToTable();
            double TAmount = 0.00;
            TAmount = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", "")));

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptBankStatementEdison", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", (this.chkBonus.Checked) ? "Festival Bonus Transfer Statement  " : "Salary Transfer Statement"));
            Rpt1.SetParameters(new ReportParameter("date", "For " + month + ", " + year));
            Rpt1.SetParameters(new ReportParameter("InWrd", "In Word : " + ASTUtility.Trans(Math.Round(TAmount), 2)));
            Rpt1.SetParameters(new ReportParameter("rptBankName", bankname));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintBankStatementAlli()
        {
            DataTable dt = ((DataTable)Session["tblover"]).Copy(); ;
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("saltrn='True'");
            dt = dv.ToTable();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string bankname = this.ddlBankName.SelectedItem.Text.Trim();


            double sumamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00
                    : dt.Compute("sum(amt)", "")));
            string inwords = ASTUtility.Trans(sumamt, 2);


            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string year = this.txtDate.Text.Substring(0, 4).ToString();
            string month = ASITUtility03.GetFullMonthName(this.txtDate.Text.Substring(4));
            //

            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptBankStatementAlli", lst, null, null);

            Rpt1.SetParameters(new ReportParameter("rptTitle", (this.chkBonus.Checked) ? "Festival Bonus Transfer Statement  " : "Salary Transfer Statement"));
            Rpt1.SetParameters(new ReportParameter("date", "For " + month + ", " + year));
            Rpt1.SetParameters(new ReportParameter("rptBankName", bankname));
            Rpt1.SetParameters(new ReportParameter("InWord", "Taka Inword: " + inwords));

            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintBankStatementSan()
        {

            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string bankname = this.ddlBankName.SelectedItem.Text.Trim();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string year = this.txtDate.Text.Substring(0, 4).ToString();
            string month = ASITUtility03.GetFullMonthName(this.txtDate.Text.Substring(4));


            ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.rptBankStatementSan();
            TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = "For " + month + ", " + year;
            TextObject BankName = rpcp.ReportDefinition.ReportObjects["bankname"] as TextObject;
            BankName.Text = bankname;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rpcp.SetDataSource(dt);
            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintBankStatementPEB()
        {
            DataTable dt = ((DataTable)Session["tblover"]).Copy(); ;
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = ("saltrn='True'");
            //dt = dv.ToTable();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string bankname = this.ddlBankName.SelectedItem.Text.Trim();


            double sumamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00
                    : dt.Compute("sum(amt)", "")));
            string inwords = ASTUtility.Trans(sumamt, 2);


            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string year = this.txtDate.Text.Substring(0, 4).ToString();
            string month = ASITUtility03.GetFullMonthName(this.txtDate.Text.Substring(4));

            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptBankStatementPEB", lst, null, null);

            Rpt1.SetParameters(new ReportParameter("rptTitle", (this.chkBonus.Checked) ? "Festival Bonus Transfer Statement  " : "Salary Transfer Statement"));
            Rpt1.SetParameters(new ReportParameter("date", "For " + month + ", " + year));
            Rpt1.SetParameters(new ReportParameter("rptBankName", bankname));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintrptBankStatement()
        {
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string bankname = this.ddlBankName.SelectedItem.Text.Trim();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string year = this.txtDate.Text.Substring(0, 4).ToString();
            string month = ASITUtility03.GetFullMonthName(this.txtDate.Text.Substring(4));

            DataView dv = dt.DefaultView;
            dv.RowFilter = ("saltrn='True'");
            dt = dv.ToTable();


            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>();

            if (comcod == "3355")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptBankStatementGreenwood", lst, null, null);

            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptBankStatement", lst, null, null);

            }


            Rpt1.SetParameters(new ReportParameter("rptTitle", (this.chkBonus.Checked) ? "Festival Bonus Transfer Statement  " : "Salary Transfer Statement"));
            Rpt1.SetParameters(new ReportParameter("date", "For " + month + ", " + year));
            Rpt1.SetParameters(new ReportParameter("rptBankName", bankname));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintBankStatementBridge()
        {
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string bankname = this.ddlBankName.SelectedItem.Text.Trim();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string year = this.txtDate.Text.Substring(0, 4).ToString();
            string month = ASITUtility03.GetFullMonthName(this.txtDate.Text.Substring(4));

            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptBankStatementBridge", lst, null, null);

            Rpt1.SetParameters(new ReportParameter("rptTitle", (this.chkBonus.Checked) ? "Festival Bonus Transfer Statement  " : "Salary Transfer Statement"));
            Rpt1.SetParameters(new ReportParameter("date", "For " + month + ", " + year));
            Rpt1.SetParameters(new ReportParameter("rptBankName", bankname));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintForwardingLetter()
        {


            string comcod = this.GetComeCode();
            switch (comcod)
            {


                case "3333":

                    this.PrintForwardingLetterAlli();
                    break;
                //case "3101":
                case "3344":
                    this.PrintForwardingLetterTerra();
                    break;

                //case "3101":
                case "3355":
                    this.PrintForwardingLetterGreen();
                    break;

                case "3354": //Edison
                    this.PrintForwardingLetterEdison();
                    break;

                default:
                    this.PrintForwardingLettergen();
                    break;


            }


        }

        private void PrintForwardingLetterEdison()
        {
            DataTable dt = (DataTable)Session["tblover"];
            DataTable dt1 = (DataTable)Session["tblBankTrns"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtcuDate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string year = this.txtDate.Text.Substring(0, 4).ToString();
            string month = ASITUtility03.GetFullMonthName(this.txtDate.Text.Substring(4));

            string motheraccno = dt.Rows[0]["banksl"].ToString();
            string addr = dt.Rows[0]["bankaddr"].ToString();
            string bankname = dt.Rows[0]["bankname"].ToString();
            // string bankAccNo = dt.Rows[0]["acno"].ToString();
            //string bankAccNo = this.ddlBankName.SelectedValue.ToString();
            string totNoTrans = dt1.Rows[0]["ttrnsecno"].ToString();

            string[] add = addr.Split(',');

            string Badd = "";

            foreach (string add1 in add)
                Badd = Badd + add1 + "," + "\n";
            Badd = Badd.Substring(1, Badd.Length - 1);


            string sumamt = "";
            sumamt = ((Label)this.gvBankPayment.FooterRow.FindControl("lgvFBamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00
            : dt.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            string inwords = ASTUtility.Trans(Convert.ToDouble(sumamt), 2);
            string subject = "";
            subject = "Subject: Request for Disbursement of Salary- " + month + "-" + year + " as Per Attached Sheet.";

            string Det1 = "";
            Det1 = "I/We hereby request you to take the necessary initiatives to proceed with bulk salary transfer. The transfer detail is attached herewith duly signed by the signatory.";

            string Det2 = "";
            Det2 = "I/We take the responsibility for the attached sheet for its detailed information given to the bank, which is fair and free from any anti-money laundering issue.";

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptForLetterEdison", list, null, null);
            Rpt1.SetParameters(new ReportParameter("BankAdd", addr));
            Rpt1.SetParameters(new ReportParameter("Date", Convert.ToDateTime(txtcuDate).ToString("MMMM dd, yyyy")));
            Rpt1.SetParameters(new ReportParameter("Attn", "Assistant Vice President"));
            Rpt1.SetParameters(new ReportParameter("Bank", bankname));
            Rpt1.SetParameters(new ReportParameter("subject", subject));
            Rpt1.SetParameters(new ReportParameter("Det1", Det1));
            Rpt1.SetParameters(new ReportParameter("Det2", Det2));
            Rpt1.SetParameters(new ReportParameter("totalAmt", sumamt));
            Rpt1.SetParameters(new ReportParameter("InWrd", inwords));
            Rpt1.SetParameters(new ReportParameter("totNoTrans", totNoTrans));
            Rpt1.SetParameters(new ReportParameter("bankAccNo", motheraccno));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintForwardingLetterTerra()
        {
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            //string bankname = this.ddlBankName.SelectedItem.Text.Trim();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtcuDate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string year = this.txtDate.Text.Substring(0, 4).ToString();
            string month = ASITUtility03.GetFullMonthName(this.txtDate.Text.Substring(4));



            string banksl = dt.Rows[0]["banksl"].ToString();
            string acno = dt.Rows[0]["acno"].ToString();

            string addr = dt.Rows[0]["bankaddr"].ToString();
            string bankname = dt.Rows[0]["bankname"].ToString();

            string[] add = addr.Split('^');
            string Badd = "";
            foreach (string add1 in add)
                Badd = Badd + add1 + "\n";
            Badd = Badd.Substring(0, Badd.Length - 1);


            string sumamt = ((Label)this.gvBankPayment.FooterRow.FindControl("lgvFBamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00
                    : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
            string inwords = ASTUtility.Trans(Convert.ToDouble(sumamt), 2);

            string Det2 = "";
            if (comcod == "4315")
            {

                Det2 = "We would like to request you to transfer the amount to the respective accounts of our employees (details list attached) by debiting our Account Title ASSURE DEVELOPMENT & DESIGN LTD C/D account no. "
                          + comname + " " + banksl + " as per bellow details: ";
            }



            else if (comcod == "3344")
            {
                Det2 = "We would like to request you to transfer the amount to the respective accounts of our employees (details list attached) by debiting our C/D account name"
                           + " Terranova Developments Ltd.Ac Number " + banksl + " as per bellow details: ";
            }

            else
            {

                Det2 = "We would like to request you to transfer the amount to the respective accounts of our employees (details list attached) by debiting our C/D account no. "
                            + banksl + " as per bellow details: ";
            }


            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptForLetterTerra", null, null, null);

            Rpt1.SetParameters(new ReportParameter("Date", Convert.ToDateTime(txtcuDate).ToString("MMMM dd, yyyy")));
            Rpt1.SetParameters(new ReportParameter("Attn", "Attn: "));
            Rpt1.SetParameters(new ReportParameter("Bank", bankname));
            Rpt1.SetParameters(new ReportParameter("BankAdd", addr));
            Rpt1.SetParameters(new ReportParameter("subject", ((this.chkBonus.Checked) ? "Subject: Festival Bonus " : "Subject: Salary ") + "Payment advise for the Month of " + month + " " + year + "."));
            Rpt1.SetParameters(new ReportParameter("Det1", (this.chkBonus.Checked) ? "We forwarded herewith a payment instruction for processing of festival bonus in favor of our employees maintaining accounts with your bank." : "We forwarded herewith a payment instruction for processing of salary in favor of our employees maintaining accounts with your bank"));
            Rpt1.SetParameters(new ReportParameter("Det2", Det2));
            Rpt1.SetParameters(new ReportParameter("purpose", " : " + ((this.chkBonus.Checked) ? "Festival Bonus " : "Salary ")));
            Rpt1.SetParameters(new ReportParameter("vdate", " : " + Convert.ToDateTime(txtcuDate).ToString("MMMM dd, yyyy")));
            Rpt1.SetParameters(new ReportParameter("Det3", " : BDT " + sumamt));
            Rpt1.SetParameters(new ReportParameter("inwords", " : " + inwords));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintForwardingLettergen()
        {
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            //string bankname = this.ddlBankName.SelectedItem.Text.Trim();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtcuDate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string year = this.txtDate.Text.Substring(0, 4).ToString();
            string month = ASITUtility03.GetFullMonthName(this.txtDate.Text.Substring(4));



            string banksl = dt.Rows[0]["banksl"].ToString();
            string addr = dt.Rows[0]["bankaddr"].ToString();
            string bankname = dt.Rows[0]["bankname"].ToString();

            // string[] add = addr.Split('^');
            string[] add = addr.Split(',');

            string Badd = "";

            foreach (string add1 in add)
                Badd = Badd + add1 + "," + "\n";
            Badd = Badd.Substring(1, Badd.Length - 1);


            string sumamt = "";
            if (comcod == "3330")
            {
                sumamt = ((Label)this.gvBankPayment.FooterRow.FindControl("lgvFBamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00
            : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
            }
            else
            {
                sumamt = ((Label)this.gvBankPayment.FooterRow.FindControl("lgvFBamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00
            : dt.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            }


            string inwords = ASTUtility.Trans(Convert.ToDouble(sumamt), 2);
            string subject = "";
            if (comcod == "3330")
            {
                subject = ((this.chkBonus.Checked) ? "Subject: Festival Bonus " : "Subject: ") + "Payment advice " + ".";
            }

            else
            {
                subject = ((this.chkBonus.Checked) ? "Subject: Festival Bonus " : "Subject: Salary ") + "Payment advise for the Month of " + month + " " + year + ".";

            }
            string Det1 = "";
            if (this.chkBonus.Checked)
            {
                Det1 = "Subject: Festival Bonus";


            }


            else if (comcod == "3330")
            {
                Det1 = "We forwarded herewith a payment advice to make the salary transfer from our CD Account no. " + banksl + "  to our employee account numbers as per the attached statement.";


            }


            else
            {
                Det1 = "We forwarded herewith a payment instruction for processing of salary in favor of our employees maintaining accounts with your bank";
            }
            string Det2 = "";
            if (comcod == "4315")
            {

                Det2 = "We would like to request you to transfer the amount to the respective accounts of our employees (details list attached) by debiting our Account Title ASSURE DEVELOPMENT & DESIGN LTD C/D account no. "
                          + comname + " " + banksl + " as per bellow details: ";
            }

            else if (comcod == "3330")
            {
                Det2 = "We would like to request you to transfer the particular amount to the respective employee accounts of our company accordingly.";
            }

            else
            {

                Det2 = "We would like to request you to transfer the amount to the respective accounts of our employees (details list attached) by debiting our C/D account no. "
                            + banksl + " as per bellow details: ";
            }


            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();
            if (comcod == "3330")
            {
                string[] add2 = addr.Split(',');
                string ad0 = add2[0];
                string ad1 = add2[1];
                string ad2 = add2[2];
                string ad3 = add2[3];

                var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.bnkStatement>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptForLetterBridge", lst, null, null);
                Rpt1.SetParameters(new ReportParameter("BankAdd", ad0 + "," + ad1 + "," + ad2));
                Rpt1.SetParameters(new ReportParameter("BankAdd2", ad3));

            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptForLetter", null, null, null);
                Rpt1.SetParameters(new ReportParameter("BankAdd", addr));
            }


            Rpt1.SetParameters(new ReportParameter("Date", Convert.ToDateTime(txtcuDate).ToString("MMMM dd, yyyy")));
            Rpt1.SetParameters(new ReportParameter("Attn", "Attn: "));
            Rpt1.SetParameters(new ReportParameter("Bank", bankname));

            Rpt1.SetParameters(new ReportParameter("subject", subject));
            Rpt1.SetParameters(new ReportParameter("Det1", Det1));
            Rpt1.SetParameters(new ReportParameter("Det2", Det2));
            Rpt1.SetParameters(new ReportParameter("Det3", "Total Amount: BDT " + sumamt));
            Rpt1.SetParameters(new ReportParameter("inwords", "Amount in Words: " + inwords));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintForwardingLetterGreen()
        {
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtcuDate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string year = this.txtDate.Text.Substring(0, 4).ToString();
            string month = ASITUtility03.GetFullMonthName(this.txtDate.Text.Substring(4));


            if (dt == null || dt.Rows.Count == 0)
                return;

            string banksl = dt.Rows[0]["banksl"].ToString();
            string addr = dt.Rows[0]["bankaddr"].ToString();
            string bankname = dt.Rows[0]["bankname"].ToString();

            // string[] add = addr.Split('^');
            string[] add = addr.Split(',');

            string Badd = "";
            foreach (string add1 in add)
                Badd = Badd + add1 + "," + "\n";
            Badd = Badd.Substring(1, Badd.Length - 1);


            // todo for greenwood
            string bankacc = this.ddlBankName.SelectedValue.ToString();
            string bankacc2 = this.ddlBankName.SelectedItem.ToString();

            string[] add2 = bankacc2.Split('#');
            string bank2 = add2[1];
            string bank1 = add2[0];

            string empType = "";
            string Det1 = "";
            string Det2 = "";
            string Det3 = "";
            string concern = "Dear Sir,";
            string branch = "";
            string attn = "";
            string bank = "";
            string address = "";

            string mantype = (this.rbtnlistsaltype.SelectedIndex == 0) || (this.rbtnlistsaltype.SelectedIndex == 1) ? "Management" : (this.rbtnlistsaltype.SelectedIndex == 2) ? "General" : "All";

            if (mantype == "General")
            {
                empType = "Employees ";
                Det1 = "We are maintaining an SND account with your branch (AC/ #" + bank2 + " ) of our company and all of our employees are also maintaining their respective accounts with your branch. Now we like to pay off their salaries through their respective bank accounts.";
                Det2 = "Such, we would request you to debit our (AC/ #" + bank2 + " ) and transfer the same amount to the respective employee’s personal accounts as per attached list.";
                attn = "The Assistant Vice President & Manager";
                //bank = "Shahajalal Islami Bank Ltd.";
                //branch = "Mirpur Branch";
                //address = "Dhaka";
                bank = bank1;
                branch = add.Length > 0 ? add[0].ToString(): "";
                var list = new List<string>(add);                
                list.RemoveAt(0);
                foreach (var item in list)
                {
                    address += item + ", ";
                }


            }

            else if (mantype == "Management")
            {
                empType = "Managements ";
                Det1 = "We are maintaining a current account with your branch (AC/ #" + bank2 + " ) of our company and some of our managements are also maintaining their respective accounts with your branch. Now we like to pay off their salaries through their respective bank accounts.";
                Det2 = "As such, we would request you to debit our (AC/ #" + bank2 + " ) and transfer the same amount to the respective management’s personal accounts as per attached list.";
                attn = "The Manager";
                //bank = "Trust Bank Ltd.	";
                //branch = "Dilkhusha Corporate Branch";
                //address = "Dhaka";
                bank = bank1;
                branch = add.Length > 0 ? add[0].ToString() : "";
                var list = new List<string>(add);
                list.RemoveAt(0);
                foreach (var item in list)
                {
                    address += item + ", ";
                }

            }
            else
            {
                empType = "Employees ";
                Det1 = "We are maintaining an SND account with your branch (AC/ #" + bank2 + " ) of our company and all of our employees are also maintaining their respective accounts with your branch. Now we like to pay off their salaries through their respective bank accounts.";
                Det2 = "Such, we would request you to debit our (AC/ #" + bank2 + " ) and transfer the same amount to the respective employee’s personal accounts as per attached list.";
                attn = "The Assistant Vice President & Manager";
                //bank = bankacc;
                //branch = "Mirpur Branch";
                //address = addr;
                bank = bank1;
                branch = add.Length > 0 ? add[0].ToString() : "";
                var list = new List<string>(add);
                list.RemoveAt(0);
                foreach (var item in list)
                {
                    address += item + ", ";
                }
            }

            string sumamt = "";
            if (dt.Rows.Count != 0)
            {
                sumamt = ((Label)this.gvBankPayment.FooterRow.FindControl("lgvFBamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00
                    : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
            }

            string inwords = ASTUtility.Trans(Convert.ToDouble(sumamt), 2);
            string subject = "Subject: " + "Transfer of  fund  for " + empType + ((this.chkBonus.Checked) ? " Festival Bonus " : " Salary ") + "for the Month of " + month + " " + year + ".";
            if (this.chkBonus.Checked)
            {
                Det1 = "Subject: Festival Bonus";
            }

            Det3 = "Therefore, we would request you to debit/transfer BDT " + sumamt + " " + inwords + " to respective accounts from our aforesaid account.";


            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptForLetterGreenwood", null, null, null);

            Rpt1.SetParameters(new ReportParameter("Date", Convert.ToDateTime(txtcuDate).ToString("MMMM dd, yyyy")));
            Rpt1.SetParameters(new ReportParameter("Attn", attn));
            Rpt1.SetParameters(new ReportParameter("Bank", bank));
            Rpt1.SetParameters(new ReportParameter("branch", branch));
            Rpt1.SetParameters(new ReportParameter("BankAdd", address));
            Rpt1.SetParameters(new ReportParameter("subject", subject));
            Rpt1.SetParameters(new ReportParameter("concern", concern));
            Rpt1.SetParameters(new ReportParameter("salam", "Assalamu Alaikum Wa Rahmatuallah."));
            Rpt1.SetParameters(new ReportParameter("Det1", Det1));
            Rpt1.SetParameters(new ReportParameter("Det2", Det2));
            Rpt1.SetParameters(new ReportParameter("Det3", Det3));
            Rpt1.SetParameters(new ReportParameter("txtName", "Muhammad Ibrahim Shami"));
            Rpt1.SetParameters(new ReportParameter("txtDesig", "Managing Director"));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }




        private void PrintForwardingLetterAlli()
        {
            DataTable dt = ((DataTable)Session["tblover"]).Copy(); ;
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("saltrn='True'");
            dt = dv.ToTable();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            //string bankname = this.ddlBankName.SelectedItem.Text.Trim();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtcuDate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string year = this.txtDate.Text.Substring(0, 4).ToString();
            string month = ASITUtility03.GetFullMonthName(this.txtDate.Text.Substring(4));



            string banksl = dt.Rows[0]["banksl"].ToString();
            string addr = dt.Rows[0]["bankaddr"].ToString();
            string bankname = dt.Rows[0]["bankname"].ToString();

            string[] add = addr.Split('^');
            string Badd = "";
            foreach (string add1 in add)
                Badd = Badd + add1 + "\n";
            Badd = Badd.Substring(0, Badd.Length - 1);


            string sumamt = ((Label)this.gvBankPayment.FooterRow.FindControl("lgvFBamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00
                    : dt.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            string inwords = ASTUtility.Trans(Convert.ToDouble(sumamt), 2);


            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptForLetter", null, null, null);

            Rpt1.SetParameters(new ReportParameter("Date", Convert.ToDateTime(txtcuDate).ToString("MMMM dd, yyyy")));
            Rpt1.SetParameters(new ReportParameter("Attn", "Attn: "));
            Rpt1.SetParameters(new ReportParameter("Bank", bankname));
            Rpt1.SetParameters(new ReportParameter("BankAdd", addr));
            Rpt1.SetParameters(new ReportParameter("subject", ((this.chkBonus.Checked) ? "Subject: Festival Bonus " : "Subject: Salary ") + "Payment advise for the Month of " + month + " " + year + "."));
            Rpt1.SetParameters(new ReportParameter("Det1", (this.chkBonus.Checked) ? "We forwarded herewith a payment instruction for processing of festival bonus in favor of our employees maintaining accounts with your bank." : "We forwarded herewith a payment instruction for processing of salary in favor of our employees maintaining accounts with your bank"));
            Rpt1.SetParameters(new ReportParameter("Det2", "We would like to request you to transfer the amount to the respective accounts of our employees (details list attached) by debiting our C/D account no. "
                            + banksl + " as per bellow details: "));
            Rpt1.SetParameters(new ReportParameter("Det3", "Total Amount: BDT " + sumamt));
            Rpt1.SetParameters(new ReportParameter("inwords", "Amount in Words: " + inwords));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintAccountTrans()
        {


            DataTable dt = ((DataTable)Session["tblover"]).Copy(); ;
            DataView dv = dt.DefaultView;
            string comcod = this.GetComeCode();
            switch (comcod)
            {


                case "3333":
                    //case "3101":
                    dv.RowFilter = ("saltrn='True'");
                    break;
                default:
                    this.PrintForwardingLettergen();
                    break;


            }
            dt = dv.ToTable();


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string bankname = this.ddlBankName.SelectedItem.Text.Trim();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtcuDate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string banksl = dt.Rows[0]["banksl"].ToString();
            string addr = dt.Rows[0]["bankaddr"].ToString();

            string sumamt = ((Label)this.gvBankPayment.FooterRow.FindControl("lgvFBamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00
                    : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
            string inwords = ASTUtility.Trans(Convert.ToDouble(sumamt), 2);

            string Det1 = "";
            if (comcod == "4315")
            {

                Det1 = "Please refer to our previous discussion and we would like to transfer an amount of BDT " + sumamt + " " + inwords + " from ASSURE DEVELOPMENT & DESIGN LTD CD A/C # " + banksl + " to " + "      " +
                               " Nos. of salary A/C as per attached sheet on " + Convert.ToDateTime(txtcuDate).ToString("MMMM dd, yyyy") + ".";

            }
            else
            {

                Det1 = "Please refer to our previous discussion and we would like to transfer an amount of BDT " + sumamt + " " + inwords + " from our CD A/C # " + banksl + " to " +
                               " Nos. of salary A/C as per attached sheet on " + Convert.ToDateTime(txtcuDate).ToString("MMMM dd, yyyy") + ".";


            }


            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptAccTransfer", null, null, null);

            Rpt1.SetParameters(new ReportParameter("Date", "Date: " + Convert.ToDateTime(txtcuDate).ToString("MMMM dd, yyyy")));
            Rpt1.SetParameters(new ReportParameter("Bank", bankname));
            Rpt1.SetParameters(new ReportParameter("BankAdd", addr));
            Rpt1.SetParameters(new ReportParameter("Det1", Det1));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        protected void gvBankPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvBankPayment.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblover"];
            int rowindex;

            for (int i = 0; i < this.gvBankPayment.Rows.Count; i++)
            {
                string empid = ((Label)this.gvBankPayment.Rows[i].FindControl("lgempid")).Text.Trim();
                string acno = ((Label)this.gvBankPayment.Rows[i].FindControl("lgvBACNo")).Text.Trim();
                double amount = Convert.ToDouble("0" + ((Label)this.gvBankPayment.Rows[i].FindControl("lblgvAmt")).Text.Trim());
                string saltrn = ((CheckBox)this.gvBankPayment.Rows[i].FindControl("chksaltrns")).Checked ? "True" : "False";

                rowindex = (this.gvBankPayment.PageSize) * (this.gvBankPayment.PageIndex) + i;

                dt.Rows[rowindex]["empid"] = empid;
                dt.Rows[rowindex]["acno"] = acno;
                dt.Rows[rowindex]["amt"] = amount;
                dt.Rows[rowindex]["saltrn"] = saltrn;


            }

            Session["tblover"] = dt;


        }
        protected void lbtSalUpdate_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.SaveValue();
            string bankcode = this.ddlBankName.SelectedValue.ToString();
            string monthid = this.txtDate.Text;
            DataTable dt1 = (DataTable)Session["tblover"];
            string saldate = (this.txtasdate.Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : this.txtasdate.Text.Trim();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string empid = dt1.Rows[i]["empid"].ToString();
                string acno = dt1.Rows[i]["acno"].ToString();
                double amount = Convert.ToDouble(dt1.Rows[i]["amt"].ToString());
                string saltrn = dt1.Rows[i]["saltrn"].ToString();

                //if (amount > 0)
                //{
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "BANKTRANSFER", monthid, bankcode, empid, acno,
                                       amount.ToString(), saltrn, saldate, "", "", "", "", "", "", "", "");

                if (result == true)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                }

                //}


            }


            string Banklock = (((CheckBox)this.gvBankPayment.FooterRow.FindControl("chkBankLock")).Checked) ? "1" : "0";
            bool result1 = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "INORUPBANKLOCK", monthid, bankcode, Banklock, "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result1)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }
        protected void chkAllfrm_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblover"];
            int i, index;
            if (((CheckBox)this.gvBankPayment.HeaderRow.FindControl("chkAllfrm")).Checked)
            {

                for (i = 0; i < gvBankPayment.Rows.Count; i++)
                {
                    ((CheckBox)this.gvBankPayment.Rows[i].FindControl("chksaltrns")).Checked = true;
                    index = (this.gvBankPayment.PageSize) * (this.gvBankPayment.PageIndex) + i;
                    dt.Rows[index]["saltrn"] = "True";


                }


            }



            else
            {
                for (i = 0; i < gvBankPayment.Rows.Count; i++)
                {
                    ((CheckBox)this.gvBankPayment.Rows[i].FindControl("chksaltrns")).Checked = false;
                    index = (this.gvBankPayment.PageSize) * (this.gvBankPayment.PageIndex) + i;
                    dt.Rows[index]["saltrn"] = "False";

                }

            }

            Session["tblover"] = dt;
        }

        protected void gvBankPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string comcod = this.GetComeCode();
            if (comcod == "3330")
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ((Label)e.Row.FindControl("lblgvAmt")).Text = Convert.ToDouble(((Label)e.Row.FindControl("lblgvAmt")).Text).ToString("#,##0;(#,##0); ");
                }
            }
        }
    }
}