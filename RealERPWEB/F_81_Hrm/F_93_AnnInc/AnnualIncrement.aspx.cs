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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_81_Hrm.F_93_AnnInc
{

    public partial class AnnualIncrement : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length==0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();

                this.txtdate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                this.GetCompany();
                this.GetIncreNo();
                this.tableintosession();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            if (this.lnkbtnShow.Text == "New")
                return;

            Session.Remove("tblcompany");
            string comcod = this.GetComeCode();
            string txtCompany = "%%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETCOMPANYNAME", txtCompany, "", "", "", "", "", "", "", "");

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GET_ACCESSED_COMPANYLIST", txtCompany, userid, "", "", "", "", "", "", "");

            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.GetDeptName();
            ds1.Dispose();
        }
        private void GetDeptName()
        {
            if (this.lnkbtnShow.Text == "New")
                return;
            string comcod = this.GetComeCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string txtCompany = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETPROJECTNAME", Company, txtCompany, "", "", "", "", "", "", "");
            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = ds1.Tables[0];
            this.ddlDept.DataBind();
            this.GetSection();
            ds1.Dispose();
        }
        private void GetSection()
        {

            if (this.lnkbtnShow.Text == "New")
                return;
            string comcod = this.GetComeCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";

            string DeptName = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string SrchSection = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETSECTION", Company, DeptName, SrchSection, "", "", "", "", "", "");
            this.ddlSection.DataTextField = "section";
            this.ddlSection.DataValueField = "seccode";
            this.ddlSection.DataSource = ds1.Tables[0];
            this.ddlSection.DataBind();
            this.ddlSection_SelectedIndexChanged(null, null);
            ds1.Dispose();


        }

        private void GetEmployeeName()
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetComeCode();
                int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
                string compcode = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
                string deptcode = (this.ddlDept.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
                string Section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

                string txtSProject = "%";
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETEMPLOYEENAME", compcode, deptcode, Section, txtSProject, "", "", "", "", "");
                Session["tblempdsg"] = ds3.Tables[0];
                this.ddlEmployee.DataTextField = "empname";
                this.ddlEmployee.DataValueField = "empid";
                this.ddlEmployee.DataSource = ds3.Tables[0];
                this.ddlEmployee.DataBind();
                this.ddlEmployee_SelectedIndexChanged(null, null);
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);


            }


        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptName();
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSection();
        }
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string empid = this.ddlEmployee.SelectedValue.ToString().Trim();
                DataTable dt = (DataTable)Session["tblempdsg"];
                DataRow[] dr = dt.Select("empid = '" + empid + "'");
                if (dr.Length > 0)
                {
                    string errMsg = ((DataTable)Session["tblempdsg"]).Select("empid='" + empid + "'")[0]["desig"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + errMsg + "');", true);

                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);

            }
        }
        private void GetPreviousList()
        {

            string comcod = GetComeCode();
            string mREQDAT = this.GetStdDate(this.txtdate.Text);
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETPREINCREMENTNO", mREQDAT, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.ddlPrevIncList.Items.Clear();
                return;

            }

            this.ddlPrevIncList.DataTextField = "incrno1";
            this.ddlPrevIncList.DataValueField = "incrno";
            this.ddlPrevIncList.DataSource = ds2.Tables[0];
            this.ddlPrevIncList.DataBind();

        }

        private void GetIncreNo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = this.GetStdDate(this.txtdate.Text);
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETINCREMENTNO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            DataTable dt1 = ds3.Tables[0];
            this.txtdate.Text = Convert.ToDateTime(ds3.Tables[0].Rows[0]["maxincdt"].ToString().Trim()).ToString("dd.MM.yyyy");
            this.lblCurIncrNo.Text = ds3.Tables[0].Rows[0]["maxincno1"].ToString().Substring(0, 5);
            this.txtCurIncrNo.Text = ds3.Tables[0].Rows[0]["maxincno1"].ToString().Substring(6);
        }


        protected void GetIncrementNo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = this.GetStdDate(this.txtdate.Text.Trim());
            string mIncNo = "NEWINC";
            if (this.ddlPrevIncList.Items.Count > 0)
                mIncNo = this.ddlPrevIncList.SelectedValue.ToString();

            if (mIncNo == "NEWINC")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETINCREMENTNO", date, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurIncrNo.Text = ds1.Tables[0].Rows[0]["maxincno1"].ToString().Substring(0, 6);
                    this.txtCurIncrNo.Text = ds1.Tables[0].Rows[0]["maxincno1"].ToString().Substring(6, 5);
                    this.ddlPrevIncList.DataTextField = "maxincno1";
                    this.ddlPrevIncList.DataValueField = "maxincno1";
                    this.ddlPrevIncList.DataSource = ds1.Tables[0];
                    this.ddlPrevIncList.DataBind();
                }

            }






        }


        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }



        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "New")
            {
                this.imgbtnPreList.Visible = true;
                this.ddlPrevIncList.Visible = true;
                this.ddlPrevIncList.Items.Clear();

                this.lnkbtnShow.Text = "Ok";
                this.gvAnnIncre.DataSource = null;
                this.gvAnnIncre.DataBind();
                this.ddlCompany.Enabled = true;
                this.ddlDept.Enabled = true;
                this.ddlSection.Enabled = true;
                this.txtdate.Enabled = true;
                Session.Remove("tblAnnInc");
                tableintosession();
                return;
            }


            this.lnkbtnShow.Text = "New";
            this.imgbtnPreList.Visible = false;
            this.ddlPrevIncList.Visible = false;
            this.ddlCompany.Enabled = false;
            this.ddlDept.Enabled = false;
            this.ddlSection.Enabled = false;
            this.ShowInc();
        }

        private string CompanyCalltype()
        {
            string callType = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            switch (comcod)
            {

                case "3347":
                    callType = "GETINCREMENTPEB";//Peb steel
                    break;
                default:

                    callType = "GETINCREMENT"; //
                    break;

            }

            return callType;

        }
        private void ShowInc()
        {
            string comcod = this.GetComeCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string DeptCode = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string SecCode = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string empID = ((this.ddlEmployee.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlEmployee.SelectedValue.ToString()) + "%";

            string txtDate = this.GetStdDate(this.txtdate.Text);
            DataSet ds2;
            if (ddlPrevIncList.Items.Count == 0)
            {
                this.GetIncreNo();
                string calltype = this.CompanyCalltype();

                ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", calltype, Company, DeptCode, SecCode, txtDate, empID, "", "", "", "");
            }
            else
            {

                string preincreno = this.ddlPrevIncList.SelectedValue.ToString();
                ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETPREINCREMENT", preincreno, txtDate, "", "", "", "", "", "", "");
                this.lblCurIncrNo.Text = ds2.Tables[1].Rows[0]["incrno1"].ToString().Substring(0, 6);
                this.txtCurIncrNo.Text = ds2.Tables[1].Rows[0]["incrno1"].ToString().Substring(6, 5);
                this.txtdate.Text = Convert.ToDateTime(ds2.Tables[1].Rows[0]["incrdate"].ToString()).ToString("dd.MM.yyyy");

            }

            if (ds2 == null)
            {
                this.gvAnnIncre.DataSource = null;
                this.gvAnnIncre.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tblAnnInc"] = dt;
            this.LoadGrid();
        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string deptcode = dt1.Rows[0]["deptcode"].ToString();
            string seccode = dt1.Rows[0]["seccode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["deptcode"].ToString() == deptcode && dt1.Rows[j]["seccode"].ToString() == seccode)
                {

                    dt1.Rows[j]["deptname"] = "";
                    dt1.Rows[j]["section"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                        dt1.Rows[j]["deptname"] = "";
                    if (dt1.Rows[j]["seccode"].ToString() == seccode)
                        dt1.Rows[j]["section"] = "";
                }

                deptcode = dt1.Rows[j]["deptcode"].ToString();
                seccode = dt1.Rows[j]["seccode"].ToString();

            }
            return dt1;
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblAnnInc"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_93_AnnInc.AnnIncReport.AnnualIncrement>();

            LocalReport Rpt1 = new LocalReport();

            if (comcod == "3330")
            {

                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_93_AnnInc.RptAnnInctrmentBridge", lst, null, null);
                Rpt1.SetParameters(new ReportParameter("txtDate", "Basic Salary As On  " + Convert.ToDateTime(this.GetStdDate(this.txtdate.Text)).ToString("MMMM dd , yyyy")));

                //rptmattrans = new RealERPRPT.R_81_Hrm.R_93_AnnInc.RptAnnInctrmentBridge();
                //TextObject rptdate = rptmattrans.ReportDefinition.ReportObjects["date"] as TextObject;
                //rptdate.Text = "Basic Salary As On  " + Convert.ToDateTime(this.GetStdDate(this.txtdate.Text)).ToString("MMMM dd , yyyy");


            }

            else if (comcod == "3336" || comcod == "3338")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_93_AnnInc.RptAnnInctrmentAcme", lst, null, null);
                Rpt1.SetParameters(new ReportParameter("txtDate", "Basic Salary As On  " + Convert.ToDateTime(this.GetStdDate(this.txtdate.Text)).ToString("MMMM dd , yyyy")));


            }

            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_93_AnnInc.RptAnnInctrment", lst, null, null);
                Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.GetStdDate(this.txtdate.Text)).ToString("MMMM, yyyy")));


            }


            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            //  Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Annual Increment Proposal"));
            Rpt1.SetParameters(new ReportParameter("txtIncrementNo", "Increment No: " + this.lblCurIncrNo.Text.Trim() + "-" + this.txtCurIncrNo.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";






            // Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string company = this.ddlCompany.SelectedItem.Text.Trim();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblAnnInc"];

            //ReportDocument rptmattrans = new ReportDocument();

            //string comcod = this.GetComeCode();
            //if (comcod=="3101"||comcod=="3330")
            //{
            //    rptmattrans = new RealERPRPT.R_81_Hrm.R_93_AnnInc.RptAnnInctrmentBridge();
            //    TextObject rptdate = rptmattrans.ReportDefinition.ReportObjects["date"] as TextObject;
            //    rptdate.Text = "Basic Salary As On  " + Convert.ToDateTime(this.GetStdDate(this.txtdate.Text)).ToString("MMMM dd , yyyy");

            //}

            //else if(comcod=="3336"||comcod=="3338")
            //{
            //  //  rptmattrans = new RealERPRPT.R_81_Hrm.R_93_AnnInc.RptAnnInctrmentAcme();
            //    TextObject rptdate = rptmattrans.ReportDefinition.ReportObjects["date"] as TextObject;
            //    rptdate.Text = "Date: " + Convert.ToDateTime(this.GetStdDate(this.txtdate.Text)).ToString("MMMM, yyyy");


            //}

            //else
            //{
            //    rptmattrans = new RealERPRPT.R_81_Hrm.R_93_AnnInc.RptAnnInctrment();
            //    TextObject rptdate = rptmattrans.ReportDefinition.ReportObjects["date"] as TextObject;
            //    rptdate.Text = "Date: " + Convert.ToDateTime(this.GetStdDate(this.txtdate.Text)).ToString("MMMM, yyyy");

            //}

            //TextObject rptCname = rptmattrans.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
            //rptCname.Text = company;

            //TextObject rpttrnno = rptmattrans.ReportDefinition.ReportObjects["txtIncreno"] as TextObject;
            //rpttrnno.Text = "Increment No: " + this.lblCurIncrNo.Text.Trim() + "-" + this.txtCurIncrNo.Text.Trim();

            ////TextObject rpttxtinformation = rptmattrans.ReportDefinition.ReportObjects["txtinformation"] as TextObject;
            ////rpttxtinformation.Text = this.txtfmaters.Text.Trim();
            ////TextObject rpttxtspnote = rptmattrans.ReportDefinition.ReportObjects["txtspnote"] as TextObject;
            ////rpttxtspnote.Text = this.txtspnote.Text.Trim();
            //TextObject txtuserinfo = rptmattrans.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptmattrans.SetDataSource(dt1);

            ////string comcod = this.GetComeCode();
            ////string comcod = hst["comcod"].ToString();
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //////rptmattrans.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptmattrans;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }
        protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        {
            this.GetDeptName();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            // this.SaveValue();
            this.LoadGrid();

        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblAnnInc"];
            int TblRowIndex;
            for (int i = 0; i < this.gvAnnIncre.Rows.Count; i++)
            {

                double presal = Convert.ToDouble("0" + ((TextBox)this.gvAnnIncre.Rows[i].FindControl("txtgvpreamt")).Text.Trim());
                double incpercnt = Convert.ToDouble("0" + ((TextBox)this.gvAnnIncre.Rows[i].FindControl("lgvincpercnt")).Text.Trim());
                double incamt = Convert.ToDouble("0" + ((TextBox)this.gvAnnIncre.Rows[i].FindControl("txtgvincamt")).Text.Trim());

                //TblRowIndex = (gvAnnIncre.PageIndex) * gvAnnIncre.PageSize + i;


                incpercnt = incpercnt > 0 ? incpercnt : presal > 0 ? (incamt * 100) / presal : 0.00;
                incamt = incamt > 0 ? incamt : incpercnt > 0 ? (presal * 0.01 * incpercnt) : 0.00;

                double revisesal = presal + incamt;

                TblRowIndex = (this.gvAnnIncre.PageSize) * (this.gvAnnIncre.PageIndex) + i;
                dt.Rows[TblRowIndex]["grossal"] = presal;
                dt.Rows[TblRowIndex]["inpercnt"] = incpercnt;
                dt.Rows[TblRowIndex]["incamt"] = incamt;
                dt.Rows[TblRowIndex]["finincamt"] = incamt;
                dt.Rows[TblRowIndex]["revisesal"] = revisesal;


            }
            Session["tblAnnInc"] = dt;

        }
        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblAnnInc"];
            this.gvAnnIncre.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvAnnIncre.DataSource = dt;
            this.gvAnnIncre.DataBind();
            this.FooterCal();
        }
        protected void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblAnnInc"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFpresal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(grossal)", "")) ? 0.00 : dt.Compute("sum(grossal)", ""))).ToString("#,##0;(#,##0);");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFincamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(incamt)", "")) ? 0.00 : dt.Compute("sum(incamt)", ""))).ToString("#,##0;(#,##0);");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFfinincamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(finincamt)", "")) ? 0.00 : dt.Compute("sum(finincamt)", ""))).ToString("#,##0;(#,##0);");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFRevise")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(revisesal)", "")) ? 0.00 : dt.Compute("sum(revisesal)", ""))).ToString("#,##0;(#,##0);");

            Session["Report1"] = gvAnnIncre;
            ((HyperLink)this.gvAnnIncre.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            this.LoadGrid();

        }

        protected void gvAnnIncre_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvAnnIncre.PageIndex = e.NewPageIndex;
            //this.lbtnTotal_Click(null,null);
            this.LoadGrid();
        }
        protected void lnkFiUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            this.SaveValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string cutdate = this.GetStdDate(this.txtdate.Text);
            try
            {
                string comcod = this.GetComeCode();
                DataTable dt = (DataTable)Session["tblAnnInc"];
                if (ddlPrevIncList.Items.Count == 0)
                    this.GetIncrementNo();
                string incno = this.lblCurIncrNo.Text.ToString().Trim().Substring(0, 3) + cutdate.Substring(7, 4) + this.lblCurIncrNo.Text.ToString().Trim().Substring(3, 2) + this.txtCurIncrNo.Text.ToString().Trim();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string grossal = dt.Rows[i]["grossal"].ToString();
                    string incpercnt = dt.Rows[i]["inpercnt"].ToString();
                    string incamt = dt.Rows[i]["incamt"].ToString();
                    double finincamt = Convert.ToDouble(dt.Rows[i]["finincamt"].ToString());
                    double revisesal = Convert.ToDouble(dt.Rows[i]["revisesal"].ToString());
                    //  string finincamt = dt.Rows[i]["finincamt"].ToString();
                    if (finincamt != 0)
                    {
                        bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "INSUPDATE_INCREMENT", incno, empid,
                                                        cutdate, grossal, incpercnt, incamt, finincamt.ToString(), revisesal.ToString(), userid, postDat, trmid, sessionid, "", "", "");
                    }
                }
             ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        //protected void lblPutPreDate_Click(object sender, EventArgs e)
        //{
        //    DataTable dt1 = (DataTable)Session["tblAnnInc"];
        //    for (int i = 0; i < dt1.Rows.Count; i++)
        //    {
        //        dt1.Rows[i]["linday"] =Convert.ToDateTime(this.txtPreDate.Text).ToString("dd-MMM-yyyy");
        //    }
        //    Session["tblAnnInc"] = dt1;
        //    this.LoadGrid();
        //}
        //protected void lblPutCurDate_Click(object sender, EventArgs e)
        //{
        //    DataTable dt1 = (DataTable)Session["tblAnnInc"];
        //    for (int i = 0; i < dt1.Rows.Count; i++)
        //    {
        //        dt1.Rows[i]["linday"] = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy");

        //    }
        //    Session["tblAnnInc"] = dt1;
        //    this.LoadGrid();
        //    //this.Datedipcount();
        //}
        //private void Datedipcount()
        //{
        //    DataTable dt1 = (DataTable)Session["tblAnnInc"];
        //    int TblRowIndex;
        //    for (int i = 0; i < this.gvAnnIncre.Rows.Count; i++)
        //    {
        //        TblRowIndex = (gvAnnIncre.PageIndex) * gvAnnIncre.PageSize + i;
        //        DateTime dtfrom = Convert.ToDateTime(((Label)this.gvAnnIncre.Rows[i].FindControl("lgvpredate")).Text.Trim());
        //        DateTime dtto = Convert.ToDateTime(((TextBox)this.gvAnnIncre.Rows[i].FindControl("lgvcurdate")).Text.Trim());
        //        int noofday=ASTUtility.Datediffday(dtto, dtfrom);
        //        dt1.Rows[TblRowIndex]["lgvdaydiff"] = noofday;
        //    }
        //    Session["tblAnnInc"] = dt1;
        //    this.LoadGrid();      
        //}

      
        protected void imgbtnPreList_Click(object sender, EventArgs e)
        {
            this.GetPreviousList();
        }
       
        protected void lbtnPutSameValue_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblAnnInc"];
            double incpercnt = Convert.ToDouble(dt.Rows[0]["inpercnt"]);
            for (int i = 1; i < dt.Rows.Count; i++)
            {

                double grossal = Convert.ToDouble(dt.Rows[i]["grossal"]);
                dt.Rows[i]["inpercnt"] = incpercnt;
                dt.Rows[i]["incamt"] = grossal * 0.01 * incpercnt;
                dt.Rows[i]["finincamt"] = grossal * 0.01 * incpercnt;
            }
            Session["tblAnnInc"] = dt;
            this.LoadGrid();

        }

        protected void lbtnRound_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblAnnInc"];
            int TblRowIndex;
            for (int i = 0; i < this.gvAnnIncre.Rows.Count; i++)
            {

                double Finincamt = Convert.ToDouble("0" + ((TextBox)this.gvAnnIncre.Rows[i].FindControl("lgvfinamount")).Text.Trim());
                TblRowIndex = (gvAnnIncre.PageIndex) * gvAnnIncre.PageSize + i;
                dt.Rows[TblRowIndex]["finincamt"] = Finincamt;
            }
            Session["tblAnnInc"] = dt;
            this.LoadGrid();


        }

        protected void btnIncreEmp_Click(object sender, EventArgs e)
        {

            int rownum = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tblAnnInc"];
            //string incno = this.lblCurIncrNo.Text.ToString().Trim().Substring(0, 3) + cutdate.Substring(7, 4) + this.lblCurIncrNo.Text.ToString().Trim().Substring(3, 2) + this.txtCurIncrNo.Text.ToString().Trim();

           // this.txtdate.Text
            string incno = this.lblCurIncrNo.Text.Trim().Substring(0, 3) + ASTUtility.Right((this.txtdate.Text.Trim()), 4) + this.lblCurIncrNo.Text.Trim().Substring(3, 2) + this.txtCurIncrNo.Text.Trim();     
            string empid = ((Label)this.gvAnnIncre.Rows[rownum].FindControl("lgvEmpId")).Text.Trim();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "INCREMENTEMPLOYEEDELETE",
                       incno, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                dt.Rows[rownum].Delete();
            }

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Delete Fail !!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }




            DataView dv = dt.DefaultView;
            Session.Remove("tblAnnInc");
            Session["tblAnnInc"] = dv.ToTable();
            this.LoadGrid();
        }

        protected void linkAddEmp_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string DeptCode = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string SecCode = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string empID = ((this.ddlEmployee.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlEmployee.SelectedValue.ToString()) + "%";

            string txtDate = this.GetStdDate(this.txtdate.Text);
            DataSet ds2;
            if (ddlPrevIncList.Items.Count == 0)
            {
                this.GetIncreNo();
                string calltype = this.CompanyCalltype();

                ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", calltype, Company, DeptCode, SecCode, txtDate, empID, "", "", "", "");
            }
            else
            {

                string preincreno = this.ddlPrevIncList.SelectedValue.ToString();
                ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETPREINCREMENT", preincreno, txtDate, "", "", "", "", "", "", "");
        

            }
            string empid = ds2.Tables[0].Rows[0]["empid"].ToString();
            DataTable dt = (DataTable)Session["tblAnnInc"];
            DataRow[] projectrow2 = dt.Select("empid = '" + empid + "'");
            if (projectrow2.Length > 0)
            {
                return;

            }
       
            DataRow drforgrid = dt.NewRow();
            drforgrid["comcod"] = ds2.Tables[0].Rows[0]["comcod"].ToString();
            drforgrid["companycod"] = ds2.Tables[0].Rows[0]["companycod"].ToString();
            drforgrid["deptcode"] = ds2.Tables[0].Rows[0]["deptcode"].ToString();
            drforgrid["seccode"] = ds2.Tables[0].Rows[0]["seccode"].ToString();
            drforgrid["desigid"] = ds2.Tables[0].Rows[0]["desigid"].ToString();

            drforgrid["empid"] = ds2.Tables[0].Rows[0]["empid"].ToString();
            drforgrid["idcardno"] = ds2.Tables[0].Rows[0]["idcardno"].ToString();
            drforgrid["empname"] = ds2.Tables[0].Rows[0]["empname"].ToString();
            drforgrid["joindate"] = ds2.Tables[0].Rows[0]["joindate"].ToString();

            drforgrid["confirmdate"] = ds2.Tables[0].Rows[0]["confirmdate"].ToString();
            drforgrid["years"] = ds2.Tables[0].Rows[0]["years"].ToString();
            drforgrid["months"] = ds2.Tables[0].Rows[0]["months"].ToString();
            drforgrid["companyname"] = ds2.Tables[0].Rows[0]["companyname"].ToString();
            drforgrid["deptname"] = ds2.Tables[0].Rows[0]["deptname"].ToString();
            drforgrid["section"] = ds2.Tables[0].Rows[0]["section"].ToString();
            drforgrid["desig"] = ds2.Tables[0].Rows[0]["desig"].ToString();
            drforgrid["grossal"] = ds2.Tables[0].Rows[0]["grossal"].ToString();
            drforgrid["inpercnt"] = ds2.Tables[0].Rows[0]["inpercnt"].ToString();
            drforgrid["incamt"] = ds2.Tables[0].Rows[0]["incamt"].ToString();
            drforgrid["finincamt"] = ds2.Tables[0].Rows[0]["finincamt"].ToString();
            drforgrid["revisesal"] = ds2.Tables[0].Rows[0]["revisesal"].ToString();


            dt.Rows.Add(drforgrid);
            Session["tblAnnInc"] = dt;
            this.LoadGrid();








        }

        protected void tableintosession()
        {
            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("comcod", Type.GetType("System.String"));
            dttemp.Columns.Add("companycod", Type.GetType("System.String"));
            dttemp.Columns.Add("deptcode", Type.GetType("System.String"));
            dttemp.Columns.Add("seccode", Type.GetType("System.String"));
            dttemp.Columns.Add("desigid", Type.GetType("System.String"));
            dttemp.Columns.Add("empid", Type.GetType("System.String"));
            dttemp.Columns.Add("idcardno", Type.GetType("System.String"));
            dttemp.Columns.Add("empname", Type.GetType("System.String"));
            dttemp.Columns.Add("joindate", Type.GetType("System.DateTime"));

            dttemp.Columns.Add("confirmdate", Type.GetType("System.DateTime"));
            dttemp.Columns.Add("years", Type.GetType("System.String"));
            dttemp.Columns.Add("months", Type.GetType("System.String"));
            dttemp.Columns.Add("companyname", Type.GetType("System.String"));
            dttemp.Columns.Add("deptname", Type.GetType("System.String"));
            dttemp.Columns.Add("section", Type.GetType("System.String"));
            dttemp.Columns.Add("desig", Type.GetType("System.String"));
            dttemp.Columns.Add("grossal", Type.GetType("System.Double"));
            dttemp.Columns.Add("inpercnt", Type.GetType("System.Double"));

            dttemp.Columns.Add("incamt", Type.GetType("System.Double"));
            dttemp.Columns.Add("finincamt", Type.GetType("System.Double"));
            dttemp.Columns.Add("revisesal", Type.GetType("System.Double"));
            Session["tblAnnInc"] = dttemp;
        }
    }
}