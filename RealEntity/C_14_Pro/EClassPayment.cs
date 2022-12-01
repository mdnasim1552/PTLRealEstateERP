using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_14_Pro
{
   public class EClassPayment
    {
       [Serializable]
       public class EClassDaily 
       {

           public string grp { get; set; }
           public string grpdesc { get; set; }
           public double day1 { get; set; }
           public double day2 { get; set; }
           public double day3 { get; set; }
           public double day4 { get; set; }
           public double day5 { get; set; }
           public double day6 { get; set; }
           public double day7 { get; set; }
           public double tobill { get; set; }

           public EClassDaily() 
           {
           }

           public EClassDaily(string grp, string grpdesc, double day1, double day2, double day3, double day4, double day5, double day6, double day7, double tobill) 
           {
               this.grp = grp;
               this.grpdesc = grpdesc;
               this.day1 = day1;
               this.day2 = day2;
               this.day3 = day3;
               this.day4 = day4;
               this.day5 = day5;
               this.day6 = day6;
               this.day7 = day7;
               this.tobill = tobill;
           
           }
       
       }

       [Serializable]
       public class EclassWeekly 
       {

           public string grp { get; set; }
           public string grpdesc { get; set; }
           public double week1 { get; set; }
           public double week2 { get; set; }
           public double week3 { get; set; }
           public double week4 { get; set; }
           public double tobill { get; set; }
        

           public EclassWeekly() 
           {
           }

           public EclassWeekly(string grp, string grpdesc, double week1, double week2, double week3, double week4, double tobill) 
           {
               this.grp = grp;
               this.grpdesc = grpdesc;
               this.week1 = week1;
               this.week2 = week2;
               this.week3 = week3;
               this.week4 = week4;
               this.tobill = tobill;
              
           
           }
       
       }

       [Serializable]
       public class EclassMonthly
       {

           public string grp { get; set; }
           public string grpdesc { get; set; }
           public double month1 { get; set; }
           public double month2 { get; set; }
           public double month3 { get; set; }
           public double monthab3 { get; set; }
           public double tobill { get; set; }


           public EclassMonthly()
           {
           }

           public EclassMonthly(string grp, string grpdesc, double month1, double month2, double month3, double monthab3, double tobill)
           {
               this.grp = grp;
               this.grpdesc = grpdesc;
               this.month1 = month1;
               this.month2 = month2;
               this.month3 = month3;
               this.monthab3 = monthab3;
               this.tobill = tobill;
             


           }

       }


       [Serializable]
       public class EclassCatWise
       {


           public double supam { get; set; }
           public double conam { get; set; }
           public double genam { get; set; }
        

           public EclassCatWise()
           {
           }

           public EclassCatWise(double supam, double conam, double genam)
           {
               this.supam = supam;
               this.conam = conam;
               this.genam = genam;
           }

       }
             
      [Serializable]
       public class EclassAccHead
       {


           public string actcode { get; set; }
           public string actdesc { get; set; }
           public double amt { get; set; }


           public EclassAccHead()
           {
           }

           public EclassAccHead(string actcode, string actdesc, double amt)
           {
               this.actcode = actcode;
               this.actdesc = actdesc;
               this.amt = amt;
           }

       }


      [Serializable]
      public class EclassPaywithPro
      {



          public string grp { get; set; }
          public string grpdesc { get; set; }
          public double billam { get; set; }
         


         
          public EclassPaywithPro()
          {
          }

          public EclassPaywithPro(string grp, string grpdesc, double billam)
          {

              this.grp = grp;
              this.grpdesc = grpdesc;
              this.billam = billam;
              
          }

      }
        [Serializable]
        public class EclassRptDateWiseBill
        {

            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string sirdesc { get; set; }
            public string billno { get; set; }
            public string billno1 { get; set; }
            public string ssircode { get; set; }
            public string billref { get; set; }
            public string vounum { get; set; }
            public DateTime billdat { get; set; }
            public DateTime chequedat { get; set; }
            public double billamt { get; set; }
            public double sdamt { get; set; }
            public double advamt { get; set; }
            public double dedamt { get; set; }
            public double netpayable { get; set; }
            public double tdeduction { get; set; }

            public EclassRptDateWiseBill()
            {
            }

        }
        [Serializable]
        public class EclassRptPOMRRBillStatus
        {

            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string sirdesc { get; set; }
        
            public string ssircode { get; set; }
            public double ordamt { get; set; }
            public double mrramt { get; set; }
            public double billamt { get; set; }


            public EclassRptPOMRRBillStatus()
            {
            }

        }

    }
}
