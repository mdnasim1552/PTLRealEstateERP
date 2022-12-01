using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm.C_89_Pay
{
    public class SalarySheet2
    {
        [Serializable]
        public class SalarySumm
        {
            public string  section { get; set; }
            public string sirdesc { get; set; }
            public double cursal { get; set; }
            public double curpay { get; set; }
            public double curempno { get; set; }
            public double presal { get; set; }
             public double prepay { get; set; }
            public double preempno { get; set; }
        }

       
        [Serializable]
        public class bnkStatement
        {
            public string idcard { get; set; }
            public string empname { get; set; }
            public string banksl { get; set; }
            public string bankaddr { get; set; }
            public string bankname { get; set; }
            public string acno { get; set; }
            public double amt { get; set; }            
            public string desig { get; set; }
            public string routing { get; set; }
            public string dept { get; set; }


        }



        [Serializable]
        public class SalSummary2
        {
            public string refno { get; set; }
            public string section { get; set; }
            public string refdesc { get; set; }
            public string sectionname { get; set; }
            public double nofemployee { get; set; }
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
            public double othded { get; set; }
            public double arded { get; set; }
            public double dallow { get; set; }            
            public double oallow { get; set; }	
            public double hallow { get; set; }
            public double elallow { get; set; }
            public double mbill { get; set; }
            public double mbillded { get; set; }
            public double lwpday { get; set; }            
            public double lwided { get; set; }
            public double loanins { get; set; }
            public double gssal { get; set; }
            public double gspay { get; set; }
            public double absded { get; set; }
            public double tallow { get; set; }            
            public double tdeduc { get; set; } 
            public double tothdeduc { get; set; }
            public double sdedamt { get; set; }
            public double elftam { get; set; }
            public double gspay1 { get; set; }
            public double netpay { get; set; }            
            public double nagpercommi { get; set; }
            public double netpayatax { get; set; }
            public double mcadj { get; set; }
            public double othallow { get; set; }
            public double othearn { get; set; }
            public double mcallow { get; set; }            
            public double teallow { get; set; }
            public double foodal { get; set; }
            public double cashamt { get; set; }
            public double bankamt { get; set; }
            public double bankamt2 { get; set; }
            public double presal { get; set; }            
            public double prepay { get; set; }
            public double preempno { get; set; }
            public double toother { get; set; }
            public double caothallow { get; set; }
            public double advaloan { get; set; }
            public double mbilaothded { get; set; }
            public double dedday { get; set; }
            public double otall { get; set; }
            public double ohour { get; set; }
        }

         [Serializable]
         public class ForLetterbr
         {
             public string refno { get; set; }
             public string section { get; set; }

             public ForLetterbr() {}
         }


         [Serializable]
         public class RptCashPay02
         {
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
             public double wd { get; set; }
             public double netpay { get; set; }

             public RptCashPay02() { }
         }

        [Serializable]

        public class RptGrpSalSummary
        {
            public string comcod { get; set; }
            public string company { get; set; }
            public string companyname { get; set; }
            public double curempno { get; set; }
            public double loanbal { get; set; }
            public double loanbal2 { get; set; }
            public double loanins { get; set; }
            public double gssal { get; set; }
            public double pfund { get; set; }
            public double tothdeduc { get; set; }
            public double gspay { get; set; }
            public double itax { get; set; }
            public double netpay { get; set; }
            public double netpaybftax { get; set; }
            public double preempno { get; set; }
            public double pregssal { get; set; }
            public double prenetpay { get; set; }

            public RptGrpSalSummary() { }
        }

        [Serializable]
        public class RptMonthlySalaryTax
        {
            public string section { get; set; }
            public string sectionname { get; set; }
            public string idcard { get; set; }
            public string empname { get; set; }
            public string desig { get; set; }
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
            public double toam { get; set; }

            public RptMonthlySalaryTax() { }
        }


        [Serializable]
        public class RptMonthlySalaryTaxFinlay
        {
            public string empname { get; set; }
            public string empid { get; set; }
            public string grp { get; set; }
            public string idcardno { get; set; }
           
            public string tin { get; set; }
            public string monthid1 { get; set; }
            public double itax { get; set; }
         

            public RptMonthlySalaryTaxFinlay() { }
        }





    }
}
