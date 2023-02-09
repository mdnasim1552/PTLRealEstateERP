using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Web.Configuration;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Web.Services;
using RealERPLIB;
using System.Data.OleDb;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Data;
//using System.Configuration;
//using System.Collections;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
//using CrystalDecisions.ReportSource;
//using System.IO;
//using System.Data.OleDb;
//using RealERPLIB;
//using RealERPRPT;

namespace RealERPWEB.F_81_Hrm.F_83_Att
{


    public partial class HRDailyAttenUpload : System.Web.UI.Page
    {
        //Attendance _AppLib = new Attendance();
        string Msg;

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                Session.Remove("DayAtten");
                this.txtMrrDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");


            }

            this.Visibility();
            this.CreateTable();

            //if (fileuploadExcel.HasFile)
            //{
            //    try
            //    {
            //        Session.Remove("XcelData");
            //        //  ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //        string connString = "";
            //        string StrFileName = string.Empty;
            //        if (fileuploadExcel.PostedFile != null && fileuploadExcel.PostedFile.FileName != "")
            //        {
            //            StrFileName =
            //                fileuploadExcel.PostedFile.FileName.Substring(
            //                    fileuploadExcel.PostedFile.FileName.LastIndexOf("\\") + 1);
            //            string StrFileType = fileuploadExcel.PostedFile.ContentType;
            //            int IntFileSize = fileuploadExcel.PostedFile.ContentLength;
            //            if (IntFileSize <= 0)
            //            {
            //                //  ((Label)this.Master.FindControl("lblmsg")).Text = "Uploading Fail";
            //                // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert(' file Uploading failed');", true);
            //                return;
            //            }
            //            else
            //            {
            //                string savelocation = Server.MapPath("~") + "\\ExcelFile\\";
            //                string[] filePaths = Directory.GetFiles(savelocation);
            //                foreach (string filePath in filePaths)
            //                    File.Delete(filePath);
            //                fileuploadExcel.PostedFile.SaveAs(Server.MapPath("~") + "\\ExcelFile\\" + StrFileName);
            //                //   ((Label)this.Master.FindControl("lblmsg")).Text = "Uploading Successfully";
            //            }
            //        }

            //        string strFileType = Path.GetExtension(fileuploadExcel.FileName).ToLower();
            //        string apppath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.ToString();
            //        //string path = apppath + "ExcelFile\\" + StrFileName;
            //        string path = Server.MapPath("~") + ("\\ExcelFile\\" + StrFileName);

            //        //Connection String to Excel Workbook
            //        if (strFileType.Trim() == ".xls")
            //        {
            //            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path +
            //                         ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            //        }
            //        else if (strFileType.Trim() == ".xlsx")
            //        {
            //            connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path +
            //                         ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
            //        }

            //        // string query = "SELECT [No] FROM [Sheet1$]";
            //        string query =
            //           "SELECT * FROM [Sheet1$]";
            //        OleDbConnection conn = new OleDbConnection(connString);
            //        if (conn.State == ConnectionState.Closed)
            //            conn.Open();
            //        OleDbCommand cmd = new OleDbCommand(query, conn);
            //        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            //        DataSet ds = new DataSet();
            //        da.Fill(ds);

            //        Session["XcelData"] = ds.Tables[0];
            //        // this.DataInsert();
            //        da.Dispose();
            //        conn.Close();
            //        conn.Dispose();
            //        //this.GetExelData();
            //    }
            //    catch (Exception)
            //    {

            //        throw;
            //    }
            //}



        }


        private void Visibility()
        {
            string comcod = this.GetCompCode();

            switch (comcod)
            {

               
                case "3347":// Peb steel
                    this.panelexcel.Visible = true;
                    this.Label2.Visible = false;
                    this.CmdUpload.Visible = false;
                    this.File1.Visible = false;
                    break;


                default:
                    this.panelexcel.Visible = false;
                    this.Label2.Visible = true;
                    this.CmdUpload.Visible = true;
                    this.File1.Visible = true;
                    break;


            }    
           


        }

        protected void CreateTable()
        {

            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("idcardno", Type.GetType("System.String"));
            dttemp.Columns.Add("adate", Type.GetType("System.String"));
            dttemp.Columns.Add("atime", Type.GetType("System.String"));
            dttemp.Columns.Add("machid", Type.GetType("System.String"));
            //dttemp.Columns.Add("chequeno", Type.GetType("System.String"));
            //dttemp.Columns.Add("bankname", Type.GetType("System.String"));
            //dttemp.Columns.Add("branchname", Type.GetType("System.String"));
            //dttemp.Columns.Add("paydate", Type.GetType("System.String"));
            //dttemp.Columns.Add("refid", Type.GetType("System.String"));
            //dttemp.Columns.Add("repchqno", Type.GetType("System.String"));
            //dttemp.Columns.Add("remarks", Type.GetType("System.String"));
            //dttemp.Columns.Add("collfrm", Type.GetType("System.String"));
            //dttemp.Columns.Add("collfrmd", Type.GetType("System.String"));
            //dttemp.Columns.Add("RecType", Type.GetType("System.String"));
            //dttemp.Columns.Add("RecTyped", Type.GetType("System.String"));
            //dttemp.Columns.Add("billno", Type.GetType("System.String"));
            //dttemp.Columns.Add("cactcode", Type.GetType("System.String"));
            //dttemp.Columns.Add("cactdesc", Type.GetType("System.String"));
            //dttemp.Columns.Add("recndt", Type.GetType("System.String"));
            //dttemp.Columns.Add("instype", Type.GetType("System.String"));
            //dttemp.Columns.Add("insdesc", Type.GetType("System.String"));

            Session["tblattn"] = dttemp;

        }





        protected void UploadFile(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();

            switch (comcod)
            {

               


                //case "3101":

                //    this.UploadPEB();
                //    break;

                case "3315": // Assure Group
                             //this.UploadData();
                    this.UploadDataNew();
                    break;

                case "4306": // Greenland
                    this.UploadDataGreenLand();
                    break;


                case "3101":
                case "3368":                
                    this.UploadDataFinlay();
                    break;

                case "3370":
                    this.UploadDataCPDL();
                    break;
                    
                default:
                    this.UploadDataGreenLand();
                    break;







            }

        }


        private void UploadDataNew()
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            try
            {
                string StrFileName = string.Empty;

                if (File1.PostedFile != null)
                {
                    StrFileName = File1.PostedFile.FileName.Substring(File1.PostedFile.FileName.LastIndexOf("\\") + 1);
                    string StrFileType = File1.PostedFile.ContentType;
                    int IntFileSize = File1.PostedFile.ContentLength;
                    if (IntFileSize <= 0)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Uploading of file failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    }



                    else
                    {
                        File1.PostedFile.SaveAs(Server.MapPath("..\\..\\Upload\\Attndancefiles\\" + StrFileName));
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Data Uploading Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    }
                }
                if (StrFileName == "")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Please fill a file";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }

                if (txtMrrDate.Text.Trim() == "")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = " Date can not be a blank";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }


                string filename1 = Server.MapPath("~") + ("\\Upload\\Attndancefiles\\" + StrFileName); //IIS Path

                //string filename1 = Server.MapPath("~") + ("../Upload/" + StrFileName); //IIS Path
                //string filename1 = Server.MapPath("~") + ("Upload/" + StrFileName); Local Path

                //string savelocation = Server.MapPath("~") + "\\Image1";

                System.IO.FileStream fs = new System.IO.FileStream(filename1, System.IO.FileMode.Open);
                System.IO.StreamReader r = new System.IO.StreamReader(fs);
                Label3.Text = r.ReadToEnd();
                Label4.Text = filename1;
                //UpdatePanel1.Controls.Add(Label1);
                r.Close();


                // Update  Data

                string comcod = this.GetCompCode();
                DataTable t4 = new DataTable();
                t4.Columns.Add("adate", typeof(String));
                t4.Columns.Add("atime", typeof(String));
                t4.Columns.Add("IDCARDNO", typeof(String));
                t4.Columns.Add("machid", typeof(String));


                string ROWID = string.Empty;
                string MACHID = string.Empty;
                string IDCARDNO = string.Empty;
                string LastNo = string.Empty;
                string seldate = Convert.ToDateTime(this.txtMrrDate.Text).ToString("dd-MMM-yyyy");//Problem
                DateTime ADAT;
                DateTime ATIME;



                string retFilePath = Label4.Text.Trim();

                StreamReader objReader = new StreamReader(retFilePath);
                ///////
                string[] X1 = new string[30000];
                string sLine = "";
                int i = 0;
                DataTable t1 = new DataTable();
                t1.Columns.Add("empattn", typeof(String));
                while (sLine != null)
                {
                    DataRow dr = t1.NewRow();
                    sLine = objReader.ReadLine();
                    X1[i] = sLine;
                    dr["empattn"] = X1[i];
                    t1.Rows.Add(dr);
                    i = i + 1;
                }
                objReader.Close();

                string allstr;
                string IDCARDNO1;
                string adt;
                string atm;
                string h1;
                string ampm;
                //  Condition 

                if (chktype.Checked == true)
                {

                    for (int j = 0; j < t1.Rows.Count - 1; j++)
                    {

                        allstr = t1.Rows[j]["empattn"].ToString();

                        adt = allstr.Substring(3, 8).ToString();
                        IDCARDNO1 = "0" + allstr.Substring(21, 5).ToString(); //allstr.Substring(10, 6).ToString();
                        MACHID = ASTUtility.Left(allstr, 3);//MACHID = ASTUtility.Right(allstr, 2);//allstr.Substring(26,2).ToString();


                        string ADAT1 = adt.Substring(4, 4).ToString() + "/" + adt.Substring(2, 2).ToString() + "/" + adt.Substring(0, 2).ToString();
                        ADAT = Convert.ToDateTime(ADAT1);

                        atm = allstr.Substring(11, 4).ToString();
                        if (Convert.ToInt32(atm.Substring(0, 2)) >= 12)
                        {
                            h1 = (Convert.ToInt32(atm.Substring(0, 2))).ToString();
                            h1 = h1.PadLeft(2, '0');
                            ampm = " PM";
                        }
                        else
                        {
                            h1 = (atm.Substring(0, 2)).ToString();
                            ampm = " AM"; //TEST
                        }
                        string dttime = ADAT1 + "  " + h1 + ":" + atm.Substring(2, 2).ToString() + ":" +
                                "00" + ampm.ToString();

                        ATIME = Convert.ToDateTime(dttime.ToString());

                        DateTime seldt = Convert.ToDateTime(this.txtMrrDate.Text);
                        seldate = (seldt.ToString("dd-MMM-yyyy"));
                        bool result = HRData.UpdateTransInfo(comcod, "SP_ATTN_UPDATE", "ATTN_UPDATE_TEMP", "", IDCARDNO1, Convert.ToDateTime(ADAT).ToString(),
                                Convert.ToDateTime(ATIME).ToString(), MACHID, seldate, "", "", "", "", "", "", "", "", "");
                        if (!result)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        }

                    }
                }

                else
                {

                    for (int j = 0; j < t1.Rows.Count - 1; j++)
                    {

                        allstr = t1.Rows[j]["empattn"].ToString();
                        adt = allstr.Substring(3, 6).ToString();
                        IDCARDNO1 = "0" + allstr.Substring(19, 5).ToString(); //allstr.Substring(10, 6).ToString();
                        MACHID = ASTUtility.Left(allstr, 3);//MACHID = ASTUtility.Right(allstr, 2);//allstr.Substring(26,2).ToString();


                        string ADAT1 = "20" + adt.Substring(4, 2).ToString() + "/" + adt.Substring(2, 2).ToString() + "/" + adt.Substring(0, 2).ToString();
                        ADAT = Convert.ToDateTime(ADAT1);

                        atm = allstr.Substring(9, 4).ToString();
                        if (Convert.ToInt32(atm.Substring(0, 2)) >= 12)
                        {
                            h1 = (Convert.ToInt32(atm.Substring(0, 2))).ToString();
                            h1 = h1.PadLeft(2, '0');
                            ampm = " PM";
                        }
                        else
                        {
                            h1 = (atm.Substring(0, 2)).ToString();
                            ampm = " AM"; //TEST
                        }
                        string dttime = ADAT1 + "  " + h1 + ":" + atm.Substring(2, 2).ToString() + ":" +
                                "00" + ampm.ToString();

                        ATIME = Convert.ToDateTime(dttime.ToString());

                        DateTime seldt = Convert.ToDateTime(this.txtMrrDate.Text);
                        seldate = (seldt.ToString("dd-MMM-yyyy"));
                        bool result = HRData.UpdateTransInfo(comcod, "SP_ATTN_UPDATE", "ATTN_UPDATE_TEMP", "", IDCARDNO1, Convert.ToDateTime(ADAT).ToString(),
                                Convert.ToDateTime(ATIME).ToString(), MACHID, seldate, "", "", "", "", "", "", "", "", "");
                        if (!result)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        }

                    }

                }
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);





                //Delete File
                string savelocation = Server.MapPath("~") + "\\Upload\\Attndancefiles\\";
                string[] filePaths = Directory.GetFiles(savelocation);
                foreach (string filePath in filePaths)
                    File.Delete(filePath);





            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }




            //((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //try
            //{
            //    string StrFileName = string.Empty;

            //    if (File1.PostedFile != null)
            //    {
            //        StrFileName = File1.PostedFile.FileName.Substring(File1.PostedFile.FileName.LastIndexOf("\\") + 1);
            //        string StrFileType = File1.PostedFile.ContentType;
            //        int IntFileSize = File1.PostedFile.ContentLength;
            //        if (IntFileSize <= 0)

            //            ((Label)this.Master.FindControl("lblmsg")).Text = "Uploading of file failed";

            //        else
            //        {
            //            File1.PostedFile.SaveAs(Server.MapPath("..\\..\\Upload\\" + StrFileName));
            //            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //        }
            //    }
            //    if (StrFileName == "")
            //    {
            //        ((Label)this.Master.FindControl("lblmsg")).Text = "Please fill a file";
            //        return;

            //    }

            //    if (txtMrrDate.Text.Trim() == "")
            //    {
            //        ((Label)this.Master.FindControl("lblmsg")).Text = " Date can not be a blank";
            //        return;

            //    }

            //    string filename1 = Server.MapPath("~") + ("../Upload/" + StrFileName); //IIS Path
            //    //string filename1 = Server.MapPath("~") + ("Upload/" + StrFileName); Local Path

            //    //string savelocation = Server.MapPath("~") + "\\Image1";

            //    System.IO.FileStream fs = new System.IO.FileStream(filename1, System.IO.FileMode.Open);
            //    System.IO.StreamReader r = new System.IO.StreamReader(fs);
            //    Label3.Text = r.ReadToEnd();
            //    Label4.Text = filename1;
            //    //UpdatePanel1.Controls.Add(Label1);
            //    r.Close();

            //    //******************************************
            //    DateTime adtc = Convert.ToDateTime(this.txtMrrDate.Text);
            //    string seldate = (adtc.ToString("dd-MMM-yyyy"));
            //    string dayidc = (adtc.ToString("yyyyMMdd"));
            //    string fileDateStr = StrFileName.Substring(0, 4) + "/" + StrFileName.Substring(4, 2) + "/" + StrFileName.Substring(6, 2);
            //    DateTime fileDate = Convert.ToDateTime(fileDateStr);
            //    string dayidfile = (fileDate.ToString("yyyyMMdd"));
            //    if (dayidc == dayidfile)
            //    {
            //        SelectedDates("");
            //        GetData_Button();
            //        this.ShowData();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            //}


        }











        private void UploadData()
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            try
            {
                string StrFileName = string.Empty;

                //string testfilename = File1.PostedFile.FileName;
                //string strFileName = Path.GetFileName(testfilename);

                if (File1.PostedFile != null)
                {
                    StrFileName = File1.PostedFile.FileName.Substring(File1.PostedFile.FileName.LastIndexOf("\\") + 1);
                    string StrFileType = File1.PostedFile.ContentType;
                    int IntFileSize = File1.PostedFile.ContentLength;
                    if (IntFileSize <= 0)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Uploading of file failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    }

                    else
                    {
                        File1.PostedFile.SaveAs(Server.MapPath("..\\..\\Upload\\Attndancefiles\\" + StrFileName));
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Data Uploading Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    }
                }
                if (StrFileName == "")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Please fill a file";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }

                if (txtMrrDate.Text.Trim() == "")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = " Date can not be a blank";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }


                string filename1 = Server.MapPath("~") + ("\\Upload\\Attndancefiles\\" + StrFileName); //IIS Path

                //string filename1 = Server.MapPath("~") + ("../Upload/" + StrFileName); //IIS Path
                //string filename1 = Server.MapPath("~") + ("Upload/" + StrFileName); Local Path

                //string savelocation = Server.MapPath("~") + "\\Image1";

                System.IO.FileStream fs = new System.IO.FileStream(filename1, System.IO.FileMode.Open);
                System.IO.StreamReader r = new System.IO.StreamReader(fs);
                Label3.Text = r.ReadToEnd();
                Label4.Text = filename1;
                //UpdatePanel1.Controls.Add(Label1);
                r.Close();


                // Update  Data

                string comcod = this.GetCompCode();
                DataTable t4 = new DataTable();
                t4.Columns.Add("adate", typeof(String));
                t4.Columns.Add("atime", typeof(String));
                t4.Columns.Add("IDCARDNO", typeof(String));
                t4.Columns.Add("machid", typeof(String));


                string ROWID = string.Empty;
                string MACHID = string.Empty;
                string IDCARDNO = string.Empty;
                string LastNo = string.Empty;
                string seldate = Convert.ToDateTime(this.txtMrrDate.Text).ToString("dd-MMM-yyyy");//Problem
                DateTime ADAT;
                DateTime ATIME;
                string retFilePath = Label4.Text.Trim();
                StreamReader objReader = new StreamReader(retFilePath);
                ///////
                string[] X1 = new string[30000];
                string sLine = "";
                int i = 0;
                DataTable t1 = new DataTable();
                t1.Columns.Add("empattn", typeof(String));
                while (sLine != null)
                {
                    DataRow dr = t1.NewRow();
                    sLine = objReader.ReadLine();
                    X1[i] = sLine;
                    dr["empattn"] = X1[i];
                    t1.Rows.Add(dr);
                    i = i + 1;
                }
                objReader.Close();

                string allstr;
                string IDCARDNO1;
                string adt;
                string atm;
                string h1;
                string ampm;

                for (int j = 0; j < t1.Rows.Count - 1; j++)
                {

                    allstr = t1.Rows[j]["empattn"].ToString();
                    adt = allstr.Substring(3, 6).ToString();
                    IDCARDNO1 = "0" + allstr.Substring(19, 5).ToString(); //allstr.Substring(10, 6).ToString();
                    MACHID = ASTUtility.Left(allstr, 3);//MACHID = ASTUtility.Right(allstr, 2);//allstr.Substring(26,2).ToString();

                    string a1 = adt.Substring(0, 4).ToString();
                    string ADAT1 = "20" + adt.Substring(4, 2).ToString() + "/" + adt.Substring(2, 2).ToString() + "/" + adt.Substring(0, 2).ToString();
                    ADAT = Convert.ToDateTime(ADAT1);

                    atm = allstr.Substring(9, 4).ToString();
                    if (Convert.ToInt32(atm.Substring(0, 2)) >= 12)
                    {
                        h1 = (Convert.ToInt32(atm.Substring(0, 2))).ToString();
                        h1 = h1.PadLeft(2, '0');
                        ampm = " PM";
                    }
                    else
                    {
                        h1 = (atm.Substring(0, 2)).ToString();
                        ampm = " AM"; //TEST
                    }
                    string dttime = ADAT1 + "  " + h1 + ":" + atm.Substring(2, 2).ToString() + ":" +
                            "00" + ampm.ToString();

                    ATIME = Convert.ToDateTime(dttime.ToString());

                    DateTime seldt = Convert.ToDateTime(this.txtMrrDate.Text);
                    seldate = (seldt.ToString("dd-MMM-yyyy"));
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ATTN_UPDATE", "ATTN_UPDATE_TEMP", "", IDCARDNO1, Convert.ToDateTime(ADAT).ToString(),
                            Convert.ToDateTime(ATIME).ToString(), MACHID, seldate, "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    }

                }

             ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);





                //Delete File
                string savelocation = Server.MapPath("~") + "\\Upload\\Attndancefiles\\";
                string[] filePaths = Directory.GetFiles(savelocation);
                foreach (string filePath in filePaths)
                    File.Delete(filePath);





            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }




            //this.lblmsg.Visible = true;
            //try
            //{
            //    string StrFileName = string.Empty;

            //    if (File1.PostedFile != null)
            //    {
            //        StrFileName = File1.PostedFile.FileName.Substring(File1.PostedFile.FileName.LastIndexOf("\\") + 1);
            //        string StrFileType = File1.PostedFile.ContentType;
            //        int IntFileSize = File1.PostedFile.ContentLength;
            //        if (IntFileSize <= 0)

            //         ((Label)this.Master.FindControl("lblmsg")).Text = "Uploading of file failed";

            //        else
            //        {
            //            File1.PostedFile.SaveAs(Server.MapPath("..\\..\\Upload\\" + StrFileName));
            //         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //        }
            //    }
            //    if (StrFileName == "")
            //    {
            //     ((Label)this.Master.FindControl("lblmsg")).Text = "Please fill a file";
            //        return;

            //    }

            //    if (txtMrrDate.Text.Trim() == "")
            //    {
            //     ((Label)this.Master.FindControl("lblmsg")).Text = " Date can not be a blank";
            //        return;

            //    }

            //    string filename1 = Server.MapPath("~") + ("../Upload/" + StrFileName); //IIS Path
            //    //string filename1 = Server.MapPath("~") + ("Upload/" + StrFileName); Local Path

            //    //string savelocation = Server.MapPath("~") + "\\Image1";

            //    System.IO.FileStream fs = new System.IO.FileStream(filename1, System.IO.FileMode.Open);
            //    System.IO.StreamReader r = new System.IO.StreamReader(fs);
            //    Label3.Text = r.ReadToEnd();
            //    Label4.Text = filename1;
            //    //UpdatePanel1.Controls.Add(Label1);
            //    r.Close();

            //    //******************************************
            //    DateTime adtc = Convert.ToDateTime(this.txtMrrDate.Text);
            //    string seldate = (adtc.ToString("dd-MMM-yyyy"));
            //    string dayidc = (adtc.ToString("yyyyMMdd"));
            //    string fileDateStr = StrFileName.Substring(0, 4) + "/" + StrFileName.Substring(4, 2) + "/" + StrFileName.Substring(6, 2);
            //    DateTime fileDate = Convert.ToDateTime(fileDateStr);
            //    string dayidfile = (fileDate.ToString("yyyyMMdd"));
            //    if (dayidc == dayidfile)
            //    {
            //        SelectedDates("");
            //        GetData_Button();
            //        this.ShowData();
            //    }

            //}
            //catch (Exception ex)
            //{
            // ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            //}


        }

        private void UploadDataFinlay()
        {


        
            try
            {
                string StrFileName = string.Empty;
                if (File1.PostedFile != null)
                {
                    StrFileName = File1.PostedFile.FileName.Substring(File1.PostedFile.FileName.LastIndexOf("\\") + 1);
                    string StrFileType = File1.PostedFile.ContentType;
                    int IntFileSize = File1.PostedFile.ContentLength;
                    if (IntFileSize <= 0)
                    {
                        Msg = "Uploading of file failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);
                         
                    }

                    else
                    {
                        File1.PostedFile.SaveAs(Server.MapPath("..\\..\\Upload\\Attndancefiles\\" + StrFileName));
                        Msg = "Data Uploading Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Msg + "');", true);

 
                    }
                }
                if (StrFileName == "")
                {
                    Msg = "Please fill a file";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);
                     
                    return;

                }

                if (txtMrrDate.Text.Trim() == "")
                {
                    Msg = " Date can not be a blank";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);

                     
                    return;

                }


                string filename1 = Server.MapPath("~") + ("\\Upload\\Attndancefiles\\" + StrFileName); //IIS Path

                //string filename1 = Server.MapPath("~") + ("../Upload/" + StrFileName); //IIS Path
                //string filename1 = Server.MapPath("~") + ("Upload/" + StrFileName); Local Path

                //string savelocation = Server.MapPath("~") + "\\Image1";

                System.IO.FileStream fs = new System.IO.FileStream(filename1, System.IO.FileMode.Open);
                System.IO.StreamReader r = new System.IO.StreamReader(fs);
                Label3.Text = r.ReadToEnd();
                Label4.Text = filename1;
                //UpdatePanel1.Controls.Add(Label1);
                r.Close();


                // Update  Data

                string comcod = this.GetCompCode();
                DataTable t4 = new DataTable();
                t4.Columns.Add("adate", typeof(String));
                t4.Columns.Add("atime", typeof(String));
                t4.Columns.Add("IDCARDNO", typeof(String));
                t4.Columns.Add("machid", typeof(String));


                string ROWID = string.Empty;
                string MACHID = string.Empty;
                string IDCARDNO = string.Empty;
                string LastNo = string.Empty;
                string seldate = Convert.ToDateTime(this.txtMrrDate.Text).ToString("dd-MMM-yyyy");//Problem
                DateTime ADAT;
                DateTime ATIME;
                string retFilePath = Label4.Text.Trim();
                StreamReader objReader = new StreamReader(retFilePath);
                ///////
                string[] X1 = new string[30000];
                string sLine = "";
                int i = 0;
                DataTable t1 = new DataTable();
                t1.Columns.Add("empattn", typeof(String));
                while (sLine != null)
                {
                    DataRow dr = t1.NewRow();
                    sLine = objReader.ReadLine();
                    X1[i] = sLine;
                    dr["empattn"] = X1[i];
                    t1.Rows.Add(dr);
                    i = i + 1;
                }
                objReader.Close();               
                string IDCARDNO1;
                string adt;
                string[] arr;
                foreach (DataRow dr1 in t1.Rows)
                {
                    arr = dr1["empattn"].ToString().Split(',');
                    if (dr1["empattn"].ToString().Trim().Length ==0)
                    {

                        break;
                    }

                   
                    IDCARDNO1 = arr[2].Substring(1, arr[2].Length - 2);
                    adt =arr[4].Substring(1,arr[4].Length-2);
                    ATIME = Convert.ToDateTime(adt + " "+arr[5].Substring(1,arr[5].Length-2));
                    MACHID = arr[7].Substring(1, arr[7].Length - 2);                  
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ATTN_UPDATE", "ATTN_UPDATE_TEMP", "", IDCARDNO1, adt,
                            Convert.ToDateTime(ATIME).ToString(), MACHID, "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        Msg = "Updated Fail";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);

                       
                    }
                    

                }

                Msg = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);

                
                //Delete File
                string savelocation = Server.MapPath("~") + "\\Upload\\Attndancefiles\\";
                string[] filePaths = Directory.GetFiles(savelocation);
                foreach (string filePath in filePaths)
                    File.Delete(filePath);





            }
            catch (Exception ex)
            {
                Msg = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);
 
            }




        }
        private void UploadDataGreenLand()
        {
            string Msg;
            try
            {
                string StrFileName = string.Empty;

                if (File1.PostedFile != null)
                {
                    StrFileName = File1.PostedFile.FileName.Substring(File1.PostedFile.FileName.LastIndexOf("\\") + 1);
                    string StrFileType = File1.PostedFile.ContentType;
                    int IntFileSize = File1.PostedFile.ContentLength;
                    if (IntFileSize <= 0)
                    {
                        Msg = "Uploading of file failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);
                         
                    }

                    else
                    {
                        File1.PostedFile.SaveAs(Server.MapPath("..\\..\\Upload\\Attndancefiles\\" + StrFileName));

                        Msg = "Data Uploading Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Msg + "');", true);
                         
                    }
                }
                if (StrFileName == "")
                {
                    Msg = "Please fill a file";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);
                     
                    return;

                }

                if (txtMrrDate.Text.Trim() == "")
                {
                    Msg = " Date can not be a blank";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);

                    
                    return;

                }


                string filename1 = Server.MapPath("~") + ("\\Upload\\Attndancefiles\\" + StrFileName); //IIS Path

                //string filename1 = Server.MapPath("~") + ("../Upload/" + StrFileName); //IIS Path
                //string filename1 = Server.MapPath("~") + ("Upload/" + StrFileName); Local Path

                //string savelocation = Server.MapPath("~") + "\\Image1";

                System.IO.FileStream fs = new System.IO.FileStream(filename1, System.IO.FileMode.Open);
                System.IO.StreamReader r = new System.IO.StreamReader(fs);
                Label3.Text = r.ReadToEnd();
                Label4.Text = filename1;
                //UpdatePanel1.Controls.Add(Label1);
                r.Close();


                // Update  Data

                string comcod = this.GetCompCode();
                DataTable t4 = new DataTable();
                t4.Columns.Add("adate", typeof(String));
                t4.Columns.Add("atime", typeof(String));
                t4.Columns.Add("IDCARDNO", typeof(String));
                t4.Columns.Add("machid", typeof(String));


                string ROWID = string.Empty;
                string MACHID = string.Empty;
                string IDCARDNO = string.Empty;
                string LastNo = string.Empty;
                string seldate = Convert.ToDateTime(this.txtMrrDate.Text).ToString("dd-MMM-yyyy");//Problem
                DateTime ADAT;
                DateTime ATIME;



                string retFilePath = Label4.Text.Trim();

                StreamReader objReader = new StreamReader(retFilePath);
                ///////
                string[] X1 = new string[5000000];
                string sLine = "";
                int i = 0;
                DataTable t1 = new DataTable();
                t1.Columns.Add("empattn", typeof(String));
                while (sLine != null)
                {
                    DataRow dr = t1.NewRow();
                    sLine = objReader.ReadLine();
                    X1[i] = sLine;
                    dr["empattn"] = X1[i];
                    t1.Rows.Add(dr);
                    i = i + 1;
                }
                objReader.Close();

                string allstr;
                string IDCARDNO1;
                string adt;
                string atm;
                string h1;
                string ampm;

                for (int j = 0; j < t1.Rows.Count - 1; j++)
                {

                    allstr = t1.Rows[j]["empattn"].ToString();
                    adt = allstr.Substring(16, 8).ToString();
                    IDCARDNO1 = allstr.Substring(5, 10).ToString(); //allstr.Substring(10, 6).ToString();
                    MACHID = allstr.Substring(1, 3).ToString();

                    string ADAT1 = adt.Substring(0, 4).ToString() + "/" + adt.Substring(4, 2).ToString() + "/" + adt.Substring(6, 2).ToString();//(yyyy/MM/dd)
                    ADAT = Convert.ToDateTime(ADAT1);

                    atm = allstr.Substring(25, 6).ToString();
                    if (Convert.ToInt32(atm.Substring(0, 2)) >= 12)
                    {
                        h1 = (Convert.ToInt32(atm.Substring(0, 2))).ToString();
                        h1 = h1.PadLeft(2, '0');
                        ampm = " PM";
                    }
                    else
                    {
                        h1 = (atm.Substring(0, 2)).ToString();
                        ampm = " AM"; //TEST
                    }
                    string dttime = ADAT1 + "  " + h1 + ":" + atm.Substring(2, 2).ToString() + ":" +
                             atm.Substring(4, 2).ToString();

                    ATIME = Convert.ToDateTime(dttime.ToString());
                    DateTime seldt = Convert.ToDateTime(this.txtMrrDate.Text);
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ATTN_UPDATE", "ATTENDANCEUPDATE", "", IDCARDNO1, Convert.ToDateTime(ADAT).ToString(),
                            Convert.ToDateTime(ATIME).ToString(), MACHID, seldate, "", "", "", "", "", "", "", "", "");

                }


                //Delete File
                string savelocation = Server.MapPath("~") + "\\Upload\\Attndancefiles\\";
                string[] filePaths = Directory.GetFiles(savelocation);
                foreach (string filePath in filePaths)
                    File.Delete(filePath);





            }
            catch (Exception ex)
            {
                 
                Msg = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);

            }



        }
        private void UploadDataCPDL()
        {



            try
            {
                string StrFileName = string.Empty;
                if (File1.PostedFile != null)
                {
                    StrFileName = File1.PostedFile.FileName.Substring(File1.PostedFile.FileName.LastIndexOf("\\") + 1);
                    string StrFileType = File1.PostedFile.ContentType;
                    int IntFileSize = File1.PostedFile.ContentLength;
                    if (IntFileSize <= 0)
                    {
                        Msg = "Uploading of file failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);

                    }

                    else
                    {
                        File1.PostedFile.SaveAs(Server.MapPath("..\\..\\Upload\\Attndancefiles\\" + StrFileName));
                        Msg = "Data Uploading Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Msg + "');", true);


                    }
                }
                if (StrFileName == "")
                {
                    Msg = "Please fill a file";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);

                    return;

                }

                if (txtMrrDate.Text.Trim() == "")
                {
                    Msg = " Date can not be a blank";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);


                    return;

                }


                string filename1 = Server.MapPath("~") + ("\\Upload\\Attndancefiles\\" + StrFileName); //IIS Path

                //string filename1 = Server.MapPath("~") + ("../Upload/" + StrFileName); //IIS Path
                //string filename1 = Server.MapPath("~") + ("Upload/" + StrFileName); Local Path

                //string savelocation = Server.MapPath("~") + "\\Image1";

                System.IO.FileStream fs = new System.IO.FileStream(filename1, System.IO.FileMode.Open);
                System.IO.StreamReader r = new System.IO.StreamReader(fs);
                Label3.Text = r.ReadToEnd();
                Label4.Text = filename1;
                //UpdatePanel1.Controls.Add(Label1);
                r.Close();


                // Update  Data

                string comcod = this.GetCompCode();
                DataTable t4 = new DataTable();
                t4.Columns.Add("adate", typeof(String));
                t4.Columns.Add("atime", typeof(String));
                t4.Columns.Add("IDCARDNO", typeof(String));
                t4.Columns.Add("machid", typeof(String));


                string ROWID = string.Empty;
                string MACHID = string.Empty;
                string IDCARDNO = string.Empty;
                string LastNo = string.Empty;
                string seldate = Convert.ToDateTime(this.txtMrrDate.Text).ToString("dd-MMM-yyyy");//Problem
                DateTime ADAT;
                DateTime ATIME;
                string retFilePath = Label4.Text.Trim();
                StreamReader objReader = new StreamReader(retFilePath);
                ///////
                string[] X1 = new string[30000];
                string sLine = "";
                int i = 0;
                DataTable t1 = new DataTable();
                t1.Columns.Add("empattn", typeof(String));
                while (sLine != null)
                {
                    DataRow dr = t1.NewRow();
                    sLine = objReader.ReadLine();
                    X1[i] = sLine;
                    dr["empattn"] = X1[i];
                    t1.Rows.Add(dr);
                    i = i + 1;
                }
                objReader.Close();
                string IDCARDNO1;
                string adt;
                string adtime;
                string addatetime;
                
                string[] arr;
                int rowCount = 0;
                string  rowIdCard = "";
                foreach (DataRow dr1 in t1.Rows)
                {
                    arr = dr1["empattn"].ToString().Trim().Split(',');

                    if (dr1["empattn"].ToString().Trim().Length != 0)
                    {
                        IDCARDNO1 = arr[0].Trim();
                        adt = arr[2].Trim();
                        adtime = arr[3].Trim();

                        addatetime = (adt + " " + adtime);
                        //ATIME = Convert.ToDateTime(adt + " " + adtime);
                        MACHID = arr[1];
                        bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ATTN_UPDATE", "ATTENDANCEUPDATE", comcod, IDCARDNO1, Convert.ToDateTime(adt).ToString(),
                                addatetime, MACHID.Trim(), seldate, "", "", "", "", "", "", "", "", "");
                        rowCount++;
                    }
                    else
                    {
                        rowIdCard+= arr[0].Trim()+",";
                    }
                }

                Msg = "Updated Successfully: "+ rowCount.ToString()+", Failed="+ rowIdCard;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);


                //Delete File
                string savelocation = Server.MapPath("~") + "\\Upload\\Attndancefiles\\";
                string[] filePaths = Directory.GetFiles(savelocation);
                foreach (string filePath in filePaths)
                    File.Delete(filePath);





            }
            catch (Exception ex)
            {
                Msg = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);

            }




        }
        protected void lbtnShowData_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }

        protected void SelectedDates(string stat1)
        {
            string mTRNDAT = this.txtMrrDate.Text.Trim(); // joinning Date
            this.CalExt1.SelectedDate = Convert.ToDateTime(mTRNDAT);
        }


        protected void cusbuttondupload_Click(object sender, EventArgs e)
        {
            DateTime adtc = Convert.ToDateTime(this.txtMrrDate.Text);
            string seldate = (adtc.ToString("dd-MMM-yyyy"));
            string dayidc = (adtc.ToString("yyyyMMdd"));
            string fileDateStr = this.Label4.Text.Substring(0, 4) + "/" + this.Label4.Text.Substring(4, 2) + "/" + this.Label4.Text.Substring(6, 2);
            DateTime fileDate = Convert.ToDateTime(fileDateStr);
            string dayidfile = (fileDate.ToString("yyyyMMdd"));
            if (dayidc == dayidfile)
            {
                SelectedDates("");
                GetData_Button();
            }
        }

        protected void GetData_Button()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataTable t4 = new DataTable();
            t4.Columns.Add("adate", typeof(String));
            t4.Columns.Add("atime", typeof(String));
            t4.Columns.Add("IDCARDNO", typeof(String));
            t4.Columns.Add("machid", typeof(String));


            string ROWID = string.Empty;
            string MACHID = string.Empty;
            string IDCARDNO = string.Empty;
            string LastNo = string.Empty;
            string seldate = Convert.ToDateTime(this.txtMrrDate.Text).ToString("dd-MMM-yyyy");//Problem
                                                                                              //string ATIME = string.Empty;
                                                                                              //D_D.DataSource = null;
                                                                                              //D_D.DataBind();
            DateTime ADAT;
            DateTime ATIME;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string aday = Convert.ToDateTime(this.txtMrrDate.Text).ToString("yyyyMMdd");
            /////////////////////////
            SelectedDates("");
            //string mach = cmbmach.Text;
            string retFilePath = Label4.Text.Trim();

            StreamReader objReader = new StreamReader(retFilePath);
            ///////
            string[] X1 = new string[30000];
            string sLine = "";
            int i = 0;
            DataTable t1 = new DataTable();
            t1.Columns.Add("empattn", typeof(String));
            while (sLine != null)
            {
                DataRow dr = t1.NewRow();
                sLine = objReader.ReadLine();
                X1[i] = sLine;
                dr["empattn"] = X1[i];
                t1.Rows.Add(dr);
                i = i + 1;
            }
            objReader.Close();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ATTN_UPDATE", "ATTN_LAST_NO", seldate, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            if (ds3.Tables.Count == 0)
                return;
            if (ds3.Tables[0].Rows.Count == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "File uploaded Successfully";
                return;
            }
            LastNo = (ds3.Tables[0].Rows[0]["MaxNo"]).ToString().Trim();
            for (int j = 0; j < t1.Rows.Count - 1; j++)
            {
                string allstr;
                string IDCARDNO1;
                string adt;
                string atm;
                string h1;
                string ampm;
                allstr = t1.Rows[j]["empattn"].ToString();
                adt = allstr.Substring(3, 6).ToString();
                IDCARDNO1 = "0" + allstr.Substring(19, 5).ToString(); //allstr.Substring(10, 6).ToString();
                MACHID = ASTUtility.Left(allstr, 3);//MACHID = ASTUtility.Right(allstr, 2);//allstr.Substring(26,2).ToString();

                string a1 = adt.Substring(0, 4).ToString();
                string ADAT1 = "20" + adt.Substring(4, 2).ToString() + "/" + adt.Substring(2, 2).ToString() + "/" + adt.Substring(0, 2).ToString();
                ADAT = Convert.ToDateTime(ADAT1);

                atm = allstr.Substring(9, 4).ToString();
                if (Convert.ToInt32(atm.Substring(0, 2)) >= 12)
                {
                    h1 = (Convert.ToInt32(atm.Substring(0, 2))).ToString();
                    h1 = h1.PadLeft(2, '0');
                    ampm = " PM";
                }
                else
                {
                    h1 = (atm.Substring(0, 2)).ToString();
                    ampm = " AM"; //TEST
                }
                string dttime = ADAT1 + "  " + h1 + ":" + atm.Substring(2, 2).ToString() + ":" +
                        "00" + ampm.ToString();

                ATIME = Convert.ToDateTime(dttime.ToString());



                DateTime seldt = Convert.ToDateTime(this.txtMrrDate.Text);
                seldate = (seldt.ToString("dd-MMM-yyyy"));
                SelectedDates("");
                string rid = seldt.ToString("yyMMdd");
                LastNo = Convert.ToString((Convert.ToInt64(LastNo) + 1));
                ROWID = (rid + LastNo.PadLeft(5, '0'));

                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ATTN_UPDATE", "ATTN_UPDATE_TEMP", "", IDCARDNO1, Convert.ToDateTime(ADAT).ToString(),
                        Convert.ToDateTime(ATIME).ToString(), MACHID, seldate, "", "", "", "", "", "", "", "", "");

            }



        }

        protected void lbtnTranfered_Click(object sender, EventArgs e)
        {
            bool uptest = true;
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            if (this.txtMrrDate.Text.Trim() != "")
            {

                string seldate = Convert.ToDateTime(this.txtMrrDate.Text).ToString("dd-MMM-yyyy");
                DataSet ds3 = HRData.GetTransInfo("dbo_hrm.SP_ATTN_UPDATE", "UPDATETREANSFEREDINFO", seldate, "", "", "", "", "", "", "", "", "");

                if (ds3 != null)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated  failed!";

                }
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Enter  Date";
            }
        }
        protected void D_M_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }

        protected void lnkbtaUpLocalpc_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            bool uptest = true;
            System.IO.StreamReader sr = new System.IO.StreamReader(@"E:\tas\output\inout.txt");
            string line;
            while (sr.Peek() != -1)
            {
                line = sr.ReadLine();
                string[] s = line.Split(',');
                string d = s[2].Substring(0, 2);
                string yyyy = s[2].Substring(6, 4);
                string mmm = s[2].Substring(3, 2);
                string mon = (mmm == "01" ? "Jan" : mmm == "02" ? "Feb" : mmm == "03" ? "Mar" : mmm == "04" ? "Apr" : mmm == "05" ? "May" : mmm == "06" ? "Jun"
                : mmm == "07" ? "Jul" : mmm == "08" ? "Aug" : mmm == "09" ? "Sep" : mmm == "10" ? "Oct" : mmm == "11" ? "Nov" : "Dec");
                string adate = d + "-" + mon + "-" + yyyy;
                string atime = adate + " " + s[3].ToString().Trim();


                uptest = HRData.UpdateTransInfo("dbo_hrm.SP_ATTN_UPDATE", "NEW_ATTN_UPDATE", s[0].ToString().Trim(), s[1].Substring(6).Trim(), adate, atime, adate, "", "", "", "", "", "", "", "", "", "", "");
                if (uptest)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated  failed!";
                }
            }
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        private string cutdate(string ss)
        {
            string month = "";
            if (ss == "01")
                month = "Jan";
            if (ss == "02")
                month = "Feb";
            if (ss == "03")
                month = "Mar";
            if (ss == "04")
                month = "Apr";
            if (ss == "05")
                month = "May";
            if (ss == "06")
                month = "Jun";
            if (ss == "07")
                month = "Jul";
            if (ss == "08")
                month = "Aug";
            if (ss == "09")
                month = "Sep";
            if (ss == "10")
                month = "Oct";
            if (ss == "11")
                month = "Nov";
            if (ss == "12")
                month = "Dec";
            return month;
        }



        //

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void ShowData()
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            Session.Remove("ShowAtten");

            string comcod = this.GetCompCode();

            string date = this.txtMrrDate.Text;
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "SHOWEMPATTEN", "", "", date, "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvDailyAttn.DataSource = null;
                this.gvDailyAttn.DataBind();
                return;
            }

            Session["ShowAtten"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string comid = dt1.Rows[0]["comid"].ToString();
            string secid = dt1.Rows[0]["secid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["comid"].ToString() == comid)
                {
                    comid = dt1.Rows[j]["comid"].ToString();
                    dt1.Rows[j]["comname"] = "";

                }
                if (dt1.Rows[j]["secid"].ToString() == secid)
                {

                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["section"] = "";


                }

                else
                {
                    comid = dt1.Rows[j]["comid"].ToString();
                    secid = dt1.Rows[j]["secid"].ToString();
                }
            }



            return dt1;

        }
        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            bool result;
            this.SaveValue();
            DataTable dt = (DataTable)Session["ShowAtten"];
            string comcod = this.GetCompCode();
            string date = this.txtMrrDate.Text;
            string dayid = Convert.ToDateTime(this.txtMrrDate.Text).ToString("yyyyMMdd");

            //this.lFinalUpdate.Enabled = (Convert.ToBoolean(dt[0]["entry"]));

            //result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DELETEOFFTIME", dayid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //string absent = dt.Rows[i]["absnt"].ToString().Trim();
                //string leave = dt.Rows[i]["leave"].ToString().Trim();
                //if ((absent != "A") && (leave != "L"))
                //{

                string empid = dt.Rows[i]["empid"].ToString();
                string machid = "01";
                string idcardno = dt.Rows[i]["idcardno"].ToString();
                string intime = dt.Rows[i]["intime"].ToString();
                string outtime = dt.Rows[i]["outtime"].ToString();
                string offintime = dt.Rows[i]["offintime"].ToString();
                string offoutime = dt.Rows[i]["offouttime"].ToString();
                string lnintime = dt.Rows[i]["lnchintime"].ToString();
                string lnoutime = dt.Rows[i]["lnchouttime"].ToString();
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTORUPEMPOFFTIMEAUTO", dayid, empid, machid, idcardno, intime, outtime, offintime, offoutime, lnintime, lnoutime, "", "", "", "", "");
                // }
                //if (absent == "A")
                //{
                //    string empid = dt.Rows[i]["empid"].ToString();
                //    string frmdate = Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("dd-MMM-yyyy");
                //    string absfl = "1";
                //    string month = Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("ddMMyyyy").Substring(2, 2);
                //    //tring month1 = month.PadLeft(2, '0');
                //    string year = ASTUtility.Right(Convert.ToDateTime(dt.Rows[i]["intime"]).ToString("dd-MMM-yyyy"), 4);
                //    string monyr = month + year;

                //    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INORUPDATEABSENTCT", empid, frmdate, absfl, monyr, "", "", "", "", "", "", "", "", "", "", "");

                //}

            }

         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";



        }
        private void SaveValue()
        {
            string comcod = this.GetCompCode();

            DataTable dt = (DataTable)Session["ShowAtten"];
            int TblRowIndex;


            for (int i = 0; i < this.gvDailyAttn.Rows.Count; i++)
            {

                string intime = ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvIntime")).Text.Trim();
                string outime = ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvOuttime")).Text.Trim();
                TblRowIndex = (gvDailyAttn.PageIndex) * gvDailyAttn.PageSize + i;

                dt.Rows[TblRowIndex]["intime"] = Convert.ToDateTime(this.txtMrrDate.Text).ToString("dd-MMM-yyyy") + " " + intime;
                dt.Rows[TblRowIndex]["outtime"] = Convert.ToDateTime(this.txtMrrDate.Text).ToString("dd-MMM-yyyy") + " " + outime;

            }



            Session["ShowAtten"] = dt;


        }



        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["ShowAtten"];

            this.gvDailyAttn.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvDailyAttn.DataSource = (DataTable)Session["ShowAtten"]; ;
            this.gvDailyAttn.DataBind();




        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }
        protected void gvDailyAttn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvDailyAttn.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void btnexcuplosd_Click(object sender, EventArgs e)
        {
            DataTable dt = ((DataTable)Session["tblattn"]);
            DataTable dt1 = (DataTable)Session["XcelData"];

            foreach (DataRow dr1 in dt1.Rows)
            {
                if (dr1["ID_Number"].ToString().Trim().Length == 0)
                    break;

                DataRow dr = dt.NewRow();

                dr["idcardno"] = dr1["ID_Number"];
                dr["adate"] = dr1["Date"];
                dr["atime"] = dr1["Date_Time"];
                dr["machid"] = dr1["Location"];

                dt.Rows.Add(dr);


            }



            Session["tblAttnupload"] = dt;

            this.data_Bind();



            //this.panelexcel.Visible = false;

        }

        private void data_Bind()
        {



            DataTable dt = (DataTable)Session["tblAttnupload"];
            this.gbattn.DataSource = dt;
            this.gbattn.DataBind();
        }



        //private void SaveValueAttn()
        //{
        //    DataTable dt = (DataTable)Session["tblattnupload"];
        //    int TblRowIndex;
        //    for (int i = 0; i < this.gbattn.Rows.Count; i++)
        //    {

        //        string intime = ((TextBox)this.gbattn.Rows[i].FindControl("txtgvIntime")).Text.Trim();
        //        string outime = ((TextBox)this.gbattn.Rows[i].FindControl("txtgvOuttime")).Text.Trim();


        //        TblRowIndex = (gvDailyAttn.PageIndex) * gvDailyAttn.PageSize + i;

        //        dt.Rows[TblRowIndex]["intime"] = Convert.ToDateTime(this.txtMrrDate.Text).ToString("dd-MMM-yyyy") + " " + intime;
        //        dt.Rows[TblRowIndex]["outtime"] = Convert.ToDateTime(this.txtMrrDate.Text).ToString("dd-MMM-yyyy") + " " + outime;

        //    }
        //    Session["ShowAtten"] = dt;
        //}
        protected void lFinalUpdatAttn_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            bool result;
            //this.SaveValueAttn();
            DataTable dt = (DataTable)Session["tblAttnupload"];
            string comcod = this.GetCompCode();

            for (int i = 0; i < dt.Rows.Count; i++)
            {


                string idcardno = dt.Rows[i]["idcardno"].ToString();
                string date = Convert.ToDateTime(dt.Rows[i]["adate"]).ToString("dd-MMM-yyyy");
                string datetime = dt.Rows[i]["atime"].ToString();
                string machid = dt.Rows[i]["machid"].ToString();

                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTORUPEMPATTENDECE", idcardno, date, datetime, machid, "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail !!!!";

                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                }





            }



        }

    }
}

