using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_17_Acc
{
   public class EClassAccVoucher
    {


     
       [Serializable]
       public class EClassResHead
       {
           public string rescode { set; get; }
           public string resdesc { set; get; }
           public string resdesc1 { set; get; }
           public string resunit { set; get; }

           public EClassResHead(string rescode, string resdesc, string resdesc1, string resunit)
           {
               this.rescode = rescode;
               this.resdesc = resdesc;
               this.resdesc1 = resdesc1;
               this.resunit = resunit;
              
           }
       }




       [Serializable]
       public class EClassAccHead
       {
           public string actcode { set; get; }
           public string actdesc { set; get; }
           public string actdesc1 { set; get; }
           public string actelev { set; get; }
           public string acttype { set; get; }

            public EClassAccHead() { }
           public EClassAccHead(string actcode, string actdesc, string actdesc1, string actelev, string acttype)
           {
               this.actcode = actcode;
               this.actdesc = actdesc;
               this.actdesc1 = actdesc1;
               this.actelev = actelev;
               this.acttype = acttype;

           }
       }




       [Serializable]
       public class EClassVoucher
       {
           public string vounum { set; get; }
           public string vounum1 { set; get; }


           public EClassVoucher(string vounum, string vounum1)
           {
               this.vounum = vounum;
               this.vounum1 = vounum1;
               

           }
       }

       [Serializable]
       public class EClassGenReq
       {
           public string pactdesc { set; get; }
           public string reqno1 { set; get; }
           public DateTime reqdat { set; get; }
           public string mrfno { set; get; }
           public double proamt { set; get; }
           public double appamt { set; get; }
           
           public string aprname { set; get; }
           public string aprfname { set; get; }
           public string reqname { set; get; }
           public decimal payment { set; get; }


           public  EClassGenReq ()  { }
           //public EClassGenReq(string pactdesc, string reqno1, DateTime reqdat, string mrfno, double proamt, double appamt, string aprname, string aprfname)
           //{
           //     this.pactdesc = pactdesc;
           //     this.reqno1 = reqno1;
           //     this.reqdat = reqdat;
           //     this.mrfno = mrfno;
           //     this.proamt = proamt;
           //     this.appamt = appamt;
           //     this.aprname = aprname;
           //     this.aprfname = aprfname;
              
           //}
         

       }

         [Serializable]
       public class VoutopSheet
       {
            public string usrid { set; get; }
            public string usrname { set; get; }
           public string vounum2 { set; get; }
           public string vounum1 { set; get; }
           public DateTime voudat { set; get; }
           public string refnum { set; get; }
           public string chequedat { set; get; }
           public string actdesc { set; get; }
           public string supdesc { set; get; }
           public string venar { set; get; }
           public string payto { set; get; }
           public string isunum { set; get; }
           public double amount { set; get; }
           public double amt { set; get; }
           public VoutopSheet() { }
           
       }

         [Serializable]
         public class VouTopSheetSum
         {
            public string usrid { set; get; }

            public string usrname { set; get; }
             public double pdcvou { get; set; }
             public double cashvou { get; set; }
             public double bankvou { get; set; }
             public double contravou { get; set; }
             public double jourvou { get; set; }
             public double tonum { get; set; }
             public VouTopSheetSum() { }

         }


    }
}
