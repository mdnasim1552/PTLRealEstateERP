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
namespace RealERPWEB.F_81_Hrm.F_82_App
{
    public partial class RptMyAttendenceSheet : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "  Employee Status";
                this.getMyAttData();

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private string GetComLateAccTime()
        {
            string comcod = this.GetComeCode();
            string acclate = "";
            switch (comcod)
            {

                case "3336":
                    acclate = "acclate";
                    break;

                default:
                    break;
            }

            return acclate;
        }
        private void getMyAttData()
        {
            string comcod = this.GetComeCode();
            string frmdate = this.Request.QueryString["frmdate"].ToString();
            string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            string empid = this.Request.QueryString["empid"].ToString();
            string Actime = this.GetComLateAccTime();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPATTNIDWISE", frmdate, todate, empid, Actime, "", "", "", "", "");

            if (ds1 == null)
            {
                return;
            }

            //this.lblDateOn.Text = " From " + this.Request.QueryString["frmdate"].ToString() + " To " + this.Request.QueryString["todate"].ToString();
            // this.lblcompname.Text = ds1.Tables[2].Rows[0]["companyname"].ToString();
            this.lblname.Text = ds1.Tables[0].Rows[0]["empnam"].ToString();
            this.lbldpt.Text = ds1.Tables[0].Rows[0]["empdept"].ToString();
            this.lbldesg.Text = ds1.Tables[0].Rows[0]["empdsg"].ToString();
            this.lblcard.Text = ds1.Tables[0].Rows[0]["idcardno"].ToString();
            this.lblIntime.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["offintime1"]).ToString("hh:mm tt");
            this.lblout.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["stdtimeout"]).ToString("hh:mm tt");
            this.lblwork.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["twrkday"]).ToString("#, ##0;(#, ##0);");
            this.lblLate.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["tlday"]).ToString("#, ##0;(#, ##0);");
            this.lblLeave.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["tlvday"]).ToString("#, ##0;(#, ##0);");
            this.lblAbsent.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["tabsday"]).ToString("#, ##0;(#, ##0);");
            this.lblHoliday.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["thday"]).ToString("#, ##0;(#, ##0);");


            Session["tblempdatewise"] = ds1.Tables[0];

            this.RptMyAttenView.DataSource = ds1;
            this.RptMyAttenView.DataBind();




        }




        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintEmpAttnIdWise();
        }

        private void PrintEmpAttnIdWise()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = this.GetComeCode();
            //string Company = "";
            // string PCompany = "";
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string empid = this.Request.QueryString["empid"].ToString();
            string frmdate = this.Request.QueryString["frmdate"].ToString();
            string todate = this.Request.QueryString["todate"].ToString();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPATTNIDWISE", frmdate, todate, empid, "", "", "", "", "", "");


            DataTable dtdailyiemp = ds4.Tables[0];
            int sum = 0;
            string hour, minute;
            for (int i = 0; i < dtdailyiemp.Rows.Count; i++)
            {
                sum += Convert.ToInt32(dtdailyiemp.Rows[i]["actualattnminute"]);
            }
            hour = Convert.ToInt32(sum / 60).ToString();
            minute = ASTUtility.Right((Convert.ToString("00" + (sum % 60))), 2);
            string TotalHour = hour + ":" + minute;
            ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_83_Att.rptDailyAttnEmp();
            rptTest.SetDataSource(ds4.Tables[0]);
            TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            txtRptComName.Text = ds4.Tables[2].Rows[0]["companyname"].ToString();

            TextObject txttowrkday = rptTest.ReportDefinition.ReportObjects["txttowrkday"] as TextObject;
            txttowrkday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["twrkday"]).ToString("#,##0;(#,##0); ");
            TextObject txttolateday = rptTest.ReportDefinition.ReportObjects["txttolateday"] as TextObject;
            txttolateday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tLday"]).ToString("#,##0;(#,##0); ");
            TextObject txttoleaveday = rptTest.ReportDefinition.ReportObjects["txttoleaveday"] as TextObject;
            txttoleaveday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tlvday"]).ToString("#,##0;(#,##0); ");
            TextObject txtoabsday = rptTest.ReportDefinition.ReportObjects["txtoabsday"] as TextObject;
            txtoabsday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tabsday"]).ToString("#,##0;(#,##0); ");
            TextObject txtohday = rptTest.ReportDefinition.ReportObjects["txtohday"] as TextObject;
            txtohday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["thday"]).ToString("#,##0;(#,##0); ");


            TextObject txtrptTotalHour = rptTest.ReportDefinition.ReportObjects["txtTHour"] as TextObject;
            txtrptTotalHour.Text = TotalHour;
            TextObject txtuserinfo = rptTest.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            Session["Report1"] = rptTest;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void RptMyAttenView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                string comcod = this.GetComeCode();
                string ahleave = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "leav")).ToString().Trim();
                string lateapp = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "lateapp")).ToString().Trim();

                DateTime offimein = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "stdtimein"));
                DateTime offouttim = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "stdtimeout"));


                DateTime actualin = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "actualin"));
                DateTime actualout = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "actualout"));


                switch (comcod)
                {
                    case "3365":
                    case "3101":
                        if (ahleave == "A")
                        {
                            ((Label)e.Item.FindControl("lblactualout")).Visible = false;
                            ((Label)e.Item.FindControl("lblactualin")).Visible = false;
                            ((Label)e.Item.FindControl("lblstatus")).Attributes["style"] = "font-weight:bold;";
                            ((LinkButton)e.Item.FindControl("lnkRequstApply")).Visible = true;

                        }
                        else if (ahleave == "H" || ahleave == "Lv")
                        {
                            ((Label)e.Item.FindControl("lblactualout")).Visible = false;
                            ((Label)e.Item.FindControl("lblactualin")).Visible = false;
                            ((Label)e.Item.FindControl("lblstatus")).Attributes["style"] = "font-weight:bold;";

                        }

                        else if ((offimein < actualin) && lateapp == "False")
                        {
                            ((Label)e.Item.FindControl("lblactualout")).Attributes["style"] = "font-weight:bold; color:red;";
                            ((Label)e.Item.FindControl("lblactualin")).Attributes["style"] = "font-weight:bold; color:red;";
                            ((Label)e.Item.FindControl("lbldtimehour")).Attributes["style"] = "font-weight:bold; color:red;";
                            ((LinkButton)e.Item.FindControl("lnkRequstApply")).Visible = true;


                        }
                        else
                        {
                            ((LinkButton)e.Item.FindControl("lnkRequstApply")).Visible = false;

                        }


                        break;
                    default:
                        if (ahleave == "A" || ahleave == "H" || ahleave == "Lv")
                        {
                            ((Label)e.Item.FindControl("lblactualout")).Visible = false;
                            ((Label)e.Item.FindControl("lblactualin")).Visible = false;
                            ((Label)e.Item.FindControl("lblstatus")).Attributes["style"] = "font-weight:bold;";

                        }
                        else if ((offimein < actualin || offouttim > actualout) && lateapp == "False")
                        {
                            ((Label)e.Item.FindControl("lblactualout")).Attributes["style"] = "font-weight:bold; color:red;";
                            ((Label)e.Item.FindControl("lblactualin")).Attributes["style"] = "font-weight:bold; color:red;";
                            ((Label)e.Item.FindControl("lbldtimehour")).Attributes["style"] = "font-weight:bold; color:red;";


                        }
                        break;
                }




            }



            if (e.Item.ItemType == ListItemType.Footer)
            {
                double AcTime = 0.00;
                DataTable dt3 = (DataTable)Session["tblempdatewise"];
                foreach (DataRow dr in dt3.Rows)
                {
                    double time = Convert.ToDouble("0" + dr["actTimehour"]);
                    AcTime = AcTime + time;
                }
                ((Label)e.Item.FindControl("lblTotalHour")).Text = AcTime.ToString("#,##0.00;(#,##0.00);"); //? 0.00 : dt3.Compute("Sum(actTimehour)", ""))).ToString("#,##0.00;(#,##0.00);");

                //Double actTimehour =Convert.ToDouble(dt3.Rows[0]["actTimehour"]);
                //((Label)e.Item.FindControl("lblTotalHour")).Text = Convert.ToDouble((Convert.IsDBNull(Convert.ToDouble(dt3.Compute("Sum(actTimehour)", ""))))).ToString("#,##0.00;(#,##0.00);"); //? 0.00 : dt3.Compute("Sum(actTimehour)", ""))).ToString("#,##0.00;(#,##0.00);");

            }
        }

        protected void lnkRequstApply_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            //  this.lblabsheading.Text = "Apply for RequestDate :" + this.txtfrmDate.Text.ToString() + " To: " + this.txttoDate.Text.ToString();

            LinkButton lnkBtn1 = sender as LinkButton;
            RepeaterItem Rptitem = (RepeaterItem)lnkBtn1.NamingContainer;
            Label lblIntime = (Label)Rptitem.FindControl("lblIntime");
            Label issuedate = (Label)Rptitem.FindControl("lblacintime");
            Label actualin = (Label)Rptitem.FindControl("lblactualin");
            Label lblstatus = (Label)Rptitem.FindControl("lblstatus");

            string attstatus = lblstatus.Text.Trim();
            ddlReqType.SelectedValue = (attstatus == "A" ? "AB" : "LP");
            ddlReqType.Enabled = (attstatus == "A" ? false : true);
            if (attstatus == "A")
            {
                ddlReqType.Items.Remove("TC");
                ddlReqType.Items.Remove("LP");
                ddlReqType.Items.Remove("LA");
            }
            else
            {
                ListItem removeItem = ddlReqType.Items.FindByValue("AB");
                ddlReqType.Items.Remove(removeItem);
            }

            //lblIntime


            this.lbldadteIntime.Text = lblIntime.Text;
            this.lbldadte.Text = issuedate.Text;
            this.lbldadteTime.Text = actualin.Text;
            this.lbldadteOuttime.Text = lblIntime.Text;
            

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModalAbs();", true);
        }

        protected void lbntnAbsentApproval_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            
            string compsms = hst["compsms"].ToString();
            string compmail = hst["compmail"].ToString();
            string ssl = hst["ssl"].ToString();
            //Sender Informaiton
            string comcod = this.GetComeCode();
            string sendUsername = hst["userfname"].ToString();
            string empid = this.GetEmpID();
            string sendDptdesc = hst["dptdesc"].ToString();
            string sendUsrdesig = hst["usrdesig"].ToString();
            string compName = hst["comnam"].ToString();
            string htmtableboyd = "";
            string usrid = hst["usrid"].ToString();
            string deptcode = hst["deptcode"].ToString();
            


            string reqtype = this.ddlReqType.SelectedValue.ToString();
            string reqfor = reqtype == "AB" ? "Absent Approval" : reqtype == "LP" ? "Late Present Approval" : reqtype == "LA"? "Late Approval" : "Time of Correction";
            string reqdate = this.lbldadte.Text.Trim();
            string dayID =  Convert.ToDateTime(this.lbldadte.Text.Trim()).ToString("yyyyMMdd");
            string reqtimeIN = this.lbldadteIntime.Text.Trim();
            string reqtimeOUT = this.lbldadteOuttime.Text.Trim();
            string txtReson = txtAreaReson.Text.Trim();
            string usetime = "0:00";
            string postDat = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string qtype = this.Request.QueryString["Type"] ?? "";
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "INSERT_REQ_ATTN_CAHNGE", dayID, empid, reqdate, reqtype, reqtimeIN, reqtimeOUT, txtReson, usetime, usrid, postDat, "");


            if (!result)
            {

                string errMsg = "Update Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + errMsg + "');", true);
                return;
            }

            else
            {
                string trnid = this.GetattAppId(empid);
                string Messaged = "Successfully applied for "+ reqfor + ", please wait for approval";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messaged + "');", true);
                if (qtype != "MGT")
                {
                    this.SendNotificaion(reqdate, reqdate, trnid, deptcode, compsms, compmail, ssl, compName, htmtableboyd);

                }

                string eventdesc2 = "Details: " + htmtableboyd;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "New Request for "+ reqfor, htmtableboyd, Messaged);
            }
        }

        private string GetattAppId(string empid)
        {

            string comcod = this.GetComeCode();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.[SP_REPORT_HR_MGT_INTERFACE]", "GETATTAPPID", empid, "", "", "", "", "", "", "", "");
            string lstid = ds5.Tables[0].Rows[0]["ltrnid"].ToString().Trim();
            return lstid;
        }
        private void SendNotificaion(string frmdate, string todate, string ltrnid, string deptcode, string compsms, string compmail, string ssl, string compName, string htmtableboyd)
        {
            try
            {
                string comcod = this.GetComeCode();
                DataTable dt = (DataTable)ViewState["tblempinfo"];
                string reqtype = this.ddlReqType.SelectedValue.ToString();
                string reqfor = reqtype == "AB" ? "Absent Approval" : reqtype == "LP" ? "Late Present Approval" : reqtype == "LA" ? "Late Approval" : "Time of Correction";
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
                string uhostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_81_Hrm/F_84_Lea/";
                string currentptah = "EmpLvApproval?Type=Ind&comcod=" + comcod + "&refno=" + deptcode + "&ltrnid=" + ltrnid + "&Date=" + frmdate + "&usrid=" + suserid + "&RoleType=SUP";
                string totalpath = uhostname + currentptah;


                string maildescription = "Dear Sir, Please Approve My Request." + "<br> Employee ID Card : " + idcard + ",<br>" + "Employee Name : " + empname + ",<br>" + "Designation : " + empdesig + "," + "<br>" +
                     "Department Name : " + deptname + "," + "<br>" + "Request Type : " + reqfor + ",<br>" + " Request id: " + ltrnid + ". <br>";
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
                #endregion

                #region

                string subj = "New Request "+ reqfor; ;
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

                    bool Result_email = UserNotify.SendEmailPTL(hostname, portnumber, frmemail, psssword, subj, empname, empdesig, deptname, compName, tomail, msgbody);
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
        public string GetEmpID()
        {
            string Empid = "";
            string qtype = this.Request.QueryString["Type"] ?? "";
            if (qtype == "MGT")
            {
                Empid = this.Request.QueryString["empid"] ??"";//this.ddlEmpName.SelectedValue.ToString();

            }
            else
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                Empid = (hst["empid"].ToString() == "") ? "" : hst["empid"].ToString();

            }
            return (Empid);

        }
    }
}