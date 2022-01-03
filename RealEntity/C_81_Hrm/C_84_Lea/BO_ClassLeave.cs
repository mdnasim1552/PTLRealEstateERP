using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_81_Hrm.C_84_Lea
{
    public class BO_ClassLeave
    {
        [Serializable]
        public class LvApproval
        {
            public string comcod { get; set; }
            public string ltrnid { get; set; }
            public string lvtype { get; set; }
          
            public string gcod { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string idcard { get; set; }
            public DateTime strtdat { get; set; }
            public DateTime enddat { get; set; }
            public DateTime aplydat { get; set; }
            public string desig { get; set; }
            public string deptanme { get; set; }
            public double duration { get; set; }
            public bool forward { get; set; }

        }
        [Serializable]
        public class Empperformance
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string deptid { get; set; }
            public string secid { get; set; }
            public string desigid { get; set; }
            public string empname { get; set; }
            public string position { get; set; }
            public string deptname { get; set; }
            public string section { get; set; }
            public string dessig { get; set; }
            public string secadesig { get; set; }
            public double mark1 { get; set; }
            public double mark2 { get; set; }
            public double mark3 { get; set; }
            public double mark4 { get; set; }
            public double mark5 { get; set; }
            public double mark6 { get; set; }
            public double mark7 { get; set; }
            public double mark8 { get; set; }
            public double mark9 { get; set; }
            public double mark10 { get; set; }
            public double mark11 { get; set; }
            public double mark12 { get; set; }
            public double avgmark { get; set; }
            public Empperformance() { }
        }
        [Serializable]
        public class BonusSheet
        {
            public string comcod { get; set; }
            public string idcard { get; set; }
            public string section { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string refno { get; set; }
            public string empid { get; set; }
            public double bsal { get; set; }
            public double hrent { get; set; }
            public double cven { get; set; }
            public double mallow { get; set; }
            public double arsal { get; set; }
            public double oth { get; set; }
            public double pfund { get; set; }
            public double itax { get; set; }
            public double adv { get; set; }
            public double acat { get; set; }
            public double othded { get; set; }
            public double gssal { get; set; }
            public double tallow { get; set; }
            public double tdeduc { get; set; }
            public double perbon { get; set; }
            public string bankgrp { get; set; }
            public double bonamt { get; set; }
            public double bankamt { get; set; }
            public double bankamt2 { get; set; }
            public double cashamt { get; set; }
            public string bankamta { get; set; }
            public string cashamta { get; set; }
            public DateTime joindate { get; set; }
            public double duration { get; set; }
            public string refdesc { get; set; }
            public string sectionname { get; set; }
            public BonusSheet() { }
        }
        
        [Serializable]
        public class EMpEvaluationn
        {
            public string mgcod { get; set; }
            public string gcod { get; set; }
            public string sgcod1 { get; set; }
            public string sgcod2 { get; set; }
            public string sgcod3 { get; set; }
            public string sgcod4 { get; set; }
            public string sgcod5 { get; set; }
            public string sgval1 { get; set; }
            public string sgval2 { get; set; }
            public string sgval3 { get; set; }
            public string sgval4 { get; set; }
            public string sgval5 { get; set; }
            public string mgdesc { get; set; }
            public string gdesc { get; set; }
            public string dgdesc { get; set; }
            public string sgdesc1 { get; set; }
            public string sgdesc2 { get; set; }
            public string sgdesc3 { get; set; }
            public string sgdesc4 { get; set; }
            public string sgdesc5 { get; set; }
            public  EMpEvaluationn(){}
        }

        [Serializable]
         public class EmpLeaveStatus
        {
            public int rowid { get; set; }
            public string comcod { get; set; }
            public string secid { get; set; }
            public string desigid { get; set; }
            public string empid { get; set; }
            public string section { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string idcardno { get; set; }
            public string joindate { get; set; }
            public double gssal { get; set; }
            public string gcod { get; set; }
            public string gdesc { get; set; }
            public double opleave { get; set; }
            public double enleave { get; set; }
            public double enjleave { get; set; }
            public double balleave { get; set; }
            public string lstrtdat { get; set; }
            public string descrip { get; set; }
       
            public EmpLeaveStatus() { }
        }


        [Serializable]
        public class RptEmpLeavStatus
        {
           
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string comcod { get; set; }
            public string empid { get; set; }
            public string gcod { get; set; }
            public double opleave { get; set; }
            public double enleave { get; set; }
            public double enjleave { get; set; }
            public double balleave { get; set; }
            public string descrip { get; set; }
            public string aplydat { get; set; }
            public string strtdat { get; set; }
            public string enddat { get; set; }
            public double lvday { get; set; }
            public string lreason { get; set; }
            public string gdesc { get; set; }
            public RptEmpLeavStatus(){}
        }

         
        [Serializable]
        public class RptEmpInfoData
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string rowid { get; set; }
            public string company { get; set; }
            public string secid { get; set; }
            public string desigid { get; set; }
            public string idcardno { get; set; }
            public string joindate { get; set; }
            public string condate { get; set; }
            public string conperiod { get; set; }
            public string empname { get; set; }
            public string desig { get; set; }
            public string companyname { get; set; }
            public string section { get; set; }
            public string lstrtdat { get; set; }
            public string serlength { get; set; }
            public double gssal { get; set; }
            public double today { get; set; }
            public RptEmpInfoData() { }
 
        }
        [Serializable]
        public class RptMonWiseEmpLeave
        {
 
            public string comcod { get; set; }
            public string company { get; set; }
            public string empid { get; set; }
            public string secid { get; set; }
            public string desigid { get; set; }
            public string idcardno { get; set; }
            public string joindate { get; set; }
            public string empname { get; set; }
            public string desig { get; set; }
            public string section { get; set; }
            public string desigadept { get; set; }
            public string companyname { get; set; }
            public double enleave { get; set; }
            public double cleave { get; set; }
            public double balleave { get; set; }
            public double sleave { get; set; }
            public double lv1 { get; set; }
            public double lv2 { get; set; }
            public double lv3 { get; set; }
            public double lv4 { get; set; }
            public double lv5 { get; set; }
            public double lv6 { get; set; }
            public double lv7 { get; set; }
            public double lv8 { get; set; }
            public double lv9 { get; set; }
            public double lv10 { get; set; }
            public double lv11 { get; set; }
            public double lv12 { get; set; }
            public double lvavailed { get; set; }

            public RptMonWiseEmpLeave() { }
        }


        [Serializable]
        public class BonusSheet01
        {
            public string comcod { get; set; }
            public string idcard { get; set; }
            public string section { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string refno { get; set; }
            public string empid { get; set; }
            public double bsal { get; set; }
            public double hrent { get; set; }
            public double cven { get; set; }
            public double mallow { get; set; }
            public double arsal { get; set; }
            public double oth { get; set; }
            public double pfund { get; set; }
            public double itax { get; set; }
            public double adv { get; set; }
            public double othded { get; set; }
            public double gssal { get; set; }
            public double tallow { get; set; }
            public double tdeduc { get; set; }
            public double perbon { get; set; }
            public string bankgrp { get; set; }
            public double bonamt { get; set; }
            public double bankamt { get; set; }
            public double bankamt2 { get; set; }
            public double cashamt { get; set; }
            public string bankamta { get; set; }
            public string cashamta { get; set; }
            public DateTime joindate { get; set; }
            public double duration { get; set; }
            public string refdesc { get; set; }
            public string sectionname { get; set; }
            public string rmrks { get; set; }
            public BonusSheet01() { }
        }
        //a.comcod,a.idcard, a.section, a.desigid, a.desig, a.empname,a.refno, a.empid, a.bsal, a.hrent, a.cven, a.mallow, a.arsal, a.oth, a.pfund,
	//a.itax, a.adv,  a.othded, a.gssal, a.tallow, a.tdeduc, a.perbon, a.bonamt, bankgrp, a.bankamt, a.bankamt2, a.cashamt, a.bankamta, a.cashamta, 
	//a.joindate, a.duration, refdesc=b.sirdesc,  sectionname=c.sirdesc ,a.rmrks

        [Serializable]
        public class BonusSheetPEB
        {
            public string comcod { get; set; }
            public string idcard { get; set; }
            public string section { get; set; }
            public string desigid { get; set; }
            public string desig { get; set; }
            public string empname { get; set; }
            public string refno { get; set; }
            public string empid { get; set; }
            public double bsal { get; set; }
            public double hrent { get; set; }
            public double cven { get; set; }
            public double mallow { get; set; }
            public double arsal { get; set; }
            public double oth { get; set; }
            public double pfund { get; set; }
            public double itax { get; set; }
            public double adv { get; set; }
            public double othded { get; set; }
            public double gssal { get; set; }
            public double tallow { get; set; }
            public double tdeduc { get; set; }
            public double perbon { get; set; }
            public string bankgrp { get; set; }
            public double bonamt { get; set; }
            public double bankamt { get; set; }
            public double bankamt2 { get; set; }
            public double cashamt { get; set; }
            public string bankamta { get; set; }
            public string cashamta { get; set; }
            public DateTime joindate { get; set; }
            public double duration { get; set; }
            public string refdesc { get; set; }
            public string sectionname { get; set; }
            public string slength { get; set; }
            public DateTime cutdate { get; set; }
            public string bankacno { get; set; }
            public string rmrks { get; set; }
            public string rowid { get; set; }


            public BonusSheetPEB() { }
        }

        [Serializable]
        public class EmpLeaveAPP
        {
            public string gcod { get; set; }
            public string gdesc { get; set; }
            public double entitle { get; set; }
            public double permonth { get; set; }
            public double pbal { get; set; }
            public double adjfleave { get; set; }
            public double ltaken { get; set; }
            public double balleave { get; set; }
            public double tltakreq { get; set; }
            public DateTime lenjoydt1 { get; set; }
            public DateTime lenjoydt2 { get; set; }
            public double lenjoyday { get; set; }
            public DateTime appdate { get; set; }
            public double appday { get; set; }
            public DateTime applydate { get; set; }
            public double applyday { get; set; }
            public DateTime lrstrtdat { get; set; }
            public DateTime lrentdat { get; set; }
            public EmpLeaveAPP() { }
        }


        [Serializable]
        public class EmpLeaveRecord
        {
            public string empid { get; set; }
            public string empnar { get; set; }
            public string deptid { get; set; }
            public string secname { get; set; }
            public string desig { get; set; }
            public string idcardno { get; set; }
            public string joindate { get; set; }
            public string monjoin { get; set; }
            public string servday { get; set; }
            public double oplv { get; set; }
            public string ernid { get; set; }
            public double ernleave { get; set; }
            public string csid { get; set; }
            public double csleave { get; set; }
            public string skid { get; set; }
            public double skleave { get; set; }
            public string cleavenj { get; set; }
            public string sckenjoy { get; set; }
            public string ernenjoy { get; set; }
            public double balernlv { get; set; }
            public double balclv { get; set; }
            public double balsklv { get; set; }
            public string empname { get; set; }
            public EmpLeaveRecord() { }
        }
    }
}
