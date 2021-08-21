using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace RealEntity.F_35_MgtAct
{
     public class EClassMisOverall
    {


            public string actcode { set; get; }
            public string actdesc { set; get; }
            public double amt01 { set; get; }
            public double amt02 { set; get; }
            public double amt03 { set; get; }
           

            //public EClassMisOverall() 
            //{ }

            public EClassMisOverall(string actcode, string actdesc, double amt01, double amt02, double amt03)
           
            {
                this.actcode = actcode;
                this.actdesc = actdesc;
                this.amt01 = amt01;
                this.amt02 = amt02;
                this.amt03 = amt03;
                
            
            
            }
    }
}
