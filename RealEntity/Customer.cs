using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEntity
{
    public class Customer
    {
        private string custname;
        private string custid;
        private string custadd;
        public string CustName 
        {
            get
            { return custname; 
            }

            set 
            { this.custname = value; 
            
            }
        
        
        }

        public string CustId
        {
            get
            {
                return custid;
            }

            set
            {
                this.custid = value;

            }


        }

        public string CustAdd
        {
            get
            {
                return custadd;
            }

            set
            {
                this.custadd = value;

            }


        }


    }
}
