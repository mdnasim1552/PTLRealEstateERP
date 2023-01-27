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
using AjaxControlToolkit;
using System.IO;
using System.Net;
using System.ComponentModel;
namespace RealERPWEB.F_33_Doc
{
    public partial class FileManagement : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static int i, j;
        public static string Url = "";
        //public string ftpuser = "ftpuser001";
        //public string ftppass = "123321";
        //public string ftpStr1 = "ftp://123.200.23.60/LMS4101/" + "LMSSHARE/";
        public string ftpuser = "";
        public string ftppass = "";
        public string ftpStr1 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3101": //PTL
                case "2306": //Ptl Land
                    ftpuser = "administrator";
                    ftppass = "`123asit1qaz@`123";
                    ftpStr1 = "ftp://123.200.23.58/PTLFTP/";
                    break;
                case "3355": //Green wood
                    ftpuser = "administrator";
                    ftppass = "Edifice@my";
                    ftpStr1 = "ftp://119.40.85.147/MAINLOC/";
                    break;
                case "3343": //Dominion          
                    ftpuser = "administrator";
                    ftppass = "ecl12345";
                    ftpStr1 = "ftp://103.165.162.11/FTPLOCATION/";
                    break;
            }
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = "File Management";
                if (this.ddlPrjName.Items.Count == 0)
                {
                    this.GetProjectName();
                }
                this.GetDirectoryList(this.ftpStr1 + this.lblFolderPathtag.Text.Trim());
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Sucessfully downloaded the file on location: ";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);



            }

            if (Request["__EVENTARGUMENT"] != null && Request["__EVENTARGUMENT"] == "move")
            {
                lstFolder_SelectedIndexChanged(null, null);
            }
            lstFolder.Attributes.Add("ondblclick", ClientScript.GetPostBackEventReference(lstFolder, "move"));
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = false;
        }
        private void GetProjectName()
        {
            Session.Remove("tblpro");
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "GETEXPRJNAME", "%", "", "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "actdesc";
            this.ddlPrjName.DataValueField = "actcode";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();
            Session["tblpro"] = ds1.Tables[0];

        }
        private void GetDirectoryList(string ftpStr2)
        {
            try
            {

                List<RealEntity.C_33_Doc.EClassDoc> lst = new List<RealEntity.C_33_Doc.EClassDoc>();
                Session.Remove("tbllst");
                this.lstFolder.Items.Clear();

                if (this.lblFolderPathtag.Text.Trim().Length > 0)
                {
                    RealEntity.C_33_Doc.EClassDoc item2 = new RealEntity.C_33_Doc.EClassDoc { Content = "<Parent Directory>", Tag = "", Uid = "DIR" };
                    //ListItem item2 = new ListItem() { Content = "<Parent Directory>", Tag = "", Uid = "DIR", FontWeight = FontWeights.Bold };
                    lst.Add(item2);

                    //  this.lstFolder.Items.Add(item2);
                }
                FtpWebRequest request1 = (FtpWebRequest)WebRequest.Create(ftpStr2);
                request1.Method = WebRequestMethods.Ftp.ListDirectory;
                request1.Credentials = new NetworkCredential(this.ftpuser, this.ftppass);
                FtpWebResponse response1 = (FtpWebResponse)request1.GetResponse();
                Stream responseStream1 = response1.GetResponseStream();
                StreamReader reader1 = new StreamReader(responseStream1);
                string names1 = reader1.ReadToEnd();
                reader1.Close();
                response1.Close();
                List<string> DirList1 = names1.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                FtpWebRequest request2 = (FtpWebRequest)WebRequest.Create(ftpStr2);
                request2.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request2.Credentials = new NetworkCredential(ftpuser, ftppass);
                FtpWebResponse response2 = (FtpWebResponse)request2.GetResponse();
                Stream responseStream2 = response2.GetResponseStream();
                StreamReader reader2 = new StreamReader(responseStream2);
                string names2 = reader2.ReadToEnd();
                reader2.Close();
                response2.Close();
                List<string> DirList2 = names2.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                int findex1 = 0;
                foreach (string item in DirList2)
                {
                    if (item.Contains("<DIR>"))
                    {

                        RealEntity.C_33_Doc.EClassDoc item2 = new RealEntity.C_33_Doc.EClassDoc { Content = "Folder: " + DirList1[findex1], Tag = DirList1[findex1], Uid = "DIR" };
                        // ListBox item2 = new ListItem() { Content = "<Dir> " + DirList1[findex1], Tag = DirList1[findex1], Uid = "DIR", FontWeight = FontWeights.Bold };
                        //this.lstFolder.Items.Add(item2);
                        lst.Add(item2);
                        //this.lstFolder.Items.Add(item2);
                    }
                    findex1++;
                }

                RealEntity.C_33_Doc.EClassDoc item2a = new RealEntity.C_33_Doc.EClassDoc { Content = "", Tag = "", Uid = "DIR" };
                // ListItem item2a = new ListItem { Content = "", Tag = "", Uid = "DIR" };
                // ListItemItem item2a = new ListItemItem() { Content = "", Tag = "", Uid = "DIR", Height = 5, IsEnabled = false };
                lst.Add(item2a);

                findex1 = 0;
                foreach (string item in DirList2)
                {
                    if (!item.Contains("<DIR>"))
                    {

                        RealEntity.C_33_Doc.EClassDoc item2 = new RealEntity.C_33_Doc.EClassDoc { Content = "File: " + DirList1[findex1], Tag = DirList1[findex1], Uid = "FILE" };
                        //ListItem item2 = new ListItem { Content = DirList1[findex1], Tag = DirList1[findex1], Uid = "FILE" };
                        // ListBoxItem item2 = new ListBoxItem() { Content = DirList1[findex1], Tag = DirList1[findex1], Uid = "FILE" };
                        lst.Add(item2);
                        //   this.lstFile.Items.Add(item2);
                    }
                    findex1++;
                }
                this.lstFolder.DataTextField = "Content";
                this.lstFolder.DataValueField = "Tag";
                this.lstFolder.DataSource = lst;
                this.lstFolder.DataBind();
                this.ListView1.DataSource = lst;
                this.ListView1.DataBind();

                Session["tbllst"] = lst;
            }



            catch (Exception ex)
            {
                return;
                //throw;
            }
        }


        public List<RealEntity.C_33_Doc.EClassDoc> GetProducts(string ftpStr2 = null)
        {
            List<RealEntity.C_33_Doc.EClassDoc> lst = new List<RealEntity.C_33_Doc.EClassDoc>();
            if (this.lblFolderPathtag.Text.Trim().Length > 0)
            {
                RealEntity.C_33_Doc.EClassDoc item2 = new RealEntity.C_33_Doc.EClassDoc { Content = "<Parent Directory>", Tag = "", Uid = "DIR" };
                //ListItem item2 = new ListItem() { Content = "<Parent Directory>", Tag = "", Uid = "DIR", FontWeight = FontWeights.Bold };
                lst.Add(item2);

                //  this.lstFolder.Items.Add(item2);
            }
            FtpWebRequest request1 = (FtpWebRequest)WebRequest.Create("ftp://123.200.23.60/LMS4101/LMSSHARE/");
            request1.Method = WebRequestMethods.Ftp.ListDirectory;
            request1.Credentials = new NetworkCredential(this.ftpuser, this.ftppass);
            FtpWebResponse response1 = (FtpWebResponse)request1.GetResponse();
            Stream responseStream1 = response1.GetResponseStream();
            StreamReader reader1 = new StreamReader(responseStream1);
            string names1 = reader1.ReadToEnd();
            reader1.Close();
            response1.Close();
            List<string> DirList1 = names1.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            FtpWebRequest request2 = (FtpWebRequest)WebRequest.Create("ftp://123.200.23.60/LMS4101/LMSSHARE/");
            request2.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request2.Credentials = new NetworkCredential(ftpuser, ftppass);
            FtpWebResponse response2 = (FtpWebResponse)request2.GetResponse();
            Stream responseStream2 = response2.GetResponseStream();
            StreamReader reader2 = new StreamReader(responseStream2);
            string names2 = reader2.ReadToEnd();
            reader2.Close();
            response2.Close();
            List<string> DirList2 = names2.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            int findex1 = 0;
            foreach (string item in DirList2)
            {
                if (item.Contains("<DIR>"))
                {

                    RealEntity.C_33_Doc.EClassDoc item2 = new RealEntity.C_33_Doc.EClassDoc { Content = "Folder: " + DirList1[findex1], Tag = DirList1[findex1], Uid = "DIR" };
                    // ListBox item2 = new ListItem() { Content = "<Dir> " + DirList1[findex1], Tag = DirList1[findex1], Uid = "DIR", FontWeight = FontWeights.Bold };
                    //this.lstFolder.Items.Add(item2);
                    lst.Add(item2);
                    //this.lstFolder.Items.Add(item2);
                }
                findex1++;
            }

            RealEntity.C_33_Doc.EClassDoc item2a = new RealEntity.C_33_Doc.EClassDoc { Content = "", Tag = "", Uid = "DIR" };
            // ListItem item2a = new ListItem { Content = "", Tag = "", Uid = "DIR" };
            // ListItemItem item2a = new ListItemItem() { Content = "", Tag = "", Uid = "DIR", Height = 5, IsEnabled = false };
            lst.Add(item2a);

            findex1 = 0;
            foreach (string item in DirList2)
            {
                if (!item.Contains("<DIR>"))
                {

                    RealEntity.C_33_Doc.EClassDoc item2 = new RealEntity.C_33_Doc.EClassDoc { Content = "File: " + DirList1[findex1], Tag = DirList1[findex1], Uid = "FILE" };
                    //ListItem item2 = new ListItem { Content = DirList1[findex1], Tag = DirList1[findex1], Uid = "FILE" };
                    // ListBoxItem item2 = new ListBoxItem() { Content = DirList1[findex1], Tag = DirList1[findex1], Uid = "FILE" };
                    lst.Add(item2);
                    //   this.lstFile.Items.Add(item2);
                }
                findex1++;
            }
            return lst.ToList();
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lbtnCreateFolder_Click(object sender, EventArgs e)
        {
            try
            {
                if (isProject.Checked)
                {
                    string subDir = this.ddlPrjName.SelectedItem.Text.Trim().Replace(' ', '_');
                    string currentDir = this.ftpStr1 + (this.lblFolderPathtag.Text.Replace(">", "")) + "/" + subDir;
                    FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(currentDir);
                    reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(this.ftpuser, this.ftppass);
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    response.Close();
                    this.txtFileName.Text = "";
                    //  this.txtFileName.Tag = null;
                    //this.txtFileName.ToolTip = null;
                    this.GetDirectoryList(this.ftpStr1 + this.lblFolderPathtag.Text.Trim());
                }
                else
                {
                    string subDir = this.txtFileName.Text.Trim().Replace(' ', '_');
                    string currentDir = this.ftpStr1 + (this.lblFolderPathtag.Text.Replace(">", "")) + "/" + subDir;
                    FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(currentDir);
                    reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(this.ftpuser, this.ftppass);
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    response.Close();
                    this.txtFileName.Text = "";
                    //  this.txtFileName.Tag = null;
                    //this.txtFileName.ToolTip = null;
                    this.GetDirectoryList(this.ftpStr1 + this.lblFolderPathtag.Text.Trim());
                }

            }
            catch (Exception exp)
            {
                // System.Windows.MessageBox.Show("FileMan-5: " + exp.Message.ToString(), WpfProcessClass.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        protected void BtnLoadFileName_Click(object sender, EventArgs e)
        {



        }


        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {


            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            AsyncFileUpload1.SaveAs(Server.MapPath("~/ftp/") + filename);

        }
        protected void lbtnCreateFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload.HasFile != null)
                {
                    var fileFullName1 = System.IO.Path.GetFileName(FileUpload.PostedFile.FileName);
                    string savelocation = Server.MapPath("~") + "\\ftp" + "\\" + fileFullName1;
                    string filepath = savelocation;
                    FileUpload.PostedFile.SaveAs(savelocation);


                    string phpath = Server.MapPath("~/ftp/") + System.IO.Path.GetFileName(FileUpload.PostedFile.FileName);
                    //   string fileFullName1 = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
                    // string fileName1 = System.IO.Path.GetFileName(fileFullName1);
                    string fileName1 = System.IO.Path.GetFileName(FileUpload.PostedFile.FileName);


                    //if (System.Windows.MessageBox.Show("Confirm update document/file", WpfProcessClass.AppTitle, MessageBoxButton.YesNoCancel,
                    //    MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
                    //{
                    //    return;
                    //}
                    string filePath1 = this.ftpStr1 + this.lblFolderPathtag.Text.Trim() + fileName1;
                    string filepath2 = fileFullName1;
                    //WebClient client = new WebClient
                    //{
                    //    Credentials = new System.Net.NetworkCredential(this.ftpuser, this.ftppass)
                    //};

                    //  client.UploadFileAsync(new Uri(filePath1), "STOR", phpath);




                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(filePath1));
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential(this.ftpuser, this.ftppass);
                    using (FileStream fileStream = new FileStream(phpath, FileMode.Open, FileAccess.Read))
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        fileStream.CopyTo(requestStream);
                    }




                    this.GetDirectoryList(this.ftpStr1 + this.lblFolderPathtag.Text.Trim());

                }
                // Delete Temporary File

                string ftplocaction = Server.MapPath("~/ftp");
                string[] filePaths = Directory.GetFiles(ftplocaction);
                foreach (string filePath in filePaths)
                    File.Delete(filePath);


            }
            catch (Exception exp)
            {
                ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = exp.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                //System.Windows.MessageBox.Show("Member.Gen-2: " + exp.Message.ToString(), WpfProcessClass.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }





        protected void lbtnDownload_Click(object sender, EventArgs e)
        {

            try
            {

                List<RealEntity.C_33_Doc.EClassDoc> lst = (List<RealEntity.C_33_Doc.EClassDoc>)Session["tbllst"];

                if (this.lstFolder.SelectedItem == null)
                    return;


                string Tag = this.lstFolder.SelectedValue.ToString();
                string Uid = (lst.FindAll(l => l.Tag == Tag))[0].Uid.ToString().Trim();

                if (Uid == "DIR" || this.txtFileName.Text.Trim() == null)
                    return;


                string fileName1 = Tag;
                string dfilename = fileName1.Replace(" ", "");
                string serverpath = Server.MapPath("~/ftpdown") + "\\" + dfilename;
                string localpath = @"C:\Users\Administrator\Downloads\" + dfilename;
                string ftpStr3 = this.ftpStr1 + this.lblFolderPathtag.Text.Trim() + fileName1;



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

                FileInfo fileInfo = new FileInfo(serverpath);

                if (fileInfo.Exists)
                {
                    Response.Clear();
                    //Response.BufferOutput = false;              
                    Response.AddHeader("Content-Disposition", "inline;attachment; filename=" + fileInfo.Name);
                    Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.Flush();
                    Response.WriteFile(fileInfo.FullName);
                    Response.BufferOutput = true;
                    Response.End();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();


                }
                ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Sucessfully downloaded the file on location: " + fileInfo.Name;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }
            catch (Exception exp)
            {
                ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = exp.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }


        protected void lbtnUpload_Click(object sender, EventArgs e)
        {

        }
        //protected void lbtnDeleteFolder_Click(object sender, EventArgs e)
        //{

        //    try
        //    {

        //        List<RealEntity.C_33_Doc.EClassDoc> lst = (List<RealEntity.C_33_Doc.EClassDoc>)Session["tbllst"];
        //        //if (this.lstFolder.SelectedItem == null)
        //        //    return;

        //        string Tag = this.lstFolder.SelectedValue.ToString();
        //        string Uid = (lst.FindAll(l => l.Tag == Tag))[0].Uid.ToString().Trim();

        //        if (Uid != "DIR")
        //            return;

        //        string subDir1 = Tag;
        //        if (subDir1.Length == 0)
        //            return;

        //        //if (System.Windows.MessageBox.Show("Confirm remove directory/folder\n" + subDir1, WpfProcessClass.AppTitle, MessageBoxButton.YesNoCancel,
        //        //MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
        //        //{
        //        //    return;
        //        //}
        //        string subDir2 = this.ftpStr1 + this.lblFolderPathtag.Text.Trim() + subDir1;
        //        FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(subDir2);
        //        reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;
        //        reqFTP.UseBinary = true;
        //        reqFTP.Credentials = new NetworkCredential(this.ftpuser, this.ftppass);
        //        FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
        //        response.Close();
        //        this.GetDirectoryList(this.ftpStr1 + this.lblFolderPathtag.Text.Trim());
        //    }
        //    catch (Exception exp)
        //    {

        //    }

        //}
        protected void lbtnDeleteFile_Click(object sender, EventArgs e)
        {

            try
            {

                List<RealEntity.C_33_Doc.EClassDoc> lst = (List<RealEntity.C_33_Doc.EClassDoc>)Session["tbllst"];
                if (this.lstFolder.SelectedItem == null)
                    return;
                string Tag = this.lstFolder.SelectedValue.ToString();
                string Uid = (lst.FindAll(l => l.Tag == Tag))[0].Uid.ToString().Trim();
                string lblFolderPath2 = this.lblFolderPathtag.Text.ToString();
                if (Uid == "DIR")
                {
                    if (Tag.Length == 0 && lblFolderPath2.Length > 0)
                    {
                        lblFolderPath2 = lblFolderPath2.Substring(0, lblFolderPath2.Length - 1);
                        if (lblFolderPath2.Contains("\\"))
                            lblFolderPath2 = lblFolderPath2.Substring(0, lblFolderPath2.LastIndexOf("\\") + 1);
                        else
                            lblFolderPath2 = "";
                    }

                    lblFolderPath2 = lblFolderPath2 + Tag + (Tag.Length > 0 ? @"\" : "");
                    string location = this.ftpStr1 + lblFolderPath2.Replace("\\", "/");


                    DeleteDirectory(location);
                    GetDirectoryList(this.ftpStr1 + this.lblFolderPathtag.Text.ToString());
                    return;
                }

                string fileNam1 = Tag;

                //if (System.Windows.MessageBox.Show("Confirm delete document/file\n" + fileNam1, WpfProcessClass.AppTitle, MessageBoxButton.YesNoCancel,
                //MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
                //{
                //    return;
                //}
                string fileNam2 = this.ftpStr1 + this.lblFolderPathtag.Text.Trim() + fileNam1;
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(fileNam2);
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(this.ftpuser, this.ftppass);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
                this.embedPdf.Text = String.Empty;
                this.BindImg.ImageUrl = String.Empty;
                this.HypLInk.Visible = false;
                this.tbldetails.Visible = false;
                this.GetDirectoryList(this.ftpStr1 + this.lblFolderPathtag.Text.Trim());
            }
            catch (Exception exp)
            {
                ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = exp.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                // System.Windows.MessageBox.Show("FileMan-5: " + exp.Message.ToString(), WpfProcessClass.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        public void DeleteDirectory(string url)
        {
            FtpWebRequest listRequest = (FtpWebRequest)WebRequest.Create(url.Remove(url.Length - 1, 1));
            listRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            listRequest.Credentials = new NetworkCredential(this.ftpuser, this.ftppass);

            List<string> lines = new List<string>();

            using (FtpWebResponse listResponse = (FtpWebResponse)listRequest.GetResponse())
            using (Stream listStream = listResponse.GetResponseStream())
            using (StreamReader listReader = new StreamReader(listStream))
            {
                while (!listReader.EndOfStream)
                {
                    lines.Add(listReader.ReadLine());
                }
            }

            foreach (string line in lines)
            {
                string[] tokens =
                    line.Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                string dir = tokens[2];
                string name = tokens[3];
                string permissions = tokens[0];

                if (dir == "<DIR>")
                {
                    var tempname = url + name;
                    FtpWebRequest frequest = (FtpWebRequest)WebRequest.Create(tempname);
                    frequest.Method = WebRequestMethods.Ftp.RemoveDirectory;
                    frequest.Credentials = new NetworkCredential(this.ftpuser, this.ftppass);
                    frequest.GetResponse().Close();
                    continue;
                }

                string fileUrl = url + name;

                if (permissions[0] == 'd')
                {
                    DeleteDirectory(fileUrl + "/");
                }
                else
                {
                    FtpWebRequest deleteRequest = (FtpWebRequest)WebRequest.Create(fileUrl);
                    deleteRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                    deleteRequest.Credentials = new NetworkCredential(this.ftpuser, this.ftppass);

                    deleteRequest.GetResponse();
                }
            }

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url.Remove(url.Length - 1, 1));
            request.Method = WebRequestMethods.Ftp.RemoveDirectory;
            request.Credentials = new NetworkCredential(this.ftpuser, this.ftppass);
            request.GetResponse().Close();
        }
        protected void lstFolder_SelectedIndexChanged(object sender, EventArgs e)
        {


            List<RealEntity.C_33_Doc.EClassDoc> lst = (List<RealEntity.C_33_Doc.EClassDoc>)Session["tbllst"];

            if (this.lstFolder.SelectedItem == null)
                return;

            string lblFolderPath2 = this.lblFolderPathtag.Text.ToString();

            string Tag = this.lstFolder.SelectedValue.ToString();
            string Uid = (lst.FindAll(l => l.Tag == Tag))[0].Uid.ToString().Trim();

            if (Uid == "DIR")
            {


                this.PanelView.Visible = false;
                BindImg.ImageUrl = null;
                if (Tag.Length == 0 && lblFolderPath2.Length > 0)
                {
                    lblFolderPath2 = lblFolderPath2.Substring(0, lblFolderPath2.Length - 1);
                    if (lblFolderPath2.Contains("\\"))
                        lblFolderPath2 = lblFolderPath2.Substring(0, lblFolderPath2.LastIndexOf("\\") + 1);
                    else
                        lblFolderPath2 = "";
                }

                lblFolderPath2 = lblFolderPath2 + Tag + (Tag.Length > 0 ? @"\" : "");

                this.lblFolderPathtag.Text = lblFolderPath2;
                this.lblFolderPath.Text = "Root" + (lblFolderPath2.Length > 0 ? (">" + lblFolderPath2.Substring(0, lblFolderPath2.Length - 1).Replace("\\", ">")) : "");

                this.GetDirectoryList(this.ftpStr1 + lblFolderPath2);
                return;

            }
            if (this.txtFileName.Text.Trim() == null)
                return;

            if (Uid != "DIR")
            {
                this.PanelView.Visible = true;
                string folder1 = this.txtFileName.Text.ToString().Trim();
                //string filenam1 = Tag;

                string fileName1 = Tag;
                string dfilename = fileName1.Replace(" ", "");
                string serverpath = Server.MapPath("~/ftpdown") + "\\" + dfilename;
                string ftpStr3 = this.ftpStr1 + this.lblFolderPathtag.Text.Trim() + fileName1;


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




                embedPdf.Visible = false;
                string extension = Path.GetExtension(fileName1);
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":
                        var url1 = "data:image;base64," + Convert.ToBase64String(this.ImageViewer(ftpStr3));
                        //  this.ImageViewer(ftpStr3);
                        BindImg.Visible = true;
                        BindImg.ImageUrl = "~/ftpdown/" + dfilename.Trim();
                        break;
                    case ".PDF":
                    case ".pdf":
                        string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"400px\" height=\"400px\"></object>";
                        embedPdf.Visible = true;
                        embedPdf.Text = string.Format(embed, ResolveUrl("~/ftpdown/" + dfilename.Trim()));

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
                        break;
                    default:
                        BindImg.Visible = true;
                        BindImg.ImageUrl = "~/Images/word.png";
                        break;

                }
                this.tbldetails.Visible = true;
                this.HypLInk.Visible = true;
                this.TblExtension.Text = Path.GetExtension(Server.MapPath(@"~/ftpdown/" + dfilename.Trim()));
                this.TblFileName.Text = Path.GetFileName(Server.MapPath(@"~/ftpdown/" + dfilename.Trim()));
                FileInfo fi = new FileInfo(Server.MapPath(@"~/ftpdown/" + dfilename.Trim()));
                this.TblFilesize.Text = (fi.Length / 1024).ToString() + " KB";
                this.lastmodified.Text = fi.LastWriteTimeUtc.ToString();

                this.HypLInk.NavigateUrl = "~/ftpdown/" + dfilename.Trim();
            }
            //  FileInfo fileInfo = new FileInfo(ftpStr3);
            //if (fileInfo.Exists)
            //{

            //}
            // this.txtFileName.Text = folder1 + @"\" + filenam1;


        }

        public Byte[] ImageViewer(string ftpStr3)
        {

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpStr3);
            request.Method = WebRequestMethods.Ftp.DownloadFile;


            //Enter FTP Server credentials.
            request.Credentials = new NetworkCredential(this.ftpuser, this.ftppass);
            request.UsePassive = true;
            request.UseBinary = true;
            request.EnableSsl = false;

            String lsResponse = string.Empty;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                using (BinaryReader reader = new BinaryReader(response.GetResponseStream()))
                {
                    Byte[] lnByte = reader.ReadBytes(1 * 1024 * 1024 * 10);
                    //using (FileStream lxFS = new FileStream("34891.jpg", FileMode.Create))
                    //{
                    //    lxFS.Write(lnByte, 0, lnByte.Length);
                    //}
                    return lnByte;
                }
            }


        }
        protected void LbtnUpload_Click(object sender, EventArgs e)
        {


        }






        protected void LbtnRename_Click(object sender, EventArgs e)
        {
            List<RealEntity.C_33_Doc.EClassDoc> lst = (List<RealEntity.C_33_Doc.EClassDoc>)Session["tbllst"];
            string Tag = this.lstFolder.SelectedValue.ToString();
            string Uid = (lst.FindAll(l => l.Tag == Tag))[0].Uid.ToString().Trim();
            string lblFolderPath2 = this.lblFolderPathtag.Text.ToString();
            if (Uid != "DIR" || Tag == "")
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select A Folder Please ";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }
            else
            {
                this.TxtRenameFolder.Text = Tag;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ShowModal();", true);
            }
        }

        protected void lbtnRenameFolder_Click(object sender, EventArgs e)
        {
            List<RealEntity.C_33_Doc.EClassDoc> lst = (List<RealEntity.C_33_Doc.EClassDoc>)Session["tbllst"];
            string Tag = this.lstFolder.SelectedValue.ToString();
            string Uid = (lst.FindAll(l => l.Tag == Tag))[0].Uid.ToString().Trim();
            string lblFolderPath2 = this.lblFolderPathtag.Text.ToString();
            string rename = this.TxtRenameFolder.Text.ToString();
            if (Uid != "DIR")
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Sucessfully downloaded the file on location: ";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }
            if (Tag.Length == 0 && lblFolderPath2.Length > 0)
            {
                lblFolderPath2 = lblFolderPath2.Substring(0, lblFolderPath2.Length - 1);
                if (lblFolderPath2.Contains("\\"))
                    lblFolderPath2 = lblFolderPath2.Substring(0, lblFolderPath2.LastIndexOf("\\") + 1);
                else
                    lblFolderPath2 = "";
            }
            var lblFolderPath2s = lblFolderPath2.Substring(0, lblFolderPath2.LastIndexOf("\\") + 1);

            lblFolderPath2 = lblFolderPath2 + Tag + (Tag.Length > 0 ? @"\" : "");
            string location = this.ftpStr1 + lblFolderPath2.Replace("\\", "/");

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(location.Remove(location.Length - 1, 1));
            request.Credentials = new NetworkCredential(this.ftpuser, this.ftppass);
            request.UsePassive = true;
            request.UseBinary = true;
            request.EnableSsl = false;
            request.Method = WebRequestMethods.Ftp.Rename;
            request.RenameTo = rename;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            GetDirectoryList(this.ftpStr1 + this.lblFolderPathtag.Text.ToString());
        }

        protected void LbtnBack_Click(object sender, EventArgs e)
        {
            List<RealEntity.C_33_Doc.EClassDoc> lst = (List<RealEntity.C_33_Doc.EClassDoc>)Session["tbllst"];

            this.embedPdf.Text = String.Empty;
            this.BindImg.ImageUrl = String.Empty;
            this.HypLInk.Visible = false;
            this.tbldetails.Visible = false;


            string lblFolderPath2 = this.lblFolderPathtag.Text.ToString();

            string Tag = "";
            string Uid = (lst.FindAll(l => l.Tag == Tag))[0].Uid.ToString().Trim();

            if (Uid == "DIR")
            {
                if (Tag.Length == 0 && lblFolderPath2.Length > 0)
                {
                    lblFolderPath2 = lblFolderPath2.Substring(0, lblFolderPath2.Length - 1);
                    if (lblFolderPath2.Contains("\\"))
                        lblFolderPath2 = lblFolderPath2.Substring(0, lblFolderPath2.LastIndexOf("\\") + 1);
                    else
                        lblFolderPath2 = "";
                }

                lblFolderPath2 = lblFolderPath2 + Tag + (Tag.Length > 0 ? @"\" : "");

                this.lblFolderPathtag.Text = lblFolderPath2;
                this.lblFolderPath.Text = "Root" + (lblFolderPath2.Length > 0 ? (">" + lblFolderPath2.Substring(0, lblFolderPath2.Length - 1).Replace("\\", ">")) : "");

                this.GetDirectoryList(this.ftpStr1 + lblFolderPath2);
            }
        }

        protected void lbtnForward_Click(object sender, EventArgs e)
        {
            lstFolder_SelectedIndexChanged(null, null);
        }

        protected void LbtnEmail_Click(object sender, EventArgs e)
        {

        }

        protected void LbtnHome_Click(object sender, EventArgs e)
        {
            this.lblFolderPathtag.Text = "";
            this.lblFolderPath.Text = "";
            this.GetDirectoryList(this.ftpStr1);
        }
    }
}

