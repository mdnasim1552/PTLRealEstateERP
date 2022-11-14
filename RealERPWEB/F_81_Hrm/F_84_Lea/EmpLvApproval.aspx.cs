using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Collections.Generic;
using RealERPLIB;
using RealERPRPT;
using RealEntity;

using System.Net;
using System.Net.Mail;

namespace RealERPWEB.F_81_Hrm.F_84_Lea
{

    public partial class EmpLvApproval : System.Web.UI.Page
    {
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();
        private Hashtable _errObj;


        //public static string Narration = "";
        public static double TAmount = 0;
        ProcessAccess HRData = new ProcessAccess();
        public static int PageNumber = 0;
        // SmsSend SmsApps = new SmsSend();
        List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.LvApproval> lstsalorder = new List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.LvApproval>();
        string Messagesd;
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];

                string qusrid = this.Request.QueryString["usrid"] ?? "";
                if (qusrid.Length > 0  && GetCompCode() !="3367")
                {
                    this.GetComNameAAdd();
                    this.GetUserPermission();
                    this.MasComNameAndAdd();
                }
                else
                {
                    if (hst == null)
                    {
                        string loginUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                         Request.ApplicationPath.TrimEnd('/') + "/LogIn.aspx";

                         Response.Redirect(loginUrl);
                    }
                    int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                    if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                   (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                        Response.Redirect("~/AcceessError.aspx");
                }

                ((Label)this.Master.FindControl("lblTitle")).Text = "Leave Approval";
                string Type = this.Request.QueryString["Type"].ToString();
                string Date = (Type == "Ind") ? this.Request.QueryString["Date"].ToString() : System.DateTime.Today.ToString("dd-MMM-yyyy"); ;
                this.txtdate.Text = Date;

                this.IsApprovalCheck();
                this.GetDptUserCheck();

                // this.CommonButton();
                this.GetProjectName();
                this.GetOrderName();
                //this.PanelHead.Visible = true;
                this.PnlNarration.Visible = true;
                this.ShowData();
                stepForward();
                if (GetCompCode() == "3365" || GetCompCode() == "3102")
                {
                    this.pnlCommon.Visible = true;
                    this.pnlFinly.Visible = false;
                }
                else
                {
                    this.pnlCommon.Visible = false;
                    this.pnlFinly.Visible = true;
                }

                if (GetCompCode()=="3370")
                {
                    this.btnFward.Visible = false;
                }

            }

        }
        private void IsApprovalCheck()
        {
            string comcod = this.GetCompCode();
            string ltrnId = this.Request.QueryString["ltrnid"] ?? "";

            var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GET_LEAVE_APPROVAL", ltrnId, "", "", "", "", "", "", "", "");
            if (ds == null)
            {
                return;
            }
            string aprovDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["aprovdat"]).ToString("dd-MMM-yyyy");
            if (aprovDate != "01-Jan-1900")
            {
                this.divApproval.Visible = true;
                this.levapp.Visible = false;
                return;
            }

        }
        private void GetDptUserCheck()
        {
            string comcod = this.GetCompCode();
            string refno = this.Request.QueryString["refno"] ?? "";
            string RoleType = this.Request.QueryString["RoleType"] ?? "";

            if (RoleType == "SUP")
            {
                RoleType = RoleType == "SUP" ? "DPT" : "";

                var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLEAVEDPTSETUSER", refno, RoleType, "", "", "", "", "", "", "");
                if (ds == null)
                {
                    return;
                }
                string dptdesc = ds.Tables[0].Rows[0]["dptname"].ToString();
                if (dptdesc != "000000000000")
                {
                    this.dptNameset.InnerText = ds.Tables[0].Rows[0]["dptname"].ToString();
                    this.warning.Visible = true;
                    this.levapp.Visible = false;
                    return;
                }
            }

        }
        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Approved";
            ((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkbtnLedger_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler();
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkbtnAdd_Click1);
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Click += new EventHandler(lnkbtnEdit_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(btnUpdate_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Click += new EventHandler(lnkbtnDelete_Click);
            //((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((CheckBox)this.Master.FindControl("chkBoxN")).Checked += new EventHandler(chkBoxN_Click);

        }
        private void UserPermission()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string Userid = hst["usrid"].ToString();
            string Store = this.ddlCenter.SelectedValue.ToString();
            DataSet ds4 = HRData.GetTransInfo(comcod, "SP_ENTRY_SALES_ORDER_APPROVAL", "GETUSERINF", Userid, Store, "", "", "", "", "", "", "");
            if (ds4.Tables[0].Rows.Count == 0)
            {

                Response.Redirect("../AcceessError.aspx");
                return;
            }
        }

        private void Refrsh()
        {

        }
        private string GetCompCode()
        {
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            qcomcod = qcomcod.Length > 0 ? qcomcod : hst["comcod"].ToString();
            return (qcomcod);
        }

        private void stepForward()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3366":
                case "3367":
                case "3368":
                case "3365":
                case "3102":
                    this.chkbod.Visible = false;
                    this.lblforward.Visible = false;
                    break;
                case "3101":
                case "3348":
                    this.chkbod.Visible = true;
                    this.lblforward.Visible = true;
                    break;
            }

        }

        protected void ImgbtnFindProjectName_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        private void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string Type = (this.Request.QueryString["Type"]).ToString();
            string srchproject = (Type == "Ind") ? this.Request.QueryString["refno"].ToString() : ("%%");
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "GETDEPTNAME", srchproject, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            this.ddlCenter.DataTextField = "deptanme";
            this.ddlCenter.DataValueField = "deptid";
            this.ddlCenter.DataSource = ds2.Tables[0];
            this.ddlCenter.DataBind();
            if (Request.QueryString.AllKeys.Contains("refno"))
            {
                this.ddlCenter.SelectedValue = this.Request.QueryString["refno"].ToString();
            }

            if (Type != "Ind")
                this.GetOrderName();

        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {

            this.ShowData();

        }

        private void GetOrderName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string Type = (this.Request.QueryString["Type"]).ToString();
            string comcod = this.GetCompCode();
            string Date = (Type == "Ind") ? this.Request.QueryString["Date"].ToString() : Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            string srchproject = (Type == "Ind") ? this.Request.QueryString["ltrnid"].ToString().Trim() : "%%"; //+ this.txtserchmrf.Text.Trim() 
            string pactcode = this.ddlCenter.SelectedValue.ToString();
            string Usrid = hst["usrid"].ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "GETLVAPPAUT", Date, srchproject, pactcode, Usrid, "", "", "", "", "");
            if (ds2 == null)
                return;

            this.lstOrderNo.DataTextField = "ltrnid2";
            this.lstOrderNo.DataValueField = "ltrnid";
            this.lstOrderNo.DataSource = ds2.Tables[0];
            this.lstOrderNo.DataBind();
            this.lstOrderNo.Focus();
            if (this.lstOrderNo.Items.Count > 0)
                this.lstOrderNo.SelectedIndex = 0;

            this.chkbod.Visible = ds2.Tables[0].Rows.Count == 0 ? false : Convert.ToBoolean(ds2.Tables[0].Rows[0]["tcount"]);
            this.lblforward.Visible = ds2.Tables[0].Rows.Count == 0 ? false : Convert.ToBoolean(ds2.Tables[0].Rows[0]["tcount"]);


        }

        private void GetLeavType(string empid)
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETLEAVETYPE", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tbltype"] = ds1.Tables[0];



        }
        private void ShowData()
        {

            //this.lblmsg.Text = " "; 100000000088
            try
            {
                Session.Remove("tblOrder");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string Date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
                string SrchChequeno = "%%";

                string DeptCode = ((this.ddlCenter.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCenter.SelectedValue.ToString()) + "%";
                //string Approval = this.RateorApproved();
                string Userid = hst["usrid"].ToString();
                string Lvidno = this.lstOrderNo.SelectedValue.ToString();
                DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "SHOWLVSTATUS", Date, SrchChequeno, "", "", DeptCode, Userid, Lvidno, "", "");
                if (ds == null)
                {
                    this.gvLvReq.DataSource = null;
                    this.gvLvReq.DataBind();
                    return;
                }
                var lst = ds.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.LvApproval>();
                ViewState["tblt01"] = lst;
                ViewState["tblempinfo"] = ds.Tables[2];

                //applied informaiton 
                DataTable dt1 = ds.Tables[2];
                DataTable dtstatus = ds.Tables[3];

                string empid = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["empid"].ToString();
                string empUsrID = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["empuserid"].ToString();
                string empEmail = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["empEmail"].ToString();
                string idcard = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["idcard"].ToString();
                string deptName = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["deptanme"].ToString();
                string empdesig = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["desig"].ToString();
                string empname = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["empname"].ToString();
                string servlength = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["servlen"].ToString();
                string denameadesig = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["denameadesig"].ToString();
                this.lblelv.Text = dtstatus.Rows.Count == 0 ? "" : Convert.ToDouble("0" + dtstatus.Rows[0]["upachivelv"]).ToString("#,##0.00;(#,##0.00); ");
                this.lblclv.Text = dtstatus.Rows.Count == 0 ? "" : Convert.ToDouble("0" + dtstatus.Rows[0]["upachivclv"]).ToString("#,##0.00;(#,##0.00); ");
                this.lblslv.Text = dtstatus.Rows.Count == 0 ? "" : Convert.ToDouble("0" + dtstatus.Rows[0]["upachivslv"]).ToString("#,##0.00;(#,##0.00); ");
                this.lblelvenjoy.Text = dtstatus.Rows.Count == 0 ? "" : Convert.ToDouble("0" + dtstatus.Rows[0]["enjenleave"]).ToString("#,##0.00;(#,##0.00); ");
                this.lblclenj.Text = dtstatus.Rows.Count == 0 ? "" : Convert.ToDouble("0" + dtstatus.Rows[0]["enjcleave"]).ToString("#,##0.00;(#,##0.00); ");
                this.lblslenj.Text = dtstatus.Rows.Count == 0 ? "" : Convert.ToDouble("0" + dtstatus.Rows[0]["enjsleave"]).ToString("#,##0.00;(#,##0.00); ");

                this.elvallow.Text = dtstatus.Rows.Count == 0 ? "" : dtstatus.Rows[0]["upachivelv"].ToString();
                this.clvallow.Text = dtstatus.Rows.Count == 0 ? "" : dtstatus.Rows[0]["upachivclv"].ToString();
                this.slvallow.Text = dtstatus.Rows.Count == 0 ? "" : dtstatus.Rows[0]["upachivslv"].ToString();

                this.elvenjoy.Text = dtstatus.Rows.Count == 0 ? "" : dtstatus.Rows[0]["enjenleave"].ToString();
                this.clvenjoy.Text = dtstatus.Rows.Count == 0 ? "" : dtstatus.Rows[0]["enjcleave"].ToString();
                this.slvenjoy.Text = dtstatus.Rows.Count == 0 ? "" : dtstatus.Rows[0]["enjsleave"].ToString();

                this.elvbalanc.Text = dtstatus.Rows.Count == 0 ? "" : dtstatus.Rows[0]["balleleave"].ToString();
                this.clvbalanc.Text = dtstatus.Rows.Count == 0 ? "" : dtstatus.Rows[0]["ballcleave"].ToString();
                this.slvbalanc.Text = dtstatus.Rows.Count == 0 ? "" : dtstatus.Rows[0]["ballsleave"].ToString();


                string elst = dtstatus.Rows.Count == 0 ? "" : dtstatus.Rows[0]["elst"].ToString();
                string clst = dtstatus.Rows.Count == 0 ? "" : dtstatus.Rows[0]["clst"].ToString();
                string slst = dtstatus.Rows.Count == 0 ? "" : dtstatus.Rows[0]["slst"].ToString();
                this.lblelvenjoy.Attributes["class"] = "badge text-white bg-" + elst + "";
                this.lblclenj.Attributes["class"] = "badge text-white bg-" + clst + "";
                this.lblslenj.Attributes["class"] = "badge text-white bg-" + slst + "";


                this.spEmpInfo.InnerText = "Employee ID: " + idcard + "," + "Employee Name : " + empname + "," + "Designation: " + empdesig + "," +
                    "Department Name : " + deptName + "," + "Service Length : " + servlength + " Years";
                this.lblDutesInfo.Text = denameadesig;
                //end head data
                this.ShowEmppLeave(ds.Tables[0].Rows[0]["empid"].ToString());
                this.lblvalNarration.Text = ds.Tables[0].Rows[0]["LREASON"].ToString();
                this.lblRemarks.Text = ds.Tables[0].Rows[0]["LRMARKS"].ToString();
                this.Chboxforward.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["forward"]);

                GetLeavType(empid);
                this.Data_Bind();

                if (ds.Tables[1].Rows.Count == 0)
                    return;
                this.lblRemarks.Text = ds.Tables[1].Rows[0]["usrname"].ToString();
            }
            catch (Exception ex)
            {
                string Messagesd = "Error :" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                return;
            }

        }
        private string ShowEmppLeave(string Empid)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string aplydat = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVESTATUS02", Empid, aplydat, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLeaveStatus.DataSource = null;
                this.gvLeaveStatus.DataBind();
                return "";
            }
            this.gvLeaveStatus.DataSource = ds1.Tables[0];
            this.gvLeaveStatus.DataBind();

            return "";
        }
        protected void lstOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowData();
        }
        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
            //    return;
            //}
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = ((this.ddlCenter.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCenter.SelectedValue.ToString());

            string Orderno = this.lstOrderNo.SelectedValue.ToString();
            if (Orderno.Length == 0)
            {
                ((Label)this.Master.FindControl("lblANMgsBox")).Visible = true;
                ((Label)this.Master.FindControl("lblANMgsBox")).Text = "Please select your item for Delete";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

            bool result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_SALES_ORDER_APPROVAL", "ORDERAPPDELETE", pactcode, Orderno, "", "", "");

            if (result == true)
            {
                ((Label)this.Master.FindControl("lblANMgsBox")).Visible = true;
                ((Label)this.Master.FindControl("lblANMgsBox")).Text = "Successfully Deleted";
                ((Label)this.Master.FindControl("lblANMgsBox")).Attributes["style"] = "background:Green;";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                this.ShowData();
            }
            //this.Data_Bind();
        }
        private void Data_Bind()
        {

            var lst = (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.LvApproval>)ViewState["tblt01"];
            this.gvLvReq.DataSource = lst;
            this.gvLvReq.DataBind();

            DataTable dt = (DataTable)ViewState["tbltype"];
            DropDownList ddlgval;
            for (int i = 0; i < this.gvLvReq.Rows.Count; i++)
            {
                TextBox gcod = (TextBox)this.gvLvReq.Rows[i].FindControl("lblgvhrdesc");
                ddlgval = ((DropDownList)this.gvLvReq.Rows[i].FindControl("ddlLvtype"));
                ddlgval.DataTextField = "hrgdesc";
                ddlgval.DataValueField = "hrgcod";
                ddlgval.DataSource = dt;
                ddlgval.DataBind();
                //ddlgval.SelectedItem.Text = gcod.Text;
                ddlgval.SelectedValue = ((Label)this.gvLvReq.Rows[i].FindControl("lblgvgcod")).Text.Trim();
            }
        }
        //private DataTable HiddenSameDate(DataTable dt1)
        //{
        //    if (dt1.Rows.Count == 0)
        //        return dt1;
        //    if (dt1.Rows.Count == 0)
        //        return dt1;

        //    string centrid = dt1.Rows[0]["centrid"].ToString();
        //    string orderno = dt1.Rows[0]["orderno"].ToString();
        //    string custcode = dt1.Rows[0]["custcode"].ToString();

        //    int j;
        //    for (j = 1; j < dt1.Rows.Count; j++)
        //    {
        //        if (dt1.Rows[0]["centrid"].ToString() == centrid && dt1.Rows[j]["orderno"].ToString() == orderno && dt1.Rows[j]["custcode"].ToString() == custcode)
        //        {

        //            dt1.Rows[j]["orderno1"] = "";
        //            dt1.Rows[j]["refno"] = "";
        //            dt1.Rows[j]["orderdat"] = "";
        //            dt1.Rows[j]["teamdesc"] = "";

        //            dt1.Rows[j]["centrdesc"] = "";
        //            dt1.Rows[j]["custdesc"] = "";
        //            dt1.Rows[j]["limit"] = 0.00;
        //            dt1.Rows[j]["dues"] = 0.00;
        //            //dt1.Rows[j]["rsirunit"] = "";
        //        }
        //        else
        //        {
        //            if (dt1.Rows[j]["orderno"].ToString() == orderno)
        //            {
        //                dt1.Rows[j]["orderno1"] = "";
        //                dt1.Rows[j]["refno"] = "";
        //                dt1.Rows[j]["orderdat"] = "";
        //            }
        //            if (dt1.Rows[j]["centrid"].ToString() == centrid)
        //            {
        //                dt1.Rows[j]["centrdesc"] = "";
        //            }
        //            if (dt1.Rows[j]["custcode"].ToString() == custcode)
        //            {
        //                dt1.Rows[j]["custdesc"] = "";
        //                dt1.Rows[j]["teamdesc"] = "";
        //                //dt1.Rows[j]["rsirunit"] = "";
        //            }
        //        }
        //        centrid = dt1.Rows[j]["centrid"].ToString();
        //        orderno = dt1.Rows[j]["orderno"].ToString();
        //        custcode = dt1.Rows[j]["custcode"].ToString();
        //    }
        //    return dt1;

        //}

        protected void imgbtnSearchCheqNO_Click(object sender, ImageClickEventArgs e)
        {

            this.ShowData();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string comcod = this.GetCompCode();
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comnam = hst["comnam"].ToString();
            //    string compname = hst["compname"].ToString();
            //    string comadd = hst["comadd1"].ToString();
            //    string username = hst["username"].ToString();
            //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //    string orderno = this.lstOrderNo.SelectedValue.ToString();
            //    string centrid = this.ddlCenter.SelectedValue.ToString();

            //    DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_Sales.SP_REPORT_SALES_INFO", "RPTCUSTINFORMATION", orderno, centrid, "", "", "", "", "", "", "");
            //    if (ds2 == null)
            //        return;

            //    ReportDocument rptSOrder = new MFGRPT.R_23_SaM.RptSalOrdrZelta();

            //    TextObject txtrptcomp = rptSOrder.ReportDefinition.ReportObjects["Company"] as TextObject;
            //    txtrptcomp.Text = comnam;

            //    TextObject txtHeader = rptSOrder.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //    txtHeader.Text = "SALES ORDER";

            //    TextObject txtCompAdd = rptSOrder.ReportDefinition.ReportObjects["txtCompAdd"] as TextObject;
            //    txtCompAdd.Text = comadd;

            //    TextObject txtsaledate = rptSOrder.ReportDefinition.ReportObjects["txtsaledate"] as TextObject;
            //    txtsaledate.Text = this.txtdate.Text;

            //    TextObject txtCust = rptSOrder.ReportDefinition.ReportObjects["txtCust"] as TextObject;
            //    txtCust.Text = ds2.Tables[2].Rows[0]["name"].ToString().Trim();

            //    TextObject txtAdd = rptSOrder.ReportDefinition.ReportObjects["txtAdd"] as TextObject;
            //    txtAdd.Text = ds2.Tables[2].Rows[0]["addr"].ToString().Trim();

            //    TextObject txtPhone = rptSOrder.ReportDefinition.ReportObjects["txtPhone"] as TextObject;
            //    txtPhone.Text = ds2.Tables[2].Rows[0]["phone"].ToString().Trim();

            //    TextObject txtTrans = rptSOrder.ReportDefinition.ReportObjects["txtTrans"] as TextObject;
            //    txtTrans.Text = ds2.Tables[0].Rows[0]["courie"].ToString().Trim();

            //    TextObject txtStore = rptSOrder.ReportDefinition.ReportObjects["txtStore"] as TextObject;
            //    txtStore.Text = ds2.Tables[2].Rows[0]["storename"].ToString().Trim();


            //    TextObject txtCode = rptSOrder.ReportDefinition.ReportObjects["txtCode"] as TextObject;
            //    txtCode.Text = ds2.Tables[2].Rows[0]["sirtdes"].ToString().Trim();

            //    TextObject txtOrdTime = rptSOrder.ReportDefinition.ReportObjects["txtOrdTime"] as TextObject;
            //    txtOrdTime.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["posteddat"]).ToString("hh:mm:ss tt").Trim();


            //    TextObject txtsaleNo = rptSOrder.ReportDefinition.ReportObjects["txtsaleNo"] as TextObject;
            //    txtsaleNo.Text = orderno;
            //    //BALANCE 

            //    DataTable dt = ds2.Tables[0];
            //    DataTable dt2 = ds2.Tables[1];
            //    DataTable dt3 = ds2.Tables[2];

            //    double oStdAmt, Dipsamt, ordAmt, balAmt;
            //    oStdAmt = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("sum(dues)", "")) ? 0.00 : dt3.Compute("sum(dues)", "")));
            //    ordAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamount)", "")) ? 0.00 : dt.Compute("sum(tamount)", "")));
            //    Dipsamt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(paidamt)", "")) ? 0.00 : dt2.Compute("sum(paidamt)", "")));

            //    balAmt = (oStdAmt + ordAmt) - Dipsamt;

            //    TextObject txtOutStdBal = rptSOrder.ReportDefinition.ReportObjects["txtOutStdBal"] as TextObject;
            //    txtOutStdBal.Text = oStdAmt.ToString("#,##0.00;(#,##0.00);");

            //    TextObject txtDipositeAmt = rptSOrder.ReportDefinition.ReportObjects["txtDipositeAmt"] as TextObject;
            //    txtDipositeAmt.Text = Dipsamt.ToString("#,##0.00;(#,##0.00);");

            //    TextObject txtOrderAmt = rptSOrder.ReportDefinition.ReportObjects["txtOrderAmt"] as TextObject;
            //    txtOrderAmt.Text = ordAmt.ToString("#,##0.00;(#,##0.00);");

            //    TextObject txtBalanceAmt = rptSOrder.ReportDefinition.ReportObjects["txtBalanceAmt"] as TextObject;
            //    txtBalanceAmt.Text = balAmt.ToString("#,##0.00;(#,##0.00);");

            //    TextObject txtAppby = rptSOrder.ReportDefinition.ReportObjects["txtAppby"] as TextObject;
            //    txtAppby.Text = ds2.Tables[2].Rows[0]["appby"].ToString().Trim();

            //    TextObject txtPreBy = rptSOrder.ReportDefinition.ReportObjects["txtPreBy"] as TextObject;
            //    txtPreBy.Text = ds2.Tables[0].Rows[0]["usrname"].ToString().Trim();

            //    TextObject txtuserinfo = rptSOrder.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //    rptSOrder.SetDataSource(ds2.Tables[0]);

            //    if (ConstantInfo.LogStatus == true)
            //    {
            //        string eventtype = "SALES ORDER";
            //        string eventdesc = "Print Report";
            //        string eventdesc2 = "ORDER: " + this.lstOrderNo.SelectedItem.Text;
            //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //    }

            //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //    rptSOrder.SetParameterValue("ComLogo", ComLogo);

            //    Session["Report1"] = rptSOrder;

            //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //}
            //catch (Exception ex)
            //{

            //}

        }


        protected void ddlCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetOrderName();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        protected void lnkbtnEdit_Click(object sender, EventArgs e)
        {
            string centrid = this.ddlCenter.SelectedValue.ToString();
            string orderno = this.lstOrderNo.SelectedValue.ToString();
            //if (billstatus == "True")
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('This Bill Already Adjusted');", true);
            //}
            //else
            //{
            if (orderno.Length != 0)
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_23_SaM/SalesOrder.aspx?Type=Edit" + "&orderno=" + orderno + "&centrid=" + centrid + "', target='_self');</script>";
            // }
        }

        private void SaveLeave()
        {

            for (int i = 0; i < this.gvLvReq.Rows.Count; i++)
            {
                //TimeSpan ts = (this.CalExt3.SelectedDate.Value - this.CalExt2.SelectedDate.Value);
                double leaveday = Convert.ToDouble("0" + ((TextBox)this.gvLvReq.Rows[i].FindControl("txtgvlapplied")).Text.Trim());

                if (leaveday > 0)
                {

                    string stdat = Convert.ToDateTime(((TextBox)this.gvLvReq.Rows[i].FindControl("txtgvlstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                    string endat = Convert.ToDateTime(((TextBox)this.gvLvReq.Rows[i].FindControl("txtgvlstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                    if (leaveday != 0.5)
                    {
                        endat = Convert.ToDateTime(stdat).AddDays(leaveday - 1).ToString("dd-MMM-yyyy");

                        ((CheckBox)this.gvLvReq.Rows[i].FindControl("ishalfday")).Checked = false;

                    }
                    else
                    {
                        ((CheckBox)this.gvLvReq.Rows[i].FindControl("ishalfday")).Checked = true;

                    }


                    ((Label)this.gvLvReq.Rows[i].FindControl("lblgvenddat")).Text = endat;
                }
            }


        }

        private void LeaveUpdate()
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string trnid = this.lstOrderNo.SelectedValue.ToString();
            string empid = ((Label)this.gvLvReq.Rows[0].FindControl("lblgvempid")).Text.Trim();
            bool result = false;
            string applydat = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            for (int i = 0; i < gvLvReq.Rows.Count; i++)
            {
                double lapplied = Convert.ToDouble("0" + ((TextBox)this.gvLvReq.Rows[i].FindControl("txtgvlapplied")).Text.Trim());
                if (lapplied > 0)
                {
                    string ishalfday = (((CheckBox)gvLvReq.Rows[i].FindControl("ishalfday")).Checked) ? "1" : "0";
                    string lbllevid = ((Label)this.gvLvReq.Rows[i].FindControl("lbllevid")).Text.Trim();
                    string gcod = ((DropDownList)this.gvLvReq.Rows[i].FindControl("ddlLvtype")).SelectedValue.Trim();

                    string frmdate = Convert.ToDateTime(((TextBox)this.gvLvReq.Rows[i].FindControl("txtgvlstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                    string todate = Convert.ToDateTime(((Label)this.gvLvReq.Rows[i].FindControl("lblgvenddat")).Text.Trim()).ToString("dd-MMM-yyyy");
                    string forword = Convert.ToBoolean(this.Chboxforward.Checked).ToString();

                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPEMLEAVAPP02", trnid, empid, gcod, frmdate, todate, applydat, forword, ishalfday, lbllevid, lapplied.ToString(), "", "", "", "", "");

                    if (!result)
                    {
                        Messagesd = "Updated Fail";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                    }
                }

            }
        }

        private void LeaveReset()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string trnid = this.lstOrderNo.SelectedValue.ToString();
            string empid = ((Label)this.gvLvReq.Rows[0].FindControl("lblgvempid")).Text.Trim();
            bool result = false;
            string applydat = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            for (int i = 0; i < gvLvReq.Rows.Count; i++)
            {
                double lapplied = Convert.ToDouble("0" + ((TextBox)this.gvLvReq.Rows[i].FindControl("txtgvlapplied")).Text.Trim());
                if (lapplied > 0)
                {

                    string frmdate = Convert.ToDateTime(((TextBox)this.gvLvReq.Rows[i].FindControl("txtgvlstdate")).Text.Trim()).ToString("yyyyMMdd");
                    string todate = Convert.ToDateTime(((Label)this.gvLvReq.Rows[i].FindControl("lblgvenddat")).Text.Trim()).ToString("yyyyMMdd");


                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "INORUPDATEABSENTCT_RESET", empid, frmdate, todate, "1", "", "", "", "", "", "", "", "", "");

                    if (!result)
                    {
                        Messagesd = "Updated Fail";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                    }
                }
            }
        }

        //protected void ApprovedBtn_Click(object sender, EventArgs e)
        //{

        //    //((Label)this.Master.FindControl("lblmsg")).Visible = true;
        //    //try
        //    //{



        //    //    int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        //    //    if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
        //    //        Response.Redirect("../AcceessError.aspx");
        //    //    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
        //    //    //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
        //    //    if (!Convert.ToBoolean(dr1[0]["entry"]))
        //    //    {
        //    //        ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
        //    //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
        //    //        return;
        //    //    }

        //    //    //this.CheckValue();
        //    //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    //    string comcod = hst["comcod"].ToString();
        //    //    string ApprovByid = hst["usrid"].ToString();
        //    //    string Approvtrmid = hst["compname"].ToString();
        //    //    string ApprovSession = hst["session"].ToString();
        //    //    this.SaveLeave();
        //    //    this.LeaveUpdate();
        //    //    string roletype = this.Request.QueryString["RoleType"].ToString();
        //    //    //string approvdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
        //    //    string approvdat = System.DateTime.Now.ToString("dd-MMM-yyyy");

        //    //    string Centrid = this.ddlCenter.SelectedValue.ToString();
        //    //    //string Type = (this.Request.QueryString["Type"]).ToString();
        //    //    //string Lvidno = (Type == "Ind") ? this.Request.QueryString["ltrnid"].ToString() : this.lstOrderNo.SelectedValue.ToString();
        //    //    string Orderno = this.lstOrderNo.SelectedValue.ToString();
        //    //    string approved = "Ok";
        //    //    string apDate = this.txtdate.Text.ToString();
        //    //    string isForward = Convert.ToBoolean(this.Chboxforward.Checked).ToString();


        //    //    //for (int i = 0; i < dt1.Rows.Count; i++)
        //    //    //{
        //    //    //    string Chk = dt1.Rows[i]["chkmv"].ToString();
        //    //    //    if (Chk == "False")
        //    //    //    {
        //    //    //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Check CheckBox');", true);
        //    //    //        return;
        //    //    //    }
        //    //    //}
        //    //    //--------------------------Check this Order Approved Or Not--------------//

        //    //    DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "LEAVEAPPROVED", Orderno, ApprovByid, Centrid, "", "", "", "", "", "");
        //    //    if (ds4.Tables[0].Rows[0]["approved"].ToString() != "")
        //    //    {

        //    //        ((Label)this.Master.FindControl("lblmsg")).Text = "Order Number Already Approved";
        //    //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
        //    //        return;
        //    //    }

        //    //    //--------------------------Check More then 1 Approval--------------//

        //    //    if (ds4.Tables[1].Rows[0]["chk"].ToString() == "OK")
        //    //    {
        //    //        if (ds4.Tables[2].Rows.Count == 0)
        //    //        {
        //    //            ((Label)this.Master.FindControl("lblmsg")).Text = "Need More then 1 Approval";
        //    //            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

        //    //            return;
        //    //        }

        //    //    }


        //    //    else
        //    //    {
        //    //        // if(comcod!="3365")
        //    //        //  {
        //    //        if (hst["compsms"].ToString() == "True")
        //    //        {
        //    //            this.sendsms();
        //    //        }

        //    //        else if (hst["compmail"].ToString() == "True")
        //    //        {
        //    //            this.sendMail();
        //    //        }
        //    //        //   }



        //    //    }


        //    //    bool result = false;
        //    //    string Orderno1 = "XXXXXXXXXXXXXX";


        //    //    //------------------C Table----------------------//


        //    //    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "UPDATELVAPP", Orderno, ApprovByid, Approvtrmid, ApprovSession, approvdat, Centrid, roletype, "", "", "", "", "", "", "", "");
        //    //    if (result == false)
        //    //    {
        //    //        ((Label)this.Master.FindControl("lblmsg")).Text = "Order Not Approved";
        //    //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
        //    //        return;
        //    //    }



        //    //    if (!this.Chboxforward.Checked)
        //    //    {


        //    //        DataSet ds6 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "CHKFINALAPP", ApprovByid, Centrid, "", "", "", "", "", "", "");

        //    //        //------------------B Table----------------------//
        //    //        if (ds6.Tables[0].Rows.Count != 0)
        //    //        {
        //    //            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "UPDATELVAPPSTATUS", Orderno, apDate, "", "", "", "", "", "", "", "", "", "", "", "", "");

        //    //            if (result == false)
        //    //            {
        //    //                Messagesd = "Order Not Approved";
        //    //                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);


        //    //                return;
        //    //            }
        //    //            if (comcod != "3365")
        //    //            {
        //    //                this.SMSORMAIL();
        //    //            }

        //    //          //  this.SendNotificaion(Orderno, Centrid, roletype, isForward, "", "", "","","","");


        //    //        }


        //    //        if (!result)
        //    //        {
        //    //            Messagesd = HRData.ErrorObject["Msg"].ToString();
        //    //            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);

        //    //            return;
        //    //        }

        //    //    }

        //    //    else
        //    //    {


        //    //        if (this.Request["Type"] == "App")
        //    //        {

        //    //            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "UPDATELVAPPSTATUS", Orderno, apDate, "", "", "", "", "", "", "", "", "", "", "", "", "");

        //    //            if (result == false)
        //    //            {
        //    //                Messagesd = "Order Not Approved";
        //    //                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);

        //    //                return;
        //    //            }

        //    //            if (comcod != "3365")
        //    //            {
        //    //                this.lvconfirmSMS();
        //    //            }

        //    //        }



        //    //    }
        //    //    Messagesd = "Approved";
        //    //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagesd + "');", true);




        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Messagesd = "Error:" + ex.Message;
        //    //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);

        //    //}


        //}


        //private void SMSORMAIL()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    if (hst["compsms"].ToString() == "True")
        //    {
        //        this.lvconfirmSMS();
        //    }

        //    else if (hst["compmail"].ToString() == "True")
        //    {
        //        lvconfirmMail();
        //    }


        //}


        //protected void lvconfirmMail()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string usrid = hst["usrid"].ToString();
        //    string deptcode = hst["deptcode"].ToString();
        //    string username = hst["username"].ToString();
        //    string comcod = this.GetCompCode();

        //    string frmdate = Convert.ToDateTime(((TextBox)this.gvLvReq.Rows[0].FindControl("txtgvlstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
        //    string todate = Convert.ToDateTime(((Label)this.gvLvReq.Rows[0].FindControl("lblgvenddat")).Text.Trim()).ToString("dd-MMM-yyyy");
        //    string empid = ((Label)this.gvLvReq.Rows[0].FindControl("lblgvempid")).Text;

        //    //string empid = this.ddlEmpName.SelectedValue.ToString();
        //    var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPMAIL", empid, "", "", "", "", "", "", "", "");
        //    var ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEPTHEADDETAILS", usrid, "", "", "", "", "", "", "", "");

        //    string empname = (string)ds1.Tables[0].Rows[0]["name"];
        //    string empdesig = (string)ds1.Tables[0].Rows[0]["desig"];
        //    string deptName = (string)ds1.Tables[0].Rows[0]["deptname"];


        //    if (ds == null)
        //        return;

        //    string mail = (string)ds.Tables[0].Rows[0]["mail"];

        //    string maildesc = "Leave Approved From : " + frmdate + " To " + todate + "\n" + "Approved By : " + empname + "\n" + "Designation : " + empdesig + "\n" +
        //    "Department Name : " + deptName;

        //    this.SendMailPass(maildesc, mail);

        //}

        //private void sendMail()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string deptcode = this.ddlCenter.SelectedValue.ToString();
        //    string username = hst["username"].ToString();
        //    string comcod = this.GetCompCode();
        //    string frmdate = Convert.ToDateTime(((TextBox)this.gvLvReq.Rows[0].FindControl("txtgvlstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
        //    string todate = Convert.ToDateTime(((Label)this.gvLvReq.Rows[0].FindControl("lblgvenddat")).Text.Trim()).ToString("dd-MMM-yyyy");
        //    string empname = ((Label)this.gvLvReq.Rows[0].FindControl("lblgvempname")).Text;
        //    string empdesig = ((Label)this.gvLvReq.Rows[0].FindControl("lgdesig")).Text;
        //    string deptName = ((Label)this.gvLvReq.Rows[0].FindControl("lgdeptanme")).Text;
        //    string leavetype = ((Label)this.gvLvReq.Rows[0].FindControl("lglvtype")).Text;
        //    string lapplied = ((TextBox)this.gvLvReq.Rows[0].FindControl("txtgvlapplied")).Text;
        //    string idcard = ((Label)this.gvLvReq.Rows[0].FindControl("lgidcard")).Text;
        //    string lvreason = lblvalNarration.Text;





        //    //string empid = this.ddlEmpName.SelectedValue.ToString();
        //    var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETAPPRVPMAIL", deptcode, "", "", "", "", "", "", "", "");

        //    if (ds == null)
        //        return;

        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        string mail = (string)ds.Tables[0].Rows[i]["mail"];
        //        string maildesc = "Employee ID : " + idcard + " \n" + "Employee Name : " + empname + "\n" + "Designation : " + empdesig + "\n" +
        //        "Department Name : " + deptName + "\n" + "Leave Period : " + frmdate + " To " + todate + "\n" + "Leave Duration : " + lapplied +
        //        "\n" + "Leave Type : " + leavetype + "\n" + "Leave Reason : " + lvreason;
        //        this.SendMailPass(maildesc, mail);
        //        // }
        //    }

        //}


        //public void SendMailPass(string maildesc, string mail)
        //{

        //    //Hashtable hst = (Hashtable)Session["tblLogin"];
        //    //string comcod = this.GetCompCode();
        //    //string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
        //    //DataSet dssmtpandmail = HRData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");

        //    ////SMTP
        //    //string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
        //    //int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
        //    //string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
        //    //string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
        //    //string mailtousr = mail;
        //    ////string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf";

        //    //EASendMail.SmtpMail oMail = new EASendMail.SmtpMail("TryIt");

        //    ////Connection Details 
        //    //SmtpServer oServer = new SmtpServer(hostname);
        //    //oServer.User = frmemail;
        //    //oServer.Password = psssword;
        //    //oServer.Port = portnumber;
        //    //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

        //    ////oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;


        //    //EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
        //    //oMail.From = frmemail;
        //    //oMail.To = mailtousr;
        //    //oMail.Cc = frmemail;
        //    //// oMail.Subject = subject;


        //    //oMail.HtmlBody = "<html><head></head><body><pre style='max-width:700px;text-align:justify; font-weight: bold;font-size: 14px'>" + "<br/>" + maildesc + "</pre></body></html>";
        //    //// oMail.HtmlBody = "<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Dear Sir," + "<br/>" + maildesc + "</pre></body></html>";

        //    //try
        //    //{

        //    //    oSmtp.SendMail(oServer, oMail);

        //    //    Messagesd = "Your message has been successfully sent";
        //    //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagesd + "');", true);

        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Messagesd = "Error occured while sending your message." + ex.Message;
        //    //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);

        //    //}


        //}
        //protected void sendsms()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string deptcode = this.ddlCenter.SelectedValue.ToString();
        //    string username = hst["username"].ToString();
        //    string comcod = this.GetCompCode();

        //    string frmdate = Convert.ToDateTime(((TextBox)this.gvLvReq.Rows[0].FindControl("txtgvlstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
        //    string todate = Convert.ToDateTime(((Label)this.gvLvReq.Rows[0].FindControl("lblgvenddat")).Text.Trim()).ToString("dd-MMM-yyyy");
        //    string empname = ((Label)this.gvLvReq.Rows[0].FindControl("lblgvempname")).Text;
        //    string empdesig = ((Label)this.gvLvReq.Rows[0].FindControl("lgdesig")).Text;

        //    //string empid = this.ddlEmpName.SelectedValue.ToString();
        //    var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETAPPRVPPHONE", deptcode, "", "", "", "", "", "", "", "");

        //    if (ds == null)
        //        return;

        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        string phone = (string)ds.Tables[0].Rows[i]["phone"];
        //        if (hst["compsms"].ToString() == "True")
        //        {
        //            SendSmsProcess sms = new SendSmsProcess();
        //            string comnam = hst["comnam"].ToString();
        //            string compname = hst["compname"].ToString();
        //            // string frmname = "PurReqApproval.aspx?Type=RateInput";

        //            string SMSText = "Leave applied from : " + frmdate + " To " + todate + "\n" + "Name: " + empname + " Designation : " + empdesig + "\n" + "First approved by " + username;
        //            bool resultsms = sms.SendSmmsPwd(comcod, SMSText, phone);
        //        }
        //    }
        //}

        //protected void lvconfirmSMS()
        //{
        //    //Hashtable hst = (Hashtable)Session["tblLogin"];
        //    //string deptcode = hst["deptcode"].ToString();
        //    //string username = hst["username"].ToString();
        //    //string comcod = this.GetCompCode();

        //    //string frmdate = Convert.ToDateTime(((TextBox)this.gvLvReq.Rows[0].FindControl("txtgvlstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
        //    //string todate = Convert.ToDateTime(((Label)this.gvLvReq.Rows[0].FindControl("lblgvenddat")).Text.Trim()).ToString("dd-MMM-yyyy");
        //    //string empid = ((Label)this.gvLvReq.Rows[0].FindControl("lblgvempid")).Text;

        //    ////string empid = this.ddlEmpName.SelectedValue.ToString();
        //    //var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPPHONE", empid, "", "", "", "", "", "", "", "");



        //    //if (ds == null)
        //    //    return;

        //    //string phone = (string)ds.Tables[0].Rows[0]["phone"];
        //    //if (hst["compsms"].ToString() == "True")
        //    //{
        //    //    SendSmsProcess sms = new SendSmsProcess();
        //    //    string comnam = hst["comnam"].ToString();
        //    //    string compname = hst["compname"].ToString();
        //    //    // string frmname = "PurReqApproval.aspx?Type=RateInput";

        //    //    string SMSText = "Leave approved from : " + frmdate + " To " + todate;// 
        //    //    bool resultsms = sms.SendSmmsPwd(comcod, SMSText, phone);
        //    //}
        //}

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveLeave();
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string compName = hst["comnam"].ToString();

                DataTable dt = (DataTable)ViewState["tblempinfo"];


                string empUsrID = dt.Rows.Count == 0 ? "" : dt.Rows[0]["empuserid"].ToString();
                string empEmail = dt.Rows.Count == 0 ? "" : dt.Rows[0]["empEmail"].ToString();
                string idcard = dt.Rows.Count == 0 ? "" : dt.Rows[0]["idcard"].ToString();

                string to_empname = dt.Rows.Count == 0 ? "" : dt.Rows[0]["empname"].ToString();
                string leavedesc = dt.Rows.Count == 0 ? "" : dt.Rows[0]["lvtype"].ToString();



                string trnid = this.lstOrderNo.SelectedValue.ToString();
                string remarks = this.txtremarks.Text;

                if (remarks.Length == 0)
                {
                    string Messagesd = "Please Fill remarks";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                    return;
                }


                // reset if delete abs table data

                this.LeaveReset();

                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMLEAVAPP_ALL", trnid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    string Messagesd = "Deleted failed";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                    return;

                }




                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert(' Not Apporved');", true);

                if (hst["compsms"].ToString() == "True")
                {
                    string empid = ((Label)this.gvLvReq.Rows[0].FindControl("lblgvempid")).Text;
                    string canname = hst["username"].ToString(); ;
                    //string empid = this.ddlEmpName.SelectedValue.ToString();
                    var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPPHONE", empid, "", "", "", "", "", "", "", "");

                    if (ds == null)
                        return;
                    string phone = (string)ds.Tables[0].Rows[0]["phone"];
                    SendSmsProcess sms = new SendSmsProcess();
                    string SMSText = "Leave Canceled by : " + canname; // 
                    bool resultsms = sms.SendSmmsPwd(SMSText, SMSText, phone);

                }

                else if (hst["compmail"].ToString() == "True")

                {

                    string usrid = hst["usrid"].ToString();
                    string deptcode = hst["deptcode"].ToString();
                    string username = hst["username"].ToString();
                    string frmdate = Convert.ToDateTime(((TextBox)this.gvLvReq.Rows[0].FindControl("txtgvlstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                    string todate = Convert.ToDateTime(((Label)this.gvLvReq.Rows[0].FindControl("lblgvenddat")).Text.Trim()).ToString("dd-MMM-yyyy");
                    string empid = ((Label)this.gvLvReq.Rows[0].FindControl("lblgvempid")).Text;

                    //string empid = this.ddlEmpName.SelectedValue.ToString();
                    var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPMAIL", empid, "", "", "", "", "", "", "", "");
                    var ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEPTHEADDETAILS", usrid, "", "", "", "", "", "", "", "");

                    string empname = (string)ds1.Tables[0].Rows[0]["name"];
                    string empdesig = (string)ds1.Tables[0].Rows[0]["desig"];
                    string deptName = (string)ds1.Tables[0].Rows[0]["deptname"];
                    // string t = (string)ds1.Tables[0].Rows[0]["deptname"];
                    if (ds == null)
                        return;

                    ///GET SMTP AND SMS API INFORMATION
                    #region
                    //SMTP
                    DataSet dssmtpandmail = HRData.GetTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");
                    string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
                    int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
                    string frmemail = dssmtpandmail.Tables[0].Rows[0]["mailid"].ToString();
                    string psssword = dssmtpandmail.Tables[0].Rows[0]["mailpass"].ToString();
                    bool isSSL = Convert.ToBoolean(dssmtpandmail.Tables[0].Rows[0]["issl"].ToString());

                    #endregion

                    string mail = (string)ds.Tables[0].Rows[0]["mail"];
                    string toEmpsub = "Leave Request Canceled";
                    string toMSgBody = "Dear " + to_empname + ",\n" + " Reason : " + remarks + "\n" + ", Leave Canceled By : " + empname + ", Designation " + empdesig + ", Department Name" + deptName + "\n";
                    bool Result_email = UserNotify.SendEmailPTL(hostname, portnumber, frmemail, psssword, toEmpsub, empname, empdesig, deptName, compName, mail, toMSgBody, isSSL);

                    bool result2 = UserNotify.SendNotification(toEmpsub, toMSgBody, empUsrID);
                }

                string Messagessd = "Deleted Success";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagessd + "');", true);
                ShowData();
            }
            catch (Exception ex)
            {
                string Messagessd = "Something Wrong !!" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagessd + "');", true);
            }


        }

        protected void lnkIntsLvDelete_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            var lst = (List<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.LvApproval>)ViewState["tblt01"];


            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string trnid = ((Label)this.gvLvReq.Rows[RowIndex].FindControl("lbllevid")).Text.Trim();
            string lvid = ((Label)this.gvLvReq.Rows[RowIndex].FindControl("lgvltrnleaveid")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMLEAVAPP", lvid, trnid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                string Messaged = "Deleted Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                return;
            }
            string Messagesd = "Deleted Success";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagesd + "');", true);


            int rowindex = (this.gvLvReq.PageSize) * (this.gvLvReq.PageIndex) + RowIndex;
            lst.RemoveAt(rowindex);

            ViewState["tblt01"] = lst;
            this.gvLvReq.DataSource = lst;
            this.gvLvReq.DataBind();

        }

        protected void lbtnnotapproved_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_test_Click(object sender, EventArgs e)
        {
            try
            {
                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ////DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //if (!Convert.ToBoolean(dr1[0]["entry"]))
                //{
                //    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                //    return;
                //}


                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

                int index = row.RowIndex + 1;

                string lvtype = ((Label)this.gvLvReq.Rows[index].FindControl("lglvtype")).Text.ToString();
                string aplydat = ((Label)this.gvLvReq.Rows[index].FindControl("lblgvaplydat")).Text.ToString();
                string duration = ((TextBox)this.gvLvReq.Rows[index].FindControl("txtgvlapplied")).Text.ToString();

                string strtdat = ((TextBox)this.gvLvReq.Rows[index].FindControl("txtgvlstdate")).Text.ToString();
                string endat = ((Label)this.gvLvReq.Rows[index].FindControl("lblgvenddat")).Text.ToString();



                //this.CheckValue();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string compsms = hst["compsms"].ToString();
                string compmail = hst["compmail"].ToString();
                string ssl = hst["ssl"].ToString();
                //Sender Informaiton
                string comcod = hst["comcod"].ToString();
                string sendUsername = hst["userfname"].ToString();

                string sendDptdesc = hst["dptdesc"].ToString();
                string sendUsrdesig = hst["usrdesig"].ToString();
                string compName = hst["comnam"].ToString();


                string ApprovByid = hst["usrid"].ToString();
                string Approvtrmid = hst["compname"].ToString();
                string ApprovSession = hst["session"].ToString();
                this.SaveLeave();
                string isForward = Convert.ToBoolean(this.Chboxforward.Checked).ToString();


                string roletype = this.Request.QueryString["RoleType"].ToString();
                string approvdat = System.DateTime.Now.ToString("dd-MMM-yyyy");
                string Centrid = this.ddlCenter.SelectedValue.ToString();
                string Orderno = this.lstOrderNo.SelectedValue.ToString();
                bool result = false;
                string apDate = this.txtdate.Text.ToString();
                string htmtableboyd = "";

                DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "GETCEHCKAPPROVALBYID", Orderno, roletype, Centrid, "", "", "", "", "", "");
                if (ds4.Tables[0].Rows.Count != 0)
                {
                    string Messagesd = "Leave Already Approved";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                    return;
                }
                else
                {
                    if (roletype == "DPT")
                    {
                        this.LeaveUpdate();

                    }
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "UPDATELVAPP", Orderno, ApprovByid, Approvtrmid, ApprovSession, approvdat, Centrid, roletype, isForward, "", "", "", "", "", "", "");
                    if (result == false)
                    {
                        string Messagesd = "Leave Approved Fail";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                        return;
                    }
                    else
                    {

                        if (compmail == "true")
                        {
                            htmtableboyd = "<table style='border: 1px solid black;border-collapse: collapse;'>" +
                          "<tr>" +
                           "<th style='border-collapse: collapse;border: 1px solid black;'>From Date</th>" +
                           "<th style='border-collapse: collapse;border: 1px solid black;'>To Date</th>" +
                           "<th style='border-collapse: collapse;border: 1px solid black;'>Days</th>" +
                          "</tr>";


                            htmtableboyd += "<tr>" +
                                "<td style='border: 1px solid black;border-collapse: collapse;'>" + strtdat + "</td>" +
                                "<td style='border: 1px solid black;border-collapse: collapse;'>" + endat + "</td>" +
                                "<td style='border: 1px solid black;border-collapse: collapse;'>(" + duration.Remove(1, 3) + ") day</td>" +
                                "</tr>";
                            htmtableboyd += "</table>";

                            this.SendNotificaion(Orderno, Centrid, roletype, isForward, compsms, compmail, ssl, sendUsername, sendDptdesc, sendUsrdesig, compName, htmtableboyd);
                        }

                        string Messagesd = "Leave Approved";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagesd + "');", true);

                        string eventdesc2 = "Details: " + sendUsername + sendDptdesc + sendUsrdesig + compName;
                        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), Messagesd, Messagesd, eventdesc2);

                    }
                }

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Leave Approved";
                    string eventdesc = "Leave Approved";
                    string eventdesc2 = Orderno;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }
            catch (Exception ex)
            {
                string Messagesd = "Something Wrong !!" + ex.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                return;
            }
        }

        private void SendNotificaion(string ltrnid, string deptcode, string roletype, string isForward, string compsms, string compmail, string ssl, string sendUsername, string sendDptdesc, string sendUsrdesig, string compName, string htmtableboyd)
        {
            try
            {

                string comcod = this.GetCompCode();
                string frmdate = Convert.ToDateTime(((TextBox)this.gvLvReq.Rows[0].FindControl("txtgvlstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(((Label)this.gvLvReq.Rows[0].FindControl("lblgvenddat")).Text.Trim()).ToString("dd-MMM-yyyy");
                DataTable dt = (DataTable)ViewState["tblempinfo"];
                string supapp = "Your leave request has been approved by the Supervisor, please waiting for Department/Section Head approval.";
                string dptapp = "Your leave request has been approved by the Department/Section Head.";

                string empUsrID = dt.Rows.Count == 0 ? "" : dt.Rows[0]["empuserid"].ToString();
                string empEmail = dt.Rows.Count == 0 ? "" : dt.Rows[0]["empEmail"].ToString();
                //string  empEmail = "inforakib831@gmail.com";
                string idcard = dt.Rows.Count == 0 ? "" : dt.Rows[0]["idcard"].ToString();
                string deptName = dt.Rows.Count == 0 ? "" : dt.Rows[0]["deptanme"].ToString();
                string empdesig = dt.Rows.Count == 0 ? "" : dt.Rows[0]["desig"].ToString();
                string empname = dt.Rows.Count == 0 ? "" : dt.Rows[0]["empname"].ToString();
                string leavedesc = dt.Rows.Count == 0 ? "" : dt.Rows[0]["lvtype"].ToString();



                ///GET SMTP AND SMS API INFORMATION
                #region
                string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
                DataSet dssmtpandmail = HRData.GetTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");

                //SMTP
                string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
                int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
                string frmemail = dssmtpandmail.Tables[0].Rows[0]["mailid"].ToString();
                string psssword = dssmtpandmail.Tables[0].Rows[0]["mailpass"].ToString();
                bool isSSL = Convert.ToBoolean(dssmtpandmail.Tables[0].Rows[0]["issl"].ToString());

                #endregion




                string roletypeCHk = (roletype == "SUP") ? "DPT" : "DPT";// MGT now Removed, Pls discused wiht nahid
                var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETAPPRVPMAIL", deptcode, roletypeCHk, "", "", "", "", "", "", "");
                if (ds == null)
                {
                    string Messagesd = "Leave Approved";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                    return;
                }
                // var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "HRAPPROVAL_DPT_HEAD_USERID", deptcode, roletypeCHk, "", "", "", "", "", "", "");
                if (ds == null)
                    return;

                #region

                string subj = "New Leave Request";



                #endregion

                #region
                // User profile notifcation 
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string appusrid = ds.Tables[0].Rows[i]["usrid"].ToString();
                    string phone = ds.Tables[0].Rows[i]["phone"].ToString();
                    string tomail = ds.Tables[0].Rows[i]["mail"].ToString();
                    //string tomail = "inforakib831@gmail.com";
                    string isrole = (roletype == "SUP" ? "DPT" :
                                    roletype == "DPT" ? "MGT" : "MGT");

                    string uhostname = "https://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_81_Hrm/F_84_Lea/";
                    string currentptah = "EmpLvApproval.aspx?Type=Ind&comcod=" + comcod + "&refno=" + deptcode + "&ltrnid=" + ltrnid + "&Date=" + frmdate + "&usrid = " + appusrid + "&RoleType=" + isrole;
                    string totalpath = uhostname + currentptah;

                    string maildescription = "Dear Sir, Please Approve Leave Request." + "<br> Employee ID Card : " + idcard + ",<br>" + "Employee Name : " + empname + ",<br>" + "Designation : " + empdesig + "," + "<br>" +
                      "Department Name : " + deptName + "," + "<br>" + "Leave Type : " + leavedesc + ",<br>" + " Request id: " + ltrnid + ". <br>" + htmtableboyd;
                    maildescription += "<div style='color:red'><a style='color:blue; text-decoration:underline' href = '" + totalpath + "'>Click for Approved</a> or Login ERP Software and check Leave Interface</div>" + "<br/>";


                    string msgbody = maildescription;

                    bool result2 = UserNotify.SendNotification(subj, msgbody, appusrid);

                    if (compsms == "True")
                    {
                        SendSmsProcess sms = new SendSmsProcess();
                        string SMSText = "Leave approved from : " + frmdate + " To " + todate;// 
                        bool resultsms = sms.SendSmmsPwd(comcod, SMSText, phone);
                    }
                    if (compmail == "True")
                    {
                        bool Result_email = UserNotify.SendEmailPTL(hostname, portnumber, frmemail, psssword, subj, sendUsername, sendUsrdesig, sendDptdesc, compName, tomail, msgbody, isSSL);
                        if (Result_email == false)
                        {
                            string Messagesd = "Leave Approved, Notification did not send";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                        }
                    }
                }


                // for applied user send notification
                string toMSgBody = (roletype == "SUP" ? supapp :
                                    roletype == "DPT" ? dptapp : "Your leave request has been approved");


                toMSgBody = "Hi !! ," + toMSgBody + "<br> Employee ID Card : " + idcard + ",<br>" + "Employee Name : " + empname + ",<br>" + "Designation : " + empdesig + "," + "<br>" +
                   "Department Name : " + deptName + "," + "<br>" + "Leave Type : " + leavedesc + ",<br>" + " Request id: " + ltrnid + ". <br>" + htmtableboyd;


                string toEmpsub = "Leave Request Approved";
                bool result3 = UserNotify.SendNotification(toEmpsub, toMSgBody, empUsrID);
                if (result3 == false)
                {
                    string Messagesd = "Leave Approved, Notification did not send";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                }
                //end user profile notifcaion
                #endregion

                /// SMS and EMail SEND 

                #region
                if (compsms == "True")
                {
                    // bool result2 = UserNotify.SendNotification(eventdesc, eventdesc2, appusrid);

                }
                if (compmail == "True")
                {
                    // bool result2 = UserNotify.SendNotification(eventdesc, eventdesc2, appusrid);
                    bool Result_email = UserNotify.SendEmailPTL(hostname, portnumber, frmemail, psssword, toEmpsub, sendUsername, sendUsrdesig, sendDptdesc, compName, empEmail, toMSgBody, isSSL);

                }
                #endregion


            }
            catch (Exception ex)
            {
                string Messagesd = "Leave Approved, Notification did not send " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
            }

        }



        private void GetComNameAAdd()
        {
            string comcod = this.GetCompCode();
            //Access Database (List View)
            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetNameAdd();

            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = ("comcod = '" + comcod + "'");
            DataTable dt = dv.ToTable();
            Session["tbllog"] = dt;
            ds1.Dispose();


        }
        private void GetUserPermission()
        {
            string comcod = this.GetCompCode();

            string usrid = this.Request.QueryString["usrid"];
            string HostAddress = Request.UserHostAddress.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSERNAMEAPASS", usrid, "", "", "", "", "", "", "", "");

            //  if()

            //  ProcessAccess ulogin = (ASTUtility.Left(this.ddlCompany.SelectedValue.ToString(), 1) == "4") ? new ProcessAccess() : new ProcessAccess();

            string username = ds1.Tables[0].Rows[0]["username"].ToString();
            string pass = ds1.Tables[0].Rows[0]["password"].ToString();

            //string decodepass = ASTUtility.EncodePassword(pass);

            //        string pass = ASTUtility.EncodePassword(hst["password"].ToString());
            string modulid = "AA";
            string modulename = "All Module";
            DataSet ds5 = HRData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSER", username, pass, modulid, modulename, "", "", "", "", "");
            DataSet dsmenu = HRData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSER_NAHID", username, pass, modulid, modulename, "", "", "", "", "");

            Session["tblusrlog"] = ds5;
            Session["dsmenu"] = dsmenu.Tables[1];

            DataTable dt1 = (DataTable)Session["tbllog"];
            DataTable dt2 = new DataTable();

            //if ((DataTable)Session["tbllog1"] == null)
            // {
            dt2.Columns.Add("comcod", Type.GetType("System.String"));
            dt2.Columns.Add("comnam", Type.GetType("System.String"));
            dt2.Columns.Add("comsnam", Type.GetType("System.String"));
            dt2.Columns.Add("comadd1", Type.GetType("System.String"));
            dt2.Columns.Add("comadd", Type.GetType("System.String"));
            dt2.Columns.Add("usrsname", Type.GetType("System.String"));
            dt2.Columns.Add("session", Type.GetType("System.String"));
            dt2.Columns.Add("compsms", Type.GetType("System.String"));
            dt2.Columns.Add("compmail", Type.GetType("System.String"));

            Session["tbllog1"] = dt2;
            // }

            DataRow[] dr = dt1.Select("comcod='" + comcod + "'");
            // Hashtable hst = (Hashtable)Session["tblLogin"];
            Hashtable hst = new Hashtable();

            if (dr.Length > 0)
            {

                hst["comnam"] = dr[0]["comnam"];
                hst["comnam"] = dr[0]["comnam"];
                hst["comsnam"] = dr[0]["comsnam"];
                hst["comadd1"] = dr[0]["comadd1"];
                hst["comweb"] = dr[0]["comadd3"];
                hst["combranch"] = dr[0]["combranch"];
                hst["comadd"] = dr[0]["comadd"];


                DataRow dr2 = dt2.NewRow();
                dr2["comcod"] = comcod;
                dr2["comnam"] = dr[0]["comnam"];
                dr2["comsnam"] = dr[0]["comsnam"];
                dr2["comadd1"] = dr[0]["comadd1"];
                dr2["comadd"] = dr[0]["comadd"];

                dt2.Rows.Add(dr2);

            }
            string sessionid = (ASTUtility.RandNumber(111111, 999999)).ToString();
            hst["comcod"] = comcod;
            hst["deptcode"] = ds5.Tables[0].Rows[0]["deptcode"];
            hst["dptdesc"] = ds5.Tables[0].Rows[0]["dptdesc"];

            // hst["comnam"] = ComName;
            hst["modulenam"] = "";
            hst["username"] = ds5.Tables[0].Rows[0]["usrsname"];
            hst["userfname"] = ds5.Tables[0].Rows[0]["usrname"];
            hst["compname"] = HostAddress;
            hst["usrid"] = ds5.Tables[0].Rows[0]["usrid"];
            hst["password"] = pass;
            hst["session"] = sessionid;
            hst["trmid"] = "";
            hst["commod"] = "1";
            hst["compsms"] = ds5.Tables[0].Rows[0]["compsms"];
            hst["ssl"] = ds5.Tables[0].Rows[0]["ssl"];
            hst["opndate"] = ds5.Tables[0].Rows[0]["opndate"];
            hst["empid"] = ds5.Tables[0].Rows[0]["empid"];
            hst["teamid"] = ds5.Tables[0].Rows[0]["teamid"];
            hst["mcomcod"] = ds5.Tables[5].Rows[0]["mcomcod"];
            hst["usrdesig"] = ds5.Tables[0].Rows[0]["usrdesig"];
            hst["events"] = ds5.Tables[0].Rows[0]["eventspanel"];
            hst["usrrmrk"] = ds5.Tables[0].Rows[0]["usrrmrk"];
            hst["userrole"] = ds5.Tables[0].Rows[0]["userrole"];
            hst["compmail"] = ds5.Tables[0].Rows[0]["compmail"];
            hst["userimg"] = ds5.Tables[0].Rows[0]["imgurl"];
            hst["ddldesc"] = ds5.Tables[0].Rows[0]["ddldesc"];
            if (ds5.Tables[0].Columns.Contains("comunpost"))
            {
                hst["comunpost"] = ds5.Tables[0].Rows[0]["comunpost"];
            }
            else
            {
                hst["comunpost"] = "0";
            }

            if (ds5.Tables[0].Columns.Contains("homeurl"))
            {
                hst["homeurl"] = ds5.Tables[0].Rows[0]["homeurl"];
            }
            else
            {
                hst["homeurl"] = "UserProfile";
            }



            Session["tblLogin"] = hst;
            dt2.Rows[0]["usrsname"] = ds5.Tables[0].Rows[0]["usrsname"];
            dt2.Rows[0]["session"] = sessionid;
            Session["tbllog1"] = dt2;
        }

        private void MasComNameAndAdd()
        {
            //((Image)this.Master.FindControl("ComLogo")).ImageUrl = "";
            string comcod = this.GetCompCode();
            DataTable dt1 = ((DataTable)Session["tbllog"]);
            DataRow[] dr = dt1.Select("comcod='" + comcod + "'");
            DataTable dt = ((DataTable)Session["tbllog1"]);
            dt.Rows[0]["comcod"] = comcod;
            Session["tbllog1"] = dt;
            ((Label)this.Master.FindControl("LblGrpCompany")).Text = ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();
            //((Label)this.Master.FindControl("lbladd")).Text = (dr[0]
        }

        protected void gvLvReq_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}

