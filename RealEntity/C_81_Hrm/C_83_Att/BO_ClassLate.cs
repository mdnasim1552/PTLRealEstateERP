using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm.C_83_Att
{
    public class BO_ClassLate
    {
        [Serializable]
        public class DailyLate
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string deptname { get; set; }
            public string secid { get; set; }
            public string section { get; set; }
            public string idcardno { get; set; }
            public string empname { get; set; }
            public string empnam { get; set; }
            public string desig { get; set; }
            public DateTime actualin { get; set; }
            public DateTime actualout { get; set; }
            public DateTime stdtimein { get; set; }
            public DateTime stdtimeout { get; set; }
            public DateTime ellfthour { get; set; }
            public string emlatemin { get; set; }
            public string remarks { get; set; }

       
        }


        [Serializable]
        public class MonthlyPresent
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string secid { get; set; }
            public string desig { get; set; }
            public string section { get; set; }
            public string empname { get; set; }
            public double present { get; set; }
            public double absnt { get; set; }
            public double holiday { get; set; }
            public double late { get; set; }
            public double noleav { get; set; }
            public double wd { get; set; }
            public MonthlyPresent ( ) { }
           
        }

        [Serializable]
        public class EmpSatausLate
        {
            public string comcod { get; set; }
            public string dayid { get; set; }
            public string empid { get; set; }
            public string idcardno { get; set; }
            public DateTime stdtimein { get; set; }
            public DateTime stdtimeout { get; set; }
            public DateTime actualin { get; set; }
            public DateTime actualout { get; set; }
            public string empdeptid { get; set; }
            public string empdept { get; set; } 
            public string empnam { get; set; }
            public string empdsg { get; set; }
            public string addtime2 { get; set; }
            public string addday { get; set; }
            public DateTime wintime { get; set; }
            public DateTime wouttime { get; set; }
            public EmpSatausLate() { }
        }

        [Serializable]
        public class Holiday
        {
            public string holidayType { get; set; }
            public DateTime HolidayDate { get; set; }
            public string Occasion { get; set; }

        }



    }
}
