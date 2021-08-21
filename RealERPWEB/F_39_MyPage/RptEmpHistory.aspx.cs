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
//using  RealERPRPT;
using RealEntity;
namespace RealERPWEB.F_39_MyPage
{

    public partial class RptEmpHistory : System.Web.UI.Page
    {
        public UserManagerKPI objUser = new UserManagerKPI();
        UserManager userManager = new UserManager();
        ProcessAccess KpiData = new ProcessAccess();
        public static string TString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Month Wise Evaluation";
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.txtfrmdate.Text = Convert.ToDateTime("01-Jan-" + date.Substring(7)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                this.lbluseid.Text = (Request.QueryString["Type"] == "IndEmp") ? hst["usrid"].ToString() : "";
                //  this.GetEmpList();


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkprint_Click);

        }
        private void GetEmpList()
        {

            //-----------Get Person List ---------------//

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Getcomcod();
            string srchEmp = "%" + this.txtSrchSalesTeam.Text.Trim() + "%";
            string userid = (this.Request.QueryString["Type"] == "IndEmp") ? hst["usrid"].ToString() : "";
            string deptcode = hst["deptcode"].ToString();
            List<RealEntity.C_47_Kpi.EClassEmpCode> lst3 = new List<RealEntity.C_47_Kpi.EClassEmpCode>();
            lst3 = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", srchEmp, userid, deptcode);

            this.ddlEmpid.DataTextField = "empname";
            this.ddlEmpid.DataValueField = "empid";
            this.ddlEmpid.DataSource = lst3;
            this.ddlEmpid.DataBind();
            Session["tblemployee"] = lst3;
        }



        //[System.Web.Services.WebMethod(EnableSession=true)]

        //public  static List<RealEntity.C_47_Kpi.EClassEmpCode> GetEmpList02(string srchteam) 
        //{

        //    List<RealEntity.C_47_Kpi.EClassEmpCode> lst = new List<RealEntity.C_47_Kpi.EClassEmpCode>();

        //    //Hashtable hst = (Hashtable)Session["tblLogin"];
        //    //string comcod = Getcomcod();
        //    //string userid = (Request.QueryString["Type"] == "Client") ? hst["usrid"].ToString() : "";
        //    string userid = "";

        //    lst = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", srchteam, userid);
        //    return lst;


        //}


        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void imgSearchSalesTeam_Click(object sender, EventArgs e)
        {
            this.GetEmpList();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            string empid = this.ddlEmpid.SelectedValue.ToString();
            string frmdate = this.txtfrmdate.Text.Trim();
            string todate = this.txttodate.Text.Trim();

            List<RealEntity.C_47_Kpi.EClassEmpHistory> lst = new List<RealEntity.C_47_Kpi.EClassEmpHistory>();
            lst = userManager.GetEmpHistory(empid, frmdate, todate);
            Session["tblemphis"] = lst;
            this.Data_Bind();

        }
        private void Data_Bind()
        {
            List<RealEntity.C_47_Kpi.EClassEmpHistory> lst = (List<RealEntity.C_47_Kpi.EClassEmpHistory>)Session["tblemphis"];
            this.gvEmpHis.DataSource = lst;
            this.gvEmpHis.DataBind();







        }
        protected void gvResMonth_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void lnkprint_Click(object sender, EventArgs e)
        {
            string empid = this.ddlEmpid.SelectedValue.ToString();
            List<RealEntity.C_47_Kpi.EClassEmpHistory> lst = (List<RealEntity.C_47_Kpi.EClassEmpHistory>)Session["tblemphis"];
            List<RealEntity.C_47_Kpi.EClassEmpCode> lstemp = (List<RealEntity.C_47_Kpi.EClassEmpCode>)Session["tblemployee"];
            List<RealEntity.C_47_Kpi.EClassEmpCode> lstemp1 = lstemp.FindAll((p => p.empid == empid));

            //lst3 = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", srchEmp, userid);

            //this.ddlEmpid.DataTextField = "empname";
            //this.ddlEmpid.DataValueField = "empid";
            //this.ddlEmpid.DataSource = lst3;
            //this.ddlEmpid.DataBind();
            //Session["tblemployee"] = lst3;




            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string deptname = hst["deptname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptAppMonitor = new RealERPRPT.R_39_MyPage.rptIndEmpHistoryLand();
            TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            CompName.Text = comnam;
            TextObject txtempname = rptAppMonitor.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            txtempname.Text = "Name: " + lstemp1[0].empname1;


            TextObject txtdeptname = rptAppMonitor.ReportDefinition.ReportObjects["txtdeptname"] as TextObject;
            txtdeptname.Text = "Department: " + deptname;


            //TextObject txtDesignation = rptAppMonitor.ReportDefinition.ReportObjects["txtdesignation"] as TextObject;
            //txtDesignation.Text = "Designation: " + (((DataTable)Session["tblsaleteam"]).Select("teamcode='" + Salesteamcode + "'"))[0]["designtion"];

            TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = " (From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";

            TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptAppMonitor.SetDataSource(lst);

            Session["Report1"] = rptAppMonitor;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        protected void gvPrjInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkgvcomments = (HyperLink)e.Row.FindControl("hlnkgvcomments");
                Label deloadv = (Label)e.Row.FindControl("lblgvdelay");
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();
                string deloadvsign = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deloadvsign")).ToString();
                if (deloadvsign == "delay")
                {
                    deloadv.Style.Add("color", "red");
                }

            }
        }
        protected void gvEmpHis_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label deloadv = (Label)e.Row.FindControl("lblgvdelay");

                string deloadvsign = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deloadvsign")).ToString();
                if (deloadvsign == "delay")
                {
                    deloadv.Style.Add("color", "red");
                }

            }

        }
    }
}