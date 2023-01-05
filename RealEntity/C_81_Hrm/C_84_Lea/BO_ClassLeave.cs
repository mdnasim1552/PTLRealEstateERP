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
            
            public string id { get; set; }
            public string empuserid { get; set; }
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
            public bool ishalfday { get; set; }
            

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
            public double bank1 { get; set; }
            public double bank2 { get; set; }
            public double bank3 { get; set; }
            public double bank4 { get; set; }
            public double cashamt { get; set; }
            public string bankamta { get; set; }
            public string cashamta { get; set; }
            public DateTime joindate { get; set; }
            public double duration { get; set; }
            public double spbonamt { get; set; }
            public double tbamt { get; set; }
            public string refdesc { get; set; }
            public string sectionname { get; set; }
            public string slength { get; set; }
            public string categorydesc { get; set; }
            public string rmrks { get; set; }
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
            public string unit { get; set; }
            public string deptid { get; set; }
            public string deptdesc { get; set; }


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
        public class RptLeaveRegister
        {

            public string empname { get; set; }
            public string idcardno { get; set; }
            public string desigadept { get; set; }
            public string joindate { get; set; }
            public string section { get; set; }
            public string secid { get; set; }
            public double serlength { get; set; }
            public double cleave { get; set; }
            public double sleave { get; set; }
            public double enleave { get; set; }
            public double upachivclv { get; set; }
            public double upachivslv { get; set; }
            public double upachivelv { get; set; }
            public double avclv { get; set; }
            public double avslv { get; set; }
            public double avelv { get; set; }
            public double upbclv { get; set; }
            public double upbslv { get; set; }
            public double upbelv { get; set; }
            public double balclv { get; set; }
            public double balslv { get; set; }
            public double balelv { get; set; }
            public double balstlv { get; set; }
           
            public RptLeaveRegister() { 

            }
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
            public double boncomi { get; set; }
            public double bonwcomi { get; set; }
            public string nagadno { get; set; } 


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

        [Serializable]
        public class YearlyLeaveRecord
        {
            public string comcod { get; set; }
            public string companycode { get; set; }
            public string departcode { get; set; }
            public string secid { get; set; }
        
            public string empid { get; set; }
            public string desigid { get; set; }
            public double opnbal { get; set; }
            public double earnlv { get; set; }
            public double enjoylv { get; set; }
            public double carrfor { get; set; }
            public double ballv { get; set; }
      
            public string desig { get; set; }

            public string idcardno { get; set; }
            public string joindat { get; set; }
            public string empname { get; set; }
            public string companyname { get; set; }
          
            public string deptdesc { get; set; }
          
            public string sectionname { get; set; }
            public YearlyLeaveRecord() { }
        }
        [Serializable]
        public class yearlyholiday
        {
            //comcod,wkdate,wkdate2,reason,dstatus, diff
           public  string comcod { get; set; }
            public DateTime wkdate { get; set; }
            public DateTime wkdate2 { get; set; }
            public string reason { get; set; }
            public string dstatus { get; set; }
            public int diff { get; set; }
            public yearlyholiday() { }
        }


        [Serializable]
        public class LeaveRule
        {
          public  string empid { get; set; }
          public string lvname { get; set; }
          public double leave { get; set; }
          public string yearid { get; set; }
          public LeaveRule() { }
        }


        [Serializable]
        public class currentLeaveInfo
        {
            //comcod,empid,gcod,strtdat,enddat,aplydat,aprdat,leaveday ,lvname,lvblnc
            public string comcod { get; set; }

            public string empid { get; set; }
            public string gcod { get; set; }
            public double eleave { get; set; }
            public double cleave { get; set; }
            public double sleave { get; set; }
            public double stleave { get; set; }
            public double bleave { get; set; }
            public double baleleave { get; set; }
            public double balcleave { get; set; }
            public double balsleave { get; set; }
            public double balstleave { get; set; }

            public DateTime aplydat { get; set; }
            public DateTime strtdat { get; set; }
            public DateTime enddat { get; set; }
            public DateTime aprdat { get; set; }
            public string lreason { get; set; }
            public string supname { get; set; }
            public string dptuser { get; set; }
            public string lvname { get; set; }


            public currentLeaveInfo() { }
        }

        [Serializable]
        public class prevtLeaveInfo
        {
            //comcod,empid,gcod,strtdat,enddat,aplydat,aprdat,leaveday ,lvname,lvblnc
            public string comcod { get; set; }
            public string gcod { get; set; }
            public string empid { get; set; }
            public string prevlvname { get; set; }
            public double prevleaveday { get; set; }
            public double prevlvblnc { get; set; }
            public DateTime prevstrtdat { get; set; }
            public DateTime prevenddat { get; set; }
            public DateTime prevaprdat { get; set; }
            public DateTime prevaplydat { get; set; }
            public prevtLeaveInfo() { }
        }

        [Serializable]
        public class birthdayDate
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string bddate { get; set; }
            public string mobile { get; set; }
            public string idcard { get; set; }
            public string email { get; set; }
            public string desig { get; set; }
            public string dept { get; set; }
            public string gdatad { get; set; }

            public birthdayDate() { }
        }


        [Serializable]
        public class applytimeoff
        {
            public string comcod { get; set; }
            public string empid { get; set; }
            public string dayid { get; set; }
            public DateTime reqdate { get; set; }
            public string outtime { get; set; }
            public string intime { get; set; }
            public string usetime { get; set; }
            public string remarks { get; set; }
            public string reqtype { get; set; }
            public string reqstatus { get; set; }
            public string useTimeMin { get; set; }
            public string remaintime { get; set; }
            public string dptuser { get; set; }
            public string supname { get; set; }
            public applytimeoff() { }
        }


        [Serializable]
        public class EmpBasicInf
        {
            public string desig { get; set; }
            public string dept { get; set; }
            public string cdate { get; set; }
            public string doj { get; set; }

            public EmpBasicInf() { }
        }

    }
}
