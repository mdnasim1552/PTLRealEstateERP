using System;
using System.Web;
using RealERPLIB;
using System.Data;
using System.Web.SessionState;
namespace RealERPWEB.F_33_Doc
{
    public class FileUploadHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            string chatno = context.Request["chatno"];

            if (context.Request.Files.Count > 0)
            {
                try
                {

                    HttpFileCollection files = context.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        Random rnd = new Random();
                        int rndnumber = rnd.Next();
                        HttpPostedFile file = files[i];
                        var filesname = chatno + Convert.ToString(rndnumber) + file.FileName;
                        string fname = context.Server.MapPath("~/Upload/CHAT/" + filesname);
                        string filesname1 = "/Upload/CHAT/" + filesname;
                        file.SaveAs(fname);
                        //string name = context.Session["comnam"].ToString();
                        Common ObjCommon = new Common();
                        string comcod = ObjCommon.GetCompCode();
                        string posteduser = ObjCommon.GetUserCode();
                        string terminal = ObjCommon.Terminal();
                        string session = ObjCommon.Sessionid();
                        string posteddat = System.DateTime.Today.ToString("dd-MMM-yyy hh:mm:ss");
                        ProcessAccess _DataEntry = new ProcessAccess();

                        DataSet result = _DataEntry.GetTransInfo(comcod, "SP_CHAT_MGT", "SAVE_CHAT_MSG", chatno, filesname1, posteduser, posteddat, terminal, session, "True");



                    }
                }
                catch (Exception ex)
                {
                    var messgae = ex.Message;
                }

            }

            context.Response.ContentType = "text/plain";
            context.Response.Write("File(s) Uploaded Successfully!");

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}