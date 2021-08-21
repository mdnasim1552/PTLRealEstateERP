using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealERPLIB
{
   public  class classracer
    {

        private string name;
        public string Name 
        {
            get 
            {
                return name;
            
            }
        
        }

        private string car;
        public string Car 
        {
            get
            {
                return car;

            }
        
        }

        public classracer(string name1, string car1) 
        {
            this.name = name1;
            this.car = car1;
        
        }

        public override string ToString()
        {
            return name + " " + car;
        }






    }
}
