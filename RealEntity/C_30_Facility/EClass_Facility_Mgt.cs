using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_30_Facility
{
    public class EClass_Facility_Mgt
    {
        [Serializable]
        public class EClass_Complain_List
        {
            public string issueId { get; set; }
            public string issueType { get; set; }
            public int complainId { get; set; }
            public string complainDesc { get; set; }
            public string remarks { get; set; }
            public bool isQC { get; set; }
            public bool isCC { get; set; }
        }
        [Serializable]
        public class EClass_Material_List
        {
            public string materialId { get; set; }
            public string materialDesc { get; set; }
            public string unit { get; set; }
            public double quantity { get; set; }
            public double rate { get; set; }
            public double amount { get; set; }
            public int seq { get; set; }
            public double percnt { get; set; }
            public string type { get; set; }
            

        }
    }
}
