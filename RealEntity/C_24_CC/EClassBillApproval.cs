using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_24_CC
{
    [Serializable]
    public class RptClientModification
    {

        public string comcod { get; set; }
        public string adno { get; set; }
        public string adno1 { get; set; }
        public DateTime addate { get; set; }
        public string pactdesc { get; set; }
        public string pactcode { get; set; }
        public string usircode { get; set; }
        public string udesc { get; set; }
        public string cusname { get; set; }
        public string gcod { get; set; }
        public string gdesc { get; set; }
        public decimal amt { get; set; }
        public decimal disamt { get; set; }
        public decimal netamt { get; set; }
        public RptClientModification() { }
    }
}
