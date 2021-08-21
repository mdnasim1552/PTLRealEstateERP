using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_39_MyPage
{
    public class EClassMIS
    {

        #region Land
        [Serializable]
        public class EvaluationonProject
        {

            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public DateTime tstdat { get; set; }
            public DateTime tenddat { get; set; }
            public double tper { get; set; }
            public double aper { get; set; }
            public EvaluationonProject() { }
     
            public EvaluationonProject(string pactcode, string pactdesc, DateTime tstdat, DateTime tenddat, double tper, double aper)
      
            {
                this.pactcode = pactcode;
                this.pactdesc = pactdesc;
                this.tstdat = tstdat;
                this.tenddat = tenddat;
                this.tper = tper;
                this.aper = aper;
            }

        }

        [Serializable]
        public class EmployeeEva
        {

            public string empid { get; set; }
            public string empname { get; set; }

            public double tar { get; set; }
            public double cumtar { get; set; }
            public double act { get; set; }
            public double cumact { get; set; }
            public double tper { get; set; }
            public string gpa { get; set; }
            public string graph { get; set; }
            public EmployeeEva() { }

            public EmployeeEva(string empid, string empname,  double tar, double cumtar, double act, double cumact, double tper, string gpa, string graph)
            {
                this.empid = empid;
                this.empname = empname;
                this.tar = tar;
                this.cumtar = cumtar;
                this.act = act;
                this.cumact = cumact;
                this.tper = tper;
                this.gpa = gpa;
                this.graph = graph;
            }



        }


        [Serializable]
        public class EmployeeEvaGen
        {

            public string empid { get; set; }
            public string empname { get; set; }

            public double tar { get; set; }
         
            public double act { get; set; }
         
        
            public string gpa { get; set; }
            public string graph { get; set; }
            public string desig { get; set; }
            public double gtar { get; set; }
            public double gact { get; set; }
            public EmployeeEvaGen() { }

            public EmployeeEvaGen(string empid, string empname, double tar, double act, string gpa, string graph, string desig, double gtar, double gact)
            {
                this.empid = empid;
                this.empname = empname;
                this.tar = tar;
                this.act = act;
                this.gpa = gpa;
                this.graph = graph;
                this.desig = desig;
                this.gtar = gtar;
                this.gact = gact;
            }



        }

        //Legal

        [Serializable]
        public class EmployeeEvaLeg
        {

            public string empid { get; set; }
          

            public double tar { get; set; }

            public double act { get; set; }


            public string gpa { get; set; }
              public string empname { get; set; }
            public string graph { get; set; }
            public string desig { get; set; }
            public double gtar { get; set; }
            public double gact { get; set; }
            public EmployeeEvaLeg() { }

            public EmployeeEvaLeg(string empid,  double tar, double act, string gpa, string empname, string graph, string desig, double gtar, double gact)
            {
                this.empid = empid;
               
                this.tar = tar;
                this.act = act;
                this.gpa = gpa;
                this.empname = empname;
                this.graph = graph;
                this.desig = desig;
                this.gtar = gtar;
                this.gact = gact;
            }



        }



        [Serializable]
        public class EmployeeEvaGenGraph
        {

          
            public double gtar { get; set; }
            public double gact { get; set; }
            public EmployeeEvaGenGraph() { }

            public EmployeeEvaGenGraph( double gtar, double gact)
            {
                
                this.gtar = gtar;
                this.gact = gact;
            }



        }



        [Serializable]

        public class EClassDepartment
        {

            public string deptcode { get; set; }
            public string deptname { get; set; }

            public EClassDepartment() { }

            public EClassDepartment(string deptcode, string deptname)
            {
                this.deptcode = deptcode;
                this.deptname = deptname;
                
            }



        }

        public class EmployeeHistory
        {

            public string empid { get; set; }
            public string empname { get; set; }
            public string pactcode { get; set; }
            public string actcode { get; set; }
            public string pactdesc { get; set; }
            public string actdesc { get; set; }
            public  DateTime disdate { get; set; }
            public string discuss { get; set; }
            public DateTime tstdat { get; set; }
            public DateTime tenddat { get; set; }

            public EmployeeHistory() { }

            public EmployeeHistory(string empid, string empname, string pactcode, string actcode, string pactdesc, string actdesc, DateTime disdate, string discuss, DateTime tstdat, DateTime tenddat)
            {
                this.empid = empid;
                this.empname = empname;
                this.pactcode = pactcode;
                this.actcode = actcode;
                this.pactdesc = pactdesc;
                this.actdesc = actdesc;
                this.disdate = disdate;
                this.discuss = discuss;
                this.tstdat = tstdat;
                this.tenddat = tenddat;
            }



        }

        [Serializable]
        public class EclassEmployeeHistoryLand
        {

            public string empid { get; set; }
            public string empname { get; set; }
            public string pactcode { get; set; }
            public string actcode { get; set; }
            public string pactdesc { get; set; }
            public string actdesc { get; set; }
            public DateTime cdate { get; set; }
            public string discuss { get; set; }
            public DateTime tstdat { get; set; }
            public DateTime tenddat { get; set; }
            public DateTime acstdat { get; set; }
            public DateTime acenddat { get; set; }
            public double deloadv { get; set; }
            public string deloadvsign { get; set; }
         

            public EclassEmployeeHistoryLand() { }

            public EclassEmployeeHistoryLand(string empid, string empname, string pactcode, string actcode, string pactdesc, string actdesc, DateTime cdate, string discuss, DateTime tstdat, DateTime tenddat, DateTime acstdat, DateTime acenddat, double deloadv, string deloadvsign)
            {
                this.empid = empid;
                this.empname = empname;
                this.pactcode = pactcode;
                this.actcode = actcode;
                this.pactdesc = pactdesc;
                this.actdesc = actdesc;
                this.cdate = cdate;
                this.discuss = discuss;
                this.tstdat = tstdat;
                this.tenddat = tenddat;
                this.acstdat = acstdat;
                this.acenddat = acenddat;
                this.deloadv = deloadv;
                this.deloadvsign = deloadvsign;
            }



        }


        [Serializable]
        public class EclassEmployeeHistoryMar
        {

            public string empid { get; set; }
            public string empname { get; set; }
            public string prono { get; set; }
            public string actcode { get; set; }
            public string refno { get; set; }
            public string actdesc { get; set; }
            public DateTime cdate { get; set; }
            public string discuss { get; set; }
            public DateTime tstdat { get; set; }
            public DateTime tenddat { get; set; }
            public DateTime acstdat { get; set; }
            public DateTime acenddat { get; set; }
            public double deloadv { get; set; }
            public string deloadvsign { get; set; }


            public EclassEmployeeHistoryMar() { }

            public EclassEmployeeHistoryMar(string empid, string empname, string prono, string actcode, string refno, string actdesc, DateTime cdate, string discuss, DateTime tstdat, DateTime tenddat, DateTime acstdat, DateTime acenddat, double deloadv, string deloadvsign)
            {
                this.empid = empid;
                this.empname = empname;
                this.prono = prono;
                this.actcode = actcode;
                this.refno = refno;
                this.actdesc = actdesc;
                this.cdate = cdate;
                this.discuss = discuss;
                this.tstdat = tstdat;
                this.tenddat = tenddat;
                this.acstdat = acstdat;
                this.acenddat = acenddat;
                this.deloadv = deloadv;
                this.deloadvsign = deloadvsign;
            }



        }

          [Serializable]
        public class EclassDeptPendWork
        {

            
             
            public string tarmon { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public double bal { get; set; }
            


            public EclassDeptPendWork() { }

            public EclassDeptPendWork(string tarmon, string empid, string empname, string actcode,  string actdesc, double bal)
            {
                this.tarmon = tarmon;
                this.empid = empid;
                this.empname = empname;
                this.actcode = actcode;
                this.actdesc = actdesc;
                this.bal = bal;
                
            }



        }

        #endregion


        #region Marketing

        public class EvaluationonProgram
        {

            public string prono { get; set; }
            public string refno { get; set; }
            public DateTime tstdat { get; set; }
            public DateTime tenddat { get; set; }
            public double tper { get; set; }
            public double aper { get; set; }
            public EvaluationonProgram() { }

            public EvaluationonProgram(string prono, string refno, DateTime tstdat, DateTime tenddat, double tper, double aper)
            {
                this.prono = prono;
                this.refno = refno;
                this.tstdat = tstdat;
                this.tenddat = tenddat;
                this.tper = tper;
                this.aper = aper;
            }

        }


        [Serializable]
        public class EmployeeHistory02
        {

            public string empid { get; set; }
            public string empname { get; set; }
            public string prono { get; set; }
            public string actcode { get; set; }
            public string refno { get; set; }
            public string actdesc { get; set; }
            public DateTime disdate { get; set; }
            public string discuss { get; set; }
            public DateTime tstdat { get; set; }
            public DateTime tenddat { get; set; }

            public EmployeeHistory02() { }

            public EmployeeHistory02(string empid, string empname, string prono, string actcode, string refno, string actdesc, DateTime disdate, string discuss, DateTime tstdat, DateTime tenddat)
            {
                this.empid = empid;
                this.empname = empname;
                this.prono = prono;
                this.actcode = actcode;
                this.refno = refno;
                this.actdesc = actdesc;
                this.disdate = disdate;
                this.discuss = discuss;
                this.tstdat = tstdat;
                this.tenddat = tenddat;
            }



        }

        public class EClassEmpHistory02
        {
            public string prono { get; set; }
            public string refno { get; set; }
            public string actdesc { get; set; }
            public double duration { get; set; }
            public double aduration { get; set; }
            public double deloadv { get; set; }

            public string deloadvsign { get; set; }



            public EClassEmpHistory02() { }
            public EClassEmpHistory02(string prono, string refno, string actdesc, double duration, double aduration, double deloadv, string deloadvsign)
            {
                this.prono = prono;
                this.refno = refno;
                this.actdesc = actdesc;
                this.duration = duration;
                this.aduration = aduration;
                this.deloadv = deloadv;
                this.deloadvsign = deloadvsign;




            }





        }
        #endregion


        [Serializable]
        public class EclassDeptEvaSheet
        {

            public string deptcode { get; set; }
            public double nofemp { get; set; }
            public double tar { get; set; }
            public double avgact { get; set; }
            public string prgdesc { get; set; }
            public string gpa { get; set; }

            public EclassDeptEvaSheet(){}
            public EclassDeptEvaSheet(string deptcode, double nofemp, double tar, double avgact, string prgdesc, string gpa)
            {
                this.deptcode = deptcode;
                this.nofemp = nofemp;
                this.tar = tar;
                this.avgact = avgact;
                this.prgdesc = prgdesc;
                this.gpa = gpa;

            }

        }


    }
}
