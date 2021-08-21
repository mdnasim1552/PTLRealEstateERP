using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm.C_86_All
{
   public class EClassAllowance
    {

       [Serializable]
       public class EClassMobileBill
       { 
       
           public string deptid { get; set; }
           public string secid { get; set; }
           public string desig { get; set; }
           public string empname { get; set; }
           public string idcardno { get; set; }
           public double mbillamt { get; set; }
           public double mbilllimit { get; set; }
           public double intbill { get; set; }
           public double balance { get; set; }
           public string deptname { get; set; }
           public string section { get; set; }
           public string phone { get; set; }


           public EClassMobileBill()
           { 
           }
           
           
       
       
       }
    }
}
