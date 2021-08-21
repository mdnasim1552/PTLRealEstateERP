using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_12_Inv
{
  public  class EclassPurchase
    {
        [Serializable]
        public class PurchaseOrderInfo
        {
            public string orderno { get; set; }
            public string grp { get; set; }
            public int rowid { get; set; }
            public string grpdesc { get; set; }
            public string comcod { get; set; }
            public string reqno { get; set; }
            public string mrfno { get; set; }
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public string rsirdesc { get; set; }
            public string spcfdesc { get; set; }
            public string rsirunit { get; set; }
            public double ordrqty { get; set; }
            public double aprovrate { get; set; }
            public double ordramt { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string proadd { get; set; }
            public string reqnar { get; set; }
            public double aprovsrate { get; set; }
            public double dispercnt { get; set; }


        }

        [Serializable]
        public class PurOrderTermsCondition
        {
            public string orderno { get; set; }
            public string termsid { get; set; }
            public string termssubj { get; set; }
            public string termsdesc { get; set; }
            public string termsrmrk { get; set; }

        }
        [Serializable]
        public class PaymentSchedule
        {
            public string inscode { get; set; }
            public string insdesc { get; set; }
            public DateTime insdate { get; set; }
            public double insamt { get; set; }
            public string rmrks { get; set; }
            public string rmrks02 { get; set; }
        }
    }
}
