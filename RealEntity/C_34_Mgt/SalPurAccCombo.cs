using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_34_Mgt
{
    public class  SalPurAccCombo
    {
        public string yearmon {set;get;}
        public string yearmon1 { set; get; }
        public double ttlsalamt { set; get; }
        public double tpayamt { set; get; }
        public double tllpuramt { set; get; }
        public double collamt { set; get; }
        public double dram { set; get; }
        public double cram { set; get; }

        public double taramt { set; get; }
        public double examt { set; get; }



    }
    //a.comcod, a.reqno, a.reqdat, a.pactcode, a.rsircode, a.spcfcod, a.mrfno, a.reqnar, a.proamt, a.faprvbyid, a.faprvdat,  a.appamt, a.postedbyid, a.postseson, 
    //--a.postrmid, a.posteddat,
    //      a.aprvbyid, a.aprvdat, a.aprvtrmid, a.aprvseson, a.qty, a.paytype,  a.PAYTO, a.ppdamt,
 //   a.supcode, a.adjcod, a.termacon, a.payofmod, ssirdesc=isnull(b.SIRDESC,''), 
    //      projadds=isnull(c.gdatat, '') 
    [Serializable]
    public class GenBillReq
    {
        public string comcod { get; set; }
        public string reqno { get; set; }
        public DateTime reqdat { get; set; }
        public string pactcode { get; set; }
        public string rsircode { get; set; }
        public string spcfcod { get; set; }
        public string mrfno { get; set; }
        public string reqnar { get; set; }
        public double proamt { get; set; }
        public string faprvbyid { get; set; }
        public DateTime faprvdat { get; set; }
        public double appamt { get; set; }
        public string postedbyid { get; set; }
        public string postseson { get; set; }
        public string postrmid { get; set; }
        public DateTime posteddat { get; set; }
        public string aprvbyid { get; set; }
        public DateTime aprvdat { get; set; }
        public string aprvtrmid { get; set; }
        public string aprvseson { get; set; }
        public double qty { get; set; }
        public string paytype { get; set; }
        public string PAYTO { get; set; }
        public double ppdamt { get; set; }
        public string supcode { get; set; }
        public string adjcod { get; set; }
        public string termacon { get; set; }
        public string payofmod { get; set; }
        public string ssirdesc { get; set; }
        public string projadds { get; set; }
        public string sunit { get; set; }
        public double rate { get; set; }
        public string actdesc { get; set; }
        public GenBillReq() { }
    }

//    a.comcod, a.supcode,
//conadd=isnull(b.gdatat,''), atten=isnull(b.gdatat, ''), mobile=isnull(d.gdatat, ''), email=isnull(e.gdatat,''),
// conperson=isnull(h.gdatat,''), natureofwork=isnull(j.gdatat,''), firmname=isnull(i.gdatat,'')
    [Serializable]
    public class GenBillSupdesc
    {
        public string comcod { get; set; }
        public string supcode { get; set; }
        public string conadd { get; set; }
        public string atten { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string conperson { get; set; }
        public string natureofwork { get; set; }
        public string firmname { get; set; }
        public GenBillSupdesc() { }
    }

    



}
