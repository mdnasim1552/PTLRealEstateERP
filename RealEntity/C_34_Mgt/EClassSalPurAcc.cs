using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_34_Mgt
{
    [Serializable]
    public class  EClassSalPurAcc
    {
        public string yearmon {set;get;}
        public string yearmon1 { set; get; }
        public double ttlsalamt { set; get; }
        public double collamt { set; get; }
        public double ttlpuramt { set; get; }
        public double tpayamt { set; get; }
   
   
        public double dram { set; get; }
        public double cram { set; get; }

        public EClassSalPurAcc()
        {
        }


        public EClassSalPurAcc(string yearmon, string yearmon1, double ttlsalamt, double collamt, double ttlpuramt, double tpayamt, double dram, double cram)
            {
                this.yearmon = yearmon;
                this.yearmon1 = yearmon1;
                this.ttlsalamt = ttlsalamt;
                this.collamt = collamt;
                this.collamt = collamt;
                this.ttlpuramt = ttlpuramt;
                this.tpayamt = tpayamt;
                this.dram = dram;
                this.cram = cram;
              
            }

        public class RequisationAdjust
        {
        }
    }
    [Serializable]
    public class EclassGroupChat
    {
        public string chatno { get; set; }
        public string chtname { get; set; }
        public string concern { get; set; }
        public string postedname { get; set; }
        public string postedbyid { get; set; }
        public string posteddat { get; set; }
        public string actcode { get; set; }
        public string actdesc { get; set; }
        public string probledat { get; set; }
        public string taskcod { get; set; }
        public string taskname { get; set; }
        public string asinuser { get; set; }
    }
    [Serializable]
    public class EclassChatMSG
    {
        public string chatno { get; set; }
        public string message { get; set; }
        public string postedname { get; set; }
        public string postedbyid { get; set; }
        public string posteddat { get; set; }
        public string mestatus { get; set; }
        public string pday { get; set; }
        public string ptime { get; set; }
        public Boolean files { get; set; }
    }

    [Serializable]
    public class Grouptaskchat
    {
        public string comcod { get; set; }
        public string chatno { get; set; }
        public string userid { get; set; }
        public string postedname { get; set; }
        public string actcode { get; set; }
        public string actdesc { get; set; }
        public string taskcod { get; set; }
        public string fmessage { get; set; }
        public string cstatus { get; set; }
        public string taskname { get; set; }
        public DateTime probledat { get; set; }
        public string clstatus { get; set; }
        public string chtname { get; set; }
        public string postedbyid { get; set; }
        public DateTime posteddat { get; set; }
        public Grouptaskchat() { }
    }

    [Serializable]
    public class Userinfo
    {
        
        public string usrid { get; set; }
        public string usrsname { get; set; }
        public string usrname { get; set; }
        public string usrdesig { get; set; }
        public string usrrmrk { get; set; }
        public string roledesc { get; set; }

        public Userinfo() { }
    }

    [Serializable]
    public class GetCompany
    {
        public string comcod { get; set; }
        public string comname { get; set; }
        public string usrid { get; set; }
        public string usrsname { get; set; }
        public string usrname { get; set; }
        public string usrdesig { get; set; }
        public string usrpass { get; set; }
        public string usrrmrk { get; set; }
        public string empid { get; set; }
        public string urole { get; set; }
        public string usractive { get; set; }
    
        public GetCompany()
        {

        }
        public GetCompany(string comcod, string comname, string usrid, string usrsname, string usrname, string usrdesig, string usrpass, string usrrmrk, string empid, string urole, string usractive)
        {
            this.comcod = comcod;
            this.comname = comname;
            this.usrid = usrid;
            this.usrsname = usrsname;
            this.usrname = usrname;
            this.usrdesig = usrdesig;
            this.usrpass = usrpass;
            this.usrrmrk = usrrmrk;
            this.empid = empid;
            this.urole = urole;
            this.usractive = usractive;

        }
    }
    [Serializable]

    ///pactcode, pactdesc, rsircode,spcfcod, sirdesc, bgdamt, trnamt, balamt, proamt, appamt, qty, rate=iif(qty=0.00,0.00,proamt/qty), ppdamt ,billno,approval

    public class EClassOtherReq
    {
        public string pactcode { get; set; }
        public string pactdesc { get; set; }
        public string rsircode { get; set; }
        public string spcfcod { get; set; }
        public string sirdesc { get; set; }
        public double bgdamt { get; set; }
        public double trnamt { get; set; }
        public double balamt { get; set; }
        public double proamt { get; set; }
        public double appamt { get; set; }
        public double qty { get; set; }
        public double rate { get; set; }
        public double ppdamt { get; set; }
        public string billno { get; set; }
        public string approval { get; set; }

       
    }

    


}
