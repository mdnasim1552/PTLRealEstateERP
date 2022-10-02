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
    public partial class RptIncrement : System.Web.UI.Page
    {
        Common compUtility = new Common();

        ProcessAccess HRData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();

                //this.txtfrmDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtfrmDate.Text = "01" + this.txtfrmDate.Text.Trim().Substring(2);
                //this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                //// this.txtdate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                GetDate();
                this.GetCompany();
                this.GetIncreNo();
                this.RadioButtonList1.SelectedIndex = 0;
                RadioButtonList1_SelectedIndexChanged(null, null);
               
            }

        }
        private void GetDate()
        {
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
                return;

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "26" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
            this.txtfrmDate.Text = startdate + this.txtfrmDate.Text.Trim().Substring(2);
            this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");


            //string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            //string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            //this.txtFdate.Text = startdate + date.Substring(2);
            //this.txtTdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
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
            if (this.lnkbtnShow.Text == "New")
                return;
            string comcod = this.GetComeCode();
            string txtCompany = "%" + this.ddlCompany.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETCOMPANYNAME", txtCompany, "", "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            this.ddlCompany_SelectedIndexChanged(null, null);
            ds1.Dispose();
        }
        private void GetDeptName()
        {
            if (this.lnkbtnShow.Text == "New")
                return;
            string comcod = this.GetComeCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtCompany = "%" + this.ddlDept.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETPROJECTNAME", Company, txtCompany, "", "", "", "", "", "", "");
            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = ds1.Tables[0];
            this.ddlDept.DataBind();
            this.ddlDept_SelectedIndexChanged(null, null);
            ds1.Dispose();
        }
        private void GetSection()
        {

            if (this.lnkbtnShow.Text == "New")
                return;
            string comcod = this.GetComeCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string DeptName = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 8)) + "%";
            string SrchSection = "%" + this.ddlSection.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETSECTION", Company, DeptName, SrchSection, "", "", "", "", "", "");
            this.ddlSection.DataTextField = "section";
            this.ddlSection.DataValueField = "seccode";
            this.ddlSection.DataSource = ds1.Tables[0];
            this.ddlSection.DataBind();
            ds1.Dispose();


        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptName();
        }




        private void GetIncreNo()
        {
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    string date = this.GetStdDate(this.txtdate.Text);
            //    DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_ANNUAL_INCREMENT", "GETINCREMENTNO", date, "", "", "", "", "", "", "", "");
            //    if (ds3 == null)
            //        return;
            //    DataTable dt1 = ds3.Tables[0];
            //    this.txtdate.Text = Convert.ToDateTime(ds3.Tables[0].Rows[0]["maxincdt"].ToString().Trim()).ToString("dd.MM.yyyy");
            //    this.lblCurIncrNo.Text = ds3.Tables[0].Rows[0]["maxincno1"].ToString().Substring(0, 5);
            //    this.txtCurIncrNo.Text = ds3.Tables[0].Rows[0]["maxincno1"].ToString().Substring(6);
        }


        protected void GetIncrementNo()
        {



        }


        protected string GetStdDate(string Date1)
        {
            //Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            //string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            //Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }



        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            //if (this.lnkbtnShow.Text == "New")
            //{



            //    this.lblCompany.Visible = false;
            //    this.lblDept.Visible = false;
            //    this.lblSection.Visible = false;
            //    this.ddlCompany.Visible = true;
            //    this.ddlDept.Visible = true;
            //    this.ddlSection.Visible = true;
            //    this.lnkbtnShow.Text = "Ok";

            //    return;
            //}


            //this.lblPreVious.Visible = false;
            //this.txtSrchPreviousList.Visible = false;
            //this.imgbtnPreList.Visible = false;
            //this.ddlPrevIncList.Visible = false;
            //this.lnkbtnShow.Text = "New";
            //this.lblCompany.Text = this.ddlCompany.SelectedItem.Text.Trim();
            //this.lblDept.Text = this.ddlDept.SelectedItem.Text.Trim();
            //this.lblSection.Text = this.ddlSection.SelectedItem.Text.Trim();
            //this.lblCompany.Visible = true;
            //this.lblDept.Visible = true;
            //this.lblSection.Visible = true;
            //this.ddlCompany.Visible = false;
            //this.ddlDept.Visible = false;
            //this.ddlSection.Visible = false;
            this.ShowInc();
        }

        private void ShowInc()
        {
            Session.Remove("tblAnnInc");
            string comcod = this.GetComeCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string DeptCode = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string SecCode = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_ANNUAL_INCREMENT", "GETPREINCREMENTSTATUS", frmdate, todate, Company, DeptCode, SecCode, "", "", "", "");

            if (ds2 == null)
            {
                this.gvAnnIncre.DataSource = null;
                this.gvAnnIncre.DataBind();
                return;
            }

            DataTable dt1 = new DataTable();
            DataView view = new DataView();

            view.Table = ds2.Tables[0];
            string type = this.RadioButtonList1.SelectedValue.ToString();

            if (type == "approved")
            {
                view.RowFilter = "ack='OK'";
                dt1 = view.ToTable();
            }
            else
            {
                view.RowFilter = "ack=''";
                dt1 = view.ToTable();
            }
            DataTable dt = HiddenSameData(dt1);



            Session["tblAnnInc"] = dt;
            this.LoadGrid();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string deptcode = dt1.Rows[0]["deptcode"].ToString();
            string seccode = dt1.Rows[0]["seccode"].ToString();
            string incrno = dt1.Rows[0]["incrno"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["deptcode"].ToString() == deptcode && dt1.Rows[j]["seccode"].ToString() == seccode && dt1.Rows[j]["incrno"].ToString() == incrno)
                {

                    dt1.Rows[j]["deptname"] = "";
                    dt1.Rows[j]["section"] = "";
                    dt1.Rows[j]["incrno1"] = "";
                    dt1.Rows[j]["incrdate1"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                        dt1.Rows[j]["deptname"] = "";
                    if (dt1.Rows[j]["seccode"].ToString() == seccode)
                        dt1.Rows[j]["section"] = "";
                    if (dt1.Rows[j]["incrno"].ToString() == incrno)
                    {
                        dt1.Rows[j]["incrdate1"] = "";
                        dt1.Rows[j]["incrno1"] = "";

                    }


                }

                deptcode = dt1.Rows[j]["deptcode"].ToString();
                seccode = dt1.Rows[j]["seccode"].ToString();
                incrno = dt1.Rows[j]["incrno"].ToString();

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
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_93_AnnInc.AnnIncReport.AnnualIncrementStatus>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_93_AnnInc.RptIncrementStatus", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", " Increment Information Status"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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

            this.LoadGrid();
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
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFGross")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(grossal)", "")) ? 0.00 : dt.Compute("sum(grossal)", ""))).ToString("#,##0;(#,##0);");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFicreamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(incamt)", "")) ? 0.00 : dt.Compute("sum(incamt)", ""))).ToString("#,##0;(#,##0);");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFtogross")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tosalary)", "")) ? 0.00 : dt.Compute("sum(tosalary)", ""))).ToString("#,##0;(#,##0);");


        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {

            //this.SaveValue();
            this.LoadGrid();

        }

        protected void gvAnnIncre_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //this.SaveValue();
            this.gvAnnIncre.PageIndex = e.NewPageIndex;
            //this.lbtnTotal_Click(null,null);
            this.LoadGrid();
        }


        protected void imgbtnSectionSrch_Click(object sender, EventArgs e)
        {
            //this.GetSection();
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSection();
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

        protected void lnkFiUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSection();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowInc();
        }
    }
}

