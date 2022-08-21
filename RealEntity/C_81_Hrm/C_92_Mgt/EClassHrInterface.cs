using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm.C_92_Mgt
{
  public  class EClassHrInterface
    {
        [Serializable]
        public class HrInterfaceLeave {

            public string gcod { get; set; }
            public string gdesc { get; set; }
            public double entitle { get; set; }
            public double permonth { get; set; }
            public double pbal { get; set; }
            public double ltaken { get; set; }
            public double balleave { get; set; }
            public double tltakreq { get; set; }
            public DateTime lenjoydt1 { get; set; }
            public DateTime lenjoydt2 { get; set; }
            public double lenjoyday { get; set; }
            public DateTime appdate { get; set; }
            public double applyday { get; set; }
            public double appday { get; set; }
            public DateTime applydate { get; set; }
            public DateTime lrstrtdat { get; set; }
            public DateTime lrentdat { get; set; }


        }


        [Serializable]
        public class EclassSepEmployee
        {
            public string empid { get; set; }
            public string empname { get; set; }
            public string empname1 { get; set; }
            public string septype { get; set; }
            public string septypedesc { get; set; }
            public DateTime retdat { get; set; }
            public DateTime joindat { get; set; }
            public string idno { get; set; }
            public string designation { get; set; }
            public string deptcode { get; set; }
            public string deptname { get; set; }
            public string servleng { get; set; }
            public DateTime billdate { get; set; }
            public bool aprvstatus { get; set; }
            public double ttlamt { get; set; }
            public string refno { get; set; }


        }

        [Serializable]
        public class EclassSttlemntInfo
        {
            public string hrgcod { get; set; }
            public string hrgdesc { get; set; }
            public double amount { get; set; }
            public double numofday { get; set; }
            public double perday { get; set; }
            public double ttlamt { get; set; }
        }
        [Serializable]
        public class SummarySalarySheet
        {
            public string comcod { get; set; }
            public double bankemp { get; set; }
            public double cashemp { get; set; }
            public double toemp { get; set; }
            public double bankamt { get; set; }
            public double cashamt { get; set; }
            public double toamt { get; set; }
            public string sectionname { get; set; }
            public SummarySalarySheet() { }
        }

        public class BO_EmpConfirm
        {
            public string cardno { get; set; }
            public string empname { get; set; }
            public string desig { get; set; }
            public string joindat { get; set; }
            public string condat { get; set; }
            public string remarks { get; set; }

        }
        

        [Serializable]


        public class EmpOfferLetter
        {

//           
            public string advno { get; set; }

            public string comcod { get; set; }
            public string name { get; set; }
            public string desig { get; set; }
            public string mobile { get; set; }
            public string email { get; set; }
            public string preadd { get; set; }
            //public string peradd { get; set; }
            public string dept { get; set; }
            public string sec { get; set; }
            //public string bsal { get; set; }
            //public string hrent { get; set; }
            //public string conven { get; set; }
            //public string mallow { get; set; }
            //public string eleave { get; set; }
            //public string cleave { get; set; }
            //public string sleave { get; set; }
            //public string doj { get; set; }
            //public string grade { get; set; }
            //public string refno { get; set; }
           

            public EmpOfferLetter()
            {

            }

        }

    }
}
