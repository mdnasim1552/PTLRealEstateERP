using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_99_AllInterface
{
    [Serializable]
    public class SalesJournal
    {
        public string actdesc { get; set; }
        public string usirdesc { get; set; }
        public string munit { get; set; }
        public double unitsize { get; set; }
        public double unitamt { get; set; }
        public DateTime schdate { get; set; }

    }

    [Serializable]
    public class RptCkhDeposit
    {
        public string pactdesc { get; set; }
        public string udesc { get; set; }
        public string refno { get; set; }
        public string mrno { get; set; }
        public string bankname { get; set; }
        public string chqno { get; set; }
        public DateTime paydate { get; set; }
        public double paidamt { get; set; }
        public string usrname { get; set; }

    }
    [Serializable]
    public class RptUpdateCol
    {
        public string actdesc { get; set; }
        public string udesc { get; set; }
        public string mrno { get; set; }
        public string bankname { get; set; }
        public string chqno { get; set; }
        public DateTime paydate { get; set; }
        public double cramt { get; set; }
    }

    [Serializable]
    public class RptUpdatePur
    {
        public string actdesc { get; set; }
        public string billno1 { get; set; }
        public string ssirdesc { get; set; }
        public DateTime billdat { get; set; }
        public double billamt { get; set; }
    }


    [Serializable]
    public class RptConBillUp
    {
        public string actdesc { get; set; }
        public string billno1 { get; set; }
        public string billno { get; set; }
        public string csirdesc { get; set; }
        public string cbillref { get; set; }
        public DateTime billdate { get; set; }
        public double billamt { get; set; }
    }


    [Serializable]
    public class RptUpdateMatTrans
    {
        public string actdesc { get; set; }
        public string tpactdesc { get; set; }
        public string trnno1 { get; set; }
        public string trnno { get; set; }
        public DateTime trnsdate { get; set; }
        public double trsnamt { get; set; }
    }


    [Serializable]
    public class Rptcrateappanorder
    {
        public string pactdesc { get; set; }
        public DateTime reqdat1 { get; set; }
        public string reqno1 { get; set; }
        public string mrfno { get; set; }
        public double itemcount { get; set; }
        public double apamt { get; set; }
    }


    [Serializable]
    public class RptReqSts
    {
        public string pactdesc { get; set; }
        public DateTime reqdat1 { get; set; }
        public string reqno1 { get; set; }
        public string mrfno { get; set; }
        public double itemcount { get; set; }
        public double apamt { get; set; }
        public string cstatus { get; set; }
    }

    [Serializable]
    public class RptPurOrder
    {
        public string pactdesc { get; set; }
        public string ssirdesc { get; set; }
        public DateTime aprovdat1 { get; set; }
        public string aprovno1 { get; set; }
        public string reqno1 { get; set; }
        public string mrfno { get; set; }
        public double itemcount { get; set; }
        public double woamt { get; set; }

    }

    [Serializable]
    public class RptCashRcv
    {
        public string pactdesc { get; set; }
        public string ssirdesc { get; set; }
        public DateTime orderdat1 { get; set; }
        public string orderno1 { get; set; }
        public string reqno1 { get; set; }
        public string mrfno { get; set; }
        public double itemcount { get; set; }
        public double recvamt { get; set; }
        public double mrramt { get; set; }
        public double balamt { get; set; }

    }
    [Serializable]
    public class RptBillCon
    {
        public string pactdesc { get; set; }
        public string ssirdesc { get; set; }
        public DateTime mrrdat1 { get; set; }
        public string mrrno1 { get; set; }
        public string orderno1 { get; set; }
        public string reqno1 { get; set; }
        public string mrfno { get; set; }
        public double itemcount { get; set; }
        public double mrramt { get; set; }

    }
    //comcod, pactcode,  usircode, custname,  udesc, predues, curdues, receivable, dueins, pactdesc,mrno, recdate, recndt, recamt, netdues
    [Serializable]
    public class Netdues01
    {
        // Iqbal Nayan
        public string comcod { get; set; }
        public string pactcode { get; set; }
        public string usircode { get; set; }
        public string custname { get; set; }
        public string udesc { get; set; }
        public double predues { get; set; }
        public double curdues { get; set; }
        public double receivable { get; set; }
        public double dueins { get; set; }
        public string mrno { get; set; }
        public string pactdesc { get; set; }
        public string recdate { get; set; }
        public string recndt { get; set; }
        public double recamt { get; set; }
        public double netdues { get; set; }
        public Netdues01() { }
    }

    [Serializable]
    public class AllDues
    {
        // robi
        public string comcod { get; set; }
        public string pactcode { get; set; }
        public string usircode { get; set; }
        public string custname { get; set; }
        public string forwordby { get; set; }
        public string udesc { get; set; }
        public double predues { get; set; }
        public double curdues { get; set; }
        public double receivable { get; set; }
        public double dueins { get; set; }
        public string mrno { get; set; }
        public string pactdesc { get; set; }
        public string recdate { get; set; }
        public string recndt { get; set; }
        public double recamt { get; set; }
        public double netdues { get; set; }
        public AllDues() { }
    }

    //comcod, reqno, reqno1, pactcode, rsircode,mrfno, reqdat, reqdat1,rescount,reqamt, appamt, paidamt,balamt, pactdesc,sirdesc,supdesc,  userid,  suserid, 
    //aprvbyid, frecid, secrecid, threcid, faprvbyid,faprvdat, rusername, supcode,aprvname,faprvname,cstatus, bundno
    public class GbFinalApproval
    {
        public string comcod { get; set; }
        public string reqno { get; set; }
        public string reqno1 { get; set; }
        public string pactcode { get; set; }
        public string rsircode { get; set; }
        public string mrfno { get; set; }
        public DateTime reqdat { get; set; }
        public string reqdat1 { get; set; }
        public double rescount { get; set; }
        public double reqamt { get; set; }
        public double appamt { get; set; }
        public double paidamt { get; set; }
        public double balamt { get; set; }

        public string pactdesc { get; set; }
        public string sirdesc { get; set; }
        public string supdesc { get; set; }
        public string userid { get; set; }
        public string suserid { get; set; }
        public string aprvbyid { get; set; }
        public string frecid { get; set; }
        public string secrecid { get; set; }
        public string threcid { get; set; }
        public string faprvbyid { get; set; }
        public DateTime faprvdat { get; set; }


        public GbFinalApproval() { }
    }

}
