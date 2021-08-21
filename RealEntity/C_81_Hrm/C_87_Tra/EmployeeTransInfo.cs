using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm.C_87_Tra
{
    [Serializable]
    public class EmployeeTransInfo
    {
        public string comcod { get; set; }
        public string idcard { get; set; }
        public string trnno { get; set; }
        public string fcomcod { get; set; }
        public string fcompany { get; set; }
        public string fdepart { get; set; }
        public string tsircode { get; set; }
        public string tcompany { get; set; }
        public string tdepart { get; set; }
        public string empid { get; set; }
        public string empname { get; set; }
        public DateTime tdate { get; set; }
        public string gcod { get; set; }
        public string hrgdesc { get; set; }

        public EmployeeTransInfo()
        {

        }
    }

    [Serializable]
    public class EmployeeTransInfo01
    {
        public string comcod { get; set; }
        public string idcardno { get; set; }
        public string empname { get; set; }
        public string desig { get; set; }
        public string tfcomdesc { get; set; }
        public string tfprjdesc { get; set; }
        public string ttcomdesc { get; set; }
        public string ttprjdesc { get; set; }
        public DateTime pplacedate { get; set; }
        public string empid { get; set; }
        public string rmrks { get; set; }
        public string address { get; set; }
        public EmployeeTransInfo01()
        {

        }
    }
}
