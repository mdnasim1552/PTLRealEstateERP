using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RealEntity.C_22_Sal
{
    public class EClassSales_02
    {
        [Serializable]
        public class EClassVaT
        {
            public string centrid { set; get; }
            public string centrdesc { set; get; }

            public string custid { set; get; }
            public string custdesc { set; get; }

            public string memono1 { set; get; }
            public string vounum1 { set; get; }

            public string memodat { set; get; }
            public double itmamt { set; get; }
            public double vat { set; get; }

            public double invdis { set; get; }
            public EClassVaT(string centrid, string centrdesc, string custid, string custdesc, string memono1, string vounum1, string memodat, double itmamt, double vat, double invdis)
            {
                this.centrid = centrid;
                this.centrdesc = centrdesc;
                this.custid = custid;
                this.custdesc = custdesc;
                this.memono1 = memono1;
                this.vounum1 = vounum1;
                this.memodat = memodat;
                this.itmamt = itmamt;
                this.vat = vat;
                this.invdis = invdis;
            }
        }

        #region SalesDash_Board
        [Serializable]
        public class EClassYear
        {
            public string yearid { set; get; }

            public double samt { set; get; }
            public double collamt { set; get; }

            public EClassYear(string yearid, double samt, double collamt)
            {
                this.yearid = yearid;
                this.samt = samt;
                this.collamt = collamt;
            }
        }
        [Serializable]
        public class EClassMonthly
        {
            public string yearmon { set; get; }

            public string yearmon1 { set; get; }

            public double ttlsalamt { set; get; }
            public double collamt { set; get; }
            public double targtsaleamt { set; get; }
            public double tarcollamt { set; get; }

            public double targtsaleamtcore { set; get; }
            public double tarcollamtcore { set; get; }


            public double ttlsalamtcore { set; get; }
            public double collamtcrore { set; get; }
            public string ymon { set; get; }






            public EClassMonthly(string yearmon, string yearmon1, double ttlsalamt, double collamt, double targtsaleamt, double tarcollamt, double targtsaleamtcore, double tarcollamtcore, double ttlsalamtcore, double collamtcrore, string ymon)
            {
                this.yearmon = yearmon;
                this.yearmon1 = yearmon1;
                this.ttlsalamt = ttlsalamt;
                this.collamt = collamt;


                this.targtsaleamt = targtsaleamt;
                this.tarcollamt = tarcollamt;
                this.targtsaleamtcore = targtsaleamtcore;
                this.tarcollamtcore = tarcollamtcore;


                this.ttlsalamtcore = ttlsalamtcore;
                this.collamtcrore = collamtcrore;
                this.ymon = ymon;

            }
        }
        [Serializable]
        public class EClassWeekly
        {
            public string wcode1 { set; get; }
            public double wsamt1 { set; get; }
            public double wcamt1 { set; get; }

            public string wcode2 { set; get; }
            public double wsamt2 { set; get; }
            public double wcamt2 { set; get; }

            public string wcode3 { set; get; }
            public double wsamt3 { set; get; }
            public double wcamt3 { set; get; }

            public string wcode4 { set; get; }
            public double wsamt4 { set; get; }
            public double wcamt4 { set; get; }

            public EClassWeekly(string wcode1, double wsamt1, double wcamt1, string wcode2, double wsamt2, double wcamt2, string wcode3, double wsamt3, double wcamt3,
                    string wcode4, double wsamt4, double wcamt4)
            {
                this.wcode1 = wcode1;
                this.wsamt1 = wsamt1;
                this.wcamt1 = wcamt1;
                this.wcode2 = wcode2;
                this.wsamt2 = wsamt2;
                this.wcamt2 = wcamt2;
                this.wcode3 = wcode3;
                this.wsamt3 = wsamt3;
                this.wcamt3 = wcamt3;
                this.wcode4 = wcode4;
                this.wsamt4 = wsamt4;
                this.wcamt4 = wcamt4;
            }
        }
        [Serializable]
        public class EClassDayWise
        {
            public string centrid { set; get; }
            public string centrdesc { set; get; }
            public string custid { set; get; }
            public string custdesc { set; get; }
            public string memono1 { set; get; }

            public string memono { set; get; }
            public string memodat { set; get; }
            public string vounum1 { set; get; }
            public string vounum { set; get; }
            public double itmamt { set; get; }
            public double vat { set; get; }
            public double invdis { set; get; }

            public EClassDayWise(string centrid, string centrdesc, string custid, string custdesc, string memono1, string memono, string memodat, string vounum1, string vounum,
                    double itmamt, double vat, double invdis)
            {
                this.centrid = centrid;
                this.centrdesc = centrdesc;
                this.custid = custid;
                this.custdesc = custdesc;
                this.memono1 = memono1;
                this.memono = memono;
                this.memodat = memodat;
                this.vounum1 = vounum1;
                this.vounum = vounum;
                this.itmamt = itmamt;
                this.vat = vat;
                this.invdis = invdis;
            }
        }

        [Serializable]
        public class EClassDayWiseColl
        {
            public string centrid { set; get; }
            public string centrdesc { set; get; }
            public string custid { set; get; }
            public string custdesc { set; get; }
            public string mrslno1 { set; get; }

            public string mrslno { set; get; }
            public string mrdat { set; get; }
            public string vounum1 { set; get; }
            public string vounum { set; get; }
            public double amount { set; get; }

            public EClassDayWiseColl(string centrid, string centrdesc, string custid, string custdesc, string mrslno1, string mrslno, string mrdat, string vounum1,
                string vounum, double amount)
            {
                this.centrid = centrid;
                this.centrdesc = centrdesc;
                this.custid = custid;
                this.custdesc = custdesc;
                this.mrslno1 = mrslno1;
                this.mrslno = mrslno;
                this.mrdat = mrdat;
                this.vounum1 = vounum1;
                this.vounum = vounum;
                this.amount = amount;
            }
        }



        #endregion

        [Serializable]
        public class EClassYearlyTarget
        {

            public string pactdesc { set; get; }
            public double usize { set; get; }
            public double tunit { set; get; }

            public double sqty { set; get; }
            public double usqty { set; get; }

            public double qty1 { set; get; }
            public double amt1 { set; get; }

            public double qty2 { set; get; }
            public double amt2 { set; get; }
            public double qty3 { set; get; }
            public double amt3 { set; get; }
            public double qty4 { set; get; }
            public double amt4 { set; get; }
            public double qty5 { set; get; }
            public double amt5 { set; get; }
            public double qty6 { set; get; }
            public double amt6 { set; get; }
            public double qty7 { set; get; }
            public double amt7 { set; get; }
            public double qty8 { set; get; }
            public double amt8 { set; get; }
            public double qty9 { set; get; }
            public double amt9 { set; get; }
            public double qty10 { set; get; }
            public double amt10 { set; get; }
            public double qty11 { set; get; }
            public double amt11 { set; get; }
            public double qty12 { set; get; }
            public double amt12 { set; get; }
            public double tqty { set; get; }
            public double tamt { set; get; }







            //public EClassYearlyTarget(string centrid, string centrdesc, string custid, string custdesc, string memono1, string vounum1, string memodat, double itmamt, double vat, double invdis)
            //{
            //    this.centrid = centrid;
            //    this.centrdesc = centrdesc;
            //    this.custid = custid;
            //    this.custdesc = custdesc;
            //    this.memono1 = memono1;
            //    this.vounum1 = vounum1;
            //    this.memodat = memodat;
            //    this.itmamt = itmamt;
            //    this.vat = vat;
            //    this.invdis = invdis;
            //}
        }


        //Created By MD.Ibrahim Khalil-----Date: 02.08.2018------=======
        #region EclassUnitWiseCost

        [Serializable]
        public class EclassGetUnitWiseCost
        {
            public string comcod { get; set; }
            public string usircode { get; set; }
            public string udesc { get; set; }
            public string munit { get; set; }
            public string bstat { get; set; }
            public string urmrks { get; set; }
            public string facing { get; set; }
            public string uview { get; set; }
            public string mgtbook { get; set; }
            public double cooperative { get; set; }
            public double trate { get; set; }
            public double usize { get; set; }
            public double uqty { get; set; }
            public double urate { get; set; }
            public double uamt { get; set; }
            public double pqty { get; set; }
            public double pamt { get; set; }
            public double tamt { get; set; }
            public double minbam { get; set; }
            public double utility { get; set; }
            public double cbgdpsft { get; set; }
            public double lbgdpsft { get; set; }
            public double tobgdpsft { get; set; }

            public EclassGetUnitWiseCost() { }

            public EclassGetUnitWiseCost(string comcod, string usircode, string udesc, string munit, string bstat, string urmrks, string facing, string uview,
              double cooperative, string mgtbook, double trate, double usize, double uqty, double urate, double uamt, double pqty, double pamt,
                double tamt, double minbam, double utility, double cbgdpsft, double lbgdpsft, double tobgdpsft)
            {
                this.comcod = comcod;
                this.usircode = usircode;
                this.udesc = udesc;
                this.munit = munit;
                this.bstat = bstat;
                this.urmrks = urmrks;
                this.facing = facing;
                this.uview = uview;
                this.cooperative = cooperative;
                this.mgtbook = mgtbook;
                this.trate = trate;

                this.usize = usize;
                this.uqty = uqty;
                this.urate = urate;
                this.uamt = uamt;
                this.pqty = pqty;
                this.pamt = pamt;
                this.tamt = tamt;
                this.minbam = minbam;
                this.utility = utility;
                this.cbgdpsft = cbgdpsft;
                this.lbgdpsft = lbgdpsft;
                this.tobgdpsft = tobgdpsft;

            }
        }

        #endregion

        #region
        [Serializable]
        public class EclassCurrentDues
        {

            public string comcod { get; set; }
            public string pactdesc { get; set; }
            public string custname { get; set; }
            public string udesc { get; set; }
            public string cteam { get; set; }
            public string gdesc { get; set; }
            public string schdate { get; set; }
            public double dueins { get; set; }
            public double dueamt { get; set; }
            public double cdueamt { get; set; }


            public EclassCurrentDues() { }

            public EclassCurrentDues(string comcod, string pactdesc, string custname, string udesc, string gdesc, string schdate, double dueins, double dueamt, double cdueamt)
            {
                this.comcod = comcod;
                this.pactdesc = pactdesc;
                this.custname = custname;
                this.udesc = udesc;
                this.gdesc = gdesc;
                this.schdate = schdate;
                this.dueins = dueins;
                this.dueamt = dueamt;
                this.cdueamt = cdueamt;


            }


        }


        [Serializable]
        public class EclassPrjClientStatus
        {

            public string comcod { get; set; }
            public string uunitname { get; set; }
            public double unusize { get; set; }
            public string sunitname { get; set; }
            public double usize { get; set; }
            public double tsize { get; set; }
            public string custname { get; set; }
            public double aptcost { get; set; }
            public double modifee { get; set; }
            public double assofee { get; set; }
            public double delayfee { get; set; }
            public double regfee { get; set; }
            public double toamt { get; set; }
            public double ramt { get; set; }
            public double todue { get; set; }


            public string register { get; set; }




            public EclassPrjClientStatus() { }

            //public EclassPrjClientStatus(string comcod, string pactdesc, string custname, string udesc, string gdesc, string schdate, double dueins, double dueamt, double cdueamt)
            //{
            //    this.comcod = comcod;
            //    this.pactdesc = pactdesc;
            //    this.custname = custname;
            //    this.udesc = udesc;
            //    this.gdesc = gdesc;
            //    this.schdate = schdate;
            //    this.dueins = dueins;
            //    this.dueamt = dueamt;
            //    this.cdueamt = cdueamt;


            //}


        }



        #endregion

        [Serializable]
        public class MonthlyConAss
        {
            public string assno { get; set; }
            public string csircod { get; set; }
            public string sirdesc { get; set; }
            public string pojcod { get; set; }
            public string actdesc { get; set; }
            public double mark { get; set; }
            public string position { get; set; }

        }

        [Serializable]
        public class RptCustApp
        {
            public string name { get; set; }
            public string fname { get; set; }
            public string mname { get; set; }
            public string occupation { get; set; }
            public string paddress { get; set; }
            public string peraddress { get; set; }
            public string telephone { get; set; }
            public string mob { get; set; }
            public string nationality { get; set; }
            public string email { get; set; }
            public DateTime dob { get; set; }
            public string bloodg { get; set; }
            public string religion { get; set; }
            public string hname { get; set; }
            public string nomname { get; set; }
            public string nomrel { get; set; }
            public string projectname { get; set; }
            public string prjadd { get; set; }
            public string aptname { get; set; }
            public string aptsize { get; set; }
            public string flrdesc { get; set; }
            public double aptprice { get; set; }
            public double carparking { get; set; }
            public double othprice { get; set; }
            public double bookamt { get; set; }
            public double downamt { get; set; }

        }




        [Serializable]
        public class RptCustBookApp2
        {
            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string appdate { get; set; }
            public string bookamt { get; set; }
            public string chequeno { get; set; }
            public string bankname { get; set; }
            public string bbranch { get; set; }
            public string paydate { get; set; }
            public string intavail { get; set; }
            public string paymode { get; set; }
            public string custimg { get; set; }
            public string bookingType { get; set; }
            public string clientID { get; set; }
            public string towerNo { get; set; }
            public string Apartmenttype { get; set; }
            public string ttype { get; set; }
            public string floorr { get; set; }
            public string size { get; set; }
            public string side { get; set; }
            public string parkingNo { get; set; }
            public string parkingLevel { get; set; }
            public string locatedAt { get; set; }
            public string moneyReceipt { get; set; }
            public string bookingChart { get; set; }
            public string fullname { get; set; }
            public string spouse { get; set; }
            public string fathername { get; set; }
            public string mothername { get; set; }
            public string presentaddr { get; set; }
            public string permenentaddr { get; set; }
            public string mobilenum { get; set; }
            public string telephonenum { get; set; }
            public string birthdate { get; set; }
            public string marriageday { get; set; }
            public string nationalid { get; set; }
            public string tinnumber { get; set; }
            public string mailingaddre { get; set; }
            public string occupation { get; set; }
            public string nationality { get; set; }
            public string religion { get; set; }
            public string drivlicence { get; set; }
            public string bloodgroup { get; set; }
            public string emailaddr { get; set; }
            public string ClientStatus { get; set; }
            public string namapplicant { get; set; }
            public string relationApp { get; set; }
            public string nationaldr { get; set; }
            public string applicant { get; set; }
            public string nomineinfo { get; set; }
            public string nameofnominee1 { get; set; }
            public string relationshipnom1 { get; set; }
            public string custnote { get; set; }
            public string nominenumber { get; set; }
        }


        [Serializable]
        public class RptNomineeBookApp2
        {
            public string nomineename { get; set; }
            public string fathersname { get; set; }
            public string mothersname { get; set; }
            public string presentaddr { get; set; }
            public string permanentaddr { get; set; }
            public string email { get; set; }
            public string relationship { get; set; }
            public string mobileno { get; set; }
            public string telephone { get; set; }
            public string dateofbirth { get; set; }
            public string nationalid { get; set; }
            public string csignature { get; set; }
            public string share { get; set; }
            public string nationality { get; set; }
            public string date { get; set; }
            public string nomineeimage { get; set; }
        }


        [Serializable]
        public class RptNominatedBookApp2
        {
            public string nomineename { get; set; }
            public string profession { get; set; }
            public string presentaddr { get; set; }
            public string permanentaddress { get; set; }
            public string mobileno { get; set; }
            public string email { get; set; }
            public string csignature { get; set; }
            public string relation { get; set; }
            public string telephone { get; set; }
            public string nominatedsig { get; set; }
            public string nationality { get; set; }
            public string date { get; set; }
            public string nominatedimage { get; set; }
        }



        [Serializable]
        public class Customerinf
        {
            public string gdesc { get; set; }
            public string gdesc1 { get; set; }


        }

        [Serializable]
        public class RptAvailability
        {
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string usircode { get; set; }
            public string flrdesc3 { get; set; }
            public string flrdesc { get; set; }
            public string udesc { get; set; }
            public string custname { get; set; }
            public string munit { get; set; }
            public double uqty { get; set; }
            public double usize { get; set; }
            public double rate { get; set; }
            public double parcent { get; set; }
            public string udesc2 { get; set; }
            public double luqty { get; set; }
            public double ownsize { get; set; }
            public double pamt { get; set; }
            public double utility { get; set; }
            public double cooperative { get; set; }
            public double famt { get; set; }

        }
        [Serializable]
        public class RtpAvailChartPrint
        {
            public string comcod { get; set; }
            public string flrcod { get; set; }
            public string flrdesc { get; set; }
            public string untd1 { get; set; }
            public string untd2 { get; set; }
            public string untd3 { get; set; }
            public string untd4 { get; set; }
            public string untd5 { get; set; }
            public string untd6 { get; set; }
            public string untd7 { get; set; }
            public string untd8 { get; set; }
            public string untd9 { get; set; }
            public string untd10 { get; set; }
            public string untd11 { get; set; }
            public string untd12 { get; set; }
            public string untd13 { get; set; }
            public string untd14 { get; set; }
            public string untd15 { get; set; }
            public string untd16 { get; set; }
            public string untd17 { get; set; }
            public string untd18 { get; set; }
            public string untd19 { get; set; }
            public string untd20 { get; set; }
            public string untd21 { get; set; }
            public string untd22 { get; set; }
            public string untd23 { get; set; }
            public string untd24 { get; set; }
            public string untd25 { get; set; }
            public string untd26 { get; set; }
            public string untd27 { get; set; }
            public string untd28 { get; set; }
            public string untd29 { get; set; }
            public string untd30 { get; set; }
            public string untd31 { get; set; }
            public string untd32 { get; set; }
            public string untd33 { get; set; }
            public string untd34 { get; set; }
            public string untd35 { get; set; }
            public string untd36 { get; set; }
            public string untd37 { get; set; }
            public string untd38 { get; set; }
            public string untd39 { get; set; }
            public string untd40 { get; set; }
            public string untd41 { get; set; }
            public string untd42 { get; set; }
            public string untd43 { get; set; }
            public string untd44 { get; set; }
            public string untd45 { get; set; }
            public string untd46 { get; set; }
            public string untd47 { get; set; }
            public string untd48 { get; set; }
            public string untd49 { get; set; }
            public string untd50 { get; set; }
            public string unts1 { get; set; }
            public string unts2 { get; set; }
            public string unts3 { get; set; }
            public string unts4 { get; set; }
            public string unts5 { get; set; }
            public string unts6 { get; set; }
            public string unts7 { get; set; }
            public string unts8 { get; set; }
            public string unts9 { get; set; }
            public string unts10 { get; set; }
            public string unts11 { get; set; }
            public string unts12 { get; set; }
            public string unts13 { get; set; }
            public string unts14 { get; set; }
            public string unts15 { get; set; }
            public string unts16 { get; set; }
            public string unts17 { get; set; }
            public string unts18 { get; set; }
            public string unts19 { get; set; }
            public string unts20 { get; set; }
            public string unts21 { get; set; }
            public string unts22 { get; set; }
            public string unts23 { get; set; }
            public string unts24 { get; set; }
            public string unts25 { get; set; }
            public string unts26 { get; set; }
            public string unts27 { get; set; }
            public string unts28 { get; set; }
            public string unts29 { get; set; }
            public string unts30 { get; set; }
            public string unts31 { get; set; }
            public string unts32 { get; set; }
            public string unts33 { get; set; }
            public string unts34 { get; set; }
            public string unts35 { get; set; }
            public string unts36 { get; set; }
            public string unts37 { get; set; }
            public string unts38 { get; set; }
            public string unts39 { get; set; }
            public string unts40 { get; set; }
            public string unts41 { get; set; }
            public string unts42 { get; set; }
            public string unts43 { get; set; }
            public string unts44 { get; set; }
            public string unts45 { get; set; }
            public string unts46 { get; set; }
            public string unts47 { get; set; }
            public string unts48 { get; set; }
            public string unts49 { get; set; }
            public string unts50 { get; set; }
            public string untsz1 { get; set; }
            public string untsz2 { get; set; }
            public string untsz3 { get; set; }
            public string untsz4 { get; set; }
            public string untsz5 { get; set; }
            public string untsz6 { get; set; }
            public string untsz7 { get; set; }
            public string untsz8 { get; set; }
            public string untsz9 { get; set; }
            public string untsz10 { get; set; }
           
        }

        [Serializable]
        public class CustomerImagePath
        {
            public string imgath { get; set; }


            public CustomerImagePath()
            { }

            public CustomerImagePath(string imgath)
            {
                this.imgath = imgath;


            }

        }

        [Serializable]
        public class EClassSaleRegisClearance
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string rescode { get; set; }
            public string gdesc { get; set; }
            public string unit { get; set; }
            public decimal usize { get; set; }
            public double urate { get; set; }
            public double amt { get; set; }

            public EClassSaleRegisClearance() { }







        }



        #region dummyschedule
        [Serializable]
        public class EClassDumPaSchdule
        {

            public string gcod { get; set; }
            public string gdesc { get; set; }
            public DateTime schdate { get; set; }
            public double schamt { get; set; }


            public EClassDumPaSchdule()
            { }

            public EClassDumPaSchdule(string gcod, string gdesc, DateTime schdate, double schamt)
            {
                this.gcod = gcod;
                this.gdesc = gdesc;
                this.schdate = schdate;
                this.schamt = schamt;


            }

        }

        [Serializable]
        public class EClassAcPaSchdule
        {
            public DateTime schdate { get; set; }
            public double schamt { get; set; }


            public EClassAcPaSchdule()
            { }

            public EClassAcPaSchdule(DateTime schdate, double schamt)
            {
                this.schdate = schdate;
                this.schamt = schamt;


            }

        }

        [Serializable]
        public class EClasActual
        {
            public string code { get; set; }
            public double amount { get; set; }
            public string gdesc { get; set; }


            public EClasActual()
            { }

            public EClasActual(string code, double amount, string gdesc)
            {
                this.code = code;
                this.amount = amount;
                this.gdesc = gdesc;
            }

        }

        [Serializable]
        public class EClasActual01
        {
            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string code { get; set; }
            public double charge { get; set; }
            public EClasActual01()
            { }

            public EClasActual01(string comcod, string pactcode, string usircode, string code, double charge)
            {
                this.comcod = comcod;
                this.pactcode = pactcode;
                this.usircode = usircode;
                this.code = code;
                this.charge = charge;
            }

        }


        [Serializable]
        public class EClasInterestCalculation
        {
            public string comcod { get; set; }
            public DateTime cinsdat { get; set; }
            public DateTime trdat { get; set; }
            public DateTime recndat { get; set; }
            public double day { get; set; }
            public double cinsam { get; set; }
            public double cumdue { get; set; }
            public double pamount { get; set; }
            public double cumpaid { get; set; }
            public double cumbalance { get; set; }
            public double interest { get; set; }
            public double cuminterest { get; set; }
            public double dueamt { get; set; }
           
            public EClasInterestCalculation()
            { }
        }

        [Serializable]
        public class EClassInterestDummyPay02
        {
            public string gdesc { get; set; }
            public string schdate { get; set; }

            public double cinsam { get; set; }

            public DateTime paiddate { get; set; }
           

            public double pamount { get; set; }
            public double balamt { get; set; }
            public double advday { get; set; }
            public double delday { get; set; }
            public double delamt { get; set; }
            public double disamt { get; set; }
            public double dodisday { get; set; }
            public double intrate { get; set; }
            public double intamtpday { get; set; }
            public double delodis { get; set; }


            public EClassInterestDummyPay02() { }
        }
        
       

        [Serializable]
        public class EClassClientSum
        {
            public string insno { get; set; }
            public string paidins { get; set; }

            public string duesinsno { get; set; }

            public double paidamt { get; set; }

            public double schamt { get; set; }
            public double insdueamt { get; set; }
            public double delamt { get; set; }
            public double disamt { get; set; }
            public double netdisdelamt { get; set; }
            public double netreceivable { get; set; }
            public EClassClientSum()
            {

            }
        }

        [Serializable]
        public class account
        {
            public string grp { get; set; }
            public string head { get; set; }
            public double amount { get; set; }
            public double peramt { get; set; }

            public account()
            {

            }
        }



        [Serializable]
        public class sales
        {
            public string gcod { get; set; }
            public string head { get; set; }
            public double amount { get; set; }
            public double peramt { get; set; }

            public sales()
            {

            }
        }

        //[Serializable]
        //public class purchase
        //{
        //    public string gcod { get; set; }
        //    public string head { get; set; }
        //    public double amount { get; set; }

        //    public purchase ( )
        //    {

        //    }
        //}

        //[Serializable]
        //public class construction
        //{
        //    public string gcod { get; set; }
        //    public string head { get; set; }
        //    public double amount { get; set; }

        //    public construction ( )
        //    {

        //    }
        //}

        //[Serializable]
        //public class bankbalance
        //{
        //    public string gcod { get; set; }
        //    public string head { get; set; }
        //    public double amount { get; set; }

        //    public bankbalance ( )
        //    {

        //    }
        //}

        //[Serializable]
        //public class stock
        //{
        //    public string gcod { get; set; }
        //    public string head { get; set; }
        //    public double amount { get; set; }

        //    public stock ( )
        //    {

        //    }
        //}
        //[Serializable]
        //public class duesoverdue
        //{
        //    public string gcod { get; set; }
        //    public string head { get; set; }
        //    public double amount { get; set; }

        //    public duesoverdue ( )
        //    {

        //    }
        //}
        //[Serializable]
        //public class penbil
        //{
        //    public string gcod { get; set; }
        //    public string head { get; set; }
        //    public double amount { get; set; }

        //    public penbil ( )
        //    {

        //    }
        //}


        //[Serializable]
        //public class ffund
        //{
        //    public string gcod { get; set; }
        //    public string head { get; set; }
        //    public double amount { get; set; }

        //    public ffund ( )
        //    {

        //    }
        //}

        //[Serializable]
        //public class fcost
        //{
        //    public string gcod { get; set; }
        //    public string head { get; set; }
        //    public double amount { get; set; }

        //    public fcost ( )
        //    {

        //    }
        //}

        //[Serializable]
        //public class conprogress
        //{
        //    public string gcod { get; set; }
        //    public string head { get; set; }
        //    public double amount { get; set; }

        //    public conprogress ( )
        //    {

        //    }
        //}


        //[Serializable]
        //public class fundcost
        //{
        //    public string gcod { get; set; }
        //    public string head { get; set; }
        //    public double amount { get; set; }

        //    public fundcost ( )
        //    {

        //    }
        //}

        [Serializable]
        public class monsale
        {
            public string comcod { get; set; }
            public string team { get; set; }
            public double uasaleamt { get; set; }
            public double msaleamt { get; set; }
            public double pmonsaleamt { get; set; }
            public double lstymonth { get; set; }

            public monsale()
            {

            }
        }


        [Serializable]
        public class yearsale
        {
            public string comcod { get; set; }
            public string team { get; set; }
            public double actty { get; set; }
            public double msaleamt { get; set; }
            public double actly { get; set; }


            public yearsale()
            {

            }
        }



        [Serializable]
        public class PrjSummary
        {
            public string mrptcode { get; set; }
            public string rptcode { get; set; }
            public string rptunit { get; set; }
            public double qty { get; set; }
            public double amt { get; set; }
            public double rate { get; set; }
            public string rptdesc { get; set; }
            //public string  colst { get; set; }
            //public double  peramt { get; set; }

            public PrjSummary()
            {

            }
        }

        [Serializable]
        public class dammySchedule
        {
            public string code { get; set; }
            public string gdesc { get; set; }
            public double amount { get; set; }
            public dammySchedule() { }
        }

        [Serializable]
        public class Eclassdummyschedule
        {
            public List<EClassSales_02.EClassDumPaSchdule> Lst01EClassDumPaSchdule { get; set; }
            public List<EClassSales_02.EClassAcPaSchdule> Lst02EClassAcPaSchdule { get; set; }
            public List<EClassSales_02.EClasActual> Lst04EClasActual { get; set; }
            public List<EClassSales_02.EClasActual01> Lst05EClasActual01 { get; set; }
            public List<EClassSales_02.EClasInterestCalculation> Lst06EClasInterestCalculation { get; set; }
            public List<EClassSales_02.dammySchedule> Lst03EClassSehedual { get; set; }
            public List<EClassSales_02.EClassInterestDummyPay02> Lst07EClassInterestDummyPay02 { get; set; }
            public Eclassdummyschedule() { }
        }


        #endregion

        #region
        [Serializable]
        public class RptBookingtDues
        {
            public string pactdesc { get; set; }
            public string pactcode { get; set; }
            public string custname { get; set; }
            public string udesc { get; set; }
            public double usize { get; set; }
            public double aptrate { get; set; }
            public double aptcost { get; set; }
            public double cpcost { get; set; }
            public double utltycost { get; set; }
            public double othcost { get; set; }
            public double tocost { get; set; }
            public double reconamt { get; set; }
            public double retcheque { get; set; }
            public double fcheque { get; set; }
            public double pcheque { get; set; }
            public double ramt { get; set; }
            public double pbookam { get; set; }
            public double cbookam { get; set; }
            public double tbdues { get; set; }


            public RptBookingtDues() { }

        }

        #region
        [Serializable]
        public class RptCalTValAvgVal
        {
            public string grp { get; set; }
            public string pactcode { get; set; }
            public string pactcode1 { get; set; }
            public string pactdesc { get; set; }
            public string usircode { get; set; }
            public string custname { get; set; }
            public string munit { get; set; }
            public string udesc { get; set; }
            public double usize { get; set; }
            public double urate { get; set; }
            public double uamt { get; set; }
            public double cparkam { get; set; }
            public double utlityam { get; set; }
            public double coprtvam { get; set; }
            public double otham { get; set; }
            public double totalval { get; set; }
            public RptCalTValAvgVal() { }

        }




        #endregion


        #region
        [Serializable]
        public class RptUnsoldUnit
        {
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string udesc { get; set; }
            public string munit { get; set; }
            public double usize { get; set; }
            public double urate { get; set; }
            public double uamt { get; set; }
            public double pamt { get; set; }
            public double utility { get; set; }
            public double cooperative { get; set; }
            public double usuamt { get; set; }
            public string facing { get; set; }
            public string uview { get; set; }
            public RptUnsoldUnit() { }

        }




        #endregion

        #region
        [Serializable]
        public class RptCollDetailsinfo
        {
            public string pactdesc { get; set; }
            public string custname { get; set; }
            public string udesc { get; set; }
            public double pbookam { get; set; }
            public double pinsam { get; set; }
            public double cbookam { get; set; }
            public double cinsam { get; set; }
            public double advcoll { get; set; }
            public double tocollam { get; set; }
            public double repchqamt { get; set; }



            public RptCollDetailsinfo() { }

        }
        #endregion
        #region
        [Serializable]
        public class RptSalSummery
        {

            public string deptcode { get; set; }
            public string mdeptcode { get; set; }
            public string mdeptname { get; set; }
            public string deptname { get; set; }
            public double msaleamt { get; set; }
            public double uasaleamt { get; set; }
            public double salsfall { get; set; }
            public double perontsale { get; set; }
            public double pmonsaleamt { get; set; }
            public double tsaleamt { get; set; }
            public double tasaleamt { get; set; }
            public double mcollamt { get; set; }
            public double uacollamt { get; set; }
            public double collsfall { get; set; }
            public double perontcoll { get; set; }
            public double tcollamt { get; set; }
            public double tacollamt { get; set; }
            public double pmoncollamt { get; set; }







            public RptSalSummery() { }

        }
        #endregion


        #region
        [Serializable]
        public class RptSalSummeryDetails
        {
            public string comcod { get; set; }

            public string pactcode { get; set; }

            public string usircode { get; set; }

            public string deptcode { get; set; }

            public string pactdesc { get; set; }
            public string custname { get; set; }
            public string mrno { get; set; }

            public string frmcode { get; set; }

            public string rectype { get; set; }
            public string rectypedesc { get; set; }
            public double acamt { get; set; }
            public double bankclear { get; set; }
            public double reconamt { get; set; }
            public double depchq { get; set; }
            public double inhrchq { get; set; }
            public double inhfchq { get; set; }
            public double inhpchq { get; set; }
            public double repchq { get; set; }

            public RptSalSummeryDetails() { }

        }
        #endregion



        #region
        [Serializable]
        public class RevenueStatus
        {

            public string grpdesc { get; set; }
            public string pactdesc { get; set; }
            public double tstkam { get; set; }
            public double ususize { get; set; }
            public double usamt { get; set; }
            public double percnt { get; set; }
            public double usize { get; set; }
            public double aptrate { get; set; }
            public double aptcost { get; set; }
            public double cpaocost { get; set; }
            public double tocost { get; set; }
            public double reconamt { get; set; }
            public double retcheque { get; set; }
            public double fcheque { get; set; }
            public double pcheque { get; set; }
            public double ramt { get; set; }
            public double atodues { get; set; }
            public double todues { get; set; }
            public double bamt { get; set; }
            public double pbookam { get; set; }
            public double pinsam { get; set; }
            public double cbookam { get; set; }
            public double cinsam { get; set; }
            public double ctodues { get; set; }
            public double vbamt { get; set; }
            public double cdelay { get; set; }
            public double ntodues { get; set; }
            public double discharge { get; set; }
            public double ptodues { get; set; }
            public double vtodues { get; set; }











            public RevenueStatus() { }

        }
        #endregion

        #endregion



        [Serializable]
        public class CustomerID
        {
            public string custcode { get; set; }



            public CustomerID()
            {



            }

            public CustomerID(string custcode)
            {

                this.custcode = custcode;

            }

        }

        //nahid
        #region

        [Serializable]
        public class EclassComInfo
        {
            public string comcod { get; set; }
            public string compname { get; set; }


            public EclassComInfo()
            { }

            public EclassComInfo(string comcod, string compname)
            {
                this.comcod = comcod;
                this.compname = compname;


            }

        }
        #endregion

        #region

        [Serializable]
        public class EclassPrjInfo
        {
            public string actcode { get; set; }
            public string actdesc { get; set; }


            public EclassPrjInfo()
            { }

            public EclassPrjInfo(string actcode, string actdesc)
            {
                this.actcode = actcode;
                this.actdesc = actdesc;


            }

        }
        #endregion

        #region

        [Serializable]
        public class EclassCustInfo
        {
            public string usircode { get; set; }
            public string custdesc { get; set; }
            public EclassCustInfo()
            { }
            public EclassCustInfo(string usircode, string custdesc)
            {
                this.usircode = usircode;
                this.custdesc = custdesc;
            }
        }
        #endregion

        #region

        [Serializable]
        public class EclassSunOfFlow
        {
            public string inflowid { get; set; }
            public string comcod { get; set; }
            public string actcode { get; set; }
            public string rescode { get; set; }
            public string acccomcod { get; set; }
            public string accactcode { get; set; }
            public string accrescode { get; set; }
            public double uamt { get; set; }
            public double paidamt { get; set; }
            public double recvbale { get; set; }
            public double accuamt { get; set; }
            public double accpaidamt { get; set; }
            public double accrecvbale { get; set; }
            public double ttlsolamt { get; set; }
            public double ttsolrecv { get; set; }
            public double ttldue { get; set; }
            public string remarks { get; set; }
            public string custdesc { get; set; }
            public string unitinf { get; set; }
            public double unitsize { get; set; }


            public EclassSunOfFlow()
            { }
            public EclassSunOfFlow(string inflowid, string comcod, string actcode, string rescode, string acccomcod, string accactcode, string accrescode,
                double uamt, double paidamt, double recvbale, double accuamt, double accpaidamt, double accrecvbale, double ttlsolamt,
                double ttsolrecv, double ttldue, string remarks, string custdesc, string unitinf, double unitsize)
            {

                this.inflowid = inflowid;
                this.comcod = comcod;
                this.actcode = actcode;
                this.rescode = rescode;
                this.acccomcod = acccomcod;
                this.accrescode = accrescode;
                this.uamt = uamt;
                this.paidamt = paidamt;
                this.recvbale = recvbale;
                this.accuamt = accuamt;
                this.accpaidamt = accpaidamt;
                this.accrecvbale = accrecvbale;
                this.ttlsolamt = ttlsolamt;
                this.ttsolrecv = ttsolrecv;
                this.ttldue = ttldue;
                this.remarks = remarks;
                this.custdesc = custdesc;
                this.unitinf = unitinf;
                this.unitsize = unitsize;
            }
        }
        #endregion

        #region

        [Serializable]
        public class AccCustPayLedger
        {
            //comcod	grp	pactcode			schdate	schamt	schamt1	gdesc	mrno	recdate	paiddate	
            //asondues	paidamt	balamt	bunamt	chqno	bankname	bbranch	recndate	mrdate1	bname	refno	rmrks	dischk

            public string comcod { get; set; }
            public string grp { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string gcod { get; set; }
            public string gdesc { get; set; }
            public DateTime schdate { get; set; }
            public double schamt { get; set; }
            public string mrno { get; set; }
            public string refno { get; set; }
            public DateTime recdate { get; set; }
            public string chqno { get; set; }
            public DateTime paiddate { get; set; }

            public string bankname { get; set; }
            public string bbranch { get; set; }

            public double paidamt { get; set; }

            public DateTime recndate { get; set; }
            public double bunamt { get; set; }
            public double balamt { get; set; }
            public string bname { get; set; }
            public string intdesc { get; set; }

            public AccCustPayLedger()
            { }

        }


        [Serializable]
        public class AccCustPayLedgerCHL
        {
            //comcod	grp	pactcode			schdate	schamt	schamt1	gdesc	mrno	recdate	paiddate	
            //asondues	paidamt	balamt	bunamt	chqno	bankname	bbranch	recndate	mrdate1	bname	refno	rmrks	dischk

            public string comcod { get; set; }
            public string grp { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string gcod { get; set; }
            public string gdesc { get; set; }
            public string schdate { get; set; }
            public double schamt { get; set; }
            public string mrno { get; set; }
            public string refno { get; set; }
            public string recdate { get; set; }
            public string chqno { get; set; }
            public string paiddate { get; set; }

            public string bankname { get; set; }
            public string bbranch { get; set; }

            public double paidamt { get; set; }

            public string recndate { get; set; }
            public double bunamt { get; set; }
            public double balamt { get; set; }
            public string bname { get; set; }
            public string intdesc { get; set; }

            public AccCustPayLedgerCHL()
            { }

        }
        #endregion

        [Serializable]
        public class RptMonWiseNewSales
        {
            public string rowid { get; set; }
            public string monthid1 { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public double usize { get; set; }
            public string munit { get; set; }
            public string udesc { get; set; }
            public string pactdesc { get; set; }
            public string custname { get; set; }
            public string schdate { get; set; }
            public double colamt { get; set; }
            public double tsalvalue { get; set; }
            public string teamlead { get; set; }

            public RptMonWiseNewSales() { }

        }


        [Serializable]
        public class RptSalPaySchedules
        {
            //grp, gdesc, gdesc1, munit, usize, urate, uamt,schamt, schdate,rmrks
            public string grp { get; set; }
            public string gdesc { get; set; }
            public string gdesc1 { get; set; }
            public string munit { get; set; }
            public double usize { get; set; }
            public double urate { get; set; }
            public double uamt { get; set; }
            public double schamt { get; set; }
            public DateTime schdate { get; set; }
            public string rmrks { get; set; }

            public RptSalPaySchedules() { }

        }

        [Serializable]
        
        public class Rptalloreport
        {
           
            public string gdesc { get; set; }
            public double schamt { get; set; }
            public DateTime schdate { get; set; }
             public Rptalloreport()
            {

            }


        }

    }

}
