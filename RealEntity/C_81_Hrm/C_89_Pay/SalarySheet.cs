using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm.C_89_Pay
{
    public class SalarySheet
    {
        [Serializable]
        public class SalaryGrandT
        {
            public string  comcod { get; set; }
            public string refno { get; set; } 
            public string refdesc { get; set; }
            public double netpay { get; set; }
            public double gssal { get; set; }
            public double toadd { get; set; }
            public double tdeduc { get; set; } 
        }

        [Serializable]
        public class SecuritySalarySuvstu
        {
            public string comcod { get; set; }
            public string section { get; set; }
            public string sectionname { get; set; }
            public string empname { get; set; }
            public string desig { get; set; }
            public double gssal { get; set; }
            public double wd { get; set; }
            public double absday { get; set; }
            public double acat { get; set; }
            public double gspayaabsd { get; set; }
            public double oallow { get; set; }
            public double haircutal { get; set; }
            public double foodal { get; set; }
            public double nfoodal { get; set; }
            public double netpay { get; set; }
            public string rmrks { get; set; }

        }

        [Serializable]
        public class EmpMonthSummary
        {
            public string comcod { get; set; }
            public int rowid { get; set; }
            public string empid { get; set; }
            public string grade { get; set; }
            public DateTime joindate { get; set; }
            public decimal wd { get; set; }
            public decimal wjd { get; set; }
            public decimal absday { get; set; }
            public decimal wld { get; set; }
            public decimal acat { get; set; }
            public decimal tabday { get; set; }
            public decimal netwday { get; set; }
            public decimal loanbal { get; set; }
            public decimal loanbal2 { get; set; }
            public decimal bsal { get; set; }
            public decimal salary { get; set; }
            public decimal hrent { get; set; }
            public decimal cven { get; set; }
            public decimal mallow { get; set; }

            public decimal otall { get; set; }
            public decimal arsal { get; set; }
            public decimal pickup { get; set; }
            public decimal fuel { get; set; }
            public decimal entaint { get; set; }
            public decimal mcell { get; set; }
            public decimal incent { get; set; }
            public decimal empcont { get; set; }
            public decimal oth { get; set; }
            public decimal pfund { get; set; }
            public decimal itax { get; set; }
            public decimal adv { get; set; }
            public decimal fallded { get; set; }
            public decimal mbillded { get; set; }
            public decimal othded { get; set; }

            public decimal dallow { get; set; }
            public decimal teallow { get; set; }
            public decimal oallow { get; set; }
            public decimal ohour { get; set; }
            public decimal hallow { get; set; }
            public decimal elallow { get; set; }
            public decimal mbill { get; set; }
            public decimal lwided { get; set; }
            public decimal tothdeduc { get; set; }
            public decimal arded { get; set; }
            public decimal loanins { get; set; }
            public decimal gssal { get; set; }
            public decimal salpday { get; set; }
            public decimal gspay { get; set; }
            public decimal gspay1 { get; set; }
            public decimal absded { get; set; }

            public decimal tallow { get; set; }
            public decimal tdeduc { get; set; }
            public decimal dedday { get; set; }
            public decimal ddaya10 { get; set; }
            public decimal dday10amt { get; set; }
            public decimal mcallow { get; set; }
            public decimal mcadj { get; set; }
            public double othallow { get; set; }
            public decimal othearn { get; set; }
            public decimal elftam { get; set; }
            public string ellfthour { get; set; }
            public decimal netpayatax { get; set; }
            public decimal netpay { get; set; }
            public string bankacno { get; set; }
            public decimal bankamt { get; set; }
            public decimal cashamt { get; set; }
            public decimal thday { get; set; }
            public decimal lwpday { get; set; }
            public string idcard { get; set; }
            public string section { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string refno { get; set; }
            public string refdesc { get; set; }
            public string sirdesc { get; set; }
            public string sectionname { get; set; }
            public decimal asloanins { get; set; }
            public decimal asloanbal { get; set; }
            public decimal asloanbal2 { get; set; }
            public decimal gssal1 { get; set; }
            public decimal dalday { get; set; }
            public decimal sdedamt { get; set; }
            public decimal cl { get; set; }
            public decimal el { get; set; }
            public decimal sl { get; set; }
            public decimal ml { get; set; }
            public decimal wpl { get; set; }
            public decimal lft { get; set; }
            public decimal pl { get; set; }
            public decimal hl { get; set; }
            public decimal pmd { get; set; }
            public decimal drate { get; set; }
            public decimal hrate { get; set; }
            public decimal elrate { get; set; }
            public decimal ovthour { get; set; }
            public decimal ovtrate { get; set; }
            public decimal ovtamt { get; set; }
        }

        // a.comcod, a.empid,empname,idcard,section, desigid, desig, refno ,a.bsal ,a.hrent ,a.cven ,a.mallow ,a.gsal  ,a.gsal1 ,a.itax ,a.cashamt ,a.bankamt
        [Serializable]
        public class aitpurpose
        {
            public string comcod { get; set; }
            public string monthid { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string idcard { get; set; }
            public string section { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public string refno { get; set; }
            public decimal bsal { get; set; }
            public decimal hrent { get; set; }
            public decimal cven { get; set; }
            public decimal mallow { get; set; }
            public decimal gsal { get; set; }
            public decimal gsal1 { get; set; }
            public decimal itax { get; set; }
            public decimal cashamt { get; set; }
            public decimal bankamt { get; set; }
            public decimal incent { get; set; }
            public decimal adv { get; set; }
            public DateTime posteddat { get; set; }
            public string challanno { get; set; }
            public decimal bonamt { get; set; }
            public decimal otallow { get; set; }
            public decimal pfund { get; set; }





        }

        [Serializable]
        public class BankFord
        {
            public string idcard { get; set; }
            public string empid { get; set; }
            public string monthid { get; set; }
            public string empname { get; set; }
            public string bankcode { get; set; }
            public string banksl { get; set; }
            public string bankaddr { get; set; }
            public string section { get; set; }
            public string detname { get; set; }
            public string acno { get; set; }
            public double amt { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }

        }

        [Serializable]
        public class LeaveApp
        {


            public string gcod { get; set; }
            public string gdesc { get; set; }
            public double entitle { get; set; }
            public double permonth { get; set; }
            public double pbal { get; set; }
            public double ltaken { get; set; }
            public double balleave { get; set; }
            public double tltakreq { get; set; }
            public DateTime lenjoydt1 { get; set; }
            public DateTime lenjoydt2 { get; set; }
            public double lenjoyday { get; set; }
            public DateTime appdate { get; set; }
            public double applyday { get; set; }
            public double appday { get; set; }


        }
        [Serializable]
        public class Incomesalary
        {


            public string paydate { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public double cashamt { get; set; }
            public double bankamt { get; set; }
            public double bankamt2 { get; set; }
            public double totalsal { get; set; }
          

        }

        [Serializable]
        public class Disbursement
        {
           public string comcod { get; set; }
           public string monthid { get; set; }
           public string companycode { get; set; }
           public string refno { get; set; }
           public string section { get; set; }
           public string empid { get; set; }
           public string idcardno { get; set; }
           public string sectionname { get; set; }
           public string empdname { get; set; }
           public string nagadno { get; set; }
           public double netpay { get; set; }
           public double cashoutfee { get; set; }
           public double cashoutfeenag { get; set; }
           public double disbuamt { get; set; }
           public double servicefee { get; set; }
           public string wd { get; set; }
           public string wjd { get; set; }
           public string absday { get; set; }
           public string wld { get; set; }
           public string acat { get; set; }
           public string tabday { get; set; }
           public string netwday { get; set; }
           public string bsal { get; set; }
           public string salary { get; set; }
           public string hrent { get; set; }
           public string cven { get; set; }
           public string mallow { get; set; }
           public string arsal { get; set; }
           public string pickup { get; set; }
           public string fuel { get; set; }
           public string entaint { get; set; }
           public string mcell { get; set; }
           public string incent { get; set; }
           public string empcont { get; set; }
           public string oth { get; set; }
           public string pfund { get; set; }
           public string adv { get; set; }
           public string othded { get; set; }
           public string arded { get; set; }
           public string dallow { get; set; }
           public string oallow { get; set; }
           public string ohour { get; set; }
           public string thday { get; set; }
           public string hallow { get; set; }
           public string elallow { get; set; }
           public string mbill { get; set; }
           public string lwpday { get; set; }
           public string lwided { get; set; }
           public string loanins { get; set; }
           public string gssal { get; set; }
           public string salpday { get; set; }
           public string gspay { get; set; }
           public string absded { get; set; }
           public string tallow { get; set; }
           public string tdeduc { get; set; }
           public string tothdeduc { get; set; }
           public string dedday { get; set; }
           public string dalday { get; set; }
           public string sdedamt { get; set; }
           public string gspay1 { get; set; }
           public string netpayatax { get; set; }
           public string desigid { get; set; }
           public string mcadj { get; set; }
           public string othallow { get; set; }
           public string othearn { get; set; }
           public string mcallow { get; set; }
           public string teallow { get; set; }
           public string foodal { get; set; }
           public string cashamt { get; set; }
           public string bankamt { get; set; }
           public string bankamt2 { get; set; }
           public string mbillded { get; set; }

        }
        [Serializable]
        public class DisbursementSummary
        {
            public string grp { get; set; }
            public string descrip { get; set; }
            public double amount { get; set; }
        }

        [Serializable]
        public class RptSalaryStatementNagad   
        {
            public string comcod { get; set; }
            public Byte[] comlogo { get; set; }
            public string monthid { get; set; }
            public string refno { get; set; }
            public string section { get; set; }
            public string desigid { get; set; }
            public string empid { get; set; }
            public string idcard { get; set; }
            public string refdesc { get; set; }
            public string sectionname { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string empname1 { get; set; }
            public string nmobileno { get; set; }
            public double wd { get; set; }
            public double netpay { get; set; }
            public string rmrks { get; set; }


            public RptSalaryStatementNagad()
            {

            }  

        }

   
        [Serializable]
        public class RptSalarySheetRupayan
           {
               public string rowid { get; set; }
               public string empid { get; set; }
               public DateTime joindate { get; set; }
               public double wd { get; set; }
               public double wjd { get; set; }
               public double absday { get; set; }
               public double wld { get; set; }
               public double acat { get; set; }
               public double tabday { get; set; }
               public double netwday { get; set; }
               public double loanbal { get; set; }
               public double loanbal2 { get; set; }
               public double bsal { get; set; }
               public double salary { get; set; }  
               public double hrent { get; set; }
               public double cven { get; set; }
               public double mallow { get; set; }
               public double arsal { get; set; }
               public double pickup { get; set; }
               public double fuel { get; set; }
               public double entaint { get; set; } 
               public double mcell { get; set; }
               public double incent { get; set; }
               public double empcont { get; set; }
               public double oth { get; set; }
               public double pfund { get; set; }
               public double itax { get; set; }
               public double adv { get; set; }
               public double fallded { get; set; }
               public double mbillded { get; set; }
               public double othded { get; set; }
               public double dallow { get; set; }
               public double teallow { get; set; }
               public double oallow { get; set; }
               public double ohour { get; set; }
               public double hallow { get; set; }
               public double elallow { get; set; }
               public double mbill { get; set; }
               public double lwided { get; set; }
               public double tothdeduc { get; set; }
               public double arded { get; set; }
               public double loanins { get; set; }
               public double gssal { get; set; } 
               public double salpday { get; set; } 
               public double gspay { get; set; } 
               public double gspay1 { get; set; } 
               public double absded { get; set; } 
               public double tallow { get; set; } 
               public double tdeduc { get; set; } 
               public double dedday { get; set; } 
               public double dalday { get; set; } 
               public double mcallow { get; set; } 
               public double mcadj { get; set; } 
               public double othallow { get; set; } 
               public double othearn { get; set; } 
               public double sdedamt { get; set; } 
               public double elftam { get; set; } 
               public string ellfthour { get; set; } 
               public double netpay { get; set; } 
               public double netpayatax { get; set; } 
               public string bankacno { get; set; } 
               public double bankamt { get; set; } 
               public double cashamt { get; set; } 
               public double thday { get; set; } 
               public double lwpday { get; set; } 
               public string idcard { get; set; } 
               public string section { get; set; } 
               public string desigid { get; set; } 
               public string desig { get; set; } 
               public string empname { get; set; } 
               public string refno { get; set; } 
               public string refdesc { get; set; } 
               public double ddaya10 { get; set; } 
               public double dday10amt { get; set; } 
               public string permobile { get; set; } 
               public string cormobile { get; set; } 
               public double bankamt2 { get; set; } 
               public double wkday { get; set; } 
               public double govday { get; set; } 
               public string sectionname { get; set; } 
               public RptSalarySheetRupayan()
               {
               }
           }

        [Serializable]

        public class RptSalarySheet
        {
            public string rowid { get; set; }
            public string empid { get; set; }
            public string grade { get; set; }
            public DateTime joindate { get; set; }
            public double wd { get; set; }
            public double wjd { get; set; }
            public double absday { get; set; }
            public double wld { get; set; }
            public double acat { get; set; }
            public double tabday { get; set; }
            public double netwday { get; set; }
            public double loanbal { get; set; }
            public double loanbal2 { get; set; }
            public double bsal { get; set; }
            public double salary { get; set; }
            public double hrent { get; set; }
            public double cven { get; set; }
            public double mallow { get; set; }
            public double otall { get; set; }
            public double arsal { get; set; }
            public double pickup { get; set; }
            public double fuel { get; set; }
            public double entaint { get; set; } 
            public double mcell { get; set; } 
            public double incent { get; set; } 
            public double empcont { get; set; } 
            public double oth { get; set; } 
            public double pfund { get; set; } 
            public double itax { get; set; } 
            public double adv { get; set; } 
            public double fallded { get; set; } 
            public double mbillded { get; set; } 
            public double othded { get; set; } 
            public double dallow { get; set; } 
            public double teallow { get; set; } 
            public double oallow { get; set; } 
            public double ohour { get; set; } 
            public double hallow { get; set; } 
            public double elallow { get; set; } 
            public double mbill { get; set; } 
            public double lwided { get; set; } 
            public double tothdeduc { get; set; } 
            public double arded { get; set; } 
            public double loanins { get; set; }
            public double gssal { get; set; }
            public double gssal1 { get; set; } 
            public double salpday { get; set; } 
            public double gspay { get; set; } 
            public double gspay1 { get; set; } 
            public double absded { get; set; } 
            public double tallow { get; set; } 
            public double tdeduc { get; set; } 
            public double dedday { get; set; } 
            public double dalday { get; set; } 
            public double ddaya10 { get; set; } 
            public double dday10amt { get; set; } 
            public double mcallow { get; set; } 
            public double mcadj { get; set; } 
            public double othallow { get; set; } 
            public double tptallow { get; set; } 
            public double kpi { get; set; } 
            public double perbon { get; set; } 
            public double othearn { get; set; } 
            public double haircutal { get; set; } 
            public double foodal { get; set; } 
            public double nfoodal { get; set; } 
            public double sdedamt { get; set; } 
            public double elftam { get; set; } 
            public string ellfthour{get; set;}
            public double netpay { get; set; } 
            public double netpayatax { get; set; } 
            public string bankacno {get; set;}
            public double bankamt { get; set; } 
            public double bankamt2 { get; set; } 
            public double cashamt { get; set; } 
            public double thday { get; set; } 
            public double lwpday { get; set; } 
            public string idcard {get; set;}
            public string section {get; set;}
            public string desigid {get; set;}
            public string desig {get; set;}
            public string empname {get; set;}
            public string refno {get; set;}
            public double wkday { get; set; } 
            public double govday { get; set; } 
            public string refdesc {get; set;}
            public string sectionname {get; set;}
            public string rmrks {get; set;}
            public double redamt { get; set; } 
            public double todecashsal { get; set; } 
            public double grosscash { get; set; } 
            public double ChequePay { get; set; } 
            public double hardship { get; set; } 
            public double fine { get; set; }
            public double finedays { get; set; } 
            public double cashded { get; set; } 
            public double tripal { get; set; } 
            public double absded2 { get; set; } 
            public double absded3 { get; set; } 
            public string rmrks2 { get; set; } 
            public double ottotal { get; set; } 
            public double toadd { get; set; }
            public double gloan { get; set; }
            public double carloan { get; set; }
            public double carallow { get; set; }
            public double refund { get; set; }
            public double elencash { get; set; }  
            public string grpcode { get; set; }
            public string grpdesc { get; set; }
            public string sectionname1 { get; set; }
            public double payodramt { get; set; }
            public double stamp { get; set; }
            public double nagpercommi { get; set; }
            public double netpaywnag { get; set; }
            public string nagadno { get; set; }
            public double tripday { get; set; }
            public double trprate { get; set; }
            public double factualday { get; set; }
            public double fpayamt { get; set; }
            public double payamta { get; set; }
            public double otrate { get; set; } 
            public string slno { get; set; }
            public string joindatebn { get; set; }
            public double lateday { get; set; }
            public double latededuc { get; set; }
            public double adjustamt { get; set; } 
            public double joinsal { get; set; }
            public double years { get; set; }
            public double months { get; set; }
            public double casuleave { get; set; }
            public double sicklv { get; set; }
            public double balleave { get; set; }
            public DateTime prodat { get; set; }
            public DateTime incrdate { get; set; }
            public string predeg { get; set; }
            public string aknment { get; set; }
            public double perloan { get; set; }
            public double genloan { get; set; }
            public double transded { get; set; }
            public double payables { get; set; }
            public int secsl { get; set; }
            public int deptsl { get; set; }
            public double bank1 { get; set; }
            public double bank2 { get; set; }
            public double bank3 { get; set; }
            public double bank4 { get; set; }
            public double bank5 { get; set; }
            public double bank6 { get; set; }
            public string maingrpdesc { get; set; }
            public string maingrp { get; set; }
            public string grpcod { get; set; }
            public string fgrp { get; set; }
            public string ftdesc { get; set; }
            public string desig2 { get; set; }
            public double swf { get; set; }
            public double subfee { get; set; }
            public int seq { get; set; }
            public double extday { get; set; }
            public double extdayamt { get; set; }

            public RptSalarySheet ()
            {

            }
        }


        [Serializable]
        public class SalaryPaySlip
        {

            public string comcod { get; set; }
            public string monthid { get; set; }
            public DateTime date { get; set; }
            public string refno { get; set; }
            public string empid { get; set; }
            public string section { get; set; }
            public string desigid { get; set; }
            public double wd { get; set; }
            public double absday { get; set; }
            public double absded2 { get; set; }
            public double wld { get; set; }
            public double acat { get; set; }
            public double bsal { get; set; }
            public double hrent { get; set; }
            public double cven { get; set; }
            public double mallow { get; set; }
            public double arsal { get; set; }
            public double pickup { get; set; }

            public double fuel { get; set; }
            public double entaint { get; set; }
            public double mcell { get; set; }
            public double incent { get; set; }
            public double oth { get; set; }
            public double ohour { get; set; }
            public double pfund { get; set; }
            public double itax { get; set; }
            public double adv { get; set; }
            public double mbill { get; set; }
            public double empcont { get; set; }
            public double fallded { get; set; }
            public double loanins { get; set; }
            public double othded { get; set; }
            public double dallow { get; set; }
            public double teallow { get; set; }
            public double oallow { get; set; }
            public double thday { get; set; }
            public double hallow { get; set; }
            public double elallow { get; set; }
            public double lwpday { get; set; }
            public double lwided { get; set; }
            public double gssal { get; set; }
            public double salpday { get; set; }
            public double gspay { get; set; }
            public double absded { get; set; }
            public double tdeduc { get; set; }
            public double mcallow { get; set; }
            public double mcadj { get; set; }
            public double othallow { get; set; }
            public double othearn { get; set; }
            public double sdedamt { get; set; }
            public double dedday { get; set; }
            public double netpay { get; set; }
            public double bankamt { get; set; }
            public double cashamt { get; set; }
            public double chequepay { get; set; }
            public double todecashsal { get; set; }
            public double totadd { get; set; }
            public double tgrsmon { get; set; }
            public double todecmon { get; set; }
            public double netpayable { get; set; }
            public double redamt { get; set; }
            public double mbillded { get; set; }
            public double stamp { get; set; }
            public double netsalarypay { get; set; }
            public double grossal1 { get; set; }
            public double grossalsub { get; set; }
            public double totaldeduction { get; set; }
            public string idcard { get; set; }
            public string empname { get; set; }
            public string desig { get; set; }
            public string comnam { get; set; }
            public string comadd { get; set; }
            public string refdesc { get; set; }
            public string aminword { get; set; }
            public string bankacno { get; set; }
            public double haircutal { get; set; }
            public double genloan { get; set; }
            public double carloan { get; set; }
            public double perloan { get; set; }
            public double motolon { get; set; }
            public double dresslon { get; set; }
            public double msetloan { get; set; }
            public double mscloan { get; set; }
            public double tptallow { get; set; }
            public double kpi { get; set; }
            public double perbon { get; set; }
            public double foodal { get; set; }
            public double transded { get; set; }
            public DateTime joindate { get; set; }
            public string deptname { get; set; }
            public double swf { get; set; }
            public double subfee { get; set; }
            public double  otallow { get; set; }
            public double tallow { get; set; }

            public double extday { get; set; }
            public double extdayamt { get; set; }


            public SalaryPaySlip()
            {

            }
            

        }


        [Serializable]
        public class BonusSummary
        {
            
            public string comcod { get; set; }
            public string monthid { get; set; }
            public string refno { get; set; }
            public string section { get; set; }
            public string empname { get; set; }
            public string desig { get; set; }
            public DateTime joindate { get; set; }
            public string refdesc { get; set; }
            public string sectionname { get; set; }
            public double nofemployee { get; set; }
            public double arsal { get; set; }
            public double foodal { get; set; }
            public double ottotal { get; set; }
            public double toother { get; set; }
            public double nagpercommi { get; set; }
            public double tdeduc { get; set; }
            public double netpayable { get; set; }
            public double bsal { get; set; }
            public double gssal { get; set; }
            public double bonamt { get; set; }
            public double bankamt { get; set; }
            public double bankamt2 { get; set; }
            public double cashamt { get; set; }
            public double duration { get; set; }
            public double perbon { get; set; }

            public BonusSummary()
            {

            }

        }

        [Serializable]
        public class EClassBankStatment
        {
            public string comcod { get; set; }
            public string company { get; set; }
            public string bnkcode { get; set; }
            public string section { get; set; }
            public string companyname { get; set; }
            public string bnkname { get; set; }
            public string sectionname { get; set; }
            public double salam { get; set; }
            public EClassBankStatment() { }
        }
        [Serializable]
        public class RptSalaryReconciliation
        {
            public string empid { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string empname { get; set; }
            public string desig { get; set; }
            public string replacement { get; set; }
            public string joresigndate { get; set; }
            public double curamt { get; set; }
            public double preamt { get; set; }
            public RptSalaryReconciliation() { }
        }

        [Serializable]
        public class AllBankSummary
        {
            public string comcod { get; set; }
            public string seq { get; set; }
            public string bankcode { get; set; }
            public string bankname { get; set; }
            public double bankamt { get; set; }
            public double cashamt { get; set; }
            public string remarks { get; set; }

            public string refno { get; set; }
            public string refdesc { get; set; }
            public double netpay { get; set; }
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
            public double netpayprev { get; set; }
            public double diffrent { get; set; }
            public double per { get; set; }
            public string grp { get; set; }
            public string empname { get; set; }
            public string replacement { get; set; }
            public string joresigndate { get; set; }
            public string desig { get; set; }
            public double preamt { get; set; }
            public double curamt { get; set; }
            public string empid { get; set; }
            public double gssal { get; set; }
            public double incamt { get; set; }
            public double inper { get; set; }
            public double gssalfinal { get; set; }
            public double gssalprev { get; set; }
            public string idcardno { get; set; }

            


        }
        [Serializable]
        public class MonthDesc
        {
            public string mon1 { get; set; }
            public DateTime mondat { get; set; }
            public string monname { get; set; }
        }

        public class BankDesc
        {
            public string comcod { get; set; }
            public string seq { get; set; }
            public string bankcode { get; set; }
            public string bankname { get; set; }

        }

        [Serializable]
        public class DeptWiseSal
        {
            public string comcod { get; set; }
            public string acgcode { get; set; }
            public string acgcodedesc { get; set; }
            public string grp { get; set; }
            public double netpayprev { get; set; }
            public double pernprev { get; set; }
            public double netpay { get; set; }
            public double perncur { get; set; }
            public double diffrent { get; set; }
            public double per { get; set; }
            public double netpayprev2 { get; set; }
            public double pern2prev { get; set; }
            public double net2pay { get; set; }
            public double per2ncur { get; set; }
            public double diffrent2 { get; set; }
            public double per2 { get; set; }
            public double net2payprev { get; set; }
            public double per2monprev { get; set; }
            public double net2paycurr { get; set; }
            public double per2cumon { get; set; }
            public double diff2mon { get; set; }
            public double per2mon { get; set; }

            public DeptWiseSal() { }
        }
        public class SalSummaryInfo
        {
            public string comcod { get; set; }
            public string grp { get; set; }
            public string descrip { get; set; }
            public double cashamt { get; set; }
            public double bankamt { get; set; }
      
            public SalSummaryInfo() { 
            }
        }
    }
}
