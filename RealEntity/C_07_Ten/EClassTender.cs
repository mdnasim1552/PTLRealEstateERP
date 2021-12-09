using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_07_Ten
{
        [Serializable]
        public class RptCivilConBOQ
        {
            public string comcod { get; set; }
            public  string actcode { get; set;}
            public string boqid { get; set; }
            public DateTime boqdate { get; set; }
            public decimal sbtrate_per { get; set; }
            public decimal actamt_per { get; set; }
            public decimal  costvatoh_per  { get; set; }
            public string postedbyid { get; set; }
            public string   postseson{ get; set; }
            public DateTime posteddat { get; set; }
            public string subcode { get; set; }
            public string itemcode { get; set; }
            public decimal qty { get; set; }
            public decimal rate { get; set; }
            public decimal ordam { get; set; }
            public decimal sbtamt { get; set; }
            public decimal sbtrate { get; set; }
            public decimal ohamt { get; set; }
            public decimal ttamt { get; set; }
            public decimal taxvatamt { get; set; }
            public decimal costvatoh { get; set; }
            public decimal actamt { get; set; }
            public decimal diffamt { get; set; }
            public int baseUnit { get; set; }
            public string actdesc { get; set; }
            public string subdesc { get; set; }
            public string sdetails { get; set; }
            public decimal convrate { get; set; }
            public int itemid { get; set; }
            public string unit { get; set; }
            public string grpdesc { get; set; }
            public  string grpcode { get; set; }
            public RptCivilConBOQ() { }
        }
}
