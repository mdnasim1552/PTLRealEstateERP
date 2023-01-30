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
        public class PromMatHistory
        {
            public string comcod { get; set; }
            public string issueno { get; set; }
            public string actcode { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string unit { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public string deptcode { get; set; }//
            public string deptname { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string idcard { get; set; }

            public DateTime issuedat { get; set; }
            public double issueqty { get; set; }
            public double issueamt { get; set; }
            public double rate { get; set; }
            public string vounum { get; set; }
            public string narr { get; set; }
            public string apstatus { get; set; }

        }
        [Serializable]
        public class PurchaseOrderInfo
        {
            public string orderno { get; set; }
             public int grpsl { get; set; }
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
            public string expusedt { get; set; }  
            public string rmrks { get; set; }   
            public string deptdesc { get; set; }   


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

        [Serializable]
        public class PaymentOtherCost
        {
            public string pactcode { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc1 { get; set; }
            public string ssircode { get; set; }
            public string ssirdesc1 { get; set; }
            public double ordramt { get; set; }
        }

        [Serializable]
        public class InventoryAmountBasis
        {
            public string pactcode { get; set; }
            public string rsircode { get; set; }
            public string pactdesc { get; set; }
            public string resdesc{ get; set; }
            public double rcvamt { get; set; }
            public double tminamt { get; set; }
            public double trninamt { get; set; }
            public double trnoutamt { get; set; }
            
            public double tmoutamt { get; set; }
            public double lsamt { get; set; }
            public double netrcvamt { get; set; }
            public double issueamt { get; set; }
            public double bgdconamt { get; set; }
            public double actstock { get; set; }
            public double bgdstock { get; set; }
            public double varamt { get; set; }
            public double opamt { get; set; }
        }
        public class InventoryQtyBasisPeriodic
        {
            public string rsircode { get; set; }
            public string sircode { get; set; }
            public string sirdesc { get; set; }
            public double trninqty { get; set; }
            public double trnoutqty { get; set; }
            public double opqty { get; set; }
            public double rcvqty { get; set; }
            public double tminqty { get; set; }
            public double tmoutqty { get; set; }
            public double lqty { get; set; }
            public double netrcvqty { get; set; }
      
            public double issueqty { get; set; }
            public double actstock { get; set; }

        }

        [Serializable]
        public class MaterialWiseStock
        {
            public string gp { get; set; }
            public string grp { get; set; }
            public string isuno { get; set; }
            public string isuno1 { get; set; }
            public string exdate { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string subcode { get; set; }
            public string subdesc { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public string unit { get; set; }
            public double inqty { get; set; }
            public double outqty { get; set; }
            public double stock { get; set; }
            public string ssircode { get; set; }
            public string ssirdesc { get; set; }
            public double rate { get; set; }


        }

         [Serializable]
        public class MktPurchaseOrderInfo
        {
            public string orderno { get; set; }
            public string grp { get; set; }
            public int rowid { get; set; }
            public string grpdesc { get; set; }
            public string comcod { get; set; }
            public string reqno { get; set; }
            public string mrfno { get; set; }
            public string prtype { get; set; }
            public string acttype { get; set; }
            public string mkttype { get; set; }
            public string prtypedesc { get; set; }
            public string acttypedesc { get; set; }
            public string mkttypedesc { get; set; }
            public double ordrqty { get; set; }
            public double aprvqty { get; set; }
            public double reqrat { get; set; }
            public double ordramt { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string proadd { get; set; }
            public string reqnar { get; set; }
            public double dispercnt { get; set; }
            public string rsirdetdesc { get; set; }

            public MktPurchaseOrderInfo() { }
        }

        [Serializable]
        public class MktPurchasePayment
        {

            public string comcod { get; set; }
            public string billno { get; set; }
            public DateTime voudat { get; set; }
            public double trnam { get; set; }
            public MktPurchasePayment() { }
        }
    }
}
