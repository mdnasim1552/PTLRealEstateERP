using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_16_Bill
{
    public class BO_BillEntry
    {
        [Serializable]
        public class BillEmtry
        {
            //Iqbal Nayan
            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string isircode { get; set; }
            public string flrcod { get; set; }
            public string flrdes { get; set; }
            public string unit { get; set; }
            public string mbnumber { get; set; }
            public string pagenumber { get; set; }
            public string isirdesc { get; set; }
            public string sdetails { get; set; }
            public double bgdqty { get; set; }
            public double exeqty { get; set; }
            public double prebqty { get; set; }
            public double balqty { get; set; }
            public double proqty { get; set; }
            public double sysqty { get; set; }
            public double billqty { get; set; }
            public double exosqty { get; set; }
            public double bgdam { get; set; }
            public double ordrate { get; set; }
            public double ordam { get; set; }
            public double prebam { get; set; }
            public double billam { get; set; }
            public double balam { get; set; }

            //Added By Nime 

            public string misircode { get; set; }
            public string misirdesc { get; set; }   

            public double percnt { get; set; }
            public BillEmtry() { }
        }
        [Serializable]
        public class BillingRateEntry
        {
           /// iqbal Nayan
            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string isircode { get; set; }
            public string flrcod { get; set; }
            public string flrdes { get; set; }
            public string unit { get; set; }
            public double qty { get; set; }
            public double bgdqty { get; set; }
            public string isirdesc { get; set; }
            public double bgdrate { get; set; }
            public double rate { get; set; }
            public double bgdam { get; set; }
            public double ordam { get; set; }
            public double perobgd { get; set; }
            public string sdetails { get; set; }
            public BillingRateEntry() { }
        }
   
        [Serializable]
        public class ProjectStarus
        {
            // Iqbal Nayan
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string isircode { get; set; }
            public string flrcod { get; set; }
            public string flrdes { get; set; }
            public string unit { get; set; }
            public string isirdesc { get; set; }
            public string sdetails { get; set; }
            public double bgdqty { get; set; }
            public double bgdrat { get; set; }
            public double preqty { get; set; }
            public double curqty { get; set; }
            public double tbillqty { get; set; }
            public double balqty { get; set; }
            public double curam { get; set; }
            public double tbillam { get; set; }
            public double bgdam { get; set; }
            public double ordrate { get; set; }
            public double ordam { get; set; }
            public double pream { get; set; }
            public double balam { get; set; }
            public ProjectStarus() { }
        }

        //comcod, majbook, majpnumber, isircode, flrcod, conrate, conqty, conuqty,conamt, conuamt, upconqty, upconuqty, uprate,
        //upconamt, upconuamt, difupacon=upconuqty-conuqty, isirdesc, isirunit, flrdes, sdetails
        [Serializable]
        public class UpcomSubCon
        {
            // Iqbal Nayan
            public string comcod { get; set; }
            public string majbook { get; set; }
            public string majpnumber { get; set; }
            public string isircode { get; set; }
            public string flrcod { get; set; }
            public double conrate { get; set; }
            public double conqty { get; set; }
            public double conuqty { get; set; }
            public double conamt { get; set; }
            public double conuamt { get; set; }
            public double upconqty { get; set; }
            public double upconuqty { get; set; }
            public double uprate { get; set; }
            public double upconamt { get; set; }
            public double upconuamt { get; set; }
            public double difupacon { get; set; }
            public string isirdesc { get; set; }
            public string isirunit { get; set; }
            public string flrdes { get; set; }
            public string sdetails { get; set; }
            public UpcomSubCon() { }
        }

    }
}
