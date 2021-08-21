using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm.C_83_Att
{
   public class EClassAttendance
    {

       [Serializable]
       public class EmpAttendncLog
       {
           public DateTime cdate { get; set; }

       }

       [Serializable]
       public class EmpDailyAttenSummary
       {
           public string sectionname { get; set; }
           public double nofemployee { get; set; }
           public double absnt { get; set; }
           public double leave { get; set; }
           public double present { get; set; }
           public double lw5min { get; set; }
           public double lw6to10min { get; set; }
           public double lw11to30min { get; set; }
           public double la10am { get; set; }
           public double tolate { get; set; }
           public double eleavebofot { get; set; }
           public double leaveaofot { get; set; }
           public double toelaafleave { get; set; }
           public EmpDailyAttenSummary() { }
       }

       [Serializable]
       public class RptEmployeeStatus
       {
           public string department { get; set; }
           public string section { get; set; }
           public string empid { get; set; }
           public string idcard { get; set; }
           public string desigid { get; set; }
           public string desig { get; set; }
           public double netpay { get; set; }
           public string empname { get; set; }
           public string refno { get; set; }
           public DateTime joindate { get; set; }
           public DateTime retiredate { get; set; }
           public string acadeg { get; set; }
           public string passyear { get; set; }
           public DateTime condate { get; set; }
           public string tecst { get; set; }
           public string companyname { get; set; }
           public string departmentname { get; set; }
           public string sectionname { get; set; }
           public RptEmployeeStatus() { }

       }

    }
}
