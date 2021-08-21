using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_21_Mkt
{
  public  class ESourceWiseLeadsclass
    {
        [Serializable]
        public class CallCenterLeads
        {
          public string comcod { get; set; }
            public string gcod { get; set; }
            public string mgcod { get; set; }
            public string gdesc { get; set; }
            public string hmgdesc { get; set; }
            public double p1 { get; set; }
            public double p2 { get; set; }
            public double p3 { get; set; }
            public double p4 { get; set; }

            public double p5 { get; set; }

            public double p6 { get; set; }
            public double p7 { get; set; }

            public double total { get; set; }
        public  CallCenterLeads()
            {

            }

        }

        [Serializable]
        public class CallCenter
        {
            public string comcod { get; set; }
           
            public string gcod { get; set; }

            public string sourdesc { get; set; }
            public CallCenter()
            {

            }

        }
    }
}
