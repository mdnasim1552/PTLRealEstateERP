using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_09_LCM
{
   public class BO_AllLCInfo
    {
       [Serializable]
       public class AllLCInfolist
       {
           public string actcode { set; get; }
           public string actdesc1 { set; get; }            
           public string lcdate{set; get;}
           public string shipdate { set; get; }
           public string shipardate { set; get; }
           public string deldate { set; get; }
           public string expdate { set; get; }
           public double cnvrsion { set; get; }
           public double fcamt { set; get; }
           public double bdamt { set; get; }
           public string bankname { set; get; }
           public string actdesc { set; get; }
           public string currency { set; get; }
           public string curdesc { set; get; }
           public string csplname { set; get; }
           public string cspldesc { set; get; }
           public AllLCInfolist(string actdesc1,string actcode, string lcdate, string shipdate, string shipardate, string deldate, string expdate, double cnvrsion, double fcamt, double bdamt, string bankname, string actdesc,
               string currency, string curdesc, string csplname, string cspldesc)
           {
               this.actcode = actcode;
                this.actdesc1 = actdesc1;
               this.lcdate = lcdate;
               this.shipdate = shipdate;
               this.shipardate = shipardate;
               this.deldate = deldate;
               this.expdate = expdate;
               this.cnvrsion = cnvrsion;
               this.fcamt = fcamt;
               this.bdamt = bdamt;
               this.bankname = bankname;
               this.actdesc = actdesc;
               this.currency = currency;
               this.curdesc = curdesc;
               this.csplname = csplname;
               this.cspldesc = cspldesc;
           }

       }
    }
}

