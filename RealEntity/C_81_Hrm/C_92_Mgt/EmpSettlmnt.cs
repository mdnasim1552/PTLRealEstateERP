using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm.C_92_mgt
{
   public  class EmpSettlmnt
    {
       [Serializable]
       public class EmpFinalSettlmnt
       {
 
           public string code { get; set; }
           public string gdesc { get; set; }
           public double sal { get; set; }
           public EmpFinalSettlmnt ( ) { }
          
       }


        [Serializable]
       public class EmpList
       {
 

           public int rowid { get; set; }
           public string  company { get; set; }
           public string  secid { get; set; }
           public string  desigid { get; set; }
           public string  empid { get; set; }
           public string empname { get; set; }

           public string  idcardno { get; set; }
           public string  companyname { get; set; }
           public string  section { get; set; }
           public string  desig { get; set; }
           public string  joindate { get; set; }
           public string  birthdate { get; set; }

           public string  slength { get; set; }
           public string  blood { get; set; }

           public double  salary { get; set; }


           public EmpList ( ) { }

    
          
       }



   [Serializable]
       public class InActiveEmpList
       {
 
        
           public int rowid { get; set; }
           public string  company { get; set; }
           public string  secid { get; set; }
           public string  desigid { get; set; }
           public string  empid { get; set; }
           public string  empname { get; set; }      
           public string  idcardno { get; set; }
           public string  companyname { get; set; }
           public string  section { get; set; }
           public string  desig { get; set; }
           public string  joindate { get; set; }
           public string  slength { get; set; }
           public string  blood { get; set; }
           public DateTime  resdat { get; set; }
       
           public InActiveEmpList ( ) { }
          
       }


   [Serializable]
   public class TotalEmpList
   {

      
       public int rowid { get; set; }
       public string company { get; set; }
       public string secid { get; set; }
       public string desigid { get; set; }
       public string empid { get; set; }
       public string empname { get; set; }
       public string idcardno { get; set; }
       public string companyname { get; set; }
       public string section { get; set; }
       public string desig { get; set; }
       public string joindate { get; set; }
       public string slength { get; set; }
       public string blood { get; set; }
       public DateTime resdat { get; set; }

       public TotalEmpList() { }

   }


          [Serializable]
   public class EmpLoanStatus
   {    
       public string idcard { get; set; }
       public string empid { get; set; }
       public string empname { get; set; }    
       public string refno { get; set; }
       public string refdesc { get; set; }
       public string section { get; set; }
       public string secdesc { get; set; }
       public string desigid { get; set; }
       public string desig { get; set; }
       public double tloan { get; set; }
       public double paidamt { get; set; }
       public double balamt { get; set; }
       public double lnamt { get; set; }    
       public DateTime insdat { get; set; }      
       public EmpLoanStatus() { }

   }


    

    }
}
