using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealERPLIB;
using System.Data.SqlClient;
using System.Collections;
using System.Data;


namespace RealEntity.C_46_GrMgtInter
{
    public class DailyGrpRptMan
    {
        ProcessAccess MktData = new ProcessAccess();
        Common ComcodGetter = new Common();

       
        private string CallCompanyList()
        {
            string comcod = System.Web.HttpContext.Current.Session["comcod"].ToString();

           // string comcod = "8305";
            string CallType = (comcod.Substring(0, 1) == "8") ? "COMPLIST" : "GETSINGLECOM";
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", CallType, "", "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];
            //System.Web.HttpContext.Current.Session["tblData"] = dt;
            string comp1 = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comp1 += dt.Rows[i]["comcod"];
            }
            return comp1;

        }

        //private string GetCompCode()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    return (hst["comcod"].ToString());


        //}

        public List<DailyGrpRpt> GetRptGrpDailyReport(string frdate, string todate)
        {
            System.Web.HttpContext.Current.Session["frdate"] = frdate;
            System.Web.HttpContext.Current.Session["todate"] = todate;
            List<DailyGrpRpt> rptlst = new List<DailyGrpRpt>();
            string comcod = CallCompanyList();
            SqlDataReader dr = MktData.GetSqlReader(comcod, "SP_REPORT_GROUP_MIS03", "RPTMGTDAILYACT", frdate, todate, "", "", "", "", "", "", "");

            while (dr.Read())
            {
                DailyGrpRpt rptGrpDailyReport = new DailyGrpRpt();

                rptGrpDailyReport.grp = dr["grp"].ToString();
                rptGrpDailyReport.grpdesc = dr["grpdesc"].ToString();
                rptGrpDailyReport.compname = dr["compname"].ToString();
                rptGrpDailyReport.tsal = Convert.ToDouble(dr["tsal"].ToString());
                rptGrpDailyReport.tcoll = Convert.ToDouble(dr["tcoll"].ToString());
                rptGrpDailyReport.trec = Convert.ToDouble(dr["trec"].ToString());
                rptGrpDailyReport.tpay = Convert.ToDouble(dr["tpay"].ToString());
                rptGrpDailyReport.tcrec = Convert.ToDouble(dr["tcrec"].ToString());
                rptGrpDailyReport.tcisu = Convert.ToDouble(dr["tcisu"].ToString());
                rptGrpDailyReport.tsaleamt = Convert.ToDouble(dr["tsaleamt"].ToString());
                rptGrpDailyReport.tosaleamt = Convert.ToDouble(dr["tosaleamt"].ToString());
                rptGrpDailyReport.acsaleamt = Convert.ToDouble(dr["acsaleamt"].ToString());
                rptGrpDailyReport.tdayamt = Convert.ToDouble(dr["tdayamt"].ToString());
                rptGrpDailyReport.perotsale = Convert.ToDouble(dr["perotsale"].ToString());
                rptGrpDailyReport.tcollamt = Convert.ToDouble(dr["tcollamt"].ToString());
                rptGrpDailyReport.accollam = Convert.ToDouble(dr["accollam"].ToString());
                rptGrpDailyReport.tdaycollamt = Convert.ToDouble(dr["tdaycollamt"].ToString());
                rptGrpDailyReport.perotcoll = Convert.ToDouble(dr["perotcoll"].ToString());
                rptGrpDailyReport.acamt = Convert.ToDouble(dr["acamt"].ToString());
                rptGrpDailyReport.reconamt = Convert.ToDouble(dr["reconamt"].ToString());
                rptGrpDailyReport.depchq = Convert.ToDouble(dr["depchq"].ToString());
                rptGrpDailyReport.inhrchq = Convert.ToDouble(dr["inhrchq"].ToString());
                rptGrpDailyReport.inhfchq = Convert.ToDouble(dr["inhfchq"].ToString());
                rptGrpDailyReport.inhpchq = Convert.ToDouble(dr["inhpchq"].ToString());
                rptGrpDailyReport.repchq = Convert.ToDouble(dr["repchq"].ToString());
                rptGrpDailyReport.ncollamt = Convert.ToDouble(dr["ncollamt"].ToString());
                rptGrpDailyReport.lmrecamt = Convert.ToDouble(dr["lmrecamt"].ToString());
                rptGrpDailyReport.cmrecamt = Convert.ToDouble(dr["cmrecamt"].ToString());
                rptGrpDailyReport.otrecamt = Convert.ToDouble(dr["otrecamt"].ToString());
                rptGrpDailyReport.recpam = Convert.ToDouble(dr["recpam"].ToString());
                rptGrpDailyReport.payam = Convert.ToDouble(dr["payam"].ToString());
                rptGrpDailyReport.ainhfchq = Convert.ToDouble(dr["ainhfchq"].ToString());
                rptGrpDailyReport.ainhrchq = Convert.ToDouble(dr["ainhrchq"].ToString());
                rptGrpDailyReport.adepchq = Convert.ToDouble(dr["adepchq"].ToString());
                rptGrpDailyReport.arepchq = Convert.ToDouble(dr["arepchq"].ToString());
                rptGrpDailyReport.closbal = Convert.ToDouble(dr["closbal"].ToString());
                rptGrpDailyReport.bankbal = Convert.ToDouble(dr["bankbal"].ToString());
                rptGrpDailyReport.ainhpchq = Convert.ToDouble(dr["ainhpchq"].ToString());
                rptGrpDailyReport.tavamt = Convert.ToDouble(dr["tavamt"].ToString());
                rptGrpDailyReport.amt1 = Convert.ToDouble(dr["amt1"].ToString());
                rptGrpDailyReport.arepchq = Convert.ToDouble(dr["arepchq"].ToString());
                rptGrpDailyReport.amt2 = Convert.ToDouble(dr["amt2"].ToString());
                rptGrpDailyReport.amt3 = Convert.ToDouble(dr["amt3"].ToString());
                rptGrpDailyReport.amt4 = Convert.ToDouble(dr["amt4"].ToString());
                rptGrpDailyReport.amt5 = Convert.ToDouble(dr["amt5"].ToString());
                rptGrpDailyReport.amt6 = Convert.ToDouble(dr["amt6"].ToString());
                rptGrpDailyReport.tamt = Convert.ToDouble(dr["tamt"].ToString());
                rptGrpDailyReport.recpamis = Convert.ToDouble(dr["recpamis"].ToString());
                rptGrpDailyReport.payamis = Convert.ToDouble(dr["payamis"].ToString());
                rptGrpDailyReport.comcod = dr["comcod"].ToString();
                rptGrpDailyReport.mrramt = Convert.ToDouble(dr["mrramt"].ToString());
                rptGrpDailyReport.monplan = Convert.ToDouble(dr["monplan"].ToString());
                rptGrpDailyReport.excution = Convert.ToDouble(dr["excution"].ToString());
                rptGrpDailyReport.revamt = Convert.ToDouble(dr["revamt"].ToString());
                rptGrpDailyReport.usoldamt = Convert.ToDouble(dr["usoldamt"].ToString());
                rptGrpDailyReport.soldamt = Convert.ToDouble(dr["soldamt"].ToString());
                rptGrpDailyReport.recamt = Convert.ToDouble(dr["recamt"].ToString());
                rptGrpDailyReport.recabamt = Convert.ToDouble(dr["recabamt"].ToString());
                rptGrpDailyReport.costamt = Convert.ToDouble(dr["costamt"].ToString());
                rptGrpDailyReport.collamt = Convert.ToDouble(dr["collamt"].ToString());
                rptGrpDailyReport.netamt = Convert.ToDouble(dr["netamt"].ToString());
                rptGrpDailyReport.curempno = Convert.ToDouble(dr["curempno"].ToString());
                rptGrpDailyReport.curpay = Convert.ToDouble(dr["curpay"].ToString());
                rptGrpDailyReport.tastcollamt = Convert.ToDouble(dr["tastcollamt"].ToString());
                rptGrpDailyReport.hrcomcod = dr["hrcomcod"].ToString();
                rptlst.Add(rptGrpDailyReport);

            }

            System.Web.HttpContext.Current.Session["tblData"] = rptlst;
            return rptlst;
        }
    }
}
