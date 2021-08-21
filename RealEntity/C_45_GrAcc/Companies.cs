using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_45_GrAcc
{
   public class Companies
   {
       public string comcod { set; get;}
       public string comsnam { set; get;}
   }

   [Serializable]
   public class GetAttachedDocs
   {
       public string id { set; get; }
       public string refno { set; get; }
       public string itemsurl { set; get; }

       public GetAttachedDocs()
       {

       }

       public GetAttachedDocs(string id, string refno, string itemsurl)
       {
           this.id = id;
           this.refno = refno;
           this.itemsurl = itemsurl;

       }

    }
}
