using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_16_Bill
{
   public class EClassBilling
    {

       [Serializable]

       public class EClassUpconVsSubCon
       {

       
        public string majbook{get;set;}
        public string majpnumber{get;set;}
        public string isircode{get;set;}
        public string flrcod{get;set;}
        public string isirdesc{get;set;}
        public string isirunit{get;set;}
        public string flrdes{get;set;}
        public string sdetails{get;set;}
        public double  conrate{get;set;}
        public double  conqty{get;set;}
        public double  conuqty{get;set;}
        public double  conamt{get;set;}
        public double  conuamt{get;set;}
        public double  upconqty{get;set;}
        public double  upconuqty{get;set;}
        public double  uprate{get;set;}
        public double  upconamt{get;set;}
        public double  upconuamt{get;set;}
        public double  difupacon{get;set;}

           public EClassUpconVsSubCon()
           {
           
           }

           public EClassUpconVsSubCon(string majbook, string majpnumber, string isircode, string flrcod, string isirdesc, string isirunit, string flrdes, string sdetails, double conrate, double conqty, double conuqty, double conamt, double conuamt, double upconqty, double upconuqty, double uprate, double upconamt, double upconuamt, double difupacon)
           {

               this.majbook = majbook;
               this.majpnumber = majpnumber;
               this.isircode = isircode;
               this.flrcod = flrcod;
               this.isirdesc = isirdesc;
               this.isirunit = isirunit;
               this.flrdes = flrdes;
               this.sdetails = sdetails;
               this.conrate = conrate;
               this.conqty = conqty;
               this.conuqty = conuqty;
               this.conamt = conamt;
               this.conuamt = conuamt;
               this.upconqty=upconqty;
               this.upconuqty=upconuqty;
               this.uprate=uprate;
               this.upconamt=upconamt;
               this.upconuamt=upconuamt;
               this.difupacon=difupacon;
           
           
           }
	

       
       
       
       }

    }
}
