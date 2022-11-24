using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_17_Acc
{
    public class EClassDB_BO
    {
        [Serializable]
        public class EClassAccYearly
        {
            public string yearid { set; get; }
            public double dram { set; get; }
            public double cram { set; get; }
            public EClassAccYearly(string yearid, double dram, double cram)
            {
                this.yearid = yearid;
                this.dram = dram;
                this.cram = cram;
            }
        }
        [Serializable]
        public class EClassAccMonthly
        {
            public string yearmon { set; get; }
            public string yearmon1 { set; get; }
            public double dram { set; get; }
            public double cram { set; get; }
            public double dramcore { set; get; }
            public double cramcore { set; get; }
            public EClassAccMonthly(string yearmon, string yearmon1, double dram, double cram, double dramcore, double cramcore)
            {
                this.yearmon = yearmon;
                this.yearmon1 = yearmon1;
                this.dram = dram;
                this.cram = cram;
                this.dramcore = dramcore;
                this.cramcore = cramcore;
            }
        }
        [Serializable]
        public class EClassAccWeekly
        {
            public string wcode1 { set; get; }
            public double wpamt1 { set; get; }
            public double wramt1 { set; get; }

            public string wcode2 { set; get; }
            public double wpamt2 { set; get; }
            public double wramt2 { set; get; }

            public string wcode3 { set; get; }
            public double wpamt3 { set; get; }
            public double wramt3 { set; get; }

            public string wcode4 { set; get; }
            public double wpamt4 { set; get; }
            public double wramt4 { set; get; }

            public double brec { set; get; }
            public double bpay { set; get; }

            public EClassAccWeekly(string wcode1, double wpamt1, double wramt1, string wcode2, double wpamt2, double wramt2, string wcode3, double wpamt3, double wramt3,
                    string wcode4, double wpamt4, double wramt4, double brec, double bpay)
            {
                this.wcode1 = wcode1;
                this.wpamt1 = wpamt1;
                this.wramt1 = wramt1;
                this.wcode2 = wcode2;
                this.wpamt2 = wpamt2;
                this.wramt2 = wramt2;
                this.wcode3 = wcode3;
                this.wpamt3 = wpamt3;
                this.wramt3 = wramt3;
                this.wcode4 = wcode4;
                this.wpamt4 = wpamt4;
                this.wramt4 = wramt4;
                this.brec = brec;
                this.bpay = bpay;
            }
        }
        [Serializable]
        public class ReceptPayment
        {
            public string grp1 { get; set; }
            public string grprpdesc { get; set; }
            public string grppaydesc { get; set; }
            public string rowid { get; set; }
            public string comcod { get; set; }
            public string recpcode { get; set; }
            public string recpdesc { get; set; }
            public double recpam { get; set; }
            public double trecpam { get; set; }
            public string paycode { get; set; }
            public string paydesc { get; set; }
            public string pleb2 { get; set; }
            public double payam { get; set; }
            public double tpayam { get; set; }
            public ReceptPayment() { }
        }

        [Serializable]
        public class EClassLPROYearly
        {
            public string yearid { set; get; }
            public double puram { set; get; }
            public double paymntam { set; get; }
            public EClassLPROYearly(string yearid, double puram, double paymntam)
            {
                this.yearid = yearid;
                this.puram = puram;
                this.paymntam = paymntam;
            }
        }
        [Serializable]
        public class EClassPROMonthly
        {
            public string yearmon { set; get; }
            public string yearmon1 { set; get; }
            public double puram { set; get; }
            public double paymntam { set; get; }
            public EClassPROMonthly(string yearmon, string yearmon1, double puram, double paymntam)
            {
                this.yearmon = yearmon;
                this.yearmon1 = yearmon1;
                this.puram = puram;
                this.paymntam = paymntam;
            }
        }

        [Serializable]
        public class EClassPROWeekly
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


            public EClassPROWeekly(string wcode1, double wpamt1, double wpayamt1, string wcode2, double wpamt2, double wpayamt2, string wcode3, double wpamt3, double wpayamt3,
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
        public class NoteIncoStatement
        {
            public string comcod { get; set; }
            public string actcod4 { get; set; }
            public string actdesc4 { get; set; }
            public double curam { get; set; }
            public double opnam { get; set; }
            public double closam { get; set; }
            public double percentcu { get; set; }
            public double cpercent { get; set; }
            public NoteIncoStatement() { }

        }


        [Serializable]
        public class AccRatioAnalysis ///grp, grpdesc , rcode, rdesc, rfourm, ratio, rstd, inter
        {
            public string comcode { get; set; }
            public string rcode { get; set; }
            public string rcode2 { get; set; }
            public string rcode3 { get; set; }
            public string rdesc { get; set; }
            public string rfourm { get; set; }
            public decimal rstd { get; set; }
            public string ratio { get; set; }
            public string grpdesc { get; set; }
            public string inter { get; set; }
            public string grp { get; set; }
            public string ratioprcen { get; set; }

        }


        [Serializable]
        public class DailyPaymentTrn
        {
            public string comcod { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public DateTime voudat { get; set; }
            public string voudat1 { get; set; }
            public string chequedat { get; set; }
            public string chequedat1 { get; set; }
            public string actcode { get; set; }
            public string cactcode { get; set; }
            public string rescode { get; set; }
            public string actdesc { get; set; }
            public string cactdesc { get; set; }
            public string resdesc { get; set; }
            public double depam { get; set; }
            public double payam { get; set; }
            public string trnrmrk { get; set; }
            public string refnum { get; set; }
            public string vounar { get; set; }
            public string payto { get; set; }
            public DailyPaymentTrn() { }
        }

        public class AccTrialBl1
        {
            //Iqbal Nayan
            public string comcod { get; set; }
            public string actcode1 { get; set; }
            public string actdesc1 { get; set; }
            public string actcode2 { get; set; }
            public string actdesc2 { get; set; }
            public string actcode3 { get; set; }
            public string actdesc3 { get; set; }
            public string actcode4 { get; set; }
            public string actdesc4 { get; set; }
            public Decimal opnam { get; set; }
            public Decimal opndram { get; set; }
            public Decimal opncram { get; set; }
            public Decimal dram { get; set; }
            public Decimal cram { get; set; }
            public Decimal closdram { get; set; }
            public Decimal closcram { get; set; }
            public Decimal curam { get; set; }
            public Decimal closam { get; set; }
            public Decimal netdram { get; set; }
            public Decimal netcram { get; set; }
            public string mainhead { get; set; }
            public string leb2 { get; set; }
            public string drcr { get; set; }
            public string rescode { get; set; }
            public string spcfcod { get; set; }
            public string resdesc { get; set; }
            public AccTrialBl1() { }
        }
        [Serializable]

        public class PaymentVouClas1
        {
            //Iqbal Nayan
            public string vounum { get; set; }
            public string mactcode { get; set; }
            public string grp1 { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string spclcode { get; set; }
            public string spdesc { get; set; }
            public string billno { get; set; }
            public string billrefno { get; set; }
            public string trnrmrk { get; set; }
            public string unit { get; set; }
            public double Dr { get; set; }
            public double Cr { get; set; }
            public double totalamt { get; set; }
            public double trnqty { get; set; }
            public double trnrat { get; set; }
            public PaymentVouClas1() { }
        }
        [Serializable]
        public class PaymentVouClas2
        {
            // Iqbal nayan
            public string vounum { get; set; }
            public DateTime voudat { get; set; }
            public string isunum { get; set; }
            public string refnum { get; set; }
            public string voutyp { get; set; }
            public string srinfo { get; set; }
            public string venar { get; set; }
            public string payto { get; set; }
            public string cactcode { get; set; }
            public string cactdesc { get; set; }
            public double tdram { get; set; }
            public double tcram { get; set; }
            public DateTime posteddat { get; set; }
            public string banknam { get; set; }
            public string postedbyid { get; set; }
            public string postuser { get; set; }
            public PaymentVouClas2() { }
        }

        public class AccLedger1
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string comcod { get; set; }
            public string actcode { get; set; }
            public string head1 { get; set; }
            public DateTime voudat { get; set; }
            public string vounum { get; set; }
            public string cactcode { get; set; }
            public string cactdesc { get; set; }
            public Decimal trnam { get; set; }
            public Decimal trnqty { get; set; }
            public Decimal dram { get; set; }
            public Decimal cram { get; set; }
            public Decimal balamt { get; set; }
            public string trnrmrk { get; set; }
            public string refnum { get; set; }
            public string srinfo { get; set; }
            public string venar1 { get; set; }
            public string venar2 { get; set; }
            public string voudat1 { get; set; }
            public Decimal trnrate { get; set; }
            public string vounum2 { get; set; }
            public string rescode { get; set; }
            public string resdesc { get; set; }

            public string billno { get; set; }
            public string payto { get; set; }
            public AccLedger1() { }
        }

        [Serializable]
        //Iqbal Nayan
        public class SpLedger
        {
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string actcode { get; set; }
            public string cactcode { get; set; }
            public string actdesc { get; set; }
            public string cactdesc { get; set; }
            public string head1 { get; set; }
            public DateTime voudat { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string rescode { get; set; }
            public string resdesc { get; set; }
            public double opam { get; set; }
            public double trnam { get; set; }
            public double trnrate { get; set; }
            public double trnqty { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double clsam { get; set; }
            public double rate { get; set; } 
            public string trnrmrk { get; set; }
            public string refnum { get; set; }
            public string chequedat { get; set; }
            public string srinfo { get; set; }
            public string venar { get; set; }
            public string voudat1 { get; set; }
            public SpLedger() { }

        }



        [Serializable]
        //Iqbal Nayan
        public class RptSupPayment
        {

            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string voudat1 { get; set; }
            public string vounum1 { get; set; }
            public string trnrmrk { get; set; }
            public double cram { get; set; }
            public double dram { get; set; }
            public Decimal balamt { get; set; }
            public RptSupPayment() { }

        }


        [Serializable]
        //Iqbal Nayan
        public class RptSupPayment02
        {
            public string voudat1 { get; set; }
            public string vounum1 { get; set; }
            public string refnum { get; set; }
            public string actdesc { get; set; }
            public double opnam { get; set; }
            public double billamt { get; set; }
            public double payam { get; set; }
            public double clsam { get; set; }
            public RptSupPayment02() { }

        }





        public class LedgerinSch
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string comcod { get; set; }
            public string actcode { get; set; }
            public string head1 { get; set; }
            public DateTime voudat { get; set; }
            public string vounum { get; set; }
            public string cactcode { get; set; }
            public string cactdesc { get; set; }
            public Decimal trnam { get; set; }
            public Decimal trnqty { get; set; }
            public Decimal dram { get; set; }
            public Decimal cram { get; set; }
            public Decimal balamt { get; set; }
            public string trnrmrk { get; set; }
            public string refnum { get; set; }
            public string srinfo { get; set; }
            public string venar1 { get; set; }
            public string venar2 { get; set; }
            public string voudat1 { get; set; }
            public Decimal trnrate { get; set; }
            public string vounum2 { get; set; }
            public string rescode { get; set; }
            public string resdesc { get; set; }
            public string insdate { get; set; }
            public double insamt { get; set; }
            public LedgerinSch() { }
        }


        [Serializable]
        public class PendingBil
        {
            public string pactcode { get; set; }
            public string ssircode { get; set; }
            public string ssirdesc { set; get; }
            public double pactdesc { set; get; }
            public double billno1 { set; get; }
            //public string tdate { get; set; }
            public string billref { get; set; }
            public double amt { get; set; }
            public PendingBil()
            {

            }
        }


        [Serializable]
        public class SupPayPro
        {
            public string billno1 { get; set; }
            public string mslnum1 { get; set; }
            public string slnum { get; set; }
            public string valdate { get; set; }
            public DateTime rcvdate { get; set; }
            public string actdesc { get; set; }
            public string resdesc { get; set; }
            public double billamt1 { get; set; }
            public DateTime apppaydate { get; set; }
            public double amt { get; set; }
            public double advamt { get; set; }
            public double netamt { get; set; }
            public string billndesc { get; set; }
            public string paydesc { get; set; }
            public string refno { get; set; }
            public SupPayPro() { }
        }


        [Serializable]
        public class RptBgdvsExpense
        {
            public string comcod { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public double bgdcost { get; set; }
            public double actcost { get; set; }
            public double preyrcost { get; set; }

            public RptBgdvsExpense() { }

        }

        [Serializable]
        public class bugdvExpensis
        {
            // IQBAL NAYAN
            public string comcod { get; set; }
            public string actcode1 { get; set; }
            public string actdesc1 { get; set; }
            public string actcode2 { get; set; }
            public string actdesc2 { get; set; }
            public string actcode3 { get; set; }
            public string actdesc3 { get; set; }
            public string actcode4 { get; set; }
            public string actdesc4 { get; set; }
            public string subcode1 { get; set; }
            public string subdesc1 { get; set; }
            public string sirunit { get; set; }
            public string subdesc4 { get; set; }
            public double closam { get; set; }
            public double closqty { get; set; }
            public double closrate { get; set; }
            public string mainhead { get; set; }
            public double bgdqty { get; set; }
            public double bgdrat { get; set; }
            public double bgdam { get; set; }
            public double tavqty { get; set; }
            public double tavrat { get; set; }
            public double tavamt { get; set; }
            public double taper { get; set; }
            public bugdvExpensis() { }
        }

        public class ChequeInHand01
        {
            //Iqbal Nayan
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string mrno { get; set; }
            public string chqno { get; set; }
            public string bankname { get; set; }
            public string bbranch { get; set; }
            public string mrdate { get; set; }
            public string paydate { get; set; }
            public double cashamt { get; set; }
            public double chqamt { get; set; }
            public string depositdat { get; set; }
            public string dhonrdate { get; set; }
            public string refno { get; set; }
            public string pactdesc { get; set; }
            public string udesc { get; set; }
            public string custname { get; set; }
            public string deptname { get; set; }
            public ChequeInHand01() { }


        }

        [Serializable]
        public class CqeCollChqSt
        {
            //Iqbal Nayan
            public string comcod { get; set; }
            public string slno { get; set; }
            public string mrno { get; set; }
            public string chqno { get; set; }
            public DateTime mrdate { get; set; }
            public DateTime paydate { get; set; }
            public string pactdesc { get; set; }
            public string udesc { get; set; }
            public string custname { get; set; }
            public string banname { get; set; }
            public string depositdat { get; set; }
            public string dhonrdate { get; set; }
            public string recndt { get; set; }
            public double paidamt { get; set; }
            public string collfrm { get; set; }

            public CqeCollChqSt() { }
        }
        [Serializable]
        public class ChequeReceiClear
        {
            // IQBAL NAYAN
            public string rowid { get; set; }
            public string comcod { get; set; }
            public string clvounum { get; set; }
            public string clmrno { get; set; }
            public string clchqno { get; set; }
            public DateTime rarcndate1 { get; set; }
            public string rarcndate { get; set; }
            public string clcode { get; set; }
            public string clrescode { get; set; }
            public string clcactcode { get; set; }
            public string recdate { get; set; }
            public double clcuram { get; set; }
            public double clpream { get; set; }
            public string recpcode { get; set; }
            public string recrescode { get; set; }
            public string mrno { get; set; }
            public string chqno { get; set; }
            public double recam { get; set; }
            public string clactdesc { get; set; }
            public string clcactdesc { get; set; }
            public string clresdesc { get; set; }
            public string recpdesc { get; set; }
            public string cresdesc { get; set; }
            public string ccactdesc { get; set; }
            public string bbranch { get; set; }
            public ChequeReceiClear() { }
        }
        [Serializable]
        public class RealCollDetails
        {
            //IQBAL NAYAN
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string mrno { get; set; }
            public string chqno { get; set; }
            public string bankname { get; set; }
            public string bbranch { get; set; }
            public string mrdate { get; set; }
            public string paydate { get; set; }
            public double cashamt { get; set; }
            public double chqamt { get; set; }
            public string refno { get; set; }
            public string pactdesc { get; set; }
            public string udesc { get; set; }
            public string custname { get; set; }
            public RealCollDetails() { }
        }


        [Serializable]
        public class vouPrint
        {
            // Iqbal Nayan
            public string vounum { get; set; }
            public string mactcode { get; set; }
            public string grp1 { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string spclcode { get; set; }
            public string spdesc { get; set; }
            public string billno { get; set; }
            public string billrefno { get; set; }
            public string trnrmrk { get; set; }
            public string unit { get; set; }
            public double Dr { get; set; }
            public double Cr { get; set; }
            public double totalamt { get; set; }
            public double trnqty { get; set; }
            public double trnrat { get; set; }
            //public string comlogo { get; set; }
            public vouPrint() { }
        }

        [Serializable]
        public class PostVoucherPrint
        {
            public string vounum { get; set; }
            public string mactcode { get; set; }
            public string grp1 { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string trnrmrk { get; set; }
            public string chequeno { get; set; }
            public DateTime chequedate { get; set; }
            public string isunum { get; set; }
            public string payto { get; set; }
            public string billno { get; set; }
            public string billrefno { get; set; }
            public string unit { get; set; }
            public double Dr { get; set; }
            public double Cr { get; set; }
            public double trnqty { get; set; }
            public double totalamt { get; set; }
            public string spcldesc { get; set; }

            
            public PostVoucherPrint() { }
        }

        [Serializable]
        public class ListIsssuChq
        {
            //  Iqbal  Nayan 
            public string comcod { get; set; }
            public string vounum { get; set; }
            public string cactcode { get; set; }
            public double trnamt { get; set; }
            public string bankname { get; set; }
            public string partyname { get; set; }
            public string cheqeno { get; set; }
            public string pmode { get; set; }
            public string voudat { get; set; }
            public string rmks { get; set; }
            public string pactdesc { get; set; }
            public string chequetype { get; set; }
            


            public ListIsssuChq() { }
        }


        [Serializable]
        public class ChqIssuClear
        {
            // Iqbal Nayan
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string typesum { get; set; }
            public string actcode { get; set; }
            public string rescode { get; set; }
            public string cactcode { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string voudat { get; set; }
            public string chequeno { get; set; }
            public string chequedat { get; set; }
            public double cramt { get; set; }
            public double dramt { get; set; }
            public double reconamt { get; set; }
            public string bcldate { get; set; }
            public string trnrmrk { get; set; }
            public string vnar { get; set; }
            public string acvounum { get; set; }
            public string actdesc { get; set; }
            public string cactdesc { get; set; }
            public string resdesc { get; set; }
            public string payto { get; set; }
            // public string recndt { get; set; }
            public ChqIssuClear() { }
        }



        [Serializable]
        public class ReceiClear
        {
            //Iqbal Nayan
            public string rowid { get; set; }
            public string comcod { get; set; }
            public string voudat { get; set; }
            public DateTime voudat1 { get; set; }
            public string actcode1 { get; set; }
            public string actdesc1 { get; set; }
            public string isunum { get; set; }
            public string chqno1 { get; set; }
            public string payto1 { get; set; }
            public double isuamt { get; set; }
            public string cactcode1 { get; set; }
            public string cactdesc1 { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string actcode2 { get; set; }
            public string actdesc2 { get; set; }
            public string cactcode2 { get; set; }
            public string cactdesc2 { get; set; }
            public string vounum2 { get; set; }
            public string chqno2 { get; set; }
            public string payto2 { get; set; }
            public string isudat { get; set; }
            public double recamt { get; set; }
            public double preamt { get; set; }
            public ReceiClear() { }
        }


        [Serializable]
        public class DueCollStatmentRe
        {
            // Iqbal Nayan
            public string rowid { get; set; }
            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string custname { get; set; }
            public string udesc { get; set; }
            public string mobile { get; set; }
            public double usize { get; set; }
            public double aptcost { get; set; }
            public double aptrate { get; set; }
            public double cpcost { get; set; }
            public double utltycost { get; set; }
            public double othcost { get; set; }
            public double tocost { get; set; }
            public double atodues { get; set; }
            public double todues { get; set; }
            public double reconamt { get; set; }
            public double retcheque { get; set; }
            public double fcheque { get; set; }
            public double pcheque { get; set; }
            public double ramt { get; set; }
            public double bamt { get; set; }
            public double pbookam { get; set; }
            public double pinsam { get; set; }
            public double ptodues { get; set; }
            public double cbookam { get; set; }
            public double cinsam { get; set; }
            public double ctodues { get; set; }
            public double vtodues { get; set; }
            public double cdelay { get; set; }
            public double discharge { get; set; }
            public double abookam { get; set; }
            public double ainsam { get; set; }
            public double adtodues { get; set; }
            public double nettodues { get; set; }
            public double ntodues { get; set; }
            public string pactdesc { get; set; }
            public DueCollStatmentRe() { }
        }

        // comcod,slno,  vounum, isunum,voudat, payto, actdesc, resdesc, varnar, cactdesc, chequeno, chequedat,recndt, trnam
        [Serializable]
        public class ChequeStatus
        {
            // Iqbal Nayan
            public string comcod { get; set; }
            public string slno { get; set; }
            public string vounum { get; set; }
            public string isunum { get; set; }
            public string voudat { get; set; }
            public string payto { get; set; }
            public string actdesc { get; set; }
            public string resdesc { get; set; }
            public string varnar { get; set; }
            public string cactdesc { get; set; }
            public string chequeno { get; set; }
            public DateTime chequedat { get; set; }
            public string recndt { get; set; }
            public double trnam { get; set; }
            public ChequeStatus() { }
        }

        [Serializable]
        public class DayWiseissueCheek
        {
            // Iqbal Nayan
            public string comcod { get; set; }
            public string grp { get; set; }
            public string comnam { get; set; }
            public string grpdesc { get; set; }
            public string typesum { get; set; }
            public string actcode { get; set; }
            public string rescode { get; set; }
            public string cactcode { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string voudat { get; set; }
            public string isunum { get; set; }
            public string chequeno { get; set; }
            public string chequedat { get; set; }
            public double cramt { get; set; }
            public double dramt { get; set; }
            public string newvocnum { get; set; }
            public string trnrmrk { get; set; }
            public string vnar { get; set; }
            public string acvounum { get; set; }
            public string actdesc { get; set; }
            public string cactdesc { get; set; }
            public string resdesc { get; set; }
            public string payto { get; set; }
            public string recndt { get; set; }
            public string billno { get; set; }
            public string chkmv { get; set; }
            public string i { get; set; }
            public DayWiseissueCheek() { }
        }

        //comcod,  typesum, actcode, rescode, cactcode, vounum,  vounum1 , voudat, isunum, chequeno, chequedat, payam, vnar,  actdesc, 
        //cactdesc,resdesc, payto

        [Serializable]
        public class DaywiseGpIssue
        {
            // Iqbal Nayan
            public string comcod { get; set; }
            public string typesum { get; set; }
            public string actcode { get; set; }
            public string rescode { get; set; }
            public string cactcode { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string voudat { get; set; }
            public string isunum { get; set; }
            public string chequeno { get; set; }
            public string chequedat { get; set; }
            public double payam { get; set; }
            public string vnar { get; set; }
            public string actdesc { get; set; }
            public string cactdesc { get; set; }
            public string resdesc { get; set; }
            public string payto { get; set; }
            public DaywiseGpIssue() { }
        }


        [Serializable]
        public class EClassTranList
        {
            // Iqbal Nayan
            public string rowid { get; set; }
            public string comcod { get; set; }
            public DateTime voudat { get; set; }
            public string voudat1 { get; set; }
            public string vounum1 { get; set; }
            public string vounum { get; set; }
            public string acrescode { get; set; }
            public string acresdesc { get; set; }
            public double trnqty { get; set; }
            public double inneram { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public string trnrmrk { get; set; }
            public string refnum { get; set; }
            public string srinfo { get; set; }
            public string venarr { get; set; }
            public string voutyp { get; set; }
            public string spcldesc { get; set; }
            public string refvno { get; set; }
            public string comsnam { get; set; }
            public string postedbydesc { get; set; }
            public string drcr { get; set; }
            public string actcode { get; set; }
            public string rescode { get; set; }
            public string spclcode { get; set; }
            public string payto { get; set; }
            public EClassTranList() { }
        }


        [Serializable]
        public class BankPosition
        {
            // Iqbal Nayan
            public string comcod { get; set; }
            public string actcode { get; set; }
            public double opnam { get; set; }
            public double opndram { get; set; }
            public double opncram { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double trnam { get; set; }
            public double isuamt { get; set; }
            public double closdram { get; set; }
            public double closcram { get; set; }
            public double closam { get; set; }
            public double collamt { get; set; }
            public double bankamt { get; set; }
            public double bankbal { get; set; }
            public double banklia { get; set; }
            public string actdesc { get; set; }
            public BankPosition() { }

        }
        [Serializable]
        public class PWRPDetails
        {
            // Iqbal Nayan
            public string rowid { get; set; }
            public string comcod { get; set; }
            public string recpcode { get; set; }
            public string recpdesc { get; set; }
            public double recpam { get; set; }
            public string paycode { get; set; }
            public string paydesc { get; set; }
            public double payam { get; set; }
            public string rleb2 { get; set; }
            public string pleb2 { get; set; }
            public string grppaydesc { get; set; }
            public string grprpdesc { get; set; }    


            public PWRPDetails() { }
        }

        [Serializable]
        public class IssuedVsColl
        {
            // Iqbal Nayan
            public string rowid { get; set; }
            public string comcod { get; set; }
            public string recpcode { get; set; }
            public string recpdesc { get; set; }
            public double recpam { get; set; }
            public string paycode { get; set; }
            public string paydesc { get; set; }
            public double payam { get; set; }
            public string rleb2 { get; set; }
            public string pleb2 { get; set; }
            public IssuedVsColl() { }
        }


        [Serializable]
        public class OppPayment1
        {
            //Iqbal Nayan
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string rpcode { get; set; }
            public string rpdesc { get; set; }
            public double payam1 { get; set; }
            public double payam2 { get; set; }
            public double payam3 { get; set; }
            public double payam4 { get; set; }
            public double payam5 { get; set; }
            public double tpay { get; set; }
            public string section { get; set; }
            public OppPayment1() { }
        }

        [Serializable]
        public class RescPayment0
        {
            //Iqbal Nayan
            public string grp1 { get; set; }
            public string grprpdesc { get; set; }
            public string grppaydesc { get; set; }
            public string rowid { get; set; }
            public string comcod { get; set; }
            public string recpcode { get; set; }
            public string recpdesc { get; set; }
            public string rleb2 { get; set; }
            public double recpam { get; set; }
            public string paycode { get; set; }
            public string paydesc { get; set; }
            public string pleb2 { get; set; }
            public double payam { get; set; }
            public RescPayment0() { }
        }

        [Serializable]
        public class RescPayment01
        {
            //Iqbal Nayan
            public double recpam { get; set; }
            public double payam { get; set; }
            public RescPayment01() { }
        }

        [Serializable]
        public class RescPayment02
        {
            //Iqbal Nayan
            public string comcod { get; set; }
            public string code { get; set; }
            public string codedesc { get; set; }
            public double opnam { get; set; }
            public double closam { get; set; }
            public double netbal { get; set; }
            public RescPayment02() { }
        }

        [Serializable]
        public class Cashflow
        {
            // iqbal Nayan
            public string comcod { get; set; }
            public string acgcode { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public double opnam { get; set; }
            public double closam { get; set; }
            public double curam { get; set; }
            public double changeam { get; set; }
            public string rleb2 { get; set; }
            public Cashflow() { }
        }


        [Serializable]
        public class AccTrialBal001
        {
            // iqbal Nayan
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string actcode { get; set; }
            public double opnam { get; set; }
            public double closam { get; set; }
            public double netbal { get; set; }
            public string actdesc { get; set; }
            public AccTrialBal001() { }
        }

        [Serializable]
        public class AccDetailsSchedul
        {
            public string comcod { get; set; }
            public string actcode1 { get; set; }
            public string actdesc1 { get; set; }
            public string actdesc { get; set; }
            public string actcode2 { get; set; }
            public string actdesc2 { get; set; }
            public string actcode3 { get; set; }
            public string actdesc3 { get; set; }
            public string actcode4 { get; set; }
            public string actdesc4 { get; set; }
            public string subcode1 { get; set; }
            public string subdesc1 { get; set; }
            public string subdesc4 { get; set; }
            public double opnam { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double closam { get; set; }
            public string mainhead { get; set; }
            public AccDetailsSchedul() { }
        }


        [Serializable]
        public class SaleDetailsSchedule
        {
            // Iqbal Nayan
            public string comcod { get; set; }
            public string actcode1 { get; set; }
            public string actcode2 { get; set; }
            public string actcode3 { get; set; }
            public string actcode4 { get; set; }
            public string subcode1 { get; set; }
            public string subcode4 { get; set; }
            public double opnam { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public string mindat { get; set; }
            public string maxdat { get; set; }
            public string custname { get; set; }
            public double closam { get; set; }
            public string actdesc4 { get; set; }
            public string subdesc1 { get; set; }
            public SaleDetailsSchedule() { }
        }

        [Serializable]
        public class AdvancedSummary
        {
            public string comcod { get; set; }
            public string rescode { get; set; }
            public double opnam { get; set; }
            public double opnbill { get; set; }
            public double opnadv { get; set; }
            public double trnam { get; set; }
            public double trnqty { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double clsbill { get; set; }
            public double clsadv { get; set; }
            public double inuamt { get; set; }
            public double isuamt { get; set; }



            public double billamt { get; set; }
            public double isubal { get; set; }
            public double isudr { get; set; }
            public double isucr { get; set; }
            public string resdesc { get; set; }
            public double mrramt { get; set; }
            public double netbill { get; set; }
            public double balamt { get; set; }
            public AdvancedSummary() { }
        }

        [Serializable]
        public class TrialHeadOf
        {
            public string comcod { get; set; }
            public string grpcode { get; set; }
            public string actcode4 { get; set; }
            public string actdesc4 { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double closdram { get; set; }
            public double closcram { get; set; }
            public double netdram { get; set; }
            public double netcram { get; set; }
            public double trnam { get; set; }
            public string leb2 { get; set; }
            public TrialHeadOf() { }


        }



        [Serializable]
        public class SaleReg2
        {

            // Iqbal  Nayan
            public string comcod { get; set; }
            public string cusname { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public double tusize { get; set; }
            public double tqty { get; set; }
            public double tuamt { get; set; }
            public double susize { get; set; }
            public double sqty { get; set; }
            public double srate { get; set; }
            public double suamt { get; set; }
            public double usqty { get; set; }
            public double urate { get; set; }
            public double usuamt { get; set; }
            public double disamt { get; set; }
            public double disper { get; set; }
            public double usize { get; set; }
            public string munit { get; set; }
            public string udesc { get; set; }
            public string pactdesc { get; set; }
            public string schdate { get; set; }
            public string datename { get; set; }
            public double colamt { get; set; }
            public double recvamt { get; set; }
            public string salteam { get; set; }
            public string salperson { get; set; }
            public SaleReg2() { }

        }

        [Serializable]
        public class TrialBalDetails
        {
            
            public string comcod { get; set; }
            public string actcode4 { get; set; }
            public string rescode4 { get; set; }
            public string rescode { get; set; }
            public double opnam { get; set; }
            public double trnam { get; set; }
            public double curam { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double closam { get; set; }
            public string actdesc4 { get; set; }
            public string actnotes { get; set; }
            public string resdesc { get; set; }
            public TrialBalDetails() { }
        }


        // iQBAL NAYAN
        [Serializable]
        public class OnlineBillReg
        {
            public string comcod { get; set; }
            public string slnum { get; set; }
            public string actcode { get; set; }
            public string rescode { get; set; }
            public string billno { get; set; }
            public string billno1 { get; set; }
            public DateTime chqdate { get; set; }
            public double amount { get; set; }
            public string bankcode { get; set; }
            public DateTime payapdat { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public DateTime fdate { get; set; }
            public string resdesc { get; set; }
            public string actdesc { get; set; }
            public string bankname { get; set; }
            public string checqno { get; set; }
            public OnlineBillReg() { }
        }

        [Serializable]
        public class RecePaya
        {
            //Iqbal Nayan
            public string rescode { get; set; }
            public string resdesc { get; set; }
            public double opndram { get; set; }
            public double opncram { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double closam { get; set; }
            public RecePaya() { }
        }

        [Serializable]
        public class InStIP
        {
            // Iqbal Nayan
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string actcode { get; set; }
            public string rescode { get; set; }
            public double trnqty { get; set; }
            public double trnrate { get; set; }
            public double trnam { get; set; }
            public string actdesc { get; set; }
            public string actdesc1 { get; set; }
            public string resdesc { get; set; }
            public string resunit { get; set; }
            public InStIP() { }
        }

        //Nayan Iqbal 
        [Serializable]
        public class ProjCostSales
        {
            // Iqbal Nayan
            public string comcod { get; set; }
            public string actcode { get; set; }
            public double lcost { get; set; }
            public double comcost { get; set; }
            public double dcost { get; set; }
            public double bgdcons { get; set; }
            public double conscost { get; set; }
            public double remcons { get; set; }
            public double saleamt { get; set; }
            public double collamt { get; set; }
            public double remcollamt { get; set; }
            public string actdesc { get; set; }
            public ProjCostSales() { }
        }

        [Serializable]
        public class BankStatementInfo
        {
            //Iqbal   Nayan
            public string grp { get; set; }
            public string grp1 { get; set; }
            public string comcod { get; set; }
            public DateTime voudat { get; set; }
            public string voudat1 { get; set; }
            public DateTime recndt { get; set; }
            public string recndt1 { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string cactcode { get; set; }
            public string cactdesc { get; set; }
            public string rescode { get; set; }
            public string resdesc { get; set; }
            public double depam { get; set; }
            public double payam { get; set; }
            public double balamt { get; set; }
            public string trnrmrk { get; set; }
            public string refnum { get; set; }
            public string chequedat1 { get; set; }
            public string vounar { get; set; }
            public string chequedat { get; set; }
            public string payto { get; set; }

            public string isunum { get; set; }
            public BankStatementInfo() { }
        }

        [Serializable]
        public class EClassSupplierProposedPayment
        {
            public string comcod { get; set; }
            public string rescode { get; set; }
            public string actcode { get; set; }
            public double opnam { get; set; }
            public double opnbill { get; set; }
            public double opnadv { get; set; }
            public double trnam { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double balamt { get; set; }
            public double clsbill { get; set; }
            public double clsadv { get; set; }
            public double proamt { get; set; }
            public double apramt { get; set; }
            public string resdesc { get; set; }
            public string actdesc { get; set; }

            public EClassSupplierProposedPayment() { }

        }

        [Serializable]
        public class EClassSubContractorProposedPayment
        {
            public string comcod { get; set; }
            public string rescode { get; set; }
            public string actcode { get; set; }
            public double opnam { get; set; }
            public double opnbill { get; set; }
            public double opnadv { get; set; }
            public double trnam { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double balamt { get; set; }
            public double clsbill { get; set; }
            public double clsadv { get; set; }
            public double proamt { get; set; }
            public double apramt { get; set; }
            public string resdesc { get; set; }
            public string actdesc { get; set; }

            public EClassSubContractorProposedPayment() { }


        }

        [Serializable]
        public class RegChequeHistory
        {

            public string comcod { get; set; }
            public string slno { get; set; }
            public string isunum { get; set; }
            public string recdat { get; set; }
            public string billnat { get; set; }
            public string payto { get; set; }
            public string actdesc { get; set; }
            public string refno { get; set; }
            public double trnam { get; set; }
            public string paydat { get; set; }

            public RegChequeHistory() { }
        }
        [Serializable]
        public class EClassPendingCliMod
        {
            public string comcod { get; set; }
            public string adno { get; set; }
            public string adno1 { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string usircode { get; set; }
            public string udesc { get; set; }
            public string cusname { get; set; }
            public string gcod { get; set; }
            public string gdesc { get; set; }
            public string addate { get; set; }
            public double qty { get; set; }
            public double amt { get; set; }
            public double rate { get; set; }

            public EClassPendingCliMod() { }


        }


        [Serializable]
        public class InvoiceP2P360
        {

            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string gcod { get; set; }
            public string gdesc { get; set; }

            public double amount { get; set; }

            public InvoiceP2P360() { }
        }


        [Serializable]
        public class FinancialPosition02
        {
            public string actcode4 { get; set; }
            public string actdesc4 { get; set; }
            public double opnam { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double closam { get; set; }
            public double trnam { get; set; }

        }

        [Serializable]
        public class WorkingBudgetVsAchievement
        {
            public string grpcode { get; set; }
            public string rescode { get; set; }
            public string actcode { get; set; }
            public string grpdesc { get; set; }
            public string actdesc { get; set; }
            public string resdesc { get; set; }
            public double bgdamt { get; set; }
            public double acamt { get; set; }
            public double diffamt { get; set; }

            public WorkingBudgetVsAchievement() { }
        }



    }
}