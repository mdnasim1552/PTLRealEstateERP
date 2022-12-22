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
using RealERPRPT;
using RealEntity;

namespace RealERPWEB.ASMX_46_GrMgtInter
{
    /// <summary>
    /// Summary description for RptGrpMisDailyActiviteisWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class RptGrpMisDailyActiviteisWebService : System.Web.Services.WebService
    {

        public RptGrpMisDailyActiviteisWebService()
        {

        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DailyGrpRpt> GetDailyGrpRpt(string frdate, string todate)
        {
            Session["frdate"] = frdate;
            Session["todate"] = todate;
            DailyGrpRptMan dailyGrpRptMan = new DailyGrpRptMan();
            List<DailyGrpRpt> lst = dailyGrpRptMan.GetRptGrpDailyReport(frdate, todate);
            return lst;
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
            List<RealEntity.C_46_GrMgtInter.DailyGrpRpt> list = (List<RealEntity.C_46_GrMgtInter.DailyGrpRpt>)Session["tblData"];


            ReportDocument rrs1 = new ReportDocument();

            string frdate = Session["frdate"].ToString();
            string todate = Session["todate"].ToString();

            rrs1 = new RealERPRPT.R_46_GrMgtInter.RptGrpMisDailyAct();
            TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            rptCname.Text = comnam;

            TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtFDate1.Text = "(From " + frdate.Trim() + " To " + todate.Trim() + ")";


            TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rrs1.SetDataSource(list);
            HttpContext.Current.Session["Report1"] = rrs1;
        }

    }
}
