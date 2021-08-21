using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEntity
{
    public class ProjectName1
    {

        public string pactcode;
        public string pactdesc;
        public ProjectName1()
        {
           

        }
        public ProjectName1(string pactcode, string pactdesc) 
        {
            this.PactCode = pactcode;
            this.PactDesc = pactdesc;
        
        }
        public string PactCode {

            get { return pactcode; }
            set { this.pactcode=value; }
        
        }

        public string PactDesc{

            get { return pactdesc; }
            set { this.pactdesc = value; }
        }
    }
}
