using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealERPEntity.C_12_Inv
{
    public class EClassIndentStatus
    {
        [Serializable]
        public class IndentStatus
        {
            public string comcod { get; set; }
            public string deptdesc { get; set; }
            public string rsircode { get; set; }
            public string sirunit { get; set; }
            public double issueqty { get; set; }
            public string refno { get; set; }
            public string isuno1 { get; set; }
            public string issueno { get; set; }
            public string ISSUEDAT { get; set; }
            public string isudat1 { get; set; }
            public string actcode { get; set; }
            public string deptno { get; set; }
            public string spcfcod { get; set; }
            public string sirdesc { get; set; }
            public string actdesc { get; set; }
        
            public string spcfdesc { get; set; }
            
            public IndentStatus()
            {

            }
        }
    }
}
