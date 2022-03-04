using EASendMail;
using RealERPLIB;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                ////    Response.Redirect("../../AcceessError.aspx");
                GetLeavType();
                string nextday = DateTime.Now.AddDays(+1).ToString("dd-MMM-yyyy");
                this.txtgvenjoydt1.Text = nextday;
                this.txtgvenjoydt2.Text = nextday;
                txtgvenjoydt2_CalendarExtender.StartDate = Convert.ToDateTime(this.txtgvenjoydt1.Text);
                this.txtaplydate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                getVisibilty();
                CreateTable();
                this.EmpLeaveInfo();
                this.ShowEmppLeave();
                GetCalCulateDay();

            }
        }
        private void getVisibilty()
        {
            string comcod = this.GetComeCode();
            if (comcod == "3365")
            {
             //   this.sspnlv.Visible = true;
                
            }
            else
            {
              //  this.sspnlv.Visible = false;


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
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETLEAVETYPE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlLvType.DataTextField = "hrgdesc";
            this.ddlLvType.DataValueField = "hrgcod";
            this.ddlLvType.DataSource = ds1.Tables[0];
            this.ddlLvType.DataBind();

        }

        private void GetCalCulateDay()
        {

            DataTable dt = (DataTable)Session["tblleavest"];
            DataTable dt1 = (DataTable)ViewState["tblSlevDay"];

            string gcod = this.ddlLvType.SelectedValue.ToString();
            DateTime fdate = Convert.ToDateTime(this.txtgvenjoydt1.Text);
            DateTime tdate = Convert.ToDateTime(this.txtgvenjoydt2.Text);

            getLevExitingLv(fdate.ToString(), tdate.ToString());

            DataTable extlv = (DataTable)ViewState["tblextlv"];

            if (extlv.Rows.Count == 0)
            {

                double isHalfday = (this.chkHalfDay.Checked ? 0.5 : 0.00);
                TimeSpan difference = (tdate - fdate); //create TimeSpan object
                string diffdays = "0.00";


                if(chkBoxSkippWH.Checked==false)
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
                






                DataView dv = dt.Copy().DefaultView;
                dv.RowFilter = ("gcod=" + gcod);
                dt = dv.ToTable();
                double ballv = Convert.ToDouble(dt.Rows[0]["balleave"]);
                double dfdays = Convert.ToDouble(diffdays);
                this.Duration.Value = diffdays;
                if (dfdays > ballv)
                {
                    string Messaged = "Oops!! Insufficient Leave Balance, Please conctact with your Managment Team";
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
                string Messaged = "Oops!! Already Applied between date";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                this.btnSave.Enabled = false;
            }


        }

        private void getLevExitingLv(string fdate, string tdate)
        {
            string comcod = this.GetComeCode();
            string empid = this.GetEmpID();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "GETEXITINGELEAVEBYEMPID", empid, fdate, tdate, "", "", "", "", "", "");


            ViewState["tblextlv"] = ds5.Tables[0];
        }

        protected void txtgvenjoydt1_TextChanged1(object sender, EventArgs e)
        {
            if(chkBoxSkippWH.Checked==true)
            {
                seLvDate();
            }
            
           

            DateTime fdate = Convert.ToDateTime(this.txtgvenjoydt1.Text);
            string nextday = fdate.AddDays(+0).ToString("dd-MMM-yyyy");
            this.txtgvenjoydt2.Text = nextday;
            txtgvenjoydt2_CalendarExtender.StartDate = fdate;
            GetCalCulateDay();
        }

        protected void txtgvenjoydt2_TextChanged(object sender, EventArgs e)
        {
            DateTime fdate = Convert.ToDateTime(this.txtgvenjoydt1.Text);
            //string nextday = fdate.AddDays(+0).ToString("dd-MMM-yyyy");
            //this.txtgvenjoydt2.Text = nextday;
            txtgvenjoydt2_CalendarExtender.StartDate = fdate;


            GetCalCulateDay();
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

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string Empid = (hst["empid"].ToString() == "") ? "93" : hst["empid"].ToString();
            return (Empid);

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string trnid = this.GetLeaveid();
            string empid = this.GetEmpID();

            string isHalfday = (this.chkHalfDay.Checked ? "True" : "False");
            string ttdays = this.Duration.Value.ToString();

            if (ttdays != "0")
            {
                string gcod = this.ddlLvType.SelectedValue.ToString();
                string frmdate = Convert.ToDateTime(this.txtgvenjoydt1.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txtgvenjoydt2.Text).ToString("dd-MMM-yyyy");
                string applydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");


                string reason = this.txtLeavLreasons.Text.Trim(); ;
                string addentime = this.txtaddofenjoytime.Text.Trim();
                string remarks = this.txtLeavRemarks.Text.Trim();
                string dnameadesig = this.txtdutiesnameandDesig.Text.Trim();
                string APRdate = "";

                bool result = false;
                if (chkBoxSkippWH.Checked == true)
                {
                    DataTable dt1 = (DataTable)ViewState["tblSlevDay"];
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        frmdate = Convert.ToDateTime(dt1.Rows[j]["leavday"]).ToString("dd-MMM-yyyy");
                        isHalfday = dt1.Rows[j]["isHalfday"].ToString();
                        result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPEMLEAVAPP_SKIPPHOLIDAY", trnid, empid, gcod, frmdate, frmdate, applydat, reason, remarks, APRdate, addentime, dnameadesig, ttdays.ToString(), isHalfday, usrid, "");

                    }

                }
                else
                {
                   
                      result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPEMLEAVAPP", trnid, empid, gcod, frmdate, todate, applydat, reason, remarks, APRdate, addentime, dnameadesig, ttdays.ToString(), isHalfday, usrid, "");

                }
                 
                if (!result)
                {
                    string Messaged = "Something wrong!! Please check your Apply Data";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                    return;
                }
                else
                {
                    string Messaged = "Congratulations !! Your leave applied, please wait for approval";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messaged + "');", true);


                    if (hst["compsms"].ToString() == "True")
                    {
                        switch (comcod)
                        {
                            case "3333":
                                break;

                            default:
                                this.SendSms(frmdate, todate);
                                break;
                        }
                    }

                    else if (hst["compmail"].ToString() == "True")
                    {
                        this.sendmail(frmdate, todate);

                    }
                }

                this.EmpLeaveInfo();
                this.ShowEmppLeave();

            }

        }

        private void EmpLeaveInfo()
        {
            ViewState.Remove("tblempleaveinfo");
            string comcod = this.GetComeCode();
            string empid = this.GetEmpID();
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

        private void ShowEmppLeave()
        {

            this.txtLeavLreasons.Text = "";
            this.txtLeavRemarks.Text = "";
            Session.Remove("tblleavest");
            string comcod = this.GetComeCode();
            string empid = this.GetEmpID();
            string aplydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");


            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVESTATUS02", empid, aplydat, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLeaveStatus.DataSource = null;
                this.gvLeaveStatus.DataBind();
                return;
            }

            Session["tblleavest"] = ds1.Tables[0];
            this.Data_Bind();

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



        private void sendmail(string frmdate, string todate)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string empid = this.GetEmpID();
            var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETSUPERVISERMAIL", empid, "", "", "", "", "", "", "", "");

            if (ds == null)
                return;

            double lapplied = Convert.ToDouble(this.Duration.Value.ToString());
            string leavedesc = this.ddlLvType.SelectedValue.ToString();


            string idcard = (string)ds.Tables[1].Rows[0]["idcard"];
            string empname = (string)ds.Tables[1].Rows[0]["name"];
            string empdesig = (string)ds.Tables[1].Rows[0]["desig"];
            string deptname = (string)ds.Tables[1].Rows[0]["deptname"];

            string maildescription = "Employee ID : " + idcard + " \n" + "Employee Name : " + empname + "\n" + "Designation : " + empdesig + "\n" +
                "Department Name : " + deptname + "\n" + "Leave Period : " + frmdate + " To " + todate + "\n" + "Leave Duration : " + lapplied +
                "\n" + "Leave Type : " + leavedesc + "\n" + "Leave Reason : " + this.txtLeavLreasons.Text;




            string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
            DataSet dssmtpandmail = HRData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");

            //SMTP
            string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
            string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
            string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
            string mailtousr = ds.Tables[0].Rows[0]["mail"].ToString();


            EASendMail.SmtpMail oMail = new EASendMail.SmtpMail("TryIt");

            //Connection Details 
            SmtpServer oServer = new SmtpServer(hostname);
            oServer.User = frmemail;
            oServer.Password = psssword;
            oServer.Port = portnumber;
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;


            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
            oMail.From = frmemail;
            oMail.To = mailtousr;
            oMail.Cc = frmemail;
            // oMail.Subject = subject;


            oMail.HtmlBody = "<html><head></head><body><pre style='max-width:700px;text-align:justify; font-weight: bold;font-size: 14px'>" + "Dear Sir," + "<br/>" + maildescription + "</pre></body></html>";


            try
            {

                oSmtp.SendMail(oServer, oMail);

                string Messaged = "Your message has been successfully sent";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messaged + "');", true);

            }
            catch (Exception ex)
            {
                string Messaged = "Error occured while sending your message." + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
            }







        }

        protected void gvleaveInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }



        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)ViewState["tblempleaveinfo"];
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string trnid = ((Label)this.gvleaveInfo.Rows[RowIndex].FindControl("lgvltrnleaveid")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMLEAVAPP", trnid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                string Messaged = "Deleted Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                return;
            }
            string Messagesd = "Deleted Success";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagesd + "');", true);


            int rowindex = (this.gvleaveInfo.PageSize) * (this.gvleaveInfo.PageIndex) + RowIndex;
            dt.Rows[rowindex].Delete();
            DataView dv = dt.DefaultView;
            ViewState.Remove("tblempleaveinfo");
            ViewState["tblempleaveinfo"] = dv.ToTable();
            this.gvleaveInfo.DataSource = dv.ToTable();
            this.gvleaveInfo.DataBind();
        }

       

        protected void gvInterstLev_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


        private void seLvDate(){
            DataTable dt1 = (DataTable)ViewState["tblSlevDay"];
            string leavday = Convert.ToDateTime(this.txtgvenjoydt1.Text).ToString("dd-MMM-yyyy");
            string isHalfday = (this.CheckBox1.Checked ? "True" : "False");

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
            if (chkBoxSkippWH.Checked==false)
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
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            seLvDate();
        }
    }
}