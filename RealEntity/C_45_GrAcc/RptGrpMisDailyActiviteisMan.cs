using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEntity.C_45_GrAcc;
using RealERPLIB;
using System.Data.SqlClient;

namespace RealEntity.C_45_GrAcc
{
    public class RptGrpMisDailyActiviteisMan
    {
        private ProcessAccess MktData = new ProcessAccess();
        //DataSet ds1 = MktData.GetTransInfo(comp1, "SP_REPORT_GROUP_MIS02", "RPTMGTACTIVITEIS", frdate, todate, "", "", "", "", "", "", "");
        public List<RptGrpMisDailyActiviteis> GetRptGrpMisDailyActiviteis(string comp1,string frdate,string todate) 
        {
            List<RptGrpMisDailyActiviteis> lst = new List<RptGrpMisDailyActiviteis>();

            SqlDataReader dr = MktData.GetSqlReader(comp1, "SP_REPORT_GROUP_MIS02", "RPTMGTACTIVITEIS", frdate, todate, "", "", "", "", "", "", "");
            while (dr.Read())
            {
                RptGrpMisDailyActiviteis rptGrpMisDailyActiviteis = new RptGrpMisDailyActiviteis();

                rptGrpMisDailyActiviteis.grp= dr["grp"].ToString();
                rptGrpMisDailyActiviteis.grpdesc = dr["grpdesc"].ToString();
                rptGrpMisDailyActiviteis.deptname = dr["deptname"].ToString();
                rptGrpMisDailyActiviteis.compname = dr["compname"].ToString();
                rptGrpMisDailyActiviteis.capacity= Convert.ToDouble(dr["capacity"].ToString());
                rptGrpMisDailyActiviteis.masbgd = Convert.ToDouble(dr["masbgd"].ToString());
                rptGrpMisDailyActiviteis.bep= Convert.ToDouble(dr["bep"].ToString());
                rptGrpMisDailyActiviteis.tsaleamt = Convert.ToDouble(dr["tsaleamt"].ToString());
                rptGrpMisDailyActiviteis.tosaleamt = Convert.ToDouble(dr["tosaleamt"].ToString());
                rptGrpMisDailyActiviteis.suamt = Convert.ToDouble(dr["suamt"].ToString());
                rptGrpMisDailyActiviteis.perotsal = Convert.ToDouble(dr["perotsal"].ToString());

                //rptGrpMisDailyActiviteis.perotsal = dr["perotsal"].ToString();
                rptGrpMisDailyActiviteis.actdesc = dr["actdesc"].ToString();

                rptGrpMisDailyActiviteis.tcollamt=Convert.ToDouble(dr["tcollamt"].ToString());
                rptGrpMisDailyActiviteis.tastcollamt = Convert.ToDouble(dr["tastcollamt"].ToString());
                rptGrpMisDailyActiviteis.acamt=Convert.ToDouble(dr["acamt"].ToString());
                rptGrpMisDailyActiviteis.perotcoll=Convert.ToDouble(dr["perotcoll"].ToString());
                rptGrpMisDailyActiviteis.inhrchq=Convert.ToDouble(dr["inhrchq"].ToString());
                rptGrpMisDailyActiviteis.inhfchq=Convert.ToDouble(dr["inhfchq"].ToString());
                rptGrpMisDailyActiviteis.inhpchq=Convert.ToDouble(dr["inhpchq"].ToString());
                rptGrpMisDailyActiviteis.chqinhand=Convert.ToDouble(dr["chqinhand"].ToString());


                rptGrpMisDailyActiviteis.recpam = Convert.ToDouble(dr["recpam"].ToString());
                rptGrpMisDailyActiviteis.payam = Convert.ToDouble(dr["payam"].ToString());
                rptGrpMisDailyActiviteis.balam = Convert.ToDouble(dr["balam"].ToString());
                rptGrpMisDailyActiviteis.closbal = Convert.ToDouble(dr["closbal"].ToString());
                rptGrpMisDailyActiviteis.closlia = Convert.ToDouble(dr["closlia"].ToString());
                rptGrpMisDailyActiviteis.avloan = Convert.ToDouble(dr["avloan"].ToString());
                rptGrpMisDailyActiviteis.toam = Convert.ToDouble(dr["toam"].ToString());
                rptGrpMisDailyActiviteis.dueam = Convert.ToDouble(dr["dueam"].ToString());
                rptGrpMisDailyActiviteis.pdcam = Convert.ToDouble(dr["pdcam"].ToString());
                rptGrpMisDailyActiviteis.comcod = dr["comcod"].ToString();
                rptGrpMisDailyActiviteis.reqamt = Convert.ToDouble(dr["reqamt"].ToString());
                rptGrpMisDailyActiviteis.mrramt = Convert.ToDouble(dr["mrramt"].ToString());
                rptGrpMisDailyActiviteis.billamt = Convert.ToDouble(dr["billamt"].ToString());
                rptGrpMisDailyActiviteis.masplan = Convert.ToDouble(dr["masplan"].ToString());
                rptGrpMisDailyActiviteis.monplan = Convert.ToDouble(dr["monplan"].ToString());
                rptGrpMisDailyActiviteis.excution = Convert.ToDouble(dr["excution"].ToString());
                rptGrpMisDailyActiviteis.peromasp = Convert.ToDouble(dr["peromasp"].ToString());
                rptGrpMisDailyActiviteis.peromonp = Convert.ToDouble(dr["peromonp"].ToString());
                rptGrpMisDailyActiviteis.oramt = Convert.ToDouble(dr["oramt"].ToString());
                rptGrpMisDailyActiviteis.revamt = Convert.ToDouble(dr["revamt"].ToString());
                rptGrpMisDailyActiviteis.maramt = Convert.ToDouble(dr["maramt"].ToString());
                rptGrpMisDailyActiviteis.chngeper = Convert.ToDouble(dr["chngeper"].ToString());
                rptGrpMisDailyActiviteis.unsoldam = Convert.ToDouble(dr["unsoldam"].ToString());
                rptGrpMisDailyActiviteis.soldam = Convert.ToDouble(dr["soldam"].ToString());
                rptGrpMisDailyActiviteis.ramt = Convert.ToDouble(dr["ramt"].ToString());
                rptGrpMisDailyActiviteis.atodues = Convert.ToDouble(dr["atodues"].ToString());
                rptGrpMisDailyActiviteis.bamt = Convert.ToDouble(dr["bamt"].ToString());

                rptGrpMisDailyActiviteis.ptodues = Convert.ToDouble(dr["ptodues"].ToString());
                rptGrpMisDailyActiviteis.ctodues = Convert.ToDouble(dr["ctodues"].ToString());
                rptGrpMisDailyActiviteis.cdlyadcharge1 = dr["cdlyadcharge1"].ToString();
                rptGrpMisDailyActiviteis.salamt = Convert.ToDouble(dr["salamt"].ToString());
                rptGrpMisDailyActiviteis.trecamt = Convert.ToDouble(dr["trecamt"].ToString());
                rptGrpMisDailyActiviteis.netexpamt = Convert.ToDouble(dr["netexpamt"].ToString());
                rptGrpMisDailyActiviteis.liaamt = Convert.ToDouble(dr["liaamt"].ToString());
                rptGrpMisDailyActiviteis.netlnamt = Convert.ToDouble(dr["netlnamt"].ToString());
                rptGrpMisDailyActiviteis.compname1 =dr["compname1"].ToString();
                rptGrpMisDailyActiviteis.costamt = Convert.ToDouble(dr["costamt"].ToString());
                rptGrpMisDailyActiviteis.collamt = Convert.ToDouble(dr["collamt"].ToString());
                rptGrpMisDailyActiviteis.netamt = Convert.ToDouble(dr["netamt"].ToString());
                rptGrpMisDailyActiviteis.issuebasis = dr["issuebasis"].ToString();
                rptGrpMisDailyActiviteis.probasis =dr["probasis"].ToString();
                rptGrpMisDailyActiviteis.matcon =dr["matcon"].ToString();
                rptGrpMisDailyActiviteis.actdesc1 = dr["actdesc1"].ToString();
                rptGrpMisDailyActiviteis.toemp = Convert.ToDouble(dr["toemp"].ToString());
                rptGrpMisDailyActiviteis.netpay = Convert.ToDouble(dr["netpay"].ToString());
                lst.Add(rptGrpMisDailyActiviteis);

            }
            System.Web.HttpContext.Current.Session["tblDataOv"] = lst;
            return lst;
        }
    }
}
