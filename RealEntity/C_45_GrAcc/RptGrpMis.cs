using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_45_GrAcc
{
    public class RptGrpMis
    {
        [Serializable]
        public class RptGrpRecPayment
        {
            //rpcode, grp1,  actcode, rpdesc, actcode1, actdesc, actdesc1, rescode, resdesc, totamt,amt01, amt02,
            //amt03, amt04, amt05, amt06, amt07, amt08, amt09, amt10,amt11, amt12, amt13, amt14, amt15, amt16, amt17, amt18 , amt19, amt20 
            public string rpcode { get; set; }
            public string grp1 { get; set; }
            public string actcode { get; set; }
            public string rpdesc { get; set; }
            public string actcode1 { get; set; }
            public string actdesc { get; set; }
            public string actdesc1 { get; set; }
            public string rescode { get; set; }
            public string resdesc { get; set; }
            public double totamt { get; set; }
            public double amt01 { get; set; }
            public double amt02 { get; set; }
            public double amt03 { get; set; }
            public double amt04 { get; set; }
            public double amt05 { get; set; }
            public double amt06 { get; set; }
            public double amt07 { get; set; }
            public double amt08 { get; set; }
            public double amt10 { get; set; }
            public double amt11 { get; set; }
            public double amt12 { get; set; }
            public double amt13 { get; set; }
            public double amt14 { get; set; }
            public double amt15 { get; set; }
            public double amt16 { get; set; }
            public double amt17 { get; set; }
            public double amt18 { get; set; }
            public double amt19 { get; set; }
            public double amt20 { get; set; }

        }

        [Serializable]
        public class RptGrpRecPaymentBank
        {
            //grp, actcode, opnam ,closam,netbal,chang,actdesc, grpdesc
            public string grp { get; set; }
            public string actcode { get; set; }
            public double opnam { get; set; }
            public double closam { get; set; }
            public double netbal { get; set; }
            public double chang { get; set; }
            public string actdesc { get; set; }
            public string grpdesc { get; set; }

        }

        [Serializable]
        public class RptAccRecPayment
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string rescode { get; set; }
            public string rescode1 { get; set; }
            public string resdesc { get; set; }
            public string dramt { get; set; }
            public string cramt { get; set; }
            public string dramt1 { get; set; }
            public string cramt1 { get; set; }

            public RptAccRecPayment()
            {

            }

        }
    }
}
