using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_14_Pro
{
    public class EClassPur
    {
        //------------BO---

        #region purchase Dashboard
        [Serializable]
        public class EClassYear
        {
            public string yearid { set; get; }
            public double ttlamt { set; get; }

            public double purpay { set; get; }

            public EClassYear(string yearid, double ttlamt, double purpay)
            {
                this.yearid = yearid;
                this.ttlamt = ttlamt;
                this.purpay = purpay;
            }
        }
        [Serializable]
        public class EClassMonthly
        {
            public string yearmon { set; get; }
            public string yearmon1 { set; get; }
            public double ttlsalamt { set; get; }
            public double tpayamt { set; get; }
            public double ttlsalamtcore { set; get; }
            public double tpayamtcore { set; get; }





            public EClassMonthly(string yearmon, string yearmon1, double ttlsalamt, double tpayamt, double ttlsalamtcore, double tpayamtcore)
            {
                this.yearmon = yearmon;
                this.yearmon1 = yearmon1;
                this.ttlsalamt = ttlsalamt;
                this.tpayamt = tpayamt;
                this.ttlsalamtcore = ttlsalamtcore;
                this.tpayamtcore = tpayamtcore;

            }

        }
        [Serializable]
        public class EClassWeekly
        {
            public string wcode1 { set; get; }
            public double wpamt1 { set; get; }
            public double wpayamt1 { set; get; }
            public string wcode2 { set; get; }
            public double wpamt2 { set; get; }
            public double wpayamt2 { set; get; }
            public string wcode3 { set; get; }
            public double wpamt3 { set; get; }
            public double wpayamt3 { set; get; }
            public string wcode4 { set; get; }
            public double wpamt4 { set; get; }
            public double wpayamt4 { set; get; }

            public EClassWeekly(string wcode1, double wpamt1, double wpayamt1, string wcode2, double wpamt2, double wpayamt2, string wcode3, double wpamt3, double wpayamt3,
                    string wcode4, double wpamt4, double wpayamt4)
            {
                this.wcode1 = wcode1;
                this.wpamt1 = wpamt1;
                this.wpayamt1 = wpayamt1;
                this.wcode2 = wcode2;
                this.wpamt2 = wpamt2;
                this.wpayamt2 = wpayamt2;
                this.wcode3 = wcode3;
                this.wpamt3 = wpamt3;
                this.wpayamt3 = wpayamt3;
                this.wcode4 = wcode4;
                this.wpamt4 = wpamt4;
                this.wpayamt4 = wpayamt4;

            }

        }

        [Serializable]
        public class EClassDayWisePur
        {
            public string pactcode { set; get; }
            public string pactdesc { set; get; }
            public string rsircode { set; get; }
            public string rsirdesc { set; get; }

            public string ssircode { set; get; }
            public string ssirdesc { set; get; }
            public string billno1 { set; get; }

            public string billno { set; get; }
            public string billdate1 { set; get; }
            public string vounum1 { set; get; }
            public string vounum { set; get; }
            public double billamt { set; get; }

            public EClassDayWisePur(string pactcode, string pactdesc, string rsircode, string rsirdesc, string ssircode, string ssirdesc, string billno1, string billno,
                    string billdate1, string vounum1, string vounum, double billamt)
            {
                this.pactcode = pactcode;
                this.pactdesc = pactdesc;
                this.rsircode = rsircode;
                this.rsirdesc = rsirdesc;
                this.ssircode = ssircode;
                this.ssirdesc = ssirdesc;
                this.billno1 = billno1;
                this.billno = billno;
                this.billdate1 = billdate1;
                this.vounum1 = vounum1;
                this.vounum = vounum;
                this.billamt = billamt;
            }
        }

        [Serializable]
        public class EClassDayWisePay
        {
            public string pactcode { set; get; }
            public string pactdesc { set; get; }
            public string cactcode { set; get; }
            public string cactdesc { set; get; }

            public string ssircode { set; get; }
            public string ssirdesc { set; get; }
            public string billno1 { set; get; }

            public string billno { set; get; }
            public string voudat { set; get; }
            public string vounum1 { set; get; }
            public string vounum { set; get; }
            public double payamt { set; get; }

            public EClassDayWisePay(string pactcode, string pactdesc, string cactcode, string cactdesc, string ssircode, string ssirdesc, string billno1, string billno,
                    string voudat, string vounum1, string vounum, double payamt)
            {
                this.pactcode = pactcode;
                this.pactdesc = pactdesc;
                this.cactcode = cactcode;
                this.cactdesc = cactdesc;
                this.ssircode = ssircode;
                this.ssirdesc = ssirdesc;
                this.billno1 = billno1;
                this.billno = billno;
                this.voudat = voudat;
                this.vounum1 = vounum1;
                this.vounum = vounum;
                this.payamt = payamt;
            }
        }

        #endregion

        #region SubCon Dashboard
        [Serializable]
        public class EClassYearSCon
        {
            public string yearid { set; get; }
            public double tbamt { set; get; }

            public double tpayamt { set; get; }

            public EClassYearSCon(string yearid, double tbamt, double tpayamt)
            {
                this.yearid = yearid;
                this.tbamt = tbamt;
                this.tpayamt = tpayamt;
            }
        }
        [Serializable]
        public class EClassMonthlySCon
        {
            public string yearmon { set; get; }
            public string yearmon1 { set; get; }
            public double tcbamt { set; get; }
            public double tcbpayamt { set; get; }
            public double tcbamtcore { set; get; }
            public double tcbpayamtcore { set; get; }





            public EClassMonthlySCon(string yearmon, string yearmon1, double tcbamt, double tcbpayamt, double tcbamtcore, double tcbpayamtcore)
            {
                this.yearmon = yearmon;
                this.yearmon1 = yearmon1;
                this.tcbamt = tcbamt;
                this.tcbpayamt = tcbpayamt;
                this.tcbamtcore = tcbamtcore;
                this.tcbpayamtcore = tcbpayamtcore;

            }

        }




        #endregion




        [Serializable]
        public class RptBillAproved01
        {
            public string comcod { set; get; }
            public string actcode { set; get; }
            public string slnum { set; get; }
            public string billno { set; get; }
            public string reqno { set; get; }
            public string valdate { get; set; }
            public string mslnum1 { set; get; }
            public string usrdesig { set; get; }
            public string actdesc { set; get; }
            public string actdesc2 { set; get; }
            public string rescount { set; get; }
            public string apdate { set; get; }
            public string billno2 { set; get; }
            public string useridapp { set; get; }
            public double apamt { set; get; }
            public double amt { set; get; }
            public double advamt { set; get; }
            public double netamt { get; set; }
            public string refno { set; get; }
            public string revdate { set; get; }
            public string apppaydate { set; get; }
            public string rec { set; get; }
            public string approved { set; get; }
            public string forward { set; get; }
            public string nochq { set; get; }
            public string issued { set; get; }
            public double issuedamt { set; get; }
            public string issn { get; set; }
            public string appisedate { set; get; }
            public string confirm { set; get; }
            public string checqno { set; get; }
            public string bankinf { set; get; }
            public string checqdt { set; get; }
            public string preparedid { set; get; }
            public string recomid { set; get; }
            public string appovedid { set; get; }
            public RptBillAproved01() { }
        }

        [Serializable]
        public class EClassSupAmt
        {
            public string rsircode { set; get; }
            public string rsirdesc { set; get; }
            public double samount { set; get; }



            public EClassSupAmt()
            {
            }


            public EClassSupAmt(string rsircode, string rsirdesc, double samount)
            {
                this.rsircode = rsircode;
                this.rsirdesc = rsirdesc;
                this.samount = samount;


            }



        }

        [Serializable]
         public class RptPurchasePrjwise
        {
            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }

            public double amt { get; set; }
            public double percntage { get; set; }
        }

        [Serializable]
        public class MkrServay02
        {
            public string comcod { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc1 { get; set; }
            public string flrdes { get; set; }
            public string rsirunit { get; set; }
            public string spcfcod { get; set; }
            public double qty { get; set; }
            public double bgdqty { get; set; }
            public double bgdrat { get; set; }
            public double resrate1 { get; set; }
            public double resrate2 { get; set; }
            public double resrate3 { get; set; }
            public double resrate4 { get; set; }
            public double resrate5 { get; set; }
            public double amt1 { get; set; }
            public double amt2 { get; set; }
            public double amt3 { get; set; }
            public double amt4 { get; set; }
            public double amt5 { get; set; }
            public string camt1 { get; set; }
            public string camt2 { get; set; }
            public string camt3 { get; set; }
            public string camt4 { get; set; }
            public string camt5 { get; set; }
            public string msrrmrk { get; set; }
            public double aprovrate { get; set; }
            public string spcfdesc { get; set; }

            public double prerate1 { get; set; }
            public double prerate2 { get; set; }
            public double prerate3 { get; set; }
            public double prerate4 { get; set; }
            public double prerate5 { get; set; }
            public MkrServay02() { }
        }

        [Serializable]
        public class MkrServay03
        {
            //  a.comcod, a.ssircode,  a.discount, a.ccharge, a.payterm, ssirdesc=isnull(b.sirdesc, '')
            //comcod,msrno,ssircode,discount ,ccharge,payterm,qutdate,worktime,notes ,goodwill,matavailable,delcon,ait
            public string comcod { get; set; }
            public string msrno { get; set; }
            public string ssircode { get; set; }
            public double discount { get; set; }
            public string ccharge { get; set; }
            public string payterm { get; set; }
            public DateTime qutdate { get; set; }
            public string worktime { get; set; }
            public string notes { get; set; }
            public string contact { get; set; }
            public string crperiod { get; set; }
            public string ssirdesc { get; set; }
            public string ssirdesc1 { get; set; }
            public string goodwill { get; set; }
            public string matavailable { get; set; }
            public string delcon { get; set; }
            public string ait { get; set; }


            public MkrServay03() { }

        }




        public class SuppDetails
        {
            //  a.comcod, a.ssircode,  a.discount, a.ccharge, a.payterm, ssirdesc=isnull(b.sirdesc, '')

            public string supcode { get; set; }
            public string supname { get; set; }
            public string conperson { get; set; }
            public string phoneno { get; set; }
            public string mobile { get; set; }
            public string supaddress { get; set; }

            public string location { get; set; }

            public string mail { get; set; }


            public SuppDetails() { }

        }

        [Serializable]
        public class ConDetails
        {
            //  a.comcod, a.ssircode,  a.discount, a.ccharge, a.payterm, ssirdesc=isnull(b.sirdesc, '')

            public string concode { get; set; }
            public string conname { get; set; }
            public string conperson { get; set; }
            public string mobile { get; set; }
            public string supaddress { get; set; }
            public string location { get; set; }
            public string wrkt { get; set; }
            public string id { get; set; }
            public ConDetails() { }

        }
        [Serializable]
        public class RptSupMonthAss
        {
            public string sircod { get; set; }
            public string sirdesc { get; set; }
            public string mark { get; set; }
            public string position { get; set; }
        }

        public class LastSupplier
        {
            public string sircode { get; set; }

            public LastSupplier()
            {

            }
            public LastSupplier(string sircode)
            {
                this.sircode = sircode;

            }
        }

        [Serializable]
        public class EClassSuppaContractior
        {
            public string gcod { get; set; }
            public string gdesc { get; set; }
            public string gdesc1 { get; set; }

            public EClassSuppaContractior() { }

            //public EClassSuppaContractior(string gcod, string gdesc, string gdes1)
            //{
            //    this.gcod = gcod;
            //    this.gdesc = gdesc;
            //    this.gdesc1 = gdesc1;

            //}


        }


        [Serializable]
        public class EClassSuppaContractior02
        {
            public string gcod { get; set; }
            public string gdesc { get; set; }
            public string gdesc1 { get; set; }

            public EClassSuppaContractior02() { }

            public EClassSuppaContractior02(string gcod, string gdesc, string gdes1)
            {
                this.gcod = gcod;
                this.gdesc = gdesc;
                this.gdesc1 = gdesc1;

            }


        }

        [Serializable]
        public class RptBillRegTrack
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string slnum { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string rescode { get; set; }
            public string resdesc { get; set; }
            public string billno { get; set; }
            public DateTime posteddate { get; set; }
            public string username { get; set; }
            public double amount { get; set; }


            public RptBillRegTrack() { }




        }

        [Serializable]
        public class RptSupPayment
        {
            public string ssircode { get; set; }
            public string pactcode { get; set; }
            public string ssirdesc { get; set; }
            public string pactdesc { get; set; }
            public double pendingamt { get; set; }
            public double billamt { get; set; }
            public double payment { get; set; }
            public double totalpay { get; set; }
            public double aitamt { get; set; }
            public double aitper { get; set; }
            public double aprovamt { get; set; }
            public double netpayable { get; set; }
        }


        [Serializable]
        public class RptRequisitionStatus1
        {
            public string pactdesc { get; set; }
            public string reqno { set; get; }

            public string mrfno { get; set; }
            public DateTime reqdat { get; set; }
            public string resdesc { get; set; }
            public string resunit { get; set; }
            public string spcfdesc { get; set; }
            public double areqty { get; set; }
            public double ordrqty { get; set; }
            public double mrrqty { get; set; }
            public double balqty { get; set; }


        }


        [Serializable]
        public class RptRequisitionStatus2
        {
            public string pactdesc { get; set; }
            public string reqno { set; get; }

            public string mrfno { get; set; }
            public DateTime reqdat { get; set; }
            public string resdesc { get; set; }
            public string resunit { get; set; }
            public double areqty { get; set; }
            public double progqty { get; set; }
            public double rqty { get; set; }
            public string spcfdesc { get; set; }
            public string rsircode { get; set; }


        }

        [Serializable]
        public class RptReqAppStatus
        {
            public string pactdesc { get; set; }
            public string reqno1 { set; get; }

            public string mrfno { get; set; }
            public string reqdat1 { get; set; }
            public string resdesc { get; set; }
            public string resunit { get; set; }
            public string spcfdesc { get; set; }
            public double areqty { get; set; }
            public double reqrat { get; set; }
            public double reqamt { get; set; }

        }

        [Serializable]
        public class RptPurchaseSummary02
        {
            public string pactdesc { get; set; }
            public string rptunit { get; set; }
            public string rptdesc { get; set; }
            public string rsircode { get; set; }
            public string rptcode { get; set; }
            public double qty { get; set; }
            public double rate { get; set; }
            public double amt { get; set; }
        }
        public class RptSummaryProject
        {
            public string comcod { get; set; }
            public string pactcode { get; set; }

            public double amt { get; set; }
            public double percnt { get; set; }
            public string pactdesc { get; set; }
        }


        [Serializable]
        public class RptSupCreditLimit
        {
            public string resdesc { get; set; }
            public double billamt { get; set; }
            public double crlimit { get; set; }
            public double nyetdues { get; set; }
            public double netdues { get; set; }

        }

        [Serializable]
        public class RptSupPayableStatus
        {
            public string resdesc { get; set; }
            public double opnam { get; set; }
            public double periodamt { get; set; }
            public double netbal { get; set; }

        }

        [Serializable]
        public class RptMatGrpwisePayable
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string mwgdesc { get; set; }
            public double opnam { get; set; }
            public double periodamt { get; set; }
            public double netbal { get; set; }
        }



        public class RptOrderStatus
        {
            public string ssircode { get; set; }
            public string pactcode { get; set; }
            public string sirdesc { get; set; }
            public string pactdesc { get; set; }
            public string orderno { get; set; }
            public double payment { get; set; }
            public double orderamt { get; set; }
            public DateTime orderdat { get; set; }
            public double payable { get; set; }

            public RptOrderStatus()
            {

            }

        }


        [Serializable]
        public class EClassOrderRange
        {
            public string slnum { get; set; }
            public double minamt { get; set; }
            public double maxamt { get; set; }

            public EClassOrderRange() { }

            public EClassOrderRange(string slnum, double minamt, double maxamt)
            {
                this.slnum = slnum;
                this.minamt = minamt;
                this.maxamt = maxamt;

            }


        }

        [Serializable]
        public class EClassMatRateVar
        {
            public string comcod { get; set; }
            public string rsircode { get; set; }
            public string spcfcod { get; set; }

            public string rsirdesc { get; set; }

            public string rsirunit { get; set; }
            public string spcfdesc { get; set; }

            public double amt1 { get; set; }
            public double amt2 { get; set; }
            public double amt3 { get; set; }
            public double amt4 { get; set; }
            public double amt5 { get; set; }
            public double amt6 { get; set; }
            public double amt7 { get; set; }
            public double amt8 { get; set; }
            public double amt9 { get; set; }
            public double amt10 { get; set; }
            public double amt11 { get; set; }
            public double amt12 { get; set; }

            public EClassMatRateVar() { }


        }
        [Serializable]
        public class EClassRequisitionApproval
        {
            public string pactcode { get; set; }
            public string projdesc1 { get; set; }
            public string reqno { get; set; }
            public string reqno1 { get; set; }
            public string msrno { get; set; }
            public string mrfno { get; set; }
            public string reqdat { get; set; }
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public string rspcfcod { get; set; }
            public string rsirdesc1 { get; set; }
            public string spcfdesc { get; set; }
            public string rsirunit { get; set; }
            public string ssircode { get; set; }
            public string ssirdesc1 { get; set; }
            public double areqty { get; set; }
            public double comqty { get; set; }
            public double balqty { get; set; }
            public double aprovqty { get; set; }
            public double aprovsrate { get; set; }
            public double dispercnt { get; set; }
            public double aprovrate { get; set; }
            public double maprovrate { get; set; }
            public double aprovamt { get; set; }
            public string paytype { get; set; }
            public string eusrname { get; set; }


            public EClassRequisitionApproval() { }



        }
        [Serializable]
        public class RequisionApprovalSignature
        {

            public string comcod { get; set; }
            public string reqno { get; set; }
            public string reqname { get; set; }
            public string reqchkname { get; set; }
            public string reqratename { get; set; }
            public string reqaprname { get; set; }
            public string reqfaprname { get; set; }
            public string requsr { get; set; }
            public string reqdat { get; set; }
            public string reqchk { get; set; }
            public string CHECKDAT { get; set; }
            public string RATEPBYID { get; set; }
            public string RATEIDATE { get; set; }
            public string APRVBYID { get; set; }
            public string APRVDAT { get; set; }

            public RequisionApprovalSignature() { }


        }
        [Serializable]
        public class EClassMaterialTransferTacking
        {

            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string mtreqno { get; set; }
            public string genno { get; set; }
            public string mtrdat { get; set; }
            public string mtrdttex { get; set; }
            public string refno { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string runit { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public double qty { get; set; }
            public double rate { get; set; }
            public double amt { get; set; }
            public string userid { get; set; }
            public string posteddat { get; set; }
            public string postdttext { get; set; }
            public string postrmid { get; set; }
            public string usrname { get; set; }

            public EClassMaterialTransferTacking() { }





        }

        [Serializable]
        public class RptBillForward
        {
            public string comcod { set; get; }
            public string mslnum1 { set; get; }
            public string billno2 { set; get; }
            public string slnum { set; get; }
            public string actcode { set; get; }
            public string actdesc { set; get; }
            public string actdesc2 { set; get; }
            public string rescount { set; get; }
            public string useridapp { set; get; }
            public string usrdesig { set; get; }
            public double amt { set; get; }
            public double advamt { set; get; }
            public double netamt { get; set; }
            public string refno { set; get; }
            public string billno { set; get; }

            public string reqno { set; get; }
            public string rcvdate { set; get; }
            public string valdate { get; set; }
            public string apppaydate { set; get; }
            public string rec { set; get; }
            public string approved { set; get; }
            public string forward { set; get; }
            public string apdate { set; get; }
            public string nochq { set; get; }

            public double apamt { set; get; }
            public string issued { set; get; }
            public double issuedamt { set; get; }
            public string issnochq { set; get; }

            public string appisedate { set; get; }
            public string confirm { set; get; }
            public string orderno { set; get; }
            public string chkmerge { set; get; }

            public string billrmrks { set; get; }




            public RptBillForward() { }
        }


        #region
        [Serializable]
        public class RptPurHisMatWise
        {
            public string comcod { get; set; }
            public string mrrno1 { get; set; }
            public string mrrref { get; set; }
            public string mrrdat1 { get; set; }

            public string reqno1 { get; set; }
            public string mrfno { get; set; }
            public string pactdesc { get; set; }
            public string ssirdesc { get; set; }
            public string spcfdesc { get; set; }
            public double mrrqty { get; set; }
            public double mrrrat { get; set; }
            public double mrramt { get; set; }


            public RptPurHisMatWise() { }


        }

        #endregion


        #region
        [Serializable]
        public class RptIndSupPurchase
        {
            public string comcod { get; set; }
            public string mrrno1 { get; set; }
            public string mrrref { get; set; }
            public string mrrdat { get; set; }
            public string mrfno { get; set; }

            public string pactcode { get; set; }
            public string ssircode { get; set; }
            public string chalanno { get; set; }
            public double qty { get; set; }
            public double amt { get; set; }
            public double rate { get; set; }
            public string orderno1 { get; set; }
            public string ordrref { get; set; }
            public string reqno1 { get; set; }
            public string billno1 { get; set; }
            public string billref { get; set; }
            public string resdesc { get; set; }
            public string resunit { get; set; }
            public string supdesc { get; set; }
            public string pactdesc { get; set; }
            public string spcfdesc { get; set; }
            public string usrsname { get; set; }
            public RptIndSupPurchase() { }


        }

        #endregion
        #region
        [Serializable]
        public class RptWorkOrdHisSup
        {
            public string comcod { get; set; }
            public string ssirdesc { get; set; }
            public string orderno1 { get; set; }
            public string orderdat1 { get; set; }
            public string rsirdesc { get; set; }
            public string mrfno { get; set; }
            public string spcfdesc { get; set; }
            public string rsirunit { get; set; }
            public double ordrqty { get; set; }
            public double ordrrate { get; set; }
            public double ordramt { get; set; }
            public string pactdesc { get; set; }
            public string mrrno1 { get; set; }
            public string mrrdat { get; set; }
            public double mrrqty { get; set; }
            public double mrramt { get; set; }
            public string billno1 { get; set; }
            public double billqty { get; set; }
            public double billamt { get; set; }
            public double acpaidamt { get; set; }
            public double balbonpo { get; set; }
            public double balbonmrr { get; set; }
            public double balbonbill { get; set; }


            public RptWorkOrdHisSup() { }


        }

        #endregion
        #region
        [Serializable]
        public class RptWorkOrdHisResource
        {
            public string comcod { get; set; }
            public string ssirdesc { get; set; }
            public string orderno1 { get; set; }
            public string orderdat1 { get; set; }
            public string rsirdesc { get; set; }
            public string mrfno { get; set; }
            public string spcfdesc { get; set; }
            public string rsirunit { get; set; }
            public double ordrqty { get; set; }
            public double ordrrate { get; set; }
            public double ordramt { get; set; }
            public string pactdesc { get; set; }
            public string mrrno1 { get; set; }
            public string mrrdat { get; set; }
            public double mrrqty { get; set; }
            public double mrramt { get; set; }
            public string billno1 { get; set; }
            public double billqty { get; set; }
            public double billamt { get; set; }
            public double acpaidamt { get; set; }
            public double balbonpo { get; set; }
            public double balbonmrr { get; set; }
            public double balbonbill { get; set; }
            public string vounum1 { get; set; }
            public double acpaidqty { get; set; }
            public double ordbalqty { get; set; }
            public double ordbalamt { get; set; }
            public double mrrbalqty { get; set; }
            public double mrrbalamt { get; set; }
            public double bilbalqty { get; set; }
            public double bilbalamt { get; set; }
            public RptWorkOrdHisResource() { }


        }

        #endregion

        #region
        [Serializable]
        public class RptPaymentSupWise
        {
            public string comcod { get; set; }
            public string billno { get; set; }
            public string billno1 { get; set; }
            public string billref { get; set; }
            public string billdat { get; set; }
            public string pactcode { get; set; }
            public string ssircode { get; set; }
            public string rsircode { get; set; }
            public string pactdesc { get; set; }
            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public string spcfdesc { get; set; }
            public double billqty { get; set; }
            public double billrat { get; set; }
            public double billamt { get; set; }
            public string paydate { get; set; }
            public double payamt { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string uncleared { get; set; }
            public string refnum { get; set; }
            public RptPaymentSupWise() { }



        }

        #endregion


        [Serializable]

        public class RptPurTrack01
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string genno { get; set; }
            public string gdate { get; set; }

            public string refno { get; set; }
            public string chlno { get; set; }
            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public string spcfdesc { get; set; }
            public double qty { get; set; }
            public double rate { get; set; }
            public double amt { get; set; }
            public string ssirdesc { get; set; }
            public string usrname { get; set; }

            public RptPurTrack01() { }
        }


        [Serializable]

        public class RptOrderTrack01
        {
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string reqno { get; set; }
            public string genno { get; set; }
            public string refno { get; set; }
            public string gdate { get; set; }
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public string ssircode { get; set; }
            public double qty { get; set; }
            public double rate { get; set; }
            public double amt { get; set; }
            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public string ssirdesc { get; set; }
            public string spcfdesc { get; set; }
            public string usirdesc { get; set; }

            public RptOrderTrack01() { }
        }




        #region
        [Serializable]
        public class RptWorkOrderStatus
        {

            public string comcod { get; set; }
            public string reqno { get; set; }
            public string reqno1 { get; set; }
            public DateTime reqdat { get; set; }

            public string reqdat1 { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string mrfno { get; set; }
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public double areqty { get; set; }
            public double progqty { get; set; }
            public double ordrqty { get; set; }
            public double rqty { get; set; }
            public string resdesc { get; set; }
            public string resunit { get; set; }
            public string spcfdesc { get; set; }



            public RptWorkOrderStatus() { }


        }



        [Serializable]
        public class RptWorkOrderStatus02
        {

            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }

            public string orderno { get; set; }
            public string orderno2 { get; set; }
            public DateTime orderdat { get; set; }
            public string pordref { get; set; }
            public string aprovno { get; set; }
            public string aprovno2 { get; set; }
            public string reqno { get; set; }
            public string reqno2 { get; set; }
            public string mrfno { get; set; }
            public string ssircode { get; set; }
            public string ssirdesc { get; set; }
            public string sirdesc { get; set; }
            public string sirunit { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public double ordrqty { get; set; }
            public double aprovqty { get; set; }
            public double aprovrate { get; set; }
            public double amount { get; set; }
            public string username { get; set; }
            public string orusername { get; set; }
            public string aprusername { get; set; }

            public RptWorkOrderStatus02() { }


        }


        #endregion

        #region

        [Serializable]
        public class RptBillConfirmation01
        {
            public string grp1 { set; get; }
            public string reqno { set; get; }
            public string reqno1 { set; get; }
            public string orderno { set; get; }
            public string orderno1 { set; get; }
            public string mrrno { get; set; }
            public string mrrno1 { set; get; }
            public string pactcode { set; get; }
            public string pactdesc { set; get; }
            public string rsircode { set; get; }
            public string spcfcod { set; get; }
            public string rsirdesc1 { set; get; }
            public string spcfdesc { set; get; }
            public string rsirunit { set; get; }
            public double mrrqty { set; get; }
            public double mrrrate { set; get; }
            public double mrramt { set; get; }
            public DateTime mrrdate { get; set; }
            public string mrrref { set; get; }
            public string chlnno { set; get; }
            public double chlnqty { set; get; }
            public string mrrnote { set; get; }
            public double var1 { set; get; }
            public string oissueno { set; get; }

            public RptBillConfirmation01() { }
        }

        #endregion

        [Serializable]
        public class EclassSuplistWithMat
        {
            public string comcod { get; set; }
            public string ssircode { get; set; }
            public string supdesc { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public string rsirunit { get; set; }
            public double rate { get; set; }
            public string cperson { get; set; }
            public string mobile { get; set; }
            public string email { get; set; }
            public string addr { get; set; }
        }
        [Serializable]
        public class EclassBudgetTracking
        {
            public string comcod { get; set; }
            public string reqno { get; set; }
            public string reqno1 { get; set; }
            public DateTime reqdat { get; set; }
            public string rsircode { get; set; }
            public string mrfno { get; set; }
            public string pactcode { get; set; }
            public double progqty { get; set; }
            public double prograte { get; set; }
            public double areqty { get; set; }
            public double areqamt { get; set; }
            public double ordrqty { get; set; }
            public double mrrqty { get; set; }
            public double mrramt { get; set; }
        }

        [Serializable]
        public class EclassReqAllOrderList
        {

            public string comcod { get; set; }
            public string reqno { get; set; }
            public DateTime aprovdat { get; set; }
            public DateTime orderdat { get; set; }
            public string aprovno { get; set; }
            public string mrfno { get; set; }
            public string pordref { get; set; }
            public string orderno { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string ssircode { get; set; }
            public string ssirdesc { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public double preqty { get; set; }
            public double aprovqty { get; set; }
            public double areqty { get; set; }
            public double aprovrate { get; set; }
            public double ordrqty { get; set; }
            public string approved { get; set; }
            public bool chk { get; set; }
        }

        [Serializable]
        public class EclassReqAllMrrList
        {

            public string comcod { get; set; }
            public string reqno { get; set; }
            public DateTime mrrdat { get; set; }
            public DateTime billdat { get; set; }
            public string mrrno { get; set; }
            public string mrrref { get; set; }
            public string billno { get; set; }
            public string billref { get; set; }
            public string orderno { get; set; }
            public string ssircode { get; set; }
            public string ssirdesc { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public double billqty { get; set; }
            public string vounum { get; set; }
            public double mrrqty { get; set; }
        }

        [Serializable]

        public class RptBillTracking
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string genno { get; set; }
            public string refno { get; set; }
            public string gdate { get; set; }
            public DateTime date01 { get; set; }
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public string ssircode { get; set; }
            public double qty { get; set; }
            public double rate { get; set; }
            public double amt { get; set; }
            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public string ssirdesc { get; set; }
            public string spcfdesc { get; set; }
            public double duration { get; set; }
            public double cduration { get; set; }
            public string usrname { get; set; }

            public RptBillTracking() { }
        }

        [Serializable]
        public class GeneralBillTracking
        {
            public string pactdesc { get; set; }
            public string billno { get; set; }
            public string billref { get; set; }
            public string billdate { get; set; }
            public string orderno { get; set; }
            public string mrrno { get; set; }
            public string matdesc { get; set; }
            public string spcdesc { get; set; }
            public string unit { get; set; }
            public double qty { get; set; }
            public double amt { get; set; }
            public double rate { get; set; }
            public string supdesc { get; set; }
            public string vounum { get; set; }
            public string reqno { get; set; }
            public string mrfno { get; set; }
            public GeneralBillTracking() { }

        }


        [Serializable]
        public class RptPrjWiseMrfHistory
        {
            //a.comcod, a.pactcode, pactdesc=isnull(b.actdesc,''), a.reqqty, a.procesqty, a.orderqty, a.mrqty, a.billqty
            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public double reqqty { get; set; }
            public double procesqty { get; set; }
            public double orderqty { get; set; }
            public double mrqty { get; set; }
            public double billqty { get; set; }

            public RptPrjWiseMrfHistory() { }

        }

        [Serializable]
        public class DateWiseReqCheckHistory
        {
            //comcod	pactcode	pactdesc	mrfno	nreqQty	checkdat1
            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string mrfno { get; set; }
            public double nreqQty { get; set; }
            public DateTime checkdat1 { get; set; }
            
            public DateWiseReqCheckHistory() { }

        }
         
        [Serializable] 
        public class ChequeSheet01
        {

            //comcod,slnum,actcode, actdesc=replace(actdesc,'WIP-',''), rescode, sirdesc, spcfcod,billno,amount,payapdat,postedbyid,fdate
            public string comcod { get; set; }
            public string slnum { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string rescode { get; set; }
            public string sirdesc { get; set; }
            public string spcfcod { get; set; }
            public string billno { get; set; }
            public double amount { get; set; }

            public DateTime payapdat { get; set; }
            public string postedbyid { get; set; }
            public DateTime fdate { get; set; }

            public ChequeSheet01() { }
        }

        [Serializable]
        public class ComparativeStatementCreate
        {
            public string comcod { get; set; }
            public string ssircode { get; set; }
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public decimal rate { get; set; }
            public decimal stkqty { get; set; }
            public decimal areqty { get; set; }
            public decimal propqty { get; set; }
            public decimal csreqqty { get; set; }
            public decimal propqty1 { get; set; }
            public decimal amount { get; set; }
            public string supdesc { get; set; }
            public string rsirdesc { get; set; }
            public string spcfdesc { get; set; }
            public string rsirunit { get; set; }
            public decimal istpurate { get; set; }
            public string payment { get; set; }
            public string cperson { get; set; }
            public string mobile { get; set; }
            public decimal leadtime { get; set; }
            public decimal auditrate { get; set; }
            public string paytype { get; set; }
            public decimal advamt { get; set; }

        }

        [Serializable]
        public class DeliveryEffciency {

            //comcod, pactcode, pactdesc, mrfno, reqdat, rsircode, rsirdesc, unit, qty, recdat, daddat, leadtime, actdat, daystak, dayvar, remrks
            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; } 
            public string mrfno { get; set; }
            public DateTime reqdat { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string unit { get; set; }
            public decimal qty { get; set; }
            public DateTime recdat { get; set; }
            public DateTime daddat { get; set; }
            public decimal leadtime { get; set; }
            public string actdat { get; set; }
            public int daystak { get; set; }
            public decimal dayvar { get; set; }
            public string remrks { get; set; }

            public DeliveryEffciency() { 
            }
        }
        [Serializable]

        public class PurOrderTopSheet
        {
            public string pactdesc { get; set; }
            public string ssirdesc { get; set; }
            public string cactdesc { get; set; }
            public string orderno { get; set; }
            public DateTime orderdat { get; set; }

            public string oissueno { get; set; }

            public double amount { get; set; }
            public string deldate { get; set; }

            public string mrrdat { get; set; }

            public string chlnno { get; set; }
            public string pactcode { get; set; }

            public string mrfno { get; set; }
            public string reqdat { get; set; }

            public string checkdat { get; set; }

            public string crmcheckdat { get; set; }

            public string expusedt { get; set; }

            public PurOrderTopSheet()
            {

            }
        }

        [Serializable]
        public class RptSupAdvanceDetails
        {
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string rsirdesc { get; set; }
            public DateTime orderdat { get; set; }

            public string orderno1 { get; set; }

            public string oissueno { get; set; }
            public DateTime mrrdat { get; set; }
            public string chlnno { get; set; }
            public double amount { get; set; }
            public double taxam { get; set; }
            public double netamount { get; set; }
            public double payment { get; set; }
            public double actualorder { get; set; }
            public string chequeno { get; set; }

            public string voudat { get; set; }
            public string rmrks { get; set; }

            public RptSupAdvanceDetails()
            {

            }
        }
        [Serializable]

        public class OtherCollHistory
        {
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string refno { get; set; }
            public string mrno { get; set; }
            public  DateTime mrdate { get; set; }
            public string chqno { get; set; }

            public DateTime paydate { get; set; }
            public string bankname { get; set; }
            public double paidamt { get; set; }
            public DateTime recondate { get; set; }

            public OtherCollHistory()
            {

            }
        }

    }
}