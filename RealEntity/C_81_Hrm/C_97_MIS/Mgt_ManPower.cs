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
            public string empname { get; set; }
            public string idcardno { get; set; }
            public string desig { get; set; }
            public string section { get; set; }

            public HrmEnvelopPrint()
            {

            }

        }
       

    }
}
