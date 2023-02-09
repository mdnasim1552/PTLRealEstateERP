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

namespace RealERPWEB.F_81_Hrm.F_86_All
{
    public partial class EntryOvertimeExelUpload : System.Web.UI.Page
    {
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

                Session.Remove("XcelData");
                //this.txtMrrDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Overtime Upload";
                this.GetYearMonth();

            }


            this.CreateTable();

            if (fileuploadExcel.HasFile)
            {
                try
                {
                    Session.Remove("XcelData");
                    //  ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                    string connString = "";
                    string StrFileName = string.Empty;
                    if (fileuploadExcel.PostedFile != null && fileuploadExcel.PostedFile.FileName != "")
                    {
                        StrFileName =
                            fileuploadExcel.PostedFile.FileName.Substring(
                                fileuploadExcel.PostedFile.FileName.LastIndexOf("\\") + 1);
                        string StrFileType = fileuploadExcel.PostedFile.ContentType;
                        int IntFileSize = fileuploadExcel.PostedFile.ContentLength;
                        if (IntFileSize <= 0)
                        {
                            //  ((Label)this.Master.FindControl("lblmsg")).Text = "Uploading Fail";
                            // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert(' file Uploading failed');", true);
                            return;
                        }
                        else
                        {
                            string savelocation = Server.MapPath("~") + "\\ExcelFileOvertime\\";
                            string[] filePaths = Directory.GetFiles(savelocation);
                            foreach (string filePath in filePaths)
                                File.Delete(filePath);
                            fileuploadExcel.PostedFile.SaveAs(Server.MapPath("~") + "\\ExcelFileOvertime\\" + StrFileName);
                            //   ((Label)this.Master.FindControl("lblmsg")).Text = "Uploading Successfully";
                        }
                    }

                    string strFileType = Path.GetExtension(fileuploadExcel.FileName).ToLower();
                    string apppath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.ToString();
                    //string path = apppath + "ExcelFile\\" + StrFileName;
                    string path = Server.MapPath("~") + ("\\ExcelFileOvertime\\" + StrFileName);

                    //Connection String to Excel Workbook
                    if (strFileType.Trim() == ".xls")
                    {
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path +
                                     ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (strFileType.Trim() == ".xlsx")
                    {
                        connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path +
                                     ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                    }

                    // string query = "SELECT [No] FROM [Sheet1$]";
                    string query =
                       "SELECT * FROM [Sheet1$]";
                    OleDbConnection conn = new OleDbConnection(connString);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    Session["XcelData"] = ds.Tables[0];
                    // this.DataInsert();
                    da.Dispose();
                    conn.Close();
                    conn.Dispose();
                    //this.GetExelData();
                }
                catch (Exception)
                {

                    throw;
                }
            }





        }


        protected void CreateTable()
        {

            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("idcardno", Type.GetType("System.String"));
            // dttemp.Columns.Add("monthid", Type.GetType("System.String"));
            dttemp.Columns.Add("fixedhour", Type.GetType("System.Double"));
            //  dttemp.Columns.Add("hourly", Type.GetType("System.Double"));
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

            Session["tblovrtime"] = dttemp;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetYearMonth()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];

            this.ddlyearmon.SelectedValue = System.DateTime.Today.AddMonths(-1).ToString("yyyyMM");
            this.ddlyearmon.DataBind();
            //this.ddlyearmon.DataBind();
            //string txtdate = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMMM-yyyy");
            ds1.Dispose();
        }


        protected void btnexcuplosd_Click(object sender, EventArgs e)
        {

            DataTable dt = ((DataTable)Session["tblovrtime"]);
            DataTable dt1 = (DataTable)Session["XcelData"];

            if (dt1.Rows.Count == 0 && dt1 == null)
                return;

            foreach (DataRow dr1 in dt1.Rows)
            {
                if (dr1["IdcardNo"].ToString().Trim().Length == 0)
                    break;

                DataRow dr = dt.NewRow();

                dr["idcardno"] = dr1["IdcardNo"];
                // dr["monthid"] = dr1["MonthId"];
                dr["fixedhour"] = dr1["FixedHour"];
                //dr["hourly"] = dr1["Hourly"];

                dt.Rows.Add(dr);


            }


            Session["tblOvrtimeUpload"] = dt;
            this.data_Bind();


        }

        private void data_Bind()
        {
            DataTable dt = (DataTable)Session["tblOvrtimeUpload"];
            // this.gvovertime.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvovertime.DataSource = dt;
            this.gvovertime.DataBind();
            this.Footercal();
        }

        private void Footercal()
        {
            DataTable dt = (DataTable)Session["tblOvrtimeUpload"];

            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvovertime.FooterRow.FindControl("lgvFgvFixed")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(fixedhour)", "")) ? 0.00
                       : dt.Compute("sum(fixedhour)", ""))).ToString("#,##0.00;(#,##0.00); ");

            //((Label)this.gvovertime.FooterRow.FindControl("lgvFhourly")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hourly)", "")) ? 0.00
            //           : dt.Compute("sum(hourly)", ""))).ToString("#,##0.00;(#,##0.00); ");

        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.data_Bind();
        }
        protected void lFinalUpdatOvertime_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            // this.SaveValue();
            DataTable dt = (DataTable)Session["tblOvrtimeUpload"];
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string msg = "";

            string ymon = this.ddlyearmon.SelectedValue.ToString();

            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            bool result = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // string empid = dt.Rows[i]["empid"].ToString();
                string gcod = "07004";
                string Idcardno = dt.Rows[i]["idcardno"].ToString();

                double fixhour = Convert.ToDouble(dt.Rows[i]["fixedhour"]);
                // double hourly = Convert.ToDouble(dt.Rows[i]["hourly"]);
                //double c1hour = Convert.ToDouble(dt.Rows[i]["c1hour"]);
                //double c2hour = Convert.ToDouble(dt.Rows[i]["c2hour"]);
                //double c3hour = Convert.ToDouble(dt.Rows[i]["c3hour"]);
                //double fixrate = Convert.ToDouble(dt.Rows[i]["fixrate"]);
                //double hourrate = Convert.ToDouble(dt.Rows[i]["hourrate"]);
                //double c1rate = Convert.ToDouble(dt.Rows[i]["c1rate"]);
                //double c2rate = Convert.ToDouble(dt.Rows[i]["c2rate"]);
                //double c3rate = Convert.ToDouble(dt.Rows[i]["c3rate"]);


                //string fixamt = (fixhour * fixrate).ToString();
                //string houramt = (hourly * hourrate).ToString();
                //string c1amt = (c1hour * c1rate).ToString();
                //string c2amt = (c2hour * c2rate).ToString();
                //string c3amt = (c3hour * c3rate).ToString();
                //double tohour = Convert.ToDouble(dt.Rows[i]["tohour"]); ; 


                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPlOYEE_OVERTIMEUPLAOD", "INORUPDATEOVERTIMEUPLOADE", ymon, Idcardno, gcod, fixhour.ToString(), date, userid, Sessionid, date, "", "", "", "", "", "");
                if (!result)


                    return;

            }

            //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);



        }
    }
}