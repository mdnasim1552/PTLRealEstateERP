using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEntity.C_34_Mgt
{


    [Serializable]
     public class EntityClassConversion
    {
        public string fcode { get; set; }
        public string tcode { get; set; }
        public string fcodedesc { get; set; }
        public string tcodedesc { get; set; }
        public double conrate { get; set; }
        public double conrate1 { get; set; }

        public EntityClassConversion(string fcode, string tcode, string fcodedesc, string tcodedesc, double conrate, double conrate1) 
        {
            this.fcode = fcode;
            this.tcode = tcode;
            this.fcodedesc = fcodedesc;
            this.tcodedesc = tcodedesc;
            this.conrate = conrate;
            this.conrate1 = conrate1;
        
        }



    }


    [Serializable]
    public class EclassUnitConversion
    {
        public string bcod { get; set; }
        public string bcodesc { get; set; }
        public string ccod { get; set; }
        public string ccodesc { get; set; }
        public double conrat { get; set; }
        public string remarks { get; set; }
        public EclassUnitConversion() { }
        public EclassUnitConversion(string bcod, string bcodesc, string ccod, string ccodesc, double conrat, string remarks)
        {
            this.bcod = bcod;
            this.bcodesc = bcodesc;
            this.ccod = ccod;
            this.ccodesc = ccodesc;
            this.conrat = conrat;
            this.remarks = remarks;
        }

    }
}


