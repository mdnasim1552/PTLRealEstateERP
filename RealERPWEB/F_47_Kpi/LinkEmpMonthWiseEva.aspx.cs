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
namespace RealERPWEB.F_47_Kpi
{

    public partial class LinkEmpMonthWiseEva : System.Web.UI.Page
    {
        //private UserManagerKPI objUser = new UserManagerKPI();
        //ProcessAccess KpiData = new ProcessAccess();
        //public static string TString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                ((Label)this.Master.FindControl("lblTitle")).Text = "Month Wise Evaluation";
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //  this.GetEmpList();


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkprint_Click);

        }
        //private void GetEmpList()
        //{

        //    //-----------Get Person List ---------------//

        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = Getcomcod();
        //    string srchEmp = "%" + this.txtSrchSalesTeam.Text.Trim() + "%";
        //    string userid = (this.Request.QueryString["Type"] == "IndEmp") ? hst["usrid"].ToString() : "";
        //    string deptcode = hst["deptcode"].ToString();
        //    List<RealEntity.C_47_Kpi.EClassEmpCode> lst3 = new List<RealEntity.C_47_Kpi.EClassEmpCode>();
        //    lst3 = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", srchEmp, userid,deptcode);

        //    this.ddlEmpid.DataTextField = "empname";
        //    this.ddlEmpid.DataValueField = "empid";
        //    this.ddlEmpid.DataSource = lst3;
        //    this.ddlEmpid.DataBind();
        //    Session["tblemployee"] = lst3;
        //}



        ////[System.Web.Services.WebMethod(EnableSession=true)]

        ////public  static List<RealEntity.C_47_Kpi.EClassEmpCode> GetEmpList02(string srchteam) 
        ////{

        ////    List<RealEntity.C_47_Kpi.EClassEmpCode> lst = new List<RealEntity.C_47_Kpi.EClassEmpCode>();

        ////    //Hashtable hst = (Hashtable)Session["tblLogin"];
        ////    //string comcod = Getcomcod();
        ////    //string userid = (Request.QueryString["Type"] == "Client") ? hst["usrid"].ToString() : "";
        ////    string userid = "";

        ////    lst = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", srchteam, userid);
        ////    return lst;


        ////}


        //private string Getcomcod()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    return (hst["comcod"].ToString());
        //}


        //protected void imgSearchSalesTeam_Click(object sender, EventArgs e)
        //{
        //    this.GetEmpList();
        //}
        //protected void lnkok_Click(object sender, EventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string Empid = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
        //    string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
        //    string todate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");




        //    DataSet ds1 = KpiData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "SHOWDAYWISEDATA", Empid, frmdate, todate);

        //    ViewState["tblmoneva"] = ds1.Tables[0];

        //    this.lblName.Text = "Name: " + ds1.Tables[1].Rows[0]["gdatat"].ToString();
        //    this.lblDesg.Text = "Designation: " + ds1.Tables[1].Rows[2]["gdatat"].ToString();
        //    this.lblJoin.Text = "Date of Joining: " + ds1.Tables[1].Rows[1]["gdatad"].ToString();
        //    this.Data_Bind();

        //}
        //private void Data_Bind() 
        //{








        //}
        //protected void gvResMonth_RowDataBound(object sender, GridViewRowEventArgs e)
        //{

        //}
        protected void lnkprint_Click(object sender, EventArgs e)
        {
            //string empid = this.ddlEmpid.SelectedValue.ToString();
            //List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> lst = (List<RealEntity.C_47_Kpi.EClassEmployeeMonEva>)Session["lst1"];
            //List<RealEntity.C_47_Kpi.EClassEmpCode> lstemp = (List<RealEntity.C_47_Kpi.EClassEmpCode>)Session["tblemployee"];
            //List<RealEntity.C_47_Kpi.EClassEmpCode> lstemp1 = lstemp.FindAll((p => p.empid == empid));

            ////lst3 = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", srchEmp, userid);

            ////this.ddlEmpid.DataTextField = "empname";
            ////this.ddlEmpid.DataValueField = "empid";
            ////this.ddlEmpid.DataSource = lst3;
            ////this.ddlEmpid.DataBind();
            ////Session["tblemployee"] = lst3;




            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //ReportDocument rptAppMonitor = new  RealERPRPT.R_32_Mis.rptMonthWiseEmpEva();
            //TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //CompName.Text = comnam;
            //TextObject txtempname = rptAppMonitor.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            //txtempname.Text = "Name: " + lstemp1[0].empname1 + ", " + "Designation: " + lstemp1[0].desig + ", Salary:       , Benifits:     ";


            ////TextObject txtDesignation = rptAppMonitor.ReportDefinition.ReportObjects["txtdesignation"] as TextObject;
            ////txtDesignation.Text = "Designation: " + (((DataTable)Session["tblsaleteam"]).Select("teamcode='" + Salesteamcode + "'"))[0]["designtion"];

            //TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = " (From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";



            //TextObject CompName02 = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName02"] as TextObject;
            //CompName02.Text = comnam;

            //TextObject txtdate02 = rptAppMonitor.ReportDefinition.ReportObjects["txtdate02"] as TextObject;
            //txtdate02.Text = " (From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";

            //TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptAppMonitor.SetDataSource(lst);

            //Session["Report1"] = rptAppMonitor;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

    }
}