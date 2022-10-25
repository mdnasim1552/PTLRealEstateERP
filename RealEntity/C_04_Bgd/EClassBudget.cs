using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace RealEntity.C_04_Bgd
{
   public class EClassBudget
   {

       [Serializable]
       public class EClassMatStock 
       {
       public DateTime vdate{get;set;}
       public string vnumber{get;set;}
       public string refno{get;set;}
       public double  inqty{get;set;}
       public double  outqty{get;set;}
       public double  clqty{get;set;}

       public EClassMatStock() 
       {
       
       }

       public EClassMatStock(DateTime vdate, string vnumber, string refno, double inqty, double outqty, double clqty) 
       {
           this.vdate = vdate;
           this.vnumber = vnumber;
           this.refno = refno;
           this.inqty = inqty;
           this.outqty = outqty;
           this.clqty = clqty;
       
       
       }
       

          
       
       }
       //Nayan
       [Serializable]
       public class EClassMatRequired
       {

           
           public string rptcod { get; set; }
           public string flrdes { get; set; }
           public string rptdesc { get; set; }
           public string rptunit { get; set; }

           public double rptbgdqty { get; set; }
           public double rptqty { get; set; }
           public double rptrat { get; set; }
           public double rptamt { get; set; }

           public EClassMatRequired()
           {

           }

           public EClassMatRequired(string rptcod, string flrdes, string rptdesc, string rptunit, double rptbgdqty, double rptqty, double rptrat, double rptamt)
           {
               this.rptcod = rptcod;
               this.rptcod = rptcod;
               this.rptdesc = rptdesc;
               this.rptunit = rptunit;
               this.rptbgdqty = rptbgdqty;
               this.rptqty = rptqty;
               this.rptrat = rptrat;
               this.rptamt = rptamt;


           }




       }
       [Serializable]
       public class MatReq
       {
           public string comcod { get; set; }
           public string rsircode { get; set; }
           public double rcvqty { get; set; }
           public double trninqty { get; set; }
           public double trnoutqty { get; set; }
           public double trcvqty { get; set; }
           public double inproqty { get; set; }
           public double bgdrqty { get; set; }
           public double requirqty { get; set; }
           public string rsirdesc { get; set; }
           public string rsirunit { get; set; }
           public MatReq() { }
       }
       //Nayan
       [Serializable]
       public class BudgetInmStaSum 
       {
           public string comcod { get; set; }
           public string grp { get; set; }
           public string actcode { get; set; }
           public string acgcode { get; set; }
           public string rescode { get; set; }
           public double bgdqty { get; set; }
           public double bgdrate { get; set; }
           public double bgdam { get; set; }
           public double devcost { get; set; }
           public double salcost { get; set; }
           public string actdesc { get; set; }
           public string resdesc { get; set; }
           public string resunit { get; set; }
            public double actam { get; set; }
            public double adevcost { get; set; }
            public double asalcost { get; set; }
            public BudgetInmStaSum() { }
       }
       //Nayan      
       [Serializable]
       public class ProjBgdCon
       {
           public string comcod { get; set; }
           public string grp { get; set; }
           public string actcode { get; set; }
           public string rescode { get; set; }
           public double bgdqty { get; set; }
           public double bgdrate { get; set; }
           public double bgdam { get; set; }
           public double devcost { get; set; }
           public double salcost { get; set; }
           public string actdesc { get; set; }
           public string resdesc { get; set; }
           public string resunit { get; set; }
           public double actam { get; set; }
           public double adevcost { get; set; }
           public double asalcost { get; set; }
           public ProjBgdCon() { }
       }
       [Serializable]
       public class WorkvsRes
       {
           public string comcod { get; set; }
           public string bldcod { get; set; }
           public string flrcod { get; set; }
           public string isircode { get; set; }
           public string rsircode { get; set; }
           public string flrdes { get; set; }
           public double itemqty { get; set; }
           public double bgdwqty { get; set; }
           public double resqty { get; set; }
           public double resrat { get; set; }
           public double resamt { get; set; }
           public double subconrat { get; set; }
           public string isirdesc { get; set; }
           public string isirunit { get; set; }
           public string rsirdesc { get; set; }
           public string rsirunit { get; set; }
           public string unit { get; set; }
           public WorkvsRes() { }
       }
       //Nayan
       [Serializable]
       public class BudgTotalCost
       {
           public string comcod { get; set; }
           public string rowid { get; set; }
           public string actcode { get; set; }
           public string rescode { get; set; }
           public double bgdqty { get; set; }
           public double bgdrate { get; set; }
           public double bgdam { get; set; }
           public double devcost { get; set; }
           public double salcost { get; set; }
           public string actdesc { get; set; }
           public string resdesc { get; set; }
           public string resunit { get; set; }
           public BudgTotalCost() { }
       }
       //Nayan
       [Serializable]
       public class IndiMateDetails
       {
           public string comcod { get; set; }
           public string bldcod { get; set; }
           public string rptcod { get; set; }
           public string flrcod { get; set; }
           public string flrdes { get; set; }
           public double rptqty { get; set; }
           public double rptbgdqty { get; set; }
           public double uresqty { get; set; }
           public double rptrat { get; set; }
           public double rptamt { get; set; }
           public double peramt { get; set; }
           public string rptdesc { get; set; }
           public string rptdesc1 { get; set; }
           public string rptunit { get; set; }
           public IndiMateDetails() { }
       }
     
       //Nayan
       [Serializable]  
       public class WorkList
       {
           public string comcod { get; set; }
           public string sircode { get; set; }
           public string sircode1 { get; set; }
           public string sirdesc { get; set; }
           public string sirunit { get; set; }
           public double sirval { get; set; }
           public WorkList() { }
       }
    
       [Serializable]
       public class OtherReqStatus
       {
           // IQBAL NAYAN
           public string comcod { get; set; }
           public string reqno { get; set; }
           public string reqno1 { get; set; }
           public string pactcode { get; set; }
           public string rsircode { get; set; }
           public string pactdesc { get; set; }
           public string resdesc { get; set; }
           public string mrfno { get; set; }
           public double proamt { get; set; }
           public double appamt { get; set; }
           public DateTime reqdat { get; set; }
           public string reqdat1 { get; set; }
           public OtherReqStatus() { }
       }

       [Serializable]
       public class BudBalAfterAppro
       {
           // IQBAL NAYAN
           public string comcod { get; set; }
           public string grp { get; set; }
           public string actcode { get; set; }
           public string rescode { get; set; }
           public double bgdqty { get; set; }
           public double bgdrate { get; set; }
           public double bgdam { get; set; }
           public double alcamt { get; set; }
           public double balamt { get; set; }
           public double balp { get; set; }
           public string actdesc { get; set; }
           public string resdesc { get; set; }
           public string resunit { get; set; }
           public BudBalAfterAppro() { }
       } 

       [Serializable]
       public class BugPlanInfo
       {
           //  IQBAL NAYAN
           public string comcod { get; set; }
           public string bldcod { get; set; }
           public string flrcod { get; set; }
           public string isircode { get; set; }
           public string rsircode { get; set; }
           public string flrdes { get; set; }
           public double itemqty { get; set; }
           public double resqty { get; set; }
           public double resrat { get; set; }
           public double resamt { get; set; }
           public double abgdqty { get; set; }
           public double matqty { get; set; }
           public double alrate { get; set; }
           public double bgdalamt { get; set; }
           public double difqty { get; set; }
           public double difamt { get; set; }
           public string isirdesc { get; set; }
           public string isirunit { get; set; }
           public string rsirdesc { get; set; }
           public string rsirunit { get; set; }
           public BugPlanInfo() { }
       }

       [Serializable]
       public class bugdBalance
       {
           // IQBAL NAYAN
           public string comcod { get; set; }
           public string pactcode { get; set; }
           public string rsircode { get; set; }
           public double bgdqty { get; set; }
           public double rcvqty { get; set; }
           public double balqty { get; set; }
           public double bgdamt { get; set; }
           public double rcvamt { get; set; }
           public double balamt { get; set; }
           public double bgdrate { get; set; }
           public double rcvrate { get; set; }
           public double balrate { get; set; }
           public string rsirdesc { get; set; }
           public string rsirunit { get; set; }
           public double stdrat { get; set; }
           public bugdBalance() { }
       }
      
       [Serializable]
       public class BugdAna
       {
           // IQBAL NAYAN
           public string grpcod { get; set; }
           public string rescod { get; set; }
           public string resdesc { get; set; }
           public string resunit { get; set; }
           public double resqtyf { get; set; }
           public double resrat { get; set; }
           public double resamt { get; set; }
           public BugdAna() { }
       }

       [Serializable]
       public class YearlyPlan
       {
           // Iqbal Nayan
           public string comcod { get; set; }
           public string yearid { get; set; }
           public string rescode { get; set; }
           public string resdesc { get; set; }
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
           public double tamt { get; set; }
           public double perc { get; set; }
           public YearlyPlan() { }
       }
  
       [Serializable]
       public class BugMasterDetails
       {
           
           public string msircode { get; set; }
           public string actcode { get; set; }
           public string rsircode { get; set; }
           public string spcfcod { get; set; }
           public string msirdesc { get; set; }
           public string rsirdesc { get; set; }
           public string actdesc { get; set; }
           public string spcfdesc { get; set; }
           public string rsirunit { get; set; }
           public string actelev { get; set; }
           public double bgdqty { get; set; }
           public double bgdrate { get; set; }
           public double dram { get; set; }
           public double cram { get; set; }
           public string bgdrmrk { get; set; }
           public BugMasterDetails() { }
       }

        [Serializable]
       public class BudMasterResGroupWiseDate
       {
            public string rescode { get; set; }
           public string resdesc { get; set; }
           public string acgcode { get; set; }
           public string acgdesc { get; set; }
           public string resunit { get; set; }
           public double bgdam { get; set; }
           public double acam { get; set; }
           public double acamdr { get; set; }

            public BudMasterResGroupWiseDate() { }
       }

        [Serializable]
        public class BgdProjectAnalysis
        {
            public string rptcod { get; set; }
            public string flrcod { get; set; }
            public string flrdes { get; set; }
            public string rptdesc { get; set; }
            public string grpdesc { get; set; }
            public string rptdesc1 { get; set; }
            public string sirdesc1 { get; set; }
            public string rptunit { get; set; }
            public double rptqty { get; set; }
            public double rptrat { get; set; }
            public double rptamt { get; set; }
            public double peramt { get; set; }
            public BgdProjectAnalysis() { }
        }

        [Serializable]
        public class LandBgdStdAna
        {
            public string wrkcode { get; set; }
            public string rsircode { get; set; }
            public string wrkdesc { get; set; }
            public string rsirdesc { get; set; }
            public string bnumber { get; set; }
            public string rsirunit { get; set; }
            public string sirdesc1 { get; set; }
            public string rptunit { get; set; }
            public double lnght { get; set; }
            public double qty { get; set; }
            public double weight { get; set; }
            public double toweight { get; set; }
            public double tbase { get; set; }
            public double toqty { get; set; }
            public double wastage { get; set; }
            public double gtqty { get; set; }
            public LandBgdStdAna() { }
        }

        [Serializable]
        public class LandPurRegister
        {
            public string groupdesc { get; set; }
            public string actdesc { get; set; }
            public string resunit { get; set; }
            public string dhagno1 { get; set; }
            public string resdesc { get; set; }
            public double dhagtpur { get; set; }
            public double dhagbal { get; set; }
            public double lsize { get; set; }
            public double purchase { get; set; }
            public double bal { get; set; }
            public LandPurRegister() { }
        }

   }
}
