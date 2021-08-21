using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_24_CC
{
    [Serializable]
   public class RptSalesPermissionCost
    {
        public string regdesc { get; set; }
        public double recamt { get; set; }
        public double acamt { get; set; }
    }
    [Serializable]
    public class Landinfo
    {
        public string udesc { get; set; }
        public string munit { get; set; }
        public double usize { get; set; }
        public double fsize { get; set; }
        public string customer { get; set; }
        public double noflat { get; set; }
        public double totaland { get; set; }
        public string gunit { get; set; }
        public double nocar { get; set; }
    }
}
