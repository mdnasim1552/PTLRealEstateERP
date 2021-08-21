using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm
{
   public class IndvPf
    {
        public class PaymentScheduleList
        {
            public string paydate { get; set; }
            public decimal salary { get; set; }
            public decimal pfamt { get; set; }
            public decimal contribu { get; set; }
            public decimal balance { get; set; }
            public PaymentScheduleList() { }
        }
       public class Empinfo
       {
           public string empid { get; set; }
           public string name { get; set; }
           public string joindate { get; set; }
           public string desig { get; set; }
           public string idcard { get; set; }
          public double months { get; set; }
           public double years { get; set; }

       }
    }
}
