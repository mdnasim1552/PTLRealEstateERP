


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
using RealERPLIB;



public class FileUploadHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{


    public void ProcessRequest(HttpContext context)
    {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        string refNodata = context.Request["refNodata"];
        Hashtable hst = (Hashtable)System.Web.HttpContext.Current.Session["tblLogin"];

        string comcod = hst["comcod"].ToString();
        string userid = hst["usrid"].ToString();

        var messge = "";


        if (refNodata != null)
        {
            if (context.Request.Files.Count > 0)
            {
                try
                {
                    DataTable mnuTbl1 = new DataTable();
                    mnuTbl1.Columns.Add(new DataColumn("id")
                    {
                        AutoIncrement = true,
                        AllowDBNull = false,
                        AutoIncrementSeed = 1,
                        AutoIncrementStep = 1,
                        DataType = typeof(System.Int32),
                        Unique = true
                    });
                    mnuTbl1.Columns.Add("refno", Type.GetType("System.String"));
                    mnuTbl1.Columns.Add("itemsurl", Type.GetType("System.String"));

                    HttpFileCollection files = context.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        Random rnd = new Random();
                        int rndnumber = rnd.Next();
                        HttpPostedFile file = files[i];

                        string extension = System.IO.Path.GetExtension(file.FileName);

                        var filesname = refNodata + '_' + Convert.ToString(rndnumber) + extension;
                        string fname = context.Server.MapPath("~/Upload/Purchase/" + filesname);
                        string filesname1 = "/Upload/Purchase/" + filesname;
                        file.SaveAs(fname);

                        DataRow[] dr2 = mnuTbl1.Select("itemsurl = '" + filesname + "'");

                        if (dr2.Length == 0)
                        {
                            DataRow dr1 = mnuTbl1.NewRow();
                            dr1["refno"] = refNodata;
                            dr1["itemsurl"] = filesname;
                            mnuTbl1.Rows.Add(dr1);
                        }

                    }


                    ///Doc Upload

                    DataSet ds1 = new DataSet("ds1");
                    ds1.Merge(mnuTbl1);
                    ds1.Tables[0].TableName = "tbl1";



                    //// string posteddat = System.DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss");
                    ProcessAccess _DataEntry = new ProcessAccess();
                    bool result = _DataEntry.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "XMLINSERATTACHEDDOCS", ds1, null, null, refNodata);
                    if (!result == false)
                    {

                        messge = "File(s) Uploaded Successfully!";

                    }
                }
                catch (Exception ex)
                {
                    messge = "Error:" + ex.Message;

                }

            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(messge);

        }


        else
        {
            //HttpPostedFile uploads = context.Request.Files["upload"];
            ////string CKEditorFuncNum = context.Request["CKEditorFuncNum"];
            //string file = System.IO.Path.GetFileName(uploads.FileName);
            //// uploads.SaveAs(context.Server.MapPath(".") + "Upload\\Products\\" + file);
            ////  string url = "/Upload/Products/" + file;


            //Random rnd = new Random();
            //int rndnumber = rnd.Next();
            //var filesname = chatno + Convert.ToString(rndnumber) + uploads.FileName;
            //string fname = context.Server.MapPath("~/Upload/Products/" + filesname);
            //string url = "../Upload/Products/" + filesname;
            //uploads.SaveAs(fname);

            //context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");

            //context.Response.End();
        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}