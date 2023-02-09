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
using System.IO;
using System.Data.OleDb;

namespace RealERPWEB.F_81_Hrm.F_86_All
{
    public partial class EmpFoodAllow : System.Web.UI.Page
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

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                // this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //this.lblfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");



                this.GetCompName();
                this.GetYearMonth();


            }

            ////Excel Upload (Deduction Upload)
            //if (fileuploadExcel.HasFile)
            //{
            //    try
            //    {
            //        Session.Remove("ExcelData");
            //        string connString = "";
            //        string StrFileName = string.Empty;
            //        if (fileuploadExcel.PostedFile != null && fileuploadExcel.PostedFile.FileName != "")
            //        {
            //            StrFileName = fileuploadExcel.PostedFile.FileName.Substring(fileuploadExcel.PostedFile.FileName.LastIndexOf("\\") + 1);
            //            string StrFileType = fileuploadExcel.PostedFile.ContentType;
            //            int IntFileSize = fileuploadExcel.PostedFile.ContentLength;
            //            if (IntFileSize <= 0)
            //            {
            //                return;
            //            }
            //            else
            //            {
            //                string savelocation = Server.MapPath("~") + "\\ExcelFile\\";
            //                string[] filePaths = Directory.GetFiles(savelocation);
            //                foreach (string filePath in filePaths)
            //                    File.Delete(filePath);
            //                fileuploadExcel.PostedFile.SaveAs(Server.MapPath("~") + "\\ExcelFile\\" + StrFileName);
            //            }
            //        }

            //        string strFileType = Path.GetExtension(fileuploadExcel.FileName).ToLower();
            //        string apppath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.ToString();
            //        string path = Server.MapPath("~") + ("\\ExcelFile\\" + StrFileName);

            //        //Connection String to Excel Workbook
            //        if (strFileType.Trim() == ".xls")
            //        {
            //            connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            //        }
            //        else if (strFileType.Trim() == ".xlsx")
            //        {
            //            connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
            //        }

            //        string query = "";
            //        query = "SELECT * FROM [Sheet1$]";
            //        OleDbConnection conn = new OleDbConnection(connString);
            //        if (conn.State == ConnectionState.Closed)
            //            conn.Open();
            //        OleDbCommand cmd = new OleDbCommand(query, conn);
            //        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            //        DataSet ds = new DataSet();
            //        da.Fill(ds);


            //        DataView dv = ds.Tables[0].DefaultView;
            //        // dv.RowFilter = ("Card<>''");
            //        Session["ExcelData"] = dv.ToTable();
            //        da.Dispose();
            //        conn.Close();
            //        conn.Dispose();
            //        string msg = "Please Click Adjust Button";
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

            //    }
            //    catch (Exception ex)
            //    {

            //        throw ex;
            //    }
            //}

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetYearMonth()
        {
            string comcod = this.GetComeCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];

            this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            this.ddlyearmon.DataBind();
            //this.ddlyearmon.DataBind();
            //string txtdate = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMMM-yyyy");
            ds1.Dispose();
        }
        protected void chkcopy_CheckedChanged(object sender, EventArgs e)
        {

            if (this.chkcopy.Checked)
            {
                this.GetPreYearMonth();
            }
            this.Pnlother.Visible = (this.chkcopy.Checked);
        }
        private void GetPreYearMonth()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlpreyearmon.DataTextField = "yearmon";
            this.ddlpreyearmon.DataValueField = "ymon";
            this.ddlpreyearmon.DataSource = ds1.Tables[0];
            this.ddlpreyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            this.ddlpreyearmon.DataBind();

            ds1.Dispose();
        }

        private void GetCompName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string txtCompany = "%";

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");

            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompanyName.DataTextField = "actdesc";
            this.ddlCompanyName.DataValueField = "actcode";
            this.ddlCompanyName.DataSource = ds1.Tables[0];
            this.ddlCompanyName.DataBind();
            //this.ddlCompanyName_SelectedIndexChanged(null, null);
            this.GetDepartment();
        }

        private void GetDepartment()
        {
            if (this.ddlCompanyName.Items.Count == 0)
                return;
            string comcod = this.GetComeCode();
            string txtCompanyname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSearchDept = "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
        }



        protected void lnkbtnShow_Click1(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "Ok")
            {
                this.ddlyearmon.Enabled = false;
                this.ddlCompanyName.Enabled = false;
                this.ddlDepartment.Enabled = false;
                this.lnkcopy.Visible = true;
                
                //this.lblCompanyName.Visible = true;
                //this.lblDeptDesc.Visible = true;

                //this.lblPage.Visible = true;
                //this.ddlpagesize.Visible = true;
                this.lnkbtnShow.Text = "New";
                //this.lblCompanyName.Text = this.ddlCompanyName.SelectedItem.Text;
                //this.lblDeptDesc.Text = this.ddlDepartment.SelectedItem.Text;
                this.EmpFoodAllowance();

                return;
            }

            this.ddlyearmon.Enabled = true;
            this.ddlCompanyName.Enabled = true;
            this.ddlDepartment.Enabled = true;
            //this.lblCompanyName.Visible = false;
            //this.lblDeptDesc.Visible = false;
            //this.lblPage.Visible = false;
            //this.ddlpagesize.Visible = false;
            this.lnkcopy.Visible = false;
            this.Pnlother.Visible = false;
            this.lnkbtnShow.Text = "Ok";
            //this.lblCompanyName.Text = "";


        }

        private void EmpFoodAllowance()
        {
            Session.Remove("tblfallow");
            string comcod = this.GetComeCode();
            string comnam = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString() + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPFOODALLOWANCEBILLINFO", deptname, MonthId, date, comnam, Empcode, "", "", "", "");
            if (ds2 == null)
            {
                this.gvEmpFAllow.DataSource = null;
                this.gvEmpFAllow.DataBind();
                return;
            }
            Session["tblfallow"] = HiddenSameData(ds2.Tables[0]);

            this.Data_Bind();


        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblfallow"];

            this.gvEmpFAllow.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvEmpFAllow.DataSource = dt;
            this.gvEmpFAllow.DataBind();
            this.FooterCalculation();
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
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblfallow"];
            if (dt.Rows.Count == 0)
                return;

            {

                ((Label)this.gvEmpFAllow.FooterRow.FindControl("lgvFFoodbillamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mfoodamt)", "")) ? 0.00
                    : dt.Compute("sum(mfoodamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                ((Label)this.gvEmpFAllow.FooterRow.FindControl("lgvFFbillothamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othbill)", "")) ? 0.00
                    : dt.Compute("sum(othbill)", ""))).ToString("#,##0.00;(#,##0.00); ");





            }


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string companyname = ddlCompanyName.SelectedItem.Text.Trim();
            string dptName = ddlDepartment.SelectedItem.Text.Trim().Substring(13);
            string monthid = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("MMM-yyyy");// txtdate

            // double tfoodamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mfoodamt)", "")) ? 0.00 : dt.Compute("sum(mfoodamt)", "")));
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            ReportDocument rptFoodAll = new RealERPRPT.R_81_Hrm.R_86_All.rptFoodAllowance();
            DataTable dt2 = (DataTable)Session["tblfallow"];

            DataView dv = dt2.DefaultView;
            dv.RowFilter = ("mfoodamt>0");
            dt2 = dv.ToTable();


            double tfoodamt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(mfoodamt)", "")) ? 0.00 : dt2.Compute("sum(mfoodamt)", "")));
            TextObject CompName = rptFoodAll.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = companyname;

            TextObject rptTxtDptname = rptFoodAll.ReportDefinition.ReportObjects["txtDpt"] as TextObject;
            rptTxtDptname.Text = "Department : " + dptName;

            TextObject rptTxtDate = rptFoodAll.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            rptTxtDate.Text = "The Month of " + monthid;
            TextObject txttk = rptFoodAll.ReportDefinition.ReportObjects["txtInWord"] as TextObject;
            txttk.Text = "In Word: " + ASTUtility.Trans(tfoodamt, 2);

            TextObject txtuserinfo = rptFoodAll.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptFoodAll.SetDataSource(dt2);

            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptFoodAll;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void ibtnFindDepartment_Click(object sender, EventArgs e)
        {
            this.GetCompName();

        }
        protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        {

            this.GetDepartment();

        }

        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tblfallow"];
            int rowindex;
            for (int i = 0; i < this.gvEmpFAllow.Rows.Count; i++)
            {
                double fbillamt = Convert.ToDouble("0" + ((TextBox)this.gvEmpFAllow.Rows[i].FindControl("txtgvfbill")).Text.Trim());

                double fbillothamt = Convert.ToDouble("0" + ((TextBox)this.gvEmpFAllow.Rows[i].FindControl("txtgvothbill")).Text.Trim());
                string remarks = ((TextBox)this.gvEmpFAllow.Rows[i].FindControl("txtgvRemarks")).Text.Trim();



                rowindex = (this.gvEmpFAllow.PageSize) * (this.gvEmpFAllow.PageIndex) + i;
                dt.Rows[rowindex]["mfoodamt"] = fbillamt;
                dt.Rows[rowindex]["othbill"] = fbillothamt;
                dt.Rows[rowindex]["remarks"] = remarks;




            }
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbntUpdateFbill_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            DataTable dt = (DataTable)Session["tblfallow"];
            string comcod = this.GetComeCode();
            string Monthid = this.ddlyearmon.Text.Trim();
            string msg = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();

                double fbillamt = Convert.ToDouble(dt.Rows[i]["mfoodamt"]);

                double othbill = Convert.ToDouble(dt.Rows[i]["othbill"]);
                string remarks = dt.Rows[i]["remarks"].ToString();




                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPDATEFOODBILL", empid, Monthid, fbillamt.ToString(), othbill.ToString(), remarks, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    //((Label)this.Master.FindControl("lblmsg")).Text = "Data update Fail !";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    msg = "Data update Failed !";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

                    return;
                }




            }
            //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //   ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);



        }
        protected void lbtnTotalFoodBill_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void gvEmpFAllow_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            //string comcod = this.GetComeCode();
            //DataTable dt = (DataTable)Session["tblover"];
            //int rowindex = (this.gvEmpFAllow.PageSize) * (this.gvEmpFAllow.PageIndex) + e.RowIndex;
            //string ymon = this.ddlyearmon.SelectedValue.ToString();
            //string dayid = ymon + "01";
            //string empid = dt.Rows[rowindex]["empid"].ToString();
            //bool result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_EMPLOYEE01", "DELEMPOVRTIME", dayid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            //if (result == true)
            //{

            //    dt.Rows[rowindex].Delete();
            //}
            //DataView dv = dt.DefaultView;
            //Session.Remove("tblover");
            //Session["tblover"] = dv.ToTable();
            //this.Data_Bind();
        }
        protected void imgbtnSearchEmployee_Click(object sender, EventArgs e)
        {
            this.EmpFoodAllowance();
        }
        protected void gvEmpFAllow_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvEmpFAllow.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnCopy_Click(object sender, EventArgs e)
        {
            Session.Remove("tblfallow");
            string comcod = this.GetComeCode();
            //string comnam = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";

            //int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            //string nozero = (hrcomln == 4) ? "0000" : "00";
            //string txtCompanyname = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";


            string comnam = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString() + "%";
            string MonthId = this.ddlpreyearmon.SelectedValue.ToString();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlpreyearmon.Text.Trim(), 2) + "/01/" + this.ddlpreyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string mantype ="86%";
            string section = "%%";

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPFOODALLOWANCEBILLINFO", deptname, MonthId, date, comnam, Empcode, "", "", "", "");
            if (ds2 == null)
            {      
                return;
            }
          
            Session["tblfallow"] = HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
            this.chkcopy.Checked = false;
            this.chkcopy_CheckedChanged(null, null);
        }



    }
}