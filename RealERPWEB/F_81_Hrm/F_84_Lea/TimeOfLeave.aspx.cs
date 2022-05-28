﻿using RealERPLIB;
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
    public partial class TimeOfLeave : System.Web.UI.Page
    {
        public List<TimeSpan> list = new List<TimeSpan>();

        ProcessAccess HRData = new ProcessAccess();
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();
        private Hashtable _errObj;
        Common compUtility = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "APPLY TIME OFF";
                string nextday = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtaplydate.Text = nextday;
                GetRemaningTime();
                this.txtFromTime.Text = System.DateTime.Now.ToString("HH:mm");
                this.txtToTime.Text = System.DateTime.Now.ToString("HH:mm");


            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetRemaningTime()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();
            string comcod = this.GetCompCode();
            DateTime date = Convert.ToDateTime(txtaplydate.Text);

            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
                return;
            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            //  string curdate=System.DateTime.Today.ToString("")
            string frmdate = Convert.ToInt32(date.ToString("dd")) > Convert.ToInt32(startdate) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
            frmdate = startdate + frmdate.Substring(2);

            string tdate = date.ToString("dd-MMM-yyyy");

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETTIMEOFLEAVEHISTORY", empid, frmdate, tdate, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DateTime useTime = ds1.Tables[2].Rows.Count == 0 ? DateTime.Parse("06:00") : DateTime.Parse(ds1.Tables[2].Rows[0]["USETIME"].ToString());
            this.txtTimeLVRem.Text = Convert.ToDateTime(useTime).ToString("HH:mm");
            if (ds1.Tables[0].Rows.Count != 0)
            {

                DateTime maxTime = DateTime.Parse("06:00");

                this.gvLvReq.DataSource = (ds1.Tables[0]);
                this.gvLvReq.DataBind();

                ((Label)this.gvLvReq.FooterRow.FindControl("lblAmtTotalremtime")).Text = ds1.Tables[1].Rows[0]["footSum"].ToString().Length == 0 ? "0" : ds1.Tables[1].Rows[0]["footSum"].ToString();

                if (useTime > maxTime)
                {
                    string errMsg = "Already Use Time " + useTime.ToString();

                    this.btnSave.Visible = false;
                    this.ApplicFrm.Visible = false;
                    this.divError.Visible = true;
                    this.spnErrorTxt.InnerText = errMsg;
                }
                else
                {
                    this.btnSave.Visible = true;
                    this.ApplicFrm.Visible = true;
                    this.divError.Visible = false;
                }
            }

        }
        public string GetEmpID()
        {
            string Empid = "";
            string qtype = this.Request.QueryString["Type"] ?? "";
            if (qtype == "MGT")
            {
                Empid = this.Request.QueryString["empid"] ?? "";//this.ddlEmpName.SelectedValue.ToString();

            }
            else
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                Empid = (hst["empid"].ToString() == "") ? "" : hst["empid"].ToString();

            }
            return (Empid);

        }

        public void GetUseTimeCalCulate()
        {
            DateTime frmdate, todate;

            frmdate = Convert.ToDateTime(this.txtaplydate.Text.ToString() + " " + txtFromTime.Text.ToString());
            todate = Convert.ToDateTime(this.txtaplydate.Text.ToString() + " " + txtToTime.Text.ToString());

            DateTime remTime = DateTime.Parse(txtTimeLVRem.Text);
            DateTime d1 = DateTime.Parse(txtFromTime.Text);
            DateTime d2 = DateTime.Parse(txtToTime.Text);
            string cdate = Convert.ToDateTime(txtaplydate.Text).ToString("dd-MMM-yyyy");
            TimeSpan timeDiff;
            DateTime luntime_st = Convert.ToDateTime(cdate + " 01:00 PM");
            DateTime luntime_end = Convert.ToDateTime(cdate + " 01:59 PM");
            DateTime Offic_end = Convert.ToDateTime(cdate + " 05:30 PM");


            if (frmdate <= luntime_st && todate >= luntime_end)
            {
                TimeSpan timeFrom = TimeSpan.Parse(d1.ToString("HH:mm"));
                TimeSpan timeTo = TimeSpan.Parse(d2.ToString("HH:mm"));
                TimeSpan lunchddif = luntime_end.Subtract(luntime_st);
                if (timeFrom.TotalSeconds > timeTo.TotalSeconds)
                {
                    d2 = d2.AddDays(1);
                    timeDiff = d2.Subtract(d1);
                }
                else
                {
                    timeDiff = (d2.Subtract(d1)).Subtract(lunchddif);
                }

            }
            else
            {
                TimeSpan timeFrom = TimeSpan.Parse(d1.ToString("HH:mm"));
                TimeSpan timeTo = TimeSpan.Parse(d2.ToString("HH:mm"));

                if (timeFrom.TotalSeconds > timeTo.TotalSeconds)
                {
                    d2 = d2.AddDays(1);
                    timeDiff = d2.Subtract(d1);
                }
                else
                {
                    timeDiff = d2.Subtract(d1);
                }
            }


            TimeSpan remTimeConvt = TimeSpan.Parse(remTime.ToString("HH:mm"));
            TimeSpan time3 = timeDiff;
            TimeSpan maxTime1 = TimeSpan.Parse("06:00");
            TimeSpan enjtime = TimeSpan.Parse("0:00");

            TimeSpan usetime = maxTime1 - remTimeConvt;

            enjtime = time3;
            TimeSpan mintime = TimeSpan.Parse("0:00");

            if (usetime > maxTime1)
            {
                string Messaged = "Your time is exceed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
            }
            else if ((enjtime > maxTime1))
            {
                string Messaged = "Your office Time exceed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                this.btnSave.Enabled = false;
                this.txtUseTime.Text = "0";
            }
            else if ((enjtime > remTimeConvt))
            {
                string Messaged = "Your remaning Time exceed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                this.btnSave.Enabled = false;
                this.txtUseTime.Text = "0";
            }
            else
            {
                this.txtUseTime.Text = timeDiff.ToString();
                this.btnSave.Enabled = true;

            }

        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            GetUseTimeCalCulate();
            string compsms = hst["compsms"].ToString();
            string compmail = hst["compmail"].ToString();
            string ssl = hst["ssl"].ToString();
            //Sender Informaiton
            string comcod = this.GetCompCode();
            string sendUsername = hst["userfname"].ToString();
            string empid = this.GetEmpID();
            string sendDptdesc = hst["dptdesc"].ToString();
            string sendUsrdesig = hst["usrdesig"].ToString();
            string compName = hst["comnam"].ToString();
            string htmtableboyd = "";
            string usrid = hst["usrid"].ToString();
            string deptcode = hst["deptcode"].ToString();


            string remTime = this.txtTimeLVRem.Text.Trim();
            string reqtype = "TLV";// time of leave
            string reqfor = "Application for Time Of Leave";
            string reqdate = this.txtaplydate.Text.Trim();
            string dayID = Convert.ToDateTime(this.txtaplydate.Text.Trim()).ToString("yyyyMMdd");
            string reqtimeOUT = this.txtFromTime.Text.Trim();
            string reqtimeIN = this.txtToTime.Text.Trim();
            string txtReson = txtLeavLreasons.Text.Trim();
            string usetime = this.txtUseTime.Text.Trim();
            string postDat = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string qtype = this.Request.QueryString["Type"] ?? "";
            if (usetime == "00:00:00")
            {
                string Messaged = "Submit Fail, Use time 00:00";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                return;
            }
            else
            {
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "INSERT_REQ_ATTN_CAHNGE", dayID, empid, reqdate, reqtype, reqtimeOUT, reqtimeIN, txtReson, usetime, usrid, postDat, "");
                //reqtimeOUT actual INTIME, reqtimeIN actual OUTTIME its change only for time of leave case simarlar other type  insert data, any issue discuse with emdad,nahid, ibrahim


                if (!result)
                {

                    string errMsg = "Update Fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + errMsg + "');", true);
                    return;
                }

                else
                {
                    htmtableboyd = "Details: Apply Date: " + reqdate + ", <br>  Out time :" + reqtimeOUT + ",  In time :" + reqtimeIN + ",<br>  Use Time:" + usetime + " Hour, <br>  Previous Use Time :" + remTime + ",<br> Reason/Remarks " + txtReson;


                    string trnid = this.GetattAppId(empid);
                    string Messaged = "Successfully applied for " + reqfor + ", please wait for approval";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messaged + "');", true);
                    if (qtype != "MGT")
                    {
                        this.SendNotificaion(reqdate, reqdate, trnid, deptcode, compsms, compmail, ssl, compName, htmtableboyd);

                    }

                    string eventdesc2 = htmtableboyd;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "New Request for " + reqfor, htmtableboyd, Messaged);
                    GetRemaningTime();
                }
            }


        }
        private string GetattAppId(string empid)
        {

            string comcod = this.GetCompCode();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.[SP_REPORT_HR_MGT_INTERFACE]", "GETATTAPPID", empid, "", "", "", "", "", "", "", "");
            string lstid = ds5.Tables[0].Rows[0]["ltrnid"].ToString().Trim();
            return lstid;
        }



        protected void txtFromTime_TextChanged(object sender, EventArgs e)
        {
            string cdate = Convert.ToDateTime(txtaplydate.Text).ToString("dd-MMM-yyyy");
            DateTime todate = Convert.ToDateTime(this.txtaplydate.Text.ToString() + " " + txtToTime.Text.ToString());
            DateTime fodate = Convert.ToDateTime(this.txtaplydate.Text.ToString() + " " + txtFromTime.Text.ToString());
            DateTime Offic_end = Convert.ToDateTime(cdate + " 05:30 PM");
            DateTime Offic_ST = Convert.ToDateTime(cdate + " 09:00 AM");
            if (Offic_ST > fodate || Offic_end < fodate)
            {
                this.txtFromTime.Text = System.DateTime.Now.ToString("HH:mm");
                string Messaged = "Your office Time exceed ";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                return;
            }

            //if (todate > fodate)
            //{
            //    this.txtToTime.Text = Convert.ToDateTime(Offic_end).ToString("HH:mm");                
            //    string Messaged = "Your office Time exceed ";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
            //    return;
            //}

            GetUseTimeCalCulate();

        }

        protected void txtToTime_TextChanged(object sender, EventArgs e)
        {
            string cdate = Convert.ToDateTime(txtaplydate.Text).ToString("dd-MMM-yyyy");
            DateTime Offic_end = Convert.ToDateTime(cdate + " 05:30 PM");
            DateTime Offic_ST = Convert.ToDateTime(cdate + " 09:00 AM");
            DateTime todate = Convert.ToDateTime(this.txtaplydate.Text.ToString() + " " + txtToTime.Text.ToString());
            DateTime fodate = Convert.ToDateTime(this.txtaplydate.Text.ToString() + " " + txtFromTime.Text.ToString());

            if (todate > Offic_end)
            {
                this.txtToTime.Text = Convert.ToDateTime(Offic_end).ToString("HH:mm");
                string Messaged = "Your office Time exceed ";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);

            }

            if (todate < fodate)
            {
                this.txtToTime.Text = Convert.ToDateTime(Offic_end).ToString("HH:mm");
                string Messaged = "Your office Time exceed ";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);

            }



            GetUseTimeCalCulate();

        }

        private void SendNotificaion(string frmdate, string todate, string ltrnid, string deptcode, string compsms, string compmail, string ssl, string compName, string htmtableboyd)
        {
            try
            {
                string comcod = this.GetCompCode();
                DataTable dt = (DataTable)ViewState["tblempinfo"];
                string reqtype = "TLV";// time of leave
                string reqfor = "Application for Time Of Leave";
                string empid = this.GetEmpID();
                var ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETSUPERVISERMAIL", empid, "", "", "", "", "", "", "", "");

                if (ds1 == null)
                    return;
                string supphone = "";
                string suserid = ds1.Tables[0].Rows[0]["suserid"].ToString();
                string tomail = ds1.Tables[0].Rows[0]["mail"].ToString();
                string idcard = (string)ds1.Tables[1].Rows[0]["idcard"];
                string empname = (string)ds1.Tables[1].Rows[0]["name"];
                string empdesig = (string)ds1.Tables[1].Rows[0]["desig"];
                string deptname = (string)ds1.Tables[1].Rows[0]["deptname"];
                string uhostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_81_Hrm/F_92_Mgt/";
                string currentptah = "EmpAttApproval?Type=Ind&comcod=" + comcod + "&refno=" + deptcode + "&ltrnid=" + ltrnid + "&Date=" + frmdate + "&usrid=" + suserid + "&RoleType=SUP" + "&Reqtype=TLV";
                string totalpath = uhostname + currentptah;


                string maildescription = "Dear Sir, Please Approve My Request." + "<br> Employee ID Card : " + idcard + ",<br>" + "Employee Name : " + empname + ",<br>" + "Designation : " + empdesig + "," + "<br>" +
                     "Department Name : " + deptname + "," + "<br>" + "<b>Request Type : " + reqfor + "</b>,<br>" + " Request id: " + ltrnid + ". <br>";
                maildescription += htmtableboyd;
                maildescription += "<div style='color:red'><a style='color:blue; text-decoration:underline' href = '" + totalpath + "'>Click for Approved</a> or Login ERP Software and check Request Interface</div>" + "<br/>";


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

                #region

                string subj = "New Request for Time Of Leave";
                string msgbody = maildescription;



                bool result2 = UserNotify.SendNotification(subj, msgbody, suserid);

                if (compsms == "True")
                {
                    SendSmsProcess sms = new SendSmsProcess();
                    string SMSText = "New Request from : " + frmdate + " To " + todate;// 
                    bool resultsms = sms.SendSmmsPwd(comcod, SMSText, supphone);
                }
                if (compmail == "True")
                {

                    bool Result_email = UserNotify.SendEmailPTL(hostname, portnumber, frmemail, psssword, subj, empname, empdesig, deptname, compName, tomail, msgbody, isSSL);
                    if (Result_email == false)
                    {
                        string Messagesd = "Request Applied but Notification has not been sent";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                string Messagesd = "Request Applied but Notification has not been sent " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
            }

        }

        protected void gvLvReq_RowDataBound(object sender, GridViewRowEventArgs e)
        {


        }

        protected void lkDelete_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string trnid = ((Label)this.gvLvReq.Rows[RowIndex].FindControl("lbllevid")).Text.Trim();
            string empid = ((Label)this.gvLvReq.Rows[RowIndex].FindControl("lblgvempid")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "DELETEMPTIMEOFLEAVE", empid, trnid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                string Messaged = "Deleted Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                return;
            }


            string Messagesd = "Deleted Success";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagesd + "');", true);

            GetRemaningTime();

            string eventdesc2 = "Leave Request deleted, Request ID by:  " + trnid;
            bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "Delete Time Of Leave Request", eventdesc2, "");
        }
    }
}