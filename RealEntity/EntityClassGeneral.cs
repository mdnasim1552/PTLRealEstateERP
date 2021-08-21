using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEntity
{
    public class EntityClassGeneral
    {
    }


    public class EClassResource 
    {

        public string ResCode { get; set; }
        public string ResDesc { get; set; }

        public EClassResource(string rescode, string resdesc) 
        {

            this.ResCode = rescode;
            this.ResDesc = resdesc;
        
        }
    
    }
}
