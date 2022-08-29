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
using System.IO;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
using RealEntity.C_22_Sal;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class InterfaceAttApp : System.Web.UI.Page
    {
        //public static string orderno = "", centrid = "", custid = "", orderno1 = "", orderdat = "", Delstatus = "", Delorderno = "", RDsostatus="";
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        Common compUtility = new Common();
        bool sup_app = false, dpthead_app = false, mgt_app = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect(this.ResolveUrl("~/AcceessError.aspx"));
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "REQUEST INTERFACE";//
                this.GetCompany();
                this.GetRequestType();
                this.SelectDate();
                this.RadioButtonList1.SelectedIndex = 0;
                this.pnlInt.Visible = true;
                this.GetBranch();
                this.visibilityBracnh();
                this.GetStep();
                this.SaleRequRpt();
                this.RadioButtonList1_SelectedIndexChanged(null, null);
            }
        }
       
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tbltotalleav"];
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EInterfaceAttApp>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptInterfaceAttApp", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comname));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Request"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void GetRequestType()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();

            DataSet ds = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETREQUESTTYPE", "", "");
            if (ds == null)
                return;
            //dtprof.Rows.Add("000000000000", "Choose Peofession..", "");
            this.ddrequesttype.DataTextField = "hrgdesc";
            this.ddrequesttype.DataValueField = "unit";
            this.ddrequesttype.DataSource = ds.Tables[0];
            this.ddrequesttype.DataBind();
            this.ddrequesttype.Items.Insert(0, new ListItem("--Select Request Type--", "%%"));
        }
        private void SelectDate()
        {
            string comcod = this.GetCompCode();          
            this.txFdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DateTime date = Convert.ToDateTime(txFdate.Text);
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
                return;
            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            string frmdate = Convert.ToInt32(date.ToString("dd")) > Convert.ToInt32(startdate) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
            frmdate = startdate + frmdate.Substring(2);
            this.txFdate.Text = frmdate;
            this.txtdate.Text = Convert.ToDateTime(this.txFdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
           //string tdate = date.ToString("dd-MMM-yyyy");
        }
        private void GetStep()
        {
            DataSet copSetup = compUtility.GetCompUtility();
            if (copSetup == null)
                return;
            //sup_app = copSetup.Tables[0].Rows.Count == 0 ? false : Convert.ToBoolean(copSetup.Tables[0].Rows[0]["LVAPP_SUPERVISOR"]);
            //dpthead_app = copSetup.Tables[0].Rows.Count == 0 ? false : Convert.ToBoolean(copSetup.Tables[0].Rows[0]["LVAPP_DPTHEAD"]);
            //mgt_app = copSetup.Tables[0].Rows.Count == 0 ? false : Convert.ToBoolean(copSetup.Tables[0].Rows[0]["LVAPP_MGTHEAD"]);

        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {

        }
        protected void lnkbtnok_Click(object sender, EventArgs e)
        {
            this.SaleRequRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            this.SaleRequRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }
        //protected void lnkOk_Click(object sender, EventArgs e)
        //{
        //    this.SaleRequRpt();
        //}
        protected void lnkInterface_Click(object sender, EventArgs e)
        {
            this.pnlInt.Visible = true;
            this.pnlReport.Visible = false;
        }
        protected void lnkRept_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            this.pnlInt.Visible = false;
            this.pnlReport.Visible = true;
            if (ASTUtility.Left(comcod, 1) == "7")
            {
                this.pnlTrade.Visible = true;
            }
            else
            {
                this.plnMgf.Visible = true;
            }
        }
        private void SaleRequRpt()
        {
            Session.Remove("tblaproved");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = hst["usrid"].ToString();// (this.Request.QueryString["Type"] == "Ind") || (this.Request.QueryString["Type"] == "DeptHead") ? hst["usrid"].ToString() : "";
            string fDate = Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy");
            string tDate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            string type = "";//(this.Request.QueryString["Type"]) == "Ind" || (this.Request.QueryString["Type"] == "DeptHead") ? "" : "Management";
            string DeptHead = "";//(this.Request.QueryString["Type"]) == "DeptHead" ? "DeptHead" : "";
            string reqtyp = this.ddrequesttype.SelectedValue.ToString();
            string searchkey = "%" + this.txtSearch.Text.Trim() + "%";

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string CompanyName = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
            string branch = (this.ddlBranch.SelectedValue.ToString() == "000000000000" || this.ddlBranch.SelectedValue.ToString() == "" ? CompanyName : this.ddlBranch.SelectedValue.ToString().Substring(0, 4)) + "%";
            string projectcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? branch : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%");
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? projectcode : this.ddlSection.SelectedValue.ToString());

           
            DataSet ds1 = accData.GetTransInfoNew(comcod, "DBO_HRM.SP_REPORT_HR_MGT_INTERFACE", "GETALLATTREQUEST", null,null,null, fDate, tDate, usrid, type, DeptHead,"", reqtyp, searchkey,
                CompanyName, branch, projectcode, section,"","","","","","","","");
            if (ds1 == null)
                return;

            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading deep-sky-blue counter'>" + ds1.Tables[1].Rows[0]["tcount"].ToString() + "</div></a><div class='circle-tile-content deep-sky-blue'><div class='circle-tile-description txt-white'>Request</div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ds1.Tables[1].Rows[0]["reqcount"].ToString() + "</div></a><div class='circle-tile-content purple'><div class='circle-tile-description txt-white'>Process</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading deep-pink counter'>" + ds1.Tables[1].Rows[0]["appcount"].ToString() + "</div></a><div class='circle-tile-content deep-pink'><div class='circle-tile-description txt-white'> Approval</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + ds1.Tables[1].Rows[0]["tappcount"].ToString() + "</div></a><div class='circle-tile-content orange'><div class='circle-tile-description txt-white'>Confirmed</div></div></div>";
            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading deep-green counter'>" + ds1.Tables[1].Rows[0]["tcancel"].ToString() + "</div></a><div class='circle-tile-content deep-green'><div class='circle-tile-description txt-white'>Canceled</div></div></div>";

            // All Order
            DataTable dt = new DataTable();
            DataView dv = new DataView();
            dt = ((DataTable)ds1.Tables[0]).Copy();
            ViewState["tbltotalleav"] = dt;
            this.Data_Bind("gvAttReq", dt);

            //In-process
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            //dv.RowFilter = ("sostatus = 'In-process' or  sostatus = 'Request' ");
            dv.RowFilter = ("supstatus='' and lvstatus <> 'Approved' and lvstatus <> 'Canceled'");
           
            this.Data_Bind("gvInprocess", dv.ToTable());
            //Approved
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("dptstatus = '' and  supstatus<>''  and lvstatus <> 'Approved'");
            //dv.RowFilter = ("sostatus = 'Approved' or sostatus = 'In-process' ");
            this.Data_Bind("gvApproved", dv.ToTable());
        
            //Confirm
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("lvstatus = 'Approved' ");
            //dv.RowFilter = ("sostatus = 'Approved' or sostatus = 'In-process' ");
            this.Data_Bind("gvConfirm", dv.ToTable());

            //Canceled Data
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("lvstatus = 'Canceled'");
            //dv.RowFilter = ("sostatus = 'Approved' or sostatus = 'In-process' ");
            this.Data_Bind("gvfiApproved", dv.ToTable());
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    this.pnlallReq.Visible = true;
                    this.PnlProcess.Visible = false;
                    this.PnlApp.Visible = false;
                    this.pnlFApp.Visible = false;
                    this.PnlConfrm.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    break;
                case "1":
                    this.pnlallReq.Visible = false;
                    this.PnlProcess.Visible = true;
                    this.PnlApp.Visible = false;
                    this.pnlFApp.Visible = false;
                    this.PnlConfrm.Visible = false;
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                case "2":
                    this.pnlallReq.Visible = false;
                    this.PnlProcess.Visible = false;
                    this.PnlApp.Visible = true;
                    this.pnlFApp.Visible = false;
                    this.PnlConfrm.Visible = false;
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;
                case "3":
                    this.pnlallReq.Visible = false;
                    this.PnlProcess.Visible = false;
                    this.PnlApp.Visible = false;
                    this.pnlFApp.Visible = false;
                    this.PnlConfrm.Visible = true;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    break;
                case "4":
                    this.pnlallReq.Visible = false;
                    this.PnlProcess.Visible = false;
                    this.PnlApp.Visible = false;
                    this.pnlFApp.Visible = true;
                    this.PnlConfrm.Visible = false;
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    break;
            }
        }
        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "gvAttReq":
                    this.gvAttReq.DataSource = (dt);
                    this.gvAttReq.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvInprocess":
                    this.gvInprocess.DataSource = (dt);
                    this.gvInprocess.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvApproved":
                    this.gvApproved.DataSource = (dt);
                    this.gvApproved.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvfiApproved":
                    this.gvfiApproved.DataSource = (dt);
                    this.gvfiApproved.DataBind();
                    break;
                case "gvConfirm":
                    this.gvConfirm.DataSource = (dt);
                    this.gvConfirm.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;
            }
        }
        protected void gvInprocess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HylvPrint");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnApp");
                HyperLink lnkbtnDptApp = (HyperLink)e.Row.FindControl("lnkbtnDptApp");
                HyperLink hlnEdit = (HyperLink)e.Row.FindControl("lnkbtnEditIN");
                LinkButton hlnDel = (LinkButton)e.Row.FindControl("lnkRemove");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string empusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empusrid")).ToString().Trim();
                string dptusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dptusid")).ToString().Trim();
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString().Trim();
                string strtdat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "strtdat")).ToString();

                string refno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string suserid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "susrid")).ToString();
                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();
                string reqtyp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lvtype")).ToString();
                string aplydat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "aplydat")).ToString("dd-MMM-yyyy");

                if (userid == dptusrid)
                {
                    hlink3.Visible = false;
                    lnkbtnDptApp.Visible = true;
                    lnkbtnDptApp.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/EmpAttApproval.aspx?Type=Ind&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + strtdat + "&RoleType=DPT"+"&Reqtype="+ reqtyp;                   
                }
                if (userid == suserid)
                {
                    lnkbtnDptApp.Visible = false;
                    hlink3.Visible = true;
                    hlink3.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/EmpAttApproval.aspx?Type=Ind&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + strtdat + "&RoleType=SUP" + "&Reqtype="+ reqtyp;

                }

                if ((userid == suserid) && (userid == dptusrid))
                {
                    lnkbtnDptApp.Visible = true;
                    hlink3.Visible = false;
                    lnkbtnDptApp.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/EmpAttApproval.aspx?Type=Ind&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + strtdat + "&RoleType=DPT" + "&Reqtype="+ reqtyp;

                }
                hlnDel.Visible = (userid == empusrid) ? true : false;
                //hlnEdit.Visible = (userid == empusrid) ? true : false;

                //hlnEdit.NavigateUrl = "~/F_81_Hrm/F_84_Lea/MyLeave.aspx?Type=User&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid + "&Reqtype="; ;
                //hlink1.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/PrintLeaveInterface.aspx?Type=ApplyPrint&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid + "&Reqtype="+ reqtyp;
            }
        }
        protected void gvApproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HylvPrint");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnApp");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string strtdat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "strtdat")).ToString();
                string refno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string urefno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();
                string aplydat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "aplydat")).ToString("dd-MMM-yyyy");
                string dptusid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dptusid")).ToString();
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();
                string reqtyp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lvtype")).ToString();
                string lvstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lvstatus")).ToString();

                hlink3.Visible = ((userid == dptusid) && (lvstatus != "Approved")) ? true : false;
                hlink3.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/EmpAttApproval.aspx?Type=Ind&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + strtdat + "&RoleType=DPT" + "&Reqtype=" + reqtyp;
                //hlink1.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/PrintLeaveInterface.aspx?Type=ApplyPrint&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid;

            }
        }
        protected void gvAttReq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyperApplyPrint");
                HyperLink hlnEdit = (HyperLink)e.Row.FindControl("lnkbtnEditUser");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string usrid = hst["usrid"].ToString();

                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();
                string empusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empusrid")).ToString();
                string strtdat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "strtdat")).ToString();
                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();
                string lvstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lvstatus")).ToString();
                //hlnEdit.Visible = ((usrid == empusrid) && (lvstatus != "Approved")) ? true : false;
                //hlnEdit.NavigateUrl = "~/F_81_Hrm/F_84_Lea/MyLeave.aspx?Type=User&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid;
                //hlink1.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/PrintLeaveInterface.aspx?Type=ApplyPrint&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid;
            }
        }

        protected void gvConfirm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyOrderPrint");
                LinkButton hlinkForward = (LinkButton)e.Row.FindControl("lnkRemoveForward");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();
                string strtdat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "strtdat")).ToString();
                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();

                string refno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string urefno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();

                string aplydat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "aplydat")).ToString("dd-MMM-yyyy");
                string dptusid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dptusid")).ToString();

                string lvstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lvstatus")).ToString();

                hlinkForward.Visible = ((userid == dptusid) && (lvstatus == "Approved")) ? true : false;
                //hlink1.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/PrintLeaveInterface.aspx?Type=ApplyPrint&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid;
            }
        }
        protected void gvfiApproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DataTable dte = (DataTable)Session["tblmaproved"];
                //DataTable dt = (DataTable)Session["tbleaproved"];
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnAppfi");
                LinkButton hlink1 = (LinkButton)e.Row.FindControl("lnkRemoveFAp");
             
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string usrid = hst["usrid"].ToString();
                string refno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string urefno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();
                string aplydat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "aplydat")).ToString("dd-MMM-yyyy");
                string lvstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lvstatus")).ToString();
                string dptusid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dptusid")).ToString();

                switch (comcod)
                {
                    case "3348": //Credence
                        break;
                    default:
                        //DataRow[] dr1 = dt.Select("usrid='" + usrid + "' and centrid='" + refno + "'");
                        //DataRow[] dre = dte.Select("usrid='" + usrid + "'");
                        //hlink3.Enabled = dre.Length > 0 ? true : ((dr1.Length > 0) ? true : false);
                        //hlink3.Visible = dre.Length > 0 ? true : ((dr1.Length > 0) ? true : false);
                        break;
                }
                hlink1.Visible = ((usrid == dptusid) && (lvstatus == "Canceled")) ? true : false;
                hlink3.NavigateUrl = "~/F_81_Hrm/F_84_Lea/EmpLvApproval.aspx?Type=App&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + aplydat + "&RoleType=MGT";
            }
        }

        protected void lnkRemove_Click(object sender, EventArgs e)
        {
            
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            int index = row.RowIndex;
            string empid = ((Label)this.gvInprocess.Rows[index].FindControl("lblgvempid")).Text.ToString();
            string leavid = ((Label)this.gvInprocess.Rows[index].FindControl("lblLeavId")).Text.ToString();

            DataTable dt = (DataTable)ViewState["tbltotalleav"];
            bool result = accData.UpdateTransInfo(comcod, "DBO_HRM.SP_REPORT_HR_INTERFACE", "DELETE_REQUEST_INFORMATION", leavid, empid, usrid, "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {
                //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data deleted successfully')", true);
                string Messaged = "deleted successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentl('" + Messaged + "');", true);

                int ins = this.gvInprocess.PageSize * this.gvInprocess.PageIndex + index;
                dt.Rows[ins].Delete();
                ViewState.Remove("tbltotalleav");
                DataView dv = dt.DefaultView;
                ViewState["tbltotalleav"] = dv.ToTable();


                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Requset Delete";
                    string eventdesc = "Requset Delete";
                    string eventdesc2 = leavid;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }
            this.SaleRequRpt();



        }

        protected void lnkRemoveForward_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["delete"]))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
            //    return;
            //}
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            int index = row.RowIndex;
            string empid = ((Label)this.gvConfirm.Rows[index].FindControl("lblgvempid")).Text.ToString();
            string leavid = ((Label)this.gvConfirm.Rows[index].FindControl("lblLeavId")).Text.ToString();
            string lvdptuid = ((Label)this.gvConfirm.Rows[index].FindControl("lbldptusid")).Text.ToString();

            DataTable dt = (DataTable)ViewState["tbltotalleav"];
            bool result = accData.UpdateTransInfo(comcod, "DBO_HRM.SP_REPORT_HR_INTERFACE", "ATTREQUESTAPPFROWARD", leavid, empid, lvdptuid, "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {
                //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data deleted successfully')", true);
                string Messaged = "Approved Request Forward successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentl('" + Messaged + "');", true);

                int ins = this.gvInprocess.PageSize * this.gvInprocess.PageIndex + index;
                dt.Rows[ins].Delete();
                ViewState.Remove("tbltotalleav");
                DataView dv = dt.DefaultView;
                ViewState["tbltotalleav"] = dv.ToTable();


                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Requset Forward";
                    string eventdesc = "Requset Forward";
                    string eventdesc2 = leavid;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }
            this.SaleRequRpt();

        }

        protected void lnkRemoveApp_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            int index = row.RowIndex;
            string empid = ((Label)this.gvApproved.Rows[index].FindControl("lblgvempid")).Text.ToString();
            string leavid = ((Label)this.gvApproved.Rows[index].FindControl("lblLeavId")).Text.ToString();
            //ViewState["tbltotalleav"] = dt;
            DataTable dt = (DataTable)ViewState["tbltotalleav"];
            bool result = accData.UpdateTransInfo(comcod, "DBO_HRM.SP_REPORT_HR_INTERFACE", "DELETELEAVEINFO", leavid, empid, usrid, "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data deleted successfully')", true);
                int ins = this.gvApproved.PageSize * this.gvApproved.PageIndex + index;
                dt.Rows[ins].Delete();
                ViewState.Remove("tbltotalleav");
                DataView dv = dt.DefaultView;
                ViewState["tbltotalleav"] = dv.ToTable();

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Request Requset Delete";
                    string eventdesc = "Request Requset Delete, Employe id" + empid;
                    string eventdesc2 = leavid;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }
            this.SaleRequRpt();



        }

        protected void lnkRemoveFAp_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["delete"]))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
            //    return;
            //}
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            int index = row.RowIndex;
            string empid = ((Label)this.gvfiApproved.Rows[index].FindControl("lblgvempidfi")).Text.ToString();
            string leavid = ((Label)this.gvfiApproved.Rows[index].FindControl("lblLeavIdfi")).Text.ToString();
            //ViewState["tbltotalleav"] = dt;
            DataTable dt = (DataTable)ViewState["tbltotalleav"];
            bool result = accData.UpdateTransInfo(comcod, "DBO_HRM.SP_REPORT_HR_INTERFACE", "ATTREQUESTAPPFROWARD", leavid, empid, usrid, "cancel", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Canceled Request Forward  successfully')", true);
                int ins = this.gvfiApproved.PageSize * this.gvfiApproved.PageIndex + index;
                dt.Rows[ins].Delete();
                ViewState.Remove("tbltotalleav");
                DataView dv = dt.DefaultView;
                ViewState["tbltotalleav"] = dv.ToTable();

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Request Canceled";
                    string eventdesc = "Request Cancel, Employe id" + empid;
                    string eventdesc2 = leavid;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }
            this.SaleRequRpt();
        }
        private void GetCompany()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = "94%";
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GET_ACCESSED_COMPANYLIST", txtCompany, userid, "", "", "", "", "", "", "");
            // DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");


            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.ddlCompany_SelectedIndexChanged(null, null);
            ds1.Dispose();
        }
        private void GetBranch()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string txtSProject = "%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETBRANCH", Company, txtSProject, "", "", "", "", "", "", "");

            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GETBRANCH_NEW", Company, userid, "", "", "", "", "", "", "");

            this.ddlBranch.DataTextField = "actdesc";
            this.ddlBranch.DataValueField = "actcode";
            this.ddlBranch.DataSource = ds1.Tables[0];
            this.ddlBranch.DataBind();
            this.ddlBranch_SelectedIndexChanged(null, null);
        }
        private void visibilityBracnh()
        {
            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3315":
                case "3347":
                case "3353":
                case "3358":
                    this.divBracnhLsit.Visible = false;
                    this.ddlBranch.Items.Clear();
                    break;
                default:
                    this.divBracnhLsit.Visible = true;
                    break;
            }
        }
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
            string branch = (this.ddlBranch.SelectedValue.ToString() == "000000000000" || this.ddlBranch.SelectedValue.ToString() == "" ? Company : this.ddlBranch.SelectedValue.ToString().Substring(0, 4)) + "%";
            string txtSProject = "%%";
            //  DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", branch, txtSProject, "", "", "", "", "", "", "");
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GETDPTLIST_NEW", branch, userid, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);
            // this.SectionName();
        }
        private void SectionName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            //  string projectcode = this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "%%" : this.ddlProjectName.SelectedValue.ToString();
            string projectcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9)) + "%";
            string txtSSec = "%%";
            // DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");

            DataSet ds2 = accData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GETSECTION_LIST", projectcode, userid, "", "", "", "", "", "", "");

            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();
            // this.GetEmpName();
            //ddlSection_SelectedIndexChanged(null, null);
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3315":
                case "3347":
                case "3353":
                case "3358":
                    this.divBracnhLsit.Visible = false;
                    this.ddlBranch.Items.Clear();
                    this.GetProjectName();
                    break;

                default:
                    this.GetBranch();
                    break;

            }
        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }
    }
}