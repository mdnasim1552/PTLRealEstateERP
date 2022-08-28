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
    public partial class InterfaceLeavApp : System.Web.UI.Page
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
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect(this.ResolveUrl("~/AcceessError.aspx"));
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "LEAVE INTERFACE";//
                this.SelectDate();
                this.GetLeaveType();
                this.RadioButtonList1.SelectedIndex = 0;
                this.pnlInt.Visible = true;
                this.GetStep();
                this.SaleRequRpt();
                this.RadioButtonList1_SelectedIndexChanged(null, null);
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        private void SelectDate()
        {
            string comcod = this.GetCompCode();
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
                return;
            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            //this.txFdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
            //this.txFdate.Text = startdate + this.txFdate.Text.Trim().Substring(2);
            //this.txtdate.Text = Convert.ToDateTime(this.txFdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

            switch (comcod)
            {
                case "3330":
                case "3355":
                case "3365":
                
                    this.txFdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txFdate.Text = startdate + this.txFdate.Text.Trim().Substring(2);
                    this.txtdate.Text = Convert.ToDateTime(this.txFdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;
                default:
                    this.txFdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txFdate.Text = startdate + this.txFdate.Text.Trim().Substring(2);
                    this.txtdate.Text = Convert.ToDateTime(this.txFdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;
            }
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

        private void GetLeaveType()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETGENLEAVETYPE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddleavetype.DataTextField = "hrgdesc";
            this.ddleavetype.DataValueField = "hrgcod";
            this.ddleavetype.DataSource = ds1.Tables[0];
            this.ddleavetype.DataBind();
            this.ddleavetype.Items.Insert(0, new ListItem("--Select Leave Type--", ""));
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
            ViewState.Remove("tbltotalleav");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = hst["usrid"].ToString();// (this.Request.QueryString["Type"] == "Ind") || (this.Request.QueryString["Type"] == "DeptHead") ? hst["usrid"].ToString() : "";
            string fDate = Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy");
            string tDate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            string type = "";//(this.Request.QueryString["Type"]) == "Ind" || (this.Request.QueryString["Type"] == "DeptHead") ? "" : "Management";
            string DeptHead = "";//(this.Request.QueryString["Type"]) == "DeptHead" ? "DeptHead" : "";
            string searchkey = "%"+this.txtSearch.Text.Trim()+"%";

            DataSet ds1 = accData.GetTransInfo(comcod, "DBO_HRM.SP_REPORT_HR_MGT_INTERFACE", "GETLEAVEREQUEST", fDate, tDate, usrid, type, DeptHead, searchkey, "", "", "");
            if (ds1 == null)
                return;

            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading deep-sky-blue counter'>" + ds1.Tables[1].Rows[0]["tcount"].ToString() + "</div></a><div class='circle-tile-content deep-sky-blue'><div class='circle-tile-description txt-white'>Leave Request</div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ds1.Tables[1].Rows[0]["reqcount"].ToString() +"</div></a><div class='circle-tile-content purple'><div class='circle-tile-description txt-white'>Leave Process</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading deep-pink counter'>" + ds1.Tables[1].Rows[0]["appcount"].ToString() +  "</div></a><div class='circle-tile-content deep-pink'><div class='circle-tile-description txt-white'>Leave Approval</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading deep-green counter'>" + ds1.Tables[1].Rows[0]["fappcount"].ToString() + "</div></a><div class='circle-tile-content orange'><div class='circle-tile-description txt-white'>Final Approval</div></div></div>";
            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + ds1.Tables[1].Rows[0]["tappcount"].ToString() + "</div></a><div class='circle-tile-content deep-green'><div class='circle-tile-description txt-white'>Leave Confirmed</div></div></div>";

            // All Order
            DataTable dt = new DataTable();

            DataView dv = new DataView();
            dt = ((DataTable)ds1.Tables[0]).Copy();
            ViewState["tbltotalleav"] = dt;
            this.Data_Bind("gvLvReq", dt);

            //In-process
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            //dv.RowFilter = ("sostatus = 'In-process' or  sostatus = 'Request' ");
            dv.RowFilter = ("supstatus='' and lvstatus <> 'Approved' ");
            this.Data_Bind("gvInprocess", dv.ToTable());

            //Approved
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("dptstatus = '' and  supstatus<>''  and lvstatus <> 'Approved'");
            //dv.RowFilter = ("sostatus = 'Approved' or sostatus = 'In-process' ");
            this.Data_Bind("gvApproved", dv.ToTable());

            //Final Approved
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("lvstatus = 'Final Approved'");
            //dv.RowFilter = ("sostatus = 'Approved' or sostatus = 'In-process' ");
            this.Data_Bind("gvfiApproved", dv.ToTable());

            //Confirm
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("lvstatus = 'Approved' ");
            //dv.RowFilter = ("sostatus = 'Approved' or sostatus = 'In-process' ");
            this.Data_Bind("gvConfirm", dv.ToTable());
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
                    this.pnlFApp.Visible = true;
                    this.PnlConfrm.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    break;

                case "4":
                    this.pnlallReq.Visible = false;
                    this.PnlProcess.Visible = false;
                    this.PnlApp.Visible = false;
                    this.pnlFApp.Visible = false;
                    this.PnlConfrm.Visible = true;
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    break;
            }
        }
        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "gvLvReq":
                    this.gvLvReq.DataSource = (dt);
                    this.gvLvReq.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    Session["Report1"] = gvLvReq;
                    ((HyperLink)this.gvLvReq.HeaderRow.FindControl("hlbtntbCdataExelSP2")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
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

                string mgtusid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mgtusid")).ToString().Trim();

                string refno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string suserid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "susrid")).ToString();
                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();
                string aplydat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "aplydat")).ToString("dd-MMM-yyyy");

                if (userid == dptusrid)
                {
                    hlink3.Visible = false;
                    lnkbtnDptApp.Visible = true;
                    lnkbtnDptApp.NavigateUrl = "~/F_81_Hrm/F_84_Lea/EmpLvApproval.aspx?Type=Ind&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + aplydat + "&RoleType=DPT";
                }
                if (userid == suserid)
                {
                    lnkbtnDptApp.Visible = false;
                    hlink3.Visible = true;
                    hlink3.NavigateUrl = "~/F_81_Hrm/F_84_Lea/EmpLvApproval.aspx?Type=Ind&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + aplydat + "&RoleType=SUP";
                }
                if ((userid == suserid) && (userid == dptusrid))
                {
                    lnkbtnDptApp.Visible = true;
                    hlink3.Visible = false;
                    lnkbtnDptApp.NavigateUrl = "~/F_81_Hrm/F_84_Lea/EmpLvApproval.aspx?Type=Ind&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + aplydat + "&RoleType=DPT";
                }
                if ((userid == mgtusid))
                {
                    lnkbtnDptApp.Visible = true;
                    hlink3.Visible = false;
                    lnkbtnDptApp.NavigateUrl = "~/F_81_Hrm/F_84_Lea/EmpLvApproval.aspx?Type=Ind&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + aplydat + "&RoleType=DPT";
                }

                hlnDel.Visible = (userid == empusrid) ? true : false;
                hlnEdit.Visible = (userid == empusrid) ? true : false;

                hlnEdit.NavigateUrl = "~/F_81_Hrm/F_84_Lea/MyLeave.aspx?Type=User&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid;
                hlink1.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/PrintLeaveInterface.aspx?Type=ApplyPrint&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid;
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
                string mgtusid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mgtusid")).ToString();
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();
                string lvstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lvstatus")).ToString();

                hlink3.Visible = (((userid == dptusid) || (userid == mgtusid))  && (lvstatus != "Approved")) ? true : false;
                hlink3.NavigateUrl = "~/F_81_Hrm/F_84_Lea/EmpLvApproval.aspx?Type=Ind&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + aplydat + "&RoleType=DPT";
                hlink1.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/PrintLeaveInterface.aspx?Type=ApplyPrint&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid;

            }
        }
        protected void gvLvReq_RowDataBound(object sender, GridViewRowEventArgs e)
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


                hlnEdit.Visible = ((usrid == empusrid) && (lvstatus != "Approved")) ? true : false;


                hlnEdit.NavigateUrl = "~/F_81_Hrm/F_84_Lea/MyLeave.aspx?Type=User&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid;
                hlink1.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/PrintLeaveInterface.aspx?Type=ApplyPrint&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid;
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
                hlink1.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/PrintLeaveInterface.aspx?Type=ApplyPrint&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid;
            }
        }
        protected void gvfiApproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DataTable dte = (DataTable)Session["tblmaproved"];
                //DataTable dt = (DataTable)Session["tbleaproved"];
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnAppfi");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string usrid = hst["usrid"].ToString();
                string refno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string urefno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();
                string aplydat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "aplydat")).ToString("dd-MMM-yyyy");
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
                hlink3.NavigateUrl = "~/F_81_Hrm/F_84_Lea/EmpLvApproval.aspx?Type=App&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + aplydat + "&RoleType=MGT";
            }
        }
        protected void lnkRemove_Click(object sender, EventArgs e)
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
            string empid = ((Label)this.gvInprocess.Rows[index].FindControl("lblgvempid")).Text.ToString();
            string leavid = ((Label)this.gvInprocess.Rows[index].FindControl("lblLeavId")).Text.ToString();

            DataTable dt = (DataTable)ViewState["tbltotalleav"];
            bool result = accData.UpdateTransInfo(comcod, "DBO_HRM.SP_REPORT_HR_INTERFACE", "DELETELEAVEINFO", leavid, empid, usrid, "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {
                string Messaged = "Leave deleted successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messaged + "');", true);

                int ins = this.gvInprocess.PageSize * this.gvInprocess.PageIndex + index;
                dt.Rows[ins].Delete();
                ViewState.Remove("tbltotalleav");
                DataView dv = dt.DefaultView;
                ViewState["tbltotalleav"] = dv.ToTable();


                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Leave Requset Delete";
                    string eventdesc = "Leave Requset Delete";
                    string eventdesc2 = leavid;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + accData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }
            this.SaleRequRpt();
        }

        protected void lnkRemoveForward_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "You have no permission" + "');", true);
                return;
            }
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            int index = row.RowIndex;
            string empid = ((Label)this.gvConfirm.Rows[index].FindControl("lblgvempid")).Text.ToString();
            string leavid = ((Label)this.gvConfirm.Rows[index].FindControl("lblLeavId")).Text.ToString();
            string lvdptuid = ((Label)this.gvConfirm.Rows[index].FindControl("lbldptusid")).Text.ToString();
            string lvstatdat = ((Label)this.gvConfirm.Rows[index].FindControl("lblgvstrtdat")).Text.ToString();
            string lvenddat = ((Label)this.gvConfirm.Rows[index].FindControl("lblgvenddat")).Text.ToString();
            DataTable dt = (DataTable)ViewState["tbltotalleav"];

            bool result = accData.UpdateTransInfo(comcod, "DBO_HRM.SP_REPORT_HR_INTERFACE", "LEVAAPPFROWARD", leavid, empid, lvdptuid, usrid, "", "", "", "", "", "", "", "", "", "");
            if (result)
            {
                string Messaged = "Leave Forward Remove successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messaged + "');", true);

                int ins = this.gvInprocess.PageSize * this.gvInprocess.PageIndex + index;
                dt.Rows[ins].Delete();
                ViewState.Remove("tbltotalleav");
                DataView dv = dt.DefaultView;
                ViewState["tbltotalleav"] = dv.ToTable();


                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Leave Requset Forward";
                    string eventdesc = "Leave Requset Forward";
                    string eventdesc2 = leavid;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + accData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }
            this.SaleRequRpt();
            this.LeaveReset(leavid, empid, lvstatdat, lvenddat);
        }
        protected void lnkRemoveApp_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "You have no permission" + "');", true);
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
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Data deleted successfully" + "');", true);
                int ins = this.gvApproved.PageSize * this.gvApproved.PageIndex + index;
                dt.Rows[ins].Delete();
                ViewState.Remove("tbltotalleav");
                DataView dv = dt.DefaultView;
                ViewState["tbltotalleav"] = dv.ToTable();

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Leave Requset Delete";
                    string eventdesc = "Leave Requset Delete, Employe id" + empid;
                    string eventdesc2 = leavid;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }
            this.SaleRequRpt();
        }
        protected void lnkRemoveFAp_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "You have no permission" + "');", true);
                return;
            }
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            int index = row.RowIndex;
            string empid = ((Label)this.gvfiApproved.Rows[index].FindControl("lblgvempid")).Text.ToString();
            string leavid = ((Label)this.gvfiApproved.Rows[index].FindControl("lblLeavId")).Text.ToString();
            //ViewState["tbltotalleav"] = dt;
            DataTable dt = (DataTable)ViewState["tbltotalleav"];
            bool result = accData.UpdateTransInfo(comcod, "DBO_HRM.SP_REPORT_HR_INTERFACE", "DELETELEAVEINFO", leavid, empid, usrid, "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data deleted successfully')", true);
                int ins = this.gvfiApproved.PageSize * this.gvfiApproved.PageIndex + index;
                dt.Rows[ins].Delete();
                ViewState.Remove("tbltotalleav");
                DataView dv = dt.DefaultView;
                ViewState["tbltotalleav"] = dv.ToTable();

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Leave Requset Delete";
                    string eventdesc = "Leave Requset Delete, Employe id" + empid;
                    string eventdesc2 = leavid;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + accData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }
            this.SaleRequRpt();
        }

        private void LeaveReset(string leavid, string empidd, string lvstatdat, string lvenddat)
        {
            string comcod = this.GetCompCode();
            string trnid = leavid;
            string empid = empidd;
            bool result = false;
            string applydat = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string frmdate = Convert.ToDateTime(lvstatdat).ToString("yyyyMMdd");
            string todate = Convert.ToDateTime(lvenddat).ToString("yyyyMMdd");
            result = accData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "INORUPDATEABSENTCT_RESET", empid, frmdate, todate, "1", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                string Messagesd = "Updated Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
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
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EInterfaceLeave>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptInterfaceLeave", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comname));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Task Info Dept"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}