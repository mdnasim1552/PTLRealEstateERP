using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_12_Inv
{
    public class EClassMaterial
    {
        [Serializable]
        public class MatTransStatus
        {
            public string comcod { get; set; }
            public string trnno { get; set; }

            public string refno { get; set;}
            public string getpref { get; set; }
            public string tfpactcode { get; set; }
            public string tfproj { get; set; }
            public string ttpactcode { get; set; }
            public string ttproj { get; set; }
            public string trsircode { get; set; }
            public string sirdesc { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public string tunit { get; set; }
            public double tqty { get; set; }
            public double trate { get; set; }
            public double tamount { get; set; }
            public DateTime tdate { get; set; }


            public MatTransStatus()
            {

            }
        }
    }
}
