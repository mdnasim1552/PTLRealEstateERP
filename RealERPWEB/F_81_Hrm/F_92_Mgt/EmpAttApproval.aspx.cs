using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class EmpAttApproval : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDptUserCheck();
                GetRequestType();
                this.ShowData();
                ((Label)this.Master.FindControl("lblTitle")).Text = "REQUEST INTERFACE APPROVAL";//             
            }
        }

        private void ShowData()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            //if (comcod == "3354" || comcod == "3101")
            //{
            //    ddlReqType.Enabled = false;
            //}
            string date = this.Request.QueryString["Date"] ?? "";
            
            string usrid = hst["usrid"].ToString();// (this.Request.QueryString["Type"] == "Ind") || (this.Request.QueryString["Type"] == "DeptHead") ? hst["usrid"].ToString() : "";
            string fDate = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
            string tDate = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
          
            string type = "";//(this.Request.QueryString["Type"]) == "Ind" || (this.Request.QueryString["Type"] == "DeptHead") ? "" : "Management";
            string DeptHead = "";//(this.Request.QueryString["Type"]) == "DeptHead" ? "DeptHead" : "";
            string id = this.Request.QueryString["ltrnid"] ?? "";

            DataSet ds1 = HRData.GetTransInfoNew(comcod, "DBO_HRM.SP_REPORT_HR_MGT_INTERFACE", "GETALLATTREQUEST",null,null,null, fDate, tDate, usrid, type, DeptHead, id, "%%", "%", "%","%","%","%");
               if (ds1 == null)
                return;
            ViewState["tblattreq"] = ds1.Tables[0];
            this.data_Bind();
        }
        private void GetRequestType()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETREQUESTTYPE", "", "");
            if (ds == null)
                return;



            if (comcod == "3365")
            {
                DataView dv = new DataView();
                dv.Table = ds.Tables[0];
                dv.RowFilter = "hrgcod = '94803' or hrgcod = '94804' or hrgcod = '94805'";
                this.ddlReqType.DataTextField = "hrgdesc";
                this.ddlReqType.DataValueField = "unit";
                this.ddlReqType.DataSource = dv;
                this.ddlReqType.DataBind();
            }
            else
            {
                this.ddlReqType.DataTextField = "hrgdesc";
                this.ddlReqType.DataValueField = "unit";
                this.ddlReqType.DataSource = ds.Tables[0];
                this.ddlReqType.DataBind();

            }

        }
        private void data_Bind()
        {
            DataTable dt1 = (DataTable)ViewState["tblattreq"];
            string reqtype = this.Request.QueryString["Reqtype"]??"";
            string empid = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["empid"].ToString();

            string empUsrID = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["empuserid"].ToString();
            string empEmail = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["empEmail"].ToString();
            string idcard = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["idcard"].ToString();
            string deptName = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["deptanme"].ToString();
            string empdesig = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["desig"].ToString();
            string empname = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["empname"].ToString();
            string empreson = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["empreson"].ToString();
            string atttyp= dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["lvtype"].ToString();
            string reqdate= dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["strtdat"].ToString();
            string restatus = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["lvstatus1"].ToString();
            string intime = dt1.Rows.Count == 0 ? "" : dt1.Rows[0]["intime"].ToString();

            this.lbldadte.Text =Convert.ToDateTime(reqdate).ToString("dd-MMM-yyyy");
            this.lbldadteTime.Text = Convert.ToDateTime(intime).ToString("hh:mm tt");
            this.ddlReqType.SelectedValue = reqtype;

            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3354":
                case "3101":
                    //var isReadonly = ((DropDownList)ddlReqType).ReadOnly;
                    //this.ddlReqType.IsReadOnly=true;
                    //this.ddlReqType.Items.Remove("LA");
                    //this.ddlReqType.Items.Remove("TC");
                    //this.ddlReqType.Items.Remove("LP");
                    //this.ddlReqType.Items.Remove("TLV");
                    //this.ddlReqType.Items.RemoveAt(0);
                    //this.ddlReqType.Items.RemoveAt(1);
                    //this.ddlReqType.Items.RemoveAt(2);              
                    break;
                default:
                    break;
            }

            /// for employee information 

            this.UserName.InnerText = empname;
            this.UDesignation.InnerText = empdesig;
            this.UDptment.InnerText = deptName;
            this.idcard.InnerText = "ID Card - "+ idcard;
            this.txtAreaReson.Text = empreson;
            //this.lblRemarks.Text = ds.Tables[1].Rows[0]["usrname"].ToString();
            //// Approval Part 
            ///
            this.Reqst.InnerHtml ="Current Status : "+ restatus;
        }

        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
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
                    this.LateApp.Visible = false;

                    return;
                }
            }

        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            

        }
        private void SendNotificaion(string ltrnid, string deptcode, string roletype, string isForward, string compsms, string compmail, string ssl, string sendUsername, string sendDptdesc, string sendUsrdesig, string compName)
        {
            try
            {

                string comcod = this.GetCompCode();
             
                string date = this.Request.QueryString["Date"] ?? "";
                string frmdate = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
                DataTable dt = (DataTable)ViewState["tblattreq"];
                string supapp = "Your Request approved has been approved by the Supervisor, please waiting for Department/Section Head approval.";
                string dptapp = "Your Request has been approved by the Department/Section Head.";
                string empid = dt.Rows.Count == 0 ? "" : dt.Rows[0]["empid"].ToString();
                string empUsrID = dt.Rows.Count == 0 ? "" : dt.Rows[0]["empuserid"].ToString();
                string empEmail = dt.Rows.Count == 0 ? "" : dt.Rows[0]["empEmail"].ToString();
                string idcard = dt.Rows.Count == 0 ? "" : dt.Rows[0]["idcard"].ToString();
                string deptName = dt.Rows.Count == 0 ? "" : dt.Rows[0]["deptanme"].ToString();
                string empdesig = dt.Rows.Count == 0 ? "" : dt.Rows[0]["desig"].ToString();
                string empname = dt.Rows.Count == 0 ? "" : dt.Rows[0]["empname"].ToString();
                string reqdesc = dt.Rows.Count == 0 ? "" : dt.Rows[0]["attstatus"].ToString();
                string reqtype = this.Request.QueryString["Reqtype"] ?? "";
                string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
                string absapp = "0";
                string lateapp = "0";
                
                // this part for the Attedance update process 
                if (roletype == "DPT" || (roletype=="SUP" && comcod=="3354"))
                {
                    if (reqtype == "AB")
                    {
                        absapp = "1";
                        bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTORUPDATEOFFTIMEANDDELABSENTALL", frmdate, todate, empid, absapp, idcard, "req", "", "", "", "", "", "", "", "", "");
                    }

                    else if (reqtype == "LP")
                    {
                        lateapp = "1";
                        string remarks = "Late Present Approval";
                        frmdate = Convert.ToDateTime(frmdate).ToString("yyyyMMdd");
                        bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "UPDATEATTLATEAPPROVAL", frmdate,  empid, idcard, lateapp, remarks, usrid, "", "", "", "", "", "", "", "");
                    }
                    else if (reqtype == "LA")
                    {
                        lateapp = "1";
                        string remarks = "Late Approval";
                        frmdate = Convert.ToDateTime(frmdate).ToString("yyyyMMdd");
                        bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "UPDATEATTLATEAPPROVAL", frmdate, empid, idcard, lateapp, remarks, usrid, "", "", "", "", "", "", "");
                    }

                    else if (reqtype == "TC")
                    {
                        absapp = "0";
                        string remarks = "Time Correction";
                        frmdate = Convert.ToDateTime(frmdate).ToString("yyyyMMdd");
                        bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "UPDATEATTLATEAPPROVAL", frmdate, empid, idcard, "0", remarks, usrid, reqtype, "", "", "", "", "", "");
                    }
                }               
                ///GET SMTP AND SMS API INFORMATION
                #region
               
                DataSet dssmtpandmail = HRData.GetTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");

                //SMTP
                string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
                int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
                string frmemail = dssmtpandmail.Tables[0].Rows[0]["mailid"].ToString();
                string psssword = dssmtpandmail.Tables[0].Rows[0]["mailpass"].ToString();
                bool isSSL = Convert.ToBoolean(dssmtpandmail.Tables[0].Rows[0]["issl"].ToString());

                #endregion

                string roletypeCHk = (roletype == "SUP") ? "DPT" : "MGT";

                var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETAPPRVPMAIL", deptcode, roletypeCHk, "", "", "", "", "", "", "");
                // var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "HRAPPROVAL_DPT_HEAD_USERID", deptcode, roletypeCHk, "", "", "", "", "", "", "");
                if (ds == null)
                    return;

                #region
                string subj = "New Request for "+ reqdesc;
                #endregion

                #region
                // User profile notifcation 
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string appusrid = ds.Tables[0].Rows[i]["usrid"].ToString();
                    string phone = ds.Tables[0].Rows[i]["phone"].ToString();
                    string tomail = ds.Tables[0].Rows[i]["mail"].ToString();
                    string isrole = (roletype == "SUP" ? "DPT" :
                                    roletype == "DPT" ? "MGT" : "MGT");

                    string uhostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_81_Hrm/F_92_Mgt/";
                    string currentptah = "EmpAttApproval?Type=Ind&comcod=" + comcod + "&refno=" + deptcode + "&ltrnid=" + ltrnid + "&Date=" + frmdate + "&usrid = " + appusrid + "&RoleType=" + isrole+ "&Reqtype=TLV";
                    string totalpath = uhostname + currentptah;
                   
                    string maildescription = "Dear Sir, Please Approve "+ reqdesc + " Request." + "<br> Employee ID Card : " + idcard + ",<br>" + "Employee Name : " + empname + ",<br>" + "Designation : " + empdesig + "," + "<br>" +
                      "Department Name : " + deptName + "," + "<br>" + "Leave Type : " + reqdesc + ",<br>" + " Request id: " + ltrnid + ". <br>";
                    maildescription += "<div style='color:red'><a style='color:blue; text-decoration:underline' href = '" + totalpath + "'>Click for Approved</a> or Login ERP Software and check Leave Interface</div>" + "<br/>";


                    string msgbody = maildescription;

                    bool result2 = UserNotify.SendNotification(subj, msgbody, appusrid);

                    if (compsms == "True")
                    {
                        SendSmsProcess sms = new SendSmsProcess();
                        string SMSText = "Request approved from : " + frmdate + " To " + todate;// 
                        bool resultsms = sms.SendSmmsPwd(comcod, SMSText, phone);
                    }
                    if (compmail == "True")
                    {
                        bool Result_email = UserNotify.SendEmailPTL(hostname, portnumber, frmemail, psssword, subj, sendUsername, sendUsrdesig, sendDptdesc, compName, tomail, msgbody, isSSL);
                        if (Result_email == false)
                        {
                            string Messagesd = "Request Approved, Notification did not send";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                        }
                    }
                }


                // for applied user send notification

                string toMSgBody = (roletype == "SUP" ? supapp :
                                     roletype == "DPT" ? dptapp : "Your Request has been approved");
                string toEmpsub = "Request Approved";
                bool result3 = UserNotify.SendNotification(toEmpsub, toMSgBody, empUsrID);
                if (result3 == false)
                {
                    string Messagesd = "Request Approved, Notification did not send";
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
                string Messagesd = "Request Approved, Notification did not send" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
            }

        }

        protected void lnkApproved_Click(object sender, EventArgs e)
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
                /// this.SaveLeave();
                string reqtype = this.ddlReqType.SelectedValue.ToString();
                string remarks = this.txtremarks.Text.ToString();
                string intime = this.lbldadteTime.Text.Trim();
                string ldate = Convert.ToDateTime(this.lbldadte.Text.Trim()).ToString("yyyyMMdd");


                string roletype = this.Request.QueryString["RoleType"].ToString();
                string approvdat = System.DateTime.Now.ToString("dd-MMM-yyyy");
                string Centrid = this.Request.QueryString["refno"] ?? "";
                string Orderno = this.Request.QueryString["ltrnid"] ?? "";
                bool result = false;
                string apDate = System.DateTime.Now.ToString(); ;
                DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MGT_INTERFACE", "GETCEHCKAPPROVALBYID", Orderno, roletype, Centrid, "", "", "", "", "", "");
                if (ds4.Tables[0].Rows.Count != 0)
                {
                    string Messagesd = "Request Already Approved";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                    return;
                }
                else
                {
                   // this.LeaveUpdate();
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MGT_INTERFACE", "UPDATEATTAPPREQ", Orderno, ApprovByid, Approvtrmid, ApprovSession, approvdat, Centrid, roletype, remarks, reqtype, "", "", "", "", "", "");
                    if (result == false)
                    {
                        string Messagesd = "Request Approved Fail";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                        return;
                    }
                    else
                    {
                        this.SendNotificaion(Orderno, Centrid, roletype, "", compsms, compmail, ssl, sendUsername, sendDptdesc, sendUsrdesig, compName);
                        if (reqtype == "TC")
                        {
                            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MGT_INTERFACE", "UPDATETIME", Orderno, reqtype, intime, "", "", "", "", "");

                        }

                        string Messagesd = "Request Approved";

                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagesd + "');", true);

                        string eventdesc2 = "Details: " + sendUsername + sendDptdesc + sendUsrdesig + compName;
                        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), Messagesd, Messagesd, eventdesc2);

                    }
                }

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Request Approved";
                    string eventdesc = "Request Approved";
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

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string compName = hst["comnam"].ToString();

                DataTable dt = (DataTable)ViewState["tblattreq"];


                string empUsrID = dt.Rows.Count == 0 ? "" : dt.Rows[0]["empuserid"].ToString();
                string empEmail = dt.Rows.Count == 0 ? "" : dt.Rows[0]["empEmail"].ToString();
                string idcard = dt.Rows.Count == 0 ? "" : dt.Rows[0]["idcard"].ToString();

                string to_empname = dt.Rows.Count == 0 ? "" : dt.Rows[0]["empname"].ToString();
                string atttyp = dt.Rows.Count == 0 ? "" : dt.Rows[0]["lvtype"].ToString();

                string reqtype = this.ddlReqType.SelectedItem.Text.ToString();
                
                string trnid = this.Request.QueryString["ltrnid"] ?? ""; ;
                string remarks = this.txtremarks.Text;

                if (remarks.Length == 0)
                {
                    this.txtremarks.Focus();
                    this.txtremarks.CssClass = "form-control is-invalid";
                    string Messagesd = "Please Fill remarks";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                    return;
                }

                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MGT_INTERFACE", "UPDATECANCELREQUEST", trnid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    string Messagesd = "Canceled failed";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                    return;

                }


                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert(' Not Apporved');", true);

                if (hst["compsms"].ToString() == "True")
                {
                    string empid = dt.Rows.Count == 0 ? "" : dt.Rows[0]["empid"].ToString();

                   
                    string canname = hst["username"].ToString(); ;
                  //  string empid = this.ddlEmpName.SelectedValue.ToString();
                    var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPPHONE", empid, "", "", "", "", "", "", "", "");

                    if (ds == null)
                        return;
                    string phone = (string)ds.Tables[0].Rows[0]["phone"];
                    SendSmsProcess sms = new SendSmsProcess();
                    string SMSText = "Request Canceled by : " + canname; // 
                    bool resultsms = sms.SendSmmsPwd(SMSText, SMSText, phone);

                }

                else if (hst["compmail"].ToString() == "True")

                {

                    string usrid = hst["usrid"].ToString();
                    string deptcode = hst["deptcode"].ToString();
                    string username = hst["username"].ToString();

                    string empid = dt.Rows.Count == 0 ? "" : dt.Rows[0]["empid"].ToString();


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
                    string toEmpsub = "Request Canceled";
                    string toMSgBody = "Dear " + to_empname + ",\n" + " Reason : " + remarks + "\n" + ", Request Canceled By : " + empname + ", Designation " + empdesig + ", Department Name: " + deptName + "\n";
                    bool Result_email = UserNotify.SendEmailPTL(hostname, portnumber, frmemail, psssword, toEmpsub, empname, empdesig, deptName, compName, mail, toMSgBody, isSSL);

                    bool result2 = UserNotify.SendNotification(toEmpsub, toMSgBody, empUsrID);
                }
                
                this.lnkCancel.Visible = false;
                this.lnkApproved.Visible = false;

                string Messagessd = reqtype+ " Canceled Success";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagessd + "');", true);
                ShowData();
            }
            catch (Exception ex)
            {
                string Messagessd = "Something Wrong !!" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagessd + "');", true);
            }

        }

        protected void ddlReqType_SelectedIndexChanged(object sender, EventArgs e)
        {
     
            if (ddlReqType.SelectedValue.ToString() == "TC")
            {
                this.lbldadteTime.Enabled = true;
            }
            else
            {
                this.lbldadteTime.Enabled = false;
            }
        }
    }
}