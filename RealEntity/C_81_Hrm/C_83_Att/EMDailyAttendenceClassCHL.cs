using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm.C_83_Att
{
    public class EMDailyAttendenceClassCHL
    {
        public class DailyAttenCHLGroupWize
        {
            public string grp { get; set; }
            public string company { get; set; }
            public string rowid { get; set; }
            public string comcod { get; set; }
            public string deptid { get; set; }
            public string secid { get; set; }
            public string desigid { get; set; }
            public string empid { get; set; }
            public string idcardno { get; set; }
            public string grpdesc { get; set; }
            public string deptdesc { get; set; }
            public string deptname { get; set; }
            public string section { get; set; }
            public string sectionid { get; set; }
            public string desig { get; set; }
            public string empdsg { get; set; }
            public string empname { get; set; }
            public DateTime offintime { get; set; }
            public DateTime offouttime { get; set; }
            public DateTime intime { get; set; }
            public DateTime outtime { get; set; }
            public string late { get; set; }
            public string status { get; set; }
            public string eleave { get; set; }
            public string companyname { get; set; }
            public string desigadept { get; set; }
            public string rmrks { get; set; }
            public string remarks { get; set; }
            public double lpmday { get; set; }
            public double lcmday { get; set; }
            public double lvcurm { get; set; }
            public double today { get; set; }
            public double abscurm { get; set; }
            public DailyAttenCHLGroupWize() { }
        }


        public class EmpAttnIdWise
        {
            public string comcod { get; set; }
            public string dayid { get; set; }
            public string empid { get; set; }
            public string empnam { get; set; }
            public string machid { get; set; }
            public string idcardno { get; set; }
            public string spotid { get; set; }
            public DateTime stdtimein { get; set; }
            public DateTime stdtimeout { get; set; }
            public DateTime stdlunchtime { get; set; }
            public DateTime actualin { get; set; }
            public DateTime actualout { get; set; }
            public string actTimehour { get; set; }
            public double dedtimePenal1 { get; set; }
            public string dedtimeLunc { get; set; }
            public string dedout { get; set; }
            public string addhour { get; set; }
            public string addoffhour { get; set; }
            public string ActualWhour { get; set; }
            public string lateappv { get; set; }
            public string absstatus { get; set; }
            public string LeaveST { get; set; }
            public string attnhour { get; set; }
            public string attnminute { get; set; }
            public string actualattnminute { get; set; }
            public string attntimeminute { get; set; }
            public string addtime2 { get; set; }
            public DateTime wintime { get; set; }
            public DateTime wouttime { get; set; }
            public string latetime { get; set; }
            public string earlytime { get; set; }
            public string overtime { get; set; }
            public string lockfl { get; set; }
            public string addday { get; set; }
            public string empdept { get; set; }
            public string empdsg { get; set; }
            public string leav { get; set; }
            public string lt { get; set; }
            public DateTime joindate { get; set; }
            public string remarks { get; set; }
            public string dlathour { get; set; }
            public string derlevtimehour { get; set; }

        }

        public class EmpMnthAttn
        {
            public int secsl { get; set; }
            public int deptsl { get; set; }
            public string comcod { get; set; }
            public string idcardno { get; set; }
            public string empid { get; set; }
            public string empnam { get; set; }
            public string empdeptid { get; set; }
            public string sectionid { get; set; }
            public string empdsgid { get; set; }
            public string empdept { get; set; }
            public string section { get; set; }
            public string empdsg { get; set; }
            public string addday { get; set; }
            public string addtime1 { get; set; }
            public string addtime2 { get; set; }
            public double addtime3 { get; set; }
            public string col1 { get; set; }
            public string col1s { get; set; }
            public double col1o1 { get; set; }
            public string col1o { get; set; }
            public string col2 { get; set; }
            public string col2s { get; set; }
            public double col1o2 { get; set; }
            public string col2o { get; set; }
            public string col3 { get; set; }
            public string col3s { get; set; }
            public double col1o3 { get; set; }
            public string col3o { get; set; }
            public string col4 { get; set; }
            public string col4s { get; set; }
            public double col1o4 { get; set; }
            public string col4o { get; set; }
            public string col5 { get; set; }
            public string col5s { get; set; }
            public double col1o5 { get; set; }
            public string col5o { get; set; }
            public string col6 { get; set; }
            public string col6s { get; set; }
            public double col1o6 { get; set; }
            public string col6o { get; set; }
            public string col7 { get; set; }
            public string col7s { get; set; }
            public double col1o7 { get; set; }
            public string col7o { get; set; }
            public string col8 { get; set; }
            public string col8s { get; set; }
            public double col1o8 { get; set; }
            public string col8o { get; set; }
            public string col9 { get; set; }
            public string col9s { get; set; }
            public double col1o9 { get; set; }
            public string col9o { get; set; }
            public string col10 { get; set; }
            public string col10s { get; set; }
            public double col1o10 { get; set; }
            public string col10o { get; set; }
            public string col11 { get; set; }
            public string col11s { get; set; }
            public double col1o11 { get; set; }
            public string col11o { get; set; }
            public string col12 { get; set; }
            public string col12s { get; set; }
            public double col1o12 { get; set; }
            public string col12o { get; set; }
            public string col13 { get; set; }
            public string col13s { get; set; }
            public double col1o13 { get; set; }
            public string col13o { get; set; }
            public string col14 { get; set; }
            public string col14s { get; set; }
            public double col1o14 { get; set; }
            public string col14o { get; set; }
            public string col15 { get; set; }
            public string col15s { get; set; }
            public double col1o15 { get; set; }
            public string col15o { get; set; }
            public string col16 { get; set; }
            public string col16s { get; set; }
            public double col1o16 { get; set; }
            public string col16o { get; set; }
            public string col17 { get; set; }
            public string col17s { get; set; }
            public double col1o17 { get; set; }
            public string col17o { get; set; }
            public string col18 { get; set; }
            public string col18s { get; set; }
            public double col1o18 { get; set; }
            public string col18o { get; set; }
            public string col19 { get; set; }
            public string col19s { get; set; }
            public double col1o19 { get; set; }
            public string col19o { get; set; }
            public string col20 { get; set; }
            public string col20s { get; set; }
            public double col1o20 { get; set; }
            public string col20o { get; set; }
            public string col21 { get; set; }
            public string col21s { get; set; }
            public double col1o21 { get; set; }
            public string col21o { get; set; }
            public string col22 { get; set; }
            public string col22s { get; set; }
            public double col1o22 { get; set; }
            public string col22o { get; set; }
            public string col23 { get; set; }
            public string col23s { get; set; }
            public double col1o23 { get; set; }
            public string col23o { get; set; }
            public string col24 { get; set; }
            public string col24s { get; set; }
            public double col1o24 { get; set; }
            public string col24o { get; set; }
            public string col25 { get; set; }
            public string col25s { get; set; }
            public double col1o25 { get; set; }
            public string col25o { get; set; }
            public string col26 { get; set; }
            public string col26s { get; set; }
            public double col1o26 { get; set; }
            public string col26o { get; set; }
            public string col27 { get; set; }
            public string col27s { get; set; }
            public double col1o27 { get; set; }
            public string col27o { get; set; }
            public string col28 { get; set; }
            public string col28s { get; set; }
            public double col1o28 { get; set; }
            public string col28o { get; set; }
            public string col29 { get; set; }
            public string col29s { get; set; }
            public double col1o29 { get; set; }
            public string col29o { get; set; }
            public string col30 { get; set; }
            public string col30s { get; set; }
            public double col1o30 { get; set; }
            public string col30o { get; set; }
            public string col31 { get; set; }
            public string col31s { get; set; }
            public double col1o31 { get; set; }
            public string col31o { get; set; }
            public string col32 { get; set; }
            public double col1o32 { get; set; }
            public string col32o { get; set; }
            public double present { get; set; }
            public double absnt { get; set; }
            public double late { get; set; }
            public double earnlev { get; set; }
            public double siclev { get; set; }
            public double tleav { get; set; }
            public double casuallev { get; set; }
            public double withpaylev { get; set; }
            public double holyday { get; set; }
            public double dayout { get; set; }
            public double tpayable { get; set; }
            public double onduty { get; set; }
            public double abslate { get; set; }
            public string grpcod { get; set; }
            public string grpdesc { get; set; }
            public double levded { get; set; }

            public EmpMnthAttn() { }
        }
        [Serializable]
        public class RptMntAttenReport
        {
            public int secsl { get; set; }
            public int deptsl { get; set; }
            public string comcod { get; set; }
            public string idcardno { get; set; }
            public string empid { get; set; }
            public string empnam { get; set; }
            public string empdeptid { get; set; }
            public string sectionid { get; set; }
            public string empdsgid { get; set; }
            public string empdept { get; set; }
            public string section { get; set; }
            public string empdsg { get; set; }
            public string addday { get; set; }
            public string addtime1 { get; set; }
            public string addtime2 { get; set; }
            public double addtime3 { get; set; }
            public string col1 { get; set; }
            public string col1s { get; set; }
            public double col1o1 { get; set; }
            public string col1o { get; set; }
            public string col2 { get; set; }
            public string col2s { get; set; }
            public double col1o2 { get; set; }
            public string col2o { get; set; }
            public string col3 { get; set; }
            public string col3s { get; set; }
            public double col1o3 { get; set; }
            public string col3o { get; set; }
            public string col4 { get; set; }
            public string col4s { get; set; }
            public double col1o4 { get; set; }
            public string col4o { get; set; }
            public string col5 { get; set; }
            public string col5s { get; set; }
            public double col1o5 { get; set; }
            public string col5o { get; set; }
            public string col6 { get; set; }
            public string col6s { get; set; }
            public double col1o6 { get; set; }
            public string col6o { get; set; }
            public string col7 { get; set; }
            public string col7s { get; set; }
            public double col1o7 { get; set; }
            public string col7o { get; set; }
            public string col8 { get; set; }
            public string col8s { get; set; }
            public double col1o8 { get; set; }
            public string col8o { get; set; }
            public string col9 { get; set; }
            public string col9s { get; set; }
            public double col1o9 { get; set; }
            public string col9o { get; set; }
            public string col10 { get; set; }
            public string col10s { get; set; }
            public double col1o10 { get; set; }
            public string col10o { get; set; }
            public string col11 { get; set; }
            public string col11s { get; set; }
            public double col1o11 { get; set; }
            public string col11o { get; set; }
            public string col12 { get; set; }
            public string col12s { get; set; }
            public double col1o12 { get; set; }
            public string col12o { get; set; }
            public string col13 { get; set; }
            public string col13s { get; set; }
            public double col1o13 { get; set; }
            public string col13o { get; set; }
            public string col14 { get; set; }
            public string col14s { get; set; }
            public double col1o14 { get; set; }
            public string col14o { get; set; }
            public string col15 { get; set; }
            public string col15s { get; set; }
            public double col1o15 { get; set; }
            public string col15o { get; set; }
            public string col16 { get; set; }
            public string col16s { get; set; }
            public double col1o16 { get; set; }
            public string col16o { get; set; }
            public string col17 { get; set; }
            public string col17s { get; set; }
            public double col1o17 { get; set; }
            public string col17o { get; set; }
            public string col18 { get; set; }
            public string col18s { get; set; }
            public double col1o18 { get; set; }
            public string col18o { get; set; }
            public string col19 { get; set; }
            public string col19s { get; set; }
            public double col1o19 { get; set; }
            public string col19o { get; set; }
            public string col20 { get; set; }
            public string col20s { get; set; }
            public double col1o20 { get; set; }
            public string col20o { get; set; }
            public string col21 { get; set; }
            public string col21s { get; set; }
            public double col1o21 { get; set; }
            public string col21o { get; set; }
            public string col22 { get; set; }
            public string col22s { get; set; }
            public double col1o22 { get; set; }
            public string col22o { get; set; }
            public string col23 { get; set; }
            public string col23s { get; set; }
            public double col1o23 { get; set; }
            public string col23o { get; set; }
            public string col24 { get; set; }
            public string col24s { get; set; }
            public double col1o24 { get; set; }
            public string col24o { get; set; }
            public string col25 { get; set; }
            public string col25s { get; set; }
            public double col1o25 { get; set; }
            public string col25o { get; set; }
            public string col26 { get; set; }
            public string col26s { get; set; }
            public double col1o26 { get; set; }
            public string col26o { get; set; }
            public string col27 { get; set; }
            public string col27s { get; set; }
            public double col1o27 { get; set; }
            public string col27o { get; set; }
            public string col28 { get; set; }
            public string col28s { get; set; }
            public double col1o28 { get; set; }
            public string col28o { get; set; }
            public string col29 { get; set; }
            public string col29s { get; set; }
            public double col1o29 { get; set; }
            public string col29o { get; set; }
            public string col30 { get; set; }
            public string col30s { get; set; }
            public double col1o30 { get; set; }
            public string col30o { get; set; }
            public string col31 { get; set; }
            public string col31s { get; set; }
            public double col1o31 { get; set; }
            public string col31o { get; set; }
            public string col32 { get; set; }
            public double col1o32 { get; set; }
            public string col32o { get; set; }
            public double present { get; set; }
            public double absnt { get; set; }
            public double late { get; set; }
            public double earnlev { get; set; }
            public double siclev { get; set; }
            public double tleav { get; set; }
            public double casuallev { get; set; }
            public double withpaylev { get; set; }
            public double holyday { get; set; }
            public double dayout { get; set; }
            public double tpayable { get; set; }
            public double onduty { get; set; }
            public double abslate { get; set; }
            public string grpcod { get; set; }
            public string grpdesc { get; set; }
            public double spl { get; set; }
            public double mtl { get; set; }
            public double levded { get; set; }
            public RptMntAttenReport()
            {

            }
        }
        [Serializable]
        public class DailyAttenCHLDayWize
        {

            public string grp { get; set; }
            public string comcod { get; set; }
            public string deptid { get; set; }
            public string secid { get; set; }
            public string desigid { get; set; }
            public string empid { get; set; }
            public string idcardno { get; set; }
            public string grpdesc { get; set; }
            public string deptdesc { get; set; }
            public string section { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public DateTime offintime { get; set; }
            public DateTime offouttime { get; set; }
            public DateTime intime { get; set; }
            public DateTime outtime { get; set; }
            public string late { get; set; }
            public string status { get; set; }
            public string eleave { get; set; }

            public DailyAttenCHLDayWize() { }
        }
       

       
    }
}
