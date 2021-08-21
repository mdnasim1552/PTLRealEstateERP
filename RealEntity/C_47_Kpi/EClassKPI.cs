using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEntity.C_47_Kpi
{

    public class EClassEmpCode
    {
        public string empid { get; set; }
        public string empname { get; set; }
        public string empname1 { get; set; }
        public string desig { get; set; }
        public DateTime joindate { get; set; }
        public EClassEmpCode() { }
        public EClassEmpCode(string empid, string empname, string empname1, string desig, DateTime joindate)
        {

            this.empid = empid;
            this.empname = empname;
            this.empname1 = empname1;
            this.desig = desig;
            this.joindate = joindate;
        }

    }
    [Serializable]
    public class EClassEmpCode2
    {
        public string empid { get; set; }
        public string empname { get; set; }
        public string empname1 { get; set; }
        public string desgcod { get; set; }
        public string desg { get; set; }
        public DateTime joindat { get; set; }
        public string deptcode { get; set; }
        public string deptname { get; set; }
        public EClassEmpCode2() { }
        public EClassEmpCode2(string empid, string empname, string empname1, string desgcod, string desg, DateTime joindat, string deptcode, string deptname)
        {

            this.empid = empid;
            this.empname = empname;
            this.empname1 = empname1;
            this.desgcod = desgcod;
            this.desg = desg;
            this.joindat = joindat;
            this.deptcode = deptcode;
            this.deptname = deptname;
        }

    }

    public class EClassCompCode
    {
        public string comcod { get; set; }
        public string comnam { get; set; }
        public EClassCompCode() { }
        public EClassCompCode(string comcod, string comnam)
        {

            this.comcod = comcod;
            this.comnam = comnam;
        }

    }
    public class EClassDptCode
    {
        public string wrkdpt { get; set; }
        public string wrkdptdesc { get; set; }
        public EClassDptCode() { }
        public EClassDptCode(string wrkdpt, string wrkdptdesc)
        {

            this.wrkdpt = wrkdpt;
            this.wrkdptdesc = wrkdptdesc;
        }

    }
    public class EClassSirCode
    {
        public string sircode { get; set; }
        public string sirdesc { get; set; }
        public EClassSirCode() { }
        public EClassSirCode(string sircode, string sirdesc)
        {

            this.sircode = sircode;
            this.sirdesc = sirdesc;
        }

    }

    [Serializable]
    public class EClassClientCode
    {
        public string ccode { get; set; }
        public string cdesc { get; set; }
        public EClassClientCode() { }
        public EClassClientCode(string ccode, string cdesc)
        {

            this.ccode = ccode;
            this.cdesc = cdesc;
        }

    }


    [Serializable]
    public class EClassLandowner
    {
        public string ccode { get; set; }
        public string cdesc { get; set; }
        public double lsize { get; set; }
        public double lamt { get; set; }
        public double broamt { get; set; }
        public EClassLandowner() { }
        public EClassLandowner(string ccode, string cdesc, double lsize, double lamt, double broamt)
        {

            this.ccode = ccode;
            this.cdesc = cdesc;
            this.lsize = lsize;
            this.lamt = lamt;
            this.broamt = broamt;
        }

    }

    [Serializable]
    public class EClassClientDis
    {

        public string grp { get; set; }
        public string grpdesc { get; set; }
        public string empid { get; set; }
        public string empname { get; set; }
        public string prosdesc { get; set; }
        public string gdesc { get; set; }
        public string gdesc1 { get; set; }

        public string gdesc2 { get; set; }
        public string gdesc3 { get; set; }
        public string gdesc4 { get; set; }
        public string gdesc5 { get; set; }
        public string gdesc6 { get; set; }
        public string gdesc7 { get; set; }
        public string gdesc8 { get; set; }
        public string gdesc9 { get; set; }
        public string gdesc10 { get; set; }
        public string gdesc11 { get; set; }
        public string gdesc12 { get; set; }
        public string gdesc13 { get; set; }
        public string gdesc14 { get; set; }
        public string gdesc15 { get; set; }
        public string gdesc16 { get; set; }
        public string gdesc17 { get; set; }
        public string gdesc18 { get; set; }

        public string gdesc19 { get; set; }

        public string phone { get; set; }
        public double difpercnt { get; set; }
        public string uausize { get; set; }
        
        
        public EClassClientDis() { }
        public EClassClientDis(string grp, string grpdesc, string empid, string empname, string prosdesc, string gdesc, string gdesc1, string gdesc2, string gdesc3, string gdesc4, string gdesc5, 
            string gdesc6, string gdesc7, string gdesc8, string gdesc9, string gdesc10, string gdesc11, string gdesc12, string gdesc13, string gdesc14, string gdesc15,
            string gdesc16, string gdesc17, string gdesc18, string gdesc19, string phone, double difpercnt, string uausize)
        {

            this.grp = grp;
            this.grpdesc = grpdesc;
            this.empid = empid;
            this.empname = empname;
            this.prosdesc = prosdesc;
            this.gdesc = gdesc;
            this.gdesc1 = gdesc1;
            this.gdesc2 = gdesc2;
            this.gdesc3 = gdesc3;
            this.gdesc4 = gdesc4;
            this.gdesc5 = gdesc5;
            this.gdesc6 = gdesc6;
            this.gdesc7 = gdesc7;
            this.gdesc8 = gdesc8;
            this.gdesc9 = gdesc9;
            this.gdesc10 = gdesc10;
            this.gdesc11 = gdesc11;
            this.gdesc12 = gdesc12;
            this.gdesc13 = gdesc13;
            this.gdesc14 = gdesc14;
            this.gdesc15 = gdesc15;
            this.gdesc16 = gdesc16;
            this.gdesc17 = gdesc17;
            this.gdesc18 = gdesc18;
            this.gdesc19 = gdesc19;
            this.phone = phone;
            this.difpercnt = difpercnt;
            this.uausize = uausize;


            
        }

       

        

    }

    public class EClassHeader
    {
        public string sircode { get; set; }
        public string sirdesc { get; set; }
        public EClassHeader() { }
        public EClassHeader(string sircode, string sirdesc)
        {

            this.sircode = sircode;
            this.sirdesc = sirdesc;
        }

    }

    [Serializable]
    public class EClassDaySales
    {

        public string custname { get; set; }
        public string custadd { get; set; }
        public string gcod { get; set; }
        public string pactcode { get; set; }
        public string usircode { get; set; }

        public string pactdesc { get; set; }
        public string udesc { get; set; }
        public string munit { get; set; }
        public string schdate { get; set; }

        public double tuamt { get; set; }
        public double suamt { get; set; }
        public double disamt { get; set; }
        public double disper { get; set; }
        public double usize { get; set;}

        public double sftpr { get; set; }
        


        public EClassDaySales() { }
        public EClassDaySales(string custname, string custadd, string gcod, string pactcode, string usircode, string pactdesc, string udesc, string munit, string schdate,
            double tuamt, double suamt, double disamt, double disper, double usize, double sftpr)
        {

            this.custname = custname;
            this.custadd = custadd;
            this.gcod = gcod;
            this.pactcode = pactcode;
            this.usircode = usircode;
            this.pactdesc = pactdesc;
            this.udesc = udesc;
            this.munit = munit;
            this.schdate = schdate;
            this.tuamt = tuamt;
            this.suamt = suamt;
            this.disamt = disamt;
            this.disper = disper;
            this.usize = usize;
            this.sftpr = sftpr;



        }





    }

    public class EClassEmpInf
    {
        public string gdatat { get; set; }
        public string gdatad { get; set; }
        public EClassEmpInf() { }
        public EClassEmpInf(string gdatat, string gdatad)
        {

            this.gdatat = gdatat;
            this.gdatad = gdatad;
        }

    }

    [Serializable]
    public class EClassDayCollection
    {

        public string grp { get; set; }
        public string grpdesc { get; set; }
        public string mrno { get; set; }
        public string mrdate1 { get; set; }

        public string pactcode { get; set; }
        public string usircode { get; set; }

        public string pactdesc { get; set; }
        public string udesc { get; set; }
        public string custname { get; set; }
        public string chqdate { get; set; }

        public string chqno { get; set; }
        public string bankname { get; set; }
        public string bbranch { get; set; }
        public string repchqno { get; set; }
        public string recndt { get; set; }

        public string entrydat { get; set; }
        public double chqamt { get; set; }
        public double cashamt { get; set; }
        


        public EClassDayCollection() { }
        public EClassDayCollection(string grp, string grpdesc, string mrno, string mrdate1, string pactcode, string usircode, string pactdesc, string udesc, string custname, string chqdate,
            string chqno, string bankname, string bbranch, string repchqno, string recndt, string entrydat, double chqamt, double cashamt)
        {

            this.grp = grp;
            this.grpdesc = grpdesc;
            this.mrno = mrno;
            this.mrdate1 = mrdate1;
            this.pactcode = pactcode;
            this.usircode = usircode;
            this.pactdesc = pactdesc;
            this.udesc = udesc;
            this.custname = custname;
            this.chqdate = chqdate;
            this.chqno = chqno;
            this.bankname = bankname;
            this.bbranch = bbranch;
            this.repchqno = repchqno;
            this.recndt = recndt;
            this.entrydat = entrydat;
            this.chqamt = chqamt;
            this.cashamt = cashamt;



        }





    }
    
    [Serializable]
    public class EClassEmployeeMonEva
    {
        public string ymonid { get; set; }
        public string yearmon { get; set; }
        public double tamt1 { get; set; }
        public double tamt2 { get; set; }
        public double tamt3 { get; set; }
        public double tamt4 { get; set; }
        public double tamt5 { get; set; }
        public double tamt6 { get; set; }
        public double tamt7 { get; set; }
        public double tamt8 { get; set; }
        public double amt1 { get; set; }
        public double amt2 { get; set; }
        public double amt3 { get; set; }
        public double amt4 { get; set; }
        public double amt5 { get; set; }
        public double amt6 { get; set; }
        public double amt7 { get; set; }
        public double amt8 { get; set; }
        public double tper { get; set; }
        public double tmark { get; set; }
        public string gpa { get; set; }
        public double Target { get; set; }
        public double Actual { get; set; }
        public double avgmark { get; set; }

        public  EClassEmployeeMonEva() { }
        public EClassEmployeeMonEva(string ymonid, string yearmon, double tamt1, double tamt2, double tamt3, double tamt4, double tamt5, double tamt6, double tamt7, double tamt8, double amt1,
                double amt2, double amt3, double amt4, double amt5, double amt6, double amt7, double amt8, double tper, double tmark, string gpa, double Target, double Actual, double avgmark) 
        {
            this.ymonid = ymonid;
            this.yearmon = yearmon;
            this.tamt1 = tamt1;
            this.tamt2 = tamt2;
            this.tamt3 = tamt3;
            this.tamt4 = tamt4;
            this.tamt5 = tamt5;
            this.tamt6 = tamt6;
            this.tamt7 = tamt7;
            this.tamt8 = tamt8;
            this.amt1 = amt1;
            this.amt2 = amt2;
            this.amt3 = amt3;
            this.amt4 = amt4;
            this.amt5 = amt5;
            this.amt6 = amt6;
            this.amt7 = amt7;
            this.amt8 = amt8;
            this.tper = tper;
            this.tmark = tmark;
            this.gpa = gpa;
            this.Target = Target;
            this.Actual = Actual;
            this.avgmark = avgmark;
           
        
        }
    
    }


    [Serializable]
    public class EClassEmployeeMonEva02
    {
        public string ymonid { get; set; }
        public string yearmon { get; set; }
        public double tar { get; set; }
        public double cumtar { get; set; }
        public double act { get; set; }
        public double cumact { get; set; }
        public double tper { get; set; }
        public double tmark { get; set; }
        public string gpa { get; set; }
        public double Target { get; set; }
        public double Actual { get; set; }
      
     

        public EClassEmployeeMonEva02() { }
        public EClassEmployeeMonEva02(string ymonid, string yearmon, double tar, double cumtar, double act, double cumact, double tper, double tmark, string gpa, double Target, double Actual)
        {
            this.ymonid = ymonid;
            this.yearmon = yearmon;
            this.tar = tar;
            this.cumtar = cumtar;
            this.act = act;
            this.cumact = cumact;
            this.tper = tper;
            this.tmark = tmark;
            this.gpa = gpa;
            this.Target = Target;
            this.Actual = Actual;
           


        }


       


    }




    [Serializable]
    public class EClassEmployeeMonEvagen
    {
        public string ymonid { get; set; }
        public string yearmon { get; set; }
        public double tmark { get; set; }
        public double acmark { get; set; }
        public double Target { get; set; }
        public double Actual { get; set; }
        public double avgmark { get; set; }
        public string gpa { get; set; }
       



        public EClassEmployeeMonEvagen() { }
        public EClassEmployeeMonEvagen(string ymonid, string yearmon, double tmark, double acmark, double Target, double Actual, double avgmark , string gpa)
        {
            this.ymonid = ymonid;
            this.yearmon = yearmon;
            this.tmark = tmark;
            this.acmark = acmark;
            this.Target = Target;
            this.Actual = Actual;
            this.avgmark = avgmark;
            this.gpa = gpa;
            



        }





    }

     [Serializable]
    public class EClassEmpHistory
    {
        public string pactdesc { get; set; }
        public string actdesc { get; set; }
        public double duration { get; set; }
        public double aduration { get; set; }
        public double deloadv { get; set; }
     
        public string deloadvsign { get; set; }
        


        public EClassEmpHistory() { }
        public EClassEmpHistory(string pactdesc, string actdesc, double duration, double aduration, double deloadv, string deloadvsign)
        {
            this.pactdesc = pactdesc;
            this.actdesc = actdesc;
            this.duration = duration;
            this.aduration = aduration;
            this.deloadv = deloadv;
            this.deloadvsign = deloadvsign;
           



        }





    }


    [Serializable]
    public class EClassShowEmpData
    {
         public string actcode1 { get; set; }
        public string actdesc1 { get; set; }
        public string actcode { get; set; }
        public string actdesc { get; set; }
        public double wqty { get; set; }
        public double marks { get; set; }
        public double acqty { get; set; }

        public double acmarks { get; set; }
        public double ppercent { get; set; }


        public EClassShowEmpData(string actcode1, string actdesc1, string actcode, string actdesc, double wqty, double marks, double acqty, double acmarks, double ppercent)
        {

            this.actcode1 = actcode1;
            this.actdesc1 = actdesc1;
            this.actcode = actcode;
            this.actdesc = actdesc;

            this.wqty = wqty;
            this.marks = marks;
            this.acqty = acqty;
            this.acmarks = acmarks;
            this.ppercent = ppercent;
        }

    }

    [Serializable]
    public class EClassShowEval
    {

        public double actmark { get; set; }
        public double acpar { get; set; }
        public string grade { get; set; }


        public EClassShowEval(double actmark, double acpar, string grade)
        {

            this.actmark = actmark;
            this.acpar = acpar;
            this.grade = grade;
        }

    }
      
    [Serializable]
    //comcod, mkpigrp, kpigrp, kpidesc, stdtarget,stdkpival, kraval=0.00, achived=0.00, mkpidesc
    public class EclassKeyResult
    {

        public int slnum { get; set; }
        public string comcod { get; set; }
        public string mkpigrp { get; set; }
        public string kpigrp { get; set; }
        public string kpidesc { get; set; }
        public double stdtarget { get; set; }
        public double stdkpival { get; set; }
        public double kraval { get; set; }
        public double achived { get; set; }
        public string mkpidesc { get; set; }

        public EclassKeyResult()
        {

        }
    }

    [Serializable]

    public class EclassKRANOte
    {
        public string code { get; set; }
        public string codeval { get; set; }
        public EclassKRANOte()
        {

        }
    }

}
