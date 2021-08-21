using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_17_Acc
{
    public class EClassFinanStatement
    {
        [Serializable]

        public class IncomeStatementSHE
        {
            public string actdcode { get; set; }
            public string actdesc { get; set; }
            public double opnam { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double closam { get; set; }
           
        }

        [Serializable]
        public class CashFlowIndirect
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public double curam { get; set; }
            public double opnam { get; set; }
            public double closam { get; set; }
            public double changeam { get; set; }



        }

        //comcod,monthid,monthid1, actcode,opnam, trndr,trncr,clsamt 
        [Serializable]
        public class MonthWiseBankLedger
        {
            public string comcod { get; set; }
            public DateTime monthid { get; set; }
            public string monthid1 { get; set; }
            public string actcode { get; set; }
            public double opnam { get; set; }
            public double trndr { get; set; }
            public double trncr { get; set; }
            public double clsamt { get; set; }

            public MonthWiseBankLedger() { }


        }
        /// rowid, comcod,  actcode4, rescode4 ,	rescode, opndram, opncram,   opnam, trnam, dram, cram,  closam, actdesc4, resdesc

        [Serializable]
        public class DetailsScheduleTB
        {
            public int rowid { get; set; }
            public string comcod { get; set; }
            public string actcode4 { get; set; }
            public string rescode4 { get; set; }
            public string rescode { get; set; }

            public double opndram { get; set; }
            public double opncram { get; set; }
            public double opnam { get; set; }
            public double trnam { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double closam { get; set; }
            public string actdesc4 { get; set; }
            public string resdesc { get; set; }

            public DetailsScheduleTB() { }


        }

        //comcod, actcode, rescode, actdesc, resdesc,resunit ,qty, opnam,dram,cram ,clsamt,rate

        [Serializable]
        public class PrjWiseMaterialCost
        {
            public string comcod { get; set; }
            public string actcode { get; set; }
            public string rescode { get; set; }
            public string actdesc { get; set; }
            public string resdesc { get; set; }
            public string resunit { get; set; }

            public double qty { get; set; }
            public double opnam { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double clsamt { get; set; }
            public double rate { get; set; }
            

            public PrjWiseMaterialCost() { }


        }
    }
}
