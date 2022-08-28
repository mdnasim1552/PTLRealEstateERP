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
            public double chkamt { get; set; }
            public double aprqty { get; set; }
            public double apramt { get; set; }
            public double percnt { get; set; }
            public string type { get; set; }
        }
    }
}
