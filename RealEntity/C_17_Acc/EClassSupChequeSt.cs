using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
namespace RealEntity.C_17_Acc
{
    public class EClassSupChequeSt
    {


            public string vounum1 { set; get; }
            public string isunum { set; get; }
            public string  chequeno { set; get; }
            public string voudat1 { set; get; }
            public string chequedat { set; get; }
            public double chequeam { set; get; }

            public string clchequeno { set; get; }
            public string cldate { set; get; }
            public double clchequeam { set; get; }
            public string actdesc { set; get; }
            public string cactdesc { set; get; }
            public string resdesc { set; get; }






            public EClassSupChequeSt(string vounum1, string isunum, string chequeno, string voudat1, string chequedat, double chequeam, string clchequeno, string cldate, double clchequeam,
                    string actdesc, string cactdesc, string resdesc)
           
            {
                this.vounum1 = vounum1;
                this.isunum = isunum;
                this.chequeno = chequeno;
                this.voudat1 = voudat1;
                this.chequedat = chequedat;
                this.chequeam = chequeam;
                this.clchequeno = clchequeno;
                this.cldate = cldate;
                this.clchequeam = clchequeam;
                this.actdesc = actdesc;
                this.cactdesc = cactdesc;
                this.resdesc = resdesc;
               
            }
    }
}
