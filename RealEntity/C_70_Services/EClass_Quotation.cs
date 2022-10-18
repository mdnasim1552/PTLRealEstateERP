using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealERPEntity.C_70_Services
{
    public class EClass_Quotation
    {
        [Serializable]
        public class EQuotation
        {
            public string comcod { get; set; }
            public string worktypecode { get; set; }
            public string worktypedesc { get; set; }
            public string resourcecode { get; set; }
            public string resourcedesc { get; set; }
            public string unit { get; set; }
            public double qrate { get; set; }
            public double qqty { get; set; }
            public double qamt { get; set; }
            public double chkqty { get; set; }
            public double chkrate { get; set; }
            public double chkamt { get; set; }
            public double aprqty { get; set; }
            public double aprrate { get; set; }
            public double apramt { get; set; }
            public double percnt { get; set; }
            public double chkpercnt { get; set; }
            public double aprpercnt { get; set; }
            public string type { get; set; }
            public bool isprocess { get; set; }
        }
        [Serializable]
        public class EQuotationinfo
        {
            public string comcod { get; set; }
            public string quotid { get; set; }
            public string quotid1 { get; set; }
            public DateTime quotdate { get; set; }
            public string customerid { get; set; }
            public string pactcode { get; set; }
            public string custdesc { get; set; }
            public string worktype { get; set; }
            public string workdesc { get; set; }
            public string qamt { get; set; }
            public string qqty { get; set; }
            public double apramt { get; set; }
            public double aprqty { get; set; }
            public double aprate { get; set; }
            public string status { get; set; }
            public string aptdesc { get; set; }
            public string phone { get; set; }
            public string paddress { get; set; }
            public string rsirdesc { get; set; }
            public string remarks { get; set; }


        }

    }
}
