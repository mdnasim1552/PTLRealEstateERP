using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_28_Mpro
{
    public class EClassMktProcurement
    {
        [Serializable]
        public class RptMktPurchaseRequisition
        {
            public string prtype { get; set; }
            public string acttype { get; set; }
            public string mkttype { get; set; }
            public string prdesc { get; set; }
            public string actdesc { get; set; }
            public string mktdesc { get; set; }
            public double bgdqty { get; set; }
            public double bgdrat { get; set; }
            public double treceived { get; set; }
            public double bbgdqty { get; set; }
            public double bbgdamt { get; set; }
            public double bbgdqty1 { get; set; }
            public double bbgdamt1 { get; set; }
            public double stkqty { get; set; }
            public double preqty { get; set; }
            public double areqty { get; set; }
            public double reqrat { get; set; }
            public double preqamt { get; set; }
            public double areqamt { get; set; }
            public double chqty { get; set; }
            public string expusedt { get; set; }
            public string reqnote { get; set; }
            public double lpurrate { get; set; }
            public string ssircode { get; set; }
            public string ssirdesc { get; set; }
            public string orderno { get; set; }
            public string justific { get; set; }

            public RptMktPurchaseRequisition() { }

        }

        [Serializable]
        public class RptMktPurchaseTrack
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string genno { get; set; }
            public string gdate { get; set; }
            public string refno { get; set; }
            public string chlno { get; set; }
            public string prtypedesc { get; set; }
            public string acttypedesc { get; set; }
            public string mkttypedesc { get; set; }
            public double qty { get; set; }
            public double rate { get; set; }
            public double amt { get; set; }
            public string ssirdesc { get; set; }
            public string usrname { get; set; }
            public RptMktPurchaseTrack() { }
        }
    }
}
