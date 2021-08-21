using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealERPLIB
{
    public class ClassBase
    {

        public virtual double VirtualMethod(int rad)
 
        {
            double area = 3.14 * (rad * rad);
            return area;
        
        }
    }
}
