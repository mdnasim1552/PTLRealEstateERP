using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RealERPLIB
{
    public class SendNotifyForUsers : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        private Hashtable _errObj;

        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        public bool SendNotification(string ntitle, string ndetails, string recvid)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["userfname"].ToString();
                string ncreatedby = hst["usrid"].ToString();
                string ncreated = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                ndetails = ndetails + "Created by :"+ username;

                DataSet ds3 = purData.GetTransInfo(comcod, "SP_REPORT_NOTICE", "INSERT_NOTIFICAIOTN_USER_WISE", ntitle, ndetails, ncreated, ncreatedby, recvid, "", "");
               


                return true;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }// try


        }



        
        public bool SendEmailPTL(string hostname, int portnumber,  string frmemail, string psssword, string subj, string sendUsername, string sendUsrdesig, string sendDptdesc, string compName, string tomail, string msgbody, bool isSSL)
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
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(frmemail);
                string body = string.Empty;
                msg.To.Add(new MailAddress(tomail));
               //  msg.CC.Add(new MailAddress("ibrahim.diu26@gmail.com"));
                //msg.Bcc.Add(new MailAddress("nahid@pintechltd.com"));
                msg.Subject = subj;
                body += msgbody;
               // body += "<br />Thanks & Regards<br/>" + sendUsername + "<br>" + sendUsrdesig + "<br>" + sendDptdesc + "<br>" + compName;
                msg.Body = body;
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
            }// try


        }
        private void SetError(Exception exp)
        {
            this._errObj["Src"] = exp.Source;
            this._errObj["Msg"] = exp.Message;
            this._errObj["Location"] = exp.StackTrace;
        }
    }
}
