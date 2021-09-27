using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_21_Mkt
{
   public class ECRMClientInfo
    {

        [Serializable]

        public class CrmClientInfo
        {
            public string sircode1 { get; set;}
            public string generated { get; set; }
            public string sirdesc { get; set; }
            public string appbydat { get; set; }
            public string prefdesc { get; set; }
            public string assoc { get; set; }
            public string teamdesc { get; set; }
            public string virnotes { get; set; }

            public CrmClientInfo() { }
        }

        [Serializable]
        public class EClassLeadReason

        {

            public string gcod { get; set; }
            public string gdesc { get; set; }


            public EClassLeadReason()
            { 
            
            
            }





        }


    }
}
