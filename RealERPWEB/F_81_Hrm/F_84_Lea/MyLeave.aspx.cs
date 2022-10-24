using EASendMail;
using Microsoft.Reporting.WinForms;
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

namespace RealERPWEB.F_81_Hrm.F_84_Lea
{

    public partial class ApplyLeave : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();
        private Hashtable _errObj;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtgvenjoydt2_CalendarExtender.StartDate = Convert.ToDateTime(this.txtgvenjoydt1.Text);
                string reqdate = this.Request.QueryString["LevDay"] ?? "";
                if (reqdate.Length > 0)
                {
                    this.txtaplydate.Text = Convert.ToDateTime(reqdate).ToString("dd-MMM-yyyy");
                    string nextday = Convert.ToDateTime(reqdate).ToString("dd-MMM-yyyy");
                    this.txtgvenjoydt1.Text = nextday;
                    this.txtgvenjoydt2.Text = nextday;
                }
                else
                {
                    string nextday = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtgvenjoydt1.Text = nextday;
                    this.txtgvenjoydt2.Text = nextday;
                    this.txtaplydate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                }
                string qtype = this.Request.QueryString["Type"] ?? "";

                if (qtype == "MGT")
                {
                    int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                            (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                        Response.Redirect("~/AcceessError.aspx");
                    ((Label)this.Master.FindControl("lblTitle")).Text = "APPLY LEAVE (MGT)";
                    this.empMgt.Visible = true;
                    GetEmpLoyee();
                    getVisibilty();
                    // GetSupvisorCheck();
                    // this.ddlEmpName_SelectedIndexChanged(null, null);
                }
                else if (qtype == "COMMON")
                {
                    int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                            (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                        Response.Redirect("~/AcceessError.aspx");
                    ((Label)this.Master.FindControl("lblTitle")).Text = "APPLY LEAVE (COMMON)";

                    this.empMgt.Visible = true;
                    GetEmpLoyee();
                    // GetSupvisorCheck();
                    this.ddlEmpName_SelectedIndexChanged(null, null);
                }
                else
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "APPLY LEAVE";
                    CreateTable();
                    GetLeavType();
                    GetSupvisorCheck();
                    getVisibilty();
                    this.EmpLeaveInfo();
                    this.ShowEmppLeave();
                    GetCalCulateDay();
                }
                GetEmpLoyeeAltDutys();

                Hashtable hst2 = (Hashtable)Session["tblLogin"];
                string usrRole = hst2["userrole"].ToString();


                if (this.GetComeCode() == "3365" && qtype!="MGT")
                {

                    DisabledCalender();
                }

            }
        }

        private void DisabledCalender()
        {



            string curDay = System.DateTime.Now.ToString("dd");

            string curMonth = System.DateTime.Now.ToString("MM");
            string curYear = System.DateTime.Now.ToString("yyyy");

            string prevMonth = System.DateTime.Now.AddMonths(-1).ToString("MM");
            string prevYear = System.DateTime.Now.AddMonths(-1).ToString("yyyy");

            string nextMonth = System.DateTime.Now.AddMonths(1).ToString("MM");
            string nextYear = System.DateTime.Now.AddMonths(1).ToString("yyyy");


            if (Convert.ToInt32(curDay) < 27)
            {
                this.txtgvenjoydt1_CalendarExtender.StartDate = DateTime.ParseExact(26 + "/" + prevMonth + "/" + prevYear, "dd/MM/yyyy", null);
                this.txtgvenjoydt1_CalendarExtender.EndDate = DateTime.ParseExact(27 + "/" + curMonth + "/" + curYear, "dd/MM/yyyy", null);

            }
            else
            {
                this.txtgvenjoydt1_CalendarExtender.StartDate = DateTime.ParseExact(26 + "/" + curMonth + "/" + curYear, "dd/MM/yyyy", null);
                this.txtgvenjoydt1_CalendarExtender.EndDate = DateTime.ParseExact(27 + "/" + nextMonth + "/" + nextYear, "dd/MM/yyyy", null);
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        private void GetSupvisorCheck()
        {
            try
            {
                string comcod = this.GetComeCode();
                string empid = this.GetEmpID();
                var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETSUPERVISERMAIL", empid, "", "", "", "", "", "", "", "");
                if (ds == null)
                {
                    this.Lvform.Visible = false;
                    this.warning.Visible = true;
                    return;
                }
                if (ds.Tables[0].Rows[0]["sempid"].ToString() == "000000000000")
                {
                    this.Lvform.Visible = false;
                    this.warning.Visible = true;
                    this.btnSave.Enabled = false;
                    return;
                }
                else
                {
                    this.Lvform.Visible = true;
                    this.warning.Visible = false;
                }
            }
            catch (Exception ex)
            {
                string Messaged = "Oops!! " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
            }
        }
        private void GetEmpLoyee()
        {
            //ddlEmpName.ClearSelection();
            //this.ddlEmpName.SelectedValue = string.Empty;
            //ddlEmpName.SelectedIndex = -1;
            //ddlEmpName.Items.Insert(0, new ListItem("", ""));
            string comcod = this.GetComeCode();
            string empid = this.Request.QueryString["Empid"] ?? "";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTWSEMPNAME", "94%", "%%", "%%", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds1.Tables[0];
            this.ddlEmpName.DataBind();

            this.ddlDutyEmp.DataTextField = "empname";
            this.ddlDutyEmp.DataValueField = "empid";
            this.ddlDutyEmp.DataSource = ds1.Tables[0];
            this.ddlDutyEmp.DataBind();

            if (empid != "")
            {
                this.ddlEmpName.SelectedValue = empid;
            }
        }

        private void GetEmpLoyeeAltDutys()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTWSEMPNAME", "94%", "%%", "%%", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlDutyEmp.DataTextField = "empname";
            this.ddlDutyEmp.DataValueField = "empid";
            this.ddlDutyEmp.DataSource = ds1.Tables[0];
            this.ddlDutyEmp.DataBind();
        }
        private void GetEmpLoyeeResign()
        {
            string comcod = this.GetComeCode();
            string empid = this.Request.QueryString["Empid"] ?? "";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTWSEMPNAME", "94%", "%%", "%%", "resign", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds1.Tables[0];
            this.ddlEmpName.DataBind();
        }
        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            getVisibilty();
            CreateTable();
            this.EmpLeaveInfo();
            this.ShowEmppLeave();
            GetLeavType();
            GetCalCulateDay();
        }
        private void getVisibilty()
        {
            string qtype = this.Request.QueryString["Type"] ?? "";
            string comcod = this.GetComeCode();
            if (comcod == "3365" || comcod == "3354" || comcod == "3101" || comcod=="3102")
            {
                this.sspnlv.Visible = true;

                this.chkBoxSkippWH.Checked = true;
                chkBoxSkippWH_CheckedChanged(null, null);
                // this part for BTI Resign Employee show
                if ((comcod == "3365" || comcod == "3101") && qtype == "MGT")
                {
                    this.SpResign.Visible = true;
                }
                else
                {
                    this.SpResign.Visible = false;

                }
            }
            else
            {
                this.sspnlv.Visible = false;
                this.SpResign.Visible = false;
                this.chkBoxSkippWH.Checked = false;
            }
        }
        private void CreateTable()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("leavday", Type.GetType("System.DateTime"));
            tblt01.Columns.Add("isHalfday", Type.GetType("System.String"));
            ViewState["tblSlevDay"] = tblt01;
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetLeavType()
        {
            string comcod = this.GetComeCode();
            string empid = this.GetEmpID();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETLEAVETYPE", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlLvType.DataTextField = "hrgdesc";
            this.ddlLvType.DataValueField = "hrgcod";
            this.ddlLvType.DataSource = ds1.Tables[0];
            this.ddlLvType.DataBind();
            this.ddlLvType.Items.Insert(0, new ListItem("--Select Leave Type--", ""));
        }

        private void GetCalCulateDay()
        {
            try
            {
                string comcod = this.GetComeCode();
                string gcod = this.ddlLvType.SelectedValue.ToString();
                if (gcod == "")
                {
                    this.btnSave.Enabled = false;
                    return;
                }
                else if (gcod == "51999") // altarnative leav apply
                {
                    DateTime fdate = Convert.ToDateTime(this.txtgvenjoydt1.Text);
                    DateTime tdate = Convert.ToDateTime(this.txtgvenjoydt2.Text);
                    getLevExitingLv(fdate.ToString(), fdate.ToString());
                    DataTable extlv = (DataTable)ViewState["tblextlv"];
                    if (extlv.Rows.Count == 0)
                    {
                        //getLevExitingHoliday(tdate.ToString(), tdate.ToString());
                        //DataTable extHoliday = (DataTable)ViewState["tblextHoliday"];
                        //if (extHoliday.Rows.Count != 0)
                        //{
                        //    string Messaged = "Oops!! This Date is not  a Holiday:  " + extHoliday.Rows[0]["REASON"];
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                        //    this.btnSave.Enabled = false;
                        //}
                        this.Duration.Value = "1";
                        this.btnSave.Enabled = true;
                    }
                    else
                    {
                        string Messaged = "Oops!! Already applied for leave within date range";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                        this.btnSave.Enabled = false;
                    }


                }
                else if (gcod == "51005") // without pay leave condiiton 
                {
                    DataTable dt = (DataTable)Session["tblleavest"];
                    DataTable dt1 = (DataTable)ViewState["tblSlevDay"];
                    DateTime fdate = Convert.ToDateTime(this.txtgvenjoydt1.Text);
                    DateTime tdate = Convert.ToDateTime(this.txtgvenjoydt2.Text);
                    getLevExitingLv(fdate.ToString(), tdate.ToString());
                    DataTable extlv = (DataTable)ViewState["tblextlv"];
                    if (extlv.Rows.Count == 0)
                    {

                        double isHalfday = (this.chkHalfDay.Checked ? 0.5 : 0.00);
                        TimeSpan difference = (tdate - fdate); //create TimeSpan object
                        string diffdays = "0.00";
                        if (chkBoxSkippWH.Checked == false)
                        {
                            isHalfday = (this.CheckBox1.Checked ? 0.5 : 0.00);
                            if (difference.Days == 0 && isHalfday == 0.5)
                            {
                                diffdays = (difference.Days + isHalfday).ToString();
                            }
                            else if (difference.Days != 0 && isHalfday == 0.5)
                            {
                                diffdays = (difference.Days + isHalfday).ToString();
                            }
                            else
                            {
                                diffdays = (difference.Days + isHalfday + 1).ToString();
                            }
                        }
                        else
                        {
                            int skpday = dt1.Rows.Count;
                            if (skpday == 1 && isHalfday == 0.5)
                            {
                                diffdays = (skpday - isHalfday).ToString();
                            }
                            else if (skpday != 0 && isHalfday == 0.5)
                            {
                                diffdays = (skpday + isHalfday).ToString();
                            }
                            else
                            {
                                diffdays = (skpday + isHalfday).ToString();
                            }
                        }
                        //DataView dv = dt.Copy().DefaultView;
                        //dv.RowFilter = ("gcod=" + gcod);
                        //dt = dv.ToTable();
                        //double ballv = Convert.ToDouble(dt.Rows[0]["balleave"]);
                        //double dfdays = Convert.ToDouble(diffdays);
                        this.Duration.Value = diffdays;
                        this.btnSave.Enabled = true;
                    }
                    else
                    {
                        string Messaged = "Oops!! Already applied for leave within date range";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                        this.btnSave.Enabled = false;
                    }
                }

                else
                {
                    DataTable dt = (DataTable)Session["tblleavest"];
                    DataTable dt1 = (DataTable)ViewState["tblSlevDay"];
                    DateTime fdate = Convert.ToDateTime(this.txtgvenjoydt1.Text);
                    DateTime tdate = Convert.ToDateTime(this.txtgvenjoydt2.Text);
                    getLevExitingLv(fdate.ToString(), tdate.ToString());
                    DataTable extlv = (DataTable)ViewState["tblextlv"];
                    if (extlv.Rows.Count == 0)
                    {
                        double isHalfday = (this.chkHalfDay.Checked ? 0.5 : 0.00);
                        TimeSpan difference = (tdate - fdate); //create TimeSpan object
                        string diffdays = "0.00";
                        if (chkBoxSkippWH.Checked == false)
                        {
                            //isHalfday = (this.CheckBox1.Checked ? 0.5 : 0.00);
                            isHalfday = (this.chkHalfDay.Checked ? 0.5 : 0.00);
                            if (difference.Days == 0 && isHalfday == 0.5)
                            {
                                diffdays = (difference.Days + isHalfday).ToString();
                            }
                            else if (difference.Days != 0 && isHalfday == 0.5)
                            {
                                diffdays = (difference.Days + isHalfday).ToString();
                            }
                            else
                            {
                                diffdays = (difference.Days + isHalfday + 1).ToString();
                            }
                        }
                        else
                        {
                            int skpday = dt1.Rows.Count;
                            if (skpday == 1 && isHalfday == 0.5)
                            {
                                diffdays = (skpday - isHalfday).ToString();
                            }
                            else if (skpday != 0 && isHalfday == 0.5)
                            {
                                diffdays = (skpday + isHalfday).ToString();
                            }
                            else
                            {
                                diffdays = (skpday + isHalfday).ToString();
                            }
                        }
                        if (gcod == "51002" && comcod == "3368" && Convert.ToDouble(diffdays) > 3)
                        {
                            string Messaged = "Oops!! Casual leave cannot be taken for more than 3 days at a time";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                            this.btnSave.Enabled = false;
                            return;
                        }

                        DataView dv = dt.Copy().DefaultView;
                        dv.RowFilter = ("gcod=" + gcod);
                        dt = dv.ToTable();
                        double ballv = Convert.ToDouble(dt.Rows[0]["balleave"]);
                        double dfdays = Convert.ToDouble(diffdays);
                        this.Duration.Value = diffdays;

                        if (comcod != "3330")
                        {
                            if (dfdays > ballv && gcod != "51005")
                            {
                                string Messaged = "Oops!! Insufficient Leave Balance, please conctact with your Managment";
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                                this.btnSave.Enabled = false;
                            }
                            else
                            {
                                this.btnSave.Enabled = true;
                            }
                        }
                        else
                        {
                            this.btnSave.Enabled = true;
                        }

                    }
                    else
                    {
                        string Messaged = "Oops!! Already applied for leave within date range";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                        this.btnSave.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                string Messaged = "Oops!! " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                this.btnSave.Enabled = false;
            }
        }

        private void getLevExitingLv(string fdate, string tdate)
        {
            string comcod = this.GetComeCode();
            string empid = this.GetEmpID();
            string fdate1 = Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy");
            string tdate1 = Convert.ToDateTime(tdate).ToString("dd-MMM-yyyy");
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "GETEXITINGELEAVEBYEMPID", empid, fdate1, tdate1, "", "", "", "", "", "");
            ViewState["tblextlv"] = ds5.Tables[0];
        }
        private void getLevExitingHoliday(string fdate, string tdate)
        {
            string comcod = this.GetComeCode();
            string empid = this.GetEmpID();
            string fdate1 = Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy");
            string tdate1 = Convert.ToDateTime(tdate).ToString("dd-MMM-yyyy");

            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "GET_EXITINGE_HOLIDAY_BYEMPID", empid, fdate1, tdate1, "", "", "", "", "", "");

            ViewState["tblextHoliday"] = ds5.Tables[0];
        }
        protected void txtgvenjoydt1_TextChanged1(object sender, EventArgs e)
        {

            try
            {
                string gcod = this.ddlLvType.SelectedValue.ToString();
                string comcod = this.GetComeCode();
                DateTime fdate = Convert.ToDateTime(this.txtgvenjoydt1.Text);
                string nextday = DateTime.Now.AddDays(+1).ToString("dd-MMM-yyyy");

                if (gcod == "")
                {
                    string Messaged = "Please Select Leave Type";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);

                    this.txtgvenjoydt1.Text = nextday;
                    this.txtgvenjoydt2.Text = nextday;

                    this.btnSave.Enabled = false;
                    return;
                }
                else if (gcod == "51999")
                {

                    //txtgvenjoydt2_CalendarExtender.StartDate = DateTime.Now.AddMonths(1);

                }
                else
                {
                    nextday = fdate.AddDays(+0).ToString("dd-MMM-yyyy");
                    this.txtgvenjoydt2.Text = nextday;
                    txtgvenjoydt2_CalendarExtender.StartDate = fdate;
                }

                if (chkBoxSkippWH.Checked == true)
                {
                    bool isvalidate = true;
                    if (comcod == "3365" || comcod == "3354" || comcod == "3101")
                    {
                        getLevExitingHoliday(fdate.ToString(), fdate.ToString());
                        DataTable extHoliday = (DataTable)ViewState["tblextHoliday"];
                        if (extHoliday.Rows.Count != 0)
                        {
                            this.txtgvenjoydt1.Text = nextday;
                            this.txtgvenjoydt2.Text = nextday;

                            string Messaged = "Oops!! This Date is already a Holiday:  " + extHoliday.Rows[0]["REASON"];
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                            this.btnSave.Enabled = false;
                            isvalidate = false;
                        }
                    }
                    if (isvalidate == false)
                        return;
                    seLvDate();
                }
                GetCalCulateDay();
            }
            catch (Exception ex)
            {
                string Messaged = "Oops!! " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                this.btnSave.Enabled = false;
            }

        }

        protected void txtgvenjoydt2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string gcod = this.ddlLvType.SelectedValue.ToString();

                if (gcod == "")
                {
                    string Messaged = "Please Select Leave Type";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);

                    this.btnSave.Enabled = false;
                    return;
                }
                else if (gcod == "51999")
                {
                    // this.txtgvenjoydt1.Text = nextday;
                    // this.txtgvenjoydt2.Text = nextday;
                }
                else
                {
                    DateTime fdate = Convert.ToDateTime(this.txtgvenjoydt1.Text);
                    //string nextday = fdate.AddDays(+0).ToString("dd-MMM-yyyy");
                    //this.txtgvenjoydt2.Text = nextday;
                    txtgvenjoydt2_CalendarExtender.StartDate = fdate;
                }

                GetCalCulateDay();
            }
            catch (Exception ex)
            {
                string Messaged = "Oops!! " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                this.btnSave.Enabled = false;
            }

        }

        protected void chkHalfDay_CheckedChanged(object sender, EventArgs e)
        {
            GetCalCulateDay();

        }
        private string GetLeaveid()
        {

            string comcod = this.GetComeCode();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLEAVEID", "", "", "", "", "", "", "", "", "");
            string lstid = ds5.Tables[0].Rows[0]["ltrnid"].ToString().Trim();
            return lstid;
        }
        public string GetEmpID()
        {
            string Empid = "";
            string qtype = this.Request.QueryString["Type"] ?? "";
            if ((qtype == "MGT") || (qtype == "COMMON"))
            {
                Empid = this.ddlEmpName.SelectedValue.ToString();

            }
            else
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                Empid = (hst["empid"].ToString() == "") ? "" : hst["empid"].ToString();

            }
            return (Empid);

        }


        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
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

                string usrid = hst["usrid"].ToString();
                string deptcode = hst["deptcode"].ToString();

                string trnid = this.GetLeaveid();
                string empid = this.GetEmpID();
                string htmtableboyd = "";
                DataTable dt1 = (DataTable)ViewState["tblSlevDay"];
                string isHalfday = (this.chkHalfDay.Checked ? "True" : "False");
                string ttdays = this.Duration.Value.ToString();
                string qtype = this.Request.QueryString["Type"] ?? "";

                if (ttdays != "0")
                {
                    string gcod = this.ddlLvType.SelectedValue.ToString();
                    string frmdate = Convert.ToDateTime(this.txtgvenjoydt1.Text).ToString("dd-MMM-yyyy");
                    string todate = Convert.ToDateTime(this.txtgvenjoydt2.Text).ToString("dd-MMM-yyyy");
                    string applydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
                    string reason = this.txtLeavLreasons.Text.Trim(); ;
                    string addentime = this.txtaddofenjoytime.Text.Trim();
                    string remarks = this.txtLeavRemarks.Text.Trim();
                    string onDutiesEmp = this.ddlDutyEmp.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDutyEmp.SelectedItem.ToString() + ", ";
                    string dnameadesig = onDutiesEmp + this.txtdutiesnameandDesig.Text.Trim();
                    string delegationEMPID = this.ddlDutyEmp.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDutyEmp.SelectedValue.ToString();
                    string APRdate = (qtype == "MGT" ? applydat : "");

                    bool result = false;
                    //below code for if apply without date range 
                    if (chkBoxSkippWH.Checked == true)
                    {
                        htmtableboyd = "<p><br>Applied Leave:<p><table class='courses-table'><tr class='green'><th>Date</th><th>Days</th></tr>";
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            frmdate = Convert.ToDateTime(dt1.Rows[j]["leavday"]).ToString("dd-MMM-yyyy");
                            isHalfday = dt1.Rows[j]["isHalfday"].ToString();
                            ttdays = (dt1.Rows[j]["isHalfday"].ToString() == "True") ? "0.5" : "1.00";
                            result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPEMLEAVAPP_SKIPPHOLIDAY", trnid, empid, gcod, frmdate, frmdate, applydat, reason, remarks, APRdate, addentime, dnameadesig, ttdays, isHalfday, usrid, qtype, delegationEMPID, "", "", "", "", "");

                            htmtableboyd += "<tr><td>" + frmdate + "</td><td>(" + ttdays + ") Day</td></tr>";
                        }
                        htmtableboyd += "</table>";

                        ViewState.Remove("tblSlevDay");
                        gvInterstLev.DataSource = null;
                        gvInterstLev.DataBind();

                    }
                    else
                    {
                        htmtableboyd = "<table><tr><th>From Date</th><th>To Date</th><th>Days</th></tr>";

                        result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPEMLEAVAPP", trnid, empid, gcod, frmdate, todate, applydat, reason, remarks, APRdate, addentime, dnameadesig, ttdays.ToString(), isHalfday, usrid, qtype, delegationEMPID, "", "", "", "", "");
                        htmtableboyd += "<tr><td>" + frmdate + "<td><td>" + todate + "</td><td>(" + ttdays.ToString() + ") day</td></tr>";
                        htmtableboyd += "</table>";
                    }

                    if (!result)
                    {
                        string Messaged = "Something wrong!! Please check your Apply Data";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                        return;
                    }
                    else
                    {
                        string Messaged = "Successfully applied for leave, please wait for approval";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messaged + "');", true);
                        if (qtype != "MGT")
                        {
                            this.SendNotificaion(frmdate, todate, trnid, deptcode, compsms, compmail, ssl, compName, htmtableboyd);

                        }

                        string eventdesc2 = "Details: " + htmtableboyd;
                        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "New Leave Request", htmtableboyd, Messaged);

                        CreateTable();
                        GetLeavType();
                        GetSupvisorCheck();
                        getVisibilty();
                        this.EmpLeaveInfo();
                        this.ShowEmppLeave();
                        GetCalCulateDay();
                        GetEmpLoyeeAltDutys();
                    }

                    this.EmpLeaveInfo();
                    this.ShowEmppLeave();
                    this.txtLeavLreasons.Text = "";
                    this.txtdutiesnameandDesig.Text = "";
                    this.chkBoxSkippWH.Checked = false;
                    chkBoxSkippWH_CheckedChanged(null, null);

                }

            }
            catch (Exception ex)
            {

                string Messaged = "Error occured while sending your message." + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
            }


        }


        private void SendNotificaion(string frmdate, string todate, string ltrnid, string deptcode, string compsms, string compmail, string ssl, string compName, string htmtableboyd)
        {
            try
            {

                string comcod = this.GetComeCode();
                DataTable dt = (DataTable)ViewState["tblempinfo"];
                string leavedesc = this.ddlLvType.SelectedItem.ToString();
                string empid = this.GetEmpID();
                string delgationemp = this.ddlDutyEmp.SelectedValue.ToString();

                ///GET SMTP AND SMS API INFORMATION
                #region
                string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
                DataSet dssmtpandmail = HRData.GetTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");
                if (dssmtpandmail == null)
                    return;
                //SMTP
                string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
                int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
                string frmemail = dssmtpandmail.Tables[0].Rows[0]["mailid"].ToString();
                string psssword = dssmtpandmail.Tables[0].Rows[0]["mailpass"].ToString();
                bool isSSL = Convert.ToBoolean(dssmtpandmail.Tables[0].Rows[0]["issl"].ToString());
                #endregion

                string callType = "GETSUPERVISERMAIL";
                if ((comcod == "3368" || comcod == "3101"))
                {
                    callType = "GETDELEGATIONEMPEMAIL";
                }
                else
                {
                    callType = "GETSUPERVISERMAIL";
                }


                var ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", callType, empid, delgationemp, ltrnid, "", "", "", "", "", "");

                if (ds1 == null)
                    return;
                string supphone = "";
                string idcard = (string)ds1.Tables[1].Rows[0]["idcard"];
                string empname = (string)ds1.Tables[1].Rows[0]["name"];
                string empdesig = (string)ds1.Tables[1].Rows[0]["desig"];
                string deptname = (string)ds1.Tables[1].Rows[0]["deptname"];



                string hyptext =  "http://";

                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                {
                    string suserid = ds1.Tables[0].Rows[0]["suserid"].ToString();
                    string tomail = ds1.Tables[0].Rows[0]["mail"].ToString();
                    string roletype = (string)ds1.Tables[0].Rows[0]["roletype"];
                    string uhostname = hyptext + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_81_Hrm/F_84_Lea/";
                    string currentptah = "EmpLvApproval?Type=Ind&comcod=" + comcod + "&refno=" + deptcode + "&ltrnid=" + ltrnid + "&Date=" + frmdate + "&usrid=" + suserid + "&RoleType=" + roletype;
                    string totalpath = uhostname + currentptah;


                    //string maildescription = "Dear Sir, Please Approve My Leave Request." + "<br> Employee ID Card : " + idcard + ",<br>" + "Employee Name : " + empname + ",<br>" + "Designation : " + empdesig + "," + "<br>" +
                    //     "Department Name : " + deptname + "," + "<br>" + "Leave Type : " + leavedesc + ",<br>" + " Request id: " + ltrnid + ". <br>";
                    //maildescription += htmtableboyd;
                    //maildescription += "<div style='color:red'><a style='color:blue; text-decoration:underline' href = '" + totalpath + "'>Click for Approved</a> or Login ERP Software and check Leave Interface</div>" + "<br/>";













                    string maildescription = @"
<html lang=""en"">
	<head>	
		<meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">
		<title>
			Today Work Details
		</title>
		<style type=""text/css"">
			HTML{background-color: #e8e8e8;}
			.courses-table{font-size: 12px; padding: 3px; border-collapse: collapse; border-spacing: 0;}
			.courses-table .description{color: #505050;}
			.courses-table td{border: 1px solid #D1D1D1; background-color: #F3F3F3; padding: 0 10px;}
			.courses-table th{border: 1px solid #424242; color: #FFFFFF;text-align: left; padding: 0 10px;}
			.green{background-color: #6B9852;}
.badge-success {
    color: #fff;
    background-color: #44cf9c;
}
.badge-pink {
    color: #fff;
    background-color: #f672a7;
}
.badge-warning {
    color: #fff;
    background-color: #fcc015;
}
.badge-info {
    color: #fff;
    background-color: #43bee1;
}
.text-danger {
    color:red;
    font-weight:bold;
}
.badge-danger {
    color: #fff;
    background-color: #f672a7;
}
.badge-success {
    color: #fff;
    background-color: #44cf9c;
}
.badge {
    display: inline-block;
    padding: 0.25em 0.4em;
    font-size: 75%;
    font-weight: 700;
    line-height: 1;
    text-align: center;
    white-space: nowrap;
    vertical-align: baseline;
    border-radius: 0.25rem;
    transition: color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out;
}
		</style>
	</head>
	<body><p>Dear Sir, Please Approve My Leave Request.</p>
        <table class=""courses-table"">
			<thead>
				<tr>
					<th class=""green"">Sl#</th>					 
					<th class=""green"">Card# </th>
					<th class=""green"">Employee Name</th>					
					<th class=""green"">Designation </th>
					<th class=""green"">Department Name</th>
					<th class=""green"">Leave Type</th>
					<th class=""green"">Request Id</th>                  		 
				</tr>
			</thead>
			<tbody><tr><td>1</td><td>"
         + idcard
         + "</td><td>"
         + empname
         + "</td><td>"
         + empdesig
         + "</td><td>"
         + deptname
         + "</td><td>"
         + leavedesc
         + "</td><td>"
         + ltrnid
         + "</td><tr>"
         + "</tbody></table>" +
         "" + htmtableboyd +
         "<div style='color:red'><br><a style='color:blue; text-decoration:underline' href = '" + totalpath + "'>Click for Approved</a> or Login ERP Software and check Leave Interface</div>" + "<br/>" +
        "</body></html>";


                    #region
                    string subj = "New Leave Request";
                    string msgbody = maildescription;
                    bool result2 = UserNotify.SendNotification(subj, msgbody, suserid);
                    if (compsms == "True")
                    {
                        SendSmsProcess sms = new SendSmsProcess();
                        string SMSText = "New Leave Request from : " + frmdate + " To " + todate;// 
                        bool resultsms = sms.SendSmmsPwd(comcod, SMSText, supphone);
                    }
                        if (compmail == "True")
                    {
                        bool Result_email = UserNotify.SendEmailPTL(hostname, portnumber, frmemail, psssword, subj, empname, empdesig, deptname, compName, tomail, msgbody, isSSL);
                        if (Result_email == false)
                        {
                            string Messagesd = "Leave Applied but Notification has not been sent, Email or SMTP info Empty";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                        }
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                string Messagesd = "Leave Applied but Notification has not been sent " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
            }

        }
        private void EmpLeaveInfo()
        {
            try
            {
                ViewState.Remove("tblempleaveinfo");
                string comcod = this.GetComeCode();
                string empid = this.GetEmpID();
                if (empid.Length == 0)
                {
                    string Messaged = "Employee ID has not been set, Please contact your management team";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                    return;
                }
                string aplydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVEINFORMATION", empid, aplydat, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvleaveInfo.DataSource = null;
                    this.gvleaveInfo.DataBind();
                    return;
                }
                DataTable dt1 = ds1.Tables[0];
                if (dt1.Rows.Count == 0)
                {
                    this.gvleaveInfo.DataSource = null;
                    this.gvleaveInfo.DataBind();
                    return;
                }
                string gcod = dt1.Rows[0]["gcod"].ToString();
                for (int j = 1; j < dt1.Rows.Count; j++)
                {
                    if (dt1.Rows[j]["gcod"].ToString() == gcod)
                        dt1.Rows[j]["gdesc"] = "";
                    gcod = dt1.Rows[j]["gcod"].ToString();
                }
                ViewState["tblempleaveinfo"] = dt1;
                this.gvleaveInfo.DataSource = dt1;
                this.gvleaveInfo.DataBind();
                ds1.Dispose();
            }
            catch (Exception ex)
            {
                string Messaged = "Oops!! " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                this.btnSave.Enabled = false;
            }

        }

        private void ShowEmppLeave()
        {
            try
            {
                this.txtLeavLreasons.Text = "";
                this.txtLeavRemarks.Text = "";
                Session.Remove("tblleavest");
                string comcod = this.GetComeCode();
                string empid = this.GetEmpID();
                string aplydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
                string calltype = "";
                switch (comcod)
                {
                    /* case "3101":*/  // For BTI as Per concern Nahid Vai  create by Md Ibrahim Khalil
                    case "3365":
                        calltype = "LEAVE_STATUS02BTI";
                        break;

                    default:
                        calltype = "LEAVESTATUS02";
                        break;
                }

                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", calltype, empid, aplydat, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvLeaveStatus.DataSource = null;
                    this.gvLeaveStatus.DataBind();
                    return;
                }

                Session["tblleavest"] = ds1.Tables[0];
                this.Data_Bind();
            }
            catch (Exception ex)
            {
                string Messaged = "Oops!! " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                this.btnSave.Enabled = false;
            }


        }
        private void Data_Bind()
        {
            this.gvLeaveStatus.DataSource = (DataTable)Session["tblleavest"];
            this.gvLeaveStatus.DataBind();
        }
        protected void SendSms(string frmdate, string todate)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string empid = this.GetEmpID();
            var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETSUPERVISER", empid, "", "", "", "", "", "", "", "");

            if (ds == null)
                return;
            DataTable dt = (DataTable)Session["tblleave"];

            //DataRow[] dr = dt.Select("lapplied>0"); 
            double lapplied = Convert.ToDouble(this.Duration.Value.ToString());
            string leavedesc = this.ddlLvType.SelectedValue.ToString();


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string phone = (string)ds.Tables[0].Rows[i]["phone"];
                string empname = (string)ds.Tables[1].Rows[0]["name"];
                string empdesig = (string)ds.Tables[1].Rows[0]["desig"];
                string appdate = "";
                if (hst["compsms"].ToString() == "True")
                {
                    SendSmsProcess sms = new SendSmsProcess();
                    string comnam = hst["comnam"].ToString();
                    string compname = hst["compname"].ToString();
                    // string frmname = "PurReqApproval.aspx?Type=RateInput";
                    // string SMSHead = "Leave Applied From : ";
                    string SMSText = leavedesc + " applied from : " + frmdate + " To " + todate + "\n" + "Name: " + empname + " Designation : " + empdesig;
                    bool resultsms = sms.SendSmmsPwd(comcod, SMSText, phone);
                }
            }
        }


        protected void gvleaveInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkbtn = (LinkButton)e.Row.FindControl("lnkDelete");
                string isapproved = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "isapproved")).ToString();
                string grpsl = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grpsl")).ToString();

                if (isapproved == "true")
                {
                    lnkbtn.Visible = false;
                }
                if ((this.Request.QueryString["Type"] == "MGT") && grpsl == "AAAAA")
                {
                    lnkbtn.Visible = true;
                }

            }
        }



        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)ViewState["tblempleaveinfo"];
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string trnid = ((Label)this.gvleaveInfo.Rows[RowIndex].FindControl("lbllevid")).Text.Trim();
            string lvid = ((Label)this.gvleaveInfo.Rows[RowIndex].FindControl("lgvltrnleaveid")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMLEAVAPP", lvid, trnid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                string Messaged = "Deleted Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                return;
            }
            if (result)
            {
                string eventdesc2 = "LeaveDelete: " + "Leave ID" + trnid + ", " + lvid;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "Leave Request Deleted", eventdesc2, "");
            }
            string Messagesd = "Deleted Success";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagesd + "');", true);


            int rowindex = (this.gvleaveInfo.PageSize) * (this.gvleaveInfo.PageIndex) + RowIndex;
            dt.Rows[rowindex].Delete();
            DataView dv = dt.DefaultView;
            ViewState.Remove("tblempleaveinfo");
            ViewState["tblempleaveinfo"] = dv.ToTable();
            this.EmpLeaveInfo();
        }



        protected void gvInterstLev_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


        private void seLvDate()
        {
            try
            {
                DataTable dt1 = (DataTable)ViewState["tblSlevDay"];
                string leavday = Convert.ToDateTime(this.txtgvenjoydt1.Text).ToString("dd-MMM-yyyy");
                string isHalfday = (this.CheckBox1.Checked ? "True" : "False");
                getLevExitingLv(leavday.ToString(), leavday.ToString());
                DataTable extlv = (DataTable)ViewState["tblextlv"];
                if (extlv.Rows.Count != 0)
                {
                    return;
                }
                if (dt1 == null)
                {
                    return;
                }
                DataRow[] dr2 = dt1.Select("leavday='" + leavday + "'");
                if (dr2.Length > 0)
                {
                    return;
                }
                DataRow dr1 = dt1.NewRow();
                dr1["leavday"] = leavday.ToString();
                dr1["isHalfday"] = isHalfday.ToString();
                dt1.Rows.Add(dr1);
                ViewState["tblSlevDay"] = dt1;
                this.gvInterstLev.DataSource = dt1;
                this.gvInterstLev.DataBind();
            }
            catch (Exception ex)
            {
                string Messaged = "Oops!! " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                this.btnSave.Enabled = false;
            }


        }
        protected void lnkIntsLvDelete_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)ViewState["tblSlevDay"];
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string trnid = ((Label)this.gvInterstLev.Rows[RowIndex].FindControl("lgvapplydate")).Text.Trim();
            dt.Rows[RowIndex].Delete();
            dt.AcceptChanges();
            this.gvInterstLev.DataSource = dt;
            this.gvInterstLev.DataBind();

        }

        protected void chkBoxSkippWH_CheckedChanged(object sender, EventArgs e)
        {
            ViewState.Remove("tblSlevDay");
            if (chkBoxSkippWH.Checked == false)
            {
                divDurStatus.Visible = true;
                divBTWDay.Visible = true;
                diSkippDay.Visible = false;
                diSkippDayDetails.Visible = false;

            }
            else
            {
                divBTWDay.Visible = false;
                diSkippDay.Visible = true;
                diSkippDayDetails.Visible = true;
                divDurStatus.Visible = false;

            }
            this.Duration.Value = "0";
            CreateTable();
            GetCalCulateDay();

        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            seLvDate();
            GetCalCulateDay();
        }
        private void SetError(Exception exp)
        {
            this._errObj["Src"] = exp.Source;
            this._errObj["Msg"] = exp.Message;
            this._errObj["Location"] = exp.StackTrace;
        }

        protected void lnkAddSKDAy_Click(object sender, EventArgs e)
        {
            string gcod = this.ddlLvType.SelectedValue.ToString();

            if (gcod == "")
            {
                string Messaged = "Please Select Leave Type";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                this.btnSave.Enabled = false;
                return;
            }

            GetCalCulateDay();
        }

        protected void ddlLvType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string gcod = this.ddlLvType.SelectedValue.ToString();

            if (gcod == "51999")
            {
                chkBoxSkippWH.Checked = false;
                chkBoxSkippWH_CheckedChanged(null, null);
                frmdate.InnerText = "Leave Day";
                todate.InnerText = "For Duty/Off Day";
                this.divDurStatus.Visible = false;
            }
            else
            {
                //chkBoxSkippWH.Checked = true;
                chkBoxSkippWH_CheckedChanged(null, null);
                frmdate.InnerText = "From Date";
                todate.InnerText = "To Date";
                //this.divDurStatus.Visible = true;
                GetCalCulateDay();

            }
            string reqdate = this.Request.QueryString["LevDay"] ?? "";
            if (reqdate.Length > 0)
            {
                //this.txtaplydate.Text = Convert.ToDateTime(reqdate).ToString("dd-MMM-yyyy");
                string nextday = Convert.ToDateTime(reqdate).ToString("dd-MMM-yyyy");
                this.txtgvenjoydt1.Text = nextday;
                this.txtgvenjoydt2.Text = nextday;
            }
            else
            {
                txtgvenjoydt1.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                txtgvenjoydt2.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }

        }


        //protected void txtgvenjoydt1_DayRender(object sender, DayRenderEventArgs e)
        //{
        //    DateTime stratDate = new DateTime(2022, 3, 13); ;
        //    DateTime endDate = new DateTime(2022, 3, 21); ;

        //    if (e.Day.Date > stratDate && e.Day.Date < endDate)
        //    {
        //        e.Cell.Font.Italic = true;
        //        e.Cell.Font.Size = FontUnit.XLarge;
        //        e.Cell.Font.Strikeout = true;
        //        e.Day.IsSelectable = false;
        //        e.Cell.BackColor = System.Drawing.Color.DarkRed;
        //        e.Cell.Font.Name = "Courier New Baltic";
        //    }
        //}
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();

            switch (comcod)
            {

                case "3365":
                    this.EmployeeLeaveCard();
                    break;

            }

        }

        private void EmployeeLeaveCard()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string curr_year = System.DateTime.Now.ToString("yyyy");
            string curr_date = "26-Dec-" + curr_year;
            string lvname = "";
            string empid = "";


            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + this.GetComeCode() + ".jpg")).AbsoluteUri;

            string qtype = this.Request.QueryString["Type"] ?? "";

            if (qtype == "MGT")
            {
                empid = this.ddlEmpName.SelectedValue.ToString();
            }
            else
            {
                empid = hst["empid"].ToString();
            }






            var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "EMPLOYEELEAVECARD", empid, curr_date);
            if (ds == null)
            {
                return;
            }

            DataTable dt1 = ds.Tables[1];
            DataTable dt2 = ds.Tables[2];

            //this.ClientQueryString("MGT")

            string empname = ds.Tables[0].Rows[0]["empname"].ToString() ?? "";
            string doj = ds.Tables[0].Rows[0]["doj"].ToString() ?? "";
            string dept = ds.Tables[0].Rows[0]["dept"].ToString() ?? "";
            string desig = ds.Tables[0].Rows[0]["desig"].ToString() ?? "";
            string idcardno = ds.Tables[0].Rows[0]["empid"].ToString() ?? "";


            var list1 = dt1.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.LeaveRule>();
            var list2 = dt2.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.currentLeaveInfo>();
            var list3 = dt2.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.currentLeaveInfo>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_84_Lea.rptEmpLeaveCard", list1, list2, list3);
            Rpt1.EnableExternalImages = true;

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                lvname = dt1.Rows[i]["leave"].ToString().Substring(0, 4);
                Rpt1.SetParameters(new ReportParameter("lvname" + i.ToString(), lvname));
            }

            Rpt1.SetParameters(new ReportParameter("compName", comnam));

            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee's Leave Card"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Rpt1.SetParameters(new ReportParameter("idcardno", idcardno));
            Rpt1.SetParameters(new ReportParameter("empname", empname));
            Rpt1.SetParameters(new ReportParameter("doj", doj));
            Rpt1.SetParameters(new ReportParameter("dept", dept));
            Rpt1.SetParameters(new ReportParameter("desig", desig));
            Rpt1.SetParameters(new ReportParameter("curyear", curr_year));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            //Rpt1.PrintToPrinter();
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void chkresign_CheckedChanged(object sender, EventArgs e)
        {

            string getmethod = (this.chkresign.Checked ? "True" : "False");
            if (getmethod == "True")
            {
                this.GetEmpLoyeeResign();
            }
            else
            {
                this.GetEmpLoyee();
            }
          //  this.ddlEmpName_SelectedIndexChanged(null, null);

        }


    }
}