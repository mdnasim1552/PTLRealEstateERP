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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDptUserCheck();
                this.ShowData();
            }


           
        }

        private void ShowData()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();

            string date = this.Request.QueryString["Date"] ?? "";
            
            string usrid = hst["usrid"].ToString();// (this.Request.QueryString["Type"] == "Ind") || (this.Request.QueryString["Type"] == "DeptHead") ? hst["usrid"].ToString() : "";
            string fDate = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
            string tDate = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
          
            string type = "";//(this.Request.QueryString["Type"]) == "Ind" || (this.Request.QueryString["Type"] == "DeptHead") ? "" : "Management";
            string DeptHead = "";//(this.Request.QueryString["Type"]) == "DeptHead" ? "DeptHead" : "";
            string id = this.Request.QueryString["ltrnid"] ?? "";

            DataSet ds1 = HRData.GetTransInfo(comcod, "DBO_HRM.SP_REPORT_HR_MGT_INTERFACE", "GETALLATTREQUEST", fDate, tDate, usrid, type, DeptHead, id, "", "", "");
               if (ds1 == null)
                return;
            ViewState["tblattreq"] = ds1.Tables[0];
            this.data_Bind();
        }

        private void data_Bind()
        {
            DataTable dt1 = (DataTable)ViewState["tblattreq"];
            string reqtype = this.Request.QueryString["Reqtype"].ToString();
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

            this.lbldadte.Text =Convert.ToDateTime(reqdate).ToString("dd-MMM-yyyy");
            this.ddlReqType.SelectedValue = reqtype;

            /// for employee information 
            /// 

            this.UserName.InnerText = empname;
            this.UDesignation.InnerText = empdesig;
            this.UDptment.InnerText = deptName;
            this.idcard.InnerText = "ID Card - "+ idcard;
            this.txtAreaReson.Text = empreson;
            //this.lblRemarks.Text = ds.Tables[1].Rows[0]["usrname"].ToString();
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
            //try
            //{
            //    string comcod = this.GetCompCode();
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string compName = hst["comnam"].ToString();

            //    DataTable dt = (DataTable)ViewState["tblempinfo"];


            //    string empUsrID = dt.Rows.Count == 0 ? "" : dt.Rows[0]["empuserid"].ToString();
            //    string empEmail = dt.Rows.Count == 0 ? "" : dt.Rows[0]["empEmail"].ToString();
            //    string idcard = dt.Rows.Count == 0 ? "" : dt.Rows[0]["idcard"].ToString();

            //    string to_empname = dt.Rows.Count == 0 ? "" : dt.Rows[0]["empname"].ToString();
            //    string leavedesc = dt.Rows.Count == 0 ? "" : dt.Rows[0]["lvtype"].ToString();



            //    string trnid = this.Request.QueryString["ltrnid"]??""; ;
            //    string remarks = this.txtremarks.Text;

            //    if (remarks.Length == 0)
            //    {
            //        string Messagesd = "Please Fill remarks";
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
            //        return;
            //    }

            //    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMLEAVAPP_ALL", trnid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            //    if (!result)
            //    {
            //        string Messagesd = "Deleted failed";
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
            //        return;

            //    }


            //    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert(' Not Apporved');", true);

            //    if (hst["compsms"].ToString() == "True")
            //    {
            //        string empid = ((Label)this.gvLvReq.Rows[0].FindControl("lblgvempid")).Text;
            //        string canname = hst["username"].ToString(); ;
            //        //string empid = this.ddlEmpName.SelectedValue.ToString();
            //        var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPPHONE", empid, "", "", "", "", "", "", "", "");

            //        if (ds == null)
            //            return;
            //        string phone = (string)ds.Tables[0].Rows[0]["phone"];
            //        SendSmsProcess sms = new SendSmsProcess();
            //        string SMSText = "Leave Canceled by : " + canname; // 
            //        bool resultsms = sms.SendSmmsPwd(SMSText, SMSText, phone);

            //    }

            //    else if (hst["compmail"].ToString() == "True")

            //    {

            //        string usrid = hst["usrid"].ToString();
            //        string deptcode = hst["deptcode"].ToString();
            //        string username = hst["username"].ToString();
            //        string frmdate = Convert.ToDateTime(((TextBox)this.gvLvReq.Rows[0].FindControl("txtgvlstdate")).Text.Trim()).ToString("dd-MMM-yyyy");
            //        string todate = Convert.ToDateTime(((Label)this.gvLvReq.Rows[0].FindControl("lblgvenddat")).Text.Trim()).ToString("dd-MMM-yyyy");
            //        string empid = ((Label)this.gvLvReq.Rows[0].FindControl("lblgvempid")).Text;

            //        //string empid = this.ddlEmpName.SelectedValue.ToString();
            //        var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPMAIL", empid, "", "", "", "", "", "", "", "");
            //        var ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEPTHEADDETAILS", usrid, "", "", "", "", "", "", "", "");

            //        string empname = (string)ds1.Tables[0].Rows[0]["name"];
            //        string empdesig = (string)ds1.Tables[0].Rows[0]["desig"];
            //        string deptName = (string)ds1.Tables[0].Rows[0]["deptname"];
            //        // string t = (string)ds1.Tables[0].Rows[0]["deptname"];
            //        if (ds == null)
            //            return;

            //        ///GET SMTP AND SMS API INFORMATION
            //        #region
            //        //SMTP
            //        DataSet dssmtpandmail = HRData.GetTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");
            //        string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            //        int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
            //        string frmemail = dssmtpandmail.Tables[0].Rows[0]["mailid"].ToString();
            //        string psssword = dssmtpandmail.Tables[0].Rows[0]["mailpass"].ToString();
            //        #endregion

            //        string mail = (string)ds.Tables[0].Rows[0]["mail"];
            //        string toEmpsub = "Leave Request Canceled";
            //        string toMSgBody = "Dear " + to_empname + ",\n" + " Reason : " + remarks + "\n" + ", Leave Canceled By : " + empname + ", Designation " + empdesig + ", Department Name" + deptName + "\n";
            //        bool Result_email = UserNotify.SendEmailPTL(hostname, portnumber, frmemail, psssword, toEmpsub, empname, empdesig, deptName, compName, mail, toMSgBody);

            //        bool result2 = UserNotify.SendNotification(toEmpsub, toMSgBody, empUsrID);
            //    }

            //    string Messagessd = "Deleted Success";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagessd + "');", true);
            //    ShowData();
            //}
            //catch (Exception ex)
            //{
            //    string Messagessd = "Something Wrong !!" + ex.Message;
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagessd + "');", true);
            //}


        }
    }
}