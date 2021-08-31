using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_08_PPlan
{
    public class BO_Class_Con
    {
        #region purchase Constraction
        [Serializable]
        public class EClassYear
        {
            public string ymonth { set; get; }
            public double taramt { set; get; }

            public double examt { set; get; }

            public EClassYear(string ymonth, double taramt, double examt)
            {
                this.ymonth = ymonth;
                this.taramt = taramt;
                this.examt = examt;
            }
        }
        [Serializable]
        public class EClassMonthly
        {
            public string yearmon { set; get; }
            public string ymon { set; get; }
            public double taramt { set; get; }
            public double examt { set; get; }
            public double taramtcore { set; get; }
            public double examtcore { set; get; }
            public EClassMonthly(string yearmon, string ymon, double taramt, double examt, double taramtcore, double examtcore)
            {
                this.yearmon = yearmon;
                this.ymon = ymon;
                this.taramt = taramt;
                this.examt = examt;
                this.taramtcore = taramtcore;
                this.examtcore = examtcore;

            }

        }

        [Serializable]
        public class ConsAnaGraphClass1
        {
            public string pactcode { set; get; }
            public string pactdesc { set; get; }
            public double taramt { set; get; }
            public double exeamt { set; get; }
        }

        [Serializable]
        public class ConsAnaGraphClass2
        {
            public string pactcode { set; get; }
            public string isircode1 { set; get; }
            public string isirdesc { set; get; }
            public double taramt { set; get; }
            public double exeamt { set; get; }
        }

        [Serializable]
        public class EClassWeekly
        {
            public string wcode1 { set; get; }
            public double wtaramt1 { set; get; }
            public double wexamt1 { set; get; }
            public string wcode2 { set; get; }
            public double wtaramt2 { set; get; }
            public double wexamt2 { set; get; }
            public string wcode3 { set; get; }
            public double wtaramt3 { set; get; }
            public double wexamt3 { set; get; }
            public string wcode4 { set; get; }
            public double wtaramt4 { set; get; }
            public double wexamt4 { set; get; }

            public EClassWeekly(string wcode1, double wtaramt1, double wexamt1, string wcode2, double wtaramt2, double wexamt2, string wcode3, double wtaramt3, double wexamt3,
                    string wcode4, double wtaramt4, double wexamt4)
            {
                this.wcode1 = wcode1;
                this.wtaramt1 = wtaramt1;
                this.wexamt1 = wexamt1;
                this.wcode2 = wcode2;
                this.wtaramt2 = wtaramt2;
                this.wexamt2 = wexamt2;
                this.wcode3 = wcode3;
                this.wtaramt3 = wtaramt3;
                this.wexamt3 = wexamt3;
                this.wcode4 = wcode4;
                this.wtaramt4 = wtaramt4;
                this.wexamt4 = wexamt4;

            }

        }

        //[Serializable]
        //public class EClassDayWisePur
        //{
        //    public string pactcode { set; get; }
        //    public string pactdesc { set; get; }
        //    public string rsircode { set; get; }
        //    public string rsirdesc { set; get; }

        //    public string ssircode { set; get; }
        //    public string ssirdesc { set; get; }
        //    public string billno1 { set; get; }

        //    public string billno { set; get; }
        //    public string billdate1 { set; get; }
        //    public string vounum1 { set; get; }
        //    public string vounum { set; get; }
        //    public double billamt { set; get; }

        //    public EClassDayWisePur(string pactcode, string pactdesc, string rsircode, string rsirdesc, string ssircode, string ssirdesc, string billno1, string billno,
        //            string billdate1, string vounum1, string vounum, double billamt)
        //    {
        //        this.pactcode = pactcode;
        //        this.pactdesc = pactdesc;
        //        this.rsircode = rsircode;
        //        this.rsirdesc = rsirdesc;
        //        this.ssircode = ssircode;
        //        this.ssirdesc = ssirdesc;
        //        this.billno1 = billno1;
        //        this.billno = billno;
        //        this.billdate1 = billdate1;
        //        this.vounum1 = vounum1;
        //        this.vounum = vounum;
        //        this.billamt = billamt;
        //    }
        //}

        //[Serializable]
        //public class EClassDayWisePay
        //{
        //    public string pactcode { set; get; }
        //    public string pactdesc { set; get; }
        //    public string cactcode { set; get; }
        //    public string cactdesc { set; get; }

        //    public string ssircode { set; get; }
        //    public string ssirdesc { set; get; }
        //    public string billno1 { set; get; }

        //    public string billno { set; get; }
        //    public string voudat { set; get; }
        //    public string vounum1 { set; get; }
        //    public string vounum { set; get; }
        //    public double payamt { set; get; }

        //    public EClassDayWisePay(string pactcode, string pactdesc, string cactcode, string cactdesc, string ssircode, string ssirdesc, string billno1, string billno,
        //            string voudat, string vounum1, string vounum, double payamt)
        //    {
        //        this.pactcode = pactcode;
        //        this.pactdesc = pactdesc;
        //        this.cactcode = cactcode;
        //        this.cactdesc = cactdesc;
        //        this.ssircode = ssircode;
        //        this.ssirdesc = ssirdesc;
        //        this.billno1 = billno1;
        //        this.billno = billno;
        //        this.voudat = voudat;
        //        this.vounum1 = vounum1;
        //        this.vounum = vounum;
        //        this.payamt = payamt;
        //    }
        //}

        #endregion


        [Serializable]
        public class ProjectDesign
        {
            public string comcod { set; get; }
            public string actcode { set; get; }

            public string actdesc { get; set; }
            public double proesam { set; get; }
            public double proadam { set; get; }
            public double dueam { set; get; }

            public string gcod { get; set; }
            public string gdesc { get; set; }
            public string gval { get; set; }

            public string job1 { get; set; }
            public string job2 { get; set; }
            public string job3 { get; set; }
            public string job4 { get; set; }
            public string job5 { get; set; }
            public string job6 { get; set; }
            public string job7 { get; set; }
            public string job8 { get; set; }
            public string job9 { get; set; }
            public string job10 { get; set; }

            public string job11 { get; set; }
            public string job12 { get; set; }
            public string job13 { get; set; }
            public string job14 { get; set; }

            public ProjectDesign()
            {

            }
        }




        [Serializable]
        public class ProjectTargetAnalysis
        {

            //comcod, pactcode, isircode, flrcod, bgdqty, bgdrat, bgdamt, startdate, enddate, today, exstartdate, exenddate, exdur,proamt, flrdes, isirdesc, isirunit
            public string comcod { set; get; }
            public string pactcode { set; get; }
            public string isircode { get; set; }
            public string flrcod { get; set; }
            public double bgdqty { get; set; }
            public double bgdrat { get; set; }
            public double bgdamt { get; set; }
            public DateTime startdate { get; set; }
            public DateTime enddate { get; set; }
            public double today { set; get; }

            public DateTime exstartdate { get; set; }
            public DateTime exenddate { get; set; }
            public double exdur { set; get; }
            public double proamt { set; get; }
            public string flrdes { get; set; }
            public string isirdesc { get; set; }
            public string isirunit { get; set; }



            public ProjectTargetAnalysis()
            {

            }
        }

        [Serializable]
        public class RptTenderProposal
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string rescode { get; set; }
            public string resdesc { get; set; }
            public double amt1 { get; set; }
            public double amt2 { get; set; }
            public double amt3 { get; set; }
            public RptTenderProposal(){ }
        }
    }
}
