using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_09_PIMP
{
    public class SubConBill
    {

        [Serializable]

        public class Subconbillwise
        {

            public string pactdesc { get; set; }
            public string billno1 { get; set; }
            public string billno { get; set; }

            public string billdate { get; set; }
            public double billamt { get; set; }
            public double directamt { get; set; }
            public double payment { get; set; }
            public double balance { get; set; }
            public string pactcode { get; set; }


            public Subconbillwise()
            {
            }

        }
        [Serializable]
        public class ImpPlan02
        {
            public string bldcod { get; set; }
            public string flrcod { get; set; }
            public string rptcod { get; set; }
            public string flrdes { get; set; }
            public string rptdesc { get; set; }
            public string rptdesc1 { get; set; }
            public string rptunit { get; set; }
            public string pactdesc { get; set; }
            public DateTime prjstdate { get; set; }
            public DateTime prjenddate { get; set; }
            public double ttqty { get; set; }
            public double trqty { get; set; }
            public double eqty { get; set; }
            public double impqty { get; set; }
            public double tramt { get; set; }
            public double impamt { get; set; }
            public double rptrat { get; set; }


            public ImpPlan02()
            {
            }

        }

        [Serializable]
        public class PrjwiseSubConBill
        {
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string csircode { get; set; }
            public string sirdesc { get; set; }

            public double billamt { get; set; }
            public double payment { get; set; }
            public double netpayable { get; set; }

            public PrjwiseSubConBill()
            {
            }

        }
        [Serializable]

        public class SubconbillPrjwise
        {
            public string pactcode { get; set; }
            public string pactdesc { get; set; }

            public double billamt { get; set; }
            public double payment { get; set; }
            public double netpayable { get; set; }
            public string rmrks { get; set; }

            public SubconbillPrjwise()
            {
            }

        }

        [Serializable]

        public class BillApproval
        {

            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string voudate { get; set; }
            public double billamt { get; set; }
            public string narrations { get; set; }
            public string pactdesc { get; set; }
            public string pactcode { get; set; }
            public string rescode { get; set; }
            public string resdesc { get; set; }


            public BillApproval()
            {
            }

        }




        [Serializable]

        public class EClassConBill
        {

            public string lisuno { get; set; }
            public string lisuno1 { get; set; }
            public string isudat { get; set; }

            public string pactcode { get; set; }
            public string csircode { get; set; }

            public double billamt { get; set; }
            public double billaamt { get; set; }
            public double sdamt { get; set; }
            public double dedamt { get; set; }
            public double penamt { get; set; }
            public double advamt { get; set; }
            public double vatamt { get; set; }
            public double taxamt { get; set; }
            public double netamt { get; set; }

            public string pactdesc { get; set; }
            public string csirdesc { get; set; }



            public EClassConBill()
            {
            }

            public EClassConBill(string lisuno, string isudat, string lisuno1, string pactcode, string csircode,
                double billamt, double billaamt, double sdamt, double dedamt, double penamt, double advamt,
                double vatamt, double taxamt, double netamt, string pactdesc, string csirdesc)
            {

                this.lisuno = lisuno;
                this.lisuno1 = lisuno1;
                this.isudat = isudat;
                this.pactcode = pactcode;
                this.csircode = csircode;
                this.billamt = billamt;
                this.billaamt = billaamt;
                this.sdamt = sdamt;
                this.dedamt = dedamt;
                this.penamt = penamt;
                this.advamt = advamt;
                this.vatamt = vatamt;
                this.taxamt = taxamt;
                this.netamt = netamt;
                this.pactdesc = pactdesc;
                this.csirdesc = csirdesc;



            }

        }




        [Serializable]

        public class EClassConBillSummary
        {


            public string csircode { get; set; }

            public double billamt { get; set; }
            public double billaamt { get; set; }
            public double sdamt { get; set; }
            public double dedamt { get; set; }
            public double penamt { get; set; }
            public double advamt { get; set; }
            public double vatamt { get; set; }
            public double taxamt { get; set; }
            public double netamt { get; set; }
            public double payamt { get; set; }


            public string csirdesc { get; set; }



            public EClassConBillSummary()
            {
            }

            public EClassConBillSummary(string csircode, double billamt, double billaamt, double sdamt, double dedamt,
                double penamt, double advamt, double vatamt, double taxamt, double netamt, double payamt, string csirdesc)
            {


                this.csircode = csircode;
                this.billamt = billamt;
                this.billaamt = billaamt;
                this.sdamt = sdamt;
                this.dedamt = dedamt;
                this.penamt = penamt;
                this.advamt = advamt;
                this.vatamt = vatamt;
                this.taxamt = taxamt;
                this.netamt = netamt;
                this.payamt = payamt;
                this.csirdesc = csirdesc;



            }






        }



        [Serializable]

        public class EClassConPayment
        {


            public string csircode { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string voudate { get; set; }
            public string billno { get; set; }
            public string billno1 { get; set; }
            public double payamt { get; set; }
            public string pactdesc { get; set; }
            public string csirdesc { get; set; }



            public EClassConPayment()
            {
            }

            public EClassConPayment(string csircode, string vounum, string vounum1, string voudate, string billno,
                string billno1, double payamt, string pactdesc, string csirdesc)
            {


                this.csircode = csircode;
                this.vounum = vounum;
                this.vounum1 = vounum1;
                this.voudate = voudate;
                this.billno = billno;
                this.billno1 = billno1;
                this.payamt = payamt;
                this.pactdesc = pactdesc;
                this.csirdesc = csirdesc;
            }






        }

        [Serializable]
        public class ConRaBill
        {

            public string comcod { get; set; }

            public string flrdes { get; set; }
            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public string mbbook { get; set; }
            public double balqty { get; set; }
            public double wrkqty { get; set; }
            public double prcent { get; set; }
            public double peronbgd { get; set; }
            public double preqty { get; set; }
            public double isuqty { get; set; }
            public double isurat { get; set; }
            public double isuamt { get; set; }
            public double bgdqty { get; set; }





            public ConRaBill() { }


            //public ConRaBill(string comcod, string flrdes, string rsirdesc, string rsirunit, double isuqty, double isurat, double isuamt)
            //{

            //    //this.comcod = comcod;
            //    this.flrdes = flrdes;
            //    this.rsirdesc = rsirdesc;
            //    this.rsirunit = rsirunit;
            //    this.isuqty = isuqty;
            //    this.isurat = isurat;
            //    this.isuamt = isuamt;




            //}

        }

        [Serializable]
        public class ContractorBillDetails
        {
            public string pactdesc { get; set; }
            public string billno1 { get; set; }
            public string cbillref { get; set; }
            public string lisurefno { get; set; }
            public string billdate { get; set; }
            public double billamt { get; set; }
            public double percntge { get; set; }
            public double sdamt { get; set; }
            public double dedamt { get; set; }
            public double penamt { get; set; }
            public double netpayamt { get; set; }
            public double payment { get; set; }
            public double spayment { get; set; }
            public double dedpayment { get; set; }
            public double netpayable { get; set; }


            public ContractorBillDetails() { }


        }

        [Serializable]
        public class RptSubConOverAll2
        {


            public string sirdesc { get; set; }
            public string pactdesc { get; set; }
            public double dwithoutibll { get; set; }
            public double billamt { get; set; }
            public double payment { get; set; }
            public double netpayable { get; set; }
            public double ncpayable { get; set; }

            public RptSubConOverAll2() { }


        }

        [Serializable]
        public class EClassSubConBillFinalTopSheet
        {

            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public double paidamt { get; set; }
            public double tbillamt { get; set; }

            public EClassSubConBillFinalTopSheet()
            {

            }



        }


        [Serializable]

        public class ContractorBillWorkWise
        {

            public string flrcod { get; set; }
            public string rsircode { get; set; }
            public double pbillqty { get; set; }
            public double curbillqty { get; set; }
            public double tbillqty { get; set; }
            public double tbillamt { get; set; }
            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public double avgerate { get; set; }
            public string flrdesc { get; set; }
            public string slno { get; set; }
            public ContractorBillWorkWise()
            {

            }


        }


        [Serializable]
        public class SubConBillReq
        {
            public string comcod { get; set; }
            public string flrdes { get; set; }
            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public string mbbook { get; set; }
            public double balqty { get; set; }
            public double wrkqty { get; set; }
            public double prcent { get; set; }
            public double peronbgd { get; set; }
            public double preqty { get; set; }
            public double isuqty { get; set; }
            public double isurat { get; set; }
            public double isuamt { get; set; }
            public double bgdqty { get; set; }

            public double bgdrat { get; set; }

            public double balamt { get; set; }
            public double reqqty { get; set; }
            public double reqrat { get; set; }

            public double reqamt { get; set; }

            //, balamt,reqqty,reqrat,reqamt
            public SubConBillReq() { }

        }


        [Serializable]
        public class SubConBillTopSheet
        {
            public string comcod { get; set; }
            public string rsircode { get; set; }
            public double prebillamt { get; set; }
            public double curbillamt { get; set; }
            public double totalbill { get; set; }
            public string cbillref { get; set; }
            public string rsirdesc { get; set; }
            public string rmrks { get; set; }

            public SubConBillTopSheet()
            {
            }

        }

        [Serializable]

        public class RptSubConBillOverAll
        {
            public string csircode { get; set; }
            public string sirdesc { get; set; }
            public double billamt { get; set; }
            public double sdamt { get; set; }
            public double dedamt { get; set; }
            public double penamt { get; set; }
            public double netamt { get; set; }
            public double payment { get; set; }
            public double netpayable { get; set; }
            public RptSubConBillOverAll(){ }

        }

        [Serializable]
        public class SubConAllBill
        {
            public string flrdes { get; set; }
            public string rptdesc { get; set; }
            public string rptunit { get; set; }
            public string resdesc { get; set; }
            public string unitdesc { get; set; }
            public double rptqty { get; set; }
            public double rptrat { get; set; }
            public double rptamt { get; set; }
            public double exqty { get; set; }
            public double subconrat { get; set; }
            public double subconamt { get; set; }
            public double difqty { get; set; }
            public double difrat { get; set; }
            public double difamt { get; set; }
            public double bgdrat { get; set; }
            public double balamt { get; set; }
            public double reqqty { get; set; }
            public double reqrat { get; set; }
            public double reqamt { get; set; }
            public double preqty { get; set; }
            public double preamt { get; set; }
            public double curqty { get; set; }
            public double curamt { get; set; }
            public double totalqut { get; set; }
            public double totalamt { get; set; }
            public SubConAllBill() { }

        }
        [Serializable]
        public class EClassResourceWiseMB
        {
            public string rsircode { get; set; }
            public string flrcod { get; set; }
            public int sl { get; set; }
            public double tweight { get; set; }
            public EClassResourceWiseMB() { }

            public EClassResourceWiseMB(string rsircode, string flrcod, double tweight, int sl)
            {

                this.rsircode = rsircode;
                this.flrcod = flrcod;
                this.tweight = tweight;
                this.sl = sl;

            }


        }



        public class EClassResourceWiseMBSum
        {
            public string rsircode { get; set; }
            public string flrcod { get; set; }
            public double tweight { get; set; }
            public EClassResourceWiseMBSum() { }

            public EClassResourceWiseMBSum(string rsircode, string flrcod, double tweight)
            {

                this.rsircode = rsircode;
                this.flrcod = flrcod;
                this.tweight = tweight;

            }


        }


    }

}