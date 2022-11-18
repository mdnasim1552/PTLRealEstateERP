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

        [Serializable]
        public class RptMonAttnSumEmpWise
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string idcardno { get; set; }
            public string company { get; set; }
            public string deptid { get; set; }
            public string section { get; set; }
            public string joindate { get; set; }
            public string desigid { get; set; }
            public string gradeid { get; set; }
            public int present { get; set; }
            public int latecount { get; set; }
            public int earlv { get; set; }
            public int ab { get; set; }
            public int wh { get; set; }
            public int fh { get; set; }
            public int cl { get; set; }
            public int sl { get; set; }
            public int ml { get; set; }
            public int el { get; set; }
            public int wpl { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string companyname { get; set; }
            public string sectionname { get; set; }
            public string deptname { get; set; }
            public string grade { get; set; }
            public int paydays { get; set; }
            public int monthdays { get; set; }
            public int wrkday { get; set; }
            public int holiday { get; set; }
            public int leave { get; set; }

            public RptMonAttnSumEmpWise()
            {
                    
            }
        }
    }
}
