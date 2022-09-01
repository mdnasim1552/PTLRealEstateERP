using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm.C_81_Rec
{
   public class BO_ClassManPower
    {
        [Serializable]
        public class HrgcodType
        {
            public string hrgcod { get; set; }
            public string hrgdesc { get; set; }
            public HrgcodType() { }

            public HrgcodType(string hrgcod, string hrgdesc)
            {
                this.hrgcod = hrgcod;
                this.hrgdesc = hrgdesc;
            }

        }

        //comcod, actcode, actdesc
        [Serializable]
        public class HrSirInf
        {
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public HrSirInf() { }
            public HrSirInf(string actcode, string actdesc)
            {
                this.actcode = actcode;
                this.actdesc = actdesc;
            }

        }

        [Serializable]
        public class HrSectionList
        {
            public string sectionname { get; set; }
            public string section { get; set; }
            public HrSectionList(string sectionname, string section)
            {
                this.sectionname = sectionname;
                this.section = section;
            }

        }

        [Serializable]
        public class HrDeisgList
        {
            public string designation { get; set; }
            public string desigcod { get; set; }
            public HrDeisgList(string designation, string desigcod)
            {
                this.designation = designation;
                this.desigcod = desigcod;
            }

        }

        [Serializable]

        //a.comcod, a.bgdyear, a.bgdtypecode, a.emptypecode, a.dptcode, a.seccode, a.desigcode, a.grade, a.numpeople, a.avgsalary, a.ttlsalary, a.reson, a.imgofmemo, bgdtdesc=isnull(b.hrgdesc,''), emptypdesc=isnull(c.hrgdesc,''), deptdesc=isnull(d.sirdesc,''), sectdesc=isnull(e.sirdesc,''), desigdesc=isnull(f.hrgdesc,''), gradedesc=isnull(g.hrgdesc,'')
       
        public class ManPowerBudgt
        {
            public string comcod { get; set; }
            public string bgdyear { get; set; }
            public string bgdtypecode { get; set; }
            public string emptypecode { get; set; }
            public string dptcode { get; set; }
            public string seccode { get; set; }
            public string desigcode { get; set; }
            public string grade { get; set; }
            public double numpeople { get; set; }
            public double avgsalary { get; set; }
            public double ttlsalary { get; set; }
            public string reson { get; set; }
            public string imgofmemo { get; set; }
            public string bgdtdesc { get; set; }
            public string emptypdesc { get; set; }
            public string deptdesc { get; set; }
            public string sectdesc { get; set; }
            public string desigdesc { get; set; }
            public string gradedesc { get; set; }
            public string wsdesc { get; set; }
            public string divdesc { get; set; } 
            public ManPowerBudgt()
            {
            }
        }

        [Serializable]
        public class ManPowerBudgtActual
        {
            public string comcod { get; set; }
            public string bgdyear { get; set; }
            public string bgdtypecode { get; set; }
            public string emptypecode { get; set; }
            public string dptcode { get; set; }
            public string seccode { get; set; }
            public string desigcode { get; set; }
            public string grade { get; set; }
            public double numpeople { get; set; }
            public double joinpeop { get; set; }
            public double avgsalary { get; set; }
            public double ttlsalary { get; set; }
            public double grsalry { get; set; }
            public double paysalr { get; set; }
            public string bgdtdesc { get; set; }
            public string emptypdesc { get; set; }
            public string deptdesc { get; set; }
            public string sectdesc { get; set; }
            public string desigdesc { get; set; }
            public string gradedesc { get; set; }
            public string wsdesc { get; set; }
            public string divdesc { get; set; }
            public double balpeople { get; set; } 
            public ManPowerBudgtActual()
            {
            }
        }
        [Serializable]
        public class Empnomineelist
        {
            public string gcod { get; set; }
            public string gval { get; set; }
            public string nomname { get; set; }
            public string rel { get; set; }
            public string age { get; set; }
            public string perctben { get; set; }
            public string signurl { get; set; }
            public string nompic { get; set; }
            public string nomaddress { get; set; }

            public string comcod { get; set; }
            public string empid { get; set; }
            public string empnam { get; set; }
            public DateTime joindate { get; set; }
            public string degnam { get; set; }
            public string idno { get; set; }
            public string deptnam { get; set; }
            public string sesonnam { get; set; }
            public string bdate { get; set; }
            public string gender { get; set; }
            public string fnam { get; set; }
            public string mname { get; set; }
            public double paddess { get; set; }
          
            public Empnomineelist()
            {
            }
        }
        [Serializable]
        public class EmpAllInformation
        {
            public string grpid { get; set; }
            public string empid { get; set; }
            public string gcod { get; set; }
            public string gdesc { get; set; }
            public string gdescbn { get; set; }
            public double amt { get; set; }
            public string gdatat1 { get; set; }
            public string gdatat2 { get; set; }
            public string gdatat3 { get; set; }
            public string gdatat4 { get; set; }
            public double percnt { get; set; }
            public string unit { get; set; }
            public double qty { get; set; }
            public double raate { get; set; }
            public string grpdesc { get; set; }
            public string grpdescbn { get; set; }
            public string tdesc { get; set; }
            public string tdescbn { get; set; }
            public string empimg { get; set; }
            public string empsign { get; set; }

            public EmpAllInformation() { }
        }
        public class EmpAllInformationBn
        {
            public string grpid { get; set; }
            public string empid { get; set; }
            public string gcod { get; set; }
            public string gdesc { get; set; }
            public string gdescbn { get; set; }
            public string amt { get; set; }
            public string gdatat1 { get; set; }
            public string gdatat2 { get; set; }
            public string gdatat3 { get; set; }
            public string gdatat4 { get; set; }
            public double percnt { get; set; }
            public string unit { get; set; }
            public double qty { get; set; }
            public double raate { get; set; }
            public string grpdesc { get; set; }
            public string grpdescbn { get; set; }
            public string tdesc { get; set; }
            public string tdescbn { get; set; }
            public string empimg { get; set; }
            public string empsign { get; set; }

            public EmpAllInformationBn() { }
        }

        [Serializable]


        public class EmployeeAllInfo
        {
            public string grpid { get; set; }
            public string empid { get; set; }
            public string gcod { get; set; }
            public string gdesc { get; set; }
            public string gdescbn { get; set; }
            public double amt { get; set; }
            public string gdatat1 { get; set; }
            public string gdatat2 { get; set; }
            public string gdatat3 { get; set; }
            public string gdatat4 { get; set; }
            public double percnt { get; set; }
            public string unit { get; set; }
            public double qty { get; set; }
            public double raate { get; set; }
            public string grpdesc { get; set; }
            public string grpdescbn { get; set; }
            public string tdesc { get; set; }
            public string tdescbn { get; set; }
            public string empimg { get; set; }
            public string empsign { get; set; }

            public EmployeeAllInfo()
            {

            }
        }



    }
}

