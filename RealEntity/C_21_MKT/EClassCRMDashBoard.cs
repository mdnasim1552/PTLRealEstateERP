using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealERPEntity.C_21_MKT
{
  public  class EClassCRMDashBoard
    {


        [Serializable]
        public class EClassPwiseSum
        {

           public string prjcode { get; set; }
            public int total { get; set; }
            public string prjdesc { get; set; }

            public  EClassPwiseSum()
            { 
            
            
            }

        }

        [Serializable]
        public class EClassSourceWiseSum
        {

            public string sourcecode { get; set; }
            public int total { get; set; }
            public string sourcedesc { get; set; }

            public EClassSourceWiseSum()
            {


            }

        }


        [Serializable]
        public class EClassLeadWiseSum
        {

            public string leadcode { get; set; }
            public int total { get; set; }
            public string leadst { get; set; }

            public EClassLeadWiseSum()
            {


            }

        }


    }
}
