using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEntity.C_39_MyPage
{


    public class EClassShowKPIData
    {
        public string kpigrpdesc { get; set; }
        public double stdkpival { get; set; }
        public double stdtarget { get; set; }
        public double actual { get; set; }
        public double mparcnt { get; set; }


        public EClassShowKPIData() { }
        public EClassShowKPIData(string kpigrpdesc, double stdkpival, double stdtarget, double actual, double mparcnt)
        {

            this.kpigrpdesc = kpigrpdesc;
            this.stdkpival = stdkpival;
            this.stdtarget = stdtarget;
            this.actual = actual;
            this.mparcnt = mparcnt;
        }

    }
    


}
