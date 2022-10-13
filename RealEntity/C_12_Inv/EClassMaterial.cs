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

            public string refno { get; set; }
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
        [Serializable]
        public class IndentStatusSummary
        {
            public string comcod { get; set; }
            public string deptdesc { get; set; }
            public string pactcode { get; set; }
            public string deptno { get; set; }
            public string rsircode { get; set; }
            public double openqty { get; set; }
            public double rcvqty { get; set; }
            public double issueqty { get; set; }
            public double blncqty { get; set; }
            public string rsirdesc { get; set; }
            public string pactdesc { get; set; }
            public string deptname { get; set; }
           
            public IndentStatusSummary()
            {

            }
        }
    }
}
