using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using RealERPLIB;
using RealEntity.C_46_GrMgtInter;
using System.Web.Script.Services;
using System.Collections;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using RealEntity.C_45_GrAcc;
/// <summary>
/// Summary description for RptGrpMisDailyActiviteis
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class RptGrpMisDailyActiviteis : System.Web.Services.WebService
{

    private RealEntity.C_45_GrAcc.CompanyManager companyManager = new RealEntity.C_45_GrAcc.CompanyManager();
    private RealEntity.C_45_GrAcc.RptGrpMisDailyActiviteisMan man = new RealEntity.C_45_GrAcc.RptGrpMisDailyActiviteisMan();
    public RptGrpMisDailyActiviteis()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //[WebMethod(EnableSession = true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    //checks consolidate

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<RealEntity.C_45_GrAcc.Companies> CallCompanyList(string isConsolidate)
    {

        List<RealEntity.C_45_GrAcc.Companies> compList = new List<RealEntity.C_45_GrAcc.Companies>();
        string comcod = HttpContext.Current.Session["comcod"].ToString();
        compList = companyManager.GetCompanyList(comcod, isConsolidate);
        return compList;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<RealEntity.C_45_GrAcc.RptGrpMisDailyActiviteis> GetRptGrpMisDailyActiviteis(string comp1, string frdate, string endDate)
    {
        Session["frdate"] = frdate;
        Session["todate"] = endDate;
        List<RealEntity.C_45_GrAcc.RptGrpMisDailyActiviteis> tmp = man.GetRptGrpMisDailyActiviteis(comp1, frdate, endDate);
        return tmp;

    }
    [WebMethod(EnableSession = true)]
    public void PrintRpt()
    {
        Hashtable hst = (Hashtable)Session["tblLogin"];
        string comcod = hst["comcod"].ToString();
        string comnam = hst["comnam"].ToString();
        string compname = hst["compname"].ToString();
        string username = hst["username"].ToString();
        string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        List<RealEntity.C_45_GrAcc.RptGrpMisDailyActiviteis> list = (List<RealEntity.C_45_GrAcc.RptGrpMisDailyActiviteis>)Session["tblDataOv"];


        ReportDocument rrs1 = new ReportDocument();

        string frdate = Session["frdate"].ToString();
        string todate = Session["todate"].ToString();

        rrs1 = new RealERPRPT.R_45_GrAcc.RptGrpMisMgtActivities();
        TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtComName"] as TextObject;
        rptCname.Text = comnam;

        TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
        txtFDate1.Text = "(From " + frdate.Trim() + " To " + todate.Trim() + ")";


        TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        rrs1.SetDataSource(list);
        string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        rrs1.SetParameterValue("ComLogo", ComLogo);

        HttpContext.Current.Session["Report1"] = rrs1;
    }




}
