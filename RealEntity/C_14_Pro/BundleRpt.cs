using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_14_Pro
{
   public class BundleRpt
    {
       public string rowid { get; set; }
       public string comcod { get; set; }
       public string billno { get; set; }
       public string billno1 { get; set; }
       public string billref { get; set; }
       public DateTime billdate { get; set; }
       public string billdate1 { get; set; }
       public string ssircode { get; set; }
       public string orderno { get; set; }
       public string orderno1 { get; set; }
       public double billamt { get; set; }
       public string rsirdesc { get; set; }
       public string ssirdesc { get; set; }
       public string pactdesc { get; set; }
       public string itemcount { get; set; }
       public string ncode { get; set; }
       public string ndesc { get; set; }
    }
    [Serializable]
   public class suppbgd
   {
        public string pactdesc { get; set; }
        public string ssirdesc { get; set; }
        public double pendingamt { get; set; }
        public double totalbill { get; set; }
        public double billamt { get; set; }
        public double payment { get; set; }
        public double netpayable { get; set; }
      

   }

    #region
    [Serializable]
    public class RptSuppPayable
    {



        public string comcod { get; set; }
        public string mwgcod { get; set; }
        public string rescode { get; set; }
        public double opnam { get; set; }
        public double cram { get; set; }
        public double periodam { get; set; }
        public double netbal { get; set; }
        public string resdesc { get; set; }
        public string mwgdesc { get; set; }


        public RptSuppPayable()
        {
        }



    }

    #endregion
    #region
    [Serializable]
    public class RptSuppDueStatus
    {



        public string comcod { get; set; }        
        public string billno { get; set; }
        public string billdate { get; set; }
        public double paidamt { get; set; }
        public double unpaid { get; set; }
        public double asingdays { get; set; }
        public RptSuppDueStatus()
        {
        }



    }

    #endregion

    #region
    [Serializable]
    public class RptDayWiseAdv
    {



        public string comcod { get; set; }
        public string actdesc { get; set; }
        public string sirdesc { get; set; }
        public string orderno1 { get; set; }
        public string pordref { get; set; }
        public string orderdat { get; set; }
        public string reqno1 { get; set; }
        public string mrfno { get; set; }
        public double advamt { get; set; }
        public string pordnar { get; set; }


           
        public RptDayWiseAdv()
        {
        }



    }

    #endregion
    #region
    [Serializable]
    public class RptAdvVsPayment
    {



        public string comcod { get; set; }
        public string pactdesc { get; set; }
        public string ssirdesc { get; set; }
        public string orderno2 { get; set; }
        public string orderdat { get; set; }
        public double orderamt { get; set; }
        public double advamt { get; set; }
        public string vounum { get; set; }
        public double vouamt { get; set; }
        public string chequeno { get; set; }
        public string bank { get; set; }


        

        public RptAdvVsPayment()
        {
        }



    }

    [Serializable]
    public class RptOrderVsReceived
    {
        public string comcod { get; set; }
        public string pactdesc { get; set; }
        public string rsirdesc { get; set; }
        public string reqno1 { get; set; }
        public string reqdat { get; set; }
        public string mrfno { get; set; }
        public string mrrno1 { get; set; }
        public string mrrdat { get; set; }
        public string mrrref { get; set; }
        public string orderno { get; set; }
        public string orderno1 { get; set; }
        public string orderdat { get; set; }
        public string pordref { get; set; }
        public double ordrqty { get; set; }
        public double mrrqty { get; set; }
        public double aprovrate { get; set; }
        public double amount { get; set; }

        public RptOrderVsReceived()
        {
        }
    }

    #endregion
    #region
    [Serializable]
    public class RptSubBillDetails
    {



        public string comcod { get; set; }
        public string pactdesc { get; set; }
        public string ssirdesc { get; set; }
        public string billno1 { get; set; }
        public string billref { get; set; }
        public string clnno { get; set; }
        public string billdat { get; set; }
        public double billamt { get; set; }     
        public double percntge { get; set; }
        public double sdamt { get; set; }
        public double dedamt { get; set; }
        public double penamt { get; set; }
        public double netpayamt { get; set; }
        public double payment { get; set; }
        public double spayment { get; set; }
        public double advamt { get; set; }
        public double netpayable { get; set; }

        public RptSubBillDetails()
        {
        }
    }

   
   
    #endregion

    
    #region

    public class SupplierCheqHistory
    {
        public string bankname { get; set; }
        public double tamt { get; set; }
        public double dueam1 { get; set; }
        public SupplierCheqHistory()
        {
        }
    }


  
    [Serializable]
    public class SupplierCheqHistory01
    {
        public string comcod { get; set; }
        public string bankcode { get; set; }
        public string bankname { get; set; }
        public string rescode { get; set; }
        public string resdesc { get; set; }
        public double tamt { get; set; }
        public double dueam1 { get; set; }
        public double advamt { get; set; }
        public double dueam3 { get; set; }

        public SupplierCheqHistory01()
        {

        }


    }

    //a.comcod, a.rescode,resdesc=isnull(c.sirdesc,''), a.chequeno,a.chequedat, a.tamt,a.advamt, a.dueam1, a.dueam2, a.dueam3,a.dueam4, a.dueam5 

    [Serializable]
    public class SupplierCheqDetails
    {
        public string comcod { get; set; }
        public string rescode { get; set; }
        public string resdesc { get; set; }
        public string chequeno { get; set; }
        public DateTime chequedat { get; set; }
        public double tamt { get; set; }
        public double advamt { get; set; }
        public double dueam1 { get; set; }
        public double dueam2 { get; set; }
        public double dueam3 { get; set; }
        public double dueam4 { get; set; }
        public double dueam5 { get; set; }

        public SupplierCheqDetails()
        {

        }


    }
    #endregion
}

