using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEntity.C_17_Acc
{
  public  class EClassSpecification
    {


      [Serializable]
 
      public class EClassLastSpcfcodeofRes 
      {
          public string spcfcod { get; set; }

          public EClassLastSpcfcodeofRes() 
          {
          }

          public EClassLastSpcfcodeofRes(string spcfcod) 
          {

              this.spcfcod = spcfcod;
          
          }
      
      }

    }
}
