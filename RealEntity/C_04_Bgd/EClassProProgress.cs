using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
namespace RealEntity.C_04_Bgd
{
    public class EClassProProgress
    {


            public string pactcode { set; get; }
            public string pactdesc { set; get; }
            public double  conprogress { set; get; }
            public string comdate { set; get; }
            public string catagory { set; get; }
        
            

            public EClassProProgress() 
            { }

            public EClassProProgress(string pactcode, string pactdesc, double conprogress, string comdate, string catagory)
           
            {
                this.pactcode = pactcode;
                this.pactdesc = pactdesc;
                this.conprogress = conprogress;
                this.comdate = comdate;
                this.catagory = catagory;
            }
    }
}
