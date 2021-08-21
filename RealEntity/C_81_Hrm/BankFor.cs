using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm
{
    public class BankFor
    {

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

            public BankFord() { }

        }
    

    }
}
