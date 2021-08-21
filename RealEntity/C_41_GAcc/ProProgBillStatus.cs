using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_41_GAcc
{
    [Serializable] 
   public class ProProgBillStatus
    {
        public string comcod { get; set; }
        public string rowid { get; set; }
        public string actcode { get; set; }
        public string vounum { get; set; }
        public string vounum1 { get; set; }
        public string voudat1 { get; set; }
        public double billam { get; set; }
        public double netbillam { get; set; }
        public double recamt1 { get; set; }
        public double recamt2 { get; set; }
        public double recamt3 { get; set; }
        public string actdesc { get; set; }
        public ProProgBillStatus() { }
      
    }
}
