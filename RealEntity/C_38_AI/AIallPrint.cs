using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_38_AI
{
    public class AIallPrint
    {

        [Serializable]

        public class InvoicePrint
        {
            
             public string sircode { get; set; }
             public string prjid { get; set; }
             public string invno { get; set; }
             public string customer { get; set; }
             public string projectname { get; set; }
            
             public string dataset { get; set; }
             public string nationality { get; set; }
             public string city { get; set; }
             public string addres { get; set; }
            public string notes { get; set; }

            public double qty { get; set; }
            public double totalrate { get; set; }
            public DateTime invoicedate { get; set; }
             public DateTime duedate { get; set; }
             public string subjects { get; set; }
             
             public double totalamount { get; set; }

        }

        [Serializable]

        public class RptOngoingProject
        {
  
            public string prjnamebatchwise { get; set; }
            public string client { get; set; }
            public string responsibility { get; set; }
            public string annotortype { get; set; }
            public string unit { get; set; }
            public double rate { get; set; }
            public double qtyofwork { get; set; }
            public double expusd { get; set; }
            public double annovalueper { get; set; }
            public double annotperday { get; set; }
            public double annotdept { get; set; }
            public double inhouse { get; set; }
            public double contact { get; set; }
            public double total { get; set; }
            public double totalfeelan { get; set; }
            public double totalannotor { get; set; }
            public double qadepl { get; set; }
            public double reqquestday { get; set; }
            public double holidays { get; set; }
            public double extraordy { get; set; }
            public DateTime startdat { get; set; }
            public DateTime finishdat { get; set; }
            public DateTime qatime { get; set; }
            public DateTime clientdate { get; set; }
            public DateTime submissiondate { get; set; }
           
            public RptOngoingProject()
            {

            }


        }
    }
}
