using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_23_CR 
{
    public class EClassLand
    {
        [Serializable]
        public class RptLandownerColStatus
        {

            public string comcod { set; get; }
            public string pactcode { set; get; }
            public string usircode { set; get; }
            public string mrno { set; get; } 
            public DateTime paydate { set; get; } 
            public string chqno { set; get; }  
            public string bankname { set; get; }  
            public string benefname { set; get; }  
            public decimal paidamt { set; get; }  
            public decimal loamt { set; get; }
            public string udesc { set; get; }
            public string pactdesc { set; get; }
            public string custname { set; get; } 
            public string benefnamedesc { set; get; }  


            public RptLandownerColStatus() { }

        }
    }
}
