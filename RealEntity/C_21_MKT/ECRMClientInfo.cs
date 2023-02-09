using System;
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
            public DateTime generatedate { get; set; }
            public DateTime followupdate { get; set; }
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


        [Serializable]
        public class RptProspectTransfer
        {
            //comcod, grp, grpdesc,teamcode, proscod, createdate, assocname, prospectname, phone, email, profession, preaddress, interestproj, leadsrc, chkper
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
            public RptProspectTransfer() { }
        }

        [Serializable]
        public class RptLeadStatusTimestamp
        {
            public string proscod { get; set; }
            public string proscod1 { get; set; }
            public string teamleader { get; set; }
            public string sourcecode { get; set; }
            public string source { get; set; }
            public DateTime createdate { get; set; }
            public string query { get; set; }
            public string lead { get; set; }
            public string qualifiedlead { get; set; }
            public string negotiation { get; set; }
            public string finalnegotiation { get; set; }
            public string win { get; set; }
            public string lstatus { get; set; }
            public RptLeadStatusTimestamp() { }
        }


            [Serializable]
        public class EClassDailyWorkStatus
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string teamcode { get; set; }
            public string teamdesc { get; set; }
            public string sircode { get; set; }
            public string sircode1 { get; set; }
            public string sirdesc { get; set; }
            public DateTime generated { get; set; }
            public DateTime nfollowupcdate { get; set; }
            public DateTime nfollowupdate { get; set; }
            public string followup { get; set; }
            public string followupdesc { get; set; }
            public string statuslead { get; set; }
            public string statusleadp { get; set; }
            public EClassDailyWorkStatus() { }
        }

    }
}
