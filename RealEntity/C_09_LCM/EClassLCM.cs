using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEntity.C_09_LCM
{

    public class EClassLCCode
    {
        
        public string actcode { get; set; }
        public string actdesc { get; set; }
        public EClassLCCode() { }
        public EClassLCCode(string actcode, string actdesc)
        {

            this.actcode = actcode;
            this.actdesc = actdesc;
        }

    }

    
    public class EClassLCCosting
    {
        public string grp { get; set; }
        public string rescode { get; set; }
        public string resdesc { get; set; }
        public string unit { get; set; }
        public double qty { get; set; }
        public double fcrate { get; set; }
        public double fcamt { get; set; }
        public double bdamt { get; set; }
        public double tparcent { get; set; }
        public string lcdate { get; set; }
        public string bankname { get; set; }
        public string currency { get; set; }
        public string expdate { get; set; }
        public double conrate { get; set; }
        public string supname { get; set; }

        public EClassLCCosting() { }
        public EClassLCCosting(string grp, string rescode, string resdesc, string unit, double qty, double fcrate, double fcamt, double bdamt, double tparcent, string lcdate, string bankname,
                string currency, string expdate, double conrate, string supname)
        {
            this.grp = grp;
            this.rescode = rescode;
            this.resdesc = resdesc;
            this.unit = unit;
            this.qty = qty;
            this.fcrate = fcrate;
            this.fcamt = fcamt;
            this.bdamt = bdamt;
            this.tparcent = tparcent;
            this.lcdate = lcdate;
            this.bankname = bankname;
            this.currency = currency;
            this.expdate = expdate;
            this.conrate = conrate;
            this.supname = supname;
        }

    }

     [Serializable]
    public class EClassLCStatus
    {
        public string actcode { get; set; }
        public string actdesc { get; set; }
        public string rescod { get; set; }
        public string resdesc { get; set; }
        public double ordrqty { get; set; }
        public double rate { get; set; }
        public double lcamt { get; set; }
        public string lcdate { get; set; }
        public string shipdate { get; set; }
        public string shipardate { get; set; }
        public string deldate { get; set; }
        public string expdate { get; set; }
        public string rcvdate { get; set; }
        public string lcstatus { get; set; }
        public EClassLCStatus() { }
        public EClassLCStatus(string actcode, string actdesc, string rescod, string resdesc, double ordrqty, double rate, double lcamt, string lcdate, string shipdate, 
                string shipardate, string deldate, string expdate, string rcvdate, string lcstatus)
        {
            this.actcode = actcode;
            this.actdesc = actdesc;
            this.rescod = rescod;
            this.resdesc = resdesc;
            this.ordrqty = ordrqty;
            this.rate = rate;
            this.lcamt = lcamt;
            this.shipdate = shipdate;
            this.lcdate = lcdate;
            this.shipardate = shipardate;
            this.deldate = deldate;
            this.expdate = expdate;
            this.rcvdate = rcvdate;
            this.lcstatus = lcstatus;
        }

    }

    [Serializable]
    public class EClassExcelData
    {

      
        public string LC_Number { get; set; }
        public string Product_Id { get; set; }
        public string Pack_No { get; set; }
        public string M_IMEI { get; set; }
        public string S_IMEI { get; set; }
        public string Serial_No { get; set; }
        public string Color { get; set; }
        
        public EClassExcelData() { }
        public EClassExcelData(string LC_Number, string Product_Id, string Pack_No, string M_IMEI, string S_IMEI, string Serial_No, string Color)
        {
            this.LC_Number = LC_Number;
            this.Product_Id = Product_Id;
            this.Pack_No = Pack_No;
            this.M_IMEI = M_IMEI;
            this.S_IMEI = S_IMEI;
            this.Serial_No = Serial_No;
            this.Color = Color;
          
        }

    }

     [Serializable]
    public class EClassLCVari
    {
        public string actcode { get; set; }
        public string rescod { get; set; }
        public string resdesc { get; set; }
        public string unit { get; set; }

        public double ordrqty { get; set; }
        public double brate { get; set; }
        public double bamt { get; set; }
        public double rcvqty { get; set; }
        public double rcvamt { get; set; }
        public double trate { get; set; }
        public double varqty { get; set; }
        public EClassLCVari() { }
        public EClassLCVari(string actcode, string rescod, string resdesc, string unit, double ordrqty, double brate, double bamt, double rcvqty, double rcvamt,
                double trate, double varqty)
        {
            this.actcode = actcode;
            this.rescod = rescod;
            this.resdesc = resdesc;
            this.unit=unit;
            this.ordrqty = ordrqty;
            this.brate = brate;
            this.bamt = bamt;
            this.rcvqty = rcvqty;
            this.rcvamt = rcvamt;
            this.trate = trate;
            this.varqty = varqty;
        }

    }

     [Serializable]
     public class EClassLCRcvCons
     {
         public string storid { get; set; }
         public string stordesc { get; set; }
         public string actcode { get; set; }
         public string actdesc { get; set; }
         public string grrno { get; set; }
         public string grrno1 { get; set; }
         public string rescode { get; set; }
         public string resdesc { get; set; }
         public string unit { get; set; }
         public double rcvqty { get; set; }
         public double rcvamt { get; set; }
         public double trate { get; set; }
         public string rcvdate { get; set; }
         public string vounum { get; set; }
         public string rstatus { get; set; }
         public string rstatus1 { get; set; }//
         public EClassLCRcvCons() { }
         public EClassLCRcvCons(string storid, string stordesc, string actcode, string actdesc, string grrno, string grrno1, string rescode, string resdesc,
             string unit, double rcvqty, double rcvamt, double trate, string rcvdate, string vounum, string rstatus, string rstatus1)
         {
             this.storid = storid;
             this.stordesc = stordesc;
             this.actcode = actcode;
             this.actdesc = actdesc;
             this.grrno = grrno;
             this.grrno1 = grrno1;
             this.rescode = rescode;
             this.resdesc = resdesc;
             this.unit = unit;
             this.rcvqty = rcvqty;
             this.rcvamt = rcvamt;
             this.trate = trate;
             this.rcvdate = rcvdate;
             this.vounum = vounum;
             this.rstatus = rstatus;
             this.rstatus1 = rstatus1;
         }

     }

    //a.comcod,a.model,a.modelnam,grnno,a.rcvdate,a.openddate,a.lactcode,a.sactcode,a.rqty,fob=b.fob,a.invvalu,a.advance,a.cduty,
    //            a.vat,a.ait, a.scm,a.freight,a.insurance,a.others,a.total,a.lcost
   
     //comcod	model rcvdate	modelnam	actcode	openddate  // rqty  ,   fob   ,invvalu  ,  advance   ,  cduty,   vat,  ait,   scm  ,freight,  insurance,  others  ,total,   lcost
    [Serializable]
     public class LCReceived01
     {
        public string comcod { get; set; }
        public string model { get; set; }
        public string grnno { get; set; }
        public DateTime rcvdate { get; set; }
        public string modelnam { get; set; }
        public string lactcode { get; set; }
        public string lcname { get; set; }

        public string sactcode { get; set; }
        public DateTime openddate { get; set; }
        public decimal rqty { get; set; }
        public decimal fob { get; set; }
        public decimal invvalu { get; set; }
        public decimal advance { get; set; }
        public decimal cduty { get; set; }
        public decimal vat { get; set; }
        public decimal ait { get; set; }
        public decimal scm { get; set; }
        public decimal freight { get; set; }
        public decimal insurance { get; set; }
        public decimal others { get; set; }
        public decimal total { get; set; }
        public decimal lcost { get; set; }
     }

    [Serializable]
    public class EClassLCMGT
    {
        public string comcod { get; set; }
        public string actcode { get; set; }
        public string actdesc { get; set; }
        public string storid { get; set; }
        public string stordesc { get; set; }
        public string rcvno { get; set; }
        public string grrno { get; set; }
        public string lotno { get; set; }
        public DateTime expdate { get; set; }
        public DateTime rcvdate { get; set; }
        public string rescod { get; set; }
        public string resdesc { get; set; }
        public string spcfcod { get; set; }
        public string spcfdesc { get; set; }
        public string unit { get; set; }
        public double rcvqty { get; set; }
        public double qcqty { get; set; }
        public double ordrqty { get; set; }
        public double freeqty { get; set; }
        public double rcvuptolast { get; set; }
        public double remainordr { get; set; }
        public double trate { get; set; }
        public double revamt { get; set; }
        public double preqcqty { get; set; }
        public double remqty { get; set; }
        public string remarks { get; set; }


    }
}

