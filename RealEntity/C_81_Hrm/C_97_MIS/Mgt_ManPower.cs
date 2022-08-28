using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm.C_97_MIS
{
    
    public  class Mgt_ManPower
    {
        [Serializable]
        public class HrmEnvelopPrint {
            public string Name { get; set; }
            public string Card { get; set; }
            public string Designation { get; set; }
            public string Department { get; set; }

            public HrmEnvelopPrint()
            {

            }

        }
       

    }
}
