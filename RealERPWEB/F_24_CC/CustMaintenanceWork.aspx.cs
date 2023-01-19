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
using RealERPLIB;
using RealERPRDLC;
namespace RealERPWEB.F_24_CC
{
    public partial class CustMaintenanceWork : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        public static bool result;
        string msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetProjectName();
                this.GetUnitName();
                this.txtCurTransDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetType();
                this.ibtnType_Click(null, null);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

                string type = this.Request.QueryString["Type"];
                if (type == "Check" || type == "Audit" || type == "Approv" || type == "FirstApproval" || type == "SecondApproval")
                {
                    PreviousAddNumber();
                    lbtnOk_Click(null, null);
                }
                else if (type == "ReqPrint")
                {
                    PrintAdditionalWorkOrder();
                }

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
            return (hst["comcod"].ToString());
        }

        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string txtSProject = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPNAMEMAINTENANCE", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }

        private void GetUnitName()
        {

            Session.Remove("tblunit");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSUnit = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETUNITNAME", pactcode, txtSUnit, "", "", "", "", "", "", "");
            this.ddlUnitName.DataTextField = "udesc1";
            this.ddlUnitName.DataValueField = "usircode";
            this.ddlUnitName.DataSource = ds1.Tables[0];
            this.ddlUnitName.DataBind();
            Session["tblunit"] = ds1.Tables[0];

        }

        private void GetInsType()
        {
            string comcod = this.GetCompCode();
            string txtSProject = "%";
            string type = this.Request.QueryString["Type"];
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETINSTYPE", txtSProject, type, "", "", "", "", "", "", "");
            this.ddlType.DataTextField = "gdesc";
            this.ddlType.DataValueField = "gcod";
            this.ddlType.DataSource = ds1.Tables[0];
            this.ddlType.DataBind();


        }

        protected void ibtnType_Click(object sender, EventArgs e)
        {
            this.GetInsType();
        }


        private void GetItemName()
        {
            ViewState.Remove("tblwrk");
            string comcod = this.GetCompCode();
            string code = this.ddlType.SelectedValue.ToString().Substring(0, 2);
            string txtsrchitem = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETITEMNAME", code, txtsrchitem, "", "", "", "", "", "", "");
            this.ddlItemName.DataTextField = "gdesc";
            this.ddlItemName.DataValueField = "gcod";
            this.ddlItemName.DataSource = ds1.Tables[0];
            this.ddlItemName.DataBind();
            ViewState["tblwrk"] = ds1.Tables[0];
            ds1.Dispose();
        }

        private void GetInstallment()
        {
            string comcod = this.GetCompCode();
            string projectCode = this.ddlProjectName.SelectedValue.ToString();
            string custCode = this.ddlUnitName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETINSMNTPRJCUSTWISE", projectCode, custCode, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlInstallment.DataTextField = "delschdesc";
            this.ddlInstallment.DataValueField = "delschcode";
            this.ddlInstallment.DataSource = ds1.Tables[0];
            this.ddlInstallment.DataBind();
            ds1.Dispose();
        }

        private void ShowInterest()
        {


            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string custid = this.ddlUnitName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtCurTransDate.Text).ToString("dd-MMM-yyyy");
            //  string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            // string frmdate = "01-" + ASTUtility.Right(date, 8);
            string todate = Convert.ToDateTime(this.txtCurTransDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string permonth = "";//this.txtinpermonth.Text.Trim().Replace("%", "");

            string ddlintslist = this.ddlInstallment.SelectedValue.ToString();


            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_DELAYCHARGE", "RPTINTEREST_SALESINCREASE", pactcode, custid, frmdate, todate, permonth, ddlintslist, "", "", "");
            if (ds2 == null)
            {
                return;
            }

            ViewState["tblDelayCharge"] = ds2.Tables[0];

        }


        private void PreviousAddNumber()
        {
            string comcod = this.GetCompCode();

            string qdate = this.Request.QueryString["Date1"] ?? "";
            string curdate = qdate.Length > 0 ? qdate : Convert.ToDateTime(this.txtCurTransDate.Text).ToString("dd-MMM-yyyy");
            string qgenno = this.Request.QueryString["genno"] ?? "";
            string adno = qgenno.Length > 0 ? ("%" + qgenno + "%") : "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPREADWORK", curdate, adno, "", "", "", "", "", "", "");
            this.ddlPrevADNumber.DataTextField = "adno1";
            this.ddlPrevADNumber.DataValueField = "adno";
            this.ddlPrevADNumber.DataSource = ds1.Tables[0];
            this.ddlPrevADNumber.DataBind();
            ds1.Dispose();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
                return;
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ddlUnitName_SelectedIndexChanged(null, null);
                this.ddlProjectName.Enabled = false;
                this.ddlUnitName.Enabled = false;
                this.lblUnitName.Visible = true;
                this.ibtnPreAdNo.Visible = false;
                this.ddlPrevADNumber.Visible = false;
                this.PanelItem.Visible = true;
                this.ddlType.Enabled = false;
                this.PnlNarration.Visible = true;
                if (comcod == "3354" || comcod == "3101")
                {
                    this.lblInsmnt.Visible = true;
                    this.ddlInstallment.Visible = true;
                }
                this.GetItemName();
                this.GetInstallment();
                this.ShowAdWork();
                this.ColumnVisible();

                return;

            }

            this.lbtnOk.Text = "Ok";
            this.ddlProjectName.Visible = true;
            this.ddlUnitName.Visible = true;
            this.ddlPrevADNumber.Items.Clear();
            this.ibtnPreAdNo.Visible = true;
            this.ddlPrevADNumber.Visible = true;
            this.PanelItem.Visible = false;
            this.gvAddWork.DataSource = null;
            this.gvAddWork.DataBind();
            this.ddlItemName.Items.Clear();
            this.ddlType.Enabled = true;
            this.ddlProjectName.Enabled = true;
            this.ddlUnitName.Enabled = true;
            this.PnlNarration.Visible = false;
            this.lblSchCode.Text = "";

        }
        private void ColumnVisible()
        {
            string code = this.ddlType.SelectedValue.ToString();

            switch (code)
            {
                case "130000000":
                case "140000000":
                case "150000000":
                case "160000000":
                case "170000000":
                case "180000000":
                case "190000000":
                case "200000000":
                case "210000000":
                case "230000000":
                case "240000000":
                case "260000000":
                case "290000000":
                case "300000000":
                    this.gvAddWork.Columns[5].Visible = false;
                    this.gvAddWork.Columns[6].Visible = false;
                    this.gvAddWork.Columns[7].Visible = false;
                    this.gvAddWork.Columns[8].Visible = false;
                    this.gvAddWork.Columns[9].Visible = false;
                    this.gvAddWork.Columns[10].Visible = false;
                    this.gvAddWork.Columns[11].Visible = false;
                    this.gvAddWork.Columns[12].Visible = false;
                    this.gvAddWork.Columns[13].Visible = false;
                    this.gvAddWork.Columns[14].Visible = false;
                    this.gvAddWork.Columns[15].Visible = false;
                    this.gvAddWork.Columns[16].Visible = false;
                    this.gvAddWork.Columns[17].Visible = false;
                    break;


                default:
                    this.gvAddWork.Columns[5].Visible = true;
                    this.gvAddWork.Columns[6].Visible = true;
                    this.gvAddWork.Columns[7].Visible = true;
                    this.gvAddWork.Columns[8].Visible = true;
                    this.gvAddWork.Columns[9].Visible = true;
                    this.gvAddWork.Columns[10].Visible = true;
                    this.gvAddWork.Columns[11].Visible = true;
                    this.gvAddWork.Columns[12].Visible = true;
                    this.gvAddWork.Columns[13].Visible = true;
                    this.gvAddWork.Columns[14].Visible = true;
                    this.gvAddWork.Columns[15].Visible = true;
                    this.gvAddWork.Columns[16].Visible = true;
                    this.gvAddWork.Columns[17].Visible = true;
                    break;

            }

        }


        protected void GetAddDedNumber()
        {
            string comcod = this.GetCompCode();
            string mAddNUmber = "NEWADD";
            if (this.ddlPrevADNumber.Items.Count > 0)
                mAddNUmber = this.ddlPrevADNumber.SelectedValue.ToString();

            string CurDate1 = this.txtCurTransDate.Text.Trim();
            if (mAddNUmber == "NEWADD")
            {
                DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "LASTADDNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxaddno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds2.Tables[0].Rows[0]["maxaddno1"].ToString().Substring(6, 5);

                    this.ddlPrevADNumber.DataTextField = "maxaddno1";
                    this.ddlPrevADNumber.DataValueField = "maxaddno";
                    this.ddlPrevADNumber.DataSource = ds2.Tables[0];
                    this.ddlPrevADNumber.DataBind();
                }

                ds2.Dispose();
            }

        }

        private void ShowAdWork()
        {
            Session.Remove("tbladwork");
            string comcod = this.GetCompCode();
            string CurDate1 = this.txtCurTransDate.Text.Trim();
            string mAdNo = "NEWAD";
            if (this.ddlPrevADNumber.Items.Count > 0)
                mAdNo = this.ddlPrevADNumber.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETADINFO", mAdNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tbladwork"] = ds1.Tables[0];
            Session["tbltaddwork"] = ds1.Tables[3];

            if (mAdNo == "NEWAD")
            {
                DataSet ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "LASTADDNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxaddno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxaddno1"].ToString().Substring(6);
                return;
            }
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["adno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["adno1"].ToString().Substring(6, 5);
            this.txtCurTransDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["addate"]).ToString("dd-MMM-yyyy");
            this.txtNarr.Text = ds1.Tables[1].Rows[0]["narration"].ToString();
            this.ddlProjectName.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.ddlProjectName_SelectedIndexChanged(null, null);
            this.ddlUnitName.SelectedValue = ds1.Tables[1].Rows[0]["usircode"].ToString();
            this.ddlProjectName.Enabled = false;
            this.ddlUnitName.Enabled = false;
            this.lblSchCode.Text = ds1.Tables[0].Rows[0]["shcod"].ToString();
            this.ddlType.SelectedValue = ds1.Tables[0].Rows[0]["gcod"].ToString().Substring(0, 2) + "0000000";
            this.ddlInstallment.SelectedValue = ds1.Tables[0].Rows[0]["delschcode"].ToString();
            this.Data_DataBind();

        }
        private void Data_DataBind()
        {
            string comcod = this.GetCompCode();
            this.gvAddWork.DataSource = (DataTable)Session["tbladwork"];
            this.gvAddWork.DataBind();
            if (comcod == "3354" || comcod == "3101")//Edison Real Estate
            {
                this.gvAddWork.Columns[18].Visible = true;
            }
            this.FooterCalculation((DataTable)Session["tbladwork"]);

        }

        private void FooterCalculation(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvAddWork.FooterRow.FindControl("lgvFnrefund")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(nrefund)", "")) ? 0.00 :
                 dt.Compute("sum(nrefund)", ""))).ToString("#,##0.00;-#,##0.00; ");

            ((Label)this.gvAddWork.FooterRow.FindControl("lgvFndemand")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ndemand)", "")) ? 0.00 :
                 dt.Compute("sum(ndemand)", ""))).ToString("#,##0.00;-#,##0.00; ");

            ((Label)this.gvAddWork.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 :
                 dt.Compute("sum(amt)", ""))).ToString("#,##0.00;-#,##0.00; ");
            ((Label)this.gvAddWork.FooterRow.FindControl("lgvFdisAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(disamt)", "")) ? 0.00 :
                 dt.Compute("sum(disamt)", ""))).ToString("#,##0.00;-#,##0.00; ");
            ((Label)this.gvAddWork.FooterRow.FindControl("lgvFnetAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netamt)", "")) ? 0.00 :
                 dt.Compute("sum(netamt)", ""))).ToString("#,##0.00;-#,##0.00; ");

        }

        private string ComPrintAddWork()
        {
            string comcod = this.GetCompCode();
            string CompAddWork = "";

            switch (comcod)
            {
                case "3301":
                case "1301":
                case "2301":
                    CompAddWork = "PrintAddWorkSan";
                    break;
                case "3336":
                case "3337":
                    CompAddWork = "PrintAddWorkSanSuvastu";
                    break;

               
                case "3367":
                    CompAddWork = "PrintAddWorkEpic";
                    break;

                default:
                    CompAddWork = "PrintAddWork";
                    break;
            }
            return CompAddWork;
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintAdditionalWorkOrder();
        }

        private void PrintAdditionalWorkOrder()
        {
            string CompAddWork = this.ComPrintAddWork();

            switch (CompAddWork)
            {
                case "PrintAddWorkSan":
                    this.PrintAddWorkSan();
                    break;

                case "PrintAddWorkSanSuvastu":
                    this.PrintAddWorkSanSuvastu();
                    break;

                case "PrintAddWorkEpic":
                    this.PrintAddWorkEpic();
                    break;

                default:
                    this.PrintAddWork();
                    break;
            }

        }

        private void PrintAddWorkSanSuvastu()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            string UnitName = this.ddlUnitName.SelectedItem.Text;
            LocalReport Rpt1 = new LocalReport();

            string mAdNo = this.ddlPrevADNumber.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETADINFO", mAdNo, "",
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            Session["tbladwork"] = ds1.Tables[0];

            DataTable dt = (DataTable)Session["tbladwork"];
            var lst = dt.DataTableToList<RealEntity.C_24_CC.EClassAddwork.AddWorkCus>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_24_CC.RptAddWorkSuvastu", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Statement on Optional Works"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("projectName", projectName));
            Rpt1.SetParameters(new ReportParameter("UnitName", UnitName));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintAddWork()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            string unitName = this.ddlUnitName.SelectedItem.Text.Trim();
          
            DataTable dt = (DataTable)Session["tbladwork"];
            DataTable dt1 = (DataTable)Session["tbltaddwork"];
            string inwrd = Convert.ToDouble(dt1.Rows[0]["tnetamt"]).ToString("#,##0.00;(#,##0.00); ");
            double tnet = Convert.ToDouble(dt1.Rows[0]["tnetamt"]);
          

            string inword = ASTUtility.Trans(Convert.ToDouble(tnet), 2);
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_24_CC.EClassAddwork.AddWorkCus>();
          


            if (comcod== "3316" || comcod == "3315" || comcod == "1109" || comcod == "1108")
            {
                Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.RptMaintenanceWrkAssure", lst, null, null);
                Rpt1.EnableExternalImages = true;
            }
            else
            {
                Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.RptMaintenanceWrk", lst, null, null);
               
                double tpay = Convert.ToDouble(tnet);
                Rpt1.EnableExternalImages = true;
                if (tpay < 0)
                {
                    Rpt1.SetParameters(new ReportParameter("tpay", "Total Payable to Client: " + tnet*-1));
                }
                else
                {
                    Rpt1.SetParameters(new ReportParameter("tpay", "Total Payable to Company: " + tnet));
                }
                Rpt1.SetParameters(new ReportParameter("InWrd", "In Words: " + inword));
            }

            //switch (comcod)
            //{
            //    case "3101":
            //    case "1108":
            //    case "1109":
            //    case "3315":
            //    case "3316":
            //        Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.RptMaintenanceWrkAssure", lst, null, null);
            //        break;
            //    default:
            //        Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.RptMaintenanceWrk", lst, null, null);
            //        break;
            //}
          

          
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "CLIENT'S MODIFICATION"));
            Rpt1.SetParameters(new ReportParameter("projectName", projectName));
            Rpt1.SetParameters(new ReportParameter("unitName", unitName));
           
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtCurTransDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtAddNo", "Modification No: " + this.lblCurNo1.Text.ToString().Trim() + "-" + this.lblCurNo2.Text.ToString().Trim()));
            Rpt1.SetParameters(new ReportParameter("txtNarration", this.txtNarr.Text));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintAddWorkEpic()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string projectName = this.ddlProjectName.SelectedItem.Text.Substring(16);
            //string unitName = this.ddlUnitName.SelectedItem.Text.Substring(1,10);
            string Client = this.ddlUnitName.SelectedItem.Text.Substring(12);
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string mAdNo = this.Request.QueryString["Type"].ToString() == "ReqPrint" ? this.Request.QueryString["Genno"].ToString() : this.ddlPrevADNumber.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETADINFO", mAdNo, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            DataTable dt = ds1.Tables[0];
            DataTable dt1 = ds1.Tables[1];
           
            string unitName = dt1.Rows[0]["udesc"].ToString(); 
            string ClientName = dt1.Rows[0]["custname"].ToString(); 
            string projectName = dt1.Rows[0]["pactdesc"].ToString();

            string ftxtcheck = "";// ds1.Tables[2].Rows[0]["chkusr"].ToString();
            string ftxtapp1st = ds1.Tables[2].Rows[0]["fapvusr"].ToString();
            string ftxtapp2nd = ds1.Tables[2].Rows[0]["sapvusr"].ToString();
            string ftxtapp3rd = ds1.Tables[2].Rows[0]["auditusr"].ToString();
            string finalapvusr = ds1.Tables[2].Rows[0]["finalapvusr"].ToString();
            string txtAddNo = mAdNo.Substring(0, 3) + mAdNo.Substring(7, 2) + "-" + mAdNo.Substring(9);

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_24_CC.EClassAddwork.AddWorkCus>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.RptMaintenanceWrkEpic", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "CLIENT'S MODIFICATION"));
            Rpt1.SetParameters(new ReportParameter("projectName", projectName));
            Rpt1.SetParameters(new ReportParameter("unitName", unitName));
            Rpt1.SetParameters(new ReportParameter("ClientName", ClientName));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Modification Date: " + Convert.ToDateTime(this.txtCurTransDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtAddNo", "Modification No: " + txtAddNo));
            Rpt1.SetParameters(new ReportParameter("txtNarration", this.txtNarr.Text));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("ftxtcheck", ftxtcheck));
            Rpt1.SetParameters(new ReportParameter("ftxtapp1st", ftxtapp1st));
            Rpt1.SetParameters(new ReportParameter("ftxtapp2nd", ftxtapp2nd));
            Rpt1.SetParameters(new ReportParameter("ftxtapp3rd", ftxtapp3rd));
            Rpt1.SetParameters(new ReportParameter("finalapvusr", finalapvusr));

            Session["Report1"] = Rpt1;
            if (this.Request.QueryString["Type"].ToString() == "ReqPrint")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }

        }
        private void PrintAddWorkSan()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            string unitName = this.ddlUnitName.SelectedItem.Text.Trim();

            DataTable dt = (DataTable)Session["tbladwork"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_24_CC.EClassAddwork.AddWorkCus>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.RptMaintenanceWrkSan", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "CLIENT'S MODIFICATION"));
            Rpt1.SetParameters(new ReportParameter("projectName", projectName));
            Rpt1.SetParameters(new ReportParameter("unitName", unitName));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtCurTransDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtAddNo", "Modification No: " + this.lblCurNo1.Text.ToString().Trim() + "-" + this.lblCurNo2.Text.ToString().Trim()));
            Rpt1.SetParameters(new ReportParameter("txtNarration", this.txtNarr.Text));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetUnitName();
        }
        protected void ibtnFindUnitName_Click(object sender, EventArgs e)
        {
            this.GetUnitName();
        }
        protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
        {

            //string Unitcode=this.ddlUnitName.SelectedValue.ToString();
            //DataRow[] dr = ((DataTable)Session["tblunit"]).Select("usircode='" + Unitcode + "'");
            //if (dr.Length > 0) 
            //{
            //    this.lblCustomerName.Text = dr[0]["custname"].ToString();
            //    return;
            //}
            //this.lblCustomerName.Text = "";
        }
        protected void ibtnPreAdNo_Click(object sender, EventArgs e)
        {
            this.PreviousAddNumber();
        }
        protected void ibtnFindAdWork_Click(object sender, EventArgs e)
        {
            this.GetItemName();
        }
        protected void lbtnAddWork_Click(object sender, EventArgs e)
        {
            ShowInterest();
            DataTable dt = (DataTable)Session["tbladwork"];



            string gcod = this.ddlItemName.SelectedValue.ToString();
            string inscod = this.ddlInstallment.SelectedValue.ToString();
            DataRow[] dr = dt.Select("gcod='" + gcod + "'");
            //if (dr.Length == 0)
            //{

            DataRow dr1 = dt.NewRow();
            dr1["id"] = 0;
            dr1["gcod"] = gcod;
            dr1["gdesc"] = this.ddlItemName.SelectedItem.Text.Trim();
            dr1["unit"] = (((DataTable)ViewState["tblwrk"]).Select("gcod='" + gcod + "'"))[0]["unit"];
            dr1["wrkdesc"] = "";
            dr1["qty"] = 0.00;
            dr1["comqty"] = 0.00;
            dr1["comamt"] = 0.00;
            // dr1["clamt"] = 0.00;
            dr1["rate"] = 0.00;
            dr1["crate"] = 0.00;
            dr1["comlrate"] = 0.00;
            dr1["clrate"] = 0.00;
            dr1["cllrate"] = 0.00;
            dr1["amt"] = ASTUtility.Left(gcod, 2) == "13" ? (((DataTable)ViewState["tblDelayCharge"]).Select("delschcode='" + inscod + "'"))[0]["delcharge"] : 0.00;
            dr1["disamt"] = 0.00;
            dr1["netamt"] = 0.00;
            dr1["comamt"] = 0.00;
            dr1["clamt"] = 0.00;
            dr1["nrefund"] = 0.00;
            dr1["ndemand"] = 0.00;
            dr1["location"] = "";
            //   dr1["seq"] = "0";
            dr1["delschcode"] = this.ddlInstallment.Items.Count == 0 ? "" : this.ddlInstallment.SelectedValue.ToString();
            dr1["delschdesc"] = this.ddlInstallment.Items.Count == 0 ? "" : this.ddlInstallment.SelectedItem.Text.Trim();


            dt.Rows.Add(dr1);
            //}
            this.SaveValue();


        }
        private void SaveValue()
        {
            DataTable dt1 = (DataTable)Session["tbladwork"];
            int TblRowIndex;
            double comamt, clamt;
            for (int i = 0; i < this.gvAddWork.Rows.Count; i++)
            {
                TblRowIndex = (gvAddWork.PageIndex) * gvAddWork.PageSize + i;
                string seq = ((Label)this.gvAddWork.Rows[i].FindControl("lblgvSlNo3")).Text.Trim();

                string wrkdesc = ((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvdesclchoice")).Text.Trim();
                string txtgvlocateion = ((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvlocateion")).Text.Trim();
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvqty")).Text.Trim());
                double crate = Convert.ToDouble("0" + ((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvcommRate")).Text.Trim());
                double comlrate = Convert.ToDouble("0" + ((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvcomlRate")).Text.Trim());

                double clrate = Convert.ToDouble("0" + ((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvclmRate")).Text.Trim());
                double cllrate = Convert.ToDouble("0" + ((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvcllRate")).Text.Trim());
                //double rate = Convert.ToDouble("0" + ((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvRate")).Text.Trim());
                double disamt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvdiscount")).Text.Trim()));
                double comqty = Convert.ToDouble("0" + ((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvcomqty")).Text.Trim());
                double amt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvAddWork.Rows[i].FindControl("txtgvamt")).Text.Trim()));



                double rate = (this.ddlType.SelectedValue.ToString().Substring(0, 2) == "12" || this.ddlType.SelectedValue.ToString().Substring(0, 2) == "15" || this.ddlType.SelectedValue.ToString().Substring(0, 2) == "17") ? ((crate + comlrate) - (clrate + cllrate)) : ((clrate + cllrate) - (crate + comlrate));
                comamt = comqty * (crate + comlrate);
                clamt = qty * (clrate + cllrate);
                amt = amt != 0 ? amt : ((this.ddlType.SelectedValue.ToString().Substring(0, 2) == "12" || this.ddlType.SelectedValue.ToString().Substring(0, 2) == "15" || this.ddlType.SelectedValue.ToString().Substring(0, 2) == "17") ? (comamt - clamt) * -1 : clamt - comamt);


                dt1.Rows[TblRowIndex]["wrkdesc"] = wrkdesc;
                dt1.Rows[TblRowIndex]["qty"] = qty;
                dt1.Rows[TblRowIndex]["crate"] = crate;
                dt1.Rows[TblRowIndex]["comlrate"] = comlrate;
                dt1.Rows[TblRowIndex]["clrate"] = clrate;
                dt1.Rows[TblRowIndex]["cllrate"] = cllrate;
                dt1.Rows[TblRowIndex]["rate"] = rate;

                dt1.Rows[TblRowIndex]["amt"] = amt;
                dt1.Rows[TblRowIndex]["disamt"] = disamt;
                dt1.Rows[TblRowIndex]["netamt"] = amt - disamt;
                dt1.Rows[TblRowIndex]["comqty"] = comqty;
                dt1.Rows[TblRowIndex]["comamt"] = comamt;
                dt1.Rows[TblRowIndex]["clamt"] = clamt;
                dt1.Rows[TblRowIndex]["location"] = txtgvlocateion;
                //dt1.Rows[TblRowIndex]["seq"] = seq;

            }
            Session["tbladwork"] = dt1;
            this.Data_DataBind();
        }
        private string GetSchCode()
        {

            string instype = this.ddlType.SelectedValue.ToString().Substring(0, 2);
            string SchCode = "";

            switch (instype)
            {
                case "11":
                    SchCode = "819880";
                    break;
                case "12":
                    SchCode = "819890";
                    break;
                case "13":
                    SchCode = "819900";
                    break;
                case "14":
                    SchCode = "819910";
                    break;

                case "15":
                    SchCode = "819920";
                    break;

                case "16":
                    SchCode = "819930";
                    break;
                case "17":
                    SchCode = "819940";
                    break;

                case "26":
                    SchCode = "819860";
                    break;

                case "30":
                    SchCode = "819980";
                    break;

                case "32":
                    SchCode = "819990";
                    break;
                case "33":
                    SchCode = "819970";
                    break;
            }

            if (SchCode == "")
                return "";

            else
            {

                string comcod = this.GetCompCode();
                string PactCode = this.ddlProjectName.SelectedValue.ToString();
                string Usircode = this.ddlUnitName.Text.Trim();
                DataSet ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPAYSCHCODE", PactCode, Usircode, ASTUtility.Left(SchCode, 5), "", "", "", "", "", "");
                SchCode = ds3.Tables[0].Rows[0]["schcode"].ToString();
                this.lblSchCode.Text = ds3.Tables[0].Rows[0]["schcode"].ToString();
            }

            return SchCode;


        }

        private void CreateDataTable()
        {

            ViewState.Remove("tblapproval");
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("postbyid", Type.GetType("System.String"));
            tblt01.Columns.Add("postrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("postdate", Type.GetType("System.String"));
            tblt01.Columns.Add("postseson", Type.GetType("System.String"));
            tblt01.Columns.Add("chkbyid", Type.GetType("System.String"));
            tblt01.Columns.Add("chktrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("chkdate", Type.GetType("System.String"));
            tblt01.Columns.Add("chksession", Type.GetType("System.String"));
            tblt01.Columns.Add("auditid", Type.GetType("System.String"));
            tblt01.Columns.Add("audittrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("auditdate", Type.GetType("System.String"));
            tblt01.Columns.Add("auditsession", Type.GetType("System.String"));
            tblt01.Columns.Add("approvbyid", Type.GetType("System.String"));
            tblt01.Columns.Add("approvtrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("approvdate", Type.GetType("System.String"));
            tblt01.Columns.Add("approvsession", Type.GetType("System.String"));

            ViewState["tblapproval"] = tblt01;
        }

        private void CreateDataTable02()
        {
            ViewState.Remove("tblrapproval");
            DataTable tblt02 = new DataTable();
            tblt02.Columns.Add("fappid", Type.GetType("System.String"));
            tblt02.Columns.Add("fappdat", Type.GetType("System.String"));
            tblt02.Columns.Add("fapptrmid", Type.GetType("System.String"));
            tblt02.Columns.Add("fappseson", Type.GetType("System.String"));
            tblt02.Columns.Add("sappid", Type.GetType("System.String"));
            tblt02.Columns.Add("sappdat", Type.GetType("System.String"));
            tblt02.Columns.Add("sapptrmid", Type.GetType("System.String"));
            tblt02.Columns.Add("sappseson", Type.GetType("System.String"));
            ViewState["tblrapproval"] = tblt02;
        }

        private bool IsReqApprovalChk()
        {
            bool isChk = false;
            switch (GetCompCode())
            {
                case "3367":
                    isChk = true;
                    break;

                default:
                    isChk = false;
                    break;
            }
            return isChk;
        }

        private string GetReqApproval02(string rapproval)
        {
            string type = this.Request.QueryString["Type"];
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataSet ds1 = new DataSet("ds1");
            System.IO.StringReader xmlSR;

            // todo for approval 2 step epic
            if (IsReqApprovalChk())
            {
                switch (type)
                {
                    case "Entry":
                        this.CreateDataTable02();
                        DataTable dt1 = (DataTable)ViewState["tblrapproval"];
                        DataRow dr1 = dt1.NewRow();
                        dr1["fappid"] = "";
                        dr1["fappdat"] = "";
                        dr1["fapptrmid"] = "";
                        dr1["fappseson"] = "";
                        dr1["sappid"] = "";
                        dr1["sappdat"] = "";
                        dr1["sapptrmid"] = "";
                        dr1["sappseson"] = "";
                        dt1.Rows.Add(dr1);
                        ds1.Merge(dt1);
                        ds1.Tables[0].TableName = "tbl1";
                        rapproval = ds1.GetXml();
                        break;

                    case "FirstApproval":
                        this.CreateDataTable02();
                        DataTable dt2 = (DataTable)ViewState["tblrapproval"];
                        DataRow dr2 = dt2.NewRow();
                        dr2["fappid"] = usrid;
                        dr2["fappdat"] = Date;
                        dr2["fapptrmid"] = trmnid;
                        dr2["fappseson"] = session;
                        dr2["sappid"] = "";
                        dr2["sappdat"] = "";
                        dr2["sapptrmid"] = "";
                        dr2["sappseson"] = "";
                        dt2.Rows.Add(dr2);
                        ds1.Merge(dt2);
                        ds1.Tables[0].TableName = "tbl1";
                        rapproval = ds1.GetXml();
                        break;

                    case "SecondApproval":
                        xmlSR = new System.IO.StringReader(rapproval);
                        ds1.ReadXml(xmlSR);
                        ds1.Tables[0].TableName = "tbl1";
                        ds1.Tables[0].Rows[0]["sappid"] = usrid;
                        ds1.Tables[0].Rows[0]["sappdat"] = Date;
                        ds1.Tables[0].Rows[0]["sapptrmid"] = trmnid;
                        ds1.Tables[0].Rows[0]["sappseson"] = session;
                        rapproval = ds1.GetXml();
                        break;

                    default:
                        break;
                }

            }
            else
            {
                this.CreateDataTable02();
                DataTable dt2 = (DataTable)ViewState["tblrapproval"];
                DataRow dr2 = dt2.NewRow();
                dr2["fappid"] = usrid;
                dr2["fappdat"] = Date;
                dr2["fapptrmid"] = trmnid;
                dr2["fappseson"] = session;
                dr2["sappid"] = usrid;
                dr2["sappdat"] = Date;
                dr2["sapptrmid"] = trmnid;
                dr2["sappseson"] = session;

                dt2.Rows.Add(dr2);
                ds1.Merge(dt2);
                ds1.Tables[0].TableName = "tbl1";
                rapproval = ds1.GetXml();
            }
            return rapproval;

        }

        private string GetReqApproval(string approval)
        {
            string type = this.Request.QueryString["Type"];
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataSet ds1 = new DataSet("ds1");
            System.IO.StringReader xmlSR;

            switch (type)
            {
                case "Entry":
                case "EntrySales":
                    switch (comcod)
                    {
                        // todo for skip customer care interface
                        case "3330": // Bridge
                        case "3333": // Alliance
                        case "3335": // Edison
                        case "3336": // Suvastu
                        case "3337": // Suvastu
                        case "3338": // Acme
                        case "3339": // Tropical
                        case "3340": // Urban                   
                        case "3344": // Terranova
                        case "3354": // ERL Own
                        case "3343": // Dominion
                                     //case "3101": // pintech

                            if (approval == "")
                            {
                                this.CreateDataTable();
                                DataTable dt = (DataTable)ViewState["tblapproval"];
                                DataRow dr1 = dt.NewRow();

                                dr1["postbyid"] = usrid;
                                dr1["postrmid"] = trmnid;
                                dr1["postdate"] = Date;
                                dr1["postseson"] = session;
                                dr1["chkbyid"] = usrid;
                                dr1["chktrmid"] = trmnid;
                                dr1["chkdate"] = Date;
                                dr1["chksession"] = session;
                                dr1["auditid"] = usrid;
                                dr1["audittrmid"] = trmnid;
                                dr1["auditdate"] = Date;
                                dr1["auditsession"] = session;
                                dr1["approvbyid"] = usrid;
                                dr1["approvtrmid"] = trmnid;
                                dr1["approvdate"] = Date;
                                dr1["approvsession"] = session;

                                dt.Rows.Add(dr1);

                                ds1.Merge(dt);
                                ds1.Tables[0].TableName = "tbl1";
                                approval = ds1.GetXml();
                            }
                            break;

                        default:

                            if (approval == "")
                            {

                                this.CreateDataTable();
                                DataTable dt = (DataTable)ViewState["tblapproval"];
                                DataRow dr1 = dt.NewRow();

                                dr1["postbyid"] = usrid;
                                dr1["postrmid"] = trmnid;
                                dr1["postdate"] = Date;
                                dr1["postseson"] = session;
                                dr1["chkbyid"] = "";
                                dr1["chktrmid"] = "";
                                dr1["chkdate"] = "";
                                dr1["chksession"] = "";
                                dr1["auditid"] = "";
                                dr1["audittrmid"] = "";
                                dr1["auditdate"] = "";
                                dr1["auditsession"] = "";
                                dr1["approvbyid"] = "";
                                dr1["approvtrmid"] = "";
                                dr1["approvdate"] = "";
                                dr1["approvsession"] = "";
                                dt.Rows.Add(dr1);

                                ds1.Merge(dt);
                                ds1.Tables[0].TableName = "tbl1";
                                approval = ds1.GetXml();
                            }
                            break;
                    }
                    break;

                case "Check":
                    // string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString();  

                    if (approval == "")
                    {
                        this.CreateDataTable();
                        DataTable dt = (DataTable)ViewState["tblapproval"];
                        if (comcod == "3315" || comcod == "3316" || comcod == "3364"|| comcod == "3101")
                        {
                            DataRow dr1 = dt.NewRow();
                            dr1["chkbyid"] = usrid;
                            dr1["chktrmid"] = trmnid;
                            dr1["chkdate"] = Date;
                            dr1["chksession"] = session;
                            dr1["auditid"] = usrid;
                            dr1["audittrmid"] = trmnid;
                            dr1["auditdate"] = Date;
                            dr1["auditsession"] = session;
                            dr1["approvbyid"] = "";
                            dr1["approvtrmid"] = "";
                            dr1["approvdate"] = "";
                            dr1["approvsession"] = "";
                            dt.Rows.Add(dr1);
                            ds1.Merge(dt);
                            ds1.Tables[0].TableName = "tbl1";
                            approval = ds1.GetXml();

                        }
                        else
                        {

                            DataRow dr1 = dt.NewRow();
                            dr1["chkbyid"] = usrid;
                            dr1["chktrmid"] = trmnid;
                            dr1["chkdate"] = Date;
                            dr1["chksession"] = session;
                            dr1["auditid"] = "";
                            dr1["audittrmid"] = "";
                            dr1["auditdate"] = "";
                            dr1["auditsession"] = "";
                            dr1["approvbyid"] = "";
                            dr1["approvtrmid"] = "";
                            dr1["approvdate"] = "";
                            dr1["approvsession"] = "";
                            dt.Rows.Add(dr1);
                            ds1.Merge(dt);
                            ds1.Tables[0].TableName = "tbl1";
                            approval = ds1.GetXml();
                        }
                    }

                    else
                    {
                        xmlSR = new System.IO.StringReader(approval);
                        ds1.ReadXml(xmlSR);
                        ds1.Tables[0].TableName = "tbl1";
                        ds1.Tables[0].Rows[0]["chkbyid"] = usrid;
                        ds1.Tables[0].Rows[0]["chktrmid"] = trmnid;
                        ds1.Tables[0].Rows[0]["chkdate"] = Date;
                        ds1.Tables[0].Rows[0]["chksession"] = session;
                        ds1.Tables[0].Rows[0]["auditid"] = "";
                        ds1.Tables[0].Rows[0]["audittrmid"] = "";
                        ds1.Tables[0].Rows[0]["auditdate"] = "";
                        ds1.Tables[0].Rows[0]["auditsession"] = "";
                        ds1.Tables[0].Rows[0]["approvbyid"] = "";
                        ds1.Tables[0].Rows[0]["approvtrmid"] = "";
                        ds1.Tables[0].Rows[0]["approvdate"] = "";
                        ds1.Tables[0].Rows[0]["approvsession"] = "";
                        approval = ds1.GetXml();
                    }
                    break;

                case "Audit":
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["auditid"] = usrid;
                    ds1.Tables[0].Rows[0]["audittrmid"] = trmnid;
                    ds1.Tables[0].Rows[0]["auditdate"] = Date;
                    ds1.Tables[0].Rows[0]["auditsession"] = session;
                    approval = ds1.GetXml();
                    break;

                case "Approv":
                    // string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString();  
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["approvbyid"] = usrid;
                    ds1.Tables[0].Rows[0]["approvtrmid"] = trmnid;
                    ds1.Tables[0].Rows[0]["approvdate"] = Date;
                    ds1.Tables[0].Rows[0]["approvsession"] = session;
                    approval = ds1.GetXml();
                    break;
            }
            return approval;

        }


        private string FinalApproval()
        {
            string comcod = this.GetCompCode();
            string type = this.Request.QueryString["Type"];
            string approval = "";
            switch (type)
            {
                case "Entry":
                case "EntrySales":
                    switch (comcod)
                    {
                        case "3101":
                        case "3315": //Assure 
                        case "3316":
                        case "3317":
                        case "3364": //jbs
                        case "3367": //epic
                            break;
                        default:
                            approval = "approval";
                            break;
                    }
                    break;


                case "Check":
                    switch (comcod)
                    {
                        case "3101":
                        case "3315"://Assure 
                        case "3316":
                        case "3317":
                        case "3364": //jbs
                            break;
                    }
                    break;

                case "Audit":
                    switch (comcod)
                    {
                        case "3101":
                        case "3315"://Assure 
                        case "3316":
                        case "3317":
                        case "3364": //jbs
                            break;
                    }
                    break;


                case "Approv":
                    switch (comcod)
                    {
                        case "3101":
                        case "3315"://Assure 
                        case "3316":
                        case "3317":
                        case "3364": //jbs
                            approval = "approval";
                            break;
                    }
                    break;
            }
            return approval;

        }

        protected void lFinalUpdateAdWork_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                msg = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }
            string Gcode = "";
            string comcod = this.GetCompCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.ddlUnitName.Text.Trim();
            string curdate = Convert.ToDateTime(this.txtCurTransDate.Text).ToString("dd-MMM-yyyy");

            string paysch = "";
            if (comcod == "3315" || comcod == "3316" || comcod == "3317" || comcod == "3364" ||comcod == "3101")
            {
                paysch = this.GetSchCode();
            }
            else
            {
                paysch = (this.lblSchCode.Text.Trim() == "") ? this.GetSchCode() : this.lblSchCode.Text.Trim();
            }

            //string payschtest = this.GetSchCode();
            this.SaveValue();
            DataTable dt = (DataTable)Session["tbladwork"];
            double schamt = 0;
            string PaydelSchCode = dt.Rows[0]["delschcode"].ToString();
            //bool result;
            string SchCode1 = "";
            if (this.ddlPrevADNumber.Items.Count == 0)
                this.GetAddDedNumber();

            string addno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string narration = this.txtNarr.Text.Trim();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Gcode = dt.Rows[i]["gcod"].ToString();

                string seq = (i + 1).ToString();
                string id = dt.Rows[i]["id"].ToString();
                string wrkdesc = dt.Rows[i]["wrkdesc"].ToString();
                string location = dt.Rows[i]["location"].ToString();
                string qty = Convert.ToDouble(dt.Rows[i]["qty"].ToString()).ToString();
                string clamt = Convert.ToDouble(dt.Rows[i]["clamt"].ToString()).ToString();
                string comqty = Convert.ToDouble(dt.Rows[i]["comqty"].ToString()).ToString();
                string comamt = Convert.ToDouble(dt.Rows[i]["comamt"].ToString()).ToString();
                string appxml = dt.Rows[i]["approval"].ToString();
                string Approval = this.GetReqApproval(appxml);
                string rappxml = dt.Rows[i]["rapproval"].ToString();
                string ReqApproval = this.GetReqApproval02(rappxml); // todo for epic 
                string ApChk = Convert.ToString(IsReqApprovalChk()); // todo for epic 
                double disamt = (this.ddlType.SelectedValue.ToString().Substring(0, 2) == "12" || this.ddlType.SelectedValue.ToString().Substring(0, 2) == "15" || this.ddlType.SelectedValue.ToString().Substring(0, 2) == "17"
                                    || this.ddlType.SelectedValue.ToString().Substring(0, 2) == "32") ? (Convert.ToDouble(dt.Rows[i]["disamt"]) * -1)
                    : Convert.ToDouble(dt.Rows[i]["disamt"]);

                double amt = Convert.ToDouble(dt.Rows[i]["amt"].ToString());
                //double amt = (this.ddlType.SelectedValue.ToString().Substring(0, 2) == "12" || this.ddlType.SelectedValue.ToString().Substring(0, 2) == "15" || this.ddlType.SelectedValue.ToString().Substring(0, 2) == "17") ? (Convert.ToDouble(dt.Rows[i]["amt"].ToString()) * -1)
                //    : Convert.ToDouble(dt.Rows[i]["amt"].ToString());

                schamt = schamt + amt - disamt;

                string crate = Convert.ToDouble(dt.Rows[i]["crate"].ToString()).ToString();
                string comlrate = Convert.ToDouble(dt.Rows[i]["comlrate"].ToString()).ToString();
                string clrate = Convert.ToDouble(dt.Rows[i]["clrate"].ToString()).ToString();
                string cllrate = Convert.ToDouble(dt.Rows[i]["cllrate"].ToString()).ToString();
                string delSchCode = dt.Rows[i]["delschcode"].ToString();

                //if (amt > 0)
                bool ressult2 = MktData.UpdateTransInfo01(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATEADWORK", addno, PactCode, Usircode, Gcode, curdate, qty, amt.ToString(), paysch, comamt, wrkdesc, narration, disamt.ToString(), comqty, clamt, location, crate, comlrate, clrate, cllrate, Approval, seq, delSchCode, ReqApproval, ApChk);

                if (!ressult2)
                {
                    msg = "Updated Failed in Payment Information ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }


                msg = "Sales Increase Info Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            }

            string approval = FinalApproval();
            if (approval == "approval")
            {

                if (Gcode != "")
                {
                    SchCode1 = Gcode.Substring(0, 2);
                    string gcode = (SchCode1 == "11") ? "02090" : (SchCode1 == "12") ? "02091" : (SchCode1 == "13") ? "02092" : (SchCode1 == "14") ? "02003" : (SchCode1 == "15") ? "02094" : (SchCode1 == "16") ? "02095"
                        : (SchCode1 == "17") ? "02096"
                        : (SchCode1 == "26") ? "02086"
                        : (SchCode1 == "32") ? "02098"
                        : (SchCode1 == "33") ? "02085" : "02097";
                    //if net amount is negative then  refundable 
                    //  gcode = (gcode == "02090" && schamt < 0) ? "02091" : gcode;
                    result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INORUPSALGINF1CUMOD", PactCode, Usircode, gcode, SchCode1, "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        msg = "Updated Failed in Revenue Information ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                        return;
                    }
                }

                result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATEPAYMENTINF", PactCode, Usircode, paysch, curdate, schamt.ToString(), "", "", "", "", "", PaydelSchCode, "", "", "");

                if (!result)
                {
                    msg = "Updated Failed in Payment Information ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }

                msg = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Client Modification";
                string eventdesc = "Update Info";
                string eventdesc2 = addno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        protected void lbtnTotalAddWork_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }

        protected void lbtnAdWrkdelete_Click(object sender, EventArgs e)
        {
            int rownum = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tbladwork"];
            string id = ((Label)this.gvAddWork.Rows[rownum].FindControl("lblgbID")).Text.Trim();

            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "DELETEADDWRK",
                       id, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                dt.Rows[rownum].Delete();
            }

            if (!result)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Data Delete Fail" + "');", true);

                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Data Delete Success" + "');", true);


            DataView dv = dt.DefaultView;
            Session.Remove("tbladwork");
            Session["tbladwork"] = dv.ToTable();
            this.Data_DataBind();

        }
    }
}