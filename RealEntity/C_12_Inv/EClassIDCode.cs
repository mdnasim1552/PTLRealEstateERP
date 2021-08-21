using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEntity.C_12_Inv
{
    public class EClassIDCode
    {
        public string typecode { get; set; }
        public string typedesc { get; set; }

        public EClassIDCode(string typecode, string typedeesc) 
        {

            this.typecode = typecode;
            this.typedesc = typedeesc;
        
        }




        public class EClasPurMrr
        {
            public string reqno1 { get; set; }
            public string rsirdesc1 { get; set; }
            public string spcfdesc { get; set; }
            public string rsirunit { get; set; }
            public string orderdat { get; set; }
            public decimal orderqty { get; set; }
            public decimal mrrqty { get; set; }
            public decimal orderbal { get; set; }
            public decimal recup { get; set; }
            public decimal mrrrate { get; set; }
            public decimal mrramt { get; set; }
            public string mrrnote { get; set; }
            public decimal chlnqty { get; set; }
            public string challandat { get; set; }
            public decimal oqty { get; set; }
            public string reqdat { get; set; }
            public string mrfno { get; set; }
            public decimal areqty { get; set; }
            public string pordref { get; set; }




           public  EClasPurMrr() { }

        }


    }
}
