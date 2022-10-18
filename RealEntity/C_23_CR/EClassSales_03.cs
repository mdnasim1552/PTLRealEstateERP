using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RealEntity.C_23_CRR
{
    public  class EClassSales_03
    {
        [Serializable]
        public class Customer_Dues_info
        {     
            public string pactdesc { set; get; }
            public string custname { set; get; }
            public string custadd { set; get; }
            public string udesc { set; get; }
            public string cteam { set; get; }
            public string gdesc { set; get; }
            public string schdate { set; get; }
            public double dueins { set; get; }
            public double dueamt { set; get; }
            public double cdueamt { set; get; }
            public string mrno { set; get; }
            public string recdate { set; get; }
            public double paidamt { set; get; }

            public Customer_Dues_info() { }

          

            //public EClassVaT(string centrid, string centrdesc, string custid, string custdesc, string memono1, string vounum1, string memodat, double itmamt, double vat, double invdis)
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

            public EClassMonthly(string yearmon, string yearmon1, double ttlsalamt, double collamt)
            {
                this.yearmon = yearmon;
                this.yearmon1 = yearmon1;
                this.ttlsalamt = ttlsalamt;
                this.collamt = collamt;
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

            [Serializable]
        public class CustInovoce
        {
            public string gdesc { set; get; }
            public string schdate { set; get; }
            public double dueamt { set; get; }

         public CustInovoce() {}

            
        }
          [Serializable]
            public class Custaddress
            {
            public string udesc { set; get; }
            public string custadd { set; get; }
            public string custmobile { set; get; }
            public string prjadd { set; get; }
            public Custaddress() { }


            }

#endregion

   
        [Serializable]
        public class TrnStatInfo
        {
            //Iqbal Nayan
            public string comcod { get; set; }
            public string mrno { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public DateTime mrdate { get; set; }
            public string chqno { get; set; }
            public string bankname { get; set; }
            public string bbranch { get; set; }
            public double paidamt { get; set; }
            public string mstatus { get; set; }
            public string postrmid { get; set; }
            public string postedbyid { get; set; }
            public string postseson { get; set; }
            public string deletebyid { get; set; }
            public string deleteseson { get; set; }
            public string deletetrmid { get; set; }
            public DateTime deletedat { get; set; }
            public string unitname { get; set; }
            public string cname { get; set; }
            public string pname { get; set; }
            public string username { get; set; }
            public string deluser { get; set; }
            public TrnStatInfo() { }
        }


        //comcod, pactcode,  usircode, custname,  udesc, predues, curdues, receivable, dueins, pactdesc, mrno, recdate, recamt, netdues

        [Serializable]
        public class CustDewsOverDews
        {
            // Iqbal Nayan
            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string custname { get; set; }
            public string udesc { get; set; }
            public double predues { get; set; }
            public double curdues { get; set; }
            public double receivable { get; set; }
            public double dueins { get; set; }
            public string pactdesc { get; set; }
            public string mrno { get; set; }
            public string recdate { get; set; }
            public double recamt { get; set; }
            public double netdues { get; set; }
            public CustDewsOverDews() { }
        }

        //rowid, comcod, pactcode,  usircode, custname=convert(nvarchar(4),rowid) + '. '+custname,  udesc, mobile, usize,  aptcost, aptrate=(case when  usize=0 then 0 else aptcost/usize end), cpcost , 
        //utltycost, othcost, tocost, atodues=tocost-ramt,  todues,reconamt, retcheque, fcheque, pcheque,  ramt, bamt,    pbookam, pinsam, ptodues, cbookam, cinsam, 
        //ctodues, vtodues,  cdelay, discharge, abookam, ainsam, adtodues, nettodues=pbookam+pinsam +cbookam+cinsam+abookam+ainsam , ntodues=bamt+cdelay+discharge,  pactdesc
    
        
        [Serializable]
        public class EClassListOfReturnCheque
        {

            public string mrno { get; set; }
            public string mrdate { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string chqno { get; set; }
            public string chqdate { get; set; }
            public string dishdat { get; set; }
            public string bankname { get; set; }
            public double chqamt { get; set; }
            public string pactdesc { get; set; }
            public string udesc { get; set; }
            public string custname { get; set; }


            public EClassListOfReturnCheque() { }


        }

        [Serializable]
        public class EClassClientPayDetails
        {

            public string comcod { get; set; }
            public string grp { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string gcod { get; set; }
            public string schdate { get; set; }
            public double schamt { get; set; }
            public double schamt1 { get; set; }
            public string gdesc { get; set; }
            public string mrno { get; set; }
            public string recdate { get; set; }
            public string paiddate { get; set; }
            public double paidamt { get; set; }
            public double asondues { get; set; }
            public double balamt { get; set; }
            public double bunamt { get; set; }
            public string chqno { get; set; }
            public string bankname { get; set; }
            public string bbranch { get; set; }
            public string recndate { get; set; }
            public string mrdate1 { get; set; }
            public string bname { get; set; }
            public string refno { get; set; }
            public string bookno { get; set; }
            public string rmrks { get; set; }
            public string dischk { get; set; }


            public EClassClientPayDetails() { }




        }

        [Serializable]
        public class EClassCustomerDuesInfo
        {

            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string gcod { get; set; }
            public string custname { get; set; }
            public string custadd { get; set; }
            public string custmob { get; set; }
            public string steam { get; set; }
            public string cteam { get; set; }
            public string udesc { get; set; }
            public string gdesc { get; set; }
            public string salsdate { get; set; }
            public string schdate1 { get; set; }
            public string schdate { get; set; }
            public double uamt { get; set; }
            public double todues { get; set; }
            public double bamt { get; set; }
            public double dueins { get; set; }
            public double dueamt { get; set; }
            public double cdueamt { get; set; }
            public string pactdesc { get; set; }


            public EClassCustomerDuesInfo() { }


        }

        [Serializable]
        public class EClassDuesCollection
        {
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string pactcode { get; set; }
            public double usize { get; set; }
            public double aptcost { get; set; }
            public double aptrate { get; set; }
            public double cpcost { get; set; }
            public double utltycost { get; set; }
            public double othcost { get; set; }
            public double tocost { get; set; }
            public double todues { get; set; }
            public double atodues { get; set; }
            public double reconamt { get; set; }
            public double retcheque { get; set; }
            public double fcheque { get; set; }
            public double pcheque { get; set; }
            public double ramt { get; set; }
            public double bamt { get; set; }
            public double pbookam { get; set; }
            public double pinsam { get; set; }
            public double ptodues { get; set; }
            public double cbookam { get; set; }
            public double cinsam { get; set; }
            public double cdelay { get; set; }
            public double crdelay { get; set; }
            public double discharge { get; set; }
            public double ctodues { get; set; }
            public double vtodues { get; set; }
            public double ntodues { get; set; }
            public string pactdesc { get; set; }

            public EClassDuesCollection() { }


        }

        [Serializable]
        public class EClassDuesCollection02
        {
            public string comcod { get; set; }
            public string rowid { get; set; }
            public string usircode { get; set; }
            public string pactcode { get; set; }
            public string custname { get; set; }
            public string udesc { get; set; }
            public string mobile { get; set; }          
            public double usize { get; set; }
            public double aptcost { get; set; }
            public double aptrate { get; set; }
            public double cpcost { get; set; }
            public double utltycost { get; set; }
            public double othcost { get; set; }
            public double tocost { get; set; }
            public double todues { get; set; }
            public double atodues { get; set; }
            public double reconamt { get; set; }
            public double retcheque { get; set; }
            public double fcheque { get; set; }
            public double pcheque { get; set; }
            public double ramt { get; set; }
            public double bamt { get; set; }
            public double pbookam { get; set; }
            public double pinsam { get; set; }
            public double ptodues { get; set; }
            public double cbookam { get; set; }
            public double cinsam { get; set; }
            public double cdelay { get; set; }
            public double crdelay { get; set; }
            public double discharge { get; set; }
            public double ctodues { get; set; }
            public double vtodues { get; set; }
            public double ntodues { get; set; }

            public double abookam { get; set; }
            public double ainsam { get; set; }
            public double adtodues { get; set; }
            public double nettodues { get; set; }
            public string pactdesc { get; set; }

            public EClassDuesCollection02() { }


        }

        [Serializable]
        public class EClassSalesSatusReport
        {

            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string salpcode { get; set; }

            public double salesam { get; set; }
            public double scollam { get; set; }
            public double sreceivable { get; set; }
            public double associaam { get; set; }
            public double modcharge { get; set; }
            public double ocollam { get; set; }
            public double delcharge { get; set; }
            public double curschamt { get; set; }
            public string curschdate { get; set; }
            public double odues { get; set; }
            public double tsaleeam { get; set; }
            public double tcollam { get; set; }
            public double treceivable { get; set; }
            public double tomadcharge { get; set; }
            public double scham { get; set; }
            public double curcollam { get; set; }
            public double curocollam { get; set; } 
            public double tcurcollam { get; set; }
            public double uptocollam { get; set; }
            public double tpcreceivable { get; set; }
            public double lcollam { get; set; }
            public string lcolldate { get; set; }
            public double insdues { get; set; }
            public string salesdate { get; set; }
            public string salpcode1 { get; set; }
            public string custname { get; set; }
            public string udesc { get; set; }
            public string salpdesc { get; set; }
            public string aptst { get; set; }
            public string pactdesc { get; set; }


            public EClassSalesSatusReport() { }



        }


        [Serializable]
        public class RptMonthlyCollecionReceiptType
        {
            // Iqbal Nayan
            public string comcod { get; set; }
            public string mrdate { get; set; }
            public string mrdate1 { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string mrno { get; set; }
            public string chequeno { get; set; }
            public double r1 { get; set; }
            public double r2 { get; set; }
            public double r3 { get; set; }
            public double r4 { get; set; }
            public double r5 { get; set; }
            public double r6 { get; set; }
            public double r7 { get; set; }

            public double r8 { get; set; }
            public double r9 { get; set; }
            public double r10 { get; set; }
            public double r11 { get; set; }
            public double r12 { get; set; }
            public double r13 { get; set; }
            public double r14 { get; set; }
            public double r15 { get; set; }   
            public double tocoll { get; set; }
            public string pactdesc { get; set; }
            public string custname { get; set; }
            public string udesc { get; set; }   
            public RptMonthlyCollecionReceiptType() { }
        }


        [Serializable]
        public class EClassClientInfos
        {
            // name, spname, proname, unit, carprk, mailaddr, preaddr, phone, email, steam, birth, marr

            public string name { get; set; }
            public string spname { get; set; }
            public string proname { get; set; }
            public string unit { get; set; }
            public string carprk { get; set; }
            public string mailaddr { get; set; }
            public string preaddr { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public string steam { get; set; }

            public string birth { get; set; }
            public string marr { get; set; }

            public EClassClientInfos() { }
        }



         [Serializable]
        public class EClassEarlyBenifit
        {
            
            public string gcod { get; set; }
            public string dayid { get; set; }
            public DateTime schdate { get; set; }
            public string paiddate { get; set; }
            public DateTime paiddate1 { get; set; }
            public double schamt { get; set; }
            public double paidamt { get; set; }
            public double eday { get; set; }
            public double inrate { get; set; }
            public double inam { get; set; }
            public string gdesc { get; set; }

            public EClassEarlyBenifit() { }
        }


         [Serializable]
         public class ProjectWiseClientStatus
         {
             public string uunitname { get; set; }
             public double unusize { get; set; }
             public string sunitname { get; set; }
             public string register { get; set; }
             public string custname { get; set; }
             public double usize { get; set; }
             public double tsize { get; set; }
             public double tocost { get; set; }
             public double reconamt { get; set; }
             public double atodues { get; set; }
             public double cdelay { get; set; }
             public double ntodues { get; set; }
             public double stdamt { get; set; }
             public string salteam { get; set; } 
             public string pactdesc { get; set; }  
             
             public ProjectWiseClientStatus() { }
         }

        public class RptUtilityAndOtherCollection
        {
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string pactdesc { get; set; }
            public string custname { get; set; }
            public string udesc { get; set; }
            public double reciptreg { get; set; }
            public double paidregamt { get; set; }
            public double regisbal { get; set; }
            public double reciptadw { get; set; }
            public double paidadw { get; set; }
            public double adwbal { get; set; }
            public double reciptassocia { get; set; }
            public double paidassocia { get; set; }
            public double associabal { get; set; }
            public double reciptutility { get; set; }
            public double paidutility { get; set; }
            public double utilityabal { get; set; }  
            public double reciptsociety { get; set; }
            public double paidsociety { get; set; }
            public double societybal { get; set; }
            public double reciptservice { get; set; }
            public double paidservice { get; set; }
            public double servicebal { get; set; }
            public double reciptmutation { get; set; }
            public double paidmutation { get; set; }
            public double mutationbal { get; set; }
            public double reciptoptional { get; set; }
            public double paidoptional { get; set; }
            public double optionalbal { get; set; }
            public double reciptdelay { get; set; }
            public double paiddelay { get; set; }
            public double delaybal { get; set; }
            public double reciptamt { get; set; }
            public double paidamt { get; set; }
            public double balance { get; set; }

            public RptUtilityAndOtherCollection() { }
        }

    }
}
