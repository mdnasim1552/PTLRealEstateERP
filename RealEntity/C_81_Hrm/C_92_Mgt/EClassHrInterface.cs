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
        [Serializable]

        public class EmptaskDesk
        {
            public string comcod { get; set; }
            public double bankemp { get; set; }
            public double cashemp { get; set; }
            public double toemp { get; set; }
            public double bankamt { get; set; }
            public double cashamt { get; set; }
            public double toamt { get; set; }
            public string fdate { get; set; }
            public string empdesc { get; set; }
            public string ftime  { get; set; }
            public string ttime { get; set; }
            public string durtime { get; set; } 
            public string taskdesc { get; set; }
            public string tdesc { get; set; }
            public string taskcode { get; set; }
            public string rowid { get; set; }
            public string floctn { get; set; }
            public string tloctn { get; set; }
            public string tloctndesc { get; set; }
            public string empcode { get; set; }
            public string floctndesc { get; set; }
            public string rmk { get; set; }

            public string sectionname { get; set; }
            public EmptaskDesk()
            {

            }

        }
        [Serializable]
        public class EInterfaceLeave
        {
            public string comcod { get; set; }
            public double bankemp { get; set; }
            public double cashemp { get; set; }
            public double toemp { get; set; }
            public double bankamt { get; set; }
            public double cashamt { get; set; }
            public double toamt { get; set; }
            public string ltrnid { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public DateTime aplydat { get; set; }
            public DateTime strtdat { get; set; }
            public DateTime enddat { get; set; }
            public string duration { get; set; }
            public string idcard { get; set; }
            public string deptanme { get; set; }
            public string desig { get; set; }
            public string lvtype { get; set; }
            public string lreason { get; set; }
            public string denameadesig { get; set; }
            public string lvstatus { get; set; }
            public string lvstatus1 { get; set; }
            

            public string sectionname { get; set; }
            public EInterfaceLeave()
            {

            }
        }
        [Serializable]
        public class EInterfaceAttApp
        {
            public string comcod { get; set; }
            public double bankemp { get; set; }
            public double cashemp { get; set; }
            public double toemp { get; set; }
            public double bankamt { get; set; }
            public double cashamt { get; set; }
            public double toamt { get; set; }
            public string ltrnid { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public DateTime aplydat { get; set; }
            public DateTime strtdat { get; set; }
            public DateTime enddat { get; set; }
            public string duration { get; set; }
            public string idcard { get; set; }
            public string deptanme { get; set; }
            public string desig { get; set; }
            public string lvtype { get; set; }
            public string intime { get; set; }
            public string toleavo { get; set; }
            public string outtime { get; set; }
            public string toleavin { get; set; }
            public string empreson { get; set; }
            public string denameadesig { get; set; }
            public string lvstatus { get; set; }
            public string lvstatus1 { get; set; }


            public string sectionname { get; set; }
            public EInterfaceAttApp()
            {

            }
        }
        [Serializable]
        public class ERptGroupAtt
        {
            public string comcod { get; set; }
            public string comnam { get; set; }
            public string deptdesc { get; set; }
            public double ttlstap { get; set; }
            public double present { get; set; }
            public double late { get; set; }
            public double earlyLev { get; set; }
            public double onlev { get; set; }
            public double absnt { get; set; }


            public ERptGroupAtt()
            {

            }
        }
        [Serializable]
        public class Elvlateabbs
        {
            public string comcod { get; set; }
            public string comnam { get; set; }
            public string inempname { get; set; }
            public string incomm { get; set; }
            public string outcomm { get; set; }
            public string lempname { get; set; }
            public string lcomm { get; set; }
            public string elempname { get; set; }
            public string elcomm { get; set; }
            public string olempname { get; set; }
            public string olcomm { get; set; }
            public string aempname { get; set; }
            public string acomm { get; set; }


            public Elvlateabbs()
            {

            }
        }
        [Serializable]
        public class Elvlateabbs02
        {
            public string comcod { get; set; }
            public string comnam { get; set; }

            public string lempname { get; set; }
            public string lempdisg { get; set; }
            public string lempdept { get; set; }
            public string lcomm { get; set; }
            public string lresaon { get; set; }

            public string elempname { get; set; }
            public string elempdisg { get; set; }
            public string elemdept { get; set; }
            public string elcomm { get; set; }

            public string olempname { get; set; }
            public string olempdisg { get; set; }
            public string olemdept { get; set; }
            public string olcomm { get; set; } 

            public string inempname { get; set; }
            public string inempdisg { get; set; }
            public string ineemdept { get; set; }
            public string incomm { get; set; }

            public string aempname { get; set; }
            public string aempdisg { get; set; }
            public string aeemdept { get; set; }
            public string acomm { get; set; }

            public string outcomm { get; set; }

            public Elvlateabbs02()
            {

            }
        }



    }
}
