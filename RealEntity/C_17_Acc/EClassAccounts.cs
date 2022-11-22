using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_17_Acc
{
    public class EClassAccounts
    {
        #region Accounts Code (Ontime)

        [Serializable]
        public class EClassAccCode
        {
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string actelev { get; set; }
            public string acttype { get; set; }
            public string acttdesc { get; set; }
            public string userdesc { get; set; }






            public EClassAccCode()
            {

            }

            public EClassAccCode(string actcode, string actdesc, string actelev, string acttype, string acttdesc, string userdesc)
            {
                this.actcode = actcode;
                this.actdesc = actdesc;
                this.actelev = actelev;
                this.acttype = acttype;
                this.acttdesc = acttdesc;
                this.userdesc = userdesc;

            }
        }




        [Serializable]
        public class EClassSupplierBill
        {


            public string actcode { get; set; }
            public string subcode { get; set; }
            public string actdesc { get; set; }
            public string subdesc { get; set; }
            public double trnqty { get; set; }
            public double trnrate { get; set; }
            public double trndram { get; set; }
            public double trncram { get; set; }







            public EClassSupplierBill()
            {

            }

            public EClassSupplierBill(string actcode, string subcode, string actdesc, string subdesc, double trnqty, double trnrate, double trndram, double trncram)
            {
                this.actcode = actcode;
                this.subcode = subcode;
                this.actdesc = actdesc;
                this.subdesc = subdesc;
                this.trnqty = trnqty;
                this.trnrate = trnrate;
                this.trndram = trndram;
                this.trncram = trncram;

            }
        }

        [Serializable]
        public class GeneralAdminOverH
        {
            public string comcod { get; set; }
            public string actcode4 { get; set; }
            public string actdesc4 { get; set; }
            public double curam { get; set; }
            public double opnam { get; set; }
            public double closam { get; set; }
            public double percentcu { get; set; }
            public double cpercent { get; set; }
            public GeneralAdminOverH() { }
        }


        //   comcod, comnam, grpcode, actcode1, actdesc1, actcode2, actdesc2, actcode3, actdesc3, actcode4, actdesc4, 
        //opnam, dram, cram, curam = trnam, closam, changeam, mainhead, cpercent, percentcu 

        [Serializable]
        public class CompIncome
        {
            public string comcod { get; set; }
            public string comnam { get; set; }
            public string grpcode { get; set; }
            public string actcode1 { get; set; }
            public string actdesc1 { get; set; }
            public string actcode2 { get; set; }
            public string actdesc2 { get; set; }
            public string actcode3 { get; set; }
            public string actdesc3 { get; set; }
            public string actcode4 { get; set; }
            public string actdesc4 { get; set; }
            public double opnam { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double curam { get; set; }
            public double closam { get; set; }
            public double changeam { get; set; }
            public string mainhead { get; set; }
            public double cpercent { get; set; }
            public double percentcu { get; set; }
            public double percnt { get; set; }
            public CompIncome() { }
        }

        public class CompIncome01
        {
            public double recnum { get; set; }
            public double opnam { get; set; }
            public double opndram { get; set; }
            public double opncram { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double curam { get; set; }
            public double closam { get; set; }
            public double changeam { get; set; }
            public double closdram { get; set; }
            public double closcram { get; set; }
            public CompIncome01() { }
        }


        #endregion

        [Serializable]
        public class NoteIncoStatement
        {
            public string comcod { get; set; }
            public string actcode4 { get; set; }
            public string actdesc4 { get; set; }
            public double curam { get; set; }
            public double opnam { get; set; }
            public double closam { get; set; }
            public double percentcu { get; set; }
            public double cpercent { get; set; }
            public NoteIncoStatement() { }

        }


        [Serializable]
        public class Rptspbalancesheet
        {
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string comnam { get; set; }
            public string actcode4 { get; set; }
            public string actdesc4 { get; set; }
            public double opnam { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double curam { get; set; }
            public double closam { get; set; }
            public string mainhead { get; set; }


            public Rptspbalancesheet() { }

        }
        [Serializable]
        public class BankCheque
        {
            public string comcod { get; set; }
            public string bankcode { get; set; }
            public string chequeno { get; set; }
            public string flag { get; set; }
            public string vounum { get; set; }
            public string bstatus { get; set; }



            public BankCheque() { }


        }


        [Serializable]
        public class EclassPettyCash
        {
            public string empid { get; set; }
            public string empname { get; set; }
            public string pcblno { get; set; }
            public string pcblno1 { get; set; }
            public string vounum { get; set; }
            public double itmcount { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string sircode { get; set; }
            public string sirdesc { get; set; }
            public string billno { get; set; }
            public DateTime billdate { get; set; }
            public string partculrs { get; set; }
            public double amount { get; set; }
            public string remarks { get; set; }
            public bool chkmv { get; set; }
            public string newvocnum { get; set; }


            public EclassPettyCash() { }
            public EclassPettyCash(string actcode, string actdesc, string sircode, string sirdesc, string billno, DateTime billdate, string partculrs, double amount, string remarks, bool chkmv, string newvocnum)
            {
                this.actcode = actcode;
                this.actdesc = actdesc;
                this.sircode = sircode;
                this.sirdesc = sirdesc;
                this.billno = billno;
                this.billdate = billdate;
                this.partculrs = partculrs;
                this.amount = amount;
                this.remarks = remarks;
                this.chkmv = chkmv;
                this.newvocnum = newvocnum;

            }
        }

        [Serializable]
        public class EclassPettyCashEmp
        {
            public string comcod { get; set; }
            public string bndlno { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string designation { get; set; }
            public string deptname { get; set; }
            public string pcblno { get; set; }
            public string billdate { get; set; }
            public EclassPettyCashEmp() { }
        }

        [Serializable]
        public class EClassBankInterest
        {
            public string comcod { get; set; }
            public string actcode { get; set; }
            public string cactcode { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string voudat { get; set; }
            public string checkno { get; set; }
            public string narration { get; set; }
            public double trnam { get; set; }
            public string trnrmrk { get; set; }

            public EClassBankInterest()
            {

            }
        }    

        [Serializable]
        public class EClassProjectReport
        {
            public string grp1 { get; set; }
            public string comcod { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string subcode1 { get; set; }
            public string sirunit { get; set; }
            public string subdesc1 { get; set; }
            public string subdesc4 { get; set; }
            public double opnam { get; set; }
            public double opnqty { get; set; }
            public double oprate { get; set; }
            public double trnam { get; set; }
            public double cramt { get; set; }
            public double trnqty { get; set; }
            public double closdramt { get; set; }
            public double closcramt { get; set; }
            public double closqty { get; set; }
            public string mainhead { get; set; }
            public double dramt { get; set; }
            public double closeamt { get; set; }
            public EClassProjectReport() {}
        }

        [Serializable]
        public class CashBankGrpReport
        {

            public string actcode { get; set; }
            public string actdesc { get; set; }
            public double clscr { get; set; }
            public double clsdr { get; set; }
            public double clsam { get; set; }
            public CashBankGrpReport()
            {

            }
        }
        [Serializable]
        public class CashBankGrpMonthRpt
        {

            public string monthnam { get; set; }

            public double dram { get; set; }
            public double cram { get; set; }
            public double clsam { get; set; }
            public string drcr { get; set; }

            public CashBankGrpMonthRpt()
            {

            }

        }

        [Serializable]
        public class CashBankGrpMonthDtsRpt
        {

            public string grpdesc { get; set; }
            public string voudat1 { get; set; }
            public string cactcode { get; set; }
            public string cactdesc { get; set; }

            public string vounum1 { get; set; }
            public string refnum { get; set; }
            public string resdesc { get; set; }
            public string venar1 { get; set; }
            public string venar2 { get; set; }
            public double trnqty { get; set; }
            public double trnrate { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double balamt { get; set; }
            public string trnrmrk { get; set; }
            public string username { get; set; }
            public CashBankGrpMonthDtsRpt ( )
            {

            }
        }

        [Serializable]
        public class FlrWiseConPrg
        {
            public string rescode { get; set; }
            public string resdesc { get; set; }
            public double topamt    { get; set; }
            public double p1{ get; set; }
            public double p2{ get; set; }
            public double p3{ get; set; }
            public double p4 { get; set; }
            public double p5 { get; set; }
            public double p6 { get; set; }
            public double p7 { get; set; }
            public double p8 { get; set; }
            public double p9 { get; set; }
            public double p10 { get; set; }
            public double p11 { get; set; }
            public double p12 { get; set; }
            public double p13 { get; set; }
            public double p14 { get; set; }
            public double p15 { get; set; }
            public double p16 { get; set; }
            public double p17 { get; set; }
            public double p18 { get; set; }
            public double p19 { get; set; }
            public double p20 { get; set; }
            public double p21 { get; set; }
            public double p22 { get; set; }
            public double p23 { get; set; }
            public double p24 { get; set; }
            public double p25 { get; set; }
            public double p26 { get; set; }
            public double p27 { get; set; }
            public double p28 { get; set; }
            public double p29 { get; set; }
            public double p30 { get; set; }
            public double p31 { get; set; }
            public double p32 { get; set; }
            public double p33 { get; set; }
            public double p34 { get; set; }
            public double p35 { get; set; }
            public double p36 { get; set; }
            public double p37 { get; set; }
            public double p38 { get; set; }
            public double p39 { get; set; }
            public double p40 { get; set; }
            public double p41 { get; set; }
            public double p42 { get; set; }
            public double p43 { get; set; }
            public double p44 { get; set; }
            public double p45 { get; set; }
            public double p46 { get; set; }
            public double p47 { get; set; }
            public double p48 { get; set; }
            public double p49 { get; set; }
            public double p50 { get; set; }
            public double p51 { get; set; }
            public double p52 { get; set; }
            public double p53 { get; set; }
            public double p54 { get; set; }
            public double p55 { get; set; }
            public double p56 { get; set; }
            public double p57 { get; set; }
            public double p58 { get; set; }
            public double p59 { get; set; }
            public double p60 { get; set; }
            public double p61 { get; set; }
            public double p62 { get; set; }
            public double p63 { get; set; }
            public double p64 { get; set; }
            public double p65 { get; set; }
            public double p66 { get; set; }
            public double p67 { get; set; }
            public double p68 { get; set; }
            public double p69 { get; set; }
            public double p70 { get; set; }
            public double p71 { get; set; }
            public double p72 { get; set; }
            public double p73 { get; set; }
            public double p74 { get; set; }
            public double p75 { get; set; }
            public double p76 { get; set; }
            public double p77 { get; set; }
            public double p78 { get; set; }
            public double p79 { get; set; }
            public double p80 { get; set; }
            public double p81 { get; set; }
            public double p82 { get; set; }
            public double p83 { get; set; }
            public double p84 { get; set; }
            public double p85 { get; set; }
            public double p86 { get; set; }
            public double p87 { get; set; }
            public double p88 { get; set; }
            public double p89 { get; set; }
            public double p90 { get; set; }
            public double p91 { get; set; }
            public double p92 { get; set; }
            public double p93 { get; set; }
            public double p94 { get; set; }
            public double p95 { get; set; }
            public double p96 { get; set; }
            public double p97 { get; set; }
            public double p98 { get; set; }
            public double p99 { get; set; }
            public double p100 { get; set; }

            public FlrWiseConPrg ( )
            {

            }
           
        }
        [Serializable]
        public class RptCashCrPur
        {
            public string sirdesc { get; set; }
            public string rescode { get; set; }
            public double trnqty { get; set; }
            public double rate { get; set; }
            public double amount { get; set; }
            public RptCashCrPur()
            {

            }
        }

        [Serializable]
        public class Rptlabour
        {

            public string sirdesc { get; set; }
            public string rescode { get; set; }
            public double amount { get; set; }
            public Rptlabour ( )
            {

            }
        }

 

        [Serializable]
        public class PrijectCostN
        {
            //Iqbal Nayan
            public string grp1 { get; set; }
            public string comcod { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string subcode1 { get; set; }
            public string sirunit { get; set; }
            public string subdesc1 { get; set; }
            public string subdesc4 { get; set; }
            public double opnam { get; set; }
            public double opnqty { get; set; }
            public double oprate { get; set; }
            public double trnam { get; set; }
            public double dramt { get; set; }
            public double cramt { get; set; }
            public double trnqty { get; set; }
            public double closdramt { get; set; }
            public double closcramt { get; set; }
            public double closqty { get; set; }
            public double closamt { get; set; }
            public string mainhead { get; set; }
            public PrijectCostN() { }
        }


        [Serializable]
        public class BankInterest
        {

            public string rowid { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string comcod { get; set; }
            public string actcode { get; set; }
            public string head1 { get; set; }
            public string voudat { get; set; }
            public string voudat1 { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string vounum2 { get; set; }
            public string cactcode { get; set; }
            public string cactdesc { get; set; }
            public string rescode { get; set; }
            public string resdesc { get; set; }
            public double trnam { get; set; }
            public double trnqty { get; set; }
            public double trnrate { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double balamt { get; set; }
            public string trnrmrk { get; set; }
            public string refnum { get; set; }
            public string srinfo { get; set; }
            public string venar1 { get; set; }
            public string venar2 { get; set; }
            public string username { get; set; }
            public double insamt { get; set; }
            public string insdate { get; set; }

            public BankInterest() { }

        }

        [Serializable]
        public class EClassRptIssueVsPayment  
        {

            public string comcod { get; set; }
            public string grp1 { get; set; }
            public string grp { get; set; }
            public string actcode { get; set; }
            public string rescode { get; set; }
            public string cactcode { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string voudat { get; set; }
            public string chequeno { get; set; }
            public string chequedat { get; set; }
            public double isuamt { get; set; }
            public double dramt { get; set; }
            public double reconamt { get; set; }
            public string trnrmrk { get; set; }
            public string actdesc { get; set; }
            public string resdesc { get; set; }
            public string isudat { get; set; }


            public EClassRptIssueVsPayment() { }



        }


        [Serializable]
        public class EClassRptIssueVsPaymentSummary
        {

            public string comcod { get; set; }
            public string rescode { get; set; }
            public double isuamt { get; set; }
            public double reconamt { get; set; }
            public double preconamt { get; set; }
            public double creconamt { get; set; }
            public string resdesc { get; set; }

            public EClassRptIssueVsPaymentSummary() { }

        }

        [Serializable]
        public class RptDailyPayment
        {
            public string comcod { get; set; }
            public string paycode { get; set; }
            public string paydesc { get; set; }
            public double payam { get; set; }


            public RptDailyPayment() { }


        }

        [Serializable]
        public class RptDailyPaymentSummaryCostWise
        {
            public string comcod { get; set; }
            public string grp { get; set; }
            public string actcode1 { get; set; }
             public string actdesc1 { get; set; }
            public string actcode2 { get; set; }
            public string actdesc2 { get; set; }
            public double dram { get; set; }


            public RptDailyPaymentSummaryCostWise() { }


        }
        [Serializable]
        public class RptBankReconc
        {

            public string grp1 { get; set; }
            public string voudat { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string note1 { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double balam { get; set; }
            public double trnam { get; set; }
            public double vousum { get; set; }
            public string actdesc { get; set; }
            public string subdesc { get; set; }
            public string refnum { get; set; }
            public string comcod { get; set; }
            public string comsnam { get; set; }
            public RptBankReconc() { }


        }

        [Serializable]
        public class RptTransactionLink
        {

            public string recndt1 { get; set; }
            public string voudat1 { get; set; }
            public string chequedat { get; set; }
            public string isunum { get; set; }
            public string vounum1 { get; set; }
            public string refnum { get; set; }
            public string rpdesc { get; set; }
            public string ctransdes { get; set; }
            public string actdesc { get; set; }
            public string resdesc1 { get; set; }
            public string payto { get; set; }
            public double payam { get; set; }
            public double depam { get; set; }            
            public string venar { get; set; }

            public RptTransactionLink() { }


        }


        [Serializable]
        public class RptTranLinkPost
        {
            public string voudat1 { get; set; }
            public string chequedat { get; set; }
            public string isunum { get; set; }
            public string vounum1 { get; set; }
            public string refnum { get; set; }
            public string rpdesc { get; set; }
            public string ctransdes { get; set; }
            public string actdesc { get; set; }
            public string resdesc1 { get; set; }
            public string payto { get; set; }
            public double payam { get; set; }
            public string venar { get; set; }
            public RptTranLinkPost() { }


        }

      


        [Serializable]
        public class rptAccBudVsExpen
        {

            public string comcod { get; set; }

            public string recndt1 { get; set; }
            public string voudat1 { get; set; }
            public string chequedat { get; set; }
            public string isunum { get; set; }
            public string vounum1 { get; set; }
            public string refnum { get; set; }
            public string rpdesc { get; set; }
            public string ctransdes { get; set; }
            public string actdesc { get; set; }
            public string resdesc1 { get; set; }
            public string payto { get; set; }
            public double payam { get; set; }
            public string venar { get; set; }

            public rptAccBudVsExpen() { }


        }




        [Serializable]
        public class RptDailyPaymentDetails
        {
            public string comcod { get; set; }
            public string grp { get; set; }
            public string paycode { get; set; }
            public string paydesc { get; set; }
            public string rescode { get; set; }
            public string resdesc { get; set; }
            public double payam { get; set; }   


            public RptDailyPaymentDetails() { }


        }



        [Serializable]
        public class EClassSupOrConPayment
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
            public double balamt { get; set; }
            public double clsbill { get; set; }
            public double clsadv { get; set; }
            public double isuamt { get; set; }
            public double billamt { get; set; }
            public double isubal { get; set; }
            public double isudr { get; set; }
            public double isucr { get; set; }
            public string resdesc { get; set; }
            public double mrramt { get; set; }
            public double netbill { get; set; }

            public EClassSupOrConPayment() { }



        }

        [Serializable]
        public class EClassPendingConBill
        {
            public string comcod { get; set; }
            public string billno { get; set; }
            public string textbillno { get; set; }
            public string billref { get; set; }
            public string tdate { get; set; }
            public string csircode { get; set; }
            public string ssirdesc { get; set; }
            public double amt { get; set; }

            public EClassPendingConBill() { }

        }

        [Serializable]
        public class EClassAitVatSdDeduction
        {

            public string comcod { get; set; }
            public string rescode { get; set; }
            public double opam { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double clsam { get; set; }
            public string resdesc { get; set; }

            public EClassAitVatSdDeduction() { }


        }

        [Serializable]
        public class RptMontlySupplierBill
        {
            public string comcod { get; set; }
            public string rescode { get; set; }
            public string sirdesc { get; set; }
            public double sbillamt { get; set; }
            public double ttaxamt { get; set; }
            public double pbillamt { get; set; }
            public double netbill { get; set; }

            public RptMontlySupplierBill() { }




        }

        [Serializable]
        public class RptMonthlySubConBill
        {

            public string comcod { get; set; }
            public string csircode { get; set; }
            public string sirdesc { get; set; }
            public double billamt { get; set; }
            public double sdamt { get; set; }
            public double dedamt { get; set; }
            public double penamt { get; set; }
            public double netamt { get; set; }
            public double payment { get; set; }
            public double advamt { get; set; }
            public double netpayable { get; set; }


            public RptMonthlySubConBill() { }


        }

        [Serializable]
        public class TdsVdsSdDeducProjWise
        {

            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string rescode { get; set; }
            public string sirdesc { get; set; }
            public double opam { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double netamt { get; set; }

            public TdsVdsSdDeducProjWise() { }


        }

        [Serializable]
        public class RptAitVatSdDeduction
        {

            public string comcod { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string head1 { get; set; }
            public string voudat { get; set; }
            public string voudat1 { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string rescode { get; set; }
            public string resdesc { get; set; }
            public double opam { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double trnam { get; set; }
            public double trnqty { get; set; }
            public double clsam { get; set; }
            public string trnrmrk { get; set; }
            public string refnum { get; set; }
            public string srinfo { get; set; }
            public string venar { get; set; }

            public RptAitVatSdDeduction() { }


        }


        public class RptAitTaxVatProjectWise
        {

            public string comcod { get; set; }
            public string resdesc { get; set; }
            public string rescode { get; set; }
            public double tamt { get; set; }
            public double p1 { get; set; }
            public double p2 { get; set; }
            public double p3 { get; set; }
            public double p4 { get; set; }
            public double p5 { get; set; }
            public double p6 { get; set; }
            public double p7 { get; set; }
            public double p8 { get; set; }
            public double p9 { get; set; }
            public double p10 { get; set; }
            public double p11 { get; set; }
            public double p12 { get; set; }
            public double p13 { get; set; }
            public double p14 { get; set; }

            public double p15 { get; set; }
            public double p16 { get; set; }
            public double p17 { get; set; }
            public double p18 { get; set; }
            public double p19 { get; set; }
            public double p20 { get; set; }
            public double p21 { get; set; }
            public double p22 { get; set; }


            public RptAitTaxVatProjectWise() { }

        }

        [Serializable]
        public class RptTransactionList
        {

            public string comcod { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string rescode { get; set; }
            public string voudat { get; set; }
            public string voudat1 { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public string refnum { get; set; }
            public string srinfo { get; set; }
            public string voutype { get; set; }
            public string payto { get; set; }
            public string usrname { get; set; }
            public string resdesc { get; set; }
                                
            public RptTransactionList() { }


        }


        [Serializable]
        public class RptProjectCostPersft
        {

            public string comcod { get; set; }
            public string actcode { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string rescode { get; set; }
          
            public double bgdam { get; set; }
            public double trnam { get; set; }
            public double bcncst { get; set; }
            public double bslcst { get; set; }
            public double cncst { get; set; }
            public double cnslcst { get; set; }
            public string resdesc { get; set; }
            public string resunit { get; set; }

            public RptProjectCostPersft() { }


        }

        [Serializable]
        public class RptProjectSpecification
        {

            public string comcod { get; set; }
            public string actcode { get; set; }                  
            public string rescode { get; set; }
            public string spclcode { get; set; }
            public double trnqty { get; set; }
            public double trnam { get; set; }
            public double trnrate { get; set; }         
            public string resdesc { get; set; }
            public string resunit { get; set; }
            public string spcldesc { get; set; }
            public RptProjectSpecification() { }


        }


        [Serializable]
        public class RptProjectPettyCash
        {

            public string comcod { get; set; }          
            public string rescode { get; set; }
            public double amount { get; set; }
            public string unit { get; set; }
            public string sirdesc { get; set; }
            public RptProjectPettyCash() { }


        }

        [Serializable]
        public class AccControlSchedule01
        {
            public string actcode4 { get; set; }
            public string actdesc4 { get; set; }
            public double opndram { get; set; }
            public double opncram { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double closdram { get; set; }
            public double closcram { get; set; }
            public double closam { get; set; }
            public string clremks { get; set; }

            public AccControlSchedule01(){}
        }


          [Serializable]
        public sealed class AccCashBankBook1
        {
            public string comcod { get; set; }
            public string grp1 { get; set; }
            public DateTime voudat { get; set; }
            public string voudat1 { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string cactcode { get; set; }
            public string cactdesc { get; set; }
            public string rescode { get; set; }
            public string resdesc { get; set; }
            public double opnam { get; set; }
            public double depam { get; set; }
            public double payam { get; set; }
            public double clsam { get; set; }
            public double casham { get; set; }
            public double bankam { get; set; }
            public string trnrmrk { get; set; }
            public string refnum { get; set; }
            public string vounar { get; set; }
            public string payto { get; set; }

        }

        [Serializable]
        public class PurchaseNotYetUpdated
        {
            public string ssircode { get; set; }
            public string pactcode { get; set; }
            public string ssirdesc { get; set; }
            public string pactdesc { get; set; }
            public string billno1 { get; set; }
            public string tdate { get; set; }
            public string billref { get; set; }
            public double amt { get; set; }

            public PurchaseNotYetUpdated() { }

        }

        [Serializable]
        public class ChequeDepositPrint
        {
            public string grp { get; set; }
            public string bankcode { get; set; }
            public string depositbank { get; set; }
            public string grpdesc { get; set; }
            public string mrno { get; set; }
            public string mrdate { get; set; }
            public string mrdate1 { get; set; }
            public string collfrm { get; set; }
            public string pactcode { get; set; }
            public string usircode { get; set; }
            public string pactdesc { get; set; }
            public string custname { get; set; }
            public string udesc { get; set; }
            public string refno { get; set; }
            public string chqno { get; set; }
            public string paydate { get; set; }
            public string chqdate { get; set; }
            public string bankname { get; set; }
            public string bbranch { get; set; }
            public string depositdat { get; set; }
            public string dhonrdate { get; set; }
            public string bcleardate { get; set; }
            public string recndt { get; set; }
            public string bank { get; set; }
            public double cashamt { get; set; }
            public double chqamt { get; set; }
            public double returnval { get; set; } 
            public double payamt { get; set; } 
            public double recvamt { get; set; } 

            public ChequeDepositPrint() { }

        }

        





        [Serializable]

        public class RptCashBank
        {
            public string grp1 { get; set; }
            public string grpsum { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string voudat1 { get; set; }
            public string recndt1 { get; set; }
            public string isunum { get; set; }
            public string chequedat { get; set; }
            public string actcode { get; set; }
            public string cactcode { get; set; }
            public string rescode { get; set; }
            public double depam { get; set; }
            public string actdesc { get; set; }
            public string cactdesc { get; set; }
            public string resdesc { get; set; }
            public double payam { get; set; }
            public double casham { get; set; }
            public double bankam { get; set; }
            public double srcham { get; set; }
            public string tmmrk { get; set; }
            public string refnum { get; set; }

            public string vounar { get; set; }
            public string payto { get; set; }
 
            public RptCashBank() { }
        }


        [Serializable]

        public class AccOpening
        {
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string actelev { get; set; }
            public string acttype { get; set; }
            public double Dr { get; set; }
            public double Cr { get; set; }
            public string spcfdesc { get; set; }
            public double qty { get; set; }
            public double rate { get; set; }
            public string rescode { get; set; }
            public string resdesc { get; set; }
            public string resunit { get; set; }
            public AccOpening() { }
        }


        [Serializable]


        public class RptMonthlyProbCollection
        {
            public string pactcode { get; set; }

            public string pactdesc { get; set; }

            public string custname { get; set; }
            public string udesc { get; set; }
            public string mobileno { get; set; }
            public DateTime schdate { get; set; }

            public double cdueamt { get; set; }

            public string steam { get; set; }

             public RptMonthlyProbCollection()
            {

            }
        }

        [Serializable]
        public class RptDuesReportAll
        {
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string custname { get; set; }
            public string Unitname { get; set; }
            public string gdesc { get; set; }
            public string schdat { get; set; }

            public double pbookam { get; set; }
            public double pinsam { get; set; }
            public double cbookam { get; set; }
            public double cinsam { get; set; }
            public double todues { get; set; }

           
            public RptDuesReportAll()
            {

            }
        }
        [Serializable]
        public class RptSupplierOverAllPSummaryDetails
        {
            public string rescode { get; set; }
            public string resdesc { get; set; }
            public string actdesc { get; set; }
            public string grp { get; set; }
            public double opnam { get; set; }
            public double sdamt { get; set; }
            public double taxamt { get; set; }
            public string vounum1 { get; set; }
            public string voudat { get; set; }
            public double vatamt { get; set; }
            public double netbillamt { get; set; }
            public double payamt { get; set; }
            public double discountamt { get; set; }

            public double dram { get; set; }
            public double cram { get; set; }

            public double netpayble { get; set; }



            public RptSupplierOverAllPSummaryDetails()
            {

            }
        }
        [Serializable]
        public class RptSupplierOverAllPSummary
        {
            public string rescode { get; set; }
            public string resdesc { get; set; }
            public string actdesc { get; set; }
            public string grp { get; set; }
            public double opnam { get; set; }
            public double sdamt { get; set; }
            public double payamt { get; set; }
            public double taxamt { get; set; }
            public double vatamt { get; set; }
            public double netbillamt { get; set; }
            
            public double dram { get; set; }
            public double cram { get; set; }
     
            public double netpayable { get; set; }

            public double discountamt { get; set; }

            public RptSupplierOverAllPSummary()
            {

            }
        }




    }
}
