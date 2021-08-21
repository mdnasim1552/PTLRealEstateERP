using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_17_Acc
{
    public class RptInvList
    {
        [Serializable]

        public class InterComMaterial
        {

            public string vounum1 { get; set; }
            public DateTime voudat { get; set; }
            public string refno { get; set; }
            public string ftcomdesc { get; set; }
            public string ttcomdesc { get; set; }
            public string tfprjdesc { get; set; }
            public string ttprjdesc { get; set; }
            public string matdesc { get; set; }
            public double tqty { get; set; }
            public double rate { get; set; }

            public double tamount { get; set; }
      

        }
        

    }
}
