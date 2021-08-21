using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_47_Kpi
{
   public class EClassEMPKPI
    {

       public class EClassProject 
       {
        public string pactcode { get; set; }
        public string pactdesc { get; set; }

        public EClassProject(string pactcode, string pactdesc)
        {

            this.pactcode = pactcode;
            this.pactdesc = pactdesc;
        }
       
       
       }

       public class EClassActivities
       {
           public string actcode { get; set; }
           public string actdesc { get; set; }

           public EClassActivities(string actcode, string actdesc)
           {

               this.actcode = actcode;
               this.actdesc = actdesc;
           }


       }

    }
}
