using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_23_CRR
{
  public  class EClassSalesStatus
   
  {
      [Serializable]
      public class SalesStatus 
      {
      
          public double salesam{get;set;}
          public double scollam{get;set;}
          public double salesbalam{get;set;}
          public double utsalesam{get;set;}
          public double utcollam{get;set;}
          public double utbalam{get;set;}
          public double modsalesam{get;set;}
          public double modcollam{get;set;}
          public double modbalam{get;set;}
          public double opsalesam{get;set;}
          public double opcollam{get;set;}
          public double opbalam{get;set;}
          public double trnsalesam{get;set;}
          public double trncollam{get;set;}
          public double trnbalam{get;set;}
          public double regsalesam{get;set;}
          public double regcollam{get;set;}
          public double regbalam{get;set;}
          public double asosalesam{get;set;}
          public double asocollam{get;set;}
          public double asobalam{get;set;}
          public double mutsalesam{get;set;}
          public double mutcollam{get;set;}         
          public double mutbalam{get;set;}


          public double upsalesam { get; set; }
          public double upcollam { get; set; }
          public double upbalam { get; set; }
          public string salesdate{get;set;}
          public string custname{get;set;}
          public string udesc{get;set;}
          public string pactdesc { get; set; }
          public string pactcode { get; set; }
          public string usircode { get; set; }

          public SalesStatus() 
          {
          }

          public SalesStatus(double salesam, double scollam, double salesbalam, double utsalesam, double utcollam, double utbalam, double modsalesam, double modcollam, double modbalam, double opsalesam, double opcollam, double opbalam, double trnsalesam, double trncollam, double trnbalam, double regsalesam, double regcollam, double regbalam, double asosalesam, double asocollam, double asobalam, double mutsalesam, double mutcollam, double mutbalam, double upsalesam, double upcollam, double upbalam,string salesdate, string custname, string udesc, string pactdesc, string pactcode, string usircode) 
          {
              this.salesam = salesam;
              this.scollam = scollam;
              this.salesbalam = salesbalam;
              this.utsalesam = utsalesam;
              this.utcollam = utcollam;
              this.utbalam = utbalam;
              this.modsalesam = modsalesam;
              this.modcollam = modcollam;
              this.modbalam = modbalam;
              this.opsalesam = opsalesam;
              this.opcollam = opcollam;
              this.opbalam = opbalam;
              this.trnsalesam = trnsalesam;
              this.trncollam = trncollam;
              this.trnbalam = trnbalam;
              this.regsalesam = regsalesam;
              this.regcollam = regcollam;
              this.regbalam = regbalam;
              this.asosalesam = asosalesam;
              this.asocollam = asocollam;
              this.asobalam = asobalam;
              this.mutsalesam = mutsalesam;
              this.mutcollam = mutcollam;
              this.mutbalam = mutbalam;

              this.upsalesam = upsalesam;
              this.upcollam = upcollam;
              this.upbalam = upbalam;
              this.salesdate = salesdate;
              this.custname = custname;
              this.udesc = udesc;
              this.pactdesc = pactdesc;
              this.pactcode = pactcode;
              this.usircode = usircode;
          
          }
      
      
      }
     //  a.comcod, a.gcod, a.receivable, a.receipt, a.duebal, gdesc
      [Serializable]
      public class Resreceivable
      {
          //IQBAL NAYAN
          public string comcod { get; set; }
          public string gcod { get; set; }
          public double receivable { get; set; }
          public double receipt { get; set; }
          public double duebal { get; set; }
          public string gdesc { get; set; }
          public Resreceivable() { }
      }

     


      [Serializable]
      public  class EClassDuesAOverDues
      {
          public string pactcode{get;set;}
          public string pactdesc{get;set;}
          public double tstkam{get;set;}
          public double ususize{get;set;}
          public double usamt{get;set;}
          public double percnt{get;set;}
          public double usize{get;set;}
          public double aptcost{get;set;}
          public double aptrate{get;set;}
          public double utltycost{get;set;}
          public double othcost{get;set;}
          public double cpaocost{get;set;}
          public double tocost{get;set;}
          public double todues{get;set;}
          public double atodues{get;set;}
          public double reconamt{get;set;}
          public double retcheque{get;set;}
          public double fcheque{get;set;}
          public double ramt{get;set;}
          public double bamt{get;set;}
          public double pbookam{get;set;}
          public double pinsam{get;set;}
          public double ptodues{get;set;}
          public double cbookam{get;set;}
          public double cinsam{get;set;}
          public double ctodues{get;set;}
          public double cdelay{get;set;}
          public double discharge{get;set;}
          public double ntodues{get;set;}
          public double balamt { get; set; }

            public EClassDuesAOverDues() 
          {
          
          
          }
        


      
      }




      [Serializable]
      public class EClassDuesAOverDuesIndPro
      {
          public string pactcode { get; set; }
          public string pactdesc { get; set; }
          public string usircode { get; set; }
          public string custname { get; set; }
          public string udesc { get; set; }
          public double tstkam { get; set; }
          public double ususize { get; set; }
          public double usamt { get; set; }
          public double percnt { get; set; }
          public double usize { get; set; }
          public double aptcost { get; set; }
          public double aptrate { get; set; }
          public double utltycost { get; set; }
          public double othcost { get; set; }
          public double cpaocost { get; set; }
          public double tocost { get; set; }
          public double todues { get; set; }
          public double vtodues { get; set; }
          public double atodues { get; set; }
          public double reconamt { get; set; }
          public double retcheque { get; set; }
          public double fcheque { get; set; }
          public double ramt { get; set; }
          public double bamt { get; set; }
          public double pbookam { get; set; }
          public double pinsam { get; set; }
          public double ptodues { get; set; }
          public double cbookam { get; set; }
          public double cinsam { get; set; }
          public double ctodues { get; set; }
          public double cdelay { get; set; }
          public double discharge { get; set; }
          public double ntodues { get; set; }
          public double balamt { get; set; }

            public EClassDuesAOverDuesIndPro()
          {


          }




      }
     









     [Serializable]

      public class EClassClientLedger
     {

     public string grp { get; set; }
     public string mrno { get; set; }
     public string pactcode { get; set; }
     public string usircode{get;set;}
     public string schdate{get;set;}
     public double schamt{get;set;}
     public double schamt1{get;set;}
     public string  gdesc{get;set;}
     public string  recdate{get;set;}
     public string  paiddate{get;set;}
     public double   paidamt{get;set;}
     public double   asondues{get;set;}
     public double   balamt{get;set;}
     public double   bunamt{get;set;}
     public string   chqno{get;set;}
     public string   bankname{get;set;}
     public string   bbranch{get;set;}
     public string   recndate{get;set;}
     public  DateTime mrdate1{get;set;}
     public string   bname{get;set;}
     public string   refno{get;set;}
     public string   bookno{get;set;}
     public string   rmrks{get;set;}



     public EClassClientLedger()
     {


     }
     }

     
     [Serializable]
      public class EClassYearlyColletionForcasting
      {
          public string comcod { get; set; }
          public string pactcode { get; set; }
          public double bgdcost { get; set; }
          public double tocost { get; set; }
          public double ramt { get; set; }
          public double bamt { get; set; }
          public double pdueam { get; set; }
          public double dueam1 { get; set; }
          public double dueam2 { get; set; }
          public double dueam3{ get; set; }
          public double dueam4 { get; set; }
          public double dueam5 { get; set; }
          public double dueam6 { get; set; }
          public double dueam7 { get; set; }
          public double dueam8 { get; set; }
          public double dueam9 { get; set; }
          public double dueam10 { get; set; }
          public double dueam11 { get; set; }
          public double dueam12  { get; set; }
          public double todueam { get; set; }
          public double gtodueam { get; set; }
          public string pactdesc { get; set; }

          public EClassYearlyColletionForcasting()
          {

          }



      }



     [Serializable]
     public class EClassMonthlyCollectionSchedule   
     {
         public string comcod { get; set; }
         public string cdate { get; set; }
         public string cdate1 { get; set; }
         public string usircode { get; set; }
         public string udesc { get; set; }
         public string custname { get; set; }
         public double p1 { get; set; }
         public double p2 { get; set; }
         public double p3 { get; set; }
         public double p4 { get; set; }
         public double p5 { get; set; }
         public double p6 { get; set; }
         public double p7 { get; set; }
         public double p8 { get; set; }
         public double p9 { get; set; }
         public double p10 { get; set; }
         public double p11 { get; set; }
         public double p12 { get; set; }
         public double p13 { get; set; }
         public double p14 { get; set; }
         public double p15 { get; set; }
         public double p16 { get; set; }
         public double p17 { get; set; }
         public double p18 { get; set; }
         public double p19 { get; set; }
         public double p20 { get; set; }
         public double todues { get; set; }
         public EClassMonthlyCollectionSchedule()
         {

         }
     }

     [Serializable]
     public class EClassMonthlyCollectionScheduleProject   
     {
         public string comcod { get; set; }
         public string actcode { get; set; }
         public string pactdesc { get; set; }

         public EClassMonthlyCollectionScheduleProject()
         {

         }

     }

     [Serializable]
     public class ClientPaymentStatus
     {
         public string gdesc { get; set; }
         public string schdate { get; set; }
         public double schamt { get; set; }
         public string paiddate { get; set; }
         public double paidamt { get; set; }
         public ClientPaymentStatus() { }
     }
      
      [Serializable]

    
      public class PaymentStatus
     {
         public string grp { get; set; }
         public string gcod { get; set; }
         public string gdesc { get; set; }
         public string mrno { get; set; }
         public string recdate { get; set; }
         public double exessamt { get; set; }
         public double balamt { get; set; }
         public string chqno { get; set; }
         public string bankname { get; set; }
         public string bbranch { get; set; }
         public string refno { get; set; }
         public string bookno { get; set; }
         public string schdate { get; set; }
         public double schamt { get; set; }
         public string paiddate { get; set; }
         public double paidamt { get; set; }
         public double advamt { get; set; }
            public PaymentStatus() { }
     }


     [Serializable]
     public class EClassRevenue
     {
         public string gcod { get; set; }
         public string gdesc { get; set; }
         public string munit { get; set; }
         public double usize { get; set; }
         public double uamt { get; set; }
         public EClassRevenue() { }
     }

     [Serializable]
     public class MonCollScheSummmay
     {
         public string pactdesc { get; set; }
         public string pactdescbn { get; set; }
         public double schamt { get; set; }
         public string schamtbn { get; set; }
         public MonCollScheSummmay() { }
     }

     [Serializable]
      public class MonthlyColScheduleDet
      {
          public string rowid1 { get; set; }
          public string pactcode { get; set; }
          public string usircode { get; set; }
          public string pactdesc { get; set; }
          public string pactdescbn { get; set; }
          public string custname { get; set; }
          public string custnamebn { get; set; }
          public string udesc { get; set; }
          public string udescbn { get; set; }
          public string salesdate { get; set; }
          public double usize { get; set; }
          public double aptamt { get; set; }
          public double cpamt { get; set; }
          public double utlaassoamt { get; set; }
          public double modiamt { get; set; }
          public double othamt { get; set; }
          public double tsalamt { get; set; }
          public double rcvamt { get; set; }
          public double balamt { get; set; }
          public double curdues { get; set; }
          public string curduesdate { get; set; }
          public string curduesdatebn { get; set; }
          public string salesdatebn { get; set; }
         public MonthlyColScheduleDet(){}

      }

     [Serializable]
     public class ProjWiseColSummaryDetails
     {
         //a.comcod, a.pactcode,prjmcode ,pactdesc,a.prjcode,gdesc, a.mramt 
         public string comcod { get; set; }

         public string pactcode { get; set; }
         public string prjmcode { get; set; }
         public string pactdesc { get; set; }
         public string prjcode { get; set; }
         public string gdesc { get; set; }
         public double mramt { get; set; }
         public ProjWiseColSummaryDetails() { }
     }

   }
}
