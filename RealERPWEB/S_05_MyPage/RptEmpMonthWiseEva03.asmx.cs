using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Data;
using RealERPLIB;
using RealEntity;
using System.Web.Script.Services;

/// <summary>
/// Summary description for RptEmpMonthWiseEva03
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class RptEmpMonthWiseEva03 : System.Web.Services.WebService
{

    private UserManagerKPI objUser = new UserManagerKPI();
    ProcessAccess KPIData = new ProcessAccess();
    public RptEmpMonthWiseEva03()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }



    [WebMethod(EnableSession = true)]
    public void PrintRptMgt(string stdate, string endDate, string eid)
    {

        List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> lst = (List<RealEntity.C_47_Kpi.EClassEmployeeMonEva>)Session["lst1"];
        List<RealEntity.C_47_Kpi.EClassEmpCode> lstemp = (List<RealEntity.C_47_Kpi.EClassEmpCode>)Session["tblemployee"];
        string empid = eid;
        List<RealEntity.C_47_Kpi.EClassEmpCode> lstemp1 = lstemp.FindAll((p => p.empid == empid));
        string frdate = stdate;
        string todate = endDate;
        Hashtable hst = (Hashtable)Session["tblLogin"];
        string comcod = hst["comcod"].ToString();
        string comnam = hst["comnam"].ToString();
        string compname = hst["compname"].ToString();
        string username = hst["username"].ToString();
        string deptcode = hst["deptcode"].ToString();
        string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");



        //Grade
        DataSet ds = KPIData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "RPTGRADE", "", "", "", "", "");
        if (ds == null) { return; }
        //KPI
        DataSet ds2 = KPIData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "RPTKPI", deptcode, "", "", "", "");
        if (ds2 == null) { return; }


        ReportDocument rptAppMonitor = new RealERPRPT.R_62_Mis.rptMonthWiseEmpEva();
        TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
        CompName.Text = comnam;

        TextObject txtempname = rptAppMonitor.ReportDefinition.ReportObjects["txtempname"] as TextObject;
        txtempname.Text = "Name: " + lstemp1[0].empname1 + ", " + "Designation: " + lstemp1[0].desig + ", Joinning Date: " + Convert.ToDateTime(lstemp1[0].joindate).ToString("dd-MMM-yyyy") + ", Salary:       , Benifits:     ";

        TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
        txtdate.Text = " (From " + frdate.Trim() + " To " + todate.Trim() + ")";


        //TextObject CompName02 = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName02"] as TextObject;
        //CompName02.Text = comnam;

        //TextObject txtdate02 = rptAppMonitor.ReportDefinition.ReportObjects["txtdate02"] as TextObject;
        //txtdate02.Text = " (From " + frdate.Trim() + " To " + todate.Trim() + ")";

        TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //Sub Report
        rptAppMonitor.Subreports["rptGradeSystem.rpt"].SetDataSource(ds.Tables[0]);
        rptAppMonitor.Subreports["rptKPIIndex.rpt"].SetDataSource(ds2.Tables[0]);
        rptAppMonitor.SetDataSource(lst);

        Session["Report1"] = rptAppMonitor;
    }

    [WebMethod(EnableSession = true)]
    public void PrintRpt(string stdate, string endDate, string eid)
    {

        List<RealEntity.C_47_Kpi.EClassEmployeeMonEva02> lst = (List<RealEntity.C_47_Kpi.EClassEmployeeMonEva02>)Session["lst1"];
        List<RealEntity.C_47_Kpi.EClassEmpCode> lstemp = (List<RealEntity.C_47_Kpi.EClassEmpCode>)Session["tblemployee"];
        string empid = eid;
        List<RealEntity.C_47_Kpi.EClassEmpCode> lstemp1 = lstemp.FindAll((p => p.empid == empid));


        string frdate = stdate;
        string todate = endDate;


        Hashtable hst = (Hashtable)Session["tblLogin"];
        string comcod = hst["comcod"].ToString();
        string comnam = hst["comnam"].ToString();
        string compname = hst["compname"].ToString();
        string username = hst["username"].ToString();
        string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //Grade
        DataSet ds = KPIData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "RPTGRADE", "", "", "", "", "");
        if (ds == null) { return; }

        ReportDocument rptAppMonitor = new RealERPRPT.R_39_MyPage.rptMonthWiseEmpEva02();
        TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
        CompName.Text = comnam;
        TextObject txtempname = rptAppMonitor.ReportDefinition.ReportObjects["txtempname"] as TextObject;
        txtempname.Text = "Name: " + lstemp1[0].empname1 + ", " + "Designation: " + lstemp1[0].desig + ", Joinning Date: " + Convert.ToDateTime(lstemp1[0].joindate).ToString("dd-MMM-yyyy") + ", Salary:       , Benifits:     ";

        TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
        txtdate.Text = " (From " + frdate.Trim() + " To " + todate.Trim() + ")";

        TextObject CompName02 = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName02"] as TextObject;
        CompName02.Text = comnam;

        TextObject txtdate02 = rptAppMonitor.ReportDefinition.ReportObjects["txtdate02"] as TextObject;
        txtdate02.Text = " (From " + stdate.Trim() + " To " + endDate.Trim() + ")";

        TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        rptAppMonitor.Subreports["rptGradeSystem.rpt"].SetDataSource(ds.Tables[0]);
        rptAppMonitor.SetDataSource(lst);
        string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        rptAppMonitor.SetParameterValue("ComLogo", ComLogo);

        Session["Report1"] = rptAppMonitor;
    }



    [WebMethod(EnableSession = true)]
    public void PrintRptEmpEva02(string stdate, string endDate, string eid)
    {




        string empid = eid;
        List<RealEntity.C_47_Kpi.EClassEmployeeMonEva02> lst = (List<RealEntity.C_47_Kpi.EClassEmployeeMonEva02>)Session["lst1"];
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
        string deptcode = hst["deptcode"].ToString();
        string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

        //Grade
        DataSet ds = KPIData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "RPTGRADE", "", "", "", "", "");
        if (ds == null) { return; }


        ReportDocument rptAppMonitor = new RealERPRPT.R_39_MyPage.rptMonthWiseEmpEva02();
        TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
        CompName.Text = comnam;
        TextObject txtempname = rptAppMonitor.ReportDefinition.ReportObjects["txtempname"] as TextObject;
        txtempname.Text = "Name: " + lstemp1[0].empname1 + ", " + "Designation: " + lstemp1[0].desig + ", Joinning Date: " + Convert.ToDateTime(lstemp1[0].joindate).ToString("dd-MMM-yyyy") + ", Salary:       , Benifits:     ";


        //TextObject txtDesignation = rptAppMonitor.ReportDefinition.ReportObjects["txtdesignation"] as TextObject;
        //txtDesignation.Text = "Designation: " + (((DataTable)Session["tblsaleteam"]).Select("teamcode='" + Salesteamcode + "'"))[0]["designtion"];

        TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
        txtdate.Text = " (From " + stdate + " To " + endDate + ")";

        //TextObject CompName02 = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName02"] as TextObject;
        //CompName02.Text = comnam;

        //TextObject txtdate02 = rptAppMonitor.ReportDefinition.ReportObjects["txtdate02"] as TextObject;
        //txtdate02.Text = " (From " + stdate.Trim() + " To " + endDate.Trim() + ")";


        TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        rptAppMonitor.Subreports["rptGradeSystem.rpt"].SetDataSource(ds.Tables[0]);

        rptAppMonitor.SetDataSource(lst);
        string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        rptAppMonitor.SetParameterValue("ComLogo", ComLogo);

        Session["Report1"] = rptAppMonitor;
        Session["Report1"] = rptAppMonitor;
    }





    [WebMethod(EnableSession = true)]
    public void PrintRptMonEva04(string stdate, string endDate, string eid)
    {

        List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> lst = (List<RealEntity.C_47_Kpi.EClassEmployeeMonEva>)Session["lst1"];
        List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp = (List<RealEntity.C_47_Kpi.EClassEmpCode2>)Session["tblemployee"];


        string empid = eid;

        List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp1 = lstemp.FindAll((p => p.empid == empid));


        string frdate = stdate;
        string todate = endDate;


        Hashtable hst = (Hashtable)Session["tblLogin"];
        string comcod = hst["comcod"].ToString();
        string comnam = hst["comnam"].ToString();
        string compname = hst["compname"].ToString();
        string username = hst["username"].ToString();
        string deptcode = hst["deptcode"].ToString();
        string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //Grade
        DataSet ds = KPIData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "RPTGRADE", "", "", "", "", "");
        if (ds == null) { return; }
        //KPI
        DataSet ds2 = KPIData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "RPTKPI", deptcode, "", "", "", "");
        if (ds2 == null) { return; }

        ReportDocument rptAppMonitor = new RealERPRPT.R_39_MyPage.rptMonthWiseEmpEval04();
        TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
        CompName.Text = comnam;

        TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
        txtdate.Text = " (From " + frdate.Trim() + " To " + todate.Trim() + ")";

        TextObject txtempname = rptAppMonitor.ReportDefinition.ReportObjects["txtempname"] as TextObject;
        txtempname.Text = "Name: " + lstemp1[0].empname1 + ", " + "Designation: " + lstemp1[0].desg + ", Joinning Date: " + Convert.ToDateTime(lstemp1[0].joindat).ToString("dd-MMM-yyyy") + ", Salary:       , Benifits:     ";


        //TextObject CompName02 = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName02"] as TextObject;
        //CompName02.Text = comnam;

        //TextObject txtdate02 = rptAppMonitor.ReportDefinition.ReportObjects["txtdate02"] as TextObject;
        //txtdate02.Text = " (From " + frdate.Trim() + " To " + todate.Trim() + ")";

        TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        rptAppMonitor.Subreports["rptGradeSystem.rpt"].SetDataSource(ds.Tables[0]);
        rptAppMonitor.Subreports["rptKPIIndex.rpt"].SetDataSource(ds2.Tables[0]);
        rptAppMonitor.SetDataSource(lst);

        Session["Report1"] = rptAppMonitor;
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<RealEntity.C_47_Kpi.EClassEmpCode> GetEmployeeList(string txtSrchSalesTeam, string queryString)
    {
        Hashtable hst = (Hashtable)Session["tblLogin"];
        string comcod = hst["comcod"].ToString();
        string srchEmp = "%" + txtSrchSalesTeam.Trim() + "%";
        string userid = "";
        if (queryString == "IndEmp")
            userid = hst["usrid"].ToString();
        string deptcode = hst["deptcode"].ToString();
        List<RealEntity.C_47_Kpi.EClassEmpCode> lst3 = new List<RealEntity.C_47_Kpi.EClassEmpCode>();
        lst3 = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", srchEmp, userid, deptcode);
        Session["tblemployee"] = lst3;
        return lst3;
    }



    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<RealEntity.C_47_Kpi.EClassEmpCode2> GetEmployeeList02(string txtSrchSalesTeam, string queryString, string date)
    {
        Hashtable hst = (Hashtable)Session["tblLogin"];
        string comcod = hst["comcod"].ToString();
        string srchEmp = "%" + txtSrchSalesTeam.Trim() + "%";
        string userid = "";
        if (queryString == "IndEmp")
            userid = hst["usrid"].ToString();
        string deptcode = hst["deptcode"].ToString();
        List<RealEntity.C_47_Kpi.EClassEmpCode2> lst3 = new List<RealEntity.C_47_Kpi.EClassEmpCode2>();
        string ymonid = Convert.ToDateTime(date).ToString("yyyyMM");
        lst3 = objUser.GetEmpCode2(srchEmp, userid, deptcode, ymonid);
        Session["tblemployee"] = lst3;
        return lst3;
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<RealEntity.C_47_Kpi.EClassEmpCode> GetFilterEmployeeList(string txtSrchSalesTeam)
    {
        List<RealEntity.C_47_Kpi.EClassEmpCode> lst = (List<RealEntity.C_47_Kpi.EClassEmpCode>)Session["tblemployee"];
        string srchEmp = txtSrchSalesTeam.Trim();
        if (srchEmp.Length > 0)
        {

            IEnumerable<RealEntity.C_47_Kpi.EClassEmpCode> ProjectQuery = (from employee in lst
                                                                           where employee.empname1.ToUpper().Contains(srchEmp.ToUpper())
                                                                           orderby employee.empid ascending
                                                                           select employee);
            return ProjectQuery.ToList();

        }

        else
            return lst;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<RealEntity.C_47_Kpi.EClassEmpCode2> GetFilterEmployeeList02(string txtSrchSalesTeam)
    {
        List<RealEntity.C_47_Kpi.EClassEmpCode2> lst = (List<RealEntity.C_47_Kpi.EClassEmpCode2>)Session["tblemployee"];
        string srchEmp = txtSrchSalesTeam.Trim();
        if (srchEmp.Length > 0)
        {

            IEnumerable<RealEntity.C_47_Kpi.EClassEmpCode2> ProjectQuery = (from employee in lst
                                                                            where employee.empname1.ToUpper().Contains(srchEmp.ToUpper())
                                                                            orderby employee.empid ascending
                                                                            select employee);
            return ProjectQuery.ToList();

        }

        else
            return lst;

    }

}
