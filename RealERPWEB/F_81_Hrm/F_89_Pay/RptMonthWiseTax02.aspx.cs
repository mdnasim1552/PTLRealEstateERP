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
namespace RealERPWEB.F_81_Hrm.F_89_Pay
{
    public partial class RptMonthWiseTax02 : System.Web.UI.Page
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
                //string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtfromdate.Text = "01" + date.Substring(2);
                //this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01-Jan-" + date.Substring(7);
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text.Trim()).AddMonths(12).AddDays(-1).ToString("dd-MMM-yyyy");
                // this.ShowView();
                this.GetCompanyName();
               // ((Label)this.Master.FindControl("lblTitle")).Text = "Month Wise Tax Report";

            }

        }
   
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Create an event handler for the master page's contentCallEvent event
        ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

        //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

    }

    private void GetCompanyName()
    {
        Hashtable hst = (Hashtable)Session["tblLogin"];
        string userid = hst["usrid"].ToString();
        string comcod = hst["comcod"].ToString();
        string txtCompany = "%%";
        DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
        this.ddlCompanyAgg.DataTextField = "actdesc";
        this.ddlCompanyAgg.DataValueField = "actcode";
        this.ddlCompanyAgg.DataSource = ds5.Tables[0];
        this.ddlCompanyAgg.DataBind();
        Session["tblcompany"] = ds5.Tables[0];
        this.GetDepartment();
        // this.ddlCompanyAgg_SelectedIndexChanged(null, null);
    }
    private void GetDepartment()
    {
        string comcod = this.GetComeCode();
        int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyAgg.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
        string Company = this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, hrcomln) + "%";
        //string Company = ((this.ddlCompanyAgg.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, 2)) + "%";
        string txtSProject = this.ddldepartmentagg.Text.Trim() + "%";
        DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEPTNAME", Company, txtSProject, "", "", "", "", "", "", "");
        this.ddldepartmentagg.DataTextField = "deptdesc";
        this.ddldepartmentagg.DataValueField = "deptcode";
        this.ddldepartmentagg.DataSource = ds4.Tables[0];
        this.ddldepartmentagg.DataBind();
        this.GetProjectName();
    }

    private void GetProjectName()
    {
        string comcod = this.GetComeCode();
        string deptcode = ((this.ddldepartmentagg.SelectedValue == "000000000000") ? "94" : this.ddldepartmentagg.SelectedValue.ToString().Substring(0, 9)) + "%";
        string txtSProject = "%";
        DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAME", deptcode, txtSProject, "", "", "", "", "", "", "");
        this.ddlProjectName.DataTextField = "actdesc";
        this.ddlProjectName.DataValueField = "actcode";
        this.ddlProjectName.DataSource = ds4.Tables[0];
        this.ddlProjectName.DataBind();
        this.GetEmpName();
    }
    private string GetComeCode()
    {
        Hashtable hst = (Hashtable)Session["tblLogin"];
        return (hst["comcod"].ToString());

    }

    private void GetEmpName()
    {
        string comcod = this.GetComeCode();
        //string ProjectCode = (this.ddlEmployee.Text.Trim().Length > 0) ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
        string ProjectCode =  this.ddlProjectName.SelectedValue.ToString() + "%";
        string txtSProject = "%";

        DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "GETEMPLOYEE", ProjectCode, txtSProject, "", "", "", "", "", "", "");
        this.ddlEmployee.DataTextField = "empname";
        this.ddlEmployee.DataValueField = "empid";
        this.ddlEmployee.DataSource = ds5.Tables[0];
        this.ddlEmployee.DataBind();
        ViewState["tblemp"] = ds5.Tables[0];
        //this.GetComASecSelected();
    }

    //private void GetComASecSelected()
    //{
    //    string empid = this.ddlEmployee.SelectedValue.ToString();
    //    DataTable dt = (DataTable)ViewState["tblemp"];
    //    DataRow[] dr = dt.Select("empid = '" + empid + "'");
    //    if (dr.Length > 0)
    //    {
    //        this.ddlCompanyAgg.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["companycode"].ToString();
    //        this.ddldepartmentagg.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["deptcode"].ToString();
    //        this.ddlProjectName.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["refno"].ToString();
    //    }

    //}

    protected void ibtnFindCompanyAgg_Click(object sender, EventArgs e)
    {
        this.GetCompanyName();
    }

    protected void ddlCompanyAgg_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetDepartment();
        //this.ddlProjectName_SelectedIndexChanged(null,null);
    }

    protected void lbtndeptagg_Click(object sender, EventArgs e)
    {
        this.GetDepartment();
    }
    protected void ddldepartmentagg_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetProjectName();

    }
    protected void ibtnFindProject_Click(object sender, EventArgs e)
    {
        this.ddlProjectName_SelectedIndexChanged(null, null);
        //this.ddlProjectName_SelectedIndexChanged(null, null);
        this.GetProjectName();
    }

    protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetEmpName();

    }
    protected void ibtnEmpListAllinfo_Click(object sender, EventArgs e)
    {
        this.GetEmpName();
    }
    //protected void ddlEmpNameAllInfo_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    this.GetComASecSelected();
    //}

    private string GetCompCode()
    {
        Hashtable hst = (Hashtable)Session["tblLogin"];
        return (hst["comcod"].ToString());

    }

    protected void lbtnOk_Click(object sender, EventArgs e)
    {
        Session.Remove("tbltax");
        string comcod = this.GetCompCode();
        ((Label)this.Master.FindControl("lblmsg")).Text = "";
        //int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfromdate.Text.Trim()));
        //if (mon > 12)
        //{

        //    string msg = "Month Less Than Equal Twelve";
        //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

        //    return;
        //}

        int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyAgg.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
        string CompanyName = this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, hrcomln) + "%";
        //  string CompanyName = ((this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, 2) == "00") ? "" : this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, 2)) + "%";
        string projectcode = ((this.ddldepartmentagg.SelectedValue.ToString() == "000000000000") ? "" : this.ddldepartmentagg.SelectedValue.ToString().Substring(0, 9)) + "%";
        string section = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString()) + "%";
        string frmdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
        string todate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
        string empid = ((this.ddlEmployee.SelectedValue.ToString() == "000000000000") ? "" : this.ddlEmployee.SelectedValue.ToString()) + "%";


        DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "SHOWEMPMONTHWISETAX02", CompanyName, projectcode, section, frmdate, todate, empid, "", "", "");
        if (ds1 == null)
        {
            this.gvsalary.DataSource = null;
            this.gvsalary.DataBind();
            return;
        }

            Session["tbltax"] = HiddenSameData(ds1.Tables[0]);
          

           // Session["tbltax"] = ds1.Tables[0];
            this.Data_Bind();
    }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
           string empid = dt1.Rows[0]["empid"].ToString();
           string idcardno = dt1.Rows[0]["idcardno"].ToString();
           string grp = dt1.Rows[0]["grp"].ToString();
           
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if ((dt1.Rows[j]["empid"].ToString() == empid) || (dt1.Rows[j]["grp"].ToString() == "B"))
                {
                    empid = dt1.Rows[j]["empid"].ToString();
                    //idcardno = dt1.Rows[j]["idcardno"].ToString();
                    //grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["empname"] = "";
                    dt1.Rows[j]["idcardno"] = "";
                    //dt1.Rows[j]["grp"] = "";
                }

                else
                {
                    empid = dt1.Rows[j]["empid"].ToString();
                    idcardno = dt1.Rows[j]["idcardno"].ToString();
                    //grp = dt1.Rows[j]["grp"].ToString();
                }

            }

            return dt1;
        }


    private void Data_Bind()
    {

        //DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
        //DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
        //for (int i = 4; i < 16; i++)
        //{
        //    if (datefrm > dateto)
        //        break;

        //    this.gvsalary.Columns[i].HeaderText = datefrm.ToString("MMM yy");
        //    datefrm = datefrm.AddMonths(1);

        //}

        this.gvsalary.DataSource = (DataTable)Session["tbltax"];
        this.gvsalary.DataBind();
        this.FooterCalCulation();

    }


    private void FooterCalCulation()
    {

        DataTable dt = (DataTable)Session["tbltax"];
        if (dt.Rows.Count == 0)
        {
            return;
        }

       
        Session["Report1"] = gvsalary;
        ((HyperLink)this.gvsalary.HeaderRow.FindControl("hlbtntbCdataExel11")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

    }

    protected void lbtnPrint_Click(object sender, EventArgs e)
    {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();


            switch (comcod)
            {
                case "3101":
                case "3368":
                    this.PrintTaxReportFinlay();
                    break;

                default:
                    this.PrintTaxReportGen();
                    break;
            }



      

        //ReportDocument rptpf = new RealERPRPT.R_81_Hrm.R_89_Pay.RptMonthWiseTax();


        //TextObject rptCname = rptpf.ReportDefinition.ReportObjects["CompName"] as TextObject;
        //rptCname.Text = comnam;

        //TextObject rptTxtHead = rptpf.ReportDefinition.ReportObjects["txtHead"] as TextObject;
        //rptTxtHead.Text = "Month Wise Tax Sheet";


        //TextObject rptdate = rptpf.ReportDefinition.ReportObjects["txtDate"] as TextObject;
        //rptdate.Text = "(From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";

        ////TextObject txtempname = rptpf.ReportDefinition.ReportObjects["txtempname"] as TextObject;
        ////txtempname.Text = "Empoyee Name : "+ this.ddlEmployee.SelectedItem.Text.Substring(7);

        //DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
        //DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
        //for (int i = 1; i <= 12; i++)
        //{
        //    if (datefrm > dateto)
        //        break;
        //    TextObject rpttxth = rptpf.ReportDefinition.ReportObjects["txtMonth" + i.ToString()] as TextObject;
        //    rpttxth.Text = datefrm.ToString("MMM yy");
        //    datefrm = datefrm.AddMonths(1);

        //}

        //TextObject txtuserinfo = rptpf.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

        //rptpf.SetDataSource(dt);
        //Session["Report1"] = rptpf;

        //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
        //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

    }

        private void PrintTaxReportGen()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbltax"];

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.RptMonthlySalaryTax>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptMonthWiseTax", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Month Wise Tax Sheet"));
            DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            for (int i = 1; i <= 12; i++)
            {
                if (datefrm > dateto)
                    break;
                Rpt1.SetParameters(new ReportParameter("txtMonth" + i.ToString(), datefrm.ToString("MMM yy")));
                datefrm = datefrm.AddMonths(1);
            }
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintTaxReportFinlay()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;


            DataTable dt = (DataTable)Session["tbltax"];

            EnumerableRowCollection<DataRow> query = from tax in dt.AsEnumerable()
                                                     where tax.Field<string>("grp") != "B"
                                                     orderby tax.Field<string>("monthid"), tax.Field<string>("idcardno")
                                                     select tax;

            DataView view = query.AsDataView();

            DataTable dt2 = view.ToTable();

            var list = dt2.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.RptMonthlySalaryTaxFinlay>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptMonthWiseTaxFinlay", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Tax Report"));
            DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

            //for (int i = 1; i <= 12; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    Rpt1.SetParameters(new ReportParameter("txtMonth" + i.ToString(), datefrm.ToString("MMM yy")));
            //    datefrm = datefrm.AddMonths(1);
            //}
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void imgbtnEmployee_Click(object sender, EventArgs e)
    {
        this.GetEmpName();
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetEmpName();
    }

        protected void gvsalary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvmonthid = (Label)e.Row.FindControl("lgvmonthid");
                Label lgvtax = (Label)e.Row.FindControl("lgvtax");

                
                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();

                if (grp == "")
                {
                    return;
                }




                if (grp == "B")
                {

                    lgvtax.Font.Bold = true;
                    lgvmonthid.Font.Bold = true;
                    lgvtax.Font.Bold = true;

                    lgvmonthid.Font.Size = 12;
                    lgvtax.Font.Size = 12;

                    e.Row.BackColor = System.Drawing.Color.Pink;
                    ////e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                    //lgvmonthid.Attributes["style"] = "font-weight:bold; color:green; ";
                    //lgvfaccashamt.Attributes["style"] = "font-weight:bold; color:black;";
                    //lgvfbanksalary.Attributes["style"] = "font-weight:bold; color:black;";




                    //lgvNagad.Style.Add("text-align", "left");
                    lgvmonthid.Style.Add("text-align", "right");

                }






            }  

               
        }
    }
}
