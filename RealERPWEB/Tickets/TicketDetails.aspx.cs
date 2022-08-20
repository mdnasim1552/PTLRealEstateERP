using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.Tickets
{
    public partial class TicketDetails : System.Web.UI.Page
    {
        ProcessAccess _linkVendorDb = new ProcessAccess();

        public string ftpStr1 = "ftp://123.200.23.58/TicketFTP/";
        public string ftpuser = "administrator";
        public string ftppass = "`123ptl1qaz#`123";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                this.GetTicketDetails();
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetTicketDetails()
        {
            string comcod = GetCompCode();
            string ticketID = this.Request.QueryString["TicketId"].ToString();
            DataSet ds1 = _linkVendorDb.GetTicketDataByID(comcod, ticketID);
            if (ds1 == null)
                return;
            assignEngName.InnerText = ds1.Tables[0].Rows[0]["assignuname"].ToString();

            creatDate.InnerText = Convert.ToDateTime(ds1.Tables[0].Rows[0]["createtask"].ToString()).ToString("dd-MMM-yyyy");
            creatBy.InnerText = ds1.Tables[0].Rows[0]["createusername"].ToString();
            ticketDesc.InnerText = ds1.Tables[0].Rows[0]["taskdesc"].ToString();
            this.tickteID.InnerText= ds1.Tables[0].Rows[0]["id"].ToString();

            FtpWebRequest request1 = (FtpWebRequest)WebRequest.Create(ftpStr1);
            request1.Method = WebRequestMethods.Ftp.ListDirectory;
            request1.Credentials = new NetworkCredential(this.ftpuser, this.ftppass);
            FtpWebResponse response1 = (FtpWebResponse)request1.GetResponse();
            Stream responseStream1 = response1.GetResponseStream();
            StreamReader reader1 = new StreamReader(responseStream1);
            string names1 = reader1.ReadToEnd();
            reader1.Close();
            response1.Close();
            List<string> DirList1 = names1.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var item in DirList1)
            {
                var items = item.Split('.');
                if (items[0] == ticketID + "01")
                {
                    string serverpath = Server.MapPath("~/Image1") + "\\" + item;
                    string ftpStr3 = this.ftpStr1 + item;


                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpStr3);
                    request.Method = WebRequestMethods.Ftp.DownloadFile;


                    //Enter FTP Server credentials.
                    request.Credentials = new NetworkCredential(this.ftpuser, this.ftppass);
                    request.UsePassive = true;
                    request.UseBinary = true;
                    request.EnableSsl = false;


                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (Stream fileStream = new FileStream(serverpath, FileMode.Append))
                        {
                            responseStream.CopyTo(fileStream);
                        }
                    }

                    string extension = Path.GetExtension(item);
                    switch (extension)
                    {

                        case ".pdf":

                            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"100%\" height=\"450px\"></object>";
                            //embedPdf.Visible = true;
                            embedPdf.ResolveUrl("~/Image1/" + item);
                            embedPdf.Text = string.Format(embed, ResolveUrl("~/Image1/" + item));

                            BindImg.Visible = false;
                            //BindImg.ImageUrl = "~/Images/pdf.png";
                            break;
                        case ".xls":
                        case ".xlsx":
                            BindImg.Visible = true;
                            BindImg.ImageUrl = "~/Images/excel.svg";
                            break;
                        case ".doc":
                        case ".docx":
                            BindImg.Visible = true;
                            BindImg.ImageUrl = "~/Images/word.png";
                            BindImg.Height = 125;


                            break;
                        default:
                            this.BindImg.ImageUrl = "~/Image1/" + item;
                            break;

                    }



                }
            }

        }
    }
}