using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_84_Lea
{
    public partial class UploadLeavExcel : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        string msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtaplydate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                GetCompName();
                lnkbtnShow_Click(null, null);
            }
            if (fileuploadExcel.HasFile)
            {
                try
                {
                    Session.Remove("ExcelData");
                    string connString = "";
                    string StrFileName = string.Empty;
                    if (fileuploadExcel.PostedFile != null && fileuploadExcel.PostedFile.FileName != "")
                    {
                        StrFileName = fileuploadExcel.PostedFile.FileName.Substring(fileuploadExcel.PostedFile.FileName.LastIndexOf("\\") + 1);
                        string StrFileType = fileuploadExcel.PostedFile.ContentType;
                        int IntFileSize = fileuploadExcel.PostedFile.ContentLength;
                        if (IntFileSize <= 0)
                        {
                            return;
                        }
                        else
                        {
                            string savelocation = Server.MapPath("~") + "\\ExcelFile\\";
                            string[] filePaths = Directory.GetFiles(savelocation);
                            foreach (string filePath in filePaths)
                                File.Delete(filePath);
                            fileuploadExcel.PostedFile.SaveAs(Server.MapPath("~") + "\\ExcelFile\\" + StrFileName);
                        }
                    }

                    string strFileType = Path.GetExtension(fileuploadExcel.FileName).ToLower();
                    string apppath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.ToString();
                    string path = Server.MapPath("~") + ("\\ExcelFile\\" + StrFileName);

                    //Connection String to Excel Workbook
                    if (strFileType.Trim() == ".xls")
                    {
                        connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (strFileType.Trim() == ".xlsx")
                    {

                        connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                    }

                    string query = "";
                    query = "SELECT * FROM [Sheet1$]";
                    OleDbConnection conn = new OleDbConnection(connString);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);


                    DataView dv = ds.Tables[0].DefaultView;
                    // dv.RowFilter = ("Card<>''");
                    Session["ExcelData"] = dv.ToTable();
                    da.Dispose();
                    conn.Close();
                    conn.Dispose();
                    string msg = "Please Click Adjust Button";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        private void GetCompName()
        {
            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string txtCompany = "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompanyName.DataTextField = "actdesc";
            this.ddlCompanyName.DataValueField = "actcode";
            this.ddlCompanyName.DataSource = ds1.Tables[0];
            this.ddlCompanyName.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.ddlCompanyName_SelectedIndexChanged(null, null);
            ds1.Dispose();
        }
        private void GetDepartment()
        {
            if (this.ddlCompanyName.Items.Count == 0)
                return;
            string comcod = this.GetComeCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string txtCompanyname = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";

            // string txtCompanyname =(this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) =="00")?"%":this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSearchDept = "%";

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            this.ddlDepartment_SelectedIndexChanged(null, null);
        }
        private void SectionName()
        {

            string comcod = this.GetComeCode();
            string projectcode = this.ddlDepartment.SelectedValue.ToString();
            string txtSSec = "%%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();

        }


        protected void lbtnDedorOtherEernExcelAdjust_Click(object sender, EventArgs e)
        {
            bool isAllValid = true;
            DataTable dt = (DataTable)Session["ExcelData"];
            int rowCount = 0;

            DataTable dt1 = (DataTable)Session["tblover"];
            if (dt.Rows.Count == 0 || dt1.Rows.Count == 0)
            {
                return;
            }
            string Type = "exllv";
            switch (Type)
            {
                case "exllv":


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Card = dt.Rows[i]["Card"].ToString();
                        if (Card.Length == 0)
                        {
                            dt.Rows.RemoveAt(i);
                            continue;
                        }
                        string STR_DATEs = dt.Rows[i]["STR_DATE"].ToString();
                        string END_DATEs = dt.Rows[i]["END_DATE"].ToString();

                        DateTime dateNow = DateTime.Now;
                        DateTime formatedDate = DateTime.TryParse(STR_DATEs, out dateNow) ? Convert.ToDateTime(STR_DATEs) : DateTime.FromOADate(Convert.ToDouble(STR_DATEs));
                        DateTime formatedDateto = DateTime.TryParse(STR_DATEs, out dateNow) ? Convert.ToDateTime(END_DATEs) : DateTime.FromOADate(Convert.ToDouble(END_DATEs));
                       


                       

                        string STR_DATE = Convert.ToDateTime(formatedDate).ToString("dd-MMM-yyyy");
                        string END_DATE = Convert.ToDateTime(formatedDateto).ToString("dd-MMM-yyyy");
                       
                        
                        
                        
                       
                        string EL = dt.Rows[i]["EL"].ToString().Length == 0 ? "0" : dt.Rows[i]["EL"].ToString();
                        string CL = dt.Rows[i]["CL"].ToString().Length == 0 ? "0" : dt.Rows[i]["CL"].ToString();
                        string STAR = dt.Rows[i]["STAR"].ToString().Length == 0 ? "0" : dt.Rows[i]["STAR"].ToString();
                        string Total = dt.Rows[i]["Total"].ToString().Length == 0 ? "0" : dt.Rows[i]["Total"].ToString();

                        
                        if (!IsDate(STR_DATE))
                        {
                            dt.Rows.RemoveAt(i);
                            continue;
                        }
                         
                        if (!IsDate(END_DATE))
                        {
                            dt.Rows.RemoveAt(i);
                            continue;
                        }
                          
                        // Check Adv_Deduction is Number or not.
                        if (!IsNuoDecimal(EL))
                        {
                            dt.Rows[i]["EL"] = 0.00;
                        }
                        // Check Other_Deduction is Number or not.
                        if (!IsNuoDecimal(CL))
                        {
                            dt.Rows[i]["CL"] = 0.00;
                        }
                        // Check Transport is Number or not.
                        if (!IsNuoDecimal(STAR))
                        {
                            dt.Rows[i]["STAR"] = 0.00;
                        }
                        dt.AcceptChanges();
                        isAllValid = true;

                    }
                    if (isAllValid)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            DataRow[] rows = dt.Select("Card ='" + dt1.Rows[i]["idcardno"] + "'");

                            if (rows.Length > 0)
                            {
                                string startdate = Convert.ToDateTime((rows[0]["STR_DATE"])).ToString("dd-MMM-yyyy");
                                string enddate = Convert.ToDateTime((rows[0]["END_DATE"])).ToString("dd-MMM-yyyy");

                                double el = Convert.ToDouble("0" + (rows[0]["EL"]));
                                double cl = Convert.ToDouble("0" + (rows[0]["CL"]));
                                double sl = Convert.ToDouble("0" + (rows[0]["SL"]));
                                double star = Convert.ToDouble("0" + (rows[0]["STAR"]));

                                dt1.Rows[i]["startdate"] = startdate;
                                dt1.Rows[i]["enddate"] = enddate;
                                dt1.Rows[i]["el"] = el;
                                dt1.Rows[i]["cl"] = cl;
                                dt1.Rows[i]["sl"] = sl;
                                dt1.Rows[i]["star"] = star;
                                rowCount++;
                                dt1.AcceptChanges();

                            }

                        }
                    }

                    break;


            }

            Session["tblover"] = dt1;
            this.Data_Bind();

            string msg = "Total Row Adjust : " + rowCount;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

        }

        private void Data_Bind()
        {
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tblover"];

            this.gvLeavExcel.DataSource = dt;
            this.gvLeavExcel.DataBind();
            //this.FooterCalculation();
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();

        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            SectionName();
        }

        protected void ibtnFindDepartment_Click(object sender, EventArgs e)
        {
            this.GetCompName();

        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "Show")
            {

                this.ddlCompanyName.Enabled = false;
                this.ddlDepartment.Enabled = false;
                this.ddlSection.Enabled = false;
                this.lnkbtnShow.Text = "New";
                pnlDedEarnExcel.Visible = true;
                this.SectionView();
                return;
            }

            this.ddlCompanyName.Enabled = true;
            this.ddlDepartment.Enabled = true;
            this.ddlSection.Enabled = true;
            pnlDedEarnExcel.Visible = false;

            this.gvLeavExcel.DataSource = null;
            this.gvLeavExcel.DataBind();
            this.lnkbtnShow.Text = "Show";
        }


        private void SectionView()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();

            string tdate = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
            string comnam = (this.ddlCompanyName.SelectedValue.ToString() == "000000000000") ? "94%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "GETEMPLOYELEAVE_EXCELUPLOAD", tdate, comnam, deptname, section, "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvLeavExcel.DataSource = null;
                this.gvLeavExcel.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid;

            int j;
            secid = dt1.Rows[0]["secid"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["secid"].ToString() == secid)
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["section"] = "";
                }

                else
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                }

            }



            return dt1;

        }

        protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        {

        }

        protected void imgbtnSecSrch_Click(object sender, EventArgs e)
        {

        }

        private bool IsNumber(string value)
        {
            return value.All(char.IsDigit);
        }
        private bool IsNuoDecimal(string value)
        {

            Regex regexLetter = new Regex(@"^[+-] ? ([0 - 9] +\.?[0 - 9]*|\.[0 - 9]+)+$");
            return !(regexLetter.IsMatch(value));

        }
        private bool IsLetter(string value)
        {
            Regex regexLetter = new Regex(@"^[a-zA-Z]+$");
            return regexLetter.IsMatch(value);
        }
        private bool IsDate(string value)
        {
             
            Regex regex = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");
            //Verify whether date entered in dd/MM/yyyy format.
            bool isValid = regex.IsMatch(value);
            //Verify whether entered date is Valid date.
            DateTime dt;
            isValid = DateTime.TryParseExact(value, "dd-MMM-yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out dt);
            return isValid;
        }

        protected void gvLeavExcel_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvLeavExcel.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvLeavExcel_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvLeavExcel_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private void SaveValue()
        {

            string comcod = this.GetComeCode();
            string type = "exllv";
            DataTable dt = (DataTable)Session["tblover"];
            int rowindex;
            switch (type)
            {
                case "exllv":

                    for (int i = 0; i < this.gvLeavExcel.Rows.Count; i++)
                    {

                        double fixhour = Convert.ToDouble("0" + ((TextBox)this.gvLeavExcel.Rows[i].FindControl("txtgvFixed")).Text.Trim());
                        double hourly = Convert.ToDouble("0" + ((TextBox)this.gvLeavExcel.Rows[i].FindControl("txtgvhourly")).Text.Trim());
                        double c1hour = Convert.ToDouble("0" + ((TextBox)this.gvLeavExcel.Rows[i].FindControl("txtgvc1")).Text.Trim());
                        double c2hour = Convert.ToDouble("0" + ((TextBox)this.gvLeavExcel.Rows[i].FindControl("txtgvc2")).Text.Trim());
                        double c3hour = Convert.ToDouble("0" + ((TextBox)this.gvLeavExcel.Rows[i].FindControl("txtgvc3")).Text.Trim());
                        double tohour = fixhour + hourly + c1hour + c2hour + c3hour;
                        rowindex = (this.gvLeavExcel.PageSize) * (this.gvLeavExcel.PageIndex) + i;
                        dt.Rows[rowindex]["fixhour"] = fixhour;
                        dt.Rows[rowindex]["hourly"] = hourly;
                        dt.Rows[rowindex]["c1hour"] = c1hour;
                        dt.Rows[rowindex]["c2hour"] = c2hour;
                        dt.Rows[rowindex]["c3hour"] = c3hour;
                        dt.Rows[rowindex]["tohour"] = tohour;
                    }

                    break;



            }
            Session["tblover"] = dt;
        }
    }
}