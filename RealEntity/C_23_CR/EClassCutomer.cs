using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_23_CRR
{
    public class EClassCutomer
    {

        [Serializable]
        public class Customer_Invoice01
        {
            //grp, grpdesc,  pactcode, usircode, mrno, chqno, gcod,  schdate, mrdate, predue,  curdue, delayamt,  todue, gdesc
            public string grp { set; get; }
            public string grpdesc { set; get; }
            public string pactcode { set; get; }
            public string usircode { set; get; }
            public string mrno { set; get; }
            public string chqno { set; get; }
            public string gcod { set; get; }
            public DateTime schdate { set; get; }
            public string mrdate { set; get; }
            public double predue { set; get; }
            public double curdue { set; get; }
            public double delayamt { set; get; }
            public double todue { set; get; }
            public string gdesc { set; get; }
            public Customer_Invoice01() { }




        }

        [Serializable]
        public class Customer_Invoice02
        {
            //comcod, pactcode,  usircode, custnam, custadd, unitdesc, pactdesc
            public string comcod { set; get; }
            public string pactcode { set; get; }
            public string usircode { set; get; }
            public string custnam { set; get; }
            public string custadd { set; get; }
            public string unitdesc { set; get; }
            public string pactdesc { set; get; }
            
            public Customer_Invoice02() { }
            

        }

        [Serializable]
        public class ClientModification
        {
            public string adno1 { get; set; }
            public string adno { get; set; }
            public string grpdesc { get; set; }
            public string gcod { get; set; }
            public string grpcod { get; set; }
            public string addate { get; set; }
            public DateTime addate1 { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string udesc { get; set; }
            public string cusname { get; set; }
            public string gdesc { get; set; }
            public double amt { get; set; }
            public double qty { get; set; }
            public double rate { get; set; }
            public double disamt { get; set; }
            public double netamt { get; set; }
            public ClientModification() { }
        }

        [Serializable]
        public class UtilityOtherCharges
        {
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string custname { get; set; }
            public string udesc { get; set; }
            public double sales { get; set; }
            public double regavat { get; set; }
            public double spanel { get; set; }
            public double adwork { get; set; }
            public double associafee { get; set; }
            public double societyfee { get; set; }
            public double sercharge { get; set; }
            public double delcharge { get; set; }
            public double others { get; set; }
            public double total { get; set; }
            public UtilityOtherCharges() { }
        }

        [Serializable]
        public class AssociationFee
        {
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string custname { get; set; }
            public string udesc { get; set; }
            public double assotarget { get; set; }
            public double asoreceipt { get; set; }
            public double receivable { get; set; }
            public double asopayment { get; set; }
            public double balance { get; set; }
            public AssociationFee() { }
        }

        [Serializable]
        public class ClientDOBMrrDay
        {
            public string name { get; set; }
            public string preaddress { get; set; }
            public string homephone { get; set; }
            public string mobile { get; set; }
            public string email { get; set; }
        
            public ClientDOBMrrDay() { }
        }

        [Serializable]
        public class InvoicePrint
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string mrno { get; set; }
            public string chqno { get; set; }
            public string gcod { get; set; }
            public DateTime schdate { get; set; }
            public string mrdate { get; set; }
            public double predue { get; set; }
            public double curdue { get; set; }
            public double delayamt { get; set; }
            public double todue { get; set; }
            public string gdesc { get; set; }
            public string custnam { get; set; }
            public string custadd { get; set; }
            public string unitdesc { get; set; }
            public string pactdesc { get; set; }
            public InvoicePrint() { }

        }

    }
}
