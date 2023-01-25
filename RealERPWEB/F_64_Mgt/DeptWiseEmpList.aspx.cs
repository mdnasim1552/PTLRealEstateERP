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
namespace RealERPWEB.F_64_Mgt
{

    public partial class DeptWiseEmpList : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                //----------------udate-20150120---------
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "department link info";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

                this.GetDeptName();
                this.GetYearMonth();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void GetYearMonth()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];
            this.ddlyearmon.DataBind();
            this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            ds1.Dispose();


        }
        private void GetDeptName()
        {
            if (this.lbtnOk.Text == "New")
                return;

            string comcod = this.GetCompCode();


            string mSrchTxt = this.txtsrchDept.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_MGT", "GETDEPTNAME", mSrchTxt, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddldeptlist.DataTextField = "deptdesc";
            this.ddldeptlist.DataValueField = "deptcode";
            this.ddldeptlist.DataSource = ds1.Tables[0];
            this.ddldeptlist.DataBind();
            ds1.Dispose();
        }

        //protected void GetHRDeptName()
        //{

        //    string comcod = this.GetCompCode();
        //    string FindProject ="%"+ this.txtsrchhrdept.Text.Trim() + "%";
        //    DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETHRDEPTNAME", FindProject, "", "", "", "", "", "", "", "");
        //    if (ds1 == null)
        //        return;

        //    this.ddlhrdept.DataTextField = "actdesc";
        //    this.ddlhrdept.DataValueField = "actcode";
        //    this.ddlhrdept.DataSource = ds1.Tables[0];
        //    this.ddlhrdept.DataBind();
        //    ds1.Dispose();



        //}

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                this.ddldeptlist.Enabled = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.gvDepWiseEmp.DataSource = null;
                this.gvDepWiseEmp.DataBind();
                this.lbtnOk.Text = "Ok";
                this.lblPage.Visible = false;
                this.ddlpagesize.Visible = false;
                return;
            }



            this.ddldeptlist.Enabled = false;
            this.lbtnOk.Text = "New";
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.GetInfo();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //ReportClass rptstk = null;
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //if (this.ddlSurveyType.SelectedValue.ToString().Trim() == "1")
            //{
            //    DataTable dt = (DataTable)Session["tblMSR"];
            //   RealERPRPT.R_03_Pro.RptPurMktSurvey rptstk1 = new RealERPRPT.R_03_Pro.RptPurMktSurvey() ;
            //    rptstk1.SetDataSource((DataTable)Session["tblMSR"]);
            //    Session["Report1"] = rptstk1;
            //    rptstk = rptstk1;
            //}
            //else if (this.ddlSurveyType.SelectedValue.ToString().Trim() == "2")
            //{
            //     RealERPRPT.R_03_Pro.RptMktSurveyMatWiseSupList rptstk2 = new RealERPRPT.R_03_Pro.RptMktSurveyMatWiseSupList()  ;
            //    rptstk2.SetDataSource((DataTable)Session["tbPreLink"]);
            //    Session["Report1"] = rptstk2;
            //    rptstk = rptstk2;
            //}
            //else if (this.ddlSurveyType.SelectedValue.ToString().Trim() == "3")
            //{
            //    RealERPRPT.R_03_Pro.RptMktSurveySupWiseMatList rptstk3 = new RealERPRPT.R_03_Pro. RptMktSurveySupWiseMatList();
            //    rptstk3.SetDataSource((DataTable)Session["SuplRes"]);
            //    Session["Report1"] = rptstk3;
            //    rptstk = rptstk3;
            //}


            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text =comnam;
            ////TextObject txtCompanyAddress = rptstk.ReportDefinition.ReportObjects["companyaddress"] as TextObject;
            ////txtCompanyAddress.Text = ConstantInfo.ComAdd;
            //TextObject txtsurveynoname = rptstk.ReportDefinition.ReportObjects["surveynoname"] as TextObject;
            //txtsurveynoname.Text =this.lblCurMSRNo1.Text.Trim()+ this.txtCurMSRNo2.Text.ToString().Trim();
            //TextObject txtadate = rptstk.ReportDefinition.ReportObjects["adate"] as TextObject;
            //txtadate.Text = this.txtApprovalDate.Text.ToString().Trim();
            //TextObject txtnarrationname = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //txtnarrationname.Text = this.txtMSRNarr.Text.ToString().Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = this.Label1.Text;
            //    string eventdesc = "Print Report Survey";
            //    string eventdesc2 = this.lblCurMSRNo1.Text.Trim() + this.txtCurMSRNo2.Text.ToString().Trim(); 
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //this.lblprintstk.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }


        private void GetInfo()
        {

            ViewState.Remove("DepWiseEmp");
            string comcod = this.GetCompCode();
            string deptcode = this.ddldeptlist.SelectedValue.ToString();
            string yearmon = this.ddlyearmon.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_MGT", "SHOWDEPWISEEMP", deptcode, yearmon, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["DepWiseEmp"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();

        }
        protected void Data_Bind()
        {
            DataTable tbl1 = (DataTable)ViewState["DepWiseEmp"];
            this.gvDepWiseEmp.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvDepWiseEmp.DataSource = tbl1;
            this.gvDepWiseEmp.DataBind();

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }


        protected void ImgbtnFindDept_Click(object sender, EventArgs e)
        {
            this.GetDeptName();
        }




        protected void gvDepWiseEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDepWiseEmp.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvDepWiseEmp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("lblgvSetup");
            string monid = this.ddlyearmon.SelectedValue.ToString();
            string empid = ((Label)e.Row.FindControl("lblgvempid")).Text;
            string Deptcode = this.ddldeptlist.SelectedValue.ToString();
            string Month = this.ddlyearmon.SelectedItem.Text;


            Label Empname = (Label)e.Row.FindControl("lblgvEmpname");
            Label Desg = (Label)e.Row.FindControl("lblgvDesg");
            Label Joindat = (Label)e.Row.FindControl("lblgvJoindat");
            string estatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "estatus")).ToString();
            if (estatus != "active")
            {
                Empname.Style.Add("color", "red");
                Desg.Style.Add("color", "red");
                Joindat.Style.Add("color", "red");
            }


            hlink1.NavigateUrl = "OvKpiSetup.aspx?Type=SupInfo&comcod=" + mCOMCOD + "&empid=" + empid + "&monid=" + monid + "&deptcode=" + Deptcode + "&month=" + Month;
        }
        protected void gvDepWiseEmp_DataBound(object sender, EventArgs e)
        {
            //for (int i = gvDepWiseEmp.Rows.Count - 1; i > 0; i--)
            //{
            //    GridViewRow row = gvDepWiseEmp.Rows[i];
            //    GridViewRow previousRow = gvDepWiseEmp.Rows[i - 1];
            //    for (int j = 0; j < row.Cells.Count; j++)
            //    {
            //        if (row.Cells[j].Text == previousRow.Cells[j].Text)
            //        {
            //            if (previousRow.Cells[j].RowSpan == 0)
            //            {
            //                if (row.Cells[j].RowSpan == 0)
            //                {
            //                    previousRow.Cells[j].RowSpan += 2;
            //                }
            //                else
            //                {
            //                    previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
            //                }
            //                row.Cells[j].Visible = false;
            //            }
            //        }
            //    }
            //}
        }
    }
}