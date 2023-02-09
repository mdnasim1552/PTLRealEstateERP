using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_47_Kpi
{
    [Serializable]
    public class EntryKpi
    {
        public string actcode1 { get; set; }
        public string actdesc1 { get; set; }
        public string actcode { get; set; }
        public string actdesc { get; set; }
        public double acqty { get; set; }
        public double ppercent { get; set; }
        public string note { get; set; }
        public string remarks { get; set; }
        public double wQty { get; set; }

        public EntryKpi(string actcode1, string actdesc1, string actcode, string actdesc, double acqty, double ppercent, string note, string remarks,double wQty)
        {
            this.actcode1 = actcode1;
            this.actdesc1 = actdesc1;
            this.actcode = actcode;
            this.actdesc = actdesc;
            this.ppercent = ppercent;
            this.acqty = acqty;
            this.note = note;
            this.remarks = remarks;
            this.wQty = wQty;
        }

    }
    [Serializable]
    public class GradeWise
    {
        public string mrange { set; get; }
        public string mdescrip { set; get; }

        public GradeWise()
        { 
        
        }
        public GradeWise(string mrange, string mdescrip)
        {
            this.mrange = mrange;
            this.mdescrip = mdescrip;
        }

    }
}
