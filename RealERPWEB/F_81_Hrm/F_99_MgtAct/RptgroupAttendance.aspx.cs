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
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Net.Mail;

namespace RealERPWEB.F_81_Hrm.F_99_MgtAct
{
    public partial class RptgroupAttendance : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        static string prevPage = String.Empty;
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();
        private Hashtable _errObj;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtFdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Attendence Summery";

                Hashtable hst = (Hashtable)Session["tblLogin"];
               
                if (hst["comcod"].ToString().Substring(0, 1) == "8")
                {
                    this.comlist.Visible = true;
                    this.Company();
                }
                this.ShowGroupAttendance();
                if (GetCompCode() == "3367")
                {
                    this.lnksendMail.Visible = true;
                }
            }
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowGroupAttendance();
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.ddlComName.SelectedValue.Length > 0 ? this.ddlComName.SelectedValue.ToString() : comcod;
            return comcod;
        }

        private void Company()
        {
            string comcod = this.GetCompCode();
            string consolidate = "";
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", "COMPLIST", consolidate, "", "", "", "", "", "", "", "");
            this.ddlComName.DataTextField = "comsnam";
            this.ddlComName.DataValueField = "comcod";
            this.ddlComName.DataSource = ds1.Tables[0];
            this.ddlComName.DataBind();

        }


        protected void gvRptAttn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvRptAttn.PageIndex = e.NewPageIndex;
            //this.Data_Bind();

        }
        private void ShowGroupAttendance()
        {
            string comcod = this.GetCompCode();
            string todydate = this.txtFdate.Text;
            string calltype = this.Request.QueryString["Type"].ToString() == "Dept" ? "GETDEPARTATTENDENCE" : "GETGROUPATTENDENCE";

            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_GROUP_ATTENDENCE", calltype, todydate, "", "", "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                string totmsg = "No Data Found";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + totmsg + "');", true);

                return;
            }
            ViewState["tblgroupAttendace"] = ds.Tables[0];
            ViewState["tblgroupAttenPersen"] = ds.Tables[1];
            ViewState["tblattgraph"] = ds.Tables[2];
            this.Data_Bind();
        }



        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblgroupAttendace"];
            DataTable dt2 = (DataTable)ViewState["tblgroupAttenPersen"];

            this.gvRptAttn.DataSource = dt;
            this.gvRptAttn.DataBind();

            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "drawChart();", true);
            if(this.Request.QueryString["Type"] != "Dept")
            {
                this.gvAttPersent.DataSource = dt2;
                this.gvAttPersent.DataBind();
            }
           


            double tostaff = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ttlstap)", "")) ? 0.00 : dt.Compute("Sum(ttlstap)", "")));
            double present = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(present)", "")) ? 0.00 : dt.Compute("Sum(present)", "")));
            double late = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(late)", "")) ? 0.00 : dt.Compute("Sum(late)", "")));
            double eleave = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(earlyLev)", "")) ? 0.00 : dt.Compute("Sum(earlyLev)", "")));
            double onlaeve = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(onlev)", "")) ? 0.00 : dt.Compute("Sum(onlev)", "")));
            double absent = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(absnt)", "")) ? 0.00 : dt.Compute("Sum(absnt)", "")));
            double prepostaff = 0.00, latepostaff = 0.00, eleavepostaff = 0.00, onlaevepostaff = 0.00, absentpostaff = 0.00;
            prepostaff = (present * 100) / tostaff;
            latepostaff = (late * 100) / tostaff;
            eleavepostaff = (eleave * 100) / tostaff;
            onlaevepostaff = (onlaeve * 100) / tostaff;
            absentpostaff = (absent * 100) / tostaff;
            this.txtpresent.Text = prepostaff.ToString("#,##0;(#,##0); ");
            this.txtlate.Text = latepostaff.ToString("#,##0;(#,##0); ");
            this.txtearlylev.Text = eleavepostaff.ToString("#,##0;(#,##0); ");
            this.txtonleave.Text = onlaevepostaff.ToString("#,##0;(#,##0); ");
            this.txtabsent.Text = absentpostaff.ToString("#,##0;(#,##0); ");




            //((Label)this.gvAttPersent.FooterRow.FindControl("lblstaf")).Text = tostaff.ToString("#,##0;(#,##0); ");
            //((Label)this.gvAttPersent.FooterRow.FindControl("lblprs")).Text = prepostaff.ToString("#,##0;(#,##0); ");
            //((Label)this.gvAttPersent.FooterRow.FindControl("lblfotlate")).Text = latepostaff.ToString("#,##0;(#,##0); ");
            //((Label)this.gvAttPersent.FooterRow.FindControl("lbleleave")).Text = eleavepostaff.ToString("#,##0;(#,##0); ");
            //((Label)this.gvAttPersent.FooterRow.FindControl("lblol")).Text = onlaevepostaff.ToString("#,##0;(#,##0); ");
            //((Label)this.gvAttPersent.FooterRow.FindControl("lblabs")).Text = absentpostaff.ToString("#,##0;(#,##0); ");
        }



        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void gvRptAttn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkgvcomname = (HyperLink)e.Row.FindControl("hlnkgvcomname");
                HyperLink hlnkgvdept = (HyperLink)e.Row.FindControl("hlnkgvdept");
                //  string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string deptcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deptcode")).ToString();
                if (comcod == "")
                {
                    return;
                }
                hlnkgvcomname.Font.Bold = true;
                hlnkgvcomname.Style.Add("color", "blue");
                hlnkgvcomname.NavigateUrl = "~/F_81_Hrm/F_99_MgtAct/LinkLateElLeaveAAbs.aspx?Type=LELLAndAbsent&comcod=" + comcod + "&Date=" + Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");

                hlnkgvdept.Font.Bold = true;
                hlnkgvdept.Style.Add("color", "Maroon");
                hlnkgvdept.NavigateUrl = "~/F_81_Hrm/F_99_MgtAct/LinkLateElLeaveAAbs.aspx?Type=LELLAndAbsent&comcod=" + comcod + "&Date=" + Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy") + "&dept=" + deptcode;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string fdate = this.txtFdate.Text.ToString();

            string todydate = this.txtFdate.Text;
            string title = this.Request.QueryString["Type"].ToString() == "Dept" ? "Daily Attendence Department wise" : "Daily Attendence Group";

            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_GROUP_ATTENDENCE02", "RPTLATEEONANDABSENTDET", todydate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblLVatlet"] = ds1.Tables[2];
            
            DataTable dt = (DataTable)ViewState["tblgroupAttendace"];
            DataTable dt1 = (DataTable)ViewState["tblLVatlet"];
            DataTable dt3 = (DataTable)ViewState["tblattgraph"];

            if (dt1 == null || dt1.Rows.Count == 0)
                return;

            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.ERptGroupAtt>();
            var lst1 = dt1.DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.Elvlateabbs02>();
            var lst3 = dt3.DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.AttgraphLbl>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptGroupAtt", lst, lst1, lst3);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", comname));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", title));
            Rpt1.SetParameters(new ReportParameter("printdate", printdate));
            Rpt1.SetParameters(new ReportParameter("Fdate", fdate));

            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    
    
    
         protected void lnksendMail_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string fdate = this.txtFdate.Text.ToString();

            string todydate = this.txtFdate.Text;
            string title = this.Request.QueryString["Type"].ToString() == "Dept" ? "Daily Attendence Department wise" : "Daily Attendence Group";

            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_GROUP_ATTENDENCE02", "RPTLATEEONANDABSENTDET", todydate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblLVatlet"] = ds1.Tables[2];

            DataTable dt = (DataTable)ViewState["tblgroupAttendace"];
            DataTable dt1 = (DataTable)ViewState["tblLVatlet"];
            DataTable dt3 = (DataTable)ViewState["tblattgraph"];

            if (dt1 == null || dt1.Rows.Count == 0)
                return;

            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.ERptGroupAtt>();
            var lst1 = dt1.DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.Elvlateabbs02>();
            var lst3 = dt3.DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.AttgraphLbl>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptGroupAtt", lst, lst1, lst3);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", comname));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", title));
            Rpt1.SetParameters(new ReportParameter("printdate", printdate));
            Rpt1.SetParameters(new ReportParameter("Fdate", fdate));

            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath("~") + "\\Upload" + "\\attreport\\");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }


            string tdate = Convert.ToDateTime(System.DateTime.Today).ToString("ddMMyyy");
            string apppath = Server.MapPath("~") + "\\Upload" + "\\attreport\\" + tdate + ".pdf"; ;

            string mimeType, encoding, extension;
            string[] streamids; Microsoft.Reporting.WinForms.Warning[] warnings;
            string format = "PDF";
            byte[] bytes = Rpt1.Render(format, "", out mimeType, out encoding, out extension, out streamids, out warnings);
            //save the pdf byte to the folder
            FileStream fs = new FileStream(apppath, FileMode.OpenOrCreate);
            byte[] data = new byte[fs.Length];
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            string filepath = "~/../../../Upload/attreport/" + tdate + ".pdf";
            this.AttReport.InnerHtml = "<iframe src='" + filepath + "' width='100%' height='600px'></iframe>";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openReportModal();", true);
 


        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string compsms = hst["compsms"].ToString();
            string compmail = hst["compmail"].ToString();
            string ssl = hst["ssl"].ToString();

            string compName = hst["comnam"].ToString();
            this.SendNotificaion(compsms, compmail, ssl, compName);
        }

        private void SendNotificaion(string compsms, string compmail, string ssl, string compName)
        {
            try
            {
                //this.AutoSavePDF();
                string comcod = this.GetCompCode();


                ///GET SMTP AND SMS API INFORMATION
                #region
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string sendUsername = hst["userfname"].ToString();
                string sendDptdesc = hst["dptdesc"].ToString();
                string sendUsrdesig = hst["usrdesig"].ToString();
                string usrid = hst["usrid"].ToString();
                string deptcode = hst["deptcode"].ToString();


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
             
                string subj = "Daily Attendance Report";
                string tomail = "chairman@epicpl.com";
                //string tomail = "rakib@pintechltd.com";


                string tdate = Convert.ToDateTime(System.DateTime.Today).ToString("ddMMyyy");
                string apppath = Server.MapPath("~") + "\\Upload" + "\\attreport\\" + tdate + ".pdf"; ;
    
        
                if (tomail == "")
                {
                    string Messagesd = "Update the suppliers email address to be used for email notifications";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagesd + "');", true);
                    return;
                }


                string msgbody = @"
<html lang=""en"">
	<head>	
		<meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">
		<title>
			Work Order
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
	<body>
<p>Dear Sir/ Madam,</p>
<p>Hope you are having a good day. Please check the attached file for your kind consideration.</p>

<p style='text-danger'>Note :This is Software generated mail. </p>

<p></br></p>
<p>Best Regards,</p>
<p>" + sendUsername + "</p><p> " + sendUsrdesig + " </p><p> " + compName + " </p></body></html>";
                #endregion



                if (compmail == "True")
                {
                    bool Result_email = this.SendEmailPTLSUP(hostname, portnumber, frmemail, psssword, subj, sendUsername, sendUsrdesig, sendDptdesc, compName, tomail, msgbody, isSSL, apppath);
                    if (Result_email == false)
                    {
                        string Messagesd = "Email has not been sent, Email or SMTP info Empty";
                      //  this.lblMsg.InnerText = "Mail sent successfully!";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                        return;
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "sentMail();", true);
                        //string Messagesd = "Email has been sent";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagesd + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                string Messagesd = "Email has not been sent " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                return;

            }

        }

        public bool SendEmailPTLSUP(string hostname, int portnumber, string frmemail, string psssword, string subj, string sendUsername, string sendUsrdesig, string sendDptdesc, string compName, string tomail, string msgbody, bool isSSL, string apppath)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                SmtpClient client = new SmtpClient(hostname, portnumber);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = isSSL;
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(apppath);
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.From = new MailAddress(frmemail);
                string body = string.Empty;
                msg.To.Add(new MailAddress(tomail));


                //msg.CC.Add(new MailAddress("inforakib831@gmailcom"));


                msg.CC.Add(new MailAddress("hra.epicpl@gmailcom"));
                msg.CC.Add(new MailAddress("csd.epicpl@gmail.com"));
                msg.CC.Add(new MailAddress("architect.epicpl@gmail.com"));
                msg.CC.Add(new MailAddress("purchase.epicpl@gmail.com"));
                msg.CC.Add(new MailAddress("brand.epicpl@gmail.com"));
                msg.CC.Add(new MailAddress("md@epicpl.com"));
                msg.CC.Add(new MailAddress("lokman@epicpl.com"));
                msg.CC.Add(new MailAddress("brand@epicpl.com"));
                msg.CC.Add(new MailAddress("sales@epicpl.com"));
                msg.CC.Add(new MailAddress("engineering@epicpl.com"));
                msg.CC.Add(new MailAddress("finance@epicpl.com"));
                msg.CC.Add(new MailAddress("saifur.epicpl@gmail.com"));
                msg.CC.Add(new MailAddress("didarepicpl@gmail.com"));
                msg.CC.Add(new MailAddress("hamid.epicpl@gmail.com"));
                msg.CC.Add(new MailAddress("salahuddin.epicpl@gmail.com"));


                msg.Subject = subj;
                body += msgbody;
                // body += "<br />Thanks & Regards<br/>" + sendUsername + "<br>" + sendUsrdesig + "<br>" + sendDptdesc + "<br>" + compName;
                msg.Body = body;
                msg.Attachments.Add(attachment);
                msg.IsBodyHtml = true;
                try
                {
                    client.Send(msg);
                    return true;
                }
                catch (Exception ex)
                {
                    this.SetError(ex);
                    return false;

                }

            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }


        }

        private void SetError(Exception exp)
        {
            this._errObj["Src"] = exp.Source;
            this._errObj["Msg"] = exp.Message;
            this._errObj["Location"] = exp.StackTrace;
        }

    }
}