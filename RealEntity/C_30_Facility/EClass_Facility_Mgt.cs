using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealERPEntity.C_30_Facility
{
    public class EClass_Facility_Mgt
    {
        [Serializable]
        public class EClass_Complain_List
        {
            public int complainId { get; set; }
            public string complainDesc { get; set; }
            public string remarks { get; set; }
        }
    }
}
