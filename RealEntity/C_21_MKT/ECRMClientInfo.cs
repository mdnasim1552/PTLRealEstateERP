﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_21_Mkt
{
   public class ECRMClientInfo
    {

        [Serializable]

        public class CrmClientInfo
        {
            public string sircode1 { get; set;}
            public string generated { get; set; }
            public string sirdesc { get; set; }
            public string appbydat { get; set; }
            public string prefdesc { get; set; }
            public string assoc { get; set; }
            public string teamdesc { get; set; }
            public string virnotes { get; set; }

            public CrmClientInfo() { }
        }

        [Serializable]
        public class EClassLeadReason

        {

            public string gcod { get; set; }
            public string gdesc { get; set; }

            public EClassLeadReason()
            { 
            
            
            }
        }


        [Serializable]
        public class EClassYearlySalesCRM

        {

            public string comcod { get; set; }
            public string empcode { get; set; }
            public string empname { get; set; }
            public double  qty1 { get; set; }
            public double  qty2 { get; set; }
            public double  qty3 { get; set; }
            public double  qty4 { get; set; }
            public double  qty5 { get; set; }
            public double  qty6 { get; set; }
            public double  qty7 { get; set; }
            public double  qty8 { get; set; }
            public double  qty9 { get; set; }
            public double  qty10 { get; set; }
            public double  qty11 { get; set; }
            public double  qty12 { get; set; }
            public double  tqty { get; set; }
           
            public EClassYearlySalesCRM()
            {


            }
        }

        [Serializable]
        public class RptProspectWorking
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string teamcode { get; set; }
            public string proscod { get; set; }
            public DateTime createdate { get; set; }
            public string assocname { get; set; }
            public string prospectname { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public string profession { get; set; }
            public string preaddress { get; set; }
            public string interestproj { get; set; }
            public string leadsrc { get; set; }
            public string ldiscuss { get; set; }
            public RptProspectWorking() { }
        }




    }
}
