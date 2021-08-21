using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_17_Acc
{
  public  class EclassPendingBill
    {
        [Serializable]
        public class ClassPendingBil
        {
            public string pactcode { get; set; }
            public string ssircode { get; set; }
            public string ssirdesc { set; get; }
            public string pactdesc { set; get; }
            public string billno1 { set; get; }
            public DateTime tdate { get; set; }
            public string billref { get; set; }
            public double amt { get; set; }
            public ClassPendingBil ( )
            {

            }
        }

    }
}
