using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity
{
  public  class EClassCommon
    {

        [Serializable]
        public class EClassProject
        { 
        
        public string actcode { get; set; }
        public string actdesc { get; set; }

            public EClassProject()
            { 
            
            }

            public EClassProject(string actcode, string actdesc)
            {
                this.actcode = actcode;
                this.actdesc = actdesc;
            
            }



        }
    
    }
}
