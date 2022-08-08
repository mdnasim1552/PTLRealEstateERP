using AjaxControlToolkit;
using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.Tickets
{
    public partial class CreateTicket : System.Web.UI.Page
    {
        ProcessAccess _linkVendorDb = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
               
                this.txtTdate.Text = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                checkUser();

            }
        }
        private void checkUser()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string userId = hst["usrid"].ToString();
            string comcod = GetCompCode();
            DataSet ds1 = _linkVendorDb.GetcheckUser(comcod, userId);
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {
                string Url1 = "Dashboard.aspx";
                Response.Redirect(Url1);
            }
            else
            {
                Session["TicketUseId"] = ds1.Tables[0].Rows[0]["USERID"].ToString();
                Session["TicketComCod"] = ds1.Tables[0].Rows[0]["COMCOD"].ToString();

            }


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

       
        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            this.btnSave.Disabled = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string username = Session["TicketUseId"].ToString();
            string comcod = Session["TicketComCod"].ToString();
            string usrid = hst["usrid"].ToString();

            string ticketType = this.ddlTicketType.SelectedValue.ToString();
            string txtTdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            string txtTicketDesc = this.txtTicketDesc.Text.ToString();
            string priority = this.ddlPriority.SelectedValue.ToString();

            if (txtTicketDesc != "" && txtTicketDesc != null)
            {

                DataSet ds = _linkVendorDb.InsertTicketSP(comcod, txtTicketDesc, ticketType, "99200", priority, txtTdate, username, "", "", usrid);
                string TicketId = ds.Tables[0].Rows[0]["lastid"].ToString();
                if (TicketId == null)
                {
                    return;
                }
                else
                {

                    this.txtTicketDesc.Text = "";
                    this.CreateFile(TicketId);

                }
            }
            else
            {
                string Messaged = "Ticket Create Failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
 
            }


        }
       
        
        public string ftpStr1 = "ftp://123.200.23.58/TicketFTP/";
        public string ftpuser = "administrator";
        public string ftppass = "`123ptl1qaz#`123";
        protected void smptMail(string TicketId)
        {

            MailMessage mail = new MailMessage();
            mail.To.Add("nahid@pintechltd.com");
            mail.From = new MailAddress("support@pintechltd.com");

            mail.Subject = "Ticket Opening";
            string Body = "Your ticket created";
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "webmail.edison-bd.com";
            smtp.Port = 25;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new System.Net.NetworkCredential("support@pintechltd.com", "Ptlbd@2021"); // Enter seders User name and password  
            smtp.EnableSsl = false;

            try
            {
                smtp.Send(mail);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            AsyncFileUpload1.SaveAs(Server.MapPath("~/Image1/") + filename);
        }
        protected void CreateFile(string TicketId)
        {

            if (AsyncFileUpload1.HasFile != false)
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string trmid = hst["trmid"].ToString();
                string username = hst["username"].ToString();
                string date = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                var fileFullName1 = System.IO.Path.GetFileName(AsyncFileUpload1.PostedFile.FileName);
                var extan = fileFullName1.Split('.');
                string savelocation = Server.MapPath("~") + "Image1" + "\\" + TicketId + "01." + extan[1];
                string filepath = savelocation;
                AsyncFileUpload1.PostedFile.SaveAs(savelocation);

                string phpath = Server.MapPath("~/Image1/") + TicketId + "01." + extan[1];
                string fileName1 = TicketId + "01." + extan[1];

                string filePath1 = this.ftpStr1 + fileName1;
                string filepath2 = savelocation;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(filePath1));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(this.ftpuser, this.ftppass);
                using (FileStream fileStream = new FileStream(phpath, FileMode.Open, FileAccess.Read))
                using (Stream requestStream = request.GetRequestStream())
                {
                    fileStream.CopyTo(requestStream);
                }
                // Delete Temporary File

                string ftplocaction = Server.MapPath("~/Image1");
                string[] filePaths = Directory.GetFiles(ftplocaction);
                foreach (string filePath in filePaths)
                { File.Delete(filePath); }

                bool TicketPath = _linkVendorDb.InsertTicketAttach(comcod, TicketId, filePath1, date, username, trmid, "");

                string Messaged = "Ticket Created Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messaged + "');", true);

                //((Panel)this.Master.FindControl("AlertArea")).Visible = true;
                //((Label)this.Master.FindControl("lblANMgsBox")).Visible = true;
                //((Label)this.Master.FindControl("lblANMgsBox")).Text = "Ticket Created Successfully";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                if (this.ChkSendMail.Checked == true)
                {
                    this.smptMail(TicketId);
                }
            }
            else
            {
                string Messaged = "Ticket Created Successfully Without File!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messaged + "');", true);


                //((Panel)this.Master.FindControl("AlertArea")).Visible = true;
                //((Label)this.Master.FindControl("lblANMgsBox")).Visible = true;
                //((Label)this.Master.FindControl("lblANMgsBox")).Text = "Ticket Created Successfully Without File!!";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                if (this.ChkSendMail.Checked == true)
                {
                    this.smptMail(TicketId);
                }

            }

        }
    }
}