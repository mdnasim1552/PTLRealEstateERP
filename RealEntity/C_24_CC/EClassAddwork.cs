using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_24_CC
{
    public class EClassAddwork
    {
        [Serializable]
        //       select mgcod,mgdesc, gcod, gdesc,  rate=abs(rate),comqty, crate=abs(crate), comlrate=abs(comlrate),comamt=abs(comamt),qty, clrate=abs(clrate), cllrate=abs(cllrate),
        //clamt=abs(clamt),  amt=amt, disamt=abs(disamt), netamt=netamt, shcod, unit, wrkdesc, location   
        //from #tbladwrk order by 1
        public class AddWorkCus
        {
            public string mgcod { get; set; }
            public string mgdesc { get; set; }
            public string gcod { get; set; }
            public string gdesc { get; set; }
            public double rate { get; set; }
            public double comqty { get; set; }
            public double crate { get; set; }
            public double comlrate { get; set; }
            public double comamt { get; set; }
            public double qty { get; set; }
            public double clrate { get; set; }
            public double cllrate { get; set; }
            public double clamt { get; set; }
            public double amt { get; set; }
            public double disamt { get; set; }
            public double netamt { get; set; }
            public string shcod { get; set; }
            public string unit { get; set; }
            public string wrkdesc { get; set; }
            public string location { get; set; }
            public double nrefund { get; set; }
            public double ndemand { get; set; }
            public AddWorkCus() { }
        }

        //       a.comcod,  a.gcod,  demamt=(case when a.amt>0 then a.amt else 0.00 end), 
        //refamt=(case when a.amt<0 then a.amt*-1 else 0.00 end), disamt, netamt, gdesc=b.gdesc
        [Serializable]
        public class AddTopSheet
        {
            public string comcod { get; set; }
            public string gcod { get; set; }
            public double demamt { get; set; }
            public double refamt { get; set; }
            public double disamt { get; set; }
            public double netamt { get; set; }
            public string gdesc { get; set; }
            public int level { get; set; }

            public AddTopSheet()
            {}
        }

        [Serializable]
        public class LoanStatus
        {
            public string unitdesc { get; set; }
            public string custname { get; set; }
            public string lnprovdr { get; set; }
            public double lnamt { get; set; }
            public DateTime rcvdate { get; set; }
            public DateTime regdate { get; set; }
            public LoanStatus() { }
        }

        [Serializable]
        public class HandOverLetter
        {
            public string mgcod { get; set; }
            public string gcod { get; set; }
            public string compcod { get; set; }
            public string comdesc { get; set; }
            public DateTime compdat { get; set; }
            public string mgdesc { get; set; }
            public string gdesc { get; set; }
            public string complete { get; set; }
            public string incomplete { get; set; }
            public string signowner { get; set; }
            public HandOverLetter() { }
        }

        [Serializable]
        public class CancellationAddWork
        {
            public string adno { get; set; }
            public DateTime addate { get; set; }
            public string actdesc { get; set; }
            public string sirdesc { get; set; }
            public string gdatat { get; set; }
            public int qty { get; set; }
            public string reason { get; set; }
            public double amt { get; set; }
            public CancellationAddWork() { }
        }

    }
}
