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
             public string batchid { get; set; }
             public string sircode { get; set; }
             public string prjid { get; set; }
             public string invno { get; set; }
             public string customer { get; set; }
             public string projectname { get; set; }
             public string batchname { get; set; }
             public string dataset { get; set; }
             public string nationality { get; set; }
             public string city { get; set; }
             public string addres { get; set; }
            public string notes { get; set; }

            public double qty { get; set; }
             public DateTime invoicedate { get; set; }
             public DateTime duedate { get; set; }
             public string subjects { get; set; }
             public double totalrate { get; set; }
             public double totalamount { get; set; }

        }


    }
}
