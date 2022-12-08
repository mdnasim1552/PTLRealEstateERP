using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm.C_92_mgt
{
  public  class BO_ClassEmployee
    {
      [Serializable]
      public class EmployeeInfo
      {
          //a.comcod,  a.rowid,  a.company, a.secid, a.desigid,  a.empid, a.idcardno, a.companyname, a.section, a.desig, a.empname, joindate=format(a.joindate,'dd-MMM-yyy'), a.birthdate, a.slength,resdat
          public string comcod { get; set; }
          public string rowid { get; set; }
          public string company { get; set; }
          public string secid { get; set; }
          public string desigid { get; set; }
          public string empid { get; set; }
          public string idcardno { get; set; }
          public string companyname { get; set; }
          public string section { get; set; }
          public string desig { get; set; }
          public string empname { get; set; }
          public string joindate { get; set; }
          public string birthdate { get; set; }
          public string slength { get; set; }
          public string blood { get; set; }
          public decimal salary { get; set; }
          public DateTime resdat { get; set; }
          public string mobile { get; set; }
          public string email { get; set; }
          public string extention { get; set; }

           
          public EmployeeInfo() { }

      }
        [Serializable]
        public class EmployeeIDCardInfo
        {
            //a.comcod,  a.rowid,  a.company, a.secid, a.desigid,  a.empid, a.idcardno, a.companyname, a.section, a.desig, a.empname, joindate=format(a.joindate,'dd-MMM-yyy'), a.birthdate, a.slength,resdat
            public string comcod { get; set; }
            public string rowid { get; set; }
            public string company { get; set; }
            public string secid { get; set; }
            public string desigid { get; set; }
            public string empid { get; set; }
            public string idcardno { get; set; }
            public string companyname { get; set; }
            public string section { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string empimg { get; set; }
            public string joindate { get; set; }
            public string birthdate { get; set; }
            public string slength { get; set; }
            public string blood { get; set; }
            public decimal salary { get; set; }
            public DateTime resdat { get; set; }
            public string mobile { get; set; }
            public string email { get; set; }
            public string extention { get; set; }


            public EmployeeIDCardInfo() { }

        }
        [Serializable]
        public class EmpTransList
        {
            public string empname { get; set; }
            public string fcompname { get; set; }
            public string fdesig { get; set; }
            public string tfdeptname { get; set; }
            public string tcompname { get; set; }
            public string tdesig { get; set; }
            public string ttdeptname { get; set; }
            public DateTime tdate { get; set; }
            public string rmrks { get; set; }
            public EmpTransList() { }
        }

        [Serializable]
        public class EmpSepList
        {
            public string compcod { get; set; }
            public string empname { get; set; }
            public double rowid { get; set; }
            public string compname { get; set; }
            public string department { get; set; }
            public string companyname { get; set; }
            public string idcardno { get; set; }
            public string desig { get; set; }
            public string secname { get; set; }
            public string spdesc { get; set; }
            public string company { get; set; }
            public DateTime spdate { get; set; }
            public DateTime frmdate { get; set; }
            public DateTime todate { get; set; }
            public string section { get; set; }
            public double taday { get; set; }
            public double noj { get; set; }
            public string remarks { get; set; }
            public EmpSepList() { }
        }
    }
}
