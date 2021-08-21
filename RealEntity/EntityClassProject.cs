using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEntity
{
    public class EntityClassProject
    {
        public string pactcode;
        public string pactdesc;

        public string PactCode 
        {
            get { return pactcode; }
            set { this.pactcode = value; }
        }
        public string PactDesc
        {
            get { return pactdesc; }
            set { this.pactdesc = value; }
        }

        public EntityClassProject()
        {
            


        }
        public EntityClassProject(string pactcode, string pactdesc) 
        {
            this.pactcode = pactcode;
            this.pactdesc = pactdesc;
        
        
        }
    }
}
