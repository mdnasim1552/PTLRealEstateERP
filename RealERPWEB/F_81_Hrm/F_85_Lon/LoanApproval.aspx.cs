using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
namespace RealERPWEB.F_81_Hrm.F_85_Lon
{
    public partial class LoanApproval : System.Web.UI.Page
    {
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
            //    Response.Redirect("../../AcceessError.aspx");
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (dr1.Length == 0)
            //    Response.Redirect("../AcceessError.aspx");
            //((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();

            this.txtcreateDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            string empid = this.Request.QueryString["refno"].ToString() ?? "";

            string loanid = this.Request.QueryString["ltrnid"].ToString() ?? "";
            this.stepIDNEXT.Value = this.Request.QueryString["RoleType"].ToString() ?? "";
            this.GetEmplist();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();

            this.GetLoanType();
            this.GetGross();
            this.GetPrevLoan();
            GetApprovalLog(loanid);
            this.AllVie_Data(empid, loanid);
            this.ComponentVisibale();

        }

        private void ComponentVisibale()
        {
            this.txtcreateDate.Enabled = false;          
            this.txtLoanDescc.Enabled = false;
           
           
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetEmplist()
        {
            string comcod = this.GetCompCode();
            string txtEmpname = "%%";
            string type = "";
            switch (comcod)
            {
                case "3365":
                case "3101":
                    type = "lnemp";
                    break;
                default:
                    type = "";
                    break;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = this.Request.QueryString["refno"].ToString() ?? "";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLNEMPLIST", txtEmpname, type, "", "", "", "", "", "", "");
            this.ddlEmpList.DataTextField = "empname";
            this.ddlEmpList.DataValueField = "empid";
            this.ddlEmpList.DataSource = ds1.Tables[0];
            this.ddlEmpList.DataBind();
            if (empid.Length != 0)
            {
                this.ddlEmpList.SelectedValue = empid;
            }
            this.ddlEmpList.Enabled = false;

        }

        private void GetLoanType()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLOANTYPE", "", "", "", "", "", "", "", "", "");
            this.ddlLoanType.DataTextField = "loantype";
            this.ddlLoanType.DataValueField = "gcod";
            this.ddlLoanType.DataSource = ds1.Tables[0];
            this.ddlLoanType.DataBind();
            ddlLoanType.Items.Insert(0, new ListItem("All loan", ""));
            ddlLoanType.Items[0].Attributes["disabled"] = "disabled";

        }

        private void GetPrevLoan()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string empid = this.ddlEmpList.SelectedValue.ToString() ?? "";
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "PREVLOANAMT", empid, "", "", "", "", "", "", "", "");
            if (ds == null || ds.Tables.Count == 0)
                return;
            DataTable dt1 = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];
            this.txtPloanAmt.Text = dt1.Rows[0]["ttlloanamt"].ToString();

            int loanid = Convert.ToInt32(dt2.Rows[0]["lnno"]) + 1;

            this.txtLoanId.Text = "Ln-" + loanid.ToString();
        }

        private void GetGross()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string empid = this.ddlEmpList.SelectedValue.ToString() ?? "";
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "GETGROSS", empid, "", "", "", "", "", "", "", "");
            if (ds == null || ds.Tables.Count == 0)
                return;
            DataTable dt1 = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];
            DataTable dt3 = ds.Tables[2];
            this.txtGMS.Text = dt1.Rows[0]["grosssal"].ToString() ?? "";
            this.txtPFAmt.Text = dt2.Rows[0]["pffund"].ToString() ?? "";
            this.txtTax.Text = dt3.Rows[0]["inctax"].ToString() ?? "";

        }

        private void GetApprovalLog(string loanid)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string empid = this.ddlEmpList.SelectedValue.ToString() ?? "";
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "GETLOANAPPROVEDLOG", loanid, "", "", "", "", "", "", "", "");
            if (ds == null || ds.Tables.Count == 0)
                return;

            this.gvLoanApprovalLog.DataSource = ds.Tables[0];
            this.gvLoanApprovalLog.DataBind();
        }

        private void AllVie_Data(string empid, string lnid)
        {

            string comcod = this.GetCompCode();

            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "GETLOANBYID", empid, lnid, "", "", "", "", "", "", "", "");
            if (ds == null || ds.Tables.Count == 0)
                return;
            DataTable dt = ds.Tables[0];


            this.txtLoanId.Text = "Ln-" + dt.Rows[0]["id"].ToString();
            this.txtcreateDate.Text = Convert.ToDateTime(dt.Rows[0]["createdate"]).ToString("dd-MMM-yyyy");

            this.txtLoanAmt.Text = Convert.ToDouble(dt.Rows[0]["loanamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtInstNum.Text = Convert.ToInt32(dt.Rows[0]["instlnum"]).ToString("#,##0;(#,##0); ");
            this.txtAmtPerIns.Text = Convert.ToDouble(dt.Rows[0]["perinstlamt"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtStd.Text = Convert.ToDouble(dt.Rows[0]["statdeduction"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtOI.Text = Convert.ToDouble(dt.Rows[0]["othincome"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtOD.Text = Convert.ToDouble(dt.Rows[0]["othdeduction"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtrt.Text = Convert.ToDouble(dt.Rows[0]["rate"]).ToString("#,##0.00;(#,##0.00); ");

            this.txtEffDate.Text = Convert.ToDateTime(dt.Rows[0]["effdate"]).ToString("dd-MMM-yyyy");
            ddlLoanType.ClearSelection();
            string loantype = dt.Rows[0]["loantype"].ToString().Trim();
            this.ddlEmpList.SelectedValue = empid;
            ddlLoanType.Items.FindByValue(loantype).Selected = true;
            this.txtLoanDescc.Text = dt.Rows[0]["loandesc"].ToString() ?? "";


        }
        protected void ddlEmpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetGross();
            this.GetPrevLoan();
        }


        //update loan
        protected void lnkUpdate_Click(object sender, EventArgs e)
        {

            string Message;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string compsms = hst["compsms"].ToString();
            string compmail = hst["compmail"].ToString();
            string ssl = hst["ssl"].ToString();
            string compName = hst["comnam"].ToString();
            string usrid = hst["usrid"].ToString();
            string deptcode = hst["deptcode"].ToString();

            string comcod = this.GetCompCode();
            string empid = this.ddlEmpList.SelectedValue.ToString();
            //string empid = hst["empid"].ToString() ?? "";
            string id = this.txtLoanId.Text.ToString().Remove(0, 3);
            string loantype = ddlLoanType.SelectedValue.ToString() ?? "";


            string loanamt = Convert.ToDouble("0" + (this.txtLoanAmt.Text.Trim())).ToString();
            string perinstlamt = Convert.ToDouble("0" + (this.txtAmtPerIns.Text.Trim())).ToString();
            string rate = Convert.ToDouble("0" + (this.txtrt.Text.Trim())).ToString();
            string instlnum = Convert.ToInt32("0" + (this.txtInstNum.Text.Trim())).ToString();

            string othincm = Convert.ToDouble("0" + (this.txtOI.Text.Trim())).ToString();
            string othdeduct = Convert.ToDouble("0" + (this.txtOD.Text.Trim())).ToString();
            string stddeduct = Convert.ToDouble("0" + (this.txtStd.Text.Trim())).ToString();



            string effedat = Convert.ToDateTime(this.txtEffDate.Text).ToString("dd-MMM-yyyy") ?? "";
            string pstdusredt = hst["usrid"].ToString();
            string pstdssnedt = hst["session"].ToString();
            string pstdtrmnledt = hst["compname"].ToString();
            string postdateedited = System.DateTime.Now.ToString("dd-MMM-yyy") ?? "";
            
            string createDate = Convert.ToDateTime(this.txtcreateDate.Text).ToString("dd-MMM-yyyy") ?? "";


            string loantypeDesc = ddlLoanType.SelectedItem.ToString() ?? "";
            string note = this.txtnote.Text.ToString() ?? "";
            string loandesc = this.txtLoanDescc.Text.ToString() ?? "";

            string stepid = this.stepIDNEXT.Value+1;


            //maincode = (editedid != "") ? editedid : maincode;
            bool result = HRData.UpdateTransHREMPInfo3(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "LOANUPDATEPROCESS", empid, id, loantype, loanamt, instlnum, perinstlamt, rate, effedat,
                stddeduct, othincm, othdeduct, stepid, note, pstdusredt, pstdtrmnledt, pstdssnedt, postdateedited, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

             

            if (result == true)
            {
                
                Message = "Successfully Updated";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);
                string subj = "Loan Process Approved";
                string htmbody = "Loan Type: " + loantypeDesc + ", Loan Amount: " + loanamt + ", Purpose  of Loan: " + loandesc;
                this.SendNotificaion(createDate, effedat, id, deptcode, compsms, compmail, ssl, compName, htmbody, subj, stepid);

 
            }
            else
            {
                
                Message = "Update Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
          
            }

           

        }



        protected void lnkCancel_Click(object sender, EventArgs e)
        {

            string empid = this.ddlEmpList.SelectedValue.ToString();
            string loanid = this.txtLoanId.Text.ToString().Remove(0, 3);
            string lnstatus = "0";
            string lsApproved = "0";
            string IsCancelled = "1";
            string stepid = "8";
            this.ChangeloanStatus(empid, loanid, lnstatus, lsApproved, IsCancelled, stepid);
        }
        //loan process approval
        protected void lnkApprov_Click(object sender, EventArgs e)
        {
            string stepid = this.stepIDNEXT.Value+1;

            string empid = this.ddlEmpList.SelectedValue.ToString();
            string loanid = this.txtLoanId.Text.ToString().Remove(0, 3);
            string lnstatus = "1";
            string lsApproved = "1";
            string lsCancelled = "0";
            this.ChangeloanStatus(empid, loanid, lnstatus, lsApproved, lsCancelled, stepid);
        }
        //change  loan status
        private void ChangeloanStatus(string empid, string loanid, string lnstatus, string lsApproved, string lsCancelled, string stepid)
        {
            string note = this.txtnote.Text.ToString() ?? "";
            if (note == "" && lsCancelled == "1")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#ApplyLoan", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#ApplyLoan').hide();", true);

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "checkEmptyNote();", true);
                return;
            }
            string Message;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string compsms = hst["compsms"].ToString();
            string compmail = hst["compmail"].ToString();
            string ssl = hst["ssl"].ToString();
            string compName = hst["comnam"].ToString();
            string usrid = hst["usrid"].ToString();
            string deptcode = hst["deptcode"].ToString();
            string comcod = this.GetCompCode();
            string loantype = ddlLoanType.SelectedValue.ToString() ?? "";
            string loantypeDesc = ddlLoanType.SelectedItem.ToString() ?? "";

            string loanamt = Convert.ToDouble("0" + (this.txtLoanAmt.Text.Trim())).ToString();
            string perinstlamt = Convert.ToDouble("0" + (this.txtAmtPerIns.Text.Trim())).ToString();
            string rate = Convert.ToDouble("0" + (this.txtrt.Text.Trim())).ToString();

            string instlnum = Convert.ToInt32("0" + (this.txtInstNum.Text.Trim())).ToString();

            string othincm = Convert.ToDouble("0" + (this.txtOI.Text.Trim())).ToString();
            string othdeduct = Convert.ToDouble("0" + (this.txtOD.Text.Trim())).ToString();
            string stddeduct = Convert.ToDouble("0" + (this.txtStd.Text.Trim())).ToString();



            string loandesc = this.txtLoanDescc.Text.ToString();

            string effedat = Convert.ToDateTime(this.txtEffDate.Text).ToString("dd-MMM-yyyy") ?? "";
            string pstdusredt = hst["usrid"].ToString();
            string pstdssnedt = hst["session"].ToString();
            string pstdtrmnledt = hst["compname"].ToString();
            string postdateedited = System.DateTime.Now.ToString("dd-MMM-yyy") ?? "";
            string createDate = Convert.ToDateTime(this.txtcreateDate.Text).ToString("dd-MMM-yyyy") ?? "";



            bool result = HRData.UpdateTransHREMPInfo3(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "LOANUPDATEPROCESS", empid, loanid, loantype, loanamt, instlnum, perinstlamt, rate, effedat,
               stddeduct, othincm, othdeduct, stepid, note, pstdusredt, pstdtrmnledt, pstdssnedt, postdateedited, lsApproved, lsCancelled, "", "", "", "", "", "", "", "", "", "", "", "", "");

            //bool result = HRData.UpdateTransHREMPInfo3(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "LOANUPDATEPROCESS", empid, loanid, loantype, loanamt, instlnum, perinstlamt, loandesc,
            //    rate, effedat, "", "", "", "", pstdusredt, pstdssnedt, pstdtrmnledt, postdateedited, createDate, stddeduct, othincm, othdeduct, lnstatus, lsApproved, lsCancelled, note, "", "", "", "", "", "", "");
            if (result == true)
            {

                Message = "Successfully Updated";
                //if (qtype != "MGT")
                //{
                string subj = lsCancelled == "1" ? "Loan Request Cancel" : lsApproved == "1" ? "Loan Request Approved" : "New Loan Request";
                string htmbody = "Loan Type: " + loantypeDesc + ", Loan Amount: " + loanamt + ", Purpose  of Loan" + loandesc;
                this.SendNotificaion(createDate, effedat, loanid, deptcode, compsms, compmail, ssl, compName, htmbody, subj, stepid);

                // }

                string eventdesc2 = "Details: " + "Loan Process Approval";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "New Leave Request", "", "");

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#ApplyLoan", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#ApplyLoan').hide();", true);


            }
            else
            {
                Message = "Applied Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#ApplyLoan", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#ApplyLoan').hide();", true);



            }


        }


        //notification process
        private void SendNotificaion(string frmdate, string todate, string ltrnid, string deptcode, string compsms, string compmail, string ssl, string compName, string htmtableboyd, string subj, string stepid)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                DataTable dt = (DataTable)ViewState["tblempinfo"];
                string leavedesc = this.ddlLoanType.SelectedItem.ToString();
                string empid = this.ddlEmpList.SelectedValue.ToString();


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
                string callType = "GETMGTHEADDATA";
                if (stepid == "2")
                {
                    callType = "GETDPTMGTHEADDATA";

                }
                else
                {
                    callType = "GETINTERFACESTEPUSERINFO";

                }


                var ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", callType, empid, "", ltrnid, stepid, "", "", "", "", "");

                if (ds1 == null)
                    return;
                string supphone = "";
                string idcard = (string)ds1.Tables[1].Rows[0]["idcard"];
                string empname = (string)ds1.Tables[1].Rows[0]["name"];
                string empdesig = (string)ds1.Tables[1].Rows[0]["desig"];
                string deptname = (string)ds1.Tables[1].Rows[0]["deptname"];
                #endregion

                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                {
                    string suserid = ds1.Tables[0].Rows[0]["suserid"].ToString();
                    string tomail = ds1.Tables[0].Rows[0]["mail"].ToString();
                    string roletype = (string)ds1.Tables[0].Rows[0]["roletype"];
                    string uhostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_81_Hrm/F_85_Lon/";
                    string currentptah = "LoanApproval?Type=&comcod=" + comcod + "&refno=" + empid + "&ltrnid=" + ltrnid + "&Date=" + frmdate + "&usrid=" + suserid + "&RoleType=" + stepid;
                    string totalpath = uhostname + currentptah;


                    string maildescription = "Dear Sir, Please Approve loan Request." + "<br> Employee ID Card : " + idcard + ",<br>" + "Employee Name : " + empname + ",<br>" + "Designation : " + empdesig + "," + "<br>" +
                         "Department Name : " + deptname + "," + "<br>" + "Loan Type : " + leavedesc + ",<br>" + " Request id: " + ltrnid + ". <br>";
                    maildescription += htmtableboyd;
                    maildescription += "<div style='color:red'><a style='color:blue; text-decoration:underline' href = '" + totalpath + "'>Click for Approved</a> or Login ERP Software and check Loan Interface</div>" + "<br/>";

                    #region

                    string msgbody = maildescription;
                    bool result2 = UserNotify.SendNotification(subj, msgbody, suserid);
                    if (compsms == "True")
                    {
                        SendSmsProcess sms = new SendSmsProcess();
                        string SMSText = "New Loan Request Create Date : " + frmdate + " Effective Date " + todate;// 
                        bool resultsms = sms.SendSmmsPwd(comcod, SMSText, supphone);
                    }
                    if (compmail == "True")
                    {
                        bool Result_email = UserNotify.SendEmailPTL(hostname, portnumber, frmemail, psssword, subj, empname, empdesig, deptname, compName, tomail, msgbody, isSSL);
                        if (Result_email == false)
                        {
                            string Messagesd = "Loan Applied  but Notification has not been sent, Email or SMTP info Empty";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                        }
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                string Messagesd = "Loan Applied but Notification has not been sent " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
            }

        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}