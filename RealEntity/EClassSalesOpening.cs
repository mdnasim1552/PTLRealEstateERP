using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEntity
{
    public  class EClassSalesOpening
    {
        public string pactcode;
        public string pactdesc;
        public double opnamt;

        public string PactCode 
        {
            get {
                return pactcode;
            
            }
            set {
                this.pactcode = value;
            
            }        
        
        }

        public string PactDesc 
        {

            get {
                return pactdesc;
            }

            set {
                this.pactdesc = value;
            }
        }

        public double OpnAmt 
        {
            get {
                return opnamt;
            }
            set {
                this.opnamt = value;
            }
        
        }
        public EClassSalesOpening()
        {
           

        }
        public EClassSalesOpening(string pactcode, string pactdesc, double  opnamt)
              
        {
         this.PactCode =pactcode ;
         this.PactDesc=pactdesc;
         this.OpnAmt=opnamt;
        
        }
            
            
            
      
    }
}
