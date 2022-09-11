using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_34_Mgt
{
    public class EClassEnventory
    {

        [Serializable]
        public class RequisationAdjust
        {
            public string comcod { get; set; }
            public string reqno1 { get; set; }
            public string mrfno { get; set; }
            public string reqdat1 { get; set; }
            public string resdesc { get; set; }
            public string resunit { get; set; }
            public double areqty { get; set; }
            public double mrrqty { get; set; }
            public double rqty { get; set; }
            public double adjstqty { get; set; }

            public string spcfdesc { get; set; }
            public RequisationAdjust()
            {

            }
        }
    }
}
